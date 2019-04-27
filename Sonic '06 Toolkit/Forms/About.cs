using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace Sonic_06_Toolkit
{
    public partial class About : Form
    {
        public About()
        {
            InitializeComponent();
        }

        void About_Load(object sender, EventArgs e)
        {
            lbl_versionNumber.Text = Global.versionNumber;
        }

        void Btn_GitHub_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/HyperPolygon64/Sonic-06-Toolkit");
        }

        void Btn_Close_Click(object sender, EventArgs e)
        {
            Close();
        }

        void Lbl_Title_Click(object sender, EventArgs e)
        {
            new Logo().ShowDialog();
        }

        void Btn_Credits_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Sonic '06 Toolkit\nDeveloped by Hyper Development Team\nHosted by SEGA Carnival\n\nLicensed under the GNU General Public License (v2.0).\n\nContributors:\nHyper - Lead Developer\nShadow LAG - Sonic '06 SDK\nxose - ARC Unpacker\ng0ldenlink - ARC Repacker\nSkyth - XNO Converter\nDarioSamo - XNO Converter\nNatsumi - Design Guidance", "Credits", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
