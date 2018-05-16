/*
 * ValueToString
 * Convert a binary value into string. 
 */

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Text;


//TODO: CValueToString en CStringToValue in 1 class?

namespace LST.Helpers
{
    public static class CValueToString
    {
        public static bool Ascii16BigEndian;

        /// <summary>
        /// Convert value to string, with a given Type
        /// Value can be less than 32 bits, given by Size. Use sign-extend if needed
        /// </summary>
        /// <param name="Value">Value to convert</param>
        /// <param name="Size">Width of value in bits</param>
        /// <param name="Type">Int, Uint, Hex, ...</param>
        /// <param name="Decimals">number of decimals, for Int/UInt only</param>
        /// <param name="MinLen">minimum number of digits</param>
        /// <returns></returns>
        public static string ValueToString(Int32 Value, int Size, ModbusControlSpace.Global.eFormat Format, int Decimals, int MinLen)
        {
            string Result = string.Empty;
            UInt32 UValue;

            // check
            if (!((Size >= 1) && (Size <= 32)))
            {
                throw new ArgumentException("Invalid size!", "Size");
            }

            // convert
            switch (Format)
            {
                case ModbusControlSpace.Global.eFormat.Int:
                    // signed decimal
                    Value = ConvertSignedValueTo32bit(Value, Size);
                    Result = CValueToString.AsInt(Value, Decimals, MinLen);
                    break;
                case ModbusControlSpace.Global.eFormat.UInt:
                    // unsigned decimal
                    UValue = ConvertUnsignedValueTo32bit((UInt32)Value, Size);
                    Result = CValueToString.AsUInt(UValue, Decimals, MinLen);
                    break;
                case ModbusControlSpace.Global.eFormat.Hex:
                    // hexadecimal
                    UValue = ConvertUnsignedValueTo32bit((UInt32)Value, Size);
                    Result = CValueToString.AsHex(UValue, MinLen);
                    break;
                case ModbusControlSpace.Global.eFormat.Bin:
                    // binary
                    UValue = ConvertUnsignedValueTo32bit((UInt32)Value, Size);
                    Result = CValueToString.AsBin(UValue, MinLen);
                    break;
                case ModbusControlSpace.Global.eFormat.Float:
                    // float32
                    if (!(Size == 32))
                    {
                        throw new ArgumentException("Invalid size!", "Size");
                    }
                    Result = CValueToString.AsFloat(Value);
                    break;
                case ModbusControlSpace.Global.eFormat.ASCII:
                    Result = CValueToString.AsASCII((UInt32)Value, Size);
	                break;
                default:
                    // invalid
                    throw new ArgumentException("Invalid format enum!", "Type");
                    //break;
            }

            return Result;
        }

        /// <summary>
        /// Convert a signed value with Width=1..32 to a 32-bit value
        /// </summary>
        /// <param name="Value"></param>
        /// <param name="Size"></param>
        /// <returns></returns>
        private static Int32 ConvertSignedValueTo32bit(Int32 Value, int Width)
        {
            // if 2 bits or more: convert any n-bit value to a signed 32-bit value
            if (Width >= 2)
            {
                // do a sign-extend
                Value <<= (32 - Width);
                Value >>= (32 - Width);
            }

            return Value;
        }

        /// <summary>
        /// Convert an unsigned value with Width=1..32 to a 32-bit value
        /// </summary>
        /// <param name="Value"></param>
        /// <param name="Size"></param>
        /// <returns></returns>
        private static UInt32 ConvertUnsignedValueTo32bit(UInt32 Value, int Width)
        {
            // add zero's to the left
            Value <<= (32 - Width);
            Value >>= (32 - Width);
            return Value;
        }

        public static string AsInt(Int32 Value, int Decimals, int MinLen)
        {
            // signed decimal

            string Result = "";
            string Formatter;

            if (Decimals >= 1)
            {
                // show with decimals
                double dValue = (double)Value / Math.Pow(10.0, Decimals);
                Formatter = String.Format("F{0}", Decimals);
                Result = dValue.ToString(Formatter);
            }
            else
            {
                // show normal, with minimum length
                Formatter = String.Format("D{0}", MinLen);
                Result = Value.ToString(Formatter);
            }

            return Result;
        }

        public static string AsUInt(UInt32 Value, int Decimals, int MinLen)
        {
            // unsigned decimal

            string Result = "";

            if (Decimals >= 1)
            {
                // show with decimals
                double dValue = (double)Value / Math.Pow(10.0, Decimals);
                string Formatter = String.Format("F{0}", Decimals);
                Result = dValue.ToString(Formatter);
            }
            else
            {
                // show normal, with minimum length
                string Formatter = String.Format("D{0}", MinLen);
                Result = Value.ToString(Formatter);
            }

            return Result;
        }
                    
