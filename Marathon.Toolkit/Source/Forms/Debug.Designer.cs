namespace Marathon.Toolkit.Forms
{
    partial class Debug
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Debug));
            this.ButtonFlat_Test = new Marathon.Toolkit.Components.ButtonFlat();
            this.SuspendLayout();
            // 
            // ButtonFlat_Test
            // 
            this.ButtonFlat_Test.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonFlat_Test.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ButtonFlat_Test.FlatAppearance.BorderSize = 0;
            this.ButtonFlat_Test.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonFlat_Test.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ButtonFlat_Test.Location = new System.Drawing.Point(12, 12);
            this.ButtonFlat_Test.Name = "ButtonFlat_Test";
            this.ButtonFlat_Test.Size = new System.Drawing.Size(909, 31);
            this.ButtonFlat_Test.TabIndex = 0;
            this.ButtonFlat_Test.Text = "the debug button with no purpose";
            this.ButtonFlat_Test.UseVisualStyleBackColor = false;
            this.ButtonFlat_Test.Click += new System.EventHandler(this.ButtonFlat_Test_Click);
            // 
            // Debug
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.ClientSize = new System.Drawing.Size(933, 519);
            this.Controls.Add(this.ButtonFlat_Test);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.SystemColors.Control;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Debug";
            this.Text = "Debug";
            this.ResumeLayout(false);

        }

        #endregion

        private Components.ButtonFlat ButtonFlat_Test;
    }
}