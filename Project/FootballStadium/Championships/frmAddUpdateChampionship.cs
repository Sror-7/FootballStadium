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
    public partial class frmAddUpdateChampionship : Form
    {
        private clsChampionship _Championship;
        enum enMode { AddNew = 1, Update = 2 }
        enMode Mode = enMode.AddNew;
        private int _ChampionshipID = -1;
        public frmAddUpdateChampionship()
        {
            InitializeComponent();
        }
        public frmAddUpdateChampionship(int ChampionshipID)
        {
            _ChampionshipID = ChampionshipID;
            Mode = enMode.Update;
            InitializeComponent();
        }

        private void _ResetDefaultValues()
        {
            if(Mode == enMode.AddNew)
            {
                this.Text = "Add Champoinship";
                lblTitle.Text = "Add New Championship";
                _Championship = new clsChampionship();
            }
            else
            {
                this.Text = "Update Champoinship";
                lblTitle.Text = "Update Championship Info";
            }
            txtChampionshipName.Text = "";
            dtpStartDate.Value = DateTime.Now;
            dtpStartDate.MinDate = DateTime.Now;
            richtxtNote.Text = "";
        }
        private void _LoadData()
        {
            _Championship = clsChampionship.Find(_ChampionshipID);

            if(_Championship != null)
            {
                txtChampionshipName.Text = _Championship.Name;

                if (_Championship.StartDate < DateTime.Now)
                    dtpStartDate.Enabled = false;
                else
                    dtpStartDate.Value = _Championship.StartDate;

                richtxtNote.Text = _Championship.Note;
            }
            else
            {
                MessageBox.Show($"Error: Championship with ID [{_ChampionshipID}] not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmAddNewChampionship_Load(object sender, EventArgs e)
        {
            _ResetDefaultValues();
            if (Mode == enMode.Update)
            {
                _LoadData();
            }
        }

        private void txtChampionshipName_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtChampionshipName.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtChampionshipName, "Error: Write Championship Name.");
                return;
            }
            else
                errorProvider1.SetError(txtChampionshipName, null);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some fields are not valid!", "Validation Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _Championship.Name = txtChampionshipName.Text;
            _Championship.StartDate = dtpStartDate.Value.Date;
            _Championship.Note = richtxtNote.Text;
            if (Mode == enMode.AddNew)
            {
                _Championship.EnrolledTeamsCount = 0;
                _Championship.CreatedDate = DateTime.Now;
            }
            _Championship.IsEndet = false;
            _Championship.CreatedByUserID = clsGlobal.CurrentUser.UserID;


            if(_Championship.Save())
            {
                this.Text = "Update";
                lblTitle.Text = "Update Championship Info";
                Mode = enMode.Update;

                MessageBox.Show("Data Saved Successfully!", "Succeded", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                MessageBox.Show("Error: Data Saved Faild!", "Faild", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
    }
}
