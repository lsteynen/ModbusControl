using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Diagnostics;

using LST.Helpers;

namespace Modbus_net
{
    public class TcpChannel : ICommChannel
    {
        private IPAddress mServerIp;
        private UInt16 mServerPort;
        private IPEndPoint mEndpoint;
        private TcpClient mConnection;
        private NetworkStream mStream;
        private Thread ReceiveThread;
        private bool ReceiveThread_Stop;
        private ByteQueue RxQueue = new ByteQueue(2000);
        public bool mIsOpen;

        // constructor
        public TcpChannel( IPAddress ServerIp, UInt16 Port)
        {
            mServerIp = ServerIp;
            mServerPort = Port;

            // make endpoint
            mEndpoint = new IPEndPoint(mServerIp, mServerPort);

            // init properties
            ConnectTimeout = 3000;

            // already make a connection object so mConnection members will work
            mConnection = new TcpClient();
            mConnection.SendTimeout = 1000;
        }

        // destructor
        ~TcpChannel()
        {
            if (mIsOpen)
            {
                Close();
            }
        }

        // properties

        public string ChannelName
        {
            get { return mEndpoint.ToString(); }
        }

        public bool IsOpen
        {
            get { return mIsOpen; }
        }

        public bool IsConnected
        {
            get
            {
                return (mConnection.Client != null) && mConnection.Connected;
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

        public uint ConnectTimeout
        {
            get;
            set;
        }

        public uint ResponseTimeout
        {
            get;
            set;
        }

        public uint CharacterTimeout
        {
            // is not used here
            get
            {
                return 0;
            }
            set
            {
                // ignore
            }
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

        public event TxRxCallbackHandler OnDataSent;
        public event TxRxCallbackHandler OnDataReceived;
        public event LogCallbackString OnWarning;

        // methods

        public void Open()
        {
            // can throw exception!

            // always make a new connection object because Close() has disposed it
            mConnection = new TcpClient();
            mConnection.SendTimeout = 1000;

            // don't linger
            // set LingerState = true (enabled), A zero timeout. Discards any pending data. For connection-oriented socket (TCP, for example), Winsock resets the connection. 
            mConnection.LingerState = new LingerOption(true, 0);

            // we do a try/catch here to be able to update our diagnostics counters
            try
            {
                // do a connect with a custom timeout (not supported in .NET < 4.5)
                TcpConnectWithTimeout(mConnection, mEndpoint, ConnectTimeout);
                mStream = mConnection.GetStream();

                // we are connected
                ++ConnectCount;

                // clear receive queue
                RxQueue.Clear();

                // Start receive thread
                ReceiveThread = new Thread(ReceiveThread_Work);
                ReceiveThread_Stop = false;
                ReceiveThread.Start();

                mIsOpen = true;
            }
            catch (Exception)
            {
                ++ConnectFailCount;
                // rethrow the exception for the caller
                throw;
            }
        }

        public void Close()
        {
            mIsOpen = false;

            mStream = null;

            // stop backgroundworker if it runs
            if (ReceiveThread != null)
            {
                ReceiveThread_Stop = true;

                // wait for thread to end
                ReceiveThread.Join();
                ReceiveThread = null;
            }

            // on the forums, some say it is best to close the underlying connection explicitly
            mConnection.Client.Disconnect(false);
            mConnection.Close();
        }

        public bool BeforeTransaction()
        {
            // channel is open? 
            if (mIsOpen)
            {
                // still be connected?
                if (IsConnected)
                {
                    // okay, nothing to do
                }
                else
                {
                    // connection was lost
                    ++ConnectionLostCount;

                    // log that connection was lost
                    if (OnWarning != null)
                    {
                        string s = "Connection lost, reconnecting";
                        OnWarning(s);
                    }

                    // try to reconnect
                    // do not call Open() because the workerthread is still running
                    // catch exceptions here, return false on fail
                    // always make a new connection object because Close() has disposed it
                    mConnection = new TcpClient();
                    mConnection.SendTimeout = 1000;

                    // don't linger
                    // set LingerState = true (enabled), A zero timeout. Discards any pending data. For connection-oriented socket (TCP, for example), Winsock resets the connection. 
                    mConnection.LingerState = new LingerOption(true, 0);

                    // don't throw exceptions from here
                    try
                    {
                        // do a connect with a custom timeout (not supported in .NET < 4.5)
                        TcpConnectWithTimeout(mConnection, mEndpoint, ConnectTimeout);
                        mStream = mConnection.GetStream();

                        // we are connected
                        ++ConnectCount;

                        // clear receive queue
                        RxQueue.Clear();
                    }
                    catch (Exception)
                    {
                        ++ConnectFailCount;
                    }

                    // still not connected?
                    if (IsConnected == false)
                    {
                        // log that connect failed
                        if (OnWarning != null)
                        {
                            string s = "Reconnect failed";
                            OnWarning(s);
                        }
                    }

                    RxQueue.Clear();
                }

                // We don't flush TCP channel buffers here
            }
            else
            {
                // open
                // catch exceptions here
                try
                {
                    Open();
                }
                catch (Exception)
                {
                }
            }

            // return true if the channel is open and connected
            return mIsOpen && IsConnected;
        }

        public void AfterTransaction()
        {
            // nothing to do
        }

        public void Write(byte[] data, int start, int length)
        {
            mStream.Write(data, start, length);

            // log sent data
            if (OnDataSent != null)
            {
                OnDataSent(data, length);
            }
        }

        public int Read(ref byte[] buffer, int start, int maxcount)
        {
            return RxQueue.Get(ref buffer, start, maxcount);
        }

        public void SetCharacterTimeout(uint timeout)
        {
            // character/end-of-message timeout is not used in TCP
            // always use ResponseTimeout 
        }

        public int ReadWait(ref byte[] buffer, int count)
        {
            Debug.Print(DateTime.Now.TimeOfDay + ": TcpChannel.ReadWait() enter");

            long startTime = DateTime.UtcNow.Ticks;

            // wait till we received all bytes, or timeout
            while (RxQueue.Count < count)
            {
                // timeout?
                if (((DateTime.UtcNow.Ticks - startTime) / TimeSpan.TicksPerMillisecond) >= ResponseTimeout)
                {
                    // timeout, return the bytes we received till now
                    count = RxQueue.Count;

                    Debug.Print(DateTime.Now.TimeOfDay + ": TcpChannel.ReadWait(): loop timeout, got " + count + " bytes");
                }
                else
                {
                    // wait for more bytes
                    Thread.Sleep(10);
                }
            }

            Debug.Print(DateTime.Now.TimeOfDay + ": TcpChannel.ReadWait(): exit, got " + count + " bytes");

            // copy the bytes we received
            RxQueue.Get(ref buffer, 0, count);

            return count;
        }

        private void TcpConnectWithTimeout(TcpClient tcp, IPEndPoint endpoint, uint timeout)
        {
            // do a connect with a custom timeout (not supported in .net < 4.5)

            IAsyncResult ar = tcp.BeginConnect(endpoint.Address, endpoint.Port, null, null);
            System.Threading.WaitHandle wh = ar.AsyncWaitHandle;
            try
            {
                // timeout?
                if (!ar.AsyncWaitHandle.WaitOne((int)timeout, false))
                {
                    // timeout
                    tcp.Close();
                    throw new TimeoutException();
                }

                // this throws exception if connect fails
                tcp.EndConnect(ar);
            }
            finally
            {
                wh.Close();
            }
        }

        private void ReceiveThread_Work()
        {
            byte[] buffer = new byte[256];

            // wait for incoming data
            do
            {
                try
                {
                    if ((mStream != null) && mStream.DataAvailable)
                    {
                        int count = mStream.Read(buffer, 0, 256);
                        if (count > 0)
                        {
                            // add to queue
                            RxQueue.Add(buffer, count);

                            // log received data
                            if (OnDataReceived != null)
                            {
                                OnDataReceived(buffer, count);
                            }
                        }
                    }

                    Thread.Sleep(10);
                }
                catch (Exception)
                {
                }
            }
            while (ReceiveThread_Stop == false);
        }

    }
}
