using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Runtime.InteropServices;

using LST.Helpers;
using ModbusControlSpace;
using System.Resources;

//#define DEBUG_VISIBILITY

//TODO: proberen om de beep bij Edit-Escape te vermijden? Lukte nog niet, is blijkbaar by design :-(

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
        private const int FlagHasChildren = 1;
        private const int FlagIsExpanded = 2;
        private const int FlagIsEnum = 4;

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
            public int Size;            // var size in #registers, 1 or 2
            public ModbusControlSpace.Global.eFormat Format;      // display as dec, unsigned, hex, binary
            public int MinLen;          // minimum display length
            public int Decimals;        // nr of decimals to show
            public bool Readable;       // readable?
            public bool Writeable;      // writable?
            public string InfoStr;      // info string, like 'uint16, rw, dec=2'
            public Color? ForeColor;    // the foreground/font color, nullable
            public Color? BackColor;    // the background color, nullable
            public Int32 Value32;       // actual value in 32-bit
            public string ValueStr;     // value as a string
            public Global.CEnum EnumDict;   // enum convert dictionary
            public string LongDescr;    // <longdescr> tooltip text

            // constructor
            public RegItem()
            {
                Reg = 0;
                Size = 1;
                Descr = "";
                Format = ModbusControlSpace.Global.eFormat.Int;
                MinLen = 1;
                Decimals = 0;
                Readable = false;
                Writeable = false;
                ForeColor = null;
                BackColor = null;
                Value32 = 0;
                ValueStr = String.Empty;
                EnumDict = null;
            }
        };

        public class RegfieldItem : ListItem
        {
            public int Pos;             // lowest starting bit position
            public int Width;           // bitfield width in bits (1-32)
            public string RegStr;       // as string
            public string Descr;        // description
            public ModbusControlSpace.Global.eFormat Format;      // display as dec, unsigned, hex, binary
            public int MinLen;          // minimum display length
            public int Decimals;        // nr of decimals to show
            public string InfoStr;      // info string
            public Color? ForeColor;     // the foreground/font color
            public Color? BackColor;     // the background color
            public Int32 Value32;       // actual value in 32-bit
            public string ValueStr;     // value as a string
            public Global.CEnum EnumDict;   // enum convert dictionary
            public string LongDescr;    // <longdescr> tooltip text

            // constructor
            public RegfieldItem()
            {
                Pos = 0;
                Width = 1;
                Descr = "";
                Format = ModbusControlSpace.Global.eFormat.Int;
                MinLen = 1;
                Decimals = 0;
                ForeColor = null;
                BackColor = null;
                Value32 = 0;
                ValueStr = String.Empty;
                EnumDict = null;
            }
        };

        private struct TFocusPos
        {
            public int Row;
            public int Col;
        };

        public struct TReadGroup
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
            public int Size;            // var size in #registers, 1 or 2
            public ModbusControlSpace.Global.eFormat Format;      // display as dec, unsigned, hex, binary
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
        private volatile DataRow editingDatarow;    // volatile, to block update while editing
        private bool mAutosizeRows;
        private int rightclickRowIndex;

        // properties values
        private bool mConnectionIsOpen;
        private bool mStartEditOnAnyCharacter;

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
                if (value != colReg.FillWeight) colReg.FillWeight = value;
            }
        }

        public float DescriptionColumnWidthWeight
        {
            get { return colDescr.FillWeight; }
            set
            {
                if (value != colDescr.FillWeight) colDescr.FillWeight = value;
            }
        }

        public float PropertiesColumnWidthWeight
        {
            get { return colProps.FillWeight; }
            set
            {
                if (value != colProps.FillWeight) colProps.FillWeight = value;
            }
        }

        public float ValueColumnWidthWeight
        {
            get { return colValue.FillWeight; }
            set
            {
                if (value != colValue.FillWeight) colValue.FillWeight = value;
            }
        }

        public uint ExportOrder;

        #endregion

        #region events

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

        #endregion

        #region Implementation

        /// <summary>
        /// Constructor
        /// </summary>
        public ModbusGridView()
        {
            InitializeComponent();

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
        public ListItem AddReg(ListItem Parent, UInt32 Reg, string Descr, int Size, ModbusControlSpace.Global.eFormat Format, bool Readable, bool Writeable)
        {
            RegItem newReg = new RegItem();

            newReg.Reg = Reg;
            newReg.Descr = Descr;
            newReg.Size = Size;
            newReg.Format = Format;
            newReg.Decimals = 0;
            newReg.Readable = Readable;
            newReg.Writeable = Writeable;

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
        public ListItem RegAddField(ListItem Parent, int Pos, int Width, string Descr, ModbusControlSpace.Global.eFormat Format)
        {
            RegfieldItem newField = new RegfieldItem();

            newField.Pos = Pos;
            newField.Width = Width;
            newField.RegStr = FieldPosAndWidthToString(Pos, Width);
            newField.Descr = Descr;
            newField.Format = Format;
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
                // size is in #registers, show in bits
                string infoStr = ri.Format.ToString().ToLower();
                infoStr += (ri.Size * 16).ToString();

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
                string infoStr = ModbusControlSpace.Global.FormatText[(int)fi.Format].ToLower();
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
            data.Columns.Add("RowVisible", System.Type.GetType("System.Int32"));    // 3=visible, b1 voor filtering, b0 voor collapse/expand
            data.Columns.Add("RegStr", System.Type.GetType("System.String"));
            data.Columns.Add("Descr", System.Type.GetType("System.String"));
            data.Columns.Add("InfoStr", System.Type.GetType("System.String"));
            data.Columns.Add("FCol", typeof(System.Drawing.Color));                 // foreground color
            data.Columns.Add("BCol", typeof(System.Drawing.Color));                 // background color
            data.Columns.Add("ValueMode", System.Type.GetType("System.Int32"));     // mode of value column, 0=normal, 1=edit, 2=error
            data.Columns.Add("ValueStr", System.Type.GetType("System.String"));
            data.Columns.Add("Flags", System.Type.GetType("System.Int32"));
            data.Columns.Add("Longdescr", System.Type.GetType("System.String"));      // longdescription tooltip text

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
            Debug.Print("UpdateExpandedCollapsed()");

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
            Debug.Print("HandleRegCellClick() on Reg column, e.RowIndex=" + e.RowIndex.ToString());

            // get datatable row from datagridview row
            DataRowView drv = grid.Rows[e.RowIndex].DataBoundItem as DataRowView;
            DataRow dRow = drv.Row;                 // data row

            int Flags = (int)dRow[(int)dtColIndex.Flags];
            if ((Flags & FlagHasChildren) == FlagHasChildren)
            {
                Debug.Print(" isExpandable");
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
            //Debug.Print("e.Control=" + e.Control.ToString() + ", e.KeyCode=" + e.KeyCode.ToString());

            // use F2 key to start edit via keyboard
            if (e.KeyCode == Keys.F2)
            {
                TryBeginEdit(grid.CurrentCell.RowIndex);

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
            if (grid.CurrentCell.ColumnIndex == (int)eGridColIndex.Value)
            {
                EnumValueEditingControl cb = e.Control as EnumValueEditingControl;
                if (cb != null)
                {
                    // get IsEnum property 
                    EnumValueCell c = this.grid.CurrentCell as EnumValueCell;
                    if (c.IsEnum)
                    {
                        //editingReg and editingRegfield are already set when edit started
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
                }

                if (cb != null)
                {
                    // got a buffered key?
                    // then send it to the editing control
                    if (editingBufferedChar > 0)
                    {
                        PostMessage(cb.Handle, WM_CHAR, (int)editingBufferedChar, 0);
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
        /// Get a list of group read commands
        /// </summary>
        /// <param name="maxFrameSize">maximum size of the response frame (in bytes)</param>
        /// <returns></returns>
        public List<TReadGroup> GetReadGroupingList(int maxFrameSize)
        {
            List<TReadGroup> glist = new List<TReadGroup>();

            // no registers?
            if (RegList.Count == 0)
            {
                return null;
            }

            // bepaal max aantal 16-bit registers te lezen
            int max16BitRegs = (int)(maxFrameSize - 5) / 2;

            // voorlopig gaan we er van uit dat in RegList level 0 alleen registers zitten

            bool grouping = false;
            int lineIndex = 0;
            int groupFirstLineIndex = 0;
            int groupLineCount = 0;
            UInt32 groupFirstReg = 0;
            int groupRegCount = 0;
            UInt32 groupNextRegister = 0;
            foreach (ListItem Lit in RegList)
            {
                RegItem Reg = Lit as RegItem;
                if (grouping)
                {
                    // find next readable register in group
                    // is consecutive and readable?
                    if ((Reg.Reg == groupNextRegister)
                        && (Reg.Readable == true)
                        && ((groupRegCount + Reg.Size) <= max16BitRegs))           // allowed to read so many registers?
                    {
                        ++groupLineCount;
                        groupRegCount += Reg.Size;
                        groupNextRegister += (UInt32)Reg.Size;
                    }
                    else
                    {
                        TReadGroup g = new TReadGroup();
                        g.lineIndex = groupFirstLineIndex;
                        g.lineCount = groupLineCount;
                        g.readFirstReg = groupFirstReg;
                        g.readRegCount = groupRegCount;
                        glist.Add(g);
                        grouping = false;
                    }
                }
                if (grouping == false)
                {
                    // find next readable register to start new group
                    if ((Reg as RegItem).Readable)
                    {
                        groupFirstLineIndex = lineIndex;    // grid line index of first Reg
                        groupLineCount = 1;
                        groupFirstReg = Reg.Reg;          // first register of group
                        groupRegCount = Reg.Size;            // Size in number of 16-bit registers
                        groupNextRegister = Reg.Reg + (UInt32)Reg.Size;
                        grouping = true;
                    }
                }

                ++lineIndex;
            }

            if (grouping)
            {
                TReadGroup g = new TReadGroup();
                g.lineIndex = groupFirstLineIndex;
                g.lineCount = groupLineCount;
                g.readFirstReg = groupFirstReg;
                g.readRegCount = groupRegCount;
                glist.Add(g);
                grouping = false;
            }

            return glist;
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

                    // first 16-bit
                    Int16[] RawReg = new Int16[2];
                    RawReg[0] = readData[dataIndex++];
                    RawReg[1] = 0;
                    // is it 32-bit?
                    if (r.Size == 2)
                    {
                        RawReg[1] = readData[dataIndex++];
                    }

                    // convert format to type
                    Global.eType Type = Global.ConvertFormatToType(r.Format);

                    // make 32-bit value, watch endian
                    r.Value32 = Endians.ConvertShortsToValue(RawReg, Type, r.Size * 16);

                    // update register value, and its children
                    SetRegisterValue(r);
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
                    SetRegisterError(r, ErrorString);
                }
            }
        }

        /// <summary>
        /// Update a register value, and its children
        /// </summary>
        /// <param name="r"></param>
        private void SetRegisterValue(RegItem r)
        {
            // update the value string and grid
            UpdateRegisterValue(r);

            // has bitfields? then update the bitfield values too
            if (r.childrenList != null)
            {
                for (int ci = 0; ci < r.childrenList.Count; ++ci)
                {
                    RegfieldItem bf = r.childrenList[ci] as RegfieldItem;
                    Int32 NewFieldValue = GetBitfieldValue(r.Value32, bf.Pos, bf.Width, bf.Format == ModbusControlSpace.Global.eFormat.Int);
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
        private void UpdateRegisterValue(RegItem r)
        {
            // update the value string
            r.ValueStr = CValueToString.ValueToString(r.Value32, r.Size * 16, r.Format, r.Decimals, r.MinLen, r.EnumDict);
            // update the grid
            // but only if is is not beging edited
            DataRow dRow = r.dataRow;
            if (dRow != editingDatarow)
            {
                // update value in grid, only if it changes, to minimize flicker
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
            f.ValueStr = CValueToString.ValueToString(f.Value32, f.Width, f.Format, 0, f.MinLen, f.EnumDict);
            // update the grid
            // but only if is is not beging edited
            DataRow dRow = f.dataRow;
            if (dRow != editingDatarow)
            {
                // update value in grid, only if it changes, to minimize flicker
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
        /// Update a register value wit herror string, and its children
        /// </summary>
        /// <param name="r"></param>
        /// <param name="ErrorString"></param>
        private void SetRegisterError(RegItem r, string ErrorString)
        {
            // update the grid
            // but only if is is not beging edited
            DataRow dRow = r.dataRow;
            if (dRow != editingDatarow)
            {
                // update value in grid, only if it changes, to minimize flicker
                string dispStr = r.ValueStr + "  ?  <" + ErrorString + ">";
                if ((dispStr != GetDbStr(dRow[(int)dtColIndex.ValueStr]))
                    || ((int)dRow[(int)dtColIndex.ValueMode] != ValueModeError))
                {
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
                    if (dRow != editingDatarow)
                    {
                        // update value in grid, only if it changes, to minimize flicker
                        if ((dispStr != GetDbStr(dRow[(int)dtColIndex.ValueStr]))
                            || ((int)dRow[(int)dtColIndex.ValueMode] != ValueModeError))
                        {
                            dRow[(int)dtColIndex.ValueStr] = dispStr;
                            // set error color
                            dRow[(int)dtColIndex.ValueMode] = ValueModeError;
                        }
                    }
                }
            }
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

            //find item in RegList
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

            // make the value column the one to edit
            grid.CurrentCell = grid.Rows[gridRowIndex].Cells[(int)eGridColIndex.Value];

            // revert to normal color
            dRow[(int)dtColIndex.ValueMode] = ValueModeNormal;

            // start edit cell
            Debug.Print("before editingDatarow = dRow");
            editingDatarow = dRow;
            Debug.Print("after editingDatarow = dRow");
            return grid.BeginEdit(true);
        }

        private void grid_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            Debug.Print("grid_CellBeginEdit: e.rowindex=" + e.RowIndex.ToString() + ", grid.rowcount=" + grid.RowCount.ToString() + "/" + grid.Rows.Count.ToString());

            grid.EscapeDetected = false;
        }

        private void grid_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            Debug.Print("grid_CellEndEdit");

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
                // only if message is not there yet
                string dispStr = "<CONNECTION NOT OPEN>";
                if (!GetDbStr(dRow[(int)dtColIndex.ValueStr]).StartsWith(dispStr))
                {
                    dRow[(int)dtColIndex.ValueStr] = dispStr;
                }
                // Show error in red
                dRow[(int)dtColIndex.ValueMode] = ValueModeError;

                // report error on status bar
                OnReportErrorMessageEvent("Input error: connection is not open");
                // beep
                OnBeepEvent();
            }
            else if (editingReg.Writeable == false)
            {
                // show that the value was not accepted
                // only if message is not there yet
                string dispStr = "<REGISTER IS READ-ONLY>";
                if ( ! GetDbStr(dRow[(int)dtColIndex.ValueStr]).StartsWith(dispStr))
                {
                    dRow[(int)dtColIndex.ValueStr] = dispStr;
                }
                // Show error in red
                dRow[(int)dtColIndex.ValueMode] = ValueModeError;

                // report error on status bar
                OnReportErrorMessageEvent("Input error: register is read-only");
                // beep
                OnBeepEvent();
            }
            else
            {
                //editReg and editRegfield are already set in TryBeginEdit

                ModbusControlSpace.Global.eFormat edFormat = ModbusControlSpace.Global.eFormat.Int;
                int edWidth = 1;
                int edDecimals = 0;
                bool edIsEnum = false;

                // edited a field or register?
                if (editingRegfield != null)
                {
                    edFormat = editingRegfield.Format;
                    edWidth = editingRegfield.Width;
                    edDecimals = editingRegfield.Decimals;
                    edIsEnum = editingRegfield.EnumDict != null;
                }
                else
                {
                    edFormat = editingReg.Format;
                    edWidth = editingReg.Size * 16;
                    edDecimals = editingReg.Decimals;
                    edIsEnum = editingReg.EnumDict != null;
                }

                // convert string to value
                // is it a float?
                bool IsFloat = (edFormat == ModbusControlSpace.Global.eFormat.Float);

                // is it an enum?
                // the remove the (enum descr) part
                if (edIsEnum)
                {
                    newValueStr = CleanValuestrEnumText(newValueStr);
                }

                // convert & check
                Int32 NewValue;
                Global.eType Type;
                bool bIsValid = CValueToString.StringToValue(newValueStr, IsFloat, edWidth, edDecimals, out NewValue, out Type);

                // if this is a bitfield: insert bitfield value in register value
                if (bIsValid && (editingRegfield != null))
                {
                    int Position = editingRegfield.Pos;
                    NewValue = InsertBitfieldInValue(editingReg.Value32, NewValue, Position, edWidth);
                }

                // if value is valid: write, otherwise show error
                if (bIsValid)
                {
                    // update value and all shown fields of this register
                    editingReg.Value32 = NewValue;
                    SetRegisterValue(editingReg);

                    // the typed string is shown as the cell's new value, till the next read

                    // mark the parent register as edited, kleur
                    dRow[(int)dtColIndex.ValueMode] = ValueModeEdited;

                    // create the modbus values to write
                    Int16[] Data = Endians.ConvertValueToShorts(NewValue, Type, editingReg.Size);

                    // call write handler
                    if (OnRegisterWrite != null)
                    {
                        RegisterWriteEventArgs rwe = new RegisterWriteEventArgs();
                        rwe.Reg = editingReg.Reg;
                        rwe.Values = Data;
                        OnRegisterWrite(this, rwe);
                    }
                }
                else
                {
                    // show that the value was not accepted, put "<INVALID VALUE>" in front
                    // only if message is not there yet
                    string dispStr = "<INVALID VALUE> " + newValueStr;
                    if (dispStr != GetDbStr(dRow[(int)dtColIndex.ValueStr]))
                    {
                        dRow[(int)dtColIndex.ValueStr] = dispStr;
                    }
                    // Show error in red
                    dRow[(int)dtColIndex.ValueMode] = ValueModeError;

                    // report error on status bar
                    OnReportErrorMessageEvent("Input error: invalid value entered for register " + editingReg.Reg.ToString() + ": value: " + newValueStr);
                    // beep
                    OnBeepEvent();
                }
            }

            Debug.Print("before editingDatarow = null");
            editingDatarow = null;
            Debug.Print("after editingDatarow = null");
        }

        private Int32 InsertBitfieldInValue(Int32 Value, Int32 BitfieldValue, int Position, int BitWidth)
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

                        // show current format with checked flags
                        ModbusControlSpace.Global.eFormat curFormat = GetLineValueFormat(dRow);
                        int curWidth = GetLineValueWidth(dRow);
                        ((ToolStripMenuItem)ContextMenuForValueColumn.Items[0]).Checked = (curFormat == ModbusControlSpace.Global.eFormat.Int);
                        ((ToolStripMenuItem)ContextMenuForValueColumn.Items[1]).Checked = (curFormat == ModbusControlSpace.Global.eFormat.UInt);
                        ((ToolStripMenuItem)ContextMenuForValueColumn.Items[2]).Checked = (curFormat == ModbusControlSpace.Global.eFormat.Hex);
                        ((ToolStripMenuItem)ContextMenuForValueColumn.Items[3]).Checked = (curFormat == ModbusControlSpace.Global.eFormat.Bin);
                        // allow float for registers only, not bitfields
                        ((ToolStripMenuItem)ContextMenuForValueColumn.Items[4]).Enabled = (curWidth >= 32);
                        ((ToolStripMenuItem)ContextMenuForValueColumn.Items[4]).Checked = (curFormat == ModbusControlSpace.Global.eFormat.Float);
                        ((ToolStripMenuItem)ContextMenuForValueColumn.Items[5]).Checked = (curFormat == ModbusControlSpace.Global.eFormat.ASCII);

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
            CopyLineToClipboard(rightclickRowIndex);
        }

        private void CopyLineToClipboard(int row)
        {
            DataRow dRow = (grid.Rows[rightclickRowIndex].DataBoundItem as DataRowView).Row;

            string s = dRow[(int)dtColIndex.RegStr].ToString()
                    + "\t" + dRow[(int)dtColIndex.Descr].ToString();

            if (grid.Columns[(int)eGridColIndex.Properties].Visible)
            {
                s += "\t" + dRow[(int)dtColIndex.InfoStr].ToString();
            }

            s += "\t" + dRow[(int)dtColIndex.ValueStr].ToString();

            if (s.Length > 0)
            {
                System.Windows.Forms.Clipboard.SetText(s);
            }
        }

        private void ShowValueAsIntToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataRow dRow = (grid.Rows[rightclickRowIndex].DataBoundItem as DataRowView).Row;
            ChangeLineStringValueFormat(dRow, ModbusControlSpace.Global.eFormat.Int);
            grid.InvalidateRow(rightclickRowIndex);
        }

        private void ShowValueAsUIntToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataRow dRow = (grid.Rows[rightclickRowIndex].DataBoundItem as DataRowView).Row;
            ChangeLineStringValueFormat(dRow, ModbusControlSpace.Global.eFormat.UInt);
            grid.InvalidateRow(rightclickRowIndex);
        }

        private void ShowValueAsHexToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataRow dRow = (grid.Rows[rightclickRowIndex].DataBoundItem as DataRowView).Row;
            ChangeLineStringValueFormat(dRow, ModbusControlSpace.Global.eFormat.Hex);
            grid.InvalidateRow(rightclickRowIndex);
        }

        private void ShowValueAsBinaryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataRow dRow = (grid.Rows[rightclickRowIndex].DataBoundItem as DataRowView).Row;
            ChangeLineStringValueFormat(dRow, ModbusControlSpace.Global.eFormat.Bin);
            grid.InvalidateRow(rightclickRowIndex);
        }

        private void ShowValueAsFloatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataRow dRow = (grid.Rows[rightclickRowIndex].DataBoundItem as DataRowView).Row;
            ChangeLineStringValueFormat(dRow, ModbusControlSpace.Global.eFormat.Float);
            grid.InvalidateRow(rightclickRowIndex);
        }

        private void ShowValueAsASCIIToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataRow dRow = (grid.Rows[rightclickRowIndex].DataBoundItem as DataRowView).Row;
            ChangeLineStringValueFormat(dRow, ModbusControlSpace.Global.eFormat.ASCII);
            grid.InvalidateRow(rightclickRowIndex);
        }

        private ModbusControlSpace.Global.eFormat GetLineValueFormat(DataRow dRow)
        {
            RegItem selReg;
            RegfieldItem selRegfield;
            FindListItemsByRow(dRow, out selReg, out selRegfield);
            if (selRegfield != null)
            {
                return selRegfield.Format;
            }
            if (selReg != null)
            {
                return selReg.Format;
            }

            return ModbusControlSpace.Global.eFormat.Int;
        }
        private int GetLineValueWidth(DataRow dRow)
        {
            RegItem selReg;
            RegfieldItem selRegfield;
            FindListItemsByRow(dRow, out selReg, out selRegfield);
            if (selRegfield != null)
            {
                return selRegfield.Width;
            }
            if (selReg != null)
            {
                return selReg.Size * 16;
            }

            return 0;
        }
        private void ChangeLineStringValueFormat(DataRow dRow, ModbusControlSpace.Global.eFormat format)
        {
            RegItem selReg;
            RegfieldItem selRegfield;
            FindListItemsByRow(dRow, out selReg, out selRegfield);
            if (selRegfield != null)
            {
                selRegfield.Format = format;
                UpdateRegisterFieldValue(selRegfield);
            }
            if (selReg != null)
            {
                selReg.Format = format;
                UpdateRegisterValue(selReg);
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

        private string CleanValuestrEnumText(string str)
        {
            // expected formats: "123", " 123 (text)", " 123 (...", ...

            int i = str.IndexOf('(');
            if (i >= 0)
            {
                str = str.Substring(0, i);
            }

            return str;
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

            Debug.Print("SetIconSizes(): currentZoomFactor=" + currentZoomFactor.ToString() + ", size=" + size.ToString() + ", iconWidth=" + expandIcon.Width.ToString());

        }
    }
}
