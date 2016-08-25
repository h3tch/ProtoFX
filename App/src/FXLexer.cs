using ScintillaNET;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml;
using SciStyle = ScintillaNET.Style;

namespace App.Lexer
{
    public interface ILexer
    {
        /// <summary>
        /// Style the specified text position.
        /// </summary>
        /// <param name="editor"></param>
        /// <param name="pos"></param>
        /// <param name="endPos"></param>
        /// <returns></returns>
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
        /// <returns>List of styles.</returns>
        IEnumerable<Style> Styles { get; }
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
        #region FIELDS
        protected string lexerType;
        protected BaseLexer[] lexer;
        protected Trie<Keyword>[] keywords;
        protected Style[] styles;
        public IEnumerable<Style> Styles => styles.Concat(lexer.SelectMany(x => x.Styles));
        public int firstStyle { get; private set; }
        public int lastStyle { get; private set; }
        public static int firstBaseStyle { get; private set; }
        public static int lastBaseStyle { get; private set; }
        public virtual Type StateType => null;
        public int MaxStyle => Math.Max(lastStyle, lexer.Select(x => x.MaxStyle).MaxOr(0));
        protected BaseLexer parentLexer;
        protected BaseLexer defaultLexer;
        private static Regex regexHint = new Regex(@"\n.*\\");
        #endregion

        public BaseLexer(int firstFreeStyle, XmlNode lexerNode, BaseLexer parent)
        {
            parentLexer = parent;
            defaultLexer = GetType().Name == "DefaultLexer" ? null
                : new DefaultLexer(firstFreeStyle, null, this);

            // get style and state ranges
            var stateValues = Enum.GetValues(StateType);
            int styleCount = stateValues.Length;
            int firstState = styleCount > 0 ? ((int[])stateValues).Min() : 0;

            // adjust style ranges if they fall into the Scintilla styles
            firstStyle = firstFreeStyle;
            if (SciStyle.Default <= firstStyle + styleCount && firstStyle <= SciStyle.CallTip)
                firstStyle = SciStyle.CallTip + 1;
            lastStyle = firstStyle + styleCount - 1;
            firstFreeStyle = lastStyle + 1;

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

            // SET TO DEFAULT LEXER
            if (lexerNode == null)
            {
                lexerType = "default";
                lexer = new BaseLexer[0];
                return;
            }

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
                if (hint != null && hint.IndexOf('\\') >= 0)
                    hint = regexHint.Replace(hint, "\n");
                if (keywords[idx] == null)
                    keywords[idx] = new Trie<Keyword>();
                keywords[idx].Add(name, new Keyword { word = name, hint = hint });
            }

            // instantiate sub-lexers
            var lexerList = lexerNode.SelectNodes("Lexer");
            lexer = new BaseLexer[lexerList.Count];
            for (int i = 0; i < lexerList.Count; i++)
            {
                var type = lexerList[i].GetAttributeValue("lexer");
                var param = new object[] { firstFreeStyle, lexerList[i], this };
                var t = Type.GetType($"App.Lexer.{type}");
                lexer[i] = (BaseLexer)Activator.CreateInstance(t, param);
                firstFreeStyle = lexer[i].MaxStyle + 1;
            }
        }

        #region METHODS
        public virtual int Style(CodeEditor editor, int pos, int endPos)
        {
            editor.StartStyling(pos);
            var textLen = editor.TextLength;

            // instantiate region class
            var c = new Region(editor, pos);

            // continue processing from the last state
            for (var state = GetPrevState(c); c.Pos < textLen; c.Pos++)
            {
                var curStyle = c.GetStyleAt(0);
                state = ProcessState(editor, state, c, textLen);
                var newStyle = StateToStyle(state);

                // reached end position
                if (c.Pos >= endPos && (c.Pos >= textLen || (newStyle != 0 && curStyle == newStyle)))
                    break;

                // the lexer can no longer handle the code
                // go to the parent lexer and lex the code there
                if (state < 0)
                    return parentLexer?.Style(editor, c.Pos, endPos) ?? endPos;

                // style the current character
                editor.SetStyling(1, newStyle);
            }

            return c.Pos;
        }

        public virtual int ProcessState(CodeEditor editor, int state, Region c, int endPos) => state;

        public virtual int ProcessDefaultState(CodeEditor editor, Region c) => 0;

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

