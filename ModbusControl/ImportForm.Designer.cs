namespace ModbusControl
{
    partial class ImportForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImportForm));
            buttonStart = new Button();
            textboxLog = new TextBox();
            openFileDialog1 = new OpenFileDialog();
            SelectFileButton = new Button();
            textboxFilename = new TextBox();
            buttonClearLog = new Button();
            buttonCancel = new Button();
            toolTip1 = new ToolTip(components);
            panel1 = new Panel();
            chkContinueOnError = new CheckBox();
            panel2 = new Panel();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // buttonStart
            // 
            buttonStart.Location = new Point(12, 61);
            buttonStart.Name = "buttonStart";
            buttonStart.Size = new Size(150, 23);
            buttonStart.TabIndex = 3;
            buttonStart.Text = "Start";
            toolTip1.SetToolTip(buttonStart, "Start the import");
            buttonStart.UseVisualStyleBackColor = true;
            buttonStart.Click += buttonStart_Click;
            // 
            // textboxLog
            // 
            textboxLog.BorderStyle = BorderStyle.FixedSingle;
            textboxLog.Dock = DockStyle.Fill;
            textboxLog.Location = new Point(0, 0);
            textboxLog.Multiline = true;
            textboxLog.Name = "textboxLog";
            textboxLog.ReadOnly = true;
            textboxLog.ScrollBars = ScrollBars.Both;
            textboxLog.Size = new Size(544, 270);
            textboxLog.TabIndex = 0;
            textboxLog.WordWrap = false;
            // 
            // openFileDialog1
            // 
            openFileDialog1.AddExtension = false;
            openFileDialog1.CheckFileExists = false;
            openFileDialog1.DefaultExt = "csv";
            openFileDialog1.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
            // 
            // SelectFileButton
            // 
            SelectFileButton.Location = new Point(13, 11);
            SelectFileButton.Name = "SelectFileButton";
            SelectFileButton.Size = new Size(88, 23);
            SelectFileButton.TabIndex = 0;
            SelectFileButton.Text = "Select file...";
            toolTip1.SetToolTip(SelectFileButton, "Select the file to import");
            SelectFileButton.UseVisualStyleBackColor = true;
            SelectFileButton.Click += buttonSelectFile_Click;
            // 
            // textboxFilename
            // 
            textboxFilename.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textboxFilename.Location = new Point(107, 14);
            textboxFilename.Name = "textboxFilename";
            textboxFilename.Size = new Size(425, 20);
            textboxFilename.TabIndex = 1;
            textboxFilename.Text = "Aaaa";
            // 
            // buttonClearLog
            // 
            buttonClearLog.Location = new Point(249, 61);
            buttonClearLog.Name = "buttonClearLog";
            buttonClearLog.Size = new Size(75, 23);
            buttonClearLog.TabIndex = 5;
            buttonClearLog.Text = "Clear log";
            toolTip1.SetToolTip(buttonClearLog, "Clear the result log window");
            buttonClearLog.UseVisualStyleBackColor = true;
            buttonClearLog.Click += buttonClearLog_Click;
            // 
            // buttonCancel
            // 
            buttonCancel.Enabled = false;
            buttonCancel.Location = new Point(168, 61);
            buttonCancel.Name = "buttonCancel";
            buttonCancel.Size = new Size(75, 23);
            buttonCancel.TabIndex = 4;
            buttonCancel.Text = "Cancel";
            toolTip1.SetToolTip(buttonCancel, "Cancel the current operation");
            buttonCancel.UseVisualStyleBackColor = true;
            buttonCancel.Click += buttonCancel_Click;
            // 
            // panel1
            // 
            panel1.Controls.Add(chkContinueOnError);
            panel1.Controls.Add(buttonStart);
            panel1.Controls.Add(buttonCancel);
            panel1.Controls.Add(SelectFileButton);
            panel1.Controls.Add(buttonClearLog);
            panel1.Controls.Add(textboxFilename);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Margin = new Padding(0);
            panel1.Name = "panel1";
            panel1.Size = new Size(544, 92);
            panel1.TabIndex = 0;
            // 
            // chkContinueOnError
            // 
            chkContinueOnError.AutoSize = true;
            chkContinueOnError.Location = new Point(14, 41);
            chkContinueOnError.Name = "chkContinueOnError";
            chkContinueOnError.Size = new Size(107, 17);
            chkContinueOnError.TabIndex = 2;
            chkContinueOnError.Text = "Continue on error";
            chkContinueOnError.UseVisualStyleBackColor = true;
            chkContinueOnError.CheckedChanged += chkContinueOnError_CheckedChanged;
            // 
            // panel2
            // 
            panel2.Controls.Add(textboxLog);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(0, 92);
            panel2.Name = "panel2";
            panel2.Size = new Size(544, 270);
            panel2.TabIndex = 101;
            // 
            // ImportForm
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(544, 362);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "ImportForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Import registers from file";
            FormClosing += OnFormClosing;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.TextBox textboxLog;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button SelectFileButton;
        private System.Windows.Forms.TextBox textboxFilename;
        private System.Windows.Forms.Button buttonClearLog;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.CheckBox chkContinueOnError;
    }
}