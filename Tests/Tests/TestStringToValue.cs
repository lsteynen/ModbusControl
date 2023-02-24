using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using LST.Helpers;

namespace Tests
{
    public class TestStringToValueClass
    {
        public void Run()
        {
            Test_Width01();
        }


        public void Test_Width01()
        {
            Int32 ResultValue;
            int Width = 1;

            //StringToValue(string str, bool IsFloat, int Width, int Decimals, out Int32 Result, out Global.eType Type)
            
            Assert.IsFalse(CValueToString.StringToValue("a", false, Width, 0, out ResultValue, Global.eType.Int));
            
            //Assert.IsFalse(StringToValueClass.Convert("-4294967295", false, Width, 0, false, out ResultValue));
            
            //Assert.IsFalse(StringToValueClass.Convert("-2147483647", false, Width, 0, false, out ResultValue));
            
            //Assert.IsFalse(StringToValueClass.Convert("-65535", false, Width, 0, false, out ResultValue));
            
            //Assert.IsFalse(StringToValueClass.Convert("-1", false, Width, 0, false, out ResultValue));
            
            //Assert.IsTrue(StringToValueClass.Convert("0", false, Width, 0, false, out ResultValue)); Assert.AreEqual(0, ResultValue);
            
            //Assert.IsTrue(StringToValueClass.Convert("1", false, Width, 0, false, out ResultValue)); Assert.AreEqual(1, ResultValue);
            
            //Assert.IsFalse(StringToValueClass.Convert("2", false, Width, 0, false, out ResultValue));
            
            //Assert.IsFalse(StringToValueClass.Convert("255", false, Width, 0, false, out ResultValue));
            
            //Assert.IsFalse(StringToValueClass.Convert("256", false, Width, 0, false, out ResultValue));
            
            //Assert.IsFalse(StringToValueClass.Convert("65535", false, Width, 0, false, out ResultValue));
            
            //Assert.IsFalse(StringToValueClass.Convert("65536", false, Width, 0, false, out ResultValue));
            
            //Assert.IsFalse(StringToValueClass.Convert("2147483647", false, Width, 0, false, out ResultValue));
            
            //Assert.IsFalse(StringToValueClass.Convert("4294967295", false, Width, 0, false, out ResultValue));
        }

