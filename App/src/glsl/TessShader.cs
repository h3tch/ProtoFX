using App;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Linq;

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
            BeginTracing();
            Execute(Settings.ts_PrimitiveID, Settings.ts_InvocationID);
            EndTracing();
        }

        internal void Execute(int primitiveID, int invocationID)
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
            for (int i = 0; i < gl_PatchVerticesIn; i++)
            {
                vert.Execute((int)patch.GetValue(i), gl_InvocationID);
                gl_in[i].gl_Position = vert.GetOutputVarying<vec4>("gl_Position");
                gl_in[i].gl_PointSize = vert.GetOutputVarying<float>("gl_PointSize");
                gl_in[i].gl_ClipDistance = vert.GetOutputVarying<float[]>("gl_ClipDistance");
            }

            // execute shader
            main();
        }
    }
}
