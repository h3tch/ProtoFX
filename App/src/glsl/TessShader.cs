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
            try
            {
                Execute(Settings.ts_PrimitiveID, Settings.vs_InstanceID, true);
            }
            catch (Exception e)
            {
                TraceExeption(e);
            }
            finally
            {
                // end debug trace generation
                EndTracing();
            }
        }

        /// <summary>
        /// Execute the shader.
        /// </summary>
        /// <param name="primitiveID"></param>
        /// <param name="invocationID"></param>
        /// <param name="debug">Enable debug tracing if true.</param>
        internal void Execute(int primitiveID, int instanceID, bool debug = false)
        {
            // get data from the vertex shader
            GetVertexShaderOutput(primitiveID, instanceID);

            // only generate debug trace if the shader is linked to a file
            if (LineInFile >= 0 && debug)
                BeginTracing();

            // execute the main function of the shader
            // for each vertex of the patch
            for (int i = 0; i < gl_in.Length; i++)
            {
                gl_InvocationID = i;
                main();
            }

            // end debug trace generation
            if (debug)
                EndTracing();
        }

        private void GetVertexShaderOutput(int primitiveID, int instanceID)
        {
            if (DrawCall?.cmd?.Count == 0)
                return;

            // set shader input
            gl_PatchVerticesIn = GL.GetInteger(GetPName.PatchVertices);
            gl_PrimitiveID = primitiveID;

            // load patch data from vertex shader
            var patch = DrawCall.GetPatch(primitiveID);
            var vert = (VertShader)Prev;
            for (int i = 0; i < patch.Length; i++)
            {
                var vertexID = Convert.ToInt32(patch.GetValue(i));
                vert.Execute(vertexID, instanceID);
                gl_in[i].gl_Position = vert.GetOutputVarying<vec4>("gl_Position");
                gl_in[i].gl_PointSize = vert.GetOutputVarying<float>("gl_PointSize");
                gl_in[i].gl_ClipDistance = vert.GetOutputVarying<float[]>("gl_ClipDistance");
            }
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

        public static object GetUniform<T>(string uniformName)
            => GetUniform<T>(uniformName, ProgramPipelineParameter.TessControlShader);

        #endregion
    }
}
