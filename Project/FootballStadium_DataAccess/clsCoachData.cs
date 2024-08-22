using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballStadium_DataAccess
{
    public class clsCoachData
    {
        public static int AddNewCoach(int PersonID, int YearsOfExperience, int ChampionshipsCount, DateTime CreatedDate, int CreatedByUserID)
        {
            //this function will return the new person id if succeeded and -1 if not.
            int CoachID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"INSERT INTO Coachs (PersonID, YearsOfExperience, ChampionshipsCount, CreatedDate, CreatedByUserID)
                             VALUES (@PersonID, @YearsOfExperience, @ChampionshipsCount, @CreatedDate, @CreatedByUserID);
                             SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@YearsOfExperience", YearsOfExperience);
            command.Parameters.AddWithValue("@ChampionshipsCount", ChampionshipsCount);
            command.Parameters.AddWithValue("@CreatedDate", CreatedDate);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    CoachID = insertedID;
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

            return CoachID;
        }



        public static bool UpdateCoach(int CoachID, int YearsOfExperience, int ChampionshipsCount, int CreatedByUserID)
        {

            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Update  Coachs  
                            set
                                YearsOfExperience = @YearsOfExperience, 
                                ChampionshipsCount = @ChampionshipsCount,
                                CreatedByUserID = @CreatedByUserID
                                where CoachID = @CoachID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@CoachID", CoachID);
            command.Parameters.AddWithValue("@YearsOfExperience", YearsOfExperience);
            command.Parameters.AddWithValue("@ChampionshipsCount", ChampionshipsCount);
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
        public static bool IncrementChampionshipCount(int CoachID)
        {
            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "UPDATE Coachs SET ChampionshipsCount += 1 WHERE CoachID = @CoachID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@CoachID", CoachID);

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
        public static DataTable GetAllCoachs()
        {

            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"select Coachs.CoachID, People.FirstName + ' ' + LastName as 'Name', People.DateOfBirth,
                             YearsOfExperience, Coachs.ChampionshipsCount, Teams.TeamName
                             
                             from coachs
                             left Join Teams On Teams.CoachID = Coachs.CoachID
                             Inner Join People ON People.PersonID = Coachs.PersonID";


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
        public static bool GetCoachInfoByCoachID(int CoachID, ref int PersonID, ref int TeamID, ref int YearsOfExperience, ref int ChampionshipsCount, ref DateTime CreatedDate, ref int CreatedByUserID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT Coachs.CoachID, PersonID, YearsOfExperience, Coachs.ChampionshipsCount, Coachs.CreatedDate, Coachs.CreatedByUserID, TeamID
                             FROM Coachs
                             left Join teams ON teams.CoachID = Coachs.CoachID
                             where coachs.CoachID = @CoachID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@CoachID", CoachID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    // The record was found
                    isFound = true;

                    PersonID = (int)reader["PersonID"];
                    if (reader["TeamID"] != DBNull.Value)
                        TeamID = (int)reader["TeamID"];
                    else
                        TeamID = -1;

                    YearsOfExperience = (int)reader["YearsOfExperience"];
                    ChampionshipsCount = (int)reader["ChampionshipsCount"];
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
        public static bool DeleteCoach(int CoachID)
        {

            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Delete Coachs 
                                where CoachID = @CoachID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@CoachID", CoachID);

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
