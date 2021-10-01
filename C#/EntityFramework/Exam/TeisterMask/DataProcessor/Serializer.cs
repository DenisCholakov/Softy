using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Xml.Serialization;
using Newtonsoft.Json;
using TeisterMask.Data.Models.Enums;
using TeisterMask.DataProcessor.ExportDto;
using TeisterMask.DataProcessor.ImportDto;

namespace TeisterMask.DataProcessor
{
    using System;

    using Data;

    using Formatting = Newtonsoft.Json.Formatting;

    public class Serializer
    {
        public static string ExportProjectWithTheirTasks(TeisterMaskContext context)
        {
            var projects = context.Projects.Where(p => p.Tasks.Any())
                .ToArray()
                .Select(p => new ProjectExportModel
                {
                    TasksCount = p.Tasks.Count(),
                    ProjectName = p.Name,
                    HasEndDate = p.DueDate == null ? "No" : "Yes",
                    Tasks = p.Tasks.Select(t => new TaskExportModel
                    {
                        Name = t.Name,
                        LabelType = (LabelType) t.LabelType
                    }).OrderBy(t => t.Name).ToArray()
                }).OrderByDescending(p => p.TasksCount)
                .ThenBy(p => p.ProjectName).ToArray();
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ProjectExportModel[]), new XmlRootAttribute("Projects"));

            var textWriter = new StringWriter();
            var ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            xmlSerializer.Serialize(textWriter, projects, ns);

            var result = textWriter.ToString();

            return result;
        }

        public static string ExportMostBusiestEmployees(TeisterMaskContext context, DateTime date)
        {
            var employees = context.Employees.ToList()
                .Where(e => e.EmployeesTasks
                    .Select(et => et.Task.OpenDate).Any(tod => tod >= date))
                .Select(e => new
                {
                    e.Username,
                    Tasks = e.EmployeesTasks.Where(et => et.Task.OpenDate >= date)
                        .OrderByDescending(x => x.Task.DueDate)
                        .ThenBy(x => x.Task.Name)
                        .Select(et => new
                        {
                            TaskName = et.Task.Name,
                            OpenDate = et.Task.OpenDate.ToString("d",CultureInfo.InvariantCulture),
                            DueDate = et.Task.DueDate.ToString("d", CultureInfo.InvariantCulture),
                            LabelType = ((LabelType)et.Task.LabelType).ToString(),
                            ExecutionType = ((ExecutionType)et.Task.ExecutionType).ToString()
                        }).ToArray()
                }).OrderByDescending(x => x.Tasks.Count())
                .ThenBy(x => x.Username).Take(10).ToList();

            var result = JsonConvert.SerializeObject(employees, Formatting.Indented);

            return result;
        }
    }
}