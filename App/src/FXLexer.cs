using ScintillaNET;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace App
{
    class FXLexer
    {
        public const int Default = 0;
        public const int Keyword = Default + 1;
        public const int Annotation = Keyword + 1;
        public const int Command = Annotation + 1;
        public const int Argument = Command + 1;
        public const int GlslKeyword = Argument + 1;
        public const int GlslQualifier = GlslKeyword + 1;
        public const int GlslFunction = GlslQualifier + 1;
        public const int Identifier = GlslFunction + 1;
        public const int Number = Identifier + 1;
        public const int String = Number + 1;
        public const int Char = String + 1;
        public const int Comment = Char + 1;
        public const int Operator = Comment + 1;
        public const int Preprocessor = Operator + 1;

        private HashSet<string>[] keywords = new HashSet<string>[GlslFunction];

        public FXLexer(
            string[] keywords,
            string[] annotations,
            string[] commands,
            string[] arguments,
            string[] glslkeywords,
            string[] glslqualiriers,
            string[] glslfunctions)
        {
            this.keywords = new[] {
                new HashSet<string>(keywords),
                new HashSet<string>(annotations),
                new HashSet<string>(commands),
                new HashSet<string>(arguments),
                new HashSet<string>(glslkeywords),
                new HashSet<string>(glslqualiriers),
                new HashSet<string>(glslfunctions)
            };
        }

        private const int STATE_UNKNOWN = 0;
        private const int STATE_IDENTIFIER = 1;
        private const int STATE_NUMBER = 2;
        private const int STATE_STRING = 3;
        private const int STATE_LINE_COMMENT = 4;
        private const int STATE_BLOCK_COMMENT = 5;
        private const int STATE_PREPROCESSOR = 6;
        private const int STATE_OPERATOR = 7;

        public void Style(Scintilla editor, int startPos, int endPos, int[] keywords)
        {
            // Back up to the line start
            var length = 0;
            var state = STATE_UNKNOWN;

            // Start styling
            editor.StartStyling(startPos);
            while (startPos < endPos)
            {
                var c = (char)editor.GetCharAt(startPos);

            REPROCESS:
                switch (state)
                {
                    case STATE_UNKNOWN:
                        if (c == '/')
                        {
                            var c2 = (char)editor.GetCharAt(startPos + 1);
                            if (c2 == '/')
                            {
                                editor.SetStyling(2, Comment);
                                state = STATE_LINE_COMMENT;
                                startPos++;
                            }
                            else if (c2 == '*')
                            {
                                editor.SetStyling(2, Comment);
                                state = STATE_BLOCK_COMMENT;
                                startPos++;
                            }
                        }
                        else if (c == '"' || c == '\'')
                        {
                            editor.SetStyling(1, String);
                            state = STATE_STRING;
                        }
                        else if (c == '#')
                        {
                            editor.SetStyling(1, Preprocessor);
                            state = STATE_PREPROCESSOR;
                        }
                        else if (char.IsSymbol(c))
                        {
                            editor.SetStyling(1, Operator);
                            state = STATE_OPERATOR;
                        }
                        else if (char.IsDigit(c))
                        {
                            state = STATE_NUMBER;
                            goto REPROCESS;
                        }
                        else if (char.IsLetter(c))
                        {
                            state = STATE_IDENTIFIER;
                            goto REPROCESS;
                        }
                        else
                            editor.SetStyling(1, Default);
                        break;

                    case STATE_LINE_COMMENT:
                        if (c == '\r' || c == '\n')
                        {
                            editor.SetStyling(length + 1, Comment);
                            length = 0;
                            state = STATE_UNKNOWN;
                        }
                        else
                        {
                            length++;
                        }
                        break;

                    case STATE_BLOCK_COMMENT:
                        if (c == '*' && (char)editor.GetCharAt(startPos + 1) == '/')
                        {
                            editor.SetStyling(length + 2, Comment);
                            length = 0;
                            state = STATE_UNKNOWN;
                        }
                        else
                        {
                            length++;
                        }
                        break;

                    case STATE_STRING:
                        if (c == '"' || c == '\'')
                        {
                            editor.SetStyling(length + 1, String);
                            length = 0;
                            state = STATE_UNKNOWN;
                        }
                        else
                        {
                            length++;
                        }
                        break;

                    case STATE_PREPROCESSOR:
                        if (c == '\r' || c == '\n')
                        {
                            editor.SetStyling(length + 1, Preprocessor);
                            length = 0;
                            state = STATE_UNKNOWN;
                        }
                        else
                        {
                            length++;
                        }
                        break;

                    case STATE_OPERATOR:
                        if (char.IsSymbol(c))
                        {
                            length++;
                        }
                        else
                        {
                            editor.SetStyling(length, Operator);
                            length = 0;
                            state = STATE_UNKNOWN;
                            goto REPROCESS;
                        }
                        break;

                    case STATE_NUMBER:
                        if (char.IsDigit(c) || (c >= 'a' && c <= 'f') || (c >= 'A' && c <= 'F') || c == 'x')
                        {
                            length++;
                        }
                        else
                        {
                            editor.SetStyling(length, Number);
                            length = 0;
                            state = STATE_UNKNOWN;
                            goto REPROCESS;
                        }
                        break;

                    case STATE_IDENTIFIER:
                        if (char.IsLetterOrDigit(c) || c == '_' || c == '/')
                        {
                            length++;
                        }
                        else
                        {
                            var style = Identifier;
                            var identifier = editor.GetTextRange(startPos - length, length);

                            foreach (var i in keywords)
                            {
                                if (this.keywords[i - Keyword].Contains(identifier))
                                {
                                    style = i;
                                    break;
                                }
                            }

                            editor.SetStyling(length, style);
                            length = 0;
                            state = STATE_UNKNOWN;
                            goto REPROCESS;
                        }
                        break;
                }

                startPos++;
            }
        }
    }
}
