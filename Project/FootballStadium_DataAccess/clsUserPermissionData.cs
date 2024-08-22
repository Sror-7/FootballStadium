using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FootballStadium_DataAccess
{
    public class clsUserPermissionData
    {
        public static int AddNewPermission(int UserID, string FormName, bool FullAccess, bool Add, bool Edit, bool Show, bool Delete)
        {
            
            int PermissionID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"INSERT INTO UserPermissions (UserID, FormName, FullAccess, [Add], Edit, Show, [Delete])
                             VALUES (@UserID, @FormName, @FullAccess, @Add, @Edit, @Show, @Delete);
                             SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@UserID", UserID);
            command.Parameters.AddWithValue("@FormName", FormName);
            command.Parameters.AddWithValue("@FullAccess", FullAccess);
            command.Parameters.AddWithValue("@Add", Add);
            command.Parameters.AddWithValue("@Edit", Edit);
            command.Parameters.AddWithValue("@Show", Show);
            command.Parameters.AddWithValue("@Delete", Delete);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    PermissionID = insertedID;
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

            return PermissionID;
        }



        public static bool UpdatePermission(int UserID, string FormName, bool FullAccess, bool Add, bool Edit, bool Show, bool Delete)
        {

            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Update  UserPermissions  
                            set FullAccess = @FullAccess,
                                [Add] = @Add,
                                Edit = @Edit,
                                Show = @Show,
                                [Delete] = @Delete
                                where UserID = @UserID AND FormName = @FormName";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@UserID", UserID);
            command.Parameters.AddWithValue("@FormName", FormName);
            command.Parameters.AddWithValue("@FullAccess", FullAccess);
            command.Parameters.AddWithValue("@Add", Add);
            command.Parameters.AddWithValue("@Edit", Edit);
            command.Parameters.AddWithValue("@Show", Show);
            command.Parameters.AddWithValue("@Delete", Delete);

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
        public static bool GetPermissionsByID(ref int PermissionID, int UserID, string FormName, ref bool FullAccess, ref bool Add,
            ref bool Edit, ref bool Show, ref bool Delete)
        {
            bool isFound = false;
            
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM UserPermissions WHERE UserID = @UserID And FormName = @FormName";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@UserID", UserID);
            command.Parameters.AddWithValue("@FormName", FormName);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    // The record was found
                    isFound = true;

                    PermissionID = (int)reader["PermissionID"];

                    FullAccess = (bool)reader["FullAccess"];
                    Add = (bool)reader["Add"];
                    Edit = (bool)reader["Edit"];
                    Show = (bool)reader["Show"];
                    Delete = (bool)reader["Delete"];

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
    }
}
