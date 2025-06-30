using System;
using System.Collections.Generic;

namespace HandsOn6
{
    class Employee
    {
        public int Id;
        public string Name;
        public string Department;
        public double Salary;
    }
    class Program
    {
        static List<Employee> employees = new List<Employee>();
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("\n1. Add 2.View #.Search 4.Update 5.Delete 6.Exit");
                Console.Write("Choice : ");

                try
                {
                    int choice = int.Parse(Console.ReadLine());
                    if (choice == 6)
                        break;

                    switch (choice)
                    {
                        case 1:
                            {
                                Add();
                                break;
                            }
                        case 2:
                            {
                                View();
                                break;
                            }
                        case 3:
                            {
                                Search();
                                break;
                            }
                        case 4:
                            {
                                Update();
                                break;
                            }
                        case 5:
                            {
                                Delete();
                                break;
                            }
                        default:
                            {
                                Console.WriteLine("Invalid option.");
                                break;
                            }
                    }
                }
                catch { Console.WriteLine("Invalid input."); }
            }
        }
        static void Add()
        {
            try
            {
                Employee e = new Employee();
                Console.Write("Id : ");
                e.Id = int.Parse(Console.ReadLine());
                Console.Write("Name : ");
                e.Name = Console.ReadLine();
                Console.Write("Dept. : ");
                e.Department = Console.ReadLine();
                Console.Write("Salary : ");
                e.Salary = double.Parse(Console.ReadLine());
                employees.Add(e);
                Console.WriteLine("Employees added.");
            }
            catch { Console.WriteLine("Invalid data."); }
        }

        static void View()
        {
            if(employees.Count == 0)
                Console.WriteLine("No records.");
            else foreach (var e in employees)
                    Console.WriteLine($"{e.Id} {e.Name} {e.Department} Rs.{e.Salary}");
        }

        static void Search()
        {
            Console.Write("Enter Id : ");
            int id = int.Parse(Console.ReadLine());
            var e = employees.Find(x => x.Id == id);
            if(e!=null)
                Console.WriteLine($"{e.Id} {e.Name} {e.Department} Rs.{e.Salary}");
            else
                Console.WriteLine("Not found.");
        }

        static void Update()
        {
            Console.Write("Enter ID : ");
            int id = int.Parse(Console.ReadLine());
            var e = employees.Find(x => x.Id == id);
            if(e!=null)
            {
                Console.Write("New Name :");
                e.Name = Console.ReadLine();
                Console.Write("New Dept. :");
                e.Department = Console.ReadLine();
                Console.Write("New Salary : ");
                e.Salary = double.Parse(Console.ReadLine());
                Console.WriteLine("Updated.");
            }
            else
                Console.WriteLine("Not found.");
        }

        static void Delete()
        {
            Console.Write("Enter ID :");
            int id = int.Parse(Console.ReadLine());
            var e = employees.Find(x => x.Id == id);
            if(e!=null)
            {
                employees.Remove(e);
                Console.WriteLine("Deleted.");
            }
            else
                Console.WriteLine("Not found.");
        }
    }
}
