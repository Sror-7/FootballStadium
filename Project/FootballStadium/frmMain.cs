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
    public partial class frmMain : Form
    {
        Form _frmLogin;
        public frmMain(Form frmLogin)
        {
            _frmLogin = frmLogin;
            InitializeComponent();
        }

        private void addNewTeamToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmAddUpdateTeam();
            frm.ShowDialog();
        }
        private void _UserPermission()
        {

            clsUserPermission UserPermission = clsUserPermission.Find(clsGlobal.CurrentUser.UserID, "Bookings");

            tsmBookingList.Enabled = UserPermission.Show;
            tsmOldBookings.Enabled = UserPermission.Show;
            tsmConstant.Enabled = UserPermission.Show;

            UserPermission = clsUserPermission.Find(clsGlobal.CurrentUser.UserID, "Teams");
            tsmShowTeams.Enabled = UserPermission.Show;
            tsmAddNewTeam.Enabled = UserPermission.Add;


            UserPermission = clsUserPermission.Find(clsGlobal.CurrentUser.UserID, "Championships");
            tsmChampionships.Enabled = UserPermission.Show;
            tsmShowChampionshipsHistory.Enabled = UserPermission.Show;
            tsmShowCurrentChampion.Enabled = UserPermission.Show;
            tsmAddNewChampion.Enabled = UserPermission.Add;


            UserPermission = clsUserPermission.Find(clsGlobal.CurrentUser.UserID, "Coachs");
            tsmCoachList.Enabled = UserPermission.Show;


            UserPermission = clsUserPermission.Find(clsGlobal.CurrentUser.UserID, "Users");
            tsmShowCurrentUserInfo.Enabled = UserPermission.Show;
            tsmListUsers.Enabled = UserPermission.Show;
        }
        private void frmMain_Load(object sender, EventArgs e)
        {
            _UserPermission();
        }

        private void showTeamsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmListTeams();
            frm.ShowDialog();
        }

        private void showCurrentUserInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmShowUserInfo(clsGlobal.CurrentUser.UserID);
            frm.ShowDialog();
        }

        private void showUsersInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmListUsers();
            frm.ShowDialog();
        }

        private void ShowBookingsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void addNewChampionshipToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form frm = new frmAddUpdateChampionship();
            frm.ShowDialog();

        }

        private void addNewChampionshipToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmListCurrentChampionships();
            frm.ShowDialog();
        }

        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmListEndetChampionships();
            frm.ShowDialog();
        }

        private void showCoachsListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmListCoachs();
            frm.ShowDialog();
        }

        private void showBookingsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form frm = new frmListBookings();
            frm.ShowDialog();
        }

        private void showToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form frm = new frmListBookings(true);
            frm.ShowDialog();
        }

        private void constantBookingsListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmListConstantBookings();
            frm.ShowDialog();
        }

        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsGlobal.CurrentUser = null;
            _frmLogin.Show();
            this.Close();
        }

        private void changePasswordAndUsernameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmChangePassword(clsGlobal.CurrentUser.UserID);
            frm.ShowDialog();
        }
    }
}
