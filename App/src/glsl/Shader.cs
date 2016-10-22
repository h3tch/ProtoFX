using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace App.Glsl
{
    abstract class Shader : MathFunctions
    {
        #region Texture Access

        public static vec4 texture(sampler1D sampler, float P, float bias = 0) { return new vec4(0); }
        public static vec4 texture(isampler1D sampler, float P, float bias = 0) { return new vec4(0); }
        public static vec4 texture(usampler1D sampler, float P, float bias = 0) { return new vec4(0); }
        public static vec4 texture(sampler2D sampler, vec2 P, float bias = 0) { return new vec4(0); }
        public static vec4 texture(isampler2D sampler, vec2 P, float bias = 0) { return new vec4(0); }
        public static vec4 texture(usampler2D sampler, vec2 P, float bias = 0) { return new vec4(0); }
        public static vec4 texture(sampler3D sampler, vec3 P, float bias = 0) { return new vec4(0); }
        public static vec4 texture(isampler3D sampler, vec3 P, float bias = 0) { return new vec4(0); }
        public static vec4 texture(usampler3D sampler, vec3 P, float bias = 0) { return new vec4(0); }
        public static vec4 texture(samplerCube sampler, vec3 P, float bias = 0) { return new vec4(0); }
        public static vec4 texture(isamplerCube sampler, vec3 P, float bias = 0) { return new vec4(0); }
        public static vec4 texture(usamplerCube sampler, vec3 P, float bias = 0) { return new vec4(0); }
        public static vec4 texture(sampler1DShadow sampler, vec3 P, float bias = 0) { return new vec4(0); }
        public static vec4 texture(sampler2DShadow sampler, vec3 P, float bias = 0) { return new vec4(0); }
        public static vec4 texture(samplerCubeShadow sampler, vec3 P, float bias = 0) { return new vec4(0); }
        public static vec4 texture(sampler1DArray sampler, vec2 P, float bias = 0) { return new vec4(0); }
        public static vec4 texture(isampler1DArray sampler, vec2 P, float bias = 0) { return new vec4(0); }
        public static vec4 texture(usampler1DArray sampler, vec2 P, float bias = 0) { return new vec4(0); }
        public static vec4 texture(sampler2DArray sampler, vec3 P, float bias = 0) { return new vec4(0); }
        public static vec4 texture(isampler2DArray sampler, vec3 P, float bias = 0) { return new vec4(0); }
        public static vec4 texture(usampler2DArray sampler, vec3 P, float bias = 0) { return new vec4(0); }
        public static vec4 texture(samplerCubeArray sampler, vec4 P, float bias = 0) { return new vec4(0); }
        public static vec4 texture(isamplerCubeArray sampler, vec4 P, float bias = 0) { return new vec4(0); }
        public static vec4 texture(usamplerCubeArray sampler, vec4 P, float bias = 0) { return new vec4(0); }
        public static vec4 texture(sampler1DArrayShadow sampler, vec3 P, float bias = 0) { return new vec4(0); }
        public static vec4 texture(sampler2DArrayShadow sampler, vec4 P, float bias = 0) { return new vec4(0); }
        public static vec4 texture(isampler2DArrayShadow sampler, vec4 P, float bias = 0) { return new vec4(0); }
        public static vec4 texture(usampler2DArrayShadow sampler, vec4 P, float bias = 0) { return new vec4(0); }
        public static vec4 texture(sampler2DRect sampler, vec2 P) { return new vec4(0); }
        public static vec4 texture(isampler2DRect sampler, vec2 P) { return new vec4(0); }
        public static vec4 texture(usampler2DRect sampler, vec2 P) { return new vec4(0); }
        public static vec4 texture(sampler2DRectShadow sampler, vec3 P) { return new vec4(0); }
        public static vec4 texture(samplerCubeArrayShadow sampler, vec4 P, float compare) { return new vec4(0); }
        public static vec4 texture(isamplerCubeArrayShadow sampler, vec4 P, float compare) { return new vec4(0); }
        public static vec4 texture(usamplerCubeArrayShadow sampler, vec4 P, float compare) { return new vec4(0); }

        #endregion

        #region Debug Trace

        public static List<TraceInfo> TraceLog { get; protected set; } = new List<TraceInfo>();

        internal static T TraceVar<T>(T value) => Trace(false, null, value);

        internal static T TraceFunc<T>(T output, params object[] input) => Trace(true, input, output);

        private static T Trace<T>(bool isFunc, object[] input, T output)
        {
            var trace = new StackTrace(true);
            var traceMethod = $"Trace{(isFunc ? "Func" : "Var")}";

            for (var i = 0; i < trace.FrameCount; i++)
            {
                if (traceMethod == trace.GetFrame(i).GetMethod()?.Name)
                {
                    TraceLog.Add(new TraceInfo
                    {
                        Line = trace.GetFrame(i + 2).GetFileLineNumber(),
                        Column = trace.GetFrame(i + 2).GetFileColumnNumber(),
                        Function = isFunc ? trace.GetFrame(i + 1).GetMethod().Name : null,
                        Output = output?.ToString(),
                        Input = input?.Select(x => x.ToString()).ToArray(),
                    });
                    break;
                }
            }
            return output;
        }

        public struct TraceInfo
        {
            public int Line;
            public int Column;
            public string Function;
            public string Output;
            public string[] Input;
            public override string ToString()
            {
                string func;
                switch (Function)
                {
                    case "op_Addition":
                        func = " = " + Input[0] + " + " + Input[1];
                        break;
                    case "op_Substraction":
                        func = " = " + Input[0] + " + " + Input[1];
                        break;
                    case "op_Multiply":
                        func = " = " + Input[0] + " / " + Input[1];
                        break;
                    case "op_Division":
                        func = " = " + Input[0] + " / " + Input[1];
                        break;
                    default:
                        func = Function != null ? " = " + Function + "(" + Input.Cat(", ") + ")" : "";
                        break;
                }
                return "L" + Line + "::C" + Column + "::" + Output + func;
            }
        }

        #endregion

        public abstract void Init(Shader prev, Shader next);

        public abstract void main();

        public struct INOUT
        {
            public vec4 gl_Position;
            public float gl_PointSize;
            public float[] gl_ClipDistance;
        }
    }
}
