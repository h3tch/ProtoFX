using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace protofx
{
    [Serializable]
    class CompileException : Exception, IEnumerable<CompileException.MessageInfo>, IDisposable
    {
        private List<string> callstack;
        private List<MessageInfo> messages;

        // compile call stack into a single string
        private string Callstackstring => callstack.Cat(": ") + ": ";

        /// <summary>
        /// Returns true if there are any errors messages in the class.
        /// </summary>
        /// <returns></returns>
        public bool HasErrors => messages.Any(m => m.Category == MessageCategory.Error);

        /// <summary>
        /// Create new compiler exception.
        /// </summary>
        /// <param name="callstackstring">Indicates the current position in the call stack.</param>
        public CompileException(string callstackstring)
        {
            callstack = new List<string> { callstackstring };
            messages = new List<MessageInfo>();
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
        public CompileException Error(string message, string file, int line)
        {
            messages.Add(new MessageInfo(MessageCategory.Error, file, line, Callstackstring + message));
            return this;
        }

        /// <summary>
        /// Add a compiler info message to the exception.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="file"></param>
        /// <param name="line"></param>
        /// <returns></returns>
        public CompileException Info(string message, string file, int line)
        {
            messages.Add(new MessageInfo(MessageCategory.Info, file, line, Callstackstring + message));
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

        public IEnumerator<MessageInfo> GetEnumerator()
        {
            return messages.GetEnumerator();
        }

        IEnumerator<MessageInfo> IEnumerable<MessageInfo>.GetEnumerator()
        {
            return messages.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return messages.GetEnumerator();
        }

        public void Dispose()
        {
            if (callstack.Count > 0)
                callstack.RemoveAt(callstack.Count - 1);
        }

        #endregion

        #region INNER CLASSES

        public enum MessageCategory
        {
            Error,
            Warning,
            Info,
            Debug,
        }

        public struct MessageInfo
        {
            public MessageInfo(MessageCategory category, string file, int line, string msg)
            {
                Category = category;
                File = file;
                Line = line;
                Msg = msg;
            }
            public MessageCategory Category;
            public string File;
            public int Line;
            public string Msg;
        }

        #endregion
    }
}
