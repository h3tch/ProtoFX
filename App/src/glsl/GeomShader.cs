using OpenTK.Graphics.OpenGL4;

namespace App.Glsl
{
    class GeomShader : Shader
    {
        #region Field

        public static readonly GeomShader Default = new GeomShader(0);

        #endregion

#pragma warning disable 0649
#pragma warning disable 0169

        #region Input

        protected int gl_PrimitiveIDIn;
        protected int gl_InvocationID;
        protected __InOut[] gl_in;

        #endregion

        #region Output

        [__out] protected int gl_PrimitiveID;
        [__out] protected int gl_Layer;
        [__out] protected int gl_ViewportIndex;
        [__out] protected vec4 gl_Position;
        [__out] protected float gl_PointSize;
        [__out] protected float[] gl_ClipDistance;

        #endregion

#pragma warning restore 0649
#pragma warning restore 0169

        #region Constructors

        public GeomShader() : this(0) { }

        public GeomShader(int startLine) : base(startLine) { }

        #endregion

        internal void Debug()
        {
            if (this != Default)
                BeginTracing();
            Execute(Settings.gs_PrimitiveIDIn, Settings.gs_InvocationID);
            EndTracing();
        }

        internal void Execute(int primitiveID, int invocationID)
        {
            // set shader input
            gl_PrimitiveIDIn = primitiveID;
            gl_InvocationID = invocationID;
            gl_in = Prev.GetOutputVarying<__InOut[]>("gl_out");

            // execute shader
            main();
        }

        public static object GetUniform<T>(string uniformName)
            => GetUniform<T>(uniformName, ProgramPipelineParameter.GeometryShader);
    }
}
