using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModbusControlSpace;

namespace UnitTests
{
    [TestClass]
    public class Test_ToInt16
    {
        // public static Int16 ToInt16(byte byte0, byte byte1)

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
    public class Test_ToUInt1616
    {
        // public static UInt16 ToUInt16(byte byte0, byte byte1, Global.eEndian endian = Global.eEndian.Little)

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
            Int16[] aInt16 = { 1234};
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
    public class Test_IntToString
    {
        // public static string IntToString(Int32 value, int decimals = 0, int minLen = 0)

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

        [TestMethod]
        public void Test_Int16RegistersToValue32_Int16_1()
        {
            Int16[] aInt16 = new Int16[] { 0x1234 };
            Converts.Int32Endian = Global.eEndian.Little;
            UInt32 result = Converts.Int16ArrayToUInt32Value(aInt16, 1, ModbusControlSpace.Global.eType.Int);
            Assert.AreEqual<UInt32>((UInt32)0x1234, result);
        }

        [TestMethod]
        public void Test_Int16RegistersToValue32_Int16_1b()
        {
            Int16[] aInt16 = new Int16[] { Helpers.ConvToInt16(0x1234), 0x5678 };
            Converts.Int32Endian = Global.eEndian.Big;
            UInt32 result = Converts.Int16ArrayToUInt32Value(aInt16, 1, ModbusControlSpace.Global.eType.Int);
            Assert.AreEqual<UInt32>((UInt32)0x1234, result);
        }

        [TestMethod]
        public void Test_Int16RegistersToValue32_Int32()
        {
            Int16[] aInt16 = new Int16[] { Helpers.ConvToInt16(0xAAAA), Helpers.ConvToInt16(0xBBBB) };
            Converts.Int32Endian = Global.eEndian.Little;
            UInt32 result = Converts.Int16ArrayToUInt32Value(aInt16, 2, ModbusControlSpace.Global.eType.Int);
            Assert.AreEqual<UInt32>(0xBBBBAAAA, result);

            Converts.Int32Endian = Global.eEndian.Big;
            result = Converts.Int16ArrayToUInt32Value(aInt16, 2, ModbusControlSpace.Global.eType.Int);
            Assert.AreEqual<UInt32>(0xAAAABBBB, result);
        }

        [TestMethod]
        public void Test_Int16RegistersToValue32_Float32_1_0()
        {
            Int16[] aInt16 = new Int16[] { 0x3F80, 0x0010 };
            Converts.Float32Endian = Global.eEndian.Little;
            UInt32 result = Converts.Int16ArrayToUInt32Value(aInt16, 2, ModbusControlSpace.Global.eType.Float);
            Assert.AreEqual<UInt32>(0x00103F80, result); // IEEE 754 for 1.0

            Converts.Float32Endian = Global.eEndian.Big;
            result = Converts.Int16ArrayToUInt32Value(aInt16, 2, ModbusControlSpace.Global.eType.Float);
            Assert.AreEqual<UInt32>(0x3F800010, result); // IEEE 754 for 1.0
        }

        [TestMethod]
        public void Test_Int16RegistersToValue32_Float32_Negative()
        {
            Int16[] aInt16 = new Int16[] { Helpers.ConvToInt16(0xBF80), 0x000A };
            Converts.Float32Endian = Global.eEndian.Little;
            UInt32 result = Converts.Int16ArrayToUInt32Value(aInt16, 2, ModbusControlSpace.Global.eType.Float);
            Assert.AreEqual<UInt32>(0x000ABF80, result); // IEEE 754 for -1.0

            aInt16 = new Int16[] { 0x000A, Helpers.ConvToInt16(0xBF80) };
            Converts.Float32Endian = Global.eEndian.Big;
            result = Converts.Int16ArrayToUInt32Value(aInt16, 2, ModbusControlSpace.Global.eType.Float);
            Assert.AreEqual<UInt32>(0x000ABF80, result); // IEEE 754 for -1.0
        }

        [TestMethod]
        public void Test_Int16RegistersToValue32_Ascii32_1()
        {
            Int16[] aInt16 = new Int16[] { 0x4142, 0x4344 };
            Converts.Int32Endian = Global.eEndian.Little;
            UInt32 result = Converts.Int16ArrayToUInt32Value(aInt16, 2, ModbusControlSpace.Global.eType.ASCII);
            Assert.AreEqual<UInt32>(0x43444142, result); // "ABCD", little-endian

            // Int32BigEndian does not matter
            Converts.Int32Endian = Global.eEndian.Big;
            result = Converts.Int16ArrayToUInt32Value(aInt16, 2, ModbusControlSpace.Global.eType.ASCII);
            Assert.AreEqual<UInt32>(0x43444142, result); // "ABCD", little-endian
        }


