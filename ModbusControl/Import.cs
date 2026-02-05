using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Threading;

using LST.Helpers;
using Modbus_net;

namespace ModbusControlSpace
{
	public partial class Import : Form
    {
        #region Enums

        public enum EReturnCode { Success = 0, FileError, CommError };

        #endregion

        #region Data

        private MainForm mParent;
        private const int mSleeptime = 10;
        private bool mContinueOnError = false;
        private BackgroundWorker mWorker;

        private class ImportLine
        {
            public int lineNum;
            public string tabName;
            public List<string> fields;

            // constructor
            public ImportLine()
            {
                lineNum = 0;
                tabName = null;
                fields = null;
            }
        }

        private class Register
        {
            public UInt32 Reg;
            public UInt16 RegOffset;
            public int Size;
            public string Description;
            public bool isReadable;
            public bool isWriteable;
            public Global.eFormat Format;
            public string ValueString;
            public Int16[] aRegValue;

            // constructor
            public Register()
            {
                Reg = 0;
                RegOffset = 0;
                Size = 1;
                Description = null;
                isReadable = true;
                isWriteable = true;
                Format = Global.eFormat.Int;
                ValueString = null;
                aRegValue =  null;
            }
        }

        // list stores the import file
        private List<ImportLine> ImportList = new List<ImportLine>();

        #endregion

        #region Properties

        public ModbusInterface Modbus;

        public byte SlaveAddress;

        public int MaximumFrameSize = 256; 

        public bool ContinueOnError
        {
            get { return mContinueOnError; }

            set
            {
                mContinueOnError = value;
                chkContinueOnError.Checked = value;
            }
        }

        public int SuccessCount;

        public int NotWriteableCount;

        public int FileErrorCount;

        public int CommErrorCount;

        #endregion

        #region Implementation

        // constructor
        public Import(MainForm parent)
        {
            InitializeComponent();

            mParent = parent;
            mContinueOnError = chkContinueOnError.Checked;            
        }

