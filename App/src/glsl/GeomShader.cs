using System.Collections.Generic;

namespace App.Glsl
{
    abstract class GeomShader : Shader
    {
        #region Input
        
        int gl_PrimitiveIDIn;
        int gl_InvocationID;
        __InOut[] gl_in;
        __InOut[] __in = new __InOut[0];

        #endregion

        #region Output

        [__out] int gl_PrimitiveID;
        [__out] int gl_Layer;
        [__out] int gl_ViewportIndex;
        [__out] vec4 gl_Position;
        [__out] float gl_PointSize;
        [__out] float[] gl_ClipDistance;

        #endregion

        internal void Debug()
        {
            DebugTrace.Clear();
            TraceLog = DebugTrace;
            Execute(Settings.gs_PrimitiveIDIn, Settings.gs_InvocationID);
            TraceLog = null;
        }

        internal void Execute(int primitiveID, int invocationID)
        {
            // set shader input
            gl_PrimitiveIDIn = primitiveID;
            gl_InvocationID = invocationID;
            gl_in = prev?.GetOutputVarying<__InOut[]>("gl_out") ?? __in;

            // execute shader
            main();
        }
    }
}
