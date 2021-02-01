// Marathon is licensed under the MIT License:
/* 
 * MIT License
 * 
 * Copyright (c) 2021 HyperBE32
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
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using Marathon.IO.Helpers;

namespace Marathon.Components.Helpers
{
    public static class GraphicsHelper
    {
        /// <summary>
        /// <para>Draws a rounded rectangle.</para>
        /// <para><see href="https://stackoverflow.com/a/33853557">Learn more...</see></para>
        /// </summary>
        public static void DrawRoundedRectangle(this Graphics graphics, Pen pen, Rectangle bounds, int cornerRadius)
        {
            if (graphics == null)
                throw new ArgumentNullException("graphics");

            if (pen == null)
                throw new ArgumentNullException("pen");

            using (GraphicsPath path = RoundedRectangle(bounds, cornerRadius))
            {
                graphics.DrawPath(pen, path);
            }
        }

        /// <summary>
        /// <para>Draws a rounded rectangle.</para>
        /// <para><see href="https://stackoverflow.com/a/33853557">Learn more...</see></para>
        /// </summary>
        public static void FillRoundedRectangle(this Graphics graphics, Brush brush, Rectangle bounds, int cornerRadius)
        {
            if (graphics == null)
                throw new ArgumentNullException("graphics");

            if (brush == null)
                throw new ArgumentNullException("brush");

            using (GraphicsPath path = RoundedRectangle(bounds, cornerRadius))
            {
                graphics.FillPath(brush, path);
            }
        }

        /// <summary>
        /// <para>Creates a rounded rectangle as a graphics path.</para>
        /// <para><see href="https://stackoverflow.com/a/33853557">Learn more...</see></para>
        /// </summary>
        /// <param name="bounds">Rectangle base.</param>
        /// <param name="radius">Corner radius.</param>
        public static GraphicsPath RoundedRectangle(Rectangle bounds, int radius)
        {
            int diameter = radius * 2;
            Size size = new Size(diameter, diameter);
            Rectangle arc = new Rectangle(bounds.Location, size);
            GraphicsPath path = new GraphicsPath();

            if (radius == 0)
            {
                path.AddRectangle(bounds);
                return path;
            }

            // Top-left arc.  
            path.AddArc(arc, 180, 90);

            // Top-right arc.  
            arc.X = bounds.Right - diameter;
            path.AddArc(arc, 270, 90);

            // Bottom-right arc.  
            arc.Y = bounds.Bottom - diameter;
            path.AddArc(arc, 0, 90);

            // Bottom-left arc.
            arc.X = bounds.Left;
            path.AddArc(arc, 90, 90);

            path.CloseFigure();

            return path;
        }

        /// <summary>
        /// Inflates a rectangle and returns it.
        /// </summary>
        /// 
        /// <remarks>
        /// Because the built-in function doesn't return a rectangle, for some insane reason.
        /// </remarks>
        public static Rectangle InflateRectangle(this Rectangle rect, Size size)
        {
            rect.Inflate(size);

            return rect;
        }

        /// <summary>
        /// Inflates a rectangle and returns it.
        /// </summary>
        /// 
        /// <remarks>
        /// Because the built-in function doesn't return a rectangle, for some insane reason.
        /// </remarks>
        public static Rectangle InflateRectangle(this Rectangle rect, int width, int height)
        {
            rect.Inflate(width, height);

            return rect;
        }

        /// <summary>
        /// Resizes a bitmap maintaining the aspect ratio.
        /// </summary>
        /// <param name="image">Image to resize.</param>
        /// <param name="canvasWidth">Width of the canvas.</param>
        /// <param name="canvasHeight">Height of the canvas.</param>
        /// <param name="maintainX">Should the bitmap resize maintaining the X axis (uses Y if false).</param>
        public static Bitmap ResizeBitmapMaintainingRatio(Bitmap image, int canvasWidth, int canvasHeight, bool maintainX = false)
        {
            // Calculate the ratio.
            double ratio = (maintainX ? (double)canvasWidth : (double)canvasHeight) / (double)image.Width;

            // Calculate new width and height.
            int newWidth = MathsHelper.Clamp((int)(image.Width * ratio), 1, int.MaxValue);
            int newHeight = MathsHelper.Clamp((int)(image.Height * ratio), 1, int.MaxValue);

            return new Bitmap(image, new Size(newWidth, newHeight));
        }

        /// <summary>
        /// <para>Turns a bitmap greyscale.</para>
        /// <para><see href="https://web.archive.org/web/20130111215043/http://www.switchonthecode.com/tutorials/csharp-tutorial-convert-a-color-image-to-grayscale">Learn more...</see></para>
        /// </summary>
        /// <param name="original">Input bitmap to change.</param>
        public static Bitmap MakeGreyscale(Bitmap original)
        {
            // Create a blank bitmap the same size as original.
            Bitmap newBitmap = new Bitmap(original.Width, original.Height);

            // Get a graphics object from the new image.
            Graphics g = Graphics.FromImage(newBitmap);

            // Create the greyscale matrix.
            ColorMatrix colorMatrix = new ColorMatrix
            (
                new float[][]
                {
                    new float[] { 0.3f, 0.3f, 0.3f, 0, 0 },
                    new float[] { 0.59f, 0.59f, 0.59f, 0, 0 },
                    new float[] { 0.11f, 0.11f, 0.11f, 0, 0 },
                    new float[] { 0, 0, 0, 1, 0 },
                    new float[] { 0, 0, 0, 0, 1 }
                }
            );

            // Create some image attributes.
            ImageAttributes attributes = new ImageAttributes();

            // Set the colour matrix attribute.
            attributes.SetColorMatrix(colorMatrix);

            // Draw the original image on the new image using the greyscale colour matrix.
            g.DrawImage
            (
                original,
                new Rectangle(0, 0, original.Width, original.Height),
                0,
                0,
                original.Width,
                original.Height,
                GraphicsUnit.Pixel,
                attributes
            );

            // Dispose the graphics object.
            g.Dispose();

            return newBitmap;
        }

        /// <summary>
        /// Changes the brightness of the source colour.
        /// </summary>
        /// <param name="source">Input colour.</param>
        /// <param name="level">Brightness incrementation.</param>
        public static Color ChangeBrightness(this Color source, int level)
        {
            // Number is positive.
            if (level > 0)
            {
                return Color.FromArgb
                (
                    MathsHelper.Clamp(source.A + level, 0, 255),
                    MathsHelper.Clamp(source.R + level, 0, 255),
                    MathsHelper.Clamp(source.G + level, 0, 255),
                    MathsHelper.Clamp(source.B + level, 0, 255)
                );
            }

            // Number is negative.
            else
            {
                // Gets the positive version of the input.
                int toPositive = Math.Abs(level);

                return Color.FromArgb
                (
                    MathsHelper.Clamp(source.A - toPositive, 0, 255),
                    MathsHelper.Clamp(source.R - toPositive, 0, 255),
                    MathsHelper.Clamp(source.G - toPositive, 0, 255),
                    MathsHelper.Clamp(source.B - toPositive, 0, 255)
                );
            }
        }

        /// <summary>
        /// Converts a ContentAlignment enumerator to the appropriate StringFormat.
        /// </summary>
        public static StringFormat ContentAlignmentToStringFormat(ContentAlignment textAlign)
        {
            StringFormat format = new StringFormat();

            switch (textAlign)
            {
                case ContentAlignment.TopLeft:
                {
                    format.Alignment = StringAlignment.Near;
                    format.LineAlignment = StringAlignment.Far;

                    break;
                }

                case ContentAlignment.TopCenter:
                {
                    format.Alignment = StringAlignment.Center;
                    format.LineAlignment = StringAlignment.Far;

                    break;
                }

                case ContentAlignment.TopRight:
                {
                    format.Alignment = StringAlignment.Far;
                    format.LineAlignment = StringAlignment.Far;

                    break;
                }

                case ContentAlignment.MiddleLeft:
                {
                    format.Alignment = StringAlignment.Near;
                    format.LineAlignment = StringAlignment.Center;

                    break;
                }

                case ContentAlignment.MiddleCenter:
                {
                    format.Alignment = StringAlignment.Center;
                    format.LineAlignment = StringAlignment.Center;

                    break;
                }

                case ContentAlignment.MiddleRight:
                {
                    format.Alignment = StringAlignment.Far;
                    format.LineAlignment = StringAlignment.Center;

                    break;
                }

                case ContentAlignment.BottomLeft:
                {
                    format.Alignment = StringAlignment.Near;
                    format.LineAlignment = StringAlignment.Near;

                    break;
                }

                case ContentAlignment.BottomCenter:
                {
                    format.Alignment = StringAlignment.Center;
                    format.LineAlignment = StringAlignment.Near;

                    break;
                }

                case ContentAlignment.BottomRight:
                {
                    format.Alignment = StringAlignment.Far;
                    format.LineAlignment = StringAlignment.Near;

                    break;
                }
            }

            return format;
        }
    }
}
