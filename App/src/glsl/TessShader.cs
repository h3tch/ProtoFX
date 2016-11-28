using OpenTK.Graphics.OpenGL4;
using System;
using System.Diagnostics;

namespace App.Glsl
{
    class TessShader : Shader
    {
        #region Input

#pragma warning disable 0649
#pragma warning disable 0169

        protected int gl_MaxPatchVertices;
        protected int gl_PatchVerticesIn;
        protected int gl_PrimitiveID;
        protected int gl_InvocationID;
        protected __InOut[] gl_in;

        #endregion

        #region Output

        [__out] protected float[] gl_TessLevelOuter;
        [__out] protected float[] gl_TessLevelInner;
        [__out] protected __InOut[] gl_out;
        private VertShader VertShader => (VertShader)Prev;

#pragma warning restore 0649
#pragma warning restore 0169

        #endregion

        #region Constructors

        public TessShader() : this(-1) { }

        public TessShader(int startLine) : base(startLine, ProgramPipelineParameter.TessControlShader)
        {
            gl_MaxPatchVertices = GL.GetInteger(GetPName.MaxPatchVertices);
            gl_TessLevelOuter = new float[4];
            gl_TessLevelInner = new float[2];
            gl_in = __InOut.Create(gl_MaxPatchVertices);
            gl_out = __InOut.Create(gl_MaxPatchVertices);
        }

        #endregion

        /// <summary>
        /// Execute shader and generate debug trace
        /// if the shader is linked to a file.
        /// </summary>
        internal void Debug()
        {
            try
            {
                Execute(Settings.ts_PrimitiveID, Settings.vs_InstanceID, LineInFile >= 0);
            }
            catch (Exception e)
            {
                Debugger.TraceExeption(e);
            }
            finally
            {
                Debugger.EndTracing();
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
            DebugGetError(new StackTrace(true));

            // get data from the vertex shader
            GetVertexShaderOutput(primitiveID, instanceID);
            ProcessFields(this);

            // only generate debug trace if the shader is linked to a file
            if (debug)
                Debugger.BeginTracing(LineInFile);

            // execute the main function of the shader
            // for each vertex of the patch
            for (int i = 0; i < gl_PatchVerticesIn; i++)
            {
                gl_InvocationID = i;
                main();
            }

            // end debug trace generation
            Debugger.EndTracing();

            DebugGetError(new StackTrace(true));
        }

        /// <summary>
        /// Get the output from the vertex shader.
        /// </summary>
        /// <param name="primitiveID"></param>
        /// <param name="instanceID"></param>
        private void GetVertexShaderOutput(int primitiveID, int instanceID)
        {
            if (DrawCall?.cmd?.Count == 0)
                return;
            
            // load patch data from vertex shader
            var patch = DrawCall.GetPatch(primitiveID);
            DebugGetError(new StackTrace(true));

            gl_PatchVerticesIn = patch.Length;
            gl_PrimitiveID = primitiveID;

            for (int i = 0; i < gl_PatchVerticesIn; i++)
            {
                // compute vertex shader data
                VertShader.Execute(Convert.ToInt32(patch.GetValue(i)), instanceID);
                // set input data for the vertex
                gl_in[i].gl_Position = VertShader.GetOutputVarying<vec4>("gl_Position");
                gl_in[i].gl_PointSize = VertShader.GetOutputVarying<float>("gl_PointSize");
                var clipDistance = VertShader.GetOutputVarying<float[]>("gl_ClipDistance");
                for (int j = 0; j < clipDistance.Length; j++)
                    gl_in[i].gl_ClipDistance[j] = clipDistance[j];
            }
        }
        
        /// <summary>
        /// Default shader main function.
        /// Used when no custom tessellation shader is prescient.
        /// </summary>
        public override void main()
        {
            gl_TessLevelInner[0] = gl_TessLevelInner[1] = 1f;
            gl_TessLevelOuter[0] = gl_TessLevelOuter[1] = 1f;
            gl_TessLevelOuter[2] = gl_TessLevelOuter[3] = 1f;
            gl_out[gl_InvocationID].gl_Position = gl_in[gl_InvocationID].gl_Position;
            gl_out[gl_InvocationID].gl_PointSize = gl_in[gl_InvocationID].gl_PointSize;
            for (int j = 0; j < gl_out[gl_InvocationID].gl_ClipDistance.Length; j++)
                gl_out[gl_InvocationID].gl_ClipDistance[j] = gl_in[gl_InvocationID].gl_ClipDistance[j];
        }
    }
}
