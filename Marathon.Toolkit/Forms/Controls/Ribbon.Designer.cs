namespace Marathon.Toolkit.Controls
{
    partial class Ribbon
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
            this.ButtonFlat_Visibility = new Marathon.Toolkit.Components.ButtonFlat();
            this.TabControlFlat_Ribbon = new Marathon.Toolkit.Components.TabControlFlat();
            this.SuspendLayout();
            // 
            // ButtonFlat_Visibility
            // 
            this.ButtonFlat_Visibility.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonFlat_Visibility.BackColor = System.Drawing.Color.Transparent;
            this.ButtonFlat_Visibility.BackgroundImage = global::Marathon.Toolkit.Properties.Resources.Ribbon_ExpandUp;
            this.ButtonFlat_Visibility.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ButtonFlat_Visibility.FlatAppearance.BorderSize = 0;
            this.ButtonFlat_Visibility.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.ButtonFlat_Visibility.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.ButtonFlat_Visibility.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonFlat_Visibility.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ButtonFlat_Visibility.Location = new System.Drawing.Point(230, 0);
            this.ButtonFlat_Visibility.Name = "ButtonFlat_Visibility";
            this.ButtonFlat_Visibility.Size = new System.Drawing.Size(21, 21);
            this.ButtonFlat_Visibility.TabIndex = 1;
            this.ButtonFlat_Visibility.UseVisualStyleBackColor = false;
            this.ButtonFlat_Visibility.Click += new System.EventHandler(this.ButtonFlat_Visibility_Click);
            // 
            // TabControlFlat_Ribbon
            // 
            this.TabControlFlat_Ribbon.ActiveColour = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.TabControlFlat_Ribbon.AllowDragging = false;
            this.TabControlFlat_Ribbon.AllowDrop = true;
            this.TabControlFlat_Ribbon.BorderColour = System.Drawing.Color.Transparent;
            this.TabControlFlat_Ribbon.CloseButtonColour = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(230)))), ((int)(((byte)(245)))));
            this.TabControlFlat_Ribbon.CloseOnMiddleClick = false;
            this.TabControlFlat_Ribbon.ClosingMessage = null;
            this.TabControlFlat_Ribbon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TabControlFlat_Ribbon.HeaderColour = System.Drawing.Color.Black;
            this.TabControlFlat_Ribbon.HorizontalLineColour = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.TabControlFlat_Ribbon.ItemSize = new System.Drawing.Size(73, 23);
            this.TabControlFlat_Ribbon.Location = new System.Drawing.Point(0, 0);
            this.TabControlFlat_Ribbon.Name = "TabControlFlat_Ribbon";
            this.TabControlFlat_Ribbon.Padding = new System.Drawing.Point(0, 0);
            this.TabControlFlat_Ribbon.SelectedIndex = 0;
            this.TabControlFlat_Ribbon.SelectedTextColour = System.Drawing.SystemColors.Control;
            this.TabControlFlat_Ribbon.ShowCloseButton = false;
            this.TabControlFlat_Ribbon.ShowClosingMessage = false;
            this.TabControlFlat_Ribbon.Size = new System.Drawing.Size(251, 122);
            this.TabControlFlat_Ribbon.TabIndex = 0;
            this.TabControlFlat_Ribbon.TabPageBackColour = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.TabControlFlat_Ribbon.TextColour = System.Drawing.Color.White;
            this.TabControlFlat_Ribbon.ControlAdded += new System.Windows.Forms.ControlEventHandler(this.TabControlFlat_Ribbon_ControlAdded);
            this.TabControlFlat_Ribbon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.TabControlFlat_Ribbon_MouseClick);
            // 
            // Ribbon
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Black;
            this.Controls.Add(this.ButtonFlat_Visibility);
            this.Controls.Add(this.TabControlFlat_Ribbon);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "Ribbon";
            this.Size = new System.Drawing.Size(251, 122);
            this.ResumeLayout(false);

        }

        #endregion

        private Components.TabControlFlat TabControlFlat_Ribbon;
        private Components.ButtonFlat ButtonFlat_Visibility;
    }
}
