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
    public partial class SerialPortOpenFailed : Form
    {
        public string ErrorText
        {
            get { return labelErrortext.Text; }
            set { labelErrortext.Text = value; }
        }
        public string SelectedPortName
        {
            get { return comboSerialPort.Text.Trim(); }
        }

        public SerialPortOpenFailed()
        {
            InitializeComponent();

            pic.Image = SystemIcons.Warning.ToBitmap();
            //e.Graphics.DrawIcon(SystemIcons.Error, 16, 16);

            // fill listbox with available serial ports
            comboSerialPort.Items.Clear();
            comboSerialPort.Text = string.Empty;
            comboSerialPort.Items.AddRange(SerialPort.GetPortNames());
            // select first port if we have any
            if (comboSerialPort.Items.Count > 0)
            {
                comboSerialPort.SelectedIndex = 0;
            }
        }

    }
}
