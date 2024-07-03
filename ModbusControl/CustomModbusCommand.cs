using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

using LST.Helpers;
using Modbus_net;

//TODO: scheiding met komma of puntkomma? je moet gelezen data kunnen plakken in te schrijven data
//TODO: er is nog een probleem met zoomen
//TODO: Read/Write Multiple Registers (F23) toevoegen?
//TODO: alle addressen kunnen gebruiken, en alle qty 0-65535 kunnen gebruiken??? allowAll
//TODO: data updaten als user format wijzigt?
//TODO: move up: lijn blijft geselecteerd, je kan blijven moven

namespace ModbusControlSpace
{
    public partial class CustomModbusCommand : Form
    {
        MainForm mParent;
        public ModbusInterface mModbus;
        private float ZoomFactor = 1.0F;
        private float currentZoomFactor;
        private float originalFontSize;

        // array to translate between command combobox index and XML file command_code
        // array index matches command combobox index
        private string[] CommandStdcode = new string[] { "RHR", "WSHR", "WMHR", "RIR", "RC", "WSC", "WMC", "RDI", "RAW" };

        #region properties

        #endregion

        public CustomModbusCommand(MainForm parent)
        {
            InitializeComponent();

            mParent = parent;
            mModbus = parent.mb;

            originalFontSize = grid.Font.Size;

            //grid.RowTemplate.Height = 25;
            setAutosizeAndWrapRows(false);
            this.DoubleBuffered = true;

            // enable mousewheel zoom
            this.MouseWheel += new MouseEventHandler(Form_MouseWheel);

#if true
            // debugging cases
            AddTestRow("RHR", "1", "40001", "1", "Int16", "", "good");
            AddTestRow("RHR", "1", "40001", "10", "Int16", "", "good");
            AddTestRow("RHR", "1", "40001", "0", "Int16", "", "good");
            AddTestRow("RHR", "1", "40001", "125", "Int16", "", "good");
            AddTestRow("RHR", "1", "51000", "10", "Int16", "", "error");
            AddTestRow("RHR", "1", "40001", "126", "Int16", "", "error");
            AddTestRow("RHR", "1", "", "10", "Int16", "", "error");
            AddTestRow("RHR", "1", "40001", "", "Int16", "", "error");
            AddTestRow("RHR", "1", "40001", "10", "Int16", "101", "error");

            AddTestRow("WSHR", "1", "40002", "1", "Int16", "32000", "good");
            AddTestRow("WSHR", "1", "52000", "1", "Int16", "32000", "error");
            AddTestRow("WSHR", "1", "40002", "0", "Int16", "32000", "error");
            AddTestRow("WSHR", "1", "40002", "999", "Int16", "32000", "error");
            AddTestRow("WSHR", "1", "", "1", "Int16", "32000", "error");
            AddTestRow("WSHR", "1", "40002", "", "Int16", "32000", "error");
            AddTestRow("WSHR", "1", "40002", "1", "Int16", "", "error");

            AddTestRow("WMHR", "1", "40003", "3", "Int16", "1001, 1002, 1003", "good");
            AddTestRow("WMHR", "1", "52000", "3", "Int16", "1001, 1002, 1003", "error");
            AddTestRow("WMHR", "1", "40003", "130", "Int16", "1001, 1002, 1003", "error");
            AddTestRow("WMHR", "1", "40003", "100", "Int16", "21001, 21002, 21003, 21004, 21005, 21006, 21007, 21008, 21009, 21010, 21011, 21012, 21013, 21014, 21015, 21016, 21017, 21018, 21019, 21020, 21021, 21022, 21023, 21024, 21025, 21026, 21027, 21028, 21029, 21030, 21031, 21032, 21033, 21034, 21035, 21036, 21037, 21038, 21039, 21040, 21041, 21042, 21043, 21044, 21045, 21046, 21047, 21048, 21049, 21050, 21051, 21052, 21053, 21054, 21055, 21056, 21057, 21058, 21059, 21060, 21061, 21062, 21063, 21064, 21065, 21066, 21067, 21068, 21069, 21070, 21071, 21072, 21073, 21074, 21075, 21076, 21077, 21078, 21079, 21080, 21081, 21082, 21083, 21084, 21085, 21086, 21087, 21088, 21089, 21090, 21091, 21092, 21093, 21094, 21095, 21096, 21097, 21098, 21099, 21100, 21101, 21102, 21103, 21104, 21105, 21106, 21107, 21108, 21109, 21110", "write 100 values");
            AddTestRow("WMHR", "1", "", "3", "Int16", "1001, 1002, 1003", "error");
            AddTestRow("WMHR", "1", "40003", "", "Int16", "1001, 1002, 1003", "error");
            AddTestRow("WMHR", "1", "40003", "3", "Int16", "", "error");
            AddTestRow("WMHR", "1", "40003", "3", "Int16", "1001, 1002", "error");

            AddTestRow("RIR", "1", "30001", "1", "Int16", "", "good");
            AddTestRow("RIR", "1", "30001", "10", "Int16", "", "good");
            AddTestRow("RIR", "1", "30001", "0", "Int16", "", "good");
            AddTestRow("RIR", "1", "30001", "125", "Int16", "", "good");
            AddTestRow("RIR", "1", "41000", "10", "Int16", "", "error");
            AddTestRow("RIR", "1", "30001", "126", "Int16", "", "error");
            AddTestRow("RIR", "1", "", "10", "Int16", "", "error");
            AddTestRow("RIR", "1", "30001", "", "Int16", "", "error");
            AddTestRow("RIR", "1", "30001", "10", "Int16", "101", "error");

            AddTestRow("RC", "1", "1", "1", "Int16", "", "good");
            AddTestRow("RC", "1", "1", "12", "Int16", "", "good");
            AddTestRow("RC", "1", "1", "100", "Int16", "", "good");
            AddTestRow("RC", "1", "1", "2000", "Int16", "", "good");
            AddTestRow("RC", "1", "0", "12", "Int16", "", "error");
            AddTestRow("RC", "1", "1", "", "Int16", "", "error");
            AddTestRow("RC", "1", "1", "12", "Int16", "456", "error");

            AddTestRow("WSC", "1", "1", "1", "Int16", "1", "good");
            AddTestRow("WSC", "1", "", "1", "Int16", "1", "error");
            AddTestRow("WSC", "1", "1", "2", "Int16", "1", "error");
            AddTestRow("WSC", "1", "1", "1", "Int16", "", "error");
            AddTestRow("WSC", "1", "1", "1", "Int16", "1,0", "error");

            AddTestRow("WMC", "1", "2", "10", "Int16", "1,0,0,0,1,0,0,1,1,0", "good");
            AddTestRow("WMC", "1", "", "10", "Int16", "1,0,0,0,1,0,0,1,1,0", "error");
            AddTestRow("WMC", "1", "2", "", "Int16", "1,0,0,0,1,0,0,1,1,0", "error");
            AddTestRow("WMC", "1", "2", "10", "Int16", "1,0,0,0,1,0,0,1,1", "error");
            AddTestRow("WMC", "1", "2", "10", "Int16", "1,0,0,0,1,0,0,1,1,0,1", "error");

            AddTestRow("RDI", "1", "10001", "12", "Int16", "", "good");
            AddTestRow("RDI", "1", "", "12", "Int16", "", "error");
            AddTestRow("RDI", "1", "10001", "", "Int16", "", "error");
            AddTestRow("RDI", "1", "10001", "12", "Int16", "55", "error");

            AddTestRow("RAW", "1", "", "", "Int16", "03 00 00 00 05", "good");
            AddTestRow("RAW", "1", "40001", "", "Int16", "03 00 00 00 05", "error");
            AddTestRow("RAW", "1", "", "10", "Int16", "03 00 00 00 05", "error");
            AddTestRow("RAW", "1", "", "", "Int16", "", "error");
            AddTestRow("RAW", "1", "", "", "Int16", "03", "error");
            AddTestRow("RAW", "1", "", "", "Int16", "03 aaa", "error");
#endif
        }

