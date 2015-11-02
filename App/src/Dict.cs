using System.Collections.Generic;

namespace App
{
    class Dict<T> : Dictionary<string, T> where T : GLObject
    {
        public TResult FindClass<TResult>(string classname)
            where TResult : T
        {
            T obj = default(T);
            if (classname != null && TryGetValue(classname, out obj) && obj is TResult)
                return (TResult)obj;
            return default(TResult);
        }

        public bool TryFindClass<TResult>(string instancename, out TResult obj, GLException err = null)
            where TResult : T
        {
            // try to find the object instance
            if ((obj = FindClass<TResult>(instancename)) == null)
            {
                // get class name of object type
                var classname = typeof(TResult).Name.Substring(2).ToLower();
                err?.Add($"The name '{instancename}' could not be found or "
                    + $"does not reference an object of type '{classname}'.");
                return false;
            }
            return true;
        }

        public TResult ParseObject<TResult>(string arg, string info)
            where TResult : T
        {
            T tmp = default(T);
            if (TryGetValue(arg, out tmp) && tmp is TResult)
                return (TResult)tmp;
            throw new GLException(info);
        }

        public bool TryParseObject<TResult>(string name, ref TResult obj)
            where TResult : T
        {
            T tmp = default(T);
            if (obj == null && TryGetValue(name, out tmp) && tmp is TResult)
            {
                obj = (TResult)tmp;
                return true;
            }
            return false;
        }
    }
}
