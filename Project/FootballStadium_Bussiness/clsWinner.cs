using FootballStadium_DataAccess;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace FootballStadium_Business
{
    public class clsWinner
    {
        enum enMode { AddNew = 1, Update = 2 }
        enMode Mode = enMode.AddNew;

        public int WinnerID { get; set; }
        public int TeamID { get; set; }
        public int ChampionshipID { get; set; }
        public string Note { get; set; }
        public int CreatedByUserID { get; set; }

        public clsWinner()
        {
            this.WinnerID = -1;
            this.TeamID = -1;
            this.ChampionshipID = -1;
            this.Note = "";
            this.CreatedByUserID = -1;
        }
        private clsWinner(int WinnerID, int TeamID, int ChampionshipID,
            string Note, int CreatedByUserID)
        {
            this.WinnerID = WinnerID;
            this.TeamID = TeamID;
            this.ChampionshipID = ChampionshipID;
            this.Note = Note;
            this.CreatedByUserID = CreatedByUserID;
        }
        public static clsWinner Find(int ChampionshipID)
        {
            int WinnerID = -1, TeamID = -1, CreatedByUserID = -1;
            string Note = "";

            if (clsWinnerData.GetWinnerInfoByChampionshipID(ChampionshipID, ref WinnerID, ref TeamID, ref Note, ref CreatedByUserID))
                return new clsWinner(WinnerID, TeamID, ChampionshipID, Note, CreatedByUserID);

            else
                return null;
        }
        private bool _AddNewWinner()
        {
            this.WinnerID = clsWinnerData.AddNewWinner(this.TeamID, this.ChampionshipID, this.Note, this.CreatedByUserID);

            clsChampionship Championship = clsChampionship.Find(this.ChampionshipID);
            Championship.IsEndet = true;
            Championship.EndDate = DateTime.Now;

            if (Championship.Save())
            {
                clsTeam Team = clsTeam.Find(this.TeamID);
                if(Team.IncrementChampionship())
                {
                    if(Team.CoachID != null)
                    {
                        return Team.CoachInfo.IncrementChampionship();
                    }
                    return true;
                }
            }
            return false;
        }
        private bool _UpdateWinner()
        {
            return false;
        }
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewWinner())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;

                case enMode.Update:
                    return _UpdateWinner();
            }

            return false;
        }

    }
}
