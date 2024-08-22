using FootballStadium_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FootballStadium_Business
{
    public class clsTeam
    {
        enum enMode { AddNew = 1, Update = 2 };
        enMode Mode = enMode.AddNew;

        public clsCoach CoachInfo;
        public int TeamID { get; set; }
        public string TeamName { get; set; }
        public Nullable<int> CoachID { get; set; }
        public int PlayersCount { get; set; }
        public int ChampionshipsCount { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedByUserID { get; set; }
        public string TeamDescription { get; set; }


        public clsTeam()
        {
            TeamID = -1;
            TeamName = "";
            CoachID = null;
            CoachInfo = new clsCoach();
            PlayersCount = 0;
            ChampionshipsCount = 0;
            CreatedDate = DateTime.Now;
            CreatedByUserID = 0;
            TeamDescription = "";
        }

        private clsTeam(int teamID, string teamName, Nullable<int> CoachID, int championshipsCount, int playersCount, DateTime CreatedDate, string teamDescription, int createdByUserID)
        {
            this.TeamID = teamID;
            this.TeamName = teamName;
            this.CoachID = CoachID;
            if (this.CoachID.HasValue)
                this.CoachInfo = clsCoach.Find(this.CoachID.Value);
            else
                this.CoachInfo = null;

            this.PlayersCount = playersCount;
            this.ChampionshipsCount = championshipsCount;
            this.CreatedDate = CreatedDate;
            this.CreatedByUserID = createdByUserID;
            this.TeamDescription = teamDescription;

            Mode = enMode.Update;
        }
        private bool _AddNewTeam()
        {
            this.TeamID = clsTeamData.AddNewTeam(this.TeamName, this.CoachID, this.PlayersCount, this.ChampionshipsCount, this.CreatedDate, this.CreatedByUserID, this.TeamDescription);

            return (this.TeamID != -1);
        }
        private bool _UpdateTeam()
        {
            return clsTeamData.UpdateTeam(this.TeamID, this.TeamName, this.CoachID, this.PlayersCount, this.ChampionshipsCount, this.CreatedDate, this.CreatedByUserID, this.TeamDescription);
        }
        public static clsTeam Find(int TeamID)
        {
            Nullable<int> CoachID = null;

            string TeamName = "", TeamDescription = "";
            int PlayersCount = 0, ChampionshipsCount = 0, CreatedByUserID = -1;
            DateTime CreatedDate = DateTime.Now;

            if (clsTeamData.GetTeamInfoByTeamID(TeamID, ref TeamName, ref CoachID, ref PlayersCount, ref ChampionshipsCount, ref CreatedDate, ref TeamDescription, ref CreatedByUserID))
                return new clsTeam(TeamID, TeamName,CoachID, ChampionshipsCount, PlayersCount, CreatedDate, TeamDescription, CreatedByUserID);

            else
                return null;
        }
        public static bool AddPlayer(int TeamID)
        {
            return clsTeamData.AddPlayer(TeamID);
        }
        public static bool IsTeamExist(string TeamName)
        {
            return clsTeamData.IsTeamExist(TeamName);
        }
        public bool Save()
        {
            switch(Mode)
            {
                case enMode.AddNew:
                    if (_AddNewTeam())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;

                case enMode.Update:
                    return _UpdateTeam();
            }

            return false;
        }
        public static bool IncrementChampionship(int TeamID)
        {
            return clsTeamData.IncrementChampionshipCount(TeamID);
        }
        public bool IncrementChampionship()
        {
            return clsTeamData.IncrementChampionshipCount(this.TeamID);
        }
        public static DataTable GetAllTeams()
        {
            return clsTeamData.GetAllTeams();
        }
        public static DataTable GetTeamsHasNoCoachs()
        {
            return clsTeamData.GetTeamsHasNoCoachs();
        }
        public static DataTable GetPlayersInfo(int TeamID)
        {
            return clsTeamData.GetPlayersInfo(TeamID);
        }
        public DataTable GetPlayersInfo()
        {
            return clsTeamData.GetPlayersInfo(this.TeamID);
        }
        public bool Delete()
        {
            return clsTeamData.DeleteTeam(this.TeamID);
        }
        public static bool Delete(int TeamID)
        {
            return clsTeamData.DeleteTeam(TeamID);
        }
        public bool DoesTeamHaveCoach()
        {
            return clsTeamData.DoesTeamHaveCoach(this.TeamID);
        }
        public static bool DoesTeamHaveCoach(int TeamID)
        {
            return clsTeamData.DoesTeamHaveCoach(TeamID);
        }
        public static int GetTeamIDByTeamName(string TeamName)
        {
            return clsTeamData.GetTeamIDByTeamName(TeamName);
        }
    }
}
