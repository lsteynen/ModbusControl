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
            StartBtn = new Button();
            LogTextbox = new TextBox();
            ExportCurrentTabRadiobtn = new RadioButton();
            SaveFileDialog1 = new SaveFileDialog();
            SelectFileBtn = new Button();
            TabPanel = new Panel();
            ExportAllTabsRadiobtn = new RadioButton();
            FilenameTextbox = new TextBox();
            ClearLogBtn = new Button();
            ExportFormatGroupbox = new GroupBox();
            FormatAsHexRadiobtn = new RadioButton();
            FormatAsUIntRadiobtn = new RadioButton();
            FormatAsIntRadiobtn = new RadioButton();
            FormatDefaultRadiobtn = new RadioButton();
            StringFormatPanel = new Panel();
            StringFormatAsHexRadiobtn = new RadioButton();
            StringFormatAsUIntRadiobtn = new RadioButton();
            StringFormatAsIntRadiobtn = new RadioButton();
            StringFormatDefaultRadiobtn = new RadioButton();
            label1 = new Label();
            CancelBtn = new Button();
            toolTip1 = new ToolTip(components);
            RunTestsBtn = new Button();
            ContinueOnErrorCheckbox = new CheckBox();
            panel1 = new Panel();
            panel2 = new Panel();
            TabPanel.SuspendLayout();
            ExportFormatGroupbox.SuspendLayout();
            StringFormatPanel.SuspendLayout();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // StartBtn
            // 
            StartBtn.Location = new Point(12, 113);
            StartBtn.Name = "StartBtn";
            StartBtn.Size = new Size(150, 23);
            StartBtn.TabIndex = 5;
            StartBtn.Text = "Start";
            toolTip1.SetToolTip(StartBtn, "Start the export");
            StartBtn.UseVisualStyleBackColor = true;
            StartBtn.Click += StartBtn_Click;
            // 
            // LogTextbox
            // 
            LogTextbox.BorderStyle = BorderStyle.FixedSingle;
            LogTextbox.Dock = DockStyle.Fill;
            LogTextbox.Location = new Point(0, 0);
            LogTextbox.Multiline = true;
            LogTextbox.Name = "LogTextbox";
            LogTextbox.ReadOnly = true;
            LogTextbox.ScrollBars = ScrollBars.Both;
            LogTextbox.Size = new Size(603, 238);
            LogTextbox.TabIndex = 0;
            LogTextbox.WordWrap = false;
            // 
            // ExportCurrentTabRadiobtn
            // 
            ExportCurrentTabRadiobtn.AutoSize = true;
            ExportCurrentTabRadiobtn.Checked = true;
            ExportCurrentTabRadiobtn.Location = new Point(6, 6);
            ExportCurrentTabRadiobtn.Name = "ExportCurrentTabRadiobtn";
            ExportCurrentTabRadiobtn.Size = new Size(163, 17);
            ExportCurrentTabRadiobtn.TabIndex = 0;
            ExportCurrentTabRadiobtn.TabStop = true;
            ExportCurrentTabRadiobtn.Tag = "";
            ExportCurrentTabRadiobtn.Text = "Export registers of current tab";
            toolTip1.SetToolTip(ExportCurrentTabRadiobtn, "Export only the registers of the currently active tab page");
            ExportCurrentTabRadiobtn.UseVisualStyleBackColor = true;
            // 
            // SaveFileDialog1
            // 
            SaveFileDialog1.AddExtension = false;
            SaveFileDialog1.DefaultExt = "csv";
            SaveFileDialog1.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
            // 
            // SelectFileBtn
            // 
            SelectFileBtn.Location = new Point(12, 85);
            SelectFileBtn.Name = "SelectFileBtn";
            SelectFileBtn.Size = new Size(88, 23);
            SelectFileBtn.TabIndex = 3;
            SelectFileBtn.Text = "Select file...";
            toolTip1.SetToolTip(SelectFileBtn, "Select the file to export to");
            SelectFileBtn.UseVisualStyleBackColor = true;
            SelectFileBtn.Click += SelectFileBtn_Click;
            // 
            // TabPanel
            // 
            TabPanel.Controls.Add(ExportAllTabsRadiobtn);
            TabPanel.Controls.Add(ExportCurrentTabRadiobtn);
            TabPanel.Location = new Point(7, 11);
            TabPanel.Name = "TabPanel";
            TabPanel.Size = new Size(189, 51);
            TabPanel.TabIndex = 0;
            // 
            // ExportAllTabsRadiobtn
            // 
            ExportAllTabsRadiobtn.AutoSize = true;
            ExportAllTabsRadiobtn.Location = new Point(6, 27);
            ExportAllTabsRadiobtn.Name = "ExportAllTabsRadiobtn";
            ExportAllTabsRadiobtn.Size = new Size(145, 17);
            ExportAllTabsRadiobtn.TabIndex = 1;
            ExportAllTabsRadiobtn.Tag = "";
            ExportAllTabsRadiobtn.Text = "Export registers of all tabs";
            toolTip1.SetToolTip(ExportAllTabsRadiobtn, "Export the registers of all tab pages");
            ExportAllTabsRadiobtn.UseVisualStyleBackColor = true;
            // 
            // FilenameTextbox
            // 
            FilenameTextbox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            FilenameTextbox.Location = new Point(106, 88);
            FilenameTextbox.Name = "FilenameTextbox";
            FilenameTextbox.RightToLeft = RightToLeft.No;
            FilenameTextbox.Size = new Size(485, 20);
            FilenameTextbox.TabIndex = 4;
            // 
            // ClearLogBtn
            // 
            ClearLogBtn.Location = new Point(250, 113);
            ClearLogBtn.Name = "ClearLogBtn";
            ClearLogBtn.Size = new Size(75, 23);
            ClearLogBtn.TabIndex = 7;
            ClearLogBtn.Text = "Clear log";
            toolTip1.SetToolTip(ClearLogBtn, "Clear the result log window");
            ClearLogBtn.UseVisualStyleBackColor = true;
            ClearLogBtn.Click += ClearLogBtn_Click;
            // 
            // ExportFormatGroupbox
            // 
            ExportFormatGroupbox.Controls.Add(FormatAsHexRadiobtn);
            ExportFormatGroupbox.Controls.Add(FormatAsUIntRadiobtn);
            ExportFormatGroupbox.Controls.Add(FormatAsIntRadiobtn);
            ExportFormatGroupbox.Controls.Add(FormatDefaultRadiobtn);
            ExportFormatGroupbox.Controls.Add(StringFormatPanel);
            ExportFormatGroupbox.Location = new Point(201, 5);
            ExportFormatGroupbox.Name = "ExportFormatGroupbox";
            ExportFormatGroupbox.Size = new Size(389, 62);
            ExportFormatGroupbox.TabIndex = 1;
            ExportFormatGroupbox.TabStop = false;
            ExportFormatGroupbox.Text = "Export values format:";
            // 
            // FormatAsHexRadiobtn
            // 
            FormatAsHexRadiobtn.AutoSize = true;
            FormatAsHexRadiobtn.Location = new Point(227, 17);
            FormatAsHexRadiobtn.Name = "FormatAsHexRadiobtn";
            FormatAsHexRadiobtn.Size = new Size(58, 17);
            FormatAsHexRadiobtn.TabIndex = 3;
            FormatAsHexRadiobtn.Tag = "";
            FormatAsHexRadiobtn.Text = "as Hex";
            toolTip1.SetToolTip(FormatAsHexRadiobtn, "Export all register values in hexadecimal");
            FormatAsHexRadiobtn.UseVisualStyleBackColor = true;
            FormatAsHexRadiobtn.CheckedChanged += FormatRadiobtn_CheckedChanged;
            // 
            // FormatAsUIntRadiobtn
            // 
            FormatAsUIntRadiobtn.AutoSize = true;
            FormatAsUIntRadiobtn.Location = new Point(153, 17);
            FormatAsUIntRadiobtn.Name = "FormatAsUIntRadiobtn";
            FormatAsUIntRadiobtn.Size = new Size(59, 17);
            FormatAsUIntRadiobtn.TabIndex = 2;
            FormatAsUIntRadiobtn.Tag = "";
            FormatAsUIntRadiobtn.Text = "as UInt";
            toolTip1.SetToolTip(FormatAsUIntRadiobtn, "Export all register values as unsigned integers");
            FormatAsUIntRadiobtn.UseVisualStyleBackColor = true;
            FormatAsUIntRadiobtn.CheckedChanged += FormatRadiobtn_CheckedChanged;
            // 
            // FormatAsIntRadiobtn
            // 
            FormatAsIntRadiobtn.AutoSize = true;
            FormatAsIntRadiobtn.Location = new Point(88, 17);
            FormatAsIntRadiobtn.Name = "FormatAsIntRadiobtn";
            FormatAsIntRadiobtn.Size = new Size(51, 17);
            FormatAsIntRadiobtn.TabIndex = 1;
            FormatAsIntRadiobtn.Tag = "";
            FormatAsIntRadiobtn.Text = "as Int";
            toolTip1.SetToolTip(FormatAsIntRadiobtn, "Export all register values as signed integers");
            FormatAsIntRadiobtn.UseVisualStyleBackColor = true;
            FormatAsIntRadiobtn.CheckedChanged += FormatRadiobtn_CheckedChanged;
            // 
            // FormatDefaultRadiobtn
            // 
            FormatDefaultRadiobtn.AutoSize = true;
            FormatDefaultRadiobtn.Checked = true;
            FormatDefaultRadiobtn.Location = new Point(7, 17);
            FormatDefaultRadiobtn.Name = "FormatDefaultRadiobtn";
            FormatDefaultRadiobtn.Size = new Size(67, 17);
            FormatDefaultRadiobtn.TabIndex = 0;
            FormatDefaultRadiobtn.TabStop = true;
            FormatDefaultRadiobtn.Tag = "";
            FormatDefaultRadiobtn.Text = "as in grid";
            toolTip1.SetToolTip(FormatDefaultRadiobtn, "Export register values with the format as defined in the project");
            FormatDefaultRadiobtn.UseVisualStyleBackColor = true;
            FormatDefaultRadiobtn.CheckedChanged += FormatRadiobtn_CheckedChanged;
            // 
            // StringFormatPanel
            // 
            StringFormatPanel.Controls.Add(StringFormatAsHexRadiobtn);
            StringFormatPanel.Controls.Add(StringFormatAsUIntRadiobtn);
            StringFormatPanel.Controls.Add(StringFormatAsIntRadiobtn);
            StringFormatPanel.Controls.Add(StringFormatDefaultRadiobtn);
            StringFormatPanel.Controls.Add(label1);
            StringFormatPanel.Location = new Point(1, 35);
            StringFormatPanel.Name = "StringFormatPanel";
            StringFormatPanel.Size = new Size(382, 21);
            StringFormatPanel.TabIndex = 4;
            // 
            // StringFormatAsHexRadiobtn
            // 
            StringFormatAsHexRadiobtn.AutoSize = true;
            StringFormatAsHexRadiobtn.Location = new Point(322, 3);
            StringFormatAsHexRadiobtn.Name = "StringFormatAsHexRadiobtn";
            StringFormatAsHexRadiobtn.Size = new Size(44, 17);
            StringFormatAsHexRadiobtn.TabIndex = 4;
            StringFormatAsHexRadiobtn.Tag = "";
            StringFormatAsHexRadiobtn.Text = "Hex";
            toolTip1.SetToolTip(StringFormatAsHexRadiobtn, "Export all register values as unsigned integers");
            StringFormatAsHexRadiobtn.UseVisualStyleBackColor = true;
            // 
            // StringFormatAsUIntRadiobtn
            // 
            StringFormatAsUIntRadiobtn.AutoSize = true;
            StringFormatAsUIntRadiobtn.Location = new Point(267, 3);
            StringFormatAsUIntRadiobtn.Name = "StringFormatAsUIntRadiobtn";
            StringFormatAsUIntRadiobtn.Size = new Size(45, 17);
            StringFormatAsUIntRadiobtn.TabIndex = 3;
            StringFormatAsUIntRadiobtn.Tag = "";
            StringFormatAsUIntRadiobtn.Text = "UInt";
            toolTip1.SetToolTip(StringFormatAsUIntRadiobtn, "Export all register values as signed integers");
            StringFormatAsUIntRadiobtn.UseVisualStyleBackColor = true;
            // 
            // StringFormatAsIntRadiobtn
            // 
            StringFormatAsIntRadiobtn.AutoSize = true;
            StringFormatAsIntRadiobtn.Location = new Point(220, 3);
            StringFormatAsIntRadiobtn.Name = "StringFormatAsIntRadiobtn";
            StringFormatAsIntRadiobtn.Size = new Size(37, 17);
            StringFormatAsIntRadiobtn.TabIndex = 2;
            StringFormatAsIntRadiobtn.Tag = "";
            StringFormatAsIntRadiobtn.Text = "Int";
            toolTip1.SetToolTip(StringFormatAsIntRadiobtn, "Export all register values as signed integers");
            StringFormatAsIntRadiobtn.UseVisualStyleBackColor = true;
            // 
            // StringFormatDefaultRadiobtn
            // 
            StringFormatDefaultRadiobtn.AutoSize = true;
            StringFormatDefaultRadiobtn.Checked = true;
            StringFormatDefaultRadiobtn.Location = new Point(126, 3);
            StringFormatDefaultRadiobtn.Name = "StringFormatDefaultRadiobtn";
            StringFormatDefaultRadiobtn.Size = new Size(84, 17);
            StringFormatDefaultRadiobtn.TabIndex = 1;
            StringFormatDefaultRadiobtn.TabStop = true;
            StringFormatDefaultRadiobtn.Tag = "";
            StringFormatDefaultRadiobtn.Text = "String/ASCII";
            toolTip1.SetToolTip(StringFormatDefaultRadiobtn, "Export register values with the format as defined in the project");
            StringFormatDefaultRadiobtn.UseVisualStyleBackColor = true;
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
            // CancelBtn
            // 
            CancelBtn.Enabled = false;
            CancelBtn.Location = new Point(169, 113);
            CancelBtn.Name = "CancelBtn";
            CancelBtn.Size = new Size(75, 23);
            CancelBtn.TabIndex = 6;
            CancelBtn.Text = "Cancel";
            toolTip1.SetToolTip(CancelBtn, "Cancel the current operation");
            CancelBtn.UseVisualStyleBackColor = true;
            CancelBtn.Click += CancelBtn_Click;
            // 
            // RunTestsBtn
            // 
            RunTestsBtn.Location = new Point(339, 113);
            RunTestsBtn.Name = "RunTestsBtn";
            RunTestsBtn.Size = new Size(75, 23);
            RunTestsBtn.TabIndex = 8;
            RunTestsBtn.Text = "Run test";
            RunTestsBtn.UseVisualStyleBackColor = true;
            RunTestsBtn.Visible = false;
            RunTestsBtn.Click += TestBtn_Click;
            // 
            // ContinueOnErrorCheckbox
            // 
            ContinueOnErrorCheckbox.AutoSize = true;
            ContinueOnErrorCheckbox.Location = new Point(13, 65);
            ContinueOnErrorCheckbox.Name = "ContinueOnErrorCheckbox";
            ContinueOnErrorCheckbox.Size = new Size(107, 17);
            ContinueOnErrorCheckbox.TabIndex = 2;
            ContinueOnErrorCheckbox.Text = "Continue on error";
            ContinueOnErrorCheckbox.UseVisualStyleBackColor = true;
            ContinueOnErrorCheckbox.CheckedChanged += ContinueOnErrorCheckbox_CheckedChanged;
            // 
            // panel1
            // 
            panel1.Controls.Add(StartBtn);
            panel1.Controls.Add(ContinueOnErrorCheckbox);
            panel1.Controls.Add(SelectFileBtn);
            panel1.Controls.Add(RunTestsBtn);
            panel1.Controls.Add(TabPanel);
            panel1.Controls.Add(ExportFormatGroupbox);
            panel1.Controls.Add(FilenameTextbox);
            panel1.Controls.Add(CancelBtn);
            panel1.Controls.Add(ClearLogBtn);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(603, 144);
            panel1.TabIndex = 10;
            // 
            // panel2
            // 
            panel2.Controls.Add(LogTextbox);
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
            TabPanel.ResumeLayout(false);
            TabPanel.PerformLayout();
            ExportFormatGroupbox.ResumeLayout(false);
            ExportFormatGroupbox.PerformLayout();
            StringFormatPanel.ResumeLayout(false);
            StringFormatPanel.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button StartBtn;
        private System.Windows.Forms.TextBox LogTextbox;
        private System.Windows.Forms.RadioButton ExportCurrentTabRadiobtn;
        private System.Windows.Forms.SaveFileDialog SaveFileDialog1;
        private System.Windows.Forms.Button SelectFileBtn;
        private System.Windows.Forms.Panel TabPanel;
        private System.Windows.Forms.RadioButton ExportAllTabsRadiobtn;
        private System.Windows.Forms.TextBox FilenameTextbox;
        private System.Windows.Forms.Button ClearLogBtn;
        private System.Windows.Forms.GroupBox ExportFormatGroupbox;
        private System.Windows.Forms.RadioButton FormatDefaultRadiobtn;
        private System.Windows.Forms.RadioButton FormatAsHexRadiobtn;
        private System.Windows.Forms.RadioButton FormatAsIntRadiobtn;
        private System.Windows.Forms.RadioButton FormatAsUIntRadiobtn;
        private System.Windows.Forms.Button CancelBtn;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Panel StringFormatPanel;
        private System.Windows.Forms.RadioButton StringFormatDefaultRadiobtn;
        private System.Windows.Forms.RadioButton StringFormatAsIntRadiobtn;
        private System.Windows.Forms.RadioButton StringFormatAsHexRadiobtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton StringFormatAsUIntRadiobtn;
        private System.Windows.Forms.Button RunTestsBtn;
        private System.Windows.Forms.CheckBox ContinueOnErrorCheckbox;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
    }
}