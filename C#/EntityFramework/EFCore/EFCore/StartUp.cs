using SoftUni.Data;
using System;
using System.Globalization;
using System.Linq;
using System.Runtime.Loader;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SoftUni.Models;

namespace SoftUni
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            SoftUniContext context = new SoftUniContext();
            var employees = RemoveTown(context);
            Console.WriteLine(employees);
        }

        public static string RemoveTown(SoftUniContext context)
        {
            var town = context.Towns.FirstOrDefault(t => t.Name == "Seattle");
            var adressesToRemove = context.Addresses.Where(a => a.TownId == town.TownId).ToList();
            var emplyeesTohaveAdressRemoved =
                context.Employees.Where(e => adressesToRemove.Contains(e.Address)).ToList();

            foreach (var employee in emplyeesTohaveAdressRemoved)
            {
                employee.AddressId = null;
            }

            foreach (var address in adressesToRemove)
            {
                context.Addresses.Remove(address);
            }

            context.Towns.Remove(town);

            context.SaveChanges();

            return $"{adressesToRemove.Count} addresses in Seattle were deleted";

        }

        public static string DeleteProjectById(SoftUniContext context)
        {
            var projectToDelete = context.Projects.FirstOrDefault(p => p.ProjectId == 2);
            var EmployeeProjectsToRemove = context.EmployeesProjects.Where(ep => ep.ProjectId == 2).ToList();
            context.EmployeesProjects.RemoveRange(EmployeeProjectsToRemove);
            context.Projects.Remove(projectToDelete);
            context.SaveChanges();

            var projects = context.Projects
                .Select(p => p.Name)
                .Take(10).ToList();

            var sb = new StringBuilder();

            foreach (var project in projects)
            {
                sb.AppendLine(project);
            }

            return sb.ToString().TrimEnd();
        }

        public static string GetEmployeesByFirstNameStartingWithSa(SoftUniContext context)
        {
            var employees = context.Employees
                .Where(e => e.FirstName.StartsWith("Sa"))
                .Select(e => new
                {
                    e.FirstName,
                    e.LastName,
                    e.JobTitle,
                    e.Salary
                }).OrderBy(e => e.FirstName)
                .ThenBy(e => e.LastName).ToList();

            var sb = new StringBuilder();

            foreach (var employee in employees)
            {
                sb.AppendLine($"{employee.FirstName} {employee.LastName} - {employee.JobTitle} - (${employee.Salary:f2})");
            }

            return sb.ToString().TrimEnd();
        }

        public static string IncreaseSalaries(SoftUniContext context)
        {
            var departmentsToChange = new string[] {"Engineering", "Tool Design", "Marketing", "Information Services"};

            var emplyees = context.Employees
                .Where(x => departmentsToChange.Contains(x.Department.Name))
                .OrderBy(x => x.FirstName)
                .ThenBy(x => x.LastName)
                .ToList();

            foreach (var employee in emplyees)
            {
                employee.Salary *= 1.12m;
            }

            context.SaveChanges();

            var sb = new StringBuilder();

            foreach (var employee in emplyees)
            {
                sb.AppendLine($"{employee.FirstName} {employee.LastName} (${employee.Salary:f2})");
            }

            return sb.ToString().TrimEnd();
        }

        public static string GetLatestProjects(SoftUniContext context)
        {
            var projects = context.Projects.OrderByDescending(p => p.StartDate)
                .Take(10).Select(p => new
                {
                    p.Name,
                    p.Description,
                    p.StartDate
                }).OrderBy(p => p.Name)
                .ToList();

            var sb = new StringBuilder();

            foreach (var project in projects)
            {
                sb.AppendLine($"{project.Name}");
                sb.AppendLine($"{project.Description}");
                sb.AppendLine($"{project.StartDate}");
            }

            return sb.ToString().TrimEnd();
        }

        public static string GetDepartmentsWithMoreThan5Employees(SoftUniContext context)
        {
            var departments = context.Departments.Where(d => d.Employees.Count > 5)
                .Include(x => x.Manager)
                .Include(x => x.Employees)
                .OrderBy(d => d.Employees.Count)
                .ThenBy(x => x.Name)
                .ToList();

            var sb = new StringBuilder();

            foreach (var department in departments)
            {
                sb.AppendLine($"{department.Name} - {department.Manager.FirstName} {department.Manager.LastName}");

                foreach (var employee in department.Employees)
                {
                    sb.AppendLine($"{employee.FirstName} {employee.LastName} - {employee.JobTitle}");
                }
            }

            return sb.ToString().TrimEnd();
        }

        public static string GetEmployee147(SoftUniContext context)
        {
            var employee = context.Employees
                .Select(e => new
                {
                    e.EmployeeId,
                    e.FirstName,
                    e.LastName,
                    e.JobTitle,
                    Projects = e.EmployeesProjects.Select(p => p.Project.Name)
                        .OrderBy(p => p).ToList()
                }).FirstOrDefault(e => e.EmployeeId == 147);

            var sb = new StringBuilder();

            sb.AppendLine($"{employee.FirstName} {employee.LastName} - {employee.JobTitle}");

            foreach (var project in employee.Projects)
            {
                sb.AppendLine(project);
            }

            return sb.ToString().TrimEnd();
        }

        public static string GetAddressesByTown(SoftUniContext context)
        {
            var addresses = context.Addresses
                .Select(a => new
                {
                    a.AddressText,
                    TownName = a.Town.Name,
                    EmployeesCount = a.Employees.Count
                }).OrderByDescending(a => a.EmployeesCount)
                .ThenBy(a => a.TownName)
                .ThenBy(a => a.AddressText)
                .Take(10)
                .ToList();

            var sb = new StringBuilder();

            foreach (var address in addresses)
            {
                sb.AppendLine($"{address.AddressText}, {address.TownName} - {address.EmployeesCount} employees");
            }

            return sb.ToString().TrimEnd();
        }

        public static string GetEmployeesInPeriod(SoftUniContext context)
        {
            var employees = context.Employees
                .Include(e => e.EmployeesProjects)
                .ThenInclude(ep => ep.Project)
                .Where(x => x.EmployeesProjects.Any(x => x.Project.StartDate.Year >= 2001 &&
                                                         x.Project.StartDate.Year <= 2003))
                .Select(e => new
                {
                    e.FirstName,
                    e.LastName,
                    ManagerFirstName = e.Manager.FirstName,
                    ManagerLastName = e.Manager.LastName,
                    Projects = e.EmployeesProjects.Select(p => new {p.Project.Name, p.Project.StartDate, p.Project.EndDate})
                })
                .Take(10)
                .ToList();

            var sb = new StringBuilder();

            foreach (var employee in employees)
            {
                sb.AppendLine($"{employee.FirstName} {employee.LastName} - Manager: {employee.ManagerFirstName} {employee.ManagerLastName}");

                foreach (var project in employee.Projects)
                {
                    var endDate = project.EndDate == null ? "not finished" : project.EndDate.ToString();

                    sb.AppendLine($"--{project.Name} - {project.StartDate} - {endDate}");
                }
            }

            return sb.ToString().TrimEnd();
        }

        public static string AddNewAddressToEmployee(SoftUniContext context)
        {
            var nakov = context.Employees.FirstOrDefault(e => e.LastName == "Nakov");
            nakov.Address = new Address
            {
                AddressText = "Vitoshka 15",
                TownId = 4
            }; ;

            context.SaveChanges();

            var addresses = context.Employees.Select(e => new
                {
                    e.Address.AddressText, 
                    e.Address.AddressId
                })
                .OrderByDescending(x => x.AddressId)
                .Take(10)
                .ToList();

            var sb = new StringBuilder();

            foreach (var address in addresses)
            {
                sb.AppendLine($"{address.AddressText}");
            }

            return sb.ToString().TrimEnd();
        }

        public static string GetEmployeesFromResearchAndDevelopment(SoftUniContext context)
        {
            var employees = context.Employees.Where(e => e.Department.Name == "Research and Development")
                .Select(e => new
                {
                    e.FirstName,
                    e.LastName,
                    e.Salary,
                    DepartmentName = e.Department.Name
                }).OrderBy(e => e.Salary).ThenByDescending(e => e.FirstName).ToList();

            var sb = new StringBuilder();

            foreach (var employee in employees)
            {
                sb.AppendLine($"{employee.FirstName} {employee.LastName} from {employee.DepartmentName} - ${employee.Salary:f2}");
            }

            return sb.ToString().TrimEnd();
        }

        public static string GetEmployeesWithSalaryOver50000(SoftUniContext context)
        {
            var employees = context.Employees.Where(e => e.Salary > 50000)
                .Select(e => new
                {
                    e.FirstName,
                    e.Salary
                }).OrderBy(e => e.FirstName).ToList();

            var sb = new StringBuilder();

            foreach (var employee in employees)
            {
                sb.AppendLine($"{employee.FirstName} - {employee.Salary:f2}");
            }

            return sb.ToString().TrimEnd();
        }

        public static string GetEmployeesFullInformation(SoftUniContext context)
        {
            var employees = context.Employees.Select(e => new
            {
                e.EmployeeId,
                e.FirstName,
                e.LastName,
                e.MiddleName,
                e.JobTitle,
                e.Salary
            }).OrderBy(e => e.EmployeeId).ToList();

            var sb = new StringBuilder();

            foreach (var employee in employees)
            {
                sb.AppendLine($"{employee.FirstName} {employee.LastName} {employee.MiddleName} {employee.JobTitle} {employee.Salary:f2}");
            }

            return sb.ToString().TrimEnd();
        }
    }
}
