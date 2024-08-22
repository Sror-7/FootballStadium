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
using static FootballStadium.frmAddUpdateTeam;

namespace FootballStadium
{
    public partial class frmAddUpdateTeam : Form
    {

        private clsUserPermission _UPermission = new clsUserPermission();
        enum enMode { AddNew = 1, Update = 2};
        enMode Mode = enMode.AddNew;

        private int _TeamID = -1;
        clsTeam _Team = new clsTeam();
        public frmAddUpdateTeam()
        {
            InitializeComponent();
        }
        public frmAddUpdateTeam(int TeamID)
        {
            _TeamID = TeamID;
            Mode = enMode.Update;
            InitializeComponent();
        }


       private void _ResetDefaultData()
        {
            if(Mode == enMode.AddNew)
            {
                lblTitle.Text = "Add New Team";
                this.Text = "Add New Team";
                btnAddUpdateTeam.Text = "Add Team";
            }
            else
            {
                btnAddCoachInfo.Enabled = true;
                btnAddPlayers.Enabled = true;
                lblTitle.Text = "Update Team";
                this.Text = "Update Team";
                btnAddUpdateTeam.Text = "Update Team";
            }



            txtTeamName.Text = "";
            nudChampionshipsCount.Value = 0;
            richtxtNotes.Text = "";
        }
        private void _GetCoachID(int CoachID)
        {
            btnAddCoachInfo.Text = "Update Coach Info";
            _Team.CoachID = CoachID;
            _Team.Save();
        }
        private void _LoadData()
        {
            _Team = clsTeam.Find(_TeamID);

            if(_Team == null)
            {
                MessageBox.Show($"No Team with ID [{_TeamID}]", "Team not found!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); 
                return;
            }
            txtTeamName.Text = _Team.TeamName;
            nudChampionshipsCount.Value = _Team.ChampionshipsCount;
            richtxtNotes.Text = _Team.TeamDescription;
            if(_Team.CoachID.HasValue)
            {
                btnAddCoachInfo.Text = "Update Coach Info";
            }
        }
        private void _UserPermission()
        {
            _UPermission = clsUserPermission.Find(clsGlobal.CurrentUser.UserID, "Coachs");
            btnAddCoachInfo.Enabled = _UPermission.Add;

            _UPermission = clsUserPermission.Find(clsGlobal.CurrentUser.UserID, "Players");
            btnAddPlayers.Enabled = _UPermission.Add;
        }
        private void frmAddNewTeam_Load(object sender, EventArgs e)
        {
            _ResetDefaultData();
            if (Mode == enMode.Update)
            {
                _UserPermission();
                _LoadData();
            }
        }
        private void _EnabledAddCoachPlayers()
        {
            _UserPermission();
        }
        private void btnAddUpdateTeam_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some fileds are not valide!",
                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _Team.TeamName = txtTeamName.Text;
            _Team.ChampionshipsCount = (int)nudChampionshipsCount.Value;
            _Team.TeamDescription = richtxtNotes.Text;
            _Team.CreatedDate = DateTime.Now;
            _Team.CreatedByUserID = clsGlobal.CurrentUser.UserID;

            if (Mode == enMode.AddNew)
            {
                _Team.PlayersCount = 0;
                _Team.CoachID = null;
            }

            if (_Team.Save())
            {

                Mode = enMode.Update;
                lblTitle.Text = "Update Team";
                this.Text = "Update Team";
                btnAddUpdateTeam.Text = "Update Team";

                _EnabledAddCoachPlayers();

                MessageBox.Show("Data Saved Successfully!", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Error: Data Is not Saved.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void btnAddCoachInfo_Click(object sender, EventArgs e)
        {
            if (_Team.DoesTeamHaveCoach())
            {
                Form frm = new frmAddUpdateCoach(_Team.CoachID.Value);
                frm.ShowDialog();
            }
            else
            {
                frmAddUpdateCoach frm = new frmAddUpdateCoach();
                frm.PassData += _GetCoachID;
                frm.ShowDialog();
            }
        }

        private void btnAddPlayers_Click(object sender, EventArgs e)
        {
            Form frm = new frmListPlayers(_Team.TeamID);
            frm.ShowDialog();
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
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

            if ((clsTeam.IsTeamExist(txtTeamName.Text.Trim()) && Mode != enMode.Update)
                || 
                (_Team.TeamName != txtTeamName.Text.Trim() && Mode == enMode.Update && (clsTeam.IsTeamExist(txtTeamName.Text.Trim()))))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtTeamName, "Team Name is used for another Team!");
                return;
            }
            else
                errorProvider1.SetError(txtTeamName, null);

        }

        
    }
}
