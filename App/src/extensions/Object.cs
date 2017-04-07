using System;
using System.Linq;
using System.Reflection;

namespace App.Extensions
{
    static class Object
    {
        static BindingFlags flags = BindingFlags.Public |
            BindingFlags.NonPublic | BindingFlags.Instance;

        static void FindMember<T>(this T me, string access, out object Owner, out object Info, out int Index)
        {
            var parts = access.Split('.');
            object owner = me;
            object info = null;
            object value = null;
            string name;
            int index;

            Owner = null;
            Info = null;
            Index = -1;

            // navigate to last field
            for (int i = 0; i < parts.Length - 1; i++, owner = value)
            {
                (name, index) = GetNameAndIndex(parts[i]);

                // update info
                info = GetFieldOrProp(owner.GetType(), name);
                if (info == null)
                    return;

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
            Owner = owner;
            Info = info;
            Index = index;
        }

        public static (object Owner, object Info, int Index) FindMember<T>(this T me, string access)
        {
            FindMember(me, access, out object owner, out object info, out int index);
            return (owner, info, index);
        }

        /// LOCAL FUNCTIONS

        static (string, int) GetNameAndIndex(string part)
        {
            int idx = -1;
            var open = part.IndexOf('[');
            var str = part;

            if (open >= 0)
            {
                var close = part.IndexOf(']');
                if (close < 0)
                    throw new FormatException($"']' missing in '{part}'");
                idx = int.Parse(part.Subrange(open + 1, close));
                str = part.Substring(0, open);
            }

            return (str, idx);
        }

        static object GetFieldOrProp(Type type, string str)
        {
            object fieldInfo = type.GetField(str, flags);
            if (fieldInfo != null)
                return fieldInfo;

            fieldInfo = type.GetProperty(str, flags);
            if (fieldInfo != null)
                return fieldInfo;

            fieldInfo = type.GetFields(flags).FirstOrDefault(f => f.FieldType.Name == str);
            if (fieldInfo != null)
                return fieldInfo;

            fieldInfo = type.GetProperties(flags).FirstOrDefault(p => p.PropertyType.Name == str);
            if (fieldInfo != null)
                return fieldInfo;

            return null;
        }

        public static void SetValue<T>(this T obj, string access, object value)
        {
            var (owner, info, index) = obj.FindMember(access);

            switch (info)
            {
                case FieldInfo field:
                    SetValue(field, field.FieldType, owner, index, value);
                    break;
                case PropertyInfo prop:
                    SetValue(prop, prop.PropertyType, owner, index, value);
                    break;
            }
        }

        private static void SetValue(object field, Type fieldType, object owner, int index, object value)
        {
            var type = index >= 0 ? fieldType.GetElementType() : fieldType;

            if (type != value.GetType())
            {
                var ctr = type.GetConstructor(new[] { value.GetType() });
                if (ctr != null)
                    value = ctr.Invoke(new[] { value });
            }
            
            if (index >= 0)
            {
                var method = field.GetType().GetMethod("GetValue", flags, null,
                    CallingConventions.Any, new[] { typeof(object) }, null);
                var array = method.Invoke(field, new[] { owner }) as Array;
                array.SetValue(value, index);
            }
            else
            {
                var method = field.GetType().GetMethod("SetValue", flags, null,
                    CallingConventions.Any, new[] { typeof(object), typeof(object) }, null);
                method.Invoke(field, new[] { owner, value });
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
