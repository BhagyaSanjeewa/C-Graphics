namespace TestGraphics
{
    partial class frmTest
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
            this.ucGraphicMain1 = new TestGraphics.ucGraphicMain();
            this.SuspendLayout();
            // 
            // ucGraphicMain1
            // 
            this.ucGraphicMain1.Location = new System.Drawing.Point(23, 58);
            this.ucGraphicMain1.lstGlobalMachines = null;
            this.ucGraphicMain1.LstMachineBar = null;
            this.ucGraphicMain1.Name = "ucGraphicMain1";
            this.ucGraphicMain1.Size = new System.Drawing.Size(971, 449);
            this.ucGraphicMain1.TabIndex = 0;
            this.ucGraphicMain1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.ucGraphicMain1_Scroll);
            // 
            // frmTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1019, 532);
            this.Controls.Add(this.ucGraphicMain1);
            this.Name = "frmTest";
            this.Text = "frmTest";
            this.Load += new System.EventHandler(this.frmTest_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private ucGraphicMain ucGraphicMain1;
    }
}