namespace MoiteRecepti.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MoiteRecepti.Data.Common.Models;

    public class Category : BaseDeletableModel<int>
    {
        public Category()
        {
            this.Recipes = new HashSet<Recipe>();
        }

        public string Name { get; set; }

        public ICollection<Recipe> Recipes { get; set; }
    }
}
