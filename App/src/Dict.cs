using System;
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
        public T GetValue<T>(string key) where T : GLObject
        {
            GLObject obj = default(GLObject);
            if (key != null && TryGetValue(key, out obj) && obj is T)
                return (T)obj;
            return default(T);
        }

        public bool TryGetValue<T>(string key, out T obj, Compiler.Command cmd, CompileException err = null)
            where T : GLObject
            => TryGetValue(key, out obj, cmd.File, cmd.LineInFile);

        public bool TryGetValue<T>(string key, out T obj, Compiler.Block block, CompileException err = null)
            where T : GLObject
            => TryGetValue(key, out obj, block.File, block.LineInFile);

        public bool TryGetValue<T>(string key, out T obj, string file, int line, CompileException err = null)
            where T : GLObject
        {
            // try to find the object instance
            if ((obj = GetValue<T>(key)) != null)
                return true;

            // get class name of object type
            var classname = typeof(T).Name.Substring(2).ToLower();
            err?.Add($"The name '{key}' could not be found or does not "
                + $"reference an object of type '{classname}'.", file, line);
            return false;
        }

        public T GetValue<T>(string key, string info) where T : GLObject
        {
            GLObject tmp = default(GLObject);
            if (TryGetValue(key, out tmp) && tmp is T)
                return (T)tmp;
            throw new CompileException(info);
        }

        public bool TryGetValue<T>(string key, ref T obj) where T : GLObject
        {
            GLObject tmp;
            if (obj != null || !TryGetValue(key, out tmp) || !(tmp is T))
                return false;
            obj = (T)tmp;
            return true;
        }
    }
}
