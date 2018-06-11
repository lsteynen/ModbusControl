using System;
using System.Diagnostics;
using System.IO.Ports;
using System.Text;
using System.Threading;
using System.Net;
using System.Net.Sockets;

using Modbus_net;
using LST.Helpers;

namespace ModbusControlSpace
{
    public class ModbusInterface
    {
        private UInt32 mResponseTimeout = 1000;
        private UInt32 mEndOfMessageTimeout = 100;
        private bool mOneWireEchoSuppression = false;
        private UInt32 mTries = 1;

        private ModbusProtocol mb;
        private ModbusRtuProtocol mbRtuProtocol;
        private ModbusTcpProtocol mbTcpProtocol;
        private SerialChannel mbSerChannel;
        private TcpChannel mbTcpChannel;
        private Object mQueryLock = new Object();
        private Logger mLogger;
        private bool mClosing;

        #region constructor

        public ModbusInterface()
        {
        }

        #endregion

        #region properties

        public bool IsOpen
        {
            get
            {
                return (mb != null) && mb.IsOpen;
            }
        }

        public UInt32 ResponseTimeout
        {
            get
            {
                return mResponseTimeout;
            }
            set
            {
                mResponseTimeout = value;
                if (mb != null) mb.ResponseTimeout = mResponseTimeout;
            }
        }

        public UInt32 EndOfMessageTimeout
        {
            get
            {
                return mEndOfMessageTimeout;
            }
            set
            {
                mEndOfMessageTimeout = value;
                if (mb != null) mb.EndOfMessageTimeout = mEndOfMessageTimeout;
            }
        }

        public bool OneWireEchoSuppression
        {
            get
            {
                return mOneWireEchoSuppression;
            }
            set
            {
                mOneWireEchoSuppression = value;
                if (mb != null) mb.OneWireEchoSuppression = mOneWireEchoSuppression;
            }
        }
        
        public uint SuccessCount
        {
            get
            {
                return (mb != null) ? mb.SuccessCount : 0;
            }
        }

        public uint NoResponseCount
        {
            get
            {
                return (mb != null) ? mb.NoResponseCount : 0;
            }
        }

        public uint ResponseCrcErrorCount
        {
            get
            {
                return (mb != null) ? mb.ResponseCrcErrorCount : 0;
            }
        }

        public uint ResponseFormatErrorCount
        {
            get
            {
                return (mb != null) ? mb.ResponseFormatErrorCount : 0;
            }
        }

        public uint ResponseMBAPheaderErrorCount
        {
            get
            {
                return (mb != null) ? mb.ResponseMBAPheaderErrorCount : 0;
            }
        }

        public uint TcpConnectCount
        {
            get
            {
                return (mb != null) ? mb.TcpConnectCount : 0;
            }
        }

        public uint TcpConnectFailCount
        {
            get
            {
                return (mb != null) ? mb.TcpConnectFailCount : 0;
            }
        }

        public uint TcpConnectionLostCount
        {
            get
            {
                return (mb != null) ? mb.TcpConnectionLostCount : 0;
            }
        }

        public uint TcpConnectionClosedBecauseOfResponseError
        {
            get
            {
                return (mb != null) ? mb.TcpConnectionClosedBecauseOfResponseError : 0;
            }
        }

        public uint OneWireEchoErrorCount
        {
            get
            {
                return (mb != null) ? mb.OneWireEchoErrorCount : 0;
            }
        }

        public uint IllegalFunctionExceptionCount
        {
            get
            {
                return (mb != null) ? mb.IllegalFunctionExceptionCount : 0;
            }
        }

        public uint IllegalAddressExceptionCount
        {
            get
            {
                return (mb != null) ? mb.IllegalAddressExceptionCount : 0;
            }
        }

        public uint IllegalDataExceptionCount
        {
            get
            {
                return (mb != null) ? mb.IllegalDataExceptionCount : 0;
            }
        }

