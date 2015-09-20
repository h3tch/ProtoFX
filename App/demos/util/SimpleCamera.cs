using OpenTK;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace util
{
    class SimpleCamera
    {
        #region FIELDS
        private Vector3 pos;
        private Vector3 rot;
        private Vector4 info;
        private Matrix4 view;
        private Matrix4 proj;
        private Matrix4 vwpj;
        private Dictionary<int, CameraUniforms> uniform = new Dictionary<int, CameraUniforms>();
        private Point mousedown = new Point(0, 0);
        private Point mousepos = new Point(0, 0);
        #endregion

        public struct CameraUniforms
        {
            public int view;
            public int proj;
            public int vwpj;
            public int info;
        }

        public SimpleCamera(string[] cmd)
        {
            // default values
            pos = Vector3.Zero;
            rot = Vector3.Zero;
            float fovy = 60.0f;
            float n = 0.1f;
            float f = 100.0f;
            float red2deg = (float)(Math.PI / 180);
            // parse command for values specified by the user
            int i = 3;
            if (cmd.Length > i) float.TryParse(cmd[i], out fovy); i++;
            if (cmd.Length > i) float.TryParse(cmd[i], out n); i++;
            if (cmd.Length > i) float.TryParse(cmd[i], out f); i++;
            if (cmd.Length > i) float.TryParse(cmd[i], out pos.X); i++;
            if (cmd.Length > i) float.TryParse(cmd[i], out pos.Y); i++;
            if (cmd.Length > i) float.TryParse(cmd[i], out pos.Z); i++;
            if (cmd.Length > i) float.TryParse(cmd[i], out rot.X); i++;
            if (cmd.Length > i) float.TryParse(cmd[i], out rot.Y); i++;
            if (cmd.Length > i) float.TryParse(cmd[i], out rot.Z); i++;
            rot = rot * red2deg;
            Proj(fovy * red2deg, 16f / 9f, n, f);
        }

        public void Bind(int program, int width, int height, int widthTex, int heightTex)
        {
            // GET OR CREATE CAMERA UNIFORMS FOR program
            CameraUniforms unif;
            if (uniform.TryGetValue(program, out unif) == false)
                uniform.Add(program, unif = CreateCameraUniforms(program, width, height));

            // SET INTERNAL VARIABLES
            if (unif.view >= 0 || unif.vwpj >= 0)
            {
                view = Matrix4.CreateTranslation(-pos)
                    * Matrix4.CreateRotationY(-rot.Y)
                    * Matrix4.CreateRotationX(-rot.X);
                GL.UniformMatrix4(unif.view, false, ref view);
            }
            if (unif.proj >= 0 || unif.vwpj >= 0)
            {
                proj = Matrix4.CreatePerspectiveFieldOfView(info.X, info.Y, info.Z, info.W);
                GL.UniformMatrix4(unif.proj, false, ref proj);
            }
            if (unif.vwpj >= 0)
            {
                vwpj = view * proj;
                GL.UniformMatrix4(unif.vwpj, false, ref vwpj);
            }
            if (unif.info >= 0)
                GL.Uniform4(unif.info, ref info);
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
                Rotate((float)(Math.PI / 360) * (mousepos.Y - e.Y), (float)(Math.PI / 360) * (mousepos.X - e.X), 0);
            else if (e.Button == MouseButtons.Right)
                Move(0, 0, 0.03f * (e.Y - mousepos.Y));
            mousepos.X = e.X;
            mousepos.Y = e.Y;
        }

        public void Resize(object sender, EventArgs e)
        {
            GLControl gl = (GLControl)sender;
            float aspect = (float)gl.Width / gl.Height;
            Proj((float)(60 * (Math.PI / 180)), aspect, 0.1f, 100.0f);
        }

        #endregion

        #region PRIVATE UTILITY METHODS

        private CameraUniforms CreateCameraUniforms(int program, int width, int height)
        {
            info.Y = (float)width / height;

            CameraUniforms unif = new CameraUniforms();
            unif.view = GL.GetUniformLocation(program, "g_view");
            unif.proj = GL.GetUniformLocation(program, "g_proj");
            unif.vwpj = GL.GetUniformLocation(program, "g_viewproj");
            unif.info = GL.GetUniformLocation(program, "g_info");
            return unif;
        }

        private void Rotate(float x, float y, float z)
        {
            rot += new Vector3(x, y, z);
        }

        private void Move(float x, float y, float z)
        {
            pos += view.Column0.Xyz * x + view.Column1.Xyz * y + view.Column2.Xyz * z;
        }

        private void Proj(float fovy, float aspect, float znear, float zfar)
        {
            info = new Vector4(fovy, aspect, znear, zfar);
        }

        #endregion
    }
}