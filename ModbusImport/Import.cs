using System;
using System.Collections.Generic;
using System.Threading;

using LST.Helpers;
using Modbus_net;
using ModbusControlSpace;

namespace ModbusImport
{
    class FileException : ApplicationException
    {
        // thrown in error related to file
        public FileException() { }

        public FileException(string name) : base(String.Format(name))
        {
        }
    }

    class CommsException : ApplicationException
    {
        // thrown in error related to communications
        public CommsException() { }

        public CommsException(string name) : base(String.Format(name))
        {
        }
    }

    public class Import
    {
        public byte mSlaveAddress;
        public ModbusInterface mModbus;
        public int mNotWriteableCount;
        public int mWrittenCount;
        public int mErrorCount;
        private const int mSleeptime = 10;
        public string mFilename;
        public bool Verbose;
        public bool ContinueOnError = false;
        public List<string> TabsSelectList;      // List of selected tab names to export, in this order

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

        // callback to convert app register numbering to protocol offset
        public event GetHoldingregisterModbusOffset OnGetHoldingregisterModbusOffset;
        public delegate bool GetHoldingregisterModbusOffset(UInt32 Regnr, out UInt16 Offset);

        // callback for progress report, only if Verbose=true
        public event ReportProgress OnReportProgress;
        public delegate void ReportProgress(string s);

        /// <summary>
        /// Do import
        /// Write registers from file to target
        /// Throw on error
        /// </summary>
        public void DoImport()
        {
            // can throw
            LoadImportFile(mFilename);

            // check if port is opened
            if (mModbus.IsOpen == false)
            {
                throw new CommsException("Connection is not open.");
            }

            // all tabs or TabsSelectList?
            if ((TabsSelectList != null) && (TabsSelectList.Count > 0))
            {
                // tabs selected on command line

                // validate that selected tab names are valid
                // throws on error
                ValidateSelectedTabNames();

                DoImportSelectedTabs();
            }
            else
            {
                // all tabs

                // process all lines in the file, in that order
                // can throw
                DoImportAll();
            }
        }


