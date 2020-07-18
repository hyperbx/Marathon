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
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using WeifenLuo.WinFormsUI.Docking;
using Marathon.IO.Formats.SonicNext;
using Marathon.Components;

namespace Marathon.Controls
{
    public partial class ListViewExplorer : DockContent
    {
        private string _CurrentFile;

        [Description("The current file serialised in the TreeView and ListView controls.")]
        public string CurrentFile
        {
            get => _CurrentFile;
            
            set
            {
                Text += $"({_CurrentFile = value})";

                // Refresh the TreeView nodes only if the directory tree is available.
                if (!SplitContainer_TreeView.Panel1Collapsed) RefreshNodes();
            }
        }

        /// <summary>
        /// Refresh the TreeView nodes.
        /// </summary>
        private void RefreshNodes()
        {
            if (!string.IsNullOrEmpty(CurrentFile))
            {
                TreeView_Explorer.Nodes.Clear();

                PictureFont pft = new PictureFont();
                pft.Load(_CurrentFile);

                foreach (PictureFont.SubImage entry in pft.Entries)
                {
                    TreeNode node = new TreeNode {
                        Text = entry.Placeholder,
                        Tag = entry
                    };

                    TreeView_Explorer.Nodes.Add(node);
                }
            }
        }

        public ListViewExplorer() => InitializeComponent();

        /// <summary>
        /// Navigates to the selected node if valid.
        /// </summary>
        private void TreeView_Explorer_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            // TODO
        }

        /// <summary>
        /// Draws all items unmodified.
        /// </summary>
        private void ListView_Explorer_DrawItem(object sender, DrawListViewItemEventArgs e) => e.DrawDefault = true;

        /// <summary>
        /// Redraws the column header.
        /// </summary>
        private void ListView_Explorer_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e) => ListViewDark.DrawColumnHeader(sender, e);

        /// <summary>
        /// Hacky way of keeping the column colour consistent when resizing the control.
        /// </summary>
        private void ListView_Explorer_Resize(object sender, EventArgs e)
            => ((ListView)sender).Columns[((ListView)sender).Columns.Count - 1].Width = ((ListView)sender).Width; 
    }
}
