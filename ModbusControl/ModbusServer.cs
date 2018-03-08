using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Ports;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

using LoggerSpace;

namespace ModbusServer
{
    public class ModbusServer : IDisposable
    {
        private bool _disposed = false;
        private bool _listening = false;
        private bool _listeningPaused = false;

        private int MaxConnections = 100;
        private TcpListener _listener;
        private readonly object mListenerLock = new object();
        private List<ClientConnection> mActiveConnections;
        private readonly object mActiveConnectionsLock = new object();
        private IPEndPoint _localEndPoint;

        private string mLastExceptionText;
        private Logger _Logger;

        public ModbusControlSpace.ModbusInterface modbusinterface;
        private string MyName;
        private string LogHeader;

        public UInt32 ConnectionsCount;
        public UInt32 ConnectionsClosedCount;
        public UInt32 ReceivesFailedCount;
        public UInt32 InvalidMbapHeaderCount;
        public UInt32 QueriesReceivedCount;
        public UInt32 ResponsesSentCount;
        public UInt32 ResponsesException10SentCount;
        public UInt32 ResponsesException11SentCount;
        public UInt32 ExceptionsCount;

        #region constructor

        public ModbusServer()
        {
            mActiveConnections = new List<ClientConnection>();
            _localEndPoint = new IPEndPoint(IPAddress.Any, 502);
        }

        public ModbusServer(IPAddress ipAddress, int port)
        {
            mActiveConnections = new List<ClientConnection>();
            _localEndPoint = new IPEndPoint(ipAddress, port);
        }

        #endregion

        #region properties

        public IPAddress IpAddress
        {
            get
            {
                return _localEndPoint.Address;
            }
            set
            {
                _localEndPoint.Address = value;
            }
        }

        public int Port
        {
            get
            {
                return _localEndPoint.Port;
            }
            set
            {
                _localEndPoint.Port = value;
            }
        }

        public Logger Logger
        {
            get
            {
                return _Logger;
            }
            set
            {
                _Logger = value;
            }
        }

        public string LastExceptionText
        {
            get
            {
                return mLastExceptionText;
            }
        }

        public UInt32 PendingConnectionsCount
        {
            get { return (UInt32)mActiveConnections.Count;}
        }

        public UInt32 ErrorsCount
        {
            // return the sum of communications errors (not include exception responses)
            get 
            {
                return (ReceivesFailedCount
                       + InvalidMbapHeaderCount
                       + ResponsesException10SentCount
                       + ResponsesException11SentCount);
            }
        }

        public bool IsRunning
        {
            get { return _listening; }
            set { _listening = value; }
        }

        #endregion

        // implementation

        public void Start()
        {
            try
            {
                // start the listener
                lock( mListenerLock)
                {
                    _listener = new TcpListener(_localEndPoint);

                    _listener.Start();
                    _listeningPaused = false;
                    _listening = true;

                    _listener.BeginAcceptTcpClient(HandleAcceptTcpClient, _listener);

                    MyName = "Bridge";
                    LogHeader = MyName + ": ";
                }
            }
            catch (Exception ex)
            {
                mLastExceptionText = ex.Message;
            }
        }

        public void Stop()
        {
            Debug.Print(DateTime.Now.TimeOfDay + ": Bridge: Server.Stop()");

            // stop the listener
            lock (mListenerLock)
            {
                _listening = false;
                _listener.Stop();
                _listener = null;
            }

            // disconnect active connections
            lock (mActiveConnectionsLock)
            {
                foreach (ClientConnection conn in mActiveConnections)
                {
                    conn.Dispose();
                }
            }
        }

        private void HandleAcceptTcpClient(IAsyncResult result)
        {
            if (_listening)
            {
                try
                {
                    TcpClient client = _listener.EndAcceptTcpClient(result);

                    ClientConnection connection = new ClientConnection(client);
                    connection.parent = this;
                    connection.modbusinterface = modbusinterface;
                    connection.Logger = Logger;
                    connection.LogHeader = LogHeader;

                    lock (mActiveConnectionsLock)
                    {
                        mActiveConnections.Add(connection);
                    }

                    Debug.Print(DateTime.Now.TimeOfDay + ": Bridge: New connection (" + mActiveConnections.Count + " open connections now)");

                    // report new client
                    IPEndPoint remoteEndPoint = (IPEndPoint)client.Client.RemoteEndPoint;
                    if (_Logger.LogBridgeInfo)
                    {
                        _Logger.Log(Logger.LogType.Info, LogHeader + "new connection from " + remoteEndPoint.ToString());
                    }

                    ++ConnectionsCount;

                    ThreadPool.QueueUserWorkItem(connection.HandleClient, client);

                    // max number of connections?
                    if (mActiveConnections.Count >= MaxConnections)
                    {
                        lock (mListenerLock)
                        {
                            // stop the listener
                            _listeningPaused = true;
                            _listener.Stop();
                        }
                    }
                    else
                    {
                        // prep for next accept
                        _listener.BeginAcceptTcpClient(HandleAcceptTcpClient, _listener);
                    }
                }
                catch (Exception)
                {
                }
            }
        }

        public void ClientHasClosed(ClientConnection connection)
        {
            lock (mActiveConnectionsLock)
            {
                bool ret = mActiveConnections.Remove(connection);
            }

            Debug.Print(DateTime.Now.TimeOfDay + ": Bridge: Connection ended (" + mActiveConnections.Count + " open connections now)");

            // re-enable our listener?
            lock (mListenerLock)
            {
                if (_listeningPaused && (_listener != null) && (mActiveConnections.Count < MaxConnections))
                {
                    // okay
                    _listener.Start();
                    _listeningPaused = false;
                    _listener.BeginAcceptTcpClient(HandleAcceptTcpClient, _listener);
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    Stop();
                }
            }

            _disposed = true;
        }

        public void ClearStatistics()
        {
            ConnectionsCount = 0;
            ConnectionsClosedCount = 0;
            ReceivesFailedCount = 0;
            InvalidMbapHeaderCount = 0;
            QueriesReceivedCount = 0;
            ResponsesSentCount = 0;
            ResponsesException10SentCount = 0;
            ResponsesException11SentCount = 0;
            ExceptionsCount = 0;
        }
    }
}
