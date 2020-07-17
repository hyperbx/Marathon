using System;
using Microsoft.Win32;
using System.Reflection;
using System.Diagnostics;
using System.Windows.Forms;
using System.Security.Principal;

namespace Marathon
{
    static class Program
    {
        public static readonly string GlobalVersion = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).FileVersion;
        public static readonly int RegistryColour = (int)Registry.GetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationColor", null);

        [STAThread]

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
#if !DEBUG
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.ThreadException += (sender, e) => new MarathonErrorHandler(e.Exception).ShowDialog();
#endif
            Application.Run(new Toolkit());
        }

        /// <summary>
        /// Returns the process architecture.
        /// </summary>
        public static string Architecture()
            => Environment.Is64BitProcess ? "x64" : "x86";

        /// <summary>
        /// Returns the process role state.
        /// </summary>
        public static bool RunningAsAdmin()
            => new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator);
    }
}