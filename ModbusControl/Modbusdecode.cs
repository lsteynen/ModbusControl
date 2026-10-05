using System;
using System.Text;

namespace ModbusControl
{
    public class ModbusDecoder
    {
        static int mHoldingRegistersBaseAddress;
        static int mInputRegistersBaseAddress;
        static int mCoilsBaseAddress;
        static int mDiscreteInputsBaseAddress;
        static bool mUnsigned;

        // constructor
        static ModbusDecoder()
        {
            // set defaults
            mHoldingRegistersBaseAddress = 40001;
            mInputRegistersBaseAddress = 30001;
            mCoilsBaseAddress = 1;
            mDiscreteInputsBaseAddress = 10001;
            mUnsigned = false;
        }

        #region properties

        static public int HoldingRegistersBaseAddress
        {
            get
            {
                return mHoldingRegistersBaseAddress;
            }
            set
            {
                mHoldingRegistersBaseAddress = value;
            }
        }

        static public int InputRegistersBaseAddress
        {
            get
            {
                return mInputRegistersBaseAddress;
            }
            set
            {
                mInputRegistersBaseAddress = value;
            }
        }

        static public int CoilsBaseAddress
        {
            get
            {
                return mCoilsBaseAddress;
            }
            set
            {
                mCoilsBaseAddress = value;
            }
        }

        static public int DiscreteInputsBaseAddress
        {
            get
            {
                return mDiscreteInputsBaseAddress;
            }
            set
            {
                mDiscreteInputsBaseAddress = value;
            }
        }

        static public bool Unsigned
        {
            get
            {
                return mUnsigned;
            }
            set
            {
                mUnsigned = value;
            }
        }

        #endregion

