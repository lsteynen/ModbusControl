using LST.Helpers;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Reflection;
using System.Windows.Forms;

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
            Text = String.Format("About {0}", AssemblyTitle);
            labelName.Text = AssemblyProduct;
            labelVersion.Text = "Version " + VersionString;

            labelAppdata.Text = "User application data folder: " + AppdataPath;

            // show the .net core version
            string dotnetcoreVersion = System.Runtime.InteropServices.RuntimeInformation.FrameworkDescription;
            labelDotNetVersion.Text = $".NET (core) version: {dotnetcoreVersion}";

            labelFormSize.Text = "Main window size: " + FormWidth + " x " + FormHeight;
        }

        private void AboutBox1_Shown(object sender, EventArgs e)
        {
            AdjustFormSize();

            /*
            // test 
            string s1 = AssemblyTitle;
            string s2 = AssemblyDescription;
            string s3 = AssemblyProduct;
            string s4 = AssemblyCopyright;
            string s5 = AssemblyCompany;
            string s6 = AssemblyVersion;
            */
        }

        private void AdjustFormSize()
        {
            Debug.Print("AdjustFormSize:");
            Debug.Print($"DeviceDpi={DeviceDpi}");

            PerformLayout();

            // get margin, DPI adjusted 
            int hmargin = AppDpiHelpers.ScaleValueFrom96DpiToThisDpiInt(40, DeviceDpi);
            int vmargin = AppDpiHelpers.ScaleValueFrom96DpiToThisDpiInt(8, DeviceDpi);
            // get new form size
            int newWidth = tableLayoutPanel1.PreferredSize.Width + hmargin;
            int newHeight = panel1.Location.Y + panel1.Height + vmargin;
            panel1.Width = newWidth;
            Debug.Print($"newWidth={newWidth}, newHeight={newHeight}");
            ClientSize = new Size(newWidth, newHeight);
        }

        #region Assembly Attribute Accessors

        public string AssemblyTitle
        {
            get
            {
                var title = Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyTitleAttribute>() ?.Title;
                return title ?? "";
            }
        }

        public string AssemblyDescription
        {
            get
            {
                var description = Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyDescriptionAttribute>() ?.Description;
                return description ?? "";
            }
        }

        public string AssemblyProduct
        {
            get
            {
                var product = Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyProductAttribute>()?.Product;
                return product ?? "";
            }
        }

        public string AssemblyCopyright
        {
            get
            {
                var copyright = Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyCopyrightAttribute>()?.Copyright;
                return copyright ?? "";
            }
        }

        public string AssemblyCompany
        {
            get
            {
                var company = Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyCompanyAttribute>()?.Company;
                return company ?? ""; 
            }
        }

        public string AssemblyVersion
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }


        #endregion


    }
}
