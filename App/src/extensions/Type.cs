using System.Collections.Generic;
using System.Reflection;

namespace System
{
    public static class TypeExtensions
    {
        public static TResult GetAttribute<TResult>(this Type type)
            where TResult : Attribute
        {
            return ((TResult)Attribute.GetCustomAttribute(type, typeof(TResult)));
        }

        public static object GetAttribute(this Type type, string attrName,
            StringComparison comparisonType = StringComparison.CurrentCulture)
        {
            foreach (var attr in Attribute.GetCustomAttributes(type))
            {
                if (attr.GetType().Name.Equals(attrName, comparisonType))
                    return attr;
            }
            return null;
        }

        public static object GetAttribute(this TypeInfo type, string attrName,
            StringComparison comparisonType = StringComparison.CurrentCulture)
        {
            foreach (var attr in type.GetCustomAttributes(true))
            {
                if (attr.GetType().Name.Equals(attrName, comparisonType))
                    return attr;
            }
            return null;
        }

        public static Dictionary<string, Type> Str2Type = new Dictionary<string, Type>
        {
            {"bool"    , typeof(bool)   },
            {"byte"    , typeof(byte)   },
            {"sbyte"   , typeof(sbyte)  },
            {"char"    , typeof(char)   },
            {"decimal" , typeof(decimal)},
            {"double"  , typeof(double) },
            {"float"   , typeof(float)  },
            {"int"     , typeof(int)    },
            {"uint"    , typeof(uint)   },
            {"long"    , typeof(long)   },
            {"ulong"   , typeof(ulong)  },
            {"object"  , typeof(object) },
            {"short"   , typeof(short)  },
            {"ushort"  , typeof(ushort) },
        };
    }
}
