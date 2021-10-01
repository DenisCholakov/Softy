using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using TeisterMask.Data.Models.Enums;

namespace TeisterMask.DataProcessor.ExportDto
{
    [XmlType("Project")]
    public class ProjectExportModel
    {
        [XmlAttribute("TasksCount")]
        public int TasksCount { get; set; }

        public string ProjectName { get; set; }

        public string HasEndDate { get; set; }

        [XmlArray("Tasks")]
        public TaskExportModel[] Tasks { get; set; }
    }

    [XmlType("Task")]
    public class TaskExportModel
    {
        public string Name { get; set; }

        [XmlElement("Label")]
        public LabelType LabelType { get; set; }
    }
}
