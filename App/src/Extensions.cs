using System;
using System.Collections.Generic;

namespace App
{
    public static class Extensions
    {
        public static void ForEach<T>(this IEnumerable<T> ie, Action<T, int> action)
        {
            var i = 0;
            foreach (var e in ie)
                action(e, i++);
        }

        public static IEnumerable<TResult> ForEach<T, TResult>(
            this IEnumerable<T> ie, Func<T, int, TResult> action)
        {
            var i = 0;
            foreach (var e in ie)
                yield return action(e, i++);
        }

        public static object Find<T>(this IEnumerable<T> ie, Func<T, bool> action)
        {
            foreach (var e in ie)
                if (action(e))
                    return e;
            return null;
        }

        public static bool Has<T>(this IEnumerable<T> ie, T value)
        {
            foreach (var e in ie)
                if (e.Equals(value))
                    return true;
            return false;
        }
    }
}
