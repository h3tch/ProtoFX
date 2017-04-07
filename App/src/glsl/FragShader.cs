using App.Extensions;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Shadertype = OpenTK.Graphics.OpenGL4.ShaderType;

namespace App.Glsl
{
    public abstract class FragShader : Shader
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
        
        private int DbgOutUnit;
        private Dictionary<int, string> DbgLoc2Input;
        private static byte[] DebugData = new byte[1024];

        #endregion

        #region Constructors

        public FragShader() : this(-1) { }

        public FragShader(int startLine)
            : base(startLine, ProgramPipelineParameter.FragmentShader) { }

        #endregion

        /// <summary>
        /// Execute shader and generate debug trace
        /// if the shader is linked to a file.
        /// </summary>
        internal void BindDebugShaderResources()
        {
            DbgOutUnit = -1;

            // get the active shader program
            var program = GLU.ActiveProgram(ShaderType);
            if (program == 0)
                return;

            // get the uniform location of the debug variables
            var _dbgOut = GL.GetUniformLocation(program, "_dbgOut");
            if (_dbgOut < 0)
                return;
            
            var _dbgFragment = GL.GetUniformLocation(program, "_dbgFragment");
            if (_dbgFragment < 0)
                return;
            
            // get input varying to uniform location mappings from the program
            (DbgLoc2Input, _) = GLU.InputLocationMappings(program);

            // find an unused image unit
            DbgOutUnit = GLTexture.FirstUnusedImgUnit(0, gl_MaxTextureImageUnits);
            if (DbgOutUnit < 0)
                return;

            // bind debug resources
            GL.ProgramUniform1(program, _dbgOut, DbgOutUnit);
            GL.ProgramUniform2(program, _dbgFragment, 0, 0);
            GLTexture.BindImg(DbgOutUnit, Debugger.DebugTexture, 0, 0,
                TextureAccess.WriteOnly, GpuFormat.Rgba32f);
        }

        /// <summary>
        /// Gather the shader input variables from the debug
        /// fragment shader and execute the CPU debug shader.
        /// </summary>
        internal void ExecuteCpuDebugShader()
        {
            if (DbgOutUnit < 0)
                return;

            // unbind shader resources
            GLTexture.BindImg(DbgOutUnit, null, 0, 0,
                TextureAccess.WriteOnly, GpuFormat.Rgba32f);

            // get class fields
            ProcessFields(this);
            GetFragmentInputVaryings();

            // debug the fragment shader
            Debugger.BeginTracing(LineInFile);
            main();
            Debugger.EndTracing();
        }

        /// <summary>
        /// Read the debug output of the debug fragment shader.
        /// </summary>
        private void GetFragmentInputVaryings()
        {
            Debugger.DebugBuffer.Read(ref DebugData);
            
            for (int i = 1, num = BitConverter.ToInt32(DebugData, 0); i < num;)
            {
                var head = new DebugVariable(DebugData, i);
                i += head.Heigth + 1;

                var varying = DbgLoc2Input[head.Location];
                this.SetValue(varying, head.Value);
            }
        }

        /// <summary>
        /// Read a debug variable from a byte array.
        /// </summary>
        private struct DebugVariable
        {
            const int BOOL = 1;
            const int INT = 2;
            const int UINT = 3;
            const int FLOAT = 4;

            private ivec4 Vec;
            public Array Value;
            public int Type => Vec.x;
            public int Heigth => Vec.y;
            public int Width => Vec.z;
            public int Location => Vec.w;

            public DebugVariable(byte[] data, int index)
            {
                Vec = data.Skip(16 * index++).Take(16).ToArray().To<ivec4>()[0];
                Value = null;

                var w = 4 * Width;
                var bytes = Enumerable.Range(index, Heigth)
                    .SelectMany(i => data.Skip(16 * i).Take(w)).ToArray();

                switch (Type)
                {
                    case BOOL: Value = bytes.To<int>().Select(v => v != 0).ToArray(); break;
                    case INT: Value = bytes.To<int>(); break;
                    case UINT: Value = bytes.To<uint>(); break;
                    case FLOAT: Value = bytes.To<float>(); break;
                    default: throw new ArgumentException("");
                }
            }
        };
    }
}
