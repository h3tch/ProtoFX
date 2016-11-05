using OpenTK.Graphics.OpenGL4;
using System;
using System.Diagnostics;
using System.Linq;

namespace App.Glsl
{
    public class VertShader : Shader
    {
        #region Input

#pragma warning disable 0649
#pragma warning disable 0169
        protected int gl_VertexID;
        protected int gl_InstanceID;
        protected int gl_MaxClipDistances;

        #endregion

        #region Output
        
        [__out] protected vec4 gl_Position;
        [__out] protected float gl_PointSize;
        [__out] protected float[] gl_ClipDistance;
#pragma warning restore 0649
#pragma warning restore 0169

        #endregion
        
        #region Constructors

        public VertShader() : this(-1) { }

        public VertShader(int startLine) : base(startLine)
        {
            gl_MaxClipDistances = GL.GetInteger(GetPName.MaxClipDistances);
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
                Execute(Settings.vs_VertexID, Settings.vs_InstanceID, LineInFile >= 0);
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
        /// <param name="vertexID"></param>
        /// <param name="instanceID"></param>
        internal void Execute(int vertexID, int instanceID, bool debug = false)
        {
            DebugGetError(new StackTrace(true));

            // only generate debug trace if the shader is linked to a file
            if (debug)
                BeginTracing();

            // set shader input
            gl_VertexID = vertexID;
            gl_InstanceID = instanceID;
            // execute shader
            main();

            DebugGetError(new StackTrace(true));
        }

        #region Overrides

        public override T GetInputVarying<T>(string varyingName)
        {
            DebugGetError(new StackTrace(true));

            if (DrawCall == null)
                return DebugGetError<T>(new StackTrace(true));

            // get current shader program pipeline
            var pipeline = GL.GetInteger(GetPName.ProgramPipelineBinding);
            if (pipeline <= 0)
                return DebugGetError<T>(new StackTrace(true));

            // get vertex shader
            int program;
            GL.GetProgramPipeline(pipeline, ProgramPipelineParameter.VertexShader, out program);
            if (program <= 0)
                return DebugGetError<T>(new StackTrace(true));

            // get vertex input attribute location
            var location = GL.GetAttribLocation(program, varyingName);
            if (location < 0)
                return DebugGetError<T>(new StackTrace(true));

            // get vertex input data
            var data = DrawCall?.vertin.GetVertexData(gl_VertexID, gl_InstanceID);
            var array = data?.Skip(location).First();
            if (array == null)
                return DebugGetError<T>(new StackTrace(true));

            DebugGetError(new StackTrace(true));

            // return base type
            if (BaseTypes.Any(x => x == typeof(T)))
                return (T)array.To(typeof(T)).GetValue(0);

            // create new object from byte array
            return (T)typeof(T).GetConstructor(new[] { typeof(byte[]) })?.Invoke(new[] { array });
        }

        public static object GetUniform<T>(string uniformName)
            => GetUniform<T>(uniformName, ProgramPipelineParameter.VertexShader);

        #endregion
    }
}
