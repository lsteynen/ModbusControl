using System;
using System.Collections.Generic;
using System.Text;

namespace Modbus_net
{
    public class ModbusProtocol
    {
        private IModbusProtocol mProtocol;
        private ICommChannel mChannel;
        private bool mIsOpen;
        private uint mInterFrameDelay;

        // all these result codes must be supported in AdjustResultCounters() and GetResultText()
        public enum Result
        {
            // successful operation
            SUCCESS = 0,
            // modbus exception codes (but a valid response)
            ILLEGALFUNCTIONEXCEPTION = 1,
            ILLEGALADDRESSEXCEPTION = 2,
            ILLEGALDATAVALUEEXCEPTION = 3,
            SLAVEDEVICEFAILUREEXCEPTION = 4,
            ACKFORLONGDURATIONEXCEPTION = 5,
            SLAVEDEVICEBUSYEXCEPTION = 6,
            NEGATIVEACKEXCEPTION = 7,
            MEMORYPARITYERROREXCEPTION = 8,
            GATEWAYPATHUNAVAILABLEEXCEPTION = 10,
            GATEWAYTARGETDEVICEFAILEDTORESPONDEXCEPTION = 11,
            UNDEFINEDEXCEPTION = 12,
            // response fails
            NORESPONSE,
            RESPONSEFORMATERROR,
            RESPONSECRCERROR,
            RESPONSEMBAPHEADERERROR,
            ONEWIREECHOERROR,
            // internal errors
            CHANNELNOTOPEN,
            UNSUPPORTED,
            INVALIDARGUMENT,
        }

        #region constructors

        public ModbusProtocol(IModbusProtocol protocol, ICommChannel channel)
        {
            mProtocol = protocol;
            mProtocol.Channel = channel;
            mChannel = channel;

            // set defaults
            ConnectTimeout = 3000;
            ResponseTimeout = 1000;
            EndOfMessageTimeout = 100;
            mInterFrameDelay = 0;
            Tries = 3;
            RetryOnException11 = false;
        }

        #endregion

        #region properties

        public bool IsOpen
        {
            get
            {
                return mIsOpen;
            }
        }

        public bool OneWireEchoSuppression
        {
            get { return mProtocol.OneWireEchoSuppression; }
            set { mProtocol.OneWireEchoSuppression = value; }
        }

        public uint ConnectTimeout
        {
            get;
            set;
        }

        public uint ResponseTimeout
        {
            get { return mProtocol.ResponseTimeout; }
            set { mProtocol.ResponseTimeout = value; }
        }

        public uint EndOfMessageTimeout
        {
            get { return mProtocol.EndOfMessageTimeout; }
            set { mProtocol.EndOfMessageTimeout = value; }
        }

        public uint InterFrameDelay
        {
            get { return mInterFrameDelay; }
            set { SetInterFrameDelay( value);
                  mInterFrameDelay = value; 
                }
        }

        public uint Tries
        {
            get;
            set;
        }

        public bool RetryOnException11
        {
            get;
            set;
        }

