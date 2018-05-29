namespace ModbusControlSpace
{
    partial class CustomModbusCommand
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CustomModbusCommand));
            this.label4 = new System.Windows.Forms.Label();
            this.textboxCommand = new System.Windows.Forms.TextBox();
            this.buttonSend = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textboxResult = new System.Windows.Forms.TextBox();
            this.buttonClear = new System.Windows.Forms.Button();
            this.checkboxEnableMainPolling = new System.Windows.Forms.CheckBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 10);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(323, 26);
            this.label4.TabIndex = 0;
            this.label4.Text = "Enter modbus command in RTU form, hex bytes, space separated:\r\n(First byte is sla" +
    "ve ID. MBAP header or CRC is added automatically)";
            // 
            // textboxCommand
            // 
            this.textboxCommand.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textboxCommand.Location = new System.Drawing.Point(16, 39);
            this.textboxCommand.Multiline = true;
            this.textboxCommand.Name = "textboxCommand";
            this.textboxCommand.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textboxCommand.Size = new System.Drawing.Size(516, 60);
            this.textboxCommand.TabIndex = 2;
            this.textboxCommand.Text = "01 03 00 00 00 0A";
            this.toolTip1.SetToolTip(this.textboxCommand, "Enter the raw modbus command in RTU format, hex bytes, space separated. \r\nThe MBA" +
        "P header or CRC are added automatically by the transport layers as needed.\r\nFor " +
        "example: 01 03 00 00 00 0A");
            // 
            // buttonSend
            // 
            this.buttonSend.Location = new System.Drawing.Point(16, 105);
            this.buttonSend.Name = "buttonSend";
            this.buttonSend.Size = new System.Drawing.Size(150, 23);
            this.buttonSend.TabIndex = 3;
            this.buttonSend.Text = "Send";
            this.toolTip1.SetToolTip(this.buttonSend, "Send the command entered above");
            this.buttonSend.UseVisualStyleBackColor = true;
            this.buttonSend.Click += new System.EventHandler(this.buttonSend_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 140);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(279, 26);
            this.label1.TabIndex = 5;
            this.label1.Text = "Result log:\r\n(Response first byte is slave ID, no MBAP header or CRC)";
            // 
            // textboxResult
            // 
            this.textboxResult.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textboxResult.Location = new System.Drawing.Point(17, 169);
            this.textboxResult.Multiline = true;
            this.textboxResult.Name = "textboxResult";
            this.textboxResult.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textboxResult.Size = new System.Drawing.Size(516, 181);
            this.textboxResult.TabIndex = 6;
            this.toolTip1.SetToolTip(this.textboxResult, "The result of the command is shown here. Or an error message.");
            // 
            // buttonClear
            // 
            this.buttonClear.Location = new System.Drawing.Point(172, 105);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(75, 23);
            this.buttonClear.TabIndex = 4;
            this.buttonClear.Text = "Clear log";
            this.toolTip1.SetToolTip(this.buttonClear, "Clear the result log window");
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // checkboxEnableMainPolling
            // 
            this.checkboxEnableMainPolling.AutoSize = true;
            this.checkboxEnableMainPolling.Checked = true;
            this.checkboxEnableMainPolling.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkboxEnableMainPolling.Location = new System.Drawing.Point(396, 14);
            this.checkboxEnableMainPolling.Name = "checkboxEnableMainPolling";
            this.checkboxEnableMainPolling.Size = new System.Drawing.Size(140, 17);
            this.checkboxEnableMainPolling.TabIndex = 1;
            this.checkboxEnableMainPolling.Text = "Enable main form polling";
            this.toolTip1.SetToolTip(this.checkboxEnableMainPolling, "Enable or disable the polling in the main form tabs.\r\n");
            this.checkboxEnableMainPolling.UseVisualStyleBackColor = true;
            this.checkboxEnableMainPolling.CheckedChanged += new System.EventHandler(this.checkboxEnableMainPolling_CheckedChanged);
            // 
            // CustomModbusCommand
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(544, 362);
            this.Controls.Add(this.checkboxEnableMainPolling);
            this.Controls.Add(this.buttonClear);
            this.Controls.Add(this.textboxResult);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textboxCommand);
            this.Controls.Add(this.buttonSend);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CustomModbusCommand";
            this.Text = "Custom modbus command";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CustomModbusCommand_FormClosing);
            this.Shown += new System.EventHandler(this.CustomModbusCommand_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textboxCommand;
        private System.Windows.Forms.Button buttonSend;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textboxResult;
        private System.Windows.Forms.Button buttonClear;
        private System.Windows.Forms.CheckBox checkboxEnableMainPolling;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}