using ScintillaNET;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using static App.FxLexer.Styles;

namespace App
{
    class FxLexer
    {
        #region FIELDS
        public enum Styles : int
        {
            Default,
            Identifier,
            Number,
            String,
            Char,
            LineComment,
            BlockComment,
            Preprocessor,
            Operator,
            Punctuation,
        }
        
        private enum FoldState : int
        {
            Unknown,
            StartFolding,
            Foldable,
            EndFolding,
        }

        private static UnicodeCategory[] Punctuation = new[] {
            UnicodeCategory.OpenPunctuation,
            UnicodeCategory.ClosePunctuation,
            UnicodeCategory.ConnectorPunctuation,
        };

        private static HashSet<string>[] keywords = null;
        public static Dictionary<string, KeyDef> Defs { get; private set; } = null;
        public static int KeywordStylesStart => (int)Styles.Punctuation + 1;
        public static int KeywordStylesEnd => KeywordStylesStart + keywords.Length;
        #endregion

        /// <summary>
        /// Create a lexer from a keyword definition list.
        /// </summary>
        /// <param name="keywordDef"></param>
        public FxLexer(string keywordDef, Dictionary<string, KeyDef> defs)
        {
            Styles style;

            if (Defs == null)
            {
                // adapt style IDs
                foreach (var def in (Defs = defs))
                    def.Value.Id = Enum.TryParse(def.Key, true, out style)
                        ? (int)style : KeywordStylesStart + def.Value.Id;

                // allocate hashsets for keywords
                keywords = new HashSet<string>[Defs.Select(x => x.Value.Id).Max() + 1];

                // search for keywords and fill hashsets
                foreach (var def in Defs)
                {
                    var id = $"{def.Value.Id}";
                    var words = from x in Regex.Matches(keywordDef, @"\[" + id + @"\]\w+").Cast<Match>()
                                select x.Value.Substring(id.Length + 2);
                    keywords[def.Value.Id] = new HashSet<string>(words);
                }
            }
        }

