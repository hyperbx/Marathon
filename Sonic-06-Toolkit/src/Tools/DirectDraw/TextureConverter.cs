using System;
using System.IO;
using System.Linq;
using Toolkit.Text;
using Toolkit.Tools;
using System.Drawing;
using Toolkit.EnvironmentX;
using System.Windows.Forms;
using System.Collections.Generic;

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

namespace Toolkit
{
    public partial class TextureConverter : Form
    {
        private Main mainForm = null;
        private string location = Paths.currentPath;
        private string compression = string.Empty;

        public TextureConverter(Form callingForm) {
            mainForm = callingForm as Main;
            InitializeComponent();
            tm_NoCheckOnClickTimer.Start();
            compression = Properties.Settings.Default.tex_Compression;

            switch (compression) {
                case "-f BC1_UNORM":
                    compression_DXT1.Checked = true;
                    compression_DXT3.Checked = false;
                    compression_DXT5.Checked = false;
                    compression_A8R8G8B8.Checked = false;
                    break;
                case "-f BC2_UNORM":
                    compression_DXT1.Checked = false;
                    compression_DXT3.Checked = true;
                    compression_DXT5.Checked = false;
                    compression_A8R8G8B8.Checked = false;
                    break;
                case "-f BC3_UNORM":
                    compression_DXT1.Checked = false;
                    compression_DXT3.Checked = false;
                    compression_DXT5.Checked = true;
                    compression_A8R8G8B8.Checked = false;
                    break;
                case "-f R8G8B8A8_UNORM":
                    compression_DXT1.Checked = false;
                    compression_DXT3.Checked = false;
                    compression_DXT5.Checked = false;
                    compression_A8R8G8B8.Checked = true;
                    break;
            }
        }

