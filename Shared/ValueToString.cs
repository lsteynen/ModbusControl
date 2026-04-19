/*
 * ValueToString
 * Convert a binary value into string. 
 */

using System;
using System.Globalization;
using System.Text;
using ModbusControl;

namespace LST.Helpers
{
    public static class CValueToString
    {
        public static string Int16RegistersToValueString(Int16[] Regs, Global.eFormat Format, Global.eFormat DisplayFormat, int Width, int Decimals = 0, int MinLen = 0, ModbusControl.Global.CEnum EnumDict = null)
        {
            string Result = string.Empty;
            Int32 Int32Val = 0;
            UInt32 UInt32Val = 0;
            Int32 DictSearchValue = 0;  // int32 value used for dictionary search, for example -1 for int16 0xFFFF or 65335 for uint16 0xFFFF

            switch (DisplayFormat)
            {
                case Global.eFormat.Int:
                    // is it a string originally?
                    if (Format == Global.eFormat.String)
                    {
                        // display as list of UInt values
                        StringBuilder sb = new StringBuilder(2000);     // 150 * "-16000, "
                        for (int i = 0; i < Regs.Length; i++)
                        {
                            // insert separator
                            if (i > 0)
                            {
                                sb.Append(", ");
                            }

                            // conv to string, no decimals, no minlen
                            sb.Append(Converts.IntToString(Regs[i]));
                        }
                        Result = sb.ToString();
                    }
                    else
                    {
                        // signed decimal
                        if (Width == 32)
                        {
                            Int32Val = Converts.ToInt32(Regs[0], Regs[1]);
                        }
                        else
                        {
                            Int32Val = Regs[0];
                        }
                        Result = IntValueToDecString(Int32Val, Decimals, MinLen);
                        DictSearchValue = Int32Val;
                    }
                    break;

                case Global.eFormat.UInt:
                    // is it a string originally?
                    if (Format == Global.eFormat.String)
                    {
                        // display as list of UInt values
                        StringBuilder sb = new StringBuilder(2000);     // 150 * "-16000, "
                        for (int i = 0; i < Regs.Length; i++)
                        {
                            // insert separator
                            if (i > 0)
                            {
                                sb.Append(", ");
                            }

                            // conv to string, no decimals, no minlen
                            sb.Append(Converts.UIntToString((UInt16)Regs[i]));
                        }
                        Result = sb.ToString();
                    }
                    else
                    {
                        // unsigned decimal
                        if (Width == 32)
                        {
                            UInt32Val = Converts.ToUInt32(Regs[0], Regs[1]);
                        }
                        else
                        {
                            UInt32Val = (UInt32)(UInt16)Regs[0];
                        }
                        Result = UIntValueToDecString(UInt32Val, Decimals, MinLen);
                        DictSearchValue = (Int32)UInt32Val;
                    }
                    break;

                case Global.eFormat.Hex:
                    // is it a string originally?
                    if (Format == Global.eFormat.String)
                    {
                        // display as list of hex values
                        StringBuilder sb = new StringBuilder(2000);     // 150 * "0x1234, "
                        for (int i = 0; i < Regs.Length; i++)
                        {
                            // insert separator
                            if (i > 0)
                            {
                                sb.Append(", ");
                            }

                            // conv to string, no decimals, 4 digits
                            sb.Append(Converts.IntToStringHex((UInt16)Regs[i], 4));
                        }
                        Result = sb.ToString();
                    }
                    else
                    {
                        // unsigned decimal to hex
                        if (Width == 32)
                        {
                            UInt32Val = Converts.ToUInt32(Regs[0], Regs[1]);
                        }
                        else
                        {
                            UInt32Val = (UInt32)(UInt16)Regs[0];
                        }
                        Result = UIntValueToStringAsHex(UInt32Val, MinLen);
                        DictSearchValue = (Int32)UInt32Val;
                    }
                    break;

                case Global.eFormat.Bin:
                    // binary
                    if (Width == 32)
                    {
                        UInt32Val = Converts.ToUInt32(Regs[0], Regs[1]);
                    }
                    else
                    {
                        UInt32Val = (UInt32)(UInt16)Regs[0];
                    }
                    Result = UIntValueToStringAsBin(UInt32Val, MinLen);
                    DictSearchValue = (Int32)UInt32Val;
                    break;

                case Global.eFormat.Float:
                    // float32
                    if (!(Width == 32))
                    {
                        throw new ArgumentException("Invalid size!", "Width");
                    }
                    // convert the bytes of the int into a float
                    float TmpFloat = Converts.Int16ToFloat32(Regs[0], Regs[1]);
                    Result = TmpFloat.ToString();
                    //Dict not supported for float?
                    EnumDict = null;
                    //DictSearchValue = (Int32)Value;
                    break;

                case Global.eFormat.ASCII:
                    // ASCII
                    if (Width == 32)
                    {
                        UInt32Val = Converts.ToUInt32(Regs[0], Regs[1]);
                        Result = "\"" + Converts.Int16ToAscii(Regs[0]) + Converts.Int16ToAscii(Regs[1]) + "\"";
                    }
                    else
                    {
                        UInt32Val = (UInt32)(UInt16)Regs[0];
                        Result = "\"" + Converts.Int16ToAscii(Regs[0]) + "\"";
                    }
                    DictSearchValue = (Int32)UInt32Val;
                    break;

                case Global.eFormat.String:
                    // string
                    try
                    {
                        Result = "\"" + Converts.Int16ArrayDecodeToString(Regs) + "\"";
                    }
                    catch(Exception e)
                    {
                        Result = "Error: " + e.Message;
                    }                    
                    break;

                default:
                    // invalid
                    throw new ArgumentException("Invalid value!", "Format");
                    //break;
            }

            // enum?
            if (EnumDict != null)
            {
                string Enumstr;
                if ((EnumDict.Dict != null) && EnumDict.Dict.TryGetValue(DictSearchValue, out Enumstr))
                {
                    // found
                    Result += " (" + Enumstr + ")";
                }
                else
                {
                    // not found
                    // show ElseText?
                    if (string.IsNullOrEmpty(EnumDict.ElseText) == false)
                        Result += " (" + EnumDict.ElseText + ")";
                }
            }

            return Result;
        }

