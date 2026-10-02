namespace ModbusControl
{
    partial class ExportForm
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExportForm));
            buttonStart = new Button();
            textboxLog = new TextBox();
            radExportCurrentTab = new RadioButton();
            SaveFileDialog1 = new SaveFileDialog();
            SelectFileButton = new Button();
            panelTab = new Panel();
            radExportAllTabs = new RadioButton();
            textboxFilename = new TextBox();
            buttonClearLog = new Button();
            groupboxExportFormat = new GroupBox();
            radFormatDefault = new RadioButton();
            radFormatAsHex = new RadioButton();
            radFormatAsInt = new RadioButton();
            radFormatAsUInt = new RadioButton();
            StringFormatPanel = new Panel();
            radStringFormatAsUInt = new RadioButton();
            label1 = new Label();
            radStringFormatDefault = new RadioButton();
            radStringFormatAsInt = new RadioButton();
            radStringFormatAsHex = new RadioButton();
            buttonCancel = new Button();
            toolTip1 = new ToolTip(components);
            butRunTests = new Button();
            chkContinueOnError = new CheckBox();
            panel1 = new Panel();
            panel2 = new Panel();
            panelTab.SuspendLayout();
            groupboxExportFormat.SuspendLayout();
            StringFormatPanel.SuspendLayout();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // buttonStart
            // 
            buttonStart.Location = new Point(12, 113);
            buttonStart.Name = "buttonStart";
            buttonStart.Size = new Size(150, 23);
            buttonStart.TabIndex = 5;
            buttonStart.Text = "Start";
            toolTip1.SetToolTip(buttonStart, "Start the export");
            buttonStart.UseVisualStyleBackColor = true;
            buttonStart.Click += buttonStart_Click;
            // 
            // textboxLog
            // 
            textboxLog.BorderStyle = BorderStyle.FixedSingle;
            textboxLog.Dock = DockStyle.Fill;
            textboxLog.Location = new Point(0, 0);
            textboxLog.Multiline = true;
            textboxLog.Name = "textboxLog";
            textboxLog.ReadOnly = true;
            textboxLog.ScrollBars = ScrollBars.Both;
            textboxLog.Size = new Size(603, 238);
            textboxLog.TabIndex = 0;
            textboxLog.WordWrap = false;
            // 
            // radExportCurrentTab
            // 
            radExportCurrentTab.AutoSize = true;
            radExportCurrentTab.Checked = true;
            radExportCurrentTab.Location = new Point(6, 6);
            radExportCurrentTab.Name = "radExportCurrentTab";
            radExportCurrentTab.Size = new Size(163, 17);
            radExportCurrentTab.TabIndex = 0;
            radExportCurrentTab.TabStop = true;
            radExportCurrentTab.Tag = "";
            radExportCurrentTab.Text = "Export registers of current tab";
            toolTip1.SetToolTip(radExportCurrentTab, "Export only the registers of the currently active tab page");
            radExportCurrentTab.UseVisualStyleBackColor = true;
            // 
            // SaveFileDialog1
            // 
            SaveFileDialog1.AddExtension = false;
            SaveFileDialog1.DefaultExt = "csv";
            SaveFileDialog1.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
            // 
            // SelectFileButton
            // 
            SelectFileButton.Location = new Point(12, 85);
            SelectFileButton.Name = "SelectFileButton";
            SelectFileButton.Size = new Size(88, 23);
            SelectFileButton.TabIndex = 3;
            SelectFileButton.Text = "Select file...";
            toolTip1.SetToolTip(SelectFileButton, "Select the file to export to");
            SelectFileButton.UseVisualStyleBackColor = true;
            SelectFileButton.Click += buttonSelectFile_Click;
            // 
            // panelTab
            // 
            panelTab.Controls.Add(radExportAllTabs);
            panelTab.Controls.Add(radExportCurrentTab);
            panelTab.Location = new Point(7, 11);
            panelTab.Name = "panelTab";
            panelTab.Size = new Size(189, 51);
            panelTab.TabIndex = 0;
            // 
            // radExportAllTabs
            // 
            radExportAllTabs.AutoSize = true;
            radExportAllTabs.Location = new Point(6, 27);
            radExportAllTabs.Name = "radExportAllTabs";
            radExportAllTabs.Size = new Size(145, 17);
            radExportAllTabs.TabIndex = 1;
            radExportAllTabs.Tag = "";
            radExportAllTabs.Text = "Export registers of all tabs";
            toolTip1.SetToolTip(radExportAllTabs, "Export the registers of all tab pages");
            radExportAllTabs.UseVisualStyleBackColor = true;
            // 
            // textboxFilename
            // 
            textboxFilename.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textboxFilename.Location = new Point(106, 88);
            textboxFilename.Name = "textboxFilename";
            textboxFilename.RightToLeft = RightToLeft.No;
            textboxFilename.Size = new Size(485, 20);
            textboxFilename.TabIndex = 4;
            // 
            // buttonClearLog
            // 
            buttonClearLog.Location = new Point(250, 113);
            buttonClearLog.Name = "buttonClearLog";
            buttonClearLog.Size = new Size(75, 23);
            buttonClearLog.TabIndex = 7;
            buttonClearLog.Text = "Clear log";
            toolTip1.SetToolTip(buttonClearLog, "Clear the result log window");
            buttonClearLog.UseVisualStyleBackColor = true;
            buttonClearLog.Click += buttonClearLog_Click;
            // 
            // groupboxExportFormat
            // 
            groupboxExportFormat.Controls.Add(radFormatAsHex);
            groupboxExportFormat.Controls.Add(radFormatAsUInt);
            groupboxExportFormat.Controls.Add(radFormatAsInt);
            groupboxExportFormat.Controls.Add(radFormatDefault);
            groupboxExportFormat.Controls.Add(StringFormatPanel);
            groupboxExportFormat.Location = new Point(201, 5);
            groupboxExportFormat.Name = "groupboxExportFormat";
            groupboxExportFormat.Size = new Size(389, 62);
            groupboxExportFormat.TabIndex = 1;
            groupboxExportFormat.TabStop = false;
            groupboxExportFormat.Text = "Export values format:";
            // 
            // radFormatDefault
            // 
            radFormatDefault.AutoSize = true;
            radFormatDefault.Checked = true;
            radFormatDefault.Location = new Point(7, 17);
            radFormatDefault.Name = "radFormatDefault";
            radFormatDefault.Size = new Size(67, 17);
            radFormatDefault.TabIndex = 0;
            radFormatDefault.TabStop = true;
            radFormatDefault.Tag = "";
            radFormatDefault.Text = "as in grid";
            toolTip1.SetToolTip(radFormatDefault, "Export register values with the format as defined in the project");
            radFormatDefault.UseVisualStyleBackColor = true;
            radFormatDefault.CheckedChanged += radFormat_CheckedChanged;
            // 
            // radFormatAsHex
            // 
            radFormatAsHex.AutoSize = true;
            radFormatAsHex.Location = new Point(227, 17);
            radFormatAsHex.Name = "radFormatAsHex";
            radFormatAsHex.Size = new Size(58, 17);
            radFormatAsHex.TabIndex = 3;
            radFormatAsHex.Tag = "";
            radFormatAsHex.Text = "as Hex";
            toolTip1.SetToolTip(radFormatAsHex, "Export all register values in hexadecimal");
            radFormatAsHex.UseVisualStyleBackColor = true;
            radFormatAsHex.CheckedChanged += radFormat_CheckedChanged;
            // 
            // radFormatAsInt
            // 
            radFormatAsInt.AutoSize = true;
            radFormatAsInt.Location = new Point(88, 17);
            radFormatAsInt.Name = "radFormatAsInt";
            radFormatAsInt.Size = new Size(51, 17);
            radFormatAsInt.TabIndex = 1;
            radFormatAsInt.Tag = "";
            radFormatAsInt.Text = "as Int";
            toolTip1.SetToolTip(radFormatAsInt, "Export all register values as signed integers");
            radFormatAsInt.UseVisualStyleBackColor = true;
            radFormatAsInt.CheckedChanged += radFormat_CheckedChanged;
            // 
            // radFormatAsUInt
            // 
            radFormatAsUInt.AutoSize = true;
            radFormatAsUInt.Location = new Point(153, 17);
            radFormatAsUInt.Name = "radFormatAsUInt";
            radFormatAsUInt.Size = new Size(59, 17);
            radFormatAsUInt.TabIndex = 2;
            radFormatAsUInt.Tag = "";
            radFormatAsUInt.Text = "as UInt";
            toolTip1.SetToolTip(radFormatAsUInt, "Export all register values as unsigned integers");
            radFormatAsUInt.UseVisualStyleBackColor = true;
            radFormatAsUInt.CheckedChanged += radFormat_CheckedChanged;
            // 
            // StringFormatPanel
            // 
            StringFormatPanel.Controls.Add(radStringFormatAsHex);
            StringFormatPanel.Controls.Add(radStringFormatAsUInt);
            StringFormatPanel.Controls.Add(radStringFormatAsInt);
            StringFormatPanel.Controls.Add(radStringFormatDefault);
            StringFormatPanel.Controls.Add(label1);
            StringFormatPanel.Location = new Point(1, 35);
            StringFormatPanel.Name = "StringFormatPanel";
            StringFormatPanel.Size = new Size(382, 21);
            StringFormatPanel.TabIndex = 4;
            // 
            // radStringFormatAsUInt
            // 
            radStringFormatAsUInt.AutoSize = true;
            radStringFormatAsUInt.Location = new Point(267, 3);
            radStringFormatAsUInt.Name = "radStringFormatAsUInt";
            radStringFormatAsUInt.Size = new Size(45, 17);
            radStringFormatAsUInt.TabIndex = 3;
            radStringFormatAsUInt.Tag = "";
            radStringFormatAsUInt.Text = "UInt";
            toolTip1.SetToolTip(radStringFormatAsUInt, "Export all register values as signed integers");
            radStringFormatAsUInt.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(3, 5);
            label1.Name = "label1";
            label1.Size = new Size(114, 13);
            label1.TabIndex = 0;
            label1.Text = "Export string/ASCII as:";
            // 
            // radStringFormatDefault
            // 
            radStringFormatDefault.AutoSize = true;
            radStringFormatDefault.Checked = true;
            radStringFormatDefault.Location = new Point(126, 3);
            radStringFormatDefault.Name = "radStringFormatDefault";
            radStringFormatDefault.Size = new Size(84, 17);
            radStringFormatDefault.TabIndex = 1;
            radStringFormatDefault.TabStop = true;
            radStringFormatDefault.Tag = "";
            radStringFormatDefault.Text = "String/ASCII";
            toolTip1.SetToolTip(radStringFormatDefault, "Export register values with the format as defined in the project");
            radStringFormatDefault.UseVisualStyleBackColor = true;
            // 
            // radStringFormatAsInt
            // 
            radStringFormatAsInt.AutoSize = true;
            radStringFormatAsInt.Location = new Point(220, 3);
            radStringFormatAsInt.Name = "radStringFormatAsInt";
            radStringFormatAsInt.Size = new Size(37, 17);
            radStringFormatAsInt.TabIndex = 2;
            radStringFormatAsInt.Tag = "";
            radStringFormatAsInt.Text = "Int";
            toolTip1.SetToolTip(radStringFormatAsInt, "Export all register values as signed integers");
            radStringFormatAsInt.UseVisualStyleBackColor = true;
            // 
            // radStringFormatAsHex
            // 
            radStringFormatAsHex.AutoSize = true;
            radStringFormatAsHex.Location = new Point(322, 3);
            radStringFormatAsHex.Name = "radStringFormatAsHex";
            radStringFormatAsHex.Size = new Size(44, 17);
            radStringFormatAsHex.TabIndex = 4;
            radStringFormatAsHex.Tag = "";
            radStringFormatAsHex.Text = "Hex";
            toolTip1.SetToolTip(radStringFormatAsHex, "Export all register values as unsigned integers");
            radStringFormatAsHex.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            buttonCancel.Enabled = false;
            buttonCancel.Location = new Point(169, 113);
            buttonCancel.Name = "buttonCancel";
            buttonCancel.Size = new Size(75, 23);
            buttonCancel.TabIndex = 6;
            buttonCancel.Text = "Cancel";
            toolTip1.SetToolTip(buttonCancel, "Cancel the current operation");
            buttonCancel.UseVisualStyleBackColor = true;
            buttonCancel.Click += buttonCancel_Click;
            // 
            // butRunTests
            // 
            butRunTests.Location = new Point(339, 113);
            butRunTests.Name = "butRunTests";
            butRunTests.Size = new Size(75, 23);
            butRunTests.TabIndex = 8;
            butRunTests.Text = "Run test";
            butRunTests.UseVisualStyleBackColor = true;
            butRunTests.Visible = false;
            butRunTests.Click += buttonTest_Click;
            // 
            // chkContinueOnError
            // 
            chkContinueOnError.AutoSize = true;
            chkContinueOnError.Location = new Point(13, 65);
            chkContinueOnError.Name = "chkContinueOnError";
            chkContinueOnError.Size = new Size(107, 17);
            chkContinueOnError.TabIndex = 2;
            chkContinueOnError.Text = "Continue on error";
            chkContinueOnError.UseVisualStyleBackColor = true;
            chkContinueOnError.CheckedChanged += chkContinueOnError_CheckedChanged;
            // 
            // panel1
            // 
            panel1.Controls.Add(buttonStart);
            panel1.Controls.Add(chkContinueOnError);
            panel1.Controls.Add(SelectFileButton);
            panel1.Controls.Add(butRunTests);
            panel1.Controls.Add(panelTab);
            panel1.Controls.Add(groupboxExportFormat);
            panel1.Controls.Add(textboxFilename);
            panel1.Controls.Add(buttonCancel);
            panel1.Controls.Add(buttonClearLog);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(603, 144);
            panel1.TabIndex = 10;
            // 
            // panel2
            // 
            panel2.Controls.Add(textboxLog);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(0, 144);
            panel2.Name = "panel2";
            panel2.Size = new Size(603, 238);
            panel2.TabIndex = 11;
            // 
            // ExportForm
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(603, 382);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "ExportForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Export registers to file";
            FormClosing += OnFormClosing;
            panelTab.ResumeLayout(false);
            panelTab.PerformLayout();
            groupboxExportFormat.ResumeLayout(false);
            groupboxExportFormat.PerformLayout();
            StringFormatPanel.ResumeLayout(false);
            StringFormatPanel.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ResumeLayout(false);

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