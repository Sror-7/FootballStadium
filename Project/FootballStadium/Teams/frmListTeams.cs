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
    public partial class frmListTeams : Form
    {
        private DataTable _dtTeams;

        public frmListTeams()
        {
            InitializeComponent();
        }

        private void _RefreshTeamsList()
        {
            _dtTeams = clsTeam.GetAllTeams();

            dgvTeams.DataSource = _dtTeams;
            lblRecords.Text = dgvTeams.Rows.Count.ToString();

            if (dgvTeams.Rows.Count > 0)
            {
                cbFilterBy.Enabled = true;
                cmsManageTeams.Enabled = true;
            }
            else
            {
                cbFilterBy.Enabled = false;
                cmsManageTeams.Enabled = false;
            }
        }
        private void _UserPermission()
        {

            clsUserPermission UserPermission = clsUserPermission.Find(clsGlobal.CurrentUser.UserID, "Teams");

            updateTeamToolStripMenuItem.Enabled = UserPermission.Edit;
            deleteTeamToolStripMenuItem.Enabled = UserPermission.Delete;
            btnAdd.Enabled = UserPermission.Add;
        }
        private void frmListTeams_Load(object sender, EventArgs e)
        {
            _UserPermission();
            _dtTeams = clsTeam.GetAllTeams();
            dgvTeams.DataSource = _dtTeams;
            cbFilterBy.SelectedIndex = 0;
            lblRecords.Text = dgvTeams.Rows.Count.ToString();

            if(dgvTeams.Rows.Count > 0)
            {
                dgvTeams.Columns[0].HeaderText = "Team ID";
                dgvTeams.Columns[0].Width = 80;
                
                dgvTeams.Columns[1].HeaderText = "Team Name";
                dgvTeams.Columns[1].Width = 160;

                dgvTeams.Columns[2].HeaderText = "Coach Name";
                dgvTeams.Columns[2].Width = 150;

                dgvTeams.Columns[3].HeaderText = "Players Count";
                dgvTeams.Columns[3].Width = 70;

                dgvTeams.Columns[4].HeaderText = "Championships Count";
                dgvTeams.Columns[4].Width = 70;

                dgvTeams.Columns[5].HeaderText = "Created Date";
                dgvTeams.Columns[5].Width = 70;

                dgvTeams.Columns[6].HeaderText = "Team Description";
                dgvTeams.Columns[6].Width = 280;
            }
            else
            {
                cbFilterBy.Enabled = false;
                cmsManageTeams.Enabled = false;
            }
        }

        private void showTeamsPlayersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int TeamID = (int)dgvTeams.CurrentRow.Cells[0].Value;

            Form frm = new frmListPlayers(TeamID);
            frm.ShowDialog();

            _RefreshTeamsList();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Form frm = new frmAddUpdateTeam();
            frm.ShowDialog();

            _RefreshTeamsList();
        }

        private void showCoachInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int TeamID = (int)dgvTeams.CurrentRow.Cells[0].Value;
            clsTeam Team = clsTeam.Find(TeamID);

            if (Team != null)
            {
                if (Team.CoachID.HasValue)
                {
                    Form frm = new frmShowCoachInfo(Team.CoachID.Value);
                    frm.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Team Doesn't have a Coach!", "No Coach available", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                _RefreshTeamsList();
            }
        }

        private void deleteTeamToolStripMenuItem_Click(object sender, EventArgs e)
        {

            int TeamID = (int)dgvTeams.CurrentRow.Cells[0].Value;
           
            if (MessageBox.Show("Are you sure you want to delete Team ( " + dgvTeams.CurrentRow.Cells[1].Value + " )", "Confirm Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
           
            {
           
                if (clsTeam.Delete(TeamID))
           
                {
           
                    MessageBox.Show("Delete Team Successfully!", "Deleted succeded", MessageBoxButtons.OK, MessageBoxIcon.Information);
           
                    _RefreshTeamsList();
           
                }
           
                else
           
                    MessageBox.Show("Delete Team faild! Team cannot be deleted because has realations in system.", "Deleted faild", MessageBoxButtons.OK, MessageBoxIcon.Error);
           
            }
            
        }

        private void updateTeamToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int TeamID = (int)dgvTeams.CurrentRow.Cells[0].Value;
            
            Form frm = new frmAddUpdateTeam(TeamID);
            frm.ShowDialog();
            _RefreshTeamsList();
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Visible = (cbFilterBy.Text != "None");

            if (txtFilterValue.Visible)
            {
                txtFilterValue.Text = "";
                txtFilterValue.Focus();
            }
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";
            //Map Selected Filter to real Column name 
            switch (cbFilterBy.Text)
            {
                case "Team ID":
                    FilterColumn = "TeamID";
                    break;

                case "Team Name":
                    FilterColumn = "TeamName";
                    break;

                case "Coach Name":
                    FilterColumn = "CoachName";
                    break;

                case "Championships Count":
                    FilterColumn = "ChampionshipsCount";
                    break;

                default:
                    FilterColumn = "None";
                    break;

            }

            //Reset the filters in case nothing selected or filter value conains nothing.
            if (txtFilterValue.Text.Trim() == "" || FilterColumn == "None")
            {
                _dtTeams.DefaultView.RowFilter = "";
                lblRecords.Text = dgvTeams.Rows.Count.ToString();
                return;
            }

            if (FilterColumn == "TeamID" || FilterColumn == "ChampionshipsCount")
                //in this case we deal with integer not string.

                _dtTeams.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilterValue.Text.Trim());
            else
                _dtTeams.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, txtFilterValue.Text.Trim());

            lblRecords.Text = dgvTeams.Rows.Count.ToString();

        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilterBy.Text == "Team ID" || cbFilterBy.Text == "Championships Count")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form frm = new frmShowTeamInfo((int)dgvTeams.CurrentRow.Cells[0].Value);
            frm.ShowDialog();

            _RefreshTeamsList();
        }
    }
}
