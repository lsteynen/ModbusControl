using LST.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Text;
using System.Windows.Forms;

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
            AdjustFormSize();
        }

        private void AdjustFormSize()
        {
            Debug.Print("AdjustFormSize:");
            Debug.Print($"DeviceDpi={DeviceDpi}");

            // get margin, DPI adjusted 
            int hmargin = AppDpiHelpers.ScaleValueFrom96DpiToThisDpiInt(50, DeviceDpi);
            int vmargin = AppDpiHelpers.ScaleValueFrom96DpiToThisDpiInt(12, DeviceDpi);
            // get new form size
            int newWidth = buttonCancel.Left + buttonCancel.Width + hmargin;
            int newHeight = buttonCancel.Top + buttonCancel.Height + vmargin;
            Debug.Print($"newWidth={newWidth}, newHeight={newHeight}");
            ClientSize = new Size(newWidth, newHeight);
        }
    }
}
