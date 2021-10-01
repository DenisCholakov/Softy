using System;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RealEstates.Data;
using RealEstates.Models;
using RealEstates.Services;
using RealEstates.Services.Models;

namespace RealEstates.ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.Unicode;
            var db = new ApplicationDbContext();
            db.Database.Migrate();

            while (true)
            {
                Console.WriteLine("Choose an option:"); 
                Console.WriteLine("1. Property search");
                Console.WriteLine("2. Most expensive districts");
                Console.WriteLine("3. Average price per square meter");
                Console.WriteLine("4. Add Tag");
                Console.WriteLine("5. Bulk tag to properties");
                Console.WriteLine("6. Property full info");
                Console.WriteLine("0. Exit");

                bool parsed = int.TryParse(Console.ReadLine(), out int option);

                if (parsed && option >= 1 && option <= 6)
                {
                    switch (option)
                    {
                        case 1:
                            PropertySearch(db);
                            break;
                        case 2:
                            MostExpensiveDistrict(db);
                            break;
                        case 3:
                            AveragePricePerSquareMeter(db);
                            break;
                        case 4:
                            AddTag(db);
                            break;
                        case 5:
                            BulkTagToProperties(db);
                            break;
                        case 6:
                            PropertyFullInfo(db);
                            break;
                    }

                    Console.WriteLine("Press any key to continiue...");
                    Console.ReadKey();
                }
                else if (parsed && option == 0)
                {
                    return;
                }

                Console.Clear();
            }
        }

        private static void PropertyFullInfo(ApplicationDbContext db)
        {
            Console.Write("Count of properties: ");
            int count = int.Parse(Console.ReadLine());

            IPropertiesService propertiesService = new PropertiesService(db);
            var result = propertiesService.GetFullData(count);

            var xmlSerializer = new XmlSerializer(typeof(PropertyInfoFullData[]));
            using var stringWriter = new StringWriter();
            xmlSerializer.Serialize(stringWriter, result);
            Console.WriteLine(stringWriter.ToString().TrimEnd());
        }

        private static void BulkTagToProperties(ApplicationDbContext db)
        {
            Console.WriteLine("Bulk operation started!");
            IDistrictsService districtsService = new DistrictsService(db);
            ITagService tagService = new TagService(db, districtsService);
            tagService.BulkTagToProperties();
            Console.WriteLine("Bulk operation finished!");
        }

        private static void AddTag(ApplicationDbContext db)
        {
            IDistrictsService districtsService = new DistrictsService(db);
            ITagService tagService = new TagService(db, districtsService);

            Console.Write("Tag name: ");
            string tagName = Console.ReadLine();

            Console.Write("Importance (optional): ");
            bool parsedImportance = int.TryParse(Console.ReadLine(), out int tagImportnace);

            if (parsedImportance)
            {
                tagService.Add(tagName, tagImportnace);
            }
            else
            {
                tagService.Add(tagName);
            }
        }

        private static void AveragePricePerSquareMeter(ApplicationDbContext context)
        {
            IPropertiesService propertiesSevice = new PropertiesService(context);
            Console.WriteLine("Average price per square meter: " + propertiesSevice.AveragePricePerSquareMeter());
        }

        private static void MostExpensiveDistrict(ApplicationDbContext context)
        {
            Console.Write("Districts count: ");
            int count = int.Parse((Console.ReadLine()));

            IDistrictsService districtsService = new DistrictsService(context);

            var districts = districtsService.GetMostExpensiveDistricts(count);

            foreach (var district in districts)
            {
                Console.WriteLine($"{district.Name} => {district.AveragePricePerSquareMeter:0.00} euro per square meter; {district.PropertiesCount}"); 
            }
        }

        private static void PropertySearch(ApplicationDbContext context)
        {
            Console.Write("Min price: ");
            int minPrice = int.Parse(Console.ReadLine());
            Console.Write("Max price: ");
            int maxPrice = int.Parse(Console.ReadLine());
            Console.Write("Min size: ");
            int minSize = int.Parse(Console.ReadLine());
            Console.Write("Max size: ");
            int maxSize = int.Parse(Console.ReadLine());

            IPropertiesService service = new PropertiesService(context);
            var properties = service.Search(minPrice, maxPrice, minSize, maxSize);

            foreach (var property in properties)
            {
                Console.WriteLine($"{property.DistrictName}; {property.BuildingType}; {property.Price} euro; {property.Size} square meters");
            }
        }
    }
}
