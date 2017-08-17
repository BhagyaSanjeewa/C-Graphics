using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace TestGraphics
{
    public class CommonMethods
    {
        private  int MaxHours = 24;
        public List<Plan> ChangeSequence(List<Plan> lstPlans)
        {
            List<Plan> lstSeqChangedPlans = new List<Plan>();
            lstSeqChangedPlans = lstPlans;
            lstSeqChangedPlans = lstSeqChangedPlans.OrderBy(x => x.From).ToList();
            int changedCount = 0;            

            int[] distinctGroupIDs = lstSeqChangedPlans.Select(x => x.GroupID).Distinct().ToArray();

            List<Plan> tempPlans = new List<Plan>();
            List<Plan> lstFinalChangedPlans = new List<Plan>();
            foreach (int distinctGroupID in distinctGroupIDs)
            {
                changedCount = 0;
                tempPlans = (from lst in lstSeqChangedPlans
                             where lst.GroupID.Equals(distinctGroupID)
                             select lst).ToList();


                if (tempPlans != null && tempPlans.Count > 0)
                {
                    while (tempPlans.Count != changedCount)
                    {
                        Plan obj = new Plan();
                        tempPlans[changedCount].SequenceNo = changedCount + 1;
                        obj = tempPlans[changedCount];
                        lstFinalChangedPlans.Add(obj);
                        changedCount++;
                    }
                }
            }            
            return lstFinalChangedPlans;
        }

        public List<Plan> ChangeXYCordinations(List<Plan> lstPlans)
        {
            List<Plan> lstXYCordinationChangedPlans = new List<Plan>();
            lstXYCordinationChangedPlans = lstPlans;
            lstXYCordinationChangedPlans = lstXYCordinationChangedPlans.OrderBy(x => x.SequenceNo).ToList();
            int planCount = lstXYCordinationChangedPlans.Count;
            int changedCount = 0;

            while (planCount != changedCount)
            {
                foreach (var items in lstXYCordinationChangedPlans)
                {
                    if (lstXYCordinationChangedPlans[changedCount] != items)
                    {
                        if (((lstXYCordinationChangedPlans[changedCount].SequenceNo < items.SequenceNo) && (lstXYCordinationChangedPlans[changedCount].GroupID == items.GroupID)))
                        {
                            if (lstXYCordinationChangedPlans[changedCount].YCor > items.YCor)
                            {
                                int beforeChangeYCor = lstXYCordinationChangedPlans[changedCount].YCor;
                                lstXYCordinationChangedPlans[changedCount].YCor = items.YCor;
                                items.YCor = beforeChangeYCor;
                            }
                        }
                    }
                }
                changedCount++;
            }

            return lstXYCordinationChangedPlans;
        }
        public bool DrawRectangle(int x, int y, int height, int width)
        {
            bool drawed = false;

            return drawed;
        }

        public bool IsAvailableSlot(int fromHrs, List<Plan> lstCurrentPlans, int planHours, int currentXcor, int planwidth, List<Horizontal> lstEligibleHorizontals)
        {
            bool isCan = false;
            int avialableLocation = 0;
            List<Plan> tempLeftSidePlan = new List<Plan>();
            List<Plan> tempRightSidePlan = new List<Plan>();
            
            tempLeftSidePlan = (from lst in lstCurrentPlans
                        where lst.To <= fromHrs
                        select lst).ToList();

            tempRightSidePlan = (from lst in lstCurrentPlans
                                where lst.From >= fromHrs
                                select lst).ToList();

            int endXcor = currentXcor + planwidth;

            if (tempLeftSidePlan != null && tempLeftSidePlan.Count > 0)
            {
                tempLeftSidePlan = tempLeftSidePlan.OrderByDescending(x => x.SequenceNo).ToList();
                int latestToLocation = tempLeftSidePlan[0].To;
                int currentPlanHours = 0;
                if (tempRightSidePlan != null && tempRightSidePlan.Count > 0)
                {
                    tempRightSidePlan = tempRightSidePlan.OrderByDescending(x => x.SequenceNo).ToList();
                    int latestFromLocation = tempRightSidePlan[tempRightSidePlan.Count - 1].From;
                    int diffHours = latestFromLocation - latestToLocation;
                    if ((planHours <= diffHours) && (endXcor <= tempRightSidePlan[tempRightSidePlan.Count - 1].XCor))
                    {
                        isCan = true;
                        avialableLocation = latestToLocation;
                    }
                }
                else
                {
                    currentPlanHours = tempLeftSidePlan.Sum(x => x.To - x.From);
                    if (currentPlanHours < 12 && currentPlanHours + planHours <= 12)
                    {
                        isCan = true;
                    }
                }
            }
            else
            {
                if (tempRightSidePlan != null && tempRightSidePlan.Count > 0)
                {
                    tempRightSidePlan = tempRightSidePlan.OrderByDescending(x => x.SequenceNo).ToList();
                    int latestFromLocation = tempRightSidePlan[tempRightSidePlan.Count - 1].From;
                    int diffHours = latestFromLocation - lstEligibleHorizontals[0].Hrs;
                    if ((planHours <= diffHours) && (endXcor <= tempRightSidePlan[tempRightSidePlan.Count - 1].XCor))
                    {
                        isCan = true;                        
                    }
                }
                else
                {

                    if (planHours <= 12)
                    {
                        isCan = true;
                    }
                }
            }
            
            return isCan;
        }

        public List<Plan> DrawPlan(List<Machine> lstM,int MCID, int fromdt,int NoOfHours, decimal AllocQty)
        {
            List<Plan> lstPlan = new List<Plan>();
            try
            {
                Machine obljMachine = null;
                List<Machine> lstMachine = new List<Machine>();
                lstMachine = lstM;             
                int mcID = MCID;
                int from = fromdt;              
                int to = 0;
                int noOfHours = 0;               
                noOfHours = NoOfHours;
                decimal allocQty = AllocQty;
                to = from + noOfHours;
                Machine objMCN = new Machine();
                Plan objplan = new Plan();               

                objMCN.MCID = mcID;

                int currentGroupID = 0;
                currentGroupID = (fromdt /12) +1;

                if (lstMachine.FindAll(x => x.MCID == objMCN.MCID).Count > 0)
                {

                    List<Machine> lstMachines = (from lst in lstM
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
                                                where lst.GroupID.Equals(fromdt)
                                                select lst).ToList();

                       
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
                                        MaxHours = MaxHours * ((fromdt/12) + 1);
                                        if ((lstLocationNotChangedPlans[i].To != MaxHours) && ((currentFilledPlanHours + noOfHours) <= 12))
                                        {
                                            startLocation = lstLocationNotChangedPlans[i].To;
                                            sequence = lstLocationNotChangedPlans[i].SequenceNo;
                                            break;
                                        }
                                        else
                                        {
                                            throw new ApplicationException("Not Enoght Space for Plan this Quantity");
                                        }
                                    }
                                }
                            }
                            else
                            {
                                startLocation = from;
                                sequence = 0;
                            }
                            //Machine objM = lstMachine.Find(x => x.MCID == objMCN.MCID);                         
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
                            lstPlan.Add(objplan);
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
                }              
            }            
            catch (Exception ex)
            {

                throw ex;
            }

            return lstPlan;
        }


        //public int LastestHrsForNewPlan(List<Plan> lstSelectedPlan, int mcID, Rectangle tempRec)
        //{

        //    foreach (var item in lstSelectedPlan)
        //    {
        //        int MCID = mcID;
        //        item.XCor = tempRec.X;
        //        item.YCor = tempRec.Y;
        //        //item.IsSelected = false;
        //        Vertical objVertical = new Vertical();
        //        foreach (Vertical vertical in GloballstVertical)
        //        {
        //            if (vertical.MCID == MCID)
        //            {
        //                objVertical = vertical;
        //                break;
        //            }
        //        }

        //        Horizontal objHorizontal = null;
        //        List<Horizontal> lstEligibleHorizontals = new List<Horizontal>();
        //        foreach (Horizontal horizontal in GloballstHorizontal)
        //        {
        //            if (horizontal.X <= item.XCor && horizontal.Y <= item.YCor)
        //            {
        //                lstEligibleHorizontals.Add(horizontal);
        //            }
        //        }
        //        lstEligibleHorizontals = lstEligibleHorizontals.OrderByDescending(x => x.Hrs).ToList();
        //        int selectedAreaGroupID = lstEligibleHorizontals.Max(x => x.GroupID);

        //        lstEligibleHorizontals = (from lst in lstEligibleHorizontals
        //                                  where lst.GroupID.Equals(selectedAreaGroupID)
        //                                  select lst).ToList();
        //        if (lstEligibleHorizontals.Count > 0)
        //        {


        //            List<Plan> lstCurrentPlans = (from lst in MCN.LstPlan.ToList()
        //                                          where lst.IsSelected.Equals(false) && lst.GroupID.Equals(selectedAreaGroupID)
        //                                          select lst).ToList();

        //            foreach (Plan objplan in lstCurrentPlans)
        //            {
        //                foreach (Horizontal hr in lstEligibleHorizontals.ToList())
        //                {
        //                    if (hr.Hrs >= objplan.From && hr.Hrs < objplan.To)
        //                    {
        //                        lstEligibleHorizontals.Remove(hr);
        //                    }
        //                }

        //            }
        //            if (lstEligibleHorizontals != null && lstEligibleHorizontals.Count > 0)
        //            {
        //                CommonMethods objCommonMethods = new CommonMethods();
        //                if (lstCurrentPlans != null && lstCurrentPlans.Count > 0)
        //                {
        //                    int distance = item.To - item.From;
        //                    bool isCan = objCommonMethods.IsAvailableSlot(lstEligibleHorizontals[0].Hrs, lstCurrentPlans, distance, item.XCor, item.W, lstEligibleHorizontals);
        //                    if (isCan)
        //                    {
        //                        item.From = lstEligibleHorizontals[0].Hrs;
        //                        item.To = item.From + distance;
        //                        item.IsLocationChanged = true;
        //                        if ((item.To % 12) == 0)
        //                        {
        //                            item.GroupID = item.To / 12;
        //                        }
        //                        else
        //                        {
        //                            item.GroupID = (item.To / 12) + 1;
        //                        }
        //                        newGroupID = item.GroupID;
        //                    }
        //                }

        //                else
        //                {
        //                    int distance = item.To - item.From;
        //                    item.From = lstEligibleHorizontals[0].Hrs;
        //                    item.To = item.From + distance;
        //                    item.IsLocationChanged = true;
        //                    if ((item.To % 12) == 0)
        //                    {
        //                        item.GroupID = item.To / 12;
        //                    }
        //                    else
        //                    {
        //                        item.GroupID = (item.To / 12) + 1;
        //                    }

        //                    newGroupID = item.GroupID;
        //                }
                        
        //            }
        //        }
        //    }
        //    return 1;
        //}


        public List<Plan> DrawPlanByDate(List<Machine> lstM, int MCID, DateTime fromdt, int NoOfHours, decimal AllocQty)
        {
            List<Plan> lstPlan = new List<Plan>();
            try
            {
                int currentTotalPlanHours = 0;
                List<Machine> lstMachine = new List<Machine>();
                lstMachine = lstM;
                Machine objMCN = new Machine();
                objMCN.MCID = MCID;
                Plan objplan = new Plan();
                if (lstMachine.FindAll(x => x.MCID == objMCN.MCID).Count > 0)
                {
                    List<Machine> lstSelectedMC = new List<Machine>();
                    lstSelectedMC = lstMachine.FindAll(x => x.MCID == objMCN.MCID);

                    if (lstSelectedMC != null && lstSelectedMC.Count > 0)
                    {
                        if (lstSelectedMC[0].LstPlan != null && lstSelectedMC[0].LstPlan.Count > 0)
                        {
                            List<Plan> lstCurrentDatePlans = new List<Plan>();
                            lstCurrentDatePlans = (from lst in lstSelectedMC[0].LstPlan
                                                   where lst.FromDate.Date.Equals(fromdt.Date)
                                                   select lst).ToList();

                            if (lstCurrentDatePlans != null && lstCurrentDatePlans.Count > 0)
                            {
                                currentTotalPlanHours = lstCurrentDatePlans.Sum(x => (x.To - x.From));
                                if (currentTotalPlanHours <= MaxHours)
                                {
                                    int startLocation = 0;
                                    int sequence = 0;
                                    #region Rechange the sequence

                                    lstCurrentDatePlans = this.ChangeSequenceByDate(lstCurrentDatePlans);
                                    lstCurrentDatePlans = this.ChangeXYCordinationsByDate(lstCurrentDatePlans);
                                    #endregion
                                    lstCurrentDatePlans = lstCurrentDatePlans.OrderBy(x => x.SequenceNo).ToList();
                                    if (lstCurrentDatePlans.Count > 0)
                                    {
                                        for (int i = 0; i < lstCurrentDatePlans.Count; i++)
                                        {
                                            if (lstCurrentDatePlans.Count == 1)
                                            {
                                                int availableHours = lstCurrentDatePlans[i].To - MaxHours;
                                                if ((lstCurrentDatePlans[i].From - NoOfHours) >= NoOfHours)
                                                {
                                                    startLocation = 0;
                                                    sequence = 0;
                                                    break;
                                                }
                                                else if ((MaxHours - lstCurrentDatePlans[i].To) >= NoOfHours)
                                                {
                                                    startLocation = lstCurrentDatePlans[i].To;
                                                    sequence = 0;
                                                    break;
                                                }                                    
                                                else
                                                {
                                                    throw new ApplicationException("Not Enoght Space for Plan this Quantity");
                                                }
                                            }
                                            if ((lstCurrentDatePlans.Count - 1) > i)
                                            {
                                                int availableHours = lstCurrentDatePlans[i + 1].From - lstCurrentDatePlans[i].To;
                                                if (availableHours >= NoOfHours)
                                                {
                                                    startLocation = lstCurrentDatePlans[i].To;
                                                    sequence = lstCurrentDatePlans[i].SequenceNo;
                                                    break;
                                                }
                                            }
                                            else
                                            {
                                                if ((lstCurrentDatePlans[i].To != MaxHours) && ((currentTotalPlanHours + NoOfHours) <= MaxHours))
                                                {
                                                    startLocation = lstCurrentDatePlans[i].To;
                                                    sequence = lstCurrentDatePlans[i].SequenceNo;
                                                    break;
                                                }
                                                else
                                                {
                                                    throw new ApplicationException("Not Enoght Space for Plan this Quantity");
                                                }
                                                
                                            }
                                        }
                                    }
                                    else
                                    {
                                        startLocation = 0;
                                        sequence = 0;
                                    }                                                      
                                    objplan.From = startLocation;
                                    objplan.To = objplan.From + NoOfHours;
                                    objplan.XCor = 0;
                                    objplan.AllocQty = AllocQty;
                                    objplan.FromDate = fromdt;
                                    objplan.SequenceNo = sequence + 1;
                                    objplan.IsAlreadyAddedPlan = false;
                                    objplan.MCID = MCID;
                                    if ((objplan.To % 12) == 0)
                                    {
                                        objplan.GroupID = objplan.To / 12;
                                    }
                                    else
                                    {
                                        objplan.GroupID = (objplan.To / 12) + 1;
                                    }
                                    lstPlan.Add(objplan);
                                }                            

                            }
                            else
                            {
                                objplan.From = 0;
                                objplan.To = objplan.From + NoOfHours;
                                objplan.FromDate = fromdt;
                                objplan.SequenceNo = 1;
                                objplan.AllocQty = AllocQty;
                                objplan.MCID = MCID;
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
                            }
                        }
                        else
                        {
                            objplan.From = 0;
                            objplan.To = objplan.From + NoOfHours;
                            objplan.FromDate = fromdt;
                            objplan.SequenceNo = 1;
                            objplan.AllocQty = AllocQty;
                            objplan.MCID = MCID;
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
                        }
                    }
                }
                
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return lstPlan;
        }

        public List<Plan> ChangeSequenceByDate(List<Plan> lstPlans)
        {
            List<Plan> lstSeqChangedPlans = new List<Plan>();
            lstSeqChangedPlans = lstPlans;
            lstSeqChangedPlans = lstSeqChangedPlans.OrderBy(x => x.From).ToList();
            int changedCount = 0;

            int[] distinctGroupIDs = lstSeqChangedPlans.Select(x => x.GroupID).Distinct().ToArray();
            DateTime[] distinctDates = lstSeqChangedPlans.Select(x => x.FromDate.Date).Distinct().ToArray();
            List<Plan> tempPlans = new List<Plan>();
            List<Plan> lstFinalChangedPlans = new List<Plan>();
            foreach (DateTime distinctdates in distinctDates)
            {
                changedCount = 0;
                tempPlans = (from lst in lstSeqChangedPlans
                             where lst.FromDate.Date.Equals(distinctdates.Date)
                             select lst).ToList();


                if (tempPlans != null && tempPlans.Count > 0)
                {
                    while (tempPlans.Count != changedCount)
                    {
                        Plan obj = new Plan();
                        tempPlans[changedCount].SequenceNo = changedCount + 1;
                        obj = tempPlans[changedCount];
                        lstFinalChangedPlans.Add(obj);
                        changedCount++;
                    }
                }
            }
            return lstFinalChangedPlans;
        }

        public List<Plan> ChangeXYCordinationsByDate(List<Plan> lstPlans)
        {
            List<Plan> lstXYCordinationChangedPlans = new List<Plan>();
            lstXYCordinationChangedPlans = lstPlans;
            lstXYCordinationChangedPlans = lstXYCordinationChangedPlans.OrderBy(x => x.SequenceNo).ToList();
            int planCount = lstXYCordinationChangedPlans.Count;
            int changedCount = 0;

            while (planCount != changedCount)
            {
                foreach (var items in lstXYCordinationChangedPlans)
                {
                    if (lstXYCordinationChangedPlans[changedCount] != items)
                    {
                        if (((lstXYCordinationChangedPlans[changedCount].SequenceNo < items.SequenceNo) && (lstXYCordinationChangedPlans[changedCount].FromDate == items.FromDate)))
                        {
                            if (lstXYCordinationChangedPlans[changedCount].YCor > items.YCor)
                            {
                                int beforeChangeYCor = lstXYCordinationChangedPlans[changedCount].YCor;
                                lstXYCordinationChangedPlans[changedCount].YCor = items.YCor;
                                items.YCor = beforeChangeYCor;
                            }
                        }
                    }
                }
                changedCount++;
            }

            return lstXYCordinationChangedPlans;
        }

        public bool IsAvailableSlotByDate(int fromHrs, List<Plan> lstCurrentPlans, int planHours, int currentXcor, int planwidth, List<Horizontal> lstEligibleHorizontals)
        {
            bool isCan = false;
            int avialableLocation = 0;
            List<Plan> tempLeftSidePlan = new List<Plan>();
            List<Plan> tempRightSidePlan = new List<Plan>();

            tempLeftSidePlan = (from lst in lstCurrentPlans
                                where lst.To <= fromHrs
                                select lst).ToList();

            tempRightSidePlan = (from lst in lstCurrentPlans
                                 where lst.From >= fromHrs
                                 select lst).ToList();

            int endXcor = currentXcor + planwidth;

            if (tempLeftSidePlan != null && tempLeftSidePlan.Count > 0)
            {
                tempLeftSidePlan = tempLeftSidePlan.OrderByDescending(x => x.SequenceNo).ToList();
                int latestToLocation = tempLeftSidePlan[0].To;
                int currentPlanHours = 0;
                if (tempRightSidePlan != null && tempRightSidePlan.Count > 0)
                {
                    tempRightSidePlan = tempRightSidePlan.OrderByDescending(x => x.SequenceNo).ToList();
                    int latestFromLocation = tempRightSidePlan[tempRightSidePlan.Count - 1].From;
                    int diffHours = latestFromLocation - latestToLocation;
                    if ((planHours <= diffHours) && (endXcor <= tempRightSidePlan[tempRightSidePlan.Count - 1].XCor))
                    {
                        isCan = true;
                        avialableLocation = latestToLocation;
                    }
                }
                else
                {
                    currentPlanHours = tempLeftSidePlan.Sum(x => x.To - x.From);
                    if (currentPlanHours <= MaxHours && currentPlanHours + planHours <= MaxHours)
                    {
                        isCan = true;
                    }
                }
            }
            else
            {
                if (tempRightSidePlan != null && tempRightSidePlan.Count > 0)
                {
                    tempRightSidePlan = tempRightSidePlan.OrderByDescending(x => x.SequenceNo).ToList();
                    int latestFromLocation = tempRightSidePlan[tempRightSidePlan.Count - 1].From;
                    int diffHours = latestFromLocation - lstEligibleHorizontals[0].Hrs;
                    if ((planHours <= diffHours) && (endXcor <= tempRightSidePlan[tempRightSidePlan.Count - 1].XCor))
                    {
                        isCan = true;
                    }
                }
                else
                {

                    if (planHours <= MaxHours)
                    {
                        isCan = true;
                    }
                }
            }

            return isCan;
        }

        public int[] GetStartXCordination()
        {
            int[] returnAry = new Int32[2];

            return returnAry;
        }
    }
}
