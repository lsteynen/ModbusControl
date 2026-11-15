using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using RJCP.IO.Ports;
using LST.Helpers;

namespace LST.Modbus
{
    public class SerialChannel : ICommChannel, IDisposable
    {
        // Port settings (a fresh SerialPortStream object is created on every Open())
        private readonly string _portName;
        private readonly int _baudrate;
        private readonly Parity _parity;
        private readonly int _dataBits;
        private readonly StopBits _stopBits;

        // Session state. Open()/Close() are serialized by _stateLock.
        private readonly object _stateLock = new object();
        private volatile SerialPortStream mPort;
        private Thread ReaderThread;
        private CancellationTokenSource _cts;
        private volatile bool _running;

        private const int ReaderChunkSize = 1024;
        private const int ReaderPollTimeoutMs = 50;     // SerialPort.ReadTimeout; also the max delay for the reader to notice Close()
        private const int ReaderJoinTimeoutMs = 1000;
        private const int PortCloseTimeoutMs = 3000;

        // Receive buffer shared between reader thread and ReadWait(). ALWAYS use RxQueueLock for it.
        private readonly Queue<byte> RxQueue = new Queue<byte>();
        private readonly int RxQueueMaxSize = 400;
        private readonly object RxQueueLock = new object();
        
        private volatile bool _disposed;

        /// <summary>Raised (on the reader thread) when the port fails, e.g. USB adapter unplugged. Call Close() to clean up.</summary>
        public event Action<Exception> Faulted;

        // constructor
        public SerialChannel(string portName, uint baudrate, uint dataBits, Parity parity, StopBits stopBits)
        {
            _portName = portName;
            _baudrate = (int)baudrate;
            _dataBits = (int)dataBits;
            _parity = parity;
            _stopBits = stopBits;
        }

        // properties

        public string ChannelName
        {
            // read only
            get { return _portName; }
        }

        public bool IsOpen
        {
            get
            {
                SerialPortStream port = mPort;
                return _running && (port != null) && port.IsOpen;
            }
        }

        public bool IsSerialChannel
        {
            get { return true; }
        }

        public bool IsTcpChannel
        {
            get { return false; }
        }

        public uint ConnectTimeout
        {
            // is not used here
            get { return 0; }
            set { /* ignore */ }
        }

        /// <summary>
		/// Time (ms) after the end of Write() in which the first byte of the response must arrive. MUST be set to a non-zero value.
		/// </summary>
        public uint ResponseTimeout
        {
            get;
            set;
        }

        /// <summary>
        /// Max time (ms) between two received bytes once a response has started.
        /// </summary>
        public uint MoreTimeout
        {
            get;
            set;
        }

        public uint Baudrate
        {
            // read only
            get { return (uint)_baudrate; }
        }

        public uint ConnectCount
        {
            // not used here but must be implemented
            get;
            set;
        }

        public uint ConnectFailCount
        {
            // not used here but must be implemented
            get;
            set;
        }

        public uint ConnectionLostCount
        {
            // not used here but must be implemented
            get;
            set;
        }

        // events
        public delegate void LogCallbackString(string s);
        public event TxRxCallbackHandler OnDataSent;
        public event TxRxCallbackHandler OnDataReceived;
        public event LogCallbackString OnWarning;

        // methods

        /// <summary>
        /// Open serial port.
        /// Can throw; after a failed Open() the object is unchanged and Open() can be retried.
        /// </summary>
        public void Open()
        {
            lock (_stateLock)
            {
                ThrowIfDisposed();

                if (mPort != null)
                    throw new InvalidOperationException("SerialChannel is already open.");

                // Use a local until everything succeeded, so a failure leaves no half-initialized state behind
                SerialPortStream port = new SerialPortStream();
                port.PortName = _portName;
                port.BaudRate = _baudrate;
                port.DataBits = _dataBits;
                port.Parity = _parity;
                port.StopBits = _stopBits;
                port.ReadTimeout = ReaderPollTimeoutMs;
                port.ReadBufferSize = 16384;
                port.WriteTimeout = 1000;

                try
                {
                    port.Open();
                    port.DiscardInBuffer();
                }
                catch
                {
                    try { port.Dispose(); } catch { }
                    throw;
                }

                ClearReceiveBuffer();

                var cts = new CancellationTokenSource();
                CancellationToken token = cts.Token;

                // Init receiver thread
                // The reader works only with its own port/token, never with the fields
                Thread thread = new Thread(() => ReaderThread_Work(port, token));
                thread.Name = "SerialChannel.Reader";
                thread.IsBackground = true;   // a stuck driver call must never keep the process alive

                _cts = cts;
                ReaderThread = thread;
                mPort = port;
                _running = true;
                thread.Start();
            }
        }

