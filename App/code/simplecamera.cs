using OpenTK;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace gled
{
    class SimpleCamera
    {
        private Vector3 pos;
        private Vector3 rot;
        private Vector4 info;
        private Matrix4 view;
        private Matrix4 proj;
        private Matrix4 vwpj;
        private Dictionary<int, CameraUniforms> uniform = new Dictionary<int, CameraUniforms>();
        private Point mousedown = new Point(0, 0);
        private Point mousepos = new Point(0, 0);

        public struct CameraUniforms
        {
            public int view;
            public int proj;
            public int vwpj;
            public int info;
        }

        public void Bind(int program)
        {
            CameraUniforms unif;
            if (uniform.TryGetValue(program, out unif) == false)
                uniform.Add(program, unif = CreateCameraUniforms(program));

            // SET INTERNAL VARIABLES
            if (unif.view >= 0)
                GL.UniformMatrix4(unif.view, false, ref view);
            if (unif.proj >= 0)
                GL.UniformMatrix4(unif.proj, false, ref proj);
            if (unif.vwpj >= 0)
                GL.UniformMatrix4(unif.vwpj, false, ref vwpj);
            if (unif.info >= 0)
                GL.Uniform4(unif.info, ref info);
        }

        public void Unbind(int program)
        {

        }

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

        private CameraUniforms CreateCameraUniforms(int program)
        {
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
    }
}