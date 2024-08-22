using FootballStadium.Global_Classes;
using FootballStadium.Properties;
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
    public partial class frmListUsers : Form
    {
        private static DataTable _dtAllUsers;

        public frmListUsers()
        {

            InitializeComponent();
        }
        private void _RefreshUsersList()
        {
            _dtAllUsers = clsUser.GetAllUsers();
            dgvUsers.DataSource = _dtAllUsers;
            cbFilterBy.SelectedIndex = 0;
            lblRecordsCount.Text = dgvUsers.Rows.Count.ToString();
            if (dgvUsers.Rows.Count > 0)
            {
                cbFilterBy.Enabled = true;
                cmsUsers.Enabled = true;
            }
            else
            {
                cmsUsers.Enabled = false;
                cbFilterBy.Enabled = false;
            }
        }
        private void _UserPermission()
        {
            clsUserPermission UserPermission = clsUserPermission.Find(clsGlobal.CurrentUser.UserID, "Users");
            editToolStripMenuItem.Enabled = UserPermission.Edit;
            addNewUserToolStripMenuItem.Enabled = UserPermission.Add;
            btnAdd.Enabled = UserPermission.Add;
            deleteToolStripMenuItem1.Enabled = UserPermission.Delete;
            deactivateToolStripMenuItem.Enabled = UserPermission.Edit;
        }
        private void frmListUsers_Load(object sender, EventArgs e)
        {
            _UserPermission();


            _dtAllUsers = clsUser.GetAllUsers();
            dgvUsers.DataSource = _dtAllUsers;
            cbFilterBy.SelectedIndex = 0;
            lblRecordsCount.Text = dgvUsers.Rows.Count.ToString();

            if(dgvUsers.Rows.Count > 0)
            {
                dgvUsers.Columns[0].HeaderText = "User ID";
                dgvUsers.Columns[0].Width = 80;

                dgvUsers.Columns[1].HeaderText = "Person ID";
                dgvUsers.Columns[1].Width = 80;

                dgvUsers.Columns[2].HeaderText = "Full Name";
                dgvUsers.Columns[2].Width = 180;

                dgvUsers.Columns[3].HeaderText = "User Name";
                dgvUsers.Columns[3].Width = 180;

                dgvUsers.Columns[4].HeaderText = "Is Active";
                dgvUsers.Columns[4].Width = 100;
            }
            else
            {
                cmsUsers.Enabled = false;
                cbFilterBy.Enabled = false;
            }
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";

            switch (cbFilterBy.Text)
            {
                case "User ID":
                    FilterColumn = "UserID";
                    break;

                case "Person ID":
                    FilterColumn = "PersonID";
                    break;

                case "Full Name":
                    FilterColumn = "FullName";
                    break;

                case "User Name":
                    FilterColumn = "UserName";
                    break;

                default:
                    FilterColumn = "None";
                    break;
            }

            if (txtFilterValue.Text.Trim() == "" || FilterColumn == "None")
            {
                _dtAllUsers.DefaultView.RowFilter = "";
                lblRecordsCount.Text = dgvUsers.Rows.Count.ToString();
                return;
            }

            if (FilterColumn == "UserID" || FilterColumn == "PersonID")
                //in this case we deal with integer not string.

                _dtAllUsers.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilterValue.Text.Trim());
            else
                _dtAllUsers.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, txtFilterValue.Text.Trim());

            lblRecordsCount.Text = dgvUsers.Rows.Count.ToString();

        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Visible = (cbFilterBy.Text != "None" && cbFilterBy.Text != "Is Active");

            cbIsActive.Visible = (cbFilterBy.Text == "Is Active");

            if(txtFilterValue.Visible)
            {
                txtFilterValue.Text = "";
                txtFilterValue.Focus();
            }
            if(cbIsActive.Visible)
            {
                cbIsActive.SelectedIndex = 0;
                cbIsActive.Focus();
            }
            if (cbFilterBy.Text == "None")
            {
                _dtAllUsers.DefaultView.RowFilter = "";
                lblRecordsCount.Text = dgvUsers.Rows.Count.ToString();
                return;
            }
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilterBy.Text == "User ID" || cbFilterBy.Text == "Person ID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void cbFilterByIsActive_SelectedIndexChanged(object sender, EventArgs e)
        {
            string FilterColumn = "IsActive";
            string FilterValue = cbIsActive.Text;

            switch (FilterValue)
            {
                case "All":
                    break;
                case "Yes":
                    FilterValue = "1";
                    break;
                case "No":
                    FilterValue = "0";
                    break;
            }


            if (FilterValue == "All")
                _dtAllUsers.DefaultView.RowFilter = "";
            else
                //in this case we deal with numbers not string.
                _dtAllUsers.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, FilterValue);

            lblRecordsCount.Text = dgvUsers.Rows.Count.ToString();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Form frm = new frmAddUpdateUser();
            frm.ShowDialog();

            _RefreshUsersList();
        }
        private void addNewUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmAddUpdateUser();
            frm.ShowDialog();

            _RefreshUsersList();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmAddUpdateUser((int)dgvUsers.CurrentRow.Cells[0].Value);
            frm.ShowDialog();

            _RefreshUsersList();
        }

        private void showuserinfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmShowUserInfo((int)dgvUsers.CurrentRow.Cells[0].Value);
            frm.ShowDialog();

            _RefreshUsersList();
        }
        private void deleteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if((int)dgvUsers.CurrentRow.Cells[0].Value == clsGlobal.CurrentUser.UserID)
            {
                MessageBox.Show("You cannot Delete this user because you logged with this user in system. Please Log in with another user and then Delete", "Cannot Delete logged User", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (MessageBox.Show($"Are you sure you want to delete user with ID [ {(int)dgvUsers.CurrentRow.Cells[0].Value} ] ?", "Confirm Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                clsUser User = clsUser.Find((int)dgvUsers.CurrentRow.Cells[0].Value);
                if (User.Delete())
                {
                    MessageBox.Show($"User with ID [ {(int)dgvUsers.CurrentRow.Cells[0].Value} ] Deleted", "Succeded", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _RefreshUsersList();
                    return;
                }
                else
                {
                    MessageBox.Show($"User with ID [ {(int)dgvUsers.CurrentRow.Cells[0].Value} ] deleted faild!", "Faild", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmsUsers_Opening(object sender, CancelEventArgs e)
        {
            int userID = (int)dgvUsers.CurrentRow.Cells[0].Value;

            if(clsUser.isUserActive(userID)) 
            {
                deactivateToolStripMenuItem.Text = "Deactive";
                deactivateToolStripMenuItem.Image = Resources.delete__3_;
            }
            else
            {
                deactivateToolStripMenuItem.Text = "Active";
                deactivateToolStripMenuItem.Image = Resources.active_user;
            }
        }

        private void deactivateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int userID = (int)dgvUsers.CurrentRow.Cells[0].Value;

            if (deactivateToolStripMenuItem.Text == "Active")
                clsUser.Active(userID);

            else
                clsUser.Deactive(userID);

        }
    }
}
