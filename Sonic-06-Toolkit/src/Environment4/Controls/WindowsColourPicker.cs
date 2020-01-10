using System;
using System.Windows.Forms;

namespace Toolkit.Environment4
{
    public partial class WindowsColourPicker : UserControl
    {
        public WindowsColourPicker() { InitializeComponent(); }

        public event EventHandler ButtonClick;

        private void Button_Colour_Click(object sender, EventArgs e) { if (this.ButtonClick != null) this.ButtonClick(sender, e); }

        private void Button_Colour_YellowGold_MouseEnter(object sender, EventArgs e) { ((Button)sender).FlatAppearance.BorderSize = 1; }

        private void Button_Colour_YellowGold_MouseLeave(object sender, EventArgs e) { ((Button)sender).FlatAppearance.BorderSize = 0; }
    }
}
