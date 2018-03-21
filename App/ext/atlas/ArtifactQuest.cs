using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;
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
            artifactSize,
            bandlimitFilterRadius,
            shadowFilterRadius,
            LAST,
        }
        protected static string[] UniformNames = Enum.GetNames(typeof(Names))
            .Take((int)Names.LAST).ToArray();
        
        protected static string[] UniformNamesDisk = Enum.GetNames(typeof(PoissonDisk.Names))
            .Take((int)PoissonDisk.Names.LAST).ToArray();

        private string name;
        private bool matlabConsol = false;
        [Connectable] private double lineAngle;

        #endregion

        #region QUESTS

        private float[] poissonRadii = new[] { 0.1f, 0.2f };
        private PoissonDisk[] poissonDisk;
        private Quest[] quests = new[] {
            new Quest {
                artifactSize = 10,
                poissonId = 1,
                bandlimitFilterRadius = 2f,
                shadowFilterRadius = 3f
            },
            new Quest {
                artifactSize = 10,
                poissonId = 1,
                bandlimitFilterRadius = 6f,
                shadowFilterRadius = 3f
            },
        };
        private int[] nQuests;
        private int activeQuest;
        private Random rnd = new Random();
        private const double deg2rad = Math.PI / 180;

        #endregion

        public ArtifactQuest(object @params) : base(@params)
        {
            name = @params.GetMemberValue<string>("Name");
            Convert(commands, "name", ref name);
            Convert(commands, "matlabConsol", ref matlabConsol);

            // CREATE POISSON DISCS

            var diskParams = new Params()
            {
                Name = name,
                Scene = objects,
                Commands = null,
                Debug = @params.GetMemberValue<bool>("Debug")
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

            // CREATE PSYCHOPHYSICS TOOLBOX QUESTS

            nQuests = new int[quests.Length];

            activeQuest = NextRandomQuest();
        }

        public void Update(int pipeline, int widthScreen, int heightScreen, int widthTex, int heightTex)
        {
            var sub = quests[activeQuest];
            var size = sub.artifactSize;
            var disk = poissonDisk[sub.poissonId];
            var bandlimitRadius = sub.bandlimitFilterRadius;
            var shadowRadius = sub.shadowFilterRadius;
            var numPoints = disk != null ? disk.points.GetLength(0) : 0;
            var numClosest = disk != null ? disk.indices.GetLength(1) : 0;

            var line = new float[] { (float)Math.Sin(lineAngle), (float)Math.Cos(lineAngle), 0 };
            line[2] = line[0] * widthTex / 2 + line[1] * heightTex / 2;

            // GET OR CREATE CAMERA UNIFORMS FOR program

            var unif = GetUniformBlock(pipeline, name, UniformNames);
            var unifDisk = GetUniformBlock(pipeline, name + "Disk", UniformNamesDisk);
            if (unif == null || unifDisk == null)
                return;

            // SET UNIFORM VALUES
            unif.Set((int)Names.rendertargetSize, new int[] {
                widthTex, heightTex, widthScreen, heightScreen,
            });
            unif.Set((int)Names.lineAngle, new float[] {
                (float)lineAngle, size, shadowRadius, bandlimitRadius
            });

            if (unifDisk.Has((int)PoissonDisk.Names.numPoints))
                unifDisk.Set((int)PoissonDisk.Names.numPoints, new[] { numPoints });
            if (unifDisk.Has((int)PoissonDisk.Names.numClosest))
                unifDisk.Set((int)PoissonDisk.Names.numClosest, new[] { numClosest });
            if (numPoints > 0 && unifDisk.Has((int)PoissonDisk.Names.points))
                unifDisk.Set((int)PoissonDisk.Names.points, disk.points);
            if (numClosest > 0 && unifDisk.Has((int)PoissonDisk.Names.indices))
                unifDisk.Set((int)PoissonDisk.Names.indices, disk.indices);

            // UPDATE UNIFORM BUFFER
            unif.Update();
            unif.Bind();
            unifDisk.Update();
            unifDisk.Bind();
        }

        public void KeyUp(object sender, KeyEventArgs args)
        {
            var sub = quests[activeQuest];

            switch (args.KeyData)
            {
                case Keys.Space:
                    activeQuest = NextRandomQuest();
                    return;
                case Keys.Left:
                    sub.shadowFilterRadius -= 2f;
                    if (sub.shadowFilterRadius < 1f)
                        sub.shadowFilterRadius = 1f;
                    return;
                case Keys.Right:
                    sub.shadowFilterRadius += 2f;
                    return;
                case Keys.Down:
                    sub.bandlimitFilterRadius -= 2f;
                    if (sub.bandlimitFilterRadius < 1f)
                        sub.bandlimitFilterRadius = 1f;
                    return;
                case Keys.Up:
                    sub.bandlimitFilterRadius += 2f;
                    return;
                case Keys.PageDown:
                    sub.artifactSize -= 1;
                    if (sub.artifactSize < 1)
                        sub.artifactSize = 1;
                    return;
                case Keys.PageUp:
                    sub.artifactSize += 1;
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

        private static ILookup<string, string[]> ToCommand(string key, string[] args)
        {
            return new[] {
                new KeyValuePair<string, string[]>(key, args)
            }.ToLookup(x => x.Key, x => x.Value);
        }

        private class Quest
        {
            public int artifactSize;
            public int poissonId;
            public float bandlimitFilterRadius;
            public float shadowFilterRadius;
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
