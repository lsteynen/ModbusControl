using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.IO.Pipes;

namespace Modbus_net
{
    public class NamedPipeChannel : ICommChannel
    {
        private NamedPipeClientStream mPort;
        private readonly string mPipeName;
        private uint mWriteTimeout;
        private uint mReadTimeout;

        public NamedPipeChannel(string pipeName)
        {
            mPipeName = pipeName;
            mPort = new NamedPipeClientStream(pipeName);
        }

        public void Open()
        {
            mPort.Connect();
        }

        public void Close()
        {
            mPort.Close();
        }

        public void Prep()
        {
            if (IsOpen)
            {
                mPort.Flush();
            }
        }

        public bool IsOpen
        {
            get
            {
                return mPort.IsConnected;
            }
        }

        public uint ConnectTimeout
        {
            // ConnectTimeout is not used here
            get
            {
                return 0;
            }
            set
            {
                // ignore
            }
        }

        public uint WriteTimeout
        {
            get
            {
                return mWriteTimeout;
            }
            set
            {
                mWriteTimeout = value;
            }
        }

        public uint ReadTimeout
        {
            get
            {
                return mReadTimeout;
            }
            set
            {
                mReadTimeout = value;
            }
        }

        public bool IsSerialChannel
        {
            get { return false; }
        }

        public bool IsTcpChannel
        {
            get { return true; }
        }

        public string ChannelName
        {
            get
            {
                return mPipeName;
            }
        }

        public uint CharacterTime(uint nrOfCharacters)
        {
            return (uint)Math.Ceiling(nrOfCharacters * 1000 * (0 + 8 + 1) / 2400.0);
        }

        public void Write(byte[] data, int start, int length)
        {
            mPort.Write(data, start, length);
        }

        public ushort Read(ref byte[] data, int start, int maxSize)
        {
            return (ushort)mPort.Read(data, start, maxSize);
        }
    }
}
