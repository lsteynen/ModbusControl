/*
 * Converts
 * All kinds of conversions like byte[] to Int16[], ...
 */

using System;
using System.Collections.Generic;
using System.Text;

namespace ModbusControlSpace
{
    public static class Converts
    {
        // configure endians, with defaults
        public static Global.eEndian Int32Endian = Global.eEndian.Little;      // word order in 32-bit integer
        public static Global.eEndian Float32Endian = Global.eEndian.Little;    // word order in 32-bit float
        public static Global.eEndian StringEndian = Global.eEndian.Big;        // big endian means first character is in high byte of Int16 register

        public static Int16 ToInt16(byte byte0, byte byte1)
        {
            // always little endian
            Int16 result = (Int16)((((UInt16)byte1) << 8) | byte0);

            return result;
        }

        public static UInt16 ToUInt16(byte byte0, byte byte1, Global.eEndian endian = Global.eEndian.Little)
        {
            UInt16 result;

            if (endian == Global.eEndian.Little)
            {
                result = (UInt16)((((UInt16)byte1) << 8) | byte0);
            }
            else
            {
                result = (UInt16)((((UInt16)byte0) << 8) | byte1);           
            }

            return result;
        }

        public static Int32 ToInt32(Int16 word0, Int16 word1, Global.eEndian? endian = null)
        {
            Int32 result = 0;

            // use default word order?
            if (endian == null)
            {
                endian = Int32Endian;
            }

            if (endian == Global.eEndian.Big)
            {
                result = (((Int32)(UInt16)word0) << 16) | (UInt16)word1;
            }
            else
            {
                result = (((Int32)(UInt16)word1) << 16) | (UInt16)word0;
            }

            return result;
        }

        public static UInt32 ToUInt32(Int16 word0, Int16 word1, Global.eEndian? endian = null)
        {
            UInt32 result = 0;

            // use default word order?
            if (endian == null)
            {
                endian = Int32Endian;
            }

            if (endian == Global.eEndian.Big)
            {
                result = (((UInt32)(UInt16)word0) << 16) | (UInt32)(UInt16)word1;
            }
            else
            {
                result = (((UInt32)(UInt16)word1) << 16) | (UInt32)(UInt16)word0;
            }
                
           return result;
        }

        public static UInt32 ToUInt32(UInt16 word0, UInt16 word1, Global.eEndian? endian = null)
        {
            UInt32 result = 0;

            // use default word order?
            if (endian == null)
            {
                endian = Int32Endian;
            }

            if (endian == Global.eEndian.Big)
            {
                result = (((UInt32)(UInt16)word0) << 16) | (UInt32)(UInt16)word1;
            }
            else
            {
                result = (((UInt32)(UInt16)word1) << 16) | (UInt32)(UInt16)word0;
            }

            return result;
        }

        public static UInt16[] Int16ArrayToUInt16Array(Int16[] aValues)
        {
            UInt16[] result = new UInt16[aValues.Length];

            for (int i = 0; i < aValues.Length; ++i)
            {
                result[i] = (UInt16)aValues[i];
            }

            return result;
        }

        public static byte[] Int16ArrayToByteArray(Int16[] aValues, Global.eEndian endian = Global.eEndian.Little)
        {
            byte[] result = new byte[aValues.Length * 2];

            if (endian == Global.eEndian.Little)
            {
                // little endian
                for (int i = 0; i < aValues.Length; ++i)
                {
                    result[i * 2] = (byte)aValues[i];
                    result[(i * 2) + 1] = (byte)(((UInt16)aValues[i]) >> 8);
                }
            }
            else
            {
                // big endian
                for (int i = 0; i < aValues.Length; ++i)
                {
                    result[i * 2] = (byte)(((UInt16)aValues[i]) >> 8);
                    result[(i * 2) + 1] = (byte)aValues[i]; 
                }
            }

            return result;
        }

        /// <summary>
        /// Convert 1 byte to ASCII representation
        /// Returns escape sequences for non-printable characters, like "\xFF"
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        private static string ByteToAscii(byte b)
        {
            string result = string.Empty;

            // if printable ASCII 32-33, 35-126
            if (((b >= 32) && (b <= 33))
                || ((b >= 35) && (b <= 126)))
            {
                byte[] ba = new byte[1];
                ba[0] = b;
                result += System.Text.Encoding.ASCII.GetString(ba);
            }
            else
            {
                // print as escape sequence
                //Result += "\\" + aBytes[i].ToString("D3");  // in decimal, 3 digits, is not standard
                result += "\\x" + b.ToString("X2");  // in hexadecimal, 2 digits, this is standard
            }

            return result;
        }

