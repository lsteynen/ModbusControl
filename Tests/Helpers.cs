using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModbusControlSpace;

namespace UnitTests
{
    /// <summary>
    /// helpers
    /// </summary>
    public class Helpers
    {
        public static Int16 ConvToInt16(UInt16 Uval)
        {
            Int16 res = 0;
            unchecked
            {
                res = (Int16)Uval;
            }
            return res;
        }

        public static Int32 ConvToInt32(UInt32 Uval)
        {
            Int32 res = 0;
            unchecked
            {
                res = (Int32)Uval;
            }
            return res;
        }
    }
}
