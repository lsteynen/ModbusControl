using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;

/* Usage:
 * 
 * Enable:  logging on/off
 *          
 * Clear:   clear logger memory
 * 
 * Auto-scroll on: resume scrolling, always show newest logs
 *             off: stop scrolling, stay at current position
 *             
 * Top:     stop scrolling, jump to oldest
 * 
 * Bottom:  jump to end, auto-scroll on
*/

namespace LST.Helpers
{
    public partial class Logger : Form
    {
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

        // 1 log record
        public class LogEntry
        {
            public string Time;
            public string Type;
            public string Text;
        }

        private bool mEnable = false;
        private int mCapacity = 1000;
        private bool mAutoScroll;
        private int mViewUpdateInterval = 150;  // ms
        private StringBuilder mStatusStringBuilder = new StringBuilder(100);
        
        // logs storage
        private readonly List<LogEntry> logQueue = new List<LogEntry>();
        private readonly object logLock = new object();     // lock for making things threadsafe
        private int logDataChanged = 0;                    // flag that show if logdata has changed
        
        #region constructor

        public Logger()
        {
            InitializeComponent();

            mAutoScroll = chkAutoScroll.Checked;

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

                // repaint if timer doesn't
                if (mEnable == false)
                {
                    RefreshView();
                }
            }
        }

