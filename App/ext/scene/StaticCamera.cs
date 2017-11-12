using protofx;
using OpenTK;
using System;
using System.Linq;

namespace scene
{
    class StaticCamera : Node
    {
        #region UNIFORM NAMES

        protected enum Names
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
            LAST,
        }
        protected static string[] UniformNames = Enum.GetNames(typeof(Names))
            .Take((int)Names.LAST).ToArray();

        #endregion

        #region FIELDS

        [Connectable] protected float posx;
        [Connectable] protected float posy;
        [Connectable] protected float posz;
        [Connectable] protected float rotx;
        [Connectable] protected float roty;
        [Connectable] protected float rotz;
        [Connectable] protected float fov = 60f;
        [Connectable] protected float near = 0.1f;
        [Connectable] protected float far = 100f;
        protected const float deg2rad = (float)(Math.PI / 180);
        protected string name;
        protected Matrix4 view;

        #endregion

        #region PROPERTIES
        // Properties accessible by ProtoFX

        public string Name { get { return name; } }
        public float[] Position {
            get { return new float[] { posx, posy, posz }; }
            set { posx = value[0]; posy = value[1]; posz = value[2]; }
        }
        public float[] Rotation
        {
            get { return new float[] { rotx, roty, rotz }; }
            set { rotx = value[0]; roty = value[1]; rotz = value[2]; }
        }
        public float FieldOfViewY { get { return fov; } set { fov = value; } }
        public float NearPlane { get { return near; } set { near = value; } }
        public float FarPlane { get { return far; } set { far = value; } }

        #endregion

        /// <summary>
        /// ProtoFX Constructor.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="cmds"></param>
        /// <param name="objs"></param>
        public StaticCamera(object @params) : base(@params)
        {
            // The constructor is executed only once when the pass is created.

            // ProtoGL code:
            // exec csharp_name util.SimpleCamera fovy nearz farz x y z rotx roty rotz ...
            //      uniform_view_name uniform_proj_name uniform_view_proj_name uniform_info_name

            // argument cmd contains the whole command including
            // 'exec', 'csharp_name' and 'util.SimpleCamera'

            // parse command for values specified by the user

            name = @params.GetInstanceValue<string>("Name");
            // PARSE COMMAND VALUES SPECIFIED BY THE USER
            float[] pos = new float[3] { 0f, 0f, 0f };
            float[] rot = new float[3] { 0f, 0f, 0f };
            Convert(commands, "name", ref name);
            Convert(commands, "pos", ref pos);
            Convert(commands, "rot", ref rot);
            Convert(commands, "fov", ref fov);
            Convert(commands, "near", ref near);
            Convert(commands, "far", ref far);
            Position = pos;
            Rotation = rot;
        }

        /// <summary>
        /// ProtoFX update method.
        /// </summary>
        /// <param name="pipeline">Active shader pipeline</param>
        /// <param name="width">Backbuffer width</param>
        /// <param name="height">Backbuffer height</param>
        /// <param name="widthTex">Framebuffer width</param>
        /// <param name="heightTex">Framebuffer height</param>
        public void Update(int pipeline, int width, int height, int widthTex, int heightTex)
        {
            // GET OR CREATE CAMERA UNIFORMS FOR program
            var unif = PrepareUpdate(pipeline, width, height, widthTex, heightTex, UniformNames);
            if (unif == null)
                return;

            // UPDATE UNIFORM BUFFER
            unif.Update();
            unif.Bind();
        }

        protected UniformBlock PrepareUpdate(int pipeline, int width, int height, int widthTex, int heightTex, string[] uniformNames)
        {
            // GET OR CREATE CAMERA UNIFORMS FOR program
            var unif = GetUniformBlock(pipeline, name, uniformNames);
            if (unif == null)
                return null;

            // This function is executed every frame at the beginning of a pass.
            view = Matrix4.CreateTranslation(-posx, -posy, -posz)
                 * Matrix4.CreateRotationY(-roty * deg2rad)
                 * Matrix4.CreateRotationX(-rotx * deg2rad);
            float aspect = (float)width / height;
            float angle = fov * deg2rad;
            Matrix4 proj = Matrix4.CreatePerspectiveFieldOfView(angle, aspect, near, far);

            // SET UNIFORM VALUES
            if (unif.Has((int)Names.view))
                unif.Set((int)Names.view, view.AsInt32());

            if (unif.Has((int)Names.proj))
                unif.Set((int)Names.proj, proj.AsInt32());

            if (unif.Has((int)Names.viewProj))
                unif.Set((int)Names.viewProj, (view * proj).AsInt32());

            if (unif.Has((int)Names.viewInv))
                unif.Set((int)Names.viewInv, view.Inverted().AsInt32());

            if (unif.Has((int)Names.projInv))
                unif.Set((int)Names.projInv, proj.Inverted().AsInt32());

            if (unif.Has((int)Names.viewProjInv))
                unif.Set((int)Names.viewProjInv, (view * proj).Inverted().AsInt32());

            if (unif.Has((int)Names.camera))
                unif.Set((int)Names.camera, new[] { fov, aspect, near, far }.AsInt32());

            if (unif.Has((int)Names.position))
                unif.Set((int)Names.position, Position.AsInt32());

            if (unif.Has((int)Names.rotation))
                unif.Set((int)Names.rotation, Rotation.AsInt32());

            if (unif.Has((int)Names.direction))
                unif.Set((int)Names.direction, view.Row2.AsInt32());

            if (unif.Has((int)Names.nearPlane))
            {
                var n = view.Row2.Xyz;
                var w = n.X * posx + n.Y * posy + n.Z * posz;
                unif.Set((int)Names.nearPlane, new[] { n.X, n.Y, n.Z, w }.AsInt32());
            }

            return unif;
        }
    }
}
