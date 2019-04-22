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
    }
}
