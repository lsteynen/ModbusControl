using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Windows.Forms;

namespace ModbusControlSpace
{
    public class Settings
    {
        private XmlDocument doc;
        private XmlNode xmlroot;

        // public

        public class CValues
        {
            public bool StopOnError { get; set; }
            public bool UseLocalOverrides { get; set; }
            public bool BeepOnInputError { get; set; }
            public bool AutoNextRowOnEnter { get; set; }
            public bool AutoStartEdit { get; set; }
            public bool IgnoreSizeOnReload { get; set; }
            public bool CustomEditorEnable { get; set; }
            public string CustomEditorCommand { get; set; }
            public bool CheckForUpdates { get; set; }
            public DateTime LastCheckForUpdate { get; set; }
            public List<string> MbcMruList { get; set; }
            public CExport Export { get; set; }
            public CCompare Compare { get; set; }
            public CCustomRead CustomRead { get; set; }
            public CCustomWrite CustomWrite { get; set; }
            public CCustomCommand CustomCommand { get; set; }
            public CLoggerValues Logger { get; set; }

            // constructor
            public CValues()
            {
                // sets default values
                StopOnError = false;
                UseLocalOverrides = true;
                BeepOnInputError = true;
                AutoNextRowOnEnter = false;
                AutoStartEdit = false;
                IgnoreSizeOnReload = false;
                CustomEditorEnable = false;
                CustomEditorCommand = "";
                CheckForUpdates = true;
                LastCheckForUpdate = DateTime.MinValue;
                MbcMruList = new List<string>();
                Export = new CExport();
                Compare = new CCompare();
                CustomRead = new CCustomRead();
                CustomWrite = new CCustomWrite();
                CustomCommand = new CCustomCommand();
                Logger = new CLoggerValues();
            }
        };

        public class CExport
        {
            public string Tabs { get; set; }
            public string ExportFormat { get; set; }
            public string ExportAsciiAs { get; set; }

            // constructor
            public CExport()
            {
                // sets default values
                Tabs = "current";
                ExportFormat = "grid";
                ExportAsciiAs = "ascii";
            }
        }

        public class CCompare
        {
            public bool ShowEqual { get; set; }

            // constructor
            public CCompare()
            {
                // sets default values
                ShowEqual = true;
            }
        }

        public class CCustomRead
        {
            public int SlaveID { get; set; }
            public int Register { get; set; }
            public int Quantity { get; set; }
            public string Datasize { get; set; } // Datasize is string because that's how we save radiobuttons
            public string ShowValuesAs { get; set; }

            // constructor
            public CCustomRead()
            {
                // sets default values
                SlaveID = 1;
                Register = 40001;
                Quantity = 10;
                Datasize = "16";
                ShowValuesAs = "int";
            }
        }

        public class CCustomWrite
        {
            public int SlaveID { get; set; }
            public int Register { get; set; }
            public int Quantity { get; set; }
            public string Functioncode { get; set; }
            public string Datasize { get; set; }
            public string Data { get; set; }

            // constructor
            public CCustomWrite()
            {
                // sets default values
                SlaveID = 1;
                Register = 40001;
                Quantity = 1;
                Functioncode = "16";
                Datasize = "16";
                Data = String.Empty;
            }
        }

        public class CCustomCommand
        {
            public string Command { get; set; }

            // constructor
            public CCustomCommand()
            {
                // sets default values
                Command = "01 03 00 00 00 0A";
            }
        }

        public class CLoggerValues
        {
            public bool LogErrors { get; set; }
            public bool LogWarnings { get; set; }
            public bool LogInfo { get; set; }
            public bool LogTrace { get; set; }
            public bool LogReads { get; set; }
            public bool LogWrites { get; set; }
            public bool LogBridgeInfo { get; set; }
            public bool LogBridgeData { get; set; }
            public bool LogBridgeDecode { get; set; }
            public int Capacity { get; set; }

