using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Transactions;
using System.Data.SqlClient;

namespace TestGraphics
{
    public partial class frmAddEditPlansNew : Form
    {
        List<Machine> lstExistingMachines = new List<Machine>();
        List<Machine> lstMachine = new List<Machine>();
        public int NextPlanFrom { get; set; }
        private const int MaxHours = 24;
        private string SelectedMCName = "";
        public DateTime plnFrom;
        private int OperationType = 1;
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
                objPlan.From = Convert.ToInt32(dr["intFromHrs"]);
                objPlan.SequenceNo = Convert.ToInt32(dr["intSequenceNo"]);
                objPlan.To = Convert.ToInt32(dr["intToHrs"]);
                objPlan.GroupID = Convert.ToInt32(dr["intGroupID"]);
                objPlan.MCID = Convert.ToInt32(dr["intMCID"]);
                objPlan.AllocQty = Convert.ToDecimal(dr["decAllocQty"]);
                objPlan.RowVersion = Convert.ToString(dr["vcRowVersion"]);
                objPlan.IsAlreadyAddedPlan = true;
                objPlan.FromDate = Convert.ToDateTime(dr["dtPlanDate"]);
                lstPlan.Add(objPlan);
                objMachine.LstPlan = lstPlan;
            }

            return lstPlan;
        }

        public frmAddEditPlansNew()
        {
            InitializeComponent();
            ucAddEditPlansNew1.OperationType = Convert.ToInt32(TestGraphics.OperationType.Add);
        }
        public frmAddEditPlansNew(int operationType, Plan objPlan)
        {
            InitializeComponent();
            this.OperationType = operationType;
            dtpShowFrom.Value = Convert.ToDateTime(objPlan.FromDate);

        }
        private void frmAddEditPlansNew_Load(object sender, EventArgs e)
        {
            FillMachines();

            lstExistingMachines = ucAddEditPlansNew1.lstMACHINES;
            try
            {


                Machine objMachine = null;
                List<Machine> temp = new List<Machine>();
                temp = (List<Machine>)cmbMachine.DataSource;
                foreach (var item in temp)
                {
                    objMachine  = new Machine();
                    objMachine.MCID = Convert.ToInt32(item.MCID);
                    objMachine.MCName = (from lst in temp
                                         where lst.MCID.Equals(objMachine.MCID)
                                         select lst.MCName).SingleOrDefault();
                    objMachine.Capacity = (from lst in temp
                                           where lst.MCID.Equals(objMachine.MCID)
                                           select lst.Capacity).SingleOrDefault();
                    objMachine.LstPlan = FillCurrentMachinePlans(objMachine.MCID);                  
                   
                    lstExistingMachines.Add(objMachine);
                    
                    SelectedMCName = objMachine.MCName;
                }                
                ucAddEditPlansNew1.lstMACHINES = lstExistingMachines;
                ucAddEditPlansNew1.Refresh();
            }
            catch (Exception ex)
            {

                throw ex;
            }

            ucAddEditPlansNew1.OperationType = this.OperationType;
            ucAddEditPlansNew1.NewPlanStartDate = Convert.ToDateTime(dtpFrom.Value);
            ucAddEditPlansNew1.AutoScroll = false;
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

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            try
            {
                lstMachine = ucAddEditPlansNew1.lstMACHINES;
                ucAddEditPlansNew1.OperationType = 2;
                #region Validating Maximum Hours Reached
                //int currentGroupPlanHours = 0;
                //List<Machine> lstSelectedMachine = new List<Machine>();
                //lstSelectedMachine = (from lst in lstMachine
                //                      where lst.MCID.Equals(Convert.ToInt32(cmbMachine.SelectedValue))
                //                      select lst).ToList();
                //if (lstSelectedMachine != null && lstSelectedMachine.Count > 0)
                //{
                //    List<Plan> lstGroupPlans = new List<Plan>();
                //    lstGroupPlans = (from lst in lstSelectedMachine[0].LstPlan
                //                     where lst.GroupID.Equals((Convert.ToInt32(txtFrom.Text) / 12) + 1)
                //                     select lst).ToList();
                //    if (lstGroupPlans.Count > 0)
                //    {
                //        foreach (var item in lstGroupPlans)
                //        {
                //            currentGroupPlanHours = currentGroupPlanHours + (item.To - item.From);
                //        }
                //    }

                //    if (currentGroupPlanHours > 11)
                //    {
                //        throw new ApplicationException("Maximum Plan Hours Reached Select New Date");
                //    }
                //}
                #endregion

                frmAddData objfrmAddData = new frmAddData();
                objfrmAddData.ShowDialog();

                //if ((currentGroupPlanHours + objfrmAddData.NoOfHours) > 11)
                //{
                //    throw new ApplicationException("Maximum Plan Hours Reached Select New Date");
                //}

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
            catch (ApplicationException ax)
            {
                MessageBox.Show(ax.Message);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (OperationType == Convert.ToInt32(TestGraphics.OperationType.Add))
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
                                    Execute.AddParameter("@MCID",pn.MCID),
                                    Execute.AddParameter("@FromDate",pn.FromDate),
                                    Execute.AddParameter("@FromHrs",pn.From),
                                    Execute.AddParameter("@ToHrs",pn.To),
                                    Execute.AddParameter("@AllocQty",pn.AllocQty),
                                    Execute.AddParameter("@SequenceNo",pn.SequenceNo),
                                };
                                    objExecute.Executes("spSaveNewPlans", param, CommandType.StoredProcedure);
                                }
                                res = true;
                                ts.Complete();
                            }
                            if (res)
                            {
                                MessageBox.Show("Successfully Saved");
                                cmbMachine.SelectedIndex = -1;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void dtpShowFrom_Validated(object sender, EventArgs e)
        {
            plnFrom = Convert.ToDateTime(dtpShowFrom.Value);
            ucAddEditPlansNew1.plnFrom = plnFrom;
            Machine objMachine = null;
            List<Machine> temp = new List<Machine>();
            temp = (List<Machine>)cmbMachine.DataSource;
            foreach (var item in temp)
            {
                objMachine = new Machine();
                objMachine.MCID = Convert.ToInt32(item.MCID);
                objMachine.MCName = (from lst in temp
                                     where lst.MCID.Equals(objMachine.MCID)
                                     select lst.MCName).SingleOrDefault();
                objMachine.Capacity = (from lst in temp
                                       where lst.MCID.Equals(objMachine.MCID)
                                       select lst.Capacity).SingleOrDefault();
                objMachine.LstPlan = FillCurrentMachinePlans(objMachine.MCID);

                lstExistingMachines.Add(objMachine);

                SelectedMCName = objMachine.MCName;
            }
            ucAddEditPlansNew1.lstMACHINES = lstExistingMachines;
            ucAddEditPlansNew1.Refresh();
        }

        private void dtpShowFrom_ValueChanged(object sender, EventArgs e)
        {
            plnFrom = Convert.ToDateTime(dtpShowFrom.Value);
            ucAddEditPlansNew1.plnFrom = plnFrom;
            Machine objMachine = null;
            List<Machine> temp = new List<Machine>();
            temp = (List<Machine>)cmbMachine.DataSource;
            foreach (var item in temp)
            {
                objMachine = new Machine();
                objMachine.MCID = Convert.ToInt32(item.MCID);
                objMachine.MCName = (from lst in temp
                                     where lst.MCID.Equals(objMachine.MCID)
                                     select lst.MCName).SingleOrDefault();
                objMachine.Capacity = (from lst in temp
                                       where lst.MCID.Equals(objMachine.MCID)
                                       select lst.Capacity).SingleOrDefault();
                objMachine.LstPlan = FillCurrentMachinePlans(objMachine.MCID);

                lstExistingMachines.Add(objMachine);

                SelectedMCName = objMachine.MCName;
            }
            ucAddEditPlansNew1.lstMACHINES = lstExistingMachines;
            ucAddEditPlansNew1.Refresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

    }

    
}
