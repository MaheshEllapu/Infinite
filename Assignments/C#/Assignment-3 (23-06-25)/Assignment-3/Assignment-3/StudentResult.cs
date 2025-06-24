using System;

namespace Assignment_3
{
    class StudentResult
    {
        static void Main()
        {
            Console.Write("Enter Roll No. : ");
            int rollno = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter Name : ");
            string name = Console.ReadLine();

            Console.Write("Enter Semester : ");
            string semester = Console.ReadLine();

            Console.Write("Enter Branch : ");
            string branch = Console.ReadLine();

            Student student = new Student(rollno, name, semester, branch);

            student.GetMarks();
            student.DisplayResult();
            student.DisplayData();
            Console.Read();
        }
    }
    class Student
    {
        private int rollno;
        private string name;
        private string semester;
        private string branch;
        private int[] marks = new int[5];

        public Student(int rollno,string name, string semester, string branch)
        {
            this.rollno = rollno;
            this.name = name;
            this.semester = semester;
            this.branch = branch;
        }

        public void GetMarks()
        {
            Console.WriteLine("Enter marks for 5 subjects : ");
            for(int i=0; i < 5; i++)
            {
                Console.Write($"Subject {i+1} :");
                marks[i] = Convert.ToInt32(Console.ReadLine());
            }
            Console.WriteLine("");
        }

        public void DisplayResult()
        {
            int total = 0;
            bool hasFailedSubject = false;
            for (int i=0; i < 5; i++)
                {
                total += marks[i];
                if(marks[i] < 35)
                {
                    hasFailedSubject = true;
                }
            }
            double average = (double)total / 5;
            Console.WriteLine("************ Result ************");
            if(hasFailedSubject)
            {
                Console.WriteLine("Result : Failed one or more subjects.");
            }
            else if(average<50)
            {
                Console.WriteLine("Result : Failed Average below 50");
            }
            else
            {
                Console.WriteLine("Result : PASSED");
            }
            Console.WriteLine("Average Marks : "+average);
        }
        public void DisplayData()
        {
            Console.WriteLine("");
            Console.WriteLine("************ Student Information ************");
            Console.WriteLine("Roll No. : "+rollno);
            Console.WriteLine("Name : "+name);
            Console.WriteLine("Semester : "+semester);
            Console.WriteLine("Branch : "+branch);
            Console.WriteLine("Marks : ");
            for(int i=0;i<5;i++)
            {
                Console.WriteLine($"Subject {i+1} : {marks[i]}");
            }
        }
    }

}
