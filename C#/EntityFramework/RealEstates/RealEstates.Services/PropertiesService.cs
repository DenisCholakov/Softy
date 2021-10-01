using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using AutoMapper.QueryableExtensions;
using RealEstates.Data;
using RealEstates.Models;
using RealEstates.Services.Models;

namespace RealEstates.Services
{
    public class PropertiesService : BaseService, IPropertiesService
    {
        private readonly ApplicationDbContext _context;

        public PropertiesService(ApplicationDbContext context)
        {
            this._context = context;
        }
        public void Add(string district, int floor, int maxFloor, int size, int yardSize, int year, string propertyType,
            string buildingType, int price)
        {
            var property = new Property
            {
                Size = size,
                Price = price <= 0 ? (int?) null : price,
                Floor = floor <= 0 || floor > 255 ? null : (byte?)floor,
                TotalFloors = maxFloor <= 0 || maxFloor > 255 ? null : (byte?)maxFloor,
                YardSize = yardSize <= 0 ? (int?) null : yardSize,
                Year = year <= 1800 ? (int?) null : year,
            };

            var dbDistrict = this._context.Districts.FirstOrDefault(x => x.Name == district) ?? new District {Name = district};
            property.District = dbDistrict;

            var dbPropertyType = this._context.PropertyTypes.FirstOrDefault(x => x.Name == propertyType) ??
                                 new PropertyType {Name = propertyType};
            property.PropertyType = dbPropertyType;

            var dbBuildingType = this._context.BuildingTypes.FirstOrDefault(x => x.Name == buildingType) ??
                                 new BuildingType {Name = buildingType};
            property.BuildingType = dbBuildingType;

            this._context.Properties.Add(property);
            this._context.SaveChanges();
        }

        public decimal AveragePricePerSquareMeter()
        {
            return this._context.Properties.Where(x => x.Price.HasValue)
                .Average(x => x.Price / (decimal) x.Size) ?? 0;
        }

        public IEnumerable<PropertyInfoFullData> GetFullData(int count)
        {
            var properties = this._context.Properties
                .Where(x => x.Floor.HasValue && x.Floor.Value > 1 && x.Floor.Value <= 8
                            && x.Year.HasValue && x.Year > 2015)
                .ProjectTo<PropertyInfoFullData>(this.Mapper.ConfigurationProvider)
                //.Select(x => new PropertyInfoFullData
                //{

                //})
                .OrderByDescending(x => x.Price)
                .ThenBy(x => x.Size)
                .ThenBy(x => x.Year)
                .Take(count)
                .ToArray();

            return properties;
        }

        public IEnumerable<PropertyInfoDto> Search(int minPrice, int maxPrice, int minSize, int maxSize)
        {
            var properties = this._context.Properties.Where(x =>
                    (x.Price >= minPrice && x.Price <= maxPrice) && (x.Size >= minSize && x.Size <= maxSize))
                .ProjectTo<PropertyInfoDto>(this.Mapper.ConfigurationProvider)
                .ToList();

            return properties;
        }
    }
}
