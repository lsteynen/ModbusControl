using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Net;
using System.Text;
using System.Xml;
using System.Windows.Forms;

// Windows special folders: https://blogs.msdn.microsoft.com/patricka/2010/03/18/where-should-i-store-my-data-and-configuration-files-if-i-target-multiple-os-versions

namespace ModbusControlSpace
{
    /// <summary>
    /// Global class holds global stuff
    /// </summary>
    public static class Global
    {
        public enum eConnectionType { SerialRTU = 0, ModbusTcp = 1, RtuOverTcp = 2 }
        public enum eTcpConnectMode { Longlived = 0, Shortlived = 1 }
        public enum eWriteHoldingregFunctionCode { FC6, FC16, Auto, Alternate};
        public enum eEndian { Little, Big };
        public enum eFormat { Int = 0, UInt, Hex, Bin, Float, ASCII };
        public static string[] FormatText = { "int", "uint", "hex", "bin", "float", "ASCII" };

        public static string VersionString;
        public static string ApplicationStartupPath;
        public static string MyDocumentsDir;
        public static float SystemDpiX = 96.0F;
        public static float SystemDpiY = 96.0F;

        static Global()
        {
            // Get version string
            Version v = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            VersionString = "v" + v.Major + "." + v.Minor + "." + v.Build;
            if (v.Revision > 0) VersionString += "." + v.Revision;

            // Get Special Folders paths
            ApplicationStartupPath = Application.StartupPath;
            MyDocumentsDir = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments), "ModbusControl");
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

        /// <summary>
        /// Get setting of radiobuttons group
        /// Uses RadioButton.Tag as string
        /// </summary>
        public static string GetRadioGroupTag(Control Container)
        {
            foreach (Control c in Container.Controls)
            {
                if ((c is RadioButton) && (c as RadioButton).Checked)
                {
                    return (string)(c as RadioButton).Tag;
                }
            }

            return String.Empty;
        }

        /// <summary>
        /// Set setting of radiobuttons group
        /// Uses RadioButton.Tag as string
        /// </summary>
        public static void SetRadioGroupByTag(Control Container, string Tag)
        {
            foreach (Control c in Container.Controls)
            {
                if ((c is RadioButton) && ((string)((c as RadioButton).Tag)) == Tag)
                {
                    (c as RadioButton).Checked = true;
                    break;
                }
            }
        }

        /// <summary>
        /// Split a command line string into command (program) and arguments string
        /// Both can be quoted
        /// </summary>
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
