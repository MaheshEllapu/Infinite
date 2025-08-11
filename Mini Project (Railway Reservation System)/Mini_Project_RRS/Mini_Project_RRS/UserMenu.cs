using System;
using System.Data.SqlClient;

namespace Mini_Project_RRS
{
    public static class UserMenu
    {
        public static void ShowMenu()
        {
            while (AuthService.LoggedInUserRole == "User")
            {
                Console.Clear();
                Console.WriteLine($"Welcome, {AuthService.LoggedInUsername}!");
                Console.WriteLine("----------------------------------");
                Console.WriteLine("1. Reserve a Ticket");
                Console.WriteLine("2. View My Tickets");
                Console.WriteLine("3. Cancel a Ticket");
                Console.WriteLine("4. Logout");
                Console.Write("Enter your choice: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": ReserveTicket(); break;
                    case "2": ViewMyTickets(); break;
                    case "3": CancelTicket(); break;
                    case "4": AuthService.Logout(); Console.WriteLine("Logged out successfully."); Console.ReadKey(); break;
                    default: Console.WriteLine("Invalid choice. Press any key to continue."); Console.ReadKey(); break;
                }
            }
        }

        private static void ReserveTicket()
        {
            Console.Clear();
            Console.WriteLine("--- Reserve a Ticket ---");
            try
            {
                Console.Write("Enter Source: ");
                string source = Console.ReadLine();
                Console.Write("Enter Destination: ");
                string destination = Console.ReadLine();
                Console.Write("Enter Date of Travel (YYYY-MM-DD): ");
                DateTime dateOfTravel = DateTime.Parse(Console.ReadLine());

                string query = "SELECT * FROM TrainDetails WHERE Source = @Source AND Destination = @Destination AND IsDeleted = 0";

                using (SqlConnection connection = Connection.GetConnection())
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Source", source);
                        command.Parameters.AddWithValue("@Destination", destination);

                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (!reader.HasRows)
                            {
                                Console.WriteLine("No trains found for this route.");
                                Console.ReadKey();
                                return;
                            }
                            Console.WriteLine("\nAvailable Trains:");
                            Console.WriteLine($"{"Number",-10} {"Name",-20} {"Duration",-10} {"Departure",-10}");
                            while (reader.Read())
                            {
                                Console.WriteLine($"{reader["TrainNumber"],-10} {reader["TrainName"],-20} {reader["Duration"],-10} {((TimeSpan)reader["Departure"]).ToString("hh\\:mm"),-10}");
                            }
                        }
                    }
                }

                Console.Write("\nEnter Train Number to book: ");
                int selectedTrainNumber = int.Parse(Console.ReadLine());
                Console.Write("Enter Class (1AC, 2AC, 3AC, 3E, Sleeper): ");
                string classType = Console.ReadLine();
                Console.Write("Enter Customer Name: ");
                string customerName = Console.ReadLine();

                using (SqlConnection connection = Connection.GetConnection())
                {
                    connection.Open();
                    SqlTransaction transaction = connection.BeginTransaction();
                    try
                    {
                        decimal totalCost = 0;
                        int seatsAvailable = 0;

                        string checkQuery = $"SELECT Cost_{classType}, Seats_{classType} FROM TrainDetails WHERE TrainNumber = @TrainNumber";
                        using (SqlCommand checkCommand = new SqlCommand(checkQuery, connection, transaction))
                        {
                            checkCommand.Parameters.AddWithValue("@TrainNumber", selectedTrainNumber);
                            using (SqlDataReader reader = checkCommand.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    totalCost = reader.GetDecimal(0);
                                    seatsAvailable = reader.GetInt32(1);
                                }
                            }
                        }

                        if (seatsAvailable > 0)
                        {
                            string pnr = Guid.NewGuid().ToString().Substring(0, 8).ToUpper();
                            string insertQuery = @"INSERT INTO Reservation (PNR, UserID, TrainNumber, ClassType, DateOfTravel, TotalCost, BerthAllotment, CustomerName)
                                               VALUES (@PNR, @UserID, @TrainNumber, @ClassType, @DateOfTravel, @TotalCost, 'Berth Allotted', @CustomerName)";
                            using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection, transaction))
                            {
                                insertCommand.Parameters.AddWithValue("@PNR", pnr);
                                insertCommand.Parameters.AddWithValue("@UserID", AuthService.LoggedInUserId);
                                insertCommand.Parameters.AddWithValue("@TrainNumber", selectedTrainNumber);
                                insertCommand.Parameters.AddWithValue("@ClassType", classType);
                                insertCommand.Parameters.AddWithValue("@DateOfTravel", dateOfTravel);
                                insertCommand.Parameters.AddWithValue("@TotalCost", totalCost);
                                insertCommand.Parameters.AddWithValue("@CustomerName", customerName);
                                insertCommand.ExecuteNonQuery();
                            }

                            string updateQuery = $"UPDATE TrainDetails SET Seats_{classType} = Seats_{classType} - 1 WHERE TrainNumber = @TrainNumber";
                            using (SqlCommand updateCommand = new SqlCommand(updateQuery, connection, transaction))
                            {
                                updateCommand.Parameters.AddWithValue("@TrainNumber", selectedTrainNumber);
                                updateCommand.ExecuteNonQuery();
                            }

