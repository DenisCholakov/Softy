using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using RealEstates.Data;
using RealEstates.Services;

namespace RealEstates.Importer
{
    class Program
    {
        static void Main(string[] args)
        {
            var dbContext = new ApplicationDbContext();
            IPropertiesService propertiesService = new PropertiesService(dbContext);

            ImportJsonFile(propertiesService, "houses.json");
            Console.WriteLine();
            ImportJsonFile(propertiesService, "apartments.json");
        }

        private static void ImportJsonFile(IPropertiesService propertiesService, string jsonFileName)
        {
            var properties = JsonSerializer.Deserialize<IEnumerable<PropertyAsJson>>(File.ReadAllText(jsonFileName));

            foreach (var propertyAsJson in properties)
            {
                propertiesService.Add(propertyAsJson.District, propertyAsJson.Floor, propertyAsJson.TotalFloors,
                    propertyAsJson.Size, propertyAsJson.YardSize,
                    propertyAsJson.Year, propertyAsJson.Type, propertyAsJson.BuildingType, propertyAsJson.Price);
                Console.Write("-");
            }
        }
    }
}
