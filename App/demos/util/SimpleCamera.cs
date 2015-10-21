using OpenTK;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace util
{
    using Commands = Dictionary<string, string[]>;

    class SimpleCamera : StaticCamera
    {
        #region FIELDS
        private Point mousedown = new Point(0, 0);
        private Point mousepos = new Point(0, 0);
        #endregion

        public SimpleCamera(Commands cmd)
            : base(cmd, "SimpleCamera")
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
                Rotate((float)(Math.PI / 360) * (mousepos.Y - e.Y),
                    (float)(Math.PI / 360) * (mousepos.X - e.X), 0);
            else if (e.Button == MouseButtons.Right)
                Move(0, 0, 0.03f * (e.Y - mousepos.Y));
            mousepos.X = e.X;
            mousepos.Y = e.Y;
        }

        public void Resize(object sender, EventArgs e)
        {
            GLControl gl = (GLControl)sender;
            aspect = (float)gl.Width / gl.Height;
        }
        #endregion

        #region PRIVATE UTILITY METHODS
        private void Rotate(float x, float y, float z)
        {
            rot.X += x;
            rot.Y += y;
            rot.Z += z;
        }

        private void Move(float x, float y, float z)
        {
            pos += view.Column0.Xyz * x + view.Column1.Xyz * y + view.Column2.Xyz * z;
        }
        #endregion
    }
}