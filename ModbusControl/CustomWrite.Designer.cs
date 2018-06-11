namespace ModbusControlSpace
{
    partial class CustomWrite
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CustomWrite));
            this.textboxLog = new System.Windows.Forms.TextBox();
            this.buttonClear = new System.Windows.Forms.Button();
            this.buttonSend = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.radiobuttonSize16bit = new System.Windows.Forms.RadioButton();
            this.radiobuttonSize32bit = new System.Windows.Forms.RadioButton();
            this.textboxData = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.radiobuttonWriteFC16 = new System.Windows.Forms.RadioButton();
            this.radiobuttonWriteFC6 = new System.Windows.Forms.RadioButton();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.numericQuantity = new System.Windows.Forms.NumericUpDown();
            this.numericRegister = new System.Windows.Forms.NumericUpDown();
            this.numericSlaveAddress = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericQuantity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericRegister)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericSlaveAddress)).BeginInit();
            this.SuspendLayout();
            // 
            // textboxLog
            // 
            this.textboxLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textboxLog.Location = new System.Drawing.Point(16, 218);
            this.textboxLog.Multiline = true;
            this.textboxLog.Name = "textboxLog";
            this.textboxLog.ReadOnly = true;
            this.textboxLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textboxLog.Size = new System.Drawing.Size(516, 159);
            this.textboxLog.TabIndex = 13;
            this.textboxLog.WordWrap = false;
            // 
            // buttonClear
            // 
            this.buttonClear.Location = new System.Drawing.Point(172, 189);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(75, 23);
            this.buttonClear.TabIndex = 12;
            this.buttonClear.Text = "Clear log";
            this.toolTip1.SetToolTip(this.buttonClear, "Clear the result log window");
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // buttonSend
            // 
            this.buttonSend.Location = new System.Drawing.Point(16, 189);
            this.buttonSend.Name = "buttonSend";
            this.buttonSend.Size = new System.Drawing.Size(150, 23);
            this.buttonSend.TabIndex = 11;
            this.buttonSend.Text = "Send";
            this.toolTip1.SetToolTip(this.buttonSend, "Send the write command");
            this.buttonSend.UseVisualStyleBackColor = true;
            this.buttonSend.Click += new System.EventHandler(this.buttonSend_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.radiobuttonSize16bit);
            this.groupBox2.Controls.Add(this.radiobuttonSize32bit);
            this.groupBox2.Location = new System.Drawing.Point(147, 41);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(79, 53);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Data size:";
            // 
            // radiobuttonSize16bit
            // 
            this.radiobuttonSize16bit.AutoSize = true;
            this.radiobuttonSize16bit.Checked = true;
            this.radiobuttonSize16bit.Location = new System.Drawing.Point(9, 13);
            this.radiobuttonSize16bit.Name = "radiobuttonSize16bit";
            this.radiobuttonSize16bit.Size = new System.Drawing.Size(51, 17);
            this.radiobuttonSize16bit.TabIndex = 0;
            this.radiobuttonSize16bit.TabStop = true;
            this.radiobuttonSize16bit.Text = "16-bit";
            this.toolTip1.SetToolTip(this.radiobuttonSize16bit, "Handle all registers as 16-bit values");
            this.radiobuttonSize16bit.UseVisualStyleBackColor = true;
            // 
            // radiobuttonSize32bit
            // 
            this.radiobuttonSize32bit.AutoSize = true;
            this.radiobuttonSize32bit.Location = new System.Drawing.Point(9, 30);
            this.radiobuttonSize32bit.Name = "radiobuttonSize32bit";
            this.radiobuttonSize32bit.Size = new System.Drawing.Size(51, 17);
            this.radiobuttonSize32bit.TabIndex = 1;
            this.radiobuttonSize32bit.Text = "32-bit";
            this.toolTip1.SetToolTip(this.radiobuttonSize32bit, "Handle all registers as 32-bit values (2 registers per value)");
            this.radiobuttonSize32bit.UseVisualStyleBackColor = true;
            // 
            // textboxData
            // 
            this.textboxData.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textboxData.Location = new System.Drawing.Point(111, 104);
            this.textboxData.Multiline = true;
            this.textboxData.Name = "textboxData";
            this.textboxData.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textboxData.Size = new System.Drawing.Size(421, 75);
            this.textboxData.TabIndex = 10;
            this.toolTip1.SetToolTip(this.textboxData, "Enter the decimal or hex values to be writtten, 16 or 32-bit values, comma separa" +
        "ted. Must match quantity.");
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 107);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(97, 26);
            this.label4.TabIndex = 9;
            this.label4.Text = "Write data:\r\n(comma separated)\r\n";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.radiobuttonWriteFC16);
            this.groupBox3.Controls.Add(this.radiobuttonWriteFC6);
            this.groupBox3.Location = new System.Drawing.Point(16, 41);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(127, 53);
            this.groupBox3.TabIndex = 7;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Use function code:";
            // 
            // radiobuttonWriteFC16
            // 
            this.radiobuttonWriteFC16.AutoSize = true;
            this.radiobuttonWriteFC16.Checked = true;
            this.radiobuttonWriteFC16.Location = new System.Drawing.Point(9, 30);
            this.radiobuttonWriteFC16.Name = "radiobuttonWriteFC16";
            this.radiobuttonWriteFC16.Size = new System.Drawing.Size(116, 17);
            this.radiobuttonWriteFC16.TabIndex = 1;
            this.radiobuttonWriteFC16.TabStop = true;
            this.radiobuttonWriteFC16.Text = "16 (set multiple reg)";
            this.toolTip1.SetToolTip(this.radiobuttonWriteFC16, "Use function code 16 (set multiple register) only");
            this.radiobuttonWriteFC16.UseVisualStyleBackColor = true;
            // 
            // radiobuttonWriteFC6
            // 
            this.radiobuttonWriteFC6.AutoSize = true;
            this.radiobuttonWriteFC6.Location = new System.Drawing.Point(9, 13);
            this.radiobuttonWriteFC6.Name = "radiobuttonWriteFC6";
            this.radiobuttonWriteFC6.Size = new System.Drawing.Size(102, 17);
            this.radiobuttonWriteFC6.TabIndex = 0;
            this.radiobuttonWriteFC6.TabStop = true;
            this.radiobuttonWriteFC6.Text = "6 (set single reg)";
            this.toolTip1.SetToolTip(this.radiobuttonWriteFC6, "Use function code 6 (set single register) of FC16 (set multiple registers) only");
            this.radiobuttonWriteFC6.UseVisualStyleBackColor = true;
            // 
            // numericQuantity
            // 
            this.numericQuantity.Location = new System.Drawing.Point(315, 13);
            this.numericQuantity.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numericQuantity.Name = "numericQuantity";
            this.numericQuantity.Size = new System.Drawing.Size(56, 20);
            this.numericQuantity.TabIndex = 5;
            this.toolTip1.SetToolTip(this.numericQuantity, "Number of 16-bit registers to write");
            this.numericQuantity.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // numericRegister
            // 
            this.numericRegister.Location = new System.Drawing.Point(175, 13);
            this.numericRegister.Maximum = new decimal(new int[] {
            -1,
            0,
            0,
            0});
            this.numericRegister.Name = "numericRegister";
            this.numericRegister.Size = new System.Drawing.Size(82, 20);
            this.numericRegister.TabIndex = 3;
            this.toolTip1.SetToolTip(this.numericRegister, "First register to write");
            // 
            // numericSlaveAddress
            // 
            this.numericSlaveAddress.Location = new System.Drawing.Point(64, 13);
            this.numericSlaveAddress.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericSlaveAddress.Name = "numericSlaveAddress";
            this.numericSlaveAddress.Size = new System.Drawing.Size(53, 20);
            this.numericSlaveAddress.TabIndex = 1;
            this.toolTip1.SetToolTip(this.numericSlaveAddress, "Slave address (0-255)");
            this.numericSlaveAddress.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(265, 15);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(49, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Quantity:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(125, 15);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(49, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "Register:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 15);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(51, 13);
            this.label7.TabIndex = 0;
            this.label7.Text = "Slave ID:";
            // 
            // CustomWrite
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(544, 389);
            this.Controls.Add(this.numericQuantity);
            this.Controls.Add(this.numericRegister);
            this.Controls.Add(this.numericSlaveAddress);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textboxData);
            this.Controls.Add(this.textboxLog);
            this.Controls.Add(this.buttonClear);
            this.Controls.Add(this.buttonSend);
            this.Controls.Add(this.groupBox2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CustomWrite";
            this.Text = "Custom write (holding registers)";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnFormClosing);
            this.Shown += new System.EventHandler(this.CustomWrite_Shown);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericQuantity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericRegister)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericSlaveAddress)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textboxLog;
        private System.Windows.Forms.Button buttonClear;
        private System.Windows.Forms.Button buttonSend;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton radiobuttonSize16bit;
        private System.Windows.Forms.RadioButton radiobuttonSize32bit;
        private System.Windows.Forms.TextBox textboxData;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton radiobuttonWriteFC16;
        private System.Windows.Forms.RadioButton radiobuttonWriteFC6;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.NumericUpDown numericQuantity;
        private System.Windows.Forms.NumericUpDown numericRegister;
        private System.Windows.Forms.NumericUpDown numericSlaveAddress;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
    }
}