using MLApp;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
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
        private double[,] intensities = new[,] { { 0.0, 1.0 }, { 0.0, 0.5 }, { 0.5, 1.0 } };
        private int[] artifactSize = new[] { 5, 10, 20 };
        private double[] lineAngles = new[] { 0.1, 0.7 };
        #endregion

        public ArtifactQuest(string name, Commands cmds, GLNames glNames)
        {
            this.name = name;
            Convert(cmds, "name", ref this.name);

            // CREATE PSYCHOPHYSICS TOOLBOX QUESTS //

            // clear matlab workspace
            matlab.Execute("clear all;");

            // default parameters
            var beta = 3.5;
            var delta = 0.01;
            var gamma = 0.5;

            // for each line angle
            for (int lineId = 0, questId = 0; lineId < lineAngles.Length; lineId++)
            {
                // for each intensity pair
                for (int intensityId = 0; intensityId < intensities.GetLength(1); intensityId++)
                {
                    // for each artifact size
                    for (int artifactId = 0; artifactId < artifactSize.Length; artifactId++, questId++)
                    {
                        var contrast = Math.Abs(intensities[intensityId, 0] - intensities[intensityId, 1]);
                        var tGuess = 0.03 * artifactSize[artifactId] * contrast;
                        var tGuessSd = 0.028 * artifactSize[artifactId] * contrast;
                        var pThreshold = 0.82;
                        // create quest
                        matlab.Execute(string.Format(EN, "Q{0} = QuestCreate({1},{2},{3},{4},{5},{6});",
                            questId, tGuess, tGuessSd, pThreshold, beta, delta, gamma));
                    }
                }
            }
        }

        public void Update(int program, int width, int height, int widthTex, int heightTex)
        {
            // GET OR CREATE CAMERA UNIFORMS FOR program
            UniformBlock<Names> unif;
            if (uniform.TryGetValue(program, out unif) == false)
                uniform.Add(program, unif = new UniformBlock<Names>(program, name));

            // SET UNIFORM VALUES
            if (unif[Names.lineAngle] >= 0)
                unif.Set(Names.lineAngle, new[] { lineAngle, lineDist, randomAngle, artifactSize[0] });

            if (unif[Names.filterRadius] >= 0)
                unif.Set(Names.filterRadius, new[] { filterRadius });

            if (unif[Names.resolutionFramebuffer] >= 0)
                unif.Set(Names.resolutionFramebuffer, new[] { width, height });

            // UPDATE UNIFORM BUFFER
            unif.Update();
            unif.Bind();
        }

        public void KeyUp(object sender, KeyEventArgs args)
        {
            if (args.KeyCode == Keys.LControlKey)
            {

            }
            else if (args.KeyCode == Keys.RControlKey)
            {

            }
        }
    }
}
