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
    public static class Endians
    {
        public enum eType { Int, Float, ASCII };

        public static bool Int32BigEndian;
        public static bool Float32BigEndian;
        public static bool Ascii16BigEndian;

        /// <summary>
        /// Converteer 1 of 2 16-bit modbus registers naar binaire value (of float of ascii)
        /// </summary>
        public static Int32 ConvertShortsToValue(Int16[] Shorts, eType Type, int Size)
        {
            Int32 Value = 0;

            switch (Type)
            {
                case eType.Int:
                    if (Size == 32)
                    {
                        Value = ToInt32(Shorts[0], Shorts[1], Int32BigEndian);
                    }
                    else
                    {
                        Value = Shorts[0];
                    }
                    break;
                case eType.Float:
                    Value = ToInt32(Shorts[0], Shorts[1], Float32BigEndian);
                    break;
                case eType.ASCII:
                    // Ascii16Endian: determines byte order in a 16-bit word
                    // value holds the string with first char in low byte
                    //  big_endian  little_endian   Value
                    //  4100        0041            "A"
                    //  4142        4241            "AB"
                    //  4142 4300   4241 0043       "ABC"
                    //  4142 4344   4241 4443       "ABCD"
                    if( Ascii16BigEndian)
                        Value = UInt16Swab( (UInt16)Shorts[0]);
                    else
                        Value = (UInt16)Shorts[0];
                    if (Size == 32)
                    {
                        UInt16 Tmp16 = 0;
                        if (Ascii16BigEndian)
                            Tmp16 = UInt16Swab((UInt16)Shorts[1]);
                        else
                            Tmp16 = (UInt16)Shorts[1];
                        Value |= (Tmp16 << 16);
                    }
                    break;
                default:
                    // invalid
                    throw new ArgumentException("Invalid Type enum!", "Type");
                    //break;
            }

            return Value;
        }

        public static Int32 ToInt32(Int16 Value0, Int16 Value1, bool BigEndian)
        {
            if (BigEndian)
                return ((((Int32)Value0) << 16) | (UInt16)Value1);
            else
                return ((((Int32)Value1) << 16) | (UInt16)Value0);
        }

        public static UInt32 ToUInt32(UInt16 Value0, UInt16 Value1)
        {
            return ((((UInt32)Value1) << 16) | (UInt32)Value0);
        }

        public static UInt16 UInt16Swab(UInt16 Value)
        {
            return (UInt16)(((Value << 8) & 0xFF00) | ((Value >> 8) & 0xFF));
        }

        /// <summary>
        /// Converteer binaire value (of float of ascii) naar 1 of 2 16-bit modbus registers
        /// </summary>
        public static Int16[] ConvertValueToShorts(Int32 Value, eType Type, int Size)
        {
            // Int: size 16: return 1 short
            //      size 32: return 2 shorts, afh van Int32BigEndian
            // Float: moet size 32 zijn, return 2 shorts, afh van Float32BigEndian
            // ASCII: endian is op byte level

            Int16[] result = new Int16[(Size + 15) / 16];

            switch (Type)
            {
                case Endians.eType.Int:
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
                case Endians.eType.Float:
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
                case Endians.eType.ASCII:
                    // Ascii16Endian: determines byte order in a 16-bit word
                    // value holds the string with first char in low byte
                    //         big_endian  little_endian
                    // "A":    4100        0041
                    // "AB":   4142        4241
                    // "ABC":  4142 4300   4241 0043
                    // "ABCD": 4142 4344   4241 4443
                    if( Ascii16BigEndian)
                        result[0] = (Int16)UInt16Swab( (UInt16)Value);
                    else
                        result[0] = (Int16)(UInt16)Value;
                    if (result.Length >= 2)
                    {
                        UInt16 Tmp16 = (UInt16)(((UInt32)Value) >> 16);
                        if( Ascii16BigEndian)
                            result[1] = (Int16)UInt16Swab(Tmp16);
                        else
                            result[1] = (Int16)Tmp16;
                    }
                    break;

                default:
                    throw new ArgumentException();
            }

            return result;
        }

    }
}
