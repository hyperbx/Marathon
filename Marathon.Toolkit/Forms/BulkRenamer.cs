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
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using Marathon.IO.Formats;
using Marathon.IO.Helpers;

namespace Marathon.Toolkit.Forms
{
    public partial class BulkRenamer : Form
    {
        private string _MarathonExplorerPath;
        private ListView _ArchiveExplorerListView;
        private SearchOption _SearchOption = SearchOption.TopDirectoryOnly;
        private RenameState _RenameState;

        public enum RenameState
        {
            Archive,
            Drive
        }

        public class RenameModule
        {
            public string Name, Rename;
            public object Tag;

            public RenameModule(string name, object tag)
            {
                Name = name;
                Tag = tag;
            }
        }

        public BulkRenamer(string path)
        {
            InitializeComponent();

            _MarathonExplorerPath = path;
        }

        public BulkRenamer(ListView listView)
        {
            InitializeComponent();

            _ArchiveExplorerListView = listView;
        }

        /// <summary>
        /// Load events for Bulk Renamer.
        /// </summary>
        private void BulkRenamer_Load(object sender, EventArgs e)
        {
            // Initialised from Marathon Explorer.
            if (!string.IsNullOrEmpty(_MarathonExplorerPath))
            {
                // Input is a local path.
                _RenameState = RenameState.Drive;
            }

            // Initialised from Archive Explorer.
            else if (_ArchiveExplorerListView != null)
            {
                // Input is an archive.
                _RenameState = RenameState.Archive;

                // Remove redundant properties.
                FlowLayoutPanel_Properties.Controls.Remove(CheckBox_IncludeFiles);
                FlowLayoutPanel_Properties.Controls.Remove(CheckBox_IncludeDirectories);
                FlowLayoutPanel_Properties.Controls.Remove(CheckBox_IncludeSubdirectoryContents);
            }

            // Load files.
            LoadContentNodes();
        }

        /// <summary>
        /// Loads the nodes from the current location.
        /// </summary>
        private void LoadContentNodes()
        {
            if (_RenameState == RenameState.Drive)
            {
                if (CheckBox_IncludeDirectories.Checked)
                {
                    foreach (string node in Directory.GetDirectories(_MarathonExplorerPath, "*", _SearchOption))
                    {
                        // Create module for this node so we can refer to its full name later.
                        RenameModule nodeModule = new RenameModule(Path.GetFileName(node), node);

                        // Add node to the ListView.
                        ListViewDark_Preview.Items.Add(new ListViewItem(new [] {nodeModule.Name, "" })
                        {
                            Tag = nodeModule,
                            Checked = true,
                            ImageKey = "Folder"
                        });
                    }
                }

                if (CheckBox_IncludeFiles.Checked)
                {
                    foreach (string node in Directory.GetFiles(_MarathonExplorerPath, "*", _SearchOption))
                    {
                        // Create module for this node so we can refer to its full name later.
                        RenameModule nodeModule = new RenameModule(Path.GetFileName(node), node);

                        // Add node to the ListView.
                        ListViewDark_Preview.Items.Add(new ListViewItem(new[] { nodeModule.Name, "" })
                        {
                            Tag = nodeModule,
                            Checked = true,
                            ImageKey = "File"
                        });
                    }
                }
            }
            else if (_RenameState == RenameState.Archive)
            {
                foreach (ListViewItem node in _ArchiveExplorerListView.SelectedItems)
                {
                    // Create module for this node so we can refer to its ArchiveData later.
                    RenameModule nodeModule = new RenameModule(node.Text, node.Tag);

                    // Add node to the ListView.
                    ListViewDark_Preview.Items.Add(new ListViewItem(new[] { nodeModule.Name, "" })
                    {
                        Tag = nodeModule,
                        Checked = true,
                        ImageKey = "File"
                    });
                }
            }
        }

