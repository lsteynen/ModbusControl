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

namespace ModbusControlSpace
{
    public partial class CustomQueries : Form
    {
        public ModbusInterface mModbus;

        private MainForm mParent;
        private float ZoomFactor = 1.0F;
        private float currentZoomFactor;
        private float originalFontSize;
        private int mHoldingRegistersStartAddress = 40001;

        // command enums
        // Must match the order of the texts in the Command dropdown!
        private enum ECommand { RIR = 0, RHR, WSHR, WMHR, RDI, RC, WSC, WMC, CUSTQ };

        // format enums
        // Must match the order of the texts in the Format dropdown!
        private enum EFormat { INT16, UINT16, HEX16, INT32, UINT32, HEX32, HEXBYTES, BITS };

        #region properties

        public int HoldingRegistersStartAddress
        {
            get
            {
                return mHoldingRegistersStartAddress;
            }

            set
            {
                // ignored
                mHoldingRegistersStartAddress = value; 
            }
        }

        public bool MultilineAutosizeRows
        {
            get
            {
                return chkMultilineRows.Checked;
            }

            set
            {
                chkMultilineRows.Checked = value;
                SetMultilineRows(chkMultilineRows.Checked);
            }
        }

        public ColumnWidthsType ColumnWidthsInWeights
        {
            get
            {
                return GetColumnWidthsInWeights();
            }

            set
            {
                SetColumnWidthsInWeights( value);
            }
        }

        public List<CustomQueryItem> QueriesList
        {
            get
            {
                return GetQueriesList();
            }

            set
            {
                SetQueriesList(value);
            }
        }

        // class used to save/restore column widths
        public class ColumnWidthsType
        {
            public float sendWidth;
            public float slaveIDWidth;
            public float commandWidth;
            public float registerWidth;
            public float qtyWidth;
            public float formatWidth;
            public float dataWidth;
            public float resultWidth;
            public float commentWidth;
        }

        // class used to save/restore Queries list
        public class CustomQueryItem
        {
            public string slaveID;
            public string command;
            public string register;
            public string qty;
            public string format;
            public string data;
            public string comment;
        }

        #endregion

        public CustomQueries(MainForm parent)
        {
            InitializeComponent();

#if TESTBUILD
            butRunTests.Visible = true;
#endif

            mParent = parent;
            mModbus = parent.mb;

            originalFontSize = grid.Font.Size;

            //grid.RowTemplate.Height = 25;
            SetAutoSizeRows(true);
            SetMultilineRows(chkMultilineRows.Checked);
            this.DoubleBuffered = true;

            // enable mousewheel zoom
            this.MouseWheel += new MouseEventHandler(Form_MouseWheel);

#if TESTBUILD
            // add rows for testing

            AddQueryRow("RIR", "", "30001", "10", "INT16", "", "invalid slaveid");
            AddQueryRow("RIR", "a", "30001", "10", "INT16", "", "invalid slaveid");
            AddQueryRow("RIR", "1", "30001", "10", "INT16", "", "data");
            AddQueryRow("RIR", "255", "30001", "10", "INT16", "", "no response");
            AddQueryRow("RIR", "256", "30001", "10", "INT16", "", "invalid slaveid");
            AddQueryRow("RIR", "1", "", "10", "INT16", "", "invalid register");
            AddQueryRow("RIR", "1", "a", "10", "INT16", "", "invalid register");
            AddQueryRow("RIR", "1", "30001", "10", "INT16", "", "data");
            AddQueryRow("RIR", "1", "39999", "1", "INT16", "", "data");
            AddQueryRow("RIR", "1", "300001", "20", "INT16", "", "data");
            AddQueryRow("RIR", "1", "365536", "1", "INT16", "", "data");
            AddQueryRow("RIR", "1", "30001", "10", "INT16", "123", "data");
            AddQueryRow("RIR", "1", "30001", "", "INT16", "", "invalid qty");
            AddQueryRow("RIR", "1", "30001", "a", "INT16", "", "invalid qty");
            AddQueryRow("RIR", "1", "30001", "0", "INT16", "", "no data");
            AddQueryRow("RIR", "1", "30001", "1", "INT16", "", "data");
            AddQueryRow("RIR", "1", "30001", "10", "INT16", "", "data");
            AddQueryRow("RIR", "1", "30001", "125", "INT16", "", "data");
            AddQueryRow("RIR", "1", "30001", "200", "INT16", "", "error");
            AddQueryRow("RIR", "1", "30001", "10", "UINT16", "", "data");
            AddQueryRow("RIR", "1", "30001", "10", "HEX16", "", "data");
            AddQueryRow("RIR", "1", "30001", "10", "INT32", "", "data");
            AddQueryRow("RIR", "1", "30001", "10", "UINT32", "", "data");
            AddQueryRow("RIR", "1", "30001", "10", "HEX32", "", "data");
            AddQueryRow("RIR", "1", "30001", "9", "HEX32", "", "invalid qty");
            AddQueryRow("RIR", "1", "30001", "4", "HEXBYTES", "", "data");  
            AddQueryRow("RIR", "1", "30001", "1", "BITS", "", "invalid format");

            AddQueryRow("RHR", "", "----------", "----------", "INT16", "------------------------------", "----------");

            AddQueryRow("RHR", "", "40001", "1", "INT16", "", "invalid slaveid");
            AddQueryRow("RHR", "a", "40001", "1", "INT16", "", "invalid slaveid");
            AddQueryRow("RHR", "1", "40001", "1", "INT16", "", "data");
            AddQueryRow("RHR", "255", "40001", "1", "INT16", "", "no response");
            AddQueryRow("RHR", "256", "40001", "1", "INT16", "", "invalid slaveid");
            AddQueryRow("RHR", "1", "", "10", "INT16", "", "invalid register");
            AddQueryRow("RHR", "1", "a", "10", "INT16", "", "invalid register");
            AddQueryRow("RHR", "1", "40001", "10", "INT16", "", "data");
            AddQueryRow("RHR", "1", "49999", "1", "INT16", "", "data");
            AddQueryRow("RHR", "1", "400001", "20", "INT16", "", "data");
            AddQueryRow("RHR", "1", "465536", "1", "INT16", "", "data");
            AddQueryRow("RHR", "1", "40001", "10", "INT16", "123", "data");
            AddQueryRow("RHR", "1", "40001", "", "INT16", "", "invalid qty");
            AddQueryRow("RHR", "1", "40001", "a", "INT16", "", "invalid qty");
            AddQueryRow("RHR", "1", "40001", "0", "INT16", "", "no data");
            AddQueryRow("RHR", "1", "40001", "1", "INT16", "", "data");
            AddQueryRow("RHR", "1", "40001", "10", "INT16", "", "data");
            AddQueryRow("RHR", "1", "40001", "125", "INT16", "", "data");
            AddQueryRow("RHR", "1", "40001", "200", "INT16", "", "error");
            AddQueryRow("RHR", "1", "40001", "10", "UINT16", "", "data");
            AddQueryRow("RHR", "1", "40001", "10", "HEX16", "", "data");
            AddQueryRow("RHR", "1", "40001", "10", "INT32", "", "data");
            AddQueryRow("RHR", "1", "40001", "10", "UINT32", "", "data");
            AddQueryRow("RHR", "1", "40001", "10", "HEX32", "", "data");
            AddQueryRow("RHR", "1", "40001", "9", "HEX32", "", "invalid qty");
            AddQueryRow("RHR", "1", "40001", "8", "HEXBYTES", "", "data");
            AddQueryRow("RHR", "1", "40001", "1", "BITS", "", "invalid format");

            AddQueryRow("WSHR", "", "----------", "----------", "INT16", "------------------------------", "----------");

            AddQueryRow("WSHR", "", "40001", "1", "INT16", "101", "invalid slaveid");
            AddQueryRow("WSHR", "a", "40001", "1", "INT16", "101", "invalid slaveid");
            AddQueryRow("WSHR", "1", "40001", "1", "INT16", "101", "success");
            AddQueryRow("WSHR", "255", "40001", "1", "INT16", "101", "no response");
            AddQueryRow("WSHR", "256", "40001", "1", "INT16", "101", "invalid slaveid");
            AddQueryRow("WSHR", "1", "", "10", "INT16", "", "invalid register");
            AddQueryRow("WSHR", "1", "a", "10", "INT16", "", "invalid register");
            AddQueryRow("WSHR", "1", "40001", "1", "INT16", "108", "success");
            AddQueryRow("WSHR", "1", "49999", "1", "INT16", "102", "success");
            AddQueryRow("WSHR", "1", "400001", "1", "INT16", "106", "success");
            AddQueryRow("WSHR", "1", "465536", "1", "INT16", "107", "success");
            AddQueryRow("WSHR", "1", "40001", "", "INT16", "108", "success");
            AddQueryRow("WSHR", "1", "40001", "0", "INT16", "109", "success");
            AddQueryRow("WSHR", "1", "40001", "1", "INT16", "", "no data");
            AddQueryRow("WSHR", "1", "40001", "0", "INT16", "1.98", "invalid data");
            AddQueryRow("WSHR", "1", "40001", "0", "INT16", "101,102", "invalid data");
            AddQueryRow("WSHR", "1", "40001", "1", "INT16", "0", "success");
            AddQueryRow("WSHR", "1", "40001", "1", "INT16", "1", "success");
            AddQueryRow("WSHR", "1", "40001", "1", "INT16", "32000", "success");
            AddQueryRow("WSHR", "1", "40001", "1", "INT16", "-1", "success");
            AddQueryRow("WSHR", "1", "40001", "1", "INT16", "3001", "success");
            AddQueryRow("WSHR", "1", "40001", "1", "UINT16", "3002", "success");
            AddQueryRow("WSHR", "1", "40001", "1", "HEX16", "3003", "success");
            AddQueryRow("WSHR", "1", "40001", "1", "INT32", "3004", "invalid format");
            AddQueryRow("WSHR", "1", "40001", "1", "HEXBYTES", "3005", "invalid format");
            AddQueryRow("WSHR", "1", "40001", "1", "BITS", "3006", "invalid format");

            AddQueryRow("WMHR", "", "----------", "----------", "INT16", "------------------------------", "----------");

            AddQueryRow("WMHR", "", "40001", "1", "INT16", "101", "invalid slaveid");
            AddQueryRow("WMHR", "a", "40001", "1", "INT16", "101", "invalid slaveid");
            AddQueryRow("WMHR", "1", "40001", "1", "INT16", "101", "success");
            AddQueryRow("WMHR", "255", "40001", "1", "INT16", "101", "no response");
            AddQueryRow("WMHR", "256", "40001", "1", "INT16", "101", "invalid slaveid");
            AddQueryRow("WMHR", "1", "", "10", "INT16", "", "invalid register");
            AddQueryRow("WMHR", "1", "a", "10", "INT16", "", "invalid register");
            AddQueryRow("WMHR", "1", "40001", "1", "INT16", "401", "success");
            AddQueryRow("WMHR", "1", "49999", "1", "INT16", "402", "success");
            AddQueryRow("WMHR", "1", "400001", "1", "INT16", "403", "success");
            AddQueryRow("WMHR", "1", "465536", "1", "INT16", "404", "success");
            AddQueryRow("WMHR", "1", "40001", "0", "INT16", "", "success");
            AddQueryRow("WMHR", "1", "40001", "1", "INT16", "", "no data");
            AddQueryRow("WMHR", "1", "40001", "1", "INT16", "0", "success");
            AddQueryRow("WMHR", "1", "40001", "1", "INT16", "32000", "success");
            AddQueryRow("WMHR", "1", "40001", "1", "INT16", "-1", "success");
            AddQueryRow("WMHR", "1", "40001", "1", "INT16", "-1, 46", "invalid data");
            AddQueryRow("WMHR", "1", "40001", "2", "INT16", "-1, 46", "success");
            AddQueryRow("WMHR", "1", "40001", "7", "INT16", "1001, 1002, 1003, -1004, -1005, 0x1006, 0x89AB", "success");
            AddQueryRow("WMHR", "1", "40001", "7", "UINT16", "2001, 2002, 2003, -2004, -2005, 0x2006, 0x99AB", "success");
            AddQueryRow("WMHR", "1", "40001", "7", "HEX16", "3001, 3002, 3003, -3004, -3005, 0x3006, 0xA9AB", "success");
            AddQueryRow("WMHR", "1", "40003", "110", "INT16", "21001, 21002, 0x457A, 21004, -21005, 21006, 21007, 21008, 21009, 21010, 21011, 21012, 21013, 21014, 21015, 21016, 21017, 21018, 21019, 21020, 21021, 21022, 21023, 21024, 21025, 21026, 21027, 21028, 21029, 21030, 21031, 21032, 21033, 21034, 21035, 21036, 21037, 21038, 21039, 21040, 21041, 21042, 21043, 21044, 21045, 21046, 21047, 21048, 21049, 21050, 21051, 21052, 21053, 21054, 21055, 21056, 21057, 21058, 21059, 21060, 21061, 21062, 21063, 21064, 21065, 21066, 21067, 21068, 21069, 21070, 21071, 21072, 21073, 21074, 21075, 21076, 21077, 21078, 21079, 21080, 21081, 21082, 21083, 21084, 21085, 21086, 21087, 21088, 21089, 21090, 21091, 21092, 21093, 21094, 21095, 21096, 21097, 21098, 21099, 21100, 21101, 21102, 21103, 21104, 21105, 21106, 21107, 21108, 21109, 21110", "success");
            AddQueryRow("WMHR", "1", "40001", "1", "INT32", "1000000", "invalid qty");
            AddQueryRow("WMHR", "1", "40001", "2", "INT32", "1000000", "success");
            AddQueryRow("WMHR", "1", "40001", "6", "INT32", "1000000000, 1000000001", "invalid data");
            AddQueryRow("WMHR", "1", "40001", "8", "INT32", "1000000, -1000001, 4000000000, 0x49A9C916", "success");
            AddQueryRow("WMHR", "1", "40001", "2", "UINT32", "1000000", "success");
            AddQueryRow("WMHR", "1", "40001", "4", "HEX32", "1000001000, 2000001000", "success");
            AddQueryRow("WMHR", "1", "40001", "1", "HEXBYTES", "31", "invalid format"); 
            AddQueryRow("WMHR", "1", "40001", "1", "BITS", "32", "invalid format");

            AddQueryRow("RDI", "", "----------", "----------", "INT16", "------------------------------", "----------");

            AddQueryRow("RDI", "", "10001", "1", "BITS", "", "invalid slaveid");
            AddQueryRow("RDI", "a", "10001", "1", "BITS", "", "invalid slaveid");
            AddQueryRow("RDI", "1", "10001", "1", "BITS", "", "data");
            AddQueryRow("RDI", "255", "10001", "1", "BITS", "", "no response");
            AddQueryRow("RDI", "256", "10001", "1", "BITS", "", "invalid slaveid");
            AddQueryRow("RDI", "1", "", "10", "BITS", "", "invalid address");
            AddQueryRow("RDI", "1", "a", "10", "BITS", "", "invalid address");
            AddQueryRow("RDI", "1", "10001", "1", "BITS", "", "data");
            AddQueryRow("RDI", "1", "19999", "1", "BITS", "", "data");
            AddQueryRow("RDI", "1", "100001", "1", "BITS", "", "data");
            AddQueryRow("RDI", "1", "165536", "1", "BITS", "", "data");
            AddQueryRow("RDI", "1", "10001", "10", "BITS", "123", "data");
            AddQueryRow("RDI", "1", "10001", "0", "BITS", "", "no data");
            AddQueryRow("RDI", "1", "10001", "1", "BITS", "", "data");
            AddQueryRow("RDI", "1", "10001", "10", "BITS", "", "data");
            AddQueryRow("RDI", "1", "10001", "100", "BITS", "", "data");
            AddQueryRow("RDI", "1", "10001", "2000", "BITS", "", "data");
            AddQueryRow("RDI", "1", "10001", "3000", "BITS", "", "error");
            AddQueryRow("RDI", "1", "10001", "1", "HEXBYTES", "", "data");
            AddQueryRow("RDI", "1", "10001", "100", "HEXBYTES", "", "data");
            AddQueryRow("RDI", "1", "10001", "100", "INT16", "", "invalid format");
            AddQueryRow("RDI", "1", "10001", "100", "UINT16", "", "invalid format");
            AddQueryRow("RDI", "1", "10001", "100", "HEX16", "", "invalid format");
            AddQueryRow("RDI", "1", "10001", "100", "INT32", "", "invalid format");
            AddQueryRow("RDI", "1", "10001", "100", "UINT32", "", "invalid format");
            AddQueryRow("RDI", "1", "10001", "100", "HEX32", "", "invalid format");

            AddQueryRow("RC", "", "----------", "----------", "INT16", "------------------------------", "----------");

            AddQueryRow("RC", "", "1", "1", "BITS", "", "invalid slaveid");
            AddQueryRow("RC", "1", "", "1", "BITS", "", "invalid address");
            AddQueryRow("RC", "1", "a", "1", "BITS", "", "invalid address");
            AddQueryRow("RC", "1", "0", "10", "BITS", "", "invalid address");
            AddQueryRow("RC", "1", "1", "10", "BITS", "", "data");
            AddQueryRow("RC", "1", "9999", "10", "BITS", "", "data");
            AddQueryRow("RC", "1", "10000", "10", "BITS", "", "invalid address");
            AddQueryRow("RC", "1", "40001", "10", "BITS", "", "invalid address");
            AddQueryRow("RC", "1", "1", "", "BITS", "", "invalid qty");
            AddQueryRow("RC", "1", "1", "10", "BITS", "123", "data");
            AddQueryRow("RC", "1", "1", "0", "BITS", "", "no data");
            AddQueryRow("RC", "1", "1", "1", "BITS", "", "data");
            AddQueryRow("RC", "1", "1", "10", "BITS", "", "data");
            AddQueryRow("RC", "1", "1", "100", "BITS", "", "data");
            AddQueryRow("RC", "1", "1", "2000", "BITS", "", "data");
            AddQueryRow("RC", "1", "1", "3000", "BITS", "", "error");
            AddQueryRow("RC", "1", "1", "100", "HEXBYTES", "", "data");
            AddQueryRow("RC", "1", "1", "100", "BITS", "", "data");
            AddQueryRow("RC", "1", "1", "100", "INT16", "", "invalid format");
            AddQueryRow("RC", "1", "1", "100", "UINT16", "", "invalid format");
            AddQueryRow("RC", "1", "1", "100", "HEX16", "", "invalid format");
            AddQueryRow("RC", "1", "1", "100", "INT32", "", "invalid format");
            AddQueryRow("RC", "1", "1", "100", "UINT32", "", "invalid format");
            AddQueryRow("RC", "1", "1", "100", "HEX32", "", "invalid format");

            AddQueryRow("WSC", "", "----------", "----------", "INT16", "------------------------------", "----------");

            AddQueryRow("WSC", "", "1", "1", "BITS", "0", "invalid slaveid");
            AddQueryRow("WSC", "a", "1", "1", "BITS", "0", "invalid slaveid");
            AddQueryRow("WSC", "1", "1", "1", "BITS", "", "no data");
            AddQueryRow("WSC", "255", "1", "1", "BITS", "0", "no response");
            AddQueryRow("WSC", "256", "1", "1", "BITS", "0", "invalid slaveid");
            AddQueryRow("WSC", "1", "", "1", "BITS", "1", "invalid address");
            AddQueryRow("WSC", "1", "a", "1", "BITS", "1", "invalid address");
            AddQueryRow("WSC", "1", "0", "10", "BITS", "", "invalid address");
            AddQueryRow("WSC", "1", "1", "10", "BITS", "", "no data");
            AddQueryRow("WSC", "1", "9999", "10", "BITS", "", "no data");
            AddQueryRow("WSC", "1", "10000", "10", "BITS", "", "invalid address");
            AddQueryRow("WSC", "1", "40001", "10", "BITS", "", "invalid address");
            AddQueryRow("WSC", "1", "1", "2", "BITS", "1", "success");
            AddQueryRow("WSC", "1", "1", "1", "BITS", "", "no data");
            AddQueryRow("WSC", "1", "1", "1", "BITS", "1,0", "invalid data");
            AddQueryRow("WSC", "1", "1", "1", "INT16", "1", "invalid format");
            AddQueryRow("WSC", "1", "1", "1", "UINT16", "1", "invalid format");
            AddQueryRow("WSC", "1", "1", "1", "HEX16", "1", "invalid format");
            AddQueryRow("WSC", "1", "1", "1", "INT32", "1", "invalid format");
            AddQueryRow("WSC", "1", "1", "1", "UINT32", "1", "invalid format");
            AddQueryRow("WSC", "1", "1", "1", "HEX32", "1", "invalid format");

            AddQueryRow("WMC", "", "----------", "----------", "INT16", "------------------------------", "----------");

            AddQueryRow("WMC", "", "1", "1", "BITS", "1", "invalid slaveid");
            AddQueryRow("WMC", "a", "1", "1", "BITS", "1", "invalid slaveid");
            AddQueryRow("WMC", "1", "1", "1", "BITS", "1", "success");
            AddQueryRow("WMC", "255", "1", "1", "BITS", "1", "no response");
            AddQueryRow("WMC", "256", "1", "1", "BITS", "1", "invalid slaveid");
            AddQueryRow("WMC", "1", "", "10", "BITS", "1,0,0,0,1,0,0,1,1,0", "invalid address");
            AddQueryRow("WMC", "1", "a", "10", "BITS", "1,0,0,0,1,0,0,1,1,0", "invalid address");
            AddQueryRow("WMC", "1", "0", "10", "BITS", "", "invalid address");
            AddQueryRow("WMC", "1", "1", "10", "BITS", "", "no data");
            AddQueryRow("WMC", "1", "9999", "10", "BITS", "", "no data");
            AddQueryRow("WMC", "1", "10000", "10", "BITS", "", "invalid address");
            AddQueryRow("WMC", "1", "40001", "10", "BITS", "", "invalid address");
            AddQueryRow("WMC", "1", "2", "", "BITS", "1,0,0,0,1,0,0,1,1,0", "invalid qty");
            AddQueryRow("WMC", "1", "2", "10", "BITS", "1,0,0,0,1,0,0,1,1", "invalid data");
            AddQueryRow("WMC", "1", "2", "10", "BITS", "1,0,0,0,1,0,0,1,1 , 0,", "success");
            AddQueryRow("WMC", "1", "2", "10", "BITS", "1,0,0,0,1,0,0,1,1,0,1", "invalid data");
            AddQueryRow("WMC", "1", "1", "1", "INT16", "0", "invalid format");
            AddQueryRow("WMC", "1", "1", "1", "UINT16", "0", "invalid format");
            AddQueryRow("WMC", "1", "1", "1", "HEX16", "0", "invalid format");
            AddQueryRow("WMC", "1", "1", "1", "INT32", "0", "invalid format");
            AddQueryRow("WMC", "1", "1", "1", "UINT32", "0", "invalid format");
            AddQueryRow("WMC", "1", "1", "1", "HEX32", "0", "invalid format");

            AddQueryRow("CUSTQ", "", "----------", "----------", "HEXBYTES", "------------------------------", "----------");

            AddQueryRow("CUSTQ", "", "", "", "HEXBYTES", "0x03,0x00,0x10,0x00,0x11", "invalid slaveid");
            AddQueryRow("CUSTQ", "a", "", "", "HEXBYTES", "0x03,0x00,0x10,0x00,0x11", "invalid slaveid");
            AddQueryRow("CUSTQ", "1", "a", "", "HEXBYTES", "0x03,0x00,0x10,0x00,0x11", "data");
            AddQueryRow("CUSTQ", "255", "a", "", "HEXBYTES", "0x03,0x00,0x10,0x00,0x11", "no response");
            AddQueryRow("CUSTQ", "1", "", "1", "HEXBYTES", "0x03,0x00,0x10,0x00,0x11", "data");
            AddQueryRow("CUSTQ", "1", "", "", "HEXBYTES", "0x03,0x00,0x10,0x00,0x11", "data");
            AddQueryRow("CUSTQ", "1", "", "", "HEXBYTES", "0x03,0x00, 0x10,  0x00  ,0x11 ", "data");
            AddQueryRow("CUSTQ", "1", "", "", "HEXBYTES", "3, 0x00, 16,  0x00  ,17 ", "data");
            AddQueryRow("CUSTQ", "1", "", "", "HEXBYTES", "3, 0x00x, 16,  0x00  ,17 ", "invalid data");
            AddQueryRow("CUSTQ", "1", "", "", "HEXBYTES", "", "no data");
            AddQueryRow("CUSTQ", "1", "", "", "INT16", "0x03,0x00,0x10,0x00,0x11", "invalid format");
            AddQueryRow("CUSTQ", "1", "", "", "UINT16", "0x03,0x00,0x10,0x00,0x11", "invalid format");
            AddQueryRow("CUSTQ", "1", "", "", "HEX16", "0x03,0x00,0x10,0x00,0x11", "invalid format");
            AddQueryRow("CUSTQ", "1", "", "", "INT32", "0x03,0x00,0x10,0x00,0x11", "invalid format");
            AddQueryRow("CUSTQ", "1", "", "", "UINT32", "0x03,0x00,0x10,0x00,0x11", "invalid format");
            AddQueryRow("CUSTQ", "1", "", "", "HEX32", "0x03,0x00,0x10,0x00,0x11", "invalid format");

#endif
        }

