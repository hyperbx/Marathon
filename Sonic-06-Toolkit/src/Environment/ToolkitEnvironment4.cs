using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace Toolkit.Environment4
{
    public partial class ToolkitEnvironment4 : Form
    {
        public static readonly string VersionNumber = "Prototype 4.0";

        public ToolkitEnvironment4() {
            InitializeComponent();
            UnloadTabs();

            Version.Text = VersionNumber;
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
            SectionButton section = sender as SectionButton;
            if (sender == Section_General) Navigator_Preferences.SelectedTab = Submenu_General;
            else if (sender == Section_Appearance) Navigator_Preferences.SelectedTab = Submenu_Appearance;
            else if (sender == Section_About) Navigator_Preferences.SelectedTab = Submenu_About;
        }

        private void Help_About_Click(object sender, EventArgs e) {
            LoadAndFocusTab(Tab_Preferences);
            Navigator_Preferences.SelectedTab = Submenu_About;
        }

        /// <summary>
        /// Checks what link is clicked and directs the user to their page.
        /// </summary>
        private void Link_User_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            if (sender == Link_Hyper) Process.Start("https://www.github.com/HyperPolygon64");
            else if (sender == Link_GerbilSoft) Process.Start("https://www.github.com/GerbilSoft");
            else if (sender == Link_Sable) Process.Start("https://www.twitter.com/nectarhime");
            else if (sender == Link_Natsumi) Process.Start("https://www.github.com/NatsumiFox");
            else if (sender == Link_ShadowLAG) Process.Start("https://www.github.com/lllsondowlll");
            else if (sender == Link_SEGACarnival) Process.Start("https://www.segacarnival.com/");
            else if (sender == Link_Nonami) Process.Start("https://www.youtube.com/channel/UC35wsF1NUwoUWmw2DLz6uJg");
            else if (sender == Link_Reimous) Process.Start("https://www.youtube.com/channel/UC3ACu6igwlAIckO9Gg2i4PA");
            else if (sender == Link_Radfordhound) Process.Start("https://www.github.com/Radfordhound");
            else if (sender == Link_Skyth) Process.Start("https://www.github.com/blueskythlikesclouds");
            else if (sender == Link_Sajid) Process.Start("https://www.github.com/Sajidur78");
            else if (sender == Link_DarioSamo) Process.Start("https://www.github.com/DarioSamo");
            else if (sender == Link_xorloser) Process.Start("http://www.xorloser.com/");
        }

        private void File_Exit_Click(object sender, EventArgs e) { Application.Exit(); }
    }
}
