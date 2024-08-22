using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FootballStadium
{
    public partial class frmShowCoachInfo : Form
    {
        private int _CoachID = -1;

        public frmShowCoachInfo(int CoachID)
        {
            _CoachID = CoachID;
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmShowCoachInfo_Load(object sender, EventArgs e)
        {
            ctrlShowCoachInfo1.LoadCoachInfo(_CoachID);
        }
    }
}
