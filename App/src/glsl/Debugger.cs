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
    
    /// <summary>
    /// GLSL debug helper class for tracing
    /// and retrieving debug information.
    /// </summary>
    public static class Debugger
    {
        private static int ShaderLineOffset = 0;
        private static bool CollectDebugData = false;
        private static List<TraceInfo> TraceLog = new List<TraceInfo>();
        
        #region Debug Trace

        /// <summary>
        /// Return list of debug trace information.
        /// </summary>
        public static IEnumerable<TraceInfo> DebugTrace => TraceLog;

        /// <summary>
        /// Clear debug trace.
        /// </summary>
        public static void ClearDebugTrace()
        {
            TraceLog.Clear();
        }

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
        /// Get debug information for the specified position (if there is any).
        /// </summary>
        /// <param name="line"></param>
        /// <param name="column"></param>
        /// <param name="startInfo"></param>
        /// <returns></returns>
        public static TraceInfo? GetTraceInfo(int line, int column, int startInfo = 0)
        {
            if (TraceLog.Count == 0)
                return null;

            int i = Math.Min(Math.Max(0, startInfo), TraceLog.Count);

            var log = line < TraceLog[i].Location.Line
                || (line == TraceLog[i].Location.Line && column < TraceLog[i].Location.Column)
                ? TraceLog.Take(i).Reverse()
                : TraceLog.Skip(Math.Max(i - 1, 0));
            
            foreach (var info in log)
            {
                var l = info.Location;
                if (l.Line == line && l.Column <= column && column <= l.EndColumn)
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

    /// <summary>
    /// Structure to identify a region in the code.
    /// </summary>
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

    /// <summary>
    /// Structure to trace debug information.
    /// </summary>
    public struct TraceInfo
    {
        public Location Location;
        public TraceInfoType Type;
        public string Name;
        public object Output;
        public Array OutputArray
        {
            get
            {
                if (Output == null)
                    return null;
                var toArray = Output.GetType().GetMethod("ToArray");
                if (toArray != null)
                    return (Array)toArray.Invoke(Output, null);
                var array = Array.CreateInstance(Output.GetType(), 1);
                array.SetValue(Output, 0);
                return array;
            }
        }
            

        #region Format Output

        private static Func<object, string> FuncFormatMat = 
            (obj) => '\n' + obj.ToString().Replace("; "," \n");

        private static Dictionary<Type,Func<object, string>> FormatSwitch =
            new Dictionary<Type,Func<object, string>>
            {
                { typeof(mat2), FuncFormatMat },
                { typeof(mat3), FuncFormatMat },
                { typeof(mat4), FuncFormatMat },
            };

        /// <summary>
        /// Format output to a human readable format.
        /// </summary>
        private string FormatedOutput
            => FormatSwitch.ContainsKey(Output.GetType())
                ? FormatSwitch[Output.GetType()](Output)
                : Output.ToString();

        #endregion

        public override string ToString()
        {
            return $"{Name}: {FormatedOutput}";
        }
    }

}
