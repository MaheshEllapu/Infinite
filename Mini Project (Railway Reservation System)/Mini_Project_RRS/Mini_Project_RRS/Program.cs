using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mini_Project_RRS
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Railway Reservation System";
            while (true)
            {
                Console.Clear();
                Console.WriteLine("-------------------------------------------");
                Console.WriteLine(" Welcome to the Railway Reservation System ");
                Console.WriteLine("-------------------------------------------");

                if (AuthService.LoggedInUserId == 0) // Not logged in
                {
                    ShowAuthMenu();
                }
                else // Logged in
                {
                    if (AuthService.LoggedInUserRole == "Admin")
                    {
                        AdminMenu.ShowMenu();
                    }
                    else
                    {
                        UserMenu.ShowMenu();
                    }
                }
            }
        }

        private static void ShowAuthMenu()
        {
            Console.WriteLine("1. Login");
            Console.WriteLine("2. Register");
            Console.WriteLine("3. Exit");
            Console.Write("Enter your choice: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Write("Enter Username: ");
                    string loginUsername = Console.ReadLine();
                    Console.Write("Enter Password: ");
                    string loginPassword = Console.ReadLine();
                    if (AuthService.Login(loginUsername, loginPassword))
                    {
                        Console.WriteLine($"Login successful! Welcome, {AuthService.LoggedInUsername}!");
                    }
                    else
                    {
                        Console.WriteLine("Login failed. Please check your credentials or register.");
                    }
                    Console.ReadKey();
                    break;
                case "2":
                    Console.Clear();
                    Console.WriteLine("--- User Registration ---");
                    Console.Write("Enter Username: ");
                    string regUsername = Console.ReadLine();
                    Console.Write("Enter Email: ");
                    string regEmail = Console.ReadLine();
                    Console.Write("Enter Phone Number: ");
                    string regPhone = Console.ReadLine();
                    Console.Write("Enter Password: ");
                    string regPassword = Console.ReadLine();
                    if (AuthService.Register(regUsername, regEmail, regPhone, regPassword))
                    {
                        Console.WriteLine("Registration successful! You can now log in.");
                    }
                    Console.ReadKey();
                    break;
                case "3":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid choice. Press any key to try again.");
                    Console.ReadKey();
                    break;
            }
        }
    }
}
