using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;

namespace ModbusControlSpace
{
    public partial class ConfigForm : Form
    {
        private bool mConnectionInUse;

        // Properties - settings

        public bool OverridesWarning
        {
            // not used
            get { return panelWarning.Visible; }
            set { panelWarning.Visible = value; }
        }

        public bool ConnectionInUse
        {
            // not used
            get { return mConnectionInUse; }
            set { mConnectionInUse = value; }
        }

        public Global.eConnectionType ConnectionType
        {
            get { return (Global.eConnectionType)comboConnection.SelectedIndex; }
            set 
            { 
                comboConnection.SelectedIndex = (int)value; 
            }
        }

        public string ConnectionSerialPort
        {
            get { return comboSerialPort.Text; }
            set
            {
                comboSerialPort.Text = value;
                comboSerialPort.SelectedIndex = comboSerialPort.Items.IndexOf(value); 
            }
        }

        public int ConnectionSerialBaudrate
        {
            get
            {
                UInt32 TmpBaudrate = 0;
                UInt32.TryParse(comboSerialBaudrate.Text, out TmpBaudrate);
                return (int)TmpBaudrate;
            }
            set
            {
                comboSerialBaudrate.Text = value.ToString();
                comboSerialBaudrate.SelectedIndex = comboSerialBaudrate.Items.IndexOf(value.ToString());
            }
        }

        public Parity ConnectionSerialParity
        {
            get
            {
                // get parity enum
                return (Parity)Enum.Parse(typeof(Parity), comboSerialParity.Text, true);
            }
            set
            {
                comboSerialParity.SelectedIndex = comboSerialParity.Items.IndexOf(value.ToString());
            }
        }

        public int ConnectionSerialEndOfMessageTimeout
        {
            get { return (int)numericSerialEndOfMessageTimeout.Value; }
            set
            {
                numericSerialEndOfMessageTimeout.Value = value;
            }
        }

        public bool ConnectionSerialOneWireEchoSuppression
        {
            get { return checkboxSerialOneWireEchoSuppression.Checked; }
            set
            {
                checkboxSerialOneWireEchoSuppression.Checked = value;
            }
        }

        public string ConnectionIPAddress
        {
            get { return textboxIpAddress.Text; }
            set
            {
                textboxIpAddress.Text = value;
            }
        }

        public UInt16 ConnectionIPPort
        {
            get { return (UInt16)numericIpPort.Value; }
            set
            {
                numericIpPort.Value = value;
            }
        }

        public Global.eTcpConnectMode ConnectionTcpConnectMode
        {
            get { return (Global.eTcpConnectMode)comboTcpConnectMode.SelectedIndex; }
            set
            {
                comboTcpConnectMode.SelectedIndex = (int)value;
            }
        }

        public Byte ConnectionSlaveID
        {
            get { return (Byte)numericSlaveAddress.Value; }
            set
            {
                numericSlaveAddress.Value = value;
            }
        }

        public int ConnectionMaximumFrameSize
        {
            get { return (int)numericMaximumFrameSize.Value; }
            set
            {
                numericMaximumFrameSize.Value = value;
            }
        }

        public int ConnectionResponseTimeout
        {
            get { return (int)numericResponseTimeout.Value; }
            set
            {
                numericResponseTimeout.Value = value;
            }
        }

        public int ConnectionInterFrameDelay
        {
            get { return (int)numericInterFrameDelay.Value; }
            set
            {
                numericInterFrameDelay.Value = value;
            }
        }

        public Global.eWriteHoldingregFunctionCode ConnectionWriteHoldingregFunctionCode
        {
            get 
            { 
                if( radiobuttonWriteFC6.Checked) return Global.eWriteHoldingregFunctionCode.FC6; 
                if( radiobuttonWriteFC16.Checked) return Global.eWriteHoldingregFunctionCode.FC16;
                if (radiobuttonWriteFCAuto.Checked) return Global.eWriteHoldingregFunctionCode.Auto;
                return Global.eWriteHoldingregFunctionCode.Alternate; 
            }
            set
            {
                radiobuttonWriteFC6.Checked = (value == Global.eWriteHoldingregFunctionCode.FC6); 
                radiobuttonWriteFC16.Checked = (value == Global.eWriteHoldingregFunctionCode.FC16); 
                radiobuttonWriteFCAuto.Checked = (value == Global.eWriteHoldingregFunctionCode.Auto);
                radiobuttonWriteFCAlternate.Checked = (value == Global.eWriteHoldingregFunctionCode.Alternate);
            }
        }