        /// <summary>
        /// Convert value to string, with a given Format
        /// Value width can be less than 32 bits, given by Width. 
        /// </summary>
        /// <param name="Value">Value to convert</param>
        /// <param name="Width">Width of value in bits</param>
        /// <param name="Format">Int, Uint, Hex, Bin, ASCII...</param>
        /// <param name="Decimals">Optional: number of decimals, for Int/UInt only</param>
        /// <param name="MinLen">Optional: minimum number of digits</param>
        /// <param name="EnumDict">Optional: enum dictionary</param>
        /// <returns></returns>
        public static string IntValueToValueString(Int32 Value, int Width, Global.eFormat Format, int Decimals = 0, int MinLen = 0, Global.CEnum EnumDict = null)
        {
            string Result = string.Empty;
            UInt32 UValue;
            Int32 DictSearchValue;  // int32 value used for dictionary search, for example -1 for int16 0xFFFF or 65335 for uint16 0xFFFF

            // check
            if (!((Width >= 1) && (Width <= 32)))
            {
                throw new ArgumentException("Invalid size!", "Size");
            }

            // convert
            switch (Format)
            {
                case Global.eFormat.Int:
                    // signed decimal
                    Value = ConvertSignedValueToInt32(Value, Width);
                    Result = IntValueToDecString(Value, Decimals, MinLen);
                    DictSearchValue = Value;
                    break;
                case Global.eFormat.UInt:
                    // unsigned decimal
                    UValue = ConvertUnsignedValueTo32bit((UInt32)Value, Width);
                    Result = UIntValueToDecString(UValue, Decimals, MinLen);
                    DictSearchValue = (Int32)UValue;
                    break;
                case Global.eFormat.Hex:
                    // hexadecimal
                    UValue = ConvertUnsignedValueTo32bit((UInt32)Value, Width);
                    Result = UIntValueToStringAsHex(UValue, MinLen);
                    DictSearchValue = (Int32)UValue;
                    break;
                case Global.eFormat.Bin:
                    // binary
                    UValue = ConvertUnsignedValueTo32bit((UInt32)Value, Width);
                    Result = UIntValueToStringAsBin(UValue, MinLen);
                    DictSearchValue = (Int32)UValue;
                    break;
                case Global.eFormat.ASCII:
                    Result = "\"" + Converts.UInt32ToAscii((UInt32)Value, Width) + "\"";
                    DictSearchValue = (Int32)Value;
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
                if ((EnumDict.Dict != null) && EnumDict.Dict.TryGetValue(DictSearchValue, out Enumstr))
                {
                    // found
                    Result += " (" + Enumstr + ")";
                }
                else
                {
                    // not found
                    // show ElseText?
                    if (string.IsNullOrEmpty(EnumDict.ElseText) == false)
                        Result += " (" + EnumDict.ElseText + ")";
                }
            }

            return Result;
        }

