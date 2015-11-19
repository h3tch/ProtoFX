using OpenTK;
using System.Drawing;
using System.Windows.Forms;

namespace App
{
    class DebugTexture : PictureBox
    {
        private GLControl glControl;

        public DebugTexture(GLControl glControl)
        {
            this.glControl = glControl;
        }

        public void SetDebug(int ID, int Layer, int Level)
        {
            glControl.MakeCurrent();
            var bmp = GLImage.Read(ID, Level, Layer);
            bmp.RotateFlip(RotateFlipType.RotateNoneFlipY);
            Image = bmp;
        }
    }
}
