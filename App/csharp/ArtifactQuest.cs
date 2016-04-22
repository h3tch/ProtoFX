using MLApp;
using System.Collections.Generic;
using System.Windows.Forms;
using Commands = System.Collections.Generic.Dictionary<string, string[]>;
using GLNames = System.Collections.Generic.Dictionary<string, int>;

namespace csharp
{
    class ArtifactQuest : CsObject
    {
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
        private float artifactSize = 5;
        private int filterRadius = 16;
        protected Dictionary<int, UniformBlock<Names>> uniform =
            new Dictionary<int, UniformBlock<Names>>();

        public ArtifactQuest(string name, Commands cmds, GLNames glNames)
        {
            this.name = name;
            Convert(cmds, "name", ref this.name);
        }

        public void Update(int program, int width, int height, int widthTex, int heightTex)
        {
            // GET OR CREATE CAMERA UNIFORMS FOR program
            UniformBlock<Names> unif;
            if (uniform.TryGetValue(program, out unif) == false)
                uniform.Add(program, unif = new UniformBlock<Names>(program, name));

            // SET UNIFORM VALUES
            if (unif[Names.lineAngle] >= 0)
                unif.Set(Names.lineAngle, new[] { lineAngle, lineDist, randomAngle, artifactSize }.AsInt32());

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
