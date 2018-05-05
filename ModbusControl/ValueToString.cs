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

namespace LST.Helpers
{
    public static class ValueToStringClass
    {
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
        public static string ValueToString(Int32 Value, int Size, ModbusControlSpace.Global.eFormat Format, int Decimals, int MinLen, bool Ascii16BigEndian)
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
                    Result = ValueToStringClass.AsInt(Value, Decimals, MinLen);
                    break;
                case ModbusControlSpace.Global.eFormat.UInt:
                    // unsigned decimal
                    UValue = ConvertUnsignedValueTo32bit((UInt32)Value, Size);
                    Result = ValueToStringClass.AsUInt(UValue, Decimals, MinLen);
                    break;
                case ModbusControlSpace.Global.eFormat.Hex:
                    // hexadecimal
                    UValue = ConvertUnsignedValueTo32bit((UInt32)Value, Size);
                    Result = ValueToStringClass.AsHex(UValue, MinLen);
                    break;
                case ModbusControlSpace.Global.eFormat.Bin:
                    // binary
                    UValue = ConvertUnsignedValueTo32bit((UInt32)Value, Size);
                    Result = ValueToStringClass.AsBin(UValue, MinLen);
                    break;
                case ModbusControlSpace.Global.eFormat.Float:
                    // float32
                    if (!(Size == 32))
                    {
                        throw new ArgumentException("Invalid size!", "Size");
                    }
                    Result = ValueToStringClass.AsFloat(Value);
                    break;
                case ModbusControlSpace.Global.eFormat.ASCII:
                    Result = ValueToStringClass.AsASCII((UInt32)Value, Size, Ascii16BigEndian);
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
            
        public static string AsASCII(UInt32 Value, int Size, bool bHighByteFirst)
        {
            // ASCII

            // Show as ASCII characters, show 1 character for each 8 bits.
            // We show the 8 lower bits as the first character, the upper bits as the last character.
            // The OPEN MODBUS/TCP SPECIFICATION by Andy Swales states: The first character displayed was the UPPER byte and the second character displayed was the LOWER byte. 
            // But I implemented first character in the lower bits before I found this specification...

            // 1-8 bits: show 1 character A
            // 9-16 bits: show 2 characters AB or BA
            // 17-24 bits: show 3 characters ABC or BAC
            // 25-32 bits: show 4 characters  ABCD or BADC

            // 0x4142 0x4344    -> ABCD if bHighByteFirst=true
            //                  -> BADC if bHighByteFirst=false

            string Result;

            // split value into bytes
            byte[] aBytes = SplitValueIntoBytes(Value, Size, bHighByteFirst);

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
        // if bHighByteFirst=false: we do first character in lower bits
        // if bHighByteFirst=true: first character in upper bits
        // Size in bits
        private static byte[] SplitValueIntoBytes(UInt32 Value, int Size, bool bHighByteFirst)
        {
            //TODO: configurable endian?
            //          bHighByteFirst=false    bHighByteFirst=true     
            // xxxxxxx1         1---                   1
            // xxxxxx41         A---                   A
            // xxxxx241         A2--                   2A
            // xxxx4241         AB--                   BA
            // xxx34241         AB3-                   BA3
            // xx434241         ABC-                   BAC
            // x5434241         ABC5                   BA5C
            // 45434241         ABCE                   BAEC

            // 1 byte:  41              or 41
            // 2 bytes: 41 42           or 42 41
            // 3 bytes: 41 42 43        or 42 41 43
            // 4 bytes: 41 42 43 44     or 42 41 44 43

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

            // if high byte first: swap bytes
            if (bHighByteFirst)
            {
                for( int i=0; (i+1) < NrOfBytes; i += 2)
                {
                    byte b = aBytes[i];
                    aBytes[i] = aBytes[i+1];
                    aBytes[i+1] = b;
                }
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
