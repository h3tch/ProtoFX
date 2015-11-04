using OpenTK.Graphics.OpenGL4;
using System.Text;
using System.Linq;

namespace App
{
    using static ProgramProperty;
    using Property = ProgramProperty;
    using Interface = ProgramInterface;

    class Introspection
    {
        public Uniform[] uniforms;
        public UniformBlock[] uniformBlocks;
        public Feedback[] feedback;
        public Varying[] varyings;

        public class Uniform
        {
            public string name;
            public ActiveUniformType type;
            public int location;
            public int offset;
            public int stride;
            public int length;

            public Uniform(int prog, int uniform)
            {
                var @params = GetResourceParams(prog, Interface.Uniform, uniform,
                    new[] { Type, Location, Offset, ArrayStride, ArraySize });

                name = GetResourceName(prog, Interface.Uniform, uniform);
                type = (ActiveUniformType)@params[0];
                location = @params[1];
                offset = @params[2];
                stride = @params[3];
                length = @params[4];
            }
        }

        public class UniformBlock
        {
            public string name;
            public int binding;
            public int size;
            public Uniform[] uniforms;

            public UniformBlock(int prog, int block)
            {
                var @params = GetResourceParams(prog, Interface.UniformBlock, block,
                    new[] { BufferBinding, BufferDataSize });

                name = GetResourceName(prog, Interface.UniformBlock, block);
                binding = @params[0];
                size = @params[1];
            }
        }

        public class Feedback
        {
            public string name;
            public int binding;
            public int stride;
            public int size;

            public Feedback(int prog, int feedback)
            {
                var @params = GetResourceParams(prog, Interface.TransformFeedbackBuffer, feedback,
                    new[] { BufferBinding, TransformFeedbackBufferStride, BufferDataSize, NumActiveVariables });

                name = GetResourceName(prog, Interface.TransformFeedbackBuffer, feedback);
                binding = @params[0];
                stride = @params[1];
                size = @params[2];
            }
        }

        public class Varying
        {
            public string name;
            public ActiveUniformType type;
            public int offset;
            public int length;
            public int bufferIndex;

            public Varying(int prog, int varying)
            {
                var @params = GetResourceParams(prog, Interface.TransformFeedbackVarying, varying,
                    new[] { Type, Offset, ArraySize, TransformFeedbackBufferIndex });

                name = GetResourceName(prog, Interface.TransformFeedbackVarying, varying);
                type = (ActiveUniformType)@params[0];
                offset = @params[1];
                length = @params[2];
                bufferIndex = @params[3];
            }
        }

        public Introspection(int program)
        {
            int numUnif, numBlocks, numFeedback, numVarying, numInputs, numOutputs;
            var ACTIVE = ProgramInterfaceParameter.ActiveResources;

            GL.GetProgramInterface(program, Interface.Uniform, ACTIVE, out numUnif);
            GL.GetProgramInterface(program, Interface.UniformBlock, ACTIVE, out numBlocks);
            GL.GetProgramInterface(program, Interface.TransformFeedbackBuffer, ACTIVE, out numFeedback);
            GL.GetProgramInterface(program, Interface.TransformFeedbackVarying, ACTIVE, out numVarying);
            GL.GetProgramInterface(program, Interface.ProgramInput, ACTIVE, out numInputs);
            GL.GetProgramInterface(program, Interface.ProgramOutput, ACTIVE, out numOutputs);

            uniforms = Enumerable.Range(0, numUnif).Select(x => new Uniform(program, x)).ToArray();
            uniformBlocks = Enumerable.Range(0, numBlocks).Select(x => new UniformBlock(program, x)).ToArray();
            feedback = Enumerable.Range(0, numFeedback).Select(x => new Feedback(program, x)).ToArray();
            varyings = Enumerable.Range(0, numVarying).Select(x => new Varying(program, x)).ToArray();
        }

        private static string GetResourceName(int program, Interface @interface, int uniform)
        {
            int nameLen = 128;
            StringBuilder str = new StringBuilder(nameLen);
            GL.GetProgramResourceName(program, @interface, uniform, nameLen, out nameLen, str);
            return str.ToString();
        }

        private static int[] GetResourceParams(int program, Interface @interface, int uniform, Property[] props)
        {
            var numParams = props.Length;
            var @params = new int[numParams];
            GL.GetProgramResource(program, @interface, uniform,
                props.Length, props, numParams, out numParams, @params);
            return @params;
        }
    }
}
