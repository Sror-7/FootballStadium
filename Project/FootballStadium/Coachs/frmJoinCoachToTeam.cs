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
    public partial class frmJoinCoachToTeam : Form
    {
        private int _CoachID, _TeamID;
        public frmJoinCoachToTeam(int CoachID)
        {
            _CoachID = CoachID;
            InitializeComponent();
        }

        private void _FillTeamsinComboBox()
        {
            DataTable dt = clsTeam.GetTeamsHasNoCoachs();

            foreach (DataRow row in dt.Rows) 
            {
                cbTeams.Items.Add(row["TeamName"]);
            }
        }
        private void frmJoinCoachToTeam_Load(object sender, EventArgs e)
        {
            ctrlShowCoachInfo1.LoadCoachInfo(_CoachID);
            _FillTeamsinComboBox();
        }

        private void btnJoin_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show($"Are you sure you want to join Coach with ID [ {_CoachID} ] in Team With ID [ {_TeamID} ] ?", "Confirm Join", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
            {
                if (clsTeam.DoesTeamHaveCoach(_TeamID))
                {
                    MessageBox.Show("Error: Team already has a coach cannot add another one.", "Note", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                clsTeam Team = clsTeam.Find(_TeamID);
                Team.CoachID = _CoachID;
                if (Team.Save())
                {
                    MessageBox.Show("Coach Joined Team successfully!", "Joined", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnJoin.Enabled = false;
                    cbTeams.Enabled = false;
                    ctrlShowTeamInfo1.LoadTeamInfo(_TeamID);
                    ctrlShowCoachInfo1.LoadCoachInfo(_CoachID);
                    return;
                }
                else
                {
                    MessageBox.Show("Coach Joined Team Faild!", "Faild", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cbTeams_SelectedIndexChanged(object sender, EventArgs e)
        {
            _TeamID = clsTeam.GetTeamIDByTeamName(cbTeams.Text);
            ctrlShowTeamInfo1.LoadTeamInfo(_TeamID);

            btnJoin.Enabled = true;
        }
    }
}
