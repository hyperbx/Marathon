using System;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
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
        public int notifState = 0;
        public string notifModifier = string.Empty;

        public Status(int state, string modifier)
        {
            notifState = state;
            notifModifier = modifier;
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
            else if (new[] { 0, 1 }.Contains(notifState) & notifModifier == "ARC")
            {
                NOW_LOADING.Visible = false;
                Text = "Unpacking ARC...";
                lbl_unpackState.Text = "Unpacking ARC. Please wait...";
                pnl_windowCheck.BackColor = Color.Honeydew; BackColor = Color.Honeydew;
                Width = 284;
                Height = 138;
            }
            else if (notifState == 4 && notifModifier == "ARC")
            {
                NOW_LOADING.Visible = false;
                Text = "Processing ARCs...";
                lbl_unpackState.Text = "Processing ARCs. Please wait...";
                pnl_windowCheck.BackColor = Color.Honeydew; BackColor = Color.Honeydew;
                Width = 288;
                Height = 138;
            }
            else if (new[] { 2, 3 }.Contains(notifState) & notifModifier == "ARC")
            {
                NOW_LOADING.Visible = false;
                Text = "Repacking ARC...";
                lbl_unpackState.Text = "Repacking ARC. Please wait...";
                pnl_windowCheck.BackColor = Color.Honeydew; BackColor = Color.Honeydew;
                Width = 284;
                Height = 138;
            }
            else if (new[] { 0, 1 }.Contains(notifState) && notifModifier == "BIN")
            {
                NOW_LOADING.Visible = false;
                Text = "Exporting BIN files...";
                lbl_unpackState.Text = "Exporting BIN files. Please wait...";
                pnl_windowCheck.BackColor = Color.Honeydew; BackColor = Color.Honeydew;
                Width = 295;
                Height = 138;
            }
            else if (new[] { 2, 3 }.Contains(notifState) && notifModifier == "BIN")
            {
                NOW_LOADING.Visible = false;
                Text = "Importing collision...";
                lbl_unpackState.Text = "Importing collision. Please wait...";
                pnl_windowCheck.BackColor = Color.Honeydew; BackColor = Color.Honeydew;
                Width = 290;
                Height = 138;
            }
            else if (new[] { 0, 1 }.Contains(notifState) && notifModifier == "ADX")
            {
                NOW_LOADING.Visible = false;
                Text = "Encoding ADX files...";
                lbl_unpackState.Text = "Encoding ADX files. Please wait...";
                pnl_windowCheck.BackColor = Color.AliceBlue; BackColor = Color.AliceBlue;
                Width = 300;
                Height = 138;
            }
            else if (notifState == 2 && notifModifier == "ADX")
            {
                NOW_LOADING.Visible = false;
                Text = "Encoding WAV files...";
                lbl_unpackState.Text = "Encoding WAV files. Please wait...";
                pnl_windowCheck.BackColor = Color.AliceBlue; BackColor = Color.AliceBlue;
                Width = 300;
                Height = 138;
            }
            else if (new[] { 0, 1 }.Contains(notifState) && notifModifier == "AT3")
            {
                NOW_LOADING.Visible = false;
                Text = "Encoding AT3 files...";
                lbl_unpackState.Text = "Encoding AT3 files. Please wait...";
                pnl_windowCheck.BackColor = Color.AliceBlue; BackColor = Color.AliceBlue;
                Width = 295;
                Height = 138;
            }
            else if (notifState == 2 && notifModifier == "AT3")
            {
                NOW_LOADING.Visible = false;
                Text = "Encoding WAV files...";
                lbl_unpackState.Text = "Encoding WAV files. Please wait...";
                pnl_windowCheck.BackColor = Color.AliceBlue; BackColor = Color.AliceBlue;
                Width = 300;
                Height = 138;
            }
            else if (new[] { 0, 1 }.Contains(notifState) && notifModifier == "DDS")
            {
                NOW_LOADING.Visible = false;
                Text = "Converting DDS files...";
                lbl_unpackState.Text = "Converting DDS files. Please wait...";
                pnl_windowCheck.BackColor = Color.Honeydew; BackColor = Color.Honeydew;
                Width = 304;
                Height = 138;
            }
            else if (new[] { 0, 1 }.Contains(notifState) && notifModifier == "PNG")
            {
                NOW_LOADING.Visible = false;
                Text = "Converting PNG files...";
                lbl_unpackState.Text = "Converting PNG files. Please wait...";
                pnl_windowCheck.BackColor = Color.Honeydew; BackColor = Color.Honeydew;
                Width = 302;
                Height = 138;
            }
            else if (new[] { 0, 1 }.Contains(notifState) && notifModifier == "CSB")
            {
                NOW_LOADING.Visible = false;
                Text = "Unpacking CSBs...";
                lbl_unpackState.Text = "Unpacking CSBs. Please wait...";
                pnl_windowCheck.BackColor = Color.AliceBlue; BackColor = Color.AliceBlue;
                Width = 290;
                Height = 138;
            }
            else if (new[] { 2, 3 }.Contains(notifState) && notifModifier == "CSB")
            {
                NOW_LOADING.Visible = false;
                Text = "Repacking CSBs...";
                lbl_unpackState.Text = "Repacking CSBs. Please wait...";
                pnl_windowCheck.BackColor = Color.AliceBlue; BackColor = Color.AliceBlue;
                Width = 288;
                Height = 138;
            }
            else if (notifModifier == "LUB")
            {
                NOW_LOADING.Visible = false;
                Text = "Decompiling LUBs...";
                lbl_unpackState.Text = "Decompiling LUBs. Please wait...";
                pnl_windowCheck.BackColor = Color.AliceBlue; BackColor = Color.AliceBlue;
                Width = 295;
                Height = 138;
            }
            else if (new[] { 0, 1 }.Contains(notifState) && notifModifier == "XNO")
            {
                NOW_LOADING.Visible = false;
                Text = "Converting XNOs...";
                lbl_unpackState.Text = "Converting XNOs. Please wait...";
                pnl_windowCheck.BackColor = Color.FromArgb(239, 224, 201); BackColor = Color.FromArgb(239, 224, 201);
                Width = 290;
                Height = 138;
            }
            else if (new[] { 0, 1, 3, 4 }.Contains(notifState) && notifModifier == "XMA")
            {
                NOW_LOADING.Visible = false;
                Text = "Encoding XMAs...";
                lbl_unpackState.Text = "Encoding XMAs. Please wait...";
                pnl_windowCheck.BackColor = Color.FromArgb(239, 224, 201); BackColor = Color.FromArgb(239, 224, 201);
                Width = 284;
                Height = 138;
            }
            else if (notifState == 2 && notifModifier == "XMA")
            {
                NOW_LOADING.Visible = false;
                Text = "Encoding WAV files...";
                lbl_unpackState.Text = "Encoding WAV files. Please wait...";
                pnl_windowCheck.BackColor = Color.FromArgb(239, 224, 201); BackColor = Color.FromArgb(239, 224, 201);
                Width = 300;
                Height = 138;
            }
            else if (notifState == 0 && notifModifier == "exiso")
            {
                NOW_LOADING.Visible = false;
                Text = "Extracting ISO...";
                lbl_unpackState.Text = "Extracting ISO. Please wait...";
                pnl_windowCheck.BackColor = Color.Honeydew; BackColor = Color.Honeydew;
                Width = 278;
                Height = 138;
            }
            else if (notifState == 0 && notifModifier == "deep-search")
            {
                NOW_LOADING.Visible = false;
                Text = "Searching ARCs...";
                lbl_unpackState.Text = "Searching ARCs. Please wait...";
                pnl_windowCheck.BackColor = Color.Honeydew; BackColor = Color.Honeydew;
                Width = 278;
                Height = 138;
            }

            pnl_windowCheck.Width = Width;
        }
    }
}
