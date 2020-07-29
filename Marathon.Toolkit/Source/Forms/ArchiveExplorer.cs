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
using Marathon.Helpers;
using Marathon.Components;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.Generic;
using WeifenLuo.WinFormsUI.Docking;
using Marathon.IO.Formats.Archives;
using static Marathon.IO.Formats.Archives.CompressedU8Archive;

namespace Marathon.Controls
{
    public partial class ArchiveExplorer : DockContent
    {
        private string _CurrentArchive;
        private TreeNode _CurrentActiveNode, _TreeNodeClipboardContent;
        private CompressedU8Archive _LoadedArchive = new CompressedU8Archive();

        [Description("The current archive serialised in the TreeView and ListView controls.")]
        public string CurrentArchive
        {
            get => _CurrentArchive;

            set
            {
                Text += $" ({_CurrentArchive = value})";

                _LoadedArchive.Load(_CurrentArchive);

                TreeNode rootNode = new TreeNode
                {
                    Text = Path.GetFileName(_CurrentArchive),
                    Tag = _LoadedArchive.Entries[0],
                    ImageKey = "Folder",
                };

                // Stores the currently active node.
                _CurrentActiveNode = rootNode;

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
                    ListViewItem node = new ListViewItem(new[]
                    {
                        // Name
                        entry.Name,

                        // Type
                        DataTypeHelper.IsInputOfType(entry, typeof(U8FileEntry)) ? $"{Path.GetExtension(entry.Name).ToUpper()} File".Substring(1) : "File folder",

                        // Size
                        DataTypeHelper.IsInputOfType(entry, typeof(U8FileEntry)) ? Strings.ByteLengthToDecimalString(((U8FileEntry)entry).UncompressedSize) : string.Empty
                    })
                    {
                        Tag = entry,
                        ImageKey = "File"
                    };

                    ListView_Explorer.Items.Add(node);
                }
            }
        }

        public ArchiveExplorer() => InitializeComponent();

        //_LoadedArchive.DecompressFileData(new FileStream(_CurrentArchive, FileMode.Open), (U8DataEntryZlib)e.Node.Tag);

        /// <summary>
        /// Perform tasks upon clicking a ListViewItem.
        /// </summary>
        private void ListView_Explorer_MouseDown(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                {
                    if (e.Clicks == 2 && ListView_Explorer.SelectedItems.Count != 0)
                    {
                        if (DataTypeHelper.IsInputOfType(ListView_Explorer.SelectedItems[0].Tag, typeof(U8DirectoryEntry)))
                        {
                            // Get the first matching result to navigate in the TreeView.
                            TreeNode @directoryEntry = _CurrentActiveNode =
                                TreeView_Explorer.SelectedNode.Nodes.Descendants().Single(x => ((U8DataEntry)x.Tag).ID == ((U8DataEntry)ListView_Explorer.SelectedItems[0].Tag).ID);

                            if (@directoryEntry != null)
                            {
                                // Populate ListView with the tag's directory data.
                                InitialiseFileItems(@directoryEntry.Tag);

                                // Navigate to the matching TreeNode.
                                @directoryEntry.EnsureVisible();
                                @directoryEntry.Expand();
                            }
                        }
                    }

                    break;
                }
            }
        }

        /// <summary>
        /// Perform tasks upon clicking a TreeNode.
        /// </summary>
        private void TreeView_Explorer_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeView_Explorer.SelectedNode = e.Node;

            switch (e.Button)
            {
                case MouseButtons.Right:
                {
                    ContextMenuStripDark menu = new ContextMenuStripDark();

                    menu.Items.Clear();

                    menu.Items.Add(new ToolStripMenuItem("Copy", Properties.Resources.Placeholder, delegate { _TreeNodeClipboardContent = e.Node; }));

                    if (_TreeNodeClipboardContent != null)
                    {
                        menu.Items.Add(new ToolStripMenuItem("Paste", Properties.Resources.Placeholder, delegate
                        {
                            TreeNode content = new TreeNode(_TreeNodeClipboardContent.Text);

                            for (int i = 0; i < _TreeNodeClipboardContent.Nodes.Count; i++)
                            {
                                TreeNode clone = (TreeNode)_TreeNodeClipboardContent.Nodes[i].Clone();
                                content.Nodes.Add(clone);
                            }

                            e.Node.Nodes.Add(content);
                        }));
                    }

                    menu.Items.Add(new ToolStripSeparator());

                    menu.Items.AddRange(new ToolStripMenuItem[]
                    {
                        new ToolStripMenuItem("Delete", Properties.Resources.Placeholder, delegate
                        {
                            List<U8DataEntry> lastDirectory = _LoadedArchive.Entries;

                            GetIdentifier(lastDirectory);

                            void GetIdentifier(List<U8DataEntry> entries)
                            {
                                for (int i = 0; i < entries.Count; i++)
                                {
                                    if (entries[i].ID == ((U8DataEntry)e.Node.Tag).ID)
                                    {
                                        entries.Remove(entries[i]);

                                        e.Node.Remove();

                                        // Reloads the active node.
                                        InitialiseFileItems(_CurrentActiveNode.Tag);

                                        // Ensures the renamed node is visible once finished.
                                        e.Node.EnsureVisible();
                                    }

                                    if (entries[i].GetType().Equals(typeof(U8DirectoryEntry)) &&
                                        ((U8DirectoryEntry)entries[i]).Contents.OfType<U8DirectoryEntry>().Count() != 0)
                                    {
                                        lastDirectory = ((U8DirectoryEntry)entries[i]).Contents;
                                        GetIdentifier(lastDirectory);
                                    }
                                }
                            }
                        }),

                        new ToolStripMenuItem("Rename", Properties.Resources.Placeholder, delegate { e.Node.BeginEdit(); })
                    });

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
                    // Stores the currently active node.
                    _CurrentActiveNode = e.Node;

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

        /// <summary>
        /// Sets the archive entry's name to the new node label.
        /// </summary>
        private void TreeView_Explorer_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Label))
            {
                // Edits the name of the data entry to match the new label.
                ((U8DataEntry)e.Node.Tag).Name = e.Label;

                // Reloads the active node.
                InitialiseFileItems(_CurrentActiveNode.Tag);

                // Ensures the renamed node is visible once finished.
                e.Node.EnsureVisible();
            }
        }
    }
}