        public int Capacity
        {
            get
            {
                return mCapacity;
            }
            set
            {
                // minimum
                if (value < 1000)
                {
                    value = 1000;
                }

                if (value != mCapacity)
                {
                    AdjustLoggerCapacity(value);

                    // repaint if timer doesn't
                    if (mEnable == false)
                    {
                        RefreshView();
                    }
                }
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
            timerView.Interval = mViewUpdateInterval;
            timerView.Enabled = false;
        }

        private void AdjustLoggerCapacity( int newCapacity)
        {
            if (newCapacity < mCapacity)
            {
                // must eliminate oldest entries?
                // threadsafe
                lock (logLock)
                {
                    if (newCapacity < logQueue.Count)
                    {
                        logQueue.RemoveRange(0, logQueue.Count - newCapacity);
                    }
                    mCapacity = newCapacity;
                }

                // set changed flag
                logDataChanged = 1;
            }
            else if (newCapacity > mCapacity)
            {
                // simply set new size
                mCapacity = newCapacity;
            }
        }

        // Do not close, hide
        private void Logger_FormClosing(object sender, FormClosingEventArgs e)
        {
            // hide
            this.Hide();
            // cancel this close event
            e.Cancel = true; 
        }


        private void timerView_Tick(object sender, EventArgs e)
        {
            timerView.Enabled = false;

            RefreshView();

            timerView.Enabled = true;
        }

        private void RefreshView()
        {
            // if logQueuechanged: do grid refresh
            // threadsafe exchange
            if (Interlocked.Exchange(ref logDataChanged, 0) == 1)
            {
                logGridView.RowCount = logQueue.Count;
                
                // if auto-scrolling
                if (mAutoScroll && (logGridView.RowCount > 0))
                {
                    try
                    {
                        int last = logGridView.RowCount - 1;
                        logGridView.FirstDisplayedScrollingRowIndex = last;
                    }
                    catch
                    {
                        // ignore
                    }
                }

                logGridView.Invalidate();
            }

            // update status line
            mStatusStringBuilder.Clear();
            if( mEnable)
            {
                mStatusStringBuilder.Append("Enabled");
            }
            else
            {
                mStatusStringBuilder.Append("Stopped");
            }

            mStatusStringBuilder.Append(" - ");
            mStatusStringBuilder.Append(logQueue.Count.ToString());
            mStatusStringBuilder.Append("/");
            mStatusStringBuilder.Append(mCapacity.ToString());

            if (mAutoScroll)
            {
                mStatusStringBuilder.Append(" - Auto-scroll");
            }

            string s = mStatusStringBuilder.ToString();
            if (s != toolStripStatusLabel2.Text)
            {
                toolStripStatusLabel2.Text = s;
            }
        }

        private void checkboxEnable_CheckedChanged(object sender, EventArgs e)
        {
            // Enable/disable logging, use the property for side effects
            Enable = checkboxEnable.Checked;
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            lock (logLock)
            {
                // clear list
                logQueue.Clear();
                logDataChanged = 1;
            }

            // repaint if timer doesn't
            if (mEnable == false)
            {
                RefreshView();
            }
        }

        /// <summary>
        /// Auto-scrolling enabled/disable
        /// </summary>
        private void chkAutoScroll_CheckedChanged(object sender, EventArgs e)
        {
            mAutoScroll = chkAutoScroll.Checked;

            if (mAutoScroll)
            {
                // show newest (last) log entry
                if (logQueue.Count > 0)
                {
                    try
                    {
                        int last = logGridView.RowCount - 1;
                        logGridView.FirstDisplayedScrollingRowIndex = last;
                    }
                    catch
                    {
                        // ignore
                    }
                }
            }

            // repaint if timer doesn't
            if (mEnable == false)
            {
                RefreshView();
            }
        }

        private void buttonTop_Click(object sender, EventArgs e)
        {
            // Stop auto-scroll
            chkAutoScroll.Checked = false;

            // Show top (oldest) log entries
            if (logGridView.RowCount > 0)
            {
                try
                {
                    logGridView.FirstDisplayedScrollingRowIndex = 0;
                }
                catch
                {
                    // ignore
                }
            }

            // chkAutoScroll change forces RefreshView
            //RefreshView();
        }

        private void buttonBottom_Click(object sender, EventArgs e)
        {
            // Resume auto-scrolling
            chkAutoScroll.Checked = true;

            if (mAutoScroll && (logGridView.RowCount > 0))
            {
                try
                {
                    int last = logGridView.RowCount - 1;
                    logGridView.FirstDisplayedScrollingRowIndex = last;
                }
                catch
                {
                    // ignore
                }
            }

            // chkAutoScroll change forces RefreshView
            //RefreshView();
        }

        /// <summary>
        /// User clicked in the grid
        /// Using CellMouseDown because CellClick is unreliable, probably because of auto-scrolling
        /// </summary>
        private void logGridView_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            // accept left mouse button
            // accept rows, not column headers
            if( (e.RowIndex >= 0)
                 && (e.Button == MouseButtons.Left))
            {
                // pause at current position
                chkAutoScroll.Checked = false;

                // chkAutoScroll change forces RefreshView
                //RefreshView();
            }
        }

        // enter a log, 1 line
        public void Log(LogType Type, string Text)
        {
            if (mEnable)
            {
                LogEntry newLog = new LogEntry();
                newLog.Time = DateTime.Now.ToString("HH:mm:ss.fff");
                newLog.Type = Type.ToString();
                newLog.Text = Text;

                lock (logLock)
                {
                    logQueue.Add(newLog);
                }

                // remove oldest logs if needed               
                lock (logLock)
                {
                    int removeCount = logQueue.Count - mCapacity;
                    if (removeCount > 0)
                    {
                        logQueue.RemoveRange(0, removeCount);
                    }
                }

                logDataChanged = 1;
            }
        }

        // enter a log, 2 lines, keep together
        public void Log(LogType Type, string Text1, string Text2)
        {
            if (mEnable)
            {
                LogEntry newLog1 = new LogEntry();
                newLog1.Time = DateTime.Now.ToString("HH:mm:ss.fff");
                newLog1.Type = Type.ToString();
                newLog1.Text = Text;

                LogEntry newLog2 = new LogEntry();
                newLog2.Time = newLog1.Time;
                newLog2.Type = newLog1.Type;
                newLog2.Text = Text;

                // ensure these logs stay together
                lock (logLock)
                {
                    logQueue.Add(newLog1);
                    logQueue.Add(newLog2);
                }

                // remove oldest logs if needed               
                lock (logLock)
                {
                    int removeCount = logQueue.Count - mCapacity;
                    if (removeCount > 0)
                    {
                        logQueue.RemoveRange(0, removeCount);
                    }
                }

                logDataChanged = 1;
            }
        }

