using System;
using System.Collections.Generic;

namespace App
{
    class ErrorCollector : Exception
    {
        private Stack<string> callstack = new Stack<string>();
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
            throw new Exception(callstackstring + message);
        }

        public void ThrowExeption()
        {
            string str = "";
            foreach (var msg in messages)
                str += msg + '\n';
            throw new Exception(str);
        }

        public void PushStack(string text)
        {
            callstack.Push(text);
        }

        public void PopStack()
        {
            callstack.Pop();
        }

        public bool HasErrors()
        {
            return messages.Count > 0;
        }
    }
}
