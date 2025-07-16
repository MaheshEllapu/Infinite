using System;
using System.Linq;

namespace Assignment_7
{
    class NumSquare
    {
        static void Main(string[] args)
        {
            Console.Write("Enter numbers : ");
            var numbers = Console.ReadLine().Split().Select(int.Parse);

            var result = numbers.Where(n => n * n > 20).Select(n => $"{n}-{n * n}");

            foreach(var item in result)
                Console.WriteLine(item);
            Console.Read();
        }
    }
}