using System;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;
using System.Security.Principal;

// Sonic '06 Toolkit is licensed under the MIT License:
/*
 * MIT License

 * Copyright (c) 2019 Gabriel (HyperPolygon64)

 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:

 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.

 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 */

namespace Sonic_06_Toolkit
{
    static class Program
    {
        [STAThread]

        public static void Main(string[] args)
        {
            WritePrerequisites();

            var generateSessionID = new Random();
            Tools.Global.sessionID = generateSessionID.Next(1, 99999); //Generates a random number between 1 to 99999 for a unique Session ID.

            if (Properties.Settings.Default.skipWorkaround == true)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Main(args));
            }
            else
            {
                try
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new Main(args));
                }
                catch
                {
                    WritePrerequisites();
                }
            }
        }

        public static bool RunningAsAdmin()
        {
            return new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator);
        }

        public static void WritePrerequisites()
        {
            if (!File.Exists(Path.Combine(Application.StartupPath, "HedgeLib.dll")))
            {
                try
                {
                    File.WriteAllBytes(Path.Combine(Application.StartupPath, "HedgeLib.dll"), Properties.Resources.HedgeLib);
                    MessageBox.Show("HedgeLib.dll was written to the application path.", "Sonic '06 Toolkit", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex) { MessageBox.Show($"Failed to write HedgeLib.dll. Please reinstall Sonic '06 Toolkit.\n\n{ex}", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error); Application.Exit(); }
            }

            if (!File.Exists(Path.Combine(Application.StartupPath, "Ookii.Dialogs.dll")))
            {
                try
                {
                    File.WriteAllBytes(Path.Combine(Application.StartupPath, "Ookii.Dialogs.dll"), Properties.Resources.Ookii_Dialogs);
                    MessageBox.Show("Ookii.Dialogs.dll was written to the application path.", "Sonic '06 Toolkit", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex) { MessageBox.Show($"Failed to write Ookii.Dialogs.dll. Please reinstall Sonic '06 Toolkit.\n\n{ex}", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error); Application.Exit(); }
            }

            Application.Exit();
        }
    }
}
