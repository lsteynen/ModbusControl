namespace LST.Helpers
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
            panel1 = new Panel();
            checkboxLogTrace = new CheckBox();
            checkboxBridgeInfo = new CheckBox();
            checkboxLogBridgeDecode = new CheckBox();
            checkboxBridgeData = new CheckBox();
            checkboxLogWrites = new CheckBox();
            checkboxLogReads = new CheckBox();
            checkboxLogInfo = new CheckBox();
            checkboxLogWarnings = new CheckBox();
            checkboxLogErrors = new CheckBox();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(checkboxLogTrace);
            panel1.Controls.Add(checkboxBridgeInfo);
            panel1.Controls.Add(checkboxLogBridgeDecode);
            panel1.Controls.Add(checkboxBridgeData);
            panel1.Controls.Add(checkboxLogWrites);
            panel1.Controls.Add(checkboxLogReads);
            panel1.Controls.Add(checkboxLogInfo);
            panel1.Controls.Add(checkboxLogWarnings);
            panel1.Controls.Add(checkboxLogErrors);
            panel1.Location = new Point(0, 24);
            panel1.Name = "panel1";
            panel1.Size = new Size(120, 151);
            panel1.TabIndex = 0;
            // 
            // checkboxLogTrace
            // 
            checkboxLogTrace.AutoSize = true;
            checkboxLogTrace.Checked = true;
            checkboxLogTrace.CheckState = CheckState.Checked;
            checkboxLogTrace.Location = new Point(3, 51);
            checkboxLogTrace.Name = "checkboxLogTrace";
            checkboxLogTrace.Size = new Size(91, 17);
            checkboxLogTrace.TabIndex = 8;
            checkboxLogTrace.Text = "Trace TX/RX";
            checkboxLogTrace.UseVisualStyleBackColor = true;
            // 
            // checkboxBridgeInfo
            // 
            checkboxBridgeInfo.AutoSize = true;
            checkboxBridgeInfo.Location = new Point(3, 99);
            checkboxBridgeInfo.Name = "checkboxBridgeInfo";
            checkboxBridgeInfo.Size = new Size(77, 17);
            checkboxBridgeInfo.TabIndex = 7;
            checkboxBridgeInfo.Text = "Bridge Info";
            checkboxBridgeInfo.UseVisualStyleBackColor = true;
            // 
            // checkboxLogBridgeDecode
            // 
            checkboxLogBridgeDecode.AutoSize = true;
            checkboxLogBridgeDecode.Location = new Point(3, 131);
            checkboxLogBridgeDecode.Name = "checkboxLogBridgeDecode";
            checkboxLogBridgeDecode.Size = new Size(97, 17);
            checkboxLogBridgeDecode.TabIndex = 6;
            checkboxLogBridgeDecode.Text = "Bridge Decode";
            checkboxLogBridgeDecode.UseVisualStyleBackColor = true;
            // 
            // checkboxBridgeData
            // 
            checkboxBridgeData.AutoSize = true;
            checkboxBridgeData.Location = new Point(3, 115);
            checkboxBridgeData.Name = "checkboxBridgeData";
            checkboxBridgeData.Size = new Size(82, 17);
            checkboxBridgeData.TabIndex = 5;
            checkboxBridgeData.Text = "Bridge Data";
            checkboxBridgeData.UseVisualStyleBackColor = true;
            // 
            // checkboxLogWrites
            // 
            checkboxLogWrites.AutoSize = true;
            checkboxLogWrites.Location = new Point(3, 83);
            checkboxLogWrites.Name = "checkboxLogWrites";
            checkboxLogWrites.Size = new Size(88, 17);
            checkboxLogWrites.TabIndex = 4;
            checkboxLogWrites.Text = "Write queries";
            checkboxLogWrites.UseVisualStyleBackColor = true;
            // 
            // checkboxLogReads
            // 
            checkboxLogReads.AutoSize = true;
            checkboxLogReads.Location = new Point(3, 67);
            checkboxLogReads.Name = "checkboxLogReads";
            checkboxLogReads.Size = new Size(89, 17);
            checkboxLogReads.TabIndex = 3;
            checkboxLogReads.Text = "Read queries";
            checkboxLogReads.UseVisualStyleBackColor = true;
            // 
            // checkboxLogInfo
            // 
            checkboxLogInfo.AutoSize = true;
            checkboxLogInfo.Checked = true;
            checkboxLogInfo.CheckState = CheckState.Checked;
            checkboxLogInfo.Location = new Point(3, 35);
            checkboxLogInfo.Name = "checkboxLogInfo";
            checkboxLogInfo.Size = new Size(44, 17);
            checkboxLogInfo.TabIndex = 2;
            checkboxLogInfo.Text = "Info";
            checkboxLogInfo.UseVisualStyleBackColor = true;
            // 
            // checkboxLogWarnings
            // 
            checkboxLogWarnings.AutoSize = true;
            checkboxLogWarnings.Checked = true;
            checkboxLogWarnings.CheckState = CheckState.Checked;
            checkboxLogWarnings.Location = new Point(3, 19);
            checkboxLogWarnings.Name = "checkboxLogWarnings";
            checkboxLogWarnings.Size = new Size(71, 17);
            checkboxLogWarnings.TabIndex = 1;
            checkboxLogWarnings.Text = "Warnings";
            checkboxLogWarnings.UseVisualStyleBackColor = true;
            // 
            // checkboxLogErrors
            // 
            checkboxLogErrors.AutoSize = true;
            checkboxLogErrors.Checked = true;
            checkboxLogErrors.CheckState = CheckState.Checked;
            checkboxLogErrors.Location = new Point(3, 3);
            checkboxLogErrors.Name = "checkboxLogErrors";
            checkboxLogErrors.Size = new Size(53, 17);
            checkboxLogErrors.TabIndex = 0;
            checkboxLogErrors.Text = "Errors";
            checkboxLogErrors.UseVisualStyleBackColor = true;
            // 
            // LogFilter
            // 
            AnchorSize = new Size(122, 21);
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            Controls.Add(panel1);
            Name = "LogFilter";
            Size = new Size(122, 175);
            Tag = "";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);

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
