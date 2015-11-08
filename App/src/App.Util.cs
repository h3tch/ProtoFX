using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
                $"{blockComments}|{lineComments}|{strings}|{verbatimStrings}",
                me =>
                {
                    if (me.Value.StartsWith("/*") || me.Value.StartsWith("//"))
                        return me.Value.StartsWith("//") ? Environment.NewLine : "";
                    // Keep the literal strings
                    return me.Value;
                },
                RegexOptions.Singleline);
        }

        private static string RemoveNewLineIndicators(string code)
        {
            return Regex.Replace(code, @"\.\.\.(\s?)(\n|\r|\r\n)", "");
        }

        private static string IncludeFiles(string dir, string code)
        {
            // find include files
            var matches = Regex.Matches(code, @"#include \""[^""]*\""");

            // insert all include files
            int offset = 0;
            foreach (Match match in matches)
            {
                // get file path
                var include = code.Substring(match.Index + offset, match.Length);
                var startidx = include.IndexOf('"');
                var incfile = include.Substring(startidx + 1, include.LastIndexOf('"') - startidx - 1);
                var path = Path.IsPathRooted(incfile) ? incfile : dir + incfile;

                // check if file exists
                if (File.Exists(path) == false)
                    throw new GLException($"The include file '{incfile}' could not be found.\n");

                // load the file and insert it, replacing #include
                var content = File.ReadAllText(path);
                code = code.Substring(0, match.Index + offset)
                    + content + code.Substring(match.Index + offset + match.Length);

                // because the string now has a different 
                // length, we need to remember the offset
                offset += content.Length - match.Length;
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
                    throw new GLException($"FATAL ERROR in line {newline}: Unexpected occurrence of '}}'.");
            }

            // where 'matches' and 'blockBr' are aligned we have a block
            List<string> blocks = new List<string>();
            foreach (Match match in matches)
            {
                int idx = blockBr.IndexOf(match.Index + match.Length - 1);
                if (idx >= 0)
                    blocks.Add(code.Substring(match.Index, blockBr[idx + 1] - match.Index + 1));
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
    }
}
