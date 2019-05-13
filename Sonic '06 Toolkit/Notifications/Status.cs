using System;
using System.Drawing;
using System.Windows.Forms;

namespace Sonic_06_Toolkit
{
    public partial class Status : Form
    {
        public Status()
        {
            InitializeComponent();
        }

        void Status_Notifier_Load(object sender, EventArgs e)
        {
            if (Global.arcState == "typical")
            {
                Text = "Unpacking ARC...";
                lbl_unpackState.Text = "Unpacking ARC. Please wait...";
                pnl_windowCheck.BackColor = Color.Honeydew; BackColor = Color.Honeydew;
                Width = 284;
            }
            if (Global.arcState == "processing")
            {
                Text = "Processing ARCs...";
                lbl_unpackState.Text = "Processing ARCs. Please wait...";
                pnl_windowCheck.BackColor = Color.Honeydew; BackColor = Color.Honeydew;
                Width = 236;
            }
            else if (Global.arcState == "save")
            {
                Text = "Repacking ARC...";
                lbl_unpackState.Text = "Repacking ARC. Please wait...";
                pnl_windowCheck.BackColor = Color.Honeydew; BackColor = Color.Honeydew;
                Width = 284;
            }
            else if (Global.arcState == "save-as")
            {
                Text = "Repacking ARC...";
                lbl_unpackState.Text = "Repacking ARC. Please wait...";
                pnl_windowCheck.BackColor = Color.Honeydew; BackColor = Color.Honeydew;
                Width = 284;
            }
            else if (Global.adxState == "adx")
            {
                Text = "Encoding ADX files...";
                lbl_unpackState.Text = "Encoding ADX files. Please wait...";
                pnl_windowCheck.BackColor = Color.AliceBlue; BackColor = Color.AliceBlue;
                Width = 300;
            }
            else if (Global.adxState == "wav")
            {
                Text = "Encoding WAV files...";
                lbl_unpackState.Text = "Encoding WAV files. Please wait...";
                pnl_windowCheck.BackColor = Color.AliceBlue; BackColor = Color.AliceBlue;
                Width = 300;
            }
            else if (Global.at3State == "at3")
            {
                Text = "Encoding AT3 files...";
                lbl_unpackState.Text = "Encoding AT3 files. Please wait...";
                pnl_windowCheck.BackColor = Color.AliceBlue; BackColor = Color.AliceBlue;
                Width = 300;
            }
            else if (Global.at3State == "wav")
            {
                Text = "Encoding WAV files...";
                lbl_unpackState.Text = "Encoding WAV files. Please wait...";
                pnl_windowCheck.BackColor = Color.AliceBlue; BackColor = Color.AliceBlue;
                Width = 300;
            }
            else if (Global.csbState == "unpack")
            {
                Text = "Unpacking CSBs...";
                lbl_unpackState.Text = "Unpacking CSBs. Please wait...";
                pnl_windowCheck.BackColor = Color.AliceBlue; BackColor = Color.AliceBlue;
                Width = 209;
            }
            else if (Global.csbState == "repack")
            {
                Text = "Repacking CSBs...";
                lbl_unpackState.Text = "Repacking CSBs. Please wait...";
                pnl_windowCheck.BackColor = Color.AliceBlue; BackColor = Color.AliceBlue;
                Width = 212;
            }
            else if (Global.lubState == "decompile")
            {
                Text = "Decompiling LUBs...";
                lbl_unpackState.Text = "Decompiling LUBs. Please wait...";
                pnl_windowCheck.BackColor = Color.AliceBlue; BackColor = Color.AliceBlue;
                Width = 295;
            }
            else if (Global.mstState == "decode")
            {
                Text = "Decoding MSTs...";
                lbl_unpackState.Text = "Decoding MSTs. Please wait...";
                pnl_windowCheck.BackColor = Color.Thistle; BackColor = Color.Thistle;
                Width = 219;
            }
            else if (Global.xnoState == "xno" || Global.xnoState == "xnm")
            {
                Text = "Converting XNOs...";
                lbl_unpackState.Text = "Converting XNOs. Please wait...";
                pnl_windowCheck.BackColor = Color.FromArgb(239, 224, 201); BackColor = Color.FromArgb(239, 224, 201);
                Width = 219;
            }
            else if (Global.exisoState == "extract")
            {
                Text = "Extracting ISO...";
                lbl_unpackState.Text = "Extracting ISO. Please wait...";
                pnl_windowCheck.BackColor = Color.Honeydew; BackColor = Color.Honeydew;
            }
        }
    }
}
