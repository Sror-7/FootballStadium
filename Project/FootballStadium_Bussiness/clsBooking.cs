using FootballStadium_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballStadium_Business
{
    public class clsBooking
    {
        enum enMode { AddNew = 1, Update = 2 }
        enMode Mode = enMode.AddNew;

        public int BookingID { get; set; }
        public int TeamID { get; set; }
        public DateTime BookingDate { get; set; }
        public TimeSpan FromTime { get; set; }
        public TimeSpan ToTime { get; set; }
        public string DayName { get; set; }
        public int CreatedByUserID { get; set; }

        public clsBooking()
        {
            this.BookingID = -1;
            this.TeamID = -1;
            this.BookingDate = DateTime.Now;
            this.FromTime = TimeSpan.Zero;
            this.ToTime = TimeSpan.Zero;
            this.DayName = "";
            this.CreatedByUserID = -1;
        }
        private clsBooking(int BookingID, int TeamID, DateTime BookingDate, string DayName, bool IsConstant, int CreatedByUserID)
        {
            this.BookingID = BookingID;
            this.TeamID = TeamID;
            this.BookingDate = BookingDate;
            this.DayName = DayName;
            this.CreatedByUserID = CreatedByUserID;
        }

        private bool _AddNewBooking()
        {
           this.BookingID = clsBookingData.AddNewBooking(this.TeamID, this.BookingDate, this.FromTime, this.ToTime, this.DayName, this.CreatedByUserID);
            
            return (this.BookingID != -1);
        }
        private bool _UpdateBooking()
        {
            return false;
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewBooking())
                    {
                        //Mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;

                case enMode.Update:
                    return _UpdateBooking();
            }

            return false;
        }
        public static DataTable GetAllBookings()
        {
            return clsBookingData.GetAllBookings();
        }
        public static DataTable GetBookings()
        {
            return clsBookingData.GetNewBookings();
        }
        public static DataTable GetOldBookings()
        {
            return clsBookingData.GetOldBookings();
        }
        public static bool IsTimeBooked(DateTime BookingDate, TimeSpan FromTime, TimeSpan ToTime)
        {
            return clsBookingData.IsTimeBooked(BookingDate, FromTime, ToTime);
        }
        public static bool IsTimeBooked(string DayName, TimeSpan FromTime, TimeSpan ToTime)
        {
            return clsBookingData.IsTimeBooked(DayName, FromTime, ToTime);
        }
        public static bool DeleteBooking(int BookingID)
        {
            return clsBookingData.DeleteBooking(BookingID);
        }
    }
}
