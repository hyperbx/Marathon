using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sonic_06_Toolkit
{
    public partial class Loop : Form
    {
        public Loop()
        {
            InitializeComponent();
        }

        void Btn_Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        void Btn_Confirm_Click(object sender, EventArgs e)
        {
            if (track_Start.Value > track_End.Value) MessageBox.Show("The start of the loop can't be initialised after the end...", "Stupid Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                AT3_Studio.wholeLoop = "";
                AT3_Studio.beginLoop = "-loop ";
                AT3_Studio.startLoop = track_Start.Value.ToString() + " ";
                AT3_Studio.endLoop = track_End.Value.ToString() + " ";
                Close();
            }
        }
    }
}
