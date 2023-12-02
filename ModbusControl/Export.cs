using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;

using Modbus_net;
using LST.Helpers;
using LST.ModbusGridView;

namespace ModbusControlSpace
{
    public partial class Export : Form
    {
        private MainForm mParent;
        private byte mSlaveAddress;
        private ModbusInterface mModbus;
        private BackgroundWorker mWorker;
        private int mSuccessCount;
        private int mErrorCount;
        private ExportMode mExportMode;
        private const int mSleeptime = 10;
        private CsvWriter mCsvWriter;
        private int mMaximumFrameSize = 256;

        private enum ExportMode { CurrentTab = 0, AllTabs };

        // properties

        public string TabSelect
        {
            // uses Radiobutton.Tag
            get { return RadioGroupHelpers.GetRadioGroupTag( panelTab); }
            set { RadioGroupHelpers.SetRadioGroupByTag(panelTab, value); }
        }

        public string ExportFormatSelect
        {
            // uses Radiobutton.Tag
            get { return RadioGroupHelpers.GetRadioGroupTag(groupboxExportFormat); }
            set { RadioGroupHelpers.SetRadioGroupByTag(groupboxExportFormat, value); }
        }

        public string AsciiFormatSelect
        {
            // uses Radiobutton.Tag
            get { return RadioGroupHelpers.GetRadioGroupTag(FormatAsciiPanel); }
            set { RadioGroupHelpers.SetRadioGroupByTag(FormatAsciiPanel, value); }
        }

        public int MaximumFrameSize
        {
            // uses Radiobutton.Tag
            get { return mMaximumFrameSize; }
            set { mMaximumFrameSize = value; }
        }

        private class ExportOrderElem
        {
            public int tabIndex;
            public uint order;
        }

