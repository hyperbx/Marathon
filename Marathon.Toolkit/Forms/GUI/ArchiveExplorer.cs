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
using Marathon.Toolkit.Components;
using Marathon.IO.Formats.Archives;
using static Marathon.IO.Formats.Archives.CompressedU8Archive;
using Marathon.Toolkit.Dialogs;

namespace Marathon.Toolkit.Forms
{
    public partial class ArchiveExplorer : DockContent
    {
        private string _CurrentArchive;
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
                if (DataTypeHelper.IsInputOfType(entry, typeof(U8DirectoryEntry)))
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
            if (DataTypeHelper.IsInputOfType(child, typeof(U8DirectoryEntry)))
            {
                // Clear current directory.
                ListView_Explorer.Items.Clear();

                // Add directory nodes to the ListView control.
                foreach (U8DataEntry entry in ((U8DirectoryEntry)child).Contents)
                {
                    if (DataTypeHelper.IsInputOfType(entry, typeof(U8FileEntry)))
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
        private byte[] DecompressSelectedData(object selected)
            => _LoadedArchive.DecompressFileData(new FileStream(_CurrentArchive, FileMode.Open), (U8DataEntryZlib)selected);

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

                    menu.Items.Add(new ToolStripMenuItem("Extract", Properties.Resources.Export, delegate
                    {
                        switch (ListView_Explorer.SelectedItems.Count)
                        {
                            case 0:
                                return;

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
                                                       DecompressSelectedData(((U8FileEntry)ListView_Explorer.SelectedItems[0].Tag).Information));

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
                                                           DecompressSelectedData(((U8FileEntry)selected.Tag).Information));

                                break;
                            }
                        }
                    }));

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

                    menu.Items.Add(new ToolStripMenuItem("Extract", Properties.Resources.Export, delegate
                    {
                        OpenFolderDialog folderDialog = new OpenFolderDialog
                        {
                            Title = "Extract"
                        };

                        if (folderDialog.ShowDialog() == DialogResult.OK)
                        {
                            WriteDataRecursive(folderDialog.SelectedPath, (U8DirectoryEntry)e.Node.Tag);

                            void WriteDataRecursive(string location, U8DirectoryEntry directory)
                            {
                                foreach (U8DataEntry dataEntry in directory.Contents)
                                {
                                    if (dataEntry is U8FileEntry childFile)
                                        File.WriteAllBytes(Path.Combine(location, childFile.Name), DecompressSelectedData(childFile.Information));

                                    if (dataEntry is U8DirectoryEntry childDirectory)
                                        WriteDataRecursive(Directory.CreateDirectory(Path.Combine(location, childDirectory.Name)).FullName, childDirectory);
                                }
                            }
                        }
                    }));

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
                    // Navigates to the selected node if valid.
                    InitialiseFileItems(e.Node.Tag);

                    // Set expanded state.
                    e.Node.Expand();

                    break;
                }
            }
        }

        /// <summary>
        /// Closes the form.
        /// </summary>
        private void MenuStripDark_Main_File_Close_Click(object sender, EventArgs e) => Close();
    }
}
