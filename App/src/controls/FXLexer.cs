using ScintillaNET.Lexing;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;
using SciStyle = ScintillaNET.Style;

namespace ScintillaNET
{
    /// <summary>
    /// ProtoFX tech file lexer.
    /// </summary>
    public class FxLexer : BaseLexer
    {
        public FxLexer(string file) : base(0, LoadFxLexerFromXml(file), null) { }

        public override int Style(CodeEditor editor, int pos, int endPos)
        {
            BaseLexer lex = null;
            int end;

            // Find a lexer that can continue lexing.
            // Use the previous state to select the right lexer.
            // As long as no lexer can be found, move the start position backward.
            for (end = Math.Min(pos + 1, endPos); pos >= 0; pos--)
                if ((lex = FindLexerForStyle(editor.GetStyleAt(pos))) != null)
                    break;
            if (lex == null)
                lex = lexer.First();

            // Because there might be changes to the preceding characters of
            // that state, we go to the beginning of that (state) region and ...
            for (var style = editor.GetStyleAt(pos); pos >= 0; pos--)
                if (editor.GetStyleAt(pos) != style)
                    break;

            // ... to the end of that (state) region and ...
            for (var style = editor.GetStyleAt(end); end < endPos; end++)
                if (editor.GetStyleAt(end) != style)
                    break;

            // ... invalidate it. This is necessary,
            // because of changes within indicators.
            editor.StartStyling(++pos);
            editor.SetStyling(end - pos, 0);

            // start lexing from the newly determined start position
            while (pos < endPos && lex != null)
            {
                pos = lex.Style(editor, pos, endPos);
                lex = lex.ParentLexer;
            }
            return pos;
        }

        private new BaseLexer FindLexerForStyle(int style)
        {
            return lexer.Select(x => x.FindLexerForStyle(style)).FirstOr(x => x != null, null);
        }

        private static XmlNode LoadFxLexerFromXml(string file)
        {
            var xml = new XmlDocument();
            if (System.IO.File.Exists(file))
                xml.Load(file);
            else
                xml.LoadXml(file);
            return xml.SelectSingleNode("FxLexer");
        }

        protected override Type StateType => typeof(BaseState);
    }

    /// <summary>
    /// Base lexer encapsulating commonly
    /// used method for sub-lexers.
    /// </summary>
    public abstract class BaseLexer : ILexer
    {
        #region FIELDS

        protected string lexerType;
        protected BaseLexer[] lexer;
        protected Trie<Keyword>[] keywords;
        protected Lexing.Style[] styles;
        public IEnumerable<Lexing.Style> Styles => styles.Concat(lexer.SelectMany(x => x.Styles));
        public int firstStyle { get; private set; }
        public int lastStyle { get; private set; }
        public static int firstBaseStyle { get; private set; }
        public static int lastBaseStyle { get; private set; }
        protected virtual Type StateType => null;
        public int MaxStyle => Math.Max(lastStyle, lexer.Select(x => x.MaxStyle).MaxOr(0));
        public BaseLexer ParentLexer;
        protected BaseLexer DefaultLexer;
        private static Regex regexHint = new Regex(@"\n.*\\");

        #endregion

