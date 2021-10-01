using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;
using TeisterMask.Data.Models;
using TeisterMask.Data.Models.Enums;

namespace TeisterMask.DataProcessor.ImportDto
{
    [XmlType("Task")]
    public class TaskInputModel
    {
        [Required]
        [MinLength(2)]
        [MaxLength(40)]
        public string Name { get; set; }

        [Required]
        [RegularExpression(@"[0-9]{2}\/[0-9]{2}\/[0-9]{4}")]
        public string OpenDate { get; set; }


        [RegularExpression(@"[0-9]{2}\/[0-9]{2}\/[0-9]{4}")]
        public string DueDate { get; set; }

        [EnumDataType(typeof(ExecutionType))]
        public int ExecutionType { get; set; }

        [EnumDataType(typeof(LabelType))]
        public int LabelType { get; set; }
    }
}
