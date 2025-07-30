//1.Write a stored Procedure that inserts records in the Employee_Details table
 
//The procedure should generate the EmpId automatically to insert and should return the generated value to the user
 
//Also the Salary Column is a calculated column (Salary is givenSalary - 10%)
 
//Table: Employee_Details(Empid, Name, Salary, Gender)
//Hint(User should not give the EmpId)


//Test the Procedure using ADO classes and show the generated Empid and Salary

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace CodeChallenge6
{
    class InsertEmployee
    {
        public static SqlConnection con;
        public static SqlCommand cmd;

        static void Main(string[] args)
        {
            InsertData();
            Console.ReadLine();
        }

        static SqlConnection getConnection()
        {
            con = new SqlConnection("Data Source=ICS-LT-2R6BJ84\\SQLEXPRESS; Initial Catalog=Assessments;user id=sa; password=MaheshEllapu@123;");
            con.Open();
            return con;
        }

        static void InsertData()
        {
            try
            {
                con = getConnection();

                Console.WriteLine("Enter Employee Name:");
                string empname = Console.ReadLine();

                Console.WriteLine("Enter Gender (M/F):");
                string gender = Console.ReadLine();

                Console.WriteLine("Enter Given Salary:");
                decimal salary = Convert.ToDecimal(Console.ReadLine());

                cmd = new SqlCommand("InsertEmployee", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Name", empname);
                cmd.Parameters.AddWithValue("@Gender", gender);
                cmd.Parameters.AddWithValue("@Salary", salary);

                SqlParameter outEmpId = new SqlParameter("@GeneratedEmpid", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(outEmpId);

                SqlParameter outNetSalary = new SqlParameter("@NetSalary", SqlDbType.Decimal)
                {
                    Precision = 10,
                    Scale = 2,
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(outNetSalary);

                cmd.ExecuteNonQuery();

                Console.WriteLine("\nEmployee Record Inserted Successfully!");
                Console.WriteLine("Generated EmpId     : " + outEmpId.Value);
                Console.WriteLine("Calculated Net Salary: " + outNetSalary.Value);
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error: " + ex.Message);
            }
        }
    }
}