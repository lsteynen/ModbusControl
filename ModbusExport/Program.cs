using System;
using System.IO;
using System.IO.Ports;

using ModbusControlSpace;
using LST.Helpers;

namespace ModbusExport
{
    class Program
    {
        private static CommandlineProc CommandlineArgs;
        private static string mbcFilename;
        private static string csvFilename;
        private static CProject Project;
        public static ModbusInterface mb = new ModbusInterface();
        private static Export export = new Export();
        private static bool Verbose = false;
        private static bool ContinueOnError = false;
        private static bool isSerialOpenPortError;
        private static Logger logger;

        private enum eExitcodes
        {
            NoError = 0,
            CommandlineError = 1,
            FileError = 2,
            CommError = 3,
            OtherError = 9
        };

        static int Main(string[] args)
        {
            // create project object, with defaults
			// is required
            Project = new CProject();

            // parse commandline
            CommandlineArgs = new CommandlineProc();
            CommandlineArgs.versionString = AppHelpers.VersionString;
            try
            {
                CommandlineArgs.Load();
            }
            catch (Exception e)
            {
                // Commandline error
                string s = "Error: " + e.Message;
                Print(s);

                if (CommandlineArgs.ArgHelpSwitch == true)
                {
                    Print(CommandlineArgs.GetUsageString());
                }

                // exit
                return (int)eExitcodes.CommandlineError;
            }

            if (CommandlineArgs.ArgHelpSwitch == true)
            {
                Print(CommandlineArgs.GetUsageString());

                // exit
                return (int)eExitcodes.NoError;
            }

            Verbose = CommandlineArgs.ArgVerboseSwitch;
            ContinueOnError = CommandlineArgs.ArgContinueOnErrorSwitch;

            // setup logger if used
            if (string.IsNullOrEmpty(CommandlineArgs.ArgLogFile) == false)
            {
                try
                {
                    logger = new Logger();
                    logger.Append = true;                   // append to existing file
                    if (CommandlineArgs.ArgLogTimeSwitch)
                    {
                        logger.AddTimestampsMode = 1;           // ISO8601 datetime
                    }
                    logger.AddHeaderLine = true;            // add separator line at start
                    logger.HeaderLineText = "";             // add empty separator line
                    logger.Open(CommandlineArgs.ArgLogFile);
                }
                catch (Exception ex)
                {
                    logger = null;
                    string s = "Error: Could not open log file: " + ex.Message;
                    Print(s);
                    // exit
                    return (int)eExitcodes.CommandlineError;
                }
            }

            // writer starter line into logfile
            PrintLog("ModbusExport.exe " + AppHelpers.VersionString);

            // process files
            // mbc file is required
            // csv file is required
            mbcFilename = CommandlineArgs.ArgMbcFile;
            csvFilename = CommandlineArgs.ArgCsvFile;

            // load mbc file, required
            Print("Load mbc file: " + mbcFilename);

            try
            {
                Project.Load(mbcFilename);
            }
            catch (Exception ex)
            {
                string s = "Error: Error in loading .mbc project file: " + ex.Message;
                Print(s);

                // exit
                return (int)eExitcodes.FileError;
            }

            // apply command line arguments to the Project (connections settings)
            ApplyCommandlineToProjectConfig();

            // pass new configuration to Converts
            Converts.Int32Endian = Project.Connection.Int32Endian;
            Converts.Float32Endian = Project.Connection.Float32Endian;
            Converts.StringEndian = Project.Connection.StringEndian;
            Converts.StringEncodingName = Project.Connection.StringEncoding;
            // pass new configuration to RegisterToOffset
            RegisterToOffset.RegisterNumberingSchema = Project.Options.HoldingregistersStart;

            // Connect
            Print("Opening connection: " + GetConnectionInfoString());

            try
            {
                OpenConnection();
            }
            catch (Exception e)
            {
                string s = "Error: Could not connect to " + GetConnectionInfoString() + ": " + e.Message;
                Print(s);

                // if serial port error: show which ports exist
                if (isSerialOpenPortError)
                {
                    string infomessage = "Detected serial ports: " + GetDetectedSerialPortsString();
                    Print(infomessage);
                }

                // exit
                return (int)eExitcodes.CommError;
            }

            // Export
            Print("Export to CSV file: " + csvFilename);
            export.TabsList = Project.TabsList;
            export.Modbus = mb;
            export.SlaveAddress = Project.Connection.SlaveID;
            export.Filename = csvFilename;
            export.MbcFilename = System.IO.Path.GetFileName(mbcFilename);
            export.OnReportProgress += ReportProgressCallback;
            export.Verbose = Verbose;
            export.ExportFormat = isnull(CommandlineArgs.ArgExportFormat, ""); 
            export.StringExportFormat = isnull(CommandlineArgs.ArgStringExportFormat, ""); 
            export.MaximumFrameSize = Project.Connection.MaximumFrameSize;
            export.ContinueOnError = ContinueOnError;
            export.TabsSelectList = CommandlineArgs.ArgTabsList;

            // exit code
            int exitCode = (int)eExitcodes.NoError;

            // do export
            try
            {
                // Can throw exception on error
                Export.EReturnCode retcode = export.DoExport();

                // show counters
                Print("Export end: " + export.SuccessCount + " registers exported, " + (export.FileErrorCount + export.CommErrorCount) + " errors.");

                // exitcode
                if (retcode != Export.EReturnCode.Success)
                {
                    if (retcode == Export.EReturnCode.FileError)
                    {
                        exitCode = (int)eExitcodes.FileError;
                    }
                    else if (retcode == Export.EReturnCode.CommError)
                    {
                        exitCode = (int)eExitcodes.CommError;
                    }
                    else 
                    {
                        // other error
                        exitCode = (int)eExitcodes.OtherError;
                    }
                }                
            }
            catch (Exception e)
            {
                string s = "Error: " + e.Message;
                Print(s);

                // must return an error      
                exitCode = (int)eExitcodes.FileError;
            }

            // close interface
            CloseConnection();

#if DEBUGX
            Console.WriteLine(">>> DEBUG - Press any key to end...");
            Console.ReadKey();
#endif

            return exitCode;
        }

