using System;
using System.Collections.Generic;
using System.Text;

namespace LST.Modbus
{
    public delegate void LogCallbackString(string s);

    public interface IModbusProtocol
    {
        // properties

        ICommChannel Channel { get; set; }
        bool OneWireEchoSuppression { get; set; } // not used here
        uint ResponseTimeout { get; set; }
        uint EndOfMessageTimeout { get; set; }
        uint InterFrameDelay { get; set; }
        uint ConnectionClosedBecauseOfResponseErrorCount { get; set; }

        // methods
        
        ModbusProtocol.Result ProtocolSendReceive( byte[] query, int querylength, out byte[] response, int expectedresponselength);

        // events
#pragma warning disable 67
        event LogCallbackString OnInfo;
        event LogCallbackString OnError;
#pragma warning restore 67
    }
}
