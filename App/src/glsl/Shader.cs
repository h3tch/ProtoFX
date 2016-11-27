using App.Glsl.SamplerTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using static System.Reflection.BindingFlags;
using OpenTK.Graphics.OpenGL4;

namespace App.Glsl
{
    #region Necessary Extension

    public static class ArrayExtention
    {
        public static int length(this Array data) => data.Length;
    }

    #endregion

    public class Shader : Access
    {
        #region Qualifiers

        public const string points                  = "points";
        public const string lines                   = "lines";
        public const string lines_adjacency​         = "lines_adjacency​";
        public const string triangles​               = "triangles​";
        public const string triangles_adjacency​     = "triangles_adjacency​";
        public const string quads                   = "quads";
        public const string isolines                = "isolines";
        public const string line_strip              = "line_strip";
        public const string triangle_strip          = "triangle_strip";
        public const string equal_spacing​           = "equal_spacing​";
        public const string fractional_even_spacing​ = "fractional_even_spacing​";
        public const string fractional_odd_spacing​  = "fractional_odd_spacing​";
        public const string std140                  = "std140";
        public const string std430                  = "std430";
        public const string shared                  = "shared";
        public const string packed                  = "packed";
        public const string row_major               = "row_major";
        public const string column_major            = "column_major";
        public const string origin_default          = "origin_default";
        public const string origin_upper_left       = "origin_upper_left";
        public const string pixel_center_default    = "pixel_center_default";
        public const string pixel_center_integer    = "pixel_center_integer";
        public const string default_test            = "default_test";
        public const string early_fragment_tests    = "early_fragment_tests";
        public const string rgba32f                 = "rgba32f";
        public const string rgba16f                 = "rgba16f";
        public const string rg32f                   = "rg32f";
        public const string rg16f                   = "rg16f";
        public const string r11f_g11f_b10f          = "r11f_g11f_b10f";
        public const string r32f                    = "r32f";
        public const string r16f                    = "r16f";
        public const string rgba16                  = "rgba16";
        public const string rgb10_a2                = "rgb10_a2";
        public const string rgba8                   = "rgba8";
        public const string rg16                    = "rg16";
        public const string rg8                     = "rg8";
        public const string r16                     = "r16";
        public const string r8                      = "r8";
        public const string rgba16_snorm            = "rgba16_snorm";
        public const string rgba8_snorm             = "rgba8_snorm";
        public const string rg16_snorm              = "rg16_snorm";
        public const string rg8_snorm               = "rg8_snorm";
        public const string r16_snorm               = "r16_snorm";
        public const string r8_snorm                = "r8_snorm";
        public const string rgba32i                 = "rgba32i";
        public const string rgba16i                 = "rgba16i";
        public const string rgba8i                  = "rgba8i";
        public const string rg32i                   = "rg32i";
        public const string rg16i                   = "rg16i";
        public const string rg8i                    = "rg8i";
        public const string r32i                    = "r32i";
        public const string r16i                    = "r16i";
        public const string r8i                     = "r8i";
        public const string rgba32ui                = "rgba32ui";
        public const string rgba16ui                = "rgba16ui";
        public const string rgb10_a2ui              = "rgb10_a2ui";
        public const string rgba8ui                 = "rgba8ui";
        public const string rg32ui                  = "rg32ui";
        public const string rg16ui                  = "rg16ui";
        public const string rg8ui                   = "rg8ui";
        public const string r32ui                   = "r32ui";
        public const string r16ui                   = "r16ui";
        public const string r8ui                    = "r8ui";

        #endregion

        #region Fields

        protected int LineInFile;
        protected ProgramPipelineParameter ShaderType;
        internal Shader Prev;
        internal static int ShaderLineOffset = 0;
        internal static bool CollectDebugData = false;
        internal static List<TraceInfo> TraceLog = new List<TraceInfo>();
        internal static GLPass.MultiDrawCall DrawCall;
        internal static DebugSettings Settings = new DebugSettings();
        internal static readonly Type[] VecIntTypes = new[] {
            typeof(bvec2), typeof(bvec3), typeof(bvec4),
            typeof(ivec2), typeof(ivec3), typeof(ivec4),
            typeof(uvec2), typeof(uvec3), typeof(uvec4),
        };
        internal static readonly Type[] VecFloatTypes = new[] {
            typeof(vec2), typeof(vec3), typeof(vec4),
            typeof(dvec2), typeof(dvec3), typeof(dvec4)
        };
        internal static readonly Type[] VecTypes = BaseIntTypes.Concat(BaseFloatTypes).ToArray();
        internal static readonly Type[] MatFloatTypes = new[] {
            typeof(mat2), typeof(mat3), typeof(mat4),
        };
        internal static readonly Type[] MatTypes = MatFloatTypes;
        internal static readonly Type[] IntTypes = BaseIntTypes.Concat(VecIntTypes).ToArray();
        internal static readonly Type[] FloatTypes = BaseFloatTypes.Concat(VecIntTypes).Concat(MatFloatTypes).ToArray();

