using ScintillaNET;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
//using static App.Lexer.FxLexer.Styles;

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
        void Style(CodeEditor editor, int pos, int endPos);
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
        int[] GetStyles();
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
        /// Get the style of the word at the specified position. If the word
        /// could not be found the default style 0 will be returned.
        /// </summary>
        /// <param name="editor"></param>
        /// <param name="position"></param>
        /// <returns>Returns the style for the word at the specified position or 0.</returns>
        int GetKeywordStyle(string text, int position, string word = null);
        /// <summary>
        /// Get the hint of the word at the specified position. If the word
        /// could not be found <code>null</code> be returned.
        /// </summary>
        /// <param name="editor"></param>
        /// <param name="position"></param>
        /// <returns>Returns the style for the word at the specified
        /// position or <code>null</code>.</returns>
        string GetKeywordHint(string text, int position, string word = null);
        /// <summary>
        /// Find all potential keywords starting with the word at the specified position.
        /// </summary>
        /// <param name="editor"></param>
        /// <param name="position"></param>
        /// <returns>Returns a list of keywords.</returns>
        IEnumerable<string> GetPotentialKeywords(string text, int position, string word = null);
    }
    
    public class FxLexer : ILexer
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
            => GetPotentialKeywordDefs(text, position, word).FirstOrDefault().style;

        public string GetKeywordHint(string text, int position, string word) 
            => GetPotentialKeywordDefs(text, position, word).FirstOrDefault().hint;

        public IEnumerable<string> GetPotentialKeywords(string text, int position, string word)
        {
            foreach (var def in GetPotentialKeywordDefs(text, position, word))
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
        private IEnumerable<Node.KeywordDef> GetPotentialKeywordDefs(string text, int position, string word)
        {
            foreach (var def in root.GetKeywordDefs(text, position, 0, text.Length, word))
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

        private class Node
        {
            private string pre;
            private string post;
            private string anno;
            private char openBrace = ' ';
            private char closeBrace = '\n';
            //private Regex regex;
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
                var headerTabs = headerLine.LastIndexOf('┬');

                // get header
                var header = headerLine.Substring(headerTabs + 1).Trim().Split(SPLIT);

                // compile regular expression
                //regex = new Regex(header[0], RegexOptions.Singleline);
                var def = header[0].Split('|').Select(x => x.Trim()).ToArray();
                pre = def[0].Length > 0 ? def[0] : null;
                anno = def[1].Length > 0 ? def[1] : null;
                openBrace = def[2].Length > 0 ? def[2][0] : openBrace;
                closeBrace = def[3].Length > 0 ? def[3][0] : closeBrace;
                post = def[4].Length > 0 ? def[4] : null;

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
            //private Match IsActiveNode(string text, int position)
            //{
            //    foreach (Match match in regex.Matches(text))
            //    {
            //        if (match.Index <= position && position < match.Index + match.Length)
            //            return match;
            //    }
            //    return null;
            //}

            private MATCH IsActiveNode2(string text, int position, int startIndex, int count)
            {
                if (pre == null)
                {
                    return new MATCH
                    {
                        Index = 0,
                        Length = text.Length
                    };
                }

                for (int i = text.IndexOfWholeWords(pre, startIndex, count), endIndex = startIndex + count;
                    i >= 0 && i < endIndex;
                    i = text.IndexOfWholeWords(pre, i + 1, endIndex - i - 1))
                {
                    if (position < i)
                        break;

                    int annoIndex = anno == null ? i : text.IndexOfWholeWords(anno, i, endIndex - i);
                    if (annoIndex < 0 || position < annoIndex)
                        break;

                    int openIndex = text.IndexOf(openBrace, annoIndex, endIndex - annoIndex);
                    if (openIndex < 0 || position < openIndex)
                        break;

                    int closeIndex = text.IndexOfBraceMatch(openBrace, closeBrace, openIndex, endIndex - openIndex);
                    if (closeIndex < 0 || closeIndex < position)
                        continue;

                    int postIndex = post == null ? closeIndex : text.IndexOfWholeWords(post, closeIndex, endIndex - closeIndex);
                    if (postIndex < 0 || postIndex < position)
                        continue;
                    
                    return new MATCH
                    {
                        Index = openIndex,
                        Length = closeIndex - openIndex
                    };
                }
                return default(MATCH);
            }

            private struct MATCH
            {
                public int Index;
                public int Length;
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="text"></param>
            /// <param name="position"></param>
            /// <param name="word"></param>
            /// <returns></returns>
            public IEnumerable<KeywordDef> GetKeywordDefs(string text, int position, int startIndex, int count, string word)
            {
                // if no word has been specified
                if (word == null)
                    word = text.WordFromPosition(position);

                // is the keyword within a block handled by this node?
                var match = IsActiveNode2(text, position, startIndex, count);
                if (match.IsDefault())
                    // word not handled by this node
                    yield break;

                // is the keyword within a block handled by a child node?
                foreach (var child in children)
                {
                    var keywords = child.GetKeywordDefs(text, position, match.Index, match.Length, word);
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
        #endregion
    }
}
