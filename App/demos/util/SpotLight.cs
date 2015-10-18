using OpenTK;
using System;

namespace util
{
    class SpotLight
    {
        protected Vector3 pos;
        protected Vector3 rot;
        protected Vector4 info;
        protected Vector4 light;
        protected Matrix4 view;
        protected Matrix4 proj;
        protected Matrix4 vwpj;
        protected string name;
        protected string name_view = "view";
        protected string name_proj = "proj";
        protected string name_vipj = "viewProj";
        protected string name_info = "info";
        protected string name_light = "light";
        protected int unif_view;
        protected int unif_proj;
        protected int unif_vwpj;
        protected int unif_info;
        protected int unif_light;

        public SpotLight(string[] cmd)
        {
            // default values
            pos = Vector3.Zero;
            rot = Vector3.Zero;
            float fovy = 60.0f;
            float znear = 0.1f;
            float zfar = 100.0f;

            // parse command for values specified by the user
            int i = 3; // start with 'fovy'
            if (cmd.Length > i) float.TryParse(cmd[i++], out fovy);
            if (cmd.Length > i) float.TryParse(cmd[i++], out znear);
            if (cmd.Length > i) float.TryParse(cmd[i++], out zfar);
            if (cmd.Length > i) float.TryParse(cmd[i++], out pos.X);
            if (cmd.Length > i) float.TryParse(cmd[i++], out pos.Y);
            if (cmd.Length > i) float.TryParse(cmd[i++], out pos.Z);
            if (cmd.Length > i) float.TryParse(cmd[i++], out rot.X);
            if (cmd.Length > i) float.TryParse(cmd[i++], out rot.Y);
            if (cmd.Length > i) float.TryParse(cmd[i++], out rot.Z);
            if (cmd.Length > i) name = cmd[i++];
            if (cmd.Length > i) name_view = cmd[i++];
            if (cmd.Length > i) name_proj = cmd[i++];
            if (cmd.Length > i) name_vipj = cmd[i++];
            if (cmd.Length > i) name_info = cmd[i++];

            float rad2deg = (float)(Math.PI / 180);
            rot = rot * rad2deg;
            Proj(fovy * rad2deg, 16f / 9f, znear, zfar);
        }

        public void Update(int program, int width, int height, int widthTex, int heightTex)
        {

        }

        private void Proj(float fovy, float aspect, float znear, float zfar)
        {
            info.X = fovy;
            info.Y = aspect;
            info.Z = znear;
            info.W = zfar;
        }
    }
}
