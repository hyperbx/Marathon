using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace Sonic_06_Toolkit
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        void File_Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        void Help_About_Click(object sender, EventArgs e)
        {
            new About().ShowDialog();
        }

        void File_OpenARC_Click(object sender, EventArgs e)
        {
            if (ofd_OpenARC.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string failsafeCheck = Path.GetRandomFileName();

                    #region Building unpack data...
                    var unpackBuildSession = new StringBuilder();
                    unpackBuildSession.Append(Global.archivesPath);
                    unpackBuildSession.Append(Global.sessionID);
                    unpackBuildSession.Append(@"\");
                    unpackBuildSession.Append(failsafeCheck);
                    unpackBuildSession.Append(@"\");
                    unpackBuildSession.Append(Path.GetFileNameWithoutExtension(ofd_OpenARC.FileName));
                    unpackBuildSession.Append(@"\");
                    if (!Directory.Exists(unpackBuildSession.ToString())) Directory.CreateDirectory(unpackBuildSession.ToString());
                    #endregion

                    #region Building ARC data...
                    var arcBuildSession = new StringBuilder();
                    arcBuildSession.Append(Global.archivesPath);
                    arcBuildSession.Append(Global.sessionID);
                    arcBuildSession.Append(@"\");
                    arcBuildSession.Append(failsafeCheck);
                    arcBuildSession.Append(@"\");
                    if (!Directory.Exists(arcBuildSession.ToString())) Directory.CreateDirectory(arcBuildSession.ToString());
                    if (File.Exists(ofd_OpenARC.FileName)) File.Copy(ofd_OpenARC.FileName, arcBuildSession.ToString() + Path.GetFileName(ofd_OpenARC.FileName), true);
                    #endregion

                    #region Unpacking ARC...
                    var basicWrite = File.Create(Global.toolsPath + "unpack.bat");
                    var basicSession = new UTF8Encoding(true).GetBytes("\"" + Global.unpackFile + "\" \"" + arcBuildSession.ToString() + Path.GetFileName(ofd_OpenARC.FileName) + "\"");
                    basicWrite.Write(basicSession, 0, basicSession.Length);
                    basicWrite.Close();
                    var unpackSession = new ProcessStartInfo(Global.toolsPath + "unpack.bat");
                    unpackSession.WorkingDirectory = Global.toolsPath;
                    unpackSession.WindowStyle = ProcessWindowStyle.Hidden;
                    var Unpack = Process.Start(unpackSession);
                    var unpackDialog = new Unpacking();
                    var parentLeft = Left + ((Width - unpackDialog.Width) / 2);
                    var parentTop = Top + ((Height - unpackDialog.Height) / 2);
                    unpackDialog.Location = new System.Drawing.Point(parentLeft, parentTop);
                    unpackDialog.Show();
                    Unpack.WaitForExit();
                    Unpack.Close();
                    #endregion

                    #region Writing metadata...
                    var metadataWrite = File.Create(arcBuildSession.ToString() + "metadata.ini");
                    var metadataSession = new UTF8Encoding(true).GetBytes(ofd_OpenARC.FileName);
                    metadataWrite.Write(metadataSession, 0, metadataSession.Length);
                    metadataWrite.Close();
                    unpackDialog.Close();
                    #endregion

                    web_Debug.Navigate(unpackBuildSession.ToString());
                }
                catch
                {
                    MessageBox.Show("An error occurred when unpacking the archive.", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        void Main_Load(object sender, EventArgs e)
        {
            #region Session ID
            var generateSessionID = new Random();
            Global.sessionID = generateSessionID.Next(1, 99999); //Generates a random number between 1 to 99999 for a unique Session ID.
            btn_SessionID.Text = Global.sessionID.ToString();
            #endregion

            #region Directory Check
            try
            {
                //The below code checks if the directories in the Global class exist; if not, they will be created.
                if (!Directory.Exists(Global.tempPath)) Directory.CreateDirectory(Global.tempPath);
                if (!Directory.Exists(Global.archivesPath)) Directory.CreateDirectory(Global.archivesPath);
                if (!Directory.Exists(Global.toolsPath)) Directory.CreateDirectory(Global.toolsPath);
                if (!Directory.Exists(Global.unlubPath)) Directory.CreateDirectory(Global.unlubPath);
                if (!Directory.Exists(Global.xnoPath)) Directory.CreateDirectory(Global.xnoPath);
            }
            catch
            {
                MessageBox.Show("An error occurred when writing a directory.", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
            #endregion

            #region File Check
            try
            {
                //The below code checks if the files in the Global class exist; if not, they will be created.
                if (!File.Exists(Global.unpackFile)) File.WriteAllBytes(Global.unpackFile, Properties.Resources.unpack);
                if (!File.Exists(Global.repackFile)) File.WriteAllBytes(Global.repackFile, Properties.Resources.repack);
                if (!File.Exists(Global.xnoFile)) File.WriteAllBytes(Global.xnoFile, Properties.Resources.xno2dae);
            }
            catch
            {
                MessageBox.Show("An error occurred when writing a file.", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
            #endregion
        }

        private void Btn_Back_Click(object sender, EventArgs e)
        {
            web_Debug.GoBack();
        }

        private void Btn_Forward_Click(object sender, EventArgs e)
        {
            web_Debug.GoForward();
        }
    }
}
