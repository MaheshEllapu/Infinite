using System;

namespace CodingChallenge1
{
    class numberCheck
    {
        static void Main()
        {
            string[] parts = Console.ReadLine().Split(',');
            int a = int.Parse(parts[0]);
            int b = int.Parse(parts[1]);
            int c = int.Parse(parts[2]);

            int largest = Math.Max(a, Math.Max(b, c));

            //or

            //if (a >= b && a >= c)
            //    largest = a;
            //else if (b >= c)
            //    largest = b;
            //else
            //    largest = c;

            Console.WriteLine(largest);
            Console.Read();
        }
    }
}
