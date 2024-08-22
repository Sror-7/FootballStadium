namespace FootballStadium
{
    partial class frmAddUpdateTeam
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
            this.components = new System.ComponentModel.Container();
            this.lblTitle = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtTeamName = new System.Windows.Forms.TextBox();
            this.nudChampionshipsCount = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.richtxtNotes = new System.Windows.Forms.RichTextBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnAddPlayers = new System.Windows.Forms.Button();
            this.btnAddCoachInfo = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnAddUpdateTeam = new System.Windows.Forms.Button();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.nudChampionshipsCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Yu Gothic UI", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblTitle.Location = new System.Drawing.Point(206, 15);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(261, 47);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Add New Team";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Yu Gothic UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(107, 268);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(119, 25);
            this.label1.TabIndex = 3;
            this.label1.Text = "Team Name:";
            // 
            // txtTeamName
            // 
            this.txtTeamName.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTeamName.Location = new System.Drawing.Point(232, 268);
            this.txtTeamName.Name = "txtTeamName";
            this.txtTeamName.Size = new System.Drawing.Size(224, 29);
            this.txtTeamName.TabIndex = 4;
            this.txtTeamName.Validating += new System.ComponentModel.CancelEventHandler(this.txtTeamName_Validating);
            // 
            // nudChampionshipsCount
            // 
            this.nudChampionshipsCount.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudChampionshipsCount.Location = new System.Drawing.Point(232, 312);
            this.nudChampionshipsCount.Name = "nudChampionshipsCount";
            this.nudChampionshipsCount.Size = new System.Drawing.Size(224, 29);
            this.nudChampionshipsCount.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Yu Gothic UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(21, 312);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(205, 25);
            this.label2.TabIndex = 6;
            this.label2.Text = "Championships Count:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Yu Gothic UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(158, 358);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 25);
            this.label4.TabIndex = 15;
            this.label4.Text = "Notes:";
            // 
            // richtxtNotes
            // 
            this.richtxtNotes.Font = new System.Drawing.Font("Yu Gothic UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richtxtNotes.Location = new System.Drawing.Point(230, 358);
            this.richtxtNotes.Name = "richtxtNotes";
            this.richtxtNotes.Size = new System.Drawing.Size(226, 68);
            this.richtxtNotes.TabIndex = 16;
            this.richtxtNotes.Text = "";
            // 
            // btnClose
            // 
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Image = global::FootballStadium.Properties.Resources.close__13_;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(599, 631);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(45, 39);
            this.btnClose.TabIndex = 14;
            this.btnClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnAddPlayers
            // 
            this.btnAddPlayers.Enabled = false;
            this.btnAddPlayers.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddPlayers.Font = new System.Drawing.Font("Yu Gothic UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddPlayers.ForeColor = System.Drawing.Color.DarkGreen;
            this.btnAddPlayers.Image = global::FootballStadium.Properties.Resources.football_team;
            this.btnAddPlayers.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAddPlayers.Location = new System.Drawing.Point(197, 530);
            this.btnAddPlayers.Name = "btnAddPlayers";
            this.btnAddPlayers.Size = new System.Drawing.Size(270, 71);
            this.btnAddPlayers.TabIndex = 10;
            this.btnAddPlayers.Text = "Add Players";
            this.btnAddPlayers.UseVisualStyleBackColor = true;
            this.btnAddPlayers.Click += new System.EventHandler(this.btnAddPlayers_Click);
            // 
            // btnAddCoachInfo
            // 
            this.btnAddCoachInfo.Enabled = false;
            this.btnAddCoachInfo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddCoachInfo.Font = new System.Drawing.Font("Yu Gothic UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddCoachInfo.ForeColor = System.Drawing.Color.DarkGreen;
            this.btnAddCoachInfo.Image = global::FootballStadium.Properties.Resources.coach__3_;
            this.btnAddCoachInfo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAddCoachInfo.Location = new System.Drawing.Point(197, 453);
            this.btnAddCoachInfo.Name = "btnAddCoachInfo";
            this.btnAddCoachInfo.Size = new System.Drawing.Size(270, 71);
            this.btnAddCoachInfo.TabIndex = 9;
            this.btnAddCoachInfo.Text = "Add Coach Info";
            this.btnAddCoachInfo.UseVisualStyleBackColor = true;
            this.btnAddCoachInfo.Click += new System.EventHandler(this.btnAddCoachInfo_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::FootballStadium.Properties.Resources.team__4_;
            this.pictureBox1.Location = new System.Drawing.Point(12, 65);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(626, 125);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // btnAddUpdateTeam
            // 
            this.btnAddUpdateTeam.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddUpdateTeam.Location = new System.Drawing.Point(471, 270);
            this.btnAddUpdateTeam.Name = "btnAddUpdateTeam";
            this.btnAddUpdateTeam.Size = new System.Drawing.Size(98, 23);
            this.btnAddUpdateTeam.TabIndex = 1;
            this.btnAddUpdateTeam.Text = "Add Team";
            this.btnAddUpdateTeam.UseVisualStyleBackColor = true;
            this.btnAddUpdateTeam.Click += new System.EventHandler(this.btnAddUpdateTeam_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // frmAddUpdateTeam
            // 
            this.AcceptButton = this.btnAddUpdateTeam;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(650, 673);
            this.Controls.Add(this.btnAddUpdateTeam);
            this.Controls.Add(this.richtxtNotes);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnAddPlayers);
            this.Controls.Add(this.btnAddCoachInfo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.nudChampionshipsCount);
            this.Controls.Add(this.txtTeamName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmAddUpdateTeam";
            this.Text = "Add New Team";
            this.Load += new System.EventHandler(this.frmAddNewTeam_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudChampionshipsCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtTeamName;
        private System.Windows.Forms.NumericUpDown nudChampionshipsCount;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnAddCoachInfo;
        private System.Windows.Forms.Button btnAddPlayers;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RichTextBox richtxtNotes;
        private System.Windows.Forms.Button btnAddUpdateTeam;
        private System.Windows.Forms.ErrorProvider errorProvider1;
    }
}