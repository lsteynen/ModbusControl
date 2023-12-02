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

using LST.Helpers;
using Modbus_net;

namespace ModbusControlSpace
{
    public partial class Import : Form
    {
        private MainForm mParent;
        private byte mSlaveAddress;
        private ModbusInterface mModbus;
        private BackgroundWorker mWorker;
        private int mNotWriteableCount;
        private int mWrittenCount;
        private int mErrorCount;
        private const int mSleeptime = 10;

        private class ImportLine
        {
            public int lineNum;
            public string tabName;
            public List<string> fields;
            //public string origLine;

            // constructor
            public ImportLine()
            {
                lineNum = 0;
                tabName = null;
                fields = null;
                //origLine = null;
            }
        };

        private class Register
        {
            public UInt32 Reg;
            public UInt16 RegOffset;
            public int Width;
            public string Description;
            public bool isReadable;
            public bool isWriteable;
            public Global.eFormat Format;
            public string ValueString;
            public Int32 Value;

            // constructor
            public Register()
            {
                Reg = 0;
                RegOffset = 0;
                Width = 16;
                Description = null;
                isReadable = true;
                isWriteable = true;
                Format = Global.eFormat.Int;
                ValueString = null;
                Value = 0;
            }
        };

        // list stores the import file
        List<ImportLine> ImportList = new List<ImportLine>();

        // constructor
        public Import(MainForm parent)
        {
            InitializeComponent();

            mParent = parent;
            mModbus = parent.mb;
            mSlaveAddress = parent.Project.Connection.SlaveID;
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

            DoImport();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            // cancel the backgroundworker
            if (mWorker.IsBusy)
            {
                mWorker.CancelAsync();
            }
        }

        private bool DoImport()
        {
            // Write registers from file to target

            // get the file
            Cursor.Current = Cursors.WaitCursor;
            bool bResult = LoadImportFile(textboxFilename.Text);
            Cursor.Current = Cursors.Default;
            if (bResult == false)
            {
                // LoadImportFile() has already shown error
                return false;
            }

            // check if port is opened
            if (mModbus.IsOpen == false)
            {
                textboxLog.AppendText("ERROR: Connection is not open." + Environment.NewLine);
                return false;
            }

            // process all lines in the file, in that order
            DoImportAll();

            return true;
        }

        private void DoImportAll()
        {
            // Write registers from list to target

            // reset counters
            mNotWriteableCount = 0;
            mWrittenCount = 0;
            mErrorCount = 0;

            buttonStart.Enabled = false;
            buttonCancel.Enabled = true;
            mParent.MainPollingIsEnabled = false;

            // start backgroundworker to do the import, so we can cancel
            mWorker = new BackgroundWorker();
            mWorker.DoWork += new DoWorkEventHandler(Worker_Import);
            mWorker.ProgressChanged += new ProgressChangedEventHandler(Worker_ProgressChanged);
            mWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(Worker_ImportWorkerCompleted);
            mWorker.WorkerReportsProgress = true;
            mWorker.WorkerSupportsCancellation = true;
            mWorker.RunWorkerAsync();
        }