        // diagnostics counters
        public uint SuccessCount
        {
            get;
            private set;
        }
        public uint RetryCount
        {
            get;
            private set;
        }
        public uint FatalCount
        {
            get;
            private set;
        }
        public uint NoResponseCount
        {
            get;
            private set;
        }
        public uint ResponseCrcErrorCount
        {
            get;
            private set;
        }
        public uint ResponseFormatErrorCount
        {
            get;
            private set;
        }
        public uint ResponseMBAPheaderErrorCount
        {
            get;
            private set;
        }
        public uint TcpConnectCount
        {
            // TCP connect successful count
            get { return mChannel.ConnectCount; }
            private set { mChannel.ConnectCount = value; }
        }
        public uint TcpConnectFailCount
        {
            // TCP connect failed count
            get { return mChannel.ConnectFailCount; }
            private set { mChannel.ConnectFailCount = value; }
        }
        public uint TcpConnectionLostCount
        {
            // TCP connection was closed by the server count
            get { return mChannel.ConnectionLostCount; }
            private set { mChannel.ConnectionLostCount = value; }
        }
        public uint TcpConnectionClosedBecauseOfResponseError
        {
            // We closed TCP connection because of response error count
            get { return mProtocol.ConnectionClosedBecauseOfResponseErrorCount; }
            private set { mProtocol.ConnectionClosedBecauseOfResponseErrorCount = value; }
        }
        public uint OneWireEchoErrorCount
        {
            get;
            private set;
        }
        public uint IllegalFunctionExceptionCount
        {
            get;
            private set;
        }
        public uint IllegalAddressExceptionCount
        {
            get;
            private set;
        }
        public uint IllegalDataExceptionCount
        {
            get;
            private set;
        }
        public uint GatewayPathUnavailableExceptionCount
        {
            get;
            private set;
        }
        public uint GatewayTargetdeviceFailedToRespondExceptionCount
        {
            get;
            private set;
        }
        public uint OtherExceptionCount
        {
            get;
            private set;
        }
        public uint TotalExceptionCount
        {
            get;
            private set;
        }
        public bool IsTcpChannel
        {
            get { return ((mChannel != null) && mChannel.IsTcpChannel); }
        }

        #endregion

        #region methods

        public void Open()
        {
            // throws exception on fail!

            mChannel.ConnectTimeout = ConnectTimeout;

            // throws exception on fail!
            mChannel.Open();

            mIsOpen = true;
        }

        public void Close()
        {
            mIsOpen = false;

            mChannel.Close();
        }

        public void ClearCounters()
        {
            SuccessCount = 0;
            RetryCount = 0;
            FatalCount = 0;
            NoResponseCount = 0;
            ResponseCrcErrorCount = 0;
            ResponseFormatErrorCount = 0;
            ResponseMBAPheaderErrorCount = 0;
            mChannel.ConnectCount = 0;
            mChannel.ConnectFailCount = 0;
            mChannel.ConnectionLostCount = 0;
            TcpConnectionClosedBecauseOfResponseError = 0;
            OneWireEchoErrorCount = 0;
            IllegalFunctionExceptionCount = 0;
            IllegalAddressExceptionCount = 0;
            IllegalDataExceptionCount = 0;
            GatewayPathUnavailableExceptionCount = 0;
            GatewayTargetdeviceFailedToRespondExceptionCount = 0;
            OtherExceptionCount = 0;
            TotalExceptionCount = 0;
        }

        #endregion

        protected void AdjustResultCounters(Result result)
        {
            switch (result)
            {
                case Result.SUCCESS:
                    ++SuccessCount;
                    break;
                case Result.ILLEGALFUNCTIONEXCEPTION:
                    ++IllegalFunctionExceptionCount;
                    ++TotalExceptionCount;
                    break;
                case Result.ILLEGALADDRESSEXCEPTION:
                    ++IllegalAddressExceptionCount;
                    ++TotalExceptionCount;
                    break;
                case Result.ILLEGALDATAVALUEEXCEPTION:
                    ++IllegalDataExceptionCount;
                    ++TotalExceptionCount;
                    break;
                case Result.GATEWAYPATHUNAVAILABLEEXCEPTION:
                    ++GatewayPathUnavailableExceptionCount;
                    ++TotalExceptionCount;
                    break;
                case Result.GATEWAYTARGETDEVICEFAILEDTORESPONDEXCEPTION:
                    ++GatewayTargetdeviceFailedToRespondExceptionCount;
                    ++TotalExceptionCount;
                    break;
                case Result.SLAVEDEVICEFAILUREEXCEPTION:
                case Result.ACKFORLONGDURATIONEXCEPTION:
                case Result.SLAVEDEVICEBUSYEXCEPTION:
                case Result.NEGATIVEACKEXCEPTION:
                case Result.MEMORYPARITYERROREXCEPTION:
                case Result.UNDEFINEDEXCEPTION:
                    ++OtherExceptionCount;
                    ++TotalExceptionCount;
                    break;
                case Result.NORESPONSE:
                    ++NoResponseCount;
                    break;
                case Result.RESPONSEFORMATERROR:
                    ++ResponseFormatErrorCount;
                    break;
                case Result.RESPONSECRCERROR:
                    ++ResponseCrcErrorCount;
                    break;
                case Result.RESPONSEMBAPHEADERERROR:
                    ++ResponseMBAPheaderErrorCount;
                    break;
                case Result.ONEWIREECHOERROR:
                    ++OneWireEchoErrorCount;
                    break;
                case Result.CHANNELNOTOPEN:
                case Result.UNSUPPORTED:
                case Result.INVALIDARGUMENT:
                default:
                    // not counted
                    break;
            }
        }