        /// <summary>
        /// Load import file into ImportList
        /// Throw on error
        /// </summary>
        /// <param name="FileName"></param>
        /// <returns></returns>
        private void LoadImportFile(string FileName)
        {
            CsvReader csvreader = null;

            ImportList.Clear();

            //ReportVerbose("Open file '" + FileName + "'");

            // ensure close
            try
            {
                // open
                try
                {
                    // Assume Windows's default encoding, usually an ANSI code page (should match default export encoding).
                    // Can also be System.Text.Encoding.UTF8 for UTF8 encoding.
                    // Files with a unicode BOM will be recognized and decoded correctly.
                    // See Modbuscontrol Export.cs for more info on Encodings.

                    csvreader = new CsvReader(FileName, System.Text.Encoding.Default);
                    csvreader.CommentHeaders = new string[] { "#", "//" };
                    csvreader.Separator = ';';
                    csvreader.SkipCommentLines = false;
                    csvreader.SkipEmptyLines = true;

                    //textboxLog.AppendText("Opened file: " + FileName + Environment.NewLine);
                }
                catch (Exception ex)
                {
                    // open failed
                    //textboxLog.AppendText("Error: Can't open file: " + ex.Message + Environment.NewLine); 
                    string s = "Cannot open file: " + ex.Message;
                    throw new FileException(s);
                }

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
                        ImportLine imp = new ImportLine();
                        imp.lineNum = csvreader.LineNumber;
                        imp.tabName = currentTabName;
                        imp.fields = fields;
                        ImportList.Add(imp);
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
        /// Validates that all tabnames in TabsSelectList are valid,
        /// meaning they exist in the CSV list.
        /// Throw on error.
        /// </summary>
        private void ValidateSelectedTabNames()
        {
            foreach (string selName in TabsSelectList)
            {
                bool found = false;
                foreach (ImportLine line in ImportList)
                {
                    if (line.tabName.ToLower() == selName.ToLower())
                    {
                        // valid
                        found = true;
                        break;
                    }
                }
                // error if tab name not found
                if (found == false)
                {
                    throw new ArgumentException("Invalid tab name: '" + selName + "'");
                }
            }
        }

        /// <summary>
        /// Do the actual import, all tabs
        /// Throws on error
        /// </summary>
        private void DoImportAll()
        {
            // Write registers from list to target

            // reset counters
            mNotWriteableCount = 0;
            mWrittenCount = 0;
            mErrorCount = 0;

            foreach (ImportLine line in ImportList)
            {
                ImportOneLine(line);

                Thread.Sleep(mSleeptime);
            }
        }

        /// <summary>
        /// Do the actual import, only selected tabs, in order of selection
        /// Throws on error
        /// </summary>
        private void DoImportSelectedTabs()
        {
            // Write registers from list to target

            // reset counters
            mNotWriteableCount = 0;
            mWrittenCount = 0;
            mErrorCount = 0;

            // import each selected tab
            // in order, may appear multiple times
            foreach (string selName in TabsSelectList)
            {
                ReportVerbose("Import tab: " + selName);
                foreach (ImportLine line in ImportList)
                {
                    if (line.tabName.ToLower() == selName.ToLower())
                    {
                        ImportOneLine(line);

                        Thread.Sleep(mSleeptime);
                    }
                }
            }
        }

        /// <summary>
        /// Import 1 line, write 1 var
        /// Throws on error
        /// </summary>
        private void ImportOneLine(ImportLine line)
        {
            Register reg = null;

            // convert line fields to Register
            try
            {
                reg = ConvertLineToRegister(line);
            }
            catch (Exception e)
            {
                // error
                ++mErrorCount;
                // continue?
                if (ContinueOnError)
                {
                    // report error and continue
                    ReportError("Error: " + e.Message);
                }
                else
                {
                    //rethrow
                    throw;
                }
            }

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
                // width from bits to registers
                ModbusProtocol.Result result = mModbus.MifWriteHoldingRegistersAuto(mSlaveAddress, reg.RegOffset, (ushort)(reg.Width / 16), Data);

                if (result == ModbusProtocol.Result.SUCCESS)
                {
                    // success
                    ++mWrittenCount;

                    string s = BuildImportLineString(reg);
                    ReportVerbose("Write: " + s);
                }
                else
                {
                    // error
                    ++mErrorCount;

                    string s = "Line " + line.lineNum.ToString() + ": Write: " + BuildImportLineString(reg) + " >> " + ModbusProtocol.GetResultText(result);
                    if (ContinueOnError)
                        ReportError("Error: " + s);
                    else
                        throw new FileException(s);
                }
            }
        }

        private Register ConvertLineToRegister(ImportLine line)
        {
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
                string s = "Line " + line.lineNum.ToString() + ": Invalid number of fields: found " + line.fields.Count.ToString() + " fields, expecting 2, 3, 4, 5 or 6 fields.";
                throw new FileException(s);
            }

            Register reg = new Register();

            if (GetReg(RegString, out reg.Reg, out reg.RegOffset) == false)
            {
                string s = "Line " + line.lineNum.ToString() + ": Register number (" + RegString + ") is invalid.";
                throw new FileException(s);
            }

            if (GetSize(SizeString, out reg.Width) == false)
            {
                string s = "Line " + line.lineNum.ToString() + ": Register " + RegString + ": size (" + SizeString + ") is invalid.";
                throw new FileException(s);
            }

            GetDescr(DescriptionString, out reg.Description);

            if (GetReadWriteFlags(RwString, out reg.isReadable, out reg.isWriteable) == false)
            {
                string s = "Line " + line.lineNum.ToString() + ": Register " + RegString + ": r/w definition (" + RwString + ") is invalid.";
                throw new FileException(s);
            }

            if (GetFormat(FormatString, out reg.Format) == false)
            {
                string s = "Line " + line.lineNum.ToString() + ": Register " + RegString + ": type (" + FormatString + ") is invalid.";
                throw new FileException(s);
            }

            bool bIsFloat = (reg.Format == Global.eFormat.Float);
            if (bIsFloat && (reg.Width != 32))
            {
                string s = "Line " + line.lineNum.ToString() + ": Register " + RegString + ": float register size (" + reg.Width.ToString() + ") is not 32-bit.";
                throw new FileException(s);
            }
            reg.ValueString = ValueString;
            Global.eType Type;
            if (CValueToString.StringToValue(ValueString, bIsFloat, reg.Width, 0, out reg.Value, out Type) == false)
            {
                string s = "Line " + line.lineNum.ToString() + ": Register " + RegString + ": value (" + ValueString + ") is invalid.";
                throw new FileException(s);
            }

            return reg;
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

        private string BuildImportLineString(Register reg)
        {
            // trim descr
            string descr = TrimText(reg.Description);
            // make RW string
            string RWstring = (reg.isReadable ? "r" : "") + (reg.isWriteable ? "w" : "");
            // make output string
            string s = reg.Reg + ";" + reg.Width + ";\"" + descr + "\";" + RWstring + ";" + reg.Format + ";" + reg.ValueString;

            return s;
        }

        private bool GetReg(string text, out uint reg, out ushort regOffset)
        {
            reg = 0;
            regOffset = 0;

            if (uint.TryParse(text, out reg))
            {
                if (OnGetHoldingregisterModbusOffset(reg, out regOffset))
                {
                    return true;
                }
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

            format = Global.eFormat.Int;
            return false;
        }

        private void ReportVerbose(string s)
        {
            if ((OnReportProgress != null) && Verbose)
            {
                OnReportProgress(s);
            }
        }

        private void ReportError(string s)
        {
            if (OnReportProgress != null)
            {
                OnReportProgress(s);
            }
        }
    }
}