        public uint GatewayPathUnavailableExceptionCount
        {
            get
            {
                return (mb != null) ? mb.GatewayPathUnavailableExceptionCount : 0;
            }
        }

        public uint GatewayTargetdeviceFailedToRespondExceptionCount
        {
            get
            {
                return (mb != null) ? mb.GatewayTargetdeviceFailedToRespondExceptionCount : 0;
            }
        }

        public uint OtherExceptionCount
        {
            get
            {
                return (mb != null) ? mb.OtherExceptionCount : 0;
            }
        }

        public uint TotalExceptionCount
        {
            get
            {
                return (mb != null) ? mb.TotalExceptionCount : 0;
            }
        }

        public Logger Logger
        {
            get
            {
                return mLogger;
            }
            set
            {
                mLogger = value;
            }
        }

        public bool IsTcpChannel
        {
            get
            {
                return (mb != null) && mb.IsTcpChannel;
            }
        }

        #endregion

        public void OpenSerialRtu( string portName, uint baudRate, Parity parity, StopBits stopBits)
        {
            mClosing = false;
            mbSerChannel = new SerialChannel(portName, baudRate, 8U, parity, stopBits);
            mbSerChannel.OnDataSent += LogChannelSent;
            mbSerChannel.OnDataReceived += LogChannelReceived;
            mbSerChannel.OnWarning += LogWarning;
            mbRtuProtocol = new ModbusRtuProtocol();
            mbRtuProtocol.OnInfo += LogInfo;
            mbRtuProtocol.OnWarning += LogWarning;
            mbRtuProtocol.OnError += LogError;
            mb = new ModbusProtocol(mbRtuProtocol, mbSerChannel);
            mb.OneWireEchoSuppression = mOneWireEchoSuppression;
            mb.ResponseTimeout = mResponseTimeout;
            mb.EndOfMessageTimeout = mEndOfMessageTimeout;
            mb.Tries = mTries;
            mb.RetryOnException11 = false;

            // can throw exception!
            mb.Open();
        }

        public void OpenModbusTcp(IPAddress IpAddress, UInt16 TcpPort)
        {
            mClosing = false;
            mbTcpChannel = new TcpChannel(IpAddress, TcpPort);
            mbTcpChannel.OnDataSent += LogChannelSent;
            mbTcpChannel.OnDataReceived += LogChannelReceived;
            mbTcpChannel.OnWarning += LogWarning;
            mbTcpProtocol = new ModbusTcpProtocol();
            mbTcpProtocol.OnInfo += LogInfo;
            mbTcpProtocol.OnWarning += LogWarning;
            mbTcpProtocol.OnError += LogError;
            mb = new ModbusProtocol(mbTcpProtocol, mbTcpChannel);
            uint ConnectTimeout = Math.Max(3000, mResponseTimeout); // 3000ms or higher if ResponseTimeout is hoger
            mb.ConnectTimeout = ConnectTimeout;
            mb.ResponseTimeout = mResponseTimeout;
            mb.Tries = mTries;
            mb.RetryOnException11 = false;

            // can throw exception!
            mb.Open();
        }

        public void OpenRtuOverTcp(IPAddress IpAddress, UInt16 TcpPort)
        {
            mClosing = false;
            mbTcpChannel = new TcpChannel(IpAddress, TcpPort);
            mbTcpChannel.OnDataSent += LogChannelSent;
            mbTcpChannel.OnDataReceived += LogChannelReceived;
            mbTcpChannel.OnWarning += LogWarning;
            mbRtuProtocol = new ModbusRtuProtocol();
            mbRtuProtocol.OnInfo += LogInfo;
            mbRtuProtocol.OnWarning += LogWarning;
            mbRtuProtocol.OnError += LogError;
            mb = new ModbusProtocol(mbRtuProtocol, mbTcpChannel);
            uint ConnectTimeout = Math.Max(3000, mResponseTimeout); // 3000ms or higher if ResponseTimeout is hoger
            mb.ConnectTimeout = ConnectTimeout;
            mb.ResponseTimeout = mResponseTimeout;
            mb.Tries = mTries;
            mb.RetryOnException11 = false;

            // can throw exception!
            mb.Open();
        }

