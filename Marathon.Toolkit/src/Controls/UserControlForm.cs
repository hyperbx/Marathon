using System.Drawing;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace Marathon.Controls
{
    public partial class UserControlForm : DockContent
    {
        public static UserControl _Controller;

        public UserControl Controller
        {
            get => _Controller;

            set
            {
                Controls.Add(_Controller = value);

                Text = Controller.Name;
                Controller.Dock = DockStyle.Fill;
            }
        }

        public UserControlForm() => InitializeComponent();
    }

    public class MarathonFloatWindow : FloatWindow
    {
        public MarathonFloatWindow(DockPanel dockPanel, DockPane pane) : base(dockPanel, pane)
        {
            MinimumSize = UserControlForm._Controller.MinimumSize;
            FormBorderStyle = FormBorderStyle.Sizable;
            DoubleClickTitleBarToDock = false;
            ShowInTaskbar = true;
            Owner = null;
        }

        public MarathonFloatWindow(DockPanel dockPanel, DockPane pane, Rectangle bounds) : base(dockPanel, pane, bounds)
        {
            MinimumSize = UserControlForm._Controller.MinimumSize;
            FormBorderStyle = FormBorderStyle.Sizable;
            DoubleClickTitleBarToDock = false;
            ShowInTaskbar = true;
            Owner = null;
        }
    }

    public class MarathonFloatWindowFactory : DockPanelExtender.IFloatWindowFactory
    {
        public FloatWindow CreateFloatWindow(DockPanel dockPanel, DockPane pane, Rectangle bounds)
            => new MarathonFloatWindow(dockPanel, pane, bounds);

        public FloatWindow CreateFloatWindow(DockPanel dockPanel, DockPane pane)
            => new MarathonFloatWindow(dockPanel, pane);
    }
}