        private void buttonSelectFile_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textboxFilename.Text = openFileDialog1.FileName;
            }
        }
		
		private void chkContinueOnError_CheckedChanged(object sender, EventArgs e)
        {
            mContinueOnError = chkContinueOnError.Checked;
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

            // check if port is opened
            if (Modbus.IsOpen == false)
            {
                throw new CommException("Connection is not open.");
            }

            try
            {
                StartImport();
			}
			catch( Exception ex)
			{
				textboxLog.AppendText("ERROR: " + ex.Message + Environment.NewLine);
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
		
		private void buttonClearLog_Click(object sender, EventArgs e)
        {
            textboxLog.Clear();
            textboxLog.Update();
        }
        
        private void OnFormClosing(object sender, FormClosingEventArgs e)
        {
            // Do not close, hide
            this.Hide();
            e.Cancel = true; // this cancels the close event.
        }


        /// <summary>
        /// Start import
		/// Load file, start import thread
        /// </summary>
        private void StartImport()
        {
            // get the file
			// can throw exception
			Cursor.Current = Cursors.WaitCursor;
            try
            {
                LoadImportFile(textboxFilename.Text);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
			
			// update buttons
            buttonStart.Enabled = false;
            buttonCancel.Enabled = true;
			// stop main form polling, for import speed
            mParent.MainPollingIsEnabled = false;

            // reset counters
            SuccessCount = 0;
            NotWriteableCount = 0;
            FileErrorCount = 0;
            CommErrorCount = 0;

            // Write registers from list to target
			// process all lines in the file, in that order
            StartImportThread();
        }

        /// <summary>
        /// Start the import thread, all tabs
        /// </summary>
		private void StartImportThread()
        {
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
            string prevTabName = String.Empty;

            foreach (ImportLine line in ImportList)
            {
                if (line.tabName != prevTabName)
                {
                    ReportVerbose("Import tab '" + line.tabName + "'");
                    prevTabName = line.tabName;
                }

                // Import this line
                EReturnCode tmpRet = ImportOneLine(line);
				// error?
                if (tmpRet != EReturnCode.Success)
                {
					/*
                    // return code
                    if (retco == EReturnCode.Success)
                    {
                        retco = tmpRet;
                    }
					*/

                    // stop on error?
                    if (ContinueOnError == false)
                    {
						break;
                    }
                } 

                // cancel?
                if (mWorker.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }			
               
                Thread.Sleep(mSleeptime);
            }

            //return retco;
        }
		
		private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // ProgressChanged callback is used to display progress strings in textboxLog
            textboxLog.AppendText((e.UserState as String) + Environment.NewLine);
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
            textboxLog.AppendText(ResultString + " " + SuccessCount + " registers written. " + NotWriteableCount + " registers non-writeable. " + (FileErrorCount + CommErrorCount) + " errors." + Environment.NewLine);

            //Change the status of the buttons on the UI accordingly
            buttonStart.Enabled = true;
            buttonCancel.Enabled = false;

            mParent.MainPollingIsEnabled = true;
        }        
		
		/// <summary>
        /// Load import file into ImportList
        /// Throws exception on error
        /// </summary>
        /// <param name="FileName"></param>
        private void LoadImportFile(string FileName)
        {
            CsvReader csvreader = null;

            ImportList.Clear();

			// ensure close
            try
            {
                // open file to read from

                // Use UTF-8 encoding. 
                // reads files with or without UTF-8 BOM
                
                csvreader = new CsvReader(FileName, new UTF8Encoding(false));
				csvreader.CommentHeaders = new string[] { "#", "//" };
				csvreader.Separator = ';';
				csvreader.SkipCommentLines = false;
				csvreader.SkipEmptyLines = true;

				// process all lines in the file
				List<string> fields;
				bool bContinue = true;
				string currentTabName = "";
				
				while (bContinue)
				{
					CsvReader.Result lineType = csvreader.ReadRow(out fields);

					if (lineType == CsvReader.Result.Comment)
					{
						// save the tab names, there is an option to import only certain tabs
						string line = fields[0];
						if (line.ToLower().StartsWith("tab:"))
						{
							currentTabName = line.Substring(4).Trim();
						}
					}
					else if (lineType == CsvReader.Result.Data)
					{
						// got a data line
						// store the fields
						ImportLine lin = new ImportLine();
						lin.lineNum = csvreader.LineNumber;
						lin.tabName = currentTabName;
						lin.fields = fields;
						ImportList.Add(lin);                // got a data line
					}
					else
					{
						// end 
						bContinue = false;
					}
                }
            }
            finally
            {
                if (csvreader != null)
                {
                    csvreader.Close();
                }
            }
        }
				
        /// <summary>
        /// Import 1 line, write 1 var, 1 var is 1 or more registers
        /// </summary>
        private EReturnCode ImportOneLine(ImportLine line)
        {
            Register reg = null;

#if DEBUG
            Debug.Print("Import line " + line.lineNum.ToString());
#endif

            // convert line fields to Register class
            try
            {
                reg = ConvertLineToRegister(line);
            }
			catch (Exception ex)
            {
                // file error
                ReportError(ex.Message); 
                ++FileErrorCount;
                return EReturnCode.FileError;
            }

            // skip lines that are not writeable
			if (reg.isWriteable == false)
			{
				++NotWriteableCount;
				return EReturnCode.Success;
			}

            // write
            ModbusProtocol.Result result = Modbus.MifWriteHoldingRegistersPaged(SlaveAddress, reg.RegOffset, (UInt16)reg.Size, reg.aRegValue);
            
			if (result == ModbusProtocol.Result.SUCCESS)
			{
				// success
				ReportImportLine(line.lineNum, reg.Reg, reg.Size, reg.Description, reg.Format, reg.isReadable, reg.isWriteable, reg.ValueString);
				++SuccessCount;
			}
			else
			{
				// comm error
				string errorStr = ModbusProtocol.GetResultText(result);
				ReportImportLineError(line.lineNum, reg.Reg, reg.Size, reg.Description, reg.Format, reg.isReadable, reg.isWriteable, reg.ValueString, errorStr);
			    ++CommErrorCount;
				return EReturnCode.CommError;
			}
			
			return EReturnCode.Success;
        }
		
		/// <summary>
        /// Convert 1 import line to Register class
        /// Throws exception on error
        /// </summary>
        /// <param name="line"></param>
        /// <returns>Register object</returns>
        private Register ConvertLineToRegister(ImportLine line)
        {
            // accept 2, 3, 4 ,5, or 6 fields

            // init default value strings
            // then process all fields
            string RegString = "";
            string SizeString = "1";
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
                string s = "Line " + line.lineNum.ToString() + ": Invalid number of fields: found " + line.fields.Count.ToString() + " fields, expecting 2, 3, 4, 5 or 6 fields.";
                throw new Exception(s);
            }

            Register reg = new Register();

            if (GetReg(RegString, out reg.Reg, out reg.RegOffset) == false)
            {
                string s = "Line " + line.lineNum.ToString() + ": Register number '" + RegString + "' is invalid.";
                throw new Exception(s);
            }

            if (GetSize(SizeString, out reg.Size) == false)
            {
                string s = "Line " + line.lineNum.ToString() + ": Register " + RegString + ": size '" + SizeString + "' is invalid.";
                throw new Exception(s);
            }

            GetDescr(DescriptionString, out reg.Description);

            if (GetReadWriteFlags(RwString, out reg.isReadable, out reg.isWriteable) == false)
            {
                string s = "Line " + line.lineNum.ToString() + ": Register " + RegString + ": r/w definition '" + RwString + "' is invalid.";
                throw new Exception(s);
            }

            if (GetFormat(FormatString, out reg.Format) == false)
            {
                string s = "Line " + line.lineNum.ToString() + ": Register " + RegString + ": type '" + FormatString + "' is invalid.";
                throw new Exception(s);
            }

            // check format and size combinations
            if (reg.Format == Global.eFormat.Int || reg.Format == Global.eFormat.UInt || reg.Format == Global.eFormat.Hex || reg.Format == Global.eFormat.Bin || reg.Format == Global.eFormat.ASCII)
            {
                // accept new format with number of registers
                if (reg.Size == 1 || reg.Size == 2)
                {
                    // OK
                }
                else if (reg.Size == 16 || reg.Size == 32)
                {
                    // OK, accept old format with width in bits
                    reg.Size = reg.Size / 16;
                }
                else
                {
                    string s = "Line " + line.lineNum.ToString() + ": Register " + RegString + " : integer register size '" + reg.Size.ToString() + "' is invalid, must be 1 or 2.";
                    throw new Exception(s);
                }
            }
            if (reg.Format == Global.eFormat.Float)
            {
                // accept new format with number of registers
                if (reg.Size == 2)
                {
                    // OK
                }
                else if (reg.Size == 32)
                {
                    // OK, accept old format with width in bits
                    reg.Size = reg.Size / 16;
                }
                else
                {
                    string s = "Line " + line.lineNum.ToString() + ": Register " + RegString + " : float register size '" + reg.Size.ToString() + "' is invalid, must be 2.";
                    throw new Exception(s);
                }
            }
            if (reg.Format == Global.eFormat.String)
            {
                // can only be new format with number of registers
                if ((reg.Size >= 1) && (reg.Size <= Global.StringMaxRegisters))
                {
                    // OK
                }
                else
                {
                    string s = "Line " + line.lineNum.ToString() + ": Register " + RegString + " : string register size '" + reg.Size.ToString() + "' is invalid, must be 1.." + Global.StringMaxRegisters.ToString() + ".";
                    throw new Exception(s);
                }
            }

            reg.ValueString = ValueString;
            try
            {
                int regCount = reg.Size;
                bool allowIntegers = (reg.Format == Global.eFormat.Int) || (reg.Format == Global.eFormat.UInt) || (reg.Format == Global.eFormat.Bin) || (reg.Format == Global.eFormat.Hex) || (reg.Format == Global.eFormat.ASCII) || ((reg.Format == Global.eFormat.Float) && (regCount == 2));
                bool allowFloat = (reg.Format == Global.eFormat.Float);
                bool allowString = (reg.Format == Global.eFormat.String);
                reg.aRegValue = CValueToString.ValueStringToInt16Registers(ValueString, regCount, 0, allowIntegers, allowFloat, allowString);
                // if it is a string: write only as many registers as needed
                if (reg.Format == Global.eFormat.String)
                {
                    if (reg.aRegValue.Length < reg.Size)
                    {
                        reg.Size = reg.aRegValue.Length;
                    }
                }
            }
            catch (Exception ex)
            {
                string s = "Line " + line.lineNum.ToString() + ": Register " + RegString + ": value '" + ValueString + "' is invalid. " + ex.Message;
                throw new Exception(s);
            }

            // valid
            return reg;
        }

        private bool GetReg(string text, out uint reg, out ushort regOffset)
        {
            reg = 0;
            regOffset = 0;

            if ( uint.TryParse(text, out reg))
            {
                try
                {
                    regOffset = RegisterToOffset.GetHoldingregisterOffset(reg);
                    return true;
                }
                catch
                {
                    // return false;
                }
            }

            return false;
        }

        private bool GetSize(string text, out int size)
        {
            // allow int 1..max
            return int.TryParse(text, out size) && (size >= 1);
        }

        private bool GetDescr(string text, out string descr)
        {
            descr = text;
            return true;
        }

        private bool GetReadWriteFlags(string text, out bool bRead, out bool bWrite)
        {
            // allow "r", "w", "rw", "wr"
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
            else if (text == "string")
            {
                format = Global.eFormat.String;
                return true;
            }

            format = Global.eFormat.Int;
            return false;
        }

        private void ReportImportLine(int Linenum, UInt32 Reg, int Size, string Description, Global.eFormat Format, bool isReadable, bool isWriteable, string ValueString)
        {
            // output 1 import line, all register info, and value
            // linenum is not shown

            // trim descr
            Description = TrimText(Description);
            // make RW string
            string RWstring = (isReadable ? "r" : "") + (isWriteable ? "w" : "");
            // make output string
            string s = Reg + ";" + Size + ";\"" + Description + "\";" + RWstring + ";" + Format + ";" + ValueString;

            ReportVerbose("Write: " + s);
        }

        private void ReportImportLineError(int Linenum, UInt32 Reg, int Size, string Description, Global.eFormat Format, bool isReadable, bool isWriteable, string ValueString, string ErrorText)
        {
            // output 1 import line, all register info, and error message
			// shows linenum

            // trim descr
            Description = TrimText(Description);
            // make RW string
            string RWstring = (isReadable ? "r" : "") + (isWriteable ? "w" : "");
            // make output string
            string s = Reg + ";" + Size + ";\"" + Description + "\";" + RWstring + ";" + Format + ";" + ValueString 
				+ "   >> ERROR: " + ErrorText;

            ReportError("Line " + Linenum.ToString() + ": Write: " + s);
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

        private void ReportVerbose(string s)
        {
            mWorker.ReportProgress(0, s);
        }

        private void ReportError(string s)
        {
            mWorker.ReportProgress(0, "Error: " + s);
        }
    }

    #endregion

}
