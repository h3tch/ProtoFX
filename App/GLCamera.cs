using OpenTK;
using System;

namespace gled
{
    class GLCamera : GLObject
    {
        public static string cameraname { get; } = "__glst_camera__";
        private Vector3 pos;
        private Vector3 rot;
        public Vector4 info { get; protected set; }
        public Matrix4 view {
            get
            {
                return (mview.M11 == float.NaN) ? mview = Matrix4.CreateTranslation(-pos) * Matrix4.CreateRotationX(-rot[1]) * Matrix4.CreateRotationX(-rot[0]) : mview;
            }
        }
        public Matrix4 mview;
        public Matrix4 proj
        {
            get
            {
                return (mproj.M11 == float.NaN) ? mproj = Matrix4.CreatePerspectiveFieldOfView(info.W, info.X, info.Y, info.Z) : mproj;
            }
        }
        public Matrix4 mproj;
        public Matrix4 viewproj
        {
            get
            {
                return (mviewproj.M11 == float.NaN) ? mviewproj = proj * view : mviewproj;
            }
        }
        public Matrix4 mviewproj;

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
            mview.M11 = float.NaN;
            mviewproj.M11 = float.NaN;
        }

        public void Move(float x, float y, float z)
        {
            pos += new Vector3(x, y, z);
            mview.M11 = float.NaN;
            mviewproj.M11 = float.NaN;
        }

        public void Proj(float fovy, float aspect, float znear, float zfar)
        {
            info = new Vector4(fovy, aspect, znear, zfar);
            mproj.M11 = float.NaN;
            mviewproj.M11 = float.NaN;
        }

        public override void Bind(int unit)
        {

        }

        public override void Unbind(int unit)
        {

        }

        public override void Delete()
        {
        }
    }
}
