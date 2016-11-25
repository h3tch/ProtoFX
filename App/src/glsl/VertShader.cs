using OpenTK.Graphics.OpenGL4;
using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using static System.Reflection.BindingFlags;

namespace App.Glsl
{
    public class VertShader : Shader
    {
        #region Input

#pragma warning disable 0649
#pragma warning disable 0169
        protected int gl_VertexID;
        protected int gl_InstanceID;
        protected int gl_MaxClipDistances;

        #endregion

        #region Output
        
        [__out] protected vec4 gl_Position;
        [__out] protected float gl_PointSize;
        [__out] protected float[] gl_ClipDistance;
#pragma warning restore 0649
#pragma warning restore 0169

        #endregion
        
        #region Constructors

        public VertShader() : this(-1) { }

        public VertShader(int startLine) : base(startLine)
        {
            gl_MaxClipDistances = GL.GetInteger(GetPName.MaxClipDistances);
            gl_ClipDistance = new float[gl_MaxClipDistances];
        }

        #endregion

        /// <summary>
        /// Execute shader and generate debug trace
        /// if the shader is linked to a file.
        /// </summary>
        internal void Debug()
        {
            try
            { 
                Execute(Settings.vs_VertexID, Settings.vs_InstanceID, LineInFile >= 0);
            }
            catch (Exception e)
            {
                TraceExeption(e);
            }
            finally
            {
                EndTracing();
            }
        }

        /// <summary>
        /// Execute the shader.
        /// </summary>
        /// <param name="vertexID"></param>
        /// <param name="instanceID"></param>
        internal void Execute(int vertexID, int instanceID, bool debug = false)
        {
            DebugGetError(new StackTrace(true));

            // only generate debug trace if the shader is linked to a file
            if (debug)
                BeginTracing();

            // set shader input
            gl_VertexID = vertexID;
            gl_InstanceID = instanceID;
            ProcessFields(this, AllocField, new[] { typeof(__in), typeof(__out), typeof(__uniform) });
            ProcessFields(this, ProcessInField, new[] { typeof(__in) });
            ProcessFields(this, ProcessUniformField, new[] { typeof(__uniform) });
            // execute shader
            main();

            DebugGetError(new StackTrace(true));
        }

        #region Overrides
        private delegate void ProcessField(object obj, FieldInfo field, string prefix);

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
            // If this is a input-stream or uniform-buffer structure or class 
            if (field.FieldType.IsClass)
                // process fields of the object
                ProcessFields(field.GetValue(obj), ProcessUniformField, null, $"{prefix}{field.FieldType.Name}.");
            else
                // else load input stream data
                field.SetValue(obj, GetUniform(prefix + field.Name, field.FieldType,
                    ProgramPipelineParameter.VertexShader));
        }

        private object GetInputVarying(string varyingName, Type type)
        {
            DebugGetError(new StackTrace(true));

            if (DrawCall == null)
                return DebugGetError(type, new StackTrace(true));

            // get current shader program pipeline
            var pipeline = GL.GetInteger(GetPName.ProgramPipelineBinding);
            if (pipeline <= 0)
                return DebugGetError(type, new StackTrace(true));

            // get vertex shader
            int program;
            GL.GetProgramPipeline(pipeline, ProgramPipelineParameter.VertexShader, out program);
            if (program <= 0)
                return DebugGetError(type, new StackTrace(true));

            // get vertex input attribute location
            var location = GL.GetAttribLocation(program, varyingName);
            if (location < 0)
                return DebugGetError(type, new StackTrace(true));

            // get vertex input data
            var data = DrawCall?.vertin.GetVertexData(gl_VertexID, gl_InstanceID);
            var array = data?.Skip(location).First();
            if (array == null)
                return DebugGetError(type, new StackTrace(true));

            DebugGetError(new StackTrace(true));

            // return base type
            if (BaseTypes.Any(x => x == type))
                return array.To(type).GetValue(0);

            // create new object from byte array
            return type.GetConstructor(new[] { typeof(byte[]) })?.Invoke(new[] { array });
        }
        
        #endregion
    }
}
