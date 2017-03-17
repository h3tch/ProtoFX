using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace App
{
    public static class ConvertExtensions
    {
        public static object DeepCopy(this object obj, Type outType = null)
        {
            if (obj == null)
                return null;

            var inType = obj.GetType();
            if (outType == null)
                outType = inType;

            // If the type of object is the value type, we will always get a new object when  
            // the original object is assigned to another variable. So if the type of the  
            // object is primitive or enum, we just return the object. We will process the  
            // struct type subsequently because the struct type may contain the reference  
            // fields. 
            // If the string variables contain the same chars, they always refer to the same  
            // string in the heap. So if the type of the object is string, we also return the  
            // object. 
            if (inType.IsPrimitive || inType.IsEnum || inType == typeof(string))
                return inType == outType ? obj : null;

            // If the type of the object is the Array, we use the CreateInstance method to get 
            // a new instance of the array. We also process recursively this method in the  
            // elements of the original array because the type of the element may be the reference  
            // type. 
            else if (inType.IsArray)
            {
                var array = obj as Array;
                outType = outType.GetElementType();
                var copy = Array.CreateInstance(outType, array.Length);

                for (int i = 0; i < array.Length; i++)
                    // Get the deep clone of the element in the original
                    // array and assign the   clone to the new array. 
                    copy.SetValue(array.GetValue(i).DeepCopy(outType), i);
                
                return copy;
            }

            // If the type of the object is class or struct, it may contain the reference fields,  
            // so we use reflection and process recursively this method in the fields of the object  
            // to get the deep clone of the object.  
            // We use Type.IsValueType method here because there is no way to indicate directly 
            // whether  the Type is a struct type. 
            else if (inType.IsClass || inType.IsValueType)
            {
                var copy = Activator.CreateInstance(outType);
                var flags =
                    BindingFlags.Public |
                    BindingFlags.NonPublic |
                    BindingFlags.Instance;

                // Copy all fields. 
                var inFields = inType.GetFields(flags);
                var outFields = outType.GetFields(flags);
                for (int i = 0; i < inFields.Length; i++)
                {
                    var inField = inFields[i];
                    var outField = outFields[i];
                    var value = inField.GetValue(obj).DeepCopy(outField.FieldType);
                    outField.SetValue(copy, value);
                }

                return copy;
            }
            else
                throw new ArgumentException("The object has an unknown type");
        }

        public static TResult GetAttribute<TResult>(this Type type)
            where TResult : Attribute
        {
            return ((TResult)Attribute.GetCustomAttribute(type, typeof(TResult)));
        }

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
        {
            return Marshal.SizeOf(array.GetType().GetElementType()) * array.Length;
        }

        public static T Fetch<T>(this T[] array, int index)
        {
            return index <= 0 && index < array.Length ? array[index] : default(T);
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
            var result = new TResult[array.Length];
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
        public static (Array, Type) To(this Array data, string typeName)
        {
            // convert input type to bytes
            var bytes = data.GetType().GetElementType() == typeof(byte) ? (byte[])data : data.ToBytes();
            // convert bytes to output type
            var type = str2type[typeName];
            var array = bytes.To(type);
            return (array, type);
        }

        /// <summary>
        /// Convert array to IEnumerable.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="a"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static IEnumerable<object> ToEnumerable(this Array a)
        {
            return ForEach(a, new int[a.Rank]);
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
                    dst.SetValue(string.Format(CultureInfo.CurrentCulture, format, src.GetValue(tmpIdx)), tmpIdx);
            }
        }

        /// <summary>
        /// Convert MatchCollection to an array.
        /// </summary>
        /// <param name="matches"></param>
        /// <returns></returns>
        public static IEnumerable<Match> ToArray(this MatchCollection matches)
        {
            foreach (Match match in matches)
                yield return match;
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

    public static class MemoryExtensions
    {
        /// <summary>
        /// Copy the data from one memory position to another.
        /// </summary>
        /// <param name="src"></param>
        /// <param name="dst"></param>
        /// <param name="size"></param>
        public static void CopyTo(this IntPtr src, IntPtr dst, int size)
        {
            NativeMethods.CopyMemory(dst, src, (uint)size);
        }

        internal static class NativeMethods
        {
            [DllImport("kernel32.dll", EntryPoint = "CopyMemory", SetLastError = false)]
            public static extern void CopyMemory(IntPtr dst, IntPtr src, uint count);
        }
    }
}