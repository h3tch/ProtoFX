using OpenTK;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace csharp
{
    using Commands = Dictionary<string, string[]>;

    class TurntableCamera : StaticCamera
    {
        #region FIELDS
        private Point mousedown = new Point(0, 0);
        private Point mousepos = new Point(0, 0);
        #endregion

        #region PROPERTIES
        private float Dist
        { get { return (float)Math.Sqrt(pos[0] * pos[0] + pos[1] * pos[1] + pos[2] * pos[2]); } }
        #endregion

        public TurntableCamera(string name, Commands cmds)
            : base(name, cmds)
        {
            float tilt = rot[0], yaw = rot[1], dist = Dist;
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
            Turntable(rot[0] + deltaTilt, rot[1] + deltaYaw, Dist);
        }

        private void Move(float delta)
        {
            Turntable(rot[0], rot[1], Dist + delta);
        }

        private void Turntable(float tilt, float yaw, float dist)
        {
            // set rotation
            rot[0] = tilt;
            rot[1] = yaw;
            rot[2] = 0f;
            // set position
            var camRotM = Matrix4.CreateRotationY(-rot[1] * rad2deg)
                        * Matrix4.CreateRotationX(-rot[0] * rad2deg);
            var camPos = camRotM.Column2.Xyz * dist;
            pos[0] = camPos[0];
            pos[1] = camPos[1];
            pos[2] = camPos[2];
        }
        #endregion
    }
}
