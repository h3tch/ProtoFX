using System.Collections.Generic;

namespace App
{
    /// <summary>
    /// Specialized dictionary to handle OpenGL objects.
    /// </summary>
    /// <typeparam name="TValue">
    /// Type to be stored in the dictionary. Has to be a GLObject type.</typeparam>
    class Dict : Dictionary<string, GLObject>
    {
        /// <summary>
        /// Get the value to the specified key. Throw
        /// and exception if the value could be found.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">Key to search for.</param>
        /// <param name="info">Error string to provide if the value could not be found.</param>
        /// <returns>Returns the value to the respective string.</returns>
        public T GetValue<T>(string key, string info = "Value not found.") where T : GLObject
        {
            if (TryGetValue(key, out var tmp) && tmp is T)
                return (T)tmp;
            throw new CompileException(info);
        }

        /// <summary>
        /// Get the value to the specified key. Throw
        /// and exception if the value could be found.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">Key to search for.</param>
        /// <returns>Returns the value to the respective
        /// string or the default value of the type.</returns>
        public T GetValueOrDefault<T>(string key) where T : GLObject
        {
            return key != null && TryGetValue(key, out var obj) && obj is T ? (T)obj : default(T);
        }

        /// <summary>
        /// Get the value to the specified key.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">Key to search for.</param>
        /// <param name="obj">Output object reference.</param>
        /// <returns>Returns true if the value could be found.</returns>
        public bool TryGetValue<T>(string key, ref T obj) where T : GLObject
        {
            if (obj != null || !TryGetValue(key, out var tmp) || !(tmp is T))
                return false;
            obj = (T)tmp;
            return true;
        }

        /// <summary>
        /// Try to find the value to a key. If the key could not be found add an exception message.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">Key to search for.</param>
        /// <param name="obj">Output object reference.</param>
        /// <param name="file">Specify the file identifying an exception if it occurs.</param>
        /// <param name="line">Specify the file line identifying an exception if it occurs.</param>
        /// <param name="err">Add new exceptions to this existing one.</param>
        /// <returns></returns>
        public bool TryGetValue<T>(string key, out T obj, int line, string file,
            CompileException err)
            where T : GLObject
        {
            // try to find the object instance
            if ((obj = GetValueOrDefault<T>(key)) != default(T))
                return true;

            // get class name of object type
            var classname = typeof(T).Name.Substring(2).ToLower();
            err.Error($"The name '{key}' could not be found or does not "
                + $"reference an object of type '{classname}'.", file, line);
            return false;
        }
    }
}
