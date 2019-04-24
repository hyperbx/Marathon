using System.Windows.Forms;

namespace Sonic_06_Toolkit
{
    public partial class Logo : Form
    {
        public Logo()
        {
            InitializeComponent();
        }

        void Btn_SaveToDisk_Click(object sender, System.EventArgs e)
        {
            if (sfd_SaveLogo.ShowDialog() == DialogResult.OK)
            {
                Properties.Resources.logo_main.Save(sfd_SaveLogo.FileName, System.Drawing.Imaging.ImageFormat.Png);
            }
        }
    }
}
