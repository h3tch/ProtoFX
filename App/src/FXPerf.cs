using System.Collections.Generic;

namespace App
{
    class FXPerf : GLObject
    {
        protected DropOutStack<float> timings = new DropOutStack<float>(60 * 5);
        protected DropOutStack<int> frames = new DropOutStack<int>(60 * 5);

        public int TimingsCount => timings?.Count ?? 0;
        public IEnumerable<float> Timings => timings;
        public IEnumerable<int> Frames => frames;

        public FXPerf(string name, string anno) : base(name, anno) { }
    }
}
