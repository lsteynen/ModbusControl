using System;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModbusControl;

namespace UnitTests
{
    [TestClass]
    public class Test_ToInt16
    {
        // public static Int16 ToInt16(byte byte0, byte byte1)

        [TestInitialize]
        public void Setup()
        {
            // This runs before EACH test
            Converts.Int32Endian = Global.eEndian.Little;
            Converts.Float32Endian = Global.eEndian.Little;
            Converts.StringEndian = Global.eEndian.Little;
            Converts.StringEncodingName = "us-ascii";
        }

        [TestMethod]
        public void Test_ToInt16_1()
        {
            Int16 res = Converts.ToInt16(200, 30);
            Assert.AreEqual<Int16>(Helpers.ConvToInt16(0x1ec8), res);
        }

        [TestMethod]
        public void Test_ToInt16_2()
        {
            Int16 res = Converts.ToInt16(19, 240);
            Assert.AreEqual<Int16>(Helpers.ConvToInt16(0xF013), res);
        }

        [TestMethod]
        public void Test_ToInt16_3()
        {
            Converts.Int32Endian = Global.eEndian.Little; // no effect
            Int16 res = Converts.ToInt16(0x41, 0x42);
            Assert.AreEqual<Int16>(0x4241, res);

            Converts.Int32Endian = Global.eEndian.Big; // no effect
            res = Converts.ToInt16(0x41, 0x42);
            Assert.AreEqual<Int16>(0x4241, res);
        }
    }

    [TestClass]
    public class Test_ToUInt16
    {
        // public static UInt16 ToUInt16(byte byte0, byte byte1, Global.eEndian endian = Global.eEndian.Little)

        [TestInitialize]
        public void Setup()
        {
            // This runs before EACH test
            Converts.Int32Endian = Global.eEndian.Little;
            Converts.Float32Endian = Global.eEndian.Little;
            Converts.StringEndian = Global.eEndian.Little;
            Converts.StringEncodingName = "us-ascii";
        }

        [TestMethod]
        public void Test_ToUInt16_1()
        {
            Converts.Int32Endian = Global.eEndian.Little; // no effect
            UInt16 res = Converts.ToUInt16(0x41, 0x42);
            Assert.AreEqual<UInt16>(0x4241, res);

            Converts.Int32Endian = Global.eEndian.Big; // no effect
            res = Converts.ToUInt16(0x41, 0x42);
            Assert.AreEqual<UInt16>(0x4241, res);
        }

        [TestMethod]
        public void Test_ToUInt16_2()
        {
            UInt16 res = Converts.ToUInt16(0x41, 0x42, Global.eEndian.Little);
            Assert.AreEqual<UInt16>(0x4241, res);

            res = Converts.ToUInt16(0x41, 0x42, Global.eEndian.Big);
            Assert.AreEqual<UInt16>(0x4142, res);
        }
    }

    [TestClass]
    public class Test_ToInt32
    {
        // public static Int32 ToInt32(Int16 word0, Int16 word1, Global.eEndian? endian = null)

        [TestInitialize]
        public void Setup()
        {
            // This runs before EACH test
            Converts.Int32Endian = Global.eEndian.Little;
            Converts.Float32Endian = Global.eEndian.Little;
            Converts.StringEndian = Global.eEndian.Little;
            Converts.StringEncodingName = "us-ascii";
        }

        [TestMethod]
        public void Test_ToInt32_1()
        {
            Converts.Int32Endian = Global.eEndian.Little;

            Int32 res = Converts.ToInt32(0, 0);
            Assert.AreEqual(0, res);
        }

        [TestMethod]
        public void Test_ToInt32_2()
        {
            Converts.Int32Endian = Global.eEndian.Little;

            Int32 res = Converts.ToInt32(10000, -15000); // 0xc5682710
            Assert.AreEqual(-983030000, res);
        }

        [TestMethod]
        public void Test_ToInt32_3()
        {
            Converts.Int32Endian = Global.eEndian.Little;

            Int32 res = Converts.ToInt32(-15000, 10000); // 0x2710c568
            Assert.AreEqual(655410536, res);
        }

        [TestMethod]
        public void Test_ToInt32_11()
        {
            Converts.Int32Endian = Global.eEndian.Big;

            Int32 res = Converts.ToInt32(0, 0);
            Assert.AreEqual(0, res);
        }

        [TestMethod]
        public void Test_ToInt32_12()
        {
            Converts.Int32Endian = Global.eEndian.Big;

            Int32 res = Converts.ToInt32(10000, -15000); // 0x2710c568
            Assert.AreEqual(655410536, res);
        }

        [TestMethod]
        public void Test_ToInt32_13()
        {
            Converts.Int32Endian = Global.eEndian.Big;

            Int32 res = Converts.ToInt32(-15000, 10000); // 0xc5682710
            Assert.AreEqual(-983030000, res);
        }
    }

    [TestClass]
    public class Test_ToUInt32
    {
        // public static UInt32 ToUInt32(Int16 word0, Int16 word1, Global.eEndian? endian = null)

        [TestInitialize]
        public void Setup()
        {
            // This runs before EACH test
            Converts.Int32Endian = Global.eEndian.Little;
            Converts.Float32Endian = Global.eEndian.Little;
            Converts.StringEndian = Global.eEndian.Little;
            Converts.StringEncodingName = "us-ascii";
        }

        [TestMethod]
        public void Test_ToUInt32_1()
        {
            Converts.Int32Endian = Global.eEndian.Little;

            UInt32 res = Converts.ToUInt32(0, 0);
            Assert.AreEqual<UInt32>(0, res);
        }

        [TestMethod]
        public void Test_ToUInt32_2()
        {
            Converts.Int32Endian = Global.eEndian.Little;

            UInt32 res = Converts.ToUInt32(10000, -15000); // 0xc5682710
            Assert.AreEqual<UInt32>(3311937296, res);
        }

        [TestMethod]
        public void Test_ToUInt32_3()
        {
            Converts.Int32Endian = Global.eEndian.Little;

            UInt32 res = Converts.ToUInt32(-15000, 10000); // 0x2710c568
            Assert.AreEqual<UInt32>(655410536, res);
        }

        [TestMethod]
        public void Test_ToUInt32_11()
        {
            Converts.Int32Endian = Global.eEndian.Big;

            UInt32 res = Converts.ToUInt32(0, 0);
            Assert.AreEqual<UInt32>(0, res);
        }

        [TestMethod]
        public void Test_ToUInt321_12()
        {
            Converts.Int32Endian = Global.eEndian.Big;

            UInt32 res = Converts.ToUInt32(10000, -15000); // 0x2710c568
            Assert.AreEqual<UInt32>(655410536, res);
        }

        [TestMethod]
        public void Test_ToUInt321_13()
        {
            Converts.Int32Endian = Global.eEndian.Big;

            UInt32 res = Converts.ToUInt32(-15000, 10000); //0xc5682710
            Assert.AreEqual<UInt32>(3311937296, res);
        }
    }

    [TestClass]
    public class Test_Int16ArrayToUInt16Array
    {
        // public static UInt16[] Int16ArrayToUInt16Array(Int16[] aValues) 

        [TestInitialize]
        public void Setup()
        {
            // This runs before EACH test
            Converts.Int32Endian = Global.eEndian.Little;
            Converts.Float32Endian = Global.eEndian.Little;
            Converts.StringEndian = Global.eEndian.Little;
            Converts.StringEncodingName = "us-ascii";
        }

        [TestMethod]
        public void Test_Int16ArrayToUInt16Array_1()
        {
            Int16[] aInt16 = { };
            UInt16[] res = Converts.Int16ArrayToUInt16Array(aInt16);
            Assert.AreEqual<int>(0, res.Length);
        }

        [TestMethod]
        public void Test_Int16ArrayToUInt16Array_2()
        {
            Int16[] aInt16 = { Helpers.ConvToInt16(0x0003) };
            UInt16[] res = Converts.Int16ArrayToUInt16Array(aInt16);
            Assert.AreEqual<int>(1, res.Length);
            Assert.AreEqual<UInt16>(3, res[0]);
        }

        [TestMethod]
        public void Test_Int16ArrayToUint16_3()
        {
            Int16[] aInt16 = { Helpers.ConvToInt16(0x0003), Helpers.ConvToInt16(0x6986), Helpers.ConvToInt16(0xFFFF), Helpers.ConvToInt16(0x9150) };
            UInt16[] res = Converts.Int16ArrayToUInt16Array(aInt16);
            Assert.AreEqual<int>(4, res.Length);
            Assert.AreEqual<UInt16>(3, res[0]);
            Assert.AreEqual<UInt16>(27014, res[1]);
            Assert.AreEqual<UInt16>(65535, res[2]);
            Assert.AreEqual<UInt16>(37200, res[3]);
        }
    }

    [TestClass]
    public class Test_Int16ArrayToByteArray
    {
        // public static byte[] Int16ArrayToByteArray(Int16[] aValues, Global.eEndian endian = Global.eEndian.Little)

        [TestInitialize]
        public void Setup()
        {
            // This runs before EACH test
            Converts.Int32Endian = Global.eEndian.Little;
            Converts.Float32Endian = Global.eEndian.Little;
            Converts.StringEndian = Global.eEndian.Little;
            Converts.StringEncodingName = "us-ascii";
        }

        [TestMethod]
        public void Int16ArrayToByteArray_1()
        {
            Int16[] aInt16 = { };
            byte[] res = Converts.Int16ArrayToByteArray(aInt16);
            Assert.AreEqual<int>(0, res.Length);
        }

        [TestMethod]
        public void Int16ArrayToByteArray_2()
        {
            Int16[] aInt16 = { 1234 };
            // default little endian
            byte[] res = Converts.Int16ArrayToByteArray(aInt16);
            Assert.AreEqual<int>(2, res.Length);
            Assert.AreEqual<byte>(0xD2, res[0]);
            Assert.AreEqual<byte>(0x04, res[1]);

            // also test big endian
            res = Converts.Int16ArrayToByteArray(aInt16, Global.eEndian.Big);
            Assert.AreEqual<int>(2, res.Length);
            Assert.AreEqual<byte>(0x04, res[0]);
            Assert.AreEqual<byte>(0xD2, res[1]);
        }

        [TestMethod]
        public void Int16ArrayToByteArray_3()
        {
            Int16[] aInt16 = { 31000, 0, -158 };
            // default little endian
            byte[] res = Converts.Int16ArrayToByteArray(aInt16);
            Assert.AreEqual<int>(6, res.Length);
            Assert.AreEqual<byte>(0x18, res[0]);
            Assert.AreEqual<byte>(0x79, res[1]);
            Assert.AreEqual<byte>(0x00, res[2]);
            Assert.AreEqual<byte>(0x00, res[3]);
            Assert.AreEqual<byte>(0x62, res[4]);
            Assert.AreEqual<byte>(0xFF, res[5]);

            // also test big endian
            res = Converts.Int16ArrayToByteArray(aInt16, Global.eEndian.Big);
            Assert.AreEqual<int>(6, res.Length);
            Assert.AreEqual<byte>(0x79, res[0]);
            Assert.AreEqual<byte>(0x18, res[1]);
            Assert.AreEqual<byte>(0x00, res[2]);
            Assert.AreEqual<byte>(0x00, res[3]);
            Assert.AreEqual<byte>(0xFF, res[4]);
            Assert.AreEqual<byte>(0x62, res[5]);
        }
    }

    [TestClass]
    public class Test_ByteArrayToInt16Array
    {
        //public static Int16[] ByteArrayToInt16Array(byte[] aBytes, Global.eEndian endian = Global.eEndian.Little)

        [TestInitialize]
        public void Setup()
        {
            // This runs before EACH test
            Converts.Int32Endian = Global.eEndian.Little;
            Converts.Float32Endian = Global.eEndian.Little;
            Converts.StringEndian = Global.eEndian.Little;
            Converts.StringEncodingName = "us-ascii";
        }

