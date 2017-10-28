using OpenTK;
using System;
using System.Drawing;
using System.Windows.Forms;
using Commands = System.Linq.ILookup<string, string[]>;
using Objects = System.Collections.Generic.Dictionary<string, object>;
using GLNames = System.Collections.Generic.Dictionary<string, int>;

namespace camera
{
    class TurntableCamera : StaticCamera
    {
        #region FIELDS
        private Point mousedown = new Point(0, 0);
        private Point mousepos = new Point(0, 0);
        #endregion

        #region PROPERTIES
        private float Dist { get { return (float)Math.Sqrt(posx * posx + posy * posy + posz * posz); } }
        #endregion

        public TurntableCamera(string name, Commands cmds, Objects objs, GLNames glNames)
            : base(name, cmds, objs, glNames)
        {
            float tilt = rotx, yaw = roty, dist = Dist;
            Convert(cmds, "tilt", ref tilt);
            Convert(cmds, "yaw", ref yaw);
            Convert(cmds, "dist", ref dist);
            Turntable(tilt, yaw, dist);
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
                Rotate(0.1f * (mousepos.Y - e.Y), 0.1f * (mousepos.X - e.X));
            else if (e.Button == MouseButtons.Right)
                Move(0.03f * (e.Y - mousepos.Y));
            mousepos.X = e.X;
            mousepos.Y = e.Y;
        }
        #endregion

        #region PRIVATE UTILITY METHODS
        private void Rotate(float deltaTilt, float deltaYaw)
        {
            Turntable(rotx + deltaTilt, roty + deltaYaw, Dist);
        }

        private void Move(float delta)
        {
            Turntable(rotx, roty, Dist + delta);
        }

        private void Turntable(float tilt, float yaw, float dist)
        {
            // set rotation
            rotx = tilt;
            roty = yaw;
            rotz = 0f;
            // set position
            var camRotM = Matrix4.CreateRotationY(-roty * deg2rad)
                        * Matrix4.CreateRotationX(-rotx * deg2rad);
            var camPos = camRotM.Column2.Xyz * dist;
            posx = camPos[0];
            posy = camPos[1];
            posz = camPos[2];
        }
        #endregion
    }
}
