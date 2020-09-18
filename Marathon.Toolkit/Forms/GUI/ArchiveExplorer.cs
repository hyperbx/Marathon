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
                ListView_Explorer.Items.Clear();

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

                        ListView_Explorer.Items.Add(node);
                    }
                }

                // Set empty directory text visibility.
                Label_DirectoryEmpty.Visible = ListView_Explorer.Items.Count == 0;
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
            if (selected.Information.Size != 0)
                return _LoadedArchive.DecompressFileData(new FileStream(_CurrentArchive, FileMode.Open), selected);
            else
                return selected.Data;
        }

        /// <summary>
        /// Adds a file to the currently active node from a path.
        /// </summary>
        private void AddFileToActiveNode(string file)
        {
            byte[] data = File.ReadAllBytes(file);

            U8DataEntryZlib zEntry = new U8DataEntryZlib() { UncompressedSize = (uint)data.Length };

            ((U8DirectoryEntry)_ActiveNode.Tag).Contents.Add(new U8FileEntry(Path.GetFileName(file), data) { Information = zEntry });

            InitialiseFileItems(_ActiveNode.Tag);
        }

        /// <summary>
        /// Perform tasks upon clicking a ListViewItem.
        /// </summary>
        private void ListView_Explorer_MouseDown(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Right:
                {
                    ContextMenuStripDark menu = new ContextMenuStripDark();

                    if (ListView_Explorer.SelectedItems.Count == 0)
                    {
                        menu.Items.Add(new ToolStripMenuItem("Add", Resources.LoadBitmapResource(nameof(Properties.Resources.Task_Export)), delegate
                        {
                            OpenFileDialog openDialog = new OpenFileDialog
                            {
                                Title = "Please select a file...",
                                Filter = "All files (*.*)|*.*"
                            };

                            if (openDialog.ShowDialog() == DialogResult.OK)
                                AddFileToActiveNode(openDialog.FileName);
                        }));
                    }
                    else
                    {
                        menu.Items.Add(new ToolStripMenuItem("Extract", Resources.LoadBitmapResource(nameof(Properties.Resources.Task_Export)), delegate
                        {
                            switch (ListView_Explorer.SelectedItems.Count)
                            {
                                case 1:
                                {
                                    SaveFileDialog saveDialog = new SaveFileDialog
                                    {
                                        Title = "Extract",
                                        Filter = "All files (*.*)|*.*",
                                        FileName = ((U8FileEntry)ListView_Explorer.SelectedItems[0].Tag).Name
                                    };

                                    if (saveDialog.ShowDialog() == DialogResult.OK)
                                        File.WriteAllBytes(saveDialog.FileName,
                                                           DecompressSelectedFile((U8FileEntry)ListView_Explorer.SelectedItems[0].Tag));

                                    break;
                                }

                                default:
                                {
                                    OpenFolderDialog folderDialog = new OpenFolderDialog
                                    {
                                        Title = "Extract"
                                    };

                                    if (folderDialog.ShowDialog() == DialogResult.OK)
                                        foreach (ListViewItem selected in ListView_Explorer.SelectedItems)
                                            File.WriteAllBytes(Path.Combine(folderDialog.SelectedPath, ((U8FileEntry)selected.Tag).Name),
                                                               DecompressSelectedFile((U8FileEntry)selected.Tag));

                                    break;
                                }
                            }
                        }));
                    }

                    menu.Show(Cursor.Position);

                    break;
                }
            }
        }

        private void TreeView_Explorer_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Right:
                {
                    ContextMenuStripDark menu = new ContextMenuStripDark();

                    menu.Items.Add(new ToolStripMenuItem("Extract", Resources.LoadBitmapResource(nameof(Properties.Resources.Task_Export)), delegate { ExtractDialog(); }));

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
        private void ExtractDialog()
        {
            OpenFolderDialog folderDialog = new OpenFolderDialog
            {
                Title = "Extract"
            };

            if (folderDialog.ShowDialog() == DialogResult.OK)
                _LoadedArchive.Extract(folderDialog.SelectedPath);
        }

        /// <summary>
        /// Group event for MenuStripDark_Main items.
        /// </summary>
        private void MenuStripDark_Main_File_Click_Group(object sender, EventArgs e)
        {
            // Displays the extract dialog.
            if (sender == MenuStripDark_Main_File_Extract)
            {
                ExtractDialog();
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
    }
}
