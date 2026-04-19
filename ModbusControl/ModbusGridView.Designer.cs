
namespace ModbusControl
{
    partial class ModbusGridView
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ModbusGridView));
            this.ContextMenuForOtherColumns = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.markThisLineInGreenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.markThisLineInBlueToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.markThisLineInYellowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeColorMarkOfThisLineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.copyGridLineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextMenuForValueColumn = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.showValueAsIntToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showValueAsUIntToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showValueAsHexToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showValueAsBinaryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showValueAsFloatToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showValueAsASCIIToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showValueAsStringToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.copyValueToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextMenuForRegColumn = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem7 = new System.Windows.Forms.ToolStripMenuItem();
            this.expandAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.collapseAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.grid = new LST.Controls.MyDataGridView();
            this.colReg = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDescr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colProps = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colValue = new EnumValueColumn();
            this.colDebuginfo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ContextMenuForOtherColumns.SuspendLayout();
            this.ContextMenuForValueColumn.SuspendLayout();
            this.ContextMenuForRegColumn.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            this.SuspendLayout();
            // 
            // ContextMenuForOtherColumns
            // 
            this.ContextMenuForOtherColumns.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.ContextMenuForOtherColumns.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.markThisLineInGreenToolStripMenuItem,
            this.markThisLineInBlueToolStripMenuItem,
            this.markThisLineInYellowToolStripMenuItem,
            this.removeColorMarkOfThisLineToolStripMenuItem,
            this.toolStripSeparator1,
            this.copyGridLineToolStripMenuItem});
            this.ContextMenuForOtherColumns.Name = "ContextMenuForFirstColumns";
            this.ContextMenuForOtherColumns.Size = new System.Drawing.Size(240, 166);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItem1.Image")));
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(239, 26);
            this.toolStripMenuItem1.Text = "Mark this line in red";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.Menu_SetColorRed);
            // 
            // markThisLineInGreenToolStripMenuItem
            // 
            this.markThisLineInGreenToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("markThisLineInGreenToolStripMenuItem.Image")));
            this.markThisLineInGreenToolStripMenuItem.Name = "markThisLineInGreenToolStripMenuItem";
            this.markThisLineInGreenToolStripMenuItem.Size = new System.Drawing.Size(239, 26);
            this.markThisLineInGreenToolStripMenuItem.Text = "Mark this line in green";
            this.markThisLineInGreenToolStripMenuItem.Click += new System.EventHandler(this.Menu_SetColorGreen);
            // 
            // markThisLineInBlueToolStripMenuItem
            // 
            this.markThisLineInBlueToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("markThisLineInBlueToolStripMenuItem.Image")));
            this.markThisLineInBlueToolStripMenuItem.Name = "markThisLineInBlueToolStripMenuItem";
            this.markThisLineInBlueToolStripMenuItem.Size = new System.Drawing.Size(239, 26);
            this.markThisLineInBlueToolStripMenuItem.Text = "Mark this line in blue";
            this.markThisLineInBlueToolStripMenuItem.Click += new System.EventHandler(this.Menu_SetColorBlue);
            // 
            // markThisLineInYellowToolStripMenuItem
            // 
            this.markThisLineInYellowToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("markThisLineInYellowToolStripMenuItem.Image")));
            this.markThisLineInYellowToolStripMenuItem.Name = "markThisLineInYellowToolStripMenuItem";
            this.markThisLineInYellowToolStripMenuItem.Size = new System.Drawing.Size(239, 26);
            this.markThisLineInYellowToolStripMenuItem.Text = "Mark this line in yellow";
            this.markThisLineInYellowToolStripMenuItem.Click += new System.EventHandler(this.Menu_SetColorYellow);
            // 
            // removeColorMarkOfThisLineToolStripMenuItem
            // 
            this.removeColorMarkOfThisLineToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("removeColorMarkOfThisLineToolStripMenuItem.Image")));
            this.removeColorMarkOfThisLineToolStripMenuItem.Name = "removeColorMarkOfThisLineToolStripMenuItem";
            this.removeColorMarkOfThisLineToolStripMenuItem.Size = new System.Drawing.Size(239, 26);
            this.removeColorMarkOfThisLineToolStripMenuItem.Text = "Remove color mark of this line";
            this.removeColorMarkOfThisLineToolStripMenuItem.Click += new System.EventHandler(this.Menu_SetColorDefault);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(236, 6);
            // 
            // copyGridLineToolStripMenuItem
            // 
            this.copyGridLineToolStripMenuItem.Name = "copyGridLineToolStripMenuItem";
            this.copyGridLineToolStripMenuItem.Size = new System.Drawing.Size(239, 26);
            this.copyGridLineToolStripMenuItem.Text = "Copy grid line";
            this.copyGridLineToolStripMenuItem.Click += new System.EventHandler(this.Menu_CopyLine);
            // 
            // ContextMenuForValueColumn
            // 
            this.ContextMenuForValueColumn.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.ContextMenuForValueColumn.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showValueAsIntToolStripMenuItem,
            this.showValueAsUIntToolStripMenuItem,
            this.showValueAsHexToolStripMenuItem,
            this.showValueAsBinaryToolStripMenuItem,
            this.showValueAsFloatToolStripMenuItem,
            this.showValueAsASCIIToolStripMenuItem,
            this.showValueAsStringToolStripMenuItem,
            this.toolStripSeparator2,
            this.copyValueToolStripMenuItem});
            this.ContextMenuForValueColumn.Name = "ContextMenuForValueColumn";
            this.ContextMenuForValueColumn.Size = new System.Drawing.Size(185, 208);
            // 
            // showValueAsIntToolStripMenuItem
            // 
            this.showValueAsIntToolStripMenuItem.Name = "showValueAsIntToolStripMenuItem";
            this.showValueAsIntToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.showValueAsIntToolStripMenuItem.Text = "Show value as Int";
            this.showValueAsIntToolStripMenuItem.Click += new System.EventHandler(this.ShowValueAsIntToolStripMenuItem_Click);
            // 
            // showValueAsUIntToolStripMenuItem
            // 
            this.showValueAsUIntToolStripMenuItem.Name = "showValueAsUIntToolStripMenuItem";
            this.showValueAsUIntToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.showValueAsUIntToolStripMenuItem.Text = "Show value as UInt";
            this.showValueAsUIntToolStripMenuItem.Click += new System.EventHandler(this.ShowValueAsUIntToolStripMenuItem_Click);
            // 
            // showValueAsHexToolStripMenuItem
            // 
            this.showValueAsHexToolStripMenuItem.Name = "showValueAsHexToolStripMenuItem";
            this.showValueAsHexToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.showValueAsHexToolStripMenuItem.Text = "Show value as Hex";
            this.showValueAsHexToolStripMenuItem.Click += new System.EventHandler(this.ShowValueAsHexToolStripMenuItem_Click);
            // 
            // showValueAsBinaryToolStripMenuItem
            // 
            this.showValueAsBinaryToolStripMenuItem.Name = "showValueAsBinaryToolStripMenuItem";
            this.showValueAsBinaryToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.showValueAsBinaryToolStripMenuItem.Text = "Show value as Binary";
            this.showValueAsBinaryToolStripMenuItem.Click += new System.EventHandler(this.ShowValueAsBinaryToolStripMenuItem_Click);
            // 
            // showValueAsFloatToolStripMenuItem
            // 
            this.showValueAsFloatToolStripMenuItem.Name = "showValueAsFloatToolStripMenuItem";
            this.showValueAsFloatToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.showValueAsFloatToolStripMenuItem.Text = "Show value as Float";
            this.showValueAsFloatToolStripMenuItem.Click += new System.EventHandler(this.ShowValueAsFloatToolStripMenuItem_Click);
            // 
            // showValueAsASCIIToolStripMenuItem
            // 
            this.showValueAsASCIIToolStripMenuItem.Name = "showValueAsASCIIToolStripMenuItem";
            this.showValueAsASCIIToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.showValueAsASCIIToolStripMenuItem.Text = "Show value as ASCII";
            this.showValueAsASCIIToolStripMenuItem.Click += new System.EventHandler(this.ShowValueAsASCIIToolStripMenuItem_Click);
            // 
            // showValueAsStringToolStripMenuItem
            // 
            this.showValueAsStringToolStripMenuItem.Name = "showValueAsStringToolStripMenuItem";
            this.showValueAsStringToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.showValueAsStringToolStripMenuItem.Text = "Show value as String";
            this.showValueAsStringToolStripMenuItem.Click += new System.EventHandler(this.ShowValueAsStringToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(181, 6);
            // 
            // copyValueToolStripMenuItem
            // 
            this.copyValueToolStripMenuItem.Name = "copyValueToolStripMenuItem";
            this.copyValueToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.copyValueToolStripMenuItem.Text = "Copy value";
            this.copyValueToolStripMenuItem.Click += new System.EventHandler(this.CopyValueToolStripMenuItem_Click);
            // 
            // ContextMenuForRegColumn
            // 
            this.ContextMenuForRegColumn.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.ContextMenuForRegColumn.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem2,
            this.toolStripMenuItem3,
            this.toolStripMenuItem4,
            this.toolStripMenuItem5,
            this.toolStripMenuItem6,
            this.toolStripSeparator3,
            this.toolStripMenuItem7,
            this.expandAllToolStripMenuItem,
            this.collapseAllToolStripMenuItem});
            this.ContextMenuForRegColumn.Name = "ContextMenuForFirstColumns";
            this.ContextMenuForRegColumn.Size = new System.Drawing.Size(240, 218);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItem2.Image")));
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(239, 26);
            this.toolStripMenuItem2.Text = "Mark this line in red";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.Menu_SetColorRed);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItem3.Image")));
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(239, 26);
            this.toolStripMenuItem3.Text = "Mark this line in green";
            this.toolStripMenuItem3.Click += new System.EventHandler(this.Menu_SetColorGreen);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItem4.Image")));
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(239, 26);
            this.toolStripMenuItem4.Text = "Mark this line in blue";
            this.toolStripMenuItem4.Click += new System.EventHandler(this.Menu_SetColorBlue);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItem5.Image")));
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(239, 26);
            this.toolStripMenuItem5.Text = "Mark this line in yellow";
            this.toolStripMenuItem5.Click += new System.EventHandler(this.Menu_SetColorYellow);
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItem6.Image")));
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(239, 26);
            this.toolStripMenuItem6.Text = "Remove color mark of this line";
            this.toolStripMenuItem6.Click += new System.EventHandler(this.Menu_SetColorDefault);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(236, 6);
            // 
            // toolStripMenuItem7
            // 
            this.toolStripMenuItem7.Name = "toolStripMenuItem7";
            this.toolStripMenuItem7.Size = new System.Drawing.Size(239, 26);
            this.toolStripMenuItem7.Text = "Copy grid line";
            this.toolStripMenuItem7.Click += new System.EventHandler(this.Menu_CopyLine);
            // 
            // expandAllToolStripMenuItem
            // 
            this.expandAllToolStripMenuItem.Name = "expandAllToolStripMenuItem";
            this.expandAllToolStripMenuItem.Size = new System.Drawing.Size(239, 26);
            this.expandAllToolStripMenuItem.Text = "Expand all";
            this.expandAllToolStripMenuItem.Click += new System.EventHandler(this.expandAllToolStripMenuItem_Click);
            // 
            // collapseAllToolStripMenuItem
            // 
            this.collapseAllToolStripMenuItem.Name = "collapseAllToolStripMenuItem";
            this.collapseAllToolStripMenuItem.Size = new System.Drawing.Size(239, 26);
            this.collapseAllToolStripMenuItem.Text = "Collapse all";
            this.collapseAllToolStripMenuItem.Click += new System.EventHandler(this.collapseAllToolStripMenuItem_Click);
            // 
            // grid
            // 
            this.grid.AllowUserToAddRows = false;
            this.grid.AllowUserToDeleteRows = false;
            this.grid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colReg,
            this.colDescr,
            this.colProps,
            this.colValue,
            this.colDebuginfo});
            this.grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.grid.Location = new System.Drawing.Point(0, 0);
            this.grid.Margin = new System.Windows.Forms.Padding(2);
            this.grid.MultiSelect = false;
            this.grid.Name = "grid";
            this.grid.RowHeadersVisible = false;
            this.grid.RowHeadersWidth = 25;
            this.grid.RowTemplate.Height = 23;
            this.grid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grid.Size = new System.Drawing.Size(669, 222);
            this.grid.TabIndex = 0;
            this.grid.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.grid_CellBeginEdit);
            this.grid.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grid_CellDoubleClick);
            this.grid.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.grid_CellEndEdit);
            this.grid.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.grid_CellFormatting);
            this.grid.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.grid_CellMouseDown);
            this.grid.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.grid_CellPainting);
            this.grid.CellToolTipTextNeeded += new System.Windows.Forms.DataGridViewCellToolTipTextNeededEventHandler(this.grid_CellToolTipTextNeeded);
            this.grid.ColumnWidthChanged += new System.Windows.Forms.DataGridViewColumnEventHandler(this.grid_ColumnWidthChanged);
            this.grid.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.grid_EditingControlShowing);
            this.grid.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.grid_RowPrePaint);
            this.grid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.grid_KeyDown);
            this.grid.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.grid_KeyPress);
            this.grid.MouseUp += new System.Windows.Forms.MouseEventHandler(this.grid_MouseUp);
            // 
            // colReg
            // 
            this.colReg.HeaderText = "Register";
            this.colReg.MinimumWidth = 6;
            this.colReg.Name = "colReg";
            this.colReg.ReadOnly = true;
            this.colReg.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // colDescr
            // 
            this.colDescr.FillWeight = 400F;
            this.colDescr.HeaderText = "Description";
            this.colDescr.MinimumWidth = 6;
            this.colDescr.Name = "colDescr";
            this.colDescr.ReadOnly = true;
            this.colDescr.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // colProps
            // 
            this.colProps.HeaderText = "Properties";
            this.colProps.MinimumWidth = 6;
            this.colProps.Name = "colProps";
            this.colProps.ReadOnly = true;
            this.colProps.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // colValue
            // 
            this.colValue.HeaderText = "Value";
            this.colValue.MinimumWidth = 6;
            this.colValue.Name = "colValue";
            // 
            // colDebuginfo
            // 
            this.colDebuginfo.FillWeight = 400F;
            this.colDebuginfo.HeaderText = "Debuginfo";
            this.colDebuginfo.MinimumWidth = 6;
            this.colDebuginfo.Name = "colDebuginfo";
            this.colDebuginfo.ReadOnly = true;
            this.colDebuginfo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colDebuginfo.Visible = false;
            // 
            // ModbusGridView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.grid);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "ModbusGridView";
            this.Size = new System.Drawing.Size(669, 222);
            this.ContextMenuForOtherColumns.ResumeLayout(false);
            this.ContextMenuForValueColumn.ResumeLayout(false);
            this.ContextMenuForRegColumn.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private LST.Controls.MyDataGridView grid;
        private System.Windows.Forms.ContextMenuStrip ContextMenuForOtherColumns;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem markThisLineInGreenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem markThisLineInBlueToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem markThisLineInYellowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeColorMarkOfThisLineToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem copyGridLineToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip ContextMenuForValueColumn;
        private System.Windows.Forms.ToolStripMenuItem showValueAsIntToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showValueAsUIntToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showValueAsHexToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showValueAsBinaryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showValueAsFloatToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showValueAsASCIIToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem copyValueToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip ContextMenuForRegColumn;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem6;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem7;
        private System.Windows.Forms.ToolStripMenuItem expandAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem collapseAllToolStripMenuItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn colReg;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDescr;
        private System.Windows.Forms.DataGridViewTextBoxColumn colProps;
        private EnumValueColumn colValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDebuginfo;
        private System.Windows.Forms.ToolStripMenuItem showValueAsStringToolStripMenuItem;
    }
}
