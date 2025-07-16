using System;
using System.Linq;

namespace Assignment_7
{
    class WordsFilter
    {
        static void Main()
        {
            Console.Write("Enter words : ");
            var result = Console.ReadLine().Split()
                .Where(w => w.StartsWith("a", StringComparison.OrdinalIgnoreCase) && w.EndsWith("m", StringComparison.OrdinalIgnoreCase));
            foreach(var word in result)
                Console.WriteLine(word);
            Console.Read();
        }
    }
}