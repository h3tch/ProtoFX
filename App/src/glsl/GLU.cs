using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace protofx.Glsl
{
    public static class GLU
    {
        public static int ActivePipeline => GL.GetInteger(GetPName.ProgramPipelineBinding);

        public static int ActiveProgram(ProgramPipelineParameter shaderType, int pipeline = 0)
        {
            if (pipeline == 0)
                pipeline = ActivePipeline;
            if (pipeline == 0)
                return 0;
            GL.GetProgramPipeline(pipeline, shaderType, out int program);
            return program;
        }

        public static (Dictionary<int, string>, Dictionary<string, int>) InputLocationMappings(int program)
        {
            // we want infos about the shader input varyings
            const ProgramInterface @interface = ProgramInterface.ProgramInput;

            // get number of shader input varyings
            GL.GetProgramInterface(program, @interface,
                ProgramInterfaceParameter.ActiveResources, out int numResources);

            // define necessary variables
            var props = new[] {
                ProgramProperty.Location, // input varyings location
                ProgramProperty.ArraySize // input varyings array size
            };
            var values = new int[props.Length];
            var name = new StringBuilder(128);
            var attr2loc = new Dictionary<string, int>(numResources);
            var loc2attr = new Dictionary<int, string>(numResources);

            // define helper functions
            int AttrLocation() => values[0];
            int AttrArraySize() => values[1];

            for (int resIdx = 0; resIdx < numResources; resIdx++)
            {
                // retrieve shader input varying information
                #pragma warning disable CS0618
                GL.GetProgramResource(program, @interface, resIdx, props.Length, props,
                    values.Length, out int numValues, values);
                GL.GetProgramResourceName(program, @interface, resIdx, name.Capacity,
                    out int nameLength, name);
                #pragma warning restore CS0618

                // add shader input attribute
                for (int arrayIdx = 0; arrayIdx < AttrArraySize(); arrayIdx++)
                {
                    var attr = name.ToString().Replace("[0]", $"[{arrayIdx}]");
                    var loc = AttrLocation() + arrayIdx;
                    attr2loc.Add(attr, loc);
                    loc2attr.Add(loc, attr);
                }
            }

            return (loc2attr, attr2loc);
        }
    }
}
