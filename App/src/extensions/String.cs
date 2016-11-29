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
        /// Get the start position of the word before or after the current position.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="startIndex">Start searching from this position.</param>
        /// <param name="ignoreWordCount">The number of words to ignore before
        /// returning the word position. If positive, the next words will be
        /// processed. If negative, the preceding words will be processed. If
        /// zero, the start position of the current word will be returned.</param>
        /// <returns>Returns the start position of the indicated word.</returns>
        public static int IndexOfWord(this string s, int startIndex, int ignoreWordCount = 1)
        {
            var step = ignoreWordCount <= 0 ? -1 : 1;

            // skip specified number of words
            for (var count = Math.Abs(ignoreWordCount); count != 0; count--)
            {
                // while inside the word
                for (; 0 <= startIndex && startIndex < s.Length; startIndex += step)
                    if (char.IsWhiteSpace(s[startIndex]))
                        break;
                // while a whitespace
                for (; 0 <= startIndex && startIndex < s.Length; startIndex += step)
                    if (!char.IsWhiteSpace(s[startIndex]))
                        break;
            }

            // goto the start index of the word
            for (int next = startIndex - 1; 0 <= next && next < s.Length; startIndex = next, next--)
                if (char.IsWhiteSpace(s[next]))
                    break;

            return startIndex;
        }

        public static string Word(this string s, int position)
        {
            int start = position;
            while (start > 0 && !char.IsWhiteSpace(s[start - 1]))
                start--;
            int end = start;
            while (end < s.Length && !char.IsWhiteSpace(s[end]))
                end++;

            return s.Subrange(Math.Max(0, start), Math.Min(s.Length, end));
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
