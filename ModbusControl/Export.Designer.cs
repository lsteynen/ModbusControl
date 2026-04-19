namespace ModbusControl
{
    partial class Export
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Export));
            this.buttonStart = new System.Windows.Forms.Button();
            this.textboxLog = new System.Windows.Forms.TextBox();
            this.radExportCurrentTab = new System.Windows.Forms.RadioButton();
            this.SaveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.SelectFileButton = new System.Windows.Forms.Button();
            this.panelTab = new System.Windows.Forms.Panel();
            this.radExportAllTabs = new System.Windows.Forms.RadioButton();
            this.textboxFilename = new System.Windows.Forms.TextBox();
            this.buttonClearLog = new System.Windows.Forms.Button();
            this.groupboxExportFormat = new System.Windows.Forms.GroupBox();
            this.radFormatDefault = new System.Windows.Forms.RadioButton();
            this.radFormatAsHex = new System.Windows.Forms.RadioButton();
            this.radFormatAsInt = new System.Windows.Forms.RadioButton();
            this.radFormatAsUInt = new System.Windows.Forms.RadioButton();
            this.StringFormatPanel = new System.Windows.Forms.Panel();
            this.radStringFormatAsUInt = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.radStringFormatDefault = new System.Windows.Forms.RadioButton();
            this.radStringFormatAsInt = new System.Windows.Forms.RadioButton();
            this.radStringFormatAsHex = new System.Windows.Forms.RadioButton();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.butRunTests = new System.Windows.Forms.Button();
            this.chkContinueOnError = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panelTab.SuspendLayout();
            this.groupboxExportFormat.SuspendLayout();
            this.StringFormatPanel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonStart
            // 
            this.buttonStart.Location = new System.Drawing.Point(13, 110);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(150, 23);
            this.buttonStart.TabIndex = 5;
            this.buttonStart.Text = "Start";
            this.toolTip1.SetToolTip(this.buttonStart, "Start the export");
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // textboxLog
            // 
            this.textboxLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textboxLog.Location = new System.Drawing.Point(0, 0);
            this.textboxLog.Multiline = true;
            this.textboxLog.Name = "textboxLog";
            this.textboxLog.ReadOnly = true;
            this.textboxLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textboxLog.Size = new System.Drawing.Size(619, 239);
            this.textboxLog.TabIndex = 0;
            this.textboxLog.WordWrap = false;
            // 
            // radExportCurrentTab
            // 
            this.radExportCurrentTab.AutoSize = true;
            this.radExportCurrentTab.Checked = true;
            this.radExportCurrentTab.Location = new System.Drawing.Point(6, 3);
            this.radExportCurrentTab.Name = "radExportCurrentTab";
            this.radExportCurrentTab.Size = new System.Drawing.Size(163, 17);
            this.radExportCurrentTab.TabIndex = 0;
            this.radExportCurrentTab.TabStop = true;
            this.radExportCurrentTab.Tag = "";
            this.radExportCurrentTab.Text = "Export registers of current tab";
            this.toolTip1.SetToolTip(this.radExportCurrentTab, "Export only the registers of the currently active tab page");
            this.radExportCurrentTab.UseVisualStyleBackColor = true;
            // 
            // SaveFileDialog1
            // 
            this.SaveFileDialog1.AddExtension = false;
            this.SaveFileDialog1.DefaultExt = "csv";
            this.SaveFileDialog1.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
            // 
            // SelectFileButton
            // 
            this.SelectFileButton.Location = new System.Drawing.Point(13, 81);
            this.SelectFileButton.Name = "SelectFileButton";
            this.SelectFileButton.Size = new System.Drawing.Size(75, 23);
            this.SelectFileButton.TabIndex = 3;
            this.SelectFileButton.Text = "Select file...";
            this.toolTip1.SetToolTip(this.SelectFileButton, "Select the file to export to");
            this.SelectFileButton.UseVisualStyleBackColor = true;
            this.SelectFileButton.Click += new System.EventHandler(this.buttonSelectFile_Click);
            // 
            // panelTab
            // 
            this.panelTab.Controls.Add(this.radExportAllTabs);
            this.panelTab.Controls.Add(this.radExportCurrentTab);
            this.panelTab.Location = new System.Drawing.Point(7, 9);
            this.panelTab.Name = "panelTab";
            this.panelTab.Size = new System.Drawing.Size(189, 43);
            this.panelTab.TabIndex = 0;
            // 
            // radExportAllTabs
            // 
            this.radExportAllTabs.AutoSize = true;
            this.radExportAllTabs.Location = new System.Drawing.Point(6, 23);
            this.radExportAllTabs.Name = "radExportAllTabs";
            this.radExportAllTabs.Size = new System.Drawing.Size(145, 17);
            this.radExportAllTabs.TabIndex = 1;
            this.radExportAllTabs.Tag = "";
            this.radExportAllTabs.Text = "Export registers of all tabs";
            this.toolTip1.SetToolTip(this.radExportAllTabs, "Export the registers of all tab pages");
            this.radExportAllTabs.UseVisualStyleBackColor = true;
            // 
            // textboxFilename
            // 
            this.textboxFilename.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textboxFilename.Location = new System.Drawing.Point(94, 83);
            this.textboxFilename.Name = "textboxFilename";
            this.textboxFilename.Size = new System.Drawing.Size(513, 20);
            this.textboxFilename.TabIndex = 4;
            // 
            // buttonClearLog
            // 
            this.buttonClearLog.Location = new System.Drawing.Point(250, 110);
            this.buttonClearLog.Name = "buttonClearLog";
            this.buttonClearLog.Size = new System.Drawing.Size(75, 23);
            this.buttonClearLog.TabIndex = 7;
            this.buttonClearLog.Text = "Clear log";
            this.toolTip1.SetToolTip(this.buttonClearLog, "Clear the result log window");
            this.buttonClearLog.UseVisualStyleBackColor = true;
            this.buttonClearLog.Click += new System.EventHandler(this.buttonClearLog_Click);
            // 
            // groupboxExportFormat
            // 
            this.groupboxExportFormat.Controls.Add(this.radFormatDefault);
            this.groupboxExportFormat.Controls.Add(this.radFormatAsHex);
            this.groupboxExportFormat.Controls.Add(this.radFormatAsInt);
            this.groupboxExportFormat.Controls.Add(this.radFormatAsUInt);
            this.groupboxExportFormat.Controls.Add(this.StringFormatPanel);
            this.groupboxExportFormat.Location = new System.Drawing.Point(201, 3);
            this.groupboxExportFormat.Name = "groupboxExportFormat";
            this.groupboxExportFormat.Size = new System.Drawing.Size(384, 57);
            this.groupboxExportFormat.TabIndex = 1;
            this.groupboxExportFormat.TabStop = false;
            this.groupboxExportFormat.Text = "Export values format:";
            // 
            // radFormatDefault
            // 
            this.radFormatDefault.AutoSize = true;
            this.radFormatDefault.Checked = true;
            this.radFormatDefault.Location = new System.Drawing.Point(7, 17);
            this.radFormatDefault.Name = "radFormatDefault";
            this.radFormatDefault.Size = new System.Drawing.Size(68, 17);
            this.radFormatDefault.TabIndex = 0;
            this.radFormatDefault.TabStop = true;
            this.radFormatDefault.Tag = "";
            this.radFormatDefault.Text = "As in grid";
            this.toolTip1.SetToolTip(this.radFormatDefault, "Export register values with the format as defined in the project");
            this.radFormatDefault.UseVisualStyleBackColor = true;
            this.radFormatDefault.CheckedChanged += new System.EventHandler(this.radFormat_CheckedChanged);
            // 
            // radFormatAsHex
            // 
            this.radFormatAsHex.AutoSize = true;
            this.radFormatAsHex.Location = new System.Drawing.Point(226, 15);
            this.radFormatAsHex.Name = "radFormatAsHex";
            this.radFormatAsHex.Size = new System.Drawing.Size(59, 17);
            this.radFormatAsHex.TabIndex = 3;
            this.radFormatAsHex.Tag = "";
            this.radFormatAsHex.Text = "As Hex";
            this.toolTip1.SetToolTip(this.radFormatAsHex, "Export all register values in hexadecimal");
            this.radFormatAsHex.UseVisualStyleBackColor = true;
            this.radFormatAsHex.CheckedChanged += new System.EventHandler(this.radFormat_CheckedChanged);
            // 
            // radFormatAsInt
            // 
            this.radFormatAsInt.AutoSize = true;
            this.radFormatAsInt.Location = new System.Drawing.Point(80, 15);
            this.radFormatAsInt.Name = "radFormatAsInt";
            this.radFormatAsInt.Size = new System.Drawing.Size(52, 17);
            this.radFormatAsInt.TabIndex = 1;
            this.radFormatAsInt.Tag = "";
            this.radFormatAsInt.Text = "As Int";
            this.toolTip1.SetToolTip(this.radFormatAsInt, "Export all register values as signed integers");
            this.radFormatAsInt.UseVisualStyleBackColor = true;
            this.radFormatAsInt.CheckedChanged += new System.EventHandler(this.radFormat_CheckedChanged);
            // 
            // radFormatAsUInt
            // 
            this.radFormatAsUInt.AutoSize = true;
            this.radFormatAsUInt.Location = new System.Drawing.Point(149, 15);
            this.radFormatAsUInt.Name = "radFormatAsUInt";
            this.radFormatAsUInt.Size = new System.Drawing.Size(60, 17);
            this.radFormatAsUInt.TabIndex = 2;
            this.radFormatAsUInt.Tag = "";
            this.radFormatAsUInt.Text = "As UInt";
            this.toolTip1.SetToolTip(this.radFormatAsUInt, "Export all register values as unsigned integers");
            this.radFormatAsUInt.UseVisualStyleBackColor = true;
            this.radFormatAsUInt.CheckedChanged += new System.EventHandler(this.radFormat_CheckedChanged);
            // 
            // StringFormatPanel
            // 
            this.StringFormatPanel.Controls.Add(this.radStringFormatAsUInt);
            this.StringFormatPanel.Controls.Add(this.label1);
            this.StringFormatPanel.Controls.Add(this.radStringFormatDefault);
            this.StringFormatPanel.Controls.Add(this.radStringFormatAsInt);
            this.StringFormatPanel.Controls.Add(this.radStringFormatAsHex);
            this.StringFormatPanel.Location = new System.Drawing.Point(1, 35);
            this.StringFormatPanel.Name = "StringFormatPanel";
            this.StringFormatPanel.Size = new System.Drawing.Size(387, 21);
            this.StringFormatPanel.TabIndex = 4;
            // 
            // radStringFormatAsUInt
            // 
            this.radStringFormatAsUInt.AutoSize = true;
            this.radStringFormatAsUInt.Location = new System.Drawing.Point(260, 0);
            this.radStringFormatAsUInt.Name = "radStringFormatAsUInt";
            this.radStringFormatAsUInt.Size = new System.Drawing.Size(60, 17);
            this.radStringFormatAsUInt.TabIndex = 3;
            this.radStringFormatAsUInt.Tag = "";
            this.radStringFormatAsUInt.Text = "As UInt";
            this.toolTip1.SetToolTip(this.radStringFormatAsUInt, "Export all register values as signed integers");
            this.radStringFormatAsUInt.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(114, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Export string/ASCII as:";
            // 
            // radStringFormatDefault
            // 
            this.radStringFormatDefault.AutoSize = true;
            this.radStringFormatDefault.Checked = true;
            this.radStringFormatDefault.Location = new System.Drawing.Point(119, 0);
            this.radStringFormatDefault.Name = "radStringFormatDefault";
            this.radStringFormatDefault.Size = new System.Drawing.Size(84, 17);
            this.radStringFormatDefault.TabIndex = 1;
            this.radStringFormatDefault.TabStop = true;
            this.radStringFormatDefault.Tag = "";
            this.radStringFormatDefault.Text = "String/ASCII";
            this.toolTip1.SetToolTip(this.radStringFormatDefault, "Export register values with the format as defined in the project");
            this.radStringFormatDefault.UseVisualStyleBackColor = true;
            // 
            // radStringFormatAsInt
            // 
            this.radStringFormatAsInt.AutoSize = true;
            this.radStringFormatAsInt.Location = new System.Drawing.Point(205, 0);
            this.radStringFormatAsInt.Name = "radStringFormatAsInt";
            this.radStringFormatAsInt.Size = new System.Drawing.Size(52, 17);
            this.radStringFormatAsInt.TabIndex = 2;
            this.radStringFormatAsInt.Tag = "";
            this.radStringFormatAsInt.Text = "As Int";
            this.toolTip1.SetToolTip(this.radStringFormatAsInt, "Export all register values as signed integers");
            this.radStringFormatAsInt.UseVisualStyleBackColor = true;
            // 
            // radStringFormatAsHex
            // 
            this.radStringFormatAsHex.AutoSize = true;
            this.radStringFormatAsHex.Location = new System.Drawing.Point(325, 0);
            this.radStringFormatAsHex.Name = "radStringFormatAsHex";
            this.radStringFormatAsHex.Size = new System.Drawing.Size(59, 17);
            this.radStringFormatAsHex.TabIndex = 4;
            this.radStringFormatAsHex.Tag = "";
            this.radStringFormatAsHex.Text = "As Hex";
            this.toolTip1.SetToolTip(this.radStringFormatAsHex, "Export all register values as unsigned integers");
            this.radStringFormatAsHex.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Enabled = false;
            this.buttonCancel.Location = new System.Drawing.Point(169, 110);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 6;
            this.buttonCancel.Text = "Cancel";
            this.toolTip1.SetToolTip(this.buttonCancel, "Cancel the current operation");
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // butRunTests
            // 
            this.butRunTests.Location = new System.Drawing.Point(331, 110);
            this.butRunTests.Name = "butRunTests";
            this.butRunTests.Size = new System.Drawing.Size(75, 23);
            this.butRunTests.TabIndex = 8;
            this.butRunTests.Text = "Run test";
            this.butRunTests.UseVisualStyleBackColor = true;
            this.butRunTests.Visible = false;
            this.butRunTests.Click += new System.EventHandler(this.buttonTest_Click);
            // 
            // chkContinueOnError
            // 
            this.chkContinueOnError.AutoSize = true;
            this.chkContinueOnError.Location = new System.Drawing.Point(14, 58);
            this.chkContinueOnError.Name = "chkContinueOnError";
            this.chkContinueOnError.Size = new System.Drawing.Size(107, 17);
            this.chkContinueOnError.TabIndex = 2;
            this.chkContinueOnError.Text = "Continue on error";
            this.chkContinueOnError.UseVisualStyleBackColor = true;
            this.chkContinueOnError.CheckedChanged += new System.EventHandler(this.chkContinueOnError_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.buttonStart);
            this.panel1.Controls.Add(this.chkContinueOnError);
            this.panel1.Controls.Add(this.SelectFileButton);
            this.panel1.Controls.Add(this.butRunTests);
            this.panel1.Controls.Add(this.panelTab);
            this.panel1.Controls.Add(this.groupboxExportFormat);
            this.panel1.Controls.Add(this.textboxFilename);
            this.panel1.Controls.Add(this.buttonCancel);
            this.panel1.Controls.Add(this.buttonClearLog);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(619, 143);
            this.panel1.TabIndex = 10;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.textboxLog);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 143);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(619, 239);
            this.panel2.TabIndex = 11;
            // 
            // Export
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(619, 382);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Export";
            this.Text = "Export registers to file";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnFormClosing);
            this.panelTab.ResumeLayout(false);
            this.panelTab.PerformLayout();
            this.groupboxExportFormat.ResumeLayout(false);
            this.groupboxExportFormat.PerformLayout();
            this.StringFormatPanel.ResumeLayout(false);
            this.StringFormatPanel.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.TextBox textboxLog;
        private System.Windows.Forms.RadioButton radExportCurrentTab;
        private System.Windows.Forms.SaveFileDialog SaveFileDialog1;
        private System.Windows.Forms.Button SelectFileButton;
        private System.Windows.Forms.Panel panelTab;
        private System.Windows.Forms.RadioButton radExportAllTabs;
        private System.Windows.Forms.TextBox textboxFilename;
        private System.Windows.Forms.Button buttonClearLog;
        private System.Windows.Forms.GroupBox groupboxExportFormat;
        private System.Windows.Forms.RadioButton radFormatDefault;
        private System.Windows.Forms.RadioButton radFormatAsHex;
        private System.Windows.Forms.RadioButton radFormatAsInt;
        private System.Windows.Forms.RadioButton radFormatAsUInt;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Panel StringFormatPanel;
        private System.Windows.Forms.RadioButton radStringFormatDefault;
        private System.Windows.Forms.RadioButton radStringFormatAsInt;
        private System.Windows.Forms.RadioButton radStringFormatAsHex;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton radStringFormatAsUInt;
        private System.Windows.Forms.Button butRunTests;
        private System.Windows.Forms.CheckBox chkContinueOnError;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
    }
}