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
    public partial class frmAddUpdateBooking : Form
    {
        private clsBooking _Booking;
        enum enMode { AddNew = 1, Update = 2}
        enMode Mode = enMode.AddNew;
        public frmAddUpdateBooking()
        {
            InitializeComponent();
        }
        private void _ResetDefaultValue()
        {
            if(Mode == enMode.AddNew)
            {
                this.Text = "Add New Booking";
                lblTitle.Text = "Add New Booking";
                _Booking = new clsBooking();
            }
            else
            {
                this.Text = "Update Booking";
                lblTitle.Text = "Update Booking";
            }
            txtTeamName.Text = "";
            dtpBookingDate.MinDate = DateTime.Now;
            dtpFromTime.Value = dtpFromTime.Value.AddMinutes(-dtpFromTime.Value.Minute);
            dtpToTime.Value = dtpToTime.Value.AddMinutes(-dtpToTime.Value.Minute);
            dtpToTime.Value = dtpToTime.Value.AddHours(1);
        }
        private void frmAddUpdateBooking_Load(object sender, EventArgs e)
        {
            _ResetDefaultValue();
            //in This Form i don't want to use update therefore i did this if statement broken.
            //in Future when i want to use update in this form i will again write my code in this if statement....
            //if (Mode == enMode.Update) 
            //{

            //}
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

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some fields are not valid!", "Validation Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _Booking.TeamID = clsTeam.GetTeamIDByTeamName(txtTeamName.Text);
            _Booking.CreatedByUserID = clsGlobal.CurrentUser.UserID;
            _Booking.FromTime = new TimeSpan(dtpFromTime.Value.Hour, dtpFromTime.Value.Minute, 0);
            _Booking.ToTime = new TimeSpan(dtpToTime.Value.Hour, dtpToTime.Value.Minute, 0);
          
            _Booking.BookingDate = new DateTime(dtpBookingDate.Value.Year, dtpBookingDate.Value.Month, dtpBookingDate.Value.Day);
            _Booking.DayName = _Booking.BookingDate.DayOfWeek.ToString();

            if (clsConstantBooking.IsTimeBooked(_Booking.DayName, _Booking.FromTime, _Booking.ToTime))
            {
                MessageBox.Show("Sorry! this time is Constant booked! a team has booked constant", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (clsBooking.IsTimeBooked(_Booking.BookingDate, _Booking.FromTime, _Booking.ToTime))
            {
                MessageBox.Show("Sorry! this time is booked!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
               
            if(_Booking.Save())
            {
                //i don't want update mode in this form...
                //this.Text = "Update Booking";
                //lblTitle.Text = "Update Booking";
                //Mode = enMode.Update;
                txtTeamName.Text = "";
                MessageBox.Show("Data Saved Successfully!", "Succeded", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                MessageBox.Show("Data Saved Faild!", "Faild!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
    }
}
