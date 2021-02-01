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
using System.IO;
using System.Linq;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Marathon.Helpers;
using Marathon.Components;
using Marathon.IO.Formats.Archives;
using BrightIdeasSoftware;

namespace Marathon.Toolkit.Forms
{
    public partial class BulkRenamer : Form
    {
        private string _MarathonExplorerPath;
        private ListView _ArchiveExplorerListView;
        private SearchOption _SearchOption = SearchOption.TopDirectoryOnly;
        private RenameState _RenameState;

        // List of rename modules for the ObjectListView.
        private List<RenameModule> Modules = new List<RenameModule>();

        public enum RenameState
        {
            Archive,
            Drive
        }

        public class RenameModule
        {
            public string Original, // Original file name.
                          Renamed;   // Renamed file name.

            public object Tag;

            public RenameModule(string name, object tag)
            {
                Original = name;
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
                FlowLayoutPanel_Properties.Controls.Remove(CheckBoxDark_IncludeFiles);
                FlowLayoutPanel_Properties.Controls.Remove(CheckBoxDark_IncludeDirectories);
                FlowLayoutPanel_Properties.Controls.Remove(CheckBoxDark_IncludeSubdirectoryContents);
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
                if (CheckBoxDark_IncludeDirectories.Checked)
                {
                    foreach (string node in Directory.GetDirectories(_MarathonExplorerPath, "*", _SearchOption))
                    {
                        // Add this file to the modules list.
                        Modules.Add(new RenameModule(Path.GetFileName(node), node));
                    }
                }

                if (CheckBoxDark_IncludeFiles.Checked)
                {
                    foreach (string node in Directory.GetFiles(_MarathonExplorerPath, "*", _SearchOption))
                    {
                        // Add this file to the modules list.
                        Modules.Add(new RenameModule(Path.GetFileName(node), node));
                    }
                }
            }
            else if (_RenameState == RenameState.Archive)
            {
                foreach (ListViewItem node in _ArchiveExplorerListView.SelectedItems)
                {
                    // Add this file to the modules list.
                    Modules.Add(new RenameModule(node.Text, node.Tag));
                }
            }

            // Load all rename modules as objects.
            MarathonListView_Preview.ListView.SetObjects(Modules);
        }

