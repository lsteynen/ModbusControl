using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Text;
using System.Threading;

using Modbus_net;
using LST.Helpers;

namespace ModbusServer
{
    public class ClientConnection : IDisposable
    {
        private class DataConnectionOperation
        {
            public Func<NetworkStream, string, string> Operation { get; set; }
            public string Arguments { get; set; }
        }

        public ModbusServer parent;
        public ModbusControlSpace.ModbusInterface modbusinterface;
        private Logger mLogger;
        private string mLogHeader;
        //private bool bLogData = true;

        private bool _disposed = false;

        private TcpClient mControlClient;
        private string mClientName;

        private NetworkStream mNetStream;
        private byte[] mHeaderBuf;

        private IPEndPoint mRemoteEndPoint;

        #region constructor

        public ClientConnection(TcpClient client)
        {
            mControlClient = client;
        }

        #endregion

        #region properties

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

        public string LogHeader
        {
            get
            {
                return mLogHeader;
            }
            set
            {
                mLogHeader = value;
            }
        }

        #endregion

        // implementation

        public void HandleClient(object obj)
        {
            // some exceptions may happen while the server is closing 
            try
            {
                mRemoteEndPoint = (IPEndPoint)mControlClient.Client.RemoteEndPoint;

                mClientName = mRemoteEndPoint.ToString();

                mNetStream = mControlClient.GetStream();

                mHeaderBuf = new byte[6]; // buffer to receive 6-byte MBAP header
                byte[] FrameBuf = new byte[300];
                byte[] ResponseBuffer;
                byte[] WriteBuffer = new byte[300];

                bool bContinue = true;

                while (bContinue && (_disposed==false))
                {
                    int HeaderLen = mNetStream.Read(mHeaderBuf, 0, 6);
                    if (HeaderLen == 6)
                    {
                        UInt16 MbapProtocol = (UInt16)(mHeaderBuf[2] * 256 + mHeaderBuf[3]);
                        if (MbapProtocol == 0)
                        {
                            UInt16 MbapFrameLen = (UInt16)(mHeaderBuf[4] * 256 + mHeaderBuf[5]);
                            if ((MbapFrameLen >= 2) && (MbapFrameLen <= 256))
                            {
                                int FrameLen = mNetStream.Read(FrameBuf, 0, MbapFrameLen);
                                if (FrameLen == MbapFrameLen)
                                {
                                    if (mLogger.LogBridgeData)
                                    {
                                        mLogger.Log(Logger.LogType.Data, mLogHeader + "rx from " + mClientName + ": " + BytesToString.Convert(mHeaderBuf, HeaderLen) + " - " + BytesToString.Convert(FrameBuf, FrameLen));
                                    }
                                    if (mLogger.LogBridgeDecode)
                                    {
                                        mLogger.Log(Logger.LogType.Decode, mLogHeader + "query decode: " + ModbusDecoder.DecodeQuery(FrameBuf, FrameLen));
                                    }
                                    ++parent.QueriesReceivedCount;

                                    // do we have a connection to the slave?
                                    if (modbusinterface.IsOpen)
                                    {
                                        // Response will be != null if we received a valid response (easier for the caller)
                                        ModbusProtocol.Result result = modbusinterface.MifSendQuery(FrameBuf, (ushort)FrameLen, out ResponseBuffer);
                                        // did we get a valid or exception response?
                                        if ((result == ModbusProtocol.Result.SUCCESS) || (ResponseBuffer != null))
                                        {
                                            // we got a valid or exception response, forward it

                                            // build response header + frame in a single buffer
                                            int ResponseLen = ResponseBuffer.Length;
                                            // header
                                            WriteBuffer[0] = mHeaderBuf[0];
                                            WriteBuffer[1] = mHeaderBuf[1];
                                            WriteBuffer[2] = mHeaderBuf[2];
                                            WriteBuffer[3] = mHeaderBuf[3];
                                            WriteBuffer[4] = (byte)(ResponseLen >> 8);
                                            WriteBuffer[5] = (byte)ResponseLen;
                                            // frame
                                            Array.Copy(ResponseBuffer, 0, WriteBuffer, 6, ResponseLen);
                                            // send in a single write (best practice, and for Wireshark to decode it correctly)
                                            mNetStream.Write(WriteBuffer, 0, 6 + ResponseLen);

                                            if (mLogger.LogBridgeData)
                                            {
                                                mLogger.Log(Logger.LogType.Data, mLogHeader + "tx to " + mClientName + ": " + BytesToString.Convert(WriteBuffer, 6) + " - " + BytesToString.Convert(ResponseBuffer, ResponseLen));
                                            }
                                            if (mLogger.LogBridgeDecode)
                                            {
                                                mLogger.Log(Logger.LogType.Decode, mLogHeader + "response decode: " + ModbusDecoder.DecodeResponse(ResponseBuffer, ResponseLen));
                                            }
                                            ++parent.ResponsesSentCount;
                                        }
                                        else
                                        {
                                            // we got no (valid) response, return a GATEWAY TARGET DEVICE FAILED TO RESPOND exception

                                            // build exception response header + frame in a single buffer
                                            ResponseBuffer = new byte[3];
                                            ResponseBuffer[0] = FrameBuf[0];  // echo slaveID
                                            ResponseBuffer[1] = (byte)(0x80 | FrameBuf[1]);  // 0x80 | echo function
                                            ResponseBuffer[2] = 11;           // exception code
                                            int ResponseLen = 3;
                                            // header
                                            WriteBuffer[0] = mHeaderBuf[0];
                                            WriteBuffer[1] = mHeaderBuf[1];
                                            WriteBuffer[2] = mHeaderBuf[2];
                                            WriteBuffer[3] = mHeaderBuf[3];
                                            WriteBuffer[4] = (byte)(ResponseLen >> 8);
                                            WriteBuffer[5] = (byte)ResponseLen;
                                            // frame
                                            WriteBuffer[6] = ResponseBuffer[0];
                                            WriteBuffer[7] = ResponseBuffer[1];
                                            WriteBuffer[8] = ResponseBuffer[2];
                                            // send in a single write (best practice, and for Wireshark to decode it correctly)
                                            mNetStream.Write(WriteBuffer, 0, 6 + ResponseLen);

                                            if (mLogger.LogErrors)
                                            {
                                                mLogger.Log(Logger.LogType.Error, mLogHeader + "tx to " + mClientName + ": exception 11 (GATEWAY TARGET DEVICE FAILED TO RESPOND)");
                                            }
                                            if (mLogger.LogBridgeData)
                                            {
                                                mLogger.Log(Logger.LogType.Data, mLogHeader + "tx to " + mClientName + ": " + BytesToString.Convert(WriteBuffer, 6) + " - " + BytesToString.Convert(ResponseBuffer, ResponseLen));
                                            }
                                            if (mLogger.LogBridgeDecode)
                                            {
                                                mLogger.Log(Logger.LogType.Decode, mLogHeader + "response decode: " + ModbusDecoder.DecodeResponse(ResponseBuffer, ResponseLen));
                                            }
                                            ++parent.ResponsesException11SentCount;
                                        }
                                    }
                                    else
                                    {
                                        // we have no connection to a slave, return a GATEWAY PATH UNAVAILABLE exception

                                        // build exception response header + frame in a single buffer
                                        ResponseBuffer = new byte[3];
                                        ResponseBuffer[0] = FrameBuf[0];  // echo slaveID
                                        ResponseBuffer[1] = (byte)(0x80 | FrameBuf[1]);  // 0x80 | echo function
                                        ResponseBuffer[2] = 10;           // exception code
                                        int ResponseLen = 3;
                                        // header
                                        WriteBuffer[0] = mHeaderBuf[0];
                                        WriteBuffer[1] = mHeaderBuf[1];
                                        WriteBuffer[2] = mHeaderBuf[2];
                                        WriteBuffer[3] = mHeaderBuf[3];
                                        WriteBuffer[4] = (byte)(ResponseLen >> 8);
                                        WriteBuffer[5] = (byte)ResponseLen;
                                        // frame
                                        WriteBuffer[6] = ResponseBuffer[0];
                                        WriteBuffer[7] = ResponseBuffer[1];
                                        WriteBuffer[8] = ResponseBuffer[2];
                                        // send in a single write (best practice, and for Wireshark to decode it correctly)
                                        mNetStream.Write(WriteBuffer, 0, 6 + ResponseLen);

                                        if (mLogger.LogErrors)
                                        {
                                            mLogger.Log(Logger.LogType.Error, mLogHeader + "tx to " + mClientName + ": exception 10 (GATEWAY PATH UNAVAILABLE)");
                                        }
                                        if (mLogger.LogBridgeData)
                                        {
                                            mLogger.Log(Logger.LogType.Data, mLogHeader + "tx to " + mClientName + ": " + BytesToString.Convert(WriteBuffer, 6) + " - " + BytesToString.Convert(ResponseBuffer, ResponseLen));
                                        }
                                        if (mLogger.LogBridgeDecode)
                                        {
                                            mLogger.Log(Logger.LogType.Decode, mLogHeader + "response decode: " + ModbusDecoder.DecodeResponse(ResponseBuffer, ResponseLen));
                                        }
                                        ++parent.ResponsesException10SentCount;
                                    }
                                }
                                else
                                {
                                    // receive PDU failed
                                    if (mLogger.LogErrors)
                                    {
                                        mLogger.Log(Logger.LogType.Error, mLogHeader + "rx from " + mClientName + ": receive of PDU part failed! (received " + FrameLen + " bytes of " + MbapFrameLen + ")");
                                    }
                                    ++parent.ReceivesFailedCount;
                                    // close connection on error
                                    bContinue = false;
                                }
                            }
                            else
                            {
                                // MBAP header: invalid PDU length
                                if (mLogger.LogErrors)
                                {
                                    mLogger.Log(Logger.LogType.Error, mLogHeader + "rx from " + mClientName + ": received invalid MBAP header: Invalid PDU length! Data: " + BytesToString.Convert(mHeaderBuf, HeaderLen));
                                }
                                ++parent.InvalidMbapHeaderCount;
                                // close connection on error
                                bContinue = false;
                            }
                        }
                        else
                        {
                            // MBAP header: invalid protocol identifier
                            if (mLogger.LogErrors)
                            {
                                mLogger.Log(Logger.LogType.Error, mLogHeader + "rx from " + mClientName + ": received invalid MBAP header: Invalid Protocol Identifier! Data: " + BytesToString.Convert(mHeaderBuf, HeaderLen));
                            }
                            ++parent.InvalidMbapHeaderCount;
                            // close connection on error
                            bContinue = false;
                        }
                    }
                    else
                    {
                        // receive MBAP header failed
                        // if received length == 0: the connection was closed
                        if (HeaderLen == 0)
                        {
                            // no error
                        }
                        else
                        {
                            if (mLogger.LogErrors)
                            {
                                mLogger.Log(Logger.LogType.Error, mLogHeader + "rx from " + mClientName + ": received invalid MBAP header: Len<6! Data: " + BytesToString.Convert(mHeaderBuf, HeaderLen));
                            }
                            ++parent.ReceivesFailedCount;
                        }
                        // close connection on error
                        bContinue = false;
                    }

                    Thread.Sleep(5);
                }
            }
            catch (Exception)
            {
                // don't log exceptions, exceptions may happen while the server is closing 
                ++parent.ExceptionsCount;
            }

            // tell parent that connection has closed
            parent.ClientHasClosed(this);

            if (mLogger.LogBridgeInfo)
            {
                mLogger.Log(Logger.LogType.Info, mLogHeader + mClientName + " closed connection");
            }

            ++parent.ConnectionsClosedCount;

            Dispose();
        }

        #region IDisposable

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            Debug.Print(DateTime.Now.TimeOfDay + ": Bridge: Connection.Dispose()");

            if (!_disposed)
            {
                if (disposing)
                {
                    if (mControlClient != null)
                    {
                        mControlClient.Close();
                    }

                    if (mNetStream != null)
                    {
                        mNetStream.Close();
                    }
                }
            }

            _disposed = true;
        }
        
        #endregion
    }
}