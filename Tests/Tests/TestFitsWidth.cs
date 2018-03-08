
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Text;
using NUnit.Framework;

namespace TestProject
{
    [TestFixture]
    public class TestFitsWidth
    {
        public enum eType { Signed, Unsigned, Float };

        static int RanSigned;
        static int RanUnsigned;

        [Test]
        public void Test_Signed_000()
        {
            RanSigned = 0;
            RanUnsigned = 0;
        }

        [Test]
        public void Test_Signed_01_01()
        {
            int w = 1;
            Int32 v;
            v = -2147483647; Assert.False(FitsWidth(v, w));
            v = -2; Assert.False(FitsWidth(v, w));
            v = -1; Assert.False(FitsWidth(v, w));
            v = 0; Assert.True(FitsWidth(v, w));
            v = 1; Assert.True(FitsWidth(v, w));
            v = 2; Assert.False(FitsWidth(v, w));
            v = 3; Assert.False(FitsWidth(v, w));
            v = 255; Assert.False(FitsWidth(v, w));
            v = 65535; Assert.False(FitsWidth(v, w));
            v = 2147483647; Assert.False(FitsWidth(v, w));
            v = unchecked((Int32)0xFFFFFFFF); Assert.False(FitsWidth(v, w));
        }

        [Test]
        public void Test_Signed_02()
        {
            int w = 2;
            Int32 v;
            v = -2147483647; Assert.False(FitsWidth(v, w));
            v = -3; Assert.False(FitsWidth(v, w));
            v = -2; Assert.True(FitsWidth(v, w));
            v = -1; Assert.True(FitsWidth(v, w));
            v = 0; Assert.True(FitsWidth(v, w));
            v = 1; Assert.True(FitsWidth(v, w));
            v = 2; Assert.False(FitsWidth(v, w));
            v = 3; Assert.False(FitsWidth(v, w));
            v = 255; Assert.False(FitsWidth(v, w));
            v = 65535; Assert.False(FitsWidth(v, w));
            v = 2147483647; Assert.False(FitsWidth(v, w));
            v = unchecked((Int32)0xFFFFFFFF); Assert.True(FitsWidth(v, w));
        }

        [Test]
        public void Test_Signed_08()
        {
            int w = 8;
            Int32 v;
            v = -2147483647; Assert.False(FitsWidth(v, w));
            v = -60000; Assert.False(FitsWidth(v, w));
            v = -129; Assert.False(FitsWidth(v, w));
            v = -128; Assert.True(FitsWidth(v, w));
            v = -1; Assert.True(FitsWidth(v, w));
            v = 0; Assert.True(FitsWidth(v, w));
            v = 1; Assert.True(FitsWidth(v, w));
            v = 127; Assert.True(FitsWidth(v, w));
            v = 255; Assert.False(FitsWidth(v, w));
            v = 256; Assert.False(FitsWidth(v, w));
            v = 65535; Assert.False(FitsWidth(v, w));
            v = 2147483647; Assert.False(FitsWidth(v, w));
            v = unchecked((Int32)0xFFFFFFFF); Assert.True(FitsWidth(v, w));
        }

        [Test]
        public void Test_Signed_16()
        {
            int w = 16;
            Int32 v;
            v = -2147483647; Assert.False(FitsWidth(v, w));
            v = -60000; Assert.False(FitsWidth(v, w));
            v = -32769; Assert.False(FitsWidth(v, w));
            v = -32768; Assert.True(FitsWidth(v, w));
            v = -1; Assert.True(FitsWidth(v, w));
            v = 0; Assert.True(FitsWidth(v, w));
            v = 1; Assert.True(FitsWidth(v, w));
            v = 32767; Assert.True(FitsWidth(v, w));
            v = 32768; Assert.False(FitsWidth(v, w));
            v = 65535; Assert.False(FitsWidth(v, w));
            v = 2147483647; Assert.False(FitsWidth(v, w));
            v = unchecked((Int32)0xFFFFFFFF); Assert.True(FitsWidth(v, w));
        }

        [Test]
        public void Test_Signed_31()
        {
            int w = 31;
            Int32 v;
            v = -2147483648; Assert.False(FitsWidth(v, w));
            v = -2147483647; Assert.False(FitsWidth(v, w));
            v = -1073741825; Assert.False(FitsWidth(v, w));
            v = -1073741824; Assert.True(FitsWidth(v, w));
            v = -1; Assert.True(FitsWidth(v, w));
            v = 0; Assert.True(FitsWidth(v, w));
            v = 1; Assert.True(FitsWidth(v, w));
            v = 1073741823; Assert.True(FitsWidth(v, w));
            v = 1073741824; Assert.False(FitsWidth(v, w));
            v = 2147483647; Assert.False(FitsWidth(v, w));
            v = unchecked((Int32)0xFFFFFFFF); Assert.True(FitsWidth(v, w));
        }

