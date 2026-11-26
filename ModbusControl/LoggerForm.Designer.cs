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
            toolStripStatusLabel3 = new ToolStripStatusLabel();
            panel1 = new Panel();
            flowLayoutPanel1 = new FlowLayoutPanel();
            EnableCheckbox = new CheckBox();
            LogFilter = new LogFilter();
            ClearBtn = new Button();
            AutoScrollCheckbox = new CheckBox();
            TopBtn = new Button();
            BottomBtn = new Button();
            CopyToClipboardBtn = new Button();
            SaveToFileBtn = new Button();
            ViewTimer = new System.Windows.Forms.Timer(components);
            toolTip1 = new ToolTip(components);
            SaveFileDialog1 = new SaveFileDialog();
            LogGrid = new DataGridView();
            Time = new DataGridViewTextBoxColumn();
            Type = new DataGridViewTextBoxColumn();
            Logtext = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn3 = new DataGridViewTextBoxColumn();
            statusStrip1.SuspendLayout();
            panel1.SuspendLayout();
            flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)LogGrid).BeginInit();
            SuspendLayout();
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel1, toolStripStatusLabel2, toolStripStatusLabel3 });
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
            // toolStripStatusLabel3
            // 
            toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            resources.ApplyResources(toolStripStatusLabel3, "toolStripStatusLabel3");
            // 
            // panel1
            // 
            panel1.Controls.Add(flowLayoutPanel1);
            resources.ApplyResources(panel1, "panel1");
            panel1.Name = "panel1";
            // 
            // flowLayoutPanel1
            // 
            resources.ApplyResources(flowLayoutPanel1, "flowLayoutPanel1");
            flowLayoutPanel1.Controls.Add(EnableCheckbox);
            flowLayoutPanel1.Controls.Add(LogFilter);
            flowLayoutPanel1.Controls.Add(ClearBtn);
            flowLayoutPanel1.Controls.Add(AutoScrollCheckbox);
            flowLayoutPanel1.Controls.Add(TopBtn);
            flowLayoutPanel1.Controls.Add(BottomBtn);
            flowLayoutPanel1.Controls.Add(CopyToClipboardBtn);
            flowLayoutPanel1.Controls.Add(SaveToFileBtn);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            // 
            // EnableCheckbox
            // 
            resources.ApplyResources(EnableCheckbox, "EnableCheckbox");
            EnableCheckbox.Name = "EnableCheckbox";
            toolTip1.SetToolTip(EnableCheckbox, resources.GetString("EnableCheckbox.ToolTip"));
            EnableCheckbox.UseVisualStyleBackColor = true;
            EnableCheckbox.CheckedChanged += EnableCheckbox_CheckedChanged;
            // 
            // LogFilter
            // 
            LogFilter.AnchorSize = new Size(122, 21);
            LogFilter.BackColor = Color.White;
            LogFilter.DockSide = LST.Controls.DropDownControl.eDockSide.Left;
            resources.ApplyResources(LogFilter, "LogFilter");
            LogFilter.LogBridgeData = false;
            LogFilter.LogBridgeDecode = false;
            LogFilter.LogBridgeInfo = false;
            LogFilter.LogErrors = true;
            LogFilter.LogInfo = true;
            LogFilter.LogReads = false;
            LogFilter.LogTrace = true;
            LogFilter.LogWarnings = true;
            LogFilter.LogWrites = false;
            LogFilter.Name = "LogFilter";
            LogFilter.Tag = "What to log";
            // 
            // ClearBtn
            // 
            resources.ApplyResources(ClearBtn, "ClearBtn");
            ClearBtn.Name = "ClearBtn";
            toolTip1.SetToolTip(ClearBtn, resources.GetString("ClearBtn.ToolTip"));
            ClearBtn.UseVisualStyleBackColor = true;
            ClearBtn.Click += ClearBtn_Click;
            // 
            // AutoScrollCheckbox
            // 
            resources.ApplyResources(AutoScrollCheckbox, "AutoScrollCheckbox");
            AutoScrollCheckbox.Checked = true;
            AutoScrollCheckbox.CheckState = CheckState.Checked;
            AutoScrollCheckbox.Name = "AutoScrollCheckbox";
            toolTip1.SetToolTip(AutoScrollCheckbox, resources.GetString("AutoScrollCheckbox.ToolTip"));
            AutoScrollCheckbox.UseVisualStyleBackColor = true;
            AutoScrollCheckbox.CheckedChanged += AutoScrollCheckbox_CheckedChanged;
            // 
            // TopBtn
            // 
            resources.ApplyResources(TopBtn, "TopBtn");
            TopBtn.Name = "TopBtn";
            toolTip1.SetToolTip(TopBtn, resources.GetString("TopBtn.ToolTip"));
            TopBtn.UseVisualStyleBackColor = true;
            TopBtn.Click += buttonTop_Click;
            // 
            // BottomBtn
            // 
            resources.ApplyResources(BottomBtn, "BottomBtn");
            BottomBtn.Name = "BottomBtn";
            toolTip1.SetToolTip(BottomBtn, resources.GetString("BottomBtn.ToolTip"));
            BottomBtn.UseVisualStyleBackColor = true;
            BottomBtn.Click += BottomBtn_Click;
            // 
            // CopyToClipboardBtn
            // 
            resources.ApplyResources(CopyToClipboardBtn, "CopyToClipboardBtn");
            CopyToClipboardBtn.Name = "CopyToClipboardBtn";
            toolTip1.SetToolTip(CopyToClipboardBtn, resources.GetString("CopyToClipboardBtn.ToolTip"));
            CopyToClipboardBtn.UseVisualStyleBackColor = true;
            CopyToClipboardBtn.Click += CopyToClipboardBtn_Click;
            // 
            // SaveToFileBtn
            // 
            resources.ApplyResources(SaveToFileBtn, "SaveToFileBtn");
            SaveToFileBtn.Name = "SaveToFileBtn";
            toolTip1.SetToolTip(SaveToFileBtn, resources.GetString("SaveToFileBtn.ToolTip"));
            SaveToFileBtn.UseVisualStyleBackColor = true;
            SaveToFileBtn.Click += SaveToFileBtn_Click;
            // 
            // ViewTimer
            // 
            ViewTimer.Interval = 150;
            ViewTimer.Tick += ViewTimer_Tick;
            // 
            // SaveFileDialog1
            // 
            SaveFileDialog1.AddExtension = false;
            SaveFileDialog1.DefaultExt = "csv";
            resources.ApplyResources(SaveFileDialog1, "SaveFileDialog1");
            // 
            // LogGrid
            // 
            LogGrid.AllowUserToAddRows = false;
            LogGrid.AllowUserToDeleteRows = false;
            LogGrid.AllowUserToResizeRows = false;
            LogGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            LogGrid.Columns.AddRange(new DataGridViewColumn[] { Time, Type, Logtext });
            resources.ApplyResources(LogGrid, "LogGrid");
            LogGrid.Name = "LogGrid";
            LogGrid.ReadOnly = true;
            LogGrid.RowHeadersVisible = false;
            LogGrid.VirtualMode = true;
            LogGrid.CellMouseDown += LogGrid_CellMouseDown;
            LogGrid.CellValueNeeded += dataGridView1_CellValueNeeded;
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
            Controls.Add(LogGrid);
            Controls.Add(panel1);
            Controls.Add(statusStrip1);
            Name = "Logger";
            FormClosing += Logger_FormClosing;
            Load += Form1_Load;
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            flowLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)LogGrid).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Timer ViewTimer;
        private System.Windows.Forms.Button CopyToClipboardBtn;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.Button ClearBtn;
        private LogFilter LogFilter;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button SaveToFileBtn;
        private System.Windows.Forms.SaveFileDialog SaveFileDialog1;
        private System.Windows.Forms.CheckBox AutoScrollCheckbox;
        private System.Windows.Forms.Button BottomBtn;
        private System.Windows.Forms.Button TopBtn;
        private System.Windows.Forms.CheckBox EnableCheckbox;
        private System.Windows.Forms.DataGridView LogGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Time;
        private System.Windows.Forms.DataGridViewTextBoxColumn Type;
        private System.Windows.Forms.DataGridViewTextBoxColumn Logtext;
        private ToolStripStatusLabel toolStripStatusLabel3;
        private FlowLayoutPanel flowLayoutPanel1;
    }
}

