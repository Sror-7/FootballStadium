using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace FootballStadium_DataAccess
{
    public class clsPlayerData
    {
        public static int AddNewPlayer(int PersonID, int TeamID, int PlayerNumber, int CreatedByUserID, DateTime CreatedDate)
        {
            //this function will return the new person id if succeeded and -1 if not.
            int PlayerID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"INSERT INTO Players (PersonID, TeamID, PlayerNumber, CreatedByUserID, CreatedDate)
                             VALUES (@PersonID, @TeamID, @PlayerNumber, @CreatedByUserID, @CreatedDate);
                             SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@TeamID", TeamID);
            command.Parameters.AddWithValue("@PlayerNumber", PlayerNumber);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("@CreatedDate", CreatedDate);
            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    PlayerID = insertedID;
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

            return PlayerID;
        }



        public static bool UpdatePlayer(int PlayerID, int TeamID, int PlayerNumber, int CreatedByUserID)
        {

            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Update  Players  
                            set TeamID = @TeamID,
                                PlayerNumber = @PlayerNumber, 
                                CreatedByUserID = @CreatedByUserID
                                where PlayerID = @PlayerID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PlayerID", PlayerID);
            command.Parameters.AddWithValue("@TeamID", TeamID);
            command.Parameters.AddWithValue("@PlayerNumber", PlayerNumber);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
                return false;
            }

            finally
            {
                connection.Close();
            }

            return (rowsAffected > 0);
        }
        public static bool GetPlayerInfoByPlayerID(int PlayerID, ref int PersonID, ref int TeamID, ref int PlayerNumber, ref DateTime CreatedDate, ref int CreatedByUserID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM Players WHERE PlayerID = @PlayerID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PlayerID", PlayerID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    // The record was found
                    isFound = true;

                    PersonID = (int)reader["PersonID"];
                    PlayerNumber = (int)reader["PlayerNumber"];
                    TeamID = (int)reader["TeamID"];
                    CreatedDate = (DateTime)reader["CreatedDate"];
                    CreatedByUserID = (int)reader["CreatedByUserID"];
                }
                else
                {
                    // The record was not found
                    isFound = false;
                }

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
        public static bool DeletePlayer(int PlayerID)
        {

            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Delete Players 
                                where PlayerID = @PlayerID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PlayerID", PlayerID);

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
