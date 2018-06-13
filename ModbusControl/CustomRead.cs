using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

using LST.Helpers;
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

            // limit number of digits that can be typed in numericupdown
            ((TextBox)numericSlaveAddress.Controls[1]).MaxLength = numericSlaveAddress.Maximum.ToString().Length;
            ((TextBox)numericRegister.Controls[1]).MaxLength = numericRegister.Maximum.ToString().Length;
            ((TextBox)numericQuantity.Controls[1]).MaxLength = numericQuantity.Maximum.ToString().Length;

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
            if (mParent.TryGetRegOffset(Register, out RegOffset))
            {
                // ok
            }
            else
            {
                textboxLog.AppendText("ERROR: Register is invalid! "
                    + "Must be " + mParent.GetHoldingRegistersStart().ToString() 
                    + " - " + mParent.GetHoldingRegistersEnd().ToString() + "."
                    + Environment.NewLine);
                
                return;
            }

            RegQuantity = (UInt16)numericQuantity.Value;

            if (radiobuttonSize32bit.Checked)
            {
                if (radiobuttonSize32bit.Checked && ((RegQuantity & 1) == 1))
                {
                    textboxLog.AppendText("ERROR: Quantity must be multiple of 2 to read 32-bit values!" + Environment.NewLine);
                    return;
                }
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

                    // get desired format
                    Global.eFormat Format = Global.eFormat.Int;
                    if (radiobuttonAsInt.Checked)
                    {
                        // All to Int
                        Format = Global.eFormat.Int;
                    }
                    else if (radiobuttonAsUInt.Checked)
                    {
                        // All to UInt
                        Format = Global.eFormat.UInt;
                    }
                    else if (radiobuttonAsHex.Checked)
                    {
                        // All to Hex
                        Format = Global.eFormat.Hex;
                    }
                    else if (radiobuttonAsFloat.Checked)
                    {
                        // All to Hex
                        Format = Global.eFormat.Float;
                    }

                    // convert format to type
                    Endians.eType Type = ConvertFormatToType(Format);

                    // 16 or 32-bit?
                    int Size = radiobuttonSize32bit.Checked ? 32 : 16;

                    // make 32-bit value, watch size and endian
                    short[] aTmpRegs = new short[2];
                    aTmpRegs[0] = aValues[i];
                    aTmpRegs[1] = 0;
                    // is it 32-bit?
                    if (Size == 32)
                    {
                        aTmpRegs[1] = aValues[++i];
                    }
                    Value = Endians.ConvertShortsToValue(aTmpRegs, Type, Size);

                    // make value string
                    // decimals are not used
                    // minlen is not used
                    Result += CValueToString.ValueToString(Value, Size, Format, 0, 0);
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

        private void radiobuttonSize16bit_CheckedChanged(object sender, EventArgs e)
        {
            radiobuttonAsFloat.Enabled = false;
            if (radiobuttonAsFloat.Checked)
            {
                radiobuttonAsInt.Checked = true;
            }
        }

        private void radiobuttonSize32bit_CheckedChanged(object sender, EventArgs e)
        {
            radiobuttonAsFloat.Enabled = true;
        }

        private void CustomRead_Shown(object sender, EventArgs e)
        {
        }

        // convert format to type
        private Endians.eType ConvertFormatToType(ModbusControlSpace.Global.eFormat Format)
        {
            Endians.eType Type = Endians.eType.Int;

            switch (Format)
            {
                case ModbusControlSpace.Global.eFormat.Int:
                case ModbusControlSpace.Global.eFormat.UInt:
                case ModbusControlSpace.Global.eFormat.Hex:
                case ModbusControlSpace.Global.eFormat.Bin:
                    Type = Endians.eType.Int;
                    break;
                case ModbusControlSpace.Global.eFormat.Float:
                    Type = Endians.eType.Float;
                    break;
                case ModbusControlSpace.Global.eFormat.ASCII:
                    Type = Endians.eType.ASCII;
                    break;
                default:
                    // invalid
                    throw new ArgumentException("Invalid format enum!");
            }

            return Type;
        }

    }
}
