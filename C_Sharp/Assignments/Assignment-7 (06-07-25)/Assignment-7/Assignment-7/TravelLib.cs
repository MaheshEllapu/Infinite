namespace TravelLib
{
    public class Travel
    {
        public string CalculateConcession(int age, float fare)
        {
            if (age <= 5)
                return "Little Champs - Free Ticket";
            else if (age > 60)
                return $"SeniorCitizen - Fare : Rs.{fare * 0.7f}";
            else
                return $"Ticket Booked - Fare : Rs.{fare}";
        }
    }
}