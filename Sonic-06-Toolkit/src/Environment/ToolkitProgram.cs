using System;
using Toolkit.Environment4;
using System.Windows.Forms;

namespace Toolkit
{
    static class Program
    {
        [STAThread]

        static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ToolkitEnvironment4());
        }
    }
}
