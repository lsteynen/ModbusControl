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
            this.checkboxFormSize = new System.Windows.Forms.CheckBox();
            this.checkboxConnection = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.checkboxSettings = new System.Windows.Forms.CheckBox();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.checkboxBridge = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // checkboxFormSize
            // 
            this.checkboxFormSize.AutoSize = true;
            this.checkboxFormSize.Location = new System.Drawing.Point(35, 101);
            this.checkboxFormSize.Name = "checkboxFormSize";
            this.checkboxFormSize.Size = new System.Drawing.Size(104, 17);
            this.checkboxFormSize.TabIndex = 1;
            this.checkboxFormSize.Text = "Current form size";
            this.checkboxFormSize.UseVisualStyleBackColor = true;
            this.checkboxFormSize.CheckedChanged += new System.EventHandler(this.checkbox_CheckedChanged);
            // 
            // checkboxConnection
            // 
            this.checkboxConnection.AutoSize = true;
            this.checkboxConnection.Location = new System.Drawing.Point(35, 38);
            this.checkboxConnection.Name = "checkboxConnection";
            this.checkboxConnection.Size = new System.Drawing.Size(181, 17);
            this.checkboxConnection.TabIndex = 2;
            this.checkboxConnection.Text = "Connection and protocol settings";
            this.checkboxConnection.UseVisualStyleBackColor = true;
            this.checkboxConnection.CheckedChanged += new System.EventHandler(this.checkbox_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(281, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Save the following configuration settings to the project file:";
            // 
            // checkboxSettings
            // 
            this.checkboxSettings.AutoSize = true;
            this.checkboxSettings.Location = new System.Drawing.Point(35, 59);
            this.checkboxSettings.Name = "checkboxSettings";
            this.checkboxSettings.Size = new System.Drawing.Size(133, 17);
            this.checkboxSettings.TabIndex = 3;
            this.checkboxSettings.Text = "Sample rate, show info";
            this.checkboxSettings.UseVisualStyleBackColor = true;
            this.checkboxSettings.CheckedChanged += new System.EventHandler(this.checkbox_CheckedChanged);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(83, 128);
            this.buttonSave.Margin = new System.Windows.Forms.Padding(3, 3, 3, 10);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 4;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(166, 128);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 5;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // checkboxBridge
            // 
            this.checkboxBridge.AutoSize = true;
            this.checkboxBridge.Location = new System.Drawing.Point(35, 80);
            this.checkboxBridge.Name = "checkboxBridge";
            this.checkboxBridge.Size = new System.Drawing.Size(161, 17);
            this.checkboxBridge.TabIndex = 6;
            this.checkboxBridge.Text = "Modbus/TCP bridge settings";
            this.checkboxBridge.UseVisualStyleBackColor = true;
            this.checkboxBridge.CheckedChanged += new System.EventHandler(this.checkbox_CheckedChanged);
            // 
            // SaveConfigurationForm
            // 
            this.AcceptButton = this.buttonSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(324, 163);
            this.ControlBox = false;
            this.Controls.Add(this.checkboxBridge);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.checkboxSettings);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.checkboxConnection);
            this.Controls.Add(this.checkboxFormSize);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SaveConfigurationForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Tag = "";
            this.Text = "Save configuration to project file";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox checkboxFormSize;
        private System.Windows.Forms.CheckBox checkboxConnection;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkboxSettings;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.CheckBox checkboxBridge;
    }
}