        /// <summary>
        /// Convert 32-bit signed int to decimal string
        /// Supports minimum number of digits, decimals
        /// </summary>
        private static string IntValueToDecString(Int32 Value, int Decimals, int MinLen)
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
        /// Convert 32-bit unsigned int to decimal string
        /// Options: minimum number of digits, decimals
        /// </summary>
        private static string UIntValueToDecString(UInt32 Value, int Decimals, int MinLen)
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
        /// Convert 32-bit unsigned int to binary string, like 0b1000110 
        /// Options: minimum number of digits
        /// </summary>
        private static string UIntValueToStringAsBin(UInt32 Value, int MinLen)
        {
            // binary
            return "0b" + IntToBinaryString(Value, 4, MinLen);
        }

        /// <summary>
        /// Convert 32-bit unsigned int to hex string, like 0x12FA 
        /// Options: minimum number of digits
        /// </summary>
        private static string UIntValueToStringAsHex(UInt32 Value, int MinLen)
        {
            // hexadecimal

            string Formatter = Formatter = String.Format("X{0}", MinLen);
            return "0x" + Value.ToString(Formatter);
        }

        /// <summary>
        /// Convert a signed value with Width=1-32 to a 32-bit value
        /// </summary>
        /// <param name="Value"></param>
        /// <param name="Size"></param>
        /// <returns></returns>
        private static Int32 ConvertSignedValueToInt32(Int32 Value, int Width)
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
        /// Convert an unsigned value with Width=1-32 to a 32-bit value
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
                n /= 2;
                ++grouplencount;
                ++totallen;
            } while ((n != 0) || (totallen < Minlen));

