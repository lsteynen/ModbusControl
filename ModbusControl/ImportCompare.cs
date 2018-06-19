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
using ModbusRegViewSpace;
using Modbus_net;

namespace ModbusControlSpace
{
    public partial class Importcompare : Form
    {
        private Form1 mParent;
        private byte mSlaveAddress;
        private ModbusInterface mModbus;
        private BackgroundWorker mWorker;
        private int mNotReadableCount;
        private int mEqualCount;
        private int mDifferentCount;
        private int mErrorCount;
        private const int mSleeptime = 10;

        private class ImportLine
        {
            public UInt32 Reg;
            public int Size;
            public string Description;
            public bool isReadable;
            public bool isWriteable;
            public Global.eFormat Format;
            public string ValueString;
            public Int32 Value;

            // constructor
            public ImportLine()
            {
                Reg = 0;
                Size = 16;
                Description = null;
                isReadable = true;
                isWriteable = true;
                Format = Global.eFormat.Int;
                ValueString = null;
                Value = 0;
            }
        };

        // here's where the import file is stored
        List<ImportLine> ImportList = new List<ImportLine>();

        // properties

        public bool ShowEquals
        {
            get { return checkboxShowEquals.Checked; }
            set { checkboxShowEquals.Checked = value; }
        }

        // constructor
        public Importcompare(Form1 parent)
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
                textboxLog.AppendText("ERROR: No file name!" + Environment.NewLine);
                return;
            }