        private void CheckBoxDark_Properties_CheckedChanged_Group(object sender, EventArgs e)
        {
            // Store the checked state for easier reference.
            bool @checked = ((CheckBoxDark)sender).Checked;

            if (sender == CheckBoxDark_CreatePrefix || sender == CheckBoxDark_CreateSuffix)
            {
                switch (@checked)
                {
                    case true:
                    {
                        // The opposite CheckBox to the sender.
                        CheckBoxDark opposed = sender == CheckBoxDark_CreatePrefix ?
                                                         CheckBoxDark_CreateSuffix :
                                                         CheckBoxDark_CreatePrefix;

                        // Rename the watermark based on sender.
                        TextBoxDark_Criteria_2.Watermark = sender == CheckBoxDark_CreatePrefix ? "Text to prepend" : "Text to append";

                        /* Disable the opposite CheckBox, as well as the Case Sensitive one,
                            since it's redundant. */
                        opposed.Enabled = CheckBoxDark_CaseSensitive.Enabled = false;

                        // Set visibility state for CheckBox_AppendSuffixToExtension.
                        if (sender == CheckBoxDark_CreateSuffix)
                        {
                            // We only want this to display for suffix creation.
                            CheckBoxDark_AppendSuffixToExtension.Visible = true;
                        }

                        // Disable the second TextBox, as it won't be necessary.
                        TextBoxDark_Criteria_1.Enabled = false;

                        break;
                    }

                    case false:
                    {
                        // Restore TextBox watermark.
                        TextBoxDark_Criteria_2.Watermark = "New name";

                        // Restore the boxes.
                        CheckBoxDark_CreatePrefix.Enabled  =
                        CheckBoxDark_CreateSuffix.Enabled  =
                        CheckBoxDark_CaseSensitive.Enabled = true;

                        // Reset visibility state for CheckBox_AppendSuffixToExtension.
                        CheckBoxDark_AppendSuffixToExtension.Visible = false;

                        // Restore the second TextBox.
                        TextBoxDark_Criteria_1.Enabled = true;

                        break;
                    }
                }
            }
            else if (sender == CheckBoxDark_IncludeFiles || sender == CheckBoxDark_IncludeDirectories || sender == CheckBoxDark_IncludeSubdirectoryContents)
            {
                if (@checked)
                {
                    if (sender == CheckBoxDark_IncludeFiles)
                    {
                        // Enable CheckBox_IncludeSubdirectoryContents.
                        CheckBoxDark_IncludeSubdirectoryContents.Enabled = true;
                    }

                    if (sender == CheckBoxDark_IncludeSubdirectoryContents)
                    {
                        // Set the global search option.
                        _SearchOption = SearchOption.AllDirectories;
                    }
                }
                else
                {
                    if (sender == CheckBoxDark_IncludeFiles)
                    {
                        // Disable CheckBox_IncludeSubdirectoryContents.
                        CheckBoxDark_IncludeSubdirectoryContents.Enabled = false;
                    }

                    if (sender == CheckBoxDark_IncludeSubdirectoryContents)
                    {
                        // Reset the global search option.
                        _SearchOption = SearchOption.TopDirectoryOnly;
                    }
                }

                // Load files.
                LoadContentNodes();
            }
            else if (sender == CheckBoxDark_MakeUppercase || sender == CheckBoxDark_MakeLowercase || sender == CheckBoxDark_MakeTitlecase)
            {
                /* This is kind of a messy solution, but it's 2am and I can't think of anything better right now.
                   Feel free to grill me if you come across this - alternatively, a reminder will do; I don't make a good snack. */

                if (@checked)
                {
                    if (sender == CheckBoxDark_MakeUppercase)
                    {
                        CheckBoxDark_MakeLowercase.Enabled = false;
                        CheckBoxDark_MakeTitlecase.Enabled = false;
                    }
                    else if (sender == CheckBoxDark_MakeLowercase)
                    {
                        CheckBoxDark_MakeUppercase.Enabled = false;
                        CheckBoxDark_MakeTitlecase.Enabled = false;
                    }
                    else if (sender == CheckBoxDark_MakeTitlecase)
                    {
                        CheckBoxDark_MakeUppercase.Enabled = false;
                        CheckBoxDark_MakeLowercase.Enabled = false;
                    }
                }
                else
                {
                    CheckBoxDark_MakeUppercase.Enabled = true;
                    CheckBoxDark_MakeLowercase.Enabled = true;
                    CheckBoxDark_MakeTitlecase.Enabled = true;
                }
            }

            // Update the live preview.
            UpdatePreview();
        }

