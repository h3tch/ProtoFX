using System.Drawing;
using System.Reflection;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
    /// <summary>
    /// Description of NativeMethods.
    /// </summary>
    internal sealed class NativeMethods
    {
        #region Windows Constants

        [Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        public const int WM_GETTABRECT = 0x130a;
        [Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        public const int WS_EX_TRANSPARENT = 0x20;
        [Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        public const int WM_SETFONT = 0x30;
        [Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        public const int WM_FONTCHANGE = 0x1d;
        [Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        public const int WM_HSCROLL = 0x114;
        [Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        public const int TCM_HITTEST = 0x130D;
        [Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        public const int WM_PAINT = 0xf;
        [Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        public const int WS_EX_LAYOUTRTL  = 0x400000;
        [Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        public const int WS_EX_NOINHERITLAYOUT = 0x100000;
        
        #endregion

        #region Content Alignment

        [Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        public static readonly ContentAlignment AnyRightAlign = ContentAlignment.BottomRight | ContentAlignment.MiddleRight | ContentAlignment.TopRight;
        [Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        public static readonly ContentAlignment AnyLeftAlign = ContentAlignment.BottomLeft | ContentAlignment.MiddleLeft | ContentAlignment.TopLeft;
        [Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        public static readonly ContentAlignment AnyTopAlign = ContentAlignment.TopRight | ContentAlignment.TopCenter | ContentAlignment.TopLeft;
        [Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        public static readonly ContentAlignment AnyBottomAlign = ContentAlignment.BottomRight | ContentAlignment.BottomCenter | ContentAlignment.BottomLeft;
        [Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        public static readonly ContentAlignment AnyMiddleAlign = ContentAlignment.MiddleRight | ContentAlignment.MiddleCenter | ContentAlignment.MiddleLeft;
        [Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        public static readonly ContentAlignment AnyCenterAlign = ContentAlignment.BottomCenter | ContentAlignment.MiddleCenter | ContentAlignment.TopCenter;

        #endregion

        #region User32.dll
        
        public static IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam)
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

        public static int LoWord(IntPtr dWord)
        {
            return dWord.ToInt32() & 0xffff;
        }

        public static int HiWord(IntPtr dWord)
        {
            return (dWord.ToInt32() & 0x80000000) == 0x80000000
                ? dWord.ToInt32() >> 16 : (dWord.ToInt32() >> 16) & 0xffff;
        }
 
        [Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2106:SecureAsserts")]
        [Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2122:DoNotIndirectlyExposeMethodsWithLinkDemands")]
        public static IntPtr ToIntPtr(object structure)
        {
            var lparam = IntPtr.Zero;
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

        #endregion
    }
}
