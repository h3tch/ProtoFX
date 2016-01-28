using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace App
{
    class Compiler
    {
        public static File Compile(string path) => new File(path);

        public class File : IEnumerable<Block>
        {
            public File owner { get; private set; }
            public string path { get; private set; }
            public int position { get; private set; }
            public int line { get; private set; }
            public string text { get; private set; }
            public File[] include { get; private set; }
            public Block[] block { get; private set; }
            private static HashSet<string> incpath = new HashSet<string>();

            public File(string path, File owner = null, int position = 0, int line = 0)
            {
                this.owner = owner;
                this.position = position;
                this.line = line;
                this.path = path;

                // does the include file exist
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

            /// <summary>
            /// Get all blocks related to this file.
            /// </summary>
            /// <param name="includeIncludeFiles">Include included file blocks.</param>
            /// <returns>Returns a list of blocks sorted by occurrence.</returns>
            public IEnumerable<Block> GetBlocks(bool includeIncludeFiles = true)
            {
                int iInc = 0, iBlock = 0;
                // iterate through the sorted block lists
                while ((includeIncludeFiles && iInc < include.Length) || iBlock < block.Length)
                {
                    // DECIDE WHETHER TO READ THE NEXT BLOCK OR THE NEXT INCLUDE FILE

                    // if there is any block left
                    if (iBlock < block.Length)
                        // if include files should be included and there is any include file left
                        if (includeIncludeFiles && iInc < include.Length)
                            // which one is the next in the code
                            if (include[iInc].position < block[iBlock].Position)
                                // return include file blocks
                                foreach (var b in include[iInc++].GetBlocks().ToArray())
                                    yield return b;
                            else
                                // return block
                                yield return block[iBlock++];
                        else
                            // return block
                            yield return block[iBlock++];
                    // if include files should be included
                    else if (includeIncludeFiles)
                        // return include file blocks
                        foreach (var b in include[iInc++].GetBlocks().ToArray())
                            yield return b;
                }
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

            #region IEnumerable Interface
            public IEnumerator<Block> GetEnumerator() => GetBlocks().GetEnumerator();

            IEnumerator<Block> IEnumerable<Block>.GetEnumerator() => GetBlocks().GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator() => GetBlocks().GetEnumerator();
            #endregion
        }

        public class Block : IEnumerable<Command>
        {
            public File Owner { get; private set; }
            public int Position { get; private set; }
            public int Line { get; private set; }
            public string Text { get; private set; }
            public string Type { get; private set; }
            public string Name { get; private set; }
            public string Anno { get; private set; }
            public string File { get { return Owner.path; } }
            public Command[] Cmds { get; private set; }
            public Command this[int i] { get { return Cmds[i]; } }
            public IEnumerable<Command> this[string name] { get { return GetCommands(name); } }

            public Block(File owner, int position, int line, string text)
            {
                this.Owner = owner;
                this.Position = position;
                this.Line = line;
                this.Text = text;

                // find all words before the brace
                var matches = Regex.Matches(text.Substring(0, text.IndexOf('{')), @"\w+");

                // first word is the block type
                if (matches.Count > 0)
                    Type = matches[0].Value;
                // if there are more than 2 words
                // an annotation was provided
                if (matches.Count > 2)
                {
                    Anno = matches[1].Value;
                    Name = matches[2].Value;
                }
                else if (matches.Count > 1)
                    Name = matches[1].Value;

                // process command body of the block
                Cmds = ProcessCommands().ToArray();
            }
            
            /// <summary>
            /// Get all commands of the parent block.
            /// </summary>
            /// <param name="name">Restrict the search to certain commands.</param>
            /// <returns>Returns all commands sorted by occurrence.</returns>
            public IEnumerable<Command> GetCommands(string name = null)
            {
                foreach (var cmd in Cmds)
                    if (name == null || cmd.Name == name)
                        yield return cmd;
            }

            private IEnumerable<Command> ProcessCommands()
            {
                // split the command body of the block into lines
                var braceOpen = Text.IndexOf('{');
                var braceClose = Text.LastIndexOf('}');
                var body = Text.Substring(braceOpen + 1, braceClose - braceOpen - 2);
                var lines = body.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);

                // process each line for possible commands
                for (int i = 0; i < lines.Length; i++)
                {
                    var cmd = new Command(this, Text.PositionFromLine(i), i, lines[i]);
                    // only return valid commands
                    if (cmd.ArgCount > 0)
                        yield return cmd;
                }
            }

            #region IEnumerable Interface
            public IEnumerator<Command> GetEnumerator() => GetCommands().GetEnumerator();

            IEnumerator<Command> IEnumerable<Command>.GetEnumerator() => GetCommands().GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator() => GetCommands().GetEnumerator();
            #endregion
        }

        public class Command : IEnumerable<Argument>
        {
            public Block Owner { get; private set; }
            public int Position { get; private set; }
            public int Line { get; private set; }
            public string Text { get; private set; }
            private Argument[] Args { get; set; }
            public string File { get { return Owner.Owner.path; } }
            public string Name { get { return Args[0].Text; } }
            public int ArgCount { get { return Args.Length - 1; } }
            public Argument this[int i] { get { return Args[i + 1]; } }

            public Command(Block owner, int position, int line, string text)
            {
                Owner = owner;
                Position = position;
                Line = line;
                Text = text;

                // process arguments in the command line
                Args = ProcessArguments().ToArray();
            }
            
            private IEnumerable<Argument> ProcessArguments()
            {
                // replace all non word characters with \n
                char[] chars = Text.ToCharArray();
                bool inQuote = false;
                for (int i = 0; i < chars.Length; i++)
                {
                    if (chars[i] == '"')
                        inQuote = !inQuote;
                    if (!inQuote && new[] { ' ', '\t', '\r' }.Any(x => x == chars[i]))
                        chars[i] = '\n';
                }
                var commandLine = new string(chars);

                // find all words in the command line
                var matches = Regex.Matches(commandLine, @"[^\n]+");

                // convert them to arguments
                foreach (Match match in matches)
                    yield return new Argument(this, match.Index, 0, match.Value);
            }

            #region IEnumerable Interface
            public IEnumerator<Argument> GetEnumerator() => Args.Skip(1).GetEnumerator();

            IEnumerator<Argument> IEnumerable<Argument>.GetEnumerator() => Args.Skip(1).GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator() => Args.Skip(1).GetEnumerator();
            #endregion
        }

        public class Argument
        {
            public Command Owner { get; private set; }
            public int Position { get; private set; }
            public int Line { get; private set; }
            public string Text { get; private set; }

            public Argument(Command owner, int position, int line, string text)
            {
                Owner = owner;
                Position = position;
                Line = line;
                Text = text;
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
