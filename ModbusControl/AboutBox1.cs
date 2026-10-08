using LST.Helpers;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Reflection;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

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
            int hmargin = AppDpiHelpers.ScaleValueFrom96DpiToThisDpiInt(24, DeviceDpi);
            // get new form width
            int maxRight = 300;
            foreach (Control c in Controls)
            {
                if (c.Right > maxRight)
                    maxRight = c.Right;
            }
            int newWidth = maxRight + hmargin;
            Debug.Print($"new ClientSize Width={newWidth}");
            ClientSize = new Size(newWidth, ClientSize.Height);

            // center OK button (auto-center issues on scaling systems)
            int newOkButtonLeft = (ClientSize.Width - okButton.Width) / 2;
            okButton.Left = newOkButtonLeft;
        }

        private void licenseLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                string pathToLicenseFile = Path.Combine(AppHelpers.ApplicationStartupPath, "LICENSE.txt");

                if (!File.Exists(pathToLicenseFile))
                    throw new FileNotFoundException("The license file could not be found.", pathToLicenseFile);

                Process.Start(new ProcessStartInfo
                    {
                        FileName = pathToLicenseFile,
                        UseShellExecute = true   // required in .NET Core/5+ to open with the default app
                    });

                licenseLinkLabel.LinkVisited = true;
            }
            catch (FileNotFoundException ex)
            {
                MessageBox.Show($"License file not found:\n{ex.FileName}",
                    "File Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An unexpected error occurred:\n{ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region Assembly Attribute Accessors

        public string AssemblyTitle
        {
            get
            {
                var title = Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyTitleAttribute>()?.Title;
                return title ?? "";
            }
        }

        public string AssemblyDescription
        {
            get
            {
                var description = Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyDescriptionAttribute>()?.Description;
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
