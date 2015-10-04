using System;
using System.Collections.Generic;

namespace App
{
    class ErrorCollector : Exception
    {
        private Stack<string> callstack = new Stack<string>();
        private List<string> messages = new List<string>();

        public void Add(string message)
        {
            string str = "";
            foreach (var s in callstack)
                str += s;
            messages.Add(str + ": " + message);
        }

        public void Throw(string message)
        {
            string str = "";
            foreach (var s in callstack)
                str += s;
            throw new Exception(str + ": " + message);
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
