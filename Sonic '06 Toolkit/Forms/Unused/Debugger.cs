using System;
using System.Windows.Forms;

// Sonic '06 Toolkit is licensed under the MIT License:
/*
 * MIT License

 * Copyright (c) 2019 Gabriel (HyperPolygon64)

 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:

 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.

 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 */

namespace Sonic_06_Toolkit
{
    public partial class Debugger : Form
    {
        public Debugger()
        {
            InitializeComponent();
        }

        private Main mainForm = null;
        public Debugger(Form callingForm)
        {
            mainForm = callingForm as Main;
            InitializeComponent();
        }

        public static bool unsafeState = false;

        void Debugger_Load(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.debugShow == true) { check_DebugAdvanced.Checked = true; }
            else { check_DebugAdvanced.Checked = false; }

            if (Properties.Settings.Default.skipWorkaround == true) { check_skipWorkaround.Checked = true; }
            else { check_skipWorkaround.Checked = false; }

            nud_Progress.Value = this.mainForm.UpdateProgressValue;

            tm_Update.Start();
            tm_getStorage.Start();
        }

        void Tm_Update_Tick(object sender, EventArgs e)
        {
            #region Global
            if (Tools.Global.versionNumber != null) versionNumber.Text = Tools.Global.versionNumber; else versionNumber.Text = "None";
            if (Tools.Global.latestVersion != null) latestVersion.Text = Tools.Global.latestVersion; else latestVersion.Text = "None";
            if (Tools.Global.serverStatus != null) serverStatus.Text = Tools.Global.serverStatus; else serverStatus.Text = "None";
            if (Tools.Global.currentPath != null) currentPath.Text = Tools.Global.currentPath; else currentPath.Text = "None";
            if (Tools.Global.updateState != null) updateState.Text = Tools.Global.updateState; else updateState.Text = "None";
            if (Tools.Global.exisoState != null) exisoState.Text = Tools.Global.exisoState; else exisoState.Text = "None";
            if (Tools.Global.getStorage != null || Tools.Global.getStorage == string.Empty) lbl_getStorage.Text = Tools.Global.getStorage; else lbl_getStorage.Text = "None";
            if (Tools.Global.arcState != null) arcState.Text = Tools.Global.arcState; else arcState.Text = "None";
            if (Tools.Global.adxState != null) adxState.Text = Tools.Global.adxState; else adxState.Text = "None";
            if (Tools.Global.at3State != null) at3State.Text = Tools.Global.at3State; else at3State.Text = "None";
            if (Tools.Global.csbState != null) csbState.Text = Tools.Global.csbState; else csbState.Text = "None";
            if (Tools.Global.ddsState != null) ddsState.Text = Tools.Global.ddsState; else ddsState.Text = "None";
            if (Tools.Global.lubState != null) lubState.Text = Tools.Global.lubState; else lubState.Text = "None";
            if (Tools.Global.setState != null) setState.Text = Tools.Global.setState; else setState.Text = "None";
            if (Tools.Global.mstState != null) mstState.Text = Tools.Global.mstState; else mstState.Text = "None";
            if (Tools.Global.xnoState != null) xnoState.Text = Tools.Global.xnoState; else xnoState.Text = "None";
            if (Tools.Global.applicationData != null) applicationData.Text = Tools.Global.applicationData; else applicationData.Text = "None";
            sessionID.Text = Tools.Global.sessionID.ToString();
            getIndex.Text = Tools.Global.getIndex.ToString();
            javaCheck.Text = Tools.Global.javaCheck.ToString();
            gameChanged.Text = Tools.Global.gameChanged.ToString();
            #endregion

            #region Settings
            showSessionID.Text = Properties.Settings.Default.showSessionID.ToString();
            if (Properties.Settings.Default.theme != "") theme.Text = Properties.Settings.Default.theme; else theme.Text = "None";
            if (Properties.Settings.Default.rootPath != "") rootPath.Text = Properties.Settings.Default.rootPath; else rootPath.Text = "None";
            if (Properties.Settings.Default.toolsPath != "") toolsPath.Text = Properties.Settings.Default.toolsPath; else toolsPath.Text = "None";
            if (Properties.Settings.Default.archivesPath != "") archivesPath.Text = Properties.Settings.Default.archivesPath; else archivesPath.Text = "None";
            disableSoftwareUpdater.Text = Properties.Settings.Default.disableSoftwareUpdater.ToString();
            if (Properties.Settings.Default.unlubPath != "") unlubPath.Text = Properties.Settings.Default.unlubPath; else unlubPath.Text = "None";
            if (Properties.Settings.Default.xnoPath != "") xnoPath.Text = Properties.Settings.Default.xnoPath; else xnoPath.Text = "None";
            if (Properties.Settings.Default.unpackFile != "") unpackFile.Text = Properties.Settings.Default.unpackFile; else unpackFile.Text = "None";
            if (Properties.Settings.Default.repackFile != "") repackFile.Text = Properties.Settings.Default.repackFile; else repackFile.Text = "None";
            if (Properties.Settings.Default.xnoFile != "") xnoFile.Text = Properties.Settings.Default.xnoFile; else xnoFile.Text = "None";
            if (Properties.Settings.Default.csbFile != "") csbFile.Text = Properties.Settings.Default.csbFile; else csbFile.Text = "None";
            if (Properties.Settings.Default.adx2wavFile != "") adx2wavFile.Text = Properties.Settings.Default.adx2wavFile; else adx2wavFile.Text = "None";
            if (Properties.Settings.Default.criconverterFile != "") criconverterFile.Text = Properties.Settings.Default.criconverterFile; else criconverterFile.Text = "None";
            showLogo.Text = Properties.Settings.Default.showLogo.ToString();
            unpackAndLaunch.Text = Properties.Settings.Default.unpackAndLaunch.ToString();
            if (Properties.Settings.Default.gamePath != "") gamePath.Text = Properties.Settings.Default.gamePath; else gamePath.Text = "None";
            if (Properties.Settings.Default.exisoFile != "") exisoFile.Text = Properties.Settings.Default.exisoFile; else exisoFile.Text = "None";
            if (Properties.Settings.Default.at3File != "") at3File.Text = Properties.Settings.Default.at3File; else at3File.Text = "None";
            if (Properties.Settings.Default.xeniaFile != "") xeniaFile.Text = Properties.Settings.Default.xeniaFile; else xeniaFile.Text = "None";
            if (Properties.Settings.Default.directXFile != "") directXFile.Text = Properties.Settings.Default.directXFile; else directXFile.Text = "None";
            if (Properties.Settings.Default.arctoolFile != "") arctoolFile.Text = Properties.Settings.Default.arctoolFile; else arctoolFile.Text = "None";
            ignoreLoop.Text = Properties.Settings.Default.ignoreLoop.ToString();
            removeLoop.Text = Properties.Settings.Default.removeLoop.ToString();
            downmix.Text = Properties.Settings.Default.downmix.ToString();
            volume.Text = Properties.Settings.Default.volume.ToString();
            wholeLoop.Text = Properties.Settings.Default.wholeLoop.ToString();
            useGPU.Text = Properties.Settings.Default.useGPU.ToString();
            forceDirectX10.Text = Properties.Settings.Default.forceDirectX10.ToString();
            backupSET.Text = Properties.Settings.Default.backupSET.ToString();
            deleteXML.Text = Properties.Settings.Default.deleteXML.ToString();
            if (Properties.Settings.Default.mstFile != "") mstFile.Text = Properties.Settings.Default.mstFile; else mstFile.Text = "None";
            gameDir.Text = Properties.Settings.Default.gameDir.ToString();
            debugMode.Text = Properties.Settings.Default.debugMode.ToString();
            debugShow.Text = Properties.Settings.Default.debugShow.ToString();
            disableWarns.Text = Properties.Settings.Default.disableWarns.ToString();
            skipWorkaround.Text = Properties.Settings.Default.skipWorkaround.ToString();
            NOWLOADING.Text = Properties.Settings.Default.NOWLOADING.ToString();
            #endregion

            foreach (Control x in this.Controls)
            {
                if (x is Label)
                {
                    ((Label)x).Update();
                }
            }
        }

