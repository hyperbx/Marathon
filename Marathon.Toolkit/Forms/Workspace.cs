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
using System.Windows.Forms;
using Marathon.Toolkit.Helpers;
using Marathon.Toolkit.Dialogs;

namespace Marathon.Toolkit.Forms
{
    public partial class Workspace : Form
    {
        public Workspace()
        {
            InitializeComponent();

            Text = Program.GetExtendedInformation(Text);
#if DEBUG
            // Display debug option...
            MenuStripDark_Main_Debug.Visible = true;
            MenuStripDark_Main_Debug.Click += delegate { new Debugger().Show(DockPanel_Main); };
#endif
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
        private void MenuStripDark_Main_Open_File_Click(object sender, EventArgs e)
        {
            string commonFileTypesXML = Properties.Resources.FileTypes;

            OpenFileDialog fileDialog = new OpenFileDialog
            {
                Title = "Please select a file...",
                Filter = XML.ParseFileTypesToFilter(commonFileTypesXML),
                InitialDirectory = ActiveMarathonExplorerAddress()
            };

            if (fileDialog.ShowDialog() == DialogResult.OK)
                new TaskDashboard(DockPanel_Main, fileDialog.FileName).ShowDialog();
        }

        /// <summary>
        /// Prompts the user for a folder...
        /// </summary>
        private void MenuStripDark_Main_Open_Folder_Click(object sender, EventArgs e)
        {
            OpenFolderDialog folderDialog = new OpenFolderDialog
            {
                Title = "Please select a folder..."
            };

            if (folderDialog.ShowDialog() == DialogResult.OK)
                new MarathonExplorer { CurrentAddress = folderDialog.SelectedPath }.Show(DockPanel_Main);
        }

        /// <summary>
        /// Returns the current address of the active MarathonExplorer document.
        /// </summary>
        private string ActiveMarathonExplorerAddress()
        {
            if (DockPanel_Main.ActiveDocument != null && DockPanel_Main.ActiveDocument is MarathonExplorer explorer)
            {
                object @controller = explorer;

                return @controller != null ? ((MarathonExplorer)@controller).CurrentAddress : string.Empty;
            }

            return string.Empty;
        }

        /// <summary>
        /// Displays the MarathonLog form upon clicking.
        /// </summary>
        private void MenuStripDark_Main_View_Output_Click(object sender, EventArgs e) => new Output().Show(DockPanel_Main);

        /// <summary>
        /// Displays the Windows form upon clicking.
        /// </summary>
        private void MenuStripDark_Main_Window_Windows_Click(object sender, EventArgs e) => new Windows().ShowDialog();

        /// <summary>
        /// Displays the Options form upon clicking.
        /// </summary>
        private void MenuStripDark_Main_File_Options_Click(object sender, EventArgs e) => new Options().Show(DockPanel_Main);

        /// <summary>
        /// Displays the File Converter form upon clicking.
        /// </summary>
        private void MenuStripDark_Main_View_FileConverter_Click(object sender, EventArgs e) => new FileConverter().Show(DockPanel_Main);

        /// <summary>
        /// Redirects the user to the GitHub issues page.
        /// </summary>
        private void MenuStripDark_Main_Help_SendFeedback_Click_Group(object sender, EventArgs e)
        {
            string commonTitle = "[Marathon.Toolkit]";

            // Bug report...
            if (sender.Equals(MenuStripDark_Main_Help_SendFeedback_ReportAProblem))
                Program.InvokeFeedback(commonTitle, string.Empty, "bug");

            // Feature request...
            else if (sender.Equals(MenuStripDark_Main_Help_SendFeedback_SuggestAFeature))
                Program.InvokeFeedback(commonTitle, string.Empty, "enhancement");
        }
    }
}