using ScintillaNET;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Xml;

namespace App.Lexer
{
    public struct Keyword
    {
        public string word;
        public string hint;
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
        IEnumerable<int> GetStyles();
        /// <summary>
        /// Get foreground color of the specified style.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Color GetStyleForeColor(int id);
        /// <summary>
        /// Get background color of the specified style.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Color GetStyleBackColor(int id);
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
        /// <summary>
        /// Is the specified style handled by this lexer?
        /// </summary>
        /// <param name="style"></param>
        /// <returns></returns>
        bool IsLexerState(int style);
        /// <summary>
        /// Is lexer for the specified block type?
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        bool IsLexerFor(string type);
        ILexer FindLexer(int state);
        int MaxState();
    }

    public abstract class BaseLexer : ILexer
    {
        protected string lexerType;
        protected ILexer[] lexer;
        protected Color[] foreColor;
        protected Color[] backColor;
        protected Trie<Keyword>[] keywords;
        protected int firstState = 0;
        protected int lastStyle = -1;
        protected int lastState = -1;
        private enum FoldState : int
        {
            Unknown,
            StartFolding,
            Foldable,
            EndFolding,
        }

        public BaseLexer(int firstFreeState, XmlNode lexerNode)
        {
            if (StateType != null)
            {
                // get style and state ranges
                int styleCount = (int)Enum.Parse(StateType, "ENDSTYLE");
                int stateCount = (int)Enum.Parse(StateType, "ENDSTATE");
                firstState = firstFreeState;
                lastState = (firstFreeState += stateCount) - 1;
                lastStyle = firstState + styleCount - 1;

                // allocate arrays for styles
                foreColor = new Color[styleCount];
                backColor = new Color[styleCount];
                keywords = new Trie<Keyword>[styleCount];

                // get lexer type
                lexerType = lexerNode.GetAttributeValue("type");

                // get style colors
                var styleList = lexerNode.SelectNodes("Style");
                foreach (XmlNode style in styleList)
                {
                    var id = (int)Enum.Parse(StateType, style.GetAttributeValue("name"), true);
                    foreColor[id] = ColorTranslator.FromHtml(style.GetAttributeValue("fore"));
                    backColor[id] = ColorTranslator.FromHtml(style.GetAttributeValue("back"));
                }

                // get keyword definitions
                var keywordList = lexerNode.SelectNodes("Keyword");
                foreach (XmlNode keyword in keywordList)
                {
                    var style = (int)Enum.Parse(StateType, keyword.GetAttributeValue("style_name"), true);
                    var name = keyword.GetAttributeValue("name");
                    var hint = keyword.GetAttributeValue("hint");
                    if (keywords[style] == null)
                        keywords[style] = new Trie<Keyword>();
                    keywords[style].Add(name, new Keyword { word = name, hint = hint });
                }
            }

            // instantiate sub-lexers
            var lexerList = lexerNode.SelectNodes("Lexer");
            lexer = new ILexer[lexerList.Count];
            for (int i = 0; i < lexerList.Count; i++)
            {
                var type = lexerList[i].GetAttributeValue("lexer");
                var param = new object[] { firstFreeState, lexerList[i] };
                var t = Type.GetType($"App.Lexer.{type}");
                lexer[i] = (ILexer)Activator.CreateInstance(t, param);
                firstFreeState = lexer[i].MaxState() + 1;
            }
        }

        public abstract int Style(CodeEditor editor, int pos, int endPos);

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

        public int StyleToIdx(int style) => style - firstState;

        public string StyleToName(int style) => StateType == null ? "" : Enum.GetName(StateType, style);

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
            if (IsLexerStyle(style))
                return keywords[StyleToIdx(style)] != null
                    ? keywords[StyleToIdx(style)][word]
                    : Enumerable.Empty<Keyword>();
            return lexer.SelectMany(x => x.SelectKeywordInfo(style, word));
        }

        public Color GetStyleForeColor(int style)
        {
            return IsLexerStyle(style) ? foreColor[StyleToIdx(style)]
                : lexer.Select(x => x.GetStyleForeColor(style)).FirstOrDefault(x => x != null);
        }

        public Color GetStyleBackColor(int style)
        {
            return IsLexerStyle(style) ? backColor[StyleToIdx(style)]
                : lexer.Select(x => x.GetStyleBackColor(style)).FirstOrDefault(x => x != null);
        }
        
        public IEnumerable<int> GetStyles()
        {
            var styles = lexer.SelectMany(x => x.GetStyles());
            return StateType == null ? styles : Enum.GetValues(StateType).Cast<int>().Concat(styles);
        }

        public virtual Type StateType => null;

        public ILexer FindLexer(int state)
        {
            return IsLexerState(state) ? this
                : lexer.Select(x => x.FindLexer(state)).FirstOrDefault(x => x != null);
        }

        public bool IsLexerState(int state) => firstState <= state && state <= lastState;

        public bool IsLexerStyle(int style) => firstState <= style && style <= lastStyle;

        public virtual bool IsLexerFor(string type) => lexerType != null ? lexerType == type : true;

