using System;
using System.Drawing;
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
    public partial class ToolkitEnvironment4 : Form
    {
        public static readonly string VersionNumber = "Prototype 4.0";

        public ToolkitEnvironment4() {
            InitializeComponent();
            LoadSettings();
            UnloadTabs();

            Properties.Settings.Default.SettingsSaving += Settings_SettingsSaving;
            Button_Label_Version.Text = VersionNumber;
            Toolkit_TabControl.AllowDragging = true;
            Size = MinimumSize;
        }

        private void Settings_SettingsSaving(object sender, CancelEventArgs e) { LoadSettings(); }

        /// <summary>
        /// Loads all settings from the user config.
        /// </summary>
        private void LoadSettings() {
            // Load default directory.
            if (Properties.Settings.Default.DefaultDirectory != string.Empty)
                ExplorerBrowser_StartPage.Navigate(Properties.Settings.Default.DefaultDirectory);

            // Set controls to HighContrastText setting.
            if (Properties.Settings.Default.HighContrastText)
                Toolkit_TabControl.SelectedTextColor = SystemColors.ControlText;
            else
                Toolkit_TabControl.SelectedTextColor = SystemColors.Control;

            // Set controls to AccentColour setting.
            Toolkit_TabControl.HorizontalLineColor =
            Toolkit_TabControl.ActiveColor =
            StatusStrip_Main.BackColor =
            Label_Status.BackColor =
            Properties.Settings.Default.AccentColour;

            Toolkit_TabControl.Refresh(); // Clears up any issues with the software renderer.
        }

        /// <summary>
        /// Unloads all tabs to reset the tab control.
        /// </summary>
        private void UnloadTabs() {
            Toolkit_TabControl.TabPages.Clear();
            Toolkit_TabControl.TabPages.Add(Tab_StartPage);
        }

        /// <summary>
        /// Loads a pre-made tab and focuses it.
        /// </summary>
        private void LoadAndFocusTab(TabPage tab) {
            if (!Toolkit_TabControl.TabPages.Contains(tab)) Toolkit_TabControl.TabPages.Add(tab);
            Toolkit_TabControl.SelectedTab = tab;
        }

        private void Button_Preferences_Click(object sender, EventArgs e) { LoadAndFocusTab(Tab_Preferences); }

        private void Toolkit_TabControl_SelectedIndexChanged(object sender, EventArgs e) {
            Toolkit_TabControl.Refresh(); // Clears up any issues with the software renderer.
        }

        private void Help_Click(object sender, EventArgs e) {
            if (sender == Help_Documentation) {
                // Load Documentation tab when ready.
            } else if (sender == Help_CheckForUpdates) {
                // Load Updater tab when ready.
            } else if (sender == Help_About) {
                foreach (Control control in Tab_Preferences.Controls)
                    if (control is SectionButton) ((SectionButton)control).SelectedSection = false;

                LoadAndFocusTab(Tab_Preferences);
                Toolkit_Preferences.SelectedIndex = 2;
            }
        }

        private void File_Exit_Click(object sender, EventArgs e) { Application.Exit(); }

        private void Tools_Click(object sender, EventArgs e) {
            if (sender == Tools_ArchiveMerger) LoadAndFocusTab(Tab_ArchiveMerger);
            else if (sender == Tools_CollisionGenerator) LoadAndFocusTab(Tab_CollisionGenerator);
            else if (sender == Tools_ContainerGenerator) LoadAndFocusTab(Tab_ContainerGenerator);
            else if (sender == Tools_ExecutableTweaker) LoadAndFocusTab(Tab_ExecutableTweaker);
            else if (sender == Tools_LuaCompilation) LoadAndFocusTab(Tab_LuaCompilation);
            else if (sender == Tools_SEGANNConverter) LoadAndFocusTab(Tab_SEGANNConverter);
            else if (sender == Tools_PlacementConverter) LoadAndFocusTab(Tab_PlacementConverter);
            else if (sender == Tools_SonicSoundStudio) LoadAndFocusTab(Tab_SonicSoundStudio);
            else if (sender == Tools_TextureConverter) LoadAndFocusTab(Tab_TextureConverter);
            else if (sender == Tools_TextEncoding) LoadAndFocusTab(Tab_TextEncoding);
            else if (sender == Tools_Xbox360ISOExtractor) LoadAndFocusTab(Tab_Xbox360ISOExtractor);
        }
    }
}