        // constructor
        public Export(MainForm parent)
        {
            InitializeComponent();

            mParent = parent;
            mModbus = parent.mb;
            mSlaveAddress = parent.Project.Connection.SlaveID;

            UpdateFormatAsciiPanel();
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            // trim the filename
            textboxFilename.Text = textboxFilename.Text.Trim();

            // got filename?
            if (textboxFilename.Text.Length == 0)
            {
                textboxLog.AppendText("ERROR: No file name." + Environment.NewLine);
                return;
            }

            if (radiobuttonExportCurrentTab.Checked)
            {
                mExportMode = ExportMode.CurrentTab;
                DoExport();
            }
            else if (radiobuttonExportAllTabs.Checked)
            {
                mExportMode = ExportMode.AllTabs;
                DoExport();
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            // cancel the backgroundworker
            if (mWorker.IsBusy)
            {
                mWorker.CancelAsync();
            }
        }

        private void DoExport()
        {
            // read registers from target, write to file

            // reset counters
            mSuccessCount = 0;
            mErrorCount = 0;

            // check if port is opened
            if (mModbus.IsOpen == false)
            {
                textboxLog.AppendText("ERROR: Connection is not open." + Environment.NewLine);
                return;
            }

            // check if we have tab pages
            if( mParent.GetTabPageCount() < 1)
            {
                textboxLog.AppendText("ERROR: No tab pages." + Environment.NewLine);
                return;
            }

            // open file to write to
            try
            {
                // open output file

                // Use Windows's current ANSI codepage encoding, this allows easy import in Excel.
                // ANSI encoding is 1 byte per character, 0-127 is ASCII, 128-255 is per codepage.
                // Encodings: Encoding.Default for the system's default encoding, usually codepage 1252 in West-EU
                //            Encoding.ASCII for strict ASCII (0-127 only)
                //            System.Text.Encoding.GetEncoding(1252) for windows codepage 1252 for Latin 1 languages
                //            System.Text.Encoding.GetEncoding("ISO-8859-2") for ISO codepage with Polish
                //            Encoding.UTF8 for encoding the whole unicode character set in UTF8
                // See http://www.science.co.il/Language/Locale-codes.asp for a list of languages/countries and codepages.

                mCsvWriter = new CsvWriter(textboxFilename.Text, System.Text.Encoding.Default);
                mCsvWriter.CommentHeader = "#";
                mCsvWriter.Separator = ';';
                // always put quotes around the description field
                mCsvWriter.QuoteSomeFields = new bool[] { false, false, true }; // we choose to always quote the description field

                textboxLog.AppendText("Opened file: " + textboxFilename.Text + Environment.NewLine);
                //textboxLog.AppendText("(Encoding used: " + enc.WebName + ", Windows code page: " + enc.WindowsCodePage + ")" + Environment.NewLine);
            }
            catch (Exception err)
            {
                // open failed
                textboxLog.AppendText("ERROR: Can't open file: " + err.Message + Environment.NewLine);
                return;
            }

            buttonStart.Enabled = false;
            buttonCancel.Enabled = true;
            mParent.MainPollingIsEnabled = false;

            // start backgroundworker to do the export, so we can cancel
            mWorker = new BackgroundWorker();
            mWorker.DoWork += new DoWorkEventHandler(Worker_Export);
            mWorker.ProgressChanged += new ProgressChangedEventHandler(Worker_ProgressChanged);
            mWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(Worker_Completed);
            mWorker.WorkerReportsProgress = true;
            mWorker.WorkerSupportsCancellation = true;
            mWorker.RunWorkerAsync();
        }

        private void Worker_Export(object sender, DoWorkEventArgs e)
        {
            // background worker thread

            // export file header info
            mCsvWriter.WriteComment("Export file created by ModbusControl " + AppHelpers.VersionString + " - " + DateTime.Now.ToString());
            mCsvWriter.WriteComment("Project file: " + System.IO.Path.GetFileName(mParent.ProjectPath));
            mCsvWriter.WriteComment("Fields: Register;Size;Description;Read/write;Format;Value");

            // export registers of all tabs?
            if (mExportMode == ExportMode.AllTabs)
            {
                // export registers of all tabs

                // make list of tabs in export order
                List<ExportOrderElem> exportList = GetTabsExportOrderList();

                // export in requested order
                // this loop and DoExportTab can be canceled
                foreach (ExportOrderElem tabElem in exportList)
                {
                	TabPage tab = mParent.GetTabPageByIndex(tabElem.tabIndex);
                    DoExportTab(tab);

                    if (mWorker.CancellationPending)
                        break;
                }
            }
            else
            {
                // export registers of current tab page only
                // DoExportTab can be canceled
                DoExportTab(mParent.GetCurrentTabPage());
            }

            mCsvWriter.Close();

            // canceled? let Worker_Completed() know
            if (mWorker.CancellationPending)
            {
                e.Cancel = true;
            }
        }

        private List<ExportOrderElem> GetTabsExportOrderList()
        {
            List<ExportOrderElem> list = new List<ExportOrderElem>();

            for (int i = 0; mParent.GetTabPageByIndex(i) != null; ++i)
            {
                // get tab ModbusGridView
                ModbusGridView mrv = mParent.GetTabPageByIndex(i).Tag as ModbusGridView;
                ExportOrderElem el = new ExportOrderElem();
                el.tabIndex = i;
                el.order = mrv.ExportOrder;
                list.Add(el);
            }

            list.Sort(ExportOrderComparer);

            return list;
        }

        private int ExportOrderComparer(ExportOrderElem el1, ExportOrderElem el2)
        {
            // return 1 if el1 > el2
            // return 0 if el1 == el2
            // return -1 if el1 < el2

            if (el1.order > el2.order)
                return 1;
            if (el1.order < el2.order)
                return -1;

            if (el1.tabIndex > el2.tabIndex)
                return 1;
            if (el1.tabIndex < el2.tabIndex)
                return -1;

            return 0;
        }

        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // ProgressChanged callback is used to display progress strings in textboxLog
            textboxLog.AppendText( (e.UserState as String) + Environment.NewLine);
        }

        private void Worker_Completed(object sender, RunWorkerCompletedEventArgs e)
        {
            // completed or canceled?
            string ResultString = "Done.";
            if (e.Cancelled)
            {
                ResultString = "Cancelled.";
            }

            // show counters
            textboxLog.AppendText(ResultString + " " + mSuccessCount + " registers exported. " + mErrorCount + " errors." + Environment.NewLine);

            //Change the status of the buttons on the UI accordingly
            buttonStart.Enabled = true;
            buttonCancel.Enabled = false;

            mParent.MainPollingIsEnabled = true;
        }

