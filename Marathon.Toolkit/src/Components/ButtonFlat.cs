using System.Drawing;
using System.Windows.Forms;

namespace Marathon.Components
{
	public partial class ButtonFlat : Button
	{
		public ButtonFlat()
		{
			InitializeComponent();
			FlatStyle = FlatStyle.Flat;
			FlatAppearance.BorderSize = 0;
			ForeColor = SystemColors.ControlText;
			BackColor = SystemColors.ControlLightLight;
		}
	}
}
