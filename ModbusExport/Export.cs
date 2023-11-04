using System;
using System.Collections.Generic;

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
        public int mSuccessCount;
        public int mErrorCount;
        public string mFilename;
        public string mMbcFilename;
        public bool Verbose;
        public bool ContinueOnError = false;
        public string ExportFormat = "";        // ""=default, I=int, U=UInt, H=Hex 
        public string ExportAsciiFormat = "";   // ""=default/ASCII, I=Int, U=UInt, H=Hex 
        public int MaximumFrameSize = 256;
        public List<CProject.TTab> TabsList;    // List with all tab pages
        public List<string> TabsSelectList;      // List of selected tab names to export, in this order

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

        private const int mSleeptime = 10;
        private CsvWriter mCsvWriter;

        private class ExportOrderElem
        {
            public int tabIndex;
            public uint order;
        }

        /// <summary>
        /// Do export
        /// Read registers from target and write to file 
        /// Throw on error
        /// </summary>
        public void DoExport()
        {
        	// read registers from target, write to file

			// reset counters
       		mSuccessCount = 0;
        	mErrorCount = 0;
  
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

                // export tabs/registers
                List<ExportOrderElem> exportList = null;

                // all tabs or TabsSelectList?
                if ((TabsSelectList != null) && (TabsSelectList.Count > 0))
                {
                    // tabs selected on command line

                    // make list of tabs in export order
                    exportList = GetTabsExportOrderListFromSelectList(TabsSelectList);
                }
                else
                {
                    // all tabs

                    // make list of tabs in export order
                    exportList = GetTabsExportOrderList();
                }

                // export in requested order
                foreach (ExportOrderElem tabElem in exportList)
                {
                    CProject.TTab tab = TabsList[tabElem.tabIndex];
                    DoExportTab(tab);
                }
            }
        }

        private List<ExportOrderElem> GetTabsExportOrderList()
        {
            List<ExportOrderElem> list = new List<ExportOrderElem>();

            for (int i = 0; i < TabsList.Count; ++i)
            {
                ExportOrderElem el = new ExportOrderElem();
                el.tabIndex = i;
                el.order = TabsList[i].ExportOrder;
                list.Add(el);
            }

            list.Sort(ExportOrderComparer);

            return list;
        }

        private List<ExportOrderElem> GetTabsExportOrderListFromSelectList(List<string> selectList)
        {
            List<ExportOrderElem> list = new List<ExportOrderElem>();

            foreach( string tn in selectList)
            {
                bool found = false;
                for (int i = 0; i < TabsList.Count; ++i)
                {
                    if (TabsList[i].Name.ToLower() == tn.ToLower())
                    {
                        ExportOrderElem el = new ExportOrderElem();
                        el.tabIndex = i;
                        el.order = 0;
                        list.Add(el);
                        found = true;
                        break;
                    }
                }

                // error if tab name not found
                if (found == false)
                {
                    throw new ArgumentException("Invalid tab name: '" + tn + "'");
                }
            }

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

        public void DoExportTab(CProject.TTab tabPage)
        {
            // read registers from target, write to file

            short[] readData;

            // get tab name 
            string tabName = tabPage.Name;

            // show tab name 
            mCsvWriter.WriteComment("Tab: " + tabName);
            ReportVerbose("Export tab: \"" + TrimText(tabName) + "\"");

            // get groups of vars to read
            List<TReadGroup> groupList = GetReadGroupingList(tabPage, MaximumFrameSize);

            // read each group, output data
            foreach (TReadGroup rg in groupList)
            {
                //Debug:
                //ReportVerbose("Read group: " + rg.readFirstReg.ToString() + " #" + rg.readRegCount.ToString());

                // get register offset
                UInt16 regOffset = 0;
                if (OnGetHoldingregisterModbusOffset(rg.readFirstReg, out regOffset) == true)
                {
                    //ReportVerbose("    firstRegOffset:" + regOffset.ToString());

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
							TVarProperties varp = GetVarPropertiesByIndex(tabPage, lineIndex);

                            //Debug:
                            //string s = "Reg " + var.Regnr.ToString() + " size " + var.Size + " descr \"" + TrimText(var.Description) + "\"";
                            //ReportVerbose("    " + s);

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

                            OutputExportLine(varp.Reg, varp.Size, varp.Descr, varp.Format, varp.Readable, varp.Writeable, Value);
                            ++mSuccessCount;
                        }
                    }
                    else
                    {
                        // error

                        // if it is an address error? and we tried a group read? then read alls vars of this group with single reads to get per-var errors
                        //  some slaves return eILLEGALDATAVALUE if address + regcount is invalid, some return ILLEGALADDRESSEXCEPTION
                        if (((result == ModbusProtocol.Result.ILLEGALADDRESSEXCEPTION) || (result == ModbusProtocol.Result.ILLEGALDATAVALUEEXCEPTION)) && (rg.lineCount > 1))
                        {
                            // read per var instead of group, detect error per var
							int lineCount = 0;
							for (int lineIndex = rg.lineIndex; lineCount < rg.lineCount; ++lineIndex, ++lineCount)
                            {
								TVarProperties varp = GetVarPropertiesByIndex(tabPage, lineIndex);

                                // get register offset
                                OnGetHoldingregisterModbusOffset(varp.Reg, out regOffset);
                                //Debug.Print(DateTime.Now.TimeOfDay + ": MifReadHoldingRegisters(" + mSlaveAddress + "," + GetUserReg(ReadOffset) + "," + ReadCount + ")");
                                result = mModbus.MifReadHoldingRegisters(mSlaveAddress, regOffset, (UInt16)varp.Size, out readData);
                                //Debug.Print(DateTime.Now.TimeOfDay + ": MifReadHoldingRegisters returned " + result);
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

                                    OutputExportLine(varp.Reg, varp.Size, varp.Descr, varp.Format, varp.Readable, varp.Writeable, Value);
                                    ++mSuccessCount;
                                }
                                else
                                {
                                    // error
                                    // OutputExportLineError() handles ContinueOnError
                                    OutputExportLineError(varp.Reg, varp.Size, varp.Descr, varp.Format, varp.Readable, varp.Writeable, ModbusProtocol.GetResultText(result));
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
                                TVarProperties varp = GetVarPropertiesByIndex(tabPage, lineIndex);
                            
                                //Debug:
                                //string s = "Reg " + varp.Reg.ToString() + " size " + varp.Size + " descr \"" + TrimText(varp.Descr) + "\"";
                                //ReportVerbose("    " + s);

                                // OutputExportLineError() handles ContinueOnError
                                OutputExportLineError(varp.Reg, varp.Size, varp.Descr, varp.Format, varp.Readable, varp.Writeable, ModbusProtocol.GetResultText(result));
                                ++mErrorCount;
                            }
                        }
                    }
                }
                else
                {
                    string s = "Register " + rg.readFirstReg.ToString() + ": Register number is invalid!";
                    if (ContinueOnError)
                        ReportError("Error: " + s);
                    else
                        throw new FileException(s);
                }
            }
        }

        /// <summary>
        /// Get a list of group read commands
        /// </summary>
        /// <param name="maxFrameSize">maximum size of the response frame (in bytes)</param>
        /// <returns></returns>
        public List<TReadGroup> GetReadGroupingList(CProject.TTab tab, int maxFrameSize)
        {
            List<TReadGroup> glist = new List<TReadGroup>();

            // no registers?
            if (tab.VarList.Count == 0)
            {
                return null;
            }

            // bepaal max aantal 16-bit registers te lezen
            int max16BitRegs = (int)(maxFrameSize - 5) / 2;

            bool grouping = false;
            int lineIndex = 0;
            int groupFirstLineIndex = 0;
            int groupLineCount = 0;
            UInt32 groupFirstReg = 0;
            int groupRegCount = 0;
            UInt32 groupNextRegister = 0;
            foreach (CProject.TVar var in tab.VarList)
            {
                if (grouping)
                {
                    // find next readable register in group
                    // is consecutive and readable?
                    if ((var.Regnr == groupNextRegister)
                        && (var.isReadable == true)
                        && ((groupRegCount + var.Size) <= max16BitRegs))           // allowed to read so many registers?
                    {
                        ++groupLineCount;
                        groupRegCount += var.Size;
                        groupNextRegister += (UInt32)var.Size;
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
                    if (var.isReadable)
                    {
                        groupFirstLineIndex = lineIndex;    // grid line index of first Reg
                        groupLineCount = 1;
                        groupFirstReg = var.Regnr;          // first register of group
                        groupRegCount = var.Size;            // Size in number of 16-bit registers
                        groupNextRegister = var.Regnr + (UInt32)var.Size;
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
        public TVarProperties GetVarPropertiesByIndex(CProject.TTab tab, int lineIndex)
        {
            //ModbusGridView.TRegProperties prop = mrv.GetRegisterPropertiesByIndex(0);

            TVarProperties p;

            CProject.TVar reg = tab.VarList[lineIndex];

            p.Reg = reg.Regnr;
            p.Descr = reg.Description;
            p.Size = reg.Size;
            p.Format = reg.Format;
            p.MinLen = reg.MinLen;
            p.Decimals = reg.Decimals;
            p.Readable = reg.isReadable;
            p.Writeable = reg.isWriteable;
            p.EnumDict = reg.EnumDict;

            return p;
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
