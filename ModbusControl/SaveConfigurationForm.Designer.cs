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
            checkboxUpdateXML = new CheckBox();
            checkboxBridge = new CheckBox();
            buttonCancel = new Button();
            buttonSave = new Button();
            label1 = new Label();
            checkboxProjectConfig = new CheckBox();
            checkboxFormSize = new CheckBox();
            toolTip1 = new ToolTip(components);
            panelWarning = new Panel();
            label2 = new Label();
            pictureBox1 = new PictureBox();
            optionsPanel = new Panel();
            panelWarning.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            optionsPanel.SuspendLayout();
            SuspendLayout();
            // 
            // checkboxUpdateXML
            // 
            checkboxUpdateXML.AutoSize = true;
            checkboxUpdateXML.Checked = true;
            checkboxUpdateXML.CheckState = CheckState.Checked;
            checkboxUpdateXML.Location = new Point(32, 95);
            checkboxUpdateXML.Margin = new Padding(16, 3, 3, 3);
            checkboxUpdateXML.Name = "checkboxUpdateXML";
            checkboxUpdateXML.Size = new Size(171, 17);
            checkboxUpdateXML.TabIndex = 4;
            checkboxUpdateXML.Text = "Update XML to current version";
            toolTip1.SetToolTip(checkboxUpdateXML, "Update the project file XML to the current version, convert XML tags to lower case, remove obsolete tags, etc");
            checkboxUpdateXML.UseVisualStyleBackColor = true;
            checkboxUpdateXML.CheckedChanged += checkbox_CheckedChanged;
            // 
            // checkboxBridge
            // 
            checkboxBridge.AutoSize = true;
            checkboxBridge.Location = new Point(32, 51);
            checkboxBridge.Margin = new Padding(16, 3, 3, 3);
            checkboxBridge.Name = "checkboxBridge";
            checkboxBridge.Size = new Size(161, 17);
            checkboxBridge.TabIndex = 2;
            checkboxBridge.Text = "Modbus/TCP bridge settings";
            toolTip1.SetToolTip(checkboxBridge, "Save the Modbus/TCP bridge server settings");
            checkboxBridge.UseVisualStyleBackColor = true;
            checkboxBridge.CheckedChanged += checkbox_CheckedChanged;
            // 
            // buttonCancel
            // 
            buttonCancel.Anchor = AnchorStyles.Top;
            buttonCancel.AutoSize = true;
            buttonCancel.DialogResult = DialogResult.Cancel;
            buttonCancel.Location = new Point(225, 124);
            buttonCancel.Margin = new Padding(11, 3, 3, 3);
            buttonCancel.Name = "buttonCancel";
            buttonCancel.Size = new Size(75, 25);
            buttonCancel.TabIndex = 1;
            buttonCancel.Text = "Cancel";
            buttonCancel.UseVisualStyleBackColor = true;
            buttonCancel.Click += buttonCancel_Click;
            // 
            // buttonSave
            // 
            buttonSave.Anchor = AnchorStyles.Top;
            buttonSave.AutoSize = true;
            buttonSave.Location = new Point(137, 124);
            buttonSave.Margin = new Padding(3, 3, 11, 3);
            buttonSave.Name = "buttonSave";
            buttonSave.Size = new Size(75, 25);
            buttonSave.TabIndex = 0;
            buttonSave.Text = "Save";
            buttonSave.UseVisualStyleBackColor = true;
            buttonSave.Click += buttonSave_Click;
            // 
            // label1
            // 
            label1.Location = new Point(17, 8);
            label1.Margin = new Padding(8, 8, 3, 0);
            label1.Name = "label1";
            label1.Size = new Size(285, 13);
            label1.TabIndex = 0;
            label1.Text = "Save the following items to the project file:";
            // 
            // checkboxProjectConfig
            // 
            checkboxProjectConfig.AutoSize = true;
            checkboxProjectConfig.Checked = true;
            checkboxProjectConfig.CheckState = CheckState.Checked;
            checkboxProjectConfig.Location = new Point(32, 29);
            checkboxProjectConfig.Margin = new Padding(16, 8, 3, 3);
            checkboxProjectConfig.Name = "checkboxProjectConfig";
            checkboxProjectConfig.Size = new Size(162, 17);
            checkboxProjectConfig.TabIndex = 1;
            checkboxProjectConfig.Text = "Project configuration settings";
            toolTip1.SetToolTip(checkboxProjectConfig, "Save the current connection and protocol settings");
            checkboxProjectConfig.UseVisualStyleBackColor = true;
            checkboxProjectConfig.CheckedChanged += checkbox_CheckedChanged;
            // 
            // checkboxFormSize
            // 
            checkboxFormSize.AutoSize = true;
            checkboxFormSize.Location = new Point(32, 73);
            checkboxFormSize.Margin = new Padding(16, 3, 3, 3);
            checkboxFormSize.Name = "checkboxFormSize";
            checkboxFormSize.Size = new Size(109, 17);
            checkboxFormSize.TabIndex = 3;
            checkboxFormSize.Text = "Main window size";
            toolTip1.SetToolTip(checkboxFormSize, "Save the current form size ");
            checkboxFormSize.UseVisualStyleBackColor = true;
            checkboxFormSize.CheckedChanged += checkbox_CheckedChanged;
            // 
            // panelWarning
            // 
            panelWarning.BorderStyle = BorderStyle.FixedSingle;
            panelWarning.Controls.Add(label2);
            panelWarning.Controls.Add(pictureBox1);
            panelWarning.Dock = DockStyle.Top;
            panelWarning.Location = new Point(0, 0);
            panelWarning.Name = "panelWarning";
            panelWarning.Size = new Size(453, 55);
            panelWarning.TabIndex = 2;
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
            optionsPanel.Controls.Add(label1);
            optionsPanel.Controls.Add(buttonCancel);
            optionsPanel.Controls.Add(buttonSave);
            optionsPanel.Controls.Add(checkboxProjectConfig);
            optionsPanel.Controls.Add(checkboxFormSize);
            optionsPanel.Controls.Add(checkboxBridge);
            optionsPanel.Controls.Add(checkboxUpdateXML);
            optionsPanel.Dock = DockStyle.Top;
            optionsPanel.Location = new Point(0, 55);
            optionsPanel.Name = "optionsPanel";
            optionsPanel.Size = new Size(453, 154);
            optionsPanel.TabIndex = 5;
            // 
            // SaveConfigurationForm
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            CancelButton = buttonCancel;
            ClientSize = new Size(453, 210);
            ControlBox = false;
            Controls.Add(optionsPanel);
            Controls.Add(panelWarning);
            Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "SaveConfigurationForm";
            Padding = new Padding(0, 0, 0, 6);
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Tag = "";
            Text = "Save project file";
            Load += SaveConfigurationForm_Load;
            Shown += SaveConfigurationForm_Shown;
            panelWarning.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            optionsPanel.ResumeLayout(false);
            optionsPanel.PerformLayout();
            ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.CheckBox checkboxBridge;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkboxProjectConfig;
        private System.Windows.Forms.CheckBox checkboxFormSize;
        private System.Windows.Forms.CheckBox checkboxUpdateXML;
        private System.Windows.Forms.ToolTip toolTip1;
        private Panel panelWarning;
        private Label label2;
        private PictureBox pictureBox1;
        private Panel optionsPanel;
    }
}