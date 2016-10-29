using OpenTK.Graphics.OpenGL4;
using System;
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

        #endregion

        #region Output
        
        [__out] protected vec4 gl_Position;
        [__out] protected float gl_PointSize;
        [__out] protected float[] gl_ClipDistance = new float[4];
#pragma warning restore 0649
#pragma warning restore 0169

        #endregion
        
        #region Constructors

        public VertShader() : this(-1) { }

        public VertShader(int startLine) : base(startLine) { }

        #endregion

        /// <summary>
        /// Execute shader and generate debug trace
        /// if the shader is linked to a file.
        /// </summary>
        internal void Debug()
        {
            try
            { 
                // only generate debug trace if the shader is linked to a file
                if (LineInFile >= 0)
                    BeginTracing();
                // execute the shader
                Execute(Settings.vs_VertexID, Settings.vs_InstanceID);
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
        /// <param name="vertexID"></param>
        /// <param name="instanceID"></param>
        internal void Execute(int vertexID, int instanceID)
        {
            // set shader input
            gl_VertexID = vertexID;
            gl_InstanceID = instanceID;
            // execute shader
            main();
        }

        #region Overrides

        public override T GetInputVarying<T>(string varyingName)
        {
            if (drawcall == null)
                return default(T);

            // get current shader program pipeline
            var pipeline = GL.GetInteger(GetPName.ProgramPipelineBinding);
            if (pipeline <= 0)
                return default(T);

            // get vertex shader
            int program;
            GL.GetProgramPipeline(pipeline, ProgramPipelineParameter.VertexShader, out program);
            if (program <= 0)
                return default(T);

            // get vertex input attribute location
            var location = GL.GetAttribLocation(program, varyingName);
            if (location < 0)
                return default(T);

            // get vertex input data
            var data = drawcall?.vertin.GetVertexData(gl_VertexID, gl_InstanceID);
            var array = data?.Skip(location).First();
            if (array == null)
                return default(T);

            // return default type
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