        #region Read/Write functions

        public Result ReadCoils(byte address, ushort startcoil, ushort coilcount, out byte[] values)
        {
            values = null;

            // we build the frame in RTU format, no CRC
            // Modbus specs state: read 2000 coils max
            if (coilcount <= 2000)
            {
                // build message
                byte[] query = new byte[256];
                int Idx = 0;
                query[Idx++] = address;                   // slave id
                query[Idx++] = (byte)1;                   // function code
                query[Idx++] = (byte)(startcoil >> 8);    // startreg addr high
                query[Idx++] = (byte)(startcoil);         // startreg addr low
                query[Idx++] = (byte)(coilcount >> 8);    // regcount high
                query[Idx++] = (byte)(coilcount);         // regcount low

                byte[] response;
                int NrOfDatabytes = (coilcount + 7) / 8;
                int expectedresponselength = 1 + 1 + 1 + NrOfDatabytes;   // addr, func, bytecnt, databytes
                Result retVal = ProtocolSendReceive(query, Idx, out response, expectedresponselength);

                // slave address and function code echo are already checked by ProtocolSendReceive()

                // valid response?
                if (retVal == Result.SUCCESS)
                {
                    // check response length
                    // check bytecount field
                    if(  (response.Length == expectedresponselength)
                        && (response[2] == NrOfDatabytes))
                    {
                        values = new byte[NrOfDatabytes];
                        Array.Copy(response, 3, values, 0, NrOfDatabytes);
                        // first coil is in bit0 of first byte
                    }
                    else
                    {
                        retVal = Result.RESPONSEFORMATERROR;
                    }

                    // update statistics counters
                    AdjustResultCounters(retVal);
                }

                return retVal;
            }
            else
            {
                return Result.INVALIDARGUMENT;
            }
        }

        public Result ReadInputRegisters(byte address, ushort startreg, ushort regcount, out short[] values)
        {
            values = null;

            // we build the frame in RTU format, no CRC
            // RTU frame with CRC is max 256 bytes
            // check regcount for response frame <= 256 bytes (addr,func,bytecnt,databytes,crc)
            if ((1 + 1 + 1 + (regcount * 2) + 2) <= 256)
            {
                // build query
                byte[] query = new byte[256];
                int Idx = 0;
                query[Idx++] = address;                   // slave id
                query[Idx++] = (byte)4;                   // function code
                query[Idx++] = (byte)(startreg >> 8);     // startreg addr high
                query[Idx++] = (byte)(startreg);          // startreg addr low
                query[Idx++] = (byte)(regcount >> 8);     // reg regcount high
                query[Idx++] = (byte)(regcount);          // reg regcount low

                byte[] response;
                int NrOfDatabytes = 2 * regcount;
                int expectedresponselength = 1 + 1 + 1 + NrOfDatabytes;   // addr, func, bytecnt, 2*regcnt
                Result retVal = ProtocolSendReceive(query, Idx, out response, expectedresponselength);

                // slave address and function code echo are already checked by ProtocolSendReceive()

                // valid response?
                if (retVal == Result.SUCCESS)
                {
                    // check response length
                    // check bytecount field
                    if ((response.Length == expectedresponselength)
                        && (response[2] == NrOfDatabytes))
                    {
                        values = new short[regcount];
                        for (uint index = 0; index < values.Length; ++index)
                        {
                            values[index] = response[3 + (2 * index)];
                            values[index] <<= 8;
                            values[index] += response[3 + (2 * index) + 1];
                        }
                    }
                    else
                    {
                        retVal = Result.RESPONSEFORMATERROR;
                    }

                    // update statistics counters
                    AdjustResultCounters(retVal);
                }

                return retVal;
            }
            else
            {
                return Result.INVALIDARGUMENT;
            }
        }