        [TestMethod]
        public void Test_ByteArrayToInt16Array_1()
        {
            byte[] ab = { };
            Int16[] res = Converts.ByteArrayToInt16Array(ab);
            Assert.AreEqual<int>(0, res.Length);
        }

        [TestMethod]
        public void Test_ByteArrayToInt16Array_2()
        {
            byte[] ab = { 0 };
            Int16[] res = Converts.ByteArrayToInt16Array(ab);
            Assert.AreEqual<int>(1, res.Length);
            Assert.AreEqual<Int16>(0, res[0]);
        }

        [TestMethod]
        public void Test_ByteArrayToInt16Array_3()
        {
            byte[] ab = { 0x7F };
            Int16[] res = Converts.ByteArrayToInt16Array(ab, Global.eEndian.Little);
            Assert.AreEqual<int>(1, res.Length);
            Assert.AreEqual<Int16>(0x007F, res[0]);

            res = Converts.ByteArrayToInt16Array(ab, Global.eEndian.Big);
            Assert.AreEqual<int>(1, res.Length);
            Assert.AreEqual<Int16>(0x7F00, res[0]);
        }

        [TestMethod]
        public void Test_ByteArrayToInt16Array_4()
        {
            byte[] ab = { 1, 2 };
            Int16[] res = Converts.ByteArrayToInt16Array(ab, Global.eEndian.Little);
            Assert.AreEqual<int>(1, res.Length);
            Assert.AreEqual<Int16>(0x0201, res[0]);

            res = Converts.ByteArrayToInt16Array(ab, Global.eEndian.Big);
            Assert.AreEqual<int>(1, res.Length);
            Assert.AreEqual<Int16>(0x0102, res[0]);
        }

        [TestMethod]
        public void Test_ByteArrayToInt16Array_5()
        {
            byte[] ab = { 0x81, 0x82, 0x83 };
            Int16[] res = Converts.ByteArrayToInt16Array(ab, Global.eEndian.Little);
            Assert.AreEqual<int>(2, res.Length);
            Assert.AreEqual<Int16>(Helpers.ConvToInt16(0x8281), res[0]);
            Assert.AreEqual<Int16>(Helpers.ConvToInt16(0x0083), res[1]);

            res = Converts.ByteArrayToInt16Array(ab, Global.eEndian.Big);
            Assert.AreEqual<int>(2, res.Length);
            Assert.AreEqual<Int16>(Helpers.ConvToInt16(0x8182), res[0]);
            Assert.AreEqual<Int16>(Helpers.ConvToInt16(0x8300), res[1]);
        }

        [TestMethod]
        public void Test_ByteArrayToInt16Array_6()
        {
            byte[] ab = { 0x71, 0x81, 0x72, 0x82, 0x83, 0x73, 0x84, 0x74, 0x85, 0x95 };
            Int16[] res = Converts.ByteArrayToInt16Array(ab, Global.eEndian.Little);
            Assert.AreEqual<int>(5, res.Length);
            Assert.AreEqual<Int16>(Helpers.ConvToInt16(0x8171), res[0]);
            Assert.AreEqual<Int16>(Helpers.ConvToInt16(0x8272), res[1]);
            Assert.AreEqual<Int16>(Helpers.ConvToInt16(0x7383), res[2]);
            Assert.AreEqual<Int16>(Helpers.ConvToInt16(0x7484), res[3]);
            Assert.AreEqual<Int16>(Helpers.ConvToInt16(0x9585), res[4]);

            res = Converts.ByteArrayToInt16Array(ab, Global.eEndian.Big);
            Assert.AreEqual<int>(5, res.Length);
            Assert.AreEqual<Int16>(Helpers.ConvToInt16(0x7181), res[0]);
            Assert.AreEqual<Int16>(Helpers.ConvToInt16(0x7282), res[1]);
            Assert.AreEqual<Int16>(Helpers.ConvToInt16(0x8373), res[2]);
            Assert.AreEqual<Int16>(Helpers.ConvToInt16(0x8474), res[3]);
            Assert.AreEqual<Int16>(Helpers.ConvToInt16(0x8595), res[4]);
        }

    }

    [TestClass]
    public class Test_IntToString
    {
        // public static string IntToString(Int32 value, int decimals = 0, int minLen = 0)

        [TestInitialize]
        public void Setup()
        {
            // This runs before EACH test
            Converts.Int32Endian = Global.eEndian.Little;
            Converts.Float32Endian = Global.eEndian.Little;
            Converts.StringEndian = Global.eEndian.Little;
            Converts.StringEncodingName = "us-ascii";
        }

        [TestMethod]
        public void Test_IntToString_1()
        {
            string res = Converts.IntToString(0);
            Assert.AreEqual<string>("0", res);
        }

        [TestMethod]
        public void Test_IntToString_2()
        {
            string res = Converts.IntToString(1);
            Assert.AreEqual<string>("1", res);
        }

        [TestMethod]
        public void Test_IntToString_3()
        {
            string res = Converts.IntToString(-1);
            Assert.AreEqual<string>("-1", res);
        }

        [TestMethod]
        public void Test_IntToString_4()
        {
            string res = Converts.IntToString(32000);
            Assert.AreEqual<string>("32000", res);
        }

        [TestMethod]
        public void Test_IntToString_5()
        {
            string res = Converts.IntToString(-32000);
            Assert.AreEqual<string>("-32000", res);
        }

        [TestMethod]
        public void Test_IntToString_6()
        {
            string res = Converts.IntToString(60000, 0);
            Assert.AreEqual<string>("60000", res);
        }

        [TestMethod]
        public void Test_IntToString_7()
        {
            string res = Converts.IntToString(1222333);
            Assert.AreEqual<string>("1222333", res);
        }

        [TestMethod]
        public void Test_IntToString_8()
        {
            string res = Converts.IntToString(12345, 5);
            Assert.AreEqual<string>("0,12345", res);
        }

        [TestMethod]
        public void Test_IntToString_9()
        {
            string res = Converts.IntToString(314, 0, 0);
            Assert.AreEqual<string>("314", res);
        }

        [TestMethod]
        public void Test_IntToString_10()
        {
            string res = Converts.IntToString(314, 0, 3);
            Assert.AreEqual<string>("314", res);
        }

        [TestMethod]
        public void Test_IntToString_11()
        {
            string res = Converts.IntToString(314, 0, 4);
            Assert.AreEqual<string>("0314", res);
        }

        [TestMethod]
        public void Test_IntToString_12()
        {
            string res = Converts.IntToString(314, 0, 6);
            Assert.AreEqual<string>("000314", res);
        }
    }

    [TestClass]
    public class Test_UIntToString
    {
        // public static string UIntToString(UInt32 value, int decimals = 0, int minLen = 0)

        [TestInitialize]
        public void Setup()
        {
            // This runs before EACH test
            Converts.Int32Endian = Global.eEndian.Little;
            Converts.Float32Endian = Global.eEndian.Little;
            Converts.StringEndian = Global.eEndian.Little;
            Converts.StringEncodingName = "us-ascii";
        }

        [TestMethod]
        public void Test_UIntToString_1()
        {
            string res = Converts.UIntToString(0);
            Assert.AreEqual<string>("0", res);
        }

        [TestMethod]
        public void Test_UIntToString_2()
        {
            string res = Converts.UIntToString(32000);
            Assert.AreEqual<string>("32000", res);
        }

        [TestMethod]
        public void Test_UIntToString_3()
        {
            string res = Converts.UIntToString(123456789, 0);
            Assert.AreEqual<string>("123456789", res);
        }

        [TestMethod]
        public void Test_UIntToString_4()
        {
            string res = Converts.UIntToString(123456789, 3);
            Assert.AreEqual<string>("123456,789", res);
        }

        [TestMethod]
        public void Test_UIntToString_5()
        {
            string res = Converts.UIntToString(4294967295, 6);
            Assert.AreEqual<string>("4294,967295", res);
        }

        [TestMethod]
        public void Test_UIntToString_6()
        {
            string res = Converts.UIntToString(123, 0, 0);
            Assert.AreEqual<string>("123", res);
        }

        [TestMethod]
        public void Test_UIntToString_7()
        {
            string res = Converts.UIntToString(123, 0, 6);
            Assert.AreEqual<string>("000123", res);
        }
    }

    [TestClass]
    public class Test_IntToStringHex
    {
        // public static string IntToStringHex(Int32 value, int minLen = 0)

        [TestInitialize]
        public void Setup()
        {
            // This runs before EACH test
            Converts.Int32Endian = Global.eEndian.Little;
            Converts.Float32Endian = Global.eEndian.Little;
            Converts.StringEndian = Global.eEndian.Little;
            Converts.StringEncodingName = "us-ascii";
        }

        [TestMethod]
        public void Test_IntToStringHex_1()
        {
            string res = Converts.IntToStringHex(0);
            Assert.AreEqual<string>("0x0", res);
        }

        [TestMethod]
        public void Test_IntToStringHex_2()
        {
            string res = Converts.IntToStringHex(127);
            Assert.AreEqual<string>("0x7F", res);
        }

        [TestMethod]
        public void Test_IntToStringHex_3()
        {
            string res = Converts.IntToStringHex(123456789);
            Assert.AreEqual<string>("0x75BCD15", res);
        }

        [TestMethod]
        public void Test_IntToStringHex_4()
        {
            string res = Converts.IntToStringHex(-1);
            Assert.AreEqual<string>("0xFFFFFFFF", res);
        }

        [TestMethod]
        public void Test_IntToStringHex_5()
        {
            string res = Converts.IntToStringHex(1000, 0);
            Assert.AreEqual<string>("0x3E8", res);
        }

        [TestMethod]
        public void Test_IntToStringHex_6()
        {
            string res = Converts.IntToStringHex(1000, 6);
            Assert.AreEqual<string>("0x0003E8", res);
        }
    }

    [TestClass]
    public class Test_Int16ToFloat32
    {
        // public static float Int16ToFloat32(Int16 word0, Int16 word1)

        [TestInitialize]
        public void Setup()
        {
            // This runs before EACH test
            Converts.Int32Endian = Global.eEndian.Little;
            Converts.Float32Endian = Global.eEndian.Little;
            Converts.StringEndian = Global.eEndian.Little;
            Converts.StringEncodingName = "us-ascii";
        }

        [TestMethod]
        public void Test_Int16ToFloat32_1()
        {
            Converts.Float32Endian = Global.eEndian.Little;
            float res = Converts.Int16ToFloat32(0, 0);
            Assert.AreEqual<float>((float)0.0, res);
        }

        [TestMethod]
        public void Test_Int16ToFloat32_2()
        {
            Converts.Float32Endian = Global.eEndian.Little;
            float res = Converts.Int16ToFloat32(0x0000, Helpers.ConvToInt16(0x3f80));
            Assert.AreEqual<float>((float)1.0, res);
        }

        [TestMethod]
        public void Test_Int16ToFloat32_3()
        {
            Converts.Float32Endian = Global.eEndian.Little;
            float res = Converts.Int16ToFloat32(Helpers.ConvToInt16(0x0fd0), Helpers.ConvToInt16(0x4049));
            Assert.AreEqual<float>((float)3.14159, res);
        }

        [TestMethod]
        public void Test_Int16ToFloat32_4()
        {
            Converts.Float32Endian = Global.eEndian.Little;
            float res = Converts.Int16ToFloat32(Helpers.ConvToInt16(0x205a), Helpers.ConvToInt16(0x47f1));
            Assert.AreEqual<float>((float)123456.7, res);
        }

        [TestMethod]
        public void Test_Int16ToFloat32_5()
        {
            Converts.Float32Endian = Global.eEndian.Little;
            float res = Converts.Int16ToFloat32(Helpers.ConvToInt16(0x64fe), Helpers.ConvToInt16(0xc3e4));
            Assert.AreEqual<float>((float)-456.789, res);
        }

        [TestMethod]
        public void Test_Int16ToFloat32_11()
        {
            Converts.Float32Endian = Global.eEndian.Big;
            float res = Converts.Int16ToFloat32(0, 0);
            Assert.AreEqual<float>((float)0.0, res);
        }

