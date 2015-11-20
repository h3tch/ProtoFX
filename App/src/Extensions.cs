using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace App
{
    /// <summary>
    /// This class extends several other classes with useful methods
    /// that are not (yet) provided by the .Net framework.
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Find tab in tab collection by the file path attached to it.
        /// </summary>
        /// <param name="tab">
        /// The object which receives this extension method is the TabPageCollection type.</param>
        /// <param name="path">
        /// The path to search for.</param>
        /// <returns>The tab index or -1 if the tab could not be found.</returns>
        public static int Find(this TabControl.TabPageCollection tab, string path)
        {
            for (int i = 0; i < tab.Count; i++)
                if (((TabPage)tab[i]).filepath == path)
                    return i;
            return -1;
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

        public static IEnumerable<T> Join<T>(this IEnumerable<T[]> list)
        {
            foreach (var el in list)
                foreach (var e in el)
                    yield return e;
        }

        public static string Join(this IEnumerable<string> list, string separator)
        {
            string str = "";
            foreach (var s in list)
                str += s + separator;
            return str;
        }

        public static T UseIf<T>(this T obj, bool condition)
            => condition ? obj : default(T);

        public static void Do<T>(this IEnumerable<T> ie, Action<T> func)
        {
            foreach (var e in ie)
                func(e);
        }

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
                throw new GLException(exeptionMessage);
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
    }
}
