using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ModbusControlSpace
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Tell windows that this app is DPI-aware. Is supported in Vista and higher.
            if (Environment.OSVersion.Version.Major >= 6)
                SetProcessDPIAware(); 

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool SetProcessDPIAware();
    }
}