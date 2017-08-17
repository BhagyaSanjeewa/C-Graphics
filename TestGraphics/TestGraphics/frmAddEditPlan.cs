using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace TestGraphics
{
    public partial class frmAddEditPlan : Form
    {
        List<Machine> lstExistingMachines = new List<Machine>();
        List<Machine> lstMachine = new List<Machine>();
        public int NextPlanFrom { get; set; }
        private const int MaxHours = 12;
        private string SelectedMCName = "";
        public frmAddEditPlan()
        {
            InitializeComponent();

        }

        public frmAddEditPlan(int operationType)
        {
            InitializeComponent();
            ucAddNewPlan1.OperationType = operationType;
        }

        private void FillMachines()
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

        private List<Plan> FillCurrentMachinePlans(int mcID)
        {
            Execute objExecute = new Execute();
            SqlParameter[] param = new SqlParameter[]
                        {
                            Execute.AddParameter("@intMCID",mcID)
                        };
            DataTable dt = (DataTable)objExecute.Executes("spGetPlan", ReturnType.DataTable, param, CommandType.StoredProcedure);
            List<Plan> lstPlan = new List<Plan>();
            Plan objPlan;
            Machine objMachine = null;

            objMachine = new Machine();          
            foreach (DataRow dr in dt.Rows)
            {
                objPlan = new Plan();
                objPlan.PlanID = Convert.ToInt32(dr["intPlanID"]);
                objPlan.From = Convert.ToInt32(dr["intFrom"]);
                objPlan.SequenceNo = Convert.ToInt32(dr["intSequenceNo"]);
                objPlan.To = Convert.ToInt32(dr["intTo"]);
                objPlan.GroupID = Convert.ToInt32(dr["intGroupID"]);
                objPlan.MCID = Convert.ToInt32(dr["intMCID"]);
                objPlan.AllocQty = Convert.ToDecimal(dr["decAllocatedQty"]);
                objPlan.IsAlreadyAddedPlan = true;
                lstPlan.Add(objPlan);
                objMachine.LstPlan = lstPlan;
            }

            return lstPlan;
        }
        private void frmAddEditPlan_Load(object sender, EventArgs e)
        {
            FillMachines();
        }

        private void cmbMachine_SelectionChangeCommitted(object sender, EventArgs e)
        {
            lstExistingMachines = ucAddNewPlan1.lstMACHINES;
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
                    if(obj.MCID == objMachine.MCID )
                    {
                        isAlreadyAdded = true;
                        break;
                    }
                }

                if (!isAlreadyAdded)
                {
                    lstExistingMachines.Add(objMachine);
                }
                SelectedMCName = objMachine.MCName;
                ucAddNewPlan1.lstMACHINES = lstExistingMachines;
                ucAddNewPlan1.Refresh();
                }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

        private void btnAddEditPlan_Click(object sender, EventArgs e)
        {
            try
            {
                lstMachine = ucAddNewPlan1.lstMACHINES;
                #region Validating Maximum Hours Reached
                int currentGroupPlanHours = 0;
                List<Machine> lstSelectedMachine = new List<Machine>();
                lstSelectedMachine = (from lst in lstMachine
                                      where lst.MCID.Equals(Convert.ToInt32(cmbMachine.SelectedValue))
                                      select lst).ToList();
                if (lstSelectedMachine != null && lstSelectedMachine.Count > 0)
                {
                    List<Plan> lstGroupPlans = new List<Plan>();
                    lstGroupPlans = (from lst in lstSelectedMachine[0].LstPlan
                                     where lst.GroupID.Equals((Convert.ToInt32(txtFrom.Text) / 12) + 1)
                                     select lst).ToList();
                    if (lstGroupPlans.Count > 0)
                    {
                        foreach (var item in lstGroupPlans)
                        {
                            currentGroupPlanHours = currentGroupPlanHours + (item.To - item.From);
                        }
                    }

                    if (currentGroupPlanHours > 11)
                    {
                        throw new ApplicationException("Maximum Plan Hours Reached Select New Date");
                    }
                }
                #endregion

                frmAddData objfrmAddData = new frmAddData();
                objfrmAddData.ShowDialog();

                if ((currentGroupPlanHours + objfrmAddData.NoOfHours) > 11)
                {
                    throw new ApplicationException("Maximum Plan Hours Reached Select New Date");
                }

                CommonMethods objCommonMethods = new CommonMethods();
                Machine objMachine = new Machine();
                objMachine = ucAddNewPlan1.lstMACHINES.Find(x => x.MCID == Convert.ToInt32(cmbMachine.SelectedValue));

                if (objMachine.LstPlan != null)
                {
                    List<Plan> lstTmp = new List<Plan>();
                    lstTmp = objCommonMethods.DrawPlan(ucAddNewPlan1.lstMACHINES, Convert.ToInt32(cmbMachine.SelectedValue), Convert.ToInt32(txtFrom.Text), objfrmAddData.NoOfHours, objfrmAddData.AllocQty);
                    var xx = objMachine.LstPlan.Concat(lstTmp);
                    objMachine.LstPlan = xx.ToList();
                }
                else
                {
                    objMachine.LstPlan = objCommonMethods.DrawPlan(ucAddNewPlan1.lstMACHINES, Convert.ToInt32(cmbMachine.SelectedValue), Convert.ToInt32(txtFrom.Text), objfrmAddData.NoOfHours, objfrmAddData.AllocQty);
                }

                foreach (var item in ucAddNewPlan1.lstMACHINES.ToList())
                {
                    if (item.MCID.Equals(objMachine.MCID))
                    {
                        ucAddNewPlan1.lstMACHINES.Remove(item);
                        break;
                    }
                }
                ucAddNewPlan1.lstMACHINES.Add(objMachine);
                ucAddNewPlan1.Refresh();

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

        private void txtFrom_TextChanged(object sender, EventArgs e)
        {
            ucAddNewPlan1.dtfrom = Convert.ToInt32(txtFrom.Text);
            ucAddNewPlan1.Refresh();
        }
    }
}
