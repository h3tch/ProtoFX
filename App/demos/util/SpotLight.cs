using OpenTK;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;

namespace util
{
    using Commands = Dictionary<string, string[]>;

    class SpotLight
    {
        #region FIELDS
        protected float[] pos = new float[] { 0f, 0f, 0f };
        protected float[] rot = new float[] { 0f, 0f, 0f };
        protected float fov = 60f;
        protected float near = 0.1f;
        protected float far = 100f;
        protected float[] color = new float[] { 1f, 1f, 1f };
        protected float intensity = 100f;
        protected float innerCone = 80f;
        protected float outerCone = 100f;
        protected const float rad2deg = (float)(Math.PI / 180);
        protected string name = "SpotLight";
        protected static string name_view = "view";
        protected static string name_proj = "proj";
        protected static string name_vwpj = "viewProj";
        protected static string name_camera = "camera";
        protected static string name_color = "color";
        protected static string name_light = "light";
        protected Dictionary<int, Unif> unif = new Dictionary<int, Unif>();
        protected List<string> errors = new List<string>();
        #endregion

        #region PROPERTIES
        public float[] Position { get { return pos; } set { pos = value; } }
        public float[] Rotation { get { return rot; } set { rot = value; } }
        public float FieldOfViewY { get { return fov; } set { fov = value; } }
        public float NearPlane { get { return near; } set { near = value; } }
        public float FarPlane { get { return far; } set { far = value; } }
        public float[] Color { get { return color; } set { color = value; } }
        public float Intensity { get { return intensity; } set { intensity = value; } }
        public float InnerCone { get { return innerCone; } set { innerCone = value; } }
        public float OuterCone { get { return outerCone; } set { outerCone = value; } }
        #endregion

        public SpotLight(Commands cmds)
        {
            // parse command for values specified by the user
            Convert(cmds, "pos", ref pos);
            Convert(cmds, "rot", ref rot);
            Convert(cmds, "fov", ref fov);
            Convert(cmds, "near", ref near);
            Convert(cmds, "far", ref far);
            Convert(cmds, "color", ref color);
            Convert(cmds, "intensity", ref intensity);
            Convert(cmds, "innerCone", ref innerCone);
            Convert(cmds, "outerCone", ref outerCone);
            Convert(cmds, "name", ref name);
        }

        public void Update(int program, int width, int height, int widthTex, int heightTex)
        {
            // GET OR CREATE CAMERA UNIFORMS FOR program
            Unif unif;
            if (this.unif.TryGetValue(program, out unif) == false)
                this.unif.Add(program, unif = new Unif(program, name));

            // COMPUTE MATH
            Matrix4 view = Matrix4.CreateTranslation(-pos[0], -pos[1], -pos[2])
                         * Matrix4.CreateRotationY(-rot[0] * rad2deg)
                         * Matrix4.CreateRotationX(-rot[1] * rad2deg);
            float aspect = (float)width / height;
            Matrix4 proj = Matrix4.CreatePerspectiveFieldOfView(fov * rad2deg, aspect, near, far);

            // SET INTERNAL VARIABLES
            if (unif.view >= 0)
                GL.UniformMatrix4(unif.view, false, ref view);

            if (unif.proj >= 0)
                GL.UniformMatrix4(unif.proj, false, ref proj);

            if (unif.vwpj >= 0)
            {
                Matrix4 vwpj = view * proj;
                GL.UniformMatrix4(unif.vwpj, false, ref vwpj);
            }

            if (unif.camera >= 0)
            {
                Vector4 camera = new Vector4(fov * rad2deg, aspect, near, far);
                GL.Uniform4(unif.camera, ref camera);
            }

            if (unif.color >= 0)
            {
                Vector4 col = new Vector4(color[0], color[1], color[2], intensity);
                GL.Uniform4(unif.color, ref col);
            }

            if (unif.light >= 0)
            {
                Vector4 light = new Vector4(innerCone * rad2deg, outerCone * rad2deg, 0f, 0f);
                GL.Uniform4(unif.light, ref light);
            }
        }

        public List<string> GetErrors()
        {
            return errors;
        }

        #region UTILITY METHOD
        private void Convert<T>(Commands cmds, string cmd, ref T[] v)
        {
            int i = 0, l;

            int length = v.Length;

            if (cmds.ContainsKey(cmd))
            {
                var s = cmds[cmd];
                for (l = Math.Min(s.Length, length); i < s.Length; i++)
                    if (!TryChangeType(s[i], ref v[i]))
                        errors.Add("Command '" + cmd + "': Could not convert argument "
                            + (i+1) + " '" + s[i] + "'.");
            }
        }

        private void Convert<T>(Commands cmds, string cmd, ref T v)
        {
            if (cmds.ContainsKey(cmd))
            {
                var s = cmds[cmd];
                if (s.Length == 0)
                    return;
                if(!TryChangeType(s[0], ref v))
                    errors.Add("Command '" + cmd + "': Could not convert argument 1 '" + s[0] + "'.");
            }
        }

        private static bool TryChangeType<T>(object invalue, ref T value)
        {
            if (invalue == null || invalue as IConvertible == null)
                return false;

            try
            {
                value = (T)System.Convert.ChangeType(invalue, typeof(T));
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
                color = GL.GetUniformLocation(program, name + "." + name_color);
                light = GL.GetUniformLocation(program, name + "." + name_light);
            }
            public int view;
            public int proj;
            public int vwpj;
            public int camera;
            public int color;
            public int light;
        }
        #endregion
    }
}
