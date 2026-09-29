using LST.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Ports;
using System.Text;
using System.Windows.Forms;

namespace ModbusControl
{
    public partial class ProjectConfigurationForm : Form
    {
        private bool mConnectionInUse;

        // Properties - settings

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

        public int ConnectionWriteHoldingregFunctionCode
        {
            // 6=write_single, 16=write_multiple, 0=auto
            get
            {
                if (radWriteFC6.Checked) return 6;
                if (radWriteFC16.Checked) return 16;
                return 0;
            }
            set
            {
                if (value == 6) radWriteFC6.Checked = true;
                else if (value == 16) radWriteFC16.Checked = true;
                else radWriteFCAuto.Checked = true;
            }
        }

        public Global.eEndian ConnectionInt32Endian
        {
            get
            {
                if (radInt32LittleEndian.Checked) return Global.eEndian.Little;
                return Global.eEndian.Big;
            }
            set
            {
                radInt32LittleEndian.Checked = (value == Global.eEndian.Little);
                radInt32BigEndian.Checked = (value == Global.eEndian.Big);
            }
        }

        public Global.eEndian ConnectionFloat32Endian
        {
            get
            {
                if (radFloat32LittleEndian.Checked) return Global.eEndian.Little;
                return Global.eEndian.Big;
            }
            set
            {
                radFloat32LittleEndian.Checked = (value == Global.eEndian.Little);
                radFloat32BigEndian.Checked = (value == Global.eEndian.Big);
            }
        }

        public Global.eEndian ConnectionStringEndian
        {
            get
            {
                if (radStringLittleEndian.Checked) return Global.eEndian.Little;
                return Global.eEndian.Big;
            }
            set
            {
                radStringLittleEndian.Checked = (value == Global.eEndian.Little);
                radStringBigEndian.Checked = (value == Global.eEndian.Big);
            }
        }

        public string ConnectionStringEncoding
        {
            get
            {
                // the encoding name 
                return comboStringEncoding.SelectedValue as string;
            }
            set
            {
                bool isValid = SetComboboxSelectedValueIgnoreCase(comboStringEncoding, value);
                if (isValid == false)
                {
                    comboStringEncoding.SelectedValue = "us-ascii";
                }
            }
        }

        public bool SetComboboxSelectedValueIgnoreCase(ComboBox combo, string value)
        {
            if (combo == null)
            {
                return false;
            }

            if (value == null)
            {
                combo.SelectedValue = null;
                return false;
            }

            string valueMember = combo.ValueMember;
            for (int i = 0; i < combo.Items.Count; i++)
            {
                object item = combo.Items[i];
                string itemValue = combo.GetItemText(item.GetType().GetProperty(valueMember)?.GetValue(item, null));

                if (string.Equals(itemValue.Trim(), value.Trim(), StringComparison.OrdinalIgnoreCase))
                {
                    combo.SelectedValue = itemValue;
                    return true;
                }
            }

            return false;
        }


