using System.Windows.Forms;

namespace Toolkit.Environment4
{
    public partial class UserContainer : UserControl
    {
        public UserContainer() { InitializeComponent(); }

        public string Title {
            get { return Label_Title.Text; }
            set { Label_Title.Text = value; }
        }

        public int SplitterDistance {
            get { return Container_Control.SplitterDistance; }
            set { Container_Control.SplitterDistance = value; }
        }
    }
}
