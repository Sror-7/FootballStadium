namespace FootballStadium
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.tsmBookings = new System.Windows.Forms.ToolStripMenuItem();
            this.BookingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmBookingList = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmOldBookings = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmConstant = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmTeams = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmShowTeams = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmAddNewTeam = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmCoachList = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmChampionships = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmShowCurrentChampion = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmShowChampionshipsHistory = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmAddNewChampion = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmUserManagment = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmShowCurrentUserInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmListUsers = new System.Windows.Forms.ToolStripMenuItem();
            this.changePasswordAndUsernameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.logOutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmBookings,
            this.tsmTeams,
            this.tsmChampionships,
            this.tsmUserManagment});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1513, 72);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // tsmBookings
            // 
            this.tsmBookings.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.BookingsToolStripMenuItem});
            this.tsmBookings.Font = new System.Drawing.Font("Yu Gothic UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsmBookings.Image = ((System.Drawing.Image)(resources.GetObject("tsmBookings.Image")));
            this.tsmBookings.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmBookings.Name = "tsmBookings";
            this.tsmBookings.Size = new System.Drawing.Size(166, 68);
            this.tsmBookings.Text = "Bookings";
            // 
            // BookingsToolStripMenuItem
            // 
            this.BookingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmBookingList,
            this.tsmOldBookings,
            this.tsmConstant});
            this.BookingsToolStripMenuItem.Font = new System.Drawing.Font("Yu Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BookingsToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.BookingsToolStripMenuItem.Image = global::FootballStadium.Properties.Resources.booking__2_;
            this.BookingsToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.BookingsToolStripMenuItem.Name = "BookingsToolStripMenuItem";
            this.BookingsToolStripMenuItem.Size = new System.Drawing.Size(165, 38);
            this.BookingsToolStripMenuItem.Text = "Bookings";
            this.BookingsToolStripMenuItem.Click += new System.EventHandler(this.ShowBookingsToolStripMenuItem_Click);
            // 
            // tsmBookingList
            // 
            this.tsmBookingList.Font = new System.Drawing.Font("Yu Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsmBookingList.Image = global::FootballStadium.Properties.Resources.appointment__1_;
            this.tsmBookingList.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmBookingList.Name = "tsmBookingList";
            this.tsmBookingList.Size = new System.Drawing.Size(234, 30);
            this.tsmBookingList.Text = "Bookings List";
            this.tsmBookingList.Click += new System.EventHandler(this.showBookingsToolStripMenuItem1_Click);
            // 
            // tsmOldBookings
            // 
            this.tsmOldBookings.Font = new System.Drawing.Font("Yu Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsmOldBookings.Image = global::FootballStadium.Properties.Resources.cancel_booking;
            this.tsmOldBookings.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmOldBookings.Name = "tsmOldBookings";
            this.tsmOldBookings.Size = new System.Drawing.Size(234, 30);
            this.tsmOldBookings.Text = "Old Bookings List";
            this.tsmOldBookings.Click += new System.EventHandler(this.showToolStripMenuItem1_Click);
            // 
            // tsmConstant
            // 
            this.tsmConstant.Font = new System.Drawing.Font("Yu Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsmConstant.Image = global::FootballStadium.Properties.Resources.constant;
            this.tsmConstant.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmConstant.Name = "tsmConstant";
            this.tsmConstant.Size = new System.Drawing.Size(234, 30);
            this.tsmConstant.Text = "Constant Bookings List";
            this.tsmConstant.Click += new System.EventHandler(this.constantBookingsListToolStripMenuItem_Click);
            // 
            // tsmTeams
            // 
            this.tsmTeams.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmShowTeams,
            this.tsmAddNewTeam,
            this.toolStripSeparator3,
            this.tsmCoachList});
            this.tsmTeams.Font = new System.Drawing.Font("Yu Gothic UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsmTeams.Image = global::FootballStadium.Properties.Resources.team__1_;
            this.tsmTeams.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmTeams.Name = "tsmTeams";
            this.tsmTeams.Size = new System.Drawing.Size(141, 68);
            this.tsmTeams.Text = "Teams";
            // 
            // tsmShowTeams
            // 
            this.tsmShowTeams.Font = new System.Drawing.Font("Yu Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsmShowTeams.Image = global::FootballStadium.Properties.Resources.club;
            this.tsmShowTeams.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmShowTeams.Name = "tsmShowTeams";
            this.tsmShowTeams.Size = new System.Drawing.Size(207, 38);
            this.tsmShowTeams.Text = "Show Teams";
            this.tsmShowTeams.Click += new System.EventHandler(this.showTeamsToolStripMenuItem_Click);
            // 
            // tsmAddNewTeam
            // 
            this.tsmAddNewTeam.Font = new System.Drawing.Font("Yu Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsmAddNewTeam.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.tsmAddNewTeam.Image = global::FootballStadium.Properties.Resources.football_uniform;
            this.tsmAddNewTeam.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmAddNewTeam.Name = "tsmAddNewTeam";
            this.tsmAddNewTeam.Size = new System.Drawing.Size(207, 38);
            this.tsmAddNewTeam.Text = "Add New Team";
            this.tsmAddNewTeam.Click += new System.EventHandler(this.addNewTeamToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(204, 6);
            // 
            // tsmCoachList
            // 
            this.tsmCoachList.Font = new System.Drawing.Font("Yu Gothic UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsmCoachList.Image = global::FootballStadium.Properties.Resources.coach__7_;
            this.tsmCoachList.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmCoachList.Name = "tsmCoachList";
            this.tsmCoachList.Size = new System.Drawing.Size(207, 38);
            this.tsmCoachList.Text = "Coachs List";
            this.tsmCoachList.Click += new System.EventHandler(this.showCoachsListToolStripMenuItem_Click);
            // 
            // tsmChampionships
            // 
            this.tsmChampionships.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmShowCurrentChampion,
            this.tsmShowChampionshipsHistory,
            this.toolStripSeparator1,
            this.tsmAddNewChampion});
            this.tsmChampionships.Font = new System.Drawing.Font("Yu Gothic UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsmChampionships.Image = global::FootballStadium.Properties.Resources.world_cup;
            this.tsmChampionships.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmChampionships.Name = "tsmChampionships";
            this.tsmChampionships.Size = new System.Drawing.Size(219, 68);
            this.tsmChampionships.Text = "Championships";
            // 
            // tsmShowCurrentChampion
            // 
            this.tsmShowCurrentChampion.Font = new System.Drawing.Font("Yu Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsmShowCurrentChampion.ForeColor = System.Drawing.Color.Black;
            this.tsmShowCurrentChampion.Image = global::FootballStadium.Properties.Resources.soccer;
            this.tsmShowCurrentChampion.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmShowCurrentChampion.Name = "tsmShowCurrentChampion";
            this.tsmShowCurrentChampion.Size = new System.Drawing.Size(311, 38);
            this.tsmShowCurrentChampion.Text = "Show Current Championship";
            this.tsmShowCurrentChampion.Click += new System.EventHandler(this.addNewChampionshipToolStripMenuItem_Click);
            // 
            // tsmShowChampionshipsHistory
            // 
            this.tsmShowChampionshipsHistory.Font = new System.Drawing.Font("Yu Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsmShowChampionshipsHistory.Image = global::FootballStadium.Properties.Resources.history;
            this.tsmShowChampionshipsHistory.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmShowChampionshipsHistory.Name = "tsmShowChampionshipsHistory";
            this.tsmShowChampionshipsHistory.Size = new System.Drawing.Size(311, 38);
            this.tsmShowChampionshipsHistory.Text = "Show Championships History";
            this.tsmShowChampionshipsHistory.Click += new System.EventHandler(this.showToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(308, 6);
            // 
            // tsmAddNewChampion
            // 
            this.tsmAddNewChampion.Font = new System.Drawing.Font("Yu Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsmAddNewChampion.ForeColor = System.Drawing.Color.Green;
            this.tsmAddNewChampion.Image = global::FootballStadium.Properties.Resources.achievement_add__2_;
            this.tsmAddNewChampion.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmAddNewChampion.Name = "tsmAddNewChampion";
            this.tsmAddNewChampion.Size = new System.Drawing.Size(311, 38);
            this.tsmAddNewChampion.Text = "Add New Championship";
            this.tsmAddNewChampion.Click += new System.EventHandler(this.addNewChampionshipToolStripMenuItem1_Click);
            // 
            // tsmUserManagment
            // 
            this.tsmUserManagment.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmShowCurrentUserInfo,
            this.tsmListUsers,
            this.changePasswordAndUsernameToolStripMenuItem,
            this.toolStripSeparator2,
            this.logOutToolStripMenuItem});
            this.tsmUserManagment.Font = new System.Drawing.Font("Yu Gothic UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsmUserManagment.Image = global::FootballStadium.Properties.Resources.user_config;
            this.tsmUserManagment.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmUserManagment.Name = "tsmUserManagment";
            this.tsmUserManagment.Size = new System.Drawing.Size(118, 68);
            this.tsmUserManagment.Text = "Users";
            // 
            // tsmShowCurrentUserInfo
            // 
            this.tsmShowCurrentUserInfo.Font = new System.Drawing.Font("Yu Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsmShowCurrentUserInfo.Image = global::FootballStadium.Properties.Resources.user_info__3_;
            this.tsmShowCurrentUserInfo.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmShowCurrentUserInfo.Name = "tsmShowCurrentUserInfo";
            this.tsmShowCurrentUserInfo.Size = new System.Drawing.Size(270, 38);
            this.tsmShowCurrentUserInfo.Text = "Show Current User Info";
            this.tsmShowCurrentUserInfo.Click += new System.EventHandler(this.showCurrentUserInfoToolStripMenuItem_Click);
            // 
            // tsmListUsers
            // 
            this.tsmListUsers.Font = new System.Drawing.Font("Yu Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsmListUsers.Image = global::FootballStadium.Properties.Resources.users_info;
            this.tsmListUsers.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmListUsers.Name = "tsmListUsers";
            this.tsmListUsers.Size = new System.Drawing.Size(270, 38);
            this.tsmListUsers.Text = "Show Users";
            this.tsmListUsers.Click += new System.EventHandler(this.showUsersInfoToolStripMenuItem_Click);
            // 
            // changePasswordAndUsernameToolStripMenuItem
            // 
            this.changePasswordAndUsernameToolStripMenuItem.Font = new System.Drawing.Font("Yu Gothic UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.changePasswordAndUsernameToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("changePasswordAndUsernameToolStripMenuItem.Image")));
            this.changePasswordAndUsernameToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.changePasswordAndUsernameToolStripMenuItem.Name = "changePasswordAndUsernameToolStripMenuItem";
            this.changePasswordAndUsernameToolStripMenuItem.Size = new System.Drawing.Size(270, 38);
            this.changePasswordAndUsernameToolStripMenuItem.Text = "Change Password";
            this.changePasswordAndUsernameToolStripMenuItem.Click += new System.EventHandler(this.changePasswordAndUsernameToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(267, 6);
            // 
            // logOutToolStripMenuItem
            // 
            this.logOutToolStripMenuItem.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.logOutToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.logOutToolStripMenuItem.Image = global::FootballStadium.Properties.Resources.sign_out__2_;
            this.logOutToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.logOutToolStripMenuItem.Name = "logOutToolStripMenuItem";
            this.logOutToolStripMenuItem.Size = new System.Drawing.Size(270, 38);
            this.logOutToolStripMenuItem.Text = "Log Out";
            this.logOutToolStripMenuItem.Click += new System.EventHandler(this.logOutToolStripMenuItem_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ClientSize = new System.Drawing.Size(1513, 698);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "frmMain";
            this.Text = "Main";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsmBookings;
        private System.Windows.Forms.ToolStripMenuItem tsmTeams;
        private System.Windows.Forms.ToolStripMenuItem tsmChampionships;
        private System.Windows.Forms.ToolStripMenuItem tsmUserManagment;
        private System.Windows.Forms.ToolStripMenuItem tsmShowTeams;
        private System.Windows.Forms.ToolStripMenuItem tsmAddNewTeam;
        private System.Windows.Forms.ToolStripMenuItem BookingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmShowCurrentChampion;
        private System.Windows.Forms.ToolStripMenuItem tsmShowChampionshipsHistory;
        private System.Windows.Forms.ToolStripMenuItem tsmAddNewChampion;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem tsmShowCurrentUserInfo;
        private System.Windows.Forms.ToolStripMenuItem tsmListUsers;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem logOutToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem tsmCoachList;
        private System.Windows.Forms.ToolStripMenuItem tsmBookingList;
        private System.Windows.Forms.ToolStripMenuItem tsmOldBookings;
        private System.Windows.Forms.ToolStripMenuItem tsmConstant;
        private System.Windows.Forms.ToolStripMenuItem changePasswordAndUsernameToolStripMenuItem;
    }
}

