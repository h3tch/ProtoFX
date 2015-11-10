using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace App
{
    /// <summary>
    /// This class extends several other classes with useful methods
    /// that are not (yet) provided by the .Net framework.
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Extends the ForEach method by adding a index variable.
        /// </summary>
        /// <typeparam name="T">
        /// Type of the IEnumerable object elements.</typeparam>
        /// <param name="ie">
        /// The object which receives this extension method is the generic IEnumerable type.</param>
        /// <param name="action">
        /// The action to be performed on every element of the IEnumerable object.</param>
        public static void ForEach<T>(this IEnumerable<T> ie, Action<T, int> action)
        {
            var i = 0;
            foreach (var e in ie)
                action(e, i++);
        }

        /// <summary>
        /// Extends the Select method by adding a index variable.
        /// </summary>
        /// <typeparam name="T">Type of the IEnumerable object elements.</typeparam>
        /// <typeparam name="TResult">The return type after processing each element.</typeparam>
        /// <param name="ie">
        /// The object which receives this extension method is the generic IEnumerable type.</param>
        /// <param name="func">
        /// The function to be performed on every element of the IEnumerable object.</param>
        /// <returns>Returns the processed elements.</returns>
        public static IEnumerable<TResult> Select<T, TResult>(this IEnumerable<T> ie,
            Func<T, int, TResult> func)
        {
            var i = 0;
            foreach (var e in ie)
                yield return func(e, i++);
        }

        /// <summary>
        /// Extends the IEnumerable class by a search and find functionality.
        /// </summary>
        /// <typeparam name="T">Type of the IEnumerable object elements.</typeparam>
        /// <param name="ie">
        /// The object which receives this extension method is the generic IEnumerable type.</param>
        /// <param name="func">
        /// The test function to be performed on every element of the IEnumerable object.</param>
        /// <returns>
        /// Returns the first object which fulfills the condition specified in func.</returns>
        public static object Find<T>(this IEnumerable<T> ie, Func<T, bool> func)
        {
            foreach (var e in ie)
                if (func(e))
                    return e;
            return null;
        }

        /// <summary>
        /// Extends the IEnumerable class with has-element functionality.
        /// </summary>
        /// <typeparam name="T">Type of the IEnumerable object elements.</typeparam>
        /// <param name="ie">
        /// The object which receives this extension method is the generic IEnumerable type.</param>
        /// <param name="value">
        /// The value to search for.</param>
        /// <returns>Returns true if the value was found.</returns>
        public static bool Has<T>(this IEnumerable<T> ie, T value)
        {
            foreach (var e in ie)
                if (e.Equals(value))
                    return true;
            return false;
        }

        /// <summary>
        /// Find tab in tab collection by the file path attached to it.
        /// </summary>
        /// <param name="tab">
        /// The object which receives this extension method is the TabPageCollection type.</param>
        /// <param name="path">
        /// The path to search for.</param>
        /// <returns>The tab index or -1 if the tab could not be found.</returns>
        public static int Find(this TabControl.TabPageCollection tab, string path)
        {
            for (int i = 0; i < tab.Count; i++)
            {
                if (((TabPage)tab[i]).filepath == path)
                    return i;
            }
            return -1;
        }
    }
}