        /// <summary>
        /// parse a quoted ASCII string like "A", "AB", "Xy\x01\xFF"
        /// supports escape sequences
        /// return array of bytes, 1 character per byte
        /// Throws exception on error
        ///  Examples:	            	
        ///  "A"   		    0x41		
        ///  "AB"   		0x41, 0x42 		
        ///  "AB\x00F" 		0x41, 0x42, 0x00, 0x46
        /// </summary>
        private static List<byte> AsciiStringToByteList(string str)
        {
            List<byte> byteList = new List<byte>();
            int i = 0;

            if ((str.Length >= 1) && str[i++] == '\"')
            {
                // OK
            }
            else
            {
                throw new ArgumentException("Invalid value", "str");
            }

            while ((i < str.Length) && (str[i] != '\"'))
            {
                char code = str[i++];

                // end of quoted string?
                if (code == '\"')
                {
                    break;
                }
                // only codes 0-255 are valid
                else if ((code >= 0) && (code <= 255))
                {
                    // escape sequence?
                    if (code == '\\')
                    {
                        // xNN should follow
                        if (((i + 3) < str.Length) && (str[i] == 'x') && Uri.IsHexDigit(str[i + 1]) && Uri.IsHexDigit(str[i + 2]))
                        {
                            byteList.Add((byte)((GetValueOfHexDigit(str[i + 1]) * 16) + GetValueOfHexDigit(str[i + 2])));
                            i += 3;
                        }
                        else
                        {
                            throw new ArgumentException("Invalid value", "str");
                        }
                    }
                    else
                    {
                        byteList.Add((byte)code);
                    }
                }
                else
                {
                    throw new ArgumentException("Invalid value", "str");
                }
            }

            // got end quote?
            if (!((i < str.Length) && (str[i++] == '\"')))
            {
                throw new ArgumentException("Invalid value", "str");
            }

            // garbage after end quote?
            if (i < str.Length)
            {
                throw new ArgumentException("Invalid value", "str");
            }

            return byteList;
        }

        /// <summary>
        /// Convert binary value to ASCII characters, not yet quoted
        /// Does not use StringEndian, always low-to-high 
        /// Needs width to know if we should return 1, 2, 3 or 4 characters
        /// 
        /// Examples:
        ///  0x00000041     "A"   		    		
        ///  0x00004241     "AB"   				
        ///  0x00434241     "ABC"   		
        ///  0x01434241     "ABC\x01" 		
        /// </summary>
        public static string UInt32ToAscii(UInt32 value, int width)
        {
            string result = String.Empty;

            if (width >= 1)
            {
                byte b = (byte)(value & 0xFF);
                value >>= 8;
                result += ByteToAscii(b);
            }
            if (width >= (8+1))
            {
                byte b = (byte)(value & 0xFF);
                value >>= 8;
                result += ByteToAscii(b);
            }
            if (width >= (16+1))
            {
                byte b = (byte)(value & 0xFF);
                value >>= 8;
                result += ByteToAscii(b);
            }
            if (width >= (24+1))
            {
                byte b = (byte)(value & 0xFF);
                value >>= 8;
                result += ByteToAscii(b);
            }

            return result;
        }

        /// <summary>
        /// parse a quoted ASCII string like "Ab", "AB\xFA\xFF", with escape sequences
        /// return UInt32 with 0 to 4 characters, max 4 characters/bytes
        /// Does not use StringEndian, always low-to-high
        /// Throws exception on error
        /// 
        ///  "A"   		    0x00000041   		
        ///  "AB"   		0x00004241   		
        ///  "ABC"   		0x00434241		
        ///  "ABC\x01" 		0x01434241	
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static UInt32 AsciiStringToUInt32(string str)
        {
            UInt32 result = 0;
            List<byte> byteList = AsciiStringToByteList(str);

            if (byteList.Count > 4)
            {
                throw new ArgumentException("Invalid value", "str");
            }

            if (byteList.Count >= 1)
            {
                result = byteList[0];
            }
            if (byteList.Count >= 2)
            {
                result |= ((UInt32)byteList[1]) << 8;
            }
            if (byteList.Count >= 3)
            {
                result |= ((UInt32)byteList[2]) << 16;
            }
            if (byteList.Count >= 4)
            {
                result |= ((UInt32)byteList[3]) << 24;
            }

            return result;
        }

