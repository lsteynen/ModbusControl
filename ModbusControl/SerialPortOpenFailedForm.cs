using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using System.IO.Ports;

namespace ModbusControl
{
    public partial class SerialPortOpenFailedForm : Form
    {
        public string ErrorText
        {
            get { return ErrortextLabel.Text; }
            set { ErrortextLabel.Text = value; }
        }
        public string SelectedPortName
        {
            get { return SerialPortCombo.Text.Trim(); }
        }

        public SerialPortOpenFailedForm()
        {
            InitializeComponent();

            Pict.Image = SystemIcons.Warning.ToBitmap();
            //e.Graphics.DrawIcon(SystemIcons.Error, 16, 16);

            // fill listbox with available serial ports
            SerialPortCombo.LoadWithFirstSerialPort();
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
