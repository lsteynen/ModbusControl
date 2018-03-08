namespace ModbusControlSpace
{
    partial class DiagnosticsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DiagnosticsForm));
            this.label1 = new System.Windows.Forms.Label();
            this.lRwSuccessCount = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lIllegalAddressExceptionCount = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.lIllegalFunctionExceptionCount = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.lTcpClosedOnErrorCount = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.lTcpConnectionLostCount = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.lTcpConnectFailCount = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.lTcpConnectCount = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.lRespMbapErrorCount = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.lOnewireEchoErrorCount = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lResponseCrcErrorCount = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lResponseFormatErrorCount = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lNoResponseCount = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label37 = new System.Windows.Forms.Label();
            this.lBridgeErrorsCount = new System.Windows.Forms.Label();
            this.lBridgeQueriesReceivedCount = new System.Windows.Forms.Label();
            this.lBridgeOpenConnections = new System.Windows.Forms.Label();
            this.lBridgeConnectCount = new System.Windows.Forms.Label();
            this.lOtherExceptionCount = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.lGatewayDeviceFailedToRespondExceptionCount = new System.Windows.Forms.Label();
            this.lGatewayPathUnavailableExceptionCount = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lIllegalDataExceptionCount = new System.Windows.Forms.Label();
            this.buttonReset = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Read/write success:";
            // 
            // lRwSuccessCount
            // 
            this.lRwSuccessCount.AutoSize = true;
            this.lRwSuccessCount.Location = new System.Drawing.Point(205, 0);
            this.lRwSuccessCount.Name = "lRwSuccessCount";
            this.lRwSuccessCount.Size = new System.Drawing.Size(13, 13);
            this.lRwSuccessCount.TabIndex = 1;
            this.lRwSuccessCount.Text = "0";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 202F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.lIllegalAddressExceptionCount, 1, 11);
            this.tableLayoutPanel1.Controls.Add(this.label23, 0, 11);
            this.tableLayoutPanel1.Controls.Add(this.lIllegalFunctionExceptionCount, 1, 10);
            this.tableLayoutPanel1.Controls.Add(this.label21, 0, 10);
            this.tableLayoutPanel1.Controls.Add(this.lTcpClosedOnErrorCount, 1, 9);
            this.tableLayoutPanel1.Controls.Add(this.label19, 0, 9);
            this.tableLayoutPanel1.Controls.Add(this.lTcpConnectionLostCount, 1, 8);
            this.tableLayoutPanel1.Controls.Add(this.label17, 0, 8);
            this.tableLayoutPanel1.Controls.Add(this.lTcpConnectFailCount, 1, 7);
            this.tableLayoutPanel1.Controls.Add(this.label15, 0, 7);
            this.tableLayoutPanel1.Controls.Add(this.lTcpConnectCount, 1, 6);
            this.tableLayoutPanel1.Controls.Add(this.label13, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.lRespMbapErrorCount, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.label11, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.lOnewireEchoErrorCount, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.label9, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.lResponseCrcErrorCount, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.label7, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.lResponseFormatErrorCount, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lNoResponseCount, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lRwSuccessCount, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label37, 0, 19);
            this.tableLayoutPanel1.Controls.Add(this.lBridgeErrorsCount, 1, 19);
            this.tableLayoutPanel1.Controls.Add(this.lBridgeQueriesReceivedCount, 1, 18);
            this.tableLayoutPanel1.Controls.Add(this.lBridgeOpenConnections, 1, 17);
            this.tableLayoutPanel1.Controls.Add(this.lBridgeConnectCount, 1, 16);
            this.tableLayoutPanel1.Controls.Add(this.lOtherExceptionCount, 1, 15);
            this.tableLayoutPanel1.Controls.Add(this.label35, 0, 18);
            this.tableLayoutPanel1.Controls.Add(this.label33, 0, 17);
            this.tableLayoutPanel1.Controls.Add(this.label31, 0, 16);
            this.tableLayoutPanel1.Controls.Add(this.label29, 0, 15);
            this.tableLayoutPanel1.Controls.Add(this.label27, 0, 14);
            this.tableLayoutPanel1.Controls.Add(this.label25, 0, 13);
            this.tableLayoutPanel1.Controls.Add(this.lGatewayDeviceFailedToRespondExceptionCount, 1, 14);
            this.tableLayoutPanel1.Controls.Add(this.lGatewayPathUnavailableExceptionCount, 1, 13);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 12);
            this.tableLayoutPanel1.Controls.Add(this.lIllegalDataExceptionCount, 1, 12);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(6, 37);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 20;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(312, 326);
            this.tableLayoutPanel1.TabIndex = 22;
            // 
            // lIllegalAddressExceptionCount
            // 
            this.lIllegalAddressExceptionCount.AutoSize = true;
            this.lIllegalAddressExceptionCount.Location = new System.Drawing.Point(205, 176);
            this.lIllegalAddressExceptionCount.Name = "lIllegalAddressExceptionCount";
            this.lIllegalAddressExceptionCount.Size = new System.Drawing.Size(13, 13);
            this.lIllegalAddressExceptionCount.TabIndex = 23;
            this.lIllegalAddressExceptionCount.Text = "0";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(3, 176);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(131, 13);
            this.label23.TabIndex = 22;
            this.label23.Text = "Illegal address exceptions:";
            // 
            // lIllegalFunctionExceptionCount
            // 
            this.lIllegalFunctionExceptionCount.AutoSize = true;
            this.lIllegalFunctionExceptionCount.Location = new System.Drawing.Point(205, 160);
            this.lIllegalFunctionExceptionCount.Name = "lIllegalFunctionExceptionCount";
            this.lIllegalFunctionExceptionCount.Size = new System.Drawing.Size(13, 13);
            this.lIllegalFunctionExceptionCount.TabIndex = 21;
            this.lIllegalFunctionExceptionCount.Text = "0";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(3, 160);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(132, 13);
            this.label21.TabIndex = 20;
            this.label21.Text = "Illegal function exceptions:";
            // 
            // lTcpClosedOnErrorCount
            // 
            this.lTcpClosedOnErrorCount.AutoSize = true;
            this.lTcpClosedOnErrorCount.Location = new System.Drawing.Point(205, 144);
            this.lTcpClosedOnErrorCount.Name = "lTcpClosedOnErrorCount";
            this.lTcpClosedOnErrorCount.Size = new System.Drawing.Size(13, 13);
            this.lTcpClosedOnErrorCount.TabIndex = 19;
            this.lTcpClosedOnErrorCount.Text = "0";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(3, 144);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(154, 13);
            this.label19.TabIndex = 18;
            this.label19.Text = "TCP connection close on error:";
            // 
            // lTcpConnectionLostCount
            // 
            this.lTcpConnectionLostCount.AutoSize = true;
            this.lTcpConnectionLostCount.Location = new System.Drawing.Point(205, 128);
            this.lTcpConnectionLostCount.Name = "lTcpConnectionLostCount";
            this.lTcpConnectionLostCount.Size = new System.Drawing.Size(13, 13);
            this.lTcpConnectionLostCount.TabIndex = 17;
            this.lTcpConnectionLostCount.Text = "0";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(3, 128);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(139, 13);
            this.label17.TabIndex = 16;
            this.label17.Text = "TCP connection lost count: ";
            // 
            // lTcpConnectFailCount
            // 
            this.lTcpConnectFailCount.AutoSize = true;
            this.lTcpConnectFailCount.Location = new System.Drawing.Point(205, 112);
            this.lTcpConnectFailCount.Name = "lTcpConnectFailCount";
            this.lTcpConnectFailCount.Size = new System.Drawing.Size(13, 13);
            this.lTcpConnectFailCount.TabIndex = 15;
            this.lTcpConnectFailCount.Text = "0";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(3, 112);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(119, 13);
            this.label15.TabIndex = 14;
            this.label15.Text = "TCP connect fail count:";
            // 
            // lTcpConnectCount
            // 
            this.lTcpConnectCount.AutoSize = true;
            this.lTcpConnectCount.Location = new System.Drawing.Point(205, 96);
            this.lTcpConnectCount.Name = "lTcpConnectCount";
            this.lTcpConnectCount.Size = new System.Drawing.Size(13, 13);
            this.lTcpConnectCount.TabIndex = 13;
            this.lTcpConnectCount.Text = "0";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(3, 96);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(103, 13);
            this.label13.TabIndex = 12;
            this.label13.Text = "TCP connect count:";
            // 
            // lRespMbapErrorCount
            // 
            this.lRespMbapErrorCount.AutoSize = true;
            this.lRespMbapErrorCount.Location = new System.Drawing.Point(205, 80);
            this.lRespMbapErrorCount.Name = "lRespMbapErrorCount";
            this.lRespMbapErrorCount.Size = new System.Drawing.Size(13, 13);
            this.lRespMbapErrorCount.TabIndex = 11;
            this.lRespMbapErrorCount.Text = "0";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(3, 80);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(156, 13);
            this.label11.TabIndex = 10;
            this.label11.Text = "Response MBAP-header errors:";
            // 
            // lOnewireEchoErrorCount
            // 
            this.lOnewireEchoErrorCount.AutoSize = true;
            this.lOnewireEchoErrorCount.Location = new System.Drawing.Point(205, 64);
            this.lOnewireEchoErrorCount.Name = "lOnewireEchoErrorCount";
            this.lOnewireEchoErrorCount.Size = new System.Drawing.Size(13, 13);
            this.lOnewireEchoErrorCount.TabIndex = 9;
            this.lOnewireEchoErrorCount.Text = "0";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(3, 64);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(105, 13);
            this.label9.TabIndex = 8;
            this.label9.Text = "Onewire echo errors:";
            // 
            // lResponseCrcErrorCount
            // 
            this.lResponseCrcErrorCount.AutoSize = true;
            this.lResponseCrcErrorCount.Location = new System.Drawing.Point(205, 48);
            this.lResponseCrcErrorCount.Name = "lResponseCrcErrorCount";
            this.lResponseCrcErrorCount.Size = new System.Drawing.Size(13, 13);
            this.lResponseCrcErrorCount.TabIndex = 7;
            this.lResponseCrcErrorCount.Text = "0";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 48);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(112, 13);
            this.label7.TabIndex = 6;
            this.label7.Text = "Response CRC errors:";
            // 
            // lResponseFormatErrorCount
            // 
            this.lResponseFormatErrorCount.AutoSize = true;
            this.lResponseFormatErrorCount.Location = new System.Drawing.Point(205, 32);
            this.lResponseFormatErrorCount.Name = "lResponseFormatErrorCount";
            this.lResponseFormatErrorCount.Size = new System.Drawing.Size(13, 13);
            this.lResponseFormatErrorCount.TabIndex = 5;
            this.lResponseFormatErrorCount.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "No response:";
            // 
            // lNoResponseCount
            // 
            this.lNoResponseCount.AutoSize = true;
            this.lNoResponseCount.Location = new System.Drawing.Point(205, 16);
            this.lNoResponseCount.Name = "lNoResponseCount";
            this.lNoResponseCount.Size = new System.Drawing.Size(13, 13);
            this.lNoResponseCount.TabIndex = 3;
            this.lNoResponseCount.Text = "0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 32);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(119, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Response format errors:";
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Location = new System.Drawing.Point(3, 304);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(135, 13);
            this.label37.TabIndex = 36;
            this.label37.Text = "Modbus/TCP bridge errors:";
            // 
            // lBridgeErrorsCount
            // 
            this.lBridgeErrorsCount.AutoSize = true;
            this.lBridgeErrorsCount.Location = new System.Drawing.Point(205, 304);
            this.lBridgeErrorsCount.Name = "lBridgeErrorsCount";
            this.lBridgeErrorsCount.Size = new System.Drawing.Size(13, 13);
            this.lBridgeErrorsCount.TabIndex = 37;
            this.lBridgeErrorsCount.Text = "0";
            // 
            // lBridgeQueriesReceivedCount
            // 
            this.lBridgeQueriesReceivedCount.AutoSize = true;
            this.lBridgeQueriesReceivedCount.Location = new System.Drawing.Point(205, 288);
            this.lBridgeQueriesReceivedCount.Name = "lBridgeQueriesReceivedCount";
            this.lBridgeQueriesReceivedCount.Size = new System.Drawing.Size(13, 13);
            this.lBridgeQueriesReceivedCount.TabIndex = 35;
            this.lBridgeQueriesReceivedCount.Text = "0";
            // 
            // lBridgeOpenConnections
            // 
            this.lBridgeOpenConnections.AutoSize = true;
            this.lBridgeOpenConnections.Location = new System.Drawing.Point(205, 272);
            this.lBridgeOpenConnections.Name = "lBridgeOpenConnections";
            this.lBridgeOpenConnections.Size = new System.Drawing.Size(13, 13);
            this.lBridgeOpenConnections.TabIndex = 33;
            this.lBridgeOpenConnections.Text = "0";
            // 
            // lBridgeConnectCount
            // 
            this.lBridgeConnectCount.AutoSize = true;
            this.lBridgeConnectCount.Location = new System.Drawing.Point(205, 256);
            this.lBridgeConnectCount.Name = "lBridgeConnectCount";
            this.lBridgeConnectCount.Size = new System.Drawing.Size(13, 13);
            this.lBridgeConnectCount.TabIndex = 31;
            this.lBridgeConnectCount.Text = "0";
            // 
            // lOtherExceptionCount
            // 
            this.lOtherExceptionCount.AutoSize = true;
            this.lOtherExceptionCount.Location = new System.Drawing.Point(205, 240);
            this.lOtherExceptionCount.Name = "lOtherExceptionCount";
            this.lOtherExceptionCount.Size = new System.Drawing.Size(13, 13);
            this.lOtherExceptionCount.TabIndex = 29;
            this.lOtherExceptionCount.Text = "0";
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Location = new System.Drawing.Point(3, 288);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(143, 13);
            this.label35.TabIndex = 34;
            this.label35.Text = "Modbus/TCP bridge queries:";
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(3, 272);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(194, 13);
            this.label33.TabIndex = 32;
            this.label33.Text = "Modbus/TCP bridge open connections:";
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(3, 256);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(153, 13);
            this.label31.TabIndex = 30;
            this.label31.Text = "Modbus/TCP bridge connects:";
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(3, 240);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(137, 13);
            this.label29.TabIndex = 28;
            this.label29.Text = "Received other exceptions:";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(3, 224);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(188, 13);
            this.label27.TabIndex = 26;
            this.label27.Text = "Gateway dev no response exceptions:";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(3, 208);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(167, 13);
            this.label25.TabIndex = 24;
            this.label25.Text = "Gateway path unavail exceptions:";
            // 
            // lGatewayDeviceFailedToRespondExceptionCount
            // 
            this.lGatewayDeviceFailedToRespondExceptionCount.AutoSize = true;
            this.lGatewayDeviceFailedToRespondExceptionCount.Location = new System.Drawing.Point(205, 224);
            this.lGatewayDeviceFailedToRespondExceptionCount.Name = "lGatewayDeviceFailedToRespondExceptionCount";
            this.lGatewayDeviceFailedToRespondExceptionCount.Size = new System.Drawing.Size(13, 13);
            this.lGatewayDeviceFailedToRespondExceptionCount.TabIndex = 27;
            this.lGatewayDeviceFailedToRespondExceptionCount.Text = "0";
            // 
            // lGatewayPathUnavailableExceptionCount
            // 
            this.lGatewayPathUnavailableExceptionCount.AutoSize = true;
            this.lGatewayPathUnavailableExceptionCount.Location = new System.Drawing.Point(205, 208);
            this.lGatewayPathUnavailableExceptionCount.Name = "lGatewayPathUnavailableExceptionCount";
            this.lGatewayPathUnavailableExceptionCount.Size = new System.Drawing.Size(13, 13);
            this.lGatewayPathUnavailableExceptionCount.TabIndex = 25;
            this.lGatewayPathUnavailableExceptionCount.Text = "0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 192);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(115, 13);
            this.label2.TabIndex = 38;
            this.label2.Text = "Illegal data exceptions:";
            // 
            // lIllegalDataExceptionCount
            // 
            this.lIllegalDataExceptionCount.AutoSize = true;
            this.lIllegalDataExceptionCount.Location = new System.Drawing.Point(205, 192);
            this.lIllegalDataExceptionCount.Name = "lIllegalDataExceptionCount";
            this.lIllegalDataExceptionCount.Size = new System.Drawing.Size(13, 13);
            this.lIllegalDataExceptionCount.TabIndex = 39;
            this.lIllegalDataExceptionCount.Text = "0";
            // 
            // buttonReset
            // 
            this.buttonReset.Location = new System.Drawing.Point(11, 8);
            this.buttonReset.Name = "buttonReset";
            this.buttonReset.Size = new System.Drawing.Size(75, 23);
            this.buttonReset.TabIndex = 23;
            this.buttonReset.Text = "Reset";
            this.buttonReset.UseVisualStyleBackColor = true;
            this.buttonReset.Click += new System.EventHandler(this.buttonReset_Click);
            // 
            // DiagnosticsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(282, 381);
            this.Controls.Add(this.buttonReset);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DiagnosticsForm";
            this.Text = "ModbusControl diagnostics counters";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lRwSuccessCount;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lTcpConnectFailCount;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label lTcpConnectCount;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label lRespMbapErrorCount;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lOnewireEchoErrorCount;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lResponseCrcErrorCount;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lResponseFormatErrorCount;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lNoResponseCount;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lIllegalAddressExceptionCount;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label lIllegalFunctionExceptionCount;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label lTcpClosedOnErrorCount;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label lTcpConnectionLostCount;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Button buttonReset;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.Label lBridgeErrorsCount;
        private System.Windows.Forms.Label lBridgeQueriesReceivedCount;
        private System.Windows.Forms.Label lBridgeOpenConnections;
        private System.Windows.Forms.Label lBridgeConnectCount;
        private System.Windows.Forms.Label lOtherExceptionCount;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label lGatewayDeviceFailedToRespondExceptionCount;
        private System.Windows.Forms.Label lGatewayPathUnavailableExceptionCount;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lIllegalDataExceptionCount;
    }
}