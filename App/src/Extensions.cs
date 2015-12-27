using System;
using System.Collections.Generic;
using System.IO;
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
        #endregion

        #region Extensions For All Types
        public static T UseIf<T>(this T obj, bool condition)
            => condition ? obj : default(T);

        public static void Do<T>(this T obj, Action<T> func)
            => func(obj);

        public static TResult Do<T, TResult>(this T obj, Func<T, TResult> func)
            => func(obj);
        #endregion

        #region IEnumerable<T> Extensions
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

        public static void Do<T>(this IEnumerable<T> ie, Action<T> func)
        {
            foreach (var e in ie)
                func(e);
        }
        #endregion

        #region Convert Types
        public static byte[] ToBytes(this Array src)
        {
            var ellType = src.GetType().GetElementType();
            var ellSize = ellType == typeof(char) ? 2 : Marshal.SizeOf(ellType);
            byte[] dst = new byte[ellSize * src.Length];
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

        public static TResult[] To<TResult>(this byte[] data)
            where TResult : struct
        {
            var ellSize = typeof(TResult) == typeof(char) ? 2 : Marshal.SizeOf(typeof(TResult));
            TResult[] rs = new TResult[(data.Length + ellSize - 1) / ellSize];
            Buffer.BlockCopy(data, 0, rs, 0, data.Length);
            return rs;
        }

        public static TResult[] To<TResult>(this IntPtr data, int size)
            where TResult : struct
        {
            var bytes = new byte[size];
            Marshal.Copy(data, bytes, 0, bytes.Length);
            return bytes.To<TResult>();
        }

        public static Array To(this Array data, string typeName, out Type type)
        {
            type = Data.str2type[typeName];
            var bytes = data.GetType().GetElementType() == typeof(byte) ?
                (byte[])data : data.ToBytes();

            // convert data to specified type
            switch (typeName)
            {
                case "char":   return bytes.To<char>();
                case "short":  return bytes.To<short>();
                case "ushort": return bytes.To<ushort>();
                case "int":    return bytes.To<int>();
                case "uint":   return bytes.To<uint>();
                case "long":   return bytes.To<long>();
                case "ulong":  return bytes.To<ulong>();
                case "float":  return bytes.To<float>();
                case "double": return bytes.To<double>();
            }

            throw new ArgumentException("ERROR: Could not convert buffer data to specified type.");
        }
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

        #region WinForm Control Extensions
        public static IEnumerable<Control> FindParent(this Control control, string name)
        {
            while (control.Parent != null)
            {
                control = control.Parent;
                if (control.Name == name)
                    yield return control;
            }
        }
        #endregion
    }
}