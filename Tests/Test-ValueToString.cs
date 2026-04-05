using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LST.Helpers;
using ModbusControlSpace;

namespace UnitTests
{
    [TestClass]
    public class Test_Int16RegistersToValueString
    {
        // public static string Int16RegistersToValueString(Int16[] Regs, Global.eFormat Format, int Width, int Decimals = 0, int MinLen = 0, Global.CEnum EnumDict = null)

        [TestMethod]
        public void Test_Int16_1()
        {
            Int16[] Regs = new short[2];
            Regs[0] = 0;
            Converts.Int32Endian = Global.eEndian.Little;
            string res = CValueToString.Int16RegistersToValueString(Regs, Global.eFormat.Int, Global.eFormat.Int, 16);
            Assert.AreEqual<string>("0", res);
        }

        [TestMethod]
        public void Test_Int16_2()
        {
            Int16[] Regs = new short[2];
            Regs[0] = 1;
            Converts.Int32Endian = Global.eEndian.Little;
            string res = CValueToString.Int16RegistersToValueString(Regs, Global.eFormat.Int, Global.eFormat.Int, 16);
            Assert.AreEqual<string>("1", res);
        }

        [TestMethod]
        public void Test_Int16_3()
        {
            Int16[] Regs = new short[2];
            Regs[0] = 254;
            Converts.Int32Endian = Global.eEndian.Little;
            string res = CValueToString.Int16RegistersToValueString(Regs, Global.eFormat.Int, Global.eFormat.Int, 16);
            Assert.AreEqual<string>("254", res);
        }

        [TestMethod]
        public void Test_Int16_3b()
        {
            Int16[] Regs = new short[2];
            Regs[0] = 255;
            Converts.Int32Endian = Global.eEndian.Little;
            string res = CValueToString.Int16RegistersToValueString(Regs, Global.eFormat.Int, Global.eFormat.Int, 16);
            Assert.AreEqual<string>("255", res);
        }

        [TestMethod]
        public void Test_Int16_3c()
        {
            Int16[] Regs = new short[2];
            Regs[0] = 256;
            Converts.Int32Endian = Global.eEndian.Little;
            string res = CValueToString.Int16RegistersToValueString(Regs, Global.eFormat.Int, Global.eFormat.Int, 16);
            Assert.AreEqual<string>("256", res);
        }

        [TestMethod]
        public void Test_Int16_4()
        {
            Int16[] Regs = new short[2];
            Regs[0] = 32000;
            Converts.Int32Endian = Global.eEndian.Little;
            string res = CValueToString.Int16RegistersToValueString(Regs, Global.eFormat.Int, Global.eFormat.Int, 16);
            Assert.AreEqual<string>("32000", res);

            Converts.Int32Endian = Global.eEndian.Big;
            res = CValueToString.Int16RegistersToValueString(Regs, Global.eFormat.Int, Global.eFormat.Int, 16);
            Assert.AreEqual<string>("32000", res);
        }

        [TestMethod]
        public void Test_Int16_5()
        {
            Int16[] Regs = new short[2];
            Regs[0] = -31000;
            Converts.Int32Endian = Global.eEndian.Little;
            string res = CValueToString.Int16RegistersToValueString(Regs, Global.eFormat.Int, Global.eFormat.Int, 16);
            Assert.AreEqual<string>("-31000", res);
        }

        [TestMethod]
        public void Test_Int16_d_1()
        {
            Int16[] Regs = new short[2];
            Regs[0] = 32000;
            Converts.Int32Endian = Global.eEndian.Little;
            string res = CValueToString.Int16RegistersToValueString(Regs, Global.eFormat.Int, Global.eFormat.Int, 16, 0);
            Assert.AreEqual<string>("32000", res);
        }

        [TestMethod]
        public void Test_Int16_d_2()
        {
            Int16[] Regs = new short[2];
            Regs[0] = 12345;
            Converts.Int32Endian = Global.eEndian.Little;
            string res = CValueToString.Int16RegistersToValueString(Regs, Global.eFormat.Int, Global.eFormat.Int, 16, 1);
            Assert.AreEqual<string>("1234,5", res);
        }

        [TestMethod]
        public void Test_Int16_m_1()
        {
            Int16[] Regs = new short[2];
            Regs[0] = 0;
            Converts.Int32Endian = Global.eEndian.Little;
            string res = CValueToString.Int16RegistersToValueString(Regs, Global.eFormat.Int, Global.eFormat.Int, 16, 0, 3);
            Assert.AreEqual<string>("000", res);
        }

        [TestMethod]
        public void Test_Int16_m_2()
        {
            Int16[] Regs = new short[2];
            Regs[0] = 18;
            Converts.Int32Endian = Global.eEndian.Little;
            string res = CValueToString.Int16RegistersToValueString(Regs, Global.eFormat.Int, Global.eFormat.Int, 16, 0, 8);
            Assert.AreEqual<string>("00000018", res);
        }

        [TestMethod]
        public void Test_Int16_m_3()
        {
            Int16[] Regs = new short[2];
            Regs[0] = -18;
            Converts.Int32Endian = Global.eEndian.Little;
            string res = CValueToString.Int16RegistersToValueString(Regs, Global.eFormat.Int, Global.eFormat.Int, 16, 0, 8);
            Assert.AreEqual<string>("-00000018", res);
        }

        [TestMethod]
        public void Test_Int16_d_m_1()
        {
            // decimals and minlen together is not supported 
        }

        [TestMethod]
        public void Test_UInt16_1()
        {
            Int16[] Regs = new short[2];
            Regs[0] = 38;
            Converts.Int32Endian = Global.eEndian.Little;
            string res = CValueToString.Int16RegistersToValueString(Regs, Global.eFormat.UInt, Global.eFormat.UInt, 16);
            Assert.AreEqual<string>("38", res);
        }

        [TestMethod]
        public void Test_UInt16_2()
        {
            Int16[] Regs = new short[2];
            Regs[0] = Helpers.ConvToInt16(50000);
            Converts.Int32Endian = Global.eEndian.Little;
            string res = CValueToString.Int16RegistersToValueString(Regs, Global.eFormat.UInt, Global.eFormat.UInt, 16);
            Assert.AreEqual<string>("50000", res);
        }

        [TestMethod]
        public void Test_UInt16_d_1()
        {
            Int16[] Regs = new short[2];
            Regs[0] = Helpers.ConvToInt16(44123);
            Converts.Int32Endian = Global.eEndian.Little;
            string res = CValueToString.Int16RegistersToValueString(Regs, Global.eFormat.UInt, Global.eFormat.UInt, 16, 3);
            Assert.AreEqual<string>("44,123", res);
        }

        [TestMethod]
        public void Test_UInt16_m_1()
        {
            Int16[] Regs = new short[2];
            Regs[0] = Helpers.ConvToInt16(35119);
            Converts.Int32Endian = Global.eEndian.Little;
            string res = CValueToString.Int16RegistersToValueString(Regs, Global.eFormat.UInt, Global.eFormat.UInt, 16, 0, 12);
            Assert.AreEqual<string>("000000035119", res);
        }

        [TestMethod]
        public void Test_Bin16_1()
        {
            Int16[] Regs = new short[2];
            Regs[0] = 0;
            Converts.Int32Endian = Global.eEndian.Little;
            string res = CValueToString.Int16RegistersToValueString(Regs, Global.eFormat.UInt, Global.eFormat.Bin, 16);
            Assert.AreEqual<string>("0b0", res);
        }

        [TestMethod]
        public void Test_Bin16_2()
        {
            Int16[] Regs = new short[2];
            Regs[0] = 13158;
            Converts.Int32Endian = Global.eEndian.Little;
            string res = CValueToString.Int16RegistersToValueString(Regs, Global.eFormat.UInt, Global.eFormat.Bin, 16);
            Assert.AreEqual<string>("0b11.0011.0110.0110", res);
        }

        [TestMethod]
        public void Test_Bin16_m_1()
        {
            Int16[] Regs = new short[2];
            Regs[0] = 1000;
            Converts.Int32Endian = Global.eEndian.Little;
            string res = CValueToString.Int16RegistersToValueString(Regs, Global.eFormat.Int, Global.eFormat.Bin, 16, 0, 32);
            Assert.AreEqual<string>("0b0000.0000.0000.0000.0000.0011.1110.1000", res);
        }

        [TestMethod]
        public void Test_Bin16_m_2()
        {
            Int16[] Regs = new short[2];
            Regs[0] = -1000;
            Converts.Int32Endian = Global.eEndian.Little;
            string res = CValueToString.Int16RegistersToValueString(Regs, Global.eFormat.Int, Global.eFormat.Bin, 16, 0, 32);
            Assert.AreEqual<string>("0b0000.0000.0000.0000.1111.1100.0001.1000", res);
        }

        [TestMethod]
        public void Test_Hex16_1()
        {
            Int16[] Regs = new short[2];
            Regs[0] = 0;
            Converts.Int32Endian = Global.eEndian.Little;
            string res = CValueToString.Int16RegistersToValueString(Regs, Global.eFormat.Int, Global.eFormat.Hex, 16);
            Assert.AreEqual<string>("0x0", res);
        }

        [TestMethod]
        public void Test_Hex16_2()
        {
            Int16[] Regs = new short[2];
            Regs[0] = 15;
            Converts.Int32Endian = Global.eEndian.Little;
            string res = CValueToString.Int16RegistersToValueString(Regs, Global.eFormat.Int, Global.eFormat.Hex, 16);
            Assert.AreEqual<string>("0xF", res);
        }

        [TestMethod]
        public void Test_Hex16_3()
        {
            Int16[] Regs = new short[2];
            Regs[0] = Helpers.ConvToInt16(50000);
            Converts.Int32Endian = Global.eEndian.Little;
            string res = CValueToString.Int16RegistersToValueString(Regs, Global.eFormat.Int, Global.eFormat.Hex, 16);
            Assert.AreEqual<string>("0xC350", res);
        }

        [TestMethod]
        public void Test_Hex16_m_3()
        {
            Int16[] Regs = new short[2];
            Regs[0] = Helpers.ConvToInt16(10000);
            Converts.Int32Endian = Global.eEndian.Little;
            string res = CValueToString.Int16RegistersToValueString(Regs, Global.eFormat.Hex, Global.eFormat.Hex, 16, 0, 8);
            Assert.AreEqual<string>("0x00002710", res);
        }

        [TestMethod]
        public void Test_Int32_1()
        {
            Int16[] Regs = new short[2];
            Regs[0] = 0; Regs[1] = 0;
            Converts.Int32Endian = Global.eEndian.Little;
            string res = CValueToString.Int16RegistersToValueString(Regs, Global.eFormat.Int, Global.eFormat.Int, 32);
            Assert.AreEqual<string>("0", res);

            Converts.Int32Endian = Global.eEndian.Big;
            res = CValueToString.Int16RegistersToValueString(Regs, Global.eFormat.Int, Global.eFormat.Int, 32);
            Assert.AreEqual<string>("0", res);
        }

        [TestMethod]
        public void Test_Int32_2()
        {
            Int16[] Regs = new short[2];
            Regs[0] = 1; Regs[1] = 0;
            Converts.Int32Endian = Global.eEndian.Little;
            string res = CValueToString.Int16RegistersToValueString(Regs, Global.eFormat.Int, Global.eFormat.Int, 32);
            Assert.AreEqual<string>("1", res);

            Converts.Int32Endian = Global.eEndian.Big;
            res = CValueToString.Int16RegistersToValueString(Regs, Global.eFormat.Int, Global.eFormat.Int, 32);
            Assert.AreEqual<string>("65536", res);
        }

