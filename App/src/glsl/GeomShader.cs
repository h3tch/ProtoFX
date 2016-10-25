using System.Collections.Generic;

namespace App.Glsl
{
    class GeomShader : Shader
    {
        #region Field

        public static readonly GeomShader Default = new GeomShader();

        #endregion

#pragma warning disable 0649
#pragma warning disable 0169

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

#pragma warning restore 0649
#pragma warning restore 0169

        internal void Debug()
        {
            BeginTracing();
            Execute(Settings.gs_PrimitiveIDIn, Settings.gs_InvocationID);
            EndTracing();
        }

        internal void Execute(int primitiveID, int invocationID)
        {
            // set shader input
            gl_PrimitiveIDIn = primitiveID;
            gl_InvocationID = invocationID;
            gl_in = Prev?.GetOutputVarying<__InOut[]>("gl_out") ?? __in;

            // execute shader
            main();
        }
    }
}
