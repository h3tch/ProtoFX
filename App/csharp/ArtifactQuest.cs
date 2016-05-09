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
        private int filterRadius = 16;
        protected Dictionary<int, UniformBlock<Names>> uniform =
            new Dictionary<int, UniformBlock<Names>>();
        protected Dictionary<int, UniformBlock<Disc>> uniformDisc =
            new Dictionary<int, UniformBlock<Disc>>();
        #endregion

        #region MATLAB
        private MLApp.MLApp matlab = new MLApp.MLApp();
        private double[][] intensities = new[] {
            new[] { 0.0, 1.0 },
            new[] { 0.0, 0.5 },
            new[] { 0.0, 0.25 },
            new[] { 0.5, 1.0 }
        };
        private int[] artifactSize = new[] { 5, 10, 20 };
        private double[] lineAngles = new[] { deg2rad * 1, deg2rad * 45 };
        private float[] poissonRadii = new[] { 0.2f, 0.1f, 0.05f };
        private PoissonDisc[] poissonDisc;
        private Quest[] quests = new[] {
            // intensity quests
            new Quest { intensityId = 0, artifactId = 1, lineId = 0, poissonId = -1},
            new Quest { intensityId = 1, artifactId = 1, lineId = 0, poissonId = -1},
            // artifact quests
            new Quest { intensityId = 0, artifactId = 0, lineId = 0, poissonId = -1},
            new Quest { intensityId = 0, artifactId = 1, lineId = 0, poissonId = -1},
            new Quest { intensityId = 0, artifactId = 2, lineId = 0, poissonId = -1},
            // line angle quests
            new Quest { intensityId = 0, artifactId = 1, lineId = 0, poissonId = -1},
            new Quest { intensityId = 0, artifactId = 1, lineId = 1, poissonId = -1},
            // poisson quests
            new Quest { intensityId = 0, artifactId = 1, lineId = 0, poissonId = 0},
            new Quest { intensityId = 0, artifactId = 1, lineId = 0, poissonId = 1},
            new Quest { intensityId = 0, artifactId = 1, lineId = 0, poissonId = 2},
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
                if (cmds.ContainsKey("minRadius"))
                    cmds.Remove("minRadius");
                cmds.Add("minRadius", new[] { poissonRadii[i].ToString(EN) });
                poissonDisc[i] = new PoissonDisc(name, cmds, glNames);
            }

            // CREATE PSYCHOPHYSICS TOOLBOX QUESTS //

            nQuests = new int[quests.Length];
            factor = artifactSize.Max();

            // clear matlab workspace
            matlab.Visible = matlabConsol ? 1 : 0;
            matlab.Execute("clear all;");

            // default parameters
            var beta = 3.5;
            var delta = 0.01;
            var gamma = 0.5;

            for (int questId = 0; questId < quests.Length; questId++)
            {
                int intensityId = quests[questId].intensityId;
                int artifactId = quests[questId].artifactId;
                var contrast = Math.Abs(intensities[intensityId][0] - intensities[intensityId][1]);
                var tGuess = 10 * artifactSize[artifactId] * contrast / factor;
                var tGuessSd = 4.0 * artifactSize[artifactId] * contrast / factor;
                var pThreshold = 0.82;
                // create quest
                matlab.Execute(string.Format(EN, "Q{0} = QuestCreate({1},{2},{3},{4},{5},{6});",
                    questId, tGuess, tGuessSd, pThreshold, beta, delta, gamma));
            }

            activeQuest = NextRandomQuest();
            randomAngle = NextRandomAngle();
        }

        public void Update(int program, int widthScreen, int heightScreen, int widthTex, int heightTex)
        {
            matlab.Execute("quantile" + activeQuest + " = QuestQuantile(Q" + activeQuest + ");");
            var quantile = MatlabVar<double>("quantile" + activeQuest);

            var sub = quests[activeQuest];
            var I = intensities[sub.intensityId];
            var size = artifactSize[sub.artifactId];
            var angle = lineAngles[sub.lineId];
            var samples = sub.poissonId < 0 ? 0 : poissonDisc[sub.poissonId].points.GetLength(0);

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
                (float)angle, randomAngle, size, (float)Math.Round(quantile * factor)
            });

            unifDisc.Set(Disc.nPoints, new[] { samples });
            if (sub.poissonId >= 0)
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
            int response = -1;

            switch (args.KeyData)
            {
                case Keys.Space:
                    response = 1;
                    break;
                case Keys.Escape:
                    response = 0;
                    break;
                case Keys.Left:
                    lineAngles[sub.lineId] += deg2rad * 0.5;
                    return;
                case Keys.Right:
                    lineAngles[sub.lineId] -= deg2rad * 0.5;
                    return;
            }

            if (response != -1)
            {
                var quest = "Q" + activeQuest;
                var quantile = "QuestQuantile(" + quest + ")";
                var cmd = quest + "=QuestUpdate(" + quest + "," + quantile + "," + response + ");";
                matlab.Execute(cmd);
            }

            activeQuest = NextRandomQuest();
            randomAngle = NextRandomAngle();
        }
        
        private T MatlabVar<T>(string var)
        {
            object val;
            matlab.GetWorkspaceData(var, "base", out val);
            return (T)val;
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

        struct Quest
        {
            public int intensityId;
            public int artifactId;
            public int lineId;
            public int poissonId;
        }

    }
}
