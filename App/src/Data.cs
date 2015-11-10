using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace App
{
    public static class Data
    {
        public static Dictionary<string, Type> str2type = new Dictionary<string, Type>
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

        public static Array Convert(Array data, string typeName, out Type type)
        {
            type = str2type[typeName];
            var bytes = data.GetType().GetElementType() == typeof(byte) ?
                (byte[])data : Convert(data);

            // convert data to specified type
            switch (typeName)
            {
                case "byte":   return Convert<byte>  (bytes);
                case "short":  return Convert<short> (bytes);
                case "ushort": return Convert<ushort>(bytes);
                case "int":    return Convert<int>   (bytes);
                case "uint":   return Convert<uint>  (bytes);
                case "long":   return Convert<long>  (bytes);
                case "ulong":  return Convert<ulong> (bytes);
                case "float":  return Convert<float> (bytes);
                case "double": return Convert<double>(bytes);
            }

            throw new GLException("ERROR: Could not convert buffer data to specified type.");
        }

        public static TResult[] Convert<TResult>(Array data)
            where TResult : struct
        {
            return Convert<TResult>(Convert(data));
        }

        public static TResult[] Convert<TResult>(byte[] data)
            where TResult : struct
        {
            TResult[] rs = new TResult[data.Length / Marshal.SizeOf(typeof(TResult))];
            Buffer.BlockCopy(data, 0, rs, 0, data.Length);
            return rs;
        }

        public static byte[] Convert(Array src)
        {
            byte[] dst = new byte[Marshal.SizeOf(src.GetType().GetElementType()) * src.Length];
            Buffer.BlockCopy(src, 0, dst, 0, dst.Length);
            return dst;
        }

        public static IEnumerable<T> Join<T>(IEnumerable<T[]> list)
        {
            foreach (var el in list)
                foreach (var e in el)
                    yield return e;
        }

        public static T ParseType<T>(string arg, string info)
        {
            try
            {
                return (T)System.Convert.ChangeType(arg, typeof(T), App.culture);
            }
            catch
            {
                throw new GLException(info);
            }
        }
    }
}
