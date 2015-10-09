using System;
using System.Collections.Generic;

namespace App
{
    class ErrorCollector
    {
        private List<string> callstack = new List<string>();
        private List<string> messages = new List<string>();
        private string callstackstring
        {
            get
            {
                string str = "";
                foreach (var s in callstack)
                    str += s + ": ";
                return str;
            }
        }

        public void Add(string message)
        {
            messages.Add(callstackstring + message);
        }

        public void Throw(string message)
        {
            throw new GLException(callstackstring + message);
        }

        public void ThrowExeption()
        {
            string str = "";
            foreach (var msg in messages)
                str += msg + '\n';
            throw new GLException(str);
        }

        public void PushStack(string text)
        {
            callstack.Add(text);
        }

        public void PopStack()
        {
            if (callstack.Count > 0)
                callstack.RemoveAt(callstack.Count - 1);
        }

        public bool HasErrors()
        {
            return messages.Count > 0;
        }
    }
}
