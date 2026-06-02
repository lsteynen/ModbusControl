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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CustomQueriesForm));
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.butDeleteRows = new System.Windows.Forms.Button();
            this.butAddRow = new System.Windows.Forms.Button();
            this.butClearResults = new System.Windows.Forms.Button();
            this.butSendAll = new System.Windows.Forms.Button();
            this.butRunTests = new System.Windows.Forms.Button();
            this.butMoveDown = new System.Windows.Forms.Button();
            this.butMoveUp = new System.Windows.Forms.Button();
            this.chkMultilineRows = new System.Windows.Forms.CheckBox();
            this.grid = new LST.Controls.MyDataGridView();
            this.SendColumn = new System.Windows.Forms.DataGridViewButtonColumn();
            this.SlaveidColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CommandColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.RegisterColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QtyColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FormatColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.DataColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ResultColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CommentColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CountColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.butDeleteRows);
            this.panel1.Controls.Add(this.butAddRow);
            this.panel1.Controls.Add(this.butClearResults);
            this.panel1.Controls.Add(this.butSendAll);
            this.panel1.Controls.Add(this.butRunTests);
            this.panel1.Controls.Add(this.butMoveDown);
            this.panel1.Controls.Add(this.butMoveUp);
            this.panel1.Controls.Add(this.chkMultilineRows);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1003, 29);
            this.panel1.TabIndex = 0;
            // 
            // butDeleteRows
            // 
            this.butDeleteRows.Location = new System.Drawing.Point(246, 4);
            this.butDeleteRows.Name = "butDeleteRows";
            this.butDeleteRows.Size = new System.Drawing.Size(72, 21);
            this.butDeleteRows.TabIndex = 3;
            this.butDeleteRows.Text = "Delete row";
            this.butDeleteRows.UseVisualStyleBackColor = true;
            this.butDeleteRows.Click += new System.EventHandler(this.ButDeleteRows_Click);
            // 
            // butAddRow
            // 
            this.butAddRow.Location = new System.Drawing.Point(168, 4);
            this.butAddRow.Name = "butAddRow";
            this.butAddRow.Size = new System.Drawing.Size(72, 21);
            this.butAddRow.TabIndex = 2;
            this.butAddRow.Text = "Add row";
            this.butAddRow.UseVisualStyleBackColor = true;
            this.butAddRow.Click += new System.EventHandler(this.ButAddRow_Click);
            // 
            // butClearResults
            // 
            this.butClearResults.Location = new System.Drawing.Point(601, 3);
            this.butClearResults.Name = "butClearResults";
            this.butClearResults.Size = new System.Drawing.Size(89, 23);
            this.butClearResults.TabIndex = 6;
            this.butClearResults.Text = "Clear all results";
            this.butClearResults.UseVisualStyleBackColor = true;
            this.butClearResults.Click += new System.EventHandler(this.ButClearResults_Click);
            // 
            // butSendAll
            // 
            this.butSendAll.Location = new System.Drawing.Point(511, 3);
            this.butSendAll.Name = "butSendAll";
            this.butSendAll.Size = new System.Drawing.Size(75, 23);
            this.butSendAll.TabIndex = 5;
            this.butSendAll.Text = "Send all";
            this.butSendAll.UseVisualStyleBackColor = true;
            this.butSendAll.Click += new System.EventHandler(this.ButSendAll_Click);
            // 
            // butRunTests
            // 
            this.butRunTests.Location = new System.Drawing.Point(708, 3);
            this.butRunTests.Name = "butRunTests";
            this.butRunTests.Size = new System.Drawing.Size(75, 23);
            this.butRunTests.TabIndex = 7;
            this.butRunTests.Text = "Run tests";
            this.butRunTests.UseVisualStyleBackColor = true;
            this.butRunTests.Visible = false;
            this.butRunTests.Click += new System.EventHandler(this.ButRunTests_Click);
            // 
            // butMoveDown
            // 
            this.butMoveDown.Location = new System.Drawing.Point(90, 4);
            this.butMoveDown.Name = "butMoveDown";
            this.butMoveDown.Size = new System.Drawing.Size(72, 21);
            this.butMoveDown.TabIndex = 1;
            this.butMoveDown.Text = "Move down";
            this.butMoveDown.UseVisualStyleBackColor = true;
            this.butMoveDown.Click += new System.EventHandler(this.ButMoveDown_Click);
            // 
            // butMoveUp
            // 
            this.butMoveUp.Location = new System.Drawing.Point(12, 4);
            this.butMoveUp.Name = "butMoveUp";
            this.butMoveUp.Size = new System.Drawing.Size(72, 21);
            this.butMoveUp.TabIndex = 0;
            this.butMoveUp.Text = "Move up";
            this.butMoveUp.UseVisualStyleBackColor = true;
            this.butMoveUp.Click += new System.EventHandler(this.ButMoveUp_Click);
            // 
            // chkMultilineRows
            // 
            this.chkMultilineRows.AutoSize = true;
            this.chkMultilineRows.Location = new System.Drawing.Point(324, 7);
            this.chkMultilineRows.Name = "chkMultilineRows";
            this.chkMultilineRows.Size = new System.Drawing.Size(176, 17);
            this.chkMultilineRows.TabIndex = 4;
            this.chkMultilineRows.Text = "Multiline rows with auto row size";
            this.chkMultilineRows.UseVisualStyleBackColor = true;
            this.chkMultilineRows.CheckedChanged += new System.EventHandler(this.ChkMultilineRows_CheckedChanged);
            // 
            // grid
            // 
            this.grid.AllowUserToAddRows = false;
            this.grid.AllowUserToDeleteRows = false;
            this.grid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.grid.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
            this.grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SendColumn,
            this.SlaveidColumn,
            this.CommandColumn,
            this.RegisterColumn,
            this.QtyColumn,
            this.FormatColumn,
            this.DataColumn,
            this.ResultColumn,
            this.CommentColumn,
            this.CountColumn});
            this.grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grid.Location = new System.Drawing.Point(0, 29);
            this.grid.Margin = new System.Windows.Forms.Padding(2);
            this.grid.Name = "grid";
            this.grid.RowHeadersVisible = false;
            this.grid.RowTemplate.Height = 23;
            this.grid.Size = new System.Drawing.Size(1003, 563);
            this.grid.TabIndex = 1;
            this.grid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.MyDataGridView1_CellContentClick);
            // 
            // SendColumn
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.SendColumn.DefaultCellStyle = dataGridViewCellStyle1;
            this.SendColumn.FillWeight = 50F;
            this.SendColumn.HeaderText = "Send";
            this.SendColumn.Name = "SendColumn";
            this.SendColumn.Text = "Send";
            this.SendColumn.UseColumnTextForButtonValue = true;
            // 
            // SlaveidColumn
            // 
            this.SlaveidColumn.FillWeight = 20F;
            this.SlaveidColumn.HeaderText = "SlaveID";
            this.SlaveidColumn.Name = "SlaveidColumn";
            // 
            // CommandColumn
            // 
            this.CommandColumn.DropDownWidth = 220;
            this.CommandColumn.HeaderText = "Command";
            this.CommandColumn.Items.AddRange(new object[] {
            "R input registers (FC 04)",
            "R holding registers (FC 03)",
            "W single holding register (FC 06)",
            "W multiple holding registers (FC 10)",
            "R discrete inputs (FC 02)",
            "R coils (FC 01)",
            "W single coil (FC 05)",
            "W multiple coils (FC 0F)",
            "Custom query"});
            this.CommandColumn.Name = "CommandColumn";
            this.CommandColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // RegisterColumn
            // 
            this.RegisterColumn.FillWeight = 50F;
            this.RegisterColumn.HeaderText = "Register";
            this.RegisterColumn.Name = "RegisterColumn";
            // 
            // QtyColumn
            // 
            this.QtyColumn.FillWeight = 50F;
            this.QtyColumn.HeaderText = "Qty";
            this.QtyColumn.Name = "QtyColumn";
            // 
            // FormatColumn
            // 
            this.FormatColumn.FillWeight = 50F;
            this.FormatColumn.HeaderText = "Format";
            this.FormatColumn.Items.AddRange(new object[] {
            "Int16",
            "UInt16",
            "Hex16",
            "Int32",
            "UInt32",
            "Hex32",
            "HexBytes",
            "Bits"});
            this.FormatColumn.Name = "FormatColumn";
            this.FormatColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // DataColumn
            // 
            this.DataColumn.HeaderText = "Data";
            this.DataColumn.Name = "DataColumn";
            // 
            // ResultColumn
            // 
            this.ResultColumn.FillWeight = 300F;
            this.ResultColumn.HeaderText = "Result";
            this.ResultColumn.Name = "ResultColumn";
            this.ResultColumn.ReadOnly = true;
            // 
            // CommentColumn
            // 
            this.CommentColumn.HeaderText = "Comment";
            this.CommentColumn.Name = "CommentColumn";
            // 
            // CountColumn
            // 
            this.CountColumn.FillWeight = 40F;
            this.CountColumn.HeaderText = "Count";
            this.CountColumn.Name = "CountColumn";
            this.CountColumn.ReadOnly = true;
            this.CountColumn.Visible = false;
            // 
            // CustomQueries
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1003, 592);
            this.Controls.Add(this.grid);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CustomQueries";
            this.Text = "Custom modbus queries";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CustomQueries_FormClosing);
            this.Shown += new System.EventHandler(this.CustomQueries_Shown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            this.ResumeLayout(false);

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
        private System.Windows.Forms.DataGridViewButtonColumn SendColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn SlaveidColumn;
        private System.Windows.Forms.DataGridViewComboBoxColumn CommandColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn RegisterColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn QtyColumn;
        private System.Windows.Forms.DataGridViewComboBoxColumn FormatColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn DataColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ResultColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn CommentColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn CountColumn;
    }
}