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
        private UInt32 mInterFrameDelay = 0;
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
        private int HoldingregisterStart = 40001;
        private int InputregisterStart = 30001;

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

        public UInt32 InterFrameDelay
        {
            get
            {
                return mInterFrameDelay;
            }
            set
            {
                mInterFrameDelay = value;
                if (mb != null) mb.InterFrameDelay = mInterFrameDelay;
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

        /// <summary>
        /// Open connection over serial port, using RTU protocol
        /// </summary>
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
            mb.InterFrameDelay = mInterFrameDelay;
            mb.Tries = mTries;
            mb.RetryOnException11 = false;

            // can throw exception!
            mb.Open();
        }

        /// <summary>
        /// Open network TCP connection, using Modbus/TCP protocol
        /// </summary>
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
            mb.InterFrameDelay = mInterFrameDelay;
            mb.Tries = mTries;
            mb.RetryOnException11 = false;

            // can throw exception!
            mb.Open();
        }

        /// <summary>
        /// Open network TCP connection, using RTU protocol
        /// </summary>
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
            mb.InterFrameDelay = mInterFrameDelay;
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
        
        /// <summary>
        /// Read holding registers
        /// </summary>
        public ModbusProtocol.Result MifReadHoldingRegisters(byte address, ushort startreg, ushort regcount, out short[] values, bool allowAll = true)
        {
            // must assign
            values = null;

            // modbus connection object created?
            if (mb == null)
            {
                return ModbusProtocol.Result.CHANNELNOTOPEN;
            }

            // make it threadsafe here to keep logging synced with queries/responses
            lock (mQueryLock)
            {
                if (mClosing) return ModbusProtocol.Result.CHANNELNOTOPEN;

                if (mLogger.LogReads)
                {
                    mLogger.Log(Logger.LogType.Read, "Read holding reg: slave=" + address + ", addr=" + (HoldingregisterStart + startreg).ToString() + ", quant=" + regcount);
                }

                // send query, get response
                ModbusProtocol.Result result = mb.ReadHoldingRegisters(address, startreg, regcount, out values, allowAll);
                if (result == ModbusProtocol.Result.SUCCESS)
                {
                    if (mLogger.LogReads)
                    {
                        mLogger.Log(Logger.LogType.Read, "Response: " + ShortsToString(values, regcount));
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

        /// <summary>
        /// Write a single holding register
        /// </summary>
        public ModbusProtocol.Result MifWriteSingleHoldingRegister(byte address, ushort startreg, short value)
        {
            // modbus connection object created?
            if (mb == null)
            {
                return ModbusProtocol.Result.CHANNELNOTOPEN;
            }

            // make it threadsafe here to keep logging synced with queries/responses
            lock (mQueryLock)
            {
                if (mClosing) return ModbusProtocol.Result.CHANNELNOTOPEN;

                if (mLogger.LogWrites)
                {
                    mLogger.Log(Logger.LogType.Write, "Write single holding reg: slave=" + address + ", addr=" + (HoldingregisterStart + startreg).ToString() + ", value=" + value);
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

        /// <summary>
        /// Write multiple holding registers
        /// </summary>
        public ModbusProtocol.Result MifWriteMultipleHoldingRegisters(byte address, ushort startreg, ushort regcount, short[] values)
        {
            // modbus connection object created?
            if (mb == null)
            {
                return ModbusProtocol.Result.CHANNELNOTOPEN;
            }

            // make it threadsafe here to keep logging synced with queries/responses
            lock (mQueryLock)
            {
                if (mClosing) return ModbusProtocol.Result.CHANNELNOTOPEN;

                if (mLogger.LogWrites)
                {
                    mLogger.Log(Logger.LogType.Write, "Write multiple holding reg: slave=" + address + ", addr=" + (HoldingregisterStart + startreg).ToString() + ", quant=" + regcount + ", values=" + ShortsToString(values, regcount));
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

        /// <summary>
        /// Read input registers
        /// </summary>
        public ModbusProtocol.Result MifReadInputRegisters(byte address, ushort startreg, ushort regcount, out short[] values, bool allowAll = true)
        {
            // must assign
            values = null;

            // modbus connection object created?
            if (mb == null)
            {
                return ModbusProtocol.Result.CHANNELNOTOPEN;
            }

            // make it threadsafe here to keep logging synced with queries/responses
            lock (mQueryLock)
            {
                if (mClosing) return ModbusProtocol.Result.CHANNELNOTOPEN;

                if (mLogger.LogReads)
                {
                    mLogger.Log(Logger.LogType.Read, "Read input reg: slave=" + address + ", addr=" + (InputregisterStart + startreg).ToString() + ", quant=" + regcount);
                }

                // send query, get response
                ModbusProtocol.Result result = mb.ReadInputRegisters(address, startreg, regcount, out values, allowAll);
                if (result == ModbusProtocol.Result.SUCCESS)
                {
                    if (mLogger.LogReads)
                    {
                        mLogger.Log(Logger.LogType.Read, "Response: " + ShortsToString(values, regcount));
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

        /// <summary>
        /// Read coils
        /// Return data as array of bytes, values 0/1
        /// </summary>
        public ModbusProtocol.Result MifReadCoils(byte address, ushort startreg, ushort regcount, out byte[] values, bool allowAll = true)
        {
            // must assign
            values = null;

            // modbus connection object created?
            if (mb == null)
            {
                return ModbusProtocol.Result.CHANNELNOTOPEN;
            }

            // make it threadsafe here to keep logging synced with queries/responses
            lock (mQueryLock)
            {
                if (mClosing) return ModbusProtocol.Result.CHANNELNOTOPEN;

                if (mLogger.LogReads)
                {
                    mLogger.Log(Logger.LogType.Read, "Read coils: slave=" + address + ", addr=" + (InputregisterStart + startreg).ToString() + ", quant=" + regcount);
                }

                // send query, get response
                ModbusProtocol.Result result = mb.ReadCoils(address, startreg, regcount, out values, allowAll);
                if (result == ModbusProtocol.Result.SUCCESS)
                {
                    if (mLogger.LogReads)
                    {
                        mLogger.Log(Logger.LogType.Read, "Response: " + BytesToString.Convert(values, regcount));
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
        /// <summary>
        /// Read discrete inputs
        /// Return data as array of bytes, values 0/1
        /// </summary>
        public ModbusProtocol.Result MifReadDiscreteInputs(byte address, ushort startreg, ushort regcount, out byte[] values, bool allowAll = true)
        {
            // must assign
            values = null;

            // modbus connection object created?
            if (mb == null)
            {
                return ModbusProtocol.Result.CHANNELNOTOPEN;
            }

            // make it threadsafe here to keep logging synced with queries/responses
            lock (mQueryLock)
            {
                if (mClosing) return ModbusProtocol.Result.CHANNELNOTOPEN;

                if (mLogger.LogReads)
                {
                    mLogger.Log(Logger.LogType.Read, "Read discrete inputs: slave=" + address + ", addr=" + (InputregisterStart + startreg).ToString() + ", quant=" + regcount);
                }

                // send query, get response
                ModbusProtocol.Result result = mb.ReadDiscreteInputs(address, startreg, regcount, out values, allowAll);
                if (result == ModbusProtocol.Result.SUCCESS)
                {
                    if (mLogger.LogReads)
                    {
                        mLogger.Log(Logger.LogType.Read, "Response: " + BytesToString.Convert(values, regcount));
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

        public string ShortsToString(short[] aValues, int Count)
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
