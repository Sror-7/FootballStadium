using FootballStadium_DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballStadium_Business
{
    public class clsPerson
    {
        enum enMode { AddNew = 1, Update = 2 }
        enMode Mode = enMode.AddNew;

        public int PersonID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName
        {
            get { return FirstName + " " + LastName; }
        }
        public DateTime DateOfBirth { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        
        public clsPerson()
        {
            this.PersonID = -1;
            this.FirstName = "";
            this.LastName = "";
            this.DateOfBirth = DateTime.Now;
            this.Phone = "";
            this.Address = "";

        }
        private clsPerson (int personID, string firstName, string lastName, DateTime dateOfBirth, string phone, string address)
        {
            this.PersonID = personID;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.DateOfBirth = dateOfBirth;
            this.Phone = phone;
            this.Address = address;

            Mode = enMode.Update;
        }

        private bool _AddNewPerson()
        {
            this.PersonID = clsPersonData.AddNewPerson(this.FirstName, this.LastName, this.DateOfBirth, this.Phone, this.Address);

            return (this.PersonID != -1);
        }
        private bool _UpdatePerson()
        {
            return clsPersonData.UpdatePerson(this.PersonID, this.FirstName, this.LastName, this.DateOfBirth, this.Phone, this.Address);
        }
        public static clsPerson Find(int PersonID)
        {

            string FirstName = "", LastName = "", Phone = "", Address = "";
            DateTime DateOfBirth = DateTime.Now;

            bool IsFound = clsPersonData.GetPersonInfoByID(PersonID, ref FirstName, ref LastName, ref DateOfBirth,ref Address, ref Phone);

            if (IsFound)
                //we return new object of that person with the right data
                return new clsPerson(PersonID, FirstName, LastName, DateOfBirth, Address, Phone);
            else
                return null;
        }

        public bool Save()
        {
            switch(Mode)
            {
                case enMode.AddNew:
                    if (_AddNewPerson())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;

                case enMode.Update:
                    return _UpdatePerson();
            }

            return false;
        }
        public static bool DeletePerson(int PersonID)
        {
            return clsPersonData.DeletePerson(PersonID);
        }
        public bool Delete()
        {
            return clsPersonData.DeletePerson(this.PersonID);
        }
    }
}
