using System.Drawing;
using System.Windows.Forms;

namespace Marathon.Components
{
	public partial class ContextMenuStripDark : ContextMenuStrip
	{
		public ContextMenuStripDark()
		{
			InitializeComponent();
			RenderMode = ToolStripRenderMode.Professional;
			Renderer = new ToolStripProfessionalRenderer(new MarathonDarkColorTable());
			ForeColor = SystemColors.Control;
		}
	}
}
