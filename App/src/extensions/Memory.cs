using System;
using System.Runtime.InteropServices;

namespace App.Extensions
{
    public static class MemoryExtensions
    {
        /// <summary>
        /// Copy the data from one memory position to another.
        /// </summary>
        /// <param name="src"></param>
        /// <param name="dst"></param>
        /// <param name="size"></param>
        public static void CopyTo(this IntPtr src, IntPtr dst, int size)
        {
            NativeMethods.CopyMemory(dst, src, (uint)size);
        }

        internal static class NativeMethods
        {
            [DllImport("kernel32.dll", EntryPoint = "CopyMemory", SetLastError = false)]
            public static extern void CopyMemory(IntPtr dst, IntPtr src, uint count);
        }
    }
}