        public Result ReadHoldingRegisters(byte address, ushort startreg, ushort regcount, out short[] values)
        {
            values = null;

            // we build the frame in RTU format, no CRC
            // RTU frame with CRC is max 256 bytes
            // check regcount for response frame <= 256 bytes (addr,func,bytecnt,databytes,crc)
            if ((1 + 1 + 1 + (regcount * 2) + 2) <= 256)
            {
                // build query
                byte[] query = new byte[256];
                int Idx = 0;
                query[Idx++] = address;                   // slave id
                query[Idx++] = (byte)3;                   // function code
                query[Idx++] = (byte)(startreg >> 8);     // startreg addr high
                query[Idx++] = (byte)(startreg);          // startreg addr low
                query[Idx++] = (byte)(regcount >> 8);     // reg regcount high
                query[Idx++] = (byte)(regcount);          // reg regcount low

                byte[] response;
                int NrOfDatabytes = 2 * regcount;
                int expectedresponselength = 1 + 1 + 1 + NrOfDatabytes;   // addr, func, bytecnt, 2*regcnt
                Result retVal = ProtocolSendReceive(query, Idx, out response, expectedresponselength);

                // slave address and function code echo are already checked by ProtocolSendReceive()

                // valid response?
                if (retVal == Result.SUCCESS)
                {
                    // check response length
                    // check bytecount field
                    if(  (response.Length == expectedresponselength)
                        && (response[2] == NrOfDatabytes))
                    {
                        values = new short[regcount];
                        for (uint index = 0; index < values.Length; ++index)
                        {
                            values[index] = response[3 + (2*index)];
                            values[index] <<= 8;
                            values[index] += response[3 + (2 * index) + 1];
                        }
                    }
                    else
                    {
                        retVal = Result.RESPONSEFORMATERROR;
                    }

                    // update statistics counters
                    AdjustResultCounters(retVal);
                }

                return retVal;
            }
            else
            {
                return Result.INVALIDARGUMENT;
            }
        }

        public Result WriteSingleHoldingRegister(byte address, ushort startreg, short value)
        {
            // we build the frame in RTU format, no CRC
            // RTU frame with CRC is max 256 bytes
            // FC6 frames have fixed length, so no check needed

            // build query
            byte[] query = new byte[256];
            int Idx = 0;
            query[Idx++] = address;                   // slave id
            query[Idx++] = (byte)6;                   // function code
            query[Idx++] = (byte)(startreg >> 8);     // reg addr high
            query[Idx++] = (byte)(startreg);          // reg addr low
            query[Idx++] = (byte)(value >> 8);        // value high
            query[Idx++] = (byte)(value);             // value low

            byte[] response;
            int expectedresponselength = 1 + 1 + 2 + 2;   // addr, func, regaddr, value
            Result retVal = ProtocolSendReceive(query, Idx, out response, expectedresponselength);

            // slave address and function code echo are already checked by ProtocolSendReceive()

            // valid response?
            if (retVal == Result.SUCCESS)
            {
                // check response length
                // check all fields (echo of query)
                if ((response.Length == expectedresponselength)
                    && (response[2] == query[2])
                    && (response[3] == query[3])
                    && (response[4] == query[4])
                    && (response[5] == query[5]))
                {
                    // okay
                }
                else
                {
                    retVal = Result.RESPONSEFORMATERROR;
                }

                // update statistics counters
                AdjustResultCounters(retVal);
            }

            return retVal;
        }

