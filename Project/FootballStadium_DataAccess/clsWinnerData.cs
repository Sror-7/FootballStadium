using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballStadium_DataAccess
{
    public class clsWinnerData
    {
        public static bool GetWinnerInfoByChampionshipID(int ChampionshipID, ref int WinnerID, ref int TeamID, ref string Note, ref int CreatedByUserID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM Winners WHERE ChampionshipID = @ChampionshipID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ChampionshipID", ChampionshipID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    // The record was found
                    isFound = true;

                    WinnerID = (int)reader["WinnerID"];
                    TeamID = (int)reader["TeamID"];

                    if(reader["Note"] != DBNull.Value)
                        Note = (string)reader["Note"];
                    else
                        Note = "";

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
        public static int AddNewWinner(int TeamID, int ChampionshipID, string Note, int CreatedByUserID)
        {
            //this function will return the new person id if succeeded and -1 if not.
            int WinnerID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"INSERT INTO Winners (TeamID, ChampionshipID, Note, CreatedByUserID)
                             VALUES (@TeamID, @ChampionshipID, @Note, @CreatedByUserID);
                             SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@TeamID", TeamID);
            command.Parameters.AddWithValue("@ChampionshipID", ChampionshipID);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            if (!string.IsNullOrEmpty(Note))
                command.Parameters.AddWithValue("@Note", Note);
            else
                command.Parameters.AddWithValue("@Note", DBNull.Value);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    WinnerID = insertedID;
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

            return WinnerID;
        }
    }
}
