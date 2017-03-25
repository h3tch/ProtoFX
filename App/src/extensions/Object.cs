using System;
using System.Reflection;

namespace App.Extensions
{
    static class Object
    {
        public static (object Owner, object Info, int Index) FindMember<T>(this T me, string access)
        {
            var parts = access.Split('.');
            object owner = me;
            object info = null;
            object value = null;
            string name;
            int index;

            // navigate to last field
            for (int i = 0; i < parts.Length - 1; i++, owner = value)
            {
                (name, index) = GetNameAndIndex(parts[i]);

                // update info
                info = GetFieldOrProp(owner.GetType(), name);
                if (info == null)
                    return (null, null, -1);

                // update value
                switch (info)
                {
                    case FieldInfo field: value = field.GetValue(owner); break;
                    case PropertyInfo prop: value = prop.GetValue(owner); break;
                }
                // is array
                if (index >= 0)
                    value = (value as Array).GetValue(index);
            }

            (name, index) = GetNameAndIndex(parts[parts.Length - 1]);
            info = GetFieldOrProp(owner.GetType(), name);
            return (owner, info, index);

            /// LOCAL FUNCTIONS
            /// 
            (string, int) GetNameAndIndex(string part)
            {
                int idx = -1;
                var open = part.IndexOf('[');
                var str = part;

                if (open >= 0)
                {
                    var close = part.IndexOf(']');
                    if (close < 0)
                        throw new FormatException($"']' missing in '{part}'");
                    idx = int.Parse(part.Substring(open + 1, close));
                    str = part.Substring(0, open);
                }

                return (str, idx);
            }

            object GetFieldOrProp(Type type, string str)
            {
                const BindingFlags flags = BindingFlags.Public |
                    BindingFlags.NonPublic | BindingFlags.Instance;
                object fieldInfo = type.GetField(str, flags);
                if (fieldInfo == null)
                    fieldInfo = type.GetProperty(str, flags);
                return fieldInfo;
            }
        }

        public static void SetValue<T>(this T obj, string access, object value)
        {
            var (owner, info, index) = obj.FindMember(access);

            switch (info)
            {
                case FieldInfo field:
                    if (index >= 0)
                        (field.GetValue(owner) as Array).SetValue(value, index);
                    else
                        field.SetValue(owner, value);
                    break;
                case PropertyInfo prop:
                    if (index >= 0)
                        (prop.GetValue(owner) as Array).SetValue(value, index);
                    else
                        prop.SetValue(owner, value);
                    break;
            }
        }

        public static object GetValue<T>(this T obj, string access)
        {
            var (owner, info, index) = obj.FindMember(access);

            switch (info)
            {
                case FieldInfo field:
                    if (index >= 0)
                        return (field.GetValue(owner) as Array).GetValue(index);
                    else
                        return field.GetValue(owner);
                case PropertyInfo prop:
                    if (index >= 0)
                        return (prop.GetValue(owner) as Array).GetValue(index);
                    else
                        return prop.GetValue(owner);
            }

            return null;
        }
    }
}
