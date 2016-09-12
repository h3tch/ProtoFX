using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;
using Commands = System.Collections.Generic.Dictionary<string, string[]>;
using GLNames = System.Collections.Generic.Dictionary<string, int>;

namespace csharp
{
    class ArtifactQuest : CsObject
    {
        #region FIELDS
        public enum Names
        {
            rendertargetSize,
            framebufferSize,
            lineAngle,
            randomAngle,
            artifactSize,
            filterRadius,
        }

        public enum Disc
        {
            nPoints,
            points,
        }

        private string name;
        private bool matlabConsol = false;
        private float randomAngle = 0;
        protected Dictionary<int, UniformBlock<Names>> uniform =
            new Dictionary<int, UniformBlock<Names>>();
        protected Dictionary<int, UniformBlock<Disc>> uniformDisc =
            new Dictionary<int, UniformBlock<Disc>>();
        #endregion

        #region QUESTS
        private const int startFactor = 10;
        private double[,] intensities = new[,] { { 0.0, 1.0 }, { 0.0, 0.5 }, { 0.5, 1.0 } };
        private int[] artifactSize = new[] { 10, 20, 30 };
        private double[] lineAngles = new[] { deg2rad * 1, deg2rad * 45 };
        private float[] poissonRadii = new[] { 0.0f, 0.1f, 0.2f };
        private PoissonDisc[] poissonDisc;
        private Quest[] quests = new[] {
            // intensity quests
            new Quest { intensityId = 0, artifactId = 2, lineId = 0, poissonId = 0, radius = 30*startFactor},
            new Quest { intensityId = 1, artifactId = 2, lineId = 0, poissonId = 0, radius = 30*startFactor},
            new Quest { intensityId = 2, artifactId = 2, lineId = 0, poissonId = 0, radius = 30*startFactor},
            // artifact quests
            new Quest { intensityId = 0, artifactId = 0, lineId = 0, poissonId = 0, radius = 10*startFactor},
            new Quest { intensityId = 0, artifactId = 1, lineId = 0, poissonId = 0, radius = 20*startFactor},
            new Quest { intensityId = 0, artifactId = 2, lineId = 0, poissonId = 0, radius = 30*startFactor},
            // line angle quests
            new Quest { intensityId = 0, artifactId = 2, lineId = 0, poissonId = 0, radius = 30*startFactor},
            new Quest { intensityId = 0, artifactId = 2, lineId = 1, poissonId = 0, radius = 30*startFactor},
            // poisson quests
            new Quest { intensityId = 0, artifactId = 2, lineId = 0, poissonId = 0, radius = 30*startFactor},
            new Quest { intensityId = 0, artifactId = 2, lineId = 0, poissonId = 1, radius = 30*startFactor},
            new Quest { intensityId = 0, artifactId = 2, lineId = 0, poissonId = 2, radius = 30*startFactor},
        };
        private int[] nQuests;
        private int activeQuest;
        private Random rnd = new Random();
        private const double deg2rad = Math.PI / 180;
        private double factor;
        #endregion

        public ArtifactQuest(string name, Commands cmds, GLNames glNames)
        {
            this.name = name;
            Convert(cmds, "name", ref this.name);
            Convert(cmds, "matlabConsol", ref matlabConsol);

            // CREATE POISSON DISCS

            poissonDisc = new PoissonDisc[poissonRadii.Length];
            for (int i = 0; i < poissonRadii.Length; i++)
            {
                if (poissonRadii[i] <= 0f)
                    continue;
                if (cmds.ContainsKey("minRadius"))
                    cmds.Remove("minRadius");
                cmds.Add("minRadius", new[] { poissonRadii[i].ToString(EN) });
                poissonDisc[i] = new PoissonDisc(name, cmds, glNames);
            }

            // CREATE PSYCHOPHYSICS TOOLBOX QUESTS //

            nQuests = new int[quests.Length];
            factor = artifactSize.Max();

            activeQuest = NextRandomQuest();
            randomAngle = NextRandomAngle();
        }

        public void Update(int program, int widthScreen, int heightScreen, int widthTex, int heightTex)
        {
            var sub = quests[activeQuest];
            var size = artifactSize[sub.artifactId];
            var angle = lineAngles[sub.lineId];
            var samples = poissonDisc[sub.poissonId] != null
                ? poissonDisc[sub.poissonId].points.GetLength(0) : 0;

            var line = new[] { (float)Math.Sin(angle), (float)Math.Cos(angle), 0 };
            line[2] = line[0] * widthTex / 2 + line[1] * heightTex / 2;

            // GET OR CREATE CAMERA UNIFORMS FOR program
            UniformBlock<Names> unif;
            UniformBlock<Disc> unifDisc;
            if (uniform.TryGetValue(program, out unif) == false)
                uniform.Add(program, unif = new UniformBlock<Names>(program, name));
            if (uniformDisc.TryGetValue(program, out unifDisc) == false)
                uniformDisc.Add(program, unifDisc = new UniformBlock<Disc>(program, name + "Disc"));

            // SET UNIFORM VALUES
            unif.Set(Names.rendertargetSize, new[] {
                widthTex, heightTex, widthScreen, heightScreen,
            });
            unif.Set(Names.lineAngle, new[] {
                (float)angle, randomAngle, size, sub.radius
            });

            unifDisc.Set(Disc.nPoints, new[] { samples });
            if (samples > 0)
                unifDisc.Set(Disc.points, poissonDisc[sub.poissonId].points);
            
            // UPDATE UNIFORM BUFFER
            unif.Update();
            unif.Bind();
            unifDisc.Update();
            unifDisc.Bind();
        }

        public void KeyUp(object sender, KeyEventArgs args)
        {
            var sub = quests[activeQuest];

            switch (args.KeyData)
            {
                case Keys.Space:
                    activeQuest = NextRandomQuest();
                    randomAngle = NextRandomAngle();
                    return;
                case Keys.Left:
                    sub.radius -= 5;
                    if (sub.radius < 1)
                        sub.radius = 1;
                    return;
                case Keys.Right:
                    sub.radius += 5;
                    return;
            }
        }
        
        private int NextRandomQuest()
        {
            var min = nQuests.Min();
            var minIds = Enumerable.Range(0, nQuests.Length).Where(x => nQuests[x] == min).ToArray();
            var minId = minIds[rnd.Next(minIds.Length)];
            nQuests[minId]++;
            return minId;
        }

        private float NextRandomAngle()
        {
            return (float)(360 * deg2rad * rnd.NextDouble());
        }

        private class Quest
        {
            public int intensityId;
            public int artifactId;
            public int lineId;
            public int poissonId;
            public int radius;
        }

    }
}
