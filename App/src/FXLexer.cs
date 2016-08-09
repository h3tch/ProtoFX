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
        /// Get name of the specified style.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        string GetStyleName(int id);
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
                int styleCount = (int)Enum.Parse(StateType, "SEPARATOR");
                int endState = (int)Enum.Parse(StateType, "END");
                firstState = firstFreeState;
                lastState = (firstFreeState += endState) - 1;
                lastStyle = firstState + styleCount - 1;

                // allocate arrays for styles
                foreColor = new Color[styleCount];
                backColor = new Color[styleCount];
                keywords = new Trie<Keyword>[styleCount];
                for (int i = 0; i < keywords.Length; i++)
                    keywords[i] = new Trie<Keyword>();

                // get lexer type
                lexerType = lexerNode.GetAttributeValue("type");

                // get style colors
                var styleList = lexerNode.SelectNodes("Style");
                foreach (XmlNode style in styleList)
                {
                    var id = int.Parse(style.GetAttributeValue("id"));
                    foreColor[id] = ColorTranslator.FromHtml(style.GetAttributeValue("fore"));
                    backColor[id] = ColorTranslator.FromHtml(style.GetAttributeValue("back"));
                }

                // get keyword definitions
                var keywordList = lexerNode.SelectNodes("Keyword");
                foreach (XmlNode keyword in keywordList)
                {
                    var style = int.Parse(keyword.GetAttributeValue("style"));
                    var name = keyword.GetAttributeValue("name");
                    var hint = keyword.GetAttributeValue("hint");
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

        public string StyleToName(int style)
            => StateType == null ? "unknown" : Enum.GetName(StateType, style);

        public string GetKeywordHint(int style, string word)
            => SelectKeywordInfo(style, word).FirstOrDefault().hint;

        public IEnumerable<string> SelectKeywords(int style, string word)
            => SelectKeywordInfo(style, word).Select(x => x.word);

        public IEnumerable<Keyword> SelectKeywordInfo(int style, string word)
            => IsLexerStyle(style)
                ? keywords[StyleToIdx(style)][word]
                : lexer.SelectMany(x => x.SelectKeywordInfo(style, word));

        public Color GetStyleForeColor(int style)
            => IsLexerStyle(style)
                ? foreColor[StyleToIdx(style)]
                : lexer.Select(x => x.GetStyleForeColor(style)).FirstOrDefault(x => x != null);

        public Color GetStyleBackColor(int style)
            => IsLexerStyle(style)
                ? backColor[StyleToIdx(style)]
                : lexer.Select(x => x.GetStyleBackColor(style)).FirstOrDefault(x => x != null);

        public string GetStyleName(int id)
            => IsLexerStyle(id)
                ? StyleToName(id)
                : lexer.Select(x => x.GetStyleName(id)).FirstOrDefault(x => x != null);

        public IEnumerable<int> GetStyles()
        {
            var styles = lexer.SelectMany(x => x.GetStyles());
            return StateType == null ? styles : Enum.GetValues(StateType).Cast<int>().Concat(styles);
        }

        public virtual Type StateType => null;

        public bool IsLexerState(int state) => firstState <= state && state <= lastState;

        public bool IsLexerStyle(int style) => firstState <= style && style <= lastStyle;

        public int MaxState()
        {
            var max = lexer.Length == 0 ? 0 : lexer.Select(x => x.MaxState()).Max();
            return Math.Max(lastState, max);
        }

        public virtual bool IsLexerFor(string type)
            => lexerType != null ? lexerType == type : true;
    }

    public class FxLexer : BaseLexer
    {

        public FxLexer() : base(0, LoadXml())
        {
        }

        public override int Style(CodeEditor editor, int pos, int endPos)
        {
            // go back to the first valid style
            while (pos > 0 && editor.GetStyleAt(pos) == 0)
                pos--;
            
            while (pos < endPos)
            {
                // get style at the current position
                var style = editor.GetStyleAt(pos);
                // select the lexer that can continue lexing from the current position
                var lex = lexer.Where(x => x.IsLexerState(style)).First();
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

        public TechLexer(int firstFreeStyle, XmlNode xml) : base(firstFreeStyle, xml)
        {
        }

        public override int Style(CodeEditor editor, int pos, int endPos)
        {
            for (var state = (State)editor.GetStyleAt(pos); pos < endPos; pos++)
            {
                state = ProcessState(editor, state, (char)editor.GetCharAt(pos));
                editor.SetStyling(1, firstState + (int)state);
            }
            return pos;
        }

        #region PROCESS STATE
        private State ProcessState(CodeEditor editor, State state, char c)
        {
            switch (state)
            {
                case State.Comment:
                    return ProcessCommentState(editor, c);
                case State.LineComment:
                    return ProcessLineCommentState(editor, c);
                case State.BlockComment:
                    return ProcessBlockCommentState(editor, c);
                case State.PotentialEndBlockComment:
                    return ProcessPotentialEndBlockCommentState(editor, c);
                case State.EndBlockComment:
                    return ProcessEndBlockCommentState(editor, c);
                case State.Type:
                    return ProcessTypeState(editor, c);
                case State.TypeSpace:
                    return ProcessTypeSpaceState(editor, c);
                case State.Name:
                    return ProcessNameState(editor, c);
                case State.NameSpace:
                    return ProcessNameSpaceState(editor, c);
                case State.Anno:
                    return ProcessAnnoState(editor, c);
                case State.AnnoSpace:
                    return ProcessAnnoSpaceState(editor, c);
                case State.OpenBrace:
                    return ProcessOpenBraceState(editor, c);
                case State.CloseBrace:
                    return ProcessCloseBraceState(editor, c);
                case State.BlockCode:
                    return ProcessBlockCodeState(editor, c);
                default:
                    return ProcessDefaultState(editor, c);
            }
        }

        private State ProcessDefaultState(CodeEditor editor, char c)
        {
            if (c == '/')
                return State.Comment;
            else if (char.IsLetter(c))
                return State.Type;
            else
                return State.Default;
        }

        private State ProcessCommentState(CodeEditor editor, char c)
        {
            switch (c)
            {
                case '/':
                    return State.LineComment;
                case '*':
                    return State.BlockComment;
            }
            return ProcessDefaultState(editor, c);
        }

        private State ProcessLineCommentState(CodeEditor editor, char c)
            => c == '\n' ? State.Default : State.LineComment;

        private State ProcessBlockCommentState(CodeEditor editor, char c)
            => c == '*' ? State.PotentialEndBlockComment : State.BlockComment;

        private State ProcessPotentialEndBlockCommentState(CodeEditor editor, char c)
        {
            switch (c)
            {
                case '/':
                    return State.EndBlockComment;
                case '*':
                    return State.PotentialEndBlockComment;
                default:
                    return State.BlockComment;
            }
        }

        private State ProcessEndBlockCommentState(CodeEditor editor, char c)
            => ProcessDefaultState(editor, c);

        private State ProcessTypeState(CodeEditor editor, char c)
        {
            if (char.IsWhiteSpace(c))
                return State.TypeSpace;
            else if (c == '{')
                return State.OpenBrace;
            else
                return State.Type;
        }

        private State ProcessTypeSpaceState(CodeEditor editor, char c)
        {
            if (c == '{')
                return State.OpenBrace;
            else if (char.IsLetter(c))
                return State.Name;
            else
                return State.TypeSpace;
        }

        private State ProcessNameState(CodeEditor editor, char c)
        {
            if (char.IsWhiteSpace(c))
                return State.NameSpace;
            else if (c == '{')
                return State.OpenBrace;
            else
                return State.Name;
        }

        private State ProcessNameSpaceState(CodeEditor editor, char c)
        {
            if (c == '{')
                return State.OpenBrace;
            else if (char.IsLetter(c))
                return State.Anno;
            else
                return State.NameSpace;
        }

        private State ProcessAnnoState(CodeEditor editor, char c)
        {
            if (char.IsWhiteSpace(c))
                return State.AnnoSpace;
            else if (c == '{')
                return State.OpenBrace;
            else
                return State.Anno;
        }

        private State ProcessAnnoSpaceState(CodeEditor editor, char c)
            => c == '{' ? State.OpenBrace : State.AnnoSpace;

        private State ProcessOpenBraceState(CodeEditor editor, char c)
        {
            if (c == '}')
                return State.CloseBrace;
            braceCount = 1;
            return State.BlockCode;
        }

        private State ProcessCloseBraceState(CodeEditor editor, char c)
            => ProcessDefaultState(editor, c);

        private State ProcessBlockCodeState(CodeEditor editor, char c)
        {
            switch (c)
            {
                case '{':
                    braceCount++;
                    break;
                case '}':
                    if (--braceCount > 0)
                        break;
                    // RE-LEX ALL BLOCKCODE PARTS
                    // get code region of the block
                    var end = editor.GetEndStyled();
                    var start = FindLastStyleOf(editor, (int)State.OpenBrace, end, end);
                    // get header string from block position
                    var header = FindLastHeaderFromPosition(editor, start);
                    // find lexer that can lex this code block
                    var lex = lexer?.Where(x => x.IsLexerFor(header[0])).FirstOrDefault();
                    // re-lex code block
                    lex?.Style(editor, start + 1, end - 1);
                    return State.CloseBrace;
            }
            return State.BlockCode;
        }
        #endregion

        #region HELPER FUNCTION
        private string[] FindLastHeaderFromPosition(CodeEditor editor, int pos)
        {
            int bracePos = FindLastStyleOf(editor, (int)State.OpenBrace, pos, pos);
            int typePos = FindLastStyleOf(editor, (int)State.Type, bracePos, bracePos);
            int annoPos = FindLastStyleOf(editor, (int)State.Anno, bracePos, bracePos - typePos);

            return new string[] {
                typePos >= 0 ? editor.GetWordFromPosition(typePos) : null,
                annoPos >= 0 ? editor.GetWordFromPosition(annoPos) : null,
            };
        }

        private int FindLastStyleOf(CodeEditor editor, int style, int from, int length)
        {
            int to = Math.Max(from - Math.Max(length, 0), 0);
            while (from > to && editor.GetStyleAt(from) != style)
                from--;
            return editor.GetStyleAt(from) == style ? from : -1;
        }

        private enum State : int
        {
            Default,
            Type,
            Anno,
            SEPARATOR,
            Comment,
            LineComment,
            BlockComment,
            PotentialEndBlockComment,
            EndBlockComment,
            TypeSpace,
            Name,
            NameSpace,
            AnnoSpace,
            OpenBrace,
            CloseBrace,
            BlockCode,
            END
        }

        public override Type StateType => typeof(State);
        #endregion
    }

    class BlockLexer : BaseLexer
    {
        public BlockLexer(int firstFreeStyle, XmlNode xml) : base(firstFreeStyle, xml)
        {
        }

        public override int Style(CodeEditor editor, int pos, int endPos)
        {
            return endPos;
        }

        #region HELPER FUNCTION
        private enum State : int
        {
            Default,
            Command,
            Number,
            Argument,
            SEPARATOR,
            END
        }

        public override Type StateType => typeof(State);
        #endregion
    }

    class CommandLexer : BaseLexer
    {
        public CommandLexer(int firstFreeStyle, XmlNode xml) : base(firstFreeStyle, xml)
        {
        }

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
            SEPARATOR,
            END
        }

        public override Type StateType => typeof(State);
        #endregion
    }

    class GlslLexer : BaseLexer
    {
        public GlslLexer(int firstFreeStyle, XmlNode xml) : base(firstFreeStyle, xml)
        {
        }

        public override int Style(CodeEditor editor, int pos, int endPos)
        {
            return endPos;
        }

        #region HELPER FUNCTION
        private enum State : int
        {
            Default,
            Command,
            Number,
            Argument,
            SEPARATOR,
            END
        }

        public override Type StateType => typeof(State);
        #endregion
    }
}
