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
            if (Tools.Global.arcState == "typical" || Tools.Global.arcState == "launch-typical")
            {
                Text = "Unpacking ARC...";
                lbl_unpackState.Text = "Unpacking ARC. Please wait...";
                pnl_windowCheck.BackColor = Color.Honeydew; BackColor = Color.Honeydew;
                Width = 284;
            }
            if (Tools.Global.arcState == "processing")
            {
                Text = "Processing ARCs...";
                lbl_unpackState.Text = "Processing ARCs. Please wait...";
                pnl_windowCheck.BackColor = Color.Honeydew; BackColor = Color.Honeydew;
                Width = 236;
            }
            else if (Tools.Global.arcState == "save")
            {
                Text = "Repacking ARC...";
                lbl_unpackState.Text = "Repacking ARC. Please wait...";
                pnl_windowCheck.BackColor = Color.Honeydew; BackColor = Color.Honeydew;
                Width = 284;
            }
            else if (Tools.Global.arcState == "save-as")
            {
                Text = "Repacking ARC...";
                lbl_unpackState.Text = "Repacking ARC. Please wait...";
                pnl_windowCheck.BackColor = Color.Honeydew; BackColor = Color.Honeydew;
                Width = 284;
            }
            else if (Tools.Global.adxState == "adx" || Tools.Global.adxState == "launch-adx")
            {
                Text = "Encoding ADX files...";
                lbl_unpackState.Text = "Encoding ADX files. Please wait...";
                pnl_windowCheck.BackColor = Color.AliceBlue; BackColor = Color.AliceBlue;
                Width = 300;
            }
            else if (Tools.Global.adxState == "wav" || Tools.Global.adxState == "launch-wav")
            {
                Text = "Encoding WAV files...";
                lbl_unpackState.Text = "Encoding WAV files. Please wait...";
                pnl_windowCheck.BackColor = Color.AliceBlue; BackColor = Color.AliceBlue;
                Width = 300;
            }
            else if (Tools.Global.at3State == "at3" || Tools.Global.at3State == "launch-at3")
            {
                Text = "Encoding AT3 files...";
                lbl_unpackState.Text = "Encoding AT3 files. Please wait...";
                pnl_windowCheck.BackColor = Color.AliceBlue; BackColor = Color.AliceBlue;
                Width = 300;
            }
            else if (Tools.Global.at3State == "wav")
            {
                Text = "Encoding WAV files...";
                lbl_unpackState.Text = "Encoding WAV files. Please wait...";
                pnl_windowCheck.BackColor = Color.AliceBlue; BackColor = Color.AliceBlue;
                Width = 300;
            }
            else if (Tools.Global.ddsState == "dds" || Tools.Global.ddsState == "launch-dds")
            {
                Text = "Converting DDS files...";
                lbl_unpackState.Text = "Converting DDS files. Please wait...";
                pnl_windowCheck.BackColor = Color.Honeydew; BackColor = Color.Honeydew;
                Width = 302;
            }
            else if (Tools.Global.ddsState == "png" || Tools.Global.ddsState == "launch-png")
            {
                Text = "Converting PNG files...";
                lbl_unpackState.Text = "Converting PNG files. Please wait...";
                pnl_windowCheck.BackColor = Color.Honeydew; BackColor = Color.Honeydew;
                Width = 302;
            }
            else if (Tools.Global.csbState == "unpack" || Tools.Global.csbState == "launch-unpack" || Tools.Global.csbState == "unpack-all")
            {
                Text = "Unpacking CSBs...";
                lbl_unpackState.Text = "Unpacking CSBs. Please wait...";
                pnl_windowCheck.BackColor = Color.AliceBlue; BackColor = Color.AliceBlue;
                Width = 209;
            }
            else if (Tools.Global.csbState == "repack")
            {
                Text = "Repacking CSBs...";
                lbl_unpackState.Text = "Repacking CSBs. Please wait...";
                pnl_windowCheck.BackColor = Color.AliceBlue; BackColor = Color.AliceBlue;
                Width = 212;
            }
            else if (Tools.Global.lubState == "decompile" || Tools.Global.lubState == "launch-decompile" || Tools.Global.lubState == "decompile-all")
            {
                Text = "Decompiling LUBs...";
                lbl_unpackState.Text = "Decompiling LUBs. Please wait...";
                pnl_windowCheck.BackColor = Color.AliceBlue; BackColor = Color.AliceBlue;
                Width = 295;
            }
            //else if (Tools.Global.mstState == "mst" || Tools.Global.mstState == "launch-mst")
            //{
            //    Text = "Decoding MSTs...";
            //    lbl_unpackState.Text = "Decoding MSTs. Please wait...";
            //    pnl_windowCheck.BackColor = Color.Thistle; BackColor = Color.Thistle;
            //    Width = 219;
            //}
            else if (Tools.Global.xnoState == "xno" || Tools.Global.xnoState == "xnm" || Tools.Global.xnoState == "launch-xno")
            {
                Text = "Converting XNOs...";
                lbl_unpackState.Text = "Converting XNOs. Please wait...";
                pnl_windowCheck.BackColor = Color.FromArgb(239, 224, 201); BackColor = Color.FromArgb(239, 224, 201);
                Width = 219;
            }
            else if (Tools.Global.exisoState == "extract")
            {
                Text = "Extracting ISO...";
                lbl_unpackState.Text = "Extracting ISO. Please wait...";
                pnl_windowCheck.BackColor = Color.Honeydew; BackColor = Color.Honeydew;
            }
        }
    }
}
