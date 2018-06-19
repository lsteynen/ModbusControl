namespace ModbusControlSpace
{
    partial class ConfigForm
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
            this.groupboxConnection = new System.Windows.Forms.GroupBox();
            this.numericMaximumFrameSize = new System.Windows.Forms.NumericUpDown();
            this.numericResponseTimeout = new System.Windows.Forms.NumericUpDown();
            this.numericSlaveAddress = new System.Windows.Forms.NumericUpDown();
            this.groupBoxTcpInterface = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.numericIpPort = new System.Windows.Forms.NumericUpDown();
            this.comboTcpConnectMode = new System.Windows.Forms.ComboBox();
            this.textboxIpAddress = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.groupBoxSerialInterface = new System.Windows.Forms.GroupBox();
            this.numericSerialEndOfMessageTimeout = new System.Windows.Forms.NumericUpDown();
            this.checkboxSerialOneWireEchoSuppression = new System.Windows.Forms.CheckBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.comboSerialParity = new System.Windows.Forms.ComboBox();
            this.comboSerialBaudrate = new System.Windows.Forms.ComboBox();
            this.comboSerialPort = new System.Windows.Forms.ComboBox();
            this.labelConnection = new System.Windows.Forms.Label();
            this.comboConnection = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.checkboxShowInfo = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radiobuttonWriteFC6 = new System.Windows.Forms.RadioButton();
            this.radiobuttonWriteFC16 = new System.Windows.Forms.RadioButton();
            this.radiobuttonWriteFCAuto = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.radiobuttonInt32LittleEndian = new System.Windows.Forms.RadioButton();
            this.radiobuttonInt32BigEndian = new System.Windows.Forms.RadioButton();
            this.groupboxModbusTcpServer = new System.Windows.Forms.GroupBox();
            this.numericTcpServerPort = new System.Windows.Forms.NumericUpDown();
            this.checkboxTcpServerEnable = new System.Windows.Forms.CheckBox();
            this.label13 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.radiobuttonAsciiLittleEndian = new System.Windows.Forms.RadioButton();
            this.radiobuttonAsciiBigEndian = new System.Windows.Forms.RadioButton();
            this.label14 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.radiobuttonFloat32LittleEndian = new System.Windows.Forms.RadioButton();
            this.radiobuttonFloat32BigEndian = new System.Windows.Forms.RadioButton();
            this.numericSamplerate = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.label5 = new System.Windows.Forms.Label();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.textboxHoldingRegistersStart = new System.Windows.Forms.TextBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.numericInterFrameDelay = new System.Windows.Forms.NumericUpDown();
            this.label15 = new System.Windows.Forms.Label();
            this.groupboxConnection.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericMaximumFrameSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericResponseTimeout)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericSlaveAddress)).BeginInit();
            this.groupBoxTcpInterface.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericIpPort)).BeginInit();
            this.groupBoxSerialInterface.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericSerialEndOfMessageTimeout)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupboxModbusTcpServer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericTcpServerPort)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericSamplerate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericInterFrameDelay)).BeginInit();
            this.SuspendLayout();
            // 
            // groupboxConnection
            // 
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
            this.groupboxConnection.Location = new System.Drawing.Point(12, 12);
            this.groupboxConnection.Name = "groupboxConnection";
            this.groupboxConnection.Size = new System.Drawing.Size(194, 372);
            this.groupboxConnection.TabIndex = 0;
            this.groupboxConnection.TabStop = false;
            this.groupboxConnection.Text = "Connection / Protocol";
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
            this.numericMaximumFrameSize.TabIndex = 7;
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
            9999999,
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
            this.numericResponseTimeout.TabIndex = 9;
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
            this.groupBoxTcpInterface.Size = new System.Drawing.Size(194, 94);
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
            this.toolTip1.SetToolTip(this.numericIpPort, "TCP port number, usually 502");
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
            this.groupBoxSerialInterface.Controls.Add(this.numericSerialEndOfMessageTimeout);
            this.groupBoxSerialInterface.Controls.Add(this.checkboxSerialOneWireEchoSuppression);
            this.groupBoxSerialInterface.Controls.Add(this.label12);
            this.groupBoxSerialInterface.Controls.Add(this.label1);
            this.groupBoxSerialInterface.Controls.Add(this.label2);
            this.groupBoxSerialInterface.Controls.Add(this.label4);
            this.groupBoxSerialInterface.Controls.Add(this.comboSerialParity);
            this.groupBoxSerialInterface.Controls.Add(this.comboSerialBaudrate);
            this.groupBoxSerialInterface.Controls.Add(this.comboSerialPort);
            this.groupBoxSerialInterface.Location = new System.Drawing.Point(0, 45);
            this.groupBoxSerialInterface.Name = "groupBoxSerialInterface";
            this.groupBoxSerialInterface.Size = new System.Drawing.Size(194, 128);
            this.groupBoxSerialInterface.TabIndex = 2;
            this.groupBoxSerialInterface.TabStop = false;
            this.groupBoxSerialInterface.Text = "Serial interface";
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
            9999999,
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
            // comboSerialPort
            // 
            this.comboSerialPort.FormattingEnabled = true;
            this.comboSerialPort.Location = new System.Drawing.Point(95, 13);
            this.comboSerialPort.MaxLength = 20;
            this.comboSerialPort.Name = "comboSerialPort";
            this.comboSerialPort.Size = new System.Drawing.Size(91, 21);
            this.comboSerialPort.TabIndex = 1;
            this.toolTip1.SetToolTip(this.comboSerialPort, "Serial port name. Dropdown shows existing ports");
            this.comboSerialPort.DropDown += new System.EventHandler(this.comboSerialPort_DropDown);
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
            this.comboConnection.SelectedIndexChanged += new System.EventHandler(this.comboConnection_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 298);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(117, 13);
            this.label8.TabIndex = 8;
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
            this.label7.TabIndex = 6;
            this.label7.Text = "Maximum frame size:";
            // 
            // checkboxShowInfo
            // 
            this.checkboxShowInfo.AutoSize = true;
            this.checkboxShowInfo.Checked = true;
            this.checkboxShowInfo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkboxShowInfo.Location = new System.Drawing.Point(466, 68);
            this.checkboxShowInfo.Name = "checkboxShowInfo";
            this.checkboxShowInfo.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.checkboxShowInfo.Size = new System.Drawing.Size(138, 17);
            this.checkboxShowInfo.TabIndex = 10;
            this.checkboxShowInfo.Text = "Show info in description";
            this.toolTip1.SetToolTip(this.checkboxShowInfo, "Add info like format, read/write and decimals to the description");
            this.checkboxShowInfo.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radiobuttonWriteFC6);
            this.groupBox1.Controls.Add(this.radiobuttonWriteFC16);
            this.groupBox1.Controls.Add(this.radiobuttonWriteFCAuto);
            this.groupBox1.Location = new System.Drawing.Point(212, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(210, 74);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Functioncode to write holding registers";
            // 
            // radiobuttonWriteFC6
            // 
            this.radiobuttonWriteFC6.AutoSize = true;
            this.radiobuttonWriteFC6.Location = new System.Drawing.Point(6, 17);
            this.radiobuttonWriteFC6.Name = "radiobuttonWriteFC6";
            this.radiobuttonWriteFC6.Size = new System.Drawing.Size(167, 17);
            this.radiobuttonWriteFC6.TabIndex = 0;
            this.radiobuttonWriteFC6.TabStop = true;
            this.radiobuttonWriteFC6.Text = "Use FC6 only (write single reg)";
            this.toolTip1.SetToolTip(this.radiobuttonWriteFC6, "Use only function code 6 \'write single register\'");
            this.radiobuttonWriteFC6.UseVisualStyleBackColor = true;
            // 
            // radiobuttonWriteFC16
            // 
            this.radiobuttonWriteFC16.AutoSize = true;
            this.radiobuttonWriteFC16.Checked = true;
            this.radiobuttonWriteFC16.Location = new System.Drawing.Point(6, 33);
            this.radiobuttonWriteFC16.Name = "radiobuttonWriteFC16";
            this.radiobuttonWriteFC16.Size = new System.Drawing.Size(181, 17);
            this.radiobuttonWriteFC16.TabIndex = 1;
            this.radiobuttonWriteFC16.TabStop = true;
            this.radiobuttonWriteFC16.Text = "Use FC16 only (write multiple reg)";
            this.toolTip1.SetToolTip(this.radiobuttonWriteFC16, "Use only function code 16 \'write multiple registers\'");
            this.radiobuttonWriteFC16.UseVisualStyleBackColor = true;
            // 
            // radiobuttonWriteFCAuto
            // 
            this.radiobuttonWriteFCAuto.AutoSize = true;
            this.radiobuttonWriteFCAuto.Location = new System.Drawing.Point(6, 49);
            this.radiobuttonWriteFCAuto.Name = "radiobuttonWriteFCAuto";
            this.radiobuttonWriteFCAuto.Size = new System.Drawing.Size(108, 17);
            this.radiobuttonWriteFCAuto.TabIndex = 2;
            this.radiobuttonWriteFCAuto.TabStop = true;
            this.radiobuttonWriteFCAuto.Tag = "";
            this.radiobuttonWriteFCAuto.Text = "Use FC6 and F16";
            this.toolTip1.SetToolTip(this.radiobuttonWriteFCAuto, "Use FC6 for single register write and FC16 for multiple registers write");
            this.radiobuttonWriteFCAuto.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.radiobuttonInt32LittleEndian);
            this.groupBox2.Controls.Add(this.radiobuttonInt32BigEndian);
            this.groupBox2.Location = new System.Drawing.Point(212, 92);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(210, 36);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "32-bit integer word order";
            // 
            // radiobuttonInt32LittleEndian
            // 
            this.radiobuttonInt32LittleEndian.AutoSize = true;
            this.radiobuttonInt32LittleEndian.Checked = true;
            this.radiobuttonInt32LittleEndian.Location = new System.Drawing.Point(6, 15);
            this.radiobuttonInt32LittleEndian.Name = "radiobuttonInt32LittleEndian";
            this.radiobuttonInt32LittleEndian.Size = new System.Drawing.Size(70, 17);
            this.radiobuttonInt32LittleEndian.TabIndex = 0;
            this.radiobuttonInt32LittleEndian.TabStop = true;
            this.radiobuttonInt32LittleEndian.Text = "Low-High";
            this.toolTip1.SetToolTip(this.radiobuttonInt32LittleEndian, "First register is lower 16 bits of 32-bit value (most used)");
            this.radiobuttonInt32LittleEndian.UseVisualStyleBackColor = true;
            // 
            // radiobuttonInt32BigEndian
            // 
            this.radiobuttonInt32BigEndian.AutoSize = true;
            this.radiobuttonInt32BigEndian.Location = new System.Drawing.Point(82, 15);
            this.radiobuttonInt32BigEndian.Name = "radiobuttonInt32BigEndian";
            this.radiobuttonInt32BigEndian.Size = new System.Drawing.Size(70, 17);
            this.radiobuttonInt32BigEndian.TabIndex = 1;
            this.radiobuttonInt32BigEndian.Text = "High-Low";
            this.toolTip1.SetToolTip(this.radiobuttonInt32BigEndian, "First register is higher 16 bits of 32-bit value");
            this.radiobuttonInt32BigEndian.UseVisualStyleBackColor = true;
            // 
            // groupboxModbusTcpServer
            // 
            this.groupboxModbusTcpServer.Controls.Add(this.numericTcpServerPort);
            this.groupboxModbusTcpServer.Controls.Add(this.checkboxTcpServerEnable);
            this.groupboxModbusTcpServer.Controls.Add(this.label13);
            this.groupboxModbusTcpServer.Location = new System.Drawing.Point(466, 285);
            this.groupboxModbusTcpServer.Name = "groupboxModbusTcpServer";
            this.groupboxModbusTcpServer.Size = new System.Drawing.Size(178, 75);
            this.groupboxModbusTcpServer.TabIndex = 11;
            this.groupboxModbusTcpServer.TabStop = false;
            this.groupboxModbusTcpServer.Text = "Modbus/TCP bridge server";
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
            this.toolTip1.SetToolTip(this.numericTcpServerPort, "TCP port number where server listens on (usually 502)");
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
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(8, 40);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(52, 13);
            this.label13.TabIndex = 1;
            this.label13.Text = "TCP port:";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.radiobuttonAsciiLittleEndian);
            this.groupBox4.Controls.Add(this.radiobuttonAsciiBigEndian);
            this.groupBox4.Controls.Add(this.label14);
            this.groupBox4.Location = new System.Drawing.Point(212, 179);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(210, 63);
            this.groupBox4.TabIndex = 4;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "ASCII/string byte order";
            // 
            // radiobuttonAsciiLittleEndian
            // 
            this.radiobuttonAsciiLittleEndian.AutoSize = true;
            this.radiobuttonAsciiLittleEndian.Location = new System.Drawing.Point(6, 32);
            this.radiobuttonAsciiLittleEndian.Name = "radiobuttonAsciiLittleEndian";
            this.radiobuttonAsciiLittleEndian.Size = new System.Drawing.Size(68, 17);
            this.radiobuttonAsciiLittleEndian.TabIndex = 1;
            this.radiobuttonAsciiLittleEndian.Text = "Low byte";
            this.toolTip1.SetToolTip(this.radiobuttonAsciiLittleEndian, "Register with value 0x4241 is shown as \'AB\'");
            this.radiobuttonAsciiLittleEndian.UseVisualStyleBackColor = true;
            // 
            // radiobuttonAsciiBigEndian
            // 
            this.radiobuttonAsciiBigEndian.AutoSize = true;
            this.radiobuttonAsciiBigEndian.Checked = true;
            this.radiobuttonAsciiBigEndian.Location = new System.Drawing.Point(82, 32);
            this.radiobuttonAsciiBigEndian.Name = "radiobuttonAsciiBigEndian";
            this.radiobuttonAsciiBigEndian.Size = new System.Drawing.Size(70, 17);
            this.radiobuttonAsciiBigEndian.TabIndex = 2;
            this.radiobuttonAsciiBigEndian.TabStop = true;
            this.radiobuttonAsciiBigEndian.Text = "High byte";
            this.toolTip1.SetToolTip(this.radiobuttonAsciiBigEndian, "Register with value 0x4241 is shown as \'BA\'");
            this.radiobuttonAsciiBigEndian.UseVisualStyleBackColor = true;
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
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.radiobuttonFloat32LittleEndian);
            this.groupBox3.Controls.Add(this.radiobuttonFloat32BigEndian);
            this.groupBox3.Location = new System.Drawing.Point(212, 134);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(210, 39);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "32-bit float word order";
            // 
            // radiobuttonFloat32LittleEndian
            // 
            this.radiobuttonFloat32LittleEndian.AutoSize = true;
            this.radiobuttonFloat32LittleEndian.Checked = true;
            this.radiobuttonFloat32LittleEndian.Location = new System.Drawing.Point(6, 13);
            this.radiobuttonFloat32LittleEndian.Name = "radiobuttonFloat32LittleEndian";
            this.radiobuttonFloat32LittleEndian.Size = new System.Drawing.Size(70, 17);
            this.radiobuttonFloat32LittleEndian.TabIndex = 0;
            this.radiobuttonFloat32LittleEndian.TabStop = true;
            this.radiobuttonFloat32LittleEndian.Text = "Low-High";
            this.toolTip1.SetToolTip(this.radiobuttonFloat32LittleEndian, "First register is lower 16 bits of 32-bit float (most used)");
            this.radiobuttonFloat32LittleEndian.UseVisualStyleBackColor = true;
            // 
            // radiobuttonFloat32BigEndian
            // 
            this.radiobuttonFloat32BigEndian.AutoSize = true;
            this.radiobuttonFloat32BigEndian.Location = new System.Drawing.Point(82, 13);
            this.radiobuttonFloat32BigEndian.Name = "radiobuttonFloat32BigEndian";
            this.radiobuttonFloat32BigEndian.Size = new System.Drawing.Size(70, 17);
            this.radiobuttonFloat32BigEndian.TabIndex = 1;
            this.radiobuttonFloat32BigEndian.Text = "High-Low";
            this.toolTip1.SetToolTip(this.radiobuttonFloat32BigEndian, "First register is higher 16 bits of 32-bit float");
            this.radiobuttonFloat32BigEndian.UseVisualStyleBackColor = true;
            // 
            // numericSamplerate
            // 
            this.numericSamplerate.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericSamplerate.Location = new System.Drawing.Point(557, 18);
            this.numericSamplerate.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.numericSamplerate.Name = "numericSamplerate";
            this.numericSamplerate.Size = new System.Drawing.Size(87, 20);
            this.numericSamplerate.TabIndex = 7;
            this.toolTip1.SetToolTip(this.numericSamplerate, "Sample period in milliseconds");
            this.numericSamplerate.Value = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(463, 20);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(88, 13);
            this.label6.TabIndex = 6;
            this.label6.Text = "Sample rate (ms):";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(463, 46);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(98, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Holding registers #:";
            this.toolTip1.SetToolTip(this.label5, "Numbering convention of first holding register (usually 40001, 400000 or 400001)");
            // 
            // buttonOK
            // 
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Location = new System.Drawing.Point(250, 366);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 13;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(339, 366);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 14;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // textboxHoldingRegistersStart
            // 
            this.textboxHoldingRegistersStart.Location = new System.Drawing.Point(561, 43);
            this.textboxHoldingRegistersStart.Name = "textboxHoldingRegistersStart";
            this.textboxHoldingRegistersStart.ReadOnly = true;
            this.textboxHoldingRegistersStart.Size = new System.Drawing.Size(83, 20);
            this.textboxHoldingRegistersStart.TabIndex = 9;
            // 
            // groupBox5
            // 
            this.groupBox5.Location = new System.Drawing.Point(438, 12);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(10, 348);
            this.groupBox5.TabIndex = 16;
            this.groupBox5.TabStop = false;
            // 
            // numericInterFrameDelay
            // 
            this.numericInterFrameDelay.Location = new System.Drawing.Point(126, 344);
            this.numericInterFrameDelay.Maximum = new decimal(new int[] {
            9999999,
            0,
            0,
            0});
            this.numericInterFrameDelay.Name = "numericInterFrameDelay";
            this.numericInterFrameDelay.Size = new System.Drawing.Size(61, 20);
            this.numericInterFrameDelay.TabIndex = 11;
            this.toolTip1.SetToolTip(this.numericInterFrameDelay, "Delay between frames, in milliseconds, use 0 for default");
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
            // ConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(664, 405);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.textboxHoldingRegistersStart);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.numericSamplerate);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupboxModbusTcpServer);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.checkboxShowInfo);
            this.Controls.Add(this.groupboxConnection);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "ConfigForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Project configuration";
            this.groupboxConnection.ResumeLayout(false);
            this.groupboxConnection.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericMaximumFrameSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericResponseTimeout)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericSlaveAddress)).EndInit();
            this.groupBoxTcpInterface.ResumeLayout(false);
            this.groupBoxTcpInterface.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericIpPort)).EndInit();
            this.groupBoxSerialInterface.ResumeLayout(false);
            this.groupBoxSerialInterface.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericSerialEndOfMessageTimeout)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupboxModbusTcpServer.ResumeLayout(false);
            this.groupboxModbusTcpServer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericTcpServerPort)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericSamplerate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericInterFrameDelay)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupboxConnection;
        private System.Windows.Forms.NumericUpDown numericMaximumFrameSize;
        private System.Windows.Forms.NumericUpDown numericResponseTimeout;
        private System.Windows.Forms.NumericUpDown numericSlaveAddress;
        private System.Windows.Forms.GroupBox groupBoxTcpInterface;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox comboTcpConnectMode;
        private System.Windows.Forms.NumericUpDown numericIpPort;
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
        private System.Windows.Forms.ComboBox comboSerialPort;
        private System.Windows.Forms.Label labelConnection;
        private System.Windows.Forms.ComboBox comboConnection;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox checkboxShowInfo;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton radiobuttonInt32LittleEndian;
        private System.Windows.Forms.RadioButton radiobuttonInt32BigEndian;
        private System.Windows.Forms.GroupBox groupboxModbusTcpServer;
        private System.Windows.Forms.NumericUpDown numericTcpServerPort;
        private System.Windows.Forms.CheckBox checkboxTcpServerEnable;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton radiobuttonFloat32LittleEndian;
        private System.Windows.Forms.RadioButton radiobuttonFloat32BigEndian;
        private System.Windows.Forms.NumericUpDown numericSamplerate;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.RadioButton radiobuttonWriteFC16;
        private System.Windows.Forms.RadioButton radiobuttonWriteFCAuto;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.RadioButton radiobuttonWriteFC6;
        private System.Windows.Forms.RadioButton radiobuttonAsciiLittleEndian;
        private System.Windows.Forms.RadioButton radiobuttonAsciiBigEndian;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox textboxHoldingRegistersStart;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.NumericUpDown numericInterFrameDelay;
        private System.Windows.Forms.Label label15;

    }
}