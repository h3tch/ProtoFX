using OpenTK;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Linq;

namespace csharp
{
    using Commands = Dictionary<string, string[]>;

    public class StaticCamera
    {
        public enum Names
        {
            view,
            proj,
            viewProj,
            camera,
        }

        #region FIELDS
        public float[] pos = new float[] { 0f, 0f, 0f };
        public float[] rot = new float[] { 0f, 0f, 0f };
        public float fov = 60f;
        public float near = 0.1f;
        public float far = 100f;
        protected const float rad2deg = (float)(Math.PI / 180);
        protected string name;
        protected Matrix4 view;
        protected Dictionary<int, Unif> uniform = new Dictionary<int, Unif>();
        protected List<string> errors = new List<string>();
        #endregion

        #region PROPERTIES
        public string Name { get { return name; } }
        public float[] Position { get { return pos; } set { pos = value; } }
        public float[] Rotation { get { return rot; } set { rot = value; } }
        public float FieldOfViewY { get { return fov; } set { fov = value; } }
        public float NearPlane { get { return near; } set { near = value; } }
        public float FarPlane { get { return far; } set { far = value; } }
        #endregion

        public StaticCamera(string name, Commands cmds)
        {
            // The constructor is executed only once when the pass is created.

            // ProtoGL code:
            // exec csharp_name util.SimpleCamera fovy nearz farz x y z rotx roty rotz ...
            //      uniform_view_name uniform_proj_name uniform_view_proj_name uniform_info_name

            // argument cmd contains the whole command including
            // 'exec', 'csharp_name' and 'util.SimpleCamera'

            // parse command for values specified by the user

            // PARSE COMMAND VALUES SPECIFIED BY THE USER
            this.name = name;
            Convert(cmds, "name", ref this.name);
            Convert(cmds, "pos", ref pos);
            Convert(cmds, "rot", ref rot);
            Convert(cmds, "fov", ref fov);
            Convert(cmds, "near", ref near);
            Convert(cmds, "far", ref far);
        }

        public void Update(int program, int width, int height, int widthTex, int heightTex)
        {
            // This function is executed every frame at the beginning of a pass.
            
            // GET OR CREATE CAMERA UNIFORMS FOR program
            Unif unif;
            if (uniform.TryGetValue(program, out unif) == false)
                uniform.Add(program, unif = new Unif(program, name));

            // COMPUTE MATH
            view = Matrix4.CreateTranslation(-pos[0], -pos[1], -pos[2])
                 * Matrix4.CreateRotationY(-rot[1] * rad2deg)
                 * Matrix4.CreateRotationX(-rot[0] * rad2deg);
            float aspect = (float)widthTex / heightTex;
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
        }

        //public void EndPass(int program)
        //{
        //    // Executed at the end of a pass every frame.
        //    // not used
        //}

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
                            + (i + 1) + " '" + s[i] + "'.");
            }
        }

        private void Convert<T>(Commands cmds, string cmd, ref T v)
        {
            if (cmds.ContainsKey(cmd))
            {
                var s = cmds[cmd];
                if (s.Length == 0)
                    return;
                if (!TryChangeType(s[0], ref v))
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

            public int this[Names name]{
                get { return location[(int)name]; }
            }
        }
        #endregion
    }
}
