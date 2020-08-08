using System;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using Marathon.IO.Formats.Miscellaneous;

namespace Marathon.Toolkit.Forms
{
    public partial class SaveEditor : DockContent
    {
        SaveData SonicNextSaveData = new SaveData();

        public SaveEditor(string file)
        {
            InitializeComponent();

            SonicNextSaveData.Load(file);

            // Sonic's upgrades.
            CheckedListBox_Upgrades.SetItemChecked(0, SonicNextSaveData.Upgrades.equip_lightdash);
            CheckedListBox_Upgrades.SetItemChecked(1, SonicNextSaveData.Upgrades.equip_sliding);
            CheckedListBox_Upgrades.SetItemChecked(2, SonicNextSaveData.Upgrades.equip_boundjump);
            CheckedListBox_Upgrades.SetItemChecked(3, SonicNextSaveData.Upgrades.equip_homingsmash);
            CheckedListBox_Upgrades.SetItemChecked(4, SonicNextSaveData.Upgrades.equip_gem_green);
            CheckedListBox_Upgrades.SetItemChecked(5, SonicNextSaveData.Upgrades.equip_gem_red);
            CheckedListBox_Upgrades.SetItemChecked(6, SonicNextSaveData.Upgrades.equip_gem_blue);
            CheckedListBox_Upgrades.SetItemChecked(7, SonicNextSaveData.Upgrades.equip_gem_white);
            CheckedListBox_Upgrades.SetItemChecked(8, SonicNextSaveData.Upgrades.equip_gem_sky);
            CheckedListBox_Upgrades.SetItemChecked(9, SonicNextSaveData.Upgrades.equip_gem_yellow);
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

            CheckBox_Subtitles.Checked = SonicNextSaveData.Settings.Subtitles;
            NumericUpDown_System_Music.Value  = (decimal)SonicNextSaveData.Settings.Music;
            NumericUpDown_System_Effect.Value = (decimal)SonicNextSaveData.Settings.Effects;
        }

        private void ComboBox_Episode_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (((ComboBox)sender).SelectedIndex)
            {
                // Sonic
                case 0:
                {
                    SetNumericUpDownByEpisodeIndex(0);
                    break;
                }

                // Shadow
                case 1:
                {
                    SetNumericUpDownByEpisodeIndex(1);
                    break;
                }

                // Silver
                case 2:
                {
                    SetNumericUpDownByEpisodeIndex(2);
                    break;
                }

                // Last Episode
                case 3:
                {
                    SetNumericUpDownByEpisodeIndex(3);
                    break;
                }
            }
        }

        private void SetNumericUpDownByEpisodeIndex(int index)
        {
            NumericUpDown_Episode_Lives.Value    = SonicNextSaveData.Episodes[index].Lives;
            NumericUpDown_Episode_Rings.Value    = SonicNextSaveData.Episodes[index].Rings;
            NumericUpDown_Episode_Progress.Value = SonicNextSaveData.Episodes[index].Progress;

            NumericUpDown_Episode_Year.Value   = SonicNextSaveData.Episodes[index].Year;
            NumericUpDown_Episode_Month.Value  = SonicNextSaveData.Episodes[index].Month;
            NumericUpDown_Episode_Day.Value    = SonicNextSaveData.Episodes[index].Day;
            NumericUpDown_Episode_Hour.Value   = SonicNextSaveData.Episodes[index].Hour;
            NumericUpDown_Episode_Minute.Value = SonicNextSaveData.Episodes[index].Minute;
        }

        private void MenuStripDark_Main_File_Save_Click(object sender, EventArgs e)
        {
            // TODO: Save stuff.
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
                // TODO: Save stuff.
            }
        }

        private void MenuStripDark_Main_File_Close_Click(object sender, EventArgs e) => Close();
    }
}