        private void AddTestRow(string cmd, string slaveid, string reg, string qty, string format, string data, string comment)
        {
            int rowIndex = grid.Rows.Count;
            grid.Rows.Add("", slaveid, "", reg, qty, format, data, "", comment);
            SetCommandComboByStdcode(rowIndex, cmd);
            //SetDgvComboboxSelectedText(FormatColumn, rowIndex, format);
        }

        private void CustomModbusCommand_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Do not close, hide
            this.Hide();
            e.Cancel = true; // this cancels the close event.

            // main form will restore the polling
        }

        private void Form_MouseWheel(object sender, MouseEventArgs e)
        {
            int wheelFactor = 120;

            // CTRL+scrollwheel: zoom/unzoom
            if (Control.ModifierKeys == Keys.Control)
            {
                // send +1 to zoom in, -1 to zoom out
                if ((e.Delta / wheelFactor) >= +1)
                {
                    ChangeZoom(+1);
                }
                else if ((e.Delta / wheelFactor) <= -1)
                {
                    ChangeZoom(-1);
                }
            }
        }

        /// <summary>
        /// Change the zoom factor
        /// </summary>
        /// <param name="action">+1 do zoom in, -1 for zoom out, 0 for reset zoom</param>
        private void ChangeZoom(int action)
        {
            // zoom callback from RegView

            float minZoom = 1;
            float maxZoom = 10;
            float changeFactor = 1.15F;

            if (action > 0)
            {
                // zoom in
                if (ZoomFactor < maxZoom)
                {
                    ZoomFactor = ZoomFactor * changeFactor;
                }
            }
            else if (action < 0)
            {
                // zoom out
                if (ZoomFactor > minZoom)
                {
                    ZoomFactor = ZoomFactor / changeFactor;
                }
            }
            else
            {
                // reset zoom
                ZoomFactor = 1.0F;
            }

            // update zoom 
            SetZoom(ZoomFactor);
        }

