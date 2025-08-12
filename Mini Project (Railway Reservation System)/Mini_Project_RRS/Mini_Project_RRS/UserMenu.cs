using System;
using System.Collections.Generic;
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
                DateTime dateOfTravel;
                if (!DateTime.TryParse(Console.ReadLine(), out dateOfTravel))
                {
                    Console.WriteLine("Invalid date format. Please use YYYY-MM-DD.");
                    Console.ReadKey();
                    return;
                }

                if (dateOfTravel.Date < DateTime.Now.Date)
                {
                    Console.WriteLine("Booking failed: You cannot book a ticket for a past date.");
                    Console.ReadKey();
                    return;
                }

                int selectedTrainNumber;
                string selectedClass;
                int numberOfSeats;
                decimal costPerSeat = 0;
                int seatsAvailable = 0;
                TimeSpan departureTime = new TimeSpan();

                using (SqlConnection connection = Connection.GetConnection())
                {
                    connection.Open();
                    string query = "SELECT * FROM TrainDetails WHERE LOWER(Source) = LOWER(@Source) AND LOWER(Destination) = LOWER(@Destination) AND IsDeleted = 0";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Source", source);
                        command.Parameters.AddWithValue("@Destination", destination);
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
                if (!int.TryParse(Console.ReadLine(), out selectedTrainNumber))
                {
                    Console.WriteLine("Invalid train number.");
                    Console.ReadKey();
                    return;
                }

                using (SqlConnection connection = Connection.GetConnection())
                {
                    connection.Open();
                    string trainDetailsQuery = "SELECT * FROM TrainDetails WHERE TrainNumber = @TrainNumber AND IsDeleted = 0";
                    using (SqlCommand command = new SqlCommand(trainDetailsQuery, connection))
                    {
                        command.Parameters.AddWithValue("@TrainNumber", selectedTrainNumber);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                departureTime = (TimeSpan)reader["Departure"];

                                if (dateOfTravel.Date == DateTime.Now.Date && departureTime < DateTime.Now.TimeOfDay)
                                {
                                    Console.WriteLine("Booking failed: This train has already departed today.");
                                    Console.ReadKey();
                                    return;
                                }

                                Console.WriteLine("\nAvailable Classes and Costs:");
                                Console.WriteLine("-------------------------------------------------------");
                                Console.WriteLine($"{"Class",-10} {"Available Seats",-15} {"Cost per Seat",-15}");
                                Console.WriteLine("-------------------------------------------------------");
                                Console.WriteLine($"{"1AC",-10} {reader["Seats_1AC"],-15} {reader["Cost_1AC"],-15:C}");
                                Console.WriteLine($"{"2AC",-10} {reader["Seats_2AC"],-15} {reader["Cost_2AC"],-15:C}");
                                Console.WriteLine($"{"3AC",-10} {reader["Seats_3AC"],-15} {reader["Cost_3AC"],-15:C}");
                                Console.WriteLine($"{"3E",-10} {reader["Seats_3E"],-15} {reader["Cost_3E"],-15:C}");
                                Console.WriteLine($"{"Sleeper",-10} {reader["Seats_Sleeper"],-15} {reader["Cost_Sleeper"],-15:C}");
                                Console.WriteLine("-------------------------------------------------------");
                            }
                            else
                            {
                                Console.WriteLine("Invalid train number.");
                                Console.ReadKey();
                                return;
                            }
                        }
                    }
                }

                Console.Write("\nEnter Class to book (1AC, 2AC, etc.): ");
                selectedClass = Console.ReadLine();
                Console.Write("Enter number of seats to book: ");
                if (!int.TryParse(Console.ReadLine(), out numberOfSeats) || numberOfSeats <= 0)
                {
                    Console.WriteLine("Invalid number of seats.");
                    Console.ReadKey();
                    return;
                }

                using (SqlConnection connection = Connection.GetConnection())
                {
                    connection.Open();
                    string checkQuery = $"SELECT Cost_{selectedClass}, Seats_{selectedClass} FROM TrainDetails WHERE TrainNumber = @TrainNumber";
                    using (SqlCommand checkCommand = new SqlCommand(checkQuery, connection))
                    {
                        checkCommand.Parameters.AddWithValue("@TrainNumber", selectedTrainNumber);
                        using (SqlDataReader reader = checkCommand.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                costPerSeat = reader.GetDecimal(0);
                                seatsAvailable = reader.GetInt32(1);
                            }
                        }
                    }
                }

                if (numberOfSeats > seatsAvailable)
                {
                    Console.WriteLine($"Booking Failed: Only {seatsAvailable} seats are available in {selectedClass}.");
                    Console.ReadKey();
                    return;
                }

                List<Reservation> passengers = new List<Reservation>();
                for (int i = 0; i < numberOfSeats; i++)
                {
                    string name;
                    int age;
                    string gender;

                    Console.WriteLine($"\nEnter details for passenger {i + 1}:");
                    Console.Write("Name: ");
                    name = Console.ReadLine();
                    Console.Write("Age: ");
                    while (!int.TryParse(Console.ReadLine(), out age) || age <= 0)
                    {
                        Console.Write("Invalid age. Please enter a valid number: ");
                    }
                    Console.Write("Gender (M/F/O): ");
                    gender = Console.ReadLine();
                    while (gender.ToUpper() != "M" && gender.ToUpper() != "F" && gender.ToUpper() != "O")
                    {
                        Console.Write("Invalid gender. Please enter M, F, or O: ");
                        gender = Console.ReadLine();
                    }

                    passengers.Add(new Reservation
                    {
                        CustomerName = name,
                        Age = age,
                        Gender = gender
                    });
                }

                using (SqlConnection connection = Connection.GetConnection())
                {
                    connection.Open();
                    SqlTransaction transaction = connection.BeginTransaction();
                    try
                    {
                        string insertQuery = @"INSERT INTO Reservation (PNR, UserID, TrainNumber, ClassType, DateOfTravel, DateOfBooking, TotalCost, BerthAllotment, CustomerName, Age, Gender, IsCancelled, IsDeleted)
                                           VALUES (@PNR, @UserID, @TrainNumber, @ClassType, @DateOfTravel, GETDATE(), @TotalCost, @BerthAllotment, @CustomerName, @Age, @Gender, 0, 0)";

                        string pnr = Guid.NewGuid().ToString().Substring(0, 8).ToUpper();
                        decimal totalCost = costPerSeat * numberOfSeats;

                        foreach (var passenger in passengers)
                        {
                            using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection, transaction))
                            {
                                insertCommand.Parameters.AddWithValue("@PNR", pnr);
                                insertCommand.Parameters.AddWithValue("@UserID", AuthService.LoggedInUserId);
                                insertCommand.Parameters.AddWithValue("@TrainNumber", selectedTrainNumber);
                                insertCommand.Parameters.AddWithValue("@ClassType", selectedClass);
                                insertCommand.Parameters.AddWithValue("@DateOfTravel", dateOfTravel);
                                insertCommand.Parameters.AddWithValue("@TotalCost", totalCost);
                                insertCommand.Parameters.AddWithValue("@BerthAllotment", "Berth Allotted");
                                insertCommand.Parameters.AddWithValue("@CustomerName", passenger.CustomerName);
                                insertCommand.Parameters.AddWithValue("@Age", passenger.Age);
                                insertCommand.Parameters.AddWithValue("@Gender", passenger.Gender);
                                insertCommand.ExecuteNonQuery();
                            }
                        }

                        string updateQuery = $"UPDATE TrainDetails SET Seats_{selectedClass} = Seats_{selectedClass} - @NumberOfSeats WHERE TrainNumber = @TrainNumber";
                        using (SqlCommand updateCommand = new SqlCommand(updateQuery, connection, transaction))
                        {
                            updateCommand.Parameters.AddWithValue("@NumberOfSeats", numberOfSeats);
                            updateCommand.Parameters.AddWithValue("@TrainNumber", selectedTrainNumber);
                            updateCommand.ExecuteNonQuery();
                        }

                        transaction.Commit();
                        Console.WriteLine($"\nBooking Successful! Your PNR is: {pnr}");
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
            string query = "SELECT R.PNR, T.TrainName, R.CustomerName, R.Age, R.Gender, R.DateOfTravel, R.ClassType, R.IsCancelled FROM Reservation R JOIN TrainDetails T ON R.TrainNumber = T.TrainNumber WHERE R.UserID = @UserID AND R.IsDeleted = 0";

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
                                Console.WriteLine($"{"PNR",-15} {"Train Name",-20} {"Customer Name",-20} {"Age",-5} {"Gender",-8} {"Date of Travel",-20} {"Class",-10} {"Cancelled",-10}");
                                Console.WriteLine("--------------------------------------------------------------------------------------------------------------------");
                                while (reader.Read())
                                {
                                    Console.WriteLine($"{reader["PNR"],-15} {reader["TrainName"],-20} {reader["CustomerName"],-20} {reader["Age"],-5} {reader["Gender"],-8} {((DateTime)reader["DateOfTravel"]).ToString("yyyy-MM-dd"),-20} {reader["ClassType"],-10} {((bool)reader["IsCancelled"] ? "Yes" : "No"),-10}");
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
                Console.Write("Enter PNR to cancel a ticket from: ");
                string pnr = Console.ReadLine();

                using (SqlConnection connection = Connection.GetConnection())
                {
                    connection.Open();
                    SqlTransaction transaction = connection.BeginTransaction();
                    try
                    {
                        string passengerQuery = "SELECT CustomerName, ReservationID FROM Reservation WHERE PNR = @PNR AND UserID = @UserID AND IsCancelled = 0 AND IsDeleted = 0";
                        Dictionary<string, int> passengers = new Dictionary<string, int>();

                        using (SqlCommand command = new SqlCommand(passengerQuery, connection, transaction))
                        {
                            command.Parameters.AddWithValue("@PNR", pnr);
                            command.Parameters.AddWithValue("@UserID", AuthService.LoggedInUserId);
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                if (!reader.HasRows)
                                {
                                    Console.WriteLine("Invalid PNR, or all tickets for this PNR are already cancelled.");
                                    transaction.Rollback();
                                    Console.ReadKey();
                                    return;
                                }

                                Console.WriteLine("\nPassengers on this PNR:");
                                int i = 1;
                                while (reader.Read())
                                {
                                    string customerName = reader["CustomerName"].ToString();
                                    int reservationId = Convert.ToInt32(reader["ReservationID"]);
                                    Console.WriteLine($"{i}. {customerName}");
                                    passengers.Add(customerName, reservationId);
                                    i++;
                                }
                            }
                        }

                        Console.Write("\nEnter the name of the passenger to cancel: ");
                        string passengerName = Console.ReadLine();

                        if (!passengers.ContainsKey(passengerName))
                        {
                            Console.WriteLine("Invalid passenger name. Cancellation aborted.");
                            transaction.Rollback();
                            Console.ReadKey();
                            return;
                        }

                        int selectedReservationID = passengers[passengerName];

                        Console.Write($"\nAre you sure you want to cancel the ticket for {passengerName}? (Y/N): ");
                        if (Console.ReadLine().ToUpper() == "Y")
                        {
                            // Get the cost of a SINGLE ticket from the TrainDetails table
                            string getClassAndTrainQuery = "SELECT R.ClassType, T.TrainNumber, T.Cost_1AC, T.Cost_2AC, T.Cost_3AC, T.Cost_3E, T.Cost_Sleeper FROM Reservation R JOIN TrainDetails T ON R.TrainNumber = T.TrainNumber WHERE R.ReservationID = @ReservationID";
                            string classType = "";
                            int trainNumber = 0;
                            decimal individualTicketCost = 0;

                            using (SqlCommand getDetailsCommand = new SqlCommand(getClassAndTrainQuery, connection, transaction))
                            {
                                getDetailsCommand.Parameters.AddWithValue("@ReservationID", selectedReservationID);
                                using (SqlDataReader reader = getDetailsCommand.ExecuteReader())
                                {
                                    if (reader.Read())
                                    {
                                        classType = reader.GetString(reader.GetOrdinal("ClassType"));
                                        trainNumber = reader.GetInt32(reader.GetOrdinal("TrainNumber"));

                                        // Get the cost based on the class type
                                        individualTicketCost = reader.GetDecimal(reader.GetOrdinal($"Cost_{classType}"));
                                    }
                                }
                            }

                            decimal refundAmount = individualTicketCost / 2;

                            string updateReservationQuery = "UPDATE Reservation SET IsCancelled = 1 WHERE ReservationID = @ReservationID";
                            using (SqlCommand updateReservationCommand = new SqlCommand(updateReservationQuery, connection, transaction))
                            {
                                updateReservationCommand.Parameters.AddWithValue("@ReservationID", selectedReservationID);
                                updateReservationCommand.ExecuteNonQuery();
                            }

                            string insertCancellationQuery = "INSERT INTO Cancellation (PNR, ReservationID, RefundAmount) VALUES (@PNR, @ReservationID, @RefundAmount)";
                            using (SqlCommand insertCancellationCommand = new SqlCommand(insertCancellationQuery, connection, transaction))
                            {
                                insertCancellationCommand.Parameters.AddWithValue("@PNR", pnr);
                                insertCancellationCommand.Parameters.AddWithValue("@ReservationID", selectedReservationID);
                                insertCancellationCommand.Parameters.AddWithValue("@RefundAmount", refundAmount);
                                insertCancellationCommand.ExecuteNonQuery();
                            }

                            string updateTrainQuery = $"UPDATE TrainDetails SET Seats_{classType} = Seats_{classType} + 1 WHERE TrainNumber = @TrainNumber";
                            using (SqlCommand updateTrainCommand = new SqlCommand(updateTrainQuery, connection, transaction))
                            {
                                updateTrainCommand.Parameters.AddWithValue("@TrainNumber", trainNumber);
                                updateTrainCommand.ExecuteNonQuery();
                            }

                            transaction.Commit();
                            Console.WriteLine($"\nCancellation successful. A refund of {refundAmount:C} will be processed for {passengerName}.");
                        }
                        else
                        {
                            Console.WriteLine("Cancellation aborted.");
                            transaction.Rollback();
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