        [TestMethod]
        public void Test_Int32_3()
        {
            Int16[] Regs = new short[2];
            Regs[0] = 0; Regs[1] = 1;
            Converts.Int32Endian = Global.eEndian.Little;
            string res = CValueToString.Int16RegistersToValueString(Regs, Global.eFormat.Int, Global.eFormat.Int, 32);
            Assert.AreEqual<string>("65536", res);

            Converts.Int32Endian = Global.eEndian.Big;
            res = CValueToString.Int16RegistersToValueString(Regs, Global.eFormat.Int, Global.eFormat.Int, 32);
            Assert.AreEqual<string>("1", res);
        }

        [TestMethod]
        public void Test_Int32_4()
        {
            Int16[] Regs = new short[2];
            Regs[0] = 100; Regs[1] = 200;
            Converts.Int32Endian = Global.eEndian.Little;
            string res = CValueToString.Int16RegistersToValueString(Regs, Global.eFormat.Int, Global.eFormat.Int, 32);
            Assert.AreEqual<string>("13107300", res);

            Converts.Int32Endian = Global.eEndian.Big;
            res = CValueToString.Int16RegistersToValueString(Regs, Global.eFormat.Int, Global.eFormat.Int, 32);
            Assert.AreEqual<string>("6553800", res);
        }

        [TestMethod]
        public void Test_Int32_5()
        {
            Int16[] Regs = new short[2];
            Regs[0] = 20000; Regs[1] = 21000;
            Converts.Int32Endian = Global.eEndian.Little;
            string res = CValueToString.Int16RegistersToValueString(Regs, Global.eFormat.Int, Global.eFormat.Int, 32);
            Assert.AreEqual<string>("1376276000", res);

            Converts.Int32Endian = Global.eEndian.Big;
            res = CValueToString.Int16RegistersToValueString(Regs, Global.eFormat.Int, Global.eFormat.Int, 32);
            Assert.AreEqual<string>("1310741000", res);
        }

        [TestMethod]
        public void Test_Int32_6()
        {
            Int16[] Regs = new short[2];
            Regs[0] = Helpers.ConvToInt16(50000); Regs[1] = 21000;
            Converts.Int32Endian = Global.eEndian.Little;
            string res = CValueToString.Int16RegistersToValueString(Regs, Global.eFormat.Int, Global.eFormat.Int, 32);
            Assert.AreEqual<string>("1376306000", res);

            Converts.Int32Endian = Global.eEndian.Big;
            res = CValueToString.Int16RegistersToValueString(Regs, Global.eFormat.Int, Global.eFormat.Int, 32);
            Assert.AreEqual<string>("-1018146296", res);
        }

        [TestMethod]
        public void Test_Int32_7()
        {
            Int16[] Regs = new short[2];
            Regs[0] = 20000; Regs[1] = Helpers.ConvToInt16(51000);
            Converts.Int32Endian = Global.eEndian.Little;
            string res = CValueToString.Int16RegistersToValueString(Regs, Global.eFormat.Int, Global.eFormat.Int, 32);
            Assert.AreEqual<string>("-952611296", res);

            Converts.Int32Endian = Global.eEndian.Big;
            res = CValueToString.Int16RegistersToValueString(Regs, Global.eFormat.Int, Global.eFormat.Int, 32);
            Assert.AreEqual<string>("1310771000", res);
        }

        [TestMethod]
        public void Test_Int32_8()
        {
            Int16[] Regs = new short[2];
            Regs[0] = Helpers.ConvToInt16(50000); Regs[1] = Helpers.ConvToInt16(51000);
            Converts.Int32Endian = Global.eEndian.Little;
            string res = CValueToString.Int16RegistersToValueString(Regs, Global.eFormat.Int, Global.eFormat.Int, 32);
            Assert.AreEqual<string>("-952581296", res);

            Converts.Int32Endian = Global.eEndian.Big;
            res = CValueToString.Int16RegistersToValueString(Regs, Global.eFormat.Int, Global.eFormat.Int, 32);
            Assert.AreEqual<string>("-1018116296", res);
        }

        [TestMethod]
        public void Test_Int32_d_1()
        {
            Int16[] Regs = new short[2];
            Regs[0] = 12345; Regs[1] = 21000;
            Converts.Int32Endian = Global.eEndian.Little;
            string res = CValueToString.Int16RegistersToValueString(Regs, Global.eFormat.Int, Global.eFormat.Int, 32, 3);
            Assert.AreEqual<string>("1376268,345", res);
        }


        [TestMethod]
        public void Test_Int32_d_2()
        {
            Int16[] Regs = new short[2];
            Regs[0] = Helpers.ConvToInt16(50000); Regs[1] = Helpers.ConvToInt16(51000);
            Converts.Int32Endian = Global.eEndian.Little;
            string res = CValueToString.Int16RegistersToValueString(Regs, Global.eFormat.Int, Global.eFormat.Int, 32, 3);
            Assert.AreEqual<string>("-952581,296", res);
        }

        [TestMethod]
        public void Test_Int32_m_1()
        {
            Int16[] Regs = new short[2];
            Regs[0] = 20000; Regs[1] = 0;
            Converts.Int32Endian = Global.eEndian.Little;
            string res = CValueToString.Int16RegistersToValueString(Regs, Global.eFormat.Int, Global.eFormat.Int, 32, 0, 3);
            Assert.AreEqual<string>("20000", res);
        }

        [TestMethod]
        public void Test_Int32_m_2()
        {
            Int16[] Regs = new short[2];
            Regs[0] = 20000; Regs[1] = 0;
            Converts.Int32Endian = Global.eEndian.Little;
            string res = CValueToString.Int16RegistersToValueString(Regs, Global.eFormat.Int, Global.eFormat.Int, 32, 0, 8);
            Assert.AreEqual<string>("00020000", res);
        }

        [TestMethod]
        public void Test_Int32_m_3()
        {
            Int16[] Regs = new short[2];
            Regs[0] = Helpers.ConvToInt16(50000); Regs[1] = Helpers.ConvToInt16(51000);
            Converts.Int32Endian = Global.eEndian.Little;
            string res = CValueToString.Int16RegistersToValueString(Regs, Global.eFormat.Int, Global.eFormat.Int, 32, 0, 15);
            Assert.AreEqual<string>("-000000952581296", res);
        }

        [TestMethod]
        public void Test_UInt32_1()
        {
            Int16[] Regs = new short[2];
            Regs[0] = 0; Regs[1] = 0;
            Converts.Int32Endian = Global.eEndian.Little;
            string res = CValueToString.Int16RegistersToValueString(Regs, Global.eFormat.Int, Global.eFormat.UInt, 32);
            Assert.AreEqual<string>("0", res);

            Converts.Int32Endian = Global.eEndian.Big;
            res = CValueToString.Int16RegistersToValueString(Regs, Global.eFormat.Int, Global.eFormat.UInt, 32);
            Assert.AreEqual<string>("0", res);
        }

        [TestMethod]
        public void Test_UInt32_2()
        {
            Int16[] Regs = new short[2];
            Regs[0] = 1; Regs[1] = 0;
            Converts.Int32Endian = Global.eEndian.Little;
            string res = CValueToString.Int16RegistersToValueString(Regs, Global.eFormat.Int, Global.eFormat.UInt, 32);
            Assert.AreEqual<string>("1", res);

            Converts.Int32Endian = Global.eEndian.Big;
            res = CValueToString.Int16RegistersToValueString(Regs, Global.eFormat.Int, Global.eFormat.UInt, 32);
            Assert.AreEqual<string>("65536", res);
        }

        [TestMethod]
        public void Test_UInt32_3()
        {
            Int16[] Regs = new short[2];
            Regs[0] = 0; Regs[1] = 1;
            Converts.Int32Endian = Global.eEndian.Little;
            string res = CValueToString.Int16RegistersToValueString(Regs, Global.eFormat.UInt, Global.eFormat.UInt, 32);
            Assert.AreEqual<string>("65536", res);

            Converts.Int32Endian = Global.eEndian.Big;
            res = CValueToString.Int16RegistersToValueString(Regs, Global.eFormat.UInt, Global.eFormat.UInt, 32);
            Assert.AreEqual<string>("1", res);
        }

        [TestMethod]
        public void Test_UInt32_4()
        {
            Int16[] Regs = new short[2];
            Regs[0] = 100; Regs[1] = 200;
            Converts.Int32Endian = Global.eEndian.Little;
            string res = CValueToString.Int16RegistersToValueString(Regs, Global.eFormat.UInt, Global.eFormat.UInt, 32);
            Assert.AreEqual<string>("13107300", res);

            Converts.Int32Endian = Global.eEndian.Big;
            res = CValueToString.Int16RegistersToValueString(Regs, Global.eFormat.UInt, Global.eFormat.UInt, 32);
            Assert.AreEqual<string>("6553800", res);
        }

        [TestMethod]
        public void Test_UInt32_5()
        {
            Int16[] Regs = new short[2];
            Regs[0] = 20000; Regs[1] = 21000;
            Converts.Int32Endian = Global.eEndian.Little;
            string res = CValueToString.Int16RegistersToValueString(Regs, Global.eFormat.UInt, Global.eFormat.UInt, 32);
            Assert.AreEqual<string>("1376276000", res);

            Converts.Int32Endian = Global.eEndian.Big;
            res = CValueToString.Int16RegistersToValueString(Regs, Global.eFormat.UInt, Global.eFormat.UInt, 32);
            Assert.AreEqual<string>("1310741000", res);
        }

        [TestMethod]
        public void Test_UInt32_6()
        {
            Int16[] Regs = new short[2];
            Regs[0] = Helpers.ConvToInt16(50000); Regs[1] = 21000;
            Converts.Int32Endian = Global.eEndian.Little;
            string res = CValueToString.Int16RegistersToValueString(Regs, Global.eFormat.UInt, Global.eFormat.UInt, 32);
            Assert.AreEqual<string>("1376306000", res);

            Converts.Int32Endian = Global.eEndian.Big;
            res = CValueToString.Int16RegistersToValueString(Regs, Global.eFormat.UInt, Global.eFormat.UInt, 32);
            Assert.AreEqual<string>("3276821000", res);
        }

        [TestMethod]
        public void Test_UInt32_7()
        {
            Int16[] Regs = new short[2];
            Regs[0] = 20000; Regs[1] = Helpers.ConvToInt16(51000);
            Converts.Int32Endian = Global.eEndian.Little;
            string res = CValueToString.Int16RegistersToValueString(Regs, Global.eFormat.UInt, Global.eFormat.UInt, 32);
            Assert.AreEqual<string>("3342356000", res);

            Converts.Int32Endian = Global.eEndian.Big;
            res = CValueToString.Int16RegistersToValueString(Regs, Global.eFormat.UInt, Global.eFormat.UInt, 32);
            Assert.AreEqual<string>("1310771000", res);
        }

        [TestMethod]
        public void Test_UInt32_8()
        {
            Int16[] Regs = new short[2];
            Regs[0] = Helpers.ConvToInt16(50000); Regs[1] = Helpers.ConvToInt16(51000);
            Converts.Int32Endian = Global.eEndian.Little;
            string res = CValueToString.Int16RegistersToValueString(Regs, Global.eFormat.UInt, Global.eFormat.UInt, 32);
            Assert.AreEqual<string>("3342386000", res);

            Converts.Int32Endian = Global.eEndian.Big;
            res = CValueToString.Int16RegistersToValueString(Regs, Global.eFormat.UInt, Global.eFormat.UInt, 32);
            Assert.AreEqual<string>("3276851000", res);
        }

        [TestMethod]
        public void Test_UInt32_d_1()
        {
            Int16[] Regs = new short[2];
            Regs[0] = 12345; Regs[1] = 21000;
            Converts.Int32Endian = Global.eEndian.Little;
            string res = CValueToString.Int16RegistersToValueString(Regs, Global.eFormat.UInt, Global.eFormat.UInt, 32, 3);
            Assert.AreEqual<string>("1376268,345", res);
        }


