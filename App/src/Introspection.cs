using OpenTK.Graphics.OpenGL4;
using System.Text;

namespace App
{
    class Introspection
    {
        public Uniform[] uniforms;
        public UniformBlock[] uniformBlocks;
        public TransformFeedback transformFeedback;

        public struct Uniform
        {
            string name;
            int location;
            int offset;
            int stride;
            int length;
            ActiveUniformType type;
        }

        public struct UniformBlock
        {
            string name;
            int binding;
            int index;
            int size;
            Uniform[] uniforms;
        }

        public struct TransformFeedback
        {
            string name;
            int binding;
            int stride;
            int size;
            int varyingMode;
            Varying[] varyings;
        }

        public struct Varying
        {
            string name;
            int offset;
            int length;
        }

        public Introspection(int program)
        {
            int numUnif;
            GL.GetProgramInterface(program, ProgramInterface.Uniform,
                ProgramInterfaceParameter.ActiveResources, out numUnif);

            var props = new[] { ProgramProperty.Type, ProgramProperty.BlockIndex };
            var numParams = props.Length;
            var @params = new int[numParams];
            GL.GetProgramResource(program, ProgramInterface.Uniform, 0,
                props.Length, props, numParams, out numParams, @params);

            int nameLen = 128;
            StringBuilder str = new StringBuilder(nameLen);
            GL.GetProgramResourceName(program, ProgramInterface.Uniform, 0, nameLen, out nameLen, str);
        }
    }
}