        public void SetZoom(float factor)
        {
            if (factor != currentZoomFactor)
            {
                currentZoomFactor = factor;

                // zoom this grid
                float newFontSize = originalFontSize * currentZoomFactor;
                Font newFont = new Font(Font.FontFamily, newFontSize, Font.Style, Font.Unit);
                grid.ColumnHeadersDefaultCellStyle.Font = newFont;
                grid.DefaultCellStyle.Font = newFont;
            }
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

        private void CustomModbusCommand_Shown(object sender, EventArgs e)
        {
        }

        private void myDataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                e.RowIndex >= 0)
            {
                ExecuteRow(e.RowIndex);
            }
        }
        private void ExecuteRow(int rowIndex)
        {
            Debug.Print(DateTime.Now.TimeOfDay + ": ExecuteRow: " + rowIndex.ToString());

            string slaveidStr = isnull(grid.Rows[rowIndex].Cells[SlaveidColumn.Index].Value, "");
            string commandCode = GetCommandcomboAsStdcode(rowIndex);
            string registerStr = isnull(grid.Rows[rowIndex].Cells[RegisterColumn.Index].Value, "");
            string qtyStr = isnull(grid.Rows[rowIndex].Cells[QtyColumn.Index].Value, "");
            string formatStr = isnull(grid.Rows[rowIndex].Cells[FormatColumn.Index].Value, "");
            string dataStr = isnull(grid.Rows[rowIndex].Cells[DataColumn.Index].Value, "");

            string ResultStr = "INTERNAL ERROR";

            if (commandCode == "RHR")
            {
                ResultStr = ExecuteRHR(registerStr, qtyStr, formatStr, dataStr);
            }
            else if (commandCode == "WSHR")
            {
                ResultStr = ExecuteWSHR(registerStr, qtyStr, dataStr);
            }
            else if (commandCode == "WMHR")
            {
                ResultStr = ExecuteWMHR(registerStr, qtyStr, dataStr);
            }
            else if (commandCode == "RIR")
            {
                ResultStr = ExecuteRIR(registerStr, qtyStr, formatStr, dataStr);
            }
            else if (commandCode == "RC")
            {
                ResultStr = ExecuteRC(registerStr, qtyStr, dataStr);
            }
            else if (commandCode == "WSC")
            {
                ResultStr = ExecuteWSC(registerStr, qtyStr, dataStr);
            }
            else if (commandCode == "WMC")
            {
                ResultStr = ExecuteWMC(registerStr, qtyStr, dataStr);
            }
            else if (commandCode == "RDI")
            {
                ResultStr = ExecuteRDI(registerStr, qtyStr, dataStr);
            }
            else if (commandCode == "RAW")
            {
                ResultStr = ExecuteRAW(registerStr, qtyStr, dataStr);
            }

            // show result
            grid.Rows[rowIndex].Cells[ResultColumn.Index].Value = ResultStr;
        }

        private static string isnull(object v, string repl)
        {
            if (v != null)
                return (string)v;
            return repl;
        }

        private void ConvertFormatstrToWidthAndFormat(string formatStr, out int width, out Global.eFormat format)
        {
            width = 0;
            format = Global.eFormat.Int;

            if (formatStr.ToLower() == "int16")
            {
                width = 16;
                format = Global.eFormat.Int;
            }
            else if (formatStr.ToLower() == "uint16")
            {
                width = 16;
                format = Global.eFormat.UInt;
            }
            else if (formatStr.ToLower() == "hex16")
            {
                width = 16;
                format = Global.eFormat.Hex;
            }
            else if (formatStr.ToLower() == "bin16")
            {
                width = 16;
                format = Global.eFormat.Bin;
            }
            else if (formatStr.ToLower() == "ascii16")
            {
                width = 16;
                format = Global.eFormat.ASCII;
            }
            else if (formatStr.ToLower() == "int32")
            {
                width = 32;
                format = Global.eFormat.Int;
            }
            else if (formatStr.ToLower() == "uint32")
            {
                width = 32;
                format = Global.eFormat.UInt;
            }
            else if (formatStr.ToLower() == "hex32")
            {
                width = 32;
                format = Global.eFormat.Hex;
            }
            else if (formatStr.ToLower() == "bin32")
            {
                width = 32;
                format = Global.eFormat.Bin;
            }
            else if (formatStr.ToLower() == "ascii32")
            {
                width = 32;
                format = Global.eFormat.ASCII;
            }
            else if (formatStr.ToLower() == "float32")
            {
                width = 32;
                format = Global.eFormat.Float;
            }
            else
            {
                throw new Exception("Invalid argument formatStr");
            }
        }

        private string ExecuteRHR(string registerStr, string qtyStr, string formatStr, string data)
        {
            string ResultStr = "ERROR";
            bool Continue = true;
            byte slaveID = 1;
            UInt32 register = 0;
            UInt16 regOffset = 0;
            UInt16 regQuantity = 0;

            //TODO: 40000, 40001, 400000, 400001?
            if (uint.TryParse(registerStr, out register)
                && mParent.TryHoldingregisterGetOffset(register, out regOffset))
            {
                // okay
            }
            else
            {
                ResultStr = "Error: Invalid Register";
                Continue = false;
            }

            if (Continue)
            {
                if (UInt16.TryParse(qtyStr, out regQuantity))
                {
                    // okay
                }
                else
                {
                    ResultStr = "Error: Invalid Qty";
                    Continue = false;
                }
            }

            if (Continue)
            {
                if (data.Trim() == "")
                {
                    // okay
                }
                else
                {
                    ResultStr = "Error: Data should be empty";
                    Continue = false;
                }
            }

            if (Continue)
            {
                // Read
                short[] aValues;
                ModbusProtocol.Result result = mModbus.MifReadHoldingRegisters(slaveID, regOffset, (ushort)regQuantity, out aValues, true);
                if (result == ModbusProtocol.Result.SUCCESS)
                {
                    // ok
                    ResultStr = ConvertRegistersToResultString(aValues, formatStr);
                }
                else
                {
                    // error

                    string tmpErrorText = result.ToString();
                    string headstr = "";
                    if (tmpErrorText.IndexOf("EXCEPTION", StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        headstr = "Error response: ";
                    }
                    else
                    {
                        headstr = "Error: ";
                    }
                    ResultStr = headstr + ModbusProtocol.GetResultText(result);
                }
            }

            return ResultStr;
        }

        private string ExecuteWSHR(string registerStr, string qtyStr, string data)
        {
            string ResultStr = "ERROR";
            bool Continue = true;
            byte slaveID = 1;
            UInt32 register = 0;
            UInt16 regOffset = 0;
            UInt16 regQuantity = 0;
            Int16[] aValues = null;

            //TODO: 40000, 40001, 400000, 400001?
            if (uint.TryParse(registerStr, out register)
                && mParent.TryHoldingregisterGetOffset(register, out regOffset))
            {
                //ParseStringToInt16();
                // okay
            }
            else
            {
                ResultStr = "Error: Invalid Register";
                Continue = false;
            }

            if (Continue)
            {
                if ((UInt16.TryParse(qtyStr, out regQuantity))
                    && (regQuantity == 1))
                {
                    // okay
                }
                else
                {
                    ResultStr = "Error: Invalid Qty";
                    Continue = false;
                }
            }

            if (Continue)
            {
                if (data.Trim() != "")
                {
                    // okay
                }
                else
                {
                    ResultStr = "Error: Data is empty";
                    Continue = false;
                }
            }

            if (Continue)
            {
                try
                {
                    aValues = ParseStringIntoInt16Values(data);
                }
                catch (Exception e)
                {
                    ResultStr = "Error: " + e.Message;
                    Continue = false;
                }
            }

            if (Continue)
            {
                if (aValues.Length > 1)
                {
                    ResultStr = "Error: Too many values";
                    Continue = false;
                }
            }

            if (Continue)
            {
                // Write
                ModbusProtocol.Result result = mModbus.MifWriteSingleHoldingRegister(slaveID, regOffset, aValues[0]);
                if (result == ModbusProtocol.Result.SUCCESS)
                {
                    // ok
                    ResultStr = "Success";
                }
                else
                {
                    // error

                    string tmpErrorText = result.ToString();
                    string headstr = "";
                    if (tmpErrorText.IndexOf("EXCEPTION", StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        headstr = "Error response: ";
                    }
                    else
                    {
                        headstr = "Error: ";
                    }
                    ResultStr = headstr + ModbusProtocol.GetResultText(result);
                }
            }

            return ResultStr;
        }

        private string ExecuteWMHR(string registerStr, string qtyStr, string data)
        {
            string ResultStr = "ERROR";
            bool Continue = true;
            byte slaveID = 1;
            UInt32 register = 0;
            UInt16 regOffset = 0;
            UInt16 regQuantity = 0;
            Int16[] aValues = null;

            //TODO: 40000, 40001, 400000, 400001?
            if (uint.TryParse(registerStr, out register)
                && mParent.TryHoldingregisterGetOffset(register, out regOffset))
            {
                //ParseStringToInt16();
                // okay
            }
            else
            {
                ResultStr = "Error: Invalid Register";
                Continue = false;
            }

            if (Continue)
            {
                if ((UInt16.TryParse(qtyStr, out regQuantity))
                    && (regQuantity >= 1)
                    && (regQuantity <= 125))
                {
                    // okay
                }
                else
                {
                    ResultStr = "Error: Invalid Qty";
                    Continue = false;
                }
            }

            if (Continue)
            {
                if (data.Trim() != "")
                {
                    // okay
                }
                else
                {
                    ResultStr = "Error: Data is empty";
                    Continue = false;
                }
            }

            if (Continue)
            {
                try
                {
                    aValues = ParseStringIntoInt16Values(data);
                }
                catch (Exception e)
                {
                    ResultStr = "Error: " + e.Message;
                    Continue = false;
                }
            }

            if (Continue)
            {
                if (aValues.Length < regQuantity)
                {
                    ResultStr = "Error: Not enough values";
                    Continue = false;
                }
            }

            if (Continue)
            {
                if (aValues.Length > regQuantity)
                {
                    ResultStr = "Error: Too many values";
                    Continue = false;
                }
            }

            if (Continue)
            {
                // Write
                ModbusProtocol.Result result = mModbus.MifWriteMultipleHoldingRegisters(slaveID, regOffset, regQuantity, aValues);
                if (result == ModbusProtocol.Result.SUCCESS)
                {
                    // ok
                    ResultStr = "Success";
                }
                else
                {
                    // error

                    string tmpErrorText = result.ToString();
                    string headstr = "";
                    if (tmpErrorText.IndexOf("EXCEPTION", StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        headstr = "Error response: ";
                    }
                    else
                    {
                        headstr = "Error: ";
                    }
                    ResultStr = headstr + ModbusProtocol.GetResultText(result);
                }
            }

            return ResultStr;
        }

        private string ExecuteRIR(string registerStr, string qtyStr, string formatStr, string data)
        {
            string ResultStr = "ERROR";
            bool Continue = true;
            byte slaveID = 1;
            UInt32 register = 0;
            UInt16 regOffset = 0;
            UInt16 regQuantity = 0;

            // 30001, 300001
            if (uint.TryParse(registerStr, out register)
                && mParent.TryInputregisterGetOffset(register, out regOffset))
            {
                // okay
            }
            else
            {
                ResultStr = "Error: Invalid Register";
                Continue = false;
            }

            if (Continue)
            {
                if (UInt16.TryParse(qtyStr, out regQuantity))
                {
                    // okay
                }
                else
                {
                    ResultStr = "Error: Invalid Qty";
                    Continue = false;
                }
            }

            if (Continue)
            {
                if (data.Trim() == "")
                {
                    // okay
                }
                else
                {
                    ResultStr = "Error: Data should be empty";
                    Continue = false;
                }
            }

            if (Continue)
            {
                // Read
                // aValues is coils packed in bytes
                short[] aValues;
                ModbusProtocol.Result result = mModbus.MifReadInputRegisters(slaveID, regOffset, (ushort)regQuantity, out aValues, true);
                if (result == ModbusProtocol.Result.SUCCESS)
                {
                    // ok
                    ResultStr = ConvertRegistersToResultString(aValues, formatStr);
                }
                else
                {
                    // error

                    string tmpErrorText = result.ToString();
                    string headstr = "";
                    if (tmpErrorText.IndexOf("EXCEPTION", StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        headstr = "Error response: ";
                    }
                    else
                    {
                        headstr = "Error: ";
                    }
                    ResultStr = headstr + ModbusProtocol.GetResultText(result);
                }
            }

            return ResultStr;
        }

        private string ExecuteRC(string registerStr, string qtyStr, string data)
        {
            string ResultStr = "ERROR";
            bool Continue = true;
            byte slaveID = 1;
            UInt32 register = 0;
            UInt16 regOffset = 0;
            UInt16 regQuantity = 0;

            // TODO: 1..9999 or 1..65536
            if (uint.TryParse(registerStr, out register)
                && mParent.TryCoilsGetOffset(register, out regOffset))
            {
                // okay
            }
            else
            {
                ResultStr = "Error: Invalid Register";
                Continue = false;
            }

            if (Continue)
            {
                if (UInt16.TryParse(qtyStr, out regQuantity))
                {
                    // okay
                }
                else
                {
                    ResultStr = "Error: Invalid Qty";
                    Continue = false;
                }
            }

            if (Continue)
            {
                if (data.Trim() == "")
                {
                    // okay
                }
                else
                {
                    ResultStr = "Error: Data should be empty";
                    Continue = false;
                }
            }

            if (Continue)
            {
                // Read
                byte[] aValues;
                ModbusProtocol.Result result = mModbus.MifReadCoils(slaveID, regOffset, (ushort)regQuantity, out aValues, true);
                if (result == ModbusProtocol.Result.SUCCESS)
                {
                    // ok
                    // width is 1, format is int 0/1
                    ResultStr = ConvertPackedBitsToResultString(aValues, (int)regQuantity);
                }
                else
                {
                    // error

                    string tmpErrorText = result.ToString();
                    string headstr = "";
                    if (tmpErrorText.IndexOf("EXCEPTION", StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        headstr = "Error response: ";
                    }
                    else
                    {
                        headstr = "Error: ";
                    }
                    ResultStr = headstr + ModbusProtocol.GetResultText(result);
                }
            }

            return ResultStr;
        }

        private string ExecuteWSC(string registerStr, string qtyStr, string data)
        {
            return "TODO???";
        }

        private string ExecuteWMC(string registerStr, string qtyStr, string data)
        {
            return "TODO???";
        }

        private string ExecuteRDI(string registerStr, string qtyStr, string data)
        {
            string ResultStr = "ERROR";
            bool Continue = true;
            byte slaveID = 1;
            UInt32 register = 0;
            UInt16 regOffset = 0;
            UInt16 regQuantity = 0;

            // TODO: 1..9999 or 1..65536
            if (uint.TryParse(registerStr, out register)
                && mParent.TryDiscreteInputsGetOffset(register, out regOffset))
            {
                // okay
            }
            else
            {
                ResultStr = "Error: Invalid Register"; //TODO??
                Continue = false;
            }

            if (Continue)
            {
                if (UInt16.TryParse(qtyStr, out regQuantity))
                {
                    // okay
                }
                else
                {
                    ResultStr = "Error: Invalid Qty";
                    Continue = false;
                }
            }

            if (Continue)
            {
                if (data.Trim() == "")
                {
                    // okay
                }
                else
                {
                    ResultStr = "Error: Data should be empty";
                    Continue = false;
                }
            }

            if (Continue)
            {
                // Read
                byte[] aValues;
                ModbusProtocol.Result result = mModbus.MifReadDiscreteInputs(slaveID, regOffset, (ushort)regQuantity, out aValues, true);
                if (result == ModbusProtocol.Result.SUCCESS)
                {
                    // ok
                    // width is 1, format is int 0/1
                    ResultStr = ConvertPackedBitsToResultString(aValues, (int)regQuantity);
                }
                else
                {
                    // error

                    string tmpErrorText = result.ToString();
                    string headstr = "";
                    if (tmpErrorText.IndexOf("EXCEPTION", StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        headstr = "Error response: ";
                    }
                    else
                    {
                        headstr = "Error: ";
                    }
                    ResultStr = headstr + ModbusProtocol.GetResultText(result);
                }
            }

            return ResultStr;
        }

        private string ExecuteRAW(string registerStr, string qtyStr, string data)
        {
            return "TODO???";
        }

        private string ConvertRegistersToResultString(short[] aValues, string formatStr)
        {
            string Result = "";
            string separatorStr = "; ";
            bool Continue = true;
            int width;
            Global.eFormat format;
            int qty = aValues.Length;

            ConvertFormatstrToWidthAndFormat(formatStr, out width, out format);

            // check: qty must be multiple of 2 for 32-bit values
            if ((width == 32) && ((qty % 2) != 0))
            {
                Result = "Error: Qty must be multiple of 2 for 32-bit values";
                Continue = false;
            }

            if (Continue)
            {
                // no data?
                if (qty == 0)
                {
                    Result = "(no data)";
                    Continue = false;
                }
            }

            if (Continue)
            {
                for (int i = 0; i < qty; i++)
                {
                    Int32 Value = 0;

                    // insert separator
                    if (i > 0)
                    {
                        Result += separatorStr;
                    }

                    // convert format to type
                    Global.eType Type = Global.ConvertFormatToType(format);

                    // make 32-bit value, watch size and endian
                    short[] aTmpRegs = new short[2];
                    aTmpRegs[0] = aValues[i];
                    aTmpRegs[1] = 0;
                    // is it 32-bit?
                    if (width == 32)
                    {
                        aTmpRegs[1] = aValues[++i];
                    }
                    Value = Endians.ConvertShortsToValue(aTmpRegs, Type, width);

                    // make value string
                    // decimals not used, minlen not used
                    Result += CValueToString.ValueToString(Value, width, format);
                }
            }

            return Result;
        }

        private string ConvertPackedBitsToResultString(byte[] aValues, int qty)
        {
            string Result = "";
            string separatorStr = "; ";
            int groupPer = 10;
            bool Continue = true;

            // coils are packed in bytes
            // first coil is in bit0 of first byte

            // no data?
            if (qty == 0)
            {
                Result = "(no data)";
                Continue = false;
            }

            if (Continue)
            {
                for (int i = 0; i < qty; i++)
                {
                    // insert separator every groupPer coils
                    if ((i > 0) && ((i % groupPer) == 0))
                    {
                        Result += separatorStr;
                    }

                    byte mask = (byte)(1 << (i % 8));
                    if ((aValues[i / 8] & mask) != 0)
                        Result += "1";
                    else
                        Result += "0";
                }
            }

            return Result;
        }

        /// <summary>
        /// Parse string into Int16 values
        /// Accepts signed and unsigned values
        /// Accepts decimal, hex and binary values
        /// Throws exception on error
        /// </summary>
        Int16[] ParseStringIntoInt16Values(string str)
        {
            int valuePos = 0;
            // get data
            string[] aValuesStr = str.Split(',');
            // an empty data field is split into 1 empty string
            if ((aValuesStr.Length == 1) && (aValuesStr[0] == ""))
            {
                throw new Exception("No values");
            }

            // array of 16-bit values to store binary data
            Int16[] aValues = new short[aValuesStr.Length];

            // get values
            for (int i = 0; i < aValuesStr.Length; ++i)
            {
                Int32 Value;
                Global.eType Type;
                bool isValid = CValueToString.StringToValue(aValuesStr[i].Trim(), false, 16, 0, out Value, out Type);
                if (isValid)
                {
                    // create the modbus values to write
                    Int16[] tmpInt16 = Endians.ConvertValueToShorts(Value, Type, 1);
                    aValues[i] = tmpInt16[0];
                }
                else
                {
                    // error
                    throw new Exception("Invalid value, at position " + valuePos.ToString());
                }

                // get pos of next value
                valuePos += aValuesStr[i].Length + 1;
            }

            return aValues;
        }



        /// <summary>
        /// Standard code like: RHR, WHR, RIR, RC, WC, RDI, RAW
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <returns>Standard code</returns>
        private string GetCommandcomboAsStdcode(int rowIndex)
        {
            int selIndex = GetDgvComboboxSelectedIndex(CommandColumn, rowIndex);
            return CommandStdcode[selIndex];
        }



        /// <summary>
        /// Standard code like: RHR, WHR, RIR, RC, WC, RDI, RAW
        /// </summary>
        /// <param name="stdCode"></param>
        private void SetCommandComboByStdcode(int rowIndex, string stdCode)
        {
            int selIndex = -1;
            for (int i = 0; i < CommandStdcode.Length; ++i)
            {
                if (CommandStdcode[i] == stdCode)
                {
                    selIndex = i;
                }
            }

            if (selIndex >= 0)
            {
                SetDgvComboboxSelectedIndex(CommandColumn, rowIndex, selIndex);
            }
        }

        private int GetDgvComboboxSelectedIndex(DataGridViewComboBoxColumn column, int rowIndex)
        {
            int Result = -1;

            // get selected text
            string selText = grid.Rows[rowIndex].Cells[column.Index].Value.ToString();
            // find the index
            for (int i = 0; i < column.Items.Count; ++i)
            {
                if (selText == column.Items[i].ToString())
                {
                    Result = i;
                    break;
                }
            }

            return Result;
        }

        private void SetDgvComboboxSelectedIndex(DataGridViewComboBoxColumn column, int rowIndex, int selIndex)
        {
            //TODO: selIndex out of range

            // get selected text
            string selText = column.Items[selIndex].ToString();
            // select
            grid.Rows[rowIndex].Cells[column.Index].Value = selText;
        }

        private void SetDgvComboboxSelectedText(DataGridViewComboBoxColumn column, int rowIndex, string text)
        {
            // select
            grid.Rows[rowIndex].Cells[column.Index].Value = text;
        }

        private void checkboxAutoRowSize_CheckedChanged(object sender, EventArgs e)
        {
            setAutosizeAndWrapRows(checkboxAutoRowSize.Checked);

            //debugging
            //checkboxAutoRowSize.Text = grid.Rows[0].Height.ToString();
        }

        private void setAutosizeAndWrapRows(bool value)
        {
            if (value)
            {
                grid.SuspendLayout();

                // set autosize
                grid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;

                // set wrap
                grid.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                DataColumn.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                ResultColumn.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                CommentColumn.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

                grid.ResumeLayout(true);
            }
            else
            {
                grid.SuspendLayout();

                // set autosize
                for (int gri = 0; gri < grid.Rows.Count; ++gri)
                    grid.Rows[gri].Height = grid.RowTemplate.Height;
                grid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;

                // set wrap
                grid.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
                DataColumn.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
                ResultColumn.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
                CommentColumn.DefaultCellStyle.WrapMode = DataGridViewTriState.False;

                grid.ResumeLayout(true);
            }
        }

        private void butMoveUp_Click(object sender, EventArgs e)
        {
            if (grid.SelectedCells.Count > 0)
            {
                int selrow = grid.SelectedCells[0].RowIndex;
                int selcol = grid.SelectedCells[0].ColumnIndex;
                if (selrow > 0)
                {
                    DataGridViewRow row = grid.Rows[selrow];

                    grid.Rows.Remove(row);
                    grid.Rows.Insert(selrow - 1, row);

                    grid.CurrentCell = grid[selcol, selrow - 1];
                }
            }
        }

        private void butMoveDown_Click(object sender, EventArgs e)
        {
            if (grid.SelectedCells.Count > 0)
            {
                int selrow = grid.SelectedCells[0].RowIndex;
                int selcol = grid.SelectedCells[0].ColumnIndex;
                if (selrow < (grid.Rows.Count - 1))
                {
                    DataGridViewRow row = grid.Rows[selrow];

                    grid.Rows.Remove(row);
                    grid.Rows.Insert(selrow + 1, row);

                    grid.CurrentCell = grid[selcol, selrow + 1];
                }
            }
        }
    }
}