using System;
using System.Collections.Generic;
using System.IO;
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


                if (!System.IO.File.Exists(path))
                    throw new FileNotFoundException("Compilation aborted " +
                        $"because '{path}' could not be found.");

                // do not allow recursive inclusion of files
                if (incpath.Contains(path))
                    throw new NotSupportedException("Compilation aborted " +
                        $"because of a recursiveinclusion of '{path}'.");
                incpath.Add(path);
                // remove comments
                text = RemoveComments(System.IO.File.ReadAllText(path));
                // process all include files in the file
                include = ProcessIncludes().ToArray();
                // process all blocks in the file
                block = ProcessBlocks().ToArray();
            }

            private IEnumerable<File> ProcessIncludes()
            {
                // get directory form path
                var dir = Path.GetDirectoryName(path) + '/';
                // find #include statements
                var matches = Regex.Matches(text, @"^\s*#include \""[^""]*\""", RegexOptions.Multiline);

                // load all include files
                for (int i = 0; i < matches.Count; i++)
                {
                    // get filename from include statement
                    var incfile = Regex.Match(matches[i].Value, @"\""[^""]*\""").Value;
                    // load and process file
                    yield return new File(dir + incfile.Substring(1, incfile.Length - 2),
                        this, matches[i].Index, text.LineFromPosition(matches[i].Index));
                }
            }

            private IEnumerable<Block> ProcessBlocks()
            {
                // find block definitions
                var open = "{";
                var close = "}";
                var header = @"(\w+[ \t]*){2,3}";
                var oc = "" + open + close;
                var matches = Regex.Matches(text, header +
                    $"{open}[^{oc}]*(((?<Open>{open})[^{oc}]*)+" +
                    $"((?<Close-Open>{close})[^{oc}]*)+)*(?(Open)(?!)){close}");

                // process found block strings
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
            private static HashSet<string> incpath = new HashSet<string>();
        }

        public class Block
        {
            public Block(File owner, int position, int line, string text)
            {
                this.owner = owner;
                this.position = position;
                this.line = line;
                this.text = text;

                // find all words before the brace
                var matches = Regex.Matches(text.Substring(0, text.IndexOf('{')), @"\w+");

                // first word is the block type
                if (matches.Count > 0)
                    type = matches[0].Value;
                // if there are more than 2 words
                // an annotation was provided
                if (matches.Count > 2)
                {
                    anno = matches[1].Value;
                    name = matches[2].Value;
                }
                else if (matches.Count > 1)
                    name = matches[1].Value;

                // process command body of the block
                commands = ProcessCommands().ToArray();
            }

            private IEnumerable<Command> ProcessCommands()
            {
                // split the command body of the block into lines
                var braceOpen = text.IndexOf('{');
                var braceClose = text.LastIndexOf('}');
                var body = text.Substring(braceOpen + 1, braceClose - braceOpen - 2);
                var lines = body.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);

                // process each line for possible commands
                for (int i = 0; i < lines.Length; i++)
                {
                    var cmd = new Command(this, text.PositionFromLine(i), i, lines[i]);
                    // only return valid commands
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

                // process arguments in the command line
                argument = ProcessArguments().ToArray();
            }
            
            private IEnumerable<Argument> ProcessArguments()
            {
                // replace all non word characters with \n
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

                // find all words in the command line
                var matches = Regex.Matches(commandLine, @"[^\n]+");

                // convert them to arguments
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
