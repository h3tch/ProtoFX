using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Xml;

namespace App
{
    class DataXml
    {
        #region PROPERTIES
        public byte[] data { get; protected set; }
        #endregion

        #region FIELDS
        private static Dictionary<string, Type> str2type = new Dictionary<string, Type>
        {
            {"bool"    , typeof(bool)},
            {"byte"    , typeof(byte)},
            {"sbyte"   , typeof(sbyte)},
            {"char"    , typeof(char)},
            {"decimal" , typeof(decimal)},
            {"double"  , typeof(double)},
            {"float"   , typeof(float)},
            {"int"     , typeof(int)},
            {"uint"    , typeof(uint)},
            {"long"    , typeof(long)},
            {"ulong"   , typeof(ulong)},
            {"object"  , typeof(object)},
            {"short"   , typeof(short)},
            {"ushort"  , typeof(ushort)}
        };
        #endregion

        public static byte[] Load(string filename, string itemname)
        {
            string errstr = "XML '" + filename + "' " + itemname + ": ";

            // load XML file
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(filename);
            // rood node must be <gled>
            if (!xmlDoc.DocumentElement.Name.Equals("data"))
                throw new Exception(errstr + "Root node must be <data>...</data>.");

            // find element by name
            var list = xmlDoc.DocumentElement.GetElementsByTagName(itemname);
            if (list.Count == 0)
                throw new Exception(errstr + "Element '" + itemname + "' could not be found.");
            var item = list[0];

            // get type and values of the element
            Type type;
            Array values;
            // the values are in binary format
            if (item.Attributes["isbinary"] != null && item.Attributes["isbinary"].Value.Equals("true"))
            {
                type = typeof(char);
                values = item.InnerText.ToCharArray();
            }
            // the values are given as strings and have to be converted
            else
            {
                // get value type and check for errors
                if (item.Attributes["type"] == null)
                    throw new Exception(errstr + "For non binary data a type has to be specified "
                        + "(e.g. <" + itemname + " type='float'>).");

                if (!str2type.TryGetValue(item.Attributes["type"].Value, out type))
                    throw new Exception(errstr + "Type '" + item.Attributes["type"].Value + "' not supported.");

                // convert values
                var raw = Regex.Matches(item.InnerText, "(\\+|\\-)?[0-9\\.\\,]+");
                values = Array.CreateInstance(type, raw.Count);
                for (var i = 0; i < values.Length; i++)
                    values.SetValue(Convert.ChangeType(raw[i].Value, type, App.culture), i);
            }

            // convert to byte array
            var data = new byte[values.Length * Marshal.SizeOf(type)];
            Buffer.BlockCopy(values, 0, data, 0, data.Length);
            return data;
        }
    }
}
