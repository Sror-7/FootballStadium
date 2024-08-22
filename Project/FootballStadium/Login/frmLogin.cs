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
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }
        private void picShowPassword_Click(object sender, EventArgs e)
        {
            if (picShowPassword.Tag == "hide")
            {
                txtPassword.PasswordChar = '\0';
                picShowPassword.Image = Resources.seen;
                picShowPassword.Tag = "seen";

            }
            else
            {
                txtPassword.PasswordChar = '*';
                picShowPassword.Image = Resources.hide; 
                picShowPassword.Tag = "hide";
            }
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            string UserName = "", Password = "";

            if (clsGlobal.GetStoredCredential(ref UserName, ref Password))
            {
                txtUsername.Text = UserName;
                txtPassword.Text = Password;
                chRememberMe.Checked = true;
            }
            else
                chRememberMe.Checked = false;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {

            clsUser user = clsUser.FindByUserNameAndPassword(txtUsername.Text.Trim(), clsGlobal.ComputeHash(txtPassword.Text));

            if(user == null)
            {
                MessageBox.Show("UserName or Password is wrong! Please write correct info!", "Wrong", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {

                if(chRememberMe.Checked)
                {
                    clsGlobal.RememberUsernameAndPassword(txtUsername.Text.Trim(), txtPassword.Text.Trim());
                }
                else
                {
                    clsGlobal.RememberUsernameAndPassword("", "");
                }

                if (!user.IsActive)
                {

                    txtUsername.Focus();
                    MessageBox.Show("Your accound is not Active, Contact Admin.", "In Active Account", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                clsGlobal.CurrentUser = user;
                this.Hide();
                frmMain frm = new frmMain(this);
                frm.ShowDialog();
            }
        }
    }
}
