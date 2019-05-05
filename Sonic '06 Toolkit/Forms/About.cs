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
            MessageBox.Show(
                "Sonic '06 Toolkit\n" +
                "Developed by Hyper Development Team\n" +
                "Hosted by SEGA Carnival\n\n" +
                "" +
                "Licensed under the GNU General Public License v2.0\n\n" +
                "" +
                "Contributors:\n" +
                "Hyper - Lead Developer\n" +
                "Shadow LAG - Sonic '06 SDK\n" +
                "xose - ARC Unpacker\n" +
                "g0ldenlink - ARC Repacker\n" +
                "Natsumi - Design Guidance\n" +
                "Skyth - XNO Converter & Sonic Audio Tools\n" +
                "DarioSamo - XNO Converter\n" +
                "CRI Middleware - ADX Encoding\n" +
                "RadfordHound - HedgeLib",
                "Credits", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
