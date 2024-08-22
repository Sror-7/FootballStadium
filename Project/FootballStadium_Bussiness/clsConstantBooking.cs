using FootballStadium_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballStadium_Business
{
    public class clsConstantBooking
    {
        enum enMode { AddNew = 1, Update = 2 }
        enMode Mode = enMode.AddNew;
        public int ConstantBookingID { get; set; }
        public int TeamID { get; set; }
        public string DayName { get; set; }
        public TimeSpan FromTime { get; set; }
        public TimeSpan ToTime { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedByUserID { get; set; }

        public clsConstantBooking()
        {
            this.ConstantBookingID = -1;
            this.TeamID = -1;
            this.DayName = "";
            this.FromTime = TimeSpan.Zero;
            this.ToTime = TimeSpan.Zero;
            this.CreatedDate = DateTime.Now;
            this.CreatedByUserID = -1;

            Mode = enMode.AddNew;
        }
        private clsConstantBooking(int ConstantBookingID, int TeamID, string DayName, TimeSpan FromTime, TimeSpan ToTime, DateTime CreatedDate, int CreatedByUserID)
        {
            this.ConstantBookingID = ConstantBookingID;
            this.TeamID = TeamID;
            this.DayName = DayName;
            this.FromTime = FromTime;
            this.ToTime = ToTime;
            this.CreatedDate = CreatedDate;
            this.CreatedByUserID = CreatedByUserID;

            Mode = enMode.Update;
        }

        private bool _AddNewConstantBooking()
        {
            this.ConstantBookingID = clsConstantBookingData.AddNewConstantBooking(this.TeamID, this.DayName, this.FromTime, this.ToTime, this.CreatedDate, this.CreatedByUserID);
            
            return (this.ConstantBookingID != -1);
        }
        private bool _UpdateConstantBooking()
        {
            return false;
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewConstantBooking())
                    {
                        //Mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;

                case enMode.Update:
                    return _UpdateConstantBooking();
            }

            return false;
        }
        public static DataTable GetConstantBookings()
        {
            return clsConstantBookingData.GetAllConstantBookings();
        }
        public static bool IsTimeBooked(string DayName, TimeSpan FromTime, TimeSpan ToTime)
        {
            return clsConstantBookingData.IsTimeBooked(DayName, FromTime, ToTime);
        }
        public static bool Delete(int ConstantBookingID)
        {
            return clsConstantBookingData.DeleteConstantBooking(ConstantBookingID);
        }
    }
}
