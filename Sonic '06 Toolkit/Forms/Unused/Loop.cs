using System;
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
    public partial class Loop : Form
    {
        public Loop()
        {
            InitializeComponent();
        }

        void Btn_Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        void Btn_Confirm_Click(object sender, EventArgs e)
        {
            if (track_Start.Value > track_End.Value) MessageBox.Show("The start of the loop can't be initialised after the end...", "Stupid Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                AT3_Studio.wholeLoop = "";
                AT3_Studio.beginLoop = "-loop ";
                AT3_Studio.startLoop = track_Start.Value.ToString() + " ";
                AT3_Studio.endLoop = track_End.Value.ToString() + " ";
                Close();
            }
        }
    }
}
