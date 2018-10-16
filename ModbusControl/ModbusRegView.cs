using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using TreeGridViewSpace;
using System.Runtime.InteropServices;

using LST.Helpers;


namespace ModbusRegViewSpace
{
    public class ModbusRegView : TreeGridView
    {
        [DllImport("user32.dll")]
        static extern bool PostMessage(IntPtr hWnd, uint Msg, int wParam, int lParam);
        private const int WM_CHAR = 0x0102;

        private static bool mIsReadOnly = false;
        private static bool mNextRowOnEnter = false;
        private static bool mStartEditOnAnyCharacter = false;
        private bool mShowInfo = false;
        private char BufferedChar = (char)0;

        // properties

        public static bool IsReadOnly
        {
            get { return mIsReadOnly; }
            set { mIsReadOnly = value; }
        }

        public static bool NextRowOnEnter
        {
            get { return mNextRowOnEnter; }
            set { mNextRowOnEnter = value; }
        }

        public static bool StartEditOnAnyCharacter
        {
            get { return mStartEditOnAnyCharacter; }
            set { mStartEditOnAnyCharacter = value; }
        }

        public bool showInfo
        {
            get { return mShowInfo; }
            set
            {
                if (value != mShowInfo)
                {
                    mShowInfo = value;
                    UpdateDescriptionInfo();
                }
            }
        }

        private ContextMenuStrip ContextMenuForFirstColumns = new ContextMenuStrip();
        private ContextMenu ContextMenuForValueColumn = new ContextMenu();

        public delegate void FindHandler(object sender);
        public event FindHandler OnFind;
        public delegate void ZoomHandler(object sender, int Action);
        public event ZoomHandler OnZoom;

        public delegate void ReportErrorMessageHandler( string s);
        public event ReportErrorMessageHandler OnReportErrorMessage;
        
        // per register
        public class RegEntry
        {
            public UInt32 Reg;          // universal register number 
            public int Size;            // var size in bits, usually 16 or 32
            public string Text;         // description
            public ModbusControlSpace.Global.eFormat Format;      // display as dec, unsigned, hex, binary
            public int MinLen;          // minimum display length
            public int Decimals;        // nr of decimals to show
            public bool Readable;       // readable?
            public bool Writeable;      // writable?
            public List<BitfieldEntry> ListOfBitfields; // list of bitfields
            public Color ForeColor;     // the foreground/font color
            public Color BackColor;     // the background color
            public TreeGridNode node;   // TreeGridView node tree
            public Int16[] RawReg;      // raw 16-bit register values
            public Int32 Value32;       // actual value in 32-bit
            public string ValueStr;     // value as a string

            // constructor
            public RegEntry()
            {
                Reg = 0;
                Size = 16;
                Text = "";
                Format = ModbusControlSpace.Global.eFormat.Int;
                MinLen = 1;
                Decimals = 0;
                Readable = false;
                Writeable = false;
                ListOfBitfields = null;
                node = null;
                RawReg = new Int16[2];
                Value32 = 0;
                ValueStr = String.Empty;
            }
        };

        // structure holding a bitfield
        public class BitfieldEntry
        {
            public int Size;            // number of bits
            public int Position;        // number of lowest bit
            public string Text;         // description
            public ModbusControlSpace.Global.eFormat Format;      // display as dec, unsigned, hex, binary
            public int MinLen;          // minimum display length
            public Color ForeColor;     // the foreground/font color
            public Color BackColor;     // the background color
            public Int32 Value;         // actual value
            public TreeGridNode node;   // TreeGridView node tree
        }

        // list of all our registers
        List<RegEntry> ListOfReg = new List<RegEntry>();

        private RegEntry CurrentEditReg;
        private int CurrentEditRowIndex;
        private int CurrentEditBitfieldIndex;
        private ModbusControlSpace.Global.eFormat CurrentEditFormat;
        private int CurrentEditDecimals;
        private bool CurrentEditIsCanceled;
        private float OriginalFontSize;
        private int OriginalRowHeight;

        internal ResourceManager resourceManager;
        internal Bitmap RedIcon, GreenIcon, BlueIcon, YellowIcon, WhiteIcon;

        #region events

        public event RegisterWriteHandler OnRegisterWrite;
        public delegate void RegisterWriteHandler(object sender, RegisterWriteEventArgs e);
        public class RegisterWriteEventArgs : EventArgs
        {
            public UInt32 Reg;
            public Int16[] Values;
        }

        public event BeepHandler OnBeep;
        public delegate void BeepHandler(object sender);

        #endregion

