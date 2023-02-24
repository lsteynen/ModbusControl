using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using LST.Helpers;

namespace Tests
{
    public class TestColorFromStringClass
    {
        public void Run()
        {
            TestToColor1();
            TestToColor2();
            TestToColor3();
        }

        private void TestToColor1()
        {
            Color c;

            // test null

            try
            {
                c = ColorFromString.ToColor(null);
            }
            catch (NullReferenceException)
            {
                // okay
            }

            // test empty

            try
            {
                c = ColorFromString.ToColor("");
            }
            catch (ArgumentException)
            {
                // okay
            }

            // test space

            try
            {
                c = ColorFromString.ToColor(" ");
            }
            catch (ArgumentException)
            {
                // okay
            }

            // test spaces

            try
            {
                c = ColorFromString.ToColor("         ");
            }
            catch (ArgumentException)
            {
                // okay
            }

            // test garbage

            try
            {
                c = ColorFromString.ToColor("kajzhd");
            }
            catch (ArgumentException)
            {
                // okay
            }

            // test garbage

            try
            {
                c = ColorFromString.ToColor("----");
            }
            catch (ArgumentException)
            {
                // okay
            }

            // test garbage

            try
            {
                c = ColorFromString.ToColor("xxxxxxx");
            }
            catch (ArgumentException)
            {
                // okay
            }

            // test garbage

            try
            {
                c = ColorFromString.ToColor("*");
            }
            catch (ArgumentException)
            {
                // okay
            }

            // test html colors

            c = ColorFromString.ToColor("#000000");
            Assert.AreEqual(c, Color.FromArgb(0, 0, 0));

            c = c = ColorFromString.ToColor("#1A4C5F");
            Assert.AreEqual(c, Color.FromArgb(26, 76, 95));

            c = ColorFromString.ToColor("#FFFFFF");
            Assert.AreEqual(c, Color.FromArgb(255, 255, 255));

            c = ColorFromString.ToColor("#FF0000");
            Assert.AreEqual(c, Color.FromArgb(255, 0, 0));

            c = ColorFromString.ToColor("#00FF00");
            Assert.AreEqual(c, Color.FromArgb(0, 255, 0));

            c = ColorFromString.ToColor("#0000FF");
            Assert.AreEqual(c, Color.FromArgb(0, 0, 255));

            // test garbage

            try
            {
                c = ColorFromString.ToColor("#");
            }
            catch (ArgumentException)
            {
                // okay
            }

            // test garbage

            try
            {
                c = ColorFromString.ToColor("#      ");
            }
            catch (ArgumentException)
            {
                // okay
            }

            // test garbage

            try
            {
                c = ColorFromString.ToColor("#00");
            }
            catch (ArgumentException)
            {
                // okay
            }

            // test garbage

            try
            {
                c = ColorFromString.ToColor("#00000");
            }
            catch (ArgumentException)
            {
                // okay
            }

            // test garbage

            try
            {
                c = ColorFromString.ToColor("#0000000");
            }
            catch (ArgumentException)
            {
                // okay
            }

            // test garbage

            try
            {
                c = ColorFromString.ToColor("#1A4C5");
            }
            catch (ArgumentException)
            {
                // okay
            }

            try
            {
                c = ColorFromString.ToColor("#1A4C5F9");
            }
            catch (ArgumentException)
            {
                // okay
            }

            try
            {
                c = ColorFromString.ToColor("#1A4C5G");
            }
            catch (ArgumentException)
            {
                // okay
            }
        }

        private void TestToColor2()
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

            try
            {
                c = ColorFromString.ToColor("100,,");
            }
            catch (ArgumentException)
            {
                // okay
            }

            try
            {
                c = ColorFromString.ToColor("10,,20,30");
            }
            catch (ArgumentException)
            {
                // okay
            }

            try
            {
                c = ColorFromString.ToColor("10,20");
            }
            catch (ArgumentException)
            {
                // okay
            }

            try
            {
                c = ColorFromString.ToColor("10,,20");
            }
            catch (ArgumentException)
            {
                // okay
            }

            try
            {
                c = ColorFromString.ToColor("10,20,");
            }
            catch (ArgumentException)
            {
                // okay
            }

            try
            {
                c = ColorFromString.ToColor("0,256,0");
            }
            catch (ArgumentException)
            {
                // okay
            }

            try
            {
                c = ColorFromString.ToColor("10,20,30,40");
            }
            catch (ArgumentException)
            {
                // okay
            }
        }

        private void TestToColor3()
        {
            Color c;

            ColorFromString.ToColor("blue");

            ColorFromString.ToColor("black");

            ColorFromString.ToColor("White");

            ColorFromString.ToColor("CYAN");

            ColorFromString.ToColor("yeLLow");

            ColorFromString.ToColor("DarkGray");

            ColorFromString.ToColor("DarkGoldenrod");

            ColorFromString.ToColor("MediumSlateBlue");

            ColorFromString.ToColor("ButtonFace");

            try
            {
                c = ColorFromString.ToColor("whi te");
            }
            catch (ArgumentException)
            {
                // okay
            }

            try
            {
                c = ColorFromString.ToColor("blackblack");
            }
            catch (ArgumentException)
            {
                // okay
            }

            try
            {
                c = ColorFromString.ToColor("white*");
            }
            catch (ArgumentException)
            {
                // okay
            }


        }
    }
}
