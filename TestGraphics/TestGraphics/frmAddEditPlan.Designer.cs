namespace TestGraphics
{
    partial class frmAddEditPlan
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
            this.label1 = new System.Windows.Forms.Label();
            this.btnAddEditPlan = new System.Windows.Forms.Button();
            this.cmbMachine = new System.Windows.Forms.ComboBox();
            this.txtFrom = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ucAddNewPlan1 = new TestGraphics.ucAddNewPlan();
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 13);
            this.label1.TabIndex = 0;
            // 
            // btnAddEditPlan
            // 
            this.btnAddEditPlan.Location = new System.Drawing.Point(1382, 12);
            this.btnAddEditPlan.Name = "btnAddEditPlan";
            this.btnAddEditPlan.Size = new System.Drawing.Size(75, 23);
            this.btnAddEditPlan.TabIndex = 3;
            this.btnAddEditPlan.Text = "Add New Plan";
            this.btnAddEditPlan.UseVisualStyleBackColor = true;
            this.btnAddEditPlan.Visible = false;
            this.btnAddEditPlan.Click += new System.EventHandler(this.btnAddEditPlan_Click);
            // 
            // cmbMachine
            // 
            this.cmbMachine.FormattingEnabled = true;
            this.cmbMachine.Location = new System.Drawing.Point(76, 36);
            this.cmbMachine.Name = "cmbMachine";
            this.cmbMachine.Size = new System.Drawing.Size(121, 21);
            this.cmbMachine.TabIndex = 4;
            this.cmbMachine.SelectionChangeCommitted += new System.EventHandler(this.cmbMachine_SelectionChangeCommitted);
            // 
            // txtFrom
            // 
            this.txtFrom.Location = new System.Drawing.Point(76, 10);
            this.txtFrom.Name = "txtFrom";
            this.txtFrom.Size = new System.Drawing.Size(100, 20);
            this.txtFrom.TabIndex = 9;
            this.txtFrom.TextChanged += new System.EventHandler(this.txtFrom_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "From";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "MC Name";
            // 
            // ucAddNewPlan1
            // 
            this.ucAddNewPlan1.Location = new System.Drawing.Point(12, 63);
            this.ucAddNewPlan1.Name = "ucAddNewPlan1";
            this.ucAddNewPlan1.Size = new System.Drawing.Size(1475, 497);
            this.ucAddNewPlan1.TabIndex = 11;
            // 
            // dtpFrom
            // 
            this.dtpFrom.Location = new System.Drawing.Point(237, 5);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.Size = new System.Drawing.Size(132, 20);
            this.dtpFrom.TabIndex = 12;
            // 
            // frmAddEditPlan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1499, 647);
            this.Controls.Add(this.dtpFrom);
            this.Controls.Add(this.ucAddNewPlan1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtFrom);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmbMachine);
            this.Controls.Add(this.btnAddEditPlan);
            this.Controls.Add(this.label1);
            this.Name = "frmAddEditPlan";
            this.Text = "Edit Plan";
            this.Load += new System.EventHandler(this.frmAddEditPlan_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnAddEditPlan;
        private System.Windows.Forms.ComboBox cmbMachine;
        private System.Windows.Forms.TextBox txtFrom;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private ucAddNewPlan ucAddNewPlan1;
        private System.Windows.Forms.DateTimePicker dtpFrom;
    }
}