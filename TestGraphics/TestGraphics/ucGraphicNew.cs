using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace TestGraphics
{
    public partial class ucGraphicNew : UserControl
    {
        public string mc = "";
        public int from = 0;
        public int to = 0;
        public int ls = 0;
        public int mh = 0;
        int index = 0;
        private const int MaxCount = 10;
        private const int MaxHours = 100;
        List<Plan> lstGlobalPlans = new List<Plan>();
        

        public List<Machine> lstMACHINES = new List<Machine>();
        public List<Machine> lstExistingMachines = new List<Machine>();
        Size ScrollOffset = new Size(0, 0);
        Point scrollStartPoint, currentPoint;
        bool isScrolling = false;
        bool IsScol = false;

        bool isDragable = false;
        int tx, ty;
        Rectangle tempRec;

        List<Horizontal> GloballstHorizontal = null;
        List<Vertical> GloballstVertical = null;

        int selectedMachineBarMachineID = 0;      
        private int taskBarWidth = 35;

        public ucGraphicNew()
        {
            InitializeComponent();
            lblDescription.Visible = false;
        }
        private void ucGraphicNew_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                #region Common Variables
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                System.Drawing.Graphics formGraphics;
                formGraphics = this.CreateGraphics();

                Color[] Lines = { Color.Black, Color.Yellow };
                Pen p = new Pen(Lines[0], 2);
                Pen pn1 = new Pen(Lines[1], 2);
                Pen pn2 = new Pen(Color.Orange, 2);
                Pen pn3 = new Pen(Color.Red, 2);

                SolidBrush brblue = new SolidBrush(Color.Blue);

                Vertical objVertical;
                List<Vertical> lstVertical = new List<Vertical>();

                Horizontal objHorizontal;
                List<Horizontal> lstHorizontal = new List<Horizontal>();

                GloballstHorizontal = new List<Horizontal>();
                GloballstVertical = new List<Vertical>();

                int sx, ex, sy, ey = 0;
                int msx, mex, msy, mey = 0;
                int drwingBeginx = ex = msx = 100;
                int drwingBeginy = ey = msy = 100;
                int lineheight = 70;
                int lineSpace = 100 + ls;
                int mcWidth = 70;
                int mcHeight = 50 + mh;
                int mcSpace = mcHeight + 5;
                int mcDeviceCount = 0;
                int mcDevideY = 0;
                sx = ex;
                sy = ey - lineheight;
                msx = msx - mcWidth;
                int GroupID = 0;

                #endregion

                #region Background of form
                Brush background;
                background = new LinearGradientBrush(new Rectangle(0, 0, this.Width, this.Height), Color.DimGray, Color.Gray, 45);
                e.Graphics.FillRectangle(background, new Rectangle(0, 0, this.Width, this.Height));
                #endregion

                #region Draw Planinig Area
                e.Graphics.DrawLine(pn1, new Point(msx, 100), new Point(this.Width, 100));
                //e.Graphics.DrawLine(pn1, new Point(100, 0), new Point(100, this.Height));

                for (int i = 0; i < lstExistingMachines.Count; i++)
                {
                    objVertical = new Vertical();
                    objVertical.MCID = lstExistingMachines[i].MCID;
                    objVertical.MCCode = lstExistingMachines[i].MCName;
                    objVertical.Capacity = lstExistingMachines[i].Capacity;

                    if (ScrollOffset.Width < 0)
                    {
                        objVertical.X = msx + mcWidth;
                    }
                    else
                    {
                        objVertical.X = msx + mcWidth + ScrollOffset.Width;
                    }
                    objVertical.Y = msy + ScrollOffset.Height;

                    lstVertical.Add(objVertical);

                    Rectangle rc;
                    if (ScrollOffset.Width < 0 || ScrollOffset.Height < 0)
                    {
                        if (msy + ScrollOffset.Height >= 100)
                        {
                            rc = new Rectangle(msx, msy + ScrollOffset.Height, mcWidth, mcHeight);
                            LinearGradientBrush l = new LinearGradientBrush(rc, Color.Red, Color.DarkRed, 1, true);
                            e.Graphics.FillRectangle(l, rc);
                            e.Graphics.DrawString(objVertical.MCCode.ToString(), new Font("Tahoma", 10), brblue, new Point(msx, msy + ScrollOffset.Height));
                            //e.Graphics.DrawLine(pn1, new Point(msx, msy), new Point(mcWidth, 100));
                            if (i % 6 == 0)
                            {
                                e.Graphics.DrawLine(pn3, new Point(sx, sy - 20), new Point(sx, this.Height));
                            }
                            else
                            {
                                e.Graphics.DrawLine(pn2, new Point(sx, sy), new Point(sx, this.Height));
                            }
                        }
                    }
                    else
                    {
                        rc = new Rectangle(msx + ScrollOffset.Width, msy + ScrollOffset.Height, mcWidth, mcHeight);
                        LinearGradientBrush l = new LinearGradientBrush(rc, Color.Red, Color.DarkRed, 1, true);
                        e.Graphics.FillRectangle(l, rc);
                        e.Graphics.DrawString(objVertical.MCCode.ToString(), new Font("Tahoma", 10), brblue, new Point(msx + ScrollOffset.Width, msy + ScrollOffset.Height));
                        //e.Graphics.DrawLine(pn1, new Point(msx + ScrollOffset.Width, msy + ScrollOffset.Height), new Point(mcWidth, 100));
                        if (i % 6 == 0)
                        {
                            e.Graphics.DrawLine(pn3, new Point(sx, sy - 20), new Point(sx, this.Height));
                        }
                        else
                        {
                            e.Graphics.DrawLine(pn2, new Point(sx, sy), new Point(sx, this.Height));
                        }
                    }

                    msy = msy + mcSpace;
                    objHorizontal = new Horizontal();
                    objHorizontal.Hrs = i;
                    if (i <= 12)
                    {
                        objHorizontal.GroupID = 1;
                    }
                    else
                    {
                        objHorizontal.GroupID = (i / 12) + 1;
                    }

                    if (ScrollOffset.Height < 0)
                    {
                        objHorizontal.Y = ey;
                    }
                    else
                    {
                        objHorizontal.Y = ey + ScrollOffset.Height;
                    }
                    objHorizontal.X = ex + ScrollOffset.Width;

                    lstHorizontal.Add(objHorizontal);

                    if (ScrollOffset.Width < 0 || ScrollOffset.Height < 0)
                    {
                        if (sx + ScrollOffset.Width >= 100)
                        {
                            if (!(i % 6 == 0))
                            {
                                e.Graphics.DrawLine(p, new Point(sx + ScrollOffset.Width, sy), new Point(ex + ScrollOffset.Width, ey));
                                e.Graphics.DrawString(i.ToString(), new Font("Tahoma", 10), brblue, new Point(sx - 8 + ScrollOffset.Width, sy - 15));
                            }
                            else
                            {
                                e.Graphics.DrawString(i.ToString(), new Font("Tahoma", 10), brblue, new Point(sx - 8 + ScrollOffset.Width, sy - 30));
                            }
                            e.Graphics.DrawLine(pn1, new Point(msx, msy), new Point(this.Width, msy));
                        }

                    }
                    else
                    {
                        if (!(i % 6 == 0))
                        {
                            e.Graphics.DrawLine(p, new Point(sx + ScrollOffset.Width, sy + ScrollOffset.Height), new Point(ex + ScrollOffset.Width, ey + ScrollOffset.Height));
                            e.Graphics.DrawString(i.ToString(), new Font("Tahoma", 10), brblue, new Point(sx - 8 + ScrollOffset.Width, sy - 15 + ScrollOffset.Height));
                        }
                        else
                        {
                            e.Graphics.DrawString(i.ToString(), new Font("Tahoma", 10), brblue, new Point(sx - 8 + ScrollOffset.Width, sy - 30));
                        }
                        e.Graphics.DrawLine(pn1, new Point(msx, msy), new Point(this.Width, msy));

                    }

                    sx = sx + lineSpace;
                    ex = ex + lineSpace;
                }
                #endregion

                #region Draw More Than One Plans
                Plan objPln;
                List<Plan> lstp;
                if (!isDragable || isScrolling)
                {
                    foreach (Machine MCN in lstMACHINES)
                    {
                        if (MCN.LstPlan != null && MCN.LstPlan.Count > 0)
                        {
                            foreach (var item in MCN.LstPlan.ToList())
                            {
                                #region Checking Max Hours
                                int upToNowHours = 0;
                                foreach (var items in MCN.LstPlan)
                                {
                                    upToNowHours = upToNowHours + (items.To - items.From);
                                }
                                if ((upToNowHours > MaxHours))
                                {
                                    MessageBox.Show("You Cannot Exceed Maximum Hours");
                                    MCN.LstPlan.RemoveAt(MCN.LstPlan.Count - 1);
                                }
                                #endregion
                            }

                            lstp = new List<Plan>();
                            if (MCN.LstPlan.Count > 1)
                            {
                                index++;
                                mcDevideY = 0; mcDeviceCount = 0;

                                int previousGroupID = 0;
                                int currentGroupID = 0;
                                //mcDeviceCount = mcHeight / Convert.ToInt32(MCN.LstPlan.Count);
                                int plancount = 0;
                                foreach (Plan Pn in MCN.LstPlan.ToList())
                                {
                                    List<Plan> lstPlan = new List<Plan>();
                                    #region Getting the previous plan End Position And Draw the Next Plan
                                    plancount++;

                                    var xxxxx = (MCN.LstPlan[MCN.LstPlan.Count - 1]);
                                    Pn.From = Pn.From;

                                    Vertical v = new Vertical();
                                    v.MCID = MCN.MCID;
                                    Vertical vv = lstVertical.Find(x => x.MCID == v.MCID);

                                    Horizontal hr = new Horizontal();
                                    hr.Hrs = Pn.From;
                                    Horizontal hrr = lstHorizontal.Find(x => x.Hrs == hr.Hrs);

                                    Horizontal too = new Horizontal();
                                    too.Hrs = Pn.To;
                                    Horizontal tooo = lstHorizontal.Find(x => x.Hrs == too.Hrs);

                                    int width = tooo.X - hrr.X;

                                    objPln = new Plan();
                                    objPln.From = Pn.From;
                                    objPln.To = Pn.To;
                                    if ((too.Hrs % 12) == 0)
                                    {
                                        objPln.GroupID = too.Hrs / 12;
                                    }
                                    else
                                    {
                                        objPln.GroupID = (too.Hrs / 12) + 1;
                                    }
                                    objPln.XCor = hrr.X;

                                    previousGroupID = MCN.LstPlan[MCN.LstPlan.Count - 2].GroupID;
                                    currentGroupID = objPln.GroupID;

                                    int currentPlanCount = MCN.LstPlan.Where(s => s.GroupID == currentGroupID).Count();
                                    if (currentPlanCount == 1)
                                    {
                                        mcDevideY = 0;
                                    }
                                    mcDeviceCount = mcHeight / currentPlanCount;

                                    List<Plan> tempCurrent = new List<Plan>();
                                    tempCurrent = (from lst in lstp
                                                   where lst.GroupID.Equals(currentGroupID)
                                                   select lst).ToList();

                                    mcDevideY = mcDeviceCount * tempCurrent.Count;

                                    objPln.YCor = vv.Y + mcDevideY;
                                    objPln.SequenceNo = Pn.SequenceNo;
                                    objPln.IsLocationChanged = Pn.IsLocationChanged;
                                    objPln.W = width;
                                    objPln.H = mcDeviceCount;
                                    objPln.PlanID = Pn.PlanID;
                                    objPln.AllocQty = Pn.AllocQty;
                                    objPln.IsAlreadyAddedPlan = Pn.IsAlreadyAddedPlan;
                                    lstp.Add(objPln);
                                    lstGlobalPlans.Add(objPln);

                                    #endregion

                                    #region Old Method
                                    //Vertical v = new Vertical();
                                    //v.MCID = MCN.MCID;
                                    //Vertical vv = lstVertical.Find(x => x.MCID == v.MCID);

                                    //Horizontal hr = new Horizontal();
                                    //hr.Hrs = Pn.From;
                                    //Horizontal hrr = lstHorizontal.Find(x => x.Hrs == hr.Hrs);

                                    //Horizontal too = new Horizontal();
                                    //too.Hrs = Pn.To;
                                    //Horizontal tooo = lstHorizontal.Find(x => x.Hrs == too.Hrs);

                                    //int width = tooo.X - hrr.X;

                                    //objPln = new Plan();
                                    //objPln.From = Pn.From;
                                    //objPln.To = Pn.To;

                                    //objPln.XCor = hrr.X;
                                    //objPln.YCor = vv.Y + mcDevideY;


                                    //objPln.W = width;
                                    //objPln.H = mcDeviceCount;

                                    //lstp.Add(objPln);
                                    //lstGlobalPlans.Add(objPln);
                                    //if ((hrr.X >= 100) && vv.Y >= 100)
                                    //{
                                    //    Rectangle rc = new Rectangle(hrr.X, vv.Y + mcDevideY, width, mcDeviceCount);
                                    //    LinearGradientBrush l = new LinearGradientBrush(rc, Color.Blue, Color.White, 1, true);
                                    //    e.Graphics.FillRectangle(l, rc);
                                    //}

                                    //mcDevideY = mcDevideY + mcDeviceCount; 
                                    #endregion
                                }

                                CommonMethods objCommonMethods = new CommonMethods();
                                lstp = objCommonMethods.ChangeSequence(lstp);
                                lstp = objCommonMethods.ChangeXYCordinations(lstp);
                                foreach (var item in lstp)
                                {
                                    if (item.IsAlreadyAddedPlan)
                                    {
                                        Rectangle rc = new Rectangle(item.XCor, item.YCor, item.W, item.H);
                                        LinearGradientBrush l = new LinearGradientBrush(rc, Color.Blue, Color.White, 1, true);
                                        e.Graphics.FillRectangle(l, rc);
                                    }
                                    else
                                    {
                                        Rectangle rc = new Rectangle(item.XCor, item.YCor, item.W, item.H);
                                        LinearGradientBrush l = new LinearGradientBrush(rc, Color.Purple, Color.White, 1, true);
                                        e.Graphics.FillRectangle(l, rc);
                                    }
                                }
                            }
                            else
                            {
                                foreach (Plan Pn in MCN.LstPlan)
                                {
                                    Vertical v = new Vertical();
                                    v.MCID = MCN.MCID;
                                    Vertical vv = lstVertical.Find(x => x.MCID == v.MCID);

                                    Horizontal hr = new Horizontal();
                                    hr.Hrs = Pn.From;
                                    Horizontal hrr = lstHorizontal.Find(x => x.Hrs == hr.Hrs);

                                    Horizontal too = new Horizontal();
                                    too.Hrs = Pn.To;
                                    Horizontal tooo = lstHorizontal.Find(x => x.Hrs == too.Hrs);

                                    int width = tooo.X - hrr.X;

                                    objPln = new Plan();
                                    objPln.From = Pn.From;
                                    objPln.To = Pn.To;
                                    objPln.XCor = hrr.X;
                                    objPln.YCor = vv.Y;
                                    objPln.PlanID = Pn.PlanID;
                                    objPln.W = width;
                                    objPln.H = mcHeight;
                                    objPln.SequenceNo = Pn.SequenceNo;
                                    objPln.AllocQty = Pn.AllocQty;
                                    objPln.IsLocationChanged = Pn.IsLocationChanged;
                                    objPln.IsAlreadyAddedPlan = Pn.IsAlreadyAddedPlan;
                                    if ((too.Hrs % 12) == 0)
                                    {
                                        objPln.GroupID = too.Hrs / 12;
                                    }
                                    else
                                    {
                                        objPln.GroupID = (too.Hrs / 12) + 1;
                                    }
                                    lstp.Add(objPln);
                                    lstGlobalPlans.Add(objPln);
                                  
                                    foreach (var item in lstp)
                                    {
                                        if (item.IsAlreadyAddedPlan)
                                        {
                                            Rectangle rc = new Rectangle(item.XCor, item.YCor, item.W, item.H);
                                            LinearGradientBrush l = new LinearGradientBrush(rc, Color.Blue, Color.White, 1, true);
                                            e.Graphics.FillRectangle(l, rc);
                                        }
                                        else
                                        {
                                            Rectangle rc = new Rectangle(item.XCor, item.YCor, item.W, item.H);
                                            LinearGradientBrush l = new LinearGradientBrush(rc, Color.Purple, Color.White, 1, true);
                                            e.Graphics.FillRectangle(l, rc);
                                        }
                                    }
                                }
                            }
                            MCN.LstPlan.RemoveAll(x => x.XCor == 0);
                            MCN.LstPlan = lstp;
                        }
                    }
                }
                else
                {
                    foreach (Machine MCN in lstMACHINES)
                    {
                        if (MCN.LstPlan != null && MCN.LstPlan.Count > 0)
                        {
                            lstp = new List<Plan>();
                            index++;
                            mcDevideY = 0; mcDeviceCount = 0;
                            List<Plan> lsttt = new List<Plan>();
                            lsttt = (from lst in MCN.LstPlan
                                     where lst.IsSelected.Equals(false)
                                     select lst).ToList();
                            foreach (Plan Pn in lsttt.ToList())
                            {
                                List<Plan> lstPlan = new List<Plan>();

                                var xxxxx = (MCN.LstPlan[MCN.LstPlan.Count - 1]);
                                Pn.From = Pn.From;
                                Vertical v = new Vertical();
                                v.MCID = MCN.MCID;
                                Vertical vv = lstVertical.Find(x => x.MCID == v.MCID);

                                Horizontal hr = new Horizontal();
                                hr.Hrs = Pn.From;
                                Horizontal hrr = lstHorizontal.Find(x => x.Hrs == hr.Hrs);

                                Horizontal too = new Horizontal();
                                too.Hrs = Pn.To;
                                Horizontal tooo = lstHorizontal.Find(x => x.Hrs == too.Hrs);

                                int width = tooo.X - hrr.X;

                                objPln = new Plan();
                                objPln.From = Pn.From;
                                objPln.To = Pn.To;
                                objPln.GroupID = Pn.GroupID;
                                objPln.XCor = Pn.XCor;
                                objPln.PlanID = Pn.PlanID;
                                objPln.AllocQty = Pn.AllocQty;
                                objPln.YCor = Pn.YCor;
                                objPln.SequenceNo = Pn.SequenceNo;
                                objPln.IsLocationChanged = Pn.IsLocationChanged;
                                objPln.IsAlreadyAddedPlan = Pn.IsAlreadyAddedPlan;
                                objPln.W = Pn.W;
                                objPln.H = Pn.H;
                                lstp.Add(objPln);
                                lstGlobalPlans.Add(objPln);                               

                                foreach (var item in lstp)
                                {
                                    if ((item.XCor >= 100) && item.YCor >= 100)
                                    {
                                        if (item.IsAlreadyAddedPlan)
                                        {
                                            Rectangle rc = new Rectangle(item.XCor, item.YCor, item.W, item.H);
                                            LinearGradientBrush l = new LinearGradientBrush(rc, Color.Blue, Color.White, 1, true);
                                            e.Graphics.FillRectangle(l, rc);
                                        }
                                        else
                                        {
                                            Rectangle rc = new Rectangle(item.XCor, item.YCor, item.W, item.H);
                                            LinearGradientBrush l = new LinearGradientBrush(rc, Color.Purple, Color.White, 1, true);
                                            e.Graphics.FillRectangle(l, rc);
                                        }
                                    }
                                }
                            }
                            Plan objTempplan = new Plan();

                            int selectedMachinePlans = (from lst in MCN.LstPlan
                                                        where lst.IsSelected.Equals(true)
                                                        select lst).Count();

                            if (selectedMachinePlans > 0)
                            {
                                var temp = (Plan)(from lst in MCN.LstPlan
                                                  where lst.IsSelected.Equals(true)
                                                  select lst).Single();
                                lstp.Add((Plan)temp);

                                if (temp.IsAlreadyAddedPlan)
                                {
                                    LinearGradientBrush ll = new LinearGradientBrush(tempRec, Color.Blue, Color.White, 1, true);
                                    e.Graphics.FillRectangle(ll, tempRec);
                                }
                                else
                                {
                                    LinearGradientBrush ll = new LinearGradientBrush(tempRec, Color.Purple, Color.White, 1, true);
                                    e.Graphics.FillRectangle(ll, tempRec);
                                }
                                this.Refresh();
                            }
                            MCN.LstPlan.RemoveAll(x => x.XCor == 0);
                            MCN.LstPlan = lstp;
                        }
                    }
                }
                GloballstHorizontal = lstHorizontal;
                GloballstVertical = lstVertical;
                #endregion
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

        private void ucGraphicNew_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                ContextMenu mnuContext;
                MenuItem mnuEditPlan;
                MenuItem mnuCombineSameColorJobs;                          
                //startXcoodinate = machineShowingWidth + 11;

                #region Left Click Events
                if (e.Button == MouseButtons.Left)
                {
                    foreach (Machine MCN in lstMACHINES)
                    {
                        if (MCN.LstPlan != null && MCN.LstPlan.Count > 0)
                        {
                            foreach (var item in MCN.LstPlan.ToList())
                            {
                                if (((item.XCor + item.W) > e.X) && (e.X > item.XCor) && ((item.YCor + item.H) > e.Y) && (item.YCor < e.Y))
                                {
                                    tempRec = new Rectangle(item.XCor, item.YCor, item.W, item.H);
                                    tx = e.X - tempRec.X;
                                    ty = e.Y - tempRec.Y;
                                    item.IsSelected = true;
                                    isDragable = true;
                                    selectedMachineBarMachineID = MCN.MCID;
                                    return;
                                }
                            }
                        }
                    }
                } 
                #endregion

                #region Right Click Events
                else
                {
                    if (GloballstVertical != null && GloballstVertical.Count > 0)
                    {
                        foreach (Vertical vertical in GloballstVertical)
                        {
                            List<Machine>  lstSelectedMachine = new List<Machine>();
                            lstSelectedMachine = (from lst in lstMACHINES
                                                  where lst.MCID.Equals(vertical.MCID)
                                                  select lst).ToList();
                            if (lstSelectedMachine != null && lstSelectedMachine.Count > 0)
                            {
                                if (lstSelectedMachine[0].LstPlan != null && lstSelectedMachine[0].LstPlan.Count > 0)
                                {
                                    for (int i = 0; i < lstSelectedMachine[0].LstPlan.Count; i++)
                                    {
                                        if (((e.X >= lstSelectedMachine[0].LstPlan[i].XCor) && (e.X <= lstSelectedMachine[0].LstPlan[i].XCor + lstSelectedMachine[0].LstPlan[i].W)) && ((e.Y >= lstSelectedMachine[0].LstPlan[i].YCor) && (e.Y <= lstSelectedMachine[0].LstPlan[i].YCor + lstSelectedMachine[0].LstPlan[i].H)))
                                        {
                                            mnuContext = new ContextMenu();
                                            mnuEditPlan = new MenuItem();
                                            mnuEditPlan.Text = "Edit Plan";
                                            mnuContext.MenuItems.Add(mnuEditPlan);
                                            mnuEditPlan.Click += new System.EventHandler(this.mnuEditPlan_Click);

                                            this.ContextMenu = mnuContext;
                                            this.ContextMenu.Show(this, new Point(e.X, e.Y));
                                            ContextMenu = null;
                                            return;
                                        }
                                    }
                                }
                            }
                        }
                    }                    
                } 
                #endregion
                //isScrolling = true;
                //scrollStartPoint = new Point(e.X - ScrollOffset.Width, e.Y - ScrollOffset.Height);
                //Cursor = Cursors.Hand;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void ucGraphicNew_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                List<Machine> lstSelectedMachine;                
                #region Moving Plan
                if (isDragable)
                {
                    if (selectedMachineBarMachineID != 0)
                    {
                        this.Cursor = Cursors.SizeAll;
                        this.Cursor = Cursors.SizeAll;
                        if ((e.X - tx) >= tempRec.X + ScrollOffset.Width)
                        {
                            tempRec.X = e.X - tx;
                        }
                        else
                        {
                            tempRec.X = e.X - tx;
                        }
                        this.Refresh();

                    }
                }
                else if (isScrolling)
                {
                    if (ScrollOffset.Width <= 0 && ScrollOffset.Height <= 0)
                    {
                        currentPoint = new Point(e.X, e.Y);
                        ScrollOffset.Width = (currentPoint.X - scrollStartPoint.X);
                        ScrollOffset.Height = (currentPoint.Y - scrollStartPoint.Y);
                        if (ScrollOffset.Width > 0)
                        {
                            ScrollOffset.Width = 0;
                        }

                        if (ScrollOffset.Height > 0)
                        {
                            ScrollOffset.Height = 0;
                        }
                        this.Refresh();
                    }
                }
                #endregion
                else
                {
                    #region Showing Description
                    string description = "";
                    if (GloballstVertical != null && GloballstVertical.Count > 0)
                    {
                        foreach (Vertical vertical in GloballstVertical)
                        {
                            #region Showing Machine Details
                            if (((e.X >= 30) && (e.X <= 70)) && ((e.Y >= vertical.Y) && (e.Y <= vertical.Y + 50)))
                            {
                                description = "MC-Code : " + vertical.MCCode;
                                lblDescription.Text = description;
                                lblDescription.BackColor = Color.BurlyWood;
                                if (e.Y + lblDescription.Height > (this.Height - taskBarWidth))
                                {
                                    if (e.X + lblDescription.Width > this.Width)
                                    {
                                        lblDescription.Location = new Point(e.X, e.Y);
                                    }
                                    else
                                    {
                                        lblDescription.Location = new Point(e.X, e.Y);
                                    }
                                }
                                else
                                {
                                    if (e.X + lblDescription.Width > this.Width)
                                    {
                                        lblDescription.Location = new Point(this.Width - lblDescription.Width, e.Y + 10);
                                    }
                                    else
                                    {
                                        lblDescription.Location = new Point(e.X + 10, e.Y + 10);
                                    }
                                }

                                lblDescription.Visible = true;
                                return;
                            }
                            #endregion

                            #region Showing Plan Details

                            lstSelectedMachine = new List<Machine>();
                            lstSelectedMachine = (from lst in lstMACHINES
                                                  where lst.MCID.Equals(vertical.MCID)
                                                  select lst).ToList();
                            if (lstSelectedMachine != null && lstSelectedMachine.Count > 0)
                            {
                                if (lstSelectedMachine[0].LstPlan != null && lstSelectedMachine[0].LstPlan.Count > 0)
                                {
                                    for (int i = 0; i < lstSelectedMachine[0].LstPlan.Count; i++)
                                    {
                                        if (((e.X >= lstSelectedMachine[0].LstPlan[i].XCor) && (e.X <= lstSelectedMachine[0].LstPlan[i].XCor + lstSelectedMachine[0].LstPlan[i].W)) && ((e.Y >= lstSelectedMachine[0].LstPlan[i].YCor) && (e.Y <= lstSelectedMachine[0].LstPlan[i].YCor + lstSelectedMachine[0].LstPlan[i].H)))
                                        {
                                            description = "Plan ID : " + lstSelectedMachine[0].LstPlan[i].PlanID +
                                                          "\nFrom : " + lstSelectedMachine[0].LstPlan[i].From +
                                                          "\nTo : " + lstSelectedMachine[0].LstPlan[i].To +
                                                          "\nSequence No : " + lstSelectedMachine[0].LstPlan[i].SequenceNo +
                                                          "\nGroup : " + lstSelectedMachine[0].LstPlan[i].GroupID +
                                                          "\nAllocatedQty : " + lstSelectedMachine[0].LstPlan[i].AllocQty +
                                                          "\nXcor : " + lstSelectedMachine[0].LstPlan[i].XCor +
                                                          "\nYCor : " + lstSelectedMachine[0].LstPlan[i].YCor;

                                            lblDescription.Location = new Point(e.X + 10, e.Y + 10);
                                            lblDescription.Text = description;
                                            lblDescription.Visible = true;
                                            return;
                                        }
                                    }
                                }
                            }
                            #endregion
                        }
                    }
                }
                lblDescription.Visible = false;

                    #endregion
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void ucGraphicNew_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                isScrolling = false;
                tx = 0;
                foreach (Machine MCN in lstMACHINES)
                {
                    if (MCN.LstPlan != null && MCN.LstPlan.Count > 0)
                    {
                        List<Plan> lstPlan = (from lst in MCN.LstPlan.ToList()
                                              where lst.IsSelected.Equals(true)
                                              select lst).ToList();
                        int previousGroupID = (from lst in lstPlan
                                               select lst.GroupID).SingleOrDefault();

                        int newGroupID = 0;
                        foreach (var item in lstPlan)
                        {
                            int MCID = Convert.ToInt32(MCN.MCID);
                            item.XCor = tempRec.X;
                            item.YCor = tempRec.Y;
                            //item.IsSelected = false;
                            Vertical objVertical = new Vertical();
                            foreach (Vertical vertical in GloballstVertical)
                            {
                                if (vertical.MCID == MCID)
                                {
                                    objVertical = vertical;
                                    break;
                                }
                            }

                            Horizontal objHorizontal = null;
                            List<Horizontal> lstEligibleHorizontals = new List<Horizontal>();
                            foreach (Horizontal horizontal in GloballstHorizontal)
                            {
                                if (horizontal.X <= item.XCor && horizontal.Y <= item.YCor)
                                {
                                    lstEligibleHorizontals.Add(horizontal);
                                }
                            }
                            lstEligibleHorizontals = lstEligibleHorizontals.OrderByDescending(x => x.Hrs).ToList();
                            int selectedAreaGroupID = lstEligibleHorizontals.Max(x => x.GroupID);

                            lstEligibleHorizontals = (from lst in lstEligibleHorizontals
                                                      where lst.GroupID.Equals(selectedAreaGroupID)
                                                      select lst).ToList();
                            if (lstEligibleHorizontals.Count > 0)
                            {


                                List<Plan> lstCurrentPlans = (from lst in MCN.LstPlan.ToList()
                                                              where lst.IsSelected.Equals(false) && lst.GroupID.Equals(selectedAreaGroupID)
                                                              select lst).ToList();

                                foreach (Plan objplan in lstCurrentPlans)
                                {
                                    foreach (Horizontal hr in lstEligibleHorizontals.ToList())
                                    {
                                        if (hr.Hrs >= objplan.From && hr.Hrs < objplan.To)
                                        {
                                            lstEligibleHorizontals.Remove(hr);
                                        }
                                    }

                                }
                                if (lstEligibleHorizontals != null && lstEligibleHorizontals.Count > 0)
                                {
                                    CommonMethods objCommonMethods = new CommonMethods();
                                    if (lstCurrentPlans != null && lstCurrentPlans.Count > 0)
                                    {
                                        
                                        int distance = item.To - item.From;
                                        bool isCan = objCommonMethods.IsAvailableSlot(lstEligibleHorizontals[0].Hrs, lstCurrentPlans, distance, item.XCor, item.W, lstEligibleHorizontals);
                                        if (isCan)
                                        {
                                            item.From = lstEligibleHorizontals[0].Hrs;
                                            item.To = item.From + distance;
                                            item.IsLocationChanged = true;
                                            if ((item.To % 12) == 0)
                                            {
                                                item.GroupID = item.To / 12;
                                            }
                                            else
                                            {
                                                item.GroupID = (item.To / 12) + 1;
                                            }

                                            newGroupID = item.GroupID;
                                        }                                       
                                    }

                                    else
                                    {
                                        int distance = item.To - item.From;
                                        item.From = lstEligibleHorizontals[0].Hrs;
                                        item.To = item.From + distance;
                                        item.IsLocationChanged = true;
                                        if ((item.To % 12) == 0)
                                        {
                                            item.GroupID = item.To / 12;
                                        }
                                        else
                                        {
                                            item.GroupID = (item.To / 12) + 1;
                                        }

                                        newGroupID = item.GroupID;
                                    }                                    
                                    List<Plan> tempFirstGroup = new List<Plan>();
                                    tempFirstGroup = (from lst in MCN.LstPlan.ToList()
                                                      where lst.GroupID.Equals(previousGroupID)
                                                      select lst).ToList();

                                    List<Plan> tempNewGroup = new List<Plan>();
                                    tempNewGroup = (from lst in MCN.LstPlan.ToList()
                                                    where lst.GroupID.Equals(newGroupID)
                                                    select lst).ToList();
                                    tempFirstGroup = objCommonMethods.ChangeSequence(tempFirstGroup);
                                    tempNewGroup = objCommonMethods.ChangeSequence(tempNewGroup);
                                    tempFirstGroup = objCommonMethods.ChangeXYCordinations(tempFirstGroup);
                                    tempNewGroup = objCommonMethods.ChangeXYCordinations(tempNewGroup);
                                }
                            }
                        }

                    }

                }
                isDragable = false;
                this.Refresh();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        #region Menu Click Events
        public void mnuEditPlan_Click(object sender, EventArgs e)
        {
            frmAddEditPlan objfrmAddEditPlan = new frmAddEditPlan(Convert.ToInt32(OperationType.Edit));
            objfrmAddEditPlan.Show();
        }
        #endregion
    }
}
