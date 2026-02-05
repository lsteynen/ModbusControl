using System;
using System.Collections.Generic;

namespace ModbusControlSpace
{
    /// <summary>
    /// Global class holds global stuff
    /// </summary>
    public static class Global
    {
        public enum eConnectionType { SerialRTU = 0, ModbusTcp = 1, RtuOverTcp = 2 }
        public enum eTcpConnectMode { Longlived = 0, Shortlived = 1 }
        public enum eEndian { Little, Big };
        public enum eFormat { Int = 0, UInt, Hex, Bin, Float, ASCII, String };
        public static string[] FormatText = { "int", "uint", "hex", "bin", "float", "ASCII", "string" };
        public const int StringMaxLength = 300;
        public const int StringMaxRegisters = StringMaxLength / 2;
        public enum eType { Int, Float, ASCII };
        public class CEnum
        {
            public Dictionary<Int64, string> Dict; // enum convert dictionary
            public string ElseText;              // can be null or empty, no ()
        }

    }

    class FileException : ApplicationException
    {
        // thrown in error related to file
        public FileException() { }

        public FileException(string name) : base(String.Format(name))
        {
        }
    }

    class CommException : ApplicationException
    {
        // thrown in error related to communications
        public CommException() { }

        public CommException(string name) : base(String.Format(name))
        {
        }
    }


}
