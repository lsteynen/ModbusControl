using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Diagnostics;

// CSV file rules:
// - fields separated by Separator, usually comma ',', semicolon ';' or tab
// - fields containing the separator must be quoted
// - quoting is with "
// - if a field contains the quote character it must be quoted and each quote in the field must be escaped with a preceding quote (as in MS Excel)
// - spaces: unquoted fields: any leading or trailing spaces are part of the field
// - spaces: quoted fields: spaces inside the quotes are part of the field
// - spaces: quoted fields: spaces leading or trailing the quoted fields are forbidden and considered invalid format
// - an empty line is returned as 1 empty field if SkipEmptyLines=false
// - a line with only spaces is seen as 1 field containing spaces

namespace LST.Helpers
{
    public class CsvReader : IDisposable
    {
		// Private members
		
		private StreamReader mReader;
        private string mCurrentLine;
        private int mCurrentPos;

        // properties

        public string[] CommentHeaders
        {
            get;
            set;
        }

        public char Separator
        {
            get;
            set;
        }

        public bool SkipEmptyLines
        {
            get;
            set;
        }

        public bool SkipCommentLines
        {
            get;
            set;
        }

        public string CurrentLine
        {
            get { return mCurrentLine; }
        }
        
        /// <summary>
        /// returns the actual line number in the file of the last ReadRow()
        /// </summary>
        public int LineNumber
        {
            get;
            private set;
        }

        public enum Result
        {
            Data,
            Comment,
            EOF,
        };

		// constructor

        // use encoding for the operating system's current ANSI code page
        public CsvReader( string path)
        {
            // set defaults
            CommentHeaders = new string[] { "#"};
            SkipEmptyLines = true;
            SkipCommentLines = true;
            Separator = ',';
			mReader = new StreamReader(path, Encoding.Default);
        }

        // use specified encoding (Default, a certain codepage, ASCII, UTF-8, ...)
        public CsvReader(string path, Encoding encoding)
        {
            // set defaults
            CommentHeaders = new string[] { "#"};
            SkipEmptyLines = true;
            SkipCommentLines = true;
            Separator = ',';
            mReader = new StreamReader(path, encoding);
        }


        /// <summary>
        /// Reads a row of data from a CSV file
        /// </summary>
        /// <param name="fields"></param>
        /// <returns>Result.EOF if end of file</returns>
        /// <returns>Result.Empty if empty line and SkipEmptyLines == false</returns>
        /// <returns>Result.Comment  if comment line and SkipCommentLines == false</returns>
        /// <returns>Result.Data if data record</returns>
        public Result ReadRow(out List<string> fields)
        {
            fields = new List<string>();

        ReadNextLine:
            // Read next line from the file
            mCurrentLine = mReader.ReadLine();
            mCurrentPos = 0;
            // End of file?
            if (mCurrentLine == null)
                return Result.EOF;
            ++LineNumber;
            // Is it an empty line? (not even spaces)
            if (mCurrentLine.Length == 0)
            {
                if (SkipEmptyLines)
                    goto ReadNextLine;
                else
                {
                    fields.Add("");
                    return Result.Data;
                }
            }
            // Is it a comment line?
            foreach( string header in CommentHeaders)
            {
                if (mCurrentLine.StartsWith(header))
                {
                    if (SkipCommentLines)
                        goto ReadNextLine;
                    else
                    {
                        fields.Add(mCurrentLine.Substring(header.Length));
                        return Result.Comment;
                    }
                }
            }

            // Parse line
            string field;
            int numFields = 0;
            while (true)
            {
                // Read next field
                if ((mCurrentPos < mCurrentLine.Length) && (mCurrentLine[mCurrentPos] == '"'))
                    field = ReadQuotedField();
                else
                    field = ReadUnquotedField();
                // Add field to list
                fields.Add(field);
                numFields++;
                // Break if we reached the end of the line
                if (mCurrentLine == null || mCurrentPos == mCurrentLine.Length)
                    break;
                // Otherwise skip delimiter
                Debug.Assert(mCurrentLine[mCurrentPos] == Separator);
                mCurrentPos++;
            }
            // Indicate success
            return Result.Data;
        }