        [TestMethod]
        public void Test_UInt32_d_2()
        {
            Int16[] Regs = new short[2];
            Regs[0] = Helpers.ConvToInt16(50000); Regs[1] = Helpers.ConvToInt16(51000);
            Converts.Int32Endian = Global.eEndian.Little;
            string res = CValueToString.Int16RegistersToValueString(Regs, Global.eFormat.UInt, Global.eFormat.UInt, 32, 3);
            Assert.AreEqual<string>("3342386,000", res);
        }

        [TestMethod]
        public void Test_UInt32_m_1()
        {
            Int16[] Regs = new short[2];
            Regs[0] = 20000; Regs[1] = 0;
            Converts.Int32Endian = Global.eEndian.Little;
            string res = CValueToString.Int16RegistersToValueString(Regs, Global.eFormat.UInt, Global.eFormat.UInt, 32, 0, 3);
            Assert.AreEqual<string>("20000", res);
        }

        [TestMethod]
        public void Test_UInt32_m_2()
        {
            Int16[] Regs = new short[2];
            Regs[0] = 20000; Regs[1] = 0;
            Converts.Int32Endian = Global.eEndian.Little;
            string res = CValueToString.Int16RegistersToValueString(Regs, Global.eFormat.UInt, Global.eFormat.UInt, 32, 0, 8);
            Assert.AreEqual<string>("00020000", res);
        }

        [TestMethod]
        public void Test_UInt32_m_3()
        {
            Int16[] Regs = new short[2];
            Regs[0] = Helpers.ConvToInt16(50000); Regs[1] = Helpers.ConvToInt16(51000);
            Converts.Int32Endian = Global.eEndian.Little;
            string res = CValueToString.Int16RegistersToValueString(Regs, Global.eFormat.UInt, Global.eFormat.UInt, 32, 0, 15);
            Assert.AreEqual<string>("000003342386000", res);
        }

        [TestMethod]
        public void Test_Bin32_1()
        {
            Int16[] Regs = new short[2];
            Regs[0] = 0; Regs[1] = 0;
            Converts.Int32Endian = Global.eEndian.Little;
            string res = CValueToString.Int16RegistersToValueString(Regs, Global.eFormat.UInt, Global.eFormat.Bin, 32);
            Assert.AreEqual<string>("0b0", res);

            Converts.Int32Endian = Global.eEndian.Big;
            res = CValueToString.Int16RegistersToValueString(Regs, Global.eFormat.UInt, Global.eFormat.Bin, 32);
            Assert.AreEqual<string>("0b0", res);
        }

        [TestMethod]
        public void Test_Bin32_2()
        {
            Int16[] Regs = new short[2];
            Regs[0] = 100; Regs[1] = 200;
            Converts.Int32Endian = Global.eEndian.Little;
            string res = CValueToString.Int16RegistersToValueString(Regs, Global.eFormat.UInt, Global.eFormat.Bin, 32);
            Assert.AreEqual<string>("0b1100.1000.0000.0000.0110.0100", res);

            Converts.Int32Endian = Global.eEndian.Big;
            res = CValueToString.Int16RegistersToValueString(Regs, Global.eFormat.UInt, Global.eFormat.Bin, 32);
            Assert.AreEqual<string>("0b110.0100.0000.0000.1100.1000", res);
        }

        [TestMethod]
        public void Test_Bin32_3()
        {
            Int16[] Regs = new short[2];
            Regs[0] = Helpers.ConvToInt16(34345); Regs[1] = Helpers.ConvToInt16(51000);
            Converts.Int32Endian = Global.eEndian.Little;
            string res = CValueToString.Int16RegistersToValueString(Regs, Global.eFormat.UInt, Global.eFormat.Bin, 32);
            Assert.AreEqual<string>("0b1100.0111.0011.1000.1000.0110.0010.1001", res);

            Converts.Int32Endian = Global.eEndian.Big;
            res = CValueToString.Int16RegistersToValueString(Regs, Global.eFormat.UInt, Global.eFormat.Bin, 32);
            Assert.AreEqual<string>("0b1000.0110.0010.1001.1100.0111.0011.1000", res);
        }

        [TestMethod]
        public void Test_Bin32_m_1()
        {
            Int16[] Regs = new short[2];
            Regs[0] = 100; Regs[1] = 200;
            Converts.Int32Endian = Global.eEndian.Little;
            string res = CValueToString.Int16RegistersToValueString(Regs, Global.eFormat.UInt, Global.eFormat.Bin, 32, 0, 10);
            Assert.AreEqual<string>("0b1100.1000.0000.0000.0110.0100", res);
        }

        [TestMethod]
        public void Test_Bin32_m_2()
        {
            Int16[] Regs = new short[2];
            Regs[0] = 100; Regs[1] = 200;
            Converts.Int32Endian = Global.eEndian.Little;
            string res = CValueToString.Int16RegistersToValueString(Regs, Global.eFormat.UInt, Global.eFormat.Bin, 32, 0, 32);
            Assert.AreEqual<string>("0b0000.0000.1100.1000.0000.0000.0110.0100", res);
        }

        [TestMethod]
        public void Test_Hex32_1()
        {
            Int16[] Regs = new short[2];
            Regs[0] = 0; Regs[1] = 0;
            Converts.Int32Endian = Global.eEndian.Little;
            string res = CValueToString.Int16RegistersToValueString(Regs, Global.eFormat.Int, Global.eFormat.Hex, 32);
            Assert.AreEqual<string>("0x0", res);

            Converts.Int32Endian = Global.eEndian.Big;
            res = CValueToString.Int16RegistersToValueString(Regs, Global.eFormat.Int, Global.eFormat.Hex, 32);
            Assert.AreEqual<string>("0x0", res);
        }

        [TestMethod]
        public void Test_Hex32_2()
        {
            Int16[] Regs = new short[2];
            Regs[0] = 100; Regs[1] = 200;
            Converts.Int32Endian = Global.eEndian.Little;
            string res = CValueToString.Int16RegistersToValueString(Regs, Global.eFormat.Int, Global.eFormat.Hex, 32);
            Assert.AreEqual<string>("0xC80064", res);

            Converts.Int32Endian = Global.eEndian.Big;
            res = CValueToString.Int16RegistersToValueString(Regs, Global.eFormat.Int, Global.eFormat.Hex, 32);
            Assert.AreEqual<string>("0x6400C8", res);
        }

        [TestMethod]
        public void Test_Hex32_3()
        {
            Int16[] Regs = new short[2];
            Regs[0] = Helpers.ConvToInt16(50000); Regs[1] = Helpers.ConvToInt16(21000);
            Converts.Int32Endian = Global.eEndian.Little;
            string res = CValueToString.Int16RegistersToValueString(Regs, Global.eFormat.Int, Global.eFormat.Hex, 32);
            Assert.AreEqual<string>("0x5208C350", res);

            Converts.Int32Endian = Global.eEndian.Big;
            res = CValueToString.Int16RegistersToValueString(Regs, Global.eFormat.Int, Global.eFormat.Hex, 32);
            Assert.AreEqual<string>("0xC3505208", res);
        }

        [TestMethod]
        public void Test_Hex32_4()
        {
            Int16[] Regs = new short[2];
            Regs[0] = Helpers.ConvToInt16(21000); Regs[1] = Helpers.ConvToInt16(50000);
            Converts.Int32Endian = Global.eEndian.Little;
            string res = CValueToString.Int16RegistersToValueString(Regs, Global.eFormat.Hex, Global.eFormat.Hex, 32, 0, 8);
            Assert.AreEqual<string>("0xC3505208", res);

            Converts.Int32Endian = Global.eEndian.Big;
            res = CValueToString.Int16RegistersToValueString(Regs, Global.eFormat.Hex, Global.eFormat.Hex, 32);
            Assert.AreEqual<string>("0x5208C350", res);
        }

        [TestMethod]
        public void Test_Hex32_m_1()
        {
            Int16[] Regs = new short[2];
            Regs[0] = 100; Regs[1] = 200;
            Converts.Int32Endian = Global.eEndian.Little;
            string res = CValueToString.Int16RegistersToValueString(Regs, Global.eFormat.Hex, Global.eFormat.Hex, 32, 0, 4);
            Assert.AreEqual<string>("0xC80064", res);
        }

        [TestMethod]
        public void Test_Hex32_m_2()
        {
            Int16[] Regs = new short[2];
            Regs[0] = 100; Regs[1] = 200;
            Converts.Int32Endian = Global.eEndian.Little;
            string res = CValueToString.Int16RegistersToValueString(Regs, Global.eFormat.Hex, Global.eFormat.Hex, 32, 0, 8);
            Assert.AreEqual<string>("0x00C80064", res);
        }

        [TestMethod]
        public void Test_Float32_1()
        {
            Int16[] Regs = new short[2];
            Regs[0] = 0; Regs[1] = 0;
            Converts.Float32Endian = Global.eEndian.Little;
            string res = CValueToString.Int16RegistersToValueString(Regs, Global.eFormat.Hex, Global.eFormat.Float, 32);
            Assert.AreEqual<string>("0", res);

            Converts.Float32Endian = Global.eEndian.Big;
            res = CValueToString.Int16RegistersToValueString(Regs, Global.eFormat.Hex, Global.eFormat.Float, 32);
            Assert.AreEqual<string>("0", res);
        }

        [TestMethod]
        public void Test_Float32_2()
        {
            Int16[] Regs = new short[2];
            Regs[0] = 0; Regs[1] = 0x42f6;
            Converts.Float32Endian = Global.eEndian.Little;
            string res = CValueToString.Int16RegistersToValueString(Regs, Global.eFormat.Float, Global.eFormat.Float, 32);
            Assert.AreEqual<string>("123", res);

            Regs[0] = 0x42f6; Regs[1] = 0;
            Converts.Float32Endian = Global.eEndian.Big;
            res = CValueToString.Int16RegistersToValueString(Regs, Global.eFormat.Float, Global.eFormat.Float, 32);
            Assert.AreEqual<string>("123", res);
        }

        [TestMethod]
        public void Test_Float32_3()
        {
            Int16[] Regs = new short[2];
            Regs[0] = 0x0e56; Regs[1] = 0x4049;
            Converts.Float32Endian = Global.eEndian.Little;
            string res = CValueToString.Int16RegistersToValueString(Regs, Global.eFormat.Float, Global.eFormat.Float, 32);
            Assert.AreEqual<string>("3,1415", res);

            Regs[0] = 0x4049; Regs[1] = 0x0e56;
            Converts.Float32Endian = Global.eEndian.Big;
            res = CValueToString.Int16RegistersToValueString(Regs, Global.eFormat.Float, Global.eFormat.Float, 32);
            Assert.AreEqual<string>("3,1415", res);
        }

        [TestMethod]
        public void Test_Float32_4()
        {
            Int16[] Regs = new short[2];
            Regs[0] = Helpers.ConvToInt16(0xda66); Regs[1] = 0x466c;
            Converts.Float32Endian = Global.eEndian.Little;
            string res = CValueToString.Int16RegistersToValueString(Regs, Global.eFormat.Float, Global.eFormat.Float, 32);
            Assert.AreEqual<string>("15158,6", res);

            Regs[0] = 0x466c; Regs[1] = Helpers.ConvToInt16(0xda66);
            Converts.Float32Endian = Global.eEndian.Big;
            res = CValueToString.Int16RegistersToValueString(Regs, Global.eFormat.Float, Global.eFormat.Float, 32);
            Assert.AreEqual<string>("15158,6", res);
        }

        [TestMethod]
        public void Test_Float32_5()
        {
            Int16[] Regs = new short[2];
            Regs[0] = Helpers.ConvToInt16(0xe4cd); Regs[1] = Helpers.ConvToInt16(0xc497);
            Converts.Float32Endian = Global.eEndian.Little;
            string res = CValueToString.Int16RegistersToValueString(Regs, Global.eFormat.Float, Global.eFormat.Float, 32);
            Assert.AreEqual<string>("-1215,15", res);

            Regs[0] = Helpers.ConvToInt16(0xc497); ; Regs[1] = Helpers.ConvToInt16(0xe4cd);
            Converts.Float32Endian = Global.eEndian.Big;
            res = CValueToString.Int16RegistersToValueString(Regs, Global.eFormat.Float, Global.eFormat.Float, 32);
            Assert.AreEqual<string>("-1215,15", res);
        }

