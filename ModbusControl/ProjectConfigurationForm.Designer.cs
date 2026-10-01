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
            numericSamplerate = new NumericUpDown();
            radFloat32LittleEndian = new RadioButton();
            radFloat32BigEndian = new RadioButton();
            radStringLittleEndian = new RadioButton();
            radStringBigEndian = new RadioButton();
            numericTcpServerPort = new NumericUpDown();
            checkboxTcpServerEnable = new CheckBox();
            radInt32LittleEndian = new RadioButton();
            radInt32BigEndian = new RadioButton();
            radWriteFC6 = new RadioButton();
            radWriteFC16 = new RadioButton();
            radWriteFCAuto = new RadioButton();
            numericMaximumFrameSize = new NumericUpDown();
            numericResponseTimeout = new NumericUpDown();
            numericSlaveAddress = new NumericUpDown();
            numericIpPort = new NumericUpDown();
            comboTcpConnectMode = new ComboBox();
            textboxIpAddress = new TextBox();
            numericSerialEndOfMessageTimeout = new NumericUpDown();
            checkboxSerialOneWireEchoSuppression = new CheckBox();
            comboSerialParity = new ComboBox();
            comboSerialBaudrate = new ComboBox();
            comboConnection = new ComboBox();
            textboxHoldingRegistersStart = new TextBox();
            picConnectionTypeIsOverride = new PictureBox();
            picPortIsOverride = new PictureBox();
            picBaudrateIsOverride = new PictureBox();
            picParityIsOverride = new PictureBox();
            picEndofmessagetimeoutIsOverride = new PictureBox();
            picIPAddressIsOverride = new PictureBox();
            picIPPortIsOverride = new PictureBox();
            picOnewireechosuppressionIsOverride = new PictureBox();
            picSlaveIDIsOverride = new PictureBox();
            picResponsetimeoutIsOverride = new PictureBox();
            picInterframedelayIsOverride = new PictureBox();
            picSamplerateIsOverride = new PictureBox();
            picMaxFrameSizeIsOverride = new PictureBox();
            picwriteregFCIsOverride = new PictureBox();
            numericInterFrameDelay = new LST.Controls.NumericUpDown2();
            comboSerialPort = new SerialPortCombobox();
            label5 = new Label();
            backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            panel2 = new Panel();
            butDebugInfo = new Button();
            label17 = new Label();
            buttonApply = new Button();
            comboStringEncoding = new ComboBox();
            buttonCancel = new Button();
            buttonOK = new Button();
            label6 = new Label();
            groupBox3 = new GroupBox();
            groupBox4 = new GroupBox();
            label14 = new Label();
            groupboxModbusTcpServer = new GroupBox();
            label13 = new Label();
            groupBox2 = new GroupBox();
            groupBox1 = new GroupBox();
            groupboxConnection = new GroupBox();
            label15 = new Label();
            groupBoxTcpInterface = new GroupBox();
            label9 = new Label();
            label11 = new Label();
            label10 = new Label();
            groupBoxSerialInterface = new GroupBox();
            label12 = new Label();
            label1 = new Label();
            label2 = new Label();
            label4 = new Label();
            labelConnection = new Label();
            label8 = new Label();
            label3 = new Label();
            label7 = new Label();
            pictureBox1 = new PictureBox();
            label16 = new Label();
            warningPanel = new Panel();
            ((System.ComponentModel.ISupportInitialize)numericSamplerate).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericTcpServerPort).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericMaximumFrameSize).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericResponseTimeout).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericSlaveAddress).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericIpPort).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericSerialEndOfMessageTimeout).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picConnectionTypeIsOverride).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picPortIsOverride).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picBaudrateIsOverride).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picParityIsOverride).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picEndofmessagetimeoutIsOverride).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picIPAddressIsOverride).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picIPPortIsOverride).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picOnewireechosuppressionIsOverride).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picSlaveIDIsOverride).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picResponsetimeoutIsOverride).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picInterframedelayIsOverride).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picSamplerateIsOverride).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picMaxFrameSizeIsOverride).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picwriteregFCIsOverride).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericInterFrameDelay).BeginInit();
            panel2.SuspendLayout();
            groupBox3.SuspendLayout();
            groupBox4.SuspendLayout();
            groupboxModbusTcpServer.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox1.SuspendLayout();
            groupboxConnection.SuspendLayout();
            groupBoxTcpInterface.SuspendLayout();
            groupBoxSerialInterface.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            warningPanel.SuspendLayout();
            SuspendLayout();
            // 
            // numericSamplerate
            // 
            numericSamplerate.Increment = new decimal(new int[] { 100, 0, 0, 0 });
            numericSamplerate.Location = new Point(610, 16);
            numericSamplerate.Maximum = new decimal(new int[] { int.MaxValue, 0, 0, 0 });
            numericSamplerate.Name = "numericSamplerate";
            numericSamplerate.Size = new Size(82, 20);
            numericSamplerate.TabIndex = 8;
            toolTip1.SetToolTip(numericSamplerate, "Sample period in milliseconds");
            numericSamplerate.Value = new decimal(new int[] { int.MaxValue, 0, 0, 0 });
            // 
            // radFloat32LittleEndian
            // 
            radFloat32LittleEndian.Checked = true;
            radFloat32LittleEndian.Location = new Point(6, 16);
            radFloat32LittleEndian.Name = "radFloat32LittleEndian";
            radFloat32LittleEndian.Size = new Size(80, 17);
            radFloat32LittleEndian.TabIndex = 0;
            radFloat32LittleEndian.TabStop = true;
            radFloat32LittleEndian.Text = "Low-High";
            toolTip1.SetToolTip(radFloat32LittleEndian, "First register is lower 16 bits of 32-bit float (most used)");
            radFloat32LittleEndian.UseVisualStyleBackColor = true;
            // 
            // radFloat32BigEndian
            // 
            radFloat32BigEndian.Location = new Point(92, 16);
            radFloat32BigEndian.Name = "radFloat32BigEndian";
            radFloat32BigEndian.Size = new Size(78, 17);
            radFloat32BigEndian.TabIndex = 1;
            radFloat32BigEndian.Text = "High-Low";
            toolTip1.SetToolTip(radFloat32BigEndian, "First register is higher 16 bits of 32-bit float");
            radFloat32BigEndian.UseVisualStyleBackColor = true;
            // 
            // radStringLittleEndian
            // 
            radStringLittleEndian.Location = new Point(6, 33);
            radStringLittleEndian.Name = "radStringLittleEndian";
            radStringLittleEndian.Size = new Size(79, 17);
            radStringLittleEndian.TabIndex = 1;
            radStringLittleEndian.Text = "Low byte";
            toolTip1.SetToolTip(radStringLittleEndian, "Register with value 0x4241 is shown as 'AB'");
            radStringLittleEndian.UseVisualStyleBackColor = true;
            // 
            // radStringBigEndian
            // 
            radStringBigEndian.Checked = true;
            radStringBigEndian.Location = new Point(91, 33);
            radStringBigEndian.Name = "radStringBigEndian";
            radStringBigEndian.Size = new Size(79, 17);
            radStringBigEndian.TabIndex = 2;
            radStringBigEndian.TabStop = true;
            radStringBigEndian.Text = "High byte";
            toolTip1.SetToolTip(radStringBigEndian, "Register with value 0x4241 is shown as 'BA'");
            radStringBigEndian.UseVisualStyleBackColor = true;
            // 
            // numericTcpServerPort
            // 
            numericTcpServerPort.Location = new Point(66, 37);
            numericTcpServerPort.Maximum = new decimal(new int[] { 65535, 0, 0, 0 });
            numericTcpServerPort.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numericTcpServerPort.Name = "numericTcpServerPort";
            numericTcpServerPort.Size = new Size(63, 20);
            numericTcpServerPort.TabIndex = 2;
            toolTip1.SetToolTip(numericTcpServerPort, "TCP port number where server listens on (default 502)");
            numericTcpServerPort.Value = new decimal(new int[] { 502, 0, 0, 0 });
            // 
            // checkboxTcpServerEnable
            // 
            checkboxTcpServerEnable.CheckAlign = ContentAlignment.MiddleRight;
            checkboxTcpServerEnable.Location = new Point(7, 17);
            checkboxTcpServerEnable.Name = "checkboxTcpServerEnable";
            checkboxTcpServerEnable.Size = new Size(73, 17);
            checkboxTcpServerEnable.TabIndex = 0;
            checkboxTcpServerEnable.Text = "Enable:";
            toolTip1.SetToolTip(checkboxTcpServerEnable, "Enable the Modbus/TCP bridge");
            checkboxTcpServerEnable.UseVisualStyleBackColor = true;
            // 
            // radInt32LittleEndian
            // 
            radInt32LittleEndian.Checked = true;
            radInt32LittleEndian.Location = new Point(6, 15);
            radInt32LittleEndian.Name = "radInt32LittleEndian";
            radInt32LittleEndian.Size = new Size(80, 17);
            radInt32LittleEndian.TabIndex = 0;
            radInt32LittleEndian.TabStop = true;
            radInt32LittleEndian.Text = "Low-High";
            toolTip1.SetToolTip(radInt32LittleEndian, "First register is lower 16 bits of 32-bit value (most used)");
            radInt32LittleEndian.UseVisualStyleBackColor = true;
            // 
            // radInt32BigEndian
            // 
            radInt32BigEndian.Location = new Point(92, 16);
            radInt32BigEndian.Name = "radInt32BigEndian";
            radInt32BigEndian.Size = new Size(78, 17);
            radInt32BigEndian.TabIndex = 1;
            radInt32BigEndian.Text = "High-Low";
            toolTip1.SetToolTip(radInt32BigEndian, "First register is higher 16 bits of 32-bit value");
            radInt32BigEndian.UseVisualStyleBackColor = true;
            // 
            // radWriteFC6
            // 
            radWriteFC6.Location = new Point(6, 16);
            radWriteFC6.Name = "radWriteFC6";
            radWriteFC6.Size = new Size(191, 17);
            radWriteFC6.TabIndex = 0;
            radWriteFC6.TabStop = true;
            radWriteFC6.Text = "Use FC6 only (write single reg)";
            toolTip1.SetToolTip(radWriteFC6, "Use only function code 6 'write single register'");
            radWriteFC6.UseVisualStyleBackColor = true;
            // 
            // radWriteFC16
            // 
            radWriteFC16.Checked = true;
            radWriteFC16.Location = new Point(6, 37);
            radWriteFC16.Name = "radWriteFC16";
            radWriteFC16.Size = new Size(202, 17);
            radWriteFC16.TabIndex = 1;
            radWriteFC16.TabStop = true;
            radWriteFC16.Text = "Use FC16 only (write multiple reg)";
            toolTip1.SetToolTip(radWriteFC16, "Use only function code 16 'write multiple registers'");
            radWriteFC16.UseVisualStyleBackColor = true;
            // 
            // radWriteFCAuto
            // 
            radWriteFCAuto.Location = new Point(6, 58);
            radWriteFCAuto.Name = "radWriteFCAuto";
            radWriteFCAuto.Size = new Size(191, 17);
            radWriteFCAuto.TabIndex = 2;
            radWriteFCAuto.TabStop = true;
            radWriteFCAuto.Tag = "";
            radWriteFCAuto.Text = "Auto select FC6 or FC16";
            toolTip1.SetToolTip(radWriteFCAuto, "Automatically use FC6 for 16-bit registers or FC16 for 32-bit registers");
            radWriteFCAuto.UseVisualStyleBackColor = true;
            // 
            // numericMaximumFrameSize
            // 
            numericMaximumFrameSize.Location = new Point(136, 324);
            numericMaximumFrameSize.Maximum = new decimal(new int[] { 256, 0, 0, 0 });
            numericMaximumFrameSize.Minimum = new decimal(new int[] { 13, 0, 0, 0 });
            numericMaximumFrameSize.Name = "numericMaximumFrameSize";
            numericMaximumFrameSize.Size = new Size(72, 20);
            numericMaximumFrameSize.TabIndex = 9;
            toolTip1.SetToolTip(numericMaximumFrameSize, "Maximum number of bytes of query or response frame in RTU format ");
            numericMaximumFrameSize.Value = new decimal(new int[] { 256, 0, 0, 0 });
            // 
            // numericResponseTimeout
            // 
            numericResponseTimeout.Increment = new decimal(new int[] { 100, 0, 0, 0 });
            numericResponseTimeout.Location = new Point(135, 301);
            numericResponseTimeout.Maximum = new decimal(new int[] { int.MaxValue, 0, 0, 0 });
            numericResponseTimeout.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numericResponseTimeout.Name = "numericResponseTimeout";
            numericResponseTimeout.Size = new Size(72, 20);
            numericResponseTimeout.TabIndex = 7;
            toolTip1.SetToolTip(numericResponseTimeout, "Response timeout, in milliseconds");
            numericResponseTimeout.Value = new decimal(new int[] { 1000, 0, 0, 0 });
            // 
            // numericSlaveAddress
            // 
            numericSlaveAddress.Location = new Point(136, 278);
            numericSlaveAddress.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
            numericSlaveAddress.Name = "numericSlaveAddress";
            numericSlaveAddress.Size = new Size(72, 20);
            numericSlaveAddress.TabIndex = 5;
            toolTip1.SetToolTip(numericSlaveAddress, "Slave ID (or slave address or unit identifier) 0-255");
            numericSlaveAddress.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // numericIpPort
            // 
            numericIpPort.Location = new Point(80, 42);
            numericIpPort.Maximum = new decimal(new int[] { 65535, 0, 0, 0 });
            numericIpPort.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numericIpPort.Name = "numericIpPort";
            numericIpPort.Size = new Size(102, 20);
            numericIpPort.TabIndex = 3;
            toolTip1.SetToolTip(numericIpPort, "TCP port number, default 502");
            numericIpPort.Value = new decimal(new int[] { 502, 0, 0, 0 });
            // 
            // comboTcpConnectMode
            // 
            comboTcpConnectMode.DropDownStyle = ComboBoxStyle.DropDownList;
            comboTcpConnectMode.Enabled = false;
            comboTcpConnectMode.FormattingEnabled = true;
            comboTcpConnectMode.Items.AddRange(new object[] { "Long-lived connection", "Short-lived connections" });
            comboTcpConnectMode.Location = new Point(57, 65);
            comboTcpConnectMode.Name = "comboTcpConnectMode";
            comboTcpConnectMode.Size = new Size(125, 21);
            comboTcpConnectMode.TabIndex = 5;
            toolTip1.SetToolTip(comboTcpConnectMode, "Type of TCP connection:");
            comboTcpConnectMode.Visible = false;
            // 
            // textboxIpAddress
            // 
            textboxIpAddress.Location = new Point(80, 18);
            textboxIpAddress.MaxLength = 15;
            textboxIpAddress.Name = "textboxIpAddress";
            textboxIpAddress.Size = new Size(102, 20);
            textboxIpAddress.TabIndex = 1;
            toolTip1.SetToolTip(textboxIpAddress, "Target IP address");
            // 
            // numericSerialEndOfMessageTimeout
            // 
            numericSerialEndOfMessageTimeout.Increment = new decimal(new int[] { 10, 0, 0, 0 });
            numericSerialEndOfMessageTimeout.Location = new Point(114, 85);
            numericSerialEndOfMessageTimeout.Maximum = new decimal(new int[] { int.MaxValue, 0, 0, 0 });
            numericSerialEndOfMessageTimeout.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numericSerialEndOfMessageTimeout.Name = "numericSerialEndOfMessageTimeout";
            numericSerialEndOfMessageTimeout.Size = new Size(68, 20);
            numericSerialEndOfMessageTimeout.TabIndex = 7;
            toolTip1.SetToolTip(numericSerialEndOfMessageTimeout, "Response end-of-message timeout, or character timeout, in milliseconds");
            numericSerialEndOfMessageTimeout.Value = new decimal(new int[] { 100, 0, 0, 0 });
            // 
            // checkboxSerialOneWireEchoSuppression
            // 
            checkboxSerialOneWireEchoSuppression.CheckAlign = ContentAlignment.MiddleRight;
            checkboxSerialOneWireEchoSuppression.Location = new Point(12, 111);
            checkboxSerialOneWireEchoSuppression.Name = "checkboxSerialOneWireEchoSuppression";
            checkboxSerialOneWireEchoSuppression.Size = new Size(170, 17);
            checkboxSerialOneWireEchoSuppression.TabIndex = 8;
            checkboxSerialOneWireEchoSuppression.Text = "One-wire echo suppression:";
            toolTip1.SetToolTip(checkboxSerialOneWireEchoSuppression, "Suppress the echo of the command on 1-wire connections");
            checkboxSerialOneWireEchoSuppression.UseVisualStyleBackColor = true;
            // 
            // comboSerialParity
            // 
            comboSerialParity.DropDownStyle = ComboBoxStyle.DropDownList;
            comboSerialParity.FormattingEnabled = true;
            comboSerialParity.Items.AddRange(new object[] { "None", "Odd", "Even", "Mark", "Space" });
            comboSerialParity.Location = new Point(91, 61);
            comboSerialParity.Name = "comboSerialParity";
            comboSerialParity.Size = new Size(91, 21);
            comboSerialParity.TabIndex = 5;
            toolTip1.SetToolTip(comboSerialParity, "Parity");
            // 
            // comboSerialBaudrate
            // 
            comboSerialBaudrate.FormattingEnabled = true;
            comboSerialBaudrate.Items.AddRange(new object[] { "1200", "2400", "4800", "9600", "19200", "38400", "57600", "115200" });
            comboSerialBaudrate.Location = new Point(91, 37);
            comboSerialBaudrate.MaxLength = 8;
            comboSerialBaudrate.Name = "comboSerialBaudrate";
            comboSerialBaudrate.Size = new Size(91, 21);
            comboSerialBaudrate.TabIndex = 3;
            toolTip1.SetToolTip(comboSerialBaudrate, "Baudrate, bits per second");
            // 
            // comboConnection
            // 
            comboConnection.DropDownStyle = ComboBoxStyle.DropDownList;
            comboConnection.FormattingEnabled = true;
            comboConnection.Items.AddRange(new object[] { "Serial/RTU", "Modbus/TCP", "RTU over TCP" });
            comboConnection.Location = new Point(80, 18);
            comboConnection.Name = "comboConnection";
            comboConnection.Size = new Size(103, 21);
            comboConnection.TabIndex = 1;
            toolTip1.SetToolTip(comboConnection, "Select connection and protocol");
            comboConnection.SelectedIndexChanged += ComboConnection_SelectedIndexChanged;
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
            // picConnectionTypeIsOverride
            // 
            picConnectionTypeIsOverride.Image = (Image)resources.GetObject("picConnectionTypeIsOverride.Image");
            picConnectionTypeIsOverride.Location = new Point(186, 18);
            picConnectionTypeIsOverride.Name = "picConnectionTypeIsOverride";
            picConnectionTypeIsOverride.Size = new Size(20, 20);
            picConnectionTypeIsOverride.SizeMode = PictureBoxSizeMode.StretchImage;
            picConnectionTypeIsOverride.TabIndex = 12;
            picConnectionTypeIsOverride.TabStop = false;
            toolTip1.SetToolTip(picConnectionTypeIsOverride, "This parameter is overridden by a command-line option.\r\nThe value shown reflects the command-line setting, not the mbc project file.");
            picConnectionTypeIsOverride.Visible = false;
            // 
            // picPortIsOverride
            // 
            picPortIsOverride.Image = (Image)resources.GetObject("picPortIsOverride.Image");
            picPortIsOverride.Location = new Point(186, 13);
            picPortIsOverride.Name = "picPortIsOverride";
            picPortIsOverride.Size = new Size(20, 20);
            picPortIsOverride.SizeMode = PictureBoxSizeMode.StretchImage;
            picPortIsOverride.TabIndex = 13;
            picPortIsOverride.TabStop = false;
            toolTip1.SetToolTip(picPortIsOverride, "This parameter is overridden by a command-line option.\r\nThe value shown reflects the command-line setting, not the mbc project file.");
            picPortIsOverride.Visible = false;
            // 
            // picBaudrateIsOverride
            // 
            picBaudrateIsOverride.Image = (Image)resources.GetObject("picBaudrateIsOverride.Image");
            picBaudrateIsOverride.Location = new Point(186, 37);
            picBaudrateIsOverride.Name = "picBaudrateIsOverride";
            picBaudrateIsOverride.Size = new Size(20, 20);
            picBaudrateIsOverride.SizeMode = PictureBoxSizeMode.StretchImage;
            picBaudrateIsOverride.TabIndex = 14;
            picBaudrateIsOverride.TabStop = false;
            toolTip1.SetToolTip(picBaudrateIsOverride, "This parameter is overridden by a command-line option.\r\nThe value shown reflects the command-line setting, not the mbc project file.");
            picBaudrateIsOverride.Visible = false;
            // 
            // picParityIsOverride
            // 
            picParityIsOverride.Image = (Image)resources.GetObject("picParityIsOverride.Image");
            picParityIsOverride.Location = new Point(186, 61);
            picParityIsOverride.Name = "picParityIsOverride";
            picParityIsOverride.Size = new Size(20, 20);
            picParityIsOverride.SizeMode = PictureBoxSizeMode.StretchImage;
            picParityIsOverride.TabIndex = 15;
            picParityIsOverride.TabStop = false;
            toolTip1.SetToolTip(picParityIsOverride, "This parameter is overridden by a command-line option.\r\nThe value shown reflects the command-line setting, not the mbc project file.");
            picParityIsOverride.Visible = false;
            // 
            // picEndofmessagetimeoutIsOverride
            // 
            picEndofmessagetimeoutIsOverride.Image = (Image)resources.GetObject("picEndofmessagetimeoutIsOverride.Image");
            picEndofmessagetimeoutIsOverride.Location = new Point(186, 85);
            picEndofmessagetimeoutIsOverride.Name = "picEndofmessagetimeoutIsOverride";
            picEndofmessagetimeoutIsOverride.Size = new Size(20, 20);
            picEndofmessagetimeoutIsOverride.SizeMode = PictureBoxSizeMode.StretchImage;
            picEndofmessagetimeoutIsOverride.TabIndex = 16;
            picEndofmessagetimeoutIsOverride.TabStop = false;
            toolTip1.SetToolTip(picEndofmessagetimeoutIsOverride, "This parameter is overridden by a command-line option.\r\nThe value shown reflects the command-line setting, not the mbc project file.");
            picEndofmessagetimeoutIsOverride.Visible = false;
            // 
            // picIPAddressIsOverride
            // 
            picIPAddressIsOverride.Image = (Image)resources.GetObject("picIPAddressIsOverride.Image");
            picIPAddressIsOverride.Location = new Point(185, 18);
            picIPAddressIsOverride.Name = "picIPAddressIsOverride";
            picIPAddressIsOverride.Size = new Size(20, 20);
            picIPAddressIsOverride.SizeMode = PictureBoxSizeMode.StretchImage;
            picIPAddressIsOverride.TabIndex = 17;
            picIPAddressIsOverride.TabStop = false;
            toolTip1.SetToolTip(picIPAddressIsOverride, "This parameter is overridden by a command-line option.\r\nThe value shown reflects the command-line setting, not the mbc project file.");
            picIPAddressIsOverride.Visible = false;
            // 
            // picIPPortIsOverride
            // 
            picIPPortIsOverride.Image = (Image)resources.GetObject("picIPPortIsOverride.Image");
            picIPPortIsOverride.Location = new Point(186, 42);
            picIPPortIsOverride.Name = "picIPPortIsOverride";
            picIPPortIsOverride.Size = new Size(20, 20);
            picIPPortIsOverride.SizeMode = PictureBoxSizeMode.StretchImage;
            picIPPortIsOverride.TabIndex = 18;
            picIPPortIsOverride.TabStop = false;
            toolTip1.SetToolTip(picIPPortIsOverride, "This parameter is overridden by a command-line option.\r\nThe value shown reflects the command-line setting, not the mbc project file.");
            picIPPortIsOverride.Visible = false;
            // 
            // picOnewireechosuppressionIsOverride
            // 
            picOnewireechosuppressionIsOverride.Image = (Image)resources.GetObject("picOnewireechosuppressionIsOverride.Image");
            picOnewireechosuppressionIsOverride.Location = new Point(185, 108);
            picOnewireechosuppressionIsOverride.Name = "picOnewireechosuppressionIsOverride";
            picOnewireechosuppressionIsOverride.Size = new Size(20, 20);
            picOnewireechosuppressionIsOverride.SizeMode = PictureBoxSizeMode.StretchImage;
            picOnewireechosuppressionIsOverride.TabIndex = 19;
            picOnewireechosuppressionIsOverride.TabStop = false;
            toolTip1.SetToolTip(picOnewireechosuppressionIsOverride, "This parameter is overridden by a command-line option.\r\nThe value shown reflects the command-line setting, not the mbc project file.");
            picOnewireechosuppressionIsOverride.Visible = false;
            // 
            // picSlaveIDIsOverride
            // 
            picSlaveIDIsOverride.Image = (Image)resources.GetObject("picSlaveIDIsOverride.Image");
            picSlaveIDIsOverride.Location = new Point(212, 278);
            picSlaveIDIsOverride.Name = "picSlaveIDIsOverride";
            picSlaveIDIsOverride.Size = new Size(20, 20);
            picSlaveIDIsOverride.SizeMode = PictureBoxSizeMode.StretchImage;
            picSlaveIDIsOverride.TabIndex = 20;
            picSlaveIDIsOverride.TabStop = false;
            toolTip1.SetToolTip(picSlaveIDIsOverride, "This parameter is overridden by a command-line option.\r\nThe value shown reflects the command-line setting, not the mbc project file.");
            picSlaveIDIsOverride.Visible = false;
            // 
            // picResponsetimeoutIsOverride
            // 
            picResponsetimeoutIsOverride.Image = (Image)resources.GetObject("picResponsetimeoutIsOverride.Image");
            picResponsetimeoutIsOverride.Location = new Point(212, 301);
            picResponsetimeoutIsOverride.Name = "picResponsetimeoutIsOverride";
            picResponsetimeoutIsOverride.Size = new Size(20, 20);
            picResponsetimeoutIsOverride.SizeMode = PictureBoxSizeMode.StretchImage;
            picResponsetimeoutIsOverride.TabIndex = 21;
            picResponsetimeoutIsOverride.TabStop = false;
            toolTip1.SetToolTip(picResponsetimeoutIsOverride, "This parameter is overridden by a command-line option.\r\nThe value shown reflects the command-line setting, not the mbc project file.");
            picResponsetimeoutIsOverride.Visible = false;
            // 
            // picInterframedelayIsOverride
            // 
            picInterframedelayIsOverride.Image = (Image)resources.GetObject("picInterframedelayIsOverride.Image");
            picInterframedelayIsOverride.Location = new Point(212, 347);
            picInterframedelayIsOverride.Name = "picInterframedelayIsOverride";
            picInterframedelayIsOverride.Size = new Size(20, 20);
            picInterframedelayIsOverride.SizeMode = PictureBoxSizeMode.StretchImage;
            picInterframedelayIsOverride.TabIndex = 22;
            picInterframedelayIsOverride.TabStop = false;
            toolTip1.SetToolTip(picInterframedelayIsOverride, "This parameter is overridden by a command-line option.\r\nThe value shown reflects the command-line setting, not the mbc project file.");
            picInterframedelayIsOverride.Visible = false;
            // 
            // picSamplerateIsOverride
            // 
            picSamplerateIsOverride.Image = (Image)resources.GetObject("picSamplerateIsOverride.Image");
            picSamplerateIsOverride.Location = new Point(694, 16);
            picSamplerateIsOverride.Name = "picSamplerateIsOverride";
            picSamplerateIsOverride.Size = new Size(20, 20);
            picSamplerateIsOverride.SizeMode = PictureBoxSizeMode.StretchImage;
            picSamplerateIsOverride.TabIndex = 23;
            picSamplerateIsOverride.TabStop = false;
            toolTip1.SetToolTip(picSamplerateIsOverride, "This parameter is overridden by a command-line option.\r\nThe value shown reflects the command-line setting, not the mbc project file.");
            picSamplerateIsOverride.Visible = false;
            // 
            // picMaxFrameSizeIsOverride
            // 
            picMaxFrameSizeIsOverride.Image = (Image)resources.GetObject("picMaxFrameSizeIsOverride.Image");
            picMaxFrameSizeIsOverride.Location = new Point(212, 324);
            picMaxFrameSizeIsOverride.Name = "picMaxFrameSizeIsOverride";
            picMaxFrameSizeIsOverride.Size = new Size(20, 20);
            picMaxFrameSizeIsOverride.SizeMode = PictureBoxSizeMode.StretchImage;
            picMaxFrameSizeIsOverride.TabIndex = 23;
            picMaxFrameSizeIsOverride.TabStop = false;
            toolTip1.SetToolTip(picMaxFrameSizeIsOverride, "This parameter is overridden by a command-line option.\r\nThe value shown reflects the command-line setting, not the mbc project file.");
            picMaxFrameSizeIsOverride.Visible = false;
            // 
            // picwriteregFCIsOverride
            // 
            picwriteregFCIsOverride.Image = (Image)resources.GetObject("picwriteregFCIsOverride.Image");
            picwriteregFCIsOverride.Location = new Point(188, 16);
            picwriteregFCIsOverride.Name = "picwriteregFCIsOverride";
            picwriteregFCIsOverride.Size = new Size(20, 20);
            picwriteregFCIsOverride.SizeMode = PictureBoxSizeMode.StretchImage;
            picwriteregFCIsOverride.TabIndex = 13;
            picwriteregFCIsOverride.TabStop = false;
            toolTip1.SetToolTip(picwriteregFCIsOverride, "This parameter is overridden by a command-line option.\r\nThe value shown reflects the command-line setting, not the mbc project file.");
            picwriteregFCIsOverride.Visible = false;
            // 
            // numericInterFrameDelay
            // 
            numericInterFrameDelay.Location = new Point(137, 347);
            numericInterFrameDelay.Maximum = new decimal(new int[] { int.MaxValue, 0, 0, 0 });
            numericInterFrameDelay.Name = "numericInterFrameDelay";
            numericInterFrameDelay.Size = new Size(72, 20);
            numericInterFrameDelay.TabIndex = 11;
            toolTip1.SetToolTip(numericInterFrameDelay, "Delay between response and next query, in milliseconds, 0 is auto, auto is 4Tchar for serial channels and 0ms for TCP channels");
            // 
            // comboSerialPort
            // 
            comboSerialPort.FormattingEnabled = true;
            comboSerialPort.Location = new Point(91, 13);
            comboSerialPort.MaxLength = 30;
            comboSerialPort.Name = "comboSerialPort";
            comboSerialPort.Size = new Size(91, 21);
            comboSerialPort.TabIndex = 1;
            toolTip1.SetToolTip(comboSerialPort, "Serial port name. Dropdown shows existing ports");
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
            panel2.Controls.Add(butDebugInfo);
            panel2.Controls.Add(label17);
            panel2.Controls.Add(buttonApply);
            panel2.Controls.Add(comboStringEncoding);
            panel2.Controls.Add(buttonCancel);
            panel2.Controls.Add(picSamplerateIsOverride);
            panel2.Controls.Add(buttonOK);
            panel2.Controls.Add(textboxHoldingRegistersStart);
            panel2.Controls.Add(label5);
            panel2.Controls.Add(numericSamplerate);
            panel2.Controls.Add(label6);
            panel2.Controls.Add(groupBox3);
            panel2.Controls.Add(groupBox4);
            panel2.Controls.Add(groupboxModbusTcpServer);
            panel2.Controls.Add(groupBox2);
            panel2.Controls.Add(groupBox1);
            panel2.Controls.Add(groupboxConnection);
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(0, 42);
            panel2.Name = "panel2";
            panel2.Size = new Size(735, 422);
            panel2.TabIndex = 1;
            // 
            // butDebugInfo
            // 
            butDebugInfo.Location = new Point(587, 394);
            butDebugInfo.Name = "butDebugInfo";
            butDebugInfo.Size = new Size(100, 23);
            butDebugInfo.TabIndex = 15;
            butDebugInfo.Text = "DEBUG info";
            butDebugInfo.UseVisualStyleBackColor = true;
            butDebugInfo.Visible = false;
            butDebugInfo.Click += button1_Click;
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Location = new Point(268, 259);
            label17.Name = "label17";
            label17.Size = new Size(133, 13);
            label17.TabIndex = 5;
            label17.Text = "String encoding/decoding:";
            // 
            // buttonApply
            // 
            buttonApply.Location = new Point(338, 394);
            buttonApply.Name = "buttonApply";
            buttonApply.Size = new Size(75, 23);
            buttonApply.TabIndex = 13;
            buttonApply.Text = "Apply";
            buttonApply.UseVisualStyleBackColor = true;
            buttonApply.Click += ButtonApply_Click;
            // 
            // comboStringEncoding
            // 
            comboStringEncoding.DropDownStyle = ComboBoxStyle.DropDownList;
            comboStringEncoding.FormattingEnabled = true;
            comboStringEncoding.Location = new Point(270, 276);
            comboStringEncoding.Name = "comboStringEncoding";
            comboStringEncoding.Size = new Size(218, 21);
            comboStringEncoding.TabIndex = 6;
            // 
            // buttonCancel
            // 
            buttonCancel.DialogResult = DialogResult.Cancel;
            buttonCancel.Location = new Point(429, 394);
            buttonCancel.Name = "buttonCancel";
            buttonCancel.Size = new Size(75, 23);
            buttonCancel.TabIndex = 14;
            buttonCancel.Text = "Cancel";
            buttonCancel.UseVisualStyleBackColor = true;
            // 
            // buttonOK
            // 
            buttonOK.DialogResult = DialogResult.OK;
            buttonOK.Location = new Point(247, 394);
            buttonOK.Name = "buttonOK";
            buttonOK.Size = new Size(75, 23);
            buttonOK.TabIndex = 12;
            buttonOK.Text = "OK";
            buttonOK.UseVisualStyleBackColor = true;
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
            groupBox3.Controls.Add(radFloat32BigEndian);
            groupBox3.Controls.Add(radFloat32LittleEndian);
            groupBox3.Location = new Point(270, 144);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(218, 39);
            groupBox3.TabIndex = 3;
            groupBox3.TabStop = false;
            groupBox3.Text = "32-bit float word order";
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(radStringBigEndian);
            groupBox4.Controls.Add(radStringLittleEndian);
            groupBox4.Controls.Add(label14);
            groupBox4.Location = new Point(270, 191);
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
            groupboxModbusTcpServer.Controls.Add(numericTcpServerPort);
            groupboxModbusTcpServer.Controls.Add(checkboxTcpServerEnable);
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
            groupBox2.Controls.Add(radInt32BigEndian);
            groupBox2.Controls.Add(radInt32LittleEndian);
            groupBox2.Location = new Point(270, 100);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(218, 38);
            groupBox2.TabIndex = 2;
            groupBox2.TabStop = false;
            groupBox2.Text = "32-bit integer word order";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(picwriteregFCIsOverride);
            groupBox1.Controls.Add(radWriteFC6);
            groupBox1.Controls.Add(radWriteFC16);
            groupBox1.Controls.Add(radWriteFCAuto);
            groupBox1.Location = new Point(270, 10);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(218, 84);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            groupBox1.Text = "Functioncode to write holding registers";
            // 
            // groupboxConnection
            // 
            groupboxConnection.Controls.Add(numericInterFrameDelay);
            groupboxConnection.Controls.Add(numericMaximumFrameSize);
            groupboxConnection.Controls.Add(numericResponseTimeout);
            groupboxConnection.Controls.Add(numericSlaveAddress);
            groupboxConnection.Controls.Add(picMaxFrameSizeIsOverride);
            groupboxConnection.Controls.Add(picInterframedelayIsOverride);
            groupboxConnection.Controls.Add(picResponsetimeoutIsOverride);
            groupboxConnection.Controls.Add(picSlaveIDIsOverride);
            groupboxConnection.Controls.Add(picConnectionTypeIsOverride);
            groupboxConnection.Controls.Add(label15);
            groupboxConnection.Controls.Add(groupBoxTcpInterface);
            groupboxConnection.Controls.Add(groupBoxSerialInterface);
            groupboxConnection.Controls.Add(labelConnection);
            groupboxConnection.Controls.Add(comboConnection);
            groupboxConnection.Controls.Add(label8);
            groupboxConnection.Controls.Add(label3);
            groupboxConnection.Controls.Add(label7);
            groupboxConnection.Location = new Point(9, 10);
            groupboxConnection.Name = "groupboxConnection";
            groupboxConnection.Size = new Size(237, 379);
            groupboxConnection.TabIndex = 0;
            groupboxConnection.TabStop = false;
            groupboxConnection.Text = "Connection / Protocol";
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
            // groupBoxTcpInterface
            // 
            groupBoxTcpInterface.Controls.Add(picIPPortIsOverride);
            groupBoxTcpInterface.Controls.Add(label9);
            groupBoxTcpInterface.Controls.Add(numericIpPort);
            groupBoxTcpInterface.Controls.Add(comboTcpConnectMode);
            groupBoxTcpInterface.Controls.Add(textboxIpAddress);
            groupBoxTcpInterface.Controls.Add(picIPAddressIsOverride);
            groupBoxTcpInterface.Controls.Add(label11);
            groupBoxTcpInterface.Controls.Add(label10);
            groupBoxTcpInterface.Enabled = false;
            groupBoxTcpInterface.Location = new Point(0, 184);
            groupBoxTcpInterface.Name = "groupBoxTcpInterface";
            groupBoxTcpInterface.Size = new Size(212, 73);
            groupBoxTcpInterface.TabIndex = 3;
            groupBoxTcpInterface.TabStop = false;
            groupBoxTcpInterface.Text = "TCP connection";
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
            // groupBoxSerialInterface
            // 
            groupBoxSerialInterface.Controls.Add(numericSerialEndOfMessageTimeout);
            groupBoxSerialInterface.Controls.Add(comboSerialPort);
            groupBoxSerialInterface.Controls.Add(picOnewireechosuppressionIsOverride);
            groupBoxSerialInterface.Controls.Add(picBaudrateIsOverride);
            groupBoxSerialInterface.Controls.Add(picPortIsOverride);
            groupBoxSerialInterface.Controls.Add(checkboxSerialOneWireEchoSuppression);
            groupBoxSerialInterface.Controls.Add(label12);
            groupBoxSerialInterface.Controls.Add(label1);
            groupBoxSerialInterface.Controls.Add(picEndofmessagetimeoutIsOverride);
            groupBoxSerialInterface.Controls.Add(label2);
            groupBoxSerialInterface.Controls.Add(label4);
            groupBoxSerialInterface.Controls.Add(picParityIsOverride);
            groupBoxSerialInterface.Controls.Add(comboSerialParity);
            groupBoxSerialInterface.Controls.Add(comboSerialBaudrate);
            groupBoxSerialInterface.Location = new Point(0, 45);
            groupBoxSerialInterface.Name = "groupBoxSerialInterface";
            groupBoxSerialInterface.Size = new Size(212, 133);
            groupBoxSerialInterface.TabIndex = 2;
            groupBoxSerialInterface.TabStop = false;
            groupBoxSerialInterface.Text = "Serial interface";
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
            // labelConnection
            // 
            labelConnection.AutoSize = true;
            labelConnection.Location = new Point(7, 21);
            labelConnection.Name = "labelConnection";
            labelConnection.Size = new Size(64, 13);
            labelConnection.TabIndex = 0;
            labelConnection.Text = "Connection:";
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
            // warningPanel
            // 
            warningPanel.BorderStyle = BorderStyle.FixedSingle;
            warningPanel.Controls.Add(label16);
            warningPanel.Controls.Add(pictureBox1);
            warningPanel.Dock = DockStyle.Top;
            warningPanel.Location = new Point(0, 0);
            warningPanel.Name = "warningPanel";
            warningPanel.Size = new Size(735, 42);
            warningPanel.TabIndex = 0;
            warningPanel.Visible = false;
            // 
            // ProjectConfigurationForm
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            ClientSize = new Size(735, 468);
            Controls.Add(panel2);
            Controls.Add(warningPanel);
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
            ((System.ComponentModel.ISupportInitialize)numericSamplerate).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericTcpServerPort).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericMaximumFrameSize).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericResponseTimeout).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericSlaveAddress).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericIpPort).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericSerialEndOfMessageTimeout).EndInit();
            ((System.ComponentModel.ISupportInitialize)picConnectionTypeIsOverride).EndInit();
            ((System.ComponentModel.ISupportInitialize)picPortIsOverride).EndInit();
            ((System.ComponentModel.ISupportInitialize)picBaudrateIsOverride).EndInit();
            ((System.ComponentModel.ISupportInitialize)picParityIsOverride).EndInit();
            ((System.ComponentModel.ISupportInitialize)picEndofmessagetimeoutIsOverride).EndInit();
            ((System.ComponentModel.ISupportInitialize)picIPAddressIsOverride).EndInit();
            ((System.ComponentModel.ISupportInitialize)picIPPortIsOverride).EndInit();
            ((System.ComponentModel.ISupportInitialize)picOnewireechosuppressionIsOverride).EndInit();
            ((System.ComponentModel.ISupportInitialize)picSlaveIDIsOverride).EndInit();
            ((System.ComponentModel.ISupportInitialize)picResponsetimeoutIsOverride).EndInit();
            ((System.ComponentModel.ISupportInitialize)picInterframedelayIsOverride).EndInit();
            ((System.ComponentModel.ISupportInitialize)picSamplerateIsOverride).EndInit();
            ((System.ComponentModel.ISupportInitialize)picMaxFrameSizeIsOverride).EndInit();
            ((System.ComponentModel.ISupportInitialize)picwriteregFCIsOverride).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericInterFrameDelay).EndInit();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            groupboxModbusTcpServer.ResumeLayout(false);
            groupboxModbusTcpServer.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            groupboxConnection.ResumeLayout(false);
            groupboxConnection.PerformLayout();
            groupBoxTcpInterface.ResumeLayout(false);
            groupBoxTcpInterface.PerformLayout();
            groupBoxSerialInterface.ResumeLayout(false);
            groupBoxSerialInterface.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            warningPanel.ResumeLayout(false);
            warningPanel.PerformLayout();
            ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ToolTip toolTip1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox textboxHoldingRegistersStart;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown numericSamplerate;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton radFloat32LittleEndian;
        private System.Windows.Forms.RadioButton radFloat32BigEndian;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.RadioButton radStringLittleEndian;
        private System.Windows.Forms.RadioButton radStringBigEndian;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.GroupBox groupboxModbusTcpServer;
        private System.Windows.Forms.NumericUpDown numericTcpServerPort;
        private System.Windows.Forms.CheckBox checkboxTcpServerEnable;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton radInt32LittleEndian;
        private System.Windows.Forms.RadioButton radInt32BigEndian;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radWriteFC6;
        private System.Windows.Forms.RadioButton radWriteFC16;
        private System.Windows.Forms.RadioButton radWriteFCAuto;
        private System.Windows.Forms.GroupBox groupboxConnection;
        private LST.Controls.NumericUpDown2 numericInterFrameDelay;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.NumericUpDown numericMaximumFrameSize;
        private System.Windows.Forms.NumericUpDown numericResponseTimeout;
        private System.Windows.Forms.NumericUpDown numericSlaveAddress;
        private System.Windows.Forms.GroupBox groupBoxTcpInterface;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.NumericUpDown numericIpPort;
        private System.Windows.Forms.ComboBox comboTcpConnectMode;
        private System.Windows.Forms.TextBox textboxIpAddress;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.GroupBox groupBoxSerialInterface;
        private System.Windows.Forms.NumericUpDown numericSerialEndOfMessageTimeout;
        private System.Windows.Forms.CheckBox checkboxSerialOneWireEchoSuppression;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboSerialParity;
        private System.Windows.Forms.ComboBox comboSerialBaudrate;
        private System.Windows.Forms.Label labelConnection;
        private System.Windows.Forms.ComboBox comboConnection;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Panel warningPanel;
        private SerialPortCombobox comboSerialPort;
        private System.Windows.Forms.PictureBox picConnectionTypeIsOverride;
        private System.Windows.Forms.PictureBox picEndofmessagetimeoutIsOverride;
        private System.Windows.Forms.PictureBox picParityIsOverride;
        private System.Windows.Forms.PictureBox picBaudrateIsOverride;
        private System.Windows.Forms.PictureBox picPortIsOverride;
        private System.Windows.Forms.PictureBox picIPPortIsOverride;
        private System.Windows.Forms.PictureBox picIPAddressIsOverride;
        private System.Windows.Forms.PictureBox picOnewireechosuppressionIsOverride;
        private System.Windows.Forms.PictureBox picInterframedelayIsOverride;
        private System.Windows.Forms.PictureBox picResponsetimeoutIsOverride;
        private System.Windows.Forms.PictureBox picSlaveIDIsOverride;
        private System.Windows.Forms.PictureBox picSamplerateIsOverride;
        private System.Windows.Forms.PictureBox picMaxFrameSizeIsOverride;
        private System.Windows.Forms.PictureBox picwriteregFCIsOverride;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.ComboBox comboStringEncoding;
        private System.Windows.Forms.Button buttonApply;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOK;
        private Button butDebugInfo;
    }
}