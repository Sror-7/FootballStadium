using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballStadium_DataAccess
{
    public class clsConstantBookingData
    {
        public static int AddNewConstantBooking(int TeamID, string DayName, TimeSpan FromTime, TimeSpan ToTime, DateTime CreatedDate, int CreatedByUserID)
        {
            int ConstantBookingID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"INSERT INTO ConstantBookings (TeamID, DayName, FromTime, ToTime, CreatedDate, CreatedByUserID)
                             VALUES (@TeamID, @DayName, @FromTime, @ToTime, @CreatedDate, @CreatedByUserID)
                             SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@TeamID", TeamID);
            command.Parameters.AddWithValue("@DayName", DayName);
            command.Parameters.AddWithValue("@FromTime", FromTime);
            command.Parameters.AddWithValue("@ToTime", ToTime);
            command.Parameters.AddWithValue("@CreatedDate", CreatedDate);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    ConstantBookingID = insertedID;
                }
            }

            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);

            }

            finally
            {
                connection.Close();
            }

            return ConstantBookingID;
        }
        public static DataTable GetAllConstantBookings()
        {

            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"select ConstantBookingID, TeamName, DayName, FromTime, ToTime, ConstantBookings.CreatedDate from ConstantBookings
                             Inner Join Teams On Teams.TeamID = ConstantBookings.TeamID;";


            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    dt.Load(reader);
                }

                reader.Close();


            }

            catch (Exception ex)
            {
                // Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return dt;

        }
        public static bool IsTimeBooked(string DayName, TimeSpan FromTime, TimeSpan ToTime)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT FOUND = 1 FROM ConstantBookings WHERE DayName = @DayName 
                             AND 
                             ((@FromTime between FromTime and ToTime) OR (@ToTime between FromTime and ToTime) OR (@ToTime >= ToTime and @FromTime <= FromTime))";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@DayName", DayName);
            command.Parameters.AddWithValue("@FromTime", FromTime);
            command.Parameters.AddWithValue("@ToTime", ToTime);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                isFound = reader.HasRows;

                reader.Close();
            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }
        public static bool DeleteConstantBooking(int ConstantBookingID)
        {

            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Delete ConstantBookings 
                                where ConstantBookingID = @ConstantBookingID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ConstantBookingID", ConstantBookingID);

            try
            {
                connection.Open();

                rowsAffected = command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                // Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {

                connection.Close();

            }

            return (rowsAffected > 0);

        }
    }
}