            return result;
        }

        /// <summary>
        /// Convert a value string to modbus int16[size] registers
        /// Accepts decimal Int and UInt, 0x... hex, 0b... binary, float, quoted ASCII and quoted string formats
        /// Accepts Int and UInt with decimals, if allowIntegers and decimals >= 1
        /// Accept -32768 .. +65535 into a 16-bit field
        /// Accept -128 .. +255 into an 8-bit field
        /// Accept 0 and 1 into an 1-bit field
        /// Checks if value fits in Int16[size] registers
        /// Throws ArgumentException on error
        /// </summary>
        public static Int16[] ValueStringToInt16Registers(string str, int size, int decimals, 
                bool allowIntegers, bool allowFloat, bool allowString)
        {
            Int16[] Result = new Int16[size];
            bool IsValid = false;
            Int64 numInt64 = 0;
            UInt32 numUInt32;

            // is it a hex value?
            if (str.StartsWith("0x", StringComparison.OrdinalIgnoreCase) && allowIntegers)
            {
                // remove the 0x
                str = str.Substring(2);
                // parse as hex, should fit unsigned 32-bit
                try
                {
                    numUInt32 = UInt32.Parse(str, NumberStyles.HexNumber, null);
                }
                catch
                {
                    throw new ArgumentException("Invalid value");
                }
                // to Int16[]
                if (size == 2)
                {
                    Result = Converts.UInt32ToInt16Array(numUInt32);
                }
                else if (size == 1)
                {
                    // check width
                    if (!IntFitsWidth(numUInt32, 16))
                    {
                        throw new ArgumentException("Value too large");
                    }
                    Result[0] = (Int16)numUInt32;
                }
                else
                {
                    // invalid
                    throw new ArgumentException("Invalid size");
                }
            }
            // is it a binary value?
            else if (str.StartsWith("0b", StringComparison.OrdinalIgnoreCase) && allowIntegers)
            {
                // remove the 0b
                str = str.Substring(2);
                // parse as binary, should fit unsigned 32-bit
                IsValid = BinaryStringToUInt32(str, out numUInt32);
                if (!IsValid)
                {
                    throw new ArgumentException("Invalid value");
                }
                // to Int16[]
                if (size == 2)
                {
                    Result = Converts.UInt32ToInt16Array(numUInt32);
                }
                else if (size == 1)
                {
                    // check width
                    if (!IntFitsWidth(numUInt32, 16))
                    {
                        throw new ArgumentException("Value too large");
                    }
                    Result[0] = (Int16)numUInt32;
                }
                else
                {
                    // invalid
                    throw new ArgumentException("Invalid size");
                }
            }
            // is it a string? 
            else if (str.StartsWith("\"", StringComparison.OrdinalIgnoreCase) && (allowIntegers || allowString))
            {
                str = UnquoteStr(str);
                if (allowString)
                {
                    // parse for encoded string
                    // throws exception if number of bytes > size
                    // throws exception if string has characters that are not in encoding set
                    Result = Converts.StringEncodeToInt16Array(str, size);
                }
                else
                {
                    // parse for ASCII format, width 16/3, quoted ASCII string, with escape sequences
                    Result = Converts.AsciiStringToInt16Array(str, size);
                }
            }
            // is it a float? 
            else if (allowFloat)
            {
                float fValue;
                IsValid = TryParseFloat(str, out fValue);
                // convert the bytes of the float into an Int32
                if (!IsValid)
                {
                    // invalid
                    throw new ArgumentException("Invalid value");
                }
                // to Int16[]
                if (size == 2)
                {
                    Result = Converts.Float32ToInt16Array(fValue);
                }
                else
                {
                    // invalid
                    throw new ArgumentException("Invalid size");
                }
            }
            // assume decimal int/uint valu, not allowed for string
            else if (allowIntegers)
            {
                // Do not accept 4294967293 in a 8-bit field (was accepted in earlier version it is also -3)
                // Store temporary result in Int64 to distinguish between 4294967293 and -3 being parsed
                
                // with decimals? then parse as float
                if (decimals >= 1)
                {
                    // get the value as double and scale it up
                    double dValue = ParseDouble(str);
                    // scale up
                    dValue *= Math.Pow(10.0, decimals);
                    // to int
                    numInt64 = System.Convert.ToInt64(dValue);
                }
                else
                {
                    // parse as int
                    try
                    {
                        numInt64 = Int64.Parse(str, NumberStyles.Integer, null);
                    }
                    catch
                    {
                        throw new ArgumentException("Invalid value");
                    }
                }
                // check width <= 32
                if (!IntFitsWidth(numInt64, 32))
                {
                    throw new ArgumentException("Invalid value");
                }
                // to Int16[]
                if (size == 2)
                {
                    Result = Converts.Int32ToInt16Array((Int32)numInt64);
                }
                else if (size == 1)
                {
                    // check width
                    if (!IntFitsWidth(numInt64, 16))
                    {
                        throw new ArgumentException("Value too large");
                    }
                    Result[0] = (Int16)numInt64;
                }
                else
                {
                    // invalid
                    throw new ArgumentException("Invalid size");
                }
            }
            else
            {
                throw new ArgumentException("Invalid value");
            }

            return Result;
        }

        /// <summary>
        /// Convert a string to a value, returned as Int32
        /// Accepts Int, UInt, 0x... hex, 0b... binary
        /// Accepts Int and UInt with decimals, if decimals >= 1
        /// DOes not accept quoted ASCII string and float formats
        /// Accept -32768 - +65535 into a 16-bit field
        /// Accept -128 - +255 into an 8-bit field
        /// Accept 0 and 1 into an 1-bit field
        /// Checks if value fits in Width bits
        /// Throws exception on error
        /// </summary>
        public static Int32 ValueStringToIntValue(string str, int width, int decimals)
        {
            Int32 Result = 0;
            bool IsValid = false;
            Int64 numInt64 = 0;
            UInt32 numUInt32;

            // is it a hex value?
            if (str.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
            {
                // remove the 0x
                str = str.Substring(2);
                // parse as hex, should fit unsigned 32-bit
                try
                {
                    numUInt32 = UInt32.Parse(str, NumberStyles.HexNumber, null);
                }
                catch
                {
                    throw new ArgumentException("Invalid value");
                }
                // check width
                if (!IntFitsWidth(numUInt32, width))
                {
                    throw new ArgumentException("Value too large");
                }
                Result = (Int32)numUInt32;
            }
            // is it a binary value?
            else if (str.StartsWith("0b", StringComparison.OrdinalIgnoreCase))
            {
                // remove the 0b
                str = str.Substring(2);
                // parse as binary, should fit unsigned 32-bit
                IsValid = BinaryStringToUInt32(str, out numUInt32);
                if (!IsValid)
                {
                    throw new ArgumentException("Invalid value");
                }
                // check width
                if (!IntFitsWidth(numUInt32, width))
                {
                    throw new ArgumentException("Value too large");
                }
                Result = (Int32)numUInt32;
               
            }
            // assume decimal int or uint 
            else
            {
                // Do not accept 4294967293 in a 8-bit field (was accepted in earlier version it is also -3)
                // Store temporary result in Int64 to distinguish between 4294967293 and -3 being parsed
                
                // with decimals? then parse as float
                if (decimals >= 1)
                {
                    // get the value as double and scale it up
                    double dValue = ParseDouble(str);
                    // scale up
                    dValue *= Math.Pow(10.0, decimals);
                    // to int
                    numInt64 = System.Convert.ToInt64(dValue);
                }
                else
                {
                    // parse as int
                    try
                    {
                        numInt64 = Int64.Parse(str, NumberStyles.Integer, null);
                    }
                    catch
                    {
                        throw new ArgumentException("Invalid value");
                    }
                }
                // check width
                if (!IntFitsWidth(numInt64, width))
                {
                    throw new ArgumentException("Value too large");
                }
                Result = (Int32)numInt64;
            }

            return Result;
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
        /// Throws exception on error
        /// </summary>
        private static double ParseDouble(string s)
        {
            double result;

            // try to parse the float with the current culture settings
            // it makes a difference for the numeric keypad decimal separator, might be dot or comma
            bool bIsValid = double.TryParse(s, NumberStyles.Float, null, out result);
            // if that failed, try parsing it culture-independent (dot for decimal separator)
            if (bIsValid == false)
            {
                bIsValid = double.TryParse(s, NumberStyles.Float, CultureInfo.InvariantCulture, out result);
            }

            if (bIsValid == false)
            {
                throw new ArgumentException("Invalid value", "s");
            }

            return result;
        }

        /// <summary>
        /// Check if a value fits in Width bits, range is Signed.Min - Unsigned.Max
        /// If Width==1: Value must be 0 or 1
        /// </summary>
        /// <param name="Value">The value</param>
        /// <param name="Width">Width in bits, 1-32</param>
        /// <returns></returns>
        public static bool IntFitsWidth(Int64 Value, int width)
        {
            // width 1 .. 32
            if ((width < 1) || (width > 32))
            {
                throw new ArgumentException("Invalid value", "width");
            }

            // width = 1: accept 0 and 1
            if (width == 1)
            {
                return ((Value == 0) || (Value == 1));
            }

            UInt32 UMax = (UInt32)((1L << width) - 1);
            Int32 Min = -(Int32)(UMax / 2) - 1;
            return ((Value >= Min) && (Value <= UMax));
        }

        /// <summary>
        /// Clean enum part of value string
        /// expected input: "123", " 123 (text)", " 123 (...", ...
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string CleanValuestrEnumText(string str)
        {
            int i = str.IndexOf('(');
            if (i >= 0)
            {
                str = str.Substring(0, i);
            }

            return str.TrimEnd();
        }

        /// <summary>
        /// Unquote a string, remove begin and ending "
        /// Begin and ending " are required
        /// Throws error if string is not properly quoted
        /// </summary>
        /// <returns></returns>
        public static string UnquoteStr(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                throw new ArgumentException("Invalid value");
            }

            // got begin quote?
            if ((input.Length >= 1) && (input[0] == '"'))
            {
                // OK
            }
            else
            {
                throw new ArgumentException("Invalid value");
            }

            // got end quote?
            int end = input.Length - 1;
            if ((input.Length >= 2) && (input[input.Length-1] == '\"'))
            {
                // OK
            }
            else
            {
                throw new ArgumentException("Invalid value");
            }

            return input.Substring(1, end - 1);
        }
    }
}
