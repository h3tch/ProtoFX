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
        public static int Style(Scintilla editor, int pos, int endPos)
        {
            // additional special states
            var possibleStartComment = false;
            var possibleEndComment = false;

            // GET CURRENT STATE //

            // get the start position of the line
            var linepos = editor.Lines[editor.LineFromPosition(pos)].Position;
            // get the state at the line position or the last position in the previous line
            var state = (Styles)editor.GetStyleAt(linepos != pos ? linepos : Math.Max(0, pos - 1));
            pos = linepos;
            // check, if the previous state needs to be continued
            if (state != BlockComment)
                state = Default;

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
        /// <param name="startPos"></param>
        /// <param name="endPos"></param>
        public static void Folding(Scintilla editor, int startPos, int endPos)
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
}
