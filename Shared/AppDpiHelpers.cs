using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace LST.Helpers
{
    /// <summary>
    /// Helpers for DPI-aware applications
    /// </summary>
    public static class AppDpiHelpers
    {
        /// <summary>
        /// Convert high-DPI value (like position) to standard 96-DPI value 
        /// </summary>
        public static float ScaleValueFromThisDpiTo96Dpi(float v, int deviceDpi)
        {
            return v / deviceDpi * 96.0f;
        }

        /// <summary>
        /// Like ScaleValueFromThisDpiTo96Dpi() but returns rounded int
        /// </summary>
        public static int ScaleValueFromThisDpiTo96DpiInt(float v, int deviceDpi)
        {
            // Use Math.Round(xxx, MidpointRounding.AwayFromZero) to get normal rounding insteaad of banker's rounding
            return (int)Math.Round(ScaleValueFromThisDpiTo96Dpi(v, deviceDpi), MidpointRounding.AwayFromZero);
        }

        /// <summary>
        /// Convert standard 96-DPI position to high-DPI high-DPI screen position
        /// </summary>
        public static float ScaleValueFrom96DpiToThisDpi(float v, int deviceDpi)
        {
            return v / 96.0f * deviceDpi;
        }

        /// <summary>
        /// Convert standard 96-DPI position to high-DPI high-DPI screen position
        /// </summary>
        public static int ScaleValueFrom96DpiToThisDpiInt(float v, int deviceDpi)
        {
            return (int)Math.Round(ScaleValueFrom96DpiToThisDpi(v, deviceDpi), MidpointRounding.AwayFromZero);
        }

    }
}
