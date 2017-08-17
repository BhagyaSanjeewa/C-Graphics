using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TestGraphics
{
    public partial class frmAddData : Form
    {
        private DateTime dtFromDate;
        public List<GraphicalPlan> lstGlobalGraphicalPlan { get; set; } 
        public int NoOfHours { get; set; }
        public decimal AllocQty { get; set; }
        private bool IsLoadedFromEditMode = false;
        private Plan objPlan = null;

        public frmAddData()
        {
            InitializeComponent();
        }

        public frmAddData(DateTime dtFromDate)
        {
            InitializeComponent();
            this.dtFromDate = dtFromDate;
            lblAllocQty.Text = AllocQty.ToString();
        }

        public frmAddData(bool isLoadedFromEditMode,Plan objPlan)
        {
            InitializeComponent();
            this.IsLoadedFromEditMode = isLoadedFromEditMode;
            this.objPlan  = objPlan;
            lblAllocQty.Text = this.objPlan.AllocQty.ToString();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (txtNoOfTapes.Text != String.Empty || txtOutPut.Text != String.Empty || txtAllocKG.Text != String.Empty)
            {
                NoOfHours = (int)((Convert.ToInt32(txtNoOfTapes.Text) * Convert.ToInt32(txtOutPut.Text)) / Convert.ToDecimal(txtAllocKG.Text));
                AllocQty = Convert.ToDecimal(txtAllocKG.Text);
            }

        }

        private void frmAddData_Load(object sender, EventArgs e)
        {
            lblAllocQty.Text = AllocQty.ToString();
        }

        
    }
}
