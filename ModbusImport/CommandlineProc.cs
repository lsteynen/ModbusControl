using System;
using System.IO;
using System.IO.Ports;
using System.Collections.Generic;
using System.Net;
using System.Diagnostics;

// Handle command line arguments.
// may have filenames
// options start with - or / and have a value
// switches have no value

namespace ModbusImport
{
    class CommandlineProc
    {
        // properties
        // set version string for usage
        public string versionString = "";

        // processed arguments
        public string ArgMbcFile;                   // optional, empty if not given
        public string ArgCsvFile;                   // required, empty if not given

        // processed options
        // switches (bool values) are null if not specified, true if present
        // other parameters are null if not specified 
        public eConnectionType? ArgConnectionType;
        public string ArgPort;                  // string is already nullable
        public int? ArgBaudrate;
        public Parity? ArgParity;
        public int? ArgEndofmessagetimeout;
        public bool ArgOnewireechosuppressionSwitch;
        public string ArgIPAddress;             // string is already nullable
        public int? ArgIPPort;
        public int? ArgSlaveID;
        public int? ArgMaxFrameSize;
        public int? ArgResponsetimeout;
        public int? ArgInterframedelay;
        public int? ArgWriteholdingregFC;       // 6, 16, 0=auto 
        public int? ArgInt32Endian;             // 0=little, 1=big
        public int? ArgFloat32Endian;           // 0=little, 1=big
        public int? ArgStringEndian;            // 0=little, 1=big
        //public string ArgExportFormat;          // ""=default, I=int, U=UInt, H=Hex
        //public string ArgStringExportFormat;    // ""=ASCII, I=Int, U=UInt, H=Hex
        public List<string> ArgTabsList;        // List of tab names
        public string ArgLogFile;               // logfile
        public bool ArgLogTimeSwitch;           // put timestamps in log    
        public bool ArgContinueOnErrorSwitch;
        public bool ArgVerboseSwitch;
        public bool ArgHelpSwitch;

        public enum eConnectionType { SerialRTU = 0, ModbusTcp = 1, RtuOverTcp = 2 }

        public void Clear()
        {
            ArgMbcFile = null;
            ArgCsvFile = null;
            ArgConnectionType = null;
            ArgPort = null;
            ArgBaudrate = null;
            ArgParity = null;
            ArgEndofmessagetimeout = null;
            ArgOnewireechosuppressionSwitch = false;
            ArgIPAddress = null;
            ArgIPPort = null;
            ArgSlaveID = null;
            ArgMaxFrameSize = null;
            ArgResponsetimeout = null;
            ArgInterframedelay = null;
            ArgWriteholdingregFC = null;
            ArgInt32Endian = null;
            ArgFloat32Endian = null;
            ArgStringEndian = null;
            //ArgExportFormat = null;
            //ArgStringExportFormat = null;
            ArgLogFile = null;
            ArgLogTimeSwitch = false;
            ArgContinueOnErrorSwitch = false;
            ArgVerboseSwitch = false;
            ArgHelpSwitch = false;
        }

