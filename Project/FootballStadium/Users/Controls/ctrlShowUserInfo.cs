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
    public partial class ctrlShowUserInfo : UserControl
    {
        public ctrlShowUserInfo()
        {
            InitializeComponent();
        }

        public void LoadUserInfo(int _UserID)
        {
            clsUser User = clsUser.Find(_UserID);

            if(User != null)
            {
                lblUserID.Text = User.UserID.ToString();
                lblName.Text = User.PersonInfo.FullName;
                lblPhone.Text = User.PersonInfo.Phone;
                lblAddress.Text = User.PersonInfo.Address;
                lblDateOfBirth.Text = User.PersonInfo.DateOfBirth.ToShortDateString();

                lblUserName.Text = User.UserName;
                lblIsAcitve.Text = User.IsActive.ToString();

            }
            else
            {
                MessageBox.Show($"No User with ID [{_UserID}]", "User not found!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
