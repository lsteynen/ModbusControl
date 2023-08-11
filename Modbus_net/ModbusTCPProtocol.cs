using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Modbus_net
{
    public class ModbusTcpProtocol : IModbusProtocol
    {
        private ushort mTransactionID;
        private byte mCurrentSlaveAddress;
        private byte mCurrentFunctioncode;
        
        // constructor

        public ModbusTcpProtocol()
        {
            mTransactionID = 0;
        }

        // properties

        public ICommChannel Channel { get; set; }
        public bool OneWireEchoSuppression { get; set; } // not used here
        public uint ResponseTimeout { get; set; }
        public uint EndOfMessageTimeout { get; set; } // not used here
        public uint InterFrameDelay { get; set; }
        public uint ConnectionClosedBecauseOfResponseErrorCount { get; set; }

        // events

#pragma warning disable 0067
        public event LogCallbackString OnInfo;
        public event LogCallbackString OnWarning;
        public event LogCallbackString OnError;
#pragma warning restore 0067

        // implementation

        /// <summary>
        /// Send a modbus query, get reponse.
        /// </summary>
        /// <param name="query">Query command, must be at least 2 bytes</param>
        /// <param name="querylength">Length of query, number of bytes</param>
        /// <param name="response">Where the response bytes will be returned. Array size will be match response length</param>
        /// <param name="expectedresponselength">Expected response length</param>
        /// <returns></returns>
        public ModbusProtocol.Result ProtocolSendReceive(byte[] query, int querylength, out byte[] response, int expectedresponselength)
        {
            // query must be in RTU format, start with slaveID, no CRC. Minimum length 2 bytes.

            // must assign
            response = null;

            // query must be at least 2 bytes
            if ((query.Length < 2) || (querylength < 2))
            {
                return ModbusProtocol.Result.INVALIDARGUMENT;
            }

            // remember these for checking later
            mCurrentSlaveAddress = query[0];
            mCurrentFunctioncode = query[1];

            // Set channel timeouts
            Channel.ResponseTimeout = ResponseTimeout;
            Channel.CharacterTimeout = EndOfMessageTimeout;

            // prepare channel (may also reconnect on connection-oriented channels)
            if (Channel.BeforeTransaction() == false)
            {
                return ModbusProtocol.Result.CHANNELNOTOPEN;
            }

            byte[] txframe = BuildQueryFrame(query, querylength);

            // Do inter-frame delay
            if (InterFrameDelay > 0)
            {
                Thread.Sleep((int)InterFrameDelay);
            }

            // Channel Write() can throw exception if connection is not open
            try
            {
                Channel.Write(txframe, 0, txframe.Length);
            }
            catch (Exception)
            {
                // Send failed
                return ModbusProtocol.Result.CHANNELNOTOPEN;
            }

            // get the response
            // slave address and function code echo are already checked by GetResponse()
            ModbusProtocol.Result result = GetResponse(out response, expectedresponselength);

            // close the TCP connection on certain errors, the next transaction will reopen it
            if (Channel.IsTcpChannel && ResultRequiresCloseConnection(result))
            {
                Channel.Close();
                ++ConnectionClosedBecauseOfResponseErrorCount;

                // log that we closed the connection
                if (OnWarning != null)
                {
                    string s = "Connection closed because of response error";
                    OnWarning(s);
                }
            }

            return result;
        }

        public byte[] BuildQueryFrame(byte[] message, int length)
        {
            // for Modbus/TCP protocol: add MBAP header, no CRC
            byte[] framedmessage = new byte[6 + length];

            // each query gets a new transaction number
            ++mTransactionID;

            // MBAP
            framedmessage[0] = (byte)(mTransactionID >> 8);
            framedmessage[1] = (byte)mTransactionID;
            framedmessage[2] = 0;
            framedmessage[3] = 0;
            framedmessage[4] = (byte)(length >> 8);
            framedmessage[5] = (byte)(length);

            // PDU
            Array.Copy(message, 0, framedmessage, 6, length);

            return framedmessage;
        }

        private ModbusProtocol.Result GetResponse( out byte[] response, int expectedlength)
        {
            // expectedlength is not used here, the MBAP header has the length

            byte[] header = new byte[6];    // buffer for MBAP header
            response = new byte[1000];       // buffer for response
            int readCnt = 0;                // nr of bytes received

            // read MBAP header, always 6 bytes
            readCnt = ReceiveBytes(ref header, header.Length);
            if (readCnt < 6)
            {
                // Timeout on read
                return (readCnt > 0) ? ModbusProtocol.Result.RESPONSEMBAPHEADERERROR : ModbusProtocol.Result.NORESPONSE;
            }

            // check MBAP header (0/1 is transaction ID, 2/3 is protocol ID, 4/5 is length)
            if (ToUShort(header[0], header[1]) == mTransactionID)
            {
                // check Protocol identifier
                if (ToUShort(header[2], header[3]) == 0)
                {
                    // This is a response to our request
                    ushort messageLength = ToUShort(header[4], header[5]);

                    // get the PDU
                    readCnt = ReceiveBytes(ref response, messageLength);
                    if( readCnt < messageLength)
                    {
                        // Timeout on read
                        return (readCnt > 0) ? ModbusProtocol.Result.RESPONSEFORMATERROR : ModbusProtocol.Result.NORESPONSE;
                    }

                    // slave address match?
                    if (response[0] == mCurrentSlaveAddress)
                    {
                        // functioncode match?
                        if (response[1] == mCurrentFunctioncode)
                        {
                            // good response
                            Array.Resize(ref response, messageLength);
                            return ModbusProtocol.Result.SUCCESS;
                        }
                        else if (response[1] == (0x80 | mCurrentFunctioncode)) // Exception response?
                        {
                            // return exception result
                            Array.Resize(ref response, 3);
                            return ModbusProtocol.ExceptioncodeToResult(response[2]);
                        }
                        else
                        {
                            // Invalid answer
                            return ModbusProtocol.Result.RESPONSEFORMATERROR;
                        }
                    }
                    else
                    {
                        // Invalid answer
                        return ModbusProtocol.Result.RESPONSEFORMATERROR;
                    }

                }
                else
                {
                    // Invalid protocol Identifier
                    return ModbusProtocol.Result.RESPONSEMBAPHEADERERROR;
                }
            }
            else
            {
                // Invalid transaction ID
                return ModbusProtocol.Result.RESPONSEMBAPHEADERERROR;
            }
        }

        bool ResultRequiresCloseConnection(ModbusProtocol.Result result)
        {
            bool bClose = (result == ModbusProtocol.Result.NORESPONSE)
                        || (result == ModbusProtocol.Result.RESPONSEFORMATERROR)
                        || (result == ModbusProtocol.Result.RESPONSEMBAPHEADERERROR)
                        || (result == ModbusProtocol.Result.CHANNELNOTOPEN);

            return bClose;
        }

        private int ReceiveBytes(ref byte[] buffer, int ntoread)
        {
            // read 'ntoread' bytes
            // or stop on timeout
            // returns number of bytes read
            
            return Channel.ReadWait(ref buffer, ntoread);
        }

        private ushort ToUShort(byte high, byte low)
        {
            return (ushort)(((int)high << 8) | low);
        }

    }
}
