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
    public partial class ucGraphicMain : UserControl
    {
        public Label lblItemDescription;
        int dateX;
        int dateY = 50;
        int minY = 50;
        private int machineShowingWidth = 150;//Machine rectangle width
        int minX = 150 + 11;
        int previousNoOfDays = 30;
        int paddingInnerLines = 8;
        public int innerDevider = 24;//Devide of a day
        public int screenWidth;
        public int screenHeight;
        public int widthOfOnePiece = 240;//Width of a day
        public int showingNoOfDays = 100;//No of days show in window
        public int barHeight = 60;//Height of a bar
        private int mousePointYdiff = 22;//
        private int taskBarWidth = 35;
        private int startXcoodinate;
        private int indexOfCurrentBar = -1;
        private int indexOfCurrentPlan = -1;
        private int indexOfContinuous = -1;
        public int widthOfInnerPiece = 12;//
        public int hoursInOnePiece = 1;
        private int[] recDrawingXcoordinateWidth;
        private bool isParalelToAnother;
        public DateTime plnFrom = DateTime.Now;
        public DateTime plnTo;
        private DateTime showDay;
        private Color[] newPlanColor = { Color.Blue, Color.MediumPurple };
        private Color[] currentPlanColor = { Color.MediumSlateBlue, Color.MediumPurple };
        private Color[] ProcessDateDeletedPlan = { Color.DarkOrchid, Color.Magenta };
        private Color[] tapeSettingColor = { Color.DimGray, Color.RosyBrown };
        private Color[] productionColor = { Color.FromArgb(200, Color.DodgerBlue), Color.FromArgb(200, Color.DeepSkyBlue) };
        private Color[] predictActualStartedColor = { Color.FromArgb(200, Color.DarkOrange), Color.FromArgb(200, Color.DarkOrange) };
        private Color[] predictLatedPlanColor = { Color.FromArgb(200, Color.DarkRed), Color.FromArgb(200, Color.DarkRed) };
        private Color[] interruptionColor = { Color.LightSeaGreen, Color.CadetBlue };
        private Color[] machineColor = { Color.Maroon, Color.Plum };
        private Color[] EndedPlanColor = { Color.FromArgb(200, Color.DodgerBlue), Color.White };
        Size ScrollOffset = new Size(0, 0);
        Point scrollStartPoint, currentPoint;
        bool isScrolling = false;
        Rectangle drawRect = new Rectangle();

        public int FromPosition = 16;

        public List<Machine> lstGlobalMachines { get; set; }

        public List<MachineBar> LstMachineBar { get; set; }

        public ucGraphicMain()
        {
            InitializeComponent();
            VScroll = true; 
            HScroll = true;
        }

        private void ucGraphicMain_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

                #region Common Variables
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                System.Drawing.Graphics formGraphics;
                formGraphics = this.CreateGraphics();


                Color[] Lines = { Color.Black, Color.HotPink };
                Pen p = new Pen(Lines[0], 6);
                Pen pn1 = new Pen(Lines[1], 2);

                e.Graphics.DrawLine(pn1, new Point(0, 100), new Point(this.Width, 100));
                e.Graphics.DrawLine(pn1, new Point(100, 0), new Point(100, this.Height));


                SolidBrush brblue = new SolidBrush(Color.Blue);

                Vertical objVertical;
                List<Vertical> lstVertical = new List<Vertical>();

                Horizontal objHorizontal;
                List<Horizontal> lstHorizontal = new List<Horizontal>();

                int sx, ex, sy, ey = 0;
                int msx, mex, msy, mey = 0;
                int drwingBeginx = ex = msx = 100;
                int drwingBeginy = ey = msy = 100;
                int lineheight = 60;
                int lineSpace = 40;
                int mcWidth = 150;
                int mcHeight = 30;                
                int mcSpace = mcHeight + 5;
                int mcDeviceCount = 0;
                int mcDevideY = 0;
                sx = ex;
                sy = ey - lineheight;
                msx = msx - mcWidth;
                #endregion

                #region Set screen properties

                screenWidth = this.Width;
                screenHeight = this.Height;

                #endregion

                #region Background of form
                Brush background;
                background = new LinearGradientBrush(new Rectangle(0, 0, screenWidth, screenHeight), Color.DimGray, Color.Gray, 45);
                e.Graphics.FillRectangle(background, new Rectangle(0, 0, screenWidth, screenHeight));
                #endregion

                #region Back ground of planning  area
                dateX = machineShowingWidth + 11;//plan start possition on x axis
                Brush backGroundBrush = new LinearGradientBrush(new Rectangle(dateX, dateY, screenWidth - dateX, screenHeight - dateY), Color.LightGray, Color.WhiteSmoke, 45, false);
                e.Graphics.FillRectangle(backGroundBrush, new Rectangle(dateX, dateY, screenWidth - dateX, screenHeight - dateY));
                #endregion

                #region Tools for draw plans
                isParalelToAnother = false;
                #endregion

                #region Show days

                #region Tools for drawing days

                SolidBrush holidayBrush = new SolidBrush(Color.Ivory);//Linen,Bisque,Ivory,Lavender,LemonChiffon,LightYellow,Moccasin
                Pen dayLine = new Pen(Color.LightCoral);
                showDay = plnFrom;
                string date;

                #endregion

                #region Showing days

                //Horizontal  line between header and planning area
                e.Graphics.DrawLine(dayLine, new Point(dateX, dateY), new Point(screenWidth, dateY));//

                for (int i = 0; i < showingNoOfDays + 1; i++)
                {
                    //devide plan days
                    if (CheckPointConsiderX(minX, dateX + ScrollOffset.Width))
                        e.Graphics.DrawLine(dayLine, new Point(dateX + ScrollOffset.Width, dateY - 30), new Point(dateX + ScrollOffset.Width, screenHeight));
                    #region Showing date description

                    date = showDay.ToString("MMM-dd");//Date description for show
                    if (i < showingNoOfDays)//Showing date description  
                    {
                        if (CheckPointConsiderX(minX, (dateX + Convert.ToInt16(widthOfOnePiece / 2) - 20 + ScrollOffset.Width)))
                            e.Graphics.DrawString(date, new Font("Times New Roman", 9, FontStyle.Bold), Brushes.Gold, new Point(dateX + Convert.ToInt16(widthOfOnePiece / 2) - 20 + ScrollOffset.Width, dateY - 35));
                    }

                    #endregion
                    #region Showing time in devided day

                    if (i < showingNoOfDays)
                    {
                        for (int j = 1; j < innerDevider; j++)
                        {
                            if (Convert.ToInt16(j % 2) == 0 && j != 1)
                            {
                                if (innerDevider == 4)//Zoom 25%
                                {
                                    if (CheckPointConsiderX(minX, dateX - 4 + (j * (widthOfOnePiece / innerDevider)) + ScrollOffset.Width))
                                        e.Graphics.DrawString(Convert.ToString(j * 6), new Font("Times New Roman", 7, FontStyle.Bold), Brushes.Black, new Point(dateX - 4 + (j * (widthOfOnePiece / innerDevider)) + ScrollOffset.Width, dateY - 20));
                                }
                                else if (innerDevider == 12)//Zoom 100%
                                {
                                    if (CheckPointConsiderX(minX, dateX - 4 + (j * (widthOfOnePiece / innerDevider)) + ScrollOffset.Width))
                                        e.Graphics.DrawString(Convert.ToString(j * 2), new Font("Times New Roman", 7, FontStyle.Bold), Brushes.Black, new Point(dateX - 4 + (j * (widthOfOnePiece / innerDevider)) + ScrollOffset.Width, dateY - 20));
                                }
                                else if (innerDevider == 24)//Zoom 100%
                                {
                                    if (CheckPointConsiderX(minX, dateX - 4 + (j * (widthOfOnePiece / innerDevider)) + ScrollOffset.Width))
                                        e.Graphics.DrawString(Convert.ToString(j * 1), new Font("Times New Roman", 7, FontStyle.Bold), Brushes.Black, new Point(dateX - 4 + (j * (widthOfOnePiece / innerDevider)) + ScrollOffset.Width, dateY - 20));
                                }
                                else //else
                                {
                                    if (CheckPointConsiderX(minX, dateX - 4 + (j * (widthOfOnePiece / innerDevider)) + ScrollOffset.Width))
                                        e.Graphics.DrawString(Convert.ToString(j * 3), new Font("Times New Roman", 7, FontStyle.Bold), Brushes.Black, new Point(dateX - 4 + (j * (widthOfOnePiece / innerDevider)) + ScrollOffset.Width, dateY - 20));
                                }

                                if (CheckPointConsiderX(minX, dateX + (j * (widthOfOnePiece / innerDevider)) + ScrollOffset.Width))
                                    e.Graphics.DrawLine(dayLine, new Point(dateX + (j * (widthOfOnePiece / innerDevider)) + ScrollOffset.Width, dateY - 7), new Point(dateX + (j * (widthOfOnePiece / innerDevider)) + ScrollOffset.Width, dateY));//devide plan days
                            }
                            else
                            {
                                if (CheckPointConsiderX(minX, dateX + (j * (widthOfOnePiece / innerDevider)) + ScrollOffset.Width))
                                    e.Graphics.DrawLine(dayLine, new Point(dateX + (j * (widthOfOnePiece / innerDevider)) + ScrollOffset.Width, dateY - 15), new Point(dateX + (j * (widthOfOnePiece / innerDevider)) + ScrollOffset.Width, dateY));//devide plan days
                            }
                        }
                    }

                    #endregion

                    dateX = dateX + widthOfOnePiece;//Move to next x possition
                    showDay = showDay.AddDays(1);//Get next date

                }
                #endregion

                #endregion


                #region Draw Plans
                if (LstMachineBar != null && LstMachineBar.Count > 0)
                {
                    #region tools for show plan

                    int yCor = dateY + 5;
                    startXcoodinate = machineShowingWidth + 11;
                    Pen devideLine = new Pen(Color.Black);
                    Pen devidePlan = new Pen(Color.White);
                    #endregion

                    for (int i = 0; i < LstMachineBar.Count; i++)
                    {
                        objVertical = new Vertical();
                        objVertical.MCID = i;
                        objVertical.MCCode = "M-" + i.ToString();

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

                        msy = msy + mcSpace;

                        objHorizontal = new Horizontal();
                        objHorizontal.Hrs = i;
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

                        sx = sx + lineSpace;
                        ex = ex + lineSpace;
                    }

                    for (int i = 0; i < LstMachineBar.Count; i++)
                    {
                        #region Draw a line to seperate machine bar

                        if (i == 0)// draw first line
                        {
                            if (CheckPointConsiderY(minY, yCor - 5))
                                e.Graphics.DrawLine(devideLine, new Point(startXcoodinate, yCor - 5), new Point(screenWidth, yCor - 5));
                        }
                        //Draw line after every machine bar
                        if (CheckPointConsiderY(minY, yCor + barHeight + 5 + ScrollOffset.Height))
                            e.Graphics.DrawLine(devideLine, new Point(startXcoodinate, yCor + barHeight + 5 + ScrollOffset.Height), new Point(screenWidth, yCor + barHeight + 5 + ScrollOffset.Height));
                        #endregion

                        #region Show machines

                        drawRectangleConsiderY(e, machineColor, new Rectangle(10, yCor - 3 + ScrollOffset.Height, machineShowingWidth, barHeight + 6));
                        if (CheckPointConsiderY(minY, ScrollOffset.Height + yCor + (barHeight / 2) - 5))
                            e.Graphics.DrawString(LstMachineBar[i].ObjMachine.MCName, new Font("Khmer UI", 8, FontStyle.Bold), Brushes.Black, new Point(40, ScrollOffset.Height + yCor + (barHeight / 2) - 5));// Row Header description

                        #endregion

                        // --==================================================
                        Plan objPln;
                        List<Plan> lstp;
                        lstp = new List<Plan>();
                        if (LstMachineBar[i].ObjMachine.LstPlan != null && LstMachineBar[i].ObjMachine.LstPlan.Count > 0)
                        {

                            #region More than one Plan
                            if (LstMachineBar[i].ObjMachine.LstPlan.Count > 1)
                            {

                            } 
                            #endregion

                            #region One Plan
                            else
                            {
                                foreach (Plan objPlan in LstMachineBar[i].ObjMachine.LstPlan)
                                {
                                    Vertical v = new Vertical();
                                    v.MCCode = LstMachineBar[i].ObjMachine.MCName;
                                    v.MCID = LstMachineBar[i].ObjMachine.MCID;
                                    Vertical vv = lstVertical.Find(x => x.MCID == v.MCID);

                                    Horizontal hr = new Horizontal();
                                    hr.Hrs = objPlan.From;
                                    Horizontal hrr = lstHorizontal.Find(x => x.Hrs == hr.Hrs);

                                    Horizontal too = new Horizontal();
                                    too.Hrs = objPlan.To;
                                    Horizontal tooo = lstHorizontal.Find(x => x.Hrs == too.Hrs);

                                    int width = tooo.X - hrr.X;

                                    objPln = new Plan();
                                    objPln.From = objPlan.From;
                                    objPln.To = objPlan.To;

                                    objPln.XCor = hrr.X;
                                    objPln.YCor = vv.Y;


                                    objPln.W = width;
                                    objPln.H = mcHeight;

                                    lstp.Add(objPln);
                                   

                                    if ((hrr.X >= 100) && vv.Y >= 100)
                                    {
                                        Rectangle rc = new Rectangle(hrr.X, vv.Y, width, mcHeight);
                                        LinearGradientBrush l = new LinearGradientBrush(rc, Color.Blue, Color.White, 1, true);
                                        e.Graphics.FillRectangle(l, rc);
                                    }
                                }
                            } 
                            #endregion
                        } 
                #endregion

                        //--===================================================


                        yCor = yCor + barHeight + 10;

                    }
                }


            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public static DateTime roundDateTime(DateTime inDate)
        {
            int minutePlan = inDate.Minute / 30;
            string date;
            if (minutePlan > 0)
            {
                if (inDate.Hour != 23)
                {
                    date = inDate.ToString("yyyy-MM-dd ") + Convert.ToString(inDate.Hour + 1) + ":00";
                }
                else
                {
                    date = inDate.AddDays(1).ToString("yyyy-MM-dd ") + "00:00";
                }
            }
            else
            {
                date = inDate.ToString("yyyy-MM-dd ") + Convert.ToString(inDate.Hour) + ":00";

            }
            return Convert.ToDateTime(date);
        }
        
        public static bool GetDrawingRectangleConsiderBoth(int minX, int minY, ref Rectangle inRect)
        {
            bool canDraw = true;
            if (minX > inRect.X)
            {
                inRect.Width = inRect.Width - (minX - inRect.X);
                inRect.X = minX;

                if (inRect.Width <= 0)
                    canDraw = false;
            }

            if (minY > inRect.Y)
            {
                inRect.Height = inRect.Height - (minY - inRect.Y);
                inRect.Y = minY;

                if (inRect.Height <= 0)
                    canDraw = false;
            }

            return canDraw;
        }

        public static bool GetDrawingRectangleConsiderX(int minX, ref Rectangle inRect)
        {
            bool canDraw = true;
            if (minX > inRect.X)
            {
                inRect.Width = inRect.Width - (minX - inRect.X);
                inRect.X = minX;

                if (inRect.Width <= 0)
                    canDraw = false;
            }
            return canDraw;
        }

        public static bool GetDrawingRectangleConsiderY(int minY, ref Rectangle inRect)
        {
            bool canDraw = true;
            if (minY > inRect.Y)
            {
                inRect.Height = inRect.Height - (minY - inRect.Y);
                inRect.Y = minY;
                if (inRect.Height <= 0)
                    canDraw = false;
            }

            return canDraw;
        }

        public static bool CheckPointConsiderBoth(int minX, int minY, Point p)
        {
            bool canDraw = false;
            if (minX >= p.X)
                canDraw = false;

            if (minY >= p.Y)
                canDraw = false;

            return canDraw;
        }

        public static bool CheckPointConsiderY(int minY, int pY)
        {
            bool canDraw = true;
            if (minY > pY)
                canDraw = false;
            return canDraw;

        }

        public static bool CheckPointConsiderX(int minX, int pX)
        {
            bool canDraw = true;
            if (minX > pX)
                canDraw = false;
            return canDraw;
        }

        private void setRectangleProperties(int x, int y, int pWidth, int pHeight)
        {
            drawRect.X = x;
            drawRect.Y = y;
            drawRect.Height = pHeight;
            drawRect.Width = pWidth;
        }

        private bool drawRectangleConsiderBoth(PaintEventArgs e, Color[] color, Rectangle rec)
        {
            bool isDraw = false;
            if (GetDrawingRectangleConsiderBoth(minX, minY, ref rec))
            {
                if (rec.Width > 0 && rec.Height > 0)
                {
                    Brush rectBrush = new LinearGradientBrush(rec, color[0], color[1], 0, false);
                    e.Graphics.FillRectangle(rectBrush, rec);
                    drawRect = rec;//for fill cordinates
                    isDraw = true;
                }

            }
            return isDraw;
        }

        private bool getRectangleConsiderBoth(Rectangle rec)
        {
            bool isDraw = false;
            if (GetDrawingRectangleConsiderBoth(minX, minY, ref rec))
            {
                if (rec.Width > 0 && rec.Height > 0)
                {
                    drawRect = rec;//for fill cordinates
                    isDraw = true;
                }
            }
            return isDraw;
        }

        private void drawRectangleConsiderY(PaintEventArgs e, Color[] color, Rectangle rec)
        {
            if (GetDrawingRectangleConsiderY(minY, ref rec))
            {
                if (rec.Width > 0 && rec.Height > 0)
                {
                    Brush rectBrush = new LinearGradientBrush(rec, color[0], color[1], 0, false);
                    e.Graphics.FillRectangle(rectBrush, rec);
                }
            }
        }

        private void drawRectangleConsiderX(PaintEventArgs e, Color[] color, Rectangle rec)
        {
            if (GetDrawingRectangleConsiderX(minX, ref rec))
            {
                if (rec.Width > 0 && rec.Height > 0)
                {
                    Brush rectBrush = new LinearGradientBrush(rec, color[0], color[1], 0, false);
                    e.Graphics.FillRectangle(rectBrush, rec);
                }
            }
        }

        private void ucGraphicMain_MouseDown(object sender, MouseEventArgs e)
        {
            #region Left
            isScrolling = true;
            scrollStartPoint = new Point(e.X - ScrollOffset.Width, e.Y - ScrollOffset.Height);
            Cursor = Cursors.Hand;
            #endregion
        }

        private void ucGraphicMain_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                DateTime dtFromDate = DateTime.Now;
                frmAddData objfrmAddData = new frmAddData(dtFromDate);
                objfrmAddData.Show();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static int[] get_StartXCordinate_Length_OfAPlan(DateTime showFrom, DateTime from, DateTime to)
        {
            if (from < showFrom)
            {
                from = showFrom;
            }
            int[] returnAry = new Int32[2];
            TimeSpan diff_ShwFrom_Start, diff_Start_To;

            from = roundDateTime(from);
            to = roundDateTime(to);
            diff_ShwFrom_Start = from.Subtract(showFrom);
            diff_Start_To = to.Subtract(from);
            returnAry[0] = Convert.ToInt32(diff_ShwFrom_Start.TotalHours);//Start possition
            returnAry[1] = Convert.ToInt32(diff_Start_To.TotalHours);//Width
            return returnAry;
        }
        public static int[] get_StartXCordinate_Length_OfAPlan(int showFrom, int from, int to)
        {
            if (from < showFrom)
            {
                from = showFrom;
            }
            int[] returnAry = new Int32[2];
            int diff_ShwFrom_Start, diff_Start_To;

          
            diff_ShwFrom_Start = from - showFrom;
            diff_Start_To = to- from;
            returnAry[0] = Convert.ToInt32(diff_ShwFrom_Start);//Start possition
            returnAry[1] = Convert.ToInt32(diff_Start_To);//Width
            return returnAry;
        }

        public static int get_StartXCordinate_OfDate(DateTime showFrom, DateTime from)
        {
            if (from < showFrom)
            {
                from = showFrom;
            }
            int xCor;
            TimeSpan diff_ShwFrom_Start;

            from = roundDateTime(from);
            diff_ShwFrom_Start = from.Subtract(showFrom);
            xCor = Convert.ToInt32(diff_ShwFrom_Start.TotalHours);//Start possition
            return xCor;
        }
        public static int get_StartXCordinate_OfDate(int showFrom, int from)
        {
            if (from < showFrom)
            {
                from = showFrom;
            }
            int xCor;

            xCor = from - showFrom;
            return xCor;
        }


        #region ScrollEventType to SB_* messages and SB_* messages to ScrollEventType

        private const int SB_LINEUP = 0;
        private const int SB_LINEDOWN = 1;
        private const int SB_PAGEUP = 2;
        private const int SB_PAGEDOWN = 3;
        private const int SB_THUMBPOSITION = 4;
        private const int SB_THUMBTRACK = 5;
        private const int SB_TOP = 6;
        private const int SB_BOTTOM = 7;
        private const int SB_ENDSCROLL = 8;

        private int getSBFromScrollEventType(ScrollEventType type)
        {
            int result = -1;
            switch (type)
            {
                case ScrollEventType.SmallDecrement:
                    result = SB_LINEUP;
                    break;
                case ScrollEventType.SmallIncrement:
                    result = SB_LINEDOWN;
                    break;
                case ScrollEventType.LargeDecrement:
                    result = SB_PAGEUP;
                    break;
                case ScrollEventType.LargeIncrement:
                    result = SB_PAGEDOWN;
                    break;
                case ScrollEventType.ThumbTrack:
                    result = SB_THUMBTRACK;
                    break;
                case ScrollEventType.First:
                    result = SB_TOP;
                    break;
                case ScrollEventType.Last:
                    result = SB_BOTTOM;
                    break;
                case ScrollEventType.ThumbPosition:
                    result = SB_THUMBPOSITION;
                    break;
                case ScrollEventType.EndScroll:
                    result = SB_ENDSCROLL;
                    break;
                default:
                    break;
            }
            return result;
        }

        private ScrollEventType getScrollEventType(System.IntPtr wParam)
        {
            ScrollEventType result = 0;
            switch (LoWord((int)wParam))
            {
                case SB_LINEUP:
                    result = ScrollEventType.SmallDecrement;
                    break;
                case SB_LINEDOWN:
                    result = ScrollEventType.SmallIncrement;
                    break;
                case SB_PAGEUP:
                    result = ScrollEventType.LargeDecrement;
                    break;
                case SB_PAGEDOWN:
                    result = ScrollEventType.LargeIncrement;
                    break;
                case SB_THUMBTRACK:
                    result = ScrollEventType.ThumbTrack;
                    break;
                case SB_TOP:
                    result = ScrollEventType.First;
                    break;
                case SB_BOTTOM:
                    result = ScrollEventType.Last;
                    break;
                case SB_THUMBPOSITION:
                    result = ScrollEventType.ThumbPosition;
                    break;
                case SB_ENDSCROLL:
                    result = ScrollEventType.EndScroll;
                    break;
                default:
                    result = ScrollEventType.EndScroll;
                    break;
            }
            return result;
        }

        #endregion

        #region WndProd override

        private const int WM_HSCROLL = 0x114;
        private const int WM_VSCROLL = 0x115;

        protected override void WndProc(ref Message msg)
        {
            if (msg.HWnd == this.Handle)
            {
                switch (msg.Msg)
                {

                    case WM_VSCROLL:
                        if (msg.LParam != IntPtr.Zero)
                        {
                            break; //do the base.WndProc
                        }
                        try
                        {
                            int oldVscrollPos = VerticalScroll.Value;
                            int newVscrollPos = oldVscrollPos;
                            ScrollEventType type = getScrollEventType(msg.WParam);
                            switch (type)
                            {
                                case ScrollEventType.SmallDecrement:
                                    newVscrollPos = oldVscrollPos - VerticalScroll.SmallChange;
                                    break;
                                case ScrollEventType.SmallIncrement:
                                    newVscrollPos = oldVscrollPos + VerticalScroll.SmallChange;
                                    break;
                                case ScrollEventType.LargeDecrement:
                                    newVscrollPos = oldVscrollPos - VerticalScroll.LargeChange;
                                    break;
                                case ScrollEventType.LargeIncrement:
                                    newVscrollPos = oldVscrollPos + VerticalScroll.LargeChange;
                                    break;
                                case ScrollEventType.First:
                                    newVscrollPos = VerticalScroll.Minimum;
                                    break;
                                case ScrollEventType.Last:
                                    newVscrollPos = VerticalScroll.Maximum;
                                    break;
                                case ScrollEventType.ThumbTrack:
                                    newVscrollPos = HiWord((int)msg.WParam);
                                    break;
                                case ScrollEventType.ThumbPosition:
                                    newVscrollPos = HiWord((int)msg.WParam);
                                    break;
                            }
                            if (newVscrollPos < VerticalScroll.Minimum)
                            {
                                newVscrollPos = VerticalScroll.Minimum;
                            }
                            if (newVscrollPos > VerticalScroll.Maximum)
                            {
                                newVscrollPos = VerticalScroll.Maximum;
                            }
                            if (oldVscrollPos != newVscrollPos)
                            {
                                VerticalScroll.Value = newVscrollPos;
                                if (type != ScrollEventType.EndScroll)
                                {
                                    ScrollEventArgs arg = new ScrollEventArgs(type, oldVscrollPos, newVscrollPos, ScrollOrientation.VerticalScroll);
                                    this.OnScroll(arg);
                                    Invalidate();
                                }
                            }
                        }
                        catch (Exception)
                        {
                        }
                        return;

                    case WM_HSCROLL:
                        if (msg.LParam != IntPtr.Zero)
                        {
                            break; //do the base.WndProc
                        }
                        try
                        {
                            int oldHscrollPos = HorizontalScroll.Value;
                            int newHscrollPos = oldHscrollPos;
                            ScrollEventType type = getScrollEventType(msg.WParam);
                            switch (type)
                            {
                                case ScrollEventType.SmallDecrement:
                                    newHscrollPos = oldHscrollPos - HorizontalScroll.SmallChange;
                                    break;
                                case ScrollEventType.SmallIncrement:
                                    newHscrollPos = oldHscrollPos + HorizontalScroll.SmallChange;
                                    break;
                                case ScrollEventType.LargeDecrement:
                                    newHscrollPos = oldHscrollPos - HorizontalScroll.LargeChange;
                                    break;
                                case ScrollEventType.LargeIncrement:
                                    newHscrollPos = oldHscrollPos + HorizontalScroll.LargeChange;
                                    break;
                                case ScrollEventType.First:
                                    newHscrollPos = HorizontalScroll.Minimum;
                                    break;
                                case ScrollEventType.Last:
                                    newHscrollPos = HorizontalScroll.Maximum;
                                    break;
                                case ScrollEventType.ThumbTrack:
                                    newHscrollPos = HiWord((int)msg.WParam);
                                    break;
                                case ScrollEventType.ThumbPosition:
                                    newHscrollPos = HiWord((int)msg.WParam);
                                    break;
                            }
                            if (newHscrollPos < HorizontalScroll.Minimum)
                            {
                                newHscrollPos = HorizontalScroll.Minimum;
                            }
                            if (newHscrollPos > HorizontalScroll.Maximum)
                            {
                                newHscrollPos = HorizontalScroll.Maximum;
                            }
                            if (oldHscrollPos != newHscrollPos)
                            {
                                HorizontalScroll.Value = newHscrollPos;
                                ScrollEventArgs arg = new ScrollEventArgs(type, oldHscrollPos, newHscrollPos, ScrollOrientation.HorizontalScroll);
                                if (type != ScrollEventType.EndScroll)
                                {
                                    this.OnScroll(arg);
                                    Invalidate();
                                }
                            }
                        }
                        catch (Exception)
                        {
                        }
                        return;

                    default:
                        break;
                }
            }
            base.WndProc(ref msg);
        }

        #endregion

        #region API32 functions

        static int MakeLong(int LoWord, int HiWord)
        {
            return (HiWord << 16) | (LoWord & 0xffff);
        }

        static IntPtr MakeLParam(int LoWord, int HiWord)
        {
            return (IntPtr)((HiWord << 16) | (LoWord & 0xffff));
        }

        static int HiWord(int number)
        {
            if ((number & 0x80000000) == 0x80000000)
                return (number >> 16);
            else
                return (number >> 16) & 0xffff;
        }

        static int LoWord(int number)
        {
            return number & 0xffff;
        }

        #endregion

        private void ucGraphicMain_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                if (isScrolling)
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
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        private void ucGraphicMain_MouseUp(object sender, MouseEventArgs e)
        {
            isScrolling = false;
            Cursor = Cursors.Default;
        }

    }
}