        [TestMethod]
        public void Test_Float32_6()
        {
            Int16[] Regs = new short[2];
            Regs[0] = Helpers.ConvToInt16(0xe4cd); Regs[1] = Helpers.ConvToInt16(0xc497);
            Converts.Float32Endian = Global.eEndian.Little;
            string res;
            Assert.ThrowsException<ArgumentException>(() =>
                res = CValueToString.Int16RegistersToValueString(Regs, Global.eFormat.Float, Global.eFormat.Float, 16)
                );
        }

        [TestMethod]
        public void Test_Ascii16_1()
        {
            Int16[] Regs = new short[2];
            Regs[0] = 0;
            Converts.StringEndian = Global.eEndian.Big;
            string res = CValueToString.Int16RegistersToValueString(Regs, Global.eFormat.Int, Global.eFormat.ASCII, 16);
            Assert.AreEqual<string>(@"""\x00\x00""", res);

            Converts.StringEndian = Global.eEndian.Little;
            res = CValueToString.Int16RegistersToValueString(Regs, Global.eFormat.Int, Global.eFormat.ASCII, 16);
            Assert.AreEqual<string>(@"""\x00\x00""", res);
        }

        [TestMethod]
        public void Test_Ascii16_2()
        {
            Int16[] Regs = new short[2];
            Regs[0] = 0x4101;
            Converts.StringEndian = Global.eEndian.Big;
            string res = CValueToString.Int16RegistersToValueString(Regs, Global.eFormat.Int, Global.eFormat.ASCII, 16);
            Assert.AreEqual<string>(@"""A\x01""", res);

            Converts.StringEndian = Global.eEndian.Little;
            res = CValueToString.Int16RegistersToValueString(Regs, Global.eFormat.Int, Global.eFormat.ASCII, 16);
            Assert.AreEqual<string>(@"""\x01A""", res);
        }

        [TestMethod]
        public void Test_Ascii16_3()
        {
            Int16[] Regs = new short[2];
            Regs[0] = Helpers.ConvToInt16(0x8120);
            Converts.StringEndian = Global.eEndian.Big;
            string res = CValueToString.Int16RegistersToValueString(Regs, Global.eFormat.Int, Global.eFormat.ASCII, 16);
            Assert.AreEqual<string>(@"""\x81 """, res);

            Converts.StringEndian = Global.eEndian.Little;
            res = CValueToString.Int16RegistersToValueString(Regs, Global.eFormat.Int, Global.eFormat.ASCII, 16);
            Assert.AreEqual<string>(@""" \x81""", res);
        }

        [TestMethod]
        public void Test_Ascii16_4()
        {
            Int16[] Regs = new short[2];
            Regs[0] = 0x7DFF;
            Converts.StringEndian = Global.eEndian.Big;
            string res = CValueToString.Int16RegistersToValueString(Regs, Global.eFormat.Int, Global.eFormat.ASCII, 16);
            Assert.AreEqual<string>(@"""}\xFF""", res);

            Converts.StringEndian = Global.eEndian.Little;
            res = CValueToString.Int16RegistersToValueString(Regs, Global.eFormat.Int, Global.eFormat.ASCII, 16);
            Assert.AreEqual<string>(@"""\xFF}""", res);
        }

        [TestMethod]
        public void Test_Ascii32_1()
        {
            Int16[] Regs = new short[2];
            Regs[0] = 0; Regs[1] = 0;
            Converts.StringEndian = Global.eEndian.Big;
            string res = CValueToString.Int16RegistersToValueString(Regs, Global.eFormat.Int, Global.eFormat.ASCII, 32);
            Assert.AreEqual<string>(@"""\x00\x00\x00\x00""", res);

            Converts.StringEndian = Global.eEndian.Little;
            res = CValueToString.Int16RegistersToValueString(Regs, Global.eFormat.Int, Global.eFormat.ASCII, 32);
            Assert.AreEqual<string>(@"""\x00\x00\x00\x00""", res);
        }

        [TestMethod]
        public void Test_Ascii32_2()
        {
            Int16[] Regs = new short[2];
            Regs[0] = 0x4142; Regs[1] = 0x4344;
            Converts.StringEndian = Global.eEndian.Big;
            string res = CValueToString.Int16RegistersToValueString(Regs, Global.eFormat.Int, Global.eFormat.ASCII, 32);
            Assert.AreEqual<string>(@"""ABCD""", res);

            Converts.StringEndian = Global.eEndian.Little;
            res = CValueToString.Int16RegistersToValueString(Regs, Global.eFormat.Int, Global.eFormat.ASCII, 32);
            Assert.AreEqual<string>(@"""BADC""", res);
        }

        [TestMethod]
        public void Test_Ascii32_3()
        {
            Int16[] Regs = new short[2];
            Regs[0] = 0x3a7f; Regs[1] = Helpers.ConvToInt16(0xb26d);
            Converts.StringEndian = Global.eEndian.Big;
            string res = CValueToString.Int16RegistersToValueString(Regs, Global.eFormat.Int, Global.eFormat.ASCII, 32);
            Assert.AreEqual<string>(@""":\x7F\xB2m""", res);

            Converts.StringEndian = Global.eEndian.Little;
            res = CValueToString.Int16RegistersToValueString(Regs, Global.eFormat.Int, Global.eFormat.ASCII, 32);
            Assert.AreEqual<string>(@"""\x7F:m\xB2""", res);
        }

        [TestMethod]
        public void Test_String_1()
        {
            Converts.StringEncodingName = "us-ascii";
            Int16[] Regs = new short[3];
            Regs[0] = 0x4142; Regs[1] = 0x4344; ; Regs[2] = 0x4500;

            string res;
            Converts.StringEndian = Global.eEndian.Big;
            res = CValueToString.Int16RegistersToValueString(Regs, Global.eFormat.String, Global.eFormat.String, 0);
            Assert.AreEqual<string>(@"""ABCDE""", res);

            Converts.StringEndian = Global.eEndian.Little;
            res = CValueToString.Int16RegistersToValueString(Regs, Global.eFormat.String, Global.eFormat.String, 0);
            Assert.AreEqual<string>(@"""BADC""", res);
        }

        [TestMethod]
        public void Test_String_2()
        {
            Converts.StringEncodingName = "us-ascii";
            Int16[] Regs = new short[3];
            Regs[0] = 0x4142; Regs[1] = 0x4344; ; Regs[2] = 0x4500;

            string res;
            Converts.StringEndian = Global.eEndian.Big;
            res = CValueToString.Int16RegistersToValueString(Regs, Global.eFormat.String, Global.eFormat.Hex, 0);
            Assert.AreEqual<string>("0x4142, 0x4344, 0x4500", res);

            Converts.StringEndian = Global.eEndian.Little;
            res = CValueToString.Int16RegistersToValueString(Regs, Global.eFormat.String, Global.eFormat.Hex, 0);
            Assert.AreEqual<string>("0x4142, 0x4344, 0x4500", res);
        }

        [TestMethod]
        public void Test_String_3()
        {
            Converts.StringEncodingName = "utf-8";
            Int16[] Regs = new short[10];

            string res;
            Converts.StringEndian = Global.eEndian.Big;
            Regs[0] = 0x4162; Regs[1] = 0x63E4; Regs[2] = Helpers.ConvToInt16(0xBDA0); Regs[3] = Helpers.ConvToInt16(0xE5A5); Regs[4] = Helpers.ConvToInt16(0xBD00);
            res = CValueToString.Int16RegistersToValueString(Regs, Global.eFormat.String, Global.eFormat.String, 0);
            Assert.AreEqual<string>(@"""Abc你好""", res);

            Regs[0] = 0x6241; Regs[1] = Helpers.ConvToInt16(0xE463); Regs[2] = Helpers.ConvToInt16(0xA0BD); Regs[3] = Helpers.ConvToInt16(0xA5E5); Regs[4] = Helpers.ConvToInt16(0x00BD);
            Converts.StringEndian = Global.eEndian.Little;
            res = CValueToString.Int16RegistersToValueString(Regs, Global.eFormat.String, Global.eFormat.String, 0);
            Assert.AreEqual<string>(@"""Abc你好""", res);
        }

        [TestMethod]
        public void Test_String_4()
        {
            Converts.StringEncodingName = "utf-8";
            Int16[] Regs = new short[10];

            string res;
            Converts.StringEndian = Global.eEndian.Big;
            Regs[0] = 0x4162; Regs[1] = 0x63E4; Regs[2] = Helpers.ConvToInt16(0xBDA0); Regs[3] = Helpers.ConvToInt16(0xE5A5); Regs[4] = Helpers.ConvToInt16(0xBD00);
            res = CValueToString.Int16RegistersToValueString(Regs, Global.eFormat.String, Global.eFormat.Hex, 0);
            Assert.AreEqual<string>(@"0x4162, 0x63E4, 0xBDA0, 0xE5A5, 0xBD00, 0x0000, 0x0000, 0x0000, 0x0000, 0x0000", res);

            Converts.StringEndian = Global.eEndian.Little;
            res = CValueToString.Int16RegistersToValueString(Regs, Global.eFormat.String, Global.eFormat.Hex, 0);
            Assert.AreEqual<string>(@"0x4162, 0x63E4, 0xBDA0, 0xE5A5, 0xBD00, 0x0000, 0x0000, 0x0000, 0x0000, 0x0000", res);
        }

        [TestMethod]
        public void Test_String_6()
        {
            Converts.StringEncodingName = "utf-8";
            Int16[] Regs = new short[10];

            string res;
            Converts.StringEndian = Global.eEndian.Big;
            Regs[0] = 0x4162; Regs[1] = 0x63E4; Regs[2] = Helpers.ConvToInt16(0xBDA0); Regs[3] = Helpers.ConvToInt16(0xE5A5); Regs[4] = Helpers.ConvToInt16(0xBD00);
            res = CValueToString.Int16RegistersToValueString(Regs, Global.eFormat.String, Global.eFormat.Int, 0);
            Assert.AreEqual<string>("16738, 25572, -16992, -6747, -17152, 0, 0, 0, 0, 0", res);

            Converts.StringEndian = Global.eEndian.Little;
            res = CValueToString.Int16RegistersToValueString(Regs, Global.eFormat.String, Global.eFormat.Int, 0);
            Assert.AreEqual<string>("16738, 25572, -16992, -6747, -17152, 0, 0, 0, 0, 0", res);
        }

        [TestMethod]
        public void Test_String_7()
        {
            Converts.StringEncodingName = "utf-8";
            Int16[] Regs = new short[10];

            string res;
            Converts.StringEndian = Global.eEndian.Big;
            Regs[0] = 0x4162; Regs[1] = 0x63E4; Regs[2] = Helpers.ConvToInt16(0xBDA0); Regs[3] = Helpers.ConvToInt16(0xE5A5); Regs[4] = Helpers.ConvToInt16(0xBD00);
            res = CValueToString.Int16RegistersToValueString(Regs, Global.eFormat.String, Global.eFormat.UInt, 0);
            Assert.AreEqual<string>("16738, 25572, 48544, 58789, 48384, 0, 0, 0, 0, 0", res);

            Converts.StringEndian = Global.eEndian.Little;
            res = CValueToString.Int16RegistersToValueString(Regs, Global.eFormat.String, Global.eFormat.UInt, 0);
            Assert.AreEqual<string>("16738, 25572, 48544, 58789, 48384, 0, 0, 0, 0, 0", res);
        }
    }

