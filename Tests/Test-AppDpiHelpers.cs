using LST.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class Test_AppDpiHelpers
    {
        // ================================================================
        // ScaleValueFromThisDpiTo96Dpi
        // ================================================================

        [TestMethod]
        public void ScaleValueFromThisDpiTo96Dpi_SmallInteger_96Dpi()
        {
            float result = AppDpiHelpers.ScaleValueFromThisDpiTo96Dpi(10, 96);

            Assert.AreEqual(10.0f, result, 0.0001f);
        }

        [TestMethod]
        public void ScaleValueFromThisDpiTo96Dpi_LargeInteger_96Dpi()
        {
            float result = AppDpiHelpers.ScaleValueFromThisDpiTo96Dpi(2000, 96);

            Assert.AreEqual(2000.0f, result, 0.0001f);
        }

        [TestMethod]
        public void ScaleValueFromThisDpiTo96Dpi_SmallInteger_192Dpi()
        {
            float result = AppDpiHelpers.ScaleValueFromThisDpiTo96Dpi(10, 192);

            Assert.AreEqual(5.0f, result, 0.0001f);
        }

        [TestMethod]
        public void ScaleValueFromThisDpiTo96Dpi_LargeInteger_192Dpi()
        {
            float result = AppDpiHelpers.ScaleValueFromThisDpiTo96Dpi(2000, 192);

            Assert.AreEqual(1000.0f, result, 0.0001f);
        }

        [TestMethod]
        public void ScaleValueFromThisDpiTo96Dpi_SmallFloat_120Dpi()
        {
            float result = AppDpiHelpers.ScaleValueFromThisDpiTo96Dpi(15.5f, 120);

            Assert.AreEqual(12.4f, result, 0.0001f);
        }

        [TestMethod]
        public void ScaleValueFromThisDpiTo96Dpi_LargeFloat_144Dpi()
        {
            float result = AppDpiHelpers.ScaleValueFromThisDpiTo96Dpi(2000.5f, 144);

            Assert.AreEqual(1333.6666f, result, 0.001f);
        }

        [TestMethod]
        public void ScaleValueFromThisDpiTo96Dpi_NegativeInteger_192Dpi()
        {
            float result = AppDpiHelpers.ScaleValueFromThisDpiTo96Dpi(-2000, 192);

            Assert.AreEqual(-1000.0f, result, 0.0001f);
        }

        [TestMethod]
        public void ScaleValueFromThisDpiTo96Dpi_FractionalValue_144Dpi()
        {
            float result = AppDpiHelpers.ScaleValueFromThisDpiTo96Dpi(25.25f, 144);

            Assert.AreEqual(16.833333f, result, 0.001f);
        }


        // ================================================================
        // ScaleValueFromThisDpiTo96DpiInt
        // ================================================================

        [TestMethod]
        public void ScaleValueFromThisDpiTo96DpiInt_ExactInteger()
        {
            int result = AppDpiHelpers.ScaleValueFromThisDpiTo96DpiInt(2000, 96);

            Assert.AreEqual(2000, result);
        }

        [TestMethod]
        public void ScaleValueFromThisDpiTo96DpiInt_ScalesDown()
        {
            int result = AppDpiHelpers.ScaleValueFromThisDpiTo96DpiInt(2000, 192);

            Assert.AreEqual(1000, result);
        }

        [TestMethod]
        public void ScaleValueFromThisDpiTo96DpiInt_RoundsDown()
        {
            int result = AppDpiHelpers.ScaleValueFromThisDpiTo96DpiInt(2010, 192);

            Assert.AreEqual(1005, result);
        }

        [TestMethod]
        public void ScaleValueFromThisDpiTo96DpiInt_RoundsFractionUp()
        {
            int result = AppDpiHelpers.ScaleValueFromThisDpiTo96DpiInt(2011, 192);

            Assert.AreEqual(1006, result);
        }

        [TestMethod]
        public void ScaleValueFromThisDpiTo96DpiInt_PositiveMidpointRoundsAwayFromZero()
        {
            // 22.5 -> 23
            int result = AppDpiHelpers.ScaleValueFromThisDpiTo96DpiInt(33.75f, 144);

            Assert.AreEqual(23, result);
        }

        [TestMethod]
        public void ScaleValueFromThisDpiTo96DpiInt_NegativeMidpointRoundsAwayFromZero()
        {
            // -22.5 -> -23
            int result = AppDpiHelpers.ScaleValueFromThisDpiTo96DpiInt(-33.75f, 144);

            Assert.AreEqual(-23, result);
        }

        [TestMethod]
        public void ScaleValueFromThisDpiTo96DpiInt_SmallFraction()
        {
            int result = AppDpiHelpers.ScaleValueFromThisDpiTo96DpiInt(1.25f, 120);

            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void ScaleValueFromThisDpiTo96DpiInt_LargeFraction()
        {
            int result = AppDpiHelpers.ScaleValueFromThisDpiTo96DpiInt(2000.5f, 192);

            Assert.AreEqual(1000, result);
        }


        // ================================================================
        // ScaleValueFrom96DpiToThisDpi
        // ================================================================

        [TestMethod]
        public void ScaleValueFrom96DpiToThisDpi_SmallInteger_96Dpi()
        {
            float result = AppDpiHelpers.ScaleValueFrom96DpiToThisDpi(10, 96);

            Assert.AreEqual(10.0f, result, 0.0001f);
        }

        [TestMethod]
        public void ScaleValueFrom96DpiToThisDpi_LargeInteger_96Dpi()
        {
            float result = AppDpiHelpers.ScaleValueFrom96DpiToThisDpi(2000, 96);

            Assert.AreEqual(2000.0f, result, 0.0001f);
        }

        [TestMethod]
        public void ScaleValueFrom96DpiToThisDpi_SmallInteger_192Dpi()
        {
            float result = AppDpiHelpers.ScaleValueFrom96DpiToThisDpi(10, 192);

            Assert.AreEqual(20.0f, result, 0.0001f);
        }

        [TestMethod]
        public void ScaleValueFrom96DpiToThisDpi_LargeInteger_192Dpi()
        {
            float result = AppDpiHelpers.ScaleValueFrom96DpiToThisDpi(2000, 192);

            Assert.AreEqual(4000.0f, result, 0.0001f);
        }

        [TestMethod]
        public void ScaleValueFrom96DpiToThisDpi_SmallFloat_120Dpi()
        {
            float result = AppDpiHelpers.ScaleValueFrom96DpiToThisDpi(15.5f, 120);

            Assert.AreEqual(19.375f, result, 0.0001f);
        }

        [TestMethod]
        public void ScaleValueFrom96DpiToThisDpi_LargeFloat_144Dpi()
        {
            float result = AppDpiHelpers.ScaleValueFrom96DpiToThisDpi(2000.5f, 144);

            Assert.AreEqual(3000.75f, result, 0.001f);
        }

        [TestMethod]
        public void ScaleValueFrom96DpiToThisDpi_NegativeInteger_192Dpi()
        {
            float result = AppDpiHelpers.ScaleValueFrom96DpiToThisDpi(-2000, 192);

            Assert.AreEqual(-4000.0f, result, 0.0001f);
        }

        [TestMethod]
        public void ScaleValueFrom96DpiToThisDpi_FractionalValue_144Dpi()
        {
            float result = AppDpiHelpers.ScaleValueFrom96DpiToThisDpi(25.25f, 144);

            Assert.AreEqual(37.875f, result, 0.0001f);
        }


        // ================================================================
        // ScaleValueFrom96DpiToThisDpiInt
        // ================================================================

        [TestMethod]
        public void ScaleValueFrom96DpiToThisDpiInt_ExactInteger()
        {
            int result = AppDpiHelpers.ScaleValueFrom96DpiToThisDpiInt(2000, 96);

            Assert.AreEqual(2000, result);
        }

        [TestMethod]
        public void ScaleValueFrom96DpiToThisDpiInt_ScalesUp()
        {
            int result = AppDpiHelpers.ScaleValueFrom96DpiToThisDpiInt(2000, 192);

            Assert.AreEqual(4000, result);
        }

        [TestMethod]
        public void ScaleValueFrom96DpiToThisDpiInt_RoundsDown()
        {
            int result = AppDpiHelpers.ScaleValueFrom96DpiToThisDpiInt(15, 120);

            Assert.AreEqual(19, result);
        }

        [TestMethod]
        public void ScaleValueFrom96DpiToThisDpiInt_RoundsUp()
        {
            int result = AppDpiHelpers.ScaleValueFrom96DpiToThisDpiInt(16, 120);

            Assert.AreEqual(20, result);
        }

        [TestMethod]
        public void ScaleValueFrom96DpiToThisDpiInt_PositiveMidpointRoundsAwayFromZero()
        {
            // 15 * 144 / 96 = 22.5 -> 23
            int result = AppDpiHelpers.ScaleValueFrom96DpiToThisDpiInt(15, 144);

            Assert.AreEqual(23, result);
        }

        [TestMethod]
        public void ScaleValueFrom96DpiToThisDpiInt_NegativeMidpointRoundsAwayFromZero()
        {
            // -15 * 144 / 96 = -22.5 -> -23
            int result = AppDpiHelpers.ScaleValueFrom96DpiToThisDpiInt(-15, 144);

            Assert.AreEqual(-23, result);
        }

        [TestMethod]
        public void ScaleValueFrom96DpiToThisDpiInt_SmallFraction()
        {
            int result = AppDpiHelpers.ScaleValueFrom96DpiToThisDpiInt(1.25f, 120);

            Assert.AreEqual(2, result);
        }

        [TestMethod]
        public void ScaleValueFrom96DpiToThisDpiInt_LargeFraction()
        {
            int result = AppDpiHelpers.ScaleValueFrom96DpiToThisDpiInt(2000.5f, 192);

            Assert.AreEqual(4001, result);
        }
    }
}
