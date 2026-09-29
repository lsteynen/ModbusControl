using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using LST.Helpers;
using ModbusControl;

namespace UnitTests
{
    [TestClass]
    public class Test_IntFitsWidth
    {
        //private static bool IntFitsWidth(Int64 Value, int Width)

        /*
         *      1 bit:  0 or 1
         *      
         *      2 bits:         unsigned    signed
         *              00      0           0
         *              01      1           1
         *              10      2           -2
         *              11      3           -1
         *              
         *      3 bits:         unsigned    signed
         *              000     0           0
         *              001     1           1
         *              010     2           2
         *              011     3           3
         *              100     4           -4
         *              101     5           -3
         *              110     6           -2
         *              111     7           -1
         *              
         *      8 bits:             unsigned    signed
         *              00000000    0           0
         *              00000001    1           1
         *              00000010    2           2
         *              01111110    126         126
         *              01111111    127         127
         *              10000000    128         -128
         *              10000001    129         -127
         *              11111110    254         -2
         *              11111111    255         -1
         *              
         *               -32,768 to 32,767, 
         *               0 to 65,535. 
         */

        [TestMethod]
        public void Test_signed_e1()
        {
            Assert.ThrowsExactly<ArgumentException>(() =>
                CValueToString.IntFitsWidth(123, 0));
        }

        [TestMethod]
        public void Test_signed_e2()
        {
            Assert.ThrowsExactly<ArgumentException>(() =>
                CValueToString.IntFitsWidth(123, 33));
        }

        [TestMethod]
        public void Test_signed_w1_1()
        {
            int w = 1;
            bool res = CValueToString.IntFitsWidth(-12345678, w);
            Assert.IsFalse(res);
        }

        [TestMethod]
        public void Test_signed_w1_2()
        {
            int w = 1;
            bool res = CValueToString.IntFitsWidth(-1, w);
            Assert.IsFalse(res);
        }

        [TestMethod]
        public void Test_signed_w1_3()
        {
            int w = 1;
            bool res = CValueToString.IntFitsWidth(0, w);
            Assert.IsTrue(res);
        }

        [TestMethod]
        public void Test_signed_w1_4()
        {
            int w = 1;
            bool res = CValueToString.IntFitsWidth(1, w);
            Assert.IsTrue(res);
        }

        [TestMethod]
        public void Test_signed_w1_5()
        { 
            int w = 1;
            bool res = CValueToString.IntFitsWidth(2, w);
            Assert.IsFalse(res);
        }

        [TestMethod]
        public void Test_signed_w1_6()
        {   
            int w = 1;
            bool res = CValueToString.IntFitsWidth(12345678, w);
            Assert.IsFalse(res);
        }

        [TestMethod]
        public void Test_signed_w2_1()
        {
            int w = 2;
            bool res = CValueToString.IntFitsWidth(-2000111222, w);
            Assert.IsFalse(res);
        }

        [TestMethod]
        public void Test_signed_w2_2()
        {
            int w = 2;
            bool res = CValueToString.IntFitsWidth(-3, w);
            Assert.IsFalse(res);
        }

        [TestMethod]
        public void Test_signed_w2_4()
        {
            int w = 2;
            bool res = CValueToString.IntFitsWidth(-2, w);
            Assert.IsTrue(res);
        }

        [TestMethod]
        public void Test_signed_w2_5()
        {
            int w = 2;
            bool res = CValueToString.IntFitsWidth(-1, w);
            Assert.IsTrue(res);
        }

        [TestMethod]
        public void Test_signed_w2_6()
        {
            int w = 2;
            bool res = CValueToString.IntFitsWidth(0, w);
            Assert.IsTrue(res);
        }

        [TestMethod]
        public void Test_signed_w2_7()
        {
            int w = 2;
            bool res = CValueToString.IntFitsWidth(1, w);
            Assert.IsTrue(res);
        }

        [TestMethod]
        public void Test_signed_w2_8()
        {
            int w = 2;
            bool res = CValueToString.IntFitsWidth(3, w);
            Assert.IsTrue(res);
        }

