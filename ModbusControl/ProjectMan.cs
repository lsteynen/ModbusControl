
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Drawing;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Linq;

using LST.Helpers;

namespace ModbusControlSpace
{
    /// <summary>
    /// The CProject class holds the project that is loaded
    /// </summary>
    public class CProject
    {
        // Types

        private enum eReq { Optional, Required }
        private string[] ConnectionTypes = new string[] { "serial/rtu", "modbus/tcp", "rtu/tcp" }; // Must be order of Global.eConnectionType.SerialRTU
        private string[] WriteHoldingregisterFCs = new string[] { "6", "16", "auto", "alternate" };

        public struct TSize
        {
            public bool WidthSeen;
            public int Width;
            public bool HeightSeen;
            public int Height;
        }

        public struct TConnection
        {
            public Global.eConnectionType ConnectionType;
            public string SerialPort;
            public int SerialBaudrate;
            public Parity SerialParity;
            public int SerialEndOfMessageTimeout;
            public bool SerialOneWireEchoSuppression;
            public string IPAddress;
            public UInt16 IPPort;
            public Global.eTcpConnectMode TcpConnectMode;
            public Byte SlaveID;
            public int MaximumFrameSize;
            public int ResponseTimeout;
            public int InterFrameDelay;
            public Global.eWriteHoldingregFunctionCode WriteHoldingregFunctioncode;
            public Global.eEndian Int32Endian;
            public Global.eEndian Float32Endian;
            public Global.eEndian Ascii16Endian;
        }

        public struct TOptions
        {
            public int HoldingregistersStart;
            public int HoldingregistersEnd;
            public int Samplerate;
            public bool ShowInfo;
            public bool AutostartSeen;
            public bool AutostartEnable;
            public string AutostartTabpage;
        }

        public struct TModbusTcpBridge
        {
            public bool Enable;
            public UInt16 IPPort;
        }

        public struct TTab
        {
            public string Name;
            public List<TVar> VarList; // List with vars in this tab page
        }

        public struct TVar
        {
            public UInt32 Regnr;
            public string Description;
            public Global.eFormat Format;
            public int Size;        // 1 for 16-bit, 2 for 32-bit
            public bool isReadable;
            public bool isWriteable;
            public int MinLen;
            public int Decimals;    // nr of decimals to show
            public Color ForeColor; // foreground/font color
            public Color BackColor; // background color
            public List<TBitfield> Bitfields; // List with bitfields
            public bool Expand;         // expand bitfields
            public Dictionary<int, string> EnumDict; // enum convert dictionary
        }

        // structure holding a bitfield definition
        public struct TBitfield
        {
            public int Position;        // lowest bit of bitfield
            public int Width;           // nr of bits width
            public string Description;
            public Global.eFormat Format;  // display as dec, unsigned, hex, binary
            public int MinLen;          // minimum number of digits to display
            public Color ForeColor;     // foreground/font color
            public Color BackColor;     // background color
            public Dictionary<int, string> EnumDict; // enum convert dictionary
        }

        // structure holding a line and column position in the xml file
        public struct TFilePosition
        {
            public int Line;
            public int Col;
        }

        // list of XML tags to convert to lower case
        List<string> TagsValueToLowerList = new List<string> { "connection", "parity", "writeholdingregisterfc",
                                        "int32endian", "float32endian" , "ascii16endian", "onewireechosuppression",
                                        "showinfo", "enable", "rw", "expandfields", "type", "expandfields" };
                

        // Properties

        public string Path; // path to loaded mbc project file
        public string ValidationSchemaPath; // path to validation XSD file
        public TSize FormSize; // Form size
        public TConnection Connection; // Modbus connection settings
        public TOptions Options; // Options settings
        public TModbusTcpBridge ModbusTcpBridge; // Modbus/TCP bridge settings
        public List<TTab> TabsList; // List with all tab pages
        public string LocalOverrideFile;
        public string LocalOverrideName;
        public bool LocalOverrideParametersUsed;

        private XDocument XmlDoc; // doc holding the current xml mbc project file, for possibility to save
        private XElement XmlRootNode; // root node of the document

        // Constructor
        public CProject()
        {
            // Set defaults
            FormSize.WidthSeen = false;
            FormSize.HeightSeen = false;
            Connection.ConnectionType = Global.eConnectionType.SerialRTU;
            Connection.SerialPort = "COM1";
            Connection.SerialBaudrate = 9600;
            Connection.SerialParity = Parity.None;
            Connection.SerialEndOfMessageTimeout = 100;
            Connection.SerialOneWireEchoSuppression = false;
            Connection.IPAddress = "";
            Connection.IPPort = 502;
            Connection.TcpConnectMode = Global.eTcpConnectMode.Longlived;
            Connection.SlaveID = 1;
            Connection.MaximumFrameSize = 256;
            Connection.ResponseTimeout = 1000;
            Connection.InterFrameDelay = 0;
            Connection.WriteHoldingregFunctioncode = Global.eWriteHoldingregFunctionCode.Auto;
            Connection.Int32Endian = Global.eEndian.Little;
            Connection.Float32Endian = Global.eEndian.Little;
            Connection.Ascii16Endian = Global.eEndian.Big;
            Options.HoldingregistersStart = 40001;
            Options.HoldingregistersEnd = GetHoldingRegistersEnd(Options.HoldingregistersStart);
            Options.Samplerate = 1000;
            Options.ShowInfo = true;
            Options.AutostartSeen = false;
            Options.AutostartEnable = true; // default is enabled if not seen
            Options.AutostartTabpage = "1";
            ValidationSchemaPath = "";
            ModbusTcpBridge.Enable = false;
            ModbusTcpBridge.IPPort = 502;

            // Create list for holding all tabs
            TabsList = new List<TTab>();
        }

        /// <summary>
        /// Load an XML project file 
        /// Throws exception on error
        /// </summary>
        /// <param name="path">Path to mbc file</param>
        public void Load(string path)
        {
            // reset the local override flag
            LocalOverrideParametersUsed = false;

            // Load xml document into XmlDoc
            // A previous document will be discarded when a new is loaded
            XmlDoc = XDocument.Load(path, LoadOptions.PreserveWhitespace | LoadOptions.SetLineInfo);

            // got validation schema?
            // throws exception on validation error
            if (ValidationSchemaPath != "")
            {
                XmlSchemaSet schemas = new XmlSchemaSet();
                schemas.Add("", XmlReader.Create(ValidationSchemaPath));
                XmlDoc.Validate(schemas, ValidationEventHandler);
            }


            // Parse the document
            // Store all data in this class

            // select <modbuscontrol> element as our root
            // XmlDoc.Element() throws an exception if the document has multiple root elements
            XmlRootNode = XmlDoc.Element("modbuscontrol");
            if (XmlRootNode == null)
            {
                throw new XmlException("XML root element <modbuscontrol> not found. The mbc project file should have one root element <modbuscontrol>.");
            }

            // process one <Size> element
            ProcessSizeElement(XmlRootNode);

            // process one <Modbus> element
            ProcessModbusElement(XmlRootNode, eReq.Required);

            // process all <tab>
            ProcessTabs(XmlRootNode);

            // process one <modbustcpserver>
            ProcessModbustcpserverElement(XmlRootNode);

            // process one <autostart>
            // do this after the tabs are loaded
            ProcessAutostartElement(XmlRootNode);

            // Success, store the path
            Path = path;
        }

        static void ValidationEventHandler(object sender, ValidationEventArgs e)
        {
            // throw exception on validation error
            string Msg = "Schema validation failed: " + Environment.NewLine 
                    + e.Message + Environment.NewLine
                    + "Line " + e.Exception.LineNumber.ToString() + ", column " + e.Exception.LinePosition.ToString() + ".";

            throw new XmlException(Msg);
        }

        /// <summary>
        /// Load local overrides for this project
        /// Throws exception on error
        /// </summary>
        /// returns true if localoverride is found, else false
        public bool LoadLocalOverride(string path)
        {
            string Filename = System.IO.Path.GetFileName(path);
            bool IsFound = false;
            LocalOverrideName = null;

            // Load xml document into xdoc
            // A previous document will be discarded when a new is loaded
            XDocument xdoc = XDocument.Load(LocalOverrideFile);

            // select <LocalOverrides> element as our root
            XElement xroot = xdoc.Element("LocalOverrides");
            if (xroot == null)
            {
                // could not find <LocalOverrides> element
                // try lower case
                xroot = xdoc.Element("localoverrides");
                if (xroot == null)
                {
                    throw new XmlException("XML root element <localoverrides> not found. The localoverrides file should have one root element <localoverrides>.");
                }
            }

            // search all <Project> elements for a matching project name
            IEnumerable<XElement> projlist = GetAllElements(xroot, "Project", eReq.Optional);
            foreach (XElement proj in projlist)
            {
                // See if any of the project names match
                IEnumerable<XElement> namelist = GetAllElements(proj, "Name", eReq.Required);
                // Parse all names of this project
                foreach (XElement projname in namelist)
                {
                    string pname = projname.Value.Trim();
                    if (WildcardMatch(Filename, pname, false))
                    {
                        // got a match, publish the name, end search
                        LocalOverrideName = pname;
                        IsFound = true;
                        break;
                    }
                }

                // if found: use these parameters
                if (IsFound)
                {
                    // process one <Size> element
                    LocalOverrideParametersUsed |= ProcessSizeElement(proj);

                    // process one <Modbus> element
                    LocalOverrideParametersUsed |= ProcessModbusElement(proj, eReq.Optional);

                    // process one <modbustcpserver>
                    LocalOverrideParametersUsed |= ProcessModbustcpserverElement(proj);

                    // process one <autostart>
                    // do this after the tabs are loaded
                    ProcessAutostartElement(proj);

                    // end search
                    break;
                }
            }

            return IsFound;
        }

