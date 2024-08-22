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
    public partial class ctrlShowTeamInfo : UserControl
    {
        private int _TeamID = -1;
        public ctrlShowTeamInfo()
        {
            InitializeComponent();
        }

        public void LoadTeamInfo(int TeamID)
        {
            _TeamID = TeamID;
            clsTeam Team = new clsTeam();
            Team = clsTeam.Find(TeamID);

            if (Team != null) 
            {
                lblTeamName.Text = Team.TeamName;
                lblPlayersCount.Text = Team.PlayersCount.ToString();
                lblChampionshipsCount.Text = Team.ChampionshipsCount.ToString();
                lblCreatedDate.Text = Team.CreatedDate.ToShortDateString();
                lblCreatedByUserID.Text = Team.CreatedByUserID.ToString();

                if(Team.CoachID.HasValue)
                    lblCoachName.Text = Team.CoachInfo.PersonInfo.FullName;
                else
                    lblCoachName.Text = "No Coach";

                if (lblNote.Text == "")
                    lblNote.Text = "???";
                else
                    lblNote.Text = Team.TeamDescription;

                linlblEditTeamInfo.Enabled = true;
            }
        }

        private void linlblEditTeamInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form frm = new frmAddUpdateTeam(_TeamID);
            frm.ShowDialog();
            LoadTeamInfo(_TeamID);
        }
    }
}