        /// <summary>
        /// Parse command line arguments
        /// Throw exception on error
        /// Usage:
        /// ModbusImport [Project.mbc] Data.csv [options]
        /// </summary>
        public void Load()
        {
            int ArgCnt = Environment.GetCommandLineArgs().Length;

            /*
            for (int i = 1; i < ArgCnt; ++i)
            {
                Debug.Print("Arg " + i.ToString() + ": " + Environment.GetCommandLineArgs()[i]);
            }
            */

            for (int i = 1; i < ArgCnt; ++i)
            {
                string arg = Environment.GetCommandLineArgs()[i];

                // is option?
                if (IsOption(arg))
                {
                    string opt = arg.Substring(1);
                    string optlow = opt.ToLower();

                    if (optlow == "connection")
                    {
                        ++i;
                        string val = GetOptString(arg, i);
                        string vallow = val.ToLower();
                        // use these string to avoid problems with / in command line
                        if (vallow == "serialrtu")
                        {
                            ArgConnectionType = eConnectionType.SerialRTU;
                        }
                        else if (vallow == "modbustcp")
                        {
                            ArgConnectionType = eConnectionType.ModbusTcp;
                        }
                        else if (vallow == "rtutcp")
                        {
                            ArgConnectionType = eConnectionType.RtuOverTcp;
                        }
                        else
                        {
                            throw new ArgumentException("Option has invalid value: " + arg + " " + val);
                        }
                    }
                    else if (optlow == "port")
                    {
                        ++i;
                        ArgPort = GetOptString(arg, i);
                    }
                    else if (optlow == "baudrate")
                    {
                        ++i;
                        ArgBaudrate = GetOptInt(arg, i, 50, Int32.MaxValue);
                    }
                    else if (optlow == "parity")
                    {
                        ++i;
                        string val = GetOptString(arg, i);

                        Parity TmpParity;
                        if (Enum.TryParse<Parity>(val, true, out TmpParity))
                        {
                            ArgParity = TmpParity;
                        }
                        else
                        {
                            throw new ArgumentException("Option has invalid value: " + arg + " " + val);
                        }
                    }
                    else if (optlow == "endofmessagetimeout")
                    {
                        ++i;
                        ArgEndofmessagetimeout = GetOptInt(arg, i, 1, Int32.MaxValue);
                    }
                    else if (optlow == "onewire")
                    {
                        ArgOnewireechosuppressionSwitch = true;
                    }
                    else if (optlow == "ipaddress")
                    {
                        ++i;
                        System.Net.IPAddress TmpIPaddress;
                        string val = GetOptString(arg, i);
                        if (IPAddress.TryParse(val, out TmpIPaddress))
                        {
                            // valid IP addres
                            ArgIPAddress = val;
                        }
                        else
                        {
                            throw new ArgumentException("Option has invalid value: " + arg + " " + val);
                        }
                    }
                    else if (optlow == "ipport")
                    {
                        ++i;
                        ArgIPPort = GetOptInt(arg, i, 1, 65535);
                    }
                    else if (optlow == "slaveid")
                    {
                        ++i;
                        ArgSlaveID = GetOptInt(arg, i, 1, 255);
                    }
                    else if (optlow == "maxframe")
                    {
                        ++i;
                        ArgMaxFrameSize = GetOptInt(arg, i, 13, 256);
                    }
                    else if (optlow == "responsetimeout")
                    {
                        ++i;
                        ArgResponsetimeout = GetOptInt(arg, i, 1, Int32.MaxValue);
                    }
                    else if (optlow == "interframedelay")
                    {
                        ++i;
                        ArgInterframedelay = GetOptInt(arg, i, 1, Int32.MaxValue);
                    }
                    else if (optlow == "writeregfc")
                    {
                        ++i;
                        string val = GetOptString(arg, i);
                        string vallow = val.ToLower();
                        if ((vallow == "6") || (vallow == "06"))
                        {
                            ArgWriteholdingregFC = 6;
                        }
                        else
                        if (vallow == "16")
                        {
                            ArgWriteholdingregFC = 16;
                        }
                        else
                        if (vallow == "auto")
                        {
                            ArgWriteholdingregFC = 0;
                        }
                        else
                        {
                            throw new ArgumentException("Option has invalid value: " + arg + " " + val);
                        }
                    }
                    else if (optlow == "int32endian")
                    {
                        ++i;
                        ArgInt32Endian = GetOptEndian(arg, i);
                    }
                    else if (optlow == "float32endian")
                    {
                        ++i;
                        ArgFloat32Endian = GetOptEndian(arg, i);
                    }
                    else if (optlow == "stringendian")
                    {
                        ++i;
                        ArgStringEndian = GetOptEndian(arg, i);
                    }
                    else if (optlow == "tabs")
                    {
                        // pass ref i, variable number of values
                        ++i;
                        ArgTabsList = GetOptStringList(arg, ref i);
                    }
                    else if (optlow == "log")
                    {
                        ++i;
                        string tmpFile = GetOptString(arg, i);
                        if (tmpFile != "")
                        {
                            ArgLogFile = tmpFile;
                        }
                    }
                    else if (optlow == "logtime")
                    {
                        ArgLogTimeSwitch = true;
                    }
                    else if (optlow == "cont")
                    {
                        ArgContinueOnErrorSwitch = true;
                    }
                    else if (optlow == "verbose")
                    {
                        ArgVerboseSwitch = true;
                    }
                    else if ((optlow == "?") || (optlow == "h") || (optlow == "help"))
                    {
                        ArgHelpSwitch = true;
                    }
                    else
                    {
                        throw new ArgumentException("Invalid option: " + arg);
                    }
                }
                else
                {
                    // assume file parameter
                    string file = arg;
                    string fext = Path.GetExtension(file);
                    if (fext.StartsWith(".mbc", StringComparison.CurrentCultureIgnoreCase))
                    {
                        // mbc project file, 1
                        if (string.IsNullOrEmpty(ArgMbcFile))
                        {
                            ArgMbcFile = file.Trim();
                        }
                        else
                        {
                            throw new ArgumentException("More than 1 mbc file was passed: '" + file + "'");
                        }
                    }
                    else
                    {
                        // not mbc, assume csv, 1
                        if (string.IsNullOrEmpty(ArgCsvFile))
                        {
                            ArgCsvFile = file.Trim();
                        }
                        else
                        {
                            throw new ArgumentException("More than 1 CSV file was passed: '" + file + "'");
                        }
                    }
                }
            }

            // do not show these errors if -help is passed
            if (ArgHelpSwitch == false)
            {
            /*
                // mbc required
                if (string.IsNullOrEmpty(ArgMbcFile))
                {
                    throw new ArgumentException("mbc filename is missing");
                }
            */

                // csv required
                if (string.IsNullOrEmpty(ArgCsvFile))
                {
                    throw new ArgumentException("CSV filename is missing");
                }
            }
        }

