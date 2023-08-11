using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

// Windows special folders: https://blogs.msdn.microsoft.com/patricka/2010/03/18/where-should-i-store-my-data-and-configuration-files-if-i-target-multiple-os-versions

namespace LST.Helpers
{
    /// <summary>
    /// AppDpiHelpers class holds App info
    /// </summary>
    public static class AppDpiHelpers
    {
        public static float SystemDpiX = 96.0F;
        public static float SystemDpiY = 96.0F;

        public static void GetCurrentSystemDpi(Control control)
        {
            // This app is DPI-aware, get system DPI
            // We support system DPI. Or do we support per-monitor-DPI?   //TODO?
            // Store it in the Global class
            Graphics g = control.CreateGraphics();
            SystemDpiX = g.DpiX;
            SystemDpiY = g.DpiY;
            g.Dispose();
        }

        /// <summary>
        /// Convert high-DPI screen X value to standard 96-DPI value 
        /// </summary>
        public static int ScaleXcoordFromSystemDpiTo96Dpi(int x)
        {
            return (int)Math.Round((double)x / SystemDpiX * 96.0f);
        }

        /// <summary>
        /// Convert standard 96-DPI value X value to high-DPI value
        /// </summary>
        public static int ScaleXcoordFrom96DpiToSystemDpi(int x)
        {
            return (int)Math.Round((double)x / 96.0f * SystemDpiX);
        }

        /// <summary>
        /// Convert high-DPI screen Y value to standard 96-DPI value 
        /// </summary>
        public static int ScaleYcoordFromSystemDpiTo96Dpi(int y)
        {
            return (int)Math.Round((double)y / SystemDpiY * 96.0f);
        }

        /// <summary>
        /// Convert standard 96-DPI value Y value to high-DPI value
        /// </summary>
        public static int ScaleYcoordFrom96DpiToSystemDpi(int y)
        {
            return (int)Math.Round((double)y / 96.0f * SystemDpiY);
        }
        
    }
}
