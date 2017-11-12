using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;
using Commands = System.Linq.ILookup<string, string[]>;
using Objects = System.Collections.Generic.Dictionary<string, object>;
using sampling;
using protofx;

namespace atlas
{
    class ArtifactQuest : Node
    {
        #region FIELDS

        protected enum Names
        {
            rendertargetSize,
            framebufferSize,
            lineAngle,
            randomAngle,
            artifactSize,
            filterRadius,
            moveOffset,
            LAST,
        }
        protected static string[] UniformNames = Enum.GetNames(typeof(Names))
            .Take((int)Names.LAST).ToArray();

        protected enum Disk
        {
            nPoints,
            points,
            LAST,
        }
        protected static string[] UniformNamesDisk = Enum.GetNames(typeof(Disk))
            .Take((int)Disk.LAST).ToArray();

        private string name;
        private bool matlabConsol = false;
        private float randomAngle = 0;
        [Connectable] private double moveOffset;

        #endregion

        #region QUESTS
        private const int startFactor = 10;
        private double[,] intensities = new[,] { { 0.0, 1.0 }, { 0.0, 0.5 }, { 0.5, 1.0 } };
        private int[] artifactSize = new[] { 10, 20, 30 };
        private double[] lineAngles = new[] { deg2rad * 1, deg2rad * 45 };
        private float[] poissonRadii = new[] { 0.1f, 0.2f };
        private PoissonDisk[] poissonDisk;
        private Quest[] quests = new[] {
            //// intensity quests
            //new Quest { intensityId = 0, artifactId = 2, lineId = 0, poissonId = 0, radius = 30*startFactor},
            //new Quest { intensityId = 1, artifactId = 2, lineId = 0, poissonId = 0, radius = 30*startFactor},
            //new Quest { intensityId = 2, artifactId = 2, lineId = 0, poissonId = 0, radius = 30*startFactor},
            //// artifact quests
            //new Quest { intensityId = 0, artifactId = 0, lineId = 0, poissonId = 0, radius = 10*startFactor},
            //new Quest { intensityId = 0, artifactId = 1, lineId = 0, poissonId = 0, radius = 20*startFactor},
            //new Quest { intensityId = 0, artifactId = 2, lineId = 0, poissonId = 0, radius = 30*startFactor},
            //// line angle quests
            //new Quest { intensityId = 0, artifactId = 2, lineId = 0, poissonId = 0, radius = 30*startFactor},
            //new Quest { intensityId = 0, artifactId = 2, lineId = 1, poissonId = 0, radius = 30*startFactor},
            // poisson quests
            new Quest { intensityId = 0, artifactId = 2, lineId = 0, poissonId = 0, radius = 30*startFactor},
            new Quest { intensityId = 0, artifactId = 2, lineId = 1, poissonId = 1, radius = 30*startFactor},
        };
        private int[] nQuests;
        private int activeQuest;
        private Random rnd = new Random();
        private const double deg2rad = Math.PI / 180;
        private double factor;
        #endregion

        public ArtifactQuest(object @params) : base(@params)
        {
            name = @params.GetInstanceValue<string>("Name");
            Convert(commands, "name", ref this.name);
            Convert(commands, "matlabConsol", ref matlabConsol);

            // CREATE POISSON DISCS

            var diskParams = new Params()
            {
                Name = name,
                Scene = objects,
                Commands = null,
                Debug = @params.GetInstanceValue<bool>("Debug")
            };

            poissonDisk = new PoissonDisk[poissonRadii.Length];
            for (int i = 0; i < poissonRadii.Length; i++)
            {
                if (poissonRadii[i] <= 0f)
                    continue;
                var radius = ToCommand("minRadius", new[] { poissonRadii[i].ToString(EN) });
                diskParams.Commands = commands.Where(x => x.Key != "minRadius")
                                              .Concat(radius)
                                              .ToLookup(x => x.Key, x => x.First());
                poissonDisk[i] = new PoissonDisk(diskParams);
            }

            // CREATE PSYCHOPHYSICS TOOLBOX QUESTS //

            nQuests = new int[quests.Length];
            factor = artifactSize.Max();

            activeQuest = NextRandomQuest();
            randomAngle = NextRandomAngle();
        }

        public void Update(int pipeline, int widthScreen, int heightScreen, int widthTex, int heightTex)
        {
            var sub = quests[activeQuest];
            var size = artifactSize[sub.artifactId];
            var angle = lineAngles[sub.lineId];
            var samples = poissonDisk[sub.poissonId] != null
                ? poissonDisk[sub.poissonId].points.GetLength(0) : 0;

            var line = new float[] { (float)Math.Sin(angle), (float)Math.Cos(angle), 0 };
            line[2] = line[0] * widthTex / 2 + line[1] * heightTex / 2;

            // GET OR CREATE CAMERA UNIFORMS FOR program

            var unif = GetUniformBlock(pipeline, name, UniformNames);
            var unifDisc = GetUniformBlock(pipeline, name + "Disk", UniformNamesDisk);
            if (unif == null || unifDisc == null)
                return;

            // SET UNIFORM VALUES
            unif.Set((int)Names.rendertargetSize, new int[] {
                widthTex, heightTex, widthScreen, heightScreen,
            });
            unif.Set((int)Names.lineAngle, new float[] {
                (float)angle, randomAngle, size, sub.radius, (float)moveOffset
            });

            unifDisc.Set((int)Disk.nPoints, new[] { samples });
            if (samples > 0)
                unifDisc.Set((int)Disk.points, poissonDisk[sub.poissonId].points);
            
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

        private static Commands ToCommand(string key, string[] args)
        {
            return new[] {
                new KeyValuePair<string, string[]>(key, args)
            }.ToLookup(x => x.Key, x => x.Value);
        }

        private class Quest
        {
            public int intensityId;
            public int artifactId;
            public int lineId;
            public int poissonId;
            public int radius;
        }

        class Params
        {
            public string Name;
            public Dictionary<string, object> Scene;
            public ILookup<string, string[]> Commands;
            public bool Debug;
        }
    }
}
