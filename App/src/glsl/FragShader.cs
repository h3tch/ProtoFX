using OpenTK.Graphics.OpenGL4;

namespace App.Glsl
{
    public class FragShader : Shader
    {
        #region Field

        public static readonly FragShader Default = new FragShader(0);

        #endregion

#pragma warning disable 0649
#pragma warning disable 0169

        #region Input

        protected vec4 gl_FragCoord;
        protected bool gl_FrontFacing;
        protected vec2 gl_PointCoord;
        protected int gl_SampleID;
        protected vec2 gl_SamplePosition;
        protected int[] gl_SampleMaskIn;
        protected float gl_ClipDistance;
        protected int gl_PrimitiveID;
        protected int gl_Layer;
        protected int gl_ViewportIndex;

        #endregion

        #region Output

        [__out] protected float gl_FragDepth;
        [__out] protected int[] gl_SampleMask;

        #endregion

#pragma warning restore 0649
#pragma warning restore 0169

        #region Constructors

        public FragShader() : this(0) { }

        public FragShader(int startLine) : base(startLine) { }

        #endregion

        public void Debug()
        {
            if (this != Default)
                BeginTracing();
            EndTracing();
        }

        public static object GetUniform<T>(string uniformName)
            => GetUniform<T>(uniformName, ProgramPipelineParameter.FragmentShader);
    }
}
