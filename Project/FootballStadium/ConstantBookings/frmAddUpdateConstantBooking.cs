using FootballStadium.Global_Classes;
using FootballStadium_Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FootballStadium
{
    public partial class frmAddUpdateConstantBooking : Form
    {
        private clsConstantBooking _ConstantBooking;
        private int _ConstantBookingID = -1;
        enum enMode { AddNew = 1, Update = 2 }
        enMode Mode = enMode.AddNew;
        public frmAddUpdateConstantBooking()
        {
            InitializeComponent();
        }
        public frmAddUpdateConstantBooking(int ConstantBookingID)
        {
            _ConstantBookingID = ConstantBookingID;
            Mode = enMode.Update;

            InitializeComponent();
        }
        private void txtTeamName_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtTeamName.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtTeamName, "Error: Write Team Name.");
                return;
            }
            else
                errorProvider1.SetError(txtTeamName, null);

            if (!clsTeam.IsTeamExist(txtTeamName.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtTeamName, "Error: Team isn't exist in system");
                return;
            }
            else
                errorProvider1.SetError(txtTeamName, null);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some fields are not valid!", "Validation Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _ConstantBooking.FromTime = new TimeSpan(dtpFromTime.Value.Hour, dtpFromTime.Value.Minute, 0);
            _ConstantBooking.ToTime = new TimeSpan(dtpToTime.Value.Hour, dtpToTime.Value.Minute, 0);

            if (clsConstantBooking.IsTimeBooked(cbDayOfWeek.Text,_ConstantBooking.FromTime,_ConstantBooking.ToTime))
            {
                MessageBox.Show("Sorry! this time is booked!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (clsBooking.IsTimeBooked(cbDayOfWeek.Text, _ConstantBooking.FromTime, _ConstantBooking.ToTime))
            {
                MessageBox.Show("This time is booked for one time. your booking will book in this time always", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            _ConstantBooking.TeamID = clsTeam.GetTeamIDByTeamName(txtTeamName.Text);
            _ConstantBooking.DayName = cbDayOfWeek.Text;
            _ConstantBooking.CreatedDate = DateTime.Now;
            _ConstantBooking.CreatedByUserID = clsGlobal.CurrentUser.UserID;

            if(_ConstantBooking.Save())
            {
                // i don't want to update the booking the i have booked so in future when i want to do update i can do it 
                //my english is bad :) hhhhh
                //lblTitle.Text = "Update Constant Booking";
                //this.Text = "Update Constant Booking";
                //Mode = enMode.Update;
                txtTeamName.Text = "";
                MessageBox.Show("Data Saved Successfully!","Succeded",MessageBoxButtons.OK,MessageBoxIcon.Information);
                return;
            }
            else
            {
                MessageBox.Show("Data Saved Faild!", "Faild", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
        private void _ResetDefaultValues()
        {
            if(Mode == enMode.AddNew)
            {
                lblTitle.Text = "Add Constant Booking";
                this.Text = "Add Constant Booking";
                _ConstantBooking = new clsConstantBooking();
            }
            else
            {
                lblTitle.Text = "Update Constant Booking";
                this.Text = "Update Constant Booking";
            }
            txtTeamName.Text = "";
            cbDayOfWeek.SelectedIndex = 0;

            dtpFromTime.Value = dtpFromTime.Value.AddMinutes(-dtpFromTime.Value.Minute);
            dtpToTime.Value = dtpToTime.Value.AddMinutes(-dtpToTime.Value.Minute);
            dtpToTime.Value = dtpToTime.Value.AddHours(1);
        }
        //private void _LoadDate()
        //{
         
              // Write your code here when you want to make update mode...

        //}
        private void frmAddUpdateConstantBooking_Load(object sender, EventArgs e)
        {
            _ResetDefaultValues();

            //if(Mode == enMode.Update)
            //{
            //    _LoadDate();
            //}
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
