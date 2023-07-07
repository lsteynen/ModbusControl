using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Net;

// Handle command line arguments.
// may have filenames
// options start with - or / and have a value
// switches have no value
// Example:
// Modbuscontrl.exe -connection serialrtu -port COM1 -baudrate 19200 -parity even -EndofmessageTimeout  10 -onewireechosuppression -ipaddress 192.168.1.154 -ipport 503 -responsetimeout 3000 -Interframedelay 25 -samplerate 110 "C:\ED&A\Modbuscontrol - recompile\modbuscontrol\bin\Debug\Demo.mbc"

namespace ModbusControlSpace
{
    class CommandlineProc
    {
        // properties

        // processed arguments
        // List of filenames
        public List<string> FileList = new List<string>();

        // processed options
        // switches (bool values) are false if absent, true if present
        // other parameters are null if not specified 
        public eConnectionType? ArgConnectionType;
        public string ArgPort;                  // string is already nullable
        public int? ArgBaudrate;
        public Parity? ArgParity;
        public int? ArgEndofmessagetimeout;
        public bool ArgOnewireechosuppression;
        public string ArgIPAddress;             // string is already nullable
        public int? ArgIPPort;
        public int? ArgSlaveID;
        public int? ArgMaxFrameSize;
        public int? ArgResponsetimeout;
        public int? ArgInterframedelay;
        public int? ArgWriteholdingregFC;     // 6, 16, 0=auto
        public int? ArgSamplerate;
        public bool ArgHelp;

        public enum eConnectionType { SerialRTU = 0, ModbusTcp = 1, RtuOverTcp = 2 }

        public void Clear()
        {
            FileList = new List<string>();
            ArgConnectionType = null;
            ArgPort = null;
            ArgBaudrate = null;
            ArgParity = null;
            ArgEndofmessagetimeout = null;
            ArgOnewireechosuppression = false;
            ArgIPAddress = null;
            ArgIPPort = null;
            ArgSlaveID = null;
            ArgMaxFrameSize = null;
            ArgResponsetimeout = null;
            ArgInterframedelay = null;
            ArgWriteholdingregFC = null;
            ArgSamplerate = null;
            ArgHelp = false;
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
                    string optlower = opt.ToLower();

                    if (optlower == "connection")
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
                    else if (optlower == "port")
                    {
                        ArgPort = GetOptString(arg, ref i);
                    }
                    else if (optlower == "baudrate")
                    {
                        ArgBaudrate = GetOptInt(arg, ref i, 50, Int32.MaxValue);
                    }
                    else if (optlower == "parity")
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
                    else if (optlower == "endofmessagetimeout")
                    {
                        ArgEndofmessagetimeout = GetOptInt(arg, ref i, 1, Int32.MaxValue);
                    }
                    else if (optlower == "onewireechosuppression")
                    {
                        ArgOnewireechosuppression = GetOptBool(arg, ref i);
                    }
                    else if (optlower == "ipaddress")
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
                    else if (optlower == "ipport")
                    {
                        ArgIPPort = GetOptInt(arg, ref i, 1, 65535);
                    }
                    else if (optlower == "slaveid")
                    {
                        ArgSlaveID = GetOptInt(arg, ref i, 1, 255);
                    }
                    else if (optlower == "maxframesize")
                    {
                        ArgMaxFrameSize = GetOptInt(arg, ref i, 13, 256);
                    }                    
                    else if (optlower == "responsetimeout")
                    {
                        ArgResponsetimeout = GetOptInt(arg, ref i, 1, Int32.MaxValue);
                    }
                    else if (optlower == "interframedelay")
                    {
                        ArgInterframedelay = GetOptInt(arg, ref i, 1, Int32.MaxValue);
                    }
                    else if (optlower == "writeregfc")
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
                    else if (optlower == "samplerate")
                    {
                        ArgSamplerate = GetOptInt(arg, ref i, 0, Int32.MaxValue);
                    }
                    else if ((optlower == "?") || (optlower == "help"))
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
            return "Example: Modbuscontrol Test1.mbc -connection serialrtu -port COM1 -baudrate 19200" + Environment.NewLine
                 + "Use - or / for options." + Environment.NewLine
                 + "-connection <type> ➝ Connection type: serialrtu, modbustcp or rtutcp" + Environment.NewLine
                 + "-port <comport> ➝ Serial port name: COM1, …" + Environment.NewLine
                 + "-baudrate <19200> ➝ Serial baudrate" + Environment.NewLine
                 + "-parity <mode> ➝ Serial parity: odd, even, none, mark or space" + Environment.NewLine
                 + "-endofmessagetimeout ### ➝ Serial end-of-message timeout (ms)" + Environment.NewLine
                 + "-onewireechosuppression ➝ Ignore query command being echoed back " + Environment.NewLine
                 + "-ipaddress ##.##.##.## ➝ Slave/server IP address: 192.168.1.100" + Environment.NewLine
                 + "-ipport ### ➝ Slave/server IP port: 502" + Environment.NewLine
                 + "-slaveid ### ➝ Slave ID number: 1..255" + Environment.NewLine
                 + "-responsetimeout ### ➝ Max wait time for first byte of response (ms)" + Environment.NewLine
                 + "-interframedelay ### ➝ Delay before sending next query (ms)" + Environment.NewLine
                 + "-samplerate ### ➝ Registers polling speed (ms)" + Environment.NewLine
                 + "-? or -help ➝ Shows this help screen";
        }
    }
}
