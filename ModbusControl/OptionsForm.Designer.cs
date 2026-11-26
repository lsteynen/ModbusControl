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
            StopOnPollingErrorCheckbox = new CheckBox();
            BeepOnInputErrorCheckbox = new CheckBox();
            CheckForUpdatesCheckbox = new CheckBox();
            label1 = new Label();
            LoggerCapacityNumeric = new NumericUpDown();
            NextRowOnEnterCheckbox = new CheckBox();
            AutostartEditCheckbox = new CheckBox();
            CustomEditorTextbox = new TextBox();
            toolTip1 = new ToolTip(components);
            CustomEditorCheckbox = new CheckBox();
            IgnoreFormSizeOnReloadCheckbox = new CheckBox();
            ShowVarPropertiesCheckbox = new CheckBox();
            AutoRowSizeCheckbox = new CheckBox();
            ClearFilterOnTabChangeCheckbox = new CheckBox();
            UseValidationSchemaCheckbox = new CheckBox();
            SelectValidationFileBtn = new Button();
            SelectCustomEditorBtn = new Button();
            DefaultValidationFileBtn = new Button();
            UseValidationSchemaTextbox = new TextBox();
            openFileDialog1 = new OpenFileDialog();
            openFileDialog2 = new OpenFileDialog();
            label2 = new Label();
            label3 = new Label();
            CancelBtn = new Button();
            OkBtn = new Button();
            ((System.ComponentModel.ISupportInitialize)LoggerCapacityNumeric).BeginInit();
            SuspendLayout();
            // 
            // StopOnPollingErrorCheckbox
            // 
            StopOnPollingErrorCheckbox.AutoSize = true;
            StopOnPollingErrorCheckbox.Location = new Point(263, 12);
            StopOnPollingErrorCheckbox.Name = "StopOnPollingErrorCheckbox";
            StopOnPollingErrorCheckbox.Size = new Size(120, 17);
            StopOnPollingErrorCheckbox.TabIndex = 1;
            StopOnPollingErrorCheckbox.Text = "Stop polling on error";
            toolTip1.SetToolTip(StopOnPollingErrorCheckbox, "Stop polling on read error, show message if stopped");
            StopOnPollingErrorCheckbox.UseVisualStyleBackColor = true;
            // 
            // BeepOnInputErrorCheckbox
            // 
            BeepOnInputErrorCheckbox.AutoSize = true;
            BeepOnInputErrorCheckbox.Location = new Point(20, 15);
            BeepOnInputErrorCheckbox.Name = "BeepOnInputErrorCheckbox";
            BeepOnInputErrorCheckbox.Size = new Size(116, 17);
            BeepOnInputErrorCheckbox.TabIndex = 0;
            BeepOnInputErrorCheckbox.Text = "Beep on input error";
            toolTip1.SetToolTip(BeepOnInputErrorCheckbox, "Beep when an invalid value is entered");
            BeepOnInputErrorCheckbox.UseVisualStyleBackColor = true;
            // 
            // CheckForUpdatesCheckbox
            // 
            CheckForUpdatesCheckbox.AutoSize = true;
            CheckForUpdatesCheckbox.Location = new Point(20, 268);
            CheckForUpdatesCheckbox.Name = "CheckForUpdatesCheckbox";
            CheckForUpdatesCheckbox.Size = new Size(154, 17);
            CheckForUpdatesCheckbox.TabIndex = 19;
            CheckForUpdatesCheckbox.Text = "Check for program updates";
            toolTip1.SetToolTip(CheckForUpdatesCheckbox, "Check for program update on start, once per day");
            CheckForUpdatesCheckbox.UseVisualStyleBackColor = true;
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
            // LoggerCapacityNumeric
            // 
            LoggerCapacityNumeric.Increment = new decimal(new int[] { 1000, 0, 0, 0 });
            LoggerCapacityNumeric.Location = new Point(119, 242);
            LoggerCapacityNumeric.Maximum = new decimal(new int[] { 999999, 0, 0, 0 });
            LoggerCapacityNumeric.Minimum = new decimal(new int[] { 1000, 0, 0, 0 });
            LoggerCapacityNumeric.Name = "LoggerCapacityNumeric";
            LoggerCapacityNumeric.Size = new Size(75, 20);
            LoggerCapacityNumeric.TabIndex = 18;
            toolTip1.SetToolTip(LoggerCapacityNumeric, "Logging memory capacity, number of lines");
            LoggerCapacityNumeric.Value = new decimal(new int[] { 5000, 0, 0, 0 });
            // 
            // NextRowOnEnterCheckbox
            // 
            NextRowOnEnterCheckbox.AutoSize = true;
            NextRowOnEnterCheckbox.Location = new Point(20, 38);
            NextRowOnEnterCheckbox.Name = "NextRowOnEnterCheckbox";
            NextRowOnEnterCheckbox.Size = new Size(151, 17);
            NextRowOnEnterCheckbox.TabIndex = 2;
            NextRowOnEnterCheckbox.Text = "Move to next row on Enter";
            toolTip1.SetToolTip(NextRowOnEnterCheckbox, "Move to the next row after entering a new value");
            NextRowOnEnterCheckbox.UseVisualStyleBackColor = true;
            // 
            // AutostartEditCheckbox
            // 
            AutostartEditCheckbox.AutoSize = true;
            AutostartEditCheckbox.Location = new Point(20, 59);
            AutostartEditCheckbox.Name = "AutostartEditCheckbox";
            AutostartEditCheckbox.Size = new Size(166, 17);
            AutostartEditCheckbox.TabIndex = 4;
            AutostartEditCheckbox.Text = "Auto-start edit mode on typing";
            toolTip1.SetToolTip(AutostartEditCheckbox, "Auto-start edit to enter a new value, without pressing F2 or double-clicking");
            AutostartEditCheckbox.UseVisualStyleBackColor = true;
            // 
            // CustomEditorTextbox
            // 
            CustomEditorTextbox.Location = new Point(79, 215);
            CustomEditorTextbox.Margin = new Padding(2);
            CustomEditorTextbox.Name = "CustomEditorTextbox";
            CustomEditorTextbox.Size = new Size(346, 20);
            CustomEditorTextbox.TabIndex = 15;
            // 
            // CustomEditorCheckbox
            // 
            CustomEditorCheckbox.AutoSize = true;
            CustomEditorCheckbox.Location = new Point(20, 193);
            CustomEditorCheckbox.Name = "CustomEditorCheckbox";
            CustomEditorCheckbox.Size = new Size(111, 17);
            CustomEditorCheckbox.TabIndex = 13;
            CustomEditorCheckbox.Text = "Use custom editor";
            toolTip1.SetToolTip(CustomEditorCheckbox, "Use this editor instead of the default one");
            CustomEditorCheckbox.UseVisualStyleBackColor = true;
            // 
            // IgnoreFormSizeOnReloadCheckbox
            // 
            IgnoreFormSizeOnReloadCheckbox.AutoSize = true;
            IgnoreFormSizeOnReloadCheckbox.Location = new Point(263, 38);
            IgnoreFormSizeOnReloadCheckbox.Name = "IgnoreFormSizeOnReloadCheckbox";
            IgnoreFormSizeOnReloadCheckbox.Size = new Size(198, 17);
            IgnoreFormSizeOnReloadCheckbox.TabIndex = 3;
            IgnoreFormSizeOnReloadCheckbox.Text = "Ignore window size on project reload";
            toolTip1.SetToolTip(IgnoreFormSizeOnReloadCheckbox, "Do not use the window size specified in the mbc file on project reload");
            IgnoreFormSizeOnReloadCheckbox.UseVisualStyleBackColor = true;
            // 
            // ShowVarPropertiesCheckbox
            // 
            ShowVarPropertiesCheckbox.AutoSize = true;
            ShowVarPropertiesCheckbox.Location = new Point(20, 81);
            ShowVarPropertiesCheckbox.Name = "ShowVarPropertiesCheckbox";
            ShowVarPropertiesCheckbox.Size = new Size(176, 17);
            ShowVarPropertiesCheckbox.TabIndex = 5;
            ShowVarPropertiesCheckbox.Text = "Show register properties column";
            toolTip1.SetToolTip(ShowVarPropertiesCheckbox, "Show an extra column with the register properties");
            ShowVarPropertiesCheckbox.UseVisualStyleBackColor = true;
            // 
            // AutoRowSizeCheckbox
            // 
            AutoRowSizeCheckbox.AutoSize = true;
            AutoRowSizeCheckbox.Location = new Point(20, 103);
            AutoRowSizeCheckbox.Name = "AutoRowSizeCheckbox";
            AutoRowSizeCheckbox.Size = new Size(176, 17);
            AutoRowSizeCheckbox.TabIndex = 6;
            AutoRowSizeCheckbox.Text = "Multiline rows with auto row size";
            toolTip1.SetToolTip(AutoRowSizeCheckbox, "Allow multi-line rows and adjust row heights automatically to fit their contents");
            AutoRowSizeCheckbox.UseVisualStyleBackColor = true;
            // 
            // ClearFilterOnTabChangeCheckbox
            // 
            ClearFilterOnTabChangeCheckbox.AutoSize = true;
            ClearFilterOnTabChangeCheckbox.Location = new Point(20, 125);
            ClearFilterOnTabChangeCheckbox.Name = "ClearFilterOnTabChangeCheckbox";
            ClearFilterOnTabChangeCheckbox.Size = new Size(169, 17);
            ClearFilterOnTabChangeCheckbox.TabIndex = 7;
            ClearFilterOnTabChangeCheckbox.Text = "Clear Find/filter on tab change";
            toolTip1.SetToolTip(ClearFilterOnTabChangeCheckbox, "Clear the Find/filter when user switches to other tab page");
            ClearFilterOnTabChangeCheckbox.UseVisualStyleBackColor = true;
            // 
            // UseValidationSchemaCheckbox
            // 
            UseValidationSchemaCheckbox.AutoSize = true;
            UseValidationSchemaCheckbox.Location = new Point(20, 147);
            UseValidationSchemaCheckbox.Margin = new Padding(2);
            UseValidationSchemaCheckbox.Name = "UseValidationSchemaCheckbox";
            UseValidationSchemaCheckbox.Size = new Size(231, 17);
            UseValidationSchemaCheckbox.TabIndex = 8;
            UseValidationSchemaCheckbox.Text = "Use XSD validation schema on project load";
            toolTip1.SetToolTip(UseValidationSchemaCheckbox, "When an mbc project file is loaded, validate it against an XSD schema");
            UseValidationSchemaCheckbox.UseVisualStyleBackColor = true;
            // 
            // SelectValidationFileBtn
            // 
            SelectValidationFileBtn.AutoSize = true;
            SelectValidationFileBtn.BackgroundImageLayout = ImageLayout.Stretch;
            SelectValidationFileBtn.Location = new Point(439, 167);
            SelectValidationFileBtn.Margin = new Padding(2, 1, 2, 2);
            SelectValidationFileBtn.Name = "SelectValidationFileBtn";
            SelectValidationFileBtn.Size = new Size(26, 23);
            SelectValidationFileBtn.TabIndex = 11;
            SelectValidationFileBtn.Text = "...";
            toolTip1.SetToolTip(SelectValidationFileBtn, "Browse for a file");
            SelectValidationFileBtn.UseVisualStyleBackColor = true;
            SelectValidationFileBtn.Click += SelectValidationFileBtn_Click;
            // 
            // SelectCustomEditorBtn
            // 
            SelectCustomEditorBtn.AutoSize = true;
            SelectCustomEditorBtn.BackgroundImageLayout = ImageLayout.Stretch;
            SelectCustomEditorBtn.Location = new Point(429, 214);
            SelectCustomEditorBtn.Margin = new Padding(2, 1, 2, 2);
            SelectCustomEditorBtn.Name = "SelectCustomEditorBtn";
            SelectCustomEditorBtn.Size = new Size(26, 23);
            SelectCustomEditorBtn.TabIndex = 16;
            SelectCustomEditorBtn.Text = "...";
            toolTip1.SetToolTip(SelectCustomEditorBtn, "Browse for a file");
            SelectCustomEditorBtn.UseVisualStyleBackColor = true;
            SelectCustomEditorBtn.Click += SelectCustomEditorBtn_Click;
            // 
            // DefaultValidationFileBtn
            // 
            DefaultValidationFileBtn.AutoSize = true;
            DefaultValidationFileBtn.Image = (Image)resources.GetObject("DefaultValidationFileBtn.Image");
            DefaultValidationFileBtn.Location = new Point(467, 167);
            DefaultValidationFileBtn.Margin = new Padding(2, 1, 2, 2);
            DefaultValidationFileBtn.Name = "DefaultValidationFileBtn";
            DefaultValidationFileBtn.Size = new Size(25, 23);
            DefaultValidationFileBtn.TabIndex = 12;
            toolTip1.SetToolTip(DefaultValidationFileBtn, "Revert to default");
            DefaultValidationFileBtn.UseVisualStyleBackColor = true;
            DefaultValidationFileBtn.Click += DefaultValidationFileBtn_Click;
            // 
            // UseValidationSchemaTextbox
            // 
            UseValidationSchemaTextbox.Location = new Point(91, 168);
            UseValidationSchemaTextbox.Margin = new Padding(2);
            UseValidationSchemaTextbox.Name = "UseValidationSchemaTextbox";
            UseValidationSchemaTextbox.Size = new Size(344, 20);
            UseValidationSchemaTextbox.TabIndex = 10;
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
            // CancelBtn
            // 
            CancelBtn.DialogResult = DialogResult.Cancel;
            CancelBtn.Location = new Point(270, 298);
            CancelBtn.Name = "CancelBtn";
            CancelBtn.Size = new Size(75, 23);
            CancelBtn.TabIndex = 21;
            CancelBtn.Text = "Cancel";
            CancelBtn.UseVisualStyleBackColor = true;
            // 
            // OkBtn
            // 
            OkBtn.DialogResult = DialogResult.OK;
            OkBtn.Location = new Point(173, 298);
            OkBtn.Name = "OkBtn";
            OkBtn.Size = new Size(75, 23);
            OkBtn.TabIndex = 20;
            OkBtn.Text = "OK";
            OkBtn.UseVisualStyleBackColor = true;
            // 
            // OptionsForm
            // 
            AcceptButton = OkBtn;
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            CancelButton = CancelBtn;
            ClientSize = new Size(516, 334);
            Controls.Add(CancelBtn);
            Controls.Add(LoggerCapacityNumeric);
            Controls.Add(OkBtn);
            Controls.Add(label1);
            Controls.Add(SelectCustomEditorBtn);
            Controls.Add(DefaultValidationFileBtn);
            Controls.Add(CheckForUpdatesCheckbox);
            Controls.Add(CustomEditorTextbox);
            Controls.Add(label3);
            Controls.Add(SelectValidationFileBtn);
            Controls.Add(StopOnPollingErrorCheckbox);
            Controls.Add(CustomEditorCheckbox);
            Controls.Add(UseValidationSchemaTextbox);
            Controls.Add(label2);
            Controls.Add(BeepOnInputErrorCheckbox);
            Controls.Add(NextRowOnEnterCheckbox);
            Controls.Add(UseValidationSchemaCheckbox);
            Controls.Add(ClearFilterOnTabChangeCheckbox);
            Controls.Add(AutostartEditCheckbox);
            Controls.Add(IgnoreFormSizeOnReloadCheckbox);
            Controls.Add(ShowVarPropertiesCheckbox);
            Controls.Add(AutoRowSizeCheckbox);
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
            ((System.ComponentModel.ISupportInitialize)LoggerCapacityNumeric).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion
        private System.Windows.Forms.CheckBox StopOnPollingErrorCheckbox;
        private System.Windows.Forms.CheckBox BeepOnInputErrorCheckbox;
        private System.Windows.Forms.CheckBox CheckForUpdatesCheckbox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown LoggerCapacityNumeric;
        private System.Windows.Forms.CheckBox NextRowOnEnterCheckbox;
        private System.Windows.Forms.CheckBox AutostartEditCheckbox;
        private System.Windows.Forms.TextBox CustomEditorTextbox;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.CheckBox CustomEditorCheckbox;
        private System.Windows.Forms.CheckBox IgnoreFormSizeOnReloadCheckbox;
        private System.Windows.Forms.CheckBox ShowVarPropertiesCheckbox;
        private System.Windows.Forms.CheckBox AutoRowSizeCheckbox;
        private System.Windows.Forms.CheckBox UseValidationSchemaCheckbox;
        private System.Windows.Forms.TextBox UseValidationSchemaTextbox;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button SelectValidationFileBtn;
        private System.Windows.Forms.Button SelectCustomEditorBtn;
        private System.Windows.Forms.OpenFileDialog openFileDialog2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox ClearFilterOnTabChangeCheckbox;
        private System.Windows.Forms.Button DefaultValidationFileBtn;
        private System.Windows.Forms.Button OkBtn;
        private System.Windows.Forms.Button CancelBtn;
    }
}