        public BaseLexer(int firstFreeStyle, XmlNode lexerNode, BaseLexer parent)
        {
            ParentLexer = parent;
            DefaultLexer = GetType().Name == "DefaultLexer" ? null
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
                .Select(x => new Lexing.Style {
                    id = x, fore = Theme.ForeColor, back = Theme.Workspace
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
                if (style.HasAttributeValue("theme") && style.GetAttributeValue("theme").ToLower() != Theme.Name.ToLower())
                    continue;
                var id = (int)Enum.Parse(StateType, style.GetAttributeValue("name"), true);
                var idx = id - firstState;
                if (style.HasAttributeValue("fore"))
                    styles[idx].fore = ColorTranslator.FromHtml(style.GetAttributeValue("fore"));
                if (style.HasAttributeValue("back"))
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
            var assembly = typeof(BaseLexer).FullName.Substring(0,
                typeof(BaseLexer).FullName.Length - typeof(BaseLexer).Name.Length);
            for (int i = 0; i < lexerList.Count; i++)
            {
                var type = lexerList[i].GetAttributeValue("lexer");
                var param = new object[] { firstFreeStyle, lexerList[i], this };
                var t = Type.GetType($"{assembly}{type}");
                lexer[i] = (BaseLexer)Activator.CreateInstance(t, param);
                firstFreeStyle = lexer[i].MaxStyle + 1;
            }
        }

        #region ILexer

        public virtual int Style(CodeEditor editor, int pos, int endPos)
        {
            editor.StartStyling(pos);
            endPos = Math.Min(endPos, editor.TextLength);

            // instantiate region class
            var c = new Region(editor, pos);

            // continue processing from the last state
            for (int oldLeftState = GetPrevState(c),
                     oldLeftStyle = c.GetStyleAt(-1);
                // while
                c.Pos < endPos;
                c.Pos++)
            {
                // process current state
                var state = ProcessState(editor, oldLeftState, c, endPos);
                if (state < 0)
                    return c.Pos;
                if (c.Pos >= endPos)
                    break;

                var leftStyle = c.GetStyleAt(-1);
                var oldStyle = c.GetStyleAt(0);
                var style = StateToStyle(state);

                // if the old style was reproduced
                if (style == oldStyle)
                    break;
                // style the current character
                else
                    editor.SetStyling(1, style);

                oldLeftStyle = oldStyle;
                oldLeftState = state;
            }

            return endPos;
        }
        
        public void Fold(CodeEditor editor, int pos, int endPos)
        {
            // setup state machine
            var line = editor.LineFromPosition(pos);
            var lastLine = -1;
            var lastCharPos = line;
            var foldLevel = editor.Lines[line].FoldLevel;
            var textLength = editor.TextLength;
            const int DEFAULT_FOLD_LEVEL = 1024;

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
                        foldLevel = Math.Max(--foldLevel, DEFAULT_FOLD_LEVEL);
                        lastLine = line;
                        // switch to folding state (which will
                        // switch to unknown state if the most
                        // outer fold level is reached)
                        state = FoldState.Foldable;
                        break;
                    // STATE: folding
                    case FoldState.Foldable:
                        // still in folding state
                        if (foldLevel > DEFAULT_FOLD_LEVEL)
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
            // style not handled by this lexer
            if (style < firstStyle || lastStyle < style)
                return lexer.SelectMany(x => x.SelectKeywordInfo(style, word));
            
            // style is default style which has no keywords
            if (style == firstStyle && keywords[0] == null)
                // get a list of all words for all styles
                return keywords.Where(x => x != null).SelectMany(x => x[word]);

            // get keywords for the specified style
            int idx = style - firstStyle;
            return keywords[idx] != null ? keywords[idx][word] : Enumerable.Empty<Keyword>();
        }

        #endregion

        #region METHODS

        /// <summary>
        /// Default process state method.
        /// </summary>
        /// <param name="e">unused</param>
        /// <param name="state"></param>
        /// <param name="c">unused</param>
        /// <param name="p">unused</param>
        /// <returns>The new state after processing the current state.</returns>
        public virtual int ProcessState(CodeEditor e, int state, Region c, int p)
        {
            return state;
        }

        /// <summary>
        /// Default process default state method.
        /// </summary>
        /// <param name="e">unused</param>
        /// <param name="c">unused</param>
        /// <returns>The new state after processing the current state.</returns>
        public virtual int ProcessDefaultState(CodeEditor e, Region c)
        {
            return 0;
        }

        /// <summary>
        /// Default process string state method.
        /// </summary>
        /// <param name="editor"></param>
        /// <param name="c"></param>
        /// <returns>The new state after processing the current state.</returns>
        protected int ProcessStringState(CodeEditor editor, Region c)
        {
            return (c.c == '\n' || (c[-1] == '"' && c[-2] != '\\' && c.GetStyleAt(-2) == StringStyle))
                           ? ProcessDefaultState(editor, c)
                           : (int)BaseState.String;
        }

        /// <summary>
        /// Default process number state method.
        /// </summary>
        /// <param name="editor"></param>
        /// <param name="c"></param>
        /// <returns>The new state after processing the current state.</returns>
        protected int ProcessNumberState(CodeEditor editor, Region c)
        {
            return char.IsNumber(c.c)
                           || c.c == '.' || c.c == 'x'
                           || ('a' <= c.c && c.c <= 'f')
                           || ('A' <= c.c && c.c <= 'F')
                           ? (int)BaseState.Number
                           : ProcessDefaultState(editor, c);
        }

        /// <summary>
        /// Default process line comment state.
        /// </summary>
        /// <param name="editor"></param>
        /// <param name="c"></param>
        /// <returns>The new state after processing the current state.</returns>
        protected int ProcessLineCommentState(CodeEditor editor, Region c)
        {
            return c.c == '\n' ? ProcessDefaultState(editor, c) : (int)BaseState.LineComment;
        }

        /// <summary>
        /// Default process block comment state.
        /// </summary>
        /// <param name="editor"></param>
        /// <param name="c"></param>
        /// <returns>The new state after processing the current state.</returns>
        protected int ProcessBlockCommentState(CodeEditor editor, Region c)
        {
            return c[-2] == '*' && c[-1] == '/' ? ProcessDefaultState(editor, c) : (int)BaseState.BlockComment;
        }

        /// <summary>
        /// Default process indicator state.
        /// </summary>
        /// <param name="editor"></param>
        /// <param name="c"></param>
        /// <param name="IndicatorState"></param>
        /// <returns>The new state after processing the current state.</returns>
        protected int ProcessIndicatorState(CodeEditor editor, Region c, int IndicatorState)
        {
            // position still inside the word range
            if (char.IsLetterOrDigit(c.c) || c.c == '_')
                return IndicatorState;

            // relex last word
            RelexPreviousWord(editor, c.Pos);

            // continue from default state
            return ProcessDefaultState(editor, c);
        }

        /// <summary>
        /// Default process new line state.
        /// </summary>
        /// <param name="editor"></param>
        /// <param name="c"></param>
        /// <returns>The new state after processing the current state.</returns>
        protected int DefaultProcessNewLine(CodeEditor editor, Region c)
        {
            // exit lexer if line does not end with "..."
            int i = c.Pos - 1;
            var C = editor.GetCharAt(i);
            while (i > 0 && C != '\n' && char.IsWhiteSpace((char)C))
                C = editor.GetCharAt(--i);
            return editor.GetTextRange(i - 2, 3) != "..." ? -1 : (int)BaseState.Default;
        }

        /// <summary>
        /// Relex the first word directly before the current position.
        /// </summary>
        /// <param name="editor"></param>
        /// <param name="pos"></param>
        /// <returns>Returns <code>true</code> if the previous word changed its style,
        /// otherwise <code>false</code> if the style did not change.</returns>
        protected void RelexPreviousWord(CodeEditor editor, int pos)
        {
            // get previous word and start position
            var start = editor.SyncFunc(x => x.WordStartPosition(pos, false));
            var word = editor.SyncFunc(x => x.GetWordFromPosition(start));

            // find out if the previous word is a keyword
            for (int i = 0; i < keywords.Length; i++)
            {
                // reset the style of the word if it is a keyword
                if (keywords[i]?[word].Any(x => x.word == word) ?? false)
                {
                    editor.StartStyling(start);
                    editor.SetStyling(pos - start, StateToStyle(i));
                    break;
                }
            }
        }

        /// <summary>
        /// Check if this lexer or a sub-lexer can handle
        /// the specified style (which indicates a state).
        /// </summary>
        /// <param name="style"></param>
        /// <returns>A lexer that can handle the style or <code>null</code></returns>
        public BaseLexer FindLexerForStyle(int style)
        {
            return firstStyle <= style && style <= lastStyle
                ? this
                : lexer.Select(x => x.FindLexerForStyle(style)).FirstOrDefault(x => x != null);
        }

        /// <summary>
        /// Convert lexer state to Scintilla style.
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        protected int StateToStyle(int state)
        {
            return state + (state < FirstBaseState ? firstStyle : -FirstBaseState);
        }

        /// <summary>
        /// Is this lexer designated for the specified type (of a tech object).
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public virtual bool IsLexerForType(string type)
        {
            return lexerType == type;
        }

        #endregion

        #region HELPER METHODS

        /// <summary>
        /// Find last position in the text that has the specified style.
        /// </summary>
        /// <param name="editor"></param>
        /// <param name="style"></param>
        /// <param name="from"></param>
        /// <returns></returns>
        protected int FindLastStyleOf(CodeEditor editor, int style, int from)
        {
            while (from > 0 && editor.GetStyleAt(from) != style)
                from--;
            return editor.GetStyleAt(from) == style ? from : -1;
        }

        /// <summary>
        /// Get the state directly before the specified position.
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
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

        protected static bool IsHelperState(int state)
        {
            return DefaultState <= state && state <= IndicatorState;
        }

        protected static bool IsKeywordState(int state)
        {
            return IndicatorState < state && state < FirstBaseState;
        }

        protected int FindClosingBrace(CodeEditor editor, int openIndex, char open, char close)
        {
            // go to matching brace
            int i = openIndex + (editor.GetCharAt(openIndex) == open ? 1 : 0);
            int textLen = editor.TextLength;
            for (int n = 0; i < textLen && n >= 0; i++)
            {
                if (editor.SyncFunc(x => x.GetCharAt(i) == open)) n++;
                if (editor.SyncFunc(x => x.GetCharAt(i) == close)) n--;
            }
            return i;
        }

        protected int FindClosingBrace(CodeEditor editor, int openIndex, string open, char close)
        {
            // go to matching brace
            int i = openIndex + (editor.GetTextRange(openIndex, 3) == open ? 3 : 2);
            int textLen = editor.TextLength;
            for (int n = 0; i < textLen && n >= 0; i++)
            {
                if (editor.GetTextRange(i - 2, 3) == open) n++;
                if (editor.GetCharAt(i) == close) n--;
            }
            return i;
        }

        #endregion

        #region STATE

        protected static int DefaultState = 0;
        protected static int IndicatorState = 1;
        protected const int FirstBaseState = (int)BaseState.Invalid;
        protected const int StringStyle = BaseState.String - BaseState.Invalid;
        protected const int BraceStyle = BaseState.Braces - BaseState.Invalid;

        private enum FoldState : int
        {
            Unknown,
            StartFolding,
            Foldable,
            EndFolding,
        }

        #endregion
    }

    #region PROTOFX LEXERS

    /// <summary>
    /// Lexer for the tech-objects in the code.
    /// </summary>
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
                    return ProcessLineCommentState(editor, c);
                case (int)BaseState.BlockComment:
                    return ProcessBlockCommentState(editor, c);
                case (int)State.Preprocessor:
                    return ProcessPreprocessorState(editor, c);
                case (int)State.PreprocessorBody:
                    return ProcessPreprocessorBodyState(editor, c);
                case (int)State.Indicator:
                    return ProcessIndicatorState(editor, c, (int)State.Indicator);
                case (int)BaseState.Braces:
                    return ProcessBraceState(editor, c, endPos);
            }
            return ProcessDefaultState(editor, c);
        }

