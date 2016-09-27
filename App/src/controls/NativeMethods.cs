/*
 * This code is provided under the Code Project Open Licence (CPOL)
 * See http://www.codeproject.com/info/cpol10.aspx for details
*/

using System.Drawing;
using System.Reflection;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
    /// <summary>
    /// Description of NativeMethods.
    /// </summary>
    //[SecurityPermission(SecurityAction.Assert, Flags=SecurityPermissionFlag.UnmanagedCode)]
    internal sealed class NativeMethods
    {
        private NativeMethods() {}
        
        #region Windows Constants

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        public const int WM_GETTABRECT = 0x130a;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        public const int WS_EX_TRANSPARENT = 0x20;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        public const int WM_SETFONT = 0x30;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        public const int WM_FONTCHANGE = 0x1d;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        public const int WM_HSCROLL = 0x114;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        public const int TCM_HITTEST = 0x130D;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        public const int WM_PAINT = 0xf;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        public const int WS_EX_LAYOUTRTL  = 0x400000;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        public const int WS_EX_NOINHERITLAYOUT = 0x100000;


        #endregion

        #region Content Alignment

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        public static readonly ContentAlignment AnyRightAlign = ContentAlignment.BottomRight | ContentAlignment.MiddleRight | ContentAlignment.TopRight;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        public static readonly ContentAlignment AnyLeftAlign = ContentAlignment.BottomLeft | ContentAlignment.MiddleLeft | ContentAlignment.TopLeft;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        public static readonly ContentAlignment AnyTopAlign = ContentAlignment.TopRight | ContentAlignment.TopCenter | ContentAlignment.TopLeft;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        public static readonly ContentAlignment AnyBottomAlign = ContentAlignment.BottomRight | ContentAlignment.BottomCenter | ContentAlignment.BottomLeft;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        public static readonly ContentAlignment AnyMiddleAlign = ContentAlignment.MiddleRight | ContentAlignment.MiddleCenter | ContentAlignment.MiddleLeft;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        public static readonly ContentAlignment AnyCenterAlign = ContentAlignment.BottomCenter | ContentAlignment.MiddleCenter | ContentAlignment.TopCenter;

        #endregion

        #region User32.dll
        
        public static IntPtr SendMessage (IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam)
        {
            //	This Method replaces the User32 method SendMessage, but will only work for sending
            //	messages to Managed controls.
            var control = Control.FromHandle(hWnd);
            if (control == null)
                return IntPtr.Zero;
            
            var message = new Message();
            message.HWnd = hWnd;
            message.LParam = lParam;
            message.WParam = wParam;
            message.Msg = msg;
            
            var wproc = control.GetType().GetMethod("WndProc",
                BindingFlags.NonPublic | BindingFlags.InvokeMethod |
                BindingFlags.FlattenHierarchy | BindingFlags.IgnoreCase |
                BindingFlags.Instance);
            
            var args = new object[] {message};
            wproc.Invoke(control, args);
            
            return ((Message)args[0]).Result;
        }

        #endregion

        #region Misc Functions

        public static int LoWord(IntPtr dWord) => dWord.ToInt32() & 0xffff;

        public static int HiWord(IntPtr dWord)
        {
            return (dWord.ToInt32() & 0x80000000) == 0x80000000
                ? dWord.ToInt32() >> 16 : (dWord.ToInt32() >> 16) & 0xffff;
        }
 
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2106:SecureAsserts")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2122:DoNotIndirectlyExposeMethodsWithLinkDemands")]
        public static IntPtr ToIntPtr(object structure)
        {
            IntPtr lparam = IntPtr.Zero;
            lparam = Marshal.AllocCoTaskMem(Marshal.SizeOf(structure));
            Marshal.StructureToPtr(structure, lparam, false);
            return lparam;
        }
        
        #endregion

        #region Windows Structures and Enums

        [Flags()]
        public enum TCHITTESTFLAGS
        {
            TCHT_NOWHERE = 1,
            TCHT_ONITEMICON = 2,
            TCHT_ONITEMLABEL = 4,
            TCHT_ONITEM = TCHT_ONITEMICON | TCHT_ONITEMLABEL
        }
    
        [StructLayout(LayoutKind.Sequential)]
        public struct TCHITTESTINFO
        {
            public TCHITTESTINFO(Drawing.Point location)
            {
                pt = location;
                flags = TCHITTESTFLAGS.TCHT_ONITEM;
            }
            
            public Drawing.Point pt;
            public TCHITTESTFLAGS flags;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        public struct PAINTSTRUCT
        {
            public IntPtr hdc;
            public int fErase;
            public RECT rcPaint;
            public int fRestore;
            public int fIncUpdate;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst=32)] 
            public byte[] rgbReserved;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
            
            public RECT(int left, int top, int right, int bottom)
            {
                this.left = left;
                this.top = top;
                this.right = right;
                this.bottom = bottom;
            }
        
            public RECT(Rectangle r)
            {
                left = r.Left;
                top = r.Top;
                right = r.Right;
                bottom = r.Bottom;
            }
        
            public static RECT FromXYWH(int x, int y, int width, int height)
                => new RECT(x, y, x + width, y + height);
        
            public static RECT FromIntPtr(IntPtr ptr) 
                => (RECT)Marshal.PtrToStructure(ptr, typeof(RECT));
            
            public Size Size => new Size(right - left, bottom - top);
        }

        #endregion
    }
}