        /// <summary>
        /// See if a filename matches a mask string like 'file*.mbc'
        /// </summary>
        private bool WildcardMatch(String s, String wildcard, bool case_sensitive)
        {
            // Replace the * with an .* and the ? with a dot. Put ^ at the
            // beginning and a $ at the end
            String pattern = "^" + Regex.Escape(wildcard).Replace(@"\*", ".*").Replace(@"\?", ".") + "$";

            // Now, run the Regex as you already know
            Regex regex;
            if (case_sensitive)
                regex = new Regex(pattern);
            else
                regex = new Regex(pattern, RegexOptions.IgnoreCase);

            return (regex.IsMatch(s));
        }


        /// <summary>
        /// Process <Size> element, optional
        /// The width and height are in pixels at 96dpi. For compatiblity of the mbc file
        /// between standard 96-dpi systems and high-dpi systems.
        /// Throws exception on error
        /// </summary>
        /// <param name="pxe"></param>
        /// <return>true if one of the elements was seen</return>
        private bool ProcessSizeElement(XElement pxe)
        {
            bool Seen = false;

            // process one <Size> element
            XElement xe1 = GetSingleElement(pxe, "Size", eReq.Optional);
            if (xe1 != null)
            {
                // Get Width, optional
                XElement xe2 = GetSingleElement(xe1, "Width", eReq.Optional);
                if (xe2 != null)
                {
                    string TmpString = xe2.Value.Trim();
                    UInt32 TmpValue;
                    if (UInt32.TryParse(TmpString, out TmpValue) && (TmpValue <= int.MaxValue))
                    {
                        // keep a minimum width
                        if (TmpValue < 100) TmpValue = 100;
                        // value in mbc file is standarized to 96DPI
                        FormSize.WidthSeen = true;
                        Seen = true;
                        FormSize.Width = Global.ScaleXcoordFrom96DpiToSystemDpi((int)TmpValue);
                    }
                    else
                    {
                        TFilePosition fp = GetFilePos(xe2);
                        throw new XmlException("XML element <" + xe2.Name + "> has an invalid value. Valid values are 0 - " + int.MaxValue + "." + Environment.NewLine + LimitXmlText(xe2.ToString()) + Environment.NewLine, null, fp.Line, fp.Col);
                    }
                }

                // Get Height, optional
                xe2 = GetSingleElement(xe1, "Height", eReq.Optional);
                if (xe2 != null)
                {
                    string TmpString = xe2.Value.Trim();
                    UInt32 TmpValue;
                    if (UInt32.TryParse(TmpString, out TmpValue) && (TmpValue <= int.MaxValue))
                    {
                        // keep a minimum height
                        if (TmpValue < 100) TmpValue = 100;
                        // value in mbc file is standarized to 96DPI
                        FormSize.HeightSeen = true;
                        Seen = true;
                        FormSize.Height = Global.ScaleYcoordFrom96DpiToSystemDpi((int)TmpValue);
                    }
                    else
                    {
                        TFilePosition fp = GetFilePos(xe2);
                        throw new XmlException("XML element <" + xe2.Name + "> has an invalid value. Valid values are 0 - " + int.MaxValue + "." + Environment.NewLine + LimitXmlText(xe2.ToString()) + Environment.NewLine, null, fp.Line, fp.Col);
                    }
                }
            }

            return Seen;
        }

