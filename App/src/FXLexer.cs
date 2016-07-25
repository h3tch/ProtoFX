using ScintillaNET;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace App.Lexer
{
    interface ILexer
    {
        /// <summary>
        /// Style the specified text position.
        /// </summary>
        /// <param name="editor"></param>
        /// <param name="pos"></param>
        /// <param name="endPos"></param>
        int Style(CodeEditor editor, int pos, int endPos);
        /// <summary>
        /// Add folding to the specified text position.
        /// </summary>
        /// <param name="editor"></param>
        /// <param name="pos"></param>
        /// <param name="endPos"></param>
        void Fold(CodeEditor editor, int pos, int endPos);
        /// <summary>
        /// Get a list of styles.
        /// </summary>
        /// <returns></returns>
        IEnumerable<int> GetStyles();
        /// <summary>
        /// Get name of the specified style.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        string GetStyleName(int id);
        /// <summary>
        /// Get foreground color of the specified style.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Color GetStyleForeColor(int id);
        /// <summary>
        /// Get background color of the specified style.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Color GetStyleBackColor(int id);
        /// <summary>
        /// Get the hint of the word at the specified position. If the word
        /// could not be found <code>null</code> be returned.
        /// </summary>
        /// <param name="editor"></param>
        /// <param name="position"></param>
        /// <returns>Returns the style for the word at the specified
        /// position or <code>null</code>.</returns>
        string GetKeywordHint(int id, string word = null);
        /// <summary>
        /// Find all potential keywords starting with the word at the specified position.
        /// </summary>
        /// <param name="editor"></param>
        /// <param name="position"></param>
        /// <returns>Returns a list of keywords.</returns>
        IEnumerable<string> SelectKeywords(int id, string word = null);
        IEnumerable<Keyword> SelectKeywordInfo(int id, string word);
        bool InStyleRange(int style);
        bool IsLexerFor(string type, string anno);
    }

    public struct Keyword
    {
        public string word;
        public string hint;
    }
    /*public class FxLexer : ILexer
    {
        private Node root;
        private Dictionary<int, StyleDef> styleDef = new Dictionary<int, StyleDef>();
        private StyleId styles = new StyleId();

        /// <summary>
        /// Default constructor.
        /// Parses the keyword.txt file and sets up the lexer.
        /// </summary>
        public FxLexer()
        {
            // predefined variables
            var fields = BindingFlags.IgnoreCase | BindingFlags.Public
                | BindingFlags.GetField | BindingFlags.Instance;

            // LOAD KEYWORD HIERARCHY //

            root = Node.LoadText(Properties.Resources.keywords);

            // PARSE STYLE COLORS AND IDS //

            // convert text file to processable format
            var lines = Properties.Resources.colors.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            var rows = lines.Select(x => x.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries));

            // process each row
            foreach (var row in rows)
            {
                // get style id
                var id = int.Parse(row[0].Trim());

                // get style definition
                var def = new StyleDef
                {
                    Name = row[1].Trim(),
                    ForeColor = row.Length > 2 ? ColorTranslator.FromHtml(row[2].Trim()) : Color.Gray,
                    BackColor = row.Length > 3 ? ColorTranslator.FromHtml(row[2].Trim()) : Color.White,
                };

                // add style information to lexer
                styleDef.Add(id, def);
                styles.GetType().GetField(def.Name, fields)?.SetValue(styles, id);
            }
        }

        #region ILexer Methods
        public void Style(CodeEditor editor, int pos, int endPos)
        {
            // additional special states
            var possibleStartComment = false;
            var possibleEndComment = false;

            // GET CURRENT STATE //

            pos = editor.GetLinePosition(pos);
            while (pos > 0 && editor.GetStyleAt(pos) != 0)
                pos--;
            var state = StyleState.Default;

            // START STYLING //

            editor.StartStyling(pos);

            // for each character that needs to be styled
            for (int length = 0, textLength = editor.TextLength;
                (pos < endPos || state != StyleState.Default) && pos < textLength;
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
                    state = c == '/' ? StyleState.LineComment
                          : c == '*' ? StyleState.BlockComment
                          : state;
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
                        state = StyleState.Default;
                        //endPos = textLength;
                        continue;
                    }
                }

                REPROCESS:

                switch (state)
                {
                    // UNKNOWN STATE
                    case StyleState.Default:
                        switch (c)
                        {
                            // is string
                            case '"':
                            case '\'':
                                state = StyleState.String;
                                editor.SetStyling(1, styles.String);
                                break;

                            // is preprocessor
                            case '#':
                                state = StyleState.Preprocessor;
                                goto REPROCESS;

                            default:
                                // is operator
                                if (IsMathSymbol(c))
                                    state = StyleState.Operator;
                                // is punctuation
                                else if (PunctuationCategory.Any(x => x == char.GetUnicodeCategory(c)))
                                {
                                    editor.SetStyling(1, styles.Punctuation);
                                    break;
                                }
                                // is number
                                else if (char.IsDigit(c))
                                    state = StyleState.Number;
                                // is letter
                                else if (char.IsLetter(c))
                                    state = StyleState.Identifier;
                                // is default styling
                                else
                                {
                                    editor.SetStyling(1, styles.Default);
                                    break;
                                }
                                goto REPROCESS;
                        }
                        break;

                    // LINE COMMENT STATE
                    case StyleState.LineComment:
                        editor.SetStyling(1, styles.LineComment);
                        // end of line comment
                        if (c == '\n')
                            state = StyleState.Default;
                        break;

                    // BLOCK COMMENT STATE
                    case StyleState.BlockComment:
                        // could indicate the end of a block comment
                        possibleEndComment = c == '*';
                        editor.SetStyling(1, styles.BlockComment);
                        break;

                    // STRING STATE
                    case StyleState.String:
                        editor.SetStyling(1, styles.String);
                        // end of string
                        if (c == '"' || c == '\'' || c == '\n')
                            state = StyleState.Default;
                        break;

                    // PREPROCESSOR STATE
                    case StyleState.Preprocessor:
                        editor.SetStyling(1, styles.Preprocessor);
                        // end of preprocessor
                        if (c == ' ' || c == '\n')
                            state = StyleState.Default;
                        break;

                    // OPERATOR STATE
                    case StyleState.Operator:
                        // could indicate the start of a comment
                        possibleStartComment = c == '/';
                        // is still a math symbol
                        if (IsMathSymbol(c))
                            editor.SetStyling(1, styles.Operator);
                        // end of operator
                        else
                        {
                            state = StyleState.Default;
                            goto REPROCESS;
                        }
                        break;

                    // NUMBER STATE
                    case StyleState.Number:
                        // still a number
                        if (char.IsDigit(c)
                            || (c >= 'a' && c <= 'f')
                            || (c >= 'A' && c <= 'F')
                            || c == 'x' || c == '.')
                            length++;
                        // end of number
                        else
                        {
                            editor.SetStyling(length, styles.Number);
                            length = 0;
                            state = StyleState.Default;
                            goto REPROCESS;
                        }
                        break;

                    // IDENTIFIER STATE
                    case StyleState.Identifier:
                        // still an identifier and possible keyword
                        if (char.IsLetterOrDigit(c) || c == '_')
                            length++;
                        // end of identifier/keyword
                        else
                        {
                            // get possible keyword string from identifier range
                            var identifier = editor.GetTextRange(pos - length, length);

                            // if part of a keyword list, use the respective style
                            var style = GetKeywordStyle(editor.Text, pos - length, identifier);
                            if (style == 0)
                                style = styles.Identifier;

                            // set styling
                            editor.SetStyling(length, style);
                            length = 0;
                            state = StyleState.Default;
                            goto REPROCESS;
                        }
                        break;
                }
            }
        }

        public void Fold(CodeEditor editor, int pos, int endPos)
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
                    case '{':
                        state = FoldState.StartFolding;
                        break;
                    // close folding
                    case '}':
                        state = FoldState.EndFolding;
                        break;
                    // next line
                    case '\n':
                        line++;
                        break;
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

        public int[] GetStyles() => styleDef.Keys.ToArray();

        public string GetStyleName(int id) => styleDef[id].Name;

        public Color GetStyleForeColor(int id) => styleDef[id].ForeColor;

        public Color GetStyleBackColor(int id) => styleDef[id].BackColor;

        public int GetKeywordStyle(string text, int position, string word)
            => GetPotentialKeywordDefs(text, word, position).FirstOrDefault().style;

        public string GetKeywordHint(string text, int position, string word)
            => GetPotentialKeywordDefs(text, word, position).FirstOrDefault().hint;

        public IEnumerable<string> GetPotentialKeywords(string text, int position, string word)
        {
            foreach (var def in GetPotentialKeywordDefs(text, word, position))
                yield return def.word;
        }
        #endregion

        /// <summary>
        /// Find all potential keywords starting with the word at the specified position.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="word"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        private IEnumerable<Node.KeywordDef> GetPotentialKeywordDefs(string text, string word, int position)
        {
            foreach (var def in root.GetKeywordDefs(text, word, position, 0, text.Length))
                yield return def;
        }

        /// <summary>
        /// Check if character is a math symbol.
        /// </summary>
        /// <param name="c"></param>
        /// <returns>True if character is a math symbol.</returns>
        private static bool IsMathSymbol(char c)
            => char.IsSymbol(c) || c == '/' || c == '*' || c == '-' || c == '&' || c == '|';

        #region INNER CLASSES
        private class Node
        {
            private string ex;
            private Regex regex;
            private Dictionary<int, Trie<Keyword>> keywordTries;
            private Node[] children;
            const char SPLIT = '║';
            const char END = '¶';

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
                var headerTabs = headerLine.IndexOf('┬');

                // get header
                var header = headerLine.Substring(headerTabs + 1).Trim().Split(SPLIT);

                // compile regular expression
                regex = new Regex(ex = header[0], RegexOptions.Singleline);

                // process all lines
                for (cur++; cur < lines.Length; cur++)
                {
                    // get number of tabs of the line
                    var brunch = lines[cur].IndexOf('┬');
                    var leafe = lines[cur].IndexOf('├');
                    // add keyword line to keywords
                    if (brunch == -1 && leafe >= headerTabs)
                    {
                        AddKeyword(lines, ref cur);
                    }
                    // belongs to child node
                    else if (brunch > headerTabs)
                    {
                        children.Add(new Node(lines, ref cur));
                    }
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
                while (line.LastIndexOf(END) < 0)
                {
                    var text = lines[++cur];
                    line += '\n' + text.Substring(text.LastIndexOf(SPLIT) + 1);
                }
                line = line.Substring(0, line.LastIndexOf(END));

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
            private Match IsActiveNode(string text, int position, int startIndex, int length)
            {
                for (var match = regex.Match(text, startIndex, length);
                    match.Success && match.Index < position;
                    match = match.NextMatch())
                {
                    if (match.Index <= position && position < match.Index + match.Length)
                        return new Match { Index = match.Index, Length = match.Length };
                }
                return default(Match);
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="text"></param>
            /// <param name="position"></param>
            /// <param name="word"></param>
            /// <returns></returns>
            public IEnumerable<KeywordDef> GetKeywordDefs(string text, string word, int position, int startIndex, int length)
            {
                // if no word has been specified
                if (word == null)
                    word = text.WordFromPosition(position);

                // is the keyword within a block handled by this node?
                var match = IsActiveNode(text, position, startIndex, length);
                if (match.IsDefault())
                    // word not handled by this node
                    yield break;

                // is the keyword within a block handled by a child node?
                foreach (var child in children)
                {
                    var keywords = child.GetKeywordDefs(text, word, position, match.Index, match.Length);
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

            #region INNDER STRUCTURES
            private struct Match
            {
                public int Index;
                public int Length;
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
            #endregion
        }

        #region INNER HELPER CLASSES
        private static UnicodeCategory[] PunctuationCategory = new[] {
            UnicodeCategory.OpenPunctuation,
            UnicodeCategory.ClosePunctuation,
            UnicodeCategory.ConnectorPunctuation,
        };

        private enum StyleState : int
        {
            Default,
            Identifier,
            Number,
            String,
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

        private struct StyleDef
        {
            public string Name;
            public Color ForeColor;
            public Color BackColor;
        }

        private class StyleId
        {
            public int Default = -1;
            public int Identifier = -1;
            public int Number = -1;
            public int String = -1;
            public int LineComment = -1;
            public int BlockComment = -1;
            public int Preprocessor = -1;
            public int Operator = -1;
            public int Punctuation = -1;
        }
        #endregion
        #endregion
    }*/

    public class FxLexer : ILexer
    {
        private ILexer[] lexer = new [] { new TechLexer() };

        public int Style(CodeEditor editor, int pos, int endPos)
        {
            while (pos > 0 && editor.GetStyleAt(pos) == 0)
                pos--;
            
            while (pos < endPos)
            {
                var style = editor.GetStyleAt(pos);
                var lex = lexer.Where(x => x.InStyleRange(style)).First();
                pos = lex.Style(editor, pos, endPos);
            }

            return pos;
        }

        public void Fold(CodeEditor editor, int pos, int endPos)
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
                    case '{':
                        state = FoldState.StartFolding;
                        break;
                    // close folding
                    case '}':
                        state = FoldState.EndFolding;
                        break;
                    // next line
                    case '\n':
                        line++;
                        break;
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
        
        public string GetKeywordHint(int id, string word)
            => lexer.Select(x => x.GetKeywordHint(id, word)).FirstOrDefault(x => x != null);

        public IEnumerable<string> SelectKeywords(int id, string word)
            => lexer.SelectMany(x => x.SelectKeywords(id, word));

        public IEnumerable<Keyword> SelectKeywordInfo(int id, string word)
            => lexer.SelectMany(x => x.SelectKeywordInfo(id, word));

        public IEnumerable<int> GetStyles()
            => lexer.SelectMany(x => x.GetStyles());

        public string GetStyleName(int id)
            => lexer.Select(x => x.GetStyleName(id)).FirstOrDefault(x => x != null);

        public Color GetStyleForeColor(int id)
            => lexer.Select(x => x.GetStyleForeColor(id)).FirstOrDefault(x => x != null);

        public Color GetStyleBackColor(int id)
            => lexer.Select(x => x.GetStyleBackColor(id)).FirstOrDefault(x => x != null);

        public bool InStyleRange(int style) => false;

        public bool IsLexerFor(string type, string anno) => false;

        private enum FoldState : int
        {
            Unknown,
            StartFolding,
            Foldable,
            EndFolding,
        }
    }

    public class TechLexer : ILexer
    {
        private ILexer[] lexer = new [] { new BlockLexer() };
        private int braceCount;

        public int Style(CodeEditor editor, int pos, int endPos)
        {
            for (var state = (State)editor.GetStyleAt(pos); pos < endPos; pos++)
            {
                state = ProcessState(editor, state, (char)editor.GetCharAt(pos));
                editor.SetStyling(1, (int)state);
            }
            return pos;
        }

        public void Fold(CodeEditor editor, int pos, int endPos) { }

        public string GetKeywordHint(int id, string word)
            => SelectKeywordInfo(id, word).FirstOrDefault().hint;

        public IEnumerable<string> SelectKeywords(int id, string word)
            => SelectKeywordInfo(id, word).Select(x => x.word);

        public IEnumerable<Keyword> SelectKeywordInfo(int id, string word)
        {
            if ((int)State.Default <= id && id < (int)State.End)
                return keywords[id - (int)State.End][word];
            return lexer.SelectMany(x => x.SelectKeywordInfo(id, word));
        }

        public IEnumerable<int> GetStyles()
            => Enum.GetValues(typeof(State)).Cast<int>().Concat(lexer.SelectMany(x => x.GetStyles()));

        public string GetStyleName(int id)
        {
            if ((int)State.Default <= id && id < (int)State.End)
                return ((State)id).ToString();
            return lexer.Select(x => x.GetStyleName(id)).FirstOrDefault(x => x != null);
        }

        public Color GetStyleForeColor(int id)
        {
            if ((int)State.Default <= id && id < (int)State.End)
                return foreColor[id];
            return lexer.Select(x => x.GetStyleForeColor(id)).FirstOrDefault(x => x != null);
        }

        public Color GetStyleBackColor(int id)
        {
            if ((int)State.Default <= id && id < (int)State.End)
                return backColor[id];
            return lexer.Select(x => x.GetStyleBackColor(id)).FirstOrDefault(x => x != null);
        }

        public bool InStyleRange(int style) => (int)State.Default <= style && style < (int)State.End;

        public bool IsLexerFor(string type, string anno) => false;

        #region PROCESS STATE
        private State ProcessState(CodeEditor editor, State state, char c)
        {
            switch (state)
            {
                case State.Comment:
                    return ProcessCommentState(editor, c);
                case State.LineComment:
                    return ProcessLineCommentState(editor, c);
                case State.BlockComment:
                    return ProcessBlockCommentState(editor, c);
                case State.PotentialEndBlockComment:
                    return ProcessPotentialEndBlockCommentState(editor, c);
                case State.EndBlockComment:
                    return ProcessEndBlockCommentState(editor, c);
                case State.Type:
                    return ProcessTypeState(editor, c);
                case State.TypeSpace:
                    return ProcessTypeSpaceState(editor, c);
                case State.Name:
                    return ProcessNameState(editor, c);
                case State.NameSpace:
                    return ProcessNameSpaceState(editor, c);
                case State.Anno:
                    return ProcessAnnoState(editor, c);
                case State.AnnoSpace:
                    return ProcessAnnoSpaceState(editor, c);
                case State.OpenBrace:
                    return ProcessOpenBraceState(editor, c);
                case State.CloseBrace:
                    return ProcessCloseBraceState(editor, c);
                case State.BlockCode:
                    return ProcessBlockCodeState(editor, c);
                default:
                    return ProcessDefaultState(editor, c);
            }
        }

        private State ProcessDefaultState(CodeEditor editor, char c)
        {
            if (c == '/')
                return State.Comment;
            else if (char.IsLetter(c))
                return State.Type;
            else
                return State.Default;
        }

        private State ProcessCommentState(CodeEditor editor, char c)
        {
            switch (c)
            {
                case '/':
                    return State.LineComment;
                case '*':
                    return State.BlockComment;
            }
            return ProcessDefaultState(editor, c);
        }

        private State ProcessLineCommentState(CodeEditor editor, char c)
        {
            return c == '\n' ? State.Default : State.LineComment;
        }

        private State ProcessBlockCommentState(CodeEditor editor, char c)
        {
            return c == '*' ? State.PotentialEndBlockComment : State.BlockComment;
        }

        private State ProcessPotentialEndBlockCommentState(CodeEditor editor, char c)
        {
            switch (c)
            {
                case '/':
                    return State.EndBlockComment;
                case '*':
                    return State.PotentialEndBlockComment;
                default:
                    return State.BlockComment;
            }
        }

        private State ProcessEndBlockCommentState(CodeEditor editor, char c)
        {
            return ProcessDefaultState(editor, c);
        }

        private State ProcessTypeState(CodeEditor editor, char c)
        {
            if (char.IsWhiteSpace(c))
                return State.TypeSpace;
            else if (c == '{')
                return State.OpenBrace;
            else
                return State.Type;
        }

        private State ProcessTypeSpaceState(CodeEditor editor, char c)
        {
            if (c == '{')
                return State.OpenBrace;
            else if (char.IsLetter(c))
                return State.Name;
            else
                return State.TypeSpace;
        }

        private State ProcessNameState(CodeEditor editor, char c)
        {
            if (char.IsWhiteSpace(c))
                return State.NameSpace;
            else if (c == '{')
                return State.OpenBrace;
            else
                return State.Name;
        }

        private State ProcessNameSpaceState(CodeEditor editor, char c)
        {
            if (c == '{')
                return State.OpenBrace;
            else if (char.IsLetter(c))
                return State.Anno;
            else
                return State.NameSpace;
        }

        private State ProcessAnnoState(CodeEditor editor, char c)
        {
            if (char.IsWhiteSpace(c))
                return State.AnnoSpace;
            else if (c == '{')
                return State.OpenBrace;
            else
                return State.Anno;
        }

        private State ProcessAnnoSpaceState(CodeEditor editor, char c)
        {
            if (c == '{')
                return State.OpenBrace;
            else
                return State.AnnoSpace;
        }

        private State ProcessOpenBraceState(CodeEditor editor, char c)
        {
            if (c == '}')
                return State.CloseBrace;
            braceCount = 1;
            return State.BlockCode;
        }

        private State ProcessCloseBraceState(CodeEditor editor, char c)
        {
            return ProcessDefaultState(editor, c);
        }

        private State ProcessBlockCodeState(CodeEditor editor, char c)
        {
            switch (c)
            {
                case '{':
                    braceCount++;
                    break;
                case '}':
                    if (--braceCount > 0)
                        break;
                    // RE-LEX ALL BLOCKCODE PARTS
                    // get code region of the block
                    var end = editor.GetEndStyled();
                    var start = FindLastStyleOf(editor, (int)State.OpenBrace, end, end);
                    // get header string from block position
                    var header = FindLastHeaderFromPosition(editor, start);
                    // find lexer that can lex this code block
                    var lex = lexer?.Where(x => x.IsLexerFor(header[0], header[1])).FirstOrDefault();
                    // re-lex code block
                    lex?.Style(editor, start + 1, end - 1);
                    return State.CloseBrace;
            }
            return State.BlockCode;
        }
        #endregion

        #region HELPER FUNCTION
        private string[] FindLastHeaderFromPosition(CodeEditor editor, int pos)
        {
            int bracePos = FindLastStyleOf(editor, (int)State.OpenBrace, pos, pos);
            int typePos = FindLastStyleOf(editor, (int)State.Type, bracePos, bracePos);
            int annoPos = FindLastStyleOf(editor, (int)State.Anno, bracePos, bracePos - typePos);

            return new string[] {
                typePos >= 0 ? editor.GetWordFromPosition(typePos) : null,
                annoPos >= 0 ? editor.GetWordFromPosition(annoPos) : null,
            };
        }

        private int FindLastStyleOf(CodeEditor editor, int style, int from, int length)
        {
            int to = Math.Max(from - Math.Max(length, 0), 0);
            while (from > to && editor.GetStyleAt(from) != style)
                from--;
            return editor.GetStyleAt(from) == style ? from : -1;
        }

        public enum State : int
        {
            Default,
            Comment,
            LineComment,
            BlockComment,
            PotentialEndBlockComment,
            EndBlockComment,
            Type,
            TypeSpace,
            Name,
            NameSpace,
            Anno,
            AnnoSpace,
            OpenBrace,
            CloseBrace,
            BlockCode,
            End
        }

        private Color[] foreColor = new Color[(int)State.End - (int)State.Default];
        private Color[] backColor = new Color[(int)State.End - (int)State.Default];
        private Trie<Keyword>[] keywords = new Trie<Keyword>[(int)State.End - (int)State.Default];
        #endregion
    }

    public class BlockLexer : ILexer
    {
        public int Style(CodeEditor editor, int pos, int endPos)
        {
            return pos;
        }

        public void Fold(CodeEditor editor, int pos, int endPos) { }

        public IEnumerable<int> GetStyles()
        {
            throw new NotImplementedException();
        }

        public string GetStyleName(int id)
        {
            throw new NotImplementedException();
        }

        public Color GetStyleForeColor(int id)
        {
            throw new NotImplementedException();
        }

        public Color GetStyleBackColor(int id)
        {
            throw new NotImplementedException();
        }

        public string GetKeywordHint(int id, string word = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> SelectKeywords(int id, string word = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Keyword> SelectKeywordInfo(int id, string word)
        {
            throw new NotImplementedException();
        }

        public bool InStyleRange(int style) => (int)State.Default <= style && style < (int)State.End;

        public bool IsLexerFor(string type, string anno) => false;

        public enum State : int
        {
            Default = TechLexer.State.End,
            End
        }
    }
    /*
    public class TechLexer2 : ILex
    {
        private TextRange Block = default(TextRange);
        private TextRange Anno = default(TextRange);
        private TextRange Name = default(TextRange);
        private TextRange Command = default(TextRange);
        private int braceCount = 0;
        private ILex parent;

        private enum State : int
        {
            Default,
            Indicator,
            PotentialComment,
            PotentialEndBlockComment,
            LineComment,
            BlockComment,
            BlockCode,
            Command,
            Argument,
            ShaderCode,
        }

        public void Style(CodeEditor editor, int pos, int endPos)
        {
            var state = (State)editor.GetStyleAt(pos - 1);
            for (int i = pos; i < endPos; i++)
                state = ProcessState(editor, state, (char)editor.GetCharAt(i));
        }

        private State ProcessState(CodeEditor editor, State state, char c)
        {
            switch (state)
            {
                case State.Indicator:
                    return ProcessIndicatorState(editor, c);
                case State.PotentialComment:
                    return ProcessPotentialCommentState(editor, c);
                case State.LineComment:
                    return ProcessLineCommentState(editor, c);
                case State.BlockComment:
                    return ProcessBlockCommentState(editor, c);
                case State.PotentialEndBlockComment:
                    return ProcessPotentialEndBlockCommentState(editor, c);
                case State.BlockCode:
                    return ProcessBlockCodeState(editor, c);
                default:
                    return ProcessDefaultState(editor, c);
            }
        }

        private State ProcessDefaultState(CodeEditor editor, char c)
        {
            switch (c)
            {
                case '/':
                    return State.PotentialComment;
                case '{':
                    if (Block.IsDefault() || Name.IsDefault())
                        break;
                    var type = Block.GetText(editor.Text);
                    var style = type == "shader" ? State.ShaderCode : State.BlockCode;
                    editor.SetStyling(1, (int)style);
                    return style;
                default:
                    if (char.IsLetterOrDigit(c))
                        return ProcessIndicatorState(editor, c);
                    break;
            }
            editor.SetStyling(1, (int)State.Default);
            return State.Default;
        }

        private State ProcessIndicatorState(CodeEditor editor, char c)
        {
            if (char.IsLetterOrDigit(c))
            {
                editor.SetStyling(1, (int)State.Indicator);
                return State.Indicator;
            }
            else if (char.IsWhiteSpace(c))
            {
                var range = GetStyleRangeAt(editor, editor.GetEndStyled());

                if (Block.IsDefault())
                {
                    Block.Index = range.Index;
                    Block.End = range.End;
                }
                else if (Name.IsDefault())
                {
                    Name.Index = range.Index;
                    Name.End = range.End;
                }
                else if (Anno.IsDefault())
                {
                    Anno.Index = Name.Index;
                    Anno.End = Name.End;
                    Name.Index = range.Index;
                    Name.End = range.End;
                }
            }
            return ProcessDefaultState(editor, c);
        }

        private State ProcessPotentialCommentState(CodeEditor editor, char c)
        {
            switch (c)
            {
                case '/':
                    editor.SetStyling(2, (int)State.LineComment);
                    return State.LineComment;
                case '*':
                    editor.SetStyling(2, (int)State.BlockComment);
                    return State.BlockComment;
                default:
                    editor.SetStyling(1, (int)State.Default);
                    return ProcessDefaultState(editor, c);
            }
        }

        private State ProcessLineCommentState(CodeEditor editor, char c)
        {
            switch (c)
            {
                case '\n':
                    editor.SetStyling(1, (int)State.Default);
                    return State.Default;
                default:
                    editor.SetStyling(1, (int)State.LineComment);
                    return State.LineComment;
            }
        }

        private State ProcessBlockCommentState(CodeEditor editor, char c)
        {
            switch (c)
            {
                case '*':
                    return State.PotentialEndBlockComment;
                default:
                    editor.SetStyling(1, (int)State.BlockComment);
                    return State.BlockComment;
            }
        }

        private State ProcessPotentialEndBlockCommentState(CodeEditor editor, char c)
        {
            switch (c)
            {
                case '/':
                    editor.SetStyling(2, (int)State.BlockComment);
                    return State.Default;
                case '*':
                    editor.SetStyling(1, (int)State.BlockComment);
                    return State.PotentialEndBlockComment;
                default:
                    editor.SetStyling(2, (int)State.BlockComment);
                    return State.BlockComment;
            }
        }

        private State ProcessBlockCodeState(CodeEditor editor, char c)
        {
            if (char.IsDigit(c))
            {
                if (Command.IsDefault())
                {

                }
                else
                {

                }
            }
            editor.SetStyling(1, (int)State.BlockCode);
            return State.BlockCode;
        }

        private State ProcessShaderCodeState(CodeEditor editor, char c)
        {
            editor.SetStyling(1, (int)State.BlockCode);
            switch (c)
            {
                case '{':
                    braceCount++;
                    editor.SetStyling(1, (int)State.BlockCode);
                    break;
                case '}':
                    if (--braceCount > 0)
                        break;
                    editor.SetStyling(1, (int)State.BlockCode);
                    return State.Default;
            }
            return State.BlockCode;
        }

        private TextRange GetStyleRangeAt(CodeEditor editor, int pos)
        {
            int start = pos, end = pos;
            int style = editor.GetStyleAt(pos);

            while (start >= 0 && editor.GetStyleAt(start) == style)
                start--;
            
            return new TextRange
            {
                Index = start + 1,
                End = end + 1
            };
        }
    }

    public class BlockLexer2 : ILex
    {
        public void Style(CodeEditor editor, int pos, int endPos)
        {

        }
    }

    public class GlslLexer
    {
        public void Style(CodeEditor editor, int pos, int endPos)
        {

        }
    }
    
    struct TextRange
    {
        public int Index;
        public int End;
        public int Length => End - Index;
        public string GetText(string text) => text.Substring(Index, Length);
    }*/
}
