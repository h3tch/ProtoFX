using System.Linq;

namespace System.Collections.Generic
{
    public static class DictionaryExtensions
    {
        /// <summary>
        /// Get the value to the specified key. Throw
        /// and exception if the value could be found.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">Key to search for.</param>
        /// <param name="info">Error string to provide if the value could not be found.</param>
        /// <returns>Returns the value to the respective string.</returns>
        public static TValue GetValue<TKey, TValue>(this Dictionary<TKey, TValue> dict,
            TKey key, string info = "Value not found.")
        {
            if (dict.TryGetValue(key, out var tmp) && tmp is TValue)
                return tmp;
            throw new ArgumentException(info);
        }

        /// <summary>
        /// Get the value to the specified key. Throw
        /// and exception if the value could be found.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">Key to search for.</param>
        /// <returns>Returns the value to the respective
        /// string or the default value of the type.</returns>
        public static object GetValueOrDefault<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key)
        {
            return key != null && dict.TryGetValue(key, out var obj) && obj is TValue
                ? obj : default(object);
        }

        public static IEnumerable<TValue> Where<TKey, TValue>(this Dictionary<TKey, TValue> dict, Type type)
        {
            return from x in dict
                   where x.Value.GetType().IsSubclassOf(type)
                   select x.Value;
        }

        public static IEnumerable<TResult> Where<TResult>(this IDictionary dict)
        {
            foreach (var value in dict.Values)
            {
                if (value is TResult)
                    yield return (TResult)Convert.ChangeType(value, typeof(TResult));
            }
        }
    }
}
