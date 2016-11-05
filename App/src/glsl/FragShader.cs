using OpenTK.Graphics.OpenGL4;
using System;

namespace App.Glsl
{
    public class FragShader : Shader
    {
        #region Input

#pragma warning disable 0649
#pragma warning disable 0169

        protected vec4 gl_FragCoord;
        protected bool gl_FrontFacing;
        protected vec2 gl_PointCoord;
        protected int gl_SampleID;
        protected vec2 gl_SamplePosition;
        protected int[] gl_SampleMaskIn;
        protected float gl_ClipDistance;
        protected int gl_PrimitiveID;
        protected int gl_Layer;
        protected int gl_ViewportIndex;

        #endregion

        #region Output

        [__out] protected float gl_FragDepth;
        [__out] protected int[] gl_SampleMask;

#pragma warning restore 0649
#pragma warning restore 0169

        #endregion

        #region Constructors

        public FragShader() : this(-1) { }

        public FragShader(int startLine) : base(startLine) { }

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

        #region Overrides

        public static object GetUniform<T>(string uniformName)
            => GetUniform<T>(uniformName, ProgramPipelineParameter.FragmentShader);

        #endregion
    }
}
