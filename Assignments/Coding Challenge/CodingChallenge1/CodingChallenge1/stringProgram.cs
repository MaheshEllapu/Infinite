using System;

namespace CodingChallenge1
{
    class stringProgram
    {
        static void Main()
        {
            string input = Console.ReadLine();
            if (input.Length <= 1)
            {
                Console.WriteLine(input);
                Console.Read();
            }
            else
            {
                string result = input[input.Length - 1] + input.Substring(1, input.Length - 2) + input[0];
                Console.WriteLine(result);
                Console.Read();
            }
        }
    }
}
