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
    public partial class frmListCoachs : Form
    {
        private DataTable _dtAllCoachs;
        public frmListCoachs()
        {
            InitializeComponent();
        }

        private void _RefreshCoachsList()
        {
            _dtAllCoachs = clsCoach.GetAllCoachs();
            dgvCoachs.DataSource = _dtAllCoachs;
            lblRecords.Text = dgvCoachs.Rows.Count.ToString();

            if (dgvCoachs.Rows.Count > 0)
            {
                cbFilterBy.Enabled = true;
                contextMenuStrip1.Enabled = true;
            }

            else
            {
                cbFilterBy.Enabled = false;
                contextMenuStrip1.Enabled = false;
            }
        }
        private void _UserPermission()
        {

            clsUserPermission UserPermission = clsUserPermission.Find(clsGlobal.CurrentUser.UserID, "Coachs");

            editToolStripMenuItem.Enabled = UserPermission.Edit;
            deleteToolStripMenuItem.Enabled = UserPermission.Delete;
            removeFromTeamToolStripMenuItem.Enabled = UserPermission.Delete;
            tsmjoin.Enabled = UserPermission.Add;
        }
        private void frmListCoachs_Load(object sender, EventArgs e)
        {
            _UserPermission();

            _dtAllCoachs = clsCoach.GetAllCoachs();
            dgvCoachs.DataSource = _dtAllCoachs;
            cbFilterBy.SelectedIndex = 0;
            lblRecords.Text = dgvCoachs.Rows.Count.ToString();

            if (dgvCoachs.Rows.Count > 0)
            {
                dgvCoachs.Columns[0].HeaderText = "Coach ID";
                dgvCoachs.Columns[0].Width = 60;

                dgvCoachs.Columns[1].HeaderText = "Name";
                dgvCoachs.Columns[1].Width = 150;

                dgvCoachs.Columns[2].HeaderText = "Date Of Birth";
                dgvCoachs.Columns[2].Width = 80;

                dgvCoachs.Columns[3].HeaderText = "Years of Experience";
                dgvCoachs.Columns[3].Width = 60;

                dgvCoachs.Columns[4].HeaderText = "Championships Count";
                dgvCoachs.Columns[4].Width = 60;

                dgvCoachs.Columns[5].HeaderText = "Team Name";
                dgvCoachs.Columns[5].Width = 150;
            }
            else
            {
                cbFilterBy.Enabled = false;
                contextMenuStrip1.Enabled = false;
            }
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Visible = (cbFilterBy.Text != "None");

            if (txtFilterValue.Visible)
            {
                txtFilterValue.Text = "";
                txtFilterValue.Focus();
            }
            if(cbFilterBy.Text == "None")
            {
                _dtAllCoachs.DefaultView.RowFilter = "";
                lblRecords.Text = dgvCoachs.Rows.Count.ToString();
                return;
            }
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";
            //Map Selected Filter to real Column name 
            switch (cbFilterBy.Text)
            {
                case "Coach ID":
                    FilterColumn = "CoachID";
                    break;

                case "Team Name":
                    FilterColumn = "TeamName";
                    break;

                case "Name":
                    FilterColumn = "Name";
                    break;

                case "Years of Experience":
                    FilterColumn = "YearsOfExperience";
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
                _dtAllCoachs.DefaultView.RowFilter = "";
                lblRecords.Text = dgvCoachs.Rows.Count.ToString();
                return;
            }

            if (FilterColumn == "CoachID" || FilterColumn == "ChampionshipsCount" || FilterColumn == "YearsOfExperience")
                //in this case we deal with integer not string.

                _dtAllCoachs.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilterValue.Text.Trim());
            else
                _dtAllCoachs.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, txtFilterValue.Text.Trim());

            lblRecords.Text = dgvCoachs.Rows.Count.ToString();
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilterBy.Text == "Coach ID" || cbFilterBy.Text == "Championships Count" || cbFilterBy.Text == "Years of Experience")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Form frm = new frmAddUpdateCoach();
            frm.ShowDialog();

            _RefreshCoachsList();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {

            Form frm = new frmAddUpdateCoach((int)dgvCoachs.CurrentRow.Cells[0].Value);
            frm.ShowDialog();

            _RefreshCoachsList();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int CoachID = (int)dgvCoachs.CurrentRow.Cells[0].Value;
            if (MessageBox.Show("Are you sure you want to delete Coach [ " + dgvCoachs.CurrentRow.Cells[1].Value + " ] ?", "Confirm Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                if (clsCoach.Delete(CoachID))
                {
                    MessageBox.Show("Deleted Coach Successfully!", "Deleted succeded", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _RefreshCoachsList();
                }
                else
                    MessageBox.Show("Deleted Coach faild! Coach cannot be deleted because has realations in system.", "Deleted faild", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void removeFromTeamToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int CoachID = (int)dgvCoachs.CurrentRow.Cells[0].Value;
            if (MessageBox.Show($"Are you sure you want to Remove Coach [ {dgvCoachs.CurrentRow.Cells[1].Value} ] From Team [ {dgvCoachs.CurrentRow.Cells[5].Value} ] ?", "Confirm Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                int TeamID = clsTeam.GetTeamIDByTeamName((string)dgvCoachs.CurrentRow.Cells[5].Value);
                clsTeam Team = clsTeam.Find(TeamID);
                Team.CoachID = -1;

                if (Team.Save())
                {
                    MessageBox.Show("Removed Coach Successfully!", "Deleted succeded", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _RefreshCoachsList();
                }
                else
                    MessageBox.Show("Removed Coach faild!", "Deleted faild", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            int CoachID = (int)dgvCoachs.CurrentRow.Cells[0].Value;
           
            Form frm = new frmJoinCoachToTeam(CoachID);
            frm.ShowDialog();
            
            _RefreshCoachsList();
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            if (dgvCoachs.Rows.Count > 0)
            {
                removeFromTeamToolStripMenuItem.Enabled = (dgvCoachs.CurrentRow.Cells[5].Value != DBNull.Value);
                tsmjoin.Enabled = (dgvCoachs.CurrentRow.Cells[5].Value == DBNull.Value);
            }
        }
    }
}
