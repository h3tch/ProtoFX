using protofx;
using OpenTK;
using System;
using System.Collections.Generic;
using Commands = System.Linq.ILookup<string, string[]>;
using Objects = System.Collections.Generic.Dictionary<string, object>;
using GLNames = System.Collections.Generic.Dictionary<string, int>;

namespace camera
{

    class SpotLight : CsObject
    {
        public enum Names
        {
            view,
            proj,
            viewProj,
            camera,
            color,
            light,
        }

        #region FIELDS
        public protofx.Double posx = new protofx.Double();
        public protofx.Double posy = new protofx.Double();
        public protofx.Double posz = new protofx.Double();
        public protofx.Double rotx = new protofx.Double();
        public protofx.Double roty = new protofx.Double();
        public protofx.Double rotz = new protofx.Double();
        public protofx.Double fov = new protofx.Double();
        public protofx.Double near = new protofx.Double();
        public protofx.Double far = new protofx.Double();
        public protofx.Double colorr = new protofx.Double();
        public protofx.Double colorg = new protofx.Double();
        public protofx.Double colorb = new protofx.Double();
        public protofx.Double intensity = new protofx.Double();
        public protofx.Double innerCone = new protofx.Double();
        public protofx.Double radius = new protofx.Double();
        protected const float deg2rad = (float)(Math.PI / 180);
        protected string name;
        protected Dictionary<int, UniformBlock<Names>> uniform =
            new Dictionary<int, UniformBlock<Names>>();
        #endregion

        #region PROPERTIES

        public float[] Position
        {
            get { return new float[] { posx, posy, posz }; }
            set { posx.value = value[0]; posy.value = value[1]; posz.value = value[2]; }
        }
        public float[] Rotation
        {
            get { return new float[] { rotx, roty, rotz }; }
            set { rotx.value = value[0]; roty.value = value[1]; rotz.value = value[2]; }
        }
        public float FieldOfViewY { get { return fov; } set { fov.value = value; } }
        public float NearPlane { get { return near; } set { near.value = value; } }
        public float FarPlane { get { return far; } set { far.value = value; } }
        public float[] Color
        {
            get { return new float[] { colorr, colorg, colorb }; }
            set { colorr.value = value[0]; colorg.value = value[1]; colorb.value = value[2]; }
        }
        public float Intensity { get { return intensity; } set { intensity.value = value; } }
        public float Radius { get { return radius; } set { radius.value = value; } }
        public float InnerCone { get { return innerCone; } set { innerCone.value = value; } }

        #endregion

        public SpotLight(string name, Commands cmds, Objects objs, GLNames glNames)
            : base(cmds, objs)
        {
            this.name = name;
            // PARSE COMMAND VALUES SPECIFIED BY THE USER
            float[] pos = new float[3] { 0f, 0f, 0f };
            float[] rot = new float[3] { 0f, 0f, 0f };
            float[] color = new float[3] { 1f, 1f, 1f };
            float fov = 60f, near = 0.1f, far = 100f;
            float intensity = 100f, innerCone = 40f, radius = 0.1f;
            Convert(cmds, "name", ref this.name);
            Convert(cmds, "pos", ref pos);
            Convert(cmds, "rot", ref rot);
            Convert(cmds, "fov", ref fov);
            Convert(cmds, "near", ref near);
            Convert(cmds, "far", ref far);
            Convert(cmds, "color", ref color);
            Convert(cmds, "intensity", ref intensity);
            Convert(cmds, "innerCone", ref innerCone);
            Convert(cmds, "radius", ref radius);
            posx.value = pos[0];
            posy.value = pos[1];
            posz.value = pos[2];
            rotx.value = rot[0];
            roty.value = rot[1];
            rotz.value = rot[2];
            this.fov.value = fov;
            this.near.value = near;
            this.far.value = far;
            this.intensity.value = intensity;
            this.innerCone.value = innerCone;
            this.radius.value = radius;
        }

        public void Update(int pipeline, int width, int height, int widthTex, int heightTex)
        {
            InitializeConnections();

            // GET OR CREATE CAMERA UNIFORMS FOR program
            var unif = GetUniformBlock(uniform, pipeline, name);
            if (unif == null)
                return;

            // COMPUTE CAMERA ORIENTATION
            var view = Matrix4.CreateTranslation(-posx, -posy, -posz)
                 * Matrix4.CreateRotationY(-roty * deg2rad)
                 * Matrix4.CreateRotationX(-rotx * deg2rad);
            var aspect = (float)width / height;
            float fovRad = (float)(fov * deg2rad);
            var proj = Matrix4.CreatePerspectiveFieldOfView(fovRad, aspect, near, far);

            // SET UNIFORM VALUES
            unif.Set(Names.view, view.AsInt32());

            unif.Set(Names.proj, proj.AsInt32());

            if (unif.Has(Names.viewProj))
                unif.Set(Names.viewProj, (view * proj).AsInt32());

            if (unif.Has(Names.camera))
                unif.Set(Names.camera, new float[] { fovRad, aspect, near, far }.AsInt32());

            if (unif.Has(Names.color))
                unif.Set(Names.color, new float[] { colorr, colorg, colorb, intensity }.AsInt32());

            if (unif.Has(Names.light))
            {
                var x = (float)(near * Math.Tan(fovRad));
                var y = x / aspect;
                unif.Set(Names.light, new float[] { (float)(innerCone * deg2rad), radius, x, y }.AsInt32());
            }

            // UPDATE UNIFORM BUFFER
            unif.Update();
            unif.Bind();
        }

        public void Delete()
        {
            foreach (var u in uniform)
            {
                if (u.Value != null)
                    u.Value.Delete();
            }
        }
    }
}