        /// <summary>
        /// Background worker thread
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Worker_Import(object sender, DoWorkEventArgs e)
        {
            // TODO: try to write multiple registers at once, will be much faster?

            foreach (ImportLine line in ImportList)
            {
                // cancel?
                if (mWorker.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                try
                {
                    ImportOneLine(line);
                }
                catch (Exception ex)
                {
                    mWorker.ReportProgress(0, "EXCEPTION: " + ex.ToString());
                    e.Cancel = true;
                    return;
                }

                Thread.Sleep(mSleeptime);
            }

            // complete
        }

        /// <summary>
        /// Import 1 line, write 1 var
        /// Throws on eror
        /// </summary>
        private void ImportOneLine(ImportLine line)
        {
            Register reg = null;

            // convert line fields to Register
            // may throw
            reg = ConvertLineToRegister(line);

            // was valid?
            if (reg != null)
            {
                // skip lines that are not writeable
                if (reg.isWriteable == false)
                {
                    ++mNotWriteableCount;
                    return;
                }

                // convert format to type
                Global.eType Type = Global.ConvertFormatToType(reg.Format);

                // create the modbus values to write
                Int16[] Data = Endians.ConvertValueToShorts(reg.Value, Type, (reg.Width / 16));

                // write
                ModbusProtocol.Result result = mParent.DoWriteHoldingRegs(reg.Reg, (ushort)(reg.Width / 16), Data);

                if (result == ModbusProtocol.Result.SUCCESS)
                {
                    // success
                    WriteImportLine(reg.Reg, reg.Width, reg.Description, reg.Format, reg.isReadable, reg.isWriteable, reg.ValueString);
                    ++mWrittenCount;
                }
                else
                {
                    // error
                    WriteImportLineError(reg.Reg, reg.Width, reg.Description, reg.Format, reg.isReadable, reg.isWriteable, reg.ValueString, ModbusProtocol.GetResultText(result));
                    ++mErrorCount;
                }
            }
        }

        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // ProgressChanged callback is used to display progress strings in textboxLog
            textboxLog.AppendText((e.UserState as String) + Environment.NewLine);
        }

        private Register ConvertLineToRegister(ImportLine line)
        {
            // we accept 2, 3, 4 ,5, or 6 fields

            // init default value strings
            // then process all fields
            string RegString = "";
            string SizeString = "16";
            string DescriptionString = "";
            string RwString = "rw";
            string FormatString = "int";
            string ValueString = "0";

            if (line.fields.Count == 2)
            {
                // Assume: Reg;Value
                RegString = line.fields[0];
                ValueString = line.fields[1];
            }
            else if (line.fields.Count == 3)
            {
                // Assume: Reg;Size;Value
                RegString = line.fields[0];
                SizeString = line.fields[1];
                ValueString = line.fields[2];
            }
            else if (line.fields.Count == 4)
            {
                // Assume: Reg;Size;Description;Value
                RegString = line.fields[0];
                SizeString = line.fields[1];
                DescriptionString = line.fields[2];
                ValueString = line.fields[3];
            }
            else if (line.fields.Count == 5)
            {
                // Assume: Reg;Size;Description;Rw;Value
                RegString = line.fields[0];
                SizeString = line.fields[1];
                DescriptionString = line.fields[2];
                RwString = line.fields[3];
                ValueString = line.fields[4];
            }
            else if (line.fields.Count == 6)
            {
                // Assume: Reg;Size;Description;Rw;Type;Value
                RegString = line.fields[0];
                SizeString = line.fields[1];
                DescriptionString = line.fields[2];
                RwString = line.fields[3];
                FormatString = line.fields[4];
                ValueString = line.fields[5];
            }
            else
            {
                // incorrect number of fields
                string s = "ERROR: Line " + line.lineNum.ToString() + ": Invalid number of fields: found " + line.fields.Count.ToString() + " fields, expecting 2, 3, 4, 5 or 6 fields.";
                mWorker.ReportProgress(0, s);
                //textboxLog.AppendText(line.origLine + Environment.NewLine);
                return null;
            }

            // validate the new line and add to list
            Register reg = new Register();

            if (GetReg(RegString, out reg.Reg, out reg.RegOffset) == false)
            {
                string s = "ERROR: line " + line.lineNum.ToString() + ": Register number (" + RegString + ") is invalid.";
                mWorker.ReportProgress(0, s);
                //textboxLog.AppendText(line.origLine + Environment.NewLine);
                return null;
            }

            if (GetSize(SizeString, out reg.Width) == false)
            {
                string s = "ERROR: line " + line.lineNum.ToString() + ": Register " + RegString + ": size (" + SizeString + ") is invalid.";
                mWorker.ReportProgress(0, s);
                //textboxLog.AppendText(line.origLine + Environment.NewLine);
                return null;
            }

            GetDescr(DescriptionString, out reg.Description);

            if (GetReadWriteFlags(RwString, out reg.isReadable, out reg.isWriteable) == false)
            {
                string s = "ERROR: line " + line.lineNum.ToString() + ": Register " + RegString + ": r/w definition (" + RwString + ") is invalid.";
                mWorker.ReportProgress(0, s);
                //textboxLog.AppendText(line.origLine + Environment.NewLine);
                return null;
            }

            if (GetFormat(FormatString, out reg.Format) == false)
            {
                string s = "ERROR: line " + line.lineNum.ToString() + ": Register " + RegString + ": type (" + FormatString + ") is invalid.";
                mWorker.ReportProgress(0, s);
                //textboxLog.AppendText(line.origLine + Environment.NewLine);
                return null;
            }

            bool bIsFloat = (reg.Format == Global.eFormat.Float);
            if (bIsFloat && (reg.Width != 32))
            {
                string s = "ERROR: line " + line.lineNum.ToString() + ": Register " + RegString + ": float register size (" + reg.Width.ToString() + ") is not 32-bit.";
                mWorker.ReportProgress(0, s);
                //textboxLog.AppendText(line.origLine + Environment.NewLine);
                return null;
            }
            reg.ValueString = ValueString;
            Global.eType Type;
            if (CValueToString.StringToValue(ValueString, bIsFloat, reg.Width, 0, out reg.Value, out Type) == false)
            {
                string s = "ERROR: line " + line.lineNum.ToString() + ": Register " + RegString + ": value (" + ValueString + ") is invalid.";
                mWorker.ReportProgress(0, s);
                //textboxLog.AppendText(line.origLine + Environment.NewLine);
                return null;
            }

            // all valid
            return reg;
        }

