using ScintillaNET;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Xml;
using SciStyle = ScintillaNET.Style;

namespace App.Lexer
{
    public struct Keyword
    {
        public string word;
        public string hint;
    }

    public struct Style
    {
        public int id;
        public Color fore, back;
    }

    public static class XmlNodeExtensions
    {
        public static string GetAttributeValue(this XmlNode node, string name)
            => node.Attributes.GetNamedItem(name)?.Value;
    }

    public interface ILexer
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
        IEnumerable<Style> GetStyles();
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
        /// <summary>
        /// Find all keyword information starting with the word at the specified position.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="word"></param>
        /// <returns></returns>
        IEnumerable<Keyword> SelectKeywordInfo(int id, string word);
    }

    public abstract class BaseLexer : ILexer
    {
        protected string lexerType;
        protected BaseLexer[] lexer;
        protected Style[] styles;
        protected Trie<Keyword>[] keywords;
        public int firstStyle { get; private set; }
        public int lastStyle { get; private set; }
        public static int firstBaseStyle { get; private set; }
        public static int lastBaseStyle { get; private set; }
        protected BaseLexer defaultLexer;

        public BaseLexer(int firstFreeStyle, XmlNode lexerNode, BaseLexer defaultLexer)
        {
            if (defaultLexer == null)
            {
                var node = lexerNode.SelectSingleNode("DefaultLexer");
                if (node != null)
                    defaultLexer = new DefaultLexer(firstFreeStyle, node, null);
            }
            this.defaultLexer = defaultLexer;

            // get style and state ranges
            var stateValues = Enum.GetValues(StateType);
            int styleCount = stateValues.Length;
            int firstState = styleCount > 0 ? ((int[])stateValues).Min() : 0;

            // adjust style ranges if they fall into the Scintilla styles
            firstStyle = firstFreeStyle;
            if (SciStyle.Default <= firstStyle + styleCount && firstStyle <= SciStyle.CallTip)
                firstStyle = SciStyle.CallTip + 1;
            lastStyle = firstStyle + styleCount - 1;

            if (StateType.IsEquivalentTo(typeof(BaseState)))
            {
                firstBaseStyle = firstStyle;
                lastBaseStyle = lastStyle;
            }

            // allocate arrays for styles
            styles = Enumerable.Range(firstStyle, styleCount)
                .Select(x => new Style {
                    id = x,
                    fore = Color.Black,
                    back = Color.White
                }).ToArray();
            keywords = new Trie<Keyword>[styleCount];

            // get lexer type
            lexerType = lexerNode.GetAttributeValue("type");

            // get style colors
            var styleList = lexerNode.SelectNodes("Style");
            foreach (XmlNode style in styleList)
            {
                var id = (int)Enum.Parse(StateType, style.GetAttributeValue("name"), true);
                var idx = id - firstState;
                styles[idx].fore = ColorTranslator.FromHtml(style.GetAttributeValue("fore"));
                styles[idx].back = ColorTranslator.FromHtml(style.GetAttributeValue("back"));
            }

            // get keyword definitions
            var keywordList = lexerNode.SelectNodes("Keyword");
            foreach (XmlNode keyword in keywordList)
            {
                var id = (int)Enum.Parse(StateType, keyword.GetAttributeValue("style_name"), true);
                var idx = id - firstState;
                var name = keyword.GetAttributeValue("name");
                var hint = keyword.GetAttributeValue("hint");
                if (keywords[idx] == null)
                    keywords[idx] = new Trie<Keyword>();
                keywords[idx].Add(name, new Keyword { word = name, hint = hint });
            }

            // instantiate sub-lexers
            var lexerList = lexerNode.SelectNodes("Lexer");
            lexer = new BaseLexer[lexerList.Count];
            firstFreeStyle = lastStyle + 1;
            for (int i = 0; i < lexerList.Count; i++)
            {
                var type = lexerList[i].GetAttributeValue("lexer");
                var param = new object[] { firstFreeStyle, lexerList[i], this.defaultLexer };
                var t = Type.GetType($"App.Lexer.{type}");
                lexer[i] = (BaseLexer)Activator.CreateInstance(t, param);
                firstFreeStyle = lexer[i].MaxStyle + 1;
            }
        }

        #region METHODS
        public virtual int Style(CodeEditor editor, int pos, int endPos)
        {
            editor.StartStyling(pos);
            //var text = editor.GetTextRange(pos, endPos - pos);

            // instantiate region class
            var c = new Region(editor);

            // continue processing from the last state
            for (var state = 0; pos < endPos; pos++)
            {
                state = ProcessState(editor, state, c.Set(pos));
                editor.SetStyling(1, StateToStyle(state));
            }

            // style partial bodies
            if (Enum.IsDefined(StateType, "Body")
                && GetStateAt(editor, Math.Max(0, pos - 1)) == (int)Enum.Parse(StateType, "Body"))
                StyleBody(editor, pos);

            return pos;
        }

        protected virtual int ProcessState(CodeEditor editor, int state, Region c) => state;

        protected virtual void StyleBody(CodeEditor editor, int pos) { }

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

        private int StyleToIdx(int style) => style - firstStyle;

        protected int GetStateAt(CodeEditor editor, int pos) {
            var style = editor.GetStyleAt(pos);
            if (firstStyle <= style && style <= lastStyle)
                return style - firstStyle;
            if (firstBaseStyle <= style && style <= lastBaseStyle)
                return style - firstBaseStyle + (int)BaseState.Default;
            return 0;
        }

        public int StateToStyle(int state)
        {
            if (state < (int)BaseState.Default)
                return state + firstStyle;
            else
                return state - (int)BaseState.Default;
        }
        
        public string GetKeywordHint(int style, string word)
        {
            return SelectKeywordInfo(style, word).FirstOrDefault().hint;
        }

        public IEnumerable<string> SelectKeywords(int style, string word)
        {
            return SelectKeywordInfo(style, word).Select(x => x.word);
        }

        public IEnumerable<Keyword> SelectKeywordInfo(int style, string word)
        {
            if (firstStyle <= style && style <= lastStyle)
                return keywords[StyleToIdx(style)] != null
                    ? keywords[StyleToIdx(style)][word]
                    : Enumerable.Empty<Keyword>();
            return lexer.SelectMany(x => x.SelectKeywordInfo(style, word));
        }
        
        public IEnumerable<Style> GetStyles()
        {
            return styles.Concat(lexer.SelectMany(x => x.GetStyles()));
        }

        public virtual Type StateType => null;

        public ILexer FindLexer(int style)
        {
            return firstStyle <= style && style <= lastStyle ? this
                : lexer.Select(x => x.FindLexer(style)).FirstOrDefault(x => x != null);
        }

        public virtual bool IsLexerType(string type) => lexerType != null ? lexerType == type : true;

        public int MaxStyle => Math.Max(lastStyle, lexer.Select(x => x.MaxStyle).MaxOr(0));
        #endregion

        #region HELPER FUNCTION
        protected int FindLastStyleOf(CodeEditor editor, int style, int from, int length)
        {
            int to = Math.Max(from - Math.Max(length, 0), 0);
            while (from > to && editor.GetStyleAt(from) != style)
                from--;
            return editor.GetStyleAt(from) == style ? from : -1;
        }
        #endregion

        #region STATE
        protected enum BaseState
        {
            Default = 16,
            String,
            Number,
            Operator,
            Braces,
            Punctuation,
            LineComment,
            BlockComment,
            Name,
        }
        protected enum BaseStyle
        {
            Braces = BaseState.Braces - BaseState.Default,
        }
        private enum FoldState : int
        {
            Unknown,
            StartFolding,
            Foldable,
            EndFolding,
        }
        #endregion
    }

    public class FxLexer : BaseLexer
    {
        public FxLexer() : base(0, LoadFxLexerFromXml(), null) { }

        public override int Style(CodeEditor editor, int pos, int endPos)
        {
            var lex = lexer.First();

            // go back to the first valid style
            var start = pos;
            for (var style = editor.GetStyleAt(start);
                start > 0 && style != lex.firstStyle;
                style = editor.GetStyleAt(start))
                start--;

            var t0 = editor.GetTextRange(pos, endPos - pos);
            var t1 = editor.GetTextRange(start, endPos - start);

            return lex.Style(editor, start, endPos);
        }

        public override Type StateType => typeof(BaseState);

        private static XmlNode LoadFxLexerFromXml()
        {
            var xml = new XmlDocument();
            xml.LoadXml(Properties.Resources.keywordsXML);
            return xml.SelectSingleNode("FxLexer");
        }
    }

    class TechLexer : BaseLexer
    {
        private int braceCount;

        public TechLexer(int nextStyle, XmlNode xml, BaseLexer defaultLexer)
            : base(nextStyle, xml, defaultLexer) { }

        #region PROCESS STATE
        protected override int ProcessState(CodeEditor editor, int state, Region c)
        {
            switch (state)
            {
                case (int)BaseState.LineComment:
                    return ProcessLineCommentState(editor, c);
                case (int)BaseState.BlockComment:
                    return ProcessBlockCommentState(editor, c);
                case (int)State.Type:
                    return ProcessTypeState(editor, c);
                case (int)BaseState.Name:
                    return ProcessNameState(editor, c);
                case (int)State.Anno:
                    return ProcessAnnoState(editor, c);
                case (int)BaseState.Braces:
                    return ProcessBraceState(editor, c);
                case (int)State.Body:
                    return ProcessBodyState(editor, c);
                default:
                    return ProcessDefaultState(editor, c);
            }
        }

        private int ProcessDefaultState(CodeEditor editor, Region c)
        {
            if (c.c == '/')
            {
                if (c.r == '/')
                    return (int)BaseState.LineComment;
                else if (c.r == '*')
                    return (int)BaseState.BlockComment;
            }
            else if (char.IsLetter(c.c))
                return (int)State.Type;
            return (int)State.Default;
        }

        private int ProcessLineCommentState(CodeEditor editor, Region c)
            => c.c == '\n' ? (int)State.Default : (int)BaseState.LineComment;

        private int ProcessBlockCommentState(CodeEditor editor, Region c)
            => c.ll == '*' && c.l == '/' ? ProcessDefaultState(editor, c) : (int)BaseState.BlockComment;

        private int ProcessTypeState(CodeEditor editor, Region c)
        {
            if (char.IsWhiteSpace(c.l) && char.IsLetter(c.c))
                return (int)BaseState.Name;
            else if (c.c == '{')
            {
                braceCount = 1;
                return (int)BaseState.Braces;
            }
            return (int)State.Type;
        }
        
        private int ProcessNameState(CodeEditor editor, Region c)
        {
            if (char.IsWhiteSpace(c.l) && char.IsLetter(c.c))
                return (int)State.Anno;
            else if (c.c == '{')
            {
                braceCount = 1;
                return (int)BaseState.Braces;
            }
            return (int)BaseState.Name;
        }
        
        private int ProcessAnnoState(CodeEditor editor, Region c)
        {
            if (c.c == '{')
            {
                braceCount = 1;
                return (int)BaseState.Braces;
            }
            return (int)State.Anno;
        }
        
        private int ProcessBraceState(CodeEditor editor, Region c)
        {
            if (c.l == '}')
                return ProcessDefaultState(editor, c);
            return ProcessBodyState(editor, c);
        }

        private int ProcessBodyState(CodeEditor editor, Region c)
        {
            switch (c.c)
            {
                case '{':
                    braceCount++;
                    break;
                case '}':
                    if (--braceCount > 0)
                        break;
                    // RE-LEX ALL BLOCKCODE PARTS
                    StyleBody(editor, c.pos);
                    return (int)BaseState.Braces;
            }
            return (int)State.Body;
        }

        protected override void StyleBody(CodeEditor editor, int pos)
        {
            // get code position before the body
            var start = Math.Max(0, pos - 1);
            for (var style = editor.GetStyleAt(start);
                start > 0 && style != (int)BaseStyle.Braces;
                style = editor.GetStyleAt(start))
                start--;

            // get header string from block position
            var header = FindLastHeaderFromPosition(editor, start);

            // find lexer that can lex this code block
            var lex = lexer.Where(x => x.IsLexerType(header[0])).FirstOrDefault();

            // re-lex code block
            lex?.Style(editor, start + 1, pos);

            // continue styling from the last position
            editor.StartStyling(pos);
        }
        #endregion

        #region HELPER FUNCTION
        private string[] FindLastHeaderFromPosition(CodeEditor editor, int pos)
        {
            int bracePos = FindLastStyleOf(editor, (int)BaseStyle.Braces, pos, pos);
            int typePos = FindLastStyleOf(editor, (int)State.Type + firstStyle, bracePos, bracePos);
            int annoPos = FindLastStyleOf(editor, (int)State.Anno + firstStyle, bracePos, bracePos - typePos);

            return new string[] {
                typePos >= 0 ? editor.GetWordFromPosition(typePos) : null,
                annoPos >= 0 ? editor.GetWordFromPosition(annoPos) : null,
            };
        }
        #endregion

        #region STATE
        private enum State : int { Default, Type, Anno, Body }

        public override Type StateType => typeof(State);
        #endregion
    }

    class BlockLexer : BaseLexer
    {
        public BlockLexer(int nextStyle, XmlNode xml, BaseLexer defaultLexer)
            : base(nextStyle, xml, defaultLexer) { }

        #region PROCESS STATE
        protected override int ProcessState(CodeEditor editor, int state, Region c)
        {
            switch (state)
            {
                case (int)BaseState.LineComment:
                    return ProcessLineCommentState(editor, c);
                case (int)BaseState.BlockComment:
                    return ProcessBlockCommentState(editor, c);
                case (int)State.Command:
                    return ProcessCommandState(editor, c);
                case (int)State.CommandBody:
                    return ProcessCommandBodyState(editor, c);
                default:
                    return ProcessDefaultState(editor, c);
            }
        }

        private int ProcessDefaultState(CodeEditor editor, Region c)
        {
            if (c.c == '/')
            {
                if (c.r == '/')
                    return (int)BaseState.LineComment;
                else if (c.r == '*')
                    return (int)BaseState.BlockComment;
            }
            else if (char.IsLetter(c.c))
                return (int)State.Command;
            return (int)State.Default;
        }

        private int ProcessLineCommentState(CodeEditor editor, Region c)
            => c.c == '\n' ? (int)State.Default : (int)BaseState.LineComment;

        private int ProcessBlockCommentState(CodeEditor editor, Region c)
            => c.ll == '*' && c.l == '/' ? ProcessDefaultState(editor, c) : (int)BaseState.BlockComment;

        private int ProcessCommandState(CodeEditor editor, Region c)
        {
            if (char.IsWhiteSpace(c.c))
                return (int)State.CommandBody;
            return (int)State.Command;
        }

        private int ProcessCommandBodyState(CodeEditor editor, Region c)
        {
            if (c.c == '\n' && c.l != '.' && c.ll != '.')
            {
                // RE-LEX ALL COMMANDCODE PARTS
                // get code region of the block
                var start = FindLastStyleOf(editor, firstStyle + (int)State.Command, c.pos, c.pos);
                // get header string from block position
                var cmd = FindLastCommandFromPosition(editor, start);
                // find lexer that can lex this code block
                var lex = lexer.Where(x => x.IsLexerType(cmd)).FirstOr(defaultLexer);
                // re-lex code block
                lex.Style(editor, start + 1, c.pos);
                // continue styling from the last position
                editor.StartStyling(c.pos);
                return (int)State.Default;
            }
            return (int)State.CommandBody;
        }
        #endregion

        #region HELPER FUNCTION
        private string FindLastCommandFromPosition(CodeEditor editor, int pos)
        {
            int cmdPos = FindLastStyleOf(editor, (int)State.Command + firstStyle, pos, pos);
            return cmdPos >= 0 ? editor.GetWordFromPosition(cmdPos) : null;
        }
        #endregion

        #region STATE
        private enum State : int { Default, Command, CommandBody }

        public override Type StateType => typeof(State);
        #endregion
    }

    class CommandLexer : BaseLexer
    {
        private int stringStartPos;

        public CommandLexer(int nextStyle, XmlNode xml, BaseLexer defaultLexer)
            : base(nextStyle, xml, defaultLexer) { }

        #region PROCESS STATE
        protected override int ProcessState(CodeEditor editor, int state, Region c)
        {
            switch (state)
            {
                case (int)State.Argument:
                    return ProcessArgumentState(editor, c);
                case (int)BaseState.Number:
                    return ProcessNumberState(editor, c);
                case (int)BaseState.String:
                    return ProcessStringState(editor, c);
                default:
                    return ProcessDefaultState(editor, c);
            }
        }

        private int ProcessDefaultState(CodeEditor editor, Region c)
        {
            if (c.c == '"')
            {
                stringStartPos = c.pos;
                return (int)BaseState.String;
            }
            else if(char.IsNumber(c.c) || (c.c == '.' && char.IsNumber(c.r)))
                return (int)BaseState.Number;
            else if (char.IsLetter(c.c))
                return (int)State.Argument;
            return (int)State.Default;
        }

        private int ProcessArgumentState(CodeEditor editor, Region c)
        {
            if (char.IsWhiteSpace(c.c))
            {
                int start = editor.WordStartPosition(c.pos - 1, true);
                var word = editor.GetTextRange(start, c.pos - start);
                if (!keywords[(int)State.Argument]?[word].Any(x => x.word == word) ?? false)
                {
                    var p = editor.GetEndStyled();
                    editor.StartStyling(start);
                    editor.SetStyling(c.pos - start, StateToStyle((int)State.Default));
                    editor.StartStyling(p);
                }
                return (int)State.Default;
            }
            return (int)State.Argument;
        }

        private int ProcessNumberState(CodeEditor editor, Region c)
        {
            return char.IsNumber(c.c)
                || c.c == '.' || c.c == 'x'
                || ('a' <= c.c && c.c <= 'f')
                || ('A' <= c.c && c.c <= 'F')
                ? (int)BaseState.Number
                : (int)State.Default;
        }

        private int ProcessStringState(CodeEditor editor, Region c)
        {
            return c.pos - 1 != stringStartPos && c.l == '"'
                ? ProcessDefaultState(editor, c)
                : (int)BaseState.String;
        }
        #endregion

        #region STATE
        private enum State : int { Default, Argument }

        public override Type StateType => typeof(State);
        #endregion
    }

    class DefaultLexer : BaseLexer
    {
        private int stringStartPos;

        public DefaultLexer(int nextStyle, XmlNode xml, BaseLexer defaultLexer)
            : base(nextStyle, xml, defaultLexer) { }

        #region PROCESS STATE
        protected override int ProcessState(CodeEditor editor, int state, Region c)
        {
            switch (state)
            {
                case (int)BaseState.Number:
                    return ProcessNumberState(editor, c);
                case (int)BaseState.String:
                    return ProcessStringState(editor, c);
                default:
                    return ProcessDefaultState(editor, c);
            }
        }

        private int ProcessDefaultState(CodeEditor editor, Region c)
        {
            if (c.c == '"')
            {
                stringStartPos = c.pos;
                return (int)BaseState.String;
            }
            else if (char.IsNumber(c.c) || (c.c == '.' && char.IsNumber(c.r)))
                return (int)BaseState.Number;
            return (int)BaseState.Default;
        }
        
        private int ProcessNumberState(CodeEditor editor, Region c)
        {
            return char.IsNumber(c.c)
                || c.c == '.' || c.c == 'x'
                || ('a' <= c.c && c.c <= 'f')
                || ('A' <= c.c && c.c <= 'F')
                ? (int)BaseState.Number
                : (int)BaseState.Default;
        }

        private int ProcessStringState(CodeEditor editor, Region c)
        {
            return c.pos - 1 != stringStartPos && c.l == '"'
                ? ProcessDefaultState(editor, c)
                : (int)BaseState.String;
        }
        #endregion

        #region STATE
        private enum State : int { }

        public override Type StateType => typeof(State);
        #endregion
    }

    class GlslLexer : BaseLexer
    {
        int braceCount;
        char openBrace;
        char closingBrace;

        public GlslLexer(int nextStyle, XmlNode xml, BaseLexer defaultLexer)
            : base(nextStyle, xml, defaultLexer) { }

        #region METHODS
        protected override int ProcessState(CodeEditor editor, int state, Region c)
        {
            switch (state)
            {
                case (int)BaseState.Braces:
                    return ProcessBracesState(editor, c);
                case (int)BaseState.LineComment:
                    return ProcessLineCommentState(editor, c);
                case (int)BaseState.BlockComment:
                    return ProcessBlockCommentState(editor, c);

                case (int)State.Preprocessor:
                    return ProcessPreprocessorState(editor, c);
                case (int)State.PreprocessorBody:
                    return ProcessPreprocessorBodyState(editor, c);
                case (int)State.Keyword:
                    return ProcessKeywordState(editor, c);
                case (int)State.Body:
                    return ProcessBodyState(editor, c);

                default:
                    return ProcessDefaultState(editor, c);
            }
        }

        private int ProcessDefaultState(CodeEditor editor, Region c)
        {
            if (c.c == '#')
                return (int)State.Preprocessor;

            else if (c.c == '/')
            {
                if (c.r == '/')
                    return (int)BaseState.LineComment;
                else if (c.r == '*')
                    return (int)BaseState.BlockComment;
            }

            else if (c.c == '(')
            {
                braceCount = 1;
                openBrace = '(';
                closingBrace = ')';
                return (int)BaseState.Braces;
            }

            else if (c.c == '{')
            {
                braceCount = 1;
                openBrace = '{';
                closingBrace = '}';
                return (int)BaseState.Braces;
            }

            else if (char.IsLetter(c.c))
                return (int)State.Keyword;

            return (int)State.Default;
        }

        private int ProcessLineCommentState(CodeEditor editor, Region c)
            => c.c == '\n' ? (int)State.Default : (int)BaseState.LineComment;

        private int ProcessBlockCommentState(CodeEditor editor, Region c)
            => c.ll == '*' && c.l == '/' ? ProcessDefaultState(editor, c) : (int)BaseState.BlockComment;

        private int ProcessPreprocessorState(CodeEditor editor, Region c)
        {
            if (c.c == '\n')
                return (int)State.Default;
            else if (char.IsWhiteSpace(c.c))
                return (int)State.PreprocessorBody;
            return (int)State.Preprocessor;
        }

        private int ProcessPreprocessorBodyState(CodeEditor editor, Region c)
        {
            if (c.c == '\n')
            {
                // RE-LEX ALL PREPROCESSORCODE PARTS
                // get code region of the block
                var start = FindLastStyleOf(editor, firstStyle + (int)State.Preprocessor, c.pos, c.pos);
                // re-lex code block
                defaultLexer.Style(editor, start + 1, c.pos);
                // continue styling from the last position
                editor.StartStyling(c.pos);
                return (int)State.Default;
            }
            return (int)State.PreprocessorBody;
        }

        private int ProcessKeywordState(CodeEditor editor, Region c)
        {
            if (!char.IsLetterOrDigit(c.c) && c.c != '_')
            {
                var start = editor.WordStartPosition(c.pos, false);
                var length = c.pos - start;
                var word = editor.GetTextRange(start, length);

                // is data type keyword
                if (keywords[(int)State.DataType][word].Any(x => x.word == word))
                {
                    editor.StartStyling(start);
                    editor.SetStyling(length, StateToStyle((int)State.DataType));
                    editor.StartStyling(c.pos);
                }

                // is not a keyword
                else if (!keywords[(int)State.Keyword][word].Any(x => x.word == word))
                {
                    editor.StartStyling(start);
                    editor.SetStyling(length, StateToStyle((int)State.Default));
                    editor.StartStyling(c.pos);
                }

                return ProcessDefaultState(editor, c);
            }
            return (int)State.Keyword;
        }

        private int ProcessBracesState(CodeEditor editor, Region c)
        {
            if (braceCount == 0 && c.l == closingBrace)
                return ProcessDefaultState(editor, c);
            return ProcessBodyState(editor, c);
        }

        private int ProcessBodyState(CodeEditor editor, Region c)
        {
            if (c.c == openBrace)
                braceCount++;

            else if (c.c == closingBrace && --braceCount == 0)
            {
                // RE-LEX ALL BLOCKCODE PARTS
                StyleBody(editor, c.pos);
                return (int)BaseState.Braces;
            }

            return (int)State.Body;
        }

        protected override void StyleBody(CodeEditor editor, int pos)
        {
            // get code position before the body
            var start = Math.Max(0, pos - 1);
            for (var style = editor.GetStyleAt(start);
                start > 0 && style != (int)BaseStyle.Braces;
                style = editor.GetStyleAt(start))
                start--;

            // get the lexer type of the body
            string type = null;
            switch (openBrace)
            {
                case '(':
                    // get preceding word from brace position
                    var wordStart = editor.WordStartPosition(start, false);
                    type = editor.GetWordFromPosition(wordStart);

                    if (type != "layout")
                        type = "struct";
                    break;
                case '{':
                    // get preceding non-whitespace position from brace position
                    var bracePos = start - 1;
                    while (bracePos >= 0
                        && (char)editor.GetCharAt(bracePos) != ';'
                        && char.IsWhiteSpace((char)editor.GetCharAt(bracePos)))
                        bracePos--;

                    // if the preceding char is a closing brace, we have to lex a function body
                    type = (char)editor.GetCharAt(bracePos) == ')' ? "function" : "struct";
                    break;
            }

            // find lexer that can lex this code block
            var lex = lexer.Where(x => x.IsLexerType(type)).FirstOrDefault();

            // re-lex body of the uniform block, struct, layout or function
            lex?.Style(editor, start + 1, pos);

            // continue styling from the last position
            editor.StartStyling(pos);
        }
        #endregion

        #region STATE
        private enum State : int
        {
            Default,
            Keyword,
            DataType,
            Preprocessor,
            PreprocessorBody,
            Body,
        }

        public override Type StateType => typeof(State);
        #endregion
    }

    class GlslLayoutLexer : BaseLexer
    {
        public GlslLayoutLexer(int nextStyle, XmlNode xml, BaseLexer defaultLexer)
            : base(nextStyle, xml, defaultLexer) { }

        #region METHODS
        protected override int ProcessState(CodeEditor editor, int state, Region c)
        {
            return (int)State.Default;
        }
        #endregion

        #region STATE
        private enum State : int { Default, Qualifier }

        public override Type StateType => typeof(State);
        #endregion
    }

    class GlslStructLexer : BaseLexer
    {
        public GlslStructLexer(int nextStyle, XmlNode xml, BaseLexer defaultLexer)
            : base(nextStyle, xml, defaultLexer) { }

        #region METHODS
        protected override int ProcessState(CodeEditor editor, int state, Region c)
        {
            return (int)State.Default;
        }
        #endregion

        #region STATE
        private enum State : int { Default, DataType }

        public override Type StateType => typeof(State);
        #endregion
    }

    class GlslFunctionLexer : BaseLexer
    {
        public GlslFunctionLexer(int nextStyle, XmlNode xml, BaseLexer defaultLexer)
            : base(nextStyle, xml, defaultLexer) { }

        #region METHODS
        protected override int ProcessState(CodeEditor editor, int state, Region c)
        {
            return (int)State.Default;
        }
        #endregion

        #region STATE
        private enum State : int { Default, Keyword, DataType, Function }

        public override Type StateType => typeof(State);
        #endregion
    }

    public class Region
    {
        private CodeEditor editor;
        public int pos;
        public char ll;
        public char l;
        public char c;
        public char r;

        public Region(CodeEditor editor)
        {
            this.editor = editor;
        }

        public Region Set(int value)
        {
            pos = value;
            ll = (char)(value - 2 < 0 ? 0 : editor.GetCharAt(value - 2));
            l = (char)(value - 1 < 0 ? 0 : editor.GetCharAt(value - 1));
            c = (char)editor.GetCharAt(value);
            r = (char)(value + 1 >= editor.TextLength ? 0 : editor.GetCharAt(value + 1));
            return this;
        }
    }
}