        private void CheckBox_Properties_CheckedChanged_Group(object sender, EventArgs e)
        {
            // Store the checked state for easier reference.
            bool @checked = ((CheckBox)sender).Checked;

            if (sender == CheckBox_CreatePrefix || sender == CheckBox_CreateSuffix)
            {
                switch (@checked)
                {
                    case true:
                    {
                        // The opposite CheckBox to the sender.
                        CheckBox opposed = sender == CheckBox_CreatePrefix ?
                                                     CheckBox_CreateSuffix :
                                                     CheckBox_CreatePrefix;

                        // Rename the label based on sender.
                        Label_TextBoxDark_Criteria_2.Text = sender == CheckBox_CreatePrefix ? "Prefix:" : "Suffix:";

                        // Disable the interfering boxes.
                        {
                            // Unsubscribe from this event so it doesn't recurse.
                            opposed.CheckedChanged -= CheckBox_Properties_CheckedChanged_Group;

                            /* Disable the opposite CheckBox, as well as the Case Sensitive one,
                               since it's redundant. */
                            opposed.Checked =
                            opposed.AutoCheck =
                            CheckBox_CaseSensitive.AutoCheck = false;

                            /* Set the states to indeterminate so we have a visual representation
                               that these CheckBoxes are being interfered with. */
                            opposed.CheckState =
                            CheckBox_CaseSensitive.CheckState = CheckState.Indeterminate;

                            // Subscribe back to this event.
                            opposed.CheckedChanged += CheckBox_Properties_CheckedChanged_Group;
                        }

                        // Set visibility state for CheckBox_AppendSuffixToExtension.
                        if (sender == CheckBox_CreateSuffix)
                        {
                            // We only want this to display for suffix creation.
                            CheckBox_AppendSuffixToExtension.Visible = true;
                        }

                        // Disable the second TextBox, as it won't be necessary.
                        Label_TextBoxDark_Criteria_1.ForeColor = SystemColors.GrayText;
                        TextBoxDark_Criteria_1.Enabled = false;

                        break;
                    }

                    case false:
                    {
                        // Restore first TextBox label.
                        Label_TextBoxDark_Criteria_2.Text = "New:";

                        // Restore the boxes.
                        CheckBox_CreatePrefix.AutoCheck =
                        CheckBox_CreateSuffix.AutoCheck =
                        CheckBox_CaseSensitive.AutoCheck = true;

                        // Uncheck the boxes.
                        CheckBox_CreatePrefix.CheckState =
                        CheckBox_CreateSuffix.CheckState =
                        CheckBox_CaseSensitive.CheckState = CheckState.Unchecked;

                        // Reset visibility state for CheckBox_AppendSuffixToExtension.
                        CheckBox_AppendSuffixToExtension.Visible = false;

                        // Restore the second TextBox.
                        Label_TextBoxDark_Criteria_1.ForeColor = SystemColors.Control;
                        TextBoxDark_Criteria_1.Enabled = true;

                        break;
                    }
                }
            }
            else if (sender == CheckBox_IncludeFiles || sender == CheckBox_IncludeDirectories || sender == CheckBox_IncludeSubdirectoryContents)
            {
                // Clear the current list.
                ListViewDark_Preview.Items.Clear();

                if (@checked)
                {
                    if (sender == CheckBox_IncludeFiles)
                    {
                        // Enable CheckBox_IncludeSubdirectoryContents.
                        SetSubdirectoryEnabledState(true);
                    }

                    if (sender == CheckBox_IncludeSubdirectoryContents)
                    {
                        // Set the global search option.
                        _SearchOption = SearchOption.AllDirectories;
                    }
                }
                else
                {
                    if (sender == CheckBox_IncludeFiles)
                    {
                        // Disable CheckBox_IncludeSubdirectoryContents.
                        SetSubdirectoryEnabledState(false);
                    }

                    if (sender == CheckBox_IncludeSubdirectoryContents)
                    {
                        // Reset the global search option.
                        _SearchOption = SearchOption.TopDirectoryOnly;
                    }
                }

                // Changes the enabled state for CheckBox_IncludeSubdirectoryContents.
                void SetSubdirectoryEnabledState(bool enabled)
                {
                    // Unsubscribe from this event so it doesn't recurse.
                    CheckBox_IncludeSubdirectoryContents.CheckedChanged -= CheckBox_Properties_CheckedChanged_Group;

                    // Restore the subdirectory contents box.
                    CheckBox_IncludeSubdirectoryContents.AutoCheck = enabled;
                    CheckBox_IncludeSubdirectoryContents.CheckState = enabled ? CheckState.Unchecked : CheckState.Indeterminate;

                    // Subscribe back to this event.
                    CheckBox_IncludeSubdirectoryContents.CheckedChanged += CheckBox_Properties_CheckedChanged_Group;
                }

                // Load files.
                LoadContentNodes();
            }
            else if (sender == CheckBox_MakeUppercase || sender == CheckBox_MakeLowercase || sender == CheckBox_MakeTitlecase)
            {
                /* This is kind of a messy solution, but it's 2am and I can't think of anything better right now.
                   Feel free to grill me if you come across this - alternatively, a reminder will do; I don't make a good snack. */

                if (@checked)
                {
                    if (sender == CheckBox_MakeUppercase)
                    {
                        SetCaseEnabledState(CheckBox_MakeLowercase, false);
                        SetCaseEnabledState(CheckBox_MakeTitlecase, false);
                    }
                    else if (sender == CheckBox_MakeLowercase)
                    {
                        SetCaseEnabledState(CheckBox_MakeUppercase, false);
                        SetCaseEnabledState(CheckBox_MakeTitlecase, false);
                    }
                    else if (sender == CheckBox_MakeTitlecase)
                    {
                        SetCaseEnabledState(CheckBox_MakeUppercase, false);
                        SetCaseEnabledState(CheckBox_MakeLowercase, false);
                    }
                }
                else
                {
                    SetCaseEnabledState(CheckBox_MakeUppercase, true);
                    SetCaseEnabledState(CheckBox_MakeLowercase, true);
                    SetCaseEnabledState(CheckBox_MakeTitlecase, true);
                }

                // Changes the enabled state for the case checkboxes.
                void SetCaseEnabledState(CheckBox checkBox, bool enabled)
                {
                    // Unsubscribe from this event so it doesn't recurse.
                    checkBox.CheckedChanged -= CheckBox_Properties_CheckedChanged_Group;

                    // Restore the case box.
                    checkBox.AutoCheck = enabled;
                    checkBox.CheckState = enabled ? CheckState.Unchecked : CheckState.Indeterminate;

                    // Subscribe back to this event.
                    checkBox.CheckedChanged += CheckBox_Properties_CheckedChanged_Group;
                }
            }

            // Update the live preview.
            UpdatePreview();
        }

