namespace ModbusControlSpace
{
    partial class OptionsForm
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
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.checkboxUseLocalOverrides = new System.Windows.Forms.CheckBox();
            this.checkboxStopOnCommsError = new System.Windows.Forms.CheckBox();
            this.checkboxBeepOnInputError = new System.Windows.Forms.CheckBox();
            this.CheckboxCheckForUpdates = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.numericLoggerCapacity = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.numericLoggerCapacity)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(167, 182);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 5;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // buttonOK
            // 
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Location = new System.Drawing.Point(78, 182);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 4;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            // 
            // checkboxUseLocalOverrides
            // 
            this.checkboxUseLocalOverrides.AutoSize = true;
            this.checkboxUseLocalOverrides.Location = new System.Drawing.Point(39, 45);
            this.checkboxUseLocalOverrides.Name = "checkboxUseLocalOverrides";
            this.checkboxUseLocalOverrides.Size = new System.Drawing.Size(116, 17);
            this.checkboxUseLocalOverrides.TabIndex = 1;
            this.checkboxUseLocalOverrides.Text = "Use local overrides";
            this.checkboxUseLocalOverrides.UseVisualStyleBackColor = true;
            // 
            // checkboxStopOnCommsError
            // 
            this.checkboxStopOnCommsError.AutoSize = true;
            this.checkboxStopOnCommsError.Location = new System.Drawing.Point(39, 22);
            this.checkboxStopOnCommsError.Name = "checkboxStopOnCommsError";
            this.checkboxStopOnCommsError.Size = new System.Drawing.Size(120, 17);
            this.checkboxStopOnCommsError.TabIndex = 0;
            this.checkboxStopOnCommsError.Text = "Stop polling on error";
            this.checkboxStopOnCommsError.UseVisualStyleBackColor = true;
            // 
            // checkboxBeepOnInputError
            // 
            this.checkboxBeepOnInputError.AutoSize = true;
            this.checkboxBeepOnInputError.Location = new System.Drawing.Point(39, 68);
            this.checkboxBeepOnInputError.Name = "checkboxBeepOnInputError";
            this.checkboxBeepOnInputError.Size = new System.Drawing.Size(116, 17);
            this.checkboxBeepOnInputError.TabIndex = 2;
            this.checkboxBeepOnInputError.Text = "Beep on input error";
            this.checkboxBeepOnInputError.UseVisualStyleBackColor = true;
            // 
            // CheckboxCheckForUpdates
            // 
            this.CheckboxCheckForUpdates.AutoSize = true;
            this.CheckboxCheckForUpdates.Location = new System.Drawing.Point(39, 136);
            this.CheckboxCheckForUpdates.Name = "CheckboxCheckForUpdates";
            this.CheckboxCheckForUpdates.Size = new System.Drawing.Size(113, 17);
            this.CheckboxCheckForUpdates.TabIndex = 3;
            this.CheckboxCheckForUpdates.Text = "Check for updates";
            this.CheckboxCheckForUpdates.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(39, 99);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 13);
            this.label1.TabIndex = 26;
            this.label1.Text = "Logger capacity (lines):";
            // 
            // numericLoggerCapacity
            // 
            this.numericLoggerCapacity.Increment = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericLoggerCapacity.Location = new System.Drawing.Point(161, 97);
            this.numericLoggerCapacity.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.numericLoggerCapacity.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericLoggerCapacity.Name = "numericLoggerCapacity";
            this.numericLoggerCapacity.Size = new System.Drawing.Size(75, 20);
            this.numericLoggerCapacity.TabIndex = 25;
            this.numericLoggerCapacity.Value = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            // 
            // OptionsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(321, 217);
            this.ControlBox = false;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numericLoggerCapacity);
            this.Controls.Add(this.CheckboxCheckForUpdates);
            this.Controls.Add(this.checkboxBeepOnInputError);
            this.Controls.Add(this.checkboxUseLocalOverrides);
            this.Controls.Add(this.checkboxStopOnCommsError);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "OptionsForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Tag = "";
            this.Text = "Options";
            ((System.ComponentModel.ISupportInitialize)(this.numericLoggerCapacity)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.CheckBox checkboxUseLocalOverrides;
        private System.Windows.Forms.CheckBox checkboxStopOnCommsError;
        private System.Windows.Forms.CheckBox checkboxBeepOnInputError;
        private System.Windows.Forms.CheckBox CheckboxCheckForUpdates;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numericLoggerCapacity;
    }
}