        public int MaxState()
        {
            var max = lexer.Length == 0 ? 0 : lexer.Select(x => x.MaxState()).Max();
            return Math.Max(lastState, max);
        }

        #region HELPER FUNCTION
        protected int FindLastStyleOf(CodeEditor editor, int style, int from, int length)
        {
            int to = Math.Max(from - Math.Max(length, 0), 0);
            while (from > to && editor.GetStyleAt(from) != style)
                from--;
            return editor.GetStyleAt(from) == style ? from : -1;
        }
        #endregion
    }

    public class FxLexer : BaseLexer
    {
        public FxLexer() : base(0, LoadXml()) { }

        public override int Style(CodeEditor editor, int pos, int endPos)
        {
            // go back to the first valid style
            while (pos > 0 && editor.GetStyleAt(pos) == 0)
                pos--;
            editor.StartStyling(pos);

            while (pos < endPos)
            {
                // get style at the current position
                var style = editor.GetStyleAt(pos);
                // select the lexer that can continue lexing from the current position
                var lex = FindLexer(style);
                // start lexing until the lexer reaches an end state,
                // then try to use another lexer
                pos = lex.Style(editor, pos, endPos);
            }

            return pos;
        }

        private static XmlNode LoadXml()
        {
            var xml = new XmlDocument();
            xml.LoadXml(Properties.Resources.keywordsXML);
            return xml.SelectSingleNode("FxLexer");
        }
    }

    class TechLexer : BaseLexer
    {
        private int braceCount;

        public TechLexer(int nextStyle, XmlNode xml) : base(nextStyle, xml) { }

        public override int Style(CodeEditor editor, int pos, int endPos)
        {
            // We cannot continue within a code block.
            // Go back to brace opening of the code block.
            if ((State)editor.GetStyleAt(pos) == State.BlockCode)
                pos = FindLastStyleOf(editor, (int)State.Brace, pos, pos);
            editor.StartStyling(pos);

            // instantiate region class
            var c = new Region(editor);

            // continue processing from the last state
            for (var state = (State)editor.GetStyleAt(pos); pos < endPos; pos++)
            {
                state = ProcessState(editor, state, c.Set(pos));
                editor.SetStyling(1, firstState + (int)state);
            }

            return pos;
        }

        #region PROCESS STATE
        private State ProcessState(CodeEditor editor, State state, Region c)
        {
            switch (state)
            {
                case State.LineComment:
                    return ProcessLineCommentState(editor, c);
                case State.BlockComment:
                    return ProcessBlockCommentState(editor, c);
                case State.Type:
                    return ProcessTypeState(editor, c);
                case State.Name:
                    return ProcessNameState(editor, c);
                case State.Anno:
                    return ProcessAnnoState(editor, c);
                case State.Brace:
                    return ProcessBraceState(editor, c);
                case State.BlockCode:
                    return ProcessBlockCodeState(editor, c);
                default:
                    return ProcessDefaultState(editor, c);
            }
        }

        private State ProcessDefaultState(CodeEditor editor, Region c)
        {
            if (c.c == '/')
            {
                if (c.r == '/')
                    return State.LineComment;
                else if (c.r == '*')
                    return State.BlockComment;
            }
            else if (char.IsLetter(c.c))
                return State.Type;
            return State.Default;
        }

        private State ProcessLineCommentState(CodeEditor editor, Region c)
            => c.c == '\n' ? State.Default : State.LineComment;

        private State ProcessBlockCommentState(CodeEditor editor, Region c)
            => c.ll == '*' && c.l == '/' ? ProcessDefaultState(editor, c) : State.BlockComment;

        private State ProcessTypeState(CodeEditor editor, Region c)
        {
            if (char.IsWhiteSpace(c.l) && char.IsLetter(c.c))
                return State.Name;
            else if (c.c == '{')
                return State.Brace;
            return State.Type;
        }
        
        private State ProcessNameState(CodeEditor editor, Region c)
        {
            if (char.IsWhiteSpace(c.l) && char.IsLetter(c.c))
                return State.Anno;
            else if (c.c == '{')
            {
                braceCount = 1;
                return State.Brace;
            }
            return State.Name;
        }
        
        private State ProcessAnnoState(CodeEditor editor, Region c)
        {
            if (c.c == '{')
            {
                braceCount = 1;
                return State.Brace;
            }
            return State.Anno;
        }
        
        private State ProcessBraceState(CodeEditor editor, Region c)
        {
            if (braceCount == 0 && c.l == '}')
                return ProcessDefaultState(editor, c);
            return ProcessBlockCodeState(editor, c);
        }

        private State ProcessBlockCodeState(CodeEditor editor, Region c)
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
                    // get code region of the block
                    var start = FindLastStyleOf(editor, firstState + (int)State.Brace, c.pos, c.pos);
                    // get header string from block position
                    var header = FindLastHeaderFromPosition(editor, start);
                    // find lexer that can lex this code block
                    var lex = lexer?.Where(x => x.IsLexerFor(header[0])).FirstOrDefault();
                    // re-lex code block
                    lex?.Style(editor, start + 1, c.pos - 1);
                    // continue styling from the last position
                    editor.StartStyling(c.pos);
                    return State.Brace;
            }
            return State.BlockCode;
        }
        #endregion

        #region HELPER FUNCTION
        private string[] FindLastHeaderFromPosition(CodeEditor editor, int pos)
        {
            int bracePos = FindLastStyleOf(editor, firstState + (int)State.Brace, pos, pos);
            int typePos = FindLastStyleOf(editor, firstState + (int)State.Type, bracePos, bracePos);
            int annoPos = FindLastStyleOf(editor, firstState + (int)State.Anno, bracePos, bracePos - typePos);

            return new string[] {
                typePos >= 0 ? editor.GetWordFromPosition(typePos) : null,
                annoPos >= 0 ? editor.GetWordFromPosition(annoPos) : null,
            };
        }
        
        private enum State : int
        {
            Default,
            Type,
            Name,
            Anno,
            Brace,
            LineComment,
            BlockComment,
            ENDSTYLE,
            BlockCode,
            ENDSTATE,
        }

        public override Type StateType => typeof(State);
        #endregion
    }

    class BlockLexer : BaseLexer
    {
        public BlockLexer(int nextStyle, XmlNode xml) : base(nextStyle, xml) { }

        public override int Style(CodeEditor editor, int pos, int endPos)
        {
            editor.StartStyling(pos);

            // instantiate region class
            var c = new Region(editor);

            // continue processing from the last state
            for (var state = State.Default; pos < endPos; pos++)
            {
                state = ProcessState(editor, state, c.Set(pos));
                editor.SetStyling(1, firstState + (int)state);
            }
            return endPos;
        }

        #region PROCESS STATE
        private State ProcessState(CodeEditor editor, State state, Region c)
        {
            switch (state)
            {
                case State.Command:
                    return ProcessCommandState(editor, c);
                case State.LineComment:
                    return ProcessLineCommentState(editor, c);
                case State.BlockComment:
                    return ProcessBlockCommentState(editor, c);
                case State.CommandCode:
                    return ProcessCommandCodeState(editor, c);
                default:
                    return ProcessDefaultState(editor, c);
            }
        }

        private State ProcessDefaultState(CodeEditor editor, Region c)
        {
            if (c.c == '/')
            {
                if (c.r == '/')
                    return State.LineComment;
                else if (c.r == '*')
                    return State.BlockComment;
            }
            else if (char.IsLetter(c.c))
                return State.Command;
            return State.Default;
        }

        private State ProcessLineCommentState(CodeEditor editor, Region c)
            => c.c == '\n' ? State.Default : State.LineComment;

        private State ProcessBlockCommentState(CodeEditor editor, Region c)
            => c.ll == '*' && c.l == '/' ? ProcessDefaultState(editor, c) : State.BlockComment;

        private State ProcessCommandState(CodeEditor editor, Region c)
        {
            if (char.IsWhiteSpace(c.c))
                return State.CommandCode;
            return State.Command;
        }

        private State ProcessCommandCodeState(CodeEditor editor, Region c)
        {
            if (c.c == '\n')
            {
                // RE-LEX ALL COMMANDCODE PARTS
                // get code region of the block
                var start = FindLastStyleOf(editor, firstState + (int)State.Command, c.pos, c.pos);
                // get header string from block position
                var cmd = FindLastCommandFromPosition(editor, start);
                // find lexer that can lex this code block
                var lex = lexer?.Where(x => x.IsLexerFor(cmd)).FirstOrDefault();
                // re-lex code block
                lex?.Style(editor, start + 1, c.pos - 1);
                // continue styling from the last position
                editor.StartStyling(c.pos);
                return State.Default;
            }
            return State.CommandCode;
        }
        #endregion

        #region HELPER FUNCTION
        private string FindLastCommandFromPosition(CodeEditor editor, int pos)
        {
            int cmdPos = FindLastStyleOf(editor, firstState + (int)State.Command, pos, pos);
            return cmdPos >= 0 ? editor.GetWordFromPosition(cmdPos) : null;
        }

        private enum State : int
        {
            Default,
            Command,
            LineComment,
            BlockComment,
            ENDSTYLE,
            CommandCode,
            ENDSTATE,
        }

        public override Type StateType => typeof(State);
        #endregion
    }

    class CommandLexer : BaseLexer
    {
        public CommandLexer(int nextStyle, XmlNode xml) : base(nextStyle, xml) { }

        public override int Style(CodeEditor editor, int pos, int endPos)
        {
            return endPos;
        }

        #region HELPER FUNCTION
        private enum State : int
        {
            Default,
            Argument,
            Number,
            String,
            ENDSTYLE,
            ENDSTATE,
        }

        public override Type StateType => typeof(State);
        #endregion
    }

    class GlslLexer : BaseLexer
    {
        public GlslLexer(int nextStyle, XmlNode xml) : base(nextStyle, xml) { }

        public override int Style(CodeEditor editor, int pos, int endPos)
        {
            return endPos;
        }

        #region HELPER FUNCTION
        private enum State : int
        {
            Default,
            ENDSTYLE,
            ENDSTATE,
        }

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
