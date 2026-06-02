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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OptionsForm));
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
            this.checkboxClearFilterOnTabChange = new System.Windows.Forms.CheckBox();
            this.checkboxUseValidationSchema = new System.Windows.Forms.CheckBox();
            this.butSelectValidationFile = new System.Windows.Forms.Button();
            this.butSelectCustomEditor = new System.Windows.Forms.Button();
            this.butResetValidationFile = new System.Windows.Forms.Button();
            this.textboxUseValidationSchema = new System.Windows.Forms.TextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.openFileDialog2 = new System.Windows.Forms.OpenFileDialog();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.numericLoggerCapacity)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // checkboxStopOnCommsError
            // 
            this.checkboxStopOnCommsError.AutoSize = true;
            this.checkboxStopOnCommsError.Location = new System.Drawing.Point(250, 3);
            this.checkboxStopOnCommsError.Name = "checkboxStopOnCommsError";
            this.checkboxStopOnCommsError.Size = new System.Drawing.Size(120, 17);
            this.checkboxStopOnCommsError.TabIndex = 1;
            this.checkboxStopOnCommsError.Text = "Stop polling on error";
            this.toolTip1.SetToolTip(this.checkboxStopOnCommsError, "Stop polling on read error, show message if stopped");
            this.checkboxStopOnCommsError.UseVisualStyleBackColor = true;
            // 
            // checkboxBeepOnInputError
            // 
            this.checkboxBeepOnInputError.AutoSize = true;
            this.checkboxBeepOnInputError.Location = new System.Drawing.Point(3, 3);
            this.checkboxBeepOnInputError.Name = "checkboxBeepOnInputError";
            this.checkboxBeepOnInputError.Size = new System.Drawing.Size(116, 17);
            this.checkboxBeepOnInputError.TabIndex = 0;
            this.checkboxBeepOnInputError.Text = "Beep on input error";
            this.toolTip1.SetToolTip(this.checkboxBeepOnInputError, "Beep when an invalid value is entered");
            this.checkboxBeepOnInputError.UseVisualStyleBackColor = true;
            // 
            // CheckboxCheckForUpdates
            // 
            this.CheckboxCheckForUpdates.AutoSize = true;
            this.CheckboxCheckForUpdates.Location = new System.Drawing.Point(3, 260);
            this.CheckboxCheckForUpdates.Name = "CheckboxCheckForUpdates";
            this.CheckboxCheckForUpdates.Size = new System.Drawing.Size(154, 17);
            this.CheckboxCheckForUpdates.TabIndex = 13;
            this.CheckboxCheckForUpdates.Text = "Check for program updates";
            this.toolTip1.SetToolTip(this.CheckboxCheckForUpdates, "Check for program update on start, once per day");
            this.CheckboxCheckForUpdates.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(0, 5);
            this.label1.Margin = new System.Windows.Forms.Padding(0, 5, 3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Logging capacity:";
            // 
            // numericLoggerCapacity
            // 
            this.numericLoggerCapacity.Increment = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericLoggerCapacity.Location = new System.Drawing.Point(97, 3);
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
            this.numericLoggerCapacity.TabIndex = 1;
            this.toolTip1.SetToolTip(this.numericLoggerCapacity, "Logging memory capacity, number of lines");
            this.numericLoggerCapacity.Value = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            // 
            // checkboxNextRowOnEnter
            // 
            this.checkboxNextRowOnEnter.AutoSize = true;
            this.checkboxNextRowOnEnter.Location = new System.Drawing.Point(3, 26);
            this.checkboxNextRowOnEnter.Name = "checkboxNextRowOnEnter";
            this.checkboxNextRowOnEnter.Size = new System.Drawing.Size(151, 17);
            this.checkboxNextRowOnEnter.TabIndex = 2;
            this.checkboxNextRowOnEnter.Text = "Move to next row on Enter";
            this.toolTip1.SetToolTip(this.checkboxNextRowOnEnter, "Move to the next row after entering a new value");
            this.checkboxNextRowOnEnter.UseVisualStyleBackColor = true;
            // 
            // checkboxAutostartEdit
            // 
            this.checkboxAutostartEdit.AutoSize = true;
            this.checkboxAutostartEdit.Location = new System.Drawing.Point(3, 49);
            this.checkboxAutostartEdit.Name = "checkboxAutostartEdit";
            this.checkboxAutostartEdit.Size = new System.Drawing.Size(166, 17);
            this.checkboxAutostartEdit.TabIndex = 4;
            this.checkboxAutostartEdit.Text = "Auto-start edit mode on typing";
            this.toolTip1.SetToolTip(this.checkboxAutostartEdit, "Auto-start edit to enter a new value, without pressing F2 or double-clicking");
            this.checkboxAutostartEdit.UseVisualStyleBackColor = true;
            // 
            // textboxCustomEditor
            // 
            this.textboxCustomEditor.Location = new System.Drawing.Point(43, 2);
            this.textboxCustomEditor.Margin = new System.Windows.Forms.Padding(2);
            this.textboxCustomEditor.Name = "textboxCustomEditor";
            this.textboxCustomEditor.Size = new System.Drawing.Size(346, 20);
            this.textboxCustomEditor.TabIndex = 1;
            // 
            // checkboxCustomEditor
            // 
            this.checkboxCustomEditor.AutoSize = true;
            this.checkboxCustomEditor.Location = new System.Drawing.Point(3, 186);
            this.checkboxCustomEditor.Name = "checkboxCustomEditor";
            this.checkboxCustomEditor.Size = new System.Drawing.Size(111, 17);
            this.checkboxCustomEditor.TabIndex = 10;
            this.checkboxCustomEditor.Text = "Use custom editor";
            this.toolTip1.SetToolTip(this.checkboxCustomEditor, "Use this editor instead of the default one");
            this.checkboxCustomEditor.UseVisualStyleBackColor = true;
            // 
            // checkboxIgnoreFormSizeOnReload
            // 
            this.checkboxIgnoreFormSizeOnReload.AutoSize = true;
            this.checkboxIgnoreFormSizeOnReload.Location = new System.Drawing.Point(250, 26);
            this.checkboxIgnoreFormSizeOnReload.Name = "checkboxIgnoreFormSizeOnReload";
            this.checkboxIgnoreFormSizeOnReload.Size = new System.Drawing.Size(182, 17);
            this.checkboxIgnoreFormSizeOnReload.TabIndex = 3;
            this.checkboxIgnoreFormSizeOnReload.Text = "Ignore form size on project reload";
            this.toolTip1.SetToolTip(this.checkboxIgnoreFormSizeOnReload, "Do not use the form size specified in the mbc file on project reload");
            this.checkboxIgnoreFormSizeOnReload.UseVisualStyleBackColor = true;
            // 
            // checkboxShowVarProperties
            // 
            this.checkboxShowVarProperties.AutoSize = true;
            this.checkboxShowVarProperties.Location = new System.Drawing.Point(3, 72);
            this.checkboxShowVarProperties.Name = "checkboxShowVarProperties";
            this.checkboxShowVarProperties.Size = new System.Drawing.Size(176, 17);
            this.checkboxShowVarProperties.TabIndex = 5;
            this.checkboxShowVarProperties.Text = "Show register properties column";
            this.toolTip1.SetToolTip(this.checkboxShowVarProperties, "Show an extra column with the register properties");
            this.checkboxShowVarProperties.UseVisualStyleBackColor = true;
            // 
            // checkboxAutoRowSize
            // 
            this.checkboxAutoRowSize.AutoSize = true;
            this.checkboxAutoRowSize.Location = new System.Drawing.Point(3, 95);
            this.checkboxAutoRowSize.Name = "checkboxAutoRowSize";
            this.checkboxAutoRowSize.Size = new System.Drawing.Size(176, 17);
            this.checkboxAutoRowSize.TabIndex = 6;
            this.checkboxAutoRowSize.Text = "Multiline rows with auto row size";
            this.toolTip1.SetToolTip(this.checkboxAutoRowSize, "Allow multi-line rows and adjust row heights automatically to fit their contents");
            this.checkboxAutoRowSize.UseVisualStyleBackColor = true;
            // 
            // checkboxClearFilterOnTabChange
            // 
            this.checkboxClearFilterOnTabChange.AutoSize = true;
            this.checkboxClearFilterOnTabChange.Location = new System.Drawing.Point(3, 118);
            this.checkboxClearFilterOnTabChange.Name = "checkboxClearFilterOnTabChange";
            this.checkboxClearFilterOnTabChange.Size = new System.Drawing.Size(169, 17);
            this.checkboxClearFilterOnTabChange.TabIndex = 7;
            this.checkboxClearFilterOnTabChange.Text = "Clear Find/filter on tab change";
            this.toolTip1.SetToolTip(this.checkboxClearFilterOnTabChange, "Clear the Find/filter when user switches to other tab page");
            this.checkboxClearFilterOnTabChange.UseVisualStyleBackColor = true;
            // 
            // checkboxUseValidationSchema
            // 
            this.checkboxUseValidationSchema.AutoSize = true;
            this.checkboxUseValidationSchema.Location = new System.Drawing.Point(2, 140);
            this.checkboxUseValidationSchema.Margin = new System.Windows.Forms.Padding(2);
            this.checkboxUseValidationSchema.Name = "checkboxUseValidationSchema";
            this.checkboxUseValidationSchema.Size = new System.Drawing.Size(231, 17);
            this.checkboxUseValidationSchema.TabIndex = 8;
            this.checkboxUseValidationSchema.Text = "Use XSD validation schema on project load";
            this.toolTip1.SetToolTip(this.checkboxUseValidationSchema, "When an mbc project file is loaded, validate it against an XSD schema");
            this.checkboxUseValidationSchema.UseVisualStyleBackColor = true;
            // 
            // butSelectValidationFile
            // 
            this.butSelectValidationFile.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.butSelectValidationFile.Location = new System.Drawing.Point(403, 1);
            this.butSelectValidationFile.Margin = new System.Windows.Forms.Padding(2, 1, 2, 2);
            this.butSelectValidationFile.Name = "butSelectValidationFile";
            this.butSelectValidationFile.Size = new System.Drawing.Size(25, 21);
            this.butSelectValidationFile.TabIndex = 2;
            this.butSelectValidationFile.Text = "...";
            this.toolTip1.SetToolTip(this.butSelectValidationFile, "Browse for a file");
            this.butSelectValidationFile.UseVisualStyleBackColor = true;
            this.butSelectValidationFile.Click += new System.EventHandler(this.butSelectValidationFile_Click);
            // 
            // butSelectCustomEditor
            // 
            this.butSelectCustomEditor.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.butSelectCustomEditor.Location = new System.Drawing.Point(393, 1);
            this.butSelectCustomEditor.Margin = new System.Windows.Forms.Padding(2, 1, 2, 2);
            this.butSelectCustomEditor.Name = "butSelectCustomEditor";
            this.butSelectCustomEditor.Size = new System.Drawing.Size(25, 22);
            this.butSelectCustomEditor.TabIndex = 2;
            this.butSelectCustomEditor.Text = "...";
            this.toolTip1.SetToolTip(this.butSelectCustomEditor, "Browse for a file");
            this.butSelectCustomEditor.UseVisualStyleBackColor = true;
            this.butSelectCustomEditor.Click += new System.EventHandler(this.butSelectCustomEditor_Click);
            // 
            // butResetValidationFile
            // 
            this.butResetValidationFile.Image = ((System.Drawing.Image)(resources.GetObject("butResetValidationFile.Image")));
            this.butResetValidationFile.Location = new System.Drawing.Point(432, 1);
            this.butResetValidationFile.Margin = new System.Windows.Forms.Padding(2, 1, 2, 2);
            this.butResetValidationFile.Name = "butResetValidationFile";
            this.butResetValidationFile.Size = new System.Drawing.Size(25, 21);
            this.butResetValidationFile.TabIndex = 3;
            this.toolTip1.SetToolTip(this.butResetValidationFile, "Revert to default");
            this.butResetValidationFile.UseVisualStyleBackColor = true;
            this.butResetValidationFile.Click += new System.EventHandler(this.butResetValidationFile_Click);
            // 
            // textboxUseValidationSchema
            // 
            this.textboxUseValidationSchema.Location = new System.Drawing.Point(55, 2);
            this.textboxUseValidationSchema.Margin = new System.Windows.Forms.Padding(2);
            this.textboxUseValidationSchema.Name = "textboxUseValidationSchema";
            this.textboxUseValidationSchema.Size = new System.Drawing.Size(344, 20);
            this.textboxUseValidationSchema.TabIndex = 1;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.AddExtension = false;
            this.openFileDialog1.DefaultExt = "xsd";
            this.openFileDialog1.Filter = "Schema files (*.xsd)|*.xsd|All files (*.*)|*.*";
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
            this.label2.Location = new System.Drawing.Point(2, 5);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 5, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Schema:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(2, 5);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 5, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Editor:";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel5, 0, 12);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel4, 0, 10);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 9);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 7);
            this.tableLayoutPanel1.Controls.Add(this.checkboxBeepOnInputError, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.checkboxStopOnCommsError, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.checkboxClearFilterOnTabChange, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.checkboxIgnoreFormSizeOnReload, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.checkboxAutoRowSize, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.checkboxNextRowOnEnter, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.checkboxUseValidationSchema, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.checkboxCustomEditor, 0, 8);
            this.tableLayoutPanel1.Controls.Add(this.checkboxShowVarProperties, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.checkboxAutostartEdit, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.CheckboxCheckForUpdates, 0, 11);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(14, 12);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 13;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(494, 316);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(100, 3);
            this.buttonCancel.Margin = new System.Windows.Forms.Padding(11, 3, 3, 3);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 1;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // buttonOK
            // 
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Location = new System.Drawing.Point(3, 3);
            this.buttonOK.Margin = new System.Windows.Forms.Padding(3, 3, 11, 3);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 0;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.AutoSize = true;
            this.tableLayoutPanel2.ColumnCount = 4;
            this.tableLayoutPanel1.SetColumnSpan(this.tableLayoutPanel2, 2);
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.Controls.Add(this.butResetValidationFile, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.butSelectValidationFile, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.textboxUseValidationSchema, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(16, 159);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(16, 0, 3, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.Size = new System.Drawing.Size(459, 24);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.AutoSize = true;
            this.tableLayoutPanel3.ColumnCount = 3;
            this.tableLayoutPanel1.SetColumnSpan(this.tableLayoutPanel3, 2);
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.Controls.Add(this.butSelectCustomEditor, 2, 0);
            this.tableLayoutPanel3.Controls.Add(this.textboxCustomEditor, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.label3, 0, 0);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(16, 206);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(16, 0, 3, 0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.Size = new System.Drawing.Size(420, 25);
            this.tableLayoutPanel3.TabIndex = 1;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.AutoSize = true;
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel1.SetColumnSpan(this.tableLayoutPanel4, 2);
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.Controls.Add(this.numericLoggerCapacity, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 231);
            this.tableLayoutPanel4.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.Size = new System.Drawing.Size(175, 26);
            this.tableLayoutPanel4.TabIndex = 1;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.tableLayoutPanel5.AutoSize = true;
            this.tableLayoutPanel5.ColumnCount = 2;
            this.tableLayoutPanel1.SetColumnSpan(this.tableLayoutPanel5, 2);
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel5.Controls.Add(this.buttonCancel, 1, 0);
            this.tableLayoutPanel5.Controls.Add(this.buttonOK, 0, 0);
            this.tableLayoutPanel5.Location = new System.Drawing.Point(158, 283);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 1;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel5.Size = new System.Drawing.Size(178, 29);
            this.tableLayoutPanel5.TabIndex = 1;
            // 
            // OptionsForm
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(514, 333);
            this.ControlBox = false;
            this.Controls.Add(this.tableLayoutPanel1);
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
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.tableLayoutPanel5.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

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
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
    }
}