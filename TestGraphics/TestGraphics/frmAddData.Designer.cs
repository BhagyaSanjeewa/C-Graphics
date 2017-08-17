namespace TestGraphics
{
    partial class frmAddData
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtAllocKG = new System.Windows.Forms.TextBox();
            this.txtOutPut = new System.Windows.Forms.TextBox();
            this.txtNoOfTapes = new System.Windows.Forms.TextBox();
            this.lblNoOfTapes = new System.Windows.Forms.Label();
            this.lblMCOut = new System.Windows.Forms.Label();
            this.lblEfficiency = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.lblAllocQty = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.lblAllocQty);
            this.panel1.Controls.Add(this.txtAllocKG);
            this.panel1.Controls.Add(this.txtOutPut);
            this.panel1.Controls.Add(this.txtNoOfTapes);
            this.panel1.Controls.Add(this.lblNoOfTapes);
            this.panel1.Controls.Add(this.lblMCOut);
            this.panel1.Controls.Add(this.lblEfficiency);
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.btnOK);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(310, 131);
            this.panel1.TabIndex = 11;
            // 
            // txtAllocKG
            // 
            this.txtAllocKG.Location = new System.Drawing.Point(94, 64);
            this.txtAllocKG.Name = "txtAllocKG";
            this.txtAllocKG.Size = new System.Drawing.Size(100, 20);
            this.txtAllocKG.TabIndex = 12;
            // 
            // txtOutPut
            // 
            this.txtOutPut.Location = new System.Drawing.Point(94, 39);
            this.txtOutPut.Name = "txtOutPut";
            this.txtOutPut.Size = new System.Drawing.Size(100, 20);
            this.txtOutPut.TabIndex = 11;
            // 
            // txtNoOfTapes
            // 
            this.txtNoOfTapes.Location = new System.Drawing.Point(94, 13);
            this.txtNoOfTapes.Name = "txtNoOfTapes";
            this.txtNoOfTapes.Size = new System.Drawing.Size(100, 20);
            this.txtNoOfTapes.TabIndex = 10;
            // 
            // lblNoOfTapes
            // 
            this.lblNoOfTapes.AutoSize = true;
            this.lblNoOfTapes.Location = new System.Drawing.Point(14, 13);
            this.lblNoOfTapes.Name = "lblNoOfTapes";
            this.lblNoOfTapes.Size = new System.Drawing.Size(62, 13);
            this.lblNoOfTapes.TabIndex = 0;
            this.lblNoOfTapes.Text = "No of tapes";
            // 
            // lblMCOut
            // 
            this.lblMCOut.AutoSize = true;
            this.lblMCOut.Location = new System.Drawing.Point(14, 39);
            this.lblMCOut.Name = "lblMCOut";
            this.lblMCOut.Size = new System.Drawing.Size(58, 13);
            this.lblMCOut.TabIndex = 1;
            this.lblMCOut.Text = "MC Output";
            // 
            // lblEfficiency
            // 
            this.lblEfficiency.AutoSize = true;
            this.lblEfficiency.Location = new System.Drawing.Point(14, 64);
            this.lblEfficiency.Name = "lblEfficiency";
            this.lblEfficiency.Size = new System.Drawing.Size(45, 13);
            this.lblEfficiency.TabIndex = 2;
            this.lblEfficiency.Text = "AllocKG";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(169, 87);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(57, 23);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(106, 87);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(57, 23);
            this.btnOK.TabIndex = 6;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // lblAllocQty
            // 
            this.lblAllocQty.AutoSize = true;
            this.lblAllocQty.Location = new System.Drawing.Point(231, 67);
            this.lblAllocQty.Name = "lblAllocQty";
            this.lblAllocQty.Size = new System.Drawing.Size(45, 13);
            this.lblAllocQty.TabIndex = 13;
            this.lblAllocQty.Text = "AllocKG";
            // 
            // frmAddData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(330, 151);
            this.Controls.Add(this.panel1);
            this.Name = "frmAddData";
            this.Text = "frmAddData";
            this.Load += new System.EventHandler(this.frmAddData_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblNoOfTapes;
        private System.Windows.Forms.Label lblMCOut;
        private System.Windows.Forms.Label lblEfficiency;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.TextBox txtAllocKG;
        private System.Windows.Forms.TextBox txtOutPut;
        private System.Windows.Forms.TextBox txtNoOfTapes;
        private System.Windows.Forms.Label lblAllocQty;
    }
}