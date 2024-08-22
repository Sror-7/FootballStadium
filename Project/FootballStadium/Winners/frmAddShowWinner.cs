using FootballStadium.Global_Classes;
using FootballStadium_Business;
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
    public partial class frmAddShowWinner : Form
    {
        private clsWinner _Winner = new clsWinner();
        private int _TeamID = -1, _ChampionshipID = -1;
        private clsChampionship _Championship;
        public frmAddShowWinner(int TeamID, int ChampionshipID)
        {
            _TeamID = TeamID;
            _ChampionshipID = ChampionshipID;
            InitializeComponent();
        }

        private void btnWin_Click(object sender, EventArgs e)
        {
            if(_Championship.StartDate >= DateTime.Now)
            {
                MessageBox.Show("Championship not started yet! cannot make a team winner", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            _Winner.TeamID = _TeamID;
            _Winner.ChampionshipID = _ChampionshipID;
            _Winner.Note = richTextBox1.Text;
            _Winner.CreatedByUserID = clsGlobal.CurrentUser.UserID;

            if (_Winner.Save())
            {
                btnWin.Visible = false;
                richTextBox1.Visible = false;
                lblNote.Visible = false;
                MessageBox.Show("Team is winner!", "Congratulations", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }



        }

        private void frmAddWinner_Load(object sender, EventArgs e)
        {
            clsTeam Team = clsTeam.Find(_TeamID);
            _Championship = clsChampionship.Find(_ChampionshipID);

            lblTitle.Text = "The Winner of [ ";
            lblTitle.Text += _Championship.Name;
            lblTitle.Text += " ] Championship is";
            lblTeamName.Text = Team.TeamName;

            if(_Championship.IsEndet)
            {
                btnWin.Visible = false;
                lblNote.Visible = false;
                richTextBox1.Visible = false;
            }
        }
    }
}
