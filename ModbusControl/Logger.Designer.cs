namespace LST.Helpers
{
    partial class Logger
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Logger));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.checkboxEnable = new System.Windows.Forms.CheckBox();
            this.buttonBottom = new System.Windows.Forms.Button();
            this.buttonTop = new System.Windows.Forms.Button();
            this.chkAutoScroll = new System.Windows.Forms.CheckBox();
            this.buttonSaveToFile = new System.Windows.Forms.Button();
            this.logFilter = new LST.Helpers.LogFilter();
            this.buttonClear = new System.Windows.Forms.Button();
            this.buttonCopyToClipboard = new System.Windows.Forms.Button();
            this.timerView = new System.Windows.Forms.Timer(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.SaveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.logGridView = new System.Windows.Forms.DataGridView();
            this.Time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Logtext = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.statusStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2});
            resources.ApplyResources(this.statusStrip1, "statusStrip1");
            this.statusStrip1.Name = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            resources.ApplyResources(this.toolStripStatusLabel1, "toolStripStatusLabel1");
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.BorderStyle = System.Windows.Forms.Border3DStyle.Etched;
            resources.ApplyResources(this.toolStripStatusLabel2, "toolStripStatusLabel2");
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.checkboxEnable);
            this.panel1.Controls.Add(this.buttonBottom);
            this.panel1.Controls.Add(this.buttonTop);
            this.panel1.Controls.Add(this.chkAutoScroll);
            this.panel1.Controls.Add(this.buttonSaveToFile);
            this.panel1.Controls.Add(this.logFilter);
            this.panel1.Controls.Add(this.buttonClear);
            this.panel1.Controls.Add(this.buttonCopyToClipboard);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // checkboxEnable
            // 
            resources.ApplyResources(this.checkboxEnable, "checkboxEnable");
            this.checkboxEnable.Name = "checkboxEnable";
            this.toolTip1.SetToolTip(this.checkboxEnable, resources.GetString("checkboxEnable.ToolTip"));
            this.checkboxEnable.UseVisualStyleBackColor = true;
            this.checkboxEnable.CheckedChanged += new System.EventHandler(this.checkboxEnable_CheckedChanged);
            // 
            // buttonBottom
            // 
            resources.ApplyResources(this.buttonBottom, "buttonBottom");
            this.buttonBottom.Name = "buttonBottom";
            this.toolTip1.SetToolTip(this.buttonBottom, resources.GetString("buttonBottom.ToolTip"));
            this.buttonBottom.UseVisualStyleBackColor = true;
            this.buttonBottom.Click += new System.EventHandler(this.buttonBottom_Click);
            // 
            // buttonTop
            // 
            resources.ApplyResources(this.buttonTop, "buttonTop");
            this.buttonTop.Name = "buttonTop";
            this.toolTip1.SetToolTip(this.buttonTop, resources.GetString("buttonTop.ToolTip"));
            this.buttonTop.UseVisualStyleBackColor = true;
            this.buttonTop.Click += new System.EventHandler(this.buttonTop_Click);
            // 
            // chkAutoScroll
            // 
            resources.ApplyResources(this.chkAutoScroll, "chkAutoScroll");
            this.chkAutoScroll.Checked = true;
            this.chkAutoScroll.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoScroll.Name = "chkAutoScroll";
            this.toolTip1.SetToolTip(this.chkAutoScroll, resources.GetString("chkAutoScroll.ToolTip"));
            this.chkAutoScroll.UseVisualStyleBackColor = true;
            this.chkAutoScroll.CheckedChanged += new System.EventHandler(this.chkAutoScroll_CheckedChanged);
            // 
            // buttonSaveToFile
            // 
            resources.ApplyResources(this.buttonSaveToFile, "buttonSaveToFile");
            this.buttonSaveToFile.Name = "buttonSaveToFile";
            this.toolTip1.SetToolTip(this.buttonSaveToFile, resources.GetString("buttonSaveToFile.ToolTip"));
            this.buttonSaveToFile.UseVisualStyleBackColor = true;
            this.buttonSaveToFile.Click += new System.EventHandler(this.buttonSaveToFile_Click);
            // 
            // logFilter
            // 
            this.logFilter.AnchorSize = new System.Drawing.Size(94, 21);
            this.logFilter.BackColor = System.Drawing.Color.White;
            this.logFilter.DockSide = UControls.DropDownControl.eDockSide.Left;
            resources.ApplyResources(this.logFilter, "logFilter");
            this.logFilter.LogBridgeData = false;
            this.logFilter.LogBridgeDecode = false;
            this.logFilter.LogBridgeInfo = false;
            this.logFilter.LogErrors = true;
            this.logFilter.LogInfo = true;
            this.logFilter.LogReads = false;
            this.logFilter.LogTrace = true;
            this.logFilter.LogWarnings = true;
            this.logFilter.LogWrites = false;
            this.logFilter.Name = "logFilter";
            this.logFilter.Tag = "What to log";
            this.toolTip1.SetToolTip(this.logFilter, resources.GetString("logFilter.ToolTip"));
            // 
            // buttonClear
            // 
            resources.ApplyResources(this.buttonClear, "buttonClear");
            this.buttonClear.Name = "buttonClear";
            this.toolTip1.SetToolTip(this.buttonClear, resources.GetString("buttonClear.ToolTip"));
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // buttonCopyToClipboard
            // 
            resources.ApplyResources(this.buttonCopyToClipboard, "buttonCopyToClipboard");
            this.buttonCopyToClipboard.Name = "buttonCopyToClipboard";
            this.toolTip1.SetToolTip(this.buttonCopyToClipboard, resources.GetString("buttonCopyToClipboard.ToolTip"));
            this.buttonCopyToClipboard.UseVisualStyleBackColor = true;
            this.buttonCopyToClipboard.Click += new System.EventHandler(this.buttonCopyToClipboard_Click);
            // 
            // timerView
            // 
            this.timerView.Interval = 150;
            this.timerView.Tick += new System.EventHandler(this.timerView_Tick);
            // 
            // SaveFileDialog1
            // 
            this.SaveFileDialog1.AddExtension = false;
            this.SaveFileDialog1.DefaultExt = "csv";
            resources.ApplyResources(this.SaveFileDialog1, "SaveFileDialog1");
            // 
            // logGridView
            // 
            this.logGridView.AllowUserToAddRows = false;
            this.logGridView.AllowUserToDeleteRows = false;
            this.logGridView.AllowUserToResizeRows = false;
            this.logGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.logGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Time,
            this.Type,
            this.Logtext});
            resources.ApplyResources(this.logGridView, "logGridView");
            this.logGridView.Name = "logGridView";
            this.logGridView.ReadOnly = true;
            this.logGridView.RowHeadersVisible = false;
            this.logGridView.VirtualMode = true;
            this.logGridView.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.logGridView_CellMouseDown);
            this.logGridView.CellValueNeeded += new System.Windows.Forms.DataGridViewCellValueEventHandler(this.dataGridView1_CellValueNeeded);
            // 
            // Time
            // 
            resources.ApplyResources(this.Time, "Time");
            this.Time.Name = "Time";
            this.Time.ReadOnly = true;
            // 
            // Type
            // 
            resources.ApplyResources(this.Type, "Type");
            this.Type.Name = "Type";
            this.Type.ReadOnly = true;
            // 
            // Logtext
            // 
            this.Logtext.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            resources.ApplyResources(this.Logtext, "Logtext");
            this.Logtext.Name = "Logtext";
            this.Logtext.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn1
            // 
            resources.ApplyResources(this.dataGridViewTextBoxColumn1, "dataGridViewTextBoxColumn1");
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn2
            // 
            resources.ApplyResources(this.dataGridViewTextBoxColumn2, "dataGridViewTextBoxColumn2");
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            resources.ApplyResources(this.dataGridViewTextBoxColumn3, "dataGridViewTextBoxColumn3");
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            // 
            // Logger
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.logGridView);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.statusStrip1);
            this.Name = "Logging";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Logger_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Timer timerView;
        private System.Windows.Forms.Button buttonCopyToClipboard;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.Button buttonClear;
        private LogFilter logFilter;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button buttonSaveToFile;
        private System.Windows.Forms.SaveFileDialog SaveFileDialog1;
        private System.Windows.Forms.CheckBox chkAutoScroll;
        private System.Windows.Forms.Button buttonBottom;
        private System.Windows.Forms.Button buttonTop;
        private System.Windows.Forms.CheckBox checkboxEnable;
        private System.Windows.Forms.DataGridView logGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Time;
        private System.Windows.Forms.DataGridViewTextBoxColumn Type;
        private System.Windows.Forms.DataGridViewTextBoxColumn Logtext;
    }
}

