using System;
using System.Collections.Generic;
using System.Text;
using RealEstates.Services.Models;

namespace RealEstates.Services
{
    public interface IPropertiesService
    {
        void Add(string district, int floor, int maxFloor, int size, int yardSize, int year, string propertyType,
            string buildingType, int price);

        decimal AveragePricePerSquareMeter();

        IEnumerable<PropertyInfoFullData> GetFullData(int count);

        IEnumerable<PropertyInfoDto> Search(int minPrice, int maxPrice, int minSize, int maxSize);
    }
}
