using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace App
{
    partial class App
    {
        /// <summary>
        /// Remove /*...*/ and // comments.
        /// </summary>
        /// <param name="text">String to remove comments from.</param>
        /// <returns>String without comments.</returns>
        public static string RemoveComments(string text)
        {
            var blockComments = @"/\*(.*?)\*/";
            var lineComments = @"//(.*?)\r?\n";
            var strings = @"""((\\[^\n]|[^""\n])*)""";
            var verbatimStrings = @"@(""[^""]*"")+";
            return Regex.Replace(text,
                $"{blockComments}|{lineComments}|{strings}|{verbatimStrings}",
                me =>
                {
                    if (me.Value.StartsWith("/*"))
                        return new string(' ', me.Length);
                    if (me.Value.StartsWith("//"))
                        return new string(' ', me.Length - Environment.NewLine.Length) + Environment.NewLine;
                    // Keep the literal strings
                    return me.Value;
                },
                RegexOptions.Singleline);
        }

        /// <summary>
        /// Remove ... new line indicators.
        /// </summary>
        /// <param name="text">String to remove new line indicators from.</param>
        /// <returns>String without new line indicators.</returns>
        private static string RemoveNewLineIndicators(string text)
        {
            return Regex.Replace(text, @"\.\.\.(\s?)(\n|\r|\r\n)", " ");
        }

        /// <summary>
        /// Include preprocessor #include files.
        /// </summary>
        /// <param name="dir">The directory of the *.tech file.</param>
        /// <param name="text">String to add included files to.</param>
        /// <returns>String including include files.</returns>
        private static string IncludeFiles(string dir, string text)
        {
            // find include files
            var matches = Regex.Matches(text, @"#include \""[^""]*\""");

            // insert all include files
            int offset = 0;
            foreach (Match match in matches)
            {
                // get file path
                var include = text.Substring(match.Index + offset, match.Length);
                var startidx = include.IndexOf('"');
                var incfile = include.Substring(startidx + 1, include.LastIndexOf('"') - startidx - 1);
                var path = Path.IsPathRooted(incfile) ? incfile : dir + incfile;

                // check if file exists
                if (File.Exists(path) == false)
                    throw new GLException($"The include file '{incfile}' could not be found.\n");

                // load the file and insert it, replacing #include
                var content = File.ReadAllText(path);
                text = text.Substring(0, match.Index + offset)
                    + content + text.Substring(match.Index + offset + match.Length);

                // because the string now has a different 
                // length, we need to remember the offset
                offset += content.Length - match.Length;
            }

            return text;
        }

        private static string ResolvePreprocessorDefinitions(string text)
        {
            // find include files
            var matches = Regex.Matches(text, @"#global(\s+\w+){2}");

            // insert all include files
            foreach (Match match in matches)
            {
                // get defined preprocessor string
                var definitions = Regex.Split(match.Value, @"[ ]+");
                var key = definitions[1];
                var value = definitions[2];
                text = text.Substring(0, match.Index) + text.Substring(match.Index + match.Length);

                int offset = 0;
                foreach (Match m in Regex.Matches(text, key))
                {
                    // replace preprocessor definition with defined preprocessor string
                    text = text.Substring(0, m.Index + offset) + value
                         + text.Substring(m.Index + offset + m.Length);

                    // because the string now has a different 
                    // length, we need to remember the offset
                    offset += value.Length - m.Length;
                }

            }

            return text;
        }

        private static string[] GetObjectBlocks(string text)
        {
            // find all '{' that potentially indicate a block
            int count = 0;
            int newline = 0;
            List<int> blockBr = new List<int>();
            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] == '\n')
                    newline++;
                if (text[i] == '{' && count++ == 0)
                    blockBr.Add(i);
                if (text[i] == '}' && --count == 0)
                    blockBr.Add(i);
                if (count < 0)
                    throw new GLException($"ERROR in line {newline}: Unexpected occurrence of '}}'.");
            }

            // find potential block positions
            var matches = Regex.Matches(text, @"(\w+\s*){2,3}\{");

            // where 'matches' and 'blockBr' are aligned we have a block
            List<string> blocks = new List<string>();
            foreach (Match match in matches)
            {
                int idx = blockBr.IndexOf(match.Index + match.Length - 1);
                if (idx >= 0)
                    blocks.Add(text.Substring(match.Index, blockBr[idx + 1] - match.Index + 1));
            }

            // return blocks as array
            return blocks.ToArray();
        }
    }
}