        [TestMethod]
        public void Test_Int16RegistersToValue32_Ascii16_2()
        {
            Int16[] aInt16 = new Int16[] { 0x0041 };
            // StringBigEndian does not matter
            Converts.StringEndian = Global.eEndian.Little;
            UInt32 result = Converts.Int16ArrayToUInt32Value(aInt16, 1, ModbusControlSpace.Global.eType.ASCII);
            Assert.AreEqual<UInt32>(0x41u, result); // 'A'

            Converts.StringEndian = Global.eEndian.Big;
            result = Converts.Int16ArrayToUInt32Value(aInt16, 1, ModbusControlSpace.Global.eType.ASCII);
            Assert.AreEqual<UInt32>(0x41u, result); // 'A'
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_Int16RegistersToValue32_Excep()
        {
            Int16[] aInt16 = new Int16[] { 0x1234 };
            Converts.Int32Endian = Global.eEndian.Little;
            Converts.Int16ArrayToUInt32Value(aInt16, 1, (ModbusControlSpace.Global.eType)999);
        }

        [TestMethod]
        public void Test_Int16RegistersToValue32_Int32_8()
        {
            Int16[] aInt16 = new Int16[] { 0x0000, 0x0000 };
            Converts.Int32Endian = Global.eEndian.Little;
            UInt32 result = Converts.Int16ArrayToUInt32Value(aInt16, 1, ModbusControlSpace.Global.eType.Int);
            Assert.AreEqual<UInt32>(0x00000000, result);

            Converts.Int32Endian = Global.eEndian.Little;
            result = Converts.Int16ArrayToUInt32Value(aInt16, 1, ModbusControlSpace.Global.eType.Int);
            Assert.AreEqual<UInt32>(0x00000000, result);
        }

        [TestMethod]
        public void Test_Int16RegistersToValue32_Float32_Max()
        {
            Int16[] aInt16 = new Int16[] { 0x7F7F, Helpers.ConvToInt16(0xFFFF) };
            Converts.Int32Endian = Global.eEndian.Little;
            UInt32 result = Converts.Int16ArrayToUInt32Value(aInt16, 2, ModbusControlSpace.Global.eType.Float);
            Assert.AreEqual<UInt32>(0x7F7FFFFF, result); // Largest positive float32

            Converts.Int32Endian = Global.eEndian.Big;
            result = Converts.Int16ArrayToUInt32Value(aInt16, 2, ModbusControlSpace.Global.eType.Float);
            Assert.AreEqual<UInt32>(0x7F7FFFFF, result); // Largest positive float32
        }

        [TestMethod]
        public void Test_Int16RegistersToValue32_Ascii32_NullBytes()
        {
            Int16[] aInt16 = new Int16[] { 0x0000, 0x0000 };
            Converts.Int32Endian = Global.eEndian.Little;
            UInt32 result = Converts.Int16ArrayToUInt32Value(aInt16, 2, ModbusControlSpace.Global.eType.ASCII);
            Assert.AreEqual<UInt32>(0x00000000, result);

            Converts.Int32Endian = Global.eEndian.Big;
            result = Converts.Int16ArrayToUInt32Value(aInt16, 2, ModbusControlSpace.Global.eType.ASCII);
            Assert.AreEqual<UInt32>(0x00000000, result);
        }

        [TestMethod]
        public void Test_Int16RegistersToValue32_Int16_Width16_SignedConversion()
        {
            Int16[] aInt16 = new Int16[] { Helpers.ConvToInt16(0xFFFF) };
            Converts.Int32Endian = Global.eEndian.Little;
            UInt32 result = Converts.Int16ArrayToUInt32Value(aInt16, 1, ModbusControlSpace.Global.eType.Int);
            Assert.AreEqual<UInt32>(0x0000FFFFu, result); // Sign extended

            Converts.Int32Endian = Global.eEndian.Big;
            result = Converts.Int16ArrayToUInt32Value(aInt16, 1, ModbusControlSpace.Global.eType.Int);
            Assert.AreEqual<UInt32>(0x0000FFFFu, result); // Sign extended
        }

        [TestMethod]
        public void Test_Int16RegistersToValue32_Float32_Zero()
        {
            Int16[] aInt16 = new Int16[] { 0x0000, 0x0000 };
            Converts.Int32Endian = Global.eEndian.Little;
            UInt32 result = Converts.Int16ArrayToUInt32Value(aInt16, 2, ModbusControlSpace.Global.eType.Float);
            Assert.AreEqual<UInt32>(0x00000000u, result); // 0.0

            Converts.Int32Endian = Global.eEndian.Big;
            result = Converts.Int16ArrayToUInt32Value(aInt16, 2, ModbusControlSpace.Global.eType.Float);
            Assert.AreEqual<UInt32>(0x00000000u, result); // 0.0
        }
    }

    [TestClass]
    public class Test_AsciiStringToUInt32
    {
        // public static UInt32 AsciiStringToUInt32(string str)

        [TestMethod]
        public void Test_AsciiStringToUInt32_1()
        {
            Assert.ThrowsException<ArgumentException>(() =>
                Converts.AsciiStringToUInt32("")
                );
        }

        [TestMethod]
        public void Test_AsciiStringToUInt32_2()
        {
            Converts.StringEndian = Global.eEndian.Big; // no effect
            UInt32 result = Converts.AsciiStringToUInt32("\"\"");
            Assert.AreEqual<UInt32>(0x0000, result);

            Converts.StringEndian = Global.eEndian.Little; // no effect
            result = Converts.AsciiStringToUInt32("\"\"");
            Assert.AreEqual<UInt32>(0x0000, result);
        }

