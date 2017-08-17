using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestGraphics
{
    public class GraphicalPlan
    {
        private string strJobNo;
        private string strInqCode;
        private string strSampleRefNo;
        private string strItemDescription;
        private string strItemCode;
        private string strRemarks;

        private int intJobID;
        private int intJobProccessForInqID;
        private int intMachinePlanID;
        private int intInquiryID;

        private DateTime planFrom;//Plan start date
        private DateTime planTo;//End date of plan
        private DateTime dtLastPoductionDate;
        private DateTime dtPredictedEndDate;
        private DateTime dtPredictedStartDate;
        private DateTime jobMinDate;//Minimum date of a plan.Use to avoid Overlapping plans
        private DateTime jobMaxDate;//Maximum date of a plan.Use to avoid Overlapping plans
        private DateTime dtActualStartday;
        private DateTime dtActualEndDate;
        private DateTime dtTapeSettingStartDate;
        private DateTime? dtProcessStartDate;
        private DateTime? dtProcessEndDate;

        private bool hasTapeSetting;
        private bool isNewPlan;
        private bool isProductionStarted;
        private bool isAlreadyLate;
        private bool isCorrectInterruption;
        private bool isActualEnd;
        private bool isShouldPredict;//If there is a remain quantity 
        private bool isJob;
        private bool isActualStarted;
        private bool isContinous;//if plan is continuous
        private bool isGoingToContinuous;//Need...
        private bool isDeleteProcessDate;

        private int topXcordinate;
        private int topXcordinateBeforeDrag;
        private int topYcordinate;
        private int showPossitionInBar;
        private int width;
        private int height;
        private int diffTopXandDragPos;
        private int diffTopYandDragPos;
        private int intNoOfTapes;
        private int intAssignMachineID;
        private int intActualNoOfTapes;
        private int intMachineActualID;
        private int intContinuousGroupNo;
        private int intContinuousSequenceNo;
        private int intSetNo;
        private int topXContinuousStart;
        private int widthOfUnproccessContinuous;
        private int prodPlanID;
        private int processID;

        private decimal decEfficiency;
        private decimal decMachineOutput;
        private decimal decProducedQty;
        private decimal decActualMCoutput;
        private decimal decAllocatedQuantity;
        private decimal decToBeProduced;
        private decimal decNetToBeProduced;
        private decimal decBoilingShrinkage;
        private decimal decWashingShrinkage;
        private decimal decDyeShrinkage;
        private decimal decProdcutionWastage;
        private decimal decWPM;
        private decimal decTransferQty;

        private List<GraphicalPlan> lstContinuousInGraphic;

        //private BeamDetailsForPlan objBeamDeatisForPlan;

        //public BeamDetailsForPlan ObjBeamDeatisForPlan
        //{
        //    get { return objBeamDeatisForPlan; }
        //    set { objBeamDeatisForPlan = value; }
        //}

        public string StrRemarks
        {
            get { return strRemarks; }
            set { strRemarks = value; }
        }
        public string StrInqCode
        {
            get { return strInqCode; }
            set { strInqCode = value; }
        }
        public string StrItemDescription
        {
            get { return strItemDescription; }
            set { strItemDescription = value; }
        }
        public string StrJobNo
        {
            get { return strJobNo; }
            set { strJobNo = value; }
        }
        public string StrSampleRefNo
        {
            get { return strSampleRefNo; }
            set { strSampleRefNo = value; }
        }
        public string StrItemCode
        {
            get { return strItemCode; }
            set { strItemCode = value; }
        }

        public DateTime PlanFrom
        {
            get { return planFrom; }
            set { planFrom = value; }
        }
        public DateTime PlanTo
        {
            get { return planTo; }
            set { planTo = value; }
        }
        public DateTime DtLastPoductionDate
        {
            get { return dtLastPoductionDate; }
            set { dtLastPoductionDate = value; }
        }
        public DateTime DtPredictedEndDate
        {
            get { return dtPredictedEndDate; }
            set { dtPredictedEndDate = value; }
        }
        public DateTime DtPredictedStartDate
        {
            get { return dtPredictedStartDate; }
            set { dtPredictedStartDate = value; }
        }
        public DateTime JobMaxDate
        {
            get { return jobMaxDate; }
            set { jobMaxDate = value; }
        }
        public DateTime JobMinDate
        {
            get { return jobMinDate; }
            set { jobMinDate = value; }
        }
        public DateTime DtActualStartday
        {
            get { return dtActualStartday; }
            set { dtActualStartday = value; }
        }
        public DateTime DtActualEndDate
        {
            get { return dtActualEndDate; }
            set { dtActualEndDate = value; }
        }
        public DateTime DtTapeSettingStartDate
        {
            get { return dtTapeSettingStartDate; }
            set { dtTapeSettingStartDate = value; }
        }
        public DateTime? DtProcessStartDate
        {
            get { return dtProcessStartDate; }
            set { dtProcessStartDate = value; }
        }
        public DateTime? DtProcessEndDate
        {
            get { return dtProcessEndDate; }
            set { dtProcessEndDate = value; }
        }

        public bool IsJob
        {
            get { return isJob; }
            set { isJob = value; }
        }
        public bool HasTapeSetting
        {
            get { return hasTapeSetting; }
            set { hasTapeSetting = value; }
        }
        public bool IsNewPlan
        {
            get { return isNewPlan; }
            set { isNewPlan = value; }
        }
        public bool IsProductionStarted
        {
            get { return isProductionStarted; }
            set { isProductionStarted = value; }
        }
        public bool IsAlreadyLate
        {
            get { return isAlreadyLate; }
            set { isAlreadyLate = value; }
        }
        public bool IsCorrectInterruption
        {
            get { return isCorrectInterruption; }
            set { isCorrectInterruption = value; }
        }
        public bool IsActualStarted
        {
            get { return isActualStarted; }
            set { isActualStarted = value; }
        }
        public bool IsActualEnd
        {
            get { return isActualEnd; }
            set { isActualEnd = value; }
        }
        public bool IsShouldPredict
        {
            get { return isShouldPredict; }
            set { isShouldPredict = value; }
        }
        public bool IsContinous
        {
            get { return isContinous; }
            set { isContinous = value; }
        }
        public bool IsDeleteProcessDate
        {
            get { return isDeleteProcessDate; }
            set { isDeleteProcessDate = value; }
        }
        //public bool IsGoingToContinuous
        //{
        //    get { return isGoingToContinuous; }
        //    set { isGoingToContinuous = value; }
        //}


        public int TopXcordinate
        {
            get { return topXcordinate; }
            set { topXcordinate = value; }
        }
        public int TopXcordinateBeforeDrag
        {
            get { return topXcordinateBeforeDrag; }
            set { topXcordinateBeforeDrag = value; }
        }
        public int TopYcordinate
        {
            get { return topYcordinate; }
            set { topYcordinate = value; }
        }
        public int ShowPossitionInBar
        {
            get { return showPossitionInBar; }
            set { showPossitionInBar = value; }
        }
        public int Width
        {
            get { return width; }
            set { width = value; }
        }
        public int Height
        {
            get { return height; }
            set { height = value; }
        }
        public int DiffTopXandDragPos
        {
            get { return diffTopXandDragPos; }
            set { diffTopXandDragPos = value; }
        }
        public int DiffTopYandDragPos
        {
            get { return diffTopYandDragPos; }
            set { diffTopYandDragPos = value; }
        }
        public int IntNoOfTapes
        {
            get { return intNoOfTapes; }
            set { intNoOfTapes = value; }
        }
        public int IntAssignMachineID
        {
            get { return intAssignMachineID; }
            set { intAssignMachineID = value; }
        }
        public int IntActualNoOfTapes
        {
            get { return intActualNoOfTapes; }
            set { intActualNoOfTapes = value; }
        }
        public int IntMachinePlanID
        {
            get { return intMachinePlanID; }
            set { intMachinePlanID = value; }
        }
        public int IntMachineActualID
        {
            get { return intMachineActualID; }
            set { intMachineActualID = value; }
        }
        public int IntContinuousGroupNo
        {
            get { return intContinuousGroupNo; }
            set { intContinuousGroupNo = value; }
        }
        public int IntContinuousSequenceNo
        {
            get { return intContinuousSequenceNo; }
            set { intContinuousSequenceNo = value; }
        }
        public int IntSetNo
        {
            get { return intSetNo; }
            set { intSetNo = value; }
        }
        public int IntJobID
        {
            get { return intJobID; }
            set { intJobID = value; }
        }
        public int TopXContinuousStart
        {
            get { return topXContinuousStart; }
            set { topXContinuousStart = value; }
        }
        public int WidthOfUnproccessContinuous
        {
            get { return widthOfUnproccessContinuous; }
            set { widthOfUnproccessContinuous = value; }
        }
        public int IntJobProccessForInqID
        {
            get { return intJobProccessForInqID; }
            set { intJobProccessForInqID = value; }
        }
        public int IntInquiryID
        {
            get { return intInquiryID; }
            set { intInquiryID = value; }
        }
        public int ProdPlanID
        {
            get { return prodPlanID; }
            set { prodPlanID = value; }
        }
        public int ProcessID
        {
            get { return processID; }
            set { processID = value; }
        }
        public decimal DecEfficiency
        {
            get { return decEfficiency; }
            set { decEfficiency = value; }
        }
        public decimal DecMachineOutput
        {
            get { return decMachineOutput; }
            set { decMachineOutput = value; }
        }
        public decimal DecProducedQty
        {
            get { return decProducedQty; }
            set { decProducedQty = value; }
        }
        public decimal DecActualMCoutput
        {
            get { return decActualMCoutput; }
            set { decActualMCoutput = value; }
        }
        public decimal DecAllocatedQuantity
        {
            get { return decAllocatedQuantity; }
            set { decAllocatedQuantity = value; }
        }
        public decimal DecWashingShrinkage
        {
            get { return decWashingShrinkage; }
            set { decWashingShrinkage = value; }
        }
        public decimal DecDyeShrinkage
        {
            get { return decDyeShrinkage; }
            set { decDyeShrinkage = value; }
        }
        public decimal DecBoilingShrinkage
        {
            get { return decBoilingShrinkage; }
            set { decBoilingShrinkage = value; }
        }
        public decimal DecProdcutionWastage
        {
            get { return decProdcutionWastage; }
            set { decProdcutionWastage = value; }
        }
        public decimal DecNetToBeProduced
        {
            get { return decNetToBeProduced; }
            set { decNetToBeProduced = value; }
        }
        public decimal DecToBeProduced
        {
            get { return decToBeProduced; }
            set { decToBeProduced = value; }
        }
        public decimal DecWPM
        {
            get { return decWPM; }
            set { decWPM = value; }
        }

        public List<GraphicalPlan> LstContinuousInGraphic
        {
            get { return lstContinuousInGraphic; }
            set { lstContinuousInGraphic = value; }
        }

        public Int64 intRowVersion { get; set; }

        public string strDeliverySchedule { get; set; }

        public decimal DecTransferQty
        {
            get { return decTransferQty; }
            set { decTransferQty = value; }
        }

    }
}
