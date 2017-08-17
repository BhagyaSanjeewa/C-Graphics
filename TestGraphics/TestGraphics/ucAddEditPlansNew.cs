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
    public partial class ucAddEditPlansNew : UserControl
    {

        public string mc = "";
        public int dtfrom = 0;
        public DateTime fromDate;
        public DateTime plnFrom = DateTime.Now;
        public DateTime NewPlanStartDate { get; set; }
        public int to = 0;
        public int ls = 0;
        public int mh = 0;
        int index = 0;
        private const int MaxCount = 10;
        private const int MaxHours = 100;
        List<Plan> lstGlobalPlans = new List<Plan>();
        public int screenWidth;
        public int screenHeight;
        public List<Machine> lstMACHINES = new List<Machine>();
        public List<Machine> lstExistingMachines = new List<Machine>();
        Size ScrollOffset = new Size(0, 0);
        Point scrollStartPoint, currentPoint;
        bool isScrolling = false;
        bool IsScol = false;
        int mcWidth = 100;
        bool isDragable = false;
        int tx, ty;
        Rectangle tempRec;
        int mcHeight = 50;
        List<Horizontal> GloballstHorizontal = null;
        List<Vertical> GloballstVertical = null;
        public int innerDivider = 24;
        int selectedMachineBarMachineID = 0;
        private int taskBarWidth = 35;
        public int WidthOfOneDate = 480;
        public int OperationType;
        int IndexOfCurrentMachineBar = 0;
        int IndexOfCurrentPlan = 0;
        public decimal RemovedQty = 0M;
        public event EventHandler UpdateDetails;


        public ucAddEditPlansNew()
        {
            InitializeComponent();
            lblDescription.Visible = false;
        }

        public ucAddEditPlansNew(int operationType)
        {
            InitializeComponent();
            lblDescription.Visible = false;
            OperationType = operationType;
        }

        private void ucAddEditPlansNew_Paint(object sender, PaintEventArgs e)
        {
            #region Common Variables
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            System.Drawing.Graphics formGraphics;
            formGraphics = this.CreateGraphics();
            screenWidth = this.Width;
            screenHeight = this.Height;
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

            mcHeight = 50 + mh;
            int mcSpace = mcHeight + 5;
            int mcDeviceCount = 0;
            int mcDevideY = 0;
            sx = ex;
            sy = ey - lineheight;
            msx = msx - mcWidth;
            int GroupID = 0;
            Pen dayLine = new Pen(Color.LightCoral);
            int minX = 100, minY = 100;
            int showingNoOfDays = 100;
            int DateDiffX = 20;
            int DateDiffY = 75;
            int NewX = 100;
            int NewY = 100;
            int NewDateX = 100;
            int NewDateY = 100;
            int MCX = 100 - mcWidth;
            int MCY = 100;
            string date = "";
            #endregion

            try
            {
                #region Background of form

                #region Background of form
                Brush background;
                background = new LinearGradientBrush(new Rectangle(0, 0, this.Width, this.Height), Color.DimGray, Color.Gray, 45);
                e.Graphics.FillRectangle(background, new Rectangle(0, 0, this.Width, this.Height));
                #endregion

                #region Back ground of planning  area
                Brush backGroundBrush = new LinearGradientBrush(new Rectangle(minX, minY, screenWidth - minX, screenHeight - minY), Color.LightGray, Color.WhiteSmoke, 45, false);
                e.Graphics.FillRectangle(backGroundBrush, new Rectangle(minX, minY, screenWidth - minY, screenHeight - minY));
                #endregion             

                #endregion
                //Vertical  line between header and planning area
                e.Graphics.DrawLine(dayLine, new Point(minX - 50, minY), new Point(this.Width, minY));
                //Horizontal line between header and planning area
                e.Graphics.DrawLine(dayLine, new Point(minX, minY - 50), new Point(minX, this.Height));

                #region Showing Days
                fromDate = plnFrom;
                date = fromDate.ToString("MMM-dd");
                for (int i = 0; i < showingNoOfDays; i++)
                {
                    date = fromDate.ToString("MMM-dd");
                    #region Showing date description
                    if (i < showingNoOfDays)
                    {
                        e.Graphics.DrawString(date, new Font("Times New Roman", 9, FontStyle.Bold), Brushes.Gold, new Point(NewDateX + ScrollOffset.Width + 100, minY - 60));
                        e.Graphics.DrawLine(dayLine, new Point(NewDateX + ScrollOffset.Width, minY - 50), new Point(NewDateX + ScrollOffset.Width, this.Height));
                    }
                    #endregion

                    NewX = NewDateX;
                    for (int j = 0; j <= innerDivider; j++)
                    {
                        objHorizontal = new Horizontal();
                        objHorizontal.Y = minY;
                        objHorizontal.X = NewX;
                        objHorizontal.Hrs = j;
                        objHorizontal.CurrentDate = fromDate;
                        if (j != innerDivider && j != 0)
                        {
                            e.Graphics.DrawLine(dayLine, new Point(NewX + ScrollOffset.Width, minY - 20), new Point(NewX + ScrollOffset.Width, this.Height));
                            e.Graphics.DrawString(j.ToString(), new Font("Tahoma", 10), brblue, new Point(NewX + ScrollOffset.Width - 6, minY - 50));
                        }

                        NewX = NewX + WidthOfOneDate / innerDivider;
                        lstHorizontal.Add(objHorizontal);
                    }

                    NewDateX = NewDateX + WidthOfOneDate;
                    fromDate = fromDate.AddDays(1);
                }
                #endregion

                #region Draw Machines
                Rectangle rc;
                if (lstMACHINES != null && lstMACHINES.Count > 0)
                {
                    for (int k = 0; k < lstMACHINES.Count; k++)
                    {
                        objVertical = new Vertical();
                        objVertical.MCCode = lstMACHINES[k].MCName;
                        objVertical.MCID = lstMACHINES[k].MCID;
                        objVertical.Capacity = lstMACHINES[k].Capacity;
                        objVertical.X = 100;
                        objVertical.Y = MCY;
                        lstVertical.Add(objVertical);
                        rc = new Rectangle(MCX, MCY + ScrollOffset.Height, mcWidth, mcHeight);
                        LinearGradientBrush l = new LinearGradientBrush(rc, Color.Red, Color.DarkRed, 1, true);
                        e.Graphics.FillRectangle(l, rc);
                        e.Graphics.DrawString(objVertical.MCCode.ToString(), new Font("Tahoma", 10), brblue, new Point(MCX + 20, MCY + 20 +ScrollOffset.Height));

                        e.Graphics.DrawLine(dayLine, new Point(minX - mcWidth, MCY + mcHeight + ScrollOffset.Height), new Point(this.Width, MCY + mcHeight + ScrollOffset.Height));
                        MCY = MCY + mcHeight;
                        IndexOfCurrentMachineBar++;
                    }
                }
                #endregion

                #region Draw More Than One Plans
                Plan objPln;
                List<Plan> lstp;
                if (!isDragable )
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
                                int plancount = 0;
                                foreach (Plan Pn in MCN.LstPlan.ToList())
                                {
                                    if (Pn.FromDate.Date >= plnFrom.Date)
                                    {
                                        List<Plan> lstPlan = new List<Plan>();
                                        #region Getting the previous plan End Position And Draw the Next Plan
                                        plancount++;

                                        Pn.From = Pn.From;

                                        Vertical v = new Vertical();
                                        v.MCID = MCN.MCID;
                                        Vertical vv = lstVertical.Find(x => x.MCID == v.MCID);

                                        Horizontal hr = new Horizontal();
                                        hr.Hrs = Pn.From;
                                        Horizontal hrr = lstHorizontal.Find(x => x.Hrs == hr.Hrs && x.CurrentDate.Date == Pn.FromDate.Date);

                                        Horizontal too = new Horizontal();
                                        too.Hrs = Pn.To;
                                        Horizontal tooo = lstHorizontal.Find(x => x.Hrs == too.Hrs && x.CurrentDate.Date == Pn.FromDate.Date);

                                        int width = tooo.X - hrr.X;

                                        objPln = new Plan();
                                        objPln.From = Pn.From;
                                        objPln.To = Pn.To;                                       

                                        previousGroupID = MCN.LstPlan[MCN.LstPlan.Count - 2].GroupID;
                                        currentGroupID = objPln.GroupID;

                                        int currentPlanCount = MCN.LstPlan.Where(s => s.FromDate.Date == Pn.FromDate.Date).Count();
                                        if (currentPlanCount == 1)
                                        {
                                            mcDevideY = 0;
                                        }
                                        mcDeviceCount = mcHeight / currentPlanCount;

                                        List<Plan> tempCurrent = new List<Plan>();
                                        tempCurrent = (from lst in lstp
                                                       where lst.FromDate.Date.Equals(Pn.FromDate.Date)
                                                       select lst).ToList();

                                        mcDevideY = mcDeviceCount * tempCurrent.Count;

                                        objPln = this.FillPlanObject(Pn);
                                        objPln.YCor = vv.Y + mcDevideY + ScrollOffset.Height;
                                        objPln.XCor = hrr.X + ScrollOffset.Width; 
                                        objPln.W = width;
                                        objPln.H = mcDeviceCount;

                                        lstp.Add(objPln);
                                        lstGlobalPlans.Add(objPln);

                                        #endregion

                                        CommonMethods objCommonMethods = new CommonMethods();
                                        lstp = objCommonMethods.ChangeSequenceByDate(lstp);
                                        lstp = objCommonMethods.ChangeXYCordinationsByDate(lstp);
                                        foreach (var item in lstp)
                                        {
                                            if (item.IsAlreadyAddedPlan)
                                            {
                                                Rectangle rcc = new Rectangle(item.XCor, item.YCor, item.W, item.H);
                                                LinearGradientBrush l = new LinearGradientBrush(rcc, Color.Blue, Color.White, 1, true);
                                                e.Graphics.FillRectangle(l, rcc);
                                            }
                                            else
                                            {
                                                Rectangle rcc = new Rectangle(item.XCor, item.YCor, item.W, item.H);
                                                LinearGradientBrush l = new LinearGradientBrush(rcc, Color.Purple, Color.White, 1, true);
                                                e.Graphics.FillRectangle(l, rcc);
                                            }
                                            if (item.IsEditable)
                                            {
                                                Rectangle rcc = new Rectangle(item.XCor, item.YCor, item.W, item.H);
                                                LinearGradientBrush l = new LinearGradientBrush(rcc, Color.Green, Color.White, 1, true);
                                                e.Graphics.FillRectangle(l, rcc);
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                foreach (Plan Pn in MCN.LstPlan)
                                {
                                    if (Pn.FromDate.Date >= plnFrom.Date)
                                    {
                                        Vertical v = new Vertical();
                                        v.MCID = MCN.MCID;
                                        Vertical vv = lstVertical.Find(x => x.MCID == v.MCID);

                                        Horizontal hr = new Horizontal();
                                        hr.Hrs = Pn.From;
                                        Horizontal hrr = lstHorizontal.Find(x => x.Hrs == hr.Hrs && x.CurrentDate.Date == Pn.FromDate.Date);

                                        Horizontal too = new Horizontal();
                                        too.Hrs = Pn.To;
                                        Horizontal tooo = lstHorizontal.Find(x => x.Hrs == too.Hrs && x.CurrentDate.Date == Pn.FromDate.Date);

                                        int width = tooo.X - hrr.X;

                                        objPln = new Plan();                                       
                                        objPln = this.FillPlanObject(Pn);
                                        objPln.XCor = hrr.X + ScrollOffset.Width;
                                        objPln.YCor = vv.Y+ ScrollOffset.Height;
                                        objPln.W = width;
                                        objPln.H = mcHeight;

                                        lstp.Add(objPln);
                                        lstGlobalPlans.Add(objPln);

                                        foreach (var item in lstp)
                                        {
                                            if (item.IsAlreadyAddedPlan)
                                            {
                                                Rectangle rcc = new Rectangle(item.XCor, item.YCor, item.W, item.H);
                                                LinearGradientBrush l = new LinearGradientBrush(rcc, Color.Blue, Color.White, 1, true);
                                                e.Graphics.FillRectangle(l, rcc);
                                            }
                                            else
                                            {
                                                Rectangle rcc = new Rectangle(item.XCor, item.YCor, item.W, item.H);
                                                LinearGradientBrush l = new LinearGradientBrush(rcc, Color.Purple, Color.White, 1, true);
                                                e.Graphics.FillRectangle(l, rcc);
                                            }
                                            if (item.IsEditable)
                                            {
                                                Rectangle rcc = new Rectangle(item.XCor, item.YCor, item.W, item.H);
                                                LinearGradientBrush l = new LinearGradientBrush(rcc, Color.Green, Color.White, 1, true);
                                                e.Graphics.FillRectangle(l, rcc);
                                            }
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
                                if (Pn.FromDate.Date >= plnFrom.Date)
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
                                    objPln = this.FillPlanObject(Pn);
                                    lstp.Add(objPln);
                                    lstGlobalPlans.Add(objPln);

                                    foreach (var item in lstp)
                                    {
                                        if ((item.XCor >= 100) && item.YCor >= 100)
                                        {
                                            if (item.IsAlreadyAddedPlan)
                                            {
                                                Rectangle rcc = new Rectangle(item.XCor, item.YCor, item.W, item.H);
                                                LinearGradientBrush l = new LinearGradientBrush(rcc, Color.Blue, Color.White, 1, true);
                                                e.Graphics.FillRectangle(l, rcc);
                                            }
                                            else
                                            {
                                                Rectangle rcc = new Rectangle(item.XCor, item.YCor, item.W, item.H);
                                                LinearGradientBrush l = new LinearGradientBrush(rcc, Color.Purple, Color.White, 1, true);
                                                e.Graphics.FillRectangle(l, rcc);
                                            }
                                            if (item.IsEditable)
                                            {
                                                Rectangle rcc = new Rectangle(item.XCor, item.YCor, item.W, item.H);
                                                LinearGradientBrush l = new LinearGradientBrush(rcc, Color.Green, Color.White, 1, true);
                                                e.Graphics.FillRectangle(l, rcc);
                                            }
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
                                if (temp.IsEditable)
                                {
                                    LinearGradientBrush ll = new LinearGradientBrush(tempRec, Color.Green, Color.White, 1, true);
                                    e.Graphics.FillRectangle(ll, tempRec);
                                }
                                this.Refresh();
                            }
                            MCN.LstPlan.RemoveAll(x => x.XCor == 0);
                            MCN.LstPlan = lstp;
                        }
                    }
                }
                #endregion
           
                GloballstHorizontal = lstHorizontal;
                GloballstVertical = lstVertical;
            }
            catch (Exception exx)
            {

                throw exx;
            }
        }

        private void ucAddEditPlansNew_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                List<Machine> lstSelectedMachine;
                #region Moving Plan

                if (isDragable)
                {
                    if (this.OperationType == Convert.ToInt32(TestGraphics.OperationType.Edit))
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
                            if (lstMACHINES != null && lstMACHINES.Count > 0)
                            {
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
                                                              "\nYCor : " + lstSelectedMachine[0].LstPlan[i].YCor +
                                                              "\nRowVersion : " + lstSelectedMachine[0].LstPlan[i].RowVersion;

                                                lblDescription.Location = new Point(e.X + 10, e.Y + 10);
                                                lblDescription.Text = description;
                                                lblDescription.Visible = true;
                                                return;
                                            }
                                        }
                                    }
                                }
                            }
                            #endregion
                        }
                    }
                    #endregion
                }
                lblDescription.Visible = false;
                   
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void ucAddEditPlansNew_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                ContextMenu mnuContext;
                MenuItem mnuDeleteMachine;
                MenuItem mnuDeletePlan;
                MenuItem mnuEditPlan;

                #region Right Click Events

                if (e.Button == MouseButtons.Right)
                {
                    #region Machine Click Events
                    foreach (var item in GloballstVertical)
                    {
                        if ((item.X - mcWidth <= e.X && item.X >= e.X && (item.Y <= e.Y && item.Y + mcHeight >= e.Y)))
                        {
                            selectedMachineBarMachineID = item.MCID;
                            mnuContext = new ContextMenu();
                            mnuDeleteMachine = new MenuItem();
                            mnuDeleteMachine.Text = "Delete Machine";
                            mnuContext.MenuItems.Add(mnuDeleteMachine);
                            mnuDeleteMachine.Click += new System.EventHandler(this.mnuDeleteMachine_Click);
                            this.ContextMenu = mnuContext;
                            this.ContextMenu.Show(this, new Point(e.X, e.Y));
                            ContextMenu = null;
                            break;
                        }
                    }
                    #endregion

                    #region Machine Plan Click Events
                    foreach (Machine mc in lstMACHINES)
                    {
                        bool isFound = false;
                        foreach (Plan pn in mc.LstPlan)
                        {
                            if (((pn.XCor + pn.W >= e.X) && (pn.XCor <= e.X)) && ((pn.YCor <= e.Y) && (pn.YCor + pn.H >= e.Y)))
                            {

                                selectedMachineBarMachineID = mc.MCID;
                                IndexOfCurrentPlan = pn.SequenceNo;
                                selectedPlanDate = pn.FromDate;
                                mnuContext = new ContextMenu();
                                mnuDeletePlan = new MenuItem();
                                mnuDeletePlan.Text = "Remove Plan";
                                mnuContext.MenuItems.Add(mnuDeletePlan);
                                mnuDeletePlan.Click += new System.EventHandler(this.mnuDeletePlan_Click);


                                mnuEditPlan = new MenuItem();
                                mnuEditPlan.Text = "Edit Plan";
                                mnuContext.MenuItems.Add(mnuEditPlan);
                                mnuEditPlan.Click += new System.EventHandler(this.mnuEditPlan_Click);

                                this.ContextMenu = mnuContext;
                                this.ContextMenu.Show(this, new Point(e.X, e.Y));
                                ContextMenu = null;
                                break;
                            }

                            if (isFound)
                            {
                                break;
                            }
                        }
                    }
                    #endregion

                }
                #endregion
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
                                    if ((item.IsEditable) || (!item.IsAlreadyAddedPlan))
                                    {
                                        tempRec = new Rectangle(item.XCor, item.YCor, item.W, item.H);
                                        tx = e.X - tempRec.X;
                                        ty = e.Y - tempRec.Y;
                                        item.IsSelected = true;
                                        isDragable = true;
                                        selectedMachineBarMachineID = MCN.MCID;
                                        return;
                                    }
                                    else
                                    {
                                        return;
                                    }
                                }
                            }
                        }
                    }

                    isScrolling = true;
                    scrollStartPoint = new Point(e.X - ScrollOffset.Width, e.Y - ScrollOffset.Height);
                    Cursor = Cursors.Hand;
                }
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

        #region Menu Click Events
        public void mnuDeleteMachine_Click(object sender, EventArgs e)
        {
            try
            {
                if (selectedMachineBarMachineID > 0)
                {
                    foreach (var item in lstMACHINES.ToList())
                    {
                        if (item.MCID == selectedMachineBarMachineID)
                        {

                            RemovedQty = item.LstPlan.Sum(x => x.AllocQty);
                            lstMACHINES.Remove(item);
                            if (UpdateDetails != null)
                            {
                                UpdateDetails(this, new EventArgs());
                            }
                            break;
                        }
                    }
                    this.Refresh();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void mnuDeletePlan_Click(object sender, EventArgs e)
        {
            try
            {
                if (selectedMachineBarMachineID > 0)
                {
                    foreach (var item in lstMACHINES.ToList())
                    {
                        bool deleted = false;
                        foreach (Plan pn in item.LstPlan.ToList())
                        {
                            if (item.MCID.Equals(selectedMachineBarMachineID) && (pn.SequenceNo.Equals(IndexOfCurrentPlan)) && (pn.FromDate.Date.Equals(selectedPlanDate.Date)))
                            {
                                if (pn.IsAlreadyAddedPlan)
                                {
                                    throw new ApplicationException("You Cannot Remove AllReady Added Plans");
                                }
                                RemovedQty = pn.AllocQty;
                                item.LstPlan.Remove(pn);
                                if (UpdateDetails != null)
                                {
                                    UpdateDetails(this, new EventArgs());
                                }
                                deleted = true;
                                break;
                            }
                        }
                        if (deleted)
                        {
                            break;
                        }
                    }
                    this.Refresh();
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

        public void mnuEditPlan_Click(object sender, EventArgs e)
        {
            try
            {
                if (selectedMachineBarMachineID > 0)
                {
                    foreach (var item in lstMACHINES.ToList())
                    {
                        bool edited = false;
                        foreach (Plan pn in item.LstPlan.ToList())
                        {
                            if (item.MCID.Equals(selectedMachineBarMachineID) && (pn.SequenceNo.Equals(IndexOfCurrentPlan)) && (pn.FromDate.Date.Equals(selectedPlanDate.Date)))
                            {

                                if (!pn.IsAlreadyAddedPlan)
                                {
                                    throw new ApplicationException("You Cannot Edit Newly Added Plans");
                                }

                                frmEditPlans objfrmAddEditPlansNew = new frmEditPlans(Convert.ToInt32(TestGraphics.OperationType.Edit), pn);
                                bool isFormOpen = IsAlreadyOpen(typeof(frmEditPlans));
                                FormCollection fc = Application.OpenForms;

                                if (isFormOpen)
                                {
                                    throw new ApplicationException("You Cannot Edit Multiple Plans Same Time");
                                }

                                else
                                {
                                    objfrmAddEditPlansNew.ShowDialog();
                                    edited = true;
                                    break;
                                }
                            }
                        }
                        if (edited)
                        {
                            break;
                        }
                    }
                    this.Refresh();
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

        #endregion

        private void ucAddEditPlansNew_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                isScrolling = false;
                Cursor = Cursors.Default;

                if (this.OperationType == Convert.ToInt32(TestGraphics.OperationType.Edit))
                {
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
                            DateTime previousDate = (from lst in lstPlan
                                                     select lst.FromDate).SingleOrDefault();
                            int newGroupID = 0;
                            DateTime newDate = DateTime.Now;
                            int MaxXCorOfCurrentGroup = 0;
                            foreach (var item in lstPlan)
                            {
                                int MCID = Convert.ToInt32(MCN.MCID);
                                item.XCor = tempRec.X;
                                item.YCor = tempRec.Y;
                                Vertical objVertical = new Vertical();
                                foreach (Vertical vertical in GloballstVertical)
                                {
                                    if (vertical.MCID == MCID)
                                    {
                                        objVertical = vertical;
                                        break;
                                    }
                                }

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
                                DateTime selectedDate = lstEligibleHorizontals.Max(x => x.CurrentDate);
                                lstEligibleHorizontals = (from lst in lstEligibleHorizontals
                                                          where lst.CurrentDate.Date.Equals(selectedDate.Date)
                                                          select lst).ToList();
                                MaxXCorOfCurrentGroup = GloballstHorizontal.Where(x => x.CurrentDate.Date.Equals(selectedDate.Date)).Max(x => x.X);
                                if (lstEligibleHorizontals.Count > 0)
                                {
                                    List<Plan> lstCurrentPlans = (from lst in MCN.LstPlan.ToList()
                                                                  where lst.IsSelected.Equals(false) && lst.FromDate.Date.Equals(selectedDate.Date)
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

                                        if (MaxXCorOfCurrentGroup >= item.XCor + item.W)
                                        {
                                            CommonMethods objCommonMethods = new CommonMethods();
                                            if (lstCurrentPlans != null && lstCurrentPlans.Count > 0)
                                            {
                                                int distance = item.To - item.From;
                                                bool isCan = objCommonMethods.IsAvailableSlotByDate(lstEligibleHorizontals[0].Hrs, lstCurrentPlans, distance, item.XCor, item.W, lstEligibleHorizontals);
                                                if (isCan)
                                                {
                                                    item.From = lstEligibleHorizontals[0].Hrs;
                                                    item.To = item.From + distance;
                                                    item.IsLocationChanged = true;
                                                    newGroupID = item.GroupID;
                                                    item.FromDate = lstEligibleHorizontals[0].CurrentDate;
                                                    newDate = item.FromDate;
                                                }
                                            }

                                            else
                                            {
                                                int distance = item.To - item.From;
                                                item.From = lstEligibleHorizontals[0].Hrs;
                                                item.To = item.From + distance;
                                                item.IsLocationChanged = true;
                                                item.FromDate = lstEligibleHorizontals[0].CurrentDate;
                                                newDate = item.FromDate;
                                            }
                                            List<Plan> tempFirstGroup = new List<Plan>();
                                            tempFirstGroup = (from lst in MCN.LstPlan.ToList()
                                                              where lst.FromDate.Date.Equals(previousDate.Date)
                                                              select lst).ToList();

                                            List<Plan> tempNewGroup = new List<Plan>();
                                            tempNewGroup = (from lst in MCN.LstPlan.ToList()
                                                            where lst.FromDate.Date.Equals(newDate.Date)
                                                            select lst).ToList();
                                            tempFirstGroup = objCommonMethods.ChangeSequenceByDate(tempFirstGroup);
                                            tempNewGroup = objCommonMethods.ChangeSequenceByDate(tempNewGroup);
                                            tempFirstGroup = objCommonMethods.ChangeXYCordinationsByDate(tempFirstGroup);
                                            tempNewGroup = objCommonMethods.ChangeXYCordinationsByDate(tempNewGroup);
                                        }
                                    }
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

        public DateTime selectedPlanDate { get; set; }

        private void ucAddEditPlansNew_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Left)
                {
                    foreach (var item in GloballstVertical)
                    {
                        if ((item.X - mcWidth <= e.X && item.X >= e.X && (item.Y <= e.Y && item.Y + mcHeight >= e.Y)))
                        {
                            List<Machine> lstMachine = new List<Machine>();
                            lstMachine = lstMACHINES;
                            #region Validating Maximum Hours Reached
                            int currentGroupPlanHours = 0;
                            List<Machine> lstSelectedMachine = new List<Machine>();
                            lstSelectedMachine = (from lst in lstMachine
                                                  where lst.MCID.Equals(item.MCID)
                                                  select lst).ToList();
                            if (lstSelectedMachine != null && lstSelectedMachine.Count > 0)
                            {
                                List<Plan> lstGroupPlans = new List<Plan>();
                                lstGroupPlans = (from lst in lstSelectedMachine[0].LstPlan
                                                 where lst.FromDate.Date.Equals(NewPlanStartDate.Date)
                                                 select lst).ToList();
                                if (lstGroupPlans.Count > 0)
                                {
                                    foreach (var itemss in lstGroupPlans)
                                    {
                                        currentGroupPlanHours = currentGroupPlanHours + (itemss.To - itemss.From);
                                    }
                                }

                                if (currentGroupPlanHours > MaxHours)
                                {
                                    throw new ApplicationException("Maximum Plan Hours Reached Select New Date");
                                }
                            }
                            #endregion
                            //Add New Plan 
                            if (this.OperationType != Convert.ToInt32(TestGraphics.OperationType.Edit))
                            {
                                frmAddData objfrmAddData = new frmAddData();
                                objfrmAddData.ShowDialog();

                                if ((currentGroupPlanHours + objfrmAddData.NoOfHours) > MaxHours)
                                {
                                    throw new ApplicationException("Maximum Plan Hours Reached Select New Date");
                                }

                                CommonMethods objCommonMethods = new CommonMethods();
                                Machine objMachine = new Machine();
                                objMachine = lstMACHINES.Find(x => x.MCID == item.MCID);

                                if (objMachine.LstPlan != null)
                                {
                                    List<Plan> lstTmp = new List<Plan>();
                                    lstTmp = objCommonMethods.DrawPlanByDate(lstMACHINES, item.MCID, NewPlanStartDate.Date, objfrmAddData.NoOfHours, objfrmAddData.AllocQty);
                                    var xx = objMachine.LstPlan.Concat(lstTmp);
                                    objMachine.LstPlan = xx.ToList();
                                }
                                else
                                {
                                    objMachine.LstPlan = objCommonMethods.DrawPlanByDate(lstMACHINES, item.MCID, NewPlanStartDate.Date, objfrmAddData.NoOfHours, objfrmAddData.AllocQty);
                                }

                                foreach (var items in lstMACHINES.ToList())
                                {
                                    if (items.MCID.Equals(objMachine.MCID))
                                    {
                                        lstMACHINES.Remove(items);
                                        break;
                                    }
                                }
                                lstMACHINES.Add(objMachine);
                                lstMACHINES = lstMACHINES.OrderBy(x => x.MCID).ToList();
                                this.Refresh();
                                break;
                            }
                            //Add New Plans When Editing a plan
                            else
                            {
                                //frmAddData objfrmAddData = new frmAddData(Convert.ToInt32(TestGraphics.OperationType.Edit),);
                                //objfrmAddData.ShowDialog();

                            }
                        }
                    }


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

        private bool IsAlreadyOpen(Type formType)
        {

            bool isOpen = false;
            foreach (Form f in Application.OpenForms)
            {
                if (f.GetType() == formType)
                {

                    f.BringToFront();
                    f.WindowState = FormWindowState.Normal;
                    isOpen = true;
                }
            }
            return isOpen;
        }

        private Plan FillPlanObject(Plan Pn)
        {
            Plan objPlan = new Plan();
            objPlan.From = Pn.From;
            objPlan.To = Pn.To;
            objPlan.GroupID = Pn.GroupID;
            objPlan.XCor = Pn.XCor;
            objPlan.PlanID = Pn.PlanID;
            objPlan.AllocQty = Pn.AllocQty;
            objPlan.YCor = Pn.YCor;
            objPlan.SequenceNo = Pn.SequenceNo;
            objPlan.IsLocationChanged = Pn.IsLocationChanged;
            objPlan.IsAlreadyAddedPlan = Pn.IsAlreadyAddedPlan;
            objPlan.W = Pn.W;
            objPlan.H = Pn.H;
            objPlan.FromDate = Pn.FromDate;
            objPlan.RowVersion = Pn.RowVersion;
            objPlan.MCID = Pn.MCID;
            objPlan.IsEditable = Pn.IsEditable;
            return objPlan;
        }

        public void ScrollToBottom(UserControl p)
        {
            using (Control c = new Control() { Parent = p, Dock = DockStyle.Bottom })
            {
                p.ScrollControlIntoView(c);
                c.Parent = null;
            }
        }
    }
}