        [TestMethod]
        public void Test_signed_w2_9()
        {
            int w = 2;
            bool res = CValueToString.IntFitsWidth(4, w);
            Assert.IsFalse(res);
        }

        [TestMethod]
        public void Test_signed_w2_10()
        {
            int w = 2;
            bool res = CValueToString.IntFitsWidth(1000, w);
            Assert.IsFalse(res);
        }

        [TestMethod]
        public void Test_signed_w3_1()
        {
            int w = 3;
            bool res = CValueToString.IntFitsWidth(-2000111222, w);
            Assert.IsFalse(res);
        }

        [TestMethod]
        public void Test_signed_w3_2()
        {
            int w = 3;
            bool res = CValueToString.IntFitsWidth(-5, w);
            Assert.IsFalse(res);
        }

        [TestMethod]
        public void Test_signed_w3_4()
        {
            int w = 3; 
            bool res = CValueToString.IntFitsWidth(-4, w);
            Assert.IsTrue(res);
        }

        [TestMethod]
        public void Test_signed_w3_5()
        {
            int w = 3;
            bool res = CValueToString.IntFitsWidth(-1, w);
            Assert.IsTrue(res);
        }

        [TestMethod]
        public void Test_signed_w3_6()
        {
            int w = 3;
            bool res = CValueToString.IntFitsWidth(0, w);
            Assert.IsTrue(res);
        }

        [TestMethod]
        public void Test_signed_w3_7()
        {
            int w = 3;
            bool res = CValueToString.IntFitsWidth(1, w);
            Assert.IsTrue(res);
        }

        [TestMethod]
        public void Test_signed_w3_8()
        {
            int w = 3;
            bool res = CValueToString.IntFitsWidth(7, w);
            Assert.IsTrue(res);
        }

        [TestMethod]
        public void Test_signed_w3_9()
        {
            int w = 3;
            bool res = CValueToString.IntFitsWidth(8, w);
            Assert.IsFalse(res);
        }

        [TestMethod]
        public void Test_signed_w3_10()
        {
            int w = 3;
            bool res = CValueToString.IntFitsWidth(12345, w);
            Assert.IsFalse(res);
        }

        [TestMethod]
        public void Test_signed_w8_1()
        {
            int w = 8;
            bool res = CValueToString.IntFitsWidth(-2147483647, w);
            Assert.IsFalse(res);
        }

        [TestMethod]
        public void Test_signed_w8_2()
        {
            int w = 8;
            bool res = CValueToString.IntFitsWidth(-129, w);
            Assert.IsFalse(res);
        }

        [TestMethod]
        public void Test_signed_w8_3()
        {
            int w = 8;
            bool res = CValueToString.IntFitsWidth(-128, w);
            Assert.IsTrue(res);
        }

        [TestMethod]
        public void Test_signed_w8_4()
        {
            int w = 8;
            bool res = CValueToString.IntFitsWidth(-1, w);
            Assert.IsTrue(res);
        }

        [TestMethod]
        public void Test_signed_w8_5()
        {
            int w = 8;
            bool res = CValueToString.IntFitsWidth(0, w);
            Assert.IsTrue(res);
        }

        [TestMethod]
        public void Test_signed_w8_6()
        {
            int w = 8;
            bool res = CValueToString.IntFitsWidth(1, w);
            Assert.IsTrue(res);
        }

        [TestMethod]
        public void Test_signed_w8_7()
        {
            int w = 8;
            bool res = CValueToString.IntFitsWidth(255, w);
            Assert.IsTrue(res);
        }

        [TestMethod]
        public void Test_signed_w8_8()
        {
            int w = 8;
            bool res = CValueToString.IntFitsWidth(256, w);
            Assert.IsFalse(res);
        }

        [TestMethod]
        public void Test_signed_w8_9()
        {
            int w = 8;
            bool res = CValueToString.IntFitsWidth(123456, w);
            Assert.IsFalse(res);
        }

        [TestMethod]
        public void Test_signed_w16_1()
        {
            int w = 16;
            bool res = CValueToString.IntFitsWidth(-2147483647, w);
            Assert.IsFalse(res);
        }

