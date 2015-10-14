using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace App
{
    class Data
    {
        public static Array Convert(byte[] data, string type, out Type T)
        {
            // convert data to specified type
            switch (type)
            {
                case "byte":
                    T = typeof(byte);
                    return Convert<byte>(data);
                case "short":
                    T = typeof(short);
                    return Convert<short>(data);
                case "ushort":
                    T = typeof(ushort);
                    return Convert<ushort>(data);
                case "int":
                    T = typeof(int);
                    return Convert<int>(data);
                case "uint":
                    T = typeof(uint);
                    return Convert<uint>(data);
                case "long":
                    T = typeof(long);
                    return Convert<long>(data);
                case "ulong":
                    T = typeof(ulong);
                    return Convert<ulong>(data);
                case "float":
                    T = typeof(float);
                    return Convert<float>(data);
                case "double":
                    T = typeof(double);
                    return Convert<double>(data);
            }

            throw new GLException("INTERNAL_ERROR: Could not convert buffer data to specified type.");
        }

        public static Array Convert<T>(byte[] data)
        {
            // find method to convert the data
            var methods = from m in typeof(BitConverter).GetMethods()
                          where m.Name == "To" + typeof(T).Name
                          select m;
            if (methods.Count() == 0)
                return data;

            var method = methods.First();

            // allocate array
            int typesize = Marshal.SizeOf(typeof(T));
            Array rs = Array.CreateInstance(typeof(T), data.Length / typesize);

            // convert data
            for (int i = 0; i < rs.Length; i++)
                rs.SetValue(System.Convert.ChangeType(method.Invoke(null, new object[] { data, typesize * i }), typeof(T)), i);

            return rs;
        }

        public static T[] Join<T>(IEnumerable<T[]> list, int maxSize = 0)
        {
            // if size has not been specified,
            // compute the summed size of all file data
            if (maxSize == 0)
            {
                maxSize = 0;
                foreach (T[] b in list)
                    maxSize += b != null ? b.Length : 0;
            }

            // copy file data to byte array
            T[] data = new T[maxSize];

            int start = 0;
            foreach (T[] b in list)
            {
                if (b != null)
                {
                    Array.Copy(b, 0, data, start, Math.Min(data.Length - start, b.Length));
                    start += b.Length;
                }
            }

            return data;
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

        public static bool TryParseType<T>(object obj, ref T output)
        {
            try
            {
                output = (T)System.Convert.ChangeType(obj, typeof(T), App.culture);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
