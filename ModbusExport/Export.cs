using System;
using System.Text;
using System.Collections.Generic;

using Modbus_net;
using LST.Helpers;
using ModbusControlSpace;

// The first part of this file is different for the GUI en commandline versions.
// The second part, in region "Implementation - shared" is almost identical.

namespace ModbusExport
{
    public class Export
    {
        #region Enums

        //private enum EExportMode { CurrentTab = 0, AllTabs };
        private enum EExportFormat { Default = 0, AsInt, AsUInt, AsHex };
        public enum EReturnCode { Success = 0, FileError, CommError };

        #endregion

        #region Data

        //private MainForm mParent;
        //private BackgroundWorker mWorker;
        private CsvWriter mCsvWriter;
        private const int mSleeptime = 10;
        //private bool mContinueOnError = false;
        //private EExportMode mExportMode = EExportMode.CurrentTab;
        private EExportFormat mExportFormat = EExportFormat.Default;
        private EExportFormat mStringExportFormat = EExportFormat.Default;
        
        #endregion

        #region Properties

        public ModbusInterface Modbus;

        public byte SlaveAddress;
        
        public string ExportFormat
        {
            get
            {
                if (mExportFormat == EExportFormat.Default) return "default";
                else if (mExportFormat == EExportFormat.AsInt) return "int";
                else if (mExportFormat == EExportFormat.AsUInt) return "uint";
                else if (mExportFormat == EExportFormat.AsHex) return "hex";
                else return null;
            }

            set
            {
                if (value == "default") mExportFormat = EExportFormat.Default;
                else if (value == "int") mExportFormat = EExportFormat.AsInt;
                else if (value == "uint") mExportFormat = EExportFormat.AsUInt;
                else if (value == "hex") mExportFormat = EExportFormat.AsHex;
            }
        }

        public string StringExportFormat
        {
            get
            {
                if (mStringExportFormat == EExportFormat.Default) return "string";
                else if (mStringExportFormat == EExportFormat.AsInt) return "int";
                else if (mStringExportFormat == EExportFormat.AsUInt) return "uint";
                else if (mStringExportFormat == EExportFormat.AsHex) return "hex";
                else return null;
            }

            set
            {
                if (value == "string") mStringExportFormat = EExportFormat.Default;
                else if (value == "int") mStringExportFormat = EExportFormat.AsInt;
                else if (value == "uint") mStringExportFormat = EExportFormat.AsUInt;
                else if (value == "hex") mStringExportFormat = EExportFormat.AsHex;
            }
        }

        public int MaximumFrameSize = 256;

        public bool ContinueOnError = false;

        public int SuccessCount;

        public int FileErrorCount;

        public int CommErrorCount;

        public string Filename;

        public bool Verbose;

        public string MbcFilename;

        public List<CProject.TTab> TabsList;   // List with all tab pages

        public List<string> TabsSelectList;      // List of selected tab names to export, in this order

        // callback for progress report, only if Verbose=true
        public event ReportProgress OnReportProgress;
        public delegate void ReportProgress(string s);

        public class CReadGroup
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
            public int Size;            // var size in #registers
            public Global.eFormat Format;      // display as dec, unsigned, hex, binary
            public int MinLen;          // minimum display length
            public int Decimals;        // nr of decimals to show
            public bool Readable;       // readable?
            public bool Writeable;      // writable?
            public Global.CEnum EnumDict;   // enum convert dictionary
        };

        private class ExportOrderItem
        {
            public int tabIndex;
            public uint order;
        }

        #endregion

        #region Implementation

