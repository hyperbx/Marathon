using System;
using System.Windows.Forms;
using Marathon.Toolkit.OpenGL;
using WeifenLuo.WinFormsUI.Docking;

namespace Marathon.Toolkit.Forms
{
    public partial class ModelViewer : DockContent
    {
        public ModelViewer() => InitializeComponent();

        private void ModelViewer_Load(object sender, EventArgs e)
        {
            Viewport.Initialise(GLControl_Viewport);

            Application.Idle += delegate { Viewport.Raster(); };
        }
    }
}