    [TestClass]
    public class Test_IntValueToValueString
    {
        // public static string IntValueToValueString(Int32 Value, int Width, Global.eFormat Format, int Decimals = 0, int MinLen = 0, ModbusControlSpace.Global.CEnum EnumDict = null)

        [TestMethod]
        public void Test_Int16_1()
        {
            Converts.Int32Endian = Global.eEndian.Little;
            string res = CValueToString.IntValueToValueString(0, 16, Global.eFormat.Int);
            Assert.AreEqual<string>("0", res);
        }

        [TestMethod]
        public void Test_Int16_2()
        {
            Converts.Int32Endian = Global.eEndian.Little;
            string res = CValueToString.IntValueToValueString(255, 16, Global.eFormat.Int);
            Assert.AreEqual<string>("255", res);
        }

        [TestMethod]
        public void Test_Int16_3()
        {
            Converts.Int32Endian = Global.eEndian.Little;
            string res = CValueToString.IntValueToValueString(32000, 16, Global.eFormat.Int);
            Assert.AreEqual<string>("32000", res);
        }

        [TestMethod]
        public void Test_Int16_4()
        {
            Converts.Int32Endian = Global.eEndian.Big;
            string res = CValueToString.IntValueToValueString(50000, 16, Global.eFormat.Int);
            Assert.AreEqual<string>("-15536", res);
        }

        [TestMethod]
        public void Test_Int16_5()
        {
            Converts.Int32Endian = Global.eEndian.Little;
            string res = CValueToString.IntValueToValueString(-18000, 16, Global.eFormat.Int);
            Assert.AreEqual<string>("-18000", res);
        }

        [TestMethod]
        public void Test_Int16_d_1()
        {
            Converts.Int32Endian = Global.eEndian.Little;
            string res = CValueToString.IntValueToValueString(12345, 16, Global.eFormat.Int);
            Assert.AreEqual<string>("12345", res);
        }

        [TestMethod]
        public void Test_Int16_d_2()
        {
            Converts.Int32Endian = Global.eEndian.Little;
            string res = CValueToString.IntValueToValueString(12345, 16, Global.eFormat.Int, 3);
            Assert.AreEqual<string>("12,345", res);
        }

        [TestMethod]
        public void Test_UInt16_d_3()
        {
            Converts.Int32Endian = Global.eEndian.Little;
            string res = CValueToString.IntValueToValueString(40000, 16, Global.eFormat.UInt, 6);
            Assert.AreEqual<string>("0,040000", res);
        }

        [TestMethod]
        public void Test_UInt16_m_1()
        {
            Converts.Int32Endian = Global.eEndian.Little;
            string res = CValueToString.IntValueToValueString(148, 16, Global.eFormat.UInt, 0, 4);
            Assert.AreEqual<string>("0148", res);
        }

        [TestMethod]
        public void Test_Hex16_1()
        {
            Converts.Int32Endian = Global.eEndian.Little;
            string res = CValueToString.IntValueToValueString(0, 16, Global.eFormat.Hex);
            Assert.AreEqual<string>("0x0", res);
        }

        [TestMethod]
        public void Test_Hex16_2()
        {
            Converts.Int32Endian = Global.eEndian.Little;
            string res = CValueToString.IntValueToValueString(-1, 16, Global.eFormat.Hex);
            Assert.AreEqual<string>("0xFFFF", res);
        }

        [TestMethod]
        public void Test_Hex16_m_1()
        {
            Converts.Int32Endian = Global.eEndian.Little;
            string res = CValueToString.IntValueToValueString(-1, 16, Global.eFormat.Hex);
            Assert.AreEqual<string>("0xFFFF", res);
        }

        [TestMethod]
        public void Test_Hex16_m_2()
        {
            Converts.Int32Endian = Global.eEndian.Little;
            string res = CValueToString.IntValueToValueString(156, 16, Global.eFormat.Hex, 0, 4);
            Assert.AreEqual<string>("0x009C", res);
        }

        [TestMethod]
        public void Test_Bin16_1()
        {
            Converts.Int32Endian = Global.eEndian.Little;
            string res = CValueToString.IntValueToValueString(0, 16, Global.eFormat.Bin);
            Assert.AreEqual<string>("0b0", res);
        }

        [TestMethod]
        public void Test_Bin16_2()
        {
            Converts.Int32Endian = Global.eEndian.Little;
            string res = CValueToString.IntValueToValueString(38, 16, Global.eFormat.Bin);
            Assert.AreEqual<string>("0b10.0110", res);
        }

        [TestMethod]
        public void Test_Bin16_3()
        {
            Converts.Int32Endian = Global.eEndian.Little;
            string res = CValueToString.IntValueToValueString(-1, 16, Global.eFormat.Bin);
            Assert.AreEqual<string>("0b1111.1111.1111.1111", res);
        }

        [TestMethod]
        public void Test_Bin16_m_1()
        {
            Converts.Int32Endian = Global.eEndian.Little;
            string res = CValueToString.IntValueToValueString(1, 16, Global.eFormat.Bin, 0, 12);
            Assert.AreEqual<string>("0b0000.0000.0001", res);
        }

        [TestMethod]
        public void Test_Bin16_m_2()
        {
            Converts.Int32Endian = Global.eEndian.Little;
            string res = CValueToString.IntValueToValueString(38, 16, Global.eFormat.Bin, 0, 12);
            Assert.AreEqual<string>("0b0000.0010.0110", res);
        }

        [TestMethod]
        public void Test_Int32_1()
        {
            string res = CValueToString.IntValueToValueString(0, 32, Global.eFormat.Int);
            Assert.AreEqual<string>("0", res);
        }

        [TestMethod]
        public void Test_Int32_2()
        {
            string res = CValueToString.IntValueToValueString(256, 32, Global.eFormat.Int);
            Assert.AreEqual<string>("256", res);
        }

        [TestMethod]
        public void Test_Int32_2b()
        {
            Converts.Int32Endian = Global.eEndian.Little;
            string res = CValueToString.IntValueToValueString(1000, 32, Global.eFormat.Int);
            Assert.AreEqual<string>("1000", res);

            Converts.Int32Endian = Global.eEndian.Big;
            res = CValueToString.IntValueToValueString(1000, 32, Global.eFormat.Int);
            Assert.AreEqual<string>("1000", res);
        }

        [TestMethod]
        public void Test_Int32_2c()
        {
            Converts.Int32Endian = Global.eEndian.Little;
            string res = CValueToString.IntValueToValueString(1000000000, 32, Global.eFormat.Int);
            Assert.AreEqual<string>("1000000000", res);

            Converts.Int32Endian = Global.eEndian.Big;
            res = CValueToString.IntValueToValueString(1500000000, 32, Global.eFormat.Int);
            Assert.AreEqual<string>("1500000000", res);
        }

        [TestMethod]
        public void Test_Int32_3()
        {
            string res = CValueToString.IntValueToValueString(51000, 32, Global.eFormat.Int);
            Assert.AreEqual<string>("51000", res);
        }

        [TestMethod]
        public void Test_Int32_4()
        {
            string res = CValueToString.IntValueToValueString(-1000111222, 32, Global.eFormat.Int);
            Assert.AreEqual<string>("-1000111222", res);
        }

        [TestMethod]
        public void Test_Int32_5()
        {
            string res = CValueToString.IntValueToValueString(-1, 32, Global.eFormat.Int);
            Assert.AreEqual<string>("-1", res);
        }

        [TestMethod]
        public void Test_Int32_d_1()
        {
            string res = CValueToString.IntValueToValueString(51000, 32, Global.eFormat.Int, 2);
            Assert.AreEqual<string>("510,00", res);
        }

        [TestMethod]
        public void Test_Int32_d_2()
        {
            string res = CValueToString.IntValueToValueString(2111222333, 32, Global.eFormat.Int, 6);
            Assert.AreEqual<string>("2111,222333", res);
        }

        [TestMethod]
        public void Test_Int32_d_3()
        {
            string res = CValueToString.IntValueToValueString(-2111222333, 32, Global.eFormat.Int, 6);
            Assert.AreEqual<string>("-2111,222333", res);
        }

        [TestMethod]
        public void Test_Int32_m_1()
        {
            string res = CValueToString.IntValueToValueString(567, 32, Global.eFormat.Int, 0, 4);
            Assert.AreEqual<string>("0567", res);
        }

        [TestMethod]
        public void Test_Int32_m_2()
        {
            string res = CValueToString.IntValueToValueString(-7894, 32, Global.eFormat.Int, 0, 8);
            Assert.AreEqual<string>("-00007894", res);
        }

        [TestMethod]
        public void Test_UInt32_1()
        {
            string res = CValueToString.IntValueToValueString(0, 32, Global.eFormat.UInt);
            Assert.AreEqual<string>("0", res);
        }

        [TestMethod]
        public void Test_UInt32_2()
        {
            string res = CValueToString.IntValueToValueString(257, 32, Global.eFormat.UInt);
            Assert.AreEqual<string>("257", res);
        }

        [TestMethod]
        public void Test_UInt32_2b()
        {
            Converts.Int32Endian = Global.eEndian.Little;
            string res = CValueToString.IntValueToValueString(2000, 32, Global.eFormat.UInt);
            Assert.AreEqual<string>("2000", res);

            Converts.Int32Endian = Global.eEndian.Big;
            res = CValueToString.IntValueToValueString(2000, 32, Global.eFormat.UInt);
            Assert.AreEqual<string>("2000", res);
        }

        [TestMethod]
        public void Test_UInt32_2c()
        {
            Converts.Int32Endian = Global.eEndian.Little;
            string res = CValueToString.IntValueToValueString(1000000000, 32, Global.eFormat.UInt);
            Assert.AreEqual<string>("1000000000", res);

            Converts.Int32Endian = Global.eEndian.Big;
            res = CValueToString.IntValueToValueString(1500000000, 32, Global.eFormat.UInt);
            Assert.AreEqual<string>("1500000000", res);
        }

        [TestMethod]
        public void Test_UInt32_3()
        {
            string res = CValueToString.IntValueToValueString(51000, 32, Global.eFormat.UInt);
            Assert.AreEqual<string>("51000", res);
        }

        [TestMethod]
        public void Test_UInt32_4()
        {
            string res = CValueToString.IntValueToValueString(Helpers.ConvToInt32(3111222333), 32, Global.eFormat.UInt);
            Assert.AreEqual<string>("3111222333", res);
        }

        [TestMethod]
        public void Test_UInt32_5()
        {
            string res = CValueToString.IntValueToValueString(-1, 32, Global.eFormat.UInt);
            Assert.AreEqual<string>("4294967295", res);
        }

        [TestMethod]
        public void Test_UInt32_d_1()
        {
            string res = CValueToString.IntValueToValueString(51000, 32, Global.eFormat.UInt, 2);
            Assert.AreEqual<string>("510,00", res);
        }

        [TestMethod]
        public void Test_UInt32_d_2()
        {
            string res = CValueToString.IntValueToValueString(Helpers.ConvToInt32(3111222333), 32, Global.eFormat.UInt, 6);
            Assert.AreEqual<string>("3111,222333", res);
        }

        [TestMethod]
        public void Test_UInt32_m_1()
        {
            string res = CValueToString.IntValueToValueString(567, 32, Global.eFormat.UInt, 0, 4);
            Assert.AreEqual<string>("0567", res);
        }

        [TestMethod]
        public void Test_UInt32_m_2()
        {
            string res = CValueToString.IntValueToValueString(Helpers.ConvToInt32(3111222333), 32, Global.eFormat.UInt, 0, 8);
            Assert.AreEqual<string>("3111222333", res);
        }

        [TestMethod]
        public void Test_Ascii_1()
        {
            Converts.StringEndian = Global.eEndian.Big;
            string res = CValueToString.IntValueToValueString(0, 8, Global.eFormat.ASCII);
            Assert.AreEqual<string>(@"""\x00""", res);
        }

