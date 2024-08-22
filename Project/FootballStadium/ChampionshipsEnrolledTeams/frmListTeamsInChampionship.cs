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
    public partial class frmListTeamsInChampionship : Form
    {
        private int _ChampionshipID = -1;
        private DataTable _dtEnrolledTeamsInChampionship;
        public frmListTeamsInChampionship(int ChampionshipID)
        {
            _ChampionshipID = ChampionshipID;
            InitializeComponent();
        }
        private void _RefreshListTeamsInChampionships()
        {
            _dtEnrolledTeamsInChampionship = clsChampionship.GetAllEnrolledTeamsInChampionship(_ChampionshipID);
            dgvChampionshipTeams.DataSource = _dtEnrolledTeamsInChampionship;
            lblRecordsCount.Text = dgvChampionshipTeams.Rows.Count.ToString();
            //we will do cbfilter enabled false when data grid view has no data beacause when data grid view is empty and user want to filter data it will be a bug in system....
            if (dgvChampionshipTeams.Rows.Count > 0)
                cbFilterBy.Enabled = true;
            else
                cbFilterBy.Enabled = false;
        }
        private void _ResetData()
        {
            clsChampionship Championship = clsChampionship.Find(_ChampionshipID);

            if (Championship != null)
            {
                if (Championship.IsEndet)
                {
                    btnAdd.Enabled = false;
                    removeToolStripMenuItem.Enabled = false;
                    lblEndDate.Text = Championship.EndDate.ToShortDateString();
                    lblEndDate.Visible = true;
                    label2.Visible = true;
                    btnShowWinner.Visible = true;
                }
            }
        }
        private void _UserPermission()
        {

            clsUserPermission UserPermission = clsUserPermission.Find(clsGlobal.CurrentUser.UserID, "Championships");

            btnAdd.Enabled = UserPermission.Add;
            removeToolStripMenuItem.Enabled = UserPermission.Delete;

            UserPermission = clsUserPermission.Find(clsGlobal.CurrentUser.UserID, "Teams");
            showListTeamsToolStripMenuItem.Enabled = UserPermission.Show;

        }
        private void frmListTeamsInChampionship_Load(object sender, EventArgs e)
        {
            _UserPermission();
            _ResetData();


            _dtEnrolledTeamsInChampionship = clsChampionship.GetAllEnrolledTeamsInChampionship(_ChampionshipID);
            dgvChampionshipTeams.DataSource = _dtEnrolledTeamsInChampionship;
            cbFilterBy.SelectedIndex = 0;
            lblRecordsCount.Text = dgvChampionshipTeams.Rows.Count.ToString();

            if (dgvChampionshipTeams.Rows.Count > 0)
            {
                dgvChampionshipTeams.Columns[0].HeaderText = "Championship's Name";
                dgvChampionshipTeams.Columns[0].Width = 150;

                dgvChampionshipTeams.Columns[1].HeaderText = "TeamID";
                dgvChampionshipTeams.Columns[1].Width = 80;

                dgvChampionshipTeams.Columns[2].HeaderText = "Team Name";
                dgvChampionshipTeams.Columns[2].Width = 150;

                dgvChampionshipTeams.Columns[3].HeaderText = "Player Count";
                dgvChampionshipTeams.Columns[3].Width = 80;

                dgvChampionshipTeams.Columns[4].HeaderText = "Enrolled Date";
                dgvChampionshipTeams.Columns[4].Width = 100;
            }
            else
            {
                cbFilterBy.Enabled = false;
            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Form frm = new frmAddTeamsToChampionship(_ChampionshipID);
            frm.ShowDialog();
            _RefreshListTeamsInChampionships();
        }

        private void showListTeamsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmListTeams();
            frm.Show();
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int TeamID = (int)dgvChampionshipTeams.CurrentRow.Cells[1].Value;
            if (MessageBox.Show($"Are you sure you want to remove this team with ID [{TeamID}] from Championship?", "Confirm Remove", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {

                if (clsChampionship.RemoveTeamFromChampionship(TeamID, _ChampionshipID))
                {
                    clsChampionship.DecrementTeamsCount(_ChampionshipID);
                    _RefreshListTeamsInChampionships();
                    MessageBox.Show("Team Removed Successfully!", "Succeded", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                {
                    MessageBox.Show("Team Removed Faild!", "Faild", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Visible = (cbFilterBy.Text != "None" && cbFilterBy.Text != "Enrolled Team Date");
            dtpFilter.Visible = (cbFilterBy.Text == "Enrolled Team Date");
            if (txtFilterValue.Visible)
            {
                txtFilterValue.Text = "";
                txtFilterValue.Focus();
            }
            if (cbFilterBy.Text == "None")
            {
                _dtEnrolledTeamsInChampionship.DefaultView.RowFilter = "";
                lblRecordsCount.Text = dgvChampionshipTeams.Rows.Count.ToString();
                return;
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

                case "Players Count":
                    FilterColumn = "PlayersCount";
                    break;

                default:
                    FilterColumn = "None";
                    break;
            }

            //Reset the filters in case nothing selected or filter value conains nothing.
            if (txtFilterValue.Text.Trim() == "" || FilterColumn == "None")
            {
                _dtEnrolledTeamsInChampionship.DefaultView.RowFilter = "";
                lblRecordsCount.Text = dgvChampionshipTeams.Rows.Count.ToString();
                return;
            }

            if (FilterColumn == "TeamID" || FilterColumn == "PlayersCount")
                //in this case we deal with integer not string.

                _dtEnrolledTeamsInChampionship.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilterValue.Text.Trim());
            else
                _dtEnrolledTeamsInChampionship.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, txtFilterValue.Text.Trim());

            lblRecordsCount.Text = dgvChampionshipTeams.Rows.Count.ToString();
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilterBy.Text == "Team ID" || cbFilterBy.Text == "Players Count")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void dtpFilter_ValueChanged(object sender, EventArgs e)
        {
            if (cbFilterBy.Text == "Enrolled Team Date")
            {
                _dtEnrolledTeamsInChampionship.DefaultView.RowFilter = "EnrolledTeamDate  >= '" + dtpFilter.Value.Date + "' and  EnrolledTeamDate  <= '" +
                  dtpFilter.Value.Date + "'";

                lblRecordsCount.Text = dgvChampionshipTeams.Rows.Count.ToString();
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            int TeamID = (int)dgvChampionshipTeams.CurrentRow.Cells[1].Value;

            Form frm = new frmAddShowWinner(TeamID, _ChampionshipID);
            frm.ShowDialog();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            int TeamID = (int)dgvChampionshipTeams.CurrentRow.Cells[1].Value;

            if (MessageBox.Show($"Are you sure you want to make this team with ID [{TeamID}] winner in Championship?", "Win", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                Form frm = new frmAddShowWinner(TeamID, _ChampionshipID);
                frm.ShowDialog();

                clsChampionship Champion = clsChampionship.Find(_ChampionshipID);
                if (Champion.IsEndet)
                {
                    btnAdd.Enabled = false;
                    removeToolStripMenuItem.Enabled = false;
                }
            }
        }
    }
}
