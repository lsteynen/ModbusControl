using System;
using System.Diagnostics;
using System.Windows.Forms;

using LST.Helpers;

namespace ModbusControl
{
    public partial class SaveConfigurationForm : Form
    {
        // properties
        public CProject Project;
        public bool OverridesWarning;
        public int MainformDeviceDPI;
        public int SizeWidth;
        public int SizeHeight;

        private void SaveConfigurationForm_Load(object sender, EventArgs e)
        {
            panelWarning.Visible = OverridesWarning;
        }

        public SaveConfigurationForm()
        {
            InitializeComponent();

            // and update
            checkbox_CheckedChanged(null, null);
        }

        private void checkbox_CheckedChanged(object sender, EventArgs e)
        {
            buttonSave.Enabled = (checkboxProjectConfig.Checked
                || checkboxBridge.Checked
                || checkboxFormSize.Checked
                || checkboxUpdateXML.Checked);
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            // Got project?
            if (Project != null)
            {
                bool Changed = false;

                if (checkboxProjectConfig.Checked)
                {
                    // new parameters already in Project
                    Project.WriteConnection();
                    Changed = true;
                }

                if (checkboxBridge.Checked)
                {
                    // new parameters already in Project
                    Project.WriteBridgeSettings();
                    Changed = true;
                }

                if (checkboxFormSize.Checked)
                {
                    // put new size in Project
                    // Project holds width in 96DPI, scale MainForm_DPI
                    Project.FormSize.Width = AppDpiHelpers.ScaleValueFromThisDpiTo96DpiInt(SizeWidth, MainformDeviceDPI);
                    Project.FormSize.Height = AppDpiHelpers.ScaleValueFromThisDpiTo96DpiInt(SizeHeight, MainformDeviceDPI);
                    Project.WriteFormSize();
                    Changed = true;
                }

                if (checkboxUpdateXML.Checked)
                {
                    // update XML to current version, like convert all XML tags to lowercase, ...
                    Project.UpdateXML();
                    Changed = true;
                }

                if (Changed)
                {
                    Project.SaveFile();
                }

                Close();
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void SaveConfigurationForm_Shown(object sender, EventArgs e)
        {
            AdjustFormLayout();
        }

        private void AdjustFormLayout()
        {
            Debug.Print("AdjustFormLayout:");
            Debug.Print($"DeviceDpi={DeviceDpi}");

            // get margin, DPI adjusted 
            int hmargin = AppDpiHelpers.ScaleValueFrom96DpiToThisDpiInt(6, DeviceDpi);
            int vmargin = AppDpiHelpers.ScaleValueFrom96DpiToThisDpiInt(6, DeviceDpi);
            // get new form size
            int newHeight = optionsPanel.Location.Y + optionsPanel.Height + vmargin;
            Debug.Print($"newWidth={ClientSize.Width}, newHeight={newHeight}");
            ClientSize = new Size(ClientSize.Width, newHeight);
        }
    }
}