        public void PrepareForClose()
        {
            Debug.Print(DateTime.Now.TimeOfDay + ": Modbusinterface: PrepareForClose()");

            // Commands the connection will be closed. Threads waiting for mQueryLock will quickly be released.
            // Must be followed by Close() lateron
            mClosing = true;
        }

        public void Close()
        {
            Debug.Print(DateTime.Now.TimeOfDay + ": Modbusinterface: Close()");

            if ((mb != null) && mb.IsOpen)
            {
                // can throw exception!
                mb.Close();
                // prevent that the autoreconnect for TCP channels reopens the connection, we are officially closed
                mb = null;
            }
        }
        
        public ModbusProtocol.Result MifReadHoldingRegisters(byte address, ushort startreg, ushort regcount, out short[] values)
        {
            // must assign
            values = null;

            // modbus connection object created?
            if (mb == null)
            {
                return ModbusProtocol.Result.CHANNELNOTOPEN;
            }

            // read holding registers
            // make it threadsafe here to keep logging synced with queries/responses
            lock (mQueryLock)
            {
                if (mClosing) return ModbusProtocol.Result.CHANNELNOTOPEN;

                if (mLogger.LogReads)
                {
                    mLogger.Log(Logger.LogType.Read, "Read holding reg: slave=" + address + ", addr=" + (40001 + startreg) + ", quant=" + regcount);
                }

                // send query, get response
                ModbusProtocol.Result result = mb.ReadHoldingRegisters(address, startreg, regcount, out values);
                if (result == ModbusProtocol.Result.SUCCESS)
                {
                    if (mLogger.LogReads)
                    {
                        mLogger.Log(Logger.LogType.Read, "Response: " + ValuesToString(values, regcount));
                    }
                }
                else
                {
                    if (mLogger.LogErrors)
                    {
                        if (ModbusProtocol.IsExceptionResponse(result))
                        {
                            mLogger.Log(Logger.LogType.Error, "Read error: Response is exception: " + ModbusProtocol.GetResultText(result));
                        }
                        else
                        {
                            mLogger.Log(Logger.LogType.Error, "Read error: " + ModbusProtocol.GetResultText(result));
                        }
                    }
                }

                return result;
            }
        }

        public ModbusProtocol.Result MifWriteSingleHoldingRegister(byte address, ushort startreg, short value)
        {
            // modbus connection object created?
            if (mb == null)
            {
                return ModbusProtocol.Result.CHANNELNOTOPEN;
            }

            // write a singe holding register
            // make it threadsafe here to keep logging synced with queries/responses
            lock (mQueryLock)
            {
                if (mClosing) return ModbusProtocol.Result.CHANNELNOTOPEN;

                if (mLogger.LogWrites)
                {
                    mLogger.Log(Logger.LogType.Write, "Write single holding reg: slave=" + address + ", addr=" + (40001 + startreg) + ", value=" + value);
                }

                // send query, get response
                ModbusProtocol.Result result = mb.WriteSingleHoldingRegister(address, startreg, value);
                if (result == ModbusProtocol.Result.SUCCESS)
                {
                    if (mLogger.LogWrites)
                    {
                        mLogger.Log(Logger.LogType.Write, "Write response: ack");
                    }
                }
                else
                {
                    if (mLogger.LogErrors)
                    {
                        if (ModbusProtocol.IsExceptionResponse(result))
                        {
                            mLogger.Log(Logger.LogType.Error, "Write error: Response is exception: " + ModbusProtocol.GetResultText(result));
                        }
                        else
                        {
                            mLogger.Log(Logger.LogType.Error, "Write error: " + ModbusProtocol.GetResultText(result));
                        }
                    }
                }

                return result;
            }
        }

