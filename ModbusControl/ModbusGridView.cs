using LST.Helpers;
using ModbusControlSpace;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

//#define DEBUG_VISIBILITY

namespace LST.ModbusGridView
{
    public partial class ModbusGridView : UserControl
    {
        private enum eRowType { Group, Reg, Field };
        private enum eGridColIndex { Reg, Descr, Properties, Value, Debuginfo };
        enum dtColIndex { RowType, RowLevel, RowVisible, RegStr, Descr, InfoStr, FCol, BCol, ValueMode, ValueStr, Flags, Longdescr };
        private const int DefaultVisible = 3;
        private const int ValueModeNormal = 0;
        private const int ValueModeEdited = 1;
        private const int ValueModeError = 2;
        private const int FlagHasChildren = 1;  // bitfield flag
        private const int FlagIsExpanded = 2;   // bitfield flag
        private const int FlagIsEnum = 4;       // bitfield flag

        // needed for PostMessage
        [DllImport("user32.dll")]
        static extern bool PostMessage(IntPtr hWnd, uint Msg, int wParam, int lParam);
        private const int WM_CHAR = 0x0102;

        // some GUI coloring
        private Color EditedColor = Color.FromArgb(50, 50, 255);
        private Color InputErrorColor = Color.Red;

        // items root class
        public class ListItem
        {
            public DataRow dataRow;
            public int dataRowIndex;
            public List<ListItem> childrenList;
            public bool expanded;                   // initally expanded or collapsed
        }

        public class RegItem : ListItem
        {
            public UInt32 Reg;          // universal register number 
            public string Descr;        // description
            public int Size;            // var size in number of Int16 registers, 1-125
            public Global.eFormat Format;           // format (int, uint, hex, string,...)
            public Global.eFormat DisplayFormat;    // display format
            public int MinLen;          // minimum display length
            public int Decimals;        // nr of decimals to show
            public bool Readable;       // readable
            public bool Writeable;      // writable
            public string InfoStr;      // info string, like 'uint16, rw, dec=2'
            public Color? ForeColor;    // the foreground/font color, nullable
            public Color? BackColor;    // the background color, nullable
            public Int16[] DataInt16Array; // data as Int16[], to be converted to Int16, Int32, ASCIIxx, stringN,...
            public UInt32 ValueUInt32;  // value as UInt32,for bitfield conversion
            public string ValueStr;     // value as a string, formatted
            public Global.CEnum EnumDict;   // enum convert dictionary
            public string LongDescr;    // <longdescr> tooltip text

            // constructor
            public RegItem()
            {
                Reg = 0;
                Descr = String.Empty;
                Size = 0;
                Format = Global.eFormat.Int;
                DisplayFormat = Global.eFormat.Int;
                MinLen = 1;
                Decimals = 0;
                Readable = false;
                Writeable = false;
                InfoStr = String.Empty;
                ForeColor = null;
                BackColor = null;
                DataInt16Array = null;
                ValueUInt32 = 0;
                ValueStr = String.Empty;
                EnumDict = null;
                LongDescr = String.Empty;
            }
        };

        public class RegfieldItem : ListItem
        {
            public int Pos;             // lowest starting bit position
            public int Width;           // bitfield width in bits (1-32)
            public string RegStr;       // as string
            public string Descr;        // description
            public Global.eFormat Format;           // format (int, uint, hex, string,...)
            public Global.eFormat DisplayFormat;    // display format
            public int MinLen;          // minimum display length
            public int Decimals;        // nr of decimals to show
            public string InfoStr;      // info string
            public Color? ForeColor;    // the foreground/font color
            public Color? BackColor;    // the background color
            public Int32 Value32;       // actual value in 32-bit
            public string ValueStr;     // value as a string
            public Global.CEnum EnumDict;   // enum convert dictionary
            public string LongDescr;    // <longdescr> tooltip text

            // constructor
            public RegfieldItem()
            {
                Pos = 0;
                Width = 1;
                RegStr = String.Empty;
                Descr = String.Empty;
                Format = Global.eFormat.Int;
                DisplayFormat = Global.eFormat.Int;
                MinLen = 1;
                Decimals = 0;
                InfoStr = String.Empty;
                ForeColor = null;
                BackColor = null;
                Value32 = 0;
                ValueStr = String.Empty;
                EnumDict = null;
                LongDescr = String.Empty;
            }
        };

        private struct TFocusPos
        {
            public int Row;
            public int Col;
        };

        public class CReadGroup
        {
            public int lineIndex;
            public int lineCount;
            public UInt32 readFirstReg;
            public int readRegCount;
        };

        public struct TVarProperties
        {
            public UInt32 Reg;          // universal register number 
            public string Descr;        // description
            public int Size;            // var size in Int16 registers, 1 or 2
            public Global.eFormat Format;           // format (int, uint, hex, string,...)
            public Global.eFormat DisplayFormat;    // display format
            public int MinLen;          // minimum display length
            public int Decimals;        // nr of decimals to show
            public bool Readable;       // readable?
            public bool Writeable;      // writable?
            public Global.CEnum EnumDict;   // enum convert dictionary
        };

        #region Data

        List<ListItem> RegList;

        // the data table
        private DataTable data;

        private Icon expandIcon;                        // tree expand icon, size depends on zoom
        private Icon collapseIcon;                      // tree collapse icon, size depends on zoom
        private Icon enumIndicatorIcon;                 // enum indicator icon, size depends on zoom
        private int expandIconWidth;                    // tree expand/collapse icon width
        private int expandIconHeight;                   // tree expand/collapse icon height
        private int expandIconPadding = 4;              // tree expand/collapse icon padding
        private int indentWidth;
        private int[] columnWidths;
        private string mFilterString;
        private bool mFilterCaseSensitive;
        private float originalFontSize;
        private float currentZoomFactor = 1;
        public bool customPainting = true;
        private RegItem editingReg;
        private RegfieldItem editingRegfield;
        private char editingBufferedChar = (char)0;
        private volatile DataRow editingDatarow;        // volatile, to block update while editing
        private bool mAutosizeRows;
        private int rightclickRowIndex;
        private bool isLoading;
        // properties values
        private bool mConnectionIsOpen;
        private bool mStartEditOnAnyCharacter;
        // entry error timer
        private DataRow inputErrorRow = null;       // the drow currently in input error
        private Timer inputErrorTimer;              // timer to make the input error stay for a (inputErrorTimerDelay) ms
        private const int inputErrorTimerDelay = 2000;

         #endregion

        #region Properties

        public bool ConnectionIsOpen
        {
            get { return mConnectionIsOpen; }
            set { mConnectionIsOpen = value; }
        }

        public bool AutosizeColumns
        {
            set { setAutosizeCol(value); }
        }

        public bool AutosizeRows
        {
            get { return mAutosizeRows; }
            set { setAutosizeRows(value); }
        }

        public bool WrapRows
        {
            set { setWrapRows(value); }
        }

        public bool AutosizeAndWrapRows
        {
            set { setAutosizeAndWrapRows(value); }
        }

        public bool CustomFormattingAndPainting
        {
            set { setCustomFormattingAndPainting(value); }
        }

        public bool StartEditOnAnyCharacter
        {
            get { return mStartEditOnAnyCharacter; }
            set { mStartEditOnAnyCharacter = value; }
        }

        public bool NextRowOnEnter
        {
            get { return grid.MoveToNextRowOnEnter; }
            set { grid.MoveToNextRowOnEnter = value; }
        }

        public bool ShowVarProperties
        {
            get { return getShowVarProperties(); }
            set { setShowVarProperties(value); }
        }

        public bool LongTooltipTime
        {
            set { setLongTooltipTime(value); }
        }

        public void SetFilter(string text, bool caseSensitive)
        {
            if ((text != mFilterString)
                || (caseSensitive != mFilterCaseSensitive))
            {
                mFilterString = text;
                mFilterCaseSensitive = caseSensitive;
                UpdateFilter();
            }
        }

        public float RegisterColumnWidthWeight
        {
            get { return colReg.FillWeight; }
            set
            {
                if (value != colReg.FillWeight)
                {
                    isLoading = true;
                    colReg.FillWeight = value;
                    isLoading = false;
                }
            }
        }

        public float DescriptionColumnWidthWeight
        {
            get { return colDescr.FillWeight; }
            set
            {
                if (value != colDescr.FillWeight)
                {
                    isLoading = true;
                    colDescr.FillWeight = value;
                    isLoading = false;
                }
            }
        }

        public float PropertiesColumnWidthWeight
        {
            get { return colProps.FillWeight; }
            set
            {
                if (value != colProps.FillWeight)
                {
                    isLoading = true;
                    colProps.FillWeight = value;
                    isLoading = false;
                }
             }
        }

        public float ValueColumnWidthWeight
        {
            get { return colValue.FillWeight; }
            set
            {
                if (value != colValue.FillWeight)
                {
                    isLoading = true;
                    colValue.FillWeight = value;
                    isLoading = false;
                }
            }
        }

        public uint ExportOrder;

        #endregion

        #region Events

        public event RegisterWriteHandler OnRegisterWrite;
        public delegate void RegisterWriteHandler(object sender, RegisterWriteEventArgs e);
        public class RegisterWriteEventArgs : EventArgs
        {
            public UInt32 Reg;
            public Int16[] Values;
        }

        public delegate void ReportErrorMessageHandler(string s);
        public event ReportErrorMessageHandler OnReportErrorMessage;

        public delegate void BeepHandler(object sender);
        public event BeepHandler OnBeep;

        public event ColumnWidthChangedHandler OnColumnWidthChanged;
        public delegate void ColumnWidthChangedHandler(object sender);


        #endregion

        #region Implementation

        /// <summary>
        /// Constructor
        /// </summary>
        public ModbusGridView()
        {
            isLoading = true;

            InitializeComponent();

            // disable DataGridView copying content on control-C
            // otherwise pressing control-C while editing the value copies the whole row content instead of the value string only
            // we handle control-C ourselves in grid_KeyDown()
            grid.ClipboardCopyMode = DataGridViewClipboardCopyMode.Disable;

            // init the error timer
            inputErrorTimer = new Timer();
            inputErrorTimer.Interval = inputErrorTimerDelay;
            inputErrorTimer.Tick += InputErrorTimer_Tick;

            // create items list
            RegList = new List<ListItem>();

            // init grid

            // bind columns
            grid.Columns[(int)eGridColIndex.Reg].DataPropertyName = "RegStr";
            grid.Columns[(int)eGridColIndex.Descr].DataPropertyName = "Descr";
            grid.Columns[(int)eGridColIndex.Properties].DataPropertyName = "InfoStr";
            grid.Columns[(int)eGridColIndex.Value].DataPropertyName = "ValueStr";

            // set grid icons and their sizes
            SetIconSizes();

            originalFontSize = Font.Size;

            // show debugging columns
            //.Columns[(int)eGridColIndex.Debuginfo].Visible = true;

            this.AutosizeRows = false;
            this.WrapRows = false;
            this.DoubleBuffered = true;
        }

