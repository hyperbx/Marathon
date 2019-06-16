using System;
using System.IO;
using System.Windows.Forms;

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
                    if (!File.Exists(Path.GetDirectoryName(Application.ExecutablePath) + @"\HedgeLib.dll"))
                    {
                        try
                        {
                            File.WriteAllBytes(Path.GetDirectoryName(Application.ExecutablePath) + @"\HedgeLib.dll", Properties.Resources.HedgeLib);
                            MessageBox.Show("HedgeLib.dll was written to the application path.", "Sonic '06 Toolkit", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Application.Restart();
                        }
                        catch { MessageBox.Show("Failed to write HedgeLib.dll. You need to reinstall Sonic '06 Toolkit.", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                    }
                    else { MessageBox.Show("A problem has been detected and Sonic '06 Toolkit has been closed to prevent nothing from happening to your computer.\n\nThe problem seems to be caused by the following file: " + Path.GetFileName(Application.ExecutablePath) + "\n\nSTUPID_ERROR\n\nIf this is the first time you've seen this Stop error screen, restart Sonic '06 Toolkit. If this screen appears again, follow these steps:\n\nCheck to be sure you have Windows installed. If .NET Framework 4.6 is not installed, please install it along with Visual C++ 2010 redistributables and Java.\n\nCheck via GitHub or GameBanana for any Sonic '06 Toolkit updates. Delete the Hyper_Development_Team folder from your Local Application Data to soft reset all binaries and settings. If you need to use a virtual machine, be my guest.\n\nTechnical information:\n\n*** STOP: 0x00000118 (0x0000000000000118, 0x0000000000000118, 0x0000000000000118, 0x0000000000000118)\n\n*** " + Path.GetFileName(Application.ExecutablePath) + " - Address 0x0000000000000118 base at 0x0000000000000118 DateStamp 0x4fa390f3", "STUPID_ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
            }
        }
    }
}
