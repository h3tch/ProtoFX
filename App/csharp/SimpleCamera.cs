using OpenTK;
using System.Drawing;
using System.Windows.Forms;
using Commands = System.Collections.Generic.Dictionary<string, string[]>;
using GLNames = System.Collections.Generic.Dictionary<string, int>;

namespace csharp
{
    class SimpleCamera : StaticCamera
    {
        #region FIELDS

        private Point mousedown = new Point(0, 0);
        private Point mousepos = new Point(0, 0);

        #endregion

        public SimpleCamera(string name, Commands cmds, GLNames glNames)
            : base(name, cmds, glNames)
        {
        }

        #region OPENTK GLCONTROL WINDOW EVENTS

        public void MouseDown(object sender, MouseEventArgs e)
        {
            mousedown.X = mousepos.X = e.X;
            mousedown.Y = mousepos.Y = e.Y;
        }

        public void MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                Rotate(0.1f * (mousepos.Y - e.Y), 0.1f * (mousepos.X - e.X), 0);
            else if (e.Button == MouseButtons.Right)
                Move(0, 0, 0.03f * (e.Y - mousepos.Y));
            mousepos.X = e.X;
            mousepos.Y = e.Y;
        }

        #endregion

        #region PRIVATE UTILITY METHODS

        private void Rotate(float x, float y, float z)
        {
            rot[0] += x;
            rot[1] += y;
            rot[2] += z;
        }

        private void Move(float x, float y, float z)
        {
            Vector3 v = view.Column0.Xyz * x + view.Column1.Xyz * y + view.Column2.Xyz * z;
            pos[0] += v[0];
            pos[1] += v[1];
            pos[2] += v[2];
        }

        #endregion
    }
}