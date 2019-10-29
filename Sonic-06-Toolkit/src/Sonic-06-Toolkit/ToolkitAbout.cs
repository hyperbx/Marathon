using System;
using System.Diagnostics;
using Toolkit.EnvironmentX;
using System.Windows.Forms;

namespace Toolkit
{
    public partial class ToolkitAbout : Form
    {
        public ToolkitAbout() {
            InitializeComponent();
            lbl_VersionNumber.Text = Main.versionNumber;
        }

        private void Link_Hyper_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            Process.Start("https://github.com/HyperPolygon64");
        }

        private void Link_GerbilSoft_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            Process.Start("https://github.com/GerbilSoft");
        }

        private void Link_Sable_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            Process.Start("https://twitter.com/nectarhime");
        }

        private void Link_Radfordhound_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            Process.Start("https://github.com/Radfordhound");
        }

        private void Link_ShadowLAG_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            Process.Start("https://github.com/lllsondowlll");
        }

        private void Link_Skyth_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            Process.Start("https://github.com/blueskythlikesclouds");
        }

        private void Link_DarioSamo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            Process.Start("https://github.com/DarioSamo");
        }

        private void Link_xorloser_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            Process.Start("http://www.xorloser.com/");
        }

        private void Link_SEGACarnival_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            Process.Start("https://www.segacarnival.com/");
        }
    }
}
