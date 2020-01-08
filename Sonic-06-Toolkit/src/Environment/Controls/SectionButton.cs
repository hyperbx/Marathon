using System;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing.Drawing2D;

namespace Toolkit
{
    public partial class SectionButton : UserControl
    {
        private Bitmap sectionImage;
        private string sectionText = "None";

        [Category("Images"), Browsable(true), Description("The image used for the section.")]
        public Bitmap SectionImage {
            get { return this.sectionImage; }
            set { this.sectionImage = value; }
        }

        [Category("Options"), Browsable(true), Description("The text used for the section.")]
        public string SectionText {
            get { return this.sectionText; }
            set { this.sectionText = value; }
        }

        public SectionButton() { InitializeComponent(); }

        private void SectionButton_MouseEnter(object sender, EventArgs e) { BackColor = Color.FromArgb(48, 48, 51); }

        private void SectionButton_MouseLeave(object sender, EventArgs e) { BackColor = Color.FromArgb(42, 42, 45); }

        private void SectionButton_MouseDown(object sender, MouseEventArgs e) { BackColor = Color.FromArgb(58, 58, 61); }

        private void SectionButton_MouseUp(object sender, MouseEventArgs e) { BackColor = Color.FromArgb(48, 48, 51); }

        protected override void OnPaint(PaintEventArgs e) {
            Graphics Drawer = e.Graphics;
            Drawer.SmoothingMode = SmoothingMode.HighQuality;
            Drawer.PixelOffsetMode = PixelOffsetMode.HighQuality;
            Drawer.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            Drawer.DrawString(sectionText, Font, new SolidBrush(SystemColors.Control), 40, 10);
            Drawer.DrawImage(sectionImage, 10, 10);
        }
    }
}
