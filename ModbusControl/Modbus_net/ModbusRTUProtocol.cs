using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Diagnostics;

namespace Modbus_net
{
    public class ModbusRtuProtocol : IModbusProtocol
    {
        private byte mCurrentSlaveAddress;
        private byte mCurrentFunctioncode;

        // constructor

        public ModbusRtuProtocol()
        {
        }

        // properties

        public ICommChannel Channel { get; set; }
        public bool OneWireEchoSuppression { get; set; }
        public uint ResponseTimeout { get; set; }
        public uint EndOfMessageTimeout { get; set; }
        public uint ConnectionClosedBecauseOfResponseErrorCount { get; set; } // not used here

        // events

#pragma warning disable 0067
        public event LogCallbackString OnInfo;
        public event LogCallbackString OnWarning;
        public event LogCallbackString OnError;
#pragma warning restore 0067

        // implementation

        public ModbusProtocol.Result ProtocolSendReceive(byte[] query, int querylength, out byte[] response, int expectedresponselength)
        {
            // excepts query in RTU format, start with slaveID, no CRC
            // excepts expectedresponselength in RTU format, no CRC

            // must assign
            response = null;

            // remember these for checking later
            mCurrentSlaveAddress = query[0];
            mCurrentFunctioncode = query[1];

            // Set channel timeouts
            Channel.ResponseTimeout = ResponseTimeout;
            Channel.CharacterTimeout = EndOfMessageTimeout;

            byte[] txframe = BuildQueryFrame(query, querylength);

            // prepare channel (may also reconnect on connection-oriented channels)
            if (Channel.BeforeTransaction() == false)
            {
                return ModbusProtocol.Result.CHANNELNOTOPEN;
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

            // must process echo of our query?
            if (OneWireEchoSuppression)
            {
                // must have some timeout to receive echo

                // remove the echo bytes from the input
                // we don't check the bytes
                byte[] echobuffer = new byte[300];
                int byteCnt = ReceiveBytes(ref echobuffer, txframe.Length);
                if (byteCnt < txframe.Length)
                {
                    // Echo error
                    return ModbusProtocol.Result.ONEWIREECHOERROR;
                }
            }

            // get the response
            // slave address and function code echo are already checked by GetResponse()
            ModbusProtocol.Result TmpResult = GetResponse( out response, expectedresponselength + 2);

            if (TmpResult == ModbusProtocol.Result.SUCCESS)
            {
                Channel.AfterTransaction();
            }

            return TmpResult;
        }

        public byte[] BuildQueryFrame(byte[] message, int length)
        {
            // for RTU mode: message is already correctly framed, just append CRC
            byte[] framedmessage = new byte[length + 2];
            Array.Copy(message, framedmessage, length);
            byte[] CRC = GetCRC(framedmessage, length);
            framedmessage[length] = CRC[0];
            framedmessage[length + 1] = CRC[1];

            return framedmessage;
        }

        public ModbusProtocol.Result GetResponse( out byte[] response, int expectedlength)
        {
            // expectedlength is used for serial channels to receive the expected number of bytes without waiting for and end-of-message timeout

            response = new byte[300];       // buffer for response
            byte[] tmpresponse = new byte[300];  // buffer for response part 2
            int byteCnt = 0;                // nr of bytes received

            // get first 2 bytes (then we know if it is the expected response or an exception)
            // ReceiveBytes() can read less bytes than wanted
            byteCnt = ReceiveBytes(ref response, 2);
            if (byteCnt < 2)
            {
                // Timeout on read
                return (byteCnt > 0) ? ModbusProtocol.Result.RESPONSEFORMATERROR : ModbusProtocol.Result.NORESPONSE;
            }

            // we got the first 2 bytes
            // check slave address
            if (response[0] == mCurrentSlaveAddress)
            {
                // Normal good response?
                if (response[1] == mCurrentFunctioncode)
                {
                    // assume expected length
                }
                else if (response[1] == (0x80 | mCurrentFunctioncode)) // Exception response?
                {
                    // Exception response
                    // receive 3 more bytes (exception code and CRC)
                    expectedlength = 2 + 3; 
                }
                else // functioncode does not match
                {
                    // receive bytes till end-of-message timeout
                    expectedlength = 300;
                }
            }
            else
            {
                // receiving garbage?
                // receive bytes till end-of-message timeout
                expectedlength = 300;
            }

            // Get the remaining bytes (we already have 2)
            // We may get less bytes than we want but it can still be a good response
            // because we don't have an expectedlength for unsupported function codes.
            // The bridge client may use function codes we don't support.
            // Use a temporary buffer and append it to response[]
            int tmpcount = ReceiveBytes(ref tmpresponse, expectedlength - 2);
            Array.Copy(tmpresponse, 0, response, 2, tmpcount);
            byteCnt += tmpcount;

            if (byteCnt == expectedlength)
            {
                Debug.Print(DateTime.Now.TimeOfDay + ": RTUprotocol: Received " + byteCnt + " bytes as expected");
            }
            else
            {
                Debug.Print(DateTime.Now.TimeOfDay + ": RTUprotocol: Received " + byteCnt + " bytes, expected " + expectedlength);
            }

            // minimum length of 4? (assume minimum is SlaveID+functioncode+CRC)
            if( (response[0] == mCurrentSlaveAddress) && (byteCnt >= 4))
            {
                // check CRC
                byte[] myCRC = GetCRC(response, byteCnt-2);
                if ((myCRC[0] == response[byteCnt - 2]) && (myCRC[1] == response[byteCnt - 1]))
                {
                    // CRC match

                    // slave address match?
                    if (response[0] == mCurrentSlaveAddress)
                    {
                        // functioncode match?
                        if (response[1] == mCurrentFunctioncode)
                        {
                            // good response
                            Array.Resize(ref response, byteCnt - 2);
                            return ModbusProtocol.Result.SUCCESS;
                        }
                        else if (response[1] == (0x80 | mCurrentFunctioncode)) // Exception response?
                        {
                            // return exception result
                            Array.Resize(ref response, 3);
                            return ModbusProtocol.ExceptioncodeToResult(response[2]);
                        }
                        else // functioncode does not match
                        {
                            return ModbusProtocol.Result.RESPONSEFORMATERROR;
                        }
                    }
                    else
                    {
                        /* Invalid response */
                        return ModbusProtocol.Result.RESPONSEFORMATERROR;
                    }
                }
                else
                {
                    // CRC error
                    return ModbusProtocol.Result.RESPONSECRCERROR;
                }
            }
            else
            {
                // Invalid response
                return ModbusProtocol.Result.RESPONSEFORMATERROR;
            }
        }

        static bool ByteArrayCompare(byte[] a1, byte[] a2, int count)
        {
            for (int i = 0; i < count; i++)
            {
                if (a1[i] != a2[i])
                    return false;
            }

            return true;
        }

        private int ReceiveBytes(ref byte[] buffer, int ntoread)
        {
            // read 'ntoread' bytes
            // or stop on timeout
            // returns number of bytes read

            return Channel.ReadWait(ref buffer, ntoread);
        }

        #region CRC Computation

        private static byte[] crcHi =
        {
            0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81,
            0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0,
            0x80, 0x41, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01,
            0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41,
            0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81,
            0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0,
            0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01,
            0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40,
            0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81,
            0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0,
            0x80, 0x41, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01,
            0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41,
            0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81,
            0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0,
            0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01,
            0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41,
            0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81,
            0x40
        };

        private static byte[] crcLo =
        {
            0x00, 0xC0, 0xC1, 0x01, 0xC3, 0x03, 0x02, 0xC2, 0xC6, 0x06, 0x07, 0xC7, 0x05, 0xC5, 0xC4,
            0x04, 0xCC, 0x0C, 0x0D, 0xCD, 0x0F, 0xCF, 0xCE, 0x0E, 0x0A, 0xCA, 0xCB, 0x0B, 0xC9, 0x09,
            0x08, 0xC8, 0xD8, 0x18, 0x19, 0xD9, 0x1B, 0xDB, 0xDA, 0x1A, 0x1E, 0xDE, 0xDF, 0x1F, 0xDD,
            0x1D, 0x1C, 0xDC, 0x14, 0xD4, 0xD5, 0x15, 0xD7, 0x17, 0x16, 0xD6, 0xD2, 0x12, 0x13, 0xD3,
            0x11, 0xD1, 0xD0, 0x10, 0xF0, 0x30, 0x31, 0xF1, 0x33, 0xF3, 0xF2, 0x32, 0x36, 0xF6, 0xF7,
            0x37, 0xF5, 0x35, 0x34, 0xF4, 0x3C, 0xFC, 0xFD, 0x3D, 0xFF, 0x3F, 0x3E, 0xFE, 0xFA, 0x3A,
            0x3B, 0xFB, 0x39, 0xF9, 0xF8, 0x38, 0x28, 0xE8, 0xE9, 0x29, 0xEB, 0x2B, 0x2A, 0xEA, 0xEE,
            0x2E, 0x2F, 0xEF, 0x2D, 0xED, 0xEC, 0x2C, 0xE4, 0x24, 0x25, 0xE5, 0x27, 0xE7, 0xE6, 0x26,
            0x22, 0xE2, 0xE3, 0x23, 0xE1, 0x21, 0x20, 0xE0, 0xA0, 0x60, 0x61, 0xA1, 0x63, 0xA3, 0xA2,
            0x62, 0x66, 0xA6, 0xA7, 0x67, 0xA5, 0x65, 0x64, 0xA4, 0x6C, 0xAC, 0xAD, 0x6D, 0xAF, 0x6F,
            0x6E, 0xAE, 0xAA, 0x6A, 0x6B, 0xAB, 0x69, 0xA9, 0xA8, 0x68, 0x78, 0xB8, 0xB9, 0x79, 0xBB,
            0x7B, 0x7A, 0xBA, 0xBE, 0x7E, 0x7F, 0xBF, 0x7D, 0xBD, 0xBC, 0x7C, 0xB4, 0x74, 0x75, 0xB5,
            0x77, 0xB7, 0xB6, 0x76, 0x72, 0xB2, 0xB3, 0x73, 0xB1, 0x71, 0x70, 0xB0, 0x50, 0x90, 0x91,
            0x51, 0x93, 0x53, 0x52, 0x92, 0x96, 0x56, 0x57, 0x97, 0x55, 0x95, 0x94, 0x54, 0x9C, 0x5C,
            0x5D, 0x9D, 0x5F, 0x9F, 0x9E, 0x5E, 0x5A, 0x9A, 0x9B, 0x5B, 0x99, 0x59, 0x58, 0x98, 0x88,
            0x48, 0x49, 0x89, 0x4B, 0x8B, 0x8A, 0x4A, 0x4E, 0x8E, 0x8F, 0x4F, 0x8D, 0x4D, 0x4C, 0x8C,
            0x44, 0x84, 0x85, 0x45, 0x87, 0x47, 0x46, 0x86, 0x82, 0x42, 0x43, 0x83, 0x41, 0x81, 0x80,
            0x40
        };

        private void GetCRC(byte messageByte, ref byte[] CRC)
        {
            int index = CRC[0] ^ messageByte;
            CRC[0] = Convert.ToByte(CRC[1] ^ crcHi[index]);
            CRC[1] = crcLo[index];
        }

        private byte[] GetCRC(byte[] message, int length)
        {
            byte[] CRC = new byte[2];
            CRC[0] = CRC[1] = 0xFF;

            for (int i = 0; i < length; i++)
            {
                int index = CRC[0] ^ message[i];
                CRC[0] = Convert.ToByte(CRC[1] ^ crcHi[index]);
                CRC[1] = crcLo[index];
            }

            return CRC;
        }

        #endregion

    }
}
