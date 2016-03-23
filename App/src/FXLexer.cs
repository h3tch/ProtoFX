using ScintillaNET;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;

namespace App
{
    class FXLexer
    {
        #region FIELDS
        public enum Styles : int
        {
            Default,
            Identifier,
            Number,
            String,
            Char,
            Comment,
            Operator,
            Punctuation,
            Preprocessor,
            Folding,
        }

        private enum StyleState : int
        {
            Unknown,
            Identifier,
            Number,
            String,
            LineComment,
            BlockComment,
            Preprocessor,
            Operator,
            Punctuation,
            Folding,
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
        public int KeywordStylesStart => (int)Styles.Folding + 1;
        public int KeywordStylesEnd => KeywordStylesStart + keywords.Length;
        public char FolingChar = (char)177;
        #endregion

        /// <summary>
        /// Create a lexer from a keyword definition list.
        /// </summary>
        /// <param name="keywordDef"></param>
        public FXLexer(string keywordDef, Dictionary<string, KeyDef> defs)
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
                var indicator = $"{def.Value.Id}";
                var words = from x in keywordDef.Matches(@"\[" + indicator + @"\]\w+")
                            select x.Value.Substring(indicator.Length + 2);
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
            var state = StyleState.Unknown;

            // Start styling
            editor.StartStyling(startPos);
            while (startPos < endPos)
            {
                var c = (char)editor.GetCharAt(startPos);
                
                REPROCESS:
                switch (state)
                {
                    // UNKNOWN STATE
                    case StyleState.Unknown:
                        switch (c)
                        {
                            // could be comment
                            case '/':
                                var c2 = (char)editor.GetCharAt(startPos + 1);
                                // is line comment
                                if (c2 == '/')
                                {
                                    editor.SetStyling(2, (int)Styles.Comment);
                                    state = StyleState.LineComment;
                                    startPos++;
                                }
                                // is block comment
                                else if (c2 == '*')
                                {
                                    editor.SetStyling(2, (int)Styles.Comment);
                                    state = StyleState.BlockComment;
                                    startPos++;
                                }
                                else
                                {
                                    editor.SetStyling(1, (int)Styles.Operator);
                                    state = StyleState.Operator;
                                }
                                break;
                            // is string
                            case '"':
                            case '\'':
                                editor.SetStyling(1, (int)Styles.String);
                                state = StyleState.String;
                                break;
                            // is preprocessor
                            case '#':
                                editor.SetStyling(1, (int)Styles.Preprocessor);
                                state = StyleState.Preprocessor;
                                break;
                            default:
                                // is operator
                                if (c == FolingChar)
                                {
                                    editor.SetStyling(1, (int)Styles.Folding);
                                    state = StyleState.Unknown;
                                }
                                else if (IsMathSymbol(c))
                                {
                                    editor.SetStyling(1, (int)Styles.Operator);
                                    state = StyleState.Operator;
                                }
                                // is punctuation
                                else if (Punctuation.Any(x => x == char.GetUnicodeCategory(c)))
                                {
                                    editor.SetStyling(1, (int)Styles.Punctuation);
                                    state = StyleState.Operator;
                                }
                                // is number
                                else if (char.IsDigit(c))
                                {
                                    state = StyleState.Number;
                                    goto REPROCESS;
                                }
                                // is letter
                                else if (char.IsLetter(c))
                                {
                                    state = StyleState.Identifier;
                                    goto REPROCESS;
                                }
                                // is default styling
                                else
                                    editor.SetStyling(1, (int)Styles.Default);
                                break;
                        }
                        break;

                    // LINE COMMENT STATE
                    case StyleState.LineComment:
                        // end of line comment
                        if (c == '\r' || c == '\n')
                        {
                            editor.SetStyling(length + 1, (int)Styles.Comment);
                            length = 0;
                            state = StyleState.Unknown;
                        }
                        // still in line comment
                        else
                            length++;
                        break;

                    // BLOCK COMMENT STATE
                    case StyleState.BlockComment:
                        // end of block comment
                        if (c == '*' && (char)editor.GetCharAt(startPos + 1) == '/')
                        {
                            editor.SetStyling(length + 2, (int)Styles.Comment);
                            length = 0;
                            state = StyleState.Unknown;
                        }
                        // still in block comment
                        else
                            length++;
                        break;

                    // STRING STATE
                    case StyleState.String:
                        // end of string
                        if (c == '"' || c == '\'')
                        {
                            editor.SetStyling(length + 1, (int)Styles.String);
                            length = 0;
                            state = StyleState.Unknown;
                        }
                        // still in string
                        else
                            length++;
                        break;

                    // PREPROCESSOR STATE
                    case StyleState.Preprocessor:
                        // end of preprocessor
                        if (c == ' ' || c == '\r' || c == '\n')
                        {
                            editor.SetStyling(length + 1, (int)Styles.Preprocessor);
                            length = 0;
                            state = StyleState.Unknown;
                        }
                        // still preprocessor
                        else
                            length++;
                        break;

                    // OPERATOR STATE
                    case StyleState.Operator:
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
                            state = StyleState.Unknown;
                            goto REPROCESS;
                        }
                        break;

                    // PUNCTUATION STATE
                    case StyleState.Punctuation:
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
                            state = StyleState.Unknown;
                            goto REPROCESS;
                        }
                        break;

                    // NUMBER STATE
                    case StyleState.Number:
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
                            state = StyleState.Unknown;
                            goto REPROCESS;
                        }
                        break;

                    // IDENTIFIER STATE
                    case StyleState.Identifier:
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
                            state = StyleState.Unknown;
                            goto REPROCESS;
                        }
                        break;
                }

                // next character position
                startPos++;
            }

            return startPos;
        }
        
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
