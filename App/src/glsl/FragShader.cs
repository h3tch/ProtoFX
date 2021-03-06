﻿using OpenTK.Graphics.OpenGL4;
using System;

namespace App.Glsl
{
    public class FragShader : Shader
    {
#pragma warning disable 0649
#pragma warning disable 0169

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

#pragma warning restore 0649
#pragma warning restore 0169

        #region Constructors

        public FragShader() : this(-1) { }

        public FragShader(int startLine) 
            : base(startLine, ProgramPipelineParameter.FragmentShader) { }

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
                    Debugger.BeginTracing(LineInFile);
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
        }

        public override void main()
        {
        }
    }
}
