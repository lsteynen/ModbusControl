using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using LST.Helpers;

namespace ModbusControlSpace
{
    public partial class SaveConfigurationForm : Form
    {
        // properties
        public CProject Project;
        public bool OverridesWarning;
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
            buttonSave.Enabled = (checkboxConnection.Checked 
                || checkboxSettings.Checked 
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

                if (checkboxConnection.Checked)
                {
                    // new parameters already in Project
                    Project.WriteConnection();
                    Changed = true;
                }

                if (checkboxSettings.Checked)
                {
                    // new parameters already in Project
                    Project.WriteOtherSettings();
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
                    // Project holds width in 96DPI
                    Project.FormSize.Width = AppDpiHelpers.ScaleXcoordFromSystemDpiTo96Dpi(SizeWidth);
                    Project.FormSize.Height = AppDpiHelpers.ScaleYcoordFromSystemDpiTo96Dpi(SizeHeight);
                    Project.WriteFormSize();
                    Changed = true;
                }

                if (checkboxUpdateXML.Checked)
                {
                    // update XML to current version, like convert all XML tags to lowercase, ...
                    Project.UpdateProjectXML();
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

    }
}