            // constructor
            public CLoggerValues()
            {
                // sets default values
                LogErrors = true;
                LogWarnings = true;
                LogInfo = true;
                LogTrace = false;
                LogReads = false;
                LogWrites = false;
                LogBridgeInfo = false;
                LogBridgeData = false;
                LogBridgeDecode = false;
                Capacity = 5000;
            }
        }

        public CValues Values;
        public string StorePath = "Settings.xml";

        /// <summary>
        /// Load settings from the xml file
        /// Return false on error
        /// </summary>
        public bool TryLoad()
        {
            bool result = true;

            try
            {
                Load();
            }
            catch
            {
                result = false;
            }

            return result;
        }

        /// <summary>
        /// Load settings from the xml file
        /// Throws exception on error
        /// </summary>
        public void Load()
        {
            XmlNode xn;

            // Create a Values object with defaults
            Values = new CValues();

            // Create the directory tree if it doesn't exists yet
            Directory.CreateDirectory(Path.GetDirectoryName(StorePath));

            // load settings xml file
            doc = new XmlDocument();
            doc.Load(StorePath);

            // select <modbuscontrol> element as our parent of everything
            xmlroot = doc.SelectSingleNode("modbuscontrol");
            if (xmlroot == null)
                throw new XmlException("root element not found");

            // parse elements
            xn = xmlroot.SelectSingleNode("stoponerror");
            if (xn != null)
            {
                Values.StopOnError = ParseBool(xn.InnerText);
            }

            xn = xmlroot.SelectSingleNode("uselocaloverrides");
            if (xn != null)
            {
                Values.UseLocalOverrides = ParseBool(xn.InnerText);
            }

            xn = xmlroot.SelectSingleNode("beeponinputerror");
            if (xn != null)
            {
                Values.BeepOnInputError = ParseBool(xn.InnerText);
            }

            xn = xmlroot.SelectSingleNode("autonextrowonenter");
            if (xn != null)
            {
                Values.AutoNextRowOnEnter = ParseBool(xn.InnerText);
            }

            xn = xmlroot.SelectSingleNode("autostartedit");
            if (xn != null)
            {
                Values.AutoStartEdit = ParseBool(xn.InnerText);
            }

            xn = xmlroot.SelectSingleNode("ignoresizeonreload");
            if (xn != null)
            {
                Values.IgnoreSizeOnReload = ParseBool(xn.InnerText);
            }

            xn = xmlroot.SelectSingleNode("customeditoreenable");
            if (xn != null)
            {
                Values.CustomEditorEnable = ParseBool(xn.InnerText);
            }

            xn = xmlroot.SelectSingleNode("customeditorcommand");
            if (xn != null)
            {
                Values.CustomEditorCommand = xn.InnerText;
            }

            xn = xmlroot.SelectSingleNode("checkforupdates");
            if (xn != null)
            {
                Values.CheckForUpdates = ParseBool(xn.InnerText);
            }

            xn = xmlroot.SelectSingleNode("lastcheckforupdate");
            if (xn != null)
            {
                Values.LastCheckForUpdate = DateTime.Parse(xn.InnerText);
            }

            xn = xmlroot.SelectSingleNode("mbcmrulist");
            if (xn != null)
            {
                XmlNodeList xl = xn.SelectNodes("file");
                foreach (XmlNode ln in xl)
                {
                    Values.MbcMruList.Add(ln.InnerText);
                }
            }

            xn = xmlroot.SelectSingleNode("export");
            if (xn != null)
            {
                XmlNode xn2 = xn.SelectSingleNode("tabs");
                if (xn2 != null)
                {
                    Values.Export.Tabs = xn2.InnerText;
                }

                xn2 = xn.SelectSingleNode("exportformat");
                if (xn2 != null)
                {
                    Values.Export.ExportFormat = xn2.InnerText;
                }

                xn2 = xn.SelectSingleNode("exportasciias");
                if (xn2 != null)
                {
                    Values.Export.ExportAsciiAs = xn2.InnerText;
                }
            }

            xn = xmlroot.SelectSingleNode("compare");
            if (xn != null)
            {
                XmlNode xn2 = xn.SelectSingleNode("showequal");
                if (xn2 != null)
                {
                    Values.Compare.ShowEqual = ParseBool(xn.InnerText);
                }
            }

            xn = xmlroot.SelectSingleNode("customread");
            if (xn != null)
            {
                XmlNode xn2 = xn.SelectSingleNode("slaveid");
                if (xn2 != null)
                {
                    Values.CustomRead.SlaveID = ParseInt(xn2.InnerText);
                }

                xn2 = xn.SelectSingleNode("register");
                if (xn2 != null)
                {
                    Values.CustomRead.Register = ParseInt(xn2.InnerText);
                }

                xn2 = xn.SelectSingleNode("quantity");
                if (xn2 != null)
                {
                    Values.CustomRead.Quantity = ParseInt(xn2.InnerText);
                }

                xn2 = xn.SelectSingleNode("datasize");
                if (xn2 != null)
                {
                    Values.CustomRead.Datasize = xn2.InnerText;
                }

                xn2 = xn.SelectSingleNode("quantity");
                if (xn2 != null)
                {
                    Values.CustomRead.Quantity = ParseInt(xn2.InnerText);
                }

                xn2 = xn.SelectSingleNode("showvaluesas");
                if (xn2 != null)
                {
                    Values.CustomRead.ShowValuesAs = xn2.InnerText;
                }
            }

            xn = xmlroot.SelectSingleNode("customwrite");
            if (xn != null)
            {
                XmlNode xn2 = xn.SelectSingleNode("slaveid");
                if (xn2 != null)
                {
                    Values.CustomWrite.SlaveID = ParseInt(xn2.InnerText);
                }

                xn2 = xn.SelectSingleNode("register");
                if (xn2 != null)
                {
                    Values.CustomWrite.Register = ParseInt(xn2.InnerText);
                }

                xn2 = xn.SelectSingleNode("quantity");
                if (xn2 != null)
                {
                    Values.CustomWrite.Quantity = ParseInt(xn2.InnerText);
                }

                xn2 = xn.SelectSingleNode("functioncode");
                if (xn2 != null)
                {
                    Values.CustomWrite.Functioncode = xn2.InnerText;
                }

                xn2 = xn.SelectSingleNode("datasize");
                if (xn2 != null)
                {
                    Values.CustomWrite.Datasize = xn2.InnerText;
                }

                xn2 = xn.SelectSingleNode("data");
                if (xn2 != null)
                {
                    Values.CustomWrite.Data = xn2.InnerText;
                }
            }

            xn = xmlroot.SelectSingleNode("customcommand");
            if (xn != null)
            {
                XmlNode xn2 = xn.SelectSingleNode("command");
                if (xn2 != null)
                {
                    Values.CustomCommand.Command = xn2.InnerText;
                }
            }

            xn = xmlroot.SelectSingleNode("logger");
            if (xn != null)
            {
                XmlNode xn2 = xn.SelectSingleNode("logerrors");
                if (xn2 != null)
                {
                    Values.Logger.LogErrors = ParseBool(xn2.InnerText);
                }

                xn2 = xn.SelectSingleNode("logwarnings");
                if (xn2 != null)
                {
                    Values.Logger.LogWarnings = ParseBool(xn2.InnerText);
                }

                xn2 = xn.SelectSingleNode("loginfo");
                if (xn2 != null)
                {
                    Values.Logger.LogInfo = ParseBool(xn2.InnerText);
                }

                xn2 = xn.SelectSingleNode("logtrace");
                if (xn2 != null)
                {
                    Values.Logger.LogTrace = ParseBool(xn2.InnerText);
                }

                xn2 = xn.SelectSingleNode("logreads");
                if (xn2 != null)
                {
                    Values.Logger.LogReads = ParseBool(xn2.InnerText);
                }

                xn2 = xn.SelectSingleNode("logwrites");
                if (xn2 != null)
                {
                    Values.Logger.LogWrites = ParseBool(xn2.InnerText);
                }

                xn2 = xn.SelectSingleNode("logbridgeinfo");
                if (xn2 != null)
                {
                    Values.Logger.LogBridgeInfo = ParseBool(xn2.InnerText);
                }

                xn2 = xn.SelectSingleNode("logbridgedata");
                if (xn2 != null)
                {
                    Values.Logger.LogBridgeData = ParseBool(xn2.InnerText);
                }

                xn2 = xn.SelectSingleNode("logbridgedecode");
                if (xn2 != null)
                {
                    Values.Logger.LogBridgeDecode = ParseBool(xn2.InnerText);
                }

                xn2 = xn.SelectSingleNode("capacity");
                if (xn2 != null)
                {
                    Values.Logger.Capacity = ParseInt(xn2.InnerText);
                }

            }
        }

