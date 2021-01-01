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

using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Marathon.Components;
using Marathon.Helpers;

namespace Marathon.Toolkit.Forms
{
    public partial class Debugger : MarathonDockContent
    {
        class File
        {
            public string Name { get; set; }
            public string Type { get; set; }
            public long Size { get; set; }
        }

        public Debugger()
        {
            InitializeComponent();

            ImportSettings();

            PopulateMarathonListViewSample();
        }

        /// <summary>
        /// Loads configuration into the TreeView.
        /// </summary>
        private void ImportSettings()
        {
            TreeView_Properties.Nodes.Clear();

            // TODO: Rework for .NET settings.
            // foreach (PropertyInfo property in typeof(ISettings).GetProperties())
            // {
            //     TreeNode propertyNode = new TreeNode()
            //     {
            //         Text = property.Name,
            //         Tag = property
            //     };
               
            //     TreeView_Properties.Nodes.Add(propertyNode);
            // }
        }

        private void PopulateMarathonListViewSample()
        {
            List<File> fileList = new List<File>();

            for (int i = 0; i < 2; i++)
            {
                File test = new File
                {
                    Name = Path.GetRandomFileName(),
                    Type = "Lua Bytecode",
                    Size = new Random(i).Next(1, 100000)
                };

                File aaaa = new File
                {
                    Name = $"Test #{i}.lub",
                    Type = "Something Else",
                    Size = new Random(i).Next(1, 100000)
                };

                fileList.Add(test);
                fileList.Add(aaaa);
            }

            olvColumn3.AspectToStringConverter = delegate (object rowObject)
            {
                return StringHelper.ByteLengthToDecimalString((long)rowObject);
            };

            MarathonListView_Sample.ListView.SetObjects(fileList);
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
        {
            PropertyInfo property = (PropertyInfo)e.Node.Tag;

            MarathonMessageBox.Show(property.GetValue(property).ToString());
        }

        /// <summary>
        /// Create a new MarathonMessageBox instance to sample.
        /// </summary>
        private void ButtonDark_MarathonMessageBox_Sample_Click(object sender, EventArgs e)
            => MarathonMessageBox.Show("Test", string.Empty, MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Error);
    }
}
