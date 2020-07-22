using System.Drawing;
using System.Windows.Forms;

namespace Marathon.Source.Components
{
    public partial class ListViewDark : ListView
    {
        public ListViewDark()
        {
            InitializeComponent();

            DrawItem += (drawItemSender, drawItemEventArgs) => drawItemEventArgs.DrawDefault = true;

            DrawColumnHeader += ListViewDark_DrawColumnHeader;

            Resize += (resizeSender, resizeEventArgs) => ((ListView)resizeSender).Columns[((ListView)resizeSender).Columns.Count - 1].Width = ((ListView)resizeSender).Width;
        }

        /// <summary>
        /// Redraws the column header.
        /// </summary>
        private void ListViewDark_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
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
    }
}
