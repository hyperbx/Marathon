using System;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;

namespace Marathon.Controls
{
    public partial class ListViewExplorer : UserControl
    {
        private string _CurrentFile;

        [Description("The current file serialised in the TreeView and ListView controls.")]
        public string CurrentFile
        {
            get => _CurrentFile;
            
            set
            {
                _CurrentFile = value;

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

                // TODO
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
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListView_Explorer_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            // Draws the column background colour.
            Color theme = Color.FromArgb(35, 35, 38);
            e.Graphics.FillRectangle(new SolidBrush(theme), e.Bounds);

            // Draws the column header by sender.
            ColumnHeader column = ((ListView)sender).Columns[e.ColumnIndex];
            e.Graphics.FillRectangle(new SolidBrush(theme), e.Bounds.X, 0, 0, e.Bounds.Height);
            e.Graphics.DrawLine(new Pen(Color.FromArgb(99, 99, 99)), e.Bounds.X, e.Bounds.Y, e.Bounds.Left, e.Bounds.Right);

            // Draws the column text by sender.
            TextRenderer.DrawText(e.Graphics, column.Text, ((ListView)sender).Font, new Point(e.Bounds.X + 4, 4), ((ListView)sender).ForeColor);
        }

        /// <summary>
        /// Hacky way of keeping the column colour consistent when resizing the control.
        /// </summary>
        private void ListView_Explorer_Resize(object sender, EventArgs e)
            => ((ListView)sender).Columns[((ListView)sender).Columns.Count - 1].Width = ((ListView)sender).Width; 
    }
}
