using System;
using System.Data;
using System.Data.SqlClient;

namespace Mini_Project_RRS
{
    public static class AuthService
    {
        public static int LoggedInUserId { get; private set; }
        public static string LoggedInUserRole { get; private set; }
        public static string LoggedInUsername { get; private set; }

        public static bool Login(string username, string password)
        {
            LoggedInUserId = 0;
            LoggedInUserRole = null;
            LoggedInUsername = null;

            using (SqlConnection connection = Connection.GetConnection())
            {
                try
                {
                    connection.Open();
                    string query = "SELECT UserID, Role FROM Users WHERE Username = @Username AND Password = @Password AND IsDeleted = 0";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Username", username);
                        command.Parameters.AddWithValue("@Password", password);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                LoggedInUserId = reader.GetInt32(reader.GetOrdinal("UserID"));
                                LoggedInUserRole = reader.GetString(reader.GetOrdinal("Role"));
                                LoggedInUsername = username;
                                return true;
                            }
                        }
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine($"Database error: {ex.Message}");
                }
            }
            return false;
        }

        public static bool Register(string username, string email, string phone, string password)
        {
            using (SqlConnection connection = Connection.GetConnection())
            {
                try
                {
                    connection.Open();
                    string query = "INSERT INTO Users (Username, Email, PhoneNumber, Password, Role) VALUES (@Username, @Email, @Phone, @Password, 'User')";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Username", username);
                        command.Parameters.AddWithValue("@Email", email);
                        command.Parameters.AddWithValue("@Phone", phone);
                        command.Parameters.AddWithValue("@Password", password);

                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
                catch (SqlException ex)
                {
                    if (ex.Number == 2627)
                    {
                        Console.WriteLine("Registration failed: Username or Email already exists. Please try again.");
                    }
                    else
                    {
                        Console.WriteLine($"An error occurred during registration: {ex.Message}");
                    }
                    return false;
                }
            }
        }

        public static void Logout()
        {
            LoggedInUserId = 0;
            LoggedInUserRole = null;
            LoggedInUsername = null;
        }
    }
}
