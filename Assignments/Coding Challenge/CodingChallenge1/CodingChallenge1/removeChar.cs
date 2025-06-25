using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingChallenge1
{
    class removeChar
    {
        static void Main(string[] args)
        {
            string word = Console.ReadLine();
            int pos = int.Parse(Console.ReadLine());

            string result = word.Remove(pos, 1);
            Console.WriteLine(result);
            Console.Read();
        }
    }
}
