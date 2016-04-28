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
            resolutionFramebuffer,
            lineAngle,
            lineDist,
            randomAngle,
            artifactSize,
            filterRadius,
        }

        private string name;
        private float lineAngle = 0;
        private float lineDist = 0;
        private float randomAngle = 0;
        private int filterRadius = 16;
        protected Dictionary<int, UniformBlock<Names>> uniform =
            new Dictionary<int, UniformBlock<Names>>();
        #endregion

        #region MATLAB
        private MLApp.MLApp matlab = new MLApp.MLApp();
        private double[][] intensities = new[] { new[] { 0.0, 1.0 }, new[] { 0.0, 0.5 }, new[] { 0.5, 1.0 } };
        private int[] artifactSize = new[] { 5, 10, 20 };
        private double[] lineAngles = new[] { deg2rad * 1, deg2rad * 45 };
        private int[][] questsId;
        private int[] nQuests;
        private int activeQuest;
        private Random rnd = new Random();
        private const double deg2rad = Math.PI / 180;
        private const double factor = 20;
        #endregion

        public ArtifactQuest(string name, Commands cmds, GLNames glNames)
        {
            this.name = name;
            Convert(cmds, "name", ref this.name);

            // CREATE PSYCHOPHYSICS TOOLBOX QUESTS //

            nQuests = new int[intensities.Length * artifactSize.Length * lineAngles.Length];
            questsId = new int[intensities.Length * artifactSize.Length * lineAngles.Length][];

            // clear matlab workspace
            //matlab.Visible = 0;
            matlab.Execute("clear all;");

            // default parameters
            var beta = 3.5;
            var delta = 0.01;
            var gamma = 0.5;

            // for each intensity pair
            for (int intensityId = 0, questId = 0; intensityId < intensities.Length; intensityId++)
            {
                // for each artifact size
                for (int artifactId = 0; artifactId < artifactSize.Length; artifactId++)
                {
                    // for each line angle
                    for (int lineId = 0; lineId < lineAngles.Length; lineId++, questId++)
                    {
                        questsId[questId] = new int[] { intensityId, artifactId, lineId };
                        var contrast = Math.Abs(intensities[intensityId][0] - intensities[intensityId][1]);
                        var tGuess = 3.5 * artifactSize[artifactId] * contrast / factor;
                        var tGuessSd = 3.0 * artifactSize[artifactId] * contrast / factor;
                        var pThreshold = 0.82;
                        // create quest
                        matlab.Execute(string.Format(EN, "Q{0} = QuestCreate({1},{2},{3},{4},{5},{6});",
                            questId, tGuess, tGuessSd, pThreshold, beta, delta, gamma));
                    }
                }
            }

            activeQuest = NextRandomQuest();
            randomAngle = NextRandomAngle();
        }

        private int sub2ind(int intensityId, int artifactId, int lineId)
        {
            return intensities.GetLength(1) * artifactSize.Length * intensityId
                + artifactSize.Length * artifactId + lineId;
        }

        private int[] ind2sub(int index)
        {
            return questsId[index];
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
            return minIds[rnd.Next(minIds.Length)];
        }

        private float NextRandomAngle()
        {
            return (float)(360 * deg2rad * rnd.NextDouble());
        }

        public void Update(int program, int widthScreen, int heightScreen, int widthTex, int heightTex)
        {
            matlab.Execute("quantile" + activeQuest + " = QuestQuantile(Q" + activeQuest + ");");
            var quantile = MatlabVar<double>("quantile" + activeQuest);

            var sub = ind2sub(activeQuest);
            var I = intensities[sub[0]];
            var size = artifactSize[sub[1]];
            var angle = lineAngles[sub[2]];

            var line = new[] { (float)Math.Sin(angle), (float)Math.Cos(angle), 0 };
            line[2] = line[0] * widthTex / 2 + line[1] * heightTex / 2;

            // GET OR CREATE CAMERA UNIFORMS FOR program
            UniformBlock<Names> unif;
            if (uniform.TryGetValue(program, out unif) == false)
                uniform.Add(program, unif = new UniformBlock<Names>(program, name));

            // SET UNIFORM VALUES
            unif.Set(Names.lineAngle, new[] { (float)angle, line[2], randomAngle });
            unif.Set(Names.artifactSize, new[] { size, (int)Math.Round(quantile * factor) });
            unif.Set(Names.resolutionFramebuffer, new[] { widthScreen, heightScreen });

            // UPDATE UNIFORM BUFFER
            unif.Update();
            unif.Bind();
        }

        public void KeyUp(object sender, KeyEventArgs args)
        {
            var sub = ind2sub(activeQuest);
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
                    lineAngles[sub[2]] += deg2rad * 0.5;
                    return;
                case Keys.Right:
                    lineAngles[sub[2]] -= deg2rad * 0.5;
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
            //randomAngle = NextRandomAngle();
        }
    }
}
