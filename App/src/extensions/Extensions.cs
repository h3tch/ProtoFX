using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace App
{
    public static class ConvertExtensions
    {
        /// <summary>
        /// Convert array to byte array.
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        public static byte[] ToBytes(this Array src)
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

        public static int Size(this Array array)
            => Marshal.SizeOf(array.GetType().GetElementType()) * array.Length;

        /// <summary>
        /// Convert string into the specified type.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="str"></param>
        /// <param name="exeptionMessage"></param>
        /// <returns></returns>
        public static TResult To<TResult>(this string str, string exeptionMessage = null)
        {
            try
            {
                return (TResult)Convert.ChangeType(str, typeof(TResult), CultureInfo.CurrentCulture);
            }
            catch
            {
                if (exeptionMessage == null)
                    return default(TResult);
                throw new CompileException(exeptionMessage);
            }
        }

        /// <summary>
        /// Convert byte array into an array of the specified type.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="type"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Convert all array values to the specified return type.
        /// </summary>
        /// <typeparam name="TResult">The return type all values should be converted to.</typeparam>
        /// <param name="array"></param>
        /// <returns>Returns an array for converted values.</returns>
        public static TResult[] To<TResult>(this Array array)
        {
            TResult[] result = new TResult[array.Length];
            for (int i = 0; i < array.Length; i++)
                result[i] = (TResult)array.GetValue(i);
            return result;
        }

        /// <summary>
        /// Cast an array to another type, not by element, but by memory.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="typeName"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Array To(this Array data, string typeName, out Type type)
        {
            // convert input type to bytes
            var bytes = data.GetType().GetElementType() == typeof(byte) ? (byte[])data : data.ToBytes();
            // convert bytes to output type
            return bytes.To(type = str2type[typeName]);
        }

        /// <summary>
        /// Convert array to IEnumerable.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="a"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static IEnumerable<object> ToEnumerable(this Array a) => ForEach(a, new int[a.Rank]);

        /// <summary>
        /// Enumerate the elements of a multidimensional array.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="tmpIdx">Temporal array used to iterate through the array dimensions.</param>
        /// <param name="startDim">The first dimension to be processed.</param>
        /// <returns></returns>
        private static IEnumerable<object> ForEach(Array data, int[] tmpIdx, int startDim = 0)
        {
            // get size of current dimension
            int dimSize = data.GetLength(startDim);

            // for each element in this dimension
            for (int i = 0; i < dimSize; i++)
            {
                // set index of current dimension
                tmpIdx[startDim] = i;
                // if the array has another dimension
                if (data.Rank > startDim + 1)
                    // output all values of this dimension
                    foreach (var x in ForEach(data, tmpIdx, startDim + 1))
                        yield return x;
                else
                    // write value to output
                    yield return data.GetValue(tmpIdx);
            }
        }

        /// <summary>
        /// Covert the elements of an array into strings.
        /// </summary>
        /// <param name="src"></param>
        /// <param name="format">How to format the type into a string.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Covert the elements of an array of a specific type into and array of strings.
        /// </summary>
        /// <param name="src">Source array.</param>
        /// <param name="dst">Destination array of strings.</param>
        /// <param name="format">How to format the type into a string.</param>
        /// <param name="tmpIdx">Temporal array used to iterate through the array dimensions.</param>
        /// <param name="startDim">The first dimension to be processed.</param>
        private static void ToStringArray(Array src, Array dst, string format, int[] tmpIdx, int startDim = 0)
        {
            // get size of current dimension
            int dimSize = src.GetLength(startDim);

            // for each element in this dimension
            for (int i = 0; i < dimSize; i++)
            {
                // set index of current dimension
                tmpIdx[startDim] = i;
                // if the array has another dimension
                if (src.Rank > startDim + 1)
                    // output all values of this dimension
                    ToStringArray(src, dst, format, tmpIdx, startDim + 1);
                else
                    // write value to output
                    dst.SetValue(string.Format(CultureInfo.CurrentCulture, format, src.GetValue(tmpIdx)), tmpIdx);
            }
        }


        public static IEnumerable<Match> ToArray(this MatchCollection matches)
        {
            foreach (Match match in matches)
                yield return match;
        }

        public static IEnumerable<Match> NextSortedMatch(this IEnumerable<IEnumerator<Match>> matches, bool leftToRight = false)
        {
            matches.Select(x => x.MoveNext());
            matches = matches.Where(x => x.Current != null);

            for (matches = matches.Where(x => x.Current != null); 
                matches.Count() > 0;
                matches = matches.Where(x => x.Current != null))
            {
                IEnumerator<Match> best = null;
                foreach (var match in matches)
                {
                    var bestInx = best == null ? 0 : best.Current.Index;
                    if (leftToRight ? bestInx < match.Current.Index : match.Current.Index < bestInx)
                        best = match;
                }
                yield return best.Current;
                best.MoveNext();
            }
        }

        public static bool IsNull(this IEnumerator iter) => iter.Current != null;

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

    public static class MemoryExtensions
    {
        /// <summary>
        /// Copy the data from one memory position to another.
        /// </summary>
        /// <param name="src"></param>
        /// <param name="dst"></param>
        /// <param name="size"></param>
        public static void CopyTo(this IntPtr src, IntPtr dst, int size)
            => NativeMethods.CopyMemory(dst, src, (uint)size);

        internal static class NativeMethods
        {
            [DllImport("kernel32.dll", EntryPoint = "CopyMemory", SetLastError = false)]
            public static extern void CopyMemory(IntPtr dst, IntPtr src, uint count);
        }
    }
}