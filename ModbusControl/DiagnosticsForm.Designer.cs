namespace ModbusControl
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
            label1 = new Label();
            lRwSuccessCount = new Label();
            tableLayoutPanel1 = new TableLayoutPanel();
            label9 = new Label();
            label5 = new Label();
            lResponseProtocolErrorCount = new Label();
            label3 = new Label();
            lNoResponseCount = new Label();
            lOnewireEchoErrorCount = new Label();
            label37 = new Label();
            label35 = new Label();
            label33 = new Label();
            label31 = new Label();
            label29 = new Label();
            label27 = new Label();
            label25 = new Label();
            lBridgeErrorsCount = new Label();
            lBridgeQueriesReceivedCount = new Label();
            lBridgeOpenConnections = new Label();
            lBridgeConnectCount = new Label();
            lOtherExceptionCount = new Label();
            lGatewayDeviceFailedToRespondExceptionCount = new Label();
            label2 = new Label();
            label23 = new Label();
            label21 = new Label();
            lGatewayPathUnavailableExceptionCount = new Label();
            lIllegalDataExceptionCount = new Label();
            lIllegalAddressExceptionCount = new Label();
            label19 = new Label();
            label17 = new Label();
            label15 = new Label();
            label13 = new Label();
            lIllegalFunctionExceptionCount = new Label();
            lTcpClosedOnErrorCount = new Label();
            lTcpConnectionLostCount = new Label();
            lTcpConnectFailCount = new Label();
            label4 = new Label();
            label11 = new Label();
            label7 = new Label();
            lTcpConnectCount = new Label();
            lResponseDataErrorCount = new Label();
            lRespMbapErrorCount = new Label();
            label50 = new Label();
            lResponseCrcErrorCount = new Label();
            lResponseShortAndCrcErrorCount = new Label();
            buttonReset = new Button();
            panel1 = new Panel();
            tableLayoutPanel1.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(3, 0);
            label1.Margin = new Padding(3, 0, 3, 2);
            label1.Name = "label1";
            label1.Size = new Size(105, 13);
            label1.TabIndex = 0;
            label1.Text = "Read/write success:";
            // 
            // lRwSuccessCount
            // 
            lRwSuccessCount.AutoSize = true;
            lRwSuccessCount.Dock = DockStyle.Right;
            lRwSuccessCount.Location = new Point(263, 0);
            lRwSuccessCount.Name = "lRwSuccessCount";
            lRwSuccessCount.Size = new Size(13, 15);
            lRwSuccessCount.TabIndex = 1;
            lRwSuccessCount.Text = "0";
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.Controls.Add(label1, 0, 0);
            tableLayoutPanel1.Controls.Add(lRwSuccessCount, 1, 0);
            tableLayoutPanel1.Controls.Add(label9, 0, 1);
            tableLayoutPanel1.Controls.Add(label5, 0, 3);
            tableLayoutPanel1.Controls.Add(lResponseProtocolErrorCount, 1, 3);
            tableLayoutPanel1.Controls.Add(label3, 0, 2);
            tableLayoutPanel1.Controls.Add(lNoResponseCount, 1, 2);
            tableLayoutPanel1.Controls.Add(lOnewireEchoErrorCount, 1, 1);
            tableLayoutPanel1.Controls.Add(label37, 0, 21);
            tableLayoutPanel1.Controls.Add(label35, 0, 20);
            tableLayoutPanel1.Controls.Add(label33, 0, 19);
            tableLayoutPanel1.Controls.Add(label31, 0, 18);
            tableLayoutPanel1.Controls.Add(label29, 0, 17);
            tableLayoutPanel1.Controls.Add(label27, 0, 16);
            tableLayoutPanel1.Controls.Add(label25, 0, 15);
            tableLayoutPanel1.Controls.Add(lBridgeErrorsCount, 1, 21);
            tableLayoutPanel1.Controls.Add(lBridgeQueriesReceivedCount, 1, 20);
            tableLayoutPanel1.Controls.Add(lBridgeOpenConnections, 1, 19);
            tableLayoutPanel1.Controls.Add(lBridgeConnectCount, 1, 18);
            tableLayoutPanel1.Controls.Add(lOtherExceptionCount, 1, 17);
            tableLayoutPanel1.Controls.Add(lGatewayDeviceFailedToRespondExceptionCount, 1, 16);
            tableLayoutPanel1.Controls.Add(label2, 0, 14);
            tableLayoutPanel1.Controls.Add(label23, 0, 13);
            tableLayoutPanel1.Controls.Add(label21, 0, 12);
            tableLayoutPanel1.Controls.Add(lGatewayPathUnavailableExceptionCount, 1, 15);
            tableLayoutPanel1.Controls.Add(lIllegalDataExceptionCount, 1, 14);
            tableLayoutPanel1.Controls.Add(lIllegalAddressExceptionCount, 1, 13);
            tableLayoutPanel1.Controls.Add(label19, 0, 11);
            tableLayoutPanel1.Controls.Add(label17, 0, 10);
            tableLayoutPanel1.Controls.Add(label15, 0, 9);
            tableLayoutPanel1.Controls.Add(label13, 0, 8);
            tableLayoutPanel1.Controls.Add(lIllegalFunctionExceptionCount, 1, 12);
            tableLayoutPanel1.Controls.Add(lTcpClosedOnErrorCount, 1, 11);
            tableLayoutPanel1.Controls.Add(lTcpConnectionLostCount, 1, 10);
            tableLayoutPanel1.Controls.Add(lTcpConnectFailCount, 1, 9);
            tableLayoutPanel1.Controls.Add(label4, 0, 7);
            tableLayoutPanel1.Controls.Add(label11, 0, 6);
            tableLayoutPanel1.Controls.Add(label7, 0, 5);
            tableLayoutPanel1.Controls.Add(lTcpConnectCount, 1, 8);
            tableLayoutPanel1.Controls.Add(lResponseDataErrorCount, 1, 7);
            tableLayoutPanel1.Controls.Add(lRespMbapErrorCount, 1, 6);
            tableLayoutPanel1.Controls.Add(label50, 0, 4);
            tableLayoutPanel1.Controls.Add(lResponseCrcErrorCount, 1, 5);
            tableLayoutPanel1.Controls.Add(lResponseShortAndCrcErrorCount, 1, 4);
            tableLayoutPanel1.Location = new Point(16, 12);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 22;
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.Size = new Size(279, 339);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(3, 15);
            label9.Margin = new Padding(3, 0, 3, 2);
            label9.Name = "label9";
            label9.Size = new Size(105, 13);
            label9.TabIndex = 2;
            label9.Text = "Onewire echo errors:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(3, 45);
            label5.Margin = new Padding(3, 0, 3, 2);
            label5.Name = "label5";
            label5.Size = new Size(128, 13);
            label5.TabIndex = 6;
            label5.Text = "Response protocol errors:";
            // 
            // lResponseProtocolErrorCount
            // 
            lResponseProtocolErrorCount.AutoSize = true;
            lResponseProtocolErrorCount.Dock = DockStyle.Right;
            lResponseProtocolErrorCount.Location = new Point(263, 45);
            lResponseProtocolErrorCount.Name = "lResponseProtocolErrorCount";
            lResponseProtocolErrorCount.Size = new Size(13, 15);
            lResponseProtocolErrorCount.TabIndex = 7;
            lResponseProtocolErrorCount.Text = "0";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(3, 30);
            label3.Margin = new Padding(3, 0, 3, 2);
            label3.Name = "label3";
            label3.Size = new Size(70, 13);
            label3.TabIndex = 4;
            label3.Text = "No response:";
            // 
            // lNoResponseCount
            // 
            lNoResponseCount.AutoSize = true;
            lNoResponseCount.Dock = DockStyle.Right;
            lNoResponseCount.Location = new Point(263, 30);
            lNoResponseCount.Name = "lNoResponseCount";
            lNoResponseCount.Size = new Size(13, 15);
            lNoResponseCount.TabIndex = 5;
            lNoResponseCount.Text = "0";
            // 
            // lOnewireEchoErrorCount
            // 
            lOnewireEchoErrorCount.AutoSize = true;
            lOnewireEchoErrorCount.Dock = DockStyle.Right;
            lOnewireEchoErrorCount.Location = new Point(263, 15);
            lOnewireEchoErrorCount.Name = "lOnewireEchoErrorCount";
            lOnewireEchoErrorCount.Size = new Size(13, 15);
            lOnewireEchoErrorCount.TabIndex = 3;
            lOnewireEchoErrorCount.Text = "0";
            // 
            // label37
            // 
            label37.AutoSize = true;
            label37.Location = new Point(3, 315);
            label37.Margin = new Padding(3, 0, 3, 2);
            label37.Name = "label37";
            label37.Size = new Size(135, 13);
            label37.TabIndex = 42;
            label37.Text = "Modbus/TCP bridge errors:";
            // 
            // label35
            // 
            label35.AutoSize = true;
            label35.Location = new Point(3, 300);
            label35.Margin = new Padding(3, 0, 3, 2);
            label35.Name = "label35";
            label35.Size = new Size(143, 13);
            label35.TabIndex = 40;
            label35.Text = "Modbus/TCP bridge queries:";
            // 
            // label33
            // 
            label33.AutoSize = true;
            label33.Location = new Point(3, 285);
            label33.Margin = new Padding(3, 0, 3, 2);
            label33.Name = "label33";
            label33.Size = new Size(194, 13);
            label33.TabIndex = 38;
            label33.Text = "Modbus/TCP bridge open connections:";
            // 
            // label31
            // 
            label31.AutoSize = true;
            label31.Location = new Point(3, 270);
            label31.Margin = new Padding(3, 0, 3, 2);
            label31.Name = "label31";
            label31.Size = new Size(153, 13);
            label31.TabIndex = 36;
            label31.Text = "Modbus/TCP bridge connects:";
            // 
            // label29
            // 
            label29.AutoSize = true;
            label29.Location = new Point(3, 255);
            label29.Margin = new Padding(3, 0, 3, 2);
            label29.Name = "label29";
            label29.Size = new Size(137, 13);
            label29.TabIndex = 34;
            label29.Text = "Received other exceptions:";
            // 
            // label27
            // 
            label27.AutoSize = true;
            label27.Location = new Point(3, 240);
            label27.Margin = new Padding(3, 0, 3, 2);
            label27.Name = "label27";
            label27.Size = new Size(188, 13);
            label27.TabIndex = 32;
            label27.Text = "Gateway dev no response exceptions:";
            // 
            // label25
            // 
            label25.AutoSize = true;
            label25.Location = new Point(3, 225);
            label25.Margin = new Padding(3, 0, 3, 2);
            label25.Name = "label25";
            label25.Size = new Size(167, 13);
            label25.TabIndex = 30;
            label25.Text = "Gateway path unavail exceptions:";
            // 
            // lBridgeErrorsCount
            // 
            lBridgeErrorsCount.AutoSize = true;
            lBridgeErrorsCount.Dock = DockStyle.Right;
            lBridgeErrorsCount.Location = new Point(263, 315);
            lBridgeErrorsCount.Name = "lBridgeErrorsCount";
            lBridgeErrorsCount.Size = new Size(13, 24);
            lBridgeErrorsCount.TabIndex = 43;
            lBridgeErrorsCount.Text = "0";
            // 
            // lBridgeQueriesReceivedCount
            // 
            lBridgeQueriesReceivedCount.AutoSize = true;
            lBridgeQueriesReceivedCount.Dock = DockStyle.Right;
            lBridgeQueriesReceivedCount.Location = new Point(263, 300);
            lBridgeQueriesReceivedCount.Name = "lBridgeQueriesReceivedCount";
            lBridgeQueriesReceivedCount.Size = new Size(13, 15);
            lBridgeQueriesReceivedCount.TabIndex = 41;
            lBridgeQueriesReceivedCount.Text = "0";
            // 
            // lBridgeOpenConnections
            // 
            lBridgeOpenConnections.AutoSize = true;
            lBridgeOpenConnections.Dock = DockStyle.Right;
            lBridgeOpenConnections.Location = new Point(263, 285);
            lBridgeOpenConnections.Name = "lBridgeOpenConnections";
            lBridgeOpenConnections.Size = new Size(13, 15);
            lBridgeOpenConnections.TabIndex = 39;
            lBridgeOpenConnections.Text = "0";
            // 
            // lBridgeConnectCount
            // 
            lBridgeConnectCount.AutoSize = true;
            lBridgeConnectCount.Dock = DockStyle.Right;
            lBridgeConnectCount.Location = new Point(263, 270);
            lBridgeConnectCount.Name = "lBridgeConnectCount";
            lBridgeConnectCount.Size = new Size(13, 15);
            lBridgeConnectCount.TabIndex = 37;
            lBridgeConnectCount.Text = "0";
            // 
            // lOtherExceptionCount
            // 
            lOtherExceptionCount.AutoSize = true;
            lOtherExceptionCount.Dock = DockStyle.Right;
            lOtherExceptionCount.Location = new Point(263, 255);
            lOtherExceptionCount.Name = "lOtherExceptionCount";
            lOtherExceptionCount.Size = new Size(13, 15);
            lOtherExceptionCount.TabIndex = 35;
            lOtherExceptionCount.Text = "0";
            // 
            // lGatewayDeviceFailedToRespondExceptionCount
            // 
            lGatewayDeviceFailedToRespondExceptionCount.AutoSize = true;
            lGatewayDeviceFailedToRespondExceptionCount.Dock = DockStyle.Right;
            lGatewayDeviceFailedToRespondExceptionCount.Location = new Point(263, 240);
            lGatewayDeviceFailedToRespondExceptionCount.Name = "lGatewayDeviceFailedToRespondExceptionCount";
            lGatewayDeviceFailedToRespondExceptionCount.Size = new Size(13, 15);
            lGatewayDeviceFailedToRespondExceptionCount.TabIndex = 33;
            lGatewayDeviceFailedToRespondExceptionCount.Text = "0";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(3, 210);
            label2.Margin = new Padding(3, 0, 3, 2);
            label2.Name = "label2";
            label2.Size = new Size(115, 13);
            label2.TabIndex = 28;
            label2.Text = "Illegal data exceptions:";
            // 
            // label23
            // 
            label23.AutoSize = true;
            label23.Location = new Point(3, 195);
            label23.Margin = new Padding(3, 0, 3, 2);
            label23.Name = "label23";
            label23.Size = new Size(131, 13);
            label23.TabIndex = 26;
            label23.Text = "Illegal address exceptions:";
            // 
            // label21
            // 
            label21.AutoSize = true;
            label21.Location = new Point(3, 180);
            label21.Margin = new Padding(3, 0, 3, 2);
            label21.Name = "label21";
            label21.Size = new Size(132, 13);
            label21.TabIndex = 24;
            label21.Text = "Illegal function exceptions:";
            // 
            // lGatewayPathUnavailableExceptionCount
            // 
            lGatewayPathUnavailableExceptionCount.AutoSize = true;
            lGatewayPathUnavailableExceptionCount.Dock = DockStyle.Right;
            lGatewayPathUnavailableExceptionCount.Location = new Point(263, 225);
            lGatewayPathUnavailableExceptionCount.Name = "lGatewayPathUnavailableExceptionCount";
            lGatewayPathUnavailableExceptionCount.Size = new Size(13, 15);
            lGatewayPathUnavailableExceptionCount.TabIndex = 31;
            lGatewayPathUnavailableExceptionCount.Text = "0";
            // 
            // lIllegalDataExceptionCount
            // 
            lIllegalDataExceptionCount.AutoSize = true;
            lIllegalDataExceptionCount.Dock = DockStyle.Right;
            lIllegalDataExceptionCount.Location = new Point(263, 210);
            lIllegalDataExceptionCount.Name = "lIllegalDataExceptionCount";
            lIllegalDataExceptionCount.Size = new Size(13, 15);
            lIllegalDataExceptionCount.TabIndex = 29;
            lIllegalDataExceptionCount.Text = "0";
            // 
            // lIllegalAddressExceptionCount
            // 
            lIllegalAddressExceptionCount.AutoSize = true;
            lIllegalAddressExceptionCount.Dock = DockStyle.Right;
            lIllegalAddressExceptionCount.Location = new Point(263, 195);
            lIllegalAddressExceptionCount.Name = "lIllegalAddressExceptionCount";
            lIllegalAddressExceptionCount.Size = new Size(13, 15);
            lIllegalAddressExceptionCount.TabIndex = 27;
            lIllegalAddressExceptionCount.Text = "0";
            // 
            // label19
            // 
            label19.AutoSize = true;
            label19.Location = new Point(3, 165);
            label19.Margin = new Padding(3, 0, 3, 2);
            label19.Name = "label19";
            label19.Size = new Size(154, 13);
            label19.TabIndex = 22;
            label19.Text = "TCP connection close on error:";
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Location = new Point(3, 150);
            label17.Margin = new Padding(3, 0, 3, 2);
            label17.Name = "label17";
            label17.Size = new Size(139, 13);
            label17.TabIndex = 20;
            label17.Text = "TCP connection lost count: ";
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Location = new Point(3, 135);
            label15.Margin = new Padding(3, 0, 3, 2);
            label15.Name = "label15";
            label15.Size = new Size(119, 13);
            label15.TabIndex = 18;
            label15.Text = "TCP connect fail count:";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(3, 120);
            label13.Margin = new Padding(3, 0, 3, 2);
            label13.Name = "label13";
            label13.Size = new Size(103, 13);
            label13.TabIndex = 16;
            label13.Text = "TCP connect count:";
            // 
            // lIllegalFunctionExceptionCount
            // 
            lIllegalFunctionExceptionCount.AutoSize = true;
            lIllegalFunctionExceptionCount.Dock = DockStyle.Right;
            lIllegalFunctionExceptionCount.Location = new Point(263, 180);
            lIllegalFunctionExceptionCount.Name = "lIllegalFunctionExceptionCount";
            lIllegalFunctionExceptionCount.Size = new Size(13, 15);
            lIllegalFunctionExceptionCount.TabIndex = 25;
            lIllegalFunctionExceptionCount.Text = "0";
            // 
            // lTcpClosedOnErrorCount
            // 
            lTcpClosedOnErrorCount.AutoSize = true;
            lTcpClosedOnErrorCount.Dock = DockStyle.Right;
            lTcpClosedOnErrorCount.Location = new Point(263, 165);
            lTcpClosedOnErrorCount.Name = "lTcpClosedOnErrorCount";
            lTcpClosedOnErrorCount.Size = new Size(13, 15);
            lTcpClosedOnErrorCount.TabIndex = 23;
            lTcpClosedOnErrorCount.Text = "0";
            // 
            // lTcpConnectionLostCount
            // 
            lTcpConnectionLostCount.AutoSize = true;
            lTcpConnectionLostCount.Dock = DockStyle.Right;
            lTcpConnectionLostCount.Location = new Point(263, 150);
            lTcpConnectionLostCount.Name = "lTcpConnectionLostCount";
            lTcpConnectionLostCount.Size = new Size(13, 15);
            lTcpConnectionLostCount.TabIndex = 21;
            lTcpConnectionLostCount.Text = "0";
            // 
            // lTcpConnectFailCount
            // 
            lTcpConnectFailCount.AutoSize = true;
            lTcpConnectFailCount.Dock = DockStyle.Right;
            lTcpConnectFailCount.Location = new Point(263, 135);
            lTcpConnectFailCount.Name = "lTcpConnectFailCount";
            lTcpConnectFailCount.Size = new Size(13, 15);
            lTcpConnectFailCount.TabIndex = 19;
            lTcpConnectFailCount.Text = "0";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(3, 105);
            label4.Margin = new Padding(3, 0, 3, 2);
            label4.Name = "label4";
            label4.Size = new Size(106, 13);
            label4.TabIndex = 14;
            label4.Text = "Reponse data errors:";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(3, 90);
            label11.Margin = new Padding(3, 0, 3, 2);
            label11.Name = "label11";
            label11.Size = new Size(156, 13);
            label11.TabIndex = 12;
            label11.Text = "Response MBAP-header errors:";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(3, 75);
            label7.Margin = new Padding(3, 0, 3, 2);
            label7.Name = "label7";
            label7.Size = new Size(112, 13);
            label7.TabIndex = 10;
            label7.Text = "Response CRC errors:";
            // 
            // lTcpConnectCount
            // 
            lTcpConnectCount.AutoSize = true;
            lTcpConnectCount.Dock = DockStyle.Right;
            lTcpConnectCount.Location = new Point(263, 120);
            lTcpConnectCount.Name = "lTcpConnectCount";
            lTcpConnectCount.Size = new Size(13, 15);
            lTcpConnectCount.TabIndex = 17;
            lTcpConnectCount.Text = "0";
            // 
            // lResponseDataErrorCount
            // 
            lResponseDataErrorCount.AutoSize = true;
            lResponseDataErrorCount.Dock = DockStyle.Right;
            lResponseDataErrorCount.Location = new Point(263, 105);
            lResponseDataErrorCount.Name = "lResponseDataErrorCount";
            lResponseDataErrorCount.Size = new Size(13, 15);
            lResponseDataErrorCount.TabIndex = 15;
            lResponseDataErrorCount.Text = "0";
            // 
            // lRespMbapErrorCount
            // 
            lRespMbapErrorCount.AutoSize = true;
            lRespMbapErrorCount.Dock = DockStyle.Right;
            lRespMbapErrorCount.Location = new Point(263, 90);
            lRespMbapErrorCount.Name = "lRespMbapErrorCount";
            lRespMbapErrorCount.Size = new Size(13, 15);
            lRespMbapErrorCount.TabIndex = 13;
            lRespMbapErrorCount.Text = "0";
            // 
            // label50
            // 
            label50.AutoSize = true;
            label50.Location = new Point(3, 60);
            label50.Margin = new Padding(3, 0, 3, 2);
            label50.Name = "label50";
            label50.Size = new Size(177, 13);
            label50.TabIndex = 8;
            label50.Text = "Response too short and CRC errors:";
            // 
            // lResponseCrcErrorCount
            // 
            lResponseCrcErrorCount.AutoSize = true;
            lResponseCrcErrorCount.Dock = DockStyle.Right;
            lResponseCrcErrorCount.Location = new Point(263, 75);
            lResponseCrcErrorCount.Name = "lResponseCrcErrorCount";
            lResponseCrcErrorCount.Size = new Size(13, 15);
            lResponseCrcErrorCount.TabIndex = 11;
            lResponseCrcErrorCount.Text = "0";
            // 
            // lResponseShortAndCrcErrorCount
            // 
            lResponseShortAndCrcErrorCount.AutoSize = true;
            lResponseShortAndCrcErrorCount.Dock = DockStyle.Right;
            lResponseShortAndCrcErrorCount.Location = new Point(263, 60);
            lResponseShortAndCrcErrorCount.Name = "lResponseShortAndCrcErrorCount";
            lResponseShortAndCrcErrorCount.Size = new Size(13, 15);
            lResponseShortAndCrcErrorCount.TabIndex = 9;
            lResponseShortAndCrcErrorCount.Text = "0";
            // 
            // buttonReset
            // 
            buttonReset.Anchor = AnchorStyles.Top;
            buttonReset.AutoSize = true;
            buttonReset.Location = new Point(104, 3);
            buttonReset.Name = "buttonReset";
            buttonReset.Size = new Size(75, 25);
            buttonReset.TabIndex = 1;
            buttonReset.Text = "Reset";
            buttonReset.UseVisualStyleBackColor = true;
            buttonReset.Click += buttonReset_Click;
            // 
            // panel1
            // 
            panel1.Controls.Add(buttonReset);
            panel1.Location = new Point(13, 357);
            panel1.Name = "panel1";
            panel1.Size = new Size(282, 32);
            panel1.TabIndex = 2;
            // 
            // DiagnosticsForm
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(307, 396);
            Controls.Add(panel1);
            Controls.Add(tableLayoutPanel1);
            Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "DiagnosticsForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "ModbusControl diagnostics counters";
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);

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
        private System.Windows.Forms.Label lResponseProtocolErrorCount;
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
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label lGatewayDeviceFailedToRespondExceptionCount;
        private System.Windows.Forms.Label lGatewayPathUnavailableExceptionCount;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lIllegalDataExceptionCount;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lResponseDataErrorCount;
        private System.Windows.Forms.Label label50;
        private System.Windows.Forms.Label lResponseShortAndCrcErrorCount;
        private Panel panel1;
    }
}