        public Global.eEndian ConnectionInt32Endian
        {
            get 
            { 
                if( radiobuttonInt32LittleEndian.Checked) return Global.eEndian.Little; 
                return Global.eEndian.Big; 
            }
            set
            {
                radiobuttonInt32LittleEndian.Checked = (value == Global.eEndian.Little); 
                radiobuttonInt32BigEndian.Checked = (value == Global.eEndian.Big); 
            }
        }

        public Global.eEndian ConnectionFloat32Endian
        {
            get 
            { 
                if( radiobuttonFloat32LittleEndian.Checked) return Global.eEndian.Little; 
                return Global.eEndian.Big; 
            }
            set
            {
                radiobuttonFloat32LittleEndian.Checked = (value == Global.eEndian.Little); 
                radiobuttonFloat32BigEndian.Checked = (value == Global.eEndian.Big); 
            }
        }

        public Global.eEndian ConnectionAscii16Endian
        {
            get
            {
                if (radiobuttonAsciiLittleEndian.Checked) return Global.eEndian.Little;
                return Global.eEndian.Big;
            }
            set
            {
                radiobuttonAsciiLittleEndian.Checked = (value == Global.eEndian.Little);
                radiobuttonAsciiBigEndian.Checked = (value == Global.eEndian.Big);
            }
        }

        public int OptionsHoldingregistersStart
        {
            get { return ValueFromString(textboxHoldingRegistersStart.Text); }
            set
            {
                string[] hrArray = { "40000-49999", "40001-49999", "400000-465535", "400001-465536" };
                // Find first element starting with substring
                textboxHoldingRegistersStart.Text = Array.Find(hrArray, element => element.StartsWith(value.ToString()));
            }
        }

        public int OptionsSamplerate
        {
            get { return (int)numericSamplerate.Value; }
            set
            {
                numericSamplerate.Value = value;
            }
        }

        public bool ModbusTcpBridgeEnable
        {
            get { return checkboxTcpServerEnable.Checked; }
            set
            {
                checkboxTcpServerEnable.Checked = value;
            }
        }

        public UInt16 ModbusTcpBridgeIPPort
        {
            get { return (UInt16)numericTcpServerPort.Value; }
            set
            {
                numericTcpServerPort.Value = value;
            }
        }

        // Apply button callback
        public delegate void ApplyHandler(object sender);
        public event ApplyHandler OnApply;

        // Implementation

        public ConfigForm()
        {
            InitializeComponent();

            // limit number of digits that can be typed in numericupdown
            ((TextBox)numericMaximumFrameSize.Controls[1]).MaxLength = numericMaximumFrameSize.Maximum.ToString().Length;
            ((TextBox)numericResponseTimeout.Controls[1]).MaxLength = numericResponseTimeout.Maximum.ToString().Length;
            ((TextBox)numericSlaveAddress.Controls[1]).MaxLength = numericSlaveAddress.Maximum.ToString().Length;
            ((TextBox)numericIpPort.Controls[1]).MaxLength = numericIpPort.Maximum.ToString().Length;
            ((TextBox)numericSerialEndOfMessageTimeout.Controls[1]).MaxLength = numericSerialEndOfMessageTimeout.Maximum.ToString().Length;
            ((TextBox)numericTcpServerPort.Controls[1]).MaxLength = numericTcpServerPort.Maximum.ToString().Length;
            ((TextBox)numericSamplerate.Controls[1]).MaxLength = numericSamplerate.Maximum.ToString().Length;

            // fill listbox with available serial ports
            comboSerialPort.LoadWithFirstSerialPort();
        }

        private void comboConnection_SelectedIndexChanged(object sender, EventArgs e)
        {
            groupBoxSerialInterface.Enabled = (comboConnection.SelectedIndex == (int)Global.eConnectionType.SerialRTU);
            groupBoxTcpInterface.Enabled = ((comboConnection.SelectedIndex == (int)Global.eConnectionType.ModbusTcp) || (comboConnection.SelectedIndex == (int)Global.eConnectionType.RtuOverTcp));
        }

        /// <summary>
        /// Get the value in the begin of the string, ignore non-digits that follow
        /// </summary>
        private int ValueFromString(string s)
        {
            int result = 0;
            for (int i = 0; (i < s.Length) && Char.IsDigit(s[i]); ++i)
            {
                result = (result * 10) + (s[i] - '0');
            }
            return result;
        }

        private void buttonApply_Click(object sender, EventArgs e)
        {
            if (this.OnApply != null)
            {
                OnApply(this);
            }
        }

    }
}