        private static void ApplyCommandlineToProjectConfig()
        {
            if (CommandlineArgs.ArgConnectionType != null)
                Project.Connection.ConnectionType = (Global.eConnectionType)CommandlineArgs.ArgConnectionType;
            if (CommandlineArgs.ArgPort != null)
                Project.Connection.SerialPort = CommandlineArgs.ArgPort;
            if (CommandlineArgs.ArgBaudrate != null)
                Project.Connection.SerialBaudrate = (int)CommandlineArgs.ArgBaudrate;
            if (CommandlineArgs.ArgParity != null)
                Project.Connection.SerialParity = (Parity)CommandlineArgs.ArgParity;
            if (CommandlineArgs.ArgEndofmessagetimeout != null)
                Project.Connection.SerialEndOfMessageTimeout = (int)CommandlineArgs.ArgEndofmessagetimeout;
            Project.Connection.SerialOneWireEchoSuppression = CommandlineArgs.ArgOnewireechosuppressionSwitch;
            if (CommandlineArgs.ArgIPAddress != null)
                Project.Connection.IPAddress = CommandlineArgs.ArgIPAddress;
            if (CommandlineArgs.ArgIPPort != null)
                Project.Connection.IPPort = (ushort)CommandlineArgs.ArgIPPort;
            if (CommandlineArgs.ArgSlaveID != null)
                Project.Connection.SlaveID = (byte)CommandlineArgs.ArgSlaveID;
            if (CommandlineArgs.ArgMaximumFrameSize != null)
                Project.Connection.MaximumFrameSize = (int)CommandlineArgs.ArgMaximumFrameSize;
            if (CommandlineArgs.ArgResponsetimeout != null)
                Project.Connection.ResponseTimeout = (int)CommandlineArgs.ArgResponsetimeout;
            if (CommandlineArgs.ArgInterframedelay != null)
                Project.Connection.InterFrameDelay = (int)CommandlineArgs.ArgInterframedelay;
            if (CommandlineArgs.ArgRetries != null)
                Project.Connection.Retries = (int)CommandlineArgs.ArgRetries;
            if (CommandlineArgs.ArgWriteHoldingregFunctioncode != null)
                Project.Connection.WriteHoldingregFunctioncode = (int)CommandlineArgs.ArgWriteHoldingregFunctioncode;
            if (CommandlineArgs.ArgInt32Endian != null)
                Project.Connection.Int32Endian = ConvertCommandlineEndianToEnum((int)CommandlineArgs.ArgInt32Endian);
            if (CommandlineArgs.ArgFloat32Endian != null)
                Project.Connection.Float32Endian = ConvertCommandlineEndianToEnum((int)CommandlineArgs.ArgFloat32Endian);
            if (CommandlineArgs.ArgStringEndian != null)
                Project.Connection.StringEndian = ConvertCommandlineEndianToEnum((int)CommandlineArgs.ArgStringEndian);
        }