        /* get all registers from a tab page, read them from target, write to file.
         *
         * return true on success (may have modbus exception errors)
         * return false on fatal error to abort the action
         */
        public void DoExportTab(TabPage tabPage)
        {
            // read registers from target, write to file
            // support cancel in the loops

            short[] readData;

            // get tab name 
            string tabName = tabPage.Name;

            // show tab name 
            mCsvWriter.WriteComment("Tab: " + tabName);
            mWorker.ReportProgress(0, "Tab: " + tabName);

            // get tab ModbusGridView
            ModbusGridView mrv = tabPage.Tag as ModbusGridView;

            // get groups of vars to read
            List<ModbusGridView.TReadGroup> groupList = mrv.GetReadGroupingList(mMaximumFrameSize);

            // read each group, output data
            foreach (ModbusGridView.TReadGroup rg in groupList)
            {
                // cancel?
                if (mWorker.CancellationPending)
                {
                    return;
                }

                // get register offset
                UInt16 regOffset = mParent.GetRegOffset(rg.readFirstReg);
				// read
                ModbusProtocol.Result result = mModbus.MifReadHoldingRegisters(mSlaveAddress, regOffset, (UInt16)rg.readRegCount, out readData);
                if (result == ModbusProtocol.Result.SUCCESS)
                {
                    // success

                    // process value of each register
                    int lineCount = 0;
                    int valueIndex = 0;
                    for (int lineIndex = rg.lineIndex; lineCount < rg.lineCount; ++lineIndex, ++lineCount)
                    {
                        ModbusGridView.TVarProperties varp = mrv.GetVarPropertiesByIndex(lineIndex);

                        // convert format to type
                        Global.eType Type = Global.ConvertFormatToType(varp.Format);

                        // get the value, 16 or 32-bit, watch size and endian
                        short[] aTmpRegs = new short[2];
                        aTmpRegs[0] = readData[valueIndex++];
                        aTmpRegs[1] = 0;
                        // is it 32-bit?
                        if (varp.Size == 2)
                        {
                            aTmpRegs[1] = readData[valueIndex++];
                        }
                        Int32 Value = Endians.ConvertShortsToValue(aTmpRegs, Type, varp.Size * 16);

                        WriteExportLine(varp.Reg, varp.Size, varp.Descr, varp.Format, varp.Readable, varp.Writeable, Value);
                        ++mSuccessCount;

                        Thread.Sleep(mSleeptime);
                    }
                }
                else
                {
                    // error

                    // if it is an address error? and we tried a group read? then read alls vars of this group with single reads to get per - var errors
                    //  some slaves return eILLEGALDATAVALUE if address + regcount is invalid, some return ILLEGALADDRESSEXCEPTION
                    if (((result == ModbusProtocol.Result.ILLEGALADDRESSEXCEPTION) || (result == ModbusProtocol.Result.ILLEGALDATAVALUEEXCEPTION)) && (rg.lineCount > 1))
                    {
                        // read per var (grid line) instead of group, detect error per var
						int lineCount = 0;
                        for (int lineIndex = rg.lineIndex; lineCount < rg.lineCount; ++lineIndex, ++lineCount)
                        {
                            // cancel?
                            if (mWorker.CancellationPending)
                            {
                                return;
                            }

                            ModbusGridView.TVarProperties varp = mrv.GetVarPropertiesByIndex(lineIndex);

                            // get register offset
                            regOffset = mParent.GetRegOffset(varp.Reg);
                            Debug.Print(DateTime.Now.TimeOfDay + ": MifReadHoldingRegisters(" + mSlaveAddress + "," + GetUserReg(regOffset) + "," + (UInt16)varp.Size + ")");
                            result = mModbus.MifReadHoldingRegisters(mSlaveAddress, regOffset, (UInt16)varp.Size, out readData);
                            Debug.Print(DateTime.Now.TimeOfDay + ": MifReadHoldingRegisters returned " + result);
                            if (result == ModbusProtocol.Result.SUCCESS)
                            {
                                // get the value, 16 or 32-bit

                                // convert format to type
                                Global.eType Type = Global.ConvertFormatToType(varp.Format);

                                // get the value, 16 or 32-bit, watch size and endian
                                short[] aTmpRegs = new short[2];
                                aTmpRegs[0] = readData[0];
                                aTmpRegs[1] = 0;
                                // is it 32-bit?
                                if (varp.Size == 2)
                                {
                                    aTmpRegs[1] = readData[1];
                                }
                                Int32 Value = Endians.ConvertShortsToValue(aTmpRegs, Type, varp.Size * 16);

                                WriteExportLine(varp.Reg, varp.Size, varp.Descr, varp.Format, varp.Readable, varp.Writeable, Value);
                                ++mSuccessCount;
                            }
                            else
                            {
                                // error
                                WriteExportLineError(varp.Reg, varp.Size, varp.Descr, varp.Format, varp.Readable, varp.Writeable, ModbusProtocol.GetResultText(result));
                                ++mErrorCount;
                            }
                        }
                    }
                    else
                    {
                        // other errors
                        // show error for each var
                        int lineCount = 0;
                        for (int lineIndex = rg.lineIndex; lineCount < rg.lineCount; ++lineIndex, ++lineCount)
                        {
                            ModbusGridView.TVarProperties varp = mrv.GetVarPropertiesByIndex(lineIndex);

                            WriteExportLineError(varp.Reg, varp.Size, varp.Descr, varp.Format, varp.Readable, varp.Writeable, ModbusProtocol.GetResultText(result));
                            ++mErrorCount;
                        }
                    }
                }
            }
        }