            DoCompare();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            // cancel the backgroundworker
            if (mWorker.IsBusy)
            {
                mWorker.CancelAsync();
            }
        }

        private bool DoCompare()
        {
            // Compare registers from file with target

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
                textboxLog.AppendText("ERROR: Connection is not open!" + Environment.NewLine);
                return false;
            }

            // process all lines in the file
            DoCompareWithList();

            return true;
        }

        private void DoCompareWithList()
        {
            // Compare registers from list with target

            // reset counters
            mNotReadableCount = 0;
            mEqualCount = 0;
            mDifferentCount = 0;
            mErrorCount = 0;

            buttonStart.Enabled = false;
            buttonCancel.Enabled = true;
            mParent.MainPollingIsEnabled = false;

            // start backgroundworker to do the compare, so we can cancel
            mWorker = new BackgroundWorker();
            mWorker.DoWork += new DoWorkEventHandler(Worker_CompareWithList);
            mWorker.ProgressChanged += new ProgressChangedEventHandler(Worker_ProgressChanged);
            mWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(Worker_CompareWorkerCompleted);
            mWorker.WorkerReportsProgress = true;
            mWorker.WorkerSupportsCancellation = true;
            mWorker.RunWorkerAsync();
        }

        private void Worker_CompareWithList(object sender, DoWorkEventArgs e)
        {
            // background worker thread

            foreach (ImportLine line in ImportList)
            {
                // cancel?
                if (mWorker.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                // skip lines that are not readable
                if (line.isReadable == false)
                {
                    ++mNotReadableCount;
                    continue;
                }

                // convert format to type
                Endians.eType Type = ConvertFormatToType(line.Format);

                // convert value in file to modbus shorts[], watching endians
                Int16[] aFileValues = Endians.ConvertValueToShorts(line.Value, Type, line.Size);

                // convert register to protocol offset
                ushort RegOffset = mParent.GetRegOffset(line.Reg);

                // read register(s)
                ushort RegCount = (ushort)(line.Size / 16);
                short[] aReadValues;
                ModbusProtocol.Result result = mModbus.MifReadHoldingRegisters(mSlaveAddress, RegOffset, RegCount, out aReadValues);

                if (result == ModbusProtocol.Result.SUCCESS)
                {
                    // success

                    // compare binary values to avoid 16/32bit signed/unsigned issues
                    // compare 16 or 32-bit?
                    Int32 TargetValue = 0; // store target value for logging
                    bool bEqual = false;
                    if (line.Size == 32)
                    {
                        // compare 32-bit
                        bEqual = (aReadValues[0] == aFileValues[0]) && (aReadValues[1] == aFileValues[1]);
                        // get the 32-bit value for printing it later
                        TargetValue = Endians.ConvertShortsToValue(aReadValues, Type, 32);
                    }
                    else
                    {
                        // compare 16-bit
                        bEqual = (aReadValues[0] == aFileValues[0]);
                        TargetValue = aReadValues[0];
                    }

                    // count
                    if (bEqual)
                        ++mEqualCount;
                    else
                        ++mDifferentCount;

                    // show result
                    WriteCompareLine(line.Reg, line.Size, line.Description, line.Format, line.isReadable, line.isWriteable, line.ValueString, bEqual, TargetValue);
                }
                else
                {
                    // error

                    WriteCompareLineError(line.Reg, line.Size, line.Description, line.Format, line.isReadable, line.isWriteable, line.ValueString, ModbusProtocol.GetResultText(result));
                    ++mErrorCount;
                }

                Thread.Sleep(mSleeptime);
            }

            // complete
        }

        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // ProgressChanged callback is used to display progress strings in textboxLog
            textboxLog.AppendText( (e.UserState as String) + Environment.NewLine);
        }

        private void Worker_CompareWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // completed or canceled?
            string ResultString = "Done.";
            if (e.Cancelled)
            {
                ResultString = "Cancelled.";
            }

            // show counters
            textboxLog.AppendText(ResultString + " " + mEqualCount + " registers were equal. " + mDifferentCount + " registers were different. " + mNotReadableCount + " registers non-readable. " + mErrorCount + " errors. " + Environment.NewLine);

            //Change the status of the buttons on the UI accordingly
            buttonStart.Enabled = true;
            buttonCancel.Enabled = false;

            mParent.MainPollingIsEnabled = true;
        }

        private bool WriteCompareLine(UInt32 Reg, int Size, string Description, Global.eFormat Format, bool isReadable, bool isWriteable, string ValueString, bool bEqual, Int32 TargetValue)
        {
            // output 1 compare line, all register info, and value

            // replace possible " by '
            Description = Description.Replace('\"', '\'');
            // make RW string
            string RWstring = (isReadable ? "r" : "") + (isWriteable ? "w" : "");
            // make value string (various formats)
            string TargetValueString = CValueToString.ValueToString(TargetValue, Size, Format, 0, 0);
            // make result string
            string ResultString = bEqual ? "EQUAL" : "DIFFERENT: target value is " + TargetValueString;
            // make output string
            string s = Reg + ";" + Size + ";\"" + Description + "\";" + RWstring + ";" + Format + ";" + ValueString + "   >> " + ResultString;

            if (bEqual && (checkboxShowEquals.Checked == false))
            {
                // not show
            }
            else
            {
                mWorker.ReportProgress(0, "Compare: " + s);
            }

            return true;
        }

        private bool WriteCompareLineError(UInt32 Reg, int Size, string Description, Global.eFormat Format, bool isReadable, bool isWriteable, string ValueString, string Error)
        {
            // output 1 compare line, all register info, and error message

            // replace possible " by '
            Description = Description.Replace('\"', '\'');
            // make RW string
            string RWstring = (isReadable ? "r" : "") + (isWriteable ? "w" : "");
            // make output string
            string s = Reg + ";" + Size + ";\"" + Description + "\";" + RWstring + ";" + Format + ";" + ValueString + "   >> ERROR: " + Error;

            mWorker.ReportProgress(0, "Compare: " + s);

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

        private bool LoadImportFile(string FileName)
        {
            // load an import file into list

            CsvReader csvreader;

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
                csvreader.CommentHeaders = new string[] { "#", "//"};
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
                // we accept 2, 3, 4 ,5, or 6 fields

                // init default value strings
                // then process all fields
                string RegString = "";
                string SizeString = "16";
                string DescriptionString = "";
                string RwString = "rw";
                string FormatString = "int";
                string ValueString = "0";

                if (fields.Count == 2)
                {
                    // Assume: Reg;Value
                    RegString = fields[0];
                    ValueString = fields[1];
                }
                else if (fields.Count == 3)
                {
                    // Assume: Reg;Size;Value
                    RegString = fields[0];
                    SizeString = fields[1];
                    ValueString = fields[2];
                }
                else if (fields.Count == 4)
                {
                    // Assume: Reg;Size;Description;Value
                    RegString = fields[0];
                    SizeString = fields[1];
                    DescriptionString = fields[2];
                    ValueString = fields[3];
                }
                else if (fields.Count == 5)
                {
                    // Assume: Reg;Size;Description;Rw;Value
                    RegString = fields[0];
                    SizeString = fields[1];
                    DescriptionString = fields[2];
                    RwString = fields[3];
                    ValueString = fields[4];
                }
                else if (fields.Count == 6)
                {
                    // Assume: Reg;Size;Description;Rw;Type;Value
                    RegString = fields[0];
                    SizeString = fields[1];
                    DescriptionString = fields[2];
                    RwString = fields[3];
                    FormatString = fields[4];
                    ValueString = fields[5];
                }
                else
                {
                    // incorrect number of fields
                    textboxLog.AppendText("ERROR: line " + csvreader.LineNumber + ": Line format is not valid! (need 2, 3, 4, 5 or 6 fields)" + Environment.NewLine);
                    textboxLog.AppendText(csvreader.CurrentLine + Environment.NewLine);
                    bValid = false;
                    continue;
                }

                // validate the new line and add to list
                ImportLine imp = new ImportLine();

                ushort DummyOffset;

                if (   uint.TryParse(RegString, out imp.Reg)
                    && mParent.TryGetRegOffset(imp.Reg, out DummyOffset))
                {
                    // okay
                }
                else
                {
                    textboxLog.AppendText("ERROR: line " + csvreader.LineNumber + ": Register number field is invalid!" + Environment.NewLine);
                    textboxLog.AppendText(csvreader.CurrentLine + Environment.NewLine);
                    bValid = false;
                    continue;
                }
                if( GetSize(SizeString, out imp.Size) == false)
                {
                    textboxLog.AppendText("ERROR: line " + csvreader.LineNumber + ": Register size field is invalid!" + Environment.NewLine);
                    textboxLog.AppendText(csvreader.CurrentLine + Environment.NewLine);
                    bValid = false;
                    continue;
                }
                imp.Description = DescriptionString;
                if( GetReadWriteFlags( RwString, out imp.isReadable, out imp.isWriteable) == false)
                {
                    textboxLog.AppendText("ERROR: line " + csvreader.LineNumber + ": R/W flags field is invalid!" + Environment.NewLine);
                    textboxLog.AppendText(csvreader.CurrentLine + Environment.NewLine);
                    bValid = false;
                    continue;
                }
                if( GetFormat(FormatString, out imp.Format) == false)
                {
                    textboxLog.AppendText("ERROR: line " + csvreader.LineNumber + ": Type field is invalid!" + Environment.NewLine);
                    textboxLog.AppendText(csvreader.CurrentLine + Environment.NewLine);
                    bValid = false;
                    continue;
                }
                bool bIsFloat = (imp.Format == Global.eFormat.Float);
                if (bIsFloat && (imp.Size!=32))
                {
                    textboxLog.AppendText("ERROR: line " + csvreader.LineNumber + ": Float format size is not 32-bit!" + Environment.NewLine);
                    textboxLog.AppendText(csvreader.CurrentLine + Environment.NewLine);
                    bValid = false;
                    continue;
                }
                imp.ValueString = ValueString;
                Endians.eType Type;
                if (CValueToString.StringToValue(ValueString, bIsFloat, imp.Size, 0, out imp.Value, out Type) == false)
                {
                    textboxLog.AppendText("ERROR: line " + csvreader.LineNumber + ": Value field is invalid!" + Environment.NewLine);
                    textboxLog.AppendText(csvreader.CurrentLine + Environment.NewLine);
                    bValid = false;
                    continue;
                }

                // all valid
                ImportList.Add(imp);
            }

            csvreader.Close();

            return bValid;
        }

        private bool GetSize(string text, out int size)
        {
            // allow 16, 32
            return int.TryParse(text, out size) && ((size==16) || (size==32));
        }

        private bool GetReadWriteFlags( string text, out bool bRead, out bool bWrite)
        {
            // allow "r", "w", "rw", "wr" and ""
            // allow upper and lower case

            bRead = false;
            bWrite = false;

            text = text.ToLower();

            if( text == "r")
            {
                bRead = true;
                return true;
            }
            else if( text == "w")
            {
                bWrite = true;
                return true;
            }
            else if( (text == "rw") || (text == "wr"))
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

            if( text == "int")
            {
                format = Global.eFormat.Int;
                return true;
            }
            else if( text == "uint")
            {
                format = Global.eFormat.UInt;
                return true;
            }
            else if( text == "hex")
            {
                format = Global.eFormat.Hex;
                return true;
            }
            else if( text == "bin")
            {
                format = Global.eFormat.Bin;
                return true;
            }
            else if( text == "float")
            {
                format = Global.eFormat.Float;
                return true;
            }
            else if( text == "ascii")
            {
                format = Global.eFormat.ASCII;
                return true;
            }

            format = Global.eFormat.Int;
            return false;
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

    }
}
