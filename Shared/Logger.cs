using System;
using System.Globalization;
using System.IO;

namespace LST.Helpers
{
    public class Logger
    {
        // properties
        public bool Append = false;
        public int AddTimestampsMode = 0;             // 1=2023-11-09T09:19:00.123Z:
        public bool AddHeaderLine = false;
        public string HeaderLineText = null;
        public bool AddFooterLine = false;
        public string FooterLineText = null;

        // private
        private string filePath;
        StreamWriter writer = null;
        private readonly object lockObj = new object();

        public void Open(string file)
        {
            filePath = file;

            if (writer != null)
            {
                writer.Close();
                writer = null;
            }

            // open, optional append
            writer = new StreamWriter(filePath, Append);

            // add begin separation line, if file already has data
            if ((AddHeaderLine) && (HeaderLineText != null))
            {
                long curlen = writer.BaseStream.Position;
                if ((curlen > 0))
                {
                    lock (lockObj)
                    {
                        writer.WriteLine(HeaderLineText);
                        writer.Flush();
                    }
                }
            }

#if false
// experiment to write to shared open file
// does not work
            // open for write in shared mode (can write while Excel has this file open)
            FileStream fs = new FileStream(filePath, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
            writer = new StreamWriter(fs);
#endif
        }

        public void Log(string message)
        {
            if (writer == null)
            {
                throw new IOException("Logfile not open.");
            }

            lock (lockObj)
            {
                string timestr = "";
                if (AddTimestampsMode == 1)
                {
                    timestr = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fff: ", CultureInfo.InvariantCulture);

                }
                writer.WriteLine(timestr + message);
                writer.Flush();
            }
        }

        public void Close()
        {
            if (writer != null)
            {
                FinalizeLog();
            }
        }

        ~Logger()
        {
            if (writer != null)
            {
                FinalizeLog();
            }
        }

        /// <summary>
        /// Finalize log, write a terminating line, close file
        /// </summary>
        private void FinalizeLog()
        {
            // if we are logging
            if (writer != null)
            {
                lock (lockObj)
                {
                    // add end line?
                    if (AddFooterLine)
                    {
                        // file closed? then reopen for append
                        if (writer.BaseStream.CanWrite == false)
                        {
                            writer = new StreamWriter(filePath, true);
                        }
                        writer.WriteLine(FooterLineText);
                    }

                    // close
                    if (writer.BaseStream.CanWrite)
                    {
                        writer.Close();
                    }
                    writer = null;
                }
            }
        }
    }
}