        private void Worker_ImportWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // completed or canceled?
            string ResultString = "Done.";
            if (e.Cancelled)
            {
                ResultString = "Cancelled.";
            }

            // show counters
            textboxLog.AppendText(ResultString + " " + mWrittenCount + " registers written. " + mNotWriteableCount + " registers non-writeable. " + mErrorCount + " errors." + Environment.NewLine);

            //Change the status of the buttons on the UI accordingly
            buttonStart.Enabled = true;
            buttonCancel.Enabled = false;

            mParent.MainPollingIsEnabled = true;
        }

        private bool WriteImportLine(UInt32 Reg, int Size, string Description, Global.eFormat Format, bool isReadable, bool isWriteable, string ValueString)
        {
            // output 1 import line, all register info, and value

            // trim descr
            Description = TrimText(Description);
            // make RW string
            string RWstring = (isReadable ? "r" : "") + (isWriteable ? "w" : "");
            // make output string
            string s = Reg + ";" + Size + ";\"" + Description + "\";" + RWstring + ";" + Format + ";" + ValueString;

            mWorker.ReportProgress(0, "Write: " + s);

            return true;
        }

        private bool WriteImportLineError(UInt32 Reg, int Size, string Description, Global.eFormat Format, bool isReadable, bool isWriteable, string ValueString, string Error)
        {
            // output 1 import line, all register info, and error message

            // trim descr
            Description = TrimText(Description);
            // make RW string
            string RWstring = (isReadable ? "r" : "") + (isWriteable ? "w" : "");
            // make output string
            string s = Reg + ";" + Size + ";\"" + Description + "\";" + RWstring + ";" + Format + ";" + ValueString + "   >> ERROR: " + Error;

            mWorker.ReportProgress(0, "Write: " + s);

            return true;
        }

        private void buttonSelectFile_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textboxFilename.Text = openFileDialog1.FileName;
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