        #region Constructor
        public ModbusRegView()
        {
            this.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.AllowUserToAddRows = false;
            this.AllowUserToDeleteRows = false;
            this.AllowUserToOrderColumns = false;
            this.AllowUserToResizeRows = false;
            this.MultiSelect = false;
            this.RowHeadersVisible = false;
            this.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;

            TreeGridColumn RegisterCol = new TreeGridColumn();
            DataGridViewTextBoxColumn DescriptionCol = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn ValueCol = new DataGridViewTextBoxColumn();

            RegisterCol.HeaderText = "Register";
            RegisterCol.ReadOnly = true;
            RegisterCol.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            RegisterCol.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            RegisterCol.FillWeight = 46;
            DescriptionCol.HeaderText = "Description";
            DescriptionCol.ReadOnly = true;
            DescriptionCol.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            DescriptionCol.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            DescriptionCol.FillWeight = 200;
            ValueCol.HeaderText = "Value";
            ValueCol.ReadOnly = false;
            ValueCol.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            ValueCol.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            ValueCol.FillWeight = 70;

            OriginalFontSize = Font.Size;
            OriginalRowHeight = RowTemplate.Height;

            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Grid_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Grid_KeyPress);
            this.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Grid_CellDoubleClick);
            this.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.Grid_CellEditingControlShowing);
            this.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.Grid_CellBeginEdit);
            this.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.Grid_CellEndEdit);
            this.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.Grid_RowPrePaint);

            this.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { RegisterCol, DescriptionCol, ValueCol });

            // get resources for menu item icons
            resourceManager = new ResourceManager("ModbusControlSpace.TreeGridView", System.Reflection.Assembly.GetExecutingAssembly());
            RedIcon = (Bitmap)resourceManager.GetObject("RedIcon");
            GreenIcon = (Bitmap)resourceManager.GetObject("GreenIcon");
            BlueIcon = (Bitmap)resourceManager.GetObject("BlueIcon");
            YellowIcon = (Bitmap)resourceManager.GetObject("YellowIcon");
            WhiteIcon = (Bitmap)resourceManager.GetObject("WhiteIcon");

            // Create context menu for regsiter/description columns
            ContextMenuForFirstColumns.Items.Add("Mark this line in red", RedIcon, new EventHandler(Menu_SetColorRed));
            ContextMenuForFirstColumns.Items.Add("Mark this line in green", GreenIcon, new EventHandler(Menu_SetColorGreen));
            ContextMenuForFirstColumns.Items.Add("Mark this line in blue", BlueIcon, new EventHandler(Menu_SetColorBlue));
            ContextMenuForFirstColumns.Items.Add("Mark this line in yellow", YellowIcon, new EventHandler(Menu_SetColorYellow));
            ContextMenuForFirstColumns.Items.Add("Remove color mark of this line", WhiteIcon, new EventHandler(Menu_SetColorDefault));
            ContextMenuForFirstColumns.Items.Add("-");
            ContextMenuForFirstColumns.Items.Add("Copy grid line", null, new EventHandler(Menu_CopyLine));

            // Create context menu for value column
            ContextMenuForValueColumn.MenuItems.Add("Show value as Int", new EventHandler(Menu_SetFormatToInt));
            ContextMenuForValueColumn.MenuItems.Add("Show value as UInt", new EventHandler(Menu_SetFormatToUInt));
            ContextMenuForValueColumn.MenuItems.Add("Show value as Hex", new EventHandler(Menu_SetFormatToHex));
            ContextMenuForValueColumn.MenuItems.Add("Show value as Binary", new EventHandler(Menu_SetFormatToBin));
            ContextMenuForValueColumn.MenuItems.Add("Show value as Float", new EventHandler(Menu_SetFormatToFloat));
            ContextMenuForValueColumn.MenuItems.Add("Show value as ASCII", new EventHandler(Menu_SetFormatToASCII));
            ContextMenuForValueColumn.MenuItems.Add("-");
            ContextMenuForValueColumn.MenuItems.Add("Copy value", new EventHandler(Menu_CopyValue));

            this.MouseUp += new System.Windows.Forms.MouseEventHandler(Grid_MouseUp);
        }
        #endregion

        /* Reg = user register number: 40000-49999, 40001-49999, 400000-465535, 400001-465536
         * Text = Description text
         * Size = size of register: 16 or 32
         * Writeable = true if writeable, false if readonly
         */
        public RegEntry AddReg(UInt32 Reg, int Size, string Text, ModbusControlSpace.Global.eFormat Format, int MinLen, int Decimals, bool Readable, bool Writeable)
        {
            RegEntry Line = new RegEntry();

            Line.Reg = Reg;
            Line.Size = Size;
            Line.Text = Text;
            Line.Format = Format;
            Line.MinLen = MinLen;
            Line.Decimals = Decimals;
            Line.Readable = Readable;
            Line.Writeable = Writeable;
            Line.node = this.Nodes.Add(Reg, mShowInfo ? Text + BuildRegisterInfoStr(Line) : Text);

            ListOfReg.Add(Line);

            return Line;
        }

        public void SetRegForegroundColor(RegEntry r, Color color)
        {
            r.BackColor = color;
            this.Nodes.SetForeColor(r.node, color);
        }

        public void SetRegBackgroundColor(RegEntry r, Color color)
        {
            r.BackColor = color;
            this.Nodes.SetBackColor(r.node, color);
        }

        public BitfieldEntry AddRegBitfield(RegEntry r, int Position, int BitWidth, string Text, ModbusControlSpace.Global.eFormat Format, int MinLen)
        {
            Debug.Assert( (Position >= 0) && (Position <= 31));
            Debug.Assert( (BitWidth >= 1) && (BitWidth <= 32));

            if (r.ListOfBitfields == null)
            {
                r.ListOfBitfields = new List<BitfieldEntry>();
            }

            BitfieldEntry Field = new BitfieldEntry();
            Field.Size = BitWidth;
            Field.Position = Position;
            Field.Text = Text;
            Field.Format = Format;
            Field.MinLen = MinLen;

            string FieldBitsStr;
            if( BitWidth == 1)
            {
                FieldBitsStr = string.Format( "b{0}", Position); 
            }
            else
            {
                FieldBitsStr = string.Format("b{0}-b{1}", Position+BitWidth-1, Position);
            }
            TreeGridNode node = r.node.Nodes.Add(FieldBitsStr, mShowInfo ? Text + BuildBitfieldInfoStr(Field) : Text);

            r.ListOfBitfields.Add(Field);
            Field.node = node;

            return Field;
        }

        public void SetBitfieldForegroundColor(BitfieldEntry f, Color color)
        {
            f.ForeColor = color;
            this.Nodes.SetForeColor(f.node, color);
        }

        public void SetBitfieldBackgroundColor(BitfieldEntry f, Color color)
        {
            f.BackColor = color;
            this.Nodes.SetBackColor(f.node, color);
        }

        public void ExpandBitfields(RegEntry r)
        {
            this.Nodes.Expand( r.node);
        }

        public void SetRegText(int Index, string Text)
        {
            RegEntry r = ListOfReg[Index];

            r.node.Cells[1].Value = Text;
        }

        public void SetRegBitfieldText(int Index, int BitfieldIndex, string Text)
        {
            RegEntry r = ListOfReg[Index];

            r.node.Nodes[BitfieldIndex].Cells[1].Value = Text;
        }

        public void SetRegErrorString(int Index, string ErrorString)
        {
            RegEntry r = ListOfReg[Index];

            // parent reg: old value + error string
            r.node.Cells[2].Value = r.ValueStr + "  ?  <" + ErrorString + ">";
            // parent reg: color
            r.node.Cells[2].Style.ForeColor = Color.Red;

            // has bitfields? then update the bitfield values too
            if (r.ListOfBitfields != null)
            {
                for (int i = 0; i < r.ListOfBitfields.Count; ++i)
                {
                    // bitfield: old value + error string
                    r.node.Nodes[i].Cells[2].Value = r.ValueStr + "  ?  <" + ErrorString + ">";
                    // bitfield: color
                    r.node.Nodes[i].Cells[2].Style.ForeColor = Color.Red;
                }
            }
        }

        public void SetRegValue(RegEntry r)
        {
            // update the value shown in the grid
            r.ValueStr = CValueToString.ValueToString(r.Value32, r.Size, r.Format, r.Decimals, r.MinLen);
            r.node.Cells[2].Value = r.ValueStr;
            r.node.Cells[2].Style.ForeColor = Color.Black;

            // has bitfields? then update the bitfield values too
            if (r.ListOfBitfields != null)
            {
                for (int i = 0; i < r.ListOfBitfields.Count; ++i)
                {
                    BitfieldEntry bf = r.ListOfBitfields[i];
                    Int32 NewFieldValue = GetBitfieldValue(r.Value32, bf.Position, bf.Size, bf.Format == ModbusControlSpace.Global.eFormat.Int);
                    // store our value
                    bf.Value = NewFieldValue;
                    // update the value shown in the grid
                    r.node.Nodes[i].Cells[2].Value = CValueToString.ValueToString(NewFieldValue, bf.Size, bf.Format, 0, bf.MinLen);
                    r.node.Nodes[i].Cells[2].Style.ForeColor = Color.Black;
                }
            }
        }

        // convert format to type
        private Endians.eType ConvertFormatToType(ModbusControlSpace.Global.eFormat Format)
        {
            Endians.eType Type = Endians.eType.Int;

            switch (Format)
            {
                case ModbusControlSpace.Global.eFormat.Int:
                case ModbusControlSpace.Global.eFormat.UInt:
                case ModbusControlSpace.Global.eFormat.Hex:
                case ModbusControlSpace.Global.eFormat.Bin:
                    Type = Endians.eType.Int;
                    break;
                case ModbusControlSpace.Global.eFormat.Float:
                    Type = Endians.eType.Float;
                    break;
                case ModbusControlSpace.Global.eFormat.ASCII:
                    Type = Endians.eType.ASCII;
                    break;
                default:
                    // invalid
                    throw new ArgumentException("Invalid format enum!");
            }

            return Type;
        }
        
        internal void MarkColorAsEditedOfRegEntry(RegEntry r, int BitfieldIndex)
        {
            // mark the parent register as edited
            r.node.Cells[2].Style.ForeColor = Color.FromArgb(50, 50, 255);

            // edited a bitfield?
            if (BitfieldIndex >= 0)
            {
                // edited a bitfield
                // mark this bitfield as edited
                if (r.ListOfBitfields != null)
                {
                    // mark the bitfield as edited
                    r.node.Nodes[BitfieldIndex].Cells[2].Style.ForeColor = Color.FromArgb(50, 50, 255);
                }
            }
            else
            {
                // edited the parent register
                // mark all fields as edited, if any
                if (r.ListOfBitfields != null)
                {
                    for (int i = 0; i < r.ListOfBitfields.Count; ++i)
                    {
                        // mark the bitfield as edited
                        r.node.Nodes[i].Cells[2].Style.ForeColor = Color.FromArgb(50, 50, 255);
                    }
                }
            }
        }

        /// <summary>
        /// Start edit dmv F2, of zoomen
        /// </summary>
        private void Grid_KeyDown(object sender, KeyEventArgs e)
        {
            // use F2 key to start edit via keyboard
            if (e.KeyCode == Keys.F2)
            {
                TryStartEdit(this.CurrentCellAddress.Y);

                // we only decide about editing
                e.Handled = true;
            }

            // Option: stay on the same row on pressing Enter in the grid
            if (mNextRowOnEnter == false)
            {
                if ((e.KeyData & Keys.KeyCode) == Keys.Enter)
                {
                    e.Handled = true;
                    return;
                }
            }

            // Pass control-plus as zoom-in to parent
            if (e.Control && ((e.KeyCode == Keys.Add) || (e.KeyCode == Keys.Oemplus)))
            {
                OnZoomEvent(sender, +1);

                e.Handled = true;
            }

            // Pass control-minus as zoom-out to parent
            if (e.Control && ((e.KeyCode == Keys.Subtract) || (e.KeyCode == Keys.OemMinus)))
            {
                OnZoomEvent(sender, -1);

                e.Handled = true;
            }

            // Pass control-0 as reset-zoom to parent
            if (e.Control && ((e.KeyCode == Keys.NumPad0) || (e.KeyCode == Keys.D0)))
            {
                OnZoomEvent(sender, 0);

                e.Handled = true;
            }

            // Pass control-F as start_find to parent
            if (e.Control && (e.KeyCode == Keys.F))
            {
                OnFindEvent( sender);

                e.Handled = true;
            }
        }

        /// <summary>
        /// Start edit dmv KeyPress letter, cijfer, leesteken
        /// </summary>
        private void Grid_KeyPress(object sender, KeyPressEventArgs e)
        {
            // control-C for Copy?
            if (e.KeyChar == (char)3)
            {
                int row = this.CurrentCellAddress.Y;
                switch(this.CurrentCellAddress.X)
                {
                    // column 0 and 1: copy whole line
                    case 0:
                    case 1:
                        CopyLineToClipboard(row);
                        break;
                    // column 2: copy value string
                    case 2:
                        CopyValueToClipboard(row);
                        break;
                }

                e.Handled = true;
            }

            // Option: start value editing on any types character
            if (mStartEditOnAnyCharacter)
            {
                // accept letters, digits and punctuation like ",;
                // do not accept control characters for example
                if (Char.IsLetterOrDigit(e.KeyChar) || Char.IsPunctuation(e.KeyChar))
                {
                    BufferedChar = e.KeyChar;

                    // go edit cell
                    TryStartEdit(this.CurrentCellAddress.Y);

                    // key handled
                    e.Handled = true;
                }
            }
        }

        /// <summary>
        /// If string s is not null: return s
        /// If string s is null: return string text
        /// </summary>
        private string isnull(string s, string text)
        {
            if (s != null) return s;
            return text;
        }

        /// <summary>
        /// Convert object to string
        /// Return empty string if objects is null
        /// </summary>
        private string ObjToString(object obj)
        {
            if (obj != null) return obj.ToString();
            return "";
        }

        /// <summary>
        /// Start edit dmv cell doubleclick
        /// </summary>
        private void Grid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // do not react on clicks on headers
            if (e.RowIndex >= 0)
            {
                // react only on clicks in value column
                if ((e.ColumnIndex == 2))
                {
                    // go edit cell
                    TryStartEdit(e.RowIndex);
                }
            }
        }

        private bool TryStartEdit(int RowIndex)
        {
            // in read-only mode?
            if ( mIsReadOnly)
            {
                // Read only
                OnBeepEvent();
                return false;
            }

            // find register/child to edit
            CurrentEditReg = FindRegFromGridIndex(RowIndex, out CurrentEditBitfieldIndex);

            // is this var writeable?
            if (CurrentEditReg.Writeable == false)
            {
                // not writeable
                OnBeepEvent();
                return false;
            }

            // save the format for later
            CurrentEditFormat = CurrentEditReg.Format;
            CurrentEditDecimals = CurrentEditReg.Decimals;

            // make third column of this row the current cell, 'value' is the one to edit
            this.CurrentCell = this.Rows[RowIndex].Cells[2];

            // go edit cell
            return this.BeginEdit(true);
        }

        /// <summary>
        /// Editing control shows
        /// if we have a buffered key: send it to this control
        /// </summary>
        private void Grid_CellEditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            DataGridViewTextBoxEditingControl ed = e.Control as DataGridViewTextBoxEditingControl;

            if (ed != null)
            {
                // got a buffered key?
                // then send it to the editing control
                if (BufferedChar > 0)
                {
                    PostMessage(ed.Handle, WM_CHAR, (int)BufferedChar, 0);
                    BufferedChar = (char)0;
                }
            }
        }

        private void Grid_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            CurrentEditIsCanceled = false;

            // dot not allow editing if we're not polling 
            if (mIsReadOnly)
            {
                e.Cancel = true;
            }
        }

        private void Grid_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            string OrigString;

            // check if this edit was entered, not canceled by ESC 
            if (CurrentEditIsCanceled == false)
            {
                // get the value string, watch out for null if value is empty
                if (this.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                {
                    OrigString = this.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString().Trim();
                }
                else
                {
                    // use empty string, avoid nullpointer
                    OrigString = "";
                }

                // convert string to value
                // is it a float?
                bool IsFloat = (CurrentEditFormat == ModbusControlSpace.Global.eFormat.Float);
                // get the width, bitfield or register?
                int Width = 0;
                if (CurrentEditBitfieldIndex >= 0)
                {
                    // bitfield
                    Width = CurrentEditReg.ListOfBitfields[CurrentEditBitfieldIndex].Size;
                }
                else
                {
                    // register
                    Width = CurrentEditReg.Size;
                }
                // convert & check
                Int32 NewValue;
                Endians.eType Type;
                bool bIsValid = CValueToString.StringToValue(OrigString, IsFloat, Width, CurrentEditDecimals, out NewValue, out Type);

                // if this is a bitfield: insert bitfield value in register value
                if (bIsValid && (CurrentEditBitfieldIndex >= 0))
                {
                    int Position = CurrentEditReg.ListOfBitfields[CurrentEditBitfieldIndex].Position;
                    NewValue = InsertBitfieldInValue(CurrentEditReg.Value32, NewValue, Position, Width);
                }

                // if value is valid: write, otherwise show error
                if (bIsValid)
                {
                    // update value and all shown fields of this register
                    CurrentEditReg.Value32 = NewValue;
                    SetRegValue(CurrentEditReg);

                    // show the typed string as the cell's new value (till the next read)
                    this.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = OrigString;

                    // mark the parent register as edited
                    MarkColorAsEditedOfRegEntry(CurrentEditReg, CurrentEditBitfieldIndex);

                    // create the modbus values to write
                    Int16[] Data = Endians.ConvertValueToShorts(NewValue, Type, CurrentEditReg.Size);

                    // call write handler
                    if (OnRegisterWrite != null)
                    {
                        RegisterWriteEventArgs rwe = new RegisterWriteEventArgs();
                        rwe.Reg = CurrentEditReg.Reg;
                        rwe.Values = Data;
                        OnRegisterWrite(this, rwe);
                    }
                }
                else
                {
                    // show that the value was not accepted, put "<INVALID VALUE>" in front
                    // only if "<INVALID VALUE>" is not there yet
                    string Errortext = "<INVALID VALUE>";
                    if (!OrigString.StartsWith(Errortext))
                    {
                        this.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = Errortext + OrigString;
                    }
                    // Show it in red
                    this.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.ForeColor = Color.Red;
                    // report error on status bar
                    OnReportErrorMessageEvent("Input error: invalid value entered for register " + CurrentEditReg.Reg.ToString() + ": value: " + OrigString);
                    // beep
                    OnBeepEvent();
                }
            }
        }

        /// <summary>
        /// Check if value fits in N bits
        /// </summary>
        private bool ValueFitsInWidth( Int32 Value, int Width)
        {
            // check
            if (!((Width >= 1) && (Width <= 32)))
            {
                throw new ArgumentException("Invalid Width!", "Width");
            }

            // determine min and max depending on bitfield width
            UInt32 uMax = 0xFFFFFFFF >> (32 - Width);
            Int32 sMin = (Int32)(-(uMax / 2) - 1);

            // compare
            return ((Value >= sMin) && ((UInt32)Value <= uMax));
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            /* this handler is just to detect that the used ended edit with escape */

            /* if escape was pressed, set a flag so that OnGridCellEndEdit() knows that the input was canceled */
            if (keyData == Keys.Escape)
            {
                CurrentEditIsCanceled = true;
            }

            // Option: stay on the same row after entering data
            if ((mNextRowOnEnter == false) && (keyData == Keys.Enter))
            {
                keyData = Keys.Control | Keys.Enter;
            }

            return base.ProcessDialogKey(keyData);
        }

        private void Grid_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if( Rows[e.RowIndex].Height != RowTemplate.Height)
            {
                Rows[e.RowIndex].Height = RowTemplate.Height;
            }
        }
    
        /* find which reg and optionally which child is targeted */
        private RegEntry FindRegFromGridIndex(int Index, out int ChildIndex)
        {
            // default childindex -1 to target the parent
            ChildIndex = -1;

            // find the parent register
            int ri = 0;
            while ((ri < ListOfReg.Count) && (Index>0))
            {
                // has bitfields? and bitfields are expanded?
                if ((ListOfReg[ri].ListOfBitfields != null) && ListOfReg[ri].node.IsExpanded)
                {
                    // walk the bitfields
                    int bi = 0;
                    while (bi < ListOfReg[ri].ListOfBitfields.Count)
                    {
                        // count bitfields
                        --Index;
                        // got it?
                        if (Index == 0)
                        {
                            // return child
                            ChildIndex = bi;
                            return ListOfReg[ri];
                        }

                        ++bi;
                    }
                }

                ++ri;
                --Index;
            }

            // return parent
            return ListOfReg[ri];
        }

        /* Get format of a register or a bitfield
         */
        private ModbusControlSpace.Global.eFormat GetLineFormat(RegEntry r, int BitfieldIndex)
        {
            ModbusControlSpace.Global.eFormat Format = ModbusControlSpace.Global.eFormat.Int;

            // get format af a bitfield?
            if (BitfieldIndex >= 0)
            {
                Format = r.ListOfBitfields[BitfieldIndex].Format;
            }
            else
            {
                // get format of parent
                Format = r.Format; 
            }

            return Format;
        }

        private Int32 GetBitfieldValue(Int32 RegValue, int Position, int BitWidth, bool bDoSignExtend)
        {
            Int32 Result;

            UInt32 Mask = 0xFFFFFFFF >> (32 - BitWidth);
            Result = (Int32)(RegValue >> (Int32)Position) & (Int32)Mask;

            if (bDoSignExtend && (BitWidth >= 2))
            {
                // value is signed
                // do a sign extend
                Result = ( Result << (32-BitWidth)) >> (32-BitWidth);
            }

            return Result;
        }

        private Int32 InsertBitfieldInValue(Int32 Value, Int32 BitfieldValue, int Position, int BitWidth)
        {
            UInt32 Mask = 0xFFFFFFFF >> (32 - BitWidth);
            BitfieldValue = (Int32)(BitfieldValue & Mask);
            Int32 Newvalue = (Int32)(  ((UInt32)Value & ~(Mask << Position))
                                      |((UInt32)BitfieldValue << Position));

            return Newvalue;
        }

        /* Zoom 
         */
        public void SetFontZoomFactor( float Factor)
        {
            // keep a min/max font size
            if (Factor < 0.1F) Factor = 0.1F;
            if (Factor > 10F) Factor = 10F;

            float NewFontSize = OriginalFontSize * Factor;

            this.SuspendLayout();
            
            /* change row height to match new font size */

            RowTemplate.Height = (int)(OriginalRowHeight * Factor + 0.5F);

            /* Changing RowTemplate.Height has no effect on the rows.
            *  We can loop through all rows to update their height (row.Height = x) but that takes a long time for many rows.
            *  Or we can use the RowPrePaint() callback to change each row's height just before it is painted. That's fast.
            */

            /* set new font with new size */
            Font newFont;
            try
            {
                // limit font size
                newFont = new System.Drawing.Font(Font.FontFamily, NewFontSize, Font.Style, Font.Unit);

                this.Font = newFont;
            }
            catch (Exception)
            {
            }

            this.ResumeLayout();
        }

        private void Grid_MouseUp(object sender, MouseEventArgs e)
        {
            // Load context menu on right mouse click
            DataGridView.HitTestInfo hitTestInfo;

            // Right button?
            if (e.Button == MouseButtons.Right)
            {
                hitTestInfo = this.HitTest(e.X, e.Y);

                // Register or Text column?
                if ((hitTestInfo.Type == DataGridViewHitTestType.Cell) && ((hitTestInfo.ColumnIndex == 0) || (hitTestInfo.ColumnIndex == 1)))
                {
                    // find register/child to edit
                    CurrentEditReg = FindRegFromGridIndex(hitTestInfo.RowIndex, out CurrentEditBitfieldIndex);
                    CurrentEditRowIndex = hitTestInfo.RowIndex;

                    // show menu
                    ContextMenuForFirstColumns.Show( this, e.X, e.Y);
                }

                // Value column?
                if ((hitTestInfo.Type == DataGridViewHitTestType.Cell) && (hitTestInfo.ColumnIndex == 2))
                {
                    // find register/child to edit
                    CurrentEditReg = FindRegFromGridIndex(hitTestInfo.RowIndex, out CurrentEditBitfieldIndex);

                    // show current type with checked flags
                    ModbusControlSpace.Global.eFormat OldFormat = GetLineFormat(CurrentEditReg, CurrentEditBitfieldIndex);
                    ContextMenuForValueColumn.MenuItems[0].Checked = (OldFormat == ModbusControlSpace.Global.eFormat.Int);
                    ContextMenuForValueColumn.MenuItems[1].Checked = (OldFormat == ModbusControlSpace.Global.eFormat.UInt);
                    ContextMenuForValueColumn.MenuItems[2].Checked = (OldFormat == ModbusControlSpace.Global.eFormat.Hex);
                    ContextMenuForValueColumn.MenuItems[3].Checked = (OldFormat == ModbusControlSpace.Global.eFormat.Bin);
                    // allow float for registers only, not bitfields
                    ContextMenuForValueColumn.MenuItems[4].Enabled = (CurrentEditReg.Size>=32) && (CurrentEditBitfieldIndex == -1);
                    ContextMenuForValueColumn.MenuItems[4].Checked = (OldFormat == ModbusControlSpace.Global.eFormat.Float);
                    ContextMenuForValueColumn.MenuItems[5].Checked = (OldFormat == ModbusControlSpace.Global.eFormat.ASCII);

                    // show menu
                    ContextMenuForValueColumn.Show(this, new Point(e.X, e.Y));
                }
            }
        }

        private void Menu_SetColorDefault(object sender, EventArgs e)
        {
            TreeGridNode n = GetNodeForRow(CurrentEditRowIndex);
            this.Nodes.SetBackColor(n, ColorTranslator.FromHtml("#FFFFFF"));
        }

        private void Menu_SetColorRed(object sender, EventArgs e)
        {
            TreeGridNode n = GetNodeForRow(CurrentEditRowIndex);
            this.Nodes.SetBackColor(n, ColorTranslator.FromHtml("#FFCCCC"));
        }

        private void Menu_SetColorGreen(object sender, EventArgs e)
        {
            TreeGridNode n = GetNodeForRow(CurrentEditRowIndex);
            this.Nodes.SetBackColor(n, ColorTranslator.FromHtml("#CCFFCC"));
        }

        private void Menu_SetColorBlue(object sender, EventArgs e)
        {
            TreeGridNode n = GetNodeForRow(CurrentEditRowIndex);
            this.Nodes.SetBackColor(n, ColorTranslator.FromHtml("#A0E1FF"));
        }

        private void Menu_SetColorYellow(object sender, EventArgs e)
        {
            TreeGridNode n = GetNodeForRow(CurrentEditRowIndex);
            this.Nodes.SetBackColor(n, ColorTranslator.FromHtml("#FFFFA0"));
        }

        private void Menu_SetFormatToInt(object sender, EventArgs e)
        {
            SetLineFormat(CurrentEditReg, CurrentEditBitfieldIndex, ModbusControlSpace.Global.eFormat.Int);
        }

        private void Menu_SetFormatToUInt(object sender, EventArgs e)
        {
            SetLineFormat(CurrentEditReg, CurrentEditBitfieldIndex, ModbusControlSpace.Global.eFormat.UInt);
        }

        private void Menu_SetFormatToHex(object sender, EventArgs e)
        {
            SetLineFormat(CurrentEditReg, CurrentEditBitfieldIndex, ModbusControlSpace.Global.eFormat.Hex);
        }

        private void Menu_SetFormatToBin(object sender, EventArgs e)
        {
            SetLineFormat(CurrentEditReg, CurrentEditBitfieldIndex, ModbusControlSpace.Global.eFormat.Bin);
        }

        private void Menu_SetFormatToFloat(object sender, EventArgs e)
        {
            SetLineFormat(CurrentEditReg, CurrentEditBitfieldIndex, ModbusControlSpace.Global.eFormat.Float);
        }

        private void Menu_SetFormatToASCII(object sender, EventArgs e)
        {
            SetLineFormat(CurrentEditReg, CurrentEditBitfieldIndex, ModbusControlSpace.Global.eFormat.ASCII);
        }

        private void SetLineFormat(RegEntry r, int BitfieldIndex, ModbusControlSpace.Global.eFormat Format)
        {
            // is it a bitfield or register?
            if (BitfieldIndex >= 0)
            {
                // bitfield
                BitfieldEntry f = r.ListOfBitfields[BitfieldIndex];

                f.Format = Format;
                // update the value shown in the grid
                r.node.Nodes[BitfieldIndex].Cells[2].Value = CValueToString.ValueToString(f.Value, f.Size, f.Format, 0, f.MinLen);
            }
            else
            {
                // register
                r.Format = Format;
                // update the value shown in the grid
                r.node.Cells[2].Value = CValueToString.ValueToString(r.Value32, r.Size, r.Format, r.Decimals, r.MinLen);
            }
        }

        /// <summary>
        /// Copy grid line string to clipboard
        /// </summary>
        private void Menu_CopyLine(object sender, EventArgs e)
        {
            int row = this.CurrentCellAddress.Y;
            CopyLineToClipboard(row);
        }

        /// <summary>
        /// Copy grid value to clipboard
        /// </summary>
        private void Menu_CopyValue(object sender, EventArgs e)
        {
            int row = this.CurrentCellAddress.Y;
            CopyValueToClipboard(row);
        }

        private void CopyLineToClipboard(int row)
        {
            string s = ObjToString(this.Rows[row].Cells[0].Value)
                    + (char)9 // tab
                    + ObjToString(this.Rows[row].Cells[1].Value)
                    + (char)9 // tab
                    + ObjToString(this.Rows[row].Cells[2].Value);
            if (s.Length > 0)
            {
                System.Windows.Forms.Clipboard.SetText(s);
            }
        }

        private void CopyValueToClipboard(int row)
        {
            string s = ObjToString(this.Rows[row].Cells[2].Value);
            if (s.Length > 0)
            {
                System.Windows.Forms.Clipboard.SetText(s);
            }
        }

        // Find event
        private void OnFindEvent(object sender)
        {
            if (this.OnFind != null)
            {
                OnFind(sender);
            }
        }

        // Zoom event
        private void OnZoomEvent(object sender, int Action)
        {
            if (this.OnZoom != null)
            {
                OnZoom(sender, Action);
            }
        }

        // Error message event
        private void OnReportErrorMessageEvent( string s)
        {
            if (this.OnReportErrorMessage != null)
            {
                OnReportErrorMessage(s);
            }
        }

        /*!
        * Get a group of successive registers to read
        *   LineIndex   in: index of the first line to read
        *               out: index of the first line of which data is read
        *   LineCount   out: number of lines processed
        *   FirstReg    out: first modbus register to read
        *   RegCount    out: number of 16-bit modbus registers to read
        *   Maximum16bitRegisters   in: the maximum number of 16-bit registers to keep framesize under MaximumFramesize
        *  
        * @retval true if we got registers to read
        * @retval false if we got no more registers to read
        */
        public bool GetGroupOfRegistersToRead(ref int LineIndex, out int LineCount, out UInt32 FirstReg, out UInt16 RegCount, int Maximum16bitRegisters)
        {
            UInt32 NextReg;
            int thislen;

            LineCount = 0;
            FirstReg = 0;
            RegCount = 0;

            /* there are lines left? */
            /* find next line that has bRead set */
            while ((LineIndex < ListOfReg.Count)
                  && (ListOfReg[LineIndex].Readable == false))
            {
                LineIndex++;
            }
            /* found? */
            if (LineIndex < ListOfReg.Count)
            {
                /* found */
            }
            else
            {
                /* not found */
                return false;
            }

            /* get this register number */
            FirstReg = ListOfReg[LineIndex].Reg;
            thislen = ListOfReg[LineIndex].Size / 16; /* number_of_bits to number_of_registers */
            NextReg = FirstReg + (UInt32)thislen;
            RegCount += (UInt16)thislen;
            LineCount++;

            /* get the number of consecutive registers */
            while (((LineIndex + LineCount) < ListOfReg.Count) /* not end of of tab? */
                  && ListOfReg[LineIndex + LineCount].Readable       /* read? */
                  && (ListOfReg[LineIndex + LineCount].Reg == NextReg))  /* consecutive? */
            {
                thislen = ListOfReg[LineIndex + LineCount].Size / 16; /* number_of_bits to number_of_registers */
                if ((RegCount + thislen) <= Maximum16bitRegisters)     /* allowed to read so many registers? */
                {
                    NextReg = NextReg + (UInt32)thislen;
                    RegCount += (UInt16)thislen;
                    LineCount++;
                }
                else
                {
                    break;
                }
            }

            return true;
        }

        /// Put the data we have read in the grid
        /// Provide delegate and invoke for calls from other threads
        private delegate void DelegateSetRegistersReadData(int LineIndex, int LineCount, short[] data);
        public void SetRegistersReadData( int LineIndex, int LineCount, short[] data)
        {
            if (this.InvokeRequired)
            {
                DelegateSetRegistersReadData del = new DelegateSetRegistersReadData(this.SetRegistersReadData);
                this.Invoke(del, LineIndex, LineCount, data);
            }
            else
            {
                int DataIndex = 0;

                this.SuspendLayout();

                for (int i = LineIndex; i < (LineIndex + LineCount); ++i)
                {
                    RegEntry r = ListOfReg[i];

                    // first 16-bit
                    r.RawReg[0] = data[DataIndex++];
                    r.RawReg[1] = 0;
                    // is it 32-bit?
                    if (r.Size == 32)
                    {
                        r.RawReg[1] = data[DataIndex++];
                    }

                    // convert format to type
                    Endians.eType Type = ConvertFormatToType(r.Format);

                    // make 32-bit value, watch endian
                    r.Value32 = Endians.ConvertShortsToValue(r.RawReg, Type, r.Size);

                    SetRegValue(r);
                }

                this.ResumeLayout();
            }
        }

        /// Put the read error message in the grid
        /// Provide delegate and invoke for calls from other threads
        private delegate void DelegateSetRegistersReadError(int LineIndex, int LineCount, string ErrorString);
        public void SetRegistersReadError(int LineIndex, int LineCount, string ErrorString)
        {
            if (this.InvokeRequired)
            {
                DelegateSetRegistersReadError del = new DelegateSetRegistersReadError(this.SetRegistersReadError);
                this.Invoke(del, LineIndex, LineCount, ErrorString);
            }
            else
            {
                this.SuspendLayout();

                for (int i = LineIndex; i < (LineIndex + LineCount); ++i)
                {
                    SetRegErrorString(i, ErrorString);
                }

                this.ResumeLayout();
            }
        }

        /*!
        * Get a list of successive registers to read
        *   LineIndex   in: index of the first line to read
        *               out: index of the first line of which data is read
        *   LineCount   out: number of lines processed
        *   FirstReg    out: first modbus register to read
        *   RegCount    out: number of 16-bit modbus registers to read
        *   Maximum16bitRegisters   in: the maximum number of 16-bit registers to keep framesize under MaximumFramesize
        *  
        * @retval: number of 16-bit registers
        *          0 if nothing to read
        */
        public int GetRegistersListToRead(ref int LineIndex, out int LineCount, int Maximum16bitRegisters, out List<RegEntry> Reglist)
        {
            int LineIndexFirst = 0;
            int Reg16Count = 0;
            UInt32 NextReg = 0;

            // init return stuff
            LineCount = 0;
            Reglist = new List<RegEntry>();

            /* there are lines left? */
            /* find next line that has bRead set */
            while ((LineIndex < ListOfReg.Count)
                  && (ListOfReg[LineIndex].Readable == false))
            {
                LineIndex++;
            }
            /* found? */
            if (LineIndex < ListOfReg.Count)
            {
                /* found */
            }
            else
            {
                /* not found */
                return 0;
            }

            /* Found first register */
            Reglist.Add( ListOfReg[LineIndex]);
            Reg16Count += ListOfReg[LineIndex].Size / 16;
            NextReg = ListOfReg[LineIndex].Reg + (UInt32)(ListOfReg[LineIndex].Size / 16);
            LineIndexFirst = LineIndex;
            LineIndex++;
            LineCount++;

            /* Find consecutive registers */
            while ((LineIndex < ListOfReg.Count)        /* not end of of tab? */
                  && ListOfReg[LineIndex].Readable      /* read? */
                  && (ListOfReg[LineIndex].Reg == NextReg)  /* consecutive? */
                  && ((Reg16Count + (ListOfReg[LineIndex].Size / 16)) <= Maximum16bitRegisters)) /* not max length reached? */
            {
                /* Add next register */
                Reglist.Add( ListOfReg[LineIndex]);
                Reg16Count += ListOfReg[LineIndex].Size / 16;
                NextReg = ListOfReg[LineIndex].Reg + (UInt32)(ListOfReg[LineIndex].Size / 16);
                LineIndex++;
                LineCount++;
            }

            return Reg16Count;
        }


        public string BuildRegisterInfoStr(RegEntry r)
        {
            StringBuilder builder = new StringBuilder();

            builder.Clear();

            builder.Append("   ");
            builder.Append("[");

            // add format and size
            builder.Append(ModbusControlSpace.Global.FormatText[(int)r.Format]);
            builder.Append(r.Size.ToString());

            // add decimals
            if (r.Decimals != 0)
            {
                builder.Append(", decimals");
            }

            // add r/w 
            if (r.Readable || r.Writeable)
            {
                builder.Append(", ");
                if (r.Readable) builder.Append("r");
                if (r.Writeable) builder.Append("w");
            }

            builder.Append("]");

            return builder.ToString();
        }

        public string BuildBitfieldInfoStr(BitfieldEntry bf)
        {
            StringBuilder builder = new StringBuilder();

            builder.Clear();

            builder.Append("   ");
            builder.Append("[");

            // add format and size
            builder.Append(ModbusControlSpace.Global.FormatText[(int)bf.Format]);
            builder.Append(bf.Size.ToString());

            builder.Append("]");

            return builder.ToString();
        }

        public void UpdateDescriptionInfo()
        {
            this.SuspendLayout();

            // Each register
            foreach (RegEntry r in ListOfReg)
            {
                // new description string
                r.node.Cells[1].Value = mShowInfo ? r.Text + BuildRegisterInfoStr(r) : r.Text;

                // has bitfields?
                if (r.ListOfBitfields != null)
                {
                    // Each bitfield
                    foreach (BitfieldEntry bf in r.ListOfBitfields)
                    {
                        // new description string
                        bf.node.Cells[1].Value = mShowInfo ? bf.Text + BuildBitfieldInfoStr(bf) : bf.Text;
                    }
                }
            }

            this.ResumeLayout();
        }


        private void OnBeepEvent()
        {
            // event 
            if (OnBeep != null)
            {
                OnBeep(this);
            }
        }

    }
}