        public int OptionsHoldingregistersStart
        {
            // is write only
            set
            {
                string text = string.Empty;

                switch (value)
                {
                    case 40001:
                        text = "40001-49999 + 400001-465536";
                        break;
                    case 40000:
                        text = "40000-49999 + 400000-465535";
                        break;
                    case 400001:
                        text = "400001-465536";
                        break;
                    case 400000:
                        text = "400000-465535";
                        break;

                    default:
                        text = value.ToString() + "(invalid)";
                        break;
                }

                textboxHoldingRegistersStart.Text = text;
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

        public bool ConnectionTypeIsOverride
        {
            set
            {
                picConnectionTypeIsOverride.Visible = value;
            }
        }

        public bool PortIsOverride
        {
            set
            {
                picPortIsOverride.Visible = value;
            }
        }

        public bool BaudrateIsOverride
        {
            set
            {
                picBaudrateIsOverride.Visible = value;
            }
        }

        public bool ParityIsOverride
        {
            set
            {
                picParityIsOverride.Visible = value;
            }
        }

        public bool EndofmessagetimeoutIsOverride
        {
            set
            {
                picEndofmessagetimeoutIsOverride.Visible = value;
            }
        }

        public bool OnewireechosuppressionIsOverride
        {
            set
            {
                picOnewireechosuppressionIsOverride.Visible = value;
            }
        }

        public bool IPAddressIsOverride
        {
            set
            {
                picIPAddressIsOverride.Visible = value;
            }
        }

        public bool IPPortIsOverride
        {
            set
            {
                picIPPortIsOverride.Visible = value;
            }
        }

        public bool SlaveIDIsOverride
        {
            set
            {
                picSlaveIDIsOverride.Visible = value;
            }
        }

        public bool MaxFrameSizeIsOverride
        {
            set
            {
                picMaxFrameSizeIsOverride.Visible = value;
            }
        }

        public bool ResponsetimeoutIsOverride
        {
            set
            {
                picResponsetimeoutIsOverride.Visible = value;
            }
        }

        public bool InterframedelayIsOverride
        {
            set
            {
                picInterframedelayIsOverride.Visible = value;
            }
        }

        public bool SamplerateIsOverride
        {
            set
            {
                picSamplerateIsOverride.Visible = value;
            }
        }

        public bool WriteregFCIsOverride
        {
            set
            {
                picwriteregFCIsOverride.Visible = value;
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

        public ProjectConfigurationForm()
        {
            InitializeComponent();

            // limit number of digits that can be typed in numericupdowns
            ((TextBox)numericMaximumFrameSize.Controls[1]).MaxLength = numericMaximumFrameSize.Maximum.ToString().Length;
            ((TextBox)numericResponseTimeout.Controls[1]).MaxLength = numericResponseTimeout.Maximum.ToString().Length;
            ((TextBox)numericSlaveAddress.Controls[1]).MaxLength = numericSlaveAddress.Maximum.ToString().Length;
            ((TextBox)numericIpPort.Controls[1]).MaxLength = numericIpPort.Maximum.ToString().Length;
            ((TextBox)numericSerialEndOfMessageTimeout.Controls[1]).MaxLength = numericSerialEndOfMessageTimeout.Maximum.ToString().Length;
            ((TextBox)numericTcpServerPort.Controls[1]).MaxLength = numericTcpServerPort.Maximum.ToString().Length;
            ((TextBox)numericSamplerate.Controls[1]).MaxLength = numericSamplerate.Maximum.ToString().Length;

            // fill combobox with available serial ports
            comboSerialPort.LoadWithFirstSerialPort();

            // load a dictionary with the supported string encodings
            // use a dictionary to have 2 values per item: key/name and displaytext
            // get the displayname of each encoding
            Dictionary<string, string> encodingDict = new Dictionary<string, string>();
            string[] listOfSupportedStringEncodings = Converts.GetListOfSupportedStringEncodings();
            foreach (string encName in listOfSupportedStringEncodings)
            {
                try
                {
                    // Encoding.GetEncoding() is case insensitive
                    Encoding enc = Encoding.GetEncoding(encName);
                    encodingDict[encName] = encName + "   (" + enc.EncodingName + ")";
                }
                catch
                {
                    // ignore, it seems our encoding is not supported in the current .NET framework.
                }
            }
            // bind dictionary to ComboBox
            comboStringEncoding.DataSource = new BindingSource(encodingDict, null);
            comboStringEncoding.DisplayMember = "Value"; // Display name
            comboStringEncoding.ValueMember = "Key";     // Encoding name

#if DEBUG
            butDebugInfo.Visible = true;
#endif

        }

        private void ComboConnection_SelectedIndexChanged(object sender, EventArgs e)
        {
            groupBoxSerialInterface.Enabled = (comboConnection.SelectedIndex == (int)Global.eConnectionType.SerialRTU);
            groupBoxTcpInterface.Enabled = ((comboConnection.SelectedIndex == (int)Global.eConnectionType.ModbusTcp) || (comboConnection.SelectedIndex == (int)Global.eConnectionType.RtuOverTcp));
        }

        private void ButtonApply_Click(object sender, EventArgs e)
        {
            if (this.OnApply != null)
            {
                OnApply(this);
            }
        }

        private void ConfigForm_Shown(object sender, EventArgs e)
        {
            // show warning panel if any of the warning icons is shown
            // can't do that earlier because the visibility of the icons is set after the properties are set by the caller
            warningPanel.Visible = picConnectionTypeIsOverride.Visible
                                || picPortIsOverride.Visible
                                || picBaudrateIsOverride.Visible
                                || picParityIsOverride.Visible
                                || picEndofmessagetimeoutIsOverride.Visible
                                || picOnewireechosuppressionIsOverride.Visible
                                || picIPAddressIsOverride.Visible
                                || picIPPortIsOverride.Visible
                                || picSlaveIDIsOverride.Visible
                                || picMaxFrameSizeIsOverride.Visible
                                || picResponsetimeoutIsOverride.Visible
                                || picInterframedelayIsOverride.Visible
                                || picSamplerateIsOverride.Visible
                                || picwriteregFCIsOverride.Visible;

            AdjustFormSize();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Debug.Print("Debug: Print info: ");
            Debug.Print($"buttonPanel.Location.Y={buttonPanel.Location.Y}, buttonPanel.Height={buttonPanel.Height}");
            Debug.Print($"form.Height={Height}");
        }

        private void AdjustFormSize()
        {
            Debug.Print("AdjustFormSize:");
            Debug.Print($"DeviceDpi={DeviceDpi}");

            // get margin, DPI adjusted 
            int hmargin = AppDpiHelpers.ScaleValueFrom96DpiToThisDpiInt(6, DeviceDpi);
            int vmargin = AppDpiHelpers.ScaleValueFrom96DpiToThisDpiInt(6, DeviceDpi);
            // get new form size
            int newWidth = ClientSize.Width + hmargin;
            int newHeight = buttonPanel.Location.Y + buttonPanel.Height + vmargin;
            Debug.Print($"newWidth={newWidth}, newHeight={newHeight}");
            ClientSize = new Size(newWidth, newHeight);
        }
    }
}
