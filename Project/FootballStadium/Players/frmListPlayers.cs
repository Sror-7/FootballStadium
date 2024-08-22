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
    public partial class frmListPlayers : Form
    {
        private int _TeamID = -1;
        DataTable _dtPlayers = new DataTable(); 
        public frmListPlayers(int TeamID)
        {
            _TeamID = TeamID;
            InitializeComponent();
        }
        private void _RefreshListPlayers()
        {
            _dtPlayers = clsTeam.GetPlayersInfo(_TeamID);

            dgvPlayers.DataSource = _dtPlayers;
            cbFilterBy.SelectedIndex = 0;
            lblRecords.Text = dgvPlayers.Rows.Count.ToString();

            if (dgvPlayers.Rows.Count > 0)
            {
                cbFilterBy.Enabled = true;
                cmsPlayers.Enabled = true;
            }
            else
            {
                cbFilterBy.Enabled = false;
                cmsPlayers.Enabled = false;
            }
        }
        private void _UserPermission()
        {

            clsUserPermission UserPermission = clsUserPermission.Find(clsGlobal.CurrentUser.UserID, "Players");

            btnAdd.Enabled = UserPermission.Add;
            deleteToolStripMenuItem.Enabled = UserPermission.Delete;
            editToolStripMenuItem.Enabled = UserPermission.Edit;
            showPlayerInfoToolStripMenuItem.Enabled = UserPermission.Show;
        }
        private void frmListPlayers_Load(object sender, EventArgs e)
        {
            _UserPermission();


            _dtPlayers = clsTeam.GetPlayersInfo(_TeamID);

            dgvPlayers.DataSource = _dtPlayers;
            cbFilterBy.SelectedIndex = 0;
            lblRecords.Text = dgvPlayers.Rows.Count.ToString();

            if(dgvPlayers.Rows.Count > 0)
            {
                dgvPlayers.Columns[0].HeaderText = "Player ID";
                dgvPlayers.Columns[0].Width = 80;

                dgvPlayers.Columns[1].HeaderText = "Team Name";
                dgvPlayers.Columns[1].Width = 140;

                dgvPlayers.Columns[2].HeaderText = "Player Name";
                dgvPlayers.Columns[2].Width = 180;

                dgvPlayers.Columns[3].HeaderText = "Player Number";
                dgvPlayers.Columns[3].Width = 60;

                dgvPlayers.Columns[4].HeaderText = "Date Of Birth";
                dgvPlayers.Columns[4].Width = 80;

                dgvPlayers.Columns[5].HeaderText = "Phone";
                dgvPlayers.Columns[5].Width = 80;

                dgvPlayers.Columns[6].HeaderText = "Address";
                dgvPlayers.Columns[6].Width = 180;

                dgvPlayers.Columns[7].HeaderText = "Date Of Accession";
                dgvPlayers.Columns[7].Width = 100;
            }
            else
            {
                cbFilterBy.Enabled = false;
                cmsPlayers.Enabled = false;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Form frm = new frmAddUpdatePlayer(_TeamID);
            frm.ShowDialog();
            _RefreshListPlayers();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void showPlayerInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PlayerID = (int)dgvPlayers.CurrentRow.Cells[0].Value;
            
            Form frm = new frmShowPlayerInfo(PlayerID);
            frm.ShowDialog();

            _RefreshListPlayers();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PlayerID = (int)dgvPlayers.CurrentRow.Cells[0].Value;
            clsPlayer Player = clsPlayer.Find(PlayerID);

            Form frm = new frmAddUpdatePlayer(Player);
            frm.ShowDialog();

            _RefreshListPlayers();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PlayerID = (int)dgvPlayers.CurrentRow.Cells[0].Value;
            clsPlayer Player = clsPlayer.Find(PlayerID);

            if(MessageBox.Show($"Are you sure you want to delete Player with ID [{PlayerID}]?", "Confirm Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                if (Player.Delete())
                {
                    MessageBox.Show("Deleted Successfully!", "Succeded", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _RefreshListPlayers();
                    return;
                }
                else
                    MessageBox.Show("Deleted Faild! player has realations in system cannot be deleted.", "faild", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Visible = (cbFilterBy.Text != "None");

            if(txtFilterValue.Visible)
            {
                txtFilterValue.Text = "";
                txtFilterValue.Focus();
            }
            if(cbFilterBy.Text == "None")
            {
                _dtPlayers.DefaultView.RowFilter = "";
                lblRecords.Text = dgvPlayers.Rows.Count.ToString();
                return;
            }
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";
            //Map Selected Filter to real Column name 
            switch (cbFilterBy.Text)
            {
                case "Player ID":
                    FilterColumn = "PlayerID";
                    break;

                case "Team Name":
                    FilterColumn = "TeamName";
                    break;

                case "Player Name":
                    FilterColumn = "PlayerName";
                    break;

                case "Player Number":
                    FilterColumn = "PlayerNumber";
                    break;

                case "Phone":
                    FilterColumn = "Phone";
                    break;

                case "Address":
                    FilterColumn = "Address";
                    break;

                default:
                    FilterColumn = "None";
                    break;

            }

            //Reset the filters in case nothing selected or filter value conains nothing.
            if (txtFilterValue.Text.Trim() == "" || FilterColumn == "None")
            {
                _dtPlayers.DefaultView.RowFilter = "";
                lblRecords.Text = dgvPlayers.Rows.Count.ToString();
                return;
            }

            if (FilterColumn == "PlayerID" || FilterColumn == "PlayerNumber")
                //in this case we deal with integer not string.

                _dtPlayers.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilterValue.Text.Trim());
            else
                _dtPlayers.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, txtFilterValue.Text.Trim());

            lblRecords.Text = dgvPlayers.Rows.Count.ToString();
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilterBy.Text == "Player ID" || cbFilterBy.Text == "Player Number" || cbFilterBy.Text == "Phone")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}
