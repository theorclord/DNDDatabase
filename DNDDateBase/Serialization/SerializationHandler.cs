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
        public static void SaveDataToFile(SerializedDataContainer data, string SaveFilePath)
        {
            // Save the Data as JSON
            string serialData = JsonConvert.SerializeObject(data);
            using(TextWriter jSonWriter = new StreamWriter(SaveFilePath))
            {
                jSonWriter.Write(serialData);
            }
        }

        public static SerializedDataContainer LoadData(string filePathName)
        {
            SerializedDataContainer appData = null;
            if (File.Exists(filePathName))
            {
                string jsonString = "";
                using (TextReader reader = new StreamReader(filePathName))
                {
                    jsonString = reader.ReadToEnd();
                }
                appData = JsonConvert.DeserializeObject<SerializedDataContainer>(jsonString);
            }
            return appData;
        }

        public static string GetDefaultSavePath()
        {
            string path = AppContext.BaseDirectory + @"\Saves\DefaultSave.json";
            return path;
        }
    }
}