        /// <summary>
        /// Add a register
        /// </summary>
        /// <param>The required parameters</param>
        /// <returns>Just added ListItem object</returns>
        public ListItem AddReg(ListItem Parent, UInt32 Reg, string Descr, int Size, Global.eFormat Format, bool Readable, bool Writeable)
        {
            RegItem newReg = new RegItem();

            newReg.Reg = Reg;
            newReg.Descr = Descr;
            newReg.Size = Size;
            newReg.Format = Format;
            newReg.DisplayFormat = Format;
            newReg.Decimals = 0;
            newReg.Readable = Readable;
            newReg.Writeable = Writeable;
            newReg.DataInt16Array = new Int16[Size];

            RegList.Add(newReg);
            UpdateInfoStr(newReg);

            return newReg;
        }

        public void RegSetOptions(ListItem Item, Color? FCol, Color? BCol, int MinLen, int Decimals, bool Expanded)
        {
            if (Item is RegItem)
            {
                RegItem r = Item as RegItem;
                r.ForeColor = FCol;
                r.BackColor = BCol;
                r.MinLen = MinLen;
                r.Decimals = Decimals;
                r.expanded = Expanded;

                UpdateInfoStr(r);
            }
        }

        /// <summary>
        /// Add a register bitfield
        /// </summary>
        /// <param>The required parameters</param>
        /// <returns>Just added ListItem object</returns>
        public ListItem RegAddField(ListItem Parent, int Pos, int Width, string Descr, Global.eFormat Format)
        {
            RegfieldItem newField = new RegfieldItem();

            newField.Pos = Pos;
            newField.Width = Width;
            newField.RegStr = FieldPosAndWidthToString(Pos, Width);
            newField.Descr = Descr;
            newField.Format = Format;
            newField.DisplayFormat = Format;
            newField.Decimals = 0;

            if (Parent.childrenList == null)
            {
                Parent.childrenList = new List<ListItem>();
            }

            Parent.childrenList.Add(newField);
            UpdateInfoStr(newField);

            return newField;
        }

        public void RegFieldSetOptions(ListItem Item, Color? FCol, Color? BCol, int MinLen, int Decimals)
        {
            if (Item is RegfieldItem)
            {
                RegfieldItem f = Item as RegfieldItem;
                f.ForeColor = FCol;
                f.BackColor = BCol;
                f.MinLen = MinLen;
                f.Decimals = Decimals;

                UpdateInfoStr(f);
            }
        }

        public void RegSetEnums(ListItem Item, Global.CEnum dict)
        {
            if (Item is RegItem)
            {
                if (dict != null)
                {
                    RegItem r = Item as RegItem;
                    r.EnumDict = dict;
                    UpdateInfoStr(r);
                }
            }
            if (Item is RegfieldItem)
            {
                if (dict != null)
                {
                    RegfieldItem f = Item as RegfieldItem;
                    f.EnumDict = dict;
                    UpdateInfoStr(f);
                }
            }
        }

        public void RegSetLongdescrText(ListItem Item, string LongdescrText)
        {
            if (Item is RegItem)
            {
                if (LongdescrText != null)
                {
                    RegItem r = Item as RegItem;
                    r.LongDescr = LongdescrText;
                }
            }
            if (Item is RegfieldItem)
            {
                if (LongdescrText != null)
                {
                    RegfieldItem f = Item as RegfieldItem;
                    f.LongDescr = LongdescrText;
                }
            }
        }

        private void UpdateInfoStr(ListItem Item)
        {
            if (Item is RegItem)
            {
                RegItem ri = Item as RegItem;

                // format and size
                // size is in #registers
                string infoStr = ri.Format.ToString().ToLower();
                if (ri.Format == Global.eFormat.String)
                {
                    // show size as nr-of-bytes for string
                    infoStr += (ri.Size * 2).ToString();
                }
                else
                {
                    // show size as bits for others
                    infoStr += (ri.Size * 16).ToString();
                }

                // add decimals
                if (ri.Decimals != 0)
                {
                    infoStr += ", dec";
                }

                // add enum
                if (ri.EnumDict != null)
                {
                    infoStr += ", enum";
                }

                // add r/w
                if (ri.Readable || ri.Writeable)
                {
                    infoStr += ", ";
                    if (ri.Readable) infoStr += "r";
                    if (ri.Writeable) infoStr += "w";
                }

                ri.InfoStr = infoStr;
            }
            if (Item is RegfieldItem)
            {
                RegfieldItem fi = Item as RegfieldItem;

                // format and size
                string infoStr = Global.FormatText[(int)fi.Format].ToLower();
                infoStr += fi.Width.ToString();

                // add decimals
                if (fi.Decimals != 0)
                {
                    infoStr += ", dec";
                }

                // add enum
                if (fi.EnumDict != null)
                {
                    infoStr += ", enum";
                }

                fi.InfoStr = infoStr;
            }
        }

        private string FieldPosAndWidthToString(int Pos, int Width)
        {
            if (Width >= 2)
                return "b" + (Pos + Width - 1).ToString() + "-b" + Pos.ToString();
            return "b" + Pos.ToString();
        }

        public void FinalizeLoad()
        {
            // all registers and bitfields are loaded
            // now create datatable for driving the grid
            // no filtering yet

            // Create data table
            data = new DataTable("GridTable");

            // Add columns
            data.Columns.Add("RowType", System.Type.GetType("System.Int32"));
            data.Columns.Add("RowLevel", System.Type.GetType("System.Int32"));
            data.Columns.Add("RowVisible", System.Type.GetType("System.Int32"));    // 3=visible, b0 voor collapse/expand, b1 voor filtering
            data.Columns.Add("RegStr", System.Type.GetType("System.String"));
            data.Columns.Add("Descr", System.Type.GetType("System.String"));
            data.Columns.Add("InfoStr", System.Type.GetType("System.String"));
            data.Columns.Add("FCol", typeof(System.Drawing.Color));                 // foreground color
            data.Columns.Add("BCol", typeof(System.Drawing.Color));                 // background color
            data.Columns.Add("ValueMode", System.Type.GetType("System.Int32"));     // mode of value column, 0=normal, 1=edit, 2=error
            data.Columns.Add("ValueStr", System.Type.GetType("System.String"));
            data.Columns.Add("Flags", System.Type.GetType("System.Int32"));
            data.Columns.Add("Longdescr", System.Type.GetType("System.String"));    // longdescription tooltip text

            // provide filtering
#if !DEBUG_VISIBILITY
            data.DefaultView.RowFilter = "([RowVisible] = 3)";
#endif
            // load data
            foreach (ListItem li in RegList)
            {
                if (li is RegItem)
                {
                    RegItem ri = li as RegItem;

                    int Flags = 0;
                    if (ri.childrenList != null) Flags |= FlagHasChildren;
                    if (ri.expanded) Flags |= FlagIsExpanded;
                    if (ri.EnumDict != null) Flags |= FlagIsEnum;

                    data.Rows.Add(new object[] { eRowType.Reg, 0, DefaultVisible,        // rowtype, level, visible
                        ri.Reg.ToString(), ri.Descr, ri.InfoStr,
                        ri.ForeColor, ri.BackColor, 0,
                        "",                             // ValueStr
                        Flags,                          // Flags
                        ri.LongDescr});                 // Longdescr tooltip text

                    li.dataRowIndex = data.Rows.Count - 1;
                    li.dataRow = data.Rows[li.dataRowIndex];

                    // add children?
                    if (ri.childrenList != null)
                    {
                        foreach (ListItem li2 in ri.childrenList)
                        {
                            if (li2 is RegfieldItem)
                            {
                                RegfieldItem fi = li2 as RegfieldItem;

                                Flags = 0;
                                if (fi.childrenList != null) Flags |= FlagHasChildren;
                                if (fi.expanded) Flags |= FlagIsExpanded;
                                if (fi.EnumDict != null) Flags |= FlagIsEnum;

                                data.Rows.Add(new object[] { eRowType.Field, 1, DefaultVisible,        // rowtype, level, visible
                                    fi.RegStr, fi.Descr, fi.InfoStr,
                                    fi.ForeColor, fi.BackColor, 0,
                                    "",                             // ValueStr
                                    Flags,                          // Flags
                                    fi.LongDescr});                 // Longdescr tooltip text

                                fi.dataRowIndex = data.Rows.Count - 1;
                                fi.dataRow = data.Rows[fi.dataRowIndex];
                            }
                        }
                    }
                }

                // load in grid
                grid.AutoGenerateColumns = false;
                grid.DataSource = data;
            }

            // after loading, all rows are visible
            // must hide children of collapsed parents 
            UpdateDataExpandedCollapsed();

            isLoading = false;
        }

        private string getRowDebuginfoStr(int dataRowIndex)
        {
            //dataRowIndex is DataTable row index
            DataRow dRow = data.Rows[dataRowIndex];

            string s = "R=" + dataRowIndex.ToString() + "; ";

            switch ((int)dRow[(int)dtColIndex.RowType])
            {
                case (int)eRowType.Group: s += "T=G; "; break;
                case (int)eRowType.Reg: s += "T=R; "; break;
                case (int)eRowType.Field: s += "T=F; "; break;
                default: s += "T=?; "; break;
            }

            s += "L=" + ((int)dRow[(int)dtColIndex.RowLevel]).ToString() + "; ";

            s += "V=" + ((int)dRow[(int)dtColIndex.RowVisible]).ToString() + "; ";

            int Flags = (int)dRow[(int)dtColIndex.Flags];
            s += "C=" + (((Flags & FlagHasChildren) == FlagHasChildren) ? "1" : "0") + "; ";
            s += "X=" + (((Flags & FlagIsExpanded) == FlagIsExpanded) ? "1" : "0") + "; ";
            s += "E=" + (((Flags & FlagIsEnum) == FlagIsEnum) ? "1" : "0");

            return s;
        }

        private void grid_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            // get datatable row from datagridview row
            DataRow dRow = (grid.Rows[e.RowIndex].DataBoundItem as DataRowView).Row;
            int Flags = (int)dRow[(int)dtColIndex.Flags];

            if ((Flags & FlagIsEnum) == FlagIsEnum)
            {
                EnumValueCell c = grid.Rows[e.RowIndex].Cells[(int)eGridColIndex.Value] as EnumValueCell;
                c.IsEnum = true;
            }
        }

        private void grid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (customPainting == false) return;

