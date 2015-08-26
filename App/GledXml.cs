using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;

namespace gled
{
    class GledXml
    {
        public byte[] data { get; protected set; }

        private static CultureInfo culture = new CultureInfo("en");

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

        public GledXml (string filename, string itemname)
        {
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(filename);
            if (!xmlDoc.DocumentElement.Name.Equals("gled"))
                throw new Exception("");

            var list = xmlDoc.DocumentElement.GetElementsByTagName(itemname);
            if (list.Count == 0)
                throw new Exception("");
            var item = list[0];

            Type type;
            Array values;
            if (item.Attributes["isbinary"] != null && item.Attributes["isbinary"].Value.Equals("true"))
            {
                type = typeof(char);
                values = item.InnerText.ToCharArray();
            }
            else
            {
                if (item.Attributes["type"] == null)
                    throw new Exception("");
                if (!str2type.TryGetValue(item.Attributes["type"].Value, out type))
                    throw new Exception("");
                var raw = Regex.Matches(item.InnerText, "(\\+|\\-)?[0-9\\.\\,]+");
                values = Array.CreateInstance(type, raw.Count);
                for (var i = 0; i < values.Length; i++)
                    values.SetValue(Convert.ChangeType(raw[i].Value, type, culture), i);
            }

            data = new byte[values.Length * Marshal.SizeOf(type)];
            System.Buffer.BlockCopy(values, 0, data, 0, data.Length);
        }
    }
}
