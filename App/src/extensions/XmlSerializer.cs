using System.IO;

namespace System.Xml
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
            
            var xmlDocument = new XmlDocument();
            var serializer = new Serialization.XmlSerializer(obj.GetType());
            using (var stream = new MemoryStream())
            {
                serializer.Serialize(stream, obj);
                stream.Position = 0;
                xmlDocument.Load(stream);
                xmlDocument.Save(fileName);
            }
        }

        /// <summary>
        /// Deserializes an xml file into an object list.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName"></param>
        /// <returns></returns>
        static public T Load<T>(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                return default(T);

            var objectOut = default(T);
            var attributeXml = string.Empty;

            // load xml file
            var xmlDocument = new XmlDocument();
            xmlDocument.Load(fileName);
            var xmlString = xmlDocument.OuterXml;

            // deserialize string to object
            var serializer = new Serialization.XmlSerializer(typeof(T));
            using (var reader = new XmlTextReader(new StringReader(xmlString)))
            {
                objectOut = (T)serializer.Deserialize(reader);
            }

            return objectOut;
        }
    }
}