        [TestMethod]
        public void Test_Int16ToFloat32_12()
        {
            Converts.Float32Endian = Global.eEndian.Big;
            float res = Converts.Int16ToFloat32(Helpers.ConvToInt16(0x3f80), Helpers.ConvToInt16(0x0000));
            Assert.AreEqual<float>((float)1.0, res);
        }

        [TestMethod]
        public void Test_Int16ToFloat32_13()
        {
            Converts.Float32Endian = Global.eEndian.Big;
            float res = Converts.Int16ToFloat32(Helpers.ConvToInt16(0x4049), Helpers.ConvToInt16(0x0fd0));
            Assert.AreEqual<float>((float)3.14159, res);
        }

        [TestMethod]
        public void Test_Int16ToFloat32_14()
        {
            Converts.Float32Endian = Global.eEndian.Big;
            float res = Converts.Int16ToFloat32(Helpers.ConvToInt16(0x47f1), Helpers.ConvToInt16(0x205a));
            Assert.AreEqual<float>((float)123456.7, res);
        }

        [TestMethod]
        public void Test_Int16ToFloat32_15()
        {
            Converts.Float32Endian = Global.eEndian.Big;
            float res = Converts.Int16ToFloat32(Helpers.ConvToInt16(0xc3e4), Helpers.ConvToInt16(0x64fe));
            Assert.AreEqual<float>((float)-456.789, res);
        }
    }

    [TestClass]
    public class Test_Int16ToAscii
    {
        // public static string Int16ToAscii(Int16 valInt16)

        [TestInitialize]
        public void Setup()
        {
            // This runs before EACH test
            Converts.Int32Endian = Global.eEndian.Little;
            Converts.Float32Endian = Global.eEndian.Little;
            Converts.StringEndian = Global.eEndian.Little;
            Converts.StringEncodingName = "us-ascii";
        }

        [TestMethod]
        public void Test_Int16ToAscii_1()
        {
            Converts.StringEndian = Global.eEndian.Big;
            string res = Converts.Int16ToAscii(Helpers.ConvToInt16(0x4241));
            Assert.AreEqual<string>("BA", res);

            Converts.StringEndian = Global.eEndian.Little;
            res = Converts.Int16ToAscii(Helpers.ConvToInt16(0x4241));
            Assert.AreEqual<string>("AB", res);
        }

        [TestMethod]
        public void Test_Int16ToAscii_2()
        {
            Converts.StringEndian = Global.eEndian.Big;
            string res = Converts.Int16ToAscii(Helpers.ConvToInt16(0x0041));
            Assert.AreEqual<string>("\\x00A", res);

            Converts.StringEndian = Global.eEndian.Little;
            res = Converts.Int16ToAscii(Helpers.ConvToInt16(0x0041));
            Assert.AreEqual<string>("A\\x00", res);
        }

        [TestMethod]
        public void Test_Int16ToAscii_3()
        {
            Converts.StringEndian = Global.eEndian.Big;
            string res = Converts.Int16ToAscii(Helpers.ConvToInt16(0));
            Assert.AreEqual<string>(@"\x00\x00", res);

            Converts.StringEndian = Global.eEndian.Little;
            res = Converts.Int16ToAscii(Helpers.ConvToInt16(0));
            Assert.AreEqual<string>(@"\x00\x00", res);
        }

        [TestMethod]
        public void Test_Int16ToAscii_4()
        {
            Converts.StringEndian = Global.eEndian.Big;
            string res = Converts.Int16ToAscii(Helpers.ConvToInt16(0x00FE));
            Assert.AreEqual<string>("\\x00\\xFE", res);

            Converts.StringEndian = Global.eEndian.Little;
            res = Converts.Int16ToAscii(Helpers.ConvToInt16(0x00FE));
            Assert.AreEqual<string>("\\xFE\\x00", res);
        }
    }

    [TestClass]
    public class Test_Int16ArrayToUInt32
    {
        // public static UInt32 Int16ArrayToUInt32(Int16[] aInt16, Global.eEndian? endian = null)

        [TestInitialize]
        public void Setup()
        {
            // This runs before EACH test
            Converts.Int32Endian = Global.eEndian.Little;
            Converts.Float32Endian = Global.eEndian.Little;
            Converts.StringEndian = Global.eEndian.Little;
            Converts.StringEncodingName = "us-ascii";
        }

        [TestMethod]
        public void Test_Int16ArrayToUInt32_1()
        {
            Converts.Int32Endian = Global.eEndian.Little;
            Int16[] aInt16 = { 0 };
            Assert.ThrowsException<ArgumentException>(() =>
                Converts.Int16ArrayToUInt32(aInt16));
        }

        [TestMethod]
        public void Test_Int16ArrayToUInt32_2()
        {
            Converts.Int32Endian = Global.eEndian.Little;
            Int16[] aInt16 = { 0, 0 };
            UInt32 res = Converts.Int16ArrayToUInt32(aInt16);
            Assert.AreEqual<UInt32>(0, res);
        }

        [TestMethod]
        public void Test_Int16ArrayToUInt32_3()
        {
            Converts.Int32Endian = Global.eEndian.Little;

            Int16[] aInt16 = { Helpers.ConvToInt16(0xCD15), Helpers.ConvToInt16(0x075B) };
            UInt32 res = Converts.Int16ArrayToUInt32(aInt16);
            Assert.AreEqual<UInt32>(123456789, res);
        }

        [TestMethod]
        public void Test_Int16ArrayToUInt32_4()
        {
            Converts.Int32Endian = Global.eEndian.Little;

            Int16[] aInt16 = { Helpers.ConvToInt16(0x3532), Helpers.ConvToInt16(0xFFDC) };
            UInt32 res = Converts.Int16ArrayToUInt32(aInt16);
            Assert.AreEqual<UInt32>(4292621618, res);
        }

        [TestMethod]
        public void Test_Int16ArrayToUInt32_5()
        {
            Converts.Int32Endian = Global.eEndian.Big;

            Int16[] aInt16 = { 0 };
            Assert.ThrowsException<ArgumentException>(() =>
                Converts.Int16ArrayToUInt32(aInt16));
        }

        [TestMethod]
        public void Test_Int16ArrayToUInt32_6()
        {
            Converts.Int32Endian = Global.eEndian.Big;

            Int16[] aInt16 = { 0, 0 };
            UInt32 res = Converts.Int16ArrayToUInt32(aInt16);
            Assert.AreEqual<UInt32>(0, res);
        }

        [TestMethod]
        public void Test_Int16ArrayToUInt32_7()
        {
            Converts.Int32Endian = Global.eEndian.Big;

            Int16[] aInt16 = { Helpers.ConvToInt16(0x075B), Helpers.ConvToInt16(0xCD15) };
            UInt32 res = Converts.Int16ArrayToUInt32(aInt16);
            Assert.AreEqual<UInt32>(123456789, res);
        }

        [TestMethod]
        public void Test_Int16ArrayToUInt32_8()
        {
            Converts.Int32Endian = Global.eEndian.Big;

            Int16[] aInt16 = { Helpers.ConvToInt16(0xFFDC), Helpers.ConvToInt16(0x3532) };
            UInt32 res = Converts.Int16ArrayToUInt32(aInt16);
            Assert.AreEqual<UInt32>(4292621618, res);
        }
    }

    [TestClass]
    public class Test_Int16ArrayToStringOfValues
    {
        // public static string Int16ArrayToStringOfValues(Int16[] aValues)

        [TestInitialize]
        public void Setup()
        {
            // This runs before EACH test
            Converts.Int32Endian = Global.eEndian.Little;
            Converts.Float32Endian = Global.eEndian.Little;
            Converts.StringEndian = Global.eEndian.Little;
            Converts.StringEncodingName = "us-ascii";
        }

        [TestMethod]
        public void Test_Int16ArrayToStringOfValues_1()
        {
            Int16[] aInt16 = { };
            string res = Converts.Int16ArrayToStringOfValues(aInt16);
            Assert.AreEqual<string>("", res);
        }

        [TestMethod]
        public void Test_Int16ArrayToStringOfValues_2()
        {
            Int16[] aInt16 = { Helpers.ConvToInt16(0x0003) };
            string res = Converts.Int16ArrayToStringOfValues(aInt16);
            Assert.AreEqual<string>("3", res);
        }

        [TestMethod]
        public void Test_Int16ArrayToStringOfValues_3()
        {
            Int16[] aInt16 = { Helpers.ConvToInt16(0x0003), Helpers.ConvToInt16(0x6986), Helpers.ConvToInt16(0xFFFF), Helpers.ConvToInt16(0x9150) };
            string res = Converts.Int16ArrayToStringOfValues(aInt16);
            Assert.AreEqual<string>("3, 27014, -1, -28336", res);
        }
    }

    [TestClass]
    public class Test_ByteArrayToStringOfValues
    {
        // public static string ByteArrayToStringOfValues(byte[] aBytes, int maxCount = -1)

        [TestInitialize]
        public void Setup()
        {
            // This runs before EACH test
            Converts.Int32Endian = Global.eEndian.Little;
            Converts.Float32Endian = Global.eEndian.Little;
            Converts.StringEndian = Global.eEndian.Little;
            Converts.StringEncodingName = "us-ascii";
        }

        [TestMethod]
        public void Test_ByteArrayToStringOfValues_1()
        {
            byte[] ba = { };
            string res = Converts.ByteArrayToStringOfValues(ba);
            Assert.AreEqual<string>("", res);
        }

        [TestMethod]
        public void Test_ByteArrayToStringOfValues_2()
        {
            byte[] ba = { 0x41 };
            string res = Converts.ByteArrayToStringOfValues(ba);
            Assert.AreEqual<string>("0x41", res);
        }

        [TestMethod]
        public void Test_ByteArrayToStringOfValues_11()
        {
            byte[] ba = { 0x41, 0x00, 0x18, 0xfa };
            string res = Converts.ByteArrayToStringOfValues(ba);
            Assert.AreEqual<string>("0x41, 0x00, 0x18, 0xFA", res);
        }

        [TestMethod]
        public void Test_ByteArrayToStringOfValues_12()
        {
            byte[] ba = { };
            string res = Converts.ByteArrayToStringOfValues(ba, 0);
            Assert.AreEqual<string>("", res);
        }

        [TestMethod]
        public void Test_ByteArrayToStringOfValues_13()
        {
            byte[] ba = { 0x41, 0x42 };
            string res = Converts.ByteArrayToStringOfValues(ba, 0);
            Assert.AreEqual<string>("", res);
        }

        [TestMethod]
        public void Test_ByteArrayToStringOfValues_14()
        {
            byte[] ba = { 0x41, 0x00, 0x18, 0xfa };
            string res = Converts.ByteArrayToStringOfValues(ba, 3);
            Assert.AreEqual<string>("0x41, 0x00, 0x18", res);
        }

        [TestMethod]
        public void Test_ByteArrayToStringOfValues_15()
        {
            byte[] ba = { 0x41, 0x00, 0x18, 0xfa };
            Assert.ThrowsException<ArgumentException>(() =>
                Converts.ByteArrayToStringOfValues(ba, 5)
                );
        }
    }

    [TestClass]
    public class Test_Int16RegistersToUInt32Value
    {
        // public static UInt32 Int16RegistersToUInt32Value(Int16[] Regs, int size, ModbusControlSpace.Global.eType type)

        [TestInitialize]
        public void Setup()
        {
            // This runs before EACH test
            Converts.Int32Endian = Global.eEndian.Little;
            Converts.Float32Endian = Global.eEndian.Little;
            Converts.StringEndian = Global.eEndian.Little;
            Converts.StringEncodingName = "us-ascii";
        }

        [TestMethod]
        public void Test_Int16RegistersToValue32_Int16_1()
        {
            Int16[] aInt16 = new Int16[] { 0x1234 };
            Converts.Int32Endian = Global.eEndian.Little;
            UInt32 result = Converts.Int16ArrayToUInt32Value(aInt16, 1, ModbusControl.Global.eType.Int);
            Assert.AreEqual<UInt32>((UInt32)0x1234, result);
        }