        [TestMethod]
        public void Test_Ascii_2()
        {
            Converts.StringEndian = Global.eEndian.Big;
            string res = CValueToString.IntValueToValueString(0, 9, Global.eFormat.ASCII);
            Assert.AreEqual<string>(@"""\x00\x00""", res);
        }

        [TestMethod]
        public void Test_Ascii_3()
        {
            Converts.StringEndian = Global.eEndian.Big; // no effect
            string res = CValueToString.IntValueToValueString(0x41, 8, Global.eFormat.ASCII);
            Assert.AreEqual<string>(@"""A""", res);

            Converts.StringEndian = Global.eEndian.Little; // no effect
            res = CValueToString.IntValueToValueString(0x41, 8, Global.eFormat.ASCII);
            Assert.AreEqual<string>(@"""A""", res);
        }

        [TestMethod]
        public void Test_Ascii_4()
        {
            Converts.StringEndian = Global.eEndian.Big; // no effect
            string res = CValueToString.IntValueToValueString(0x0141, 9, Global.eFormat.ASCII);
            Assert.AreEqual<string>(@"""A\x01""", res);
        }

        [TestMethod]
        public void Test_Ascii_5()
        {
            Converts.StringEndian = Global.eEndian.Big; // no effect
            string res = CValueToString.IntValueToValueString(0x4241, 10, Global.eFormat.ASCII);
            Assert.AreEqual<string>(@"""AB""", res);
        }

        [TestMethod]
        public void Test_Ascii_10()
        {
            Converts.StringEndian = Global.eEndian.Big; // no effect
            string res = CValueToString.IntValueToValueString(0, 16, Global.eFormat.ASCII);
            Assert.AreEqual<string>(@"""\x00\x00""", res);
        }

        [TestMethod]
        public void Test_Ascii_11()
        {
            Converts.StringEndian = Global.eEndian.Big; // no effect
            string res = CValueToString.IntValueToValueString(0x4241, 16, Global.eFormat.ASCII);
            Assert.AreEqual<string>(@"""AB""", res);

            Converts.StringEndian = Global.eEndian.Little; // no effect
            res = CValueToString.IntValueToValueString(0x4241, 16, Global.eFormat.ASCII);
            Assert.AreEqual<string>(@"""AB""", res);
        }

        [TestMethod]
        public void Test_Ascii_12()
        {
            Converts.StringEndian = Global.eEndian.Big; // no effect
            string res = CValueToString.IntValueToValueString(0x8120, 16, Global.eFormat.ASCII);
            Assert.AreEqual<string>(@""" \x81""", res);
        }

        [TestMethod]
        public void Test_Ascii_13()
        {
            Converts.StringEndian = Global.eEndian.Big; // no effect
            string res = CValueToString.IntValueToValueString(0x7DFF, 16, Global.eFormat.ASCII);
            Assert.AreEqual<string>(@"""\xFF}""", res);
        }

        [TestMethod]
        public void Test_Ascii_20()
        {
            Converts.StringEndian = Global.eEndian.Big; // no effect
            string res = CValueToString.IntValueToValueString(0, 32, Global.eFormat.ASCII);
            Assert.AreEqual<string>(@"""\x00\x00\x00\x00""", res);
        }

        [TestMethod]
        public void Test_Ascii_21()
        {
            Converts.StringEndian = Global.eEndian.Big; // no effect
            string res = CValueToString.IntValueToValueString(0x44434241, 32, Global.eFormat.ASCII);
            Assert.AreEqual<string>(@"""ABCD""", res);

            Converts.StringEndian = Global.eEndian.Little; // no effect
            res = CValueToString.IntValueToValueString(0x44434241, 32, Global.eFormat.ASCII);
            Assert.AreEqual<string>(@"""ABCD""", res);
        }

        [TestMethod]
        public void Test_Ascii_22()
        {
            Converts.StringEndian = Global.eEndian.Big; // no effect
            string res = CValueToString.IntValueToValueString(Helpers.ConvToInt32(0xb26d3a7f), 32, Global.eFormat.ASCII);
            Assert.AreEqual<string>(@"""\x7F:m\xB2""", res);
        }

    }

    [TestClass]
    public class Test_StringToInt16Registers
    {
        // Int16[] StringToInt16Registers(string str, int size, bool IsFloat, int Decimals);

        [TestMethod]
        public void Test_Int16_1()
        {
            Converts.Int32Endian = Global.eEndian.Little;
            Int16[] res = CValueToString.ValueStringToInt16Registers("0", 1, 0, true, false, false);
            Assert.AreEqual<int>(1, res.Length);
            Assert.AreEqual<Int16>(0, res[0]);
        }

        [TestMethod]
        public void Test_Int16_1b()
        {
            Converts.Int32Endian = Global.eEndian.Little;
            Int16[] res = CValueToString.ValueStringToInt16Registers(" 1", 1, 0, true, false, false);
            Assert.AreEqual<int>(1, res.Length);
            Assert.AreEqual<Int16>(1, res[0]);
        }

        [TestMethod]
        public void Test_Int16_1c()
        {
            Converts.Int32Endian = Global.eEndian.Little;
            Int16[] res = CValueToString.ValueStringToInt16Registers(" 35 ", 1, 0, true, false, false);
            Assert.AreEqual<int>(1, res.Length);
            Assert.AreEqual<Int16>(35, res[0]);
        }

        [TestMethod]
        public void Test_Int16_2()
        {
            Converts.Int32Endian = Global.eEndian.Little;
            Int16[] res = CValueToString.ValueStringToInt16Registers("32000", 1, 0, true, false, false);
            Assert.AreEqual<int>(1, res.Length);
            Assert.AreEqual<Int16>(32000, res[0]);

            Converts.Int32Endian = Global.eEndian.Big; // no effect
            res = CValueToString.ValueStringToInt16Registers("32000", 1, 0, true, false, false);
            Assert.AreEqual<int>(1, res.Length);
            Assert.AreEqual<Int16>(32000, res[0]);
        }

        [TestMethod]
        public void Test_Int16_3()
        {
            Converts.Int32Endian = Global.eEndian.Little;
            Int16[] res = CValueToString.ValueStringToInt16Registers("50000", 1, 0, true, false, false);
            Assert.AreEqual<int>(1, res.Length);
            Assert.AreEqual<Int16>(-15536, res[0]);
        }

        [TestMethod]
        public void Test_Int16_4()
        {
            Converts.Int32Endian = Global.eEndian.Little;
            Int16[] res = CValueToString.ValueStringToInt16Registers("-1234", 1, 0, true, false, false);
            Assert.AreEqual<int>(1, res.Length);
            Assert.AreEqual<Int16>(-1234, res[0]);
        }

        [TestMethod]
        public void Test_Int16_5()
        {
            Converts.Int32Endian = Global.eEndian.Little;
            Int16[] res = CValueToString.ValueStringToInt16Registers("65535", 1, 0, true, false, false);
            Assert.AreEqual<int>(1, res.Length);
            Assert.AreEqual<Int16>(Helpers.ConvToInt16(65535), res[0]);
        }

        [TestMethod]
        public void Test_Int16_6()
        {
            Converts.Int32Endian = Global.eEndian.Little;
            Int16[] res;
            Assert.ThrowsException<ArgumentException>(() =>
                res = CValueToString.ValueStringToInt16Registers("65536", 1, 0, true, false, false)
                );
        }

        [TestMethod]
        public void Test_Int16_7()
        {
            Converts.Int32Endian = Global.eEndian.Little;
            Int16[] res = CValueToString.ValueStringToInt16Registers("123.45", 1, 2, true, false, false);
            Assert.AreEqual<int>(1, res.Length);
            Assert.AreEqual<Int16>(Helpers.ConvToInt16(12345), res[0]);
        }

        [TestMethod]
        public void Test_Int16_h_1()
        {
            Converts.Int32Endian = Global.eEndian.Little;
            Int16[] res = CValueToString.ValueStringToInt16Registers("0x0", 1, 0, true, false, false);
            Assert.AreEqual<int>(1, res.Length);
            Assert.AreEqual<Int16>(0, res[0]);
        }

        [TestMethod]
        public void Test_Int16_h_2()
        {
            Converts.Int32Endian = Global.eEndian.Little;
            Int16[] res = CValueToString.ValueStringToInt16Registers("0x7151", 1, 0, true, false, false);
            Assert.AreEqual<int>(1, res.Length);
            Assert.AreEqual<Int16>(29009, res[0]);

            Converts.Int32Endian = Global.eEndian.Big; // no effect
            res = CValueToString.ValueStringToInt16Registers("0x7151", 1, 0, true, false, false);
            Assert.AreEqual<int>(1, res.Length);
            Assert.AreEqual<Int16>(29009, res[0]);
        }

        [TestMethod]
        public void Test_Int16_h_3()
        {
            Converts.Int32Endian = Global.eEndian.Little;
            Int16[] res = CValueToString.ValueStringToInt16Registers("0x00ABCD", 1, 0, true, false, false);
            Assert.AreEqual<int>(1, res.Length);
            Assert.AreEqual<Int16>(-21555, res[0]);
        }

        [TestMethod]
        public void Test_Int16_h_4()
        {
            Converts.Int32Endian = Global.eEndian.Little;
            Int16[] res = CValueToString.ValueStringToInt16Registers("0xFFFF", 1, 0, true, false, false);
            Assert.AreEqual<int>(1, res.Length);
            Assert.AreEqual<Int16>(Helpers.ConvToInt16(65535), res[0]);
        }

        [TestMethod]
        public void Test_Int16_h_5()
        {
            Int16[] res;
            Assert.ThrowsException<ArgumentException>(() =>
                res = CValueToString.ValueStringToInt16Registers("0x10000", 1, 0, true, false, false)
                );
        }

        [TestMethod]
        public void Test_Int16_b_1()
        {
            Converts.Int32Endian = Global.eEndian.Little;
            Int16[] res = CValueToString.ValueStringToInt16Registers("0b101011001", 1, 0, true, false, false);
            Assert.AreEqual<int>(1, res.Length);
            Assert.AreEqual<Int16>(345, res[0]);
        }

        [TestMethod]
        public void Test_Int16_b_2()
        {
            Converts.Int32Endian = Global.eEndian.Little;
            Int16[] res = CValueToString.ValueStringToInt16Registers("0b00000000111101", 1, 0, true, false, false);
            Assert.AreEqual<int>(1, res.Length);
            Assert.AreEqual<Int16>(61, res[0]);
        }

        [TestMethod]
        public void Test_Int16_b_3()
        {
            Converts.Int32Endian = Global.eEndian.Little;
            Int16[] res = CValueToString.ValueStringToInt16Registers("0b000", 1, 0, true, false, false);
            Assert.AreEqual<int>(1, res.Length);
            Assert.AreEqual<Int16>(0, res[0]);
        }

        [TestMethod]
        public void Test_Int16_b_4()
        {
            Int16[] res;
            Converts.Int32Endian = Global.eEndian.Little;
            Assert.ThrowsException<ArgumentException>(() =>
                res = CValueToString.ValueStringToInt16Registers("0b10000000000000000", 1, 0, true, false, false)
                );
        }

        [TestMethod]
        public void Test_Int16_ascii_1()
        {
            Converts.StringEndian = Global.eEndian.Big;
            Int16[] res = CValueToString.ValueStringToInt16Registers("\"A\"", 1, 0, true, false, false);
            Assert.AreEqual<int>(1, res.Length);
            Assert.AreEqual<Int16>(0x4100, res[0]);

            Converts.StringEndian = Global.eEndian.Little;
            res = CValueToString.ValueStringToInt16Registers("\"A\"", 1, 0, true, false, false);
            Assert.AreEqual<int>(1, res.Length);
            Assert.AreEqual<Int16>(0x0041, res[0]);
        }

