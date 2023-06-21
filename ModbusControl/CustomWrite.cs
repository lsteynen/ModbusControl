using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

using LST.Helpers;
using Modbus_net;

namespace ModbusControlSpace
{
    public partial class CustomWrite : Form
    {
        MainForm mParent;
        public ModbusInterface mModbus;

        #region properties

        public byte SlaveID
        {
            get { return (byte)numericSlaveAddress.Value; }
            set { numericSlaveAddress.Value = value; }
        }

        public UInt32 Register
        {
            get { return (UInt32)numericRegister.Value; }
            set { numericRegister.Value = value; }
        }

        public UInt16 Quantity
        {
            get { return (UInt16)numericQuantity.Value; }
            set { numericQuantity.Value = value; }
        }

        public string Functioncode
        {
            // uses Radiobutton.Tag
            get { return Global.GetRadioGroupTag(groupboxFunctioncode); }
            set { Global.SetRadioGroupByTag(groupboxFunctioncode, value); }
        }
        
        public string Datasize
        {
            // uses Radiobutton.Tag
            get { return Global.GetRadioGroupTag(groupboxDatasize); }
            set { Global.SetRadioGroupByTag(groupboxDatasize, value); }
        }

        public string Data
        {
            get { return textboxData.Text; }
            set { textboxData.Text = value; }
        }

        #endregion

        // constructor
        public CustomWrite(MainForm parent)
        {
            InitializeComponent();

            // limit number of digits that can be typed in numericupdown
            ((TextBox)numericQuantity.Controls[1]).MaxLength = numericQuantity.Maximum.ToString().Length;
            ((TextBox)numericRegister.Controls[1]).MaxLength = numericRegister.Maximum.ToString().Length;
            ((TextBox)numericSlaveAddress.Controls[1]).MaxLength = numericSlaveAddress.Maximum.ToString().Length;

            mParent = parent;
            mModbus = parent.mb;
        }

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
            if (mParent.TryHoldingregisterGetOffset(Register, out RegOffset))
            {
                // ok
            }
            else
            {
                textboxLog.AppendText("ERROR: Register is invalid! "
                    + "Must be " + mParent.HoldingregisterGetRangeString() + "." + Environment.NewLine);
                return;
            }

            RegQuantity = (UInt16)numericQuantity.Value;

            bool bValid = true;

            // No of 16-bit registers per data value
            int RegsPerValue = radiobuttonSize32bit.Checked ? 2 : 1;

            // get data
            string[] aDataStr = textboxData.Text.Split(',');
            int DataCount = aDataStr.Length;
            // an empty data field is split into 1 empty string
            if ((aDataStr.Length == 1) && (aDataStr[0] == ""))
            {
                DataCount = 0;
            }

            // Number of data values matches quantity of registers?
            if ((DataCount * RegsPerValue) == RegQuantity)
            {
                // ok
            }
            else
            {
                string Str = String.Empty;
                if (radiobuttonSize32bit.Checked)
                {
                    Str = "ERROR: Number of data values does not match quantity! (found " + DataCount + " 32-bit data values, quantity is " + RegQuantity + " 16-bit registers)";
                }
                else
                {
                    Str = "ERROR: Number of data values does not match quantity! (found " + DataCount + " data values, quantity is " + RegQuantity + " registers)";
                }
                textboxLog.AppendText( Str + Environment.NewLine);
                return;
            }

            // array of 16-bit values to store binary data
            short[] a16bitValues = new short[DataCount * RegsPerValue];

