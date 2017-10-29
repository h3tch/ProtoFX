using OpenTK;
using System;
using System.Windows.Forms;
using Commands = System.Linq.ILookup<string, string[]>;
using Objects = System.Collections.Generic.Dictionary<string, object>;
using GLNames = System.Collections.Generic.Dictionary<string, int>;

namespace scene
{
    class TurntableCamera : SimpleCamera
    {
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

        public new void MouseDown(object sender, MouseEventArgs e)
        {
            mousedown.X = mousepos.X = e.X;
            mousedown.Y = mousepos.Y = e.Y;
        }

        public new void MouseMove(object sender, MouseEventArgs e)
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
            rotx = tilt; if (rotx != tilt) Propagate(() => rotx);
            roty = yaw; if (roty != yaw) Propagate(() => roty);
            rotz = 0f; if (rotz != 0f) Propagate(() => rotz);
            // set position
            var camRotM = Matrix4.CreateRotationY(-roty * deg2rad)
                        * Matrix4.CreateRotationX(-rotx * deg2rad);
            var camPos = camRotM.Column2.Xyz * dist;
            posx = camPos[0]; if (camPos[0] != 0) Propagate(() => posx);
            posy = camPos[1]; if (camPos[1] != 0) Propagate(() => posy);
            posz = camPos[2]; if (camPos[2] != 0) Propagate(() => posz);
        }

        #endregion
    }
}