        /// <summary>
        /// Do export of selected tabs
        /// Read registers from target and write to file 
        /// Returns errorcode on error.
        /// Continues exporting when mContinueOnError==true.
        /// </summary>
        /// <returns>
        /// EReturnCode.Success    if there were no error 
        /// EReturnCode.CommError  if there was communication error 
        /// </returns>
        public EReturnCode DoExport()
        {
            EReturnCode retco = EReturnCode.Success;

            // reset counters
            SuccessCount = 0;
            FileErrorCount = 0;
            CommErrorCount = 0;
			
			// open file to write to
            // Use UTF-8 encoding, without BOM. 
            // Encodings: Encoding.Default for the system's default encoding, usually codepage 1252 in West-EU
            //            Encoding.ASCII for strict ASCII (0-127 only)
            //            System.Text.Encoding.GetEncoding(1252) for windows codepage 1252 for Latin 1 languages
            //            System.Text.Encoding.GetEncoding("ISO-8859-2") for ISO codepage with Polish
            //            Encoding.UTF8 for encoding the whole unicode character set in UTF8
            //            Use "new UTF8Encoding(false)" for UTF-8 encoding without BOM
            // See http://www.science.co.il/Language/Locale-codes.asp for a list of languages/countries and codepages.

            using (mCsvWriter = new CsvWriter(Filename, new UTF8Encoding(false)))
            {
                mCsvWriter.CommentHeader = "#";
                mCsvWriter.Separator = ';';
                // always put quotes around the description field
                mCsvWriter.QuoteSelectedFields = new bool[] { false, false, true }; 

                ReportVerbose("Opened file: " + Filename);

                // export file header info
                mCsvWriter.WriteComment("Export file created by ModbusExport " + AppHelpers.VersionString + " - " + DateTime.Now.ToString());
                mCsvWriter.WriteComment("Project file: " + MbcFilename);
                mCsvWriter.WriteComment("Fields: Register;Size;Description;Read/write;Format;Value");

                // export tabs/registers
                List<ExportOrderItem> exportList = null;

                // all tabs or TabsSelectList?
                if ((TabsSelectList != null) && (TabsSelectList.Count > 0))
                {
                    // export tabs selected on command line

                    // make list of tabs in export order
                    exportList = GetTabsExportOrderListFromSelectList(TabsSelectList);
                }
                else
                {
                    // export all tabs

                    // make list of tabs in export order
                    exportList = GetTabsExportOrderList();
                }

                // export in requested order
                foreach (ExportOrderItem tabItem in exportList)
                {
                    CProject.TTab tab = TabsList[tabItem.tabIndex];
                    // return the first error
                    EReturnCode retco2 = DoExportTab(tab);
                    if (retco2 != EReturnCode.Success)
                    {
                        retco = retco2;
                        if (ContinueOnError == false)
                        {
                            break;
                        }
                    }
                }
            }

            return retco;
        }

        /// <summary>
        /// Get list of tabs to export, in desired order if ExportOrder is specified
        /// </summary>
        private List<ExportOrderItem> GetTabsExportOrderList()
        {
            List<ExportOrderItem> list = new List<ExportOrderItem>();

            for (int i = 0; i < TabsList.Count; ++i)
            {
                ExportOrderItem el = new ExportOrderItem();
                el.tabIndex = i;
                el.order = TabsList[i].ExportOrder;
                list.Add(el);
            }

            list.Sort(ExportOrderComparer);

            return list;
        }