        private void TextureConverter_Load(object sender, EventArgs e) {
            clb_IMGs.Items.Clear();
            btn_Process.Enabled = false;

            if (Directory.GetFiles(location, "*.dds").Length > 0) {
                modes_DDStoPNG.Checked = true;
                modes_PNGtoDDS.Checked = false;
                options_Compression.Visible = false;
            } else if (Directory.GetFiles(location, "*.png").Length > 0) {
                modes_DDStoPNG.Checked = false;
                modes_PNGtoDDS.Checked = true;
                options_Compression.Visible = true;
            } else {
                MessageBox.Show(SystemMessages.msg_NoConvertableFiles, SystemMessages.tl_NoFilesAvailable(string.Empty), MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();
            }
        }

        private void Clb_IMGs_SelectedIndexChanged(object sender, EventArgs e) {
            try {
                if (Path.GetExtension(clb_IMGs.SelectedItem.ToString().ToLower()) == ".dds") {
                    var image = new DDSImage(File.ReadAllBytes(Path.Combine(location, clb_IMGs.SelectedItem.ToString())));
                    if (!DDSImage.invalid) pic_Preview.BackgroundImage = image.images[0];
                    else pic_Preview.BackgroundImage = Properties.Resources.logo_exception;
                }
                else if (Path.GetExtension(clb_IMGs.SelectedItem.ToString().ToLower()) == ".png")
                    pic_Preview.BackgroundImage = Image.FromFile(Path.Combine(location, clb_IMGs.SelectedItem.ToString()));
            } catch { pic_Preview.BackgroundImage = Properties.Resources.logo_exception; }
        }

        private async void Btn_Process_Click(object sender, EventArgs e) {
            List<object> filesToProcess = clb_IMGs.CheckedItems.OfType<object>().ToList();
            if (modes_DDStoPNG.Checked) {
                foreach (string DDS in filesToProcess) {
                    mainForm.Status = StatusMessages.cmn_Converting(DDS, "PNG", false);
                    var convert = await ProcessAsyncHelper.ExecuteShellCommand(Paths.DDSTool,
                                        $"-ft png -srgb \"{Path.Combine(location, DDS)}\" " +
                                        $"\"{Path.GetFileNameWithoutExtension(DDS)}.png\"",
                                        location,
                                        100000);
                    if (convert.Completed)
                        if (convert.ExitCode != 0)
                            MessageBox.Show($"{SystemMessages.ex_DDSConvertError}\n\n{convert.Output}", SystemMessages.tl_FatalError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            } else {
                foreach (string PNG in filesToProcess) {
                    mainForm.Status = StatusMessages.cmn_Converting(PNG, "DDS", false);
                    var convert = await ProcessAsyncHelper.ExecuteShellCommand(Paths.DDSTool,
                                        $"-ft dds -srgb {compression} \"{Path.Combine(location, PNG)}\" " +
                                        $"\"{Path.GetFileNameWithoutExtension(PNG)}.dds\"",
                                        location,
                                        100000);
                    if (convert.Completed)
                        if (convert.ExitCode != 0)
                            MessageBox.Show($"{SystemMessages.ex_DDSConvertError}\n\n{convert.Output}", SystemMessages.tl_FatalError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void Modes_DDStoPNG_CheckedChanged(object sender, EventArgs e) {
            if (modes_DDStoPNG.Checked) {
                clb_IMGs.Items.Clear();
                btn_Process.Enabled = false;

                foreach (string DDS in Directory.GetFiles(location, "*.dds", SearchOption.TopDirectoryOnly))
                    if (File.Exists(DDS) && Verification.VerifyMagicNumberCommon(DDS))
                        clb_IMGs.Items.Add(Path.GetFileName(DDS));

                modes_PNGtoDDS.Checked = false;
                options_Compression.Visible = false;

                if (Directory.GetFiles(location, "*.dds").Length == 0) {
                    MessageBox.Show(SystemMessages.msg_NoConvertableFiles, SystemMessages.tl_NoFilesAvailable("DDS"), MessageBoxButtons.OK, MessageBoxIcon.Information);

                    if (Directory.GetFiles(location, "*.png").Length == 0) Close();
                    else {
                        modes_DDStoPNG.Checked = false;
                        modes_PNGtoDDS.Checked = true;
                        options_Compression.Visible = true;
                    }
                }
            } else {
                if (Directory.GetFiles(location, "*.png").Length > 0) {
                    modes_DDStoPNG.Checked = false;
                    modes_PNGtoDDS.Checked = true;
                    options_Compression.Visible = true;
                } else {
                    modes_DDStoPNG.Checked = true;
                }
            }
        }

        private void Modes_PNGtoDDS_CheckedChanged(object sender, EventArgs e) {
            if (modes_PNGtoDDS.Checked) {
                clb_IMGs.Items.Clear();
                btn_Process.Enabled = false;

                foreach (string PNG in Directory.GetFiles(location, "*.png", SearchOption.TopDirectoryOnly))
                    if (File.Exists(PNG) && Verification.VerifyMagicNumberCommon(PNG))
                        clb_IMGs.Items.Add(Path.GetFileName(PNG));

                modes_DDStoPNG.Checked = false;
                options_Compression.Visible = true;

                if (Directory.GetFiles(location, "*.png").Length == 0) {
                    MessageBox.Show(SystemMessages.msg_NoConvertableFiles, SystemMessages.tl_NoFilesAvailable("PNG"), MessageBoxButtons.OK, MessageBoxIcon.Information);

                    if (Directory.GetFiles(location, "*.dds").Length == 0) Close();
                    else {
                        modes_DDStoPNG.Checked = true;
                        modes_PNGtoDDS.Checked = false;
                        options_Compression.Visible = false;
                    }
                }
            } else {
                if (Directory.GetFiles(location, "*.dds").Length > 0) {
                    modes_DDStoPNG.Checked = true;
                    modes_PNGtoDDS.Checked = false;
                    options_Compression.Visible = false;
                } else {
                    modes_PNGtoDDS.Checked = true;
                }
            }
        }

        private void Tm_NoCheckOnClickTimer_Tick(object sender, EventArgs e) {
            btn_Process.Enabled = clb_IMGs.CheckedItems.Count > 0;
        }

        private void Btn_SelectAll_Click(object sender, EventArgs e) {
            for (int i = 0; i < clb_IMGs.Items.Count; i++) clb_IMGs.SetItemChecked(i, true);
            btn_Process.Enabled = true;
        }

        private void Btn_DeselectAll_Click(object sender, EventArgs e) {
            for (int i = 0; i < clb_IMGs.Items.Count; i++) clb_IMGs.SetItemChecked(i, false);
            btn_Process.Enabled = false;
        }

        private void Compression_DXT1_CheckedChanged(object sender, EventArgs e) {
            Properties.Settings.Default.tex_Compression = compression = "-f BC1_UNORM";

            if (compression_DXT1.Checked) {
                compression_DXT3.Checked = false;
                compression_DXT5.Checked = false;
                compression_A8R8G8B8.Checked = false;
            }
        }

        private void Compression_DXT3_CheckedChanged(object sender, EventArgs e) {
            Properties.Settings.Default.tex_Compression = compression = "-f BC2_UNORM";

            if (compression_DXT3.Checked) {
                compression_DXT1.Checked = false;
                compression_DXT5.Checked = false;
                compression_A8R8G8B8.Checked = false;
            }
        }

        private void Compression_DXT5_CheckedChanged(object sender, EventArgs e) {
            Properties.Settings.Default.tex_Compression = compression = "-f BC3_UNORM";

            if (compression_DXT5.Checked) {
                compression_DXT1.Checked = false;
                compression_DXT3.Checked = false;
                compression_A8R8G8B8.Checked = false;
            }
        }

        private void Compression_A8R8G8B8_CheckedChanged(object sender, EventArgs e) {
            Properties.Settings.Default.tex_Compression = compression = "-f R8G8B8A8_UNORM";

            if (compression_A8R8G8B8.Checked) {
                compression_DXT1.Checked = false;
                compression_DXT3.Checked = false;
                compression_DXT5.Checked = false;
            }
        }
    }
}
