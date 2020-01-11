using System;
using System.Windows.Forms;

namespace Toolkit.Environment4
{
    public partial class SonicSoundStudio : UserControl
    {
        public SonicSoundStudio() { InitializeComponent(); }

        //private void Section_SonicSoundStudio_Click(object sender, EventArgs e) {
        //    foreach (Control control in Container_SonicSoundStudio.Panel1.Controls)
        //        if (control is SectionButton) ((SectionButton)control).SelectedSection = false;

        //    if (sender == SonicSoundStudio_Section_EncodeAT3) {
        //        Label_StartSample.ForeColor = Label_EndSample.ForeColor = SystemColors.Control;
        //        NumericUpDown_StartSample.Enabled = NumericUpDown_EndSample.Enabled = true;
        //    } else {
        //        Label_StartSample.ForeColor = Label_EndSample.ForeColor = SystemColors.GrayText;
        //        NumericUpDown_StartSample.Enabled = NumericUpDown_EndSample.Enabled = false;
        //    }

        //    Label_Title_SonicSoundStudio.Text = ((SectionButton)sender).SectionText;
        //    splitContainer1.Visible = true;
        //    Image_SonicSoundStudio_Logo.Visible = false;
        //    ((SectionButton)sender).SelectedSection = true;
        //}
    }
}
