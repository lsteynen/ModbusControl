using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModbusControl;

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

        public static void TestByteArraysAreEqual(byte[] ba1, byte[] ba2)
        {
            Assert.AreEqual<int>(ba1.Length, ba2.Length);

            if (ba1.Length > 1)
            {
                for (int i = 0; i < ba1.Length; ++i)
                {
                    Assert.AreEqual<byte>(ba1[i], ba2[i]);
                }
            }
        }
    }
}
