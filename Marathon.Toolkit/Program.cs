// Marathon is licensed under the MIT License:
/* 
 * MIT License
 * 
 * Copyright (c) 2020 HyperPolygon64
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 */

using System;
using System.Reflection;
using System.Diagnostics;
using System.Windows.Forms;
using System.Security.Principal;
using Microsoft.Win32;
using Marathon.Toolkit.Forms;

namespace Marathon.Toolkit
{
    static class Program
    {
        public static readonly string GlobalVersion = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).ProductVersion;
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
            Application.ThreadException += (sender, e) => new ErrorHandler(e.Exception).ShowDialog();
#endif

            Settings.Load();

            Application.Run(new Workspace());
        }

        /// <summary>
        /// Gets the version string for the title of the window based on flags.
        /// </summary>
        public static string GetExtendedInformation(string text = "")
        {
#if !DEBUG
            text += $" (Version {GlobalVersion})";
#else
            text += $" (Version {GlobalVersion}) [Debug]";
#endif
            text += RunningAsAdmin() ? " <Administrator>" : string.Empty;

            return text;
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