        /// <summary>
        /// Style editor text between start and end position.
        /// </summary>
        /// <param name="editor"></param>
        /// <param name="pos"></param>
        /// <param name="endPos"></param>
        /// <param name="keyStyles"></param>
        /// <returns></returns>
        public static int Style(CodeEditor editor, int pos, int endPos)
        {
            // additional special states
            var possibleStartComment = false;
            var possibleEndComment = false;

            // GET CURRENT STATE //
            pos = editor.GetLinePosition(pos);
            while (pos > 0 && editor.GetStyleAt(pos) != 0)
                pos--;
            var state = Default;

            // START STYLING //

            editor.StartStyling(pos);

            // for each character that needs to be styled
            for (int length = 0, textLength = editor.TextLength;
                (pos < endPos || state != Default) && pos < textLength;
                pos++)
            {
                var c = (char)editor.GetCharAt(pos);

                // check if the state needs to be changed to a comment state
                if (possibleStartComment)
                {
                    possibleStartComment = false;
                    // if new state is a comment state
                    // that changes the previous state
                    var curState = state;
                    state = c == '/' ? LineComment : c == '*' ? BlockComment : state;
                    if (state != curState)
                    {
                        // style for previous state
                        editor.StartStyling(pos - length);
                        editor.SetStyling(length, (int)curState);
                        // style using the new comment state
                        editor.StartStyling(pos - 1);
                        editor.SetStyling(2, (int)state);
                        continue;
                    }
                }

                // check if the state needs to be changed to default state
                if (possibleEndComment)
                {
                    possibleEndComment = false;
                    // if is end of comment
                    if (c == '/')
                    {
                        editor.SetStyling(1, (int)state);
                        state = Default;
                        //endPos = textLength;
                        continue;
                    }
                }

                REPROCESS:

                switch (state)
                {
                    // UNKNOWN STATE
                    case Default:
                        switch (c)
                        {
                            // is string
                            case '"':
                            case '\'':
                                state = Styles.String;
                                editor.SetStyling(1, (int)state);
                                break;

                            // is preprocessor
                            case '#':
                                state = Preprocessor;
                                goto REPROCESS;

                            default:
                                // is operator
                                if (IsMathSymbol(c))
                                    state = Operator;
                                // is punctuation
                                else if (Punctuation.Any(x => x == char.GetUnicodeCategory(c)))
                                {
                                    editor.SetStyling(1, (int)Styles.Punctuation);
                                    break;
                                }
                                // is number
                                else if (char.IsDigit(c))
                                    state = Number;
                                // is letter
                                else if (char.IsLetter(c))
                                    state = Identifier;
                                // is default styling
                                else
                                {
                                    editor.SetStyling(1, (int)Default);
                                    break;
                                }
                                goto REPROCESS;
                        }
                        break;

                    // LINE COMMENT STATE
                    case LineComment:
                        editor.SetStyling(1, (int)state);
                        // end of line comment
                        if (c == '\n')
                            state = Default;
                        break;

                    // BLOCK COMMENT STATE
                    case BlockComment:
                        // could indicate the end of a block comment
                        possibleEndComment = c == '*';
                        editor.SetStyling(1, (int)state);
                        break;

                    // STRING STATE
                    case Styles.String:
                        editor.SetStyling(1, (int)state);
                        // end of string
                        if (c == '"' || c == '\'' || c == '\n')
                            state = Default;
                        break;

                    // PREPROCESSOR STATE
                    case Preprocessor:
                        editor.SetStyling(1, (int)state);
                        // end of preprocessor
                        if (c == ' ' || c == '\n')
                            state = Default;
                        break;

                    // OPERATOR STATE
                    case Operator:
                        // could indicate the start of a comment
                        possibleStartComment = c == '/';
                        // is still a math symbol
                        if (IsMathSymbol(c))
                            editor.SetStyling(1, (int)state);
                        // end of operator
                        else
                        {
                            state = Default;
                            goto REPROCESS;
                        }
                        break;

                    // NUMBER STATE
                    case Number:
                        // still a number
                        if (char.IsDigit(c)
                            || (c >= 'a' && c <= 'f')
                            || (c >= 'A' && c <= 'F')
                            || c == 'x' || c == '.')
                            length++;
                        // end of number
                        else
                        {
                            editor.SetStyling(length, (int)state);
                            length = 0;
                            state = Default;
                            goto REPROCESS;
                        }
                        break;

                    // IDENTIFIER STATE
                    case Identifier:
                        // still an identifier and possible keyword
                        if (char.IsLetterOrDigit(c) || c == '_')
                            length++;
                        // end of identifier/keyword
                        else
                        {
                            // get possible keyword string from identifier range
                            var style = Identifier;
                            var identifier = editor.GetTextRange(pos - length, length);

                            // if part of a keyword list, use the respective style
                            for (int i = 0; i < keywords.Length; i++)
                            {
                                if (keywords[i].Contains(identifier))
                                {
                                    style = (Styles)(KeywordStylesStart + i);
                                    break;
                                }
                            }

                            // set styling
                            editor.SetStyling(length, (int)style);
                            length = 0;
                            state = Default;
                            goto REPROCESS;
                        }
                        break;
                }
            }

            return pos;
        }
        
