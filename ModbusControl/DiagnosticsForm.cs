using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ModbusControl
{
    public partial class DiagnosticsForm : Form
    {
        public event ResetHandler OnReset;
        public delegate void ResetHandler(object sender);

        private uint mRwSuccessCount;
        private uint mOneWireEchoErrorCount;
        private uint mNoResponseCount;
        private uint mResponseProtocolErrorCount;
        private uint mResponseShortAndCrcErrorCount;
        private uint mResponseCrcErrorCount;
        private uint mResponseMBAPheaderErrorCount;
        private uint mResponseDataErrorCount;
        private uint mTcpConnectCount;
        private uint mTcpConnectFailCount;
        private uint mTcpConnectionLostCount;
        private uint mTcpConnectionClosedOnErrorCount;
        private uint mIllegalFunctionExceptionCount;
        private uint mIllegalAddressExceptionCount;
        private uint mIllegalDataExceptionCount;
        private uint mGatewayPathUnavailableExceptionCount;
        private uint mGatewayTargetFailedToRespondExceptionCount;
        private uint mOtherExceptionCount;
        private uint mBridgeConnectsCount;
        private uint mBridgeOpenConnections;
        private uint mBridgeQueriesReceivedCount;
        private uint mBridgeErrorsCount;

        #region Properties

        public uint RwSuccessCount
        {
            set
            {
                if (value != mRwSuccessCount)
                {
                    mRwSuccessCount = value;
                    lRwSuccessCount.Text = mRwSuccessCount.ToString();
                }
            }
        }

        public uint OneWireEchoErrorCount
        {
            set
            {
                if (value != mOneWireEchoErrorCount)
                {
                    mOneWireEchoErrorCount = value;
                    lOnewireEchoErrorCount.Text = mOneWireEchoErrorCount.ToString();
                }
            }
        }

        public uint NoResponseCount
        {
            set
            {
                if (value != mNoResponseCount)
                {
                    mNoResponseCount = value;
                    lNoResponseCount.Text = mNoResponseCount.ToString();
                }
            }
        }

        public uint ResponseProtocolErrorCount
        {
            set
            {
                if (value != mResponseProtocolErrorCount)
                {
                    mResponseProtocolErrorCount = value;
                    lResponseProtocolErrorCount.Text = mResponseProtocolErrorCount.ToString();
                }
            }
        }

        public uint ResponseShortAndCrcErrorCount
        {
            set
            {
                if (value != mResponseShortAndCrcErrorCount)
                {
                    mResponseShortAndCrcErrorCount = value;
                    lResponseShortAndCrcErrorCount.Text = mResponseShortAndCrcErrorCount.ToString();
                }
            }
        }

        public uint ResponseCrcErrorCount
        {
            set
            {
                if (value != mResponseCrcErrorCount)
                {
                    mResponseCrcErrorCount = value;
                    lResponseCrcErrorCount.Text = mResponseCrcErrorCount.ToString();
                }
            }
        }

        public uint ResponseMBAPheaderErrorCount
        {
            set
            {
                if (value != mResponseMBAPheaderErrorCount)
                {
                    mResponseMBAPheaderErrorCount = value;
                    lRespMbapErrorCount.Text = mResponseMBAPheaderErrorCount.ToString();
                }
            }
        }

        public uint ResponseDataErrorCount
        {
            set
            {
                if (value != mResponseDataErrorCount)
                {
                    mResponseDataErrorCount = value;
                    lResponseDataErrorCount.Text = mResponseDataErrorCount.ToString();
                }
            }
        }

        public uint TcpConnectCount
        {
            set
            {
                if (value != mTcpConnectCount)
                {
                    mTcpConnectCount = value;
                    lTcpConnectCount.Text = mTcpConnectCount.ToString();
                }
            }
        }

        public uint TcpConnectFailCount
        {
            set
            {
                if (value != mTcpConnectFailCount)
                {
                    mTcpConnectFailCount = value;
                    lTcpConnectFailCount.Text = mTcpConnectFailCount.ToString();
                }
            }
        }

        public uint TcpConnectionLostCount
        {
            set
            {
                if (value != mTcpConnectionLostCount)
                {
                    mTcpConnectionLostCount = value;
                    lTcpConnectionLostCount.Text = mTcpConnectionLostCount.ToString();
                }
            }
        }

        public uint TcpConnectionClosedOnErrorCount
        {
            set
            {
                if (value != mTcpConnectionClosedOnErrorCount)
                {
                    mTcpConnectionClosedOnErrorCount = value;
                    lTcpClosedOnErrorCount.Text = mTcpConnectionClosedOnErrorCount.ToString();
                }
            }
        }

        public uint IllegalFunctionExceptionCount
        {
            set
            {
                if (value != mIllegalFunctionExceptionCount)
                {
                    mIllegalFunctionExceptionCount = value;
                    lIllegalFunctionExceptionCount.Text = mIllegalFunctionExceptionCount.ToString();
                }
            }
        }

        public uint IllegalAddressExceptionCount
        {
            set
            {
                if (value != mIllegalAddressExceptionCount)
                {
                    mIllegalAddressExceptionCount = value;
                    lIllegalAddressExceptionCount.Text = mIllegalAddressExceptionCount.ToString();
                }
            }
        }

        public uint IllegalDataExceptionCount
        {
            set
            {
                if (value != mIllegalDataExceptionCount)
                {
                    mIllegalDataExceptionCount = value;
                    lIllegalDataExceptionCount.Text = mIllegalDataExceptionCount.ToString();
                }
            }
        }

        public uint GatewayPathUnavailableExceptionCount
        {
            set
            {
                if (value != mGatewayPathUnavailableExceptionCount)
                {
                    mGatewayPathUnavailableExceptionCount = value;
                    lGatewayPathUnavailableExceptionCount.Text = mGatewayPathUnavailableExceptionCount.ToString();
                }
            }
        }

        public uint GatewayTargetFailedToRespondExceptionCount
        {
            set
            {
                if (value != mGatewayTargetFailedToRespondExceptionCount)
                {
                    mGatewayTargetFailedToRespondExceptionCount = value;
                    lGatewayDeviceFailedToRespondExceptionCount.Text = mGatewayTargetFailedToRespondExceptionCount.ToString();
                }
            }
        }

        public uint OtherExceptionCount
        {
            set
            {
                if (value != mOtherExceptionCount)
                {
                    mOtherExceptionCount = value;
                    lOtherExceptionCount.Text = mOtherExceptionCount.ToString();
                }
            }
        }

        public uint BridgeConnectsCount
        {
            set
            {
                if (value != mBridgeConnectsCount)
                {
                    mBridgeConnectsCount = value;
                    lBridgeConnectCount.Text = mBridgeConnectsCount.ToString();
                }
            }
        }

        public uint BridgeOpenConnections
        {
            set
            {
                if (value != mBridgeOpenConnections)
                {
                    mBridgeOpenConnections = value;
                    lBridgeOpenConnections.Text = mBridgeOpenConnections.ToString();
                }
            }
        }

        public uint BridgeQueriesReceivedCount
        {
            set
            {
                if (value != mBridgeQueriesReceivedCount)
                {
                    mBridgeQueriesReceivedCount = value;
                    lBridgeQueriesReceivedCount.Text = mBridgeQueriesReceivedCount.ToString();
                }
            }
        }

        public uint BridgeErrorsCount
        {
            set
            {
                if (value != mBridgeErrorsCount)
                {
                    mBridgeErrorsCount = value;
                    lBridgeErrorsCount.Text = mBridgeErrorsCount.ToString();
                }
            }
        }

        #endregion

        public DiagnosticsForm()
        {
            InitializeComponent();
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            // event 
            if (OnReset != null)
            {
                OnReset(this);
            }
        }

    }
}
