using protofx;
using OpenTK;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Runtime.InteropServices;

namespace scene
{
    class BufferCamera : SimpleCamera
    {
        #region FIELDS
        
        protected string[] buff = new string[2];
        protected int glBuff;
        protected int glOffset;

        #endregion

        public BufferCamera(object @params) : base(@params)
        {
            // get buffer object
            if (buff != null)
            {
                object buffer;
                if (!objects.TryGetValue(buff[0], out buffer))
                    Errors.Add("The specified buffer name '" + buff[0] + "' could not be found.");
                var prop = buffer.GetType().GetProperty("glname");
                if (prop == null)
                    Errors.Add("The buffer object '" + buff[0] + "' does not provide an OpenGL name.");
                glBuff = (int)prop.GetValue(buffer);
                if (buff.Length > 1 && !int.TryParse(buff[1], out glOffset))
                    Errors.Add("Could not parse offset value '" + buff[1] + "' of buff command.");
            }
            else
                Errors.Add("A buffer object needs to be specified (e.g., buff buf_name).");
        }

        public new void Update(int program, int width, int height, int widthTex, int heightTex)
        {
            view = Matrix4.CreateTranslation(-posx, -posy, -posz)
                 * Matrix4.CreateRotationY(-roty * deg2rad)
                 * Matrix4.CreateRotationX(-rotx * deg2rad);
            var aspect = (float)width / height;
            var angle = fov * deg2rad;
            var proj = Matrix4.CreatePerspectiveFieldOfView(angle, aspect, near, far);
            var viewProj = view * proj;
            var data = viewProj.AsInt32();

            var ptr = GL.MapNamedBufferRange(glBuff, (IntPtr)glOffset, 4 * data.Length,
                BufferAccessMask.MapWriteBit);
            Marshal.Copy(data, 0, ptr, data.Length);
            GL.UnmapNamedBuffer(glBuff);
        }
    }
}
