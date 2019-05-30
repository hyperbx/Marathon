using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Sonic_06_Toolkit
{
    public partial class Paths : Form
    {
        static bool changes = false;

        public Paths()
        {
            InitializeComponent();
        }

        void Paths_Load(object sender, EventArgs e)
        {
            text_RootPath.Text = Properties.Settings.Default.rootPath;
            text_ToolsPath.Text = Properties.Settings.Default.toolsPath;
            text_ArchivesPath.Text = Properties.Settings.Default.archivesPath;
            text_GamePath.Text = Properties.Settings.Default.gamePath;
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

        void Btn_BrowseGame_Click(object sender, EventArgs e)
        {
            fbd_Browse.Description = "Please select the path to your extracted copy of SONIC THE HEDGEHOG.";
            if (fbd_Browse.ShowDialog() == DialogResult.OK) text_GamePath.Text = fbd_Browse.SelectedPath + @"\";
        }
        #endregion

        void Btn_Restore_Click(object sender, EventArgs e)
        {
            text_RootPath.Text = Tools.Global.applicationData + @"\Hyper_Development_Team\Sonic '06 Toolkit\";
            text_ToolsPath.Text = Tools.Global.applicationData + @"\Hyper_Development_Team\Sonic '06 Toolkit\Tools\";
            text_ArchivesPath.Text = Tools.Global.applicationData + @"\Hyper_Development_Team\Sonic '06 Toolkit\Archives\";
            text_GamePath.Text = "";
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
                changes = true;
            }
            else if (text_RootPath.Text == "") MessageBox.Show("Please enter a Root path.", "Path Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                try
                {
                    Directory.CreateDirectory(text_RootPath.Text);
                    if (text_RootPath.Text.EndsWith(@"\")) Properties.Settings.Default.rootPath = text_RootPath.Text; else Properties.Settings.Default.rootPath = text_RootPath.Text + @"\";
                    Properties.Settings.Default.Save();
                    changes = true;
                }
                catch { MessageBox.Show("An error occurred when creating the Root directory.", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }

            if (Directory.Exists(text_ToolsPath.Text))
            {
                if (text_ToolsPath.Text.EndsWith(@"\")) Properties.Settings.Default.toolsPath = text_ToolsPath.Text; else Properties.Settings.Default.toolsPath = text_ToolsPath.Text + @"\";
                Properties.Settings.Default.Save();
                changes = true;
            }
            else if (text_ToolsPath.Text == "") MessageBox.Show("Please enter a Tools path.", "Path Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                try
                {
                    Directory.CreateDirectory(text_ToolsPath.Text);
                    if (text_ToolsPath.Text.EndsWith(@"\")) Properties.Settings.Default.toolsPath = text_ToolsPath.Text; else Properties.Settings.Default.toolsPath = text_ToolsPath.Text + @"\";
                    Properties.Settings.Default.Save();
                    changes = true;
                }
                catch { MessageBox.Show("An error occurred when creating the Tools directory.", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }

            if (Directory.Exists(text_ArchivesPath.Text))
            {
                if (text_ArchivesPath.Text.EndsWith(@"\")) Properties.Settings.Default.archivesPath = text_ArchivesPath.Text; else Properties.Settings.Default.archivesPath = text_ArchivesPath.Text + @"\";
                Properties.Settings.Default.Save();
                changes = true;
            }
            else if (text_ArchivesPath.Text == "") MessageBox.Show("Please enter an Archives path.", "Path Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                try
                {
                    Directory.CreateDirectory(text_ArchivesPath.Text);
                    if (text_ArchivesPath.Text.EndsWith(@"\")) Properties.Settings.Default.archivesPath = text_ArchivesPath.Text; else Properties.Settings.Default.archivesPath = text_ArchivesPath.Text + @"\";
                    Properties.Settings.Default.Save();
                    changes = true;
                }
                catch { MessageBox.Show("An error occurred when creating the Archives directory.", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }

            if (Directory.Exists(text_GamePath.Text))
            {
                if (!text_GamePath.Text.EndsWith(@"\")) text_GamePath.Text += @"\";

                #region Xbox 360
                if (File.Exists(text_GamePath.Text + "default.xex"))
                {
                    byte[] bytes = File.ReadAllBytes(text_GamePath.Text + "default.xex").Take(4).ToArray();
                    var hexString = BitConverter.ToString(bytes); hexString = hexString.Replace("-", " ");
                    if (hexString == "58 45 58 32")
                    {
                        Properties.Settings.Default.gamePath = text_GamePath.Text;
                        Properties.Settings.Default.Save();

                        Tools.Global.gameChanged = true;
                    }
                    else { MessageBox.Show("I see you're trying to cheat the system...", "XEX Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
                #endregion

                #region PlayStation 3
                else if (File.Exists(text_GamePath.Text + "PS3_DISC.SFB"))
                {
                    byte[] bytes = File.ReadAllBytes(text_GamePath.Text + "PS3_DISC.SFB").Take(4).ToArray();
                    var hexString = BitConverter.ToString(bytes); hexString = hexString.Replace("-", " ");
                    if (hexString == "2E 53 46 42")
                    {
                        Properties.Settings.Default.gamePath = text_GamePath.Text;
                        Properties.Settings.Default.Save();

                        Tools.Global.gameChanged = true;
                    }
                    else { MessageBox.Show("I see you're trying to cheat the system...", "SFB Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
                else if (File.Exists(text_GamePath.Text + "PARAM.SFO"))
                {
                    byte[] bytes = File.ReadAllBytes(text_GamePath.Text + "PARAM.SFO").Take(4).ToArray();
                    var hexString = BitConverter.ToString(bytes); hexString = hexString.Replace("-", " ");
                    if (hexString == "00 50 53 46")
                    {
                        Properties.Settings.Default.gamePath = text_GamePath.Text;
                        Properties.Settings.Default.Save();

                        Tools.Global.gameChanged = true;
                    }
                    else { MessageBox.Show("I see you're trying to cheat the system...", "SFO Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
                else if (File.Exists(text_GamePath.Text + "EBOOT.BIN"))
                {
                    byte[] bytes = File.ReadAllBytes(text_GamePath.Text + "EBOOT.BIN").Take(3).ToArray();
                    var hexString = BitConverter.ToString(bytes); hexString = hexString.Replace("-", " ");
                    if (hexString == "53 43 45")
                    {
                        Properties.Settings.Default.gamePath = text_GamePath.Text;
                        Properties.Settings.Default.Save();

                        Tools.Global.gameChanged = true;
                    }
                    else { MessageBox.Show("I see you're trying to cheat the system...", "BIN Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
                #endregion

                else { MessageBox.Show("Please select a valid SONIC THE HEDGEHOG directory.", "Path Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
            else if (text_GamePath.Text == "")
            {
                Properties.Settings.Default.gamePath = null;
                Properties.Settings.Default.Save();

                Tools.Global.gameChanged = true;
            }

            Close();
        }

        void Btn_Cancel_Click(object sender, EventArgs e)
        {
            changes = false;
            Close();
        }

        void Paths_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (changes) MessageBox.Show("Please restart Sonic '06 Toolkit to accept any changes.", "Sonic '06 Toolkit", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
