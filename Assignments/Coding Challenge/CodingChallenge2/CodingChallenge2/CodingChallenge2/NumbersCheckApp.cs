using System;

namespace CodingChallenge2
{
    class NumbersCheckApp
    {
        static void CheckNumber(int number)
        {
            if (number < 0)
                throw new ArgumentException("Number cannot be negative.");
            Console.WriteLine("Valid number : "+number);
        }

        static void Main()
        {
            try
            {
                Console.Write("Enter a number : ");
                int input = int.Parse(Console.ReadLine());

                CheckNumber(input);
            }

            catch(ArgumentException ex)
            {
                Console.WriteLine("Error : "+ex.Message);
            }
            catch(FormatException)
            {
                Console.WriteLine("Error : Please enter a valid integer.");
            }
            Console.Read();
        }
    }
}
