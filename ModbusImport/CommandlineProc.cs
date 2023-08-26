using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Net;

// Handle command line arguments.
// may have filenames
// options start with - or / and have a value
// switches have no value

namespace ModbusImport
{
    class CommandlineProc
    {
        // properties

        // processed arguments
        // List of filenames
        public List<string> FileList = new List<string>();

        // processed options
        // switches (bool values) are null if not specified, true if present
        // other parameters are null if not specified 
        public eConnectionType? ArgConnectionType;
        public string ArgPort;                  // string is already nullable
        public int? ArgBaudrate;
        public Parity? ArgParity;
        public int? ArgEndofmessagetimeout;
        public bool? ArgOnewireechosuppression;
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
        public bool? ArgContinueOnError; 
        public bool? ArgVerbose;
        public bool? ArgHelp;

        public enum eConnectionType { SerialRTU = 0, ModbusTcp = 1, RtuOverTcp = 2 }

        public void Clear()
        {
            FileList = new List<string>();
            ArgConnectionType = null;
            ArgPort = null;
            ArgBaudrate = null;
            ArgParity = null;
            ArgEndofmessagetimeout = null;
            ArgOnewireechosuppression = null;
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
            ArgContinueOnError = null;
            ArgVerbose = null;
            ArgHelp = null;
        }

