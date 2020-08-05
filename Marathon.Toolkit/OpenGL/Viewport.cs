// Marathon is licensed under the MIT License:
/* 
 * MIT License
 * 
 * Copyright (c) 2020 HyperPolygon64
 * Copyright (c) 2018 Radfordhound
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

using OpenTK;
using OpenTK.Graphics.ES30;

namespace Marathon.Toolkit.OpenGL
{
    public static class Viewport
    {
        public static GLControl _viewport;

        /// <summary>
        /// Initialises the input GLControl.
        /// </summary>
        /// <param name="viewport">GLControl to initialise.</param>
        public static void Initialise(GLControl viewport)
        {
            // Initialise local GLControl.
            _viewport = viewport;

            // Enable depth cap.
            GL.Enable(EnableCap.DepthTest);

            // Load all shaders from XML.
            Shaders.Load();

            // Enable blend cap.
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);

            // Set current viewport and properties.
            viewport.MakeCurrent();
            viewport.VSync = true;

            // Set current viewport events.
            viewport.Resize += delegate { GL.Viewport(0, 0, viewport.Width, viewport.Height); };
        }

        /// <summary>
        /// Rasterises the viewport.
        /// </summary>
        public static void Raster()
        {
            // Clear alpha buffers.
            GL.ClearColor(0, 0, 0, 1);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            // Set default shader.
            int simple = Shaders.Program["Simple"];
            GL.UseProgram(simple);

            // Double buffer the viewport.
            _viewport.SwapBuffers();

            // Draw viewport contents.
            Draw();
        }

        /// <summary>
        /// Draws viewport contents.
        /// </summary>
        public static void Draw()
        {
            // TODO: Draw stuff. lol
        }
    }
}