        public override int ProcessDefaultState(CodeEditor editor, Region c)
        {
            switch (c.c)
            {
                case '/':
                    if (c[1] == '/') return (int)BaseState.LineComment;
                    if (c[1] == '*') return (int)BaseState.BlockComment;
                    break;
                case '#': return (int)State.Preprocessor;
                case '{': return (int)BaseState.Braces;
                case '_': return (int)State.Indicator;
            }

            if (char.IsLetter(c.c))
                return (int)State.Indicator;

            return (int)State.Default;
        }
        
        private int ProcessBraceState(CodeEditor editor, Region c, int endPos)
        {
            if (c[-1] == '{')
            {
                // get header string from block position
                var typePos = FindLastStyleOf(editor, StateToStyle((int)State.Type), c.Pos);
                var typeName = typePos >= 0 ? editor.SyncFunc(x => x.GetWordFromPosition(typePos)) : null;

                // find lexer that can lex this code block
                var lex = lexer.Where(x => x.IsLexerForType(typeName)).FirstOr(null);

                // go to matching brace
                int newEndPos = FindClosingBrace(editor, c.Pos, '{', '}');

                // if no lexer found, skip this code block
                if (lex == null)
                {
                    // make code block invalid
                    editor.SetStyling(newEndPos - c.Pos, StateToStyle((int)BaseState.Default));
                    // go to matching brace
                    c.Pos = newEndPos;
                    return (int)(c.c == '}' ? BaseState.Braces : BaseState.Default);
                }

                // lex code block
                c.Pos = lex.Style(editor, c.Pos, newEndPos);
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
            var start = FindLastStyleOf(editor, StateToStyle((int)State.Preprocessor), c.Pos);
            // re-lex code block
            DefaultLexer.Style(editor, start + 1, c.Pos);
            // continue styling from the last position
            editor.StartStyling(c.Pos);
            return (int)State.Default;
        }

        #endregion

        #region STATE
        private enum State : int { Default, Indicator, Preprocessor, PreprocessorBody, Type, Anno }

        protected override Type StateType => typeof(State);
        #endregion
    }

