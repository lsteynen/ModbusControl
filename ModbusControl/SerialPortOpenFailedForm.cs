using System;
using System.Diagnostics;
using System.IO;
using System.IO.Ports;
using System.Windows.Forms;

using LST.Helpers;

namespace ModbusControl
{
    public partial class SerialPortOpenFailedForm : Form
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

        public SerialPortOpenFailedForm()
        {
            InitializeComponent();

            pic.Image = SystemIcons.Warning.ToBitmap();
            //e.Graphics.DrawIcon(SystemIcons.Error, 16, 16);

            // fill listbox with available serial ports
            comboSerialPort.LoadWithFirstSerialPort();
        }

        private void SerialPortOpenFailedForm_Shown(object sender, EventArgs e)
        {
            AdjustFormLayout();
        }

        private void AdjustFormLayout()
        {
            Debug.Print("AdjustFormLayout:");
            Debug.Print($"DeviceDpi={DeviceDpi}");

            // nothing to do here
        }
    }
}
