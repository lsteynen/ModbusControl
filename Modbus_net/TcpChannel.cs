using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace LST.Modbus
{
    public class TcpChannel : ICommChannel, IDisposable
    {
        // Connection settings (a fresh Socket is created for every connection attempt)
        private readonly IPEndPoint _endpoint;

        /// <summary>
        /// Everything that belongs to ONE connection: socket, reader thread, cancellation.
        /// The reader thread works only with its own Session, never with the channel fields, so an old
        /// reader can never touch a newer connection.
        /// </summary>
        private sealed class Session
        {
            public Socket Socket;
            public Thread Reader;
            public CancellationTokenSource Cts;
            /// <summary>False once Close() was called or once the connection is known to be dead.</summary>
            public volatile bool Alive;
        }

        // Open()/Close()/reconnect are serialized by _stateLock.
        private readonly object _stateLock = new object();
        private volatile Session _session;
        private volatile bool _isOpen;          // logically open (Open() succeeded, Close() not called). The connection itself may be down: see IsConnected.
        private volatile bool _disposed;

        private const int ReaderChunkSize = 1024;
        private const int ReaderPollTimeoutMs = 50;     // also the max delay for the reader to notice Close()
        private const int ReaderJoinTimeoutMs = 1000;
        private const int SendTimeoutMs = 1000;
        // Receive buffer shared between reader thread and ReadWait(). ALWAYS use RxLock for it.
        private readonly Queue<byte> RxQueue = new Queue<byte>(2000);
        private readonly int RxQueueMaxSize = 2000;
        private readonly object RxQueueLock = new object();

        /// <summary>
        /// Raised (on the reader thread) when the connection is lost (remote closed, reset, network down).
        /// Informational only: the channel stays open and BeforeTransaction() reconnects automatically.
        /// </summary>
        public event Action<Exception> Faulted;

        // constructor
        public TcpChannel(IPAddress ServerIp, UInt16 Port)
        {
            _endpoint = new IPEndPoint(ServerIp, Port);

            // init properties
            ConnectTimeout = 3000;
        }

        // properties

        public string ChannelName
        {
            get { return _endpoint.ToString(); }
        }

        /// <summary>
        /// True between a successful Open() and Close(). Note: the connection may be temporarily down
        /// (see IsConnected); BeforeTransaction() will try to restore it.
        /// </summary>
        public bool IsOpen
        {
            get { return _isOpen && !_disposed; }
        }

        /// <summary>True if there is a live connection right now.</summary>
        public bool IsConnected
        {
            get
            {
                Session s = _session;
                return s != null && s.Alive && s.Socket.Connected;
            }
        }

        public bool IsSerialChannel
        {
            get { return false; }
        }

        public bool IsTcpChannel
        {
            get { return true; }
        }

        public uint Baudrate
        {
            // not supported
            get { return 0; }
        }

        /// <summary>Max time (ms) to wait for a connect to succeed.</summary>
        public uint ConnectTimeout
        {
            get;
            set;
        }

        /// <summary>Max time (ms) to wait for the response. MUST be set to a non-zero value.</summary>
        public uint ResponseTimeout
        {
            get;
            set;
        }

        public uint MoreTimeout
        {
            get;
            set;
        }

        public uint ConnectCount
        {
            get;
            set;
        }

        public uint ConnectFailCount
        {
            get;
            set;
        }

        public uint ConnectionLostCount
        {
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
        /// Connect to the server.
        /// Can throw; after a failed Open() the object is unchanged and Open() can be retried.
        /// </summary>
        public void Open()
        {
            lock (_stateLock)
            {
                ThrowIfDisposed();

                if (_isOpen)
                    throw new InvalidOperationException("TcpChannel is already open.");

                try
                {
                    StartSession();
                }
                catch
                {
                    ++ConnectFailCount;
                    throw;
                }

                ++ConnectCount;
                _isOpen = true;
            }
        }

        /// <summary>
        /// Stops the reader thread and closes the connection. Safe to call multiple times and from any thread
        /// (including the Faulted handler). Note: if a (re)connect is in progress on another thread, this waits for it (max ConnectTimeout).
        /// </summary>
        public void Close()
        {
            lock (_stateLock)
            {
                _isOpen = false;
                StopSession();
            }
        }

        /// <summary>
        /// Final shutdown: closes the connection. Cannot be re-opened afterwards.
        /// </summary>
        public void Dispose()
        {
            if (_disposed)
                return;
            _disposed = true;

            Close();
        }

        /// <summary>
        /// Called before every transaction. Makes sure we are connected: if the channel is open but the connection
        /// was lost, it reconnects; if the channel is not open, it tries to open it. Never throws.
        /// Returns true if the channel is open and connected.
        /// </summary>
        public bool BeforeTransaction()
        {
            if (_disposed)
                return false;

            // fast path: nothing to do
            if (_isOpen && IsConnected)
                return true;

            lock (_stateLock)
            {
                if (_disposed)
                    return false;

                if (_isOpen)
                {
                    if (!IsConnected)
                    {
                        // connection was lost
                        ++ConnectionLostCount;
                        RaiseWarning("Connection lost, reconnecting");

                        // throw away the dead connection (stops its reader thread), then connect again
                        StopSession();

                        try
                        {
                            StartSession();
                            ++ConnectCount;
                        }
                        catch (Exception)
                        {
                            ++ConnectFailCount;
                            RaiseWarning("Reconnect failed");
                        }
                    }

                    // We don't flush TCP channel buffers here
                }
                else
                {
                    // not open: try to open, don't throw from here
                    try
                    {
                        Open();
                    }
                    catch (Exception)
                    {
                    }
                }

                return _isOpen && IsConnected;
            }
        }

        public void AfterTransaction()
        {
            // nothing to do
        }

        /// <summary>
        /// Sends a frame and logs it.
        /// Can throw (not connected, write timeout, connection reset).
        /// </summary>
        public void Write(byte[] data, int start, int length)
        {
            ThrowIfDisposed();

            Session s = _session;       // local copy: Close() may null the field at any time
            if (s == null || !s.Alive)
                throw new InvalidOperationException("TcpChannel is not connected.");

            try
            {
                int sent = 0;
                while (sent < length)
                    sent += s.Socket.Send(data, start + sent, length - sent, SocketFlags.None);
            }
            catch (SocketException ex)
            {
                // the connection is broken: BeforeTransaction() will reconnect
                s.Alive = false;
                throw new IOException("Write to " + ChannelName + " failed: " + ex.Message, ex);
            }

            // log sent data
            TxRxCallbackHandler handler = OnDataSent;
            if (handler != null)
            {
                try
                {
                    byte[] sentCopy = new byte[length];
                    Buffer.BlockCopy(data, start, sentCopy, 0, length);
                    handler(sentCopy, length);
                }
                catch
                {
                    /* a logging problem must not break communication */
                }
            }
        }

        /// <summary>
        /// Read 'count' bytes from the rx queue. Waits until 'count' bytes are in the rx queue, the timeout expires,
        /// or the connection is closed/lost. Returns exactly 'count' bytes on success, otherwise the number of bytes
        /// that did arrive, possibly 0.
        /// </summary>
        /// <param name="buffer">where to store received bytes</param>
        /// <param name="count">number of bytes to read</param>
        /// <param name="useResponseTimeout">use ResponseTimeout for first response bytes, use MoreTimeout for subsequent bytes</param>
        /// <returns>number of bytes read</returns>
        public int ReadWait(ref byte[] buffer, int count, bool useResponseTimeout)
        {
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
                    while (n < count && RxQueue.Count > 0)
                    {
                        buffer[n++] = RxQueue.Dequeue();
                    }

                    // done?
                    if (n >= count)
                        break;

                    // closed or connection lost? Then nothing more will arrive, don't wait for the timeout.
                    // (checked AFTER draining the queue, so bytes received just before the disconnect are not lost)
                    Session s = _session;
                    if (s == null || !s.Alive)
                        break;

                    // timeout?
                    int remainingMs = timeout - (int)sw.ElapsedMilliseconds;
                    if (remainingMs <= 0)
                        break;

                    // not enough yet: wait for more to come, with timeout
                    Monitor.Wait(RxQueueLock, remainingMs);
                }

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

        // ------------------------------------------------------------------------------------------
        // session handling. Callers must hold _stateLock.
        // ------------------------------------------------------------------------------------------

        /// <summary>
        /// Connects and starts the reader thread. Can throw; on failure no state is changed.
        /// </summary>
        private void StartSession()
        {
            // Use locals until everything succeeded, so a failure leaves no half-initialized state behind
            Socket socket = new Socket(_endpoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                socket.SendTimeout = SendTimeoutMs;

                // Modbus frames are small request/response messages: don't let Nagle delay them
                socket.NoDelay = true;

                // don't linger
                // LingerState = true (enabled) with a zero timeout discards any pending data; Winsock resets the connection on close.
                socket.LingerState = new LingerOption(true, 0);

                ConnectWithTimeout(socket, _endpoint, (int)ConnectTimeout);
            }
            catch
            {
                try { socket.Close(); } catch { }
                throw;
            }

            ClearRxQueue();

            var cts = new CancellationTokenSource();
            var session = new Session { Socket = socket, Cts = cts, Alive = true };

            // Init receiver thread
            Thread thread = new Thread(() => ReaderThread_Work(session));
            thread.Name = "TcpChannel.Reader";
            thread.IsBackground = true;     // a stuck call must never keep the process alive
            session.Reader = thread;

            _session = session;
            thread.Start();
        }

        /// <summary>
        /// Stops the reader thread and closes the socket of the current session (if any).
        /// </summary>
        private void StopSession()
        {
            Session s = _session;
            if (s == null)
                return;

            // 1) Tell the reader to stop and wake any ReadWait() callers
            s.Alive = false;
            s.Cts.Cancel();
            lock (RxQueueLock)
                Monitor.PulseAll(RxQueueLock);

            // 2) Wait for the reader to leave its poll (it polls every ReaderPollTimeoutMs).
            //    Skip if we ARE the reader thread (Close() called from the Faulted handler).
            Thread reader = s.Reader;
            if (reader != null
                && reader.IsAlive
                && Environment.CurrentManagedThreadId != reader.ManagedThreadId)
            {
                reader.Join(ReaderJoinTimeoutMs);
            }

            // 3) Close the socket (also frees the OS resources if the reader did not stop in time)
            try
            {
                s.Socket.Close();
            }
            catch
            {
                // ignore errors during shutdown
            }

            // 4) Reset session state
            _session = null;
            s.Cts.Dispose();
            ClearRxQueue();
        }

        // ------------------------------------------------------------------------------------------
        // reader thread
        // ------------------------------------------------------------------------------------------

        /// <summary>
        /// The reader thread. Uses only its own session (see StartSession()).
        /// Blocks in Socket.Poll() so bytes are seen (and timestamped) as soon as they arrive, and a
        /// connection that is closed or reset by the remote side is detected within milliseconds.
        /// </summary>
        private void ReaderThread_Work(Session session)
        {
            Socket socket = session.Socket;
            CancellationToken token = session.Cts.Token;
            byte[] buffer = new byte[ReaderChunkSize];

            while (!token.IsCancellationRequested)
            {
                int n;
                try
                {
                    // Poll returns true when data is available OR when the connection was closed/reset by the peer.
                    // Timeout: nothing received; loop and re-check the cancel flag.
                    if (!socket.Poll(ReaderPollTimeoutMs * 1000, SelectMode.SelectRead))
                        continue;

                    n = socket.Receive(buffer, 0, buffer.Length, SocketFlags.None);
                }
                catch (Exception ex) when (ex is SocketException
                        || ex is InvalidOperationException)         // includes ObjectDisposedException
                {
                    // connection reset, network down, ... (or the socket was closed by Close())
                    if (!token.IsCancellationRequested)
                        ConnectionLost(session, ex);
                    return;
                }

                if (n <= 0)
                {
                    // 0 bytes after Poll() = orderly shutdown by the remote side
                    if (!token.IsCancellationRequested)
                        ConnectionLost(session, new IOException("Connection closed by remote host"));
                    return;
                }

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

        /// <summary>
        /// Called on the reader thread when the connection is dead. Marks the session as dead (the next
        /// BeforeTransaction() reconnects) and wakes up a waiting ReadWait().
        /// </summary>
        private void ConnectionLost(Session session, Exception ex)
        {
            session.Alive = false;
            lock (RxQueueLock)
                Monitor.PulseAll(RxQueueLock);

            RaiseFaulted(ex);
        }

        // ------------------------------------------------------------------------------------------
        // helpers
        // ------------------------------------------------------------------------------------------

        private void ClearRxQueue()
        {
            lock (RxQueueLock)
            {
                RxQueue.Clear();
            }
        }

        private static void ConnectWithTimeout(Socket socket, IPEndPoint endpoint, int timeoutMs)
        {
            IAsyncResult ar = socket.BeginConnect(endpoint, null, null);
            WaitHandle wh = ar.AsyncWaitHandle;
            try
            {
                // timeout?
                if (!wh.WaitOne(timeoutMs, false))
                {
                    // abort the pending connect and complete the async operation to free its resources
                    socket.Close();
                    try { socket.EndConnect(ar); } catch { }

                    throw new TimeoutException("Connect to " + endpoint + " timed out after " + timeoutMs + " ms");
                }

                // this throws exception if connect fails
                socket.EndConnect(ar);
            }
            finally
            {
                wh.Close();
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