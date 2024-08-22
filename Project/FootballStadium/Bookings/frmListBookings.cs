using FootballStadium.Global_Classes;
using FootballStadium_Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FootballStadium
{
    public partial class frmListBookings : Form
    {
        private static DataTable _dtAllBookings;
        bool _OldData = false;
        public frmListBookings()
        {
            InitializeComponent();
        }
        public frmListBookings(bool OldData)
        {
            _OldData = OldData;
            InitializeComponent();
        }
        private void _RefreshBookingsInfo()
        {
            if (!_OldData)
                _dtAllBookings = clsBooking.GetBookings();
            else
                _dtAllBookings = clsBooking.GetOldBookings();

            dgvBookings.DataSource = _dtAllBookings;
            lblRecordsCount.Text = dgvBookings.Rows.Count.ToString();
            if (dgvBookings.Rows.Count > 0)
            {
                cbFilterBy.Enabled = true;
                contextMenuStrip1.Enabled = true;
            }
            else
            {
                cbFilterBy.Enabled = false;
                contextMenuStrip1.Enabled = false;
            }
        }
        private void _UserPermission()
        {

            clsUserPermission UserPermission = clsUserPermission.Find(clsGlobal.CurrentUser.UserID, "Bookings");

            btnAdd.Enabled = UserPermission.Add;
            deleteToolStripMenuItem.Enabled = UserPermission.Delete;
        }
        private void Bookings_Load(object sender, EventArgs e)
        {
            _UserPermission();



            if (!_OldData)
                _dtAllBookings = clsBooking.GetBookings();
            else
            {
                btnAdd.Visible = false;
                _dtAllBookings = clsBooking.GetOldBookings();
            }

            dgvBookings.DataSource = _dtAllBookings;
            lblRecordsCount.Text = dgvBookings.Rows.Count.ToString();
            cbFilterBy.SelectedIndex = 0;
            if(dgvBookings.Rows.Count > 0)
            {
                dgvBookings.Columns[0].HeaderText = "Booking ID";
                dgvBookings.Columns[0].Width = 40;

                dgvBookings.Columns[1].HeaderText = "Team ID";
                dgvBookings.Columns[1].Width = 70;

                dgvBookings.Columns[2].HeaderText = "Team Name";
                dgvBookings.Columns[2].Width = 180;

                dgvBookings.Columns[3].HeaderText = "Date";
                dgvBookings.Columns[3].Width = 80;

                dgvBookings.Columns[4].HeaderText = "Day Name";
                dgvBookings.Columns[4].Width = 140;

                dgvBookings.Columns[5].HeaderText = "From Time";
                dgvBookings.Columns[5].Width = 120;

                dgvBookings.Columns[6].HeaderText = "To Time";
                dgvBookings.Columns[6].Width = 160;
            }
            else
            {
                cbFilterBy.Enabled = false;
                contextMenuStrip1.Enabled = false;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Form frm = new frmAddUpdateBooking();
            frm.ShowDialog();
            _RefreshBookingsInfo();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int BookingID = (int)dgvBookings.CurrentRow.Cells[0].Value;
            if(MessageBox.Show($"Are you sure you want to Delete booking with ID [{BookingID}]?", "Confirm Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Question)==DialogResult.OK)
            {

                if(clsBooking.DeleteBooking(BookingID))
                {
                    MessageBox.Show("Booking Deleted Successfully!", "Deleted Succeded!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _RefreshBookingsInfo() ;
                    return;
                }
                else
                {
                    MessageBox.Show("Error: Booking Deleting Faild!", "Deleted Faild!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

            }
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Visible = (cbFilterBy.Text != "None" && cbFilterBy.Text != "Date");
            dtpFilter.Visible = (cbFilterBy.Text == "Date");

            if (txtFilterValue.Visible)
            {
                txtFilterValue.Text = "";
                txtFilterValue.Focus();
            }
            if (cbFilterBy.Text == "None")
            {
                _dtAllBookings.DefaultView.RowFilter = "";
                lblRecordsCount.Text = dgvBookings.Rows.Count.ToString();
                return;
            }
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {

            string FilterColumn = "";
            //Map Selected Filter to real Column name 
            switch (cbFilterBy.Text)
            {
                case "Booking ID":
                    FilterColumn = "BookingID";
                    break;

                case "Team ID":
                    FilterColumn = "TeamID";
                    break;

                case "Team Name":
                    FilterColumn = "TeamName";
                    break;

                default:
                    FilterColumn = "None";
                    break;

            }

            //Reset the filters in case nothing selected or filter value conains nothing.
            if (txtFilterValue.Text.Trim() == "" || FilterColumn == "None")
            {
                _dtAllBookings.DefaultView.RowFilter = "";
                lblRecordsCount.Text = dgvBookings.Rows.Count.ToString();
                return;
            }
            
            if (FilterColumn == "TeamID" || FilterColumn == "BookingID")
                //in this case we deal with integer not string.

                _dtAllBookings.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilterValue.Text.Trim());
        
            else
                _dtAllBookings.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, txtFilterValue.Text.Trim());

            lblRecordsCount.Text = dgvBookings.Rows.Count.ToString();
        }

        private void dtpFilter_ValueChanged(object sender, EventArgs e)
        {
            if (cbFilterBy.Text == "Date")
            {
                _dtAllBookings.DefaultView.RowFilter = "BookingDate  >= '" + dtpFilter.Value.Date + "' and  BookingDate  <= '" +
                  dtpFilter.Value.Date + "'";

                lblRecordsCount.Text = dgvBookings.Rows.Count.ToString();
            }

        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilterBy.Text == "Team ID" || cbFilterBy.Text == "Booking ID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void showConstantBookingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmListConstantBookings();
            frm.ShowDialog();
        }

        private void frmListBookings_KeyPress(object sender, KeyPressEventArgs e)
        {       
        }

        private void frmListBookings_Enter(object sender, EventArgs e)
        {
        }
    }
}
