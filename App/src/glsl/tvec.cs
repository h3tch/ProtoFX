using System.Collections.Generic;
using System.Linq;

namespace App.Glsl
{
    public class tvecN<T>
    {
        internal T[] v;
        public T this[int i] { get { return v[i]; } set { v[i] = value; } }
        public tvecN(params T[] V) { v = V.ToArray();  }
        public override string ToString() => "(" + v.Select(x => x.ToString()).Cat(", ") + ")";
    }
}
