using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TestGraphics
{
    public partial class frmTest : Form
    {
        public frmTest()
        {
            InitializeComponent();
        }

        private void frmTest_Load(object sender, EventArgs e)
        {
            ScrollProperties vScroll = ucGraphicMain1.VerticalScroll;           
            int scrollVal = vScroll.Value;           

            ScrollProperties hScroll = ucGraphicMain1.HorizontalScroll;
            ucGraphicMain1.HorizontalScroll.Visible = true;
            ucGraphicMain1.VerticalScroll.Visible = true;
            
        }


        private void ManualScrollPanel_Click(object sender, EventArgs e)
        {
            this.ucGraphicMain1.Focus();
        }

      

        private void ucGraphicMain1_Scroll(object sender, ScrollEventArgs e)
        {
            if (e.ScrollOrientation == ScrollOrientation.VerticalScroll)
            {
                String x = e.NewValue.ToString();
                return;
            }
            if (e.ScrollOrientation == ScrollOrientation.HorizontalScroll)
            {
                String y = e.NewValue.ToString();
                return;
            }
        }
    }
}
