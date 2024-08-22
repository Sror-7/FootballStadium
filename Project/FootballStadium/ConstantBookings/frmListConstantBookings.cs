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
    public partial class frmListConstantBookings : Form
    {
        private DataTable _dtAllConstantBookings;
        public frmListConstantBookings()
        {
            InitializeComponent();
        }
        private void _RefreshConstantBookingsList()
        {
            _dtAllConstantBookings = clsConstantBooking.GetConstantBookings();
            dgvConstantBookings.DataSource = _dtAllConstantBookings;
            cbFilterBy.SelectedIndex = 0;
            lblRecordsCount.Text = dgvConstantBookings.Rows.Count.ToString();

            if (dgvConstantBookings.Rows.Count > 0)
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
        private void frmListConstantBookings_Load(object sender, EventArgs e)
        {
            _UserPermission();


            _dtAllConstantBookings = clsConstantBooking.GetConstantBookings();
            dgvConstantBookings.DataSource = _dtAllConstantBookings;
            cbFilterBy.SelectedIndex = 0;
            lblRecordsCount.Text = dgvConstantBookings.Rows.Count.ToString();

            if (dgvConstantBookings.Rows.Count > 0)
            {
                dgvConstantBookings.Columns[0].HeaderText = "ConstantBooking ID";
                dgvConstantBookings.Columns[0].Width = 40;

                dgvConstantBookings.Columns[1].HeaderText = "Team Name";
                dgvConstantBookings.Columns[1].Width = 100;

                dgvConstantBookings.Columns[2].HeaderText = "Day Name";
                dgvConstantBookings.Columns[2].Width = 20;

                dgvConstantBookings.Columns[3].HeaderText = "From Time";
                dgvConstantBookings.Columns[3].Width = 60;

                dgvConstantBookings.Columns[4].HeaderText = "To Time";
                dgvConstantBookings.Columns[4].Width = 60;

                dgvConstantBookings.Columns[5].HeaderText = "Created Date";
                dgvConstantBookings.Columns[5].Width = 150;
            }
            else
            {
                cbFilterBy.Enabled = false;
                contextMenuStrip1.Enabled = false;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Form frm = new frmAddUpdateConstantBooking();
            frm.ShowDialog();
            _RefreshConstantBookingsList();
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Visible = (cbFilterBy.Text != "None" && cbFilterBy.Text != "Created Date");
            dtpFilter.Visible = (cbFilterBy.Text == "Created Date");

            if (txtFilterValue.Visible)
            {
                txtFilterValue.Text = "";
                txtFilterValue.Focus();
            }
            if (cbFilterBy.Text == "None")
            {
                _dtAllConstantBookings.DefaultView.RowFilter = "";
                lblRecordsCount.Text = dgvConstantBookings.Rows.Count.ToString();
                return;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";
            //Map Selected Filter to real Column name 
            switch (cbFilterBy.Text)
            {
                case "Constant Booking ID":
                    FilterColumn = "ConstantBookingID";
                    break;

                case "Day Name":
                    FilterColumn = "DayName";
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
                _dtAllConstantBookings.DefaultView.RowFilter = "";
                lblRecordsCount.Text = dgvConstantBookings.Rows.Count.ToString();
                return;
            }

            if (FilterColumn == "ConstantBookingID" || FilterColumn == "BookingID")
                //in this case we deal with integer not string.

                _dtAllConstantBookings.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilterValue.Text.Trim());

            else
                _dtAllConstantBookings.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, txtFilterValue.Text.Trim());

            lblRecordsCount.Text = dgvConstantBookings.Rows.Count.ToString();
        }

        private void dtpFilter_ValueChanged(object sender, EventArgs e)
        {
            if (cbFilterBy.Text == "Created Date")
            {
                _dtAllConstantBookings.DefaultView.RowFilter = "CreatedDate  >= '" + dtpFilter.Value.Date + "' and  CreatedDate  <= '" +
                  dtpFilter.Value.Date + "'";

                lblRecordsCount.Text = dgvConstantBookings.Rows.Count.ToString();
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int ConstantBookingID = (int)dgvConstantBookings.CurrentRow.Cells[0].Value;
            if (MessageBox.Show($"Are you sure you want to Delete booking with ID [{ConstantBookingID}]?", "Confirm Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {

                if (clsConstantBooking.Delete(ConstantBookingID))
                {
                    MessageBox.Show("Booking Deleted Successfully!", "Deleted Succeded!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _RefreshConstantBookingsList();
                    return;
                }
                else
                {
                    MessageBox.Show("Error: Booking Deleting Faild!", "Deleted Faild!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilterBy.Text == "Constant Booking ID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}
