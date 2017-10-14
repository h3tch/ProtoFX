using protofx;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Globalization;
using Commands = System.Collections.Generic.Dictionary<string, string[]>;
using GLNames = System.Collections.Generic.Dictionary<string, int>;

namespace camera
{

    class SpotLight
    {
        public static CultureInfo culture = new CultureInfo("en");
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
        protected const float deg2rad = (float)(Math.PI / 180);
        protected string name;
        protected Dictionary<int, UniformBlock<Names>> uniform =
            new Dictionary<int, UniformBlock<Names>>();
        protected List<string> errors = new List<string>();
        #endregion

        #region PROPERTIES
#pragma warning disable IDE0027
        public float[] Position { get { return pos; } set { pos = value; } }
        public float[] Rotation { get { return rot; } set { rot = value; } }
        public float FieldOfViewY { get { return fov; } set { fov = value; } }
        public float NearPlane { get { return near; } set { near = value; } }
        public float FarPlane { get { return far; } set { far = value; } }
        public float[] Color { get { return color; } set { color = value; } }
        public float Intensity { get { return intensity; } set { intensity = value; } }
        public float Radius { get { return radius; } set { radius = value; } }
        public float InnerCone { get { return innerCone; } set { innerCone = value; } }
#pragma warning restore IDE0027
        #endregion

        public SpotLight(string name, Commands cmds, GLNames glNames)
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

        public void Update(int pipeline, int width, int height, int widthTex, int heightTex)
        {
            var view = Matrix4.CreateTranslation(-pos[0], -pos[1], -pos[2])
                 * Matrix4.CreateRotationY(-rot[1] * deg2rad)
                 * Matrix4.CreateRotationX(-rot[0] * deg2rad);
            var aspect = (float)widthTex / heightTex;
            var proj = Matrix4.CreatePerspectiveFieldOfView(fov * deg2rad, aspect, near, far);

            // GET OR CREATE CAMERA UNIFORMS FOR program
#pragma warning disable IDE0018
            UniformBlock<Names> unif;
#pragma warning restore IDE0018
            if (uniform.TryGetValue(pipeline, out unif) == false)
                uniform.Add(pipeline, unif = new UniformBlock<Names>(pipeline, name));

            // SET UNIFORM VALUES
            unif.Set(Names.view, view.AsInt32());

            unif.Set(Names.proj, proj.AsInt32());

            if (unif.Has(Names.viewProj))
                unif.Set(Names.viewProj, (view * proj).AsInt32());

            if (unif.Has(Names.camera))
                unif.Set(Names.camera, new[] { fov * deg2rad, aspect, near, far }.AsInt32());

            if (unif.Has(Names.color))
                unif.Set(Names.color, new[] { color[0], color[1], color[2], intensity }.AsInt32());

            if (unif.Has(Names.light))
            {
                var x = near * (float)Math.Tan(fov * deg2rad);
                var y = x / aspect;
                unif.Set(Names.light, new[] { innerCone * deg2rad, radius, x, y }.AsInt32());
            }

            // UPDATE UNIFORM BUFFER
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
                value = (T)System.Convert.ChangeType(invalue, typeof(T), culture);
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
