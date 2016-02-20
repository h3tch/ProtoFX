using ScintillaNET;
using System;
using System.Collections.Generic;
using System.Linq;

namespace App
{
    class FXLexer
    {
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
            Preprocessor,
        }

        private HashSet<string>[] keywords;

        public FXLexer(string[] keywords)
        {
            var list = keywords
                .Select(x => x.Substring(0, (int)Math.Min((uint)x.Length, (uint)x.IndexOf('|'))))
                .ToArray();

            var block = list
                .Where(x => x.IndexOf('.') < 0 && x.IndexOf(',') < 0)
                .ToArray();
            var anno = list
                .Where(x => x.IndexOf(',') >= 0)
                .Select(x => x.Substring(x.IndexOf(',') + 1))
                .ToArray();
            var sel = list
                .Where(x => !x.StartsWith("shader") && x.IndexOf('.') >= 0)
                .Select(x => x.Substring(x.IndexOf('.') + 1))
                .ToArray();
            var cmd = sel
                .Where(x => x.IndexOf('.') < 0)
                .ToArray();
            var arg = sel
                .Where(x => x.IndexOf('.') >= 0)
                .Select(x => x.Substring(x.IndexOf('.') + 1))
                .ToArray();

            var shader = "shader.";
            var glslfunctions = list
                .Where(x => x.StartsWith(shader) && x.IndexOf('.', shader.Length) < 0)
                .Select(x => x.Substring(shader.Length))
                .ToArray();
            shader = "shader:";
            var glsltypes = list
                .Where(x => x.StartsWith(shader))
                .Select(x => x.Substring(shader.Length))
                .ToArray();
            var glslqualiriers = list
                .Where(x => x.StartsWith(shader) && x.IndexOf('.', shader.Length) >= 0)
                .Select(x => x.Substring(x.IndexOf('.', shader.Length) + 1))
                .ToArray();

            this.keywords = new[] {
                Keywords2Set(block),
                Keywords2Set(anno),
                Keywords2Set(cmd),
                Keywords2Set(arg),
                Keywords2Set(glsltypes),
                Keywords2Set(glslqualiriers),
                Keywords2Set(glslfunctions)
            };
        }

        private HashSet<string> Keywords2Set(string[] hints)
            => new HashSet<string>(hints
                .Select(x => x.Substring(0, (int)Math.Min((uint)x.Length, (uint)x.IndexOf(' '))))
                .ToArray());

        private const int STATE_UNKNOWN = 0;
        private const int STATE_IDENTIFIER = 1;
        private const int STATE_NUMBER = 2;
        private const int STATE_STRING = 3;
        private const int STATE_LINE_COMMENT = 4;
        private const int STATE_BLOCK_COMMENT = 5;
        private const int STATE_PREPROCESSOR = 6;
        private const int STATE_OPERATOR = 7;

        public int Style(Scintilla editor, int startPos, int endPos, int[] keywords)
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
                                editor.SetStyling(2, (int)Styles.Comment);
                                state = STATE_LINE_COMMENT;
                                startPos++;
                            }
                            else if (c2 == '*')
                            {
                                editor.SetStyling(2, (int)Styles.Comment);
                                state = STATE_BLOCK_COMMENT;
                                startPos++;
                            }
                        }
                        else if (c == '"' || c == '\'')
                        {
                            editor.SetStyling(1, (int)Styles.String);
                            state = STATE_STRING;
                        }
                        else if (c == '#')
                        {
                            editor.SetStyling(1, (int)Styles.Preprocessor);
                            state = STATE_PREPROCESSOR;
                        }
                        else if (char.IsSymbol(c))
                        {
                            editor.SetStyling(1, (int)Styles.Operator);
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
                            editor.SetStyling(1, (int)Styles.Default);
                        break;

                    case STATE_LINE_COMMENT:
                        if (c == '\r' || c == '\n')
                        {
                            editor.SetStyling(length + 1, (int)Styles.Comment);
                            length = 0;
                            state = STATE_UNKNOWN;
                        }
                        else
                            length++;
                        break;

                    case STATE_BLOCK_COMMENT:
                        if (c == '*' && (char)editor.GetCharAt(startPos + 1) == '/')
                        {
                            editor.SetStyling(length + 2, (int)Styles.Comment);
                            length = 0;
                            state = STATE_UNKNOWN;
                        }
                        else
                            length++;
                        break;

                    case STATE_STRING:
                        if (c == '"' || c == '\'')
                        {
                            editor.SetStyling(length + 1, (int)Styles.String);
                            length = 0;
                            state = STATE_UNKNOWN;
                        }
                        else
                            length++;
                        break;

                    case STATE_PREPROCESSOR:
                        if (c == '\r' || c == '\n')
                        {
                            editor.SetStyling(length + 1, (int)Styles.Preprocessor);
                            length = 0;
                            state = STATE_UNKNOWN;
                        }
                        else
                            length++;
                        break;

                    case STATE_OPERATOR:
                        if (char.IsSymbol(c))
                        {
                            length++;
                        }
                        else
                        {
                            editor.SetStyling(length, (int)Styles.Operator);
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
                            editor.SetStyling(length, (int)Styles.Number);
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
                            var style = (int)Styles.Identifier;
                            var identifier = editor.GetTextRange(startPos - length, length);

                            foreach (var i in keywords)
                            {
                                if (this.keywords[i - (int)Styles.Keyword].Contains(identifier))
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

            return endPos;
        }
    }
}
