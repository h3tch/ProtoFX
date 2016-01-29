﻿using System;
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

        public CompileException Add(string message, Compiler.Block block)
            => Add(message, block.File, block.LineInFile, block.PositionInFile);

        public CompileException Add(string message, Compiler.Command cmd)
            => Add(message, cmd.File, cmd.LineInFile, cmd.PositionInFile);

        public CompileException Add(string message, string file, int line, int pos)
        {
            messages.Add(new Err(file, line, pos, callstackstring + message));
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
            public Err(string file, int line, int pos, string msg)
            {
                File = file;
                Line = line;
                Pos = pos;
                Msg = msg;
            }
            public string File;
            public int Line;
            public int Pos;
            public string Msg;
        }
        #endregion
    }
}
