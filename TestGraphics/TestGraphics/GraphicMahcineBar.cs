using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestGraphics
{
    public class GraphicMahcineBar
    {
        private Machine objMachine;
        private int topYcordinate;//Top y coordinate
        private int topXCoordinate;//Top x coordinate
        private int height;//Howing height
        private int devider;
        private bool isSelected;
        private bool isNewlyAdded;
        private List<GraphicalPlan> listPlan = new List<GraphicalPlan>();//List of plan
        private List<InterruptionInGrahic> listInterruption;

        public Machine ObjMachine
        {
            get { return objMachine; }
            set { objMachine = value; }
        }
        public int TopYcordinate
        {
            get { return topYcordinate; }
            set { topYcordinate = value; }
        }
        public int Height
        {
            get { return height; }
            set { height = value; }
        }
        public int Devider
        {
            get { return devider; }
            set { devider = value; }
        }
        public bool IsSelected
        {
            get { return isSelected; }
            set { isSelected = value; }
        }
        public bool IsNewlyAdded
        {
            get { return isNewlyAdded; }
            set { isNewlyAdded = value; }
        }
        public List<GraphicalPlan> ListPlan
        {
            get { return listPlan; }
            set { listPlan = value; }
        }
        public List<InterruptionInGrahic> ListInterruption
        {
            get { return listInterruption; }
            set { listInterruption = value; }
        }
        public int TopXCoordinate
        {
            get { return topXCoordinate; }
            set { topXCoordinate = value; }
        }

    }
}
