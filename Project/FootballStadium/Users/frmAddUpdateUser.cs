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
    public partial class frmAddUpdateUser : Form
    {
        enum enMode { AddNew = 1, Update = 2 }
        enMode Mode = enMode.AddNew;

        private clsUser _User;
        private clsUserPermission _UserPermission;

        private int _UserID = -1;
        public frmAddUpdateUser()
        {
            InitializeComponent();
        }
        public frmAddUpdateUser(int UserID)
        {
            _UserID = UserID;
            Mode = enMode.Update;
            InitializeComponent();
        }
        private void frmAddUpdateUser_Load(object sender, EventArgs e)
        {
            _ResetDefalutValue();

            if (Mode == enMode.Update)
            {
                _LoadData();
            }
        }

        private void _ResetDefalutValue()
        {
            if (Mode == enMode.AddNew)
            {
                this.Text = "Add New User";
                lblTitle.Text = "Add New User";
                _User = new clsUser();
                _UserPermission = new clsUserPermission();
            }
            else
            {
                this.Text = "Update User Info";
                lblTitle.Text = "Update User Info";
            }

            dtpDateOfBirth.Value = DateTime.Now.AddYears(-18);
        }
        private void _LoadData()
        {
            _User = clsUser.Find(_UserID);

            if (_User != null)
            {
                txtFirstName.Text = _User.PersonInfo.FirstName;
                txtLastName.Text = _User.PersonInfo.LastName;
                dtpDateOfBirth.Value = _User.PersonInfo.DateOfBirth;
                txtPhone.Text = _User.PersonInfo.Phone;
                richtxtAddress.Text = _User.PersonInfo.Address;

                txtUserName.Text = _User.UserName;

                txtPassword.Visible = false;
                txtConfirmPassword.Visible  = false;
                cbShowPassword.Visible = false;
                lblConfirmPassword.Visible = false;
                lblPassword.Visible = false;
                
                //txtPassword.Text = _User.Password;
                //txtConfirmPassword.Text = _User.Password;

                _LoadPermissionData();
            }
            else
            {
                MessageBox.Show($"No User with ID [{_UserID}] ", "User not Found!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
        }
        private void _LoadPermissionData()
        {
            _FillUserPermission(_UserID, "Teams");
            _FillUserPermission(_UserID, "Players");
            _FillUserPermission(_UserID, "Coachs");
            _FillUserPermission(_UserID, "Bookings");
            _FillUserPermission(_UserID, "Championships");
            _FillUserPermission(_UserID, "Users");
        }

        private void _FillUserPermission(int UserID, string FormName)
        {
            _UserPermission = clsUserPermission.Find(UserID, FormName);
            if (_UserPermission != null)
            {

                if (FormName == "Teams")
                {
                    if (_UserPermission.FullAccess) chkFullAccessTeams.Checked = true;
                    else
                    {
                        chkAddTeam.Checked = _UserPermission.Add;
                        chkEditTeams.Checked = _UserPermission.Edit;
                        chkShowTeams.Checked = _UserPermission.Show;
                        chkDeleteTeams.Checked = _UserPermission.Delete;
                    }
                }
                else if (FormName == "Players")
                {
                    if (_UserPermission.FullAccess) chkFullAccessPlayers.Checked = true;
                    else
                    {
                        chkAddPlayer.Checked = _UserPermission.Add;
                        chkEditPlayers.Checked = _UserPermission.Edit;
                        chkShowPlayers.Checked = _UserPermission.Show;
                        chkDeletePlayers.Checked = _UserPermission.Delete;
                    }
                }

                else if (FormName == "Coachs")
                {
                    if (_UserPermission.FullAccess) chkFullAccessCoachs.Checked = true;
                    else
                    {
                        chkAddCoachs.Checked = _UserPermission.Add;
                        chkEditCoachs.Checked = _UserPermission.Edit;
                        chkShowCoachs.Checked = _UserPermission.Show;
                        chkDeleteCoachs.Checked = _UserPermission.Delete;
                    }
                }

                else if (FormName == "Bookings")
                {
                    if (_UserPermission.FullAccess) chkFullAccessBookings.Checked = true;
                    else
                    {
                        chkAddBookings.Checked = _UserPermission.Add;
                        chkEditBookings.Checked = _UserPermission.Edit;
                        chkShowBookings.Checked = _UserPermission.Show;
                        chkDeleteBookings.Checked = _UserPermission.Delete;
                    }
                }

                else if (FormName == "Championships")
                {
                    if (_UserPermission.FullAccess) chkFullAccessChampionships.Checked = true;
                    else
                    {
                        chkAddChampionships.Checked = _UserPermission.Add;
                        chkEditChampionships.Checked = _UserPermission.Edit;
                        chkShowChampionships.Checked = _UserPermission.Show;
                        chkDeleteChampionships.Checked = _UserPermission.Delete;
                    }
                }
                else
                {
                    if (_UserPermission.FullAccess) chkFullAccessUsers.Checked = true;
                    else
                    {
                        chkAddUsers.Checked = _UserPermission.Add;
                        chkEditUsers.Checked = _UserPermission.Edit;
                        chkShowUsers.Checked = _UserPermission.Show;
                        chkDeleteUsers.Checked = _UserPermission.Delete;
                    }
                }

            }
            else { MessageBox.Show("Something is wrong.", "Caption", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }


        }


        private void _SaveUserPermissions()
        {

            //Add or Update Permission to user for Teams 
            _UserPermission.FormName = "Teams";
            _UserPermission.UserID = _User.UserID;
            _UserPermission.Add = chkAddTeam.Checked;
            _UserPermission.Edit = chkEditTeams.Checked;
            _UserPermission.Show = chkShowTeams.Checked;
            _UserPermission.Delete = chkDeleteTeams.Checked;
            _UserPermission.Save();





            //Add or Update Permissions to user for Players
            _UserPermission.FormName = "Players";
            _UserPermission.UserID = _User.UserID;
            _UserPermission.Add = chkAddPlayer.Checked;
            _UserPermission.Edit = chkEditPlayers.Checked;
            _UserPermission.Show = chkShowPlayers.Checked;
            _UserPermission.Delete = chkDeletePlayers.Checked;
            _UserPermission.Save();





            //Add or Update Permissions to user for Coachs
            _UserPermission.FormName = "Coachs";
            _UserPermission.UserID = _User.UserID;
            _UserPermission.Add = chkAddCoachs.Checked;
            _UserPermission.Edit = chkEditCoachs.Checked;
            _UserPermission.Show = chkShowCoachs.Checked;
            _UserPermission.Delete = chkDeleteCoachs.Checked;
            _UserPermission.Save();





            //Add or Update Permission to user for Bookings
            _UserPermission.FormName = "Bookings";
            _UserPermission.UserID = _User.UserID;
            _UserPermission.Add = chkAddBookings.Checked;
            _UserPermission.Edit = chkEditBookings.Checked;
            _UserPermission.Show = chkShowBookings.Checked;
            _UserPermission.Delete = chkDeleteBookings.Checked;
            _UserPermission.Save();





            //Add or Update Permission to user for Championships
            _UserPermission.FormName = "Championships";
            _UserPermission.UserID = _User.UserID;
            _UserPermission.Add = chkAddChampionships.Checked;
            _UserPermission.Edit = chkEditChampionships.Checked;
            _UserPermission.Show = chkShowChampionships.Checked;
            _UserPermission.Delete = chkDeleteChampionships.Checked;
            _UserPermission.Save();





            //Add or Update Permission to user for Users
            _UserPermission.FormName = "Users";
            _UserPermission.UserID = _User.UserID;
            _UserPermission.Add = chkAddUsers.Checked;
            _UserPermission.Edit = chkEditUsers.Checked;
            _UserPermission.Show = chkShowUsers.Checked;
            _UserPermission.Delete = chkDeleteUsers.Checked;
            _UserPermission.Save();

            _UserPermission.UpdateMode();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some fields are not valid!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _User.PersonInfo.FirstName = txtFirstName.Text;
            _User.PersonInfo.LastName = txtLastName.Text;
            _User.PersonInfo.Phone = txtPhone.Text;
            _User.PersonInfo.Address = richtxtAddress.Text;
            _User.PersonInfo.DateOfBirth = dtpDateOfBirth.Value;

            _User.UserName = txtUserName.Text;
            if(Mode == enMode.AddNew)
                _User.Password = clsGlobal.ComputeHash(txtPassword.Text.Trim());
            _User.IsActive = true;
            if (Mode == enMode.AddNew)
                _User.CreatedDate = DateTime.Now;

            if (_User.Save())
            {
                _SaveUserPermissions();


                lblTitle.Text = "Update User Info";
                this.Text = "Update User Info";
                Mode = enMode.Update;

                MessageBox.Show("Data Saved Successfully!", "Succeded", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                MessageBox.Show("Data Saved Faild!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
        private void txtFirstName_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtFirstName.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFirstName, "Error: Write First Name.");
                return;
            }
            else
                errorProvider1.SetError(txtFirstName, null);
        }

        private void txtLastName_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtLastName.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtLastName, "Error: Write Last Name.");
                return;
            }
            else
                errorProvider1.SetError(txtLastName, null);
        }

        private void txtUserName_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtUserName.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtUserName, "Error: Write User Name.");
                return;
            }
            else
                errorProvider1.SetError(txtUserName, null);

            if (clsUser.IsUserNameUsed(txtUserName.Text) && txtUserName.Text != _User.UserName)
            {
                e.Cancel = true;
                errorProvider1.SetError(txtUserName, "Error: UserName Already exist.");
                return;
            }
            else
                errorProvider1.SetError(txtUserName, null);

        }

        private void txtPassword_Validating(object sender, CancelEventArgs e)
        {
            if (Mode == enMode.Update)
                return;
            if (string.IsNullOrEmpty(txtPassword.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtPassword, "Error: Write Password.");
                return;
            }
            else
                errorProvider1.SetError(txtPassword, null);
            if(Mode == enMode.Update && clsGlobal.ComputeHash(txtPassword.Text) != _User.Password)
            {
                e.Cancel = true;
                errorProvider1.SetError(txtPassword, "Error: Old Password is wrong.");
                return;
            }
            else
                errorProvider1.SetError(txtPassword, null);
        }

        private void txtConfirmPassword_Validating(object sender, CancelEventArgs e)
        {
            if (Mode == enMode.Update)
                return;
            if (string.IsNullOrEmpty(txtConfirmPassword.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtConfirmPassword, "Error: Confirm Password Please!");
                return;
            }
            else
                errorProvider1.SetError(txtConfirmPassword, null);

            if (txtConfirmPassword.Text != txtPassword.Text && txtPassword.Text != "")
            {
                e.Cancel = true;
                errorProvider1.SetError(txtConfirmPassword, "Error: Password is Wrong!");
                return;
            }
            else
                errorProvider1.SetError(txtConfirmPassword, null);


        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cbShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            if (cbShowPassword.Checked)
            {
                txtPassword.PasswordChar = '\0';
                txtConfirmPassword.PasswordChar = '\0';
            }
            else
            {
                txtPassword.PasswordChar = '*';
                txtConfirmPassword.PasswordChar = '*';
            }
        }

        private void CheckEnabledIffullAccess(CheckBox Add, CheckBox Edit, CheckBox Show, CheckBox Delete, bool Checked = true, bool Enabled = false)
        {
            Add.Checked = Checked;
            Edit.Checked = Checked;
            Show.Checked = Checked;
            Delete.Checked = Checked;

            Add.Enabled = Enabled;
            Edit.Enabled = Enabled;
            Show.Enabled = Enabled;
            Delete.Enabled = Enabled;
        }

        private void chkFullAccessTeams_CheckedChanged(object sender, EventArgs e)
        {
            if (chkFullAccessTeams.Checked)
                CheckEnabledIffullAccess(chkAddTeam, chkEditTeams, chkShowTeams, chkDeleteTeams);
            else
                CheckEnabledIffullAccess(chkAddTeam, chkEditTeams, chkShowTeams, chkDeleteTeams, false, true);
        }

        private void chkFullAccessPlayers_CheckedChanged(object sender, EventArgs e)
        {
            if (chkFullAccessPlayers.Checked)
                CheckEnabledIffullAccess(chkAddPlayer, chkEditPlayers, chkShowPlayers, chkDeletePlayers);
            else
                CheckEnabledIffullAccess(chkAddPlayer, chkEditPlayers, chkShowPlayers, chkDeletePlayers, false, true);
        }

        private void chkFullAccessCoachs_CheckedChanged(object sender, EventArgs e)
        {
            if (chkFullAccessCoachs.Checked)
                CheckEnabledIffullAccess(chkAddCoachs, chkEditCoachs, chkShowCoachs, chkDeleteCoachs);
            else
                CheckEnabledIffullAccess(chkAddCoachs, chkEditCoachs, chkShowCoachs, chkDeleteCoachs, false, true);
        }

        private void chkFullAccessBookings_CheckedChanged(object sender, EventArgs e)
        {
            if (chkFullAccessBookings.Checked)
                CheckEnabledIffullAccess(chkAddBookings, chkEditBookings, chkShowBookings, chkDeleteBookings);
            else
                CheckEnabledIffullAccess(chkAddBookings, chkEditBookings, chkShowBookings, chkDeleteBookings, false, true);
        }

        private void chkFullAccessChampionships_CheckedChanged(object sender, EventArgs e)
        {
            if (chkFullAccessChampionships.Checked)
                CheckEnabledIffullAccess(chkAddChampionships, chkEditChampionships, chkShowChampionships, chkDeleteChampionships);
            else
                CheckEnabledIffullAccess(chkAddChampionships, chkEditChampionships, chkShowChampionships, chkDeleteChampionships, false, true);
        }

        private void chkFullAccessUsers_CheckedChanged(object sender, EventArgs e)
        {
            if (chkFullAccessUsers.Checked)
                CheckEnabledIffullAccess(chkAddUsers, chkEditUsers, chkShowUsers, chkDeleteUsers);
            else
                CheckEnabledIffullAccess(chkAddUsers, chkEditUsers, chkShowUsers, chkDeleteUsers, false, true);
        }
    }
}
