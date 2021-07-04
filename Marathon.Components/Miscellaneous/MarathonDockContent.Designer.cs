namespace Marathon.Components
{
    partial class MarathonDockContent
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MarathonDockContent));
            this.KryptonRibbon_MarathonDockContent = new ComponentFactory.Krypton.Ribbon.KryptonRibbon();
            ((System.ComponentModel.ISupportInitialize)(this.KryptonRibbon_MarathonDockContent)).BeginInit();
            this.SuspendLayout();
            // 
            // KryptonRibbon_MarathonDockContent
            // 
            this.KryptonRibbon_MarathonDockContent.InDesignHelperMode = true;
            this.KryptonRibbon_MarathonDockContent.Name = "KryptonRibbon_MarathonDockContent";
            this.KryptonRibbon_MarathonDockContent.QATLocation = ComponentFactory.Krypton.Ribbon.QATLocation.Hidden;
            this.KryptonRibbon_MarathonDockContent.QATUserChange = false;
            this.KryptonRibbon_MarathonDockContent.RibbonAppButton.AppButtonShowRecentDocs = false;
            this.KryptonRibbon_MarathonDockContent.SelectedTab = null;
            this.KryptonRibbon_MarathonDockContent.Size = new System.Drawing.Size(240, 114);
            this.KryptonRibbon_MarathonDockContent.TabIndex = 0;
            // 
            // MarathonDockContent
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.ClientSize = new System.Drawing.Size(240, 217);
            this.Controls.Add(this.KryptonRibbon_MarathonDockContent);
            this.DockAreas = ((WeifenLuo.WinFormsUI.Docking.DockAreas)(((((WeifenLuo.WinFormsUI.Docking.DockAreas.DockLeft | WeifenLuo.WinFormsUI.Docking.DockAreas.DockRight) 
            | WeifenLuo.WinFormsUI.Docking.DockAreas.DockTop) 
            | WeifenLuo.WinFormsUI.Docking.DockAreas.DockBottom) 
            | WeifenLuo.WinFormsUI.Docking.DockAreas.Document)));
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.SystemColors.Control;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MarathonDockContent";
            this.Text = "Placeholder";
            ((System.ComponentModel.ISupportInitialize)(this.KryptonRibbon_MarathonDockContent)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public ComponentFactory.Krypton.Ribbon.KryptonRibbon KryptonRibbon_MarathonDockContent;
    }
}