        [TestMethod]
        public void Test_Int16RegistersToValue32_Int16_1b()
        {
            Int16[] aInt16 = new Int16[] { Helpers.ConvToInt16(0x1234), 0x5678 };
            Converts.Int32Endian = Global.eEndian.Big;
            UInt32 result = Converts.Int16ArrayToUInt32Value(aInt16, 1, ModbusControl.Global.eType.Int);
            Assert.AreEqual<UInt32>((UInt32)0x1234, result);
        }

        [TestMethod]
        public void Test_Int16RegistersToValue32_Int32()
        {
            Int16[] aInt16 = new Int16[] { Helpers.ConvToInt16(0xAAAA), Helpers.ConvToInt16(0xBBBB) };
            Converts.Int32Endian = Global.eEndian.Little;
            UInt32 result = Converts.Int16ArrayToUInt32Value(aInt16, 2, ModbusControl.Global.eType.Int);
            Assert.AreEqual<UInt32>(0xBBBBAAAA, result);

            Converts.Int32Endian = Global.eEndian.Big;
            result = Converts.Int16ArrayToUInt32Value(aInt16, 2, ModbusControl.Global.eType.Int);
            Assert.AreEqual<UInt32>(0xAAAABBBB, result);
        }

        [TestMethod]
        public void Test_Int16RegistersToValue32_Float32_1_0()
        {
            Int16[] aInt16 = new Int16[] { 0x3F80, 0x0010 };
            Converts.Float32Endian = Global.eEndian.Little;
            UInt32 result = Converts.Int16ArrayToUInt32Value(aInt16, 2, ModbusControl.Global.eType.Float);
            Assert.AreEqual<UInt32>(0x00103F80, result); // IEEE 754 for 1.0

            Converts.Float32Endian = Global.eEndian.Big;
            result = Converts.Int16ArrayToUInt32Value(aInt16, 2, ModbusControl.Global.eType.Float);
            Assert.AreEqual<UInt32>(0x3F800010, result); // IEEE 754 for 1.0
        }

        [TestMethod]
        public void Test_Int16RegistersToValue32_Float32_Negative()
        {
            Int16[] aInt16 = new Int16[] { Helpers.ConvToInt16(0xBF80), 0x000A };
            Converts.Float32Endian = Global.eEndian.Little;
            UInt32 result = Converts.Int16ArrayToUInt32Value(aInt16, 2, ModbusControl.Global.eType.Float);
            Assert.AreEqual<UInt32>(0x000ABF80, result); // IEEE 754 for -1.0

            aInt16 = new Int16[] { 0x000A, Helpers.ConvToInt16(0xBF80) };
            Converts.Float32Endian = Global.eEndian.Big;
            result = Converts.Int16ArrayToUInt32Value(aInt16, 2, ModbusControl.Global.eType.Float);
            Assert.AreEqual<UInt32>(0x000ABF80, result); // IEEE 754 for -1.0
        }

        [TestMethod]
        public void Test_Int16RegistersToValue32_Ascii32_1()
        {
            Int16[] aInt16 = new Int16[] { 0x4142, 0x4344 };
            Converts.Int32Endian = Global.eEndian.Little;
            UInt32 result = Converts.Int16ArrayToUInt32Value(aInt16, 2, ModbusControl.Global.eType.ASCII);
            Assert.AreEqual<UInt32>(0x43444142, result); // "ABCD", little-endian

            // Int32BigEndian does not matter
            Converts.Int32Endian = Global.eEndian.Big;
            result = Converts.Int16ArrayToUInt32Value(aInt16, 2, ModbusControl.Global.eType.ASCII);
            Assert.AreEqual<UInt32>(0x43444142, result); // "ABCD", little-endian
        }


        [TestMethod]
        public void Test_Int16RegistersToValue32_Ascii16_2()
        {
            Int16[] aInt16 = new Int16[] { 0x0041 };
            // StringBigEndian does not matter
            Converts.StringEndian = Global.eEndian.Little;
            UInt32 result = Converts.Int16ArrayToUInt32Value(aInt16, 1, ModbusControl.Global.eType.ASCII);
            Assert.AreEqual<UInt32>(0x41u, result); // 'A'

            Converts.StringEndian = Global.eEndian.Big;
            result = Converts.Int16ArrayToUInt32Value(aInt16, 1, ModbusControl.Global.eType.ASCII);
            Assert.AreEqual<UInt32>(0x41u, result); // 'A'
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_Int16RegistersToValue32_Excep()
        {
            Int16[] aInt16 = new Int16[] { 0x1234 };
            Converts.Int32Endian = Global.eEndian.Little;
            Converts.Int16ArrayToUInt32Value(aInt16, 1, (ModbusControl.Global.eType)999);
        }

        [TestMethod]
        public void Test_Int16RegistersToValue32_Int32_8()
        {
            Int16[] aInt16 = new Int16[] { 0x0000, 0x0000 };
            Converts.Int32Endian = Global.eEndian.Little;
            UInt32 result = Converts.Int16ArrayToUInt32Value(aInt16, 1, ModbusControl.Global.eType.Int);
            Assert.AreEqual<UInt32>(0x00000000, result);

            Converts.Int32Endian = Global.eEndian.Little;
            result = Converts.Int16ArrayToUInt32Value(aInt16, 1, ModbusControl.Global.eType.Int);
            Assert.AreEqual<UInt32>(0x00000000, result);
        }

        [TestMethod]
        public void Test_Int16RegistersToValue32_Float32_Max()
        {
            Int16[] aInt16 = new Int16[] { 0x7F7F, Helpers.ConvToInt16(0xFFFF) };
            Converts.Int32Endian = Global.eEndian.Big;
            Converts.Float32Endian = Global.eEndian.Little;
            UInt32 result = Converts.Int16ArrayToUInt32Value(aInt16, 2, ModbusControl.Global.eType.Float);
            Assert.AreEqual<UInt32>(0xFFFF7F7F, result); // Largest positive float32

            Converts.Float32Endian = Global.eEndian.Big;
            result = Converts.Int16ArrayToUInt32Value(aInt16, 2, ModbusControl.Global.eType.Float);
            Assert.AreEqual<UInt32>(0x7F7FFFFF, result); // Largest positive float32
        }

        [TestMethod]
        public void Test_Int16RegistersToValue32_Ascii32_NullBytes()
        {
            Int16[] aInt16 = new Int16[] { 0x0000, 0x0000 };
            Converts.Int32Endian = Global.eEndian.Little;
            UInt32 result = Converts.Int16ArrayToUInt32Value(aInt16, 2, ModbusControl.Global.eType.ASCII);
            Assert.AreEqual<UInt32>(0x00000000, result);

            Converts.Int32Endian = Global.eEndian.Big;
            result = Converts.Int16ArrayToUInt32Value(aInt16, 2, ModbusControl.Global.eType.ASCII);
            Assert.AreEqual<UInt32>(0x00000000, result);
        }

        [TestMethod]
        public void Test_Int16RegistersToValue32_Int16_Width16_SignedConversion()
        {
            Int16[] aInt16 = new Int16[] { Helpers.ConvToInt16(0xFFFF) };
            Converts.Int32Endian = Global.eEndian.Little;
            UInt32 result = Converts.Int16ArrayToUInt32Value(aInt16, 1, ModbusControl.Global.eType.Int);
            Assert.AreEqual<UInt32>(0x0000FFFFu, result); // Sign extended

            Converts.Int32Endian = Global.eEndian.Big;
            result = Converts.Int16ArrayToUInt32Value(aInt16, 1, ModbusControl.Global.eType.Int);
            Assert.AreEqual<UInt32>(0x0000FFFFu, result); // Sign extended
        }

        [TestMethod]
        public void Test_Int16RegistersToValue32_Float32_Zero()
        {
            Int16[] aInt16 = new Int16[] { 0x0000, 0x0000 };
            Converts.Int32Endian = Global.eEndian.Little;
            UInt32 result = Converts.Int16ArrayToUInt32Value(aInt16, 2, ModbusControl.Global.eType.Float);
            Assert.AreEqual<UInt32>(0x00000000u, result); // 0.0

            Converts.Int32Endian = Global.eEndian.Big;
            result = Converts.Int16ArrayToUInt32Value(aInt16, 2, ModbusControl.Global.eType.Float);
            Assert.AreEqual<UInt32>(0x00000000u, result); // 0.0
        }
    }

    [TestClass]
    public class Test_AsciiStringToUInt32
    {
        // public static UInt32 AsciiStringToUInt32(string str)

        [TestInitialize]
        public void Setup()
        {
            // This runs before EACH test
            Converts.Int32Endian = Global.eEndian.Little;
            Converts.Float32Endian = Global.eEndian.Little;
            Converts.StringEndian = Global.eEndian.Little;
            Converts.StringEncodingName = "us-ascii";
        }

        [TestMethod]
        public void Test_AsciiStringToUInt32_1()
        {
            Converts.StringEndian = Global.eEndian.Big; // no effect
            Assert.ThrowsException<ArgumentException>(() =>
                Converts.AsciiStringToUInt32("")
                );
        }

        [TestMethod]
        public void Test_AsciiStringToUInt32_2()
        {
            Converts.StringEndian = Global.eEndian.Little; // no effect
            Assert.ThrowsException<ArgumentException>(() =>
                Converts.AsciiStringToUInt32("")
                );
        }

        [TestMethod]
        public void Test_AsciiStringToUInt32_3()
        {
            Converts.StringEndian = Global.eEndian.Big; // no effect
            UInt32 result = Converts.AsciiStringToUInt32("A\x01");
            Assert.AreEqual<UInt32>(0x0141, result);

            Converts.StringEndian = Global.eEndian.Little; // no effect
            result = Converts.AsciiStringToUInt32("A\x01");
            Assert.AreEqual<UInt32>(0x0141, result);
        }

        [TestMethod]
        public void Test_AsciiStringToUInt32_4()
        {
            UInt32 result = Converts.AsciiStringToUInt32("AB");
            Assert.AreEqual<UInt32>(0x4241, result);
        }

        [TestMethod]
        public void Test_AsciiStringToUInt32_5()
        {
            UInt32 result = Converts.AsciiStringToUInt32("ABC");
            Assert.AreEqual<UInt32>(0x00434241, result);
        }

        [TestMethod]
        public void Test_AsciiStringToUInt32_6()
        {
            Converts.StringEndian = Global.eEndian.Big; // no effect
            UInt32 result = Converts.AsciiStringToUInt32("ABCD");
            Assert.AreEqual<UInt32>(0x44434241, result);

            Converts.StringEndian = Global.eEndian.Little; // no effect
            result = Converts.AsciiStringToUInt32("ABCD");
            Assert.AreEqual<UInt32>(0x44434241, result);
        }

        [TestMethod]
        public void Test_AsciiStringToUInt32_7()
        {
            Assert.ThrowsException<ArgumentException>(() =>
                Converts.AsciiStringToUInt32("ABCDE")
                );
        }
    }

    [TestClass]
    public class Test_UInt32ToAscii
    {
        // public static string UInt32ToAscii(UInt32 value, int width)

        [TestInitialize]
        public void Setup()
        {
            // This runs before EACH test
            Converts.Int32Endian = Global.eEndian.Little;
            Converts.Float32Endian = Global.eEndian.Little;
            Converts.StringEndian = Global.eEndian.Little;
            Converts.StringEncodingName = "us-ascii";
        }

        [TestMethod]
        public void Test_UInt32ToAscii_1()
        {
            Converts.StringEndian = Global.eEndian.Big; // no effect
            string res = Converts.UInt32ToAscii(0, 4);
            Assert.AreEqual<string>("\\x00", res);

            Converts.StringEndian = Global.eEndian.Little; // no effect
            res = Converts.UInt32ToAscii(0, 4);
            Assert.AreEqual<string>("\\x00", res);
        }

