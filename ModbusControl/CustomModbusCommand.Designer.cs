namespace ModbusControlSpace
{
    partial class CustomModbusCommand
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CustomModbusCommand));
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.butMoveDown = new System.Windows.Forms.Button();
            this.butMoveUp = new System.Windows.Forms.Button();
            this.checkboxAutoRowSize = new System.Windows.Forms.CheckBox();
            this.grid = new LST.Helpers.MyDataGridView();
            this.ExecColumn = new System.Windows.Forms.DataGridViewButtonColumn();
            this.SlaveidColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CommandColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.RegisterColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QtyColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FormatColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.DataColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ResultColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CommentColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.butMoveDown);
            this.panel1.Controls.Add(this.butMoveUp);
            this.panel1.Controls.Add(this.checkboxAutoRowSize);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(760, 29);
            this.panel1.TabIndex = 8;
            // 
            // butMoveDown
            // 
            this.butMoveDown.Location = new System.Drawing.Point(128, 5);
            this.butMoveDown.Name = "butMoveDown";
            this.butMoveDown.Size = new System.Drawing.Size(75, 21);
            this.butMoveDown.TabIndex = 2;
            this.butMoveDown.Text = "Move down";
            this.butMoveDown.UseVisualStyleBackColor = true;
            this.butMoveDown.Click += new System.EventHandler(this.butMoveDown_Click);
            // 
            // butMoveUp
            // 
            this.butMoveUp.Location = new System.Drawing.Point(38, 4);
            this.butMoveUp.Name = "butMoveUp";
            this.butMoveUp.Size = new System.Drawing.Size(75, 21);
            this.butMoveUp.TabIndex = 1;
            this.butMoveUp.Text = "Move up";
            this.butMoveUp.UseVisualStyleBackColor = true;
            this.butMoveUp.Click += new System.EventHandler(this.butMoveUp_Click);
            // 
            // checkboxAutoRowSize
            // 
            this.checkboxAutoRowSize.AutoSize = true;
            this.checkboxAutoRowSize.Location = new System.Drawing.Point(396, 10);
            this.checkboxAutoRowSize.Name = "checkboxAutoRowSize";
            this.checkboxAutoRowSize.Size = new System.Drawing.Size(176, 17);
            this.checkboxAutoRowSize.TabIndex = 0;
            this.checkboxAutoRowSize.Text = "Multiline rows with auto row size";
            this.checkboxAutoRowSize.UseVisualStyleBackColor = true;
            this.checkboxAutoRowSize.CheckedChanged += new System.EventHandler(this.checkboxAutoRowSize_CheckedChanged);
            // 
            // grid
            // 
            this.grid.AllowUserToAddRows = false;
            this.grid.AllowUserToDeleteRows = false;
            this.grid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.grid.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
            this.grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ExecColumn,
            this.SlaveidColumn,
            this.CommandColumn,
            this.RegisterColumn,
            this.QtyColumn,
            this.FormatColumn,
            this.DataColumn,
            this.ResultColumn,
            this.CommentColumn});
            this.grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grid.Location = new System.Drawing.Point(0, 29);
            this.grid.Margin = new System.Windows.Forms.Padding(2);
            this.grid.Name = "grid";
            this.grid.RowHeadersVisible = false;
            this.grid.RowTemplate.Height = 23;
            this.grid.Size = new System.Drawing.Size(760, 333);
            this.grid.TabIndex = 7;
            this.grid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.myDataGridView1_CellContentClick);
            // 
            // ExecColumn
            // 
            this.ExecColumn.FillWeight = 50F;
            this.ExecColumn.HeaderText = "Execute";
            this.ExecColumn.Name = "ExecColumn";
            this.ExecColumn.Text = "Exec";
            this.ExecColumn.UseColumnTextForButtonValue = true;
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
            "R holding registers (FC 03)",
            "W single holding register (FC 06)",
            "W multiple holding registers (FC 10)",
            "R input registers (FC 04)",
            "R Coils (FC 01)",
            "W single coil (FC 05)",
            "W multiple coils (FC 0F)",
            "R discrete inputs (FC 02)",
            "Raw hex query"});
            this.CommandColumn.Name = "CommandColumn";
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
            "Uint16",
            "Hex16",
            "Bin16",
            "Ascii16",
            "Int32",
            "Uint32",
            "Hex32",
            "Bin32",
            "Float32",
            "Ascii32"});
            this.FormatColumn.Name = "FormatColumn";
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
            // CustomModbusCommand
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(760, 362);
            this.Controls.Add(this.grid);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CustomModbusCommand";
            this.Text = "Custom modbus command";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CustomModbusCommand_FormClosing);
            this.Shown += new System.EventHandler(this.CustomModbusCommand_Shown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ToolTip toolTip1;
        private LST.Helpers.MyDataGridView grid;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox checkboxAutoRowSize;
        private System.Windows.Forms.Button butMoveUp;
        private System.Windows.Forms.Button butMoveDown;
        private System.Windows.Forms.DataGridViewButtonColumn ExecColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn SlaveidColumn;
        private System.Windows.Forms.DataGridViewComboBoxColumn CommandColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn RegisterColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn QtyColumn;
        private System.Windows.Forms.DataGridViewComboBoxColumn FormatColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn DataColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ResultColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn CommentColumn;
    }
}