        [TestMethod]
        public void Test_Int16_ascii_2()
        {
            Converts.StringEndian = Global.eEndian.Big;
            Int16[] res = CValueToString.ValueStringToInt16Registers("\"AB\"", 1, 0, true, false, false);
            Assert.AreEqual<int>(1, res.Length);
            Assert.AreEqual<Int16>(0x4142, res[0]);

            Converts.StringEndian = Global.eEndian.Little;
            res = CValueToString.ValueStringToInt16Registers("\"AB\"", 1, 0, true, false, false);
            Assert.AreEqual<int>(1, res.Length);
            Assert.AreEqual<Int16>(0x4241, res[0]);
        }

        [TestMethod]
        public void Test_Int16_ascii_3()
        {
            Converts.StringEndian = Global.eEndian.Big;
            Int16[] res = CValueToString.ValueStringToInt16Registers(@"""A\x81""", 1, 0, true, false, false); // "A\x81"
            Assert.AreEqual<int>(1, res.Length);
            Assert.AreEqual<Int16>(0x4181, res[0]);
        }

        [TestMethod]
        public void Test_Int16_ascii_4()
        {
            Converts.StringEndian = Global.eEndian.Big;
            Int16[] res = CValueToString.ValueStringToInt16Registers(@"""\x01A""", 1, 0, true, false, false); // "\x01A"
            Assert.AreEqual<int>(1, res.Length);
            Assert.AreEqual<Int16>(0x0141, res[0]);
        }

        [TestMethod]
        public void Test_Int16_ascii_5()
        {
            Int16[] res;
            Converts.StringEndian = Global.eEndian.Big;
            Assert.ThrowsException<ArgumentException>(() =>
                res = CValueToString.ValueStringToInt16Registers("ABC", 1, 0, true, false, false)
                );
        }

        [TestMethod]
        public void Test_Int32_1()
        {
            Converts.Int32Endian = Global.eEndian.Little;
            Int16[] res = CValueToString.ValueStringToInt16Registers("0", 2, 0, true, false, false);
            Assert.AreEqual<int>(2, res.Length);
            Assert.AreEqual<Int16>(0, res[0]);
            Assert.AreEqual<Int16>(0, res[1]);

            Converts.Int32Endian = Global.eEndian.Big;
            res = CValueToString.ValueStringToInt16Registers("0", 2, 0, true, false, false);
            Assert.AreEqual<int>(2, res.Length);
            Assert.AreEqual<Int16>(0, res[0]);
            Assert.AreEqual<Int16>(0, res[1]);
        }

        [TestMethod]
        public void Test_Int32_2()
        {
            Converts.Int32Endian = Global.eEndian.Little;
            Int16[] res = CValueToString.ValueStringToInt16Registers("1000", 2, 0, true, false, false);
            Assert.AreEqual<int>(2, res.Length);
            Assert.AreEqual<Int16>(1000, res[0]);
            Assert.AreEqual<Int16>(0, res[1]);

            Converts.Int32Endian = Global.eEndian.Big;
            res = CValueToString.ValueStringToInt16Registers("1000", 2, 0, true, false, false);
            Assert.AreEqual<int>(2, res.Length);
            Assert.AreEqual<Int16>(0, res[0]);
            Assert.AreEqual<Int16>(1000, res[1]);
        }

        [TestMethod]
        public void Test_Int32_3()
        {
            Converts.Int32Endian = Global.eEndian.Little;
            Int16[] res = CValueToString.ValueStringToInt16Registers("2111222333", 2, 0, true, false, false);
            Assert.AreEqual<int>(2, res.Length);
            Assert.AreEqual<Int16>(Helpers.ConvToInt16(0xb23d), res[0]);
            Assert.AreEqual<Int16>(0x7dd6, res[1]);

            Converts.Int32Endian = Global.eEndian.Big;
            res = CValueToString.ValueStringToInt16Registers("2111222333", 2, 0, true, false, false);
            Assert.AreEqual<int>(2, res.Length);
            Assert.AreEqual<Int16>(0x7dd6, res[0]);
            Assert.AreEqual<Int16>(Helpers.ConvToInt16(0xb23d), res[1]);
        }

        [TestMethod]
        public void Test_Int32_4()
        {
            Converts.Int32Endian = Global.eEndian.Little;
            Int16[] res = CValueToString.ValueStringToInt16Registers("-2000000000", 2, 0, true, false, false);
            Assert.AreEqual<int>(2, res.Length);
            Assert.AreEqual<Int16>(Helpers.ConvToInt16(0x6c00), res[0]);
            Assert.AreEqual<Int16>(Helpers.ConvToInt16(0x88ca), res[1]);
        }

        [TestMethod]
        public void Test_Int32_5()
        {
            Converts.Int32Endian = Global.eEndian.Little;
            Int16[] res = CValueToString.ValueStringToInt16Registers("4111000222", 2, 0, true, false, false);
            Assert.AreEqual<int>(2, res.Length);
            Assert.AreEqual<Int16>(Helpers.ConvToInt16(0xe29e), res[0]);
            Assert.AreEqual<Int16>(Helpers.ConvToInt16(0xf508), res[1]);
        }

        [TestMethod]
        public void Test_Int32_6()
        {
            Converts.Int32Endian = Global.eEndian.Little;
            Int16[] res;
            Assert.ThrowsException<ArgumentException>(() =>
                res = CValueToString.ValueStringToInt16Registers("4555444333", 2, 0, true, false, false)
                );
        }

        [TestMethod]
        public void Test_Int32_7()
        {
            Converts.Int32Endian = Global.eEndian.Big;
            Converts.Float32Endian = Global.eEndian.Little;
            Int16[] res = CValueToString.ValueStringToInt16Registers("123456,789", 2, 0, false, true, false);
            Assert.AreEqual<int>(2, res.Length);
            Assert.AreEqual<Int16>(Helpers.ConvToInt16(0x2065), res[0]);
            Assert.AreEqual<Int16>(Helpers.ConvToInt16(0x47f1), res[1]);
        }

        [TestMethod]
        public void Test_Int32_h_1()
        {
            Converts.Int32Endian = Global.eEndian.Little;
            Int16[] res = CValueToString.ValueStringToInt16Registers("0x1324564", 2, 0, true, false, false);
            Assert.AreEqual<int>(2, res.Length);
            Assert.AreEqual<Int16>(Helpers.ConvToInt16(0x4564), res[0]);
            Assert.AreEqual<Int16>(Helpers.ConvToInt16(0x0132), res[1]);
        }

        [TestMethod]
        public void Test_Int32_h_2()
        {
            Converts.Int32Endian = Global.eEndian.Little;
            Int16[] res = CValueToString.ValueStringToInt16Registers("0xFFEEDDCC", 2, 0, true, false, false);
            Assert.AreEqual<int>(2, res.Length);
            Assert.AreEqual<Int16>(Helpers.ConvToInt16(0xDDCC), res[0]);
            Assert.AreEqual<Int16>(Helpers.ConvToInt16(0xFFEE), res[1]);
        }

        [TestMethod]
        public void Test_Int32_b_1()
        {
            Converts.Int32Endian = Global.eEndian.Little;
            Int16[] res = CValueToString.ValueStringToInt16Registers("0b1001", 2, 0, true, false, false);
            Assert.AreEqual<int>(2, res.Length);
            Assert.AreEqual<Int16>(Helpers.ConvToInt16(0x0009), res[0]);
            Assert.AreEqual<Int16>(Helpers.ConvToInt16(0x0000), res[1]);
        }

        [TestMethod]
        public void Test_Int32_b_2()
        {
            Converts.Int32Endian = Global.eEndian.Little;
            Int16[] res = CValueToString.ValueStringToInt16Registers("0b10011000011101100101010000110010", 2, 0, true, false, false);
            Assert.AreEqual<int>(2, res.Length);
            Assert.AreEqual<Int16>(Helpers.ConvToInt16(0x5432), res[0]);
            Assert.AreEqual<Int16>(Helpers.ConvToInt16(0x9876), res[1]);
        }

        [TestMethod]
        public void Test_Int32_b_3()
        {
            Converts.Int32Endian = Global.eEndian.Little;
            Int16[] res;
            Assert.ThrowsException<ArgumentException>(() =>
                res = CValueToString.ValueStringToInt16Registers("0b110011000011101100101010000110010", 2, 0, true, false, false)
                );
        }

        [TestMethod]
        public void Test_Int32_ascii_1()
        {
            Converts.StringEndian = Global.eEndian.Big;
            Int16[] res = CValueToString.ValueStringToInt16Registers("\"A\"", 2, 0, true, false, false);
            Assert.AreEqual<int>(2, res.Length);
            Assert.AreEqual<Int16>(0x4100, res[0]);
            Assert.AreEqual<Int16>(0x0000, res[1]);
        }

        [TestMethod]
        public void Test_Int32_ascii_2()
        {
            Converts.StringEndian = Global.eEndian.Big;
            Int16[] res = CValueToString.ValueStringToInt16Registers("\"ABCD\"", 2, 0, true, false, false);
            Assert.AreEqual<int>(2, res.Length);
            Assert.AreEqual<Int16>(0x4142, res[0]);
            Assert.AreEqual<Int16>(0x4344, res[1]);

            Converts.StringEndian = Global.eEndian.Little;
            res = CValueToString.ValueStringToInt16Registers("\"ABCD\"", 2, 0, true, false, false);
            Assert.AreEqual<int>(2, res.Length);
            Assert.AreEqual<Int16>(0x4241, res[0]);
            Assert.AreEqual<Int16>(0x4443, res[1]);
        }

        [TestMethod]
        public void Test_Int32_ascii_3()
        {
            Converts.StringEndian = Global.eEndian.Big;
            Int16[] res;
            Assert.ThrowsException<ArgumentException>(() =>
                res = CValueToString.ValueStringToInt16Registers("\"ABCDE\"", 2, 0, true, false, false)
                );
        }

        [TestMethod]
        public void Test_float32_1()
        {
            Converts.Float32Endian = Global.eEndian.Little;
            Int16[] res;
            Assert.ThrowsException<ArgumentException>(() =>
                res = CValueToString.ValueStringToInt16Registers("12.34", 2, 0, true, false, false)
                );
        }

        [TestMethod]
        public void Test_float32_2()
        {
            Converts.Float32Endian = Global.eEndian.Little;
            Int16[] res;
            Assert.ThrowsException<ArgumentException>(() =>
                res = CValueToString.ValueStringToInt16Registers("12,34", 2, 0, true, false, false)
                );
        }

        [TestMethod]
        public void Test_float32_3()
        {
            Converts.Float32Endian = Global.eEndian.Little;
            Int16[] res;
            Assert.ThrowsException<ArgumentException>(() =>
                res = CValueToString.ValueStringToInt16Registers("12,34", 1, 0, false, true, false)
                );
        }

        [TestMethod]
        public void Test_float32_4()
        {
            Converts.Float32Endian = Global.eEndian.Little;
            Int16[] res = CValueToString.ValueStringToInt16Registers("12,34", 2, 0, false, true, false);
            Assert.AreEqual<int>(2, res.Length);
            Assert.AreEqual<Int16>(0x70a4, res[0]);
            Assert.AreEqual<Int16>(0x4145, res[1]);
        }

        [TestMethod]
        public void Test_float32_5()
        {
            Converts.Float32Endian = Global.eEndian.Little;
            Int16[] res = CValueToString.ValueStringToInt16Registers("-9123.158", 2, 0, false, true, false);
            Assert.AreEqual<int>(2, res.Length);
            Assert.AreEqual<Int16>(Helpers.ConvToInt16(0x8ca2), res[0]);
            Assert.AreEqual<Int16>(Helpers.ConvToInt16(0xc60e), res[1]);

            Converts.Float32Endian = Global.eEndian.Big;
            res = CValueToString.ValueStringToInt16Registers("-9123.158", 2, 0, false, true, false);
            Assert.AreEqual<int>(2, res.Length);
            Assert.AreEqual<Int16>(Helpers.ConvToInt16(0xc60e), res[0]);
            Assert.AreEqual<Int16>(Helpers.ConvToInt16(0x8ca2), res[1]);
        }

