using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.ComponentModel.Design.Serialization;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Design;
using System.Resources;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace TreeGridViewSpace
{
    public class TreeGridView : DataGridView
    {
        //private int _indentWidth;
        private TreeGridNode _root;
        private TreeGridColumn _expandableColumn;
        private bool _inExpandCollapse = false;
        internal bool _inExpandCollapseMouseCapture = false;
        private Control hideScrollBarControl;
        private bool _showLines = true;
        private bool _virtualNodes = false;

        //internal VisualStyleRenderer rOpen = new VisualStyleRenderer(VisualStyleElement.TreeView.Glyph.Opened);
        //internal VisualStyleRenderer rClosed = new VisualStyleRenderer(VisualStyleElement.TreeView.Glyph.Closed);
        internal Bitmap rOpen;
        internal Bitmap rClosed; 

        #region Constructor
        public TreeGridView()
        {
            // Control when edit occurs because edit mode shouldn't start when expanding/collapsing
            this.EditMode = DataGridViewEditMode.EditProgrammatically;
            this.RowTemplate = new TreeGridNode() as DataGridViewRow;
            // This sample does not support adding or deleting rows by the user.
            this.AllowUserToAddRows = false;
            this.AllowUserToDeleteRows = false;
            this._root = new TreeGridNode(this);
            this._root.IsRoot = true;

            ResourceManager resourceManager = new ResourceManager("ModbusControlSpace.TreeGridView", System.Reflection.Assembly.GetExecutingAssembly());
            this.rOpen = (Bitmap)resourceManager.GetObject("TreeCollapse");
            this.rClosed = (Bitmap)resourceManager.GetObject("TreeExpand"); 

            // Ensures that all rows are added unshared by listening to the CollectionChanged event.
            base.Rows.CollectionChanged += delegate(object sender, System.ComponentModel.CollectionChangeEventArgs e) { };
        }
        #endregion

        #region Keyboard F2 to begin edit support
        protected override void OnKeyDown(KeyEventArgs e)
        {
            // Cause edit mode to begin since edit mode is disabled to support expanding/collapsing 
            base.OnKeyDown(e);
            // go edit?
            if (!e.Handled)
            {
                if (e.KeyCode == Keys.F2 && this.CurrentCellAddress.X > -1 && this.CurrentCellAddress.Y > -1)
                {
                    if (!this.CurrentCell.Displayed)
                    {
                        this.FirstDisplayedScrollingRowIndex = this.CurrentCellAddress.Y;
                    }
                    else
                    {
                        // TODO:calculate if the cell is partially offscreen and if so scroll into view
                    }
                    //this.SelectionMode = DataGridViewSelectionMode.CellSelect;
                    this.BeginEdit(true);
                }
                else if (e.KeyCode == Keys.Enter && !this.IsCurrentCellInEditMode)
                {
                    this.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    this.CurrentCell.OwningRow.Selected = true;
                }
            }
        }
        #endregion

        #region Shadow and hide DGV properties

        // This sample does not support databinding
        [Browsable(false),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
        EditorBrowsable(EditorBrowsableState.Never)]
        public new object DataSource
        {
            get { return null; }
            set { throw new NotSupportedException("The TreeGridView does not support databinding"); }
        }

        [Browsable(false),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
        EditorBrowsable(EditorBrowsableState.Never)]
        public new object DataMember
        {
            get { return null; }
            set { throw new NotSupportedException("The TreeGridView does not support databinding"); }
        }

        [Browsable(false),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
        EditorBrowsable(EditorBrowsableState.Never)]
        public new DataGridViewRowCollection Rows
        {
            get { return base.Rows; }
        }

        [Browsable(false),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
        EditorBrowsable(EditorBrowsableState.Never)]
        public new bool VirtualMode
        {
            get { return false; }
            set { throw new NotSupportedException("The TreeGridView does not support virtual mode"); }
        }

        // none of the rows/nodes created use the row template, so it is hidden.
        [Browsable(false),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
        EditorBrowsable(EditorBrowsableState.Never)]
        public new DataGridViewRow RowTemplate
        {
            get { return base.RowTemplate; }
            set { base.RowTemplate = value; }
        }

        #endregion

        #region Public methods
        [Description("Returns the TreeGridNode for the given DataGridViewRow")]
        public TreeGridNode GetNodeForRow(DataGridViewRow row)
        {
            return row as TreeGridNode;
        }

        [Description("Returns the TreeGridNode for the given DataGridViewRow")]
        public TreeGridNode GetNodeForRow(int index)
        {
            return GetNodeForRow(base.Rows[index]);
        }
        #endregion

        #region Public properties
        // The collection of root nodes in the treelist.
        public TreeGridNodeCollection Nodes
        {
            get
            {
                return this._root.Nodes;
            }
        }

        public new TreeGridNode CurrentRow
        {
            get
            {
                return base.CurrentRow as TreeGridNode;
            }
        }

        [DefaultValue(false),
        Description("Causes nodes to always show as expandable. Use the NodeExpanding event to add nodes.")]
        public bool VirtualNodes
        {
            get { return _virtualNodes; }
            set { _virtualNodes = value; }
        }

        public TreeGridNode CurrentNode
        {
            get
            {
                return this.CurrentRow;
            }
        }

        [DefaultValue(true)]
        public bool ShowLines
        {
            get { return this._showLines; }
            set
            {
                if (value != this._showLines)
                {
                    this._showLines = value;
                    this.Invalidate();
                }
            }
        }

        public new int RowCount
        {
            get { return this.Nodes.Count; }
            set
            {
                for (int i = 0; i < value; i++)
                    this.Nodes.Add(new TreeGridNode());

            }
        }
        #endregion

        #region Site nodes and collapse/expand support
        protected override void OnRowsAdded(DataGridViewRowsAddedEventArgs e)
        {
            base.OnRowsAdded(e);
            // Notify the row when it is added to the base grid 
            int count = e.RowCount - 1;
            TreeGridNode row;
            while (count >= 0)
            {
                row = base.Rows[e.RowIndex + count] as TreeGridNode;
                if (row != null)
                {
                    row.Sited();
                }
                count--;
            }
        }

        internal protected void UnSiteAll()
        {
            this.UnSiteNode(this._root);
        }

        internal protected virtual void UnSiteNode(TreeGridNode node)
        {
            if (node.IsSited || node.IsRoot)
            {
                // remove child rows first
                foreach (TreeGridNode childNode in node.Nodes)
                {
                    this.UnSiteNode(childNode);
                }

                // now remove this row except for the root
                if (!node.IsRoot)
                {
                    base.Rows.Remove(node);
                    // Row isn't sited in the grid anymore after remove. Note that we cannot
                    // Use the RowRemoved event since we cannot map from the row index to
                    // the index of the expandable row/node.
                    node.UnSited();
                }
            }
        }

        internal protected virtual bool CollapseNode(TreeGridNode node)
        {
            if (node.IsExpanded)
            {
                CollapsingEventArgs exp = new CollapsingEventArgs(node);
                this.OnNodeCollapsing(exp);

                if (!exp.Cancel)
                {
                    this.LockVerticalScrollBarUpdate(true);
                    this.SuspendLayout();
                    _inExpandCollapse = true;
                    node.IsExpanded = false;

                    foreach (TreeGridNode childNode in node.Nodes)
                    {
                        Debug.Assert(childNode.RowIndex != -1, "Row is NOT in the grid.");
                        this.UnSiteNode(childNode);
                    }

                    CollapsedEventArgs exped = new CollapsedEventArgs(node);
                    this.OnNodeCollapsed(exped);
                    //TODO: Convert this to a specific NodeCell property
                    _inExpandCollapse = false;
                    this.LockVerticalScrollBarUpdate(false);
                    this.ResumeLayout(true);
                    this.InvalidateCell(node.Cells[0]);
                }

                return !exp.Cancel;
            }
            else
            {
                // row isn't expanded, so we didn't do anything.				
                return false;
            }
        }

        internal protected virtual void SiteNode(TreeGridNode node)
        {
            //TODO: Raise exception if parent node is not the root or is not sited.
            int rowIndex = -1;
            TreeGridNode currentRow;
            node._grid = this;

            if (node.Parent != null && node.Parent.IsRoot == false)
            {
                // row is a child
                Debug.Assert(node.Parent != null && node.Parent.IsExpanded == true);

                if (node.Index > 0)
                {
                    currentRow = node.Parent.Nodes[node.Index - 1];
                }
                else
                {
                    currentRow = node.Parent;
                }
            }
            else
            {
                // row is being added to the root
                if (node.Index > 0)
                {
                    currentRow = node.Parent.Nodes[node.Index - 1];
                }
                else
                {
                    currentRow = null;
                }
            }

            if (currentRow != null)
            {
                while (currentRow.Level >= node.Level)
                {
                    if (currentRow.RowIndex < base.Rows.Count - 1)
                    {
                        currentRow = base.Rows[currentRow.RowIndex + 1] as TreeGridNode;
                        Debug.Assert(currentRow != null);
                    }
                    else
                        // no more rows, site this node at the end.
                        break;

                }
                if (currentRow == node.Parent)
                    rowIndex = currentRow.RowIndex + 1;
                else if (currentRow.Level < node.Level)
                    rowIndex = currentRow.RowIndex;
                else
                    rowIndex = currentRow.RowIndex + 1;
            }
            else
                rowIndex = 0;


            Debug.Assert(rowIndex != -1);
            this.SiteNode(node, rowIndex);

            Debug.Assert(node.IsSited);
            if (node.IsExpanded)
            {
                // add all child rows to display
                foreach (TreeGridNode childNode in node.Nodes)
                {
                    //TODO: could use the more efficient SiteRow with index.
                    this.SiteNode(childNode);
                }
            }
        }


        internal protected virtual void SiteNode(TreeGridNode node, int index)
        {
            // show a new line in the grid, insert somewhere or add to the end

            if (index < base.Rows.Count)
            {
                base.Rows.Insert(index, node);
            }
            else
            {
                base.Rows.Add(node);
            }

            // fix the new row's height because it does not automatically take RowTemplate.Height
            if (base.Rows[index].Height != RowTemplate.Height)
            {
                base.Rows[index].Height = RowTemplate.Height;
            }
        }

        internal protected virtual bool ExpandNode(TreeGridNode node)
        {
            if (!node.IsExpanded || this._virtualNodes)
            {
                ExpandingEventArgs exp = new ExpandingEventArgs(node);
                this.OnNodeExpanding(exp);

                if (!exp.Cancel)
                {
                    this.LockVerticalScrollBarUpdate(true);
                    this.SuspendLayout();
                    _inExpandCollapse = true;
                    node.IsExpanded = true;

                    //TODO Convert this to a InsertRange
                    foreach (TreeGridNode childNode in node.Nodes)
                    {
                        Debug.Assert(childNode.RowIndex == -1, "Row is already in the grid.");

                        this.SiteNode(childNode);
                    }

                    ExpandedEventArgs exped = new ExpandedEventArgs(node);
                    this.OnNodeExpanded(exped);
                    //TODO: Convert this to a specific NodeCell property
                    _inExpandCollapse = false;
                    this.LockVerticalScrollBarUpdate(false);
                    this.ResumeLayout(true);
                    this.InvalidateCell(node.Cells[0]);
                }

                return !exp.Cancel;
            }
            else
            {
                // row is already expanded, so we didn't do anything.
                return false;
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            // used to keep extra mouse moves from selecting more rows when collapsing
            base.OnMouseUp(e);
            this._inExpandCollapseMouseCapture = false;
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            // while we are expanding and collapsing a node mouse moves are
            // supressed to keep selections from being messed up.
            if (!this._inExpandCollapseMouseCapture)
                base.OnMouseMove(e);

        }
        #endregion

        #region Collapse/Expand events
        public event ExpandingEventHandler NodeExpanding;
        public event ExpandedEventHandler NodeExpanded;
        public event CollapsingEventHandler NodeCollapsing;
        public event CollapsedEventHandler NodeCollapsed;

        protected virtual void OnNodeExpanding(ExpandingEventArgs e)
        {
            if (this.NodeExpanding != null)
            {
                NodeExpanding(this, e);
            }
        }
        
        protected virtual void OnNodeExpanded(ExpandedEventArgs e)
        {
            if (this.NodeExpanded != null)
            {
                NodeExpanded(this, e);
            }
        }
        
        protected virtual void OnNodeCollapsing(CollapsingEventArgs e)
        {
            if (this.NodeCollapsing != null)
            {
                NodeCollapsing(this, e);
            }

        }
        
        protected virtual void OnNodeCollapsed(CollapsedEventArgs e)
        {
            if (this.NodeCollapsed != null)
            {
                NodeCollapsed(this, e);
            }
        }
        #endregion

        #region Helper methods
        protected override void Dispose(bool disposing)
        {
            base.Dispose(Disposing);
            this.UnSiteAll();
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);

            // this control is used to temporarly hide the vertical scroll bar
            hideScrollBarControl = new Control();
            hideScrollBarControl.Visible = false;
            hideScrollBarControl.Enabled = false;
            hideScrollBarControl.TabStop = false;
            // control is disposed automatically when the grid is disposed
            this.Controls.Add(hideScrollBarControl);
        }

        protected override void OnRowEnter(DataGridViewCellEventArgs e)
        {
            // ensure full row select
            base.OnRowEnter(e);
            if (this.SelectionMode == DataGridViewSelectionMode.CellSelect ||
                (this.SelectionMode == DataGridViewSelectionMode.FullRowSelect &&
                base.Rows[e.RowIndex].Selected == false))
            {
                this.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                base.Rows[e.RowIndex].Selected = true;
            }
        }

        private void LockVerticalScrollBarUpdate(bool lockUpdate/*, bool delayed*/)
        {
            // Temporarly hide/show the vertical scroll bar by changing its parent
            if (!this._inExpandCollapse)
            {
                if (lockUpdate)
                {
                    this.VerticalScrollBar.Parent = hideScrollBarControl;
                }
                else
                {
                    this.VerticalScrollBar.Parent = this;
                }
            }
        }

        protected override void OnColumnAdded(DataGridViewColumnEventArgs e)
        {
            if (typeof(TreeGridColumn).IsAssignableFrom(e.Column.GetType()))
            {
                if (_expandableColumn == null)
                {
                    // identify the expanding column.			
                    _expandableColumn = (TreeGridColumn)e.Column;
                }
                else
                {
                    // this.Columns.Remove(e.Column);
                    //throw new InvalidOperationException("Only one TreeGridColumn per TreeGridView is supported.");
                }
            }

            // Expandable Grid doesn't support sorting. This is just a limitation of the sample.
            e.Column.SortMode = DataGridViewColumnSortMode.NotSortable;

            base.OnColumnAdded(e);
        }

        private static class Win32Helper
        {
            public const int WM_SYSKEYDOWN = 0x0104,
                             WM_KEYDOWN = 0x0100,
                             WM_SETREDRAW = 0x000B;

            [System.Runtime.InteropServices.DllImport("USER32.DLL", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
            public static extern IntPtr SendMessage(System.Runtime.InteropServices.HandleRef hWnd, int msg, IntPtr wParam, IntPtr lParam);

            [System.Runtime.InteropServices.DllImport("USER32.DLL", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
            public static extern IntPtr SendMessage(System.Runtime.InteropServices.HandleRef hWnd, int msg, int wParam, int lParam);

            [System.Runtime.InteropServices.DllImport("USER32.DLL", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
            public static extern bool PostMessage(System.Runtime.InteropServices.HandleRef hwnd, int msg, IntPtr wparam, IntPtr lparam);

        }
        #endregion


    }

    // ------------------

    /// <summary>
	/// Summary description for TreeGridCell.
	/// </summary>
	public class TreeGridCell:DataGridViewTextBoxCell
	{
		private const int INDENT_WIDTH = 10;
		private const int INDENT_MARGIN = 4;
		private int glyphWidth;
		private int calculatedLeftPadding;
		internal bool IsSited;
		private Padding _previousPadding;
		//private Rectangle _lastKnownGlyphRect;

		public TreeGridCell()
		{			
			glyphWidth = 15;
			calculatedLeftPadding = 0;
			this.IsSited = false;
		}

        public override object Clone()
        {
			TreeGridCell c = (TreeGridCell)base.Clone();
			
            c.glyphWidth = this.glyphWidth;
            c.calculatedLeftPadding = this.calculatedLeftPadding;

            return c;
        }

		internal protected virtual void UnSited()
		{
			// The row this cell is in is being removed from the grid.
			this.IsSited = false;
			this.Style.Padding = this._previousPadding;
		}

		internal protected virtual void Sited()
		{
			// when we are added to the DGV we can realize our style
			this.IsSited = true;

			// remember what the previous padding size is so it can be restored when unsiting
			this._previousPadding = this.Style.Padding;

			this.UpdateStyle();
		}		

		internal protected virtual void UpdateStyle()
		{
			// styles shouldn't be modified when we are not sited.
			if (this.IsSited == false) return;

			int level = this.Level;

			Padding p = this._previousPadding;
			Size preferredSize;

			using (Graphics g = this.OwningNode._grid.CreateGraphics() ) {
				preferredSize =this.GetPreferredSize(g, this.InheritedStyle, this.RowIndex, new Size(0, 0));
			}

			this.Style.Padding = new Padding(p.Left + (level * INDENT_WIDTH) + INDENT_MARGIN,
												 p.Top , p.Right, p.Bottom );

			calculatedLeftPadding = ((level - 1) * glyphWidth) + INDENT_MARGIN;
		}

		public int Level
		{
			get
			{
				TreeGridNode row = this.OwningNode;
				if (row != null)
				{
					return row.Level;
				}
				else
					return -1;
			}
		}

		protected virtual int GlyphMargin
		{
			get
			{
				return ((this.Level - 1) * INDENT_WIDTH) + INDENT_MARGIN;
			}
		}

		protected virtual int GlyphOffset
		{
			get
			{
				return ((this.Level - 1) * INDENT_WIDTH);
			}
		}

		protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
		{

            TreeGridNode node = this.OwningNode;
            if (node == null) return;

			// paint the cell normally
			base.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts);

            // get glyph size
			Rectangle glyphRect = new Rectangle(cellBounds.X + this.GlyphMargin, cellBounds.Y, INDENT_WIDTH, cellBounds.Height - 1);

			//TODO: This painting code needs to be rehashed to be cleaner
			int level = this.Level;

			// Paint tree lines			
            if (node._grid.ShowLines)
            {
                using (Pen linePen = new Pen(SystemBrushes.ControlDark, 1.0f))
                {
                    linePen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
                    bool isLastSibling = node.IsLastSibling;
                    bool isFirstSibling = node.IsFirstSibling;
                    if (node.Level >= 2)
                    {
                        if (isLastSibling)
                        {
                            // last sibling doesn't draw the line extended below. Paint horizontal then vertical
                            graphics.DrawLine(linePen, glyphRect.X + 4, cellBounds.Top + cellBounds.Height / 2, glyphRect.Right, cellBounds.Top + cellBounds.Height / 2);
                            graphics.DrawLine(linePen, glyphRect.X + 4, cellBounds.Top, glyphRect.X + 4, cellBounds.Top + cellBounds.Height / 2);
                        }
                        else
                        {
                            // normal drawing draws extended from top to bottom. Paint horizontal then vertical
                            graphics.DrawLine(linePen, glyphRect.X + 4, cellBounds.Top + cellBounds.Height / 2, glyphRect.Right, cellBounds.Top + cellBounds.Height / 2);
                            graphics.DrawLine(linePen, glyphRect.X + 4, cellBounds.Top, glyphRect.X + 4, cellBounds.Bottom);
                        }

                        // paint lines of previous levels to the root
                        TreeGridNode previousNode = node.Parent;
                        int horizontalStop = (glyphRect.X + 4) - INDENT_WIDTH;

                        while ((!previousNode.IsRoot) && (previousNode.Level >= 2))
                        {
                            if (previousNode.HasChildren && !previousNode.IsLastSibling)
                            {
                                // paint vertical line
                                graphics.DrawLine(linePen, horizontalStop, cellBounds.Top, horizontalStop, cellBounds.Bottom);
                            }
                            previousNode = previousNode.Parent;
                            horizontalStop = horizontalStop - INDENT_WIDTH;
                        }
                    }
                }
            }

            if (node.HasChildren || node._grid.VirtualNodes)
            {
                // Paint node glyphs				
                if (node.IsExpanded)
                {
                    //node._grid.rOpen.DrawBackground(graphics, new Rectangle(glyphRect.X, glyphRect.Y + (glyphRect.Height / 2) - 4, 10, 10));
                    graphics.DrawImage(node._grid.rOpen, new Point(glyphRect.X, glyphRect.Y + (glyphRect.Height / 2) - 4)); 
                }
                else
                {
                    //node._grid.rClosed.DrawBackground(graphics, new Rectangle(glyphRect.X, glyphRect.Y + (glyphRect.Height / 2) - 4, 10, 10));
                    graphics.DrawImage(node._grid.rClosed, new Point(glyphRect.X, glyphRect.Y + (glyphRect.Height / 2) - 4)); 
                }
            }
		}
        protected override void OnMouseUp(DataGridViewCellMouseEventArgs e)
        {
            base.OnMouseUp(e);

            TreeGridNode node = this.OwningNode;
            if (node != null)
                node._grid._inExpandCollapseMouseCapture = false;
        }
        
		protected override void OnMouseDown(DataGridViewCellMouseEventArgs e)
		{
			if (e.Location.X > this.InheritedStyle.Padding.Left)
			{
				base.OnMouseDown(e);
			}
			else
			{
				// Expand the node
				//TODO: Calculate more precise location
				TreeGridNode node = this.OwningNode;
                if (node != null)
				{
                    node._grid._inExpandCollapseMouseCapture = true;
                    if (node.IsExpanded)
                        node.Collapse();
					else
                        node.Expand();
                }
			}
		}
		public TreeGridNode OwningNode
		{
			get { return base.OwningRow as TreeGridNode; }
		}
	}

	public class TreeGridColumn : DataGridViewTextBoxColumn
	{
		public TreeGridColumn()
		{		
			this.CellTemplate = new TreeGridCell();
		}

		// Need to override Clone for design-time support.
		public override object Clone()
		{
			TreeGridColumn c = (TreeGridColumn)base.Clone();
			return c;
		}
	}

    // --------------

	public class TreeGridNodeCollection : System.Collections.Generic.IList<TreeGridNode>, System.Collections.IList
	{
		internal System.Collections.Generic.List<TreeGridNode> _list;
		internal TreeGridNode _owner;

		internal TreeGridNodeCollection(TreeGridNode owner)
		{
			this._owner = owner;
			this._list = new List<TreeGridNode>();
		}

		#region Public Members
		public void Add(TreeGridNode item)
		{
			// The row needs to exist in the child collection before the parent is notified.
			item._grid = this._owner._grid;

            bool hadChildren = this._owner.HasChildren;
			item._owner = this;

			this._list.Add(item);

			this._owner.AddChildNode(item);

            // if the owner didn't have children but now does (asserted) and it is sited update it
            if (!hadChildren && this._owner.IsSited)
            {
                this._owner._grid.InvalidateRow(this._owner.RowIndex);
            }
		}

        public TreeGridNode Add(string text)
        {
            TreeGridNode node = new TreeGridNode();
            this.Add(node);

            node.Cells[0].Value = text;
            return node;
        }

        public TreeGridNode Add(params object[] values)
        {
            TreeGridNode node = new TreeGridNode();
            this.Add(node);

            int cell = 0;

            if (values.Length > node.Cells.Count )
                throw new ArgumentOutOfRangeException("values");

            foreach (object o in values)
            {
                node.Cells[cell].Value = o;
                cell++;
            }

            return node;
        }

        public TreeGridNode SetForeColor(TreeGridNode node, Color color)
        {
            for (int i = 0; i < node.Cells.Count; ++i)
            {
                node.Cells[i].Style.ForeColor = color;
            }

            return node;
        }

        public TreeGridNode SetBackColor(TreeGridNode node, Color color)
        {
            for (int i = 0; i < node.Cells.Count; ++i)
            {
                node.Cells[i].Style.BackColor = color;
            }
            
            return node;
        }

        public void Expand(TreeGridNode node)
        {
            node.Expand();
        }

        public void Insert(int index, TreeGridNode item)
        {
            // The row needs to exist in the child collection before the parent is notified.
            item._grid = this._owner._grid;
            item._owner = this;

            this._list.Insert(index, item);

            this._owner.InsertChildNode(index, item);
        }

        public bool Remove(TreeGridNode item)
		{
			// The parent is notified first then the row is removed from the child collection.
			this._owner.RemoveChildNode(item);
			item._grid = null;
			return this._list.Remove(item);
		}

        public void RemoveAt(int index)
		{
			TreeGridNode row = this._list[index];

			// The parent is notified first then the row is removed from the child collection.
			this._owner.RemoveChildNode(row);
			row._grid = null;
			this._list.RemoveAt(index);
		}

        public void Clear()
		{
			// The parent is notified first then the row is removed from the child collection.
			this._owner.ClearNodes();
			this._list.Clear();
		}

        public int IndexOf(TreeGridNode item)
        {
            return this._list.IndexOf(item);
        }

		public TreeGridNode this[int index]
		{
			get
			{
				return this._list[index];
			}
			set
			{
				throw new Exception("The method or operation is not implemented.");
			}
		}

		public bool Contains(TreeGridNode item)
		{
			return this._list.Contains(item);
		}

		public void CopyTo(TreeGridNode[] array, int arrayIndex)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		public int Count
		{
			get{ return this._list.Count; }
		}

        public bool IsReadOnly
		{
			get{ return false; }
		}
        #endregion

        #region IList Interface
        void System.Collections.IList.Remove(object value)
        {
            this.Remove(value as TreeGridNode);
        }


        int System.Collections.IList.Add(object value)
        {
            TreeGridNode item = value as TreeGridNode;
            this.Add(item);
            return item.Index;
        }

        void System.Collections.IList.RemoveAt(int index)
        {
            this.RemoveAt(index);
        }


        void System.Collections.IList.Clear()
        {
            this.Clear();
        }

        bool System.Collections.IList.IsReadOnly
		{
			get { return this.IsReadOnly;}
		}

		bool System.Collections.IList.IsFixedSize
		{
			get { return false; }
		}

        int System.Collections.IList.IndexOf(object item)
        {
            return this.IndexOf(item as TreeGridNode);
        }

        void System.Collections.IList.Insert(int index, object value)
        {
            this.Insert(index, value as TreeGridNode);
        }
        int System.Collections.ICollection.Count
        {
            get { return this.Count; }
        }
        bool System.Collections.IList.Contains(object value)
        {
            return this.Contains(value as TreeGridNode);
        }
        void System.Collections.ICollection.CopyTo(Array array, int index)
        {
            throw new Exception("The method or operation is not implemented.");
        }
        object System.Collections.IList.this[int index]
        {
            get
            {
                return this[index];
            }
            set
            {
                throw new Exception("The method or operation is not implemented.");
            }
        }



		#region IEnumerable<ExpandableRow> Members

		public IEnumerator<TreeGridNode> GetEnumerator()
		{
			return this._list.GetEnumerator();
		}

		#endregion


		#region IEnumerable Members

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		#endregion
		#endregion

		#region ICollection Members

		bool System.Collections.ICollection.IsSynchronized
		{
			get { throw new Exception("The method or operation is not implemented."); }
		}

		object System.Collections.ICollection.SyncRoot
		{
			get { throw new Exception("The method or operation is not implemented."); }
		}

		#endregion
	}

    // -------------------

	[
		ToolboxItem(false),
		DesignTimeVisible(false)
	]
    public class TreeGridNode : DataGridViewRow//, IComponent
    {
		internal TreeGridView _grid;
		internal TreeGridNode _parent;
		internal TreeGridNodeCollection _owner;
        internal bool IsExpanded;
		internal bool IsRoot;
		internal bool _isSited;
		internal bool _isFirstSibling;
		internal bool _isLastSibling;

		private Random rndSeed = new Random();
		public int UniqueValue = -1;
        TreeGridCell _treeCell;
        TreeGridNodeCollection childrenNodes;

		private int _index;
		private int _level;
		private bool childCellsCreated = false;

		// needed for IComponent
		private ISite site = null;
		private EventHandler disposed = null;

		internal TreeGridNode(TreeGridView owner)
			: this()
		{
			this._grid = owner;
			this.IsExpanded = true;
		}

        public TreeGridNode()
        {            
			_index = -1;
			_level = -1;            
            IsExpanded = false;
			UniqueValue = this.rndSeed.Next();
			_isSited = false;
			_isFirstSibling = false;
			_isLastSibling = false;
		}

		public override object Clone()
		{
			TreeGridNode r = (TreeGridNode)base.Clone();
			r.UniqueValue = -1;
			r._level = this._level;
			r._grid = this._grid;
			r._parent = this.Parent;

			r.IsExpanded = this.IsExpanded;
			//r.treeCell = new TreeGridCell();

			return r;
		}
		
		internal protected virtual void UnSited()
		{
			// This row is being removed from being displayed on the grid.
			TreeGridCell cell;
			foreach (DataGridViewCell DGVcell in this.Cells)
			{
				cell = DGVcell as TreeGridCell;
				if (cell != null)
				{
					cell.UnSited();
				}
			}
			this._isSited = false;
		}

		internal protected virtual void Sited()
		{
			// This row is being added to the grid.
			this._isSited = true;
			this.childCellsCreated = true;
			Debug.Assert(this._grid != null);

			TreeGridCell cell;
			foreach (DataGridViewCell DGVcell in this.Cells)
			{
				cell = DGVcell as TreeGridCell;
				if (cell != null)
				{
					cell.Sited();// Level = this.Level;
				}
			}

		}

		// Represents the index of this row in the Grid
		[System.ComponentModel.Description("Represents the index of this row in the Grid. Advanced usage."),
		System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced),
		 Browsable(false),
		 DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int RowIndex{
			get{
				return base.Index;
			}
		}

		// Represents the index of this row based upon its position in the collection.
		[Browsable(false),
		 DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new int Index
		{
			get
			{
				if (_index == -1)
				{
					// get the index from the collection if unknown
					_index = this._owner.IndexOf(this);
				}

				return _index;
			}
			internal set
			{
				_index = value;
			}
		}

		protected override DataGridViewCellCollection CreateCellsInstance()
		{
			DataGridViewCellCollection cells = base.CreateCellsInstance();
			cells.CollectionChanged += cells_CollectionChanged;
			return cells;
		}

		void cells_CollectionChanged(object sender, System.ComponentModel.CollectionChangeEventArgs e)
		{
			// Exit if there already is a tree cell for this row
			if (_treeCell != null) return;

			if (e.Action == System.ComponentModel.CollectionChangeAction.Add || e.Action == System.ComponentModel.CollectionChangeAction.Refresh)
			{
				TreeGridCell treeCell = null;

				if (e.Element == null)
				{
					foreach (DataGridViewCell cell in base.Cells)
					{
						if (cell.GetType().IsAssignableFrom(typeof(TreeGridCell)))
						{
							treeCell = (TreeGridCell)cell;
							break;
						}

					}
				}
				else
				{
					treeCell = e.Element as TreeGridCell;
				}

				if (treeCell != null) 
				  _treeCell = treeCell;
			}
		}

		// The collection of root nodes in the treelist.
        public TreeGridNodeCollection Nodes
        {
            get
            {
                if (childrenNodes == null)
                {
                    childrenNodes = new TreeGridNodeCollection(this);
                }
                return childrenNodes;
            }
            set { ;}
        }

		// Create a new Cell property because by default a row is not in the grid and won't
		// have any cells. We have to fabricate the cell collection ourself.
        [Browsable(false),
         DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new DataGridViewCellCollection Cells
		{
			get
			{
				if (!childCellsCreated && this.DataGridView == null)
				{
                    if (this._grid == null) return null;

					this.CreateCells(this._grid);
					childCellsCreated = true;
				}
				return base.Cells;
			}
		}

		[Browsable(false),
		 DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int Level
		{
			get {
				if (this._level == -1)
				{
					// calculate level
					int walk = 0;
					TreeGridNode walkRow = this.Parent;
					while (walkRow != null)
					{
						walk++;
						walkRow = walkRow.Parent;
					}
					this._level = walk;
				}
				return this._level; }
		}

		[Browsable(false),
		 DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public TreeGridNode Parent
		{
			get
			{
				return this._parent;
			}
		}

		[Browsable(false),
		 DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual bool HasChildren
		{
			get
			{
				return (this.childrenNodes != null && this.Nodes.Count != 0);
			}
		}

        [Browsable(false)]
        public bool IsSited
        {
            get
            {
                return this._isSited;
            }
        }
		[Browsable(false)]
		public bool IsFirstSibling
		{
			get
			{
				return (this.Index == 0);
			}
		}

		[Browsable(false)]
		public bool IsLastSibling
		{
			get
			{
				TreeGridNode parent = this.Parent;
				if (parent != null && parent.HasChildren)
				{
					return (this.Index == parent.Nodes.Count - 1);
				}
				else
					return true;
			}
		}
		
		public virtual bool Collapse()
		{
			return this._grid.CollapseNode(this);
		}

		public virtual bool Expand()
		{
			if (this._grid != null)
				return this._grid.ExpandNode(this);
			else
			{
				this.IsExpanded = true;
				return true;
			}
		}

		internal protected virtual bool InsertChildNode(int index, TreeGridNode node)
		{
			node._parent = this;
			node._grid = this._grid;

            // ensure that all children of this node has their grid set
            if (this._grid != null)
                UpdateChildNodes(node);

			//TODO: do we need to use index parameter?
			if ((this._isSited || this.IsRoot) && this.IsExpanded)
				this._grid.SiteNode(node);
			return true;
		}

		internal protected virtual bool InsertChildNodes(int index, params TreeGridNode[] nodes)
		{
			foreach (TreeGridNode node in nodes)
			{
				this.InsertChildNode(index, node);
			}
			return true;
		}

		internal protected virtual bool AddChildNode(TreeGridNode node)
		{
			node._parent = this;
			node._grid = this._grid;

            // ensure that all children of this node has their grid set
            if (this._grid != null)
                UpdateChildNodes(node);

			if ((this._isSited || this.IsRoot) && this.IsExpanded && !node._isSited)
				this._grid.SiteNode(node);

			return true;
		}

		internal protected virtual bool AddChildNodes(params TreeGridNode[] nodes)
		{
			//TODO: Convert the final call into an SiteNodes?
			foreach (TreeGridNode node in nodes)
			{
				this.AddChildNode(node);
			}
			return true;

		}

		internal protected virtual bool RemoveChildNode(TreeGridNode node)
		{
			if ((this.IsRoot || this._isSited) && this.IsExpanded )
			{
				//We only unsite out child node if we are sited and expanded.
				this._grid.UnSiteNode(node);
			
			}
            node._grid = null;	
			node._parent = null;
			return true;

		}

		internal protected virtual bool ClearNodes()
		{
            if (this.HasChildren)
            {
                for (int i = this.Nodes.Count - 1; i >= 0; i--)
                {
                    this.Nodes.RemoveAt(i);
                }
            }
			return true;
		}

        [
            Browsable(false),
            EditorBrowsable(EditorBrowsableState.Advanced)
        ]
        public event EventHandler Disposed
        {
            add
            {
                this.disposed += value;
            }
            remove
            {
                this.disposed -= value;
            }
        }

		[
			Browsable(false),
			DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)
		]
		public ISite Site
		{
			get
			{
				return this.site;
			}
			set
			{
				this.site = value;
			}
		}

        private void UpdateChildNodes(TreeGridNode node)
        {
            if (node.HasChildren)
            {
                foreach (TreeGridNode childNode in node.Nodes)
                {
                    childNode._grid = node._grid;
                    this.UpdateChildNodes(childNode);
                }
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder(36);
            sb.Append("TreeGridNode { Index=");
            sb.Append(this.RowIndex.ToString(System.Globalization.CultureInfo.CurrentCulture));
            sb.Append(" }");
            return sb.ToString();
        }

		//protected override void Dispose(bool disposing) {
		//    if (disposing)
		//    {
		//        lock(this)
		//        {
		//            if (this.site != null && this.site.Container != null)
		//            {
		//                this.site.Container.Remove(this);
		//            }

		//            if (this.disposed != null)
		//            {
		//                this.disposed(this, EventArgs.Empty);
		//            }
		//        }
		//    }

		//    base.Dispose(disposing);
		//}
	}

    // -------------

	public class TreeGridNodeEventBase
	{
		private TreeGridNode _node;

		public TreeGridNodeEventBase(TreeGridNode node)
		{
			this._node = node;
		}

		public TreeGridNode Node
		{
			get { return _node; }
		}
	}
	public class CollapsingEventArgs : System.ComponentModel.CancelEventArgs
	{
		private TreeGridNode _node;

		private CollapsingEventArgs() { }
		public CollapsingEventArgs(TreeGridNode node)
			: base()
		{
			this._node = node;
		}
		public TreeGridNode Node
		{
			get { return _node; }
		}

	}
	public class CollapsedEventArgs : TreeGridNodeEventBase
	{
		public CollapsedEventArgs(TreeGridNode node)
			: base(node)
		{
		}
	}

	public class ExpandingEventArgs:System.ComponentModel.CancelEventArgs
	{
		private TreeGridNode _node;

		private ExpandingEventArgs() { }
		public ExpandingEventArgs(TreeGridNode node):base()
		{
			this._node = node;
		}
		public TreeGridNode Node
		{
			get { return _node; }
		}

	}
	public class ExpandedEventArgs : TreeGridNodeEventBase
	{
		public ExpandedEventArgs(TreeGridNode node):base(node)
		{
		}
	}

	public delegate void ExpandingEventHandler(object sender, ExpandingEventArgs e);
	public delegate void ExpandedEventHandler(object sender, ExpandedEventArgs e);

	public delegate void CollapsingEventHandler(object sender, CollapsingEventArgs e);
	public delegate void CollapsedEventHandler(object sender, CollapsedEventArgs e);

}