        /// <summary>
        /// Parse command line arguments
        /// Throw exception on error
        /// </summary>
        public void Load()
        {
            int ArgCnt = Environment.GetCommandLineArgs().Length;

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
                        string val = GetOptString(arg, ref i);
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
                            throw new ArgumentException("Invalid option value: " + arg + " " + val);
                        }
                    }
                    else if (optlow == "port")
                    {
                        ArgPort = GetOptString(arg, ref i);
                    }
                    else if (optlow == "baudrate")
                    {
                        ArgBaudrate = GetOptInt(arg, ref i, 50, Int32.MaxValue);
                    }
                    else if (optlow == "parity")
                    {
                        string val = GetOptString(arg, ref i);

                        Parity TmpParity;
                        if (Enum.TryParse<Parity>(val, true, out TmpParity))
                        {
                            ArgParity = TmpParity;
                        }
                        else
                        {
                            throw new ArgumentException("Invalid option value: " + arg + " " + val);
                        }
                    }
                    else if (optlow == "endofmessagetimeout")
                    {
                        ArgEndofmessagetimeout = GetOptInt(arg, ref i, 1, Int32.MaxValue);
                    }
                    else if (optlow == "onewireechosuppression")
                    {
                        ArgOnewireechosuppression = GetOptBool(arg, ref i);
                    }
                    else if (optlow == "ipaddress")
                    {
                        System.Net.IPAddress TmpIPaddress;
                        string val = GetOptString(arg, ref i);
                        if (IPAddress.TryParse(val, out TmpIPaddress))
                        {
                            // valid IP addres
                            ArgIPAddress = val;
                        }
                        else
                        {
                            throw new ArgumentException("Invalid option value: " + arg + " " + val);
                        }
                    }
                    else if (optlow == "ipport")
                    {
                        ArgIPPort = GetOptInt(arg, ref i, 1, 65535);
                    }
                    else if (optlow == "slaveid")
                    {
                        ArgSlaveID = GetOptInt(arg, ref i, 1, 255);
                    }
                    else if (optlow == "maxframe")
                    {
                        ArgMaxFrameSize = GetOptInt(arg, ref i, 13, 256);
                    }
                    else if (optlow == "responsetimeout")
                    {
                        ArgResponsetimeout = GetOptInt(arg, ref i, 1, Int32.MaxValue);
                    }
                    else if (optlow == "interframedelay")
                    {
                        ArgInterframedelay = GetOptInt(arg, ref i, 1, Int32.MaxValue);
                    }
                    else if (optlow == "writeregfc")
                    {
                        string val = GetOptString(arg, ref i);
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
                            throw new ArgumentException("Invalid option value: " + arg + " " + val);
                        }
                    }
                    else if (optlow == "int32endian")
                    {
                        ArgInt32Endian = GetOptEndian(arg, ref i);
                    }
                    else if (optlow == "float32endian")
                    {
                        ArgFloat32Endian = GetOptEndian(arg, ref i);
                    }
                    else if (optlow == "stringendian")
                    {
                        ArgStringEndian = GetOptEndian(arg, ref i);
                    }
                    else if (optlow == "cont")
                    {
                        ArgContinueOnError = GetOptBool(arg, ref i);
                    }
                    else if (optlow == "verbose")
                    {
                        ArgVerbose = GetOptBool(arg, ref i);
                    }
                    else if ((optlow == "?") || (optlow == "h") || (optlow == "help"))
                    {
                        ArgHelp = GetOptBool(arg, ref i);
                    }
                    else
                    {
                        throw new ArgumentException("Invalid option '" + arg + "'");
                    }
                }
                else
                {
                    // is file parameter
                    FileList.Add(arg.Trim());
                }
            }
        }

        /// <summary>
        /// Get an option value, type string
        /// </summary>
        /// <returns></returns>
        private string GetOptString(string optName, ref int i)
        {
            ++i;
            string val = ArgValue(ref i);
            if (val == null)
            {
                throw new ArgumentException("Option '" + optName + "' has no value!");
            }

            return val;
        }

        /// <summary>
        /// Get an option value, type int
        /// </summary>
        /// <returns></returns>
        private int GetOptInt(string optName, ref int i, int min, int max)
        {
            ++i;
            string val = ArgValue(ref i);
            if (val == null)
            {
                throw new ArgumentException("Option '" + optName + "' has no value!");
            }

            Int32 tmpValue;
            if (Int32.TryParse(val, out tmpValue) == false)
            {
                throw new ArgumentException("Option '" + optName + " " + val + "' invalid value!");
            }

            if ((tmpValue >= min) && (tmpValue <= max))
            {
                // OK
            }
            else
            {
                throw new ArgumentException("Option '" + optName + " " + val + "' value out of range!");
            }

            return tmpValue;
        }

        /// <summary>
        /// Get an option value, type boolean
        /// switch only, no true/false value is expected
        /// </summary>
        /// <returns></returns>
        private bool GetOptBool(string optName, ref int i)
        {
            // does not expect a value
            // switch presence means 'true'

            return true;
        }

        /// <summary>
        /// Get an option value, endian type 
        /// expects little or big
        /// </summary>
        /// <returns>0 for little, 1 for big</returns>
        private int GetOptEndian(string optName, ref int i)
        {
            int retval = 0;

            ++i;
            string val = ArgValue(ref i);
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
                throw new ArgumentException("Option '" + optName + " " + val + "' invalid value!");
            }

            return retval;
        }

        /// <summary>
        /// Get argument value
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        private string ArgValue(ref int i)
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

        public string GetOptionsUsageString()
        {
            return "Example: ModbusImport [Test1.mbc] Data1.csv -connection modbustcp -ipaddress 192.168.1.100" + Environment.NewLine
                 + "Use - or / for options." + Environment.NewLine
                 + "-connection <type> >> Connection type: serialrtu, modbustcp or rtutcp" + Environment.NewLine
                 + "-port <comport> >> Serial port name: COM1, …" + Environment.NewLine
                 + "-baudrate <19200> >> Serial baudrate" + Environment.NewLine
                 + "-parity <mode> >> Serial parity: odd, even, none, mark or space" + Environment.NewLine
                 + "-endofmessagetimeout ### >> Serial end-of-message timeout (ms)" + Environment.NewLine
                 + "-onewireechosuppression >> Ignore query command being echoed back " + Environment.NewLine
                 + "-ipaddress ##.##.##.## >> Slave/server IP address: 192.168.1.100" + Environment.NewLine
                 + "-ipport ### >> Slave/server IP port: 502" + Environment.NewLine
                 + "-slaveid ### >> Slave ID number: 1..255" + Environment.NewLine
                 + "-maxframe ### >> Maximum frame size: 13..256" + Environment.NewLine
                 + "-responsetimeout ### >> Max wait time for first byte of response (ms)" + Environment.NewLine
                 + "-interframedelay ### >> Delay before sending next query (ms)" + Environment.NewLine
                 + "-writeregfc <fc> >> Write holding registers function code: 6, 16 or auto" + Environment.NewLine
                 + "-int32endian ### >> 32-bit int word order: little, big" + Environment.NewLine
                 + "-float32endian ### >> 32-bit float word order: little, big" + Environment.NewLine
                 + "-stringendian ### >> String byte order: little, big" + Environment.NewLine
                 + "-cont >> Continue on error" + Environment.NewLine
                 + "-verbose >> Displays verbose output" + Environment.NewLine
                 + "-? or -h or -help >> Shows this help screen" + Environment.NewLine
                 +"Exit codes: 0 = No error, 1 = Commandline error, 2 = File error, 3 = Communication error";
        }
    }
}
