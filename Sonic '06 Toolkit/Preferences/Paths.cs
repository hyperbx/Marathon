using System;
using System.IO;
using System.Windows.Forms;

namespace Sonic_06_Toolkit
{
    public partial class Paths : Form
    {
        public Paths()
        {
            InitializeComponent();
        }

        void Paths_Load(object sender, EventArgs e)
        {
            text_RootPath.Text = Properties.Settings.Default.rootPath;
            text_ToolsPath.Text = Properties.Settings.Default.toolsPath;
            text_ArchivesPath.Text = Properties.Settings.Default.archivesPath;
        }

        #region Browse tasks...
        void Btn_BrowseRoot_Click(object sender, EventArgs e)
        {
            fbd_Browse.Description = "Please select the Root path.";
            if (fbd_Browse.ShowDialog() == DialogResult.OK) text_RootPath.Text = fbd_Browse.SelectedPath + @"\";
            text_ToolsPath.Text = fbd_Browse.SelectedPath + @"\Tools\";
            text_ArchivesPath.Text = fbd_Browse.SelectedPath + @"\Archives\";
        }

        void Btn_BrowseTools_Click(object sender, EventArgs e)
        {
            fbd_Browse.Description = "Please select the Tools path.";
            if (fbd_Browse.ShowDialog() == DialogResult.OK) text_ToolsPath.Text = fbd_Browse.SelectedPath + @"\";
        }

        void Btn_BrowseArchives_Click(object sender, EventArgs e)
        {
            fbd_Browse.Description = "Please select the Archives path.";
            if (fbd_Browse.ShowDialog() == DialogResult.OK) text_ArchivesPath.Text = fbd_Browse.SelectedPath + @"\";
        }
        #endregion

        void Btn_Restore_Click(object sender, EventArgs e)
        {
            text_RootPath.Text = Global.applicationData + @"\Hyper_Development_Team\Sonic '06 Toolkit\";
            text_ToolsPath.Text = Global.applicationData + @"\Hyper_Development_Team\Sonic '06 Toolkit\Tools\";
            text_ArchivesPath.Text = Global.applicationData + @"\Hyper_Development_Team\Sonic '06 Toolkit\Archives\";
        }

        void Btn_AppPath_Click(object sender, EventArgs e)
        {
            text_RootPath.Text = Path.GetDirectoryName(Application.ExecutablePath) + @"\";
            text_ToolsPath.Text = Path.GetDirectoryName(Application.ExecutablePath) + @"\Tools\";
            text_ArchivesPath.Text = Path.GetDirectoryName(Application.ExecutablePath) + @"\Archives\";
        }

        void Btn_Confirm_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(text_RootPath.Text))
            {
                if (text_RootPath.Text.EndsWith(@"\")) Properties.Settings.Default.rootPath = text_RootPath.Text; else Properties.Settings.Default.rootPath = text_RootPath.Text + @"\";
                Properties.Settings.Default.Save();
                Global.pathChange += 1;
                Close();
            }
            else if (text_RootPath.Text == "") MessageBox.Show("Please enter a Root path.", "Path Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                try
                {
                    Directory.CreateDirectory(text_RootPath.Text);
                    if (text_RootPath.Text.EndsWith(@"\")) Properties.Settings.Default.rootPath = text_RootPath.Text; else Properties.Settings.Default.rootPath = text_RootPath.Text + @"\";
                    Properties.Settings.Default.Save();
                    Global.pathChange += 1;
                    Close();
                }
                catch { MessageBox.Show("An error occurred when creating the Root directory.", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }

            if (Directory.Exists(text_ToolsPath.Text))
            {
                if (text_ToolsPath.Text.EndsWith(@"\")) Properties.Settings.Default.toolsPath = text_ToolsPath.Text; else Properties.Settings.Default.toolsPath = text_ToolsPath.Text + @"\";
                Properties.Settings.Default.Save();
                Global.pathChange += 1;
                Close();
            }
            else if (text_ToolsPath.Text == "") MessageBox.Show("Please enter a Tools path.", "Path Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                try
                {
                    Directory.CreateDirectory(text_ToolsPath.Text);
                    if (text_ToolsPath.Text.EndsWith(@"\")) Properties.Settings.Default.toolsPath = text_ToolsPath.Text; else Properties.Settings.Default.toolsPath = text_ToolsPath.Text + @"\";
                    Properties.Settings.Default.Save();
                    Global.pathChange += 1;
                    Close();
                }
                catch { MessageBox.Show("An error occurred when creating the Tools directory.", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }

            if (Directory.Exists(text_ArchivesPath.Text))
            {
                if (text_ArchivesPath.Text.EndsWith(@"\")) Properties.Settings.Default.archivesPath = text_ArchivesPath.Text; else Properties.Settings.Default.archivesPath = text_ArchivesPath.Text + @"\";
                Properties.Settings.Default.Save();
                Global.pathChange += 1;
                Close();
            }
            else if (text_ArchivesPath.Text == "") MessageBox.Show("Please enter an Archives path.", "Path Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                try
                {
                    Directory.CreateDirectory(text_ArchivesPath.Text);
                    if (text_ArchivesPath.Text.EndsWith(@"\")) Properties.Settings.Default.archivesPath = text_ArchivesPath.Text; else Properties.Settings.Default.archivesPath = text_ArchivesPath.Text + @"\";
                    Properties.Settings.Default.Save();
                    Global.pathChange += 1;
                    Close();
                }
                catch { MessageBox.Show("An error occurred when creating the Archives directory.", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
            if (Global.pathChange != 0)
            {
                MessageBox.Show("Please restart Sonic '06 Toolkit to accept your changes.", "Paths Accepted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Global.pathChange = 0;
            }
        }

        void Btn_Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
