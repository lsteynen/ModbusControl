using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Reflection;
using System.Windows.Forms;
using Microsoft.Win32;

namespace ModbusControl
{
    partial class AboutBox1 : Form
    {
        // properties

        public string VersionString { get; set; }
        public string AppdataPath { get; set; }
        public int FormWidth { get; set; }
        public int FormHeight { get; set; }
        
        public AboutBox1()
        {
            InitializeComponent();
        }

        private void AboutBox1_Load(object sender, EventArgs e)
        {
            this.Text = String.Format("About {0}", AssemblyTitle);
            this.labelName.Text = AssemblyProduct;
            this.labelVersion.Text = "Version " + VersionString;

            this.labelAppdata.Text = "User application data folder: " + AppdataPath;
            //this.labelDotNetVersion.Text = ".NET CLR runtime version: " + Environment.Version;      // is the CLR version
            this.labelDotNetVersion.Text = ".NET framework version: " + GetDotnetFrameworkVersion();
            this.labelFormSize.Text = "Form size: " + FormWidth + " x " + FormHeight;
        }

        #region Assembly Attribute Accessors

        public string AssemblyTitle
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0)
                {
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    if (titleAttribute.Title != "")
                    {
                        return titleAttribute.Title;
                    }
                }
                return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }

        public string AssemblyVersion
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        public string AssemblyDescription
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }

        public string AssemblyProduct
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }

        public string AssemblyCopyright
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        public string AssemblyCompany
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCompanyAttribute)attributes[0]).Company;
            }
        }

        public string GetDotnetFrameworkVersion()
        {
            string retstr = "?";

            using (RegistryKey ndpKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32).OpenSubKey("SOFTWARE\\Microsoft\\NET Framework Setup\\NDP\\v4\\Full\\"))
            {
                int releaseKey = Convert.ToInt32(ndpKey.GetValue("Release"));
                retstr = CheckFor45PlusVersion(releaseKey);
            }

            return retstr;
        }

        // Checking the version using >= will enable forward compatibility,  
        // however you should always compile your code on newer versions of 
        // the framework to ensure your app works the same. 
        private static string CheckFor45PlusVersion(int releaseKey)
        {
            if (releaseKey >= 533320) return "4.8.1";
            if (releaseKey >= 528040) return "4.8";
            if (releaseKey >= 461808) return "4.7.2";
            if (releaseKey >= 461308) return "4.7.1";
            if (releaseKey >= 460798) return "4.7";
            if (releaseKey >= 394802) return "4.6.2";
            if (releaseKey >= 394254) return "4.6.1";
            if (releaseKey >= 393295) return "4.6";
            if (releaseKey >= 379893) return "4.5.2";
            if (releaseKey >= 378675) return "4.5.1";
            if (releaseKey >= 378389) return "4.5";

            return "Unknown version";
        }

        #endregion

        private void logoPictureBox_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
