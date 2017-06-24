namespace BVW70
{
    partial class Form1
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
            this.bvW70ControlPanel1 = new CueGraphics.Protocol.Sony9Pin.BVW70ControlPanel();
            this.SuspendLayout();
            // 
            // bvW70ControlPanel1
            // 
            this.bvW70ControlPanel1.BackColor = System.Drawing.Color.Silver;
            this.bvW70ControlPanel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.bvW70ControlPanel1.Location = new System.Drawing.Point(0, 0);
            this.bvW70ControlPanel1.Name = "bvW70ControlPanel1";
            this.bvW70ControlPanel1.Size = new System.Drawing.Size(968, 484);
            this.bvW70ControlPanel1.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(968, 481);
            this.Controls.Add(this.bvW70ControlPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private CueGraphics.Protocol.Sony9Pin.BVW70ControlPanel bvW70ControlPanel1;
    }
}