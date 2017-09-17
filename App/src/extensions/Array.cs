using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace System
{
    public static class ArrayExtensions
    {
        /// <summary>
        /// Get the size of the array in bytes.
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public static int Size(this Array array)
        {
            return array.GetElementSize() * array.Length;
        }

        /// <summary>
        /// Get the element type of the array.
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public static Type GetElementType(this Array array)
        {
            return array.GetType().GetElementType();
        }

        /// <summary>
        /// Get the size of an element in bytes.
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public static int GetElementSize(this Array array)
        {
            var type = array.GetElementType();
            return type == typeof(char) ? 2 : Marshal.SizeOf(type);
        }

        /// <summary>
        /// Fetch the array element if it exists, otherwise return the default value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static T Fetch<T>(this T[] array, int index)
        {
            return 0 <= index && index < array.Length ? array[index] : default(T);
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
        /// Convert an array byte-wise from to the specified type.
        /// </summary>
        /// <param name="src">The source array.</param>
        /// <param name="dstType">The element type of the destination array.</param>
        /// <returns>The output array.</returns>
        public static Array To(this Array src, Type dstType)
        {
            // get element type size of source and destination array
            var srcSize = src.GetElementSize();
            var dstSize = dstType == typeof(char) ? 2 : Marshal.SizeOf(dstType);

            // copy array to unmanaged memory
            var bufSize = src.Size();
            var ptr = Marshal.AllocHGlobal(bufSize);
            for (int i = 0, offset = 0; i < src.Length; i++, offset += srcSize)
                Marshal.StructureToPtr(src.GetValue(i), ptr + offset, false);

            return ptr.To(bufSize, dstType);
        }

        public static Array To(this IntPtr src, int bufSize, Type dstType)
        {
            var dstSize = dstType == typeof(char) ? 2 : Marshal.SizeOf(dstType);

            // allocate destination array
            var dst = Array.CreateInstance(dstType, (bufSize + dstSize - 1) / dstSize);

            // copy unmanaged data to the destination array
            for (int i = 0, offset = 0; i < dst.Length; i++, offset += dstSize)
                dst.SetValue(Marshal.PtrToStructure(src + offset, dstType), i);

            return dst;
        }

        public static Array Reshape(this Array src, params int[] lengths)
        {
            // allocate destination array
            var dst = Array.CreateInstance(src.GetElementType(), lengths);

            var srcidx = new int[src.Rank];
            var dstidx = new int[dst.Rank];
            for (int i = 0; i < dst.Length; i++)
                dst.SetValueByIndex(src.GetValueByIndex(i, srcidx), i, dstidx);

            return dst;
        }

        private static void SetValueByIndex(this Array me, object value, int index, int[] indices)
        {
            for (int d = me.Rank - 1; d >= 0; d--)
            {
                var l = me.GetLength(d);
                indices[d] = index % l;
                index /= l;
            }
            me.SetValue(value, indices);
        }

        private static object GetValueByIndex(this Array me, int index, int[] indices)
        {
            for (int d = me.Rank - 1; d >= 0; d--)
            {
                var l = me.GetLength(d);
                indices[d] = index % l;
                index /= l;
            }
            return me.GetValue(indices);
        }

        /// <summary>
        /// Convert an array byte-wise from to the specified type.
        /// </summary>
        /// <param name="array"></param>
        /// <param name="typeName"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Array To(this Array array, string typeName, out Type type)
        {
            return array.To(type = TypeExtensions.Str2Type[typeName]);
        }

        /// <summary>
        /// Convert an array byte-wise from to the specified type.
        /// </summary>
        /// <typeparam name="U"></typeparam>
        /// <param name="array"></param>
        /// <returns></returns>
        public static U[] To<U>(this Array array)
        {
            return (U[])array.To(typeof(U));
        }

    }
}