        /// <summary>
        /// Updates the preview with the current properties.
        /// </summary>
        private void UpdatePreview(ListViewItem item = null)
        {
            string @criteria = TextBoxDark_Criteria_1.Text;

            // Update by iteration.
            if (item == null)
            {
                foreach (ListViewItem node in ListViewDark_Preview.CheckedItems)
                {
                    UpdateItem(node);
                }
            }

            // Update single item.
            else
            {
                UpdateItem(item);
            }

            // Updates the renamed string for the input item.
            void UpdateItem(ListViewItem node = null)
            {
                // This should never happen, but just in case...
                if (node == null)
                    return;

                // Stored for easier reference, as usual.
                RenameModule @nodeModule = (RenameModule)node.Tag;

                // Renamed string.
                string renamed = string.Empty;

                // Use prefix string.
                if (CheckBox_CreatePrefix.CheckState == CheckState.Checked)
                {
                    renamed = TextBoxDark_Criteria_2.Text + @nodeModule.Name;
                }

                // Use suffix string.
                else if (CheckBox_CreateSuffix.CheckState == CheckState.Checked)
                {
                    // Just a quick condition so we can append both ways.
                    renamed = CheckBox_AppendSuffixToExtension.Checked ?
                              @nodeModule.Name + TextBoxDark_Criteria_2.Text :
                              Path.GetFileNameWithoutExtension(@nodeModule.Name) + TextBoxDark_Criteria_2.Text + Path.GetExtension(@nodeModule.Name);
                }

                // Use text from the first TextBox.
                else
                {
                    // Ensure the criteria isn't nothingness first.
                    if (!string.IsNullOrEmpty(@criteria))
                    {
                        // Using a regular expression here so we can enable/disable case sensitivity.
                        renamed = new Regex(@criteria, CheckBox_CaseSensitive.CheckState == CheckState.Checked ? RegexOptions.None : RegexOptions.IgnoreCase)
                                     .Replace(@nodeModule.Name, TextBoxDark_Criteria_2.Text);
                    }
                }

                // Change the renamed text only if there's a difference.
                if (@nodeModule.Name != renamed)
                {
                                                                 // Uppercase result.
                    node.SubItems[1].Text = @nodeModule.Rename = CheckBox_MakeUppercase.CheckState == CheckState.Checked ?
                                                                 renamed.ToUpper() :
                                                                 
                                                                 // Lowercase result.
                                                                 CheckBox_MakeLowercase.CheckState == CheckState.Checked ? 
                                                                 renamed.ToLower() :
                                                                 
                                                                 // Titlecase result.
                                                                 CheckBox_MakeTitlecase.CheckState == CheckState.Checked ?
                                                                 CultureInfo.DefaultThreadCurrentUICulture.TextInfo.ToTitleCase(renamed) :
                                                                 
                                                                 // Default result.
                                                                 renamed;
                }

                // No difference, so reset.
                else
                {
                    // Reset the renamed string.
                    node.SubItems[1].Text = @nodeModule.Rename = string.Empty;
                }
            }
        }