        /// <summary>
        /// Updates the preview with the current properties.
        /// </summary>
        private void UpdatePreview(OLVListItem item = null)
        {
            string @criteria = TextBoxDark_Criteria_1.Text;

            // Update by iteration.
            if (item == null)
            {
                foreach (OLVListItem node in MarathonListView_Preview.ListView.CheckedItems)
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
            void UpdateItem(OLVListItem node = null)
            {
                // This should never happen, but just in case...
                if (node == null)
                    return;

                // Stored for easier reference, as usual.
                RenameModule @nodeModule = (RenameModule)node.RowObject;

                // Renamed string.
                string renamed = string.Empty;

                // Use prefix string.
                if (CheckBoxDark_CreatePrefix.Checked && !CheckBoxDark_CreatePrefix.Indeterminate)
                {
                    renamed = TextBoxDark_Criteria_2.Text + @nodeModule.Original;
                }

                // Use suffix string.
                else if (CheckBoxDark_CreateSuffix.Checked && !CheckBoxDark_CreateSuffix.Indeterminate)
                {
                    // Just a quick condition so we can append both ways.
                    renamed = CheckBoxDark_AppendSuffixToExtension.Checked ?
                              @nodeModule.Original + TextBoxDark_Criteria_2.Text :
                              Path.GetFileNameWithoutExtension(@nodeModule.Original) + TextBoxDark_Criteria_2.Text + Path.GetExtension(@nodeModule.Original);
                }

                // Use text from the first TextBox.
                else
                {
                    // Ensure the criteria isn't nothingness first.
                    if (!string.IsNullOrEmpty(@criteria))
                    {
                        // Using a regular expression here so we can enable/disable case sensitivity.
                        renamed = new Regex(@criteria, CheckBoxDark_CaseSensitive.Checked ? RegexOptions.None : RegexOptions.IgnoreCase)
                                     .Replace(@nodeModule.Original, TextBoxDark_Criteria_2.Text);
                    }
                }

                // Change the renamed text only if there's a difference.
                if (@nodeModule.Original != renamed)
                {
                                                                 // Uppercase result.
                    node.SubItems[1].Text = @nodeModule.Renamed = CheckBoxDark_MakeUppercase.Checked ?
                                                                 renamed.ToUpper() :
                                                                 
                                                                 // Lowercase result.
                                                                 CheckBoxDark_MakeLowercase.Checked ? 
                                                                 renamed.ToLower() :
                                                                 
                                                                 // Titlecase result.
                                                                 CheckBoxDark_MakeTitlecase.Checked ?
                                                                 CultureInfo.DefaultThreadCurrentUICulture.TextInfo.ToTitleCase(renamed) :
                                                                 
                                                                 // Default result.
                                                                 renamed;
                }

                // No difference, so reset.
                else
                {
                    // Reset the renamed string.
                    node.SubItems[1].Text = @nodeModule.Renamed = string.Empty;
                }
            }
        }

        /// <summary>
        /// Resets the renamed string if the item was unchecked.
        /// </summary>
        private void MarathonListView_Preview_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            // Store the item for easier reference.
            OLVListItem @checked = (OLVListItem)MarathonListView_Preview.ListView.Items[e.Index];

            // Result is unchecked.
            if (e.NewValue == CheckState.Unchecked)
            {
                // Reset the renamed string.
                @checked.SubItems[1].Text = ((RenameModule)@checked.RowObject).Renamed = string.Empty;
            }

            // Result is checked.
            else if (e.NewValue == CheckState.Checked)
            {
                // Update the renamed string.
                UpdatePreview(@checked);
            }
        }

        /// <summary>
        /// Updates the renamed preview when typed into.
        /// </summary>
        private void TextBoxDark_Criteria_TextChanged_Group(object sender, EventArgs e) => UpdatePreview();

        /// <summary>
        /// Begins the renaming process.
        /// </summary>
        private void ButtonDark_Rename_Click(object sender, EventArgs e)
        {
            foreach (OLVListItem node in MarathonListView_Preview.ListView.Items)
            {
                RenameModule @nodeModule = (RenameModule)node.RowObject;

                if (_RenameState == RenameState.Drive)
                {
                    // Original path.
                    string @modulePath = (string)@nodeModule.Tag;

                    // Check if the renamed string isn't null or invalid before we overwrite it.
                    if (!string.IsNullOrEmpty(@nodeModule.Renamed) && @nodeModule.Renamed.IndexOfAny(Path.GetInvalidFileNameChars()) != 0)
                    {
                        // Assembled path from renamed node.
                        string newPath = StringHelper.ReplaceFilename(@modulePath, @nodeModule.Renamed.TrimEnd('.'));

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
                    if (!string.IsNullOrEmpty(@nodeModule.Renamed))
                    {
                        // Rename time!
                        node.Text = @moduleData.Name = @nodeModule.Renamed.TrimEnd('.');
                    }
                }
            }

            Close();
        }
    }
}
