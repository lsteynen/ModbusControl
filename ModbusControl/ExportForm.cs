using System;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Threading;
using System.Diagnostics;
using System.Windows.Forms;
using LST.Modbus;
using LST.Helpers;

// The first part of this file is different for the GUI en commandline versions.
// The second part, in region "Implementation - shared" is almost identical.

namespace ModbusControl
{
    public partial class ExportForm : Form
    {
        #region Enums

        private enum EExportMode { CurrentTab = 0, AllTabs };
        private enum EExportFormat { Default = 0, AsInt, AsUInt, AsHex };
        public enum EReturnCode { Success = 0, FileError, CommError };

        #endregion

        #region Data

        private MainForm mParent;            
        private BackgroundWorker mWorker;
        private CsvWriter mCsvWriter;
        private const int mSleeptime = 10;
        private bool mContinueOnError = false;
        private EExportMode mExportMode = EExportMode.CurrentTab;
        private EExportFormat mExportFormat = EExportFormat.Default;
        private EExportFormat mStringExportFormat = EExportFormat.Default;
        
        #endregion

        #region Properties

        public ModbusInterface Modbus;

        public RegisterToOffset RegisterToOffset1;

        public byte SlaveAddress;

       public string TabSelect
        {
            get { if (ExportCurrentTabRadiobtn.Checked) return "current";
                else if (ExportAllTabsRadiobtn.Checked) return "all";
                else return null;
            }

            set {   
                if (value == "current") ExportCurrentTabRadiobtn.Checked = true;
                else if (value == "all") ExportAllTabsRadiobtn.Checked = true;
            }
        }

        public string ExportFormat
        {
            get
            {
                if (FormatDefaultRadiobtn.Checked) return "default";
                else if (FormatAsIntRadiobtn.Checked) return "int";
                else if (FormatAsUIntRadiobtn.Checked) return "uint";
                else if (FormatAsHexRadiobtn.Checked) return "hex";
                else return null;
            }

            set {
                if (value == "default") FormatDefaultRadiobtn.Checked = true;
                else if (value == "int") FormatAsIntRadiobtn.Checked = true;
                else if (value == "uint") FormatAsUIntRadiobtn.Checked = true;
                else if (value == "hex") FormatAsHexRadiobtn.Checked = true;
            }
        }

        public string StringExportFormat
        {
            get
            {
                if (StringFormatDefaultRadiobtn.Checked) return "string";
                else if (StringFormatAsIntRadiobtn.Checked) return "int";
                else if (StringFormatAsUIntRadiobtn.Checked) return "uint";
                else if (StringFormatAsHexRadiobtn.Checked) return "hex";
                else return null;
            }

            set {
                if (value == "string") StringFormatDefaultRadiobtn.Checked = true;
                else if (value == "int") StringFormatAsIntRadiobtn.Checked = true;
                else if (value == "uint") StringFormatAsUIntRadiobtn.Checked = true;
                else if (value == "hex") StringFormatAsHexRadiobtn.Checked = true;
            }
        }

        public int MaximumFrameSize = 256;

        public bool ContinueOnError
        {
            get { return mContinueOnError; }

            set
            {
                mContinueOnError = value;
                ContinueOnErrorCheckbox.Checked = value;
            }
        }

        public int SuccessCount;

        public int FileErrorCount;

        public int CommErrorCount;

        #endregion

        #region Implementation

        private class ExportOrderItem
        {
            public int tabIndex;
            public uint order;
        }

        // constructor
        public ExportForm(MainForm parent)
        {
            InitializeComponent();

            mParent = parent;
            mContinueOnError = ContinueOnErrorCheckbox.Checked;
            UpdateStringFormatPanel();

#if TESTBUILD
            RunTestsBtn.Visible = true;
#endif
        }