        /// <summary>
        /// Add folding between start and end position.
        /// </summary>
        /// <param name="editor"></param>
        /// <param name="pos"></param>
        /// <param name="endPos"></param>
        public static void Folding(Scintilla editor, int pos, int endPos)
        {
            // setup state machine
            var line = editor.LineFromPosition(pos);
            var lastLine = -1;
            var lastCharPos = line;
            var foldLevel = editor.Lines[line].FoldLevel;
            var textLength = editor.TextLength;

            // for each character
            for (var state = FoldState.Unknown;
                state == FoldState.Unknown ? pos < endPos : pos < textLength;
                pos++)
            {
                var c = (char)editor.GetCharAt(pos);

                switch (c)
                {
                    // open folding
                    case '{': state = FoldState.StartFolding; break;
                    // close folding
                    case '}': state = FoldState.EndFolding; break;
                    // next line
                    case '\n': line++; break;
                        // remember last character to place
                        // fold icon in the last character line
                    default:
                        if (char.IsLetterOrDigit(c))
                            lastCharPos = pos;
                        break;
                }

                switch (state)
                {
                    // STATE: open folding
                    case FoldState.StartFolding:
                        var lastCharLine = editor.LineFromPosition(lastCharPos);
                        // start folding at last character containing line
                        editor.Lines[lastCharLine].FoldLevelFlags = FoldLevelFlags.Header;
                        editor.Lines[lastCharLine].FoldLevel = foldLevel++;
                        // for all other lines up to the current position also add folding
                        for (int i = lastCharLine + 1; i <= line; i++)
                        {
                            editor.Lines[i].FoldLevelFlags = FoldLevelFlags.White;
                            editor.Lines[i].FoldLevel = foldLevel;
                        }
                        lastLine = line;
                        // switch to folding state
                        state = FoldState.Foldable;
                        break;
                    // STATE: close folding
                    case FoldState.EndFolding:
                        // end folding
                        editor.Lines[line].FoldLevel = foldLevel;
                        // decrease fold level
                        foldLevel = Math.Max(--foldLevel, 1024);
                        lastLine = line;
                        // switch to folding state (which will
                        // switch to unknown state if the most
                        // outer fold level is reached)
                        state = FoldState.Foldable;
                        break;
                    // STATE: folding
                    case FoldState.Foldable:
                        // still in folding state
                        if (foldLevel > 1024)
                        {
                            if (line != lastLine)
                            {
                                // set fold level for line
                                editor.Lines[line].FoldLevel = foldLevel;
                                lastLine = line;
                            }
                        }
                        // end folding state
                        else
                            state = FoldState.Unknown;
                        break;
                }
            }
        }

        /// <summary>
        /// Check if character is a math symbol.
        /// </summary>
        /// <param name="c"></param>
        /// <returns>True if character is a math symbol.</returns>
        private static bool IsMathSymbol(char c) 
            => char.IsSymbol(c) || c == '/' || c == '*' || c == '-' || c == '&' || c == '|';
        
        public class KeyDef
        {
            public int Id;
            public string Prefix;
            public Color ForeColor;
            public Color BackColor;
        }
    }

    public class Fx2Lexer
    {
        private Node root;

