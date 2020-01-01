using System.Diagnostics;
using Toolkit.EnvironmentX;
using System.Windows.Forms;

// Sonic '06 Toolkit is licensed under the MIT License:
/*
 * MIT License

 * Copyright (c) 2020 Gabriel (HyperPolygon64)

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

namespace Toolkit
{
    public partial class ToolkitAbout : Form
    {
        public ToolkitAbout() {
            InitializeComponent();
            lbl_versionNumber.Text = Main.versionNumber;
        }

        private void Link_Hyper_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            Process.Start("https://www.github.com/HyperPolygon64");
        }

        private void Link_GerbilSoft_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            Process.Start("https://www.github.com/GerbilSoft");
        }

        private void Link_Sable_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            Process.Start("https://www.twitter.com/nectarhime");
        }

        private void Link_ShadowLAG_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            Process.Start("https://www.github.com/lllsondowlll");
        }

        private void Link_SEGACarnival_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            Process.Start("https://www.segacarnival.com/");
        }

        private void Link_Nonami_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            Process.Start("https://www.youtube.com/channel/UC35wsF1NUwoUWmw2DLz6uJg");
        }

        private void Link_Reimous_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            Process.Start("https://www.youtube.com/channel/UC3ACu6igwlAIckO9Gg2i4PA");
        }

        private void Link_Radfordhound_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            Process.Start("https://www.github.com/Radfordhound");
        }

        private void Link_Skyth_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            Process.Start("https://www.github.com/blueskythlikesclouds");
        }

        private void Link_Sajid_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            Process.Start("https://www.github.com/Sajidur78");
        }

        private void Link_DarioSamo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            Process.Start("https://www.github.com/DarioSamo");
        }

        private void Link_xorloser_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            Process.Start("http://www.xorloser.com/");
        }

        private void Link_Natsumi_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            Process.Start("https://www.github.com/NatsumiFox");
        }

        private void Link_CriWare_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            Process.Start("https://www.criware.com/en/");
        }

        private void Link_XboxDev_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            Process.Start("https://github.com/XboxDev");
        }
    }
}
