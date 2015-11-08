using OpenTK;
using System;
using System.Collections.Generic;

namespace csharp
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
        protected float innerCone = 40f;
        protected float radius = 0.1f;
        protected const float rad2deg = (float)(Math.PI / 180);
        protected string name = "SpotLight";
        //protected Dictionary<int, Unif> unif = new Dictionary<int, Unif>();
        protected Dictionary<int, UniformBlock<Names>> uniform =
            new Dictionary<int, UniformBlock<Names>>();
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
        public float Radius { get { return radius; } set { radius = value; } }
        public float InnerCone { get { return innerCone; } set { innerCone = value; } }
        #endregion

        public SpotLight(string name, Commands cmds)
        {
            this.name = name;
            // parse command for values specified by the user
            Convert(cmds, "name", ref this.name);
            Convert(cmds, "pos", ref pos);
            Convert(cmds, "rot", ref rot);
            Convert(cmds, "fov", ref fov);
            Convert(cmds, "near", ref near);
            Convert(cmds, "far", ref far);
            Convert(cmds, "color", ref color);
            Convert(cmds, "intensity", ref intensity);
            Convert(cmds, "innerCone", ref innerCone);
            Convert(cmds, "radius", ref radius);
        }

        public void Update(int program, int width, int height, int widthTex, int heightTex)
        {
            Matrix4 view = Matrix4.CreateTranslation(-pos[0], -pos[1], -pos[2])
                 * Matrix4.CreateRotationY(-rot[1] * rad2deg)
                 * Matrix4.CreateRotationX(-rot[0] * rad2deg);
            float aspect = (float)width / height;
            Matrix4 proj = Matrix4.CreatePerspectiveFieldOfView(fov * rad2deg, aspect, near, far);

            // GET OR CREATE CAMERA UNIFORMS FOR program
            UniformBlock<Names> unif;
            if (uniform.TryGetValue(program, out unif) == false)
                uniform.Add(program, unif = new UniformBlock<Names>(program, name));

            // COMPUTE MATH
            unif.Set(Names.view, view.ToInt32());

            unif.Set(Names.proj, proj.ToInt32());

            if (unif[Names.viewProj] >= 0)
            {
                Matrix4 vwpj = view * proj;
                unif.Set(Names.viewProj, vwpj.ToInt32());
            }

            if (unif[Names.camera] >= 0)
            {
                Vector4 camera = new Vector4(fov * rad2deg, aspect, near, far);
                unif.Set(Names.camera, camera.ToInt32());
            }

            if (unif[Names.color] >= 0)
            {
                Vector4 col = new Vector4(color[0], color[1], color[2], intensity);
                unif.Set(Names.color, col.ToInt32());
            }

            if (unif[Names.light] >= 0)
            {
                Vector4 light = new Vector4(innerCone * rad2deg, radius, 0f, 0f);
                unif.Set(Names.light, light.ToInt32());
            }

            unif.Update();
            unif.Bind();
        }

        public void Delete()
        {
            foreach (var u in uniform)
                u.Value.Delete();
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
    }
}
