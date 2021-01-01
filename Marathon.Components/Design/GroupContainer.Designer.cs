namespace Marathon.Components
{
    partial class GroupContainer
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
            this.Panel_Container = new System.Windows.Forms.Panel();
            this.Label_Header = new Marathon.Components.LabelDark();
            this.SuspendLayout();
            // 
            // Panel_Container
            // 
            this.Panel_Container.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Panel_Container.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.Panel_Container.Location = new System.Drawing.Point(1, 25);
            this.Panel_Container.Name = "Panel_Container";
            this.Panel_Container.Size = new System.Drawing.Size(148, 124);
            this.Panel_Container.TabIndex = 0;
            // 
            // Label_Header
            // 
            this.Label_Header.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label_Header.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Label_Header.ForeColor = System.Drawing.SystemColors.Control;
            this.Label_Header.Location = new System.Drawing.Point(5, 5);
            this.Label_Header.Name = "Label_Header";
            this.Label_Header.Size = new System.Drawing.Size(144, 15);
            this.Label_Header.TabIndex = 0;
            this.Label_Header.Text = "Placeholder";
            this.Label_Header.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // GroupContainer
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Controls.Add(this.Label_Header);
            this.Controls.Add(this.Panel_Container);
            this.DoubleBuffered = true;
            this.Name = "GroupContainer";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel Panel_Container;
        private LabelDark Label_Header;
    }
}
