namespace TestGraphics
{
    partial class ucAddNewPlan
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
            this.lblDescription = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Location = new System.Drawing.Point(560, 157);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(60, 13);
            this.lblDescription.TabIndex = 1;
            this.lblDescription.Text = "Description";
            // 
            // ucAddNewPlan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblDescription);
            this.DoubleBuffered = true;
            this.Name = "ucAddNewPlan";
            this.Size = new System.Drawing.Size(867, 444);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.ucAddNewPlan_Paint);
            this.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.ucAddNewPlan_MouseDoubleClick);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ucAddNewPlan_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ucAddNewPlan_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ucAddNewPlan_MouseUp);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblDescription;
    }
}
