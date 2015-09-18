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

        public static string NotFoundMsg(string callertype, string callername, string classtype, string classname)
        {
            return "ERROR in " + callertype + " " + callername + ": "
                + "The name '" + classname + "' does not reference an object of type '" + classtype + "'.";
        }
    }
}
