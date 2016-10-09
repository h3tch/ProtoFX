using System.Text.RegularExpressions;

namespace System
{
    public static class StringExtensions
    {
        /// <summary>
        /// Find first match of two matching braces.
        /// </summary>
        /// <param name="s">extend string class</param>
        /// <param name="open">opening brace character</param>
        /// <param name="close">closing brace character</param>
        /// <returns>Returns the first brace match.</returns>
        public static Match BraceMatch(this string s, char open, char close)
        {
            string oc = $"{open}{close}";
            return Regex.Match(s, $"{open}[^{oc}]*(((?<Open>{open})[^{oc}]*)+" +
                $"((?<Close-Open>{close})[^{oc}]*)+)*(?(Open)(?!)){close}");
        }

        /// <summary>
        /// Get zero based line index from the zero based character position.
        /// </summary>
        /// <param name="s">extend string class</param>
        /// <param name="position">zero based character position</param>
        /// <returns>Returns the zero based line index.</returns>
        public static int LineFromPosition(this string s, int position)
        {
            int lineCount = 0;

            for (int i = 0; i < position; i++)
            {
                if (s[i] == '\n')
                {
                    lineCount++;
                }
                else if (s[i] == '\r')
                {
                    if (s[i + 1] == '\n')
                        i++;
                    lineCount++;
                }
            }

            return lineCount;
        }

        /// <summary>
        /// The sub range of the string beginning at the zero based start index
        /// and ending at the zero based end index.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="start">Zero based start index.</param>
        /// <param name="end">Zero based end index.</param>
        /// <returns></returns>
        public static string Subrange(this string s, int start, int end)
            => s.Substring(start, end - start);
    }

}
