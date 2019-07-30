using System;
using System.IO;
using System.Text;
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
    public partial class ARC_Studio : Form
    {
        public ARC_Studio()
        {
            InitializeComponent();
        }

        void mergeARC()
        {
            try
            {
                Tools.ARC.Merge(4, text_ARC1.Text, text_ARC2.Text, text_Output.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred when merging the archives.\n\n{ex}", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Tools.Notification.Dispose();
            }
        }

        void Btn_Merge_Click(object sender, EventArgs e)
        {
            if (!File.Exists(text_ARC1.Text)) MessageBox.Show("Please specify an ARC file.", "File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (!File.Exists(text_ARC2.Text)) MessageBox.Show("Please specify an ARC file.", "File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (text_Output.Text == "" || !Directory.Exists(Path.GetDirectoryName(text_Output.Text))) MessageBox.Show("Please specify an output.", "File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                mergeARC();
            }
        }

        void Btn_BrowseARC1_Click(object sender, EventArgs e)
        {
            ofd_OpenARC.Title = "Please select an ARC file...";
            ofd_OpenARC.Filter = "ARC Files|*.arc";
            ofd_OpenARC.DefaultExt = "arc";

            if (ofd_OpenARC.ShowDialog() == DialogResult.OK)
            {
                text_ARC1.Text = ofd_OpenARC.FileName;
            }
        }

        void Btn_BrowseARC2_Click(object sender, EventArgs e)
        {
            ofd_OpenARC.Title = "Please select an ARC file...";
            ofd_OpenARC.Filter = "ARC Files|*.arc";
            ofd_OpenARC.DefaultExt = "arc";

            if (ofd_OpenARC.ShowDialog() == DialogResult.OK)
            {
                text_ARC2.Text = ofd_OpenARC.FileName;
            }
        }

        void Btn_BrowseOutput_Click(object sender, EventArgs e)
        {
            sfd_Output.Title = "Save As...";
            sfd_Output.Filter = "ARC Files|*.arc";
            sfd_Output.DefaultExt = "arc";

            if (sfd_Output.ShowDialog() == DialogResult.OK)
            {
                text_Output.Text = sfd_Output.FileName;
            }
        }
    }
}
