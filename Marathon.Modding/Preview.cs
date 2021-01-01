// Marathon is licensed under the MIT License:
/* 
 * MIT License
 * 
 * Copyright (c) 2020 HyperBE32
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 */

using System.IO;
using System.Drawing;
using System.Windows.Forms;
using Marathon.Helpers;
using Marathon.Components.Helpers;

namespace Marathon.Modding
{
    public partial class Preview : UserControl
    {
        private Mod _SelectedMod;

        public Mod SelectedMod
        {
            get => _SelectedMod;

            set
            {
                _SelectedMod = value;

                UpdatePreview();
            }
        }

        public Preview()
        {
            InitializeComponent();

            UpdatePreview();
        }

        /// <summary>
        /// Updates the preview for the selected mod.
        /// </summary>
        private void UpdatePreview()
        {
            // TODO: set preview info to notify of incorrect output.
            if (SelectedMod == null)
                return;

            // Create the thumbnail preview.
            CreateThumbnail();

            // Create the information preview.
            CreateInformation();
        }

        /// <summary>
        /// Sets up the thumbnail for the selected mod.
        /// </summary>
        private void CreateThumbnail()
        {
            // Clear the images.
            PictureBox_Thumbnail.BackgroundImage = Panel_Blur.BackgroundImage = null;

            // Canvas width and height.
            int width = PictureBox_Thumbnail.Width,
                height = PictureBox_Thumbnail.Height;

            // Flag for checking if a thumbnail was found.
            bool thumbnailFound = false;

            // Get the thumbnail from the mod directory root.
            foreach (string image in Directory.GetFiles(Path.GetDirectoryName(SelectedMod.Location), "thumbnail.*", SearchOption.TopDirectoryOnly))
            {
                /* Create thumbnail background image to the size of the picture box (multiplied by two as to prevent any artefacts).
                   It may seem counter-intuitive to load it this way, but it reduces memory usage by 85%. */
                PictureBox_Thumbnail.BackgroundImage = GraphicsHelper.ResizeBitmapMaintainingRatio((Bitmap)Image.FromFile(image), width * 2, height * 2);

                // Set the thumbnail flag.
                thumbnailFound = true;

                // Break out of this loop so we don't process any lingering thumbnails.
                break;
            }

            // Thumbnail isn't null, so perform design... stuff.
            if (thumbnailFound)
            {
                try
                {
                    // Use a blurred version of the thumbnail.
                    Panel_Blur.BackgroundImage = new GaussianBlur((Bitmap)PictureBox_Thumbnail.BackgroundImage).Process(50);

                    // Set layout to stretch to cover the entire control.
                    Panel_Blur.BackgroundImageLayout = ImageLayout.Stretch;
                }
                catch
                {
                    // Use a solid colour.
                    Panel_Blur.BackColor = Color.Black;
                }
            }

            // Otherwise, use default logo.
            else
            {
                // Set thumbnail background image to default.
                PictureBox_Thumbnail.BackgroundImage = Cache.LoadBitmapResource(nameof(Properties.Resources.Manager_Full_Colour));
            }
        }

        /// <summary>
        /// Sets up the information for the selected mod.
        /// </summary>
        private void CreateInformation()
        {
            // Set title.
            SetValidShortInfo(Label_Title, SelectedMod.Title);

            // Set author.
            SetValidShortInfo(Label_Author, $"by {SelectedMod.Author}");

            // Parse line breaks and set up the description.
            MarathonRichTextBox_Description.Lines = string.IsNullOrEmpty(SelectedMod.Description) ?
                                                    new[] { "No information given." } :
                                                    StringHelper.ParseLineBreaks(SelectedMod.Description, true);

            // Set up the basic information.
            MarathonRichTextBox_Information.Text = StringHelper.PrependPrefix(SelectedMod.Version, "Version: ", 1) +
                                                   StringHelper.PrependPrefix(SelectedMod.Date, "Date: ", 1) +
                                                   StringHelper.PrependPrefix(SelectedMod.Author, "Author: ", 1) +
                                                   StringHelper.PrependPrefix(SelectedMod.System.ToString(), "Platform: ");

            // Set up the technical information.
            MarathonRichTextBox_Technical.Text = StringHelper.PrependPrefix(SelectedMod.Merge.ToString(), "Merge: ", 1) +
                                                 StringHelper.PrependPrefix(SelectedMod.SaveRedirect.ToString(), "Save File Redirection: ", 1) +
                                                 StringHelper.PrependPrefix(SelectedMod.CustomFilesystem.ToString(), "Custom Filesystem: ", 2) +
                                                 StringHelper.PrependPrefix(string.Join(", ", SelectedMod.Patches), "Patches: ", 2) +
                                                 StringHelper.PrependPrefix(string.Join(", ", SelectedMod.ReadOnly), "Read-only: ", 2) +
                                                 StringHelper.PrependPrefix(string.Join(", ", SelectedMod.Custom), "Custom: ");

            void SetValidShortInfo(Label label, string text)
            {
                if (!string.IsNullOrEmpty(text))
                {
                    // Set the tooltip for this control to display the full text.
                    ToolTipDark_Title.SetToolTip(label, text);

                    // Set label text to the original input.
                    label.Text = text;
                }
                else
                {
                    // Set unknown label text.
                    label.Text = "Unspecified";
                }
            }
        }
    }
}