        private void StartBtn_Click(object sender, EventArgs e)
        {
            // trim the filename
            FilenameTextbox.Text = FilenameTextbox.Text.Trim();

            // got filename?
            if (FilenameTextbox.Text.Length == 0)
            {
                LogTextbox.AppendText("ERROR: No file name." + Environment.NewLine);
                return;
            }

            // set export mode, current tab or all tabs
            if (ExportCurrentTabRadiobtn.Checked)
            {
                mExportMode = EExportMode.CurrentTab;
            }
            else if (ExportAllTabsRadiobtn.Checked)
            {
                mExportMode = EExportMode.AllTabs;
            }

            // set export format
            if (FormatDefaultRadiobtn.Checked)
            {
                mExportFormat = EExportFormat.Default;
            }
            else if (FormatAsIntRadiobtn.Checked)
            {
                mExportFormat = EExportFormat.AsInt;
            }
            else if (FormatAsUIntRadiobtn.Checked)
            {
                mExportFormat = EExportFormat.AsUInt;
            }
            else if (FormatAsHexRadiobtn.Checked)
            {
                mExportFormat = EExportFormat.AsHex;
            }

            // set string/ascii export format
            if (StringFormatDefaultRadiobtn.Checked)
            {
                mStringExportFormat = EExportFormat.Default;
            }
            else if (StringFormatAsIntRadiobtn.Checked)
            {
                mStringExportFormat = EExportFormat.AsInt;
            }
            else if (StringFormatAsUIntRadiobtn.Checked)
            {
                mStringExportFormat = EExportFormat.AsUInt;
            }
            else if (StringFormatAsHexRadiobtn.Checked)
            {
                mStringExportFormat = EExportFormat.AsHex;
            }

            // check if we have tab pages
            if (mParent.GetTabPageCount() == 0)
            {
                LogTextbox.AppendText("ERROR: No tab pages." + Environment.NewLine);
                return;
            }

            // check if port is opened
            if (Modbus.IsOpen == false)
            {
                throw new CommException("Connection is not open.");
            }

            try
            {
                DoExport();
            }
            catch( Exception ex)
            {
                LogTextbox.AppendText("ERROR: " + ex.Message + Environment.NewLine); 
            }

        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            // cancel the backgroundworker
            if (mWorker.IsBusy)
            {
                mWorker.CancelAsync();
            }
        }

        /// <summary>
        /// Do export
        /// Read registers from target and write to file 
        /// Can throw exception on error
        /// </summary>
        /// <returns>
        /// EReturnCode.Success    if there were no error 
        /// EReturnCode.CommError  if there was communication error 
        /// </returns>
        private EReturnCode DoExport()
        {
            // read registers from target, write to file

            EReturnCode retco = EReturnCode.Success;

            // reset counters
            SuccessCount = 0;
            FileErrorCount = 0;
            CommErrorCount = 0;

            // open file to write to
            // Use UTF-8 encoding. 
            // Encodings: Encoding.Default for the system's default encoding, usually codepage 1252 in West-EU
            //            Encoding.ASCII for strict ASCII (0-127 only)
            //            System.Text.Encoding.GetEncoding(1252) for windows codepage 1252 for Latin 1 languages
            //            System.Text.Encoding.GetEncoding("ISO-8859-2") for ISO codepage with Polish
            //            Encoding.UTF8 for encoding the whole unicode character set in UTF8
            //            Use "new UTF8Encoding(false)" for UTF-8 encoding without BOM
            // See http://www.science.co.il/Language/Locale-codes.asp for a list of languages/countries and codepages.

            mCsvWriter = new CsvWriter(FilenameTextbox.Text, new UTF8Encoding(false));
            mCsvWriter.CommentHeader = "#";
            mCsvWriter.Separator = ';';
            // always put quotes around the description field
            mCsvWriter.QuoteSelectedFields = new bool[] { false, false, true }; // we choose to always quote the description field

            LogTextbox.AppendText("Opened file: " + FilenameTextbox.Text + Environment.NewLine);
            
            StartBtn.Enabled = false;
            CancelBtn.Enabled = true;
            mParent.MainPollingIsEnabled = false;

            // start backgroundworker to do the export, to make the export cancelable
            mWorker = new BackgroundWorker();
            mWorker.DoWork += new DoWorkEventHandler(Worker_Export);
            mWorker.ProgressChanged += new ProgressChangedEventHandler(Worker_ProgressChanged);
            mWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(Worker_Completed);
            mWorker.WorkerReportsProgress = true;
            mWorker.WorkerSupportsCancellation = true;
            mWorker.RunWorkerAsync();

            return retco;
        }