        /// <summary>
        /// Process <Modbus> element, may be required or optional
        /// Throws exception on error
        /// </summary>
        /// <param name="pxe"></param>
        /// <return>true if one of the elements was seen</return>
        private bool ProcessModbusElement(XElement pxe, eReq req)
        {
            bool Seen = false;

            // process one <Modbus> element
            XElement xe1 = GetSingleElement(pxe, "Modbus", req);
            if (xe1 != null)
            {
                // Connection, optional
                XElement xe2 = GetSingleElement(xe1, "Connection", eReq.Optional);
                if (xe2 != null)
                {
                    Seen = true;
                    // accept a few strings
                    string TmpString = xe2.Value.Trim();
                    if (string.Compare(TmpString, ConnectionTypes[0], StringComparison.OrdinalIgnoreCase) == 0)
                    {
                        Connection.ConnectionType = Global.eConnectionType.SerialRTU;
                    }
                    else if (string.Compare(TmpString, ConnectionTypes[1], StringComparison.OrdinalIgnoreCase) == 0)
                    {
                        Connection.ConnectionType = Global.eConnectionType.ModbusTcp;
                    }
                    else if (string.Compare(TmpString, ConnectionTypes[2], StringComparison.OrdinalIgnoreCase) == 0)
                    {
                        Connection.ConnectionType = Global.eConnectionType.RtuOverTcp;
                    }
                    else
                    {
                        TFilePosition fp = GetFilePos(xe2);
                        throw new XmlException("XML element <" + xe2.Name + "> has an invalid value. Valid values are 'serial/rtu', 'modbus/tcp' or 'rtu/tcp'." + Environment.NewLine + LimitXmlText(xe2.ToString()) + Environment.NewLine, null, fp.Line, fp.Col);
                    }
                }

                // serial Port name, optional
                xe2 = GetSingleElement(xe1, "Port", eReq.Optional);
                if (xe2 != null)
                {
                    Seen = true;
                    string TmpString = xe2.Value.Trim();
                    if (string.IsNullOrEmpty(TmpString) == false)
                    {
                        Connection.SerialPort = TmpString;
                    }
                }

                // serial Baudrate, optional
                // >= 50
                xe2 = GetSingleElement(xe1, "Baudrate", eReq.Optional);
                if (xe2 != null)
                {
                    Seen = true;
                    string TmpString = xe2.Value.Trim();
                    UInt32 TmpValue;
                    if ((UInt32.TryParse(TmpString, out TmpValue) && (TmpValue <= 99999999))
                        && (TmpValue >= 50))
                    {
                        Connection.SerialBaudrate = (int)TmpValue;
                    }
                    else
                    {
                        TFilePosition fp = GetFilePos(xe2);
                        throw new XmlException("XML element <" + xe2.Name + "> has an invalid value. Valid values are 50 - 99999999." + Environment.NewLine + LimitXmlText(xe2.ToString()) + Environment.NewLine, null, fp.Line, fp.Col);
                    }
                }

                // serial Parity, optional
                xe2 = GetSingleElement(xe1, "Parity", eReq.Optional);
                if (xe2 != null)
                {
                    Seen = true;
                    string TmpString = xe2.Value.Trim();
                    Parity TmpParity;
                    if (Enum.TryParse<Parity>(TmpString, true, out TmpParity))
                    {
                        Connection.SerialParity = TmpParity;
                    }
                    else
                    {
                        TFilePosition fp = GetFilePos(xe2);
                        throw new XmlException("XML element <" + xe2.Name + "> has an invalid value. Valid values are 'None', 'Even', 'Odd', 'Mark' or 'Space'." + Environment.NewLine + LimitXmlText(xe2.ToString()) + Environment.NewLine, null, fp.Line, fp.Col);
                    }
                }

                // serial EndofmessageTimeout
                xe2 = GetSingleElement(xe1, "EndofmessageTimeout", eReq.Optional);
                if (xe2 != null)
                {
                    Seen = true;
                    string TmpString = xe2.Value.Trim();
                    // get value, limit range 1 - 9999999
                    UInt32 TmpValue;
                    if (uint.TryParse(TmpString, out TmpValue) && (TmpValue >= 1) && (TmpValue <= 9999999))
                    {
                        Connection.SerialEndOfMessageTimeout = (int)TmpValue;
                    }
                    else
                    {
                        TFilePosition fp = GetFilePos(xe2);
                        throw new XmlException("XML element <" + xe2.Name + "> has an invalid value. Valid values are 1 - 9999999." + Environment.NewLine + LimitXmlText(xe2.ToString()) + Environment.NewLine, null, fp.Line, fp.Col);
                    }
                }

                // serial One-wire echo suppression
                xe2 = GetSingleElement(xe1, "OnewireEchoSuppression", eReq.Optional);
                if (xe2 != null)
                {
                    Seen = true;
                    // accept true and false
                    string TmpString = xe2.Value.Trim();
                    bool TmpBool;
                    if (TryParseBool(TmpString, out TmpBool))
                    {
                        Connection.SerialOneWireEchoSuppression = TmpBool;
                    }
                    else
                    {
                        TFilePosition fp = GetFilePos(xe2);
                        throw new XmlException("XML element <" + xe2.Name + "> has an invalid value. Valid values are 'true' or 'false'." + Environment.NewLine + LimitXmlText(xe2.ToString()) + Environment.NewLine, null, fp.Line, fp.Col);
                    }
                }

                // IPaddress, optional, must be blank or valid
                xe2 = GetSingleElement(xe1, "IPaddress", eReq.Optional);
                if (xe2 != null)
                {
                    Seen = true;
                    string TmpString = xe2.Value.Trim();
                    System.Net.IPAddress TmpIPaddress;
                    if (TmpString == "")
                    {
                        // empty is okay
                        Connection.IPAddress = "";
                    }
                    else if (IPAddress.TryParse(TmpString, out TmpIPaddress))
                    {
                        // valid IP addres is okay
                        Connection.IPAddress = TmpString;
                    }
                    else
                    {
                        TFilePosition fp = GetFilePos(xe2);
                        throw new XmlException("XML element <" + xe2.Name + "> has an invalid value. Valid values are empty or a valid IPv4 address like '192.168.1.123' or '127.0.0.1'." + Environment.NewLine + LimitXmlText(xe2.ToString()) + Environment.NewLine, null, fp.Line, fp.Col);
                    }
                }

                // IPport
                xe2 = GetSingleElement(xe1, "IPport", eReq.Optional);
                if (xe2 != null)
                {
                    Seen = true;
                    string TmpString = xe2.Value.Trim();
                    // get value, limit range 1 - 65535
                    UInt16 TmpValue;
                    if (UInt16.TryParse(TmpString, out TmpValue) && (TmpValue >= 1) && (TmpValue <= 65535))
                    {
                        Connection.IPPort = TmpValue;
                    }
                    else
                    {
                        TFilePosition fp = GetFilePos(xe2);
                        throw new XmlException("XML element <" + xe2.Name + "> has an invalid value. Valid values are 1 - 65535." + Environment.NewLine + LimitXmlText(xe2.ToString()) + Environment.NewLine, null, fp.Line, fp.Col);
                    }
                }

                // SlaveID
                xe2 = GetSingleElement(xe1, "SlaveID", eReq.Optional);
                if (xe2 != null)
                {
                    Seen = true;
                    string TmpString = xe2.Value.Trim();
                    // get value, limit range 0 - 255
                    byte TmpValue;
                    if (byte.TryParse(TmpString, out TmpValue))
                    {
                        Connection.SlaveID = TmpValue;
                    }
                    else
                    {
                        TFilePosition fp = GetFilePos(xe2);
                        throw new XmlException("XML element <" + xe2.Name + "> has an invalid value. Valid values are 0 - 255." + Environment.NewLine + LimitXmlText(xe2.ToString()) + Environment.NewLine, null, fp.Line, fp.Col);
                    }
                }

                // ResponseTimeout
                xe2 = GetSingleElement(xe1, "ResponseTimeout", eReq.Optional);
                if (xe2 != null)
                {
                    Seen = true;
                    string TmpString = xe2.Value.Trim();
                    // get value, limit range 1 - 9999999
                    UInt32 TmpValue;
                    if (UInt32.TryParse(TmpString, out TmpValue) && (TmpValue >= 1) && (TmpValue <= 9999999))
                    {
                        Connection.ResponseTimeout = (int)TmpValue;
                    }
                    else
                    {
                        TFilePosition fp = GetFilePos(xe2);
                        throw new XmlException("XML element <" + xe2.Name + "> has an invalid value. Valid values are 1 - 9999999." + Environment.NewLine + LimitXmlText(xe2.ToString()) + Environment.NewLine, null, fp.Line, fp.Col);
                    }
                }

                // MaximumFramesize
                xe2 = GetSingleElement(xe1, "MaximumFramesize", eReq.Optional);
                if (xe2 != null)
                {
                    Seen = true;
                    string TmpString = xe2.Value.Trim();
                    // get value, limit range 13 - 256
                    UInt32 TmpValue;
                    if (UInt32.TryParse(TmpString, out TmpValue) && (TmpValue >= 13) && (TmpValue <= 256))
                    {
                        Connection.MaximumFrameSize = (int)TmpValue;
                    }
                    else
                    {
                        TFilePosition fp = GetFilePos(xe2);
                        throw new XmlException("XML element <" + xe2.Name + "> has an invalid value. Valid values are 13 - 256." + Environment.NewLine + LimitXmlText(xe2.ToString()) + Environment.NewLine, null, fp.Line, fp.Col);
                    }
                }

                // Inter-frame delay
                xe2 = GetSingleElement(xe1, "interframedelay", eReq.Optional);
                if (xe2 != null)
                {
                    Seen = true;
                    string TmpString = xe2.Value.Trim();
                    // get value
                    UInt32 TmpValue;
                    if (UInt32.TryParse(TmpString, out TmpValue))
                    {
                        Connection.InterFrameDelay = (int)TmpValue;
                    }
                    else
                    {
                        TFilePosition fp = GetFilePos(xe2);
                        throw new XmlException("XML element <" + xe2.Name + "> has an invalid value. Use 0 for default." + Environment.NewLine + LimitXmlText(xe2.ToString()) + Environment.NewLine, null, fp.Line, fp.Col);
                    }
                }

                // WriteHoldingregisterFC, optional
                xe2 = GetSingleElement(xe1, "WriteHoldingregisterFC", eReq.Optional);
                if (xe2 != null)
                {
                    Seen = true;
                    string TmpString = xe2.Value.Trim();
                    byte TmpValue;
                    // get value, must be 6, 16, 'auto' or 'alternate'
                    if (byte.TryParse(TmpString, out TmpValue) && ((TmpValue == 6) || (TmpValue == 16)))
                    {
                        if (TmpValue == 16)
                        {
                            Connection.WriteHoldingregFunctioncode = Global.eWriteHoldingregFunctionCode.FC16;
                        }
                        else
                        {
                            Connection.WriteHoldingregFunctioncode = Global.eWriteHoldingregFunctionCode.FC6;
                        }
                    }
                    else if (string.Compare(TmpString, "auto", StringComparison.OrdinalIgnoreCase) == 0)
                    {
                        Connection.WriteHoldingregFunctioncode = Global.eWriteHoldingregFunctionCode.Auto;
                    }
                    else if (string.Compare(TmpString, "alternate", StringComparison.OrdinalIgnoreCase) == 0)
                    {
                        Connection.WriteHoldingregFunctioncode = Global.eWriteHoldingregFunctionCode.Alternate;
                    }
                    else
                    {
                        TFilePosition fp = GetFilePos(xe2);
                        throw new XmlException("XML element <" + xe2.Name + "> has an invalid value. Valid values are '6' (or '06'), '16', 'auto' or 'alternate'." + Environment.NewLine + LimitXmlText(xe2.ToString()) + Environment.NewLine, null, fp.Line, fp.Col);
                    }
                }

                // int32endian
                xe2 = GetSingleElement(xe1, "int32endian", eReq.Optional);
                // if not found, try the old Value32BitEndian for compatibility
                if (xe2 == null)
                {
                    xe2 = GetSingleElement(xe1, "Value32BitEndian", eReq.Optional);
                }
                if (xe2 != null)
                {
                    Seen = true;
                    string TmpString = xe2.Value.Trim();
                    Global.eEndian TmpEndian;
                    if (TryParseEndian(TmpString, out TmpEndian))
                    {
                        Connection.Int32Endian = TmpEndian;
                    }
                    else
                    {
                        TFilePosition fp = GetFilePos(xe2);
                        throw new XmlException("XML element <" + xe2.Name + "> has an invalid value. Valid values are 'little' or 'big'." + Environment.NewLine + LimitXmlText(xe2.ToString()) + Environment.NewLine, null, fp.Line, fp.Col);
                    }
                }

                // float32Endian
                xe2 = GetSingleElement(xe1, "float32endian", eReq.Optional);
                if (xe2 != null)
                {
                    Seen = true;
                    string TmpString = xe2.Value.Trim();
                    Global.eEndian TmpEndian;
                    if (TryParseEndian(TmpString, out TmpEndian))
                    {
                        Connection.Float32Endian = TmpEndian;
                    }
                    else
                    {
                        TFilePosition fp = GetFilePos(xe2);
                        throw new XmlException("XML element <" + xe2.Name + "> has an invalid value. Valid values are 'little' or 'big'." + Environment.NewLine + LimitXmlText(xe2.ToString()) + Environment.NewLine, null, fp.Line, fp.Col);
                    }
                }

                // ascii16endian
                xe2 = GetSingleElement(xe1, "ascii16endian", eReq.Optional);
                if (xe2 != null)
                {
                    Seen = true;
                    string TmpString = xe2.Value.Trim();
                    Global.eEndian TmpEndian;
                    if (TryParseEndian(TmpString, out TmpEndian))
                    {
                        Connection.Ascii16Endian = TmpEndian;
                    }
                    else
                    {
                        TFilePosition fp = GetFilePos(xe2);
                        throw new XmlException("XML element <" + xe2.Name + "> has an invalid value. Valid values are 'little' or 'big'." + Environment.NewLine + LimitXmlText(xe2.ToString()) + Environment.NewLine, null, fp.Line, fp.Col);
                    }
                }

                // HoldingregistersStart
                xe2 = GetSingleElement(xe1, "HoldingregisterStart", eReq.Optional);
                if (xe2 != null)
                {
                    Seen = true;
                    string TmpString = xe2.Value.Trim();
                    UInt32 TmpNum;
                    // allow 40.000, 40.001, 400.000 and 400.001 (dots only here for clearness)
                    // use 4xxxx for holding registers to distinguish from input registers 3xxxx
                    // use 40001 for traditional numbering 
                    // use 400001 for 6-digit numbering when using > 9999 holding registers
                    // use 40000 or 400000 for numbering that actually starts from 0
                    if (UInt32.TryParse(TmpString, out TmpNum)
                        && ((TmpNum == 40000) || (TmpNum == 40001) || (TmpNum == 400000) || (TmpNum == 400001)))
                    {
                        Options.HoldingregistersStart = (int)TmpNum;
                        Options.HoldingregistersEnd = GetHoldingRegistersEnd(Options.HoldingregistersStart);
                    }
                    else
                    {
                        TFilePosition fp = GetFilePos(xe2);
                        throw new XmlException("XML element <" + xe2.Name + "> has an invalid value. Valid values are 40000, 40001, 400000 or 400001. Use 40001 for traditional numbering. Use 400000 or 400001 (6-digit numbering) to access more than 9999 registers. Use 40000 or 400000 if you want to number the holding registers from 0." + Environment.NewLine + LimitXmlText(xe2.ToString()) + Environment.NewLine, null, fp.Line, fp.Col);
                    }
                }

                // Samplerate
                xe2 = GetSingleElement(xe1, "SampleRate", eReq.Optional);
                if (xe2 != null)
                {
                    Seen = true;
                    string TmpString = xe2.Value.Trim();
                    // get value, limit range 0 - 99999999
                    UInt32 TmpValue;
                    if (UInt32.TryParse(TmpString, out TmpValue) && (TmpValue <= 99999999))
                    {
                        Options.Samplerate = (int)TmpValue;
                    }
                    else
                    {
                        TFilePosition fp = GetFilePos(xe2);
                        throw new XmlException("XML element <" + xe2.Name + "> has an invalid value. Valid values are 0 - 99999999." + Environment.NewLine + LimitXmlText(xe2.ToString()) + Environment.NewLine, null, fp.Line, fp.Col);
                    }
                }

                // ShowInfo 
                xe2 = GetSingleElement(xe1, "ShowInfo", eReq.Optional);
                if (xe2 != null)
                {
                    Seen = true;
                    // accept true and false
                    string TmpString = xe2.Value.Trim();
                    bool TmpBool;
                    if (TryParseBool(TmpString, out TmpBool))
                    {
                        Options.ShowInfo = TmpBool;
                    }
                    else
                    {
                        TFilePosition fp = GetFilePos(xe2);
                        throw new XmlException("XML element <" + xe2.Name + "> has an invalid value. Valid values are 'true' or 'false'." + Environment.NewLine + LimitXmlText(xe2.ToString()) + Environment.NewLine, null, fp.Line, fp.Col);
                    }
                }
            }

            return Seen;
        }