        /*

        public void Test_Width02()
        {
            int Width = 2;
            Assert.IsFalse(StringToValueClass.Convert("1a", false, Width, 0, false, out ResultValue));
            Assert.IsFalse(StringToValueClass.Convert("-4294967295", false, Width, 0, false, out ResultValue));
            Assert.IsFalse(StringToValueClass.Convert("-2147483647", false, Width, 0, false, out ResultValue));
            Assert.IsFalse(StringToValueClass.Convert("-65535", false, Width, 0, false, out ResultValue));
            Assert.IsFalse(StringToValueClass.Convert("-3", false, Width, 0, false, out ResultValue));
            Assert.IsTrue(StringToValueClass.Convert("-2", false, Width, 0, false, out ResultValue)); Assert.AreEqual(-2, ResultValue);
            Assert.IsTrue(StringToValueClass.Convert("-1", false, Width, 0, false, out ResultValue)); Assert.AreEqual(-1, ResultValue);
            Assert.IsTrue(StringToValueClass.Convert("0", false, Width, 0, false, out ResultValue)); Assert.AreEqual(0, ResultValue);
            Assert.IsTrue(StringToValueClass.Convert("1", false, Width, 0, false, out ResultValue)); Assert.AreEqual(1, ResultValue);
            Assert.IsTrue(StringToValueClass.Convert("2", false, Width, 0, false, out ResultValue)); Assert.AreEqual(2, ResultValue);
            Assert.IsTrue(StringToValueClass.Convert("3", false, Width, 0, false, out ResultValue)); Assert.AreEqual(3, ResultValue);
            Assert.IsFalse(StringToValueClass.Convert("4", false, Width, 0, false, out ResultValue));
            Assert.IsFalse(StringToValueClass.Convert("255", false, Width, 0, false, out ResultValue));
            Assert.IsFalse(StringToValueClass.Convert("256", false, Width, 0, false, out ResultValue));
            Assert.IsFalse(StringToValueClass.Convert("65535", false, Width, 0, false, out ResultValue));
            Assert.IsFalse(StringToValueClass.Convert("65536", false, Width, 0, false, out ResultValue));
            Assert.IsFalse(StringToValueClass.Convert("2147483647", false, Width, 0, false, out ResultValue));
            Assert.IsFalse(StringToValueClass.Convert("4294967295", false, Width, 0, false, out ResultValue));
        }

        public void Test_Width08()
        {
            int Width = 8;
            Assert.IsFalse(StringToValueClass.Convert("123a", false, Width, 0, false, out ResultValue));
            Assert.IsFalse(StringToValueClass.Convert("-4294967295", false, Width, 0, false, out ResultValue));
            Assert.IsFalse(StringToValueClass.Convert("-2147483647", false, Width, 0, false, out ResultValue));
            Assert.IsFalse(StringToValueClass.Convert("-65535", false, Width, 0, false, out ResultValue));
            Assert.IsFalse(StringToValueClass.Convert("-129", false, Width, 0, false, out ResultValue));
            Assert.IsTrue(StringToValueClass.Convert("-128", false, Width, 0, false, out ResultValue)); Assert.AreEqual(-128, ResultValue);
            Assert.IsTrue(StringToValueClass.Convert("-2", false, Width, 0, false, out ResultValue)); Assert.AreEqual(-2, ResultValue);
            Assert.IsTrue(StringToValueClass.Convert("-1", false, Width, 0, false, out ResultValue)); Assert.AreEqual(-1, ResultValue);
            Assert.IsTrue(StringToValueClass.Convert("0", false, Width, 0, false, out ResultValue)); Assert.AreEqual(0, ResultValue);
            Assert.IsTrue(StringToValueClass.Convert("1", false, Width, 0, false, out ResultValue)); Assert.AreEqual(1, ResultValue);
            Assert.IsTrue(StringToValueClass.Convert("127", false, Width, 0, false, out ResultValue)); Assert.AreEqual(127, ResultValue);
            Assert.IsTrue(StringToValueClass.Convert("128", false, Width, 0, false, out ResultValue)); Assert.AreEqual(128, ResultValue);
            Assert.IsTrue(StringToValueClass.Convert("255", false, Width, 0, false, out ResultValue)); Assert.AreEqual(255, ResultValue);
            Assert.IsFalse(StringToValueClass.Convert("256", false, Width, 0, false, out ResultValue));
            Assert.IsFalse(StringToValueClass.Convert("65535", false, Width, 0, false, out ResultValue));
            Assert.IsFalse(StringToValueClass.Convert("65536", false, Width, 0, false, out ResultValue));
            Assert.IsFalse(StringToValueClass.Convert("2147483647", false, Width, 0, false, out ResultValue));
            Assert.IsFalse(StringToValueClass.Convert("4294967295", false, Width, 0, false, out ResultValue));
        }

        public void Test_dec()
        {
            int Width = 8;
            Assert.IsFalse(StringToValueClass.Convert("12..3", false, Width, 0, false, out ResultValue));
            Assert.IsTrue(StringToValueClass.Convert("12.3", false, Width, 1, false, out ResultValue)); Assert.AreEqual(123, ResultValue);
            Assert.IsTrue(StringToValueClass.Convert("12.345", false, Width, 1, false, out ResultValue)); Assert.AreEqual(123, ResultValue);
            Assert.IsTrue(StringToValueClass.Convert("12,345", false, Width, 1, false, out ResultValue)); Assert.AreEqual(123, ResultValue);
            Assert.IsTrue(StringToValueClass.Convert("12.567", false, Width, 1, false, out ResultValue)); Assert.AreEqual(126, ResultValue);
            Assert.IsTrue(StringToValueClass.Convert("2.55", false, Width, 2, false, out ResultValue)); Assert.AreEqual(255, ResultValue);
            Assert.IsFalse(StringToValueClass.Convert("2.56", false, Width, 2, false, out ResultValue));

            Width = 16;
            Assert.IsTrue(StringToValueClass.Convert("0.00001", false, Width, 5, false, out ResultValue)); Assert.AreEqual(1, ResultValue);
            Assert.IsTrue(StringToValueClass.Convert("0.12345678", false, Width, 5, false, out ResultValue)); Assert.AreEqual(12346, ResultValue);
            Assert.IsFalse(StringToValueClass.Convert("1.2345678", false, Width, 5, false, out ResultValue));
            Assert.IsTrue(StringToValueClass.Convert("123.45678", false, Width, 2, false, out ResultValue)); Assert.AreEqual(12346, ResultValue);
            Assert.IsTrue(StringToValueClass.Convert("0.12345678", false, Width, 3, false, out ResultValue)); Assert.AreEqual(123, ResultValue);
            Assert.IsTrue(StringToValueClass.Convert("0.12345678", false, Width, 5, false, out ResultValue)); Assert.AreEqual(12346, ResultValue);
            Assert.IsFalse(StringToValueClass.Convert("0.12345678", false, Width, 6, false, out ResultValue));
            Assert.IsTrue(StringToValueClass.Convert("65.535", false, Width, 3, false, out ResultValue)); Assert.AreEqual(65535, ResultValue);
            Assert.IsFalse(StringToValueClass.Convert("65.536", false, Width, 6, false, out ResultValue));
            Assert.IsTrue(StringToValueClass.Convert("-0.12345678", false, Width, 5, false, out ResultValue)); Assert.AreEqual(-12346, ResultValue);
            Assert.IsTrue(StringToValueClass.Convert("+123.45678", false, Width, 2, false, out ResultValue)); Assert.AreEqual(12346, ResultValue);
            Assert.IsFalse(StringToValueClass.Convert("+-123.45678", false, Width, 2, false, out ResultValue));
            Assert.IsTrue(StringToValueClass.Convert("-32.768", false, Width, 3, false, out ResultValue)); Assert.AreEqual(-32768, ResultValue);
            Assert.IsTrue(StringToValueClass.Convert("-0.00001", false, Width, 5, false, out ResultValue)); Assert.AreEqual(-1, ResultValue);

            Width = 32;
            Assert.IsTrue(StringToValueClass.Convert("0.00001", false, Width, 5, false, out ResultValue)); Assert.AreEqual(1, ResultValue);
            Assert.IsTrue(StringToValueClass.Convert("212.3456789", false, Width, 7, false, out ResultValue)); Assert.AreEqual(2123456789, ResultValue);
            Assert.IsTrue(StringToValueClass.Convert("-212.3456789", false, Width, 7, false, out ResultValue)); Assert.AreEqual(-2123456789, ResultValue);
        }

        public void Test_Width16()
        {
            int Width = 16;
            Assert.IsFalse(StringToValueClass.Convert("-2147483647", false, Width, 0, false, out ResultValue));
            Assert.IsFalse(StringToValueClass.Convert("-65536", false, Width, 0, false, out ResultValue));
            Assert.IsFalse(StringToValueClass.Convert("-65535", false, Width, 0, false, out ResultValue));
            Assert.IsFalse(StringToValueClass.Convert("-32769", false, Width, 0, false, out ResultValue));
            Assert.IsTrue(StringToValueClass.Convert("-32768", false, Width, 0, false, out ResultValue)); Assert.AreEqual(-32768, ResultValue);
            Assert.IsTrue(StringToValueClass.Convert("-32767", false, Width, 0, false, out ResultValue)); Assert.AreEqual(-32767, ResultValue);
            Assert.IsTrue(StringToValueClass.Convert("-2", false, Width, 0, false, out ResultValue)); Assert.AreEqual(-2, ResultValue);
            Assert.IsTrue(StringToValueClass.Convert("-1", false, Width, 0, false, out ResultValue)); Assert.AreEqual(-1, ResultValue);
            Assert.IsTrue(StringToValueClass.Convert("0", false, Width, 0, false, out ResultValue)); Assert.AreEqual(0, ResultValue);
            Assert.IsTrue(StringToValueClass.Convert("1", false, Width, 0, false, out ResultValue)); Assert.AreEqual(1, ResultValue);
            Assert.IsTrue(StringToValueClass.Convert("128", false, Width, 0, false, out ResultValue)); Assert.AreEqual(128, ResultValue);
            Assert.IsTrue(StringToValueClass.Convert("256", false, Width, 0, false, out ResultValue)); Assert.AreEqual(256, ResultValue);
            Assert.IsTrue(StringToValueClass.Convert("32767", false, Width, 0, false, out ResultValue)); Assert.AreEqual(32767, ResultValue);
            Assert.IsTrue(StringToValueClass.Convert("32768", false, Width, 0, false, out ResultValue)); Assert.AreEqual(32768, ResultValue);
            Assert.IsTrue(StringToValueClass.Convert("65535", false, Width, 0, false, out ResultValue)); Assert.AreEqual(65535, ResultValue);
            Assert.IsFalse(StringToValueClass.Convert("65536", false, Width, 0, false, out ResultValue));
            Assert.IsFalse(StringToValueClass.Convert("2147483647", false, Width, 0, false, out ResultValue));
            Assert.IsFalse(StringToValueClass.Convert("4294967295", false, Width, 0, false, out ResultValue));
        }

         public void Test_Width31()
        {
            int Width = 31;
            Assert.IsFalse(StringToValueClass.Convert("-2147483647", false, Width, 0, false, out ResultValue));
            Assert.IsFalse(StringToValueClass.Convert("-1073741825", false, Width, 0, false, out ResultValue));
            Assert.IsTrue(StringToValueClass.Convert("-1073741824", false, Width, 0, false, out ResultValue)); Assert.AreEqual(-1073741824, ResultValue);
            Assert.IsTrue(StringToValueClass.Convert("-32768", false, Width, 0, false, out ResultValue)); Assert.AreEqual(-32768, ResultValue);
            Assert.IsTrue(StringToValueClass.Convert("-32767", false, Width, 0, false, out ResultValue)); Assert.AreEqual(-32767, ResultValue);
            Assert.IsTrue(StringToValueClass.Convert("-2", false, Width, 0, false, out ResultValue)); Assert.AreEqual(-2, ResultValue);
            Assert.IsTrue(StringToValueClass.Convert("-1", false, Width, 0, false, out ResultValue)); Assert.AreEqual(-1, ResultValue);
            Assert.IsTrue(StringToValueClass.Convert("0", false, Width, 0, false, out ResultValue)); Assert.AreEqual(0, ResultValue);
            Assert.IsTrue(StringToValueClass.Convert("1", false, Width, 0, false, out ResultValue)); Assert.AreEqual(1, ResultValue);
            Assert.IsTrue(StringToValueClass.Convert("128", false, Width, 0, false, out ResultValue)); Assert.AreEqual(128, ResultValue);
            Assert.IsTrue(StringToValueClass.Convert("256", false, Width, 0, false, out ResultValue)); Assert.AreEqual(256, ResultValue);
            Assert.IsTrue(StringToValueClass.Convert("32768", false, Width, 0, false, out ResultValue)); Assert.AreEqual(32768, ResultValue);
            Assert.IsTrue(StringToValueClass.Convert("65536", false, Width, 0, false, out ResultValue)); Assert.AreEqual(65536, ResultValue);
            Assert.IsTrue(StringToValueClass.Convert("2147483647", false, Width, 0, false, out ResultValue)); Assert.AreEqual(2147483647, ResultValue);
            Assert.IsFalse(StringToValueClass.Convert("2147483648", false, Width, 0, false, out ResultValue));
            Assert.IsFalse(StringToValueClass.Convert("4294967295", false, Width, 0, false, out ResultValue));
        }

         public void Test_Unsigned16()
        {
            int Width = 8;
            Assert.IsTrue(StringToValueClass.Convert("0x000001", false, Width, 0, false, out ResultValue)); Assert.AreEqual(1, ResultValue);
            Assert.IsTrue(StringToValueClass.Convert("0xFF", false, Width, 0, false, out ResultValue)); Assert.AreEqual(255, ResultValue);
            Assert.IsFalse(StringToValueClass.Convert("0x100", false, Width, 0, false, out ResultValue));
            Assert.IsTrue(StringToValueClass.Convert("0b000010001100", false, Width, 0, false, out ResultValue)); Assert.AreEqual(0x8C, ResultValue);
            Assert.IsTrue(StringToValueClass.Convert("0b11111111", false, Width, 0, false, out ResultValue)); Assert.AreEqual(255, ResultValue);
            Assert.IsFalse(StringToValueClass.Convert("0b100000000", false, Width, 0, false, out ResultValue));
            Width = 16;
            Assert.IsTrue(StringToValueClass.Convert("-1", false, Width, 0, false, out ResultValue)); Assert.AreEqual(-1, ResultValue);
            Assert.IsTrue(StringToValueClass.Convert("0xFFFE", false, Width, 0, false, out ResultValue)); Assert.AreEqual(65534, ResultValue);
            Width = 32;
            Assert.IsTrue(StringToValueClass.Convert("-1", false, Width, 0, false, out ResultValue)); Assert.AreEqual(-1, ResultValue);
            Assert.IsTrue(StringToValueClass.Convert("0x7FFEFDFC", false, Width, 0, false, out ResultValue)); Assert.AreEqual(2147417596, ResultValue);
            Assert.IsTrue(StringToValueClass.Convert("0xFEFDFCFB", false, Width, 0, false, out ResultValue)); Assert.AreEqual(-16909061, ResultValue);
            Assert.IsTrue(StringToValueClass.Convert("0xFFFFFCFB", false, Width, 0, false, out ResultValue)); Assert.AreEqual(-773, ResultValue);
        }

        public void Test_ASCII()
        {
            int Width = 8;
            bool AsciiBigendian = false;
            Assert.IsFalse(StringToValueClass.Convert("\"", false, Width, 0, AsciiBigendian, out ResultValue));
            Assert.IsFalse(StringToValueClass.Convert("\"A", false, Width, 0, AsciiBigendian, out ResultValue));
            Assert.IsFalse(StringToValueClass.Convert("\"AB", false, Width, 0, AsciiBigendian, out ResultValue));
            Assert.IsFalse(StringToValueClass.Convert("\"ABC", false, Width, 0, AsciiBigendian, out ResultValue));
            Assert.IsFalse(StringToValueClass.Convert("\"ABCD", false, Width, 0, AsciiBigendian, out ResultValue));
            Assert.IsFalse(StringToValueClass.Convert("\"ABCDE", false, Width, 0, AsciiBigendian, out ResultValue));
            Assert.IsFalse(StringToValueClass.Convert("\"AB\"CD", false, Width, 0, AsciiBigendian, out ResultValue));
            Assert.IsTrue(StringToValueClass.Convert("\"\"", false, Width, 0, AsciiBigendian, out ResultValue)); Assert.AreEqual(0, ResultValue);
            Assert.IsTrue(StringToValueClass.Convert("\"A\"", false, Width, 0, AsciiBigendian, out ResultValue)); Assert.AreEqual(65, ResultValue);
            Assert.IsFalse(StringToValueClass.Convert("\"Ab\"", false, Width, 0, AsciiBigendian, out ResultValue));
            Width = 15;
            Assert.IsFalse(StringToValueClass.Convert("\"Ab\"", false, Width, 0, AsciiBigendian, out ResultValue));
            Width = 16;
            Assert.IsTrue(StringToValueClass.Convert("\"Ab\"", false, Width, 0, AsciiBigendian, out ResultValue)); Assert.AreEqual(0x6241, ResultValue);
            Assert.IsFalse(StringToValueClass.Convert("\"ABc\"", false, Width, 0, AsciiBigendian, out ResultValue));
            Width = 32;
            Assert.IsTrue(StringToValueClass.Convert("\"ABcd\"", false, Width, 0, AsciiBigendian, out ResultValue)); Assert.AreEqual(0x64634241, ResultValue);
            Assert.IsFalse(StringToValueClass.Convert("\"ABcdE\"", false, Width, 0, AsciiBigendian, out ResultValue));

            Width = 8;
            AsciiBigendian = true;
            Assert.IsTrue(StringToValueClass.Convert("\"\"", false, Width, 0, AsciiBigendian, out ResultValue)); Assert.AreEqual(0, ResultValue);
            Assert.IsTrue(StringToValueClass.Convert("\"A\"", false, Width, 0, AsciiBigendian, out ResultValue)); Assert.AreEqual(65, ResultValue);
            Assert.IsFalse(StringToValueClass.Convert("\"Ab\"", false, Width, 0, AsciiBigendian, out ResultValue));
            Width = 15;
            Assert.IsFalse(StringToValueClass.Convert("\"Ab\"", false, Width, 0, AsciiBigendian, out ResultValue));
            Width = 16;
            Assert.IsTrue(StringToValueClass.Convert("\"Ab\"", false, Width, 0, AsciiBigendian, out ResultValue)); Assert.AreEqual(0x4162, ResultValue);
            Assert.IsFalse(StringToValueClass.Convert("\"ABc\"", false, Width, 0, AsciiBigendian, out ResultValue));
            Width = 32;
            Assert.IsTrue(StringToValueClass.Convert("\"ABcd\"", false, Width, 0, AsciiBigendian, out ResultValue)); Assert.AreEqual(0x63644142, ResultValue);
            Assert.IsFalse(StringToValueClass.Convert("\"ABcdE\"", false, Width, 0, AsciiBigendian, out ResultValue));
        }
        */
    }
}
