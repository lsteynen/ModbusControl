namespace ModbusControl
{
    partial class ProjectConfigurationForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProjectConfigurationForm));
            toolTip1 = new ToolTip(components);
            SamplerateNumeric = new NumericUpDown();
            Float32LittleEndianRadiobtn = new RadioButton();
            Float32BigEndianRadiobtn = new RadioButton();
            StringLittleEndianRadiobtn = new RadioButton();
            StringBigEndianRadiobtn = new RadioButton();
            TcpServerPortNumeric = new NumericUpDown();
            TcpServerEnableCheckbox = new CheckBox();
            Int32LittleEndianRadiobtn = new RadioButton();
            Int32BigEndianRadiobtn = new RadioButton();
            WriteFC6Radiobtn = new RadioButton();
            WriteFC16Radiobtn = new RadioButton();
            WriteFCAutoRadiobtn = new RadioButton();
            MaximumFrameSizeNumeric = new NumericUpDown();
            ResponseTimeoutNumeric = new NumericUpDown();
            SlaveAddressNumeric = new NumericUpDown();
            IpPortNumeric = new NumericUpDown();
            TcpConnectModeCombo = new ComboBox();
            IpAddressTextbox = new TextBox();
            SerialEndOfMessageTimeoutNumeric = new NumericUpDown();
            SerialOneWireEchoSuppressionCheckbox = new CheckBox();
            SerialParityCombo = new ComboBox();
            SerialBaudrateCombo = new ComboBox();
            ConnectionCombo = new ComboBox();
            textboxHoldingRegistersStart = new TextBox();
            ConnectionTypeIsOverrideIcon = new PictureBox();
            PortIsOverrideIcon = new PictureBox();
            BaudrateIsOverrideIcon = new PictureBox();
            ParityIsOverrideIcon = new PictureBox();
            EndofmessagetimeoutIsOverrideIcon = new PictureBox();
            IPAddressIsOverrideIcon = new PictureBox();
            IPPortIsOverrideIcon = new PictureBox();
            OnewireechosuppressionIsOverrideIcon = new PictureBox();
            SlaveIDIsOverrideIcon = new PictureBox();
            ResponsetimeoutIsOverrideIcon = new PictureBox();
            InterframedelayIsOverrideIcon = new PictureBox();
            SamplerateIsOverrideIcon = new PictureBox();
            MaxFrameSizeIsOverrideIcon = new PictureBox();
            WriteregFCIsOverrideIcon = new PictureBox();
            InterFrameDelayNumeric = new LST.Controls.NumericUpDown2();
            SerialPortCombo = new SerialPortCombobox();
            label5 = new Label();
            backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            panel2 = new Panel();
            DebugInfoBtn = new Button();
            label17 = new Label();
            ApplyBtn = new Button();
            comboStringEncoding = new ComboBox();
            CancelBtn = new Button();
            OKBtn = new Button();
            label6 = new Label();
            groupBox3 = new GroupBox();
            groupBox4 = new GroupBox();
            label14 = new Label();
            groupboxModbusTcpServer = new GroupBox();
            label13 = new Label();
            groupBox2 = new GroupBox();
            groupBox1 = new GroupBox();
            ConnectionGroupbox = new GroupBox();
            label15 = new Label();
            TcpInterfaceGroupbox = new GroupBox();
            label9 = new Label();
            label11 = new Label();
            label10 = new Label();
            SerialInterfaceGroupbox = new GroupBox();
            label12 = new Label();
            label1 = new Label();
            label2 = new Label();
            label4 = new Label();
            ConnectionLabel = new Label();
            label8 = new Label();
            label3 = new Label();
            label7 = new Label();
            pictureBox1 = new PictureBox();
            label16 = new Label();
            WarningPanel = new Panel();
            ((System.ComponentModel.ISupportInitialize)SamplerateNumeric).BeginInit();
            ((System.ComponentModel.ISupportInitialize)TcpServerPortNumeric).BeginInit();
            ((System.ComponentModel.ISupportInitialize)MaximumFrameSizeNumeric).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ResponseTimeoutNumeric).BeginInit();
            ((System.ComponentModel.ISupportInitialize)SlaveAddressNumeric).BeginInit();
            ((System.ComponentModel.ISupportInitialize)IpPortNumeric).BeginInit();
            ((System.ComponentModel.ISupportInitialize)SerialEndOfMessageTimeoutNumeric).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ConnectionTypeIsOverrideIcon).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PortIsOverrideIcon).BeginInit();
            ((System.ComponentModel.ISupportInitialize)BaudrateIsOverrideIcon).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ParityIsOverrideIcon).BeginInit();
            ((System.ComponentModel.ISupportInitialize)EndofmessagetimeoutIsOverrideIcon).BeginInit();
            ((System.ComponentModel.ISupportInitialize)IPAddressIsOverrideIcon).BeginInit();
            ((System.ComponentModel.ISupportInitialize)IPPortIsOverrideIcon).BeginInit();
            ((System.ComponentModel.ISupportInitialize)OnewireechosuppressionIsOverrideIcon).BeginInit();
            ((System.ComponentModel.ISupportInitialize)SlaveIDIsOverrideIcon).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ResponsetimeoutIsOverrideIcon).BeginInit();
            ((System.ComponentModel.ISupportInitialize)InterframedelayIsOverrideIcon).BeginInit();
            ((System.ComponentModel.ISupportInitialize)SamplerateIsOverrideIcon).BeginInit();
            ((System.ComponentModel.ISupportInitialize)MaxFrameSizeIsOverrideIcon).BeginInit();
            ((System.ComponentModel.ISupportInitialize)WriteregFCIsOverrideIcon).BeginInit();
            ((System.ComponentModel.ISupportInitialize)InterFrameDelayNumeric).BeginInit();
            panel2.SuspendLayout();
            groupBox3.SuspendLayout();
            groupBox4.SuspendLayout();
            groupboxModbusTcpServer.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox1.SuspendLayout();
            ConnectionGroupbox.SuspendLayout();
            TcpInterfaceGroupbox.SuspendLayout();
            SerialInterfaceGroupbox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            WarningPanel.SuspendLayout();
            SuspendLayout();
            // 
            // SamplerateNumeric
            // 
            SamplerateNumeric.Increment = new decimal(new int[] { 100, 0, 0, 0 });
            SamplerateNumeric.Location = new Point(610, 16);
            SamplerateNumeric.Maximum = new decimal(new int[] { int.MaxValue, 0, 0, 0 });
            SamplerateNumeric.Name = "SamplerateNumeric";
            SamplerateNumeric.Size = new Size(82, 20);
            SamplerateNumeric.TabIndex = 8;
            toolTip1.SetToolTip(SamplerateNumeric, "Sample period in milliseconds");
            SamplerateNumeric.Value = new decimal(new int[] { int.MaxValue, 0, 0, 0 });
            // 
            // Float32LittleEndianRadiobtn
            // 
            Float32LittleEndianRadiobtn.Checked = true;
            Float32LittleEndianRadiobtn.Location = new Point(6, 16);
            Float32LittleEndianRadiobtn.Name = "Float32LittleEndianRadiobtn";
            Float32LittleEndianRadiobtn.Size = new Size(80, 17);
            Float32LittleEndianRadiobtn.TabIndex = 0;
            Float32LittleEndianRadiobtn.TabStop = true;
            Float32LittleEndianRadiobtn.Text = "Low-High";
            toolTip1.SetToolTip(Float32LittleEndianRadiobtn, "First register is lower 16 bits of 32-bit float (most used)");
            Float32LittleEndianRadiobtn.UseVisualStyleBackColor = true;
            // 
            // Float32BigEndianRadiobtn
            // 
            Float32BigEndianRadiobtn.Location = new Point(92, 16);
            Float32BigEndianRadiobtn.Name = "Float32BigEndianRadiobtn";
            Float32BigEndianRadiobtn.Size = new Size(78, 17);
            Float32BigEndianRadiobtn.TabIndex = 1;
            Float32BigEndianRadiobtn.Text = "High-Low";
            toolTip1.SetToolTip(Float32BigEndianRadiobtn, "First register is higher 16 bits of 32-bit float");
            Float32BigEndianRadiobtn.UseVisualStyleBackColor = true;
            // 
            // StringLittleEndianRadiobtn
            // 
            StringLittleEndianRadiobtn.Location = new Point(6, 33);
            StringLittleEndianRadiobtn.Name = "StringLittleEndianRadiobtn";
            StringLittleEndianRadiobtn.Size = new Size(79, 17);
            StringLittleEndianRadiobtn.TabIndex = 1;
            StringLittleEndianRadiobtn.Text = "Low byte";
            toolTip1.SetToolTip(StringLittleEndianRadiobtn, "Register with value 0x4241 is shown as 'AB'");
            StringLittleEndianRadiobtn.UseVisualStyleBackColor = true;
            // 
            // StringBigEndianRadiobtn
            // 
            StringBigEndianRadiobtn.Checked = true;
            StringBigEndianRadiobtn.Location = new Point(92, 33);
            StringBigEndianRadiobtn.Name = "StringBigEndianRadiobtn";
            StringBigEndianRadiobtn.Size = new Size(79, 17);
            StringBigEndianRadiobtn.TabIndex = 2;
            StringBigEndianRadiobtn.TabStop = true;
            StringBigEndianRadiobtn.Text = "High byte";
            toolTip1.SetToolTip(StringBigEndianRadiobtn, "Register with value 0x4241 is shown as 'BA'");
            StringBigEndianRadiobtn.UseVisualStyleBackColor = true;
            // 
            // TcpServerPortNumeric
            // 
            TcpServerPortNumeric.Location = new Point(66, 37);
            TcpServerPortNumeric.Maximum = new decimal(new int[] { 65535, 0, 0, 0 });
            TcpServerPortNumeric.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            TcpServerPortNumeric.Name = "TcpServerPortNumeric";
            TcpServerPortNumeric.Size = new Size(63, 20);
            TcpServerPortNumeric.TabIndex = 2;
            toolTip1.SetToolTip(TcpServerPortNumeric, "TCP port number where server listens on (default 502)");
            TcpServerPortNumeric.Value = new decimal(new int[] { 502, 0, 0, 0 });
            // 
            // TcpServerEnableCheckbox
            // 
            TcpServerEnableCheckbox.CheckAlign = ContentAlignment.MiddleRight;
            TcpServerEnableCheckbox.Location = new Point(7, 17);
            TcpServerEnableCheckbox.Name = "TcpServerEnableCheckbox";
            TcpServerEnableCheckbox.Size = new Size(73, 17);
            TcpServerEnableCheckbox.TabIndex = 0;
            TcpServerEnableCheckbox.Text = "Enable:";
            toolTip1.SetToolTip(TcpServerEnableCheckbox, "Enable the Modbus/TCP bridge");
            TcpServerEnableCheckbox.UseVisualStyleBackColor = true;
            // 
            // Int32LittleEndianRadiobtn
            // 
            Int32LittleEndianRadiobtn.Checked = true;
            Int32LittleEndianRadiobtn.Location = new Point(6, 15);
            Int32LittleEndianRadiobtn.Name = "Int32LittleEndianRadiobtn";
            Int32LittleEndianRadiobtn.Size = new Size(80, 17);
            Int32LittleEndianRadiobtn.TabIndex = 0;
            Int32LittleEndianRadiobtn.TabStop = true;
            Int32LittleEndianRadiobtn.Text = "Low-High";
            toolTip1.SetToolTip(Int32LittleEndianRadiobtn, "First register is lower 16 bits of 32-bit value (most used)");
            Int32LittleEndianRadiobtn.UseVisualStyleBackColor = true;
            // 
            // Int32BigEndianRadiobtn
            // 
            Int32BigEndianRadiobtn.Location = new Point(92, 16);
            Int32BigEndianRadiobtn.Name = "Int32BigEndianRadiobtn";
            Int32BigEndianRadiobtn.Size = new Size(78, 17);
            Int32BigEndianRadiobtn.TabIndex = 1;
            Int32BigEndianRadiobtn.Text = "High-Low";
            toolTip1.SetToolTip(Int32BigEndianRadiobtn, "First register is higher 16 bits of 32-bit value");
            Int32BigEndianRadiobtn.UseVisualStyleBackColor = true;
            // 
            // WriteFC6Radiobtn
            // 
            WriteFC6Radiobtn.Location = new Point(6, 16);
            WriteFC6Radiobtn.Name = "WriteFC6Radiobtn";
            WriteFC6Radiobtn.Size = new Size(191, 17);
            WriteFC6Radiobtn.TabIndex = 0;
            WriteFC6Radiobtn.TabStop = true;
            WriteFC6Radiobtn.Text = "Use FC6 only (write single reg)";
            toolTip1.SetToolTip(WriteFC6Radiobtn, "Use only function code 6 'write single register'");
            WriteFC6Radiobtn.UseVisualStyleBackColor = true;
            // 
            // WriteFC16Radiobtn
            // 
            WriteFC16Radiobtn.Checked = true;
            WriteFC16Radiobtn.Location = new Point(6, 36);
            WriteFC16Radiobtn.Name = "WriteFC16Radiobtn";
            WriteFC16Radiobtn.Size = new Size(202, 17);
            WriteFC16Radiobtn.TabIndex = 1;
            WriteFC16Radiobtn.TabStop = true;
            WriteFC16Radiobtn.Text = "Use FC16 only (write multiple reg)";
            toolTip1.SetToolTip(WriteFC16Radiobtn, "Use only function code 16 'write multiple registers'");
            WriteFC16Radiobtn.UseVisualStyleBackColor = true;
            // 
            // WriteFCAutoRadiobtn
            // 
            WriteFCAutoRadiobtn.Location = new Point(6, 56);
            WriteFCAutoRadiobtn.Name = "WriteFCAutoRadiobtn";
            WriteFCAutoRadiobtn.Size = new Size(191, 17);
            WriteFCAutoRadiobtn.TabIndex = 2;
            WriteFCAutoRadiobtn.TabStop = true;
            WriteFCAutoRadiobtn.Tag = "";
            WriteFCAutoRadiobtn.Text = "Auto select FC6 or FC16";
            toolTip1.SetToolTip(WriteFCAutoRadiobtn, "Automatically use FC6 for 16-bit registers or FC16 for 32-bit registers");
            WriteFCAutoRadiobtn.UseVisualStyleBackColor = true;
            // 
            // MaximumFrameSizeNumeric
            // 
            MaximumFrameSizeNumeric.Location = new Point(136, 324);
            MaximumFrameSizeNumeric.Maximum = new decimal(new int[] { 256, 0, 0, 0 });
            MaximumFrameSizeNumeric.Minimum = new decimal(new int[] { 13, 0, 0, 0 });
            MaximumFrameSizeNumeric.Name = "MaximumFrameSizeNumeric";
            MaximumFrameSizeNumeric.Size = new Size(72, 20);
            MaximumFrameSizeNumeric.TabIndex = 9;
            toolTip1.SetToolTip(MaximumFrameSizeNumeric, "Maximum number of bytes of query or response frame in RTU format ");
            MaximumFrameSizeNumeric.Value = new decimal(new int[] { 256, 0, 0, 0 });
            // 
            // ResponseTimeoutNumeric
            // 
            ResponseTimeoutNumeric.Increment = new decimal(new int[] { 100, 0, 0, 0 });
            ResponseTimeoutNumeric.Location = new Point(135, 301);
            ResponseTimeoutNumeric.Maximum = new decimal(new int[] { int.MaxValue, 0, 0, 0 });
            ResponseTimeoutNumeric.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            ResponseTimeoutNumeric.Name = "ResponseTimeoutNumeric";
            ResponseTimeoutNumeric.Size = new Size(72, 20);
            ResponseTimeoutNumeric.TabIndex = 7;
            toolTip1.SetToolTip(ResponseTimeoutNumeric, "Response timeout, in milliseconds");
            ResponseTimeoutNumeric.Value = new decimal(new int[] { 1000, 0, 0, 0 });
            // 
            // SlaveAddressNumeric
            // 
            SlaveAddressNumeric.Location = new Point(136, 278);
            SlaveAddressNumeric.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
            SlaveAddressNumeric.Name = "SlaveAddressNumeric";
            SlaveAddressNumeric.Size = new Size(72, 20);
            SlaveAddressNumeric.TabIndex = 5;
            toolTip1.SetToolTip(SlaveAddressNumeric, "Slave ID (or slave address or unit identifier) 0-255");
            SlaveAddressNumeric.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // IpPortNumeric
            // 
            IpPortNumeric.Location = new Point(80, 42);
            IpPortNumeric.Maximum = new decimal(new int[] { 65535, 0, 0, 0 });
            IpPortNumeric.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            IpPortNumeric.Name = "IpPortNumeric";
            IpPortNumeric.Size = new Size(102, 20);
            IpPortNumeric.TabIndex = 3;
            toolTip1.SetToolTip(IpPortNumeric, "TCP port number, default 502");
            IpPortNumeric.Value = new decimal(new int[] { 502, 0, 0, 0 });
            // 
            // TcpConnectModeCombo
            // 
            TcpConnectModeCombo.DropDownStyle = ComboBoxStyle.DropDownList;
            TcpConnectModeCombo.Enabled = false;
            TcpConnectModeCombo.FormattingEnabled = true;
            TcpConnectModeCombo.Items.AddRange(new object[] { "Long-lived connection", "Short-lived connections" });
            TcpConnectModeCombo.Location = new Point(57, 65);
            TcpConnectModeCombo.Name = "TcpConnectModeCombo";
            TcpConnectModeCombo.Size = new Size(125, 21);
            TcpConnectModeCombo.TabIndex = 5;
            toolTip1.SetToolTip(TcpConnectModeCombo, "Type of TCP connection:");
            TcpConnectModeCombo.Visible = false;
            // 
            // IpAddressTextbox
            // 
            IpAddressTextbox.Location = new Point(80, 18);
            IpAddressTextbox.MaxLength = 15;
            IpAddressTextbox.Name = "IpAddressTextbox";
            IpAddressTextbox.Size = new Size(102, 20);
            IpAddressTextbox.TabIndex = 1;
            toolTip1.SetToolTip(IpAddressTextbox, "Target IP address");
            // 
            // SerialEndOfMessageTimeoutNumeric
            // 
            SerialEndOfMessageTimeoutNumeric.Increment = new decimal(new int[] { 10, 0, 0, 0 });
            SerialEndOfMessageTimeoutNumeric.Location = new Point(114, 85);
            SerialEndOfMessageTimeoutNumeric.Maximum = new decimal(new int[] { int.MaxValue, 0, 0, 0 });
            SerialEndOfMessageTimeoutNumeric.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            SerialEndOfMessageTimeoutNumeric.Name = "SerialEndOfMessageTimeoutNumeric";
            SerialEndOfMessageTimeoutNumeric.Size = new Size(68, 20);
            SerialEndOfMessageTimeoutNumeric.TabIndex = 7;
            toolTip1.SetToolTip(SerialEndOfMessageTimeoutNumeric, "Response end-of-message timeout, or character timeout, in milliseconds");
            SerialEndOfMessageTimeoutNumeric.Value = new decimal(new int[] { 100, 0, 0, 0 });
            // 
            // SerialOneWireEchoSuppressionCheckbox
            // 
            SerialOneWireEchoSuppressionCheckbox.CheckAlign = ContentAlignment.MiddleRight;
            SerialOneWireEchoSuppressionCheckbox.Location = new Point(12, 111);
            SerialOneWireEchoSuppressionCheckbox.Name = "SerialOneWireEchoSuppressionCheckbox";
            SerialOneWireEchoSuppressionCheckbox.Size = new Size(170, 17);
            SerialOneWireEchoSuppressionCheckbox.TabIndex = 8;
            SerialOneWireEchoSuppressionCheckbox.Text = "One-wire echo suppression:";
            toolTip1.SetToolTip(SerialOneWireEchoSuppressionCheckbox, "Suppress the echo of the command on 1-wire connections");
            SerialOneWireEchoSuppressionCheckbox.UseVisualStyleBackColor = true;
            // 
            // SerialParityCombo
            // 
            SerialParityCombo.DropDownStyle = ComboBoxStyle.DropDownList;
            SerialParityCombo.FormattingEnabled = true;
            SerialParityCombo.Items.AddRange(new object[] { "None", "Odd", "Even", "Mark", "Space" });
            SerialParityCombo.Location = new Point(91, 61);
            SerialParityCombo.Name = "SerialParityCombo";
            SerialParityCombo.Size = new Size(91, 21);
            SerialParityCombo.TabIndex = 5;
            toolTip1.SetToolTip(SerialParityCombo, "Parity");
            // 
            // SerialBaudrateCombo
            // 
            SerialBaudrateCombo.FormattingEnabled = true;
            SerialBaudrateCombo.Items.AddRange(new object[] { "1200", "2400", "4800", "9600", "19200", "38400", "57600", "115200" });
            SerialBaudrateCombo.Location = new Point(91, 37);
            SerialBaudrateCombo.MaxLength = 8;
            SerialBaudrateCombo.Name = "SerialBaudrateCombo";
            SerialBaudrateCombo.Size = new Size(91, 21);
            SerialBaudrateCombo.TabIndex = 3;
            toolTip1.SetToolTip(SerialBaudrateCombo, "Baudrate, bits per second");
            // 
            // ConnectionCombo
            // 
            ConnectionCombo.DropDownStyle = ComboBoxStyle.DropDownList;
            ConnectionCombo.FormattingEnabled = true;
            ConnectionCombo.Items.AddRange(new object[] { "Serial/RTU", "Modbus/TCP", "RTU over TCP" });
            ConnectionCombo.Location = new Point(80, 18);
            ConnectionCombo.Name = "ConnectionCombo";
            ConnectionCombo.Size = new Size(103, 21);
            ConnectionCombo.TabIndex = 1;
            toolTip1.SetToolTip(ConnectionCombo, "Select connection and protocol");
            ConnectionCombo.SelectedIndexChanged += ConnectionCombo_SelectedIndexChanged;
            // 
            // textboxHoldingRegistersStart
            // 
            textboxHoldingRegistersStart.Location = new Point(511, 58);
            textboxHoldingRegistersStart.Name = "textboxHoldingRegistersStart";
            textboxHoldingRegistersStart.ReadOnly = true;
            textboxHoldingRegistersStart.Size = new Size(181, 20);
            textboxHoldingRegistersStart.TabIndex = 10;
            textboxHoldingRegistersStart.Text = "40001-49999 + 400001-465536";
            textboxHoldingRegistersStart.TextAlign = HorizontalAlignment.Right;
            toolTip1.SetToolTip(textboxHoldingRegistersStart, "Register numbering scheme, as defined by <holdingregisterstart>in the mbc project file.\r\n");
            // 
            // ConnectionTypeIsOverrideIcon
            // 
            ConnectionTypeIsOverrideIcon.Image = (Image)resources.GetObject("ConnectionTypeIsOverrideIcon.Image");
            ConnectionTypeIsOverrideIcon.Location = new Point(186, 18);
            ConnectionTypeIsOverrideIcon.Name = "ConnectionTypeIsOverrideIcon";
            ConnectionTypeIsOverrideIcon.Size = new Size(20, 20);
            ConnectionTypeIsOverrideIcon.SizeMode = PictureBoxSizeMode.StretchImage;
            ConnectionTypeIsOverrideIcon.TabIndex = 12;
            ConnectionTypeIsOverrideIcon.TabStop = false;
            toolTip1.SetToolTip(ConnectionTypeIsOverrideIcon, "This parameter is overridden by a command-line option.\r\nThe value shown reflects the command-line setting, not the mbc project file.");
            ConnectionTypeIsOverrideIcon.Visible = false;
            // 
            // PortIsOverrideIcon
            // 
            PortIsOverrideIcon.Image = (Image)resources.GetObject("PortIsOverrideIcon.Image");
            PortIsOverrideIcon.Location = new Point(186, 13);
            PortIsOverrideIcon.Name = "PortIsOverrideIcon";
            PortIsOverrideIcon.Size = new Size(20, 20);
            PortIsOverrideIcon.SizeMode = PictureBoxSizeMode.StretchImage;
            PortIsOverrideIcon.TabIndex = 13;
            PortIsOverrideIcon.TabStop = false;
            toolTip1.SetToolTip(PortIsOverrideIcon, "This parameter is overridden by a command-line option.\r\nThe value shown reflects the command-line setting, not the mbc project file.");
            PortIsOverrideIcon.Visible = false;
            // 
            // BaudrateIsOverrideIcon
            // 
            BaudrateIsOverrideIcon.Image = (Image)resources.GetObject("BaudrateIsOverrideIcon.Image");
            BaudrateIsOverrideIcon.Location = new Point(186, 37);
            BaudrateIsOverrideIcon.Name = "BaudrateIsOverrideIcon";
            BaudrateIsOverrideIcon.Size = new Size(20, 20);
            BaudrateIsOverrideIcon.SizeMode = PictureBoxSizeMode.StretchImage;
            BaudrateIsOverrideIcon.TabIndex = 14;
            BaudrateIsOverrideIcon.TabStop = false;
            toolTip1.SetToolTip(BaudrateIsOverrideIcon, "This parameter is overridden by a command-line option.\r\nThe value shown reflects the command-line setting, not the mbc project file.");
            BaudrateIsOverrideIcon.Visible = false;
            // 
            // ParityIsOverrideIcon
            // 
            ParityIsOverrideIcon.Image = (Image)resources.GetObject("ParityIsOverrideIcon.Image");
            ParityIsOverrideIcon.Location = new Point(186, 61);
            ParityIsOverrideIcon.Name = "ParityIsOverrideIcon";
            ParityIsOverrideIcon.Size = new Size(20, 20);
            ParityIsOverrideIcon.SizeMode = PictureBoxSizeMode.StretchImage;
            ParityIsOverrideIcon.TabIndex = 15;
            ParityIsOverrideIcon.TabStop = false;
            toolTip1.SetToolTip(ParityIsOverrideIcon, "This parameter is overridden by a command-line option.\r\nThe value shown reflects the command-line setting, not the mbc project file.");
            ParityIsOverrideIcon.Visible = false;
            // 
            // EndofmessagetimeoutIsOverrideIcon
            // 
            EndofmessagetimeoutIsOverrideIcon.Image = (Image)resources.GetObject("EndofmessagetimeoutIsOverrideIcon.Image");
            EndofmessagetimeoutIsOverrideIcon.Location = new Point(186, 85);
            EndofmessagetimeoutIsOverrideIcon.Name = "EndofmessagetimeoutIsOverrideIcon";
            EndofmessagetimeoutIsOverrideIcon.Size = new Size(20, 20);
            EndofmessagetimeoutIsOverrideIcon.SizeMode = PictureBoxSizeMode.StretchImage;
            EndofmessagetimeoutIsOverrideIcon.TabIndex = 16;
            EndofmessagetimeoutIsOverrideIcon.TabStop = false;
            toolTip1.SetToolTip(EndofmessagetimeoutIsOverrideIcon, "This parameter is overridden by a command-line option.\r\nThe value shown reflects the command-line setting, not the mbc project file.");
            EndofmessagetimeoutIsOverrideIcon.Visible = false;
            // 
            // IPAddressIsOverrideIcon
            // 
            IPAddressIsOverrideIcon.Image = (Image)resources.GetObject("IPAddressIsOverrideIcon.Image");
            IPAddressIsOverrideIcon.Location = new Point(185, 18);
            IPAddressIsOverrideIcon.Name = "IPAddressIsOverrideIcon";
            IPAddressIsOverrideIcon.Size = new Size(20, 20);
            IPAddressIsOverrideIcon.SizeMode = PictureBoxSizeMode.StretchImage;
            IPAddressIsOverrideIcon.TabIndex = 17;
            IPAddressIsOverrideIcon.TabStop = false;
            toolTip1.SetToolTip(IPAddressIsOverrideIcon, "This parameter is overridden by a command-line option.\r\nThe value shown reflects the command-line setting, not the mbc project file.");
            IPAddressIsOverrideIcon.Visible = false;
            // 
            // IPPortIsOverrideIcon
            // 
            IPPortIsOverrideIcon.Image = (Image)resources.GetObject("IPPortIsOverrideIcon.Image");
            IPPortIsOverrideIcon.Location = new Point(186, 42);
            IPPortIsOverrideIcon.Name = "IPPortIsOverrideIcon";
            IPPortIsOverrideIcon.Size = new Size(20, 20);
            IPPortIsOverrideIcon.SizeMode = PictureBoxSizeMode.StretchImage;
            IPPortIsOverrideIcon.TabIndex = 18;
            IPPortIsOverrideIcon.TabStop = false;
            toolTip1.SetToolTip(IPPortIsOverrideIcon, "This parameter is overridden by a command-line option.\r\nThe value shown reflects the command-line setting, not the mbc project file.");
            IPPortIsOverrideIcon.Visible = false;
            // 
            // OnewireechosuppressionIsOverrideIcon
            // 
            OnewireechosuppressionIsOverrideIcon.Image = (Image)resources.GetObject("OnewireechosuppressionIsOverrideIcon.Image");
            OnewireechosuppressionIsOverrideIcon.Location = new Point(185, 108);
            OnewireechosuppressionIsOverrideIcon.Name = "OnewireechosuppressionIsOverrideIcon";
            OnewireechosuppressionIsOverrideIcon.Size = new Size(20, 20);
            OnewireechosuppressionIsOverrideIcon.SizeMode = PictureBoxSizeMode.StretchImage;
            OnewireechosuppressionIsOverrideIcon.TabIndex = 19;
            OnewireechosuppressionIsOverrideIcon.TabStop = false;
            toolTip1.SetToolTip(OnewireechosuppressionIsOverrideIcon, "This parameter is overridden by a command-line option.\r\nThe value shown reflects the command-line setting, not the mbc project file.");
            OnewireechosuppressionIsOverrideIcon.Visible = false;
            // 
            // SlaveIDIsOverrideIcon
            // 
            SlaveIDIsOverrideIcon.Image = (Image)resources.GetObject("SlaveIDIsOverrideIcon.Image");
            SlaveIDIsOverrideIcon.Location = new Point(212, 278);
            SlaveIDIsOverrideIcon.Name = "SlaveIDIsOverrideIcon";
            SlaveIDIsOverrideIcon.Size = new Size(20, 20);
            SlaveIDIsOverrideIcon.SizeMode = PictureBoxSizeMode.StretchImage;
            SlaveIDIsOverrideIcon.TabIndex = 20;
            SlaveIDIsOverrideIcon.TabStop = false;
            toolTip1.SetToolTip(SlaveIDIsOverrideIcon, "This parameter is overridden by a command-line option.\r\nThe value shown reflects the command-line setting, not the mbc project file.");
            SlaveIDIsOverrideIcon.Visible = false;
            // 
            // ResponsetimeoutIsOverrideIcon
            // 
            ResponsetimeoutIsOverrideIcon.Image = (Image)resources.GetObject("ResponsetimeoutIsOverrideIcon.Image");
            ResponsetimeoutIsOverrideIcon.Location = new Point(212, 301);
            ResponsetimeoutIsOverrideIcon.Name = "ResponsetimeoutIsOverrideIcon";
            ResponsetimeoutIsOverrideIcon.Size = new Size(20, 20);
            ResponsetimeoutIsOverrideIcon.SizeMode = PictureBoxSizeMode.StretchImage;
            ResponsetimeoutIsOverrideIcon.TabIndex = 21;
            ResponsetimeoutIsOverrideIcon.TabStop = false;
            toolTip1.SetToolTip(ResponsetimeoutIsOverrideIcon, "This parameter is overridden by a command-line option.\r\nThe value shown reflects the command-line setting, not the mbc project file.");
            ResponsetimeoutIsOverrideIcon.Visible = false;
            // 
            // InterframedelayIsOverrideIcon
            // 
            InterframedelayIsOverrideIcon.Image = (Image)resources.GetObject("InterframedelayIsOverrideIcon.Image");
            InterframedelayIsOverrideIcon.Location = new Point(212, 347);
            InterframedelayIsOverrideIcon.Name = "InterframedelayIsOverrideIcon";
            InterframedelayIsOverrideIcon.Size = new Size(20, 20);
            InterframedelayIsOverrideIcon.SizeMode = PictureBoxSizeMode.StretchImage;
            InterframedelayIsOverrideIcon.TabIndex = 22;
            InterframedelayIsOverrideIcon.TabStop = false;
            toolTip1.SetToolTip(InterframedelayIsOverrideIcon, "This parameter is overridden by a command-line option.\r\nThe value shown reflects the command-line setting, not the mbc project file.");
            InterframedelayIsOverrideIcon.Visible = false;
            // 
            // SamplerateIsOverrideIcon
            // 
            SamplerateIsOverrideIcon.Image = (Image)resources.GetObject("SamplerateIsOverrideIcon.Image");
            SamplerateIsOverrideIcon.Location = new Point(694, 16);
            SamplerateIsOverrideIcon.Name = "SamplerateIsOverrideIcon";
            SamplerateIsOverrideIcon.Size = new Size(20, 20);
            SamplerateIsOverrideIcon.SizeMode = PictureBoxSizeMode.StretchImage;
            SamplerateIsOverrideIcon.TabIndex = 23;
            SamplerateIsOverrideIcon.TabStop = false;
            toolTip1.SetToolTip(SamplerateIsOverrideIcon, "This parameter is overridden by a command-line option.\r\nThe value shown reflects the command-line setting, not the mbc project file.");
            SamplerateIsOverrideIcon.Visible = false;
            // 
            // MaxFrameSizeIsOverrideIcon
            // 
            MaxFrameSizeIsOverrideIcon.Image = (Image)resources.GetObject("MaxFrameSizeIsOverrideIcon.Image");
            MaxFrameSizeIsOverrideIcon.Location = new Point(212, 324);
            MaxFrameSizeIsOverrideIcon.Name = "MaxFrameSizeIsOverrideIcon";
            MaxFrameSizeIsOverrideIcon.Size = new Size(20, 20);
            MaxFrameSizeIsOverrideIcon.SizeMode = PictureBoxSizeMode.StretchImage;
            MaxFrameSizeIsOverrideIcon.TabIndex = 23;
            MaxFrameSizeIsOverrideIcon.TabStop = false;
            toolTip1.SetToolTip(MaxFrameSizeIsOverrideIcon, "This parameter is overridden by a command-line option.\r\nThe value shown reflects the command-line setting, not the mbc project file.");
            MaxFrameSizeIsOverrideIcon.Visible = false;
            // 
            // WriteregFCIsOverrideIcon
            // 
            WriteregFCIsOverrideIcon.Image = (Image)resources.GetObject("WriteregFCIsOverrideIcon.Image");
            WriteregFCIsOverrideIcon.Location = new Point(188, 16);
            WriteregFCIsOverrideIcon.Name = "WriteregFCIsOverrideIcon";
            WriteregFCIsOverrideIcon.Size = new Size(20, 20);
            WriteregFCIsOverrideIcon.SizeMode = PictureBoxSizeMode.StretchImage;
            WriteregFCIsOverrideIcon.TabIndex = 13;
            WriteregFCIsOverrideIcon.TabStop = false;
            toolTip1.SetToolTip(WriteregFCIsOverrideIcon, "This parameter is overridden by a command-line option.\r\nThe value shown reflects the command-line setting, not the mbc project file.");
            WriteregFCIsOverrideIcon.Visible = false;
            // 
            // InterFrameDelayNumeric
            // 
            InterFrameDelayNumeric.Location = new Point(137, 347);
            InterFrameDelayNumeric.Maximum = new decimal(new int[] { int.MaxValue, 0, 0, 0 });
            InterFrameDelayNumeric.Name = "InterFrameDelayNumeric";
            InterFrameDelayNumeric.Size = new Size(72, 20);
            InterFrameDelayNumeric.TabIndex = 11;
            toolTip1.SetToolTip(InterFrameDelayNumeric, "Delay between response and next query, in milliseconds, 0 is auto, auto is 4Tchar for serial channels and 0ms for TCP channels");
            // 
            // SerialPortCombo
            // 
            SerialPortCombo.FormattingEnabled = true;
            SerialPortCombo.Location = new Point(91, 13);
            SerialPortCombo.MaxLength = 30;
            SerialPortCombo.Name = "SerialPortCombo";
            SerialPortCombo.Size = new Size(91, 21);
            SerialPortCombo.TabIndex = 1;
            toolTip1.SetToolTip(SerialPortCombo, "Serial port name. Dropdown shows existing ports");
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(507, 44);
            label5.Name = "label5";
            label5.Size = new Size(141, 13);
            label5.TabIndex = 9;
            label5.Text = "Register numbering scheme:";
            // 
            // panel2
            // 
            panel2.Controls.Add(DebugInfoBtn);
            panel2.Controls.Add(label17);
            panel2.Controls.Add(ApplyBtn);
            panel2.Controls.Add(comboStringEncoding);
            panel2.Controls.Add(CancelBtn);
            panel2.Controls.Add(SamplerateIsOverrideIcon);
            panel2.Controls.Add(OKBtn);
            panel2.Controls.Add(textboxHoldingRegistersStart);
            panel2.Controls.Add(label5);
            panel2.Controls.Add(SamplerateNumeric);
            panel2.Controls.Add(label6);
            panel2.Controls.Add(groupBox3);
            panel2.Controls.Add(groupBox4);
            panel2.Controls.Add(groupboxModbusTcpServer);
            panel2.Controls.Add(groupBox2);
            panel2.Controls.Add(groupBox1);
            panel2.Controls.Add(ConnectionGroupbox);
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(0, 42);
            panel2.Name = "panel2";
            panel2.Size = new Size(735, 422);
            panel2.TabIndex = 1;
            // 
            // DebugInfoBtn
            // 
            DebugInfoBtn.Location = new Point(587, 394);
            DebugInfoBtn.Name = "DebugInfoBtn";
            DebugInfoBtn.Size = new Size(100, 23);
            DebugInfoBtn.TabIndex = 15;
            DebugInfoBtn.Text = "DEBUG info";
            DebugInfoBtn.UseVisualStyleBackColor = true;
            DebugInfoBtn.Visible = false;
            DebugInfoBtn.Click += DebugInfoBtn_Click;
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Location = new Point(268, 252);
            label17.Name = "label17";
            label17.Size = new Size(133, 13);
            label17.TabIndex = 5;
            label17.Text = "String encoding/decoding:";
            // 
            // ApplyBtn
            // 
            ApplyBtn.Location = new Point(338, 394);
            ApplyBtn.Name = "ApplyBtn";
            ApplyBtn.Size = new Size(75, 23);
            ApplyBtn.TabIndex = 13;
            ApplyBtn.Text = "Apply";
            ApplyBtn.UseVisualStyleBackColor = true;
            ApplyBtn.Click += ApplyBtn_Click;
            // 
            // comboStringEncoding
            // 
            comboStringEncoding.DropDownStyle = ComboBoxStyle.DropDownList;
            comboStringEncoding.FormattingEnabled = true;
            comboStringEncoding.Location = new Point(270, 269);
            comboStringEncoding.Name = "comboStringEncoding";
            comboStringEncoding.Size = new Size(218, 21);
            comboStringEncoding.TabIndex = 6;
            // 
            // CancelBtn
            // 
            CancelBtn.DialogResult = DialogResult.Cancel;
            CancelBtn.Location = new Point(429, 394);
            CancelBtn.Name = "CancelBtn";
            CancelBtn.Size = new Size(75, 23);
            CancelBtn.TabIndex = 14;
            CancelBtn.Text = "Cancel";
            CancelBtn.UseVisualStyleBackColor = true;
            // 
            // OKBtn
            // 
            OKBtn.DialogResult = DialogResult.OK;
            OKBtn.Location = new Point(247, 394);
            OKBtn.Name = "OKBtn";
            OKBtn.Size = new Size(75, 23);
            OKBtn.TabIndex = 12;
            OKBtn.Text = "OK";
            OKBtn.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(507, 18);
            label6.Name = "label6";
            label6.Size = new Size(88, 13);
            label6.TabIndex = 7;
            label6.Text = "Sample rate (ms):";
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(Float32BigEndianRadiobtn);
            groupBox3.Controls.Add(Float32LittleEndianRadiobtn);
            groupBox3.Location = new Point(270, 143);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(218, 39);
            groupBox3.TabIndex = 3;
            groupBox3.TabStop = false;
            groupBox3.Text = "32-bit float word order";
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(StringBigEndianRadiobtn);
            groupBox4.Controls.Add(StringLittleEndianRadiobtn);
            groupBox4.Controls.Add(label14);
            groupBox4.Location = new Point(270, 190);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(218, 55);
            groupBox4.TabIndex = 4;
            groupBox4.TabStop = false;
            groupBox4.Text = "String/ASCII byte order";
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new Point(3, 17);
            label14.Name = "label14";
            label14.Size = new Size(88, 13);
            label14.TabIndex = 0;
            label14.Text = "First character in:";
            // 
            // groupboxModbusTcpServer
            // 
            groupboxModbusTcpServer.Controls.Add(TcpServerPortNumeric);
            groupboxModbusTcpServer.Controls.Add(TcpServerEnableCheckbox);
            groupboxModbusTcpServer.Controls.Add(label13);
            groupboxModbusTcpServer.Location = new Point(511, 93);
            groupboxModbusTcpServer.Name = "groupboxModbusTcpServer";
            groupboxModbusTcpServer.Size = new Size(178, 67);
            groupboxModbusTcpServer.TabIndex = 11;
            groupboxModbusTcpServer.TabStop = false;
            groupboxModbusTcpServer.Text = "Modbus/TCP bridge server";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(8, 39);
            label13.Name = "label13";
            label13.Size = new Size(52, 13);
            label13.TabIndex = 1;
            label13.Text = "TCP port:";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(Int32BigEndianRadiobtn);
            groupBox2.Controls.Add(Int32LittleEndianRadiobtn);
            groupBox2.Location = new Point(270, 97);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(218, 38);
            groupBox2.TabIndex = 2;
            groupBox2.TabStop = false;
            groupBox2.Text = "32-bit integer word order";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(WriteregFCIsOverrideIcon);
            groupBox1.Controls.Add(WriteFCAutoRadiobtn);
            groupBox1.Controls.Add(WriteFC16Radiobtn);
            groupBox1.Controls.Add(WriteFC6Radiobtn);
            groupBox1.Location = new Point(270, 10);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(218, 79);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            groupBox1.Text = "Functioncode to write holding registers";
            // 
            // ConnectionGroupbox
            // 
            ConnectionGroupbox.Controls.Add(InterFrameDelayNumeric);
            ConnectionGroupbox.Controls.Add(MaximumFrameSizeNumeric);
            ConnectionGroupbox.Controls.Add(ResponseTimeoutNumeric);
            ConnectionGroupbox.Controls.Add(SlaveAddressNumeric);
            ConnectionGroupbox.Controls.Add(MaxFrameSizeIsOverrideIcon);
            ConnectionGroupbox.Controls.Add(InterframedelayIsOverrideIcon);
            ConnectionGroupbox.Controls.Add(ResponsetimeoutIsOverrideIcon);
            ConnectionGroupbox.Controls.Add(SlaveIDIsOverrideIcon);
            ConnectionGroupbox.Controls.Add(ConnectionTypeIsOverrideIcon);
            ConnectionGroupbox.Controls.Add(label15);
            ConnectionGroupbox.Controls.Add(TcpInterfaceGroupbox);
            ConnectionGroupbox.Controls.Add(SerialInterfaceGroupbox);
            ConnectionGroupbox.Controls.Add(ConnectionLabel);
            ConnectionGroupbox.Controls.Add(ConnectionCombo);
            ConnectionGroupbox.Controls.Add(label8);
            ConnectionGroupbox.Controls.Add(label3);
            ConnectionGroupbox.Controls.Add(label7);
            ConnectionGroupbox.Location = new Point(9, 10);
            ConnectionGroupbox.Name = "ConnectionGroupbox";
            ConnectionGroupbox.Size = new Size(237, 379);
            ConnectionGroupbox.TabIndex = 0;
            ConnectionGroupbox.TabStop = false;
            ConnectionGroupbox.Text = "Connection / Protocol";
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Location = new Point(6, 349);
            label15.Name = "label15";
            label15.Size = new Size(110, 13);
            label15.TabIndex = 10;
            label15.Text = "Inter-frame delay (ms):";
            // 
            // TcpInterfaceGroupbox
            // 
            TcpInterfaceGroupbox.Controls.Add(IPPortIsOverrideIcon);
            TcpInterfaceGroupbox.Controls.Add(label9);
            TcpInterfaceGroupbox.Controls.Add(IpPortNumeric);
            TcpInterfaceGroupbox.Controls.Add(TcpConnectModeCombo);
            TcpInterfaceGroupbox.Controls.Add(IpAddressTextbox);
            TcpInterfaceGroupbox.Controls.Add(IPAddressIsOverrideIcon);
            TcpInterfaceGroupbox.Controls.Add(label11);
            TcpInterfaceGroupbox.Controls.Add(label10);
            TcpInterfaceGroupbox.Enabled = false;
            TcpInterfaceGroupbox.Location = new Point(0, 184);
            TcpInterfaceGroupbox.Name = "TcpInterfaceGroupbox";
            TcpInterfaceGroupbox.Size = new Size(212, 73);
            TcpInterfaceGroupbox.TabIndex = 3;
            TcpInterfaceGroupbox.TabStop = false;
            TcpInterfaceGroupbox.Text = "TCP connection";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(12, 69);
            label9.Name = "label9";
            label9.Size = new Size(37, 13);
            label9.TabIndex = 4;
            label9.Text = "Mode:";
            label9.Visible = false;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(12, 44);
            label11.Name = "label11";
            label11.Size = new Size(52, 13);
            label11.TabIndex = 2;
            label11.Text = "TCP port:";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(12, 21);
            label10.Name = "label10";
            label10.Size = new Size(60, 13);
            label10.TabIndex = 0;
            label10.Text = "IP address:";
            // 
            // SerialInterfaceGroupbox
            // 
            SerialInterfaceGroupbox.Controls.Add(SerialEndOfMessageTimeoutNumeric);
            SerialInterfaceGroupbox.Controls.Add(SerialPortCombo);
            SerialInterfaceGroupbox.Controls.Add(OnewireechosuppressionIsOverrideIcon);
            SerialInterfaceGroupbox.Controls.Add(BaudrateIsOverrideIcon);
            SerialInterfaceGroupbox.Controls.Add(PortIsOverrideIcon);
            SerialInterfaceGroupbox.Controls.Add(SerialOneWireEchoSuppressionCheckbox);
            SerialInterfaceGroupbox.Controls.Add(label12);
            SerialInterfaceGroupbox.Controls.Add(label1);
            SerialInterfaceGroupbox.Controls.Add(EndofmessagetimeoutIsOverrideIcon);
            SerialInterfaceGroupbox.Controls.Add(label2);
            SerialInterfaceGroupbox.Controls.Add(label4);
            SerialInterfaceGroupbox.Controls.Add(ParityIsOverrideIcon);
            SerialInterfaceGroupbox.Controls.Add(SerialParityCombo);
            SerialInterfaceGroupbox.Controls.Add(SerialBaudrateCombo);
            SerialInterfaceGroupbox.Location = new Point(0, 45);
            SerialInterfaceGroupbox.Name = "SerialInterfaceGroupbox";
            SerialInterfaceGroupbox.Size = new Size(212, 133);
            SerialInterfaceGroupbox.TabIndex = 2;
            SerialInterfaceGroupbox.TabStop = false;
            SerialInterfaceGroupbox.Text = "Serial interface";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(12, 86);
            label12.Name = "label12";
            label12.Size = new Size(93, 13);
            label12.TabIndex = 6;
            label12.Text = "EOM timeout (ms):";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 16);
            label1.Name = "label1";
            label1.Size = new Size(29, 13);
            label1.TabIndex = 0;
            label1.Text = "Port:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 40);
            label2.Name = "label2";
            label2.Size = new Size(53, 13);
            label2.TabIndex = 2;
            label2.Text = "Baudrate:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(12, 65);
            label4.Name = "label4";
            label4.Size = new Size(36, 13);
            label4.TabIndex = 4;
            label4.Text = "Parity:";
            // 
            // ConnectionLabel
            // 
            ConnectionLabel.AutoSize = true;
            ConnectionLabel.Location = new Point(7, 21);
            ConnectionLabel.Name = "ConnectionLabel";
            ConnectionLabel.Size = new Size(64, 13);
            ConnectionLabel.TabIndex = 0;
            ConnectionLabel.Text = "Connection:";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(6, 303);
            label8.Name = "label8";
            label8.Size = new Size(117, 13);
            label8.TabIndex = 6;
            label8.Text = "Response timeout (ms):";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(6, 280);
            label3.Name = "label3";
            label3.Size = new Size(51, 13);
            label3.TabIndex = 4;
            label3.Text = "Slave ID:";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(6, 326);
            label7.Name = "label7";
            label7.Size = new Size(104, 13);
            label7.TabIndex = 8;
            label7.Text = "Maximum frame size:";
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(17, 3);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(32, 32);
            pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // label16
            // 
            label16.Location = new Point(62, 4);
            label16.Name = "label16";
            label16.Padding = new Padding(0, 1, 0, 0);
            label16.Size = new Size(537, 30);
            label16.TabIndex = 0;
            label16.Text = "Parameters marked with a warning icon are overridden by command-line options. \r\nThe displayed values come from the command line, not from the mbc project file.";
            // 
            // WarningPanel
            // 
            WarningPanel.BorderStyle = BorderStyle.FixedSingle;
            WarningPanel.Controls.Add(label16);
            WarningPanel.Controls.Add(pictureBox1);
            WarningPanel.Dock = DockStyle.Top;
            WarningPanel.Location = new Point(0, 0);
            WarningPanel.Name = "WarningPanel";
            WarningPanel.Size = new Size(735, 42);
            WarningPanel.TabIndex = 0;
            WarningPanel.Visible = false;
            // 
            // ProjectConfigurationForm
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            ClientSize = new Size(735, 468);
            Controls.Add(panel2);
            Controls.Add(WarningPanel);
            Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 177);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            MinimumSize = new Size(698, 45);
            Name = "ProjectConfigurationForm";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Project configuration";
            Shown += ConfigForm_Shown;
            ((System.ComponentModel.ISupportInitialize)SamplerateNumeric).EndInit();
            ((System.ComponentModel.ISupportInitialize)TcpServerPortNumeric).EndInit();
            ((System.ComponentModel.ISupportInitialize)MaximumFrameSizeNumeric).EndInit();
            ((System.ComponentModel.ISupportInitialize)ResponseTimeoutNumeric).EndInit();
            ((System.ComponentModel.ISupportInitialize)SlaveAddressNumeric).EndInit();
            ((System.ComponentModel.ISupportInitialize)IpPortNumeric).EndInit();
            ((System.ComponentModel.ISupportInitialize)SerialEndOfMessageTimeoutNumeric).EndInit();
            ((System.ComponentModel.ISupportInitialize)ConnectionTypeIsOverrideIcon).EndInit();
            ((System.ComponentModel.ISupportInitialize)PortIsOverrideIcon).EndInit();
            ((System.ComponentModel.ISupportInitialize)BaudrateIsOverrideIcon).EndInit();
            ((System.ComponentModel.ISupportInitialize)ParityIsOverrideIcon).EndInit();
            ((System.ComponentModel.ISupportInitialize)EndofmessagetimeoutIsOverrideIcon).EndInit();
            ((System.ComponentModel.ISupportInitialize)IPAddressIsOverrideIcon).EndInit();
            ((System.ComponentModel.ISupportInitialize)IPPortIsOverrideIcon).EndInit();
            ((System.ComponentModel.ISupportInitialize)OnewireechosuppressionIsOverrideIcon).EndInit();
            ((System.ComponentModel.ISupportInitialize)SlaveIDIsOverrideIcon).EndInit();
            ((System.ComponentModel.ISupportInitialize)ResponsetimeoutIsOverrideIcon).EndInit();
            ((System.ComponentModel.ISupportInitialize)InterframedelayIsOverrideIcon).EndInit();
            ((System.ComponentModel.ISupportInitialize)SamplerateIsOverrideIcon).EndInit();
            ((System.ComponentModel.ISupportInitialize)MaxFrameSizeIsOverrideIcon).EndInit();
            ((System.ComponentModel.ISupportInitialize)WriteregFCIsOverrideIcon).EndInit();
            ((System.ComponentModel.ISupportInitialize)InterFrameDelayNumeric).EndInit();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            groupboxModbusTcpServer.ResumeLayout(false);
            groupboxModbusTcpServer.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            ConnectionGroupbox.ResumeLayout(false);
            ConnectionGroupbox.PerformLayout();
            TcpInterfaceGroupbox.ResumeLayout(false);
            TcpInterfaceGroupbox.PerformLayout();
            SerialInterfaceGroupbox.ResumeLayout(false);
            SerialInterfaceGroupbox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            WarningPanel.ResumeLayout(false);
            WarningPanel.PerformLayout();
            ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ToolTip toolTip1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox textboxHoldingRegistersStart;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown SamplerateNumeric;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton Float32LittleEndianRadiobtn;
        private System.Windows.Forms.RadioButton Float32BigEndianRadiobtn;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.RadioButton StringLittleEndianRadiobtn;
        private System.Windows.Forms.RadioButton StringBigEndianRadiobtn;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.GroupBox groupboxModbusTcpServer;
        private System.Windows.Forms.NumericUpDown TcpServerPortNumeric;
        private System.Windows.Forms.CheckBox TcpServerEnableCheckbox;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton Int32LittleEndianRadiobtn;
        private System.Windows.Forms.RadioButton Int32BigEndianRadiobtn;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton WriteFC6Radiobtn;
        private System.Windows.Forms.RadioButton WriteFC16Radiobtn;
        private System.Windows.Forms.RadioButton WriteFCAutoRadiobtn;
        private System.Windows.Forms.GroupBox ConnectionGroupbox;
        private LST.Controls.NumericUpDown2 InterFrameDelayNumeric;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.NumericUpDown MaximumFrameSizeNumeric;
        private System.Windows.Forms.NumericUpDown ResponseTimeoutNumeric;
        private System.Windows.Forms.NumericUpDown SlaveAddressNumeric;
        private System.Windows.Forms.GroupBox TcpInterfaceGroupbox;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.NumericUpDown IpPortNumeric;
        private System.Windows.Forms.ComboBox TcpConnectModeCombo;
        private System.Windows.Forms.TextBox IpAddressTextbox;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.GroupBox SerialInterfaceGroupbox;
        private System.Windows.Forms.NumericUpDown SerialEndOfMessageTimeoutNumeric;
        private System.Windows.Forms.CheckBox SerialOneWireEchoSuppressionCheckbox;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox SerialParityCombo;
        private System.Windows.Forms.ComboBox SerialBaudrateCombo;
        private System.Windows.Forms.Label ConnectionLabel;
        private System.Windows.Forms.ComboBox ConnectionCombo;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Panel WarningPanel;
        private SerialPortCombobox SerialPortCombo;
        private System.Windows.Forms.PictureBox ConnectionTypeIsOverrideIcon;
        private System.Windows.Forms.PictureBox EndofmessagetimeoutIsOverrideIcon;
        private System.Windows.Forms.PictureBox ParityIsOverrideIcon;
        private System.Windows.Forms.PictureBox BaudrateIsOverrideIcon;
        private System.Windows.Forms.PictureBox PortIsOverrideIcon;
        private System.Windows.Forms.PictureBox IPPortIsOverrideIcon;
        private System.Windows.Forms.PictureBox IPAddressIsOverrideIcon;
        private System.Windows.Forms.PictureBox OnewireechosuppressionIsOverrideIcon;
        private System.Windows.Forms.PictureBox InterframedelayIsOverrideIcon;
        private System.Windows.Forms.PictureBox ResponsetimeoutIsOverrideIcon;
        private System.Windows.Forms.PictureBox SlaveIDIsOverrideIcon;
        private System.Windows.Forms.PictureBox SamplerateIsOverrideIcon;
        private System.Windows.Forms.PictureBox MaxFrameSizeIsOverrideIcon;
        private System.Windows.Forms.PictureBox WriteregFCIsOverrideIcon;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.ComboBox comboStringEncoding;
        private System.Windows.Forms.Button ApplyBtn;
        private System.Windows.Forms.Button CancelBtn;
        private System.Windows.Forms.Button OKBtn;
        private Button DebugInfoBtn;
    }
}