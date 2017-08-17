namespace TestGraphics
{
    partial class frmAddEditPlansNew
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
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbMachine = new System.Windows.Forms.ComboBox();
            this.lblFrom = new System.Windows.Forms.Label();
            this.btnAddNew = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpShowFrom = new System.Windows.Forms.DateTimePicker();
            this.ucAddEditPlansNew1 = new TestGraphics.ucAddEditPlansNew();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // dtpFrom
            // 
            this.dtpFrom.Location = new System.Drawing.Point(82, 12);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.Size = new System.Drawing.Size(132, 20);
            this.dtpFrom.TabIndex = 15;
            this.dtpFrom.Validated += new System.EventHandler(this.dtpFrom_Validated);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "MC Name";
            // 
            // cmbMachine
            // 
            this.cmbMachine.FormattingEnabled = true;
            this.cmbMachine.Location = new System.Drawing.Point(82, 39);
            this.cmbMachine.Name = "cmbMachine";
            this.cmbMachine.Size = new System.Drawing.Size(132, 21);
            this.cmbMachine.TabIndex = 13;
            this.cmbMachine.SelectionChangeCommitted += new System.EventHandler(this.cmbMachine_SelectionChangeCommitted);
            // 
            // lblFrom
            // 
            this.lblFrom.AutoSize = true;
            this.lblFrom.Location = new System.Drawing.Point(22, 12);
            this.lblFrom.Name = "lblFrom";
            this.lblFrom.Size = new System.Drawing.Size(56, 13);
            this.lblFrom.TabIndex = 16;
            this.lblFrom.Text = "From Date";
            // 
            // btnAddNew
            // 
            this.btnAddNew.Location = new System.Drawing.Point(245, 13);
            this.btnAddNew.Name = "btnAddNew";
            this.btnAddNew.Size = new System.Drawing.Size(75, 23);
            this.btnAddNew.TabIndex = 17;
            this.btnAddNew.Text = "Add New";
            this.btnAddNew.UseVisualStyleBackColor = true;
            this.btnAddNew.Click += new System.EventHandler(this.btnAddNew_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(1300, 13);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 18;
            this.btnSave.Text = "SavePlans";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(403, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 20;
            this.label1.Text = "Show From";
            // 
            // dtpShowFrom
            // 
            this.dtpShowFrom.Location = new System.Drawing.Point(466, 11);
            this.dtpShowFrom.Name = "dtpShowFrom";
            this.dtpShowFrom.Size = new System.Drawing.Size(132, 20);
            this.dtpShowFrom.TabIndex = 19;
            this.dtpShowFrom.ValueChanged += new System.EventHandler(this.dtpShowFrom_ValueChanged);
            this.dtpShowFrom.Validated += new System.EventHandler(this.dtpShowFrom_Validated);
            // 
            // ucAddEditPlansNew1
            // 
            this.ucAddEditPlansNew1.AutoScroll = true;
            this.ucAddEditPlansNew1.Location = new System.Drawing.Point(12, 69);
            this.ucAddEditPlansNew1.Name = "ucAddEditPlansNew1";
            this.ucAddEditPlansNew1.NewPlanStartDate = new System.DateTime(((long)(0)));
            this.ucAddEditPlansNew1.selectedPlanDate = new System.DateTime(((long)(0)));
            this.ucAddEditPlansNew1.Size = new System.Drawing.Size(1409, 542);
            this.ucAddEditPlansNew1.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(660, 32);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 21;
            this.button1.Text = "Add New";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(763, 32);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 22;
            this.button2.Text = "Add New";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // frmAddEditPlansNew
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1433, 623);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtpShowFrom);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnAddNew);
            this.Controls.Add(this.lblFrom);
            this.Controls.Add(this.dtpFrom);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbMachine);
            this.Controls.Add(this.ucAddEditPlansNew1);
            this.Name = "frmAddEditPlansNew";
            this.Text = "frmAddEditPlansNew";
            this.Load += new System.EventHandler(this.frmAddEditPlansNew_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ucAddEditPlansNew ucAddEditPlansNew1;
        private System.Windows.Forms.DateTimePicker dtpFrom;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbMachine;
        private System.Windows.Forms.Label lblFrom;
        private System.Windows.Forms.Button btnAddNew;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpShowFrom;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}