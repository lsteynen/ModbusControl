using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace ModbusControlSpace
{
    [XmlRoot("modbuscontrol")]
    public class Settings
    {
        // public
        // the names of the public variables are also the XML tag names

        [XmlRoot("modbuscontrol")]
        public class CValues
        {
            [XmlElement("stoponerror")]
            public bool StopOnError
            { get; set; }

            [XmlElement("uselocaloverrides")]
            public bool UseLocalOverrides
            { get; set; }

            [XmlElement("beeponinputerror")]
            public bool BeepOnInputError
            { get; set; }

            [XmlElement("autonextrowonenter")]
            public bool AutoNextRowOnEnter
            { get; set; }

            [XmlElement("autostartedit")]
            public bool AutoStartEdit
            { get; set; }

            [XmlElement("customeditoreenable")]
            public bool CustomEditorEnable
            { get; set; }

            [XmlElement("customeditorcommand")]
            public string CustomEditorCommand
            { get; set; }

            [XmlElement("checkforupdates")]
            public bool CheckForUpdates
            { get; set; }

            [XmlElement("lastcheckforupdate")]
            public DateTime LastCheckForUpdate
            { get; set; }

            [XmlArray("mbcmrulist")]
            [XmlArrayItem("file")]
            public List<string> MbcMruList
            { get; set; }

            [XmlElement("export")]
            public CExport Export
            { get; set; }

            [XmlElement("compare")]
            public CCompare Compare
            { get; set; }

            [XmlElement("customread")]
            public CCustomRead CustomRead
            { get; set; }

            [XmlElement("customwrite")]
            public CCustomWrite CustomWrite
            { get; set; }

            [XmlElement("customcommand")]
            public CCustomCommand CustomCommand
            { get; set; }

            [XmlElement("logger")]
            public CLoggerValues Logger
            { get; set; }

            // constructor
            public CValues()
            {
                // sets default values
                StopOnError = false;
                UseLocalOverrides = true;
                BeepOnInputError = true;
                AutoNextRowOnEnter = false;
                AutoStartEdit = false;
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
            [XmlElement("tabs")]
            public string Tabs
            { get; set; }

            [XmlElement("exportformat")]
            public string ExportFormat
            { get; set; }

            [XmlElement("exportasciias")]
            public string ExportAsciiAs
            { get; set; }

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
            [XmlElement("showequal")]
            public bool ShowEqual
            { get; set; }

            // constructor
            public CCompare()
            {
                // sets default values
                ShowEqual = true;
            }
        }

        public class CCustomRead
        {
            [XmlElement("slaveid")]
            public int SlaveID
            { get; set; }

            [XmlElement("register")]
            public int Register
            { get; set; }

            [XmlElement("quantity")]
            public int Quantity
            { get; set; }

            [XmlElement("datasize")]
            public string Datasize
            { get; set; }

            [XmlElement("showvaluesas")]
            public string ShowValuesAs
            { get; set; }

            // constructor
            public CCustomRead()
            {
                // sets default values
                SlaveID = 1;
                Register = 40001;
                Quantity = 1;
                Datasize = "16";
                ShowValuesAs = "int";
            }
        }

        public class CCustomWrite
        {
            [XmlElement("slaveid")]
            public int SlaveID
            { get; set; }

            [XmlElement("register")]
            public int Register
            { get; set; }

            [XmlElement("quantity")]
            public int Quantity
            { get; set; }

            [XmlElement("functioncode")]
            public string Functioncode
            { get; set; }

            [XmlElement("datasize")]
            public string Datasize
            { get; set; }

            [XmlElement("data")]
            public string Data
            { get; set; }

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
            [XmlElement("command")]
            public string Command
            { get; set; }

            // constructor
            public CCustomCommand()
            {
                // sets default values
                Command = "01 03 00 00 00 0A";
            }
        }

        public class CLoggerValues
        {
            [XmlElement("logerrors")]
            public bool LogErrors
            { get; set; }

            [XmlElement("logwarnings")]
            public bool LogWarnings
            { get; set; }

            [XmlElement("loginfo")]
            public bool LogInfo
            { get; set; }

            [XmlElement("logtrace")]
            public bool LogTrace
            { get; set; }

            [XmlElement("logreads")]
            public bool LogReads
            { get; set; }

            [XmlElement("logwrites")]
            public bool LogWrites
            { get; set; }

            [XmlElement("logbridgeinfo")]
            public bool LogBridgeInfo
            { get; set; }

            [XmlElement("logbridgedata")]
            public bool LogBridgeData
            { get; set; }

            [XmlElement("logbridgedecode")]
            public bool LogBridgeDecode
            { get; set; }

            [XmlElement("capacity")]
            public int Capacity
            { get; set; }

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
        /// </summary>
        public void Load()
        {
            StreamReader sr = null;

            // Create the directory tree if it doesn't exists yet
            Directory.CreateDirectory(Path.GetDirectoryName(StorePath));

            // Load the stored settings
            // Do not throw exception if file does not exist, that's normal on first use
            try
            {
                XmlSerializer xs = new XmlSerializer(typeof(CValues));
                sr = new StreamReader(StorePath);
                Values = xs.Deserialize(sr) as CValues;
            }
            catch
            {
                // On exception
                // Create a Values object with defaults
                Values = new CValues();
            }
            finally
            {
                if( sr != null) sr.Close();
            }
        }

        /// <summary>
        /// Save settings to the xml file
        /// </summary>
        public void Save()
        {
            using (TextWriter writer = new System.IO.StreamWriter(StorePath))
            {
                XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                ns.Add("", ""); // avoid namespace in XML file
                XmlSerializer serializer = new XmlSerializer(typeof(CValues));
                serializer.Serialize(writer, Values, ns);
                writer.Flush();
            }
        }
    }
}
