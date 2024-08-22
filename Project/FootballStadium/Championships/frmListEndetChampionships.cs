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
    public partial class frmListEndetChampionships : Form
    {
        private DataTable _dtChampionships;

        public frmListEndetChampionships()
        {
            InitializeComponent();
        }
        private void _UserPermission()
        {

            clsUserPermission UserPermission = clsUserPermission.Find(clsGlobal.CurrentUser.UserID, "Championships");

            showEnrolledTeamsToolStripMenuItem.Enabled = UserPermission.Show;
            showWinnerToolStripMenuItem.Enabled = UserPermission.Show;
        }
        private void frmListEndetChampionships_Load(object sender, EventArgs e)
        {

            _UserPermission();
            _dtChampionships = clsChampionship.GetEndetChampionships();
            dgvChampionships.DataSource = _dtChampionships;
            cbFilterBy.SelectedIndex = 0;
            lblRecordsCount.Text = dgvChampionships.Rows.Count.ToString();
            if (dgvChampionships.Rows.Count > 0)
            {
                dgvChampionships.Columns[0].HeaderText = "Championship ID";
                dgvChampionships.Columns[0].Width = 60;

                dgvChampionships.Columns[1].HeaderText = "Name";
                dgvChampionships.Columns[1].Width = 200;

                dgvChampionships.Columns[2].HeaderText = "Start Date";
                dgvChampionships.Columns[2].Width = 100;

                dgvChampionships.Columns[3].HeaderText = "E.T Count";
                dgvChampionships.Columns[3].Width = 60;

                dgvChampionships.Columns[4].HeaderText = "Is Endet";
                dgvChampionships.Columns[4].Width = 60;

                dgvChampionships.Columns[5].HeaderText = "End Date";
                dgvChampionships.Columns[5].Width = 80;

                dgvChampionships.Columns[6].HeaderText = "Created Date";
                dgvChampionships.Columns[6].Width = 80;

                dgvChampionships.Columns[7].HeaderText = "Note";
                dgvChampionships.Columns[7].Width = 150;
            }
            else
            {
                cbFilterBy.Enabled = false;
                contextMenuStrip1.Enabled = false;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void showEnrolledTeamsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmListTeamsInChampionship((int)dgvChampionships.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
        }

        private void showWinnerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsWinner Winner = clsWinner.Find((int)dgvChampionships.CurrentRow.Cells[0].Value);
            if(Winner != null)
            {
                Form frm = new frmAddShowWinner(Winner.TeamID, Winner.ChampionshipID);
                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Some think is wrong!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Visible = (cbFilterBy.Text == "Championship ID" || cbFilterBy.Text == "Name");


            if (txtFilterValue.Visible)
            {
                txtFilterValue.Text = "";
                txtFilterValue.Focus();
                dtpFilter.Visible = false;
            }
            else
                dtpFilter.Visible = (cbFilterBy.Text != "None");

            if (cbFilterBy.Text == "None")
            {
                _dtChampionships.DefaultView.RowFilter = "";
                lblRecordsCount.Text = dgvChampionships.Rows.Count.ToString();
                return;
            }
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";
            //Map Selected Filter to real Column name 
            switch (cbFilterBy.Text)
            {
                case "Championship ID":
                    FilterColumn = "ChampionshipID";
                    break;

                case "Name":
                    FilterColumn = "Name";
                    break;

                default:
                    FilterColumn = "None";
                    break;

            }

            //Reset the filters in case nothing selected or filter value conains nothing.
            if (txtFilterValue.Text.Trim() == "" || FilterColumn == "None")
            {
                _dtChampionships.DefaultView.RowFilter = "";
                lblRecordsCount.Text = dgvChampionships.Rows.Count.ToString();
                return;
            }

            if (FilterColumn == "ChampionshipID")
                //in this case we deal with integer not string.

                _dtChampionships.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilterValue.Text.Trim());
            else
                _dtChampionships.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, txtFilterValue.Text.Trim());

            lblRecordsCount.Text = dgvChampionships.Rows.Count.ToString();
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilterBy.Text == "Championship ID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void dtpFilter_ValueChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";
            //Map Selected Filter to real Column name 
            switch (cbFilterBy.Text)
            {
                case "Start Date":
                    FilterColumn = "StartDate";
                    break;

                case "End Date":
                    FilterColumn = "EndDate";
                    break;  
                
                case "Created Date":
                    FilterColumn = "CreatedDate";
                    break;

                default:
                    FilterColumn = "None";
                    break;

            }
            if (FilterColumn == "None")
            {
                _dtChampionships.DefaultView.RowFilter = "";
                lblRecordsCount.Text = dgvChampionships.Rows.Count.ToString();
                return;
            }

            _dtChampionships.DefaultView.RowFilter = $"{FilterColumn}  >= '" + dtpFilter.Value.Date + $"' and  {FilterColumn}  <= '" +
             dtpFilter.Value.Date + "'";
            
            lblRecordsCount.Text = dgvChampionships.Rows.Count.ToString();

        }
    }
}
