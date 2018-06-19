using System;
using System.Collections.Generic;
using System.Text;

namespace Modbus_net
{
    public delegate void TxRxCallbackHandler(byte[] data, int count);

    public interface ICommChannel
    {
        // methods

        void Open();
        void Close();
        bool BeforeTransaction();
        void AfterTransaction();

        void Write(byte[] data, int start, int length);
        int Read(ref byte[] data, int start, int maxSize);
        int ReadWait(ref byte[] data, int count);

        // properties

        bool IsOpen
        {
            get;
        }

        bool IsSerialChannel
        {
            get;
        }

        bool IsTcpChannel
        {
            get;
        }

        string ChannelName
        {
            get;
        }

        uint ConnectTimeout
        {
            get;
            set;
        }

        uint ResponseTimeout
        {   
            get; 
            set; 
        }

        uint CharacterTimeout
        {   
            get; 
            set; 
        }

        uint Baudrate
        {
            // read only
            get;
        }

        uint ConnectCount
        {
            get;
            set;
        }

        uint ConnectFailCount
        {
            get;
            set;
        }

        uint ConnectionLostCount
        {
            get;
            set;
        }

        event TxRxCallbackHandler OnDataSent;
        event TxRxCallbackHandler OnDataReceived;
        event LogCallbackString OnWarning;
    }
}