        [TestMethod]
        public void Test_UInt32ToAscii_2()
        {
            Converts.StringEndian = Global.eEndian.Big; // no effect
            string res = Converts.UInt32ToAscii(0, 8);
            Assert.AreEqual<string>("\\x00", res);
        }

        [TestMethod]
        public void Test_UInt32ToAscii_3()
        {
            Converts.StringEndian = Global.eEndian.Big; // no effect
            string res = Converts.UInt32ToAscii(0, 16);
            Assert.AreEqual<string>("\\x00\\x00", res);
        }

        [TestMethod]
        public void Test_UInt32ToAscii_4()
        {
            Converts.StringEndian = Global.eEndian.Big; // no effect
            string res = Converts.UInt32ToAscii(0, 32);
            Assert.AreEqual<string>("\\x00\\x00\\x00\\x00", res);
        }

        [TestMethod]
        public void Test_UInt32ToAscii_10()
        {
            Converts.StringEndian = Global.eEndian.Big; // no effect
            string res = Converts.UInt32ToAscii(0x41, 8);
            Assert.AreEqual<string>("A", res);

            Converts.StringEndian = Global.eEndian.Big; // no effect
            res = Converts.UInt32ToAscii(0x41, 16);
            Assert.AreEqual<string>("A\\x00", res);
        }

        [TestMethod]
        public void Test_UInt32ToAscii_11()
        {
            Converts.StringEndian = Global.eEndian.Big;
            string res = Converts.UInt32ToAscii(0x4241, 8);
            Assert.AreEqual<string>("A", res);

            Converts.StringEndian = Global.eEndian.Big;
            res = Converts.UInt32ToAscii(0x4241, 16);
            Assert.AreEqual<string>("AB", res);
        }

        [TestMethod]
        public void Test_UInt32ToAscii_12()
        {
            Converts.StringEndian = Global.eEndian.Big; // no effect
            string res = Converts.UInt32ToAscii(0x4241, 16);
            Assert.AreEqual<string>("AB", res);

            Converts.StringEndian = Global.eEndian.Little; // no effect
            res = Converts.UInt32ToAscii(0x4241, 16);
            Assert.AreEqual<string>("AB", res);
        }

        [TestMethod]
        public void Test_UInt32ToAscii_13()
        {
            Converts.StringEndian = Global.eEndian.Big; // no effect
            string res = Converts.UInt32ToAscii(0x44434241, 32);
            Assert.AreEqual<string>("ABCD", res);

            Converts.StringEndian = Global.eEndian.Little; // no effect
            res = Converts.UInt32ToAscii(0x44434241, 32);
            Assert.AreEqual<string>("ABCD", res);
        }
    }

    [TestClass]
    public class Test_AsciiStringToInt16Array
    {
        // public static Int16[] AsciiStringToInt16Array(string str, int size)

        [TestInitialize]
        public void Setup()
        {
            // This runs before EACH test
            Converts.Int32Endian = Global.eEndian.Little;
            Converts.Float32Endian = Global.eEndian.Little;
            Converts.StringEndian = Global.eEndian.Little;
            Converts.StringEncodingName = "us-ascii";
        }

        [TestMethod]
        public void Test_AsciiStringToInt16Array_1()
        {
            Converts.StringEndian = Global.eEndian.Big;
            Int16[] res = Converts.AsciiStringToInt16Array(null, 1);
            Assert.AreEqual<int>(1, res.Length);
            Assert.AreEqual<Int16>(0x0000, res[0]);

            Converts.StringEndian = Global.eEndian.Little;
            res = Converts.AsciiStringToInt16Array(null, 1);
            Assert.AreEqual<int>(1, res.Length);
            Assert.AreEqual<Int16>(0x0000, res[0]);
        }

        [TestMethod]
        public void Test_AsciiStringToInt16Array_1b()
        {
            Converts.StringEndian = Global.eEndian.Big;
            Int16[] res = Converts.AsciiStringToInt16Array("", 1);
            Assert.AreEqual<int>(1, res.Length);
            Assert.AreEqual<Int16>(0x0000, res[0]);

            Converts.StringEndian = Global.eEndian.Little;
            res = Converts.AsciiStringToInt16Array("", 1);
            Assert.AreEqual<int>(1, res.Length);
            Assert.AreEqual<Int16>(0x0000, res[0]);
        }

        [TestMethod]
        public void Test_AsciiStringToInt16Array_2()
        {
            Converts.StringEndian = Global.eEndian.Big;
            Int16[] res = Converts.AsciiStringToInt16Array(@"A", 1);
            Assert.AreEqual<int>(1, res.Length);
            Assert.AreEqual<Int16>(0x4100, res[0]);

            Converts.StringEndian = Global.eEndian.Little;
            res = res = Converts.AsciiStringToInt16Array(@"A", 1);
            Assert.AreEqual<int>(1, res.Length);
            Assert.AreEqual<Int16>(0x0041, res[0]);
        }

        [TestMethod]
        public void Test_AsciiStringToInt16Array_3()
        {
            Converts.StringEndian = Global.eEndian.Big;
            Int16[] res = Converts.AsciiStringToInt16Array(@"AB", 1);
            Assert.AreEqual<int>(1, res.Length);
            Assert.AreEqual<Int16>(0x4142, res[0]);

            Converts.StringEndian = Global.eEndian.Little;
            res = res = Converts.AsciiStringToInt16Array(@"AB", 1);
            Assert.AreEqual<int>(1, res.Length);
            Assert.AreEqual<Int16>(0x4241, res[0]);
        }

        [TestMethod]
        public void Test_AsciiStringToInt16Array_4()
        {
            Converts.StringEndian = Global.eEndian.Big;
            Assert.ThrowsException<ArgumentException>(() =>
                Converts.AsciiStringToInt16Array(@"ABC", 1)
                );
        }

        [TestMethod]
        public void Test_AsciiStringToInt16Array_10()
        {
            Converts.StringEndian = Global.eEndian.Big;
            Int16[] res = Converts.AsciiStringToInt16Array(@"A", 2);
            Assert.AreEqual<int>(2, res.Length);
            Assert.AreEqual<Int16>(0x4100, res[0]);
            Assert.AreEqual<Int16>(0x0000, res[1]);

            Converts.StringEndian = Global.eEndian.Little;
            res = res = Converts.AsciiStringToInt16Array(@"A", 2);
            Assert.AreEqual<int>(2, res.Length);
            Assert.AreEqual<Int16>(0x0041, res[0]);
            Assert.AreEqual<Int16>(0x0000, res[1]);
        }
    }

    [TestClass]
    public class Test_Int32ToInt16Array
    {
        // public static Int16[] Int32ToInt16Array(Int32 value32, int size = 0)

        [TestInitialize]
        public void Setup()
        {
            // This runs before EACH test
            Converts.Int32Endian = Global.eEndian.Little;
            Converts.Float32Endian = Global.eEndian.Little;
            Converts.StringEndian = Global.eEndian.Little;
            Converts.StringEncodingName = "us-ascii";
        }

        [TestMethod]
        public void Test_Int32ToInt16Array_1()
        {
            Converts.Int32Endian = Global.eEndian.Little;
            Int16[] res = Converts.Int32ToInt16Array(0);
            Assert.AreEqual<int>(2, res.Length);
            Assert.AreEqual<Int16>(0, res[0]);
            Assert.AreEqual<Int16>(0, res[1]);

            Converts.Int32Endian = Global.eEndian.Big;
            res = Converts.Int32ToInt16Array(0);
            Assert.AreEqual<int>(2, res.Length);
            Assert.AreEqual<Int16>(0, res[0]);
            Assert.AreEqual<Int16>(0, res[1]);
        }

        [TestMethod]
        public void Test_Int32ToInt16Array_2()
        {
            Converts.Int32Endian = Global.eEndian.Little;
            Int16[] res = Converts.Int32ToInt16Array(12345);
            Assert.AreEqual<int>(2, res.Length);
            Assert.AreEqual<Int16>(12345, res[0]);
            Assert.AreEqual<Int16>(0, res[1]);

            Converts.Int32Endian = Global.eEndian.Big;
            res = Converts.Int32ToInt16Array(12345);
            Assert.AreEqual<int>(2, res.Length);
            Assert.AreEqual<Int16>(0, res[0]);
            Assert.AreEqual<Int16>(12345, res[1]);
        }

        [TestMethod]
        public void Test_Int32ToInt16Array_3()
        {
            Converts.Int32Endian = Global.eEndian.Little;
            Int16[] res = Converts.Int32ToInt16Array(2000111222);
            Assert.AreEqual<int>(2, res.Length);
            Assert.AreEqual<Int16>(0x4676, res[0]);
            Assert.AreEqual<Int16>(0x7737, res[1]);

            Converts.Int32Endian = Global.eEndian.Big;
            res = Converts.Int32ToInt16Array(2000111222);
            Assert.AreEqual<int>(2, res.Length);
            Assert.AreEqual<Int16>(0x7737, res[0]);
            Assert.AreEqual<Int16>(0x4676, res[1]);
        }

        [TestMethod]
        public void Test_Int32ToInt16Array_4()
        {
            Converts.Int32Endian = Global.eEndian.Little;
            Int16[] res = Converts.Int32ToInt16Array(-1222333444);
            Assert.AreEqual<int>(2, res.Length);
            Assert.AreEqual<Int16>(Helpers.ConvToInt16(0xABFC), res[0]);
            Assert.AreEqual<Int16>(Helpers.ConvToInt16(0xB724), res[1]);

            Converts.Int32Endian = Global.eEndian.Big;
            res = Converts.Int32ToInt16Array(-1222333444);
            Assert.AreEqual<int>(2, res.Length);
            Assert.AreEqual<Int16>(Helpers.ConvToInt16(0xB724), res[0]);
            Assert.AreEqual<Int16>(Helpers.ConvToInt16(0xABFC), res[1]);
        }

        [TestMethod]
        public void Test_Int32ToInt16Array_5()
        {
            Converts.Int32Endian = Global.eEndian.Little;
            Int16[] res = Converts.Int32ToInt16Array(2000111222, 2);
            Assert.AreEqual<int>(2, res.Length);
            Assert.AreEqual<Int16>(0x4676, res[0]);
            Assert.AreEqual<Int16>(0x7737, res[1]);

            Converts.Int32Endian = Global.eEndian.Big;
            res = Converts.Int32ToInt16Array(2000111222, 2);
            Assert.AreEqual<int>(2, res.Length);
            Assert.AreEqual<Int16>(0x7737, res[0]);
            Assert.AreEqual<Int16>(0x4676, res[1]);
        }

        [TestMethod]
        public void Test_Int32ToInt16Array_6()
        {
            Converts.Int32Endian = Global.eEndian.Little;
            Int16[] res = Converts.Int32ToInt16Array(12345, 1);
            Assert.AreEqual<int>(1, res.Length);
            Assert.AreEqual<Int16>(12345, res[0]);

            Converts.Int32Endian = Global.eEndian.Big;
            res = Converts.Int32ToInt16Array(12345, 1);
            Assert.AreEqual<int>(1, res.Length);
            Assert.AreEqual<Int16>(12345, res[0]);
        }

        [TestMethod]
        public void Test_Int32ToInt16Array_7()
        {
            Converts.Int32Endian = Global.eEndian.Little;
            Int16[] res = Converts.Int32ToInt16Array(-12345, 1);
            Assert.AreEqual<int>(1, res.Length);
            Assert.AreEqual<Int16>(-12345, res[0]);

            Converts.Int32Endian = Global.eEndian.Big;
            res = Converts.Int32ToInt16Array(-12345, 1);
            Assert.AreEqual<int>(1, res.Length);
            Assert.AreEqual<Int16>(-12345, res[0]);
        }
    }

    [TestClass]
    public class Test_UInt32ToInt16Array
    {
        // public static Int16[] UInt32ToInt16Array(UInt32 value32, Global.eEndian? endian = null)

        [TestInitialize]
        public void Setup()
        {
            // This runs before EACH test
            Converts.Int32Endian = Global.eEndian.Little;
            Converts.Float32Endian = Global.eEndian.Little;
            Converts.StringEndian = Global.eEndian.Little;
            Converts.StringEncodingName = "us-ascii";
        }

