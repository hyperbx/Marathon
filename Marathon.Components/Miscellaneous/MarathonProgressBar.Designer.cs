namespace Marathon.Components
{
    partial class MarathonProgressBar
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
            this.components = new System.ComponentModel.Container();
            this.Timer_Animation = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // Timer_Animation
            // 
            this.Timer_Animation.Enabled = true;
            this.Timer_Animation.Interval = 2000;
            this.Timer_Animation.Tick += new System.EventHandler(this.Timer_Animation_Tick);
            // 
            // MarathonProgressBar
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.SystemColors.Control;
            this.Name = "MarathonProgressBar";
            this.Size = new System.Drawing.Size(150, 50);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer Timer_Animation;
    }
}
