using System;
using Ookii.Dialogs;
using System.Drawing;
using Microsoft.Win32;
using System.Diagnostics;
using System.Windows.Forms;

namespace Toolkit.Environment4
{
    public partial class ToolkitEnvironment4 : Form
    {
        public static readonly string VersionNumber = "Prototype 4.0";

        public ToolkitEnvironment4() {
            InitializeComponent();
            LoadSettings();
            UnloadTabs();

            Button_Label_Version.Text = Label_Version.Text = VersionNumber;
            Size = MinimumSize;
        }

        /// <summary>
        /// Loads all settings from the user config.
        /// </summary>
        private void LoadSettings() {
            // Restore text box strings.
            if (Properties.Settings.Default.DefaultDirectory != string.Empty)
                ExplorerBrowser_StartPage.Navigate(TextBox_DefaultDirectory.Text = Properties.Settings.Default.DefaultDirectory);

            // Restore check box states.
            CheckBox_AutoColour.Checked = Properties.Settings.Default.AutoColour;
            if (CheckBox_HighContrastText.Checked = Properties.Settings.Default.HighContrastText)
                Toolkit_TabControl.SelectedTextColor = SystemColors.ControlText;
            else
                Toolkit_TabControl.SelectedTextColor = SystemColors.Control;

            // Set controls to AccentColour setting.
            Button_ColourPicker_Preview.FlatAppearance.MouseOverBackColor =
            Button_ColourPicker_Preview.FlatAppearance.MouseDownBackColor =
            Preferences_Section_Appearance.AccentColour =
            Toolkit_TabControl.HorizontalLineColor =
            Button_ColourPicker_Preview.BackColor =
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

        /// <summary>
        /// Takes click control from all section buttons and switches the navigator control.
        /// </summary>
        private void Section_Click(object sender, EventArgs e) {
            foreach (Control control in Container_Preferences.Panel1.Controls)
                if (control is SectionButton) ((SectionButton)control).SelectedSection = false;

            if (sender == Preferences_Section_General) Navigator_Preferences.SelectedTab = Submenu_General;
            else if (sender == Preferences_Section_Appearance) Navigator_Preferences.SelectedTab = Submenu_Appearance;
            else if (sender == Preferences_Section_About) Navigator_Preferences.SelectedTab = Submenu_About;
            ((SectionButton)sender).SelectedSection = true;
        }

        private void Help_About_Click(object sender, EventArgs e) {
            foreach (Control control in Container_Preferences.Panel1.Controls)
                if (control is SectionButton) ((SectionButton)control).SelectedSection = false;

            LoadAndFocusTab(Tab_Preferences);
            Navigator_Preferences.SelectedTab = Submenu_About;
            Preferences_Section_About.SelectedSection = true;
        }

        /// <summary>
        /// Checks what link is clicked and directs the user to their page.
        /// </summary>
        private void Link_User_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            if (sender == LinkLabel_Hyper) Process.Start("https://www.github.com/HyperPolygon64");
            else if (sender == LinkLabel_GerbilSoft) Process.Start("https://www.github.com/GerbilSoft");
            else if (sender == LinkLabel_Sable) Process.Start("https://www.twitter.com/nectarhime");
            else if (sender == LinkLabel_Natsumi) Process.Start("https://www.github.com/NatsumiFox");
            else if (sender == LinkLabel_ShadowLAG) Process.Start("https://www.github.com/lllsondowlll");
            else if (sender == LinkLabel_SEGACarnival) Process.Start("https://www.segacarnival.com/");
            else if (sender == LinkLabel_Nonami) Process.Start("https://www.youtube.com/channel/UC35wsF1NUwoUWmw2DLz6uJg");
            else if (sender == LinkLabel_Reimous) Process.Start("https://www.youtube.com/channel/UC3ACu6igwlAIckO9Gg2i4PA");
            else if (sender == LinkLabel_Radfordhound) Process.Start("https://www.github.com/Radfordhound");
            else if (sender == LinkLabel_Skyth) Process.Start("https://www.github.com/blueskythlikesclouds");
            else if (sender == LinkLabel_Sajid) Process.Start("https://www.github.com/Sajidur78");
            else if (sender == LinkLabel_DarioSamo) Process.Start("https://www.github.com/DarioSamo");
            else if (sender == LinkLabel_xorloser) Process.Start("http://www.xorloser.com/");
        }

        private void File_Exit_Click(object sender, EventArgs e) { Application.Exit(); }

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

        private void Button_Colour_MouseEnter(object sender, EventArgs e) { ((Button)sender).FlatAppearance.BorderSize = 1; }

        private void Button_Colour_MouseLeave(object sender, EventArgs e) { ((Button)sender).FlatAppearance.BorderSize = 0; }

        private void Button_Colour_Click(object sender, EventArgs e) {
            Preferences_Section_Appearance.AccentColour = Properties.Settings.Default.AccentColour = ((Button)sender).BackColor;
            Properties.Settings.Default.Save();
            LoadSettings();
        }

        private void Section_Appearance_ColourPicker_Click(object sender, EventArgs e) {
            ColorDialog accentPicker = new ColorDialog {
                FullOpen = true,
                Color = Properties.Settings.Default.AccentColour
            };

            if (accentPicker.ShowDialog() == DialogResult.OK) {
                Properties.Settings.Default.AccentColour = accentPicker.Color;
                Section_Appearance_ColourPicker.Text = $"Custom colour (#{(accentPicker.Color.ToArgb() & 0x00FFFFFF).ToString("X6")})";
                Properties.Settings.Default.Save();
                LoadSettings();
            }
        }

        private void Button_ColourPicker_Default_Click(object sender, EventArgs e) {
            Properties.Settings.Default.AccentColour = Color.FromArgb(186, 0, 0);
            Properties.Settings.Default.Save();
            LoadSettings();
        }

        private void CheckBox_AutoColour_CheckedChanged(object sender, EventArgs e) {
            if (CheckBox_AutoColour.Checked) {
                int RegistryColour = (int)Registry.GetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationColor", null);
                Properties.Settings.Default.AccentColour = Color.FromArgb(RegistryColour);
            } else Properties.Settings.Default.AccentColour = Color.FromArgb(186, 0, 0);
            Properties.Settings.Default.AutoColour = CheckBox_AutoColour.Checked;
            Properties.Settings.Default.Save();
            LoadSettings();
        }

        private void ToolkitEnvironment4_Resize(object sender, EventArgs e) {
            if (Width > 980) Label_Description_FileAssociations.Text = "Edits the Windows Registry to change associations for these file types.";
            else if (Width < 980) Label_Description_FileAssociations.Text = "Edits the Windows Registry to change associations for\nthese file types.";
        }

        private void Tools_Click(object sender, EventArgs e) {
            if (sender == Tools_SonicSoundStudio) LoadAndFocusTab(Tab_SonicSoundStudio);
        }

        private void CheckBox_HighContrastText_CheckedChanged(object sender, EventArgs e) {
            Properties.Settings.Default.HighContrastText = CheckBox_HighContrastText.Checked;
            Properties.Settings.Default.Save();
            LoadSettings();
        }
    }
}