        /// <summary>
        /// Process all <tab> elements, optional
        /// Throws exception on error
        /// </summary>
        /// <param name="pxe"></param>
        private void ProcessTabs(XElement pxe)
        {
            // get all <tab> elements, optional
            IEnumerable<XElement> tabList = GetAllElements(pxe, "tab", eReq.Optional);
            foreach (XElement tabNode in tabList)
            {
                // do not allow a <tab> in a <tab>
                XElement xetit = GetSingleElement(tabNode, "tab", eReq.Optional);
                if (xetit != null)
                {
                    TFilePosition fp = GetFilePos(xetit);
                    throw new XmlException("XML element <tab> is nested in another <tab>. This is not allowed." + Environment.NewLine + LimitXmlText(xetit.ToString()) + Environment.NewLine, null, fp.Line, fp.Col);
                }

                // create new tab
                TTab newTab = new TTab();
                // init with default values
                newTab.Name = string.Empty;
                newTab.VarList = new List<TVar>();

                // Must have seen a <reg> in a tab for enabling auto-numbering
                UInt32 NextAutoRegnr = 0;

                // get name, required
                XElement xe2 = GetSingleElement(tabNode, "name", eReq.Required);
                if (xe2 != null)
                {
                    newTab.Name = xe2.Value.Trim();
                }

                // Create a TVar with the defaults for each var
                TVar DefaultVar = new TVar();
                DefaultVar.Regnr = 0;
                DefaultVar.Description = String.Empty;
                DefaultVar.Format = Global.eFormat.Int;
                DefaultVar.Size = 1;
                DefaultVar.isReadable = true;
                DefaultVar.isWriteable = false;
                DefaultVar.MinLen = 0;
                DefaultVar.Decimals = 0;
                DefaultVar.ForeColor = Color.Black;
                DefaultVar.BackColor = Color.White;
                DefaultVar.Bitfields = null;
                DefaultVar.Expand = false;
                DefaultVar.EnumDict = null;

                // in tab we can set defaults for all var's

                // get rw, optional
                XElement rwNode = GetSingleElement(tabNode, "rw", eReq.Optional);
                if (rwNode != null)
                {
                    if (GetReadWriteFlags(rwNode.Value.Trim(), out DefaultVar.isReadable, out DefaultVar.isWriteable) == false)
                    {
                        TFilePosition fp = GetFilePos(rwNode);
                        throw new XmlException("XML element <rw> has an invalid value. Valid values are 'r', 'rw', wr', or 'w'." + Environment.NewLine + LimitXmlText(rwNode.ToString()) + Environment.NewLine, null, fp.Line, fp.Col);
                    }
                }

                // get minimumlength, optional
                XElement minlenNode = GetSingleElement(tabNode, "minlen", eReq.Optional);
                if (minlenNode != null)
                {
                    string TmpString = minlenNode.Value.Trim();
                    int TmpMinlen;
                    if (int.TryParse(TmpString, out TmpMinlen)
                        && ((TmpMinlen >= 1) && (TmpMinlen <= 32)))
                    {
                        DefaultVar.MinLen = TmpMinlen;
                    }
                    else
                    {
                        TFilePosition fp = GetFilePos(minlenNode);
                        throw new XmlException("XML element <minlen> has an invalid value. Valid values are 1 - 32." + Environment.NewLine + LimitXmlText(minlenNode.ToString()) + Environment.NewLine, null, fp.Line, fp.Col);
                    }
                }

                // get decimals, optional
                XElement decimalsNode = GetSingleElement(tabNode, "decimals", eReq.Optional);
                if (decimalsNode != null)
                {
                    string TmpString = decimalsNode.Value.Trim();
                    int TmpDecimals;
                    if (int.TryParse(TmpString, out TmpDecimals)
                        && ((TmpDecimals >= 0) && (TmpDecimals <= 12)))
                    {
                        // ok
                        DefaultVar.Decimals = TmpDecimals;
                    }
                    else
                    {
                        TFilePosition fp = GetFilePos(decimalsNode);
                        throw new XmlException("XML element <decimals> has an invalid value. Valid values are 0 - 12." + Environment.NewLine + LimitXmlText(decimalsNode.ToString()) + Environment.NewLine, null, fp.Line, fp.Col);
                    }
                }

                // get fcol foregroundcolor, optional
                XElement fcolNode = GetSingleElement(tabNode, "fcol", eReq.Optional);
                if (fcolNode != null)
                {
                    try
                    {
                        DefaultVar.ForeColor = ColorFromString.ToColor(fcolNode.Value.Trim());
                    }
                    catch
                    {
                        TFilePosition fp = GetFilePos(fcolNode);
                        throw new XmlException("XML element <fcol> has an invalid value. Valid values are RGB values 0-255 like '255,0,0'; HTML style hex colors #RRGGBB like #FF0000, or a color name like 'Red'." + Environment.NewLine + LimitXmlText(fcolNode.ToString()) + Environment.NewLine, null, fp.Line, fp.Col);
                    }
                }

                // get bcol backgroundcolor, optional
                XElement bcolNode = GetSingleElement(tabNode, "bcol", eReq.Optional);
                if (bcolNode != null)
                {
                    try
                    {
                        DefaultVar.BackColor = ColorFromString.ToColor(bcolNode.Value.Trim());
                    }
                    catch
                    {
                        TFilePosition fp = GetFilePos(bcolNode);
                        throw new XmlException("XML element <bcol> has an invalid value. Valid values are RGB values 0-255 like '255,0,0'; HTML style hex colors #RRGGBB like #FF0000, or a color name like 'Red'." + Environment.NewLine + LimitXmlText(bcolNode.ToString()) + Environment.NewLine, null, fp.Line, fp.Col);
                    }
                }

                // get expandfields, optional
                XElement expandNode = GetSingleElement(tabNode, "expandfields", eReq.Optional);
                if (expandNode != null)
                {
                    string TmpString = expandNode.Value.Trim();
                    bool TmpBool;
                    if (TryParseBool(TmpString, out TmpBool))
                    {
                        DefaultVar.Expand = TmpBool;
                    }
                    else
                    {
                        TFilePosition fp = GetFilePos(expandNode);
                        throw new XmlException("XML element <expandfields> has an invalid value. Valid values are 'true' or 'false'." + Environment.NewLine + LimitXmlText(expandNode.ToString()) + Environment.NewLine, null, fp.Line, fp.Col);
                    }
                }

                // get all <var> elements, optional
                IEnumerable<XElement> varList = GetAllElements(tabNode, "var", eReq.Optional);
                foreach (XElement varNode in varList)
                {
                    // do not allow a <var> in a <var>
                    XElement xeviv = GetSingleElement(varNode, "var", eReq.Optional);
                    if (xeviv != null)
                    {
                        TFilePosition fp = GetFilePos(xeviv);
                        throw new XmlException("XML element <var> is nested in another <var>. This is not allowed." + Environment.NewLine + LimitXmlText(xeviv.ToString()) + Environment.NewLine, null, fp.Line, fp.Col);
                    }

                    // make new var, and init with the defauls
                    TVar newVar = DefaultVar;

                    // get reg, required
                    XElement regNode = GetSingleElement(varNode, "reg", eReq.Required);
                    if (regNode != null)
                    {
                        string regString = regNode.Value.Trim();
                        // auto numbering?
                        if (regString == "*")
                        {
                            // auto numbering
                            if (NextAutoRegnr > 0)
                            {
                                newVar.Regnr = NextAutoRegnr;
                            }
                            else
                            {
                                TFilePosition fp = GetFilePos(regNode);
                                throw new XmlException("Must have seen a valid register number before auto-numbering can be used." + Environment.NewLine + LimitXmlText(regNode.ToString()) + Environment.NewLine, null, fp.Line, fp.Col);
                            }
                        }
                        else
                        {
                            // parse number
                            // and check register number
                            UInt32 Regnr;
                            if (UInt32.TryParse(regString, out Regnr)
                                && ValidateHoldingregNr(Regnr))
                            {
                                // okay
                                newVar.Regnr = Regnr;
                            }
                            else
                            {
                                TFilePosition fp = GetFilePos(regNode);
                                throw new XmlException("<reg> has an invalid value (range defined by <HoldingregisterStart>)" + Environment.NewLine + LimitXmlText(regNode.ToString()) + Environment.NewLine, null, fp.Line, fp.Col);
                            }
                        }
                    }

                    // get description, optional
                    XElement textNode = GetSingleElement(varNode, "text", eReq.Optional);
                    if (textNode != null)
                    {
                        newVar.Description = textNode.Value;
                    }

                    // get type, optional
                    XElement typeNode = GetSingleElement(varNode, "type", eReq.Optional);
                    if (typeNode != null)
                    {
                        if (GetVarTypestringToFormatAndSize(typeNode.Value.Trim(), out newVar.Format, out newVar.Size))
                        {
                            // ok
                            // if the type is not int or uint: reset the default decimals
                            if ((newVar.Format != Global.eFormat.Int) && (newVar.Format != Global.eFormat.UInt))
                            {
                                newVar.Decimals = 0;
                            }
                        }
                        else
                        {
                            TFilePosition fp = GetFilePos(typeNode);
                            throw new XmlException("XML element <type> has an invalid value. Valid values are int16, uint16, int32, uint32, hex16, hex32, bin16, bin32, float32, ascii16 or ascii32." + Environment.NewLine + LimitXmlText(typeNode.ToString()) + Environment.NewLine, null, fp.Line, fp.Col);
                        }
                    }

                    // get minimumlength, optional
                    minlenNode = GetSingleElement(varNode, "minlen", eReq.Optional);
                    if (minlenNode != null)
                    {
                        string TmpString = minlenNode.Value.Trim();
                        int TmpMinlen;
                        if (int.TryParse(TmpString, out TmpMinlen)
                            && ((TmpMinlen >= 1) && (TmpMinlen <= 32)))
                        {
                            newVar.MinLen = TmpMinlen;
                        }
                        else
                        {
                            TFilePosition fp = GetFilePos(minlenNode);
                            throw new XmlException("XML element <minlen> has an invalid value. Valid values are 1 - 32." + Environment.NewLine + LimitXmlText(minlenNode.ToString()) + Environment.NewLine, null, fp.Line, fp.Col);
                        }
                    }

                    // get rw, optional
                    rwNode = GetSingleElement(varNode, "rw", eReq.Optional);
                    if (rwNode != null)
                    {
                        if (GetReadWriteFlags(rwNode.Value.Trim(), out newVar.isReadable, out newVar.isWriteable) == false)
                        {
                            TFilePosition fp = GetFilePos(rwNode);
                            throw new XmlException("XML element <rw> has an invalid value. Valid values are 'r', 'rw', wr', or 'w'." + Environment.NewLine + LimitXmlText(rwNode.ToString()) + Environment.NewLine, null, fp.Line, fp.Col);
                        }
                    }

                    // get fcol foregroundcolor, optional
                    fcolNode = GetSingleElement(varNode, "fcol", eReq.Optional);
                    if (fcolNode != null)
                    {
                        try
                        {
                            newVar.ForeColor = ColorFromString.ToColor(fcolNode.Value.Trim());
                        }
                        catch
                        {
                            TFilePosition fp = GetFilePos(fcolNode);
                            throw new XmlException("XML element <fcol> has an invalid value. Valid values are RGB values 0-255 like '255,0,0'; HTML style hex colors #RRGGBB like #FF0000, or a color name like 'Red'." + Environment.NewLine + LimitXmlText(fcolNode.ToString()) + Environment.NewLine, null, fp.Line, fp.Col);
                        }
                    }

                    // get bcol backgroundcolor, optional
                    bcolNode = GetSingleElement(varNode, "bcol", eReq.Optional);
                    if (bcolNode != null)
                    {
                        try
                        {
                            newVar.BackColor = ColorFromString.ToColor(bcolNode.Value.Trim());
                        }
                        catch
                        {
                            TFilePosition fp = GetFilePos(bcolNode);
                            throw new XmlException("XML element <bcol> has an invalid value. Valid values are RGB values 0-255 like '255,0,0'; HTML style hex colors #RRGGBB like #FF0000, or a color name like 'Red'." + Environment.NewLine + LimitXmlText(bcolNode.ToString()) + Environment.NewLine, null, fp.Line, fp.Col);
                        }
                    }

                    // get decimals, optional
                    decimalsNode = GetSingleElement(varNode, "decimals", eReq.Optional);
                    if (decimalsNode != null)
                    {
                        string TmpString = decimalsNode.Value.Trim();
                        int TmpDecimals;
                        if (int.TryParse(TmpString, out TmpDecimals)
                            && ((TmpDecimals >= 0) && (TmpDecimals <= 12)))
                        {
                            // ok
                            newVar.Decimals = TmpDecimals;
                        }
                        else
                        {
                            TFilePosition fp = GetFilePos(decimalsNode);
                            throw new XmlException("XML element <decimals> has an invalid value. Valid values are 0 - 12." + Environment.NewLine + LimitXmlText(decimalsNode.ToString()) + Environment.NewLine, null, fp.Line, fp.Col);
                        }

                        // Check: decimals != 0 is allowed only for int and uint types
                        if ((newVar.Decimals != 0) && (newVar.Format != Global.eFormat.Int) && (newVar.Format != Global.eFormat.UInt))
                        {
                            TFilePosition fp = GetFilePos(decimalsNode);
                            throw new XmlException("XML element <decimals> with value > 0 is only allowed for int and uint types." + Environment.NewLine + LimitXmlText(decimalsNode.ToString()) + Environment.NewLine, null, fp.Line, fp.Col);
                        }
                    }

                    // get all bitfields <field> in this var, optional
                    IEnumerable<XElement> fieldsList = GetAllElements(varNode, "field", eReq.Optional);
                    if (fieldsList.Count() >= 1)
                    {
                        // auto count capability for position
                        int AutoPosition = 0;
                        foreach (XElement fieldNode in fieldsList)
                        {
                            // do not allow a <field> in a <field>
                            XElement xefif = GetSingleElement(fieldNode, "field", eReq.Optional);
                            if (xefif != null)
                            {
                                TFilePosition fp = GetFilePos(xefif);
                                throw new XmlException("XML element <field> is nested in another <field>. This is not allowed." + Environment.NewLine + LimitXmlText(xefif.ToString()) + Environment.NewLine, null, fp.Line, fp.Col);
                            }

                            // new bitfield
                            TBitfield newBitfield = new TBitfield();
                            // init with default values
                            newBitfield.Position = 0;
                            newBitfield.Width = 1;
                            newBitfield.Description = string.Empty;
                            newBitfield.Format = Global.eFormat.UInt;
                            newBitfield.MinLen = 0;
                            newBitfield.ForeColor = newVar.ForeColor;
                            newBitfield.BackColor = newVar.BackColor;
                            newBitfield.EnumDict = null;

                            // default position is auto count
                            newBitfield.Position = AutoPosition;

                            // get bitfield position, optional
                            XElement positionNode = GetSingleElement(fieldNode, "pos", eReq.Optional);
                            if (positionNode != null)
                            {
                                if (int.TryParse(positionNode.Value.Trim(), out newBitfield.Position)
                                    && (newBitfield.Position >= 0) && (newBitfield.Position <= 31))
                                {
                                    // ok
                                }
                                else
                                {
                                    TFilePosition fp = GetFilePos(positionNode);
                                    throw new XmlException("XML element <pos> has an invalid value. Valid values are 0 - 31." + Environment.NewLine + LimitXmlText(positionNode.ToString()) + Environment.NewLine, null, fp.Line, fp.Col);
                                }
                            }

                            // get bitfield width, optional
                            XElement widthNode = GetSingleElement(fieldNode, "width", eReq.Optional);
                            if (widthNode != null)
                            {
                                if (int.TryParse(widthNode.Value.Trim(), out newBitfield.Width)
                                    && (newBitfield.Width >= 1) && (newBitfield.Width <= 32))
                                {
                                    // ok
                                }
                                else
                                {
                                    TFilePosition fp = GetFilePos(widthNode);
                                    throw new XmlException("XML element <width> has an invalid value. Valid values are 1 - 32." + Environment.NewLine + LimitXmlText(widthNode.ToString()) + Environment.NewLine, null, fp.Line, fp.Col);
                                }
                            }

                            // validate position and width
                            if ((newBitfield.Position + newBitfield.Width) <= (newVar.Size * 16))
                            {
                                // okay
                            }
                            else
                            {
                                TFilePosition fp = GetFilePos(fieldNode);
                                throw new XmlException("XML element <field> has an invalid position + width value. Position + width is " + (newBitfield.Position + newBitfield.Width).ToString() + " but must be <= " + (newVar.Size * 16).ToString() + " to fit." + Environment.NewLine + LimitXmlText(fieldNode.ToString()) + Environment.NewLine, null, fp.Line, fp.Col);
                            }

                            // get description, optional
                            textNode = GetSingleElement(fieldNode, "text", eReq.Optional);
                            if (textNode != null)
                            {
                                newBitfield.Description = textNode.Value;
                            }

                            // get type, optional
                            typeNode = GetSingleElement(fieldNode, "type", eReq.Optional);
                            if (typeNode != null)
                            {
                                if (GetBitfieldTypestringToFormat(typeNode.Value.Trim(), out newBitfield.Format))
                                {
                                    // ok
                                }
                                else
                                {
                                    TFilePosition fp = GetFilePos(typeNode);
                                    throw new XmlException("XML element <type> has an invalid value. Valid values are int, uint, hex, bin or ascii." + Environment.NewLine + LimitXmlText(typeNode.ToString()) + Environment.NewLine, null, fp.Line, fp.Col);
                                }
                            }

                            // get minimumlength (optional)
                            minlenNode = GetSingleElement(fieldNode, "minlen", eReq.Optional);
                            if (minlenNode != null)
                            {
                                if (int.TryParse(minlenNode.Value.Trim(), out newBitfield.MinLen)
                                    && ((newBitfield.MinLen >= 1) && (newBitfield.MinLen <= 32)))
                                {
                                    // ok
                                }
                                else
                                {
                                    TFilePosition fp = GetFilePos(minlenNode);
                                    throw new XmlException("XML element <minlen> has an invalid value. Valid values are 1 - 32." + Environment.NewLine + LimitXmlText(widthNode.ToString()) + Environment.NewLine, null, fp.Line, fp.Col);
                                }
                            }

                            // get fcol foregroundcolor, optional
                            fcolNode = GetSingleElement(fieldNode, "fcol", eReq.Optional);
                            if (fcolNode != null)
                            {
                                try
                                {
                                    newBitfield.ForeColor = ColorFromString.ToColor(fcolNode.Value.Trim());
                                }
                                catch
                                {
                                    TFilePosition fp = GetFilePos(fcolNode);
                                    throw new XmlException("XML element <fcol> has an invalid value. Valid values are RGB values 0-255 like '255,0,0'; HTML style hex colors #RRGGBB like #FF0000, or a color name like 'Red'." + Environment.NewLine + LimitXmlText(fcolNode.ToString()) + Environment.NewLine, null, fp.Line, fp.Col);
                                }
                            }

                            // get bcol backgroundcolor, optional
                            bcolNode = GetSingleElement(fieldNode, "bcol", eReq.Optional);
                            if (bcolNode != null)
                            {
                                try
                                {
                                    newBitfield.BackColor = ColorFromString.ToColor(bcolNode.Value.Trim());
                                }
                                catch
                                {
                                    TFilePosition fp = GetFilePos(bcolNode);
                                    throw new XmlException("XML element <bcol> has an invalid value. Valid values are RGB values 0-255 like '255,0,0'; HTML style hex colors #RRGGBB like #FF0000, or a color name like 'Red'." + Environment.NewLine + LimitXmlText(bcolNode.ToString()) + Environment.NewLine, null, fp.Line, fp.Col);
                                }
                            }

                            // get enum, optional
                            newBitfield.EnumDict = GetEnum(fieldNode);

                            // auto count capability for Position
                            AutoPosition += newBitfield.Width;

                            // Add new bitfield to the list
                            // If we haven't yet got a list: create one
                            if (newVar.Bitfields == null)
                            {
                                newVar.Bitfields = new List<TBitfield>();
                            }
                            newVar.Bitfields.Add(newBitfield);
                        }
                    }

                    // get expandfields, optional
                    expandNode = GetSingleElement(varNode, "expandfields", eReq.Optional);
                    if (expandNode != null)
                    {
                        string TmpString = expandNode.Value.Trim();
                        bool TmpBool;
                        if (TryParseBool(TmpString, out TmpBool))
                        {
                            newVar.Expand = TmpBool;
                        }
                        else
                        {
                            TFilePosition fp = GetFilePos(expandNode);
                            throw new XmlException("XML element <expandfields> has an invalid value. Valid values are 'true' or 'false'." + Environment.NewLine + LimitXmlText(expandNode.ToString()) + Environment.NewLine, null, fp.Line, fp.Col);
                        }
                    }

                    // get enum, optional
                    newVar.EnumDict = GetEnum(varNode);

                    // Add new var to the list
                    newTab.VarList.Add(newVar);
                    // update next auto register numbering
                    NextAutoRegnr = (UInt32)(newVar.Regnr + newVar.Size);
                }

                TabsList.Add(newTab);
            }
        }

