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
    public partial class frmMainGraphic : Form
    {
        List<Machine> lstMachine = new List<Machine>();
        public int NextPlanFrom { get; set; }
        private const int MaxHours = 12;

        public frmMainGraphic()
        {
            InitializeComponent();
        }
        private void btnProcess_Click(object sender, EventArgs e)
        {

        }

        private void btnDraw_Click(object sender, EventArgs e)
        {
            try
            {
                lstMachine = ucGraphicNew1.lstMACHINES;
                #region Validating Maximum Hours Reached
                int currentGroupPlanHours = 0;
                List<Machine> lstSelectedMachine = new List<Machine>();
                lstSelectedMachine = (from lst in lstMachine
                                      where lst.MCID.Equals(Convert.ToInt32(txtMCID.Text))
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

                int mcID = Convert.ToInt32(txtMCID.Text);
                int from = Convert.ToInt32(txtFrom.Text);
                decimal allocQty = 0M;
                frmAddData objfrmAddData = new frmAddData();
                objfrmAddData.ShowDialog();
                int to = 0;
                int noOfHours = 0;
                if (objfrmAddData.NoOfHours > 0)
                {
                    noOfHours = objfrmAddData.NoOfHours;
                    allocQty = objfrmAddData.AllocQty;
                    to = from + noOfHours;
                }

                if (currentGroupPlanHours + noOfHours > 11)
                {
                    throw new ApplicationException("Plan Exceed the Maximum Plan Hours Select New Date");
                }
                txtTo.Text = to.ToString();                
                Machine objMCN = new Machine();
                Plan objplan = new Plan();
                List<Plan> lstPlan = new List<Plan>();

                objMCN.MCID = mcID;

                int currentGroupID = 0;
                currentGroupID = from / 12 + 1;

                if (lstMachine.FindAll(x => x.MCID == objMCN.MCID).Count > 0)
                {

                    List<Machine> lstMachines = (from lst in ucGraphicNew1.lstMACHINES
                                                 where lst.MCID.Equals(mcID)
                                                 select lst).ToList();

                    if (lstMachines != null)
                    {
                        List<Plan> lstLocationNotChangedPlans = new List<Plan>();
                        lstLocationNotChangedPlans = (from lst in lstMachines[0].LstPlan
                                                      where lst.GroupID.Equals(currentGroupID)
                                                      select lst).ToList();


                        List<Plan> lstLocationChangedPlans = new List<Plan>();
                        lstLocationChangedPlans = (from lst in lstMachines[0].LstPlan
                                                   where lst.IsLocationChanged.Equals(true)
                                                   select lst).ToList();

                        List<Plan> lstCurrentGroupPlans = new List<Plan>();
                        lstCurrentGroupPlans = (from lst in lstMachines[0].LstPlan
                                                where lst.GroupID.Equals((Convert.ToInt32(txtFrom.Text) / 12) + 1)
                                                select lst).ToList();

                        #region Old


                        //if (lstLocationChangedPlans == null || lstLocationChangedPlans.Count == 0)
                        //{
                        //    var xxx = lstMachines[0].LstPlan[lstMachines[0].LstPlan.Count - 1].To;
                        //    int startLocation = 0;
                        //    int sequence = 0;
                        //    if (lstLocationNotChangedPlans != null && lstLocationNotChangedPlans.Count > 1)
                        //    {
                        //        var xx = lstLocationNotChangedPlans[lstLocationNotChangedPlans.Count - 1].To;
                        //        var seq = lstMachines[0].LstPlan[lstMachines[0].LstPlan.Count - 1].SequenceNo;
                        //        startLocation = xx;
                        //        sequence = seq;
                        //    }
                        //    else
                        //    {
                        //        var xx = lstLocationNotChangedPlans[lstLocationNotChangedPlans.Count - 1].To;
                        //        var seq = lstMachines[0].LstPlan[lstMachines[0].LstPlan.Count - 1].SequenceNo;
                        //        startLocation = xx;
                        //        sequence = seq;
                        //    }


                        //    Machine objM = lstMachine.Find(x => x.MCID == objMCN.MCID);
                        //    if (objM != null)
                        //    {
                        //        lstMachine.Remove(lstMachine.Find(x => x.MCID == objMCN.MCID));
                        //    }

                        //    objplan.From = startLocation;
                        //    objplan.To = objplan.From + noOfHours;
                        //    objplan.XCor = 0;
                        //    objplan.SequenceNo = sequence + 1;
                        //    objM.LstPlan.Add(objplan);
                        //    lstMachine.Add(objM);
                        //}
                        //else
                        //{
                        //    int startLocation = 0;
                        //    int sequence = 0;

                        //    lstLocationNotChangedPlans = lstLocationNotChangedPlans.OrderBy(x => x.SequenceNo).ToList();
                        //    if (lstLocationNotChangedPlans.Count > 1)
                        //    {
                        //        for (int i = 0; i < lstLocationNotChangedPlans.Count; i++)
                        //        {
                        //            if ((lstLocationNotChangedPlans.Count - 1) > i)
                        //            {
                        //                int availableHours = lstLocationNotChangedPlans[i + 1].From - lstLocationNotChangedPlans[i].To;
                        //                if (availableHours >= noOfHours)
                        //                {
                        //                    startLocation = lstLocationNotChangedPlans[i].To;
                        //                    sequence = lstLocationNotChangedPlans[i].SequenceNo;
                        //                    break;
                        //                }

                        //            }

                        //            else
                        //            {
                        //                startLocation = lstLocationNotChangedPlans[i].To;
                        //                sequence = lstLocationNotChangedPlans[i].SequenceNo;
                        //                break;
                        //            }
                        //        }
                        //    }

                        //    else
                        //    {
                        //        startLocation = from;
                        //        sequence = 0;
                        //    }
                        //    Machine objM = lstMachine.Find(x => x.MCID == objMCN.MCID);
                        //    if (objM != null)
                        //    {
                        //        lstMachine.Remove(lstMachine.Find(x => x.MCID == objMCN.MCID));
                        //    }

                        //    objplan.From = startLocation;
                        //    objplan.To = objplan.From + noOfHours;
                        //    objplan.XCor = 0;
                        //    objplan.SequenceNo = sequence + 1;
                        //    objM.LstPlan.Add(objplan);
                        //    lstMachine.Add(objM);
                        //} 
                        #endregion
                        int currentFilledPlanHours = 0;
                        if (lstCurrentGroupPlans.Count > 0)
                        {
                            foreach (var item in lstCurrentGroupPlans)
                            {
                                currentFilledPlanHours = currentFilledPlanHours + (item.To - item.From);
                            }
                        }

                        if (currentFilledPlanHours < 11)
                        {
                            int startLocation = 0;
                            int sequence = 0;
                            #region Rechange the sequence
                            CommonMethods objCommonMethods = new CommonMethods();
                            lstLocationNotChangedPlans = objCommonMethods.ChangeSequence(lstLocationNotChangedPlans);
                            lstLocationNotChangedPlans = objCommonMethods.ChangeXYCordinations(lstLocationNotChangedPlans);
                            #endregion
                            lstLocationNotChangedPlans = lstLocationNotChangedPlans.OrderBy(x => x.SequenceNo).ToList();
                            if (lstLocationNotChangedPlans.Count > 0)
                            {
                                for (int i = 0; i < lstLocationNotChangedPlans.Count; i++)
                                {
                                    if (lstLocationNotChangedPlans.Count == 1)
                                    {
                                        int availableHours = lstLocationNotChangedPlans[i].From - from;
                                        if (availableHours >= noOfHours)
                                        {
                                            startLocation = from;
                                            sequence = 0;
                                            break;
                                        }
                                    }
                                    if ((lstLocationNotChangedPlans.Count - 1) > i)
                                    {
                                        int availableHours = lstLocationNotChangedPlans[i + 1].From - lstLocationNotChangedPlans[i].To;
                                        if (availableHours >= noOfHours)
                                        {
                                            startLocation = lstLocationNotChangedPlans[i].To;
                                            sequence = lstLocationNotChangedPlans[i].SequenceNo;
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        startLocation = lstLocationNotChangedPlans[i].To;
                                        sequence = lstLocationNotChangedPlans[i].SequenceNo;
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                startLocation = from;
                                sequence = 0;
                            }
                            Machine objM = lstMachine.Find(x => x.MCID == objMCN.MCID);                          
                            objplan.From = startLocation;
                            objplan.To = objplan.From + noOfHours;
                            objplan.XCor = 0;
                            objplan.AllocQty = allocQty;
                            objplan.SequenceNo = sequence + 1;
                            objplan.IsAlreadyAddedPlan = false;
                            if ((objplan.To % 12) == 0)
                            {
                                objplan.GroupID = objplan.To / 12;
                            }
                            else
                            {
                                objplan.GroupID = (objplan.To / 12) + 1;
                            }
                            objM.LstPlan.Add(objplan);                            
                        }
                        else
                        {
                            throw new ApplicationException("Maximum Plan Hours Reached");
                        }
                    }
                    else
                    {
                        objplan.From = from;
                        objplan.To = to;
                        objplan.SequenceNo = 1;
                        objplan.AllocQty = allocQty;
                        objplan.IsAlreadyAddedPlan = false;
                        if ((objplan.To % 12) == 0)
                        {
                            objplan.GroupID = objplan.To / 12;
                        }
                        else
                        {
                            objplan.GroupID = (objplan.To / 12) + 1;
                        }
                        lstPlan.Add(objplan);
                        objMCN.LstPlan = lstPlan;                       
                    }
                }
                else
                {

                    objplan.From = from;
                    objplan.To = to;
                    objplan.SequenceNo = 1;
                    objplan.AllocQty = allocQty;
                    objplan.IsAlreadyAddedPlan = false;
                    if ((objplan.To % 12) == 0)
                    {
                        objplan.GroupID = objplan.To / 12;
                    }
                    else
                    {
                        objplan.GroupID = (objplan.To / 12) + 1;
                    }

                    lstPlan.Add(objplan);
                    objMCN.LstPlan = lstPlan;
                    //lstMachine.Add(objMCN);
                }
                ucGraphicNew1.lstMACHINES.Add(objMCN);
                //ucGraphicNew1.lstMACHINES = lstMachine;
                ucGraphicNew1.mc = mcID.ToString();
                ucGraphicNew1.from = Convert.ToInt32(0);
                ucGraphicNew1.to = Convert.ToInt32(0);
                ucGraphicNew1.Refresh();

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

        private void frmMainGraphic_Load(object sender, EventArgs e)
        {
            try
            {
                GetSavedPlans();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {


        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                bool res = false;
                List<Plan> lstToSavePlans = new List<Plan>();
                Plan objPlan = null;
                foreach (Machine machine in lstMachine)
                {

                    if (machine.LstPlan != null && machine.LstPlan.Count > 0)
                    {
                        foreach (var item in machine.LstPlan)
                        {
                            objPlan = new Plan();                            
                            objPlan.From = item.From ;
                            objPlan.SequenceNo = item.SequenceNo;
                            objPlan.To = item.To;
                            objPlan.GroupID = item.GroupID ;
                            objPlan.MCID = machine.MCID;
                            objPlan.AllocQty = item.AllocQty;
                            lstToSavePlans.Add(objPlan);
                        }
                    }
                }
                DataTable dt = new DataTable();
                dt.Columns.Add("From", typeof(int));
                dt.Columns.Add("SequenceNo", typeof(int));
                dt.Columns.Add("To", typeof(int));
                dt.Columns.Add("GroupID", typeof(int));
                dt.Columns.Add("MCID", typeof(int));
                dt.Columns.Add("AllocQty", typeof(decimal));              
               
                
                if (lstToSavePlans != null && lstToSavePlans.Count > 0)
                {
                    foreach (var item in lstToSavePlans)
                    {
                        Execute objExecute = new Execute();
                        SqlParameter[] param = new SqlParameter[]
                        {
                            Execute.AddParameter("@From",item.From),
                            Execute.AddParameter("@SequenceNo",item.SequenceNo),
                            Execute.AddParameter("@To",item.To),
                            Execute.AddParameter("@GroupID",item.GroupID),
                            Execute.AddParameter("@MCID",item.MCID),
                            Execute.AddParameter("@AllocQty",item.AllocQty)
                        };
                        objExecute.Executes("spSavePlans", param, CommandType.StoredProcedure);                        
                    }                    
                    res = true;
                    if (res)
                    {
                        MessageBox.Show("Sucessfully Saved");
                        GetSavedPlans();
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void GetSavedPlans()
        {
            Execute objExecute = new Execute();

            List<Machine> lstDistinctMachine = new List<Machine>();            
            DataTable dtMachines = (DataTable)objExecute.Executes("spGetMachines", ReturnType.DataTable, CommandType.StoredProcedure);
            List<Machine> lstMachines = new List<Machine>();
            if (dtMachines != null && dtMachines.Rows.Count > 0)
            {
                foreach (DataRow drr in dtMachines.Rows)
                {
                    SqlParameter[] param = new SqlParameter[]
                        {
                            Execute.AddParameter("@intMCID",Convert.ToInt32(drr["intMCID"]))
                        };
                    DataTable dt = (DataTable)objExecute.Executes("spGetPlan", ReturnType.DataTable, param, CommandType.StoredProcedure);
                    List<Plan> lstPlan = new List<Plan>();
                    Plan objPlan;
                    Machine objMachine = null;

                    objMachine = new Machine();
                    objMachine.MCID = Convert.ToInt32(drr["intMCID"]);
                    objMachine.MCName = drr["vcMCCode"].ToString();
                    objMachine.Capacity = Convert.ToDecimal(drr["decCapacity"]);
                    lstDistinctMachine.Add(objMachine);
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
                    lstMachines.Add(objMachine);
                }
            }
            ucGraphicNew1.lstExistingMachines = lstDistinctMachine;
            ucGraphicNew1.lstMACHINES = lstMachines;
            ucGraphicNew1.Refresh();

            lstMachine = ucGraphicNew1.lstMACHINES;
        }

        private void btnAddNewPlan_Click(object sender, EventArgs e)
        {

            frmAddEditPlan objfrmAddEditPlan = new frmAddEditPlan(Convert.ToInt32(OperationType.Add));
            objfrmAddEditPlan.Show();
        }

    }
}
