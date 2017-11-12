using protofx;
using System;
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
        // Properties accessible by ProtoFX

        public float[] Color
        {
            get { return new float[] { colorr, colorg, colorb }; }
            set { colorr = value[0]; colorg = value[1]; colorb = value[2]; }
        }
        public float Intensity { get { return intensity; } set { intensity = value; } }
        public float InnerCone { get { return innerCone; } set { innerCone = value; } }
        public float Radius { get { return radius; } set { radius = value; } }
        
        #endregion

        /// <summary>
        /// ProtoFX Constructor.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="cmds"></param>
        /// <param name="objs"></param>
        public SpotLight(object @params) : base(@params)
        {
            // PARSE COMMAND VALUES SPECIFIED BY THE USER
            float[] color = new float[3] { 1f, 1f, 1f };
            Convert(commands, "color", ref color);
            Convert(commands, "intensity", ref intensity);
            Convert(commands, "innerCone", ref innerCone);
            Convert(commands, "radius", ref radius);
            Color = color;
        }

        /// <summary>
        /// ProtoFX update method.
        /// </summary>
        /// <param name="pipeline">Active shader pipeline</param>
        /// <param name="width">Backbuffer width</param>
        /// <param name="height">Backbuffer height</param>
        /// <param name="widthTex">Framebuffer width</param>
        /// <param name="heightTex">Framebuffer height</param>
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
