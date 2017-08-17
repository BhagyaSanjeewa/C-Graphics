namespace TestGraphics
{
    partial class frmEditPlans
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
            this.ucAddEditPlansNew1 = new TestGraphics.ucAddEditPlansNew();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpShowFrom = new System.Windows.Forms.DateTimePicker();
            this.lblFrom = new System.Windows.Forms.Label();
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbMachine = new System.Windows.Forms.ComboBox();
            this.lblEditableQty = new System.Windows.Forms.Label();
            this.txtEditableQty = new System.Windows.Forms.TextBox();
            this.txtRemainingQty = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnAddNew = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ucAddEditPlansNew1
            // 
            this.ucAddEditPlansNew1.Location = new System.Drawing.Point(22, 94);
            this.ucAddEditPlansNew1.Name = "ucAddEditPlansNew1";
            this.ucAddEditPlansNew1.NewPlanStartDate = new System.DateTime(((long)(0)));
            this.ucAddEditPlansNew1.selectedPlanDate = new System.DateTime(((long)(0)));
            this.ucAddEditPlansNew1.Size = new System.Drawing.Size(1285, 493);
            this.ucAddEditPlansNew1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(413, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 26;
            this.label1.Text = "Show From";
            // 
            // dtpShowFrom
            // 
            this.dtpShowFrom.Location = new System.Drawing.Point(476, 21);
            this.dtpShowFrom.Name = "dtpShowFrom";
            this.dtpShowFrom.Size = new System.Drawing.Size(132, 20);
            this.dtpShowFrom.TabIndex = 25;
            this.dtpShowFrom.ValueChanged += new System.EventHandler(this.dtpShowFrom_ValueChanged);
            this.dtpShowFrom.Validated += new System.EventHandler(this.dtpShowFrom_Validated);
            // 
            // lblFrom
            // 
            this.lblFrom.AutoSize = true;
            this.lblFrom.Location = new System.Drawing.Point(32, 22);
            this.lblFrom.Name = "lblFrom";
            this.lblFrom.Size = new System.Drawing.Size(56, 13);
            this.lblFrom.TabIndex = 24;
            this.lblFrom.Text = "From Date";
            // 
            // dtpFrom
            // 
            this.dtpFrom.Location = new System.Drawing.Point(92, 22);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.Size = new System.Drawing.Size(132, 20);
            this.dtpFrom.TabIndex = 23;
            this.dtpFrom.Validated += new System.EventHandler(this.dtpFrom_Validated);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(32, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 13);
            this.label2.TabIndex = 22;
            this.label2.Text = "MC Name";
            // 
            // cmbMachine
            // 
            this.cmbMachine.FormattingEnabled = true;
            this.cmbMachine.Location = new System.Drawing.Point(92, 49);
            this.cmbMachine.Name = "cmbMachine";
            this.cmbMachine.Size = new System.Drawing.Size(132, 21);
            this.cmbMachine.TabIndex = 21;
            this.cmbMachine.SelectionChangeCommitted += new System.EventHandler(this.cmbMachine_SelectionChangeCommitted);
            // 
            // lblEditableQty
            // 
            this.lblEditableQty.AutoSize = true;
            this.lblEditableQty.Location = new System.Drawing.Point(241, 23);
            this.lblEditableQty.Name = "lblEditableQty";
            this.lblEditableQty.Size = new System.Drawing.Size(64, 13);
            this.lblEditableQty.TabIndex = 27;
            this.lblEditableQty.Text = "Editable Qty";
            // 
            // txtEditableQty
            // 
            this.txtEditableQty.Location = new System.Drawing.Point(312, 20);
            this.txtEditableQty.Name = "txtEditableQty";
            this.txtEditableQty.ReadOnly = true;
            this.txtEditableQty.Size = new System.Drawing.Size(100, 20);
            this.txtEditableQty.TabIndex = 28;
            // 
            // txtRemainingQty
            // 
            this.txtRemainingQty.Location = new System.Drawing.Point(312, 46);
            this.txtRemainingQty.Name = "txtRemainingQty";
            this.txtRemainingQty.ReadOnly = true;
            this.txtRemainingQty.Size = new System.Drawing.Size(100, 20);
            this.txtRemainingQty.TabIndex = 30;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(241, 49);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 13);
            this.label3.TabIndex = 29;
            this.label3.Text = "Remaing Qty";
            // 
            // btnAddNew
            // 
            this.btnAddNew.Location = new System.Drawing.Point(418, 52);
            this.btnAddNew.Name = "btnAddNew";
            this.btnAddNew.Size = new System.Drawing.Size(75, 23);
            this.btnAddNew.TabIndex = 31;
            this.btnAddNew.Text = "Create New Plans";
            this.btnAddNew.UseVisualStyleBackColor = true;
            this.btnAddNew.Click += new System.EventHandler(this.btnAddNew_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(899, 17);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(75, 23);
            this.btnEdit.TabIndex = 32;
            this.btnEdit.Text = "Edit";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // frmEditPlans
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1319, 599);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnAddNew);
            this.Controls.Add(this.txtRemainingQty);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtEditableQty);
            this.Controls.Add(this.lblEditableQty);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtpShowFrom);
            this.Controls.Add(this.lblFrom);
            this.Controls.Add(this.dtpFrom);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbMachine);
            this.Controls.Add(this.ucAddEditPlansNew1);
            this.Name = "frmEditPlans";
            this.Text = "frmEditPlans";
            this.Load += new System.EventHandler(this.frmEditPlans_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ucAddEditPlansNew ucAddEditPlansNew1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpShowFrom;
        private System.Windows.Forms.Label lblFrom;
        private System.Windows.Forms.DateTimePicker dtpFrom;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbMachine;
        private System.Windows.Forms.Label lblEditableQty;
        private System.Windows.Forms.TextBox txtEditableQty;
        private System.Windows.Forms.TextBox txtRemainingQty;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnAddNew;
        private System.Windows.Forms.Button btnEdit;

    }
}