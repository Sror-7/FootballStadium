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
    public class clsPlayer
    {
        enum enMode { AddNew = 1, Update = 2 }
        enMode Mode = enMode.AddNew;

        public clsPerson PersonInfo;
        public clsTeam TeamInfo;
        public int PlayerID { get; set; }
        public int PersonID { get; set; }
        public int PlayerNumber { get; set; }
        public int TeamID { get; set; }
        public int CreatedByUserID { get; set; }
        public DateTime CreatedDate { get; set; }


        public clsPlayer()
        {
            this.PlayerID = -1;
            this.PersonID = -1;
            this.PersonInfo = new clsPerson();
            this.TeamInfo = new clsTeam();
            this.PlayerNumber = 0;
            this.CreatedByUserID = -1;
            this.CreatedDate = DateTime.Now;
            
            this.Mode = enMode.AddNew;
        }
        private clsPlayer(int playerID, int personID, int teamID, int playerNumber, int createdByUserID, DateTime CreatedDate)
        {
            this.PlayerID = playerID;
            this.PersonID = personID;
            this.PersonInfo = clsPerson.Find(this.PersonID);
            this.TeamID = teamID;
            this.TeamInfo = clsTeam.Find(this.TeamID);
            this.PlayerNumber = playerNumber;
            this.CreatedByUserID = createdByUserID;
            this.CreatedDate = CreatedDate;
            
            this.Mode = enMode.Update;
        }

        private bool _AddNewPlayer()
        {
            if(PersonInfo.Save())
            {
                this.PersonID = PersonInfo.PersonID;
                this.PlayerID = clsPlayerData.AddNewPlayer(this.PersonID, this.TeamID, this.PlayerNumber, this.CreatedByUserID, this.CreatedDate);

                if (this.PlayerID != -1)
                {
                    TeamInfo.PlayersCount++;
                    return TeamInfo.Save();
                }
                else
                    return false;
                //return (this.PlayerID != -1);
            }

            return false;
        }
        private bool _UpdatePlayer()
        {
            if(PersonInfo.Save())
            {
                return clsPlayerData.UpdatePlayer(this.PlayerID, this.TeamID, this.PlayerNumber, this.CreatedByUserID);
            }

            return false;
        }

        public static clsPlayer Find(int PlayerID)
        {
            int PersonID = -1 , TeamID = -1, PlayerNumber = 0, CreatedByUserID = -1;
            DateTime CreatedDate = DateTime.Now;

            if (clsPlayerData.GetPlayerInfoByPlayerID(PlayerID, ref PersonID, ref TeamID, ref PlayerNumber, ref CreatedDate, ref CreatedByUserID))
                return new clsPlayer(PlayerID, PersonID, TeamID, PlayerNumber, CreatedByUserID, CreatedDate);

            else
                return null;
        }
        public bool Save()
        {
            switch(Mode)
            {
                case enMode.AddNew:
                    if (_AddNewPlayer())
                    {

                        Mode = enMode.Update;

                        return true;
                    }
                    else
                        return false;

                case enMode.Update:
                    return _UpdatePlayer();
            }

            return false;
        }

        public bool Delete()
        {
                TeamInfo.PlayersCount--;

            if (TeamInfo.Save())
            {
                if (clsPlayerData.DeletePlayer(this.PlayerID))
                    return PersonInfo.Delete();
            }

            return false;
        }
    }
}
