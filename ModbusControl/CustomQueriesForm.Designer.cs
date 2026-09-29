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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CustomQueriesForm));
            toolTip1 = new ToolTip(components);
            panel1 = new Panel();
            tableLayoutPanel1 = new TableLayoutPanel();
            butDebug = new Button();
            butMoveUp = new Button();
            butRunTests = new Button();
            butClearResults = new Button();
            butDeleteRows = new Button();
            butSendAll = new Button();
            butMoveDown = new Button();
            butAddRow = new Button();
            chkMultilineRows = new CheckBox();
            grid = new LST.Controls.MyDataGridView();
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
            tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)grid).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(tableLayoutPanel1);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1003, 38);
            panel1.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 9;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.Controls.Add(butDebug, 8, 0);
            tableLayoutPanel1.Controls.Add(butMoveUp, 0, 0);
            tableLayoutPanel1.Controls.Add(butRunTests, 7, 0);
            tableLayoutPanel1.Controls.Add(butClearResults, 6, 0);
            tableLayoutPanel1.Controls.Add(butDeleteRows, 3, 0);
            tableLayoutPanel1.Controls.Add(butSendAll, 5, 0);
            tableLayoutPanel1.Controls.Add(butMoveDown, 1, 0);
            tableLayoutPanel1.Controls.Add(butAddRow, 2, 0);
            tableLayoutPanel1.Controls.Add(chkMultilineRows, 4, 0);
            tableLayoutPanel1.Location = new Point(3, 3);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(997, 30);
            tableLayoutPanel1.TabIndex = 8;
            // 
            // butDebug
            // 
            butDebug.AutoSize = true;
            butDebug.Location = new Point(802, 3);
            butDebug.Name = "butDebug";
            butDebug.Size = new Size(78, 24);
            butDebug.TabIndex = 8;
            butDebug.Text = "DEBUG info";
            butDebug.UseVisualStyleBackColor = true;
            butDebug.Visible = false;
            butDebug.Click += butDebug_Click;
            // 
            // butMoveUp
            // 
            butMoveUp.AutoSize = true;
            butMoveUp.Location = new Point(3, 3);
            butMoveUp.Margin = new Padding(3, 3, 6, 3);
            butMoveUp.Name = "butMoveUp";
            butMoveUp.Size = new Size(75, 24);
            butMoveUp.TabIndex = 0;
            butMoveUp.Text = "Move up";
            butMoveUp.UseVisualStyleBackColor = true;
            butMoveUp.Click += ButMoveUp_Click;
            // 
            // butRunTests
            // 
            butRunTests.AutoSize = true;
            butRunTests.Location = new Point(718, 3);
            butRunTests.Margin = new Padding(3, 3, 6, 3);
            butRunTests.Name = "butRunTests";
            butRunTests.Size = new Size(75, 24);
            butRunTests.TabIndex = 7;
            butRunTests.Text = "Run tests";
            butRunTests.UseVisualStyleBackColor = true;
            butRunTests.Visible = false;
            butRunTests.Click += ButRunTests_Click;
            // 
            // butClearResults
            // 
            butClearResults.AutoSize = true;
            butClearResults.Location = new Point(613, 3);
            butClearResults.Margin = new Padding(3, 3, 6, 3);
            butClearResults.Name = "butClearResults";
            butClearResults.Size = new Size(96, 24);
            butClearResults.TabIndex = 6;
            butClearResults.Text = "Clear all results";
            butClearResults.UseVisualStyleBackColor = true;
            butClearResults.Click += ButClearResults_Click;
            // 
            // butDeleteRows
            // 
            butDeleteRows.AutoSize = true;
            butDeleteRows.Location = new Point(260, 3);
            butDeleteRows.Margin = new Padding(3, 3, 6, 3);
            butDeleteRows.Name = "butDeleteRows";
            butDeleteRows.Size = new Size(75, 24);
            butDeleteRows.TabIndex = 3;
            butDeleteRows.Text = "Delete row";
            butDeleteRows.UseVisualStyleBackColor = true;
            butDeleteRows.Click += ButDeleteRows_Click;
            // 
            // butSendAll
            // 
            butSendAll.AutoSize = true;
            butSendAll.Location = new Point(529, 3);
            butSendAll.Margin = new Padding(3, 3, 6, 3);
            butSendAll.Name = "butSendAll";
            butSendAll.Size = new Size(75, 24);
            butSendAll.TabIndex = 5;
            butSendAll.Text = "Send all";
            butSendAll.UseVisualStyleBackColor = true;
            butSendAll.Click += ButSendAll_Click;
            // 
            // butMoveDown
            // 
            butMoveDown.AutoSize = true;
            butMoveDown.Location = new Point(87, 3);
            butMoveDown.Margin = new Padding(3, 3, 6, 3);
            butMoveDown.Name = "butMoveDown";
            butMoveDown.Size = new Size(80, 24);
            butMoveDown.TabIndex = 1;
            butMoveDown.Text = "Move down";
            butMoveDown.UseVisualStyleBackColor = true;
            butMoveDown.Click += ButMoveDown_Click;
            // 
            // butAddRow
            // 
            butAddRow.AutoSize = true;
            butAddRow.Location = new Point(176, 3);
            butAddRow.Margin = new Padding(3, 3, 6, 3);
            butAddRow.Name = "butAddRow";
            butAddRow.Size = new Size(75, 24);
            butAddRow.TabIndex = 2;
            butAddRow.Text = "Add row";
            butAddRow.UseVisualStyleBackColor = true;
            butAddRow.Click += ButAddRow_Click;
            // 
            // chkMultilineRows
            // 
            chkMultilineRows.AutoSize = true;
            chkMultilineRows.Location = new Point(344, 8);
            chkMultilineRows.Margin = new Padding(3, 8, 6, 3);
            chkMultilineRows.Name = "chkMultilineRows";
            chkMultilineRows.Size = new Size(176, 17);
            chkMultilineRows.TabIndex = 4;
            chkMultilineRows.Text = "Multiline rows with auto row size";
            chkMultilineRows.UseVisualStyleBackColor = true;
            chkMultilineRows.CheckedChanged += ChkMultilineRows_CheckedChanged;
            // 
            // grid
            // 
            grid.AllowUserToAddRows = false;
            grid.AllowUserToDeleteRows = false;
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
            grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            grid.Columns.AddRange(new DataGridViewColumn[] { SendColumn, SlaveidColumn, CommandColumn, RegisterColumn, QtyColumn, FormatColumn, DataColumn, ResultColumn, CommentColumn, CountColumn });
            grid.Dock = DockStyle.Fill;
            grid.Location = new Point(0, 38);
            grid.Margin = new Padding(2);
            grid.Name = "grid";
            grid.RowHeadersVisible = false;
            grid.RowTemplate.Height = 23;
            grid.Size = new Size(1003, 554);
            grid.TabIndex = 1;
            grid.CellContentClick += MyDataGridView1_CellContentClick;
            grid.EditingControlShowing += grid_EditingControlShowing;
            // 
            // SendColumn
            // 
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            SendColumn.DefaultCellStyle = dataGridViewCellStyle1;
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
            Controls.Add(grid);
            Controls.Add(panel1);
            Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Icon = (Icon)resources.GetObject("$this.Icon");
            KeyPreview = true;
            Name = "CustomQueriesForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Custom modbus queries";
            FormClosing += CustomQueries_FormClosing;
            KeyDown += CustomQueriesForm_KeyDown;
            panel1.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)grid).EndInit();
            ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ToolTip toolTip1;
        private LST.Controls.MyDataGridView grid;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox chkMultilineRows;
        private System.Windows.Forms.Button butMoveUp;
        private System.Windows.Forms.Button butMoveDown;
        private System.Windows.Forms.Button butRunTests;
        private System.Windows.Forms.Button butSendAll;
        private System.Windows.Forms.Button butClearResults;
        private System.Windows.Forms.Button butAddRow;
        private System.Windows.Forms.Button butDeleteRows;
        private TableLayoutPanel tableLayoutPanel1;
        private Button butDebug;
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
    }
}