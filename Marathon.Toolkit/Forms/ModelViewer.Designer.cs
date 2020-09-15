using Marathon.Toolkit.Helpers;
using System.Windows.Forms;

namespace Marathon.Toolkit.Forms
{
    partial class ModelViewer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ModelViewer));
            this.GLControl_Viewport = new OpenTK.GLControl();
            this.SuspendLayout();
            // 
            // GLControl_Viewport
            // 
            this.GLControl_Viewport.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GLControl_Viewport.BackColor = System.Drawing.Color.Black;
            this.GLControl_Viewport.Location = new System.Drawing.Point(12, 12);
            this.GLControl_Viewport.Name = "GLControl_Viewport";
            this.GLControl_Viewport.Size = new System.Drawing.Size(909, 495);
            this.GLControl_Viewport.TabIndex = 0;
            this.GLControl_Viewport.VSync = false;
            // 
            // ModelViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.ClientSize = new System.Drawing.Size(933, 519);
            this.Controls.Add(this.GLControl_Viewport);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.SystemColors.Control;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ModelViewer";
            this.Text = "Model Viewer";
            this.Load += new System.EventHandler(this.ModelViewer_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private OpenTK.GLControl GLControl_Viewport;
    }
}
