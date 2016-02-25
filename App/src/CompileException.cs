using System;
using System.Collections;
using System.Collections.Generic;

namespace App
{
    class CompileException : Exception, IEnumerable<CompileException.Error>, IDisposable
    {
        private List<string> callstack;
        private List<Error> messages;

        // compile call stack into a single string
        private string callstackstring => callstack.Cat(": ");

        /// <summary>
        /// Returns true if there are any errors messages in the class.
        /// </summary>
        /// <returns></returns>
        public bool HasErrors() => messages.Count > 0;

        /// <summary>
        /// Create new compiler exception.
        /// </summary>
        /// <param name="callstackstring">Indicates the current position in the call stack.</param>
        public CompileException(string callstackstring)
        {
            callstack = new List<string>();
            callstack.Add(callstackstring);
            messages = new List<Error>();
        }
        
        /// <summary>
        /// Derive new compiler exception from existing one by keeping the call stack.
        /// </summary>
        /// <param name="err"></param>
        /// <param name="callstackstring"></param>
        private CompileException(CompileException err, string callstackstring)
        {
            callstack = err.callstack;
            callstack.Add(callstackstring);
            messages = err.messages;
        }
        
        /// <summary>
        /// Add a compiler error message to the exception.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="file"></param>
        /// <param name="line"></param>
        /// <returns></returns>
        public CompileException Add(string message, string file, int line)
        {
            messages.Add(new Error(file, line, callstackstring + message));
            return this;
        }
        
        /// <summary>
        /// Add another level to the call stack.
        /// </summary>
        /// <param name="err"></param>
        /// <param name="callLevel"></param>
        /// <returns></returns>
        public static CompileException operator | (CompileException err, string callLevel)
            => new CompileException(err, callLevel);

        #region Interfaces
        public IEnumerator<Error> GetEnumerator() => messages.GetEnumerator();

        IEnumerator<Error> IEnumerable<Error>.GetEnumerator() => messages.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => messages.GetEnumerator();

        public void Dispose()
        {
            if (callstack.Count > 0)
                callstack.RemoveAt(callstack.Count - 1);
        }
        #endregion

        #region INNER CLASSES
        public struct Error
        {
            public Error(string file, int line, string msg)
            {
                File = file;
                Line = line;
                Msg = msg;
            }
            public string File;
            public int Line;
            public string Msg;
        }
        #endregion
    }
}
