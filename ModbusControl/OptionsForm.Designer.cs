namespace ModbusControl
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OptionsForm));
            checkboxStopOnCommsError = new CheckBox();
            checkboxBeepOnInputError = new CheckBox();
            CheckboxCheckForUpdates = new CheckBox();
            label1 = new Label();
            numericLoggerCapacity = new NumericUpDown();
            checkboxNextRowOnEnter = new CheckBox();
            checkboxAutostartEdit = new CheckBox();
            textboxCustomEditor = new TextBox();
            toolTip1 = new ToolTip(components);
            checkboxCustomEditor = new CheckBox();
            checkboxIgnoreFormSizeOnReload = new CheckBox();
            checkboxShowVarProperties = new CheckBox();
            checkboxAutoRowSize = new CheckBox();
            checkboxClearFilterOnTabChange = new CheckBox();
            checkboxUseValidationSchema = new CheckBox();
            butSelectValidationFile = new Button();
            butSelectCustomEditor = new Button();
            butResetValidationFile = new Button();
            textboxUseValidationSchema = new TextBox();
            openFileDialog1 = new OpenFileDialog();
            openFileDialog2 = new OpenFileDialog();
            label2 = new Label();
            label3 = new Label();
            buttonCancel = new Button();
            buttonOK = new Button();
            ((System.ComponentModel.ISupportInitialize)numericLoggerCapacity).BeginInit();
            SuspendLayout();
            // 
            // checkboxStopOnCommsError
            // 
            checkboxStopOnCommsError.AutoSize = true;
            checkboxStopOnCommsError.Location = new Point(263, 12);
            checkboxStopOnCommsError.Name = "checkboxStopOnCommsError";
            checkboxStopOnCommsError.Size = new Size(120, 17);
            checkboxStopOnCommsError.TabIndex = 1;
            checkboxStopOnCommsError.Text = "Stop polling on error";
            toolTip1.SetToolTip(checkboxStopOnCommsError, "Stop polling on read error, show message if stopped");
            checkboxStopOnCommsError.UseVisualStyleBackColor = true;
            // 
            // checkboxBeepOnInputError
            // 
            checkboxBeepOnInputError.AutoSize = true;
            checkboxBeepOnInputError.Location = new Point(20, 15);
            checkboxBeepOnInputError.Name = "checkboxBeepOnInputError";
            checkboxBeepOnInputError.Size = new Size(116, 17);
            checkboxBeepOnInputError.TabIndex = 0;
            checkboxBeepOnInputError.Text = "Beep on input error";
            toolTip1.SetToolTip(checkboxBeepOnInputError, "Beep when an invalid value is entered");
            checkboxBeepOnInputError.UseVisualStyleBackColor = true;
            // 
            // CheckboxCheckForUpdates
            // 
            CheckboxCheckForUpdates.AutoSize = true;
            CheckboxCheckForUpdates.Location = new Point(20, 268);
            CheckboxCheckForUpdates.Name = "CheckboxCheckForUpdates";
            CheckboxCheckForUpdates.Size = new Size(154, 17);
            CheckboxCheckForUpdates.TabIndex = 19;
            CheckboxCheckForUpdates.Text = "Check for program updates";
            toolTip1.SetToolTip(CheckboxCheckForUpdates, "Check for program update on start, once per day");
            CheckboxCheckForUpdates.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(18, 244);
            label1.Margin = new Padding(0, 5, 3, 0);
            label1.Name = "label1";
            label1.Size = new Size(91, 13);
            label1.TabIndex = 17;
            label1.Text = "Logging capacity:";
            // 
            // numericLoggerCapacity
            // 
            numericLoggerCapacity.Increment = new decimal(new int[] { 1000, 0, 0, 0 });
            numericLoggerCapacity.Location = new Point(119, 242);
            numericLoggerCapacity.Maximum = new decimal(new int[] { 999999, 0, 0, 0 });
            numericLoggerCapacity.Minimum = new decimal(new int[] { 1000, 0, 0, 0 });
            numericLoggerCapacity.Name = "numericLoggerCapacity";
            numericLoggerCapacity.Size = new Size(75, 20);
            numericLoggerCapacity.TabIndex = 18;
            toolTip1.SetToolTip(numericLoggerCapacity, "Logging memory capacity, number of lines");
            numericLoggerCapacity.Value = new decimal(new int[] { 5000, 0, 0, 0 });
            // 
            // checkboxNextRowOnEnter
            // 
            checkboxNextRowOnEnter.AutoSize = true;
            checkboxNextRowOnEnter.Location = new Point(20, 38);
            checkboxNextRowOnEnter.Name = "checkboxNextRowOnEnter";
            checkboxNextRowOnEnter.Size = new Size(151, 17);
            checkboxNextRowOnEnter.TabIndex = 2;
            checkboxNextRowOnEnter.Text = "Move to next row on Enter";
            toolTip1.SetToolTip(checkboxNextRowOnEnter, "Move to the next row after entering a new value");
            checkboxNextRowOnEnter.UseVisualStyleBackColor = true;
            // 
            // checkboxAutostartEdit
            // 
            checkboxAutostartEdit.AutoSize = true;
            checkboxAutostartEdit.Location = new Point(20, 59);
            checkboxAutostartEdit.Name = "checkboxAutostartEdit";
            checkboxAutostartEdit.Size = new Size(166, 17);
            checkboxAutostartEdit.TabIndex = 4;
            checkboxAutostartEdit.Text = "Auto-start edit mode on typing";
            toolTip1.SetToolTip(checkboxAutostartEdit, "Auto-start edit to enter a new value, without pressing F2 or double-clicking");
            checkboxAutostartEdit.UseVisualStyleBackColor = true;
            // 
            // textboxCustomEditor
            // 
            textboxCustomEditor.Location = new Point(79, 215);
            textboxCustomEditor.Margin = new Padding(2);
            textboxCustomEditor.Name = "textboxCustomEditor";
            textboxCustomEditor.Size = new Size(346, 20);
            textboxCustomEditor.TabIndex = 15;
            // 
            // checkboxCustomEditor
            // 
            checkboxCustomEditor.AutoSize = true;
            checkboxCustomEditor.Location = new Point(20, 193);
            checkboxCustomEditor.Name = "checkboxCustomEditor";
            checkboxCustomEditor.Size = new Size(111, 17);
            checkboxCustomEditor.TabIndex = 13;
            checkboxCustomEditor.Text = "Use custom editor";
            toolTip1.SetToolTip(checkboxCustomEditor, "Use this editor instead of the default one");
            checkboxCustomEditor.UseVisualStyleBackColor = true;
            // 
            // checkboxIgnoreFormSizeOnReload
            // 
            checkboxIgnoreFormSizeOnReload.AutoSize = true;
            checkboxIgnoreFormSizeOnReload.Location = new Point(263, 38);
            checkboxIgnoreFormSizeOnReload.Name = "checkboxIgnoreFormSizeOnReload";
            checkboxIgnoreFormSizeOnReload.Size = new Size(182, 17);
            checkboxIgnoreFormSizeOnReload.TabIndex = 3;
            checkboxIgnoreFormSizeOnReload.Text = "Ignore form size on project reload";
            toolTip1.SetToolTip(checkboxIgnoreFormSizeOnReload, "Do not use the form size specified in the mbc file on project reload");
            checkboxIgnoreFormSizeOnReload.UseVisualStyleBackColor = true;
            // 
            // checkboxShowVarProperties
            // 
            checkboxShowVarProperties.AutoSize = true;
            checkboxShowVarProperties.Location = new Point(20, 81);
            checkboxShowVarProperties.Name = "checkboxShowVarProperties";
            checkboxShowVarProperties.Size = new Size(176, 17);
            checkboxShowVarProperties.TabIndex = 5;
            checkboxShowVarProperties.Text = "Show register properties column";
            toolTip1.SetToolTip(checkboxShowVarProperties, "Show an extra column with the register properties");
            checkboxShowVarProperties.UseVisualStyleBackColor = true;
            // 
            // checkboxAutoRowSize
            // 
            checkboxAutoRowSize.AutoSize = true;
            checkboxAutoRowSize.Location = new Point(20, 103);
            checkboxAutoRowSize.Name = "checkboxAutoRowSize";
            checkboxAutoRowSize.Size = new Size(176, 17);
            checkboxAutoRowSize.TabIndex = 6;
            checkboxAutoRowSize.Text = "Multiline rows with auto row size";
            toolTip1.SetToolTip(checkboxAutoRowSize, "Allow multi-line rows and adjust row heights automatically to fit their contents");
            checkboxAutoRowSize.UseVisualStyleBackColor = true;
            // 
            // checkboxClearFilterOnTabChange
            // 
            checkboxClearFilterOnTabChange.AutoSize = true;
            checkboxClearFilterOnTabChange.Location = new Point(20, 125);
            checkboxClearFilterOnTabChange.Name = "checkboxClearFilterOnTabChange";
            checkboxClearFilterOnTabChange.Size = new Size(169, 17);
            checkboxClearFilterOnTabChange.TabIndex = 7;
            checkboxClearFilterOnTabChange.Text = "Clear Find/filter on tab change";
            toolTip1.SetToolTip(checkboxClearFilterOnTabChange, "Clear the Find/filter when user switches to other tab page");
            checkboxClearFilterOnTabChange.UseVisualStyleBackColor = true;
            // 
            // checkboxUseValidationSchema
            // 
            checkboxUseValidationSchema.AutoSize = true;
            checkboxUseValidationSchema.Location = new Point(20, 147);
            checkboxUseValidationSchema.Margin = new Padding(2);
            checkboxUseValidationSchema.Name = "checkboxUseValidationSchema";
            checkboxUseValidationSchema.Size = new Size(231, 17);
            checkboxUseValidationSchema.TabIndex = 8;
            checkboxUseValidationSchema.Text = "Use XSD validation schema on project load";
            toolTip1.SetToolTip(checkboxUseValidationSchema, "When an mbc project file is loaded, validate it against an XSD schema");
            checkboxUseValidationSchema.UseVisualStyleBackColor = true;
            // 
            // butSelectValidationFile
            // 
            butSelectValidationFile.AutoSize = true;
            butSelectValidationFile.BackgroundImageLayout = ImageLayout.Stretch;
            butSelectValidationFile.Location = new Point(439, 167);
            butSelectValidationFile.Margin = new Padding(2, 1, 2, 2);
            butSelectValidationFile.Name = "butSelectValidationFile";
            butSelectValidationFile.Size = new Size(26, 23);
            butSelectValidationFile.TabIndex = 11;
            butSelectValidationFile.Text = "...";
            toolTip1.SetToolTip(butSelectValidationFile, "Browse for a file");
            butSelectValidationFile.UseVisualStyleBackColor = true;
            butSelectValidationFile.Click += butSelectValidationFile_Click;
            // 
            // butSelectCustomEditor
            // 
            butSelectCustomEditor.AutoSize = true;
            butSelectCustomEditor.BackgroundImageLayout = ImageLayout.Stretch;
            butSelectCustomEditor.Location = new Point(429, 214);
            butSelectCustomEditor.Margin = new Padding(2, 1, 2, 2);
            butSelectCustomEditor.Name = "butSelectCustomEditor";
            butSelectCustomEditor.Size = new Size(26, 23);
            butSelectCustomEditor.TabIndex = 16;
            butSelectCustomEditor.Text = "...";
            toolTip1.SetToolTip(butSelectCustomEditor, "Browse for a file");
            butSelectCustomEditor.UseVisualStyleBackColor = true;
            butSelectCustomEditor.Click += butSelectCustomEditor_Click;
            // 
            // butResetValidationFile
            // 
            butResetValidationFile.AutoSize = true;
            butResetValidationFile.Image = (Image)resources.GetObject("butResetValidationFile.Image");
            butResetValidationFile.Location = new Point(467, 167);
            butResetValidationFile.Margin = new Padding(2, 1, 2, 2);
            butResetValidationFile.Name = "butResetValidationFile";
            butResetValidationFile.Size = new Size(25, 23);
            butResetValidationFile.TabIndex = 12;
            toolTip1.SetToolTip(butResetValidationFile, "Revert to default");
            butResetValidationFile.UseVisualStyleBackColor = true;
            butResetValidationFile.Click += butResetValidationFile_Click;
            // 
            // textboxUseValidationSchema
            // 
            textboxUseValidationSchema.Location = new Point(91, 168);
            textboxUseValidationSchema.Margin = new Padding(2);
            textboxUseValidationSchema.Name = "textboxUseValidationSchema";
            textboxUseValidationSchema.Size = new Size(344, 20);
            textboxUseValidationSchema.TabIndex = 10;
            // 
            // openFileDialog1
            // 
            openFileDialog1.AddExtension = false;
            openFileDialog1.DefaultExt = "xsd";
            openFileDialog1.Filter = "Schema files (*.xsd)|*.xsd|All files (*.*)|*.*";
            // 
            // openFileDialog2
            // 
            openFileDialog2.AddExtension = false;
            openFileDialog2.DefaultExt = "exe";
            openFileDialog2.Filter = "Executable files (*.exe)|*.exe|All files (*.*)|*.*";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(34, 171);
            label2.Margin = new Padding(2, 5, 2, 0);
            label2.Name = "label2";
            label2.Size = new Size(49, 13);
            label2.TabIndex = 9;
            label2.Text = "Schema:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(34, 218);
            label3.Margin = new Padding(2, 5, 2, 0);
            label3.Name = "label3";
            label3.Size = new Size(37, 13);
            label3.TabIndex = 14;
            label3.Text = "Editor:";
            // 
            // buttonCancel
            // 
            buttonCancel.DialogResult = DialogResult.Cancel;
            buttonCancel.Location = new Point(270, 298);
            buttonCancel.Name = "buttonCancel";
            buttonCancel.Size = new Size(75, 23);
            buttonCancel.TabIndex = 21;
            buttonCancel.Text = "Cancel";
            buttonCancel.UseVisualStyleBackColor = true;
            // 
            // buttonOK
            // 
            buttonOK.DialogResult = DialogResult.OK;
            buttonOK.Location = new Point(173, 298);
            buttonOK.Name = "buttonOK";
            buttonOK.Size = new Size(75, 23);
            buttonOK.TabIndex = 20;
            buttonOK.Text = "OK";
            buttonOK.UseVisualStyleBackColor = true;
            // 
            // OptionsForm
            // 
            AcceptButton = buttonOK;
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            CancelButton = buttonCancel;
            ClientSize = new Size(516, 334);
            Controls.Add(buttonCancel);
            Controls.Add(numericLoggerCapacity);
            Controls.Add(buttonOK);
            Controls.Add(label1);
            Controls.Add(butSelectCustomEditor);
            Controls.Add(butResetValidationFile);
            Controls.Add(CheckboxCheckForUpdates);
            Controls.Add(textboxCustomEditor);
            Controls.Add(label3);
            Controls.Add(butSelectValidationFile);
            Controls.Add(checkboxStopOnCommsError);
            Controls.Add(checkboxCustomEditor);
            Controls.Add(textboxUseValidationSchema);
            Controls.Add(label2);
            Controls.Add(checkboxBeepOnInputError);
            Controls.Add(checkboxNextRowOnEnter);
            Controls.Add(checkboxUseValidationSchema);
            Controls.Add(checkboxClearFilterOnTabChange);
            Controls.Add(checkboxAutostartEdit);
            Controls.Add(checkboxIgnoreFormSizeOnReload);
            Controls.Add(checkboxShowVarProperties);
            Controls.Add(checkboxAutoRowSize);
            Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "OptionsForm";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Tag = "";
            Text = "Options";
            Shown += OptionsForm_Shown;
            ((System.ComponentModel.ISupportInitialize)numericLoggerCapacity).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion
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
        private System.Windows.Forms.CheckBox checkboxClearFilterOnTabChange;
        private System.Windows.Forms.Button butResetValidationFile;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
    }
}