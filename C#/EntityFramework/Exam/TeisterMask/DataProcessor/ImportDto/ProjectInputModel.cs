using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;
using TeisterMask.Data.Models;

namespace TeisterMask.DataProcessor.ImportDto
{
    [XmlType("Project")]
    public class ProjectInputModel
    {
        [Required]
        [StringLength(40, MinimumLength = 2)]
        public string Name { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]{2}\/[0-9]{2}\/[0-9]{4}$")]
        public string OpenDate { get; set; }

        [RegularExpression(@"^[0-9]{2}\/[0-9]{2}\/[0-9]{4}$")]
        public string DueDate { get; set; }

        [XmlArray("Tasks")]
        public TaskInputModel[] Tasks { get; set; }
    }
}