        public static string DecodeQuery(byte[] Message, int Length)
        {
            // max decodestr size? write 125reg: 125*7 = 875, and errorstr is appended at the end
            StringBuilder decodestr = new StringBuilder(1100);
            StringBuilder errorstr = new StringBuilder(100);

            // Assumes Length without CRC!

            if (Length < 2)
            {
                return ("DECODE: ERROR: QUERY TOO SHORT!");
            }

            // slave address
            decodestr.Append("Slave=");
            decodestr.Append(Message[0]);
            decodestr.Append(",");

            switch( Message[1])
            {
                case 1:
                    // Read Coils
                    decodestr.Append(" Read coils:");
                    if (Length < 6)
                    {
                        errorstr.Append(" ERROR: QUERY TOO SHORT!");
                    }
                    else
                    {
                        int addr = mCoilsBaseAddress + (Message[2] * 256) + Message[3];
                        int quant = (Message[4] * 256) + Message[5];
                        decodestr.Append(" addr=");
                        decodestr.Append(addr);
                        decodestr.Append(", quant=");
                        decodestr.Append(quant);
                        if (Length > 6)
                        {
                            errorstr.Append(" ERROR: QUERY TOO LONG!");
                        }
                    }
                    break;
                case 2:
                    // Read Discrete Inputs
                    decodestr.Append(" Read discrete inputs:");
                    if (Length < 6)
                    {
                        errorstr.Append("ERROR: QUERY TOO SHORT!");
                    }
                    else
                    {
                        int addr = mDiscreteInputsBaseAddress + (Message[2] * 256) + Message[3];
                        int quant = (Message[4] * 256) + Message[5];
                        decodestr.Append(" addr=");
                        decodestr.Append(addr);
                        decodestr.Append(", quant=");
                        decodestr.Append(quant);
                        if (Length > 6)
                        {
                            errorstr.Append(" ERROR: QUERY TOO LONG!");
                        }
                    }
                    break;
                case 3:
                    // Read Holding Registers
                    decodestr.Append(" Read holding registers:");
                    if (Length < 6)
                    {
                        errorstr.Append(" ERROR: QUERY TOO SHORT!");
                    }
                    else
                    {
                        int addr = mHoldingRegistersBaseAddress + (Message[2] * 256) + Message[3];
                        int quant = (Message[4] * 256) + Message[5];
                        decodestr.Append(" addr=");
                        decodestr.Append(addr);
                        decodestr.Append(", quant=");
                        decodestr.Append(quant);
                        if (Length > 6)
                        {
                            errorstr.Append(" ERROR: QUERY TOO LONG!");
                        }
                    }
                    break;
                case 4:
                    // Read Input Registers
                    decodestr.Append(" Read input registers:");
                    if (Length < 6)
                    {
                        errorstr.Append(" ERROR: QUERY TOO SHORT!");
                    }
                    else
                    {
                        int addr = mInputRegistersBaseAddress + (Message[2] * 256) + Message[3];
                        int quant = (Message[4] * 256) + Message[5];
                        decodestr.Append(" addr=");
                        decodestr.Append(addr);
                        decodestr.Append(", quant=");
                        decodestr.Append(quant);
                        if (Length > 6)
                        {
                            errorstr.Append(" ERROR: QUERY TOO LONG!");
                        }
                    }
                    break;
                case 5:
                    // Write Single Coil
                    decodestr.Append(" Write single coil:");
                    if (Length < 6)
                    {
                        errorstr.Append(" ERROR: QUERY TOO SHORT!");
                    }
                    else
                    {
                        // A value of 0xFF00 requests the coil to be ON.
                        // A value of 0x0000 requests the coil to be off. 
                        // All other values are illegal and will not affect the coil. 
                        int addr = mCoilsBaseAddress + (Message[2] * 256) + Message[3];
                        decodestr.Append(" addr=");
                        decodestr.Append(addr);
                        int value = (Message[4] * 256) + Message[5];
                        string valuestr="";
                        if (value == 0x0000) valuestr = "OFF";
                        else if (value == 0xFF00) valuestr = "ON";
                        else valuestr = "ERROR: ILLEGAL VALUE!";
                        decodestr.Append(", value=");
                        decodestr.Append(valuestr);
                        if (Length > 6)
                        {
                            errorstr.Append(" ERROR: QUERY TOO LONG!");
                        }
                    }
                    break;
                case 6:
                    // Write Single Register
                    decodestr.Append(" Write single register:");
                    if (Length < 6)
                    {
                        errorstr.Append(" ERROR: QUERY TOO SHORT!");
                    }
                    else
                    {
                        int addr = mHoldingRegistersBaseAddress + (Message[2] * 256) + Message[3];
                        string valuestr = bytestovalue( Message[4], Message[5]);
                        decodestr.Append(" addr=");
                        decodestr.Append(addr);
                        decodestr.Append(", value=");
                        decodestr.Append(valuestr);
                        if (Length > 6)
                        {
                            errorstr.Append(" ERROR: QUERY TOO LONG!");
                        }
                    }
                    break;
                case 15:
                    // Write Multiple Coils
                    decodestr.Append(" Write multiple coils:");
                    if (Length < 7)
                    {
                        errorstr.Append(" ERROR: QUERY TOO SHORT!");
                    }
                    else
                    {
                        int addr = mCoilsBaseAddress + (Message[2] * 256) + Message[3];
                        int quant = (Message[4] * 256) + Message[5];
                        int bytecount = Message[6];
                        if (bytecount != ((quant + 7) / 8))
                        {
                            errorstr.Append(" ERROR: BYTECOUNT DOES NOT MATCH QUANTITY!");
                        }
                        decodestr.Append(" addr=");
                        decodestr.Append(addr);
                        decodestr.Append(", quant=");
                        decodestr.Append(quant);
                        if (Length < (7+bytecount))
                        {
                            errorstr.Append(" ERROR: QUERY TOO SHORT!");
                        }
                        else if (Length > (7 + bytecount))
                        {
                            errorstr.Append(" ERROR: QUERY TOO LONG!");
                        }
                    }
                    break;
                case 16:
                    // Write Multiple registers
                    decodestr.Append(" Write multiple registers:");
                    if (Length < 7)
                    {
                        errorstr.Append(" ERROR: QUERY TOO SHORT!");
                    }
                    else
                    {
                        int addr = mHoldingRegistersBaseAddress + (Message[2] * 256) + Message[3];
                        int quant = (Message[4] * 256) + Message[5];
                        int bytecount = Message[6];
                        if (bytecount != (quant*2))
                        {
                            errorstr.Append(" ERROR: BYTECOUNT DOES NOT MATCH QUANTITY!");
                        }
                        decodestr.Append(" addr=");
                        decodestr.Append(addr);
                        decodestr.Append(", quant=");
                        decodestr.Append(quant);
                        if (Length < (7 + bytecount))
                        {
                            errorstr.Append(" ERROR: QUERY TOO SHORT!");
                        }
                        else
                        {
                            decodestr.Append(", values=");
                            int Index = 7;
                            string valuestr;
                            while ((Index - 7) < bytecount)
                            {
                                if (Index > 7) decodestr.Append(",");
                                valuestr = bytestovalue(Message[Index], Message[Index+1]);
                                Index += 2;
                                decodestr.Append(valuestr);
                            }
                            if (Length > (7 + bytecount))
                            {
                                errorstr.Append(" ERROR: QUERY TOO LONG!");
                            }
                        }
                    }
                    break;

                default:
                    // not decoded
                    decodestr.Append("(not decoded)");
                    break;
            }

            // append errorstring
            decodestr.Append(errorstr);

            return decodestr.ToString();
        }

