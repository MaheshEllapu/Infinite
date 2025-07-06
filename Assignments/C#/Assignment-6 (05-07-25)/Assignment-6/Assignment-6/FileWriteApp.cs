using System;
using System.IO;

namespace Assignment_6
{
    class FileWriteApp
    {
        static void Main()
        {
            Console.Write("Enter number of lines to write : ");
            int n = int.Parse(Console.ReadLine());
            string[] lines = new string[n];

            for(int i=0; i<n; i++)
            {
                Console.Write($"Line {i + 1} : ");
                lines[i] = Console.ReadLine();
            }

            File.WriteAllLines("output.txt", lines);
            Console.WriteLine("Written to file : output.txt");
        }
    }
}
