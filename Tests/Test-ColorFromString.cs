using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using LST.Helpers;
using ModbusControl;
using System.Drawing;

namespace UnitTests
{
    [TestClass]
    public class Test_ColorFromString
    {
        // public static Color ToColor( string value)

        [TestMethod]
        public void Test_null()
        {
            Color c;
            Assert.ThrowsException<NullReferenceException>(() =>
                c = ColorFromString.ToColor(null)
                );
        }

        [TestMethod]
        public void Test_empty()
        {
            Color c;
            Assert.ThrowsException<ArgumentException>(() =>
                c = ColorFromString.ToColor("")
                );
        }

        [TestMethod]
        public void Test_empty2()
        {
            Color c;
            Assert.ThrowsException<ArgumentException>(() =>
                c = ColorFromString.ToColor("  ")
                );
        }

        [TestMethod]
        public void Test_garb()
        {
            Color c;
            Assert.ThrowsException<ArgumentException>(() =>
                c = ColorFromString.ToColor("kajzhd")
                );
        }

        [TestMethod]
        public void Test_garb2()
        {
            Color c;
            Assert.ThrowsException<ArgumentException>(() =>
                c = ColorFromString.ToColor("----")
                );
        }

        [TestMethod]
        public void Test_html_1()
        {
            Color c;
            c = ColorFromString.ToColor("#000000");
            Assert.AreEqual<Color>(Color.FromArgb(0, 0, 0), c);
        }

        [TestMethod]
        public void Test_html_2()
        {
            Color c;
            c = ColorFromString.ToColor("#1A4C5F");
            Assert.AreEqual<Color>(Color.FromArgb(26, 76, 95), c);
        }

        [TestMethod]
        public void Test_html_3()
        {
            Color c;
            c = ColorFromString.ToColor("#FFFFFF");
            Assert.AreEqual<Color>(Color.FromArgb(255, 255, 255), c);
        }

        [TestMethod]
        public void Test_html_4()
        {
            Color c;
            c = ColorFromString.ToColor("#FF0000");
            Assert.AreEqual<Color>(Color.FromArgb(255, 0, 0), c);
        }

        [TestMethod]
        public void Test_html_5()
        {
            Color c;
            c = ColorFromString.ToColor("#00FF00");
            Assert.AreEqual<Color>(Color.FromArgb(0, 255, 0), c);
        }

        [TestMethod]
        public void Test_html_6()
        {
            Color c;
            c = ColorFromString.ToColor("#0000FF");
            Assert.AreEqual<Color>(Color.FromArgb(0, 0, 255), c);
        }

        [TestMethod]
        public void Test_html_e1()
        {
            Color c;
            Assert.ThrowsException<ArgumentException>(() =>
                c = ColorFromString.ToColor("#")
                );
        }

        [TestMethod]
        public void Test_html_e2()
        {
            Color c;
            Assert.ThrowsException<ArgumentException>(() =>
                c = ColorFromString.ToColor("#     ")
                );
        }

        [TestMethod]
        public void Test_html_e3()
        {
            Color c;
            Assert.ThrowsException<ArgumentException>(() =>
                c = ColorFromString.ToColor("#00")
                );
        }

        [TestMethod]
        public void Test_html_e4()
        {
            Color c;
            Assert.ThrowsException<ArgumentException>(() =>
                c = ColorFromString.ToColor("#1A4C5")
                );
        }

        [TestMethod]
        public void Test_html_e5()
        {
            Color c;
            Assert.ThrowsException<ArgumentException>(() =>
                c = ColorFromString.ToColor("#1A4C5F9")
                );
        }

        [TestMethod]
        public void Test_rgb_1()
        {
            Color c;
            c = ColorFromString.ToColor("1,2,3");
            Assert.AreEqual<Color>(Color.FromArgb(1, 2, 3), c);
        }

        [TestMethod]
        public void Test_rgb_2()
        {
            Color c;
            c = ColorFromString.ToColor("0,0,0");
            Assert.AreEqual<Color>(Color.FromArgb(0, 0, 0), c);
        }

        [TestMethod]
        public void Test_rgb_3()
        {
            Color c;
            c = ColorFromString.ToColor("120,0,0");
            Assert.AreEqual<Color>(Color.FromArgb(120, 0, 0), c);
        }

        [TestMethod]
        public void Test_rgb_4()
        {
            Color c;
            c = ColorFromString.ToColor("0,200,0");
            Assert.AreEqual<Color>(Color.FromArgb(0, 200, 0), c);
        }

        [TestMethod]
        public void Test_rgb_5()
        {
            Color c;
            c = ColorFromString.ToColor("0, 0, 255");
            Assert.AreEqual<Color>(Color.FromArgb(0, 0, 255), c);
        }

        [TestMethod]
        public void Test_rgb_6()
        {
            Color c;
            c = ColorFromString.ToColor("255,255,255");
            Assert.AreEqual<Color>(Color.FromArgb(255, 255, 255), c);
        }

        [TestMethod]
        public void Test_rgb_e1()
        {
            Color c;
            Assert.ThrowsException<ArgumentException>(() =>
                    c = ColorFromString.ToColor("100,,")
                    );
        }

        [TestMethod]
        public void Test_rgb_e2()
        {
            Color c;
            Assert.ThrowsException<ArgumentException>(() =>
                    c = ColorFromString.ToColor("10,,20,30")
                    );
        }

        [TestMethod]
        public void Test_rgb_e3()
        {
            Color c;
            Assert.ThrowsException<ArgumentException>(() =>
                    c = ColorFromString.ToColor("10, 20")
                    );
        }

        [TestMethod]
        public void Test_rgb_e4()
        {
            Color c;
            Assert.ThrowsException<ArgumentException>(() =>
                    c = ColorFromString.ToColor("100, 256, 200")
                    );
        }

        [TestMethod]
        public void Test_rgb_e5()
        {
            Color c;
            Assert.ThrowsException<ArgumentException>(() =>
                    c = ColorFromString.ToColor("10, 20, 30, 40")
                    );
        }

        [TestMethod]
        public void Test_name_1()
        {
            Color c;
            c = ColorFromString.ToColor("blue");
            Assert.AreEqual<int>(0, c.R);
            Assert.AreEqual<int>(0, c.G);
            Assert.AreEqual<int>(255, c.B);
        }

        [TestMethod]
        public void Test_name_2()
        {
            Color c;
            c = ColorFromString.ToColor("black");
            Assert.AreEqual<int>(0, c.R);
            Assert.AreEqual<int>(0, c.G);
            Assert.AreEqual<int>(0, c.B);
        }

        [TestMethod]
        public void Test_name_3()
        {
            Color c;
            c = ColorFromString.ToColor("CYAN");
            Assert.AreEqual<int>(0, c.R);
            Assert.AreEqual<int>(255, c.G);
            Assert.AreEqual<int>(255, c.B);
        }

        [TestMethod]
        public void Test_name_4()
        {
            Color c;
            c = ColorFromString.ToColor("DarkGray");
            Assert.AreEqual<int>(169, c.R);
            Assert.AreEqual<int>(169, c.G);
            Assert.AreEqual<int>(169, c.B);
        }

        [TestMethod]
        public void Test_name_e1()
        {
            Color c;
            Assert.ThrowsException<ArgumentException>(() =>
                    c = ColorFromString.ToColor("whi te")
                    );
        }
      
    }
}
