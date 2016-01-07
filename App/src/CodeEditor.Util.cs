using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace App
{
    partial class CodeEditor
    {
        public static int LineFromPosition(int position, int[] newLine)
            => newLine.IndexOf(x => x >= position);

        /// <summary>
        /// Find all new-line positions in a text.
        /// </summary>
        /// <param name="text">The string to process.</param>
        /// <returns>Returns an array of new-line positions.</returns>
        public static int[] GetNewLines(string text)
        {
            var matches = Regex.Matches(text, "(\r\n|\n|\r)");
            var newLine = new int[matches.Count + 1];

            newLine[0] = 0;
            for (int i = 0; i < matches.Count; i++)
                newLine[i + 1] = matches[i].Index + matches[i].Length;

            return newLine;
        }

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
            var newLineLen = Environment.NewLine.Length;
            return Regex.Replace(text,
                $"{blockComments}|{lineComments}|{strings}|{verbatimStrings}",
                x =>
                {
                    // replace comments with spaces
                    if (x.Value.StartsWith("/*"))
                        return new string(' ', x.Length);
                    if (x.Value.StartsWith("//"))
                        return new string(' ', x.Length - newLineLen) + Environment.NewLine;
                    // Keep the literal strings
                    return x.Value;
                },
                RegexOptions.Singleline);
        }

        /// <summary>
        /// Remove '...' new line indicators.
        /// </summary>
        /// <param name="text">String to remove new line indicators from.</param>
        /// <returns>String without new line indicators.</returns>
        public static string RemoveNewLineIndicators(string text)
            => Regex.Replace(text, @"\.\.\.(\s?)(\n|\r|\r\n)", 
                x => new string(' ', x.Value.Length),
                RegexOptions.None);

        /// <summary>
        /// Include preprocessor #include files.
        /// </summary>
        /// <param name="text">String to add included files to.</param>
        /// <param name="dir">The directory of the *.tech file.</param>
        /// <returns>String including include files.</returns>
        public static string IncludeFiles(string text, string dir, int[] newLine = null)
        {
            // find include files
            var matches = Regex.Matches(text, @"^\s*#include \""[^""]*\""", RegexOptions.Multiline);

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
                    throw new CompileException($"The include file '{incfile}' could not be found.\n");

                // load the file and insert it, replacing #include
                var content = IncludeFiles(File.ReadAllText(path), Path.GetDirectoryName(path));
                text = text.Substring(0, match.Index + offset) + content
                    + text.Substring(match.Index + offset + match.Length);

                // because the string now has a different 
                // length, we need to remember the offset
                offset += content.Length - match.Length;

                // update new line positions
                if (newLine != null)
                {
                    int i = newLine.IndexOf(x => x >= match.Index);
                    if (i >= 0)
                    {
                        for (; i < newLine.Length; i++)
                            newLine[i] += offset;
                    }
                }
            }

            return text;
        }

        /// <summary>
        /// Resolve preprocessor #global definitions.
        /// </summary>
        /// <param name="text">String to resolve.</param>
        /// <returns>String with resolved #global definitions.</returns>
        public static string ResolvePreprocessorDefinitions(string text, int[] newLine = null)
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

                    // update new line positions
                    int i = newLine.IndexOf(x => x >= m.Index);
                    if (i >= 0)
                    {
                        for (; i < newLine.Length; i++)
                            newLine[i] += offset;
                    }
                }

            }

            return text;
        }

        /// <summary>
        /// Find all block positions in the string.
        /// </summary>
        /// <param name="text">String to process.</param>
        /// <returns>Returns an enumerable with block positions
        /// [class type index, '{' index, '}' index]</returns>
        public static IEnumerable<int[]> GetBlockPositions(string text)
        {
            // find all '{' that potentially indicate a block
            var blockBr = new List<int>();
            for (int i = 0, count = 0, nline = 0; i < text.Length; i++)
            {
                if (text[i] == '\n')
                    nline++;
                if (text[i] == '{' && count++ == 0)
                    blockBr.Add(i);
                if (text[i] == '}' && --count == 0)
                    blockBr.Add(i);
                if (count < 0)
                    throw new CompileException($"ERROR in line {nline}: Unexpected occurrence of '}}'.");
            }

            // find potential block positions
            var matches = Regex.Matches(text, @"(\w+\s*){2,3}\{");

            // where 'matches' and 'blockBr' are aligned we have a block
            var blocks = new List<int[]>();
            foreach (Match match in matches)
            {
                int idx = blockBr.IndexOf(match.Index + match.Length - 1);
                if (idx >= 0)
                    yield return new[] { match.Index, blockBr[idx], blockBr[idx + 1] };
            }
        }

        /// <summary>
        /// Get all block strings in the string "TYPE ANNO NAME { ... }".
        /// </summary>
        /// <param name="text">String to process.</param>
        /// <returns>Returns an enumerable of all block strings.</returns>
        public static IEnumerable<Block> GetBlocks(string text, int[] newLine = null)
        {
            // return block text from block positions
            foreach (var block in GetBlockPositions(text))
                yield return new Block(block[0], text.Substring(block[0], block[2] - block[0] + 1));
        }

        public struct Block
        {
            public int pos;
            public string text;

            public Block(int pos, string text)
            {
                this.pos = pos;
                this.text = text;
            }
        }
    }
}
