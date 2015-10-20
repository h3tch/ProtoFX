using OpenTK;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;

namespace util
{
    class SpotLight
    {
        #region FIELDS
        protected Vector3 pos;
        protected Vector3 rot;
        protected Vector4 camera;
        protected Vector4 light;
        protected Matrix4 view;
        protected Matrix4 proj;
        protected Matrix4 vwpj;
        protected string name;
        protected static string name_view = "view";
        protected static string name_proj = "proj";
        protected static string name_vwpj = "viewProj";
        protected static string name_camera = "camera";
        protected static string name_light = "light";
        protected Dictionary<int, Unif> uniform = new Dictionary<int, Unif>();
        #endregion

        #region PROPERTIES
        protected float fovy { get { return camera.X; } set { camera.X = value; } }
        protected float aspect { get { return camera.Y; } set { camera.Y = value; } }
        protected float nearz { get { return camera.Z; } set { camera.Z = value; } }
        protected float farz { get { return camera.W; } set { camera.W = value; } }
        #endregion

        public SpotLight(Dictionary<string,string[]> cmds, out string errorText)
        {
            const float rad2deg = (float)(Math.PI / 180);

            // parse command for values specified by the user
            errorText = "";
            float[] pos = Convert(cmds, "pos", 3, 0f, ref errorText);
            float[] rot = Convert(cmds, "rot", 3, 0f, ref errorText);
            float[] fovy = Convert(cmds, "fov", 1, 60.0f, ref errorText);
            float[] nearz = Convert(cmds, "near", 1, 0.1f, ref errorText);
            float[] farz = Convert(cmds, "far", 1, 100.0f, ref errorText);
            string[] name = cmds.ContainsKey("name") ? cmds["name"] : new[] { "SpotLight" };

            // set fields
            this.pos = new Vector3(pos[0], pos[1], pos[2]);
            this.rot = new Vector3(rot[0], rot[1], rot[2]) * rad2deg;
            this.camera = new Vector4(fovy[0], 16f / 9f, nearz[0], farz[0]);
            this.name = name[0];
        }

        public void Update(int program, int width, int height, int widthTex, int heightTex)
        {
            // GET OR CREATE CAMERA UNIFORMS FOR program
            Unif unif;
            if (uniform.TryGetValue(program, out unif) == false)
                uniform.Add(program, unif = new Unif(program, name));

            // COMPUTE MATH
            view = Matrix4.CreateTranslation(-pos)
                 * Matrix4.CreateRotationY(-rot.Y)
                 * Matrix4.CreateRotationX(-rot.X);
            proj = Matrix4.CreatePerspectiveFieldOfView(fovy, aspect = (float)width / height, nearz, farz);

            // SET INTERNAL VARIABLES
            if (unif.view >= 0)
                GL.UniformMatrix4(unif.view, false, ref view);

            if (unif.proj >= 0)
                GL.UniformMatrix4(unif.proj, false, ref proj);

            if (unif.vwpj >= 0)
            {
                vwpj = view * proj;
                GL.UniformMatrix4(unif.vwpj, false, ref vwpj);
            }

            if (unif.camera >= 0)
                GL.Uniform4(unif.camera, ref camera);

            if (unif.light >= 0)
                GL.Uniform4(unif.light, ref light);
        }

        #region UTILITY METHOD

        private static T[] Convert<T>(Dictionary<string, string[]> cmds, string cmd, int length, T defaultValue, ref string err)
        {
            int i = 0, l;

            T[] v = new T[length];

            if (cmds.ContainsKey(cmd))
            {
                var s = cmds[cmd];
                for (l = Math.Min(s.Length, length); i < s.Length; i++)
                    if (!TryChangeType(s[i], out v[i], defaultValue))
                        err += "Command '" + cmd + "': Could not convert argument "
                            + i + " '" + s[i] + "'. ";
            }

            for (; i < length; i++)
                v[i] = defaultValue;

            return v;
        }

        private static bool TryChangeType<T>(object invalue, out T outvalue, T defaultValue)
        {
            outvalue = defaultValue;

            if (invalue == null || invalue as IConvertible == null)
                return false;

            try
            {
                outvalue = (T)System.Convert.ChangeType(invalue, typeof(T));
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region INNER CLASSES
        protected struct Unif
        {
            public Unif(int program, string name)
            {
                view = GL.GetUniformLocation(program, name + "." + name_view);
                proj = GL.GetUniformLocation(program, name + "." + name_proj);
                vwpj = GL.GetUniformLocation(program, name + "." + name_vwpj);
                camera = GL.GetUniformLocation(program, name + "." + name_camera);
                light = GL.GetUniformLocation(program, name + "." + name_light);
            }
            public int view;
            public int proj;
            public int vwpj;
            public int camera;
            public int light;
        }
        #endregion
    }
}
