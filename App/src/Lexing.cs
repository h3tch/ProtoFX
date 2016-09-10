using System.Collections.Generic;

namespace App.Lexer
{
    /// <summary>
    /// Common lexing interface for ProtoFX lexers.
    /// </summary>
    public interface ILexer
    {
        /// <summary>
        /// Style the specified text position.
        /// </summary>
        /// <param name="editor">The code editor component to be lexed.</param>
        /// <param name="pos">The start position to begin lexing at.
        /// This position is not fixed. If necessary, the start position
        /// will be shifted to a valid start position. All characters
        /// between <code>pos</code> and <code>endPos</code> are ensured
        /// to be lexed.</param>
        /// <param name="endPos">The end position to end lexing at.
        /// This position is not fixed. Lexing will end as soon as the
        /// styles do not change anymore. All characters between <code>pos</code>
        /// and <code>endPos</code> are ensured to be lexed.</param>
        /// <returns></returns>
        int Style(CodeEditor editor, int pos, int endPos);
        /// <summary>
        /// Add folding to the specified text position.
        /// </summary>
        /// <param name="editor">The code editor component to be lexed.</param>
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
        /// <param name="editor">The code editor component to be lexed.</param>
        /// <param name="position"></param>
        /// <returns>Returns the style for the word at the specified
        /// position or <code>null</code>.</returns>
        string GetKeywordHint(int id, string word = null);
        /// <summary>
        /// Find all potential keywords starting with the word at the specified position.
        /// </summary>
        /// <param name="style"></param>
        /// <param name="word"></param>
        /// <returns>Returns a list of keywords.</returns>
        IEnumerable<string> SelectKeywords(int style, string word = null);
        /// <summary>
        /// Find all keyword information starting with the word at the specified position.
        /// </summary>
        /// <param name="style"></param>
        /// <param name="word"></param>
        /// <returns></returns>
        IEnumerable<Keyword> SelectKeywordInfo(int style, string word);
    }
}
