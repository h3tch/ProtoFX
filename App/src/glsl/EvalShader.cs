using OpenTK.Graphics.OpenGL4;

namespace App.Glsl
{
    class EvalShader : Shader
    {
        #region Input

#pragma warning disable 0649
#pragma warning disable 0169

        protected int gl_PatchVerticesIn;
        protected int gl_PrimitiveID;
        protected vec3 gl_TessCoord = new vec3();
        protected float[] gl_TessLevelOuter;
        protected float[] gl_TessLevelInner;
        protected __InOut[] gl_in;

        #endregion

        #region Output

        [__out] protected vec4 gl_Position;
        [__out] protected float gl_PointSize;
        [__out] protected float[] gl_ClipDistance;

#pragma warning restore 0649
#pragma warning restore 0169

        #endregion

        #region Constructors

        public EvalShader() : this(-1) { }

        public EvalShader(int startLine) : base(startLine) { }

        #endregion

        /// <summary>
        /// Execute shader and generate debug trace
        /// if the shader is linked to a file.
        /// </summary>
        internal void Debug()
        {
            // get data from the tessellation shader
            GetTesselationOutput(Settings.ts_PrimitiveID, Settings.ts_InvocationID, Settings.ts_TessCoord);
            // only generate debug trace if the shader is linked to a file
            if (LineInFile >= 0)
                BeginTracing();
            // execute the main function of the shader
            main();
            // end debug trace generation
            EndTracing();
        }

        /// <summary>
        /// Execute the shader.
        /// </summary>
        /// <param name="primitiveID"></param>
        /// <param name="invocationID"></param>
        /// <param name="tessCoord"></param>
        internal void Execute(int primitiveID, int invocationID, float[] tessCoord)
        {
            // get data from the tessellation shader
            GetTesselationOutput(primitiveID, invocationID, tessCoord);
            // execute the main function of the shader
            main();
        }

        #region Overrides

        public override void main()
        {
            gl_ClipDistance = new float[gl_in[0].gl_ClipDistance.Length];
            // if patch is a quad
            if (gl_PatchVerticesIn == 4)
            {
                // interpolate vertices
                gl_Position = (
                    mix(gl_in[0].gl_Position, gl_in[1].gl_Position, gl_TessCoord.x) +
                    mix(gl_in[1].gl_Position, gl_in[2].gl_Position, gl_TessCoord.y) +
                    mix(gl_in[2].gl_Position, gl_in[3].gl_Position, gl_TessCoord.x) +
                    mix(gl_in[3].gl_Position, gl_in[0].gl_Position, gl_TessCoord.y)) * 0.25f;
                // interpolate vertex point sizes
                gl_PointSize = (
                    mix(gl_in[0].gl_PointSize, gl_in[1].gl_PointSize, gl_TessCoord.x) +
                    mix(gl_in[1].gl_PointSize, gl_in[2].gl_PointSize, gl_TessCoord.y) +
                    mix(gl_in[2].gl_PointSize, gl_in[3].gl_PointSize, gl_TessCoord.x) +
                    mix(gl_in[3].gl_PointSize, gl_in[0].gl_PointSize, gl_TessCoord.y)) * 0.25f;
                // interpolate clip distances
                for (int i = 0; i < gl_ClipDistance.Length; i++)
                {
                    gl_ClipDistance[i] = (
                        mix(gl_in[0].gl_ClipDistance[i], gl_in[1].gl_ClipDistance[i], gl_TessCoord.x) +
                        mix(gl_in[1].gl_ClipDistance[i], gl_in[2].gl_ClipDistance[i], gl_TessCoord.y) +
                        mix(gl_in[2].gl_ClipDistance[i], gl_in[3].gl_ClipDistance[i], gl_TessCoord.x) +
                        mix(gl_in[3].gl_ClipDistance[i], gl_in[0].gl_ClipDistance[i], gl_TessCoord.y)) * 0.25f;
                }
            }
            // if patch is a triangle
            else
            {
                // interpolate vertices
                gl_Position =
                    gl_in[0].gl_Position * gl_TessCoord.x +
                    gl_in[1].gl_Position * gl_TessCoord.y +
                    gl_in[2].gl_Position * gl_TessCoord.z;
                // interpolate vertex point sizes
                gl_PointSize =
                    gl_in[0].gl_PointSize * gl_TessCoord.x +
                    gl_in[1].gl_PointSize * gl_TessCoord.y +
                    gl_in[2].gl_PointSize * gl_TessCoord.z;
                // interpolate clip distances
                for (int i = 0; i < gl_ClipDistance.Length; i++)
                {
                    gl_ClipDistance[i] =
                        gl_in[0].gl_ClipDistance[i] * gl_TessCoord.x +
                        gl_in[1].gl_ClipDistance[i] * gl_TessCoord.y +
                        gl_in[2].gl_ClipDistance[i] * gl_TessCoord.x;
                }
            }
        }

        private void GetTesselationOutput(int primitiveID, int invocationID, float[] tessCoord)
        {
            // set build in input varyings
            gl_PatchVerticesIn = GL.GetInteger(GetPName.PatchVertices);
            gl_PrimitiveID = primitiveID;
            gl_TessCoord.x = tessCoord[0];
            gl_TessCoord.y = tessCoord[1];
            gl_TessCoord.z = tessCoord[2];
            // get data from previous shader pass
            var tess = (TessShader)Prev;
            tess.Execute(gl_PrimitiveID, invocationID);
            gl_TessLevelOuter = tess.GetOutputVarying<float[]>("gl_TessLevelOuter");
            gl_TessLevelInner = tess.GetOutputVarying<float[]>("gl_TessLevelInner");
            gl_in = tess.GetOutputVarying<__InOut[]>("gl_out");
        }

        public static object GetUniform<T>(string uniformName)
            => GetUniform<T>(uniformName, ProgramPipelineParameter.TessEvaluationShader);

        private float mix(float a, float b, float t) => a * (1 - t) + b * t;
        private vec4 mix(vec4 a, vec4 b, float t) => a * (1 - t) + b * t;

        #endregion
    }
}
