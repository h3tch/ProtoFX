using App.Glsl.SamplerTypes;
using System;
using System.ComponentModel;
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

    public abstract class Shader : Access
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

        #region Process Shader Fields

        protected delegate void ProcessField(object obj, FieldInfo field, object user);

        /// <summary>
        /// Process all fields of the shader.
        /// This includes allocation and loading of the data.
        /// </summary>
        /// <param name="obj"></param>
        protected void ProcessFields(object obj)
        {
            // allocate field data
            ProcessFields(obj, AllocField, new[] { typeof(__in), typeof(__out), typeof(__uniform) });
            // load uniform fields
            ProcessFields(obj, ProcessUniformField, new[] { typeof(__uniform) });
        }

        protected void ProcessFields(object obj, ProcessField func, Type[] attrType = null, object user = null)
        {
            // for all non static fields of the object
            foreach (var field in obj.GetType().GetFields(Public | NonPublic | Instance))
                // check if a valid attribute is defined for the field
                if (attrType?.Any(x => field.IsDefined(x)) ?? true)
                    // process field
                    func(obj, field, user);
        }

        private void AllocField(object obj, FieldInfo field, object unused = null)
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
                    int size = 0;
                    if (field.IsDefined(typeof(__in)))
                    {
                        var layout = GetQualifier<__layout>("__in__");
                        if (layout != null)
                        {
                            if (layout.@params.Any(x => x.Equals(points)))
                                size = 1;
                            else if (layout.@params.Any(x => x.Equals(lines)))
                                size = 2;
                            else if (layout.@params.Any(x => x.Equals(triangles)))
                                size = 3;
                        }
                    }
                    else if (field.IsDefined(typeof(__out)))
                    {
                        var layout = GetQualifier<__layout>("__out__");
                        if (layout != null)
                            size = layout.max_vertices;
                    }
                    else
                    {
                        var qualifier = (__array)field.GetCustomAttribute(typeof(__array));
                        if (qualifier != null)
                            size = qualifier.length;
                    }
                    value = Array.CreateInstance(type.GetElementType(), size);
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
                        array.SetValue(Activator.CreateInstance(type.GetElementType()), i);
                    ProcessFields(array.GetValue(i), AllocField);
                }
            }
            else
                ProcessFields(value, AllocField);
        }

        private void ProcessUniformField(object obj, FieldInfo field, object user)
        {
            var type = field.FieldType;
            var prefix = user as string;
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
                // get type converter
                var converter = TypeDescriptor.GetConverter(type);
                // convert value to the field type
                if (converter.CanConvertFrom(value.GetType()))
                    field.SetValue(obj, converter.ConvertFrom(value));
                else
                    // this code will cause an exception, but will
                    // be necessary to find bugs in the debugger
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
        public virtual object GetInputVarying(Type type, string varyingName = null)
            => Prev?.GetOutputVarying(type, varyingName) ?? Activator.CreateInstance(type);

        /// <summary>
        /// Get the output varying of the shader stage or
        /// the default value if the varying does not exist.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="varyingName"></param>
        /// <returns></returns>
        internal virtual object GetOutputVarying(Type type, string varyingName)
        {
            // search fields
            var fields = GetType().GetFields(NonPublic | Instance);
            foreach (var field in fields)
            {
                // has out qualifier and the respective name
                if (field.IsDefined(typeof(__out)) && field.FieldType.Name == type.Name)
                {
                    if (varyingName != null && field.Name != varyingName)
                        continue;
                    return field.GetValue(this).DeepCopy(type);
                }
            }

            // varying could not be found
            return Activator.CreateInstance(type);
        }

        internal T GetOutputVarying<T>(string varyingName = null)
            => (T)GetOutputVarying(typeof(T), varyingName);

        protected T GetQualifier<T>(string field) where T: Attribute
            => GetType().GetField(field, Instance | Public | NonPublic)?.GetCustomAttribute<T>();

        #endregion

        /// <summary>
        /// Generate debug trace for a variable.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="valueName"></param>
        /// <returns></returns>
        public static T TraceVariable<T>(Location location, T value, string valueName)
            => Debugger.Trace(TraceInfoType.Variable, location, valueName, value);

        /// <summary>
        /// Generate debug trace for a function.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="output"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        public static T TraceFunction<T>(Location location, T value, string funcName)
            => Debugger.Trace(TraceInfoType.Function, location, funcName, value);

        public abstract void main();

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
        
        public class __InOut
        {
            public vec4 gl_Position;
            public float gl_PointSize;
            public float[] gl_ClipDistance;
            public __InOut()
            {
                gl_Position = new vec4();
                gl_PointSize = 0f;
                gl_ClipDistance = new float[GL.GetInteger(GetPName.MaxClipDistances)];
            }
            public static __InOut[] Create(int n) => Enumerable.Repeat(new __InOut(), n).ToArray();
        }

        #endregion
    }
}
