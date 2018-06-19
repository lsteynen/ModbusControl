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
            public bool stoponerror
            { get; set; }

            [XmlElement("uselocaloverrides")]
            public bool uselocaloverrides
            { get; set; }

            [XmlElement("beeponinputerror")]
            public bool beeponinputerror
            { get; set; }

            [XmlElement("checkforupdates")]
            public bool checkforupdates
            { get; set; }

            [XmlElement("lastcheckforupdate")]
            public DateTime lastcheckforupdate
            { get; set; }

            [XmlArray("mbcmrulist")]
            [XmlArrayItem("file")]
            public List<string> mbcmrulist
            { get; set; }

            [XmlElement("export")]
            public CExport export
            { get; set; }

            [XmlElement("compare")]
            public CCompare compare
            { get; set; }

            [XmlElement("customread")]
            public CCustomRead customread
            { get; set; }

            [XmlElement("customwrite")]
            public CCustomWrite customwrite
            { get; set; }

            [XmlElement("customcommand")]
            public CCustomCommand customcommand
            { get; set; }

            [XmlElement("logger")]
            public CLoggerValues logger
            { get; set; }

            // constructor
            public CValues()
            {
                // sets default values
                stoponerror = false;
                uselocaloverrides = true;
                beeponinputerror = true;
                checkforupdates = true;
                lastcheckforupdate = DateTime.MinValue;
                mbcmrulist = new List<string>();
                export = new CExport();
                compare = new CCompare();
                customread = new CCustomRead();
                customwrite = new CCustomWrite();
                customcommand = new CCustomCommand();
                logger = new CLoggerValues();
            }
        };

        public class CExport
        {
            [XmlElement("tabs")]
            public string tabs
            { get; set; }

            [XmlElement("exportformat")]
            public string exportformat
            { get; set; }

            [XmlElement("exportasciias")]
            public string exportasciias
            { get; set; }

            // constructor
            public CExport()
            {
                // sets default values
                tabs = "current";
                exportformat = "grid";
                exportasciias = "ascii";
            }
        }

        public class CCompare
        {
            [XmlElement("showequal")]
            public bool showequal
            { get; set; }

            // constructor
            public CCompare()
            {
                // sets default values
                showequal = true;
            }
        }

        public class CCustomRead
        {
            [XmlElement("slaveid")]
            public int slaveid
            { get; set; }

            [XmlElement("register")]
            public int register
            { get; set; }

            [XmlElement("quantity")]
            public int quantity
            { get; set; }

            [XmlElement("datasize")]
            public string datasize
            { get; set; }

            [XmlElement("showvaluesas")]
            public string showvaluesas
            { get; set; }

            // constructor
            public CCustomRead()
            {
                // sets default values
                slaveid = 1;
                register = 40001;
                quantity = 1;
                datasize = "16";
                showvaluesas = "int";
            }
        }

        public class CCustomWrite
        {
            [XmlElement("slaveid")]
            public int slaveid
            { get; set; }

            [XmlElement("register")]
            public int register
            { get; set; }

            [XmlElement("quantity")]
            public int quantity
            { get; set; }

            [XmlElement("functioncode")]
            public string functioncode
            { get; set; }

            [XmlElement("datasize")]
            public string datasize
            { get; set; }

            // constructor
            public CCustomWrite()
            {
                // sets default values
                slaveid = 1;
                register = 40001;
                quantity = 1;
                functioncode = "16";
                datasize = "16";
            }
        }

        public class CCustomCommand
        {
            [XmlElement("command")]
            public string command
            { get; set; }

            // constructor
            public CCustomCommand()
            {
                // sets default values
                command = "01 03 00 00 00 0A";
            }
        }

        public class CLoggerValues
        {
            [XmlElement("logerrors")]
            public bool logerrors
            { get; set; }

            [XmlElement("logwarnings")]
            public bool logwarnings
            { get; set; }

            [XmlElement("loginfo")]
            public bool loginfo
            { get; set; }

            [XmlElement("logtrace")]
            public bool logtrace
            { get; set; }

            [XmlElement("logreads")]
            public bool logreads
            { get; set; }

            [XmlElement("logwrites")]
            public bool logwrites
            { get; set; }

            [XmlElement("logbridgeinfo")]
            public bool logbridgeinfo
            { get; set; }

            [XmlElement("logbridgedata")]
            public bool logbridgedata
            { get; set; }

            [XmlElement("logbridgedecode")]
            public bool logbridgedecode
            { get; set; }

            [XmlElement("capacity")]
            public int capacity
            { get; set; }

            // constructor
            public CLoggerValues()
            {
                // sets default values
                logerrors = true;
                logwarnings = true;
                loginfo = true;
                logtrace = false;
                logreads = false;
                logwrites = false;
                logbridgeinfo = false;
                logbridgedata = false;
                logbridgedecode = false;
                capacity = 5000;
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
