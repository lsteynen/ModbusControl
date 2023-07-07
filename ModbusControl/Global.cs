using System;
using System.Collections.Generic;
using System.IO;

namespace ModbusControlSpace
{
    /// <summary>
    /// Global class holds global stuff
    /// </summary>
    public static class Global
    {
        public enum eConnectionType { SerialRTU = 0, ModbusTcp = 1, RtuOverTcp = 2 }
        public enum eTcpConnectMode { Longlived = 0, Shortlived = 1 }
        public enum eWriteHoldingregFunctionCode { FC6, FC16, Auto};
        public enum eEndian { Little, Big };
        public enum eFormat { Int = 0, UInt, Hex, Bin, Float, ASCII };
        public static string[] FormatText = { "int", "uint", "hex", "bin", "float", "ASCII" };
        public enum eType { Int, Float, ASCII };
        public class CEnum
        {
            public Dictionary<int, string> Dict; // enum convert dictionary
            public string ElseText;              // can be null or empty, no ()
        }

        // convert format to type
        public static eType ConvertFormatToType(ModbusControlSpace.Global.eFormat Format)
        {
            eType Type = eType.Int;

            switch (Format)
            {
                case ModbusControlSpace.Global.eFormat.Int:
                case ModbusControlSpace.Global.eFormat.UInt:
                case ModbusControlSpace.Global.eFormat.Hex:
                case ModbusControlSpace.Global.eFormat.Bin:
                    Type = eType.Int;
                    break;
                case ModbusControlSpace.Global.eFormat.Float:
                    Type = eType.Float;
                    break;
                case ModbusControlSpace.Global.eFormat.ASCII:
                    Type = eType.ASCII;
                    break;
                default:
                    // invalid
                    throw new ArgumentException("Invalid format enum!");
            }

            return Type;
        }
    }
}
