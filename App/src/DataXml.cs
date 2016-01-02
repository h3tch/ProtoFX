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
                // find all nodes with the specified item name
                var nodes = xmlDoc.SelectNodes(itemname);
                // allocate output array
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
                            throw new CompileException($"{errstr}For non binary data a type has "
                                + $" to be specified(e.g. <{itemname} type='float'>).");
                        
                        // convert type name to Type
                        type = Extensions.str2type[attr["type"].Value];
                        if (type == null)
                            throw new CompileException($"{errstr}Type '{attr["type"].Value}' not supported.");

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

                // join the data of all nodes
                return data.Join().ToArray();
            }
            catch
            {
                throw new CompileException($"{errstr}Could not load item.");
            }
        }
    }
}