        public static string DecodeResponse(byte[] Message, int Length)
        {
            // max decodestr size? read 125reg: 125*7 = 875, and errorstr is appended at the end
            StringBuilder decodestr = new StringBuilder(1100);
            StringBuilder errorstr = new StringBuilder(100);

            // Assumes Length without CRC!

            if (Length < 3)
            {
                return ("DECODE: ERROR: RESPONSE TOO SHORT!");
            }

            // slave address
            decodestr.Append("Slave=");
            decodestr.Append(Message[0]);
            decodestr.Append(",");

            // decode without exception bit
            switch (Message[1] & 0x7F)
            {
                case 1:
                    // Read Coils
                    decodestr.Append(" Read coils:");
                    // exception?
                    if ((Message[1] & 0x80) == 0x80)
                    {
                        errorstr.Append(" Exception: ");
                        errorstr.Append(GetExceptionString(Message[2]));
                        if (Length > 3)
                        {
                            errorstr.Append( " ERROR: RESPONSE TOO LONG!");
                        }
                    }
                    else
                    {
                        int bytecount = Message[2];
                        decodestr.Append(" bytecount=");
                        decodestr.Append(bytecount);
                        if (Length < (3 + bytecount))
                        {
                            errorstr.Append( " ERROR: RESPONSE HAS LESS DATABYTES THEN BYTECOUNT!");
                        }
                        else if (Length > (3 + bytecount))
                        {
                            errorstr.Append( " ERROR: RESPONSE HAS MORE DATABYTES THEN BYTECOUNT!");
                        }
                    }
                    break;
                case 2:
                    // Read Discrete Inputs
                    decodestr.Append(" Read discrete inputs:");
                    // exception?
                    if ((Message[1] & 0x80) == 0x80)
                    {
                        errorstr.Append(" Exception: ");
                        errorstr.Append(GetExceptionString(Message[2]));
                        if (Length > 3)
                        {
                            errorstr.Append( " ERROR: RESPONSE TOO LONG!");
                        }
                    }
                    else
                    {
                        int bytecount = Message[2];
                        decodestr.Append( " bytecount=" + bytecount);
                        if (Length < (3 + bytecount))
                        {
                            errorstr.Append( " ERROR: RESPONSE HAS LESS DATABYTES THEN BYTECOUNT!");
                        }
                        else if (Length > (3 + bytecount))
                        {
                            errorstr.Append( " ERROR: RESPONSE HAS MORE DATABYTES THEN BYTECOUNT!");
                        }
                    }
                    break;
                case 3:
                    // Read Holding Registers
                    decodestr.Append(" Read holding registers:");
                    // exception?
                    if ((Message[1] & 0x80) == 0x80)
                    {
                        errorstr.Append(" Exception: ");
                        errorstr.Append(GetExceptionString(Message[2]));
                        if (Length > 3)
                        {
                            errorstr.Append( " ERROR: RESPONSE TOO LONG!");
                        }
                    }
                    else
                    {
                        int bytecount = Message[2];
                        if ((bytecount & 1) == 1)
                        {
                            errorstr.Append( " ERROR: BYTECOUNT IS NOT EVEN!");
                        }
                        if (Length < (3 + bytecount))
                        {
                            errorstr.Append( " ERROR: RESPONSE HAS LESS DATABYTES THEN BYTECOUNT!");
                        }
                        else
                        {
                            decodestr.Append( " values=");
                            int Index = 3;
                            while ((Index - 3) < bytecount)
                            {
                                if (Index > 3) decodestr.Append( ",");
                                decodestr.Append( bytestovalue(Message[Index], Message[Index + 1]));
                                Index += 2;
                            }
                            if (Length > (3 + bytecount))
                            {
                                errorstr.Append( " ERROR: RESPONSE HAS MORE DATABYTES THEN BYTECOUNT!");
                            }
                        }
                    }
                    break;
                case 4:
                    // Read Input Registers
                    decodestr.Append(" Read input registers:");
                    // exception?
                    if ((Message[1] & 0x80) == 0x80)
                    {
                        errorstr.Append(" Exception: ");
                        errorstr.Append(GetExceptionString(Message[2]));
                        if (Length > 3)
                        {
                            errorstr.Append( " ERROR: RESPONSE TOO LONG!");
                        }
                    }
                    else
                    {
                        int bytecount = Message[2];
                        if ((bytecount & 1) == 1)
                        {
                            errorstr.Append( " ERROR: BYTECOUNT IS NOT EVEN!");
                        }
                        if (Length < (3 + bytecount))
                        {
                            errorstr.Append( " ERROR: RESPONSE HAS LESS DATABYTES THEN BYTECOUNT!");
                        }
                        else
                        {
                            decodestr.Append( " values=");
                            int Index = 3;
                            while ((Index - 3) < bytecount)
                            {
                                if (Index > 3) decodestr.Append( ",");
                                decodestr.Append( bytestovalue(Message[Index], Message[Index + 1]));
                                Index += 2;
                            }
                            if (Length > (3 + bytecount))
                            {
                                errorstr.Append( " ERROR: RESPONSE HAS MORE DATABYTES THEN BYTECOUNT!");
                            }
                        }
                    }
                    break;
                case 5:
                    // Write Single Coil
                    decodestr.Append(" Write single coil:");
                    // exception?
                    if ((Message[1] & 0x80) == 0x80)
                    {
                        errorstr.Append(" Exception: ");
                        errorstr.Append(GetExceptionString(Message[2]));
                        if (Length > 3)
                        {
                            errorstr.Append( " ERROR: RESPONSE TOO LONG!");
                        }
                    }
                    else
                    {
                        if (Length < 6)
                        {
                            errorstr.Append( " ERROR: RESPONSE TOO SHORT!");
                        }
                        else
                        {
                            // A value of 0xFF00 requests the coil to be ON.
                            // A value of 0x0000 requests the coil to be off. 
                            // All other values are illegal and will not affect the coil. 
                            int addr = mCoilsBaseAddress + (Message[2] * 256) + Message[3];
                            decodestr.Append(" addr=");
                            decodestr.Append(addr);
                            int value = (Message[4] * 256) + Message[5];
                            string valuestr="";
                            if (value == 0x0000) valuestr = "OFF";
                            else if (value == 0xFF00) valuestr = "ON";
                            else valuestr = "ERROR: ILLEGAL VALUE!";
                            decodestr.Append(", value=");
                            decodestr.Append(valuestr);
                            if (Length > 6)
                            {
                                errorstr.Append( " ERROR: RESPONSE TOO LONG!");
                            }
                        }
                    }
                    break;
                case 6:
                    // Write Single Register
                    decodestr.Append(" Write single register:");
                    // exception?
                    if ((Message[1] & 0x80) == 0x80)
                    {
                        errorstr.Append(" Exception: ");
                        errorstr.Append(GetExceptionString(Message[2]));
                        if (Length > 3)
                        {
                            errorstr.Append( " ERROR: RESPONSE TOO LONG!");
                        }
                    }
                    else
                    {
                        if (Length < 6)
                        {
                            errorstr.Append( " ERROR: RESPONSE TOO SHORT!");
                        }
                        else
                        {
                            int addr = mHoldingRegistersBaseAddress + (Message[2] * 256) + Message[3];
                            decodestr.Append( " addr=");
                            decodestr.Append( addr);
                            decodestr.Append( ", value=");
                            decodestr.Append( bytestovalue(Message[4], Message[5]));
                            if (Length > 6)
                            {
                                errorstr.Append( " ERROR: RESPONSE TOO LONG!");
                            }
                        }
                    }
                    break;
                case 15:
                    // Write Multiple Coils
                    decodestr.Append(" Write multiple coils:");
                    // exception?
                    if ((Message[1] & 0x80) == 0x80)
                    {
                        errorstr.Append(" Exception: ");
                        errorstr.Append(GetExceptionString(Message[2]));
                        if (Length > 3)
                        {
                            errorstr.Append( " ERROR: RESPONSE TOO LONG!");
                        }
                    }
                    else
                    {
                        if (Length < 6)
                        {
                            errorstr.Append( " ERROR: RESPONSE TOO SHORT!");
                        }
                        else
                        {
                            int addr = mCoilsBaseAddress + (Message[2] * 256) + Message[3];
                            int quant = (Message[4] * 256) + Message[5];
                            decodestr.Append( " addr=");
                            decodestr.Append( addr);
                            decodestr.Append( ", quant=");
                            decodestr.Append( quant);
                            if (Length > 6)
                            {
                                errorstr.Append( " ERROR: RESPONSE TOO LONG!");
                            }
                        }
                    }
                    break;
                case 16:
                    // Write Multiple registers
                    decodestr.Append(" Write multiple registers:");
                    // exception?
                    if ((Message[1] & 0x80) == 0x80)
                    {
                        errorstr.Append(" Exception: ");
                        errorstr.Append(GetExceptionString(Message[2]));
                        if (Length > 3)
                        {
                            errorstr.Append( " ERROR: RESPONSE TOO LONG!");
                        }
                    }
                    else
                    {
                        if (Length < 6)
                        {
                            errorstr.Append( " ERROR: RESPONSE TOO SHORT!");
                        }
                        else
                        {
                            int addr = mHoldingRegistersBaseAddress + (Message[2] * 256) + Message[3];
                            int quant = (Message[4] * 256) + Message[5];
                            decodestr.Append( " addr=");
                            decodestr.Append( addr);
                            decodestr.Append( ", quant=");
                            decodestr.Append( quant);
                            if (Length > 6)
                            {
                                errorstr.Append( " ERROR: RESPONSE TOO LONG!");
                            }
                        }
                    }
                    break;

                default:
                    // not decoded
                    decodestr.Append("(not decoded)");
                    break;
            }

            // append errorstring
            decodestr.Append(errorstr);

            return decodestr.ToString();
        }