        private void buttonCopyToClipboard_Click(object sender, EventArgs e)
        {
            // copy logger content to clipboard

            if (logQueue.Count > 0)
            {
                // take a snapshot of the log
                // to minimize lock time
                List<LogEntry> snapshot;
                lock(logLock)
                {
                    snapshot = new List<LogEntry>(logQueue);
                }

                // build the text
                
                int initalCapacity = snapshot.Count * 70;
                StringBuilder sb = new StringBuilder(initalCapacity);
                string sepa = ";";

                // header
                sb.AppendLine("Time" + sepa + "Type" + sepa + "Text");
                sb.AppendLine("#Modbuscontrol logging export - " + DateTime.Now.ToString());

                // rows
                foreach (LogEntry log in snapshot)
                {
                    sb.AppendLine( log.Time
                                        + sepa + log.Type.ToString()
                                        + sepa + log.Text);
                }

                // copy string to clipboard
                System.Windows.Forms.Clipboard.SetText(sb.ToString());
            }
        }

        private void buttonSaveToFile_Click(object sender, EventArgs e)
        {
            if (logQueue.Count > 0)
            {
                if (SaveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    SaveLogbufferToCsvFile(SaveFileDialog1.FileName);
                }
            }
        }

        /// <summary>
        /// Save logbuffer to a file, CSV format
        /// </summary>
        private void SaveLogbufferToCsvFile(string filename)
        {
            if (logQueue.Count > 0)
            {
                // take a snapshot of the log
                // to minimize lock time
                List<LogEntry> snapshot;
                lock (logLock)
                {
                    snapshot = new List<LogEntry>(logQueue);
                }

                CsvWriter mCsvWriter;

                // open file to write to
                try
                {
                    // open output file
                    // Use Windows's current ANSI codepage encoding and ';' separator, for easy import in Excel

                    mCsvWriter = new CsvWriter(filename, System.Text.Encoding.Default);
                    // settings
                    mCsvWriter.CommentHeader = "#";
                    mCsvWriter.Separator = ';';
                    // always put quotes around the text field
                    mCsvWriter.QuoteSelectedFields = new bool[] { false, false, true }; // we choose to always quote the description field
                }
                catch (Exception ex)
                {
                    // open failed
                    string msg = ex.Message + Environment.NewLine + filename;
                    MessageBox.Show(msg, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // ensure the file is closed
                try
                {
                    // export header 
                    mCsvWriter.WriteRow(new string[] { "Time", "Type", "Text" }, false);
                    // comment
                    mCsvWriter.WriteComment("Modbuscontrol logging export - " + DateTime.Now.ToString());
                    
                    // export rows
                    string[] fields;
                    foreach (LogEntry log in snapshot)
                    {
                        fields = new string[] { log.Time.ToString(), log.Type.ToString(), log.Text };
                        mCsvWriter.WriteRow(fields);
                    }
                }
                finally
                {
                    mCsvWriter.Close();
                }
            }            
        }

        private void dataGridView1_CellValueNeeded(object sender, DataGridViewCellValueEventArgs e)
        {
            if ((e.RowIndex >= 0) && (e.RowIndex < logQueue.Count))
            {
                LogEntry log = logQueue[e.RowIndex];
                switch (e.ColumnIndex)
                {
                    case 0:
                        e.Value = log.Time;
                        break;
                    case 1:
                        e.Value = log.Type;
                        break;
                    case 2:
                        e.Value = log.Text;
                        break;
                }                 
            }
        }

        
    }
}