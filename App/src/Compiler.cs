using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace App
{
    class Compiler
    {
        public static File Compile(string path) => new File(path);

        public class File
        {
            public File(string path, File owner = null, int position = 0, int line = 0)
            {
                this.owner = owner;
                this.position = position;
                this.line = line;
                this.path = path;
                if (System.IO.File.Exists(path))
                {
                    text = RemoveComments(System.IO.File.ReadAllText(path));
                    include = ProcessIncludes().ToArray();
                    block = ProcessBlocks().ToArray();
                }
            }

            private IEnumerable<File> ProcessIncludes()
            {
                var dir = System.IO.Path.GetDirectoryName(path) + '/';
                var matches = Regex.Matches(text, @"^\s*#include \""[^""]*\""", RegexOptions.Multiline);

                for (int i = 0; i < matches.Count; i++)
                {
                    var incfile = Regex.Match(matches[i].Value, @"\""[^""]*\""").Value;
                    yield return new File(dir + incfile.Substring(1, incfile.Length - 2),
                        this, matches[i].Index, text.LineFromPosition(matches[i].Index));
                }
            }

            private IEnumerable<Block> ProcessBlocks()
            {
                var open = "{";
                var close = "}";
                var header = @"(\w+[ \t]*){2,3}";
                var oc = "" + open + close;
                var matches = Regex.Matches(text, header +
                    $"{open}[^{oc}]*(((?<Open>{open})[^{oc}]*)+" +
                    $"((?<Close-Open>{close})[^{oc}]*)+)*(?(Open)(?!)){close}");

                foreach (Match match in matches)
                    yield return new Block(this, match.Index,
                        text.LineFromPosition(match.Index), match.Value);
            }

            public File owner;
            public string path;
            public int position;
            public int line;
            public string text;
            public File[] include;
            public Block[] block;
        }

        public class Block
        {
            public Block(File owner, int position, int line, string text)
            {
                this.owner = owner;
                this.position = position;
                this.line = line;
                this.text = text;

                var matches = Regex.Matches(text.Substring(0, text.IndexOf('{')), @"\w+");

                if (matches.Count > 0)
                    type = matches[0].Value;
                if (matches.Count > 2)
                {
                    anno = matches[1].Value;
                    name = matches[2].Value;
                }
                else if (matches.Count > 1)
                    name = matches[1].Value;

                this.commands = ProcessCommands().ToArray();
            }

            private IEnumerable<Command> ProcessCommands()
            {
                var braceOpen = text.IndexOf('{');
                var braceClose = text.LastIndexOf('}');
                var body = text.Substring(braceOpen + 1, braceClose - braceOpen - 2);
                var lines = body.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);

                for (int i = 0; i < lines.Length; i++)
                {
                    var cmd = new Command(this, text.PositionFromLine(i), i, lines[i]);
                    if (cmd.argument.Length > 0)
                        yield return cmd;
                }
            }

            public File owner;
            public int position;
            public int line;
            public string text;
            public string type;
            public string name;
            public string anno;
            public Command[] commands;
        }

        public class Command
        {
            public Command(Block owner, int position, int line, string text)
            {
                this.owner = owner;
                this.position = position;
                this.line = line;
                this.text = text;
                this.argument = ProcessArguments().ToArray();
            }
            
            private IEnumerable<Argument> ProcessArguments()
            {
                char[] chars = text.ToCharArray();
                bool inQuote = false;
                for (int i = 0; i < chars.Length; i++)
                {
                    if (chars[i] == '"')
                        inQuote = !inQuote;
                    if (!inQuote && (chars[i] == ' ' || chars[i] == '\t' || chars[i] == '\r'))
                        chars[i] = '\n';
                }
                var commandLine = new string(chars);
                var matches = Regex.Matches(commandLine, @"[^\n]+");
                foreach (Match match in matches)
                    yield return new Argument(this, match.Index, 0, match.Value);
            }

            public Block owner;
            public int position;
            public int line;
            public string text;
            public Argument[] argument;
        }

        public class Argument
        {
            public Command owner;
            public int position;
            public int line;
            public string text;

            public Argument(Command owner, int position, int line, string text)
            {
                this.owner = owner;
                this.position = position;
                this.line = line;
                this.text = text;
            }
        }

        /// <summary>
        /// Remove /*...*/ and // comments.
        /// </summary>
        /// <param name="text">String to remove comments from.</param>
        /// <returns>String without comments.</returns>
        private static string RemoveComments(string text)
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
    }
}
