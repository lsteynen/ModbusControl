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
        public int MainFormWidth { get; set; }
        public int MainFormHeight { get; set; }
        public int MainFormDpi { get; set; }

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

            labelFormSize.Text = $"Main window size: {MainFormWidth} x {MainFormHeight} (at {MainFormDpi} DPI)";
        }

        private void AboutBox1_Shown(object sender, EventArgs e)
        {
            AdjustLayout();

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

        private void AdjustLayout()
        {
            Debug.Print("AdjustLayout:");
            Debug.Print($"DeviceDpi={DeviceDpi}");

            // adjust form width to fit labelAppdata text

            // get margin, DPI adjusted 
            int hmargin = AppDpiHelpers.ScaleValueFrom96DpiToThisDpiInt(30, DeviceDpi);
            // get new form width
            int newWidth = labelAppdata.Left + labelAppdata.Width + hmargin;
            Debug.Print($"new ClientSize Width={newWidth}");
            ClientSize = new Size(newWidth, ClientSize.Height);

            // center OK button (auto-center issues on scaling systems)
            int newOkButtonLeft = (ClientSize.Width - okButton.Width) / 2;
            okButton.Left = newOkButtonLeft;
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
