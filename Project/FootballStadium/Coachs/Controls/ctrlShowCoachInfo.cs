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
    public partial class ctrlShowCoachInfo : UserControl
    {
        private clsCoach _Coach = new clsCoach();
        public ctrlShowCoachInfo()
        {
            InitializeComponent();
        }
        public void LoadCoachInfo(int CoachID)
        {
            _Coach = clsCoach.Find(CoachID);

            if(_Coach != null ) 
            {
                clsTeam Team = clsTeam.Find(_Coach.TeamID);
                if(Team != null )
                    lblTeamName.Text = Team.TeamName;

                lblCoachName.Text = _Coach.PersonInfo.FullName;
                lblYearsOfExperience.Text = _Coach.YearsOfExperience.ToString();
                lblChampionshipsCount.Text = _Coach.ChampionshipsCount.ToString();
                lblDateOfBirth.Text = _Coach.PersonInfo.DateOfBirth.ToShortDateString();
                lblDateOfAccession.Text = _Coach.CreatedDate.ToShortDateString();
                lblPhone.Text = _Coach.PersonInfo.Phone;
                lblAddress.Text = _Coach.PersonInfo.Address;
                lblCreatedByUserID.Text = _Coach.CreatedByUserID.ToString();

                linlblEditCoachInfo.Enabled = true;
            }
            else
            {
                MessageBox.Show($"Coach with CoachID {CoachID} not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
        private void linlblEditCoachInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form frm = new frmAddUpdateCoach(_Coach.CoachID);
            frm.ShowDialog();
            LoadCoachInfo(_Coach.CoachID);
        }

        private void ctrlShowCoachInfo_Load(object sender, EventArgs e)
        {
            linlblEditCoachInfo.Enabled = false;
        }
    }
}