        /// <summary>
        /// Background worker thread
        /// To make the export cancelable
        /// </summary>
        private void Worker_Export(object sender, DoWorkEventArgs e)
        {
            // export file header info
            mCsvWriter.WriteComment("Export file created by ModbusControl " + AppHelpers.VersionString + " - " + DateTime.Now.ToString());
            mCsvWriter.WriteComment("Project file: " + System.IO.Path.GetFileName(mParent.ProjectPath));
            mCsvWriter.WriteComment("Fields: Register;Size;Description;Read/write;Format;Value");

            // export all tabs?
            if (mExportMode == EExportMode.AllTabs)
            {
                // export registers of all tab pages

                // make list of tabs in export order
                List<ExportOrderItem> exportList = GetTabsExportOrderList();

                // export in requested order
                // this loop and DoExportTab can be canceled
                foreach (ExportOrderItem tabItem in exportList)
                {
                    TabPage tab = mParent.GetTabPageByIndex(tabItem.tabIndex);
                    EReturnCode retco2 = DoExportTab(tab);
                    if (retco2 != EReturnCode.Success)
                    {
                        //retco = retco2;
                        if (mContinueOnError == false)
                        {
                            break;
                        }
                    }

                    if (mWorker.CancellationPending)
                    {
                        break;
                    }
                }
            }
            else
            {
                // export registers of current tab page only
                // DoExportTab can be canceled
                EReturnCode retco2 = DoExportTab(mParent.GetCurrentTabPage());
                if (retco2 != EReturnCode.Success)
                {
                    //retco = retco2;
                }
            }

            mCsvWriter.Close();

            // canceled? let Worker_Completed() know
            if (mWorker.CancellationPending)
            {
                e.Cancel = true;
            }
        }

        private List<ExportOrderItem> GetTabsExportOrderList()
        {
            List<ExportOrderItem> list = new List<ExportOrderItem>();

            for (int i = 0; mParent.GetTabPageByIndex(i) != null; ++i)
            {
                // get tab ModbusGridView
                ModbusGridView mrv = mParent.GetTabPageByIndex(i).Tag as ModbusGridView;
                ExportOrderItem el = new ExportOrderItem();
                el.tabIndex = i;
                el.order = mrv.ExportOrder;
                list.Add(el);
            }

            list.Sort(ExportOrderComparer);

            return list;
        }

        private int ExportOrderComparer(ExportOrderItem el1, ExportOrderItem el2)
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
            LogTextbox.AppendText((e.UserState as String) + Environment.NewLine);
        }

        private void Worker_Completed(object sender, RunWorkerCompletedEventArgs e)
        {
            // completed or canceled?
            string resultStr = "Done.";
            if (e.Cancelled)
            {
                resultStr = "Cancelled.";
            }

            // show counters

            resultStr += " " + SuccessCount.ToString() + " registers exported."
                       + " " + FileErrorCount.ToString() + " file errors."
                       + " " + CommErrorCount.ToString() + " read errors.";
            LogTextbox.AppendText(resultStr + Environment.NewLine);

            // update the buttons
            StartBtn.Enabled = true;
            CancelBtn.Enabled = false;

            mParent.MainPollingIsEnabled = true;
        }

        private void ClearLogBtn_Click(object sender, EventArgs e)
        {
            LogTextbox.Clear();
            LogTextbox.Update();
        }

        private void SelectFileBtn_Click(object sender, EventArgs e)
        {
            if (SaveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                FilenameTextbox.Text = SaveFileDialog1.FileName;
            }
        }

