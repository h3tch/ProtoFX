﻿using OpenTK.Graphics.OpenGL4;
using System;

namespace App.Glsl
{
    class TessShader : Shader
    {
        #region Field

        public static readonly TessShader Default = new TessShader();

        #endregion

#pragma warning disable 0649
#pragma warning disable 0169

        #region Input

        int gl_PatchVerticesIn;
        int gl_PrimitiveID;
        int gl_InvocationID;
        __InOut[] gl_in = new __InOut[4];

        #endregion

        #region Output

        [__out] float[] gl_TessLevelOuter = new float[4];
        [__out] float[] gl_TessLevelInner = new float[2];
        [__out] __InOut[] gl_out = new __InOut[4];

        #endregion

#pragma warning restore 0649
#pragma warning restore 0169

        internal void Debug()
        {
            GetVertexOutput(Settings.ts_PrimitiveID, Settings.ts_InvocationID);
            BeginTracing();
            main();
            EndTracing();
        }

        internal void Execute(int primitiveID, int invocationID)
        {
            GetVertexOutput(primitiveID, invocationID);
            main();
        }

        public override void main()
        {
            gl_TessLevelInner[0] = 0f;
            gl_TessLevelInner[1] = 0f;
            for (int i = 0; i < 4; i++)
            {
                gl_TessLevelOuter[i] = 0f;
                gl_out[i].gl_Position = gl_in[i].gl_Position;
                gl_out[i].gl_PointSize = gl_in[i].gl_PointSize;
                gl_out[i].gl_ClipDistance = gl_in[i].gl_ClipDistance;
            }
        }

        private void GetVertexOutput(int primitiveID, int invocationID)
        {
            if (drawcall?.cmd?.Count == 0)
                return;

            // set shader input
            gl_PatchVerticesIn = GL.GetInteger(GetPName.PatchVertices);
            gl_PrimitiveID = primitiveID;
            gl_InvocationID = invocationID;

            // load patch data from vertex shader
            var patch = drawcall.GetPatch(primitiveID);
            var vert = (VertShader)Prev;
            for (int i = 0; i < patch.Length; i++)
            {
                var vertexID = Convert.ToInt32(patch.GetValue(i));
                vert.Execute(vertexID, gl_InvocationID);
                gl_in[i].gl_Position = vert.GetOutputVarying<vec4>("gl_Position");
                gl_in[i].gl_PointSize = vert.GetOutputVarying<float>("gl_PointSize");
                gl_in[i].gl_ClipDistance = vert.GetOutputVarying<float[]>("gl_ClipDistance");
            }
        }
    }
}