        private bool WriteExportLine(UInt32 Reg, int Size, string Description, Global.eFormat Format, bool isReadable, bool isWriteable, int Value)
        {
            // output 1 export line, all register info, and value

            // width in bits
            int Width = Size * 16;

            // make RW string
            string RWstring = (isReadable ? "r" : "") + (isWriteable ? "w" : "");

            // Export format as in grid or override?
            if (radiobuttonAsInGrid.Checked)
            {
                // As in grid
                // ASCII can be exported as Int or Hex to avoid CSV format problems
                if (Format == Global.eFormat.ASCII)
                {
                    if (radExportAsciiAsInt.Checked) Format = Global.eFormat.Int;
                    if (radExportAsciiAsHex.Checked) Format = Global.eFormat.Hex;
                }
            }
            else if (radiobuttonAsInt.Checked)
            {
                // All to Int
                Format = Global.eFormat.Int;
            }
            else if (radiobuttonAsUInt.Checked)
            {
                // All to UInt
                Format = Global.eFormat.UInt;
            }
            else if (radiobuttonAsHex.Checked)
            {
                // All to Hex
                Format = Global.eFormat.Hex;
            }

            // make value string
            // decimals are not used in export
            // minlen is not used in export
            string ValueString = CValueToString.ValueToString(Value, Width, Format);

            // output to file
            string[] fields = new string[] { Reg.ToString(), Width.ToString(), Description, RWstring, Format.ToString(), ValueString};
            mCsvWriter.WriteRow(fields);

            // and log
            string s = Reg + ";" + Width + ";\"" + Description + "\";" + RWstring + ";" + Format + ";" + ValueString;
            mWorker.ReportProgress(0, "Read: " + s);

            return true;
        }

        private bool WriteExportLineError(UInt32 Reg, int Size, string Description, Global.eFormat Format, bool isReadable, bool isWriteable, string Error)
        {
            // output 1 export line, all register info, and error message

            // width in bits
            int Width = Size * 16;

            // make RW string
            string RWstring = (isReadable ? "r" : "") + (isWriteable ? "w" : "");

            // output to file
            string[] fields = new string[] { Reg.ToString(), Width.ToString(), Description, RWstring, Format.ToString(), "?   >> ERROR: " + Error };
            mCsvWriter.WriteRow(fields);

            // and log
            string s = Reg + ";" + Width + ";\"" + Description + "\";" + RWstring + ";" + Format + ";?   >> ERROR: " + Error;
            mWorker.ReportProgress(0, "Read: " + s);

            return true;
        }

        private void buttonSelectFile_Click(object sender, EventArgs e)
        {
            if (SaveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textboxFilename.Text = SaveFileDialog1.FileName;
            }
        }

        private void buttonClearLog_Click(object sender, EventArgs e)
        {
            textboxLog.Clear();
            textboxLog.Update();
        }

        // Do not close, hide
        private void OnFormClosing(object sender, FormClosingEventArgs e)
        {
           this.Hide();
           e.Cancel = true; // this cancels the close event.
        }

        private int GetUserReg(ushort offset)
        {
            return mParent.Project.Options.HoldingregistersStart + offset;
        }

        private void radiobuttonFormat_CheckedChanged(object sender, EventArgs e)
        {
            UpdateFormatAsciiPanel();
        }

        private void UpdateFormatAsciiPanel()
        {
            // Ascii format selection is only valid with 'format as in grid'
            FormatAsciiPanel.Enabled = radiobuttonAsInGrid.Checked;
        }

    }
}
