using System.Drawing;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace Marathon.Helpers
{
    public class MarathonFloatWindow : FloatWindow
    {
        public MarathonFloatWindow(DockPanel dockPanel, DockPane pane) : base(dockPanel, pane)
        {
            FormBorderStyle = FormBorderStyle.Sizable;
            DoubleClickTitleBarToDock = false;
            ShowInTaskbar = true;
            Owner = null;
        }

        public MarathonFloatWindow(DockPanel dockPanel, DockPane pane, Rectangle bounds) : base(dockPanel, pane, bounds)
        {
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
