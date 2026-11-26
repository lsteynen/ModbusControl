using System;
using System.Text;
using System.Windows.Forms;
using LST.Helpers;

namespace ModbusControl
{
    public partial class OptionsForm : Form
    {
        // data
        private string mDefaultMbcfileValidationFile;

        // properties

        public bool StopOnError
        {
            get { return StopOnPollingErrorCheckbox.Checked; }
            set
            {
                StopOnPollingErrorCheckbox.Checked = value;
            }
        }

        public bool BeepOnInputError
        {
            get { return BeepOnInputErrorCheckbox.Checked; }
            set
            {
                BeepOnInputErrorCheckbox.Checked = value;
            }
        }

        public bool AutoNextRowOnEnter
        {
            get { return NextRowOnEnterCheckbox.Checked; }
            set
            {
                NextRowOnEnterCheckbox.Checked = value;
            }
        }

        public bool AutoStartEdit
        {
            get { return AutostartEditCheckbox.Checked; }
            set
            {
                AutostartEditCheckbox.Checked = value;
            }
        }

        public bool IgnoreSizeOnReload
        {
            get { return IgnoreFormSizeOnReloadCheckbox.Checked; }
            set
            {
                IgnoreFormSizeOnReloadCheckbox.Checked = value;
            }
        }

        public bool ShowVarProperties
        {
            get { return ShowVarPropertiesCheckbox.Checked; }
            set
            {
                ShowVarPropertiesCheckbox.Checked = value;
            }
        }

        public bool AutoRowSize
        {
            get { return AutoRowSizeCheckbox.Checked; }
            set
            {
                AutoRowSizeCheckbox.Checked = value;
            }
        }

        public bool ClearFilterOnTabChange
        {
            get { return ClearFilterOnTabChangeCheckbox.Checked; }
            set
            {
                ClearFilterOnTabChangeCheckbox.Checked = value;
            }
        }

        public bool MbcfileValidationEnable
        {
            get { return UseValidationSchemaCheckbox.Checked; }
            set { UseValidationSchemaCheckbox.Checked = value; }
        }

        public string MbcfileValidationFile
        {
            get { return UseValidationSchemaTextbox.Text; }
            set { UseValidationSchemaTextbox.Text = value; }
        }

        public string DefaultMbcfileValidationFile
        {
            get { return mDefaultMbcfileValidationFile; }
            set
            {
                mDefaultMbcfileValidationFile = value;
                DefaultValidationFileBtn.Enabled = !string.IsNullOrEmpty(DefaultMbcfileValidationFile);
            }
        }

        public int LoggerCapacity
        {
            get { return (int)LoggerCapacityNumeric.Value; }
            set { LoggerCapacityNumeric.Value = value; }
        }

        public bool CustomEditorEnable
        {
            get { return CustomEditorCheckbox.Checked; }
            set { CustomEditorCheckbox.Checked = value; }
        }

        public string CustomEditorCommand
        {
            get { return CustomEditorTextbox.Text; }
            set { CustomEditorTextbox.Text = value; }
        }

        public bool CheckForUpdates
        {
            get { return CheckForUpdatesCheckbox.Checked; }
            set
            {
                CheckForUpdatesCheckbox.Checked = value;
            }
        }

        public OptionsForm()
        {
            InitializeComponent();
        }

        private void SelectValidationFileBtn_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                UseValidationSchemaTextbox.Text = openFileDialog1.FileName;
            }
        }

        private void DefaultValidationFileBtn_Click(object sender, EventArgs e)
        {
            UseValidationSchemaTextbox.Text = DefaultMbcfileValidationFile;
        }

        private void SelectCustomEditorBtn_Click(object sender, EventArgs e)
        {
            if (openFileDialog2.ShowDialog() == DialogResult.OK)
            {
                CustomEditorTextbox.Text = openFileDialog2.FileName;
            }
        }

        private void OptionsForm_Shown(object sender, EventArgs e)
        {
            AdjustLayout();
        }

        private void AdjustLayout()
        {
            // nothing to do
        }
    }
}

