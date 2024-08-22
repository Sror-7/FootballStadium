using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballStadium_DataAccess
{
    public class clsTeamData
    {
        public static bool GetTeamInfoByTeamID(int TeamID, ref string TeamName, ref Nullable<int> CoachID, ref int PlayersCount, ref int ChampionshipsCount, ref DateTime CreatedDate, ref string TeamDescription, ref int CreatedByUserID)
        {
            bool isFound = false;
            try
            {//this way to connection with DB by Using... in this way you can open the connection (connection.Open()) without that you
                //close the connection (connection.Close()) because when you use Using in Connection the language by defalut close the connection
                //even you forget or you don't close the connection 
                
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {

                    connection.Open();

                    string query = "SELECT * FROM Teams WHERE TeamID = @TeamID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {

                        command.Parameters.AddWithValue("@TeamID", TeamID);


                        using (SqlDataReader reader = command.ExecuteReader())
                        {

                            if (reader.Read())
                            {

                                // The record was found
                                isFound = true;

                                TeamName = (string)reader["TeamName"];

                                if (reader["CoachID"] != DBNull.Value)
                                    CoachID = (int)reader["CoachID"];
                                else
                                    CoachID = null;

                                PlayersCount = (int)reader["PlayersCount"];
                                ChampionshipsCount = (int)reader["ChampionshipsCount"];
                                CreatedDate = (DateTime)reader["CreatedDate"];
                                CreatedByUserID = (int)reader["CreatedByUserID"];

                                if (reader["TeamDescription"] != DBNull.Value)
                                    TeamDescription = (string)reader["TeamDescription"];
                                else
                                    TeamDescription = "";



                            }
                            else
                            {
                                // The record was not found
                                isFound = false;
                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
                isFound = false;
            }
            finally
            {
                
            }

            return isFound;
        }
        public static int AddNewTeam(string TeamName, Nullable<int> CoachID, int PlayersCount, int ChampionshipsCount, DateTime CreatedDate, int CreatedByUserID, string TeamDescription)
        {
            int TeamID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"INSERT INTO Teams (TeamName, CoachID, PlayersCount, ChampionshipsCount,CreatedDate,CreatedByUserID, TeamDescription)
                             VALUES (@TeamName, @CoachID, @PlayersCount, @ChampionshipsCount,@CreatedDate,@CreatedByUserID, @TeamDescription);
                             SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@TeamName", TeamName);
            command.Parameters.AddWithValue("@PlayersCount", PlayersCount);
            command.Parameters.AddWithValue("@ChampionshipsCount", ChampionshipsCount);
            command.Parameters.AddWithValue("@CreatedDate", CreatedDate);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            if (!string.IsNullOrEmpty(TeamDescription))
                command.Parameters.AddWithValue("@TeamDescription", TeamDescription);
            else
                command.Parameters.AddWithValue("@TeamDescription", DBNull.Value);

            if (CoachID.HasValue)
                command.Parameters.AddWithValue("@CoachID", CoachID);
            else
                command.Parameters.AddWithValue("@CoachID", DBNull.Value);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    TeamID = insertedID;
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

            return TeamID;
        }


        public static bool UpdateTeam(int TeamID, string TeamName, Nullable<int> CoachID, int PlayersCount, int ChampionshipsCount,
             DateTime CreatedDate, int CreatedByUserID, string TeamDescription)
        {

            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Update  Teams  
                            set TeamName = @TeamName,
                                CoachID = @CoachID,
                                PlayersCount = @PlayersCount,
                                ChampionshipsCount = @ChampionshipsCount,
                                CreatedDate = @CreatedDate,
                                CreatedByUserID = @CreatedByUserID,
                                TeamDescription = @TeamDescription
                                where TeamID = @TeamID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@TeamName", TeamName);
            if (CoachID.HasValue)
                command.Parameters.AddWithValue("@CoachID", CoachID);
            else
                command.Parameters.AddWithValue("@CoachID", DBNull.Value);
            command.Parameters.AddWithValue("@PlayersCount", PlayersCount);
            command.Parameters.AddWithValue("@ChampionshipsCount", ChampionshipsCount);
            command.Parameters.AddWithValue("@CreatedDate", CreatedDate);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("@TeamDescription", TeamDescription);
            command.Parameters.AddWithValue("@TeamID", TeamID);


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

        public static bool AddPlayer(int TeamID)
        { 
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Update  Teams  set PlayersCount += @PlayersCount
                                where TeamID = @TeamID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PlayersCount", 1);
            command.Parameters.AddWithValue("@TeamID", TeamID);


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
        public static bool IsTeamExist(string TeamName)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT Found=1 FROM Teams WHERE TeamName = @TeamName";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@TeamName", TeamName);

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
        public static bool IncrementChampionshipCount(int TeamID)
        {
            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "UPDATE Teams SET ChampionshipsCount += 1 WHERE TeamID = @TeamID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@TeamID", TeamID);

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
        public static bool DoesTeamHaveCoach(int TeamID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT Found=1 FROM Teams where TeamID = @TeamID AND CoachID IS NOT NULL";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@TeamID", TeamID);

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
        public static DataTable GetAllTeams()
        {

            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"select Teams.TeamID, TeamName, People.FirstName + ' ' + People.LastName as 'Coach Name', PlayersCount, Teams.ChampionshipsCount, Teams.CreatedDate, TeamDescription
                             from Teams
                             left Join Coachs On Coachs.CoachID = Teams.CoachID
                             left Join People On People.PersonID = Coachs.PersonID";


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
        public static DataTable GetTeamsHasNoCoachs()
        {

            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"select * from Teams where CoachID is null";


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
        public static DataTable GetPlayersInfo(int TeamID)
        {

            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"select PlayerID, TeamName, FirstName + ' ' + LastName as 'PlayerName', PlayerNumber, DateOfBirth, Phone, Address, Players.CreatedDate
                             from Players
                             Inner Join People On People.PersonID = Players.PersonID
                             Inner Join teams On Teams.TeamID = Players.TeamID
                             where Players.TeamID = @TeamID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@TeamID", TeamID);

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

        public static bool DeleteTeam(int TeamID)
        {

            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Delete Teams 
                                where TeamID = @TeamID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@TeamID", TeamID);

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
        public static int GetTeamIDByTeamName(string TeamName)
        {
            int TeamID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT TeamID from Teams where TeamName = @TeamName";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@TeamName", TeamName);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    TeamID = insertedID;
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

            return TeamID;
        }
    }
}
