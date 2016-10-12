using OpenTK.Graphics.OpenGL4;
using System.Collections.Generic;
using System.Linq;

namespace App
{
    class FXPerf : GLObject
    {
        #region FIELDS

        private DropOutStack<float> timings;
        private DropOutStack<int> frames;
        private uint glqueryStart;
        private uint glqueryEnd;
        private ulong startTime;
        private ulong endTime;
        private int timerStartFrame;
        private bool queryActive;

        #endregion

        #region PROPERTIES

        public int TimingsCount => timings?.Count ?? 0;
        public IEnumerable<float> Timings => timings ?? Enumerable.Empty<float>();
        public IEnumerable<int> Frames => frames ?? Enumerable.Empty<int>();

        #endregion

        #region CONSTRUCTORS

        public FXPerf(string name, string anno, int capacity, bool debugging) : base(name, anno)
        {
            /// INIT
            
            glqueryStart = 0;
            timerStartFrame = -1;
            startTime = 0;
            endTime = 0;
            queryActive = false;

            if (debugging)
                return;

            /// CREATE TIMER STACKS
            
            timings = new DropOutStack<float>(capacity);
            frames = new DropOutStack<int>(capacity);

            /// CREATE OPENGL TIMER QUERY

            glqueryStart = (uint)GL.GenQuery();
            glqueryEnd = (uint)GL.GenQuery();
        }

        public override void Delete()
        {
            base.Delete();
            if (glqueryStart > 0)
            {
                GL.DeleteQuery(glqueryStart);
                glqueryStart = 0;
            }
        }

        #endregion

        #region TIMING

        internal void StartTimer(int frame)
        {
            // begin timer query
            if (glqueryStart > 0 && !queryActive && timerStartFrame < 0)
            {
                GL.QueryCounter(glqueryStart, QueryCounterTarget.Timestamp);
                timerStartFrame = frame;
                queryActive = true;
            }
        }

        internal void EndTimer()
        {
            // end timer query
            if (queryActive)
            {
                GL.QueryCounter(glqueryEnd, QueryCounterTarget.Timestamp);
                queryActive = false;
            }
        }

        internal void MeasureTime()
        {
            if (timerStartFrame < 0)
                return;

            if (startTime == 0)
                GL.GetQueryObject(glqueryStart, GetQueryObjectParam.QueryResultNoWait, out startTime);

            if (endTime == 0)
                GL.GetQueryObject(glqueryEnd, GetQueryObjectParam.QueryResultNoWait, out endTime);

            var elapsedTime = endTime - startTime;

            if (elapsedTime > 0)
            {
                timings.Push((float)(elapsedTime / 1000000.0));
                frames.Push(timerStartFrame);
                timerStartFrame = -1;
                startTime = 0;
                endTime = 0;
            }
        }

        #endregion
    }
}
