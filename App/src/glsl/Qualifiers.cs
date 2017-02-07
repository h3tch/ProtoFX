using System;

namespace App.Glsl
{
    [AttributeUsage(AttributeTargets.All)]
    public class __in : Attribute { }

    [AttributeUsage(AttributeTargets.All)]
    public class __float : Attribute { }

    [AttributeUsage(AttributeTargets.All)]
    public class __smooth : Attribute { }

    [AttributeUsage(AttributeTargets.All)]
    public class __out : Attribute { }

    [AttributeUsage(AttributeTargets.All)]
    public class __array : Attribute
    {
        public int length = 0;
    }

    [AttributeUsage(AttributeTargets.All)]
    public class __uniform : Attribute { }

    [AttributeUsage(AttributeTargets.All)]
    public class __layout : Attribute
    {
#pragma warning disable 0649
#pragma warning disable 0169

        public object[] @params;
        public int location = -1;
        public int index = -1;
        public int component = -1;
        public int binding = -1;
        public int offset = -1;
        public int vertices = -1;
        public int invocations = 1;
        public int max_vertices = 1;
        public int xfb_offset = -1;
        public int xfb_stride = -1;
        public int xfb_buffer = -1;

#pragma warning restore 0649
#pragma warning restore 0169

        public __layout() { }
        public __layout(params object[] @params)
        {
            this.@params = @params;
        }
    }
}
