using System;
using System.Collections.Generic;
using System.Linq;

namespace CompanyRoster
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Department> departments = new List<Department>();
            int n = int.Parse(Console.ReadLine());

            for (int i = 0; i < n; i++)
            {
                string[] input = Console.ReadLine().Split().ToArray();
                int index = departments.FindIndex(x => x.Name == input[2]);
                if (index < 0)
                {
                    Department department = new Department(input[2]);
                    department.AddEmployee(input[0], decimal.Parse(input[1]));
                    departments.Add(department);
                }
                else
                {
                    departments[index].AddEmployee(input[0], decimal.Parse(input[1]));
                }
            }

            var dep = departments.OrderByDescending(x => x.DepSalary / x.employees.Count).First();
            System.Console.WriteLine($"Highest Average Salary: {dep.Name}");
            dep.employees.OrderByDescending(x => x.Salary).ToList().ForEach(x => System.Console.WriteLine(x));
        }
    }

    class Department
    {
        public string Name { get; set; }
        public List<Employee> employees { get; set; }
        public decimal DepSalary { get; set; }

        public void AddEmployee(string name, decimal salary)
        {
            this.employees.Add(new Employee(name, salary));
            DepSalary += salary;
        }
        public Department(string name)
        {
            this.Name = name;
            this.employees = new List<Employee>();
        }
    }

    public class Employee
    {
        public string Name { get; set; }
        public decimal Salary { get; set; }

        public Employee(string name, decimal salary)
        {
            this.Name = name;
            this.Salary = salary;
        }

        public override string ToString() => $"{this.Name} {this.Salary:f2}";
    }
}
