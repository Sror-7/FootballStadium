using FootballStadium_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballStadium_Business
{
    public class clsCoach
    {
        enum enMode { AddNew = 1, Update = 2 }
        enMode Mode = enMode.AddNew;

        public clsPerson PersonInfo;

        public int CoachID { get; set; }
        public int PersonID { get; set; }
        public int TeamID { get; set; }
        public int YearsOfExperience { get; set; }
        public int ChampionshipsCount { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedByUserID { get; set; }


        public clsCoach()
        {
            this.CoachID = -1;
            this.PersonID = -1;
            this.PersonInfo = new clsPerson();
            this.TeamID = -1;
            this.YearsOfExperience = 0;
            this.ChampionshipsCount = 0;
            this.CreatedDate = DateTime.Now;
            this.CreatedByUserID = -1;

            Mode = enMode.AddNew;
        }
        private clsCoach(int coachID, int personID, int teamID, int yearsOfExperience, int ChampionshipsCount, DateTime CreatedDate, int createdByUserID)
        {
            this.CoachID = coachID;
            this.PersonID = personID;
            this.PersonInfo = clsPerson.Find(this.PersonID);
            this.TeamID = teamID;
            this.YearsOfExperience = yearsOfExperience;
            this.ChampionshipsCount = ChampionshipsCount;
            this.CreatedDate = CreatedDate;
            this.CreatedByUserID = createdByUserID;

            this.Mode = enMode.Update;
        }

        private bool _AddNewCoach()
        {
            if (PersonInfo.Save())
            {
                this.PersonID = PersonInfo.PersonID;
                this.CoachID = clsCoachData.AddNewCoach(this.PersonID, this.YearsOfExperience,this.ChampionshipsCount,this.CreatedDate,this.CreatedByUserID);

                return (this.CoachID != -1);
            }

            return false;
        }
        private bool _UpdateCoach()
        {
            if (PersonInfo.Save())
            {
                return clsCoachData.UpdateCoach(this.CoachID, this.YearsOfExperience,this.ChampionshipsCount,this.CreatedByUserID);
            }

            return false;
        }


        public static clsCoach Find(int CoachID)
        {
            int PersonID = -1, TeamID = -1, YearsOfExperience = 0, ChampionshipsCount = 0, CreatedByUserID = -1;
            DateTime CreatedDate = DateTime.Now;

            if (clsCoachData.GetCoachInfoByCoachID(CoachID, ref PersonID, ref TeamID, ref YearsOfExperience, ref ChampionshipsCount, ref CreatedDate, ref CreatedByUserID))
                return new clsCoach(CoachID, PersonID, TeamID, YearsOfExperience, ChampionshipsCount, CreatedDate, CreatedByUserID);

            else
                return null;
        }
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewCoach())
                    {

                        Mode = enMode.Update;

                        return true;
                    }
                    else
                        return false;

                case enMode.Update:
                    return _UpdateCoach();
            }

            return false;
        }
        public static bool IncrementChampionship(int CoachID)
        {
            return clsCoachData.IncrementChampionshipCount(CoachID);
        } 
        public bool IncrementChampionship()
        {
            return clsCoachData.IncrementChampionshipCount(this.CoachID);
        }
        public static DataTable GetAllCoachs()
        {
            return clsCoachData.GetAllCoachs();
        }
        public static bool Delete(int CoachID)
        {
            return clsCoachData.DeleteCoach(CoachID);
        }
        public bool Delete()
        {
            return clsCoachData.DeleteCoach(this.CoachID);
        }
    }
}
