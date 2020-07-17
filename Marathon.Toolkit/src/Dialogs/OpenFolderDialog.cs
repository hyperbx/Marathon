using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Marathon.Dialogs
{
    public class OpenFolderDialog
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        protected static extern IntPtr GetActiveWindow();

        public string Title { get; set; }

        public string SelectedPath { get; private set; }

        public DialogResult ShowDialog()
        {
            FileOpenDialog dialog = new FileOpenDialog();
            dialog.SetTitle(Title);
            dialog.SetOptions(FOS.FOS_PICKFOLDERS | FOS.FOS_FORCEFILESYSTEM | FOS.FOS_FILEMUSTEXIST);
            HRESULT res = (HRESULT)dialog.Show(GetActiveWindow());

            if (res == HRESULT.S_OK)
            {
                dialog.GetResult(out var item);
                item.GetDisplayName(SIGDN.SIGDN_FILESYSPATH, out var sel);
                SelectedPath = sel;
                return DialogResult.OK;
            }

            return DialogResult.None;
        }
    }
}