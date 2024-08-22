using FootballStadium.Global_Classes;
using FootballStadium_Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static FootballStadium_Business.clsUser;

namespace FootballStadium
{
    public partial class frmAddTeamsToChampionship : Form
    {
        private int _ChampionshipID = -1;
        public frmAddTeamsToChampionship(int ChampionshipID)
        {
            _ChampionshipID = ChampionshipID;
            InitializeComponent();
        }

        private void frmAddTeamsToChampionship_Load(object sender, EventArgs e)
        {
            clsChampionship Championship = clsChampionship.Find(_ChampionshipID);
            if(Championship != null)
            {
                lblChampionshipName.Text = Championship.Name;
            }
        }
        private void txtTeamName_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtTeamName.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtTeamName, "Error: Write team name.");
                return;
            }
            else
                errorProvider1.SetError(txtTeamName, null);

            if (!clsTeam.IsTeamExist(txtTeamName.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtTeamName, "Team isn't exist!");
                return;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(!this.ValidateChildren())
            {
                MessageBox.Show("Error: Some Values are validate.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int TeamID = clsTeam.GetTeamIDByTeamName(txtTeamName.Text);
            if (clsChampionship.IsTeamExistInChampionship(TeamID, _ChampionshipID))
            {
                MessageBox.Show("Error: Team Already enrolled in Championship.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (clsChampionship.AddTeamToChampionship(TeamID, _ChampionshipID, DateTime.Now, clsGlobal.CurrentUser.UserID) != -1)
            {
                clsChampionship.IncrementTeamsCount(_ChampionshipID);

                txtTeamName.Text = "";
                MessageBox.Show("Data Saved Successfully!", "Succeded", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                MessageBox.Show("Error: Data Saved Faild!", "Faild", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
