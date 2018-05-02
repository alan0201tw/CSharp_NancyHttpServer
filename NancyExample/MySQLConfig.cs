using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Serialization;

namespace NancyExample
{
    public class MySQLConfig
    {
        private static MySQLConfig instance;
        public static MySQLConfig Instance { get { return instance; } }

        public static void InitialConfiguration(MySQLConfig configuration)
        {
            instance = configuration;
        }
        
        [XmlElement]
        public string DatabaseHostAddress { get; set; }
        [XmlElement]
        public string DatabaseUsername { get; set; }
        [XmlElement]
        public string DatabasePassword { get; set; }
        [XmlElement]
        public string Database { get; set; }

        public MySQLConfig() { }
        public static bool Load(string filePath, out MySQLConfig configuration)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(MySQLConfig));
            if (File.Exists(filePath))
            {
                using (XmlReader reader = XmlReader.Create(filePath))
                {
                    if (serializer.CanDeserialize(reader))
                    {
                        configuration = (MySQLConfig)serializer.Deserialize(reader);
                        return true;
                    }
                    else
                    {
                        configuration = null;
                        return false;
                    }
                }
            }
            else
            {
                MySQLConfig versionConfiguration = new MySQLConfig
                {
                    DatabaseHostAddress = "not set",
                    DatabaseUsername = "not set",
                    DatabasePassword = "not set",
                    Database = "not set",
                };
                using (XmlWriter writer = XmlWriter.Create(filePath))
                {
                    serializer.Serialize(writer, versionConfiguration);
                }
                configuration = versionConfiguration;
                return true;
            }
        }
    }

}