        Dictionary<int, string> GetEnum(XElement node)
        {
            Dictionary<int, string> dict = null;

            // get enum, optional
            XElement enumNode = GetSingleElement(node, "enum", eReq.Optional);
            if (enumNode != null)
            {
                // get all <item> in this enum, optional
                IEnumerable<XElement> itemList = GetAllElements(enumNode, "item", eReq.Optional);
                if (itemList.Count() >= 1)
                {
                    dict = new Dictionary<int, string>();

                    // get all items, each item requires value and text
                    foreach (XElement item in itemList)
                    {
                        XElement itemValue = GetSingleElement(item, "value", eReq.Required);
                        string TmpString = itemValue.Value.Trim();
                        int TmpValue;
                        if (int.TryParse(TmpString, out TmpValue))
                        {
                            // OK
                        }
                        else
                        {
                            TFilePosition fp = GetFilePos(itemValue);
                            throw new XmlException("XML element <" + itemValue.Name + "> has an invalid value. Must be valid integer." + Environment.NewLine + LimitXmlText(itemValue.ToString()) + Environment.NewLine, null, fp.Line, fp.Col);
                        }

                        XElement itemText = GetSingleElement(item, "text", eReq.Required);
                        TmpString = itemText.Value.Trim();

                        try
                        {
                            // throws exception on duplicate key
                            dict.Add(TmpValue, TmpString);
                        }
                        catch
                        {
                            TFilePosition fp = GetFilePos(itemValue);
                            throw new XmlException("XML element <" + itemValue.Name + "> has an invalid value. Enum integers must be unique." + Environment.NewLine + LimitXmlText(itemValue.ToString()) + Environment.NewLine, null, fp.Line, fp.Col);
                        }
                    }

                    // got no items? then no dictionary
                    if (dict.Count == 0)
                    {
                        dict = null;
                    }
                }
            }

            return dict;
        }