                            transaction.Commit();
                            Console.WriteLine($"\nBooking Successful! Your PNR is: {pnr}");
                        }
                        else
                        {
                            transaction.Rollback();
                            Console.WriteLine("Booking Failed: No seats available.");
                        }
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        Console.WriteLine($"Booking Failed: {ex.Message}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            Console.ReadKey();
        }

        private static void ViewMyTickets()
        {
            Console.Clear();
            Console.WriteLine("--- My Tickets ---");
            string query = "SELECT R.PNR, T.TrainName, R.DateOfTravel, R.ClassType, R.IsCancelled FROM Reservation R JOIN TrainDetails T ON R.TrainNumber = T.TrainNumber WHERE R.UserID = @UserID AND R.IsDeleted = 0";

            using (SqlConnection connection = Connection.GetConnection())
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserID", AuthService.LoggedInUserId);

                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (!reader.HasRows)
                            {
                                Console.WriteLine("You have no reservations.");
                            }
                            else
                            {
                                Console.WriteLine($"{"PNR",-15} {"Train Name",-20} {"Date of Travel",-20} {"Class",-10} {"Cancelled",-10}");
                                Console.WriteLine("--------------------------------------------------------------------------------");
                                while (reader.Read())
                                {
                                    Console.WriteLine($"{reader["PNR"],-15} {reader["TrainName"],-20} {((DateTime)reader["DateOfTravel"]).ToString("yyyy-MM-dd"),-20} {reader["ClassType"],-10} {((bool)reader["IsCancelled"] ? "Yes" : "No"),-10}");
                                }
                            }
                        }
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                }
            }
            Console.ReadKey();
        }
        private static void CancelTicket()
        {
            Console.Clear();
            Console.WriteLine("--- Cancel a Ticket ---");
            try
            {
                Console.Write("Enter PNR to cancel: ");
                string pnr = Console.ReadLine();

                using (SqlConnection connection = Connection.GetConnection())
                {
                    connection.Open();
                    SqlTransaction transaction = connection.BeginTransaction();
                    try
                    {
                        int reservationID = 0;
                        string classType = "";
                        int trainNumber = 0;
                        decimal totalCost = 0;
                        bool isCancelled = false;

                        string getDetailsQuery = "SELECT ReservationID, ClassType, TrainNumber, TotalCost, IsCancelled FROM Reservation WHERE PNR = @PNR AND UserID = @UserID";
                        using (SqlCommand getDetailsCommand = new SqlCommand(getDetailsQuery, connection, transaction))
                        {
                            getDetailsCommand.Parameters.AddWithValue("@PNR", pnr);
                            getDetailsCommand.Parameters.AddWithValue("@UserID", AuthService.LoggedInUserId);
                            using (SqlDataReader reader = getDetailsCommand.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    reservationID = reader.GetInt32(0);
                                    classType = reader.GetString(1);
                                    trainNumber = reader.GetInt32(2);
                                    totalCost = reader.GetDecimal(3);
                                    isCancelled = reader.GetBoolean(4);
                                }
                            }
                        }

                        if (reservationID == 0)
                        {
                            Console.WriteLine("Invalid PNR or you do not have permission to cancel this ticket.");
                            transaction.Rollback();
                        }
                        else if (isCancelled)
                        {
                            Console.WriteLine("This ticket has already been cancelled.");
                            transaction.Rollback();
                        }
                        else
                        {
                            Console.Write($"Are you sure you want to cancel ticket with PNR: {pnr}? (Y/N): ");
                            if (Console.ReadLine().ToUpper() == "Y")
                            {
                                decimal refundAmount = totalCost / 2; // 50% refund

                                // Step 1: Update Reservation status
                                string updateReservationQuery = "UPDATE Reservation SET IsCancelled = 1 WHERE ReservationID = @ReservationID";
                                using (SqlCommand updateReservationCommand = new SqlCommand(updateReservationQuery, connection, transaction))
                                {
                                    updateReservationCommand.Parameters.AddWithValue("@ReservationID", reservationID);
                                    updateReservationCommand.ExecuteNonQuery();
                                }

                                // Step 2: Insert into Cancellation table
                                string insertCancellationQuery = "INSERT INTO Cancellation (ReservationID, RefundAmount) VALUES (@ReservationID, @RefundAmount)";
                                using (SqlCommand insertCancellationCommand = new SqlCommand(insertCancellationQuery, connection, transaction))
                                {
                                    insertCancellationCommand.Parameters.AddWithValue("@ReservationID", reservationID);
                                    insertCancellationCommand.Parameters.AddWithValue("@RefundAmount", refundAmount);
                                    insertCancellationCommand.ExecuteNonQuery();
                                }

                                // Step 3: Increment seats in TrainDetails
                                string updateTrainQuery = $"UPDATE TrainDetails SET Seats_{classType} = Seats_{classType} + 1 WHERE TrainNumber = @TrainNumber";
                                using (SqlCommand updateTrainCommand = new SqlCommand(updateTrainQuery, connection, transaction))
                                {
                                    updateTrainCommand.Parameters.AddWithValue("@TrainNumber", trainNumber);
                                    updateTrainCommand.ExecuteNonQuery();
                                }

                                transaction.Commit();
                                Console.WriteLine($"\nCancellation successful. A refund of {refundAmount:C} will be processed.");
                            }
                            else
                            {
                                Console.WriteLine("Cancellation aborted.");
                                transaction.Rollback();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        Console.WriteLine($"An error occurred during cancellation: {ex.Message}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            Console.ReadKey();
        }
    }
}





