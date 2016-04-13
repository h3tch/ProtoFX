using ScintillaNET;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

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
        public char FolingChar = (char)177;
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
        /// <param name="startPos"></param>
        /// <param name="endPos"></param>
        /// <param name="keyStyles"></param>
        /// <returns></returns>
        public int Style(Scintilla editor, int startPos, int endPos)
        {
            // Back up to the line start
            var length = 0;
            var state = (Styles)editor.GetStyleAt(startPos);
            char c2;

            // Start styling
            editor.StartStyling(startPos);
            while (startPos < endPos)
            {
                var c = (char)editor.GetCharAt(startPos);
                
                REPROCESS:
                switch (state)
                {
                    // UNKNOWN STATE
                    case Styles.Default:
                        switch (c)
                        {
                            // could be comment
                            case '/':
                                c2 = NextChar(editor, startPos, endPos);
                                // is line comment
                                if (c2 == '/')
                                    state = Styles.LineComment;
                                // is block comment
                                else if (c2 == '*')
                                    state = Styles.BlockComment;
                                else
                                    state = Styles.Operator;
                                editor.SetStyling(1, (int)state);
                                break;
                            // is string
                            case '"':
                            case '\'':
                                state = Styles.String;
                                editor.SetStyling(1, (int)state);
                                break;
                            // is preprocessor
                            case '#':
                                state = Styles.Preprocessor;
                                editor.SetStyling(1, (int)state);
                                break;
                            default:
                                // is operator
                                if (IsMathSymbol(c))
                                {
                                    editor.SetStyling(1, (int)Styles.Operator);
                                    state = Styles.Operator;
                                }
                                // is punctuation
                                else if (Punctuation.Any(x => x == char.GetUnicodeCategory(c)))
                                {
                                    editor.SetStyling(1, (int)Styles.Punctuation);
                                    state = Styles.Operator;
                                }
                                // is number
                                else if (char.IsDigit(c))
                                {
                                    state = Styles.Number;
                                    goto REPROCESS;
                                }
                                // is letter
                                else if (char.IsLetter(c))
                                {
                                    state = Styles.Identifier;
                                    goto REPROCESS;
                                }
                                // is default styling
                                else
                                    editor.SetStyling(1, (int)Styles.Default);
                                break;
                        }
                        break;

                    // LINE COMMENT STATE
                    case Styles.LineComment:
                        // end of line comment
                        if (c == '\n')
                        {
                            editor.SetStyling(length + 1, (int)state);
                            length = 0;
                            state = Styles.Default;
                        }
                        // still in line comment
                        else
                            length++;
                        break;

                    // BLOCK COMMENT STATE
                    case Styles.BlockComment:
                        // end of block comment
                        if (c == '/' && PrevChar(editor, startPos) == '*')
                        {
                            editor.SetStyling(length + 1, (int)state);
                            length = 0;
                            state = Styles.Default;
                        }
                        // still in block comment
                        else
                            length++;
                        break;

                    // STRING STATE
                    case Styles.String:
                        // end of string
                        if (c == '"' || c == '\'')
                        {
                            editor.SetStyling(length + 1, (int)Styles.String);
                            length = 0;
                            state = Styles.Default;
                        }
                        // still in string
                        else
                            length++;
                        break;

                    // PREPROCESSOR STATE
                    case Styles.Preprocessor:
                        // end of preprocessor
                        if (c == ' ' || c == '\r' || c == '\n')
                        {
                            editor.SetStyling(length + 1, (int)Styles.Preprocessor);
                            length = 0;
                            state = Styles.Default;
                        }
                        // still preprocessor
                        else
                            length++;
                        break;

                    // OPERATOR STATE
                    case Styles.Operator:
                        // is multi char operator like >=
                        if (IsMathSymbol(c) && c != FolingChar)
                        {
                            length++;
                        }
                        // end of operator
                        else
                        {
                            editor.SetStyling(length, (int)Styles.Operator);
                            length = 0;
                            state = Styles.Default;
                            goto REPROCESS;
                        }
                        break;

                    // PUNCTUATION STATE
                    case Styles.Punctuation:
                        // is multi char operator like >=
                        if (Punctuation.Any(x => x == char.GetUnicodeCategory(c)))
                        {
                            length++;
                        }
                        // end of operator
                        else
                        {
                            editor.SetStyling(length, (int)Styles.Punctuation);
                            length = 0;
                            state = Styles.Default;
                            goto REPROCESS;
                        }
                        break;

                    // NUMBER STATE
                    case Styles.Number:
                        // still a number
                        if (char.IsDigit(c) || (c >= 'a' && c <= 'f') || (c >= 'A' && c <= 'F') || c == 'x' || c == '.')
                        {
                            length++;
                        }
                        // end of number
                        else
                        {
                            editor.SetStyling(length, (int)Styles.Number);
                            length = 0;
                            state = Styles.Default;
                            goto REPROCESS;
                        }
                        break;

                    // IDENTIFIER STATE
                    case Styles.Identifier:
                        // still an identifier and possible keyword
                        if (char.IsLetterOrDigit(c) || c == '_' || c == '/')
                        {
                            length++;
                        }
                        // end of identifier/keyword
                        else
                        {
                            // get possible keyword string from identifier range
                            var style = (int)Styles.Identifier;
                            var identifier = editor.GetTextRange(startPos - length, length);

                            // if part of a keyword list, use the respective style
                            for (int i = 0; i < keywords.Length; i++)
                            {
                                if (keywords[i].Contains(identifier))
                                {
                                    style = KeywordStylesStart + i;
                                    break;
                                }
                            }

                            // set styling
                            editor.SetStyling(length, style);
                            length = 0;
                            state = Styles.Default;
                            goto REPROCESS;
                        }
                        break;
                }

                // next character position
                startPos++;
            }

            return startPos;
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
            => char.IsSymbol(c) || c == '*' || c == '-' || c == '&' || c == '|';
        
        public class KeyDef
        {
            public int Id;
            public string Prefix;
            public Color ForeColor;
            public Color BackColor;
        }
    }
}
