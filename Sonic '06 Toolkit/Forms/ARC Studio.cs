using System;
using System.IO;
using System.Text;
using System.Diagnostics;
using System.Windows.Forms;

namespace Sonic_06_Toolkit
{
    public partial class ARC_Studio : Form
    {
        public ARC_Studio()
        {
            InitializeComponent();
        }

        void mergeARC()
        {
            try
            {
                #region Building unpack data...
                //Builds the main string which locates the ARC's final unpack directory.
                string failsafeCheck1 = Path.GetRandomFileName(); //Unpacked ARCs will have a unique directory to prevent overwriting.
                var unpackBuildSession1 = new StringBuilder();
                unpackBuildSession1.Append(Properties.Settings.Default.archivesPath);
                unpackBuildSession1.Append(Global.sessionID);
                unpackBuildSession1.Append(@"\");
                unpackBuildSession1.Append(failsafeCheck1);
                unpackBuildSession1.Append(@"\");
                unpackBuildSession1.Append(Path.GetFileNameWithoutExtension(text_ARC1.Text));
                unpackBuildSession1.Append(@"\");
                if (!Directory.Exists(unpackBuildSession1.ToString())) Directory.CreateDirectory(unpackBuildSession1.ToString());
                #endregion

                #region Building ARC data...
                //Establishes the failsafe directory and copies the ARC prepare for the unpacking process.
                var arcBuildSession1 = new StringBuilder();
                arcBuildSession1.Append(Properties.Settings.Default.archivesPath);
                arcBuildSession1.Append(Global.sessionID);
                arcBuildSession1.Append(@"\");
                arcBuildSession1.Append(failsafeCheck1);
                arcBuildSession1.Append(@"\");
                if (!Directory.Exists(arcBuildSession1.ToString())) Directory.CreateDirectory(arcBuildSession1.ToString());
                if (File.Exists(text_ARC1.Text)) File.Copy(text_ARC1.Text, arcBuildSession1.ToString() + Path.GetFileName(text_ARC1.Text), true);
                #endregion

                #region Unpacking ARC...
                Global.arcState = "unpack";

                //Sets up the BASIC application and executes the unpacking process.
                var basicWrite1 = File.Create(Properties.Settings.Default.toolsPath + "unpack.bat");
                var basicSession1 = new UTF8Encoding(true).GetBytes("\"" + Properties.Settings.Default.unpackFile + "\" \"" + arcBuildSession1.ToString() + Path.GetFileName(text_ARC1.Text) + "\"");
                basicWrite1.Write(basicSession1, 0, basicSession1.Length);
                basicWrite1.Close();
                var unpackSession1 = new ProcessStartInfo(Properties.Settings.Default.toolsPath + "unpack.bat");
                unpackSession1.WorkingDirectory = Properties.Settings.Default.toolsPath;
                unpackSession1.WindowStyle = ProcessWindowStyle.Hidden;
                var Unpack1 = Process.Start(unpackSession1);
                var unpackDialog1 = new Status();
                var parentLeft1 = Left + ((Width - unpackDialog1.Width) / 2);
                var parentTop1 = Top + ((Height - unpackDialog1.Height) / 2);
                unpackDialog1.Location = new System.Drawing.Point(parentLeft1, parentTop1);
                unpackDialog1.Show();
                Unpack1.WaitForExit();
                Unpack1.Close();

                Global.arcState = null;
                #endregion

                #region Writing metadata...
                //Writes metadata to the unpacked directory to ensure the original path is remembered.
                var metadataWrite1 = File.Create(arcBuildSession1.ToString() + "metadata.ini");
                var metadataSession1 = new UTF8Encoding(true).GetBytes(text_ARC1.Text);
                metadataWrite1.Write(metadataSession1, 0, metadataSession1.Length);
                metadataWrite1.Close();
                unpackDialog1.Close();
                #endregion

                #region Building unpack data...
                //Builds the main string which locates the ARC's final unpack directory.
                string failsafeCheck2 = Path.GetRandomFileName(); //Unpacked ARCs will have a unique directory to prevent overwriting.
                var unpackBuildSession2electricboogaloo = new StringBuilder();
                unpackBuildSession2electricboogaloo.Append(Properties.Settings.Default.archivesPath);
                unpackBuildSession2electricboogaloo.Append(Global.sessionID);
                unpackBuildSession2electricboogaloo.Append(@"\");
                unpackBuildSession2electricboogaloo.Append(failsafeCheck2);
                unpackBuildSession2electricboogaloo.Append(@"\");
                unpackBuildSession2electricboogaloo.Append(Path.GetFileNameWithoutExtension(text_ARC2.Text));
                //unpackBuildSession2.Append(@"\");
                if (!Directory.Exists(unpackBuildSession2electricboogaloo.ToString() + @"\")) Directory.CreateDirectory(unpackBuildSession2electricboogaloo.ToString() + @"\");
                #endregion

                #region Building ARC data...
                //Establishes the failsafe directory and copies the ARC prepare for the unpacking process.
                var arcBuildSession2 = new StringBuilder();
                arcBuildSession2.Append(Properties.Settings.Default.archivesPath);
                arcBuildSession2.Append(Global.sessionID);
                arcBuildSession2.Append(@"\");
                arcBuildSession2.Append(failsafeCheck2);
                arcBuildSession2.Append(@"\");
                if (!Directory.Exists(arcBuildSession2.ToString())) Directory.CreateDirectory(arcBuildSession2.ToString());
                if (File.Exists(text_ARC2.Text)) File.Copy(text_ARC2.Text, arcBuildSession2.ToString() + Path.GetFileName(text_ARC2.Text), true);
                #endregion

                #region Unpacking ARC...
                Global.arcState = "unpack";

                //Sets up the BASIC application and executes the unpacking process.
                var basicWrite2 = File.Create(Properties.Settings.Default.toolsPath + "unpack.bat");
                var basicSession2 = new UTF8Encoding(true).GetBytes("\"" + Properties.Settings.Default.unpackFile + "\" \"" + arcBuildSession2.ToString() + Path.GetFileName(text_ARC2.Text) + "\"");
                basicWrite2.Write(basicSession2, 0, basicSession2.Length);
                basicWrite2.Close();
                var unpackSession2 = new ProcessStartInfo(Properties.Settings.Default.toolsPath + "unpack.bat");
                unpackSession2.WorkingDirectory = Properties.Settings.Default.toolsPath;
                unpackSession2.WindowStyle = ProcessWindowStyle.Hidden;
                var Unpack2 = Process.Start(unpackSession2);
                var unpackDialog2 = new Status();
                var parentLeft2 = Left + ((Width - unpackDialog2.Width) / 2);
                var parentTop2 = Top + ((Height - unpackDialog2.Height) / 2);
                unpackDialog2.Location = new System.Drawing.Point(parentLeft2, parentTop2);
                unpackDialog2.Show();
                Unpack2.WaitForExit();
                Unpack2.Close();

                Global.arcState = null;
                #endregion

                #region Writing metadata...
                //Writes metadata to the unpacked directory to ensure the original path is remembered.
                var metadataWrite2 = File.Create(arcBuildSession2.ToString() + "metadata.ini");
                var metadataSession2 = new UTF8Encoding(true).GetBytes(text_ARC2.Text);
                metadataWrite2.Write(metadataSession2, 0, metadataSession2.Length);
                metadataWrite2.Close();
                unpackDialog2.Close();
                #endregion

                #region Processing ARCs...
                Global.arcState = "processing";

                var processDialog = new Status();
                var parentLeftProc = Left + ((Width - processDialog.Width) / 2);
                var parentTopProc = Top + ((Height - processDialog.Height) / 2);
                processDialog.Location = new System.Drawing.Point(parentLeft1, parentTop1);
                processDialog.Show();

                var arcData1 = unpackBuildSession1.ToString();
                var arcData2 = unpackBuildSession2electricboogaloo.ToString() + @"\";
                foreach (string dirPath in Directory.GetDirectories(arcData1, "*", SearchOption.AllDirectories)) Directory.CreateDirectory(dirPath.Replace(arcData1, arcData2));
                foreach (string newPath in Directory.GetFiles(arcData1, "*.*", SearchOption.AllDirectories)) File.Copy(newPath, newPath.Replace(arcData1, arcData2), true);
                processDialog.Close();

                Global.arcState = null;
                #endregion

                #region Repacking merged ARC...
                Global.arcState = "repack";

                //Sets up the BASIC application and executes the repacking process.
                var basicWrite = File.Create(Properties.Settings.Default.toolsPath + "repack.bat");
                var basicSession = new UTF8Encoding(true).GetBytes("\"" + Properties.Settings.Default.repackFile + "\" \"" + unpackBuildSession2electricboogaloo.ToString() + "\"");
                basicWrite.Write(basicSession, 0, basicSession.Length);
                basicWrite.Close();
                var repackSession = new ProcessStartInfo(Properties.Settings.Default.toolsPath + "repack.bat");
                repackSession.WorkingDirectory = Properties.Settings.Default.toolsPath;
                repackSession.WindowStyle = ProcessWindowStyle.Hidden;
                var Repack = Process.Start(repackSession);
                var repackDialog = new Status();
                var parentLeft = Left + ((Width - repackDialog.Width) / 2);
                var parentTop = Top + ((Height - repackDialog.Height) / 2);
                repackDialog.Location = new System.Drawing.Point(parentLeft, parentTop);
                repackDialog.Show();
                Repack.WaitForExit();
                Repack.Close();
                string archivePath = unpackBuildSession2electricboogaloo.ToString() + ".arc";
                if (File.Exists(archivePath)) File.Copy(archivePath, text_Output.Text, true);
                repackDialog.Close();

                Global.arcState = null;
                #endregion
            }
            catch { MessageBox.Show("An error occurred when merging the archives.", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        void Btn_Merge_Click(object sender, EventArgs e)
        {
            if (!File.Exists(text_ARC1.Text)) MessageBox.Show("Please specify an ARC file.", "File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (!File.Exists(text_ARC2.Text)) MessageBox.Show("Please specify an ARC file.", "File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (text_Output.Text == "" || !Directory.Exists(Path.GetDirectoryName(text_Output.Text))) MessageBox.Show("Please specify an output.", "File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                mergeARC();
            }
        }

        void Btn_BrowseARC1_Click(object sender, EventArgs e)
        {
            if (ofd_OpenARC.ShowDialog() == DialogResult.OK)
            {
                text_ARC1.Text = ofd_OpenARC.FileName;
            }
        }

        void Btn_BrowseARC2_Click(object sender, EventArgs e)
        {
            if (ofd_OpenARC.ShowDialog() == DialogResult.OK)
            {
                text_ARC2.Text = ofd_OpenARC.FileName;
            }
        }

        void Btn_BrowseOutput_Click(object sender, EventArgs e)
        {
            if (sfd_Output.ShowDialog() == DialogResult.OK)
            {
                text_Output.Text = sfd_Output.FileName;
            }
        }
    }
}
