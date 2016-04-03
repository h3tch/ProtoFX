using OpenTK;
using System;
using System.Linq;
using System.Runtime.InteropServices;

namespace data
{
    class Converter
    {
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
            var values = Byte2Float(data);
            var count = values.Length / (3 * 4); // 3 = pos,rot,proj; 4 = sizeof(float);
            var matrix = Marshal.AllocHGlobal(Marshal.SizeOf<Matrix4>());
            var output = new byte[4 * 16 * count];

            for (int i = 0; i < count; i++)
            {
                var pos = values.Skip(4 * i).Take(3).ToArray();
                var rot = values.Skip(4 * count + 4 * i).Take(2).ToArray();
                var prj = values.Skip(8 * count + 4 * i).Take(4).ToArray();
                var near = prj[0];
                var far = prj[1];
                var fov = prj[2];
                var aspect = prj[3];
                // This function is executed every frame at the beginning of a pass.
                var view = Matrix4.CreateTranslation(-pos[0], -pos[1], -pos[2])
                     * Matrix4.CreateRotationY(-rot[1] * rad2deg)
                     * Matrix4.CreateRotationX(-rot[0] * rad2deg);
                var proj = Matrix4.CreatePerspectiveFieldOfView(fov * rad2deg, aspect, near, far);

                Marshal.StructureToPtr(view * proj, matrix, true);
                Marshal.Copy(matrix, output, 4 * 16 * i, 4 * 16);
            }

            return output;
        }

        public static byte[] Convert2ModelMatrix(byte[] data)
        {
            var values = Byte2Float(data);
            var count = values.Length / (3 * 4); // 3 = pos,rot,proj; 4 = sizeof(float);
            var matrix = Marshal.AllocHGlobal(Marshal.SizeOf<Matrix4>());
            var output = new byte[4 * 16 * count];

            for (int i = 0; i < count; i++)
            {
                var pos = values.Skip(4 * i).Take(3).ToArray();
                var rot = values.Skip(4 * count + 4 * i).Take(3).ToArray();
                var sca = values.Skip(8 * count + 4 * i).Take(3).ToArray();
                // This function is executed every frame at the beginning of a pass.
                var model = Matrix4.CreateTranslation(pos[0], pos[1], pos[2])
                     * Matrix4.CreateRotationX(rot[2] * rad2deg)
                     * Matrix4.CreateRotationY(rot[1] * rad2deg)
                     * Matrix4.CreateRotationX(rot[0] * rad2deg)
                     * Matrix4.CreateScale(sca[0], sca[1], sca[2]);

                Marshal.StructureToPtr(model, matrix, true);
                Marshal.Copy(matrix, output, 4 * 16 * i, 4 * 16);
            }

            return output;
        }
        
        private const float rad2deg = (float)(Math.PI / 180);

        private static float[] Byte2Float(byte[] array)
        {
            float[] floatArr = new float[array.Length / 4];
            for (int i = 0; i < floatArr.Length; i++)
            {
                //if (BitConverter.IsLittleEndian)
                //{
                //    Array.Reverse(array, i * 4, 4);
                //}
                floatArr[i] = BitConverter.ToSingle(array, i * 4);
            }
            return floatArr;
        }
    }
}
