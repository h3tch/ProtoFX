using protofx;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Commands = System.Linq.ILookup<string, string[]>;
using Objects = System.Collections.Generic.Dictionary<string, object>;
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
        private protofx.Double value = new protofx.Double();
        private protofx.Double t = new protofx.Double();
        private double speed = 1;
        private bool reflect = false;
        private bool smooth = false;
        private Stopwatch stopwatch = new Stopwatch();
        protected Dictionary<int, UniformBlock<Names>> uniform =
            new Dictionary<int, UniformBlock<Names>>();

        public Interpolate(string name, Commands cmds, Objects objs, GLNames glNames)
            : base(cmds, objs)
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
            InitializeConnections();

            // increase value based on the elapsed time
            if (stopwatch.IsRunning)
            {
                value.value = (value + (stopwatch.ElapsedMilliseconds / 1000.0) * speed) % 1;
                value.Update();
            }

            // restart the stop watch
            stopwatch.Restart();

            // reflect and smooth the value if necessary
            t.value = reflect ? Math.Abs(value * 2 - 1) : value;
            if (smooth)
                t.value = 1 - Math.Cos(Math.PI * t) * 0.5 + 0.5;

            // interpolate between 'from' and 'to' value
            t.value = from * (1 - t) + to * t;
            t.Update();

            // GET OR CREATE CAMERA UNIFORMS FOR program
            var unif = GetUniformBlock(uniform, pipeline, name);
            if (unif == null)
                return;

            unif.Set(Names.value, new[] { (float)t });

            // UPDATE UNIFORM BUFFER
            unif.Update();
            unif.Bind();
        }

        public void Delete()
        {
            foreach (var u in uniform)
            {
                if (u.Value != null)
                    u.Value.Delete();
            }
        }
    }
}
