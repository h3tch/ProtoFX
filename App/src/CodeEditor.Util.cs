using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace App
{
    partial class CodeEditor
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
        /// Include preprocessor #include files.
        /// </summary>
        /// <param name="text">String to add included files to.</param>
        /// <param name="dir">The directory of the *.tech file.</param>
        /// <returns>String including include files.</returns>
        public static string IncludeFiles(string text, string dir, int[] linePos = null, Inc[] incPos = null)
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
                if (linePos != null)
                {
                    int i = linePos.IndexOf(x => x >= match.Index);
                    if (i >= 0)
                    {
                        for (; i < linePos.Length; i++)
                            linePos[i] += offset;
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
        
        public class Block
        {
            public string File;
            public int Line;
            public int Pos;
            public string Text;

            public Block(string file, int line, int pos, string text)
            {
                File = file;
                Line = line;
                Pos = pos;
                Text = text;
            }
        }

        public class Inc
        {
            public string File;
            public int StartPos;
            public int EndPos;

            public Inc(string file, int startpos, int endpos)
            {
                File = file;
                StartPos = startpos;
                EndPos = endpos;
            }
        }
    }
}
