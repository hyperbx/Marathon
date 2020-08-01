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
using System.Xml.Linq;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace Marathon.Toolkit.Forms
{
    public partial class Debugger : DockContent
    {
        public Debugger() => InitializeComponent();

        /// <summary>
        /// Loads configuration into the TreeView.
        /// </summary>
        private void ImportSettingsFromXML()
        {
            TreeView_Properties.Nodes.Clear();

            if (File.Exists(Settings._Configuration))
            {
                XDocument config = XDocument.Load(Settings._Configuration);

                foreach (XElement propertyElem in config.Root.Elements("Property"))
                {
                    TreeNode property = new TreeNode()
                    {
                        Text = propertyElem.Attribute("Name").Value,
                        Tag = propertyElem
                    };

                    TreeView_Properties.Nodes.Add(property);
                }
            }
        }

        /// <summary>
        /// Hides the node highlight upon clicking.
        /// </summary>
        private void TreeView_Properties_AfterSelect(object sender, TreeViewEventArgs e)
            => TreeView_Properties.SelectedNode = null;

        /// <summary>
        /// Perform actions when clicking a TreeNode.
        /// </summary>
        private void TreeView_Properties_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
            => MessageBox.Show(((XElement)e.Node.Tag).Value); // If it's not an XElement... Whoops? ¯\_(ツ)_/¯

        /// <summary>
        /// Loads the configuration.
        /// </summary>
        private void ButtonFlat_LoadSettings_Click(object sender, EventArgs e)
        {
            Settings.Load();
            ImportSettingsFromXML();
        }

        /// <summary>
        /// Saves the configuration.
        /// </summary>
        private void ButtonFlat_SaveSettings_Click(object sender, EventArgs e)
        {
            Settings.Save();
            ImportSettingsFromXML();
        }

        /// <summary>
        /// Opens the OpenGL window.
        /// </summary>
        private void ButtonFlat_OpenGL_Click(object sender, EventArgs e)
        {
            new UserControlForm() { Controller = new ModelViewer() { Name = "Model Viewer" } }.Show(DockPanel);
        }
    }
}
