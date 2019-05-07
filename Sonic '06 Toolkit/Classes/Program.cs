using System;
using System.IO;
using System.Windows.Forms;

namespace Sonic_06_Toolkit
{
    static class Program
    {
        [STAThread]

        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            try
            {
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
                else { MessageBox.Show("Unable to initialise Sonic '06 Toolkit.", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }
    }
}
