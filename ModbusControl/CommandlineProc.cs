using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Net;

// Handle command line arguments.
// Example:
// Modbuscontrl.exe -connection serialrtu -port COM1 -baudrate 19200 -parity even -EndofmessageTimeout  10 -onewireechosuppression -ipaddress 192.168.1.154 -ipport 503 -responsetimeout 3000 -Interframedelay 25 -samplerate 110 "C:\ED&A\Modbuscontrol - recompile\modbuscontrol\bin\Debug\Demo.mbc"

namespace ModbusControlSpace
{
    class CommandlineProc
    {
        // properties

        // processed arguments
        // null if not specified on commandline

        public string File1;

        // options
        public eConnectionType? ArgConnectionType;
        public string ArgPort;                  // string is already nullable
        public int? ArgBaudrate;
        public Parity? ArgParity;
        public int? ArgEndofmessagetimeout;
        public bool? ArgOnewireechosuppression;
        public string ArgIPAddress;             // string is already nullable
        public int? ArgIPPort;
        public int? ArgSlaveID;
        public int? ArgResponsetimeout;
        public int? ArgInterframedelay;
        public int? ArgSamplerate;

        public bool IsAnyOverrideUsed
        {
            get { return GetIsAnyOverrideUsed(); }
        }

        public enum eConnectionType { SerialRTU = 0, ModbusTcp = 1, RtuOverTcp = 2 }

        public void Clear()
        {
            File1 = null;
            ArgConnectionType = null;
            ArgPort = null;
            ArgBaudrate = null;
            ArgParity = null;
            ArgEndofmessagetimeout = null;
            ArgOnewireechosuppression = null;
            ArgIPAddress = null;
            ArgIPPort = null;
            ArgSlaveID = null;
            ArgResponsetimeout = null;
            ArgInterframedelay = null;
            ArgSamplerate = null;
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
                if ((arg.Length >= 1) && (arg.Substring(0, 1) == "-"))
                {
                    string opt = arg.Substring(1);
                    string opt2 = opt.ToLower();

                    if (opt2 == "connection")
                    {
                        string val = GetOptString(arg, ref i);
                        string val2 = val.ToLower();
                        // use these string to avoid problems with / in command line
                        if (val2 == "serialrtu")
                        {
                            ArgConnectionType = eConnectionType.SerialRTU;
                        }
                        else if (val2 == "modbustcp")
                        {
                            ArgConnectionType = eConnectionType.ModbusTcp;
                        }
                        else if (val2 == "rtutcp")
                        {
                            ArgConnectionType = eConnectionType.RtuOverTcp;
                        }
                        else
                        {
                            throw new ArgumentException("Invalid switch value: " + arg + " " + val);
                        }
                    }
                    else if (opt2 == "port")
                    {
                        ArgPort = GetOptString(arg, ref i);
                    }
                    else if (opt2 == "baudrate")
                    {
                        ArgBaudrate = GetOptInt(arg, ref i, 50, Int32.MaxValue);
                    }
                    else if (opt2 == "parity")
                    {
                        string val = GetOptString(arg, ref i);

                        Parity TmpParity;
                        if (Enum.TryParse<Parity>(val, true, out TmpParity))
                        {
                            ArgParity = TmpParity;
                        }
                        else
                        {
                            throw new ArgumentException("Invalid switch value: " + arg + " " + val);
                        }
                    }
                    else if (opt2 == "endofmessagetimeout")
                    {
                        ArgEndofmessagetimeout = GetOptInt(arg, ref i, 1, Int32.MaxValue);
                    }
                    else if (opt2 == "onewireechosuppression")
                    {
                        ArgOnewireechosuppression = GetOptBool(arg, ref i);
                    }
                    else if (opt2 == "ipaddress")
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
                            throw new ArgumentException("Invalid switch value: " + arg + " " + val);
                        }
                    }
                    else if (opt2 == "ipport")
                    {
                        ArgIPPort = GetOptInt(arg, ref i, 1, 65535);
                    }
                    else if (opt2 == "slaveid")
                    {
                        ArgSlaveID = GetOptInt(arg, ref i, 1, 255);
                    }
                    else if (opt2 == "responsetimeout")
                    {
                        ArgResponsetimeout = GetOptInt(arg, ref i, 1, Int32.MaxValue);
                    }
                    else if (opt2 == "interframedelay")
                    {
                        ArgInterframedelay = GetOptInt(arg, ref i, 1, Int32.MaxValue);
                    }
                    else if (opt2 == "samplerate")
                    {
                        ArgSamplerate = GetOptInt(arg, ref i, 0, Int32.MaxValue);
                    }
                    else
                    {
                        throw new ArgumentException("Invalid switch '" + arg + "'");
                    }
                }
                else
                {
                    // is file parameter
                    if (File1 == null)
                    {
                        File1 = arg;
                    }
                    else
                    {
                        throw new ArgumentException("More than 1 filename was passed! ('" + arg + "')");
                    }
                }
            }
        }

        /// <summary>
        /// Check if overrides are used
        /// </summary>
        private bool GetIsAnyOverrideUsed()
        {
            return (ArgConnectionType != null)
                    || (ArgPort != null)
                    || (ArgBaudrate != null)
                    || (ArgParity != null)
                    || (ArgEndofmessagetimeout != null)
                    || (ArgOnewireechosuppression != null)
                    || (ArgIPAddress != null)
                    || (ArgIPPort != null)
                    || (ArgSlaveID != null)
                    || (ArgResponsetimeout != null)
                    || (ArgInterframedelay != null)
                    || (ArgSamplerate != null);
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
                throw new ArgumentException("Switch '" + optName + "' missing value!");
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
                throw new ArgumentException("Switch '" + optName + "' missing value!");
            }

            Int32 tmpValue;
            if (Int32.TryParse(val, out tmpValue) == false)
            {
                throw new ArgumentException("Switch '" + optName + " " + val + "' invalid value!");
            }

            if ((tmpValue >= min) && (tmpValue <= max))
            {
                // OK
            }
            else
            {
                throw new ArgumentException("Switch '" + optName + " " + val + "' value out of range!");
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
                if ((val.Length >= 1) && (val.Substring(0, 1) == "-"))
                {
                    // next is a switch
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
    }
}
