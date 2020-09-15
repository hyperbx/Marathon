namespace Marathon.Toolkit.Forms
{
    partial class Output
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Output));
            this.RichTextBoxLocked_Console = new Marathon.Toolkit.Components.RichTextBoxLocked();
            this.SuspendLayout();
            // 
            // RichTextBoxLocked_Console
            // 
            this.RichTextBoxLocked_Console.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RichTextBoxLocked_Console.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.RichTextBoxLocked_Console.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.RichTextBoxLocked_Console.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.RichTextBoxLocked_Console.Font = new System.Drawing.Font("Consolas", 9F);
            this.RichTextBoxLocked_Console.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
            this.RichTextBoxLocked_Console.Location = new System.Drawing.Point(2, 0);
            this.RichTextBoxLocked_Console.Name = "RichTextBoxLocked_Console";
            this.RichTextBoxLocked_Console.ReadOnly = true;
            this.RichTextBoxLocked_Console.Size = new System.Drawing.Size(798, 450);
            this.RichTextBoxLocked_Console.TabIndex = 1;
            this.RichTextBoxLocked_Console.TabStop = false;
            this.RichTextBoxLocked_Console.Text = "";
            this.RichTextBoxLocked_Console.MouseDown += new System.Windows.Forms.MouseEventHandler(this.RichTextBoxLocked_Console_MouseDown);
            // 
            // Output
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.RichTextBoxLocked_Console);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Output";
            this.Text = "Output";
            this.ResumeLayout(false);

        }

        #endregion

        private Components.RichTextBoxLocked RichTextBoxLocked_Console;
    }
}