        [TestMethod]
        public void Test_UInt32ToInt16Array_1()
        {
            Converts.Int32Endian = Global.eEndian.Little;
            Int16[] res = Converts.UInt32ToInt16Array(0);
            Assert.AreEqual<int>(2, res.Length);
            Assert.AreEqual<Int16>(0, res[0]);
            Assert.AreEqual<Int16>(0, res[1]);

            Converts.Int32Endian = Global.eEndian.Big;
            res = Converts.UInt32ToInt16Array(0);
            Assert.AreEqual<int>(2, res.Length);
            Assert.AreEqual<Int16>(0, res[0]);
            Assert.AreEqual<Int16>(0, res[1]);
        }

        [TestMethod]
        public void Test_UInt32ToInt16Array_2()
        {
            Converts.Int32Endian = Global.eEndian.Little;
            Int16[] res = Converts.UInt32ToInt16Array(12345);
            Assert.AreEqual<int>(2, res.Length);
            Assert.AreEqual<Int16>(12345, res[0]);
            Assert.AreEqual<Int16>(0, res[1]);

            Converts.Int32Endian = Global.eEndian.Big;
            res = Converts.UInt32ToInt16Array(12345);
            Assert.AreEqual<int>(2, res.Length);
            Assert.AreEqual<Int16>(0, res[0]);
            Assert.AreEqual<Int16>(12345, res[1]);
        }

        [TestMethod]
        public void Test_UInt32ToInt16Array_3()
        {
            Converts.Int32Endian = Global.eEndian.Little;
            Int16[] res = Converts.UInt32ToInt16Array(4000111222);
            Assert.AreEqual<int>(2, res.Length);
            Assert.AreEqual<Int16>(Helpers.ConvToInt16(0xDA76), res[0]);
            Assert.AreEqual<Int16>(Helpers.ConvToInt16(0xEE6C), res[1]);

            Converts.Int32Endian = Global.eEndian.Big;
            res = Converts.UInt32ToInt16Array(4000111222);
            Assert.AreEqual<int>(2, res.Length);
            Assert.AreEqual<Int16>(Helpers.ConvToInt16(0xEE6C), res[0]);
            Assert.AreEqual<Int16>(Helpers.ConvToInt16(0xDA76), res[1]);
        }
    }

    [TestClass]
    public class Test_Float32ToInt16Array
    {
        public TestContext TestContext { get; set; }

        // public static Int16[] Float32ToInt16Array(float fValue)

        [TestInitialize]
        public void Setup()
        {
            // This runs before EACH test
            Converts.Int32Endian = Global.eEndian.Little;
            Converts.Float32Endian = Global.eEndian.Little;
            Converts.StringEndian = Global.eEndian.Little;
            Converts.StringEncodingName = "us-ascii";
        }

        [TestMethod]
        public void Test_Float32ToInt16Array_1()
        {
            Converts.Float32Endian = Global.eEndian.Little;
            Int16[] res = Converts.Float32ToInt16Array(0);
            Assert.AreEqual<int>(2, res.Length);
            Assert.AreEqual<Int16>(Helpers.ConvToInt16(0), res[0]);
            Assert.AreEqual<Int16>(Helpers.ConvToInt16(0), res[1]);

            Converts.Float32Endian = Global.eEndian.Big;
            res = Converts.Float32ToInt16Array(0);
            Assert.AreEqual<int>(2, res.Length);
            Assert.AreEqual<Int16>(Helpers.ConvToInt16(0), res[0]);
            Assert.AreEqual<Int16>(Helpers.ConvToInt16(0), res[1]);

            //maak en gebruik IntArraysAreAqual
        }

        [TestMethod]
        public void Test_Float32ToInt16Array_2()
        {
            Converts.Float32Endian = Global.eEndian.Little;
            Int16[] res = Converts.Float32ToInt16Array(3.1415f);
            Assert.AreEqual<int>(2, res.Length);
            Assert.AreEqual<Int16>(Helpers.ConvToInt16(0x0e56), res[0]);
            Assert.AreEqual<Int16>(Helpers.ConvToInt16(0x4049), res[1]);

            Converts.Float32Endian = Global.eEndian.Big;
            res = Converts.Float32ToInt16Array(3.1415f);
            Assert.AreEqual<int>(2, res.Length);
            Assert.AreEqual<Int16>(Helpers.ConvToInt16(0x4049), res[0]);
            Assert.AreEqual<Int16>(Helpers.ConvToInt16(0x0e56), res[1]);
        }

        [TestMethod]
        public void Test_Float32ToInt16Array_3()
        {
            Converts.Float32Endian = Global.eEndian.Little;
            Int16[] res = Converts.Float32ToInt16Array(-8000.9f);
            Assert.AreEqual<int>(2, res.Length);
            Assert.AreEqual<Int16>(Helpers.ConvToInt16(0x0733), res[0]);
            Assert.AreEqual<Int16>(Helpers.ConvToInt16(0xc5fa), res[1]);

            Converts.Float32Endian = Global.eEndian.Big;
            res = Converts.Float32ToInt16Array(-8000.9f);
            Assert.AreEqual<int>(2, res.Length);
            Assert.AreEqual<Int16>(Helpers.ConvToInt16(0xc5fa), res[0]);
            Assert.AreEqual<Int16>(Helpers.ConvToInt16(0x0733), res[1]);
        }

    }

    [TestClass]
    public class Test_property_StringEncodingName
    {
        public TestContext TestContext { get; set; }

        [TestInitialize]
        public void Setup()
        {
            // This runs before EACH test
            Converts.Int32Endian = Global.eEndian.Little;
            Converts.Float32Endian = Global.eEndian.Little;
            Converts.StringEndian = Global.eEndian.Little;
            Converts.StringEncodingName = "us-ascii";
        }

        [TestMethod]
        public void Test_GetAllSupportedStringEncodings_1()
        {
            // print all supported encodings

            string s = string.Format("{0,-10}{1,-30}{2, -50}{3}", "CodePage", "Name", "DisplayName", "\0 length");
            TestContext.WriteLine(s);
            foreach (var encinfo in Encoding.GetEncodings())
            {
                Encoding enc = encinfo.GetEncoding();
                byte[] zbytes = enc.GetBytes(new[] { '\0' });
                s = string.Format("{0,-10}{1,-30}{2, -50}{3}", encinfo.CodePage, encinfo.Name, encinfo.DisplayName, zbytes.Length);
                TestContext.WriteLine(s);
            }
        }

        // property string StringEncodingName

        [TestMethod]
        public void Test_prop_StringEncodingName_1()
        {
            // test acceptance of some encoding names

            Converts.StringEncodingName = "us-ascii";
            Converts.StringEncodingName = "Windows-1250";
            Converts.StringEncodingName = "Windows-1252";
            Converts.StringEncodingName = "iso-8859-1";
            Converts.StringEncodingName = "iso-8859-2";
            Converts.StringEncodingName = "utf-8";
        }

        [TestMethod]
        public void Test_prop_StringEncodingName_2()
        {
            // test a few case variants
            // Note: C# string encoding names are not case sensitive

            Converts.StringEncodingName = "US-ASCII";
            Converts.StringEncodingName = "US-ascii";
            Converts.StringEncodingName = "us-Ascii";
            Converts.StringEncodingName = "windows-1252";
            Converts.StringEncodingName = "WINDOWS-1252";
            Converts.StringEncodingName = "Iso-8859-1";
            Converts.StringEncodingName = "UTF-8";
            Converts.StringEncodingName = "Utf-8";
        }
        
        [TestMethod]
        public void Test_prop_StringEncodingName_10()
        {
            // test invalid encoding name

            Assert.ThrowsException<ArgumentException>(() =>
                Converts.StringEncodingName = "us-asci");
        }

        [TestMethod]
        public void Test_prop_StringEncodingName_11()
        {
            // test invalid encoding name

            Assert.ThrowsException<ArgumentException>(() =>
                Converts.StringEncodingName = "aaa");
        }
        
    }

    [TestClass]
    public class Test_IsSupportedStringEncoding
    {
        //public static bool IsSupportedStringEncoding(string encName)

        [TestInitialize]
        public void Setup()
        {
            // This runs before EACH test
            Converts.Int32Endian = Global.eEndian.Little;
            Converts.Float32Endian = Global.eEndian.Little;
            Converts.StringEndian = Global.eEndian.Little;
            Converts.StringEncodingName = "us-ascii";
        }

        [TestMethod]
        public void Test_IsSupportedStringEncoding_1()
        {
            bool result;

            result = Converts.IsSupportedStringEncoding("us-ascii");
            Assert.AreEqual<bool>(true, result);

            result = Converts.IsSupportedStringEncoding("US-ascii");
            Assert.AreEqual<bool>(true, result);

            result = Converts.IsSupportedStringEncoding("windows-1252");
            Assert.AreEqual<bool>(true, result);

            result = Converts.IsSupportedStringEncoding("WINDOWS-1252");
            Assert.AreEqual<bool>(true, result);
        }

        [TestMethod]
        public void Test_IsSupportedStringEncoding_2()
        {
            bool result;

            result = Converts.IsSupportedStringEncoding(null);
            Assert.AreEqual<bool>(false, result);

            result = Converts.IsSupportedStringEncoding("");
            Assert.AreEqual<bool>(false, result);

            result = Converts.IsSupportedStringEncoding("    ");
            Assert.AreEqual<bool>(false, result);

            result = Converts.IsSupportedStringEncoding("aa");
            Assert.AreEqual<bool>(false, result);

            result = Converts.IsSupportedStringEncoding("ascii");
            Assert.AreEqual<bool>(false, result);

            result = Converts.IsSupportedStringEncoding("WINDOWS-125");
            Assert.AreEqual<bool>(false, result);
        }
    }


    [TestClass]
    public class Test_StringEncodeToByteArray
    {
        // byte[] StringEncodeToByteArray(string str, string encodingName)

        [TestInitialize]
        public void Setup()
        {
            // This runs before EACH test
            Converts.Int32Endian = Global.eEndian.Little;
            Converts.Float32Endian = Global.eEndian.Little;
            Converts.StringEndian = Global.eEndian.Little;
            Converts.StringEncodingName = "us-ascii";
        }

        [TestMethod]
        public void Test_StringEncodeToByteArray_1()
        {
            // test Converts.StringEncodingName is not set
            Converts.Test_ResetStringEncodingName();
            byte[] ab;
            Assert.ThrowsException<Exception>(() =>
                ab = Converts.StringEncodeToByteArray("Test")
                );
        }

        [TestMethod]
        public void Test_StringEncodeToByteArray_10()
        {
            Converts.StringEncodingName = "us-ascii";
            byte[] ab = Converts.StringEncodeToByteArray("This is a test");
        }

        [TestMethod]
        public void Test_StringEncodeToByteArray_11()
        {
            Converts.StringEncodingName = "us-ascii";
            byte[] ab = Converts.StringEncodeToByteArray("");
        }

        [TestMethod]
        public void Test_StringEncodeToByteArray_12()
        {
            Converts.StringEncodingName = "us-ascii";
            byte[] ab = Converts.StringEncodeToByteArray("A");
        }

        [TestMethod]
        public void Test_StringEncodeToByteArray_13()
        {
            // test long conversion
            Converts.StringEncodingName = "us-ascii";
            byte[] ab = Converts.StringEncodeToByteArray("Lorem ipsum dolor sit amet, consectetur adipiscing elit. Cras consectetur sem id tincidunt lacinia. Phasellus laoreet convallis augue eget feugiat. Nullam venenatis lobortis placerat. Mauris vel neque non massa feugiat porttitor vel in dui. Donec in feugiat libero. Nulla eget rhoncus sapien blandit.");
        }

        [TestMethod]
        public void Test_StringEncodeToByteArray_20()
        {
            Converts.StringEncodingName = "us-ascii";
            byte[] ab;
            Assert.ThrowsException<EncoderFallbackException>(() =>
                ab = Converts.StringEncodeToByteArray("Non-ascii characters é ")
                );
        }

