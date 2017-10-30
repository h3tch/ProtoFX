using OpenTK;
using System.Drawing;
using System.Windows.Forms;
using Commands = System.Linq.ILookup<string, string[]>;
using Objects = System.Collections.Generic.Dictionary<string, object>;
using GLNames = System.Collections.Generic.Dictionary<string, int>;

namespace scene
{
    class SimpleCamera : StaticCamera
    {
        #region FIELDS

        protected Point mousedown = new Point(0, 0);
        protected Point mousepos = new Point(0, 0);

        #endregion

        public SimpleCamera(string name, Commands cmds, Objects objs, GLNames glNames)
            : base(name, cmds, objs, glNames)
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
            rotx += x; if (x != 0) connections[() => rotx].Update();
            roty += y; if (y != 0) connections[() => roty].Update();
            rotz += z; if (z != 0) connections[() => rotz].Update();
        }

        private void Move(float x, float y, float z)
        {
            Vector3 v = view.Column0.Xyz * x + view.Column1.Xyz * y + view.Column2.Xyz * z;
            posx += v[0]; if (v[0] != 0) connections[() => posx].Update();
            posy += v[1]; if (v[1] != 0) connections[() => posy].Update();
            posz += v[2]; if (v[2] != 0) connections[() => posz].Update();
        }

        #endregion
    }
}