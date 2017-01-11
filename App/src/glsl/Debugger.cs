using System;
using System.Collections.Generic;
using System.Diagnostics;

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

        public static TraceInfo? GetTraceInfo(int line, int column)
        {
            foreach (var info in TraceLog)
            {
                if (info.Location.Line == line
                    && info.Location.Column <= column
                    && column <= info.Location.EndColumn)
                    return info;
            }
            return null;
        }

        /// <summary>
        /// Generate debug trace for an exception.
        /// </summary>
        /// <param name="ex"></param>
        public static void TraceExeption(Exception ex)
        {
            if (!CollectDebugData)
                return;

            var location = new Location(-1, -1, 0, ex);
            location.Line += ShaderLineOffset;

            var info = new TraceInfo
            {
                Location = location,
                Type = TraceInfoType.Exception,
                Name = ex.GetType().Name,
                Output = ex.Message,
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
        internal static T Trace<T>(TraceInfoType type, Location location, string name, T output)
        {
            if (!CollectDebugData)
                return output;

            location.Line += ShaderLineOffset;

            TraceLog.Add(new TraceInfo {
                Location = location,
                Type = type,
                Name = name,
                Output = output,
            });

            return output;
        }

        #endregion
    }

    public struct Location
    {
        public int Line;
        public int Column;
        public int Length;
        public int Level;
        public int EndColumn => Column + Length;
        public Location(int line, int column, int length, Exception ex = null)
        {
            var delta = ex != null ? 0 : 1;
            var trace = ex != null ? new StackTrace(ex, true) : new StackTrace(true);
            var frame = trace.GetFrame(delta);
            Column = column < 0 ? frame.GetFileColumnNumber() : column;
            Length = length;
            Level = trace.FrameCount - delta;
            Line = line < 0 ? frame.GetFileLineNumber() : line;
        }
    }

    public struct TraceInfo
    {
        public Location Location;
        public TraceInfoType Type;
        public string Name;
        public object Output;
        public override string ToString()
            => "[L" + Location.Line + ", C" + Location.Column + "] " + Name + ": "
                + Output.ToString();
    }

}