        /// <summary>
        /// Stops the reader thread and closes the port. Safe to call multiple times and from any thread
        /// (including the Faulted handler). Returns within a few seconds even if the driver hangs.
        /// </summary>
        public void Close()
        {
            Debug.Print(DateTime.Now.TimeOfDay + ": SerialChannel Close() 1");

            lock (_stateLock)
            {
                SerialPortStream port = mPort;
                if (port == null)
                    return;

                // 1) Tell the reader to stop and wake any ReadWait() callers
                _running = false;
                _cts.Cancel();
                lock (RxQueueLock)
                    Monitor.PulseAll(RxQueueLock);

                Debug.Print(DateTime.Now.TimeOfDay + ": SerialChannel Close() 2");

                // 2) Wait for the reader to leave Read() (it polls every ReaderPollTimeoutMs).
                //    Skip if we ARE the reader thread (Close() called from the Faulted handler).
                Thread reader = ReaderThread;
                if (reader != null
                    && reader.IsAlive
                    && Environment.CurrentManagedThreadId != reader.ManagedThreadId)
                {
                    reader.Join(ReaderJoinTimeoutMs);
                }

                // delay between stopping the reader and closing the port, to avoid a race condition in the driver (USB adapter unplugged). The reader may still be in Read() for a few ms after Join() returns.
                Thread.Sleep(50);  

                // 3) Close the port, 
                try
                {
                    Debug.Print(DateTime.Now.TimeOfDay + ": SerialChannel Close() 3");
                    port.Close();
                    Debug.Print(DateTime.Now.TimeOfDay + ": SerialChannel Close() 4");
                    port.Dispose();
                    Debug.Print(DateTime.Now.TimeOfDay + ": SerialChannel Close() 5");

                }
                catch
                {
                    // ignore errors during shutdown
                    Debug.Print(DateTime.Now.TimeOfDay + ": SerialChannel Close() EXCEPTION");
                }
                
                /*
                // 3) Close the port, in separate thread and guarded by a timeout so a misbehaving driver can't hang the app.
                //    Uses the LOCAL 'port', so an abandoned task still closes the right object.                																				
                Task closeTask = Task.Run(() =>
                {
                    try
                    {
                        port.Close();
                        port.Dispose();
                    }
                    catch
                    {
                        // ignore errors during shutdown
                    }
                });

                if (!closeTask.Wait(PortCloseTimeoutMs))
                {
                    // Driver is stuck in Close(). Abandon this SerialPortStream object (a new one is created on
                    // the next Open()). The task runs on a pool (background) thread, so it will not
                    // keep the process alive.
                    Debug.WriteLine("SerialChannel: port.Close() timed out, abandoning port.");
                }
                */

                Debug.Print(DateTime.Now.TimeOfDay + ": SerialChannel Close() 6");

                // 4) Reset session state
                mPort = null;
                ReaderThread = null;
                _cts.Dispose();
                _cts = null;
                ClearReceiveBuffer();
            }
        }

        /// <summary>
        /// Final shutdown: closes the port. Cannot be re-opened afterwards.
		/// </summary>
        public void Dispose()
        {
            if (_disposed)
                return;
            _disposed = true;

            Close();
        }

        public void ClearReceiveBuffer()
        {
            lock (RxQueueLock)
            {
                RxQueue.Clear();
            }
        }

        /// <summary>
        /// If there are unexpected bytes in the receive queue: log this and discard them.
        /// Returns false if the channel is not open (no transactions possible).
        /// </summary>
        public bool BeforeTransaction()
        {
            bool open = IsOpen;
            if (open)
            {
                DiscardUnexpectedBytes();
            }
            return open;
        }

        /// <summary>
        /// If we received more bytes than we expected: log this and discard them
        /// </summary>
        public void AfterTransaction()
        {
            bool open = IsOpen;
            if (open)
            {
                DiscardUnexpectedBytes();
            }
        }

        /// <summary>
        /// Check if there are unexpected bytes in the receive queue, if yes remove them and report
        /// </summary>
        private void DiscardUnexpectedBytes()
        {
            byte[] garbage = null;

            lock (RxQueueLock)
            {
                if (RxQueue.Count > 0)
                {
                    garbage = RxQueue.ToArray();
                    RxQueue.Clear();
                }
            }

            if (garbage != null)
            {
                string s = "Received " + garbage.Length + " unexpected bytes: " + BytesToString.Convert(garbage, garbage.Length) + ", discarded";
                RaiseWarning(s);
            }
        }

        /// <summary>
        /// Sends a frame and logs it. 
		/// Can throw (not open, write timeout, port removed).
        /// </summary>
        public void Write(byte[] data, int start, int length)
        {
            ThrowIfDisposed();

            SerialPortStream port = mPort;      // local copy: Close() may null the field at any time
            if (port == null || !_running)
                throw new InvalidOperationException("SerialChannel is not open.");

            port.Write(data, start, length);

            // log sent data
            TxRxCallbackHandler handler = OnDataSent;
            if (handler != null)
            {
                try
                {
                    byte[] sent = new byte[length];
                    Buffer.BlockCopy(data, start, sent, 0, length);
                    handler(sent, length);
                }
                catch
                {
                    /* a logging problem must not break communication */
                }
            }
        }

