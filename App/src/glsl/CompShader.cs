using OpenTK.Graphics.OpenGL4;
using System;

namespace protofx.Glsl
{
    abstract class CompShader : Shader
    {
        #region Input

#pragma warning disable 0649
#pragma warning disable 0169

        protected uvec3 gl_NumWorkGroups;
        protected uvec3 gl_WorkGroupSize;
        protected uvec3 gl_WorkGroupID;
        protected uvec3 gl_LocalInvocationID;
        protected uvec3 gl_GlobalInvocationID;
        protected uint gl_LocalInvocationIndex;

#pragma warning restore 0649
#pragma warning restore 0169

        #endregion

        #region Constructors

        public CompShader() : this(-1) { }

        public CompShader(int startLine)
            : base(startLine, ProgramPipelineParameter.ComputeShader) { }

        #endregion

        internal void ExecuteCpuDebugShader()
        {
            try
            { 
                // only generate debug trace if the shader is linked to a file
                if (LineInFile >= 0)
                    Debugger.BeginTracing(LineInFile);
                // end debug trace generation
                Debugger.EndTracing();
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
    }
}
