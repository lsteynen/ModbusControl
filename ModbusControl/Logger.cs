using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace LST.Helpers
{
    public partial class Logger : Form
    {
        private bool mEnable = false;
        private int mMaxLines = 1000;
        private int mLogAdded;
        private bool mIsPauseView;
        
        public enum LogType
        {
            Error,
            Warning,
            Info,
            Trace,
            Read,
            Write,
            Data,
            Decode,
        };

        public class LogEntry
        {
            private string mTime;
            public string Time
            {
                get { return mTime; }
                //set { mDateTime = value; }
            }

            private string mType;
            public string Type
            {
                get { return mType; }
                //set { mType = value; }
            }

            private string mText;
            public string Text
            {
                get { return mText; }
                //set { mText = value; }
            }

            public LogEntry(DateTime DateTime, LogType Type, string Text)
            {
                this.mTime = DateTime.ToString("HH:mm:ss.fff");
                this.mType = Type.ToString();
                this.mText = Text;
            }
        }

        // here's where logs are stored
        List<LogEntry> LogList = new List<LogEntry>();
        private DevAge.ComponentModel.BoundList<LogEntry> boundLogList;

        // lock for making things threadsafe
        private readonly object LogListLock = new object();

        #region constructor

        public Logger()
        {
            InitializeComponent();

            RefreshView();
        }

        #endregion

        #region properties

        public bool Enable
        {
            get
            {
                return mEnable;
            }
            set
            {
                mEnable = value;
                timerView.Enabled = mEnable;

                buttonRecord.Enabled = !mEnable;
                buttonStop.Enabled = mEnable;
                buttonFirst.Enabled = !mEnable;
                buttonLast.Enabled = !mEnable;

                // repaint to show the "Stopped" state
                if (mEnable == false)
                {
                    RefreshView();
                }
            }
        }

        public int CapacityLines
        {
            get
            {
                return mMaxLines;
            }
            set
            {
                if (value < 1000) value = 1000;
                AdjustLoggerSize(value);
                mMaxLines = value;
                RefreshView();
            }
        }

        public int ViewUpdateInterval
        {
            get
            {
                return timerView.Interval;
            }
            set
            {
                timerView.Interval = value;
            }
        }

        public bool LogErrors
        {
            get
            {
                return mEnable && logFilter.LogErrors;
            }
        }

        public bool LogWarnings
        {
            get
            {
                return mEnable && logFilter.LogWarnings;
            }
        }

        public bool LogInfo
        {
            get
            {
                return mEnable && logFilter.LogInfo;
            }
        }

        public bool LogTrace
        {
            get
            {
                return mEnable && logFilter.LogTrace;
            }
        }

        public bool LogReads
        {
            get
            {
                return mEnable && logFilter.LogReads;
            }
        }

        public bool LogWrites
        {
            get
            {
                return mEnable && logFilter.LogWrites;
            }
        }

        public bool LogBridgeInfo
        {
            get
            {
                return mEnable && logFilter.LogBridgeInfo;
            }
        }

        public bool LogBridgeData
        {
            get
            {
                return mEnable && logFilter.LogBridgeData;
            }
        }

        public bool LogBridgeDecode
        {
            get
            {
                return mEnable && logFilter.LogBridgeDecode;
            }
        }

        public LogFilter TheLogFilter
        {
            get
            {
                return logFilter;
            }
        }

        #endregion

        private void Form1_Load(object sender, EventArgs e)
        {
            boundLogList = new DevAge.ComponentModel.BoundList<LogEntry>(LogList);
            boundLogList.AllowDelete = false;
            boundLogList.AllowEdit = false;
            boundLogList.AllowNew = false;
            boundLogList.AllowSort = true;

            Loggrid.Columns.Add("Time", "Time", typeof(string)).Width = 76;
            Loggrid.Columns.Add("Type", "Type", typeof(string)).Width = 50;
            Loggrid.Columns.Add("Text", "Text", typeof(string));

            Loggrid.Columns[0].AutoSizeMode = SourceGrid.AutoSizeMode.None;
            Loggrid.Columns[1].AutoSizeMode = SourceGrid.AutoSizeMode.None;
            Loggrid.Columns[2].AutoSizeMode = SourceGrid.AutoSizeMode.EnableAutoSize | SourceGrid.AutoSizeMode.EnableStretch;
            Loggrid.AutoStretchColumnsToFitWidth = true;
            Loggrid.Columns.StretchToFit();

            Loggrid.DataSource = boundLogList;
            // Disable sort AFTER assigning datasource
            Loggrid.EnableSort = false;

            timerView.Interval = 200;
            timerView.Enabled = false;
        }

        private void AdjustLoggerSize( int NewSize)
        {
            if( NewSize < mMaxLines)
            {
                // must eliminate oldest entries?
                // threadsafe
                lock (LogListLock)
                {
                    if (NewSize < LogList.Count)
                    {
                        LogList.RemoveRange(0, LogList.Count - NewSize);
                    }
                }

                // set changed flag
                mLogAdded = 1;
            }
            else if( NewSize > mMaxLines)
            {
                // simply set new size
                mMaxLines = NewSize;
            }
        }


#if false
        private void listView1_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            // threadsafe
            LogEntry Log;
            lock (LogListLock)
            {
                Log = LogList[e.ItemIndex];
            }

            string[] sa = new string[3];

            sa[0] = Log.DateTime.ToString("HH:mm:ss.fff");
            sa[1] = Log.Type.ToString();
            sa[2] = Log.Text;

            e.Item = new ListViewItem(sa);
        }
#endif

        private void timerView_Tick(object sender, EventArgs e)
        {
            RefreshView();
        }

        private void RefreshView()
        {
            // threadsafe
            if (Interlocked.Exchange(ref mLogAdded, 0) == 1)
            {
                // if view not paused
                if (mIsPauseView == false)
                {
                	// threadsafe
                	lock (LogListLock)
                	{
                    	//Loggrid.SuspendLayout();
                    	Loggrid.RecalcCustomScrollBars();
                    	Loggrid.ShowCell(new SourceGrid.Position(Loggrid.Rows.Count - 1, 0), true);
                    	Loggrid.Refresh();
                    	//Loggrid.ResumeLayout();
                	}
            	}
                else
                {
                    // threadsafe
                    lock (LogListLock)
                    {
                        Loggrid.RecalcCustomScrollBars();
                        Loggrid.Refresh();
                    }
                }
            }

            // update status line
            string StatusStr = "?";
            if( mEnable)
            {
			    if( mIsPauseView)
                {
                    StatusStr = "Paused";
                }
                else
                {
                    StatusStr = "Enabled";
                }
            }
            else
            {
                StatusStr = "Stopped";
            }
            toolStripStatusLabel2.Text = StatusStr + " - " 
                               + LogList.Count.ToString() + " / "
                               + mMaxLines.ToString();
        }

        private void buttonPause_Click(object sender, EventArgs e)
        {
            // pause at current position
            mIsPauseView = true;
        }

        private void buttonRecord_Click(object sender, EventArgs e)
        {
            // Enable logging, via the property
            Enable = true;

            // show last (newest) log entry
            if (Loggrid.Rows.Count > 0)
            {
                // threadsafe
                lock (LogListLock)
                {
                    //Loggrid.SuspendLayout();
                    Loggrid.RecalcCustomScrollBars();
                    Loggrid.ShowCell(new SourceGrid.Position(Loggrid.Rows.Count - 1, 0), true);
                    Loggrid.Refresh();
                    //Loggrid.ResumeLayout();
                }
            }
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            // Disable logging, via the property
            Enable = false;
        }

        private void buttonFirst_Click(object sender, EventArgs e)
        {
            // Show first (oldest) log entries
            if (Loggrid.Rows.Count > 0)
            {
                // mIsPauseView = true;

                //Loggrid.SuspendLayout();
                Loggrid.ShowCell(new SourceGrid.Position(1, 1), true);
                Loggrid.RecalcCustomScrollBars();
                Loggrid.Refresh();
                //Loggrid.ResumeLayout();
            }
        }

        private void buttonLast_Click(object sender, EventArgs e)
        {
            // Show last (newest) log entries
            if (Loggrid.Rows.Count > 0)
            {
                // mIsPauseView = true;

                // threadsafe
                lock (LogListLock)
                {
                    //Loggrid.SuspendLayout();
                    Loggrid.RecalcCustomScrollBars();
                    Loggrid.ShowCell(new SourceGrid.Position(Loggrid.Rows.Count - 1, 0), true);
                    Loggrid.Refresh();
                    //Loggrid.ResumeLayout();
                }
            }
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            // threadsafe
            lock (LogListLock)
            {
                // clear list
                LogList.Clear();

                // update view
                Loggrid.Refresh();
            }
        }

        private void buttonCopyToClipboard_Click(object sender, EventArgs e)
        {
            // copy logger content to clipboard

            StringBuilder builder = new StringBuilder();

            // Append to StringBuilder.
            lock (LogListLock)
            {
                for (int i = 0; i < LogList.Count; ++i)
                {
                    LogEntry Log = LogList[i];

                    builder.AppendLine(//Log.DateTime.ToString("HH:mm:ss.fff")
                                       Log.Time
                                      + ";" + Log.Type.ToString()
                                      + ";" + Log.Text);
                }
            }

            if (builder.Length > 0)
            {
                System.Windows.Forms.Clipboard.SetText(builder.ToString());
            }
        }

        // Do not close, hide
        private void Logger_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true; // this cancels the close event.
        }

        // enter a log
        public void Log(LogType Type, string Text)
        {
            if (mEnable)
            {
                LogEntry Log = new LogEntry(DateTime.Now, Type, Text);

                // threadsafe
                lock (LogListLock)
                {
                    if (LogList.Count >= mMaxLines)
                    {
                        LogList.RemoveAt(0);
                        if (LogList.Count >= mMaxLines)
                        {
                            LogList.RemoveAt(0);
                        }
                    }

                    LogList.Add(Log);
                }

                mLogAdded = 1;
            }
        }

        // enter a log, 2 lines
        public void Log(LogType Type, string Text1, string Text2)
        {
            if (mEnable)
            {
                LogEntry Log1 = new LogEntry(DateTime.Now, Type, Text1);
                LogEntry Log2 = new LogEntry(DateTime.Now, Type, Text2);

                // threadsafe
                lock (LogListLock)
                {
                    if (LogList.Count >= mMaxLines)
                    {
                        LogList.RemoveAt(0);
                        LogList.RemoveAt(0);
                        if (LogList.Count >= mMaxLines)
                        {
                            LogList.RemoveAt(0);
                        }
                    }

                    LogList.Add(Log1);
                    LogList.Add(Log2);
                }

                mLogAdded = 1;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoggerSettings form = new LoggerSettings();

            form.CapacityLines = CapacityLines;

            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                // Save new settings
                CapacityLines = form.CapacityLines;
            }
        }

    }
}