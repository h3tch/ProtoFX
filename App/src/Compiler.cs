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
        #region FIELD
        public static Regex RegexBlock;
        public static Regex RegexFunction;
        public static Regex RegexLayout;
        private static HashSet<string> incpath = new HashSet<string>();
        #endregion

        static Compiler()
        {
            var open = "{";
            var close = "}";
            var blockHeader = @"(\w+[ \t]*){2,3}[\s\r\n]*";
            var functionHeader = @"(\w+[ \t]*){2}\([\s\w\d\r\n,\.]*\)[\s\r\n]*";
            var oc = "" + open + close;
            RegexBlock = new Regex(blockHeader +
                $"{open}[^{oc}]*(((?<Open>{open})[^{oc}]*)+" +
                $"((?<Close-Open>{close})[^{oc}]*)+)*(?(Open)(?!)){close}");
            RegexFunction = new Regex(functionHeader +
                $"{open}[^{oc}]*(((?<Open>{open})[^{oc}]*)+" +
                $"((?<Close-Open>{close})[^{oc}]*)+)*(?(Open)(?!)){close}");
            RegexLayout = new Regex(@"\w+[ \t]*\([\s\w\d\r\n,=]*\)");
        }
        
        /// <summary>
        /// Compile file.
        /// </summary>
        /// <param name="path">Path to the file that should be compiled.</param>
        /// <returns>Returns the root of the compiled tree.</returns>
        public static File Compile(string path)
        {
            incpath.Clear();
            return new File(path);
        }

        #region Compiler classes
        public class File : IEnumerable<Block>
        {
            #region FIELDS
            public File Owner { get; private set; }
            public string Path { get; private set; }
            public int Line { get; private set; }
            public int LineInFile => Line;
            public string Text { get; private set; }
            public File[] Include { get; private set; }
            public Block[] Block { get; private set; }
            #endregion

            /// <summary>
            /// Parse file and extract block data.
            /// </summary>
            /// <param name="path">File to be compiled.</param>
            /// <param name="owner">An owner file indicates that this is an included file.</param>
            /// <param name="line">The preprocessor line of the included file.</param>
            public File(string path, File owner = null, int line = 0)
            {
                Owner = owner;
                Line = line;
                Path = path;

                // does the include file exist
                if (!System.IO.File.Exists(path))
                    throw new FileNotFoundException("Compilation aborted " +
                        $"because '{path}' could not be found.");

                // do not allow recursive inclusion of files
                if (incpath.Contains(path))
                    throw new NotSupportedException("Compilation aborted " +
                        $"because of a recursive inclusion of '{path}'.");
                incpath.Add(path);

                // remove comments
                Text = RemoveComments(System.IO.File.ReadAllText(path));
                Text = ResolvePreprocessorDefinitions(Text);

                // process all include files in the file
                Include = ProcessIncludes().ToArray();

                // process all blocks in the file
                Block = ProcessBlocks().ToArray();
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
                while ((includeIncludeFiles && iInc < Include.Length) || iBlock < Block.Length)
                {
                    // DECIDE WHETHER TO READ THE NEXT BLOCK OR THE NEXT INCLUDE FILE

                    // if there is any block left
                    if (iBlock < Block.Length)
                        // if include files should be included and there is any include file left
                        if (includeIncludeFiles && iInc < Include.Length)
                            // which one is the next in the code
                            if (Include[iInc].Line < Block[iBlock].Line)
                                // return include file blocks
                                foreach (var b in Include[iInc++].GetBlocks().ToArray())
                                    yield return b;
                            else
                                // return block
                                yield return Block[iBlock++];
                        else
                            // return block
                            yield return Block[iBlock++];
                    // if include files should be included
                    else if (includeIncludeFiles)
                        // return include file blocks
                        foreach (var b in Include[iInc++].GetBlocks().ToArray())
                            yield return b;
                }
            }

            /// <summary>
            /// Find all include definitions and process those files to build the object tree.
            /// </summary>
            /// <returns>Returns a list of processed include files.</returns>
            private IEnumerable<File> ProcessIncludes()
            {
                // get directory form path
                var dir = System.IO.Path.GetDirectoryName(Path) + System.IO.Path.DirectorySeparatorChar;
                // find #include statements
                var matches = Regex.Matches(Text, @"^\s*#include \""[^""]*\""", RegexOptions.Multiline);

                // load all include files
                for (int i = 0; i < matches.Count; i++)
                {
                    // get filename from include statement
                    var incfile = Regex.Match(matches[i].Value, @"\""[^""]*\""").Value;
                    // load and process file
                    yield return new File(dir + incfile.Substring(1, incfile.Length - 2),
                        this, Text.LineFromPosition(matches[i].Index));
                }
            }

            /// <summary>
            /// Search for and process all object blocks in the file.
            /// </summary>
            /// <returns></returns>
            private IEnumerable<Block> ProcessBlocks()
            {
                // process found block strings
                foreach (Match match in RegexBlock.Matches(Text))
                    yield return new Block(this, Text.LineFromPosition(match.Index), match.Value);
            }

            #region IEnumerable Interface
            public IEnumerator<Block> GetEnumerator() => GetBlocks().GetEnumerator();

            IEnumerator<Block> IEnumerable<Block>.GetEnumerator() => GetBlocks().GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator() => GetBlocks().GetEnumerator();
            #endregion
        }

        public class Block : IEnumerable<Command>
        {
            #region FIELD
            public File Owner { get; private set; }
            public int Line { get; private set; }
            public int LineInFile => Line;
            public string Text { get; private set; }
            public string Body => Text.Subrange(Text.IndexOf('{') + 1, Text.LastIndexOf('}') - 1);
            public string Type { get; private set; }
            public string Name { get; private set; }
            public string Anno { get; private set; }
            public string File { get { return Owner.Path; } }
            public Command[] Cmds { get; private set; }
            public Command this[int i] { get { return Cmds[i]; } }
            public IEnumerable<Command> this[string name] { get { return GetCommands(name); } }
            #endregion

            /// <summary>
            /// Parse block string and extract commands.
            /// </summary>
            /// <param name="owner">File which owns the block string.</param>
            /// <param name="line">Line in the owner file where the block string is located.</param>
            /// <param name="text">The block string to be parsed.</param>
            public Block(File owner, int line, string text)
            {
                Owner = owner;
                Line = line;
                Text = text.Trim();

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

            /// <summary>
            /// Process each line of an object block for commands.
            /// </summary>
            /// <returns>Returns a list of commands.</returns>
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
                    // read next line
                    var linei = i;
                    var line = lines[i];
                    // remove new line indicators
                    while (lines[i].TrimEnd().EndsWith("..."))
                        line += lines[++i];
                    line = line.Replace("...", " ");
                    // create new command class
                    var cmd = new Command(this, linei, line);
                    // only return valid commands
                    if (cmd.ArgCount >= 0)
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
            #region FIELD
            public Block Owner { get; private set; }
            public int Line { get; private set; }
            public int LineInFile => Owner.LineInFile + Line;
            public string Text { get; private set; }
            public int Length => Args.Length - 1;
            private Argument[] Args { get; set; }
            public string File { get { return Owner.Owner.Path; } }
            public string Name { get { return Args[0].Text; } }
            public int ArgCount { get { return Args.Length - 1; } }
            public Argument this[int i] { get { return Args[i + 1]; } }
            #endregion

            /// <summary>
            /// Parse command line for arguments.
            /// </summary>
            /// <param name="owner">Block owning the command line.</param>
            /// <param name="line">Line in the block string where the command line is located.</param>
            /// <param name="text">The command line text.</param>
            public Command(Block owner, int line, string text)
            {
                Owner = owner;
                Line = line;
                Text = text.Trim();

                // process arguments in the command line
                Args = ProcessArguments().ToArray();
            }
            
            /// <summary>
            /// Split command into arguments. The first argument is the command itself.
            /// </summary>
            /// <returns></returns>
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
                {
                    var arg = match.Value;
                    int s = arg[0] == '"' && arg[arg.Length - 1] == '"' ? 1 : 0;
                    yield return new Argument(this, arg.Substring(s, arg.Length - s * 2));
                }
            }

            /// <summary>
            /// Parse the command and extract arguments as values.
            /// </summary>
            /// <param name="types">List of argument types.</param>
            /// <param name="mandatory">Specifies which of this arguments are mandatory.</param>
            /// <param name="scene">Dictionary of scene objects.</param>
            /// <param name="err"></param>
            /// <returns>List of objects values. If a value could not be
            /// parsed, the returned value will be null.</returns>
            public object[] Parse(Type[] types, bool[][] mandatory, Dict scene,
                CompileException err = null)
            {
                string[] unused;
                return Parse(types, mandatory, out unused, scene, err);
            }

            /// <summary>
            /// Parse the command and extract arguments as values.
            /// </summary>
            /// <param name="types">List of argument types.</param>
            /// <param name="mandatory">Specifies which of this arguments are mandatory.</param>
            /// <param name="unusedArgs">List of arguments the where not parsed.</param>
            /// <param name="scene">Dictionary of scene objects.</param>
            /// <param name="err"></param>
            /// <returns>List of objects values. If a value could not be
            /// parsed, the returned value will be null.</returns>
            public object[] Parse(Type[] types, bool[][] mandatory, out string[] unusedArgs, Dict scene,
                CompileException err = null)
            {
                object[] values = new object[types.Length];

                // parse command arguments
                var lastArgUsed = 0;
                for (var a = 0; a < Length; a++)
                {
                    var arg = this[a];
                    var I = values
                        .Zip(Enumerable.Range(0, types.Length), (x, i) => x == null ? i : -1)
                        .Where(x => x >= 0);
                    foreach (var i in I)
                    {
                        try
                        {
                            values[i] = types[i].IsSubclassOf(typeof(GLObject))
                                ? scene.GetValueOrDefault<GLObject>(arg.Text)
                                : types[i].IsEnum
                                    ? Enum.Parse(types[i], arg.Text, true)
                                    : Convert.ChangeType(arg.Text, types[i], App.Culture);
                            if (values[i] != null)
                            {
                                lastArgUsed = a;
                                break;
                            }
                        }
                        catch { }
                    }
                }

                // return list of unused arguments
                unusedArgs = this.Skip(lastArgUsed + 1).Select(x => x.Text).ToArray();

                // check for errors
                var valid = values.Select(x => x != null);
                for (int i = 0; i < mandatory.Length; i++)
                    if (mandatory[i].Zip(valid, (m, v) => !m | v).All(x => x))
                        return values;

                err?.Add("Command has one or more invalid arguments.", this);
                return values;
            }

            #region IEnumerable Interface
            public IEnumerator<Argument> GetEnumerator() => Args.Skip(1).GetEnumerator();

            IEnumerator<Argument> IEnumerable<Argument>.GetEnumerator() => Args.Skip(1).GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator() => Args.Skip(1).GetEnumerator();
            #endregion
        }

        public class Argument
        {
            #region FIELD
            public Command Owner { get; private set; }
            public int Line => 0;
            public int LineInFile => Owner.LineInFile + Line;
            public string Text { get; private set; }
            #endregion

            /// <summary>
            /// Store argument string.
            /// </summary>
            /// <param name="owner">Command line owning the argument.</param>
            /// <param name="text">Argument string.</param>
            public Argument(Command owner, string text)
            {
                Owner = owner;
                Text = text.Trim();
            }
        }
        #endregion

        #region PROCESS CODE STRING
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
        /// Resolve preprocessor #global definitions.
        /// </summary>
        /// <param name="text">String to resolve.</param>
        /// <returns>String with resolved #global definitions.</returns>
        public static string ResolvePreprocessorDefinitions(string text)
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
        #endregion
    }

    // convenience extension to the CodeEditor class
    static class CodeEditorExtensions
    {
        /// <summary>
        /// Find all block positions in the string.
        /// </summary>
        /// <param name="editor">Code to be process.</param>
        /// <returns>Returns an enumerable with block positions
        /// [class type index, '{' index, '}' index]</returns>
        public static IEnumerable<Match> GetBlockPositions(this CodeEditor editor)
        {
            foreach (Match match in Compiler.RegexBlock.Matches(editor.Text))
                yield return match;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="editor"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static IEnumerable<Match> GetFunctionPositions(this CodeEditor editor, int start, int end)
        {
            foreach (Match match in Compiler.RegexFunction.Matches(editor.Text.Substring(0, end), start))
                yield return match;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="editor"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static IEnumerable<Match> GetLayoutPositions(this CodeEditor editor, int start, int end)
        {
            foreach (Match match in Compiler.RegexLayout.Matches(editor.Text.Substring(0, end), start))
                yield return match;
        }
    }

    // convenience extension to the Dict class
    static class CompilerDictExtensions
    {
        public static bool TryGetValue<T>(this Dict dict, string key, out T obj,
            Compiler.Command cmd, CompileException err)
            where T : GLObject
            => dict.TryGetValue(key, out obj, cmd.LineInFile, cmd.File, err);

        public static bool TryGetValue<T>(this Dict dict, string key, out T obj,
            Compiler.Block block, CompileException err)
            where T : GLObject
            => dict.TryGetValue(key, out obj, block.LineInFile, block.File, err);
    }

    // convenience extensions to the compiler exception class
    static class CompilerExeptionExtensions
    {
        public static CompileException Add(this CompileException err, string message, Compiler.Block block)
            => err.Add(message, block.File, block.LineInFile);

        public static CompileException Add(this CompileException err, string message, Compiler.Command cmd)
            => err.Add(message, cmd.File, cmd.LineInFile);
    }
}
