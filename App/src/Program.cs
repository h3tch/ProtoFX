using System;
using System.Windows.Forms;

namespace App
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (Environment.OSVersion.Version.Major >= 6)
                NativeMethods.SetProcessDPIAware();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new App());
        }
        
        internal static class NativeMethods
        {
            [System.Runtime.InteropServices.DllImport("user32.dll")]
            public static extern bool SetProcessDPIAware();
        }
    }
}
