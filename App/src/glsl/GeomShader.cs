using OpenTK.Graphics.OpenGL4;
using System;
using System.Diagnostics;
using System.Reflection;

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

#pragma warning restore 0649
#pragma warning restore 0169

        #endregion

        #region Constructors

        public GeomShader() : this(-1) { }

        public GeomShader(int startLine)
            : base(startLine, ProgramPipelineParameter.GeometryShader)
        {
            gl_ClipDistance = new float[gl_MaxClipDistances];
        }

        #endregion

        private VertShader VertexShader
        {
            get
            {
                for (var prev = Prev; prev != null; prev = Prev)
                    if (prev is VertShader)
                        return (VertShader)prev;
                return null;
            }
        }

        /// <summary>
        /// Execute shader and generate debug trace
        /// if the shader is linked to a file.
        /// </summary>
        internal void ExecuteCpuDebugShader()
        {
            try
            {
                Execute(Settings.gs_PrimitiveIDIn, Settings.vs_InstanceID, LineInFile >= 0);
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
        internal void Execute(int primitiveID, int instanceID, bool debug = false)
        {
            DebugGetError(new StackTrace(true));

            // set shader input
            ProcessFields(this);
            GetVertexShaderOutput(primitiveID, instanceID);

            // get input qualifier
            var layout = GetQualifier<__layout>("__in__");

            // only generate debug trace if the shader is linked to a file
            if (debug)
                Debugger.BeginTracing(LineInFile);

            // execute shader
            for (int i = 0, I = layout?.invocations ?? 1; i < I; i++)
            {
                gl_InvocationID = i;
                main();
            }

            // end debug trace generation
            Debugger.EndTracing();

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
            var vertexShader = VertexShader;

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
                vertexShader.Execute(vertexID, instanceID);
                // set geometry shader built in input varyings
                gl_in[i].gl_Position = vertexShader.GetOutputVarying<vec4>("gl_Position");
                gl_in[i].gl_PointSize = vertexShader.GetOutputVarying<float>("gl_PointSize");
                var clipDistance = vertexShader.GetOutputVarying<float[]>("gl_ClipDistance");
                for (int j = 0; j < clipDistance.Length; j++)
                    gl_in[i].gl_ClipDistance[j] = clipDistance[j];
                // set geometry shader user input varyings
                ProcessFields(this, ProcessInField, new[] { typeof(__in) }, i);
            }
        }

        private void ProcessInField(object obj, FieldInfo field, object index)
        {
            // get input varying type
            var type = field.FieldType;

            // if this is an array, get the respective output varying
            // and place in at the current index of the array
            if (type.IsArray)
            {
                type = type.GetElementType();
                var array = field.GetValue(obj) as Array;
                array.SetValue(GetInputVarying(type, type.IsClass ? null : field.Name), (int)index);
            }
            else
                field.SetValue(obj, GetInputVarying(type, type.IsClass ? null : field.Name));
        }

        /// <summary>
        /// Default shader main function.
        /// Used when no custom geometry shader is present.
        /// </summary>
        public override void main() { }

        #region Geometry Shader Functions

        public void EmitVertex() { }

        public void EndPrimitive() { }

        public void EmitStreamVertex(int stream) { }

        public void EndStreamPrimitive(int stream) { }

        #endregion
    }
}