        [TestMethod]
        public void Test_AsciiStringToUInt32_3()
        {
            Converts.StringEndian = Global.eEndian.Big; // no effect
            UInt32 result = Converts.AsciiStringToUInt32("\"A\x01\"");
            Assert.AreEqual<UInt32>(0x0141, result);

            Converts.StringEndian = Global.eEndian.Little; // no effect
            result = Converts.AsciiStringToUInt32("\"A\x01\"");
            Assert.AreEqual<UInt32>(0x0141, result);
        }

        [TestMethod]
        public void Test_AsciiStringToUInt32_4()
        {
            UInt32 result = Converts.AsciiStringToUInt32("\"AB\"");
            Assert.AreEqual<UInt32>(0x4241, result);
        }

        [TestMethod]
        public void Test_AsciiStringToUInt32_5()
        {
            UInt32 result = Converts.AsciiStringToUInt32("\"ABC\"");
            Assert.AreEqual<UInt32>(0x00434241, result);
        }

        [TestMethod]
        public void Test_AsciiStringToUInt32_6()
        {
            Converts.StringEndian = Global.eEndian.Big; // no effect
            UInt32 result = Converts.AsciiStringToUInt32("\"ABCD\"");
            Assert.AreEqual<UInt32>(0x44434241, result);

            Converts.StringEndian = Global.eEndian.Little; // no effect
            result = Converts.AsciiStringToUInt32("\"ABCD\"");
            Assert.AreEqual<UInt32>(0x44434241, result);
        }

        [TestMethod]
        public void Test_AsciiStringToUInt32_7()
        {
            Assert.ThrowsException<ArgumentException>(() =>
                Converts.AsciiStringToUInt32("\"ABCDE\"")
                );
        }
    }

    [TestClass]
    public class Test_UInt32ToAscii
    {
        // public static string UInt32ToAscii(UInt32 value, int width)

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

        [TestMethod]
        public void Test_AsciiStringToInt16Array_1()
        {
            Converts.StringEndian = Global.eEndian.Big;
            Assert.ThrowsException<ArgumentException>(() =>
                Converts.AsciiStringToInt16Array("", 1)
                );
        }

        [TestMethod]
        public void Test_AsciiStringToInt16Array_1b()
        {
            Converts.StringEndian = Global.eEndian.Big;
            Assert.ThrowsException<ArgumentException>(() =>
                Converts.AsciiStringToInt16Array("A", 1)
                );
        }

        [TestMethod]
        public void Test_AsciiStringToInt16Array_2()
        {
            Converts.StringEndian = Global.eEndian.Big;
            Int16[] res = Converts.AsciiStringToInt16Array(@"""A""", 1);
            Assert.AreEqual<int>(1, res.Length);
            Assert.AreEqual<Int16>(0x4100, res[0]);

            Converts.StringEndian = Global.eEndian.Little;
            res = res = Converts.AsciiStringToInt16Array(@"""A""", 1);
            Assert.AreEqual<int>(1, res.Length);
            Assert.AreEqual<Int16>(0x0041, res[0]);
        }

        [TestMethod]
        public void Test_AsciiStringToInt16Array_3()
        {
            Converts.StringEndian = Global.eEndian.Big;
            Int16[] res = Converts.AsciiStringToInt16Array(@"""AB""", 1);
            Assert.AreEqual<int>(1, res.Length);
            Assert.AreEqual<Int16>(0x4142, res[0]);

            Converts.StringEndian = Global.eEndian.Little;
            res = res = Converts.AsciiStringToInt16Array(@"""AB""", 1);
            Assert.AreEqual<int>(1, res.Length);
            Assert.AreEqual<Int16>(0x4241, res[0]);
        }

        [TestMethod]
        public void Test_AsciiStringToInt16Array_4()
        {
            Converts.StringEndian = Global.eEndian.Big;
            Assert.ThrowsException<ArgumentException>(() =>
                Converts.AsciiStringToInt16Array(@"""ABC""", 1)
                );
        }

        [TestMethod]
        public void Test_AsciiStringToInt16Array_10()
        {
            Converts.StringEndian = Global.eEndian.Big;
            Int16[] res = Converts.AsciiStringToInt16Array(@"""A""", 2);
            Assert.AreEqual<int>(2, res.Length);
            Assert.AreEqual<Int16>(0x4100, res[0]);
            Assert.AreEqual<Int16>(0x0000, res[1]);

            Converts.StringEndian = Global.eEndian.Little;
            res = res = Converts.AsciiStringToInt16Array(@"""A""", 2);
            Assert.AreEqual<int>(2, res.Length);
            Assert.AreEqual<Int16>(0x0041, res[0]);
            Assert.AreEqual<Int16>(0x0000, res[1]);
        }
    }

    [TestClass]
    public class Test_Int32ToInt16Array
    {
        // public static Int16[] Int32ToInt16Array(Int32 value32, int size = 0)

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
        // public static Int16[] Float32ToInt16Array(float fValue)

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

}
