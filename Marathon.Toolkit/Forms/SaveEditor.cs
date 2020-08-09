using System;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using Marathon.IO.Formats.Miscellaneous;
using System.Collections.Generic;
using Marathon.Toolkit.Components;
using System.Linq;

namespace Marathon.Toolkit.Forms
{
    public partial class SaveEditor : DockContent
    {
        private string _LoadedFile;
        private bool LifeCountClampWarningShown = true;
        private SaveData SonicNextSaveData = new SaveData();
        private List<SaveData.Episode> Episodes = new List<SaveData.Episode>();

        public SaveEditor(string file)
        {
            InitializeComponent();

            // Stores the file path for saving.
            _LoadedFile = file;

            // Load the input file as save data.
            SonicNextSaveData.Load(file);

            // Sonic's upgrades.
            CheckedListBox_Upgrades.SetItemChecked(0,  SonicNextSaveData.Upgrades.equip_lightdash);
            CheckedListBox_Upgrades.SetItemChecked(1,  SonicNextSaveData.Upgrades.equip_sliding);
            CheckedListBox_Upgrades.SetItemChecked(2,  SonicNextSaveData.Upgrades.equip_boundjump);
            CheckedListBox_Upgrades.SetItemChecked(3,  SonicNextSaveData.Upgrades.equip_homingsmash);
            CheckedListBox_Upgrades.SetItemChecked(4,  SonicNextSaveData.Upgrades.equip_gem_green);
            CheckedListBox_Upgrades.SetItemChecked(5,  SonicNextSaveData.Upgrades.equip_gem_red);
            CheckedListBox_Upgrades.SetItemChecked(6,  SonicNextSaveData.Upgrades.equip_gem_blue);
            CheckedListBox_Upgrades.SetItemChecked(7,  SonicNextSaveData.Upgrades.equip_gem_white);
            CheckedListBox_Upgrades.SetItemChecked(8,  SonicNextSaveData.Upgrades.equip_gem_sky);
            CheckedListBox_Upgrades.SetItemChecked(9,  SonicNextSaveData.Upgrades.equip_gem_yellow);
            CheckedListBox_Upgrades.SetItemChecked(10, SonicNextSaveData.Upgrades.equip_gem_purple);
            CheckedListBox_Upgrades.SetItemChecked(11, SonicNextSaveData.Upgrades.equip_gem_super);

            // Shadow's upgrades.
            CheckedListBox_Upgrades.SetItemChecked(12, SonicNextSaveData.Upgrades.equip_shadow_lightdash);
            CheckedListBox_Upgrades.SetItemChecked(13, SonicNextSaveData.Upgrades.equip_shadow_boost_lv1);
            CheckedListBox_Upgrades.SetItemChecked(14, SonicNextSaveData.Upgrades.equip_shadow_boost_lv2);
            CheckedListBox_Upgrades.SetItemChecked(15, SonicNextSaveData.Upgrades.equip_shadow_boost_lv3);

            // Silver's upgrades.
            CheckedListBox_Upgrades.SetItemChecked(16, SonicNextSaveData.Upgrades.equip_silver_holdsmash);
            CheckedListBox_Upgrades.SetItemChecked(17, SonicNextSaveData.Upgrades.equip_silver_catch_all);
            CheckedListBox_Upgrades.SetItemChecked(18, SonicNextSaveData.Upgrades.equip_silver_teleport);
            CheckedListBox_Upgrades.SetItemChecked(19, SonicNextSaveData.Upgrades.equip_silver_psychoshock);
            CheckedListBox_Upgrades.SetItemChecked(20, SonicNextSaveData.Upgrades.equip_silver_speedup);

            // Set system settings.
            CheckBox_Subtitles.Checked        = SonicNextSaveData.Settings.Subtitles;
            NumericUpDown_System_Music.Value  = (decimal)SonicNextSaveData.Settings.Music;
            NumericUpDown_System_Effect.Value = (decimal)SonicNextSaveData.Settings.Effects;

            // Default to Sonic's Episode.
            ComboBox_Episode.SelectedIndex = 0;

            // Set to false so we don't see the warning on launch.
            LifeCountClampWarningShown = false;

            // Allow double-clicking to edit items.
            ListViewDark_Information.MouseDoubleClick += (sender, e) => ((ListViewDark)sender).GetItemAt(e.X, e.Y).BeginEdit();

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
            // Sonic's upgrades.
            SonicNextSaveData.Upgrades.equip_lightdash   = CheckedListBox_Upgrades.GetItemChecked(0);
            SonicNextSaveData.Upgrades.equip_sliding     = CheckedListBox_Upgrades.GetItemChecked(1);
            SonicNextSaveData.Upgrades.equip_boundjump   = CheckedListBox_Upgrades.GetItemChecked(2);
            SonicNextSaveData.Upgrades.equip_homingsmash = CheckedListBox_Upgrades.GetItemChecked(3);
            SonicNextSaveData.Upgrades.equip_gem_green   = CheckedListBox_Upgrades.GetItemChecked(4);
            SonicNextSaveData.Upgrades.equip_gem_red     = CheckedListBox_Upgrades.GetItemChecked(5);
            SonicNextSaveData.Upgrades.equip_gem_blue    = CheckedListBox_Upgrades.GetItemChecked(6);
            SonicNextSaveData.Upgrades.equip_gem_white   = CheckedListBox_Upgrades.GetItemChecked(7);
            SonicNextSaveData.Upgrades.equip_gem_sky     = CheckedListBox_Upgrades.GetItemChecked(8);
            SonicNextSaveData.Upgrades.equip_gem_yellow  = CheckedListBox_Upgrades.GetItemChecked(9);
            SonicNextSaveData.Upgrades.equip_gem_purple  = CheckedListBox_Upgrades.GetItemChecked(10);
            SonicNextSaveData.Upgrades.equip_gem_super   = CheckedListBox_Upgrades.GetItemChecked(11);

            // Shadow's upgrades.
            SonicNextSaveData.Upgrades.equip_shadow_lightdash = CheckedListBox_Upgrades.GetItemChecked(12);
            SonicNextSaveData.Upgrades.equip_shadow_boost_lv1 = CheckedListBox_Upgrades.GetItemChecked(13);
            SonicNextSaveData.Upgrades.equip_shadow_boost_lv2 = CheckedListBox_Upgrades.GetItemChecked(14);
            SonicNextSaveData.Upgrades.equip_shadow_boost_lv3 = CheckedListBox_Upgrades.GetItemChecked(15);

            // Silver's upgrades.
            SonicNextSaveData.Upgrades.equip_silver_holdsmash   = CheckedListBox_Upgrades.GetItemChecked(16);
            SonicNextSaveData.Upgrades.equip_silver_catch_all   = CheckedListBox_Upgrades.GetItemChecked(17);
            SonicNextSaveData.Upgrades.equip_silver_teleport    = CheckedListBox_Upgrades.GetItemChecked(18);
            SonicNextSaveData.Upgrades.equip_silver_psychoshock = CheckedListBox_Upgrades.GetItemChecked(19);
            SonicNextSaveData.Upgrades.equip_silver_speedup     = CheckedListBox_Upgrades.GetItemChecked(20);

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
            SonicNextSaveData.Episodes[CurrentEpisodeIndex].Location = ListViewDark_Information.Items.Cast<ListViewItem>().Where(x => (string)x.Tag == "Location").Single().Text;
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
