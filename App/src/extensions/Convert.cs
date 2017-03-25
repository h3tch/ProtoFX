using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace App.Extensions
{
    public static class ConvertExtensions
    {

        private static byte[] Array2Bytes(Array src)
        {
            // get source type size
            var ellType = src.GetType().GetElementType();
            var ellSize = ellType == typeof(char) ? 2 : Marshal.SizeOf(ellType);
            // allocate byte array
            var dst = new byte[ellSize * src.Length];
            // copy source data to output array
            Buffer.BlockCopy(src, 0, dst, 0, dst.Length);

            return dst;
        }

        private static Array Bytes2Array(byte[] src, Type type)
        {
            // get the size of the output type
            var ellSize = type == typeof(char) ? 2 : Marshal.SizeOf(type);
            // allocate output array
            var dst = Array.CreateInstance(type, (src.Length + ellSize - 1) / ellSize);
            // copy data to output array
            Buffer.BlockCopy(src, 0, dst, 0, dst.Length);
            return dst;
        }

        public static Array To(this Array src, Type dstType)
        {
            var srcSize = src.ElementSize();
            var dstSize = dstType == typeof(char) ? 2 : Marshal.SizeOf(dstType);

            var ptr = Marshal.AllocHGlobal(src.Size());
            for (int i = 0, offset = 0; i < src.Length; i++, offset += srcSize)
                Marshal.StructureToPtr(src.GetValue(i), ptr + offset, false);

            var dst = Array.CreateInstance(dstType, (src.Size() + dstSize - 1) / dstSize);

            for (int i = 0, offset = 0; i < dst.Length; i++, offset += dstSize)
                dst.SetValue(Marshal.PtrToStructure(ptr + offset, dstType), i);

            return dst;
        }

        public static Array To(this Array array, string typeName, out Type type)
        {
            type = str2type[typeName];
            return array.To(type);
        }

        public static Array To(this Array array, string typeName)
        {
            return array.To(typeName, out Type type);
        }

        public static U[] To<U>(this Array array)
        {
            return (U[])array.To(typeof(U));
        }

        public static U[] To<U>(this string str)
        {
            return str.ToCharArray().To<U>();
        }

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
    }
}
