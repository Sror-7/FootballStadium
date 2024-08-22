using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballStadium_DataAccess
{
    public class clsChampionshipData
    {
        public static int AddNewChampionship(string Name, DateTime StartDate, int EnrolledTeamsCount, bool IsEndet,
            DateTime EndDate, DateTime CreatedDate, string Note, int CreatedByUserID)
        {
            int ChampionshipID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"INSERT INTO Championships (Name, StartDate, EnrolledTeamsCount, IsEndet, EndDate, CreatedDate, Note, CreatedByUserID)
                             VALUES (@Name, @StartDate, @EnrolledTeamsCount, @IsEndet, @EndDate, @CreatedDate, @Note, @CreatedByUserID)
                             SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@Name", Name);
            command.Parameters.AddWithValue("@StartDate", StartDate);
            command.Parameters.AddWithValue("@EnrolledTeamsCount", EnrolledTeamsCount);
            command.Parameters.AddWithValue("@IsEndet", IsEndet);
            command.Parameters.AddWithValue("@EndDate", EndDate);
            command.Parameters.AddWithValue("@CreatedDate", CreatedDate);
            command.Parameters.AddWithValue("@Note", Note);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    ChampionshipID = insertedID;
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

            return ChampionshipID;
        }
        public static bool UpdateChampionship(int ChampionshipID, string Name, DateTime StartDate, int EnrolledTeamsCount, bool IsEndet,
            DateTime EndDate, DateTime CreatedDate, string Note, int CreatedByUserID)
        {

            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Update  Championships  
                            set Name = @Name,
                                StartDate = @StartDate,
                                EnrolledTeamsCount = @EnrolledTeamsCount,
                                IsEndet = @IsEndet,
                                EndDate = @EndDate,
                                CreatedDate = @CreatedDate,
                                Note = @Note,
                                CreatedByUserID = @CreatedByUserID
                                where ChampionshipID = @ChampionshipID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ChampionshipID", ChampionshipID);
            command.Parameters.AddWithValue("@Name", Name);
            command.Parameters.AddWithValue("@StartDate", StartDate);            
            command.Parameters.AddWithValue("@EnrolledTeamsCount", EnrolledTeamsCount);
            command.Parameters.AddWithValue("@IsEndet", IsEndet);
            if (EndDate != null)
                command.Parameters.AddWithValue("@EndDate", EndDate);
            else
                command.Parameters.AddWithValue("@EndDate", DBNull.Value);

            command.Parameters.AddWithValue("@CreatedDate", CreatedDate);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            if(!string.IsNullOrEmpty(Note))
                command.Parameters.AddWithValue("@Note", Note);
            else
                command.Parameters.AddWithValue("@Note", DBNull.Value);
           
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
        public static bool GetChampionshipInfoByID(int ChampionshipID, ref string Name, ref DateTime StartDate, ref int EnrolledTeamsCount,
           ref bool IsEndet, ref DateTime EndDate, ref DateTime CreatedDate, ref string Note, ref int CreatedByUserID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM Championships WHERE ChampionshipID = @ChampionshipID";

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

                    Name = (string)reader["Name"];
                    StartDate = (DateTime)reader["StartDate"];
                    EnrolledTeamsCount = (int)reader["EnrolledTeamsCount"];
                    IsEndet = (bool)reader["IsEndet"];

                    if(reader["EndDate"] != DBNull.Value)
                        EndDate = (DateTime)reader["EndDate"];

                    CreatedDate = (DateTime)reader["CreatedDate"];
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
       
        public static DataTable GetCurrentChampionships()
        {

            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"select ChampionshipID, Name, StartDate, EnrolledTeamsCount as 'E.Teams Count', IsEndet, CreatedDate, Note
                             from championships
                             where IsEndet = 0";


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
        public static DataTable GetEndetChampionships()
        {

            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"select ChampionshipID, Name, StartDate, EnrolledTeamsCount as 'E.Teams Count', IsEndet, EndDate, CreatedDate, Note
                             from championships
                             where IsEndet = 1";


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
        public static DataTable GetAllEnrolledTeamsInChampionship(int ChampionshipID)
        {

            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"select  Championships.Name as 'ChampionshipName', ChampionshipEnrolledTeams.TeamID, Teams.TeamName, Teams.PlayersCount, EnrolledTeamDate
                             from ChampionshipEnrolledTeams
                             Inner Join Teams On Teams.TeamID = ChampionshipEnrolledTeams.TeamID
                             Inner join Championships On Championships.ChampionshipID = ChampionshipEnrolledTeams.ChampionshipID
                             where Championships.ChampionshipID = @ChampionshipID";


            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ChampionshipID", ChampionshipID);
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
        public static bool DeleteChampionship(int ChampionshipID)
        {

            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Delete Championships 
                                where ChampionshipID = @ChampionshipID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ChampionshipID", ChampionshipID);

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
        public static bool DeleteChampionshipAndEnrolledTeams(int ChampionshipID)
        {

            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Delete ChampionshipEnrolledTeams 
                                where ChampionshipID = @ChampionshipID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ChampionshipID", ChampionshipID);

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
        public static int AddTeamToChampionship(int TeamID, int ChampionshipID, DateTime EnrolledTeamDate, int CreatedByUserID)
        {
            int Ch_E_T_ID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"INSERT INTO ChampionshipEnrolledTeams (TeamID, ChampionshipID, EnrolledTeamDate, CreatedByUserID)
                             VALUES (@TeamID, @ChampionshipID, @EnrolledTeamDate, @CreatedByUserID)
                             SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@TeamID", TeamID);
            command.Parameters.AddWithValue("@ChampionshipID", ChampionshipID);
            command.Parameters.AddWithValue("@EnrolledTeamDate", EnrolledTeamDate);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    Ch_E_T_ID = insertedID;
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

            return Ch_E_T_ID;
        }
        public static bool IncrementTeamsCount(int ChampionshipID)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Update  Championships  set EnrolledTeamsCount += 1
                                where ChampionshipID = @ChampionshipID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ChampionshipID", ChampionshipID);


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
        public static bool DecrementTeamsCount(int ChampionshipID)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Update  Championships  set EnrolledTeamsCount -= 1
                                where ChampionshipID = @ChampionshipID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ChampionshipID", ChampionshipID);


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
        public static bool IsTeamExistInChampionship(int TeamID, int ChampionshipID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT Found=1 FROM ChampionshipEnrolledTeams WHERE TeamID = @TeamID and ChampionshipID = @ChampionshipID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ChampionshipID", ChampionshipID);
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
        public static bool RemoveTeamFromChampionship(int TeamID, int ChampionshipID)
        {

            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"DELETE ChampionshipEnrolledTeams WHERE TeamID = @TeamID and ChampionshipID = @ChampionshipID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@TeamID", TeamID);
            command.Parameters.AddWithValue("@ChampionshipID", ChampionshipID);

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
