namespace ModbusControlSpace
{
    partial class LogFilter
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.checkboxLogTrace = new System.Windows.Forms.CheckBox();
            this.checkboxBridgeInfo = new System.Windows.Forms.CheckBox();
            this.checkboxLogBridgeDecode = new System.Windows.Forms.CheckBox();
            this.checkboxBridgeData = new System.Windows.Forms.CheckBox();
            this.checkboxLogWrites = new System.Windows.Forms.CheckBox();
            this.checkboxLogReads = new System.Windows.Forms.CheckBox();
            this.checkboxLogInfo = new System.Windows.Forms.CheckBox();
            this.checkboxLogWarnings = new System.Windows.Forms.CheckBox();
            this.checkboxLogErrors = new System.Windows.Forms.CheckBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.checkboxLogTrace);
            this.panel1.Controls.Add(this.checkboxBridgeInfo);
            this.panel1.Controls.Add(this.checkboxLogBridgeDecode);
            this.panel1.Controls.Add(this.checkboxBridgeData);
            this.panel1.Controls.Add(this.checkboxLogWrites);
            this.panel1.Controls.Add(this.checkboxLogReads);
            this.panel1.Controls.Add(this.checkboxLogInfo);
            this.panel1.Controls.Add(this.checkboxLogWarnings);
            this.panel1.Controls.Add(this.checkboxLogErrors);
            this.panel1.Location = new System.Drawing.Point(0, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(120, 151);
            this.panel1.TabIndex = 0;
            // 
            // checkboxLogTrace
            // 
            this.checkboxLogTrace.AutoSize = true;
            this.checkboxLogTrace.Checked = true;
            this.checkboxLogTrace.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkboxLogTrace.Location = new System.Drawing.Point(3, 51);
            this.checkboxLogTrace.Name = "checkboxLogTrace";
            this.checkboxLogTrace.Size = new System.Drawing.Size(91, 17);
            this.checkboxLogTrace.TabIndex = 8;
            this.checkboxLogTrace.Text = "Trace TX/RX";
            this.checkboxLogTrace.UseVisualStyleBackColor = true;
            // 
            // checkboxBridgeInfo
            // 
            this.checkboxBridgeInfo.AutoSize = true;
            this.checkboxBridgeInfo.Location = new System.Drawing.Point(3, 99);
            this.checkboxBridgeInfo.Name = "checkboxBridgeInfo";
            this.checkboxBridgeInfo.Size = new System.Drawing.Size(77, 17);
            this.checkboxBridgeInfo.TabIndex = 7;
            this.checkboxBridgeInfo.Text = "Bridge Info";
            this.checkboxBridgeInfo.UseVisualStyleBackColor = true;
            // 
            // checkboxLogBridgeDecode
            // 
            this.checkboxLogBridgeDecode.AutoSize = true;
            this.checkboxLogBridgeDecode.Location = new System.Drawing.Point(3, 131);
            this.checkboxLogBridgeDecode.Name = "checkboxLogBridgeDecode";
            this.checkboxLogBridgeDecode.Size = new System.Drawing.Size(97, 17);
            this.checkboxLogBridgeDecode.TabIndex = 6;
            this.checkboxLogBridgeDecode.Text = "Bridge Decode";
            this.checkboxLogBridgeDecode.UseVisualStyleBackColor = true;
            // 
            // checkboxBridgeData
            // 
            this.checkboxBridgeData.AutoSize = true;
            this.checkboxBridgeData.Location = new System.Drawing.Point(3, 115);
            this.checkboxBridgeData.Name = "checkboxBridgeData";
            this.checkboxBridgeData.Size = new System.Drawing.Size(82, 17);
            this.checkboxBridgeData.TabIndex = 5;
            this.checkboxBridgeData.Text = "Bridge Data";
            this.checkboxBridgeData.UseVisualStyleBackColor = true;
            // 
            // checkboxLogWrites
            // 
            this.checkboxLogWrites.AutoSize = true;
            this.checkboxLogWrites.Location = new System.Drawing.Point(3, 83);
            this.checkboxLogWrites.Name = "checkboxLogWrites";
            this.checkboxLogWrites.Size = new System.Drawing.Size(88, 17);
            this.checkboxLogWrites.TabIndex = 4;
            this.checkboxLogWrites.Text = "Write queries";
            this.checkboxLogWrites.UseVisualStyleBackColor = true;
            // 
            // checkboxLogReads
            // 
            this.checkboxLogReads.AutoSize = true;
            this.checkboxLogReads.Location = new System.Drawing.Point(3, 67);
            this.checkboxLogReads.Name = "checkboxLogReads";
            this.checkboxLogReads.Size = new System.Drawing.Size(89, 17);
            this.checkboxLogReads.TabIndex = 3;
            this.checkboxLogReads.Text = "Read queries";
            this.checkboxLogReads.UseVisualStyleBackColor = true;
            // 
            // checkboxLogInfo
            // 
            this.checkboxLogInfo.AutoSize = true;
            this.checkboxLogInfo.Checked = true;
            this.checkboxLogInfo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkboxLogInfo.Location = new System.Drawing.Point(3, 35);
            this.checkboxLogInfo.Name = "checkboxLogInfo";
            this.checkboxLogInfo.Size = new System.Drawing.Size(44, 17);
            this.checkboxLogInfo.TabIndex = 2;
            this.checkboxLogInfo.Text = "Info";
            this.checkboxLogInfo.UseVisualStyleBackColor = true;
            // 
            // checkboxLogWarnings
            // 
            this.checkboxLogWarnings.AutoSize = true;
            this.checkboxLogWarnings.Checked = true;
            this.checkboxLogWarnings.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkboxLogWarnings.Location = new System.Drawing.Point(3, 19);
            this.checkboxLogWarnings.Name = "checkboxLogWarnings";
            this.checkboxLogWarnings.Size = new System.Drawing.Size(71, 17);
            this.checkboxLogWarnings.TabIndex = 1;
            this.checkboxLogWarnings.Text = "Warnings";
            this.checkboxLogWarnings.UseVisualStyleBackColor = true;
            // 
            // checkboxLogErrors
            // 
            this.checkboxLogErrors.AutoSize = true;
            this.checkboxLogErrors.Checked = true;
            this.checkboxLogErrors.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkboxLogErrors.Location = new System.Drawing.Point(3, 3);
            this.checkboxLogErrors.Name = "checkboxLogErrors";
            this.checkboxLogErrors.Size = new System.Drawing.Size(53, 17);
            this.checkboxLogErrors.TabIndex = 0;
            this.checkboxLogErrors.Text = "Errors";
            this.checkboxLogErrors.UseVisualStyleBackColor = true;
            // 
            // LogFilter
            // 
            this.AnchorSize = new System.Drawing.Size(122, 21);
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.panel1);
            this.Name = "LogFilter";
            this.Size = new System.Drawing.Size(122, 175);
            this.Tag = "";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox checkboxLogInfo;
        private System.Windows.Forms.CheckBox checkboxLogWarnings;
        private System.Windows.Forms.CheckBox checkboxLogErrors;
        private System.Windows.Forms.CheckBox checkboxLogBridgeDecode;
        private System.Windows.Forms.CheckBox checkboxBridgeData;
        private System.Windows.Forms.CheckBox checkboxLogWrites;
        private System.Windows.Forms.CheckBox checkboxLogReads;
        private System.Windows.Forms.CheckBox checkboxBridgeInfo;
        private System.Windows.Forms.CheckBox checkboxLogTrace;

    }
}
