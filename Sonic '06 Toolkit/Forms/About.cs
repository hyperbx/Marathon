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
