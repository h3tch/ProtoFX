using protofx;
using OpenTK;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Commands = System.Collections.Generic.Dictionary<string, string[]>;
using GlNames = System.Collections.Generic.Dictionary<string, int>;

namespace camera
{
    class BufferCamera : CsObject
    {
        #region FIELDS
        private Point mousedown = new Point(0, 0);
        private Point mousepos = new Point(0, 0);
        public float[] pos = new float[] { 0f, 0f, 0f };
        public float[] rot = new float[] { 0f, 0f, 0f };
        public float fov = 60f;
        public float near = 0.1f;
        public float far = 100f;
        protected string name;
        protected string[] buff = new string[2];
        protected int glBuff;
        protected int glOffset;
        protected Matrix4 view;
        protected const float rad2deg = (float)(Math.PI / 180);
        #endregion

        public BufferCamera(string name, Commands cmds, GlNames glNames)
        {
            // PARSE COMMAND VALUES SPECIFIED BY THE USER
            this.name = name;
            Convert(cmds, "far", ref far);
            Convert(cmds, "buff", ref buff);
            Convert(cmds, "rot", ref rot);
            Convert(cmds, "fov", ref fov);
            Convert(cmds, "near", ref near);
            Convert(cmds, "far", ref far);

            // get buffer object
            if (buff != null)
            {
                if (!glNames.TryGetValue(buff[0], out glBuff))
                    errors.Add("The specified buffer name '" + buff[0] + "' could not be found.");
                if (buff.Length > 1 && !int.TryParse(buff[1], out glOffset))
                    errors.Add("Could not parse offset value '" + buff[1] + "' of buff command.");
            }
            else
                errors.Add("A buffer object needs to be specified (e.g., buff buf_name).");
        }

        public void Update(int program, int width, int height, int widthTex, int heightTex)
        {
            view = Matrix4.CreateTranslation(-pos[0], -pos[1], -pos[2])
                 * Matrix4.CreateRotationY(-rot[1] * rad2deg)
                 * Matrix4.CreateRotationX(-rot[0] * rad2deg);
            var aspect = (float)width / height;
            var proj = Matrix4.CreatePerspectiveFieldOfView(fov * rad2deg, aspect, near, far);
            var viewProj = view * proj;
            var data = viewProj.AsInt32();

            var ptr = GL.MapNamedBufferRange(glBuff, (IntPtr)glOffset, 4 * data.Length,
                BufferAccessMask.MapWriteBit);
            Marshal.Copy(data, 0, ptr, data.Length);
            GL.UnmapNamedBuffer(glBuff);
        }
        
        public void Delete()
        {
        }
        
        #region OPENTK GLCONTROL WINDOW EVENTS
        public void MouseDown(object sender, MouseEventArgs e)
        {
            mousedown.X = mousepos.X = e.X;
            mousedown.Y = mousepos.Y = e.Y;
        }

        public void MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                Rotate(0.1f * (mousepos.Y - e.Y), 0.1f * (mousepos.X - e.X), 0);
            else if (e.Button == MouseButtons.Right)
                Move(0, 0, 0.03f * (e.Y - mousepos.Y));
            mousepos.X = e.X;
            mousepos.Y = e.Y;
        }
        #endregion

        #region PRIVATE UTILITY METHODS
        private void Rotate(float x, float y, float z)
        {
            rot[0] += x;
            rot[1] += y;
            rot[2] += z;
        }

        private void Move(float x, float y, float z)
        {
            Vector3 v = view.Column0.Xyz * x + view.Column1.Xyz * y + view.Column2.Xyz * z;
            pos[0] += v[0];
            pos[1] += v[1];
            pos[2] += v[2];
        }
        #endregion
    }
}
