using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace App
{
    partial class App
    {
        private static string RemoveComments(string code, string linecomment)
        {
            var blockComments = @"/\*(.*?)\*/";
            var lineComments = @"//(.*?)\r?\n";
            var strings = @"""((\\[^\n]|[^""\n])*)""";
            var verbatimStrings = @"@(""[^""]*"")+";
            return Regex.Replace(code,
                blockComments + "|" + lineComments + "|" + strings + "|" + verbatimStrings,
                me =>
                {
                    if (me.Value.StartsWith("/*") || me.Value.StartsWith("//"))
                        return me.Value.StartsWith("//") ? Environment.NewLine : "";
                    // Keep the literal strings
                    return me.Value;
                },
                RegexOptions.Singleline);
        }

        private static string IncludeFiles(string dir, string code)
        {
            // find include files
            var matches = Regex.Matches(code, @"#include \""[^""]*\""");

            // insert all include files
            for (int i = 0, offset = 0; i < matches.Count; i++)
            {
                // get file path
                var include = code.Substring(matches[i].Index + offset, matches[i].Length);
                var startidx = include.IndexOf('"');
                var incfile = include.Substring(startidx + 1, include.LastIndexOf('"') - startidx - 1);
                var path = Path.IsPathRooted(incfile) ? incfile : dir + incfile;

                // check if file exists
                if (File.Exists(path) == false)
                    throw new Exception("ERROR: The include file '" + incfile + "' could not be found.\n");

                // load the file and insert it, replacing #include
                var content = File.ReadAllText(path);
                code = code.Substring(0, matches[i].Index + offset)
                    + content + code.Substring(matches[i].Index + offset + matches[i].Length);

                // because the string now has a different length, we need an offset
                offset += content.Length - matches[i].Length;
            }
            return code;
        }

        private static string[] FindObjectBlocks(string code)
        {
            // find potential block positions
            var matches = Regex.Matches(code, "(\\w+\\s*){2,3}\\{");

            // find all '{' that potentially indicate a block
            int count = 0;
            int newline = 0;
            List<int> blockBr = new List<int>();
            for (int i = 0; i < code.Length; i++)
            {
                if (code[i] == '\n')
                    newline++;
                if (code[i] == '{' && count++ == 0)
                    blockBr.Add(i);
                if (code[i] == '}' && --count == 0)
                    blockBr.Add(i);
                if (count < 0)
                    throw new Exception("FATAL ERROR in line " + newline + ": Unexpected occurrence of '}'.");
            }

            // where 'matches' and 'blockBr' are aligned we have a block
            List<string> blocks = new List<string>();
            for (int i = 0; i < matches.Count; i++)
            {
                int idx = blockBr.IndexOf(matches[i].Index + matches[i].Length - 1);
                if (idx >= 0)
                    blocks.Add(code.Substring(matches[i].Index, blockBr[idx + 1] - matches[i].Index + 1));
            }

            // return blocks as array
            return blocks.ToArray();
        }

        private static string[] FindObjectClass(string objectblock)
        {
            // parse class info
            MatchCollection matches = null;
            var lines = objectblock.Split(new char[] { '\n' });
            for (int j = 0; j < lines.Length; j++)
                // ignore empty or invalid lines
                if ((matches = Regex.Matches(lines[j], "[\\w.]+")).Count > 0)
                    return matches.Cast<Match>().Select(m => m.Value).ToArray();
            // ill defined class block
            return null;
        }

        private static Array ConvertData(byte[] data, string type, out Type T)
        {
            // convert data to specified type
            switch (type)
            {
                case "byte": T = typeof(byte); return ConvertData<byte>(data);
                case "short": T = typeof(short); return ConvertData<short>(data);
                case "ushort": T = typeof(ushort); return ConvertData<ushort>(data);
                case "int": T = typeof(int); return ConvertData<int>(data);
                case "uint": T = typeof(uint); return ConvertData<uint>(data);
                case "long": T = typeof(long); return ConvertData<long>(data);
                case "ulong": T = typeof(ulong); return ConvertData<ulong>(data);
                case "float": T = typeof(float); return ConvertData<float>(data);
                case "double": T = typeof(double); return ConvertData<double>(data);
            }

            throw new Exception("INTERNAL_ERROR: Could not convert buffer data to specified type.");
        }

        private static Array ConvertData<T>(byte[] data)
        {
            // find method to convert the data
            var methods = from m in typeof(BitConverter).GetMethods()
                          where m.Name == "To" + typeof(T).Name
                          select m;
            if (methods.Count() == 0)
                return data;

            var method = methods.First();

            // allocate array
            int typesize = Marshal.SizeOf(typeof(T));
            Array rs = Array.CreateInstance(typeof(T), data.Length / typesize);

            // convert data
            for (int i = 0; i < rs.Length; i++)
                rs.SetValue(Convert.ChangeType(method.Invoke(null, new object[] { data, typesize * i }), typeof(T)), i);

            return rs;
        }
    }
}
