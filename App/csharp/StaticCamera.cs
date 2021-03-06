﻿using OpenTK;
using System;
using System.Collections.Generic;
using Commands = System.Collections.Generic.Dictionary<string, string[]>;
using GLNames = System.Collections.Generic.Dictionary<string, int>;

namespace csharp
{

    class StaticCamera : CsObject
    {
        public enum Names
        {
            view,
            proj,
            viewProj,
            camera,
            position,
            rotation,
        }

        #region FIELDS

        public float[] pos = new float[] { 0f, 0f, 0f };
        public float[] rot = new float[] { 0f, 0f, 0f };
        public float fov = 60f;
        public float near = 0.1f;
        public float far = 100f;
        protected const float rad2deg = (float)(Math.PI / 180);
        protected string name;
        protected Matrix4 view;
        protected Dictionary<int, UniformBlock<Names>> uniform =
            new Dictionary<int, UniformBlock<Names>>();

        #endregion

        #region PROPERTIES

        public string Name { get { return name; } }
        public float[] Position { get { return pos; } set { pos = value; } }
        public float[] Rotation { get { return rot; } set { rot = value; } }
        public float FieldOfViewY { get { return fov; } set { fov = value; } }
        public float NearPlane { get { return near; } set { near = value; } }
        public float FarPlane { get { return far; } set { far = value; } }

        #endregion

        public StaticCamera(string name, Commands cmds, GLNames glNames)
        {
            // The constructor is executed only once when the pass is created.

            // ProtoGL code:
            // exec csharp_name util.SimpleCamera fovy nearz farz x y z rotx roty rotz ...
            //      uniform_view_name uniform_proj_name uniform_view_proj_name uniform_info_name

            // argument cmd contains the whole command including
            // 'exec', 'csharp_name' and 'util.SimpleCamera'

            // parse command for values specified by the user

            // PARSE COMMAND VALUES SPECIFIED BY THE USER
            this.name = name;
            Convert(cmds, "name", ref this.name);
            Convert(cmds, "pos", ref pos);
            Convert(cmds, "rot", ref rot);
            Convert(cmds, "fov", ref fov);
            Convert(cmds, "near", ref near);
            Convert(cmds, "far", ref far);
        }

        public void Update(int pipeline, int width, int height, int widthTex, int heightTex)
        {
            // This function is executed every frame at the beginning of a pass.
            view = Matrix4.CreateTranslation(-pos[0], -pos[1], -pos[2])
                 * Matrix4.CreateRotationY(-rot[1] * rad2deg)
                 * Matrix4.CreateRotationX(-rot[0] * rad2deg);
            float aspect = (float)width / height;
            Matrix4 proj = Matrix4.CreatePerspectiveFieldOfView(fov * rad2deg, aspect, near, far);

            // GET OR CREATE CAMERA UNIFORMS FOR program
            UniformBlock<Names> unif;
            if (uniform.TryGetValue(pipeline, out unif) == false)
                uniform.Add(pipeline, unif = new UniformBlock<Names>(pipeline, name));

            // SET UNIFORM VALUES
            if (unif.Has(Names.view))
                unif.Set(Names.view, view.AsInt32());

            if (unif.Has(Names.proj))
                unif.Set(Names.proj, proj.AsInt32());

            if (unif.Has(Names.viewProj))
                unif.Set(Names.viewProj, (view * proj).AsInt32());

            if (unif.Has(Names.camera))
                unif.Set(Names.camera, new[] { fov * rad2deg, aspect, near, far }.AsInt32());

            if (unif.Has(Names.position))
                unif.Set(Names.position, pos.AsInt32());

            if (unif.Has(Names.rotation))
                unif.Set(Names.rotation, rot.AsInt32());

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
                u.Value.Delete();
        }
    }
}
