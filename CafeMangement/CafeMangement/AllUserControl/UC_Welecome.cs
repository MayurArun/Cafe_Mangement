using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CafeMangement.AllUserControl
{
    public partial class UC_Welecome : UserControl
    {
        public UC_Welecome()
        {
            InitializeComponent();
        }

        int num = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if(num == 0)
            {
                labelBanner.Location = new Point(94, 367);
                labelBanner.ForeColor = Color.Orange;
                num++;
            }
            else if(num == 1)
            {
                labelBanner.Location = new Point(166, 367);
                labelBanner.ForeColor = Color.Blue;
                num++;
            }
            else if(num == 2)
            {
                labelBanner.Location = new Point(268, 367);
                labelBanner.ForeColor = Color.PaleVioletRed;
                num = 0;
            }
        }

        private void UC_Welecome_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }
    }
}
