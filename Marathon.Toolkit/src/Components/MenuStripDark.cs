using System.Drawing;
using System.Windows.Forms;

namespace Marathon.Components
{
	public partial class MenuStripDark : MenuStrip
	{
		public MenuStripDark()
		{
			InitializeComponent();
			RenderMode = ToolStripRenderMode.Professional;
			Renderer = new ToolStripProfessionalRenderer(new MarathonDarkColorTable());
			BackColor = Color.FromArgb(45, 45, 48);
			ForeColor = SystemColors.Control;
		}
	}
}
