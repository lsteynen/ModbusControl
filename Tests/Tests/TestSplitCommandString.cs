using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LST.Helpers;

namespace Tests
{
    public class TestMaster
    {
        static void Main(string[] args)
        {

        }


        bool ret;
        string testcommand;
        string testargs;

        [Test]
        public void Test01()
        {
            ret = SplitCommandString("", out testcommand, out testargs);
            Assert.AreEqual(ret, false);

            ret = SplitCommandString(" ", out testcommand, out testargs);
            Assert.AreEqual(ret, false);

            ret = SplitCommandString("    ", out testcommand, out testargs);
            Assert.AreEqual(ret, false);

            ret = SplitCommandString("a", out testcommand, out testargs);
            Assert.AreEqual(ret, true);
            Assert.AreEqual(testcommand, "a");
            Assert.AreEqual(testargs, "");

            ret = SplitCommandString("\"", out testcommand, out testargs);
            Assert.AreEqual(ret, true);
            Assert.AreEqual(testcommand, "\"");
            Assert.AreEqual(testargs, "");
        }

        [Test]
        public void Test02()
        {
            ret = SplitCommandString("notepad.exe", out testcommand, out testargs);
            Assert.AreEqual(ret, true);
            Assert.AreEqual(testcommand, "notepad.exe");
            Assert.AreEqual(testargs, "");

            ret = SplitCommandString("   notepad.exe   ", out testcommand, out testargs);
            Assert.AreEqual(ret, true);
            Assert.AreEqual(testcommand, "notepad.exe");
            Assert.AreEqual(testargs, "");
        }

        [Test]
        public void Test03()
        {
            ret = SplitCommandString(@"c:\program files\notepad.exe  ", out testcommand, out testargs);
            Assert.AreEqual(ret, true);
            Assert.AreEqual(testcommand, @"c:\program");
            Assert.AreEqual(testargs, @"files\notepad.exe");

            ret = SplitCommandString("\"c:\\program files\\notepad.exe  ", out testcommand, out testargs);
            Assert.AreEqual(ret, true);
            Assert.AreEqual(testcommand, "\"c:\\program files\\notepad.exe");
            Assert.AreEqual(testargs, "");

            ret = SplitCommandString("\"c:\\program files\\notepad.exe  \"", out testcommand, out testargs);
            Assert.AreEqual(ret, true);
            Assert.AreEqual(testcommand, "\"c:\\program files\\notepad.exe  \"");
            Assert.AreEqual(testargs, "");
        }

        [Test]
        public void Test04()
        {
            ret = SplitCommandString(@"c:\program files\notepad.exe file ", out testcommand, out testargs);
            Assert.AreEqual(ret, true);
            Assert.AreEqual(testcommand, @"c:\program");
            Assert.AreEqual(testargs, @"files\notepad.exe file");

            ret = SplitCommandString("\"c:\\program files\\notepad.exe    file", out testcommand, out testargs);
            Assert.AreEqual(ret, true);
            Assert.AreEqual(testcommand, "\"c:\\program files\\notepad.exe    file");
            Assert.AreEqual(testargs, @"");

            ret = SplitCommandString("\"c:\\program files\\notepad.exe\"    file", out testcommand, out testargs);
            Assert.AreEqual(ret, true);
            Assert.AreEqual(testcommand, "\"c:\\program files\\notepad.exe\"");
            Assert.AreEqual(testargs, @"file");
        }

        [Test]
        public void Test05()
        {
            ret = SplitCommandString("\"c:\\program files\\notepad.exe\"    \\\\root\\dir\\file", out testcommand, out testargs);
            Assert.AreEqual(ret, true);
            Assert.AreEqual(testcommand, "\"c:\\program files\\notepad.exe\"");
            Assert.AreEqual(testargs, @"\\root\dir\file");

            ret = SplitCommandString("\"c:\\program files\\notepad.exe\"    \"\\\\root\\dir\\file  ", out testcommand, out testargs);
            Assert.AreEqual(ret, true);
            Assert.AreEqual(testcommand, "\"c:\\program files\\notepad.exe\"");
            Assert.AreEqual(testargs, "\"\\\\root\\dir\\file");

            ret = SplitCommandString("\"c:\\program files\\notepad.exe\"    \"\\\\root\\dir\\file.exte\"", out testcommand, out testargs);
            Assert.AreEqual(ret, true);
            Assert.AreEqual(testcommand, "\"c:\\program files\\notepad.exe\"");
            Assert.AreEqual(testargs, "\"\\\\root\\dir\\file.exte\"");
        }

        /* Tests for SplitProcessString
        SplitCommandString("  \"c:\\program files\\notepad.exe  \"  file", out testcommand, out testargs);
        SplitCommandString("  \"c:\\program files\\notepad.exe\"  c:\\program files\\file.mbc", out testcommand, out testargs);
        SplitCommandString("  \"c:\\program files\\notepad.exe\"  \"c:\\program files\\file.mbc\"  %aaa %bb   ", out testcommand, out testargs);
        */

        public static bool SplitCommandString(string input, out string command, out string arguments)
        {
            command = null;
            arguments = null;

            input = input.Trim();

            if (input.Length == 0)
            {
                return false;
            }

            // quoted?
            int i = 1;
            if ((input.Length >= 1) && (input[0] == '"'))
            {
                // find end of quoted command
                while ((i < input.Length) && (input[i] != '"'))
                {
                    ++i;
                }
                // include end quote
                if (i < input.Length) ++i;
            }
            else
            {
                // find space at end of command
                while ((i < input.Length) && (input[i] != ' '))
                {
                    ++i;
                }
            }

            command = input.Substring(0, i);
            arguments = input.Substring(i).Trim();

            return true;
        }

    }
}
