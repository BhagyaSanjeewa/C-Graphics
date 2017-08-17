using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestGraphics
{
   public class InterruptionInGrahic
    {
        private DateTime dtOccurDate;
        private DateTime dtCompleted;
        private bool isCompleted;
        private string strInterruptionType;
        private string strInterruptionTypeDetails;
        private int topXCoordinate;
        private int topYCoordinate;
        private int width;
        private int height;

        public int TopXCoordinate
        {
            get { return topXCoordinate; }
            set { topXCoordinate = value; }
        }
        public int TopYCoordinate
        {
            get { return topYCoordinate; }
            set { topYCoordinate = value; }
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
        public DateTime DtOccurDate
        {
            get { return dtOccurDate; }
            set { dtOccurDate = value; }
        }
        public DateTime DtCompleted
        {
            get { return dtCompleted; }
            set { dtCompleted = value; }
        }
        public bool IsCompleted
        {
            get { return isCompleted; }
            set { isCompleted = value; }
        }
        public string StrInterruptionType
        {
            get { return strInterruptionType; }
            set { strInterruptionType = value; }
        }
        public string StrInterruptionTypeDetails
        {
            get { return strInterruptionTypeDetails; }
            set { strInterruptionTypeDetails = value; }
        }
    }
}
