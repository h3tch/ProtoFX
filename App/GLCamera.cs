using OpenTK;
using System;

namespace gled
{
    class GLCamera : GLObject
    {
        public static string cameraname = "__glst_camera__";
        private Vector3 pos;
        private Vector3 rot;
        public Vector4 info;
        public Matrix4 view;
        public Matrix4 proj;
        public Matrix4 viewproj;

        public GLCamera()
            : base(null, null)
        {
            pos = Vector3.Zero;
            rot = Vector3.Zero;
            info = new Vector4(60f * (float)Math.PI / 180f, 16f / 9f, 0.1f, 100.0f);
        }

        public void Rotate(float x, float y, float z)
        {
            rot += new Vector3(x, y, z);
        }

        public void Move(float x, float y, float z)
        {
            pos += view.Column0.Xyz * x + view.Column1.Xyz * y + view.Column2.Xyz * z;
        }

        public void Proj(float fovy, float aspect, float znear, float zfar)
        {
            info = new Vector4(fovy, aspect, znear, zfar);
        }

        public void Update()
        {
            view = Matrix4.CreateTranslation(-pos) * Matrix4.CreateRotationY(-rot[1]) * Matrix4.CreateRotationX(-rot[0]);
            proj = Matrix4.CreatePerspectiveFieldOfView(info.X, info.Y, info.Z, info.W);
            viewproj = view * proj;
        }

        public override void Delete()
        {
        }
    }
}