        public Result WriteMultipleHoldingRegisters(byte address, ushort startreg, ushort regcount, short[] values)
        {
            // we build the frame in RTU format, no CRC
            // RTU frame with CRC is max 256 bytes
            // check for query <= 256 bytes (addr,func,startreg,regcnt,bytecnt,databytes,crc)

            if ( (values.Length >= regcount)
                && (1 + 1 + 2 + 2 + 1 + (regcount * 2) + 2) <= 256)
            {
                // build query
                byte[] query = new byte[256];
                int Idx = 0;
                query[Idx++] = address;               // slave id
                query[Idx++] = (byte)16;              // function code
                query[Idx++] = (byte)(startreg >> 8); // reg addr high
                query[Idx++] = (byte)(startreg);      // reg addr low
                query[Idx++] = (byte)(regcount >> 8); // reg regcount high
                query[Idx++] = (byte)(regcount);      // reg regcount low
                // add databytes
                query[Idx++] = (byte)(regcount * 2);   // number of databytes
                for (int valuesIdx = 0; valuesIdx < regcount; ++valuesIdx)
                {
                    query[Idx++] = (byte)(values[valuesIdx] >> 8);
                    query[Idx++] = (byte)values[valuesIdx];
                }

                byte[] response;
                int expectedresponselength = 1 + 1 + 2 + 2;   // addr, func, regaddr, regcnt
                Result retVal = ProtocolSendReceive(query, Idx, out response, expectedresponselength);

                // slave address and function code echo are already checked by ProtocolSendReceive()

                // valid response?
                if (retVal == Result.SUCCESS)
                {
                    // check response length
                    // check all fields (echo of query)
                    if ((response.Length == expectedresponselength)
                        && (response[2] == query[2])
                        && (response[3] == query[3])
                        && (response[4] == query[4])
                        && (response[5] == query[5]))
                    {
                        // okay
                    }
                    else
                    {
                        retVal = Result.RESPONSEFORMATERROR;
                    }

                    // update statistics counters
                    AdjustResultCounters(retVal);
                }

                return retVal;
            }
            else
            {
                return Result.INVALIDARGUMENT;
            }
        }

        public Result SendQuery( byte[] Query, ushort QueryLen, out byte[] Response)
        {
            // Query[] is RTU format, first byte is slave-address, no CRC
            // Response[] is RTU format, first byte is slave-address, no CRC
            // Response will be != null if we received a valid response (easier for the caller)

            Response = null;

            // RTU frame plus CRC is max 256 bytes
            // check query frame frame <= 256 bytes (addr,func,data,crc)
            if ((QueryLen + 2) <= 256)
            {
                // get the expected response length
                int expectedresponselength = DetermineModbusResponseLengthFromQuery(Query, QueryLen);
                // if we don't know it, expect 256 and let end-of-message-timeout decide
                if( expectedresponselength == 0) expectedresponselength = 256;
                // send query, get response
                Result retVal = ProtocolSendReceive(Query, QueryLen, out Response, expectedresponselength);

                // slave address and function code echo are already checked by ProtocolSendReceive()

                // we pass the response as it is, no further checks

                if (retVal == Result.SUCCESS)
                {
                    // success response

                    // update statistics counters
                    AdjustResultCounters(retVal);
                }
                else if (IsExceptionResponse(retVal))
                {
                    // exception response
                }
                else
                {
                    // no (valid) response
                    Response = null;
                }

                return retVal;
            }
            else
            {
                return Result.INVALIDARGUMENT;
            }
        }

        #endregion

        public static bool IsExceptionResponse( Result resultCode)
        {
            return ((resultCode >= Result.ILLEGALFUNCTIONEXCEPTION) && (resultCode <= Result.UNDEFINEDEXCEPTION)); 
        }

