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

using Marathon.Toolkit.Components;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace Marathon.Toolkit.Forms
{
    public partial class Windows : Form
    {
        public Windows(DockPanel parent)
        {
            InitializeComponent();

            // Add all documents to the ListView control.
            foreach (DockContent document in parent.Documents)
                ListViewDark_Windows.Items.Add(new ListViewItem(document.Text) { Tag = document });

            ListViewDark_Windows.MouseDoubleClick += (mouseDoubleClickSender, mouseDoubleClickEventArgs) =>
            {
                // Focus the selected document when clicked.
                if (mouseDoubleClickEventArgs.Button == MouseButtons.Left)
                    FocusSelectedDocument();
            };
        }

        /// <summary>
        /// Focuses the selected document in the ListView control.
        /// </summary>
        private void FocusSelectedDocument()
        {
            // Focus the selected document.
            ((DockContent)ListViewDark_Windows.SelectedItems[0].Tag).Activate();

            // Close when focused.
            Close();
        }

        /// <summary>
        /// Perform tasks upon clicking a ListViewItem.
        /// </summary>
        private void ListViewDark_Windows_MouseDown(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Right:
                {
                    /* WinForms is dumb and updates the ListViewItem selection state lazily when right-clicking.
                     * This is a workaround for that to ensure at least one item is selected.
                     * Just checking the count doesn't work and requires two right-clicks, which is painful. */
                    if (ListViewDark_Windows.GetItemAt(e.X, e.Y) == null)
                        return;

                    ContextMenuStripDark menu = new ContextMenuStripDark();

                    if (ListViewDark_Windows.SelectedItems.Count < 2)
                        menu.Items.Add(new ToolStripMenuItem("Focus", Resources.LoadBitmapResource(nameof(Properties.Resources.Placeholder)), delegate { FocusSelectedDocument(); }));

                    menu.Items.Add(new ToolStripMenuItem("Close", Resources.LoadBitmapResource(nameof(Properties.Resources.Placeholder)), delegate
                    {
                        foreach (ListViewItem window in ListViewDark_Windows.SelectedItems)
                        {
                        // Close the selected document.
                        ((DockContent)window.Tag).Close();

                        // Remove the selected item.
                        window.Remove();
                        }
                    }));

                    menu.Show(Cursor.Position);

                    break;
                }
            }
        }
    }
}
