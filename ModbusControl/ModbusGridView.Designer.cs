
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ModbusGridView));
            ContextMenuForOtherColumns = new ContextMenuStrip(components);
            toolStripMenuItem1 = new ToolStripMenuItem();
            markThisLineInGreenToolStripMenuItem = new ToolStripMenuItem();
            markThisLineInBlueToolStripMenuItem = new ToolStripMenuItem();
            markThisLineInYellowToolStripMenuItem = new ToolStripMenuItem();
            removeColorMarkOfThisLineToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            copyGridLineToolStripMenuItem = new ToolStripMenuItem();
            ContextMenuForValueColumn = new ContextMenuStrip(components);
            showValueAsIntToolStripMenuItem = new ToolStripMenuItem();
            showValueAsUIntToolStripMenuItem = new ToolStripMenuItem();
            showValueAsHexToolStripMenuItem = new ToolStripMenuItem();
            showValueAsBinaryToolStripMenuItem = new ToolStripMenuItem();
            showValueAsFloatToolStripMenuItem = new ToolStripMenuItem();
            showValueAsASCIIToolStripMenuItem = new ToolStripMenuItem();
            showValueAsStringToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator2 = new ToolStripSeparator();
            copyValueToolStripMenuItem = new ToolStripMenuItem();
            ContextMenuForRegColumn = new ContextMenuStrip(components);
            toolStripMenuItem2 = new ToolStripMenuItem();
            toolStripMenuItem3 = new ToolStripMenuItem();
            toolStripMenuItem4 = new ToolStripMenuItem();
            toolStripMenuItem5 = new ToolStripMenuItem();
            toolStripMenuItem6 = new ToolStripMenuItem();
            toolStripSeparator3 = new ToolStripSeparator();
            toolStripMenuItem7 = new ToolStripMenuItem();
            expandAllToolStripMenuItem = new ToolStripMenuItem();
            collapseAllToolStripMenuItem = new ToolStripMenuItem();
            grid = new LST.Controls.MyDataGridView();
            colReg = new DataGridViewTextBoxColumn();
            colDescr = new DataGridViewTextBoxColumn();
            colProps = new DataGridViewTextBoxColumn();
            colValue = new EnumValueColumn();
            colDebuginfo = new DataGridViewTextBoxColumn();
            ContextMenuForOtherColumns.SuspendLayout();
            ContextMenuForValueColumn.SuspendLayout();
            ContextMenuForRegColumn.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)grid).BeginInit();
            SuspendLayout();
            // 
            // ContextMenuForOtherColumns
            // 
            ContextMenuForOtherColumns.ImageScalingSize = new Size(20, 20);
            ContextMenuForOtherColumns.Items.AddRange(new ToolStripItem[] { toolStripMenuItem1, markThisLineInGreenToolStripMenuItem, markThisLineInBlueToolStripMenuItem, markThisLineInYellowToolStripMenuItem, removeColorMarkOfThisLineToolStripMenuItem, toolStripSeparator1, copyGridLineToolStripMenuItem });
            ContextMenuForOtherColumns.Name = "ContextMenuForFirstColumns";
            ContextMenuForOtherColumns.Size = new Size(240, 166);
            // 
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.Image = (Image)resources.GetObject("toolStripMenuItem1.Image");
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            toolStripMenuItem1.Size = new Size(239, 26);
            toolStripMenuItem1.Text = "Mark this line in red";
            toolStripMenuItem1.Click += Menu_SetColorRed;
            // 
            // markThisLineInGreenToolStripMenuItem
            // 
            markThisLineInGreenToolStripMenuItem.Image = (Image)resources.GetObject("markThisLineInGreenToolStripMenuItem.Image");
            markThisLineInGreenToolStripMenuItem.Name = "markThisLineInGreenToolStripMenuItem";
            markThisLineInGreenToolStripMenuItem.Size = new Size(239, 26);
            markThisLineInGreenToolStripMenuItem.Text = "Mark this line in green";
            markThisLineInGreenToolStripMenuItem.Click += Menu_SetColorGreen;
            // 
            // markThisLineInBlueToolStripMenuItem
            // 
            markThisLineInBlueToolStripMenuItem.Image = (Image)resources.GetObject("markThisLineInBlueToolStripMenuItem.Image");
            markThisLineInBlueToolStripMenuItem.Name = "markThisLineInBlueToolStripMenuItem";
            markThisLineInBlueToolStripMenuItem.Size = new Size(239, 26);
            markThisLineInBlueToolStripMenuItem.Text = "Mark this line in blue";
            markThisLineInBlueToolStripMenuItem.Click += Menu_SetColorBlue;
            // 
            // markThisLineInYellowToolStripMenuItem
            // 
            markThisLineInYellowToolStripMenuItem.Image = (Image)resources.GetObject("markThisLineInYellowToolStripMenuItem.Image");
            markThisLineInYellowToolStripMenuItem.Name = "markThisLineInYellowToolStripMenuItem";
            markThisLineInYellowToolStripMenuItem.Size = new Size(239, 26);
            markThisLineInYellowToolStripMenuItem.Text = "Mark this line in yellow";
            markThisLineInYellowToolStripMenuItem.Click += Menu_SetColorYellow;
            // 
            // removeColorMarkOfThisLineToolStripMenuItem
            // 
            removeColorMarkOfThisLineToolStripMenuItem.Image = (Image)resources.GetObject("removeColorMarkOfThisLineToolStripMenuItem.Image");
            removeColorMarkOfThisLineToolStripMenuItem.Name = "removeColorMarkOfThisLineToolStripMenuItem";
            removeColorMarkOfThisLineToolStripMenuItem.Size = new Size(239, 26);
            removeColorMarkOfThisLineToolStripMenuItem.Text = "Remove color mark of this line";
            removeColorMarkOfThisLineToolStripMenuItem.Click += Menu_SetColorDefault;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(236, 6);
            // 
            // copyGridLineToolStripMenuItem
            // 
            copyGridLineToolStripMenuItem.Name = "copyGridLineToolStripMenuItem";
            copyGridLineToolStripMenuItem.Size = new Size(239, 26);
            copyGridLineToolStripMenuItem.Text = "Copy grid line";
            copyGridLineToolStripMenuItem.Click += Menu_CopyLine;
            // 
            // ContextMenuForValueColumn
            // 
            ContextMenuForValueColumn.ImageScalingSize = new Size(20, 20);
            ContextMenuForValueColumn.Items.AddRange(new ToolStripItem[] { showValueAsIntToolStripMenuItem, showValueAsUIntToolStripMenuItem, showValueAsHexToolStripMenuItem, showValueAsBinaryToolStripMenuItem, showValueAsFloatToolStripMenuItem, showValueAsASCIIToolStripMenuItem, showValueAsStringToolStripMenuItem, toolStripSeparator2, copyValueToolStripMenuItem });
            ContextMenuForValueColumn.Name = "ContextMenuForValueColumn";
            ContextMenuForValueColumn.Size = new Size(185, 186);
            // 
            // showValueAsIntToolStripMenuItem
            // 
            showValueAsIntToolStripMenuItem.Name = "showValueAsIntToolStripMenuItem";
            showValueAsIntToolStripMenuItem.Size = new Size(184, 22);
            showValueAsIntToolStripMenuItem.Text = "Show value as Int";
            showValueAsIntToolStripMenuItem.Click += ShowValueAsIntToolStripMenuItem_Click;
            // 
            // showValueAsUIntToolStripMenuItem
            // 
            showValueAsUIntToolStripMenuItem.Name = "showValueAsUIntToolStripMenuItem";
            showValueAsUIntToolStripMenuItem.Size = new Size(184, 22);
            showValueAsUIntToolStripMenuItem.Text = "Show value as UInt";
            showValueAsUIntToolStripMenuItem.Click += ShowValueAsUIntToolStripMenuItem_Click;
            // 
            // showValueAsHexToolStripMenuItem
            // 
            showValueAsHexToolStripMenuItem.Name = "showValueAsHexToolStripMenuItem";
            showValueAsHexToolStripMenuItem.Size = new Size(184, 22);
            showValueAsHexToolStripMenuItem.Text = "Show value as Hex";
            showValueAsHexToolStripMenuItem.Click += ShowValueAsHexToolStripMenuItem_Click;
            // 
            // showValueAsBinaryToolStripMenuItem
            // 
            showValueAsBinaryToolStripMenuItem.Name = "showValueAsBinaryToolStripMenuItem";
            showValueAsBinaryToolStripMenuItem.Size = new Size(184, 22);
            showValueAsBinaryToolStripMenuItem.Text = "Show value as Binary";
            showValueAsBinaryToolStripMenuItem.Click += ShowValueAsBinaryToolStripMenuItem_Click;
            // 
            // showValueAsFloatToolStripMenuItem
            // 
            showValueAsFloatToolStripMenuItem.Name = "showValueAsFloatToolStripMenuItem";
            showValueAsFloatToolStripMenuItem.Size = new Size(184, 22);
            showValueAsFloatToolStripMenuItem.Text = "Show value as Float";
            showValueAsFloatToolStripMenuItem.Click += ShowValueAsFloatToolStripMenuItem_Click;
            // 
            // showValueAsASCIIToolStripMenuItem
            // 
            showValueAsASCIIToolStripMenuItem.Name = "showValueAsASCIIToolStripMenuItem";
            showValueAsASCIIToolStripMenuItem.Size = new Size(184, 22);
            showValueAsASCIIToolStripMenuItem.Text = "Show value as ASCII";
            showValueAsASCIIToolStripMenuItem.Click += ShowValueAsASCIIToolStripMenuItem_Click;
            // 
            // showValueAsStringToolStripMenuItem
            // 
            showValueAsStringToolStripMenuItem.Name = "showValueAsStringToolStripMenuItem";
            showValueAsStringToolStripMenuItem.Size = new Size(184, 22);
            showValueAsStringToolStripMenuItem.Text = "Show value as String";
            showValueAsStringToolStripMenuItem.Click += ShowValueAsStringToolStripMenuItem_Click;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(181, 6);
            // 
            // copyValueToolStripMenuItem
            // 
            copyValueToolStripMenuItem.Name = "copyValueToolStripMenuItem";
            copyValueToolStripMenuItem.Size = new Size(184, 22);
            copyValueToolStripMenuItem.Text = "Copy value";
            copyValueToolStripMenuItem.Click += CopyValueToolStripMenuItem_Click;
            // 
            // ContextMenuForRegColumn
            // 
            ContextMenuForRegColumn.ImageScalingSize = new Size(20, 20);
            ContextMenuForRegColumn.Items.AddRange(new ToolStripItem[] { toolStripMenuItem2, toolStripMenuItem3, toolStripMenuItem4, toolStripMenuItem5, toolStripMenuItem6, toolStripSeparator3, toolStripMenuItem7, expandAllToolStripMenuItem, collapseAllToolStripMenuItem });
            ContextMenuForRegColumn.Name = "ContextMenuForFirstColumns";
            ContextMenuForRegColumn.Size = new Size(240, 218);
            // 
            // toolStripMenuItem2
            // 
            toolStripMenuItem2.Image = (Image)resources.GetObject("toolStripMenuItem2.Image");
            toolStripMenuItem2.Name = "toolStripMenuItem2";
            toolStripMenuItem2.Size = new Size(239, 26);
            toolStripMenuItem2.Text = "Mark this line in red";
            toolStripMenuItem2.Click += Menu_SetColorRed;
            // 
            // toolStripMenuItem3
            // 
            toolStripMenuItem3.Image = (Image)resources.GetObject("toolStripMenuItem3.Image");
            toolStripMenuItem3.Name = "toolStripMenuItem3";
            toolStripMenuItem3.Size = new Size(239, 26);
            toolStripMenuItem3.Text = "Mark this line in green";
            toolStripMenuItem3.Click += Menu_SetColorGreen;
            // 
            // toolStripMenuItem4
            // 
            toolStripMenuItem4.Image = (Image)resources.GetObject("toolStripMenuItem4.Image");
            toolStripMenuItem4.Name = "toolStripMenuItem4";
            toolStripMenuItem4.Size = new Size(239, 26);
            toolStripMenuItem4.Text = "Mark this line in blue";
            toolStripMenuItem4.Click += Menu_SetColorBlue;
            // 
            // toolStripMenuItem5
            // 
            toolStripMenuItem5.Image = (Image)resources.GetObject("toolStripMenuItem5.Image");
            toolStripMenuItem5.Name = "toolStripMenuItem5";
            toolStripMenuItem5.Size = new Size(239, 26);
            toolStripMenuItem5.Text = "Mark this line in yellow";
            toolStripMenuItem5.Click += Menu_SetColorYellow;
            // 
            // toolStripMenuItem6
            // 
            toolStripMenuItem6.Image = (Image)resources.GetObject("toolStripMenuItem6.Image");
            toolStripMenuItem6.Name = "toolStripMenuItem6";
            toolStripMenuItem6.Size = new Size(239, 26);
            toolStripMenuItem6.Text = "Remove color mark of this line";
            toolStripMenuItem6.Click += Menu_SetColorDefault;
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Name = "toolStripSeparator3";
            toolStripSeparator3.Size = new Size(236, 6);
            // 
            // toolStripMenuItem7
            // 
            toolStripMenuItem7.Name = "toolStripMenuItem7";
            toolStripMenuItem7.Size = new Size(239, 26);
            toolStripMenuItem7.Text = "Copy grid line";
            toolStripMenuItem7.Click += Menu_CopyLine;
            // 
            // expandAllToolStripMenuItem
            // 
            expandAllToolStripMenuItem.Name = "expandAllToolStripMenuItem";
            expandAllToolStripMenuItem.Size = new Size(239, 26);
            expandAllToolStripMenuItem.Text = "Expand all";
            expandAllToolStripMenuItem.Click += expandAllToolStripMenuItem_Click;
            // 
            // collapseAllToolStripMenuItem
            // 
            collapseAllToolStripMenuItem.Name = "collapseAllToolStripMenuItem";
            collapseAllToolStripMenuItem.Size = new Size(239, 26);
            collapseAllToolStripMenuItem.Text = "Collapse all";
            collapseAllToolStripMenuItem.Click += collapseAllToolStripMenuItem_Click;
            // 
            // grid
            // 
            grid.AllowUserToAddRows = false;
            grid.AllowUserToDeleteRows = false;
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            grid.Columns.AddRange(new DataGridViewColumn[] { colReg, colDescr, colProps, colValue, colDebuginfo });
            grid.Dock = DockStyle.Fill;
            grid.EditMode = DataGridViewEditMode.EditProgrammatically;
            grid.Location = new Point(0, 0);
            grid.MultiSelect = false;
            grid.Name = "grid";
            grid.RowHeadersVisible = false;
            grid.RowHeadersWidth = 25;
            grid.RowTemplate.Height = 23;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grid.Size = new Size(669, 222);
            grid.TabIndex = 0;
            grid.CellBeginEdit += grid_CellBeginEdit;
            grid.CellDoubleClick += grid_CellDoubleClick;
            grid.CellEndEdit += grid_CellEndEdit;
            grid.CellFormatting += grid_CellFormatting;
            grid.CellMouseDown += grid_CellMouseDown;
            grid.CellPainting += grid_CellPainting;
            grid.CellToolTipTextNeeded += grid_CellToolTipTextNeeded;
            grid.ColumnWidthChanged += grid_ColumnWidthChanged;
            grid.EditingControlShowing += grid_EditingControlShowing;
            grid.RowPrePaint += grid_RowPrePaint;
            grid.KeyDown += grid_KeyDown;
            grid.KeyPress += grid_KeyPress;
            grid.MouseUp += grid_MouseUp;
            // 
            // colReg
            // 
            colReg.HeaderText = "Register";
            colReg.MinimumWidth = 6;
            colReg.Name = "colReg";
            colReg.ReadOnly = true;
            colReg.SortMode = DataGridViewColumnSortMode.NotSortable;
            // 
            // colDescr
            // 
            colDescr.FillWeight = 400F;
            colDescr.HeaderText = "Description";
            colDescr.MinimumWidth = 6;
            colDescr.Name = "colDescr";
            colDescr.ReadOnly = true;
            colDescr.SortMode = DataGridViewColumnSortMode.NotSortable;
            // 
            // colProps
            // 
            colProps.HeaderText = "Properties";
            colProps.MinimumWidth = 6;
            colProps.Name = "colProps";
            colProps.ReadOnly = true;
            colProps.SortMode = DataGridViewColumnSortMode.NotSortable;
            // 
            // colValue
            // 
            colValue.HeaderText = "Value";
            colValue.MinimumWidth = 6;
            colValue.Name = "colValue";
            // 
            // colDebuginfo
            // 
            colDebuginfo.FillWeight = 400F;
            colDebuginfo.HeaderText = "Debuginfo";
            colDebuginfo.MinimumWidth = 6;
            colDebuginfo.Name = "colDebuginfo";
            colDebuginfo.ReadOnly = true;
            colDebuginfo.SortMode = DataGridViewColumnSortMode.NotSortable;
            colDebuginfo.Visible = false;
            // 
            // ModbusGridView
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            Controls.Add(grid);
            Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Margin = new Padding(2);
            Name = "ModbusGridView";
            Size = new Size(669, 222);
            ContextMenuForOtherColumns.ResumeLayout(false);
            ContextMenuForValueColumn.ResumeLayout(false);
            ContextMenuForRegColumn.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)grid).EndInit();
            ResumeLayout(false);

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
