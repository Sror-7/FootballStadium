using FootballStadium.Global_Classes;
using FootballStadium_Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FootballStadium
{
    public partial class frmAddUpdatePlayer : Form
    {
        enum enMode { AddNew = 1, Update = 2}
        enMode Mode = enMode.AddNew;


        private clsPlayer _Player = new clsPlayer();
        private clsPlayer _LastInfomrationOfPlayer = new clsPlayer();
        private int _TeamID = -1;
        public frmAddUpdatePlayer(int TeamID)
        {
            _TeamID = TeamID;
            InitializeComponent();
        }
        public frmAddUpdatePlayer(clsPlayer Player)
        {
            _LastInfomrationOfPlayer = Player;
            _TeamID = _LastInfomrationOfPlayer.TeamID;
            Mode = enMode.Update;

            InitializeComponent();
        }

        private void frmAddNewPlayerOrCoach_Load(object sender, EventArgs e)
        {
            btnUpdateLastInfo.Visible = false;

            if (Mode == enMode.AddNew)
            {
                _Player.TeamInfo = clsTeam.Find(_TeamID);
                lblTeamName.Text = _Player.TeamInfo.TeamName;
                lblPlayersCount.Text = _Player.TeamInfo.PlayersCount.ToString();
            }
            else
            {
                lblTeamName.Text = _LastInfomrationOfPlayer.TeamInfo.TeamName;
                lblPlayersCount.Text = _LastInfomrationOfPlayer.TeamInfo.PlayersCount.ToString();
                btnUpdateLastInfo_Click(null, null);
            }

        }
        private void _RestartToolsEmpty()
        {
            lblTitle.Text = "Add New Player";
            btnUpdateLastInfo.Visible = true;
            lblPlayersCount.Text = _Player.TeamInfo.PlayersCount.ToString();

            _LastInfomrationOfPlayer = _Player;
            _Player = new clsPlayer();
            _Player.TeamInfo = _LastInfomrationOfPlayer.TeamInfo;

            txtFirstName.Text = "";
            txtLastName.Text = "";
            dtpDateOfBirth.Value = DateTime.Now.AddYears(-18);
            txtPhone.Text = "";
            richtxtAddress.Text = "";
            txtPlayerNumber.Text = "";

            Mode = enMode.AddNew;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some fields are not valid!", "Validation Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (_Player.TeamInfo.PlayersCount >= 17 && Mode == enMode.AddNew)
            {
                MessageBox.Show("Cannot add another player!", "Team is full", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _Player.PersonInfo.FirstName = txtFirstName.Text;
            _Player.PersonInfo.LastName = txtLastName.Text;
            _Player.PersonInfo.DateOfBirth = dtpDateOfBirth.Value;
            _Player.PersonInfo.Phone = txtPhone.Text;
            _Player.PersonInfo.Address = richtxtAddress.Text;
            _Player.TeamID = _TeamID;
            _Player.PlayerNumber = int.Parse(txtPlayerNumber.Text);
            _Player.CreatedDate = DateTime.Now;
            _Player.CreatedByUserID = clsGlobal.CurrentUser.UserID;

            if (_Player.Save())
            {
                _RestartToolsEmpty();
            }
            else
                MessageBox.Show("Player add faild!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            return;
        }

        private void btnUpdateLastInfo_Click(object sender, EventArgs e)
        {
            _Player = _LastInfomrationOfPlayer;
            txtPlayerNumber.Text = _Player.PlayerNumber.ToString();
            txtFirstName.Text = _Player.PersonInfo.FirstName;
            txtLastName.Text = _Player.PersonInfo.LastName;
            dtpDateOfBirth.Value = _Player.PersonInfo.DateOfBirth;
            txtPhone.Text = _Player.PersonInfo.Phone;
            richtxtAddress.Text = _Player.PersonInfo.Address;


            lblTitle.Text = "Update Player Info";
        }
        private void txtPlayerNumber_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtPlayerNumber.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtPlayerNumber, "Error: Write Player's number.");
                return;
            }
            else
                errorProvider1.SetError(txtPlayerNumber, null);
        }

        private void txtFirstName_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtFirstName.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFirstName, "Error: Write First name.");
                return;
            }
            else
                errorProvider1.SetError(txtFirstName, null);
        }

        private void txtLastName_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtLastName.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtLastName, "Error: Write Last name.");
                return;
            }
            else
                errorProvider1.SetError(txtLastName, null);
        }

        private void txtPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtPlayerNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
