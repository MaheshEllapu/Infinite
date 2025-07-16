using System;
using System.IO;

namespace Assignment_6
{
    class FileLineCounter
    {
        static void Main()
        {
            Console.Write("Enter full file path : ");
            string filePath = Console.ReadLine();

            if(File.Exists(filePath))
            {
                int lineCount = File.ReadAllLines(filePath).Length;
                Console.WriteLine("Total lines : "+lineCount );
            }
            else
            {
                Console.WriteLine("File not found.");
            }
            Console.Read();
        }
    }
}
