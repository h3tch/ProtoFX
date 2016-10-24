using OpenTK.Graphics.OpenGL4;
using System;

namespace App.Glsl
{
    abstract class EvalShader : Shader
    {
        #region Input
        
        int gl_PatchVerticesIn;
        int gl_PrimitiveID;
        vec3 gl_TessCoord = new vec3();
        float[] gl_TessLevelOuter;
        float[] gl_TessLevelInner;
        __InOut[] gl_in;
        float[] __TessLevelOuter = new float[4];
        float[] __TessLevelInner = new float[2];
        __InOut[] __in = new __InOut[4];

        #endregion

        #region Output

        [__out] vec4 gl_Position;
        [__out] float gl_PointSize;
        [__out] float[] gl_ClipDistance;

        #endregion

        private Random rnd;

        internal void Debug()
        {
            DebugTrace.Clear();
            TraceLog = DebugTrace;
            Execute(Settings.ts_PrimitiveID, Settings.ts_TessCoord);
            TraceLog = null;
        }

        internal void Execute(int primitiveID, float[] tessCoord)
        {
            // set shader input
            gl_PatchVerticesIn = GL.GetInteger(GetPName.PatchVertices);
            gl_PrimitiveID = primitiveID;
            gl_TessCoord.x = Math.Min(Math.Max(0, tessCoord[0]), 1);
            gl_TessCoord.y = Math.Min(Math.Max(0, tessCoord[1]), 1);
            gl_TessCoord.z = gl_PatchVerticesIn < 4 ? Math.Min(Math.Max(0, tessCoord[2]), 1) : 0f;
            gl_TessLevelOuter = prev?.GetOutputVarying<float[]>("gl_TessLevelOuter") ?? __TessLevelOuter;
            gl_TessLevelInner = prev?.GetOutputVarying<float[]>("gl_TessLevelInner") ?? __TessLevelInner;
            gl_in = prev?.GetOutputVarying<__InOut[]>("gl_out") ?? __in;

            // execute shader
            main();
        }
    }
}