        /// <summary>
        /// Load import file into ImportList
        /// </summary>
        private bool LoadImportFile(string FileName)
        {
            CsvReader csvreader = null;

            ImportList.Clear();

            // open file to read from
            try
            {
                // open file

                // Assume Windows's default encoding, usually an ANSI code page (should match default export encoding).
                // Can also be System.Text.Encoding.UTF8 for UTF8 encoding.
                // Files with a unicode BOM will be recognized and decoded correctly.
                // See Export.cs for more info on Encodings.

                csvreader = new CsvReader(FileName, System.Text.Encoding.Default);
                csvreader.CommentHeaders = new string[] { "#", "//" };
                csvreader.Separator = ';';
                csvreader.SkipCommentLines = true;
                csvreader.SkipEmptyLines = true;

                textboxLog.AppendText("Opened file: " + FileName + Environment.NewLine);
            }
            catch (Exception err)
            {
                // open failed
                textboxLog.AppendText("ERROR: Can't open file: " + err.Message + Environment.NewLine);
                return false;
            }

            // process all lines in the file
            List<string> fields;
            bool bValid = true;

            while ((csvreader.ReadRow(out fields) == CsvReader.Result.Data) && bValid)
            {
                // got a data line
                // store the fields
                ImportLine imp = new ImportLine();
                imp.lineNum = csvreader.LineNumber;
                //imp.tabName = currentTabName;
                imp.fields = fields;
                //imp.origLine = csvreader.CurrentLine;
                ImportList.Add(imp);                // got a data line
            }

            csvreader.Close();

            return bValid;
        }


        private bool GetReg(string text, out uint reg, out ushort regOffset)
        {
            reg = 0;
            regOffset = 0;

            if (    uint.TryParse(text, out reg)
                && mParent.TryHoldingregisterGetOffset(reg, out regOffset))
            {
                return true;
            }

            return false;
        }

        private bool GetSize(string text, out int size)
        {
            // allow 16, 32
            return int.TryParse(text, out size) && ((size == 16) || (size == 32));
        }

        private bool GetDescr(string text, out string descr)
        {
            descr = text;
            return true;
        }

        private bool GetReadWriteFlags(string text, out bool bRead, out bool bWrite)
        {
            // allow "r", "w", "rw", "wr" and ""
            // allow upper and lower case

            bRead = false;
            bWrite = false;

            text = text.ToLower();

            if (text == "r")
            {
                bRead = true;
                return true;
            }
            else if (text == "w")
            {
                bWrite = true;
                return true;
            }
            else if ((text == "rw") || (text == "wr"))
            {
                bRead = true;
                bWrite = true;
                return true;
            }
            else if (text == "")
            {
                return true;
            }

            return false;
        }

        private bool GetFormat(string text, out Global.eFormat format)
        {
            text = text.ToLower();

            if (text == "int")
            {
                format = Global.eFormat.Int;
                return true;
            }
            else if (text == "uint")
            {
                format = Global.eFormat.UInt;
                return true;
            }
            else if (text == "hex")
            {
                format = Global.eFormat.Hex;
                return true;
            }
            else if (text == "bin")
            {
                format = Global.eFormat.Bin;
                return true;
            }
            else if (text == "float")
            {
                format = Global.eFormat.Float;
                return true;
            }
            else if (text == "ascii")
            {
                format = Global.eFormat.ASCII;
                return true;
            }

            format = Global.eFormat.Int;
            return false;
        }

        private string TrimText(string descr)
        {
            // replace possible " by ' 
            descr = descr.Replace('\"', '\'');
            // replace tabs 
            descr = descr.Replace("\t", " ");
            // replace CR/LF 
            descr = descr.Replace("\r\n", " ");
            descr = descr.Replace("\r", " ");
            descr = descr.Replace("\n", " ");
            // limit length
            const int maxlen = 60;
            if (descr.Length >= maxlen)
            {
                descr = descr.Substring(0, (maxlen - 2)) + "..";
            }

            return descr;
        }
    }
}