        [TestMethod]
        public void Test_StringEncodeToByteArray_21()
        {
            Converts.StringEncodingName = "us-ascii";
            byte[] ab;
            Assert.ThrowsException<EncoderFallbackException>(() =>
                ab = Converts.StringEncodeToByteArray("Non-ascii characters °  ")
                );
        }

        [TestMethod]
        public void Test_StringEncodeToByteArray_30()
        {
            Converts.StringEncodingName = "Windows-1252";
            byte[] ab = Converts.StringEncodeToByteArray("");
            byte[] exp = { };
            Helpers.TestByteArraysAreEqual(exp, ab);
        }

        [TestMethod]
        public void Test_StringEncodeToByteArray_31()
        {
            Converts.StringEncodingName = "Windows-1252";
            byte[] ab = Converts.StringEncodeToByteArray(" ");
            byte[] exp = { 0x20 };
            Helpers.TestByteArraysAreEqual(exp, ab);
        }

        [TestMethod]
        public void Test_StringEncodeToByteArray_32()
        {
            Converts.StringEncodingName = "Windows-1252";
            byte[] ab = Converts.StringEncodeToByteArray("Non-ascii characters é ° ç ø ");
            //string abAsString = Converts.ByteArrayToStringOfValues(ab);
            byte[] exp = { 0x4E, 0x6F, 0x6E, 0x2D, 0x61, 0x73, 0x63, 0x69, 0x69, 0x20, 0x63, 0x68, 0x61, 0x72, 0x61, 0x63, 0x74, 0x65, 0x72, 0x73, 0x20, 0xE9, 0x20, 0xB0, 0x20, 0xE7, 0x20, 0xF8, 0x20 };
            Helpers.TestByteArraysAreEqual(exp, ab);
        }

        [TestMethod]
        public void Test_StringEncodeToByteArray_40()
        {
            Converts.StringEncodingName = "Windows-1252";
            byte[] ab;
            Assert.ThrowsException<EncoderFallbackException>(() =>
                ab = Converts.StringEncodeToByteArray("Non-Windows-1252 character é ° ç ø 💡")
                );
        }

        [TestMethod]
        public void Test_StringEncodeToByteArray_50()
        {
            Converts.StringEncodingName = "UTF-8";
            byte[] ab = Converts.StringEncodeToByteArray("UTF8 encoded string 💡 Ж ह 字 😊");
            //string abAsString = Converts.ByteArrayToStringOfValues(ab);
            byte[] exp = { 0x55, 0x54, 0x46, 0x38, 0x20, 0x65, 0x6E, 0x63, 0x6F, 0x64, 0x65, 0x64, 0x20, 0x73, 0x74, 0x72, 0x69, 0x6E, 0x67, 0x20, 0xF0, 0x9F, 0x92, 0xA1, 0x20, 0xD0, 0x96, 0x20, 0xE0, 0xA4, 0xB9, 0x20, 0xE5, 0xAD, 0x97, 0x20, 0xF0, 0x9F, 0x98, 0x8A };
            Helpers.TestByteArraysAreEqual(exp, ab);
        }
    }

    [TestClass]
    public class Test_ByteArrayDecodeToString
    {
        // string ByteArrayDecodeToString(byte[] aBytes)

        [TestInitialize]
        public void Setup()
        {
            // This runs before EACH test
            Converts.Int32Endian = Global.eEndian.Little;
            Converts.Float32Endian = Global.eEndian.Little;
            Converts.StringEndian = Global.eEndian.Little;
            Converts.StringEncodingName = "us-ascii";
        }

        [TestMethod]
        public void Test_ByteArrayDecodeToString_1()
        {
            // test Converts.StringEncodingName is not set
            Converts.Test_ResetStringEncodingName();
            byte[] ab = { 0x41, 0x42, 0x43 };
            string s;
            Assert.ThrowsException<Exception>(() =>
                s = Converts.ByteArrayDecodeToString(ab)
                );
        }

        [TestMethod]
        public void Test_ByteArrayDecodeToString_10()
        {
            Converts.StringEncodingName = "us-ascii";
            byte[] ab = { 0x4C, 0x6F, 0x72, 0x65, 0x6D, 0x20, 0x69, 0x70, 0x73, 0x75, 0x6D, 0x20, 0x64, 0x6F, 0x6C, 0x6F, 0x72, 0x20, 0x73, 0x69, 0x74, 0x20, 0x61, 0x6D, 0x65, 0x74 };
            string s;
            s = Converts.ByteArrayDecodeToString(ab);
            Assert.AreEqual<string>("Lorem ipsum dolor sit amet", s);
        }

        [TestMethod]
        public void Test_ByteArrayDecodeToString_11()
        {
            // test bytes sequence invalid, used replacement character
            Converts.StringEncodingName = "us-ascii";
            byte[] ab = { 0x54, 0x65, 0x73, 0x74, 0x20, 0xA2 };
            string s;
            s = Converts.ByteArrayDecodeToString(ab);
            Assert.AreEqual<string>("Test �", s);
        }

        [TestMethod]
        public void Test_ByteArrayDecodeToString_12()
        {
            // test bytes sequence invalid, used replacement character
            Converts.StringEncodingName = "Windows-1252";
            byte[] ab = { 0x54, 0x65, 0x73, 0x74, 0x20, 0xA2 };
            string s;
            s = Converts.ByteArrayDecodeToString(ab);
            Assert.AreEqual<string>("Test ¢", s);
        }

        [TestMethod]
        public void Test_ByteArrayDecodeToString_13()
        {
            // test UTF-8 characters
            Converts.StringEncodingName = "utf-8";
            byte[] ab = { 0x54, 0x65, 0x73, 0x74, 0x20, 0xE5, 0xAD, 0x97, 0x20, 0xF0, 0x9F, 0x98, 0x8A };
            string s;
            s = Converts.ByteArrayDecodeToString(ab);
            Assert.AreEqual<string>("Test 字 😊", s);
        }

    }

    [TestClass]
    public class Test_Int16ArrayDecodeToString
    {
        // string ByteArrayDecodeToString(byte[] aBytes)

        [TestInitialize]
        public void Setup()
        {
            // This runs before EACH test
            Converts.Int32Endian = Global.eEndian.Little;
            Converts.Float32Endian = Global.eEndian.Little;
            Converts.StringEndian = Global.eEndian.Little;
            Converts.StringEncodingName = "us-ascii";
        }

        [TestMethod]
        public void Test_Int16ArrayDecodeToString_1()
        {

            // test Converts.StringEncodingName is not set
            Converts.Test_ResetStringEncodingName();
            Int16[] ar = { 0x4142, 0x4344, 0x0000 };
            string s;
            Assert.ThrowsException<Exception>(() =>
                s = Converts.Int16ArrayDecodeToString(ar)
                );
        }

        [TestMethod]
        public void Test_Int16ArrayDecodeToString_2()
        {
            Converts.StringEncodingName = "us-ascii";
            Converts.StringEndian = Global.eEndian.Big;
            Int16[] ar = { 0x4142, 0x4344, 0x0000 };
            string s = Converts.Int16ArrayDecodeToString(ar);
            Assert.AreEqual<string>("ABCD", s);
        }

        [TestMethod]
        public void Test_Int16ArrayDecodeToString_3()
        {
            Converts.StringEncodingName = "us-ascii";
            Converts.StringEndian = Global.eEndian.Big;
            Int16[] ar = { 0x4142, 0x4344, 0x0045, 0x4647 };
            string s = Converts.Int16ArrayDecodeToString(ar);
            Assert.AreEqual<string>("ABCD", s);
        }

        [TestMethod]
        public void Test_Int16ArrayDecodeToString_4()
        {
            Converts.StringEncodingName = "us-ascii";
            Converts.StringEndian = Global.eEndian.Big;
            Int16[] ar = { 0x4142, 0x4344, 0x4500, 0x4647 };
            string s = Converts.Int16ArrayDecodeToString(ar);
            Assert.AreEqual<string>("ABCDE", s);
        }

        [TestMethod]
        public void Test_Int16ArrayDecodeToString_5()
        {
            Converts.StringEncodingName = "us-ascii";
            Converts.StringEndian = Global.eEndian.Little;
            Int16[] ar = { 0x4142, 0x4344, 0x0045, 0x4647 };
            string s = Converts.Int16ArrayDecodeToString(ar);
            Assert.AreEqual<string>("BADCE", s);
        }

        [TestMethod]
        public void Test_Int16ArrayDecodeToString_6()
        {
            Converts.StringEncodingName = "us-ascii";
            Converts.StringEndian = Global.eEndian.Little;
            Int16[] ar = { 0x4142, 0x4344, 0x4500, 0x4647 };
            string s = Converts.Int16ArrayDecodeToString(ar);
            Assert.AreEqual<string>("BADC", s);
        }

        [TestMethod]
        public void Test_Int16ArrayDecodeToString_10()
        {
            // Test "Hello 世界 🌍";
            Converts.StringEncodingName = "utf-8";
            Converts.StringEndian = Global.eEndian.Big;
            Int16[] ar = { 0x4865, 0x6C6C, 0x6F20, Helpers.ConvToInt16(0xE4B8), Helpers.ConvToInt16(0x96E7), Helpers.ConvToInt16(0x958C), 0x20F0, Helpers.ConvToInt16(0x9F8C), Helpers.ConvToInt16(0x8D00) };
            string s = Converts.Int16ArrayDecodeToString(ar);
            Assert.AreEqual<string>("Hello 世界 🌍", s);
        }

        [TestMethod]
        public void Test_Int16ArrayDecodeToString_11()
        {
            // Test "Hello 世界 🌍";
            Converts.StringEncodingName = "utf-8";
            Converts.StringEndian = Global.eEndian.Little;
            Int16[] ar = { 0x6548, 0x6C6C, 0x206F, Helpers.ConvToInt16(0xB8E4), Helpers.ConvToInt16(0xE796), Helpers.ConvToInt16(0x8C95), Helpers.ConvToInt16(0xF020), Helpers.ConvToInt16(0x8C9F), 0x008D };
            string s = Converts.Int16ArrayDecodeToString(ar);
            Assert.AreEqual<string>("Hello 世界 🌍", s);
        }

        [TestMethod]
        public void Test_Int16ArrayDecodeToString_12()
        {
            // Test with characters after \x00
            Converts.StringEncodingName = "utf-8";
            Converts.StringEndian = Global.eEndian.Big;
            Int16[] ar = { 0x4865, 0x6C6C, 0x6F20, Helpers.ConvToInt16(0xE4B8), Helpers.ConvToInt16(0x96E7), Helpers.ConvToInt16(0x958C), 0x20F0, Helpers.ConvToInt16(0x9F8C), Helpers.ConvToInt16(0x8D00), 0x4142 };
            string s = Converts.Int16ArrayDecodeToString(ar);
            Assert.AreEqual<string>("Hello 世界 🌍", s);
        }

        [TestMethod]
        public void Test_Int16ArrayDecodeToString_13()
        {
            // Test with characters after \x00
            Converts.StringEncodingName = "utf-8";
            Converts.StringEndian = Global.eEndian.Little;
            Int16[] ar = { 0x6548, 0x6C6C, 0x206F, Helpers.ConvToInt16(0xB8E4), Helpers.ConvToInt16(0xE796), Helpers.ConvToInt16(0x8C95), Helpers.ConvToInt16(0xF020), Helpers.ConvToInt16(0x8C9F), 0x008D, 0x4142 };
            string s = Converts.Int16ArrayDecodeToString(ar);
            Assert.AreEqual<string>("Hello 世界 🌍", s);
        }

    }

    [TestClass]
    public class Test_StringEncodeToInt16Array
    {
        // public static Int16[] StringEncodeToInt16Array(string str)
        // returns as many Int16 as needed

        [TestInitialize]
        public void Setup()
        {
            // This runs before EACH test
            Converts.Int32Endian = Global.eEndian.Little;
            Converts.Float32Endian = Global.eEndian.Little;
            Converts.StringEndian = Global.eEndian.Little;
            Converts.StringEncodingName = "us-ascii";
        }

