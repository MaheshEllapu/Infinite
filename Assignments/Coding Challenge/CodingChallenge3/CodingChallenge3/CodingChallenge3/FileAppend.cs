using System;
using System.IO;

namespace CodingChallenge3
{
    class FileAppend
    {
        static void Main()
        {
            string path = "appendData.txt";

            Console.Write("Enter text to append : ");
            string input = Console.ReadLine();

            using (StreamWriter writer=new StreamWriter(path,true))
            {
                writer.WriteLine(input);
            }
            Console.WriteLine("Text appended successfully.");
            Console.Read();
        }
    }
}
