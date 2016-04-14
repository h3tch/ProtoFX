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

        private UnicodeCategory[] Punctuation = new[] {
            UnicodeCategory.OpenPunctuation,
            UnicodeCategory.ClosePunctuation,
            UnicodeCategory.ConnectorPunctuation,
        };

        private HashSet<string>[] keywords;
        public Dictionary<string, KeyDef> Defs { get; private set; }
        public int KeywordStylesStart => (int)Styles.Punctuation + 1;
        public int KeywordStylesEnd => KeywordStylesStart + keywords.Length;
        #endregion

        /// <summary>
        /// Create a lexer from a keyword definition list.
        /// </summary>
        /// <param name="keywordDef"></param>
        public FxLexer(string keywordDef, Dictionary<string, KeyDef> defs)
        {
            Styles style;

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

        /// <summary>
        /// Style editor text between start and end position.
        /// </summary>
        /// <param name="editor"></param>
        /// <param name="pos"></param>
        /// <param name="endPos"></param>
        /// <param name="keyStyles"></param>
        /// <returns></returns>
        public int Style(Scintilla editor, int pos, int endPos)
        {
            var newpos = editor.Lines[editor.LineFromPosition(pos)].Position;
            Styles state = (Styles)editor.GetStyleAt(Math.Max(0, newpos != pos ? newpos : pos - 1));
            if (state != BlockComment)
                state = Default;
            pos = newpos;
            var textLength = editor.TextLength;
            bool possibleStartComment = false;
            bool possibleEndComment = false;

            // Start styling
            editor.StartStyling(pos);

            for (int length = 0; (pos < endPos || state != Default) && pos < textLength; pos++)
            {
                var c = (char)editor.GetCharAt(pos);

                if (possibleStartComment)
                {
                    possibleStartComment = false;
                    Styles initialState = state;
                    if (c == '/')
                        state = LineComment;
                    else if (c == '*')
                        state = BlockComment;
                    if (state != initialState)
                    {
                        editor.StartStyling(pos - length);
                        StyleCode(editor, length, initialState);
                        editor.StartStyling(pos - 1);
                        StyleCode(editor, 1, state);
                    }
                }

                if (possibleEndComment)
                {
                    possibleEndComment = false;
                    if (c == '/')
                    {
                        StyleCode(editor, 1, state);
                        state = Default;
                        endPos = textLength;
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
                                StyleCode(editor, 1, state);
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
                                    StyleCode(editor, 1, Styles.Punctuation);
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
                                    StyleCode(editor, 1, Default);
                                    break;
                                }
                                goto REPROCESS;
                        }
                        break;

                    // LINE COMMENT STATE
                    case LineComment:
                        StyleCode(editor, 1, state);
                        // end of line comment
                        if (c == '\n')
                            state = Default;
                        break;

                    // BLOCK COMMENT STATE
                    case BlockComment:
                        possibleEndComment = c == '*';
                        StyleCode(editor, 1, state);
                        break;

                    // STRING STATE
                    case Styles.String:
                        StyleCode(editor, 1, state);
                        // end of string
                        if (c == '"' || c == '\'' || c == '\n')
                            state = Default;
                        break;

                    // PREPROCESSOR STATE
                    case Preprocessor:
                        StyleCode(editor, 1, state);
                        // end of preprocessor
                        if (c == ' ' || c == '\n')
                            state = Default;
                        break;

                    // OPERATOR STATE
                    case Operator:
                        possibleStartComment = c == '/';
                        if (IsMathSymbol(c))
                            StyleCode(editor, 1, state);
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
                        if (char.IsDigit(c) || (c >= 'a' && c <= 'f') || (c >= 'A' && c <= 'F') || c == 'x' || c == '.')
                        {
                            length++;
                        }
                        // end of number
                        else
                        {
                            StyleCode(editor, length, state);
                            length = 0;
                            state = Default;
                            goto REPROCESS;
                        }
                        break;

                    // IDENTIFIER STATE
                    case Identifier:
                        // still an identifier and possible keyword
                        if (char.IsLetterOrDigit(c) || c == '_')
                        {
                            length++;
                        }
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
                            StyleCode(editor, length, style);
                            length = 0;
                            state = Default;
                            goto REPROCESS;
                        }
                        break;
                }
            }

            return pos;
        }

        void StyleCode(Scintilla editor, int length, Styles style)
        {
            editor.SetStyling(length, (int)style);
        }

        private char NextChar(Scintilla editor, int idx, int endPos, char defaultChar = ' ')
            => idx + 1 < endPos ? (char)editor.GetCharAt(idx + 1) : defaultChar;

        private char PrevChar(Scintilla editor, int idx, char defaultChar = ' ')
            => idx - 1 >= 0 ? (char)editor.GetCharAt(idx - 1) : defaultChar;

        /// <summary>
        /// Add folding between start and end position.
        /// </summary>
        /// <param name="editor"></param>
        /// <param name="startPos"></param>
        /// <param name="endPos"></param>
        public void Folding(Scintilla editor, int startPos, int endPos)
        {
            var state = FoldState.Unknown;
            var line = editor.LineFromPosition(startPos);
            var lastLine = -1;
            var lastCharPos = line;
            var foldLevel = editor.Lines[line].FoldLevel;
            var textLength = editor.Text.Length;

            while (state == FoldState.Unknown ? startPos < endPos : startPos < textLength)
            {
                var c = (char)editor.GetCharAt(startPos);

                switch (c)
                {
                    case '{':
                        state = FoldState.StartFolding;
                        break;
                    case '}':
                        state = FoldState.EndFolding;
                        break;
                    case '\n':
                        line++;
                        break;
                    default:
                        if (char.IsLetterOrDigit(c))
                            lastCharPos = startPos;
                        break;
                }

                switch (state)
                {
                    case FoldState.StartFolding:
                        var lastCharLine = editor.LineFromPosition(lastCharPos);
                        editor.Lines[lastCharLine].FoldLevelFlags = FoldLevelFlags.Header;
                        editor.Lines[lastCharLine].FoldLevel = foldLevel++;
                        for (int i = lastCharLine + 1; i <= line; i++)
                        {
                            editor.Lines[i].FoldLevelFlags = FoldLevelFlags.White;
                            editor.Lines[i].FoldLevel = foldLevel;
                        }
                        lastLine = line;
                        state = FoldState.Foldable;
                        break;
                    case FoldState.EndFolding:
                        editor.Lines[line].FoldLevel = foldLevel;
                        foldLevel = Math.Max(--foldLevel, 1024);
                        lastLine = line;
                        state = FoldState.Foldable;
                        break;
                    case FoldState.Foldable:
                        if (foldLevel > 1024)
                        {
                            if (line != lastLine)
                            {
                                editor.Lines[line].FoldLevel = foldLevel;
                                lastLine = line;
                            }
                        }
                        else
                            state = FoldState.Unknown;
                        break;
                }

                startPos++;
            }
        }

        /// <summary>
        /// Check if character is a math symbol.
        /// </summary>
        /// <param name="c"></param>
        /// <returns>True if character is a math symbol.</returns>
        private bool IsMathSymbol(char c) 
            => char.IsSymbol(c) || c == '/' || c == '*' || c == '-' || c == '&' || c == '|';
        
        public class KeyDef
        {
            public int Id;
            public string Prefix;
            public Color ForeColor;
            public Color BackColor;
        }
    }
}
