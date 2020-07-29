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
using Marathon.Dialogs;
using Marathon.Helpers;
using Marathon.Controls;
using System.Windows.Forms;

namespace Marathon
{
    public partial class Toolkit : Form
    {
        public Toolkit()
        {
            InitializeComponent();
            Text = Program.GetExtendedWindowInformation(Text);
        }

        /// <summary>
        /// Exits the application upon clicking.
        /// </summary>
        private void MenuStripDark_Main_File_Exit_Click(object sender, EventArgs e) => Application.Exit();

        /// <summary>
        /// Displays the About form upon clicking.
        /// </summary>
        private void MenuStripDark_Main_Help_About_Click(object sender, EventArgs e) => new About().ShowDialog();

        /// <summary>
        /// Prompts the user for a file...
        /// </summary>
        private void MenuStripDark_Main_File_OpenFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog {
                Title = "Please select a file...",
                Filter = XML.ParseFileTypesAsFilter(Properties.Resources.AllFileTypes),
                InitialDirectory = ActiveWebBrowserExplorerAddress()
            };

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                ArchiveExplorer listViewExplorer = new ArchiveExplorer { CurrentArchive = fileDialog.FileName };
                listViewExplorer.Show(DockPanel_Main);
            }
        }

        /// <summary>
        /// Prompts the user for a folder...
        /// </summary>
        private void MenuStripDark_Main_File_OpenFolder_Click(object sender, EventArgs e)
        {
            OpenFolderDialog folderDialog = new OpenFolderDialog {
                Title = "Please select a folder..."
            };

            if (folderDialog.ShowDialog() == DialogResult.OK)
            {
                MarathonExplorer webBrowserExplorer = new MarathonExplorer { CurrentAddress = folderDialog.SelectedPath };
                webBrowserExplorer.Show(DockPanel_Main);
            }
        }

        /// <summary>
        /// A function executed whenever the user selects a different child window.
        /// </summary>
        private void DockPanel_Main_ActiveDocumentChanged(object sender, EventArgs e)
        {
        
        }

        /// <summary>
        /// Returns the current address of the active WebBrowserExplorer document.
        /// </summary>
        private string ActiveWebBrowserExplorerAddress()
        {
            if (DockPanel_Main.ActiveDocument != null && DockPanel_Main.ActiveDocument.GetType().Equals(typeof(MarathonExplorer)))
            {
                object @controller = (MarathonExplorer)DockPanel_Main.ActiveDocument;

                return @controller != null ? ((MarathonExplorer)@controller).CurrentAddress : string.Empty;
            }

            return string.Empty;
        }
    }
}