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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProjectConfigurationForm));
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.numericSamplerate = new System.Windows.Forms.NumericUpDown();
            this.radFloat32LittleEndian = new System.Windows.Forms.RadioButton();
            this.radFloat32BigEndian = new System.Windows.Forms.RadioButton();
            this.radStringLittleEndian = new System.Windows.Forms.RadioButton();
            this.radStringBigEndian = new System.Windows.Forms.RadioButton();
            this.numericTcpServerPort = new System.Windows.Forms.NumericUpDown();
            this.checkboxTcpServerEnable = new System.Windows.Forms.CheckBox();
            this.radInt32LittleEndian = new System.Windows.Forms.RadioButton();
            this.radInt32BigEndian = new System.Windows.Forms.RadioButton();
            this.radWriteFC6 = new System.Windows.Forms.RadioButton();
            this.radWriteFC16 = new System.Windows.Forms.RadioButton();
            this.radWriteFCAuto = new System.Windows.Forms.RadioButton();
            this.numericMaximumFrameSize = new System.Windows.Forms.NumericUpDown();
            this.numericResponseTimeout = new System.Windows.Forms.NumericUpDown();
            this.numericSlaveAddress = new System.Windows.Forms.NumericUpDown();
            this.numericIpPort = new System.Windows.Forms.NumericUpDown();
            this.comboTcpConnectMode = new System.Windows.Forms.ComboBox();
            this.textboxIpAddress = new System.Windows.Forms.TextBox();
            this.numericSerialEndOfMessageTimeout = new System.Windows.Forms.NumericUpDown();
            this.checkboxSerialOneWireEchoSuppression = new System.Windows.Forms.CheckBox();
            this.comboSerialParity = new System.Windows.Forms.ComboBox();
            this.comboSerialBaudrate = new System.Windows.Forms.ComboBox();
            this.comboConnection = new System.Windows.Forms.ComboBox();
            this.textboxHoldingRegistersStart = new System.Windows.Forms.TextBox();
            this.picConnectionTypeIsOverride = new System.Windows.Forms.PictureBox();
            this.picPortIsOverride = new System.Windows.Forms.PictureBox();
            this.picBaudrateIsOverride = new System.Windows.Forms.PictureBox();
            this.picParityIsOverride = new System.Windows.Forms.PictureBox();
            this.picEndofmessagetimeoutIsOverride = new System.Windows.Forms.PictureBox();
            this.picIPAddressIsOverride = new System.Windows.Forms.PictureBox();
            this.picIPPortIsOverride = new System.Windows.Forms.PictureBox();
            this.picOnewireechosuppressionIsOverride = new System.Windows.Forms.PictureBox();
            this.picSlaveIDIsOverride = new System.Windows.Forms.PictureBox();
            this.picResponsetimeoutIsOverride = new System.Windows.Forms.PictureBox();
            this.picInterframedelayIsOverride = new System.Windows.Forms.PictureBox();
            this.picSamplerateIsOverride = new System.Windows.Forms.PictureBox();
            this.picMaxFrameSizeIsOverride = new System.Windows.Forms.PictureBox();
            this.picwriteregFCIsOverride = new System.Windows.Forms.PictureBox();
            this.numericInterFrameDelay = new LST.Controls.NumericUpDown2();
            this.comboSerialPort = new ModbusControl.SerialPortCombobox();
            this.label5 = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label17 = new System.Windows.Forms.Label();
            this.comboStringEncoding = new System.Windows.Forms.ComboBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label14 = new System.Windows.Forms.Label();
            this.groupboxModbusTcpServer = new System.Windows.Forms.GroupBox();
            this.label13 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupboxConnection = new System.Windows.Forms.GroupBox();
            this.label15 = new System.Windows.Forms.Label();
            this.groupBoxTcpInterface = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.groupBoxSerialInterface = new System.Windows.Forms.GroupBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.labelConnection = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label16 = new System.Windows.Forms.Label();
            this.warningPanel = new System.Windows.Forms.Panel();
            this.buttonPanel = new System.Windows.Forms.Panel();
            this.buttonApply = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numericSamplerate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericTcpServerPort)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericMaximumFrameSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericResponseTimeout)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericSlaveAddress)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericIpPort)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericSerialEndOfMessageTimeout)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picConnectionTypeIsOverride)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPortIsOverride)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBaudrateIsOverride)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picParityIsOverride)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picEndofmessagetimeoutIsOverride)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picIPAddressIsOverride)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picIPPortIsOverride)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picOnewireechosuppressionIsOverride)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSlaveIDIsOverride)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picResponsetimeoutIsOverride)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picInterframedelayIsOverride)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSamplerateIsOverride)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picMaxFrameSizeIsOverride)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picwriteregFCIsOverride)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericInterFrameDelay)).BeginInit();
            this.panel2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupboxModbusTcpServer.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupboxConnection.SuspendLayout();
            this.groupBoxTcpInterface.SuspendLayout();
            this.groupBoxSerialInterface.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.warningPanel.SuspendLayout();
            this.buttonPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // numericSamplerate
            // 
            this.numericSamplerate.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericSamplerate.Location = new System.Drawing.Point(571, 16);
            this.numericSamplerate.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.numericSamplerate.Name = "numericSamplerate";
            this.numericSamplerate.Size = new System.Drawing.Size(82, 20);
            this.numericSamplerate.TabIndex = 9;
            this.toolTip1.SetToolTip(this.numericSamplerate, "Sample period in milliseconds");
            this.numericSamplerate.Value = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            // 
            // radFloat32LittleEndian
            // 
            this.radFloat32LittleEndian.AutoSize = true;
            this.radFloat32LittleEndian.Checked = true;
            this.radFloat32LittleEndian.Location = new System.Drawing.Point(6, 13);
            this.radFloat32LittleEndian.Name = "radFloat32LittleEndian";
            this.radFloat32LittleEndian.Size = new System.Drawing.Size(70, 17);
            this.radFloat32LittleEndian.TabIndex = 0;
            this.radFloat32LittleEndian.TabStop = true;
            this.radFloat32LittleEndian.Text = "Low-High";
            this.toolTip1.SetToolTip(this.radFloat32LittleEndian, "First register is lower 16 bits of 32-bit float (most used)");
            this.radFloat32LittleEndian.UseVisualStyleBackColor = true;
            // 
            // radFloat32BigEndian
            // 
            this.radFloat32BigEndian.AutoSize = true;
            this.radFloat32BigEndian.Location = new System.Drawing.Point(82, 13);
            this.radFloat32BigEndian.Name = "radFloat32BigEndian";
            this.radFloat32BigEndian.Size = new System.Drawing.Size(70, 17);
            this.radFloat32BigEndian.TabIndex = 1;
            this.radFloat32BigEndian.Text = "High-Low";
            this.toolTip1.SetToolTip(this.radFloat32BigEndian, "First register is higher 16 bits of 32-bit float");
            this.radFloat32BigEndian.UseVisualStyleBackColor = true;
            // 
            // radStringLittleEndian
            // 
            this.radStringLittleEndian.AutoSize = true;
            this.radStringLittleEndian.Location = new System.Drawing.Point(6, 32);
            this.radStringLittleEndian.Name = "radStringLittleEndian";
            this.radStringLittleEndian.Size = new System.Drawing.Size(68, 17);
            this.radStringLittleEndian.TabIndex = 1;
            this.radStringLittleEndian.Text = "Low byte";
            this.toolTip1.SetToolTip(this.radStringLittleEndian, "Register with value 0x4241 is shown as \'AB\'");
            this.radStringLittleEndian.UseVisualStyleBackColor = true;
            // 
            // radStringBigEndian
            // 
            this.radStringBigEndian.AutoSize = true;
            this.radStringBigEndian.Checked = true;
            this.radStringBigEndian.Location = new System.Drawing.Point(82, 32);
            this.radStringBigEndian.Name = "radStringBigEndian";
            this.radStringBigEndian.Size = new System.Drawing.Size(70, 17);
            this.radStringBigEndian.TabIndex = 2;
            this.radStringBigEndian.TabStop = true;
            this.radStringBigEndian.Text = "High byte";
            this.toolTip1.SetToolTip(this.radStringBigEndian, "Register with value 0x4241 is shown as \'BA\'");
            this.radStringBigEndian.UseVisualStyleBackColor = true;
            // 
            // numericTcpServerPort
            // 
            this.numericTcpServerPort.Location = new System.Drawing.Point(66, 38);
            this.numericTcpServerPort.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numericTcpServerPort.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericTcpServerPort.Name = "numericTcpServerPort";
            this.numericTcpServerPort.Size = new System.Drawing.Size(63, 20);
            this.numericTcpServerPort.TabIndex = 2;
            this.toolTip1.SetToolTip(this.numericTcpServerPort, "TCP port number where server listens on (default 502)");
            this.numericTcpServerPort.Value = new decimal(new int[] {
            502,
            0,
            0,
            0});
            // 
            // checkboxTcpServerEnable
            // 
            this.checkboxTcpServerEnable.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkboxTcpServerEnable.Location = new System.Drawing.Point(7, 20);
            this.checkboxTcpServerEnable.Name = "checkboxTcpServerEnable";
            this.checkboxTcpServerEnable.Size = new System.Drawing.Size(73, 17);
            this.checkboxTcpServerEnable.TabIndex = 0;
            this.checkboxTcpServerEnable.Text = "Enable:";
            this.toolTip1.SetToolTip(this.checkboxTcpServerEnable, "Enable the Modbus/TCP bridge");
            this.checkboxTcpServerEnable.UseVisualStyleBackColor = true;
            // 
            // radInt32LittleEndian
            // 
            this.radInt32LittleEndian.AutoSize = true;
            this.radInt32LittleEndian.Checked = true;
            this.radInt32LittleEndian.Location = new System.Drawing.Point(6, 15);
            this.radInt32LittleEndian.Name = "radInt32LittleEndian";
            this.radInt32LittleEndian.Size = new System.Drawing.Size(70, 17);
            this.radInt32LittleEndian.TabIndex = 0;
            this.radInt32LittleEndian.TabStop = true;
            this.radInt32LittleEndian.Text = "Low-High";
            this.toolTip1.SetToolTip(this.radInt32LittleEndian, "First register is lower 16 bits of 32-bit value (most used)");
            this.radInt32LittleEndian.UseVisualStyleBackColor = true;
            // 
            // radInt32BigEndian
            // 
            this.radInt32BigEndian.AutoSize = true;
            this.radInt32BigEndian.Location = new System.Drawing.Point(82, 15);
            this.radInt32BigEndian.Name = "radInt32BigEndian";
            this.radInt32BigEndian.Size = new System.Drawing.Size(70, 17);
            this.radInt32BigEndian.TabIndex = 1;
            this.radInt32BigEndian.Text = "High-Low";
            this.toolTip1.SetToolTip(this.radInt32BigEndian, "First register is higher 16 bits of 32-bit value");
            this.radInt32BigEndian.UseVisualStyleBackColor = true;
            // 
            // radWriteFC6
            // 
            this.radWriteFC6.AutoSize = true;
            this.radWriteFC6.Location = new System.Drawing.Point(6, 17);
            this.radWriteFC6.Name = "radWriteFC6";
            this.radWriteFC6.Size = new System.Drawing.Size(167, 17);
            this.radWriteFC6.TabIndex = 0;
            this.radWriteFC6.TabStop = true;
            this.radWriteFC6.Text = "Use FC6 only (write single reg)";
            this.toolTip1.SetToolTip(this.radWriteFC6, "Use only function code 6 \'write single register\'");
            this.radWriteFC6.UseVisualStyleBackColor = true;
            // 
            // radWriteFC16
            // 
            this.radWriteFC16.AutoSize = true;
            this.radWriteFC16.Checked = true;
            this.radWriteFC16.Location = new System.Drawing.Point(6, 33);
            this.radWriteFC16.Name = "radWriteFC16";
            this.radWriteFC16.Size = new System.Drawing.Size(181, 17);
            this.radWriteFC16.TabIndex = 1;
            this.radWriteFC16.TabStop = true;
            this.radWriteFC16.Text = "Use FC16 only (write multiple reg)";
            this.toolTip1.SetToolTip(this.radWriteFC16, "Use only function code 16 \'write multiple registers\'");
            this.radWriteFC16.UseVisualStyleBackColor = true;
            // 
            // radWriteFCAuto
            // 
            this.radWriteFCAuto.AutoSize = true;
            this.radWriteFCAuto.Location = new System.Drawing.Point(6, 49);
            this.radWriteFCAuto.Name = "radWriteFCAuto";
            this.radWriteFCAuto.Size = new System.Drawing.Size(140, 17);
            this.radWriteFCAuto.TabIndex = 2;
            this.radWriteFCAuto.TabStop = true;
            this.radWriteFCAuto.Tag = "";
            this.radWriteFCAuto.Text = "Auto select FC6 or FC16";
            this.toolTip1.SetToolTip(this.radWriteFCAuto, "Automatically use FC6 for 16-bit registers or FC16 for 32-bit registers");
            this.radWriteFCAuto.UseVisualStyleBackColor = true;
            // 
            // numericMaximumFrameSize
            // 
            this.numericMaximumFrameSize.Location = new System.Drawing.Point(125, 320);
            this.numericMaximumFrameSize.Maximum = new decimal(new int[] {
            256,
            0,
            0,
            0});
            this.numericMaximumFrameSize.Minimum = new decimal(new int[] {
            13,
            0,
            0,
            0});
            this.numericMaximumFrameSize.Name = "numericMaximumFrameSize";
            this.numericMaximumFrameSize.Size = new System.Drawing.Size(61, 20);
            this.numericMaximumFrameSize.TabIndex = 9;
            this.toolTip1.SetToolTip(this.numericMaximumFrameSize, "Maximum number of bytes of query or response frame in RTU format ");
            this.numericMaximumFrameSize.Value = new decimal(new int[] {
            256,
            0,
            0,
            0});
            // 
            // numericResponseTimeout
            // 
            this.numericResponseTimeout.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericResponseTimeout.Location = new System.Drawing.Point(124, 296);
            this.numericResponseTimeout.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.numericResponseTimeout.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericResponseTimeout.Name = "numericResponseTimeout";
            this.numericResponseTimeout.Size = new System.Drawing.Size(62, 20);
            this.numericResponseTimeout.TabIndex = 7;
            this.toolTip1.SetToolTip(this.numericResponseTimeout, "Response timeout, in milliseconds");
            this.numericResponseTimeout.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // numericSlaveAddress
            // 
            this.numericSlaveAddress.Location = new System.Drawing.Point(125, 272);
            this.numericSlaveAddress.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericSlaveAddress.Name = "numericSlaveAddress";
            this.numericSlaveAddress.Size = new System.Drawing.Size(61, 20);
            this.numericSlaveAddress.TabIndex = 5;
            this.toolTip1.SetToolTip(this.numericSlaveAddress, "Slave ID (or slave address or unit identifier) 0-255");
            this.numericSlaveAddress.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // numericIpPort
            // 
            this.numericIpPort.Location = new System.Drawing.Point(84, 43);
            this.numericIpPort.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numericIpPort.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericIpPort.Name = "numericIpPort";
            this.numericIpPort.Size = new System.Drawing.Size(102, 20);
            this.numericIpPort.TabIndex = 3;
            this.toolTip1.SetToolTip(this.numericIpPort, "TCP port number, default 502");
            this.numericIpPort.Value = new decimal(new int[] {
            502,
            0,
            0,
            0});
            // 
            // comboTcpConnectMode
            // 
            this.comboTcpConnectMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboTcpConnectMode.Enabled = false;
            this.comboTcpConnectMode.FormattingEnabled = true;
            this.comboTcpConnectMode.Items.AddRange(new object[] {
            "Long-lived connection",
            "Short-lived connections"});
            this.comboTcpConnectMode.Location = new System.Drawing.Point(61, 67);
            this.comboTcpConnectMode.Name = "comboTcpConnectMode";
            this.comboTcpConnectMode.Size = new System.Drawing.Size(125, 21);
            this.comboTcpConnectMode.TabIndex = 5;
            this.toolTip1.SetToolTip(this.comboTcpConnectMode, "Type of TCP connection:");
            this.comboTcpConnectMode.Visible = false;
            // 
            // textboxIpAddress
            // 
            this.textboxIpAddress.Location = new System.Drawing.Point(84, 18);
            this.textboxIpAddress.MaxLength = 15;
            this.textboxIpAddress.Name = "textboxIpAddress";
            this.textboxIpAddress.Size = new System.Drawing.Size(102, 20);
            this.textboxIpAddress.TabIndex = 1;
            this.toolTip1.SetToolTip(this.textboxIpAddress, "Target IP address");
            // 
            // numericSerialEndOfMessageTimeout
            // 
            this.numericSerialEndOfMessageTimeout.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericSerialEndOfMessageTimeout.Location = new System.Drawing.Point(115, 85);
            this.numericSerialEndOfMessageTimeout.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.numericSerialEndOfMessageTimeout.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericSerialEndOfMessageTimeout.Name = "numericSerialEndOfMessageTimeout";
            this.numericSerialEndOfMessageTimeout.Size = new System.Drawing.Size(71, 20);
            this.numericSerialEndOfMessageTimeout.TabIndex = 7;
            this.toolTip1.SetToolTip(this.numericSerialEndOfMessageTimeout, "Response end-of-message timeout, or character timeout, in milliseconds");
            this.numericSerialEndOfMessageTimeout.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // checkboxSerialOneWireEchoSuppression
            // 
            this.checkboxSerialOneWireEchoSuppression.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkboxSerialOneWireEchoSuppression.Location = new System.Drawing.Point(18, 108);
            this.checkboxSerialOneWireEchoSuppression.Name = "checkboxSerialOneWireEchoSuppression";
            this.checkboxSerialOneWireEchoSuppression.Size = new System.Drawing.Size(169, 17);
            this.checkboxSerialOneWireEchoSuppression.TabIndex = 8;
            this.checkboxSerialOneWireEchoSuppression.Text = "One-wire echo suppression:";
            this.toolTip1.SetToolTip(this.checkboxSerialOneWireEchoSuppression, "Suppress the echo of the command on 1-wire connections");
            this.checkboxSerialOneWireEchoSuppression.UseVisualStyleBackColor = true;
            // 
            // comboSerialParity
            // 
            this.comboSerialParity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboSerialParity.FormattingEnabled = true;
            this.comboSerialParity.Items.AddRange(new object[] {
            "None",
            "Odd",
            "Even",
            "Mark",
            "Space"});
            this.comboSerialParity.Location = new System.Drawing.Point(95, 61);
            this.comboSerialParity.Name = "comboSerialParity";
            this.comboSerialParity.Size = new System.Drawing.Size(91, 21);
            this.comboSerialParity.TabIndex = 5;
            this.toolTip1.SetToolTip(this.comboSerialParity, "Parity");
            // 
            // comboSerialBaudrate
            // 
            this.comboSerialBaudrate.FormattingEnabled = true;
            this.comboSerialBaudrate.Items.AddRange(new object[] {
            "1200",
            "2400",
            "4800",
            "9600",
            "19200",
            "38400",
            "57600",
            "115200"});
            this.comboSerialBaudrate.Location = new System.Drawing.Point(95, 37);
            this.comboSerialBaudrate.MaxLength = 8;
            this.comboSerialBaudrate.Name = "comboSerialBaudrate";
            this.comboSerialBaudrate.Size = new System.Drawing.Size(91, 21);
            this.comboSerialBaudrate.TabIndex = 3;
            this.toolTip1.SetToolTip(this.comboSerialBaudrate, "Baudrate, bits per second");
            // 
            // comboConnection
            // 
            this.comboConnection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboConnection.FormattingEnabled = true;
            this.comboConnection.Items.AddRange(new object[] {
            "Serial/RTU",
            "Modbus/TCP",
            "RTU over TCP"});
            this.comboConnection.Location = new System.Drawing.Point(74, 18);
            this.comboConnection.Name = "comboConnection";
            this.comboConnection.Size = new System.Drawing.Size(112, 21);
            this.comboConnection.TabIndex = 1;
            this.toolTip1.SetToolTip(this.comboConnection, "Select connection and protocol");
            this.comboConnection.SelectedIndexChanged += new System.EventHandler(this.ComboConnection_SelectedIndexChanged);
            // 
            // textboxHoldingRegistersStart
            // 
            this.textboxHoldingRegistersStart.Location = new System.Drawing.Point(488, 58);
            this.textboxHoldingRegistersStart.Name = "textboxHoldingRegistersStart";
            this.textboxHoldingRegistersStart.ReadOnly = true;
            this.textboxHoldingRegistersStart.Size = new System.Drawing.Size(165, 20);
            this.textboxHoldingRegistersStart.TabIndex = 11;
            this.textboxHoldingRegistersStart.Text = "40001-49999 + 400001-465536";
            this.textboxHoldingRegistersStart.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.toolTip1.SetToolTip(this.textboxHoldingRegistersStart, "Register numbering scheme, as defined by <holdingregisterstart>in he mbc project " +
        "file.\r\n");
            // 
            // picConnectionTypeIsOverride
            // 
            this.picConnectionTypeIsOverride.Image = ((System.Drawing.Image)(resources.GetObject("picConnectionTypeIsOverride.Image")));
            this.picConnectionTypeIsOverride.Location = new System.Drawing.Point(188, 18);
            this.picConnectionTypeIsOverride.Name = "picConnectionTypeIsOverride";
            this.picConnectionTypeIsOverride.Size = new System.Drawing.Size(20, 20);
            this.picConnectionTypeIsOverride.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picConnectionTypeIsOverride.TabIndex = 12;
            this.picConnectionTypeIsOverride.TabStop = false;
            this.toolTip1.SetToolTip(this.picConnectionTypeIsOverride, "This parameter is overridden by a command-line option.\r\nThe value shown reflects " +
        "the command-line setting, not the mbc project file.");
            // 
            // picPortIsOverride
            // 
            this.picPortIsOverride.Image = ((System.Drawing.Image)(resources.GetObject("picPortIsOverride.Image")));
            this.picPortIsOverride.Location = new System.Drawing.Point(188, 58);
            this.picPortIsOverride.Name = "picPortIsOverride";
            this.picPortIsOverride.Size = new System.Drawing.Size(20, 20);
            this.picPortIsOverride.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picPortIsOverride.TabIndex = 13;
            this.picPortIsOverride.TabStop = false;
            this.toolTip1.SetToolTip(this.picPortIsOverride, "This parameter is overridden by a command-line option.\r\nThe value shown reflects " +
        "the command-line setting, not the mbc project file.");
            // 
            // picBaudrateIsOverride
            // 
            this.picBaudrateIsOverride.Image = ((System.Drawing.Image)(resources.GetObject("picBaudrateIsOverride.Image")));
            this.picBaudrateIsOverride.Location = new System.Drawing.Point(188, 82);
            this.picBaudrateIsOverride.Name = "picBaudrateIsOverride";
            this.picBaudrateIsOverride.Size = new System.Drawing.Size(20, 20);
            this.picBaudrateIsOverride.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBaudrateIsOverride.TabIndex = 14;
            this.picBaudrateIsOverride.TabStop = false;
            this.toolTip1.SetToolTip(this.picBaudrateIsOverride, "This parameter is overridden by a command-line option.\r\nThe value shown reflects " +
        "the command-line setting, not the mbc project file.");
            // 
            // picParityIsOverride
            // 
            this.picParityIsOverride.Image = ((System.Drawing.Image)(resources.GetObject("picParityIsOverride.Image")));
            this.picParityIsOverride.Location = new System.Drawing.Point(188, 106);
            this.picParityIsOverride.Name = "picParityIsOverride";
            this.picParityIsOverride.Size = new System.Drawing.Size(20, 20);
            this.picParityIsOverride.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picParityIsOverride.TabIndex = 15;
            this.picParityIsOverride.TabStop = false;
            this.toolTip1.SetToolTip(this.picParityIsOverride, "This parameter is overridden by a command-line option.\r\nThe value shown reflects " +
        "the command-line setting, not the mbc project file.");
            // 
            // picEndofmessagetimeoutIsOverride
            // 
            this.picEndofmessagetimeoutIsOverride.Image = ((System.Drawing.Image)(resources.GetObject("picEndofmessagetimeoutIsOverride.Image")));
            this.picEndofmessagetimeoutIsOverride.Location = new System.Drawing.Point(188, 129);
            this.picEndofmessagetimeoutIsOverride.Name = "picEndofmessagetimeoutIsOverride";
            this.picEndofmessagetimeoutIsOverride.Size = new System.Drawing.Size(20, 20);
            this.picEndofmessagetimeoutIsOverride.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picEndofmessagetimeoutIsOverride.TabIndex = 16;
            this.picEndofmessagetimeoutIsOverride.TabStop = false;
            this.toolTip1.SetToolTip(this.picEndofmessagetimeoutIsOverride, "This parameter is overridden by a command-line option.\r\nThe value shown reflects " +
        "the command-line setting, not the mbc project file.");
            // 
            // picIPAddressIsOverride
            // 
            this.picIPAddressIsOverride.Image = ((System.Drawing.Image)(resources.GetObject("picIPAddressIsOverride.Image")));
            this.picIPAddressIsOverride.Location = new System.Drawing.Point(188, 192);
            this.picIPAddressIsOverride.Name = "picIPAddressIsOverride";
            this.picIPAddressIsOverride.Size = new System.Drawing.Size(20, 20);
            this.picIPAddressIsOverride.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picIPAddressIsOverride.TabIndex = 17;
            this.picIPAddressIsOverride.TabStop = false;
            this.toolTip1.SetToolTip(this.picIPAddressIsOverride, "This parameter is overridden by a command-line option.\r\nThe value shown reflects " +
        "the command-line setting, not the mbc project file.");
            // 
            // picIPPortIsOverride
            // 
            this.picIPPortIsOverride.Image = ((System.Drawing.Image)(resources.GetObject("picIPPortIsOverride.Image")));
            this.picIPPortIsOverride.Location = new System.Drawing.Point(188, 218);
            this.picIPPortIsOverride.Name = "picIPPortIsOverride";
            this.picIPPortIsOverride.Size = new System.Drawing.Size(20, 20);
            this.picIPPortIsOverride.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picIPPortIsOverride.TabIndex = 18;
            this.picIPPortIsOverride.TabStop = false;
            this.toolTip1.SetToolTip(this.picIPPortIsOverride, "This parameter is overridden by a command-line option.\r\nThe value shown reflects " +
        "the command-line setting, not the mbc project file.");
            // 
            // picOnewireechosuppressionIsOverride
            // 
            this.picOnewireechosuppressionIsOverride.Image = ((System.Drawing.Image)(resources.GetObject("picOnewireechosuppressionIsOverride.Image")));
            this.picOnewireechosuppressionIsOverride.Location = new System.Drawing.Point(188, 151);
            this.picOnewireechosuppressionIsOverride.Name = "picOnewireechosuppressionIsOverride";
            this.picOnewireechosuppressionIsOverride.Size = new System.Drawing.Size(20, 20);
            this.picOnewireechosuppressionIsOverride.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picOnewireechosuppressionIsOverride.TabIndex = 19;
            this.picOnewireechosuppressionIsOverride.TabStop = false;
            this.toolTip1.SetToolTip(this.picOnewireechosuppressionIsOverride, "This parameter is overridden by a command-line option.\r\nThe value shown reflects " +
        "the command-line setting, not the mbc project file.");
            // 
            // picSlaveIDIsOverride
            // 
            this.picSlaveIDIsOverride.Image = ((System.Drawing.Image)(resources.GetObject("picSlaveIDIsOverride.Image")));
            this.picSlaveIDIsOverride.Location = new System.Drawing.Point(188, 272);
            this.picSlaveIDIsOverride.Name = "picSlaveIDIsOverride";
            this.picSlaveIDIsOverride.Size = new System.Drawing.Size(20, 20);
            this.picSlaveIDIsOverride.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picSlaveIDIsOverride.TabIndex = 20;
            this.picSlaveIDIsOverride.TabStop = false;
            this.toolTip1.SetToolTip(this.picSlaveIDIsOverride, "This parameter is overridden by a command-line option.\r\nThe value shown reflects " +
        "the command-line setting, not the mbc project file.");
            // 
            // picResponsetimeoutIsOverride
            // 
            this.picResponsetimeoutIsOverride.Image = ((System.Drawing.Image)(resources.GetObject("picResponsetimeoutIsOverride.Image")));
            this.picResponsetimeoutIsOverride.Location = new System.Drawing.Point(188, 296);
            this.picResponsetimeoutIsOverride.Name = "picResponsetimeoutIsOverride";
            this.picResponsetimeoutIsOverride.Size = new System.Drawing.Size(20, 20);
            this.picResponsetimeoutIsOverride.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picResponsetimeoutIsOverride.TabIndex = 21;
            this.picResponsetimeoutIsOverride.TabStop = false;
            this.toolTip1.SetToolTip(this.picResponsetimeoutIsOverride, "This parameter is overridden by a command-line option.\r\nThe value shown reflects " +
        "the command-line setting, not the mbc project file.");
            // 
            // picInterframedelayIsOverride
            // 
            this.picInterframedelayIsOverride.Image = ((System.Drawing.Image)(resources.GetObject("picInterframedelayIsOverride.Image")));
            this.picInterframedelayIsOverride.Location = new System.Drawing.Point(188, 344);
            this.picInterframedelayIsOverride.Name = "picInterframedelayIsOverride";
            this.picInterframedelayIsOverride.Size = new System.Drawing.Size(20, 20);
            this.picInterframedelayIsOverride.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picInterframedelayIsOverride.TabIndex = 22;
            this.picInterframedelayIsOverride.TabStop = false;
            this.toolTip1.SetToolTip(this.picInterframedelayIsOverride, "This parameter is overridden by a command-line option.\r\nThe value shown reflects " +
        "the command-line setting, not the mbc project file.");
            // 
            // picSamplerateIsOverride
            // 
            this.picSamplerateIsOverride.Image = ((System.Drawing.Image)(resources.GetObject("picSamplerateIsOverride.Image")));
            this.picSamplerateIsOverride.Location = new System.Drawing.Point(654, 15);
            this.picSamplerateIsOverride.Name = "picSamplerateIsOverride";
            this.picSamplerateIsOverride.Size = new System.Drawing.Size(20, 20);
            this.picSamplerateIsOverride.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picSamplerateIsOverride.TabIndex = 23;
            this.picSamplerateIsOverride.TabStop = false;
            this.toolTip1.SetToolTip(this.picSamplerateIsOverride, "This parameter is overridden by a command-line option.\r\nThe value shown reflects " +
        "the command-line setting, not the mbc project file.");
            // 
            // picMaxFrameSizeIsOverride
            // 
            this.picMaxFrameSizeIsOverride.Image = ((System.Drawing.Image)(resources.GetObject("picMaxFrameSizeIsOverride.Image")));
            this.picMaxFrameSizeIsOverride.Location = new System.Drawing.Point(188, 320);
            this.picMaxFrameSizeIsOverride.Name = "picMaxFrameSizeIsOverride";
            this.picMaxFrameSizeIsOverride.Size = new System.Drawing.Size(20, 20);
            this.picMaxFrameSizeIsOverride.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picMaxFrameSizeIsOverride.TabIndex = 23;
            this.picMaxFrameSizeIsOverride.TabStop = false;
            this.toolTip1.SetToolTip(this.picMaxFrameSizeIsOverride, "This parameter is overridden by a command-line option.\r\nThe value shown reflects " +
        "the command-line setting, not the mbc project file.");
            // 
            // picwriteregFCIsOverride
            // 
            this.picwriteregFCIsOverride.Image = ((System.Drawing.Image)(resources.GetObject("picwriteregFCIsOverride.Image")));
            this.picwriteregFCIsOverride.Location = new System.Drawing.Point(188, 13);
            this.picwriteregFCIsOverride.Name = "picwriteregFCIsOverride";
            this.picwriteregFCIsOverride.Size = new System.Drawing.Size(20, 20);
            this.picwriteregFCIsOverride.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picwriteregFCIsOverride.TabIndex = 13;
            this.picwriteregFCIsOverride.TabStop = false;
            this.toolTip1.SetToolTip(this.picwriteregFCIsOverride, "This parameter is overridden by a command-line option.\r\nThe value shown reflects " +
        "the command-line setting, not the mbc project file.");
            // 
            // numericInterFrameDelay
            // 
            this.numericInterFrameDelay.Location = new System.Drawing.Point(126, 344);
            this.numericInterFrameDelay.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.numericInterFrameDelay.Name = "numericInterFrameDelay";
            this.numericInterFrameDelay.Size = new System.Drawing.Size(61, 20);
            this.numericInterFrameDelay.TabIndex = 11;
            this.toolTip1.SetToolTip(this.numericInterFrameDelay, "Delay between response and next query, in milliseconds, 0 is auto, auto is 4Tchar" +
        " for serial channels and 0ms for TCP channels");
            // 
            // comboSerialPort
            // 
            this.comboSerialPort.FormattingEnabled = true;
            this.comboSerialPort.Location = new System.Drawing.Point(95, 13);
            this.comboSerialPort.MaxLength = 30;
            this.comboSerialPort.Name = "comboSerialPort";
            this.comboSerialPort.Size = new System.Drawing.Size(91, 21);
            this.comboSerialPort.TabIndex = 1;
            this.toolTip1.SetToolTip(this.comboSerialPort, "Serial port name. Dropdown shows existing ports");
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(459, 44);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(141, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Register numbering scheme:";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label17);
            this.panel2.Controls.Add(this.comboStringEncoding);
            this.panel2.Controls.Add(this.picSamplerateIsOverride);
            this.panel2.Controls.Add(this.groupBox5);
            this.panel2.Controls.Add(this.textboxHoldingRegistersStart);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.numericSamplerate);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.groupBox3);
            this.panel2.Controls.Add(this.groupBox4);
            this.panel2.Controls.Add(this.groupboxModbusTcpServer);
            this.panel2.Controls.Add(this.groupBox2);
            this.panel2.Controls.Add(this.groupBox1);
            this.panel2.Controls.Add(this.groupboxConnection);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 38);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(695, 382);
            this.panel2.TabIndex = 1;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(225, 246);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(133, 13);
            this.label17.TabIndex = 5;
            this.label17.Text = "String encoding/decoding:";
            // 
            // comboStringEncoding
            // 
            this.comboStringEncoding.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboStringEncoding.FormattingEnabled = true;
            this.comboStringEncoding.Location = new System.Drawing.Point(227, 262);
            this.comboStringEncoding.Name = "comboStringEncoding";
            this.comboStringEncoding.Size = new System.Drawing.Size(208, 21);
            this.comboStringEncoding.TabIndex = 6;
            // 
            // groupBox5
            // 
            this.groupBox5.Location = new System.Drawing.Point(444, 10);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(8, 364);
            this.groupBox5.TabIndex = 7;
            this.groupBox5.TabStop = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(459, 18);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(88, 13);
            this.label6.TabIndex = 8;
            this.label6.Text = "Sample rate (ms):";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.radFloat32LittleEndian);
            this.groupBox3.Controls.Add(this.radFloat32BigEndian);
            this.groupBox3.Location = new System.Drawing.Point(227, 133);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(210, 39);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "32-bit float word order";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.radStringLittleEndian);
            this.groupBox4.Controls.Add(this.radStringBigEndian);
            this.groupBox4.Controls.Add(this.label14);
            this.groupBox4.Location = new System.Drawing.Point(227, 180);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(210, 63);
            this.groupBox4.TabIndex = 4;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "String/ASCII byte order";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(3, 16);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(88, 13);
            this.label14.TabIndex = 0;
            this.label14.Text = "First character in:";
            // 
            // groupboxModbusTcpServer
            // 
            this.groupboxModbusTcpServer.Controls.Add(this.numericTcpServerPort);
            this.groupboxModbusTcpServer.Controls.Add(this.checkboxTcpServerEnable);
            this.groupboxModbusTcpServer.Controls.Add(this.label13);
            this.groupboxModbusTcpServer.Location = new System.Drawing.Point(463, 180);
            this.groupboxModbusTcpServer.Name = "groupboxModbusTcpServer";
            this.groupboxModbusTcpServer.Size = new System.Drawing.Size(178, 75);
            this.groupboxModbusTcpServer.TabIndex = 12;
            this.groupboxModbusTcpServer.TabStop = false;
            this.groupboxModbusTcpServer.Text = "Modbus/TCP bridge server";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(8, 40);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(52, 13);
            this.label13.TabIndex = 1;
            this.label13.Text = "TCP port:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.radInt32LittleEndian);
            this.groupBox2.Controls.Add(this.radInt32BigEndian);
            this.groupBox2.Location = new System.Drawing.Point(227, 89);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(210, 36);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "32-bit integer word order";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.picwriteregFCIsOverride);
            this.groupBox1.Controls.Add(this.radWriteFC6);
            this.groupBox1.Controls.Add(this.radWriteFC16);
            this.groupBox1.Controls.Add(this.radWriteFCAuto);
            this.groupBox1.Location = new System.Drawing.Point(227, 10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(210, 71);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Functioncode to write holding registers";
            // 
            // groupboxConnection
            // 
            this.groupboxConnection.Controls.Add(this.picMaxFrameSizeIsOverride);
            this.groupboxConnection.Controls.Add(this.picInterframedelayIsOverride);
            this.groupboxConnection.Controls.Add(this.picResponsetimeoutIsOverride);
            this.groupboxConnection.Controls.Add(this.picSlaveIDIsOverride);
            this.groupboxConnection.Controls.Add(this.picOnewireechosuppressionIsOverride);
            this.groupboxConnection.Controls.Add(this.picIPPortIsOverride);
            this.groupboxConnection.Controls.Add(this.picIPAddressIsOverride);
            this.groupboxConnection.Controls.Add(this.picEndofmessagetimeoutIsOverride);
            this.groupboxConnection.Controls.Add(this.picParityIsOverride);
            this.groupboxConnection.Controls.Add(this.picBaudrateIsOverride);
            this.groupboxConnection.Controls.Add(this.picPortIsOverride);
            this.groupboxConnection.Controls.Add(this.picConnectionTypeIsOverride);
            this.groupboxConnection.Controls.Add(this.numericInterFrameDelay);
            this.groupboxConnection.Controls.Add(this.label15);
            this.groupboxConnection.Controls.Add(this.numericMaximumFrameSize);
            this.groupboxConnection.Controls.Add(this.numericResponseTimeout);
            this.groupboxConnection.Controls.Add(this.numericSlaveAddress);
            this.groupboxConnection.Controls.Add(this.groupBoxTcpInterface);
            this.groupboxConnection.Controls.Add(this.groupBoxSerialInterface);
            this.groupboxConnection.Controls.Add(this.labelConnection);
            this.groupboxConnection.Controls.Add(this.comboConnection);
            this.groupboxConnection.Controls.Add(this.label8);
            this.groupboxConnection.Controls.Add(this.label3);
            this.groupboxConnection.Controls.Add(this.label7);
            this.groupboxConnection.Location = new System.Drawing.Point(9, 10);
            this.groupboxConnection.Name = "groupboxConnection";
            this.groupboxConnection.Size = new System.Drawing.Size(212, 372);
            this.groupboxConnection.TabIndex = 0;
            this.groupboxConnection.TabStop = false;
            this.groupboxConnection.Text = "Connection / Protocol";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(7, 346);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(110, 13);
            this.label15.TabIndex = 10;
            this.label15.Text = "Inter-frame delay (ms):";
            // 
            // groupBoxTcpInterface
            // 
            this.groupBoxTcpInterface.Controls.Add(this.label9);
            this.groupBoxTcpInterface.Controls.Add(this.numericIpPort);
            this.groupBoxTcpInterface.Controls.Add(this.comboTcpConnectMode);
            this.groupBoxTcpInterface.Controls.Add(this.textboxIpAddress);
            this.groupBoxTcpInterface.Controls.Add(this.label11);
            this.groupBoxTcpInterface.Controls.Add(this.label10);
            this.groupBoxTcpInterface.Enabled = false;
            this.groupBoxTcpInterface.Location = new System.Drawing.Point(0, 175);
            this.groupBoxTcpInterface.Name = "groupBoxTcpInterface";
            this.groupBoxTcpInterface.Size = new System.Drawing.Size(210, 94);
            this.groupBoxTcpInterface.TabIndex = 3;
            this.groupBoxTcpInterface.TabStop = false;
            this.groupBoxTcpInterface.Text = "TCP connection";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(18, 71);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(37, 13);
            this.label9.TabIndex = 4;
            this.label9.Text = "Mode:";
            this.label9.Visible = false;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(18, 45);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(52, 13);
            this.label11.TabIndex = 2;
            this.label11.Text = "TCP port:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(18, 21);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(60, 13);
            this.label10.TabIndex = 0;
            this.label10.Text = "IP address:";
            // 
            // groupBoxSerialInterface
            // 
            this.groupBoxSerialInterface.Controls.Add(this.comboSerialPort);
            this.groupBoxSerialInterface.Controls.Add(this.numericSerialEndOfMessageTimeout);
            this.groupBoxSerialInterface.Controls.Add(this.checkboxSerialOneWireEchoSuppression);
            this.groupBoxSerialInterface.Controls.Add(this.label12);
            this.groupBoxSerialInterface.Controls.Add(this.label1);
            this.groupBoxSerialInterface.Controls.Add(this.label2);
            this.groupBoxSerialInterface.Controls.Add(this.label4);
            this.groupBoxSerialInterface.Controls.Add(this.comboSerialParity);
            this.groupBoxSerialInterface.Controls.Add(this.comboSerialBaudrate);
            this.groupBoxSerialInterface.Location = new System.Drawing.Point(0, 45);
            this.groupBoxSerialInterface.Name = "groupBoxSerialInterface";
            this.groupBoxSerialInterface.Size = new System.Drawing.Size(210, 128);
            this.groupBoxSerialInterface.TabIndex = 2;
            this.groupBoxSerialInterface.TabStop = false;
            this.groupBoxSerialInterface.Text = "Serial interface";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(18, 86);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(93, 13);
            this.label12.TabIndex = 6;
            this.label12.Text = "EOM timeout (ms):";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Port:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Baudrate:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 65);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(36, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Parity:";
            // 
            // labelConnection
            // 
            this.labelConnection.AutoSize = true;
            this.labelConnection.Location = new System.Drawing.Point(7, 21);
            this.labelConnection.Name = "labelConnection";
            this.labelConnection.Size = new System.Drawing.Size(64, 13);
            this.labelConnection.TabIndex = 0;
            this.labelConnection.Text = "Connection:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 298);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(117, 13);
            this.label8.TabIndex = 6;
            this.label8.Text = "Response timeout (ms):";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 274);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Slave ID:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 322);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(104, 13);
            this.label7.TabIndex = 8;
            this.label7.Text = "Maximum frame size:";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(17, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(32, 32);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // label16
            // 
            this.label16.Location = new System.Drawing.Point(62, 4);
            this.label16.Name = "label16";
            this.label16.Padding = new System.Windows.Forms.Padding(0, 1, 0, 0);
            this.label16.Size = new System.Drawing.Size(537, 30);
            this.label16.TabIndex = 0;
            this.label16.Text = "Parameters marked with a warning icon are overridden by command-line options. \r\nT" +
    "he displayed values come from the command line, not the mbc project file.";
            // 
            // warningPanel
            // 
            this.warningPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.warningPanel.Controls.Add(this.label16);
            this.warningPanel.Controls.Add(this.pictureBox1);
            this.warningPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.warningPanel.Location = new System.Drawing.Point(0, 0);
            this.warningPanel.Name = "warningPanel";
            this.warningPanel.Size = new System.Drawing.Size(695, 38);
            this.warningPanel.TabIndex = 0;
            this.warningPanel.Visible = false;
            // 
            // buttonPanel
            // 
            this.buttonPanel.Controls.Add(this.buttonApply);
            this.buttonPanel.Controls.Add(this.buttonCancel);
            this.buttonPanel.Controls.Add(this.buttonOK);
            this.buttonPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.buttonPanel.Location = new System.Drawing.Point(0, 420);
            this.buttonPanel.Name = "buttonPanel";
            this.buttonPanel.Size = new System.Drawing.Size(695, 35);
            this.buttonPanel.TabIndex = 2;
            // 
            // buttonApply
            // 
            this.buttonApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonApply.Location = new System.Drawing.Point(398, 6);
            this.buttonApply.Name = "buttonApply";
            this.buttonApply.Size = new System.Drawing.Size(75, 23);
            this.buttonApply.TabIndex = 2;
            this.buttonApply.Text = "Apply";
            this.buttonApply.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(309, 6);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 1;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Location = new System.Drawing.Point(220, 6);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 0;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            // 
            // ProjectConfigurationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(695, 452);
            this.ControlBox = false;
            this.Controls.Add(this.buttonPanel);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.warningPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(698, 45);
            this.Name = "ProjectConfigurationForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Project configuration";
            this.Shown += new System.EventHandler(this.ConfigForm_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.numericSamplerate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericTcpServerPort)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericMaximumFrameSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericResponseTimeout)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericSlaveAddress)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericIpPort)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericSerialEndOfMessageTimeout)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picConnectionTypeIsOverride)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPortIsOverride)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBaudrateIsOverride)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picParityIsOverride)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picEndofmessagetimeoutIsOverride)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picIPAddressIsOverride)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picIPPortIsOverride)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picOnewireechosuppressionIsOverride)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSlaveIDIsOverride)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picResponsetimeoutIsOverride)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picInterframedelayIsOverride)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSamplerateIsOverride)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picMaxFrameSizeIsOverride)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picwriteregFCIsOverride)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericInterFrameDelay)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupboxModbusTcpServer.ResumeLayout(false);
            this.groupboxModbusTcpServer.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupboxConnection.ResumeLayout(false);
            this.groupboxConnection.PerformLayout();
            this.groupBoxTcpInterface.ResumeLayout(false);
            this.groupBoxTcpInterface.PerformLayout();
            this.groupBoxSerialInterface.ResumeLayout(false);
            this.groupBoxSerialInterface.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.warningPanel.ResumeLayout(false);
            this.warningPanel.PerformLayout();
            this.buttonPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ToolTip toolTip1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox groupBox5;
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
        private System.Windows.Forms.Panel buttonPanel;
        private System.Windows.Forms.Button buttonApply;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOK;
    }
}