using System;
using Microsoft.Win32;
using System.Reflection;
using System.Diagnostics;
using System.Windows.Forms;

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
            Application.Run(new Toolkit());
        }
    }
}