        public ModbusProtocol.Result MifWriteMultipleHoldingRegisters(byte address, ushort startreg, ushort regcount, short[] values)
        {
            // modbus connection object created?
            if (mb == null)
            {
                return ModbusProtocol.Result.CHANNELNOTOPEN;
            }

            // write multiple holding registers
            // make it threadsafe here to keep logging synced with queries/responses
            lock (mQueryLock)
            {
                if (mClosing) return ModbusProtocol.Result.CHANNELNOTOPEN;

                if (mLogger.LogWrites)
                {
                    mLogger.Log(Logger.LogType.Write, "Write multiple holding reg: slave=" + address + ", addr=" + (40001 + startreg) + ", quant=" + regcount + ", values=" + ValuesToString(values, regcount));
                }

                // send query, get response
                ModbusProtocol.Result result = mb.WriteMultipleHoldingRegisters(address, startreg, regcount, values);
                if (result == ModbusProtocol.Result.SUCCESS)
                {
                    if (mLogger.LogWrites)
                    {
                        mLogger.Log(Logger.LogType.Write, "Write response: ack");
                    }
                }
                else
                {
                    if (mLogger.LogErrors)
                    {
                        if (ModbusProtocol.IsExceptionResponse(result))
                        {
                            mLogger.Log(Logger.LogType.Error, "Write error: Response is exception: " + ModbusProtocol.GetResultText(result));
                        }
                        else
                        {
                            mLogger.Log(Logger.LogType.Error, "Write error: " + ModbusProtocol.GetResultText(result));
                        }
                    }
                }

                return result;
            }
        }

        public ModbusProtocol.Result MifSendQuery(byte[] QueryBuffer, ushort QueryLength, out byte[] ResponseBuffer)
        {
            // must assign
            ResponseBuffer = null;

            // modbus connection object created?
            if (mb == null)
            {
                return ModbusProtocol.Result.CHANNELNOTOPEN;
            }

            // send a raw modbus query message
            // make it threadsafe here to keep logging synced with queries/responses
            lock (mQueryLock)
            {
                if (mClosing) return ModbusProtocol.Result.CHANNELNOTOPEN;

                // send query, get response
                ModbusProtocol.Result result = mb.SendQuery(QueryBuffer, QueryLength, out ResponseBuffer);

                // no logging for MifSendQuery()

                return result;
            }
        }

        public void CloseTcpChannelConnection()
        {
            // If we use short-lived TCP connections: close the TCP connection
            mb.CloseShortLivedTcpChannelConnection();
        }

        public void ClearCounters()
        {
            if (mb != null)
            {
                mb.ClearCounters();
            }
        }

        public string ValuesToString(short[] aValues, int Count)
        {
            // provide room for 126 values of "-32767,"
            StringBuilder sb = new StringBuilder(1000);

            for( int i=0; i<Count; ++i)
            {
                if (i >= 1) sb.Append(',');
                sb.Append(aValues[i]);
            }

            return sb.ToString();
        }

        private void LogChannelSent(byte[] data, int length)
        {
            if (mLogger.LogTrace)
            {
                mLogger.Log(Logger.LogType.Trace, "Tx: " + BytesToString.Convert(data, length));
            }
        }

        private void LogChannelReceived(byte[] data, int length)
        {
            if (mLogger.LogTrace)
            {
                mLogger.Log(Logger.LogType.Trace, "Rx: " + BytesToString.Convert(data, length));
            }
        }

        private void LogInfo(string s)
        {
            if (mLogger.LogInfo)
            {
                mLogger.Log(Logger.LogType.Info, s);
            }
        }

        private void LogWarning(string s)
        {
            if (mLogger.LogWarnings)
            {
                mLogger.Log(Logger.LogType.Warning, s);
            }
        }

        private void LogError(string s)
        {
            if (mLogger.LogErrors)
            {
                mLogger.Log(Logger.LogType.Error, s);
            }
        }


    }
}