        [Test]
        public void Test_Signed_32()
        {
            int w = 32;
            Int32 v;
            v = -2147483648; Assert.True(FitsWidth(v, w));
            v = -2147483647; Assert.True(FitsWidth(v, w));
            v = -1; Assert.True(FitsWidth(v, w));
            v = 0; Assert.True(FitsWidth(v, w));
            v = 1; Assert.True(FitsWidth(v, w));
            v = 2147483647; Assert.True(FitsWidth(v, w));
            v = unchecked((Int32)0xFFFFFFFF); Assert.True(FitsWidth(v, w));
        }

        [Test]
        public void Test_Signed_ZZZ()
        {
            //Assert.AreEqual(RanSigned, 0);
            Assert.AreEqual(RanUnsigned, 0);
        }

        [Test]
        public void Test_Unsigned_000()
        {
            RanSigned = 0;
            RanUnsigned = 0;
        }

        [Test]
        public void Test_Unsigned_01()
        {
            int w = 1;
            UInt32 v;
            v = unchecked((UInt32)0x7FFFFFFF); Assert.False(FitsWidth(v, w));
            v = unchecked((UInt32)0x7FFFFFFE); Assert.False(FitsWidth(v, w));
            v = unchecked((UInt32)0x7FFFFFF0); Assert.False(FitsWidth(v, w));
            v = 0; Assert.True(FitsWidth(v, w));
            v = 1; Assert.True(FitsWidth(v, w));
            v = 2; Assert.False(FitsWidth(v, w));
            v = 3; Assert.False(FitsWidth(v, w));
            v = 255; Assert.False(FitsWidth(v, w));
            v = 65535; Assert.False(FitsWidth(v, w));
            v = 2147483647; Assert.False(FitsWidth(v, w));
            v = unchecked((UInt32)0xFFFFFFFF); Assert.False(FitsWidth(v, w));
        }

        [Test]
        public void Test_Unsigned_02()
        {
            int w = 2;
            UInt32 v;
            v = unchecked((UInt32)0x7FFFFFFF); Assert.False(FitsWidth(v, w));
            v = unchecked((UInt32)0x7FFFFFFE); Assert.False(FitsWidth(v, w));
            v = unchecked((UInt32)0x7FFFFFF0); Assert.False(FitsWidth(v, w));
            v = 0; Assert.True(FitsWidth(v, w));
            v = 1; Assert.True(FitsWidth(v, w));
            v = 2; Assert.True(FitsWidth(v, w));
            v = 3; Assert.True(FitsWidth(v, w));
            v = 4; Assert.False(FitsWidth(v, w));
            v = 255; Assert.False(FitsWidth(v, w));
            v = 65535; Assert.False(FitsWidth(v, w));
            v = 2147483647; Assert.False(FitsWidth(v, w));
            v = unchecked((UInt32)0xFFFFFFFF); Assert.False(FitsWidth(v, w));
        }

        [Test]
        public void Test_Unsigned_08()
        {
            int w = 8;
            UInt32 v;
            v = unchecked((UInt32)0x7FFFFFFF); Assert.False(FitsWidth(v, w));
            v = unchecked((UInt32)0x7FFFFFFE); Assert.False(FitsWidth(v, w));
            v = unchecked((UInt32)(-32000)); Assert.False(FitsWidth(v, w));
            v = unchecked((UInt32)(-1)); Assert.False(FitsWidth(v, w));
            v = 0; Assert.True(FitsWidth(v, w));
            v = 1; Assert.True(FitsWidth(v, w));
            v = 2; Assert.True(FitsWidth(v, w));
            v = 3; Assert.True(FitsWidth(v, w));
            v = 255; Assert.True(FitsWidth(v, w));
            v = 256; Assert.False(FitsWidth(v, w));
            v = 65535; Assert.False(FitsWidth(v, w));
            v = 2147483647; Assert.False(FitsWidth(v, w));
            v = unchecked((UInt32)0xFFFFFFFF); Assert.False(FitsWidth(v, w));
        }

