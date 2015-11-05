//using OpenTK.Graphics.OpenGL4;
//using System.Text;
//using System.Linq;

namespace App
{
    //using static ProgramProperty;
    //using Property = ProgramProperty;
    //using Interface = ProgramInterface;

    class Resources
    {
        //public Uniform[] uniforms;
        //public UBlock[] uBlocks;
        //public Variable[] variables;
        //public Feedback[] feedback;
        //public Varying[] varyings;
        //public Attribute[] input;
        //public Attribute[] output;

        //public class Uniform
        //{
        //    public string name;
        //    public ActiveUniformType type;
        //    public int location;
        //    public int blockIdx;
        //    public int offset;
        //    public int stride;
        //    public int length;
        //    public int matStride;
        //    public int isRowMajor;

        //    public Uniform(int prog, int uniform)
        //    {
        //        var @params = GetResourceParams(prog, Interface.Uniform, uniform,
        //            new[] { Type, Location, BlockIndex, Offset, ArrayStride, ArraySize,
        //                MatrixStride, IsRowMajor });

        //        name = GetResourceName(prog, Interface.Uniform, uniform);
        //        type = (ActiveUniformType)@params[0];
        //        location = @params[1];
        //        blockIdx = @params[2];
        //        offset = @params[3];
        //        stride = @params[4];
        //        length = @params[5];
        //        matStride = @params[6];
        //        isRowMajor = @params[7];
        //    }
        //}

        //public class UBlock
        //{
        //    public string name;
        //    public int binding;
        //    public int size;

        //    public UBlock(int prog, int block)
        //    {
        //        var @params = GetResourceParams(prog, Interface.UniformBlock, block,
        //            new[] { BufferBinding, BufferDataSize });

        //        name = GetResourceName(prog, Interface.UniformBlock, block);
        //        binding = @params[0];
        //        size = @params[1];
        //    }
        //}

        //public class Feedback
        //{
        //    public string name;
        //    public int binding;
        //    public int stride;
        //    public int size;

        //    public Feedback(int prog, int feedback)
        //    {
        //        var @params = GetResourceParams(prog, Interface.TransformFeedbackBuffer, feedback,
        //            new[] { BufferBinding, TransformFeedbackBufferStride, BufferDataSize, NumActiveVariables });

        //        name = GetResourceName(prog, Interface.TransformFeedbackBuffer, feedback);
        //        binding = @params[0];
        //        stride = @params[1];
        //        size = @params[2];
        //    }
        //}

        //public class Varying
        //{
        //    public string name;
        //    public ActiveUniformType type;
        //    public int offset;
        //    public int length;
        //    public int bufferIndex;

        //    public Varying(int prog, int varying)
        //    {
        //        var @params = GetResourceParams(prog, Interface.TransformFeedbackVarying, varying,
        //            new[] { Type, Offset, ArraySize, TransformFeedbackBufferIndex });

        //        name = GetResourceName(prog, Interface.TransformFeedbackVarying, varying);
        //        type = (ActiveUniformType)@params[0];
        //        offset = @params[1];
        //        length = @params[2];
        //        bufferIndex = @params[3];
        //    }
        //}

        //public class Attribute
        //{
        //    public string name;
        //    public ActiveUniformType type;
        //    public int location;
        //    public int length;
        //    public int locationComponent;
        //    public int isPerPatch;

        //    public Attribute(int prog, Interface @interface, int uniform)
        //    {
        //        var @params = GetResourceParams(prog, @interface, uniform,
        //            new[] { Type, Location, ArraySize, LocationComponent, IsPerPatch });

        //        name = GetResourceName(prog, @interface, uniform);
        //        type = (ActiveUniformType)@params[0];
        //        location = @params[1];
        //        length = @params[2];
        //        locationComponent = @params[3];
        //        isPerPatch = @params[4];
        //    }
        //}

        //public class Variable
        //{
        //    public string name;
        //    public ActiveUniformType type;
        //    public int blockIdx;
        //    public int offset;
        //    public int stride;
        //    public int length;
        //    public int topLevelStride;
        //    public int topLevelLength;
        //    public int matStride;
        //    public int isRowMajor;

        //    public Variable(int prog, int uniform)
        //    {
        //        var @params = GetResourceParams(prog, Interface.BufferVariable, uniform,
        //            new[] { Type, BlockIndex, Offset, ArrayStride, ArraySize, TopLevelArrayStride,
        //                TopLevelArraySize, MatrixStride, IsRowMajor });

        //        name = GetResourceName(prog, Interface.BufferVariable, uniform);
        //        type = (ActiveUniformType)@params[0];
        //        blockIdx = @params[1];
        //        offset = @params[2];
        //        stride = @params[3];
        //        length = @params[4];
        //        topLevelStride = @params[5];
        //        topLevelLength = @params[6];
        //        matStride = @params[7];
        //        isRowMajor = @params[8];
        //    }
        //}

        //public Resources(int prog)
        //{
        //    int numUnif, numBlocks, numFeedback, numVarying, numInputs, numOutputs;
        //    var ACTIVE = ProgramInterfaceParameter.ActiveResources;

        //    GL.GetProgramInterface(prog, Interface.Uniform, ACTIVE, out numUnif);
        //    GL.GetProgramInterface(prog, Interface.UniformBlock, ACTIVE, out numBlocks);
        //    GL.GetProgramInterface(prog, Interface.BufferVariable, ACTIVE, out numBlocks);
        //    GL.GetProgramInterface(prog, Interface.TransformFeedbackBuffer, ACTIVE, out numFeedback);
        //    GL.GetProgramInterface(prog, Interface.TransformFeedbackVarying, ACTIVE, out numVarying);
        //    GL.GetProgramInterface(prog, Interface.ProgramInput, ACTIVE, out numInputs);
        //    GL.GetProgramInterface(prog, Interface.ProgramOutput, ACTIVE, out numOutputs);

        //    uniforms = Enumerable.Range(0, numUnif).Select(x => new Uniform(prog, x)).ToArray();
        //    uBlocks = Enumerable.Range(0, numBlocks).Select(x => new UBlock(prog, x)).ToArray();
        //    variables = Enumerable.Range(0, numBlocks).Select(x => new Variable(prog, x)).ToArray();
        //    feedback = Enumerable.Range(0, numFeedback).Select(x => new Feedback(prog, x)).ToArray();
        //    varyings = Enumerable.Range(0, numVarying).Select(x => new Varying(prog, x)).ToArray();
        //    input = Enumerable.Range(0, numInputs).Select(
        //        x => new Attribute(prog, Interface.ProgramInput, x)).ToArray();
        //    output = Enumerable.Range(0, numOutputs).Select(
        //        x => new Attribute(prog, Interface.ProgramOutput, x)).ToArray();
        //}

        //private static string GetResourceName(int prog, Interface @interface, int id)
        //{
        //    int nameLen = 128;
        //    StringBuilder str = new StringBuilder(nameLen);
        //    GL.GetProgramResourceName(prog, @interface, id, nameLen, out nameLen, str);
        //    return str.ToString();
        //}

        //private static int[] GetResourceParams(int prog, Interface @interface, int id, Property[] props)
        //{
        //    var numParams = props.Length;
        //    var @params = new int[numParams];
        //    GL.GetProgramResource(prog, @interface, id,
        //        props.Length, props, numParams, out numParams, @params);
        //    return @params;
        //}
    }
}
