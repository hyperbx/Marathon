using System.Linq;
using System.Drawing;
using System.Windows.Forms;

namespace Marathon.Toolkit.Components
{
    public partial class ListViewDark : ListView
    {
        public ListViewDark()
        {
            InitializeComponent();

            DrawItem += (drawItemSender, drawItemEventArgs) => drawItemEventArgs.DrawDefault = true;

            DrawColumnHeader += (drawColumnHeaderSender, drawColumnHeaderEventArgs) =>
            {
            // Shorten event arguments for consistency's sake.
            DrawListViewColumnHeaderEventArgs e = drawColumnHeaderEventArgs;

            // Draws the column background colour.
            Color theme = Color.FromArgb(35, 35, 38);
                e.Graphics.FillRectangle(new SolidBrush(theme), e.Bounds);

            // Draws the column header.
            ColumnHeader column = Columns[e.ColumnIndex];
                e.Graphics.FillRectangle(new SolidBrush(theme), e.Bounds.X, 0, 0, e.Bounds.Height);
                e.Graphics.DrawLine(new Pen(Color.FromArgb(99, 99, 99)), e.Bounds.X, e.Bounds.Y, e.Bounds.Left, e.Bounds.Right);

            // Draws the column text.
            TextRenderer.DrawText(e.Graphics, column.Text, Font, new Point(e.Bounds.X + 4, 4), ForeColor);
            };

            Resize += delegate
            {
                if (Columns.Count != 0)
                    Columns.Cast<ColumnHeader>().Last().Width = Width - Columns.Cast<ColumnHeader>().Select(x => x.Width).FirstOrDefault();
            };
        }
    }
}
