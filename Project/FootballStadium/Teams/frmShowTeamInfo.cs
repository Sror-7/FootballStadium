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
    public partial class frmShowTeamInfo : Form
    {
        private int _TeamID = -1;
        public frmShowTeamInfo(int TeamID)
        {
            _TeamID = TeamID;
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmShowTeamInfo_Load(object sender, EventArgs e)
        {
            ctrlShowTeamInfo1.LoadTeamInfo(_TeamID);
        }
    }
}
