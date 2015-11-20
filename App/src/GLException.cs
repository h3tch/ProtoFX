using System;
using System.Collections.Generic;

namespace App
{
    class GLException : Exception
    {
        private List<string> callstack;
        private List<string> messages = new List<string>();

        // compile call stack into a single string
        private string callstackstring => callstack.Merge(": ");

        // compile all messages into a single string
        public string Text => messages.Merge("\n");

        public bool HasErrors() => messages.Count > 0;

        public GLException() : this(new List<string>()) { }

        private GLException(List<string> callstack) : base()
        {
            this.callstack = callstack;
        }

        private GLException(List<string> callstack, string callstackstring)
            : this(callstack)
        {
            callstack.Add(callstackstring);
        }

        public GLException(string callstackstring) : this()
        {
            callstack.Add(callstackstring);
        }

        public GLException(GLException err, string callstackstring)
            : this(err.callstack, callstackstring)
        {
        }

        public GLException Add(string message)
        {
            messages.Add(callstackstring + message);
            return this;
        }

        public GLException PushCall(string text)
        {
            callstack.Add(text);
            return this;
        }

        public GLException PopCall()
        {
            callstack.UseIf(callstack.Count > 0)?.RemoveAt(callstack.Count - 1);
            return this;
        }

        public static GLException operator +(GLException err, string callLevel)
            => new GLException(err, callLevel);
    }
}
