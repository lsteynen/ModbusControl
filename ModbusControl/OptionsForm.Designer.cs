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
            this.SuspendLayout();
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(240, 139);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 7;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // buttonOK
            // 
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Location = new System.Drawing.Point(151, 139);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 6;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            // 
            // checkboxUseLocalOverrides
            // 
            this.checkboxUseLocalOverrides.AutoSize = true;
            this.checkboxUseLocalOverrides.Location = new System.Drawing.Point(155, 35);
            this.checkboxUseLocalOverrides.Name = "checkboxUseLocalOverrides";
            this.checkboxUseLocalOverrides.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.checkboxUseLocalOverrides.Size = new System.Drawing.Size(116, 17);
            this.checkboxUseLocalOverrides.TabIndex = 14;
            this.checkboxUseLocalOverrides.Text = "Use local overrides";
            this.checkboxUseLocalOverrides.UseVisualStyleBackColor = true;
            // 
            // checkboxStopOnCommsError
            // 
            this.checkboxStopOnCommsError.AutoSize = true;
            this.checkboxStopOnCommsError.Location = new System.Drawing.Point(151, 12);
            this.checkboxStopOnCommsError.Name = "checkboxStopOnCommsError";
            this.checkboxStopOnCommsError.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.checkboxStopOnCommsError.Size = new System.Drawing.Size(120, 17);
            this.checkboxStopOnCommsError.TabIndex = 13;
            this.checkboxStopOnCommsError.Text = "Stop polling on error";
            this.checkboxStopOnCommsError.UseVisualStyleBackColor = true;
            // 
            // checkboxBeepOnInputError
            // 
            this.checkboxBeepOnInputError.AutoSize = true;
            this.checkboxBeepOnInputError.Location = new System.Drawing.Point(155, 58);
            this.checkboxBeepOnInputError.Name = "checkboxBeepOnInputError";
            this.checkboxBeepOnInputError.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.checkboxBeepOnInputError.Size = new System.Drawing.Size(116, 17);
            this.checkboxBeepOnInputError.TabIndex = 15;
            this.checkboxBeepOnInputError.Text = "Beep on input error";
            this.checkboxBeepOnInputError.UseVisualStyleBackColor = true;
            // 
            // CheckboxCheckForUpdates
            // 
            this.CheckboxCheckForUpdates.AutoSize = true;
            this.CheckboxCheckForUpdates.Location = new System.Drawing.Point(158, 97);
            this.CheckboxCheckForUpdates.Name = "CheckboxCheckForUpdates";
            this.CheckboxCheckForUpdates.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.CheckboxCheckForUpdates.Size = new System.Drawing.Size(113, 17);
            this.CheckboxCheckForUpdates.TabIndex = 16;
            this.CheckboxCheckForUpdates.Text = "Check for updates";
            this.CheckboxCheckForUpdates.UseVisualStyleBackColor = true;
            // 
            // OptionsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(467, 173);
            this.ControlBox = false;
            this.Controls.Add(this.CheckboxCheckForUpdates);
            this.Controls.Add(this.checkboxBeepOnInputError);
            this.Controls.Add(this.checkboxUseLocalOverrides);
            this.Controls.Add(this.checkboxStopOnCommsError);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Name = "OptionsForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Tag = "";
            this.Text = "Options";
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
    }
}