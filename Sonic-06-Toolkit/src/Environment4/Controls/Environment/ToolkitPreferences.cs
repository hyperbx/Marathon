using System;
using Ookii.Dialogs;
using System.Drawing;
using Microsoft.Win32;
using System.Windows.Forms;
using System.ComponentModel;

// Sonic '06 Toolkit is licensed under the MIT License:
/*
 * MIT License

 * Copyright (c) 2020 Gabriel (HyperPolygon64)

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

namespace Toolkit.Environment4
{
    public partial class ToolkitPreferences : UserControl
    {
        public ToolkitPreferences() {
            InitializeComponent();
            LoadSettings();

            Properties.Settings.Default.SettingsSaving += Settings_SettingsSaving;
            Preferences_TabControl.Height += 23;
        }

        private void Settings_SettingsSaving(object sender, CancelEventArgs e) { LoadSettings(); }

        private void LoadSettings() {
            // Restore text box strings.
            TextBox_DefaultDirectory.Text = Properties.Settings.Default.DefaultDirectory;

            // Restore check box states.
            CheckBox_AutoColour.Checked = Properties.Settings.Default.AutoColour;
            CheckBox_HighContrastText.Checked = Properties.Settings.Default.HighContrastText;

            // Set controls to AccentColour setting.
            Button_ColourPicker_Preview.FlatAppearance.MouseOverBackColor =
            Button_ColourPicker_Preview.FlatAppearance.MouseDownBackColor =
            Preferences_Section_Appearance.AccentColour =
            Button_ColourPicker_Preview.BackColor = 
            Properties.Settings.Default.AccentColour;
        }

        public int SelectedIndex {
            get { return Preferences_TabControl.SelectedIndex; }
            set { 
                Preferences_TabControl.SelectedIndex = value;

                if (value == 2) {
                    foreach (Control control in Controls)
                        if (control is SectionButton) ((SectionButton)control).SelectedSection = false;
                    Preferences_TabControl.Visible = true;
                    Preferences_Section_About.SelectedSection = true;
                }
            }
        }

        /// <summary>
        /// Takes click control from all section buttons and switches the navigator control.
        /// </summary>
        private void Preferences_Section_Click(object sender, EventArgs e) {
            foreach (Control control in Controls)
                if (control is SectionButton) ((SectionButton)control).SelectedSection = false;

            if (sender == Preferences_Section_General) Preferences_TabControl.SelectedTab = Tab_Preferences_General;
            else if (sender == Preferences_Section_Appearance) Preferences_TabControl.SelectedTab = Tab_Preferences_Appearance;
            else if (sender == Preferences_Section_About) Preferences_TabControl.SelectedTab = Tab_Preferences_About;
            Preferences_TabControl.Visible = ((SectionButton)sender).SelectedSection = true;
        }

        private void Button_DefaultDirectory_Click(object sender, EventArgs e) {
            VistaFolderBrowserDialog browseDefault = new VistaFolderBrowserDialog() {
                Description = "Please select a folder...",
                UseDescriptionForTitle = true
            };

            if (browseDefault.ShowDialog() == DialogResult.OK) {
                Properties.Settings.Default.DefaultDirectory = TextBox_DefaultDirectory.Text = browseDefault.SelectedPath;
                Properties.Settings.Default.Save();
            }
        }

        private void WindowsColourPicker_AccentColour_ButtonClick(object sender, EventArgs e) {
            Properties.Settings.Default.AccentColour = ((Button)sender).BackColor;
            Properties.Settings.Default.Save();
        }

        private void Preferences_TabControl_SelectedIndexChanged(object sender, EventArgs e) { Preferences_TabControl.SelectedTab.VerticalScroll.Value = 0; }

        private void CheckBox_AutoColour_CheckedChanged(object sender, EventArgs e) {
            if (CheckBox_AutoColour.Checked) {
                int RegistryColour = (int)Registry.GetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationColor", null);
                Properties.Settings.Default.AccentColour = Color.FromArgb(RegistryColour);
            } else Properties.Settings.Default.AccentColour = Color.FromArgb(186, 0, 0);
            Properties.Settings.Default.AutoColour = CheckBox_AutoColour.Checked;
            Properties.Settings.Default.Save();
        }

        private void CheckBox_HighContrastText_CheckedChanged(object sender, EventArgs e) {
            Properties.Settings.Default.HighContrastText = CheckBox_HighContrastText.Checked;
            Properties.Settings.Default.Save();
        }

        private void Button_ColourPicker_Preview_MouseEnter(object sender, EventArgs e) { Section_Appearance_ColourPicker.BackColor = Color.FromArgb(48, 48, 51); }

        private void Button_ColourPicker_Preview_MouseLeave(object sender, EventArgs e) { Section_Appearance_ColourPicker.BackColor = Color.FromArgb(42, 42, 45); }

        private void Button_ColourPicker_Preview_MouseDown(object sender, MouseEventArgs e) { Section_Appearance_ColourPicker.BackColor = Color.FromArgb(58, 58, 61); }

        private void Button_ColourPicker_Preview_MouseUp(object sender, MouseEventArgs e) {
            Section_Appearance_ColourPicker.BackColor = Color.FromArgb(48, 48, 51);
            Section_Appearance_ColourPicker_Click(sender, e);
        }

        private void Section_Appearance_ColourPicker_Click(object sender, EventArgs e) {
            ColorDialog accentPicker = new ColorDialog {
                FullOpen = true,
                Color = Properties.Settings.Default.AccentColour
            };

            if (accentPicker.ShowDialog() == DialogResult.OK) {
                Properties.Settings.Default.AccentColour = accentPicker.Color;
                Properties.Settings.Default.Save();
            }
        }

        private void Button_ColourPicker_Default_Click(object sender, EventArgs e) {
            Properties.Settings.Default.AccentColour = Color.FromArgb(186, 0, 0);
            Properties.Settings.Default.Save();
        }

        private void ToolkitPreferences_Resize(object sender, EventArgs e) {
            if (Width > 980) Label_Description_FileAssociations.Text = "Edits the Windows Registry to change associations for these file types.";
            else if (Width < 980) Label_Description_FileAssociations.Text = "Edits the Windows Registry to change associations for\nthese file types.";
        }

        /// <summary>
        /// Checks what link is clicked and directs the user to their page.
        /// </summary>
        //private void Link_User_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
        //    if (sender == LinkLabel_Hyper) Process.Start("https://www.github.com/HyperPolygon64");
        //    else if (sender == LinkLabel_GerbilSoft) Process.Start("https://www.github.com/GerbilSoft");
        //    else if (sender == LinkLabel_Sable) Process.Start("https://www.twitter.com/nectarhime");
        //    else if (sender == LinkLabel_Natsumi) Process.Start("https://www.github.com/NatsumiFox");
        //    else if (sender == LinkLabel_ShadowLAG) Process.Start("https://www.github.com/lllsondowlll");
        //    else if (sender == LinkLabel_SEGACarnival) Process.Start("https://www.segacarnival.com/");
        //    else if (sender == LinkLabel_Nonami) Process.Start("https://www.youtube.com/channel/UC35wsF1NUwoUWmw2DLz6uJg");
        //    else if (sender == LinkLabel_Reimous) Process.Start("https://www.youtube.com/channel/UC3ACu6igwlAIckO9Gg2i4PA");
        //    else if (sender == LinkLabel_Radfordhound) Process.Start("https://www.github.com/Radfordhound");
        //    else if (sender == LinkLabel_Skyth) Process.Start("https://www.github.com/blueskythlikesclouds");
        //    else if (sender == LinkLabel_Sajid) Process.Start("https://www.github.com/Sajidur78");
        //    else if (sender == LinkLabel_DarioSamo) Process.Start("https://www.github.com/DarioSamo");
        //    else if (sender == LinkLabel_xorloser) Process.Start("http://www.xorloser.com/");
        //}
    }
}
