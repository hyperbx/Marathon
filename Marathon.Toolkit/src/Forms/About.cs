using System.Windows.Forms;

namespace Marathon
{
    public partial class About : Form
    {
        public About()
        {
            InitializeComponent();

            Label_Version.Text = $"Version {Program.GlobalVersion}";
        }
    }
}
