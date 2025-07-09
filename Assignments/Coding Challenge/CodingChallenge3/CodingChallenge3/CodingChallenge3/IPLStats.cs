using System;

namespace CodingChallenge3
{
    class CricketTeam
    {
        public void PointsCalculation(int no_of_matches)
        {
            int sum = 0;
            for(int i=1; i<=no_of_matches; i++)
            {
                Console.Write($"Enter score for match {i} : ");
                sum += int.Parse(Console.ReadLine());
            }
            float avg = (float)sum / no_of_matches;
            Console.WriteLine($"Matches : {no_of_matches}, Sum : {sum}, Average : {avg}");
        }
    }
    class IPLStats
    {
        static void Main(string[] args)
        {
            Console.Write("Enter the number of matches : ");
            int matches = int.Parse(Console.ReadLine());
            CricketTeam team = new CricketTeam();
            team.PointsCalculation(matches);
            Console.Read();
        }
    }
}