        /// <summary>
        /// Convert binary value to ASCII characters, not yet quoted
        /// Uses StringEndian: high byte is first character if StringEndian==Big, low byte is first character if StringEndian==little
        /// </summary>
        public static string Int16ToAscii(Int16 valInt16)
        {
            // Show as ASCII characters, show 1 character for each 8 bits.
            // 
            // The OPEN MODBUS/TCP SPECIFICATION by Andy Swales states: The first character displayed was the UPPER byte and the second character displayed was the LOWER byte. 
            // So, StringEndian=Big is default
            //
            //  	        highbytefirst   lowbytefirst    
            //  0x4241		"BA"            "AB"            
            //  0x0041		"\x00A"         "A\x00"         
            //  0x4200		"B\x00"         "\x00B"         

            byte byte1 = 0;
            byte byte2 = 0;

            if (StringEndian == Global.eEndian.Big)
            {
                byte1 = (byte)(valInt16 >> 8);
                byte2 = (byte)(valInt16 & 0xFF);
            }
            else
            {
                byte1 = (byte)(valInt16 & 0xFF);
                byte2 = (byte)(valInt16 >> 8);
            }

            return ByteToAscii(byte1) + ByteToAscii(byte2);
        }

        /// <summary>
        /// Convert ASCII string to Int16[] len 1 or 2
        /// Assumes quoted string
        /// Uses StringEndian: high byte is first character if StringEndian==Big, low byte is first character if StringEndian==little
        /// Throws exception on error
        /// </summary>
        /// <param name="str"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static Int16[] AsciiStringToInt16Array(string str, int size)
        {
            Int16[] result = new Int16[size];

            List<byte> byteList = AsciiStringToByteList(str);

            if ((size == 1) || (size == 2))
            {
                // OK
            }
            else
            {
                throw new ArgumentException("Invalid value", "size");
            }

            if (byteList.Count > (size * 2))
            {
                throw new ArgumentException("Invalid value", "str");
            }

            while (byteList.Count < 4)
            {
                byteList.Add(0);
            }

            result[0] = (Int16)ToUInt16(byteList[0], byteList[1], StringEndian);
            if (size >= 2)
            {
                result[1] = (Int16)ToUInt16(byteList[2], byteList[3], StringEndian);
            }

            return result;
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

        public static Int16[] Int32ToInt16Array(Int32 value32, int size = 0)
        {
            // default size 2 is Int16 registers
            if (size == 0)
            {
                size = 2;
            }

            if ((size == 1) || (size == 2))
            {
                // OK
            }
            else
            {
                throw new ArgumentException("Invalid value", "size");
            }

            Int16[] aValues16 = new Int16[size];

            if (size == 2)
            {
                if (Int32Endian == Global.eEndian.Big)
                {
                    aValues16[0] = (Int16)(value32 >> 16);
                    aValues16[1] = (Int16)(value32);
                }
                else
                {
                    aValues16[0] = (Int16)(value32);
                    aValues16[1] = (Int16)(value32 >> 16);
                }
            }
            else
            {
                // in Int16/Uint16 range?
                if ((value32 >= -32768) && (value32 <= 65535))
                {
                    // OK
                }
                else
                {
                    throw new ArgumentException("Invalid value", "value32");
                }

                aValues16[0] = (Int16)(value32);
            }

            return aValues16;
        }

        public static Int16[] UInt32ToInt16Array(UInt32 value32, Global.eEndian? endian = null)
        {
            Int16[] aValues16 = new Int16[2];

            // use default word order?
            if (endian == null)
            {
                endian = Int32Endian;
            }

            if (endian == Global.eEndian.Big)
            {
                aValues16[0] = (Int16)(value32 >> 16);
                aValues16[1] = (Int16)(value32);
            }
            else
            {
                aValues16[0] = (Int16)(value32);
                aValues16[1] = (Int16)(value32 >> 16);
            }

            return aValues16;
         }


         public static string IntToString(Int32 value, int decimals = 0, int minLen = 0)
         {
            string Result = "";
            string Formatter;

            if (decimals >= 1)
            {
                // show with decimals
                double dValue = (double)value / Math.Pow(10.0, decimals);
                Formatter = String.Format("F{0}", decimals);
                Result = dValue.ToString(Formatter);
            }
            else
            {
                // show integer, with minimum length
                Formatter = String.Format("D{0}", minLen);
                Result = value.ToString(Formatter);
            }

            return Result;
        }

        public static string UIntToString(UInt32 value, int decimals = 0, int minLen = 0)
        {
            string Result = "";

            if (decimals >= 1)
            {
                // show with decimals
                double dValue = (double)value / Math.Pow(10.0, decimals);
                string Formatter = String.Format("F{0}", decimals);
                Result = dValue.ToString(Formatter);
            }
            else
            {
                // show normal, with minimum length
                string Formatter = String.Format("D{0}", minLen);
                Result = value.ToString(Formatter);
            }

            return Result;
        }

        public static string IntToStringHex(Int32 value, int minLen = 0)
        {
            string Formatter = Formatter = String.Format("X{0}", minLen);
            return "0x" + value.ToString(Formatter);
        }

        public static float Int16ToFloat32(Int16 word0, Int16 word1)
        {
            byte[] aBytes = new byte[4];

            if (Float32Endian == Global.eEndian.Big)
            {
                aBytes[0] = (byte)((UInt16)word1 & 0xFF);
                aBytes[1] = (byte)((UInt16)word1 >> 8);
                aBytes[2] = (byte)((UInt16)word0 & 0xFF);
                aBytes[3] = (byte)((UInt16)word0 >> 8);
            }
            else
            {
                aBytes[0] = (byte)((UInt16)word0 & 0xFF);
                aBytes[1] = (byte)((UInt16)word0 >> 8);
                aBytes[2] = (byte)((UInt16)word1 & 0xFF);
                aBytes[3] = (byte)((UInt16)word1 >> 8);
            }

            return BitConverter.ToSingle(aBytes, 0);
        }

        public static Int16[] Float32ToInt16Array(float fValue)
        {
            Int16[] result = new Int16[2];
            byte[] aBytes = BitConverter.GetBytes(fValue);

            if (Float32Endian == Global.eEndian.Big)
            {
                result[0] = ToInt16(aBytes[2], aBytes[3]);
                result[1] = ToInt16(aBytes[0], aBytes[1]);
            }
            else
            {
                result[0] = ToInt16(aBytes[0], aBytes[1]);
                result[1] = ToInt16(aBytes[2], aBytes[3]);
            }

            return result;
        }

        public static UInt32 Int16ArrayToUInt32(Int16[] aInt16, Global.eEndian? endian = null)
        {
            // len >= 2?
            if (aInt16.Length < 2)
            {
                throw new ArgumentException("Int16[].Length < 2", "aInt16");
            }

            // use default word order?
            if (endian == null)
            {
                endian = Int32Endian;
            }

            if (endian == Global.eEndian.Big)
                return ((((UInt32)(UInt16)aInt16[0]) << 16) | (UInt32)(UInt16)aInt16[1]);
            else
                return ((((UInt32)(UInt16)aInt16[1]) << 16) | (UInt32)(UInt16)aInt16[0]);
        }

        public static string Int16ArrayToStringOfValues(Int16[] aValues)
        {
            // provide initial room for 200 values of "-32767, "
            StringBuilder sb = new StringBuilder(200 * 8);

            for (int i = 0; i < aValues.Length; ++i)
            {
                if (i >= 1) sb.Append(", ");
                sb.Append(aValues[i]);
            }

            return sb.ToString();
        }

        /// <summary>
        /// Convert array of bytes to string with space-separated hex values.
        /// Optional maxCount to convert less bytes.
        /// </summary>
        public static string ByteArrayToStringOfValues(byte[] aBytes, int maxCount = -1)
        {
            // maxCount <= aBytes.len?
            if (maxCount > aBytes.Length)
            {
                throw new ArgumentException("maxCount > aBytes.Length", "maxCount");
            }

            int convlen = aBytes.Length;
            if (maxCount >= 0)
            {
                convlen = Math.Min(aBytes.Length, maxCount);
            }
            
            StringBuilder sb = new StringBuilder(convlen * 3 + 2);
            for (int i = 0; i < convlen; ++i)
            {
                if (i > 0) sb.Append(", ");
                sb.AppendFormat("0x{0:X2}", aBytes[i]);
            }

            return sb.ToString();
        }

        public static UInt32 Int16ArrayToUInt32Value(Int16[] Regs, int size, ModbusControlSpace.Global.eType type)
        {
            UInt32 Value = 0;

            if ((size == 1) || (size == 2))
            {
                // OK
            }
            else
            {
                throw new ArgumentException("Invalid value", "size");
            }
            
            switch (type)
            {
                case ModbusControlSpace.Global.eType.Int: 
                    if (size == 2)
                    {
                        // convert with Int32 endian
                        Value = Int16ArrayToUInt32(Regs);
                    }
                    else
                    {
                        Value = (UInt32)(UInt16)Regs[0];
                    }
                    break;

                case ModbusControlSpace.Global.eType.ASCII: 
                    if (size == 2)
                    {
                        // this is not a 32-bit value, do not swap 16-bit registers
                        Value = Int16ArrayToUInt32(Regs, Global.eEndian.Little);
                    }
                    else
                    {
                        Value = (UInt32)(UInt16)Regs[0];
                    }
                    break;

                case ModbusControlSpace.Global.eType.Float:
                    // convert with Float2 endian
                    Value = Int16ArrayToUInt32(Regs, Float32Endian);
                    break;

                default:
                    // invalid
                    throw new ArgumentException("Invalid Type enum!", "Type");
                    //break;
            }

            return Value;
            
        }

        /* future feature: string support:        
        public static string BytesToString(byte[] aBytes, Encoding enc)     "Test van ASCII of UTF8 encoded string"
        public static string BytesToString(byte[] aBytes, Encoding enc)     "Test van ASCII of UTF8 encoded string"  
        */
    }
}
