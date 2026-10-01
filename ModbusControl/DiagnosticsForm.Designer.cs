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
            buttonReset = new Button();
            label9 = new Label();
            label5 = new Label();
            lResponseProtocolErrorCount = new Label();
            label3 = new Label();
            lNoResponseCount = new Label();
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
            lOnewireEchoErrorCount = new Label();
            lRwSuccessCount = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(16, 13);
            label1.Margin = new Padding(3, 0, 3, 4);
            label1.Name = "label1";
            label1.Size = new Size(105, 13);
            label1.TabIndex = 0;
            label1.Text = "Read/write success:";
            // 
            // buttonReset
            // 
            buttonReset.Location = new Point(135, 347);
            buttonReset.Margin = new Padding(3, 13, 3, 3);
            buttonReset.Name = "buttonReset";
            buttonReset.Size = new Size(69, 23);
            buttonReset.TabIndex = 1;
            buttonReset.Text = "Reset";
            buttonReset.UseVisualStyleBackColor = true;
            buttonReset.Click += buttonReset_Click;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(16, 31);
            label9.Name = "label9";
            label9.Size = new Size(105, 13);
            label9.TabIndex = 2;
            label9.Text = "Onewire echo errors:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(16, 59);
            label5.Name = "label5";
            label5.Size = new Size(128, 13);
            label5.TabIndex = 6;
            label5.Text = "Response protocol errors:";
            // 
            // lResponseProtocolErrorCount
            // 
            lResponseProtocolErrorCount.Location = new Point(220, 59);
            lResponseProtocolErrorCount.Name = "lResponseProtocolErrorCount";
            lResponseProtocolErrorCount.Size = new Size(80, 13);
            lResponseProtocolErrorCount.TabIndex = 7;
            lResponseProtocolErrorCount.Text = "0";
            lResponseProtocolErrorCount.TextAlign = ContentAlignment.TopRight;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(16, 45);
            label3.Name = "label3";
            label3.Size = new Size(70, 13);
            label3.TabIndex = 4;
            label3.Text = "No response:";
            // 
            // lNoResponseCount
            // 
            lNoResponseCount.Location = new Point(220, 44);
            lNoResponseCount.Name = "lNoResponseCount";
            lNoResponseCount.Size = new Size(80, 13);
            lNoResponseCount.TabIndex = 5;
            lNoResponseCount.Text = "0";
            lNoResponseCount.TextAlign = ContentAlignment.TopRight;
            // 
            // label37
            // 
            label37.AutoSize = true;
            label37.Location = new Point(16, 322);
            label37.Name = "label37";
            label37.Size = new Size(135, 13);
            label37.TabIndex = 42;
            label37.Text = "Modbus/TCP bridge errors:";
            // 
            // label35
            // 
            label35.AutoSize = true;
            label35.Location = new Point(16, 308);
            label35.Name = "label35";
            label35.Size = new Size(143, 13);
            label35.TabIndex = 40;
            label35.Text = "Modbus/TCP bridge queries:";
            // 
            // label33
            // 
            label33.AutoSize = true;
            label33.Location = new Point(16, 294);
            label33.Name = "label33";
            label33.Size = new Size(194, 13);
            label33.TabIndex = 38;
            label33.Text = "Modbus/TCP bridge open connections:";
            // 
            // label31
            // 
            label31.AutoSize = true;
            label31.Location = new Point(16, 280);
            label31.Name = "label31";
            label31.Size = new Size(153, 13);
            label31.TabIndex = 36;
            label31.Text = "Modbus/TCP bridge connects:";
            // 
            // label29
            // 
            label29.AutoSize = true;
            label29.Location = new Point(16, 262);
            label29.Margin = new Padding(3, 0, 3, 4);
            label29.Name = "label29";
            label29.Size = new Size(137, 13);
            label29.TabIndex = 34;
            label29.Text = "Received other exceptions:";
            // 
            // label27
            // 
            label27.AutoSize = true;
            label27.Location = new Point(16, 249);
            label27.Name = "label27";
            label27.Size = new Size(188, 13);
            label27.TabIndex = 32;
            label27.Text = "Gateway dev no response exceptions:";
            // 
            // label25
            // 
            label25.AutoSize = true;
            label25.Location = new Point(16, 236);
            label25.Name = "label25";
            label25.Size = new Size(187, 13);
            label25.TabIndex = 30;
            label25.Text = "Gateway path unavailable exceptions:";
            // 
            // lBridgeErrorsCount
            // 
            lBridgeErrorsCount.Location = new Point(220, 322);
            lBridgeErrorsCount.Name = "lBridgeErrorsCount";
            lBridgeErrorsCount.Size = new Size(80, 13);
            lBridgeErrorsCount.TabIndex = 43;
            lBridgeErrorsCount.Text = "0";
            lBridgeErrorsCount.TextAlign = ContentAlignment.TopRight;
            // 
            // lBridgeQueriesReceivedCount
            // 
            lBridgeQueriesReceivedCount.Location = new Point(220, 308);
            lBridgeQueriesReceivedCount.Name = "lBridgeQueriesReceivedCount";
            lBridgeQueriesReceivedCount.Size = new Size(80, 13);
            lBridgeQueriesReceivedCount.TabIndex = 41;
            lBridgeQueriesReceivedCount.Text = "0";
            lBridgeQueriesReceivedCount.TextAlign = ContentAlignment.TopRight;
            // 
            // lBridgeOpenConnections
            // 
            lBridgeOpenConnections.Location = new Point(220, 294);
            lBridgeOpenConnections.Name = "lBridgeOpenConnections";
            lBridgeOpenConnections.Size = new Size(80, 13);
            lBridgeOpenConnections.TabIndex = 39;
            lBridgeOpenConnections.Text = "0";
            lBridgeOpenConnections.TextAlign = ContentAlignment.TopRight;
            // 
            // lBridgeConnectCount
            // 
            lBridgeConnectCount.Location = new Point(220, 280);
            lBridgeConnectCount.Name = "lBridgeConnectCount";
            lBridgeConnectCount.Size = new Size(80, 13);
            lBridgeConnectCount.TabIndex = 37;
            lBridgeConnectCount.Text = "0";
            lBridgeConnectCount.TextAlign = ContentAlignment.TopRight;
            // 
            // lOtherExceptionCount
            // 
            lOtherExceptionCount.Location = new Point(220, 262);
            lOtherExceptionCount.Name = "lOtherExceptionCount";
            lOtherExceptionCount.Size = new Size(80, 13);
            lOtherExceptionCount.TabIndex = 35;
            lOtherExceptionCount.Text = "0";
            lOtherExceptionCount.TextAlign = ContentAlignment.TopRight;
            // 
            // lGatewayDeviceFailedToRespondExceptionCount
            // 
            lGatewayDeviceFailedToRespondExceptionCount.Location = new Point(220, 249);
            lGatewayDeviceFailedToRespondExceptionCount.Name = "lGatewayDeviceFailedToRespondExceptionCount";
            lGatewayDeviceFailedToRespondExceptionCount.Size = new Size(80, 13);
            lGatewayDeviceFailedToRespondExceptionCount.TabIndex = 33;
            lGatewayDeviceFailedToRespondExceptionCount.Text = "0";
            lGatewayDeviceFailedToRespondExceptionCount.TextAlign = ContentAlignment.TopRight;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(16, 222);
            label2.Name = "label2";
            label2.Size = new Size(115, 13);
            label2.TabIndex = 28;
            label2.Text = "Illegal data exceptions:";
            // 
            // label23
            // 
            label23.AutoSize = true;
            label23.Location = new Point(16, 208);
            label23.Name = "label23";
            label23.Size = new Size(131, 13);
            label23.TabIndex = 26;
            label23.Text = "Illegal address exceptions:";
            // 
            // label21
            // 
            label21.AutoSize = true;
            label21.Location = new Point(16, 193);
            label21.Name = "label21";
            label21.Size = new Size(132, 13);
            label21.TabIndex = 24;
            label21.Text = "Illegal function exceptions:";
            // 
            // lGatewayPathUnavailableExceptionCount
            // 
            lGatewayPathUnavailableExceptionCount.Location = new Point(220, 236);
            lGatewayPathUnavailableExceptionCount.Name = "lGatewayPathUnavailableExceptionCount";
            lGatewayPathUnavailableExceptionCount.Size = new Size(80, 13);
            lGatewayPathUnavailableExceptionCount.TabIndex = 31;
            lGatewayPathUnavailableExceptionCount.Text = "0";
            lGatewayPathUnavailableExceptionCount.TextAlign = ContentAlignment.TopRight;
            // 
            // lIllegalDataExceptionCount
            // 
            lIllegalDataExceptionCount.Location = new Point(220, 222);
            lIllegalDataExceptionCount.Name = "lIllegalDataExceptionCount";
            lIllegalDataExceptionCount.Size = new Size(80, 13);
            lIllegalDataExceptionCount.TabIndex = 29;
            lIllegalDataExceptionCount.Text = "0";
            lIllegalDataExceptionCount.TextAlign = ContentAlignment.TopRight;
            // 
            // lIllegalAddressExceptionCount
            // 
            lIllegalAddressExceptionCount.Location = new Point(220, 208);
            lIllegalAddressExceptionCount.Name = "lIllegalAddressExceptionCount";
            lIllegalAddressExceptionCount.Size = new Size(80, 13);
            lIllegalAddressExceptionCount.TabIndex = 27;
            lIllegalAddressExceptionCount.Text = "0";
            lIllegalAddressExceptionCount.TextAlign = ContentAlignment.TopRight;
            // 
            // label19
            // 
            label19.AutoSize = true;
            label19.Location = new Point(16, 175);
            label19.Margin = new Padding(3, 0, 3, 4);
            label19.Name = "label19";
            label19.Size = new Size(154, 13);
            label19.TabIndex = 22;
            label19.Text = "TCP connection close on error:";
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Location = new Point(16, 161);
            label17.Name = "label17";
            label17.Size = new Size(139, 13);
            label17.TabIndex = 20;
            label17.Text = "TCP connection lost count: ";
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Location = new Point(16, 147);
            label15.Name = "label15";
            label15.Size = new Size(119, 13);
            label15.TabIndex = 18;
            label15.Text = "TCP connect fail count:";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(16, 133);
            label13.Name = "label13";
            label13.Size = new Size(103, 13);
            label13.TabIndex = 16;
            label13.Text = "TCP connect count:";
            // 
            // lIllegalFunctionExceptionCount
            // 
            lIllegalFunctionExceptionCount.Location = new Point(220, 193);
            lIllegalFunctionExceptionCount.Name = "lIllegalFunctionExceptionCount";
            lIllegalFunctionExceptionCount.Size = new Size(80, 13);
            lIllegalFunctionExceptionCount.TabIndex = 25;
            lIllegalFunctionExceptionCount.Text = "0";
            lIllegalFunctionExceptionCount.TextAlign = ContentAlignment.TopRight;
            // 
            // lTcpClosedOnErrorCount
            // 
            lTcpClosedOnErrorCount.Location = new Point(220, 175);
            lTcpClosedOnErrorCount.Name = "lTcpClosedOnErrorCount";
            lTcpClosedOnErrorCount.Size = new Size(80, 13);
            lTcpClosedOnErrorCount.TabIndex = 23;
            lTcpClosedOnErrorCount.Text = "0";
            lTcpClosedOnErrorCount.TextAlign = ContentAlignment.TopRight;
            // 
            // lTcpConnectionLostCount
            // 
            lTcpConnectionLostCount.Location = new Point(220, 161);
            lTcpConnectionLostCount.Name = "lTcpConnectionLostCount";
            lTcpConnectionLostCount.Size = new Size(80, 13);
            lTcpConnectionLostCount.TabIndex = 21;
            lTcpConnectionLostCount.Text = "0";
            lTcpConnectionLostCount.TextAlign = ContentAlignment.TopRight;
            // 
            // lTcpConnectFailCount
            // 
            lTcpConnectFailCount.Location = new Point(220, 147);
            lTcpConnectFailCount.Name = "lTcpConnectFailCount";
            lTcpConnectFailCount.Size = new Size(80, 13);
            lTcpConnectFailCount.TabIndex = 19;
            lTcpConnectFailCount.Text = "0";
            lTcpConnectFailCount.TextAlign = ContentAlignment.TopRight;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(16, 115);
            label4.Margin = new Padding(3, 0, 3, 4);
            label4.Name = "label4";
            label4.Size = new Size(106, 13);
            label4.TabIndex = 14;
            label4.Text = "Reponse data errors:";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(16, 101);
            label11.Name = "label11";
            label11.Size = new Size(156, 13);
            label11.TabIndex = 12;
            label11.Text = "Response MBAP-header errors:";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(16, 87);
            label7.Name = "label7";
            label7.Size = new Size(112, 13);
            label7.TabIndex = 10;
            label7.Text = "Response CRC errors:";
            // 
            // lTcpConnectCount
            // 
            lTcpConnectCount.Location = new Point(220, 133);
            lTcpConnectCount.Name = "lTcpConnectCount";
            lTcpConnectCount.Size = new Size(80, 13);
            lTcpConnectCount.TabIndex = 17;
            lTcpConnectCount.Text = "0";
            lTcpConnectCount.TextAlign = ContentAlignment.TopRight;
            // 
            // lResponseDataErrorCount
            // 
            lResponseDataErrorCount.Location = new Point(220, 115);
            lResponseDataErrorCount.Name = "lResponseDataErrorCount";
            lResponseDataErrorCount.Size = new Size(80, 13);
            lResponseDataErrorCount.TabIndex = 15;
            lResponseDataErrorCount.Text = "0";
            lResponseDataErrorCount.TextAlign = ContentAlignment.TopRight;
            // 
            // lRespMbapErrorCount
            // 
            lRespMbapErrorCount.Location = new Point(220, 101);
            lRespMbapErrorCount.Name = "lRespMbapErrorCount";
            lRespMbapErrorCount.Size = new Size(80, 13);
            lRespMbapErrorCount.TabIndex = 13;
            lRespMbapErrorCount.Text = "0";
            lRespMbapErrorCount.TextAlign = ContentAlignment.TopRight;
            // 
            // label50
            // 
            label50.AutoSize = true;
            label50.Location = new Point(16, 73);
            label50.Name = "label50";
            label50.Size = new Size(177, 13);
            label50.TabIndex = 8;
            label50.Text = "Response too short and CRC errors:";
            // 
            // lResponseCrcErrorCount
            // 
            lResponseCrcErrorCount.Location = new Point(220, 87);
            lResponseCrcErrorCount.Name = "lResponseCrcErrorCount";
            lResponseCrcErrorCount.Size = new Size(80, 13);
            lResponseCrcErrorCount.TabIndex = 11;
            lResponseCrcErrorCount.Text = "0";
            lResponseCrcErrorCount.TextAlign = ContentAlignment.TopRight;
            // 
            // lResponseShortAndCrcErrorCount
            // 
            lResponseShortAndCrcErrorCount.Location = new Point(220, 73);
            lResponseShortAndCrcErrorCount.Name = "lResponseShortAndCrcErrorCount";
            lResponseShortAndCrcErrorCount.Size = new Size(80, 13);
            lResponseShortAndCrcErrorCount.TabIndex = 9;
            lResponseShortAndCrcErrorCount.Text = "0";
            lResponseShortAndCrcErrorCount.TextAlign = ContentAlignment.TopRight;
            // 
            // lOnewireEchoErrorCount
            // 
            lOnewireEchoErrorCount.Location = new Point(220, 31);
            lOnewireEchoErrorCount.Name = "lOnewireEchoErrorCount";
            lOnewireEchoErrorCount.Size = new Size(80, 13);
            lOnewireEchoErrorCount.TabIndex = 3;
            lOnewireEchoErrorCount.Text = "0";
            lOnewireEchoErrorCount.TextAlign = ContentAlignment.TopRight;
            // 
            // lRwSuccessCount
            // 
            lRwSuccessCount.Location = new Point(220, 13);
            lRwSuccessCount.Name = "lRwSuccessCount";
            lRwSuccessCount.Size = new Size(80, 13);
            lRwSuccessCount.TabIndex = 1;
            lRwSuccessCount.Text = "0";
            lRwSuccessCount.TextAlign = ContentAlignment.TopRight;
            // 
            // DiagnosticsForm
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(323, 382);
            Controls.Add(label37);
            Controls.Add(lResponseProtocolErrorCount);
            Controls.Add(label35);
            Controls.Add(lRwSuccessCount);
            Controls.Add(label33);
            Controls.Add(lNoResponseCount);
            Controls.Add(label31);
            Controls.Add(label1);
            Controls.Add(label29);
            Controls.Add(lOnewireEchoErrorCount);
            Controls.Add(label27);
            Controls.Add(buttonReset);
            Controls.Add(lBridgeErrorsCount);
            Controls.Add(label25);
            Controls.Add(lBridgeQueriesReceivedCount);
            Controls.Add(label9);
            Controls.Add(lBridgeOpenConnections);
            Controls.Add(lBridgeConnectCount);
            Controls.Add(label5);
            Controls.Add(lOtherExceptionCount);
            Controls.Add(label50);
            Controls.Add(lGatewayDeviceFailedToRespondExceptionCount);
            Controls.Add(label7);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label11);
            Controls.Add(label23);
            Controls.Add(label4);
            Controls.Add(label21);
            Controls.Add(label13);
            Controls.Add(lGatewayPathUnavailableExceptionCount);
            Controls.Add(label15);
            Controls.Add(lIllegalDataExceptionCount);
            Controls.Add(label17);
            Controls.Add(lIllegalAddressExceptionCount);
            Controls.Add(label19);
            Controls.Add(lIllegalFunctionExceptionCount);
            Controls.Add(lResponseShortAndCrcErrorCount);
            Controls.Add(lResponseCrcErrorCount);
            Controls.Add(lTcpClosedOnErrorCount);
            Controls.Add(lRespMbapErrorCount);
            Controls.Add(lTcpConnectionLostCount);
            Controls.Add(lResponseDataErrorCount);
            Controls.Add(lTcpConnectFailCount);
            Controls.Add(lTcpConnectCount);
            Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "DiagnosticsForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "ModbusControl diagnostics counters";
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lTcpConnectFailCount;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label lTcpConnectCount;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label lRespMbapErrorCount;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lResponseCrcErrorCount;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lResponseProtocolErrorCount;
        private System.Windows.Forms.Label label3;
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
        private Label lNoResponseCount;
        private Label lOnewireEchoErrorCount;
        private Label lRwSuccessCount;
    }
}