using System.Text.RegularExpressions;

namespace System
{
    public static class StringExtensions
    {
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
        /// Get index of the next non whitespace character.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="startIndex"></param>
        /// <param name="step"></param>
        /// <returns></returns>
        public static int NextNonWhitespace(this string s, int startIndex = 0, int step = 1)
        {
            step = Math.Sign(step);
            for (int i = startIndex; step > 0 ? i < s.Length : i >= 0; i += step)
                if (!char.IsWhiteSpace(s[i]))
                    return i;
            return -1;
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
        {
            return s.Substring(start, end - start);
        }
    }

}
