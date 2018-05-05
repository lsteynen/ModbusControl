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

//TODO: probleem met formats, ValueToString en endians
//TODO: alle endians testen
//TODO: testen met ongeldig registernummer in importfile

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

        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // ProgressChanged callback is used to display progress strings in textboxLog
            textboxLog.AppendText( (e.UserState as String) + Environment.NewLine);
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
            // TODO: try to read multiple registers at once, will be much faster?

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

                // convert register to protocol offset
                ushort RegOffset = mParent.GetRegOffset(line.Reg);

                // read register(s)
                ushort RegCount = (ushort)(line.Size / 16);
                short[] aValues;
                ModbusProtocol.Result result = mModbus.MifReadHoldingRegisters(mSlaveAddress, RegOffset, RegCount, out aValues);

                if (result == ModbusProtocol.Result.SUCCESS)
                {
                    // success

                    // compare 16 or 32-bit?
                    // compare same size values to avoid signed/unsigned issues
                    Int32 TargetValue = 0;
                    bool bEqual = false;
                    if (line.Size == 32)
                    {
                        // make 32-bit value of 2 16-bit registers, respect configured word order
                        //TODO: endian
                        //TargetValue = (Int32)( ((UInt32)(aValues[0])) | (((UInt32)aValues[1]) << 16));
                        TargetValue = (Int32)(UInt16)aValues[0] | (Int32)((UInt16)aValues[1] << 16);
                        bEqual = (TargetValue == line.Value);
                    }
                    else
                    {
                        // compare 16-bit
                        TargetValue = aValues[0];
                        bEqual = ((UInt16)aValues[0] == (UInt16)line.Value);
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
            string TargetValueString = ValueToStringClass.ValueToString(TargetValue, Size, Format, 0, 0, false);
            // make result string
            string ResultString = bEqual ? "EQUAL" : "DIFFERENT: target value is " + TargetValueString;
            // make output string
            string s = Reg + ";" + Size + ";\"" + Description + "\";" + RWstring + ";" + Format + ";" + ValueString + "   >> " + ResultString;

            mWorker.ReportProgress(0, "Compare: " + s);

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

        private ModbusProtocol.Result WriteWord(int Size, UInt16 RegOffset, Int32 Value)
        {
            ModbusProtocol.Result result = ModbusProtocol.Result.SUCCESS;

            short[] awValues = new short[2];

            // split 32-bit value into 2 16-bit registers, respect configured word order
            //TODO: endian?
            if (Size == 32)
            {
                awValues[0] = (short)(Value);
                awValues[1] = (short)(Value>>16);
            }
            else
            {
                // 16-bit value in awValues[0]
                awValues[0] = (short)Value;
                awValues[1] = 0;
            }

            // use FC6 or FC16?
            //  #reg    FC6     FC16    auto
            //   1	    FC6		FC16#1	FC6
            //   >1     N*FC6	FC16#N	FC16#N

            bool useFC16 = (mParent.Project.Connection.WriteHoldingregFunctioncode == Global.eWriteHoldingregFunctionCode.FC16)
                          || ((mParent.Project.Connection.WriteHoldingregFunctioncode == Global.eWriteHoldingregFunctionCode.Auto) && (Size > 16));
            if (useFC16) 
            {
                // write multiple registers at once
                ushort regcount = (ushort)(Size/16);
                result = mModbus.MifWriteMultipleHoldingRegisters(mSlaveAddress, RegOffset, regcount, awValues);
            }
            else
            {
                // N times write single register
                ushort regcount = (ushort)(Size/16);
                for (int i = 0; (i < regcount) && (result == ModbusProtocol.Result.SUCCESS); ++i)
                {
                    result = mModbus.MifWriteSingleHoldingRegister(mSlaveAddress, (UInt16)(RegOffset + i), awValues[0]);
                }
            }

            return result;
        }

        private string[] SplitCsvLine(string instring, char separator)
        {
            // split a string (CSV line) by a separator
            // lines normally do not end with separator, lines ending with separator have a blank last field
            // the fields can be quoted, the quotes are not returned in the output
            // non-quoted fields are space-trimmed
            // quoted fields can contain heading and trailing spaces and the separator
            // spaces before or after the quoted part are ignored
            // garbage before or after the quoted part is returned in the output

            List<string> Result = new List<string>();
            string Part = string.Empty;
            bool PartQuoted = false;
            bool bInQuotes = false;

            for (int i = 0; i < instring.Length; i++)
            {
                if (bInQuotes)
                {
                    if (instring[i] == '"')
                    {
                        bInQuotes = false;
                        // end quote, ignore spaces after the quote
                        while (((i + 1) < instring.Length) && (instring[i + 1] == ' ')) { ++i; }
                    }
                    else
                        Part += instring[i];
                }
                else
                {
                    if (instring[i] == separator)
                    {
                        if (PartQuoted == false)
                            Part = Part.Trim();
                        Result.Add(Part);
                        Part = string.Empty;
                        PartQuoted = false;
                    }
                    else if (instring[i] == '"')
                    {
                        // start quote, remove spaces before the quote
                        Part = Part.TrimStart(' ');
                        PartQuoted = true;
                        bInQuotes = true;
                    }
                    else
                    {
                        Part += instring[i];
                    }
                }
            }

            // last field
            if (PartQuoted == false)
                Part = Part.Trim();
            Result.Add(Part);

            return Result.ToArray();
        }

        private string LimitStringLength(string s, int MaxLength)
        {
            if (MaxLength < 2)
            {
                MaxLength = 2;
            }

            if (s.Length >= MaxLength)
            {
                s = s.Substring(0, MaxLength - 2) + "..";
            }

            return s;
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
            bool bValid = true;
            List<string> fields;

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
                CStringToValue.eType Type;
                if (CStringToValue.Convert(ValueString, bIsFloat, imp.Size, 0, out imp.Value, out Type) == false)
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

#if false
        /*!
         * Get a list of successive variables/registers from ImportList
         * Each can be 16/32 bits
         * @retval 1 if we got registers to read (List will have entries)
         * @retval 0 if we got no more registers to read (List will be empty)
         * @retval -1 if register invalid
        */
        private int GetSuccessiveRegisters(int ListIndex, int Max16bitRegisters, bool bMustBeReadable, bool bMustBeWriteable, out int RegCount)
        {
            RegCount = 0;
            int LineCount = 0;
            UInt32 NextRegister = 0;
            bool GotNextRegister = false;

            /* there are lines left? */
            while (ListIndex < ImportList.Count)
            {
                // bMustBeReadable and bMustBeWriteable match?
                if (((bMustBeReadable == false) || ImportList[ListIndex].isReadable)
                   && ((bMustBeWriteable == false) || ImportList[ListIndex].isWriteable))
                {
                    // max register count reached?
                    if ((RegCount + ImportList[ListIndex].Size/16) > Max16bitRegisters)
                        break;

                    // not successive?
                    if (GotNextRegister && (ImportList[ListIndex].Reg != NextRegister))
                        break;

                    RegCount += ImportList[ListIndex].Size/16;

                    NextRegister = (UInt32)(ImportList[ListIndex].Reg + ImportList[ListIndex].Size/16);

                    LineCount++;
                }

                ListIndex++;
            }

            return LineCount;
        }
#endif
    }
}
