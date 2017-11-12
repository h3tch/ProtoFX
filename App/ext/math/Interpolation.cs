using protofx;
using System;
using System.Diagnostics;
using System.Linq;

namespace math
{
    [Fx("<Lexer type='interpolate' lexer='TechBodyLexer'>",
        "<Style theme='DarkTheme' name='command' fore='#7AC0F4'>",
        "<Style theme='LightTheme' name='command' fore='#7AC0F4'>",
        "<Keyword style_name='command' name='name' hint='Specify the name of the instance.'>",
        "<Keyword style_name='command' name='from' hint='The start value of the interpolation (default &lt;param&gt;0.0&lt;/param&gt).'>",
        "<Keyword style_name='command' name='start' hint='The start value of the interpolation (default &lt;param&gt;0.0&lt;/param&gt).'>",
        "<Keyword style_name='command' name='to' hint='The end value of the interpolation (default &lt;param&gt;1.0&lt;/param&gt).'>",
        "<Keyword style_name='command' name='end' hint='The end value of the interpolation (default &lt;param&gt;1.0&lt;/param&gt).'>",
        "<Keyword style_name='command' name='speed' hint='The end value of the interpolation (default &lt;param&gt;1.0&lt;/param&gt).'>",
        "<Keyword style_name='command' name='reflect' hint='The end value of the interpolation (default &lt;param&gt;false&lt;/param&gt).'>",
        "<Keyword style_name='command' name='smooth' hint='The end value of the interpolation (default &lt;param&gt;false&lt;/param&gt).'>",
        "</Lexer>")]
    class Interpolate : Node
    {
        #region UNIFORM NAMES

        protected enum Names
        {
            value,
            LAST,
        }
        protected static string[] UniformNames = Enum.GetNames(typeof(Names))
            .Take((int)Names.LAST).ToArray();

        #endregion

        #region FIELDS

        protected string name;
        private double from = 0;
        private double to = 1;
        private double v;
        [Connectable] private double value;
        private double speed = 1;
        private bool reflect = false;
        private bool smooth = false;
        private Stopwatch stopwatch = new Stopwatch();

        #endregion

        #region PROPERTIES
        // Properties accessible by ProtoFX

        public string Name { get { return name; } }
        public double From { get { return from; } set { from = value; } }
        public double To { get { return to; } set { to = value; } }
        public double Speed { get { return speed; } set { speed = value; } }
        public bool Reflect { get { return reflect; } set { reflect = value; } }
        public bool Smooth { get { return smooth; } set { smooth = value; } }

        #endregion

        /// <summary>
        /// ProtoFX Constructor.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="cmds"></param>
        /// <param name="objs"></param>
        /// <param name="glNames"></param>
        public Interpolate(object @params)
            : base(@params)
        {
            name = @params.GetInstanceValue<string>("Name");
            Convert(commands, "name", ref name);
            Convert(commands, "from", ref from);
            Convert(commands, "start", ref from);
            Convert(commands, "to", ref to);
            Convert(commands, "end", ref to);
            Convert(commands, "speed", ref speed);
            Convert(commands, "reflect", ref reflect);
            Convert(commands, "smooth", ref smooth);
        }

        /// <summary>
        /// ProtoFX update method.
        /// </summary>
        /// <param name="pipeline">Active shader pipeline</param>
        /// <param name="width">Backbuffer width</param>
        /// <param name="height">Backbuffer height</param>
        /// <param name="widthTex">Framebuffer width</param>
        /// <param name="heightTex">Framebuffer height</param>
        public void Update(int pipeline, int width, int height, int widthTex, int heightTex)
        {
            // increase value based on the elapsed time
            if (stopwatch.IsRunning)
                v = (v + (stopwatch.ElapsedMilliseconds / 1000.0) * speed) % 1;

            // restart the stop watch
            stopwatch.Restart();

            // reflect and smooth the value if necessary
            var newValue = reflect ? Math.Abs(v * 2 - 1) : v;
            if (smooth)
                newValue = 1 - Math.Cos(Math.PI * newValue) * 0.5 + 0.5;

            // interpolate between 'from' and 'to' value
            newValue = from * (1 - newValue) + to * newValue;

            // UPDATE CONNECTIONS
            if (value != newValue)
            {
                value = newValue;
                connections.TryUpdate(() => value);
            }

            // GET OR CREATE CAMERA UNIFORMS FOR program
            var unif = GetUniformBlock(pipeline, name, UniformNames);
            if (unif == null)
                return;

            if (unif.Has((int)Names.value))
                unif.Set((int)Names.value, new[] { (float)value });

            // UPDATE UNIFORM BUFFER
            unif.Update();
            unif.Bind();
        }
    }
}