        private void ContinueOnErrorCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            mContinueOnError = ContinueOnErrorCheckbox.Checked;
        }

        private void UpdateStringFormatPanel()
        {
            // Ascii format selection is only valid with 'format as in grid'
            StringFormatPanel.Enabled = FormatDefaultRadiobtn.Checked;
        }

        private void FormatRadiobtn_CheckedChanged(object sender, EventArgs e)
        {
            UpdateStringFormatPanel();
        }

        private string CleanText(string descr)
        {
            // does nothing here

            return descr;
        }

        private void ReportVerbose(string s)
        {
            mWorker.ReportProgress(0, s);
        }

        private void ReportError(string s)
        {
            mWorker.ReportProgress(0, s);
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

        #endregion

        #region Implementation - shared

        //doimport
        // The "Implementation - shared" region, doing the actual work, is almost identical for ModbusControl.Export and ModbusExport.Export.
        //

        /// <summary>
        /// Get all registers from a tab page, read them from target, write to file.
        /// Throw exception on fatal error like file write error.
        /// Continues exporting after error if mContinueOnError==true.
        /// </summary>
        /// <param name="tabPage"></param>
        /// <returns>
        /// EReturnCode.Success    if there were no error 
        /// EReturnCode.CommError  if there was communication error 
        /// </returns> 
        public EReturnCode DoExportTab(TabPage tabPage)
        {
            EReturnCode retcode = EReturnCode.Success;
            
            // read registers from target, write to file
            // support cancel in the loops

            bool bContinue = true;
            short[] readData;

            // show tab name 
            string tabName = tabPage.Name;
            mCsvWriter.WriteComment("Tab: " + tabName);
            ReportVerbose("Export tab: \"" + CleanText(tabName) + "\"");

            // determine max number of registers to read in 1 group
            int maxReadRegisters = Modbus.GetMaxRegisterToReadInFrame();

            // get groups of vars to read
            ModbusGridView mrv = tabPage.Tag as ModbusGridView;
            ModbusGridView.CReadGroup grp = mrv.GetNextReadGroup(null, maxReadRegisters);
            while ((grp != null) && bContinue)
            {
                // cancel?
                if (mWorker.CancellationPending)
                {
                    bContinue = false;
                    break;
                }

                // get register offset
                UInt16 regOffset = RegisterToOffset1.GetHoldingregisterOffset(grp.readFirstReg);
                // read
                // use paged read if needed 
                ModbusProtocol.Result result = Modbus.MifReadHoldingRegistersPaged(SlaveAddress, regOffset, (UInt16)grp.readRegCount, out readData);
                if (result == ModbusProtocol.Result.SUCCESS)
                {
                    // success

                    // process value of each var
                    int lineCount = 0;
                    int valueIndex = 0;
                    for (int lineIndex = grp.lineIndex;
                         lineCount < grp.lineCount;
                         ++lineIndex, ++lineCount)
                    {
                        // process per var
                        // 1, 2 or more registers per var

                        ModbusGridView.TVarProperties varp = mrv.GetVarPropertiesByIndex(lineIndex);

                        Int16[] aTmpRegs = new Int16[varp.Size];
                        Array.Copy(readData, valueIndex, aTmpRegs, 0, varp.Size);
                        valueIndex += varp.Size;
                        WriteExportVar(varp.Reg, varp.Size, varp.Descr, varp.Format, varp.Readable, varp.Writeable, aTmpRegs);
                        ++SuccessCount;
                        Thread.Sleep(mSleeptime);
                    }
                }
                else
                {
                    // error

                    // if it is an address error? and we tried a group read? then read alls vars of this group with single reads to get per - var errors
                    //  some slaves return eILLEGALDATAVALUE if address + regcount is invalid, some return ILLEGALADDRESSEXCEPTION
                    if (((result == ModbusProtocol.Result.ILLEGALADDRESSEXCEPTION) || (result == ModbusProtocol.Result.ILLEGALDATAVALUEEXCEPTION)) && (grp.lineCount > 1))
                    {
                        // read per var instead of group, detect error per var
                        int lineCount = 0;
                        for (int lineIndex = grp.lineIndex;
                             (lineCount < grp.lineCount) && bContinue;
                             ++lineIndex, ++lineCount)
                        {
                            // cancel?
                            if (mWorker.CancellationPending)
                            {
                                bContinue = false;
                                break;
                            }

                            ModbusGridView.TVarProperties varp = mrv.GetVarPropertiesByIndex(lineIndex);

                            // get register offset
                            regOffset = RegisterToOffset1.GetHoldingregisterOffset(varp.Reg);
                            // read
                            //Debug.Print(DateTime.Now.TimeOfDay + ": MifReadHoldingRegisters(" + mSlaveAddress + "," + GetUserReg(regOffset) + "," + (UInt16)varp.Size + ")");
                            result = Modbus.MifReadHoldingRegisters(SlaveAddress, regOffset, (UInt16)varp.Size, out readData);
                            //Debug.Print(DateTime.Now.TimeOfDay + ": MifReadHoldingRegisters returned " + result);
                            if (result == ModbusProtocol.Result.SUCCESS)
                            {
                                // get the value, 16 or 32-bit
                                // the register value is in readData as 1 or 2 16-bit registers

                                WriteExportVar(varp.Reg, varp.Size, varp.Descr, varp.Format, varp.Readable, varp.Writeable, readData);
                                ++SuccessCount;
                            }
                            else
                            {
                                // error
                                WriteExportError(varp.Reg, varp.Size, varp.Descr, varp.Format, varp.Readable, varp.Writeable, ModbusProtocol.GetResultText(result));
                                ++CommErrorCount;

                                retcode = EReturnCode.CommError;

                                if (mContinueOnError == false)
                                {
                                    bContinue = false;
                                }
                            }

                            Thread.Sleep(mSleeptime);
                        }
                    }
                    else
                    {
                        // other errors
                        // show error for each var
                        int lineCount = 0;
                        for (int lineIndex = grp.lineIndex;
                             lineCount < grp.lineCount;
                             ++lineIndex, ++lineCount)
                        {
                            // error

                            ModbusGridView.TVarProperties varp = mrv.GetVarPropertiesByIndex(lineIndex);

                            WriteExportError(varp.Reg, varp.Size, varp.Descr, varp.Format, varp.Readable, varp.Writeable, ModbusProtocol.GetResultText(result));
                            ++CommErrorCount;
                        }

                        retcode = EReturnCode.CommError;

                        if (mContinueOnError == false)
                        {
                            bContinue = false;
                        }
                    }
                }

                // next group
                grp = mrv.GetNextReadGroup(grp, maxReadRegisters);
            }

            return retcode;
        }

        private void WriteExportVar(UInt32 reg, int size, string description, Global.eFormat format, bool isReadable, bool isWriteable, Int16[] aRegValues)
        {
            // export 1 var
            // can be multiple lines to export string as registers

            // export string as single registers?
            if ((format == Global.eFormat.String) && (mExportFormat == EExportFormat.AsInt || mExportFormat == EExportFormat.AsUInt || mExportFormat == EExportFormat.AsHex || mStringExportFormat == EExportFormat.AsInt || mStringExportFormat == EExportFormat.AsUInt || mStringExportFormat == EExportFormat.AsHex))
            {
                // export string as single registers

                // in desired format
                if (mExportFormat == EExportFormat.AsInt || mStringExportFormat == EExportFormat.AsInt)
                {
                    format = Global.eFormat.Int;
                }
                else if (mExportFormat == EExportFormat.AsUInt || mStringExportFormat == EExportFormat.AsUInt)
                {
                    format = Global.eFormat.UInt;
                }
                else if (mExportFormat == EExportFormat.AsHex || mStringExportFormat == EExportFormat.AsHex)
                {
                    format = Global.eFormat.Hex;
                }

                // each register
                Int16[] aTmpReg = new Int16[1];
                for (int i = 0; i < size; ++i)
                {
                    aTmpReg[0] = aRegValues[i];
                    WriteExportVar2((UInt16)(reg + i), 1, description, format, isReadable, isWriteable, aTmpReg);
                }
            }
            else
            {
                // use format override?
                if (mExportFormat == EExportFormat.AsInt || (format == Global.eFormat.ASCII && mStringExportFormat == EExportFormat.AsInt))
                {
                    format = Global.eFormat.Int;
                }
                else if (mExportFormat == EExportFormat.AsUInt || (format == Global.eFormat.ASCII && mStringExportFormat == EExportFormat.AsUInt))
                {
                    format = Global.eFormat.UInt;
                }
                else if (mExportFormat == EExportFormat.AsHex || (format == Global.eFormat.ASCII && mStringExportFormat == EExportFormat.AsHex))
                {
                    format = Global.eFormat.Hex;
                }

                WriteExportVar2(reg, size, description, format, isReadable, isWriteable, aRegValues);
            }
        }

        private void WriteExportVar2(UInt32 reg, int size, string description, Global.eFormat format, bool isReadable, bool isWriteable, Int16[] aRegValues)
        {
            // export 1 simple var as 1 line

            // width in bits
            int width = size * 16;

            // make RW string
            string rwString = (isReadable ? "r" : "") + (isWriteable ? "w" : "");

            // use minlen for hex values
            int minlen = 0;
            if (format == Global.eFormat.Hex)
            {
                minlen = size * 4;
            }

            // make value string
            string valueString = CValueToString.Int16RegistersToValueString(aRegValues, format, format, width, 0, minlen);

            // output to file
            string[] fields = new string[] { reg.ToString(), size.ToString(), description, rwString, format.ToString(), valueString };
            mCsvWriter.WriteRow(fields);

            // and display
            string s = reg.ToString() + ";" + size.ToString() + ";\"" + CleanText(description) + "\";" + rwString + ";" + format.ToString() + ";" + valueString;
            ReportVerbose("Read: " + s);
        }

        private void WriteExportError(UInt32 reg, int size, string description, Global.eFormat format, bool isReadable, bool isWriteable, string error)
        {
            // output 1 export line with error

            // width in bits
            int width = size * 16;

            // make RW string
            string rwString = (isReadable ? "r" : "") + (isWriteable ? "w" : "");

            // output to file
            string[] fields = new string[] { reg.ToString(), size.ToString(), description, rwString, format.ToString(), "?   >> ERROR: " + error };
            mCsvWriter.WriteRow(fields);

            // and display
            string s = reg.ToString() + ";" + size.ToString() + ";\"" + CleanText(description) + "\";" + rwString + ";" + format.ToString() + ";?   >> ERROR: " + error;

            ReportError("Error in read: " + s);
        }

        #endregion

        #region Testcode

        /// <summary>-
        /// Integration test
        /// buttonTest appears in test-build
        /// </summary>
        private void TestBtn_Click(object sender, EventArgs e)
        {
            ExportAllTabsRadiobtn.Checked = true;

            FormatDefaultRadiobtn.Checked = true;
            StringFormatDefaultRadiobtn.Checked = true;
            FilenameTextbox.Text = @"export_as_in_grid.csv";
            Update();
            StartBtn_Click(null, null);
            while (StartBtn.Enabled == false)
            {
                Application.DoEvents();
                Thread.Sleep(100);
            }

            FormatAsIntRadiobtn.Checked = true;
            FilenameTextbox.Text = @"export_as_int.csv";
            Update();
            StartBtn_Click(null, null);
            while (StartBtn.Enabled == false)
            {
                Application.DoEvents();
                Thread.Sleep(100);
            }

            FormatAsUIntRadiobtn.Checked = true;
            FilenameTextbox.Text = @"export_as_uint.csv";
            Update();
            StartBtn_Click(null, null);
            while (StartBtn.Enabled == false)
            {
                Application.DoEvents();
                Thread.Sleep(100);
            }

            FormatAsHexRadiobtn.Checked = true;
            FilenameTextbox.Text = @"export_as_hex.csv";
            Update();
            StartBtn_Click(null, null);
            while (StartBtn.Enabled == false)
            {
                Application.DoEvents();
                Thread.Sleep(100);
            }

            FormatDefaultRadiobtn.Checked = true;
            StringFormatAsIntRadiobtn.Checked = true;
            FilenameTextbox.Text = @"export_string_as_int.csv";
            Update();
            StartBtn_Click(null, null);
            while (StartBtn.Enabled == false)
            {
                Application.DoEvents();
                Thread.Sleep(100);
            }

            FormatDefaultRadiobtn.Checked = true;
            StringFormatAsUIntRadiobtn.Checked = true;
            FilenameTextbox.Text = @"export_string_as_uint.csv";
            Update();
            StartBtn_Click(null, null);
            while (StartBtn.Enabled == false)
            {
                Application.DoEvents();
                Thread.Sleep(100);
            }

            FormatDefaultRadiobtn.Checked = true;
            StringFormatAsHexRadiobtn.Checked = true;
            FilenameTextbox.Text = @"export_string_as_hex.csv";
            Update();
            StartBtn_Click(null, null);
            while (StartBtn.Enabled == false)
            {
                Application.DoEvents();
                Thread.Sleep(100);
            }
        }

        #endregion

        
    }
}