        /// <summary>
        /// Reads a quoted field by reading from the current line until a
        /// closing quote is found or the end of the file is reached. On return,
        /// the current position points to the delimiter or the end of the last
        /// line in the file. Note: mCurrentLine may be set to null on return.
        /// </summary>
        private string ReadQuotedField()
        {
            // Skip opening quote character
            Debug.Assert(mCurrentLine[mCurrentPos] == '"');
            mCurrentPos++;

            // Parse field
            StringBuilder builder = new StringBuilder();
            while (true)
            {
                while (mCurrentPos == mCurrentLine.Length)
                {
                    // End of line so attempt to read the next line
                    mCurrentLine = mReader.ReadLine();
                    ++LineNumber;
                    mCurrentPos = 0;
                    // Done if we reached the end of the file
                    if (mCurrentLine == null)
                        return builder.ToString();
                    // Otherwise, treat as a multi-line field
                    builder.Append(Environment.NewLine);
                }

                // Test for quote character
                if (mCurrentLine[mCurrentPos] == '"')
                {
                    // If two quotes, skip first and treat second as literal
                    int nextPos = (mCurrentPos + 1);
                    if ((nextPos < mCurrentLine.Length) && (mCurrentLine[nextPos] == '"'))
                        mCurrentPos++;
                    else
                        break;  // Single quote ends quoted sequence
                }
                // Add current character to the field
                builder.Append(mCurrentLine[mCurrentPos++]);
            }

            if (mCurrentPos < mCurrentLine.Length)
            {
                // Consume closing quote
                Debug.Assert(mCurrentLine[mCurrentPos] == '"');
                mCurrentPos++;
                // Append any additional characters appearing before next delimiter
                builder.Append(ReadUnquotedField());
            }
            // Return field value
            return builder.ToString();
        }

        /// <summary>
        /// Reads an unquoted field by reading from the current line until a
        /// separator is found or the end of the line is reached. 
        /// On return, the current position points to the separator or the end 
        /// of the current line.
        /// </summary>
        private string ReadUnquotedField()
        {
            int startPos = mCurrentPos;
            mCurrentPos = mCurrentLine.IndexOf(Separator, mCurrentPos);
            if (mCurrentPos == -1)
                mCurrentPos = mCurrentLine.Length;
            if (mCurrentPos > startPos)
                return mCurrentLine.Substring(startPos, mCurrentPos - startPos);
            return String.Empty;
        }

        public void Close()
        {
            mReader.Close();
        }

        // Propagate Dispose to StreamReader
        public void Dispose()
        {
            mReader.Dispose();
        }
    }

    public class CsvWriter : IDisposable
    {
        // Private members

        private StreamWriter mWriter;
        private string mCurrentLine;

        // properties

        public string CommentHeader
        {
            get;
            set;
        }

        public char Separator
        {
            get;
            set;
        }

        public bool QuoteAllFields
        {
            get;
            set;
        }

        public bool[] QuoteSomeFields
        {
            get;
            set;
        }

        public int LineCount
        {
            get;
            private set;
        }

        // constructor

        // use encoding for the operating system's current ANSI code page
        public CsvWriter( string path)
        {
            // set defaults
            CommentHeader = "#";
            Separator = ',';
            QuoteAllFields = false;
            LineCount = 0;
            mWriter = new StreamWriter(path, false, Encoding.Default);
        }

        // use specified encoding (Default, a certain codepage, ASCII, UTF-8, ...)
        public CsvWriter(string path, Encoding encoding)
        {
            // set defaults
            CommentHeader = "#";
            Separator = ',';
            QuoteAllFields = false;
            LineCount = 0;
            mWriter = new StreamWriter(path, false, encoding);
        }

        // methods

        /// <summary>
        /// Writes a comment line to a CSV file.
        /// </summary>
        /// <param name="text">The comment line to be written</param>
        public void WriteComment(string text)
        {
            mCurrentLine = CommentHeader + text;
            mWriter.WriteLine(mCurrentLine);
            ++LineCount;
        }

        /// <summary>
        /// Writes a single row to a CSV file.
        /// </summary>
        /// <param name="row">The row to be written</param>
        public void WriteRow(string[] row)
        {
            StringBuilder builder = new StringBuilder();
            bool firstField = true;
            int index = 0;

            // make an char[] with the special characters that force quoting of the field
            char[] specialChars = new char[] { Separator, '"', '\r', '\n' };

            foreach (string field in row)
            {
                // Add separator if this isn't the first field
                if (!firstField)
                    builder.Append(Separator);
                // Implement special handling for values that contain comma or quote
                // Enclose in quotes and double up any double quotes
                bool quoteThisField = (QuoteSomeFields != null) && (index < QuoteSomeFields.Length) && QuoteSomeFields[index];
                if ((field.IndexOfAny(specialChars) != -1) || quoteThisField || QuoteAllFields)
                    builder.AppendFormat("\"{0}\"", field.Replace("\"", "\"\""));
                else
                    builder.Append(field);
                ++index;
                firstField = false;
            }
            mCurrentLine = builder.ToString();
            mWriter.WriteLine(mCurrentLine);
            ++LineCount;
        }

        public void Close()
        {
            mWriter.Close();
        }

        // Propagate Dispose to StreamWriter
        public void Dispose()
        {
            mWriter.Dispose();
        }
    }
}
