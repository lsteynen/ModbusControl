using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using LST.Helpers;
using ModbusControlSpace;

namespace UnitTests
{
	[TestClass]
    public class Test_SplitCommandlineString
    {
		// public static bool SplitCommandlineString(string input, out string command, out string arguments)
        
		
		[TestMethod]
        public void Test_1()
        {
			string testcommand;
			string testargs;
			
            bool ret = SplitCommandlineString("", out testcommand, out testargs);
            Assert.AreEqual(ret, false);
        }
		
		[TestMethod]
        public void Test_2()
        {
			string testcommand;
			string testargs;
			
            bool ret = SplitCommandlineString(" ", out testcommand, out testargs);
            Assert.AreEqual(ret, false);
        }
		
		[TestMethod]
        public void Test_3()
        {
			string testcommand;
			string testargs;
			
            bool ret = SplitCommandlineString("    ", out testcommand, out testargs);
            Assert.AreEqual(ret, false);
        }
		
		[TestMethod]
        public void Test_4()
        {
			string testcommand;
			string testargs;
			
            bool SplitCommandlineString("a", out testcommand, out testargs);
            Assert.AreEqual(ret, true);
            Assert.AreEqual(testcommand, "a");
            Assert.AreEqual(testargs, "");
        }
		
		[TestMethod]
        public void Test_5()
        {
			string testcommand;
			string testargs;
			
            bool ret = SplitCommandlineString("\"", out testcommand, out testargs);
            Assert.AreEqual(ret, true);
            Assert.AreEqual(testcommand, "\"");
            Assert.AreEqual(testargs, "");
        }
		
		[TestMethod]
        public void Test_10()
        {
            string testcommand;
			string testargs;
			
            bool ret = SplitCommandlineString("notepad.exe", out testcommand, out testargs);
            Assert.AreEqual(ret, true);
            Assert.AreEqual(testcommand, "notepad.exe");
            Assert.AreEqual(testargs, "");
        }

		[TestMethod]
        public void Test_11()
        {
            string testcommand;
			string testargs;
			
            bool ret = SplitCommandlineString("   notepad.exe   ", out testcommand, out testargs);
            Assert.AreEqual(ret, true);
            Assert.AreEqual(testcommand, "notepad.exe");
            Assert.AreEqual(testargs, "");
        }
		
		[TestMethod]
        public void Test_20()
        {
            string testcommand;
			string testargs;
			
            bool ret = SplitCommandlineString(@"c:\program files\notepad.exe  ", out testcommand, out testargs);
            Assert.AreEqual(ret, true);
            Assert.AreEqual(testcommand, @"c:\program");
            Assert.AreEqual(testargs, @"files\notepad.exe");
        }

		[TestMethod]
        public void Test_21()
        {
            string testcommand;
			string testargs;
			
            bool ret = SplitCommandlineString("\"c:\\program files\\notepad.exe  ", out testcommand, out testargs);
            Assert.AreEqual(ret, true);
            Assert.AreEqual(testcommand, "\"c:\\program files\\notepad.exe");
            Assert.AreEqual(testargs, "");
        }

		[TestMethod]
        public void Test_22()
        {
            string testcommand;
			string testargs;
			
            bool ret = SplitCommandlineString("\"c:\\program files\\notepad.exe  \"", out testcommand, out testargs);
            Assert.AreEqual(ret, true);
            Assert.AreEqual(testcommand, "\"c:\\program files\\notepad.exe  \"");
            Assert.AreEqual(testargs, "");
        }

        [TestMethod]
        public void Test_30()
        {
            string testcommand;
			string testargs;
			
            bool ret = SplitCommandlineString(@"c:\program files\notepad.exe file ", out testcommand, out testargs);
            Assert.AreEqual(ret, true);
            Assert.AreEqual(testcommand, @"c:\program");
            Assert.AreEqual(testargs, @"files\notepad.exe file");
        }
		
		[TestMethod]
        public void Test_31()
        {
            string testcommand;
			string testargs;
			
            bool ret = SplitCommandlineString("\"c:\\program files\\notepad.exe    file", out testcommand, out testargs);
            Assert.AreEqual(ret, true);
            Assert.AreEqual(testcommand, "\"c:\\program files\\notepad.exe    file");
            Assert.AreEqual(testargs, @"");
        }
		
		[TestMethod]
        public void Test_32()
        {
            string testcommand;
			string testargs;
			
            bool ret = SplitCommandlineString("\"c:\\program files\\notepad.exe\"    file", out testcommand, out testargs);
            Assert.AreEqual(ret, true);
            Assert.AreEqual(testcommand, "\"c:\\program files\\notepad.exe\"");
            Assert.AreEqual(testargs, @"file");
        }

        [TestMethod]
        public void Test_40()
        {
            string testcommand;
			string testargs;
			
            bool ret = SplitCommandlineString("\"c:\\program files\\notepad.exe\"    \\\\root\\dir\\file", out testcommand, out testargs);
            Assert.AreEqual(ret, true);
            Assert.AreEqual(testcommand, "\"c:\\program files\\notepad.exe\"");
            Assert.AreEqual(testargs, @"\\root\dir\file");
        }
		
		[TestMethod]
        public void Test_41()
        {
            string testcommand;
			string testargs;
			
            bool ret = SplitCommandlineString("\"c:\\program files\\notepad.exe\"    \"\\\\root\\dir\\file  ", out testcommand, out testargs);
            Assert.AreEqual(ret, true);
            Assert.AreEqual(testcommand, "\"c:\\program files\\notepad.exe\"");
            Assert.AreEqual(testargs, "\"\\\\root\\dir\\file");
        }
		
		[TestMethod]
        public void Test_42()
        {
            string testcommand;
			string testargs;
			
            bool ret = SplitCommandlineString("\"c:\\program files\\notepad.exe\"    \"\\\\root\\dir\\file.exte\"", out testcommand, out testargs);
            Assert.AreEqual(ret, true);
            Assert.AreEqual(testcommand, "\"c:\\program files\\notepad.exe\"");
            Assert.AreEqual(testargs, "\"\\\\root\\dir\\file.exte\"");
        }


    }
}