        private static Global.eEndian ConvertCommandlineEndianToEnum(int end)
        {
            Global.eEndian e = Global.eEndian.Little;

            if (end == 0)
                e = Global.eEndian.Little;
            if (end == 1)
                e = Global.eEndian.Big;

            return e;
        }

        /// <summary>
        /// can throw
        /// </summary>
        private static void OpenConnection()
        {
            bool IsOpen = false;
            isSerialOpenPortError = false;

            // which interface/protocol to use?
            switch (Project.Connection.ConnectionType)
            {
                case Global.eConnectionType.SerialRTU:
                    isSerialOpenPortError = true;
                    IsOpen = OpenConnectionForSerialRtu();
                    break;
                case Global.eConnectionType.ModbusTcp:
                    IsOpen = OpenConnectionForModbusTcp();
                    break;
                case Global.eConnectionType.RtuOverTcp:
                    IsOpen = OpenConnectionForRtuOverTcp();
                    break;
                default:
                    throw new InvalidOperationException("Connection.ConnectionType has an invalid value");
            }
        }

        /// <summary>
        /// Open serial connection
        /// can throw
        /// </summary>
        private static bool OpenConnectionForSerialRtu()
        {
            // Open serial port, RTU protocol

            string Portname = Project.Connection.SerialPort;
            bool Opened = false;

            // got serial port name?
            if (string.IsNullOrEmpty(Portname))
            {
                throw new Exception("No serial port name.");
            }
            else
            {
                mb.OpenSerialRtu(Portname, (uint)Project.Connection.SerialBaudrate, Project.Connection.SerialParity, StopBits.One);
                Opened = true;
            }

            if (Opened)
            {
                // set this port as the project's serial port
                Project.Connection.SerialPort = Portname;

                // set parameters
                mb.MaximumFrameSize = Project.Connection.MaximumFrameSize;
                mb.WriteHoldingRegistersFunctioncode = Project.Connection.WriteHoldingregFunctioncode;
                mb.OneWireEchoSuppression = Project.Connection.SerialOneWireEchoSuppression;
                mb.ResponseTimeout = (uint)Project.Connection.ResponseTimeout;
                mb.EndOfMessageTimeout = (uint)Project.Connection.SerialEndOfMessageTimeout;
                mb.InterFrameDelay = (uint)Project.Connection.InterFrameDelay;
                mb.Retries = (uint)Project.Connection.Retries;

                // success
                return true;
            }

            return false;
        }

        /// <summary>
        /// Open a Modbus/TCP connection
        /// can throw
        /// </summary>
        private static bool OpenConnectionForModbusTcp()
        {
            // Open TCP connection, Modbus/TCP protocol

            string ConnectionName = Project.Connection.IPAddress + ":" + Project.Connection.IPPort.ToString();

            // valid IP address?
            System.Net.IPAddress TargetIP;
            if (System.Net.IPAddress.TryParse(Project.Connection.IPAddress, out TargetIP))
            {
                // ok
            }
            else
            {
                // error
                throw new Exception("Invalid IP address.");
            }

            // open
            mb.OpenModbusTcp(TargetIP, Project.Connection.IPPort);

            // ok
            mb.MaximumFrameSize = Project.Connection.MaximumFrameSize;
            mb.WriteHoldingRegistersFunctioncode = Project.Connection.WriteHoldingregFunctioncode;
            mb.ResponseTimeout = (uint)Project.Connection.ResponseTimeout;
            mb.InterFrameDelay = (uint)Project.Connection.InterFrameDelay;
            mb.Retries = (uint)Project.Connection.Retries;

            // success
            return true;
        }