    /// <summary>
    /// Lexer for default tech-object bodies.
    /// </summary>
    class TechBodyLexer : BaseLexer
    {
        public TechBodyLexer(int nextStyle, XmlNode xml, BaseLexer parentLexer)
            : base(nextStyle, xml, parentLexer) { }

        #region PROCESS STATE

        public override int ProcessState(CodeEditor editor, int state, Region c, int endPos)
        {
            switch (state)
            {
                case (int)BaseState.Braces:
                    return -1; // exit lexer
                case (int)BaseState.LineComment:
                    return ProcessLineCommentState(editor, c);
                case (int)BaseState.BlockComment:
                    return ProcessBlockCommentState(editor, c);
                case (int)State.Indicator:
                    return ProcessIndicatorState(editor, c, endPos);
            }
            return ProcessDefaultState(editor, c);
        }

        public override int ProcessDefaultState(CodeEditor editor, Region c)
        {
            switch (c.c)
            {
                case '/':
                    if (c[1] == '/') return (int)BaseState.LineComment;
                    if (c[1] == '*') return (int)BaseState.BlockComment;
                    break;
                case '_': return (int)State.Indicator;
                case '.': return (int)BaseState.Punctuation;
                case '}': return (int)BaseState.Braces; // end of the block
            }

            // the beginning of a word or keyword
            if (char.IsLetter(c.c))
                return (int)State.Indicator;
            
            return (int)State.Default;
        }

