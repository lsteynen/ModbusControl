namespace ModbusControl
{
    partial class CustomQueriesForm
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
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CustomQueriesForm));
            toolTip1 = new ToolTip(components);
            panel1 = new Panel();
            flowLayoutPanel1 = new FlowLayoutPanel();
            MoveUpBtn = new Button();
            MoveDownBtn = new Button();
            AddRowBtn = new Button();
            DeleteRowsBtn = new Button();
            MultilineRowsChkb = new CheckBox();
            SendAllBtn = new Button();
            ClearResultsBtn = new Button();
            RunTestsBtn = new Button();
            DebugBtn = new Button();
            Grid = new LST.Controls.MyDataGridView();
            SendColumn = new DataGridViewButtonColumn();
            SlaveidColumn = new DataGridViewTextBoxColumn();
            CommandColumn = new DataGridViewComboBoxColumn();
            RegisterColumn = new DataGridViewTextBoxColumn();
            QtyColumn = new DataGridViewTextBoxColumn();
            FormatColumn = new DataGridViewComboBoxColumn();
            DataColumn = new DataGridViewTextBoxColumn();
            ResultColumn = new DataGridViewTextBoxColumn();
            CommentColumn = new DataGridViewTextBoxColumn();
            CountColumn = new DataGridViewTextBoxColumn();
            panel1.SuspendLayout();
            flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)Grid).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(flowLayoutPanel1);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1003, 37);
            panel1.TabIndex = 0;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.AutoSize = true;
            flowLayoutPanel1.Controls.Add(MoveUpBtn);
            flowLayoutPanel1.Controls.Add(MoveDownBtn);
            flowLayoutPanel1.Controls.Add(AddRowBtn);
            flowLayoutPanel1.Controls.Add(DeleteRowsBtn);
            flowLayoutPanel1.Controls.Add(MultilineRowsChkb);
            flowLayoutPanel1.Controls.Add(SendAllBtn);
            flowLayoutPanel1.Controls.Add(ClearResultsBtn);
            flowLayoutPanel1.Controls.Add(RunTestsBtn);
            flowLayoutPanel1.Controls.Add(DebugBtn);
            flowLayoutPanel1.Location = new Point(3, 3);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(927, 31);
            flowLayoutPanel1.TabIndex = 9;
            flowLayoutPanel1.WrapContents = false;
            // 
            // MoveUpBtn
            // 
            MoveUpBtn.Location = new Point(3, 3);
            MoveUpBtn.Margin = new Padding(3, 3, 6, 3);
            MoveUpBtn.Name = "MoveUpBtn";
            MoveUpBtn.Size = new Size(82, 23);
            MoveUpBtn.TabIndex = 0;
            MoveUpBtn.Text = "Move up";
            MoveUpBtn.UseVisualStyleBackColor = true;
            MoveUpBtn.Click += MoveUpBtn_Click;
            // 
            // MoveDownBtn
            // 
            MoveDownBtn.Location = new Point(94, 3);
            MoveDownBtn.Margin = new Padding(3, 3, 6, 3);
            MoveDownBtn.Name = "MoveDownBtn";
            MoveDownBtn.Size = new Size(82, 23);
            MoveDownBtn.TabIndex = 1;
            MoveDownBtn.Text = "Move down";
            MoveDownBtn.UseVisualStyleBackColor = true;
            MoveDownBtn.Click += MoveDownBtn_Click;
            // 
            // AddRowBtn
            // 
            AddRowBtn.Location = new Point(185, 3);
            AddRowBtn.Margin = new Padding(3, 3, 6, 3);
            AddRowBtn.Name = "AddRowBtn";
            AddRowBtn.Size = new Size(78, 23);
            AddRowBtn.TabIndex = 2;
            AddRowBtn.Text = "Add row";
            AddRowBtn.UseVisualStyleBackColor = true;
            AddRowBtn.Click += AddRowBtn_Click;
            // 
            // DeleteRowsBtn
            // 
            DeleteRowsBtn.Location = new Point(272, 3);
            DeleteRowsBtn.Margin = new Padding(3, 3, 6, 3);
            DeleteRowsBtn.Name = "DeleteRowsBtn";
            DeleteRowsBtn.Size = new Size(78, 23);
            DeleteRowsBtn.TabIndex = 3;
            DeleteRowsBtn.Text = "Delete row";
            DeleteRowsBtn.UseVisualStyleBackColor = true;
            DeleteRowsBtn.Click += DeleteRowsBtn_Click;
            // 
            // MultilineRowsChkb
            // 
            MultilineRowsChkb.AutoSize = true;
            MultilineRowsChkb.Location = new Point(359, 7);
            MultilineRowsChkb.Margin = new Padding(3, 7, 6, 3);
            MultilineRowsChkb.Name = "MultilineRowsChkb";
            MultilineRowsChkb.Size = new Size(176, 17);
            MultilineRowsChkb.TabIndex = 4;
            MultilineRowsChkb.Text = "Multiline rows with auto row size";
            MultilineRowsChkb.UseVisualStyleBackColor = true;
            MultilineRowsChkb.CheckedChanged += MultilineRowsChkb_CheckedChanged;
            // 
            // SendAllBtn
            // 
            SendAllBtn.Location = new Point(544, 3);
            SendAllBtn.Margin = new Padding(3, 3, 6, 3);
            SendAllBtn.Name = "SendAllBtn";
            SendAllBtn.Size = new Size(75, 23);
            SendAllBtn.TabIndex = 5;
            SendAllBtn.Text = "Send all";
            SendAllBtn.UseVisualStyleBackColor = true;
            SendAllBtn.Click += SendAllBtn_Click;
            // 
            // ClearResultsBtn
            // 
            ClearResultsBtn.Location = new Point(628, 3);
            ClearResultsBtn.Margin = new Padding(3, 3, 6, 3);
            ClearResultsBtn.Name = "ClearResultsBtn";
            ClearResultsBtn.Size = new Size(98, 23);
            ClearResultsBtn.TabIndex = 6;
            ClearResultsBtn.Text = "Clear all results";
            ClearResultsBtn.UseVisualStyleBackColor = true;
            ClearResultsBtn.Click += ClearResultsBtn_Click;
            // 
            // RunTestsBtn
            // 
            RunTestsBtn.Location = new Point(735, 3);
            RunTestsBtn.Margin = new Padding(3, 3, 6, 3);
            RunTestsBtn.Name = "RunTestsBtn";
            RunTestsBtn.Size = new Size(75, 23);
            RunTestsBtn.TabIndex = 7;
            RunTestsBtn.Text = "Run tests";
            RunTestsBtn.UseVisualStyleBackColor = true;
            RunTestsBtn.Visible = false;
            RunTestsBtn.Click += RunTestsBtn_Click;
            // 
            // DebugBtn
            // 
            DebugBtn.Location = new Point(819, 3);
            DebugBtn.Name = "DebugBtn";
            DebugBtn.Size = new Size(82, 23);
            DebugBtn.TabIndex = 8;
            DebugBtn.Text = "DEBUG info";
            DebugBtn.UseVisualStyleBackColor = true;
            DebugBtn.Visible = false;
            DebugBtn.Click += DebugBtn_Click;
            // 
            // Grid
            // 
            Grid.AllowUserToAddRows = false;
            Grid.AllowUserToDeleteRows = false;
            Grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            Grid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
            Grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            Grid.Columns.AddRange(new DataGridViewColumn[] { SendColumn, SlaveidColumn, CommandColumn, RegisterColumn, QtyColumn, FormatColumn, DataColumn, ResultColumn, CommentColumn, CountColumn });
            Grid.Dock = DockStyle.Fill;
            Grid.Location = new Point(0, 37);
            Grid.Margin = new Padding(2);
            Grid.Name = "Grid";
            Grid.RowHeadersVisible = false;
            Grid.RowTemplate.Height = 23;
            Grid.Size = new Size(1003, 555);
            Grid.TabIndex = 1;
            Grid.CellContentClick += MyDataGridView1_CellContentClick;
            Grid.EditingControlShowing += Grid_EditingControlShowing;
            // 
            // SendColumn
            // 
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            SendColumn.DefaultCellStyle = dataGridViewCellStyle2;
            SendColumn.FillWeight = 50F;
            SendColumn.HeaderText = "Send";
            SendColumn.Name = "SendColumn";
            SendColumn.Text = "Send";
            SendColumn.UseColumnTextForButtonValue = true;
            // 
            // SlaveidColumn
            // 
            SlaveidColumn.FillWeight = 20F;
            SlaveidColumn.HeaderText = "SlaveID";
            SlaveidColumn.Name = "SlaveidColumn";
            // 
            // CommandColumn
            // 
            CommandColumn.AutoComplete = false;
            CommandColumn.DropDownWidth = 220;
            CommandColumn.HeaderText = "Command";
            CommandColumn.Items.AddRange(new object[] { "R input registers (FC 04)", "R holding registers (FC 03)", "W single holding register (FC 06)", "W multiple holding registers (FC 10)", "R discrete inputs (FC 02)", "R coils (FC 01)", "W single coil (FC 05)", "W multiple coils (FC 0F)", "Custom query" });
            CommandColumn.Name = "CommandColumn";
            CommandColumn.SortMode = DataGridViewColumnSortMode.Automatic;
            // 
            // RegisterColumn
            // 
            RegisterColumn.FillWeight = 50F;
            RegisterColumn.HeaderText = "Register";
            RegisterColumn.Name = "RegisterColumn";
            // 
            // QtyColumn
            // 
            QtyColumn.FillWeight = 50F;
            QtyColumn.HeaderText = "Qty";
            QtyColumn.Name = "QtyColumn";
            // 
            // FormatColumn
            // 
            FormatColumn.AutoComplete = false;
            FormatColumn.FillWeight = 50F;
            FormatColumn.HeaderText = "Format";
            FormatColumn.Items.AddRange(new object[] { "Int16", "UInt16", "Hex16", "Int32", "UInt32", "Hex32", "HexBytes", "Bits" });
            FormatColumn.Name = "FormatColumn";
            FormatColumn.SortMode = DataGridViewColumnSortMode.Automatic;
            // 
            // DataColumn
            // 
            DataColumn.HeaderText = "Data";
            DataColumn.Name = "DataColumn";
            // 
            // ResultColumn
            // 
            ResultColumn.FillWeight = 300F;
            ResultColumn.HeaderText = "Result";
            ResultColumn.Name = "ResultColumn";
            ResultColumn.ReadOnly = true;
            // 
            // CommentColumn
            // 
            CommentColumn.HeaderText = "Comment";
            CommentColumn.Name = "CommentColumn";
            // 
            // CountColumn
            // 
            CountColumn.FillWeight = 40F;
            CountColumn.HeaderText = "Count";
            CountColumn.Name = "CountColumn";
            CountColumn.ReadOnly = true;
            CountColumn.Visible = false;
            // 
            // CustomQueriesForm
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(1003, 592);
            Controls.Add(Grid);
            Controls.Add(panel1);
            Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Icon = (Icon)resources.GetObject("$this.Icon");
            KeyPreview = true;
            Name = "CustomQueriesForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Custom modbus queries";
            FormClosing += CustomQueries_FormClosing;
            KeyDown += CustomQueriesForm_KeyDown;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            flowLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)Grid).EndInit();
            ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ToolTip toolTip1;
        private LST.Controls.MyDataGridView Grid;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox MultilineRowsChkb;
        private System.Windows.Forms.Button MoveUpBtn;
        private System.Windows.Forms.Button MoveDownBtn;
        private System.Windows.Forms.Button RunTestsBtn;
        private System.Windows.Forms.Button SendAllBtn;
        private System.Windows.Forms.Button ClearResultsBtn;
        private System.Windows.Forms.Button AddRowBtn;
        private System.Windows.Forms.Button DeleteRowsBtn;
        private Button DebugBtn;
        private DataGridViewButtonColumn SendColumn;
        private DataGridViewTextBoxColumn SlaveidColumn;
        private DataGridViewComboBoxColumn CommandColumn;
        private DataGridViewTextBoxColumn RegisterColumn;
        private DataGridViewTextBoxColumn QtyColumn;
        private DataGridViewComboBoxColumn FormatColumn;
        private DataGridViewTextBoxColumn DataColumn;
        private DataGridViewTextBoxColumn ResultColumn;
        private DataGridViewTextBoxColumn CommentColumn;
        private DataGridViewTextBoxColumn CountColumn;
        private FlowLayoutPanel flowLayoutPanel1;
    }
}