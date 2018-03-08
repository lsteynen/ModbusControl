using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

using ModbusRegViewSpace;
using Modbus_net;

namespace ModbusControlSpace
{
    public partial class CustomRead : Form
    {
        Form1 mParent;
        public ModbusInterface mModbus;

        // constructor
        public CustomRead(Form1 parent)
        {
            InitializeComponent();

            mParent = parent;
            mModbus = parent.mb;
        }

        #region properties

        public byte SlaveID
        {
            get
            {
                return (byte)numericSlaveAddress.Value;
            }
            set
            {
                numericSlaveAddress.Value = value;
            }
        }

        public UInt32 Register
        {
            get
            {
                return (UInt32)numericRegister.Value;
            }
            set
            {
                numericRegister.Value = value;
            }
        }

        public UInt16 Quantity
        {
            get
            {
                return (UInt16)numericQuantity.Value;
            }
            set
            {
                numericQuantity.Value = value;
            }
        }

        public bool ShortLivedTcpConnection
        { get; set; }

        #endregion

        private void OnFormClosing(object sender, FormClosingEventArgs e)
        {
            // Do not close, hide
            this.Hide();
            e.Cancel = true; // this cancels the close event.

            // main form will restore the polling
        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            byte SlaveID;
            UInt32 Register;
            UInt16 RegOffset;
            UInt16 RegQuantity;

            SlaveID = (byte)numericSlaveAddress.Value;

            Register = (UInt32)numericRegister.Value;
            if ( mParent.TryConvertUniregToOffset(Register, out RegOffset))
            {
                // ok
            }
            else
            {
                textboxLog.AppendText("ERROR: Register is invalid!" + Environment.NewLine);
                return;
            }

            RegQuantity = (UInt16)numericQuantity.Value;

            if( radiobuttonSize32bit.Checked && ((RegQuantity & 1) == 1))
            {
                textboxLog.AppendText("ERROR: Quantity must be even to read 32-bit values!" + Environment.NewLine);
                return;
            }

            // output info
            textboxLog.AppendText("Read: " + Register + " #" + RegQuantity + ": ");

            // disable SEND button for visual information
            buttonSend.Enabled = false;
            // and show waitcursor
            Cursor.Current = Cursors.WaitCursor;

            // Read registers 
            short[] aValues;
            ModbusProtocol.Result result = mModbus.MifReadHoldingRegisters(SlaveID, RegOffset, RegQuantity, out aValues);
            if( result == ModbusProtocol.Result.SUCCESS)
            {
                // ok

                string Result = "";
                char cSeparator = radiobuttonAsFloat.Checked ? ';' : ',';
                for (int i = 0; i < RegQuantity; i++)
                {
                    Int32 Value = 0;

                    if (i > 0) Result += cSeparator + " ";

                    // get 16 or 32-bit value
                    Value = aValues[i];
                    if (radiobuttonSize32bit.Checked)
                    {
                        // make 32-bit value of 2 16-bit registers, respect configured word order
                        Value = mParent.ToInt32(aValues[i], aValues[i+1]);
                        i++;
                    }

                    int Size = radiobuttonSize32bit.Checked ? 32 : 16;
                    if( radiobuttonAsInt.Checked)
                    {
                        // signed decimal
                        Result += ValueToStringClass.AsInt(Value, Size, 0, 0);
                    }
                    else if( radiobuttonAsUInt.Checked)
                    {
                        // unsigned decimal
                        Result += ValueToStringClass.AsUInt( Value, Size, 0, 0);
                    }
                    else if( radiobuttonAsHex.Checked)
                    {
                        // hexadecimal
                        Result += ValueToStringClass.AsHex( Value, Size, 0, 0);
                    }
                    else if (radiobuttonAsFloat.Checked)
                    {
                        // 32-bit float
                        Result += ValueToStringClass.AsFloat(Value, 32, 0, 0);
                    }
                    else 
                    {
                        // should not get here
                        Result += Value.ToString();
                    }
                }

                if (RegQuantity == 0) Result += "(no data)";

                textboxLog.AppendText(Result + Environment.NewLine);
            }
            else
            {
                // error

                textboxLog.AppendText("ERROR: " + ModbusProtocol.GetResultText(result) + Environment.NewLine);
            }

            // If we use short-lived TCP connections: close the TCP connection
            if (ShortLivedTcpConnection && mModbus.IsTcpChannel)
            {
                mModbus.CloseTcpChannelConnection();
            }

            // re-enable
            buttonSend.Enabled = true;
            Cursor.Current = Cursors.Default;
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            textboxLog.Clear();
            textboxLog.Update();
        }

        private void radiobuttonAsFloat_CheckedChanged(object sender, EventArgs e)
        {
            // block 16-bit if float is selected
            radiobuttonSize16bit.Enabled = !radiobuttonAsFloat.Checked;

            // switch to 32-bit if float is selected
            if (radiobuttonAsFloat.Checked) radiobuttonSize32bit.Checked = true;
        }

        private void checkboxEnableMainPolling_CheckedChanged(object sender, EventArgs e)
        {
            // enable/disable main tabs polling
            mParent.SetMainPollingEnable(checkboxEnableMainPolling.Checked);
        }

        private void CustomRead_Shown(object sender, EventArgs e)
        {
            // form is shown: disable polling if unchecked
            mParent.SetMainPollingEnable(checkboxEnableMainPolling.Checked);
        }

    }
}
