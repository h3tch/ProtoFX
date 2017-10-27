using protofx;
using OpenTK;
using System;
using System.Collections.Generic;
using Commands = System.Linq.ILookup<string, string[]>;
using Objects = System.Collections.Generic.Dictionary<string, object>;
using GLNames = System.Collections.Generic.Dictionary<string, int>;

namespace camera
{

    class StaticCamera : CsObject
    {
        public enum Names
        {
            view,
            proj,
            viewProj,
            viewInv,
            projInv,
            viewProjInv,
            camera,
            position,
            rotation,
            direction,
            nearPlane,
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
        protected const float deg2rad = (float)(Math.PI / 180);
        protected string name;
        protected Matrix4 view;
        protected Dictionary<int, UniformBlock<Names>> uniform =
            new Dictionary<int, UniformBlock<Names>>();

        #endregion

        #region PROPERTIES

        public string Name { get { return name; } }
        public float[] Position {
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
        
        #endregion

        public StaticCamera(string name, Commands cmds, Objects objs, GLNames glNames)
            : base(cmds, objs)
        {
            // The constructor is executed only once when the pass is created.

            // ProtoGL code:
            // exec csharp_name util.SimpleCamera fovy nearz farz x y z rotx roty rotz ...
            //      uniform_view_name uniform_proj_name uniform_view_proj_name uniform_info_name

            // argument cmd contains the whole command including
            // 'exec', 'csharp_name' and 'util.SimpleCamera'

            // parse command for values specified by the user

            this.name = name;
            // PARSE COMMAND VALUES SPECIFIED BY THE USER
            float[] pos = new float[3] { 0f, 0f, 0f };
            float[] rot = new float[3] { 0f, 0f, 0f };
            float fov = 60f, near = 0.1f, far = 100f;
            Convert(cmds, "name", ref this.name);
            Convert(cmds, "pos", ref pos);
            Convert(cmds, "rot", ref rot);
            Convert(cmds, "fov", ref fov);
            Convert(cmds, "near", ref near);
            Convert(cmds, "far", ref far);
            posx.value = pos[0];
            posy.value = pos[1];
            posz.value = pos[2];
            rotx.value = rot[0];
            roty.value = rot[1];
            rotz.value = rot[2];
            this.fov.value = fov;
            this.near.value = near;
            this.far.value = far;
        }

        public void Update(int pipeline, int width, int height, int widthTex, int heightTex)
        {
            InitializeConnections();

            // GET OR CREATE CAMERA UNIFORMS FOR program
            var unif = GetUniformBlock(uniform, pipeline, name);
            if (unif == null)
                return;

            // This function is executed every frame at the beginning of a pass.
            view = Matrix4.CreateTranslation(-posx, -posy, -posz)
                 * Matrix4.CreateRotationY(-roty * deg2rad)
                 * Matrix4.CreateRotationX(-rotx * deg2rad);
            float aspect = (float)width / height;
            float angle = (float)(fov * deg2rad);
            Matrix4 proj = Matrix4.CreatePerspectiveFieldOfView(angle, aspect, near, far);

            // SET UNIFORM VALUES
            if (unif.Has(Names.view))
                unif.Set(Names.view, view.AsInt32());

            if (unif.Has(Names.proj))
                unif.Set(Names.proj, proj.AsInt32());

            if (unif.Has(Names.viewProj))
                unif.Set(Names.viewProj, (view * proj).AsInt32());

            if (unif.Has(Names.viewInv))
                unif.Set(Names.viewInv, view.Inverted().AsInt32());

            if (unif.Has(Names.projInv))
                unif.Set(Names.projInv, proj.Inverted().AsInt32());

            if (unif.Has(Names.viewProjInv))
                unif.Set(Names.viewProjInv, (view * proj).Inverted().AsInt32());

            if (unif.Has(Names.camera))
                unif.Set(Names.camera, new[] { fov, aspect, near, far }.AsInt32());

            if (unif.Has(Names.position))
                unif.Set(Names.position, Position.AsInt32());

            if (unif.Has(Names.rotation))
                unif.Set(Names.rotation, Rotation.AsInt32());

            if (unif.Has(Names.direction))
                unif.Set(Names.direction, view.Row2.AsInt32());

            if (unif.Has(Names.nearPlane))
            {
                var n = view.Row2.Xyz;
                var w = n.X * posx + n.Y * posy + n.Z * posz;
                unif.Set(Names.nearPlane, new[] { n.X, n.Y, n.Z, w }.AsInt32());
            }

            // UPDATE UNIFORM BUFFER
            unif.Update();
            unif.Bind();
        }

        //public void EndPass(int program)
        //{
        //    // Executed at the end of a pass every frame.
        //    // not used
        //}

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
