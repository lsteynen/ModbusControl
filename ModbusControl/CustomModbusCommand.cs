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
    public partial class CustomModbusCommand : Form
    {
        MainForm mParent;
        public ModbusInterface mModbus;

        #region properties

        public string Command
        {
            get { return textboxCommand.Text; }
            set { textboxCommand.Text = value; }
        }

        #endregion

        public CustomModbusCommand(MainForm parent)
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
            int convresult;
            byte[] query = null;

            // convert string to array of bytes
            convresult = ConvertStringToByteArray(textboxCommand.Text, out query);
            // not OK?
            if (convresult != 0)
            {
                if (convresult == 1)
                {
                    textboxResult.AppendText("ERROR: Command was empty!" + Environment.NewLine);
                }
                else
                {
                    textboxResult.AppendText("ERROR: Command has invalid characters or invalid byte values!" + Environment.NewLine);
                }
                return;
            }

            // minimum length?
            if (query.Length < 2)
            {
                textboxResult.AppendText("ERROR: Command must be at least 2 bytes!" + Environment.NewLine);
                return;
            }

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

        /// <summary>
        /// Convert string to array of bytes.
        /// Each hex byte is 1 or 2 hex digits. Hex bytes are separated by whitespace (spaces, tabs, CR/LF). Extra whitespace is ignored.
        /// </summary>
        /// <returns>0 if OK (got at least 1 byte), 1 if string empty, 2 if error
        /// </returns>
        private int ConvertStringToByteArray(string ins, out byte[] result)
        {
            result = new byte[1000];
            int resultindex = 0;

            int i = 0;
            while (i < ins.Length)
            {
                // skip whitespace
                while ((i < ins.Length) && Char.IsWhiteSpace(ins[i]))
                {
                    ++i;
                }

                // at end?
                if (i >= ins.Length)
                {
                    break;
                }

                // hex digit?
                if (ishex(ins[i]))
                {
                    char c1 = ins[i];
                    ++i;
                    // 2 digits?
                    if ((i < ins.Length) && ishex(ins[i]))
                    {
                        // 2 digits
                        result[resultindex++] = AsciiToByte(c1, ins[i]);
                        ++i;

                        // more digits?
                        if ((i < ins.Length) && ishex(ins[i]))
                        {
                            // error
                            return 2;
                        }
                    }
                    else
                    {
                        // 1 digit
                        result[resultindex++] = AsciiToByte('0', c1);
                    }
                }
                else
                {
                    // error
                    return 2;
                }
            }

            Array.Resize(ref result, resultindex);

            // return error if string was empty
            if (resultindex == 0)
            {
                return 1;
            }

            // success
            return 0;
        }

        private bool ishex(char c)
        {
            return (c >= '0' && c <= '9') || (c >= 'a' && c <= 'f') || (c >= 'A' && c <= 'F');
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
