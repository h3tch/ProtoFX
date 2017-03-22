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
        internal void DebugBegin()
        {
            DbgOutUnit = -1;

            var program = GLU.ActiveProgram(ShaderType);
            if (program == 0)
                return;

            var _dbgOut = GL.GetUniformLocation(program, "_dbgOut");
            if (_dbgOut < 0)
                return;
            
            var _dbgFragment = GL.GetUniformLocation(program, "_dbgFragment");
            if (_dbgFragment < 0)
                return;
            
            (DbgLoc2Input, _) = GLU.InputLocationMappings(program);

            DbgOutUnit = GLTexture.FirstUnusedImgUnit(0, gl_MaxTextureImageUnits);
            if (DbgOutUnit < 0)
                return;

            GL.ProgramUniform1(program, _dbgOut, DbgOutUnit);
            GL.ProgramUniform2(program, _dbgFragment, 0, 0);
            GLTexture.BindImg(DbgOutUnit, Debugger.DebugTexture, 0, 0,
                TextureAccess.WriteOnly, GpuFormat.Rgba32f);
        }

        internal void DebugEnd()
        {
            if (DbgOutUnit < 0)
                return;
            GLTexture.BindImg(DbgOutUnit, null, 0, 0,
                TextureAccess.WriteOnly, GpuFormat.Rgba32f);
            ProcessFields(this);
            GetFragmentInputVaryings();
        }

        private void GetFragmentInputVaryings()
        {
            Debugger.DebugBuffer.Read(ref DebugData);

            var num = BitConverter.ToInt32(DebugData, 0);
            for (int i = 1; i < num;)
            {
                var head = new Head(DebugVec(i++));
                var array = head.AllocArray();
                for (int y = 0, idx = 0; y < head.Heigth && i < num; y++, i++)
                    for (int x = 0; x < head.Width; x++, idx++)
                        array.SetValue(head.Byte2Type(DebugData, i, x), idx);

                var varying = DbgLoc2Input[head.Location];
                var blockpoint = varying.IndexOf('.');
                if (blockpoint >= 0)
                {
                    var blockname = varying.Substring(0, blockpoint);
                    var fields = GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
                    var field = fields
                        .Where(f => f.FieldType.Name == blockname)
                        .FirstOrDefault();
                    if (field != null)
                        varying = field.Name + varying.Substring(blockpoint);
                }

                var varyingField = FindField(varying);
            }
        }

        private static byte[] DebugVec(int i)
        {
            return DebugData.Skip(16 * i).Take(16).ToArray();
        }

        private struct Head
        {
            const int BOOL = 1;
            const int INT = 2;
            const int UINT = 3;
            const int FLOAT = 4;

            private ivec4 Vec;
            public int Type => Vec.x;
            public int Heigth => Vec.y;
            public int Width => Vec.z;
            public int Location => Vec.w;

            public Head(byte[] data)
            {
                Vec = new ivec4(data);
            }

            public Array AllocArray()
            {

                switch (Type)
                {
                    case BOOL:  return new bool [Heigth * Width];
                    case INT:   return new int  [Heigth * Width];
                    case UINT:  return new uint [Heigth * Width];
                    case FLOAT: return new float[Heigth * Width];
                }

                return null;
            }

            public object Byte2Type(byte[] data, int vectorIdx, int dim)
            {
                int offset = 16 * vectorIdx + 4 * dim;
                switch (Type)
                {
                    case BOOL: return BitConverter.ToInt32(DebugData, offset) != 0;
                    case INT: return BitConverter.ToInt32(DebugData, offset);
                    case UINT: return BitConverter.ToUInt32(DebugData, offset);
                    case FLOAT: return BitConverter.ToSingle(DebugData, offset);
                }
                return null;
            }
        };
    }
}
