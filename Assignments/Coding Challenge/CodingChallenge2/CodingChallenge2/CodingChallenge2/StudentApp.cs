using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingChallenge2
{

    abstract class Student
    {
        public string Name;
        public int StudentId;
        public double Grade;

        public Student(string name, int id, double grade)
        {
            Name = name;
            StudentId = id;
            Grade = grade;
        }
        public abstract bool IsPassed(double grade);
    }

    class Undergraduate : Student
    {
        public Undergraduate(string name, int id, double grade) : base(name, id, grade) { }
        public override bool IsPassed(double grade) => grade > 70.0;
    }

    class Graduate : Student
    {
        public Graduate(string name, int id, double grade) : base(name, id, grade) { }
        public override bool IsPassed(double grade) => grade > 80.0;
    }
    class StudentApp
    {
        static void Main(string[] args)
        {
            Console.Write("Enter Student Name : ");
            string name = Console.ReadLine();

            Console.Write("Enter Student ID : ");
            int id = int.Parse(Console.ReadLine());

            Console.Write("Enter Grade : ");
            double grade=double.Parse(Console.ReadLine());

            Console.Write("Enter Student Type (UG for Undergraduate / PG for Graduate) : ");
            string type = Console.ReadLine().ToUpper();

            Student student;

            if (type == "UG")
                student = new Undergraduate(name, id, grade);
            else if (type == "PG")
                student = new Graduate(name, id, grade);
            else
            {
                Console.WriteLine("Invalid student type.");
                return;
            }

            Console.WriteLine($"{student.Name} ({student.StudentId}) - {(student.IsPassed(student.Grade)?"Passed":"Failed")}");
            Console.Read();
        }
    }
}
