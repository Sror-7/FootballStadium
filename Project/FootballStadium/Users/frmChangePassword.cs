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
using static FootballStadium_Business.clsUser;

namespace FootballStadium
{
    public partial class frmChangePassword : Form
    {
        clsUser _User;
        private int _UserID = -1;
        public frmChangePassword(int UserID)
        {
            _UserID = UserID;
            InitializeComponent();
        }
        private void frmChangePassword_Load(object sender, EventArgs e)
        {
            _User = clsUser.Find(_UserID);
            lblUserName.Text = _User.UserName;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(txtPassword.Text) || string.IsNullOrEmpty(txtConfirmPassword.Text))
            {
                MessageBox.Show("Some fildes are not valid!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if(txtOldPassword.Text != _User.Password)
            {
                MessageBox.Show("Old Password is wrong. Please try enter your old password correct", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if(txtPassword.Text != txtConfirmPassword.Text)
            {
                MessageBox.Show("Write you password correct. Confirm password is valid", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if(!_User.IsActive)
            {
                MessageBox.Show("User isn't Active. cannot change password!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _User.Password = clsGlobal.ComputeHash(txtPassword.Text);
            if(_User.Save())
            {
                txtOldPassword.Text = "";
                txtPassword.Text = "";
                txtConfirmPassword.Text = "";
                btnSave.Enabled = false;
                txtOldPassword.Enabled = false;
                txtPassword.Enabled = false;
                txtConfirmPassword.Enabled = false;
                MessageBox.Show("Change Password successfully!", "Succed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                MessageBox.Show("Change Password Faild!", "Faild", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtOldPassword_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txtOldPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtOldPassword.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtOldPassword, "Error: Write Password.");
                return;
            }
            else
                errorProvider1.SetError(txtOldPassword, null);
            if (clsGlobal.ComputeHash(txtOldPassword.Text) != _User.Password)
            {
                e.Cancel = true;
                errorProvider1.SetError(txtOldPassword, "Error: Old Password is wrong.");
                return;
            }
            else
                errorProvider1.SetError(txtOldPassword, null);
        }

        private void txtPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtPassword.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtPassword, "Error: Write Password.");
                return;
            }
            else
                errorProvider1.SetError(txtPassword, null);
        }

        private void txtConfirmPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtConfirmPassword.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtConfirmPassword, "Error: Write Password.");
                return;
            }
            else
                errorProvider1.SetError(txtConfirmPassword, null);

            if(txtConfirmPassword.Text != txtPassword.Text)
            {
                e.Cancel = true;
                errorProvider1.SetError(txtConfirmPassword, "Error: Confirm password isn't same.");
                return;
            }
            else
                errorProvider1.SetError(txtConfirmPassword, null);
        }
    }
}
