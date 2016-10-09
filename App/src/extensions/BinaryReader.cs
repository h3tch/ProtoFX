using System.Runtime.InteropServices;

namespace System.IO
{
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
            // does the binary reader support reading this object type
            var Read = reader.GetType().GetMethod($"Read{typeof(T).Name}");
            if (Read == null)
                return null;

            // create array of type and find suitable read-method
            Array array = new T[rows, cols];
            int skip = stride - cols * Marshal.SizeOf<T>();

            // read types from mem and store them in the array
            for (int y = 0; y < rows; y++, reader.Seek(skip))
                for (int x = 0; x < cols; x++)
                    array.SetValue(Read.Invoke(reader, null), y, x);

            return array;
        }
    }

}
