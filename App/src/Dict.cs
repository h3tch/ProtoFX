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

        public bool TryFindClass<T>(ErrorCollector err, string instancename, out T obj)
            where T : GLObject
        {
            var classname = typeof(T).Name.Substring(2).ToLower();
            if ((obj = FindClass<T>(instancename)) == null)
            {
                err.Add("The name '" + instancename + "' could not be found or "
                    + "does not reference an object of type '" + classname + "'.");
                return false;
            }
            else
                return true;
        }
    }
}
