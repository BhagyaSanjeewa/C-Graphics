using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TestGraphics
{
    class HelpForm : Form
    {
        private static HelpForm _instance;
        public static HelpForm GetInstance()
        {
            if (_instance == null) _instance = new HelpForm();
            return _instance;
        }

    }
}
