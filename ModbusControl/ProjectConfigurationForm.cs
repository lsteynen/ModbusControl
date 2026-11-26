using LST.Helpers;
using System.IO.Ports;
using System;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

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
            get { return (Global.eConnectionType)ConnectionCombo.SelectedIndex; }
            set
            {
                ConnectionCombo.SelectedIndex = (int)value;
            }
        }

        public string ConnectionSerialPort
        {
            get { return SerialPortCombo.Text; }
            set
            {
                SerialPortCombo.Text = value;
                SerialPortCombo.SelectedIndex = SerialPortCombo.Items.IndexOf(value);
            }
        }

        public int ConnectionSerialBaudrate
        {
            get
            {
                UInt32 TmpBaudrate;
                UInt32.TryParse(SerialBaudrateCombo.Text, out TmpBaudrate);
                return (int)TmpBaudrate;
            }
            set
            {
                SerialBaudrateCombo.Text = value.ToString();
                SerialBaudrateCombo.SelectedIndex = SerialBaudrateCombo.Items.IndexOf(value.ToString());
            }
        }

        public LST.Helpers.Parity.EParity ConnectionSerialParity
        {
            get
            {
                // convert to parity code 
                string text = SerialParityCombo.Text;
                LST.Helpers.Parity.EParity tmpParity;
                if (LST.Helpers.Parity.TryParseParity(text, out tmpParity))
                    return tmpParity;

                throw new InvalidOperationException($"Invalid serial parity selection: '{text}'.");
            }
            set
            {
                string text = LST.Helpers.Parity.ParityToName(value);
                SerialParityCombo.SelectedIndex = SerialParityCombo.Items.IndexOf(text);
            }
        }

        public int ConnectionSerialEndOfMessageTimeout
        {
            get { return (int)SerialEndOfMessageTimeoutNumeric.Value; }
            set
            {
                SerialEndOfMessageTimeoutNumeric.Value = value;
            }
        }

        public bool ConnectionSerialOneWireEchoSuppression
        {
            get { return SerialOneWireEchoSuppressionCheckbox.Checked; }
            set
            {
                SerialOneWireEchoSuppressionCheckbox.Checked = value;
            }
        }

        public string ConnectionIPAddress
        {
            get { return IpAddressTextbox.Text; }
            set
            {
                IpAddressTextbox.Text = value;
            }
        }

        public UInt16 ConnectionIPPort
        {
            get { return (UInt16)IpPortNumeric.Value; }
            set
            {
                IpPortNumeric.Value = value;
            }
        }

        public Global.eTcpConnectMode ConnectionTcpConnectMode
        {
            get { return (Global.eTcpConnectMode)TcpConnectModeCombo.SelectedIndex; }
            set
            {
                TcpConnectModeCombo.SelectedIndex = (int)value;
            }
        }

        public Byte ConnectionSlaveID
        {
            get { return (Byte)SlaveAddressNumeric.Value; }
            set
            {
                SlaveAddressNumeric.Value = value;
            }
        }

        public int ConnectionMaximumFrameSize
        {
            get { return (int)MaximumFrameSizeNumeric.Value; }
            set
            {
                MaximumFrameSizeNumeric.Value = value;
            }
        }

        public int ConnectionResponseTimeout
        {
            get { return (int)ResponseTimeoutNumeric.Value; }
            set
            {
                ResponseTimeoutNumeric.Value = value;
            }
        }

        public int ConnectionInterFrameDelay
        {
            get { return (int)InterFrameDelayNumeric.Value; }
            set
            {
                InterFrameDelayNumeric.Value = value;
            }
        }

        public int ConnectionWriteHoldingregFunctionCode
        {
            // 6=write_single, 16=write_multiple, 0=auto
            get
            {
                if (WriteFC6Radiobtn.Checked) return 6;
                if (WriteFC16Radiobtn.Checked) return 16;
                return 0;
            }
            set
            {
                if (value == 6) WriteFC6Radiobtn.Checked = true;
                else if (value == 16) WriteFC16Radiobtn.Checked = true;
                else WriteFCAutoRadiobtn.Checked = true;
            }
        }

        public Global.eEndian ConnectionInt32Endian
        {
            get
            {
                if (Int32LittleEndianRadiobtn.Checked) return Global.eEndian.Little;
                return Global.eEndian.Big;
            }
            set
            {
                Int32LittleEndianRadiobtn.Checked = (value == Global.eEndian.Little);
                Int32BigEndianRadiobtn.Checked = (value == Global.eEndian.Big);
            }
        }

        public Global.eEndian ConnectionFloat32Endian
        {
            get
            {
                if (Float32LittleEndianRadiobtn.Checked) return Global.eEndian.Little;
                return Global.eEndian.Big;
            }
            set
            {
                Float32LittleEndianRadiobtn.Checked = (value == Global.eEndian.Little);
                Float32BigEndianRadiobtn.Checked = (value == Global.eEndian.Big);
            }
        }

        public Global.eEndian ConnectionStringEndian
        {
            get
            {
                if (StringLittleEndianRadiobtn.Checked) return Global.eEndian.Little;
                return Global.eEndian.Big;
            }
            set
            {
                StringLittleEndianRadiobtn.Checked = (value == Global.eEndian.Little);
                StringBigEndianRadiobtn.Checked = (value == Global.eEndian.Big);
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

        public static bool SetComboboxSelectedValueIgnoreCase(ComboBox combo, string value)
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
                string text;

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
            get { return (int)SamplerateNumeric.Value; }
            set
            {
                SamplerateNumeric.Value = value;
            }
        }

        public bool ConnectionTypeIsOverride
        {
            set
            {
                ConnectionTypeIsOverrideIcon.Visible = value;
            }
        }

        public bool PortIsOverride
        {
            set
            {
                PortIsOverrideIcon.Visible = value;
            }
        }

        public bool BaudrateIsOverride
        {
            set
            {
                BaudrateIsOverrideIcon.Visible = value;
            }
        }

        public bool ParityIsOverride
        {
            set
            {
                ParityIsOverrideIcon.Visible = value;
            }
        }

        public bool EndofmessagetimeoutIsOverride
        {
            set
            {
                EndofmessagetimeoutIsOverrideIcon.Visible = value;
            }
        }

        public bool OnewireechosuppressionIsOverride
        {
            set
            {
                OnewireechosuppressionIsOverrideIcon.Visible = value;
            }
        }

        public bool IPAddressIsOverride
        {
            set
            {
                IPAddressIsOverrideIcon.Visible = value;
            }
        }

        public bool IPPortIsOverride
        {
            set
            {
                IPPortIsOverrideIcon.Visible = value;
            }
        }

        public bool SlaveIDIsOverride
        {
            set
            {
                SlaveIDIsOverrideIcon.Visible = value;
            }
        }

        public bool MaxFrameSizeIsOverride
        {
            set
            {
                MaxFrameSizeIsOverrideIcon.Visible = value;
            }
        }

        public bool ResponsetimeoutIsOverride
        {
            set
            {
                ResponsetimeoutIsOverrideIcon.Visible = value;
            }
        }

        public bool InterframedelayIsOverride
        {
            set
            {
                InterframedelayIsOverrideIcon.Visible = value;
            }
        }

        public bool SamplerateIsOverride
        {
            set
            {
                SamplerateIsOverrideIcon.Visible = value;
            }
        }

        public bool WriteregFCIsOverride
        {
            set
            {
                WriteregFCIsOverrideIcon.Visible = value;
            }
        }


        public bool ModbusTcpBridgeEnable
        {
            get { return TcpServerEnableCheckbox.Checked; }
            set
            {
                TcpServerEnableCheckbox.Checked = value;
            }
        }

        public UInt16 ModbusTcpBridgeIPPort
        {
            get { return (UInt16)TcpServerPortNumeric.Value; }
            set
            {
                TcpServerPortNumeric.Value = value;
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
            ((TextBox)MaximumFrameSizeNumeric.Controls[1]).MaxLength = MaximumFrameSizeNumeric.Maximum.ToString().Length;
            ((TextBox)ResponseTimeoutNumeric.Controls[1]).MaxLength = ResponseTimeoutNumeric.Maximum.ToString().Length;
            ((TextBox)SlaveAddressNumeric.Controls[1]).MaxLength = SlaveAddressNumeric.Maximum.ToString().Length;
            ((TextBox)IpPortNumeric.Controls[1]).MaxLength = IpPortNumeric.Maximum.ToString().Length;
            ((TextBox)SerialEndOfMessageTimeoutNumeric.Controls[1]).MaxLength = SerialEndOfMessageTimeoutNumeric.Maximum.ToString().Length;
            ((TextBox)TcpServerPortNumeric.Controls[1]).MaxLength = TcpServerPortNumeric.Maximum.ToString().Length;
            ((TextBox)SamplerateNumeric.Controls[1]).MaxLength = SamplerateNumeric.Maximum.ToString().Length;

            // fill combobox with available serial ports
            SerialPortCombo.LoadWithFirstSerialPort();

            // load a dictionary with the supported string encodings
            // use a dictionary to have 2 values per item: key/name and displaytext
            // get the displayname of each encoding
            Dictionary<string, string> encodingDict = new();
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
            DebugInfoBtn.Visible = true;
#endif

        }

        private void ConnectionCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            SerialInterfaceGroupbox.Enabled = (ConnectionCombo.SelectedIndex == (int)Global.eConnectionType.SerialRTU);
            TcpInterfaceGroupbox.Enabled = ((ConnectionCombo.SelectedIndex == (int)Global.eConnectionType.ModbusTcp) || (ConnectionCombo.SelectedIndex == (int)Global.eConnectionType.RtuOverTcp));
        }

        private void ApplyBtn_Click(object sender, EventArgs e)
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
            WarningPanel.Visible = ConnectionTypeIsOverrideIcon.Visible
                                || PortIsOverrideIcon.Visible
                                || BaudrateIsOverrideIcon.Visible
                                || ParityIsOverrideIcon.Visible
                                || EndofmessagetimeoutIsOverrideIcon.Visible
                                || OnewireechosuppressionIsOverrideIcon.Visible
                                || IPAddressIsOverrideIcon.Visible
                                || IPPortIsOverrideIcon.Visible
                                || SlaveIDIsOverrideIcon.Visible
                                || MaxFrameSizeIsOverrideIcon.Visible
                                || ResponsetimeoutIsOverrideIcon.Visible
                                || InterframedelayIsOverrideIcon.Visible
                                || SamplerateIsOverrideIcon.Visible
                                || WriteregFCIsOverrideIcon.Visible;

            AdjustFormLayout();
        }

        private void DebugInfoBtn_Click(object sender, EventArgs e)
        {
            Debug.Print("Debug: Print info: ");
            Debug.Print($"buttonPanel.Location.Y={panel2.Location.Y}, buttonPanel.Height={panel2.Height}");
            Debug.Print($"form.Height={Height}");
        }
              

        private void AdjustFormLayout()
        {
            Debug.Print("AdjustFormLayout:");
            Debug.Print($"DeviceDpi={DeviceDpi}");

            // get margins, DPI adjusted 
            //int hmargin = AppDpiHelpers.ScaleValueFrom96DpiToThisDpiInt(6, DeviceDpi);
            int vmargin = AppDpiHelpers.ScaleValueFrom96DpiToThisDpiInt(6, DeviceDpi);
            // get new form size
            int newWidth = ClientSize.Width;
            int newHeight = panel2.Location.Y + panel2.Height + vmargin;
            Debug.Print($"newWidth={newWidth}, newHeight={newHeight}");
            ClientSize = new Size(newWidth, newHeight);
        }
    }
}
