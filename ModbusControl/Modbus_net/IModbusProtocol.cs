using System;
using System.Collections.Generic;
using System.Text;

namespace Modbus_net
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
        event LogCallbackString OnInfo;
        event LogCallbackString OnError;
    }
}
