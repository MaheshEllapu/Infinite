using System;
using TravelLib;

namespace Assignment_7
{
    class TravelApp
    {
        const float TotalFare = 500;

        static void Main()
        {
            Console.Write("Enter your Name : ");
            string name = Console.ReadLine();

            Console.Write("Enter your Age : ");
            int age = int.Parse(Console.ReadLine());

            Travel travel = new Travel();
            string result = travel.CalculateConcession(age, TotalFare);

            Console.WriteLine($"Hello {name}, {result}");
            Console.Read();
        }
    }
}
