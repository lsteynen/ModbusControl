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
    public partial class CustomModbusCommand : Form
    {
        Form1 mParent;
        public ModbusInterface mModbus;

        public CustomModbusCommand(Form1 parent)
        {
            InitializeComponent();

            mParent = parent;
            mModbus = parent.mb;
        }

        private void CustomModbusCommand_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Do not close, hide
            this.Hide();
            e.Cancel = true; // this cancels the close event.

            // main form will restore the polling
        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            string cleanstring;
            byte[] query = null;

            // normalize the string (clean it up and validate)
            bool valid = NormalizeStringToHexByteList(textboxCommand.Text, out cleanstring);

            // convert string to array of bytes
            if (valid)
            {
                valid = ConvertStringToByteArray(cleanstring, out query);
            }

            if (valid == false)
            {
                textboxResult.AppendText("ERROR: Command has invalid characters or byte values!" + Environment.NewLine);
                return;
            }

            // replace the original command with the cleaned-up version
            textboxCommand.Text = cleanstring;
            textboxCommand.Update(); // make it visible immediately

            // disable SEND button for visual information
            buttonSend.Enabled = false;
            // and show waitcursor
            Cursor.Current = Cursors.WaitCursor;

            // send the command, get the response
            // response will be != null if we received a valid response (easier for the caller)
            byte[] ResponseBuffer;
            ModbusProtocol.Result result = mModbus.MifSendQuery(query, (ushort)query.Length, out ResponseBuffer);

            // did we get a valid or exception response?
            if ((result == ModbusProtocol.Result.SUCCESS) || (ResponseBuffer != null))
            {
                // we got a valid or exception response, show it
                textboxResult.AppendText( "Response:" + Environment.NewLine
                                          + BytesToString.Convert(ResponseBuffer, ResponseBuffer.Length) + Environment.NewLine);
            }
            else
            {
                // we got no (valid) response, show the error
                textboxResult.AppendText("ERROR: " + result.ToString() + Environment.NewLine);
            }

            // re-enable
            buttonSend.Enabled = true;
            Cursor.Current = Cursors.Default;
        }

        private bool NormalizeStringToHexByteList(string ins, out string result)
        {
            StringBuilder outs = new StringBuilder();

            int i = 0;
            bool insertseparator = false;
            result = null;
            while (i < ins.Length)
            {
                char c = ins[i];
                if (c == ' ')
                {
                    ++i;
                }
                else if (ishex(c))
                {
                    ++i;
                    if (insertseparator)
                    {
                        outs.Append(" ");
                    }
                    if ((i < ins.Length) && ishex(ins[i]))
                    {
                        outs.Append(c);
                        outs.Append(ins[i]);
                        ++i;

                        if ((i < ins.Length) && ishex(ins[i]))
                        {
                            return false;
                        }
                    }
                    else
                    {
                        outs.Append('0');
                        outs.Append(c);
                    }
                    insertseparator = true;
                }
                else
                {
                    return false;
                }
            }

            result = outs.ToString();

            return true;
        }

        private bool ishex(char c)
        {
            return (c >= '0' && c <= '9') || (c >= 'a' && c <= 'f') || (c >= 'A' && c <= 'F');
        }

        private bool ConvertStringToByteArray(string ins, out byte[] result)
        {
            result = new byte[ins.Length];
            int resultindex = 0;

            int i = 0;
            while (i < ins.Length)
            {
                char c = ins[i];
                if (c == ' ')
                {
                    ++i;
                }
                else if (ishex(c))
                {
                    ++i;
                    if ((i < ins.Length) && ishex(ins[i]))
                    {
                        result[resultindex++] = AsciiToByte(c, ins[i]);
                        ++i;

                        if ((i < ins.Length) && ishex(ins[i]))
                        {
                            return false;
                        }
                    }
                    else
                    {
                        result[resultindex++] = AsciiToByte('0', c);
                    }
                }
                else
                {
                    return false;
                }
            }

            Array.Resize(ref result, resultindex);

            return true;
        }

        private byte AsciiToByte(char c1, char c2)
        {
            int result = 0;

            if (c1 >= '0' && c1 <= '9') result = (c1 - '0') * 16;
            else if (c1 >= 'A' && c1 <= 'F') result = (c1 - 'A' + 10) * 16;
            else if (c1 >= 'a' && c1 <= 'f') result = (c1 - 'a' + 10) * 16;

            if (c2 >= '0' && c2 <= '9') result += (c2 - '0');
            else if (c2 >= 'A' && c2 <= 'F') result += (c2 - 'A' + 10);
            else if (c2 >= 'a' && c2 <= 'f') result += (c2 - 'a' + 10);

            return (byte)result;
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            textboxResult.Clear();
        }

        private void CustomModbusCommand_Shown(object sender, EventArgs e)
        {
        }
    }
}