        /// <summary>
        /// Process the autostart element
        /// This should be done *after* loading all tab pages
        /// </summary>
        private bool ProcessAutostartElement(XElement pxe)
        {
            // process one <autostart> element
            XElement xe1 = GetSingleElement(pxe, "autostart", eReq.Optional);
            if (xe1 != null)
            {
                // if there is an <autostart> tag, the <enable> must be present and true to activate autostart
                Options.AutostartSeen = true;
                Options.AutostartEnable = false;

                // Enable
                XElement xe2 = GetSingleElement(xe1, "enable", eReq.Optional);
                if (xe2 != null)
                {
                    // accept true and false
                    string TmpString = xe2.Value.Trim();
                    bool TmpBool;
                    if (TryParseBool(TmpString, out TmpBool))
                    {
                        Options.AutostartEnable = TmpBool;
                    }
                    else
                    {
                        TFilePosition fp = GetFilePos(xe2);
                        throw new XmlException("XML element <enable> has an invalid value. Valid values are 'true' or 'false'." + Environment.NewLine + LimitXmlText(xe2.ToString()) + Environment.NewLine, null, fp.Line, fp.Col);
                    }
                }

                // Page
                xe2 = GetSingleElement(xe1, "page", eReq.Optional);
                if (xe2 != null)
                {
                    // the page can be the name or a numeric (1 for first tab page)
                    Options.AutostartTabpage = xe2.Value.Trim();
                }

                // Got the enable and the page, now validate the page name/number
                if (Options.AutostartEnable && (string.IsNullOrEmpty(Options.AutostartTabpage) == false))
                {
                    if (TabPageIsValid(Options.AutostartTabpage) == false)
                    {
                        Options.AutostartTabpage = null;
                        TFilePosition fp = GetFilePos(xe2);
                        throw new XmlException("XML element <page> is not a valid tabpage name, or a valid tabpage number 1 .. N." + Environment.NewLine + LimitXmlText(xe2.ToString()) + Environment.NewLine, null, fp.Line, fp.Col);
                    }
                }
            }

            return true;
        }

