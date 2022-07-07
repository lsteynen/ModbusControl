namespace ModbusControlSpace
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
            this.radiobuttonExportCurrentTab = new System.Windows.Forms.RadioButton();
            this.SaveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.SelectFileButton = new System.Windows.Forms.Button();
            this.panelTab = new System.Windows.Forms.Panel();
            this.radiobuttonExportAllTabs = new System.Windows.Forms.RadioButton();
            this.textboxFilename = new System.Windows.Forms.TextBox();
            this.buttonClearLog = new System.Windows.Forms.Button();
            this.groupboxExportFormat = new System.Windows.Forms.GroupBox();
            this.radiobuttonAsInGrid = new System.Windows.Forms.RadioButton();
            this.radiobuttonAsHex = new System.Windows.Forms.RadioButton();
            this.radiobuttonAsInt = new System.Windows.Forms.RadioButton();
            this.radiobuttonAsUInt = new System.Windows.Forms.RadioButton();
            this.FormatAsciiPanel = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.radExportAscii = new System.Windows.Forms.RadioButton();
            this.radExportAsciiAsInt = new System.Windows.Forms.RadioButton();
            this.radExportAsciiAsHex = new System.Windows.Forms.RadioButton();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panelTab.SuspendLayout();
            this.groupboxExportFormat.SuspendLayout();
            this.FormatAsciiPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonStart
            // 
            this.buttonStart.Location = new System.Drawing.Point(15, 124);
            this.buttonStart.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(188, 29);
            this.buttonStart.TabIndex = 4;
            this.buttonStart.Text = "Start";
            this.toolTip1.SetToolTip(this.buttonStart, "Start the export");
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // textboxLog
            // 
            this.textboxLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textboxLog.Location = new System.Drawing.Point(15, 160);
            this.textboxLog.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textboxLog.Multiline = true;
            this.textboxLog.Name = "textboxLog";
            this.textboxLog.ReadOnly = true;
            this.textboxLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textboxLog.Size = new System.Drawing.Size(649, 276);
            this.textboxLog.TabIndex = 7;
            this.textboxLog.WordWrap = false;
            // 
            // radiobuttonExportCurrentTab
            // 
            this.radiobuttonExportCurrentTab.AutoSize = true;
            this.radiobuttonExportCurrentTab.Checked = true;
            this.radiobuttonExportCurrentTab.Location = new System.Drawing.Point(4, 4);
            this.radiobuttonExportCurrentTab.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.radiobuttonExportCurrentTab.Name = "radiobuttonExportCurrentTab";
            this.radiobuttonExportCurrentTab.Size = new System.Drawing.Size(255, 21);
            this.radiobuttonExportCurrentTab.TabIndex = 0;
            this.radiobuttonExportCurrentTab.TabStop = true;
            this.radiobuttonExportCurrentTab.Tag = "current";
            this.radiobuttonExportCurrentTab.Text = "Export registers of current tab to file";
            this.toolTip1.SetToolTip(this.radiobuttonExportCurrentTab, "Export only the registers of the currently active tab page");
            this.radiobuttonExportCurrentTab.UseVisualStyleBackColor = true;
            // 
            // SaveFileDialog1
            // 
            this.SaveFileDialog1.AddExtension = false;
            this.SaveFileDialog1.DefaultExt = "csv";
            this.SaveFileDialog1.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
            // 
            // SelectFileButton
            // 
            this.SelectFileButton.Location = new System.Drawing.Point(15, 86);
            this.SelectFileButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.SelectFileButton.Name = "SelectFileButton";
            this.SelectFileButton.Size = new System.Drawing.Size(94, 29);
            this.SelectFileButton.TabIndex = 2;
            this.SelectFileButton.Text = "Select file...";
            this.toolTip1.SetToolTip(this.SelectFileButton, "Select the file to export to");
            this.SelectFileButton.UseVisualStyleBackColor = true;
            this.SelectFileButton.Click += new System.EventHandler(this.buttonSelectFile_Click);
            // 
            // panelTab
            // 
            this.panelTab.Controls.Add(this.radiobuttonExportAllTabs);
            this.panelTab.Controls.Add(this.radiobuttonExportCurrentTab);
            this.panelTab.Location = new System.Drawing.Point(15, 15);
            this.panelTab.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelTab.Name = "panelTab";
            this.panelTab.Size = new System.Drawing.Size(256, 54);
            this.panelTab.TabIndex = 0;
            // 
            // radiobuttonExportAllTabs
            // 
            this.radiobuttonExportAllTabs.AutoSize = true;
            this.radiobuttonExportAllTabs.Location = new System.Drawing.Point(4, 26);
            this.radiobuttonExportAllTabs.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.radiobuttonExportAllTabs.Name = "radiobuttonExportAllTabs";
            this.radiobuttonExportAllTabs.Size = new System.Drawing.Size(231, 21);
            this.radiobuttonExportAllTabs.TabIndex = 1;
            this.radiobuttonExportAllTabs.Tag = "all";
            this.radiobuttonExportAllTabs.Text = "Export registers of all tabs to file";
            this.toolTip1.SetToolTip(this.radiobuttonExportAllTabs, "Export the registers of all tab pages");
            this.radiobuttonExportAllTabs.UseVisualStyleBackColor = true;
            // 
            // textboxFilename
            // 
            this.textboxFilename.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textboxFilename.Location = new System.Drawing.Point(116, 89);
            this.textboxFilename.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textboxFilename.Name = "textboxFilename";
            this.textboxFilename.ReadOnly = true;
            this.textboxFilename.Size = new System.Drawing.Size(548, 22);
            this.textboxFilename.TabIndex = 3;
            // 
            // buttonClearLog
            // 
            this.buttonClearLog.Location = new System.Drawing.Point(311, 124);
            this.buttonClearLog.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonClearLog.Name = "buttonClearLog";
            this.buttonClearLog.Size = new System.Drawing.Size(94, 29);
            this.buttonClearLog.TabIndex = 6;
            this.buttonClearLog.Text = "Clear log";
            this.toolTip1.SetToolTip(this.buttonClearLog, "Clear the result log window");
            this.buttonClearLog.UseVisualStyleBackColor = true;
            this.buttonClearLog.Click += new System.EventHandler(this.buttonClearLog_Click);
            // 
            // groupboxExportFormat
            // 
            this.groupboxExportFormat.Controls.Add(this.radiobuttonAsInGrid);
            this.groupboxExportFormat.Controls.Add(this.radiobuttonAsHex);
            this.groupboxExportFormat.Controls.Add(this.radiobuttonAsInt);
            this.groupboxExportFormat.Controls.Add(this.radiobuttonAsUInt);
            this.groupboxExportFormat.Controls.Add(this.FormatAsciiPanel);
            this.groupboxExportFormat.Location = new System.Drawing.Point(279, 12);
            this.groupboxExportFormat.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupboxExportFormat.Name = "groupboxExportFormat";
            this.groupboxExportFormat.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupboxExportFormat.Size = new System.Drawing.Size(340, 72);
            this.groupboxExportFormat.TabIndex = 1;
            this.groupboxExportFormat.TabStop = false;
            this.groupboxExportFormat.Text = "Export values format:";
            // 
            // radiobuttonAsInGrid
            // 
            this.radiobuttonAsInGrid.AutoSize = true;
            this.radiobuttonAsInGrid.Checked = true;
            this.radiobuttonAsInGrid.Location = new System.Drawing.Point(8, 19);
            this.radiobuttonAsInGrid.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.radiobuttonAsInGrid.Name = "radiobuttonAsInGrid";
            this.radiobuttonAsInGrid.Size = new System.Drawing.Size(88, 21);
            this.radiobuttonAsInGrid.TabIndex = 0;
            this.radiobuttonAsInGrid.TabStop = true;
            this.radiobuttonAsInGrid.Tag = "grid";
            this.radiobuttonAsInGrid.Text = "As in grid";
            this.toolTip1.SetToolTip(this.radiobuttonAsInGrid, "Export register values with the format as defined in the project");
            this.radiobuttonAsInGrid.UseVisualStyleBackColor = true;
            this.radiobuttonAsInGrid.CheckedChanged += new System.EventHandler(this.radiobuttonFormat_CheckedChanged);
            // 
            // radiobuttonAsHex
            // 
            this.radiobuttonAsHex.AutoSize = true;
            this.radiobuttonAsHex.Location = new System.Drawing.Point(256, 19);
            this.radiobuttonAsHex.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.radiobuttonAsHex.Name = "radiobuttonAsHex";
            this.radiobuttonAsHex.Size = new System.Drawing.Size(72, 21);
            this.radiobuttonAsHex.TabIndex = 3;
            this.radiobuttonAsHex.Tag = "hex";
            this.radiobuttonAsHex.Text = "All Hex";
            this.toolTip1.SetToolTip(this.radiobuttonAsHex, "Export all register values in hexadecimal");
            this.radiobuttonAsHex.UseVisualStyleBackColor = true;
            this.radiobuttonAsHex.CheckedChanged += new System.EventHandler(this.radiobuttonFormat_CheckedChanged);
            // 
            // radiobuttonAsInt
            // 
            this.radiobuttonAsInt.AutoSize = true;
            this.radiobuttonAsInt.Location = new System.Drawing.Point(110, 19);
            this.radiobuttonAsInt.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.radiobuttonAsInt.Name = "radiobuttonAsInt";
            this.radiobuttonAsInt.Size = new System.Drawing.Size(63, 21);
            this.radiobuttonAsInt.TabIndex = 1;
            this.radiobuttonAsInt.Tag = "int";
            this.radiobuttonAsInt.Text = "All Int";
            this.toolTip1.SetToolTip(this.radiobuttonAsInt, "Export all register values as signed integers");
            this.radiobuttonAsInt.UseVisualStyleBackColor = true;
            this.radiobuttonAsInt.CheckedChanged += new System.EventHandler(this.radiobuttonFormat_CheckedChanged);
            // 
            // radiobuttonAsUInt
            // 
            this.radiobuttonAsUInt.AutoSize = true;
            this.radiobuttonAsUInt.Location = new System.Drawing.Point(178, 19);
            this.radiobuttonAsUInt.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.radiobuttonAsUInt.Name = "radiobuttonAsUInt";
            this.radiobuttonAsUInt.Size = new System.Drawing.Size(73, 21);
            this.radiobuttonAsUInt.TabIndex = 2;
            this.radiobuttonAsUInt.Tag = "uint";
            this.radiobuttonAsUInt.Text = "All UInt";
            this.toolTip1.SetToolTip(this.radiobuttonAsUInt, "Export all register values as unsigned integers");
            this.radiobuttonAsUInt.UseVisualStyleBackColor = true;
            this.radiobuttonAsUInt.CheckedChanged += new System.EventHandler(this.radiobuttonFormat_CheckedChanged);
            // 
            // FormatAsciiPanel
            // 
            this.FormatAsciiPanel.Controls.Add(this.label1);
            this.FormatAsciiPanel.Controls.Add(this.radExportAscii);
            this.FormatAsciiPanel.Controls.Add(this.radExportAsciiAsInt);
            this.FormatAsciiPanel.Controls.Add(this.radExportAsciiAsHex);
            this.FormatAsciiPanel.Location = new System.Drawing.Point(1, 44);
            this.FormatAsciiPanel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.FormatAsciiPanel.Name = "FormatAsciiPanel";
            this.FormatAsciiPanel.Size = new System.Drawing.Size(335, 26);
            this.FormatAsciiPanel.TabIndex = 26;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 2);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Export ASCII as:";
            // 
            // radExportAscii
            // 
            this.radExportAscii.AutoSize = true;
            this.radExportAscii.Checked = true;
            this.radExportAscii.Location = new System.Drawing.Point(109, 0);
            this.radExportAscii.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.radExportAscii.Name = "radExportAscii";
            this.radExportAscii.Size = new System.Drawing.Size(62, 21);
            this.radExportAscii.TabIndex = 1;
            this.radExportAscii.TabStop = true;
            this.radExportAscii.Tag = "ascii";
            this.radExportAscii.Text = "ASCII";
            this.toolTip1.SetToolTip(this.radExportAscii, "Export register values with the format as defined in the project");
            this.radExportAscii.UseVisualStyleBackColor = true;
            // 
            // radExportAsciiAsInt
            // 
            this.radExportAsciiAsInt.AutoSize = true;
            this.radExportAsciiAsInt.Location = new System.Drawing.Point(176, 0);
            this.radExportAsciiAsInt.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.radExportAsciiAsInt.Name = "radExportAsciiAsInt";
            this.radExportAsciiAsInt.Size = new System.Drawing.Size(64, 21);
            this.radExportAsciiAsInt.TabIndex = 2;
            this.radExportAsciiAsInt.Tag = "int";
            this.radExportAsciiAsInt.Text = "As Int";
            this.toolTip1.SetToolTip(this.radExportAsciiAsInt, "Export all register values as signed integers");
            this.radExportAsciiAsInt.UseVisualStyleBackColor = true;
            // 
            // radExportAsciiAsHex
            // 
            this.radExportAsciiAsHex.AutoSize = true;
            this.radExportAsciiAsHex.Location = new System.Drawing.Point(255, 0);
            this.radExportAsciiAsHex.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.radExportAsciiAsHex.Name = "radExportAsciiAsHex";
            this.radExportAsciiAsHex.Size = new System.Drawing.Size(73, 21);
            this.radExportAsciiAsHex.TabIndex = 3;
            this.radExportAsciiAsHex.Tag = "hex";
            this.radExportAsciiAsHex.Text = "As Hex";
            this.toolTip1.SetToolTip(this.radExportAsciiAsHex, "Export all register values as unsigned integers");
            this.radExportAsciiAsHex.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Enabled = false;
            this.buttonCancel.Location = new System.Drawing.Point(210, 124);
            this.buttonCancel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(94, 29);
            this.buttonCancel.TabIndex = 5;
            this.buttonCancel.Text = "Cancel";
            this.toolTip1.SetToolTip(this.buttonCancel, "Cancel the current operation");
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // Export
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(680, 452);
            this.Controls.Add(this.groupboxExportFormat);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonClearLog);
            this.Controls.Add(this.textboxFilename);
            this.Controls.Add(this.panelTab);
            this.Controls.Add(this.SelectFileButton);
            this.Controls.Add(this.textboxLog);
            this.Controls.Add(this.buttonStart);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Export";
            this.Text = "Export registers to file";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnFormClosing);
            this.panelTab.ResumeLayout(false);
            this.panelTab.PerformLayout();
            this.groupboxExportFormat.ResumeLayout(false);
            this.groupboxExportFormat.PerformLayout();
            this.FormatAsciiPanel.ResumeLayout(false);
            this.FormatAsciiPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.TextBox textboxLog;
        private System.Windows.Forms.RadioButton radiobuttonExportCurrentTab;
        private System.Windows.Forms.SaveFileDialog SaveFileDialog1;
        private System.Windows.Forms.Button SelectFileButton;
        private System.Windows.Forms.Panel panelTab;
        private System.Windows.Forms.RadioButton radiobuttonExportAllTabs;
        private System.Windows.Forms.TextBox textboxFilename;
        private System.Windows.Forms.Button buttonClearLog;
        private System.Windows.Forms.GroupBox groupboxExportFormat;
        private System.Windows.Forms.RadioButton radiobuttonAsInGrid;
        private System.Windows.Forms.RadioButton radiobuttonAsHex;
        private System.Windows.Forms.RadioButton radiobuttonAsInt;
        private System.Windows.Forms.RadioButton radiobuttonAsUInt;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Panel FormatAsciiPanel;
        private System.Windows.Forms.RadioButton radExportAscii;
        private System.Windows.Forms.RadioButton radExportAsciiAsInt;
        private System.Windows.Forms.RadioButton radExportAsciiAsHex;
        private System.Windows.Forms.Label label1;

    }
}