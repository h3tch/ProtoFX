using OpenTK.Graphics.OpenGL4;

namespace App.Glsl
{
    abstract class TessShader : Shader
    {
        #region Input
        
        int gl_PatchVerticesIn;
        int gl_PrimitiveID;
        int gl_InvocationID;
        __InOut[] gl_in = new __InOut[4];

        #endregion

        #region Output

        [__out] float[] gl_TessLevelOuter = new float[4];
        [__out] float[] gl_TessLevelInner = new float[2];
        [__out] __InOut[] gl_out = new __InOut[4];

        #endregion

        internal void Debug(int primitiveID, int invocationID)
        {
            DebugTrace.Clear();
            TraceLog = DebugTrace;
            Execute(Settings.ts_PrimitiveID, Settings.ts_InvocationID);
            TraceLog = null;
        }

        internal void Execute(int primitiveID, int invocationID)
        {
            // load patch data from vertex shader
            var inum = GL.GetInteger(GetPName.PatchVertices);
            var isize = 4;
            switch (drawcall.cmd[0].indextype)
            {
                case DrawElementsType.UByte: isize = 1; break;
                case DrawElementsType.UShort: isize = 2; break;
            }
            var patch = new byte[isize * inum];
            drawcall.indbuf.Read(ref patch, patch.Length * primitiveID);
            
            // set shader input
            gl_PatchVerticesIn = inum;
            gl_PrimitiveID = primitiveID;
            gl_InvocationID = invocationID;

            var vert = (VertShader)prev;
            for (int i = 0; i < gl_PatchVerticesIn; i++)
            {
                vert.Execute(patch[i], gl_InvocationID);
                gl_in[i].gl_Position = vert.GetOutputVarying<vec4>("gl_Position");
                gl_in[i].gl_PointSize = vert.GetOutputVarying<float>("gl_PointSize");
                gl_in[i].gl_ClipDistance = vert.GetOutputVarying<float[]>("gl_ClipDistance");
            }

            // execute shader
            main();
        }
    }
}
