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
            this.buttonSaveToFile = new System.Windows.Forms.Button();
            this.buttonStop = new System.Windows.Forms.Button();
            this.logFilter = new LST.Helpers.LogFilter();
            this.Loggrid = new SourceGrid.DataGrid();
            this.buttonClear = new System.Windows.Forms.Button();
            this.buttonCopyToClipboard = new System.Windows.Forms.Button();
            this.buttonRecord = new System.Windows.Forms.Button();
            this.buttonLast = new System.Windows.Forms.Button();
            this.buttonFirst = new System.Windows.Forms.Button();
            this.timerView = new System.Windows.Forms.Timer(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.SaveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.statusStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
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
            this.panel1.Controls.Add(this.buttonSaveToFile);
            this.panel1.Controls.Add(this.buttonStop);
            this.panel1.Controls.Add(this.logFilter);
            this.panel1.Controls.Add(this.Loggrid);
            this.panel1.Controls.Add(this.buttonClear);
            this.panel1.Controls.Add(this.buttonCopyToClipboard);
            this.panel1.Controls.Add(this.buttonRecord);
            this.panel1.Controls.Add(this.buttonLast);
            this.panel1.Controls.Add(this.buttonFirst);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // buttonSaveToFile
            // 
            resources.ApplyResources(this.buttonSaveToFile, "buttonSaveToFile");
            this.buttonSaveToFile.Name = "buttonSaveToFile";
            this.toolTip1.SetToolTip(this.buttonSaveToFile, resources.GetString("buttonSaveToFile.ToolTip"));
            this.buttonSaveToFile.UseVisualStyleBackColor = true;
            this.buttonSaveToFile.Click += new System.EventHandler(this.buttonSaveToFile_Click);
            // 
            // buttonStop
            // 
            resources.ApplyResources(this.buttonStop, "buttonStop");
            this.buttonStop.Name = "buttonStop";
            this.toolTip1.SetToolTip(this.buttonStop, resources.GetString("buttonStop.ToolTip"));
            this.buttonStop.UseVisualStyleBackColor = true;
            this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
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
            // Loggrid
            // 
            resources.ApplyResources(this.Loggrid, "Loggrid");
            this.Loggrid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Loggrid.DeleteQuestionMessage = "Are you sure to delete all the selected rows?";
            this.Loggrid.EnableSort = false;
            this.Loggrid.FixedRows = 1;
            this.Loggrid.Name = "Loggrid";
            this.Loggrid.SelectionMode = SourceGrid.GridSelectionMode.Row;
            this.Loggrid.TabStop = true;
            this.Loggrid.ToolTipText = "";
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
            // buttonRecord
            // 
            resources.ApplyResources(this.buttonRecord, "buttonRecord");
            this.buttonRecord.Name = "buttonRecord";
            this.toolTip1.SetToolTip(this.buttonRecord, resources.GetString("buttonRecord.ToolTip"));
            this.buttonRecord.UseVisualStyleBackColor = true;
            this.buttonRecord.Click += new System.EventHandler(this.buttonRecord_Click);
            // 
            // buttonLast
            // 
            resources.ApplyResources(this.buttonLast, "buttonLast");
            this.buttonLast.Name = "buttonLast";
            this.toolTip1.SetToolTip(this.buttonLast, resources.GetString("buttonLast.ToolTip"));
            this.buttonLast.UseVisualStyleBackColor = true;
            this.buttonLast.Click += new System.EventHandler(this.buttonLast_Click);
            // 
            // buttonFirst
            // 
            resources.ApplyResources(this.buttonFirst, "buttonFirst");
            this.buttonFirst.Name = "buttonFirst";
            this.toolTip1.SetToolTip(this.buttonFirst, resources.GetString("buttonFirst.ToolTip"));
            this.buttonFirst.UseVisualStyleBackColor = true;
            this.buttonFirst.Click += new System.EventHandler(this.buttonFirst_Click);
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
            // Logger
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.statusStrip1);
            this.Name = "Logger";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Logger_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button buttonFirst;
        private System.Windows.Forms.Timer timerView;
        private System.Windows.Forms.Button buttonRecord;
        private System.Windows.Forms.Button buttonLast;
        private System.Windows.Forms.Button buttonCopyToClipboard;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.Button buttonClear;
        private SourceGrid.DataGrid Loggrid;
        private LogFilter logFilter;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button buttonStop;
        private System.Windows.Forms.Button buttonSaveToFile;
        private System.Windows.Forms.SaveFileDialog SaveFileDialog1;
    }
}

