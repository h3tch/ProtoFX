using ScintillaNET;
using System;
using System.Collections.Generic;
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
            Keyword,
            Annotation,
            Command,
            Argument,
            GlslKeyword,
            GlslQualifier,
            GlslFunction,
            Identifier,
            Number,
            String,
            Char,
            Comment,
            Operator,
            Punctuation,
            Preprocessor,
        }

        private enum State : int
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
        }

        private UnicodeCategory[] Punctuation = new[] {
            UnicodeCategory.OpenPunctuation,
            UnicodeCategory.ClosePunctuation,
            UnicodeCategory.ConnectorPunctuation,
        };

        private HashSet<string>[] keywords;
        #endregion

        /// <summary>
        /// Create a lexer from a keyword definition list.
        /// </summary>
        /// <param name="keywordDef"></param>
        public FXLexer(string[] keywordDef)
        {
            // remove hints from keyword definitions
            var list = keywordDef
                .Select(x => x.Substring(0, (int)Math.Min((uint)x.Length, (uint)x.IndexOf('|'))))
                .ToArray();

            // get block keywords
            var block = list
                .Where(x => x.IndexOf('.') < 0 && x.IndexOf(':') < 0 && x.IndexOf(',') < 0)
                .ToArray();
            // get block annotation keywords
            var anno = list
                .Where(x => x.IndexOf('.') < 0 && x.IndexOf(':') < 0 && x.Count(c => c == ',') == 1)
                .Select(x => x.Substring(x.IndexOf(',') + 1))
                .ToArray();
            // get command keywords (shaders do not have commands and are handled differently)
            var sel = list
                .Where(x => !x.StartsWith("shader") && x.IndexOf('.') >= 0)
                .Select(x => x.Substring(x.IndexOf('.') + 1))
                .ToArray();
            // get command keywords
            var cmd = sel
                .Where(x => x.IndexOf('.') < 0)
                .ToArray();
            // get command argument keywords
            var arg = sel
                .Where(x => x.IndexOf('.') >= 0)
                .Select(x => x.Substring(x.IndexOf('.') + 1))
                .ToArray();

            // get shader function keywords
            var shader = "shader";
            var glslfunctions = list
                .Where(x => x.StartsWith(shader) && x.IndexOf(':') < 0 && x.Count(c => c == '.') == 1)
                .Select(x => x.Substring(x.IndexOf('.') + 1))
                .ToArray();
            // get shader type keywords
            var glsltypes = list
                .Where(x => x.StartsWith(shader) && x.IndexOf('.') < 0 && x.Count(c => c == ':') == 1)
                .Select(x => x.Substring(x.IndexOf(':') + 1))
                .ToArray();
            // get shader qualifier keywords
            var glslqualiriers = list
                .Where(x => x.StartsWith(shader) && x.IndexOf(':') >= 0 && x.IndexOf('.') >= 0)
                .Select(x => x.Substring(x.LastIndexOf('.') + 1))
                .ToArray();

            // setup internal keyword hashsets
            this.keywords = new[] {
                new HashSet<string>(block),
                new HashSet<string>(anno),
                new HashSet<string>(cmd),
                new HashSet<string>(arg),
                new HashSet<string>(glsltypes),
                new HashSet<string>(glslqualiriers),
                new HashSet<string>(glslfunctions)
            };
        }

        /// <summary>
        /// Style editor text between start and end position.
        /// </summary>
        /// <param name="editor"></param>
        /// <param name="startPos"></param>
        /// <param name="endPos"></param>
        /// <param name="keyStyles"></param>
        /// <returns></returns>
        public int Style(Scintilla editor, int startPos, int endPos, int[] keyStyles)
        {
            // Back up to the line start
            var length = 0;
            var state = State.Unknown;

            // Start styling
            editor.StartStyling(startPos);
            while (startPos < endPos)
            {
                var c = (char)editor.GetCharAt(startPos);

                REPROCESS:
                switch (state)
                {
                    // UNKNOWN STATE
                    case State.Unknown:
                        // could be comment
                        if (c == '/')
                        {
                            var c2 = (char)editor.GetCharAt(startPos + 1);
                            // is line comment
                            if (c2 == '/')
                            {
                                editor.SetStyling(2, (int)Styles.Comment);
                                state = State.LineComment;
                                startPos++;
                            }
                            // is block comment
                            else if (c2 == '*')
                            {
                                editor.SetStyling(2, (int)Styles.Comment);
                                state = State.BlockComment;
                                startPos++;
                            }
                            else
                            {
                                editor.SetStyling(1, (int)Styles.Operator);
                                state = State.Operator;
                            }
                        }
                        // is string
                        else if (c == '"' || c == '\'')
                        {
                            editor.SetStyling(1, (int)Styles.String);
                            state = State.String;
                        }
                        // is preprocessor
                        else if (c == '#')
                        {
                            editor.SetStyling(1, (int)Styles.Preprocessor);
                            state = State.Preprocessor;
                        }
                        // is operator
                        else if (IsMathSymbol(c))
                        {
                            editor.SetStyling(1, (int)Styles.Operator);
                            state = State.Operator;
                        }
                        // is punctuation
                        else if (Punctuation.Any(x => x == char.GetUnicodeCategory(c)))
                        {
                            editor.SetStyling(1, (int)Styles.Punctuation);
                            state = State.Operator;
                        }
                        // is number
                        else if (char.IsDigit(c))
                        {
                            state = State.Number;
                            goto REPROCESS;
                        }
                        // is letter
                        else if (char.IsLetter(c))
                        {
                            state = State.Identifier;
                            goto REPROCESS;
                        }
                        // is default styling
                        else
                            editor.SetStyling(1, (int)Styles.Default);
                        break;

                    // LINE COMMENT STATE
                    case State.LineComment:
                        // end of line comment
                        if (c == '\r' || c == '\n')
                        {
                            editor.SetStyling(length + 1, (int)Styles.Comment);
                            length = 0;
                            state = State.Unknown;
                        }
                        // still in line comment
                        else
                            length++;
                        break;

                    // BLOCK COMMENT STATE
                    case State.BlockComment:
                        // end of block comment
                        if (c == '*' && (char)editor.GetCharAt(startPos + 1) == '/')
                        {
                            editor.SetStyling(length + 2, (int)Styles.Comment);
                            length = 0;
                            state = State.Unknown;
                        }
                        // still in block comment
                        else
                            length++;
                        break;

                    // STRING STATE
                    case State.String:
                        // end of string
                        if (c == '"' || c == '\'')
                        {
                            editor.SetStyling(length + 1, (int)Styles.String);
                            length = 0;
                            state = State.Unknown;
                        }
                        // still in string
                        else
                            length++;
                        break;

                    // PREPROCESSOR STATE
                    case State.Preprocessor:
                        // end of preprocessor
                        if (c == ' ' || c == '\r' || c == '\n')
                        {
                            editor.SetStyling(length + 1, (int)Styles.Preprocessor);
                            length = 0;
                            state = State.Unknown;
                        }
                        // still preprocessor
                        else
                            length++;
                        break;

                    // OPERATOR STATE
                    case State.Operator:
                        // is multi char operator like >=
                        if (IsMathSymbol(c))
                        {
                            length++;
                        }
                        // end of operator
                        else
                        {
                            editor.SetStyling(length, (int)Styles.Operator);
                            length = 0;
                            state = State.Unknown;
                            goto REPROCESS;
                        }
                        break;

                    // PUNCTUATION STATE
                    case State.Punctuation:
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
                            state = State.Unknown;
                            goto REPROCESS;
                        }
                        break;

                    // NUMBER STATE
                    case State.Number:
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
                            state = State.Unknown;
                            goto REPROCESS;
                        }
                        break;

                    // IDENTIFIER STATE
                    case State.Identifier:
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
                            foreach (var i in keyStyles)
                            {
                                if (keywords[i - (int)Styles.Keyword].Contains(identifier))
                                {
                                    style = i;
                                    break;
                                }
                            }

                            // set styling
                            editor.SetStyling(length, style);
                            length = 0;
                            state = State.Unknown;
                            goto REPROCESS;
                        }
                        break;
                }

                // next character position
                startPos++;
            }

            return startPos;
        }

        private bool IsMathSymbol(char c) => char.IsSymbol(c) || c == '*' || c == '-';
    }
}
