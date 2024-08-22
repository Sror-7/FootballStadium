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
    public partial class ctrlShowPlayerInfo : UserControl
    {
        private clsPlayer _Player = new clsPlayer();
        public ctrlShowPlayerInfo()
        {
            InitializeComponent();
        }
        public void LoadPlayerInfo(int PlayerID)
        {
            _Player = clsPlayer.Find(PlayerID);

            if(_Player != null)
            {
                lblTeamName.Text = _Player.TeamInfo.TeamName;
                lblPlayerName.Text = _Player.PersonInfo.FullName;
                lblPlayerNumber.Text = _Player.PlayerNumber.ToString();
                lblDateOfBirth.Text = _Player.PersonInfo.DateOfBirth.ToShortDateString();
                lblPhone.Text = _Player.PersonInfo.Phone;
                lblAddress.Text = _Player.PersonInfo.Address;
                lblDateOfAccession.Text = _Player.CreatedDate.ToShortDateString();
                lblCreatedByUserID.Text = _Player.CreatedByUserID.ToString();

                linlblEditPlayerInfo.Enabled = true;
            }
            else
            {
                MessageBox.Show($"Player with PlayerID ({PlayerID}) not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void linlblEditPlayerInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form frm = new frmAddUpdatePlayer(_Player);
            frm.ShowDialog();
        }

        private void ctrlShowPlayerInfo_Load(object sender, EventArgs e)
        {
            linlblEditPlayerInfo.Enabled = false;
        }
    }
}
