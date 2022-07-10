using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ModbusControlSpace
{
    public partial class OptionsForm : Form
    {
        // properties

        public bool StopOnError
        {
            get { return checkboxStopOnCommsError.Checked; }
            set
            {
                checkboxStopOnCommsError.Checked = value;
            }
        }

        public bool UseLocalOverrides
        {
            get { return checkboxUseLocalOverrides.Checked; }
            set
            {
                checkboxUseLocalOverrides.Checked = value;
            }
        }

        public bool BeepOnInputError
        {
            get { return checkboxBeepOnInputError.Checked; }
            set
            {
                checkboxBeepOnInputError.Checked = value;
            }
        }

        public bool AutoNextRowOnEnter
        {
            get { return checkboxNextRowOnEnter.Checked; }
            set
            {
                checkboxNextRowOnEnter.Checked = value;
            }
        }

        public bool AutoStartEdit
        {
            get { return checkboxAutostartEdit.Checked; }
            set
            {
                checkboxAutostartEdit.Checked = value;
            }
        }

        public bool IgnoreSizeOnReload
        {
            get { return checkboxIgnoreFormSizeOnReload.Checked; }
            set
            {
                checkboxIgnoreFormSizeOnReload.Checked = value;
            }
        }

        public bool ShowVarProperties
        {
            get { return checkboxShowVarProperties.Checked; }
            set
            {
                checkboxShowVarProperties.Checked = value;
            }
        }

        public bool AutoRowSize
        {
            get { return checkboxAutoRowSize.Checked; }
            set
            {
                checkboxAutoRowSize.Checked = value;
            }
        }

        public bool ClearFilterOnTabChange
        {
            get { return checkboxClearFilterOnTabChange.Checked; }
            set
            {
                checkboxClearFilterOnTabChange.Checked = value;
            }
        }

        public bool MbcfileValidationEnable
        {
            get { return checkboxUseValidationSchema.Checked; }
            set { checkboxUseValidationSchema.Checked = value; }
        }

        public string MbcfileValidationFile
        {
            get { return textboxUseValidationSchema.Text; }
            set { textboxUseValidationSchema.Text = value; }
        }

        public int LoggerCapacity
        {
            get { return (int)numericLoggerCapacity.Value; }
            set { numericLoggerCapacity.Value = value; }
        }

        public bool CustomEditorEnable
        {
            get { return checkboxCustomEditor.Checked; }
            set { checkboxCustomEditor.Checked = value; }
        }

        public string CustomEditorCommand
        {
            get { return textboxCustomEditor.Text; }
            set { textboxCustomEditor.Text = value; }
        }

        public bool CheckForUpdates
        {
            get { return CheckboxCheckForUpdates.Checked; }
            set
            {
                CheckboxCheckForUpdates.Checked = value;
            }
        }

        public OptionsForm()
        {
            InitializeComponent();
        }

        private void butSelectValidationFile_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textboxUseValidationSchema.Text = openFileDialog1.FileName;
            }
        }

        private void butSelectCustomEditor_Click(object sender, EventArgs e)
        {
            if (openFileDialog2.ShowDialog() == DialogResult.OK)
            {
                textboxCustomEditor.Text = openFileDialog2.FileName;
            }
        }
    }
}