        [TestMethod]
        public void Test_signed_w16_2()
        {
            int w = 16;
            bool res = CValueToString.IntFitsWidth(-32769, w);
            Assert.IsFalse(res);
        }

        [TestMethod]
        public void Test_signed_w16_3()
        {
            int w = 16;
            bool res = CValueToString.IntFitsWidth(-32768, w);
            Assert.IsTrue(res);
        }

        [TestMethod]
        public void Test_signed_w16_4()
        {
            int w = 16;
            bool res = CValueToString.IntFitsWidth(-1, w);
            Assert.IsTrue(res);
        }

        [TestMethod]
        public void Test_signed_w16_5()
        {
            int w = 16;
            bool res = CValueToString.IntFitsWidth(0, w);
            Assert.IsTrue(res);
        }

        [TestMethod]
        public void Test_signed_w16_6()
        {
            int w = 16;
            bool res = CValueToString.IntFitsWidth(1, w);
            Assert.IsTrue(res);
        }

        [TestMethod]
        public void Test_signed_w16_7()
        {
            int w = 16;
            bool res = CValueToString.IntFitsWidth(1000, w);
            Assert.IsTrue(res);
        }

        [TestMethod]
        public void Test_signed_w16_8()
        {
            int w = 16;
            bool res = CValueToString.IntFitsWidth(65535, w);
            Assert.IsTrue(res);
        }

        [TestMethod]
        public void Test_signed_w16_9()
        {
            int w = 16;
            bool res = CValueToString.IntFitsWidth(65536, w);
            Assert.IsFalse(res);
        }

        [TestMethod]
        public void Test_signed_w16_10()
        {
            int w = 16;
            bool res = CValueToString.IntFitsWidth(2111222333, w);
            Assert.IsFalse(res);
        }

        // 32-bit range: -2,147,483,648 .. 4,294,967,295

        [TestMethod]
        public void Test_w32_1()
        {
            int w = 32;
            bool res = CValueToString.IntFitsWidth(-2147483649111, w);
            Assert.IsFalse(res);
        }

        [TestMethod]
        public void Test_w32_1b()
        {
            int w = 32;
            bool res = CValueToString.IntFitsWidth(-2147483649, w);
            Assert.IsFalse(res);
        }


        [TestMethod]
        public void Test_w32_1c()
        {
            int w = 32;
            bool res = CValueToString.IntFitsWidth(-2147483648, w);
            Assert.IsTrue(res);
        }

        [TestMethod]
        public void Test_w32_2()
        {
            int w = 32;
            bool res = CValueToString.IntFitsWidth(-1000, w);
            Assert.IsTrue(res);
        }

        [TestMethod]
        public void Test_w32_3()
        {
            int w = 32;
            bool res = CValueToString.IntFitsWidth(0, w);
            Assert.IsTrue(res);
        }

        [TestMethod]
        public void Test_w32_4()
        {
            int w = 32;
            bool res = CValueToString.IntFitsWidth(2110220330, w);
            Assert.IsTrue(res);
        }

        [TestMethod]
        public void Test_w32_5()
        {
            int w = 32;
            bool res = CValueToString.IntFitsWidth(2147483647, w);
            Assert.IsTrue(res);
        }

        [TestMethod]
        public void Test_w32_6()
        {
            int w = 32;
            bool res = CValueToString.IntFitsWidth(2147483648, w);
            Assert.IsTrue(res);
        }

        [TestMethod]
        public void Test_w32_7()
        {
            int w = 32;
            bool res = CValueToString.IntFitsWidth(4294967295, w);
            Assert.IsTrue(res);
        }

        [TestMethod]
        public void Test_w32_8()
        {
            int w = 32;
            bool res = CValueToString.IntFitsWidth(4294967296, w);
            Assert.IsFalse(res);
        }

        [TestMethod]
        public void Test_w32_9()
        {
            int w = 32;
            bool res = CValueToString.IntFitsWidth(4294967295999, w);
            Assert.IsFalse(res);
        }

    }
}
