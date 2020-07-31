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

using System.Diagnostics;
using System.Windows.Forms;
using Marathon.Toolkit.Helpers;

namespace Marathon.Toolkit.Forms
{
    public partial class About : Form
    {
        public About()
        {
            InitializeComponent();

            Label_Version.Text = $"Version {Program.GlobalVersion}";

            TreeView_Contributors.Nodes.AddRange(XML.ParseContributors());
        }

        /// <summary>
        /// Hides the node highlight upon clicking.
        /// </summary>
        private void TreeView_Contributors_AfterSelect(object sender, TreeViewEventArgs e)
            => TreeView_Contributors.SelectedNode = null;

        /// <summary>
        /// Navigates to the contributor's webpage.
        /// </summary>
        private void TreeView_Contributors_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeView_Contributors.SelectedNode = null;
            if ((string)e.Node.Tag != string.Empty) Process.Start((string)e.Node.Tag);
        }
    }
}
