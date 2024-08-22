using FootballStadium_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballStadium_Business
{
    public class clsUser
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public clsPerson PersonInfo;
        public int UserID { get; set; }
        public int PersonID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }

        public clsUser()

        {
            this.UserID = -1;
            this.PersonID = -1;
            PersonInfo = new clsPerson();
            this.UserName = "";
            this.Password = "";
            this.IsActive = true;
            this.CreatedDate = DateTime.Now;

            Mode = enMode.AddNew;
        }

        private clsUser(int UserID, int PersonID,  string UserName, string Password, bool IsActive, DateTime CreatedDate)
        {
            this.UserID = UserID;
            this.PersonID = PersonID;
            PersonInfo = clsPerson.Find(PersonID);
            this.UserName = UserName;
            this.Password = Password;
            this.IsActive = IsActive;
            this.CreatedDate = CreatedDate;

            Mode = enMode.Update;
        }

        private bool _AddNewUser()
        {
            if (PersonInfo.Save())
            {
                this.PersonID = PersonInfo.PersonID;
                this.UserID = clsUserData.AddNewUser(this.PersonID, this.UserName, this.Password, this.IsActive, this.CreatedDate);

                return (this.UserID != -1);
            }
            return false;
        }
        private bool _UpdateUser()
        {
            if(PersonInfo.Save()) 
            {
                return clsUserData.UpdateUser(this.UserID, this.UserName, this.Password, this.IsActive);
            }
            return false;
        }
        public static clsUser Find(int UserID) 
        {

            int PersonID = 0;
            string UserName = "", Password = "";
            bool IsActive = false;
            DateTime CreatedDate = DateTime.Now;

            bool IsFound = clsUserData.GetUserInfoByUserID(UserID, ref PersonID, ref UserName, ref Password, ref IsActive, ref CreatedDate);

            if (IsFound)
                return new clsUser(UserID, PersonID, UserName, Password, IsActive,CreatedDate);

            else
                return null;
        }
        public static clsUser FindByUserNameAndPassword(string UserName, string Password)
        {
            int UserID = -1, PersonID = -1;
            bool IsActive = false;
            DateTime CreatedDate = DateTime.Now;


            bool IsFound = clsUserData.GetUserInfoByUserNameAndPassword(ref UserID, ref PersonID, UserName, Password, ref IsActive, ref CreatedDate);

            if (IsFound)
                return new clsUser(UserID, PersonID, UserName, Password, IsActive, CreatedDate);

            else
                return null;

        }
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewUser())
                    {

                        Mode = enMode.Update;

                        return true;
                    }
                    else
                        return false;

                case enMode.Update:
                    return _UpdateUser();
            }

            return false;
        }
        public static DataTable GetAllUsers()
        {
            return clsUserData.GetAllUsers();
        }
        public static bool DeleteUser(int UserID) 
        {
            clsUser User = clsUser.Find(UserID);
            if (User == null)
                return false;

            if(clsUserData.DeleteUser(UserID))
            {
                return User.PersonInfo.Delete();
            }

            return false;
        }
        public bool Delete()
        {
            if (clsUserData.DeleteUser(this.UserID))
            {
                return PersonInfo.Delete();
            }
            return false;
        }
        public static bool IsUserNameUsed(string UserName)
        {
            return clsUserData.IsUserNameUsed(UserName);
        }
        public static bool Active(int UserID)
        {
            return clsUserData.ActiveUser(UserID);
        }
        public static bool Deactive(int UserID)
        {
            return clsUserData.ActiveUser(UserID, false);
        }
        public static bool isUserActive(int UserID)
        {
            return clsUserData.IsUserActivated(UserID);
        }
    }
}