        public static string AsHex(UInt32 Value, int MinLen)
        {
            // hexadecimal

            string Formatter = Formatter = String.Format("X{0}", MinLen);
            return "0x" + Value.ToString(Formatter);
        }

        public static string AsBin(UInt32 Value, int MinLen)
        {
            // binary
            return "0b" + IntToBinaryString(Value, 4, MinLen);
        }

        public static string AsFloat(Int32 Value)
        {
            // float32
            // convert the bytes of the int into a float
            byte[] aTmpBytes = BitConverter.GetBytes((UInt32)Value);
            float TmpFloat = BitConverter.ToSingle(aTmpBytes, 0);
            return TmpFloat.ToString();
        }
            
        public static string AsASCII(UInt32 Value, int Size)
        {
            // ASCII

            // Show as ASCII characters, show 1 character for each 8 bits.
            // The OPEN MODBUS/TCP SPECIFICATION by Andy Swales states: The first character displayed was the UPPER byte and the second character displayed was the LOWER byte. 

			//              			        lowfirst	highfirst
	        //  1-8 bits	1 byte	0x0041		A			A
	        //  9-16 bits	2 bytes	0x4241		AB			BA
	        //  17-24 bits	3 bytes	0x434241	ABC			BAC
	        //  25-32 bits	4 bytes	0x44434241	ABCD		BADC

            string Result;

            // split value into bytes
            byte[] aBytes = SplitValueIntoBytes(Value, Size);

            // swap bytes if needed
            if (Ascii16BigEndian)
            {
                if (aBytes.Length >= 2)
                {
                    byte b = aBytes[0];
                    aBytes[0] = aBytes[1];
                    aBytes[1] = b;
                }
                if (aBytes.Length >= 4)
                {
                    byte b = aBytes[2];
                    aBytes[2] = aBytes[3];
                    aBytes[3] = b;
                }
            }

            // convert to characters
            Result = "\"";
            for (int i = 0; i < aBytes.Length; ++i)
            {
                // if printable ASCII 32-33, 35-126
                if (   ((aBytes[i] >= 32) && (aBytes[i] <= 33))
                    || ((aBytes[i] >= 35) && (aBytes[i] <= 126)))
                {
                    Result += System.Text.Encoding.ASCII.GetString(aBytes, i, 1);
                }
                else 
                {
                    // print as escape sequence
                    //Result += "\\" + aBytes[i].ToString("D3");  // in decimal, 3 digits, is not standard
                    Result += "\\x" + aBytes[i].ToString("X2");  // in hexadecimal, 2 digits, this is standard
                }
            }
            Result += "\"";

            return Result;
        }

        // Split value into bytes, from lower bits to upper bits 
        // Size in bits
        //TODO: inlinen?
        private static byte[] SplitValueIntoBytes(UInt32 Value, int Size)
        {
            //          bHighByteFirst=false         
            // xxxxxxx1         1---            
            // xxxxxx41         A---            
            // xxxxx241         A2--            
            // xxxx4241         AB--            
            // xxx34241         AB3-            
            // xx434241         ABC-            
            // x5434241         ABC5            
            // 45434241         ABCE            

            // 1 byte:  41          
            // 2 bytes: 41 42       
            // 3 bytes: 41 42 43    
            // 4 bytes: 41 42 43 44 

            int NrOfBytes = (Size + 7) / 8;
            byte[] aBytes = new byte[NrOfBytes];

            // split into bytes
            UInt32 UValue = (UInt32)Value;
            int index = 0;
            while (Size > 0)
            {
                aBytes[index++] = (byte)UValue;
                UValue >>= 8;
                Size -= 8;
            }

            return aBytes;
        }

        private static UInt32 GetUnsignedIntValue(Int32 Value, int Size)
        {
            UInt32 ret;

            // out = (in << (32 - n)) >> (32 - n);

            ret = ((UInt32)Value) << (32 - Size);
            ret = ((UInt32)ret) >> (32 - Size);

            return ret;
        }

        private static double GetDoubleValue(UInt32 Value, int Size, int Decimals)
        {
            Value = ((UInt32)Value) << (32 - Size);
            Value = ((UInt32)Value) >> (32 - Size);

            double dValue = Value;

            dValue = dValue / Math.Pow(10.0, Decimals);

            return (dValue);
        }

        private static string IntToBinaryString(UInt32 n, int grouplen, int Minlen)
        {
            string result = "";
            int grouplencount = 0;
            int totallen = 0;

            do
            {
                if (grouplencount >= grouplen)
                {
                    result = '.' + result;
                    grouplencount = 0;
                }
                result = ((n % 2 == 0) ? "0" : "1") + result;
                n = n / 2;
                ++grouplencount;
                ++totallen;
            } while ((n != 0) || (totallen < Minlen));

            return result;
        }

    }
}
