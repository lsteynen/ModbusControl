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

using ModbusControlSpace;

namespace LST.Helpers
{
    public static class Endians
    {
        public static bool Int32BigEndian;
        public static bool Float32BigEndian;

        /// <summary>
        /// Converteer 1 of 2 16-bit modbus registers naar binaire value (of float of ascii)
        /// </summary>
        public static Int32 ConvertShortsToValue(Int16[] Shorts, Global.eType Type, int Width)
        {
            Int32 Value = 0;

            switch (Type)
            {
                case Global.eType.Int: // swap 16-bit words
                    if (Width == 32)
                    {
                        Value = ToInt32(Shorts[0], Shorts[1], Int32BigEndian);
                    }
                    else
                    {
                        Value = Shorts[0];
                    }
                    break;
                case Global.eType.Float: // swap 16-bit words
                    Value = ToInt32(Shorts[0], Shorts[1], Float32BigEndian);
                    break;
                case Global.eType.ASCII: // no swap
                    if (Width == 32)
                    {
                        Value = ToInt32(Shorts[0], Shorts[1], false);
                    }
                    else
                    {
                        Value = Shorts[0];
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

        /// <summary>
        /// Converteer binaire value (of float of ascii) naar 1 of 2 16-bit modbus registers
        /// </summary>
        public static Int16[] ConvertValueToShorts(Int32 Value, Global.eType Type, int Size)
        {
            // Int: size 1: return 1 short
            //      size 2: return 2 shorts, afh van Int32BigEndian
            // Float: moet size 2 zijn, return 2 shorts, afh van Float32BigEndian
            // ASCII: endian is op byte level

            Int16[] result = new Int16[Size];

            switch (Type)
            {
                case Global.eType.Int:
                    if (Size >= 2)
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
                case Global.eType.Float:
                    if (Size == 2)
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
                case Global.eType.ASCII: // no swap
                    if (Size == 2)
                    {
                        result[1] = (Int16)(((UInt32)Value) >> 16);
                        result[0] = (Int16)(UInt16)Value;
                    }
                    else
                    {
                        result[0] = (Int16)(UInt16)Value;
                    }
                    break;

                default:
                    throw new ArgumentException();
            }

            return result;
        }

    }
}
