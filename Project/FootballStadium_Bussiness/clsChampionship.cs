using FootballStadium_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballStadium_Business
{
    public class clsChampionship
    {
        enum enMode { AddNew = 1, Update = 2 }
        enMode Mode = enMode.AddNew;

        public int ChampionshipID { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public int EnrolledTeamsCount { get; set; }
        public bool IsEndet { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Note { get; set; }
        public int CreatedByUserID { get; set; }

        public clsChampionship()
        {
            this.ChampionshipID = -1;
            this.Name = "";
            this.StartDate = DateTime.Now;
            this.EnrolledTeamsCount = 0;
            this.IsEndet = false;
            this.EndDate = DateTime.Now;
            this.CreatedDate = DateTime.Now;
            this.Note = "";
            this.CreatedByUserID = -1;
        }
        private clsChampionship(int ChampionshipID, string Name, DateTime StartDate, int EnrolledTeamsCount, bool IsEndet, DateTime EndDate, DateTime CreatedDate, 
            string Note, int CreatedByUserID)
        {
            this.ChampionshipID = ChampionshipID;
            this.Name = Name;
            this.StartDate = StartDate;
            this.EnrolledTeamsCount = EnrolledTeamsCount;
            this.IsEndet = IsEndet;
            this.EndDate = EndDate;
            this.CreatedDate = CreatedDate;
            this.Note = Note;
            this.CreatedByUserID = CreatedByUserID;

            this.Mode = enMode.Update;
        }
        private bool _AddNewChampionship()
        {
            this.ChampionshipID = clsChampionshipData.AddNewChampionship(this.Name, this.StartDate, this.EnrolledTeamsCount, this.IsEndet, this.EndDate, this.CreatedDate, this.Note, this.CreatedByUserID);
            
            return (this.ChampionshipID != -1);
        }
        private bool _UpdateChampionship()
        {
            return clsChampionshipData.UpdateChampionship(this.ChampionshipID, this.Name, this.StartDate, this.EnrolledTeamsCount, this.IsEndet, this.EndDate, this.CreatedDate, this.Note, this.CreatedByUserID);
        }
        public static clsChampionship Find(int ChampionshipID)
        {
            string Name = "", Note = "";
            DateTime StartDate = DateTime.Now, EndDate = new DateTime(), CreatedDate = DateTime.Now;
            int EnrolledTeamsCount = 0, CreatedByUserID = -1;
            bool IsEndet = false;

            if (clsChampionshipData.GetChampionshipInfoByID(ChampionshipID, ref Name, ref StartDate, ref EnrolledTeamsCount, ref IsEndet, ref EndDate, ref CreatedDate, ref Note, ref CreatedByUserID))
                return new clsChampionship(ChampionshipID, Name, StartDate, EnrolledTeamsCount, IsEndet, EndDate, CreatedDate, Note, CreatedByUserID);

            else
                return null;
            
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewChampionship())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;

                case enMode.Update:
                    return _UpdateChampionship();
            }

            return false;
        }

        public static DataTable GetEndetChampionships()
        {
            return clsChampionshipData.GetEndetChampionships();
        }   
        public static DataTable GetCurrentChampionships()
        {
            return clsChampionshipData.GetCurrentChampionships();
        }
        public static bool Delete(int ChampionshipID)
        {
            clsChampionshipData.DeleteChampionshipAndEnrolledTeams(ChampionshipID);
            return clsChampionshipData.DeleteChampionship(ChampionshipID);
        }
        public bool Delete()
        {
            clsChampionshipData.DeleteChampionshipAndEnrolledTeams(this.ChampionshipID);
            return clsChampionshipData.DeleteChampionship(this.ChampionshipID);
        }
        public static DataTable GetAllEnrolledTeamsInChampionship(int ChampionshipID)
        {
            return clsChampionshipData.GetAllEnrolledTeamsInChampionship(ChampionshipID);
        }
        public static int AddTeamToChampionship(int TeamID, int ChampionshipID, DateTime EnrolledTeamDate, int CreatedByUserID)
        {
            return clsChampionshipData.AddTeamToChampionship(TeamID, ChampionshipID, EnrolledTeamDate, CreatedByUserID);
        }
        public static bool IncrementTeamsCount(int ChampionshipID)
        {
            return clsChampionshipData.IncrementTeamsCount(ChampionshipID);
        }
        public static bool DecrementTeamsCount(int ChampionshipID)
        {
            return clsChampionshipData.DecrementTeamsCount(ChampionshipID);
        }
        public static bool IsTeamExistInChampionship(int TeamID, int ChampionshipID)
        {
            return clsChampionshipData.IsTeamExistInChampionship(TeamID, ChampionshipID);
        }
        public static bool RemoveTeamFromChampionship(int TeamID, int ChampionshipID)
        {
            return clsChampionshipData.RemoveTeamFromChampionship(TeamID, ChampionshipID);
        }
    }
}
