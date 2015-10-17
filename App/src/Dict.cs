using System.Collections.Generic;

namespace App
{
    class Dict : Dictionary<string, GLObject>
    {
        public T FindClass<T>(string classname)
            where T : GLObject
        {
            GLObject obj = null;
            if (classname != null && TryGetValue(classname, out obj) && obj.GetType() == typeof(T))
                return (T)obj;
            return null;
        }

        public bool TryFindClass<T>(string instancename, out T obj, GLException err = null)
            where T : GLObject
        {
            // try to find the object instance
            if ((obj = FindClass<T>(instancename)) == null)
            {
                // get class name of object type
                var classname = typeof(T).Name.Substring(2).ToLower();
                err?.Add($"The name '{instancename}' could not be found or "
                    + $"does not reference an object of type '{classname}'.");
                return false;
            }
            return true;
        }

        public T ParseObject<T>(string arg, string info)
            where T : GLObject
        {
            GLObject tmp = null;
            if (TryGetValue(arg, out tmp) && tmp.GetType() == typeof(T))
                throw new GLException(info);
            return (T)tmp;
        }

        public bool TryParseObject<T>(string name, ref T obj)
            where T : GLObject
        {
            GLObject tmp;
            if (obj == null && TryGetValue(name, out tmp) && tmp.GetType() == typeof(T))
            {
                obj = (T)tmp;
                return true;
            }
            return false;
        }
    }
}
