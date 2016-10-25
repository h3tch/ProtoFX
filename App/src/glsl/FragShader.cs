namespace App.Glsl
{
    class FragShader : Shader
    {
        #region Field

        public static readonly FragShader Default = new FragShader();

        #endregion

#pragma warning disable 0649
#pragma warning disable 0169

        #region Input

        vec4 gl_FragCoord;
        bool gl_FrontFacing;
        vec2 gl_PointCoord;
        int gl_SampleID;
        vec2 gl_SamplePosition;
        int[] gl_SampleMaskIn;
        float gl_ClipDistance;
        int gl_PrimitiveID;
        int gl_Layer;
        int gl_ViewportIndex;

        #endregion

        #region Output

        [__out] float gl_FragDepth;
        [__out] int[] gl_SampleMask;

        #endregion

#pragma warning restore 0649
#pragma warning restore 0169

        public void Debug()
        {
            BeginTracing();
            EndTracing();
        }
    }
}