        /// <summary>
        /// Save settings to the xml file
        /// </summary>
        public void Save()
        {
            XmlNode xn;

            // xml root does not exist?  
            if (xmlroot == null)
            {
                // create new doc
                doc = new XmlDocument();

                // the xml declaration is recommended, but not mandatory
                XmlDeclaration xmlDeclaration = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
                XmlElement root = doc.DocumentElement;
                doc.InsertBefore(xmlDeclaration, root);

                // create root element
                xmlroot = doc.CreateElement("modbuscontrol");
                doc.AppendChild(xmlroot);
            }

            // Make changes to the document
            UpdateOrInsertElementBool(xmlroot, "stoponerror", Values.StopOnError);
            UpdateOrInsertElementBool(xmlroot, "uselocaloverrides", Values.UseLocalOverrides);
            UpdateOrInsertElementBool(xmlroot, "beeponinputerror", Values.BeepOnInputError);
            UpdateOrInsertElementBool(xmlroot, "autonextrowonenter", Values.AutoNextRowOnEnter);
            UpdateOrInsertElementBool(xmlroot, "autostartedit", Values.AutoStartEdit);
            UpdateOrInsertElementBool(xmlroot, "ignoresizeonreload", Values.IgnoreSizeOnReload);
            UpdateOrInsertElementBool(xmlroot, "customeditoreenable", Values.CustomEditorEnable);
            UpdateOrInsertElementString(xmlroot, "customeditorcommand", Values.CustomEditorCommand);
            UpdateOrInsertElementBool(xmlroot, "checkforupdates", Values.CheckForUpdates);
            UpdateOrInsertElementDatetime(xmlroot, "lastcheckforupdate", Values.LastCheckForUpdate);
            xn = UpdateOrInsertElementParent(xmlroot, "mbcmrulist");
            UpdateOrInsertElementListString(xn, "file", Values.MbcMruList);

            xn = UpdateOrInsertElementParent(xmlroot, "export");
            UpdateOrInsertElementString(xn, "tabs", Values.Export.Tabs);
            UpdateOrInsertElementString(xn, "exportformat", Values.Export.ExportFormat);
            UpdateOrInsertElementString(xn, "exportasciias", Values.Export.ExportAsciiAs);

            xn = UpdateOrInsertElementParent(xmlroot, "compare");
            UpdateOrInsertElementBool(xn, "showequal", Values.Compare.ShowEqual);

            xn = UpdateOrInsertElementParent(xmlroot, "customread");
            UpdateOrInsertElementInt(xn, "slaveid", Values.CustomRead.SlaveID);
            UpdateOrInsertElementInt(xn, "register", Values.CustomRead.Register);
            UpdateOrInsertElementInt(xn, "quantity", Values.CustomRead.Quantity);
            UpdateOrInsertElementString(xn, "datasize", Values.CustomRead.Datasize);
            UpdateOrInsertElementString(xn, "showvaluesas", Values.CustomRead.ShowValuesAs);

            xn = UpdateOrInsertElementParent(xmlroot, "customwrite");
            UpdateOrInsertElementInt(xn, "slaveid", Values.CustomWrite.SlaveID);
            UpdateOrInsertElementInt(xn, "register", Values.CustomWrite.Register);
            UpdateOrInsertElementInt(xn, "quantity", Values.CustomWrite.Quantity);
            UpdateOrInsertElementString(xn, "functioncode", Values.CustomWrite.Functioncode);
            UpdateOrInsertElementString(xn, "datasize", Values.CustomWrite.Datasize);
            UpdateOrInsertElementString(xn, "data", Values.CustomWrite.Data);

            xn = UpdateOrInsertElementParent(xmlroot, "customcommand");
            UpdateOrInsertElementString(xn, "command", Values.CustomCommand.Command);

            xn = UpdateOrInsertElementParent(xmlroot, "logger");
            UpdateOrInsertElementBool(xn, "logerrors", Values.Logger.LogErrors);
            UpdateOrInsertElementBool(xn, "logwarnings", Values.Logger.LogWarnings);
            UpdateOrInsertElementBool(xn, "loginfo", Values.Logger.LogInfo);
            UpdateOrInsertElementBool(xn, "logtrace", Values.Logger.LogTrace);
            UpdateOrInsertElementBool(xn, "logreads", Values.Logger.LogReads);
            UpdateOrInsertElementBool(xn, "logwrites", Values.Logger.LogWrites);
            UpdateOrInsertElementBool(xn, "logbridgeinfo", Values.Logger.LogBridgeInfo);
            UpdateOrInsertElementBool(xn, "logbridgedata", Values.Logger.LogBridgeData);
            UpdateOrInsertElementBool(xn, "logbridgedecode", Values.Logger.LogBridgeDecode);
            UpdateOrInsertElementInt(xn, "capacity", Values.Logger.Capacity);

            // Write document
            using (XmlTextWriter xtw = new XmlTextWriter(StorePath, Encoding.UTF8))
            {
                xtw.Formatting = Formatting.Indented;
                doc.WriteContentTo(xtw);
            }
        }

