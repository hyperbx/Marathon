using System;
using System.Windows.Forms;
using System.ComponentModel;

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

namespace Toolkit.Environment4
{
    public partial class CollisionGenerator : UserControl
    {
        public CollisionGenerator() {
            InitializeComponent();
            LoadSettings();

            Properties.Settings.Default.SettingsSaving += Settings_SettingsSaving;
            CollisionGenerator_TabControl.Height += 23;
        }

        private void Settings_SettingsSaving(object sender, CancelEventArgs e) { LoadSettings(); }

        private void LoadSettings() {

        }

        private void CollisionGenerator_Section_Click(object sender, EventArgs e) {
            foreach (Control control in Controls)
                if (control is SectionButton) ((SectionButton)control).SelectedSection = false;
            if (sender == CollisionGenerator_Section_BIN2OBJ) CollisionGenerator_TabControl.SelectedIndex = 0;
            else if (sender == CollisionGenerator_Section_OBJ2BIN) CollisionGenerator_TabControl.SelectedIndex = 1;
            Label_Title.Text = ((SectionButton)sender).SectionText;
            CollisionGenerator_TabControl.Visible = ((SectionButton)sender).SelectedSection = true;
        }
    }
}
