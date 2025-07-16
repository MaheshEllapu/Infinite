using System;

namespace Assignment_5
{
    class InvalidMarksException : Exception
    {
        public InvalidMarksException(string msg) : base(msg) { }
    }

    class Scholarship
    {
        public float Merit(int marks, float fees)
        {
            if (marks >= 70 && marks <= 80)
                return fees * 0.2f;
            else if (marks >= 80 && marks <= 90)
                return fees * 0.3f;
            else if (marks > 90)
                return fees * 0.5f;
            else throw new InvalidMarksException("Not elegible for scholarship");
        }
    }
    class ScholarshipApp
    {
        static void Main()
        {
            Scholarship s = new Scholarship();
            try
            {
                Console.Write("Enter marks : ");
                int marks = int.Parse(Console.ReadLine());

                Console.Write("Enter total fees : ");
                float fees = float.Parse(Console.ReadLine());

                float scholarship = s.Merit(marks, fees);
                Console.WriteLine("Scholarship amount : Rs." + scholarship);
            }
            catch(InvalidMarksException e)
            {
                Console.WriteLine("Error : "+e.Message);
            }
            catch(FormatException)
            {
                Console.WriteLine("Error : Please enter valid numeric values");
            }
            Console.Read();
        }
    }
}