        // convert resultcode to text
        public static string GetResultText(Result resultCode)
        {
            string ReturnValue;

            switch (resultCode)
            {
                case Result.SUCCESS: ReturnValue = "SUCCESS";
                    break;
                case Result.ILLEGALFUNCTIONEXCEPTION: ReturnValue = "ILLEGAL FUNCTION EXCEPTION (1)";
                    break;
                case Result.ILLEGALADDRESSEXCEPTION: ReturnValue = "ILLEGAL ADDRESS EXCEPTION (2)";
                    break;
                case Result.ILLEGALDATAVALUEEXCEPTION: ReturnValue = "ILLEGAL DATA VALUE EXCEPTION (3)";
                    break;
                case Result.SLAVEDEVICEFAILUREEXCEPTION: ReturnValue = "SLAVE DEVICE FAILURE EXCEPTION (4)";
                    break;
                case Result.ACKFORLONGDURATIONEXCEPTION: ReturnValue = "ACKNOWLEDGE FOR LONG DURATION EXCEPTION (5)";
                    break;
                case Result.SLAVEDEVICEBUSYEXCEPTION: ReturnValue = "SLAVE DEVICE BUSY EXCEPTION (6)";
                    break;
                case Result.NEGATIVEACKEXCEPTION: ReturnValue = "NEGATIVE ACKNOWLEDGEMENT EXCEPTION (7)";
                    break;
                case Result.MEMORYPARITYERROREXCEPTION: ReturnValue = "MEMORY PARITY ERROR EXCEPTION (8)";
                    break;
                case Result.GATEWAYPATHUNAVAILABLEEXCEPTION: ReturnValue = "GATEWAY PATH UNAVAILABLE EXCEPTION (10)";
                    break;
                case Result.GATEWAYTARGETDEVICEFAILEDTORESPONDEXCEPTION: ReturnValue = "GATEWAY TARGET DEVICE FAILED TO RESPOND EXCEPTION (11)";
                    break;
                case Result.UNDEFINEDEXCEPTION: ReturnValue = "UNDEFINED EXCEPTION CODE";					
                    break;
                case Result.NORESPONSE: ReturnValue = "NO RESPONSE";
                    break;
                case Result.RESPONSEFORMATERROR: ReturnValue = "RESPONSE FORMAT ERROR";
                    break;
                case Result.RESPONSECRCERROR: ReturnValue = "RESPONSE CRC ERROR";
                    break;
                case Result.RESPONSEMBAPHEADERERROR: ReturnValue = "RESPONSE MBAP HEADER ERROR";
                    break;
                case Result.ONEWIREECHOERROR: ReturnValue = "ONEWIRE ECHO ERROR";
                    break;
                case Result.CHANNELNOTOPEN: ReturnValue = "CHANNEL NOT OPEN";
                    break;
                case Result.UNSUPPORTED: ReturnValue = "UNSUPPORTED";
                    break;
                case Result.INVALIDARGUMENT: ReturnValue = "INVALID ARGUMENT";
                    break;
                default: ReturnValue = "Result code " + (int)resultCode;
                    break;
            }

            return ReturnValue;
        }

        // calculate expected response length from query message
        public int DetermineModbusResponseLengthFromQuery(byte[] Query, int Length)
        {
            // Query[] in RTU format, first byte is slave-address, CRC is optional
            // returns length of response in RTU format, first byte is slave-address, no CRC
            // returns 0 if we don't recognize function code and length is not determined

            if ((Length >= 6) && (Query[1] == 1))
            {
                // Read Coils
                int n = (Query[4] * 256) + Query[5];
                return 3 + ((n + 7) / 8);
            }
            else if ((Length >= 6) && (Query[1] == 2))
            {
                // Read Discrete Inputs
                int n = (Query[4] * 256) + Query[5];
                return 3 + ((n + 7) / 8);
            }
            else if ((Length >= 6) && (Query[1] == 3))
            {
                // Read Holding Registers
                int n = (Query[4] * 256) + Query[5];
                return 3 + (n * 2);
            }
            else if ((Length >= 6) && (Query[1] == 4))
            {
                // Read Input Registers
                int n = (Query[4] * 256) + Query[5];
                return 3 + (n * 2);
            }
            else if ((Length >= 6) && (Query[1] == 5))
            {
                //  Write Single Coil
                return 6;
            }
            else if ((Length >= 6) && (Query[1] == 6))
            {
                // Write Single Register
                return 6;
            }
            else if ((Length >= 2) && (Query[1] == 7))
            {
                // Read Exception Status
                return 3;
            }
            else if ((Length >= 7) && (Query[1] == 15))
            {
                // Write Multiple Coils
                return 6;
            }
            else if ((Length >= 7) && (Query[1] == 16))
            {
                // Write Multiple registers
                return 6;
            }
            else if ((Length >= 8) && (Query[1] == 22))
            {
                // Mask Write Register
                return 8;
            }
            else if ((Length >= 11) && (Query[1] == 23))
            {
                // Read/Write Multiple registers
                int n = (Query[4] * 256) + Query[5];
                return 3 + (n * 2);
            }

            return 0;
        }

