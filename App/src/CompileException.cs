using System;
using System.Collections.Generic;

namespace App
{
    class CompileException : Exception
    {
        private List<string> callstack;
        private List<string> messages = new List<string>();

        // compile call stack into a single string
        private string callstackstring => callstack.Merge(": ");

        // compile all messages into a single string
        public string Text => messages.Merge("\n");

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

        public CompileException Add(string message)
        {
            messages.Add(callstackstring + message);
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
    }
}
