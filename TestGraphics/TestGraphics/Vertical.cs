using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestGraphics
{
   public class Vertical
    {
        private int mCID;

        public int MCID
        {
            get { return mCID; }
            set { mCID = value; }
        }

        private string mCCode;

        public string MCCode
        {
            get { return mCCode; }
            set { mCCode = value; }
        }

        public decimal Capacity { get; set; }

        private int x;

        public int X
        {
            get { return x; }
            set { x = value; }
        }

        private int y;

        public int Y
        {
            get { return y; }
            set { y = value; }
        }

        public int IndexOfCurrentMachine { get; set; }
    }
}
