namespace Marathon.Toolkit.Controls
{
    partial class RibbonPanel
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
            this.FlowLayoutPanel_Contents = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // FlowLayoutPanel_Contents
            // 
            this.FlowLayoutPanel_Contents.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FlowLayoutPanel_Contents.Location = new System.Drawing.Point(1, 0);
            this.FlowLayoutPanel_Contents.Name = "FlowLayoutPanel_Contents";
            this.FlowLayoutPanel_Contents.Size = new System.Drawing.Size(78, 74);
            this.FlowLayoutPanel_Contents.TabIndex = 2;
            this.FlowLayoutPanel_Contents.WrapContents = false;
            // 
            // RibbonPanel
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.Controls.Add(this.FlowLayoutPanel_Contents);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "RibbonPanel";
            this.Size = new System.Drawing.Size(80, 91);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel FlowLayoutPanel_Contents;
    }
}
