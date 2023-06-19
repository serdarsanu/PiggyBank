using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PiggyBank
{
    public static class CustomFunction
    {
        public static void clearSelectedMoney(this Control.ControlCollection Controllers)
        {
            foreach (Control control in Controllers)
            {
                if (control is Button)
                {
                    Button btn = (Button)control;
                    
                    if (btn.Name.Contains("tl") || btn.Name.Contains("kr")) {
                        btn.Text = null;
                    }
                }
                else if (control is GroupBox)
                {
                    GroupBox grb = (GroupBox)control;
                    clearSelectedMoney(grb.Controls);
                }

            }
        }
    }
}
