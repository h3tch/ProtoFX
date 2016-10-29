using OpenTK.Graphics.OpenGL4;
using System;

namespace App.Glsl
{
    class TessShader : Shader
    {
        #region Input

#pragma warning disable 0649
#pragma warning disable 0169

        protected int gl_PatchVerticesIn;
        protected int gl_PrimitiveID;
        protected int gl_InvocationID;
        protected __InOut[] gl_in = new __InOut[4];

        #endregion

        #region Output

        [__out] protected float[] gl_TessLevelOuter = new float[4];
        [__out] protected float[] gl_TessLevelInner = new float[2];
        [__out] protected __InOut[] gl_out = new __InOut[4];

#pragma warning restore 0649
#pragma warning restore 0169

        #endregion

        #region Constructors

        public TessShader() : this(-1) { }

        public TessShader(int startLine) : base(startLine) { }

        #endregion

        /// <summary>
        /// Execute shader and generate debug trace
        /// if the shader is linked to a file.
        /// </summary>
        internal void Debug()
        {
            // get data from the vertex shader
            GetVertexOutput(Settings.ts_PrimitiveID, Settings.ts_InvocationID);
            // only generate debug trace if the shader is linked to a file
            if (LineInFile >= 0)
                BeginTracing();
            // execute the main function of the shader
            main();
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
            // get data from the vertex shader
            GetVertexOutput(primitiveID, invocationID);
            // execute the main function of the shader
            main();
        }

        #region Overrides

        /// <summary>
        /// Default shader main function.
        /// Used when no custom tessellation shader is prescient.
        /// </summary>
        public override void main()
        {
            // RETURN DEFAULT OUTPUT VALUES

            gl_TessLevelInner[0] = 1f;
            gl_TessLevelInner[1] = 1f;
            for (int i = 0; i < 4; i++)
            {
                gl_TessLevelOuter[i] = 1f;
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

        public static object GetUniform<T>(string uniformName)
            => GetUniform<T>(uniformName, ProgramPipelineParameter.TessControlShader);

        #endregion
    }
}
