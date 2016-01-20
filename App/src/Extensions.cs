using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace App
{
    /// <summary>
    /// This class extends several other classes with useful methods
    /// that are not (yet) provided by the .Net framework.
    /// </summary>
    public static class Extensions
    {
        public static int IndexOf(this TabControl.TabPageCollection tab, string path)
        {
            for (int i = 0; i < tab.Count; i++)
                if (((TabPage)tab[i]).filepath == path)
                    return i;
            return -1;
        }

        #region String Extensions
        public static Match BraceMatch(this string s, char open, char close)
        {
            string oc = "" + open + close;
            return Regex.Match(s, $"{open}[^{oc}]*(((?<Open>{open})[^{oc}]*)+" +
                $"((?<Close-Open>{close})[^{oc}]*)+)*(?(Open)(?!)){close}");
        }

        public static int LineFromPosition(this string s, int position)
        {
            int lineCount = 0;

            for (int i = 0; i < position; i++)
            {
                if (s[i] == '\n')
                {
                    lineCount++;
                }
                else if (s[i] == '\r')
                {
                    if (s[i + 1] == '\n')
                        i++;
                    lineCount++;
                }
            }

            return lineCount;
        }

        public static int PositionFromLine(this string s, int line)
        {
            int i = 0;

            for (int l = 0; i < s.Length && l < line; i++)
            {
                if (s[i] == '\n')
                {
                    l++;
                }
                else if (s[i] == '\r')
                {
                    if (s[i + 1] == '\n')
                        i++;
                    l++;
                }
            }

            return i;
        }
        #endregion

        #region Extensions For All Types
        public static T UseIf<T>(this T obj, bool condition) => condition ? obj : default(T);

        public static bool IsDefault<T>(this T obj) => obj.Equals(default(T));
        #endregion

        #region IEnumerable<T> Extensions
        public static T FindOrDefault<T>(this IEnumerable<T> ie, Func<T, bool> func)
        {
            int i = 0;
            foreach (var e in ie)
            {
                if (func(e))
                    return e;
                i++;
            }
            return default(T);
        }

        public static int IndexOf<T>(this IEnumerable<T> ie, Func<T, bool> func)
        {
            int i = 0;
            foreach (var e in ie)
            {
                if (func(e))
                    return i;
                i++;
            }
            return -1;
        }

        public static int LastIndexOf<T>(this IEnumerable<T> ie, Func<T, bool> func)
        {
            int i = 0, rs = -1;
            foreach (var e in ie)
            {
                if (func(e))
                    rs = i;
                i++;
            }
            return rs;
        }

        public static IEnumerable<Exception> Catch<T>(this IEnumerable<T> ie, Action<T> func)
        {
            foreach (var e in ie)
            {
                Exception ex = null;
                try
                {
                    func(e);
                }
                catch (Exception x)
                {
                    ex = x;
                }
                if (ex != null)
                    yield return ex;
            }
        }

        public static IEnumerable<T> Join<T>(this IEnumerable<T[]> id)
        {
            foreach (var el in id)
                foreach (var e in el)
                    yield return e;
        }

        public static IEnumerable<T> Merge<T>(this IEnumerable<T> ie, IEnumerable<T> other)
        {
            foreach (var el in ie)
                yield return el;
            foreach (var el in other)
                yield return el;
        }

        public static string Merge(this IEnumerable<string> list, string separator)
        {
            string str = "";
            foreach (var s in list)
                str += s + separator;
            return str;
        }

        public static void Zip<T1,T2>(this IEnumerable<T1> ie, IEnumerable<T2> other, Action<T1,T2> func)
        {
            var enumerator = other.GetEnumerator();
            enumerator.MoveNext();
            foreach (var e in ie)
            {
                func(e, enumerator.Current);
                enumerator.MoveNext();
            }
        }

        public static void Do<T>(this IEnumerable<T> ie, Action<T> func)
        {
            foreach (var e in ie)
                func(e);
        }
        #endregion

        #region Convert Types
        public static byte[] ToBytes(this Array src)
        {
            // get source type size
            var ellType = src.GetType().GetElementType();
            var ellSize = ellType == typeof(char) ? 2 : Marshal.SizeOf(ellType);
            // allocate byte array
            byte[] dst = new byte[ellSize * src.Length];
            // copy source data to output array
            Buffer.BlockCopy(src, 0, dst, 0, dst.Length);
            return dst;
        }

        public static TResult To<TResult>(this string str, string exeptionMessage = null)
        {
            try
            {
                return (TResult)Convert.ChangeType(str, typeof(TResult), App.culture);
            }
            catch
            {
                if (exeptionMessage == null)
                    return default(TResult);
                throw new CompileException(exeptionMessage);
            }
        }

        public static Array To(this byte[] data, Type type)
        {
            // get the size of the output type
            var ellSize = type == typeof(char) ? 2 : Marshal.SizeOf(type);
            // allocate output array
            var rs = Array.CreateInstance(type, (data.Length + ellSize - 1) / ellSize);
            // copy data to output array
            Buffer.BlockCopy(data, 0, rs, 0, data.Length);
            return rs;
        }

        public static TResult[] To<TResult>(this byte[] data) => (TResult[])data.To(typeof(TResult));

        public static TResult[] To<TResult>(this IntPtr data, int size)
        {
            // copy input data to byte array
            var bytes = new byte[size];
            Marshal.Copy(data, bytes, 0, bytes.Length);
            // convert bytes to output type
            return bytes.To<TResult>();
        }

        public static Array To(this Array data, string typeName, out Type type)
        {
            // convert input type to bytes
            var bytes = data.GetType().GetElementType() == typeof(byte) ? (byte[])data : data.ToBytes();
            // convert bytes to output type
            return bytes.To(type = str2type[typeName]);
        }

        public static IEnumerable<TResult> ForEach<TResult>(this Array src, Func<object,TResult> func)
        {
            // output all values
            foreach (var x in ForEach(src, new int[src.Rank]))
                yield return func(x);
        }

        public static IEnumerable<object> ForEach(Array src, int[] idx, int curDim = 0)
        {
            // get size of current dimension
            int dimSize = src.GetLength(curDim);

            // for each element in this dimension
            for (int i = 0; i < dimSize; i++)
            {
                // set index of current dimension
                idx[curDim] = i;
                // if the array has another dimension
                if (src.Rank > curDim + 1)
                    // output all values of this dimension
                    foreach (var x in ForEach(src, idx, curDim + 1))
                        yield return x;
                else
                    // write value to output
                    yield return src.GetValue(idx);
            }
        }

        public static Array ToStringArray(this Array src, string format)
        {
            // get array size for each dimension
            var size = Enumerable.Range(0, src.Rank).Select(x => src.GetLength(x)).ToArray();
            // allocate string array
            var dst = Array.CreateInstance(typeof(string), size);
            // convert each element to a string
            ToStringArray(src, dst, format, new int[src.Rank]);
            return dst;
        }

        private static void ToStringArray(Array src, Array dst, string dstFomrat, int[] idx, int curDim = 0)
        {
            // get size of current dimension
            int dimSize = src.GetLength(curDim);

            // for each element in this dimension
            for (int i = 0; i < dimSize; i++)
            {
                // set index of current dimension
                idx[curDim] = i;
                // if the array has another dimension
                if (src.Rank > curDim + 1)
                    // output all values of this dimension
                    ToStringArray(src, dst, dstFomrat, idx, curDim + 1);
                else
                    // write value to output
                    dst.SetValue(string.Format(App.culture, dstFomrat, src.GetValue(idx)), idx);
            }
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
        #endregion

        #region Copy Extensions
        public static void CopyTo(this IntPtr src, IntPtr dst, int size)
            => CopyMemory(dst, src, (uint)size);

        [DllImport("kernel32.dll", EntryPoint = "CopyMemory", SetLastError = false)]
        private static extern void CopyMemory(IntPtr dest, IntPtr src, uint count);
        #endregion

        #region BinaryReader Extensions
        public static long Seek(this BinaryReader reader, int offset, SeekOrigin origin = SeekOrigin.Current)
            => reader.BaseStream.Seek(offset, origin);

        public static Array ReadArray<T>(this BinaryReader reader, int rows, int cols, int stride)
            where T : struct
        {
            // create array of type and find suitable read-method
            Array array = new T[rows, cols];
            var Read = reader.GetType().GetMethod("Read" + typeof(T).Name);
            int skip = stride - cols * Marshal.SizeOf<T>();

            // read types from mem and store them in the array
            for (int y = 0; y < rows; y++)
                for (int x = 0; x < cols; x++, reader.Seek(skip))
                    array.SetValue(Read.Invoke(reader, null), y, x);

            return array;
        }
        #endregion
    }
}