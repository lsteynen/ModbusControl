using System;
using System.Collections.Generic;
using System.Text;
using LST.Helpers;

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
            ONEWIREECHOERROR,
            NORESPONSE,
            RESPONSEPROTOCOLERROR,
            RESPONSESHORTANDCRCERROR,
            RESPONSECRCERROR,
            RESPONSEMBAPHEADERERROR,
            RESPONSEDATAERROR,
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
            set
            {
                SetInterFrameDelay(value);
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

        public uint ResponseProtocolErrorCount
        {
            get;
            private set;
        }

        public uint ResponseShortAndCrcErrorCount
        {
            get;
            private set;
        }

        public uint ResponseCrcErrorCount
        {
            get;
            private set;
        }

        public uint ResponseMBAPheaderErrorCount
        {
            get;
            private set;
        }

        public uint ResponseDataErrorCount
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
            OneWireEchoErrorCount = 0;
            NoResponseCount = 0;
            ResponseProtocolErrorCount = 0;
            ResponseShortAndCrcErrorCount = 0;
            ResponseCrcErrorCount = 0;
            ResponseMBAPheaderErrorCount = 0;
            ResponseDataErrorCount = 0;
            mChannel.ConnectCount = 0;
            mChannel.ConnectFailCount = 0;
            mChannel.ConnectionLostCount = 0;
            TcpConnectionClosedBecauseOfResponseError = 0;
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
                case Result.ONEWIREECHOERROR:
                    ++OneWireEchoErrorCount;
                    break;
                case Result.NORESPONSE:
                    ++NoResponseCount;
                    break;
                case Result.RESPONSEPROTOCOLERROR:
                    ++ResponseProtocolErrorCount;
                    break;
                case Result.RESPONSESHORTANDCRCERROR:
                    ++ResponseShortAndCrcErrorCount;
                    break;
                case Result.RESPONSECRCERROR:
                    ++ResponseCrcErrorCount;
                    break;
                case Result.RESPONSEMBAPHEADERERROR:
                    ++ResponseMBAPheaderErrorCount;
                    break;
                case Result.RESPONSEDATAERROR:
                    ++ResponseDataErrorCount;
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

        public Result ReadHoldingRegisters(byte address, UInt16 startreg, UInt16 regcount, out Int16[] values, bool allowAll = true)
        {
            values = null;

            // we build the frame in RTU format, no CRC
            // RTU frame with CRC is max 256 bytes
            // check regcount for response frame <= 256 bytes (addr,func,bytecnt,databytes,crc)
            if (((1 + 1 + 1 + (regcount * 2) + 2) <= 256) || allowAll)
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
                    if ((response.Length == expectedresponselength)
                        && (response[2] == NrOfDatabytes))
                    {
                        values = new Int16[regcount];
                        for (uint index = 0; index < values.Length; ++index)
                        {
                            values[index] = response[3 + (2 * index)];
                            values[index] <<= 8;
                            values[index] += response[3 + (2 * index) + 1];
                        }
                    }
                    else
                    {
                        retVal = Result.RESPONSEDATAERROR;
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

        public Result WriteSingleHoldingRegister(byte address, UInt16 startreg, Int16 value)
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
                    retVal = Result.RESPONSEDATAERROR;
                }

                // update statistics counters
                AdjustResultCounters(retVal);
            }

            return retVal;
        }

        public Result WriteMultipleHoldingRegisters(byte address, UInt16 startreg, UInt16 regcount, Int16[] values)
        {
            // we build the frame in RTU format, no CRC
            // RTU frame with CRC is max 256 bytes
            // check for query <= 256 bytes (addr,func,startreg,regcnt,bytecnt,databytes,crc)

            if ((values.Length >= regcount)
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
                        retVal = Result.RESPONSEDATAERROR;
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

        public Result ReadInputRegisters(byte address, UInt16 startreg, UInt16 regcount, out Int16[] values, bool allowAll = true)
        {
            values = null;

            // we build the frame in RTU format, no CRC
            // RTU frame with CRC is max 256 bytes
            // check regcount for response frame <= 256 bytes (addr,func,bytecnt,databytes,crc)
            if (((1 + 1 + 1 + (regcount * 2) + 2) <= 256) || allowAll)
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
                        values = new Int16[regcount];
                        for (uint index = 0; index < values.Length; ++index)
                        {
                            values[index] = response[3 + (2 * index)];
                            values[index] <<= 8;
                            values[index] += response[3 + (2 * index) + 1];
                        }
                    }
                    else
                    {
                        retVal = Result.RESPONSEDATAERROR;
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

        public Result ReadCoils(byte address, UInt16 startreg, UInt16 regcount, out byte[] values, bool allowAll = true)
        {
            values = null;

            // we build the frame in RTU format, no CRC
            // Modbus specs state: read 2000 coils max
            if ((regcount <= 2000) || allowAll)
            {
                // build message
                byte[] query = new byte[256];
                int Idx = 0;
                query[Idx++] = address;                   // slave id
                query[Idx++] = (byte)1;                   // function code
                query[Idx++] = (byte)(startreg >> 8);    // startreg addr high
                query[Idx++] = (byte)(startreg);         // startreg addr low
                query[Idx++] = (byte)(regcount >> 8);    // regcount high
                query[Idx++] = (byte)(regcount);         // regcount low

                byte[] response;
                int NrOfDatabytes = (regcount + 7) / 8;
                int expectedresponselength = 1 + 1 + 1 + NrOfDatabytes;   // addr, func, bytecnt, databytes
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
                        values = new byte[NrOfDatabytes];
                        Array.Copy(response, 3, values, 0, NrOfDatabytes);
                        // first coil is in bit0 of first byte
                    }
                    else
                    {
                        retVal = Result.RESPONSEDATAERROR;
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

        public Result WriteSingleCoil(byte address, UInt16 startreg, int value, bool allowAll = true)
        {
            // we build the frame in RTU format, no CRC
            // RTU frame with CRC is max 256 bytes
            // check for query <= 256 bytes (addr,func,startreg,regcnt,bytecnt,databytes,crc)
            // value must be 0 or 1
            
            if ((value >= 0) && (value <= 1))
            {
                // build query
                byte[] query = new byte[256];
                int Idx = 0;
                query[Idx++] = address;               // slave id
                query[Idx++] = (byte)5;               // function code
                query[Idx++] = (byte)(startreg >> 8); // reg addr high
                query[Idx++] = (byte)(startreg);      // reg addr low
                if (value == 1)
                {
                    query[Idx++] = (byte)0xFF;          // send 0xFF00 or 0x0000
                    query[Idx++] = (byte)0x00;      
                }
                else
                {
                    query[Idx++] = (byte)0x00; 
                    query[Idx++] = (byte)0x00;      
                }
                
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
                        retVal = Result.RESPONSEDATAERROR;
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

        public Result WriteMultipleCoils(byte address, UInt16 startreg, UInt16 regcount, byte[] values, bool allowAll = true)
        {
            // we build the frame in RTU format, no CRC
            // RTU frame with CRC is max 256 bytes
            // check for query <= 256 bytes (addr,func,startreg,regcnt,bytecnt,databytes,crc)
            // write 1976 coils max
            int nrOfDatabytes = ((regcount + 7) / 8);

            if ((values.Length >= nrOfDatabytes)
                && (1 + 1 + 2 + 2 + 1 + nrOfDatabytes + 2) <= 256)
            {
                // build query
                byte[] query = new byte[256];
                int Idx = 0;
                query[Idx++] = address;               // slave id
                query[Idx++] = (byte)15;              // function code
                query[Idx++] = (byte)(startreg >> 8); // reg addr high
                query[Idx++] = (byte)(startreg);      // reg addr low
                query[Idx++] = (byte)(regcount >> 8); // reg regcount high
                query[Idx++] = (byte)(regcount);      // reg regcount low
                // add databytes
                query[Idx++] = (byte)nrOfDatabytes;            // number of databytes
                Array.Copy(values, 0, query, Idx, nrOfDatabytes);
                Idx += nrOfDatabytes;

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
                        retVal = Result.RESPONSEDATAERROR;
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

        public Result ReadDiscreteInputs(byte address, UInt16 startreg, UInt16 regcount, out byte[] values, bool allowAll = true)
        {
            values = null;

            // we build the frame in RTU format, no CRC
            // Modbus specs state: read 2000 coils max
            if ((regcount <= 2000) || allowAll)
            {
                // build message
                byte[] query = new byte[256];
                int Idx = 0;
                query[Idx++] = address;                   // slave id
                query[Idx++] = (byte)2;                   // function code
                query[Idx++] = (byte)(startreg >> 8);    // startreg addr high
                query[Idx++] = (byte)(startreg);         // startreg addr low
                query[Idx++] = (byte)(regcount >> 8);    // regcount high
                query[Idx++] = (byte)(regcount);         // regcount low

                byte[] response;
                int nrOfDatabytes = (regcount + 7) / 8;
                int expectedresponselength = 1 + 1 + 1 + nrOfDatabytes;   // addr, func, bytecnt, databytes
                Result retVal = ProtocolSendReceive(query, Idx, out response, expectedresponselength);

                // slave address and function code echo are already checked by ProtocolSendReceive()

                // valid response?
                if (retVal == Result.SUCCESS)
                {
                    // check response length
                    // check bytecount field
                    if ((response.Length == expectedresponselength)
                        && (response[2] == nrOfDatabytes))
                    {
                        values = new byte[nrOfDatabytes];
                        Array.Copy(response, 3, values, 0, nrOfDatabytes);
                        // first coil is in bit0 of first byte
                    }
                    else
                    {
                        retVal = Result.RESPONSEDATAERROR;
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

        public Result SendCustomQuery(byte address, byte[] query, UInt16 queryLen, out byte[] responseBuf)
        {
            responseBuf = null;

            // queryLen <= query buffer size?
            if (queryLen <= query.Length)
            { 
                // we build the frame in RTU format, no CRC
                // build message
                byte[] query2 = new byte[1 + queryLen];
                int Idx = 0;
                query2[Idx++] = address;                        // slave id
                Array.Copy(query, 0, query2, 1, queryLen);      // custom query
                Idx += queryLen;

                // expected response length is unknown
                Result retVal = ProtocolSendReceive(query2, query2.Length, out responseBuf, 0);

                // slave address and function code echo are already checked by ProtocolSendReceive()
                
                // reponse is already in responseBuf[]

                // update statistics counters
                AdjustResultCounters(retVal);

                return  retVal;
            }
            else
            {
                return Result.INVALIDARGUMENT;
            }
        }

        public Result SendQuery(byte[] Query, UInt16 QueryLen, out byte[] Response)
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
                // 0 means unknown
                int expectedresponselength = DetermineModbusResponseLengthFromQuery(Query, QueryLen);
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

        public static bool IsExceptionResponse(Result resultCode)
        {
            return ((resultCode >= Result.ILLEGALFUNCTIONEXCEPTION) && (resultCode <= Result.UNDEFINEDEXCEPTION));
        }

        // convert resultcode to text
        public static string GetResultText(Result resultCode)
        {
            string ReturnValue;

            switch (resultCode)
            {
                case Result.SUCCESS:
                    ReturnValue = "SUCCESS";
                    break;
                case Result.ILLEGALFUNCTIONEXCEPTION:
                    ReturnValue = "ILLEGAL FUNCTION EXCEPTION (1)";
                    break;
                case Result.ILLEGALADDRESSEXCEPTION:
                    ReturnValue = "ILLEGAL ADDRESS EXCEPTION (2)";
                    break;
                case Result.ILLEGALDATAVALUEEXCEPTION:
                    ReturnValue = "ILLEGAL DATA VALUE EXCEPTION (3)";
                    break;
                case Result.SLAVEDEVICEFAILUREEXCEPTION:
                    ReturnValue = "SLAVE DEVICE FAILURE EXCEPTION (4)";
                    break;
                case Result.ACKFORLONGDURATIONEXCEPTION:
                    ReturnValue = "ACKNOWLEDGE FOR LONG DURATION EXCEPTION (5)";
                    break;
                case Result.SLAVEDEVICEBUSYEXCEPTION:
                    ReturnValue = "SLAVE DEVICE BUSY EXCEPTION (6)";
                    break;
                case Result.NEGATIVEACKEXCEPTION:
                    ReturnValue = "NEGATIVE ACKNOWLEDGEMENT EXCEPTION (7)";
                    break;
                case Result.MEMORYPARITYERROREXCEPTION:
                    ReturnValue = "MEMORY PARITY ERROR EXCEPTION (8)";
                    break;
                case Result.GATEWAYPATHUNAVAILABLEEXCEPTION:
                    ReturnValue = "GATEWAY PATH UNAVAILABLE EXCEPTION (10)";
                    break;
                case Result.GATEWAYTARGETDEVICEFAILEDTORESPONDEXCEPTION:
                    ReturnValue = "GATEWAY TARGET DEVICE FAILED TO RESPOND EXCEPTION (11)";
                    break;
                case Result.UNDEFINEDEXCEPTION:
                    ReturnValue = "UNDEFINED EXCEPTION CODE";
                    break;
                case Result.ONEWIREECHOERROR:
                    ReturnValue = "ONEWIRE ECHO ERROR";
                    break;
                case Result.NORESPONSE:
                    ReturnValue = "NO RESPONSE";
                    break;
                case Result.RESPONSEPROTOCOLERROR:
                    ReturnValue = "RESPONSE PROTOCOL ERROR";
                    break;
                case Result.RESPONSESHORTANDCRCERROR:
                    ReturnValue = "RESPONSE TOO SHORT AND CRC ERROR";
                    break;
                case Result.RESPONSECRCERROR:
                    ReturnValue = "RESPONSE CRC ERROR";
                    break;
                case Result.RESPONSEMBAPHEADERERROR:
                    ReturnValue = "RESPONSE MBAP HEADER ERROR";
                    break;
                case Result.RESPONSEDATAERROR:
                    ReturnValue = "RESPONSE DATA ERROR";
                    break;
                case Result.CHANNELNOTOPEN:
                    ReturnValue = "CHANNEL NOT OPEN";
                    break;
                case Result.UNSUPPORTED:
                    ReturnValue = "UNSUPPORTED";
                    break;
                case Result.INVALIDARGUMENT:
                    ReturnValue = "INVALID ARGUMENT";
                    break;
                default:
                    ReturnValue = "Result code " + (int)resultCode;
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

        public static Result ExceptioncodeToResult(byte ExceptionCode)
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

            // expectedresponselength: expected response length
            //  normally 1-256
            //  0 means unknown, let transport layer work it out

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
                result = mProtocol.ProtocolSendReceive(query, querylength, out response, expectedresponselength);
                ++mytries;

                // adjust counters for transport errors and exceptions
                // must do here to count the errors of first tries
                if (result != Result.SUCCESS)
                {
                    AdjustResultCounters(result);
                }
            }
            while (ResultNeedsRetry(result) && (mytries < Tries));

            // all tries failed? then it's fatal
            if (result != Result.SUCCESS)
            {
                ++FatalCount;
            }

            // upper layers will check for more errors

            return result;
        }

        bool ResultNeedsRetry(Result result)
        {
            bool bRetry = ((result == Result.GATEWAYTARGETDEVICEFAILEDTORESPONDEXCEPTION) && RetryOnException11)
                        || (result == Result.ONEWIREECHOERROR)
                        || (result == Result.NORESPONSE)
                        || (result == Result.RESPONSEPROTOCOLERROR)
                        || (result == Result.RESPONSESHORTANDCRCERROR)
                        || (result == Result.RESPONSECRCERROR)
                        || (result == Result.RESPONSEMBAPHEADERERROR)
                        || (result == Result.RESPONSEDATAERROR)
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
        private void SetInterFrameDelay(uint Delay)
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
