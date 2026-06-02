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
            this.label1 = new System.Windows.Forms.Label();
            this.lRwSuccessCount = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label9 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lResponseProtocolErrorCount = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lNoResponseCount = new System.Windows.Forms.Label();
            this.lOnewireEchoErrorCount = new System.Windows.Forms.Label();
            this.label37 = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.lBridgeErrorsCount = new System.Windows.Forms.Label();
            this.lBridgeQueriesReceivedCount = new System.Windows.Forms.Label();
            this.lBridgeOpenConnections = new System.Windows.Forms.Label();
            this.lBridgeConnectCount = new System.Windows.Forms.Label();
            this.lOtherExceptionCount = new System.Windows.Forms.Label();
            this.lGatewayDeviceFailedToRespondExceptionCount = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.lGatewayPathUnavailableExceptionCount = new System.Windows.Forms.Label();
            this.lIllegalDataExceptionCount = new System.Windows.Forms.Label();
            this.lIllegalAddressExceptionCount = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.lIllegalFunctionExceptionCount = new System.Windows.Forms.Label();
            this.lTcpClosedOnErrorCount = new System.Windows.Forms.Label();
            this.lTcpConnectionLostCount = new System.Windows.Forms.Label();
            this.lTcpConnectFailCount = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lTcpConnectCount = new System.Windows.Forms.Label();
            this.lResponseDataErrorCount = new System.Windows.Forms.Label();
            this.lRespMbapErrorCount = new System.Windows.Forms.Label();
            this.label50 = new System.Windows.Forms.Label();
            this.lResponseCrcErrorCount = new System.Windows.Forms.Label();
            this.lResponseShortAndCrcErrorCount = new System.Windows.Forms.Label();
            this.buttonReset = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(3, 0, 3, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Read/write success:";
            // 
            // lRwSuccessCount
            // 
            this.lRwSuccessCount.AutoSize = true;
            this.lRwSuccessCount.Dock = System.Windows.Forms.DockStyle.Right;
            this.lRwSuccessCount.Location = new System.Drawing.Point(247, 0);
            this.lRwSuccessCount.Name = "lRwSuccessCount";
            this.lRwSuccessCount.Size = new System.Drawing.Size(13, 15);
            this.lRwSuccessCount.TabIndex = 1;
            this.lRwSuccessCount.Text = "0";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lRwSuccessCount, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label9, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.lResponseProtocolErrorCount, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.lNoResponseCount, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.lOnewireEchoErrorCount, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label37, 0, 21);
            this.tableLayoutPanel1.Controls.Add(this.label35, 0, 20);
            this.tableLayoutPanel1.Controls.Add(this.label33, 0, 19);
            this.tableLayoutPanel1.Controls.Add(this.label31, 0, 18);
            this.tableLayoutPanel1.Controls.Add(this.label29, 0, 17);
            this.tableLayoutPanel1.Controls.Add(this.label27, 0, 16);
            this.tableLayoutPanel1.Controls.Add(this.label25, 0, 15);
            this.tableLayoutPanel1.Controls.Add(this.lBridgeErrorsCount, 1, 21);
            this.tableLayoutPanel1.Controls.Add(this.lBridgeQueriesReceivedCount, 1, 20);
            this.tableLayoutPanel1.Controls.Add(this.lBridgeOpenConnections, 1, 19);
            this.tableLayoutPanel1.Controls.Add(this.lBridgeConnectCount, 1, 18);
            this.tableLayoutPanel1.Controls.Add(this.lOtherExceptionCount, 1, 17);
            this.tableLayoutPanel1.Controls.Add(this.lGatewayDeviceFailedToRespondExceptionCount, 1, 16);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 14);
            this.tableLayoutPanel1.Controls.Add(this.label23, 0, 13);
            this.tableLayoutPanel1.Controls.Add(this.label21, 0, 12);
            this.tableLayoutPanel1.Controls.Add(this.lGatewayPathUnavailableExceptionCount, 1, 15);
            this.tableLayoutPanel1.Controls.Add(this.lIllegalDataExceptionCount, 1, 14);
            this.tableLayoutPanel1.Controls.Add(this.lIllegalAddressExceptionCount, 1, 13);
            this.tableLayoutPanel1.Controls.Add(this.label19, 0, 11);
            this.tableLayoutPanel1.Controls.Add(this.label17, 0, 10);
            this.tableLayoutPanel1.Controls.Add(this.label15, 0, 9);
            this.tableLayoutPanel1.Controls.Add(this.label13, 0, 8);
            this.tableLayoutPanel1.Controls.Add(this.lIllegalFunctionExceptionCount, 1, 12);
            this.tableLayoutPanel1.Controls.Add(this.lTcpClosedOnErrorCount, 1, 11);
            this.tableLayoutPanel1.Controls.Add(this.lTcpConnectionLostCount, 1, 10);
            this.tableLayoutPanel1.Controls.Add(this.lTcpConnectFailCount, 1, 9);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 7);
            this.tableLayoutPanel1.Controls.Add(this.label11, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.label7, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.lTcpConnectCount, 1, 8);
            this.tableLayoutPanel1.Controls.Add(this.lResponseDataErrorCount, 1, 7);
            this.tableLayoutPanel1.Controls.Add(this.lRespMbapErrorCount, 1, 6);
            this.tableLayoutPanel1.Controls.Add(this.label50, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.lResponseCrcErrorCount, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.lResponseShortAndCrcErrorCount, 1, 4);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 12);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 22;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(263, 337);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(3, 15);
            this.label9.Margin = new System.Windows.Forms.Padding(3, 0, 3, 2);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(105, 13);
            this.label9.TabIndex = 2;
            this.label9.Text = "Onewire echo errors:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 45);
            this.label5.Margin = new System.Windows.Forms.Padding(3, 0, 3, 2);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(128, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Response protocol errors:";
            // 
            // lResponseProtocolErrorCount
            // 
            this.lResponseProtocolErrorCount.AutoSize = true;
            this.lResponseProtocolErrorCount.Dock = System.Windows.Forms.DockStyle.Right;
            this.lResponseProtocolErrorCount.Location = new System.Drawing.Point(247, 45);
            this.lResponseProtocolErrorCount.Name = "lResponseProtocolErrorCount";
            this.lResponseProtocolErrorCount.Size = new System.Drawing.Size(13, 15);
            this.lResponseProtocolErrorCount.TabIndex = 7;
            this.lResponseProtocolErrorCount.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 30);
            this.label3.Margin = new System.Windows.Forms.Padding(3, 0, 3, 2);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "No response:";
            // 
            // lNoResponseCount
            // 
            this.lNoResponseCount.AutoSize = true;
            this.lNoResponseCount.Dock = System.Windows.Forms.DockStyle.Right;
            this.lNoResponseCount.Location = new System.Drawing.Point(247, 30);
            this.lNoResponseCount.Name = "lNoResponseCount";
            this.lNoResponseCount.Size = new System.Drawing.Size(13, 15);
            this.lNoResponseCount.TabIndex = 5;
            this.lNoResponseCount.Text = "0";
            // 
            // lOnewireEchoErrorCount
            // 
            this.lOnewireEchoErrorCount.AutoSize = true;
            this.lOnewireEchoErrorCount.Dock = System.Windows.Forms.DockStyle.Right;
            this.lOnewireEchoErrorCount.Location = new System.Drawing.Point(247, 15);
            this.lOnewireEchoErrorCount.Name = "lOnewireEchoErrorCount";
            this.lOnewireEchoErrorCount.Size = new System.Drawing.Size(13, 15);
            this.lOnewireEchoErrorCount.TabIndex = 3;
            this.lOnewireEchoErrorCount.Text = "0";
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Location = new System.Drawing.Point(3, 315);
            this.label37.Margin = new System.Windows.Forms.Padding(3, 0, 3, 2);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(135, 13);
            this.label37.TabIndex = 42;
            this.label37.Text = "Modbus/TCP bridge errors:";
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Location = new System.Drawing.Point(3, 300);
            this.label35.Margin = new System.Windows.Forms.Padding(3, 0, 3, 2);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(143, 13);
            this.label35.TabIndex = 40;
            this.label35.Text = "Modbus/TCP bridge queries:";
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(3, 285);
            this.label33.Margin = new System.Windows.Forms.Padding(3, 0, 3, 2);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(194, 13);
            this.label33.TabIndex = 38;
            this.label33.Text = "Modbus/TCP bridge open connections:";
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(3, 270);
            this.label31.Margin = new System.Windows.Forms.Padding(3, 0, 3, 2);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(153, 13);
            this.label31.TabIndex = 36;
            this.label31.Text = "Modbus/TCP bridge connects:";
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(3, 255);
            this.label29.Margin = new System.Windows.Forms.Padding(3, 0, 3, 2);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(137, 13);
            this.label29.TabIndex = 34;
            this.label29.Text = "Received other exceptions:";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(3, 240);
            this.label27.Margin = new System.Windows.Forms.Padding(3, 0, 3, 2);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(188, 13);
            this.label27.TabIndex = 32;
            this.label27.Text = "Gateway dev no response exceptions:";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(3, 225);
            this.label25.Margin = new System.Windows.Forms.Padding(3, 0, 3, 2);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(167, 13);
            this.label25.TabIndex = 30;
            this.label25.Text = "Gateway path unavail exceptions:";
            // 
            // lBridgeErrorsCount
            // 
            this.lBridgeErrorsCount.AutoSize = true;
            this.lBridgeErrorsCount.Dock = System.Windows.Forms.DockStyle.Right;
            this.lBridgeErrorsCount.Location = new System.Drawing.Point(247, 315);
            this.lBridgeErrorsCount.Name = "lBridgeErrorsCount";
            this.lBridgeErrorsCount.Size = new System.Drawing.Size(13, 22);
            this.lBridgeErrorsCount.TabIndex = 43;
            this.lBridgeErrorsCount.Text = "0";
            // 
            // lBridgeQueriesReceivedCount
            // 
            this.lBridgeQueriesReceivedCount.AutoSize = true;
            this.lBridgeQueriesReceivedCount.Dock = System.Windows.Forms.DockStyle.Right;
            this.lBridgeQueriesReceivedCount.Location = new System.Drawing.Point(247, 300);
            this.lBridgeQueriesReceivedCount.Name = "lBridgeQueriesReceivedCount";
            this.lBridgeQueriesReceivedCount.Size = new System.Drawing.Size(13, 15);
            this.lBridgeQueriesReceivedCount.TabIndex = 41;
            this.lBridgeQueriesReceivedCount.Text = "0";
            // 
            // lBridgeOpenConnections
            // 
            this.lBridgeOpenConnections.AutoSize = true;
            this.lBridgeOpenConnections.Dock = System.Windows.Forms.DockStyle.Right;
            this.lBridgeOpenConnections.Location = new System.Drawing.Point(247, 285);
            this.lBridgeOpenConnections.Name = "lBridgeOpenConnections";
            this.lBridgeOpenConnections.Size = new System.Drawing.Size(13, 15);
            this.lBridgeOpenConnections.TabIndex = 39;
            this.lBridgeOpenConnections.Text = "0";
            // 
            // lBridgeConnectCount
            // 
            this.lBridgeConnectCount.AutoSize = true;
            this.lBridgeConnectCount.Dock = System.Windows.Forms.DockStyle.Right;
            this.lBridgeConnectCount.Location = new System.Drawing.Point(247, 270);
            this.lBridgeConnectCount.Name = "lBridgeConnectCount";
            this.lBridgeConnectCount.Size = new System.Drawing.Size(13, 15);
            this.lBridgeConnectCount.TabIndex = 37;
            this.lBridgeConnectCount.Text = "0";
            // 
            // lOtherExceptionCount
            // 
            this.lOtherExceptionCount.AutoSize = true;
            this.lOtherExceptionCount.Dock = System.Windows.Forms.DockStyle.Right;
            this.lOtherExceptionCount.Location = new System.Drawing.Point(247, 255);
            this.lOtherExceptionCount.Name = "lOtherExceptionCount";
            this.lOtherExceptionCount.Size = new System.Drawing.Size(13, 15);
            this.lOtherExceptionCount.TabIndex = 35;
            this.lOtherExceptionCount.Text = "0";
            // 
            // lGatewayDeviceFailedToRespondExceptionCount
            // 
            this.lGatewayDeviceFailedToRespondExceptionCount.AutoSize = true;
            this.lGatewayDeviceFailedToRespondExceptionCount.Dock = System.Windows.Forms.DockStyle.Right;
            this.lGatewayDeviceFailedToRespondExceptionCount.Location = new System.Drawing.Point(247, 240);
            this.lGatewayDeviceFailedToRespondExceptionCount.Name = "lGatewayDeviceFailedToRespondExceptionCount";
            this.lGatewayDeviceFailedToRespondExceptionCount.Size = new System.Drawing.Size(13, 15);
            this.lGatewayDeviceFailedToRespondExceptionCount.TabIndex = 33;
            this.lGatewayDeviceFailedToRespondExceptionCount.Text = "0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 210);
            this.label2.Margin = new System.Windows.Forms.Padding(3, 0, 3, 2);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(115, 13);
            this.label2.TabIndex = 28;
            this.label2.Text = "Illegal data exceptions:";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(3, 195);
            this.label23.Margin = new System.Windows.Forms.Padding(3, 0, 3, 2);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(131, 13);
            this.label23.TabIndex = 26;
            this.label23.Text = "Illegal address exceptions:";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(3, 180);
            this.label21.Margin = new System.Windows.Forms.Padding(3, 0, 3, 2);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(132, 13);
            this.label21.TabIndex = 24;
            this.label21.Text = "Illegal function exceptions:";
            // 
            // lGatewayPathUnavailableExceptionCount
            // 
            this.lGatewayPathUnavailableExceptionCount.AutoSize = true;
            this.lGatewayPathUnavailableExceptionCount.Dock = System.Windows.Forms.DockStyle.Right;
            this.lGatewayPathUnavailableExceptionCount.Location = new System.Drawing.Point(247, 225);
            this.lGatewayPathUnavailableExceptionCount.Name = "lGatewayPathUnavailableExceptionCount";
            this.lGatewayPathUnavailableExceptionCount.Size = new System.Drawing.Size(13, 15);
            this.lGatewayPathUnavailableExceptionCount.TabIndex = 31;
            this.lGatewayPathUnavailableExceptionCount.Text = "0";
            // 
            // lIllegalDataExceptionCount
            // 
            this.lIllegalDataExceptionCount.AutoSize = true;
            this.lIllegalDataExceptionCount.Dock = System.Windows.Forms.DockStyle.Right;
            this.lIllegalDataExceptionCount.Location = new System.Drawing.Point(247, 210);
            this.lIllegalDataExceptionCount.Name = "lIllegalDataExceptionCount";
            this.lIllegalDataExceptionCount.Size = new System.Drawing.Size(13, 15);
            this.lIllegalDataExceptionCount.TabIndex = 29;
            this.lIllegalDataExceptionCount.Text = "0";
            // 
            // lIllegalAddressExceptionCount
            // 
            this.lIllegalAddressExceptionCount.AutoSize = true;
            this.lIllegalAddressExceptionCount.Dock = System.Windows.Forms.DockStyle.Right;
            this.lIllegalAddressExceptionCount.Location = new System.Drawing.Point(247, 195);
            this.lIllegalAddressExceptionCount.Name = "lIllegalAddressExceptionCount";
            this.lIllegalAddressExceptionCount.Size = new System.Drawing.Size(13, 15);
            this.lIllegalAddressExceptionCount.TabIndex = 27;
            this.lIllegalAddressExceptionCount.Text = "0";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(3, 165);
            this.label19.Margin = new System.Windows.Forms.Padding(3, 0, 3, 2);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(154, 13);
            this.label19.TabIndex = 22;
            this.label19.Text = "TCP connection close on error:";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(3, 150);
            this.label17.Margin = new System.Windows.Forms.Padding(3, 0, 3, 2);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(139, 13);
            this.label17.TabIndex = 20;
            this.label17.Text = "TCP connection lost count: ";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(3, 135);
            this.label15.Margin = new System.Windows.Forms.Padding(3, 0, 3, 2);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(119, 13);
            this.label15.TabIndex = 18;
            this.label15.Text = "TCP connect fail count:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(3, 120);
            this.label13.Margin = new System.Windows.Forms.Padding(3, 0, 3, 2);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(103, 13);
            this.label13.TabIndex = 16;
            this.label13.Text = "TCP connect count:";
            // 
            // lIllegalFunctionExceptionCount
            // 
            this.lIllegalFunctionExceptionCount.AutoSize = true;
            this.lIllegalFunctionExceptionCount.Dock = System.Windows.Forms.DockStyle.Right;
            this.lIllegalFunctionExceptionCount.Location = new System.Drawing.Point(247, 180);
            this.lIllegalFunctionExceptionCount.Name = "lIllegalFunctionExceptionCount";
            this.lIllegalFunctionExceptionCount.Size = new System.Drawing.Size(13, 15);
            this.lIllegalFunctionExceptionCount.TabIndex = 25;
            this.lIllegalFunctionExceptionCount.Text = "0";
            // 
            // lTcpClosedOnErrorCount
            // 
            this.lTcpClosedOnErrorCount.AutoSize = true;
            this.lTcpClosedOnErrorCount.Dock = System.Windows.Forms.DockStyle.Right;
            this.lTcpClosedOnErrorCount.Location = new System.Drawing.Point(247, 165);
            this.lTcpClosedOnErrorCount.Name = "lTcpClosedOnErrorCount";
            this.lTcpClosedOnErrorCount.Size = new System.Drawing.Size(13, 15);
            this.lTcpClosedOnErrorCount.TabIndex = 23;
            this.lTcpClosedOnErrorCount.Text = "0";
            // 
            // lTcpConnectionLostCount
            // 
            this.lTcpConnectionLostCount.AutoSize = true;
            this.lTcpConnectionLostCount.Dock = System.Windows.Forms.DockStyle.Right;
            this.lTcpConnectionLostCount.Location = new System.Drawing.Point(247, 150);
            this.lTcpConnectionLostCount.Name = "lTcpConnectionLostCount";
            this.lTcpConnectionLostCount.Size = new System.Drawing.Size(13, 15);
            this.lTcpConnectionLostCount.TabIndex = 21;
            this.lTcpConnectionLostCount.Text = "0";
            // 
            // lTcpConnectFailCount
            // 
            this.lTcpConnectFailCount.AutoSize = true;
            this.lTcpConnectFailCount.Dock = System.Windows.Forms.DockStyle.Right;
            this.lTcpConnectFailCount.Location = new System.Drawing.Point(247, 135);
            this.lTcpConnectFailCount.Name = "lTcpConnectFailCount";
            this.lTcpConnectFailCount.Size = new System.Drawing.Size(13, 15);
            this.lTcpConnectFailCount.TabIndex = 19;
            this.lTcpConnectFailCount.Text = "0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 105);
            this.label4.Margin = new System.Windows.Forms.Padding(3, 0, 3, 2);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(106, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "Reponse data errors:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(3, 90);
            this.label11.Margin = new System.Windows.Forms.Padding(3, 0, 3, 2);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(156, 13);
            this.label11.TabIndex = 12;
            this.label11.Text = "Response MBAP-header errors:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 75);
            this.label7.Margin = new System.Windows.Forms.Padding(3, 0, 3, 2);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(112, 13);
            this.label7.TabIndex = 10;
            this.label7.Text = "Response CRC errors:";
            // 
            // lTcpConnectCount
            // 
            this.lTcpConnectCount.AutoSize = true;
            this.lTcpConnectCount.Dock = System.Windows.Forms.DockStyle.Right;
            this.lTcpConnectCount.Location = new System.Drawing.Point(247, 120);
            this.lTcpConnectCount.Name = "lTcpConnectCount";
            this.lTcpConnectCount.Size = new System.Drawing.Size(13, 15);
            this.lTcpConnectCount.TabIndex = 17;
            this.lTcpConnectCount.Text = "0";
            // 
            // lResponseDataErrorCount
            // 
            this.lResponseDataErrorCount.AutoSize = true;
            this.lResponseDataErrorCount.Dock = System.Windows.Forms.DockStyle.Right;
            this.lResponseDataErrorCount.Location = new System.Drawing.Point(247, 105);
            this.lResponseDataErrorCount.Name = "lResponseDataErrorCount";
            this.lResponseDataErrorCount.Size = new System.Drawing.Size(13, 15);
            this.lResponseDataErrorCount.TabIndex = 15;
            this.lResponseDataErrorCount.Text = "0";
            // 
            // lRespMbapErrorCount
            // 
            this.lRespMbapErrorCount.AutoSize = true;
            this.lRespMbapErrorCount.Dock = System.Windows.Forms.DockStyle.Right;
            this.lRespMbapErrorCount.Location = new System.Drawing.Point(247, 90);
            this.lRespMbapErrorCount.Name = "lRespMbapErrorCount";
            this.lRespMbapErrorCount.Size = new System.Drawing.Size(13, 15);
            this.lRespMbapErrorCount.TabIndex = 13;
            this.lRespMbapErrorCount.Text = "0";
            // 
            // label50
            // 
            this.label50.AutoSize = true;
            this.label50.Location = new System.Drawing.Point(3, 60);
            this.label50.Margin = new System.Windows.Forms.Padding(3, 0, 3, 2);
            this.label50.Name = "label50";
            this.label50.Size = new System.Drawing.Size(177, 13);
            this.label50.TabIndex = 8;
            this.label50.Text = "Response too short and CRC errors:";
            // 
            // lResponseCrcErrorCount
            // 
            this.lResponseCrcErrorCount.AutoSize = true;
            this.lResponseCrcErrorCount.Dock = System.Windows.Forms.DockStyle.Right;
            this.lResponseCrcErrorCount.Location = new System.Drawing.Point(247, 75);
            this.lResponseCrcErrorCount.Name = "lResponseCrcErrorCount";
            this.lResponseCrcErrorCount.Size = new System.Drawing.Size(13, 15);
            this.lResponseCrcErrorCount.TabIndex = 11;
            this.lResponseCrcErrorCount.Text = "0";
            // 
            // lResponseShortAndCrcErrorCount
            // 
            this.lResponseShortAndCrcErrorCount.AutoSize = true;
            this.lResponseShortAndCrcErrorCount.Dock = System.Windows.Forms.DockStyle.Right;
            this.lResponseShortAndCrcErrorCount.Location = new System.Drawing.Point(247, 60);
            this.lResponseShortAndCrcErrorCount.Name = "lResponseShortAndCrcErrorCount";
            this.lResponseShortAndCrcErrorCount.Size = new System.Drawing.Size(13, 15);
            this.lResponseShortAndCrcErrorCount.TabIndex = 9;
            this.lResponseShortAndCrcErrorCount.Text = "0";
            // 
            // buttonReset
            // 
            this.buttonReset.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.buttonReset.Location = new System.Drawing.Point(107, 355);
            this.buttonReset.Name = "buttonReset";
            this.buttonReset.Size = new System.Drawing.Size(75, 23);
            this.buttonReset.TabIndex = 1;
            this.buttonReset.Text = "Reset";
            this.buttonReset.UseVisualStyleBackColor = true;
            this.buttonReset.Click += new System.EventHandler(this.buttonReset_Click);
            // 
            // DiagnosticsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(296, 383);
            this.Controls.Add(this.buttonReset);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DiagnosticsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ModbusControl diagnostics counters";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

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
    }
}