        private new int ProcessIndicatorState(CodeEditor editor, Region c, int endPos)
        {
            // is still part of the command
            if (!char.IsWhiteSpace(c.c))
                return (int)State.Indicator;

            // relex last word
            RelexPreviousWord(editor, c.Pos);

            // get header string from block position
            var cmd = editor.GetWordFromPosition(c.Pos - 1);

            // find lexer that can lex this code block
            var lex = lexer.Where(x => x.IsLexerForType(cmd)).FirstOr(DefaultLexer);

            // go to matching new line
            int newEndPos = FindClosingBrace(editor, c.Pos, "...", '\n');

            // re-lex code block
            c.Pos = lex.Style(editor, c.Pos, newEndPos);

            // continue lexing
            return ProcessDefaultState(editor, c);
        }

        #endregion

        #region STATE

        private enum State : int { Default, Indicator, Command }

        protected override Type StateType => typeof(State);

        #endregion
    }

    /// <summary>
    /// Lexer for the tech-object-body command line.
    /// </summary>
    class CommandLexer : BaseLexer
    {
        public CommandLexer(int nextStyle, XmlNode xml, BaseLexer parentLexer)
            : base(nextStyle, xml, parentLexer) { }

        #region PROCESS STATE

        public override int ProcessState(CodeEditor editor, int state, Region c, int p)
        {
            switch (state)
            {
                case (int)State.Indicator:
                    return ProcessIndicatorState(editor, c, (int)State.Indicator);
                case (int)BaseState.Number:
                    return ProcessNumberState(editor, c);
                case (int)BaseState.String:
                    return ProcessStringState(editor, c);
            }
            return ProcessDefaultState(editor, c);
        }

        public override int ProcessDefaultState(CodeEditor editor, Region c)
        {
            switch (c.c)
            {
                case  '_': return (int)State.Indicator;
                case  '"': return (int)BaseState.String;
                case  '.': return (int)(char.IsNumber(c[1]) ? BaseState.Number : BaseState.Punctuation);
                case '\n': return DefaultProcessNewLine(editor, c);
            }

            // the beginning of a word or keyword
            if (char.IsLetter(c.c))
                return (int)State.Indicator;

            // start of a number
            if(char.IsNumber(c.c))
                return (int)BaseState.Number;

            return (int)State.Default;
        }

