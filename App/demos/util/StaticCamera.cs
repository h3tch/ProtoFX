using OpenTK;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;

namespace util
{
    class StaticCamera
    {
        #region FIELDS
        private Vector3 pos;
        private Vector3 rot;
        private Vector4 info;
        private Matrix4 view;
        private Matrix4 proj;
        private Matrix4 vwpj;
        private Dictionary<int, CameraUniforms> uniform = new Dictionary<int, CameraUniforms>();
        private string unif_view = "g_view";
        private string unif_proj = "g_proj";
        private string unif_vipj = "g_viewproj";
        private string unif_info = "g_info";
        #endregion

        public struct CameraUniforms
        {
            public int view;
            public int proj;
            public int vwpj;
            public int info;
        }

        public StaticCamera(string[] cmd)
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

        #region PRIVATE UTILITY METHODS

        private CameraUniforms CreateCameraUniforms(int program, int width, int height)
        {
            info.Y = (float)width / height;

            CameraUniforms unif = new CameraUniforms();
            unif.view = GL.GetUniformLocation(program, unif_view);
            unif.proj = GL.GetUniformLocation(program, unif_proj);
            unif.vwpj = GL.GetUniformLocation(program, unif_vipj);
            unif.info = GL.GetUniformLocation(program, unif_info);
            return unif;
        }

        private void Proj(float fovy, float aspect, float znear, float zfar)
        {
            info = new Vector4(fovy, aspect, znear, zfar);
        }

        #endregion
    }
}
