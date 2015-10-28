using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace util
{
    public class Uniforms<Names> where Names : struct, IConvertible
    {
        private const ActiveUniformParameter first = ActiveUniformParameter.UniformType;
        private int[] location;
        private int[] length;
        private int[] offset;
        private int[] stride;

        public Uniforms(Dictionary<string, int[]> uniforms, string name)
        {
            string[] names = Enum.GetNames(typeof(Names)).Select(v => name + "." + v).ToArray();
            location = Enumerable.Repeat(-1, names.Length).ToArray();
            length = new int[names.Length];
            offset = new int[names.Length];
            stride = new int[names.Length];

            for (int i = 0; i < names.Length; i++)
            {
                int[] v;
                if (uniforms.TryGetValue(names[i], out v)
                    || uniforms.TryGetValue(names[i] + "[0]", out v))
                {
                    location[i] = v[v.Length - 1];
                    length[i] = v[ActiveUniformParameter.UniformSize - first];
                    offset[i] = v[ActiveUniformParameter.UniformOffset - first];
                    stride[i] = v[ActiveUniformParameter.UniformArrayStride - first];
                }
            }
        }

        public int this[Names name]
        {
            get { return location[Convert.ToInt32(name)]; }
        }

        public int Length(Names name)
        {
            return length[Convert.ToInt32(name)];
        }

        public int Stide(Names name)
        {
            return stride[Convert.ToInt32(name)];
        }
    }
}
