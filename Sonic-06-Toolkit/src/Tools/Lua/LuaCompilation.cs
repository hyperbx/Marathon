using System;
using System.IO;
using System.Linq;
using Toolkit.Text;
using System.Windows.Forms;
using Toolkit.EnvironmentX;
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

namespace Toolkit.Tools
{
    public partial class LuaCompilation : Form
    {
        private Main mainForm = null;
        private string location = Paths.currentPath;

        public LuaCompilation(Form callingForm) {
            mainForm = callingForm as Main;
            InitializeComponent();
        }

        private void LuaCompilation_Load(object sender, EventArgs e) {
            combo_Mode.SelectedIndex = 0;
        }

        private void ListVerifiedLuaBinaries() {
            clb_LUBs.Items.Clear();
            btn_Process.Enabled = false;

            if (Directory.GetFiles(location, "*.lub").Length > 0)
                if (combo_Mode.SelectedIndex == 0) {
                    foreach (string LUB in Directory.GetFiles(location, "*.lub", SearchOption.TopDirectoryOnly))
                        if (File.Exists(LUB) && Verification.VerifyMagicNumberExtended(LUB))
                            clb_LUBs.Items.Add(Path.GetFileName(LUB));

                    if (clb_LUBs.Items.Count == 0)
                        combo_Mode.SelectedIndex = 1;
                } else {
                    foreach (string LUB in Directory.GetFiles(location, "*.lub", SearchOption.TopDirectoryOnly))
                        if (File.Exists(LUB) && !Verification.VerifyMagicNumberExtended(LUB))
                            clb_LUBs.Items.Add(Path.GetFileName(LUB));

                    if (clb_LUBs.Items.Count == 0)
                        combo_Mode.SelectedIndex = 0;
                }
            else {
                MessageBox.Show(SystemMessages.msg_NoCompilableFiles, SystemMessages.tl_NoFilesAvailable(string.Empty), MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();
            } 
        }

        private void Combo_Mode_SelectedIndexChanged(object sender, EventArgs e) {
            clb_LUBs.Items.Clear();
            btn_Process.Enabled = false;

            if (combo_Mode.SelectedIndex == 0) {
                Text = "Lua Decompiler (experimental)";
                lbl_Title.Text = "Lua Decompiler";
                btn_Process.Text = "Decompile";
                lbl_Experimental.Visible = true;
                lbl_Title.Top -= 5;

                foreach (string LUB in Directory.GetFiles(location, "*.lub", SearchOption.TopDirectoryOnly))
                    if (File.Exists(LUB) && Verification.VerifyMagicNumberExtended(LUB))
                        clb_LUBs.Items.Add(Path.GetFileName(LUB));

                if (Directory.GetFiles(location, "*.lub").Length == 0) {
                    MessageBox.Show(SystemMessages.msg_NoDecompilableFiles, SystemMessages.tl_NoFilesAvailable("LUB"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                } else if (clb_LUBs.Items.Count == 0)
                    combo_Mode.SelectedIndex = 1;
            } else if (combo_Mode.SelectedIndex == 1) {
                Text = "Lua Compiler";
                lbl_Title.Text = "Lua Compiler";
                btn_Process.Text = "Compile";
                lbl_Experimental.Visible = false;
                lbl_Title.Top += 5;

                foreach (string LUB in Directory.GetFiles(location, "*.lub", SearchOption.TopDirectoryOnly))
                    if (File.Exists(LUB) && !Verification.VerifyMagicNumberExtended(LUB))
                        clb_LUBs.Items.Add(Path.GetFileName(LUB));

                if (Directory.GetFiles(location, "*.lub").Length == 0 || clb_LUBs.Items.Count == 0) {
                    MessageBox.Show(SystemMessages.msg_NoCompilableFiles, SystemMessages.tl_NoFilesAvailable("LUB"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    combo_Mode.SelectedIndex = 0;
                }
            }
        }

        private async void Btn_Decompile_Click(object sender, EventArgs e) {
            List<object> filesToProcess = clb_LUBs.CheckedItems.OfType<object>().ToList();
            if (combo_Mode.SelectedIndex == 0) {
                try {
                    if (Verification.VerifyApplicationIntegrity(Paths.LuaDecompiler))
                        foreach (string LUB in filesToProcess)
                            if (Verification.VerifyMagicNumberExtended(Path.Combine(location, LUB))) {
                                mainForm.Status = StatusMessages.lua_Decompiling(LUB, false);
                                var decompile = await ProcessAsyncHelper.ExecuteShellCommand("java.exe",
                                                      $"-jar \"{Paths.LuaDecompiler}\" \"{Path.Combine(location, LUB)}\"",
                                                      Application.StartupPath,
                                                      100000);
                                if (decompile.Completed)
                                    if (decompile.ExitCode == 0)
                                        File.WriteAllText(Path.Combine(location, LUB), decompile.Output);
                                    else
                                        MessageBox.Show($"{SystemMessages.ex_LUBDecompileError}\n\n{decompile.Error}", SystemMessages.tl_FatalError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                    ListVerifiedLuaBinaries();
                } catch (Exception ex) {
                    MessageBox.Show($"{SystemMessages.ex_LUBDecompileError}\n\n{ex}", SystemMessages.tl_FatalError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            } else {
                try {
                    if (Verification.VerifyApplicationIntegrity(Paths.LuaCompiler))
                        foreach (string LUB in filesToProcess) {
                            mainForm.Status = StatusMessages.lua_Compiling(LUB, false);
                            var compile = await ProcessAsyncHelper.ExecuteShellCommand(Paths.LuaCompiler,
                                                $"-o \"{Path.Combine(location, LUB)}\" \"{Path.Combine(location, LUB)}\"",
                                                Application.StartupPath,
                                                100000);
                            if (compile.Completed)
                                if (compile.ExitCode != 0)
                                    MessageBox.Show($"{SystemMessages.ex_LUBCompileError}\n\n{compile.Error}", SystemMessages.tl_FatalError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    ListVerifiedLuaBinaries();
                } catch (Exception ex) {
                    MessageBox.Show($"{SystemMessages.ex_LUBDecompileError}\n\n{ex}", SystemMessages.tl_FatalError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void Clb_LUBs_SelectedIndexChanged(object sender, EventArgs e) {
            clb_LUBs.ClearSelected();
            btn_Process.Enabled = clb_LUBs.CheckedItems.Count > 0;
        }

        private void Btn_SelectAll_Click(object sender, EventArgs e) {
            for (int i = 0; i < clb_LUBs.Items.Count; i++) clb_LUBs.SetItemChecked(i, true);
            btn_Process.Enabled = true;
        }

        private void Btn_DeselectAll_Click(object sender, EventArgs e) {
            for (int i = 0; i < clb_LUBs.Items.Count; i++) clb_LUBs.SetItemChecked(i, false);
            btn_Process.Enabled = false;
        }
    }
}
