using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Xml;

namespace App
{
    class DataXml
    {
        public static byte[] Load(XmlDocument xmlDoc, string itemname)
        {
            string errstr = $"<{itemname}>: ";

            try
            {
                var nodes = xmlDoc.SelectNodes(itemname);
                var data = new byte[nodes.Count][];
                int i = 0;

                foreach (XmlNode node in nodes)
                {
                    // get type and values of the element
                    Type type;
                    Array values;

                    // the values are in binary format
                    var attr = node.Attributes;
                    if (attr?["isbinary"].Value.Equals("true") ?? false)
                    {
                        type = typeof(char);
                        values = node.InnerText.ToCharArray();
                    }
                    // the values are given as strings and have to be converted
                    else
                    {
                        // get value type and check for errors
                        if (attr["type"] == null)
                            throw new GLException($"{errstr}For non binary data a type has to be "
                                + $" specified(e.g. <{itemname} type='float'>).");
                        
                        type = Data.str2type[attr["type"].Value];
                        if (type == null)
                            throw new GLException($"{errstr}Type '{attr["type"].Value}' not supported.");

                        // convert values
                        var raw = Regex.Matches(node.InnerText, "(\\+|\\-)?[0-9\\.\\,]+");
                        values = Array.CreateInstance(type, raw.Count);
                        for (var j = 0; j < values.Length; j++)
                            values.SetValue(Convert.ChangeType(raw[j].Value, type, App.culture), j);
                    }

                    // convert to byte array
                    data[i] = new byte[values.Length * Marshal.SizeOf(type)];
                    Buffer.BlockCopy(values, 0, data[i], 0, data[i].Length);
                    i++;
                }

                return data.Join().ToArray();
            }
            catch
            {
                throw new GLException($"{errstr}Could not load item.");
            }
        }

        public static byte[] Load(string filename, string itemname)
        {
            // load XML file
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(filename);
            return Load(xmlDoc, itemname);
        }
    }
}
