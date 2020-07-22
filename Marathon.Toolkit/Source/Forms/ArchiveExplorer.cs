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

using System.Windows.Forms;
using System.ComponentModel;
using WeifenLuo.WinFormsUI.Docking;
using Marathon.IO.Formats.SonicNext;

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

                RefreshNodes();
            }
        }

        /// <summary>
        /// Refresh the TreeView nodes.
        /// </summary>
        private void RefreshNodes()
        {
            if (!string.IsNullOrEmpty(_CurrentArchive))
            {
                TreeView_Explorer.Nodes.Clear();

                foreach (U8DirectoryEntry entry in _LoadedArchive.Entries)
                {
                    TreeNode node = new TreeNode
                    {
                        Text = entry.Name,
                        Tag = entry
                    };

                    TreeView_Explorer.Nodes.Add(node);

                    if (entry.Contents.Count != 0)
                        node.Nodes.Add(new TreeNode("Loading..."));
                }
            }
        }

        /// <summary>
        /// Gets the U8 data from the specified child.
        /// </summary>
        /// <param name="child"></param>
        private void GetChildNodes(TreeNode child)
        {
            if (child.Tag.GetType().Equals(typeof(U8DirectoryEntry)))
            {
                child.Nodes.Clear();

                foreach (U8DataEntry entry in ((U8DirectoryEntry)child.Tag).Contents)
                {
                    TreeNode node = new TreeNode
                    {
                        Text = entry.Name,
                        Tag = entry
                    };

                    child.Nodes.Add(node);

                    if (entry.GetType().Equals(typeof(U8DirectoryEntry)) && ((U8DirectoryEntry)entry).Contents.Count != 0)
                        node.Nodes.Add(new TreeNode("Loading..."));
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
                GetChildNodes(e.Node);

                // TODO: Add child nodes to ListView control.
            }
        }

        /// <summary>
        /// Gets the subdirectories for the current node, rather than loading all at once.
        /// </summary>
        private void TreeView_Explorer_AfterExpand(object sender, TreeViewEventArgs e)
            => GetChildNodes(e.Node);
    }
}
