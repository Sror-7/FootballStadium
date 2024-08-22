using FootballStadium.Global_Classes;
using FootballStadium_Business;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace FootballStadium
{
    public partial class frmAddUpdateCoach : Form
    {
        public delegate void delPassData(int CoachID);
        public event delPassData PassData;
        enum enMode { AddNew = 1, Update = 2}
        enMode Mode = enMode.AddNew;
        private int _CoachID = -1;
        private clsCoach _Coach;

        public frmAddUpdateCoach()
        {
            InitializeComponent();
        }
        public frmAddUpdateCoach(int CoachID)
        {
            _CoachID = CoachID;
            Mode = enMode.Update;
            InitializeComponent();
        }

        private void _EnabledToolsFalse()
        {
            btnSave.Enabled = false;
            txtFirstName.Enabled = false;
            txtLastName.Enabled = false;
            txtPhone.Enabled = false;
            richtxtAddress.Enabled = false;
            nudChampionshipsCount.Enabled = false;
            nudYearsOfExperience.Enabled = false;
            dtpDateOfBirth.Enabled = false;
        }
        private void _ResetDefaultData()
        {
            if(Mode == enMode.AddNew)
            {
                lblTitle.Text = "Add New Coach";
                this.Text = "Add Coach";
                _Coach = new clsCoach();
            }
            else
            {
                lblTitle.Text = "Update Coach Info";
                this.Text = "Update Info";
            }
        }
        private void _LoadDate()
        {
            _Coach = clsCoach.Find(_CoachID);
            if (_Coach != null)
            {
                txtFirstName.Text = _Coach.PersonInfo.FirstName;
                txtLastName.Text = _Coach.PersonInfo.LastName;
                dtpDateOfBirth.Value = _Coach.PersonInfo.DateOfBirth;
                txtPhone.Text = _Coach.PersonInfo.Phone;
                richtxtAddress.Text = _Coach.PersonInfo.Address;

                nudYearsOfExperience.Value = _Coach.YearsOfExperience;
                nudChampionshipsCount.Value = _Coach.ChampionshipsCount;
            }
            else
                MessageBox.Show($"Error: Coach with ID [ {_CoachID} ] not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            
            return;
        }
        private void frmAddUpdateCoach_Load(object sender, EventArgs e)
        {
            _ResetDefaultData();
            if(Mode == enMode.Update)
            {
                _LoadDate();
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if(!this.ValidateChildren())
            {
                MessageBox.Show("Some fields are not valid!", "Validation Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _Coach.PersonInfo.FirstName = txtFirstName.Text;
            _Coach.PersonInfo.LastName = txtLastName.Text;
            _Coach.PersonInfo.DateOfBirth = dtpDateOfBirth.Value;
            _Coach.PersonInfo.Phone = txtPhone.Text;
            _Coach.PersonInfo.Address = richtxtAddress.Text;

            _Coach.YearsOfExperience = (int)nudYearsOfExperience.Value;
            _Coach.ChampionshipsCount = (int)nudChampionshipsCount.Value;
            _Coach.CreatedDate = DateTime.Now;
            _Coach.CreatedByUserID = clsGlobal.CurrentUser.UserID;


            if(_Coach.Save())
            {
                if (Mode == enMode.AddNew)
                {
                    PassData(_Coach.CoachID);
                }

                Mode = enMode.Update;
                lblTitle.Text = "Update Coach Info";
                this.Text = "Update Info";
                MessageBox.Show("Data Saved Successfully!", "Succeded", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Data Saved Faild!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void txtFirstName_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtFirstName.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFirstName, "Error: Write Last name.");
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
                errorProvider1.SetError(txtLastName, "Error: Write Last name.");
                return;
            }
            else
                errorProvider1.SetError(txtLastName, null);
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
