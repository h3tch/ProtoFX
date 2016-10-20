using System.Linq;
using System.Text;

namespace System.Collections.Generic
{
    public static class IEnumerableExtensions
    {
        /// <summary>
        /// Find first zero based index where the specified function returns true.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ie"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static int IndexOf<T>(this IEnumerable<T> ie, Func<T, bool> func, int startIndex = 0)
        {
            int i = 0;
            foreach (var e in ie.Skip(startIndex))
            {
                if (func(e))
                    return i;
                i++;
            }
            return -1;
        }

        /// <summary>
        /// Find last zero based index where the specified function returns true.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ie"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static int LastIndexOf<T>(this IEnumerable<T> ie, Func<T, bool> func)
        {
            int i = 0, rs = -1;
            foreach (var e in ie)
            {
                if (func(e))
                    rs = i;
                i++;
            }
            return rs;
        }

        /// <summary>
        /// Return the first element or a default value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ie"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static T FirstOr<T>(this IEnumerable<T> ie, T defaultValue)
            => ie.Any() ? ie.First() : defaultValue;

        /// <summary>
        /// Return the first element matching the specified predicate or a default value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ie"></param>
        /// <param name="predicate"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static T FirstOr<T>(this IEnumerable<T> ie, Func<T, bool> predicate, T defaultValue)
        {
            var iter = ie.Where(predicate);
            return iter.Any() ? iter.First() : defaultValue;
        }

        /// <summary>
        /// Concatenate a list of arrays.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public static IEnumerable<T> Cat<T>(this IEnumerable<IEnumerable<T>> id)
        {
            foreach (var el in id)
            {
                if (el != null)
                    foreach (var e in el)
                        yield return e;
            }
        }

        /// <summary>
        /// Concatenate a list of strings into a single string.
        /// </summary>
        /// <param name="list"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static string Cat(this IEnumerable<string> list, string separator)
        {
            var sepLen = separator.Length;
            var build = new StringBuilder(list.Sum(x => x.Length + sepLen));
            foreach (var s in list)
                build.Append(s + separator);
            return build.ToString(0, Math.Max(0, build.Length - sepLen));
        }

        /// <summary>
        /// Iterate through two lists simultaneously.
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="ie"></param>
        /// <param name="other"></param>
        /// <param name="func"></param>
        public static void ForEach<T1, T2>(this IEnumerable<T1> ie, IEnumerable<T2> other, Action<T1, T2> func)
        {
            var enumerator = other.GetEnumerator();
            enumerator.MoveNext();
            foreach (var e in ie)
            {
                func(e, enumerator.Current);
                enumerator.MoveNext();
            }
        }

        /// <summary>
        /// Process each element of a list.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ie"></param>
        /// <param name="func"></param>
        public static void ForEach<T>(this IEnumerable<T> ie, Action<T> func)
        {
            foreach (var e in ie)
                func(e);
        }

        /// <summary>
        /// Return the maximum or, if the enumerable
        /// is empty, the specified default value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ie"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static T MaxOr<T>(this IEnumerable<T> ie, T defaultValue)
            => ie.Count() > 0 ? ie.Max() : defaultValue;

        /// <summary>
        /// Process each object of the list using the specified
        /// function and return all thrown exceptions.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ie"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static IEnumerable<Exception> Catch<T>(this IEnumerable<T> ie, Action<T> func)
        {
            foreach (var e in ie)
            {
                Exception ex = null;
                try
                {
                    func(e);
                }
                catch (Exception x)
                {
                    ex = x;
                }
                if (ex != null)
                    yield return ex;
            }
        }
    }

}
