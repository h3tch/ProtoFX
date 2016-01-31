using System;
using System.IO;

namespace App
{
    class XmlSerializer
    {
        /// <summary>
        /// Serializes an object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="fileName"></param>
        static public void Save<T>(T obj, string fileName)
        {
            if (obj == null)
                return;
            
            var xmlDocument = new System.Xml.XmlDocument();
            var serializer = new System.Xml.Serialization.XmlSerializer(obj.GetType());
            using (MemoryStream stream = new MemoryStream())
            {
                serializer.Serialize(stream, obj);
                stream.Position = 0;
                xmlDocument.Load(stream);
                xmlDocument.Save(fileName);
                stream.Close();
            }
        }

        /// <summary>
        /// Deserializes an xml file into an object list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName"></param>
        /// <returns></returns>
        static public T Load<T>(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                return default(T);

            T objectOut = default(T);
            
            string attributeXml = string.Empty;

            var xmlDocument = new System.Xml.XmlDocument();
            xmlDocument.Load(fileName);
            string xmlString = xmlDocument.OuterXml;

            using (var read = new StringReader(xmlString))
            {
                Type outType = typeof(T);

                var serializer = new System.Xml.Serialization.XmlSerializer(outType);
                using (var reader = new System.Xml.XmlTextReader(read))
                {
                    objectOut = (T)serializer.Deserialize(reader);
                    reader.Close();
                }

                read.Close();
            }

            return objectOut;
        }

    }
}