        /// <summary>
        /// Get list of tabs to export, from select-list, in order of select-list
        /// </summary>
        private List<ExportOrderItem> GetTabsExportOrderListFromSelectList(List<string> selectList)
        {
            List<ExportOrderItem> list = new List<ExportOrderItem>();

            foreach (string tn in selectList)
            {
                bool found = false;
                for (int i = 0; i < TabsList.Count; ++i)
                {
                    if (TabsList[i].Name.ToLower() == tn.ToLower())
                    {
                        ExportOrderItem el = new ExportOrderItem();
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


        /// <summary>
        /// Get the next group read block.
        /// Group read: read as many consecutive vars as fit in maxFrameSize.
        /// Or read a single var if it exceeds maxFrameSize. The caller will execute that read in chunks.
        /// </summary>
        /// <param name="maxReadRegisters">maximum registers to read</param>
        /// <returns>group or null at the end</returns>
        CReadGroup GetNextReadGroup(CProject.TTab tab, CReadGroup group, int maxReadRegisters)
        {
            // no registers?
            if (tab.VarList.Count == 0)
            {
                return null;
            }

            // start new group?
            if (group == null)
            {
                group = new CReadGroup();
            }

            // voorlopig gaan we er van uit dat in RegList level 0 alleen registers zitten

            int iLineIndex = group.lineIndex + group.lineCount;
            bool isGrouping = false;
            UInt32 iNextRegister = 0;

            while (iLineIndex < tab.VarList.Count)
            {
                CProject.TVar Reg = tab.VarList[iLineIndex];

                if (isGrouping)
                {
                    // find next readable register in group
                    // is consecutive and readable?
                    // fits in readgroup?
                    if ((Reg.isReadable == true)
                        && (Reg.Regnr == iNextRegister)
                        && ((group.readRegCount + Reg.Size) <= maxReadRegisters))
                    {
                        group.lineCount += 1;
                        group.readRegCount += Reg.Size;
                        iNextRegister = Reg.Regnr + (UInt32)Reg.Size;
                        // try to get more
                    }
                    else
                    {
                        // end group
                        break;
                    }
                }
                else
                {
                    // find next readable register to start new group
                    if (Reg.isReadable)
                    {
                        // start group read
                        group.lineIndex = iLineIndex;
                        group.lineCount = 1;
                        group.readFirstReg = Reg.Regnr;
                        group.readRegCount = Reg.Size;
                        iNextRegister = Reg.Regnr + (UInt32)Reg.Size;
                        isGrouping = true;
                        // got room for more?
                        if (group.readRegCount < maxReadRegisters)
                        {
                            // loop for more                            
                        }
                        else
                        {
                            // end group
                            break;
                        }
                    }
                }

                ++iLineIndex;
            }

            // got more ?
            if (isGrouping)
            {
                return group;
            }

            // no more
            return null;
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

        /// <summary>
        /// Clean description text for cleaner output
        /// </summary>
        private string CleanText(string descr)
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


        #endregion

        #region Implementation - shared

        //
        // The "Implementation - shared" region, doing the actual work, is almost identical for ModbusControl.Export and ModbusExport.Export.
        //

        /// <summary>
        /// Get all registers from a tab page, read them from target, write to file.
        /// Continues exporting after error if mContinueOnError==true.
        /// </summary>
        /// <param name="tabPage"></param>
        /// <returns>
        /// EReturnCode.Success    if there were no error 
        /// EReturnCode.CommError  if there was communication error 
        /// </returns>
        public EReturnCode DoExportTab(CProject.TTab tabPage)
        {
            EReturnCode retcode = EReturnCode.Success;

            // read registers from target, write to file

            bool bContinue = true;
            short[] readData;

            // show tab name 
            string tabName = tabPage.Name;
            mCsvWriter.WriteComment("Tab: " + tabName);
            ReportVerbose("Export tab: \"" + CleanText(tabName) + "\"");

            // determine max number of registers to read in 1 group
            int maxReadRegisters = Modbus.GetMaxRegisterToReadInFrame();

            // get groups of vars to read
            CReadGroup grp = GetNextReadGroup(tabPage, null, maxReadRegisters);
            while ((grp != null) && bContinue)
            {
                /*
                // cancel?
                if (mWorker.CancellationPending)
                {
                    bContinue = false;
                    break;
                }
                */

                // get register offset
                UInt16 regOffset = 0;
                if (TryGetHoldingregisterOffset(grp.readFirstReg, out regOffset) == true)
                {
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

                            TVarProperties varp = GetVarPropertiesByIndex(tabPage, lineIndex);

                            Int16[] aTmpRegs = new Int16[varp.Size];
                            Array.Copy(readData, valueIndex, aTmpRegs, 0, varp.Size);
                            valueIndex += varp.Size;
                            WriteExportVar(varp.Reg, varp.Size, varp.Descr, varp.Format, varp.Readable, varp.Writeable, aTmpRegs);
                            ++SuccessCount;
                            //Thread.Sleep(mSleeptime);
                        }
                    }
                    else
                    {
                        // error

                        // if it is an address error? and we tried a group read? then read alls vars of this group with single reads to get per-var errors
                        //  some slaves return eILLEGALDATAVALUE if address + regcount is invalid, some return ILLEGALADDRESSEXCEPTION
                        if (((result == ModbusProtocol.Result.ILLEGALADDRESSEXCEPTION) || (result == ModbusProtocol.Result.ILLEGALDATAVALUEEXCEPTION)) && (grp.lineCount > 1))
                        {
                            // read per var instead of group, detect error per var
                            int lineCount = 0;
                            for (int lineIndex = grp.lineIndex;
                                 (lineCount < grp.lineCount) && bContinue;
                                 ++lineIndex, ++lineCount)
                            {
                                /*
                                // cancel?
                                if (mWorker.CancellationPending)
                                {
                                    bContinue = false;
                                break;
                                }
                                */

                                TVarProperties varp = GetVarPropertiesByIndex(tabPage, lineIndex);

                                // get register offset
                                regOffset = RegisterToOffset.GetHoldingregisterOffset(varp.Reg);
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

                                    if (ContinueOnError == false)
                                    { 
                                        bContinue = false;
                                    }
                                }

                                //Thread.Sleep(mSleeptime);
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

                                TVarProperties varp = GetVarPropertiesByIndex(tabPage, lineIndex);

                                WriteExportError(varp.Reg, varp.Size, varp.Descr, varp.Format, varp.Readable, varp.Writeable, ModbusProtocol.GetResultText(result));
                                ++CommErrorCount;
                            }

                            retcode = EReturnCode.CommError;

                            if (ContinueOnError == false)
                            {
                                bContinue = false;
                            }
                        }
                    }
                }
                else
                {
                    string s = "Register " + grp.readFirstReg.ToString() + ": Register number is invalid.";
                    ReportError("Error: " + s);
                    ++FileErrorCount;
                    retcode = EReturnCode.CommError;

                    if (ContinueOnError == false)
                    {
                        bContinue = false;
                    }
                }

                // next group
                grp = GetNextReadGroup(tabPage, grp, maxReadRegisters);
            }

            return retcode;
        }

        private bool TryGetHoldingregisterOffset(UInt32 Regnr, out UInt16 Offset)
        {
            Offset = 0;

            try
            {
                Offset = RegisterToOffset.GetHoldingregisterOffset(Regnr);
                return true;
            }
            catch
            {
                // return false;
            }

            return false;
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
    }
}
