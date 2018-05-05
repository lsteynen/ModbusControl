/*
 * StringToValue
 * Convert a string into a binary value
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
    public static class CStringToValue
    {
        //TODO: bug in Modbuscontrol v1: in een signed 8bit bitveld in een 32bit register kan je 4294967293 schrijven (is ook -3)
        //      of in een unsigned 4-bit bitveld kan je 4294967295 schrijven, geeft 15
        //TODO: wat gebeurt er in TryEscapedStringToByteArray() met ANSI characters? bvb duitse? ValueToString doet ook alleen ascii. Current ANSI page doen???

        public enum eType { Int, Float, ASCII};

        public static bool Int32BigEndian;
        public static bool Float32BigEndian;
        public static bool Ascii16BigEndian;

        // convert string to Int32
        //TODO: allow -32768 .. +65535 into a 16-bit field, because it fits
        //TODO: allow -128 .. +255 into an 8-bit field, because it fits
        //TODO: allow 0 and 1 into an 1-bit field
        //TODO: store temporary result in Int64 to distinguish between 4294967293 and -3 being parsed
        // converts ascii formats with first character in low byte

        public static bool Convert(string str, bool IsFloat, int Width, int Decimals, out Int32 Result, out eType Type)
        {
            bool IsValid = false;
            Int64 numInt64 = 0;
            UInt32 numUInt32;

            // never return something undefined
            Result = 0;
            Type = eType.Int;

            // is it a hex number?
            if (str.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
            {
                // remove the 0x
                str = str.Substring(2);
                // parse as hex, should fit unsigned 32-bit
                IsValid = UInt32.TryParse(str, NumberStyles.HexNumber, null, out numUInt32);
                // check width
                if (IsValid)
                {
                    IsValid = FitsWidth(numUInt32, Width);
                    Result = (Int32)numUInt32;
                    Type = eType.Int;
                }
            }
            // is it a binary number?
            else if (str.StartsWith("0b", StringComparison.OrdinalIgnoreCase))
            {
                // remove the 0b
                str = str.Substring(2);
                // parse as binary, should fit unsigned 32-bit
                IsValid = BinaryStringToUInt32(str, out numUInt32);
                // check width
                if (IsValid)
                {
                    IsValid = FitsWidth(numUInt32, Width);
                    Result = (Int32)numUInt32;
                    Type = eType.Int;
                }
            }
            // is it an ASCII string? 
            else if (str.StartsWith("\"", StringComparison.OrdinalIgnoreCase) && (IsFloat==false))
            {
                // parse as quoted ASCII string, with escape sequences

                byte[] aBytes;
                IsValid = TryEscapedStringToByteArray(str, out aBytes);
                // check if max 4 bytes, and fits in Width
                if (IsValid)
                {
                    IsValid = (aBytes.Length <= 4) && ((aBytes.Length * 8) <= Width);
                }
                if (IsValid)
                {
                    // make the array 4 bytes
                    Array.Resize(ref aBytes, 4);
                    // convert bytes to UInt32
                    numUInt32 = BitConverter.ToUInt32(aBytes, 0);
                    Result = (Int32)numUInt32;
                    Type = eType.ASCII;
                }
            }
            // is it a float? 
            else if (IsFloat)
            {
                float fValue;
                IsValid = TryParseFloat(str, out fValue);
                // convert the bytes of the float into an Int32
                if (IsValid)
                {
                    byte[] aTmpBytes = BitConverter.GetBytes(fValue);
                    Result = BitConverter.ToInt32(aTmpBytes, 0);
                    Type = eType.Float;
                }
            }
            // it is an int or uint 
            else 
            {
                // with decimals? then parse as float
                if (Decimals >= 1)
                {
                    // get the value as double and scale it up
                    double dValue;
                    IsValid = TryParseDouble(str, out dValue);
                    if( IsValid)
                    {
                        // scale up
                        dValue = dValue * Math.Pow(10.0, Decimals);
                        IsValid = TryDoubleToInt64(dValue, out numInt64);
                    }
                }
                else
                {
                    // parse as decimal int
                    IsValid = Int64.TryParse(str, NumberStyles.Integer, null, out numInt64);
                }
                // check width
                if (IsValid)
                {
                    IsValid = FitsWidth(numInt64, Width);
                    Result = (Int32)numInt64;
                }
                Type = eType.Int;
            }

            return IsValid;
        }

        private static bool BinaryStringToUInt32(string binstr, out UInt32 Value)
        {
            bool bRetvalue = false;
            Value = 0;
            string TmpString = "";

            /* ignore '.' or ' ' separators in the string */
            for (int i = 0; i < binstr.Length; i++)
            {
                if ((binstr[i] == '.') || (binstr[i] == ' '))
                {
                    /* ignore */
                }
                else
                {
                    TmpString += binstr[i];
                }
            }

            /* try convert */
            try
            {
                Value = System.Convert.ToUInt32(TmpString, 2);
                bRetvalue = true;
            }
            catch
            {
            }

            return bRetvalue;
        }

        private static bool TryParseFloat(string s, out float result)
        {
            // try to parse the float with the current culture settings
            // it makes a difference for the numeric keypad decimal separator, might be dot or comma
            bool bIsValid = float.TryParse(s, NumberStyles.Float, null, out result);
            // if that failed, try parsing is culture-independent (dot for decimal separator)
            if (bIsValid == false)
            {
                bIsValid = float.TryParse(s, NumberStyles.Float, CultureInfo.InvariantCulture, out result);
            }

            return bIsValid;
        }

        private static bool TryParseDouble(string s, out double result)
        {
            // try to parse the float with the current culture settings
            // it makes a difference for the numeric keypad decimal separator, might be dot or comma
            bool bIsValid = double.TryParse(s, NumberStyles.Float, null, out result);
            // if that failed, try parsing is culture-independent (dot for decimal separator)
            if (bIsValid == false)
            {
                bIsValid = double.TryParse(s, NumberStyles.Float, CultureInfo.InvariantCulture, out result);
            }

            return bIsValid;
        }

        private static bool TryEscapedStringToByteArray(string s, out byte[] aResult)
        {
            int Index = 0;
            byte[] aData = new byte[s.Length];
            int aDataindex = 0;

            // expects a quoted string
            if (s[Index++] != '\"')
            {
                aResult = new byte[0];
                return false;
            }

            while ((Index < s.Length) && (s[Index] != '\"'))
            {
                char Code = s[Index++];

                // end of quoted string?
                if (Code == '\"')
                {
                    break;
                }
                // only codes 0-255 are valid
                else if ( (Code >= 0) && (Code <= 255))
                {
                    // escape sequence?
                    if( Code == '\\')
                    {
                        // xNN should follow
                        if( ((Index+3) < s.Length) && (s[Index]=='x') && Uri.IsHexDigit(s[Index+1]) && Uri.IsHexDigit(s[Index+2]))
                        {
                            aData[aDataindex++] = (byte)((GetValueOfHexDigit(s[Index+1]) * 16) + GetValueOfHexDigit(s[Index+2]));
                            Index += 3;
                        }
                        else
                        {
                            // error
                            aResult = new byte[0];
                            return false;
                        }
                    }
                    else
                    {
                        aData[aDataindex++] = (byte)Code;
                    }
                }
                else
                {
                    // error
                    aResult = new byte[0];
                    return false;
                }
            }

            // expect end quote?
            if ( ! ((Index < s.Length) && (s[Index++] == '\"')))
            {
                // error
                aResult = new byte[0];
                return false;
            }

            // garbage after the end quote?
            if (Index < s.Length)
            {
                // error
                aResult = new byte[0];
                return false;
            }

            Array.Resize( ref aData, aDataindex);
            aResult = aData;

            return true;
        }

        private static int GetValueOfHexDigit( char c)
        {
            if ((c >= '0') && (c <= '9'))
            {
                return (c - '0');
            }
            else if ((c >= 'A') && (c <= 'F'))
            {
                return (10 + c - 'A');
            }
            else if ((c >= 'a') && (c <= 'f'))
            {
                return (10 + c - 'a');
            }

            return 0;
        }

        private static bool TryDoubleToInt64(double Value, out Int64 Result)
        {
            bool IsValid = false;
            Result = 0;

            // try convert to signed int
            try
            {
                Result = System.Convert.ToInt64(Value);
                IsValid = true;
            }
            catch
            {
            }

            return IsValid;
        }

        /// <summary>
        /// Check if a value fits in Width bits, range Signed.Min .. Unsigned.Max
        /// Value of width==1 must be 0 or 1.
        /// </summary>
        /// <param name="Value">The value</param>
        /// <param name="Width">Width in bits [1-32]</param>
        /// <returns></returns>
        private static bool FitsWidth(Int64 Value, int Width)
        {
            if (Width == 1)
            {
                return ((Value == 0) || (Value == 1));
            }
            else
            {
                Int64 Max = (0xFFFFFFFF >> (32 - Width));
                Int64 Min = -(Max/2) - 1;
                return ((Value >= Min) && (Value <= Max));
            }
        }

        //TODO

        /// <summary>
        /// Converteer binaire value (of float of ascii) naar 1 of 2 16-bit modbus registers
        /// </summary>
        public static Int16[] ConvertValueToModbus(Int32 Value, CStringToValue.eType Type, int Size)
        {
            // Int: size 16: return 1 short
            //      size 32: return 2 shorts, afh van Int32BigEndian
            // Float: moet size 32 zijn, return 2 shorts, afh van Float32BigEndian
            // ASCII: endian is op byte level

            Int16[] result = new Int16[(Size + 15) / 16];

            switch (Type)
            {
                case CStringToValue.eType.Int:
                    if (result.Length >= 2)
                    {
                        if (Int32BigEndian)
                        {
                            result[0] = (Int16)(Value >> 16);
                            result[1] = (Int16)Value;
                        }
                        else
                        {
                            result[0] = (Int16)Value;
                            result[1] = (Int16)(Value >> 16);
                        }
                    }
                    else
                    {
                        result[0] = (Int16)Value;
                    }
                    break;
                case CStringToValue.eType.Float:
                    if (result.Length == 2)
                    {
                        if (Float32BigEndian)
                        {
                            result[0] = (Int16)(Value >> 16);
                            result[1] = (Int16)Value;
                        }
                        else
                        {
                            result[0] = (Int16)Value;
                            result[1] = (Int16)(Value >> 16);
                        }
                    }
                    else
                    {
                        throw new ArgumentException();
                    }
                    break;
                case CStringToValue.eType.ASCII:
                    // Ascii16Endian: determines byte order in a 16-bit word
                    // value holds the string with first char in low byte
                    //         big_endian  little_endian
                    // "A":    4100        0041
                    // "AB":   4142        4241
                    // "ABC":  4142 4300   4241 0043
                    // "ABCD": 4142 4344   4241 4443
                    for (int i = 0; i < result.Length; ++i)
                    {
                        if (Ascii16BigEndian)
                        {
                            result[i] = (Int16)(((Value >> 8) & 0xFF) | ((Value << 8) & 0xFF00));
                        }
                        else
                        {
                            result[i] = (Int16)Value;
                        }
                        Value >>= 16;
                    }
                    break;

                default:
                    throw new ArgumentException();
            }

            return result;
        }

    }
}
