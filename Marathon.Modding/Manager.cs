// Marathon is licensed under the MIT License:
/* 
 * MIT License
 * 
 * Copyright (c) 2020 HyperBE32
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
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Marathon.Components;
using Marathon.IO.Formats.SetInStone;

namespace Marathon.Modding
{
    public partial class Manager : MarathonDockContent
    {
        public Manager()
        {
            InitializeComponent();

            // Restore last details pane settings.
            switch (Marathon.Properties.Settings.Default.Modding_ManagerDetailsVisible)
            {
                case true:
                {
                    KryptonSplitContainer_Preview.Panel2Collapsed = false;
                    KryptonRibbonGroupButton_View_Panes_DetailsPane.Checked = true;

                    break;
                }

                case false:
                {
                    KryptonSplitContainer_Preview.Panel2Collapsed = true;
                    KryptonRibbonGroupButton_View_Panes_DetailsPane.Checked = false;

                    break;
                }
            }

            // Load all mod configuration files.
            LoadMods(Marathon.Properties.Settings.Default.Modding_ModsDirectory);
        }

        /// <summary>
        /// Load all mods from the mods directory.
        /// </summary>
        /// <param name="modsDirectory">Directory to load from.</param>
        private void LoadMods(string modsDirectory)
        {
            // Check if the directory exists first before attempting to load.
            if (!Directory.Exists(modsDirectory))
                return;

            foreach (string ini in Directory.GetFiles(modsDirectory, "mod.ini", SearchOption.AllDirectories))
            {
                Mod mod = new Mod
                {
                    Title = INI.ParseKey(ini, "Title"),
                    Version = INI.ParseKey(ini, "Version"),
                    Date = INI.ParseKey(ini, "Date"),
                    Author = INI.ParseKey(ini, "Author"),
                    Description = INI.ParseKey(ini, "Description"),
                    Location = ini,

                    System = Platform.GetPlatformTypeByVerboseKey(INI.ParseKey(ini, "Platform")),

                    Patches = INI.ParseKey(ini, "RequiredPatches").Split(',').ToList(),

                    Merge = INI.ParseKeyAsBoolean(ini, "Merge"),
                    SaveRedirect = INI.ParseKeyAsBoolean(ini, "SaveRedirect"),
                    CustomFilesystem = INI.ParseKeyAsBoolean(ini, "CustomFilesystem"),

                    ReadOnly = INI.ParseKey(ini, "Read-only").Split(',').ToList(),
                    Custom = INI.ParseKey(ini, "Custom").Split(',').ToList()
                };

                ListViewDark_Mods.Items.Add
                (
                    new ListViewItem
                    (
                        new[]
                        {
                            mod.Title,
                            mod.Version,
                            mod.Author,
                            mod.System.ToString()
                        }
                    )
                    {
                        Tag = mod
                    }
                );
            }
        }

        /// <summary>
        /// Refreshes the details.
        /// </summary>
        private void RefreshDetails()
        {
            // Update the details box if there's a single selected item and the details pane is visible.
            if (ListViewDark_Mods.SelectedItems.Count == 1 && KryptonRibbonGroupButton_View_Panes_DetailsPane.Checked)
                Preview_Mod.SelectedMod = (Mod)ListViewDark_Mods.SelectedItems[0].Tag;
        }

        /// <summary>
        /// Update the details box upon selected index changing.
        /// </summary>
        private void ListViewDark_Mods_SelectedIndexChanged(object sender, EventArgs e)
            => RefreshDetails();

        /// <summary>
        /// Show/hide the details pane upon checking.
        /// </summary>
        private void KryptonRibbonGroupButton_View_Panes_PreviewPane_Click(object sender, EventArgs e)
        {
            // Set new details pane settings.
            switch (KryptonRibbonGroupButton_View_Panes_DetailsPane.Checked)
            {
                case true:
                {
                    KryptonSplitContainer_Preview.Panel2Collapsed = false;
                    Marathon.Properties.Settings.Default.Modding_ManagerDetailsVisible = true;

                    RefreshDetails();

                    break;
                }

                case false:
                {
                    KryptonSplitContainer_Preview.Panel2Collapsed = true;
                    Marathon.Properties.Settings.Default.Modding_ManagerDetailsVisible = false;

                    break;
                }
            }
        }
    }
}
