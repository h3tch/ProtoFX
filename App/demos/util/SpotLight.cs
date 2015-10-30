using OpenTK;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Linq;

namespace util
{
    using Commands = Dictionary<string, string[]>;

    class SpotLight
    {
        public enum Names
        {
            view,
            proj,
            viewProj,
            camera,
            color,
            light,
        }

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
            if (unif[Names.view] >= 0)
                GL.UniformMatrix4(unif[Names.view], false, ref view);

            if (unif[Names.proj] >= 0)
                GL.UniformMatrix4(unif[Names.proj], false, ref proj);

            if (unif[Names.viewProj] >= 0)
            {
                Matrix4 vwpj = view * proj;
                GL.UniformMatrix4(unif[Names.viewProj], false, ref vwpj);
            }

            if (unif[Names.camera] >= 0)
            {
                Vector4 camera = new Vector4(fov * rad2deg, aspect, near, far);
                GL.Uniform4(unif[Names.camera], ref camera);
            }

            if (unif[Names.color] >= 0)
            {
                Vector4 col = new Vector4(color[0], color[1], color[2], intensity);
                GL.Uniform4(unif[Names.color], ref col);
            }

            if (unif[Names.light] >= 0)
            {
                Vector4 light = new Vector4(innerCone * rad2deg, outerCone * rad2deg, 0f, 0f);
                GL.Uniform4(unif[Names.light], ref light);
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
            private int[] location;

            public Unif(int program, string name)
            {
                string[] names = Enum.GetNames(typeof(Names)).Select(v => name + "." + v).ToArray();
                location = names.Select(n => GL.GetUniformLocation(program, n)).ToArray();
            }

            public int this[Names name]
            {
                get { return location[(int)name]; }
            }
        }
        #endregion
    }
}