        private void AddQueryRow(string commandCode, string slaveid, string reg, string qty, string formatCode, string data, string comment, int pos = -1)
        {
            int rowIndex = grid.Rows.Count;

            ECommand cmd = (ECommand)Enum.Parse(typeof(ECommand), commandCode);
            string commandString = GetCommandTextFromEnum(cmd);

            EFormat fmt = (EFormat)Enum.Parse(typeof(EFormat), formatCode);
            string formatString = GetFormatTextFromEnum(fmt);

            if (pos >= 0)
            {
                grid.Rows.Insert(pos, "", slaveid, commandString, reg, qty, formatString, data, "", comment);
            }
            else
            {
                grid.Rows.Add("", slaveid, commandString, reg, qty, formatString, data, "", comment);
            }
        }

        private void CustomQueries_FormClosing(object sender, FormClosingEventArgs e)
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
                    ZoomFactor *= changeFactor;
                }
            }
            else if (action < 0)
            {
                // zoom out
                if (ZoomFactor > minZoom)
                {
                    ZoomFactor /= changeFactor;
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

        private void CustomQueries_Shown(object sender, EventArgs e)
        {
        }

        private void MyDataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if ((senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn)
                && (e.RowIndex >= 0))
            {
                SendRow(e.RowIndex);
            }
        }
        private void SendRow(int rowIndex)
        {
            Debug.Print(DateTime.Now.TimeOfDay + ": SendRow: " + rowIndex.ToString());

            string slaveidStr = IsNull(grid.Rows[rowIndex].Cells[SlaveidColumn.Index].Value, "");
            string commandStr = IsNull(grid.Rows[rowIndex].Cells[CommandColumn.Index].Value, "");
            ECommand command = GetCommandEnumFromText(commandStr);
            string registerStr = IsNull(grid.Rows[rowIndex].Cells[RegisterColumn.Index].Value, "");
            string qtyStr = IsNull(grid.Rows[rowIndex].Cells[QtyColumn.Index].Value, "");
            string formatStr = IsNull(grid.Rows[rowIndex].Cells[FormatColumn.Index].Value, "");
            EFormat format = GetFormatEnumFromText(formatStr);
            string dataStr = IsNull(grid.Rows[rowIndex].Cells[DataColumn.Index].Value, "");

            string ResultStr = "INTERNAL ERROR";

            if (command == ECommand.RIR)
            {
                // data empty 
                grid.Rows[rowIndex].Cells[DataColumn.Index].Value = string.Empty;
                dataStr = string.Empty;

                ResultStr = SendReadInputreg(slaveidStr, registerStr, qtyStr, format, dataStr);
            }
            else if (command == ECommand.RHR)
            {
                // data empty 
                grid.Rows[rowIndex].Cells[DataColumn.Index].Value = string.Empty;
                dataStr = string.Empty;

                ResultStr = SendReadHoldingreg(slaveidStr, registerStr, qtyStr, format, dataStr);

                /*
                // update count
                string countStr = isnull(grid.Rows[rowIndex].Cells[CountColumn.Index].Value, "");
                UInt32 tmpCount = 0;
                UInt32.TryParse(countStr, out tmpCount);
                tmpCount += 1;
                grid.Rows[rowIndex].Cells[CountColumn.Index].Value = tmpCount.ToString();
                */
            }
            else if (command == ECommand.WSHR)
            {
                // qty = 1
                grid.Rows[rowIndex].Cells[QtyColumn.Index].Value = "1";
                qtyStr = "1";

                ResultStr = SendWriteSingleHoldingreg(slaveidStr, registerStr, qtyStr, format, dataStr);
            }
            else if (command == ECommand.WMHR)
            {
                ResultStr = SendWriteMultipleHoldingreg(slaveidStr, registerStr, qtyStr, format, dataStr);
            }
            else if (command == ECommand.RDI)
            {
                // data empty 
                grid.Rows[rowIndex].Cells[DataColumn.Index].Value = string.Empty;
                dataStr = string.Empty;

                ResultStr = SendReadDiscreteinputs(slaveidStr, registerStr, qtyStr, format, dataStr);
            }
            else if (command == ECommand.RC)
            {
                // data empty 
                grid.Rows[rowIndex].Cells[DataColumn.Index].Value = string.Empty;
                dataStr = string.Empty;

                ResultStr = SendReadCoils(slaveidStr, registerStr, qtyStr, format, dataStr);
            }
            else if (command == ECommand.WSC)
            {
                // qty = 1
                grid.Rows[rowIndex].Cells[QtyColumn.Index].Value = "1";
                qtyStr = "1";

                ResultStr = SendWriteSingleCoil(slaveidStr, registerStr, qtyStr, format, dataStr);
            }
            else if (command == ECommand.WMC)
            {
                ResultStr = SendWriteMultipleCoils(slaveidStr, registerStr, qtyStr, format, dataStr);
            }
            else if (command == ECommand.CUSTQ)
            {
                // register empty
                grid.Rows[rowIndex].Cells[RegisterColumn.Index].Value = string.Empty;
                registerStr = string.Empty;

                // qty empty
                grid.Rows[rowIndex].Cells[QtyColumn.Index].Value = string.Empty;
                qtyStr = string.Empty;

                ResultStr = SendCustomQuery(slaveidStr, registerStr, qtyStr, format, dataStr);
            }

            // show result
            grid.Rows[rowIndex].Cells[ResultColumn.Index].Value = ResultStr;
        }

        private static string IsNull(object v, string repl)
        {
            if (v != null)
                return (string)v;
            return repl;
        }

        private string SendReadInputreg(string slaveIDStr, string registerStr, string qtyStr, EFormat format, string dataStr)
        {
            string ResultStr = "ERROR";
            byte slaveID = 1;
            UInt32 register = 0;
            UInt16 regOffset = 0;
            UInt16 regQuantity = 0;
            bool Continue = true;

            // check slaveID
            if (byte.TryParse(slaveIDStr, out slaveID))
            {
                // okay
            }
            else
            {
                ResultStr = "Error: Invalid slaveID";
                Continue = false;
            }

            if (Continue)
            {
                if (uint.TryParse(registerStr, out register)
                    && TryInputregisterGetOffset30001(register, out regOffset))
                {
                    // okay
                }
                else
                {
                    ResultStr = "Error: Invalid register";
                    Continue = false;
                }
            }

            if (Continue)
            {
                if (UInt16.TryParse(qtyStr, out regQuantity))
                {
                    // okay
                }
                else
                {
                    ResultStr = "Error: Invalid qty";
                    Continue = false;
                }
            }

            if (Continue)
            {
                if (format == EFormat.INT16 || format == EFormat.UINT16 || format == EFormat.HEX16
                    || format == EFormat.INT32 || format == EFormat.UINT32 || format == EFormat.HEX32
                    || format == EFormat.HEXBYTES)
                {
                    // OK
                }
                else
                {
                    ResultStr = "Error: Invalid format";
                    Continue = false;
                }
            }

            if (Continue)
            {
                // Read
                Int16[] aValues;
                ModbusProtocol.Result result = mModbus.MifReadInputRegisters(slaveID, regOffset, regQuantity, out aValues, true);
                if (result == ModbusProtocol.Result.SUCCESS)
                {
                    // ok
                    ResultStr = ConvertRegistersToResultString(aValues, format);
                }
                else
                {
                    // error
                    ResultStr = GetErrorTextFromQueryResult(result);
                }
            }

            return ResultStr;
        }

        private string SendReadHoldingreg(string slaveIDStr, string registerStr, string qtyStr, EFormat format, string dataStr)
        {
            string ResultStr = "ERROR";
            byte slaveID = 1;
            UInt32 register = 0;
            UInt16 regOffset = 0;
            UInt16 regQuantity = 0;
            bool Continue = true;

            // check slaveID
            if (byte.TryParse(slaveIDStr, out slaveID))
            {
                // okay
            }
            else
            {
                ResultStr = "Error: Invalid slaveID";
                Continue = false;
            }

            if (Continue)
            {
                if (uint.TryParse(registerStr, out register)
                    && TryHoldingregisterGetOffset40001(register, out regOffset))
                {
                    // okay
                }
                else
                {
                    ResultStr = "Error: Invalid register";
                    Continue = false;
                }
            }

            if (Continue)
            {
                if (UInt16.TryParse(qtyStr, out regQuantity))
                {
                    // okay
                }
                else
                {
                    ResultStr = "Error: Invalid qty";
                    Continue = false;
                }
            }

            if (Continue)
            {
                if (format == EFormat.INT16 || format == EFormat.UINT16 || format == EFormat.HEX16
                    || format == EFormat.INT32 || format == EFormat.UINT32 || format == EFormat.HEX32
                    || format == EFormat.HEXBYTES)
                {
                    // OK
                }
                else
                {
                    ResultStr = "Error: Invalid format";
                    Continue = false;
                }
            }

            if (Continue)
            {
                // Read
                Int16[] aValues;
                ModbusProtocol.Result result = mModbus.MifReadHoldingRegisters(slaveID, regOffset, regQuantity, out aValues, true);
                if (result == ModbusProtocol.Result.SUCCESS)
                {
                    // ok
                    ResultStr = ConvertRegistersToResultString(aValues, format);
                }
                else
                {
                    // error
                    ResultStr = GetErrorTextFromQueryResult(result);
                }
            }

            return ResultStr;
        }

        private string SendWriteSingleHoldingreg(string slaveIDStr, string registerStr, string qtyStr, EFormat format, string dataStr)
        {
            string ResultStr = "ERROR";
            byte slaveID = 1;
            UInt32 register = 0;
            UInt16 regOffset = 0;
            UInt16 regQuantity = 0;
            Int16 value16 = 0;
            bool Continue = true;

            // check slaveID
            if (byte.TryParse(slaveIDStr, out slaveID))
            {
                // okay
            }
            else
            {
                ResultStr = "Error: Invalid slaveID";
                Continue = false;
            }

            if (Continue)
            {
                if (uint.TryParse(registerStr, out register)
                    && TryHoldingregisterGetOffset40001(register, out regOffset))
                {
                    // okay
                }
                else
                {
                    ResultStr = "Error: Invalid register";
                    Continue = false;
                }
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
                    ResultStr = "Error: Invalid qty";
                    Continue = false;
                }
            }

            if (Continue)
            {
                if ((regQuantity > 0) && (dataStr.Trim() == string.Empty))
                {
                    ResultStr = "Error: No data";
                    Continue = false;
                }
                else
                {
                    // okay
                }
            }

            if (Continue)
            {
                // force valid format
                if (format == EFormat.INT16 || format == EFormat.UINT16 || format == EFormat.HEX16)
                {
                    // OK
                }
                else
                {
                    ResultStr = "Error: Invalid format";
                    Continue = false;
                }
            }

            if (Continue)
            {
                // get 1 16-bit value
                try
                {
                    Int16[] aValues = ParseValuesStringInto16bitValues(dataStr, 1);
                    value16 = aValues[0];
                }
                catch (Exception e)
                {
                    ResultStr = "Error: " + e.Message;
                    Continue = false;
                }
            }

            if (Continue)
            {
                // Write
                ModbusProtocol.Result result = mModbus.MifWriteSingleHoldingRegister(slaveID, regOffset, value16);
                if (result == ModbusProtocol.Result.SUCCESS)
                {
                    // ok
                    ResultStr = "Success";
                }
                else
                {
                    // error
                    ResultStr = GetErrorTextFromQueryResult(result);
                }
            }

            return ResultStr;
        }

        private string SendWriteMultipleHoldingreg(string slaveIDStr, string registerStr, string qtyStr, EFormat format, string dataStr)
        {
            string ResultStr = "ERROR";
            byte slaveID = 1;
            UInt32 register = 0;
            UInt16 regOffset = 0;
            UInt16 regQuantity = 0;
            Int16[] aValues16 = null;
            bool Continue = true;

            // check slaveID
            if (byte.TryParse(slaveIDStr, out slaveID))
            {
                // okay
            }
            else
            {
                ResultStr = "Error: Invalid slaveID";
                Continue = false;
            }

            if (Continue)
            {
                if (uint.TryParse(registerStr, out register)
                    && TryHoldingregisterGetOffset40001(register, out regOffset))
                {
                    // okay
                }
                else
                {
                    ResultStr = "Error: Invalid register";
                    Continue = false;
                }
            }

            if (Continue)
            {
                if (UInt16.TryParse(qtyStr, out regQuantity))
                {
                    // okay
                }
                else
                {
                    ResultStr = "Error: Invalid qty";
                    Continue = false;
                }
            }

            if (Continue)
            {
                if ((regQuantity > 0) && (dataStr.Trim() == string.Empty))
                {
                    ResultStr = "Error: No data";
                    Continue = false;
                }
                else
                {
                    // okay
                }
            }

            // aantal values parsen, dat moet matchen met qty, mag 0 zijn
            if (Continue)
            {
                if (format == EFormat.INT16 || format == EFormat.UINT16 || format == EFormat.HEX16)
                {
                    try
                    {
                        aValues16 = ParseValuesStringInto16bitValues(dataStr, regQuantity);
                    }
                    catch (Exception e)
                    {
                        ResultStr = "Error: " + e.Message;
                        Continue = false;
                    }
                }
                else if (format == EFormat.INT32 || format == EFormat.UINT32 || format == EFormat.HEX32)
                {
                    if ((regQuantity % 2) != 0)
                    {
                        ResultStr = "Error: Qty must be multiple of 2 for 32-bit values";
                        Continue = false;
                    }
                    if (Continue)
                    {
                        try
                        {
                            Int32[] aValues32 = ParseValuesStringInto32bitValues(dataStr, regQuantity / 2);
                            aValues16 = ConvertInt32ArrayToInt16Array(aValues32);
                        }
                        catch (Exception e)
                        {
                            ResultStr = "Error: " + e.Message;
                            Continue = false;
                        }
                    }
                }
                else
                {
                    ResultStr = "Error: Invalid format";
                    Continue = false;
                }
            }

            if (Continue)
            {
                // Write
                ModbusProtocol.Result result = mModbus.MifWriteMultipleHoldingRegisters(slaveID, regOffset, regQuantity, aValues16);
                if (result == ModbusProtocol.Result.SUCCESS)
                {
                    // ok
                    ResultStr = "Success";
                }
                else
                {
                    // error
                    ResultStr = GetErrorTextFromQueryResult(result);
                }
            }

            return ResultStr;
        }

        private string SendReadDiscreteinputs(string slaveIDStr, string registerStr, string qtyStr, EFormat format, string dataStr)
        {
            string ResultStr = "ERROR";
            byte slaveID = 1;
            UInt32 register = 0;
            UInt16 regOffset = 0;
            UInt16 regQuantity = 0;
            bool Continue = true;

            // check slaveID
            if (byte.TryParse(slaveIDStr, out slaveID))
            {
                // okay
            }
            else
            {
                ResultStr = "Error: Invalid slaveID";
                Continue = false;
            }

            if (Continue)
            {
                if (uint.TryParse(registerStr, out register)
                    && TryDiscreteInputGetOffset10001(register, out regOffset))
                {
                    // okay
                }
                else
                {
                    ResultStr = "Error: Invalid address";
                    Continue = false;
                }
            }

            if (Continue)
            {
                if (UInt16.TryParse(qtyStr, out regQuantity))
                {
                    // okay
                }
                else
                {
                    ResultStr = "Error: Invalid qty";
                    Continue = false;
                }
            }

            if (Continue)
            {
                // Read
                byte[] aBytes;
                ModbusProtocol.Result result = mModbus.MifReadDiscreteInputs(slaveID, regOffset, regQuantity, out aBytes, true);
                if (result == ModbusProtocol.Result.SUCCESS)
                {
                    if (format == EFormat.BITS)
                    {
                        // bits as 0, 0, 1, 0
                        ResultStr = ConvertPackedBitsToResultString(aBytes, (int)regQuantity);
                    } 
                    else if (format == EFormat.HEXBYTES)
                    {
                        // bytes as 0x01, 0xFA, ...
                        ResultStr = Converts.ByteArrayToStringOfValues(aBytes);
                    }
                    else
                    {
                        ResultStr = "Error: Invalid format";
                        Continue = false;
                    }
                }
                else
                {
                    // error
                    ResultStr = GetErrorTextFromQueryResult(result);
                }
            }

            return ResultStr;
        }

        private string SendReadCoils(string slaveIDStr, string registerStr, string qtyStr, EFormat format, string dataStr)
        {
            string ResultStr = "ERROR";
            byte slaveID = 1;
            UInt32 register = 0;
            UInt16 regOffset = 0;
            UInt16 regQuantity = 0;
            bool Continue = true;

            // check slaveID
            if (byte.TryParse(slaveIDStr, out slaveID))
            {
                // okay
            }
            else
            {
                ResultStr = "Error: Invalid slaveID";
                Continue = false;
            }

            if (Continue)
            {
                if (format == EFormat.BITS || format == EFormat.HEXBYTES)
                {
                    // OK
                }
                else
                {
                    ResultStr = "Error: Invalid format";
                    Continue = false;
                }
            }

            if (Continue)
            {
                if (uint.TryParse(registerStr, out register)
                    && TryCoilGetOffset1(register, out regOffset))
                {
                    // okay
                }
                else
                {
                    ResultStr = "Error: Invalid address";
                    Continue = false;
                }
            }

            if (Continue)
            {
                if (UInt16.TryParse(qtyStr, out regQuantity))
                {
                    // okay
                }
                else
                {
                    ResultStr = "Error: Invalid qty";
                    Continue = false;
                }
            }

            if (Continue)
            {
                // Read
                byte[] aBytes;
                ModbusProtocol.Result result = mModbus.MifReadCoils(slaveID, regOffset, regQuantity, out aBytes, true);
                if (result == ModbusProtocol.Result.SUCCESS)
                {
                    if (format == EFormat.BITS)
                    {
                        if (aBytes.Length > 0)
                        {
                            // bits as 0,1,1,0,0,1,1,0, ...
                            ResultStr = ConvertPackedBitsToResultString(aBytes, (int)regQuantity);
                        }
                        else
                        {
                            ResultStr = "(no data)";
                        }
                    }
                    else if (format == EFormat.HEXBYTES)
                    {
                        if (aBytes.Length > 0)
                        {
                            // bytes as 0x01, 0xFA, ...
                            ResultStr = Converts.ByteArrayToStringOfValues(aBytes);
                        }
                        else
                        {
                            ResultStr = "(no data)";
                        }
                    }
                    else
                    {
                        ResultStr = "Error: Invalid format";
                        Continue = false;
                    }
                }
                else
                {
                    // error
                    ResultStr = GetErrorTextFromQueryResult(result);
                }
            }

            return ResultStr;
        }

        private string SendWriteSingleCoil(string slaveIDStr, string registerStr, string qtyStr, EFormat format, string dataStr)
        {
            string ResultStr = "ERROR";
            byte slaveID = 1;
            UInt32 register = 0;
            UInt16 regOffset = 0;
            UInt16 regQuantity = 0;
            byte[] aBytes = null;
            bool Continue = true;

            // check slaveID
            if (byte.TryParse(slaveIDStr, out slaveID))
            {
                // okay
            }
            else
            {
                ResultStr = "Error: Invalid slaveID";
                Continue = false;
            }

            if (Continue)
            {
                if (uint.TryParse(registerStr, out register)
                    && TryCoilGetOffset1(register, out regOffset))
                {
                    // okay
                }
                else
                {
                    ResultStr = "Error: Invalid address";
                    Continue = false;
                }
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
                    ResultStr = "Error: Invalid qty";
                    Continue = false;
                }
            }

            if (Continue)
            {
                if ((regQuantity > 0) && (dataStr.Trim() == string.Empty))
                {
                    ResultStr = "Error: No data";
                    Continue = false;
                }
                else
                {
                    // okay
                }
            }

            if (Continue)
            {
                if (format == EFormat.BITS)
                {
                    // value 0,1,1,0,1,0...
                    try
                    {
                        aBytes = ParseValuesStringIntoPackedBits(dataStr, 1);
                    }
                    catch (Exception e)
                    {
                        ResultStr = "Error: " + e.Message;
                        Continue = false;
                    }
                }
                else if (format == EFormat.HEXBYTES)
                {
                    // value 00 or 01
                    try
                    {
                        aBytes = ParseValuesStringIntoByteValues(dataStr, 1);

                        if ((aBytes[0] >= 0) && (aBytes[0] <= 1))
                        {
                            // OK
                        }
                        else
                        {
                            throw new Exception("Invalid value \"" + aBytes[0] + "\"");
                        }
                    }
                    catch (Exception e)
                    {
                        ResultStr = "Error: " + e.Message;
                        Continue = false;
                    }
                }                
                else
                {
                    ResultStr = "Error: Invalid format";
                    Continue = false;
                }
            }

            if (Continue)
            {
                // Write
                ModbusProtocol.Result result = mModbus.MifWriteSingleCoil(slaveID, regOffset, aBytes[0]);
                if (result == ModbusProtocol.Result.SUCCESS)
                {
                    // ok
                    ResultStr = "Success";
                }
                else
                {
                    // error
                    ResultStr = GetErrorTextFromQueryResult(result);
                }
            }

            return ResultStr;
        }

        private string SendWriteMultipleCoils(string slaveIDStr, string registerStr, string qtyStr, EFormat format, string dataStr)
        {
            string ResultStr = "ERROR";
            byte slaveID = 1;
            UInt32 register = 0;
            UInt16 regOffset = 0;
            UInt16 regQuantity = 0;
            byte[] aBytes = null;
            bool Continue = true;

            // check slaveID
            if (byte.TryParse(slaveIDStr, out slaveID))
            {
                // okay
            }
            else
            {
                ResultStr = "Error: Invalid slaveID";
                Continue = false;
            }

            if (Continue)
            {
                if (uint.TryParse(registerStr, out register)
                    && TryCoilGetOffset1(register, out regOffset))
                {
                    // okay
                }
                else
                {
                    ResultStr = "Error: Invalid address";
                    Continue = false;
                }
            }

            if (Continue)
            {
                if (UInt16.TryParse(qtyStr, out regQuantity))
                {
                    // okay
                }
                else
                {
                    ResultStr = "Error: Invalid qty";
                    Continue = false;
                }
            }

            if (Continue)
            {
                if ((regQuantity > 0) && (dataStr.Trim() == string.Empty))
                {
                    ResultStr = "Error: No data";
                    Continue = false;
                }
                else
                {
                    // okay
                }
            }

            if (Continue)
            {
                if (format == EFormat.BITS)
                {
                    // value 0,1,1,0,1,0...
                    try
                    {
                        aBytes = ParseValuesStringIntoPackedBits(dataStr, regQuantity);
                    }
                    catch (Exception e)
                    {
                        ResultStr = "Error: " + e.Message;
                        Continue = false;
                    }
                }
                else if (format == EFormat.HEXBYTES)
                {
                    // value 0x00 - 0xFF, passed like this to query 
                    try
                    {
                        int nrOfBytes = (regQuantity + 7) / 8;
                        aBytes = ParseValuesStringIntoByteValues(dataStr, nrOfBytes);
                    }
                    catch (Exception e)
                    {
                        ResultStr = "Error: " + e.Message;
                        Continue = false;
                    }
                }
                else
                {
                    ResultStr = "Error: Invalid format";
                    Continue = false;
                }
            }

            if (Continue)
            {
                // Write
                ModbusProtocol.Result result = mModbus.MifWriteMultipleCoils(slaveID, regOffset, regQuantity, aBytes);
                if (result == ModbusProtocol.Result.SUCCESS)
                {
                    // ok
                    ResultStr = "Success";
                }
                else
                {
                    // error
                    ResultStr = GetErrorTextFromQueryResult(result);
                }
            }

            return ResultStr;
        }

        private string SendCustomQuery(string slaveIDStr, string registerStr, string qtyStr, EFormat format, string dataStr)
        {
            string ResultStr = "ERROR";
            byte slaveID = 1;
            bool Continue = true;

            // check slaveID
            if (byte.TryParse(slaveIDStr, out slaveID))
            {
                // okay
            }
            else
            {
                ResultStr = "Error: Invalid slaveID";
                Continue = false;
            }

            if (Continue)
            {
                if (dataStr.Trim() == string.Empty)
                {
                    ResultStr = "Error: No data/query";
                    Continue = false;
                }
                else
                {
                    // okay
                }
            }            

            byte[] aQueryBytes = null;
            byte[] aResponseBuf = null;

            if (Continue)
            {
                if (format == EFormat.INT16 || format == EFormat.UINT16 || format == EFormat.HEX16
                    || format == EFormat.INT32 || format == EFormat.UINT32 || format == EFormat.HEX32
                    || format == EFormat.HEXBYTES)
                {
                    // OK
                }
                
            }

            if (Continue)
            {
                // query must be hexbytes
                if (format == EFormat.HEXBYTES)
                { 
                    try
                    {
                        aQueryBytes = ParseValuesStringIntoByteValues(dataStr);
                    }
                    catch (Exception e)
                    {
                        ResultStr = "Error: " + e.Message;
                        Continue = false;
                    }
                }
                else
                {
                    ResultStr = "Error: Invalid format";
                    Continue = false;
                }
            }

            if (Continue)
            {
                ModbusProtocol.Result result = mModbus.MifSendCustomQuery(slaveID, aQueryBytes, (UInt16)aQueryBytes.Length, out aResponseBuf);
                if (result == ModbusProtocol.Result.SUCCESS)
                {
                    // reponse is always hexbytes
                    if (format == EFormat.HEXBYTES)
                    {
                        // bytes as 0x01, 0xFA, ...
                        ResultStr = Converts.ByteArrayToStringOfValues(aResponseBuf);
                    }
                    // supporting other formats is difficult because we don't know the data format
                    else
                    {
                        ResultStr = "Error: Invalid format";
                        Continue = false;
                    }
                }
                else
                {
                    // error
                    ResultStr = GetErrorTextFromQueryResult(result);
                }
            }

            return ResultStr;
        }

        private string ConvertPackedBitsToResultString(byte[] aBytes, int qty = int.MinValue)
        {
            StringBuilder sb = new StringBuilder(7000);     // 2000 * "0, "

            // coil and discreteinputs are packed in bytes
            // first coil/discreteinput is in bit0 of first byte

            // default qty: all
            if (qty == int.MinValue)
            {
                qty = (aBytes.Length * 8);
            }

            // qty OK?
            if (qty > (aBytes.Length * 8))
            {
                throw new ArgumentException("qty > (aBytes[].Length * 8)", "qty");
            }

            // no data?
            if (qty == 0)
            {
                return "(no data)";
            }

            // output like "0,0,0,1,0,0,0,1, 1,0,1,0,1,0,1,0, ..." with byte[0].bit0 first
            for (int biti = 0; biti < qty; ++biti)
            {
                if (biti > 0)
                {
                    sb.Append(",");

                    if ((biti % 8) == 0)
                    {
                        sb.Append(" ");
                    }
                }

                int bit = (aBytes[biti / 8] >> (biti % 8)) & 1;
                if (bit > 0)
                    sb.Append("1");
                else
                    sb.Append("0");
            }

            return sb.ToString();
        }

        /// <summary>
        /// Parse string into 16-bit values
        /// Accepts signed and unsigned decimal values, hex and binary values
        /// Throws exception on error
        /// </summary>
        private Int16[] ParseValuesStringInto16bitValues(string str, int expectedCount)
        {
            // get data
            string[] aFields = MySplit(str, ',');

            // array of 16-bit values to store binary data
            Int16[] aValues = new short[aFields.Length];

            // get values
            for (int i = 0; i < aFields.Length; ++i)
            {
                try
                {
                    // can throw
                    Int32 res = CValueToString.ValueStringToIntValue(aFields[i].Trim(), 16, 0);
                    aValues[i] = (Int16)res;
                }
                catch (Exception)
                {
                    // error
                    throw new Exception("Invalid value: " + "\"" + aFields[i].Trim() + "\"");
                }
            }

            // match expectedCount?
            if (aFields.Length != expectedCount)
            {
                throw new Exception(String.Format("Expected {0} values, found {1}", expectedCount, aFields.Length));
            }

            return aValues;
        }

        /// <summary>
        /// Parse string into 32-bit values
        /// Accepts signed and unsigned 32-bit values
        /// Accepts decimal, hex and binary values
        /// Throws exception on error
        /// </summary>
        private Int32[] ParseValuesStringInto32bitValues(string str, int expectedCount)
        {
            // get data
            string[] aFields = MySplit(str, ',');
            
            // array of 32-bit values to store binary data
            Int32[] aValues = new Int32[aFields.Length];

            // get values
            for (int i = 0; i < aFields.Length; ++i)
            {
                try
                {
                    Int32 res = CValueToString.ValueStringToIntValue(aFields[i].Trim(), 32, 0);
                    aValues[i] = res;
                }
                catch (Exception)
                {
                    // error
                    throw new Exception("Invalid value \"" + aFields[i].Trim() + "\"");
                }
            }

            // match expectedCount?
            if (aFields.Length != expectedCount)
            {
                throw new Exception(String.Format("Expected {0} values, found {1}", expectedCount, aFields.Length));
            }

            return aValues;
        }

        /// <summary>
        /// Parse string into byte values
        /// Accept values 0-255, 0x00-0xFF
        /// Throws exception on error
        /// </summary>
        private byte[] ParseValuesStringIntoByteValues(string str, int expectedCount = int.MinValue)
        {
            // get data
            string[] aFields = MySplit(str, ',');
            
            // array of byte values to store binary data
            byte[] aValues = new byte[aFields.Length];

            // get values
            for (int i = 0; i < aFields.Length; ++i)
            {
                try
                {
                    // can throw
                    // Int32 StringToIntValue(string str, int Width, int Decimals)
                    Int32 tmpVal = CValueToString.ValueStringToIntValue(aFields[i].Trim(), 8, 0);
                    if (tmpVal >= 0 && tmpVal <= 255)
                    {
                        // okay
                        aValues[i] = (byte)tmpVal;
                    }
                    else
                    {
                        // error
                        throw new Exception("Invalid value \"" + aFields[i].Trim() + "\"");
                    }
                }
                catch (Exception)
                {
                    // error
                    throw new Exception("Invalid value: " + "\"" + aFields[i].Trim() + "\"");
                }
            }

            // match expectedCount?
            if (expectedCount != int.MinValue)
            {
                if (aFields.Length != expectedCount)
                {
                    throw new Exception(String.Format("Expected {0} values, found {1}", expectedCount, aFields.Length));
                }
            }

            return aValues;
        }

        /// <summary>
        /// Parse string into bool values, output 8 packed bits per byte
        /// Accept values 0/1 only
        /// Throws exception on error
        /// </summary>
        private byte[] ParseValuesStringIntoPackedBits(string str, int expectedCount)
        {
            // get data
            string[] aFields = MySplit(str, ',');
            
            // array of byte values to store binary data
            int nrOfBytes = (aFields.Length + 7) / 8;
            byte[] aBytes = new byte[nrOfBytes];

            // get values
            for (int i = 0; i < aFields.Length; ++i)
            {
                try
                {
                    // can throw
                    Int32 tmpVal = CValueToString.ValueStringToIntValue(aFields[i].Trim(), 1, 0);
                    if (tmpVal == 1)
                    {
                        aBytes[i / 8] |= (byte)(1 << (i % 8));
                    }
                    else if (tmpVal == 0)
                    {
                        aBytes[i / 8] &= (byte)~(1 << (i % 8));
                    }
                    else
                    {
                        // error
                        throw new Exception("Invalid value \"" + aFields[i].Trim() + "\"");
                    }
                }
                catch (Exception)
                {
                    // error
                    throw new Exception("Invalid value: " + "\"" + aFields[i].Trim() + "\"");
                }
            }

            // match expectedCount?
            if (aFields.Length != expectedCount)
            {
                throw new Exception(String.Format("Expected {0} values, found {1}", expectedCount, aFields.Length));
            }

            return aBytes;
        }

        private Int16[] ConvertInt32ArrayToInt16Array(Int32[] aInt32)
        {
            Int16[] result = new Int16[aInt32.Length * 2];

            for (int i = 0; i < aInt32.Length; ++i)
            {
                Int16[] tmpArray = Converts.Int32ToInt16Array(aInt32[i]);
                result[i * 2] = tmpArray[0];
                result[i * 2 + 1] = tmpArray[1];
            }

            return result;
        }

        public static Int32[] ConvertInt16ArrayToInt32Array(Int16[] aValues)
        {
            if ((aValues.Length % 2) != 0)
            {
                throw new ArgumentException("Int16[].Length is not multiple of 2", "aValues");
            }

            Int32[] aValues32 = new Int32[aValues.Length / 2];

            for (int i = 0; i < (aValues.Length / 2); ++i)
            {
                aValues32[i] = Converts.ToInt32(aValues[i * 2], aValues[i * 2 + 1]);
            }

            return aValues32;
        }

        public static UInt32[] ConvertInt16ArrayToUInt32Array(Int16[] aValues)
        {
            if ((aValues.Length % 2) != 0)
            {
                throw new ArgumentException("Int16[].Length is not multiple of 2", "aValues");
            }

            UInt32[] aValues32 = new UInt32[aValues.Length / 2];

            for (int i = 0; i < (aValues.Length / 2); ++i)
            {
                aValues32[i] = Converts.ToUInt32(aValues[i * 2], aValues[i * 2 + 1]);
            }

            return aValues32;
        }


        /// <summary>
        /// Command string to eCommand
        /// </summary>
        private ECommand GetCommandEnumFromText(string commandText)
        {
            for (int i = 0; i < CommandColumn.Items.Count; ++i)
            {
                if (commandText == (string)CommandColumn.Items[i])
                {
                    return (ECommand)i;
                }
            }

            throw new ArgumentException("Invalid value", "commandText");
        }

        /// <summary>
        /// eCommand value to command string
        /// </summary>
        private string GetCommandTextFromEnum(ECommand command)
        {
            int index = (int)command;
            if (index >= 0 && (index < CommandColumn.Items.Count))
            {
                return (string)CommandColumn.Items[index];
            }

            throw new ArgumentException("Invalid value", "command");
        }

        /// <summary>
        /// Format string to eFormat
        /// </summary>
        private EFormat GetFormatEnumFromText(string formatText)
        {
            for (int i = 0; i < FormatColumn.Items.Count; ++i)
            {
                if (formatText == (string)FormatColumn.Items[i])
                {
                    return (EFormat)i;
                }
            }

            throw new ArgumentException("Invalid value", "formatText");
        }

        /// <summary>
        /// eFormat value to format string
        /// </summary>
        private string GetFormatTextFromEnum(EFormat format)
        {
            int index = (int)format;
            if (index >= 0 && (index < FormatColumn.Items.Count))
            {
                return (string)FormatColumn.Items[index];
            }

            throw new ArgumentException("Invalid value", "format");
        }

        private string GetErrorTextFromQueryResult(ModbusProtocol.Result result)
        {
            string headstr = "";
            string tmpErrorText = result.ToString();
            if (tmpErrorText.IndexOf("EXCEPTION", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                headstr = "Error response: ";
            }
            else
            {
                headstr = "Error: ";
            }
            return headstr + ModbusProtocol.GetResultText(result);
        }

        private void ChkMultilineRows_CheckedChanged(object sender, EventArgs e)
        {
            //SetAutoSizeRows(chkMultilineRows.Checked);
            SetMultilineRows(chkMultilineRows.Checked);
        }

        private void SetAutoSizeRows(bool value)
        {
            if (value)
            {
                // set autosize
                grid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
            }
            else
            {
                // autosize off
                // change row heights manually
                grid.SuspendLayout();
                // set autosize
                for (int gri = 0; gri < grid.Rows.Count; ++gri)
                    grid.Rows[gri].Height = grid.RowTemplate.Height;
                grid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
                grid.ResumeLayout(true);
            }
        }

        private void SetMultilineRows(bool value)
        {
            if (value)
            {
                grid.SuspendLayout();

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

                // set wrap
                grid.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
                DataColumn.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
                ResultColumn.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
                CommentColumn.DefaultCellStyle.WrapMode = DataGridViewTriState.False;

                grid.ResumeLayout(true);
            }
        }

        /// <summary>
        /// Move all selected rows 1 row up
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButMoveUp_Click(object sender, EventArgs e)
        {
            if (grid.SelectedCells.Count > 0)
            {
                // get selected rows, sorted
                List<int> selectedRows = GetGridSelectedRowsOrderedList(grid.SelectedCells);

                try
                {
                    grid.SuspendLayout();

                    // move each line
                    for (int selIndex = 0; selIndex < selectedRows.Count; ++selIndex)
                    {
                        int rowIndex = selectedRows[selIndex];
                        // if there is room to move it up
                        if (rowIndex > selIndex)
                        {
                            DataGridViewRow row = grid.Rows[rowIndex];
                            grid.Rows.RemoveAt(rowIndex);
                            grid.Rows.Insert(rowIndex - 1, row);
                            selectedRows[selIndex] -= 1;
                        }
                    }

                    // restore selection
                    grid.ClearSelection();
                    foreach (int rowIndex in selectedRows)
                    {
                        grid.Rows[rowIndex].Selected = true;
                    }
                }
                finally
                {
                    grid.ResumeLayout();
                }
            }
        }

        /// <summary>
        /// Move all selected rows 1 row down
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButMoveDown_Click(object sender, EventArgs e)
        {
            if (grid.SelectedCells.Count > 0)
            {
                // get selected rows, sorted descending
                List<int> selectedRows = GetGridSelectedRowsOrderedList(grid.SelectedCells);
                selectedRows.Reverse();

                try
                {
                    grid.SuspendLayout();

                    // move each line
                    for (int selIndex = 0; selIndex < selectedRows.Count; ++selIndex)
                    {
                        int rowIndex = selectedRows[selIndex];
                        // if there is room to move it down
                        if (rowIndex < (grid.Rows.Count - selIndex - 1))
                        {
                            DataGridViewRow row = grid.Rows[rowIndex];
                            grid.Rows.RemoveAt(rowIndex);
                            grid.Rows.Insert(rowIndex + 1, row);
                            selectedRows[selIndex] += 1;
                        }
                    }

                    // restore selection
                    grid.ClearSelection();
                    foreach (int rowIndex in selectedRows)
                    {
                        grid.Rows[rowIndex].Selected = true;
                    }
                }
                finally
                {
                    grid.ResumeLayout();
                }
            }
        }


        /// <summary>
        /// Add a row after the last selected row
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButAddRow_Click(object sender, EventArgs e)
        {
            int insertRowIndex = 0;

            // if we have selected rows: add after last selected row
            if (grid.SelectedCells.Count > 0)
            {
                // get selected rows, sorted
                List<int> selectedRows = GetGridSelectedRowsOrderedList(grid.SelectedCells);
                int selectRowIndex = selectedRows[selectedRows.Count - 1];
                insertRowIndex = selectRowIndex + 1;
            }
            else
            {
                // add at the end
                insertRowIndex = grid.Rows.Count;
            }

            // Add row at position
            AddQueryRow("RIR", "1", "", "", "INT16", "", "", insertRowIndex);

            // make it selected
            grid.ClearSelection();
            grid.Rows[insertRowIndex].Selected = true;
        }


        /// <summary>
        /// Delete selected rows
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButDeleteRows_Click(object sender, EventArgs e)
        {
            if (grid.SelectedCells.Count > 0)
            {
                // mark the rows that are about to be deleted
                // get selected rows, sorted
                List<int> selectedRows = GetGridSelectedRowsOrderedList(grid.SelectedCells);
                grid.ClearSelection();
                foreach (int rowIndex in selectedRows)
                {
                    grid.Rows[rowIndex].Selected = true;
                }

                DialogResult result = MessageBox.Show($"Delete {selectedRows.Count} rows. Are you sure?",
                                                      "Confirmation",
                                                      MessageBoxButtons.YesNo,
                                                      MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    // sort last tofirst
                    selectedRows.Reverse();

                    // Proceed with deletion
                    grid.SuspendLayout();

                    foreach (int rowIndex in selectedRows)
                    {
                        grid.Rows.RemoveAt(rowIndex);
                    }

                    grid.ResumeLayout();
                }
            }
        }

        /// <summary>
        /// Return list with row indexes, sorted
        /// </summary>
        /// <returns></returns>
        List<int> GetGridSelectedRowsOrderedList(DataGridViewSelectedCellCollection selectedCells)
        {
            List<int> rowNums = new List<int>();

            foreach (DataGridViewCell cell in selectedCells)
            {
                if (rowNums.IndexOf(cell.RowIndex) < 0)
                {
                    rowNums.Add(cell.RowIndex);
                }
            }

            rowNums.Sort();

            return rowNums;
        }

        /// <summary>
        /// Improved split
        /// We allow a comma at the end, in standard split that results in an extra empty data field, we allow that and remote it
        /// </summary>
        private string[] MySplit(string str, char sep)
        {
            // get data
            string[] aValues = str.Split(sep);
            // we allow a comma at the end, that results in an empty data field, we allow and ignore that
            if ((aValues.Length >= 1) && (aValues[aValues.Length - 1].Trim() == ""))
            {
                Array.Resize(ref aValues, aValues.Length - 1);
            }

            return aValues;
        }


        private string ConvertRegistersToResultString(Int16[] aRegisters, EFormat format)
        {
            StringBuilder result = new StringBuilder(1500);     // 126 * "-32000, "
            string separatorStr = ", ";

            // no data?
            if (aRegisters.Length == 0)
            {
                return "(no data)";
            }

            // convert all registers to string, different handling for different formats
            switch (format)
            {
                case EFormat.INT16:
                    for (int i = 0; i < aRegisters.Length; i++)
                    {
                        // insert separator
                        if (i > 0)
                        {
                            result.Append(separatorStr);
                        }

                        // conv to string, no decimals, no minlen
                        result.Append(Converts.IntToString(aRegisters[i]));
                    }
                    break;

                case EFormat.UINT16:
                    UInt16[] aUInt16 = Converts.Int16ArrayToUInt16Array(aRegisters);
                    for (int i = 0; i < aRegisters.Length; i++)
                    {
                        // insert separator
                        if (i > 0)
                        {
                            result.Append(separatorStr);
                        }

                        // conv to string, no decimals, no minlen
                        result.Append(Converts.UIntToString(aUInt16[i]));
                    }
                    break;

                case EFormat.HEX16:
                    for (int i = 0; i < aRegisters.Length; i++)
                    {
                        // insert separator
                        if (i > 0)
                        {
                            result.Append(separatorStr);
                        }

                        // conv to string, no decimals, no minlen
                        result.Append(Converts.IntToStringHex((UInt16)aRegisters[i]));
                    }
                    break;

                case EFormat.INT32:
                    // need 2 registers per Int32
                    if ((aRegisters.Length % 2) != 0)
                    {
                        return "Error: Qty must be multiple of 2 for 32-bit values";
                    }
                    Int32[] aInt32 = ConvertInt16ArrayToInt32Array(aRegisters);
                    for (int i = 0; i < aInt32.Length; i++)
                    {
                        // insert separator
                        if (i > 0)
                        {
                            result.Append(separatorStr);
                        }

                        // conv to string, no decimals, no minlen
                        result.Append(Converts.IntToString(aInt32[i]));
                    }
                    break;

                case EFormat.UINT32:
                    // need 2 registers per Int32
                    if ((aRegisters.Length % 2) != 0)
                    {
                        return "Error: Qty must be multiple of 2 for 32-bit values";
                    }
                    UInt32[] aUInt32 = ConvertInt16ArrayToUInt32Array(aRegisters);
                    for (int i = 0; i < aUInt32.Length; i++)
                    {
                        // insert separator
                        if (i > 0)
                        {
                            result.Append(separatorStr);
                        }

                        // conv to string, no decimals, no minlen
                        result.Append(Converts.UIntToString(aUInt32[i]));
                    }
                    break;

                case EFormat.HEX32:
                    // need 2 registers per Int32
                    if ((aRegisters.Length % 2) != 0)
                    {
                        return "Error: Qty must be multiple of 2 for 32-bit values";
                    }
                    Int32[] aInt32hex = ConvertInt16ArrayToInt32Array(aRegisters);
                    for (int i = 0; i < aInt32hex.Length; i++)
                    {
                        // insert separator
                        if (i > 0)
                        {
                            result.Append(separatorStr);
                        }

                        // conv to string, no decimals, no minlen
                        result.Append(Converts.IntToStringHex(aInt32hex[i]));
                    }
                    break;

                case EFormat.HEXBYTES:
                    // show as big endian because that is how the bytes are in the modbus frame
                    byte[] aBytes = Converts.Int16ArrayToByteArray(aRegisters, Global.eEndian.Big);
                    string s = Converts.ByteArrayToStringOfValues(aBytes);
                    result.Append(s);
                    break;

                default:
                    return "Error: Invalid format";
            }

            return result.ToString();
        }

        /// <summary>
        /// Convert holding register number to register offset 0-65536
        /// </summary>
        /// <returns>true if valid, false if not</returns>
        public bool TryHoldingregisterGetOffset40001(UInt32 Regnr, out UInt16 Offset)
        {
            // accepts: 40001-49999 + 400001-465536

            bool ret = false;
            Offset = 0;

            if ((Regnr >= 40001) && (Regnr <= 49999))
            {
                Offset = (UInt16)(Regnr - 40001);
                ret = true;
            }
            else if ((Regnr >= 400001) && (Regnr <= 465536))
            {
                Offset = (UInt16)(Regnr - 400001);
                ret = true;
            }

            return ret;
        }

        /// <summary>
        /// Convert input register number to register offset 0-65536
        /// </summary>
        /// <returns>true if valid, false if not</returns>
        public bool TryInputregisterGetOffset30001(UInt32 Regnr, out UInt16 Offset)
        {
            // accepts: 30001-39999 + 300001-365536

            bool ret = false;
            Offset = 0;

            if ((Regnr >= 30001) && (Regnr <= 39999))
            {
                Offset = (UInt16)(Regnr - 30001);
                ret = true;
            }
            else if ((Regnr >= 300001) && (Regnr <= 365536))
            {
                Offset = (UInt16)(Regnr - 300001);
                ret = true;
            }

            return ret;
        }

        /// <summary>
        /// Convert discrete input number to offset 0-65536
        /// </summary>
        /// <returns>true if valid, false if not</returns>
        public bool TryDiscreteInputGetOffset10001(UInt32 Regnr, out UInt16 Offset)
        {
            // accepts: 10001–19999 + 100001–165536

            bool ret = false;
            Offset = 0;

            if ((Regnr >= 10001) && (Regnr <= 19999))
            {
                Offset = (UInt16)(Regnr - 10001);
                ret = true;
            }
            else if ((Regnr >= 100001) && (Regnr <= 165536))
            {
                Offset = (UInt16)(Regnr - 100001);
                ret = true;
            }

            return ret;
        }

        /// <summary>
        /// Convert coil number to offset 0-65536
        /// </summary>
        /// <returns>true if valid, false if not</returns>
        public bool TryCoilGetOffset1(UInt32 Regnr, out UInt16 Offset)
        {
            // accepts: 1-9999, not 10000... to void conflict with discrete inputs

            bool ret = false;
            Offset = 0;

            if ((Regnr >= 1) && (Regnr <= 9999))
            {
                Offset = (UInt16)(Regnr - 1);
                ret = true;
            }

            return ret;
        }

        private void ButSendAll_Click(object sender, EventArgs e)
        {
            int lineCount = grid.RowCount;

            Cursor.Current = Cursors.WaitCursor;

            try
            {
                for (int i = 0; i < lineCount; ++i)
                {
                    SendRow(i);
                    grid.Update();
                }
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void ButClearResults_Click(object sender, EventArgs e)
        {
            int lineCount = grid.RowCount;

            grid.SuspendLayout();
            for (int i = 0; i < lineCount; ++i)
            {
                grid.Rows[i].Cells[ResultColumn.Index].Value = string.Empty;
            }
            grid.ResumeLayout(true);

        }

        #region Save and load interface

        /// <summary>
        /// Get relative column widths
        /// </summary>
        /// <returns></returns>
        private ColumnWidthsType GetColumnWidthsInWeights()
        {
            ColumnWidthsType cw = new ColumnWidthsType();

            cw.sendWidth = grid.Columns["SendColumn"].FillWeight;
            cw.slaveIDWidth = grid.Columns["SlaveidColumn"].FillWeight;
            cw.commandWidth = grid.Columns["CommandColumn"].FillWeight;
            cw.registerWidth = grid.Columns["RegisterColumn"].FillWeight;
            cw.qtyWidth = grid.Columns["QtyColumn"].FillWeight;
            cw.formatWidth = grid.Columns["FormatColumn"].FillWeight;
            cw.dataWidth = grid.Columns["DataColumn"].FillWeight;
            cw.resultWidth = grid.Columns["ResultColumn"].FillWeight;
            cw.commentWidth = grid.Columns["CommentColumn"].FillWeight;

            return cw;
        }

        private void SetColumnWidthsInWeights(ColumnWidthsType cw)
        {
            grid.Columns["SendColumn"].FillWeight = cw.sendWidth;
            grid.Columns["SlaveidColumn"].FillWeight = cw.slaveIDWidth;
            grid.Columns["CommandColumn"].FillWeight = cw.commandWidth;
            grid.Columns["RegisterColumn"].FillWeight = cw.registerWidth;
            grid.Columns["QtyColumn"].FillWeight = cw.qtyWidth;
            grid.Columns["FormatColumn"].FillWeight = cw.formatWidth;
            grid.Columns["DataColumn"].FillWeight = cw.dataWidth;
            grid.Columns["ResultColumn"].FillWeight = cw.resultWidth;
            grid.Columns["CommentColumn"].FillWeight = cw.commentWidth;
        }

        /// <summary>
        /// Get list of custom queries, to save them
        /// Can return empty list
        /// Returns null if list must not be saved
        /// </summary>
        /// <returns></returns>
        private List<CustomQueryItem> GetQueriesList()
        {
#if TESTBUILD
            // do not save the list
            return null;
#else
            List<CustomQueryItem> clist = new List<CustomQueryItem>();

            foreach (DataGridViewRow r in grid.Rows)
            {
                CustomQueryItem c = new CustomQueryItem();
                c.slaveID = IsNull(r.Cells[SlaveidColumn.Index].Value, "");
                string commandStr = IsNull(r.Cells[CommandColumn.Index].Value, "");
                ECommand cmd = GetCommandEnumFromText(commandStr);
                c.command = cmd.ToString();
                c.register = IsNull(r.Cells[RegisterColumn.Index].Value, "");
                c.qty = IsNull(r.Cells[QtyColumn.Index].Value, "");
                string formatStr = IsNull(r.Cells[FormatColumn.Index].Value, "");
                EFormat fmt = GetFormatEnumFromText(formatStr);
                c.format = fmt.ToString();
                c.data = IsNull(r.Cells[DataColumn.Index].Value, "");
                c.comment = IsNull(r.Cells[CommentColumn.Index].Value, "");
                clist.Add(c);
            }

            return clist;
#endif
        }

        private void SetQueriesList(List<CustomQueryItem> qlist)
        {
#if !TESTBUILD
            grid.Rows.Clear();

            try
            {
                grid.SuspendLayout();

                foreach (CustomQueryItem q in qlist)
                {
                    try
                    {
                        AddQueryRow(q.command, q.slaveID, q.register, q.qty, q.format, q.data, q.comment);
                    }
                    catch
                    {
                        // show error
                        MessageBox.Show("Error: Invalid entry in Settings: " + q.command + ", " + q.slaveID + ", " + q.register + ", " + q.qty + ", " + q.format + Environment.NewLine, this.Text,
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            finally
            {
                grid.ResumeLayout();
            }
#endif
        }

#endregion

#region Functional Testing

        private void ButRunTests_Click(object sender, EventArgs e)
        {
            /* test all, for dev test
             * 
             * Use test server: "Modbusserver" by Lucst
             *                  Wraparound: on
             *                  Animation: None
             *                  InputRegisters, DiscreteInputs and Coils overlap with HoldingRegisters!
             * Each test writes and readsback data.
             */

#if TESTBUILD
            Cursor.Current = Cursors.WaitCursor;

            try
            {
                Test_ReadWriteHoldingRegisters();
                Test_ReadWriteCoils();

                Test_PrepareTestData();

                Test_SendReadInputreg();
                Test_SendReadHoldingreg();
                Test_SendWriteSingleHoldingreg();
                Test_SendWriteMultipleHoldingreg();
                Test_SendReadDiscreteinputs();
                Test_SendReadCoils();
                Test_SendWriteSingleCoil();
                Test_SendWriteMultipleCoils();
                Test_SendCustomQuery();
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
#endif
            }

#if TESTBUILD
        private void Test_PrepareTestData()
        {
            // prepare data in modbusserver

            Int16[] aValues16W = new Int16[123];
            for (int i = 0; i < aValues16W.Length; ++i)
            {
                aValues16W[i] = (Int16)(10000 + (i * 5751));
            }

            ModbusProtocol.Result result = mModbus.MifWriteMultipleHoldingRegisters(1, 0, 123, aValues16W);
            if (result != ModbusProtocol.Result.SUCCESS)
            {
                throw new Exception("TEST FAILED");
            }

            result = mModbus.MifWriteMultipleHoldingRegisters(1, 1877, 123, aValues16W);
            if (result != ModbusProtocol.Result.SUCCESS)
            {
                throw new Exception("TEST FAILED");
            }
        }

        private void Test_ReadWriteHoldingRegisters()
        {
            // Test ReadHoldingreg, WriteSingleHoldingreg, WriteMultipleHoldingreg

            byte slaveID = 1;
            UInt16 regOffset = 500;
            UInt16 regQuantity = 123;
            Int16[] aValues16W = new Int16[123];
            Int16[] aValues16R;
            Int16[] aValues16R2;

            for (int i = 0; i < aValues16W.Length; ++i)
            {
                aValues16W[i] = (Int16)(10000 + (i * 235));
            }

            ModbusProtocol.Result result = mModbus.MifWriteMultipleHoldingRegisters(slaveID, regOffset, regQuantity, aValues16W);
            if (result != ModbusProtocol.Result.SUCCESS)
            {
                throw new Exception("TEST FAILED");
            }

            result = mModbus.MifReadHoldingRegisters(slaveID, regOffset, regQuantity, out aValues16R, true);
            if (result == ModbusProtocol.Result.SUCCESS)
            {
                for (int i = 0; i < aValues16W.Length; ++i)
                {
                    if (aValues16R[i] != aValues16W[i])
                    {
                        throw new Exception("TEST FAILED");
                    }
                }
            }
            else
            {
                throw new Exception("TEST FAILED");
            }

            aValues16W[10] = 1234;
            result = mModbus.MifWriteSingleHoldingRegister(slaveID, (UInt16)(regOffset + 10), aValues16W[10]);
            if (result != ModbusProtocol.Result.SUCCESS)
            {
                throw new Exception("TEST FAILED");
            }

            aValues16W[101] = -5159;
            result = mModbus.MifWriteSingleHoldingRegister(slaveID, (UInt16)(regOffset + 101), aValues16W[101]);
            if (result != ModbusProtocol.Result.SUCCESS)
            {
                throw new Exception("TEST FAILED");
            }

            result = mModbus.MifReadHoldingRegisters(slaveID, regOffset, regQuantity, out aValues16R2, true);
            if (result == ModbusProtocol.Result.SUCCESS)
            {
                for (int i = 0; i < aValues16R2.Length; ++i)
                {
                    if (aValues16R2[i] != aValues16W[i])
                    {
                        throw new Exception("TEST FAILED");
                    }
                }
            }
            else
            {
                throw new Exception("TEST FAILED");
            }
        }

        private void Test_ReadWriteCoils()
        {
            // Test ReadCoils, WriteSingleCoil, WriteMultipleCoils

            byte slaveID = 1;
            UInt16 regOffset = 0;
            UInt16 regQuantity = 1976;
            byte[] aBytesW = new byte[1976 / 8];
            byte[] aBytesR1;
            byte[] aBytesR2;

            for (int i = 0; i < aBytesW.Length; ++i)
            {
                aBytesW[i] = (byte)(123 + (i * 3));
            }

            regOffset = 2016;    // 2016 is voorbij 125registers*16bit, en is veelvoud van 8
            ModbusProtocol.Result result = mModbus.MifWriteMultipleCoils(slaveID, regOffset, regQuantity, aBytesW);
            if (result != ModbusProtocol.Result.SUCCESS)
            {
                throw new Exception("TEST FAILED");
            }

            result = mModbus.MifReadCoils(slaveID, regOffset, regQuantity, out aBytesR1, true);
            if (result == ModbusProtocol.Result.SUCCESS)
            {
                for (int i = 0; i < aBytesR1.Length; ++i)
                {
                    if (aBytesR1[i] != aBytesW[i])
                    {
                        throw new Exception("TEST FAILED");
                    }
                }
            }
            else
            {
                throw new Exception("TEST FAILED");
            }

            regOffset = 2017;
            result = mModbus.MifWriteMultipleCoils(slaveID, regOffset, regQuantity, aBytesW);
            if (result != ModbusProtocol.Result.SUCCESS)
            {
                throw new Exception("TEST FAILED");
            }

            result = mModbus.MifReadCoils(slaveID, regOffset, regQuantity, out aBytesR1, true);
            if (result == ModbusProtocol.Result.SUCCESS)
            {
                for (int i = 0; i < aBytesR1.Length; ++i)
                {
                    if (aBytesR1[i] != aBytesW[i])
                    {
                        throw new Exception("TEST FAILED");
                    }
                }
            }
            else
            {
                throw new Exception("TEST FAILED");
            }

            regOffset = 2018;
            result = mModbus.MifWriteMultipleCoils(slaveID, regOffset, regQuantity, aBytesW);
            if (result != ModbusProtocol.Result.SUCCESS)
            {
                throw new Exception("TEST FAILED");
            }

            result = mModbus.MifReadCoils(slaveID, regOffset, regQuantity, out aBytesR1, true);
            if (result == ModbusProtocol.Result.SUCCESS)
            {
                for (int i = 0; i < aBytesR1.Length; ++i)
                {
                    if (aBytesR1[i] != aBytesW[i])
                    {
                        throw new Exception("TEST FAILED");
                    }
                }
            }
            else
            {
                throw new Exception("TEST FAILED");
            }

            regOffset = 2021;
            result = mModbus.MifWriteMultipleCoils(slaveID, regOffset, regQuantity, aBytesW);
            if (result != ModbusProtocol.Result.SUCCESS)
            {
                throw new Exception("TEST FAILED");
            }

            result = mModbus.MifReadCoils(slaveID, regOffset, regQuantity, out aBytesR1, true);
            if (result == ModbusProtocol.Result.SUCCESS)
            {
                for (int i = 0; i < aBytesR1.Length; ++i)
                {
                    if (aBytesR1[i] != aBytesW[i])
                    {
                        throw new Exception("TEST FAILED");
                    }
                }
            }
            else
            {
                throw new Exception("TEST FAILED");
            }

            aBytesW[0] = 0xA3;
            aBytesW[1] = 0x4F;
            aBytesW[2] = 0x1C;
            aBytesW[3] = 0x7D;
            result = mModbus.MifWriteMultipleCoils(slaveID, regOffset, 32, aBytesW);
            if (result != ModbusProtocol.Result.SUCCESS)
            {
                throw new Exception("TEST FAILED");
            }

            result = mModbus.MifWriteSingleCoil(slaveID, (UInt16)(regOffset + 3), 1);
            if (result != ModbusProtocol.Result.SUCCESS)
            {
                throw new Exception("TEST FAILED");
            }

            result = mModbus.MifWriteSingleCoil(slaveID, (UInt16)(regOffset + 7), 0);
            if (result != ModbusProtocol.Result.SUCCESS)
            {
                throw new Exception("TEST FAILED");
            }

            result = mModbus.MifWriteSingleCoil(slaveID, (UInt16)(regOffset + 19), 1);
            if (result != ModbusProtocol.Result.SUCCESS)
            {
                throw new Exception("TEST FAILED");
            }

            result = mModbus.MifWriteSingleCoil(slaveID, (UInt16)(regOffset + 28), 0);
            if (result != ModbusProtocol.Result.SUCCESS)
            {
                throw new Exception("TEST FAILED");
            }

            // read the coils
            // check if the MifWriteSingleCoil() worked
            result = mModbus.MifReadCoils(slaveID, regOffset, 32, out aBytesR2, true);
            if (result == ModbusProtocol.Result.SUCCESS)
            {
                if ((aBytesR2[0] == 0x2B)
                    && (aBytesR2[1] == 0x4F)
                    && (aBytesR2[2] == 0x1C)
                    && (aBytesR2[3] == 0x6D))
                {
                    // OK 
                }
                else
                {
                    throw new Exception("TEST FAILED");
                }
            }
            else
            {
                throw new Exception("TEST FAILED");
            }

            result = mModbus.MifReadCoils(slaveID, (UInt16)(regOffset + 1), 30, out aBytesR1, true);
            if (result != ModbusProtocol.Result.SUCCESS)
            {
                throw new Exception("TEST FAILED");
            }

            result = mModbus.MifReadCoils(slaveID, (UInt16)(regOffset + 2), 30, out aBytesR1, true);
            if (result != ModbusProtocol.Result.SUCCESS)
            {
                throw new Exception("TEST FAILED");
            }

            result = mModbus.MifReadCoils(slaveID, (UInt16)(regOffset + 8), 30, out aBytesR1, true);
            if (result != ModbusProtocol.Result.SUCCESS)
            {
                throw new Exception("TEST FAILED");
            }

            result = mModbus.MifWriteSingleCoil(slaveID, (UInt16)(regOffset), 0, true);
            if (result != ModbusProtocol.Result.SUCCESS)
            {
                throw new Exception("TEST FAILED");
            }

            result = mModbus.MifWriteSingleCoil(slaveID, (UInt16)(regOffset + 1), 1, true);
            if (result != ModbusProtocol.Result.SUCCESS)
            {
                throw new Exception("TEST FAILED");
            }

            result = mModbus.MifWriteSingleCoil(slaveID, (UInt16)(regOffset), 2, true);
            if (result != ModbusProtocol.Result.INVALIDARGUMENT)
            {
                throw new Exception("TEST FAILED");
            }
        }

        private void Test_SendReadInputreg()
        {
            string rs;

            rs = SendReadInputreg("", "30001", "10", EFormat.INT16, "");
            Test_TestResultStrContains("invalid slaveid", rs);

            rs = SendReadInputreg("1", "", "1", EFormat.INT16, "");
            Test_TestResultStrContains("invalid reg", rs);

            rs = SendReadInputreg("1", "a", "1", EFormat.INT16, "");
            Test_TestResultStrContains("invalid reg", rs);

            rs = SendReadInputreg("1", "30000", "10", EFormat.INT16, "");
            Test_TestResultStrContains("invalid reg", rs);

            rs = SendReadInputreg("1", "30001", "10", EFormat.INT16, "");
            Test_TestResultStrEquals("10000, 15751, 21502, 27253, -32532, -26781, -21030, -15279, -9528, -3777", rs);

            rs = SendReadInputreg("1", "39999", "10", EFormat.INT16, "");
            Test_TestResultStrEquals("-15025, -9274, 10000, 15751, 21502, 27253, -32532, -26781, -21030, -15279", rs);

            rs = SendReadInputreg("1", "40000", "10", EFormat.INT16, "");
            Test_TestResultStrContains("invalid reg", rs);

            rs = SendReadInputreg("1", "40001", "10", EFormat.INT16, "");
            Test_TestResultStrContains("invalid reg", rs);

            rs = SendReadInputreg("1", "300000", "10", EFormat.INT16, "");
            Test_TestResultStrContains("invalid reg", rs);

            rs = SendReadInputreg("1", "300001", "10", EFormat.INT16, "");
            Test_TestResultStrEquals("10000, 15751, 21502, 27253, -32532, -26781, -21030, -15279, -9528, -3777", rs);

            rs = SendReadInputreg("1", "365536", "1", EFormat.INT16, "");
            Test_TestResultStrEquals("0", rs);

            rs = SendReadInputreg("1", "365537", "1", EFormat.INT16, "");
            Test_TestResultStrContains("invalid reg", rs);

            rs = SendReadInputreg("1", "30001", "", EFormat.INT16, "");
            Test_TestResultStrContains("invalid qty", rs);

            rs = SendReadInputreg("1", "30001", "0", EFormat.INT16, "");
            Test_TestResultStrContains("no data", rs);

            rs = SendReadInputreg("1", "30001", "1", EFormat.INT16, "");
            Test_TestResultStrEquals("10000", rs);

            rs = SendReadInputreg("1", "30001", "10", EFormat.INT16, "");
            Test_TestResultStrEquals("10000, 15751, 21502, 27253, -32532, -26781, -21030, -15279, -9528, -3777", rs);

            rs = SendReadInputreg("1", "30001", "125", EFormat.INT16, "");
            Test_TestResultStrEquals("10000, 15751, 21502, 27253, -32532, -26781, -21030, -15279, -9528, -3777, 1974, 7725, 13476, 19227, 24978, 30729, -29056, -23305, -17554, -11803, -6052, -301, 5450, 11201, 16952, 22703, 28454, -31331, -25580, -19829, -14078, -8327, -2576, 3175, 8926, 14677, 20428, 26179, 31930, -27855, -22104, -16353, -10602, -4851, 900, 6651, 12402, 18153, 23904, 29655, -30130, -24379, -18628, -12877, -7126, -1375, 4376, 10127, 15878, 21629, 27380, -32405, -26654, -20903, -15152, -9401, -3650, 2101, 7852, 13603, 19354, 25105, 30856, -28929, -23178, -17427, -11676, -5925, -174, 5577, 11328, 17079, 22830, 28581, -31204, -25453, -19702, -13951, -8200, -2449, 3302, 9053, 14804, 20555, 26306, 32057, -27728, -21977, -16226, -10475, -4724, 1027, 6778, 12529, 18280, 24031, 29782, -30003, -24252, -18501, -12750, -6999, -1248, 4503, 10254, 16005, 21756, 27507, -32278, -26527, -20776, -15025, -9274, 0, 0", rs);

            rs = SendReadInputreg("1", "30001", "200", EFormat.INT16, "");
            Test_TestResultStrContains("RESPONSE DATA ERROR", rs);

            rs = SendReadInputreg("1", "30001", "10", EFormat.UINT16, "");
            Test_TestResultStrEquals("10000, 15751, 21502, 27253, 33004, 38755, 44506, 50257, 56008, 61759", rs);

            rs = SendReadInputreg("1", "30001", "10", EFormat.HEX16, "");
            Test_TestResultStrEquals("0x2710, 0x3D87, 0x53FE, 0x6A75, 0x80EC, 0x9763, 0xADDA, 0xC451, 0xDAC8, 0xF13F", rs);

            rs = SendReadInputreg("1", "30001", "10", EFormat.INT32, "");
            Test_TestResultStrEquals("1032267536, 1786074110, -1755086612, -1001280038, -247473464", rs);

            rs = SendReadInputreg("1", "30001", "10", EFormat.UINT32, "");
            Test_TestResultStrEquals("1032267536, 1786074110, 2539880684, 3293687258, 4047493832", rs);

            rs = SendReadInputreg("1", "30001", "10", EFormat.HEX32, "");
            Test_TestResultStrEquals("0x3D872710, 0x6A7553FE, 0x976380EC, 0xC451ADDA, 0xF13FDAC8", rs);

            rs = SendReadInputreg("1", "30001", "9", EFormat.HEX32, "");
            Test_TestResultStrContains("Qty must be multiple of 2", rs);

            rs = SendReadInputreg("1", "30001", "1", EFormat.HEXBYTES, "");
            Test_TestResultStrContains("0x27, 0x10", rs);  

            rs = SendReadInputreg("1", "30001", "6", EFormat.HEXBYTES, "");
            Test_TestResultStrContains("0x27, 0x10, 0x3D, 0x87, 0x53, 0xFE, 0x6A, 0x75, 0x80, 0xEC, 0x97, 0x63", rs); 

            rs = SendReadInputreg("1", "30001", "1", EFormat.BITS, "");
            Test_TestResultStrContains("invalid format", rs); 

        }

        private void Test_SendReadHoldingreg()
        {
            string rs;

            rs = SendReadHoldingreg("", "40001", "1", EFormat.INT16, "");
            Test_TestResultStrContains("invalid slaveid", rs);

            rs = SendReadHoldingreg("1", "", "1", EFormat.INT16, "");
            Test_TestResultStrContains("invalid reg", rs);

            rs = SendReadHoldingreg("1", "a", "1", EFormat.INT16, "");
            Test_TestResultStrContains("invalid reg", rs);

            rs = SendReadHoldingreg("1", "40000", "10", EFormat.INT16, "");
            Test_TestResultStrContains("invalid reg", rs);

            rs = SendReadHoldingreg("1", "40001", "10", EFormat.INT16, "");
            Test_TestResultStrEquals("10000, 15751, 21502, 27253, -32532, -26781, -21030, -15279, -9528, -3777", rs);

            rs = SendReadHoldingreg("1", "49999", "10", EFormat.INT16, "");
            Test_TestResultStrEquals("-15025, -9274, 10000, 15751, 21502, 27253, -32532, -26781, -21030, -15279", rs);

            rs = SendReadHoldingreg("1", "50000", "10", EFormat.INT16, "");
            Test_TestResultStrContains("invalid reg", rs);

            rs = SendReadHoldingreg("1", "30001", "10", EFormat.INT16, "");
            Test_TestResultStrContains("invalid reg", rs);

            rs = SendReadHoldingreg("1", "400000", "10", EFormat.INT16, "");
            Test_TestResultStrContains("invalid reg", rs);

            rs = SendReadHoldingreg("1", "400001", "10", EFormat.INT16, "");
            Test_TestResultStrEquals("10000, 15751, 21502, 27253, -32532, -26781, -21030, -15279, -9528, -3777", rs);

            rs = SendReadHoldingreg("1", "465536", "10", EFormat.INT16, "");
            Test_TestResultStrContains("ILLEGAL ADDRESS EXCEPTION", rs);

            rs = SendReadHoldingreg("1", "465537", "10", EFormat.INT16, "");
            Test_TestResultStrContains("invalid reg", rs);

            rs = SendReadHoldingreg("1", "40001", "", EFormat.INT16, "");
            Test_TestResultStrContains("invalid qty", rs);

            rs = SendReadHoldingreg("1", "40001", "0", EFormat.INT16, "");
            Test_TestResultStrContains("no data", rs);

            rs = SendReadHoldingreg("1", "40001", "1", EFormat.INT16, "");
            Test_TestResultStrEquals("10000", rs);

            rs = SendReadHoldingreg("1", "40001", "10", EFormat.INT16, "");
            Test_TestResultStrEquals("10000, 15751, 21502, 27253, -32532, -26781, -21030, -15279, -9528, -3777", rs);

            rs = SendReadHoldingreg("1", "40001", "125", EFormat.INT16, "");
            Test_TestResultStrEquals("10000, 15751, 21502, 27253, -32532, -26781, -21030, -15279, -9528, -3777, 1974, 7725, 13476, 19227, 24978, 30729, -29056, -23305, -17554, -11803, -6052, -301, 5450, 11201, 16952, 22703, 28454, -31331, -25580, -19829, -14078, -8327, -2576, 3175, 8926, 14677, 20428, 26179, 31930, -27855, -22104, -16353, -10602, -4851, 900, 6651, 12402, 18153, 23904, 29655, -30130, -24379, -18628, -12877, -7126, -1375, 4376, 10127, 15878, 21629, 27380, -32405, -26654, -20903, -15152, -9401, -3650, 2101, 7852, 13603, 19354, 25105, 30856, -28929, -23178, -17427, -11676, -5925, -174, 5577, 11328, 17079, 22830, 28581, -31204, -25453, -19702, -13951, -8200, -2449, 3302, 9053, 14804, 20555, 26306, 32057, -27728, -21977, -16226, -10475, -4724, 1027, 6778, 12529, 18280, 24031, 29782, -30003, -24252, -18501, -12750, -6999, -1248, 4503, 10254, 16005, 21756, 27507, -32278, -26527, -20776, -15025, -9274, 0, 0", rs);

            rs = SendReadHoldingreg("1", "40001", "200", EFormat.INT16, "");
            Test_TestResultStrContains("RESPONSE DATA ERROR", rs);

            rs = SendReadHoldingreg("1", "40001", "10", EFormat.UINT16, "");
            Test_TestResultStrEquals("10000, 15751, 21502, 27253, 33004, 38755, 44506, 50257, 56008, 61759", rs);

            rs = SendReadHoldingreg("1", "40001", "10", EFormat.HEX16, "");
            Test_TestResultStrEquals("0x2710, 0x3D87, 0x53FE, 0x6A75, 0x80EC, 0x9763, 0xADDA, 0xC451, 0xDAC8, 0xF13F", rs);

            rs = SendReadHoldingreg("1", "40001", "10", EFormat.INT32, "");
            Test_TestResultStrEquals("1032267536, 1786074110, -1755086612, -1001280038, -247473464", rs);

            rs = SendReadHoldingreg("1", "40001", "10", EFormat.UINT32, "");
            Test_TestResultStrEquals("1032267536, 1786074110, 2539880684, 3293687258, 4047493832", rs);

            rs = SendReadHoldingreg("1", "40001", "10", EFormat.HEX32, "");
            Test_TestResultStrEquals("0x3D872710, 0x6A7553FE, 0x976380EC, 0xC451ADDA, 0xF13FDAC8", rs);

            rs = SendReadHoldingreg("1", "40001", "9", EFormat.HEX32, "");
            Test_TestResultStrContains("must be multiple of 2", rs);

            rs = SendReadHoldingreg("1", "40001", "1", EFormat.HEXBYTES, "");
            Test_TestResultStrContains("0x27, 0x10", rs);

            rs = SendReadHoldingreg("1", "40001", "6", EFormat.HEXBYTES, "");
            Test_TestResultStrContains("0x27, 0x10, 0x3D, 0x87, 0x53, 0xFE, 0x6A, 0x75, 0x80, 0xEC, 0x97, 0x63", rs);

            rs = SendReadHoldingreg("1", "40001", "1", EFormat.BITS, "");
            Test_TestResultStrContains("invalid format", rs);
        }

        private void Test_SendWriteSingleHoldingreg()
        {
            string rs;

            rs = SendWriteSingleHoldingreg("", "40001", "1", EFormat.INT16, "100");
            Test_TestResultStrContains("invalid slaveid", rs);

            rs = SendWriteSingleHoldingreg("1", "", "1", EFormat.INT16, "100");
            Test_TestResultStrContains("invalid reg", rs);

            rs = SendWriteSingleHoldingreg("1", "a", "1", EFormat.INT16, "100");
            Test_TestResultStrContains("invalid reg", rs);

            rs = SendWriteSingleHoldingreg("1", "40000", "10", EFormat.INT16, "100");
            Test_TestResultStrContains("invalid reg", rs);

            rs = SendWriteSingleHoldingreg("1", "40301", "1", EFormat.INT16, "101");
            Test_TestResultStrContains("success", rs);
            Test_HoldingRegisterValueEqual(40301, 101);

            rs = SendWriteSingleHoldingreg("1", "49899", "1", EFormat.INT16, "102");
            Test_TestResultStrContains("success", rs);
            Test_HoldingRegisterValueEqual(49899, 102);

            rs = SendWriteSingleHoldingreg("1", "50000", "1", EFormat.INT16, "103");
            Test_TestResultStrContains("invalid reg", rs);

            rs = SendWriteSingleHoldingreg("1", "30001", "1", EFormat.INT16, "104");
            Test_TestResultStrContains("invalid reg", rs);

            rs = SendWriteSingleHoldingreg("1", "400000", "1", EFormat.INT16, "105");
            Test_TestResultStrContains("invalid reg", rs);

            rs = SendWriteSingleHoldingreg("1", "400301", "1", EFormat.INT16, "106");
            Test_TestResultStrContains("success", rs);
            Test_HoldingRegisterValueEqual(400301, 106);

            rs = SendWriteSingleHoldingreg("1", "465436", "1", EFormat.INT16, "107");
            Test_TestResultStrContains("success", rs);
            Test_HoldingRegisterValueEqual(465436, 107);

            rs = SendWriteSingleHoldingreg("1", "465537", "1", EFormat.INT16, "108");
            Test_TestResultStrContains("invalid reg", rs);

            rs = SendWriteSingleHoldingreg("1", "40301", "", EFormat.INT16, "109");
            Test_TestResultStrContains("invalid qty", rs);

            rs = SendWriteSingleHoldingreg("1", "40301", "2", EFormat.INT16, "110");
            Test_TestResultStrContains("invalid qty", rs);

            rs = SendWriteSingleHoldingreg("1", "40301", "1", EFormat.INT16, "");
            Test_TestResultStrContains("no data", rs);

            rs = SendWriteSingleHoldingreg("1", "40301", "1", EFormat.INT16, "aa");
            Test_TestResultStrContains("invalid value", rs);

            rs = SendWriteSingleHoldingreg("1", "40301", "1", EFormat.INT16, "");
            Test_TestResultStrContains("no data", rs);

            rs = SendWriteSingleHoldingreg("1", "40301", "1", EFormat.INT16, "1.98");
            Test_TestResultStrContains("invalid value", rs);

            rs = SendWriteSingleHoldingreg("1", "40301", "1", EFormat.INT16, "101, 102");
            Test_TestResultStrContains("Expected 1 values, found 2", rs);

            rs = SendWriteSingleHoldingreg("1", "40301", "1", EFormat.INT16, "0");
            Test_TestResultStrContains("success", rs);

            rs = SendWriteSingleHoldingreg("1", "40301", "1", EFormat.INT16, "1");
            Test_TestResultStrContains("success", rs);

            rs = SendWriteSingleHoldingreg("1", "40301", "1", EFormat.INT16, "201, ");
            Test_TestResultStrContains("success", rs);
            Test_HoldingRegisterValueEqual(40301, 201);

            rs = SendWriteSingleHoldingreg("1", "40301", "1", EFormat.INT16, "202, ,");
            Test_TestResultStrContains("invalid value", rs);

            rs = SendWriteSingleHoldingreg("1", "40301", "1", EFormat.INT16, "32000");
            Test_TestResultStrContains("success", rs);
            Test_HoldingRegisterValueEqual(40301, 32000);

            rs = SendWriteSingleHoldingreg("1", "40301", "1", EFormat.INT16, "-1");
            Test_TestResultStrContains("success", rs);
            Test_HoldingRegisterValueEqual(40301, -1);

            rs = SendWriteSingleHoldingreg("1", "40301", "1", EFormat.INT16, "0x1234");
            Test_TestResultStrContains("success", rs);
            Test_HoldingRegisterValueEqual(40301, 0x1234);

            rs = SendWriteSingleHoldingreg("1", "40301", "1", EFormat.INT16, "0xABCD");
            Test_TestResultStrContains("success", rs);
            Test_HoldingRegisterValueEqualUInt16(40301, 0xABCD);

            rs = SendWriteSingleHoldingreg("1", "40301", "1", EFormat.INT16, "0b0001001000110100");
            Test_TestResultStrContains("success", rs);
            Test_HoldingRegisterValueEqual(40301, 0x1234);

            rs = SendWriteSingleHoldingreg("1", "40301", "1", EFormat.UINT16, "41000");
            Test_TestResultStrContains("success", rs);
            Test_HoldingRegisterValueEqualUInt16(40301, 41000);

            rs = SendWriteSingleHoldingreg("1", "40301", "1", EFormat.UINT16, "0x1648");
            Test_TestResultStrContains("success", rs);
            Test_HoldingRegisterValueEqualUInt16(40301, 0x1648);

            rs = SendWriteSingleHoldingreg("1", "40301", "1", EFormat.HEX16, "0x8726");
            Test_TestResultStrContains("success", rs);
            Test_HoldingRegisterValueEqualUInt16(40301, 0x8726);

            rs = SendWriteSingleHoldingreg("1", "40301", "1", EFormat.HEX16, "17");
            Test_TestResultStrContains("success", rs);
            Test_HoldingRegisterValueEqual(40301, 17);

            rs = SendWriteSingleHoldingreg("1", "40301", "1", EFormat.HEXBYTES, "17");
            Test_TestResultStrContains("invalid format", rs);

            rs = SendWriteSingleHoldingreg("1", "40301", "1", EFormat.BITS, "1");
            Test_TestResultStrContains("invalid format", rs);
        }

        private void Test_SendWriteMultipleHoldingreg()
        {
            string rs;

            rs = SendWriteMultipleHoldingreg("", "40301", "1", EFormat.INT16, "");
            Test_TestResultStrContains("invalid slaveid", rs);

            rs = SendWriteMultipleHoldingreg("1", "", "1", EFormat.INT16, "");
            Test_TestResultStrContains("invalid reg", rs);

            rs = SendWriteMultipleHoldingreg("1", "a", "1", EFormat.INT16, "");
            Test_TestResultStrContains("invalid reg", rs);

            rs = SendWriteMultipleHoldingreg("1", "40000", "1", EFormat.INT16, "123");
            Test_TestResultStrContains("invalid reg", rs);

            rs = SendWriteMultipleHoldingreg("1", "40301", "1", EFormat.INT16, "7378");
            Test_TestResultStrContains("success", rs);
            Test_HoldingRegisterValueEqual(40301, 7378);

            rs = SendWriteMultipleHoldingreg("1", "49899", "1", EFormat.INT16, "123");
            Test_TestResultStrContains("success", rs);
            Test_HoldingRegisterValueEqual(49899, 123);

            rs = SendWriteMultipleHoldingreg("1", "50000", "1", EFormat.INT16, "123");
            Test_TestResultStrContains("invalid reg", rs);

            rs = SendWriteMultipleHoldingreg("1", "30001", "1", EFormat.INT16, "123");
            Test_TestResultStrContains("invalid reg", rs);

            rs = SendWriteMultipleHoldingreg("1", "400000", "1", EFormat.INT16, "1727");
            Test_TestResultStrContains("invalid reg", rs);

            rs = SendWriteMultipleHoldingreg("1", "400301", "1", EFormat.INT16, "727");
            Test_TestResultStrContains("success", rs);
            Test_HoldingRegisterValueEqual(400301, 727);

            rs = SendWriteMultipleHoldingreg("1", "465436", "1", EFormat.INT16, "737");
            Test_TestResultStrContains("success", rs);
            Test_HoldingRegisterValueEqual(465436, 737);

            rs = SendWriteMultipleHoldingreg("1", "465537", "1", EFormat.INT16, "773");
            Test_TestResultStrContains("invalid reg", rs);

            rs = SendWriteMultipleHoldingreg("1", "40301", "", EFormat.INT16, "428");
            Test_TestResultStrContains("invalid qty", rs);

            rs = SendWriteMultipleHoldingreg("1", "40301", "a", EFormat.INT16, "882");
            Test_TestResultStrContains("invalid qty", rs);

            rs = SendWriteMultipleHoldingreg("1", "40301", "1", EFormat.INT16, "");
            Test_TestResultStrContains("no data", rs);

            rs = SendWriteMultipleHoldingreg("1", "40301", "1", EFormat.INT16, "aa");
            Test_TestResultStrContains("Invalid value", rs);

            rs = SendWriteMultipleHoldingreg("1", "40301", "0", EFormat.INT16, "");
            Test_TestResultStrContains("success", rs);

            rs = SendWriteMultipleHoldingreg("1", "40301", "1", EFormat.INT16, "0");
            Test_TestResultStrContains("success", rs);
            Test_HoldingRegisterValueEqual(40301, 0);

            rs = SendWriteMultipleHoldingreg("1", "40301", "1", EFormat.INT16, "32000");
            Test_TestResultStrContains("success", rs);
            Test_HoldingRegisterValueEqual(40301, 32000);

            rs = SendWriteMultipleHoldingreg("1", "40301", "1", EFormat.INT16, "60000");
            Test_TestResultStrContains("success", rs);
            Test_HoldingRegisterValueEqualUInt16(40301, 60000);

            rs = SendWriteMultipleHoldingreg("1", "40301", "1", EFormat.INT16, "-1");
            Test_TestResultStrContains("success", rs);
            Test_HoldingRegisterValueEqual(40301, -1);

            rs = SendWriteMultipleHoldingreg("1", "40301", "1", EFormat.INT16, "-30000");
            Test_TestResultStrContains("success", rs);
            Test_HoldingRegisterValueEqual(40301, -30000);

            rs = SendWriteMultipleHoldingreg("1", "40301", "1", EFormat.INT16, "0x1234");
            Test_TestResultStrContains("success", rs);
            Test_HoldingRegisterValueEqual(40301, 0x1234);

            rs = SendWriteMultipleHoldingreg("1", "40301", "1", EFormat.INT16, "0xABCD");
            Test_TestResultStrContains("success", rs);
            Test_HoldingRegisterValueEqualUInt16(40301, 0xABCD);

            rs = SendWriteMultipleHoldingreg("1", "40301", "1", EFormat.INT16, "0b0001001000110100");
            Test_TestResultStrContains("success", rs);
            Test_HoldingRegisterValueEqualUInt16(40301, 0x1234);

            rs = SendWriteMultipleHoldingreg("1", "40301", "1", EFormat.INT16, "101, 102");
            Test_TestResultStrContains("Expected 1 values, found 2", rs);

            rs = SendWriteMultipleHoldingreg("1", "40301", "1", EFormat.INT16, "-1, 46");
            Test_TestResultStrContains("Expected 1 values, found 2", rs);

            rs = SendWriteMultipleHoldingreg("1", "40301", "4", EFormat.INT16, "1001, 1002, 1003, 1004");
            Test_TestResultStrContains("success", rs);
            Test_HoldingRegisterValueEqualUInt16(40301, 1001);
            Test_HoldingRegisterValueEqualUInt16(40302, 1002);
            Test_HoldingRegisterValueEqualUInt16(40303, 1003);
            Test_HoldingRegisterValueEqualUInt16(40304, 1004);

            rs = SendWriteMultipleHoldingreg("1", "40301", "4", EFormat.INT16, "1001 , 1002, 1003, 1005, ");
            Test_TestResultStrContains("success", rs);
            Test_HoldingRegisterValueEqualUInt16(40301, 1001);
            Test_HoldingRegisterValueEqualUInt16(40302, 1002);
            Test_HoldingRegisterValueEqualUInt16(40303, 1003);
            Test_HoldingRegisterValueEqualUInt16(40304, 1005);

            rs = SendWriteMultipleHoldingreg("1", "40301", "4", EFormat.INT16, "1001, 1002, 1003, 1004,,");
            Test_TestResultStrContains("invalid value", rs);

            rs = SendWriteMultipleHoldingreg("1", "40301", "4", EFormat.INT16, "1001, 1002, 1003");
            Test_TestResultStrContains("Expected 4 values, found 3", rs);

            rs = SendWriteMultipleHoldingreg("1", "40301", "4", EFormat.INT16, "1001, 1002, 1003, 1004,1005");
            Test_TestResultStrContains("Expected 4 values, found 5", rs);

            rs = SendWriteMultipleHoldingreg("1", "40303", "110", EFormat.INT16, "21001, 21002, 21003, 21004, 21005, 21006, 21007, 21008, 21009, 21010, 21011, 21012, 21013, 21014, 21015, 21016, 21017, 21018, 21019, 21020, 21021, 21022, 21023, 21024, 21025, 21026, 21027, 21028, 21029, 21030, 21031, 21032, 21033, 21034, 21035, 21036, 21037, 21038, 21039, 21040, 21041, 21042, 21043, 21044, 21045, 21046, 21047, 21048, 21049, 21050, 21051, 21052, 21053, 21054, 21055, 21056, 21057, 21058, 21059, 21060, 21061, 21062, 21063, 21064, 21065, 21066, 21067, 21068, 21069, 21070, 21071, 21072, 21073, 21074, 21075, 21076, 21077, 21078, 21079, 21080, 21081, 21082, 21083, 21084, 21085, 21086, 21087, 21088, 21089, 21090, 21091, 21092, 21093, 21094, 21095, 21096, 21097, 21098, 21099, 21100, 21101, 21102, 21103, 21104, 21105, 21106, 21107, 21108, 21109, 21110");
            Test_TestResultStrContains("success", rs);

            rs = SendWriteMultipleHoldingreg("1", "40301", "2", EFormat.UINT16, "1101 , 1102");
            Test_TestResultStrContains("success", rs);
            Test_HoldingRegisterValueEqualUInt16(40301, 1101);
            Test_HoldingRegisterValueEqualUInt16(40302, 1102);

            rs = SendWriteMultipleHoldingreg("1", "40301", "2", EFormat.HEX16, "1001 , 0x1579");
            Test_TestResultStrContains("success", rs);
            Test_HoldingRegisterValueEqualUInt16(40301, 1001);
            Test_HoldingRegisterValueEqualUInt16(40302, 0x1579);

            rs = SendWriteMultipleHoldingreg("1", "40301", "1", EFormat.HEXBYTES, "17");
            Test_TestResultStrContains("invalid format", rs);

            rs = SendWriteMultipleHoldingreg("1", "40301", "1", EFormat.BITS, "1");
            Test_TestResultStrContains("invalid format", rs);

            // assumes project setting is Int32 little endian

            rs = SendWriteMultipleHoldingreg("1", "40301", "4", EFormat.INT32, "1000001 , 2000002 ");
            Test_TestResultStrContains("success", rs);
            Test_HoldingRegisterValueEqualUInt16(40301, 0x4241);
            Test_HoldingRegisterValueEqualUInt16(40302, 0x000F);
            Test_HoldingRegisterValueEqualUInt16(40303, 0x8482);
            Test_HoldingRegisterValueEqualUInt16(40304, 0x001E);

            rs = SendWriteMultipleHoldingreg("1", "40301", "4", EFormat.UINT32, "3000003, 4000000000");
            Test_TestResultStrContains("success", rs);
            Test_HoldingRegisterValueEqualUInt16(40301, 0xC6C3);
            Test_HoldingRegisterValueEqualUInt16(40302, 0x002D);
            Test_HoldingRegisterValueEqualUInt16(40303, 0x2800);
            Test_HoldingRegisterValueEqualUInt16(40304, 0xEE6B);

            //also test with Int32 big endian
            Converts.Int32Endian = Global.eEndian.Big;

            rs = SendWriteMultipleHoldingreg("1", "40301", "4", EFormat.UINT32, "3000003, 4000000000");
            Test_TestResultStrContains("success", rs);
            Test_HoldingRegisterValueEqualUInt16(40301, 0x002D);
            Test_HoldingRegisterValueEqualUInt16(40302, 0xC6C3);
            Test_HoldingRegisterValueEqualUInt16(40303, 0xEE6B);
            Test_HoldingRegisterValueEqualUInt16(40304, 0x2800);

            rs = SendWriteMultipleHoldingreg("1", "40301", "4", EFormat.HEX32, "1001 , 0x8423A595");
            Test_TestResultStrContains("success", rs);
            Test_HoldingRegisterValueEqualUInt16(40301, 0);
            Test_HoldingRegisterValueEqualUInt16(40302, 1001);
            Test_HoldingRegisterValueEqualUInt16(40303, 0x8423);
            Test_HoldingRegisterValueEqualUInt16(40304, 0xA595);

            //back to Int32 little endian
            Converts.Int32Endian = Global.eEndian.Little;

            rs = SendWriteMultipleHoldingreg("1", "40301", "1", EFormat.INT32, "1000000");
            Test_TestResultStrContains(" Qty must be multiple of 2", rs);
        }

        private void Test_SendReadDiscreteinputs()
        {
            string rs;

            rs = SendReadDiscreteinputs("", "10001", "1", EFormat.HEXBYTES, "");
            Test_TestResultStrContains("invalid slaveid", rs);

            rs = SendReadDiscreteinputs("1", "", "1", EFormat.HEXBYTES, "");
            Test_TestResultStrContains("invalid address", rs);

            rs = SendReadDiscreteinputs("1", "a", "1", EFormat.HEXBYTES, "");
            Test_TestResultStrContains("invalid address", rs);

            rs = SendReadDiscreteinputs("1", "10000", "10", EFormat.HEXBYTES, "");
            Test_TestResultStrContains("invalid address", rs);

            rs = SendReadDiscreteinputs("1", "10001", "10", EFormat.HEXBYTES, "");
            Test_TestResultStrEquals("0x10, 0x03", rs);

            rs = SendReadDiscreteinputs("1", "19999", "10", EFormat.HEXBYTES, "");
            Test_TestResultStrContains("0x00, 0x00", rs);

            rs = SendReadDiscreteinputs("1", "20000", "10", EFormat.HEXBYTES, "");
            Test_TestResultStrContains("invalid address", rs);

            rs = SendReadDiscreteinputs("1", "40001", "10", EFormat.HEXBYTES, "");
            Test_TestResultStrContains("invalid address", rs);

            rs = SendReadDiscreteinputs("1", "100000", "10", EFormat.HEXBYTES, "");
            Test_TestResultStrContains("invalid address", rs);

            rs = SendReadDiscreteinputs("1", "100001", "10", EFormat.HEXBYTES, "");
            Test_TestResultStrEquals("0x10, 0x03", rs);

            rs = SendReadDiscreteinputs("1", "165536", "10", EFormat.HEXBYTES, "");
            Test_TestResultStrContains("ILLEGAL ADDRESS EXCEPTION", rs);

            rs = SendReadDiscreteinputs("1", "165537", "10", EFormat.HEXBYTES, "");
            Test_TestResultStrContains("invalid address", rs);

            rs = SendReadDiscreteinputs("1", "10001", "", EFormat.HEXBYTES, "");
            Test_TestResultStrContains("invalid qty", rs);

            rs = SendReadDiscreteinputs("1", "10001", "0", EFormat.HEXBYTES, "");
            Test_TestResultStrEquals("", rs);

            rs = SendReadDiscreteinputs("1", "10001", "1", EFormat.HEXBYTES, "");
            Test_TestResultStrEquals("0x00", rs);

            rs = SendReadDiscreteinputs("1", "10001", "10", EFormat.HEXBYTES, "");
            Test_TestResultStrEquals("0x10, 0x03", rs);

            rs = SendReadDiscreteinputs("1", "10001", "100", EFormat.HEXBYTES, "");
            Test_TestResultStrEquals("0x10, 0x27, 0x87, 0x3D, 0xFE, 0x53, 0x75, 0x6A, 0xEC, 0x80, 0x63, 0x97, 0x0A", rs);

            rs = SendReadDiscreteinputs("1", "10001", "2000", EFormat.HEXBYTES, "");
            Test_TestResultStrEquals("0x10, 0x27, 0x87, 0x3D, 0xFE, 0x53, 0x75, 0x6A, 0xEC, 0x80, 0x63, 0x97, 0xDA, 0xAD, 0x51, 0xC4, 0xC8, 0xDA, 0x3F, 0xF1, 0xB6, 0x07, 0x2D, 0x1E, 0xA4, 0x34, 0x1B, 0x4B, 0x92, 0x61, 0x09, 0x78, 0x80, 0x8E, 0xF7, 0xA4, 0x6E, 0xBB, 0xE5, 0xD1, 0x5C, 0xE8, 0xD3, 0xFE, 0x4A, 0x15, 0xC1, 0x2B, 0x38, 0x42, 0xAF, 0x58, 0x26, 0x6F, 0x9D, 0x85, 0x14, 0x9C, 0x8B, 0xB2, 0x02, 0xC9, 0x79, 0xDF, 0xF0, 0xF5, 0x67, 0x0C, 0xDE, 0x22, 0x55, 0x39, 0xCC, 0x4F, 0x43, 0x66, 0xBA, 0x7C, 0x31, 0x93, 0xA8, 0xA9, 0x1F, 0xC0, 0x96, 0xD6, 0x0D, 0xED, 0x84, 0x03, 0xFB, 0x19, 0x72, 0x30, 0xE9, 0x46, 0x60, 0x5D, 0xD7, 0x73, 0x4E, 0x8A, 0xC5, 0xA0, 0x3C, 0xB7, 0xB3, 0xCD, 0x2A, 0xE4, 0xA1, 0xFA, 0x18, 0x11, 0x8F, 0x27, 0x06, 0x3E, 0x7D, 0x54, 0xF4, 0x6A, 0x6B, 0x81, 0xE2, 0x97, 0x59, 0xAE, 0xD0, 0xC4, 0x47, 0xDB, 0xBE, 0xF1, 0x35, 0x08, 0xAC, 0x1E, 0x23, 0x35, 0x9A, 0x4B, 0x11, 0x62, 0x88, 0x78, 0xFF, 0x8E, 0x76, 0xA5, 0xED, 0xBB, 0x64, 0xD2, 0xDB, 0xE8, 0x52, 0xFF, 0xC9, 0x15, 0x40, 0x2C, 0xB7, 0x42, 0x2E, 0x59, 0xA5, 0x6F, 0x1C, 0x86, 0x93, 0x9C, 0x0A, 0xB3, 0x81, 0xC9, 0xF8, 0xDF, 0x6F, 0xF6, 0xE6, 0x0C, 0x5D, 0x23, 0xD4, 0x39, 0x4B, 0x50, 0xC2, 0x66, 0x39, 0x7D, 0xB0, 0x93, 0x27, 0xAA, 0x9E, 0xC0, 0x15, 0xD7, 0x8C, 0xED, 0x03, 0x04, 0x7A, 0x1A, 0xF1, 0x30, 0x68, 0x47, 0xDF, 0x5D, 0x56, 0x74, 0xCD, 0x8A, 0x44, 0xA1, 0xBB, 0xB7, 0x32, 0xCE, 0xA9, 0xE4, 0x20, 0xFB, 0x97, 0x11, 0x0E, 0x28, 0x85, 0x3E, 0xFC, 0x54, 0x73, 0x6B, 0xEA, 0x81, 0x61, 0x98, 0xD8, 0xAE, 0x4F, 0xC5, 0xC6, 0xDB, 0x00, 0x00, 0x00, 0x00", rs);

            rs = SendReadDiscreteinputs("1", "10001", "3000", EFormat.HEXBYTES, "");
            Test_TestResultStrContains("RESPONSE DATA ERROR", rs);

            rs = SendReadDiscreteinputs("1", "10001", "0", EFormat.BITS, "");
            Test_TestResultStrContains("no data", rs);

            rs = SendReadDiscreteinputs("1", "10001", "1", EFormat.BITS, "");
            Test_TestResultStrEquals("0", rs);

            rs = SendReadDiscreteinputs("1", "10001", "7", EFormat.BITS, "");
            Test_TestResultStrEquals("0,0,0,0,1,0,0", rs);

            rs = SendReadDiscreteinputs("1", "10001", "8", EFormat.BITS, "");
            Test_TestResultStrEquals("0,0,0,0,1,0,0,0", rs);

            rs = SendReadDiscreteinputs("1", "10001", "9", EFormat.BITS, "");
            Test_TestResultStrEquals("0,0,0,0,1,0,0,0, 1", rs);

            rs = SendReadDiscreteinputs("1", "10001", "100", EFormat.BITS, "");
            Test_TestResultStrEquals("0,0,0,0,1,0,0,0, 1,1,1,0,0,1,0,0, 1,1,1,0,0,0,0,1, 1,0,1,1,1,1,0,0, 0,1,1,1,1,1,1,1, 1,1,0,0,1,0,1,0, 1,0,1,0,1,1,1,0, 0,1,0,1,0,1,1,0, 0,0,1,1,0,1,1,1, 0,0,0,0,0,0,0,1, 1,1,0,0,0,1,1,0, 1,1,1,0,1,0,0,1, 0,1,0,1", rs);

            rs = SendReadDiscreteinputs("1", "10001", "1000", EFormat.BITS, "");
            Test_TestResultStrEquals("0,0,0,0,1,0,0,0, 1,1,1,0,0,1,0,0, 1,1,1,0,0,0,0,1, 1,0,1,1,1,1,0,0, 0,1,1,1,1,1,1,1, 1,1,0,0,1,0,1,0, 1,0,1,0,1,1,1,0, 0,1,0,1,0,1,1,0, 0,0,1,1,0,1,1,1, 0,0,0,0,0,0,0,1, 1,1,0,0,0,1,1,0, 1,1,1,0,1,0,0,1, 0,1,0,1,1,0,1,1, 1,0,1,1,0,1,0,1, 1,0,0,0,1,0,1,0, 0,0,1,0,0,0,1,1, 0,0,0,1,0,0,1,1, 0,1,0,1,1,0,1,1, 1,1,1,1,1,1,0,0, 1,0,0,0,1,1,1,1, 0,1,1,0,1,1,0,1, 1,1,1,0,0,0,0,0, 1,0,1,1,0,1,0,0, 0,1,1,1,1,0,0,0, 0,0,1,0,0,1,0,1, 0,0,1,0,1,1,0,0, 1,1,0,1,1,0,0,0, 1,1,0,1,0,0,1,0, 0,1,0,0,1,0,0,1, 1,0,0,0,0,1,1,0, 1,0,0,1,0,0,0,0, 0,0,0,1,1,1,1,0, 0,0,0,0,0,0,0,1, 0,1,1,1,0,0,0,1, 1,1,1,0,1,1,1,1, 0,0,1,0,0,1,0,1, 0,1,1,1,0,1,1,0, 1,1,0,1,1,1,0,1, 1,0,1,0,0,1,1,1, 1,0,0,0,1,0,1,1, 0,0,1,1,1,0,1,0, 0,0,0,1,0,1,1,1, 1,1,0,0,1,0,1,1, 0,1,1,1,1,1,1,1, 0,1,0,1,0,0,1,0, 1,0,1,0,1,0,0,0, 1,0,0,0,0,0,1,1, 1,1,0,1,0,1,0,0, 0,0,0,1,1,1,0,0, 0,1,0,0,0,0,1,0, 1,1,1,1,0,1,0,1, 0,0,0,1,1,0,1,0, 0,1,1,0,0,1,0,0, 1,1,1,1,0,1,1,0, 1,0,1,1,1,0,0,1, 1,0,1,0,0,0,0,1, 0,0,1,0,1,0,0,0, 0,0,1,1,1,0,0,1, 1,1,0,1,0,0,0,1, 0,1,0,0,1,1,0,1, 0,1,0,0,0,0,0,0, 1,0,0,1,0,0,1,1, 1,0,0,1,1,1,1,0, 1,1,1,1,1,0,1,1, 0,0,0,0,1,1,1,1, 1,0,1,0,1,1,1,1, 1,1,1,0,0,1,1,0, 0,0,1,1,0,0,0,0, 0,1,1,1,1,0,1,1, 0,1,0,0,0,1,0,0, 1,0,1,0,1,0,1,0, 1,0,0,1,1,1,0,0, 0,0,1,1,0,0,1,1, 1,1,1,1,0,0,1,0, 1,1,0,0,0,0,1,0, 0,1,1,0,0,1,1,0, 0,1,0,1,1,1,0,1, 0,0,1,1,1,1,1,0, 1,0,0,0,1,1,0,0, 1,1,0,0,1,0,0,1, 0,0,0,1,0,1,0,1, 1,0,0,1,0,1,0,1, 1,1,1,1,1,0,0,0, 0,0,0,0,0,0,1,1, 0,1,1,0,1,0,0,1, 0,1,1,0,1,0,1,1, 1,0,1,1,0,0,0,0, 1,0,1,1,0,1,1,1, 0,0,1,0,0,0,0,1, 1,1,0,0,0,0,0,0, 1,1,0,1,1,1,1,1, 1,0,0,1,1,0,0,0, 0,1,0,0,1,1,1,0, 0,0,0,0,1,1,0,0, 1,0,0,1,0,1,1,1, 0,1,1,0,0,0,1,0, 0,0,0,0,0,1,1,0, 1,0,1,1,1,0,1,0, 1,1,1,0,1,0,1,1, 1,1,0,0,1,1,1,0, 0,1,1,1,0,0,1,0, 0,1,0,1,0,0,0,1, 1,0,1,0,0,0,1,1, 0,0,0,0,0,1,0,1, 0,0,1,1,1,1,0,0, 1,1,1,0,1,1,0,1, 1,1,0,0,1,1,0,1, 1,0,1,1,0,0,1,1, 0,1,0,1,0,1,0,0, 0,0,1,0,0,1,1,1, 1,0,0,0,0,1,0,1, 0,1,0,1,1,1,1,1, 0,0,0,1,1,0,0,0, 1,0,0,0,1,0,0,0, 1,1,1,1,0,0,0,1, 1,1,1,0,0,1,0,0, 0,1,1,0,0,0,0,0, 0,1,1,1,1,1,0,0, 1,0,1,1,1,1,1,0, 0,0,1,0,1,0,1,0, 0,0,1,0,1,1,1,1, 0,1,0,1,0,1,1,0, 1,1,0,1,0,1,1,0, 1,0,0,0,0,0,0,1, 0,1,0,0,0,1,1,1", rs);

            rs = SendReadDiscreteinputs("1", "10001", "9", EFormat.INT16, "");
            Test_TestResultStrContains("invalid format", rs);

            rs = SendReadDiscreteinputs("1", "10001", "9", EFormat.UINT16, "");
            Test_TestResultStrContains("invalid format", rs);

            rs = SendReadDiscreteinputs("1", "10001", "9", EFormat.HEX16, "");
            Test_TestResultStrContains("invalid format", rs);

            rs = SendReadDiscreteinputs("1", "10001", "9", EFormat.INT32, "");
            Test_TestResultStrContains("invalid format", rs);

            rs = SendReadDiscreteinputs("1", "10001", "9", EFormat.UINT32, "");
            Test_TestResultStrContains("invalid format", rs);

            rs = SendReadDiscreteinputs("1", "10001", "9", EFormat.HEX32, "");
            Test_TestResultStrContains("invalid format", rs);

        }

        private void Test_SendReadCoils()
        {
            string rs;

            rs = SendReadCoils("", "1", "1", EFormat.HEXBYTES, "");
            Test_TestResultStrContains("invalid slaveid", rs);

            rs = SendReadCoils("1", "", "1", EFormat.HEXBYTES, "");
            Test_TestResultStrContains("invalid address", rs);

            rs = SendReadCoils("1", "a", "1", EFormat.HEXBYTES, "");
            Test_TestResultStrContains("invalid address", rs);

            rs = SendReadCoils("1", "1", "10", EFormat.HEXBYTES, "");
            Test_TestResultStrContains("0x10, 0x03", rs);

            rs = SendReadCoils("1", "9999", "1", EFormat.HEXBYTES, "");
            Test_TestResultStrContains("0x00", rs);

            rs = SendReadCoils("1", "10001", "10", EFormat.HEXBYTES, "");
            Test_TestResultStrContains("invalid address", rs);

            rs = SendReadCoils("1", "40001", "10", EFormat.HEXBYTES, "");
            Test_TestResultStrContains("invalid address", rs);

            rs = SendReadCoils("1", "100000", "10", EFormat.HEXBYTES, "");
            Test_TestResultStrContains("invalid address", rs);

            rs = SendReadCoils("1", "1", "10", EFormat.HEXBYTES, "");
            Test_TestResultStrEquals("0x10, 0x03", rs);

            rs = SendReadCoils("1", "9999", "100", EFormat.HEXBYTES, "");
            Test_TestResultStrContains("0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00", rs);

            rs = SendReadCoils("1", "1", "", EFormat.HEXBYTES, "");
            Test_TestResultStrContains("invalid qty", rs);

            rs = SendReadCoils("1", "1", "0", EFormat.HEXBYTES, "");
            Test_TestResultStrContains("no data", rs);

            rs = SendReadCoils("1", "1", "1", EFormat.HEXBYTES, "");
            Test_TestResultStrEquals("0x00", rs);

            rs = SendReadCoils("1", "1", "10", EFormat.HEXBYTES, "");
            Test_TestResultStrEquals("0x10, 0x03", rs);

            rs = SendReadCoils("1", "1", "100", EFormat.HEXBYTES, "");
            Test_TestResultStrEquals("0x10, 0x27, 0x87, 0x3D, 0xFE, 0x53, 0x75, 0x6A, 0xEC, 0x80, 0x63, 0x97, 0x0A", rs);

            rs = SendReadCoils("1", "1", "2000", EFormat.HEXBYTES, "");
            Test_TestResultStrEquals("0x10, 0x27, 0x87, 0x3D, 0xFE, 0x53, 0x75, 0x6A, 0xEC, 0x80, 0x63, 0x97, 0xDA, 0xAD, 0x51, 0xC4, 0xC8, 0xDA, 0x3F, 0xF1, 0xB6, 0x07, 0x2D, 0x1E, 0xA4, 0x34, 0x1B, 0x4B, 0x92, 0x61, 0x09, 0x78, 0x80, 0x8E, 0xF7, 0xA4, 0x6E, 0xBB, 0xE5, 0xD1, 0x5C, 0xE8, 0xD3, 0xFE, 0x4A, 0x15, 0xC1, 0x2B, 0x38, 0x42, 0xAF, 0x58, 0x26, 0x6F, 0x9D, 0x85, 0x14, 0x9C, 0x8B, 0xB2, 0x02, 0xC9, 0x79, 0xDF, 0xF0, 0xF5, 0x67, 0x0C, 0xDE, 0x22, 0x55, 0x39, 0xCC, 0x4F, 0x43, 0x66, 0xBA, 0x7C, 0x31, 0x93, 0xA8, 0xA9, 0x1F, 0xC0, 0x96, 0xD6, 0x0D, 0xED, 0x84, 0x03, 0xFB, 0x19, 0x72, 0x30, 0xE9, 0x46, 0x60, 0x5D, 0xD7, 0x73, 0x4E, 0x8A, 0xC5, 0xA0, 0x3C, 0xB7, 0xB3, 0xCD, 0x2A, 0xE4, 0xA1, 0xFA, 0x18, 0x11, 0x8F, 0x27, 0x06, 0x3E, 0x7D, 0x54, 0xF4, 0x6A, 0x6B, 0x81, 0xE2, 0x97, 0x59, 0xAE, 0xD0, 0xC4, 0x47, 0xDB, 0xBE, 0xF1, 0x35, 0x08, 0xAC, 0x1E, 0x23, 0x35, 0x9A, 0x4B, 0x11, 0x62, 0x88, 0x78, 0xFF, 0x8E, 0x76, 0xA5, 0xED, 0xBB, 0x64, 0xD2, 0xDB, 0xE8, 0x52, 0xFF, 0xC9, 0x15, 0x40, 0x2C, 0xB7, 0x42, 0x2E, 0x59, 0xA5, 0x6F, 0x1C, 0x86, 0x93, 0x9C, 0x0A, 0xB3, 0x81, 0xC9, 0xF8, 0xDF, 0x6F, 0xF6, 0xE6, 0x0C, 0x5D, 0x23, 0xD4, 0x39, 0x4B, 0x50, 0xC2, 0x66, 0x39, 0x7D, 0xB0, 0x93, 0x27, 0xAA, 0x9E, 0xC0, 0x15, 0xD7, 0x8C, 0xED, 0x03, 0x04, 0x7A, 0x1A, 0xF1, 0x30, 0x68, 0x47, 0xDF, 0x5D, 0x56, 0x74, 0xCD, 0x8A, 0x44, 0xA1, 0xBB, 0xB7, 0x32, 0xCE, 0xA9, 0xE4, 0x20, 0xFB, 0x97, 0x11, 0x0E, 0x28, 0x85, 0x3E, 0xFC, 0x54, 0x73, 0x6B, 0xEA, 0x81, 0x61, 0x98, 0xD8, 0xAE, 0x4F, 0xC5, 0xC6, 0xDB, 0x00, 0x00, 0x00, 0x00", rs);

            rs = SendReadCoils("1", "1", "3000", EFormat.HEXBYTES, "");
            Test_TestResultStrContains("RESPONSE DATA ERROR", rs);

            rs = SendReadCoils("1", "1", "0", EFormat.BITS, "");
            Test_TestResultStrContains("no data", rs);

            rs = SendReadCoils("1", "1", "1", EFormat.BITS, "");
            Test_TestResultStrEquals("0", rs);

            rs = SendReadCoils("1", "1", "7", EFormat.BITS, "");
            Test_TestResultStrEquals("0,0,0,0,1,0,0", rs);

            rs = SendReadCoils("1", "1", "8", EFormat.BITS, "");
            Test_TestResultStrEquals("0,0,0,0,1,0,0,0", rs);

            rs = SendReadCoils("1", "1", "9", EFormat.BITS, "");
            Test_TestResultStrEquals("0,0,0,0,1,0,0,0, 1", rs);

            rs = SendReadCoils("1", "1", "100", EFormat.BITS, "");
            Test_TestResultStrEquals("0,0,0,0,1,0,0,0, 1,1,1,0,0,1,0,0, 1,1,1,0,0,0,0,1, 1,0,1,1,1,1,0,0, 0,1,1,1,1,1,1,1, 1,1,0,0,1,0,1,0, 1,0,1,0,1,1,1,0, 0,1,0,1,0,1,1,0, 0,0,1,1,0,1,1,1, 0,0,0,0,0,0,0,1, 1,1,0,0,0,1,1,0, 1,1,1,0,1,0,0,1, 0,1,0,1", rs);

            rs = SendReadCoils("1", "1", "1000", EFormat.BITS, "");
            Test_TestResultStrEquals("0,0,0,0,1,0,0,0, 1,1,1,0,0,1,0,0, 1,1,1,0,0,0,0,1, 1,0,1,1,1,1,0,0, 0,1,1,1,1,1,1,1, 1,1,0,0,1,0,1,0, 1,0,1,0,1,1,1,0, 0,1,0,1,0,1,1,0, 0,0,1,1,0,1,1,1, 0,0,0,0,0,0,0,1, 1,1,0,0,0,1,1,0, 1,1,1,0,1,0,0,1, 0,1,0,1,1,0,1,1, 1,0,1,1,0,1,0,1, 1,0,0,0,1,0,1,0, 0,0,1,0,0,0,1,1, 0,0,0,1,0,0,1,1, 0,1,0,1,1,0,1,1, 1,1,1,1,1,1,0,0, 1,0,0,0,1,1,1,1, 0,1,1,0,1,1,0,1, 1,1,1,0,0,0,0,0, 1,0,1,1,0,1,0,0, 0,1,1,1,1,0,0,0, 0,0,1,0,0,1,0,1, 0,0,1,0,1,1,0,0, 1,1,0,1,1,0,0,0, 1,1,0,1,0,0,1,0, 0,1,0,0,1,0,0,1, 1,0,0,0,0,1,1,0, 1,0,0,1,0,0,0,0, 0,0,0,1,1,1,1,0, 0,0,0,0,0,0,0,1, 0,1,1,1,0,0,0,1, 1,1,1,0,1,1,1,1, 0,0,1,0,0,1,0,1, 0,1,1,1,0,1,1,0, 1,1,0,1,1,1,0,1, 1,0,1,0,0,1,1,1, 1,0,0,0,1,0,1,1, 0,0,1,1,1,0,1,0, 0,0,0,1,0,1,1,1, 1,1,0,0,1,0,1,1, 0,1,1,1,1,1,1,1, 0,1,0,1,0,0,1,0, 1,0,1,0,1,0,0,0, 1,0,0,0,0,0,1,1, 1,1,0,1,0,1,0,0, 0,0,0,1,1,1,0,0, 0,1,0,0,0,0,1,0, 1,1,1,1,0,1,0,1, 0,0,0,1,1,0,1,0, 0,1,1,0,0,1,0,0, 1,1,1,1,0,1,1,0, 1,0,1,1,1,0,0,1, 1,0,1,0,0,0,0,1, 0,0,1,0,1,0,0,0, 0,0,1,1,1,0,0,1, 1,1,0,1,0,0,0,1, 0,1,0,0,1,1,0,1, 0,1,0,0,0,0,0,0, 1,0,0,1,0,0,1,1, 1,0,0,1,1,1,1,0, 1,1,1,1,1,0,1,1, 0,0,0,0,1,1,1,1, 1,0,1,0,1,1,1,1, 1,1,1,0,0,1,1,0, 0,0,1,1,0,0,0,0, 0,1,1,1,1,0,1,1, 0,1,0,0,0,1,0,0, 1,0,1,0,1,0,1,0, 1,0,0,1,1,1,0,0, 0,0,1,1,0,0,1,1, 1,1,1,1,0,0,1,0, 1,1,0,0,0,0,1,0, 0,1,1,0,0,1,1,0, 0,1,0,1,1,1,0,1, 0,0,1,1,1,1,1,0, 1,0,0,0,1,1,0,0, 1,1,0,0,1,0,0,1, 0,0,0,1,0,1,0,1, 1,0,0,1,0,1,0,1, 1,1,1,1,1,0,0,0, 0,0,0,0,0,0,1,1, 0,1,1,0,1,0,0,1, 0,1,1,0,1,0,1,1, 1,0,1,1,0,0,0,0, 1,0,1,1,0,1,1,1, 0,0,1,0,0,0,0,1, 1,1,0,0,0,0,0,0, 1,1,0,1,1,1,1,1, 1,0,0,1,1,0,0,0, 0,1,0,0,1,1,1,0, 0,0,0,0,1,1,0,0, 1,0,0,1,0,1,1,1, 0,1,1,0,0,0,1,0, 0,0,0,0,0,1,1,0, 1,0,1,1,1,0,1,0, 1,1,1,0,1,0,1,1, 1,1,0,0,1,1,1,0, 0,1,1,1,0,0,1,0, 0,1,0,1,0,0,0,1, 1,0,1,0,0,0,1,1, 0,0,0,0,0,1,0,1, 0,0,1,1,1,1,0,0, 1,1,1,0,1,1,0,1, 1,1,0,0,1,1,0,1, 1,0,1,1,0,0,1,1, 0,1,0,1,0,1,0,0, 0,0,1,0,0,1,1,1, 1,0,0,0,0,1,0,1, 0,1,0,1,1,1,1,1, 0,0,0,1,1,0,0,0, 1,0,0,0,1,0,0,0, 1,1,1,1,0,0,0,1, 1,1,1,0,0,1,0,0, 0,1,1,0,0,0,0,0, 0,1,1,1,1,1,0,0, 1,0,1,1,1,1,1,0, 0,0,1,0,1,0,1,0, 0,0,1,0,1,1,1,1, 0,1,0,1,0,1,1,0, 1,1,0,1,0,1,1,0, 1,0,0,0,0,0,0,1, 0,1,0,0,0,1,1,1", rs);

            rs = SendReadCoils("1", "1", "1000", EFormat.INT16, "");
            Test_TestResultStrContains("invalid format", rs);

            rs = SendReadCoils("1", "1", "1000", EFormat.UINT16, "");
            Test_TestResultStrContains("invalid format", rs);

            rs = SendReadCoils("1", "1", "1000", EFormat.HEX16, "");
            Test_TestResultStrContains("invalid format", rs);

            rs = SendReadCoils("1", "1", "1000", EFormat.INT32, "");
            Test_TestResultStrContains("invalid format", rs);

            rs = SendReadCoils("1", "1", "1000", EFormat.UINT32, "");
            Test_TestResultStrContains("invalid format", rs);

            rs = SendReadCoils("1", "1", "1000", EFormat.HEX32, "");
            Test_TestResultStrContains("invalid format", rs);

        }

        private void Test_SendWriteSingleCoil()
        {
            string rs;

            rs = SendWriteSingleCoil("", "1", "1", EFormat.BITS, "0");
            Test_TestResultStrContains("invalid slaveid", rs);

            rs = SendWriteSingleCoil("1", "", "1", EFormat.BITS, "0");
            Test_TestResultStrContains("invalid address", rs);

            rs = SendWriteSingleCoil("1", "a", "1", EFormat.BITS, "0");
            Test_TestResultStrContains("invalid address", rs);

            rs = SendWriteSingleCoil("1", "1", "", EFormat.BITS, "0");
            Test_TestResultStrContains("invalid qty", rs);

            rs = SendWriteSingleCoil("1", "1", "10", EFormat.BITS, "0");
            Test_TestResultStrContains("invalid qty", rs);

            rs = SendWriteSingleCoil("1", "1", "2", EFormat.BITS, "0");
            Test_TestResultStrContains("invalid qty", rs);

            rs = SendWriteSingleCoil("1", "1", "1", EFormat.BITS, "0");
            Test_TestResultStrContains("success", rs);
            Test_CoilValuesEqual(1, "0");

            rs = SendWriteSingleCoil("1", "9999", "1", EFormat.BITS, "0");
            Test_TestResultStrContains("success", rs);
            Test_CoilValuesEqual(9999, "0");

            rs = SendWriteSingleCoil("1", "10000", "1", EFormat.BITS, "0");
            Test_TestResultStrContains("invalid address", rs);

            rs = SendWriteSingleCoil("1", "40001", "1", EFormat.BITS, "0");
            Test_TestResultStrContains("invalid address", rs);

            rs = SendWriteSingleCoil("1", "1", "1", EFormat.BITS, "");
            Test_TestResultStrContains("Error: No data", rs);

            rs = SendWriteSingleCoil("1", "1", "1", EFormat.BITS, "0");
            Test_TestResultStrContains("success", rs);
            rs = SendWriteSingleCoil("1", "2", "1", EFormat.BITS, "1");
            Test_TestResultStrContains("success", rs);
            Test_CoilValuesEqual(1, "0,1");

            rs = SendWriteSingleCoil("1", "1", "1", EFormat.BITS, "1");
            Test_TestResultStrContains("success", rs);
            rs = SendWriteSingleCoil("1", "2", "1", EFormat.BITS, "0");
            Test_TestResultStrContains("success", rs);
            Test_CoilValuesEqual(1, "1,0");

            rs = SendWriteSingleCoil("1", "1", "1", EFormat.BITS, "2");
            Test_TestResultStrContains("invalid value", rs);

            rs = SendWriteSingleCoil("1", "3", "1", EFormat.BITS, "000 ");
            Test_TestResultStrContains("success", rs);

            rs = SendWriteSingleCoil("1", "4", "1", EFormat.BITS, "001 ");
            Test_TestResultStrContains("success", rs);
            Test_CoilValuesEqual(3, "0,1");

            rs = SendWriteSingleCoil("1", "1", "1", EFormat.BITS, "0, 1 ");
            Test_TestResultStrContains("Expected 1 values, found 2", rs);

            rs = SendWriteSingleCoil("1", "1", "1", EFormat.INT16, "001 ");
            Test_TestResultStrContains("invalid format", rs);

            rs = SendWriteSingleCoil("1", "1", "1", EFormat.UINT16, "001 ");
            Test_TestResultStrContains("invalid format", rs);

            rs = SendWriteSingleCoil("1", "1", "1", EFormat.HEX16, "001 ");
            Test_TestResultStrContains("invalid format", rs);

            rs = SendWriteSingleCoil("1", "1", "1", EFormat.INT32, "001 ");
            Test_TestResultStrContains("invalid format", rs);

            rs = SendWriteSingleCoil("1", "1", "1", EFormat.UINT32, "001 ");
            Test_TestResultStrContains("invalid format", rs);

            rs = SendWriteSingleCoil("1", "1", "1", EFormat.HEX32, "001 ");
            Test_TestResultStrContains("invalid format", rs);

            // init first coils with testpattern (shared with registers)
            rs = SendWriteMultipleHoldingreg("1", "40001", "2", EFormat.UINT16, "1101 , 1102");
            Test_TestResultStrContains("success", rs);

            rs = SendWriteSingleCoil("1", "1", "1", EFormat.HEXBYTES, "0");
            Test_TestResultStrContains("success", rs);
            Test_CoilValuesEqual(1, "0,0,1,1,0,0,1,0");

            rs = SendWriteSingleCoil("1", "1", "1", EFormat.HEXBYTES, "1");
            Test_TestResultStrContains("success", rs);
            Test_CoilValuesEqual(1, "1,0,1,1,0,0,1,0");

            rs = SendWriteSingleCoil("1", "1", "1", EFormat.HEXBYTES, "2");
            Test_TestResultStrContains("invalid value", rs);

            rs = SendWriteSingleCoil("1", "1", "1", EFormat.HEXBYTES, "0x00");
            Test_TestResultStrContains("success", rs);
            Test_CoilValuesEqual(1, "0,0,1,1,0,0,1,0");

            rs = SendWriteSingleCoil("1", "1", "1", EFormat.HEXBYTES, "0x01");
            Test_TestResultStrContains("success", rs);
            Test_CoilValuesEqual(1, "1,0,1,1,0,0,1,0");

            rs = SendWriteSingleCoil("1", "1", "1", EFormat.HEXBYTES, "0x02");
            Test_TestResultStrContains("invalid value", rs);

            rs = SendWriteSingleCoil("1", "1", "1", EFormat.HEXBYTES, "0x21");
            Test_TestResultStrContains("invalid value", rs);

            rs = SendWriteSingleCoil("1", "1", "1", EFormat.HEXBYTES, "0x0FA");
            Test_TestResultStrContains("invalid value", rs);

            rs = SendWriteSingleCoil("1", "1", "1", EFormat.HEXBYTES, "258");
            Test_TestResultStrContains("invalid value", rs);

            rs = SendWriteSingleCoil("1", "1", "1", EFormat.HEXBYTES, "000 ");
            Test_TestResultStrContains("success", rs);

            rs = SendWriteSingleCoil("1", "1", "1", EFormat.HEXBYTES, "0x0001 ");
            Test_TestResultStrContains("success", rs);

            rs = SendWriteSingleCoil("1", "1", "1", EFormat.HEXBYTES, "0x01, 0x02 ");
            Test_TestResultStrContains("Expected 1 values, found 2", rs);

        }

        private void Test_SendWriteMultipleCoils()
        {
            string rs;

            rs = SendWriteMultipleCoils("", "1", "1", EFormat.BITS, "0");
            Test_TestResultStrContains("invalid slaveid", rs);

            rs = SendWriteMultipleCoils("1", "", "1", EFormat.BITS, "0");
            Test_TestResultStrContains("invalid address", rs);

            rs = SendWriteMultipleCoils("1", "a", "1", EFormat.BITS, "0");
            Test_TestResultStrContains("invalid address", rs);

            rs = SendWriteMultipleCoils("1", "1", "", EFormat.BITS, "0");
            Test_TestResultStrContains("invalid qty", rs);

            rs = SendWriteMultipleCoils("1", "1", "a", EFormat.BITS, "0");
            Test_TestResultStrContains("invalid qty", rs);

            rs = SendWriteMultipleCoils("1", "1", "20", EFormat.BITS, "1,0,1,1,0,0,1,0,1,1,0,1,0,0,1,0,1,1,0,0");
            Test_TestResultStrContains("success", rs);
            Test_CoilValuesEqual(1, "1,0,1,1,0,0,1,0,1,1,0,1,0,0,1,0,1,1,0,0");

            rs = SendWriteMultipleCoils("1", "1", "1", EFormat.BITS, "0");
            Test_TestResultStrContains("success", rs);
            Test_CoilValuesEqual(1, "0,0,1,1,0,0,1,0");

            rs = SendWriteMultipleCoils("1", "9999", "1", EFormat.BITS, "0");
            Test_TestResultStrContains("success", rs);

            rs = SendWriteMultipleCoils("1", "10000", "1", EFormat.BITS, "0");
            Test_TestResultStrContains("invalid address", rs);

            rs = SendWriteMultipleCoils("1", "1", "1", EFormat.HEXBYTES, "0x3A");  // 00111010
            Test_TestResultStrContains("success", rs);
            Test_CoilValuesEqual(1, "0,0,1,1,0,0,1,0,1,1,0,1,0,0,1,0,1,1,0,0");

            rs = SendWriteMultipleCoils("1", "1", "2", EFormat.HEXBYTES, "0x3A");
            Test_TestResultStrContains("success", rs);
            Test_CoilValuesEqual(1, "0,1,1,1,0,0,1,0,1,1,0,1,0,0,1,0,1,1,0,0");

            rs = SendWriteMultipleCoils("1", "1", "3", EFormat.HEXBYTES, "0x3A");
            Test_TestResultStrContains("success", rs);
            Test_CoilValuesEqual(1, "0,1,0,1,0,0,1,0,1,1,0,1,0,0,1,0,1,1,0,0");

            rs = SendWriteMultipleCoils("1", "1", "8", EFormat.HEXBYTES, "0x3A");
            Test_TestResultStrContains("success", rs);
            Test_CoilValuesEqual(1, "0,1,0,1,1,1,0,0,1,1,0,1,0,0,1,0,1,1,0,0");

            rs = SendWriteMultipleCoils("1", "1", "9", EFormat.HEXBYTES, "0x3A");
            Test_TestResultStrContains("Expected 2 values, found 1", rs);

            rs = SendWriteMultipleCoils("1", "1", "9", EFormat.HEXBYTES, "0x3A, 0x55");
            Test_TestResultStrContains("success", rs);
            Test_CoilValuesEqual(1, "0,1,0,1,1,1,0,0,1,1,0,1,0,0,1,0,1,1,0,0");

            rs = SendWriteMultipleCoils("1", "1", "10", EFormat.HEXBYTES, "0x3A, 0x55");
            Test_TestResultStrContains("success", rs);
            Test_CoilValuesEqual(1, "0,1,0,1,1,1,0,0,1,0,0,1,0,0,1,0,1,1,0,0");

            rs = SendWriteMultipleCoils("1", "1", "11", EFormat.HEXBYTES, "0x3A, 0x55");
            Test_TestResultStrContains("success", rs);
            Test_CoilValuesEqual(1, "0,1,0,1,1,1,0,0,1,0,1,1,0,0,1,0,1,1,0,0");

            rs = SendWriteMultipleCoils("1", "1", "1968", EFormat.BITS, "0,0,1,1,1,1,0,1,1,1,0,0,1,0,0,1,0,1,1,0,0,0,0,0,0,1,1,0,0,0,1,0,1,0,0,0,1,0,0,0,0,0,1,0,0,0,0,0,0,0,0,1,0,1,0,1,1,0,1,0,0,1,1,1,0,1,1,0,1,0,0,1,0,1,1,0,1,1,0,1,0,0,1,0,1,0,0,1,1,0,0,1,0,0,0,0,1,0,0,1,0,1,1,0,1,0,1,1,0,1,0,0,1,1,0,0,0,0,1,1,0,0,0,0,0,0,0,1,0,0,0,0,1,1,1,0,1,1,1,1,0,0,1,1,1,1,0,0,0,1,1,1,1,0,0,0,0,1,0,0,1,1,1,1,1,0,1,0,1,0,1,1,1,0,0,0,0,0,1,0,0,0,0,1,1,0,1,0,0,1,1,1,1,1,0,0,1,0,0,0,1,0,1,1,1,0,0,1,0,1,1,0,1,0,1,0,0,0,1,0,1,1,0,1,1,1,1,0,0,0,1,0,1,0,0,0,1,1,1,0,1,0,1,0,0,1,1,1,0,0,0,0,0,1,1,1,1,1,0,1,0,0,0,0,0,1,1,1,1,0,0,0,1,0,0,1,1,0,1,1,1,1,1,1,0,0,1,1,1,0,0,1,0,1,0,1,1,1,0,0,0,1,0,0,1,0,1,1,0,1,1,1,0,0,0,0,0,0,1,1,1,0,1,1,1,0,0,0,1,1,0,1,0,1,1,0,0,0,1,1,1,1,1,0,1,1,0,0,0,1,1,1,0,1,1,0,1,1,0,1,1,0,0,0,0,1,0,1,1,1,0,0,1,1,1,0,1,0,0,1,0,0,1,1,1,1,1,0,1,1,0,1,0,0,1,1,0,0,1,0,0,0,1,0,1,1,0,0,0,0,1,1,1,0,0,1,0,1,1,0,0,1,0,0,1,1,1,0,1,0,0,0,1,1,0,1,1,0,1,1,1,0,1,0,1,1,1,0,1,1,0,0,1,0,1,0,0,0,1,1,1,0,0,1,0,0,1,1,0,0,1,1,0,1,1,1,0,1,0,0,1,0,0,0,0,1,1,1,0,0,0,0,0,1,0,0,1,1,0,0,0,0,1,0,1,0,0,0,1,1,1,1,0,1,0,1,1,0,0,0,0,0,0,0,1,1,1,0,1,0,1,0,1,1,1,0,1,0,1,0,1,0,1,0,0,0,1,1,1,1,0,0,0,1,0,1,0,0,0,0,0,0,0,1,1,0,1,1,0,1,1,0,0,1,0,1,1,0,0,0,0,0,0,0,1,1,1,1,0,1,0,1,0,0,0,1,0,0,1,1,1,1,0,0,0,0,0,1,1,0,1,1,1,0,1,0,0,0,1,0,0,1,1,0,1,1,1,0,1,1,0,1,1,0,1,0,0,1,1,1,1,0,0,1,0,1,1,1,0,1,0,0,0,0,0,0,0,1,1,1,1,1,1,0,0,1,0,1,1,0,1,1,1,0,1,1,1,1,0,0,0,0,1,1,0,0,1,1,1,0,0,1,0,1,0,0,1,1,1,0,1,0,1,0,1,1,0,1,0,0,0,0,1,1,1,0,1,1,0,1,1,0,0,0,0,1,0,1,0,0,1,0,1,0,0,0,0,1,1,1,0,1,1,1,0,1,1,0,1,1,1,0,1,0,1,1,0,0,0,1,1,0,0,0,0,0,0,1,1,0,0,0,1,1,1,0,1,1,1,0,0,1,1,1,0,0,0,1,1,0,1,0,1,1,1,0,0,1,0,1,0,1,0,0,1,0,1,1,1,1,1,0,0,0,1,1,1,1,1,0,0,1,0,0,1,1,0,0,1,0,0,0,1,1,1,0,1,0,0,0,1,0,1,1,0,0,1,1,0,1,0,1,0,0,1,0,0,1,0,1,1,0,1,0,0,1,1,0,1,1,1,0,1,0,0,0,0,1,0,1,0,1,0,0,0,1,1,0,1,0,1,1,1,0,1,0,1,1,1,1,0,1,1,1,0,1,0,1,0,0,1,0,1,1,1,0,0,0,0,1,1,1,0,0,1,0,1,1,1,1,0,0,1,1,1,0,0,0,1,1,1,1,1,1,1,0,1,0,0,0,0,1,1,1,1,0,1,0,1,0,0,1,1,1,0,1,1,1,0,1,1,0,1,0,0,0,1,1,0,0,0,0,0,1,0,0,1,1,0,1,1,0,0,0,1,0,1,0,0,1,0,1,1,1,0,0,0,0,1,1,1,1,0,0,1,1,1,0,1,0,1,1,0,0,1,0,1,0,1,1,1,0,1,1,1,1,0,0,0,0,1,1,0,0,1,1,1,0,1,1,0,1,1,1,0,1,0,1,1,0,1,0,0,0,0,1,1,1,1,1,0,0,1,1,0,1,0,0,1,0,1,0,0,0,0,1,0,1,0,0,0,0,1,1,1,1,1,0,1,0,0,1,1,0,1,0,1,0,0,1,0,1,1,1,0,1,1,0,0,1,0,0,0,0,1,0,0,1,0,0,1,0,1,0,0,1,0,1,0,0,0,1,1,1,1,0,1,1,0,1,1,0,1,0,0,1,0,1,1,1,0,1,0,1,0,0,1,0,0,0,0,1,1,0,0,1,0,0,0,1,1,1,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,1,1,0,1,0,1,0,1,1,0,1,0,1,0,0,0,1,0,1,0,0,1,1,0,1,1,0,1,1,1,1,1,1,1,0,1,1,0,1,0,0,0,0,1,0,0,0,1,1,1,0,1,0,1,0,0,0,1,0,0,1,1,1,0,1,0,1,0,1,0,1,1,1,0,1,1,0,0,0,1,0,0,1,1,1,0,0,1,1,0,1,1,1,0,0,1,0,1,1,0,1,1,0,0,0,1,1,1,1,1,0,0,1,1,0,1,0,1,1,0,0,1,1,1,1,1,0,1,1,0,0,0,1,1,1,1,0,0,0,0,0,1,0,1,0,1,1,0,0,1,0,0,1,0,1,0,0,0,1,0,0,0,0,0,1,1,1,1,1,1,0,1,0,0,1,0,1,0,0,0,1,0,0,0,0,0,0,0,0,0,1,1,1,1,0,0,1,1,1,0,0,1,1,1,0,1,0,1,0,1,1,1,0,1,0,1,1,0,1,1,1,1,1,0,1,1,0,1,1,1,0,0,0,1,1,0,0,0,0,0,1,0,0,0,1,0,0,1,1,1,1,0,0,0,1,0,0,0,1,0,0,0,0,0,0,1,0,0,0,0,1,1,0,1,0,0,1,0,1,0,1,0,1,0,1,1,1,0,1,1,0,1,0,1,1,1,0,0,1,0,0,0,0,0,1,1,1,0,0,0,0,0,1,0,0,0,1,0,0,1,1,1,1,0,1,1,1,1,1,0,1,1,1,1,1,1,0,0,0,0,1,1,0,0,1,0,0,0,1,0,1,0,1,0,1,1,0,1,0,1,1,1,0,0,1,1,1,1,1,1,1,1,0,1,0,1,0,1,0,0,1,1,1,1,1,1,1,1,1,1,0,0,1,0,1,0,0,1,0,1,0,0,0,0,1,1,1,0,1,0,0,0,0,0,0,1,1,0,1,0,0,0,0,0,0,0,1,0,1,0,1,1,1,1,0,0,0,0,0,1,0,0,1,0,1,1,1,1,0,0,0,1,0,0,1,0,1,1,0,1,0,0,1,1,1,1,1,0,1,1,0,1,1,0,1,1,0,0,0,1,1,1,0,1,0,0,1,0,0,1,0,0,1,1,1,0,1,0,0,0,1,0,1,1,1,0,0,0,0,1,1,0,0,0,0,0,1,0,1,1,1,0,0,1,1,1,0,0,1,0,0,1,1,0,0,1,0,1,1,1,0,0,1,1,0,1,0,0,1,1,0,1,1,0,0,1,1,1,1,0,0,1,1,0,1,1,0,0,1,0,1,0,0,1,1,0,1,0,1,1,0,0,1,1,0,1,1,0,0,0,0,1,1,0,1,0,0,1,0,1,0,0,0,0,1,1,0,0,1,1,0,1,0,1,0,0,0,0,1,0,0,0,1,1,1,0,1,0,1,0,1,0,0,0,0,0,0,0,1,1,0,1,0,0,0,1,0,0,1,0,1,0,0,0,1,0,0,0,0,1,0,1,1,0,0,0,0,1,1,1,0,0,1,0,0,0,0,0,1,0,1,0,1,0,1,0,1,1,1,1,1,1,0,0,1,1,0,0,1,0,0,0,1,1,0,0,0,0,0,1,0,1,0,0,1,0,0,0,1,1,1,0,0,0,0,1,0,0,0,0,1,0,0,1,0,1,1,1,0,0,0,1,0,0,1,0,0,0,0,1,0,0,1,1,1,1,1,0,0,1,0,1,1,1,1,1,1,0,1,1,0");
            Test_TestResultStrContains("success", rs);
            Test_RegisterValuesEqual(40001, "0x93bc,0x4606,0x0411,0xe5a8,0xb696,0x0994,0x2d69,0x80c3,0xcf70,0x21e3,0x1d5f,0xe584,0x9d13,0xb456,0x7147,0xe0e5,0x1e0b,0xcfd9,0x23a9,0xc0ed,0x6b1d,0xe37c,0xa1b6,0xc973,0x4cb7,0x9c34,0x1726,0x75db,0x9c53,0x2ecc,0x20e1,0xf143,0x701a,0x5575,0x0a3c,0xa6d8,0xaf01,0x83c8,0x645d,0xe5b7,0x02e9,0xda7e,0xcc3d,0x5729,0xb70b,0x14a1,0x76ee,0x818d,0xe771,0xa758,0xc7d2,0x1327,0xcd17,0x5a4a,0xa176,0x5d62,0x4aef,0xd387,0xfc73,0x5785,0x16ee,0x6c83,0x0e94,0x35cf,0x0f75,0xbb73,0x3e16,0xa14b,0x65f0,0x9ba5,0x9490,0x6de2,0x4ae9,0x3898,0x6100,0xa2b5,0x7f6c,0x710b,0xae45,0x91ba,0xd3b3,0x67c6,0x37cd,0xd41e,0x08a4,0x297e,0x7802,0xd5ce,0x6fb5,0x20c7,0x88f2,0x5840,0x6eaa,0xe09d,0xf220,0x1fbe,0xaa26,0xfe75,0xfe55,0x14a7,0xb02e,0x7a80,0x1e90,0xf969,0x71b6,0x2e49,0x0c3a,0x273a,0x59d3,0xcf36,0xaca6,0xb0d9,0x6614,0xb885,0x580a,0x08a4,0x270d,0xfaa8,0x1899,0x3894,0x7484,0xe424,0x6fd3");

            rs = SendWriteMultipleCoils("1", "1", "1968", EFormat.BITS, "0,0,1,1,1,1,0,1,1,1,0,0,1,0,0,1,0,1,1,0,0,0,0,0,0,1,1,0,0,0,1,0,1,0,0,0,1,0,0,0,0,0,1,0,0,0,0,0,0,0,0,1,0,1,0,1,1,0,1,0,0,1,1,1,0,1,1,0,1,0,0,1,0,1,1,0,1,1,0,1,0,0,1,0,1,0,0,1,1,0,0,1,0,0,0,0,1,0,0,1,0,1,1,0,1,0,1,1,0,1,0,0,1,1,0,0,0,0,1,1,0,0,0,0,0,0,0,1,0,0,0,0,1,1,1,0,1,1,1,1,0,0,1,1,1,1,0,0,0,1,1,1,1,0,0,0,0,1,0,0,1,1,1,1,1,0,1,0,1,0,1,1,1,0,0,0,0,0,1,0,0,0,0,1,1,0,1,0,0,1,1,1,1,1,0,0,1,0,0,0,1,0,1,1,1,0,0,1,0,1,1,0,1,0,1,0,0,0,1,0,1,1,0,1,1,1,1,0,0,0,1,0,1,0,0,0,1,1,1,0,1,0,1,0,0,1,1,1,0,0,0,0,0,1,1,1,1,1,0,1,0,0,0,0,0,1,1,1,1,0,0,0,1,0,0,1,1,0,1,1,1,1,1,1,0,0,1,1,1,0,0,1,0,1,0,1,1,1,0,0,0,1,0,0,1,0,1,1,0,1,1,1,0,0,0,0,0,0,1,1,1,0,1,1,1,0,0,0,1,1,0,1,0,1,1,0,0,0,1,1,1,1,1,0,1,1,0,0,0,1,1,1,0,1,1,0,1,1,0,1,1,0,0,0,0,1,0,1,1,1,0,0,1,1,1,0,1,0,0,1,0,0,1,1,1,1,1,0,1,1,0,1,0,0,1,1,0,0,1,0,0,0,1,0,1,1,0,0,0,0,1,1,1,0,0,1,0,1,1,0,0,1,0,0,1,1,1,0,1,0,0,0,1,1,0,1,1,0,1,1,1,0,1,0,1,1,1,0,1,1,0,0,1,0,1,0,0,0,1,1,1,0,0,1,0,0,1,1,0,0,1,1,0,1,1,1,0,1,0,0,1,0,0,0,0,1,1,1,0,0,0,0,0,1,0,0,1,1,0,0,0,0,1,0,1,0,0,0,1,1,1,1,0,1,0,1,1,0,0,0,0,0,0,0,1,1,1,0,1,0,1,0,1,1,1,0,1,0,1,0,1,0,1,0,0,0,1,1,1,1,0,0,0,1,0,1,0,0,0,0,0,0,0,1,1,0,1,1,0,1,1,0,0,1,0,1,1,0,0,0,0,0,0,0,1,1,1,1,0,1,0,1,0,0,0,1,0,0,1,1,1,1,0,0,0,0,0,1,1,0,1,1,1,0,1,0,0,0,1,0,0,1,1,0,1,1,1,0,1,1,0,1,1,0,1,0,0,1,1,1,1,0,0,1,0,1,1,1,0,1,0,0,0,0,0,0,0,1,1,1,1,1,1,0,0,1,0,1,1,0,1,1,1,0,1,1,1,1,0,0,0,0,1,1,0,0,1,1,1,0,0,1,0,1,0,0,1,1,1,0,1,0,1,0,1,1,0,1,0,0,0,0,1,1,1,0,1,1,0,1,1,0,0,0,0,1,0,1,0,0,1,0,1,0,0,0,0,1,1,1,0,1,1,1,0,1,1,0,1,1,1,0,1,0,1,1,0,0,0,1,1,0,0,0,0,0,0,1,1,0,0,0,1,1,1,0,1,1,1,0,0,1,1,1,0,0,0,1,1,0,1,0,1,1,1,0,0,1,0,1,0,1,0,0,1,0,1,1,1,1,1,0,0,0,1,1,1,1,1,0,0,1,0,0,1,1,0,0,1,0,0,0,1,1,1,0,1,0,0,0,1,0,1,1,0,0,1,1,0,1,0,1,0,0,1,0,0,1,0,1,1,0,1,0,0,1,1,0,1,1,1,0,1,0,0,0,0,1,0,1,0,1,0,0,0,1,1,0,1,0,1,1,1,0,1,0,1,1,1,1,0,1,1,1,0,1,0,1,0,0,1,0,1,1,1,0,0,0,0,1,1,1,0,0,1,0,1,1,1,1,0,0,1,1,1,0,0,0,1,1,1,1,1,1,1,0,1,0,0,0,0,1,1,1,1,0,1,0,1,0,0,1,1,1,0,1,1,1,0,1,1,0,1,0,0,0,1,1,0,0,0,0,0,1,0,0,1,1,0,1,1,0,0,0,1,0,1,0,0,1,0,1,1,1,0,0,0,0,1,1,1,1,0,0,1,1,1,0,1,0,1,1,0,0,1,0,1,0,1,1,1,0,1,1,1,1,0,0,0,0,1,1,0,0,1,1,1,0,1,1,0,1,1,1,0,1,0,1,1,0,1,0,0,0,0,1,1,1,1,1,0,0,1,1,0,1,0,0,1,0,1,0,0,0,0,1,0,1,0,0,0,0,1,1,1,1,1,0,1,0,0,1,1,0,1,0,1,0,0,1,0,1,1,1,0,1,1,0,0,1,0,0,0,0,1,0,0,1,0,0,1,0,1,0,0,1,0,1,0,0,0,1,1,1,1,0,1,1,0,1,1,0,1,0,0,1,0,1,1,1,0,1,0,1,0,0,1,0,0,0,0,1,1,0,0,1,0,0,0,1,1,1,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,1,1,0,1,0,1,0,1,1,0,1,0,1,0,0,0,1,0,1,0,0,1,1,0,1,1,0,1,1,1,1,1,1,1,0,1,1,0,1,0,0,0,0,1,0,0,0,1,1,1,0,1,0,1,0,0,0,1,0,0,1,1,1,0,1,0,1,0,1,0,1,1,1,0,1,1,0,0,0,1,0,0,1,1,1,0,0,1,1,0,1,1,1,0,0,1,0,1,1,0,1,1,0,0,0,1,1,1,1,1,0,0,1,1,0,1,0,1,1,0,0,1,1,1,1,1,0,1,1,0,0,0,1,1,1,1,0,0,0,0,0,1,0,1,0,1,1,0,0,1,0,0,1,0,1,0,0,0,1,0,0,0,0,0,1,1,1,1,1,1,0,1,0,0,1,0,1,0,0,0,1,0,0,0,0,0,0,0,0,0,1,1,1,1,0,0,1,1,1,0,0,1,1,1,0,1,0,1,0,1,1,1,0,1,0,1,1,0,1,1,1,1,1,0,1,1,0,1,1,1,0,0,0,1,1,0,0,0,0,0,1,0,0,0,1,0,0,1,1,1,1,0,0,0,1,0,0,0,1,0,0,0,0,0,0,1,0,0,0,0,1,1,0,1,0,0,1,0,1,0,1,0,1,0,1,1,1,0,1,1,0,1,0,1,1,1,0,0,1,0,0,0,0,0,1,1,1,0,0,0,0,0,1,0,0,0,1,0,0,1,1,1,1,0,1,1,1,1,1,0,1,1,1,1,1,1,0,0,0,0,1,1,0,0,1,0,0,0,1,0,1,0,1,0,1,1,0,1,0,1,1,1,0,0,1,1,1,1,1,1,1,1,0,1,0,1,0,1,0,0,1,1,1,1,1,1,1,1,1,1,0,0,1,0,1,0,0,1,0,1,0,0,0,0,1,1,1,0,1,0,0,0,0,0,0,1,1,0,1,0,0,0,0,0,0,0,1,0,1,0,1,1,1,1,0,0,0,0,0,1,0,0,1,0,1,1,1,1,0,0,0,1,0,0,1,0,1,1,0,1,0,0,1,1,1,1,1,0,1,1,0,1,1,0,1,1,0,0,0,1,1,1,0,1,0,0,1,0,0,1,0,0,1,1,1,0,1,0,0,0,1,0,1,1,1,0,0,0,0,1,1,0,0,0,0,0,1,0,1,1,1,0,0,1,1,1,0,0,1,0,0,1,1,0,0,1,0,1,1,1,0,0,1,1,0,1,0,0,1,1,0,1,1,0,0,1,1,1,1,0,0,1,1,0,1,1,0,0,1,0,1,0,0,1,1,0,1,0,1,1,0,0,1,1,0,1,1,0,0,0,0,1,1,0,1,0,0,1,0,1,0,0,0,0,1,1,0,0,1,1,0,1,0,1,0,0,0,0,1,0,0,0,1,1,1,0,1,0,1,0,1,0,0,0,0,0,0,0,1,1,0,1,0,0,0,1,0,0,1,0,1,0,0,0,1,0,0,0,0,1,0,1,1,0,0,0,0,1,1,1,0,0,1,0,0,0,0,0,1,0,1,0,1,0,1,0,1,1,1,1,1,1,0,0,1,1,0,0,1,0,0,0,1,1,0,0,0,0,0,1,0,1,0,0,1,0,0,0,1,1,1,0,0,0,0,1,0,0,0,0,1,0,0,1,0,1,1,1,0,0,0,1,0,0,1,0,0,0,0,1,0,0,1,1,1,1,1,0,0,1,0,1,1,1,1,1,1,0,1,1");
            Test_TestResultStrContains("Expected 1968 values, found 1967", rs);

            rs = SendWriteMultipleCoils("1", "1", "1968", EFormat.HEXBYTES, "0x91,0x93,0x4f,0xfb,0x5c,0x65,0x67,0x31,0x93,0x97,0x4c,0x02,0xae,0xcf,0x34,0x40,0xf4,0x50,0x09,0xdc,0xc3,0x02,0xb8,0x0e,0x92,0x40,0xa6,0x43,0x40,0x66,0x45,0x36,0xf2,0x5b,0x71,0x1e,0x2c,0xf6,0x3a,0xab,0x4b,0x60,0x5b,0x14,0x3b,0x95,0xfd,0x35,0x20,0x7f,0xf7,0x78,0x40,0x38,0xb1,0x22,0xcb,0x39,0x96,0x46,0xc9,0x5d,0x29,0xc4,0xb3,0xaa,0x39,0x0d,0xc7,0x81,0xeb,0x15,0xab,0x94,0x68,0xcc,0x30,0x23,0x7e,0x49,0x6d,0x34,0xad,0xe5,0xb4,0x3d,0xcc,0x57,0x46,0x89,0x74,0x0f,0x8a,0x95,0xd0,0x9b,0x84,0x47,0x22,0x3d,0xe0,0x99,0x33,0xa1,0xd6,0x25,0x3c,0x32,0x1c,0xd2,0xe6,0x1a,0x8f,0x63,0x04,0x86,0x44,0x1d,0xe9,0x60,0x9d,0x72,0xd0,0x91,0xe2,0x21,0x9d,0x20,0x0e,0x27,0xc1,0x72,0x9c,0x0c,0x9e,0xea,0x2a,0xb6,0xe4,0x48,0x23,0x09,0xdc,0x59,0xd6,0x11,0xfb,0x99,0x0b,0x88,0xb9,0x39,0xcb,0xeb,0x5e,0x1d,0x79,0xb2,0xc9,0x37,0x2a,0x6b,0xe4,0xd3,0x3b,0x0c,0x9b,0x85,0x0f,0x49,0x25,0x60,0x9c,0xf4,0x90,0xa9,0x91,0x67,0x0a,0x01,0xdc,0x9d,0x55,0x93,0x1b,0xb5,0x1a,0x67,0xc5,0xf1,0xa5,0xe5,0xe4,0x1f,0xb4,0xdf,0x0f,0xc6,0xd2,0x18,0x4f,0xec,0x94,0xfa,0xcd,0x66,0xf2,0x83,0x98,0xac,0x56,0xb0,0x59,0x45,0x2c,0xba,0x17,0x50,0x3b,0x66,0xe2,0x58,0xd7,0x33,0x58,0x9c,0x9d,0xd4,0x7e,0x78,0xcb,0x86,0xc2,0x6c,0x8a,0xd7,0x25,0x8d,0xbc,0x23,0xb0,0xb2,0xf6,0x14,0xe0,0xA0");
            Test_TestResultStrContains("success", rs);
            Test_RegisterValuesEqual(40001, "0x9391,0xfb4f,0x655c,0x3167,0x9793,0x024c,0xcfae,0x4034,0x50f4,0xdc09,0x02c3,0x0eb8,0x4092,0x43a6,0x6640,0x3645,0x5bf2,0x1e71,0xf62c,0xab3a,0x604b,0x145b,0x953b,0x35fd,0x7f20,0x78f7,0x3840,0x22b1,0x39cb,0x4696,0x5dc9,0xc429,0xaab3,0x0d39,0x81c7,0x15eb,0x94ab,0xcc68,0x2330,0x497e,0x346d,0xe5ad,0x3db4,0x57cc,0x8946,0x0f74,0x958a,0x9bd0,0x4784,0x3d22,0x99e0,0xa133,0x25d6,0x323c,0xd21c,0x1ae6,0x638f,0x8604,0x1d44,0x60e9,0x729d,0x91d0,0x21e2,0x209d,0x270e,0x72c1,0x0c9c,0xea9e,0xb62a,0x48e4,0x0923,0x59dc,0x11d6,0x99fb,0x880b,0x39b9,0xebcb,0x1d5e,0xb279,0x37c9,0x6b2a,0xd3e4,0x0c3b,0x859b,0x490f,0x6025,0xf49c,0xa990,0x6791,0x010a,0x9ddc,0x9355,0xb51b,0x671a,0xf1c5,0xe5a5,0x1fe4,0xdfb4,0xc60f,0x18d2,0xec4f,0xfa94,0x66cd,0x83f2,0xac98,0xb056,0x4559,0xba2c,0x5017,0x663b,0x58e2,0x33d7,0x9c58,0xd49d,0x787e,0x86cb,0x6cc2,0xd78a,0x8d25,0x23bc,0xb2b0,0x14f6,0xa0e0");

            rs = SendWriteMultipleCoils("1", "1", "1968", EFormat.HEXBYTES, "0x91,0x93,0x4f,0xfb,0x5c,0x65,0x67,0x31,0x93,0x97,0x4c,0x02,0xae,0xcf,0x34,0x40,0xf4,0x50,0x09,0xdc,0xc3,0x02,0xb8,0x0e,0x92,0x40,0xa6,0x43,0x40,0x66,0x45,0x36,0xf2,0x5b,0x71,0x1e,0x2c,0xf6,0x3a,0xab,0x4b,0x60,0x5b,0x14,0x3b,0x95,0xfd,0x35,0x20,0x7f,0xf7,0x78,0x40,0x38,0xb1,0x22,0xcb,0x39,0x96,0x46,0xc9,0x5d,0x29,0xc4,0xb3,0xaa,0x39,0x0d,0xc7,0x81,0xeb,0x15,0xab,0x94,0x68,0xcc,0x30,0x23,0x7e,0x49,0x6d,0x34,0xad,0xe5,0xb4,0x3d,0xcc,0x57,0x46,0x89,0x74,0x0f,0x8a,0x95,0xd0,0x9b,0x84,0x47,0x22,0x3d,0xe0,0x99,0x33,0xa1,0xd6,0x25,0x3c,0x32,0x1c,0xd2,0xe6,0x1a,0x8f,0x63,0x04,0x86,0x44,0x1d,0xe9,0x60,0x9d,0x72,0xd0,0x91,0xe2,0x21,0x9d,0x20,0x0e,0x27,0xc1,0x72,0x9c,0x0c,0x9e,0xea,0x2a,0xb6,0xe4,0x48,0x23,0x09,0xdc,0x59,0xd6,0x11,0xfb,0x99,0x0b,0x88,0xb9,0x39,0xcb,0xeb,0x5e,0x1d,0x79,0xb2,0xc9,0x37,0x2a,0x6b,0xe4,0xd3,0x3b,0x0c,0x9b,0x85,0x0f,0x49,0x25,0x60,0x9c,0xf4,0x90,0xa9,0x91,0x67,0x0a,0x01,0xdc,0x9d,0x55,0x93,0x1b,0xb5,0x1a,0x67,0xc5,0xf1,0xa5,0xe5,0xe4,0x1f,0xb4,0xdf,0x0f,0xc6,0xd2,0x18,0x4f,0xec,0x94,0xfa,0xcd,0x66,0xf2,0x83,0x98,0xac,0x56,0xb0,0x59,0x45,0x2c,0xba,0x17,0x50,0x3b,0x66,0xe2,0x58,0xd7,0x33,0x58,0x9c,0x9d,0xd4,0x7e,0x78,0xcb,0x86,0xc2,0x6c,0x8a,0xd7,0x25,0x8d,0xbc,0x23,0xb0,0xb2,0xf6,0x14,0xe0");
            Test_TestResultStrContains("Expected 246 values, found 245", rs);

            rs = SendWriteMultipleCoils("1", "9999", "1", EFormat.INT16, "0");
            Test_TestResultStrContains("invalid format", rs);

            rs = SendWriteMultipleCoils("1", "9999", "1", EFormat.UINT16, "0");
            Test_TestResultStrContains("invalid format", rs);

            rs = SendWriteMultipleCoils("1", "9999", "1", EFormat.HEX16, "0");
            Test_TestResultStrContains("invalid format", rs);

            rs = SendWriteMultipleCoils("1", "9999", "1", EFormat.INT32, "0");
            Test_TestResultStrContains("invalid format", rs);

            rs = SendWriteMultipleCoils("1", "9999", "1", EFormat.UINT32, "0");
            Test_TestResultStrContains("invalid format", rs);

            rs = SendWriteMultipleCoils("1", "9999", "1", EFormat.HEX32, "0");
            Test_TestResultStrContains("invalid format", rs);

        }

        private void Test_SendCustomQuery()
        {
            string rs;

            rs = SendWriteMultipleHoldingreg("1", "40011", "6", EFormat.INT16, "11001 , 12002, 13003, 14004, 15005, 16006");
            Test_TestResultStrContains("success", rs);

            rs = SendCustomQuery("", "", "", EFormat.HEXBYTES, "0x03, 0x01, 0x02, 0x00, 0x006");
            Test_TestResultStrContains("invalid slaveid", rs);

            rs = SendCustomQuery("1", "", "", EFormat.HEXBYTES, "0x03, 0x00, 0x0A, 0x00, 0x06");
            Test_TestResultStrEquals("0x03, 0x0C, 0x2A, 0xF9, 0x2E, 0xE2, 0x32, 0xCB, 0x36, 0xB4, 0x3A, 0x9D, 0x3E, 0x86", rs);

            rs = SendCustomQuery("1", "", "", EFormat.HEXBYTES, " 0x03 , 0  , 0x00A, 0x0000, 0x06,");
            Test_TestResultStrEquals("0x03, 0x0C, 0x2A, 0xF9, 0x2E, 0xE2, 0x32, 0xCB, 0x36, 0xB4, 0x3A, 0x9D, 0x3E, 0x86", rs);

            rs = SendCustomQuery("1", "", "", EFormat.HEXBYTES, " 0x03 , 0  , 0x00A, 0x0000, 0x06,");
            Test_TestResultStrEquals("0x03, 0x0C, 0x2A, 0xF9, 0x2E, 0xE2, 0x32, 0xCB, 0x36, 0xB4, 0x3A, 0x9D, 0x3E, 0x86", rs);

            rs = SendCustomQuery("1", "", "", EFormat.INT16, "0x03, 0x00, 0x0A, 0x00, 0x06");
            Test_TestResultStrContains("invalid format", rs);

            rs = SendCustomQuery("1", "", "", EFormat.UINT16, "0x03, 0x00, 0x0A, 0x00, 0x06");
            Test_TestResultStrContains("invalid format", rs);

            rs = SendCustomQuery("1", "", "", EFormat.HEX16, "0x03, 0x00, 0x0A, 0x00, 0x06");
            Test_TestResultStrContains("invalid format", rs);

            rs = SendCustomQuery("1", "", "", EFormat.INT32, "0x03, 0x00, 0x0A, 0x00, 0x06");
            Test_TestResultStrContains("invalid format", rs);

            rs = SendCustomQuery("1", "", "", EFormat.UINT32, "0x03, 0x00, 0x0A, 0x00, 0x06");
            Test_TestResultStrContains("invalid format", rs);

            rs = SendCustomQuery("1", "", "", EFormat.HEX32, "0x03, 0x00, 0x0A, 0x00, 0x06");
            Test_TestResultStrContains("invalid format", rs);

        }

        private void Test_TestResultStrEquals(string exp, string actual)
        {
            // equal?
            if (exp == actual)
            {
                // OK
            }
            else
            {
                throw new ArgumentException(String.Format("TEST FAILED: Expected \"{0}\", actual \"{1}\"", exp, actual));
            }
        }

        private void Test_TestResultStrContains(string exp, string actual)
        {
            // string in string? case insensitive
            if (actual.IndexOf(exp, StringComparison.OrdinalIgnoreCase) >= 0)
            {
                // OK
            }
            else
            {
                throw new ArgumentException(String.Format("TEST FAILED: Expected \"{0}\", actual \"{1}\"", exp, actual));
            }
        }

        private void Test_HoldingRegisterValueEqual(UInt32 register, Int16 expectedValue)
        {
            UInt16 regOffset;
            if (!mParent.TryHoldingregisterGetOffset(register, out regOffset))
            {
                throw new ArgumentException("TEST FAILED");
            }
            Int16[] aValues = null;
            ModbusProtocol.Result result = mModbus.MifReadHoldingRegisters(1, regOffset, 1, out aValues);
            if (result == ModbusProtocol.Result.SUCCESS)
            {
                if (aValues[0] != expectedValue)
                {
                    throw new ArgumentException("TEST FAILED");
                }
            }
            else
            {
                throw new ArgumentException("TEST FAILED");
            }
        }

        private void Test_HoldingRegisterValueEqualUInt16(UInt32 register, UInt16 expectedValue)
        {
            Test_HoldingRegisterValueEqual(register, (Int16)expectedValue);
        }

        private void Test_CoilValuesEqual(UInt32 register, string expectedValuesStr)
        {
            string[] aRefValues = expectedValuesStr.Split(',');
            // we allow 1 comma at the end, that results in an empty data field, we allow and ignore that
            if ((aRefValues.Length >= 1) && (aRefValues[aRefValues.Length - 1].Trim() == ""))
            {
                Array.Resize(ref aRefValues, aRefValues.Length - 1);
            }

            UInt16 nrOfCoils = (UInt16)aRefValues.Length;
            UInt16 regOffset;
            if (!mParent.TryCoilGetOffset(register, out regOffset))
            {
                throw new ArgumentException("TEST FAILED");
            }
            byte[] aBytes = null;
            ModbusProtocol.Result result = mModbus.MifReadCoils(1, regOffset, nrOfCoils, out aBytes);
            if (result == ModbusProtocol.Result.SUCCESS)
            {
                for (int i = 0; i < nrOfCoils; ++i)
                {
                    byte bit = (byte)((aBytes[i / 8] >> (i % 8)) & 1);
                    string readValStr = ((char)('0' + bit)).ToString();
                    if (readValStr != aRefValues[i])
                    {
                        throw new ArgumentException("TEST FAILED");
                    }
                }
            }
            else
            {
                throw new ArgumentException("TEST FAILED");
            }
        }

        private void Test_RegisterValuesEqual(UInt32 register, string expectedValuesStr)
        {
            string[] aRefValues = expectedValuesStr.Split(',');
            // we allow 1 comma at the end, that results in an empty data field, we allow and ignore that
            if ((aRefValues.Length >= 1) && (aRefValues[aRefValues.Length - 1].Trim() == ""))
            {
                Array.Resize(ref aRefValues, aRefValues.Length - 1);
            }

            UInt16 nrOfRegisters = (UInt16)aRefValues.Length;
            UInt16 regOffset;
            if (!mParent.TryHoldingregisterGetOffset(register, out regOffset))
            {
                throw new ArgumentException("TEST FAILED");
            }

            Int16[] aReadValues = null;
            ModbusProtocol.Result result = mModbus.MifReadHoldingRegisters(1, regOffset, nrOfRegisters, out aReadValues);
            if (result == ModbusProtocol.Result.SUCCESS)
            {
                for (int i = 0; i < nrOfRegisters; ++i)
                {
                    Int16 expectedValue = Convert.ToInt16(aRefValues[i], 16);
                    if (aReadValues[i] != expectedValue)
                    {
                        throw new ArgumentException("TEST FAILED");
                    }
                }
            }
            else
            {
                throw new ArgumentException("TEST FAILED");
            }
        }

        
#endif

#endregion

        }
}
