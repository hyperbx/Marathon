using System;
using System.IO;
using Toolkit.Text;
using System.Linq;
using Toolkit.EnvironmentX;
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

namespace Toolkit.Tools
{
    public partial class XNOTool : Form
    {
        private Main mainForm = null;
        private string location = Paths.currentPath;

        public XNOTool(Form callingForm) {
            mainForm = callingForm as Main;
            InitializeComponent();
            tm_ModeCheck.Start();
        }

        private void ModelAnimationExporter_Load(object sender, EventArgs e) {
            clb_XNOs.Items.Clear();
            clb_XNOs_XNM.Items.Clear();
            clb_XNMs.Items.Clear();

            if (Directory.GetFiles(location, "*.xno").Length > 0) {
                modes_Model.Checked = true;
                modes_ModelAndAnimation.Checked = false;
                modes_BackfaceCulling.Checked = false;
            } else {
                MessageBox.Show(SystemMessages.msg_NoConvertableFiles, SystemMessages.tl_NoFilesAvailable(string.Empty), MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();
            }
        }

        private void Modes_Model_CheckedChanged(object sender, EventArgs e) {
            if (modes_Model.Checked) {
                Text = lbl_Title.Text = "Model Exporter";
                modes_ModelAndAnimation.Checked = false;
                modes_BackfaceCulling.Checked = false;
                option_Culling.Visible = false;
                clb_XNOs.Visible = true;
                split_XNMStudio.Visible = false;

                btn_Process.Enabled = false;
                clb_XNOs.Items.Clear();
                clb_XNOs_XNM.Items.Clear();
                clb_XNMs.Items.Clear();

                foreach (string XNO in Directory.GetFiles(location, "*.xno", SearchOption.TopDirectoryOnly))
                    if (File.Exists(XNO) && Verification.VerifyMagicNumberCommon(XNO))
                        clb_XNOs.Items.Add(Path.GetFileName(XNO));

                if (clb_XNOs.Items.Count == 0) {
                    MessageBox.Show(SystemMessages.msg_NoXNOsInDir, SystemMessages.tl_NoFilesAvailable("XNO"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                }
            }
        }

        private void Modes_ModelAndAnimation_CheckedChanged(object sender, EventArgs e) {
            if (modes_ModelAndAnimation.Checked) {
                Text = lbl_Title.Text = "Animation Exporter";
                modes_Model.Checked = false;
                modes_BackfaceCulling.Checked = false;
                option_Culling.Visible = false;
                clb_XNOs.Visible = false;
                split_XNMStudio.Visible = true;
                
                btn_Process.Enabled = false;
                clb_XNOs.Items.Clear();
                clb_XNOs_XNM.Items.Clear();
                clb_XNMs.Items.Clear();

                foreach (string XNO in Directory.GetFiles(location, "*.xno", SearchOption.TopDirectoryOnly))
                    if (File.Exists(XNO) && Verification.VerifyMagicNumberCommon(XNO))
                        clb_XNOs_XNM.Items.Add(Path.GetFileName(XNO));

                foreach (string XNM in Directory.GetFiles(location, "*.xnm", SearchOption.TopDirectoryOnly))
                    if (File.Exists(XNM) && Verification.VerifyMagicNumberCommon(XNM))
                        clb_XNMs.Items.Add(Path.GetFileName(XNM));

                if (clb_XNMs.Items.Count == 0) {
                    MessageBox.Show(SystemMessages.msg_NoXNMsInDir, SystemMessages.tl_NoFilesAvailable("XNM"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    modes_Model.Checked = true;
                }
            }
        }

        private void Modes_BackfaceCulling_CheckedChanged(object sender, EventArgs e) {
            if (modes_BackfaceCulling.Checked) {
                Text = lbl_Title.Text = "Backface Tool";
                modes_Model.Checked = false;
                modes_ModelAndAnimation.Checked = false;
                option_Culling.Visible = true;
                clb_XNOs.Visible = true;
                split_XNMStudio.Visible = false;

                btn_Process.Enabled = false;
                clb_XNOs.Items.Clear();
                clb_XNOs_XNM.Items.Clear();
                clb_XNMs.Items.Clear();

                foreach (string XNO in Directory.GetFiles(location, "*.xno", SearchOption.TopDirectoryOnly))
                    if (File.Exists(XNO) && Verification.VerifyMagicNumberCommon(XNO))
                        clb_XNOs.Items.Add(Path.GetFileName(XNO));

                if (clb_XNOs.Items.Count == 0) {
                    MessageBox.Show(SystemMessages.msg_NoXNOsInDir, SystemMessages.tl_NoFilesAvailable("XNO"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                }
            }
        }

        private void Tm_ModeCheck_Tick(object sender, EventArgs e) {
            if (!modes_Model.Checked && !modes_ModelAndAnimation.Checked && !modes_BackfaceCulling.Checked)
                modes_Model.Checked = true;
        }

        private async void Btn_Process_Click(object sender, EventArgs e) {
            if (modes_Model.Checked) {
                foreach (string XNO in clb_XNOs.CheckedItems) {
                    if (File.Exists(Path.Combine(location, XNO)) && Verification.VerifyMagicNumberCommon(Path.Combine(location, XNO))) {
                        mainForm.Status = StatusMessages.cmn_Converting(XNO, "DAE", false);
                        var convert = await ProcessAsyncHelper.ExecuteShellCommand(Paths.XNODecoder,
                                           $"\"{Path.Combine(location, XNO)}\"",
                                           location,
                                           100000);
                        if (convert.Completed)
                            if (convert.ExitCode != 0)
                                MessageBox.Show(SystemMessages.ex_XNOConvertError, SystemMessages.tl_FatalError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            } else if (modes_ModelAndAnimation.Checked) {
                string getXNO = string.Empty;
                string getXNM = string.Empty;

                foreach (string XNO in clb_XNOs_XNM.CheckedItems)
                    if (File.Exists(Path.Combine(location, XNO)) && Verification.VerifyMagicNumberCommon(Path.Combine(location, XNO)))
                        getXNO = Path.Combine(location, XNO);

                foreach (string XNM in clb_XNMs.CheckedItems)
                    if (File.Exists(Path.Combine(location, XNM)) && Verification.VerifyMagicNumberCommon(Path.Combine(location, XNM)))
                        getXNM = Path.Combine(location, XNM);

                if (getXNO != string.Empty && getXNM != string.Empty) {
                    mainForm.Status = StatusMessages.cmn_Converting(getXNM, "DAE", false);
                    var convert = await ProcessAsyncHelper.ExecuteShellCommand(Paths.XNODecoder,
                                       $"\"{Path.Combine(location, getXNO)}\" \"{Path.Combine(location, getXNM)}\"",
                                       location,
                                       100000);
                    if (convert.Completed)
                        if (convert.ExitCode != 0)
                            MessageBox.Show(SystemMessages.ex_XNMConvertError, SystemMessages.tl_FatalError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                } else
                    MessageBox.Show(SystemMessages.ex_InvalidFiles, SystemMessages.tl_FatalError, MessageBoxButtons.OK, MessageBoxIcon.Error);
            } else if (modes_BackfaceCulling.Checked) {
                foreach (string XNO in clb_XNOs.CheckedItems) {
                    if (File.Exists(Path.Combine(location, XNO)) && Verification.VerifyMagicNumberCommon(Path.Combine(location, XNO))) {
                        byte[] xnoBytes = File.ReadAllBytes(Path.Combine(location, XNO)).ToArray();
                        string hexString = BitConverter.ToString(xnoBytes).Replace("-", "");

                        if (option_Culling.Checked) {
                            if (hexString.Contains(Properties.Resources.xno_Decull)) {
                                mainForm.Status = StatusMessages.xno_Culling(XNO, false);
                                hexString = hexString.Replace(Properties.Resources.xno_Decull, Properties.Resources.xno_Cull);
                                File.WriteAllBytes(Path.Combine(location, XNO), ByteArray.StringToByteArrayExtended(hexString));
                            } else
                                mainForm.Status = StatusMessages.xno_NothingToCull(XNO, false);
                        } else {
                            if (hexString.Contains(Properties.Resources.xno_Cull)) {
                                mainForm.Status = StatusMessages.xno_Deculling(XNO, false);
                                hexString = hexString.Replace(Properties.Resources.xno_Cull, Properties.Resources.xno_Decull);
                                File.WriteAllBytes(Path.Combine(location, XNO), ByteArray.StringToByteArrayExtended(hexString));
                            } else
                                mainForm.Status = StatusMessages.xno_NothingToDecull(XNO, false);
                        }
                    }
                }
            }
        }

        private void Clb_XNOs_SelectedIndexChanged(object sender, EventArgs e) {
            clb_XNOs.ClearSelected();
            btn_Process.Enabled = clb_XNOs.CheckedItems.Count > 0;
        }

        private void Clb_XNOs_XNM_SelectedIndexChanged(object sender, EventArgs e) {
            clb_XNOs_XNM.ClearSelected();
            btn_Process.Enabled = clb_XNOs_XNM.CheckedItems.Count > 0 && clb_XNMs.CheckedItems.Count > 0;
        }

        private void Clb_XNMs_SelectedIndexChanged(object sender, EventArgs e) { 
            clb_XNMs.ClearSelected();
            btn_Process.Enabled = clb_XNOs_XNM.CheckedItems.Count > 0 && clb_XNMs.CheckedItems.Count > 0;
        }

        private void Clb_XNOs_XNM_ItemCheck(object sender, ItemCheckEventArgs e) {
            if (clb_XNOs_XNM.CheckedItems.Count == 1 && e.NewValue == CheckState.Checked)
                e.NewValue = CheckState.Unchecked;
        }

        private void Clb_XNMs_ItemCheck(object sender, ItemCheckEventArgs e) {
            if (clb_XNMs.CheckedItems.Count == 1 && e.NewValue == CheckState.Checked)
                e.NewValue = CheckState.Unchecked;
        }
    }
}
