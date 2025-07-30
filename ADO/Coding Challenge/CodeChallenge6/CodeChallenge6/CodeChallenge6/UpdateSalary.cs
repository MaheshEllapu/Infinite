//2.Write a procedure that takes empid as input and outputs the updated salary as current salary + 100 for the give employee.
 
//  Test the procedure using ADO classes and display the Employee details of that employee whose salary has been updated

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace CodeChallenge6
{
    class UpdateSalary
    {
        public static SqlConnection con;
        public static SqlCommand cmd;

        static void Main(string[] args)
        {
            UpdatingSalary();
            Console.Read();
        }

        static SqlConnection getConnection()
        {
            con = new SqlConnection("Data Source=ICS-LT-2R6BJ84\\SQLEXPRESS; Initial Catalog=Assessments;user id=sa; password=MaheshEllapu@123;");
            con.Open();
            return con;
        }

        static void UpdatingSalary()
        {
            try
            {
                con = getConnection();

                Console.Write("Enter Employee ID: ");
                int empid = Convert.ToInt32(Console.ReadLine());

                cmd = new SqlCommand("UpdateEmployeeSalary", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@empid", empid);

                SqlParameter nameParam = new SqlParameter("@name", SqlDbType.VarChar, 100);
                nameParam.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(nameParam);

                SqlParameter genderParam = new SqlParameter("@gender", SqlDbType.Char, 1);
                genderParam.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(genderParam);

                SqlParameter updatedSalaryParam = new SqlParameter("@updated_salary", SqlDbType.Int);
                updatedSalaryParam.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(updatedSalaryParam);

                SqlParameter netSalaryParam = new SqlParameter("@net_salary", SqlDbType.Int);
                netSalaryParam.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(netSalaryParam);

                cmd.ExecuteNonQuery();

                Console.WriteLine("--- Updated Employee Details ---");
                Console.WriteLine("EmpId          : " + empid);
                Console.WriteLine("Name           : " + nameParam.Value);
                Console.WriteLine("Gender         : " + genderParam.Value);
                Console.WriteLine("Updated Salary : " + updatedSalaryParam.Value);
                Console.WriteLine("Net Salary     : " + netSalaryParam.Value);
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }
}
