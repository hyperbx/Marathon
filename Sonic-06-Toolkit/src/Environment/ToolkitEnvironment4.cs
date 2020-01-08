using System;
using System.Drawing;
using System.Windows.Forms;

namespace Toolkit.Environment4
{
    public partial class ToolkitEnvironment4 : Form
    {
        // Variables

        public static readonly string VersionNumber = "Prototype 4.0";

        // General

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

        // Preferences

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
    }
}
