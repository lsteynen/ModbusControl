using System;
using System.Collections.Generic;
using System.Threading;

using LST.Helpers;
using Modbus_net;
using ModbusControlSpace;


namespace ModbusExport
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

    public class Export
    {
        public byte mSlaveAddress;
        public ModbusInterface mModbus;
        public int mExportedCount;
        public int mErrorCount;
        private const int mSleeptime = 10;
        public string mFilename;
        public string mMbcFilename;
        public bool Verbose;
        public bool ContinueOnError = false;
        public string ExportFormat = "";        // ""=default, I=int, U=UInt, H=Hex 
        public string ExportAsciiFormat = "";   // ""=default/ASCII, I=Int, U=UInt, H=Hex 
        public int MaximumFrameSize = 256;
        public List<CProject.TTab> TabsList;    // List with all tab pages
        private CsvWriter mCsvWriter;

        // callback to convert app register numbering to protocol offset
        public event GetHoldingregisterModbusOffset OnGetHoldingregisterModbusOffset;
        public delegate bool GetHoldingregisterModbusOffset(UInt32 Regnr, out UInt16 Offset);

        // callback for progress report, only if Verbose=true
        public event ReportProgress OnReportProgress;
        public delegate void ReportProgress(string s);

        public struct TReadGroup
        {
            public int lineIndex;
            public int lineCount;
            public List<int> lineIndexes;
            public UInt32 firstReg;
            public UInt16 regCount;
        };

        /// <summary>
        /// Do export
        /// Read registers from target and write to file 
        /// Throw on error
        /// </summary>
        public void DoExport()
        {
            // check if port is opened
            if (mModbus.IsOpen == false)
            {
                throw new CommsException("Connection is not open!");
            }

            // Use Windows's current ANSI codepage encoding, this allows easy import in Excel.
            // ANSI encoding is 1 byte per character, 0-127 is ASCII, 128-255 is per codepage.
            // Encodings: Encoding.Default for the system's default encoding, usually codepage 1252 in West-EU
            //            Encoding.ASCII for strict ASCII (0-127 only)
            //            System.Text.Encoding.GetEncoding(1252) for windows codepage 1252 for Latin 1 languages
            //            System.Text.Encoding.GetEncoding("ISO-8859-2") for ISO codepage with Polish
            //            Encoding.UTF8 for encoding the whole unicode character set in UTF8
            // See http://www.science.co.il/Language/Locale-codes.asp for a list of languages/countries and codepages.

            using (mCsvWriter = new CsvWriter(mFilename, System.Text.Encoding.Default))
            {
                mCsvWriter.CommentHeader = "#";
                mCsvWriter.Separator = ';';
                // always put quotes around the description field
                mCsvWriter.QuoteSomeFields = new bool[] { false, false, true }; // we choose to always quote the description field

                ReportVerbose("Opened file: " + mFilename);
                //("(Encoding used: " + enc.WebName + ", Windows code page: " + enc.WindowsCodePage + ")" + Environment.NewLine);

                // export file header info
                mCsvWriter.WriteComment("Export file created by ModbusExport " + AppHelpers.VersionString + " - " + DateTime.Now.ToString());
                mCsvWriter.WriteComment("Project file: " + mMbcFilename);
                mCsvWriter.WriteComment("Fields: Register;Size;Description;Read/write;Format;Value");

                // export each tab
                foreach (CProject.TTab tab in TabsList)
                {
                    DoExportTab(tab);
                }
            }
        }

        public void DoExportTab(CProject.TTab tab)
        {
            // get tab name 
            string tabName = tab.Name;

            // show tab name 
            mCsvWriter.WriteComment("Tab: " + tabName);
            ReportVerbose("Export tab: \"" + TrimText(tabName) + "\"");

            // output vars
            // read in groups for good speed
            TReadGroup grp = default;
            grp.lineIndex = 0;
            while (GetRegistersGroupToRead(tab, ref grp, MaximumFrameSize))
            {
                //Debug:
                //ReportVerbose("Read group: " + grp.firstReg.ToString() + " #" + grp.regCount.ToString());
                //foreach (int i in grp.lineIndexes)
                //{
                //    //ReportVerbose("    " + i.ToString());
                //    CProject.TVar var = tab.VarList[i];
                //    string s = "Reg " + var.Regnr.ToString() + " size " + var.Size + " descr \"" + TrimText(var.Description) + "\"";
                //    ReportVerbose("    " + s);
                //}

                UInt16 RegOffset = 0;
                if (OnGetHoldingregisterModbusOffset(grp.firstReg, out RegOffset) == true)
                {
                    // OK
                    //ReportVerbose("    RegOffset:" + RegOffset.ToString());

                    // read 
                    short[] aValues;
                    ModbusProtocol.Result result = mModbus.MifReadHoldingRegisters(mSlaveAddress, RegOffset, grp.regCount, out aValues);
                    if (result == ModbusProtocol.Result.SUCCESS)
                    {
                        // output each register of the group
                        int ValueOffset = 0;
                        foreach (int i in grp.lineIndexes)
                        {
                            CProject.TVar var = tab.VarList[i];

                            //Debug:
                            //string s = "Reg " + var.Regnr.ToString() + " size " + var.Size + " descr \"" + TrimText(var.Description) + "\"";
                            //ReportVerbose("    " + s);

                            // convert format to type
                            Global.eType Type = Global.ConvertFormatToType(var.Format);

                            // get the value, 16 or 32-bit, watch size and endian
                            short[] aTmpRegs = new short[2];
                            aTmpRegs[0] = aValues[ValueOffset++];
                            aTmpRegs[1] = 0;
                            // is it 32-bit?
                            if (var.Size == 2)
                            {
                                aTmpRegs[1] = aValues[ValueOffset++];
                            }
                            Int32 Value = Endians.ConvertShortsToValue(aTmpRegs, Type, var.Size * 16);

                            OutputExportLine(var.Regnr, var.Size, var.Description, var.Format, var.isReadable, var.isWriteable, Value);

                            ++mExportedCount;
                        }

                        // next group
                        grp.lineIndex += grp.lineCount;
                    }
                    else
                    {
                        // error

                        // if it is an address error? and we tried a group read? then read alls vars of this group with single reads to get per-var errors
                        //  some slaves return eILLEGALDATAVALUE if address + regcount is invalid, some return ILLEGALADDRESSEXCEPTION
                        if (((result == ModbusProtocol.Result.ILLEGALADDRESSEXCEPTION) || (result == ModbusProtocol.Result.ILLEGALDATAVALUEEXCEPTION)) && (grp.lineCount > 1))
                        {
                            // read each var separately
                            foreach (int i in grp.lineIndexes)
                            {
                                CProject.TVar var = tab.VarList[i];

                                // get register offset
                                OnGetHoldingregisterModbusOffset(var.Regnr, out RegOffset);
                                // get number of 16-bit registers to read
                                UInt16 ReadCount = (UInt16)var.Size;
                                //Debug.Print(DateTime.Now.TimeOfDay + ": MifReadHoldingRegisters(" + mSlaveAddress + "," + GetUserReg(ReadOffset) + "," + ReadCount + ")");
                                result = mModbus.MifReadHoldingRegisters(mSlaveAddress, RegOffset, ReadCount, out aValues);
                                //Debug.Print(DateTime.Now.TimeOfDay + ": MifReadHoldingRegisters returned " + result);
                                if (result == ModbusProtocol.Result.SUCCESS)
                                {
                                    // get the value, 16 or 32-bit

                                    // convert format to type
                                    Global.eType Type = Global.ConvertFormatToType(var.Format);

                                    // get the value, 16 or 32-bit, watch size and endian
                                    short[] aTmpRegs = new short[2];
                                    aTmpRegs[0] = aValues[0];
                                    aTmpRegs[1] = 0;
                                    // is it 32-bit?
                                    if (var.Size == 2)
                                    {
                                        aTmpRegs[1] = aValues[1];
                                    }
                                    Int32 Value = Endians.ConvertShortsToValue(aTmpRegs, Type, var.Size * 16);

                                    OutputExportLine(var.Regnr, var.Size, var.Description, var.Format, var.isReadable, var.isWriteable, Value);

                                    ++mExportedCount;
                                }
                                else
                                {
                                    // error
                                    // OutputExportLineError() handles ContinueOnError
                                    OutputExportLineError(var.Regnr, var.Size, var.Description, var.Format, var.isReadable, var.isWriteable, ModbusProtocol.GetResultText(result));
                                    ++mErrorCount;
                                }
                            }

                            // next group
                            grp.lineIndex += grp.lineCount;
                        }
                        else
                        {
                            // other errors
                            // show error for each register
                            foreach (int i in grp.lineIndexes)
                            {
                                CProject.TVar var = tab.VarList[i];

                                //Debug:
                                //string s = "Reg " + var.Regnr.ToString() + " size " + var.Size + " descr \"" + TrimText(var.Description) + "\"";
                                //ReportVerbose("    " + s);

                                // OutputExportLineError() handles ContinueOnError
                                OutputExportLineError(var.Regnr, var.Size, var.Description, var.Format, var.isReadable, var.isWriteable, ModbusProtocol.GetResultText(result));
                                ++mErrorCount;
                            }

                            // next group
                            grp.lineIndex += grp.lineCount;
                        }
                    }
                }
                else
                {
                    string s = "Register " + grp.firstReg.ToString() + ": Register number is invalid!";
                    if (ContinueOnError)
                        ReportError("Error: " + s);
                    else
                        throw new FileException(s);
                }
            }
        }

        /// <summary>
        /// Get a group of successive registers to read
        /// </summary>
        /// <param name="rg">struct TReadGroup, in/out</param>
        /// <param name="maxFrameSize">maximum size of the response frame (in bytes)</param>
        /// <returns></returns>
        public bool GetRegistersGroupToRead(CProject.TTab tab, ref TReadGroup rg, int maxFrameSize)
        {
            if (tab.VarList.Count == 0)
            {
                return false;
            }

            rg.lineIndexes = new List<int>();

            // bepaal max aantal 16-bit registers te lezen
            int max16BitRegs = (int)(maxFrameSize - 5) / 2;

            // vind eerste readable register vanaf index
            while (rg.lineIndex < tab.VarList.Count)
            {
                if (tab.VarList[rg.lineIndex].isReadable)
                    break;
                ++rg.lineIndex;
            }
            // found?
            if (rg.lineIndex < tab.VarList.Count)
            {
                // found
            }
            else
            {
                // not found
                return false;
            }

            // get this register number
            rg.firstReg = tab.VarList[rg.lineIndex].Regnr;
            int thislen = tab.VarList[rg.lineIndex].Size;   // Size in number of 16-bit registers
            rg.regCount = (UInt16)thislen;
            UInt32 nextReg = rg.firstReg + (UInt32)thislen;
            rg.lineCount = 1;
            rg.lineIndexes.Add(rg.lineIndex);

            // get consecutive registers to read
            int linei = rg.lineIndex + 1;
            while (linei < tab.VarList.Count)
            {
                // consecutive? readable?
                if ((tab.VarList[linei].Regnr == nextReg)
                  && (tab.VarList[linei].isReadable == true))
                {
                    thislen = tab.VarList[linei].Size;     // Size in number of 16-bit registers
                    if ((rg.regCount + thislen) <= max16BitRegs)           // allowed to read so many registers?
                    {
                        nextReg = nextReg + (UInt32)thislen;
                        rg.regCount += (UInt16)thislen;
                        rg.lineCount++;
                        rg.lineIndexes.Add(linei);
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    break;
                }
                ++linei;
            }

            return true;
        }

        private bool OutputExportLine(UInt32 Reg, int Size, string Description, Global.eFormat Format, bool isReadable, bool isWriteable, int Value)
        {
            // output 1 export line, all register info, and value

            // width in bits
            int Width = Size * 16;

            // make RW string
            string RWstring = (isReadable ? "r" : "") + (isWriteable ? "w" : "");

            // Export format as in grid? or override?
            if (ExportFormat == "")
            {
                // As in grid
                // ASCII can be exported as Int or Hex to avoid CSV format problems
                if (Format == Global.eFormat.ASCII)
                {
                    if (ExportAsciiFormat == "I") Format = Global.eFormat.Int;
                    if (ExportAsciiFormat == "U") Format = Global.eFormat.UInt;
                    if (ExportAsciiFormat == "H") Format = Global.eFormat.Hex;
                }
            }
            else if (ExportFormat == "I")
            {
                // All to Int
                Format = Global.eFormat.Int;
            }
            else if (ExportFormat == "U")
            {
                // All to UInt
                Format = Global.eFormat.UInt;
            }
            else if (ExportFormat == "H")
            {
                // All to Hex
                Format = Global.eFormat.Hex;
            }

            // make value string
            // decimals are not used in export
            // minlen is not used in export
            string ValueString = CValueToString.ValueToString(Value, Width, Format);

            // output to file
            string[] fields = new string[] { Reg.ToString(), Width.ToString(), Description, RWstring, Format.ToString(), ValueString };
            mCsvWriter.WriteRow(fields);

            // and log
            string s = Reg.ToString() + ";" + Width.ToString() + ";\"" + TrimText(Description) + "\";" + RWstring + ";" + Format + ";" + ValueString;
            ReportVerbose("Read: " + s);

            return true;
        }

        private bool OutputExportLineError(UInt32 Reg, int Size, string Description, Global.eFormat Format, bool isReadable, bool isWriteable, string Error)
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
            string s = Reg.ToString() + ";" + Width.ToString() + ";\"" + TrimText(Description) + "\";" + RWstring + ";" + Format + ";?   >> ERROR: " + Error;
            if (ContinueOnError)
                ReportError("Error: " + s);
            else
                throw new CommsException(s);

            return true;
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
