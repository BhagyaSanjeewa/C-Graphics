using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestGraphics
{
   public class Horizontal
    {
        private int hrs;
        public int GroupID { get; set; }

        public DateTime CurrentDate { get; set; }
        public int Hrs
        {
            get { return hrs; }
            set { hrs = value; }
        }

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
    }
}
