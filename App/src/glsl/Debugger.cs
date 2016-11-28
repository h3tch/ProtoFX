using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace App.Glsl
{
    public enum TraceInfoType
    {
        Variable,
        Function,
        Exception
    }

    public class Debugger
    {
        internal static int ShaderLineOffset = 0;
        internal static bool CollectDebugData = false;
        internal static List<TraceInfo> TraceLog = new List<TraceInfo>();
        
        #region Debug Trace

        /// <summary>
        /// Return list of debug trace information.
        /// </summary>
        public static IEnumerable<TraceInfo> DebugTrace => TraceLog;

        /// <summary>
        /// Clear debug trace.
        /// </summary>
        public static void ClearDebugTrace() => TraceLog.Clear();

        /// <summary>
        /// Begin tracing debug information.
        /// </summary>
        public static void BeginTracing(int LineInFile)
        {
            CollectDebugData = true;
            ShaderLineOffset = LineInFile;
        }

        /// <summary>
        /// Stop tracting debug information.
        /// </summary>
        public static void EndTracing()
        {
            CollectDebugData = false;
            ShaderLineOffset = 0;
        }
        
        /// <summary>
        /// Generate debug trace for an exception.
        /// </summary>
        /// <param name="ex"></param>
        public static void TraceExeption(Exception ex)
        {
            if (!CollectDebugData)
                return;

            var trace = new StackTrace(ex, true);
            var info = new TraceInfo
            {
                Line = trace.GetFrame(0).GetFileLineNumber() + ShaderLineOffset,
                Column = trace.GetFrame(0).GetFileColumnNumber(),
                Type = TraceInfoType.Exception,
                Name = ex.GetType().Name,
                Output = ex.Message,
                Input = null,
            };

            TraceLog.Add(info);

            Debug.Print(ex.GetType().Name + ": " + ex.Message + '\n' + ex.StackTrace);
        }

        /// <summary>
        /// Generate debug trace for variables or functions.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type"></param>
        /// <param name="name"></param>
        /// <param name="column"></param>
        /// <param name="output"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        internal static T Trace<T>(TraceInfoType type, int column, int length, string name, T output, params object[] input)
        {
            if (!CollectDebugData)
                return output;

            var trace = new StackTrace(true);
            var traceFunc = "Trace" + type.ToString();
            var idx = trace.GetFrames().IndexOf(x => x.GetMethod()?.Name == traceFunc);
            var frame = trace.GetFrame(idx + (name == null ? 2 : 1));

            TraceLog.Add(new TraceInfo
            {
                Line = frame.GetFileLineNumber() + ShaderLineOffset,
                Column = column,
                Length = length,
                Type = type,
                Name = name == null ? trace.GetFrame(idx + 1).GetMethod().Name : name,
                Output = output,
                Input = input,
            });

            return output;
        }

        #endregion
    }

    public struct TraceInfo
    {
        public int Line;
        public int Column;
        public int Length;
        public TraceInfoType Type;
        public string Name;
        public object Output;
        public object[] Input;

        public override string ToString()
        {
            switch (Type)
            {
                case TraceInfoType.Variable:
                case TraceInfoType.Exception:
                    return "[L" + Line + ", C" + Column + "] " + Name + ": " + Output.ToString();
                case TraceInfoType.Function:
                    return "[L" + Line + ", C" + Column + "] " + FunctionName;
            }
            return "[L" + Line + ", C" + Column + "] " + Name + ": "
                + Output?.ToString() ?? string.Empty + " = ("
                + Input?.Select(x => x?.ToString() ?? string.Empty).Cat(", ") ?? string.Empty
                + ")";
        }

        private string FunctionName
        {
            get
            {
                var Out = Output.ToString() + " = ";
                switch (Name)
                {
                    case "op_Addition":
                        return Out + Input[0].ToString() + " + " + Input[1].ToString();
                    case "op_Substraction":
                        return Out + Input[0].ToString() + " + " + Input[1].ToString();
                    case "op_Multiply":
                        return Out + Input[0].ToString() + " / " + Input[1].ToString();
                    case "op_Division":
                        return Out + Input[0].ToString() + " / " + Input[1].ToString();
                }
                return Out + Name + "(" + Input.Select(x => x.ToString()).Cat(", ") + ")";
            }
        }
    }

}
