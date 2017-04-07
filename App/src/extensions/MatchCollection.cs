using System.Collections.Generic;

namespace System.Text.RegularExpressions
{
    public static class MatchCollectionExtensions
    {
        /// <summary>
        /// Convert MatchCollection to an array.
        /// </summary>
        /// <param name="matches"></param>
        /// <returns></returns>
        public static IEnumerable<Match> ToArray(this MatchCollection matches)
        {
            foreach (Match match in matches)
                yield return match;
        }
    }
}