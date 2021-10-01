using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstates.Models
{
    public class Property
    {
        public Property()
        {
            this.Tags = new HashSet<Tag>();
        }

        public int Id { get; set; }

        public int Size { get; set; }

        public int? YardSize { get; set; }

        public byte? Floor { get; set; }

        public byte? TotalFloors { get; set; }

        public int? Year { get; set; }

        public int DistrictId { get; set; }

        /// <summary>
        /// Gets and Sets the property price is in Euro
        /// </summary>
        public int? Price { get; set; }

        public District District { get; set; }

        public int PropertyTypeId { get; set; }

        public PropertyType PropertyType { get; set; }

        public int BuildingTypeId { get; set; }

        public BuildingType BuildingType { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }
    }
}
