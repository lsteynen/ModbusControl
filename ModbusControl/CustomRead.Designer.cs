namespace ModbusControlSpace
{
    partial class CustomRead
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CustomRead));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupboxDatasize = new System.Windows.Forms.GroupBox();
            this.radiobuttonSize16bit = new System.Windows.Forms.RadioButton();
            this.radiobuttonSize32bit = new System.Windows.Forms.RadioButton();
            this.groupboxShowas = new System.Windows.Forms.GroupBox();
            this.radiobuttonAsFloat = new System.Windows.Forms.RadioButton();
            this.radiobuttonAsHex = new System.Windows.Forms.RadioButton();
            this.radiobuttonAsInt = new System.Windows.Forms.RadioButton();
            this.radiobuttonAsUInt = new System.Windows.Forms.RadioButton();
            this.buttonClear = new System.Windows.Forms.Button();
            this.buttonSend = new System.Windows.Forms.Button();
            this.textboxLog = new System.Windows.Forms.TextBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.numericSlaveAddress = new System.Windows.Forms.NumericUpDown();
            this.numericRegister = new System.Windows.Forms.NumericUpDown();
            this.numericQuantity = new System.Windows.Forms.NumericUpDown();
            this.groupboxDatasize.SuspendLayout();
            this.groupboxShowas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericSlaveAddress)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericRegister)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericQuantity)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Slave ID:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(125, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Register:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(265, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Quantity:";
            // 
            // groupboxDatasize
            // 
            this.groupboxDatasize.Controls.Add(this.radiobuttonSize16bit);
            this.groupboxDatasize.Controls.Add(this.radiobuttonSize32bit);
            this.groupboxDatasize.Location = new System.Drawing.Point(15, 41);
            this.groupboxDatasize.Name = "groupboxDatasize";
            this.groupboxDatasize.Size = new System.Drawing.Size(79, 53);
            this.groupboxDatasize.TabIndex = 7;
            this.groupboxDatasize.TabStop = false;
            this.groupboxDatasize.Text = "Data size:";
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
            this.radiobuttonSize16bit.Tag = "16";
            this.radiobuttonSize16bit.Text = "16-bit";
            this.toolTip1.SetToolTip(this.radiobuttonSize16bit, "Handle all registers as 16-bit values");
            this.radiobuttonSize16bit.UseVisualStyleBackColor = true;
            this.radiobuttonSize16bit.CheckedChanged += new System.EventHandler(this.radiobuttonSize16bit_CheckedChanged);
            // 
            // radiobuttonSize32bit
            // 
            this.radiobuttonSize32bit.AutoSize = true;
            this.radiobuttonSize32bit.Location = new System.Drawing.Point(9, 30);
            this.radiobuttonSize32bit.Name = "radiobuttonSize32bit";
            this.radiobuttonSize32bit.Size = new System.Drawing.Size(51, 17);
            this.radiobuttonSize32bit.TabIndex = 1;
            this.radiobuttonSize32bit.Tag = "32";
            this.radiobuttonSize32bit.Text = "32-bit";
            this.toolTip1.SetToolTip(this.radiobuttonSize32bit, "Handle all registers as 32-bit values (2 registers per value)");
            this.radiobuttonSize32bit.UseVisualStyleBackColor = true;
            this.radiobuttonSize32bit.CheckedChanged += new System.EventHandler(this.radiobuttonSize32bit_CheckedChanged);
            // 
            // groupboxShowas
            // 
            this.groupboxShowas.Controls.Add(this.radiobuttonAsFloat);
            this.groupboxShowas.Controls.Add(this.radiobuttonAsHex);
            this.groupboxShowas.Controls.Add(this.radiobuttonAsInt);
            this.groupboxShowas.Controls.Add(this.radiobuttonAsUInt);
            this.groupboxShowas.Location = new System.Drawing.Point(98, 41);
            this.groupboxShowas.Name = "groupboxShowas";
            this.groupboxShowas.Size = new System.Drawing.Size(147, 53);
            this.groupboxShowas.TabIndex = 8;
            this.groupboxShowas.TabStop = false;
            this.groupboxShowas.Text = "Show values as:";
            // 
            // radiobuttonAsFloat
            // 
            this.radiobuttonAsFloat.AutoSize = true;
            this.radiobuttonAsFloat.Enabled = false;
            this.radiobuttonAsFloat.Location = new System.Drawing.Point(80, 30);
            this.radiobuttonAsFloat.Name = "radiobuttonAsFloat";
            this.radiobuttonAsFloat.Size = new System.Drawing.Size(48, 17);
            this.radiobuttonAsFloat.TabIndex = 3;
            this.radiobuttonAsFloat.Tag = "float";
            this.radiobuttonAsFloat.Text = "Float";
            this.toolTip1.SetToolTip(this.radiobuttonAsFloat, "Show all values as 32-bit floating point");
            this.radiobuttonAsFloat.UseVisualStyleBackColor = true;
            // 
            // radiobuttonAsHex
            // 
            this.radiobuttonAsHex.AutoSize = true;
            this.radiobuttonAsHex.Location = new System.Drawing.Point(80, 13);
            this.radiobuttonAsHex.Name = "radiobuttonAsHex";
            this.radiobuttonAsHex.Size = new System.Drawing.Size(44, 17);
            this.radiobuttonAsHex.TabIndex = 2;
            this.radiobuttonAsHex.Tag = "hex";
            this.radiobuttonAsHex.Text = "Hex";
            this.toolTip1.SetToolTip(this.radiobuttonAsHex, "Show all values in hexadecimal");
            this.radiobuttonAsHex.UseVisualStyleBackColor = true;
            // 
            // radiobuttonAsInt
            // 
            this.radiobuttonAsInt.AutoSize = true;
            this.radiobuttonAsInt.Checked = true;
            this.radiobuttonAsInt.Location = new System.Drawing.Point(9, 13);
            this.radiobuttonAsInt.Name = "radiobuttonAsInt";
            this.radiobuttonAsInt.Size = new System.Drawing.Size(37, 17);
            this.radiobuttonAsInt.TabIndex = 0;
            this.radiobuttonAsInt.TabStop = true;
            this.radiobuttonAsInt.Tag = "int";
            this.radiobuttonAsInt.Text = "Int";
            this.toolTip1.SetToolTip(this.radiobuttonAsInt, "Show all values as signed int");
            this.radiobuttonAsInt.UseVisualStyleBackColor = true;
            // 
            // radiobuttonAsUInt
            // 
            this.radiobuttonAsUInt.AutoSize = true;
            this.radiobuttonAsUInt.Location = new System.Drawing.Point(9, 30);
            this.radiobuttonAsUInt.Name = "radiobuttonAsUInt";
            this.radiobuttonAsUInt.Size = new System.Drawing.Size(45, 17);
            this.radiobuttonAsUInt.TabIndex = 1;
            this.radiobuttonAsUInt.Tag = "uint";
            this.radiobuttonAsUInt.Text = "UInt";
            this.toolTip1.SetToolTip(this.radiobuttonAsUInt, "Show all values as unsigned int");
            this.radiobuttonAsUInt.UseVisualStyleBackColor = true;
            // 
            // buttonClear
            // 
            this.buttonClear.Location = new System.Drawing.Point(171, 100);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(75, 23);
            this.buttonClear.TabIndex = 10;
            this.buttonClear.Text = "Clear log";
            this.toolTip1.SetToolTip(this.buttonClear, "Clear the result log window");
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // buttonSend
            // 
            this.buttonSend.Location = new System.Drawing.Point(15, 100);
            this.buttonSend.Name = "buttonSend";
            this.buttonSend.Size = new System.Drawing.Size(150, 23);
            this.buttonSend.TabIndex = 9;
            this.buttonSend.Text = "Send";
            this.toolTip1.SetToolTip(this.buttonSend, "Send the read command");
            this.buttonSend.UseVisualStyleBackColor = true;
            this.buttonSend.Click += new System.EventHandler(this.buttonSend_Click);
            // 
            // textboxLog
            // 
            this.textboxLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textboxLog.Location = new System.Drawing.Point(15, 129);
            this.textboxLog.Multiline = true;
            this.textboxLog.Name = "textboxLog";
            this.textboxLog.ReadOnly = true;
            this.textboxLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textboxLog.Size = new System.Drawing.Size(517, 221);
            this.textboxLog.TabIndex = 11;
            this.textboxLog.WordWrap = false;
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
            // numericRegister
            // 
            this.numericRegister.Location = new System.Drawing.Point(175, 13);
            this.numericRegister.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.numericRegister.Name = "numericRegister";
            this.numericRegister.Size = new System.Drawing.Size(82, 20);
            this.numericRegister.TabIndex = 3;
            this.toolTip1.SetToolTip(this.numericRegister, "First register to read");
            // 
            // numericQuantity
            // 
            this.numericQuantity.Location = new System.Drawing.Point(315, 13);
            this.numericQuantity.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericQuantity.Name = "numericQuantity";
            this.numericQuantity.Size = new System.Drawing.Size(56, 20);
            this.numericQuantity.TabIndex = 5;
            this.toolTip1.SetToolTip(this.numericQuantity, "Number of 16-bit registers to read");
            this.numericQuantity.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // CustomRead
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(544, 362);
            this.Controls.Add(this.numericQuantity);
            this.Controls.Add(this.numericRegister);
            this.Controls.Add(this.numericSlaveAddress);
            this.Controls.Add(this.textboxLog);
            this.Controls.Add(this.buttonClear);
            this.Controls.Add(this.buttonSend);
            this.Controls.Add(this.groupboxShowas);
            this.Controls.Add(this.groupboxDatasize);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CustomRead";
            this.Text = "Custom read (holding registers)";
            this.toolTip1.SetToolTip(this, "Send the read command");
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnFormClosing);
            this.Shown += new System.EventHandler(this.CustomRead_Shown);
            this.groupboxDatasize.ResumeLayout(false);
            this.groupboxDatasize.PerformLayout();
            this.groupboxShowas.ResumeLayout(false);
            this.groupboxShowas.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericSlaveAddress)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericRegister)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericQuantity)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupboxDatasize;
        private System.Windows.Forms.RadioButton radiobuttonSize16bit;
        private System.Windows.Forms.RadioButton radiobuttonSize32bit;
        private System.Windows.Forms.GroupBox groupboxShowas;
        private System.Windows.Forms.RadioButton radiobuttonAsHex;
        private System.Windows.Forms.RadioButton radiobuttonAsInt;
        private System.Windows.Forms.RadioButton radiobuttonAsUInt;
        private System.Windows.Forms.Button buttonClear;
        private System.Windows.Forms.Button buttonSend;
        private System.Windows.Forms.TextBox textboxLog;
        private System.Windows.Forms.RadioButton radiobuttonAsFloat;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.NumericUpDown numericSlaveAddress;
        private System.Windows.Forms.NumericUpDown numericRegister;
        private System.Windows.Forms.NumericUpDown numericQuantity;
    }
}