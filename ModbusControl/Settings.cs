using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Windows.Forms;

//TODO: installer maakt geen default settings file, app doet dat bij eerste afsluit.

// We laden en schrijven de XML files manueel. Als we de class naar een XML file zouden schrijven
// ontstaat het probleem dat een aantal XML elementen verdwijnen als je even een oudere versie
// van de applicatie runt.

namespace ModbusControlSpace
{
    public class CSettings
    {
        // company and app name
        private string Company = @"E.D.&A";
        private string AppName = "ModbusControl";
        private string SettingsFilename = "Settings.xml";

        // paths
        public string ApplicationStartupPath;
        public string StoreRoamingPath;
        public string StoreLocalPath;
        public string StoreSharedPath;

        public CRoaming Roaming;    // user roaming settings
        //public CLocal Local;        // user local settings
        public CShared Shared;      // per applciation shared settings

        // constructor
        public CSettings()
        {
            // init paths
            // Get Special Folders paths
            ApplicationStartupPath = Application.StartupPath;
            StoreRoamingPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData), Company, AppName, SettingsFilename);
            StoreLocalPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData), Company, AppName, SettingsFilename);
            StoreSharedPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.CommonApplicationData), Company, AppName, SettingsFilename);

            Roaming = new CRoaming();
            Roaming.FilePath = StoreRoamingPath;
            //Local = new CLocal();
            //Local.FilePath = StoreLocalPath;
            Shared = new CShared();
            Shared.FilePath = StoreSharedPath;
        }

        /// <summary>
        /// Load all settings from files
        /// </summary>
        public void Load()
        {
            Roaming.Load();
            //Local.Load();
            Shared.Load();
        }

        /// <summary>
        /// Save all settings to files
        /// </summary>
        public void Save()
        {
            try
            {
                Roaming.Save();
            }
            catch (Exception ex)
            {
                throw new IOException("Error while saving user settings: " + ex.Message);
            }

            //Local.Save();

            try
            {
                Shared.Save();
            }
            catch (Exception ex)
            {
                throw new IOException("Error while saving shared settings: " + ex.Message);
            }
        }

        // per user, roaming
        public class CRoaming
        {
            public string FilePath;

            // settings
            public bool StopOnError;
            public bool UseLocalOverrides;
            public bool BeepOnInputError;
            public bool AutoNextRowOnEnter;
            public bool AutoStartEdit;
            public bool IgnoreSizeOnReload;
            public bool MbcfileValidationEnable;
            public string MbcfileValidationXSDFile;
            public bool CustomEditorEnable;
            public string CustomEditorCommand;
            public List<string> MbcMruList;
            public CExport Export;
            public CCompare Compare;
            public CCustomRead CustomRead;
            public CCustomWrite CustomWrite;
            public CCustomCommand CustomCommand;
            public CLogger Logger;

            private XmlDocument xdoc;
            private XmlNode xroot;
            private string DummyStr;

            // constructor
            public CRoaming()
            {
                // sets defaults
                StopOnError = false;
                UseLocalOverrides = true;
                BeepOnInputError = true;
                AutoNextRowOnEnter = false;
                AutoStartEdit = false;
                IgnoreSizeOnReload = false;
                MbcfileValidationEnable = false;
                MbcfileValidationXSDFile = "";
                CustomEditorEnable = false;
                CustomEditorCommand = "";
                MbcMruList = new List<string>();
                Export = new CExport();
                Compare = new CCompare();
                CustomRead = new CCustomRead();
                CustomWrite = new CCustomWrite();
                CustomCommand = new CCustomCommand();
                Logger = new CLogger();
            }

            /// <summary>
            /// Load settings from file
            /// Throws exception on error
            /// </summary>
            public void Load()
            {
                // geeft geen melding als file niet bestaat of niet het juiste root element heeft
                // bij save wordt een nieuwe file gemaakt

                // Create the directory tree if it doesn't exists yet
                Directory.CreateDirectory(Path.GetDirectoryName(FilePath));

                // get doc
                // if it does not exist, no worries, is probably the first time this user runs this program
                try
                {
                    xdoc = new XmlDocument();
                    xdoc.Load(FilePath);
                }
                catch (Exception ex)
                {
                    DummyStr = ex.Message;
#if DEBUG
                    MessageBox.Show(ex.Message, "Debug: CSettings.CRoaming.Load() error", MessageBoxButtons.OK, MessageBoxIcon.Error);
#endif

                    /////////////////
                    //TODO
                    // try load load old settings file at old location
                    LoadOldSettings();
                    /////////////

                    xdoc = null;
                    xroot = null;
                    return;
                }

                // get root
                xroot = GetRootElement(xdoc, "modbuscontrol");
                if (xroot == null)
                {
                    // not found
                    xdoc = null;
                    return;
                }

                // read elements
                ReadElementBool(xroot, "stoponerror", ref StopOnError);
                ReadElementBool(xroot, "uselocaloverrides", ref UseLocalOverrides);
                ReadElementBool(xroot, "beeponinputerror", ref BeepOnInputError);
                ReadElementBool(xroot, "autonextrowonenter", ref AutoNextRowOnEnter);
                ReadElementBool(xroot, "autostartedit", ref AutoStartEdit);
                ReadElementBool(xroot, "ignoresizeonreload", ref IgnoreSizeOnReload);
                ReadElementBool(xroot, "mbcfilevalidationenable", ref MbcfileValidationEnable);
                ReadElementString(xroot, "mbcfilevalidationxsdfile", ref MbcfileValidationXSDFile);
                ReadElementBool(xroot, "customeditoreenable", ref CustomEditorEnable);
                ReadElementString(xroot, "customeditorcommand", ref CustomEditorCommand);
                ReadElementStringList(xroot, "mbcmrulist", "file", ref MbcMruList);
                Export.Read(xroot);
                Compare.Read(xroot);
                CustomRead.Read(xroot);
                CustomWrite.Read(xroot);
                CustomCommand.Read(xroot);
                Logger.Read(xroot);
            }

            /// <summary>
            /// Save settings to the xml file
            /// Throws exception on error
            /// </summary>
            public void Save()
            {
                // Create the directory tree if it doesn't exists yet
                Directory.CreateDirectory(Path.GetDirectoryName(FilePath));

                // file or root element did not exist?  
                if (xdoc == null)
                {
                    // create new doc
                    xdoc = new XmlDocument();

                    // the xml declaration is recommended, but not mandatory
                    XmlDeclaration xmlDeclaration = xdoc.CreateXmlDeclaration("1.0", "UTF-8", null);
                    XmlElement root = xdoc.DocumentElement;
                    xdoc.InsertBefore(xmlDeclaration, root);

                    // create root element
                    xroot = xdoc.CreateElement("modbuscontrol");
                    xdoc.AppendChild(xroot);
                }

                // write data
                WriteElementBool(xroot, "stoponerror", StopOnError);
                WriteElementBool(xroot, "uselocaloverrides", UseLocalOverrides);
                WriteElementBool(xroot, "beeponinputerror", BeepOnInputError);
                WriteElementBool(xroot, "autonextrowonenter", AutoNextRowOnEnter);
                WriteElementBool(xroot, "autostartedit", AutoStartEdit);
                WriteElementBool(xroot, "ignoresizeonreload", IgnoreSizeOnReload);
                WriteElementBool(xroot, "mbcfilevalidationenable", MbcfileValidationEnable);
                WriteElementString(xroot, "mbcfilevalidationxsdfile", MbcfileValidationXSDFile);
                WriteElementBool(xroot, "customeditoreenable", CustomEditorEnable);
                WriteElementString(xroot, "customeditorcommand", CustomEditorCommand);
                WriteElementStringList(xroot, "mbcmrulist", "file", MbcMruList);
                Export.Write(xroot);
                Compare.Write(xroot);
                CustomRead.Write(xroot);
                CustomWrite.Write(xroot);
                CustomCommand.Write(xroot);

                // Write file
                // no UTF8 BOM, indented
                using (XmlTextWriter xtw = new XmlTextWriter(FilePath, new UTF8Encoding(false)))
                {
                    xtw.Formatting = Formatting.Indented;
                    xdoc.WriteContentTo(xtw);
                }
            }

            private void LoadOldSettings()
            {
                XmlDocument xdoc = null;

                // old settings.xml is in %programdata%
                string FilePath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.CommonApplicationData), @"E.D.&A", "ModbusControl", "Settings.xml");

                try
                {
                    xdoc = new XmlDocument();
                    xdoc.Load(FilePath);
                }
                catch (Exception)
                {
                    // nope
                }

                // get root
                XmlNode xroot = GetRootElement(xdoc, "modbuscontrol");
                if (xroot != null)
                {
                    // read elements
                    ReadElementBool(xroot, "stoponerror", ref StopOnError);
                    ReadElementBool(xroot, "uselocaloverrides", ref UseLocalOverrides);
                    ReadElementBool(xroot, "beeponinputerror", ref BeepOnInputError);
                    ReadElementBool(xroot, "autonextrowonenter", ref AutoNextRowOnEnter);
                    ReadElementBool(xroot, "autostartedit", ref AutoStartEdit);
                    ReadElementBool(xroot, "ignoresizeonreload", ref IgnoreSizeOnReload);
                    ReadElementBool(xroot, "mbcfilevalidationenable", ref MbcfileValidationEnable);
                    ReadElementString(xroot, "mbcfilevalidationxsdfile", ref MbcfileValidationXSDFile);
                    ReadElementBool(xroot, "customeditoreenable", ref CustomEditorEnable);
                    ReadElementString(xroot, "customeditorcommand", ref CustomEditorCommand);
                    ReadElementStringList(xroot, "mbcmrulist", "file", ref MbcMruList);
                    Export.Read(xroot);
                    Compare.Read(xroot);
                    CustomRead.Read(xroot);
                    CustomWrite.Read(xroot);
                    CustomCommand.Read(xroot);
                    Logger.Read(xroot);
                }
            }
        }

        /*
        // per user, local
        public class CLocal
        {
            public string FilePath;
            // not used
        };
        */

        // per application, shared for all users
        public class CShared
        {
            public string FilePath;

            // settings
            public CUpdater Updater;

            private XmlDocument xdoc;
            private XmlNode xroot;
            private string DummyStr;

            // constructor
            public CShared()
            {
                // sets defaults
                Updater = new CUpdater();
            }

            /// <summary>
            /// Load settings from file
            /// </summary>
            public void Load()
            {
                // geeft geen melding als file niet bestaat of niet het juiste root element heeft
                // bij save wordt dan een nieuwe file gemaakt

                // get doc
                // if it does not exist, no worries, is probably the first time this user runs this app
                try
                {
                    xdoc = new XmlDocument();
                    xdoc.Load(FilePath);
                }
                catch (Exception ex)
                {
                    DummyStr = ex.Message;
#if DEBUG
                    MessageBox.Show(ex.Message, "Debug: CSettings.CShared.Load() error", MessageBoxButtons.OK, MessageBoxIcon.Error);
#endif
                    xdoc = null;
                    xroot = null;
                    return;
                }

                // get root
                xroot = GetRootElement(xdoc, "modbuscontrol");
                if (xroot == null)
                {
                    // not found
                    xdoc = null;
                    return;
                }

                // read elements
                Updater.Read(xroot);
            }

            /// <summary>
            /// Save settings to the xml file
            /// Throws exception on error
            /// </summary>
            public void Save()
            {
                // got no document yet?
                if (xdoc == null)
                {
                    // create new doc
                    xdoc = new XmlDocument();

                    // the xml declaration is recommended, but not mandatory
                    XmlDeclaration xmlDeclaration = xdoc.CreateXmlDeclaration("1.0", "UTF-8", null);
                    XmlElement root = xdoc.DocumentElement;
                    xdoc.InsertBefore(xmlDeclaration, root);

                    // create root
                    xroot = xdoc.CreateElement("modbuscontrol");
                    xdoc.AppendChild(xroot);
                }

                // write data
                Updater.Write(xroot);

                // Write file
                // no UTF8 BOM, indented
                using (XmlTextWriter xtw = new XmlTextWriter(FilePath, new UTF8Encoding(false)))
                {
                    xtw.Formatting = Formatting.Indented;
                    xdoc.WriteContentTo(xtw);
                }
            }
        }

        public class CExport
        {
            public string Tabs;
            public string ExportFormat;
            public string ExportAsciiAs;

            // constructor
            public CExport()
            {
                // sets defaults
                Tabs = "current";
                ExportFormat = "grid";
                ExportAsciiAs = "ascii";
            }

            public void Read(XmlNode xparent)
            {
                XmlNode xn = xparent.SelectSingleNode("export");
                if (xn != null)
                {
                    ReadElementString(xparent, "tabs", ref Tabs);
                    ReadElementString(xparent, "exportformat", ref ExportFormat);
                    ReadElementString(xparent, "exportasciias", ref ExportAsciiAs);
                }
            }

            public void Write(XmlNode xparent)
            {
                XmlNode xn = WriteParentElement(xparent, "export");
                WriteElementString(xn, "tabs", Tabs);
                WriteElementString(xn, "exportformat", ExportFormat);
                WriteElementString(xn, "exportasciias", ExportAsciiAs);
            }
        }

        public class CCompare
        {
            public bool ShowEqual;

            // constructor
            public CCompare()
            {
                // sets defaults
                ShowEqual = true;
            }

            public void Read(XmlNode xparent)
            {
                XmlNode xn = xparent.SelectSingleNode("compare");
                if (xn != null)
                {
                    ReadElementBool(xn, "showequal", ref ShowEqual);
                }
            }

            public void Write(XmlNode xparent)
            {
                XmlNode xn = WriteParentElement(xparent, "compare");
                WriteElementBool(xn, "showequal", ShowEqual);
            }
        }

        public class CCustomRead
        {
            public int SlaveID;
            public int Register;
            public int Quantity;
            public string Datasize; // Datasize is string because that's how we save radiobuttons
            public string ShowValuesAs;

            // constructor
            public CCustomRead()
            {
                // sets defaults
                SlaveID = 1;
                Register = 40001;
                Quantity = 10;
                Datasize = "16";
                ShowValuesAs = "int";
            }

            public void Read(XmlNode xparent)
            {
                XmlNode xn = xparent.SelectSingleNode("customread");
                if (xn != null)
                {
                    ReadElementInt(xn, "slaveid", ref SlaveID);
                    ReadElementInt(xn, "register", ref Register);
                    ReadElementInt(xn, "quantity", ref Quantity);
                    ReadElementString(xn, "datasize", ref Datasize);
                    ReadElementString(xn, "showvaluesas", ref ShowValuesAs);
                }
            }

            public void Write(XmlNode xparent)
            {
                XmlNode xn = WriteParentElement(xparent, "customread");
                WriteElementInt(xn, "slaveid", SlaveID);
                WriteElementInt(xn, "register", Register);
                WriteElementInt(xn, "quantity", Quantity);
                WriteElementString(xn, "datasize", Datasize);
                WriteElementString(xn, "showvaluesas", ShowValuesAs);
            }
        }

        public class CCustomWrite
        {
            public int SlaveID;
            public int Register;
            public int Quantity;
            public string Functioncode;
            public string Datasize;
            public string Data;

            // constructor
            public CCustomWrite()
            {
                // sets defaults
                SlaveID = 1;
                Register = 40001;
                Quantity = 1;
                Functioncode = "16";
                Datasize = "16";
                Data = String.Empty;
            }

            public void Read(XmlNode xparent)
            {
                XmlNode xn = xparent.SelectSingleNode("customwrite");
                if (xn != null)
                {
                    ReadElementInt(xn, "slaveid", ref SlaveID);
                    ReadElementInt(xn, "register", ref Register);
                    ReadElementInt(xn, "quantity", ref Quantity);
                    ReadElementString(xn, "functioncode", ref Functioncode);
                    ReadElementString(xn, "datasize", ref Datasize);
                    ReadElementString(xn, "data", ref Data);
                }
            }

            public void Write(XmlNode xparent)
            {
                XmlNode xn = WriteParentElement(xparent, "customwrite");
                WriteElementInt(xn, "slaveid", SlaveID);
                WriteElementInt(xn, "register", Register);
                WriteElementInt(xn, "quantity", Quantity);
                WriteElementString(xn, "functioncode", Functioncode);
                WriteElementString(xn, "datasize", Datasize);
                WriteElementString(xn, "data", Data);
            }
        }

        public class CCustomCommand
        {
            public string Command;

            // constructor
            public CCustomCommand()
            {
                // sets defaults
                Command = "01 03 00 00 00 0A";
            }

            public void Read(XmlNode xparent)
            {
                XmlNode xn = xparent.SelectSingleNode("customcommand");
                if (xn != null)
                {
                    ReadElementString(xn, "command", ref Command);
                }
            }

            public void Write(XmlNode xparent)
            {
                XmlNode xn = WriteParentElement(xparent, "customcommand");
                WriteElementString(xn, "command", Command);
            }
        }

        public class CLogger
        {
            public bool LogErrors;
            public bool LogWarnings;
            public bool LogInfo;
            public bool LogTrace;
            public bool LogReads;
            public bool LogWrites;
            public bool LogBridgeInfo;
            public bool LogBridgeData;
            public bool LogBridgeDecode;
            public int Capacity;

            // constructor
            public CLogger()
            {
                // sets defaults
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

            public void Read(XmlNode xparent)
            {
                XmlNode xn = xparent.SelectSingleNode("logger");
                if (xn != null)
                {
                    ReadElementBool(xn, "logerrors", ref LogErrors);
                    ReadElementBool(xn, "logwarnings", ref LogWarnings);
                    ReadElementBool(xn, "loginfo", ref LogInfo);
                    ReadElementBool(xn, "logtrace", ref LogTrace);
                    ReadElementBool(xn, "logreads", ref LogReads);
                    ReadElementBool(xn, "logwrites", ref LogWrites);
                    ReadElementBool(xn, "logbridgeinfo", ref LogBridgeInfo);
                    ReadElementBool(xn, "logbridgedata", ref LogBridgeData);
                    ReadElementBool(xn, "logbridgedata", ref LogBridgeData);
                    ReadElementBool(xn, "logbridgedecode", ref LogBridgeDecode);
                    ReadElementInt(xn, "capacity", ref Capacity);
                }
            }

            public void Write(XmlNode xparent)
            {
                XmlNode xn = WriteParentElement(xparent, "logger");
                WriteElementBool(xn, "logerrors", LogErrors);
                WriteElementBool(xn, "logwarnings", LogWarnings);
                WriteElementBool(xn, "loginfo", LogInfo);
                WriteElementBool(xn, "logtrace", LogTrace);
                WriteElementBool(xn, "logreads", LogReads);
                WriteElementBool(xn, "logwrites", LogWrites);
                WriteElementBool(xn, "logbridgeinfo", LogBridgeInfo);
                WriteElementBool(xn, "logbridgedata", LogBridgeData);
                WriteElementBool(xn, "logbridgedecode", LogBridgeDecode);
                WriteElementInt(xn, "capacity", Capacity);
            }
        }

        public class CUpdater
        {
            // settings
            public bool CheckForUpdates;
            public DateTime LastCheckForUpdate;

            // constructor
            public CUpdater()
            {
                // sets defaults
                CheckForUpdates = true;
                LastCheckForUpdate = DateTime.MinValue;
            }

            public void Read(XmlNode xparent)
            {
                ReadElementBool(xparent, "checkforupdates", ref CheckForUpdates);
                ReadElementDatetime(xparent, "lastcheckforupdate", ref LastCheckForUpdate);
            }

            public void Write(XmlNode xparent)
            {
                XmlNode xn = WriteParentElement(xparent, "update");
                WriteElementBool(xn, "checkforupdates", CheckForUpdates);
                WriteElementDatetime(xn, "lastcheckforupdate", LastCheckForUpdate);
            }
        }

        private static void ReadElementBool(XmlNode xparent, string name, ref bool o)
        {
            XmlNode xn = xparent.SelectSingleNode(name);
            if (xn != null)
            {
                if (xn.InnerText == "true")
                    o = true;
                if (xn.InnerText == "false")
                    o = false;
            }
        }

        private static void ReadElementInt(XmlNode xparent, string name, ref int o)
        {
            XmlNode xn = xparent.SelectSingleNode(name);
            if (xn != null)
            {
                int tmpint;
                if (int.TryParse(xn.InnerText, out tmpint))
                {
                    o = tmpint;
                }
            }
        }

        private static void ReadElementString(XmlNode xparent, string name, ref string o)
        {
            XmlNode xn = xparent.SelectSingleNode(name);
            if (xn != null)
            {
                o = xn.InnerText;
            }
        }

        private static void ReadElementDatetime(XmlNode xparent, string name, ref DateTime o)
        {
            XmlNode xn = xparent.SelectSingleNode(name);
            if (xn != null)
            {
                DateTime tmpdatetime;
                if (DateTime.TryParse(xn.InnerText, out tmpdatetime))
                {
                    o = tmpdatetime;
                }
            }
        }

        //TODO: goed testen
        private static void ReadElementStringList(XmlNode xparent, string parentname, string name, ref List<string> strlist)
        {
            XmlNode xn = xparent.SelectSingleNode(parentname);
            if (xn != null)
            {
                strlist.Clear();
                XmlNodeList xl = xn.SelectNodes(name);
                foreach (XmlNode ln in xl)
                {
                    strlist.Add(ln.InnerText);
                }
            }
        }

        private static XmlNode WriteElementBool(XmlNode parent, string name, bool value)
        {
            // gebruik altijd lower case true en false
            return WriteElementString(parent, name, value.ToString().ToLower());
        }

        private static XmlNode WriteElementInt(XmlNode parent, string name, int value)
        {
            return WriteElementString(parent, name, value.ToString());
        }

        private static XmlNode WriteElementString(XmlNode parent, string name, string value)
        {
            XmlDocument xdoc = parent.OwnerDocument;
            XmlNode xn = parent.SelectSingleNode(name);
            if (xn == null)
            {
                XmlElement e = xdoc.CreateElement(name);
                xn = parent.AppendChild(e);
            }
            xn.InnerText = value;
            return xn;
        }

        private static XmlNode WriteElementDatetime(XmlNode parent, string name, DateTime value)
        {
            return WriteElementString(parent, name, value.ToString());
        }

        private static XmlNode WriteParentElement(XmlNode parent, string name)
        {
            XmlDocument doc = parent.OwnerDocument;
            XmlNode xn = parent.SelectSingleNode(name);
            if (xn == null)
            {
                XmlElement e = doc.CreateElement(name);
                xn = parent.AppendChild(e);
            }
            return xn;
        }

        //TODO: goed testen
        private static void WriteElementStringList(XmlNode parent, string parentname, string name, List<string> strlist)
        {
            XmlDocument xdoc = parent.OwnerDocument;
            XmlNode xp = WriteParentElement(parent, parentname);
            xp.RemoveAll();
            foreach (string s in strlist)
            {
                XmlElement e = xdoc.CreateElement(name);
                e.InnerText = s;
                xp.AppendChild(e);
            }
        }

        /// <summary>
        /// Get the first XML element
        /// Check if name matches
        /// Return null if no match
        /// </summary>
        private static XmlNode GetRootElement(XmlNode parent, string name)
        {
            XmlNode n = parent.FirstChild;
            // find first element, skip XML declaration
            while ((n != null) && (n.NodeType != XmlNodeType.Element))
            {
                n = n.NextSibling;
            }
            if ((n != null) && (n.Name == name))
            {
                // found
                return n;
            }
            // not found
            return null;
        }
    }
}
