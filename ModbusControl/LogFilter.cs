using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using LST.Controls;

namespace LST.Helpers
{
    public partial class LogFilter : DropDownControl
    {
        public LogFilter()
        {
            InitializeComponent();
            InitializeDropDown(panel1);
            this.Text ="What to log";
        }

        public bool LogErrors
        {
            get { return checkboxLogErrors.Checked; }
            set { checkboxLogErrors.Checked = value; }
        }

        public bool LogWarnings
        {
            get { return checkboxLogWarnings.Checked; }
            set { checkboxLogWarnings.Checked = value; }
        }

        public bool LogInfo
        {
            get { return checkboxLogInfo.Checked; }
            set { checkboxLogInfo.Checked = value; }
        }

        public bool LogTrace
        {
            get { return checkboxLogTrace.Checked; }
            set { checkboxLogTrace.Checked = value; }
        }

        public bool LogReads
        {
            get { return checkboxLogReads.Checked; }
            set { checkboxLogReads.Checked = value; }
        }

        public bool LogWrites
        {
            get { return checkboxLogWrites.Checked; }
            set { checkboxLogWrites.Checked = value; }
        }

        public bool LogBridgeInfo
        {
            get { return checkboxBridgeInfo.Checked; }
            set { checkboxBridgeInfo.Checked = value; }
        }

        public bool LogBridgeData
        {
            get { return checkboxBridgeData.Checked; }
            set { checkboxBridgeData.Checked = value; }
        }

        public bool LogBridgeDecode
        {
            get { return checkboxLogBridgeDecode.Checked; }
            set { checkboxLogBridgeDecode.Checked = value; }
        }

    }
}
