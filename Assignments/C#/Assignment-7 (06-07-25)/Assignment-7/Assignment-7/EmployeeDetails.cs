using System;
using System.Collections.Generic;
using System.Linq;

namespace Assignment_7
{
    class Employee
    {
        public int EmpId;
        public string EmpName;
        public string EmpCity;
        public double EmpSalary;
    }

    class EmployeeDetails
    {
        static void Main()
        {
            var employees = new List<Employee>
            {
                new Employee{EmpId = 1, EmpName = "Mahesh", EmpCity = "Vizag", EmpSalary = 55000},
                new Employee{EmpId = 2, EmpName = "Yasaswini", EmpCity = "Chennai", EmpSalary = 65000},
                new Employee{EmpId = 3, EmpName = "Krishna Sai", EmpCity = "Bangalore", EmpSalary = 40000},
                new Employee{EmpId = 4, EmpName = "Pravallika", EmpCity = "Hyderabad", EmpSalary = 60000},
                new Employee{EmpId = 5, EmpName = "Mohan", EmpCity = "Pune", EmpSalary = 35000}
            };
            Console.WriteLine("\nSalary > 45000 : ");
            employees.Where(e => e.EmpSalary > 45000).ToList().ForEach(e => Print(e));

            Console.WriteLine("\nEmployees from Bangalore : ");
            employees.Where(e => e.EmpCity == "Bangalore").ToList().ForEach(e => Print(e));

            Console.WriteLine("\nEmployees sorted by name : ");
            employees.OrderBy(e => e.EmpName).ToList().ForEach(e => Print(e));

            Console.Read();
        }
        static void Print(Employee e) => Console.WriteLine($"EmpID : {e.EmpId} - {e.EmpName} - {e.EmpCity} - {e.EmpSalary}");
    }
}
