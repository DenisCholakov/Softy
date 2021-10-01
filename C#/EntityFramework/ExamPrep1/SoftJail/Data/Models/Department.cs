using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SoftJail.Data.Models
{
    public class Department
    {
        public Department()
        {
            this.Cells = new HashSet<Cell>();
            this.Officers = new HashSet<Officer>();
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<Cell> Cells { get; set; }

        public ICollection<Officer> Officers { get; set; }
    }
}