using System;
using System.Diagnostics;
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
    public partial class About : Form
    {
        public About()
        {
            InitializeComponent();
        }

        public string changeLogs = new Tools.TimedWebClient { Timeout = 100000 }.DownloadString("https://segacarnival.com/hyper/updates/changelogs.txt");

        void About_Load(object sender, EventArgs e)
        {
            lbl_versionNumber.Text = Tools.Global.versionNumber;
            if (Tools.Global.versionNumber.Contains("-indev")) lbl_versionNumber.Left -= 44;
            else if (Tools.Global.versionNumber.Contains("-test")) lbl_versionNumber.Left -= 32;
            else if (Tools.Global.versionNumber.Contains("_")) lbl_versionNumber.Left -= 24;

            web_Credits.DocumentText = $"<html><body><style>{Properties.Resources.CreditsStyleSheet}</style><center>" +
                $"<h1>Sonic '06 Toolkit</h1>" +
                $"<center>Developed by <a href=\"http://hyper-dev.xyz/main/\" target=\"_blank\">Hyper Development Team</a></center>" +
                $"<center>Hosted by <a href=\"https://segacarnival.com/forum/index.php\" target=\"_blank\">SEGA Carnival</a></center>" +
                $"<center>Licensed under the <a href=\"https://github.com/HyperPolygon64/Sonic-06-Toolkit/blob/master/LICENSE\" target=\"_blank\">MIT License</a></center>" +
                $"<p align=\"left\">Contributors:<br>" +
                $"<a href=\"https://github.com/HyperPolygon64\" target=\"_blank\">Hyper</a> - Lead Developer<br>" +
                $"Shadow LAG - <a href=\"https://drive.google.com/file/d/1auuHtR91zWwNDih4l6ddlwcCbczW_b0L/view\" target=\"_blank\">Sonic '06 SDK</a><br>" +
                $"xose - ARC Unpacker<br>" +
                $"g0ldenlink - ARC Repacker<br>" +
                $"<a href=\"https://github.com/NatsumiFox\" target=\"_blank\">Natsumi</a> - <a href=\"https://github.com/HyperPolygon64/Sonic-06-Toolkit/commit/54f1ee53a1155b990058c9d8d15e91a8e09a5f23#diff-e664d6debe0eb815513a7b8c8bb39512R52\" target=\"_blank\">Deep Search</a>, <a href=\"https://github.com/HyperPolygon64/Sonic-06-Toolkit/commit/4e1e7e7b8e50eab1a0dbbe3f3c2b969b7a51c3c3\" target=\"_blank\">MST Decoder (1.83)</a> & Design Guidance<br>" +
                $"<a href=\"https://github.com/blueskythlikesclouds\" target=\"_blank\">Skyth</a> - <a href=\"https://github.com/blueskythlikesclouds/SkythTools/blob/master/Sonic%20'06/xno2dae.exe\" target=\"_blank\">XNO Converter</a>, <a href=\"https://github.com/blueskythlikesclouds/SonicAudioTools\" target=\"_blank\">Sonic Audio Tools</a> and <a href=\"https://github.com/DarioSamo/libgens-sonicglvl/tree/master/src/LibS06\" target=\"_blank\">LibS06</a><br>" +
                $"<a href=\"https://github.com/DarioSamo\" target=\"_blank\">DarioSamo</a> - <a href=\"https://github.com/blueskythlikesclouds/SkythTools/blob/master/Sonic%20'06/xno2dae.exe\" target=\"_blank\">XNO Converter</a> and <a href=\"https://github.com/DarioSamo/libgens-sonicglvl/tree/master/src/LibS06\" target=\"_blank\">LibS06</a><br>" +
                $"<a href=\"https://www.criware.com/en/\" target=\"_blank\">CRI Middleware Co.</a> - CRI Atom Encoder<br>" +
                $"<a href=\"https://github.com/Radfordhound\" target=\"_blank\">Radfordhound</a> - <a href=\"https://github.com/Radfordhound/HedgeLib\" target=\"_blank\">HedgeLib</a><br>" +
                $"<a href=\"in@fishtank.com\" target=\"_blank\">in</a> - <a href=\"https://github.com/XboxDev/extract-xiso\" target=\"_blank\">extract-xiso</a><br>" +
                $"<a href=\"https://www.playstation.com/en-gb/\" target=\"_blank\">SONY Computer Entertainment Inc.</a> - ATRAC3plus Codec TOOL<br>" +
                $"<a href=\"https://github.com/GerbilSoft\" target=\"_blank\">GerbilSoft</a> - <a href=\"https://github.com/GerbilSoft/mst06\" target=\"_blank\">mst06</a><br>" +
                $"<a href=\"https://www.microsoft.com/\" target=\"_blank\">Microsoft Corporation</a> - XMA Encode 2008<br>" +
                $"<a href=\"https://www.youtube.com/channel/UC35wsF1NUwoUWmw2DLz6uJg\" target=\"_blank\">Nonami</a> - XMA and XNO Backface Culling Research<br>" +
                $"Reimous - XMA Research<br>" +
                $"<a href=\"https://twitter.com/nectarhime\" target=\"_blank\">Sable</a> - BIN Studio Logo<br>" +
                $"Sajid - <a href=\"https://github.com/DarioSamo/libgens-sonicglvl/tree/master/src/LibS06\" target=\"_blank\">LibS06</a><br>" +
                $"<a href=\"https://github.com/Knuxfan24\" target=\"_blank\">Knuxfan24</a> - <a href=\"https://github.com/DarioSamo/libgens-sonicglvl/tree/master/src/LibS06\" target=\"_blank\">LibS06</a>" +
                $"</p></body></html>";
        }

        void Btn_Close_Click(object sender, EventArgs e)
        {
            Close();
        }

        void Lbl_Title_Click(object sender, EventArgs e)
        {
            new Logo().ShowDialog();
        }

        private void Btn_GitHub_Click_1(object sender, EventArgs e)
        {
            Process.Start("https://github.com/HyperPolygon64/Sonic-06-Toolkit");
        }

        //void Btn_Credits_Click(object sender, EventArgs e)
        //{
        //    MessageBox.Show(
        //        "Sonic '06 Toolkit\n" +
        //        "Developed by Hyper Development Team\n" +
        //        "Hosted by SEGA Carnival\n\n" +
        //        "" +
        //        "Licensed under the GNU General Public License v2.0\n\n" +
        //        "" +
        //        "Contributors:\n" +
        //        "Hyper - Lead Developer\n" +
        //        "Shadow LAG - Sonic '06 SDK\n" +
        //        "xose - ARC Unpacker\n" +
        //        "g0ldenlink - ARC Repacker\n" +
        //        "Natsumi - MST Decoder (1.83) & Design Guidance\n" +
        //        "Skyth - XNO Converter & Sonic Audio Tools\n" +
        //        "DarioSamo - XNO Converter\n" +
        //        "CRI Middleware Co. - ADX Encoder\n" +
        //        "Radfordhound - HedgeLib\n" +
        //        "in - extract-xiso\n" +
        //        "Aiyyo - extract-xiso\n" +
        //        "somski - extract-xiso\n" +
        //        "SONY Computer Entertainment Inc. - AT3 Tool",
        //        "Credits", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //}
    }
}
