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
            flowLayoutPanel1 = new FlowLayoutPanel();
            butMoveUp = new Button();
            butMoveDown = new Button();
            butAddRow = new Button();
            butDeleteRows = new Button();
            chkMultilineRows = new CheckBox();
            butSendAll = new Button();
            butClearResults = new Button();
            butRunTests = new Button();
            butDebug = new Button();
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
            flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)grid).BeginInit();
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
            flowLayoutPanel1.Controls.Add(butMoveUp);
            flowLayoutPanel1.Controls.Add(butMoveDown);
            flowLayoutPanel1.Controls.Add(butAddRow);
            flowLayoutPanel1.Controls.Add(butDeleteRows);
            flowLayoutPanel1.Controls.Add(chkMultilineRows);
            flowLayoutPanel1.Controls.Add(butSendAll);
            flowLayoutPanel1.Controls.Add(butClearResults);
            flowLayoutPanel1.Controls.Add(butRunTests);
            flowLayoutPanel1.Controls.Add(butDebug);
            flowLayoutPanel1.Location = new Point(3, 3);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(927, 31);
            flowLayoutPanel1.TabIndex = 9;
            flowLayoutPanel1.WrapContents = false;
            // 
            // butMoveUp
            // 
            butMoveUp.Location = new Point(3, 3);
            butMoveUp.Margin = new Padding(3, 3, 6, 3);
            butMoveUp.Name = "butMoveUp";
            butMoveUp.Size = new Size(82, 23);
            butMoveUp.TabIndex = 0;
            butMoveUp.Text = "Move up";
            butMoveUp.UseVisualStyleBackColor = true;
            butMoveUp.Click += ButMoveUp_Click;
            // 
            // butMoveDown
            // 
            butMoveDown.Location = new Point(94, 3);
            butMoveDown.Margin = new Padding(3, 3, 6, 3);
            butMoveDown.Name = "butMoveDown";
            butMoveDown.Size = new Size(82, 23);
            butMoveDown.TabIndex = 1;
            butMoveDown.Text = "Move down";
            butMoveDown.UseVisualStyleBackColor = true;
            butMoveDown.Click += ButMoveDown_Click;
            // 
            // butAddRow
            // 
            butAddRow.Location = new Point(185, 3);
            butAddRow.Margin = new Padding(3, 3, 6, 3);
            butAddRow.Name = "butAddRow";
            butAddRow.Size = new Size(78, 23);
            butAddRow.TabIndex = 2;
            butAddRow.Text = "Add row";
            butAddRow.UseVisualStyleBackColor = true;
            butAddRow.Click += ButAddRow_Click;
            // 
            // butDeleteRows
            // 
            butDeleteRows.Location = new Point(272, 3);
            butDeleteRows.Margin = new Padding(3, 3, 6, 3);
            butDeleteRows.Name = "butDeleteRows";
            butDeleteRows.Size = new Size(78, 23);
            butDeleteRows.TabIndex = 3;
            butDeleteRows.Text = "Delete row";
            butDeleteRows.UseVisualStyleBackColor = true;
            butDeleteRows.Click += ButDeleteRows_Click;
            // 
            // chkMultilineRows
            // 
            chkMultilineRows.AutoSize = true;
            chkMultilineRows.Location = new Point(359, 7);
            chkMultilineRows.Margin = new Padding(3, 7, 6, 3);
            chkMultilineRows.Name = "chkMultilineRows";
            chkMultilineRows.Size = new Size(176, 17);
            chkMultilineRows.TabIndex = 4;
            chkMultilineRows.Text = "Multiline rows with auto row size";
            chkMultilineRows.UseVisualStyleBackColor = true;
            chkMultilineRows.CheckedChanged += ChkMultilineRows_CheckedChanged;
            // 
            // butSendAll
            // 
            butSendAll.Location = new Point(544, 3);
            butSendAll.Margin = new Padding(3, 3, 6, 3);
            butSendAll.Name = "butSendAll";
            butSendAll.Size = new Size(75, 23);
            butSendAll.TabIndex = 5;
            butSendAll.Text = "Send all";
            butSendAll.UseVisualStyleBackColor = true;
            butSendAll.Click += ButSendAll_Click;
            // 
            // butClearResults
            // 
            butClearResults.Location = new Point(628, 3);
            butClearResults.Margin = new Padding(3, 3, 6, 3);
            butClearResults.Name = "butClearResults";
            butClearResults.Size = new Size(98, 23);
            butClearResults.TabIndex = 6;
            butClearResults.Text = "Clear all results";
            butClearResults.UseVisualStyleBackColor = true;
            butClearResults.Click += ButClearResults_Click;
            // 
            // butRunTests
            // 
            butRunTests.Location = new Point(735, 3);
            butRunTests.Margin = new Padding(3, 3, 6, 3);
            butRunTests.Name = "butRunTests";
            butRunTests.Size = new Size(75, 23);
            butRunTests.TabIndex = 7;
            butRunTests.Text = "Run tests";
            butRunTests.UseVisualStyleBackColor = true;
            butRunTests.Visible = false;
            butRunTests.Click += ButRunTests_Click;
            // 
            // butDebug
            // 
            butDebug.Location = new Point(819, 3);
            butDebug.Name = "butDebug";
            butDebug.Size = new Size(82, 23);
            butDebug.TabIndex = 8;
            butDebug.Text = "DEBUG info";
            butDebug.UseVisualStyleBackColor = true;
            butDebug.Visible = false;
            butDebug.Click += butDebug_Click;
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
            grid.Location = new Point(0, 37);
            grid.Margin = new Padding(2);
            grid.Name = "grid";
            grid.RowHeadersVisible = false;
            grid.RowTemplate.Height = 23;
            grid.Size = new Size(1003, 555);
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
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Custom modbus queries";
            FormClosing += CustomQueries_FormClosing;
            KeyDown += CustomQueriesForm_KeyDown;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            flowLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel1.PerformLayout();
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
        private FlowLayoutPanel flowLayoutPanel1;
    }
}