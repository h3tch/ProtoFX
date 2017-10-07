using protofx;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Commands = System.Collections.Generic.Dictionary<string, string[]>;
using GLNames = System.Collections.Generic.Dictionary<string, int>;

namespace animation
{
    class Interpolate : CsObject
    {
        public enum Names
        {
            value
        }

        protected string name;
        private double from = 0;
        private double to = 1;
        private double value = 0;
        private double speed = 1;
        private bool reflect = false;
        private bool smooth = false;
        private Stopwatch stopwatch = new Stopwatch();
        protected Dictionary<int, UniformBlock<Names>> uniform =
            new Dictionary<int, UniformBlock<Names>>();

        public Interpolate(string name, Commands cmds, GLNames glNames)
        {
            this.name = name;
            Convert(cmds, "name", ref this.name);
            Convert(cmds, "from", ref from);
            Convert(cmds, "start", ref from);
            Convert(cmds, "to", ref to);
            Convert(cmds, "end", ref to);
            Convert(cmds, "speed", ref speed);
            Convert(cmds, "reflect", ref reflect);
            Convert(cmds, "smooth", ref smooth);
        }

        public void Update(int pipeline, int width, int height, int widthTex, int heightTex)
        {
            // GET OR CREATE CAMERA UNIFORMS FOR program
#pragma warning disable IDE0018
            UniformBlock<Names> unif;
#pragma warning restore IDE0018
            if (uniform.TryGetValue(pipeline, out unif) == false)
                uniform.Add(pipeline, unif = new UniformBlock<Names>(pipeline, name));

            // increase value based on the elapsed time
            if (stopwatch.IsRunning)
                value = (value + (stopwatch.ElapsedMilliseconds / 1000.0) * speed) % 1;

            // restart the stop watch
            stopwatch.Restart();

            // reflect and smooth the value if necessary
            var t = reflect ? Math.Abs(value * 2 - 1) : value;
            if (smooth)
                t = 1 - Math.Cos(Math.PI * t) * 0.5 + 0.5;

            // interpolate between 'from' and 'to' value
            t = (float)(from * (1 - t) + to * t);

            unif.Set(Names.value, new[] { (float)t });

            // UPDATE UNIFORM BUFFER
            unif.Update();
            unif.Bind();
        }

        public void Delete()
        {
            foreach (var u in uniform)
                u.Value.Delete();
        }
    }
}