        [TestMethod]
        public void Test_float32_6()
        {
            Int16[] res;
            Assert.ThrowsException<ArgumentException>(() =>
                res = CValueToString.ValueStringToInt16Registers("12,34,5", 2, 0, false, true, false)
                );
        }
    }

    [TestClass]
    public class Test_StringToIntValue
    {
        // public static Int32 StringToIntValue(string str, int Width, int Decimals)

        [TestMethod]
        public void Test_Int16_1()
        {
            Int32 res = CValueToString.ValueStringToIntValue("0", 16, 0);
            Assert.AreEqual<Int32>(0, res);
        }

        [TestMethod]
        public void Test_Int16_2()
        {
            Converts.Int32Endian = Global.eEndian.Little; // no effect
            Int32 res = CValueToString.ValueStringToIntValue("32000", 16, 0);
            Assert.AreEqual<Int32>(32000, res);

            Converts.Int32Endian = Global.eEndian.Big; // no effect
            res = CValueToString.ValueStringToIntValue("32000", 16, 0);
            Assert.AreEqual<Int32>(32000, res);
        }

        [TestMethod]
        public void Test_Int16_3()
        {
            Int32 res = CValueToString.ValueStringToIntValue("50000", 16, 0);
            Assert.AreEqual<Int32>(-15536, (Int16)res);
        }

        [TestMethod]
        public void Test_Int16_4()
        {
            Int32 res = CValueToString.ValueStringToIntValue("-1234", 16, 0);
            Assert.AreEqual<Int32>(-1234, res);
        }

        [TestMethod]
        public void Test_Int16_5()
        {
            Int32 res = CValueToString.ValueStringToIntValue("65535", 16, 0);
            Assert.AreEqual<Int32>(65535, res);
        }

        [TestMethod]
        public void Test_Int16_6()
        {
            Int32 res;
            Assert.ThrowsException<ArgumentException>(() =>
                res = CValueToString.ValueStringToIntValue("65536", 16, 0)
                );
        }

        [TestMethod]
        public void Test_Int16_7()
        {
            Int32 res = CValueToString.ValueStringToIntValue("123.45", 16, 2);
            Assert.AreEqual<Int32>(Helpers.ConvToInt16(12345), res);
        }

        [TestMethod]
        public void Test_Int16_h_1()
        {
            Int32 res = CValueToString.ValueStringToIntValue("0x0", 16, 0);
            Assert.AreEqual<Int32>(0, res);
        }

        [TestMethod]
        public void Test_Int16_h_2()
        {
            Int32 res = CValueToString.ValueStringToIntValue("0x7151", 16, 0);
            Assert.AreEqual<Int32>(29009, res);

            res = CValueToString.ValueStringToIntValue("0x7151", 16, 0);
            Assert.AreEqual<Int32>(29009, res);
        }

        [TestMethod]
        public void Test_Int16_h_3()
        {
            Int32 res = CValueToString.ValueStringToIntValue("0x00ABCD", 16, 0);
            Assert.AreEqual<Int32>(0x0000ABCD, res);
        }

        [TestMethod]
        public void Test_Int16_h_4()
        {
            Int32 res = CValueToString.ValueStringToIntValue("0xFFFF", 16, 0);
            Assert.AreEqual<Int32>(65535, res);
        }

        [TestMethod]
        public void Test_Int16_h_5()
        {
            Int32 res;
            Assert.ThrowsException<ArgumentException>(() =>
                res = CValueToString.ValueStringToIntValue("0x10000", 16, 0)
                );
        }

        [TestMethod]
        public void Test_Int16_b_1()
        {
            Int32 res = CValueToString.ValueStringToIntValue("0b101011001", 16, 0);
            Assert.AreEqual<Int32>(345, res);
        }

        [TestMethod]
        public void Test_Int16_b_2()
        {
            Int32 res = CValueToString.ValueStringToIntValue("0b00000000111101", 16, 0);
            Assert.AreEqual<Int32>(61, res);
        }

        [TestMethod]
        public void Test_Int16_b_3()
        {
            Int32 res = CValueToString.ValueStringToIntValue("0b000", 16, 0);
            Assert.AreEqual<Int32>(0, res);
        }

        [TestMethod]
        public void Test_Int16_b_4()
        {
            Int32 res;
            Converts.Int32Endian = Global.eEndian.Little;
            Assert.ThrowsException<ArgumentException>(() =>
                res = CValueToString.ValueStringToIntValue("0b10000000000000000", 16, 0)
                );
        }

        [TestMethod]
        public void Test_Int16_ascii_1()
        {
            Int32 res;
            Converts.Int32Endian = Global.eEndian.Little;
            Assert.ThrowsException<ArgumentException>(() =>
                res = CValueToString.ValueStringToIntValue("\"A\"", 16, 0)
                );
        }

        [TestMethod]
        public void Test_Int16_ascii_5()
        {
            Int32 res;
            Converts.StringEndian = Global.eEndian.Big;
            Assert.ThrowsException<ArgumentException>(() =>
                res = CValueToString.ValueStringToIntValue("ABC", 16, 0)
                );
        }

        [TestMethod]
        public void Test_Int32_1()
        {
            Converts.Int32Endian = Global.eEndian.Little;
            Int32 res = CValueToString.ValueStringToIntValue("0", 32, 0);
            Assert.AreEqual<Int32>(0, res);

            Converts.Int32Endian = Global.eEndian.Big;
            res = CValueToString.ValueStringToIntValue("0", 32, 0);
            Assert.AreEqual<Int32>(0, res);
        }

        [TestMethod]
        public void Test_Int32_2()
        {
            Converts.Int32Endian = Global.eEndian.Little;
            Int32 res = CValueToString.ValueStringToIntValue("1000", 32, 0);
            Assert.AreEqual<Int32>(1000, res);

            Converts.Int32Endian = Global.eEndian.Big;
            res = CValueToString.ValueStringToIntValue("1000", 32, 0);
            Assert.AreEqual<Int32>(1000, res);
        }

        [TestMethod]
        public void Test_Int32_3()
        {
            Converts.Int32Endian = Global.eEndian.Little; // no effect
            Int32 res = CValueToString.ValueStringToIntValue("2111222333", 32, 0);
            Assert.AreEqual<Int32>(0x7dd6b23d, res);

            Converts.Int32Endian = Global.eEndian.Big; // no effect
            res = CValueToString.ValueStringToIntValue("2111222333", 32, 0);
            Assert.AreEqual<Int32>(0x7dd6b23d, res);
        }

        [TestMethod]
        public void Test_Int32_4()
        {
            Int32 res = CValueToString.ValueStringToIntValue(" -2000000000 ", 32, 0);
            Assert.AreEqual<Int32>(-2000000000, res);
        }

        [TestMethod]
        public void Test_Int32_5()
        {
            Converts.Int32Endian = Global.eEndian.Little;
            Int32 res = CValueToString.ValueStringToIntValue("4111000222  ", 32, 0);
            Assert.AreEqual<Int32>(Helpers.ConvToInt32(4111000222), res);
        }

        [TestMethod]
        public void Test_Int32_6()
        {
            Converts.Int32Endian = Global.eEndian.Little;
            Int32 res;
            Assert.ThrowsException<ArgumentException>(() =>
                res = CValueToString.ValueStringToIntValue("4555444333", 32, 0)
                );
        }

        [TestMethod]
        public void Test_Int32_7()
        {
            Converts.Int32Endian = Global.eEndian.Little;
            Int32 res = CValueToString.ValueStringToIntValue("123456,789", 32, 3);
            Assert.AreEqual<Int32>(123456789, res);
        }

        [TestMethod]
        public void Test_Int32_h_1()
        {
            Converts.Int32Endian = Global.eEndian.Little;
            Int32 res = CValueToString.ValueStringToIntValue("0x1324564", 32, 0);
            Assert.AreEqual<Int32>(0x1324564, res);
        }

        [TestMethod]
        public void Test_Int32_h_2()
        {
            Converts.Int32Endian = Global.eEndian.Little;
            Int32 res = CValueToString.ValueStringToIntValue("0xFFEEDDCC", 32, 0);
            Assert.AreEqual<Int32>(Helpers.ConvToInt32(0xFFEEDDCC), res);
        }

        [TestMethod]
        public void Test_Int32_b_1()
        {
            Converts.Int32Endian = Global.eEndian.Little;
            Int32 res = CValueToString.ValueStringToIntValue("0b1001", 32, 0);
            Assert.AreEqual<Int32>(9, res);
        }

        [TestMethod]
        public void Test_Int32_b_2()
        {
            Converts.Int32Endian = Global.eEndian.Little;
            Int32 res = CValueToString.ValueStringToIntValue("0b10011000011101100101010000110010", 32, 0);
            Assert.AreEqual<Int32>(Helpers.ConvToInt32(0x98765432), res);
        }

        [TestMethod]
        public void Test_Int32_b_3()
        {
            Converts.Int32Endian = Global.eEndian.Little;
            Int32 res;
            Assert.ThrowsException<ArgumentException>(() =>
                res = CValueToString.ValueStringToIntValue("0b110011000011101100101010000110010", 32, 0)
                );
        }

        [TestMethod]
        public void Test_Int32_ascii_1()
        {
            Converts.Int32Endian = Global.eEndian.Little;
            Int32 res;
            Assert.ThrowsException<ArgumentException>(() =>
                res = CValueToString.ValueStringToIntValue("\"A\"", 32, 0)
                );
        }

        [TestMethod]
        public void Test_Int32_ascii_3()
        {
            Converts.StringEndian = Global.eEndian.Big;
            Int32 res;
            Assert.ThrowsException<ArgumentException>(() =>
                res = CValueToString.ValueStringToIntValue("\"ABCDE\"", 32, 0)
                );
        }
    }

    [TestClass]
    public class Test_UnquoteStr
    {
        //  public static string UnquoteStr(string input)

        [TestMethod]
        public void Test_UnquoteStr_1()
        {
            string res;
            Assert.ThrowsException<ArgumentException>(() =>
                res = CValueToString.UnquoteStr("")
                );
        }

        [TestMethod]
        public void Test_UnquoteStr_2()
        {
            string res;
            Assert.ThrowsException<ArgumentException>(() =>
                res = CValueToString.UnquoteStr("A")
                );
        }

        [TestMethod]
        public void Test_UnquoteStr_3()
        {
            string res;
            Assert.ThrowsException<ArgumentException>(() =>
                res = CValueToString.UnquoteStr("Abc")
                );
        }

        [TestMethod]
        public void Test_UnquoteStr_4()
        {
            string res;
            Assert.ThrowsException<ArgumentException>(() =>
                res = CValueToString.UnquoteStr("\"Abc")
                );
        }

        [TestMethod]
        public void Test_UnquoteStr_5()
        {
            string res;
            Assert.ThrowsException<ArgumentException>(() =>
                res = CValueToString.UnquoteStr("Abc\"")
                );
        }

        [TestMethod]
        public void Test_UnquoteStr_6()
        {
            string res;
            Assert.ThrowsException<ArgumentException>(() =>
                res = CValueToString.UnquoteStr("A\"Abc\"")
                );
        }

        [TestMethod]
        public void Test_UnquoteStr_10()
        {
            string res;
            res = CValueToString.UnquoteStr("\"Abc\"");
            Assert.AreEqual<string>("Abc", res);
        }

        [TestMethod]
        public void Test_UnquoteStr_11()
        {
            string res;
            res = CValueToString.UnquoteStr("\"AZERrtyui\"knzkjkzjd\"");
            Assert.AreEqual<string>("AZERrtyui\"knzkjkzjd", res);
        }
    }
}

