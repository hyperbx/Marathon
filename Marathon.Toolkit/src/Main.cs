using System;
using Marathon.Dialogs;
using Marathon.Controls;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace Marathon
{
    public partial class Toolkit : Form
    {
        public Toolkit()
        {
            InitializeComponent();

            Text += $"(Version {Program.GlobalVersion})";
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
                Filter = Properties.Resources.Filter_CompressedU8Archive
            };

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                throw new NotImplementedException();
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
                UserControlForm webBrowserExplorerChild = new UserControlForm
                {
                    Controller = new WebBrowserExplorer
                    {
                        Name = "Marathon Explorer",
                        CurrentAddress = folderDialog.SelectedPath
                    }
                };
                webBrowserExplorerChild.Show(DockPanel_Main);
            }
        }

        /// <summary>
        /// A function executed whenever the user selects a different child window.
        /// </summary>
        private void DockPanel_Main_ActiveDocumentChanged(object sender, EventArgs e) { }
    }
}