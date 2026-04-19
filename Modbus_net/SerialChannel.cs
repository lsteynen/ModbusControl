using System;
using System.Collections.Generic;
using System.Text;
using System.IO.Ports;
using System.Threading;
using System.Diagnostics;

using LST.Helpers;

namespace LST.Modbus
{
    public class SerialChannel : ICommChannel
    {
        private SerialPort mPort;
        private Thread ReceiveThread;
        private bool ReceiveThread_Stop;
        private ByteQueue RxQueue = new ByteQueue(300);

        // constructor
        public SerialChannel(string portName, uint baud, uint dataBits, Parity parity, StopBits stopBits)
        {
            mPort = new SerialPort();

            mPort.PortName = portName;
            mPort.BaudRate = (int)baud;
            mPort.DataBits = (int)dataBits;
            mPort.Parity = parity;
            mPort.StopBits = stopBits;
        }

        // destructor
        ~SerialChannel()
        {
            Close();
        }

        // properties

        public string ChannelName
        {
            // read only
            get { return mPort.PortName; }
        }

        public bool IsOpen
        {
            get { return mPort.IsOpen; }
        }

        public bool IsSerialChannel
        {
            get { return true; }
        }

        public bool IsTcpChannel
        {
            get { return false; }
        }

        public uint ConnectTimeout
        {
            // is not used here
            get { return 0; }
            set { /* ignore */ }
        }

        public uint ResponseTimeout
        {   
            get; 
            set; 
        }

        public uint CharacterTimeout
        {
            get;
            set;
        }

        public uint Baudrate
        {
            // read only
            get { return (uint)mPort.BaudRate; }
        }

        public uint ConnectCount
        {
            // not used here but must be implemented
            get;
            set;
        }

        public uint ConnectFailCount
        {
            // not used here but must be implemented
            get;
            set;
        }

        public uint ConnectionLostCount
        {
            // not used here but must be implemented
            get;
            set;
        }

        // events

        public event TxRxCallbackHandler OnDataSent;
        public event TxRxCallbackHandler OnDataReceived;
        public event LogCallbackString OnWarning;

        // methods

        public void Open()
        {
            // can throw exception!

            mPort.Open();

            // Start receive thread
            ReceiveThread = new Thread(ReceiveThread_Work);
            ReceiveThread_Stop = false;
            ReceiveThread.Start();

            // clear receive queue
            RxQueue.Clear();
        }

        public void Close()
        {
            // stop backgroundworker if it runs
            if (ReceiveThread != null)
            {
                ReceiveThread_Stop = true;

                // wait for thread to end
                ReceiveThread.Join();
                ReceiveThread = null;
            }

            mPort.Close();
            RxQueue.Clear();
        }

        public bool BeforeTransaction()
        {
            if (IsOpen)
            {
                // if there's bytes in the rx queue: log this and discard them
                if (RxQueue.Count > 0)
                {
                    byte[] garbage = RxQueue.GetAll();
                    if (OnWarning != null)
                    {
                        string s = "Received " + garbage.Length + " unexpected bytes: " + BytesToString.Convert(garbage, garbage.Length) + ", discarded";
                        OnWarning(s);
                    }
                }
            }

            // if channel not open: no transactions
            return IsOpen;
        }

        public void AfterTransaction()
        {
            // if we received more bytes than we expected: log this and discard them
            if (RxQueue.Count > 0)
            {
                byte[] garbage = RxQueue.GetAll();
                if (OnWarning != null)
                {
                    string s = "Received " + garbage.Length + " unexpected bytes: " + BytesToString.Convert(garbage, garbage.Length) + ", discarded";
                    OnWarning(s);
                }
            }
        }

        public void Write(byte[] data, int start, int length)
        {
            mPort.Write(data, start, length);

            // log send data
            if (OnDataSent != null)
            {
                OnDataSent(data, length);
            }
        }

        public int Read(ref byte[] buffer, int start, int maxcount)
        {
            return RxQueue.Get(ref buffer, start, maxcount);
        }

        public int ReadWait(ref byte[] buffer, int count)
        {
            Debug.Print(DateTime.Now.TimeOfDay + ": SerialChannel.ReadWait() enter");

            long lastRxTime = DateTime.UtcNow.Ticks;
            uint timeout = ResponseTimeout;
            int prevCount = 0;

            // wait till we received all bytes, or timeout
            while (RxQueue.Count < count)
            {
                // received more bytes?
                if (RxQueue.Count > prevCount)
                {
                    // switch to character timeout
                    timeout = CharacterTimeout;
                    // remember when we received these bytes
                    lastRxTime = DateTime.UtcNow.Ticks;
                    prevCount = RxQueue.Count;

                    Debug.Print(DateTime.Now.TimeOfDay + ": SerialChannel.ReadWait(): loop, got " + prevCount + " bytes");
                }
                else
                {
                    // timeout?
                    if (((DateTime.UtcNow.Ticks - lastRxTime) / TimeSpan.TicksPerMillisecond) >= timeout)
                    {
                        // timeout, return the bytes we received till now
                        count = RxQueue.Count;

                        Debug.Print(DateTime.Now.TimeOfDay + ": SerialChannel.ReadWait(): loop timeout, got " + count + " bytes");
                    }
                    else
                    {
                        // wait for more bytes
                        Thread.Sleep(10);
                    }
                }
            }

            Debug.Print(DateTime.Now.TimeOfDay + ": SerialChannel.ReadWait(): exit, got " + count + " bytes");

            // copy the bytes we received
            RxQueue.Get( ref buffer, 0, count);

            return count;
        }

        private void ReceiveThread_Work()
        {
            byte[] buffer = new byte[256];

            // wait for incoming data
            do
            {
                try
                {
                    if (mPort.BytesToRead > 0)
                    {
                        int count = mPort.Read(buffer, 0, 256);
                        if (count > 0)
                        {
                            // add to queue
                            RxQueue.Add(buffer, count);

                            // log received data
                            if (OnDataReceived != null)
                            {
                                OnDataReceived(buffer, count);
                            }
                        }
                    }
                }
                catch (Exception)
                {
                }

                Thread.Sleep(10);
            }
            while (ReceiveThread_Stop == false);
        }

    }
}
