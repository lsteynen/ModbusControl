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
using System.Diagnostics;

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
        private string[] WriteHoldingregisterFCs = new string[] { "6", "16", "auto" };

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
            public int SerialNrOfBits;
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
            public int Samplerate;
            //public bool ShowInfo;
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
            public uint ExportOrder;        // initialized with uint.max if not given
            public List<TVar> VarList;      // List with vars in this tab page
        }

        public struct TVar
        {
            public UInt32 Regnr;
            public string Description;
            public string Longdescr;         // tooltip text
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
            public Global.CEnum EnumDict;      // enum convert dictionary
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
            public Global.CEnum EnumDict;      // enum convert dictionary
            public string Longdescr;         // tooltip text
        }

        // structure holding a line and column position in the XML file
        public struct TFilePosition
        {
            public int Line;
            public int Col;
        }

        // list of XML tags to convert to lower case
        List<string> TagsToConvertValueToLowerList = new List<string> { "connection", "parity", "writeholdingregisterfc",
                                        "int32endian", "float32endian", "ascii16endian", "onewireechosuppression",
                                        "showinfo", "enable", "rw", "expandfields", "type"};

        // Properties

        public string ValidationSchemaPath; // path to validation XSD file
        public TSize FormSize; // Form size in 96DPI
        public TConnection Connection; // Modbus connection settings
        public TOptions Options; // Options settings
        public TModbusTcpBridge ModbusTcpBridge; // Modbus/TCP bridge settings
        public List<TTab> TabsList; // List with all tab pages
        public bool HasLongdescrs; // true if project has variables with longdescr defined
        public bool LoadModbusOnly; //true to only load the <modbus> connection settings

        // private data

        private string Path; // path to loaded mbc project file
        private List<string> fileList;  // list of main mbc file and nested include files
        private XDocument XmlDoc; // doc holding the current xml mbc project file, for possibility to save
        private XElement XmlRootNode; // root node of the document
        private delegate void XMLIterateAllNodesCallback(XElement xe);
        private XElement autostartPageXElement = null;


        // Constructor
        public CProject()
        {
            // Set defaults
            FormSize.WidthSeen = false;
            FormSize.HeightSeen = false;
            Connection.ConnectionType = Global.eConnectionType.SerialRTU;
            Connection.SerialPort = "COM1";
            Connection.SerialBaudrate = 9600;
            Connection.SerialNrOfBits = 8;
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
            Options.Samplerate = 1000;
            //Options.ShowInfo = true;
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
            bool modbusSeen = false;

            // store path of project file
            Path = path;
            // add to nested files list
            fileList = new List<string>();
            fileList.Add(path);

            // Load xml document into XmlDoc
            // A previous document will be discarded when a new is loaded
            //XmlDoc = XDocument.Load(path, LoadOptions.PreserveWhitespace | LoadOptions.SetLineInfo);
            XmlDoc = XDocument.Load(path, LoadOptions.SetLineInfo);

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

            // check root element
            XmlRootNode = XmlDoc.Root;
            string rootElementName = XmlRootNode.Name.LocalName;
            string expectRootName = "modbuscontrol";
            if (rootElementName != expectRootName)
            {
                throw new XmlException("XML root element <" + expectRootName + "> not found. A <" + expectRootName + "> root element is required in an mbc project file.");
            }

            // iterate elements under root
            foreach (XElement xe in XmlRootNode.Elements())
            {
                string elemName = xe.Name.LocalName;
                Debug.Print("ROOT: " + elemName);

                // <size> element, optional
                if (CheckElementNameMatch(elemName, "Size"))
                {
                    if (LoadModbusOnly == false)
                    {
                        ProcessSizeElement(xe);
                    }
                }
                else
                // <modbus> element, required
                if (CheckElementNameMatch(elemName, "Modbus"))
                {
                    ProcessModbusElement(xe);
                    modbusSeen = true;
                }
                else
                // <modbustcpserver> element, optional
                if (CheckElementNameMatch(elemName, "modbustcpserver"))
                {
                    if (LoadModbusOnly == false)
                    {
                        ProcessModbustcpserverElement(xe);
                    }
                }
                else
                // <autostart> element, optional
                if (CheckElementNameMatch(elemName, "autostart"))
                {
                    if (LoadModbusOnly == false)
                    {
                        // actual processing is with Options.AutostartEnable furtheron
                        ProcessAutostartElement(xe);
                    }
                }
                else
                // <tab> elements, optional, multiple
                if (CheckElementNameMatch(elemName, "tab"))
                {
                    if (LoadModbusOnly == false)
                    {
                        ProcessTabElement(xe);
                    }
                }
                else
                // <include> elements, optional, multiple
                if (CheckElementNameMatch(elemName, "include"))
                {
                    if (LoadModbusOnly == false)
                    {
                        ProcessIncludeElement(xe);
                    }
                }
                else
                {
                    // invalid/unsupported element found
                }
            }

            if (modbusSeen == false)
            {
                string elName = "modbus";
                TFilePosition fp = GetFilePos(XmlRootNode);
                throw new XmlException("XML element <" + elName + "> not found. A <" + elName + "> element is required." + Environment.NewLine, null, fp.Line, fp.Col);
            }

            // Got the enable and the page, now validate the page name/number
            if ((LoadModbusOnly == false) && Options.AutostartEnable && (string.IsNullOrEmpty(Options.AutostartTabpage) == false))
            {
                if (TabPageIsValid(Options.AutostartTabpage) == false)
                {
                    Options.AutostartTabpage = null;
                    TFilePosition fp = GetFilePos(autostartPageXElement);
                    throw new XmlException("XML element <page> is not a valid tabpage name, or a valid tabpage number 1-N." + Environment.NewLine + LimitXmlTextLength(autostartPageXElement.ToString()) + Environment.NewLine, null, fp.Line, fp.Col);
                }
            }
        }

        static void ValidationEventHandler(object sender, ValidationEventArgs e)
        {
            // throw exception on validation error
            string Msg = e.Message + Environment.NewLine
                    + "Line " + e.Exception.LineNumber.ToString() + ", column " + e.Exception.LinePosition.ToString() + ".";

            throw new XmlSchemaException(Msg);
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
        /// Process a <Size> element
        /// The width and height are in pixels at 96dpi. For compatiblity of the mbc file
        /// between standard 96-dpi systems and high-dpi systems.
        /// Throws exception on error
        /// </summary>
        /// <param name="pxe">The parent XML element</param>
        /// <return>true if one of the elements was seen</return>
        private bool ProcessSizeElement(XElement pxe)
        {
            bool Seen = false;

            foreach (XElement xe in pxe.Elements())
            {
                string elemName = xe.Name.LocalName;
                Debug.Print("<Size> " + elemName);

                // Width, optional
                if (CheckElementNameMatch(elemName, "Width"))
                {
                    string TmpString = xe.Value.Trim();
                    UInt32 TmpValue;
                    if (UInt32.TryParse(TmpString, out TmpValue) && (TmpValue <= int.MaxValue))
                    {
                        // keep a minimum width
                        if (TmpValue < 100) TmpValue = 100;
                        // value in mbc file is standarized to 96DPI
                        FormSize.WidthSeen = true;
                        Seen = true;
                        FormSize.Width = (int)TmpValue;
                    }
                    else
                    {
                        TFilePosition fp = GetFilePos(xe);
                        throw new XmlException("XML element <" + xe.Name.LocalName + "> has an invalid value. Valid values are 0 - " + int.MaxValue + "." + Environment.NewLine + LimitXmlTextLength(xe.ToString()) + Environment.NewLine, null, fp.Line, fp.Col);
                    }
                }
                else
                // Height, optional
                if (CheckElementNameMatch(elemName, "Height"))
                {
                    string TmpString = xe.Value.Trim();
                    UInt32 TmpValue;
                    if (UInt32.TryParse(TmpString, out TmpValue) && (TmpValue <= int.MaxValue))
                    {
                        // keep a minimum height
                        if (TmpValue < 100) TmpValue = 100;
                        // value in mbc file is standarized to 96DPI
                        FormSize.HeightSeen = true;
                        Seen = true;
                        FormSize.Height = (int)TmpValue;
                    }
                    else
                    {
                        TFilePosition fp = GetFilePos(xe);
                        throw new XmlException("XML element <" + xe.Name.LocalName + "> has an invalid value. Valid values are 0 - " + int.MaxValue + "." + Environment.NewLine + LimitXmlTextLength(xe.ToString()) + Environment.NewLine, null, fp.Line, fp.Col);
                    }
                }
                else
                {
                    // invalid/unsupported element found
                }
            }

            return Seen;
        }

        /// <summary>
        /// Process a <Modbus> element
        /// Throws exception on error
        /// </summary>
        /// <param name="pxe">The parent XML element</param>
        /// <return>true if one of the elements was seen</return>
        private bool ProcessModbusElement(XElement pxe)
        {
            bool Seen = false;

            foreach (XElement xe in pxe.Elements())
            {
                string elemName = xe.Name.LocalName;
                Debug.Print("<Modbus> " + elemName);

                // Connection, optional
                if (CheckElementNameMatch(elemName, "Connection"))
                {
                    Seen = true;
                    // accept a few strings
                    string TmpString = xe.Value.Trim();
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
                        TFilePosition fp = GetFilePos(xe);
                        throw new XmlException("XML element <" + xe.Name.LocalName + "> has an invalid value. Valid values are 'serial/rtu', 'modbus/tcp' or 'rtu/tcp'." + Environment.NewLine + LimitXmlTextLength(xe.ToString()) + Environment.NewLine, null, fp.Line, fp.Col);
                    }
                }
                else
                // serial Port name, optional
                if (CheckElementNameMatch(elemName, "Port"))
                {
                    Seen = true;
                    string TmpString = xe.Value.Trim();
                    if (string.IsNullOrEmpty(TmpString) == false)
                    {
                        Connection.SerialPort = TmpString;
                    }
                }
                else
                // serial Baudrate, optional
                // >= 50
                if (CheckElementNameMatch(elemName, "Baudrate"))
                {
                    Seen = true;
                    string TmpString = xe.Value.Trim();
                    UInt32 TmpValue;
                    if ((UInt32.TryParse(TmpString, out TmpValue) && (TmpValue <= Int32.MaxValue))
                        && (TmpValue >= 50))
                    {
                        Connection.SerialBaudrate = (int)TmpValue;
                    }
                    else
                    {
                        TFilePosition fp = GetFilePos(xe);
                        throw new XmlException("XML element <" + xe.Name.LocalName + "> has an invalid value. Valid values are 50 - " + Int32.MaxValue.ToString() + "." + Environment.NewLine + LimitXmlTextLength(xe.ToString()) + Environment.NewLine, null, fp.Line, fp.Col);
                    }
                }
                else
                // serial Parity, optional
                if (CheckElementNameMatch(elemName, "Parity"))
                {
                    Seen = true;
                    string TmpString = xe.Value.Trim();
                    Parity TmpParity;
                    if (Enum.TryParse<Parity>(TmpString, true, out TmpParity))
                    {
                        Connection.SerialParity = TmpParity;
                    }
                    else
                    {
                        TFilePosition fp = GetFilePos(xe);
                        throw new XmlException("XML element <" + xe.Name.LocalName + "> has an invalid value. Valid values are 'None', 'Even', 'Odd', 'Mark' or 'Space'." + Environment.NewLine + LimitXmlTextLength(xe.ToString()) + Environment.NewLine, null, fp.Line, fp.Col);
                    }
                }
                else
                // serial EndofmessageTimeout
                if (CheckElementNameMatch(elemName, "EndofmessageTimeout"))
                {
                    Seen = true;
                    string TmpString = xe.Value.Trim();
                    // get value, limit range 1 - 2147483647
                    UInt32 TmpValue;
                    if (uint.TryParse(TmpString, out TmpValue) && (TmpValue >= 1) && (TmpValue <= 2147483647))
                    {
                        Connection.SerialEndOfMessageTimeout = (int)TmpValue;
                    }
                    else
                    {
                        TFilePosition fp = GetFilePos(xe);
                        throw new XmlException("XML element <" + xe.Name.LocalName + "> has an invalid value. Valid values are 1 - 2147483647." + Environment.NewLine + LimitXmlTextLength(xe.ToString()) + Environment.NewLine, null, fp.Line, fp.Col);
                    }
                }
                else
                // serial One-wire echo suppression
                if (CheckElementNameMatch(elemName, "OnewireEchoSuppression"))
                {
                    Seen = true;
                    // accept true and false
                    string TmpString = xe.Value.Trim();
                    bool TmpBool;
                    if (TryParseBool(TmpString, out TmpBool))
                    {
                        Connection.SerialOneWireEchoSuppression = TmpBool;
                    }
                    else
                    {
                        TFilePosition fp = GetFilePos(xe);
                        throw new XmlException("XML element <" + xe.Name.LocalName + "> has an invalid value. Valid values are 'true' or 'false'." + Environment.NewLine + LimitXmlTextLength(xe.ToString()) + Environment.NewLine, null, fp.Line, fp.Col);
                    }
                }
                else
                // IPaddress, optional, must be blank or valid
                if (CheckElementNameMatch(elemName, "IPaddress"))
                {
                    Seen = true;
                    string TmpString = xe.Value.Trim();
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
                        TFilePosition fp = GetFilePos(xe);
                        throw new XmlException("XML element <" + xe.Name.LocalName + "> has an invalid value. Valid values are empty or a valid IPv4 address like '192.168.1.123' or '127.0.0.1'." + Environment.NewLine + LimitXmlTextLength(xe.ToString()) + Environment.NewLine, null, fp.Line, fp.Col);
                    }
                }
                else
                // IPport
                if (CheckElementNameMatch(elemName, "IPport"))
                {
                    Seen = true;
                    string TmpString = xe.Value.Trim();
                    // get value, limit range 1 - 65535
                    UInt16 TmpValue;
                    if (UInt16.TryParse(TmpString, out TmpValue) && (TmpValue >= 1) && (TmpValue <= 65535))
                    {
                        Connection.IPPort = TmpValue;
                    }
                    else
                    {
                        TFilePosition fp = GetFilePos(xe);
                        throw new XmlException("XML element <" + xe.Name.LocalName + "> has an invalid value. Valid values are 1 - 65535." + Environment.NewLine + LimitXmlTextLength(xe.ToString()) + Environment.NewLine, null, fp.Line, fp.Col);
                    }
                }
                else
                // SlaveID
                if (CheckElementNameMatch(elemName, "SlaveID"))
                {
                    Seen = true;
                    string TmpString = xe.Value.Trim();
                    // get value, limit range 0 - 255
                    byte TmpValue;
                    if (byte.TryParse(TmpString, out TmpValue))
                    {
                        Connection.SlaveID = TmpValue;
                    }
                    else
                    {
                        TFilePosition fp = GetFilePos(xe);
                        throw new XmlException("XML element <" + xe.Name.LocalName + "> has an invalid value. Valid values are 0 - 255." + Environment.NewLine + LimitXmlTextLength(xe.ToString()) + Environment.NewLine, null, fp.Line, fp.Col);
                    }
                }
                else
                // ResponseTimeout
                if (CheckElementNameMatch(elemName, "ResponseTimeout"))
                {
                    Seen = true;
                    string TmpString = xe.Value.Trim();
                    // get value, limit range 1 - 2147483647
                    UInt32 TmpValue;
                    if (UInt32.TryParse(TmpString, out TmpValue) && (TmpValue >= 1) && (TmpValue <= 2147483647))
                    {
                        Connection.ResponseTimeout = (int)TmpValue;
                    }
                    else
                    {
                        TFilePosition fp = GetFilePos(xe);
                        throw new XmlException("XML element <" + xe.Name.LocalName + "> has an invalid value. Valid values are 1 - 2147483647." + Environment.NewLine + LimitXmlTextLength(xe.ToString()) + Environment.NewLine, null, fp.Line, fp.Col);
                    }
                }
                else
                // MaximumFramesize
                if (CheckElementNameMatch(elemName, "MaximumFramesize"))
                {
                    Seen = true;
                    string TmpString = xe.Value.Trim();
                    // get value, limit range 13 - 256
                    UInt32 TmpValue;
                    if (UInt32.TryParse(TmpString, out TmpValue) && (TmpValue >= 13) && (TmpValue <= 256))
                    {
                        Connection.MaximumFrameSize = (int)TmpValue;
                    }
                    else
                    {
                        TFilePosition fp = GetFilePos(xe);
                        throw new XmlException("XML element <" + xe.Name.LocalName + "> has an invalid value. Valid values are 13 - 256." + Environment.NewLine + LimitXmlTextLength(xe.ToString()) + Environment.NewLine, null, fp.Line, fp.Col);
                    }
                }
                else
                // Inter-frame delay
                if (CheckElementNameMatch(elemName, "interframedelay"))
                {
                    Seen = true;
                    string TmpString = xe.Value.Trim();
                    // get value
                    UInt32 TmpValue;
                    if (UInt32.TryParse(TmpString, out TmpValue))
                    {
                        Connection.InterFrameDelay = (int)TmpValue;
                    }
                    else
                    {
                        TFilePosition fp = GetFilePos(xe);
                        throw new XmlException("XML element <" + xe.Name.LocalName + "> has an invalid value. Use 0 for default." + Environment.NewLine + LimitXmlTextLength(xe.ToString()) + Environment.NewLine, null, fp.Line, fp.Col);
                    }
                }
                else
                // WriteHoldingregisterFC, optional
                if (CheckElementNameMatch(elemName, "WriteHoldingregisterFC"))
                {
                    Seen = true;
                    string TmpString = xe.Value.Trim();
                    byte TmpValue;
                    // get value, must be 6, 16 or 'auto'
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
                    // accept obsolete 'alternate' as 'auto'
                    else if ((string.Compare(TmpString, "auto", StringComparison.OrdinalIgnoreCase) == 0)
                          || (string.Compare(TmpString, "alternate", StringComparison.OrdinalIgnoreCase) == 0))
                    {
                        Connection.WriteHoldingregFunctioncode = Global.eWriteHoldingregFunctionCode.Auto;
                    }
                    else
                    {
                        TFilePosition fp = GetFilePos(xe);
                        throw new XmlException("XML element <" + xe.Name.LocalName + "> has an invalid value. Valid values are '6' (or '06'), '16' or 'auto'." + Environment.NewLine + LimitXmlTextLength(xe.ToString()) + Environment.NewLine, null, fp.Line, fp.Col);
                    }
                }
                else
                // int32endian
                // if not found, try the old Value32BitEndian for compatibility
                if (CheckElementNameMatch(elemName, "int32endian")
                    || CheckElementNameMatch(elemName, "Value32BitEndian"))
                {
                    Seen = true;
                    string TmpString = xe.Value.Trim();
                    Global.eEndian TmpEndian;
                    if (TryParseEndian(TmpString, out TmpEndian))
                    {
                        Connection.Int32Endian = TmpEndian;
                    }
                    else
                    {
                        TFilePosition fp = GetFilePos(xe);
                        throw new XmlException("XML element <" + xe.Name.LocalName + "> has an invalid value. Valid values are 'little' or 'big'." + Environment.NewLine + LimitXmlTextLength(xe.ToString()) + Environment.NewLine, null, fp.Line, fp.Col);
                    }
                }
                else
                // float32Endian
                if (CheckElementNameMatch(elemName, "float32endian"))
                {
                    Seen = true;
                    string TmpString = xe.Value.Trim();
                    Global.eEndian TmpEndian;
                    if (TryParseEndian(TmpString, out TmpEndian))
                    {
                        Connection.Float32Endian = TmpEndian;
                    }
                    else
                    {
                        TFilePosition fp = GetFilePos(xe);
                        throw new XmlException("XML element <" + xe.Name.LocalName + "> has an invalid value. Valid values are 'little' or 'big'." + Environment.NewLine + LimitXmlTextLength(xe.ToString()) + Environment.NewLine, null, fp.Line, fp.Col);
                    }
                }
                else
                // ascii16endian
                if (CheckElementNameMatch(elemName, "ascii16endian"))
                {
                    Seen = true;
                    string TmpString = xe.Value.Trim();
                    Global.eEndian TmpEndian;
                    if (TryParseEndian(TmpString, out TmpEndian))
                    {
                        Connection.Ascii16Endian = TmpEndian;
                    }
                    else
                    {
                        TFilePosition fp = GetFilePos(xe);
                        throw new XmlException("XML element <" + xe.Name.LocalName + "> has an invalid value. Valid values are 'little' or 'big'." + Environment.NewLine + LimitXmlTextLength(xe.ToString()) + Environment.NewLine, null, fp.Line, fp.Col);
                    }
                }
                else
                // HoldingregistersStart
                if (CheckElementNameMatch(elemName, "HoldingregisterStart"))
                {
                    Seen = true;
                    string TmpString = xe.Value.Trim();
                    UInt32 TmpNum;
                    // allow 40.000, 40.001, 400.000 and 400.001 (dots only here for clearness)
                    // use 4xxxx for holding registers 
                    // use 40001 for traditional numbering 
                    // use 400001 for 6-digit numbering when using > 9999 holding registers
                    // use 40000 or 400000 for numbering that actually starts from 0
                    if (UInt32.TryParse(TmpString, out TmpNum)
                        && ((TmpNum == 40000) || (TmpNum == 40001) || (TmpNum == 400000) || (TmpNum == 400001)))
                    {
                        Options.HoldingregistersStart = (int)TmpNum;
                    }
                    else
                    {
                        TFilePosition fp = GetFilePos(xe);
                        throw new XmlException("XML element <" + xe.Name.LocalName + "> has an invalid value. Valid values are 40000, 40001, 400000 or 400001. Use 40001 for traditional numbering. Use 400000 or 400001 (6-digit numbering) to access more than 9999 registers. Use 40000 or 400000 if you want to number the holding registers from 0." + Environment.NewLine + LimitXmlTextLength(xe.ToString()) + Environment.NewLine, null, fp.Line, fp.Col);
                    }
                }
                else
                // Samplerate
                if (CheckElementNameMatch(elemName, "SampleRate"))
                {
                    Seen = true;
                    string TmpString = xe.Value.Trim();
                    // get value, limit range 0 - 2147483647
                    UInt32 TmpValue;
                    if (UInt32.TryParse(TmpString, out TmpValue) && (TmpValue <= 2147483647))
                    {
                        Options.Samplerate = (int)TmpValue;
                    }
                    else
                    {
                        TFilePosition fp = GetFilePos(xe);
                        throw new XmlException("XML element <" + xe.Name.LocalName + "> has an invalid value. Valid values are 0 - 2147483647." + Environment.NewLine + LimitXmlTextLength(xe.ToString()) + Environment.NewLine, null, fp.Line, fp.Col);
                    }
                }
                else
                {
                    // invalid/unsupported element found
                }
            }

            return Seen;
        }


        /// <summary>
        /// Process a <tab> element
        /// Throws exception on error
        /// </summary>
        /// <param name="pxe">The parent XML element</param>
        private void ProcessTabElement(XElement pxe)
        {
            bool nameSeen = false;

            // create new tab
            TTab newTab = new TTab();
            // init with default values
            newTab.Name = string.Empty;
            newTab.ExportOrder = int.MaxValue;
            newTab.VarList = new List<TVar>();

            // Must have seen a <reg> in a tab for enabling auto-numbering
            UInt32 NextAutoRegnr = 0;

            // Create a TVar with the defaults for each var
            // in <tab> we can set defaults for all var's
            TVar DefaultVar = new TVar();
            DefaultVar.Regnr = 0;
            DefaultVar.Description = String.Empty;
            DefaultVar.Longdescr = null;
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

            // process child elements
            foreach (XElement xe in pxe.Elements())
            {
                string elemName = xe.Name.LocalName;
                Debug.Print("<Tab> " + elemName);

                // <name>, required
                if (CheckElementNameMatch(elemName, "name"))
                {
                    newTab.Name = xe.Value.Trim();
                    nameSeen = true;
                }
                else
                // <exportorder>, optional
                if (CheckElementNameMatch(elemName, "exportorder"))
                {
                    string tmpString = xe.Value.Trim();
                    uint tmpOrder;
                    if (uint.TryParse(tmpString, out tmpOrder))
                    {
                        newTab.ExportOrder = tmpOrder;
                    }
                    else
                    {
                        TFilePosition fp = GetFilePos(xe);
                        throw new XmlException("XML element <exportorder> has an invalid value. Valid values are 0-4294967295" + Environment.NewLine + LimitXmlTextLength(xe.ToString()) + Environment.NewLine, null, fp.Line, fp.Col);
                    }
                }
                else// <rw>, default for <var>, optional
                if (CheckElementNameMatch(elemName, "rw"))
                {
                    if (GetReadWriteFlags(xe.Value.Trim(), out DefaultVar.isReadable, out DefaultVar.isWriteable) == false)
                    {
                        TFilePosition fp = GetFilePos(xe);
                        throw new XmlException("XML element <rw> has an invalid value. Valid values are 'r', 'rw', wr', or 'w'." + Environment.NewLine + LimitXmlTextLength(xe.ToString()) + Environment.NewLine, null, fp.Line, fp.Col);
                    }
                }
                else
                // <minlen> minimumlength, default for <var>, optional
                if (CheckElementNameMatch(elemName, "minlen"))
                {
                    string TmpString = xe.Value.Trim();
                    int TmpMinlen;
                    if (int.TryParse(TmpString, out TmpMinlen)
                        && ((TmpMinlen >= 1) && (TmpMinlen <= 32)))
                    {
                        DefaultVar.MinLen = TmpMinlen;
                    }
                    else
                    {
                        TFilePosition fp = GetFilePos(xe);
                        throw new XmlException("XML element <minlen> has an invalid value. Valid values are 1 - 32." + Environment.NewLine + LimitXmlTextLength(xe.ToString()) + Environment.NewLine, null, fp.Line, fp.Col);
                    }
                }
                else
                // <decimals>, default for <var>, optional
                if (CheckElementNameMatch(elemName, "decimals"))
                {
                    string TmpString = xe.Value.Trim();
                    int TmpDecimals;
                    if (int.TryParse(TmpString, out TmpDecimals)
                        && ((TmpDecimals >= 0) && (TmpDecimals <= 12)))
                    {
                        // ok
                        DefaultVar.Decimals = TmpDecimals;
                    }
                    else
                    {
                        TFilePosition fp = GetFilePos(xe);
                        throw new XmlException("XML element <decimals> has an invalid value. Valid values are 0 - 12." + Environment.NewLine + LimitXmlTextLength(xe.ToString()) + Environment.NewLine, null, fp.Line, fp.Col);
                    }
                }
                else
                // <fcol> foregroundcolor, default for <var>, optional
                if (CheckElementNameMatch(elemName, "fcol"))
                {
                    try
                    {
                        DefaultVar.ForeColor = ColorFromString.ToColor(xe.Value.Trim());
                    }
                    catch
                    {
                        TFilePosition fp = GetFilePos(xe);
                        throw new XmlException("XML element <fcol> has an invalid value. Valid values are RGB values 0-255 like '255,0,0'; HTML style hex colors #RRGGBB like #FF0000, or a color name like 'Red'." + Environment.NewLine + LimitXmlTextLength(xe.ToString()) + Environment.NewLine, null, fp.Line, fp.Col);
                    }
                }
                else
                // <bcol> backgroundcolor, default for <var>, optional
                if (CheckElementNameMatch(elemName, "bcol"))
                {
                    try
                    {
                        DefaultVar.BackColor = ColorFromString.ToColor(xe.Value.Trim());
                    }
                    catch
                    {
                        TFilePosition fp = GetFilePos(xe);
                        throw new XmlException("XML element <bcol> has an invalid value. Valid values are RGB values 0-255 like '255,0,0'; HTML style hex colors #RRGGBB like #FF0000, or a color name like 'Red'." + Environment.NewLine + LimitXmlTextLength(xe.ToString()) + Environment.NewLine, null, fp.Line, fp.Col);
                    }
                }
                else
                // <expandfields>, default for <var>, optional
                if (CheckElementNameMatch(elemName, "expandfields"))
                {
                    string TmpString = xe.Value.Trim();
                    bool TmpBool;
                    if (TryParseBool(TmpString, out TmpBool))
                    {
                        DefaultVar.Expand = TmpBool;
                    }
                    else
                    {
                        TFilePosition fp = GetFilePos(xe);
                        throw new XmlException("XML element <expandfields> has an invalid value. Valid values are 'true' or 'false'." + Environment.NewLine + LimitXmlTextLength(xe.ToString()) + Environment.NewLine, null, fp.Line, fp.Col);
                    }
                }
                else
                // <var> elements, optional, multiple
                if (CheckElementNameMatch(elemName, "var"))
                {
                    // make new var, and init with the defaults, support autonumbering
                    TVar newVar = ProcessVarElement(xe, NextAutoRegnr, DefaultVar);

                    // Add new var to the tab list
                    newTab.VarList.Add(newVar);
                    // update next auto register numbering
                    NextAutoRegnr = (UInt32)(newVar.Regnr + newVar.Size);
                }
                else
                {
                    // invalid/unsupported element found
                }
            }

            // <name> is required
            if (nameSeen == false)
            {
                string elName = "name";
                TFilePosition fp = GetFilePos(pxe);
                throw new XmlException("XML element <" + elName + "> not found. A <" + elName + "> element is required in a <" + pxe.Name.LocalName + "> element." + Environment.NewLine, null, fp.Line, fp.Col);
            }

            TabsList.Add(newTab);
        }

        /// <summary>
        /// Process a <var> element
        /// Throws exception on error
        /// </summary>
        /// <param name="pxe">The parent XML element</param>
        private TVar ProcessVarElement(XElement pxe, uint nextAutoRegnr, TVar defaultVar)
        {
            TVar newVar = defaultVar;

            // auto count capability for position
            int autoFieldPos = 0;

            // reg is required
            bool regSeen = false;

            // process child elements
            foreach (XElement xe in pxe.Elements())
            {
                string elemName = xe.Name.LocalName;
                Debug.Print("<Var> " + elemName);

                // <reg>, required
                if (CheckElementNameMatch(elemName, "reg"))
                {
                    string regString = xe.Value.Trim();
                    // auto numbering?
                    if (regString == "*")
                    {
                        // auto numbering
                        // initialized?
                        if (nextAutoRegnr > 0)
                        {
                            newVar.Regnr = nextAutoRegnr;
                        }
                        else
                        {
                            TFilePosition fp = GetFilePos(xe);
                            throw new XmlException("Must have seen a valid register number before auto-numbering can be used." + Environment.NewLine + LimitXmlTextLength(xe.ToString()) + Environment.NewLine, null, fp.Line, fp.Col);
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
                            TFilePosition fp = GetFilePos(xe);
                            throw new XmlException("<reg> has an invalid value (range defined by <HoldingregisterStart>)" + Environment.NewLine + LimitXmlTextLength(xe.ToString()) + Environment.NewLine, null, fp.Line, fp.Col);
                        }
                    }
                    regSeen = true;
                }
                else
                // <text> description, optional
                if (CheckElementNameMatch(elemName, "text"))
                {
                    newVar.Description = xe.Value;
                }
                else
                // <longdescr> longdescription (tooltip) text, optional
                if (CheckElementNameMatch(elemName, "longdescr"))
                {
                    newVar.Longdescr = xe.Value;
                    HasLongdescrs = true;
                }
                else
                // <type>, optional
                if (CheckElementNameMatch(elemName, "type"))
                {
                    if (GetVarTypestringToFormatAndSize(xe.Value.Trim(), out newVar.Format, out newVar.Size))
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
                        TFilePosition fp = GetFilePos(xe);
                        throw new XmlException("XML element <type> has an invalid value. Valid values are int16, uint16, int32, uint32, hex16, hex32, bin16, bin32, float32, ascii16 or ascii32." + Environment.NewLine + LimitXmlTextLength(xe.ToString()) + Environment.NewLine, null, fp.Line, fp.Col);
                    }
                }
                else
                // get minimumlength, optional
                if (CheckElementNameMatch(elemName, "minlen"))
                {
                    string TmpString = xe.Value.Trim();
                    int TmpMinlen;
                    if (int.TryParse(TmpString, out TmpMinlen)
                        && ((TmpMinlen >= 1) && (TmpMinlen <= 32)))
                    {
                        newVar.MinLen = TmpMinlen;
                    }
                    else
                    {
                        TFilePosition fp = GetFilePos(xe);
                        throw new XmlException("XML element <minlen> has an invalid value. Valid values are 1 - 32." + Environment.NewLine + LimitXmlTextLength(xe.ToString()) + Environment.NewLine, null, fp.Line, fp.Col);
                    }
                }
                else
                // <rw>, optional
                if (CheckElementNameMatch(elemName, "rw"))
                {
                    if (GetReadWriteFlags(xe.Value.Trim(), out newVar.isReadable, out newVar.isWriteable) == false)
                    {
                        TFilePosition fp = GetFilePos(xe);
                        throw new XmlException("XML element <rw> has an invalid value. Valid values are 'r', 'rw', wr', or 'w'." + Environment.NewLine + LimitXmlTextLength(xe.ToString()) + Environment.NewLine, null, fp.Line, fp.Col);
                    }
                }
                else
                // <fcol> foregroundcolor, optional
                if (CheckElementNameMatch(elemName, "fcol"))
                {
                    try
                    {
                        newVar.ForeColor = ColorFromString.ToColor(xe.Value.Trim());
                    }
                    catch
                    {
                        TFilePosition fp = GetFilePos(xe);
                        throw new XmlException("XML element <fcol> has an invalid value. Valid values are RGB values 0-255 like '255,0,0'; HTML style hex colors #RRGGBB like #FF0000, or a color name like 'Red'." + Environment.NewLine + LimitXmlTextLength(xe.ToString()) + Environment.NewLine, null, fp.Line, fp.Col);
                    }
                }
                else
                // <bcol> backgroundcolor, optional
                if (CheckElementNameMatch(elemName, "bcol"))
                {
                    try
                    {
                        newVar.BackColor = ColorFromString.ToColor(xe.Value.Trim());
                    }
                    catch
                    {
                        TFilePosition fp = GetFilePos(xe);
                        throw new XmlException("XML element <bcol> has an invalid value. Valid values are RGB values 0-255 like '255,0,0'; HTML style hex colors #RRGGBB like #FF0000, or a color name like 'Red'." + Environment.NewLine + LimitXmlTextLength(xe.ToString()) + Environment.NewLine, null, fp.Line, fp.Col);
                    }
                }
                else
                // <decimals>, optional
                if (CheckElementNameMatch(elemName, "decimals"))
                {
                    string TmpString = xe.Value.Trim();
                    int TmpDecimals;
                    if (int.TryParse(TmpString, out TmpDecimals)
                        && ((TmpDecimals >= 0) && (TmpDecimals <= 12)))
                    {
                        // ok
                        newVar.Decimals = TmpDecimals;
                    }
                    else
                    {
                        TFilePosition fp = GetFilePos(xe);
                        throw new XmlException("XML element <decimals> has an invalid value. Valid values are 0 - 12." + Environment.NewLine + LimitXmlTextLength(xe.ToString()) + Environment.NewLine, null, fp.Line, fp.Col);
                    }
                }
                else
                // <expandfields>, optional
                if (CheckElementNameMatch(elemName, "expandfields"))
                {
                    string TmpString = xe.Value.Trim();
                    bool TmpBool;
                    if (TryParseBool(TmpString, out TmpBool))
                    {
                        newVar.Expand = TmpBool;
                    }
                    else
                    {
                        TFilePosition fp = GetFilePos(xe);
                        throw new XmlException("XML element <expandfields> has an invalid value. Valid values are 'true' or 'false'." + Environment.NewLine + LimitXmlTextLength(xe.ToString()) + Environment.NewLine, null, fp.Line, fp.Col);
                    }
                }
                else
                // <enum>, optional
                if (CheckElementNameMatch(elemName, "enum"))
                {
                    newVar.EnumDict = ProcessEnumElementIntoCEnum(xe);
                }
                else
                // <field> bitfields in this var, optional, multiple
                if (CheckElementNameMatch(elemName, "field"))
                {
                    // new bitfield
                    TBitfield defaultField = new TBitfield();
                    // init with default values
                    defaultField.Position = autoFieldPos;           // default position is auto count
                    defaultField.Width = 1;
                    defaultField.Description = string.Empty;
                    defaultField.Format = Global.eFormat.UInt;
                    defaultField.MinLen = 0;
                    defaultField.ForeColor = newVar.ForeColor;
                    defaultField.BackColor = newVar.BackColor;
                    defaultField.EnumDict = null;

                    TBitfield newField = ProcessVarFieldElement(xe, defaultField);

                    // validate position and width fits in parent
                    if ((newField.Position + newField.Width) <= (newVar.Size * 16))
                    {
                        // okay
                    }
                    else
                    {
                        TFilePosition fp = GetFilePos(xe);
                        throw new XmlException("XML element <field> has an invalid position + width value. Position + width is " + (newField.Position + newField.Width).ToString() + " but must be <= " + (newVar.Size * 16).ToString() + " to fit." + Environment.NewLine + LimitXmlTextLength(xe.ToString()) + Environment.NewLine, null, fp.Line, fp.Col);
                    }

                    // auto count capability for Position
                    autoFieldPos += newField.Width;

                    // Add new bitfield to the list
                    // If we haven't yet got a list: create one
                    if (newVar.Bitfields == null)
                    {
                        newVar.Bitfields = new List<TBitfield>();
                    }
                    newVar.Bitfields.Add(newField);

                }
                else
                {
                    // invalid/unsupported element found
                }
            }

            // <reg> is required
            if (regSeen == false)
            {
                string elName = "reg";
                TFilePosition fp = GetFilePos(pxe);
                throw new XmlException("XML element <" + elName + "> not found. A <" + elName + "> element is required in a <" + pxe.Name.LocalName + "> element." + Environment.NewLine, null, fp.Line, fp.Col);
            }

            // Check: decimals != 0 is allowed only for int and uint types
            if ((newVar.Decimals != 0) && (newVar.Format != Global.eFormat.Int) && (newVar.Format != Global.eFormat.UInt))
            {
                TFilePosition fp = GetFilePos(pxe);
                throw new XmlException("XML element <decimals> with value > 0 is only allowed for int and uint types." + Environment.NewLine + LimitXmlTextLength(pxe.ToString()) + Environment.NewLine, null, fp.Line, fp.Col);
            }

            return newVar;
        }

        /// <summary>
        /// Process a <var> element
        /// Throws exception on error
        /// </summary>
        /// <param name="pxe">The parent XML element</param>
        private TBitfield ProcessVarFieldElement(XElement pxe, TBitfield defaultField)
        {
            TBitfield newField = defaultField;

            foreach (XElement xe in pxe.Elements())
            {
                string elemName = xe.Name.LocalName;
                Debug.Print("<Field> " + elemName);

                // <pos> bitfield position, optional
                if (CheckElementNameMatch(elemName, "pos"))
                {
                    if (int.TryParse(xe.Value.Trim(), out newField.Position)
                        && (newField.Position >= 0) && (newField.Position <= 31))
                    {
                        // ok
                    }
                    else
                    {
                        TFilePosition fp = GetFilePos(xe);
                        throw new XmlException("XML element <pos> has an invalid value. Valid values are 0 - 31." + Environment.NewLine + LimitXmlTextLength(xe.ToString()) + Environment.NewLine, null, fp.Line, fp.Col);
                    }
                }
                else
                // <width> bitfield width, optional
                if (CheckElementNameMatch(elemName, "width"))
                {
                    if (int.TryParse(xe.Value.Trim(), out newField.Width)
                        && (newField.Width >= 1) && (newField.Width <= 32))
                    {
                        // ok
                    }
                    else
                    {
                        TFilePosition fp = GetFilePos(xe);
                        throw new XmlException("XML element <width> has an invalid value. Valid values are 1 - 32." + Environment.NewLine + LimitXmlTextLength(xe.ToString()) + Environment.NewLine, null, fp.Line, fp.Col);
                    }
                }
                else
                // <text> description, optional
                if (CheckElementNameMatch(elemName, "text"))
                {
                    newField.Description = xe.Value;
                }
                else
                // <type>, optional
                if (CheckElementNameMatch(elemName, "type"))
                {
                    if (GetBitfieldTypestringToFormat(xe.Value.Trim(), out newField.Format))
                    {
                        // ok
                    }
                    else
                    {
                        TFilePosition fp = GetFilePos(xe);
                        throw new XmlException("XML element <type> has an invalid value. Valid values are int, uint, hex, bin or ascii." + Environment.NewLine + LimitXmlTextLength(xe.ToString()) + Environment.NewLine, null, fp.Line, fp.Col);
                    }
                }
                else
                // <minlen> minimumlength, optional
                if (CheckElementNameMatch(elemName, "minlen"))
                {
                    if (int.TryParse(xe.Value.Trim(), out newField.MinLen)
                        && ((newField.MinLen >= 1) && (newField.MinLen <= 32)))
                    {
                        // ok
                    }
                    else
                    {
                        TFilePosition fp = GetFilePos(xe);
                        throw new XmlException("XML element <minlen> has an invalid value. Valid values are 1 - 32." + Environment.NewLine + LimitXmlTextLength(xe.ToString()) + Environment.NewLine, null, fp.Line, fp.Col);
                    }
                }
                else
                // <fcol> foreground color, optional
                if (CheckElementNameMatch(elemName, "fcol"))
                {
                    try
                    {
                        newField.ForeColor = ColorFromString.ToColor(xe.Value.Trim());
                    }
                    catch
                    {
                        TFilePosition fp = GetFilePos(xe);
                        throw new XmlException("XML element <fcol> has an invalid value. Valid values are RGB values 0-255 like '255,0,0'; HTML style hex colors #RRGGBB like #FF0000, or a color name like 'Red'." + Environment.NewLine + LimitXmlTextLength(xe.ToString()) + Environment.NewLine, null, fp.Line, fp.Col);
                    }
                }

                // <bcol> backgroundcolor, optional
                if (CheckElementNameMatch(elemName, "bcol"))
                {
                    try
                    {
                        newField.BackColor = ColorFromString.ToColor(xe.Value.Trim());
                    }
                    catch
                    {
                        TFilePosition fp = GetFilePos(xe);
                        throw new XmlException("XML element <bcol> has an invalid value. Valid values are RGB values 0-255 like '255,0,0'; HTML style hex colors #RRGGBB like #FF0000, or a color name like 'Red'." + Environment.NewLine + LimitXmlTextLength(xe.ToString()) + Environment.NewLine, null, fp.Line, fp.Col);
                    }
                }
                else
                // <longdescr> longdescription (tooltip) text, optional
                if (CheckElementNameMatch(elemName, "longdescr"))
                {
                    newField.Longdescr = xe.Value;
                    HasLongdescrs = true;
                }
                else
                // <enum>, optional
                if (CheckElementNameMatch(elemName, "enum"))
                {
                    newField.EnumDict = ProcessEnumElementIntoCEnum(xe);
                }
                else
                {
                    // invalid/unsupported element found
                }
            }

            return newField;
        }

        /// <summary>
        /// Get enum items list as a dictionary
        /// </summary>
        /// <param name="pxe"></param>
        /// <returns>Dictionary with items. Null if there are no items or Elsetext. </returns>
        private Global.CEnum ProcessEnumElementIntoCEnum(XElement pxe)
        {
            Global.CEnum myEnum = new Global.CEnum();
            myEnum.Dict = new Dictionary<int, string>();
            string ElseText = null;

            bool elseSeen = false;

            foreach (XElement xitem in pxe.Elements())
            {
                string elemName = xitem.Name.LocalName;
                Debug.Print("<enum> " + elemName);

                // <item>, optional, multiple
                if (CheckElementNameMatch(elemName, "item"))
                {
                    bool valueSeen = false;
                    int valueInt = 0;
                    bool textSeen = false;
                    string textStr = null;

                    // for this item, get <value> and <text>, required
                    foreach (XElement xe in xitem.Elements())
                    {
                        string elem2Name = xe.Name.LocalName;
                        Debug.Print("<item> " + elem2Name);

                        // item <value>
                        if (CheckElementNameMatch(elem2Name, "value"))
                        {
                            if (valueSeen)
                            {
                                // found more than 1
                                // throw exception with fileposition of 2nd
                                TFilePosition fp = GetFilePos(xe);
                                throw new XmlException("XML element <" + xe.Name.LocalName + "> is found multiple times. It is allowed only once in its parent <" + xe.Parent.Name.LocalName + "> element." + Environment.NewLine + LimitXmlTextLength(xe.ToString()) + Environment.NewLine, null, fp.Line, fp.Col);
                            }
                            else
                            {
                                // store value in valueInt
                                string TmpString = xe.Value.Trim();
                                if (int.TryParse(TmpString, out valueInt))
                                {
                                    // OK
                                }
                                else
                                {
                                    TFilePosition fp = GetFilePos(xe);
                                    throw new XmlException("XML element <" + xe.Name + "> has an invalid value. Must be valid integer." + Environment.NewLine + LimitXmlTextLength(xe.ToString()) + Environment.NewLine, null, fp.Line, fp.Col);
                                }
                                valueSeen = true;
                            }
                        }
                        else
                        // item <text>
                        if (CheckElementNameMatch(elem2Name, "text"))
                        {
                            if (textSeen)
                            {
                                // found more than 1
                                // throw exception with fileposition of 2nd
                                TFilePosition fp = GetFilePos(xe);
                                throw new XmlException("XML element <" + xe.Name.LocalName + "> is found multiple times. It is allowed only once in its parent <" + xe.Parent.Name.LocalName + "> element." + Environment.NewLine + LimitXmlTextLength(xe.ToString()) + Environment.NewLine, null, fp.Line, fp.Col);
                            }
                            else
                            {
                                textStr = xe.Value.Trim();
                                textSeen = true;
                            }
                        }
                        else
                        {
                            // invalid/unsupported element found
                        }
                    }

                    // <value> and <text> required, seen?
                    if (valueSeen && textSeen)
                    {
                        // add item to dictionary
                        try
                        {
                            // throws exception on duplicate key
                            myEnum.Dict.Add(valueInt, textStr);
                        }
                        catch
                        {
                            TFilePosition fp = GetFilePos(xitem);
                            throw new XmlException("XML element <" + xitem.Name + "> has an invalid value. Enum integers must be unique." + Environment.NewLine + LimitXmlTextLength(xitem.ToString()) + Environment.NewLine, null, fp.Line, fp.Col);
                        }

                    }
                    else
                    {
                        if (valueSeen == false)
                        {
                            string elName = "value";
                            TFilePosition fp = GetFilePos(xitem);
                            throw new XmlException("XML element <" + elName + "> not found. A <" + elName + "> element is required in a <" + xitem.Name.LocalName + "> element." + Environment.NewLine, null, fp.Line, fp.Col);
                        }
                        if (textSeen == false)
                        {
                            string elName = "text";
                            TFilePosition fp = GetFilePos(xitem);
                            throw new XmlException("XML element <" + elName + "> not found. A <" + elName + "> element is required in a <" + xitem.Name.LocalName + "> element." + Environment.NewLine, null, fp.Line, fp.Col);
                        }
                    }
                }
                else
                // else
                if (CheckElementNameMatch(elemName, "else"))
                {
                    if (elseSeen)
                    {
                        // found more than 1
                        // throw exception with fileposition of 2nd
                        TFilePosition fp = GetFilePos(xitem);
                        throw new XmlException("XML element <" + xitem.Name.LocalName + "> is found multiple times. It is allowed only once in its parent <" + xitem.Parent.Name.LocalName + "> element." + Environment.NewLine + LimitXmlTextLength(xitem.ToString()) + Environment.NewLine, null, fp.Line, fp.Col);
                    }
                    else
                    {
                        ElseText = xitem.Value.Trim();
                        elseSeen = true;
                    }
                }
                else
                {
                    // invalid/unsupported element found
                }
            }

            // pass elsetext, or use default text if it was not specified
            // can be specified as ""
            if (ElseText != null)
            {
                myEnum.ElseText = ElseText;
            }
            else
            {
                myEnum.ElseText = "enum?";
            }


            return myEnum;
        }


        /// <summary>
        /// Process an <autostart></autostart> element
        /// This should be done *after* loading all tab pages
        /// </summary>
        /// <param name="pxe">The parent XML element</param>
        /// <return>true if one of the elements was seen</return>
        private bool ProcessAutostartElement(XElement pxe)
        {
            bool seen = false;

            // if there is an <autostart> tag, the <enable> must be present and true to activate autostart
            Options.AutostartSeen = true;
            Options.AutostartEnable = false;

            foreach (XElement xe in pxe.Elements())
            {
                string elemName = xe.Name.LocalName;
                Debug.Print("<Autostart> " + elemName);

                // Enable, optional
                if (CheckElementNameMatch(elemName, "enable"))
                {
                    // accept true and false
                    string TmpString = xe.Value.Trim();
                    bool TmpBool;
                    if (TryParseBool(TmpString, out TmpBool))
                    {
                        Options.AutostartEnable = TmpBool;
                    }
                    else
                    {
                        TFilePosition fp = GetFilePos(xe);
                        throw new XmlException("XML element <enable> has an invalid value. Valid values are 'true' or 'false'." + Environment.NewLine + LimitXmlTextLength(xe.ToString()) + Environment.NewLine, null, fp.Line, fp.Col);
                    }

                    seen = true;
                }

                // Page, optional
                if (CheckElementNameMatch(elemName, "page"))
                {
                    // the page can be the name or a numeric (1 for first tab page)
                    Options.AutostartTabpage = xe.Value.Trim();

                    autostartPageXElement = xe;
                    seen = true;
                }
            }

            return seen;
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
        /// Process a <modbustcpserver> element
        /// Throws exception on error
        /// </summary>
        /// <param name="pxe">The parent XML element</param>
        /// <return>true if one of the elements was seen</return>
        private bool ProcessModbustcpserverElement(XElement pxe)
        {
            bool Seen = false;

            foreach (XElement xe in pxe.Elements())
            {
                string elemName = xe.Name.LocalName;
                Debug.Print("<Modbustcpserver> " + elemName);

                // Enable
                if (CheckElementNameMatch(elemName, "enable"))
                {
                    // accept true and false
                    string TmpString = xe.Value.Trim();
                    bool TmpBool;
                    if (TryParseBool(TmpString, out TmpBool))
                    {
                        ModbusTcpBridge.Enable = TmpBool;
                        Seen = true;
                    }
                    else
                    {
                        TFilePosition fp = GetFilePos(xe);
                        throw new XmlException("XML element <enable> has an invalid value. Valid values are 'true' or 'false'." + Environment.NewLine + LimitXmlTextLength(xe.ToString()) + Environment.NewLine, null, fp.Line, fp.Col);
                    }
                }

                // Port
                if (CheckElementNameMatch(elemName, "port"))
                {
                    string TmpString = xe.Value.Trim();
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
                        ModbusTcpBridge.Enable = false;
                        TFilePosition fp = GetFilePos(xe);
                        throw new XmlException("XML element <port> has an invalid value. Valid values are 1 - 65535." + Environment.NewLine + LimitXmlTextLength(xe.ToString()) + Environment.NewLine, null, fp.Line, fp.Col);
                    }
                }
            }

            return Seen;
        }

        /// <summary>
        /// Process an <include> element
        /// Throws exception on error
        /// </summary>
        /// <param name="pxe">The parent XML element</param>
        private void ProcessIncludeElement(XElement pxe)
        {
            bool fileSeen = false;
            string fileName = string.Empty;
            bool isRequired = true;
            XElement fileXElement = null;

            // process child elements
            foreach (XElement xe in pxe.Elements())
            {
                string elemName = xe.Name.LocalName;
                Debug.Print("<Include> " + elemName);

                // <file>, required
                if (CheckElementNameMatch(elemName, "file"))
                {
                    fileName = xe.Value.Trim();
                    fileXElement = xe;
                    fileSeen = true;

                    if (fileName == string.Empty)
                    {
                        string elName = "name";
                        TFilePosition fp = GetFilePos(pxe);
                        throw new XmlException("XML element <" + elName + "> is empty. A <" + elName + "> element is required to have a value." + Environment.NewLine, null, fp.Line, fp.Col);
                    }
                }
                else
                // <required>, optional
                if (CheckElementNameMatch(elemName, "required"))
                {
                    string TmpString = xe.Value.Trim();
                    if (TryParseBool(TmpString, out isRequired))
                    {
                        // required is set
                    }
                    else
                    {
                        TFilePosition fp = GetFilePos(xe);
                        throw new XmlException("XML element <" + xe.Name.LocalName + "> has an invalid value. Valid values are 'true' or 'false'." + Environment.NewLine + LimitXmlTextLength(xe.ToString()) + Environment.NewLine, null, fp.Line, fp.Col);
                    }
                }
                else
                {
                    // invalid/unsupported element found
                }
            }

            // <file> is required
            if (fileSeen == false)
            {
                string elName = "file";
                TFilePosition fp = GetFilePos(pxe);
                throw new XmlException("XML element <" + elName + "> not found. A <" + elName + "> element is required in a <" + pxe.Name.LocalName + "> element." + Environment.NewLine, null, fp.Line, fp.Col);
            }

            if (fileSeen && (string.IsNullOrEmpty(fileName) == false))
            {
                string incFilePath = System.IO.Path.GetFullPath(fileName);

                // get path of include file
                // if it is absolute (rooted) take that
                // if it is relative: combine with the parent file path
                if (System.IO.Path.IsPathRooted(fileName))
                {
                    incFilePath = fileName;
                }
                else
                {
                    string parentDir = System.IO.Path.GetDirectoryName(Path);
                    incFilePath = System.IO.Path.Combine(parentDir, fileName);
                }

                // File exists?
                if (File.Exists(incFilePath))
                {
                    ProcessIncludeFile(incFilePath);
                }
                else
                {
                    if (isRequired)
                    {
                        TFilePosition fp = GetFilePos(fileXElement);
                        throw new XmlException("<include> element: file not found: " + Environment.NewLine + incFilePath, null, fp.Line, fp.Col);
                    }
                }
            }
        }

        /// <summary>
        /// Process an include file
        /// load only the <tab> and <include> elements
        /// </summary>
        /// <param name="path"></param>
        private void ProcessIncludeFile(string path)
        {
            // add file to list of nested files
            fileList.Add(path);

            // Load xml document into incXmlDoc
            XDocument incXmlDoc = XDocument.Load(path, LoadOptions.SetLineInfo);

            //TODO: validation doen?
#if false
            // got validation schema?
            // throws exception on validation error
            if (ValidationSchemaPath != "")
            {
                XmlSchemaSet schemas = new XmlSchemaSet();
                schemas.Add("", XmlReader.Create(ValidationSchemaPath));
                incXmlDoc.Validate(schemas, ValidationEventHandler);
            }
#endif

            // Parse the document
            // Store all data in this class

            // check root element
            XElement incXmlRootNode = incXmlDoc.Root;
            string rootElementName = incXmlRootNode.Name.LocalName;
            string expectRootName = "modbuscontrol";
            if (rootElementName != expectRootName)
            {
                throw new XmlException("XML root element <" + expectRootName + "> not found. A <" + expectRootName + "> root element is required in an mbc project file.");
            }

            // iterate elements under root
            foreach (XElement xe in incXmlRootNode.Elements())
            {
                string elemName = xe.Name.LocalName;
                Debug.Print("Include: root: " + elemName);

                // <tab> elements, optional, multiple
                if (CheckElementNameMatch(elemName, "tab"))
                {
                    ProcessTabElement(xe);
                }
                else
                // <include> elements, optional, multiple
                if (CheckElementNameMatch(elemName, "include"))
                {
                    ProcessIncludeElement(xe);
                }
                else
                {
                    // invalid/unsupported element found
                }
            }

            // remove from nested files list
            fileList.RemoveAt(fileList.Count - 1);
        }

        /// <summary>
        /// Get the first XML element with this name.
        /// Finds elements with the given case and in lower case.
        /// </summary>
        /// <return>null if not found</return>
        private XElement FindElement(XElement parent, string name)
        {
            // get elements with this name
            XElement xel = parent.Element(name);
            // if not found, and name has upper case letters, try again with lower case
            if ((xel == null) && name.Any(char.IsUpper))
            {
                name = name.ToLower();
                xel = parent.Element(name);
            }

            return xel;
        }

        /// <summary>
        /// Remove whitespace and limit the string in length
        /// </summary>
        private string LimitXmlTextLength(string text)
        {
            const int maxlen = 180;
            char[] outs = new char[maxlen];
            int j = 0;
            int len = text.Length;

            // copy all characters except CR/LF/TAB, till max length
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
            // max length reached? Then end with '...'
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
        /// Check if Holdingreg number is valid
        /// </summary>
        /// <returns>true if valid</returns>
        private bool ValidateHoldingregNr(UInt32 Regnr)
        {
            // accepts:
            // 40000/400000: 40000-49999 + 400000-465535
            // 40001/400001: 40001-49999 + 400001-465536

            bool ret = false;

            if ((Options.HoldingregistersStart == 40000) || (Options.HoldingregistersStart == 400000))
            {
                if ((Regnr >= 40000) && (Regnr <= 49999))
                {
                    ret = true;
                }
                else if ((Regnr >= 400000) && (Regnr <= 465535))
                {
                    ret = true;
                }
            }
            if ((Options.HoldingregistersStart == 40001) || (Options.HoldingregistersStart == 400001))
            {
                if ((Regnr >= 40001) && (Regnr <= 49999))
                {
                    ret = true;
                }
                else if ((Regnr >= 400001) && (Regnr <= 465536))
                {
                    ret = true;
                }
            }

            return ret;
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
            XElement xeModbus = FindElement(XmlRootNode, "Modbus");
            if (xeModbus == null)
            {
                xeModbus = new XElement("modbus");
                XmlRootNode.Add(xeModbus);
            }

            // Find Connection node, or create it
            XElement xe = FindElement(xeModbus, "Connection");
            if (xe == null)
            {
                xe = new XElement("connection");
                xeModbus.Add(xe);
            }
            xe.Value = ConnectionTypes[(int)Connection.ConnectionType];

            // Find Serial port node, or create it
            xe = FindElement(xeModbus, "Port");
            if (xe == null)
            {
                xe = new XElement("port");
                xeModbus.Add(xe);
            }
            xe.Value = Connection.SerialPort;

            // Find Serial Baudrate node, or create it
            xe = FindElement(xeModbus, "Baudrate");
            if (xe == null)
            {
                xe = new XElement("baudrate");
                xeModbus.Add(xe);
            }
            xe.Value = Connection.SerialBaudrate.ToString();

            // Find Serial Parity node, or create it
            xe = FindElement(xeModbus, "Parity");
            if (xe == null)
            {
                xe = new XElement("parity");
                xeModbus.Add(xe);
            }
            xe.Value = Connection.SerialParity.ToString().ToLower();

            // Find Serial EndofmessageTimeout node, or create it
            xe = FindElement(xeModbus, "EndofmessageTimeout");
            if (xe == null)
            {
                xe = new XElement("endofmessagetimeout");
                xeModbus.Add(xe);
            }
            xe.Value = Connection.SerialEndOfMessageTimeout.ToString();

            // Find Serial OneWireEchoSuppression node, or create it
            xe = FindElement(xeModbus, "OnewireEchoSuppression");
            if (xe == null)
            {
                xe = new XElement("onewireechosuppression");
                xeModbus.Add(xe);
            }
            xe.Value = Connection.SerialOneWireEchoSuppression.ToString().ToLower();

            // Find IPAddress node, or create it
            xe = FindElement(xeModbus, "IPaddress");
            if (xe == null)
            {
                xe = new XElement("ipaddress");
                xeModbus.Add(xe);
            }
            xe.Value = Connection.IPAddress.ToString();

            // Find IPPort node, or create it
            xe = FindElement(xeModbus, "IPport");
            if (xe == null)
            {
                xe = new XElement("ipport");
                xeModbus.Add(xe);
            }
            xe.Value = Connection.IPPort.ToString();

            //TODO:  Global.eTcpConnectMode TcpConnectMode  static/dynamic

            // Find SlaveID node, or create it
            xe = FindElement(xeModbus, "SlaveID");
            if (xe == null)
            {
                xe = new XElement("slaveid");
                xeModbus.Add(xe);
            }
            xe.Value = Connection.SlaveID.ToString();

            // Find ResponseTimeout node, or create it
            xe = FindElement(xeModbus, "ResponseTimeout");
            if (xe == null)
            {
                xe = new XElement("responsetimeout");
                xeModbus.Add(xe);
            }
            xe.Value = Connection.ResponseTimeout.ToString();

            // Find MaximumFramesize node, or create it
            xe = FindElement(xeModbus, "MaximumFramesize");
            if (xe == null)
            {
                xe = new XElement("maximumframesize");
                xeModbus.Add(xe);
            }
            xe.Value = Connection.MaximumFrameSize.ToString();

            // Find interframedelay node, or create it
            xe = FindElement(xeModbus, "interframedelay");
            if (xe == null)
            {
                xe = new XElement("interframedelay");
                xeModbus.Add(xe);
            }
            xe.Value = Connection.InterFrameDelay.ToString();

            // Find WriteHoldingregisterFC node, or create it
            xe = FindElement(xeModbus, "WriteHoldingregisterFC");
            if (xe == null)
            {
                xe = new XElement("writeholdingregisterfc");
                xeModbus.Add(xe);
            }
            xe.Value = WriteHoldingregisterFCs[(int)Connection.WriteHoldingregFunctioncode];

            // Remove Value32BitEndian, obsolete
            xe = FindElement(xeModbus, "Value32BitEndian");
            if (xe != null)
            {
                xe.Remove();
            }

            // Find int32endian node, or create it
            xe = FindElement(xeModbus, "int32endian");
            if (xe == null)
            {
                xe = new XElement("int32endian");
                xeModbus.Add(xe);
            }
            xe.Value = Connection.Int32Endian.ToString().ToLower();

            // Find float32endian node, or create it
            xe = FindElement(xeModbus, "float32endian");
            if (xe == null)
            {
                xe = new XElement("float32endian");
                xeModbus.Add(xe);
            }
            xe.Value = Connection.Float32Endian.ToString().ToLower();

            // Find ascii16endian node, or create it
            xe = FindElement(xeModbus, "ascii16endian");
            if (xe == null)
            {
                xe = new XElement("ascii16endian");
                xeModbus.Add(xe);
            }
            xe.Value = Connection.Ascii16Endian.ToString().ToLower();

            // Remove ShowType, obsolete
            xe = FindElement(xeModbus, "ShowType");
            if (xe != null)
            {
                xe.Remove();
            }

            // Remove ShowReadwrite, obsolete
            xe = FindElement(xeModbus, "ShowReadwrite");
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
            XElement xeBridge = FindElement(XmlRootNode, "modbustcpserver");
            if (xeBridge == null)
            {
                xeBridge = new XElement("modbustcpserver");
                XmlRootNode.Add(xeBridge);
            }

            // Find Enable node, or create it
            XElement xe = FindElement(xeBridge, "enable");
            if (xe == null)
            {
                xe = new XElement("enable");
                xeBridge.Add(xe);
            }
            xe.Value = ModbusTcpBridge.Enable.ToString().ToLower();

            // Find Port node, or create it
            xe = FindElement(xeBridge, "port");
            if (xe == null)
            {
                xe = new XElement("port");
                xeBridge.Add(xe);
            }
            xe.Value = ModbusTcpBridge.IPPort.ToString();
        }

        /// <summary>
        /// Write other settings to XML document, like samplerate
        /// Create XML nodes if they don't exist yet
        /// </summary>
        public void WriteOtherSettings()
        {
            // Find Modbus node (case insensitive), or create it
            XElement xeModb = FindElement(XmlRootNode, "Modbus");
            if (xeModb == null)
            {
                xeModb = new XElement("modbus");
                XmlRootNode.Add(xeModb);
            }

            // Find Samplerate node, or create it
            XElement xe = FindElement(xeModb, "SampleRate");
            if (xe == null)
            {
                xe = new XElement("samplerate");
                xeModb.Add(xe);
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
            XElement xeSize = FindElement(XmlRootNode, "Size");
            if (xeSize == null)
            {
                xeSize = new XElement("size");
                XmlRootNode.Add(xeSize);
            }

            // Find Width node, or create it
            XElement widthNode = FindElement(xeSize, "Width");
            if (widthNode == null)
            {
                widthNode = new XElement("width");
                xeSize.Add(widthNode);
            }
            // write value
            // value in mbc file is standarized to 96DPI
            widthNode.Value = FormSize.Width.ToString();

            // Find Height node, or create it
            XElement heightNode = FindElement(xeSize, "Height");
            if (heightNode == null)
            {
                heightNode = new XElement("height");
                xeSize.Add(heightNode);
            }
            // write value
            // value in mbc file is standarized to 96DPI
            heightNode.Value = FormSize.Height.ToString();
        }

        /// Update the project mbc XML to the current version
        /// convert all XML tags to lowercase
        /// convert all enum keywords to lowercase
        /// remove or replace obsolete tags
        public void UpdateProjectXML()
        {
            //DumpXMLTree();

            // convert all XML tags to lowercase
            XMLIterateAllNodesForHandler(XmlDoc, ConvertAllTagsToLowercaseHandler);

            // convert elements with enum keywords to lower case
            XMLIterateAllNodesForHandler(XmlDoc, ConvertAllEnumValuesToLowercaseHandler);

            // remove or replace obsolete tags in <Modbus>
            XMLRemoveOrReplaceObsoleteModbusElements(XmlDoc);
        }

        /// <summary>
        /// Iterate all XML nodes and call a handler for each
        /// </summary>
        private void XMLIterateAllNodesForHandler(XDocument doc, XMLIterateAllNodesCallback handler)
        {
            foreach (XElement xe in doc.Elements())
            {
                XMLIterateAllNodesForHandlerRec(xe, handler);
            }
        }

        /// <summary>
        /// Helper for XMLIterateAllNodesForHandler recursivity
        /// </summary>
        private void XMLIterateAllNodesForHandlerRec(XElement xe, XMLIterateAllNodesCallback handler)
        {
            // call the handler 
            handler(xe);

            foreach (XElement xe2 in xe.Elements())
            {
                XMLIterateAllNodesForHandlerRec(xe2, handler);
            }
        }

        /// <summary>
        /// Handler for XMLIterateAllNodesForHandler()
        /// convert all XML tag names to lowercase
        /// </summary>
        private void ConvertAllTagsToLowercaseHandler(XElement xe)
        {
            xe.Name = xe.Name.ToString().ToLower();
        }

        /// <summary>
        /// Handler for XMLIterateAllNodesForHandler()
        /// convert all enum keywords to lowercase
        /// </summary>
        private void ConvertAllEnumValuesToLowercaseHandler(XElement xe)
        {
            if (TagsToConvertValueToLowerList.Contains(xe.Name.ToString()))
            {
                xe.Value = xe.Value.ToString().ToLower().Trim();
            }
        }

        /// <summary>
        /// remove or replace obsolete elements in <modbus>
        /// </summary>
        private void XMLRemoveOrReplaceObsoleteModbusElements(XDocument doc)
        {
            // niet itereren en intussen nodes deleten of inserten! dat gaat fout!
            // dus: met iteratie een node zoeken, die deleten, dan nieuwe iteratie beginnen

            // Search /modbus
            XElement xm = XmlFindElement(doc.Root, "modbus");
            if (xm != null)
            {
                XElement xe;

                // Remove /modbus/showreadwrite
                do
                {
                    xe = XmlFindElement(xm, "showreadwrite");
                    if (xe != null)
                    {
                        xe.Remove();
                    }
                }
                while (xe != null);

                // Remove /modbus/showtype
                do
                {
                    xe = XmlFindElement(xm, "showtype");
                    if (xe != null)
                    {
                        xe.Remove();
                    }
                }
                while (xe != null);

                // replace /modbus/value32bitendian
                // with int32endian, float32endian, ascii16endian
                // if they don't exist yet

                // Remove /modbus/value32bitendian
                do
                {
                    xe = XmlFindElement(xm, "value32bitendian");
                    if (xe != null)
                    {
                        xe.Remove();
                    }
                }
                while (xe != null);

                // insert int32endian if it doesn't exist yet
                xe = XmlFindElement(xm, "int32endian");
                if (xe == null)
                {
                    xe = new XElement("int32endian");
                    xe.Value = Connection.Int32Endian.ToString().ToLower();
                    xm.Add(xe);
                }

                // insert float32endian if it doesn't exist yet
                xe = XmlFindElement(xm, "float32endian");
                if (xe == null)
                {
                    xe = new XElement("float32endian");
                    xe.Value = Connection.Float32Endian.ToString().ToLower();
                    xm.Add(xe);
                }

                // insert ascii16endian if it doesn't exist yet
                xe = XmlFindElement(xm, "ascii16endian");
                if (xe == null)
                {
                    xe = new XElement("ascii16endian");
                    xe.Value = Connection.Ascii16Endian.ToString().ToLower();
                    xm.Add(xe);
                }
            }
        }

        /// <summary>
        /// Iterate through XML elements, recursive
        /// Return null if not found
        /// </summary>
        private XElement XmlFindElement(XElement xe, string name)
        {
            // found?
            if (xe.Name == name)
            {
                return xe;
            }

            foreach (XElement xe2 in xe.Elements())
            {
                XElement xe3 = XmlFindElement(xe2, name);
                if (xe3 != null)
                {
                    return xe3;
                }
            }

            return null;
        }

        /// <summary>
        /// Save configuration to file
        /// </summary>
        public void SaveFile()
        {
            /* 
            //default XDocument save, with BOM, no indenting
            XmlDoc.Save(Path);
            */

            // save
            // without BOM, with indenting
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Encoding = new UTF8Encoding(false); // The false means 'no BOM'
            settings.Indent = true;
            settings.IndentChars = ("\t");
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
            int level = 0;

            Debug.Print("----------------------");
            Debug.Print("DumpXMLTree:");
            foreach (XElement xel in XmlDoc.Elements())
            {
                DumpXMLElementRec(xel, level);
            }
        }

        private void DumpXMLElementRec(XElement xel, int level)
        {
            string s;

            s = new String(' ', level) + xel.Name.ToString();
            //s += ", " + xel.NodeType.ToString();
            //s += ", " + xel.HasElements.ToString();
            s += ", " + LeftStr(xel.Value.ToString(), 20);

            XmlNodeType t = xel.NodeType;

            Debug.Print(s);

            foreach (XElement xel2 in xel.Elements())
            {
                DumpXMLElementRec(xel2, level + 1);
            }
        }

        private string LeftStr(string s, int len)
        {
            int fraglen = Math.Min(len, s.Length);
            return s.Substring(0, fraglen);
        }

        /// <summary>
        /// Check element name, with old camelcase and new lowercase
        /// </summary>
        /// <param name="elemName">Name that was found</param>
        /// <param name="name">Name we are looking for</param>
        /// <returns></returns>
        private bool CheckElementNameMatch(string elemName, string name)
        {
            if (elemName == name)
                return true;

            if (elemName == name.ToLower())
                return true;

            return false;
        }
    }
}
