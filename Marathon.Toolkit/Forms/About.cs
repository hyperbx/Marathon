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

using System.Xml.Linq;
using System.Diagnostics;
using System.Windows.Forms;

namespace Marathon.Toolkit.Forms
{
    public partial class About : Form
    {
        public About()
        {
            InitializeComponent();

            Label_Version.Text = $"Version {Program.GlobalVersion}";

            TreeView_Contributors.Nodes.AddRange(Resources.ParseContributorsToTreeNodeArray());
        }

        /// <summary>
        /// Hides the node highlight upon clicking.
        /// </summary>
        private void TreeView_Contributors_AfterSelect(object sender, TreeViewEventArgs e) => TreeView_Contributors.SelectedNode = null;

        /// <summary>
        /// Accesses the selected contributor's information depending on mouse button.
        /// </summary>
        private void TreeView_Contributors_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            XElement selectedNode = (XElement)e.Node.Tag;

            switch (e.Button)
            {
                case MouseButtons.Left:
                {
                    if (!string.IsNullOrEmpty(selectedNode.Attribute("URL").Value))
                        Process.Start(selectedNode.Attribute("URL").Value);

                    break;
                }

                case MouseButtons.Right:
                {
                    if (!string.IsNullOrEmpty(selectedNode.Attribute("Description").Value))
                        MarathonMessageBox.Show(selectedNode.Attribute("Description").Value, selectedNode.Value);

                    break;
                }
            }
            
        }

        /// <summary>
        /// Navigates to the license information.
        /// </summary>
        private void Label_License_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                {
                    MarathonMessageBox.Show(Properties.Resources.About_License);

                    Process.Start("https://github.com/HyperBE32/Marathon/blob/marathon-master/LICENSE");

                    break;
                }
            }
        }
    }
}