        #endregion

        #region GLSL Built In

        protected int gl_MaxClipDistances;

        #endregion

        #region Constructors

        public Shader(int startLine, ProgramPipelineParameter shaderType)
        {
            LineInFile = startLine;
            ShaderType = shaderType;
            gl_MaxClipDistances = GL.GetInteger(GetPName.MaxClipDistances);
        }

        #endregion

        #region Debug Trace

        /// <summary>
        /// Return list of debug trace information.
        /// </summary>
        public static IEnumerable<TraceInfo> DebugTrace => TraceLog;

        /// <summary>
        /// Clear debug trace.
        /// </summary>
        public static void ClearDebugTrace() => TraceLog.Clear();
        
        /// <summary>
        /// Begin tracing debug information.
        /// </summary>
        protected void BeginTracing()
        {
            CollectDebugData = true;
            ShaderLineOffset = LineInFile;
        }

        /// <summary>
        /// Stop tracting debug information.
        /// </summary>
        protected void EndTracing()
        {
            CollectDebugData = false;
            ShaderLineOffset = 0;
        }

        /// <summary>
        /// Generate debug trace for a variable.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="valueName"></param>
        /// <returns></returns>
        public static T TraceVariable<T>(T value, string valueName)
            => Trace(TraceInfoType.Variable, valueName, value, null);

        /// <summary>
        /// Generate debug trace for a function.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="output"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        public static T TraceFunction<T>(T output, params object[] input)
            => Trace(TraceInfoType.Function, null, output, input);

        /// <summary>
        /// Generate debug trace for an exception.
        /// </summary>
        /// <param name="ex"></param>
        public static void TraceExeption(Exception ex)
        {
            if (!CollectDebugData)
                return;

            var trace = new StackTrace(ex, true);
            var info = new TraceInfo
            {
                Line = trace.GetFrame(0).GetFileLineNumber() + ShaderLineOffset,
                Column = trace.GetFrame(0).GetFileColumnNumber(),
                Type = TraceInfoType.Exception,
                Name = ex.GetType().Name,
                Output = ex.Message,
                Input = null,
            };

            TraceLog.Add(info);

            Debug.Print(ex.GetType().Name + ": " + ex.Message + '\n' + ex.StackTrace);
        }

        /// <summary>
        /// Generate debug trace for variables or functions.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type"></param>
        /// <param name="name"></param>
        /// <param name="output"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        private static T Trace<T>(TraceInfoType type, string name, T output, params object[] input)
        {
            if (!CollectDebugData)
                return output;
            
            var trace = new StackTrace(true);
            var traceFunc = "Trace" + type.ToString();
            var idx = trace.GetFrames().IndexOf(x => x.GetMethod()?.Name == traceFunc);

            TraceLog.Add(new TraceInfo
            {
                Line = trace.GetFrame(idx + 2).GetFileLineNumber() + ShaderLineOffset,
                Column = trace.GetFrame(idx + 2).GetFileColumnNumber(),
                Type = type,
                Name = name == null ? trace.GetFrame(idx + 1).GetMethod().Name : name,
                Output = output,
                Input = input,
            });

            return output;
        }

        #endregion

        #region Process Shader Fields

        private delegate void ProcessField(object obj, FieldInfo field, string prefix);

        protected void ProcessFields(object obj)
        {
            ProcessFields(obj, AllocField, new[] { typeof(__in), typeof(__out), typeof(__uniform) });
            ProcessFields(obj, ProcessInField, new[] { typeof(__in) });
            ProcessFields(obj, ProcessUniformField, new[] { typeof(__uniform) });
        }

        private void ProcessFields(object obj, ProcessField func, Type[] attrType = null, string prefix = "")
        {
            // for all non static fields of the object
            foreach (var field in obj.GetType().GetFields(Public | NonPublic | Instance))
                // check if a valid attribute is defined for the field
                if (attrType == null ? true : attrType.Any(x => field.IsDefined(x)))
                    // process field
                    func(obj, field, prefix);
        }

