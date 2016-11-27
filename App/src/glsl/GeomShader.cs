using OpenTK.Graphics.OpenGL4;
using System;
using System.Diagnostics;

namespace App.Glsl
{
    public class GeomShader : Shader
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
        private VertShader VertShader => (VertShader)Prev.Prev.Prev;

#pragma warning restore 0649
#pragma warning restore 0169

        #endregion

        #region Constructors

        public GeomShader() : this(-1) { }

        public GeomShader(int startLine) : base(startLine, ProgramPipelineParameter.GeometryShader)
        {
            gl_ClipDistance = new float[gl_MaxClipDistances];
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
                Execute(Settings.gs_PrimitiveIDIn, Settings.vs_InstanceID, LineInFile >= 0);
            }
            catch (Exception e)
            {
                TraceExeption(e);
            }
            finally
            {
                EndTracing();
            }
        }

        /// <summary>
        /// Execute the shader.
        /// </summary>
        /// <param name="primitiveID"></param>
        internal void Execute(int primitiveID, int instanceID, bool debug = false)
        {
            DebugGetError(new StackTrace(true));

            // set shader input
            GetVertexShaderOutput(primitiveID, instanceID);
            ProcessFields(this);

            // get input qualifier
            var layout = GetQualifier<__layout>("__in__");

            // only generate debug trace if the shader is linked to a file
            if (debug)
                BeginTracing();

            // execute shader
            for (int i = 0, I = layout?.invocations ?? 1; i < I; i++)
            {
                gl_InvocationID = i;
                main();
            }

            // end debug trace generation
            EndTracing();

            DebugGetError(new StackTrace(true));
        }

        /// <summary>
        /// Get the vertex shader output for the
        /// respective primitive ID and instance.
        /// </summary>
        /// <param name="primitiveID"></param>
        /// <param name="instanceID"></param>
        private void GetVertexShaderOutput(int primitiveID, int instanceID)
        {
            if (DrawCall?.cmd?.Count == 0)
                return;

            // set shader input
            gl_PrimitiveIDIn = primitiveID;

            // load patch data from vertex shader
            var patch = DrawCall.GetPatch(primitiveID);
            DebugGetError(new StackTrace(true));
            gl_in = __InOut.Create(patch.Length);
            for (int i = 0; i < patch.Length; i++)
            {
                // compute vertex shader output
                var vertexID = Convert.ToInt32(patch.GetValue(i));
                VertShader.Execute(vertexID, instanceID);
                // set geometry shader input varyings
                gl_in[i].gl_Position = VertShader.GetOutputVarying<vec4>("gl_Position");
                gl_in[i].gl_PointSize = VertShader.GetOutputVarying<float>("gl_PointSize");
                var clipDistance = VertShader.GetOutputVarying<float[]>("gl_ClipDistance");
                for (int j = 0; j < clipDistance.Length; j++)
                    gl_in[i].gl_ClipDistance[j] = clipDistance[j];
            }
        }

        #region Geometry Shader Functions

        public void EmitVertex() { }

        public void EndPrimitive() { }

        public void EmitStreamVertex(int stream) { }

        public void EndStreamPrimitive(int stream) { }

        #endregion
    }
}
