using OpenTK.Graphics.OpenGL4;
using System;
using System.Diagnostics;
using Shadertype = OpenTK.Graphics.OpenGL4.ShaderType;

namespace App.Glsl
{
    public class FragShader : Shader
    {
        #region Input

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

        #endregion

        #region Debug

        private int DebugFrag;
        private int DebugOutputBinding;
        private int DebugBuffer;
        private int DebugTexture;

        #endregion


        #region Constructors

        public FragShader() : this(-1, null) { }

        public FragShader(int startLine, string shaderString) 
            : base(startLine, ProgramPipelineParameter.FragmentShader)
        {
            var debug = Converter.InputVaryingDebugShader(shaderString);
            DebugFrag = GL.CreateShaderProgram(Shadertype.FragmentShader, 1, new[] { debug });
            GL.GetProgram(DebugFrag, GetProgramParameterName.LinkStatus, out int status);
            if (status != 1 || GL.GetError() != ErrorCode.NoError)
                Delete();

            DebugOutputBinding = GL.GetUniformLocation(DebugFrag, "_dbgOut");

            DebugBuffer = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.TextureBuffer, DebugBuffer);
            GL.NamedBufferData(DebugBuffer, 1024, IntPtr.Zero, BufferUsageHint.StaticRead);

            DebugTexture = GL.GenTexture();
            GL.BindTexture(TextureTarget.TextureBuffer, DebugTexture);
            GL.TextureBuffer(DebugTexture, SizedInternalFormat.Rgba32f, DebugBuffer);

            GL.BindTexture(TextureTarget.TextureBuffer, 0);
            GL.BindBuffer(BufferTarget.TextureBuffer, 0);
        }

        public override void Delete()
        {
            base.Delete();
            if (DebugFrag > 0)
            {
                GL.DeleteProgram(DebugFrag);
                DebugFrag = 0;
            }
            if (DebugBuffer > 0)
            {
                GL.DeleteBuffer(DebugBuffer);
                DebugBuffer = 0;
            }
            if (DebugTexture > 0)
            {
                GL.DeleteTexture(DebugTexture);
                DebugTexture = 0;
            }
        }

        #endregion

        /// <summary>
        /// Execute shader and generate debug trace
        /// if the shader is linked to a file.
        /// </summary>
        internal void Debug(int glpipe, int glfrag)
        {
            DebugGetError(new StackTrace(true));
            try
            { 
                // only generate debug trace if the shader is linked to a file
                if (LineInFile >= 0)
                    Debugger.BeginTracing(LineInFile);

                GL.UseProgramStages(glpipe, ProgramStageMask.FragmentShaderBit, DebugFrag);
                DebugGetError(new StackTrace(true));

                //main();
            }
            catch (Exception e)
            {
                Debugger.TraceExeption(e);
            }
            finally
            {
                // end debug trace generation
                Debugger.EndTracing();
            }
            GL.UseProgramStages(glpipe, ProgramStageMask.FragmentShaderBit, glfrag);
            DebugGetError(new StackTrace(true));
        }

        public override void main()
        {
        }
    }
}
