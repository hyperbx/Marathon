// OpenFolderDialog.cs is licensed under the MIT License:
/* 
 * MIT License
 * 
 * Copyright (c) 2020 thesupersonic16
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
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Marathon.Components
{
    public class OpenFolderDialog
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        protected static extern IntPtr GetActiveWindow();

        public string Title { get; set; }

        public string SelectedPath { get; private set; }

        public string InitialDirectory { get; private set; }

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