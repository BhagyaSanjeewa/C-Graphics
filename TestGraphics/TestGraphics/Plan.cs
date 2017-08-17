using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestGraphics
{
   public class Plan
    {
        public int From { get; set; }
        public int To { get; set; }
        public int XCor { get; set; }
        public int YCor { get; set; }
        public int H { get; set; }
        public int W { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int SequenceNo { get; set; }
        public bool IsSelected { get; set; }
        public bool IsLocationChanged { get; set; }
        public int GroupID { get; set; }
        public int MCID { get; set; }
        public decimal AllocQty { get; set; }
        public int PlanID { get; set; }
        public bool IsAlreadyAddedPlan { get; set; }
        public string RowVersion { get; set; }
        public bool IsEditable { get; set; }
        public Byte[] PlanRowVersion { get; set; }
    }
}
