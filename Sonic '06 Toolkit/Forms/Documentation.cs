using System.IO;
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
    public partial class Documentation : Form
    {
        public Documentation()
        {
            InitializeComponent();
        }

        void Tree_InfoSelect_AfterSelect(object sender, TreeViewEventArgs e)
        {
            #region Core
            if (tree_InfoSelect.SelectedNode.Text == "ARC") rtb_InfoDisplay.Text = "ARC files are archives compressed using ZLIB.";
            #endregion

            #region Graphics
            else if (tree_InfoSelect.SelectedNode.Text == "DDS") rtb_InfoDisplay.Text = "DDS files are DirectDraw Surface textures.";
            else if (tree_InfoSelect.SelectedNode.Text == "FXO") rtb_InfoDisplay.Text = "FXO files are DirectX shader files.";
            else if (tree_InfoSelect.SelectedNode.Text == "XNCP") rtb_InfoDisplay.Text = "XNCP files contain animations and layouts for the user interface.";
            else if (tree_InfoSelect.SelectedNode.Text == "FTM") rtb_InfoDisplay.Text = "FTM files are font maps; used to store fonts in bitmap images to be used in-game.";
            else if (tree_InfoSelect.SelectedNode.Text == "PEB") rtb_InfoDisplay.Text = "PEB files store particle effects.";
            else if (tree_InfoSelect.SelectedNode.Text == "PLC") rtb_InfoDisplay.Text = "PLC files load the particle effects.";
            else if (tree_InfoSelect.SelectedNode.Text == "XNI") rtb_InfoDisplay.Text = "XNI files contain light particle data.";
            else if (tree_InfoSelect.SelectedNode.Text == "XNV") rtb_InfoDisplay.Text = "XNV files contain special effect generators.";
            else if (tree_InfoSelect.SelectedNode.Text == "MAB") rtb_InfoDisplay.Text = "MAB files are typically used as event files and can be found being referred to by character files.";
            #endregion

            #region Animation
            else if (tree_InfoSelect.SelectedNode.Text == "XNM") rtb_InfoDisplay.Text = "XNM files store animation data. They can be converted with XNO files using XNO Studio.";
            else if (tree_InfoSelect.SelectedNode.Text == "XNO") rtb_InfoDisplay.Text = "XNO files store model data for various objects and characters. They can be converted using XNO Studio.";
            else if (tree_InfoSelect.SelectedNode.Text == "XNG") rtb_InfoDisplay.Text = "XNG files are unique to cage models.";
            else if (tree_InfoSelect.SelectedNode.Text == "XND") rtb_InfoDisplay.Text = "XND files store values for scripted camera sections.";
            #endregion

            #region Audio/Video
            else if (tree_InfoSelect.SelectedNode.Text == "AT3") rtb_InfoDisplay.Text = "AT3 files are PlayStation 3 audio files that use the ATRAC 3 format. These can be edited using AT3 Studio.";
            else if (tree_InfoSelect.SelectedNode.Text == "PAM") rtb_InfoDisplay.Text = "PAM files are PlayStation 3 video files that contain both an AVI for video and an AT3 for audio.";
            else if (tree_InfoSelect.SelectedNode.Text == "CSB") rtb_InfoDisplay.Text = "CSB files are archives for sound formats; namely in the AIF and ADX format. CSBs can be unpacked using CSB Studio.";
            else if (tree_InfoSelect.SelectedNode.Text == "ADX") rtb_InfoDisplay.Text = "ADX files are proprietary CriWare audio files. ADX files can be converted to WAV files using ADX Studio.";
            else if (tree_InfoSelect.SelectedNode.Text == "XMA") rtb_InfoDisplay.Text = "XMA files contain music and voice data";
            else if (tree_InfoSelect.SelectedNode.Text == "WMV") rtb_InfoDisplay.Text = "WMV files are Windows Media Video encoded files; typically used for cutscenes and the title screen for the Xbox 360 version of the game.";
            else if (tree_InfoSelect.SelectedNode.Text == "EPB") rtb_InfoDisplay.Text = "EPB files control cutscene playback in-game.";
            else if (tree_InfoSelect.SelectedNode.Text == "SBK") rtb_InfoDisplay.Text = "SBK files group together XMA and ADX files, but call them by their original ADX formats.";
            else if (tree_InfoSelect.SelectedNode.Text == "XNE") rtb_InfoDisplay.Text = "XNE files are what apply FXO files to certain objects.";
            else if (tree_InfoSelect.SelectedNode.Text == "PTB") rtb_InfoDisplay.Text = "PTB files refer to DDS particle textures and are speculated to specify which particles load on each area, character and/or event.";
            #endregion

            #region Engine
            else if (tree_InfoSelect.SelectedNode.Text == "SET") rtb_InfoDisplay.Text = "SET files are object and effect placement files. They can be converted to the XML format using Skyth's 06set2xml tool.";
            else if (tree_InfoSelect.SelectedNode.Text == "HKX") rtb_InfoDisplay.Text = "HKX files are Havok physics files.\n\nAdditional information: SONIC THE HEDGEHOG (2006) uses an unlicensed version of Havok 3.3.0-b2.";
            else if (tree_InfoSelect.SelectedNode.Text == "XNA") rtb_InfoDisplay.Text = "XNA files contain information for switches, breakable objects and NPC interaction.";
            else if (tree_InfoSelect.SelectedNode.Text == "MBI") rtb_InfoDisplay.Text = "MBI files are Motion Base Information files containing morph data.";
            else if (tree_InfoSelect.SelectedNode.Text == "BIN") rtb_InfoDisplay.Text = "BIN files contain miscellaneous collision data.";
            else if (tree_InfoSelect.SelectedNode.Text == "RAB") rtb_InfoDisplay.Text = "RAB files link to BIN files, meaning they could serve a similar purpose.";
            else if (tree_InfoSelect.SelectedNode.Text == "TEV") rtb_InfoDisplay.Text = "TEV files are used for breakable objects and reference XNM files.";
            else if (tree_InfoSelect.SelectedNode.Text == "ELF") rtb_InfoDisplay.Text = @"ELF files are executable files, typically used for the PlayStation and other software. The only file in this format is for the PlayStation 3 version as 'SONIC THE HEDGEHOG\PS3_GAME\USRDIR\ps3\elf\hksputhreadconstraint.elf' and it fails to start on PlayStation hardware.";
            #endregion

            #region Scripts
            else if (tree_InfoSelect.SelectedNode.Text == "LUB") rtb_InfoDisplay.Text = "LUB files are Lua binaries that have been compiled in Lua 5.1. They can be read by Sonic '06 without being compiled.";
            else if (tree_InfoSelect.SelectedNode.Text == "PATH") rtb_InfoDisplay.Text = "PATH files contain data for scripted path sequences used in-game.";
            else if (tree_InfoSelect.SelectedNode.Text == "KBF") rtb_InfoDisplay.Text = "KBF files are used to store mission data and information.";
            else if (tree_InfoSelect.SelectedNode.Text == "MST") rtb_InfoDisplay.Text = "MST files store message and subtitle data in null terminated BINA string tables. These files can be edited using a hex editor.";
            else if (tree_InfoSelect.SelectedNode.Text == "PFI") rtb_InfoDisplay.Text = "PFI files store miscellaneous text information.";
            else if (tree_InfoSelect.SelectedNode.Text == "PROP") rtb_InfoDisplay.Text = "PROP files contain variable data for stages.";
            else if (tree_InfoSelect.SelectedNode.Text == "PKG") rtb_InfoDisplay.Text = "PKG files are BINA string tables that group together some common file formats (such as; FXO, LUA, DDS, PLC, XNF, XNO, XNI, XNM and XNV files).";
            #endregion


            #region Sonic '06 Toolkit

            #region Troubleshooting

            #region ADX Studio
            else if (tree_InfoSelect.SelectedNode.Text == "WAV files aren't converting back to ADX...") rtb_InfoDisplay.Text = "You need to install both x86 and x64 variations of Microsoft Visual C++ 2010 SP1.\n\nx86: https://www.microsoft.com/de-de/download/details.aspx?id=8328 \nx64: https://www.microsoft.com/en-us/download/details.aspx?id=13523";
            #endregion

            #region AT3 Studio
            else if (tree_InfoSelect.SelectedNode.Text == "MSVCR71.dll is missing...") rtb_InfoDisplay.Text = "You need to install the x86 variant of Microsoft Visual C++ 2010.\n\nx86: https://www.microsoft.com/en-us/download/details.aspx?id=5555";
            else if (tree_InfoSelect.SelectedNode.Text == "AT3 files are encoding empty...") rtb_InfoDisplay.Text = "Encode your WAV file using a 48000Hz sample rate and try again.";
            #endregion

            #region LUB Studio
            else if (tree_InfoSelect.SelectedNode.Text == "Lua binaries are decompiling empty...") rtb_InfoDisplay.Text = "You need to install Java. This should never occur under any circumstances, so please report this immediately.";
            #endregion

            #region XNO Studio
            else if (tree_InfoSelect.SelectedNode.Text == "MSVCR100.dll is missing...") rtb_InfoDisplay.Text = "You need to install both x86 and x64 variations of Microsoft Visual C++ 2010 SP1.\n\nx86: https://www.microsoft.com/de-de/download/details.aspx?id=8328 \nx64: https://www.microsoft.com/en-us/download/details.aspx?id=13523";
            #endregion


            #region Nothing is working!
            else if (tree_InfoSelect.SelectedNode.Text == "Nothing is working!")
            {
                rtb_InfoDisplay.Text = "";
                btn_Reset.Visible = true;
            }
            #endregion

            #endregion

            #endregion

            else
            {
                rtb_InfoDisplay.Text = "";
                btn_Reset.Visible = false;
            }

            if (tree_InfoSelect.SelectedNode.Text != "Nothing is working!") btn_Reset.Visible = false;
        }

        void Button1_Click(object sender, System.EventArgs e)
        {
            DialogResult reset = MessageBox.Show("This will completely reset Sonic '06 Toolkit.\n\n" +
                "" +
                "The following data will be erased:\n" +
                "► Your selected settings.\n" +
                "► Your specified paths.\n" +
                "► Studio settings.\n" +
                "► Archives in the application data.\n" +
                "► Tools in the application data.\n\n" +
                "" +
                "Are you sure you want to continue?", "Reset?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            switch (reset)
            {
                case DialogResult.Yes:
                    var s06toolkitData = new DirectoryInfo(Tools.Global.applicationData + @"\Hyper_Development_Team\Sonic '06 Toolkit\");

                    try
                    {
                        if (Directory.Exists(Tools.Global.applicationData + @"\Hyper_Development_Team\Sonic '06 Toolkit\"))
                        {
                            foreach (FileInfo file in s06toolkitData.GetFiles())
                            {
                                file.Delete();
                            }
                            foreach (DirectoryInfo directory in s06toolkitData.GetDirectories())
                            {
                                directory.Delete(true);
                            }
                        }
                    }
                    catch { }

                    Properties.Settings.Default.Reset();

                    Application.Exit();
                break;
            }
        }
    }
}
