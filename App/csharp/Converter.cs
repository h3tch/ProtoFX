using OpenTK;
using System;
using System.Runtime.InteropServices;

namespace data
{
    class Converter
    {
        private const float rad2deg = (float)(Math.PI / 180);

        /// <summary>
        /// Converts a byte array defined as
        /// struct {
        ///     vec4 position[cameraCount],
        ///          rotation[cameraCount],
        ///          projection[cameraCount];
        /// }
        /// into an array of view-projection matrices.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] Convert2ViewProjMatrix(byte[] data)
        {
            return Convert2<float, Matrix4>(data, 3, 4, (v, i) => {
                var near = v[2, i, 0];
                var far = v[2, i, 1];
                var fov = v[2, i, 2];
                var aspect = v[2, i, 3];
                var view = Matrix4.CreateTranslation(-v[0, i, 0], -v[0, i, 1], -v[0, i, 2])
                     * Matrix4.CreateRotationY(-v[1, i, 1] * rad2deg)
                     * Matrix4.CreateRotationX(-v[1, i, 0] * rad2deg);
                var proj = Matrix4.CreatePerspectiveFieldOfView(fov * rad2deg, aspect, near, far);
                return view * proj;
            });
        }

        /// <summary>
        /// Converts a byte array defined as
        /// struct {
        ///     vec4 position[count],
        ///          rotation[count],
        ///          scale[count];
        /// }
        /// into an array of model matrices.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] Convert2ModelMatrix(byte[] data)
        {
            return Convert2<float, Matrix4>(data, 3, 4, (v, i) =>
                Matrix4.CreateTranslation(v[0, i, 0], v[0, i, 1], v[0, i, 2])
                * Matrix4.CreateRotationX(v[1, i, 2] * rad2deg)
                * Matrix4.CreateRotationY(v[1, i, 1] * rad2deg)
                * Matrix4.CreateRotationX(v[1, i, 0] * rad2deg)
                * Matrix4.CreateScale(v[2, i, 0], v[2, i, 1], v[2, i, 2]));
        }

        /// <summary>
        /// Convert a byte array into another byte array by
        /// processing the original data with the specified function.
        /// </summary>
        /// <typeparam name="T1">Type stored in the data array.</typeparam>
        /// <typeparam name="T2">Return type of the specified function.</typeparam>
        /// <param name="data"></param>
        /// <param name="arrayCount">Number of arrays stored in data.</param>
        /// <param name="vectorSize">Size of the vectors stored in data.</param>
        /// <param name="func">Function to process the source data.</param>
        /// <returns></returns>
        private static byte[] Convert2<T1,T2>(byte[] data, int arrayCount, int vectorSize,
            Func<T1[,,], int, T2> func)
        {
            // convert and resize data array
            var values = Resize2<T1>(data, arrayCount, vectorSize);
            // allocate unmanaged memory for conversion
            var mem = Marshal.AllocHGlobal(Marshal.SizeOf<T2>());
            // allocate output array
            var output = new byte[Marshal.SizeOf<T2>() * values.GetLength(1)];

            for (int i = 0; i < values.GetLength(1); i++)
            {
                // process data array and store the result in unmanaged memory
                Marshal.StructureToPtr(func(values, i), mem, true);
                // copy the result form the unmanaged memory to the output array
                Marshal.Copy(mem, output, Marshal.SizeOf<T2>() * i, Marshal.SizeOf<T2>());
            }

            // free unmanaged memory
            Marshal.FreeHGlobal(mem);
            return output;
        }

        /// <summary>
        /// Cast and resize the byte array.
        /// </summary>
        /// <typeparam name="T">Cast type.</typeparam>
        /// <param name="data"></param>
        /// <param name="arrayCount"></param>
        /// <param name="vectorSize"></param>
        /// <returns></returns>
        private static T[,,] Resize2<T>(byte[] data, int arrayCount, int vectorSize)
        {
            var count = data.Length / (Marshal.SizeOf<T>() * arrayCount * vectorSize);
            return (T[,,])Bytes2<T>(data, arrayCount, count, vectorSize);
        }

        /// <summary>
        /// Cast and resize the byte array.
        /// </summary>
        /// <typeparam name="T">Cast type.</typeparam>
        /// <param name="data"></param>
        /// <param name="lengths"></param>
        /// <returns></returns>
        private static Array Bytes2<T>(byte[] data, params int[] lengths)
        {
            // allocate output array
            var rs = Array.CreateInstance(typeof(T), lengths);
            // copy data to output array
            Buffer.BlockCopy(data, 0, rs, 0, data.Length);
            return rs;
        }
    }
}
