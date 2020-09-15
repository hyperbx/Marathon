// Marathon is licensed under the MIT License:
/* 
 * MIT License
 * 
 * Copyright (c) 2020 HyperPolygon64
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

using System;
using System.Drawing;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace Marathon.Toolkit.Forms
{
    public partial class FileConverter : DockContent
    {
        public FileConverter()
        {
            InitializeComponent();
            DrawFileDropContainer();
        }

        private void DrawFileDropContainer()
        {
            SuspendLayout(); // Suspend for redrawing...

            // Prepares the image for drawing.
            Bitmap imageSource = Resources.LoadBitmapResource(nameof(Properties.Resources.FileDrop));
            int width = imageSource.Width / 3, height = imageSource.Height / 3;

            // Draw the image on the paint event.
            Paint += (paintSender, paintEventArgs) =>
            {
                // Draws the logo at the bottom right of the MDI container if there are no children.
                paintEventArgs.Graphics.DrawImage(imageSource, new Rectangle((Width / 2) - (width / 2) - 8, (Height / 2) - (height / 2) - 6, width, height));
            };

            // Refreshes the MDI container on resize to redraw the logo.
            Resize += (resizeSender, resizeEventArgs) => Refresh();

            ResumeLayout(true); // Resume after redraw is complete...
        }

        /// <summary>
        /// Changes the cursor if the data is present.
        /// </summary>
        private void FileConverter_DragEnter(object sender, DragEventArgs e)
            => _ = e.Data.GetDataPresent(DataFormats.FileDrop) ? e.Effect = DragDropEffects.Copy : e.Effect = DragDropEffects.None;

        /// <summary>
        /// Gets the file dropped onto the window.
        /// </summary>
        private void FileConverter_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

            if (files.Length == 1) ToolAutoSelect(files[0]);
        }

        /// <summary>
        /// Auto-selects the required tool for the dropped file.
        /// </summary>
        private void ToolAutoSelect(string data)
        {
            throw new NotImplementedException(); // TODO
        }
    }
}
