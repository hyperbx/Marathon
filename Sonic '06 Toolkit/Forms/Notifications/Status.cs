using System;
using System.Drawing;
using System.Windows.Forms;

// Sonic '06 Toolkit is licensed under the MIT License:
/*
 * MIT License

 * Copyright (c) 2019 Gabriel (HyperPolygon64)

 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:

 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.

 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 */

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
            if (Properties.Settings.Default.NOWLOADING == true)
            {
                NOW_LOADING.Visible = true;
                Text = "NOW LOADING...";
                lbl_unpackState.Visible = false;
                pnl_windowCheck.BackColor = Color.Black; BackColor = Color.Black;
                Width = 275;
                Height = 138;
            }
            else if (Tools.Global.arcState == "typical" || Tools.Global.arcState == "launch-typical")
            {
                NOW_LOADING.Visible = false;
                Text = "Unpacking ARC...";
                lbl_unpackState.Text = "Unpacking ARC. Please wait...";
                pnl_windowCheck.BackColor = Color.Honeydew; BackColor = Color.Honeydew;
                Width = 284;
                Height = 138;
            }
            else if (Tools.Global.arcState == "processing")
            {
                NOW_LOADING.Visible = false;
                Text = "Processing ARCs...";
                lbl_unpackState.Text = "Processing ARCs. Please wait...";
                pnl_windowCheck.BackColor = Color.Honeydew; BackColor = Color.Honeydew;
                Width = 236;
                Height = 138;
            }
            else if (Tools.Global.arcState == "save")
            {
                NOW_LOADING.Visible = false;
                Text = "Repacking ARC...";
                lbl_unpackState.Text = "Repacking ARC. Please wait...";
                pnl_windowCheck.BackColor = Color.Honeydew; BackColor = Color.Honeydew;
                Width = 284;
                Height = 138;
            }
            else if (Tools.Global.arcState == "save-as")
            {
                NOW_LOADING.Visible = false;
                Text = "Repacking ARC...";
                lbl_unpackState.Text = "Repacking ARC. Please wait...";
                pnl_windowCheck.BackColor = Color.Honeydew; BackColor = Color.Honeydew;
                Width = 284;
                Height = 138;
            }
            else if (Tools.Global.adxState == "adx" || Tools.Global.adxState == "launch-adx")
            {
                NOW_LOADING.Visible = false;
                Text = "Encoding ADX files...";
                lbl_unpackState.Text = "Encoding ADX files. Please wait...";
                pnl_windowCheck.BackColor = Color.AliceBlue; BackColor = Color.AliceBlue;
                Width = 300;
                Height = 138;
            }
            else if (Tools.Global.adxState == "wav" || Tools.Global.adxState == "launch-wav")
            {
                NOW_LOADING.Visible = false;
                Text = "Encoding WAV files...";
                lbl_unpackState.Text = "Encoding WAV files. Please wait...";
                pnl_windowCheck.BackColor = Color.AliceBlue; BackColor = Color.AliceBlue;
                Width = 300;
                Height = 138;
            }
            else if (Tools.Global.at3State == "at3" || Tools.Global.at3State == "launch-at3")
            {
                NOW_LOADING.Visible = false;
                Text = "Encoding AT3 files...";
                lbl_unpackState.Text = "Encoding AT3 files. Please wait...";
                pnl_windowCheck.BackColor = Color.AliceBlue; BackColor = Color.AliceBlue;
                Width = 300;
                Height = 138;
            }
            else if (Tools.Global.at3State == "wav")
            {
                NOW_LOADING.Visible = false;
                Text = "Encoding WAV files...";
                lbl_unpackState.Text = "Encoding WAV files. Please wait...";
                pnl_windowCheck.BackColor = Color.AliceBlue; BackColor = Color.AliceBlue;
                Width = 300;
                Height = 138;
            }
            else if (Tools.Global.ddsState == "dds" || Tools.Global.ddsState == "launch-dds")
            {
                NOW_LOADING.Visible = false;
                Text = "Converting DDS files...";
                lbl_unpackState.Text = "Converting DDS files. Please wait...";
                pnl_windowCheck.BackColor = Color.Honeydew; BackColor = Color.Honeydew;
                Width = 302;
                Height = 138;
            }
            else if (Tools.Global.ddsState == "png" || Tools.Global.ddsState == "launch-png")
            {
                NOW_LOADING.Visible = false;
                Text = "Converting PNG files...";
                lbl_unpackState.Text = "Converting PNG files. Please wait...";
                pnl_windowCheck.BackColor = Color.Honeydew; BackColor = Color.Honeydew;
                Width = 302;
                Height = 138;
            }
            else if (Tools.Global.csbState == "unpack" || Tools.Global.csbState == "launch-unpack" || Tools.Global.csbState == "unpack-all")
            {
                NOW_LOADING.Visible = false;
                Text = "Unpacking CSBs...";
                lbl_unpackState.Text = "Unpacking CSBs. Please wait...";
                pnl_windowCheck.BackColor = Color.AliceBlue; BackColor = Color.AliceBlue;
                Width = 209;
                Height = 138;
            }
            else if (Tools.Global.csbState == "repack")
            {
                NOW_LOADING.Visible = false;
                Text = "Repacking CSBs...";
                lbl_unpackState.Text = "Repacking CSBs. Please wait...";
                pnl_windowCheck.BackColor = Color.AliceBlue; BackColor = Color.AliceBlue;
                Width = 212;
                Height = 138;
            }
            else if (Tools.Global.lubState == "decompile" || Tools.Global.lubState == "launch-decompile" || Tools.Global.lubState == "decompile-all")
            {
                NOW_LOADING.Visible = false;
                Text = "Decompiling LUBs...";
                lbl_unpackState.Text = "Decompiling LUBs. Please wait...";
                pnl_windowCheck.BackColor = Color.AliceBlue; BackColor = Color.AliceBlue;
                Width = 295;
                Height = 138;
            }
            //else if (Tools.Global.mstState == "mst" || Tools.Global.mstState == "launch-mst")
            //{
            //    NOW_LOADING.Visible = false;
            //    Text = "Decoding MSTs...";
            //    lbl_unpackState.Text = "Decoding MSTs. Please wait...";
            //    pnl_windowCheck.BackColor = Color.Thistle; BackColor = Color.Thistle;
            //    Width = 219;
            //    Height = 138;
            //}
            else if (Tools.Global.xnoState == "xno" || Tools.Global.xnoState == "xnm" || Tools.Global.xnoState == "launch-xno")
            {
                NOW_LOADING.Visible = false;
                Text = "Converting XNOs...";
                lbl_unpackState.Text = "Converting XNOs. Please wait...";
                pnl_windowCheck.BackColor = Color.FromArgb(239, 224, 201); BackColor = Color.FromArgb(239, 224, 201);
                Width = 219;
                Height = 138;
            }
            else if (Tools.Global.xmaState == "xma" || Tools.Global.xmaState == "launch-xma" || Tools.Global.xmaState == "xma-repatch" || Tools.Global.xmaState == "xma-launch-repatch")
            {
                NOW_LOADING.Visible = false;
                Text = "Encoding XMAs...";
                lbl_unpackState.Text = "Encoding XMAs. Please wait...";
                pnl_windowCheck.BackColor = Color.FromArgb(239, 224, 201); BackColor = Color.FromArgb(239, 224, 201);
                Width = 284;
                Height = 138;
            }
            else if (Tools.Global.xmaState == "wav")
            {
                NOW_LOADING.Visible = false;
                Text = "Encoding WAV files...";
                lbl_unpackState.Text = "Encoding WAV files. Please wait...";
                pnl_windowCheck.BackColor = Color.FromArgb(239, 224, 201); BackColor = Color.FromArgb(239, 224, 201);
                Width = 300;
                Height = 138;
            }
            else if (Tools.Global.exisoState == "extract")
            {
                NOW_LOADING.Visible = false;
                Text = "Extracting ISO...";
                lbl_unpackState.Text = "Extracting ISO. Please wait...";
                pnl_windowCheck.BackColor = Color.Honeydew; BackColor = Color.Honeydew;
                Width = 219;
                Height = 138;
            }
        }
    }
}
