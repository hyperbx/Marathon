﻿using System.Windows.Forms;

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
    public partial class Logo : Form
    {
        public Logo()
        {
            InitializeComponent();
        }

        void Btn_SaveToDisk_Click_1(object sender, System.EventArgs e)
        {
            if (sfd_SaveLogo.ShowDialog() == DialogResult.OK)
            {
                if (Documentation.freeModeNoMore >= 10) { Properties.Resources.sonori.Save(sfd_SaveLogo.FileName, System.Drawing.Imaging.ImageFormat.Png); }
                else { Properties.Resources.logo_main.Save(sfd_SaveLogo.FileName, System.Drawing.Imaging.ImageFormat.Png); }
            }
        }

        private void Logo_Load(object sender, System.EventArgs e)
        {
            if (Documentation.freeModeNoMore >= 10) { pic_Logo.BackgroundImage = Properties.Resources.sonori; lbl_Credit.Visible = false; }
        }
    }
}