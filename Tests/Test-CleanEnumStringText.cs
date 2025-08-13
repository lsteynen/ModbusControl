using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using LST.Helpers;
using ModbusControlSpace;

namespace UnitTests
{
    [TestClass]
    public class Test_CleanValuestrEnumText
    {
        // public static string CleanValuestrEnumText(string str)

        [TestMethod]
        public void Test_1()
        {
            string res = CValueToString.CleanValuestrEnumText("");
            Assert.AreEqual<string>("", res);
        }

        [TestMethod]
        public void Test_2()
        {
            string res = CValueToString.CleanValuestrEnumText("1234");
            Assert.AreEqual<string>("1234", res);
        }

        [TestMethod]
        public void Test_3()
        {
            string res = CValueToString.CleanValuestrEnumText("-15158 ");
            Assert.AreEqual<string>("-15158", res);
        }

        [TestMethod]
        public void Test_10()
        {
            string res = CValueToString.CleanValuestrEnumText("12 (meaning)");
            Assert.AreEqual<string>("12", res);
        }

        [TestMethod]
        public void Test_11()
        {
            string res = CValueToString.CleanValuestrEnumText(" 1234.478 (meaning");
            Assert.AreEqual<string>(" 1234.478", res);
        }

        [TestMethod]
        public void Test_12()
        {
            string res = CValueToString.CleanValuestrEnumText("-15 (ENUM)");
            Assert.AreEqual<string>("-15", res);
        }

        [TestMethod]
        public void Test_13()
        {
            string res = CValueToString.CleanValuestrEnumText("0 ()");
            Assert.AreEqual<string>("0", res);
        }

        [TestMethod]
        public void Test_14()
        {
            string res = CValueToString.CleanValuestrEnumText("  61 (text with multiple words)"   );
            Assert.AreEqual<string>("  61", res);
        }

        [TestMethod]
        public void Test_20()
        {
            string res = CValueToString.CleanValuestrEnumText("(");
            Assert.AreEqual<string>("", res);
        }

        [TestMethod]
        public void Test_21()
        {
            string res = CValueToString.CleanValuestrEnumText("  (  ");
            Assert.AreEqual<string>("", res);
        }

        [TestMethod]
        public void Test_22()
        {
            string res = CValueToString.CleanValuestrEnumText("(  ");
            Assert.AreEqual<string>("", res);
        }

        [TestMethod]
        public void Test_23()
        {
            string res = CValueToString.CleanValuestrEnumText("(  ");
            Assert.AreEqual<string>("", res);
        }
      
    }
}