        /// <summary>
        /// Resets the renamed string if the item was unchecked.
        /// </summary>
        private void ListViewDark_Preview_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            // Store the item for easier reference.
            ListViewItem @checked = ListViewDark_Preview.Items[e.Index];

            // Result is unchecked.
            if (e.NewValue == CheckState.Unchecked)
            {
                // Reset the renamed string.
                @checked.SubItems[1].Text = ((RenameModule)@checked.Tag).Rename = string.Empty;
            }

            // Result is checked.
            else if (e.NewValue == CheckState.Checked)
            {
                // Update the renamed string.
                UpdatePreview(@checked);
            }
        }

        /// <summary>
        /// Selects all items when doing CTRL + A.
        /// </summary>
        private void ListViewDark_Preview_KeyDown(object sender, KeyEventArgs e)
        {
            // CTRL + A shortcut.
            if (e.Control && e.KeyCode == Keys.A)
            {
                // Selects everything in the list.
                ListViewDark_Preview.Items.OfType<ListViewItem>().ToList().ForEach(item => item.Selected = true);
            }
        }

        /// <summary>
        /// Updates the renamed preview when typed into.
        /// </summary>
        private void TextBoxDark_Criteria_TextChanged_Group(object sender, EventArgs e) => UpdatePreview();

        /// <summary>
        /// Begins the renaming process.
        /// </summary>
        private void ButtonFlat_Rename_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem node in ListViewDark_Preview.Items)
            {
                RenameModule @nodeModule = (RenameModule)node.Tag;

                if (_RenameState == RenameState.Drive)
                {
                    // Original path.
                    string @modulePath = (string)@nodeModule.Tag;

                    // Check if the renamed string isn't null before we overwrite it with nothingness.
                    if (!string.IsNullOrEmpty(@nodeModule.Rename))
                    {
                        // Assembled path from renamed node.
                        string newPath = StringHelper.ReplaceFilename(@modulePath, @nodeModule.Rename);

                        // Original path was a file.
                        if (File.Exists(@modulePath))
                            File.Move(@modulePath, newPath);

                        // Original path was a directory.
                        else if (Directory.Exists(@modulePath))
                            Directory.Move(@modulePath, newPath);
                    }
                }
                else if (_RenameState == RenameState.Archive)
                {
                    // Original data.
                    ArchiveData @moduleData = (ArchiveData)@nodeModule.Tag;

                    // Check if the renamed string isn't null before we kill the data. lolmao
                    if (!string.IsNullOrEmpty(@nodeModule.Rename))
                    {
                        // Rename time!
                        @moduleData.Name = @nodeModule.Rename;
                    }
                }
            }

            Close();
        }
    }
}
