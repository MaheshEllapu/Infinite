using System;
using System.Data.SqlClient;
using System.Globalization;

namespace Mini_Project_RRS
{
    public static class AdminMenu
    {
        public static void ShowMenu()
        {
            while (AuthService.LoggedInUserRole == "Admin")
            {
                Console.Clear();
                Console.WriteLine($"Welcome, Admin! ({AuthService.LoggedInUsername})");
                Console.WriteLine("----------------------------------");
                Console.WriteLine("1. View All Users");
                Console.WriteLine("2. Add New Train");
                Console.WriteLine("3. View All Trains");
                Console.WriteLine("4. Update Train Details");
                Console.WriteLine("5. Soft Delete Train");
                Console.WriteLine("6. View All Reservations");
                Console.WriteLine("7. View All Cancellations");
                Console.WriteLine("8. Logout");
                Console.Write("Enter your choice: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": ViewAllUsers(); break;
                    case "2": AddNewTrain(); break;
                    case "3": ViewAllTrains(); break;
                    case "4": UpdateTrainDetails(); break;
                    case "5": SoftDeleteTrain(); break;
                    case "6": ViewAllReservations(); break;
                    case "7": ViewAllCancellations(); break;
                    case "8": AuthService.Logout(); Console.WriteLine("Logged out successfully."); Console.ReadKey(); break;
                    default: Console.WriteLine("Invalid choice. Press any key to continue."); Console.ReadKey(); break;
                }
            }
        }
        private static void ViewAllUsers()
        {
            Console.Clear();
            Console.WriteLine("------------------------------------------------ All Users ------------------------------------------------");
            try
            {
                using (SqlConnection connection = Connection.GetConnection())
                {
                    connection.Open();
                    // Corrected query: The table name [User] is now in square brackets.
                    string query = "SELECT UserID, UserName, Email, PhoneNumber, Password, Role, IsDeleted FROM [Users] ORDER BY UserID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (!reader.HasRows)
                            {
                                Console.WriteLine("No users found.");
                            }
                            else
                            {
                                Console.WriteLine($"{"UserID",-8} {"UserName",-18} {"Email",-26} {"PhoneNumber",-16} {"Password",-15} {"Role",-8} {"Is Deleted",-5}");
                                Console.WriteLine("-----------------------------------------------------------------------------------------------------------");
                                while (reader.Read())
                                {
                                    Console.WriteLine(
                                        $"{reader["UserID"],-8} " +
                                        $"{reader["UserName"],-18} " +
                                        $"{reader["Email"],-26} " +
                                        $"{reader["PhoneNumber"],-16} " +
                                        $"{reader["Password"],-15} " +
                                        $"{reader["Role"],-8} " +
                                        $"{((bool)reader["IsDeleted"] ? "Yes" : "No"),-6}");
                                }
                                Console.WriteLine("-----------------------------------------------------------------------------------------------------------");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            Console.ReadKey();
        }


        private static void AddNewTrain()
        {
            Console.Clear();
            Console.WriteLine("------------- Add New Train -------------");
            try
            {
                Console.Write("Enter Train Number: ");
                int trainNumber = int.Parse(Console.ReadLine());
                Console.Write("Enter Train Name: ");
                string trainName = Console.ReadLine();
                Console.Write("Enter Source: ");
                string source = Console.ReadLine();
                Console.Write("Enter Destination: ");
                string destination = Console.ReadLine();
                Console.Write("Enter Duration (e.g., '6h 30m'): ");
                string duration = Console.ReadLine();
                Console.Write("Enter Departure Time (HH:mm): ");
                TimeSpan departure = TimeSpan.Parse(Console.ReadLine());

                Console.Write("Seats_1AC: "); int seats1AC = int.Parse(Console.ReadLine());
                Console.Write("Cost_1AC: "); decimal cost1AC = decimal.Parse(Console.ReadLine());
                Console.Write("Seats_2AC: "); int seats2AC = int.Parse(Console.ReadLine());
                Console.Write("Cost_2AC: "); decimal cost2AC = decimal.Parse(Console.ReadLine());
                Console.Write("Seats_3AC: "); int seats3AC = int.Parse(Console.ReadLine());
                Console.Write("Cost_3AC: "); decimal cost3AC = decimal.Parse(Console.ReadLine());
                Console.Write("Seats_3E: "); int seats3E = int.Parse(Console.ReadLine());
                Console.Write("Cost_3E: "); decimal cost3E = decimal.Parse(Console.ReadLine());
                Console.Write("Seats_Sleeper: "); int seatsSleeper = int.Parse(Console.ReadLine());
                Console.Write("Cost_Sleeper: "); decimal costSleeper = decimal.Parse(Console.ReadLine());

                string query = @"INSERT INTO TrainDetails (TrainNumber, TrainName, Source, Destination, Duration, Departure, Seats_1AC, Cost_1AC, Seats_2AC, Cost_2AC, Seats_3AC, Cost_3AC, Seats_3E, Cost_3E, Seats_Sleeper, Cost_Sleeper)
                             VALUES (@TrainNumber, @TrainName, @Source, @Destination, @Duration, @Departure, @Seats_1AC, @Cost_1AC, @Seats_2AC, @Cost_2AC, @Seats_3AC, @Cost_3AC, @Seats_3E, @Cost_3E, @Seats_Sleeper, @Cost_Sleeper)";

                using (SqlConnection connection = Connection.GetConnection())
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@TrainNumber", trainNumber);
                        command.Parameters.AddWithValue("@TrainName", trainName);
                        command.Parameters.AddWithValue("@Source", source);
                        command.Parameters.AddWithValue("@Destination", destination);
                        command.Parameters.AddWithValue("@Duration", duration);
                        command.Parameters.AddWithValue("@Departure", departure);
                        command.Parameters.AddWithValue("@Seats_1AC", seats1AC);
                        command.Parameters.AddWithValue("@Cost_1AC", cost1AC);
                        command.Parameters.AddWithValue("@Seats_2AC", seats2AC);
                        command.Parameters.AddWithValue("@Cost_2AC", cost2AC);
                        command.Parameters.AddWithValue("@Seats_3AC", seats3AC);
                        command.Parameters.AddWithValue("@Cost_3AC", cost3AC);
                        command.Parameters.AddWithValue("@Seats_3E", seats3E);
                        command.Parameters.AddWithValue("@Cost_3E", cost3E);
                        command.Parameters.AddWithValue("@Seats_Sleeper", seatsSleeper);
                        command.Parameters.AddWithValue("@Cost_Sleeper", costSleeper);

                        connection.Open();
                        int rows = command.ExecuteNonQuery();
                        Console.WriteLine(rows > 0 ? "Train added successfully!" : "Failed to add train.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            Console.ReadKey();
        }

        public static void ViewAllTrains()
        {
            Console.Clear();
            Console.WriteLine("-------------------------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("                                                               All Trains");
            Console.WriteLine("-------------------------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine($"{"Train No.",-10} {"Name",-20} {"Source",-15} {"Destination",-15} {"Duration",-8} {"Departure",-10} {"1AC Seats",-10} {"1AC Cost",-8} {"2AC Seats",-10} {"2AC Cost",-8} {"3AC Seats",-10} {"3AC Cost",-8} {"3E Seats",-10} {"3E Cost",-8} {"SL Seats",-10} {"SL Cost",-8}");
            Console.WriteLine("-------------------------------------------------------------------------------------------------------------------------------------");

            string query = "SELECT * FROM TrainDetails WHERE IsDeleted = 0";

            using (SqlConnection connection = Connection.GetConnection())
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Console.WriteLine($"{reader["TrainNumber"],-10} {reader["TrainName"],-20} {reader["Source"],-15} {reader["Destination"],-15} {reader["Duration"],-8} {((TimeSpan)reader["Departure"]).ToString("hh\\:mm"),-10} {reader["Seats_1AC"],-10} {reader["Cost_1AC"],-8} {reader["Seats_2AC"],-10} {reader["Cost_2AC"],-8} {reader["Seats_3AC"],-10} {reader["Cost_3AC"],-8} {reader["Seats_3E"],-10} {reader["Cost_3E"],-8} {reader["Seats_Sleeper"],-10} {reader["Cost_Sleeper"],-8}");
                                Console.WriteLine("");
                            }
                        }
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                }
            }
            Console.WriteLine("-------------------------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("\nPress any key to return to the menu.");
            Console.ReadKey();
        }

        private static void UpdateTrainDetails()
        {
            Console.Clear();
            Console.WriteLine("------------- Update Train Details -------------");
            try
            {
                Console.Write("Enter the Train Number to update: ");
                if (!int.TryParse(Console.ReadLine(), out int trainNumber))
                {
                    Console.WriteLine("Invalid train number.");
                    Console.ReadKey();
                    return;
                }

                Console.Write("Enter the column name to update (e.g., TrainName, Source, Cost_Sleeper): ");
                string columnName = Console.ReadLine();

                if (columnName.Equals("Cost_Sleeper", StringComparison.OrdinalIgnoreCase))
                {
                    Console.Write("Enter the new value for Sleeper Cost: ");
                }
                else
                {
                    Console.Write($"Enter the new value for {columnName}: ");
                }

                string newValue = Console.ReadLine();

                using (SqlConnection connection = Connection.GetConnection())
                {
                    connection.Open();
                    string query = $"UPDATE TrainDetails SET {columnName} = @NewValue WHERE TrainNumber = @TrainNumber";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@NewValue", newValue);
                        command.Parameters.AddWithValue("@TrainNumber", trainNumber);

                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            Console.WriteLine("Train details updated successfully.");
                        }
                        else
                        {
                            Console.WriteLine("Train not found or no changes were made.");
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                if (ex.Number == 207)
                {
                    Console.WriteLine($"An error occurred: Invalid column name. Please check the spelling (e.g., 'Cost_Sleeper' instead of 'SL_Cost').");
                }
                else
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            Console.ReadKey();
        }
        public static void SoftDeleteTrain()
        {
            Console.Clear();
            Console.WriteLine("--- Soft Delete Train ---");
            try
            {
                Console.Write("Enter the Train Number to delete: ");
                if (!int.TryParse(Console.ReadLine(), out int trainNumber))
                {
                    Console.WriteLine("Invalid train number.");
                    Console.ReadKey();
                    return;
                }

                Console.Write($"Are you sure you want to soft-delete train {trainNumber}? (Y/N): ");
                if (Console.ReadLine().ToUpper() != "Y")
                {
                    Console.WriteLine("Deletion cancelled.");
                    Console.ReadKey();
                    return;
                }

                string query = "UPDATE TrainDetails SET IsDeleted = 1 WHERE TrainNumber = @TrainNumber";

                using (SqlConnection connection = Connection.GetConnection())
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@TrainNumber", trainNumber);

                        connection.Open();
                        int rows = command.ExecuteNonQuery();
                        Console.WriteLine(rows > 0 ? "Train soft-deleted successfully!" : "Train not found or already deleted.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            Console.ReadKey();
        }

        public static void ViewAllReservations()
        {
            Console.Clear();
            Console.WriteLine("----------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("                                              All Reservations");
            Console.WriteLine("----------------------------------------------------------------------------------------------------------------");
            Console.WriteLine($"{"PNR",-15} {"Train Name",-20} {"Customer Name",-20} {"Date of Travel",-15} {"Class",-10} {"Cost",-10} {"Cancelled",-10}");
            Console.WriteLine("----------------------------------------------------------------------------------------------------------------");

            string query = "SELECT R.PNR, T.TrainName, R.CustomerName, R.DateOfTravel, R.ClassType, R.TotalCost, R.IsCancelled FROM Reservation R JOIN TrainDetails T ON R.TrainNumber = T.TrainNumber WHERE R.IsDeleted = 0";

            using (SqlConnection connection = Connection.GetConnection())
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Console.WriteLine($"{reader["PNR"],-15} {reader["TrainName"],-20} {reader["CustomerName"],-20} {((DateTime)reader["DateOfTravel"]).ToString("yyyy-MM-dd"),-15} {reader["ClassType"],-10} {reader["TotalCost"],-10:C} {((bool)reader["IsCancelled"] ? "Yes" : "No"),-10}");
                            }
                        }
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                }
            }
            Console.WriteLine("----------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("\nPress any key to return to the menu.");
            Console.ReadKey();
        }

        public static void ViewAllCancellations()
        {
            Console.Clear();
            Console.WriteLine("------------------------------------------------------------------------------------------------");
            Console.WriteLine("                                       All Cancellations");
            Console.WriteLine("------------------------------------------------------------------------------------------------");
            Console.WriteLine($"{"Cancellation ID",-15} {"PNR",-15} {"Date of Cancellation",-25} {"Refund Amount",-15}");
            Console.WriteLine("------------------------------------------------------------------------------------------------");

            string query = "SELECT C.CancellationID, R.PNR, C.DateOfCancellation, C.RefundAmount FROM Cancellation C JOIN Reservation R ON C.ReservationID = R.ReservationID WHERE C.IsDeleted = 0";

            using (SqlConnection connection = Connection.GetConnection())
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Console.WriteLine($"{reader["CancellationID"],-15} {reader["PNR"],-15} {((DateTime)reader["DateOfCancellation"]).ToString("yyyy-MM-dd"),-25} {reader["RefundAmount"],-15:C}");
                            }
                        }
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                }
            }
            Console.WriteLine("------------------------------------------------------------------------------------------------");
            Console.WriteLine("\nPress any key to return to the menu.");
            Console.ReadKey();
        }
    }
}

