using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using LST.Helpers;
using ModbusControl;

namespace UnitTests
{
	[TestClass]
    public class Test_SplitCommandlineString
    {
		// public static bool AppHelpers.SplitCommandlineString(string input, out string command, out string arguments)
        
		
		[TestMethod]
        public void Test_1()
        {
			string testcommand;
			string testargs;
			
            bool ret = AppHelpers.SplitCommandlineString("", out testcommand, out testargs);
            Assert.IsFalse(ret);
        }   
		
		[TestMethod]
        public void Test_2()
        {
			string testcommand;
			string testargs;
			
            bool ret = AppHelpers.SplitCommandlineString(" ", out testcommand, out testargs);
            Assert.IsFalse(ret);
        }
		
		[TestMethod]
        public void Test_3()
        {
			string testcommand;
			string testargs;
			
            bool ret = AppHelpers.SplitCommandlineString("    ", out testcommand, out testargs);
            Assert.IsFalse(ret);
        }
		
		[TestMethod]
        public void Test_4()
        {
			string testcommand;
			string testargs;
			
            bool ret = AppHelpers.SplitCommandlineString("a", out testcommand, out testargs);
            Assert.IsTrue(ret);
            Assert.AreEqual("a", testcommand);
            Assert.AreEqual("", testargs);
        }
		
		[TestMethod]
        public void Test_5()
        {
			string testcommand;
			string testargs;
			
            bool ret = AppHelpers.SplitCommandlineString("\"", out testcommand, out testargs);
            Assert.IsTrue(ret);
            Assert.AreEqual("\"", testcommand);
            Assert.AreEqual("", testargs);
        }
		
		[TestMethod]
        public void Test_10()
        {
            string testcommand;
			string testargs;
			
            bool ret = AppHelpers.SplitCommandlineString("notepad.exe", out testcommand, out testargs);
            Assert.IsTrue(ret);
            Assert.AreEqual("notepad.exe", testcommand);
            Assert.AreEqual("", testargs);
        }

		[TestMethod]
        public void Test_11()
        {
            string testcommand;
			string testargs;
			
            bool ret = AppHelpers.SplitCommandlineString("   notepad.exe   ", out testcommand, out testargs);
            Assert.IsTrue(ret);
            Assert.AreEqual("notepad.exe", testcommand);
            Assert.AreEqual("", testargs);
        }
		
		[TestMethod]
        public void Test_20()
        {
            string testcommand;
			string testargs;
			
            bool ret = AppHelpers.SplitCommandlineString(@"c:\program files\notepad.exe  ", out testcommand, out testargs);
            Assert.IsTrue(ret);
            Assert.AreEqual(@"c:\program", testcommand);
            Assert.AreEqual(@"files\notepad.exe", testargs);
        }

		[TestMethod]
        public void Test_21()
        {
            string testcommand;
			string testargs;
			
            bool ret = AppHelpers.SplitCommandlineString("\"c:\\program files\\notepad.exe  ", out testcommand, out testargs);
            Assert.IsTrue(ret);
            Assert.AreEqual("\"c:\\program files\\notepad.exe", testcommand);
            Assert.AreEqual("", testargs);
        }

		[TestMethod]
        public void Test_22()
        {
            string testcommand;
			string testargs;
			
            bool ret = AppHelpers.SplitCommandlineString("\"c:\\program files\\notepad.exe  \"", out testcommand, out testargs);
            Assert.IsTrue(ret);
            Assert.AreEqual("\"c:\\program files\\notepad.exe  \"", testcommand);
            Assert.AreEqual("", testargs);
        }

        [TestMethod]
        public void Test_30()
        {
            string testcommand;
			string testargs;
			
            bool ret = AppHelpers.SplitCommandlineString(@"c:\program files\notepad.exe file ", out testcommand, out testargs);
            Assert.IsTrue(ret);
            Assert.AreEqual(@"c:\program", testcommand);
            Assert.AreEqual(@"files\notepad.exe file", testargs);
        }
		
		[TestMethod]
        public void Test_31()
        {
            string testcommand;
			string testargs;
			
            bool ret = AppHelpers.SplitCommandlineString("\"c:\\program files\\notepad.exe    file", out testcommand, out testargs);
            Assert.IsTrue(ret);
            Assert.AreEqual("\"c:\\program files\\notepad.exe    file", testcommand);
            Assert.AreEqual(@"", testargs);
        }
		
		[TestMethod]
        public void Test_32()
        {
            string testcommand;
			string testargs;
			
            bool ret = AppHelpers.SplitCommandlineString("\"c:\\program files\\notepad.exe\"    file", out testcommand, out testargs);
            Assert.IsTrue(ret);
            Assert.AreEqual("\"c:\\program files\\notepad.exe\"", testcommand);
            Assert.AreEqual(@"file", testargs);
        }

        [TestMethod]
        public void Test_40()
        {
            string testcommand;
			string testargs;
			
            bool ret = AppHelpers.SplitCommandlineString("\"c:\\program files\\notepad.exe\"    \\\\root\\dir\\file", out testcommand, out testargs);
            Assert.IsTrue(ret);
            Assert.AreEqual("\"c:\\program files\\notepad.exe\"", testcommand);
            Assert.AreEqual(@"\\root\dir\file", testargs);
        }
		
		[TestMethod]
        public void Test_41()
        {
            string testcommand;
			string testargs;
			
            bool ret = AppHelpers.SplitCommandlineString("\"c:\\program files\\notepad.exe\"    \"\\\\root\\dir\\file  ", out testcommand, out testargs);
            Assert.IsTrue(ret);
            Assert.AreEqual("\"c:\\program files\\notepad.exe\"", testcommand);
            Assert.AreEqual("\"\\\\root\\dir\\file", testargs);
        }
		
		[TestMethod]
        public void Test_42()
        {
            string testcommand;
			string testargs;
			
            bool ret = AppHelpers.SplitCommandlineString("\"c:\\program files\\notepad.exe\"    \"\\\\root\\dir\\file.exte\"", out testcommand, out testargs);
            Assert.IsTrue(ret);
            Assert.AreEqual("\"c:\\program files\\notepad.exe\"", testcommand);
            Assert.AreEqual("\"\\\\root\\dir\\file.exte\"", testargs);
        }


    }
}
