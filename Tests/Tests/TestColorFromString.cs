using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using NUnit.Framework;

using LST.Helpers;

namespace TestProject
{
    [TestFixture]
    public class TestColorFromString
    {
        [Test]
        [ExpectedException("System.NullReferenceException")]
        public void Test_ToColorNull()
        {
            Color c;

            c = ColorFromString.ToColor(null);
        }

        [Test]
        [ExpectedException("System.ArgumentException")]
        public void Test_ToColorEmpty1()
        {
            Color c;

            c = ColorFromString.ToColor("");
        }

        [Test]
        [ExpectedException("System.ArgumentException")]
        public void Test_ToColorEmpty2()
        {
            Color c;

            c = ColorFromString.ToColor(" ");
        }

        [Test]
        [ExpectedException("System.ArgumentException")]
        public void Test_ToColorEmpty3()
        {
            Color c;

            c = ColorFromString.ToColor("         ");
        }

        [Test]
        [ExpectedException("System.ArgumentException")]
        public void Test_ToColorGarbage2()
        {
            ColorFromString.ToColor("kajzhd");
        }

        [Test]
        [ExpectedException("System.ArgumentException")]
        public void Test_ToColorGarbage3()
        {
            ColorFromString.ToColor("----");
        }

        [Test]
        [ExpectedException("System.ArgumentException")]
        public void Test_ToColorGarbage4()
        {
            ColorFromString.ToColor("xxxxxxx");
        }

        [Test]
        [ExpectedException("System.ArgumentException")]
        public void Test_ToColorGarbage5()
        {
            ColorFromString.ToColor("*");
        }

        [Test]
        public void Test_ToColorHtmlValid1()
        {
            Color c = ColorFromString.ToColor("#000000");
            Assert.AreEqual(c, Color.FromArgb(0, 0, 0));
        }

        [Test]
        public void Test_ToColorHtmlValid2()
        {
            Color c = ColorFromString.ToColor("#1A4C5F");
            Assert.AreEqual(c, Color.FromArgb(26, 76, 95));
        }

        [Test]
        public void Test_ToColorHtmlValid3()
        {
            Color c = ColorFromString.ToColor("#000000");
            Assert.AreEqual(c, Color.FromArgb(0, 0, 0));
        }

        [Test]
        public void Test_ToColorHtmlValid4()
        {
            Color c = ColorFromString.ToColor("#FFFFFF");
            Assert.AreEqual(c, Color.FromArgb(255, 255, 255));
        }

        [Test]
        public void Test_ToColorHtmlValid5()
        {
            Color c = ColorFromString.ToColor("#FF0000");
            Assert.AreEqual(c, Color.FromArgb(255, 0, 0));
        }

        [Test]
        public void Test_ToColorHtmlValid6()
        {
            Color c = ColorFromString.ToColor("#00FF00");
            Assert.AreEqual(c, Color.FromArgb(0, 255, 0));
        }

        [Test]
        public void Test_ToColorHtmlValid7()
        {
            Color c = ColorFromString.ToColor("#0000FF");
            Assert.AreEqual(c, Color.FromArgb(0, 0, 255));
        }

        [Test]
        [ExpectedException("System.ArgumentException")]
        public void Test_ToColorHtmlInvalid1()
        {
            ColorFromString.ToColor("#");
        }

        [Test]
        [ExpectedException("System.ArgumentException")]
        public void Test_ToColorHtmlInvalid2()
        {
            ColorFromString.ToColor("#      ");
        }

        [Test]
        [ExpectedException("System.ArgumentException")]
        public void Test_ToColorHtmlInvalid3()
        {
            ColorFromString.ToColor("#00");
        }

        [Test]
        [ExpectedException("System.ArgumentException")]
        public void Test_ToColorHtmlInvalid4()
        {
            ColorFromString.ToColor("#00000");
        }

