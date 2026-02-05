using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;

using LST.Helpers;

// Note: Installer maakt geen default settings file, app doet dat bij eerste close.
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

        // paths
        public string StoreRoamingDir;
        public string StoreRoamingPath;
        //public string StoreLocalDir;
        //public string StoreLocalPath;
        public string StoreSharedDir;
        public string StoreSharedPath;

        public CRoaming Roaming;    // user roaming settings
        //public CLocal Local;        // user local settings
        public CShared Shared;      // per application shared settings

        // constructor
        public CSettings()
        {
            // init paths
            // Get Special Folders paths
            StoreRoamingDir = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData), Company, AppName);
            StoreRoamingPath = Path.Combine(StoreRoamingDir, "Settings.xml");
            //StoreLocalDir = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData), Company, AppName);
            //StoreLocalPath = Path.Combine(StoreLocalDir, "Settings.xml");
            StoreSharedDir = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.CommonApplicationData), Company, AppName);
            StoreSharedPath = Path.Combine(StoreSharedDir, "CSettings.xml"); // Common settings, andere filename omdat de file Settings.xml gedelete wordt na migratie van de user settings.

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
            public bool BeepOnInputError;
            public bool AutoNextRowOnEnter;
            public bool AutoStartEdit;
            public bool IgnoreSizeOnReload;
            public bool ShowVarProperties;
            public bool MultilineAutosizeRows;
            public bool ClearFilterOnTabChange;
            public bool MbcfileValidationEnable;
            public string MbcfileValidationXSDFile;
            public bool ShowFindFilterBox;
            public bool CustomEditorEnable;
            public string CustomEditorCommand;
            public List<string> MbcMruList;
            public ColumnWidthsType ColumnWidths;
            public CExport Export;
            public CImport Import;
            public CCustomQueries CustomQueries;
            public CLogger Logger;

            private XmlDocument xdoc;
            private XmlNode xroot;
            
            // constructor
            public CRoaming()
            {
                // sets defaults
                StopOnError = false;
                BeepOnInputError = true;
                AutoNextRowOnEnter = false;
                AutoStartEdit = false;
                IgnoreSizeOnReload = false;
                ShowVarProperties = false;
                MultilineAutosizeRows = false;
                ClearFilterOnTabChange = false;
                MbcfileValidationEnable = false; // see further
                MbcfileValidationXSDFile = ""; // see further
                ShowFindFilterBox = true;
                CustomEditorEnable = false;
                CustomEditorCommand = "";
                MbcMruList = new List<string>();
                ColumnWidths = new ColumnWidthsType();
                Export = new CExport();
                Import = new CImport();
                CustomQueries = new CCustomQueries();
                Logger = new CLogger();

                // setup ValidationXSDFile, if we find it
                string Filepath = Path.Combine(AppHelpers.ApplicationStartupPath, "MbcValidate.xsd");
                if (File.Exists(Filepath))
                {
                    MbcfileValidationEnable = true;
                    MbcfileValidationXSDFile = Filepath;
                }
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
                // then try to load load old settings file at old location
                try
                {
                    Debug.Print(DateTime.Now.TimeOfDay + ": CSettings.CRoaming.Load: " + FilePath);

                    xdoc = new XmlDocument();
                    xdoc.Load(FilePath);
                }
                catch (Exception ex)
                {
                    Debug.Print(DateTime.Now.TimeOfDay + ": CSettings.CRoaming.Load() error: " + ex.Message);

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
                ReadElementBool(xroot, "beeponinputerror", ref BeepOnInputError);
                ReadElementBool(xroot, "autonextrowonenter", ref AutoNextRowOnEnter);
                ReadElementBool(xroot, "autostartedit", ref AutoStartEdit);
                ReadElementBool(xroot, "ignoresizeonreload", ref IgnoreSizeOnReload);
                ReadElementBool(xroot, "multilineautosizerows", ref MultilineAutosizeRows);
                ReadElementBool(xroot, "clearfilterontabchange", ref ClearFilterOnTabChange);
                ReadElementBool(xroot, "showvarproperties", ref ShowVarProperties);
                ReadElementBool(xroot, "mbcfilevalidationenable", ref MbcfileValidationEnable);
                ReadElementString(xroot, "mbcfilevalidationxsdfile", ref MbcfileValidationXSDFile);
                ReadElementBool(xroot, "showfindfilterbox", ref ShowFindFilterBox);
                ReadElementBool(xroot, "customeditoreenable", ref CustomEditorEnable);
                ReadElementString(xroot, "customeditorcommand", ref CustomEditorCommand);
                ReadElementStringList(xroot, "mbcmrulist", "file", ref MbcMruList);
                ReadElementMainColumnWidths(xroot, "columnwidths", ref ColumnWidths);
                Export.Read(xroot);
                Import.Read(xroot);
                CustomQueries.Read(xroot);
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

                Debug.Print(DateTime.Now.TimeOfDay + ": CSettings.CRoaming.Save: " + FilePath);

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
                WriteElementBool(xroot, "beeponinputerror", BeepOnInputError);
                WriteElementBool(xroot, "autonextrowonenter", AutoNextRowOnEnter);
                WriteElementBool(xroot, "autostartedit", AutoStartEdit);
                WriteElementBool(xroot, "ignoresizeonreload", IgnoreSizeOnReload);
                WriteElementBool(xroot, "multilineautosizerows", MultilineAutosizeRows);
                WriteElementBool(xroot, "clearfilterontabchange", ClearFilterOnTabChange);
                WriteElementBool(xroot, "showvarproperties", ShowVarProperties);
                WriteElementBool(xroot, "mbcfilevalidationenable", MbcfileValidationEnable);
                WriteElementString(xroot, "mbcfilevalidationxsdfile", MbcfileValidationXSDFile);
                WriteElementBool(xroot, "showfindfilterbox", ShowFindFilterBox);
                WriteElementBool(xroot, "customeditoreenable", CustomEditorEnable);
                WriteElementString(xroot, "customeditorcommand", CustomEditorCommand);
                WriteElementStringList(xroot, "mbcmrulist", "file", MbcMruList);
                WriteElementMainColumnWidths(xroot, "columnwidths", ColumnWidths);
                Export.Write(xroot);
                Import.Write(xroot);
                CustomQueries.Write(xroot);                

                // Write file
                // no UTF8 BOM, indented
                using (XmlTextWriter xtw = new XmlTextWriter(FilePath, new UTF8Encoding(false)))
                {
                    xtw.Formatting = Formatting.Indented;
                    xdoc.WriteContentTo(xtw);
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
            public Version LastRunVersion;

            private XmlDocument xdoc;
            private XmlNode xroot;
            
            // constructor
            public CShared()
            {
                // sets defaults
                Updater = new CUpdater();
                LastRunVersion = new Version(0, 0, 0, 0);
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
                    Debug.Print(DateTime.Now.TimeOfDay + ": CSettings.CShared.Load(): " + FilePath);

                    xdoc = new XmlDocument();
                    xdoc.Load(FilePath);
                }
                catch (Exception ex)
                {
                    Debug.Print(DateTime.Now.TimeOfDay + ": CSettings.CShared.Load() error: " + ex.Message);

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
                ReadElementVersion(xroot, "lastrunversion", ref LastRunVersion);
            }

            /// <summary>
            /// Save settings to the xml file
            /// Throws exception on error
            /// </summary>
            public void Save()
            {
                Debug.Print(DateTime.Now.TimeOfDay + ": CSettings.CShared.Save: " + FilePath);

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
                WriteElementVersion(xroot, "lastrunversion", LastRunVersion);

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
            public string ExportStringAs;
            public bool ContinueOnError;

            // constructor
            public CExport()
            {
                // sets defaults
                Tabs = "current";
                ExportFormat = "default";
                ExportStringAs = "string";
                ContinueOnError = false;
            }

            public void Read(XmlNode xparent)
            {
                XmlNode xn = xparent.SelectSingleNode("export");
                if (xn != null)
                {
                    ReadElementString(xn, "tabs", ref Tabs);
                    ReadElementString(xn, "exportformat", ref ExportFormat);
                    ReadElementString(xn, "exportstringas", ref ExportStringAs);
                    ReadElementBool(xn, "continueonerror", ref ContinueOnError);
                }
            }

            public void Write(XmlNode xparent)
            {
                XmlNode xn = WriteParentElement(xparent, "export");
                WriteElementString(xn, "tabs", Tabs);
                WriteElementString(xn, "exportformat", ExportFormat);
                WriteElementString(xn, "exportstringas", ExportStringAs);
                WriteElementBool(xn, "continueonerror", ContinueOnError);
            }
        }

        public class CImport
        {
            public bool ContinueOnError;

            // constructor
            public CImport()
            {
                // sets defaults
                ContinueOnError = false;
            }

            public void Read(XmlNode xparent)
            {
                XmlNode xn = xparent.SelectSingleNode("import");
                if (xn != null)
                {
                    ReadElementBool(xn, "continueonerror", ref ContinueOnError);
                }
            }

            public void Write(XmlNode xparent)
            {
                XmlNode xn = WriteParentElement(xparent, "import");
                WriteElementBool(xn, "continueonerror", ContinueOnError);
            }
        }


        public class CCustomQueries
        {
            public int HoldingRegistersStartAddress;    // 40_000, 40_001, 400_000, 400_001
            public bool MultilineAutosizeRows;
            public List<CustomQueries.CustomQueryItem> QueriesList;
            public CustomQueries.ColumnWidthsType ColumnWidths;

            // constructor
            public CCustomQueries()
            {
                // set defaults

                HoldingRegistersStartAddress = 40001;
                MultilineAutosizeRows = false;

                // columns widths
                ColumnWidths = new CustomQueries.ColumnWidthsType
                {
                    sendWidth = 50,
                    slaveIDWidth = 28,
                    commandWidth = 99,
                    registerWidth = 43,
                    qtyWidth = 34,
                    formatWidth = 51,
                    dataWidth = 123,
                    resultWidth = 277,
                    commentWidth = 112
                };

                // demo queries

                QueriesList = new List<CustomQueries.CustomQueryItem>();

                CustomQueries.CustomQueryItem q = new CustomQueries.CustomQueryItem();
                q.slaveID = "1";
                q.command = "RIR";
                q.register = "30001";
                q.qty = "10";
                q.format = "INT16";
                q.data = "";
                q.comment = "demo";
                QueriesList.Add(q);

                q = new CustomQueries.CustomQueryItem();
                q.slaveID = "1";
                q.command = "RHR";
                q.register = "40001";
                q.qty = "10";
                q.format = "INT16";
                q.data = "";
                q.comment = "";
                QueriesList.Add(q);

                q = new CustomQueries.CustomQueryItem();
                q.slaveID = "1";
                q.command = "WSHR";
                q.register = "40001";
                q.qty = "1";
                q.format = "INT16";
                q.data = "1001";
                q.comment = "";
                QueriesList.Add(q);

                q = new CustomQueries.CustomQueryItem();
                q.slaveID = "1";
                q.command = "WMHR";
                q.register = "40001";
                q.qty = "4";
                q.format = "INT16";
                q.data = "2001, 2002, 2003, 2004";
                q.comment = "";
                QueriesList.Add(q);

                q = new CustomQueries.CustomQueryItem();
                q.slaveID = "1";
                q.command = "RDI";
                q.register = "10001";
                q.qty = "32";
                q.format = "BITS";
                q.data = "";
                q.comment = "";
                QueriesList.Add(q);

                q = new CustomQueries.CustomQueryItem();
                q.slaveID = "1";
                q.command = "RC";
                q.register = "1";
                q.qty = "32";
                q.format = "HEXBYTES";
                q.data = "";
                q.comment = "";
                QueriesList.Add(q);

                q = new CustomQueries.CustomQueryItem();
                q.slaveID = "1";
                q.command = "WSC";
                q.register = "1";
                q.qty = "32";
                q.format = "BITS";
                q.data = "1";
                q.comment = "";
                QueriesList.Add(q);

                q = new CustomQueries.CustomQueryItem();
                q.slaveID = "1";
                q.command = "WMC";
                q.register = "1";
                q.qty = "16";
                q.format = "BITS";
                q.data = "1,1,1,1,0,0,0,0,1,1,0,1,0,0,1,0";
                q.comment = "";
                QueriesList.Add(q);

                q = new CustomQueries.CustomQueryItem();
                q.slaveID = "1";
                q.command = "CUSTQ";
                q.register = "";
                q.qty = "";
                q.format = "HEXBYTES";
                q.data = "0x01,0x03,0x00,0x20,0x00,0x10";
                q.comment = "";
                QueriesList.Add(q);
            }

            public void Read(XmlNode xparent)
            {  
                XmlNode xn = xparent.SelectSingleNode("customqueries");
                if (xn != null)
                {
                    ReadElementInt(xn, "holdingregistersstart", ref HoldingRegistersStartAddress);
                    ReadElementBool(xn, "multilineautosizerows", ref MultilineAutosizeRows);

                    QueriesList.Clear();
                    XmlNode xc = xn.SelectSingleNode("queries");
                    if (xc != null)
                    {
                        XmlNodeList xcl = xc.SelectNodes("query");
                        if (xcl != null)
                        {
                            foreach (XmlNode c in xcl)
                            {
                                CustomQueries.CustomQueryItem nc = new CustomQueries.CustomQueryItem();
                                ReadElementString(c, "slaveid", ref nc.slaveID);
                                ReadElementString(c, "command", ref nc.command);
                                ReadElementString(c, "register", ref nc.register);
                                ReadElementString(c, "qty", ref nc.qty);
                                ReadElementString(c, "format", ref nc.format);
                                ReadElementString(c, "data", ref nc.data);
                                ReadElementString(c, "comment", ref nc.comment);
                                QueriesList.Add(nc);
                            }
                        }
                    }
                    
                    XmlNode cwn = xn.SelectSingleNode("columnwidths");
                    if (cwn != null)
                    {
                        ReadElementFloat(cwn, "send", ref ColumnWidths.sendWidth);
                        ReadElementFloat(cwn, "slaveid", ref ColumnWidths.slaveIDWidth);
                        ReadElementFloat(cwn, "command", ref ColumnWidths.commandWidth);
                        ReadElementFloat(cwn, "register", ref ColumnWidths.registerWidth);
                        ReadElementFloat(cwn, "qty", ref ColumnWidths.qtyWidth);
                        ReadElementFloat(cwn, "format", ref ColumnWidths.formatWidth);
                        ReadElementFloat(cwn, "data", ref ColumnWidths.dataWidth);
                        ReadElementFloat(cwn, "result", ref ColumnWidths.resultWidth);
                        ReadElementFloat(cwn, "comment", ref ColumnWidths.commentWidth);
                    }
                }
            }

            public void Write(XmlNode xparent)
            {
                XmlNode xn = WriteParentElement(xparent, "customqueries");
                WriteElementInt(xn, "holdingregistersstart", HoldingRegistersStartAddress);
                WriteElementBool(xn, "multilineautosizerows", MultilineAutosizeRows);
                WriteElementCustomQueriesQueriesList(xn, "queries", QueriesList);
                WriteElementCustomQueriesColumnWidths(xn, "columnwidths", ColumnWidths);
            }
        }

        public class CLogger
        {
            // settings
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
                XmlNode xn = xparent.SelectSingleNode("update");
                if (xn != null)
                {
                    ReadElementBool(xn, "checkforupdates", ref CheckForUpdates);
                    ReadElementDatetime(xn, "lastcheckforupdate", ref LastCheckForUpdate);
                }
            }

            public void Write(XmlNode xparent)
            {
                XmlNode xn = WriteParentElement(xparent, "update");
                WriteElementBool(xn, "checkforupdates", CheckForUpdates);
                WriteElementDatetime(xn, "lastcheckforupdate", LastCheckForUpdate);
            }
        }

        // class used to save/restore column widths
        public class ColumnWidthsType
        {
            public float registerWidthWeight;
            public float descriptionWidthWeight;
            public float propertiesWidthWeight;
            public float valueWidthWeight;
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

        private static void ReadElementFloat(XmlNode xparent, string name, ref float o)
        {
            XmlNode xn = xparent.SelectSingleNode(name);
            if (xn != null)
            {
                float tmpfloat;
                if (float.TryParse(xn.InnerText, out tmpfloat))
                {
                    o = tmpfloat;
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

        private static void ReadElementStringList(XmlNode xparent, string parentname, string name, ref List<string> strlist)
        {
            XmlNode xn = xparent.SelectSingleNode(parentname);
            if (xn != null)
            {
                strlist.Clear();
                XmlNodeList xl = xn.SelectNodes(name);
                if (xl != null)
                foreach (XmlNode ln in xl)
                {
                    strlist.Add(ln.InnerText);
                }
            }
        }

        private static void ReadElementMainColumnWidths(XmlNode xparent, string parentname, ref ColumnWidthsType colw)
        {
            XmlNode xn = xparent.SelectSingleNode(parentname);
            if (xn != null)
            {
                ReadElementFloat(xn, "register", ref colw.registerWidthWeight);
                ReadElementFloat(xn, "descripton", ref colw.descriptionWidthWeight);
                ReadElementFloat(xn, "properties", ref colw.propertiesWidthWeight);
                ReadElementFloat(xn, "value", ref colw.valueWidthWeight);
            }
        }


        private static void ReadElementVersion(XmlNode xparent, string name, ref Version v)
        {
            XmlNode xn = xparent.SelectSingleNode(name);
            if (xn != null)
            {
                v = Version.Parse(xn.InnerText);
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

        private static XmlNode WriteElementFloat(XmlNode parent, string name, float value)
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

        private static XmlNode WriteElementVersion(XmlNode parent, string name, Version v)
        {
            return WriteElementString(parent, name, v.ToString());
        }

        private static void WriteElementCustomQueriesQueriesList(XmlNode parent, string parentName, List<CustomQueries.CustomQueryItem> queriesList)
        {
            // do not save if queriesList is null
            if (queriesList != null)
            {
                XmlDocument xdoc = parent.OwnerDocument;
                XmlNode xpar = WriteParentElement(parent, parentName);
                xpar.RemoveAll();
                foreach (CustomQueries.CustomQueryItem q in queriesList)
                {
                    XmlNode xcmd = xdoc.CreateElement("query");
                    WriteElementString(xcmd, "slaveid", q.slaveID);
                    WriteElementString(xcmd, "command", q.command);
                    WriteElementString(xcmd, "register", q.register);
                    WriteElementString(xcmd, "qty", q.qty);
                    WriteElementString(xcmd, "format", q.format);
                    WriteElementString(xcmd, "data", q.data);
                    WriteElementString(xcmd, "comment", q.comment);
                    xpar.AppendChild(xcmd);
                }
            }
        }


        private static void WriteElementCustomQueriesColumnWidths(XmlNode parent, string parentName, CustomQueries.ColumnWidthsType columnWidths)
        {
            XmlDocument xdoc = parent.OwnerDocument;
            XmlNode xcw = WriteParentElement(parent, parentName);
            xcw.RemoveAll();
            WriteElementFloat(xcw, "send", columnWidths.sendWidth);
            WriteElementFloat(xcw, "slaveid", columnWidths.slaveIDWidth);
            WriteElementFloat(xcw, "command", columnWidths.commandWidth);
            WriteElementFloat(xcw, "register", columnWidths.registerWidth);
            WriteElementFloat(xcw, "qty", columnWidths.qtyWidth);
            WriteElementFloat(xcw, "format", columnWidths.formatWidth);
            WriteElementFloat(xcw, "data", columnWidths.dataWidth);
            WriteElementFloat(xcw, "result", columnWidths.resultWidth);
            WriteElementFloat(xcw, "comment", columnWidths.commentWidth);
        }

        private static void WriteElementMainColumnWidths(XmlNode parent, string parentName, ColumnWidthsType columnWidths)
        {
            if (columnWidths != null)
            {
                XmlDocument xdoc = parent.OwnerDocument;
                XmlNode xcw = WriteParentElement(parent, parentName);
                xcw.RemoveAll();
                WriteElementFloat(xcw, "register", columnWidths.registerWidthWeight);
                WriteElementFloat(xcw, "descripton", columnWidths.descriptionWidthWeight);
                WriteElementFloat(xcw, "properties", columnWidths.propertiesWidthWeight);
                WriteElementFloat(xcw, "value", columnWidths.valueWidthWeight);
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
