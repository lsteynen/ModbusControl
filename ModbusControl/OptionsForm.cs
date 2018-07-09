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

        public int LoggerCapacity
        {
            get { return (int)numericLoggerCapacity.Value; }
            set { numericLoggerCapacity.Value = value; }
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

    }
}
