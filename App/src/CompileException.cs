using System;
using System.Collections;
using System.Collections.Generic;

namespace App
{
    class CompileException : Exception, IEnumerable<CompileException.Err>
    {
        private List<string> callstack;
        private List<Err> messages = new List<Err>();

        // compile call stack into a single string
        private string callstackstring => callstack.Merge(": ");

        public bool HasErrors() => messages.Count > 0;

        public CompileException() : this(new List<string>()) { }

        private CompileException(List<string> callstack) : base()
        {
            this.callstack = callstack;
        }

        private CompileException(List<string> callstack, string callstackstring)
            : this(callstack)
        {
            callstack.Add(callstackstring);
        }

        public CompileException(string callstackstring) : this()
        {
            callstack.Add(callstackstring);
        }

        public CompileException(CompileException err, string callstackstring)
            : this(err.callstack, callstackstring)
        {
        }

        public CompileException Add(string message, int pos = -1)
        {
            messages.Add(new Err(pos, callstackstring + message));
            return this;
        }

        public CompileException PushCall(string text)
        {
            callstack.Add(text);
            return this;
        }

        public CompileException PopCall()
        {
            callstack.UseIf(callstack.Count > 0)?.RemoveAt(callstack.Count - 1);
            return this;
        }

        public static CompileException operator +(CompileException err, string callLevel)
            => new CompileException(err, callLevel);

        #region IEnumerable Interface
        public IEnumerator<Err> GetEnumerator() => messages.GetEnumerator();

        IEnumerator<Err> IEnumerable<Err>.GetEnumerator() => messages.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => messages.GetEnumerator();
        #endregion

        #region INNER CLASSES
        public struct Err
        {
            public Err(int pos, string msg)
            {
                this.Pos = pos;
                this.Msg = msg;
            }
            public int Pos;
            public string Msg;
        }
        #endregion
    }
}