        private void AllocField(object obj, FieldInfo field, string unused = null)
        {
            // if the field is a primitive, there
            // is nothing to be allocated
            if (field.FieldType.IsPrimitive)
                return;
            var type = field.FieldType;
            var value = field.GetValue(obj);

            // if the field has not been instantiated yet
            // allocate an instance of this type
            if (value == null)
            {
                // allocate an array
                if (type.IsArray)
                {
                    var qualifier = (__array)field.GetCustomAttribute(typeof(__array));
                    value = Array.CreateInstance(type, qualifier?.length ?? 0);
                }
                // allocate an object
                else
                    value = Activator.CreateInstance(type);
                // set the fields value
                field.SetValue(obj, value);
            }

            // if the new value is an array, we also need
            // to allocate the elements of the array
            if (type.IsArray)
            {
                var array = (Array)value;
                for (var i = 0; i < array.Length; i++)
                {
                    if (array.GetValue(i) == null)
                        array.SetValue(Activator.CreateInstance(type), i);
                    ProcessFields(array.GetValue(i), AllocField);
                }
            }
            else
                ProcessFields(value, AllocField);
        }

        private void ProcessInField(object obj, FieldInfo field, string prefix)
        {
            // If this is a input-stream or uniform-buffer structure or class 
            if (field.FieldType.IsClass)
                // process fields of the object
                ProcessFields(field.GetValue(obj), ProcessInField, null, $"{prefix}{field.Name}.");
            else
                // else load input stream data
                field.SetValue(obj, GetInputVarying(prefix + field.Name, field.FieldType));
        }

        private void ProcessUniformField(object obj, FieldInfo field, string prefix)
        {
            var type = field.FieldType;
            // If this is a input-stream or uniform-buffer structure or class 
            if (type.IsClass)
            {
                // process fields of the object
                ProcessFields(field.GetValue(obj), ProcessUniformField, null, $"{prefix}{type.Name}.");
            }
            else
            {
                // if a sampler type that also specifies a layout qualifier
                var value = type.Namespace == typeof(sampler1D).Namespace
                    && field.IsDefined(typeof(__layout))
                    // set to binding value of the layout qualifier
                    ? field.GetCustomAttribute<__layout>().binding
                    // else load uniform buffer data
                    : GetUniform(prefix + field.Name, type, ShaderType);
                field.SetValue(obj, value);
            }
        }

        #endregion

        #region Shader Data Access

        /// <summary>
        /// Get input varying of the previous shader stage
        /// or the default value if the varying does not exist.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="varyingName"></param>
        /// <returns></returns>
        public virtual object GetInputVarying(string varyingName, Type type)
        {
            return Prev != null ? Prev.GetOutputVarying(varyingName, type) : Activator.CreateInstance(type);
        }

        public T GetInputVarying<T>(string varyingName) => (T)GetInputVarying(varyingName, typeof(T));

        /// <summary>
        /// Get the output varying of the shader stage or
        /// the default value if the varying does not exist.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="varyingName"></param>
        /// <returns></returns>
        internal virtual object GetOutputVarying(string varyingName, Type type)
        {
            // search properties
            var props = GetType().GetProperties(NonPublic | Instance);
            foreach (var prop in props)
            {
                // has out qualifier and the respective name
                if (prop.GetCustomAttribute(typeof(__out)) != null && prop.Name == varyingName)
                    return prop.GetValue(this);
            }

            // search fields
            var fields = GetType().GetFields(NonPublic | Instance);
            foreach (var field in fields)
            {
                // has out qualifier and the respective name
                if (field.GetCustomAttribute(typeof(__out)) != null && field.Name == varyingName)
                    return field.GetValue(this);
            }

            // varying could not be found
            return Activator.CreateInstance(type);
        }

        internal T GetOutputVarying<T>(string varyingName) => (T)GetOutputVarying(varyingName, typeof(T));

        protected T GetQualifier<T>(string field)
            => (T)GetType().GetField(field)?.GetCustomAttributes(typeof(T), false)?.FirstOrDefault();

        #endregion

        public virtual void main() { }

        #region Inner Classes

        public class DebugSettings
        {
            [Category("Vertex Shader"), DisplayName("InstanceID"),
             Description("the index of the current instance when doing some form of instanced " +
                "rendering. The instance count always starts at 0, even when using base instance " +
                "calls. When not using instanced rendering, this value will be 0.")]
            public int vs_InstanceID { get; set; } = 0;

