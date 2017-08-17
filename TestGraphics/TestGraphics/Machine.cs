using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestGraphics
{
    public class Machine
    {
        public int MCID { get; set; }
        public string MCName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Plan ObjPlan { get; set; }

        public List<Plan> LstPlan { get; set; }

        public int TopXCordinate { get; set; }
        public int TopYCordinate { get; set; }
        public decimal Capacity { get; set; }

        public int IndexOfCurrentMachine { get; set; }
    }
}
