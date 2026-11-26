namespace ModbusControl
{
    partial class SaveConfigurationForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SaveConfigurationForm));
            UpdateXMLCheckbox = new CheckBox();
            BridgeCheckbox = new CheckBox();
            CancelBtn = new Button();
            SaveBtn = new Button();
            label1 = new Label();
            ProjectConfigCheckbox = new CheckBox();
            FormSizeCheckbox = new CheckBox();
            toolTip1 = new ToolTip(components);
            WarningPanel = new Panel();
            label2 = new Label();
            pictureBox1 = new PictureBox();
            optionsPanel = new Panel();
            WarningPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            optionsPanel.SuspendLayout();
            SuspendLayout();
            // 
            // UpdateXMLCheckbox
            // 
            UpdateXMLCheckbox.AutoSize = true;
            UpdateXMLCheckbox.Checked = true;
            UpdateXMLCheckbox.CheckState = CheckState.Checked;
            UpdateXMLCheckbox.ForeColor = SystemColors.ControlText;
            UpdateXMLCheckbox.Location = new Point(22, 96);
            UpdateXMLCheckbox.Margin = new Padding(16, 3, 3, 3);
            UpdateXMLCheckbox.Name = "UpdateXMLCheckbox";
            UpdateXMLCheckbox.Size = new Size(258, 17);
            UpdateXMLCheckbox.TabIndex = 4;
            UpdateXMLCheckbox.Text = "Update the project file XML to the current version";
            toolTip1.SetToolTip(UpdateXMLCheckbox, "Upgrade the XML in the project file to the current version. Tags are converted to lowercase, obsolete tags are removed, etc.");
            UpdateXMLCheckbox.UseVisualStyleBackColor = true;
            UpdateXMLCheckbox.CheckedChanged += UpdateXMLCheckbox_CheckedChanged;
            // 
            // BridgeCheckbox
            // 
            BridgeCheckbox.AutoSize = true;
            BridgeCheckbox.Location = new Point(36, 51);
            BridgeCheckbox.Margin = new Padding(16, 3, 3, 3);
            BridgeCheckbox.Name = "BridgeCheckbox";
            BridgeCheckbox.Size = new Size(161, 17);
            BridgeCheckbox.TabIndex = 2;
            BridgeCheckbox.Text = "Modbus/TCP bridge settings";
            toolTip1.SetToolTip(BridgeCheckbox, "Save the current Modbus/TCP bridge settings to the project file");
            BridgeCheckbox.UseVisualStyleBackColor = true;
            BridgeCheckbox.CheckedChanged += BridgeCheckbox_CheckedChanged;
            // 
            // CancelBtn
            // 
            CancelBtn.DialogResult = DialogResult.Cancel;
            CancelBtn.Location = new Point(229, 124);
            CancelBtn.Margin = new Padding(11, 3, 3, 3);
            CancelBtn.Name = "CancelBtn";
            CancelBtn.Size = new Size(75, 23);
            CancelBtn.TabIndex = 6;
            CancelBtn.Text = "Cancel";
            CancelBtn.UseVisualStyleBackColor = true;
            CancelBtn.Click += CancelBtn_Click;
            // 
            // SaveBtn
            // 
            SaveBtn.Location = new Point(141, 124);
            SaveBtn.Margin = new Padding(3, 3, 11, 3);
            SaveBtn.Name = "SaveBtn";
            SaveBtn.Size = new Size(75, 23);
            SaveBtn.TabIndex = 5;
            SaveBtn.Text = "Save";
            SaveBtn.UseVisualStyleBackColor = true;
            SaveBtn.Click += SaveBtn_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(17, 8);
            label1.Margin = new Padding(8, 8, 3, 0);
            label1.Name = "label1";
            label1.Size = new Size(214, 13);
            label1.TabIndex = 0;
            label1.Text = "Update the following items in the project file:";
            // 
            // ProjectConfigCheckbox
            // 
            ProjectConfigCheckbox.AutoSize = true;
            ProjectConfigCheckbox.Checked = true;
            ProjectConfigCheckbox.CheckState = CheckState.Checked;
            ProjectConfigCheckbox.Location = new Point(36, 29);
            ProjectConfigCheckbox.Margin = new Padding(16, 8, 3, 3);
            ProjectConfigCheckbox.Name = "ProjectConfigCheckbox";
            ProjectConfigCheckbox.Size = new Size(162, 17);
            ProjectConfigCheckbox.TabIndex = 1;
            ProjectConfigCheckbox.Text = "Project configuration settings";
            toolTip1.SetToolTip(ProjectConfigCheckbox, "Save the current connection and protocol settings to the project file");
            ProjectConfigCheckbox.UseVisualStyleBackColor = true;
            ProjectConfigCheckbox.CheckedChanged += ProjectConfigCheckbox_CheckedChanged;
            // 
            // FormSizeCheckbox
            // 
            FormSizeCheckbox.AutoSize = true;
            FormSizeCheckbox.Location = new Point(36, 73);
            FormSizeCheckbox.Margin = new Padding(16, 3, 3, 3);
            FormSizeCheckbox.Name = "FormSizeCheckbox";
            FormSizeCheckbox.Size = new Size(109, 17);
            FormSizeCheckbox.TabIndex = 3;
            FormSizeCheckbox.Text = "Main window size";
            toolTip1.SetToolTip(FormSizeCheckbox, "Save the current size of the main window to the project file");
            FormSizeCheckbox.UseVisualStyleBackColor = true;
            FormSizeCheckbox.CheckedChanged += FormSizeCheckbox_CheckedChanged;
            // 
            // WarningPanel
            // 
            WarningPanel.BorderStyle = BorderStyle.FixedSingle;
            WarningPanel.Controls.Add(label2);
            WarningPanel.Controls.Add(pictureBox1);
            WarningPanel.Dock = DockStyle.Top;
            WarningPanel.Location = new Point(0, 0);
            WarningPanel.Name = "WarningPanel";
            WarningPanel.Size = new Size(453, 55);
            WarningPanel.TabIndex = 0;
            // 
            // label2
            // 
            label2.ImageAlign = ContentAlignment.MiddleLeft;
            label2.Location = new Point(47, 7);
            label2.Margin = new Padding(3);
            label2.Name = "label2";
            label2.Size = new Size(389, 43);
            label2.TabIndex = 0;
            label2.Text = "Some connection parameters are overridden by the command line! \r\nIf you save the current configuration to the mbc project file\r\nyou may overwrite some parameters with the command line values.";
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(9, 10);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(32, 32);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // optionsPanel
            // 
            optionsPanel.Controls.Add(CancelBtn);
            optionsPanel.Controls.Add(SaveBtn);
            optionsPanel.Controls.Add(UpdateXMLCheckbox);
            optionsPanel.Controls.Add(FormSizeCheckbox);
            optionsPanel.Controls.Add(BridgeCheckbox);
            optionsPanel.Controls.Add(ProjectConfigCheckbox);
            optionsPanel.Controls.Add(label1);
            optionsPanel.Dock = DockStyle.Top;
            optionsPanel.Location = new Point(0, 55);
            optionsPanel.Name = "optionsPanel";
            optionsPanel.Size = new Size(453, 154);
            optionsPanel.TabIndex = 1;
            // 
            // SaveConfigurationForm
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            CancelButton = CancelBtn;
            ClientSize = new Size(453, 210);
            Controls.Add(optionsPanel);
            Controls.Add(WarningPanel);
            Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "SaveConfigurationForm";
            Padding = new Padding(0, 0, 0, 6);
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Tag = "";
            Text = "Save project file";
            Load += SaveConfigurationForm_Load;
            Shown += SaveConfigurationForm_Shown;
            WarningPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            optionsPanel.ResumeLayout(false);
            optionsPanel.PerformLayout();
            ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.CheckBox BridgeCheckbox;
        private System.Windows.Forms.Button CancelBtn;
        private System.Windows.Forms.Button SaveBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox ProjectConfigCheckbox;
        private System.Windows.Forms.CheckBox FormSizeCheckbox;
        private System.Windows.Forms.CheckBox UpdateXMLCheckbox;
        private System.Windows.Forms.ToolTip toolTip1;
        private Panel WarningPanel;
        private Label label2;
        private PictureBox pictureBox1;
        private Panel optionsPanel;
    }
}