using System;
using WMPLib;
using System.IO;
using System.Linq;
using Toolkit.Text;
using System.Drawing;
using VGAudio.Formats;
using SonicAudioLib.IO;
using System.Windows.Forms;
using Toolkit.EnvironmentX;
using VGAudio.Containers.Adx;
using VGAudio.Containers.Wave;

namespace Toolkit.Tools
{
    public partial class SonicSoundStudio : Form
    {
        private Main mainForm = null;
        private string location = Paths.currentPath;
        private string nowPlaying = string.Empty;

        public SonicSoundStudio(Form callingForm) {
            mainForm = callingForm as Main;
            InitializeComponent();
            tm_NoCheckOnClickTimer.Start();
        }

        private void SonicSoundStudio_Load(object sender, EventArgs e) {
            combo_Encoder.SelectedIndex = Properties.Settings.Default.sss_Encoder;
            check_PatchXMA.Checked = Properties.Settings.Default.sss_PatchXMA;
        }

        private async void Btn_MediaControl_Click(object sender, EventArgs e) {
            if (btn_MediaControl.BackColor == Color.Tomato) { //Pause
                btn_MediaControl.Text = "►";
                btn_MediaControl.BackColor = Color.LightGreen;
                axWMP_Player.Ctlcontrols.pause();
            } else { //Play
                btn_MediaControl.Text = "❚❚";
                btn_MediaControl.BackColor = Color.Tomato;
                if (axWMP_Player.URL == string.Empty) {
                    string sound = clb_SNDs.SelectedItem.ToString();
                    string tempPath = Path.GetTempPath();
                    string tempFile = Path.GetRandomFileName();
                    if (Path.GetExtension(sound) == ".wav") {
                        if (File.Exists(Path.Combine(location, sound)) && Verification.VerifyMagicNumberCommon(Path.Combine(location, sound)))
                            axWMP_Player.URL = Path.Combine(location, sound);
                    } else if (Path.GetExtension(sound) == ".adx") {
                        if (File.Exists(Path.Combine(location, sound)) && Verification.VerifyMagicNumberCommon(Path.Combine(location, sound))) {
                            byte[] adxFile = File.ReadAllBytes(Path.Combine(location, sound));
                            AudioData audio = new AdxReader().Read(adxFile);
                            byte[] wavFile = new WaveWriter().GetFile(audio);
                            File.WriteAllBytes(Path.Combine(tempPath, $"{tempFile}.wav"), wavFile);
                            nowPlaying = axWMP_Player.URL = Path.Combine(tempPath, $"{tempFile}.wav");
                        }
                    } else if (Path.GetExtension(sound) == ".at3") {
                        if (File.Exists(Path.Combine(location, sound)) && Verification.VerifyMagicNumberCommon(Path.Combine(location, sound))) {
                            var process = await ProcessAsyncHelper.ExecuteShellCommand(Paths.AT3Tool,
                                                $"-d \"{Path.Combine(location, sound)}\" \"{Path.Combine(tempPath, tempFile)}.wav\"",
                                                Application.StartupPath,
                                                100000);
                            if (process.ExitCode != 0)
                                MessageBox.Show(SystemMessages.ex_PreviewFailure, SystemMessages.tl_FatalError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            nowPlaying = axWMP_Player.URL = Path.Combine(tempPath, $"{tempFile}.wav");
                        }
                    } else if (Path.GetExtension(sound) == ".xma") {
                        try {
                            byte[] xmaBytes = File.ReadAllBytes(Path.Combine(location, sound)).ToArray();
                            string hexString = BitConverter.ToString(xmaBytes).Replace("-", "");
                            if (hexString.Contains(Properties.Resources.xma_Patch)) {
                                FileInfo fi = new FileInfo($"{Path.Combine(location, sound)}");
                                FileStream fs = fi.Open(FileMode.Open);
                                long bytesToDelete = 56;
                                fs.SetLength(Math.Max(0, fi.Length - bytesToDelete));
                                fs.Close();
                            }
                        } catch { MessageBox.Show(SystemMessages.ex_PreviewFailure, SystemMessages.tl_FatalError, MessageBoxButtons.OK, MessageBoxIcon.Error); }
                        if (File.Exists(Path.Combine(location, sound)) && Verification.VerifyMagicNumberCommon(Path.Combine(location, sound))) {
                            var process = await ProcessAsyncHelper.ExecuteShellCommand(Paths.XMADecoder,
                                                $"\"{Path.Combine(location, sound)}\"",
                                                tempPath,
                                                100000);
                            if (process.ExitCode != 0)
                                MessageBox.Show(SystemMessages.ex_PreviewFailure, SystemMessages.tl_FatalError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            nowPlaying = axWMP_Player.URL = Path.Combine(tempPath, $"{Path.GetFileNameWithoutExtension(sound)}.wav");
                        }
                    }
                }
                else
                    axWMP_Player.Ctlcontrols.play();
            }
        }

        private void Combo_Encoder_SelectedIndexChanged(object sender, EventArgs e) {
            clb_SNDs.Items.Clear();
            if (combo_Encoder.SelectedIndex == 0) {
                check_PatchXMA.Enabled = false;
                ListVerifiedSoundBytes("*.wav");
                ListVerifiedSoundBytes("*.csb");
            } else if (combo_Encoder.SelectedIndex == 1) {
                check_PatchXMA.Enabled = false;
                ListVerifiedSoundBytes("*.wav");
            } else if (combo_Encoder.SelectedIndex == 2) {
                check_PatchXMA.Enabled = false;
                if (Directory.GetDirectories(location).Length > 0)
                    foreach (string CSB in Directory.GetDirectories(location))
                        if (Directory.Exists(Path.Combine(location, CSB)) && Verification.VerifyCriWareSoundBank(Path.Combine(location, CSB)))
                            clb_SNDs.Items.Add(Path.GetFileName(CSB));
            } else if (combo_Encoder.SelectedIndex == 3) {
                check_PatchXMA.Enabled = false;
                ListVerifiedSoundBytes("*.adx");
                ListVerifiedSoundBytes("*.at3");
                ListVerifiedSoundBytes("*.csb");
                ListVerifiedSoundBytes("*.xma");
            } else if (combo_Encoder.SelectedIndex == 4) {
                check_PatchXMA.Enabled = true;
                ListVerifiedSoundBytes("*.wav");
            }
            Properties.Settings.Default.sss_Encoder = combo_Encoder.SelectedIndex;
        }

        private void ListVerifiedSoundBytes(string filter) {
            if (Directory.GetFiles(location, filter).Length > 0)
                foreach (string SND in Directory.GetFiles(location, filter, SearchOption.TopDirectoryOnly))
                    if (File.Exists(SND) && Verification.VerifyMagicNumberCommon(SND))
                        clb_SNDs.Items.Add(Path.GetFileName(SND));
        }

        private async void Btn_Process_Click(object sender, EventArgs e) {
            try {
                if (combo_Encoder.SelectedIndex == 0) { //ADX
                    foreach (string SND in clb_SNDs.CheckedItems)
                        if (File.Exists(Path.Combine(location, SND)) && Verification.VerifyMagicNumberCommon(Path.Combine(location, SND))) {
                            if (Path.GetExtension(SND).ToLower() == ".wav") {
                                try {
                                    mainForm.Status = StatusMessages.cmn_Converting(SND, "ADX", false);
                                    byte[] wavFile = File.ReadAllBytes(Path.Combine(location, SND));
                                    AudioData audio = new WaveReader().Read(wavFile);
                                    byte[] adxFile = new AdxWriter().GetFile(audio);
                                    File.WriteAllBytes(Path.Combine(location, $"{Path.GetFileNameWithoutExtension(SND)}.adx"), adxFile);
                                } catch { mainForm.Status = StatusMessages.cmn_ConvertFailed(SND, "ADX", false); }
                            } else if (Path.GetExtension(SND).ToLower() == ".csb") {
                                try {
                                    mainForm.Status = StatusMessages.cmn_Unpacking(SND, false);
                                    var extractor = new DataExtractor {
                                        MaxThreads = 1,
                                        BufferSize = 4096,
                                        EnableThreading = false
                                    };
                                    Directory.CreateDirectory(Path.Combine(location, Path.GetFileNameWithoutExtension(SND)));
                                    CSBTools.ExtractCSBNodes(extractor, Path.Combine(location, SND), Path.Combine(location, Path.GetFileNameWithoutExtension(SND)));
                                    extractor.Run();
                                    mainForm.Status = StatusMessages.cmn_Unpacked(SND, false);
                                } catch { mainForm.Status = StatusMessages.cmn_UnpackFailed(SND, false); }
                            }
                        }
                } else if (combo_Encoder.SelectedIndex == 1) { //AT3
                    foreach (string WAV in clb_SNDs.CheckedItems)
                        if (File.Exists(Path.Combine(location, WAV)) && Verification.VerifyMagicNumberCommon(Path.Combine(location, WAV))) {
                            mainForm.Status = StatusMessages.cmn_Converting(WAV, "AT3", false);
                            var process = await ProcessAsyncHelper.ExecuteShellCommand(Paths.AT3Tool,
                                                $"-e \"{Path.Combine(location, WAV)}\" \"{Path.Combine(location, Path.GetFileNameWithoutExtension(WAV))}.at3\"",
                                                Application.StartupPath,
                                                100000);
                            if (process.ExitCode != 0)
                                mainForm.Status = StatusMessages.cmn_ConvertFailed(WAV, "AT3", false);
                        }
                } else if (combo_Encoder.SelectedIndex == 2) { //CSB
                    foreach (string CSB in clb_SNDs.CheckedItems)
                        if (Directory.Exists(Path.Combine(location, CSB)) && Verification.VerifyCriWareSoundBank(Path.Combine(location, CSB))) {
                            try {
                                mainForm.Status = StatusMessages.cmn_Repacking(CSB, false);
                                CSBTools.WriteCSB(Path.Combine(location, CSB));
                                mainForm.Status = StatusMessages.cmn_Repacked(CSB, false);
                            } catch { mainForm.Status = StatusMessages.cmn_RepackFailed(CSB, false); }
                        }
                } else if (combo_Encoder.SelectedIndex == 3) { //WAV
                    foreach (string SND in clb_SNDs.CheckedItems)
                        DecodeAudioData(SND);
                } else if (combo_Encoder.SelectedIndex == 4) { //XMA
                    foreach (string WAV in clb_SNDs.CheckedItems)
                        if (File.Exists(Path.Combine(location, WAV)) && Verification.VerifyMagicNumberCommon(Path.Combine(location, WAV))) {
                            string xmaOutput = Path.Combine(location, $"{Path.GetFileNameWithoutExtension(WAV)}.xma");
                            try { if (File.Exists(xmaOutput)) File.Delete(xmaOutput); } catch { }
                            var process = await ProcessAsyncHelper.ExecuteShellCommand(Paths.XMAEncoder,
                                                $"\"{Path.Combine(location, WAV)}\" /L",
                                                location,
                                                100000);
                            if (process.ExitCode != 0)
                                mainForm.Status = StatusMessages.cmn_ConvertFailed(WAV, "XMA", false);
                            try {
                                if (check_PatchXMA.Checked) {
                                    byte[] xmaBytes = File.ReadAllBytes(xmaOutput).ToArray();
                                    string hexString = BitConverter.ToString(xmaBytes).Replace("-", "");
                                    if (!hexString.Contains(Properties.Resources.xma_Patch))
                                        ByteArray.ByteArrayToFile(xmaOutput, ByteArray.StringToByteArray(Properties.Resources.xma_Patch));
                                    else break;
                                }
                            } catch { mainForm.Status = StatusMessages.xma_EncodeFooterError(WAV, false); return; }
                        }
                }
            } catch (Exception ex) { MessageBox.Show($"{SystemMessages.ex_EncoderError}\n\n{ex}", SystemMessages.tl_FatalError, MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private async void DecodeAudioData(string sound) {
            if (File.Exists(Path.Combine(location, sound)) && Verification.VerifyMagicNumberCommon(Path.Combine(location, sound)))
                if (Path.GetExtension(sound).ToLower() == ".adx") {
                    try {
                        mainForm.Status = StatusMessages.cmn_Converting(sound, "WAV", false);
                        byte[] adxFile = File.ReadAllBytes(Path.Combine(location, sound));
                        AudioData audio = new AdxReader().Read(adxFile);
                        byte[] wavFile = new WaveWriter().GetFile(audio);
                        File.WriteAllBytes(Path.Combine(location, $"{Path.GetFileNameWithoutExtension(sound)}.wav"), wavFile);
                    } catch { mainForm.Status = StatusMessages.cmn_ConvertFailed(sound, "WAV", false); }
                } else if (Path.GetExtension(sound).ToLower() == ".at3") {
                    mainForm.Status = StatusMessages.cmn_Converting(sound, "WAV", false);
                    var process = await ProcessAsyncHelper.ExecuteShellCommand(Paths.AT3Tool,
                                        $"-d \"{Path.Combine(location, sound)}\" \"{Path.Combine(location, Path.GetFileNameWithoutExtension(sound))}.wav\"",
                                        Application.StartupPath,
                                        100000);
                    if (process.ExitCode != 0)
                        mainForm.Status = StatusMessages.cmn_ConvertFailed(sound, "WAV", false);
                } else if (Path.GetExtension(sound).ToLower() == ".csb") {
                    try {
                        mainForm.Status = StatusMessages.cmn_Unpacking(sound, false);
                        var extractor = new DataExtractor {
                            MaxThreads = 1,
                            BufferSize = 4096,
                            EnableThreading = false
                        };
                        mainForm.Status = StatusMessages.cmn_Unpacked(sound, false);
                        Directory.CreateDirectory(Path.Combine(location, Path.GetFileNameWithoutExtension(sound)));
                        CSBTools.ExtractCSBNodes(extractor, Path.Combine(location, sound), Path.Combine(location, Path.GetFileNameWithoutExtension(sound)));
                        extractor.Run();
                    } catch { mainForm.Status = StatusMessages.cmn_UnpackFailed(sound, false); }

                    if (combo_Encoder.SelectedIndex == 3)
                        if (Directory.Exists(Path.Combine(location, Path.GetFileNameWithoutExtension(sound))))
                            foreach (string ADX in Directory.GetFiles(Path.Combine(location, Path.GetFileNameWithoutExtension(sound)), "*.adx", SearchOption.AllDirectories)) {
                                try {
                                    mainForm.Status = StatusMessages.cmn_Converting(ADX, "WAV", true);
                                    byte[] adxFile = File.ReadAllBytes(ADX);
                                    AudioData audio = new AdxReader().Read(adxFile);
                                    byte[] wavFile = new WaveWriter().GetFile(audio);
                                    File.WriteAllBytes(Path.Combine(Path.GetDirectoryName(ADX), $"{Path.GetFileNameWithoutExtension(ADX)}.wav"), wavFile);
                                    File.Delete(ADX);
                                } catch { mainForm.Status = StatusMessages.cmn_ConvertFailed(ADX, "WAV", true); }
                            }
                } else if (Path.GetExtension(sound).ToLower() == ".xma") {
                    mainForm.Status = StatusMessages.cmn_Converting(sound, "WAV", false);
                    try {
                        byte[] xmaBytes = File.ReadAllBytes(Path.Combine(location, sound)).ToArray();
                        string hexString = BitConverter.ToString(xmaBytes).Replace("-", "");
                        if (hexString.Contains(Properties.Resources.xma_Patch)) {
                            FileInfo fi = new FileInfo($"{Path.Combine(location, sound)}");
                            FileStream fs = fi.Open(FileMode.Open);
                            long bytesToDelete = 56;
                            fs.SetLength(Math.Max(0, fi.Length - bytesToDelete));
                            fs.Close();
                        }
                    } catch { mainForm.Status = StatusMessages.xma_DecodeFooterError(sound, false); return; }
                    var process = await ProcessAsyncHelper.ExecuteShellCommand(Paths.XMADecoder,
                                        $"\"{Path.Combine(location, sound)}\"",
                                        location,
                                        100000);
                    if (process.ExitCode != 0)
                        mainForm.Status = StatusMessages.cmn_ConvertFailed(sound, "WAV", false);
                }
        }

        private void Clb_SNDs_SelectedIndexChanged(object sender, EventArgs e) {
            axWMP_Player.close();
            tm_MediaPlayer.Stop();
            tracker_MediaBar.Value = 0;
            btn_MediaControl.Text = "►";
            btn_MediaControl.BackColor = Color.LightGreen;
            if (File.Exists(nowPlaying)) File.Delete(nowPlaying);
            if (clb_SNDs.SelectedItems.Count > 0 && Path.GetExtension(clb_SNDs.SelectedItem.ToString()) != ".csb") {
                lbl_NowPlaying.Text = $"Now Playing: {clb_SNDs.SelectedItem}";
                tracker_MediaBar.Enabled = true;
                btn_MediaControl.Enabled = true;
            } else {
                lbl_NowPlaying.Text = "Now Playing: None.";
                tracker_MediaBar.Enabled = false;
                btn_MediaControl.Enabled = false;
            }
        }

        private void Btn_SelectAll_Click(object sender, EventArgs e) {
            for (int i = 0; i < clb_SNDs.Items.Count; i++) clb_SNDs.SetItemChecked(i, true);
            btn_Process.Enabled = true;
        }

        private void Btn_DeselectAll_Click(object sender, EventArgs e) {
            for (int i = 0; i < clb_SNDs.Items.Count; i++) clb_SNDs.SetItemChecked(i, false);
            btn_Process.Enabled = false;
        }

        private void Check_PatchXMA_CheckedChanged(object sender, EventArgs e) {
            Properties.Settings.Default.sss_PatchXMA = check_PatchXMA.Checked;
        }

        private void SonicSoundStudio_FormClosing(object sender, FormClosingEventArgs e) {
            axWMP_Player.close();
            Properties.Settings.Default.Save();
        }

        private void Tm_NoCheckOnClickTimer_Tick(object sender, EventArgs e) {
            if (clb_SNDs.CheckedItems.Count > 0) btn_Process.Enabled = true;
            else btn_Process.Enabled = false;
        }

        private void AxWMP_Player_PlayStateChange(object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e) {
            if (axWMP_Player.playState == WMPPlayState.wmppsPlaying) {
                tracker_MediaBar.Maximum = (int)axWMP_Player.Ctlcontrols.currentItem.duration;
                tm_MediaPlayer.Start();
            } else if (axWMP_Player.playState == WMPPlayState.wmppsPaused)
                tm_MediaPlayer.Stop();
            else if (axWMP_Player.playState == WMPPlayState.wmppsStopped) {
                tm_MediaPlayer.Stop();
                tracker_MediaBar.Value = 0;
                btn_MediaControl.Text = "►";
                btn_MediaControl.BackColor = Color.LightGreen;
            }
        }

        private void Tm_MediaPlayer_Tick(object sender, EventArgs e) {
            if (axWMP_Player.playState == WMPPlayState.wmppsPlaying)
                tracker_MediaBar.Value = (int)axWMP_Player.Ctlcontrols.currentPosition;
        }

        private void Tracker_MediaBar_Scroll(object sender, EventArgs e) {
            axWMP_Player.Ctlcontrols.currentPosition = tracker_MediaBar.Value;
            if (axWMP_Player.playState != WMPPlayState.wmppsPaused) {
                axWMP_Player.Ctlcontrols.play();
                tm_MediaPlayer.Start();
            }
        }
    }
}