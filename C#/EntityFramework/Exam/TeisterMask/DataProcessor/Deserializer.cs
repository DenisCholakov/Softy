using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Castle.Core.Internal;
using Newtonsoft.Json;
using TeisterMask.Data.Models;
using TeisterMask.Data.Models.Enums;
using TeisterMask.DataProcessor.ImportDto;

namespace TeisterMask.DataProcessor
{
    using System;
    using System.Collections.Generic;

    using System.ComponentModel.DataAnnotations;

    using Data;

    using ValidationContext = System.ComponentModel.DataAnnotations.ValidationContext;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";

        private const string SuccessfullyImportedProject
            = "Successfully imported project - {0} with {1} tasks.";

        private const string SuccessfullyImportedEmployee
            = "Successfully imported employee - {0} with {1} tasks.";

        public static string ImportProjects(TeisterMaskContext context, string xmlString)
        {
            var result = new StringBuilder();

            var xmlSerializer = new XmlSerializer(typeof(ProjectInputModel[]), new XmlRootAttribute("Projects"));
            var reader = new StringReader(xmlString);

            var projectsDto = (ProjectInputModel[])xmlSerializer.Deserialize(reader);

            foreach (var xmlProject in projectsDto)
            {
                if (!IsValid(xmlProject))
                {
                    result.AppendLine("Invalid data!");
                    continue;
                }

                var project = new Project
                {
                    Name = xmlProject.Name,
                    OpenDate = DateTime.ParseExact(xmlProject.OpenDate, "dd/MM/yyyy", CultureInfo.InvariantCulture),
                    DueDate = xmlProject.DueDate.IsNullOrEmpty() ? (DateTime?) null : 
                                    DateTime.ParseExact(xmlProject.DueDate, "dd/MM/yyyy", CultureInfo.InvariantCulture),
                };

                foreach (var xmlTask in xmlProject.Tasks)
                {
                    var taskOpenDate =
                        DateTime.ParseExact(xmlTask.OpenDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    var taskDueDate =
                        DateTime.ParseExact(xmlTask.DueDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                    // ToDo: Change !IsValid()
                    if (taskOpenDate < project.OpenDate || taskDueDate > project.DueDate || !IsValid(xmlTask))
                    {
                        result.AppendLine("Invalid data!");
                        continue;
                    }

                    var task = new Task
                    {
                        Name = xmlTask.Name,
                        OpenDate = taskOpenDate,
                        DueDate = taskDueDate,
                        ExecutionType = (ExecutionType) xmlTask.ExecutionType,
                        LabelType = (LabelType) xmlTask.LabelType,
                    };

                    project.Tasks.Add(task);
                }

                context.Projects.Add(project);
                context.SaveChanges();

                result.AppendLine(String.Format(SuccessfullyImportedProject, project.Name, project.Tasks.Count));
            }

            return result.ToString().TrimEnd();
        }

        public static string ImportEmployees(TeisterMaskContext context, string jsonString)
        {
            var result = new StringBuilder();
            var employeesDto = JsonConvert.DeserializeObject<ICollection<EmployeesInputModel>>(jsonString);

            // if more tasks and only few inputs use firstOrDefault
            var tasksIds = context.Tasks.Select(t => t.Id).ToHashSet();

            foreach (var jsonEmployee in employeesDto)
            {
                if (!IsValid(jsonEmployee))
                {
                    result.AppendLine("Invalid data!");
                    continue;
                }

                var employee = new Employee
                {
                    Username = jsonEmployee.Username,
                    Email = jsonEmployee.Email,
                    Phone = jsonEmployee.Phone,
                };

                foreach (var task in jsonEmployee.Tasks.Distinct())
                {
                    if (!tasksIds.Contains(task))
                    {
                        result.AppendLine("Invalid data!");
                        continue;
                    }

                    employee.EmployeesTasks.Add(new EmployeeTask { TaskId = task});
                }

                context.Employees.Add(employee);
                context.SaveChanges();

                result.AppendLine(String.Format(SuccessfullyImportedEmployee, employee.Username,
                    employee.EmployeesTasks.Count));
            }

            return result.ToString().TrimEnd();
        }

        private static bool IsValid(object dto)
        {
            var validationContext = new ValidationContext(dto);
            var validationResult = new List<ValidationResult>();

            return Validator.TryValidateObject(dto, validationContext, validationResult, true);
        }
    }
}