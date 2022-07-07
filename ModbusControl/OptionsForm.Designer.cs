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
            this.components = new System.ComponentModel.Container();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.checkboxUseLocalOverrides = new System.Windows.Forms.CheckBox();
            this.checkboxStopOnCommsError = new System.Windows.Forms.CheckBox();
            this.checkboxBeepOnInputError = new System.Windows.Forms.CheckBox();
            this.CheckboxCheckForUpdates = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.numericLoggerCapacity = new System.Windows.Forms.NumericUpDown();
            this.checkboxNextRowOnEnter = new System.Windows.Forms.CheckBox();
            this.checkboxAutostartEdit = new System.Windows.Forms.CheckBox();
            this.textboxCustomEditor = new System.Windows.Forms.TextBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.checkboxCustomEditor = new System.Windows.Forms.CheckBox();
            this.checkboxIgnoreFormSizeOnReload = new System.Windows.Forms.CheckBox();
            this.checkboxShowVarProperties = new System.Windows.Forms.CheckBox();
            this.checkboxAutoRowSize = new System.Windows.Forms.CheckBox();
            this.textboxUseValidationSchema = new System.Windows.Forms.TextBox();
            this.checkboxUseValidationSchema = new System.Windows.Forms.CheckBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.butSelectValidationFile = new System.Windows.Forms.Button();
            this.butSelectCustomEditor = new System.Windows.Forms.Button();
            this.openFileDialog2 = new System.Windows.Forms.OpenFileDialog();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericLoggerCapacity)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(230, 312);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 11;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // buttonOK
            // 
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Location = new System.Drawing.Point(142, 312);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 10;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            // 
            // checkboxUseLocalOverrides
            // 
            this.checkboxUseLocalOverrides.AutoSize = true;
            this.checkboxUseLocalOverrides.Location = new System.Drawing.Point(223, 35);
            this.checkboxUseLocalOverrides.Name = "checkboxUseLocalOverrides";
            this.checkboxUseLocalOverrides.Size = new System.Drawing.Size(116, 17);
            this.checkboxUseLocalOverrides.TabIndex = 1;
            this.checkboxUseLocalOverrides.Text = "Use local overrides";
            this.toolTip1.SetToolTip(this.checkboxUseLocalOverrides, "Enables the local overrides mechanism");
            this.checkboxUseLocalOverrides.UseVisualStyleBackColor = true;
            // 
            // checkboxStopOnCommsError
            // 
            this.checkboxStopOnCommsError.AutoSize = true;
            this.checkboxStopOnCommsError.Location = new System.Drawing.Point(223, 12);
            this.checkboxStopOnCommsError.Name = "checkboxStopOnCommsError";
            this.checkboxStopOnCommsError.Size = new System.Drawing.Size(120, 17);
            this.checkboxStopOnCommsError.TabIndex = 0;
            this.checkboxStopOnCommsError.Text = "Stop polling on error";
            this.toolTip1.SetToolTip(this.checkboxStopOnCommsError, "Stop polling on read error, show message if stopped");
            this.checkboxStopOnCommsError.UseVisualStyleBackColor = true;
            // 
            // checkboxBeepOnInputError
            // 
            this.checkboxBeepOnInputError.AutoSize = true;
            this.checkboxBeepOnInputError.Location = new System.Drawing.Point(23, 12);
            this.checkboxBeepOnInputError.Name = "checkboxBeepOnInputError";
            this.checkboxBeepOnInputError.Size = new System.Drawing.Size(116, 17);
            this.checkboxBeepOnInputError.TabIndex = 2;
            this.checkboxBeepOnInputError.Text = "Beep on input error";
            this.toolTip1.SetToolTip(this.checkboxBeepOnInputError, "Beep when an invalid value is entered");
            this.checkboxBeepOnInputError.UseVisualStyleBackColor = true;
            // 
            // CheckboxCheckForUpdates
            // 
            this.CheckboxCheckForUpdates.AutoSize = true;
            this.CheckboxCheckForUpdates.Location = new System.Drawing.Point(23, 277);
            this.CheckboxCheckForUpdates.Name = "CheckboxCheckForUpdates";
            this.CheckboxCheckForUpdates.Size = new System.Drawing.Size(113, 17);
            this.CheckboxCheckForUpdates.TabIndex = 9;
            this.CheckboxCheckForUpdates.Text = "Check for updates";
            this.toolTip1.SetToolTip(this.CheckboxCheckForUpdates, "Check for program updates on startup, once per day");
            this.CheckboxCheckForUpdates.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 244);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Logger capacity:";
            // 
            // numericLoggerCapacity
            // 
            this.numericLoggerCapacity.Increment = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericLoggerCapacity.Location = new System.Drawing.Point(114, 243);
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
            this.numericLoggerCapacity.TabIndex = 6;
            this.toolTip1.SetToolTip(this.numericLoggerCapacity, "Logger memory capacity, number of lines");
            this.numericLoggerCapacity.Value = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            // 
            // checkboxNextRowOnEnter
            // 
            this.checkboxNextRowOnEnter.AutoSize = true;
            this.checkboxNextRowOnEnter.Location = new System.Drawing.Point(23, 35);
            this.checkboxNextRowOnEnter.Name = "checkboxNextRowOnEnter";
            this.checkboxNextRowOnEnter.Size = new System.Drawing.Size(151, 17);
            this.checkboxNextRowOnEnter.TabIndex = 3;
            this.checkboxNextRowOnEnter.Text = "Move to next row on Enter";
            this.toolTip1.SetToolTip(this.checkboxNextRowOnEnter, "Move to next row after entering a new value");
            this.checkboxNextRowOnEnter.UseVisualStyleBackColor = true;
            // 
            // checkboxAutostartEdit
            // 
            this.checkboxAutostartEdit.AutoSize = true;
            this.checkboxAutostartEdit.Location = new System.Drawing.Point(23, 58);
            this.checkboxAutostartEdit.Name = "checkboxAutostartEdit";
            this.checkboxAutostartEdit.Size = new System.Drawing.Size(166, 17);
            this.checkboxAutostartEdit.TabIndex = 4;
            this.checkboxAutostartEdit.Text = "Auto-start edit mode on typing";
            this.toolTip1.SetToolTip(this.checkboxAutostartEdit, "Auto-start edit to enter a new value, without pressing F2 or double-clicking");
            this.checkboxAutostartEdit.UseVisualStyleBackColor = true;
            // 
            // textboxCustomEditor
            // 
            this.textboxCustomEditor.Location = new System.Drawing.Point(71, 210);
            this.textboxCustomEditor.Name = "textboxCustomEditor";
            this.textboxCustomEditor.Size = new System.Drawing.Size(302, 20);
            this.textboxCustomEditor.TabIndex = 8;
            this.toolTip1.SetToolTip(this.textboxCustomEditor, "Custom editor command, for example [\"c:\\windows\\system32\\notepad.exe\" \"%file\"]");
            // 
            // checkboxCustomEditor
            // 
            this.checkboxCustomEditor.AutoSize = true;
            this.checkboxCustomEditor.Location = new System.Drawing.Point(23, 192);
            this.checkboxCustomEditor.Name = "checkboxCustomEditor";
            this.checkboxCustomEditor.Size = new System.Drawing.Size(111, 17);
            this.checkboxCustomEditor.TabIndex = 7;
            this.checkboxCustomEditor.Text = "Use custom editor";
            this.toolTip1.SetToolTip(this.checkboxCustomEditor, "Use this editor instead of the default one");
            this.checkboxCustomEditor.UseVisualStyleBackColor = true;
            // 
            // checkboxIgnoreFormSizeOnReload
            // 
            this.checkboxIgnoreFormSizeOnReload.AutoSize = true;
            this.checkboxIgnoreFormSizeOnReload.Location = new System.Drawing.Point(223, 58);
            this.checkboxIgnoreFormSizeOnReload.Name = "checkboxIgnoreFormSizeOnReload";
            this.checkboxIgnoreFormSizeOnReload.Size = new System.Drawing.Size(182, 17);
            this.checkboxIgnoreFormSizeOnReload.TabIndex = 12;
            this.checkboxIgnoreFormSizeOnReload.Text = "Ignore form size on project reload";
            this.toolTip1.SetToolTip(this.checkboxIgnoreFormSizeOnReload, "Do not use the form size specified in the mbc file on project REload");
            this.checkboxIgnoreFormSizeOnReload.UseVisualStyleBackColor = true;
            // 
            // checkboxShowVarProperties
            // 
            this.checkboxShowVarProperties.AutoSize = true;
            this.checkboxShowVarProperties.Location = new System.Drawing.Point(23, 81);
            this.checkboxShowVarProperties.Name = "checkboxShowVarProperties";
            this.checkboxShowVarProperties.Size = new System.Drawing.Size(157, 17);
            this.checkboxShowVarProperties.TabIndex = 13;
            this.checkboxShowVarProperties.Text = "Show var properties column";
            this.toolTip1.SetToolTip(this.checkboxShowVarProperties, "Do not use the form size specified in the mbc file on project REload");
            this.checkboxShowVarProperties.UseVisualStyleBackColor = true;
            // 
            // checkboxAutoRowSize
            // 
            this.checkboxAutoRowSize.AutoSize = true;
            this.checkboxAutoRowSize.Location = new System.Drawing.Point(23, 104);
            this.checkboxAutoRowSize.Name = "checkboxAutoRowSize";
            this.checkboxAutoRowSize.Size = new System.Drawing.Size(176, 17);
            this.checkboxAutoRowSize.TabIndex = 14;
            this.checkboxAutoRowSize.Text = "Multiline rows with auto row size";
            this.toolTip1.SetToolTip(this.checkboxAutoRowSize, "Do not use the form size specified in the mbc file on project REload");
            this.checkboxAutoRowSize.UseVisualStyleBackColor = true;
            // 
            // textboxUseValidationSchema
            // 
            this.textboxUseValidationSchema.Location = new System.Drawing.Point(82, 158);
            this.textboxUseValidationSchema.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textboxUseValidationSchema.Name = "textboxUseValidationSchema";
            this.textboxUseValidationSchema.Size = new System.Drawing.Size(291, 20);
            this.textboxUseValidationSchema.TabIndex = 14;
            this.toolTip1.SetToolTip(this.textboxUseValidationSchema, "Custom editor command, for example [\"c:\\windows\\system32\\notepad.exe\" \"%file\"]");
            // 
            // checkboxUseValidationSchema
            // 
            this.checkboxUseValidationSchema.AutoSize = true;
            this.checkboxUseValidationSchema.Location = new System.Drawing.Point(23, 140);
            this.checkboxUseValidationSchema.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.checkboxUseValidationSchema.Name = "checkboxUseValidationSchema";
            this.checkboxUseValidationSchema.Size = new System.Drawing.Size(231, 17);
            this.checkboxUseValidationSchema.TabIndex = 13;
            this.checkboxUseValidationSchema.Text = "Use XSD validation schema on project load";
            this.checkboxUseValidationSchema.UseVisualStyleBackColor = true;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.AddExtension = false;
            this.openFileDialog1.DefaultExt = "xsd";
            this.openFileDialog1.Filter = "Schema files (*.xsd)|*.xsd|All files (*.*)|*.*";
            // 
            // butSelectValidationFile
            // 
            this.butSelectValidationFile.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.butSelectValidationFile.Location = new System.Drawing.Point(374, 158);
            this.butSelectValidationFile.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.butSelectValidationFile.Name = "butSelectValidationFile";
            this.butSelectValidationFile.Size = new System.Drawing.Size(25, 19);
            this.butSelectValidationFile.TabIndex = 15;
            this.butSelectValidationFile.Text = "...";
            this.butSelectValidationFile.UseVisualStyleBackColor = true;
            this.butSelectValidationFile.Click += new System.EventHandler(this.butSelectValidationFile_Click);
            // 
            // butSelectCustomEditor
            // 
            this.butSelectCustomEditor.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.butSelectCustomEditor.Location = new System.Drawing.Point(374, 210);
            this.butSelectCustomEditor.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.butSelectCustomEditor.Name = "butSelectCustomEditor";
            this.butSelectCustomEditor.Size = new System.Drawing.Size(25, 19);
            this.butSelectCustomEditor.TabIndex = 16;
            this.butSelectCustomEditor.Text = "...";
            this.butSelectCustomEditor.UseVisualStyleBackColor = true;
            this.butSelectCustomEditor.Click += new System.EventHandler(this.butSelectCustomEditor_Click);
            // 
            // openFileDialog2
            // 
            this.openFileDialog2.AddExtension = false;
            this.openFileDialog2.DefaultExt = "exe";
            this.openFileDialog2.Filter = "Executable files (*.exe)|*.exe|All files (*.*)|*.*";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(31, 160);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 17;
            this.label2.Text = "Schema:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(31, 212);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 13);
            this.label3.TabIndex = 18;
            this.label3.Text = "Editor:";
            // 
            // OptionsForm
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(455, 345);
            this.ControlBox = false;
            this.Controls.Add(this.checkboxAutoRowSize);
            this.Controls.Add(this.checkboxShowVarProperties);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.butSelectCustomEditor);
            this.Controls.Add(this.butSelectValidationFile);
            this.Controls.Add(this.checkboxUseValidationSchema);
            this.Controls.Add(this.textboxUseValidationSchema);
            this.Controls.Add(this.checkboxIgnoreFormSizeOnReload);
            this.Controls.Add(this.checkboxCustomEditor);
            this.Controls.Add(this.textboxCustomEditor);
            this.Controls.Add(this.checkboxAutostartEdit);
            this.Controls.Add(this.checkboxNextRowOnEnter);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numericLoggerCapacity);
            this.Controls.Add(this.CheckboxCheckForUpdates);
            this.Controls.Add(this.checkboxBeepOnInputError);
            this.Controls.Add(this.checkboxUseLocalOverrides);
            this.Controls.Add(this.checkboxStopOnCommsError);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OptionsForm";
            this.Padding = new System.Windows.Forms.Padding(0, 0, 0, 6);
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
        private System.Windows.Forms.CheckBox checkboxNextRowOnEnter;
        private System.Windows.Forms.CheckBox checkboxAutostartEdit;
        private System.Windows.Forms.TextBox textboxCustomEditor;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.CheckBox checkboxCustomEditor;
        private System.Windows.Forms.CheckBox checkboxIgnoreFormSizeOnReload;
        private System.Windows.Forms.CheckBox checkboxShowVarProperties;
        private System.Windows.Forms.CheckBox checkboxAutoRowSize;
        private System.Windows.Forms.CheckBox checkboxUseValidationSchema;
        private System.Windows.Forms.TextBox textboxUseValidationSchema;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button butSelectValidationFile;
        private System.Windows.Forms.Button butSelectCustomEditor;
        private System.Windows.Forms.OpenFileDialog openFileDialog2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;

    }
}