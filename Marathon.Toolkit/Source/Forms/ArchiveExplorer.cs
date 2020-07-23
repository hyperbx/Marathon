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

using System.IO;
using System.Linq;
using Marathon.Helpers;
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
        private CompressedU8Archive _LoadedArchive = new CompressedU8Archive();

        [Description("The current archive serialised in the TreeView and ListView controls.")]
        public string CurrentArchive
        {
            get => _CurrentArchive;

            set
            {
                Text += $"({_CurrentArchive = value})";

                _LoadedArchive.Load(_CurrentArchive);

                GetDirectories();
            }
        }

        /// <summary>
        /// Populate the TreeView control with all root directories.
        /// </summary>
        private void GetDirectories()
        {
            // Store the current expanded nodes before refreshing...
            List<string> storedExpansionState = TreeView_Explorer.Nodes.GetExpansionState();

            // Clear current node to remove ghost child nodes... Spooky!
            TreeView_Explorer.Nodes.Clear();

            foreach (U8DataEntry entry in _LoadedArchive.Entries)
            {
                if (entry.GetType().Equals(typeof(U8DirectoryEntry)))
                {
                    TreeNode node = new TreeNode
                    {
                        Text = entry.Name,
                        Tag = entry,
                        ImageKey = "Folder"
                    };

                    TreeView_Explorer.Nodes.Add(node);

                    // Add ghost child nodes so they can be expanded.
                    if (((U8DirectoryEntry)entry).Contents.OfType<U8DirectoryEntry>().Count() != 0)
                        node.Nodes.Add(new TreeNode("Loading..."));
                }
            }

            // Restore expanded nodes.
            TreeView_Explorer.Nodes.SetExpansionState(storedExpansionState);
        }

        /// <summary>
        /// Populate the TreeView control with all subdirectories.
        /// </summary>
        private void GetDirectories(TreeNode child)
        {
            if (child.Tag.GetType().Equals(typeof(U8DirectoryEntry)))
            {
                // Clear current node to remove ghost child nodes... Spooky!
                child.Nodes.Clear();

                foreach (U8DataEntry entry in ((U8DirectoryEntry)child.Tag).Contents)
                {
                    if (entry.GetType().Equals(typeof(U8DirectoryEntry)))
                    {
                        TreeNode node = new TreeNode
                        {
                            Text = entry.Name,
                            Tag = entry,
                            ImageKey = "Folder"
                        };

                        child.Nodes.Add(node);

                        // Add ghost child nodes so they can be expanded.
                        if (((U8DirectoryEntry)entry).Contents.OfType<U8DirectoryEntry>().Count() != 0)
                            node.Nodes.Add(new TreeNode("Loading..."));
                    }
                }
            }
        }

        /// <summary>
        /// Gets the U8 data from the specified child node and adds it to the ListView control.
        /// </summary>
        private void GetDataTable(TreeNode child)
        {
            if (child.Tag.GetType().Equals(typeof(U8DirectoryEntry)))
            {
                // Clear current directory.
                ListView_Explorer.Items.Clear();

                // Add directory nodes to the ListView control.
                foreach (U8DataEntry entry in ((U8DirectoryEntry)child.Tag).Contents)
                {
                    ListViewItem node = new ListViewItem(new[]
                    {
                        // Name
                        entry.Name,

                        // Type
                        entry.GetType().Equals(typeof(U8FileEntry)) ? $"{Path.GetExtension(entry.Name).ToUpper()} File".Substring(1) : "File folder",

                        // Size
                        entry.GetType().Equals(typeof(U8FileEntry)) ? Strings.ByteLengthToDecimalString(((U8FileEntry)entry).UncompressedSize) : string.Empty
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

        /// <summary>
        /// Navigates to the selected node if valid.
        /// </summary>
        private void TreeView_Explorer_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                GetDirectories(e.Node);

                GetDataTable(e.Node);

                if (e.Node.Nodes.Count != 0) e.Node.Expand();
            }
        }

        //_LoadedArchive.DecompressFileData(new FileStream(_CurrentArchive, FileMode.Open), (U8DataEntryZlib)e.Node.Tag);

        /// <summary>
        /// Gets the subdirectories for the current node, rather than loading all at once.
        /// </summary>
        private void TreeView_Explorer_AfterExpand(object sender, TreeViewEventArgs e) => GetDirectories(e.Node);

        private void ListView_Explorer_MouseDoubleClick(object sender, MouseEventArgs e)
            => GetDirectories(TreeView_Explorer.Nodes.Cast<TreeNode>().Where(x => x.Tag == ListView_Explorer.SelectedItems[0].Tag).First());
    }
}