        /// <summary>
        /// Default constructor.
        /// Parses the keyword.txt file and sets up the lexer.
        /// </summary>
        public Fx2Lexer()
        {
            root = Node.LoadText(Properties.Resources.keywords2);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="editor"></param>
        /// <param name="pos"></param>
        /// <param name="endPos"></param>
        private void Style(CodeEditor editor, int pos, int endPos)
        {
        }

        /// <summary>
        /// Get the style of the word at the specified position. If the word
        /// could not be found the default style 0 will be returned.
        /// </summary>
        /// <param name="editor"></param>
        /// <param name="position"></param>
        /// <returns>Returns the style for the word at the specified position or 0.</returns>
        public int GetKeywordStyle(string text, string word, int position)
            => GetPotentialKeywordDefs(text, word, position).FirstOrDefault().style;

        /// <summary>
        /// Get the hint of the word at the specified position. If the word
        /// could not be found <code>null</code> be returned.
        /// </summary>
        /// <param name="editor"></param>
        /// <param name="position"></param>
        /// <returns>Returns the style for the word at the specified
        /// position or <code>null</code>.</returns>
        public string GetKeywordHint(string text, string word, int position) 
            => GetPotentialKeywordDefs(text, word, position).FirstOrDefault().hint;

        /// <summary>
        /// Find all potential keywords starting with the word at the specified position.
        /// </summary>
        /// <param name="editor"></param>
        /// <param name="position"></param>
        /// <returns>Returns a list of keywords.</returns>
        public IEnumerable<string> GetPotentialKeywords(string text, string word, int position)
        {
            foreach (var def in GetPotentialKeywordDefs(text, word, position))
                yield return def.word;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="word"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        private IEnumerable<Node.KeywordDef> GetPotentialKeywordDefs(string text, string word, int position)
        {
            foreach (var def in root.GetKeywordDefs(text, position, word))
                yield return def;
        }

        private class Node
        {
            private Regex regex;
            private Dictionary<int, Trie<Keyword>> keywordTries;
            private Node[] children;
            const char SPLIT = '║';
            const char NL = '¶';

            /// <summary>
            /// 
            /// </summary>
            /// <param name="text"></param>
            /// <returns></returns>
            public static Node LoadText(string text)
            {
                int cur = 0;
                var lines = text.Split(new[] { '\n' });
                return new Node(lines, ref cur);
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="lines"></param>
            /// <param name="cur"></param>
            private Node(string[] lines, ref int cur)
            {
                keywordTries = new Dictionary<int, Trie<Keyword>>();
                var children = new List<Node>();
                var headerLine = lines[cur];

                // get number of tabs that define a keyword line
                var headerTabs = headerLine.LastIndexOf('┬');

                // get header
                var header = headerLine.Substring(headerTabs + 1).Trim().Split(SPLIT);

                // compile regular expression
                regex = new Regex(header[0], RegexOptions.Singleline);

                // process all lines
                for (cur++; cur < lines.Length; cur++)
                {
                    // get number of tabs of the line
                    var tabs = lines[cur].LastIndexOf('┬');
                    // add keyword line to keywords
                    if (tabs == -1)
                        AddKeyword(lines, ref cur);
                    // belongs to child node
                    else if (tabs > headerTabs)
                        children.Add(new Node(lines, ref cur));
                    // belongs to parent node
                    else
                    {
                        cur--;
                        break;
                    }
                }

                // convert child node lint to array
                this.children = children.ToArray();
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="lines"></param>
            /// <param name="cur"></param>
            private void AddKeyword(string[] lines, ref int cur)
            {
                // GET LINE STRING

                var line = lines[cur];
                // as long as there are new line chars append the line string
                for (int i = line.IndexOf(NL); i >= 0 && cur < lines.Length - 1; i = line.IndexOf(NL))
                {
                    var text = lines[++cur];
                    line = line.Substring(0, i) + '\n';
                    line += text.Substring(text.LastIndexOf(SPLIT)+1);
                }
                
                // process keyword definition
                var offset = line.IndexOf('├');
                var def = line.Substring(offset + 1).Trim().Split(SPLIT);
                var style = int.Parse(def[0]);

                // create new trie if none exists for this style
                if (!keywordTries.ContainsKey(style))
                    keywordTries.Add(style, new Trie<Keyword>());

                // add keyword to trie
                var keyword = new Keyword { word = def[1], hint = def.Length > 2 ? def[2] : "" };
                keywordTries[style].Add(def[1], keyword);
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="text"></param>
            /// <param name="position"></param>
            /// <returns></returns>
            private Match IsActiveNode(string text, int position)
            {
                foreach (Match match in regex.Matches(text))
                {
                    if (match.Index <= position && position < match.Index + match.Length)
                        return match;
                }
                return null;
            }
            
            /// <summary>
            /// 
            /// </summary>
            /// <param name="text"></param>
            /// <param name="position"></param>
            /// <param name="word"></param>
            /// <returns></returns>
            public IEnumerable<KeywordDef> GetKeywordDefs(string text, int position, string word)
            {
                // is the keyword within a block handled by this node?
                var match = IsActiveNode(text, position);
                if (match == null)
                    // word not handled by this node
                    yield break;

                // is the keyword within a block handled by a child node?
                foreach (var child in children)
                {
                    var keywords = child.GetKeywordDefs(match.Value, position - match.Index, word);
                    foreach (var keyword in keywords)
                        yield return keyword;
                    if (keywords.Count() > 0)
                        yield break;
                }

                // check whether the word is a keyword of this node
                foreach (var trie in keywordTries)
                {
                    // try to find the keyword
                    var keywords = trie.Value[word];
                    // if a keyword could be found
                    foreach (var keyword in keywords)
                    {
                        yield return new KeywordDef
                        {
                            style = trie.Key,
                            word = keyword.word,
                            hint = keyword.hint
                        };
                    }
                }
            }

            private struct Keyword
            {
                public string word;
                public string hint;
            }

            public struct KeywordDef
            {
                public int style;
                public string word;
                public string hint;
            }
        }
    }
}
