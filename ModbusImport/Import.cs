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

        private class ImportLine
        {
            public int LineNum;
            public UInt32 Reg;
            public int Width;
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
                throw new CommsException("Connection is not open!");
            }

            // process all lines in the file
            // can throw
            DoImportFromList();
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

            ReportVerbose("Open file '" + FileName + "'");

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
                    csvreader.SkipCommentLines = true;
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

                while ((csvreader.ReadRow(out fields) == CsvReader.Result.Data) && bContinue)
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
                        string s = "Line " + csvreader.LineNumber.ToString() + ": Line format is not valid! (need 2, 3, 4, 5 or 6 fields)";
                        if (ContinueOnError)
                            ReportError("Error: " + s);
                        else
                            throw new FileException(s);
                    }

                    // validate the new line and add to list
                    ImportLine imp = new ImportLine();
                    imp.LineNum = csvreader.LineNumber;
                    UInt16 RegOffset = 0;

                    if (uint.TryParse(RegString, out imp.Reg))
                    {
                        if (OnGetHoldingregisterModbusOffset(imp.Reg, out RegOffset) == false)
                        {
                            string s = "Line " + csvreader.LineNumber.ToString() + ": Register number field is invalid!";
                            if (ContinueOnError)
                                ReportError("Error: " + s);
                            else
                                throw new FileException(s);
                        }
                    }
                    else
                    {
                        string s = "Line " + csvreader.LineNumber.ToString() + ": Register number field is invalid!";
                        if (ContinueOnError)
                            ReportError("Error: " + s);
                        else
                            throw new FileException(s);
                    }
                    if (GetSize(SizeString, out imp.Width) == false)
                    {
                        string s = "Line " + csvreader.LineNumber.ToString() + ": Register size field is invalid!";
                        if (ContinueOnError)
                            ReportError("Error: " + s);
                        else
                            throw new FileException(s);
                    }
                    imp.Description = DescriptionString;
                    if (GetReadWriteFlags(RwString, out imp.isReadable, out imp.isWriteable) == false)
                    {
                        string s = "Line " + csvreader.LineNumber.ToString() + ": R/W flags field is invalid!";
                        if (ContinueOnError)
                            ReportError("Error: " + s);
                        else
                            throw new FileException(s);
                    }
                    if (GetFormat(FormatString, out imp.Format) == false)
                    {
                        string s = "Line " + csvreader.LineNumber.ToString() + ": Type field is invalid!";
                        if (ContinueOnError)
                            ReportError("Error: " + s);
                        else
                            throw new FileException(s);
                    }
                    bool bIsFloat = (imp.Format == Global.eFormat.Float);
                    if (bIsFloat && (imp.Width != 32))
                    {
                        string s = "Line " + csvreader.LineNumber.ToString() + ": Float format size is not 32-bit!";
                        if (ContinueOnError)
                            ReportError("Error: " + s);
                        else
                            throw new FileException(s);
                    }
                    imp.ValueString = ValueString;
                    Global.eType Type;
                    if (CValueToString.StringToValue(ValueString, bIsFloat, imp.Width, 0, out imp.Value, out Type) == false)
                    {
                        string s = "Line " + csvreader.LineNumber.ToString() + ": Value field is invalid!";
                        if (ContinueOnError)
                            ReportError("Error: " + s);
                        else
                            throw new FileException(s);
                    }

                    // valid line
                    ImportList.Add(imp);
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
        /// Do the actual import, from ImportList
        /// Throws on eror
        /// </summary>
        private void DoImportFromList()
        {
            // Write registers from list to target

            // reset counters
            mNotWriteableCount = 0;
            mWrittenCount = 0;
            mErrorCount = 0;

            foreach (ImportLine line in ImportList)
            {
                // skip lines that are not writeable
                if (line.isWriteable == false)
                {
                    ++mNotWriteableCount;
                    continue;
                }

                // convert format to type
                Global.eType Type = Global.ConvertFormatToType(line.Format);

                // create the modbus values to write
                Int16[] Data = Endians.ConvertValueToShorts(line.Value, Type, (line.Width / 16));

                // convert registernumber to offset
                UInt16 RegOffset;
                if (OnGetHoldingregisterModbusOffset(line.Reg, out RegOffset) == false)
                {
                    // error
                    ++mErrorCount;

                    // this is a File error
                    string s = "Line " + line.LineNum.ToString() + ": Register number is invalid!";
                    if (ContinueOnError)
                        ReportError("Error: " + s);
                    else
                        throw new FileException(s);
                }

                // write
                ModbusProtocol.Result result = mModbus.MifWriteHoldingRegistersAuto(mSlaveAddress, RegOffset, (ushort)(line.Width / 16), Data);

                if (result == ModbusProtocol.Result.SUCCESS)
                {
                    // success
                    ++mWrittenCount;

                    string s = BuildImportLineString(line);
                    ReportVerbose("Write: " + s);
                }
                else
                {
                    // error
                    ++mErrorCount;

                    string s = "Line " + line.LineNum.ToString() + ": Write: " + BuildImportLineString(line) + " >> " + ModbusProtocol.GetResultText(result);
                    if (ContinueOnError)
                        ReportError("Error: " + s);
                    else
                        throw new FileException(s);
                }

                Thread.Sleep(mSleeptime);
            }

            // complete
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

        private string BuildImportLineString(ImportLine line)
        {
           // trim descr
            string descr = TrimText(line.Description);
            // make RW string
            string RWstring = (line.isReadable ? "r" : "") + (line.isWriteable ? "w" : "");
            // make output string
            string s = line.Reg + ";" + line.Width + ";\"" + descr + "\";" + RWstring + ";" + line.Format + ";" + line.ValueString;

            return s;
        }

        private bool GetSize(string text, out int size)
        {
            // allow 16, 32
            return int.TryParse(text, out size) && ((size == 16) || (size == 32));
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
