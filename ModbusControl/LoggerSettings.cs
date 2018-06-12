using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LST.Helpers
{
    public partial class LoggerSettings : Form
    {
        public int CapacityLines
        {
            get { return (int)numericLoggerMaxLines.Value; }
            set
            {
                numericLoggerMaxLines.Value = value;
            }
        }

        public LoggerSettings()
        {
            InitializeComponent();

            // limit number of digits that can be typed
            ((TextBox)numericLoggerMaxLines.Controls[1]).MaxLength = numericLoggerMaxLines.Maximum.ToString().Length;
        }
    }
}
