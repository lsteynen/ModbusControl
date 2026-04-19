using System;
using System.Windows.Forms;
using System.IO.Ports;
using System.Diagnostics;

namespace ModbusControl
{
    public partial class SerialPortCombobox : ComboBox
    {
        public SerialPortCombobox()
        {
            InitializeComponent();

            // on opening dropdown: refresh the list with serial ports
            this.DropDown += new System.EventHandler(this.SerialPortCombobox_DropDown);

        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }

        private void SerialPortCombobox_DropDown(object sender, EventArgs e)
        {
            ComboBox box = (ComboBox)sender;
            Debug.Print(DateTime.Now.TimeOfDay + ": SerialPortCombobox_DropDown()");

            // fill/refresh listbox with available serial ports
            // keep the previously selected port selected, if possible
            string PrevSelected = box.Text;
            box.Items.Clear();
            box.Items.AddRange(SerialPort.GetPortNames());
            box.SelectedIndex = box.Items.IndexOf(PrevSelected);
        }

        public void LoadWithFirstSerialPort()
        {
            // fill listbox with available serial ports
            this.Items.Clear();
            this.Text = string.Empty;
            this.Items.AddRange(SerialPort.GetPortNames());
            // select first port if we have any
            if (this.Items.Count > 0)
            {
                this.SelectedIndex = 0;
            }
        }
    }
}
