using System;
using System.IO;
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
    public partial class ADX_Studio : Form
    {
        public ADX_Studio()
        {
            InitializeComponent();
        }

        public static string ignoreLoop = "";
        public static string removeLoop = "";
        public static string downmix = "MONO";
        public static double vol = 1.0;

        void ADX_Studio_Load(object sender, EventArgs e)
        {
            clb_ADX.Items.Clear();

            if (Directory.GetFiles(Tools.Global.currentPath, "*.adx").Length > 0)
            {
                modes_ADXtoWAV.Checked = true;
                modes_WAVtoADX.Checked = false;
                options_Volume.Visible = false;
                options_Looping.Visible = false;
                options_DownmixToMono.Visible = false;
            }
            else if (Directory.GetFiles(Tools.Global.currentPath, "*.wav").Length > 0)
            {
                modes_ADXtoWAV.Checked = false;
                modes_WAVtoADX.Checked = true;
                options_Volume.Visible = true;
                options_Looping.Visible = true;
                options_DownmixToMono.Visible = true;
            }
            else { MessageBox.Show("There are no encodable files in this directory.", "No files available", MessageBoxButtons.OK, MessageBoxIcon.Information); Close(); }

            #region Setting saved properties...
            if (Properties.Settings.Default.ignoreLoop == true) looping_Ignore.Checked = true;
            if (Properties.Settings.Default.removeLoop == true) looping_Remove.Checked = true;
            if (Properties.Settings.Default.downmix == true) options_DownmixToMono.Checked = true;

            if (Properties.Settings.Default.volume == 0.0) volume_0.Checked = true;
            if (Properties.Settings.Default.volume == 1.0) volume_1.Checked = true;
            if (Properties.Settings.Default.volume == 2.0) volume_2.Checked = true;
            if (Properties.Settings.Default.volume == 3.0) volume_3.Checked = true;
            if (Properties.Settings.Default.volume == 4.0) volume_4.Checked = true;
            if (Properties.Settings.Default.volume == 5.0) volume_5.Checked = true;
            #endregion
        }

        void Btn_SelectAll_Click(object sender, EventArgs e)
        {
            //Checks all available checkboxes.
            for (int i = 0; i < clb_ADX.Items.Count; i++) clb_ADX.SetItemChecked(i, true);
            btn_Convert.Enabled = true;
        }

        private void Btn_DeselectAll_Click(object sender, EventArgs e)
        {
            //Unchecks all available checkboxes.
            for (int i = 0; i < clb_ADX.Items.Count; i++) clb_ADX.SetItemChecked(i, false);
            btn_Convert.Enabled = false;
        }

        void Btn_Convert_Click(object sender, EventArgs e)
        {
            //In the odd chance that someone is ever able to click Extract without anything selected, this will prevent that.
            if (clb_ADX.CheckedItems.Count == 0) MessageBox.Show("Please select a file.", "No files specified", MessageBoxButtons.OK, MessageBoxIcon.Information);
            if (modes_ADXtoWAV.Checked)
            {
                try
                {
                    //Gets all checked boxes from the CheckedListBox and builds a string for each ADX.
                    foreach (string selectedADX in clb_ADX.CheckedItems)
                    {
                        Tools.ADX.ConvertToWAV(1, string.Empty, selectedADX);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred when encoding the selected ADX files.\n\n{ex}", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Tools.Notification.Dispose();
                }
            }
            else if (modes_WAVtoADX.Checked)
            {
                try
                {
                    //Gets all checked boxes from the CheckedListBox and builds a string for each WAV.
                    foreach (string selectedWAV in clb_ADX.CheckedItems)
                    {
                        Tools.ADX.ConvertToADX(2, selectedWAV);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred when encoding the selected WAV files.\n\n{ex}", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Tools.Notification.Dispose();
                }
            }
        }

        void Clb_ADX_SelectedIndexChanged(object sender, EventArgs e)
        {
            clb_ADX.ClearSelected(); //Removes the blue highlight on recently checked boxes.

            //Enables/disables the Encode button, depending on whether a box has been checked.
            if (clb_ADX.CheckedItems.Count > 0)
            {
                btn_Convert.Enabled = true;
            }
            else
            {
                btn_Convert.Enabled = false;
            }
        }

        void Modes_ADXtoWAV_CheckedChanged(object sender, EventArgs e)
        {
            if (modes_ADXtoWAV.Checked == true)
            {
                clb_ADX.Items.Clear();

                #region Getting ADX files to convert...
                foreach (string ADX in Directory.GetFiles(Tools.Global.currentPath, "*.adx", SearchOption.TopDirectoryOnly))
                {
                    if (File.Exists(ADX))
                    {
                        clb_ADX.Items.Add(Path.GetFileName(ADX));
                    }
                }
                #endregion

                modes_WAVtoADX.Checked = false;
                options_Volume.Visible = false;
                options_Looping.Visible = false;
                options_DownmixToMono.Visible = false;
                btn_Convert.Enabled = false;

                if (Directory.GetFiles(Tools.Global.currentPath, "*.adx").Length == 0)
                {
                    MessageBox.Show("There are no ADX files to encode in this directory.", "No ADX files available", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    if (Directory.GetFiles(Tools.Global.currentPath, "*.wav").Length == 0)
                    {
                        Close();
                    }
                    else
                    {
                        modes_ADXtoWAV.Checked = false;
                        modes_WAVtoADX.Checked = true;
                    }
                }
            }
        }

        void Modes_WAVtoADX_CheckedChanged(object sender, EventArgs e)
        {
            if (modes_WAVtoADX.Checked == true)
            {
                clb_ADX.Items.Clear();

                #region Getting WAV files to convert...
                foreach (string WAV in Directory.GetFiles(Tools.Global.currentPath, "*.wav", SearchOption.TopDirectoryOnly))
                {
                    if (File.Exists(WAV))
                    {
                        clb_ADX.Items.Add(Path.GetFileName(WAV));
                    }
                }
                #endregion

                modes_ADXtoWAV.Checked = false;
                options_Volume.Visible = true;
                options_Looping.Visible = true;
                options_DownmixToMono.Visible = true;
                btn_Convert.Enabled = false;

                if (Directory.GetFiles(Tools.Global.currentPath, "*.wav").Length == 0)
                {
                    MessageBox.Show("There are no WAV files to encode in this directory.", "No WAV files available", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    if (Directory.GetFiles(Tools.Global.currentPath, "*.adx").Length == 0)
                    {
                        Close();
                    }
                    else
                    {
                        modes_ADXtoWAV.Checked = true;
                        modes_WAVtoADX.Checked = false;
                    }
                }
            }
        }

        #region Volume Control
        void Volume_5_CheckedChanged(object sender, EventArgs e)
        {
            if (volume_5.Checked == true)
            {
                vol = 5.0; Properties.Settings.Default.volume = 5.0;
                volume_4.Checked = false;
                volume_3.Checked = false;
                volume_2.Checked = false;
                volume_1.Checked = false;
                volume_0.Checked = false;

                //MessageBox.Show("Using a volume greater than 1.0 will likely produce noise in original ADX files.", "Sound Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            Properties.Settings.Default.Save();
        }

        void Volume_4_CheckedChanged(object sender, EventArgs e)
        {
            if (volume_4.Checked == true)
            {
                volume_5.Checked = false;
                vol = 4.0; Properties.Settings.Default.volume = 4.0;
                volume_3.Checked = false;
                volume_2.Checked = false;
                volume_1.Checked = false;
                volume_0.Checked = false;

                //MessageBox.Show("Using a volume greater than 1.0 will likely produce noise in original ADX files.", "Sound Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            Properties.Settings.Default.Save();
        }

        void Volume_3_CheckedChanged(object sender, EventArgs e)
        {
            if (volume_3.Checked == true)
            {
                volume_5.Checked = false;
                volume_4.Checked = false;
                vol = 3.0; Properties.Settings.Default.volume = 3.0;
                volume_2.Checked = false;
                volume_1.Checked = false;
                volume_0.Checked = false;

                //MessageBox.Show("Using a volume greater than 1.0 will likely produce noise in original ADX files.", "Sound Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            Properties.Settings.Default.Save();
        }

        void Volume_2_CheckedChanged(object sender, EventArgs e)
        {
            if (volume_2.Checked == true)
            {
                volume_5.Checked = false;
                volume_4.Checked = false;
                volume_3.Checked = false;
                vol = 2.0; Properties.Settings.Default.volume = 2.0;
                volume_1.Checked = false;
                volume_0.Checked = false;

                //MessageBox.Show("Using a volume greater than 1.0 will likely produce noise in original ADX files.", "Sound Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            Properties.Settings.Default.Save();
        }

        void Volume_1_CheckedChanged(object sender, EventArgs e)
        {
            if (volume_1.Checked == true)
            {
                volume_5.Checked = false;
                volume_4.Checked = false;
                volume_3.Checked = false;
                volume_2.Checked = false;
                vol = 1.0; Properties.Settings.Default.volume = 1.0;
                volume_0.Checked = false;
            }
            Properties.Settings.Default.Save();
        }

        void Volume_0_CheckedChanged(object sender, EventArgs e)
        {
            if (volume_0.Checked == true)
            {
                volume_5.Checked = false;
                volume_4.Checked = false;
                volume_3.Checked = false;
                volume_2.Checked = false;
                volume_1.Checked = false;
                vol = 0.0; Properties.Settings.Default.volume = 0.0;
            }
            Properties.Settings.Default.Save();
        }
        #endregion

        void Looping_Ignore_CheckedChanged(object sender, EventArgs e)
        {
            if (looping_Ignore.Checked == true)
            {
                ignoreLoop = " -lpoff";
                Properties.Settings.Default.ignoreLoop = true;
            }
            else
            {
                ignoreLoop = "";
                Properties.Settings.Default.ignoreLoop = false;
            }
            Properties.Settings.Default.Save();
        }

        void Looping_Remove_CheckedChanged(object sender, EventArgs e)
        {
            if (looping_Remove.Checked == true)
            {
                removeLoop = " -nodelterm";
                Properties.Settings.Default.removeLoop = true;
            }
            else
            {
                removeLoop = "";
                Properties.Settings.Default.removeLoop = false;
            }
            Properties.Settings.Default.Save();
        }

        void Options_DownmixToStereo_CheckedChanged(object sender, EventArgs e)
        {
            if (options_DownmixToMono.Checked == true)
            {
                downmix = "MONO";
                Properties.Settings.Default.downmix = true;
            }
            else
            {
                downmix = "STEREO";
                Properties.Settings.Default.downmix = false;
            }
            Properties.Settings.Default.Save();
        }
    }
}