        /// <summary>
        /// Open connection for RTU over TCP
        /// can throw
        /// </summary>
        private static bool OpenConnectionForRtuOverTcp()
        {
            // Open TCP connection, RTU protocol

            string ConnectionName = Project.Connection.IPAddress + ":" + Project.Connection.IPPort.ToString();

            // valid IP address?
            System.Net.IPAddress TargetIP;
            if (System.Net.IPAddress.TryParse(Project.Connection.IPAddress, out TargetIP))
            {
                // ok
            }
            else
            {
                throw new Exception("Invalid IP address.");
            }

            // open
            mb.OpenRtuOverTcp(TargetIP, Project.Connection.IPPort);

            // ok
            mb.MaximumFrameSize = Project.Connection.MaximumFrameSize;
            mb.WriteHoldingRegistersFunctioncode = Project.Connection.WriteHoldingregFunctioncode;
            mb.ResponseTimeout = (uint)Project.Connection.ResponseTimeout;
            mb.InterFrameDelay = (uint)Project.Connection.InterFrameDelay;
            mb.Retries = (uint)Project.Connection.Retries;

            // success
            return true;
        }

        public static string GetConnectionInfoString()
        {
            string result = "";

            switch (Project.Connection.ConnectionType)
            {
                case Global.eConnectionType.SerialRTU:
                    result = "SerialRtu: " + isempty(Project.Connection.SerialPort, "''")
                        + " " + Project.Connection.SerialBaudrate.ToString() + "baud"
                        + " " + Project.Connection.SerialNrOfBits.ToString()
                            + Project.Connection.SerialParity.ToString().Substring(0, 1)
                        + " slaveid=" + Project.Connection.SlaveID.ToString();
                    break;
                case Global.eConnectionType.ModbusTcp:
                    result = "ModbusTcp: " + isempty(Project.Connection.IPAddress, "''")
                        + ":" + Project.Connection.IPPort.ToString()
                        + " slaveid=" + Project.Connection.SlaveID.ToString();
                    break;
                case Global.eConnectionType.RtuOverTcp:
                    result = "RtuOverTcp: " + isempty(Project.Connection.IPAddress, "''")
                        + ":" + Project.Connection.IPPort.ToString()
                        + " slaveid=" + Project.Connection.SlaveID.ToString();
                    break;
                default:
                    result = "(Internal error: invalid Project.Connection.ConnectionType)";
                    break;
            }

            return result;
        }

        /// <summary>
        /// return alternative string if source string is empty
        /// </summary>
        public static string isempty(string s, string alt)
        {
            if (string.IsNullOrWhiteSpace(s))
                return alt;
            return s;
        }

        public static void CloseConnection()
        {
            // close interface
            if (mb.IsOpen)
            {
                try
                {
                    mb.Close();
                }
                catch (Exception)
                {
                    // ignore
                }
            }
        }

        /// <summary>
        /// Get a string that show which serial ports exists
        /// </summary>
        /// <returns></returns>
        private static string GetDetectedSerialPortsString()
        {
            string result = "";

            string[] array = SerialPort.GetPortNames();

            if (array.Length > 0)
            {
                result = string.Join(", ", array);
            }
            else
            {
                result = "(none)";
            }

            return result;
        }

        public static bool isnull(bool? v, bool repl)
        {
            if (v != null)
                return (bool)v;
            return repl;
        }

        public static string isnull(string v, string repl)
        {
            if (v != null)
                return v;
            return repl;
        }

        public static int isnull(int? v, int repl)
        {
            if (v != null)
                return (int)v;
            return repl;
        }

        public static void ReportProgressCallback(string s)
        {
            Print(s);
        }

        public static void Print(string s)
        {
            Console.WriteLine(s);

            if (logger != null)
            {
                logger.Log(s);
            }
        }
        public static void PrintVerbose(string s)
        {
            if (Verbose)
            {
                Print(s);
            }
        }
        public static void PrintLog(string s)
        {
            if (logger != null)
            {
                logger.Log(s);
            }
        }
    }
}
