namespace ModbusControlSpace
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SaveConfigurationForm));
            this.panelWarning = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.checkboxUpdateXML = new System.Windows.Forms.CheckBox();
            this.checkboxBridge = new System.Windows.Forms.CheckBox();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.checkboxSettings = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.checkboxConnection = new System.Windows.Forms.CheckBox();
            this.checkboxFormSize = new System.Windows.Forms.CheckBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panelWarning.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelWarning
            // 
            this.panelWarning.AutoSize = true;
            this.panelWarning.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelWarning.Controls.Add(this.label2);
            this.panelWarning.Controls.Add(this.pictureBox1);
            this.panelWarning.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelWarning.Location = new System.Drawing.Point(0, 0);
            this.panelWarning.Name = "panelWarning";
            this.panelWarning.Size = new System.Drawing.Size(364, 64);
            this.panelWarning.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label2.Location = new System.Drawing.Point(48, 7);
            this.label2.Margin = new System.Windows.Forms.Padding(3);
            this.label2.MaximumSize = new System.Drawing.Size(310, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(303, 52);
            this.label2.TabIndex = 1;
            this.label2.Text = "Some connection parameters are overridden by the command line! If you save the cu" +
    "rrent configuration to the mbc project file\r\nyou may overwrite some parameters w" +
    "ith the command line values!";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(9, 10);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(32, 32);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.AutoSize = true;
            this.panel2.Controls.Add(this.checkboxUpdateXML);
            this.panel2.Controls.Add(this.checkboxBridge);
            this.panel2.Controls.Add(this.buttonCancel);
            this.panel2.Controls.Add(this.buttonSave);
            this.panel2.Controls.Add(this.checkboxSettings);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.checkboxConnection);
            this.panel2.Controls.Add(this.checkboxFormSize);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 64);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(364, 173);
            this.panel2.TabIndex = 8;
            this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            // 
            // checkboxUpdateXML
            // 
            this.checkboxUpdateXML.AutoSize = true;
            this.checkboxUpdateXML.Location = new System.Drawing.Point(31, 117);
            this.checkboxUpdateXML.Name = "checkboxUpdateXML";
            this.checkboxUpdateXML.Size = new System.Drawing.Size(171, 17);
            this.checkboxUpdateXML.TabIndex = 14;
            this.checkboxUpdateXML.Text = "Update XML to current version";
            this.toolTip1.SetToolTip(this.checkboxUpdateXML, "Update the project file XML to the current version, convert XML tags to lower cas" +
        "e, remove obsolete tags, etc");
            this.checkboxUpdateXML.UseVisualStyleBackColor = true;
            this.checkboxUpdateXML.CheckedChanged += new System.EventHandler(this.checkbox_CheckedChanged);
            // 
            // checkboxBridge
            // 
            this.checkboxBridge.AutoSize = true;
            this.checkboxBridge.Location = new System.Drawing.Point(31, 75);
            this.checkboxBridge.Name = "checkboxBridge";
            this.checkboxBridge.Size = new System.Drawing.Size(161, 17);
            this.checkboxBridge.TabIndex = 13;
            this.checkboxBridge.Text = "Modbus/TCP bridge settings";
            this.toolTip1.SetToolTip(this.checkboxBridge, "Save the Modbus/TCP bridge server settings");
            this.checkboxBridge.UseVisualStyleBackColor = true;
            this.checkboxBridge.CheckedChanged += new System.EventHandler(this.checkbox_CheckedChanged);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(186, 140);
            this.buttonCancel.Margin = new System.Windows.Forms.Padding(3, 3, 3, 10);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 12;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(103, 140);
            this.buttonSave.Margin = new System.Windows.Forms.Padding(3, 3, 3, 10);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 11;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // checkboxSettings
            // 
            this.checkboxSettings.AutoSize = true;
            this.checkboxSettings.Location = new System.Drawing.Point(31, 54);
            this.checkboxSettings.Name = "checkboxSettings";
            this.checkboxSettings.Size = new System.Drawing.Size(133, 17);
            this.checkboxSettings.TabIndex = 10;
            this.checkboxSettings.Text = "Sample rate, show info";
            this.toolTip1.SetToolTip(this.checkboxSettings, "Save the current sample rate and ShowInfo flag");
            this.checkboxSettings.UseVisualStyleBackColor = true;
            this.checkboxSettings.CheckedChanged += new System.EventHandler(this.checkbox_CheckedChanged);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(12, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(285, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Save the following configuration settings to the project file:";
            // 
            // checkboxConnection
            // 
            this.checkboxConnection.AutoSize = true;
            this.checkboxConnection.Location = new System.Drawing.Point(31, 33);
            this.checkboxConnection.Name = "checkboxConnection";
            this.checkboxConnection.Size = new System.Drawing.Size(181, 17);
            this.checkboxConnection.TabIndex = 9;
            this.checkboxConnection.Text = "Connection and protocol settings";
            this.toolTip1.SetToolTip(this.checkboxConnection, "Save the current connection and protocol settings");
            this.checkboxConnection.UseVisualStyleBackColor = true;
            this.checkboxConnection.CheckedChanged += new System.EventHandler(this.checkbox_CheckedChanged);
            // 
            // checkboxFormSize
            // 
            this.checkboxFormSize.AutoSize = true;
            this.checkboxFormSize.Location = new System.Drawing.Point(31, 96);
            this.checkboxFormSize.Name = "checkboxFormSize";
            this.checkboxFormSize.Size = new System.Drawing.Size(104, 17);
            this.checkboxFormSize.TabIndex = 8;
            this.checkboxFormSize.Text = "Current form size";
            this.toolTip1.SetToolTip(this.checkboxFormSize, "Save the current form size ");
            this.checkboxFormSize.UseVisualStyleBackColor = true;
            this.checkboxFormSize.CheckedChanged += new System.EventHandler(this.checkbox_CheckedChanged);
            // 
            // SaveConfigurationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(364, 234);
            this.ControlBox = false;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panelWarning);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(370, 39);
            this.Name = "SaveConfigurationForm";
            this.Padding = new System.Windows.Forms.Padding(0, 0, 0, 6);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Tag = "";
            this.Text = "Save project file";
            this.Load += new System.EventHandler(this.SaveConfigurationForm_Load);
            this.panelWarning.ResumeLayout(false);
            this.panelWarning.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelWarning;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.CheckBox checkboxBridge;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.CheckBox checkboxSettings;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkboxConnection;
        private System.Windows.Forms.CheckBox checkboxFormSize;
        private System.Windows.Forms.CheckBox checkboxUpdateXML;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}