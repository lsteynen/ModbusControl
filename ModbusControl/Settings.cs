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

            [XmlElement("loggercapacity")]
            public int loggercapacity
            { get; set; }

            [XmlArray("mbcmrulist")]
            [XmlArrayItem("file")]
            public List<string> mbcmrulist
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
                loggercapacity = 5000;
                mbcmrulist = new List<string>();
            }
        };

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