            // data values to binary
            if (radiobuttonSize32bit.Checked)
            {
                // get 32-bit values
                for (int i = 0; i < DataCount; ++i)
                {
                    Int32 Value;
                    Global.eType Type;
                    // the trim removes spaces, tabs and CR/LF
                    bool bIsValid = CValueToString.StringToValue(aDataStr[i].Trim(), false, 32, 0, out Value, out Type);

                    if (bIsValid)
                    {
                        // create the modbus values to write
                        Int16[] TmpsShorts = Endians.ConvertValueToShorts(Value, Type, 2);

                        a16bitValues[i * 2] = TmpsShorts[0];
                        a16bitValues[(i * 2) + 1] = TmpsShorts[1];
                    }
                    else
                    {
                        // error
                        textboxLog.AppendText("ERROR: Invalid data value! (" + NthItem(1 + i) + " value)" + Environment.NewLine);
                        bValid = false;
                        break;
                    }
                }
            }
            else
            {
                // get 16-bit values
                for (int i = 0; i < DataCount; ++i)
                {
                    Int32 Value;
                    Global.eType Type;
                    bool bIsValid = CValueToString.StringToValue(aDataStr[i].Trim(), false, 16, 0, out Value, out Type);

                    if (bIsValid)
                    {
                        // create the modbus values to write
                        Int16[] TmpsShorts = Endians.ConvertValueToShorts(Value, Type, 1);

                        a16bitValues[i] = TmpsShorts[0];
                    }
                    else
                    {
                        // error
                        // color the invalid value
                        // calc starting point
                        int StartPos = 0;
                        for (int j = 0; j < i; ++j) StartPos += aDataStr[j].Length + 1;
                        textboxData.SelectionStart = StartPos;
                        textboxData.SelectionLength = aDataStr[i].Length;
                        textboxData.Focus();
                        textboxLog.AppendText("ERROR: Invalid data value! (" + NthItem(1 + i) + " value)" + Environment.NewLine);
                        bValid = false;
                        break;
                    }
                }
            }

            if (bValid)
            {
                // write registers 

                // disable SEND button for visual information
                buttonSend.Enabled = false;
                // and show waitcursor
                Cursor.Current = Cursors.WaitCursor;

                if (radiobuttonWriteFC16.Checked)
                {
                    // use a single FC16 write

                    // output info
                    textboxLog.AppendText("Write: " + Register + " #" + RegQuantity + ": ");
                    string Resulttext = string.Empty;

                    ModbusProtocol.Result result = mModbus.MifWriteMultipleHoldingRegisters(SlaveID, RegOffset, RegQuantity, a16bitValues);
                    if (result == ModbusProtocol.Result.SUCCESS)
                    {
                        Resulttext += "OK";
                        textboxLog.AppendText(Resulttext + Environment.NewLine);
                    }
                    else
                    {
                        // error
                        Resulttext = "ERROR: " + ModbusProtocol.GetResultText(result);
                        textboxLog.AppendText(Resulttext + Environment.NewLine);
                    }
                }
                else
                {
                    // use multiple FC06 writes
                    for (int i = 0; i < RegQuantity; ++i)
                    {
                        // output info, one line per register
                        textboxLog.AppendText("Write: " + (Register+i) + ": ");
                        string Resulttext = string.Empty;

                        ModbusProtocol.Result result = mModbus.MifWriteSingleHoldingRegister(SlaveID, (UInt16)(RegOffset + i), a16bitValues[i]);
                        if (result == ModbusProtocol.Result.SUCCESS)
                        {
                            Resulttext = "OK";
                            textboxLog.AppendText(Resulttext);
                        }
                        else
                        {
                            // error
                            Resulttext = "ERROR: " + ModbusProtocol.GetResultText(result);
                            textboxLog.AppendText(Resulttext);
                        }
                        textboxLog.AppendText(Environment.NewLine);
                    }
                }

                // re-enable
                buttonSend.Enabled = true;
                Cursor.Current = Cursors.Default;
            }
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            textboxLog.Clear();
            textboxLog.Update();
        }

        private string NthItem(int N)
        {
            if (N == 1)
            {
                return "1st";
            }
            else if (N == 2)
            {
                return "2nd";
            }
            return N.ToString() + "th";
        }

        private void CustomWrite_Shown(object sender, EventArgs e)
        {
        }

    }
}
