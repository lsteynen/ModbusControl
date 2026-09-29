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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Logger));
            statusStrip1 = new StatusStrip();
            toolStripStatusLabel1 = new ToolStripStatusLabel();
            toolStripStatusLabel2 = new ToolStripStatusLabel();
            panel1 = new Panel();
            tableLayoutPanel1 = new TableLayoutPanel();
            buttonSaveToFile = new Button();
            buttonCopyToClipboard = new Button();
            buttonBottom = new Button();
            buttonTop = new Button();
            chkAutoScroll = new CheckBox();
            buttonClear = new Button();
            logFilter = new LogFilter();
            checkboxEnable = new CheckBox();
            timerView = new System.Windows.Forms.Timer(components);
            toolTip1 = new ToolTip(components);
            SaveFileDialog1 = new SaveFileDialog();
            logGridView = new DataGridView();
            Time = new DataGridViewTextBoxColumn();
            Type = new DataGridViewTextBoxColumn();
            Logtext = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn3 = new DataGridViewTextBoxColumn();
            statusStrip1.SuspendLayout();
            panel1.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)logGridView).BeginInit();
            SuspendLayout();
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel1, toolStripStatusLabel2 });
            resources.ApplyResources(statusStrip1, "statusStrip1");
            statusStrip1.Name = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            resources.ApplyResources(toolStripStatusLabel1, "toolStripStatusLabel1");
            // 
            // toolStripStatusLabel2
            // 
            toolStripStatusLabel2.BorderStyle = Border3DStyle.Etched;
            resources.ApplyResources(toolStripStatusLabel2, "toolStripStatusLabel2");
            toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            // 
            // panel1
            // 
            resources.ApplyResources(panel1, "panel1");
            panel1.Controls.Add(tableLayoutPanel1);
            panel1.Name = "panel1";
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(tableLayoutPanel1, "tableLayoutPanel1");
            tableLayoutPanel1.Controls.Add(buttonSaveToFile, 7, 0);
            tableLayoutPanel1.Controls.Add(buttonCopyToClipboard, 6, 0);
            tableLayoutPanel1.Controls.Add(buttonBottom, 5, 0);
            tableLayoutPanel1.Controls.Add(buttonTop, 4, 0);
            tableLayoutPanel1.Controls.Add(chkAutoScroll, 3, 0);
            tableLayoutPanel1.Controls.Add(buttonClear, 2, 0);
            tableLayoutPanel1.Controls.Add(logFilter, 1, 0);
            tableLayoutPanel1.Controls.Add(checkboxEnable, 0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // buttonSaveToFile
            // 
            resources.ApplyResources(buttonSaveToFile, "buttonSaveToFile");
            buttonSaveToFile.Name = "buttonSaveToFile";
            toolTip1.SetToolTip(buttonSaveToFile, resources.GetString("buttonSaveToFile.ToolTip"));
            buttonSaveToFile.UseVisualStyleBackColor = true;
            buttonSaveToFile.Click += buttonSaveToFile_Click;
            // 
            // buttonCopyToClipboard
            // 
            resources.ApplyResources(buttonCopyToClipboard, "buttonCopyToClipboard");
            buttonCopyToClipboard.Name = "buttonCopyToClipboard";
            toolTip1.SetToolTip(buttonCopyToClipboard, resources.GetString("buttonCopyToClipboard.ToolTip"));
            buttonCopyToClipboard.UseVisualStyleBackColor = true;
            buttonCopyToClipboard.Click += buttonCopyToClipboard_Click;
            // 
            // buttonBottom
            // 
            resources.ApplyResources(buttonBottom, "buttonBottom");
            buttonBottom.Name = "buttonBottom";
            toolTip1.SetToolTip(buttonBottom, resources.GetString("buttonBottom.ToolTip"));
            buttonBottom.UseVisualStyleBackColor = true;
            buttonBottom.Click += buttonBottom_Click;
            // 
            // buttonTop
            // 
            resources.ApplyResources(buttonTop, "buttonTop");
            buttonTop.Name = "buttonTop";
            toolTip1.SetToolTip(buttonTop, resources.GetString("buttonTop.ToolTip"));
            buttonTop.UseVisualStyleBackColor = true;
            buttonTop.Click += buttonTop_Click;
            // 
            // chkAutoScroll
            // 
            resources.ApplyResources(chkAutoScroll, "chkAutoScroll");
            chkAutoScroll.Checked = true;
            chkAutoScroll.CheckState = CheckState.Checked;
            chkAutoScroll.Name = "chkAutoScroll";
            toolTip1.SetToolTip(chkAutoScroll, resources.GetString("chkAutoScroll.ToolTip"));
            chkAutoScroll.UseVisualStyleBackColor = true;
            chkAutoScroll.CheckedChanged += chkAutoScroll_CheckedChanged;
            // 
            // buttonClear
            // 
            resources.ApplyResources(buttonClear, "buttonClear");
            buttonClear.Name = "buttonClear";
            toolTip1.SetToolTip(buttonClear, resources.GetString("buttonClear.ToolTip"));
            buttonClear.UseVisualStyleBackColor = true;
            buttonClear.Click += buttonClear_Click;
            // 
            // logFilter
            // 
            logFilter.AnchorSize = new Size(122, 21);
            logFilter.BackColor = Color.White;
            logFilter.DockSide = LST.Controls.DropDownControl.eDockSide.Left;
            resources.ApplyResources(logFilter, "logFilter");
            logFilter.LogBridgeData = false;
            logFilter.LogBridgeDecode = false;
            logFilter.LogBridgeInfo = false;
            logFilter.LogErrors = true;
            logFilter.LogInfo = true;
            logFilter.LogReads = false;
            logFilter.LogTrace = true;
            logFilter.LogWarnings = true;
            logFilter.LogWrites = false;
            logFilter.Name = "logFilter";
            logFilter.Tag = "What to log";
            // 
            // checkboxEnable
            // 
            resources.ApplyResources(checkboxEnable, "checkboxEnable");
            checkboxEnable.Name = "checkboxEnable";
            toolTip1.SetToolTip(checkboxEnable, resources.GetString("checkboxEnable.ToolTip"));
            checkboxEnable.UseVisualStyleBackColor = true;
            checkboxEnable.CheckedChanged += checkboxEnable_CheckedChanged;
            // 
            // timerView
            // 
            timerView.Interval = 150;
            timerView.Tick += timerView_Tick;
            // 
            // SaveFileDialog1
            // 
            SaveFileDialog1.AddExtension = false;
            SaveFileDialog1.DefaultExt = "csv";
            resources.ApplyResources(SaveFileDialog1, "SaveFileDialog1");
            // 
            // logGridView
            // 
            logGridView.AllowUserToAddRows = false;
            logGridView.AllowUserToDeleteRows = false;
            logGridView.AllowUserToResizeRows = false;
            logGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            logGridView.Columns.AddRange(new DataGridViewColumn[] { Time, Type, Logtext });
            resources.ApplyResources(logGridView, "logGridView");
            logGridView.Name = "logGridView";
            logGridView.ReadOnly = true;
            logGridView.RowHeadersVisible = false;
            logGridView.VirtualMode = true;
            logGridView.CellMouseDown += logGridView_CellMouseDown;
            logGridView.CellValueNeeded += dataGridView1_CellValueNeeded;
            // 
            // Time
            // 
            resources.ApplyResources(Time, "Time");
            Time.Name = "Time";
            Time.ReadOnly = true;
            // 
            // Type
            // 
            resources.ApplyResources(Type, "Type");
            Type.Name = "Type";
            Type.ReadOnly = true;
            // 
            // Logtext
            // 
            Logtext.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            resources.ApplyResources(Logtext, "Logtext");
            Logtext.Name = "Logtext";
            Logtext.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn1
            // 
            resources.ApplyResources(dataGridViewTextBoxColumn1, "dataGridViewTextBoxColumn1");
            dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn2
            // 
            resources.ApplyResources(dataGridViewTextBoxColumn2, "dataGridViewTextBoxColumn2");
            dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn3
            // 
            dataGridViewTextBoxColumn3.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            resources.ApplyResources(dataGridViewTextBoxColumn3, "dataGridViewTextBoxColumn3");
            dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            dataGridViewTextBoxColumn3.ReadOnly = true;
            // 
            // Logger
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Dpi;
            Controls.Add(logGridView);
            Controls.Add(panel1);
            Controls.Add(statusStrip1);
            Name = "Logger";
            FormClosing += Logger_FormClosing;
            Load += Form1_Load;
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)logGridView).EndInit();
            ResumeLayout(false);
            PerformLayout();

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
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}