        /// <summary>
        ///  Check if the page name (or number) is valid
        /// </summary>
        /// <param name="name">Page name or number, number is 1-based</param>
        private bool TabPageIsValid(string name)
        {
            int pagenumber;

            // Is it a number?
            if (int.TryParse(name, out pagenumber))
            {
                // Page number is valid?
                if ((pagenumber >= 1) && (pagenumber <= TabsList.Count))
                {
                    return true;
                }
            }
            else
            {
                // It is a name
                // Check if we have a page with this name
                foreach (TTab tab in TabsList)
                {
                    if (tab.Name == name)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Process <modbustcpserver> element, optional
        /// Throws exception on error
        /// </summary>
        /// <param name="pxe"></param>
        /// <return>true if one of the elements was seen</return>
        private bool ProcessModbustcpserverElement(XElement pxe)
        {
            bool Seen = false;

            // process one <modbustcpserver> element
            XElement xe1 = GetSingleElement(pxe, "modbustcpserver", eReq.Optional);
            if (xe1 != null)
            {
                // Enable
                XElement xe2 = GetSingleElement(xe1, "enable", eReq.Optional);
                if (xe2 != null)
                {
                    // accept true and false
                    string TmpString = xe2.Value.Trim();
                    bool TmpBool;
                    if (TryParseBool(TmpString, out TmpBool))
                    {
                        ModbusTcpBridge.Enable = TmpBool;
                        Seen = true;
                    }
                    else
                    {
                        TFilePosition fp = GetFilePos(xe2);
                        throw new XmlException("XML element <enable> has an invalid value. Valid values are 'true' or 'false'." + Environment.NewLine + LimitXmlText(xe2.ToString()) + Environment.NewLine, null, fp.Line, fp.Col);
                    }
                }

                // Port
                xe2 = GetSingleElement(xe1, "port", eReq.Optional);
                if (xe2 != null)
                {
                    string TmpString = xe2.Value.Trim();
                    // get value, limit range 1 - 65535
                    UInt16 TmpValue;
                    if (UInt16.TryParse(TmpString, out TmpValue) && (TmpValue >= 1) && (TmpValue <= 65535))
                    {
                        // ok
                        ModbusTcpBridge.IPPort = TmpValue;
                        Seen = true;
                    }
                    else
                    {
                        // error
                        TFilePosition fp = GetFilePos(xe2);
                        throw new XmlException("XML element <port> has an invalid value. Valid values are 1 - 65535." + Environment.NewLine + LimitXmlText(xe2.ToString()) + Environment.NewLine, null, fp.Line, fp.Col);
                    }
                }
            }

            return Seen;
        }


        /// <summary>
        /// Get a single XML element with this name
        /// Finds elements with the given case and in lower case. Use both.
        /// Throw exception if not found but it's required
        /// Throw exception if found more than once
        /// Return: null if not found
        /// </summary>
        private XElement GetSingleElement(XElement parent, string name, eReq required)
        {
            // make parent's name in lowercase for error message
            string parentNameLC = parent.Name.ToString().ToLower();
            // get elements with this name
            IEnumerable<XElement> xecoll = parent.Elements(name);
            TFilePosition fp;
            // if the name hase upper case letters, search again with lower case
            if (name.Any(char.IsUpper))
            {
                name = name.ToLower();
                IEnumerable<XElement> xecollLC = parent.Elements(name);
                // append to the list
                xecoll = xecoll.Concat(xecollLC);
            }
            // found 1?
            if (xecoll.Count() == 1)
            {
                // found 1, return 1
                return xecoll.ElementAt(0);
            }
            // not found?
            if (xecoll.Count() == 0)
            {
                // not found but required?
                if (required == eReq.Required)
                {
                    fp = GetFilePos(parent);
                    throw new XmlException("XML element <" + name + "> not found. There should be one <" + name + "> element in a <" + parentNameLC + "> element." + Environment.NewLine, null, fp.Line, fp.Col);
                }
                return null;
            }

            // found more than 1
            // throw exception with fileposition of 2nd
            XElement xe = xecoll.ElementAt(1);
            fp = GetFilePos(xe);
            throw new XmlException("XML element <" + name + "> is found " + xecoll.Count() + " times. It is allowed only once in its parent <" + parentNameLC + "> element." + Environment.NewLine + LimitXmlText(xe.ToString()) + Environment.NewLine, null, fp.Line, fp.Col);

            //return null;
        }

        /// <summary>
        /// Get all XML elements with this name
        /// Finds elements with the given case and in lower case. Use both.
        /// Throw exception if not found but it's required
        /// Return: an empty collection if not found
        /// </summary>
        private IEnumerable<XElement> GetAllElements(XElement parent, string name, eReq required)
        {
            // make parent's name in lowercase for error message
            string parentNameLC = parent.Name.ToString().ToLower();
            // get elements with this name
            IEnumerable<XElement> xecoll = parent.Elements(name);
            // if the tag hase upper case letters, search again with lower case
            if (name.Any(char.IsUpper))
            {
                name = name.ToLower();
                IEnumerable<XElement> xecollLC = parent.Elements(name);
                // append to the list
                xecoll = xecoll.Concat(xecollLC);
            }
            // found?
            if (xecoll.Count() > 0)
            {
                return xecoll;
            }
            // not found but required?
            if (required == eReq.Required)
            {
                TFilePosition fp = GetFilePos(parent);
                throw new XmlException("XML element <" + name + "> not found. There should be at least one <" + name + "> element in a <" + parentNameLC + "> element." + Environment.NewLine, null, fp.Line, fp.Col);
            }
            // return the empty list
            return xecoll;
        }

        /// <summary>
        /// Remove whitespace and limit the string in length
        /// </summary>
        private string LimitXmlText(string text)
        {
            const int maxlen = 180;
            char[] outs = new char[maxlen];
            int j = 0;
            int len = text.Length;

            // copy all characters that are not whitespace, till max length
            for (int i = 0; (i < len) && (j < maxlen); i++)
            {
                if ((text[i] == '\r') || (text[i] == '\n') || (text[i] == '\t'))
                {
                    // skip
                }
                else
                {
                    outs[j++] = text[i];
                }
            }
            // max length reached? Then end with ...
            if (j >= maxlen)
            {
                outs[j - 3] = '.';
                outs[j - 2] = '.';
                outs[j - 1] = '.';
            }
            return new string(outs, 0, j);
        }


        /// <summary>
        /// Parse a string for boolean value true/false
        /// </summary>
        /// <param name="s">string</param>
        /// <param name="result">result value</param>
        /// <returns>true if valid, false if not</returns>
        private bool TryParseBool(string s, out bool result)
        {
            result = false;
            if (string.Compare(s, "true", StringComparison.OrdinalIgnoreCase) == 0)
            {
                result = true;
                return true;
            }
            else if (string.Compare(s, "false", StringComparison.OrdinalIgnoreCase) == 0)
            {
                result = false;
                return true;
            }

            return false;
        }


        /// <summary>
        /// Parse a string for endian value little/big
        /// </summary>
        /// <param name="s">string</param>
        /// <param name="result">result value</param>
        /// <returns>true if valid, false if not</returns>
        private bool TryParseEndian(string s, out Global.eEndian result)
        {
            result = Global.eEndian.Little;
            if (string.Compare(s, "little", StringComparison.OrdinalIgnoreCase) == 0)
            {
                result = Global.eEndian.Little;
                return true;
            }
            else if (string.Compare(s, "big", StringComparison.OrdinalIgnoreCase) == 0)
            {
                result = Global.eEndian.Big;
                return true;
            }

            return false;
        }


        /// <summary>
        /// Check if Holdingreg number is valid, HoldingregistersStart .. HoldingregistersEnd
        /// </summary>
        /// <returns>true if valid</returns>
        private bool ValidateHoldingregNr(UInt32 Regnr)
        {
            if ((Regnr >= Options.HoldingregistersStart) && (Regnr <= Options.HoldingregistersEnd))
            {
                return true;
            }

            return false;
        }


        private bool GetReadWriteFlags(string text, out bool bRead, out bool bWrite)
        {
            // allow "r", "w", "rw", "wr" or ""
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


        private bool GetVarTypestringToFormatAndSize(string text, out Global.eFormat Format, out int Size)
        {
            text = text.ToLower();

            if (text == "int16")
            {
                Format = Global.eFormat.Int;
                Size = 1;
                return true;
            }
            else if (text == "uint16")
            {
                Format = Global.eFormat.UInt;
                Size = 1;
                return true;
            }
            else if (text == "int32")
            {
                Format = Global.eFormat.Int;
                Size = 2;
                return true;
            }
            else if (text == "uint32")
            {
                Format = Global.eFormat.UInt;
                Size = 2;
                return true;
            }
            else if (text == "hex16")
            {
                Format = Global.eFormat.Hex;
                Size = 1;
                return true;
            }
            else if (text == "hex32")
            {
                Format = Global.eFormat.Hex;
                Size = 2;
                return true;
            }
            else if (text == "bin16")
            {
                Format = Global.eFormat.Bin;
                Size = 1;
                return true;
            }
            else if (text == "bin32")
            {
                Format = Global.eFormat.Bin;
                Size = 2;
                return true;
            }
            else if (text == "float32")
            {
                Format = Global.eFormat.Float;
                Size = 2;
                return true;
            }
            else if (text == "ascii16")
            {
                Format = Global.eFormat.ASCII;
                Size = 1;
                return true;
            }
            else if (text == "ascii32")
            {
                Format = Global.eFormat.ASCII;
                Size = 2;
                return true;
            }

            Format = Global.eFormat.Int;
            Size = 0;
            return false;
        }

        private bool GetBitfieldTypestringToFormat(string text, out Global.eFormat Format)
        {
            text = text.ToLower();

            if (text == "int")
            {
                Format = Global.eFormat.Int;
                return true;
            }
            else if (text == "uint")
            {
                Format = Global.eFormat.UInt;
                return true;
            }
            else if (text == "hex")
            {
                Format = Global.eFormat.Hex;
                return true;
            }
            else if (text == "bin")
            {
                Format = Global.eFormat.Bin;
                return true;
            }
            else if (text == "ascii")
            {
                Format = Global.eFormat.ASCII;
                return true;
            }

            Format = Global.eFormat.Int;
            return false;
        }

        /// <summary>
        /// Determine last holding register user number, from first holding register user number
        /// </summary>
        static public int GetHoldingRegistersEnd(int holdingRegistersStart)
        {
            // 40000-49999, 40001-49999
            // 400000-465535, 400001-465536
            if (holdingRegistersStart >= 400000)
            {
                return holdingRegistersStart + 65535;
            }

            return 49999;
        }

        private TFilePosition GetFilePos(XElement xe)
        {
            TFilePosition result;
            result.Line = 0;
            result.Col = 0;

            if ((IXmlLineInfo)xe != null)
            {
                result.Line = ((IXmlLineInfo)xe).LineNumber;
                result.Col = ((IXmlLineInfo)xe).LinePosition;
            }

            return result;
        }

        /// <summary>
        /// Write connection parameters to XML document
        /// Create elements if they don't exist yet
        /// Remove or replace obsolete elements
        /// </summary>
        public void WriteConnection()
        {
            // Find Modbus node (case insensitive), or create it
            XElement xeModbus = GetSingleElement(XmlRootNode, "Modbus", eReq.Optional);
            if (xeModbus == null)
            {
                xeModbus = new XElement("modbus");
                XmlRootNode.Add(xeModbus);
            }

            // Find Connection node, or create it
            XElement xe = GetSingleElement(xeModbus, "Connection", eReq.Optional);
            if (xe == null)
            {
                xe = new XElement("connection");
                xeModbus.Add(xe);
            }
            xe.Value = ConnectionTypes[(int)Connection.ConnectionType];

            // Find Serial port node, or create it
            xe = GetSingleElement(xeModbus, "Port", eReq.Optional);
            if (xe == null)
            {
                xe = new XElement("port");
                xeModbus.Add(xe);
            }
            xe.Value = Connection.SerialPort;

            // Find Serial Baudrate node, or create it
            xe = GetSingleElement(xeModbus, "Baudrate", eReq.Optional);
            if (xe == null)
            {
                xe = new XElement("baudrate");
                xeModbus.Add(xe);
            }
            xe.Value = Connection.SerialBaudrate.ToString();

            // Find Serial Parity node, or create it
            xe = GetSingleElement(xeModbus, "Parity", eReq.Optional);
            if (xe == null)
            {
                xe = new XElement("parity");
                xeModbus.Add(xe);
            }
            xe.Value = Connection.SerialParity.ToString().ToLower();

            // Find Serial EndofmessageTimeout node, or create it
            xe = GetSingleElement(xeModbus, "EndofmessageTimeout", eReq.Optional);
            if (xe == null)
            {
                xe = new XElement("endofmessagetimeout");
                xeModbus.Add(xe);
            }
            xe.Value = Connection.SerialEndOfMessageTimeout.ToString();

            // Find Serial OneWireEchoSuppression node, or create it
            xe = GetSingleElement(xeModbus, "OnewireEchoSuppression", eReq.Optional);
            if (xe == null)
            {
                xe = new XElement("onewireechosuppression");
                xeModbus.Add(xe);
            }
            xe.Value = Connection.SerialOneWireEchoSuppression.ToString().ToLower();

            // Find IPAddress node, or create it
            xe = GetSingleElement(xeModbus, "IPaddress", eReq.Optional);
            if (xe == null)
            {
                xe = new XElement("ipaddress");
                xeModbus.Add(xe);
            }
            xe.Value = Connection.IPAddress.ToString();

            // Find IPPort node, or create it
            xe = GetSingleElement(xeModbus, "IPport", eReq.Optional);
            if (xe == null)
            {
                xe = new XElement("ipport");
                xeModbus.Add(xe);
            }
            xe.Value = Connection.IPPort.ToString();

            //TODO:  Global.eTcpConnectMode TcpConnectMode

            // Find SlaveID node, or create it
            xe = GetSingleElement(xeModbus, "SlaveID", eReq.Optional);
            if (xe == null)
            {
                xe = new XElement("slaveid");
                xeModbus.Add(xe);
            }
            xe.Value = Connection.SlaveID.ToString();

            // Find ResponseTimeout node, or create it
            xe = GetSingleElement(xeModbus, "ResponseTimeout", eReq.Optional);
            if (xe == null)
            {
                xe = new XElement("responsetimeout");
                xeModbus.Add(xe);
            }
            xe.Value = Connection.ResponseTimeout.ToString();

            // Find MaximumFramesize node, or create it
            xe = GetSingleElement(xeModbus, "MaximumFramesize", eReq.Optional);
            if (xe == null)
            {
                xe = new XElement("maximumframesize");
                xeModbus.Add(xe);
            }
            xe.Value = Connection.MaximumFrameSize.ToString();

            // Find interframedelay node, or create it
            xe = GetSingleElement(xeModbus, "interframedelay", eReq.Optional);
            if (xe == null)
            {
                xe = new XElement("interframedelay");
                xeModbus.Add(xe);
            }
            xe.Value = Connection.InterFrameDelay.ToString();

            // Find WriteHoldingregisterFC node, or create it
            xe = GetSingleElement(xeModbus, "WriteHoldingregisterFC", eReq.Optional);
            if (xe == null)
            {
                xe = new XElement("writeholdingregisterfc");
                xeModbus.Add(xe);
            }
            xe.Value = WriteHoldingregisterFCs[(int)Connection.WriteHoldingregFunctioncode];

            // Remove Value32BitEndian, obsolete
            xe = GetSingleElement(xeModbus, "Value32BitEndian", eReq.Optional);
            if (xe != null)
            {
                xe.Remove();
            }

            // Find int32endian node, or create it
            xe = GetSingleElement(xeModbus, "int32endian", eReq.Optional);
            if (xe == null)
            {
                xe = new XElement("int32endian");
                xeModbus.Add(xe);
            }
            xe.Value = Connection.Int32Endian.ToString().ToLower();

            // Find float32endian node, or create it
            xe = GetSingleElement(xeModbus, "float32endian", eReq.Optional);
            if (xe == null)
            {
                xe = new XElement("float32endian");
                xeModbus.Add(xe);
            }
            xe.Value = Connection.Float32Endian.ToString().ToLower();

            // Find ascii16endian node, or create it
            xe = GetSingleElement(xeModbus, "ascii16endian", eReq.Optional);
            if (xe == null)
            {
                xe = new XElement("ascii16endian");
                xeModbus.Add(xe);
            }
            xe.Value = Connection.Ascii16Endian.ToString().ToLower();

            // Remove ShowType, obsolete
            xe = GetSingleElement(xeModbus, "ShowType", eReq.Optional);
            if (xe != null)
            {
                xe.Remove();
            }

            // Remove ShowReadwrite, obsolete
            xe = GetSingleElement(xeModbus, "ShowReadwrite", eReq.Optional);
            if (xe != null)
            {
                xe.Remove();
            }
        }

        /// <summary>
        /// Write Modbus/TCP bridge settings to XML document
        /// Create elements if they don't exist yet
        /// </summary>
        public void WriteBridgeSettings()
        {
            // Find modbustcpserver node, or create it
            XElement xeBridge = GetSingleElement(XmlRootNode, "modbustcpserver", eReq.Optional);
            if (xeBridge == null)
            {
                xeBridge = new XElement("modbustcpserver");
                XmlRootNode.Add(xeBridge);
            }

            // Find Enable node, or create it
            XElement xe = GetSingleElement(xeBridge, "enable", eReq.Optional);
            if (xe == null)
            {
                xe = new XElement("enable");
                xeBridge.Add(xe);
            }
            xe.Value = ModbusTcpBridge.Enable.ToString().ToLower();

            // Find Port node, or create it
            xe = GetSingleElement(xeBridge, "port", eReq.Optional);
            if (xe == null)
            {
                xe = new XElement("port");
                xeBridge.Add(xe);
            }
            xe.Value = ModbusTcpBridge.IPPort.ToString();
        }

        /// <summary>
        /// Write other settings to XML document, like samplerate, showinfo
        /// Create XML nodes if they don't exist yet
        /// </summary>
        public void WriteOtherSettings()
        {
            // Find Modbus node (case insensitive), or create it
            XElement xeBridge = GetSingleElement(XmlRootNode, "Modbus", eReq.Optional);
            if (xeBridge == null)
            {
                xeBridge = new XElement("modbus");
                XmlRootNode.Add(xeBridge);
            }

            // Find showinfo node, or create it
            XElement xe = GetSingleElement(xeBridge, "ShowInfo", eReq.Optional);
            if (xe == null)
            {
                xe = new XElement("showinfo");
                xeBridge.Add(xe);
            }
            xe.Value = Options.ShowInfo.ToString().ToLower();

            // Find Samplerate node, or create it
            xe = GetSingleElement(xeBridge, "SampleRate", eReq.Optional);
            if (xe == null)
            {
                xe = new XElement("samplerate");
                xeBridge.Add(xe);
            }
            xe.Value = Options.Samplerate.ToString();
        }

        /// <summary>
        /// Write form size to XML document
        /// Create XML nodes if they don't exist yet
        /// </summary>
        public void WriteFormSize()
        {
            // Find Size node, or create it
            XElement xeSize = GetSingleElement(XmlRootNode, "Size", eReq.Optional);
            if (xeSize == null)
            {
                xeSize = new XElement("size");
                XmlRootNode.Add(xeSize);
            }

            // Find Width node, or create it
            XElement widthNode = GetSingleElement(xeSize, "Width", eReq.Optional);
            if (widthNode == null)
            {
                widthNode = new XElement("width");
                xeSize.Add(widthNode);
            }
            // write value
            // value in mbc file is standarized to 96DPI
            widthNode.Value = Global.ScaleXcoordFromSystemDpiTo96Dpi(FormSize.Width).ToString();

            // Find Height node, or create it
            XElement heightNode = GetSingleElement(xeSize, "Height", eReq.Optional);
            if (heightNode == null)
            {
                heightNode = new XElement("height");
                xeSize.Add(heightNode);
            }
            // write value
            // value in mbc file is standarized to 96DPI
            heightNode.Value = Global.ScaleYcoordFromSystemDpiTo96Dpi(FormSize.Height).ToString();
        }

        /// <summary>
        /// Update to current version: convert all XML tag names to lowercase, remove/replace obsolete tags
        /// </summary>
        public void UpdateXML()
        {
            //DumpXMLTree();

            // convert all XML tag names to lowercase
            foreach (XElement xe1 in XmlDoc.Elements())
            {
                ConvertAlltagsToLowercaseRec(xe1);
            }

            // Got a modbus element?
            XElement xeModbus = GetSingleElement(XmlRootNode, "Modbus", eReq.Optional);
            if (xeModbus != null)
            {
                // Remove Value32BitEndian, obsolete
                XElement xe = GetSingleElement(xeModbus, "Value32BitEndian", eReq.Optional);
                if (xe != null)
                {
                    xe.Remove();
                }

                // Find int32endian node, or create it
                xe = GetSingleElement(xeModbus, "int32endian", eReq.Optional);
                if (xe == null)
                {
                    xe = new XElement("int32endian");
                    xeModbus.Add(xe);
                    xe.Value = Connection.Int32Endian.ToString().ToLower();
                }

                // Find float32endian node, or create it
                xe = GetSingleElement(xeModbus, "float32endian", eReq.Optional);
                if (xe == null)
                {
                    xe = new XElement("float32endian");
                    xeModbus.Add(xe);
                    xe.Value = Connection.Float32Endian.ToString().ToLower();
                }

                // Find ascii16endian node, or create it
                xe = GetSingleElement(xeModbus, "ascii16endian", eReq.Optional);
                if (xe == null)
                {
                    xe = new XElement("ascii16endian");
                    xeModbus.Add(xe);
                    xe.Value = Connection.Ascii16Endian.ToString().ToLower();
                }

                // Remove ShowType
                xe = GetSingleElement(xeModbus, "ShowType", eReq.Optional);
                if (xe != null)
                {
                    xe.Remove();
                }

                // Remove ShowReadwrite
                xe = GetSingleElement(xeModbus, "ShowReadwrite", eReq.Optional);
                if (xe != null)
                {
                    xe.Remove();
                }
            }

            // convert elements with keywords to lower case
            foreach (XElement xe1 in XmlDoc.Elements())
            {
                ConvertAllEnumsToLowercaseRec(xe1);
            }

            //DumpXMLTree();
        }

        private void ConvertAlltagsToLowercaseRec(XElement xel)
        {
            xel.Name = xel.Name.ToString().ToLower();
            foreach (XElement xel2 in xel.Elements())
            {
                ConvertAlltagsToLowercaseRec(xel2);
            }
        }

        private void ConvertAllEnumsToLowercaseRec(XElement xel)
        {
            if(TagsValueToLowerList.Contains(xel.Name.ToString()))
            {
                xel.Value = xel.Value.ToString().ToLower().Trim();
            }

            foreach (XElement xel2 in xel.Elements())
            {
                ConvertAllEnumsToLowercaseRec(xel2);
            }
        }

        /// <summary>
        /// Save configuration to file
        /// </summary>
        public void SaveFile()
        {
            /* 
            //default XDocument save, saves with BOM
            XmlDoc.Save(Path);
            */

            // save without BOM
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Encoding = new UTF8Encoding(false); // The false means, do not emit the BOM.
            /* Indenting werkt niet goed
            settings.Indent = true;
            settings.IndentChars = ("\t");
            */
            using (XmlWriter w = XmlWriter.Create(Path, settings))
            {
                XmlDoc.Save(w);
            }
        }

        /// <summary>
        /// Dump the project's XML tree
        /// </summary>
        public void DumpXMLTree()
        {
            List<string> TL = new List<string>();
            int level = 0;

            foreach (XElement xel in XmlDoc.Elements())
            {
                DumpXMLElementRec(xel, level, TL);
            }
        }

        private void DumpXMLElementRec(XElement xel, int level, List<string> TL)
        {
            string s;

            s = new String(' ', level) + xel.Name.ToString();
            s += ", " + xel.NodeType.ToString();
            s += ", " + xel.HasElements.ToString();
            s += ", " + xel.Value.ToString();
            TL.Add(s);

            foreach (XElement xel2 in xel.Elements())
            {
                DumpXMLElementRec(xel2, level+1, TL);
            }
        }
    }
}