        public int StateToStyle(int state) => state + ((state < firstBaseState) ? firstStyle : -firstBaseState);
        
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
            {
                int idx = StyleToIdx(style);
                return keywords[idx] != null ? keywords[idx][word] : Enumerable.Empty<Keyword>();
            }
            return lexer.SelectMany(x => x.SelectKeywordInfo(style, word));
        }
        
        public ILexer FindLexerForStyle(int style)
        {
            return firstStyle <= style && style <= lastStyle
                ? this
                : lexer.Select(x => x.FindLexerForStyle(style)).FirstOrDefault(x => x != null);
        }

        public virtual bool IsLexerForType(string type) => lexerType == type;
        #endregion

        #region HELPER FUNCTION
        protected int FindLastStyleOf(CodeEditor editor, int style, int from, int length)
        {
            int to = Math.Max(from - Math.Max(length, 0), 0);
            while (from > to && editor.GetStyleAt(from) != style)
                from--;
            return editor.GetStyleAt(from) == style ? from : -1;
        }

        private int GetPrevState(Region c)
        {
            int state = 0;
            if (c.Pos > 0)
            {
                // if necessary convert style to state
                var style = c.GetStyleAt(-1);
                if (firstStyle <= style && style <= lastStyle)
                    state = style - firstStyle;
            }
            return state;
        }
        #endregion

        #region STATE
        protected const int firstBaseState = (int)BaseState.Default;
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
            // Find a lexer that can continue lexing.
            // Use the previous state to select the right lexer.
            // As long as no lexer can be found, move the start position backward.
            ILexer lex = null;
            while (pos >= 0 && lex == null)
                lex = FindLexerForStyle(editor.GetStyleAt(pos--));

            // start lexing from the newly determined start position
            return (lex ?? lexer.First()).Style(editor, pos + 1, endPos);
        }
        
        private new ILexer FindLexerForStyle(int style)
            => lexer.Select(x => x.FindLexerForStyle(style)).FirstOr(x => x != null, null);

        private static XmlNode LoadFxLexerFromXml()
        {
            var xml = new XmlDocument();
            xml.LoadXml(Properties.Resources.keywordsXML);
            return xml.SelectSingleNode("FxLexer");
        }

        public override Type StateType => typeof(BaseState);
    }

    class TechLexer : BaseLexer
    {
        public TechLexer(int nextStyle, XmlNode xml, BaseLexer parentLexer)
            : base(nextStyle, xml, parentLexer) { }

        #region PROCESS STATE
        public override int ProcessState(CodeEditor editor, int state, Region c, int endPos)
        {
            switch (state)
            {
                case (int)BaseState.LineComment:
                    return StateHelper.ProcessLineCommentState(this, editor, c);
                case (int)BaseState.BlockComment:
                    return StateHelper.ProcessBlockCommentState(this, editor, c);
                case (int)State.Preprocessor:
                    return ProcessPreprocessorState(editor, c);
                case (int)State.PreprocessorBody:
                    return ProcessPreprocessorBodyState(editor, c);
                case (int)State.Type:
                    return ProcessTypeState(editor, c);
                case (int)BaseState.Name:
                    return ProcessNameState(editor, c);
                case (int)State.Anno:
                    return ProcessAnnoState(editor, c);
                case (int)BaseState.Braces:
                    return ProcessBraceState(editor, c, endPos);
            }
            return ProcessDefaultState(editor, c);
        }

        public override int ProcessDefaultState(CodeEditor editor, Region c)
        {
            if (c.c == '/')
            {
                if (c[1] == '/')
                    return (int)BaseState.LineComment;
                else if (c[1] == '*')
                    return (int)BaseState.BlockComment;
            }
            if (c.c == '#')
                return (int)State.Preprocessor;
            if (char.IsLetter(c.c))
                return (int)State.Type;
            return (int)State.Default;
        }

        private int ProcessTypeState(CodeEditor editor, Region c)
        {
            if (char.IsWhiteSpace(c[-1]) && char.IsLetter(c.c))
                return (int)BaseState.Name;
            if (c.c == '{')
                return (int)BaseState.Braces;
            return (int)State.Type;
        }
        
        private int ProcessNameState(CodeEditor editor, Region c)
        {
            if (char.IsWhiteSpace(c[-1]) && char.IsLetter(c.c))
                return (int)State.Anno;
            if (c.c == '{')
                return (int)BaseState.Braces;
            return (int)BaseState.Name;
        }
        
        private int ProcessAnnoState(CodeEditor editor, Region c)
        {
            if (c.c == '{')
                return (int)BaseState.Braces;
            return (int)State.Anno;
        }
        
        private int ProcessBraceState(CodeEditor editor, Region c, int endPos)
        {
            if (c[-1] == '{')
            {
                // get header string from block position
                var header = FindLastHeaderFromPosition(editor, c.Pos);

                // find lexer that can lex this code block
                var lex = lexer.Where(x => x.IsLexerForType(header[0])).FirstOr(null);

                // skip code block
                if (lex == null)
                {
                    int start = c.Pos;
                    for (int n = 0; c.Pos < endPos && n >= 0; c.Pos++)
                    {
                        if (c.c == '{') n++;
                        if (c.c == '}') n--;
                    }
                    // make code block invalid
                    editor.SetStyling(--c.Pos - start, StateToStyle((int)BaseState.Default));
                    return (int)(c.c == '}' ? BaseState.Braces : BaseState.Default);
                }

                // re-lex code block
                c.Pos = lex.Style(editor, c.Pos, endPos);
            }
            return ProcessDefaultState(editor, c);
        }

        private int ProcessPreprocessorState(CodeEditor editor, Region c)
        {
            if (c.c == '\n')
                return (int)State.Default;
            if (char.IsWhiteSpace(c.c))
                return (int)State.PreprocessorBody;
            return (int)State.Preprocessor;
        }

        private int ProcessPreprocessorBodyState(CodeEditor editor, Region c)
        {
            // still inside the preprocessor body
            if (c.c != '\n')
                return (int)State.PreprocessorBody;

            // RE-LEX ALL PREPROCESSORCODE PARTS

            // get code region of the block
            var start = FindLastStyleOf(editor, firstStyle + (int)State.Preprocessor, c.Pos, c.Pos);
            // re-lex code block
            defaultLexer.Style(editor, start + 1, c.Pos);
            // continue styling from the last position
            editor.StartStyling(c.Pos);
            return (int)State.Default;
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
        private enum State : int { Default, Preprocessor, PreprocessorBody, Type, Anno }

        public override Type StateType => typeof(State);
        #endregion
    }

    class BlockLexer : BaseLexer
    {
        public BlockLexer(int nextStyle, XmlNode xml, BaseLexer parentLexer)
            : base(nextStyle, xml, parentLexer) { }

        #region PROCESS STATE
        public override int ProcessState(CodeEditor editor, int state, Region c, int endPos)
        {
            switch (state)
            {
                case (int)BaseState.Braces:
                    return -1; // exit lexer
                case (int)BaseState.LineComment:
                    return StateHelper.ProcessLineCommentState(this, editor, c);
                case (int)BaseState.BlockComment:
                    return StateHelper.ProcessBlockCommentState(this, editor, c);
                case (int)State.Command:
                    return ProcessCommandState(editor, c, endPos);
            }
            return ProcessDefaultState(editor, c);
        }

        public override int ProcessDefaultState(CodeEditor editor, Region c)
        {
            switch (c.c)
            {
                case '.':
                    return (int)BaseState.Punctuation;
                case '}':
                    // end of the block
                    return (int)BaseState.Braces;
                case '/':
                    // start line comment
                    if (c[1] == '/') return (int)BaseState.LineComment;
                    // start block comment
                    if (c[1] == '*') return (int)BaseState.BlockComment;
                    break;
            }

            // is the beginning of a command
            if (char.IsLetter(c.c))
                return (int)State.Command;
            
            return (int)State.Default;
        }

        private int ProcessCommandState(CodeEditor editor, Region c, int endPos)
        {
            // is still part of the command
            if (!char.IsWhiteSpace(c.c))
                return (int)State.Command;

            // get header string from block position
            var cmd = editor.GetWordFromPosition(c.Pos - 1);

            // find lexer that can lex this code block
            var lex = lexer.Where(x => x.IsLexerForType(cmd)).FirstOr(defaultLexer);

            // re-lex code block
            c.Pos = lex.Style(editor, c.Pos, endPos);

            // continue lexing
            return ProcessDefaultState(editor, c);
        }
        #endregion

        #region STATE
        private enum State : int { Default, Command }

        public override Type StateType => typeof(State);
        #endregion
    }

    class CommandLexer : BaseLexer
    {
        public CommandLexer(int nextStyle, XmlNode xml, BaseLexer parentLexer)
            : base(nextStyle, xml, parentLexer) { }

        #region PROCESS STATE
        public override int ProcessState(CodeEditor editor, int state, Region c, int endPos)
        {
            switch (state)
            {
                case (int)State.Argument:
                    return ProcessArgumentState(editor, c);
                case (int)BaseState.Number:
                    return StateHelper.ProcessNumberState(this, editor, c);
                case (int)BaseState.String:
                    return StateHelper.ProcessStringState(this, editor, c);
            }
            return ProcessDefaultState(editor, c);
        }

        public override int ProcessDefaultState(CodeEditor editor, Region c)
        {
            switch (c.c)
            {
                case '"':
                    // the beginning of the string
                    return (int)BaseState.String;
                case '.':
                    // the beginning of a number or punctuation
                    return (int)(char.IsNumber(c[1]) ? BaseState.Number : BaseState.Punctuation);
                case '\n':
                    // exit lexer if line does not end with "."
                    return StateHelper.DefaultProcessNewLine(editor, c);
            }

            // start of a number
            if(char.IsNumber(c.c))
                return (int)BaseState.Number;

            // is the beginning of an argument
            if (char.IsLetter(c.c))
                return (int)State.Argument;

            return (int)State.Default;
        }

        private int ProcessArgumentState(CodeEditor editor, Region c)
        {
            // is still part of the argument
            if (!char.IsWhiteSpace(c.c))
                return (int)State.Argument;

            // get preceding word
            int start = editor.WordStartPosition(c.Pos - 1, true);
            var word = editor.GetTextRange(start, c.Pos - start);

            // is not an argument keyword
            if (!keywords[(int)State.Argument]?[word].Any(x => x.word == word) ?? false)
            {
                // re-lex to default style
                var p = editor.GetEndStyled();
                editor.StartStyling(start);
                editor.SetStyling(c.Pos - start, StateToStyle((int)State.Default));
                editor.StartStyling(p);
            }

            // is white space which has the default style
            return (int)State.Default;
        }
        #endregion

        #region STATE
        private enum State : int { Default, Argument }

        public override Type StateType => typeof(State);
        #endregion
    }

    class DefaultLexer : BaseLexer
    {
        public DefaultLexer(int nextStyle, XmlNode xml, BaseLexer parentLexer)
            : base(nextStyle, xml, parentLexer) { }

        #region PROCESS STATE
        public override int ProcessState(CodeEditor editor, int state, Region c, int endPos)
        {
            switch (state)
            {
                case (int)BaseState.Number:
                    return StateHelper.ProcessNumberState(this, editor, c);
                case (int)BaseState.String:
                    return StateHelper.ProcessStringState(this, editor, c);
                case (int)BaseState.LineComment:
                    return StateHelper.ProcessLineCommentState(this, editor, c);
                case (int)BaseState.BlockComment:
                    return StateHelper.ProcessBlockCommentState(this, editor, c);
            }
            return ProcessDefaultState(editor, c);
        }

        public override int ProcessDefaultState(CodeEditor editor, Region c)
        {
            switch (c.c)
            {
                case '"':
                case '\'':
                    // the beginning of a string
                    return (int)BaseState.String;

                case '.':
                    // the beginning of a number or punctuation
                    return (int)(char.IsNumber(c[1]) ? BaseState.Number : BaseState.Punctuation);
                case ',':
                case ';':
                    // a punctuation
                    return (int)BaseState.Punctuation;

                case '*':
                case '+':
                case '-':
                    // an operator
                    return (int)BaseState.Operator;
                case '/':
                    // the beginning of a line comment
                    if (c[1] == '/') return (int)BaseState.LineComment;
                    // the beginning of a block comment
                    if (c[1] == '*') return (int)BaseState.BlockComment;
                    // an operator
                    return (int)BaseState.Operator;

                case '(': case ')':
                case '[': case ']':
                    // a brace
                    return (int)BaseState.Braces;

                case '}':
                    // exit lexer
                    return -1;

                case '\n':
                    // exit lexer if line does not end with "."
                    return StateHelper.DefaultProcessNewLine(editor, c);
            }

            // the beginning of a number
            if (char.IsNumber(c.c))
                return (int)BaseState.Number;

            // the beginning of an operator
            if (char.IsSymbol(c.c))
                return (int)BaseState.Operator;
            
            return (int)BaseState.Default;
        }
        #endregion

        #region STATE
        private enum State : int { }

        public override Type StateType => typeof(State);
        #endregion
    }

    class GlslLexer : BaseLexer
    {
        public GlslLexer(int nextStyle, XmlNode xml, BaseLexer parentLexer)
            : base(nextStyle, xml, parentLexer) { }

        #region METHODS
        public override int ProcessState(CodeEditor editor, int state, Region c, int endPos)
        {
            switch (state)
            {
                case (int)BaseState.Braces:
                    return ProcessBracesState(editor, c, endPos);
                case (int)BaseState.LineComment:
                    return StateHelper.ProcessLineCommentState(this, editor, c);
                case (int)BaseState.BlockComment:
                    return StateHelper.ProcessBlockCommentState(this, editor, c);

                case (int)State.Preprocessor:
                    return ProcessPreprocessorState(editor, c, endPos);
                case (int)State.Keyword:
                    return ProcessKeywordState(editor, c);
            }
            return ProcessDefaultState(editor, c);
        }

        public override int ProcessDefaultState(CodeEditor editor, Region c)
        {
            switch (c.c)
            {
                case '#':
                    // the beginning of a preprocessor statement
                    return (int)State.Preprocessor;
                case '/':
                    // the beginning of a line comment
                    if (c[1] == '/') return (int)BaseState.LineComment;
                    // the beginning of a block comment
                    if (c[1] == '*') return (int)BaseState.BlockComment;
                    break;
                case '(': case ')':
                case '{': case '}':
                    // a brace
                    return (int)BaseState.Braces;
                case '.':
                case ';':
                    // a punctuation
                    return (int)BaseState.Punctuation;
                case '=':
                    // on operator
                    return (int)BaseState.Operator;
            }

            // the beginning of a keyword
            if (char.IsLetter(c.c))
                return (int)State.Keyword;

            return (int)State.Default;
        }

        private int ProcessPreprocessorState(CodeEditor editor, Region c, int endPos)
        {
            if (c.c == '\n')
                return (int)State.Default;
            if (char.IsWhiteSpace(c.c))
            {
                c.Pos = defaultLexer.Style(editor, c.Pos, endPos);
                return ProcessDefaultState(editor, c);
            }
            return (int)State.Preprocessor;
        }

        private int ProcessKeywordState(CodeEditor editor, Region c)
        {
            // position still inside the word range
            if (char.IsLetterOrDigit(c.c) || c.c == '_')
                return (int)State.Keyword;

            // get previous word
            var start = editor.WordStartPosition(c.Pos, false);
            var length = c.Pos - start;
            var word = editor.GetTextRange(start, length);

            // is data type keyword
            if (keywords[(int)State.DataType][word].Any(x => x.word == word))
            {
                editor.StartStyling(start);
                editor.SetStyling(length, StateToStyle((int)State.DataType));
                editor.StartStyling(c.Pos);
            }
            // is not a keyword
            else if (!keywords[(int)State.Keyword][word].Any(x => x.word == word))
            {
                editor.StartStyling(start);
                editor.SetStyling(length, StateToStyle((int)State.Default));
                editor.StartStyling(c.Pos);
            }

            return ProcessDefaultState(editor, c);
        }

        private int ProcessBracesState(CodeEditor editor, Region c, int endPos)
        {
            // get the lexer type of the body
            string type = null;
            switch (c[-1])
            {
                case '(':
                    // get preceding word from brace position
                    var wordStart = editor.WordStartPosition(c.Pos - 1, false);
                    type = editor.GetWordFromPosition(wordStart);

                    if (type != "layout")
                        type = "functionheader";
                    break;
                case '{':
                    // get preceding non-whitespace position from brace position
                    var bracePos = c.Pos - 2;
                    while (bracePos >= 0
                        && (char)editor.GetCharAt(bracePos) != ';'
                        && char.IsWhiteSpace((char)editor.GetCharAt(bracePos)))
                        bracePos--;

                    // if the preceding char is a closing brace, we have to lex a function body
                    var braceChar = (char)editor.GetCharAt(bracePos);
                    type = braceChar == ')' ? "function" : "struct";
                    break;
                case '}':
                    return -1;
                default:
                    return ProcessDefaultState(editor, c);
            }

            // find lexer that can lex this code block
            var lex = lexer.Where(x => x.IsLexerForType(type)).FirstOr(null);

            // re-lex body of the uniform block, struct, layout or function
            c.Pos = lex?.Style(editor, c.Pos, endPos) ?? c.Pos;

            // continue styling from the last position
            editor.StartStyling(c.Pos);
            return ProcessDefaultState(editor, c);
        }
        #endregion

        #region STATE
        private enum State : int { Default, Keyword, DataType, Preprocessor }

        public override Type StateType => typeof(State);
        #endregion
    }

    class GlslLayoutLexer : BaseLexer
    {
        public GlslLayoutLexer(int nextStyle, XmlNode xml, BaseLexer parentLexer)
            : base(nextStyle, xml, parentLexer) { }

        #region METHODS
        public override int ProcessState(CodeEditor editor, int state, Region c, int endPos)
        {
            switch (state)
            {
                case (int)BaseState.Braces:
                    // exit lexer
                    return -1;
                case (int)State.Qualifier:
                    return ProcessQualifierState(editor, c);
                case (int)BaseState.Number:
                    return StateHelper.ProcessNumberState(this, editor, c);
            }
            return ProcessDefaultState(editor, c);
        }

        public override int ProcessDefaultState(CodeEditor editor, Region c)
        {
            switch (c.c)
            {
                case ')': return (int)BaseState.Braces;
                case '=': return (int)BaseState.Operator;
                case ',': return (int)BaseState.Punctuation;
            }

            // the beginning of a number
            if (char.IsDigit(c.c))
                return (int)BaseState.Number;

            // the beginning of a qualifier keyword
            if (char.IsLetter(c.c))
                return (int)State.Qualifier;

            return (int)State.Default;
        }
        
        private int ProcessQualifierState(CodeEditor editor, Region c)
        {
            // position still inside the word range
            if (char.IsLetterOrDigit(c.c) || c.c == '_')
                return (int)State.Qualifier;

            // get previous word
            var start = editor.WordStartPosition(c.Pos, false);
            var length = c.Pos - start;
            var word = editor.GetTextRange(start, length);
            
            // is not a keyword
            if (!keywords[(int)State.Qualifier][word].Any(x => x.word == word))
            {
                editor.StartStyling(start);
                editor.SetStyling(length, StateToStyle((int)State.Default));
                editor.StartStyling(c.Pos);
            }

            return ProcessDefaultState(editor, c);
        }
        #endregion

        #region STATE
        private enum State : int { Default, Qualifier }

        public override Type StateType => typeof(State);
        #endregion
    }

    class GlslStructLexer : GlslStructFuncHeaderLexer
    {
        public GlslStructLexer(int nextStyle, XmlNode xml, BaseLexer parentLexer)
            : base(nextStyle, xml, parentLexer, '}') { }
    }

    class GlslFunctionHeaderLexer : GlslStructFuncHeaderLexer
    {
        public GlslFunctionHeaderLexer(int nextStyle, XmlNode xml, BaseLexer parentLexer)
            : base(nextStyle, xml, parentLexer, ')') { }
    }

    class GlslStructFuncHeaderLexer : BaseLexer
    {
        private char closingBrace;

        public GlslStructFuncHeaderLexer(int nextStyle, XmlNode xml, BaseLexer parentLexer, char closingBrace)
            : base(nextStyle, xml, parentLexer)
        {
            this.closingBrace = closingBrace;
        }

        #region METHODS
        public override int ProcessState(CodeEditor editor, int state, Region c, int endPos)
        {
            switch (state)
            {
                case (int)BaseState.Braces:
                    return ProcessBraceState(editor, c);
                case (int)State.DataType:
                    return ProcessDataTypeState(editor, c);
                case (int)BaseState.Number:
                    return StateHelper.ProcessNumberState(this, editor, c);
            }
            return ProcessDefaultState(editor, c);
        }

        public override int ProcessDefaultState(CodeEditor editor, Region c)
        {
            switch (c.c)
            {
                case ',': case ';':
                    // a punctuation
                    return (int)BaseState.Punctuation;
                case '(': case ')':
                case '[': case ']':
                case '{': case '}':
                    // a brace
                    return (int)BaseState.Braces;
            }

            // the beginning of a number
            if (char.IsDigit(c.c))
                return (int)BaseState.Number;

            // the beginning of a data type keyword
            if (char.IsLetter(c.c))
                return (int)State.DataType;

            return (int)State.Default;
        }
        
        protected int ProcessBraceState(CodeEditor editor, Region c)
            => c[-1] == closingBrace ? -1 : ProcessDefaultState(editor, c);

        private int ProcessDataTypeState(CodeEditor editor, Region c)
        {
            // position still inside the word range
            if (char.IsLetterOrDigit(c.c))
                return (int)State.DataType;

            // get previous word
            var start = editor.WordStartPosition(c.Pos, false);
            var length = c.Pos - start;
            var word = editor.GetTextRange(start, length);

            // is not a keyword
            if (!keywords[(int)State.DataType][word].Any(x => x.word == word))
            {
                editor.StartStyling(start);
                editor.SetStyling(length, StateToStyle((int)State.Default));
                editor.StartStyling(c.Pos);
            }

            return ProcessDefaultState(editor, c);
        }
        #endregion

        #region STATE
        private enum State : int { Default, DataType }

        public override Type StateType => typeof(State);
        #endregion
    }

    class GlslFunctionLexer : BaseLexer
    {
        public GlslFunctionLexer(int nextStyle, XmlNode xml, BaseLexer parentLexer)
            : base(nextStyle, xml, parentLexer) { }

        #region METHODS
        public override int ProcessState(CodeEditor editor, int state, Region c, int endPos)
        {
            switch (state)
            {
                case (int)BaseState.Number:
                    return StateHelper.ProcessNumberState(this, editor, c);
                case (int)BaseState.String:
                    return StateHelper.ProcessStringState(this, editor, c);
                case (int)BaseState.Braces:
                    return ProcessBraceState(editor, c);
                case (int)BaseState.LineComment:
                    return StateHelper.ProcessLineCommentState(this, editor, c);
                case (int)BaseState.BlockComment:
                    return StateHelper.ProcessBlockCommentState(this, editor, c);
                case (int)State.Keyword:
                    return ProcessKeywordState(editor, c);
            }
            return ProcessDefaultState(editor, c);
        }

        public override int ProcessDefaultState(CodeEditor editor, Region c)
        {
            switch (c.c)
            {
                case '"': case '\'':
                    // the beginning of a string
                    return (int)BaseState.String;

                case '.':
                    // the beginning of a number or punctuation
                    return (int)(char.IsNumber(c[1]) ? BaseState.Number : BaseState.Punctuation);
                case ',':
                case ';':
                    // a punctuation
                    return (int)BaseState.Punctuation;

                case '*':
                case '+':
                case '-':
                    // an operator
                    return (int)BaseState.Operator;
                case '/':
                    // the beginning of a line comment
                    if (c[1] == '/') return (int)BaseState.LineComment;
                    // the beginning of a block comment
                    if (c[1] == '*') return (int)BaseState.BlockComment;
                    // an operator
                    return (int)BaseState.Operator;

                case '(': case ')':
                case '[': case ']':
                case '{': case '}':
                    // a brace
                    return (int)BaseState.Braces;
            }

            // the beginning of a number
            if (char.IsNumber(c.c))
                return (int)BaseState.Number;
            
            // an operator
            if (char.IsSymbol(c.c))
                return (int)BaseState.Operator;

            // the beginning of a keyword
            if (char.IsLetter(c.c))
                return (int)State.Keyword;

            return (int)State.Default;
        }

        private int ProcessBraceState(CodeEditor editor, Region c)
        {
            // is a closing brace
            if (c[-1] == '}')
            {
                int i, count, style;

                // find matching brace position
                for (i = c.Pos - 1, count = 0; i >= 0; i--)
                {
                    switch (editor.GetCharAt(i))
                    {
                        case '{': count--; break;
                        case '}': count++; break;
                    }
                    if (count == 0) break;
                }

                // if no matching brace found, exit
                if (count != 0 || i < 0)
                    return -1;

                // go to first non base state before the matching brace position
                for (style = editor.GetStyleAt(i);
                    i >= 0 && firstBaseStyle <= style && style <= lastBaseStyle;
                    style = editor.GetStyleAt(--i));

                // if no function lexer state found, exit
                if (i < 0 || style < firstStyle || lastStyle < style)
                    return -1;
            }

            return ProcessDefaultState(editor, c);
        }


        private int ProcessKeywordState(CodeEditor editor, Region c)
        {
            // position still inside the word range
            if (char.IsLetterOrDigit(c.c) || c.c == '_')
                return (int)State.Keyword;

            // get previous word
            var start = editor.WordStartPosition(c.Pos, false);
            var length = c.Pos - start;
            var word = editor.GetTextRange(start, length);

            // is data type keyword
            if (keywords[(int)State.DataType][word].Any(x => x.word == word))
            {
                editor.StartStyling(start);
                editor.SetStyling(length, StateToStyle((int)State.DataType));
                editor.StartStyling(c.Pos);
            }
            // is a function
            else if (keywords[(int)State.Function][word].Any(x => x.word == word))
            {
                editor.StartStyling(start);
                editor.SetStyling(length, StateToStyle((int)State.Function));
                editor.StartStyling(c.Pos);
            }
            // is not a keyword
            else if (!keywords[(int)State.Keyword][word].Any(x => x.word == word))
            {
                editor.StartStyling(start);
                editor.SetStyling(length, StateToStyle((int)State.Default));
                editor.StartStyling(c.Pos);
            }

            return ProcessDefaultState(editor, c);
        }
        #endregion

        #region STATE
        private enum State : int { Default, Keyword, DataType, Function }

        public override Type StateType => typeof(State);
        #endregion
    }

    #region STRUCTURES
    enum BaseState
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

    enum BaseStyle
    {
        String = BaseState.String - BaseState.Default,
        Braces = BaseState.Braces - BaseState.Default,
    }

    public struct Keyword
    {
        public string word;
        public string hint;
    }

    public struct Style
    {
        public int id;
        public Color fore;
        public Color back;
    }

    public static class StateHelper
    {
        public static int ProcessStringState<T>(T caller, CodeEditor editor, Region c) where T : BaseLexer
            => (c.c == '\n' || (c[-1] == '"' && c[-2] != '\\' && c.GetStyleAt(-2) == (int)BaseStyle.String))
                ? caller.ProcessDefaultState(editor, c)
                : (int)BaseState.String;

        public static int ProcessNumberState<T>(T caller, CodeEditor editor, Region c) where T : BaseLexer
            => char.IsNumber(c.c)
                || c.c == '.' || c.c == 'x'
                || ('a' <= c.c && c.c <= 'f')
                || ('A' <= c.c && c.c <= 'F')
                ? (int)BaseState.Number
                : caller.ProcessDefaultState(editor, c);

        public static int ProcessLineCommentState<T>(T caller, CodeEditor editor, Region c) where T : BaseLexer
            => c.c == '\n' ? caller.ProcessDefaultState(editor, c) : (int)BaseState.LineComment;

        public static int ProcessBlockCommentState<T>(T caller, CodeEditor editor, Region c) where T : BaseLexer
            => c[-2] == '*' && c[-1] == '/' ? caller.ProcessDefaultState(editor, c) : (int)BaseState.BlockComment;

        public static int DefaultProcessNewLine(CodeEditor editor, Region c)
        {
            // exit lexer if line does not end with "."
            int i = c.Pos - 1;
            var C = (char)editor.GetCharAt(i);
            while (i > 0 && C != '\n' && char.IsWhiteSpace(C))
                C = (char)editor.GetCharAt(--i);
            return editor.GetCharAt(i) != '.' ? -1 : (int)BaseState.Default;
        }
    }

    public class Region
    {
        private CodeEditor editor;
        private int pos;
        public char this[int i] => GetChar(pos + i);
        public char c { get; private set; }
        public int Pos
        {
            get
            {
                return pos;
            }
            set
            {
                c = (char)editor.GetCharAt(pos = value);
            }
        }

        public Region(CodeEditor editor, int pos)
        {
            this.editor = editor;
            Pos = pos;
        }

        public int GetStyleAt(int i) => editor.GetStyleAt(pos + i);

        private char GetChar(int i)
            => (char)(0 <= i && i < editor.TextLength ? editor.GetCharAt(i) : 0);
    }

    public static class XmlNodeExtensions
    {
        public static string GetAttributeValue(this XmlNode node, string name)
            => node.Attributes.GetNamedItem(name)?.Value;
    }
    #endregion
}
