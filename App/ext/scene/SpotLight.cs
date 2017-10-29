using protofx;
using System;
using Commands = System.Linq.ILookup<string, string[]>;
using Objects = System.Collections.Generic.Dictionary<string, object>;
using GLNames = System.Collections.Generic.Dictionary<string, int>;
using System.Linq;

namespace scene
{

    class SpotLight : StaticCamera
    {
        #region UNIFORM NAMES

        protected new enum Names
        {
            color = StaticCamera.Names.LAST,
            light,
            LAST,
        }
        protected new static string[] UniformNames;

        static SpotLight()
        {
            var names = Enum.GetNames(typeof(Names));
            names = names.Take((int)Names.LAST - (int)StaticCamera.Names.LAST).ToArray();
            UniformNames = Enumerable.Concat(StaticCamera.UniformNames, names).ToArray();
        }

        #endregion

        #region FIELDS

        [Connectable] protected float colorr;
        [Connectable] protected float colorg;
        [Connectable] protected float colorb;
        [Connectable] protected float intensity = 100f;
        [Connectable] protected float innerCone = 40f;
        [Connectable] protected float radius = 0.1f;

        #endregion

        #region PROPERTIES
        
        public float[] Color
        {
            get { return new float[] { colorr, colorg, colorb }; }
            set { colorr = value[0]; colorg = value[1]; colorb = value[2]; }
        }

        #endregion

        public SpotLight(string name, Commands cmds, Objects objs, GLNames glNames)
            : base(name, cmds, objs, glNames)
        {
            // PARSE COMMAND VALUES SPECIFIED BY THE USER
            float[] color = new float[3] { 1f, 1f, 1f };
            Convert(cmds, "color", ref color);
            Convert(cmds, "intensity", ref intensity);
            Convert(cmds, "innerCone", ref innerCone);
            Convert(cmds, "radius", ref radius);
            Color = color;
        }

        public new void Update(int pipeline, int width, int height, int widthTex, int heightTex)
        {
            // GET OR CREATE CAMERA UNIFORMS FOR program
            var unif = PrepareUpdate(pipeline, width, height, widthTex, heightTex, UniformNames);
            if (unif == null)
                return;

            // SET UNIFORM VALUES
            if (unif.Has((int)Names.color))
                unif.Set((int)Names.color, new float[] { colorr, colorg, colorb, intensity }.AsInt32());

            if (unif.Has((int)Names.light))
            {
                var aspect = (float)width / height;
                var fovRad = fov * deg2rad;
                var x = (float)(near * Math.Tan(fovRad));
                var y = x / aspect;
                unif.Set((int)Names.light, new float[] { innerCone * deg2rad, radius, x, y }.AsInt32());
            }

            // UPDATE UNIFORM BUFFER
            unif.Update();
            unif.Bind();
        }
    }
}