        private void ShowSessionID_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.showSessionID == true) Properties.Settings.Default.showSessionID = false;
            else Properties.Settings.Default.showSessionID = true;
            Properties.Settings.Default.Save();
        }

        private void DisableSoftwareUpdater_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.disableSoftwareUpdater == true) Properties.Settings.Default.disableSoftwareUpdater = false;
            else Properties.Settings.Default.disableSoftwareUpdater = true;
            Properties.Settings.Default.Save();
        }

        private void ShowLogo_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.showLogo == true) Properties.Settings.Default.showLogo = false;
            else Properties.Settings.Default.showLogo = true;
            Properties.Settings.Default.Save();
        }

        private void UnpackAndLaunch_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.unpackAndLaunch == true) Properties.Settings.Default.unpackAndLaunch = false;
            else Properties.Settings.Default.unpackAndLaunch = true;
            Properties.Settings.Default.Save();
        }

        private void IgnoreLoop_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.ignoreLoop == true) Properties.Settings.Default.ignoreLoop = false;
            else Properties.Settings.Default.ignoreLoop = true;
            Properties.Settings.Default.Save();
        }

        private void RemoveLoop_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.removeLoop == true) Properties.Settings.Default.removeLoop = false;
            else Properties.Settings.Default.removeLoop = true;
            Properties.Settings.Default.Save();
        }

        private void Downmix_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.downmix == true) Properties.Settings.Default.downmix = false;
            else Properties.Settings.Default.downmix = true;
            Properties.Settings.Default.Save();
        }

        private void WholeLoop_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.wholeLoop == true) Properties.Settings.Default.wholeLoop = false;
            else Properties.Settings.Default.wholeLoop = true;
            Properties.Settings.Default.Save();
        }

        private void UseGPU_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.useGPU == true) Properties.Settings.Default.useGPU = false;
            else Properties.Settings.Default.useGPU = true;
            Properties.Settings.Default.Save();
        }

        private void ForceDirectX10_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.forceDirectX10 == true) Properties.Settings.Default.forceDirectX10 = false;
            else Properties.Settings.Default.forceDirectX10 = true;
            Properties.Settings.Default.Save();
        }

        private void BackupSET_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.backupSET == true) Properties.Settings.Default.backupSET = false;
            else Properties.Settings.Default.backupSET = true;
            Properties.Settings.Default.Save();
        }

        private void DeleteXML_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.deleteXML == true) Properties.Settings.Default.deleteXML = false;
            else Properties.Settings.Default.deleteXML = true;
            Properties.Settings.Default.Save();
        }

        private void ArcState_Click(object sender, EventArgs e)
        {
            if (Tools.Global.arcState != "") Tools.Global.arcState = "";
        }

        private void AdxState_Click(object sender, EventArgs e)
        {
            if (Tools.Global.adxState != "") Tools.Global.adxState = "";
        }

        private void At3State_Click(object sender, EventArgs e)
        {
            if (Tools.Global.at3State != "") Tools.Global.at3State = "";
        }

        private void CsbState_Click(object sender, EventArgs e)
        {
            if (Tools.Global.csbState != "") Tools.Global.csbState = "";
        }

        private void DdsState_Click(object sender, EventArgs e)
        {
            if (Tools.Global.ddsState != "") Tools.Global.ddsState = "";
        }

        private void LubState_Click(object sender, EventArgs e)
        {
            if (Tools.Global.lubState != "") Tools.Global.lubState = "";
        }

        private void SetState_Click(object sender, EventArgs e)
        {
            if (Tools.Global.setState != "") Tools.Global.setState = "";
        }

        private void MstState_Click(object sender, EventArgs e)
        {
            if (Tools.Global.mstState != "") Tools.Global.mstState = "";
        }

        private void XnoState_Click(object sender, EventArgs e)
        {
            if (Tools.Global.xnoState != "") Tools.Global.xnoState = "";
        }

        private void JavaCheck_Click(object sender, EventArgs e)
        {
            if (Tools.Global.javaCheck == true) Tools.Global.javaCheck = false;
            else Tools.Global.javaCheck = true;
        }

        private void GameChanged_Click(object sender, EventArgs e)
        {
            if (Tools.Global.gameChanged == true) Tools.Global.gameChanged = false;
            else Tools.Global.gameChanged = true;
        }

        private void Btn_PreviewUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                var latestVersion = new Tools.TimedWebClient { Timeout = 100000 }.DownloadString("https://segacarnival.com/hyper/updates/latest_master.txt");
                var changeLogs = new Tools.TimedWebClient { Timeout = 100000 }.DownloadString("https://segacarnival.com/hyper/updates/changelogs.txt");
                if (latestVersion.Contains("Version"))
                {
                    DialogResult confirmUpdate = MessageBox.Show("Sonic '06 Toolkit - " + latestVersion + " is now available!\n\nChangelogs:\n" + changeLogs + "\n\nDo you wish to download it?", "New update available!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    switch (confirmUpdate)
                    {
                        case DialogResult.Yes:
                            MessageBox.Show("CONGRATURATIONS!\nAll Pages are displayed.\nTHANK YOU!\nYou are great debugger!", "CONGRATURATIONS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            break;
                        case DialogResult.No:
                            MessageBox.Show("CONGRATURATIONS!\nAll Pages are displayed.\nTHANK YOU!\nYou are great debugger!", "CONGRATURATIONS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            break;
                    }
                }
                else
                {
                    MessageBox.Show("The update servers are currently undergoing maintenance. Apologies for the inconvenience.", "Server Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch { MessageBox.Show("Unable to establish a connection to SEGA Carnival.", "Server Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void Btn_EraseSettings_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Reset();
        }

        private void Btn_UserClosing_Click(object sender, EventArgs e)
        {
            DialogResult confirmUserClosure = MessageBox.Show("Are you sure you want to quit? All unsaved changes will be lost if you haven't repacked.", "Are you sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            switch (confirmUserClosure)
            {
                case DialogResult.Yes:
                    MessageBox.Show("CONGRATURATIONS!\nAll Pages are displayed.\nTHANK YOU!\nYou are great debugger!", "CONGRATURATIONS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                case DialogResult.No:
                    MessageBox.Show("CONGRATURATIONS!\nAll Pages are displayed.\nTHANK YOU!\nYou are great debugger!", "CONGRATURATIONS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
            }
        }

        private void Btn_WindowsShutdown_Click(object sender, EventArgs e)
        {
            DialogResult confirmShutdownClosure = MessageBox.Show("Sonic '06 Toolkit is still running, are you sure you want to quit? All unsaved changes will be lost if you haven't repacked.", "Are you sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            switch (confirmShutdownClosure)
            {
                case DialogResult.Yes:
                    MessageBox.Show("CONGRATURATIONS!\nAll Pages are displayed.\nTHANK YOU!\nYou are great debugger!", "CONGRATURATIONS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                case DialogResult.No:
                    MessageBox.Show("CONGRATURATIONS!\nAll Pages are displayed.\nTHANK YOU!\nYou are great debugger!", "CONGRATURATIONS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
            }
        }

        private void Btn_UnsafeState_Click(object sender, EventArgs e)
        {
            if (unsafeState)
            {
                unsafeState = false;
                MessageBox.Show("Sonic '06 Toolkit is now in a safe state.", "Sonic '06 Toolkit", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                unsafeState = true;
                MessageBox.Show("Sonic '06 Toolkit is now in an unsafe state.", "Sonic '06 Toolkit", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        void Check_DebugAdvanced_CheckedChanged(object sender, EventArgs e)
        {
            if (check_DebugAdvanced.Checked == true)
            {
                this.mainForm.advanced_DebugMode.Visible = true;
                //this.mainForm.advanced_Separator1.Visible = true;
                Properties.Settings.Default.debugShow = true;
            }
            else
            {
                this.mainForm.advanced_DebugMode.Visible = false;
                //this.mainForm.advanced_Separator1.Visible = false;
                Properties.Settings.Default.debugShow = false;
            }
            Properties.Settings.Default.Save();
        }

        private void Check_ShowUpdater_CheckedChanged(object sender, EventArgs e)
        {
            if (check_ShowUpdater.Checked == true)
            {
                nud_Progress.Value = 0;
                nud_Progress.Enabled = true;
                btn_ResetProgress.Enabled = true;
                this.mainForm.UpdaterVisibility = true;
                if (Properties.Settings.Default.theme == "Original") this.mainForm.BackdropVisibility = true;
                else this.mainForm.BackdropVisibility = false;
                this.mainForm.UpdateNotifVisibility = false;
                this.mainForm.UpdateTimerState = false;
                this.mainForm.DisableUpdaterState = false;
            }
            else
            {
                nud_Progress.Value = 0;
                nud_Progress.Enabled = false;
                btn_ResetProgress.Enabled = false;
                this.mainForm.UpdaterVisibility = false;
                this.mainForm.BackdropVisibility = false;
                this.mainForm.UpdateNotifVisibility = false;
                this.mainForm.UpdateTimerState = true;
                this.mainForm.DisableUpdaterState = true;
            }
        }

        void Nud_Progress_ValueChanged(object sender, EventArgs e)
        {
            this.mainForm.UpdateProgressValue = Convert.ToInt32(nud_Progress.Value);
        }

        void Btn_ResetProgress_Click(object sender, EventArgs e)
        {
            this.mainForm.UpdateProgressValue = 0;
            nud_Progress.Value = 0;
        }

        void Tm_getStorage_Tick(object sender, EventArgs e)
        {
            if (this.mainForm.GetStorage == string.Empty)
            {
                getStorage.Text = "None";
            }
            else { getStorage.Text = this.mainForm.GetStorage; }
        }

        void Check_skipWorkaround_CheckedChanged(object sender, EventArgs e)
        {
            if (check_skipWorkaround.Checked == true)
            {
                Properties.Settings.Default.skipWorkaround = true;
            }
            else { Properties.Settings.Default.skipWorkaround = false; }

            Properties.Settings.Default.Save();
        }

        void Check_SkipWorkaround_Click(object sender, EventArgs e)
        {
            MessageBox.Show("A restart is required for this option to take effect.", "Sonic '06 Toolkit", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