            [Category("Vertex Shader"), DisplayName("VertexID"),
             Description("the index of the vertex currently being processed. When using non-indexed " +
                "rendering, it is the effective index of the current vertex (the number of vertices " +
                "processed + the first​ value). For indexed rendering, it is the index used to fetch " +
                "this vertex from the buffer.")]
            public int vs_VertexID { get; set; } = 0;

            [Category("Tesselation"), DisplayName("InvocationID"),
             Description("the index of the shader invocation within this patch. An invocation " +
                "writes to per-vertex output variables by using this to index them.")]
            public int ts_InvocationID { get; set; } = 0;

            [Category("Tesselation"), DisplayName("PrimitiveID"),
             Description("the index of the current patch within this rendering command.")]
            public int ts_PrimitiveID { get; set; } = 0;

            [Category("Tesselation"), DisplayName("TessCoord"),
             Description("the index of the current patch within this rendering command.")]
            public float[] ts_TessCoord { get; set; } = new float[3] { 1, 0, 0 };

            [Category("Geometry Shader"), DisplayName("InvocationID"),
             Description("the current instance, as defined when instancing geometry shaders.")]
            public int gs_InvocationID { get; set; } = 0;

            [Category("Geometry Shader"), DisplayName("PrimitiveIDIn"),
             Description("the current input primitive's ID, based on the number of primitives " +
                "processed by the GS since the current drawing command started.")]
            public int gs_PrimitiveIDIn { get; set; } = 0;

            [Category("Fragment Shader"), DisplayName("FragCoord"),
             Description("The location of the fragment in window space. The X and Y components " +
                "are the window-space position of the fragment.")]
            public int[] fs_FragCoord { get; set; } = new int[2] { 0, 0 };

            [Category("Fragment Shader"), DisplayName("Layer"),
             Description("is either 0 or the layer number for this primitive output by the Geometry Shader.")]
            public int fs_Layer { get; set; } = 0;

            [Category("Fragment Shader"), DisplayName("ViewportIndex"),
             Description("is either 0 or the viewport index for this primitive output by the Geometry Shader.")]
            public int fs_ViewportIndex { get; set; } = 0;

            [Category("Compute Shader"), DisplayName("GlobalInvocationID"),
             Description("uniquely identifies this particular invocation of the compute shader " +
                "among all invocations of this compute dispatch call. It's a short-hand for the " +
                "math computation: gl_WorkGroupID * gl_WorkGroupSize + gl_LocalInvocationID;")]
            public int[] cs_GlobalInvocationID { get; set; } = new int[3] { 0, 0, 0 };
        }

        public enum TraceInfoType
        {
            Variable,
            Function,
            Exception
        }

        public struct TraceInfo
        {
            public int Line;
            public int Column;
            public TraceInfoType Type;
            public string Name;
            public object Output;
            public object[] Input;

            public override string ToString()
            {
                switch (Type)
                {
                    case TraceInfoType.Variable:
                    case TraceInfoType.Exception:
                        return "[L" + Line + ", C" + Column + "] " + Name + ": " + Output.ToString();
                    case TraceInfoType.Function:
                        return "[L" + Line + ", C" + Column + "] " + FunctionName;
                }
                return "[L" + Line + ", C" + Column + "] " + Name + ": "
                    + Output?.ToString() ?? string.Empty + " = ("
                    + Input?.Select(x => x?.ToString() ?? string.Empty).Cat(", ") ?? string.Empty
                    + ")";
            }

            private string FunctionName
            {
                get
                {
                    var Out = Output.ToString() + " = ";
                    switch (Name)
                    {
                        case "op_Addition":
                            return Out + Input[0].ToString() + " + " + Input[1].ToString();
                        case "op_Substraction":
                            return Out + Input[0].ToString() + " + " + Input[1].ToString();
                        case "op_Multiply":
                            return Out + Input[0].ToString() + " / " + Input[1].ToString();
                        case "op_Division":
                            return Out + Input[0].ToString() + " / " + Input[1].ToString();
                    }
                    return Out + Name + "(" + Input.Select(x => x.ToString()).Cat(", ") + ")";
                }
            }
        }

        public struct __InOut
        {
            public vec4 gl_Position;
            public float gl_PointSize;
            public float[] gl_ClipDistance;
            public static __InOut Create()
            {
                var o = new __InOut();
                o.gl_ClipDistance = new float[GL.GetInteger(GetPName.MaxClipDistances)];
                return o;
            }
            public static __InOut[] Create(int n) => Enumerable.Repeat(Create(), n).ToArray();
        }

        #endregion
    }
}