        /// <summary>
        /// Read 'count' bytes from the rx queue. Waits until 'count' bytes are in the rx queue or the timeout expires.
        /// Returns exactly 'count' bytes on success, on timeout it returns the number of bytes that did arrive, possibly 0.
        /// </summary>
        /// <param name="buffer">where to store received bytes</param>
        /// <param name="count">number of bytes to read</param>
        /// <param name="useResponseTimeout">use ResponseTimeout for first response bytes, use MoreTimeout for subsequent bytes</param>
        /// <returns>number of bytes read</returns>
        public int ReadWait(ref byte[] buffer, int count, bool useResponseTimeout)
        {
            Debug.Print(DateTime.Now.TimeOfDay + ": SerialChannel.ReadWait() enter");

            if (buffer == null || count < 0 || count > buffer.Length)
                throw new ArgumentException("buffer is too small for count", nameof(count));

            // determine timeout
            int timeout = useResponseTimeout ? (int)ResponseTimeout : (int)MoreTimeout;

            Stopwatch sw = Stopwatch.StartNew();
            int n = 0;      // number of bytes received so far

            // wait till we received all bytes, or timeout
            lock (RxQueueLock)
            {
                while (true)
                {
                    // copy whatever is in the queue now, up to the number of bytes we still need
                    int before = n;
                    while (n < count && RxQueue.Count > 0)
                    {
                        buffer[n++] = RxQueue.Dequeue();
                    }

                    // done?
                    if (n >= count || !_running) 
                        break;

                    Debug.Print(DateTime.Now.TimeOfDay + $": SerialChannel loop, got {n} of {count}");

                    // received bytes? then switch to MoreTimeout
                    if (n > before)
                    {
                        sw.Restart();
                        timeout = (int)MoreTimeout;
                    }

                    // timeout?
                    int remainingMs = (int)timeout - (int)sw.ElapsedMilliseconds;
                    if (remainingMs <= 0)
                        break;

                    // not enough yet: wait for more to come, with timeout
                    Monitor.Wait(RxQueueLock, remainingMs);
                }
                
                Debug.Print(DateTime.Now.TimeOfDay + ": SerialChannel.ReadWait(): got " + n + " of " + count + " bytes");

                return n;
            }
        }

        public int GetAvailableCount()
        {
            lock (RxQueueLock)
            {
                return RxQueue.Count;
            }
        }


        /// <summary>
        /// The reader thread. Uses only its own port/token (see Open()).
        /// </summary>
        private void ReaderThread_Work(SerialPortStream port, CancellationToken token)
        {
            byte[] buffer = new byte[ReaderChunkSize];

            while (!token.IsCancellationRequested)
            {
                int n = 0;
                try
                {
                    // Blocks until at least 1 byte is available or ReadTimeout expires,
                    // so bytes are seen (and timestamped) as soon as they arrive.
                    n = port.Read(buffer, 0, buffer.Length);
                }
                catch (TimeoutException)
                {
                    continue;   // nothing received; loop and re-check the cancel flag
                }
                catch (Exception ex) when (ex is IOException
                        || ex is InvalidOperationException				// includes ObjectDisposedException
                        || ex is UnauthorizedAccessException)
                {
                    // If for example a USB adapter is unplugged, Read throws on a background thread, catch here.
                    if (!token.IsCancellationRequested)
                        RaiseFaulted(ex);
                    return;
                }

                if (n <= 0)
                    continue;

                // log received data
                TxRxCallbackHandler handler = OnDataReceived;
                if (handler != null)
                {
                    try
                    {
                        handler(buffer, n);
                    }
                    catch
                    {
                        /* a logging problem must not kill the reader */
                    }
                }

                // Hand to ReadWait()
                lock (RxQueueLock)
                {
                    if (token.IsCancellationRequested)
                        return;   // closed meanwhile: don't leak old bytes into the next session

                    for (int i = 0; i < n; ++i)
                        RxQueue.Enqueue(buffer[i]);

                    // Bound memory: if the device floods us, keep only the newest bytes
                    int excess = RxQueue.Count - RxQueueMaxSize;
                    if (excess > 0)
                    {
                        for (int i = 0; i < excess; i++)
                            RxQueue.Dequeue();
                    }

                    Monitor.PulseAll(RxQueueLock);
                }
            }
        }

        private void RaiseWarning(string message)
        {
            try { OnWarning?.Invoke(message); } catch { }
        }

        private void RaiseFaulted(Exception ex)
        {
            try { Faulted?.Invoke(ex); } catch { }
        }

        private void ThrowIfDisposed()
        {
            if (_disposed)
                throw new ObjectDisposedException(GetType().Name);
        }

    }
}
