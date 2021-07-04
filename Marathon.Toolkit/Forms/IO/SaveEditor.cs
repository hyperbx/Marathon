// Marathon is licensed under the MIT License:
/* 
 * MIT License
 * 
 * Copyright (c) 2021 HyperBE32
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
using System.Linq;
using System.Windows.Forms;
using Marathon.Components;
using Marathon.IO.Formats.Miscellaneous;

namespace Marathon.Toolkit.Forms
{
    public partial class SaveEditor : MarathonDockContent
    {
        private string _LoadedFile;                          // Path to the current loaded file.
        private bool LifeCountClampWarningShown = true;      // Flag to trip when going over 99 lives.
        private SonicNextSaveData SonicNextSaveData = new SonicNextSaveData(); // Stored save data properties.

        public SaveEditor(string file)
        {
            InitializeComponent();

            // Stores the file path for saving.
            _LoadedFile = file;

            // Load the input file as save data.
            SonicNextSaveData.Load(file);

            // TODO: iterate through booleans and set UI.
            /* Light Dash (Sonic)
               Antigravity
               Bounce Bracelet
               Homing Smash
               Green Gem
               Red Gem
               Blue Gem
               White Gem
               Sky Gem
               Yellow Gem
               Purple Gem
               Rainbow Gem
               Light Dash (Shadow)
               Chaos Boost [Level 1]
               Chaos Boost [Level 2]
               Chaos Boost [Level 3]
               Hold Smash
               Catch All
               Teleport
               Psychoshock
               Speed Up
             */

            // Set system settings.
            CheckBox_Subtitles.Checked        = SonicNextSaveData.Settings.Subtitles;
            NumericUpDown_System_Music.Value  = (decimal)SonicNextSaveData.Settings.Music;
            NumericUpDown_System_Effect.Value = (decimal)SonicNextSaveData.Settings.Effects;

            for (int i = 0; i < SonicNextSaveData.Missions.Count; i++)
            {
                SonicNextSaveData.Mission mission = SonicNextSaveData.Missions[i];

                TreeNode missionNode      = new TreeNode(i.ToString()),
                         missionFlagNode  = new TreeNode(mission.UnknownInt32_1.ToString()),
                         missionRankNode  = new TreeNode(mission.Rank.ToString()),
                         missionTimeNode  = new TreeNode(mission.Time.ToString()),
                         missionScoreNode = new TreeNode(mission.Score.ToString()),
                         missionRingsNode = new TreeNode(mission.Rings.ToString());

                missionNode.Nodes.AddRange
                (
                    new[]
                    {
                        missionFlagNode,
                        missionRankNode,
                        missionTimeNode,
                        missionScoreNode,
                        missionRingsNode
                    }
                );

                TreeView_Missions.Nodes.Add(missionNode);
            }

            // Default to Sonic's Episode.
            ComboBox_Episode.SelectedIndex = 0;

            // Set to false so we don't see the warning on launch.
            LifeCountClampWarningShown = false;

            // Allow double-clicking to edit items.
            ListViewDark_Information.MouseDoubleClick += (sender, e) =>
                ((ListViewDark)sender).GetItemAt(e.X, e.Y).BeginEdit();

            // Update the cache after editing labels.
            ListViewDark_Information.AfterLabelEdit += delegate { UpdateCache(); };
        }

        private void ComboBox_Episode_SelectedIndexChanged(object sender, EventArgs e)
        {
            void SetByEpisodeIndex(int index)
            {
                // Define main properties.
                NumericUpDown_Episode_Lives.Value    = SonicNextSaveData.Episodes[index].Lives;
                NumericUpDown_Episode_Rings.Value    = SonicNextSaveData.Episodes[index].Rings;
                NumericUpDown_Episode_Progress.Value = SonicNextSaveData.Episodes[index].Progress;

                // Define save time.
                NumericUpDown_Episode_Year.Value   = SonicNextSaveData.Episodes[index].Year;
                NumericUpDown_Episode_Month.Value  = SonicNextSaveData.Episodes[index].Month;
                NumericUpDown_Episode_Day.Value    = SonicNextSaveData.Episodes[index].Day;
                NumericUpDown_Episode_Hour.Value   = SonicNextSaveData.Episodes[index].Hour;
                NumericUpDown_Episode_Minute.Value = SonicNextSaveData.Episodes[index].Minute;

                // Clear the ListView control.
                ListViewDark_Information.Items.Clear();

                // Add location information.
                for (int i = 0; i < SonicNextSaveData.Episodes[index].Information.Count; i++)
                    ListViewDark_Information.Items.Add(new ListViewItem(SonicNextSaveData.Episodes[index].Information[i].Name));

                // Add location entry.
                ListViewDark_Information.Items.Add(new ListViewItem(SonicNextSaveData.Episodes[index].Location) { Tag = "Location" });

                // Update cache to store current index properties.
                UpdateCache();
            }

            switch (((ComboBox)sender).SelectedIndex)
            {
                // Sonic
                case 0:
                {
                    SetByEpisodeIndex(0);

                    break;
                }

                // Shadow
                case 1:
                {
                    SetByEpisodeIndex(1);

                    break;
                }

                // Silver
                case 2:
                {
                    SetByEpisodeIndex(2);

                    break;
                }

                // Last Episode
                case 3:
                {
                    SetByEpisodeIndex(3);

                    break;
                }
            }
        }

        private void MenuStripDark_Main_File_Save_Click(object sender, EventArgs e)
        {
            UpdateCache();

            SonicNextSaveData.Save(_LoadedFile);
        }

        private void MenuStripDark_Main_File_SaveAs_Click(object sender, EventArgs e)
        {
            // Save save save save save save save save data...
            SaveFileDialog saveSaveData = new SaveFileDialog()
            {
                DefaultExt = ".bin",
                Title = "Save As..."
            };

            if (saveSaveData.ShowDialog() == DialogResult.OK)
            {
                UpdateCache();

                SonicNextSaveData.Save(saveSaveData.FileName);
            }
        }

        private void UpdateCache()
        {
            // Set system settings.
            SonicNextSaveData.Settings.Subtitles = CheckBox_Subtitles.Checked;
            SonicNextSaveData.Settings.Music     = (float)NumericUpDown_System_Music.Value;
            SonicNextSaveData.Settings.Effects   = (float)NumericUpDown_System_Effect.Value;

            int CurrentEpisodeIndex = ComboBox_Episode.SelectedIndex;

            // Define main properties.
            SonicNextSaveData.Episodes[CurrentEpisodeIndex].Lives    = (int)NumericUpDown_Episode_Lives.Value;
            SonicNextSaveData.Episodes[CurrentEpisodeIndex].Rings    = (int)NumericUpDown_Episode_Rings.Value;
            SonicNextSaveData.Episodes[CurrentEpisodeIndex].Progress = (int)NumericUpDown_Episode_Progress.Value;

            // Define save time.
            SonicNextSaveData.Episodes[CurrentEpisodeIndex].Year   = (short)NumericUpDown_Episode_Year.Value;
            SonicNextSaveData.Episodes[CurrentEpisodeIndex].Month  = (sbyte)NumericUpDown_Episode_Month.Value;
            SonicNextSaveData.Episodes[CurrentEpisodeIndex].Day    = (sbyte)NumericUpDown_Episode_Day.Value;
            SonicNextSaveData.Episodes[CurrentEpisodeIndex].Hour   = (sbyte)NumericUpDown_Episode_Hour.Value;
            SonicNextSaveData.Episodes[CurrentEpisodeIndex].Minute = (sbyte)NumericUpDown_Episode_Minute.Value;

            // Define stage information.
            for (int i = 0; i < SonicNextSaveData.Episodes[CurrentEpisodeIndex].Information.Count; i++)
                SonicNextSaveData.Episodes[CurrentEpisodeIndex].Information[i].Name = ListViewDark_Information.Items[i].Text;

            // Define location entry.
            SonicNextSaveData.Episodes[CurrentEpisodeIndex].Location =
                ListViewDark_Information.Items.Cast<ListViewItem>().Where(x => (string)x.Tag == "Location").Single().Text;
        }

        private void MenuStripDark_Main_File_Close_Click(object sender, EventArgs e) => Close();

        private void NumericUpDown_Episode_Lives_ValueChanged(object sender, EventArgs e)
        {
            if (NumericUpDown_Episode_Lives.Value == 100 && !LifeCountClampWarningShown)
            {
                MarathonMessageBox.Show("Life count may surpass 99, but it will clamp back to 99 upon saving your game.", "Life Count Clamping");

                LifeCountClampWarningShown = true;
            }
        }
    }
}
