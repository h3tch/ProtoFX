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
        private Dictionary<int, Uniforms> uniform = new Dictionary<int, Uniforms>();
        private Point mousedown = new Point(0, 0);
        private Point mousepos = new Point(0, 0);
        private string unif_view = "g_view";
        private string unif_proj = "g_proj";
        private string unif_vipj = "g_viewproj";
        private string unif_info = "g_info";
        #endregion

        public struct Uniforms
        {
            public int view;
            public int proj;
            public int vwpj;
            public int info;
        }

        public SimpleCamera(string[] cmd)
        {
            // The constructor is executed only once when the pass is created.

            // ProtoGL code:
            // exec csharp_name util.SimpleCamera fovy nearz farz x y z rotx roty rotz ...
            //      uniform_view_name uniform_proj_name uniform_view_proj_name uniform_info_name

            // argument cmd contains the whole command including
            // 'exec', 'csharp_name' and 'util.SimpleCamera'

            // default values
            pos = Vector3.Zero;
            rot = Vector3.Zero;
            float fovy = 60.0f;
            float n = 0.1f;
            float f = 100.0f;
            float red2deg = (float)(Math.PI / 180);

            // parse command for values specified by the user
            int i = 3; // start with 'fovy'
            if (cmd.Length > i) float.TryParse(cmd[i++], out fovy);
            if (cmd.Length > i) float.TryParse(cmd[i++], out n);
            if (cmd.Length > i) float.TryParse(cmd[i++], out f);
            if (cmd.Length > i) float.TryParse(cmd[i++], out pos.X);
            if (cmd.Length > i) float.TryParse(cmd[i++], out pos.Y);
            if (cmd.Length > i) float.TryParse(cmd[i++], out pos.Z);
            if (cmd.Length > i) float.TryParse(cmd[i++], out rot.X);
            if (cmd.Length > i) float.TryParse(cmd[i++], out rot.Y);
            if (cmd.Length > i) float.TryParse(cmd[i++], out rot.Z);
            if (cmd.Length > i) unif_view = cmd[i++];
            if (cmd.Length > i) unif_proj = cmd[i++];
            if (cmd.Length > i) unif_vipj = cmd[i++];
            if (cmd.Length > i) unif_info = cmd[i++];
            rot = rot * red2deg;
            Proj(fovy * red2deg, 16f / 9f, n, f);
        }

        public void Update(int program, int width, int height, int widthTex, int heightTex)
        {
            // This function is executed every frame at the beginning of a pass.

            // GET OR CREATE CAMERA UNIFORMS FOR program
            Uniforms unif;
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

        //public void EndPass(int program)
        //{
        //    // Executed at the end of a pass every frame.
        //    // not used
        //}

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
        private Uniforms CreateCameraUniforms(int program, int width, int height)
        {
            info.Y = (float)width / height;

            Uniforms unif = new Uniforms();
            unif.view = GL.GetUniformLocation(program, unif_view);
            unif.proj = GL.GetUniformLocation(program, unif_proj);
            unif.vwpj = GL.GetUniformLocation(program, unif_vipj);
            unif.info = GL.GetUniformLocation(program, unif_info);
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