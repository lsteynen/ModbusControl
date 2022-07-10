/*
 * ValueToString
 * Convert a binary value into string. 
 */

using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Text;

using ModbusControlSpace;

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
        /// <param name="Width">Width of value in bits</param>
        /// <param name="Type">Int, Uint, Hex, ...</param>
        /// <param name="Decimals">Optional: number of decimals, for Int/UInt only</param>
        /// <param name="MinLen">Optional: minimum number of digits</param>
        /// <param name="EnumDict">Optional: enum dictionary</param>
        /// <returns></returns>
        public static string ValueToString(Int32 Value, int Width, ModbusControlSpace.Global.eFormat Format, int Decimals = 0, int MinLen = 0, Dictionary<int, string> EnumDict = null)
        {
            string Result = string.Empty;
            UInt32 UValue;

            // check
            if (!((Width >= 1) && (Width <= 32)))
            {
                throw new ArgumentException("Invalid size!", "Size");
            }

            // convert
            switch (Format)
            {
                case ModbusControlSpace.Global.eFormat.Int:
                    // signed decimal
                    Value = ConvertSignedValueTo32bit(Value, Width);
                    Result = CValueToString.AsInt(Value, Decimals, MinLen);
                    break;
                case ModbusControlSpace.Global.eFormat.UInt:
                    // unsigned decimal
                    UValue = ConvertUnsignedValueTo32bit((UInt32)Value, Width);
                    Result = CValueToString.AsUInt(UValue, Decimals, MinLen);
                    break;
                case ModbusControlSpace.Global.eFormat.Hex:
                    // hexadecimal
                    UValue = ConvertUnsignedValueTo32bit((UInt32)Value, Width);
                    Result = CValueToString.AsHex(UValue, MinLen);
                    break;
                case ModbusControlSpace.Global.eFormat.Bin:
                    // binary
                    UValue = ConvertUnsignedValueTo32bit((UInt32)Value, Width);
                    Result = CValueToString.AsBin(UValue, MinLen);
                    break;
                case ModbusControlSpace.Global.eFormat.Float:
                    // float32
                    if (!(Width == 32))
                    {
                        throw new ArgumentException("Invalid size!", "Size");
                    }
                    Result = CValueToString.AsFloat(Value);
                    break;
                case ModbusControlSpace.Global.eFormat.ASCII:
                    Result = CValueToString.AsASCII((UInt32)Value, Width);
	                break;
                default:
                    // invalid
                    throw new ArgumentException("Invalid format enum!", "Type");
                    //break;
            }

            // enum?
            if (EnumDict != null)
            {
                string Enumstr;
                if (EnumDict.TryGetValue(Value, out Enumstr))
                {
                    // found
                    Result += " (" + Enumstr + ")";
                }
                else
                {
                    // not found
                    Result += " (enum?)";
                }
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

        /// <summary>
        /// Convert 32-bit binary value unsigned int representation string
        /// Supports minimum number of decimals
        /// </summary>
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

        /// <summary>
        /// Convert 32-bit binary value unsigned int representation string
        /// Supports minimum number of decimals
        /// </summary>
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

        /// <summary>
        /// Convert 32-bit binary value hex representation string
        /// </summary>
        public static string AsHex(UInt32 Value, int MinLen)
        {
            // hexadecimal

            string Formatter = Formatter = String.Format("X{0}", MinLen);
            return "0x" + Value.ToString(Formatter);
        }

        /// <summary>
        /// Convert 32-bit binary value binary representation string
        /// </summary>
        public static string AsBin(UInt32 Value, int MinLen)
        {
            // binary
            return "0b" + IntToBinaryString(Value, 4, MinLen);
        }

        /// <summary>
        /// Convert 32-bit float value to string
        /// </summary>
        public static string AsFloat(Int32 Value)
        {
            // float32
            // convert the bytes of the int into a float
            byte[] aTmpBytes = BitConverter.GetBytes((UInt32)Value);
            float TmpFloat = BitConverter.ToSingle(aTmpBytes, 0);
            return TmpFloat.ToString();
        }
            
        /// <summary>
        /// Convert binary value to ASCII characters
        /// Low byte is first character if Ascii16BigEndian==false, or second character if Ascii16BigEndian==true
        /// </summary>
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

        /// <summary>
        /// Split value into bytes, from lower bits to upper bits  
        /// </summary>
        /// <param name="Size">Size in bits</param>
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

        /// <summary>
        /// Get value of a part of a binary value
        /// </summary>
        private static UInt32 GetUnsignedIntValue(Int32 Value, int Size)
        {
            UInt32 ret;

            // out = (in << (32 - n)) >> (32 - n);

            ret = ((UInt32)Value) << (32 - Size);
            ret = ((UInt32)ret) >> (32 - Size);

            return ret;
        }

        /// <summary>
        /// Convert binary value to double, with decimals
        /// </summary>
        private static double GetDoubleValue(UInt32 Value, int Size, int Decimals)
        {
            Value = ((UInt32)Value) << (32 - Size);
            Value = ((UInt32)Value) >> (32 - Size);

            double dValue = Value;

            dValue = dValue / Math.Pow(10.0, Decimals);

            return (dValue);
        }

        /// <summary>
        /// Convert binary value to binary representation in string
        /// Supports grouping digits with Grouplen, insert '.'
        /// Supports Minlen to output minimal number of digits
        /// </summary>
        private static string IntToBinaryString(UInt32 n, int Grouplen, int Minlen)
        {
            string result = "";
            int grouplencount = 0;
            int totallen = 0;

            do
            {
                if ((Grouplen > 0) && (grouplencount >= Grouplen))
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

        // convert string to Int32

        /// <summary>
        /// Convert a string to a binary value, returned as Int32
        /// Accepts Int, UInt, 0x hex, 0b binary, quoted ASCII string and float formats
        /// Accepts Int and UInt with decimals, is IsFloat==false and Decimals >= 1
        /// Accept -32768 .. +65535 into a 16-bit field, because it fits
        /// Accept -128 .. +255 into an 8-bit field, because it fits
        /// Accept 0 and 1 into an 1-bit field
        /// Do not accept 4294967293 in a 8-bit field (was accepted in Modbuscontrol v1 because it is also -3)
        /// Store temporary result in Int64 to distinguish between 4294967293 and -3 being parsed
        /// Stores ASCII formats with first character in low byte
        /// Checks if value fits in Width bits
        /// Returns Endians.eType Type to show what type/format whas input
        /// </summary>
        public static bool StringToValue(string str, bool IsFloat, int Width, int Decimals, out Int32 Result, out Global.eType Type)
        {
            bool IsValid = false;
            Int64 numInt64 = 0;
            UInt32 numUInt32;

            // never return something undefined
            Result = 0;
            Type = Global.eType.Int;

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
                    Type = Global.eType.Int;
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
                    Type = Global.eType.Int;
                }
            }
            // is it an ASCII string? 
            else if (str.StartsWith("\"", StringComparison.OrdinalIgnoreCase) && (IsFloat == false))
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
                    // convert bytes to UInt32
                    numUInt32 = BitConverter.ToUInt32(aBytes, 0);
                    Result = (Int32)numUInt32;
                    Type = Global.eType.ASCII;
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
                    Type = Global.eType.Float;
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
                    if (IsValid)
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
                Type = Global.eType.Int;
            }

            return IsValid;
        }

        /// <summary>
        /// Convert string with 0/1 to binary value
        /// Allows '.' and ' ' digit separators
        /// </summary>
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

        /// <summary>
        /// String to float
        /// </summary>
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

        /// <summary>
        /// String to double
        /// </summary>
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

        /// <summary>
        /// ASCII String to byte array
        /// Accepts quoted string
        /// Accepts escape sequence \xHH where H is hex digit
        /// </summary>
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
                else if ((Code >= 0) && (Code <= 255))
                {
                    // escape sequence?
                    if (Code == '\\')
                    {
                        // xNN should follow
                        if (((Index + 3) < s.Length) && (s[Index] == 'x') && Uri.IsHexDigit(s[Index + 1]) && Uri.IsHexDigit(s[Index + 2]))
                        {
                            aData[aDataindex++] = (byte)((GetValueOfHexDigit(s[Index + 1]) * 16) + GetValueOfHexDigit(s[Index + 2]));
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
            if (!((Index < s.Length) && (s[Index++] == '\"')))
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

            Array.Resize(ref aData, aDataindex);
            aResult = aData;

            return true;
        }

        /// <summary>
        /// Get ASCII value of a hex digit
        /// Returns 0 for invalid digits
        /// </summary>
        private static int GetValueOfHexDigit(char c)
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

        /// <summary>
        /// Convert double to 64-bit int
        /// </summary>
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
        /// Check if a value fits in Width bits, range is Signed.Min .. Unsigned.Max
        /// If Width==1: must return 0 or 1
        /// </summary>
        /// <param name="Value">The value</param>
        /// <param name="Width">Width in bits, 1 .. 32</param>
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
                Int64 Min = -(Max / 2) - 1;
                return ((Value >= Min) && (Value <= Max));
            }
        }

    }
}