        [Test]
        public void Test_Unsigned_16()
        {
            int w = 16;
            UInt32 v;
            v = unchecked((UInt32)0x7FFFFFFF); Assert.False(FitsWidth(v, w));
            v = unchecked((UInt32)0x7FFFFFFE); Assert.False(FitsWidth(v, w));
            v = unchecked((UInt32)(-32000)); Assert.False(FitsWidth(v, w));
            v = unchecked((UInt32)(-1)); Assert.False(FitsWidth(v, w));
            v = 0; Assert.True(FitsWidth(v, w));
            v = 1; Assert.True(FitsWidth(v, w));
            v = 2; Assert.True(FitsWidth(v, w));
            v = 3; Assert.True(FitsWidth(v, w));
            v = 256; Assert.True(FitsWidth(v, w));
            v = 32767; Assert.True(FitsWidth(v, w));
            v = 32768; Assert.True(FitsWidth(v, w));
            v = 65535; Assert.True(FitsWidth(v, w));
            v = 65536; Assert.False(FitsWidth(v, w));
            v = 2147483647; Assert.False(FitsWidth(v, w));
            v = unchecked((UInt32)0xFFFFFFFF); Assert.False(FitsWidth(v, w));
        }

        [Test]
        public void Test_Unsigned_31()
        {
            int w = 31;
            UInt32 v;
            v = unchecked((UInt32)0x80000000); Assert.False(FitsWidth(v, w));
            v = unchecked((UInt32)(-32000)); Assert.False(FitsWidth(v, w));
            v = unchecked((UInt32)(-1)); Assert.False(FitsWidth(v, w));
            v = 0; Assert.True(FitsWidth(v, w));
            v = 1; Assert.True(FitsWidth(v, w));
            v = 3; Assert.True(FitsWidth(v, w));
            v = 256; Assert.True(FitsWidth(v, w));
            v = 32768; Assert.True(FitsWidth(v, w));
            v = 65535; Assert.True(FitsWidth(v, w));
            v = unchecked((UInt32)0x7FFFFFFF); Assert.True(FitsWidth(v, w));
            v = unchecked((UInt32)0x7FFFFFFE); Assert.True(FitsWidth(v, w));
            v = 2147483647; Assert.True(FitsWidth(v, w));
            v = 2147483648; Assert.False(FitsWidth(v, w));
            v = unchecked((UInt32)0xFFFFFFFF); Assert.False(FitsWidth(v, w));
        }

        [Test]
        public void Test_Unsigned_32()
        {
            int w = 32;
            UInt32 v;
            v = unchecked((UInt32)0x80000000); Assert.True(FitsWidth(v, w));
            v = unchecked((UInt32)(-32000)); Assert.True(FitsWidth(v, w));
            v = unchecked((UInt32)(-1)); Assert.True(FitsWidth(v, w));
            v = 0; Assert.True(FitsWidth(v, w));
            v = 1; Assert.True(FitsWidth(v, w));
            v = 3; Assert.True(FitsWidth(v, w));
            v = 256; Assert.True(FitsWidth(v, w));
            v = 32768; Assert.True(FitsWidth(v, w));
            v = 65535; Assert.True(FitsWidth(v, w));
            v = unchecked((UInt32)0x7FFFFFFF); Assert.True(FitsWidth(v, w));
            v = unchecked((UInt32)0x7FFFFFFE); Assert.True(FitsWidth(v, w));
            v = 2147483647; Assert.True(FitsWidth(v, w));
            v = 2147483648; Assert.True(FitsWidth(v, w));
            v = unchecked((UInt32)0xFFFFFFFF); Assert.True(FitsWidth(v, w));
        }

        [Test]
        public void Test_Unsigned_ZZZ()
        {
            Assert.AreEqual(RanSigned, 0);
            //Assert.AreEqual(RanUnsigned, 0);
        }


        /// <summary>
        /// Check if signed int value fits in Width bits 
        /// Value of width==1 must be 0 or 1.
        /// </summary>
        /// <param name="Value">The value</param>
        /// <param name="Width">Width in bits</param>
        /// <returns></returns>
        private static bool FitsWidth(Int32 Value, int Width)
        {
            ++RanSigned;

            if (Width == 1)
            {
                return((Value == 0) || (Value == 1));
            }
            else
            {
                Int32 Max = (Int32)(0xFFFFFFFF >> (32 - Width + 1));
                Int32 Min = -Max - 1;
                return ((Value >= Min) && (Value <= Max));
            }
        }

        /// <summary>
        /// Check if unsigned int value fits in Width bits 
        /// </summary>
        /// <param name="Value">Int32 or UInt32 value</param>
        /// <param name="Width">Width in bits</param>
        /// <returns></returns>
        private static bool FitsWidth(UInt32 Value, int Width)
        {
            ++RanUnsigned;

            // check unsigned
            UInt32 Max = ((UInt32)0xFFFFFFFF) >> (32 - Width);
            return (Value <= Max);
        }
    }
}
