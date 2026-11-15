using System;
using System.Collections.Generic;
using System.Text;

namespace LST.Modbus
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
        int ReadWait(ref byte[] data, int count, bool useResponseTimeout);
        int GetAvailableCount();

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

        uint MoreTimeout
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
        //event LogCallbackString OnWarning;
    }
}
