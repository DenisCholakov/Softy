using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace CSharpDbDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var emplyees = new List<Employee>();

            using(var connection = new SqlConnection("Server=.;Integrated Security=true;Database=SoftUni"))
            {
                connection.Open();
                string query = "SELECT CONCAT(FirstName, ' ' + MiddleName, LastName) AS name, Salary FROM Employees ORDER BY [FirstName]";
                string query2 = "SELECT FirstName, LastName, Salary FROM Emplyees";
                SqlCommand sqlCommand = new SqlCommand(query2, connection);

                using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                {
                    while (sqlDataReader.Read())
                    {
                        emplyees.Add(new Employee
                        {
                            FirstName = sqlDataReader["FirstName"] as string,
                            LastName = sqlDataReader["LastName"] as string
                        });

                        //Console.WriteLine(sqlDataReader["name"] + " => " + sqlDataReader["Salary"]);
                    }
                }
            }
        }
    }

    public class Employee
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal Salary { get; set; }
    }
}
