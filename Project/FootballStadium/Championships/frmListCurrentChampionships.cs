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
    public partial class frmListCurrentChampionships : Form
    {
        private DataTable _dtChampionships;
        public frmListCurrentChampionships()
        {
            InitializeComponent();
        }

        private void _RefereshCurrentChampionshipsList()
        {
            _dtChampionships = clsChampionship.GetCurrentChampionships();
            dgvChampionships.DataSource = _dtChampionships;
            lblRecordsCount.Text = dgvChampionships.Rows.Count.ToString();

            if (dgvChampionships.Rows.Count > 0)
            {
                contextMenuStrip1.Enabled = true;
            }
            else
            {
                contextMenuStrip1.Enabled = false;
            }
        }
        private void _UserPermission()
        {

            clsUserPermission UserPermission = clsUserPermission.Find(clsGlobal.CurrentUser.UserID, "Championships");

            tsmShow.Enabled = UserPermission.Show;
            tsmEdit.Enabled = UserPermission.Edit;
            tsmDelete.Enabled = UserPermission.Delete;
        }
        private void frmShowCurrentChampionships_Load(object sender, EventArgs e)
        {
            _UserPermission();


            _dtChampionships = clsChampionship.GetCurrentChampionships();
            dgvChampionships.DataSource = _dtChampionships;
            lblRecordsCount.Text = dgvChampionships.Rows.Count.ToString();
            if (dgvChampionships.Rows.Count > 0)
            {
                dgvChampionships.Columns[0].HeaderText = "Championship ID";
                dgvChampionships.Columns[0].Width = 80;

                dgvChampionships.Columns[1].HeaderText = "Name";
                dgvChampionships.Columns[1].Width = 200;

                dgvChampionships.Columns[2].HeaderText = "Start Date";
                dgvChampionships.Columns[2].Width = 100;

                dgvChampionships.Columns[3].HeaderText = "E.T Count";
                dgvChampionships.Columns[3].Width = 80;

                dgvChampionships.Columns[4].HeaderText = "Is Endet";
                dgvChampionships.Columns[4].Width = 80;

                dgvChampionships.Columns[5].HeaderText = "Created Date";
                dgvChampionships.Columns[5].Width = 80;

                dgvChampionships.Columns[6].HeaderText = "Note";
                dgvChampionships.Columns[6].Width = 150;
            }
            else
            {
                contextMenuStrip1.Enabled = false;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void updateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int ChampionshipID = (int)dgvChampionships.CurrentRow.Cells[0].Value;
            Form frm = new frmListTeamsInChampionship(ChampionshipID);
            frm.ShowDialog();
            _RefereshCurrentChampionshipsList();
        }

        private void deleteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to this Championship [ " + dgvChampionships.CurrentRow.Cells[1].Value + " ]", "Confirm Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                if (clsChampionship.Delete((int)dgvChampionships.CurrentRow.Cells[0].Value))
                {
                    MessageBox.Show("Delete Championship Successfully!", "Deleted succeded", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _RefereshCurrentChampionshipsList();
                }
                else
                    MessageBox.Show("Delete Championship faild! Championship cannot be deleted because has realations in system.", "Deleted faild", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmAddUpdateChampionship((int)dgvChampionships.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            _RefereshCurrentChampionshipsList();
        }
    }
}
