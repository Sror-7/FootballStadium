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
    public partial class frmShowPlayerInfo : Form
    {
        int _PlayerID = -1;
        public frmShowPlayerInfo(int PlayerID)
        {
            _PlayerID = PlayerID;
            InitializeComponent();
        }

        private void frmShowPlayerInfo_Load(object sender, EventArgs e)
        {
            ctrlShowPlayerInfo1.LoadPlayerInfo(_PlayerID);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
