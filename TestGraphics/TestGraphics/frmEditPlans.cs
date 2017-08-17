using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Transactions;

namespace TestGraphics
{
    public partial class frmEditPlans : Form
    {
        private int p;
        private Plan objPlan;
        List<Machine> lstExistingMachines = new List<Machine>();
        List<Machine> lstMachine = new List<Machine>();
        public int NextPlanFrom { get; set; }
        private const int MaxHours = 24;
        private string SelectedMCName = "";
        public DateTime plnFrom;
        private int OperationType = 1;
        public decimal RemainingQty = 0M;
        bool isEditingPlanRemoved = false;

        private void FillMachines()
        {
            try
            {
                Execute objExecute = new Execute();
                DataTable dtMachines = (DataTable)objExecute.Executes("spGetMachines", ReturnType.DataTable, CommandType.StoredProcedure);

                List<Machine> lstMachine = new List<Machine>();
                Machine objMachine = null;
                foreach (DataRow dr in dtMachines.Rows)
                {
                    objMachine = new Machine();
                    objMachine.MCID = Convert.ToInt32(dr["intMCID"]);
                    objMachine.MCName = Convert.ToString(dr["vcMCCode"]);
                    objMachine.Capacity = Convert.ToDecimal(dr["decCapacity"]);
                    lstMachine.Add(objMachine);
                }

                cmbMachine.DataSource = lstMachine;
                cmbMachine.DisplayMember = "MCName";
                cmbMachine.ValueMember = "MCID";
                cmbMachine.SelectedIndex = -1;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private List<Plan> FillCurrentMachinePlans(int mcID)
        {
            Execute objExecute = new Execute();
            SqlParameter[] param = new SqlParameter[]
                        {
                            Execute.AddParameter("@intMCID",mcID)
                        };
            DataTable dt = (DataTable)objExecute.Executes("spGetSavedPlans", ReturnType.DataTable, param, CommandType.StoredProcedure);
            List<Plan> lstPlan = new List<Plan>();
            Plan objPlan;
            Machine objMachine = null;

            objMachine = new Machine();
            foreach (DataRow dr in dt.Rows)
            {
                objPlan = new Plan();
                objPlan.PlanID = Convert.ToInt32(dr["intPlanDetailID"]);
                if (objPlan.PlanID == this.objPlan.PlanID)
                {
                    objPlan.IsEditable = true;
                }
                objPlan.From = Convert.ToInt32(dr["intFromHrs"]);
                objPlan.SequenceNo = Convert.ToInt32(dr["intSequenceNo"]);
                objPlan.To = Convert.ToInt32(dr["intToHrs"]);
                objPlan.GroupID = Convert.ToInt32(dr["intGroupID"]);
                objPlan.MCID = Convert.ToInt32(dr["intMCID"]);
                objPlan.AllocQty = Convert.ToDecimal(dr["decAllocQty"]);
                objPlan.RowVersion = Convert.ToString(dr["vcRowVersion"]);
                //objPlan.RowVersion = Convert.ToString(dr["vcRowVersion"]);
                objPlan.IsAlreadyAddedPlan = true;
                objPlan.FromDate = Convert.ToDateTime(dr["dtPlanDate"]);                
                lstPlan.Add(objPlan);
                objMachine.LstPlan = lstPlan;
            }

            return lstPlan;
        }

        public void SetDetails(object sender, EventArgs e)
        {
            if (Convert.ToDecimal(txtEditableQty.Text) >= (RemainingQty + ucAddEditPlansNew1.RemovedQty))
            {                
                RemainingQty = RemainingQty + ucAddEditPlansNew1.RemovedQty;
                txtRemainingQty.Text = RemainingQty.ToString();
            }            
        }


        public frmEditPlans()
        {
            InitializeComponent();
        }

        public frmEditPlans(int opreratorType, Plan pn)
        {
            InitializeComponent();
            this.OperationType = opreratorType;
            this.objPlan = pn;
            txtEditableQty.Text = pn.AllocQty.ToString();
            txtRemainingQty.Text = pn.AllocQty.ToString();
            RemainingQty = pn.AllocQty;
        }

        private void frmEditPlans_Load(object sender, EventArgs e)
        {
            FillMachines();
            cmbMachine.SelectedValue = objPlan.MCID;
            cmbMachine_SelectionChangeCommitted(sender, e);
            ucAddEditPlansNew1.NewPlanStartDate = Convert.ToDateTime(dtpFrom.Value);
            ucAddEditPlansNew1.OperationType = this.OperationType;
            ucAddEditPlansNew1.UpdateDetails += new EventHandler(SetDetails);
            ucAddEditPlansNew1.HorizontalScroll.Visible = true;
            ucAddEditPlansNew1.VerticalScroll.Visible = true;
           
        }

        private void cmbMachine_SelectionChangeCommitted(object sender, EventArgs e)
        {
            lstExistingMachines = ucAddEditPlansNew1.lstMACHINES;
            try
            {
                Machine objMachine = new Machine();
                List<Machine> temp = new List<Machine>();
                temp = (List<Machine>)cmbMachine.DataSource;
                objMachine.MCID = Convert.ToInt32(cmbMachine.SelectedValue);
                objMachine.MCName = (from lst in temp
                                     where lst.MCID.Equals(objMachine.MCID)
                                     select lst.MCName).SingleOrDefault();
                objMachine.Capacity = (from lst in temp
                                       where lst.MCID.Equals(objMachine.MCID)
                                       select lst.Capacity).SingleOrDefault();
                objMachine.LstPlan = FillCurrentMachinePlans(objMachine.MCID);
                bool isAlreadyAdded = false;
                foreach (Machine obj in lstExistingMachines)
                {
                    if (obj.MCID == objMachine.MCID)
                    {
                        obj.LstPlan = objMachine.LstPlan;
                        isAlreadyAdded = true;
                        break;
                    }
                }
                if (!isAlreadyAdded)
                {
                    lstExistingMachines.Add(objMachine);
                }
                SelectedMCName = objMachine.MCName;
                ucAddEditPlansNew1.lstMACHINES = lstExistingMachines;
                ucAddEditPlansNew1.Refresh();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void dtpFrom_Validated(object sender, EventArgs e)
        {
            ucAddEditPlansNew1.NewPlanStartDate = Convert.ToDateTime(dtpFrom.Value);
        }

        private void dtpShowFrom_Validated(object sender, EventArgs e)
        {
            //plnFrom = Convert.ToDateTime(dtpShowFrom.Value);
            //ucAddEditPlansNew1.plnFrom = plnFrom;
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            try
            {              
                if (Convert.ToDecimal(txtRemainingQty.Text) > 0M)
                {
                    frmAddData objfrmAddData = new frmAddData(true, this.objPlan);
                    objfrmAddData.AllocQty = Convert.ToDecimal(txtRemainingQty.Text);
                    objfrmAddData.ShowDialog();
                    this.RemainingQty = (this.RemainingQty - objfrmAddData.AllocQty);
                    txtRemainingQty.Text = this.RemainingQty.ToString();
                    int noofHours = objfrmAddData.NoOfHours;

                    if (noofHours > 0)
                    {
                        int indexOfCurrentMachine = 0;
                        int currentPlanedHours = 0;
                        Machine objMachines = new Machine();
                        var machine = (Machine)ucAddEditPlansNew1.lstMACHINES.Where(x => x.MCID == this.objPlan.MCID).SingleOrDefault();
                        objMachines = machine;
                        if (objMachines != null)
                        {
                            indexOfCurrentMachine = ucAddEditPlansNew1.lstMACHINES.IndexOf(objMachines);
                            foreach (Plan pn in objMachines.LstPlan)
                            {
                                currentPlanedHours = (currentPlanedHours + (pn.To - pn.From));
                            }

                            currentPlanedHours = (currentPlanedHours - (this.objPlan.To - this.objPlan.From));
                        }


                        if ((currentPlanedHours + noofHours) <= MaxHours)
                        {
                            if (!isEditingPlanRemoved)
                            {
                                //ucAddEditPlansNew1.lstMACHINES[indexOfCurrentMachine].LstPlan.IndexOf((Plan)ucAddEditPlansNew1.lstMACHINES[indexOfCurrentMachine].LstPlan.Where(x => x.PlanID == this.objPlan.PlanID).SingleOrDefault());
                                ucAddEditPlansNew1.lstMACHINES[indexOfCurrentMachine].LstPlan.RemoveAt(ucAddEditPlansNew1.lstMACHINES[indexOfCurrentMachine].LstPlan.IndexOf((Plan)ucAddEditPlansNew1.lstMACHINES[indexOfCurrentMachine].LstPlan.Where(x => x.PlanID == this.objPlan.PlanID).SingleOrDefault()));
                                isEditingPlanRemoved = true;
                            }

                            CommonMethods objCommonMethods = new CommonMethods();
                            Machine objMachine = new Machine();
                            objMachine = ucAddEditPlansNew1.lstMACHINES.Find(x => x.MCID == Convert.ToInt32(cmbMachine.SelectedValue));

                            if (objMachine.LstPlan != null)
                            {
                                List<Plan> lstTmp = new List<Plan>();
                                lstTmp = objCommonMethods.DrawPlanByDate(ucAddEditPlansNew1.lstMACHINES, Convert.ToInt32(cmbMachine.SelectedValue), Convert.ToDateTime(dtpFrom.Value), objfrmAddData.NoOfHours, objfrmAddData.AllocQty);
                                var xx = objMachine.LstPlan.Concat(lstTmp);
                                objMachine.LstPlan = xx.ToList();
                            }
                            else
                            {
                                objMachine.LstPlan = objCommonMethods.DrawPlanByDate(ucAddEditPlansNew1.lstMACHINES, Convert.ToInt32(cmbMachine.SelectedValue), Convert.ToDateTime(dtpFrom.Value), objfrmAddData.NoOfHours, objfrmAddData.AllocQty);
                            }

                            foreach (var item in ucAddEditPlansNew1.lstMACHINES.ToList())
                            {
                                if (item.MCID.Equals(objMachine.MCID))
                                {
                                    ucAddEditPlansNew1.lstMACHINES.Remove(item);
                                    break;
                                }
                            }
                            ucAddEditPlansNew1.lstMACHINES.Add(objMachine);
                            ucAddEditPlansNew1.Refresh();
                        }
                        else
                        {
                            throw new ApplicationException("Maximum Plan Hours Reached");
                        }
                    }
                }

                else
                {
                    throw new ApplicationException("You have Planed All the Qty");
                }
            }
            catch (ApplicationException ax)
            {
                MessageBox.Show(ax.Message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                
                List<Machine> lstAddedMachine = new List<Machine>();
                lstAddedMachine = (from lst in ucAddEditPlansNew1.lstMACHINES
                                   select lst).ToList();

                Machine objMachine = null;
                if (lstAddedMachine != null && lstAddedMachine.Count > 0)
                {

                    foreach (Machine mc in lstAddedMachine)
                    {
                        objMachine = new Machine();
                        objMachine.MCID = mc.MCID;
                        List<Plan> lstCurrentPlans = new List<Plan>();
                        lstCurrentPlans = mc.LstPlan;
                        Plan objPlan = null;
                        List<Plan> lstNewlyAddedPlans = new List<Plan>();
                        List<Plan> lstToSavePlans = new List<Plan>();
                        if (lstCurrentPlans != null && lstCurrentPlans.Count > 0)
                        {
                            lstNewlyAddedPlans = (from lst in lstCurrentPlans
                                                  where lst.IsAlreadyAddedPlan.Equals(false)
                                                  select lst).ToList();

                            foreach (Plan pn in lstNewlyAddedPlans)
                            {
                                objPlan = new Plan();
                                objPlan.MCID = pn.MCID;
                                objPlan.FromDate = pn.FromDate;
                                objPlan.From = pn.From;
                                objPlan.To = pn.To;
                                objPlan.AllocQty = pn.AllocQty;
                                objPlan.SequenceNo = pn.SequenceNo;
                                lstToSavePlans.Add(objPlan);
                            }
                        }
                        bool res = false;
                        using (TransactionScope ts = new TransactionScope())
                        {
                            foreach (Plan pn in lstToSavePlans)
                            {
                                Execute objExecute = new Execute();
                                SqlParameter[] param = new SqlParameter[]
                                {
                                    Execute.AddParameter("@PlanID", this.objPlan.PlanID),
                                    Execute.AddParameter("@MCID",pn.MCID),
                                    Execute.AddParameter("@FromDate",pn.FromDate),
                                    Execute.AddParameter("@FromHrs",pn.From),
                                    Execute.AddParameter("@ToHrs",pn.To),
                                    Execute.AddParameter("@AllocQty",pn.AllocQty),
                                    Execute.AddParameter("@SequenceNo",pn.SequenceNo),
                                    Execute.AddParameter("@PreRowVersion",this.objPlan.RowVersion)
                                };
                                objExecute.Executes("spEditPlan", param, CommandType.StoredProcedure);
                            }
                            res = true;
                            ts.Complete();
                        }
                        if (res)
                        {
                            MessageBox.Show("Successfully Edited");
                            cmbMachine.SelectedIndex = -1;
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                if (sqlEx.Errors[0].Class == 16)
                {
                    MessageBox.Show(sqlEx.Message);
                }
            }
            catch (ApplicationException ax)
            {
                MessageBox.Show(ax.Message);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void dtpShowFrom_ValueChanged(object sender, EventArgs e)
        {
            plnFrom = Convert.ToDateTime(dtpShowFrom.Value);
            ucAddEditPlansNew1.plnFrom = plnFrom;
        }


    }
}
