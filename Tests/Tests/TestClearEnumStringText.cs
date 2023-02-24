using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    public class TestClearEnumStringTextClass
    {
        public void Run()
        {
            Test1();
            
        }

        private void Test1()
        {
            string r;   // result

            r = ClearEnumStringText("12 (meaning)");
            Assert.AreEqual(r, "12 ");

            r = ClearEnumStringText("1234.478 (meaning");
            Assert.AreEqual(r, "1234.478 ");

            r = ClearEnumStringText("-15 (ENUM)");
            Assert.AreEqual(r, "-15 ");

            r = ClearEnumStringText("0 ()");
            Assert.AreEqual(r, "0 ");

            r = ClearEnumStringText("");
            Assert.AreEqual(r, "");

            r = ClearEnumStringText("(");
            Assert.AreEqual(r, "");

            r = ClearEnumStringText("(  ");
            Assert.AreEqual(r, "");

            r = ClearEnumStringText("  (  ");
            Assert.AreEqual(r, "  ");

            r = ClearEnumStringText("  (");
            Assert.AreEqual(r, "  ");
        }

        private string ClearEnumStringText(string str)
        {
            // expected formats: "123", " 123 (text)", " 123 (...", ...

            int i = str.IndexOf('(');
            if (i >= 0)
            {
                str = str.Substring(0, i);
            }

            return str;
        }
    }
}