        [Test]
        [ExpectedException("System.ArgumentException")]
        public void Test_ToColorHtmlInvalid5()
        {
            ColorFromString.ToColor("#0000000");
        }

        [Test]
        [ExpectedException("System.ArgumentException")]
        public void Test_ToColorHtmlInvalid6()
        {
            ColorFromString.ToColor("#1A4C5");
        }

        [Test]
        [ExpectedException("System.ArgumentException")]
        public void Test_ToColorHtmlInvalid7()
        {
            ColorFromString.ToColor("#1A4C5F9");
        }

        [Test]
        [ExpectedException("System.ArgumentException")]
        public void Test_ToColorHtmlInvalid8()
        {
            ColorFromString.ToColor("#1A4C5G");
        }

        [Test]
        public void Test_ToColorRgb()
        {
            Color c;

            c = ColorFromString.ToColor("1,2,3");
            Assert.AreEqual(c, Color.FromArgb(1, 2, 3));

            c = ColorFromString.ToColor("0,0,0");
            Assert.AreEqual(c, Color.FromArgb(0, 0, 0));

            c = ColorFromString.ToColor("120,0,0");
            Assert.AreEqual(c, Color.FromArgb(120, 0, 0));

            c = ColorFromString.ToColor("0,200,0");
            Assert.AreEqual(c, Color.FromArgb(0, 200, 0));

            c = ColorFromString.ToColor("0,0,255");
            Assert.AreEqual(c, Color.FromArgb(0, 0, 255));

            c = ColorFromString.ToColor("255,255,255");
            Assert.AreEqual(c, Color.FromArgb(255, 255, 255));
        }

        [Test]
        [ExpectedException("System.ArgumentException")]
        public void Test_ToColorRgbInvalid1()
        {
            ColorFromString.ToColor("100,,");
        }

        [Test]
        [ExpectedException("System.ArgumentException")]
        public void Test_ToColorRgbInvalid2()
        {
            ColorFromString.ToColor("10,,20,30");
        }

        [Test]
        [ExpectedException("System.ArgumentException")]
        public void Test_ToColorRgbInvalid3()
        {
            ColorFromString.ToColor("10,20");
        }

        [Test]
        [ExpectedException("System.ArgumentException")]
        public void Test_ToColorRgbInvalid4()
        {
            ColorFromString.ToColor("10,,20");
        }

        [Test]
        [ExpectedException("System.ArgumentException")]
        public void Test_ToColorRgbInvalid5()
        {
            ColorFromString.ToColor("10,20,");
        }

        [Test]
        [ExpectedException("System.ArgumentException")]
        public void Test_ToColorRgbInvalid6()
        {
            ColorFromString.ToColor("0,256,0");
        }

        [Test]
        [ExpectedException("System.ArgumentException")]
        public void Test_ToColorRgbInvalid7()
        {
            ColorFromString.ToColor("10,20,30,40");
        }

        [Test]
        public void Test_ToColorFromNameValid1()
        {
            ColorFromString.ToColor("blue");

            ColorFromString.ToColor("black");

            ColorFromString.ToColor("White");

            ColorFromString.ToColor("CYAN");

            ColorFromString.ToColor("yeLLow");

            ColorFromString.ToColor("DarkGray");

            ColorFromString.ToColor("DarkGoldenrod");

            ColorFromString.ToColor("MediumSlateBlue");

            ColorFromString.ToColor("ButtonFace");
        }


        [Test]
        [ExpectedException("System.ArgumentException")]
        public void Test_ToColorFromNameInvalid1()
        {
            ColorFromString.ToColor("whi te");
        }

        [Test]
        [ExpectedException("System.ArgumentException")]
        public void Test_ToColorFromNameInvalid2()
        {
            ColorFromString.ToColor("blackblack");
        }

        [Test]
        [ExpectedException("System.ArgumentException")]
        public void Test_ToColorFromNameInvalid3()
        {
            ColorFromString.ToColor("white*");
        }

    }
}
