// Marathon is licensed under the MIT License:
/* 
 * MIT License
 * 
 * Copyright (c) 2020 HyperPolygon64
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
using System.ComponentModel;
using System.Collections.Generic;
using WeifenLuo.WinFormsUI.Docking;
using Marathon.Toolkit.Helpers;
using Marathon.Toolkit.Dialogs;
using Marathon.Toolkit.Components;
using Marathon.IO.Formats.Archives;
using static Marathon.IO.Formats.Archives.CompressedU8Archive;

namespace Marathon.Toolkit.Forms
{
    public partial class ArchiveExplorer : DockContent
    {
        private string _CurrentArchive;
        private TreeNode _ActiveNode;
        private CompressedU8Archive _LoadedArchive = new CompressedU8Archive();

        [Description("The current archive serialised in the TreeView and ListView controls.")]
        public string CurrentArchive
        {
            get => _CurrentArchive;

            set
            {
                Text = $"Archive Explorer ({_CurrentArchive = value})";

                _LoadedArchive.Load(_CurrentArchive);

                TreeNode rootNode = new TreeNode
                {
                    Text = Path.GetFileName(_CurrentArchive),
                    Tag = _LoadedArchive.Entries[0],
                    ImageKey = "Folder",
                };

                TreeView_Explorer.Nodes.Add(rootNode);

                // Set active node to default.
                _ActiveNode = rootNode;

                // Expand root node, because we all like better UX.
                rootNode.Expand();

                // Populate TreeView and ListView with the root files.
                InitialiseDirectoryTree(rootNode);
                InitialiseFileItems(rootNode.Tag);
            }
        }

        /// <summary>
        /// Populate the specified TreeNode with all sub-directories.
        /// </summary>
        private void InitialiseDirectoryTree(TreeNode child)
        {
            // Store the current expanded nodes before refreshing...
            List<string> storedExpansionState = TreeView_Explorer.Nodes.GetExpansionState();

            foreach (U8DataEntry entry in ((U8DirectoryEntry)child.Tag).Contents)
            {
                if (TypeHelper.IsObjectOfType(entry, typeof(U8DirectoryEntry)))
                {
                    TreeNode node = new TreeNode
                    {
                        Text = entry.Name,
                        Tag = entry,
                        ImageKey = "Folder"
                    };

                    child.Nodes.Add(node);

                    // Add child nodes for sub-directories.
                    if (((U8DirectoryEntry)entry).Contents.OfType<U8DirectoryEntry>().Count() != 0)
                        InitialiseDirectoryTree(node);
                }
            }

            // Restore expanded nodes.
            TreeView_Explorer.Nodes.SetExpansionState(storedExpansionState);
        }

        /// <summary>
        /// Gets the U8 data from the specified child node and adds it to the ListView control.
        /// </summary>
        private void InitialiseFileItems(object child)
        {
            if (TypeHelper.IsObjectOfType(child, typeof(U8DirectoryEntry)))
            {
                // Clear current directory.
                ListViewDark_Explorer.Items.Clear();

                // Add directory nodes to the ListView control.
                foreach (U8DataEntry entry in ((U8DirectoryEntry)child).Contents)
                {
                    if (TypeHelper.IsObjectOfType(entry, typeof(U8FileEntry)))
                    {
                        ListViewItem node = new ListViewItem(new[]
                        {
                            // Name
                            entry.Name,

                            // Type
                            $"{Path.GetExtension(entry.Name).ToUpper()} File".Substring(1),

                            // Size
                            Strings.ByteLengthToDecimalString(((U8FileEntry)entry).Information.UncompressedSize)
                        })
                        {
                            Tag = entry,
                            ImageKey = "File"
                        };

                        ListViewDark_Explorer.Items.Add(node);
                    }
                }

                // Set empty directory text visibility.
                Label_DirectoryEmpty.Visible = ListViewDark_Explorer.Items.Count == 0;
            }
        }

        public ArchiveExplorer(string file)
        {
            InitializeComponent();

            CurrentArchive = file;
        }

        /// <summary>
        /// Decompresses the selected data and returns the result.
        /// </summary>
        private byte[] DecompressSelectedFile(U8FileEntry selected)
        {
            // Data is compressed.
            if (selected.Information.Size != 0)
                return _LoadedArchive.DecompressFileData(File.OpenRead(_CurrentArchive), selected);

            // Data is already decompressed.
            else
                return selected.Data;
        }

        /// <summary>
        /// Adds a file to the currently active node from a path.
        /// </summary>
        private void AddFileToActiveNode(string file)
        {
            string fileName = Path.GetFileName(file);

            if (((U8DirectoryEntry)_ActiveNode.Tag).Contents.Any(x => x.Name == fileName))
            {
                MarathonMessageBox.Show($"The destination already has a file named '{fileName}' - please either rename it or delete it...",
                                        "File already exists...", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            byte[] data = File.ReadAllBytes(file);

            // Create a new data entry using the bytes from the input file.
            ((U8DirectoryEntry)_ActiveNode.Tag).Contents.Add(new U8FileEntry(fileName, data)
            {
                Information = new U8DataEntryZlib() { UncompressedSize = (uint)data.Length }
            });

            // Refresh current node.
            InitialiseFileItems(_ActiveNode.Tag);
        }

        /// <summary>
        /// Deletes a file from the currently active node.
        /// </summary>
        private void DeleteFileFromActiveNode(ListViewItem file)
        {
            // Remove the file entry from the directory entry.
            ((U8DirectoryEntry)_ActiveNode.Tag).Contents.Remove((U8FileEntry)file.Tag);

            // Remove the item from the active node.
            file.Remove();

            // Refresh current node.
            InitialiseFileItems(_ActiveNode.Tag);
        }

        /// <summary>
        /// Renames a file in the currently active node.
        /// </summary>
        private void RenameFileInActiveNode(ListViewItem file, string newName)
        {
            // Change ListViewItem and U8FileEntry text.
            file.Text = ((U8FileEntry)file.Tag).Name = newName;

            // Refresh current node.
            InitialiseFileItems(_ActiveNode.Tag);
        }

        /// <summary>
        /// Perform tasks upon clicking a ListViewItem.
        /// </summary>
        private void ListViewDark_Explorer_MouseDown(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Right:
                {
                    ContextMenuStripDark menu = new ContextMenuStripDark();

                    menu.Items.Add(new ToolStripMenuItem("Add", Resources.LoadBitmapResource(nameof(Properties.Resources.Task_AddFile)), delegate
                    {
                        OpenFileDialog openDialog = new OpenFileDialog
                        {
                            Title = "Please select a file...",
                            Filter = "All files (*.*)|*.*"
                        };

                        if (openDialog.ShowDialog() == DialogResult.OK)
                            AddFileToActiveNode(openDialog.FileName);
                    }));

                    // Get the selected item at the current X/Y position.
                    ListViewItem selected = ListViewDark_Explorer.GetItemAt(e.X, e.Y);

                    /* WinForms is dumb and updates the ListViewItem selection state lazily when right-clicking.
                       This is a workaround for that to ensure at least one item is selected.
                       Just checking the count doesn't work and requires two right-clicks, which is painful. */
                    if (selected != null)
                    {
                        // Gotta make things look pretty.
                        menu.Items.Add(new ToolStripSeparator());

                        menu.Items.Add(new ToolStripMenuItem("Extract", Resources.LoadBitmapResource(nameof(Properties.Resources.Task_Export)), delegate
                        {
                            /* These switches will be used frequently in cases where items can be operated on,
                               either as a single element or multiple. If we want to change the behaviour of either
                               operation, we can do so this way. */
                            switch (ListViewDark_Explorer.SelectedItems.Count)
                            {
                                case 1:
                                {
                                    SaveFileDialog saveDialog = new SaveFileDialog
                                    {
                                        Title = "Extract",
                                        Filter = "All files (*.*)|*.*",
                                        FileName = ((U8FileEntry)ListViewDark_Explorer.SelectedItems[0].Tag).Name
                                    };

                                    // Extract single selected item.
                                    if (saveDialog.ShowDialog() == DialogResult.OK)
                                    {
                                        File.WriteAllBytes(saveDialog.FileName,
                                                           DecompressSelectedFile((U8FileEntry)ListViewDark_Explorer.SelectedItems[0].Tag));
                                    }

                                    break;
                                }

                                default:
                                {
                                    OpenFolderDialog folderDialog = new OpenFolderDialog
                                    {
                                        Title = "Extract"
                                    };

                                    // Extract all selected items.
                                    if (folderDialog.ShowDialog() == DialogResult.OK)
                                    {
                                        foreach (ListViewItem selected in ListViewDark_Explorer.SelectedItems)
                                        {
                                            File.WriteAllBytes(Path.Combine(folderDialog.SelectedPath, ((U8FileEntry)selected.Tag).Name),
                                                               DecompressSelectedFile((U8FileEntry)selected.Tag));
                                        }
                                    }

                                    break;
                                }
                            }
                        }));

                        // Delete context menu item.
                        menu.Items.Add(new ToolStripMenuItem("Delete", Resources.LoadBitmapResource(nameof(Properties.Resources.Task_RemoveFile)), delegate
                        {
                            // Number of selected items.
                            int selectedCount = ListViewDark_Explorer.SelectedItems.Count;

                            switch (selectedCount)
                            {
                                case 1:
                                {
                                    DialogResult confirmSingleDelete =
                                        MarathonMessageBox.Show($"Are you sure that you want to permanently delete '{selected.Text}?'",
                                                                "Delete File", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                                    // Delete single selected item.
                                    if (confirmSingleDelete == DialogResult.Yes)
                                    {
                                        DeleteFileFromActiveNode(selected);
                                    }

                                    break;
                                }

                                default:
                                {
                                    DialogResult confirmMultiDelete =
                                        MarathonMessageBox.Show($"Are you sure that you want to permanently delete these {selectedCount} items?",
                                                                "Delete Multiple Items", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                                    // Delete all selected items.
                                    if (confirmMultiDelete == DialogResult.Yes)
                                    {
                                        foreach (ListViewItem selected in ListViewDark_Explorer.SelectedItems)
                                        {
                                            DeleteFileFromActiveNode(selected);
                                        }
                                    }

                                    break;
                                }
                            }
                        }));

                        // Yet another confusing looking workaround for another dumb WinForms bug.
                        if (ListViewDark_Explorer.SelectedItems.Count < 2)
                        {
                            // Rename context menu item.
                            menu.Items.Add(new ToolStripMenuItem("Rename",
                                           Resources.LoadBitmapResource(nameof(Properties.Resources.Task_Rename)),
                                           delegate { selected.BeginEdit(); }));
                        }
                    }

                    menu.Show(Cursor.Position);

                    break;
                }
            }
        }

        /// <summary>
        /// Perform tasks upon clicking a TreeNode.
        /// </summary>
        private void TreeView_Explorer_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Right:
                {
                    ContextMenuStripDark menu = new ContextMenuStripDark();

                    menu.Items.Add(new ToolStripMenuItem("Extract",
                                   Resources.LoadBitmapResource(nameof(Properties.Resources.Task_Export)),
                                   delegate { ExtractDialog((U8DirectoryEntry)e.Node.Tag); }));

                    menu.Show(Cursor.Position);

                    break;
                }
            }
        }

        /// <summary>
        /// Perform tasks upon double clicking a TreeNode.
        /// </summary>
        private void TreeView_Explorer_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                {
                    // Update active node.
                    _ActiveNode = e.Node;

                    // Navigates to the selected node if valid.
                    InitialiseFileItems(e.Node.Tag);

                    // Set expanded state.
                    e.Node.Expand();

                    break;
                }
            }
        }

        /// <summary>
        /// Extracts the archive to the selected location.
        /// </summary>
        private void ExtractDialog(U8DirectoryEntry directory)
        {
            OpenFolderDialog folderDialog = new OpenFolderDialog
            {
                Title = "Extract"
            };

            if (folderDialog.ShowDialog() == DialogResult.OK)
                _LoadedArchive.Extract(folderDialog.SelectedPath, directory);
        }

        /// <summary>
        /// Group event for MenuStripDark_Main items.
        /// </summary>
        private void MenuStripDark_Main_File_Click_Group(object sender, EventArgs e)
        {
            // Displays the extract dialog.
            if (sender == MenuStripDark_Main_File_Extract)
            {
                ExtractDialog((U8DirectoryEntry)_LoadedArchive.Entries[0]);
            }

            // Saves the loaded archive.
            else if (sender == MenuStripDark_Main_File_Save)
            {
                _LoadedArchive.Save(_CurrentArchive);
            }

            // Saves the loaded archive to the desired location.
            else if (sender == MenuStripDark_Main_File_SaveAs)
            {
                SaveFileDialog saveDialog = new SaveFileDialog
                {
                    Title = "Save As",
                    Filter = "Compressed U8 Archive (*.arc)|*.arc"
                };

                if (saveDialog.ShowDialog() == DialogResult.OK)
                    _LoadedArchive.Save(saveDialog.FileName);
            }

            // Closes the form.
            else if (sender == MenuStripDark_Main_File_Close)
            {
                Close();
            }
        }

        /// <summary>
        /// Sets the new name of the file after editing the label.
        /// </summary>
        private void ListViewDark_Explorer_AfterLabelEdit(object sender, LabelEditEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Label))
                RenameFileInActiveNode(ListViewDark_Explorer.Items[e.Item], e.Label);
        }
    }
}