            // get datatable row from datagridview row
            DataRowView dRow = grid.Rows[e.RowIndex].DataBoundItem as DataRowView;

            // do row fcol and bcol
            object colObj = dRow[(int)dtColIndex.FCol];
            if (colObj != DBNull.Value)
            {
                e.CellStyle.ForeColor = (System.Drawing.Color)colObj;
            }
            colObj = dRow[(int)dtColIndex.BCol];
            if (colObj != DBNull.Value)
            {
                e.CellStyle.BackColor = (System.Drawing.Color)colObj;
            }
            if (e.ColumnIndex == (int)eGridColIndex.Value)
            {
                int valueMode = (int)dRow[(int)dtColIndex.ValueMode];
                if (valueMode == ValueModeEdited)
                    e.CellStyle.ForeColor = EditedColor;
                else if (valueMode == ValueModeError)
                    e.CellStyle.ForeColor = InputErrorColor;
            }

#if DEBUG_VISIBILITY
            if ((int)dRow[(int)dtColIndex.RowVisible] != 3)
            {
                e.CellStyle.ForeColor = Color.Gray;
            }
#endif

            if (e.ColumnIndex == (int)eGridColIndex.Debuginfo)
            {
                int dRowIndex = dRow.Row.Table.Rows.IndexOf(dRow.Row);
                e.Value = getRowDebuginfoStr(dRowIndex);
            }
        }

        private void grid_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            //Debug.Print("grid_CellPainting() Row=" + e.RowIndex.ToString() + " Col=" + e.ColumnIndex.ToString());

            indentWidth = expandIconPadding + expandIconWidth; // + ExpandIconPadding;

            if (customPainting == false) return;

            // Reg column?
            // custom paint for expand/collapse icons
            if ((e.ColumnIndex == (int)eGridColIndex.Reg) && (e.RowIndex >= 0))
            {
                // get datatable row from datagridview row
                DataRowView dRow = grid.Rows[e.RowIndex].DataBoundItem as DataRowView;

                int level = (int)dRow[(int)dtColIndex.RowLevel];
                int xpos = level * indentWidth;

                // erase background
                e.PaintBackground(e.ClipBounds, true);

                // Paint expand/collapse icon	
                int Flags = (int)dRow[(int)dtColIndex.Flags];
                if ((Flags & FlagHasChildren) == FlagHasChildren)
                {
                    int ypos = e.CellBounds.Y + ((e.CellBounds.Height - expandIconHeight) / 2);
                    if ((Flags & FlagIsExpanded) == FlagIsExpanded)
                    {
                        e.Graphics.DrawIcon(collapseIcon, e.CellBounds.X + xpos + expandIconPadding, ypos);
                    }
                    else
                    {
                        e.Graphics.DrawIcon(expandIcon, e.CellBounds.X + xpos + expandIconPadding, ypos);
                    }
                }
                // indent
                xpos += indentWidth;

                // paint text
                Rectangle trec = e.CellBounds;
                trec.X += xpos;
                TextRenderer.DrawText(e.Graphics, e.Value.ToString(), e.CellStyle.Font, trec, e.CellStyle.ForeColor, TextFormatFlags.Left | TextFormatFlags.VerticalCenter);

                e.Handled = true;
            }
            // Value column?
            // custom paint for enum-indicator icon
            if ((e.ColumnIndex == (int)eGridColIndex.Value) && (e.RowIndex >= 0))
            {
                // get datatable row from datagridview row
                DataRowView dRow = grid.Rows[e.RowIndex].DataBoundItem as DataRowView;

                //int level = (int)dRow[(int)dtColIndex.RowLevel];
                //int xpos = level * indentWidth;

                // erase background
                e.PaintBackground(e.ClipBounds, true);

                // paint text
                Rectangle trec = e.CellBounds;
                TextRenderer.DrawText(e.Graphics, e.Value.ToString(), e.CellStyle.Font, e.CellBounds, e.CellStyle.ForeColor, TextFormatFlags.Left | TextFormatFlags.VerticalCenter);

                // Paint enum indicator icon
                int Flags = (int)dRow[(int)dtColIndex.Flags];
                if ((Flags & FlagIsEnum) == FlagIsEnum)
                {
                    // position in upper right corner
                    int ypos = e.CellBounds.Y + (enumIndicatorIcon.Height / 9);
                    int xpos = e.CellBounds.X + e.CellBounds.Width - 1 - enumIndicatorIcon.Width - (enumIndicatorIcon.Width / 9);
                    e.Graphics.DrawIcon(enumIndicatorIcon, xpos, ypos);
                }

                e.Handled = true;
            }
        }

        private void setAutosizeCol(bool value)
        {
            if (value)
            {
                if (grid.AutoSizeColumnsMode != DataGridViewAutoSizeColumnsMode.Fill)
                {
                    SaveColumnWidths();
                    grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }
            }
            else
            {
                if (grid.AutoSizeColumnsMode != DataGridViewAutoSizeColumnsMode.None)
                {
                    grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                    RestoreColumnWidths();
                }
            }
        }

        private void SaveColumnWidths()
        {
            // Columns.Count is independent of columns visible
            columnWidths = new int[grid.Columns.Count];
            for (int i = 0; i < columnWidths.Length; ++i)
                columnWidths[i] = grid.Columns[i].Width;
        }

        private void RestoreColumnWidths()
        {
            if (columnWidths != null)
            {
                int m = Math.Min(columnWidths.Length, grid.Columns.Count);
                for (int i = 0; i < m; ++i)
                    grid.Columns[i].Width = columnWidths[i];
            }
        }

        private bool getShowVarProperties()
        {
            return grid.Columns[(int)eGridColIndex.Properties].Visible;
        }

        private void setShowVarProperties(bool value)
        {
            grid.Columns[(int)eGridColIndex.Properties].Visible = value;
        }

        private void setAutosizeRows(bool value)
        {
            if (value)
            {
                // autosize on
                grid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
            }
            else
            {
                // autosize off
                // change row heights manually
                grid.SuspendLayout();
                for (int gri = 0; gri < grid.Rows.Count; ++gri)
                    grid.Rows[gri].Height = grid.RowTemplate.Height;
                grid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
                grid.ResumeLayout(true);
            }
            mAutosizeRows = value;
        }

        private void setWrapRows(bool value)
        {
            if (value)
            {
                grid.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                grid.Columns[(int)eGridColIndex.Descr].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            }
            else
            {
                grid.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
                grid.Columns[(int)eGridColIndex.Descr].DefaultCellStyle.WrapMode = DataGridViewTriState.False;
            }
        }

        private void setAutosizeAndWrapRows(bool value)
        {
            if (value)
            {
                grid.SuspendLayout();

                // set autosize
                grid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;

                // set wrap
                grid.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                grid.Columns[(int)eGridColIndex.Descr].DefaultCellStyle.WrapMode = DataGridViewTriState.True;

                grid.ResumeLayout(true);
            }
            else
            {
                grid.SuspendLayout();

                // set autosize
                for (int gri = 0; gri < grid.Rows.Count; ++gri)
                    grid.Rows[gri].Height = grid.RowTemplate.Height;
                grid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;

                // set wrap
                grid.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
                grid.Columns[(int)eGridColIndex.Descr].DefaultCellStyle.WrapMode = DataGridViewTriState.False;

                grid.ResumeLayout(true);
            }
        }

        private void setCustomFormattingAndPainting(bool value)
        {
            customPainting = value;
            grid.Invalidate();
        }

        private void setLongTooltipTime(bool value)
        {
            if (value)
            {
                grid.SetMaxTooltipTime();
            }
        }

        public void ExpandAll()
        {
            TFocusPos focus;

            if (data == null)
                return;

            // Expand all, show all child rows
            // Keep focus on same row

            focus = GetFocusedDataRow();

            for (int dri = 0; dri < data.Rows.Count; ++dri)
            {
                DataRow dRow = data.Rows[dri];

                int Flags = (int)dRow[(int)dtColIndex.Flags];
                if ((Flags & FlagHasChildren) == FlagHasChildren)
                {
                    Flags |= FlagIsExpanded;
                    dRow[(int)dtColIndex.Flags] = Flags;
                }

                if (((int)dRow[(int)dtColIndex.RowVisible] & 1) == 0)
                {
                    dRow[(int)dtColIndex.RowVisible] = (int)dRow[(int)dtColIndex.RowVisible] | 1;
                }
            }

            FocusDataRow(focus);
        }

        public void CollapseAll()
        {
            // Collapse all
            // Show level 0 rows only

            TFocusPos focus;

            if (data == null)
                return;

            focus = GetFocusedDataRow();

            // every row with children: collapse
            // only rows with level 0 stay visible
            for (int dri = 0; dri < data.Rows.Count; ++dri)
            {
                DataRow dRow = data.Rows[dri];
                int Flags = (int)dRow[(int)dtColIndex.Flags];

                if ((Flags & FlagIsExpanded) == FlagIsExpanded)
                {
                    Flags &= ~FlagIsExpanded;
                    dRow[(int)dtColIndex.Flags] = Flags;
                }

                int level = (int)dRow[(int)dtColIndex.RowLevel];

                if ((level == 0) && (((int)dRow[(int)dtColIndex.RowVisible] & 1) == 0))
                {
                    dRow[(int)dtColIndex.RowVisible] = (int)dRow[(int)dtColIndex.RowVisible] | 1;
                }
                if ((level > 0) && (((int)dRow[(int)dtColIndex.RowVisible] & 1) == 1))
                {
                    dRow[(int)dtColIndex.RowVisible] = (int)dRow[(int)dtColIndex.RowVisible] & 2;
                }
            }

            FocusDataRow(focus);
        }

        private void UpdateFilter()
        {
            //bool valid = true;

            //Debug.Print("DoFilter(): filtval=" + filterStr);
            //Debug.Print(" dt.Rows.Count= " + data.Rows.Count.ToString());

            if (data == null)
            {
                return;
            }

            // get focused datarow
            TFocusPos dFocus = GetFocusedDataRow();

            if (mFilterString != string.Empty)
            {
                // got a filter string
                // update filtering

                int numRows = data.Rows.Count;
                bool[] bufRowsVisible = new bool[numRows];
                bool[] bufRowsParentExpand = new bool[numRows];

                // ignore case?
                StringComparison compareMode = StringComparison.CurrentCultureIgnoreCase;
                if (mFilterCaseSensitive) compareMode = StringComparison.CurrentCulture;

                // iterate all register and description strings
                // if contains search string: mark row to be visible
                foreach (ListItem li in RegList)
                {
                    if (li is RegItem)
                    {
                        RegItem ri = li as RegItem;
                        string regStr = ri.Reg.ToString();
                        string descStr = ri.Descr.ToString();
                        if ((regStr.IndexOf(mFilterString, compareMode) >= 0)            // simple string search
                            || (descStr.IndexOf(mFilterString, compareMode) >= 0))
                        {
                            // mark this row
                            bufRowsVisible[ri.dataRowIndex] = true;
                        }

                        // check children
                        // if child contains search string: mark child to be visible, and parent, and mark parent to be expanded
                        if (ri.childrenList != null)
                        {
                            foreach (ListItem li2 in ri.childrenList)
                            {
                                RegfieldItem fi = li2 as RegfieldItem;
                                descStr = fi.Descr.ToString();
                                if (descStr.IndexOf(mFilterString, compareMode) >= 0)
                                {
                                    // mark this row
                                    bufRowsVisible[fi.dataRowIndex] = true;
                                    // and its parent
                                    bufRowsVisible[ri.dataRowIndex] = true;
                                    // and mark parent to expand
                                    bufRowsParentExpand[ri.dataRowIndex] = true;
                                }
                            }
                        }
                    }
                }

                // update the RowVisible flags
                for (int dri = 0; dri < numRows; ++dri)
                {
                    DataRow dRow = data.Rows[dri];
                    if (bufRowsVisible[dri] && (((int)dRow[(int)dtColIndex.RowVisible] & 2) == 0))
                    {
                        dRow[(int)dtColIndex.RowVisible] = (int)dRow[(int)dtColIndex.RowVisible] | 2;
                    }
                    if ((bufRowsVisible[dri] == false) && (((int)dRow[(int)dtColIndex.RowVisible] & 2) == 2))
                    {
                        dRow[(int)dtColIndex.RowVisible] = (int)dRow[(int)dtColIndex.RowVisible] & 1;
                    }
                }

                // expand all parent rows that need to show children 
                for (int dri = 0; dri < numRows; ++dri)
                {
                    if (bufRowsParentExpand[dri])
                    {
                        int Flags = (int)data.Rows[dri][(int)dtColIndex.Flags];
                        if ((Flags & FlagIsExpanded) == 0)
                        {
                            Flags |= FlagIsExpanded;
                            data.Rows[dri][(int)dtColIndex.Flags] = Flags;
                            ExpandParent(dri);
                        }
                    }
                }
            }
            else
            {
                // no filtering
                // show all rows
                // leave expanded as is

                for (int dri = 0; dri < data.Rows.Count; ++dri)
                {
                    DataRow dRow = data.Rows[dri];

                    if ((((int)dRow[(int)dtColIndex.RowVisible] & 2) == 0))
                    {
                        dRow[(int)dtColIndex.RowVisible] = (int)dRow[(int)dtColIndex.RowVisible] | 2;
                    }
                }
            }

            FocusDataRow(dFocus);
        }

        /// <summary>
        /// After loading a datatable, all rows are visible
        /// must hide children of collapsed parents 
        /// </summary>
        private void UpdateDataExpandedCollapsed()
        {
            //Debug.Print("UpdateExpandedCollapsed()");

            // Check all parents, if collapsed: hide its children and grandchildren

            int numRows = data.Rows.Count;
            for (int dri = 0; dri < numRows; ++dri)
            {
                DataRow dRow = data.Rows[dri];
                int Flags = (int)dRow[(int)dtColIndex.Flags];
                if (((Flags & (FlagHasChildren | FlagIsExpanded)) == (FlagHasChildren)))
                {
                    CollapseParent_ref(ref dri);
                    --dri;
                }
            }
        }

        /// <summary>
        /// Get focused data row/col
        /// </summary>
        /// <returns>TFocusPos.Row = -1 if no row was focused; >= 0 if row was focused</returns>
        private TFocusPos GetFocusedDataRow()
        {
            TFocusPos focusPos;

            focusPos.Row = -1;
            focusPos.Col = 0;

            if ((grid.CurrentCell != null) && (grid.CurrentCell.RowIndex >= 0))
            {
                DataRowView dRow = grid.Rows[grid.CurrentCell.RowIndex].DataBoundItem as DataRowView;
                int dRowIndex = data.Rows.IndexOf(dRow.Row);
                focusPos.Row = dRowIndex;
                focusPos.Col = grid.CurrentCell.ColumnIndex;
            }

            return focusPos;
        }

        /// <summary>
        /// Focus the grid on a certain datarow, focus.Row is a datarow index
        /// If the row is no longer in the filtered set, move to earlier row
        /// </summary>
        private void FocusDataRow(TFocusPos focus)
        {
            if (focus.Row >= 0)
            {
                // find matching gridrow
                // dri is data row index
                // gri is grid row index
                int dri = 0;
                int gri = 0;
                while (gri < grid.RowCount)
                {
                    DataRowView dRow = grid.Rows[gri].DataBoundItem as DataRowView;
                    dri = data.Rows.IndexOf(dRow.Row);
                    if (dri >= focus.Row)
                        break;
                    ++gri;
                }
                if ((gri > 0) && (dri > focus.Row))
                {
                    --gri;
                }

                if (gri >= grid.RowCount)
                {
                    gri = grid.RowCount - 1;
                }

                // focus, if any
                if (gri >= 0)
                {
                    // set to null first
                    // to have a focusbar if the focus is set to the same gridrow as before
                    // to scroll focused line into view
                    grid.CurrentCell = null;
                    grid.CurrentCell = grid.Rows[gri].Cells[focus.Col];
                }
            }
        }


        /// <summary>
        /// Check if formatToCheck is in list of formats
        /// </summary>
        static bool IsFormatInList(Global.eFormat formatToCheck, params Global.eFormat[] validFormats)
        {
            foreach (var format in validFormats)
            {
                if (formatToCheck == format)
                    return true;
            }
            return false;
        }


        #endregion

        private void grid_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            //Debug.Print("dataGridView1_CellMouseClick()");

            // Reg column? not header? left click?
            if ((e.ColumnIndex == (int)eGridColIndex.Reg)
                    && (e.RowIndex >= 0)
                    && (e.Button == MouseButtons.Left))
            {
                HandleRegCellClick(e);
            }
        }

        private void HandleRegCellClick(DataGridViewCellMouseEventArgs e)
        {
            //Debug.Print("HandleRegCellClick() on Reg column, e.RowIndex=" + e.RowIndex.ToString());

            // get datatable row from datagridview row
            DataRowView drv = grid.Rows[e.RowIndex].DataBoundItem as DataRowView;
            DataRow dRow = drv.Row;                 // data row

            int Flags = (int)dRow[(int)dtColIndex.Flags];
            if ((Flags & FlagHasChildren) == FlagHasChildren)
            {
                //Debug.Print(" isExpandable");
                int level = (int)dRow[(int)dtColIndex.RowLevel];

                /* 
                //op het icon geklikt? stricte check
                int iconXPos = (indentWidth * level) + ExpandIconPadding;
                int iconYPos = (datagridView1.Rows[e.RowIndex].Height - ExpandIconHeight) / 2;
                Rectangle hitRect = new Rectangle(iconXPos, iconYPos, ExpandIconWidth, ExpandIconHeight);
                */

                //op het icon geklikt? tolerante check, je mag er ook wat naast klikken
                int clickMargin = 3;
                int iconXPos = (indentWidth * level) + expandIconPadding - clickMargin;
                int iconYPos = (grid.Rows[e.RowIndex].Height - expandIconHeight - (2 * clickMargin)) / 2;

                Rectangle hitRect = new Rectangle(iconXPos, iconYPos, expandIconWidth + (2 * clickMargin), expandIconHeight + (2 * clickMargin));
                // end toleranter

                if (hitRect.Contains(e.X, e.Y))
                {
                    //Debug.Print(" hitRect.Contains");
                    // toggle
                    Flags = (int)dRow[(int)dtColIndex.Flags];
                    Flags ^= FlagIsExpanded;
                    dRow[(int)dtColIndex.Flags] = Flags;

                    // update
                    int dri = data.Rows.IndexOf(dRow);      // data row index
                    if ((Flags & FlagIsExpanded) == FlagIsExpanded)
                    {
                        //Debug.Print(" UpdateExpandParent");
                        ExpandParent(dri);
                    }
                    else
                    {
                        //Debug.Print(" UpdateCollapseParent");
                        CollapseParent(dri);
                    }
                }
            }
        }

        /// <summary>
        /// Expand parent
        /// Show the children, and if child status=expanded then show its childern too.
        /// To be called for expandable rows only.
        /// </summary>
        /// <param name="dRowIndex">data row index</param>
        private void ExpandParent(int dRowIndex)
        {
            // Expand parent
            // show the children
            // recursive, if child is expanded then show its childern too

            // call recursive worker
            ExpandParent_rec(ref dRowIndex);
        }

        /// <summary>
        /// Expand a parent, recursively
        /// To be called from ExpandParent() only
        /// </summary>
        /// <param name="dri">data row index, In: where to start, Out: next row after this parent's children and grandchildren</param>
        private void ExpandParent_rec(ref int dri)
        {
            DataRow dRow = data.Rows[dri];

            int parLevel = (int)dRow[(int)dtColIndex.RowLevel];
            ++dri;
            while (dri < data.Rows.Count)
            {
                dRow = data.Rows[dri];

                int level = (int)dRow[(int)dtColIndex.RowLevel];
                if (level <= parLevel)
                    break;

                // set visible
                if (((int)dRow[(int)dtColIndex.RowVisible] & 1) == 0)
                {
                    dRow[(int)dtColIndex.RowVisible] = (int)dRow[(int)dtColIndex.RowVisible] | 1;
                }

                // has children?
                int Flags = (int)dRow[(int)dtColIndex.Flags];
                if ((Flags & FlagHasChildren) == FlagHasChildren)
                {
                    // is status=expanded: expand
                    if ((Flags & FlagIsExpanded) == FlagIsExpanded)
                    {
                        ExpandParent_rec(ref dri);
                    }
                    else
                    {
                        // not expanded: skip all children
                        ++dri;
                        while (dri < data.Rows.Count)
                        {
                            dRow = data.Rows[dri];
                            int level2 = (int)dRow[(int)dtColIndex.RowLevel];
                            if (level2 <= level)
                                break;
                            ++dri;
                        }
                    }
                }
                else
                {
                    ++dri;
                }
            }
        }

        /// <summary>
        /// Collapse parent
        /// Hide this parent's children, and their children
        /// Calls CollapseParent_ref to do the real work
        /// </summary>
        /// <param name="dRowIndex">data row index</param>
        private void CollapseParent(int dRowIndex)
        {
            // Collapse parent
            // hide the children, and their children
            // no recursing required

            CollapseParent_ref(ref dRowIndex);
        }

        /// <summary>
        /// Collapse parent
        /// Hide this parent's children, and their children
        /// </summary>
        /// <param name="dri">data row index, In: where to start, Out: next row after this parent's children and grandchildren</param>
        private void CollapseParent_ref(ref int dri)
        {
            DataRow dRow = data.Rows[dri];

            // keeping focus is okay
            // focused row is parent of rows that are hidden

            int parLevel = (int)dRow[(int)dtColIndex.RowLevel];
            ++dri;
            while (dri < data.Rows.Count)
            {
                dRow = data.Rows[dri];

                int level = (int)dRow[(int)dtColIndex.RowLevel];
                if (level > parLevel)
                {
                    // hide
                    if (((int)dRow[(int)dtColIndex.RowVisible] & 1) == 1)
                    {
                        dRow[(int)dtColIndex.RowVisible] = (int)dRow[(int)dtColIndex.RowVisible] & 2;
                    }
                    ++dri;
                }
                else
                {
                    break;
                }
            }
        }

        private void grid_KeyDown(object sender, KeyEventArgs e)
        {
            //Debug.Print("e.Control=" + e.Control.ToString() + ", "
            //    + "e.KeyCode=" + e.KeyCode.ToString() + ", " 
            //    + "grid.ColumnIndex=" + grid.CurrentCell.ColumnIndex.ToString() + ", "
            //    + "grid.RowIndex=" + grid.CurrentCell.RowIndex.ToString()
            //    );  

            // use F2 key to start edit via keyboard
            if (e.KeyCode == Keys.F2)
            {
                TryBeginEdit(grid.CurrentCell.RowIndex);

                e.Handled = true;
            }
            
            // programmatically handle Control-C for copying grid data 
            else if (e.Control && (e.KeyCode == Keys.C))
            {
                // if value columns is selected: copy only the value string
                if (grid.CurrentCell.ColumnIndex == (int)eGridColIndex.Value)
                {
                    CopyRowValueToClipboard(grid.CurrentCell.RowIndex);
                }
                else
                {
                    CopyRowDataToClipboard(grid.CurrentCell.RowIndex);
                }
                
                e.Handled = true;
            }
        }

        /// <summary>
        /// Start edit dmv KeyPress letter, cijfer, leesteken
        /// </summary>
        private void grid_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Option: start value editing on any types character
            if (mStartEditOnAnyCharacter)
            {
                // accept letters, digits and punctuation like ",;
                // do not accept control characters
                if (Char.IsLetterOrDigit(e.KeyChar) || Char.IsPunctuation(e.KeyChar))
                {
                    editingBufferedChar = e.KeyChar;

                    // go edit cell
                    TryBeginEdit(grid.CurrentCell.RowIndex);

                    // key handled
                    e.Handled = true;
                }
            }
        }

        /// <summary>
        /// Start edit dmv cell doubleclick
        /// </summary>
        private void grid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // do not react on clicks on headers
            if (e.RowIndex >= 0)
            {
                // react only on clicks in value column
                if ((e.ColumnIndex == (int)eGridColIndex.Value))
                {
                    // go edit cell
                    TryBeginEdit(e.RowIndex);
                }
            }
        }

        /// <summary>
        /// Editing control shows
        /// if we have a buffered key: send it to this control
        /// </summary>
        private void grid_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            // edit in the value column?
            if (grid.CurrentCell.ColumnIndex == (int)eGridColIndex.Value)
            {
                // is it a EnumValueEditingControl?
                EnumValueEditingControl cb = e.Control as EnumValueEditingControl;
                if (cb != null)
                {
                    // get IsEnum property 
                    EnumValueCell c = this.grid.CurrentCell as EnumValueCell;
                    if (c.IsEnum)
                    {
                        // editingReg and editingRegfield are already set when edit started
                        // get the enum dictionary of that register/field
                        Global.CEnum EnumDict = null;
                        if (editingRegfield != null)
                        {
                            EnumDict = editingRegfield.EnumDict;
                        }
                        else
                        {
                            EnumDict = editingReg.EnumDict;
                        }

                        // fill the combo dropdown with the enum values
                        cb.Items.Clear();
                        if (EnumDict.Dict != null)
                        {
                            foreach (var item in EnumDict.Dict)
                            {
                                cb.Items.Add(item.Key.ToString() + " (" + item.Value + ")");
                            }
                        }
                    }

                    // got a buffered key?
                    // then send it to the editing control
                    if (editingBufferedChar > 0)
                    {
                        PostMessage(cb.Handle, WM_CHAR, (int)editingBufferedChar, 0);
                        editingBufferedChar = (char)0;
                    }
                }
                
                // is it a TextBoxEditingControl?
                DataGridViewTextBoxEditingControl ed = e.Control as DataGridViewTextBoxEditingControl;
                if (ed != null)
                {
                    // got a buffered key?
                    // then send it to the editing control
                    if (editingBufferedChar > 0)
                    {
                        // then send it to the editing control
                        PostMessage(ed.Handle, WM_CHAR, (int)editingBufferedChar, 0);
                        editingBufferedChar = (char)0;
                    }
                }
            }
        }

        public void SetZoom(float factor)
        {
            if (factor != currentZoomFactor)
            {
                currentZoomFactor = factor;

                // zoom this grid
                float newFontSize = originalFontSize * currentZoomFactor;
                Font newFont = new Font(Font.FontFamily, newFontSize, Font.Style, Font.Unit);
                grid.ColumnHeadersDefaultCellStyle.Font = newFont;
                grid.DefaultCellStyle.Font = newFont;

                // adjust the grid icons 
                SetIconSizes();
            }
        }

        /// <summary>
        /// Get the next group read block.
        /// Group read: read as many consecutive vars as fit in maxFrameSize.
        /// Or read a single var if it exceeds maxFrameSize. The caller will execute that read in chunks.
        /// </summary>
        /// <param name="maxReadRegisters">maximum registers to read</param>
        /// <returns>group or null at the end</returns>
        public CReadGroup GetNextReadGroup(CReadGroup group, int maxReadRegisters)
        {
            // no registers?
            if (RegList.Count == 0)
            {
                return null;
            }

            // start new group?
            if (group == null)
            {
                group = new CReadGroup();
            }

            // voorlopig gaan we er van uit dat in RegList level 0 alleen registers zitten

            int iLineIndex = group.lineIndex + group.lineCount;
            bool isGrouping = false;
            UInt32 iNextRegister = 0;

            while (iLineIndex < RegList.Count)
            {
                ListItem Lit = RegList[iLineIndex];
                RegItem Reg = Lit as RegItem;

                if (isGrouping)
                {
                    // find next readable register in group
                    // is consecutive and readable?
                    // fits in readgroup?
                    if ( (Reg.Readable == true)
                        && (Reg.Reg == iNextRegister)
                        && ((group.readRegCount + Reg.Size) <= maxReadRegisters))    
                    {
                        group.lineCount += 1;
                        group.readRegCount += Reg.Size;
                        iNextRegister = Reg.Reg + (UInt32)Reg.Size;
                        // try to get more
                    }
                    else
                    {
                        // end group
                        break;
                    }
                }
                else 
                {
                    // find next readable register to start new group
                    if (Reg.Readable)
                    {
                        // start group read
                        group.lineIndex = iLineIndex;
                        group.lineCount = 1;
                        group.readFirstReg = Reg.Reg;       
                        group.readRegCount = Reg.Size;
                        iNextRegister = Reg.Reg + (UInt32)Reg.Size;
                        isGrouping = true;
                        // got room for more?
                        if (group.readRegCount < maxReadRegisters)
                        {
                            // loop for more                            
                        }
                        else
                        {
                            // end group
                            break;
                        }
                    }
                }

                ++iLineIndex;
            }

            // got more?
            if (isGrouping)
            {
                return group;
            }

            // no more
            return null;
        }

        /// <summary>
        /// Get a list of group read commands
        /// </summary>
        /// <param name="maxFrameSize">maximum size of the response frame (in bytes)</param>
        /// <returns></returns>
        public TVarProperties GetVarPropertiesByIndex(int lineIndex)
        {
            //ModbusGridView.TRegProperties prop = mrv.GetRegisterPropertiesByIndex(0);

            TVarProperties p;

            RegItem reg = RegList[lineIndex] as RegItem;

            p.Reg = reg.Reg;
            p.Descr = reg.Descr;
            p.Size = reg.Size;
            p.Format = reg.Format;
            p.DisplayFormat = reg.DisplayFormat;
            p.MinLen = reg.MinLen;
            p.Decimals = reg.Decimals;
            p.Readable = reg.Readable;
            p.Writeable = reg.Writeable;
            p.EnumDict = reg.EnumDict;

            return p;
        }

        private delegate void SetRegistersReadDataDelegate(int lineIndex, int lineCount, short[] readData);

        /// <summary>
        /// Put the data we have read in the grid
        /// Use delegate/invoke to make it threadsafe
        /// </summary>
        /// <param name="rg">Struct with readgroup parameters</param>
        /// <param name="readData">Array of raw read data</param>
        public void SetRegistersReadData(int lineIndex, int lineCount, short[] readData)
        {
            // make this threadsafe, is called from polling thread
            if (this.InvokeRequired)
            {
                SetRegistersReadDataDelegate del = new SetRegistersReadDataDelegate(this.SetRegistersReadData);
                this.Invoke(del, lineIndex, lineCount, readData);
            }
            else
            {
                int dataIndex = 0;
                for (int linei = lineIndex; linei < (lineIndex + lineCount); ++linei)
                {
                    RegItem r = RegList[linei] as RegItem;
                    Array.Copy(readData, dataIndex, r.DataInt16Array, 0, r.Size);
                    dataIndex += r.Size;

                    // update register value, and its children
                    UpdateRegisterValue(r);
                }
            }
        }

        private delegate void SetRegistersReadErrorDelegate(int lineIndex, int lineCount, string ErrorString);

        /// <summary>
        /// Put the read error message in the grid
        /// Use delegate/invoke to make it threadsafe
        /// </summary>
        /// <param name="rg">Struct with readgroup parameters</param>
        /// <param name="ErrorString">The error message</param>
        public void SetRegistersReadError(int lineIndex, int lineCount, string ErrorString)
        {
            // make this threadsafe, is called from polling thread
            if (this.InvokeRequired)
            {
                SetRegistersReadErrorDelegate del = new SetRegistersReadErrorDelegate(this.SetRegistersReadError);
                this.Invoke(del, lineIndex, lineCount, ErrorString);
            }
            else
            {
                for (int linei = lineIndex; linei < (lineIndex + lineCount); ++linei)
                {
                    RegItem r = RegList[linei] as RegItem;

                    // update register value, and its children
                    SetRegisterReadError(r, ErrorString);
                }
            }
        }

        /// <summary>
        /// Update a register value, and its children
        /// </summary>
        /// <param name="r"></param>
        private void UpdateRegisterValue(RegItem r)
        {
            // update the value string and grid
            UpdateRegisterValueString(r);

            // has bitfields? then update the bitfield values too
            if (r.childrenList != null)
            {
                // bitfields can only exists in a integer-type var
                r.ValueUInt32 = Converts.Int16ArrayToUInt32Value(r.DataInt16Array, r.Size, Global.eType.Int);
        
                for (int ci = 0; ci < r.childrenList.Count; ++ci)
                {
                    RegfieldItem bf = r.childrenList[ci] as RegfieldItem;
                    Int32 NewFieldValue = GetBitfieldValue(r.ValueUInt32, bf.Pos, bf.Width, bf.DisplayFormat == Global.eFormat.Int);
                    // store new value
                    bf.Value32 = NewFieldValue;
                    // update the value string and grid
                    UpdateRegisterFieldValue(bf);
                }
            }
        }

        /// <summary>
        /// Update a register value row
        /// </summary>
        /// <param name="r"></param>
        private void UpdateRegisterValueString(RegItem r)
        {
            // update value string
            r.ValueStr = CValueToString.Int16RegistersToValueString(r.DataInt16Array, r.Format, r.DisplayFormat, r.Size * 16, r.Decimals, r.MinLen, r.EnumDict);

            // update the grid
            // but only if is is not beging edited
            DataRow dRow = r.dataRow;
            if ((dRow != editingDatarow) && (dRow != inputErrorRow))
            {
                // update value in grid, only if it changes to minimize flicker
                if ((r.ValueStr != GetDbStr(dRow[(int)dtColIndex.ValueStr]))
                    || ((int)dRow[(int)dtColIndex.ValueMode] != ValueModeNormal))
                {
                    dRow[(int)dtColIndex.ValueStr] = r.ValueStr;
                    // revert to normal color
                    dRow[(int)dtColIndex.ValueMode] = ValueModeNormal;
                }
            }
        }

        /// <summary>
        /// Update a bitfield value row
        /// </summary>
        /// <param name="f"></param>
        private void UpdateRegisterFieldValue(RegfieldItem f)
        {
            // update field value string
            f.ValueStr = CValueToString.IntValueToValueString(f.Value32, f.Width, f.DisplayFormat, 0, f.MinLen, f.EnumDict);

            // update the grid
            // but only if is is not beging edited
            DataRow dRow = f.dataRow;
            if ((dRow != editingDatarow) && (dRow != inputErrorRow))
            {
                // update value in grid, only if it changes to minimize flicker
                if ((f.ValueStr != GetDbStr(dRow[(int)dtColIndex.ValueStr]))
                    || ((int)dRow[(int)dtColIndex.ValueMode] != ValueModeNormal))
                {
                    dRow[(int)dtColIndex.ValueStr] = f.ValueStr;
                    // revert to normal color
                    dRow[(int)dtColIndex.ValueMode] = ValueModeNormal;
                }
            }
        }

        /// <summary>
        /// Update a register value with error string, and its children
        /// </summary>
        /// <param name="r"></param>
        /// <param name="ErrorString"></param>
        private void SetRegisterReadError(RegItem r, string ErrorString)
        {
            // update the grid
            // but only if is is not beging edited
            DataRow dRow = r.dataRow;
            if ((dRow != editingDatarow) && (dRow != inputErrorRow))
            {
                // update value in grid, only if it changes to minimize flicker
                string dispStr = r.ValueStr + "  ?  <" + ErrorString + ">";
                if ((dispStr != GetDbStr(dRow[(int)dtColIndex.ValueStr]))
                    || ((int)dRow[(int)dtColIndex.ValueMode] != ValueModeError))
                {
					// set error text
					dRow[(int)dtColIndex.ValueStr] = dispStr;
                    // set error color
                    dRow[(int)dtColIndex.ValueMode] = ValueModeError;
                }
            }

            // has bitfields? then update the bitfield values too
            if (r.childrenList != null)
            {
                for (int ci = 0; ci < r.childrenList.Count; ++ci)
                {
                    RegfieldItem bf = r.childrenList[ci] as RegfieldItem;
                    // show old value + error string
                    string dispStr = bf.ValueStr + "  ?  <" + ErrorString + ">";
                    // update the grid
                    // but only if is is not beging edited
                    dRow = bf.dataRow;
                    if ((dRow != editingDatarow) && (dRow != inputErrorRow))
                    {
                        // update value in grid, only if it changes to minimize flicker
                        if ((dispStr != GetDbStr(dRow[(int)dtColIndex.ValueStr]))
                            || ((int)dRow[(int)dtColIndex.ValueMode] != ValueModeError))
                        {
							// set error text
                            dRow[(int)dtColIndex.ValueStr] = dispStr;
                            // set error color
                            dRow[(int)dtColIndex.ValueMode] = ValueModeError;                        }
                    }
                }
            }
        }

        private void SetRegisterInputError(DataRow dRow, string errorString)
        {
            // prevent race conditions
            inputErrorTimer.Stop();

            // clear previous error
            if (inputErrorRow != null)
            {
                inputErrorRow = null;
            }

            // set error row
            inputErrorRow = dRow;
            dRow[(int)dtColIndex.ValueStr] = errorString;
            // set error color
            dRow[(int)dtColIndex.ValueMode] = ValueModeError;

            // start timer
            inputErrorTimer.Start();
        }

        /// <summary>
        /// Entry error timer expired
        /// </summary>
        private void InputErrorTimer_Tick(object sender, EventArgs e)
        {
            // prevent race conditions
            inputErrorTimer.Stop();

            // clear the row's error
            if (inputErrorRow != null)
            {
                inputErrorRow = null;
            }
        }


        private Int32 GetBitfieldValue(UInt32 RegValue, int Position, int BitWidth, bool bDoSignExtend)
        {
            Int32 Result;

            UInt32 Mask = 0xFFFFFFFF >> (32 - BitWidth);
            Result = (Int32)(RegValue >> (Int32)Position) & (Int32)Mask;

            if (bDoSignExtend && (BitWidth >= 2))
            {
                // value is signed
                // do a sign extend
                Result = (Result << (32 - BitWidth)) >> (32 - BitWidth);
            }

            return Result;
        }

        private bool TryBeginEdit(int gridRowIndex)
        {
            /* 
            // in read-only mode?
            if (mConnectionIsOpen)
            {
                // Read only
                OnBeepEvent();
                return false;
            }
            */

            // get datatable row from datagridview row
            // datagridrowindex != datatableindex if datagridview rows are invisible
            DataRow dRow = (grid.Rows[gridRowIndex].DataBoundItem as DataRowView).Row;

            // find item in RegList
            FindListItemsByRow(dRow, out editingReg, out editingRegfield);

            /*
            // is this var writeable?
            if (editingReg.Writeable == false)
            {
                // not writeable
                OnBeepEvent();
                return false;
            }
            */

            // if the value string already has an error message, then clean that
            string cleanedValueString;
            if (valueStrCleanErrorMessage(GetDbStr(dRow[(int)dtColIndex.ValueStr]), out cleanedValueString))
            {
                dRow[(int)dtColIndex.ValueStr] = cleanedValueString;
            }

            // make the value column the one to edit
            grid.CurrentCell = grid.Rows[gridRowIndex].Cells[(int)eGridColIndex.Value];

            // revert to normal color
            dRow[(int)dtColIndex.ValueMode] = ValueModeNormal;

            // start edit cell
            //Debug.Print("set editingDatarow = dRow");
            editingDatarow = dRow;
            //Debug.Print("after set editingDatarow = dRow");
            return grid.BeginEdit(true);
        }

        private bool valueStrCleanErrorMessage(string input, out string cleanedStr)
        {
            cleanedStr = "";

            if (input.StartsWith("<"))
            {
                int closingIndex = input.IndexOf('>');
                if (closingIndex > 0)
                {
                    // Remove the tag and any leading space after it
                    cleanedStr = input.Substring(closingIndex + 1).TrimStart();
                    return true;
                }
            }

            return false;
        }

        private void grid_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            //Debug.Print("--grid_CellBeginEdit: e.rowindex=" + e.RowIndex.ToString() + ", grid.rowcount=" + grid.RowCount.ToString() + "/" + grid.Rows.Count.ToString());

            grid.EscapeDetected = false;
        }

        private void grid_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            Int16[] writingDataBuffer = null;   // temp data buffer for writing, avoid data being overwritten by polling process

            //Debug.Print("grid_CellEndEdit");

            // get datatable row from datagridview row
            // datagridrowindex != datatableindex if datagridview rows are invisible
            DataRow dRow = (grid.Rows[e.RowIndex].DataBoundItem as DataRowView).Row;
            // get new value string
            string newValueStr = GetDbStr(dRow[(int)dtColIndex.ValueStr]);

            // edit canceled?
            if (grid.EscapeDetected)
            {
                // escape was pressed
                // ignore edit
            }
            else if (mConnectionIsOpen == false)
            {
                // show that the value was not accepted
                string errorStr = "CONNECTION NOT OPEN";
                string displayStr = "<" + errorStr + ">";
                SetRegisterInputError(dRow, displayStr);

                // report error on status bar
                OnReportErrorMessageEvent("Error: " + errorStr);
                // beep
                OnBeepEvent();
            }
            else if (editingReg.Writeable == false)
            {
                // show that the value was not accepted
                string errorStr = "REGISTER IS READ-ONLY";
                string displayStr = "<" + errorStr + ">";
                SetRegisterInputError(dRow, displayStr);

                // report error on status bar
                OnReportErrorMessageEvent("Error: " + errorStr);
                // beep
                OnBeepEvent();
            }
            else
            {
                //editReg and editRegfield were set in TryBeginEdit

                Global.eFormat edFormat = 0;
                int edWidth = 0;
                int edDecimals = 0;
                bool edIsEnum = false;
                bool bIsValid = true;
                string errorString = "error";

                // if it is a bitfield:
                //      the parent register must be a 16/32 bit integer type, no float, not string, ...
                //      get the new bitfield as an integer value
                //      insert it in the parent register integer value
                //      convert parent register integer value to Int16[] registers 
                // if it is a register:
                //      convert valuestring to Int16[] registers, 1, 2 of more, all types
                // write Int16[] register(s)

                // editing a field or register?
                if (editingRegfield != null)
                {
                    // editing a field

                    edFormat = editingRegfield.DisplayFormat;
                    edWidth = editingRegfield.Width;
                    edDecimals = editingRegfield.Decimals;
                    edIsEnum = editingRegfield.EnumDict != null;

                    // bitfields can only exists in a integer-type var
                    // is it an enum?
                    // then remove the enum text part
                    if (edIsEnum)
                    {
                        newValueStr = CValueToString.CleanValuestrEnumText(newValueStr);
                    }

                    // convert to int, within given Width
                    try
                    {
                        Int32 newFieldValue = CValueToString.ValueStringToIntValue(newValueStr, edWidth, edDecimals);

                        // insert bitfield value in register value
                        Int32 newRegValue = InsertBitfieldInValue(editingReg.ValueUInt32, newFieldValue, editingRegfield.Pos, edWidth);

                        // convert to Int16[] for write
                        writingDataBuffer = Converts.Int32ToInt16Array(newRegValue, editingReg.Size);

                        // update grid
                        editingReg.DataInt16Array = (Int16[])writingDataBuffer.Clone();
                        UpdateRegisterValue(editingReg);
                    }
                    catch(Exception exc)
                    {
                        // invalid
                        bIsValid = false;
                        errorString = exc.Message;
                    }
                }
                else
                {
                    // editing a register

                    edFormat = editingReg.Format;
                    edWidth = editingReg.Size * 16;
                    edDecimals = editingReg.Decimals;
                    edIsEnum = editingReg.EnumDict != null;

                    // is it an enum?
                    // then remove the enum text part
                    if (edIsEnum)
                    {
                        newValueStr = CValueToString.CleanValuestrEnumText(newValueStr);
                    }

                    // convert value string to int16[] values
                    try
                    {
                        // convert to Int16[] for write
                        bool allowIntegers = (edFormat == Global.eFormat.Int) || (edFormat == Global.eFormat.UInt) || (edFormat == Global.eFormat.Bin) || (edFormat == Global.eFormat.Hex) || (edFormat == Global.eFormat.ASCII) || ((edFormat == Global.eFormat.Float) && (editingReg.Size == 2));
                        bool allowFloat = (edFormat == Global.eFormat.Float);
                        bool allowString = (edFormat == Global.eFormat.String);
                        writingDataBuffer = CValueToString.ValueStringToInt16Registers(newValueStr, editingReg.Size, edDecimals, allowIntegers, allowFloat, allowString);

                        // update grid
                        // in case of string: the writingDataBuffer is just as large as needed, so it can be smaller than editingReg.DataInt16Array
                        Array.Copy(writingDataBuffer, editingReg.DataInt16Array, writingDataBuffer.Length);
                        UpdateRegisterValue(editingReg);
                    }
                    catch (Exception exc)
                    {
                        // invalid
                        bIsValid = false;
                        errorString = exc.Message;
                    }
                }

                // if value is valid: write
                // otherwise show error
                if (bIsValid)
                {
                    // valid 

                    // the entered string is shown as the cell's new value, till the next read
                    // mark the register as edited
                    dRow[(int)dtColIndex.ValueMode] = ValueModeEdited;

                    // call write handler
                    if (OnRegisterWrite != null)
                    {
                        RegisterWriteEventArgs rwe = new RegisterWriteEventArgs();
                        rwe.Reg = editingReg.Reg;
                        rwe.Values = writingDataBuffer; 
                        OnRegisterWrite(this, rwe);
                    }
                }
                else
                {
                    // invalid value

                    // show that the new value was not accepted
                    // error message and entered value
                    string displayStr = "<" + errorString.ToUpper() + "> " + newValueStr;
                    SetRegisterInputError(dRow, displayStr);

                    // report error on status bar
                    OnReportErrorMessageEvent("Input error: " + errorString.ToUpper() + "  for register " + editingReg.Reg.ToString() + ": value: " + newValueStr);
                    // beep
                    OnBeepEvent();
                }
            }

            //Debug.Print("set editingDatarow = null");
            editingDatarow = null;
            //Debug.Print("after set editingDatarow = null");
        }

        private Int32 InsertBitfieldInValue(UInt32 Value, Int32 BitfieldValue, int Position, int BitWidth)
        {
            UInt32 Mask = 0xFFFFFFFF >> (32 - BitWidth);
            BitfieldValue = (Int32)(BitfieldValue & Mask);
            Int32 Newvalue = (Int32)(((UInt32)Value & ~(Mask << Position))
                                      | ((UInt32)BitfieldValue << Position));

            return Newvalue;
        }

        private ListItem FindListItemByRow(DataRow dRow)
        {
            foreach (ListItem li in RegList)
            {
                if (li.dataRow == dRow)
                {
                    // found
                    return li;
                }
            }

            // not found
            throw new Exception("dRow not found in RegList");
        }

        private void FindListItemsByRow(DataRow dRow, out RegItem regIt, out RegfieldItem regfieldIt)
        {
            regIt = null;
            regfieldIt = null;

            foreach (ListItem li in RegList)
            {
                if (li is RegItem)
                {
                    RegItem ri = li as RegItem;
                    if (ri.dataRow == dRow)
                    {
                        // found
                        regIt = ri;
                        return;
                    }

                    // try children
                    if (ri.childrenList != null)
                    {
                        foreach (ListItem li2 in ri.childrenList)
                        {
                            if (li2 is RegfieldItem)
                            {
                                RegfieldItem fi = li2 as RegfieldItem;
                                if (fi.dataRow == dRow)
                                {
                                    // found
                                    regIt = ri;
                                    regfieldIt = fi;
                                    return;
                                }
                            }
                        }
                    }
                }
                else
                    throw new Exception("li is not RegItem");
            }

            // not found
            throw new Exception("dRow not found in RegList");
        }

        /// <summary>
        /// Convert datatable nullstring (DBNull.Value) to empty string.
        /// Avoids exceptions.
        /// </summary>
        /// <param name="dbObject"></param>
        /// <returns>string</returns>
        private string GetDbStr(object dbObject)
        {
            if (dbObject != DBNull.Value)
            {
                return (string)dbObject;
            }

            return string.Empty;
        }

        /// <summary>
        /// Beep callback
        /// </summary>
        private void OnBeepEvent()
        {
            // event 
            if (OnBeep != null)
            {
                OnBeep(this);
            }
        }

        /// <summary>
        /// Error message event
        /// </summary>
        private void OnReportErrorMessageEvent(string s)
        {
            if (this.OnReportErrorMessage != null)
            {
                OnReportErrorMessage(s);
            }
        }

        private void grid_MouseUp(object sender, MouseEventArgs e)
        {
            // Load context menu on right mouse click
            DataGridView.HitTestInfo hitTestInfo;

            // Right button?
            if (e.Button == MouseButtons.Right)
            {
                // hit in cell?
                hitTestInfo = this.grid.HitTest(e.X, e.Y);
                if (hitTestInfo.Type == DataGridViewHitTestType.Cell)
                {
                    // Register column?
                    if (hitTestInfo.ColumnIndex == (int)eGridColIndex.Reg)
                    {
                        // remember which gridrow was clicked
                        rightclickRowIndex = hitTestInfo.RowIndex;

                        // focus this row
                        grid.CurrentCell = null;
                        grid.CurrentCell = grid.Rows[rightclickRowIndex].Cells[0];

                        // show menu
                        ContextMenuForRegColumn.Show(this, e.X, e.Y);
                    }
                    else
                    // Value column?
                    if (hitTestInfo.ColumnIndex == (int)eGridColIndex.Value)
                    {
                        // remember which gridrow was clicked
                        rightclickRowIndex = hitTestInfo.RowIndex;

                        // focus this row
                        grid.CurrentCell = null;
                        grid.CurrentCell = grid.Rows[rightclickRowIndex].Cells[0];

                        // get current format
                        DataRow dRow = (grid.Rows[rightclickRowIndex].DataBoundItem as DataRowView).Row;
                        Global.eFormat varFormat = GetLineValueFormat(dRow);
                        Global.eFormat varDisplayFormat = GetLineValueDisplayFormat(dRow);

                        // show possible formats
                        // show grey if not allowed
                        // show current format with check mark
                        bool isBitfield = GetLineIsBitfield(dRow);
                        int curWidth = GetLineValueWidth(dRow);
                        // use Format to determine which DisplayFormats are possible
                        // use DisplayFormat to check actual display format
                        // Int
                        ((ToolStripMenuItem)ContextMenuForValueColumn.Items[0]).Enabled = IsFormatInList(varFormat, Global.eFormat.Int, Global.eFormat.UInt, Global.eFormat.Bin, Global.eFormat.Hex, Global.eFormat.ASCII, Global.eFormat.Float, Global.eFormat.String);
                        ((ToolStripMenuItem)ContextMenuForValueColumn.Items[0]).Checked = (varDisplayFormat == Global.eFormat.Int);
                        // UInt
                        ((ToolStripMenuItem)ContextMenuForValueColumn.Items[1]).Enabled = IsFormatInList(varFormat, Global.eFormat.Int, Global.eFormat.UInt, Global.eFormat.Bin, Global.eFormat.Hex, Global.eFormat.ASCII, Global.eFormat.Float, Global.eFormat.String);
                        ((ToolStripMenuItem)ContextMenuForValueColumn.Items[1]).Checked = (varDisplayFormat == Global.eFormat.UInt);
                        // Hex
                        ((ToolStripMenuItem)ContextMenuForValueColumn.Items[2]).Enabled = IsFormatInList(varFormat, Global.eFormat.Int, Global.eFormat.UInt, Global.eFormat.Bin, Global.eFormat.Hex, Global.eFormat.ASCII, Global.eFormat.Float, Global.eFormat.String);
                        ((ToolStripMenuItem)ContextMenuForValueColumn.Items[2]).Checked = (varDisplayFormat == Global.eFormat.Hex);
                        // Binary
                        ((ToolStripMenuItem)ContextMenuForValueColumn.Items[3]).Enabled = IsFormatInList(varFormat, Global.eFormat.Int, Global.eFormat.UInt, Global.eFormat.Bin, Global.eFormat.Hex, Global.eFormat.ASCII, Global.eFormat.Float);
                        ((ToolStripMenuItem)ContextMenuForValueColumn.Items[3]).Checked = (varDisplayFormat == Global.eFormat.Bin);
                        // Float
                        ((ToolStripMenuItem)ContextMenuForValueColumn.Items[4]).Enabled = IsFormatInList(varFormat, Global.eFormat.Int, Global.eFormat.UInt, Global.eFormat.Bin, Global.eFormat.Hex, Global.eFormat.ASCII, Global.eFormat.Float) && (curWidth >= 32) && (isBitfield == false);
                        ((ToolStripMenuItem)ContextMenuForValueColumn.Items[4]).Checked = (varDisplayFormat == Global.eFormat.Float);
                        // ASCII
                        ((ToolStripMenuItem)ContextMenuForValueColumn.Items[5]).Enabled = IsFormatInList(varFormat, Global.eFormat.Int, Global.eFormat.UInt, Global.eFormat.Bin, Global.eFormat.Hex, Global.eFormat.ASCII, Global.eFormat.Float) && (isBitfield == false);
                        ((ToolStripMenuItem)ContextMenuForValueColumn.Items[5]).Checked = (varDisplayFormat == Global.eFormat.ASCII);
                        // String
                        ((ToolStripMenuItem)ContextMenuForValueColumn.Items[6]).Enabled = IsFormatInList(varFormat, Global.eFormat.String) && (isBitfield == false);
                        ((ToolStripMenuItem)ContextMenuForValueColumn.Items[6]).Checked = (varDisplayFormat == Global.eFormat.String);

                        // show menu
                        ContextMenuForValueColumn.Show(this, e.X, e.Y);
                    }
                    else
                    // other columns...
                    {
                        // remember which gridrow was clicked
                        rightclickRowIndex = hitTestInfo.RowIndex;

                        // focus this row
                        grid.CurrentCell = null;
                        grid.CurrentCell = grid.Rows[rightclickRowIndex].Cells[0];

                        // show menu
                        ContextMenuForOtherColumns.Show(this, e.X, e.Y);
                    }
                }
            }
        }

        private void Menu_SetColorRed(object sender, EventArgs e)
        {
            DataRow dRow = (grid.Rows[rightclickRowIndex].DataBoundItem as DataRowView).Row;
            dRow[(int)dtColIndex.BCol] = ColorTranslator.FromHtml("#FFCCCC");
            grid.InvalidateRow(rightclickRowIndex);
        }

        private void Menu_SetColorGreen(object sender, EventArgs e)
        {
            DataRow dRow = (grid.Rows[rightclickRowIndex].DataBoundItem as DataRowView).Row;
            dRow[(int)dtColIndex.BCol] = ColorTranslator.FromHtml("#CCFFCC");
            grid.InvalidateRow(rightclickRowIndex);
        }

        private void Menu_SetColorBlue(object sender, EventArgs e)
        {
            DataRow dRow = (grid.Rows[rightclickRowIndex].DataBoundItem as DataRowView).Row;
            dRow[(int)dtColIndex.BCol] = ColorTranslator.FromHtml("#A0E1FF");
            grid.InvalidateRow(rightclickRowIndex);
        }

        private void Menu_SetColorYellow(object sender, EventArgs e)
        {
            DataRow dRow = (grid.Rows[rightclickRowIndex].DataBoundItem as DataRowView).Row;
            dRow[(int)dtColIndex.BCol] = ColorTranslator.FromHtml("#FFFFA0");
            grid.InvalidateRow(rightclickRowIndex);
        }

        private void Menu_SetColorDefault(object sender, EventArgs e)
        {
            DataRow dRow = (grid.Rows[rightclickRowIndex].DataBoundItem as DataRowView).Row;
            dRow[(int)dtColIndex.BCol] = DBNull.Value;
            grid.InvalidateRow(rightclickRowIndex);
        }

        private void Menu_CopyLine(object sender, EventArgs e)
        {
            CopyRowDataToClipboard(rightclickRowIndex);
        }

        private void CopyRowValueToClipboard(int rowIndex)
        {
            DataRow dRow = (grid.Rows[rowIndex].DataBoundItem as DataRowView).Row;
            string valueStr = dRow[(int)dtColIndex.ValueStr].ToString();

            if (valueStr.Length > 0)
            {
                System.Windows.Forms.Clipboard.SetText(valueStr);
            }
        }

        private void CopyRowDataToClipboard(int rowIndex)
        {
            DataRow dRow = (grid.Rows[rowIndex].DataBoundItem as DataRowView).Row;

            string rowStr = dRow[(int)dtColIndex.RegStr].ToString()
                    + "\t" + dRow[(int)dtColIndex.Descr].ToString();

            if (grid.Columns[(int)eGridColIndex.Properties].Visible)
            {
                rowStr += "\t" + dRow[(int)dtColIndex.InfoStr].ToString();
            }

            rowStr += "\t" + dRow[(int)dtColIndex.ValueStr].ToString();

            if (rowStr.Length > 0)
            {
                System.Windows.Forms.Clipboard.SetText(rowStr);
            }
        }

        private void ShowValueAsIntToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataRow dRow = (grid.Rows[rightclickRowIndex].DataBoundItem as DataRowView).Row;
            ChangeLineValueDisplayFormat(dRow, Global.eFormat.Int);
            grid.InvalidateRow(rightclickRowIndex);
        }

        private void ShowValueAsUIntToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataRow dRow = (grid.Rows[rightclickRowIndex].DataBoundItem as DataRowView).Row;
            ChangeLineValueDisplayFormat(dRow, Global.eFormat.UInt);
            grid.InvalidateRow(rightclickRowIndex);
        }

        private void ShowValueAsHexToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataRow dRow = (grid.Rows[rightclickRowIndex].DataBoundItem as DataRowView).Row;
            ChangeLineValueDisplayFormat(dRow, Global.eFormat.Hex);
            grid.InvalidateRow(rightclickRowIndex);
        }

        private void ShowValueAsBinaryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataRow dRow = (grid.Rows[rightclickRowIndex].DataBoundItem as DataRowView).Row;
            ChangeLineValueDisplayFormat(dRow, Global.eFormat.Bin);
            grid.InvalidateRow(rightclickRowIndex);
        }

        private void ShowValueAsFloatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataRow dRow = (grid.Rows[rightclickRowIndex].DataBoundItem as DataRowView).Row;
            ChangeLineValueDisplayFormat(dRow, Global.eFormat.Float);
            grid.InvalidateRow(rightclickRowIndex);
        }

        private void ShowValueAsASCIIToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataRow dRow = (grid.Rows[rightclickRowIndex].DataBoundItem as DataRowView).Row;
            ChangeLineValueDisplayFormat(dRow, Global.eFormat.ASCII);
            grid.InvalidateRow(rightclickRowIndex);
        }

        private void ShowValueAsStringToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataRow dRow = (grid.Rows[rightclickRowIndex].DataBoundItem as DataRowView).Row;
            ChangeLineValueDisplayFormat(dRow, Global.eFormat.String);
            grid.InvalidateRow(rightclickRowIndex);
        }

        private bool GetLineIsBitfield(DataRow dRow)
        {
            RegItem selReg;
            RegfieldItem selRegfield;
            bool isBitfield = false;

            FindListItemsByRow(dRow, out selReg, out selRegfield);
            if (selRegfield != null)
            {
                isBitfield = true;
            }
            
            return isBitfield;
        }

        private Global.eFormat GetLineValueFormat(DataRow dRow)
        {
            Global.eFormat result = Global.eFormat.Int;
            RegItem selReg;
            RegfieldItem selRegfield;

            FindListItemsByRow(dRow, out selReg, out selRegfield);
            if (selRegfield != null)
            {
                result = selRegfield.Format;
            }
            else if (selReg != null)
            {
                result = selReg.Format;
            }
            else
            {
                result = Global.eFormat.Int;
            }
            
            return result;
        }

        private Global.eFormat GetLineValueDisplayFormat(DataRow dRow)
        {
            Global.eFormat result = Global.eFormat.Int;
            RegItem selReg;
            RegfieldItem selRegfield;

            FindListItemsByRow(dRow, out selReg, out selRegfield);
            if (selRegfield != null)
            {
                result = selRegfield.DisplayFormat;
            }
            else if (selReg != null)
            {
                result = selReg.DisplayFormat;
            }
            else
            {
                result = Global.eFormat.Int;
            }

            return result;
        }

        private int GetLineValueWidth(DataRow dRow)
        {
            int result = 0;
            RegItem selReg;
            RegfieldItem selRegfield;

            FindListItemsByRow(dRow, out selReg, out selRegfield);
            if (selRegfield != null)
            {
                result = selRegfield.Width;
            }
            else if (selReg != null)
            {
                result = selReg.Size * 16;
            }
            else
            {
                result = 0;
            }

            return result;
        }

        private void ChangeLineValueDisplayFormat(DataRow dRow, Global.eFormat format)
        {
            RegItem selReg;
            RegfieldItem selRegfield;

            FindListItemsByRow(dRow, out selReg, out selRegfield);
            if (selRegfield != null)
            {
                selRegfield.DisplayFormat = format;
                UpdateRegisterFieldValue(selRegfield);
            }
            else if (selReg != null)
            {
                selReg.DisplayFormat = format;
                UpdateRegisterValueString(selReg);
            }
        }

        private void CopyValueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CopyValueStringToClipboard(rightclickRowIndex);
        }

        private void CopyValueStringToClipboard(int row)
        {
            DataRow dRow = (grid.Rows[rightclickRowIndex].DataBoundItem as DataRowView).Row;

            string s = dRow[(int)dtColIndex.ValueStr].ToString();

            if (s.Length > 0)
            {
                System.Windows.Forms.Clipboard.SetText(s);
            }
        }

        private void grid_CellToolTipTextNeeded(object sender, DataGridViewCellToolTipTextNeededEventArgs e)
        {
            if ((e.ColumnIndex == (int)eGridColIndex.Descr) && (e.RowIndex >= 0))
            {
                // get datatable row from datagridview row
                DataRowView dRow = grid.Rows[e.RowIndex].DataBoundItem as DataRowView;

                // got a longdescr tooltip text?
                object ho = dRow[(int)dtColIndex.Longdescr];
                if (ho != DBNull.Value)
                {
                    e.ToolTipText = (string)dRow[(int)dtColIndex.Longdescr];
                }
            }
        }

        private void expandAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExpandAll();
        }

        private void collapseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CollapseAll();
        }

        private void SetIconSizes()
        {
            // the icons in ModbusGridViewRes2 have images in multiple sizes
            // select one mathing the zoom factor

            // default size at zoomfactor=1 = 9x9 pixels
            int size = (int)(9.0 * currentZoomFactor + 0.5);    // round

            expandIcon = new Icon(ModbusGridViewRes2.TreeExpand, size, size);
            collapseIcon = new Icon(ModbusGridViewRes2.TreeCollapse, size, size);
            enumIndicatorIcon = new Icon(ModbusGridViewRes2.EnumIndicator, size, size);

            // size is used multiple times
            expandIconWidth = expandIcon.Width;
            expandIconHeight = expandIcon.Height;

            //Debug.Print("SetIconSizes(): currentZoomFactor=" + currentZoomFactor.ToString() + ", size=" + size.ToString() + ", iconWidth=" + expandIcon.Width.ToString());

        }

        private void grid_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            // call the handler
            if ((OnColumnWidthChanged != null) && (isLoading == false))
            {
                OnColumnWidthChanged(this);
            }
        }
    }
}
