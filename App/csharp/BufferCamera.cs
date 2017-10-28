using protofx;
using OpenTK;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Commands = System.Linq.ILookup<string, string[]>;
using Objects = System.Collections.Generic.Dictionary<string, object>;
using GlNames = System.Collections.Generic.Dictionary<string, int>;

namespace camera
{
    class BufferCamera : CsObject
    {
        #region FIELDS

        private Point mousedown = new Point(0, 0);
        private Point mousepos = new Point(0, 0);
        public float posx;
        public float posy;
        public float posz;
        public float rotx;
        public float roty;
        public float rotz;
        public float fov;
        public float near;
        public float far;
        protected string name;
        protected string[] buff = new string[2];
        protected int glBuff;
        protected int glOffset;
        protected Matrix4 view;
        protected const float deg2rad = (float)(Math.PI / 180);

        #endregion

        public BufferCamera(string name, Commands cmds, Objects objs, GlNames glNames)
            : base(cmds, objs)
        {
            this.name = name;

            // PARSE COMMAND VALUES SPECIFIED BY THE USER
            float[] pos = new float[3] { 0f, 0f, 0f };
            float[] rot = new float[3] { 0f, 0f, 0f };
            float fov = 60f, near = 0.1f, far = 100f;
            Convert(cmds, "name", ref this.name);
            Convert(cmds, "buff", ref buff);
            Convert(cmds, "pos", ref pos);
            Convert(cmds, "rot", ref rot);
            Convert(cmds, "fov", ref fov);
            Convert(cmds, "near", ref near);
            Convert(cmds, "far", ref far);
            posx = pos[0];
            posy = pos[1];
            posz = pos[2];
            rotx = rot[0];
            roty = rot[1];
            rotz = rot[2];
            this.fov = fov;
            this.near = near;
            this.far = far;

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
            if (!connectionsInitialized)
                InitializeConnections();

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
            rotx += x;
            roty += y;
            rotz += z;
        }

        private void Move(float x, float y, float z)
        {
            Vector3 v = view.Column0.Xyz * x + view.Column1.Xyz * y + view.Column2.Xyz * z;
            posx += v[0];
            posy += v[1];
            posz += v[2];
        }
        #endregion
    }
}