        private bool ParseBool(string s)
        {
            return (string.Compare(s.Trim(), "true", StringComparison.OrdinalIgnoreCase) == 0);
        }

        private int ParseInt(string s)
        {
            int result = 0;
            int.TryParse(s, out result);
            return result;
        }

        private XmlNode UpdateOrInsertElementString(XmlNode parent, string name, string value)
        {
            XmlNode xn = parent.SelectSingleNode(name);
            if (xn == null)
            {
                XmlElement e = doc.CreateElement(name);
                xn = parent.AppendChild(e);
            }
            xn.InnerText = value;
            return xn;
        }

        private XmlNode UpdateOrInsertElementBool(XmlNode parent, string name, bool value)
        {
            return UpdateOrInsertElementString(parent, name, value.ToString().ToLower());
        }

        private XmlNode UpdateOrInsertElementInt(XmlNode parent, string name, int value)
        {
            return UpdateOrInsertElementString(parent, name, value.ToString());
        }

        private XmlNode UpdateOrInsertElementDatetime(XmlNode parent, string name, DateTime value)
        {
            return UpdateOrInsertElementString(parent, name, value.ToString());
        }

        private XmlNode UpdateOrInsertElementParent(XmlNode parent, string name)
        {
            XmlNode xn = parent.SelectSingleNode(name);
            if (xn == null)
            {
                XmlElement e = doc.CreateElement(name);
                xn = parent.AppendChild(e);
            }
            return xn;
        }

        private void UpdateOrInsertElementListString(XmlNode parent, string name, List<string> valuelist)
        {
            parent.RemoveAll();
            foreach (string s in valuelist)
            {
                XmlElement e = doc.CreateElement(name);
                e.InnerText = s;
                XmlNode xn = parent.AppendChild(e);
            }
        }

    }
}
