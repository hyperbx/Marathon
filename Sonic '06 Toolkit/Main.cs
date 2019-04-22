using System;
using System.Windows.Forms;

namespace Sonic_06_Toolkit
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        void File_Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        void Help_About_Click(object sender, EventArgs e)
        {
            new About().ShowDialog();
        }

        void File_OpenARC_Click(object sender, EventArgs e)
        {
            if (ofd_OpenARC.ShowDialog() == DialogResult.OK)
            {
                //Unpacking process will be programmed here.
            }
        }

        void Main_Load(object sender, EventArgs e)
        {
            Random generateSessionID = new Random();
            Global.sessionID = generateSessionID.Next(1, 99999);
            btn_SessionID.Text = Global.sessionID.ToString();
        }
    }
}
