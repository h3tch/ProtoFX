using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;

namespace App
{
    /// <summary>
    /// This class extends several other classes with useful methods
    /// that are not (yet) provided by the .Net framework.
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Check if the object value matches the default value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns>Returns true if the object value equals the default value.</returns>
        public static bool IsDefault<T>(this T obj) => obj.Equals(default(T));

        /// <summary>
        /// Check if the class is of any of the specified types.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="types"></param>
        /// <returns></returns>
        public static bool TypeIs<T>(this T obj, Type[] types) => types.Any(x => x is T);
    }

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
            byte[] dst = new byte[ellSize * src.Length];
            // copy source data to output array
            Buffer.BlockCopy(src, 0, dst, 0, dst.Length);
            return dst;
        }

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
                return (TResult)Convert.ChangeType(str, typeof(TResult), App.Culture);
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
        /// Process each element using the specified functions.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="data"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static IEnumerable<TResult> ForEach<TResult>(this Array data, Func<object, TResult> func)
        {
            // output all values
            foreach (var x in ForEach(data, new int[data.Rank]))
                yield return func(x);
        }

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
                    dst.SetValue(string.Format(App.Culture, format, src.GetValue(tmpIdx)), tmpIdx);
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
    }

    public static class BinaryReaderExtensions
    {
        /// <summary>
        /// Seek to the specified position in the binary stream.
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="offset"></param>
        /// <param name="origin"></param>
        /// <returns></returns>
        public static long Seek(this BinaryReader reader, int offset, SeekOrigin origin = SeekOrigin.Current)
            => reader.BaseStream.Seek(offset, origin);

        /// <summary>
        /// Read a 2D array from a binary stream.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="reader"></param>
        /// <param name="rows"></param>
        /// <param name="cols"></param>
        /// <param name="stride"></param>
        /// <returns></returns>
        public static Array ReadArray<T>(this BinaryReader reader, int rows, int cols, int stride)
            where T : struct
        {
            // create array of type and find suitable read-method
            Array array = new T[rows, cols];
            var Read = reader.GetType().GetMethod("Read" + typeof(T).Name);
            int skip = stride - cols * Marshal.SizeOf<T>();

            // read types from mem and store them in the array
            for (int y = 0; y < rows; y++, reader.Seek(skip))
                for (int x = 0; x < cols; x++)
                    array.SetValue(Read.Invoke(reader, null), y, x);

            return array;
        }
    }

    public static class IEnumerableExtensions
    {
        /// <summary>
        /// Find first zero based index where the specified function returns true.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ie"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static int IndexOf<T>(this IEnumerable<T> ie, Func<T, bool> func, int startIndex = 0)
        {
            int i = 0;
            foreach (var e in ie.Skip(startIndex))
            {
                if (func(e))
                    return i;
                i++;
            }
            return -1;
        }

        /// <summary>
        /// Find last zero based index where the specified function returns true.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ie"></param>
        /// <param name="func"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Return the first element or a default value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ie"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static T FirstOr<T>(this IEnumerable<T> ie, T defaultValue)
        {
            T rs = ie.FirstOrDefault();
            return rs.IsDefault() ? defaultValue : rs;
        }

        /// <summary>
        /// Process each object of the list using the specified
        /// function and return all thrown exceptions.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ie"></param>
        /// <param name="func"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Concatenate a list of arrays.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public static IEnumerable<T> Cat<T>(this IEnumerable<IEnumerable<T>> id)
        {
            foreach (var el in id)
            {
                if (el != null)
                    foreach (var e in el)
                        yield return e;
            }
        }

        /// <summary>
        /// Concatenate a list of strings into a single string.
        /// </summary>
        /// <param name="list"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static string Cat(this IEnumerable<string> list, string separator)
        {
            var sepLen = separator.Length;
            var build = new StringBuilder(list.Sum(x => x.Length + sepLen));
            foreach (var s in list)
                build.Append(s + separator);
            return build.ToString(0, Math.Max(0, build.Length - 1));
        }

        /// <summary>
        /// Iterate through two lists simultaneously.
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="ie"></param>
        /// <param name="other"></param>
        /// <param name="func"></param>
        public static void Zip<T1, T2>(this IEnumerable<T1> ie, IEnumerable<T2> other, Action<T1, T2> func)
        {
            var enumerator = other.GetEnumerator();
            enumerator.MoveNext();
            foreach (var e in ie)
            {
                func(e, enumerator.Current);
                enumerator.MoveNext();
            }
        }

        /// <summary>
        /// Process each element of a list.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ie"></param>
        /// <param name="func"></param>
        public static void ForEach<T>(this IEnumerable<T> ie, Action<T> func)
        {
            foreach (var e in ie)
                func(e);
        }

        public static T MaxOr<T>(this IEnumerable<T> ie, T defaultValue)
            => ie.Count() > 0 ? ie.Max() : defaultValue;
    }

    public static class StringExtensions
    {
        /// <summary>
        /// Find first match of two matching braces.
        /// </summary>
        /// <param name="s">extend string class</param>
        /// <param name="open">opening brace character</param>
        /// <param name="close">closing brace character</param>
        /// <returns>Returns the first brace match.</returns>
        public static Match BraceMatch(this string s, char open, char close)
        {
            string oc = $"{open}{close}";
            return Regex.Match(s, $"{open}[^{oc}]*(((?<Open>{open})[^{oc}]*)+" +
                $"((?<Close-Open>{close})[^{oc}]*)+)*(?(Open)(?!)){close}");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <param name="open"></param>
        /// <param name="close"></param>
        /// <returns></returns>
        public static int IndexOfBraceMatch(this string s, char open, char close)
            => IndexOfBraceMatch(s, open, close, 0, s.Length);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <param name="open"></param>
        /// <param name="close"></param>
        /// <param name="startIndex"></param>
        /// <returns></returns>
        public static int IndexOfBraceMatch(this string s, char open, char close, int startIndex)
            => IndexOfBraceMatch(s, open, close, startIndex, s.Length - startIndex);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <param name="open"></param>
        /// <param name="close"></param>
        /// <param name="startPosition"></param>
        /// <returns></returns>
        public static int IndexOfBraceMatch(this string s, char open, char close, int startIndex, int count)
        {
            int i = s.IndexOf(open, startIndex, count);
            if (i >= 0)
            {
                for (int n = 0, I = startIndex + count; i < I; i++)
                {
                    if (s[i] == open)
                        n++;
                    else if (s[i] == close)
                        n--;
                    if (n == 0)
                        return i;
                }
            }

            return -1;
        }
        public static int IndexOfWholeWords(this string s, string words)
            => s.IndexOfWholeWords(words, 0, s.Length);

        public static int IndexOfWholeWords(this string s, string words, int startIndex)
            => s.IndexOfWholeWords(words, startIndex, s.Length - startIndex);

        public static int IndexOfWholeWords(this string s, string words, int startIndex, int count)
        {
            for (int i = startIndex, endIndex = i + count - words.Length; i < endIndex; i++)
            {
                if (i > 0 ? char.IsLetter(s[i - 1]) : false)
                    continue;
                int j = 0;
                while (j < words.Length && s[i + j] == words[j])
                    j++;
                if (j < words.Length)
                    continue;
                if (i + j >= endIndex || !char.IsLetterOrDigit(s[i + j]))
                    return i;
            }
            return -1;
        }

        public static bool IsWholeWords(this string s, int startIndex, int count)
            => (startIndex == 0 || !char.IsLetter(s[startIndex - 1]))
                && (startIndex + count >= s.Length || !char.IsLetterOrDigit(s[startIndex + count]));

        /// <summary>
        /// Get zero based line index from the zero based character position.
        /// </summary>
        /// <param name="s">extend string class</param>
        /// <param name="position">zero based character position</param>
        /// <returns>Returns the zero based line index.</returns>
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

        /// <summary>
        /// Find index of the specified character beginning at the startIndex.
        /// If the character could not be found, the length of the string is
        /// returned -- not -1.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="c"></param>
        /// <param name="startIndex">The zero based index to start searching at.</param>
        /// <returns>Returns the position of the first found character location
        /// or the length of the string if the character could not be found.</returns>
        public static int IndexOfOrLength(this string s, char c, int startIndex = 0)
            => (int)Math.Min((uint)s.Length, (uint)s.IndexOf(c, startIndex));

        /// <summary>
        /// The sub range of the string beginning at the zero based start index
        /// and ending at the zero based end index.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="start">Zero based start index.</param>
        /// <param name="end">Zero based end index.</param>
        /// <returns></returns>
        public static string Subrange(this string s, int start, int end)
            => s.Substring(start, end - start);

        /// <summary>
        /// Get the word surrounding the specified position.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        public static string WordFromPosition(this string s, int position)
        {
            int start = s.WordStartPosition(position);
            int end = s.WordEndPosition(position);
            int len = end - start;
            return len > 0 ? s.Substring(start, len) : string.Empty;
        }

        /// <summary>
        /// Get the word surrounding the specified position.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        public static int WordStartPosition(this string s, int position)
        {
            while (position > 1 && !char.IsWhiteSpace(s[position - 1]))
                position--;
            return position;
        }

        /// <summary>
        /// Get the word surrounding the specified position.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        public static int WordEndPosition(this string s, int position)
        {
            while (position < s.Length && !char.IsWhiteSpace(s[position]))
                position++;
            return position;
        }
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