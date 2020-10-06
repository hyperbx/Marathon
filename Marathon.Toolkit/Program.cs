// Marathon is licensed under the MIT License:
/* 
 * MIT License
 * 
 * Copyright (c) 2020 HyperBE32
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
using System.Diagnostics;
using System.Globalization;
using System.Windows.Forms;
using System.Security.Principal;
using System.Collections.Generic;
using Marathon.Toolkit.Forms;
using Marathon.Toolkit.Helpers;

namespace Marathon.Toolkit
{
    static class Program
    {
        public static readonly string GlobalVersion = Application.ProductVersion;

        public static Dictionary<string, string> FileTypes = new Dictionary<string, string>();

        [STAThread]

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            // Force culture info 'en-GB' to prevent errors with values altered by language-specific differences.
            CultureInfo.DefaultThreadCurrentCulture = CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("en-GB");

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
#if !DEBUG
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.ThreadException += (sender, e) => new ErrorHandler(e.Exception).ShowDialog();
#endif
            Application.Run(new Workspace());

            // Parse the file types into the void so we can populate the dictionary.
            XMLHelper.ParseFileTypesToFilter(Properties.Resources.FileTypes);
        }

        /// <summary>
        /// Gets the version string for the title of the window based on flags.
        /// </summary>
        public static string GetExtendedInformation(string text = "")
        {
            text += $" (Version {GlobalVersion})";
#if DEBUG
            text += $" [Debug]";
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

        /// <summary>
        /// Redirects the user to the GitHub issues page.
        /// </summary>
        /// <param name="title">Title used for the issue.</param>
        /// <param name="body">Text automatically added to the issue.</param>
        public static void InvokeFeedback(string title = "[Marathon.Toolkit]", string body = "", string labels = "")
        {
            // This doesn't look elegant, but it's the quickest way of doing it.
            Process.Start(Properties.Resources.URL_GitHubIssueNew +
                          Uri.EscapeUriString("?title=" + (string.IsNullOrEmpty(title) ? "[Marathon.Toolkit]" : title) + " ") + // Issue Title
                          (string.IsNullOrEmpty(body) ? string.Empty : $"&body={Uri.EscapeDataString(body)}") +                 // Issue Body
                          Uri.EscapeUriString("&labels=" + (string.IsNullOrEmpty(labels) ? string.Empty : labels)));            // Issue Labels
        }
    }
}