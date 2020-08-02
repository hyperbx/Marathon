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