        /// <summary>
        /// Get an option value, type string
        /// </summary>
        /// <returns></returns>
        private string GetOptString(string optName, int i)
        {
            string val = ArgValue(i);
            if (val == null)
            {
                throw new ArgumentException("Option has no value: " + optName);
            }

            return val;
        }

        /// <summary>
        /// Get an option value, type list of strings.
        /// Pass ref i, variable number of values
        /// </summary>
        /// <returns>List of strings</returns>
        private List<string> GetOptStringList(string optName, ref int i)
        {
            List<string> list = new List<string>();

            string val = ArgValue(i);
            if (val == null)
            {
                throw new ArgumentException("Option has no value: " + optName);
            }

            list.Add(val);

            while ((val = ArgValue(i+1)) != null)
            {
                list.Add(val);
                ++i;
            }

            return list;
        }

        /// <summary>
        /// Get an option value, type int
        /// </summary>
        /// <returns></returns>
        private int GetOptInt(string optName, int i, int min, int max)
        {
            string val = ArgValue(i);
            if (val == null)
            {
                throw new ArgumentException("Option has no value: " + optName);
            }

            Int32 tmpValue;
            if (Int32.TryParse(val, out tmpValue) == false)
            {
                throw new ArgumentException("Option has invalid value: " + optName + " " + val);
            }

            if ((tmpValue >= min) && (tmpValue <= max))
            {
                // OK
            }
            else
            {
                throw new ArgumentException("Option has invalid value: " + optName + " " + val);
            }

            return tmpValue;
        }

        /// <summary>
        /// Get an option value, endian type 
        /// expects little or big
        /// </summary>
        /// <returns>0 for little, 1 for big</returns>
        private int GetOptEndian(string optName, int i)
        {
            int retval = 0;

            string val = ArgValue(i);
            string vallow = val.ToLower();
            if (vallow == "little")
            {
                retval = 0;
            }
            else if (vallow == "big")
            {
                retval = 1;
            }
            else
            {
                throw new ArgumentException("Option has invalid value: " + optName + " " + val);
            }

            return retval;
        }

        /// <summary>
        /// Get argument value
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        private string ArgValue(int i)
        {
            // any left?
            if (i < Environment.GetCommandLineArgs().Length)
            {
                string val = Environment.GetCommandLineArgs()[i];
                if (IsOption(val))
                {
                    // next is an option
                    return null;
                }
                else
                {
                    return val;
                }
            }

            // no left
            return null;
        }

        private bool IsOption(string s)
        {
            if (s.Length >= 1)
            {
                string f = s.Substring(0, 1);
                if ((f == "-") || (f == "/"))
                {
                    return true;
                }
            }

            return false;
        }

        public string GetUsageString()
        {
            return "ModbusImport.exe " + versionString + Environment.NewLine
                 + "Tool to import registers from a CSV file into a modbus device." + Environment.NewLine
                 + "Usage: ModbusImport [Project.mbc] Data.csv [options]" + Environment.NewLine
                 + "Use - or / for options:" + Environment.NewLine
                 + "-connection <type>              Connection type: serialrtu, modbustcp or rtutcp" + Environment.NewLine
                 + "-port <name>                    Serial port name: COM1,..." + Environment.NewLine
                 + "-baudrate ###                   Serial baudrate" + Environment.NewLine
                 + "-parity <mode>                  Serial parity: odd, even, none, mark or space" + Environment.NewLine
                 + "-endofmessagetimeout ###        Serial end-of-message timeout (ms)" + Environment.NewLine
                 + "-onewire                        1-wire operation, ignore query/command echo" + Environment.NewLine
                 + "-ipaddress <address>            Slave/server IP address: like 192.168.1.100" + Environment.NewLine
                 + "-ipport ###                     Slave/server IP port: usually 502" + Environment.NewLine
                 + "-slaveid ###                    Slave ID number: 1-255" + Environment.NewLine
                 + "-maxframe ###                   Maximum frame size: 13-256 bytes" + Environment.NewLine
                 + "-responsetimeout ###            Max wait time for first byte of response (ms)" + Environment.NewLine
                 + "-interframedelay ###            Delay before sending next query (ms)" + Environment.NewLine
                 + "-writeregfc <mode>              Write holding registers function code: 6, 16 or auto" + Environment.NewLine
                 + "-int32endian <endian>           32-bit int word order: little, big" + Environment.NewLine
                 + "-float32endian <endian>         32-bit float word order: little, big" + Environment.NewLine
                 + "-stringendian <endian>          String byte order: little, big" + Environment.NewLine
                 + "-tabs <tabname> [tabname]...    Names of tab pages to import, in this order" + Environment.NewLine
                 + "-log <file/path>                Append output to a log file" + Environment.NewLine
                 + "-logtime                        Add timestamps to the log file" + Environment.NewLine
                 + "-cont                           Continue on error" + Environment.NewLine
                 + "-verbose                        Display verbose output" + Environment.NewLine
                 + "-? or -h or -help               Show this help screen" + Environment.NewLine
                 + "Exit codes: 0 = No error, 1 = Commandline error, 2 = File error, 3 = Communication error";
        }
    }
}