        [TestMethod]
        public void Test_StringEncodeToInt16Array_1()
        {
            // test Converts.StringEncodingName is not set
            Converts.Test_ResetStringEncodingName();
            Int16[] ar;
            Assert.ThrowsException<Exception>(() =>
                ar = Converts.StringEncodeToInt16Array("a", 2)
                );
        }

        [TestMethod]
        public void Test_StringEncodeToInt16Array_2()
        {
            // test empty string
            Converts.StringEncodingName = "us-ascii";
            Int16[] ar;
            ar = Converts.StringEncodeToInt16Array("", 2);
            Assert.AreEqual<int>(1, ar.Length);
            Assert.AreEqual<Int16>(0x0000, ar[0]);
        }

        [TestMethod]
        public void Test_StringEncodeToInt16Array_3()
        {
            Converts.StringEncodingName = "us-ascii";
            Converts.StringEndian = Global.eEndian.Little;
            Int16[] ar;
            ar = Converts.StringEncodeToInt16Array("A", 1);
            Assert.AreEqual<int>(1, ar.Length);
            Assert.AreEqual<Int16>(0x0041, ar[0]);

            Converts.StringEndian = Global.eEndian.Big;
            ar = Converts.StringEncodeToInt16Array("A", 1);
            Assert.AreEqual<int>(1, ar.Length);
            Assert.AreEqual<Int16>(0x4100, ar[0]);
        }

        [TestMethod]
        public void Test_StringEncodeToInt16Array_3b()
        {
            Converts.StringEncodingName = "us-ascii";
            Converts.StringEndian = Global.eEndian.Little;
            Int16[] ar;
            Assert.ThrowsException<ArgumentException>(() =>
                    ar = Converts.StringEncodeToInt16Array("AB", 1)
                    );
        }

        [TestMethod]
        public void Test_StringEncodeToInt16Array_4()
        {
            Converts.StringEncodingName = "us-ascii";
            Converts.StringEndian = Global.eEndian.Little;
            Int16[] ar;
            ar = Converts.StringEncodeToInt16Array("AB", 2);
            Assert.AreEqual<int>(2, ar.Length);
            Assert.AreEqual<Int16>(0x4241, ar[0]);
            Assert.AreEqual<Int16>(0x0000, ar[1]);

            Converts.StringEndian = Global.eEndian.Big;
            ar = Converts.StringEncodeToInt16Array("AB", 2);
            Assert.AreEqual<int>(2, ar.Length);
            Assert.AreEqual<Int16>(0x4142, ar[0]);
            Assert.AreEqual<Int16>(0x0000, ar[1]);
        }

        [TestMethod]
        public void Test_StringEncodeToInt16Array_5a()
        {
            Converts.StringEncodingName = "us-ascii";
            Converts.StringEndian = Global.eEndian.Little;
            Int16[] ar;
            Assert.ThrowsException<ArgumentException>(() =>
                    ar = Converts.StringEncodeToInt16Array("ABC", 1)
                    );
        }


        [TestMethod]
        public void Test_StringEncodeToInt16Array_5b()
        {
            Converts.StringEncodingName = "us-ascii";
            Converts.StringEndian = Global.eEndian.Little;
            Int16[] ar;
            ar = Converts.StringEncodeToInt16Array("ABC", 3);
            Assert.AreEqual<int>(2, ar.Length);
            Assert.AreEqual<Int16>(0x4241, ar[0]);
            Assert.AreEqual<Int16>(0x0043, ar[1]);

            Converts.StringEndian = Global.eEndian.Big;
            ar = Converts.StringEncodeToInt16Array("ABC", 3);
            Assert.AreEqual<int>(2, ar.Length);
            Assert.AreEqual<Int16>(0x4142, ar[0]);
            Assert.AreEqual<Int16>(0x4300, ar[1]);
        }

        [TestMethod]
        public void Test_StringEncodeToInt16Array_6()
        {
            Converts.StringEncodingName = "us-ascii";
            Converts.StringEndian = Global.eEndian.Little;
            Int16[] ar;
            ar = Converts.StringEncodeToInt16Array("ABC D E F ", 10);
            Assert.AreEqual<int>(6, ar.Length);
            Assert.AreEqual<Int16>(0x4241, ar[0]);
            Assert.AreEqual<Int16>(0x2043, ar[1]);
            Assert.AreEqual<Int16>(0x2044, ar[2]);
            Assert.AreEqual<Int16>(0x2045, ar[3]);
            Assert.AreEqual<Int16>(0x2046, ar[4]);
            Assert.AreEqual<Int16>(0x0000, ar[5]);

            Converts.StringEndian = Global.eEndian.Big;
            ar = Converts.StringEncodeToInt16Array("ABC D E F ", 10);
            Assert.AreEqual<int>(6, ar.Length);
            Assert.AreEqual<Int16>(0x4142, ar[0]);
            Assert.AreEqual<Int16>(0x4320, ar[1]);
            Assert.AreEqual<Int16>(0x4420, ar[2]);
            Assert.AreEqual<Int16>(0x4520, ar[3]);
            Assert.AreEqual<Int16>(0x4620, ar[4]);
            Assert.AreEqual<Int16>(0x0000, ar[5]);
        }

        [TestMethod]
        public void Test_StringEncodeToInt16Array_6b()
        {
            Converts.StringEncodingName = "us-ascii";
            Converts.StringEndian = Global.eEndian.Little;
            Int16[] ar;
            ar = Converts.StringEncodeToInt16Array("ABC D E F G", 10);
            Assert.AreEqual<int>(6, ar.Length);
            Assert.AreEqual<Int16>(0x4241, ar[0]);
            Assert.AreEqual<Int16>(0x2043, ar[1]);
            Assert.AreEqual<Int16>(0x2044, ar[2]);
            Assert.AreEqual<Int16>(0x2045, ar[3]);
            Assert.AreEqual<Int16>(0x2046, ar[4]);
            Assert.AreEqual<Int16>(0x0047, ar[5]);

            Converts.StringEndian = Global.eEndian.Big;
            ar = Converts.StringEncodeToInt16Array("ABC D E F G", 10);
            Assert.AreEqual<int>(6, ar.Length);
            Assert.AreEqual<Int16>(0x4142, ar[0]);
            Assert.AreEqual<Int16>(0x4320, ar[1]);
            Assert.AreEqual<Int16>(0x4420, ar[2]);
            Assert.AreEqual<Int16>(0x4520, ar[3]);
            Assert.AreEqual<Int16>(0x4620, ar[4]);
            Assert.AreEqual<Int16>(0x4700, ar[5]);
        }

        [TestMethod]
        public void Test_StringEncodeToInt16Array_7()
        {
            Converts.StringEncodingName = "UTF-8";
            Converts.StringEndian = Global.eEndian.Little;
            Int16[] ar;
            ar = Converts.StringEncodeToInt16Array("Hello 世界 🌍", 9);
            Assert.AreEqual<int>(9, ar.Length);
            Assert.AreEqual<Int16>(Helpers.ConvToInt16(0x6548), ar[0]);
            Assert.AreEqual<Int16>(Helpers.ConvToInt16(0x6C6C), ar[1]);
            Assert.AreEqual<Int16>(Helpers.ConvToInt16(0x206F), ar[2]);
            Assert.AreEqual<Int16>(Helpers.ConvToInt16(0xB8E4), ar[3]);
            Assert.AreEqual<Int16>(Helpers.ConvToInt16(0xE796), ar[4]);
            Assert.AreEqual<Int16>(Helpers.ConvToInt16(0x8C95), ar[5]);
            Assert.AreEqual<Int16>(Helpers.ConvToInt16(0xF020), ar[6]);
            Assert.AreEqual<Int16>(Helpers.ConvToInt16(0x8C9F), ar[7]);
            Assert.AreEqual<Int16>(Helpers.ConvToInt16(0x008D), ar[8]);

            Converts.StringEndian = Global.eEndian.Big;
            ar = Converts.StringEncodeToInt16Array("Hello 世界 🌍", 20);
            Assert.AreEqual<int>(9, ar.Length);
            Assert.AreEqual<Int16>(Helpers.ConvToInt16(0x4865), ar[0]);
            Assert.AreEqual<Int16>(Helpers.ConvToInt16(0x6C6C), ar[1]);
            Assert.AreEqual<Int16>(Helpers.ConvToInt16(0x6F20), ar[2]);
            Assert.AreEqual<Int16>(Helpers.ConvToInt16(0xE4B8), ar[3]);
            Assert.AreEqual<Int16>(Helpers.ConvToInt16(0x96E7), ar[4]);
            Assert.AreEqual<Int16>(Helpers.ConvToInt16(0x958C), ar[5]);
            Assert.AreEqual<Int16>(Helpers.ConvToInt16(0x20F0), ar[6]);
            Assert.AreEqual<Int16>(Helpers.ConvToInt16(0x9F8C), ar[7]);
            Assert.AreEqual<Int16>(Helpers.ConvToInt16(0x8D00), ar[8]);
        }

        [TestMethod]
        public void Test_StringEncodeToInt16Array_8()
        {
            Converts.StringEncodingName = "UTF-8";
            Converts.StringEndian = Global.eEndian.Little;
            Int16[] ar;
            ar = Converts.StringEncodeToInt16Array("Hello 世界 🌍A", 10);
            Assert.AreEqual<int>(10, ar.Length);
            Assert.AreEqual<Int16>(Helpers.ConvToInt16(0x6548), ar[0]);
            Assert.AreEqual<Int16>(Helpers.ConvToInt16(0x6C6C), ar[1]);
            Assert.AreEqual<Int16>(Helpers.ConvToInt16(0x206F), ar[2]);
            Assert.AreEqual<Int16>(Helpers.ConvToInt16(0xB8E4), ar[3]);
            Assert.AreEqual<Int16>(Helpers.ConvToInt16(0xE796), ar[4]);
            Assert.AreEqual<Int16>(Helpers.ConvToInt16(0x8C95), ar[5]);
            Assert.AreEqual<Int16>(Helpers.ConvToInt16(0xF020), ar[6]);
            Assert.AreEqual<Int16>(Helpers.ConvToInt16(0x8C9F), ar[7]);
            Assert.AreEqual<Int16>(Helpers.ConvToInt16(0x418D), ar[8]);
            Assert.AreEqual<Int16>(Helpers.ConvToInt16(0x0000), ar[9]);

            Converts.StringEndian = Global.eEndian.Big;
            ar = Converts.StringEncodeToInt16Array("Hello 世界 🌍", 9);
            Assert.AreEqual<int>(9, ar.Length);
            Assert.AreEqual<Int16>(Helpers.ConvToInt16(0x4865), ar[0]);
            Assert.AreEqual<Int16>(Helpers.ConvToInt16(0x6C6C), ar[1]);
            Assert.AreEqual<Int16>(Helpers.ConvToInt16(0x6F20), ar[2]);
            Assert.AreEqual<Int16>(Helpers.ConvToInt16(0xE4B8), ar[3]);
            Assert.AreEqual<Int16>(Helpers.ConvToInt16(0x96E7), ar[4]);
            Assert.AreEqual<Int16>(Helpers.ConvToInt16(0x958C), ar[5]);
            Assert.AreEqual<Int16>(Helpers.ConvToInt16(0x20F0), ar[6]);
            Assert.AreEqual<Int16>(Helpers.ConvToInt16(0x9F8C), ar[7]);
            Assert.AreEqual<Int16>(Helpers.ConvToInt16(0x8D00), ar[8]);
        }

        [TestMethod]
        public void Test_StringEncodeToInt16Array_9()
        {
            Converts.StringEncodingName = "UTF-8";
            Converts.StringEndian = Global.eEndian.Little;
            Int16[] ar;
            Assert.ThrowsException<ArgumentException>(() =>
                    ar = Converts.StringEncodeToInt16Array("Hello 世界 🌍", 8)
                    );
        }

    }
}


