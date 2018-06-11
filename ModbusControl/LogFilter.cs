using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UControls;

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
            get 
            {
                return checkboxLogErrors.Checked;
            }
        }

        public bool LogWarnings
        {
            get
            {
                return checkboxLogWarnings.Checked;
            }
        }

        public bool LogInfo
        {
            get
            {
                return checkboxLogInfo.Checked;
            }
        }

        public bool LogTrace
        {
            get
            {
                return checkboxLogTrace.Checked;
            }
        }

        public bool LogReads
        {
            get
            {
                return checkboxLogReads.Checked;
            }
        }

        public bool LogWrites
        {
            get
            {
                return checkboxLogWrites.Checked;
            }
        }

        public bool LogBridgeInfo
        {
            get
            {
                return checkboxBridgeInfo.Checked;
            }
        }

        public bool LogBridgeData
        {
            get
            {
                return checkboxBridgeData.Checked;
            }
        }

        public bool LogBridgeDecode
        {
            get
            {
                return checkboxLogBridgeDecode.Checked;
            }
        }

    }
}
