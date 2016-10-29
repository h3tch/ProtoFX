using OpenTK.Graphics.OpenGL4;

namespace App.Glsl
{
    class GeomShader : Shader
    {
        #region Input

#pragma warning disable 0649
#pragma warning disable 0169

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

#pragma warning restore 0649
#pragma warning restore 0169

        #endregion

        #region Constructors

        public GeomShader() : this(-1) { }

        public GeomShader(int startLine) : base(startLine) { }

        #endregion

        /// <summary>
        /// Execute shader and generate debug trace
        /// if the shader is linked to a file.
        /// </summary>
        internal void Debug()
        {
            // only generate debug trace if the shader is linked to a file
            if (LineInFile >= 0)
                BeginTracing();
            // execute the main function of the shader
            Execute(Settings.gs_PrimitiveIDIn, Settings.gs_InvocationID);
            // end debug trace generation
            EndTracing();
        }

        /// <summary>
        /// Execute the shader.
        /// </summary>
        /// <param name="primitiveID"></param>
        /// <param name="invocationID"></param>
        internal void Execute(int primitiveID, int invocationID)
        {
            // set shader input
            gl_PrimitiveIDIn = primitiveID;
            gl_InvocationID = invocationID;
            gl_in = Prev.GetOutputVarying<__InOut[]>("gl_out");

            // execute shader
            main();
        }

        #region Overrides

        public static object GetUniform<T>(string uniformName)
            => GetUniform<T>(uniformName, ProgramPipelineParameter.GeometryShader);

        #endregion
    }
}