        private static string bytestovalue(byte hi, byte lo)
        {
            string result;

            if (mUnsigned)
            {
                // unsigned
                UInt16 value = (UInt16)((hi * 256) + lo);
                result = value.ToString();
            }
            else
            {
                // signed
                Int16 value = (Int16)((hi * 256) + lo);
                result = value.ToString();
            }

            return result;
        }

        private static string GetExceptionString(int ExceptionCode)
        {
            string ReturnValue;

            switch (ExceptionCode)
            {
                case 1: ReturnValue = "EXCEPTION 1 (ILLEGAL FUNCTION)";
                    break;
                case 2: ReturnValue = "EXCEPTION 2 (ILLEGAL ADDRESS)";
                    break;
                case 3: ReturnValue = "EXCEPTION 3 (ILLEGAL DATA VALUE)";
                    break;
                case 4: ReturnValue = "EXCEPTION 4 (SLAVE DEVICE FAILURE)";
                    break;
                case 5: ReturnValue = "EXCEPTION 5 (ACKNOWLEDGE FOR LONG DURATION)";
                    break;
                case 6: ReturnValue = "EXCEPTION 6 (SLAVE DEVICE BUSY)";
                    break;
                case 7: ReturnValue = "EXCEPTION 7 (NEGATIVE ACKNOWLEDGEMENT)";
                    break;
                case 8: ReturnValue = "EXCEPTION 8 (MEMORY PARITY ERROR)";
                    break;
                case 10: ReturnValue = "EXCEPTION 10 (GATEWAY PATH UNAVAILABLE)";
                    break;
                case 11: ReturnValue = "EXCEPTION 11 (GATEWAY TARGET DEVICE FAILED TO RESPOND)";
                    break;
                default: ReturnValue = "EXCEPTION " + ExceptionCode;
                    break;
            }

            return ReturnValue;
        }
    }
}
