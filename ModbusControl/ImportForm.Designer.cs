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
            StartBtn = new Button();
            LogTextbox = new TextBox();
            openFileDialog1 = new OpenFileDialog();
            SelectFileBtn = new Button();
            FilenameTextbox = new TextBox();
            ClearLogBtn = new Button();
            CancelBtn = new Button();
            toolTip1 = new ToolTip(components);
            panel1 = new Panel();
            ContinueOnErrorCheckbox = new CheckBox();
            panel2 = new Panel();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // StartBtn
            // 
            StartBtn.Location = new Point(12, 61);
            StartBtn.Name = "StartBtn";
            StartBtn.Size = new Size(150, 23);
            StartBtn.TabIndex = 3;
            StartBtn.Text = "Start";
            toolTip1.SetToolTip(StartBtn, "Start the import");
            StartBtn.UseVisualStyleBackColor = true;
            StartBtn.Click += StartBtn_Click;
            // 
            // LogTextbox
            // 
            LogTextbox.BorderStyle = BorderStyle.FixedSingle;
            LogTextbox.Dock = DockStyle.Fill;
            LogTextbox.Location = new Point(0, 0);
            LogTextbox.Multiline = true;
            LogTextbox.Name = "LogTextbox";
            LogTextbox.ReadOnly = true;
            LogTextbox.ScrollBars = ScrollBars.Both;
            LogTextbox.Size = new Size(544, 270);
            LogTextbox.TabIndex = 0;
            LogTextbox.WordWrap = false;
            // 
            // openFileDialog1
            // 
            openFileDialog1.AddExtension = false;
            openFileDialog1.CheckFileExists = false;
            openFileDialog1.DefaultExt = "csv";
            openFileDialog1.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
            // 
            // SelectFileBtn
            // 
            SelectFileBtn.Location = new Point(13, 11);
            SelectFileBtn.Name = "SelectFileBtn";
            SelectFileBtn.Size = new Size(88, 23);
            SelectFileBtn.TabIndex = 0;
            SelectFileBtn.Text = "Select file...";
            toolTip1.SetToolTip(SelectFileBtn, "Select the file to import");
            SelectFileBtn.UseVisualStyleBackColor = true;
            SelectFileBtn.Click += SelectFileBtn_Click;
            // 
            // FilenameTextbox
            // 
            FilenameTextbox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            FilenameTextbox.Location = new Point(107, 14);
            FilenameTextbox.Name = "FilenameTextbox";
            FilenameTextbox.Size = new Size(425, 20);
            FilenameTextbox.TabIndex = 1;
            FilenameTextbox.Text = "Aaaa";
            // 
            // ClearLogBtn
            // 
            ClearLogBtn.Location = new Point(249, 61);
            ClearLogBtn.Name = "ClearLogBtn";
            ClearLogBtn.Size = new Size(75, 23);
            ClearLogBtn.TabIndex = 5;
            ClearLogBtn.Text = "Clear log";
            toolTip1.SetToolTip(ClearLogBtn, "Clear the result log window");
            ClearLogBtn.UseVisualStyleBackColor = true;
            ClearLogBtn.Click += ClearLogBtn_Click;
            // 
            // CancelBtn
            // 
            CancelBtn.Enabled = false;
            CancelBtn.Location = new Point(168, 61);
            CancelBtn.Name = "CancelBtn";
            CancelBtn.Size = new Size(75, 23);
            CancelBtn.TabIndex = 4;
            CancelBtn.Text = "Cancel";
            toolTip1.SetToolTip(CancelBtn, "Cancel the current operation");
            CancelBtn.UseVisualStyleBackColor = true;
            CancelBtn.Click += CancelBtn_Click;
            // 
            // panel1
            // 
            panel1.Controls.Add(ContinueOnErrorCheckbox);
            panel1.Controls.Add(StartBtn);
            panel1.Controls.Add(CancelBtn);
            panel1.Controls.Add(SelectFileBtn);
            panel1.Controls.Add(ClearLogBtn);
            panel1.Controls.Add(FilenameTextbox);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Margin = new Padding(0);
            panel1.Name = "panel1";
            panel1.Size = new Size(544, 92);
            panel1.TabIndex = 0;
            // 
            // ContinueOnErrorCheckbox
            // 
            ContinueOnErrorCheckbox.AutoSize = true;
            ContinueOnErrorCheckbox.Location = new Point(14, 41);
            ContinueOnErrorCheckbox.Name = "ContinueOnErrorCheckbox";
            ContinueOnErrorCheckbox.Size = new Size(107, 17);
            ContinueOnErrorCheckbox.TabIndex = 2;
            ContinueOnErrorCheckbox.Text = "Continue on error";
            ContinueOnErrorCheckbox.UseVisualStyleBackColor = true;
            ContinueOnErrorCheckbox.CheckedChanged += ContinueOnErrorCheckbox_CheckedChanged;
            // 
            // panel2
            // 
            panel2.Controls.Add(LogTextbox);
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

        private System.Windows.Forms.Button StartBtn;
        private System.Windows.Forms.TextBox LogTextbox;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button SelectFileBtn;
        private System.Windows.Forms.TextBox FilenameTextbox;
        private System.Windows.Forms.Button ClearLogBtn;
        private System.Windows.Forms.Button CancelBtn;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.CheckBox ContinueOnErrorCheckbox;
    }
}