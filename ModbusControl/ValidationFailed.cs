using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.IO.Ports;
using System.Windows.Forms;

namespace ModbusControlSpace
{
    public partial class ValidationFailed : Form
    {
        public string ErrorText
        {
            get { return labelErrortext.Text; }
            set { labelErrortext.Text = value; }
        }

        public ValidationFailed()
        {
            InitializeComponent();

            pic.Image = SystemIcons.Warning.ToBitmap();
            //e.Graphics.DrawIcon(SystemIcons.Error, 16, 16);

        }

    }
}