        public static Result ExceptioncodeToResult( byte ExceptionCode)
        {
            // convert modbus exception code to our Result code

            switch (ExceptionCode)
            {
                case 1: return Result.ILLEGALFUNCTIONEXCEPTION;
                case 2: return Result.ILLEGALADDRESSEXCEPTION;
                case 3: return Result.ILLEGALDATAVALUEEXCEPTION;
                case 4: return Result.SLAVEDEVICEFAILUREEXCEPTION;
                case 5: return Result.ACKFORLONGDURATIONEXCEPTION;
                case 6: return Result.SLAVEDEVICEBUSYEXCEPTION;
                case 7: return Result.NEGATIVEACKEXCEPTION;
                case 8: return Result.MEMORYPARITYERROREXCEPTION;
                // case 9: undefined
                case 10: return Result.GATEWAYPATHUNAVAILABLEEXCEPTION;
                case 11: return Result.GATEWAYTARGETDEVICEFAILEDTORESPONDEXCEPTION;
            }

            return Result.UNDEFINEDEXCEPTION;
        }

        private Result ProtocolSendReceive(byte[] query, int querylength, out byte[] response, int expectedresponselength)
        {
            Result result = Result.SUCCESS;

            // do a number of tries
            uint mytries = 0;
            do
            {
                // update the retry counter
                if (mytries > 0)
                {
                    ++RetryCount;
                }

                // try
                result = mProtocol.ProtocolSendReceive( query, querylength, out response, expectedresponselength);
                ++mytries;

                if (result != Result.SUCCESS)
                {
                    // adjust counters for transport errors and exceptions, upper layers check for more errors
                    AdjustResultCounters(result);
                }
            }
            while (ResultNeedsRetry(result) && (mytries < Tries));

            // all tries failed? then it's fatal
            if (result != Result.SUCCESS)
            {
                ++FatalCount;
            }

            return result;
        }

        bool ResultNeedsRetry(Result result)
        {
            bool bRetry = (RetryOnException11 && (result == Result.GATEWAYTARGETDEVICEFAILEDTORESPONDEXCEPTION))
                        || (result == Result.NORESPONSE)
                        || (result == Result.RESPONSEFORMATERROR)
                        || (result == Result.RESPONSECRCERROR)
                        || (result == Result.ONEWIREECHOERROR)
                        || (result == Result.RESPONSEMBAPHEADERERROR)
                        || (result == Result.CHANNELNOTOPEN);

            return bRetry;
        }

        // Close the TCP channel, for short-lived TCP connections.
        // Ignored if the channel is not open or not a TCP channel.
        public void CloseShortLivedTcpChannelConnection()
        {
            // deze mChannel.IsOpen wijzigen
            if (mChannel.IsOpen && mChannel.IsTcpChannel)
            {
                mChannel.Close();
            }
        }

        /// <summary>
        /// Set inter-frame delay
        /// 0 means set default: is 4 charactertimes for serial channel, 0ms for TCP channel
        /// >0 means delay in ms
        /// </summary>
        private void SetInterFrameDelay( uint Delay)
        {
            if ((Delay == 0) && (mChannel.IsSerialChannel))
            {
                // default, set to 4 * Tchar, naar boven afronden, ms
                uint protdelay = mProtocol.InterFrameDelay = (40000 + (mChannel.Baudrate - 1)) / mChannel.Baudrate;
                // met minimum van 2ms voor hoge baudrates
                if (protdelay < 2) protdelay = 2;
                mProtocol.InterFrameDelay = protdelay;
            }
            else
            {
                // fixed ms
                mProtocol.InterFrameDelay = Delay;
            }
        }

    }
}
