using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RealEstates.Data;
using RealEstates.Models;

namespace RealEstates.Services
{
    public class TagService : BaseService, ITagService
    {
        private readonly ApplicationDbContext _context;
        private readonly IDistrictsService _districtsService;

        public TagService(ApplicationDbContext context, IDistrictsService districtsService)
        {
            this._context = context;
            this._districtsService = districtsService;
        }

        public void Add(string name, int? importance = null)
        {
            var tag = new Tag
            {
                Name = name,
                Importance = importance
            };

            this._context.Tags.Add(tag);
            this._context.SaveChanges();
        }

        public void BulkTagToProperties()
        {
            var allProperties = this._context.Properties;

            foreach (var property in allProperties)
            {
                var averagePriceForDistrict = this._districtsService.AveragePricePerSquareMeter(property.DistrictId);

                if (property.Price > averagePriceForDistrict)
                {
                    var tag = this.GetTag("expensive");
                    property.Tags.Add(tag);
                }
                else
                {
                    var tag = this.GetTag("cheap");
                    property.Tags.Add(tag);
                }


                var currentDate = DateTime.Now.AddYears(-15);

                if (property.Year.HasValue && property.Year <= currentDate.Year)
                {
                    var tag = this.GetTag("new building");
                    property.Tags.Add(tag);
                }
                else if (property.Year.HasValue)
                {
                    var tag = this.GetTag("old building");
                    property.Tags.Add(tag);
                }

                var averagePropertySize = this._districtsService.AveragePropertySize(property.DistrictId);

                if (property.Size > averagePropertySize)
                {
                    var tag = GetTag("big property");
                    property.Tags.Add(tag);
                }
                else
                {
                    var tag = GetTag("small property");
                    property.Tags.Add(tag);
                }

                if (property.Floor.HasValue && property.Floor.Value == 1)
                {
                    var tag = GetTag("first floor");
                    property.Tags.Add(tag);
                }
                else if (property.Floor.HasValue && property.TotalFloors.HasValue && property.Floor.Value == property.TotalFloors)
                {
                    var tag = GetTag("last floor");
                    property.Tags.Add(tag);
                }

                if (property.Floor.HasValue && property.Floor.Value >= 6)
                {
                    var tag = GetTag("nice view");
                    property.Tags.Add(tag);
                }
            }

            this._context.SaveChanges();
        }

        private Tag GetTag(string tagName)
        {
            var tag = this._context.Tags.FirstOrDefault(x => x.Name == tagName);
            return tag;
        }
    }
}