        #endregion

        #region STATE

        private enum State : int { Default, Indicator, Argument }

        protected override Type StateType => typeof(State);

        #endregion
    }

    /// <summary>
    /// Default lexer used to lex parts of the code
    /// that can be strings, numbers and comments.
    /// </summary>
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
                    return ProcessNumberState(editor, c);
                case (int)BaseState.String:
                    return ProcessStringState(editor, c);
                case (int)BaseState.LineComment:
                    return ProcessLineCommentState(editor, c);
                case (int)BaseState.BlockComment:
                    return ProcessBlockCommentState(editor, c);
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
                case ',': case ';':
                    // a punctuation
                    return (int)BaseState.Punctuation;

                case '*': case '+': case '-':
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
                    return DefaultProcessNewLine(editor, c);
            }

            // the beginning of an operator
            if (char.IsSymbol(c.c))
                return (int)BaseState.Operator;

            // the beginning of a number
            if (char.IsNumber(c.c) && !char.IsLetter(c[-1]) && c[-1] != '_')
                return (int)BaseState.Number;
            
            return (int)BaseState.Default;
        }

        #endregion

        #region STATE

        private enum State : int { }

        protected override Type StateType => typeof(State);

        #endregion
    }

    #endregion

    #region GLSL LEXERS

    /// <summary>
    /// Lexer for GLSL-tech-object-body.
    /// </summary>
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
                    return ProcessLineCommentState(editor, c);
                case (int)BaseState.BlockComment:
                    return ProcessBlockCommentState(editor, c);
                case (int)State.Preprocessor:
                    return ProcessPreprocessorState(editor, c, endPos);
                case (int)State.Indicator:
                    return ProcessIndicatorState(editor, c, (int)State.Indicator);
            }
            return ProcessDefaultState(editor, c);
        }

        public override int ProcessDefaultState(CodeEditor editor, Region c)
        {
            switch (c.c)
            {
                case '#': return (int)State.Preprocessor;
                case '_': return (int)State.Indicator;
                case '(':
                case ')':
                case '{':
                case '}':  return (int)BaseState.Braces;
                case '.':
                case ';': return (int)BaseState.Punctuation;
                case '=': return (int)BaseState.Operator;
                case '/':
                    if (c[1] == '/') return (int)BaseState.LineComment;
                    if (c[1] == '*') return (int)BaseState.BlockComment;
                    break;
            }

            // the beginning of a keyword
            if (char.IsLetter(c.c))
                return (int)State.Indicator;

            return (int)State.Default;
        }

        private int ProcessPreprocessorState(CodeEditor editor, Region c, int endPos)
        {
            if (c.c == '\n')
                return (int)State.Default;
            if (char.IsWhiteSpace(c.c))
            {
                var newEndPos = editor.SyncFunc(x => x.Text.IndexOf('\n', c.Pos, endPos - c.Pos));
                c.Pos = DefaultLexer.Style(editor, c.Pos, newEndPos < 0 ? endPos : newEndPos);
                return ProcessDefaultState(editor, c);
            }
            return (int)State.Preprocessor;
        }

        private int ProcessBracesState(CodeEditor editor, Region c, int endPos)
        {
            // get the lexer type of the body
            string type = null;
            int newEndPos = endPos;
            switch (c[-1])
            {
                case '(':
                    // get preceding word from brace position
                    var wordStart = editor.WordStartPosition(c.Pos - 1, false);
                    type = editor.GetWordFromPosition(wordStart);
                    newEndPos = FindClosingBrace(editor, c.Pos, '(', ')');

                    if (type != "layout")
                        type = "functionheader";
                    break;
                case '{':
                    // get preceding non-whitespace position from brace position
                    var bracePos = c.Pos - 2;
                    while (bracePos >= 0
                        && editor.GetCharAt(bracePos) != ';'
                        && char.IsWhiteSpace((char)editor.GetCharAt(bracePos)))
                        bracePos--;

                    // if the preceding char is a closing brace, we have to lex a function body
                    var braceChar = editor.GetCharAt(bracePos);
                    type = braceChar == ')' ? "function" : "struct";
                    newEndPos = FindClosingBrace(editor, c.Pos, '{', '}');
                    break;
                default:
                    return ProcessDefaultState(editor, c);
            }

            // find lexer that can lex this code block
            var lex = lexer.Where(x => x.IsLexerForType(type)).FirstOr(null);

            // re-lex body of the uniform block, struct, layout or function
            c.Pos = lex?.Style(editor, c.Pos, newEndPos) ?? c.Pos;

            // continue styling from the last position
            editor.StartStyling(c.Pos);
            return ProcessDefaultState(editor, c);
        }

        #endregion

        #region STATE

        private enum State : int { Default, Indicator, Keyword, DataType, Preprocessor }

        protected override Type StateType => typeof(State);

        #endregion
    }

    /// <summary>
    /// Lexer for GLSL-layout-body (layout qualifiers).
    /// </summary>
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
                    return -1; // exit lexer
                case (int)State.Indicator:
                    return ProcessIndicatorState(editor, c, (int)State.Indicator);
                case (int)BaseState.Number:
                    return ProcessNumberState(editor, c);
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
                return (int)State.Indicator;

            return (int)State.Default;
        }

        #endregion

        #region STATE

        private enum State : int { Default, Indicator, Qualifier }

        protected override Type StateType => typeof(State);

        #endregion
    }

    /// <summary>
    /// Lexer for GLSL uniform blocks and structures.
    /// </summary>
    class GlslStructLexer : GlslStructFuncArgLexer
    {
        public GlslStructLexer(int nextStyle, XmlNode xml, BaseLexer parentLexer)
            : base(nextStyle, xml, parentLexer, '}') { }
    }

    /// <summary>
    /// Lexer for GLSL function arguments.
    /// </summary>
    class GlslFunctionArgLexer : GlslStructFuncArgLexer
    {
        public GlslFunctionArgLexer(int nextStyle, XmlNode xml, BaseLexer parentLexer)
            : base(nextStyle, xml, parentLexer, ')') { }
    }

    /// <summary>
    /// Lexer for GLSL uniform blocks,
    /// structures and function arguments.
    /// </summary>
    class GlslStructFuncArgLexer : BaseLexer
    {
        private char closingBrace;

        public GlslStructFuncArgLexer(
            int nextStyle, XmlNode xml, BaseLexer parentLexer, char closingBrace)
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
                    return ProcessBraceState(editor, c, endPos);
                case (int)State.Indicator:
                    return ProcessIndicatorState(editor, c, (int)State.Indicator);
                case (int)BaseState.Number:
                    return ProcessNumberState(editor, c);
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
                case '_':
                    // a brace
                    return (int)State.Indicator;
            }

            // the beginning of a number
            if (char.IsDigit(c.c))
                return (int)BaseState.Number;

            // the beginning of a data type keyword
            if (char.IsLetter(c.c))
                return (int)State.Indicator;

            return (int)State.Default;
        }

        protected int ProcessBraceState(CodeEditor editor, Region c, int endPos)
        {
            if (c[-1] == closingBrace)
                return -1;

            // get the lexer type of the body
            if (c[-1] == '(')
            {
                // get preceding word from brace position
                var wordStart = editor.WordStartPosition(c.Pos - 1, false);
                var type = editor.GetWordFromPosition(wordStart);

                if (type == "layout")
                {
                    // find lexer that can lex this code block
                    var lex = lexer.Where(x => x.IsLexerForType(type)).FirstOr(null);

                    // re-lex body of the uniform block, struct, layout or function
                    c.Pos = lex?.Style(editor, c.Pos, endPos) ?? c.Pos;

                    // continue styling from the last position
                    editor.StartStyling(c.Pos);
                }
            }

            return ProcessDefaultState(editor, c);
        }

        #endregion

        #region STATE

        private enum State : int { Default, Indicator, Keyword, DataType }

        protected override Type StateType => typeof(State);

        #endregion
    }

    /// <summary>
    /// Lexer for GLSL-function-bodies.
    /// </summary>
    class GlslFunctionBodyLexer : BaseLexer
    {
        public GlslFunctionBodyLexer(int nextStyle, XmlNode xml, BaseLexer parentLexer)
            : base(nextStyle, xml, parentLexer) { }

        #region METHODS

        public override int ProcessState(CodeEditor editor, int state, Region c, int endPos)
        {
            switch (state)
            {
                case (int)BaseState.Number:
                    return ProcessNumberState(editor, c);
                case (int)BaseState.String:
                    return ProcessStringState(editor, c);
                case (int)BaseState.Braces:
                    return ProcessBraceState(editor, c);
                case (int)BaseState.LineComment:
                    return ProcessLineCommentState(editor, c);
                case (int)BaseState.BlockComment:
                    return ProcessBlockCommentState(editor, c);
                case (int)State.Indicator:
                    return ProcessIndicatorState(editor, c, (int)State.Indicator);
            }
            return ProcessDefaultState(editor, c);
        }

        public override int ProcessDefaultState(CodeEditor editor, Region c)
        {
            switch (c.c)
            {
                case '"': case '\'':
                    return (int)BaseState.String;

                case '.':
                    // the beginning of a number or punctuation
                    return (int)(char.IsNumber(c[1]) ? BaseState.Number : BaseState.Punctuation);
                case ',': case ';':
                    return (int)BaseState.Punctuation;

                case '/':
                    if (c[1] == '/') return (int)BaseState.LineComment;
                    if (c[1] == '*') return (int)BaseState.BlockComment;
                    return (int)BaseState.Operator;
                case '*': case '+': case '-':
                    return (int)BaseState.Operator;

                case '(': case ')':
                case '[': case ']':
                case '{': case '}':
                    return (int)BaseState.Braces;
                    
                case '_':
                    return (int)State.Indicator;
            }

            // the beginning of a number
            if (char.IsNumber(c.c))
                return (int)BaseState.Number;
            
            // an operator
            if (char.IsSymbol(c.c))
                return (int)BaseState.Operator;

            // the beginning of a keyword
            if (char.IsLetter(c.c))
                return (int)State.Indicator;

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
                for (style = editor.GetStyleAt(--i);
                    i >= 0 && firstBaseStyle <= style && style <= lastBaseStyle;
                    style = editor.GetStyleAt(--i))
                {
                    var C = editor.GetCharAt(i);
                    if (C == '{' || C == '}')
                        break;
                }

                // if no function lexer state found, exit
                if ((i < 0 || style < firstStyle || lastStyle < style) && style != BraceStyle)
                    return -1;
            }

            return ProcessDefaultState(editor, c);
        }

        #endregion

        #region STATE
        
        private enum State : int { Default, Indicator, Keyword, DataType, BuiltIn, Function }

        protected override Type StateType => typeof(State);

        #endregion
    }

    #endregion

    #region STRUCTURES

    enum BaseState
    {
        Invalid = 16,
        Default,
        String,
        Number,
        Operator,
        Braces,
        Punctuation,
        LineComment,
        BlockComment,
        Name,
    }

    public class Region
    {
        private CodeEditor editor;
        private int pos;
        public char this[int i] => GetChar(pos + i);
        public char c { get; private set; }
        public int Pos
        {
            get => pos; set => c = (char)editor.GetCharAt(pos = value);
        }

        public Region(CodeEditor editor, int pos)
        {
            this.editor = editor;
            Pos = pos;
        }

        public int GetStyleAt(int i)
        {
            return editor.GetStyleAt(pos + i);
        }

        private char GetChar(int i)
        {
            return (char)(0 <= i && i < editor.TextLength ? editor.GetCharAt(i) : 0);
        }

        private string Substring(int startIndex, int length)
        {
            length -= Math.Max(0, -startIndex);
            startIndex = Math.Max(0, startIndex);
            length -= Math.Max(0, -editor.TextLength + startIndex + length);
            startIndex = Math.Min(editor.TextLength - 1, startIndex);
            return editor.GetTextRange(startIndex, length);
        }

        public override string ToString()
        {
            return $"{Substring(pos - 9, 9)}`{(char)editor.GetCharAt(pos)}´{Substring(pos + 1, 9)}";
        }
    }

    public static class XmlNodeExtensions
    {
        public static string GetAttributeValue(this XmlNode node, string name)
        {
            return node.Attributes.GetNamedItem(name)?.Value;
        }

        public static bool HasAttributeValue(this XmlNode node, string name)
        {
            return node.Attributes.GetNamedItem(name) != null;
        }
    }

    #endregion
}