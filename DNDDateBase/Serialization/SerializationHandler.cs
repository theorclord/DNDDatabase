using DNDDateBase.Utility;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace DNDDateBase.Serialization
{
    public class SerializationHandler
    {
        public static readonly string DefaultSaveFilePath = @"..\..\..\DNDDateBase\DefaultSavedData.xml";

        public static void SaveDataToFile(SerializedDataContainer data, string SaveFilePath)
        {
            string serialData = JsonConvert.SerializeObject(data);
            // Save the data to the file
            XmlSerializer serializer = new XmlSerializer(typeof(SerializedDataContainer));
            TextWriter writer = new StreamWriter(SaveFilePath);
            serializer.Serialize(writer, data);
            writer.Close();
        }

        public static SerializedDataContainer LoadData(string filePathName)
        {
            SerializedDataContainer appData = null;
            if (File.Exists(filePathName))
            {
                XmlSerializer deserializer = new XmlSerializer(typeof(SerializedDataContainer));

                StreamReader dataReader = new StreamReader(filePathName);
                XmlReader xmlDataReader = new XmlTextReader(dataReader);

                if (deserializer.CanDeserialize(xmlDataReader))
                {
                    appData = (SerializedDataContainer)deserializer.Deserialize(xmlDataReader);
                }
                dataReader.Close();
                xmlDataReader.Close();
            }
            return appData;
        }
    }
}
